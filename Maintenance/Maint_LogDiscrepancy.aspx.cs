using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using BW_WebApp.DataManagers;

namespace BW_WebApp.Maintenance
{
    public partial class Maint_LogDiscrepancy : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            MainGrid.SelectedIndexChanged += new EventHandler(MainGrid_SelectedIndexChanged);
            //chkResolved.CheckedChanged += new EventHandler(chkResolved_CheckedChanged);
            drpClientList.SelectedIndexChanged += new EventHandler(drpClientList_SelectedIndexChanged);
            drpLocationList.SelectedIndexChanged += new EventHandler(drpLocationList_SelectedIndexChanged);

            AddType.SelectedIndexChanged += new EventHandler(AddType_SelectedIndexChanged);
            drpAddDiscrepancy.SelectedIndexChanged += new EventHandler(drpAddDiscrepancy_SelectedIndexChanged);
            drpEditType.SelectedIndexChanged += new EventHandler(drpEditType_SelectedIndexChanged);
            drpEditDiscrepancy.SelectedIndexChanged += new EventHandler(drpEditDiscrepancy_SelectedIndexChanged);

            btnScanKeyGo.Click += new EventHandler(btnScanKeyGo_Click);

            btnSearch.Click += new EventHandler(btnSearch_Click);


            btnAdd.Click += new EventHandler(btnAdd_Click);
            btnEdit.Click += new EventHandler(btnEdit_Click);
            btnDelete.Click += new EventHandler(btnDelete_Click);

            btnAddOK.Click += new EventHandler(btnAddOK_Click);
            btnAddCancel.Click += new EventHandler(btnAddCancel_Click);
            btnEditOK.Click += new EventHandler(btnEditOK_Click);
            btnEditCancel.Click += new EventHandler(btnEditCancel_Click);

            //ChildGrid.SelectedIndexChanged += new EventHandler(ChildGrid_SelectedIndexChanged);
            if (!IsPostBack)
            {
                hdnUserName.Value = User.Identity.Name;
                txtEndDate.Text = DateTime.Now.AddDays(1).ToString("MM/dd/yyyy");
                txtBeginDate.Text = DateTime.Now.AddDays(-30).ToString("MM/dd/yyyy");
                clsLinqDataContext ctx = new clsLinqDataContext();
                // ProcessManager qm = new ProcessManager(User.Identity.Name);
                pnlAdd.Visible = false;
                pnlEditx.Visible = false;
                LoadClientList();
                LoadDropDownValues();
            }
        }

        void btnScanKeyGo_Click(object sender, EventArgs e)
        {
            ClientManager cm = new ClientManager(User.Identity.Name);
            ClientLocation cl = cm.GetClientLocation(txtScanKey.Text);
            if (cl != null)
            {
                ListItem _ListItem = drpClientList.Items.FindByValue(cl.ClientID.ToString());
                if (_ListItem != null)
                {
                    drpClientList.SelectedIndex = drpClientList.Items.IndexOf(_ListItem);
                    LoadClientLocationList();

                    _ListItem = drpLocationList.Items.FindByValue(cl.ClientLocationID.ToString());
                    if (_ListItem == null)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "onLoad", "alert('Location not found in clientlocation list');", true);
                    }
                    else { drpLocationList.SelectedIndex = drpLocationList.Items.IndexOf(_ListItem); }
                    UpdateMainGrid();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "onLoad", "alert('Location not found in client list');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "onLoad", "alert('Location not found');", true);
            }
        }

        void drpEditDiscrepancy_SelectedIndexChanged(object sender, EventArgs e)
        {
            ResetEditOutCome();
        }
        void drpEditType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ResetEditDiscrepancy();
        }
        void drpAddDiscrepancy_SelectedIndexChanged(object sender, EventArgs e)
        {
            ResetAddOutCome();
        }
        void AddType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ResetAddDiscrepancy();
        }

        void btnSearch_Click(object sender, EventArgs e)
        {
            UpdateMainGrid();
            SearchIMEI.Text = "";
            SearchID.Text = "";
            SearchScanKey.Text = "";
        }


        void LoadDropDownValues()
        {
            QuestionManager qm = new QuestionManager(User.Identity.Name);
            List<Option> LO = new List<Option>();
            AddType.Items.Clear();
            drpEditType.Items.Clear();
            LO = qm.GetQuestionOptionList("Discr Type").OrderBy(x => x.Sequence).ThenBy(x => x.OptionText).ToList();
            foreach (Option o in LO)
            {
                ListItem li = new ListItem(o.OptionText, o.OptionID.ToString());
                ListItem li2 = new ListItem(o.OptionText, o.OptionID.ToString());
                AddType.Items.Add(li);
                drpEditType.Items.Add(li2);
            }
            AddType.SelectedIndex = 0;

            drpAddDiscrepancy.Items.Clear();
            drpEditDiscrepancy.Items.Clear();
            LO = qm.GetQuestionOptionList("Discr Desc").OrderBy(x => x.Sequence).ThenBy(x => x.OptionText).ToList();
            foreach (Option o in LO)
            {
                ListItem li = new ListItem(o.OptionText, o.OptionID.ToString());
                ListItem li2 = new ListItem(o.OptionText, o.OptionID.ToString());
                drpAddDiscrepancy.Items.Add(li);
                drpEditDiscrepancy.Items.Add(li2);
            }
            drpAddDiscrepancy.SelectedIndex = 0;

            drpAddOutcome.Items.Clear();
            drpEditOutcome.Items.Clear();
            LO = qm.GetQuestionOptionList("Discr OutCome").OrderBy(x => x.Sequence).ThenBy(x => x.OptionText).ToList();
            foreach (Option o in LO)
            {
                ListItem li = new ListItem(o.OptionText, o.OptionID.ToString());
                ListItem li2 = new ListItem(o.OptionText, o.OptionID.ToString());
                drpAddOutcome.Items.Add(li);
                drpEditOutcome.Items.Add(li2);
            }
            drpAddOutcome.SelectedIndex = 0;
        }
        void ResetAddDiscrepancy()
        {
            decimal MID = -1;
            decimal ID = -1;
            if (decimal.TryParse(AddType.SelectedItem.Value, out MID) == false) { MID = -1; }
            DiscrepincyManager dm = new DiscrepincyManager(User.Identity.Name);
            List<decimal> keepers = dm.GetSlaves(MID);
            decimal Count = 0;
            drpAddDiscrepancy.SelectedIndex = -1;
            foreach (ListItem i in drpAddDiscrepancy.Items)
            {
                i.Selected = false;
                i.Enabled = false;
                if (decimal.TryParse(i.Value, out ID) == false) { ID = -1; }
                if (keepers.Contains(ID) == true)
                {
                    i.Enabled = true;
                    Count += 1;
                    if (Count == 1) { i.Selected = true; }
                }
            }
            ResetAddOutCome();
        }
        void ResetAddOutCome()
        {
            decimal MID = -1;
            decimal ID = -1;
            if (drpAddDiscrepancy.SelectedItem.Enabled == false || decimal.TryParse(drpAddDiscrepancy.SelectedItem.Value, out MID) == false) { MID = -1; }
            DiscrepincyManager dm = new DiscrepincyManager(User.Identity.Name);
            List<decimal> keepers = dm.GetSlaves(MID);
            decimal Count = 0;
            foreach (ListItem i in drpAddOutcome.Items)
            {
                i.Selected = false;
                i.Enabled = false;
                if (decimal.TryParse(i.Value, out ID) == false) { ID = -1; }
                if (keepers.Contains(ID) == true)
                {
                    i.Enabled = true;
                    Count += 1;
                    if (Count == 1) { i.Selected = true; }
                }
            }
        }
        void ResetEditDiscrepancy()
        {
            decimal MID = -1;
            decimal ID = -1;
            if (decimal.TryParse(drpEditType.SelectedItem.Value, out MID) == false) { MID = -1; }
            DiscrepincyManager dm = new DiscrepincyManager(User.Identity.Name);
            List<decimal> keepers = dm.GetSlaves(MID);
            decimal Count = 0;
            drpEditDiscrepancy.SelectedIndex = -1;
            foreach (ListItem i in drpEditDiscrepancy.Items)
            {
                i.Selected = false;
                i.Enabled = false;
                if (decimal.TryParse(i.Value, out ID) == false) { ID = -1; }
                if (keepers.Contains(ID) == true)
                {
                    i.Enabled = true;
                    Count += 1;
                    if (Count == 1) { i.Selected = true; }
                }
            }
            ResetEditOutCome();
        }
        void ResetEditOutCome()
        {
            decimal MID = -1;
            decimal ID = -1;
            if (drpEditDiscrepancy.SelectedItem.Enabled == false || decimal.TryParse(drpEditDiscrepancy.SelectedItem.Value, out MID) == false) { MID = -1; }
            DiscrepincyManager dm = new DiscrepincyManager(User.Identity.Name);
            List<decimal> keepers = dm.GetSlaves(MID);
            decimal Count = 0;
            foreach (ListItem i in drpEditOutcome.Items)
            {
                i.Selected = false;
                i.Enabled = false;
                if (decimal.TryParse(i.Value, out ID) == false) { ID = -1; }
                if (keepers.Contains(ID) == true)
                {
                    i.Enabled = true;
                    Count += 1;
                    if (Count == 1) { i.Selected = true; }
                }
            }
        }


        void btnEditCancel_Click(object sender, EventArgs e)
        {
            pnlEditx.Visible = false;
            pnlMainView.Visible = true;
        }
        void btnEditOK_Click(object sender, EventArgs e)
        {
            decimal DID = -1;
            decimal.TryParse(hdnDiscrepancyID.Value, out DID);




            // we need to save the data.
            Discrepancy d = new Discrepancy();
            d.DiscrepancyID = DID;

            d.DiscrepancyText = drpEditDiscrepancy.SelectedItem.Text;
            d.Division = "";              // drpEditDivision.SelectedItem.Text;
            d.IMEI = EditIMEI.Text;
            d.OutCome = drpEditOutcome.SelectedItem.Text;
            d.Resolved = chkEditResolved.Checked;
            d.ReturnTransfer = EditReturnTransfer.Text;
            d.Transfer_WO = EditTransferWO.Text;
            d.Type = drpEditType.SelectedItem.Text;

            DateTime tempdate = new DateTime();
            if (d.AttemptDate == null && DateTime.TryParse(EditFirstAttemp.Text, out tempdate) == true)
            {
                d.AttemptDate = tempdate;
                d.AttemptUser = User.Identity.Name;
            }
            if (d.AttemptDate2 == null && DateTime.TryParse(EditSecondAttemp.Text, out tempdate) == true)
            {
                d.AttemptDate2 = tempdate;
                d.AttemptUser2 = User.Identity.Name;
            }
            if (d.AttemptDate3 == null && DateTime.TryParse(EditThirdAttemp.Text, out tempdate) == true)
            {
                d.AttemptDate3 = tempdate;
                d.AttemptUser3 = User.Identity.Name;
            }

            DiscrepincyManager dm = new DiscrepincyManager(User.Identity.Name);
            decimal ID = dm.Insert(d);


            pnlEditx.Visible = false;
            pnlMainView.Visible = true;
            UpdateMainGrid();

        }
        void btnAddCancel_Click(object sender, EventArgs e)
        {
            pnlAdd.Visible = false;
            pnlMainView.Visible = true;
        }
        void btnAddOK_Click(object sender, EventArgs e)
        {
            decimal ClientID = -1;
            decimal ClientLocationID = -1;

            decimal.TryParse(drpClientList.SelectedItem.Value, out ClientID);
            decimal.TryParse(drpLocationList.SelectedItem.Value, out ClientLocationID);



            // we need to save the data.
            Discrepancy d = new Discrepancy();
            d.ClientID = ClientID;
            d.ClientLocationID = ClientLocationID;

            d.DiscrepancyText = drpAddDiscrepancy.SelectedItem.Text;
            d.Division = "";                  // drpAddDivision.SelectedItem.Text;
            d.IMEI = AddIMEI.Text;
            d.OutCome = drpAddOutcome.SelectedItem.Text;
            d.Resolved = chkAddResolved.Checked;
            d.ReturnTransfer = AddReturnTransfer.Text;
            d.Transfer_WO = AddTransferWO.Text;
            d.Type = AddType.SelectedItem.Text;

            DateTime tempdate = new DateTime();
            if (d.AttemptDate == null && DateTime.TryParse(AddFirstAttemp.Text, out tempdate) == true)
            {
                d.AttemptDate = tempdate;
                d.AttemptUser = User.Identity.Name;
            }
            if (d.AttemptDate2 == null && DateTime.TryParse(AddSecondAttemp.Text, out tempdate) == true)
            {
                d.AttemptDate2 = tempdate;
                d.AttemptUser2 = User.Identity.Name;
            }
            if (d.AttemptDate3 == null && DateTime.TryParse(AddThirdAttemp.Text, out tempdate) == true)
            {
                d.AttemptDate3 = tempdate;
                d.AttemptUser3 = User.Identity.Name;
            }

            DiscrepincyManager dm = new DiscrepincyManager(User.Identity.Name);
            decimal ID = dm.Insert(d);

            pnlAdd.Visible = false;
            pnlMainView.Visible = true;
            UpdateMainGrid();
        }


        void btnDelete_Click(object sender, EventArgs e)
        {
            decimal DID = -1;
            decimal.TryParse(hdnDiscrepancyID.Value, out DID);
            DiscrepincyManager dm = new DiscrepincyManager(User.Identity.Name);
            bool Success = dm.Delete(DID);
            UpdateMainGrid();
        }
        void btnEdit_Click(object sender, EventArgs e)
        {
            //pnlEditx.Visible = true;
            UpdateEditPanel();
            pnlEditx.Visible = true;
            pnlMainView.Visible = false;
            EditIMEI.Focus();
        }
        void btnAdd_Click(object sender, EventArgs e)
        {
            decimal ClientID = -1;
            decimal ClientLocationID = -1;
            decimal.TryParse(drpClientList.SelectedItem.Value, out ClientID);
            decimal.TryParse(drpLocationList.SelectedItem.Value, out ClientLocationID);
            ClientManager cm = new ClientManager(User.Identity.Name);
            ClientLocation cl = cm.GetClientLocation(ClientLocationID);
            if (cl != null)
            {
                AddStoreName.Text = cl.CompanyName;
                AddStore.Text = cl.ScanKey;
            }
            AddType.SelectedIndex = 0;

            ResetAddDiscrepancy();
            AddIMEI.Text = "";
            chkAddResolved.Checked = false;
            AddReturnTransfer.Text = "";
            AddTransferWO.Text = "";

            AddFirstAttemp.Text = "";
            AddSecondAttemp.Text = "";
            AddThirdAttemp.Text = "";

            pnlAdd.Visible = true;
            pnlMainView.Visible = false;
            AddIMEI.Focus();
        }



        void LoadClientList()
        {
            //decimal MasterClientID = 35;
            string ValidMasterClientIDs = "35";


            CompanyDemographics c = new CompanyDemographics(User.Identity.Name);
            ValidMasterClientIDs = c.DiscrepancyMasterClientID;
            string[] IDs = ValidMasterClientIDs.Split(',');


            ClientManager cm = new ClientManager(User.Identity.Name);
            List<Client> cl = cm.SearchClientList("", "", "").OrderBy(x => x.CompanyName).ToList();
            drpClientList.Items.Clear();
            //ListItem z = new ListItem("All", "-1");
            //drpClientList.Items.Add(z);
            ClientCell01.Visible = true; ClientCell02.Visible = true;
            foreach (Client p in cl)
            {
                ListItem x = new ListItem(p.CompanyName, p.ClientID.ToString());
                //if (p.ClientID == MasterClientID) {
                //    x.Selected = true; ClientCell01.Visible = false; ClientCell02.Visible = false; 
                //}
                if (ValidMasterClientIDs.Length == 0 || IDs.Contains(p.ClientID.ToString()))
                {
                    drpClientList.Items.Add(x);
                }
            }
            LoadClientLocationList();




        }
        void LoadClientLocationList()
        {


            drpLocationList.Items.Clear();
            if (drpClientList.SelectedItem == null) { return; }
            ClientManager cm = new ClientManager(User.Identity.Name);

            decimal ClientID = -1;
            decimal.TryParse(drpClientList.SelectedItem.Value, out ClientID);

            List<ClientLocation> cl = cm.GetClientLocationsForThisClient(ClientID).OrderBy(x => x.CompanyName).ToList();

            ListItem z = new ListItem("All", "-1");
            drpLocationList.Items.Add(z);
            foreach (ClientLocation p in cl)
            {
                ListItem x = new ListItem(p.CompanyName, p.ClientLocationID.ToString());
                drpLocationList.Items.Add(x);
            }
            drpLocationList.SelectedIndex = 0;
            UpdateMainGrid();
        }


        void drpClientList_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadClientLocationList();
        }
        void drpLocationList_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateMainGrid();
        }
        protected void MainGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
            //btnAdd.Visible = false;
            btnEdit.Visible = false;
            btnDelete.Visible = false;
            btnEditOK.Visible = true;
            UpdateEditPanel();
        }

        private void UpdateEditPanel()
        {
            if (MainGrid.SelectedIndex >= 0)
            {
                hdnDiscrepancyID.Value = "-1";
                decimal DiscrepancyID = -1;
                decimal.TryParse(MainGrid.SelectedValue.ToString(), out DiscrepancyID);
                DiscrepincyManager dmanager = new DiscrepincyManager(User.Identity.Name);
                using (clsLinqDataContext ctx = dmanager.GetDataContext(User.Identity.Name))
                {
                    Discrepancy d = ctx.Discrepancies.FirstOrDefault(x => x.DiscrepancyID == DiscrepancyID);
                    if (d != null)
                    {


                        ClientManager cManager = new ClientManager(User.Identity.Name);
                        ClientLocation cl = cManager.GetClientLocation(d.ClientLocationID);
                        if (cl != null)
                        {
                            AddStoreName.Text = cl.CompanyName;
                            AddStore.Text = cl.ScanKey;
                        }


                        hdnDiscrepancyID.Value = d.DiscrepancyID.ToString();
                        EditDiscrepancyID.Text = d.DiscrepancyID.ToString();
                        EditDiscrepancyID.Enabled = false;
                        EditIMEI.Text = d.IMEI;
                        EditCreateDate.Text = String.Format("{0:MM/dd/yyyy HH:mm:ss}", d.CreateDate);

                        EditFirstAttemp.Text = "";
                        if (d.AttemptDate != null) { EditFirstAttemp.Text = String.Format("{0:MM/dd/yyyy HH:mm:ss}", d.AttemptDate); }

                        EditSecondAttemp.Text = "";
                        if (d.AttemptDate2 != null) { EditSecondAttemp.Text = String.Format("{0:MM/dd/yyyy HH:mm:ss}", d.AttemptDate2); }

                        EditThirdAttemp.Text = "";
                        if (d.AttemptDate3 != null) { EditThirdAttemp.Text = String.Format("{0:MM/dd/yyyy HH:mm:ss}", d.AttemptDate3); }

                        ListItem li = null;

                        drpEditType.SelectedIndex = -1;
                        li = drpEditType.Items.FindByText(d.Type);
                        if (li != null && li.Enabled == true)
                        {
                            li.Selected = true;
                        }

                        drpEditDiscrepancy.SelectedIndex = -1;
                        li = drpEditDiscrepancy.Items.FindByText(d.DiscrepancyText);
                        if (li != null && li.Enabled == true)
                        {
                            li.Selected = true;
                        }
                        ResetEditOutCome();
                        drpEditOutcome.SelectedIndex = -1;
                        li = drpEditOutcome.Items.FindByText(d.OutCome);
                        if (li != null && li.Enabled == true)
                        {
                            li.Selected = true;
                        }

                        chkEditResolved.Checked = true;
                        if (d.Resolved == null || d.Resolved == false) { chkEditResolved.Checked = false; }

                        EditTransferWO.Text = d.Transfer_WO;
                        EditReturnTransfer.Text = d.ReturnTransfer;
                        EditStore.Text = d.ClientLocation.ScanKey;
                        EditStoreName.Text = d.ClientLocation.CompanyName;
                        EditStoreName.Enabled = false;
                        EditStore.Enabled = false;
                        btnEdit.Visible = true;
                        btnDelete.Visible = false;
                        if (d.Resolved != null && d.Resolved == true && Roles.IsUserInRole(User.Identity.Name, "AddDiscAdmin") == false) { btnEditOK.Visible = false; }
                        if (Roles.IsUserInRole(User.Identity.Name, "AddDiscAdmin") == true) { btnDelete.Visible = true; }

                    }
                }
            }
        }

        //void chkResolved_CheckedChanged(object sender, EventArgs e)
        //{
        //    UpdateMainGrid();
        //}
        protected void UpdateMainGrid()
        {
            if (drpLocationList.SelectedItem == null) { return; }

            if (SearchID.Text.Length > 0)
            {
                UpdateGridIDSearch();
            }
            else
            {
                if (SearchIMEI.Text.Length > 0)
                {
                    UpdateGridIMEISearch();
                }
                else if (SearchScanKey.Text.Length > 0)
                {
                    UpdateGridScankeySearch();
                }

                else { UpdateGridNormal(); }
            }

            MainGrid.SelectedIndex = -1;
            btnEdit.Visible = false;
            btnDelete.Visible = false;
        }
        private void UpdateGridScankeySearch()
        {
            bool Resolved = chkResolved.Checked;
            decimal ClientID = -1;
            decimal.TryParse(drpClientList.SelectedItem.Value, out ClientID);
            //decimal ID = -1;
            //decimal.TryParse(drpLocationList.SelectedItem.Value, out ID);
            btnAdd.Visible = false;

            DateTime BeginDate = DateTime.Now;
            DateTime EndDate = DateTime.Now;
            if (DateTime.TryParse(txtBeginDate.Text, out BeginDate) == false) { BeginDate = DateTime.Now; }
            if (DateTime.TryParse(txtEndDate.Text, out EndDate) == false) { EndDate = DateTime.Now; }
            EndDate = EndDate.AddDays(1);

            DiscrepincyManager qm = new DiscrepincyManager(User.Identity.Name);
            using (clsLinqDataContext ctx = qm.GetDataContext(User.Identity.Name))
            {
                //var locations = ctx.Discrepancies.Where(x => x.ClientID == ClientID && x.ClientLocation.ScanKey == SearchScanKey.Text);
                var locations = ctx.Discrepancies.Where(x => x.ClientLocation.ScanKey == SearchScanKey.Text
                    && ((x.Resolved != null && x.Resolved == Resolved) || (Resolved == false && (x.Resolved == null || x.Resolved == false)))
                    && (x.CreateDate >= BeginDate && x.CreateDate < EndDate)
                    );

                //&& ((x.Resolved != null && x.Resolved == Resolved) || (Resolved == false && x.Resolved == null)));

                var Data = from x in locations
                           select new
                           {
                               x.AttemptDate,
                               x.AttemptDate2,
                               x.AttemptDate3,
                               x.AttemptUser,
                               x.AttemptUser2,
                               x.AttemptUser3,
                               x.ClientID,
                               x.ClientLocationID,
                               x.CreateDate,
                               x.CreateUser,
                               x.DiscrepancyID,
                               x.DiscrepancyText,
                               x.Division,
                               x.IMEI,
                               x.LastUpdateDate,
                               x.LastUpdateUser,
                               x.OutCome,
                               x.ReceiveDetailID,
                               x.Resolved,
                               x.ReturnTransfer,
                               x.Transfer_WO,
                               x.Type,
                               x.ClientLocation.CompanyName,
                               x.ClientLocation.ScanKey
                           };

                MainGrid.DataSource = Data;
                MainGrid.DataBind();
                // we should find the proper clientlocation and set it....  (Good thing we should only have one.
            }
        }
        private void UpdateGridIMEISearch()
        {
            bool Resolved = chkResolved.Checked;
            decimal ClientID = -1;
            decimal.TryParse(drpClientList.SelectedItem.Value, out ClientID);
            //decimal ID = -1;
            //decimal.TryParse(drpLocationList.SelectedItem.Value, out ID);
            btnAdd.Visible = false;

            DiscrepincyManager qm = new DiscrepincyManager(User.Identity.Name);
            using (clsLinqDataContext ctx = qm.GetDataContext(User.Identity.Name))
            {
                //var locations = ctx.Discrepancies.Where(x => x.ClientID == ClientID && x.IMEI == SearchIMEI.Text);
                var locations = ctx.Discrepancies.Where(x => x.IMEI == SearchIMEI.Text
                    && ((x.Resolved != null && x.Resolved == Resolved) || (Resolved == false && (x.Resolved == null || x.Resolved == false))));
                //&& ((x.Resolved != null && x.Resolved == Resolved) || (Resolved == false && x.Resolved == null)));
                var Data = from x in locations
                           select new
                           {
                               x.AttemptDate,
                               x.AttemptDate2,
                               x.AttemptDate3,
                               x.AttemptUser,
                               x.AttemptUser2,
                               x.AttemptUser3,
                               x.ClientID,
                               x.ClientLocationID,
                               x.CreateDate,
                               x.CreateUser,
                               x.DiscrepancyID,
                               x.DiscrepancyText,
                               x.Division,
                               x.IMEI,
                               x.LastUpdateDate,
                               x.LastUpdateUser,
                               x.OutCome,
                               x.ReceiveDetailID,
                               x.Resolved,
                               x.ReturnTransfer,
                               x.Transfer_WO,
                               x.Type,
                               x.ClientLocation.CompanyName,
                               x.ClientLocation.ScanKey
                           };

                MainGrid.DataSource = Data;
                MainGrid.DataBind();
                // we should find the proper clientlocation and set it....  (Good thing we should only have one.
            }
        }
        private void UpdateGridIDSearch()
        {
            bool Resolved = chkResolved.Checked;
            decimal ClientID = -1;
            decimal.TryParse(drpClientList.SelectedItem.Value, out ClientID);
            decimal ID = -1;
            decimal.TryParse(SearchID.Text, out ID);
            btnAdd.Visible = false;

            DiscrepincyManager qm = new DiscrepincyManager(User.Identity.Name);
            using (clsLinqDataContext ctx = qm.GetDataContext(User.Identity.Name))
            {
                //var locations = ctx.Discrepancies.Where(x => x.ClientID == ClientID && x.DiscrepancyID == ID);
                var locations = ctx.Discrepancies.Where(x => x.DiscrepancyID == ID
                    && ((x.Resolved != null && x.Resolved == Resolved) || (Resolved == false && (x.Resolved == null || x.Resolved == false))));
                //&& ((x.Resolved != null && x.Resolved == Resolved) || (Resolved == false && x.Resolved == null)));
                var Data = from x in locations
                           select new
                           {
                               x.AttemptDate,
                               x.AttemptDate2,
                               x.AttemptDate3,
                               x.AttemptUser,
                               x.AttemptUser2,
                               x.AttemptUser3,
                               x.ClientID,
                               x.ClientLocationID,
                               x.CreateDate,
                               x.CreateUser,
                               x.DiscrepancyID,
                               x.DiscrepancyText,
                               x.Division,
                               x.IMEI,
                               x.LastUpdateDate,
                               x.LastUpdateUser,
                               x.OutCome,
                               x.ReceiveDetailID,
                               x.Resolved,
                               x.ReturnTransfer,
                               x.Transfer_WO,
                               x.Type,
                               x.ClientLocation.CompanyName,
                               x.ClientLocation.ScanKey
                           };

                MainGrid.DataSource = Data;
                MainGrid.DataBind();
                // we should find the proper clientlocation and set it....  (Good thing we should only have one.
            }
        }
        private void UpdateGridNormal()
        {
            bool Resolved = chkResolved.Checked;
            decimal ClientID = -1;
            decimal ClientLocationID = -1;
            decimal.TryParse(drpClientList.SelectedItem.Value, out ClientID);
            decimal.TryParse(drpLocationList.SelectedItem.Value, out ClientLocationID);
            if (ClientLocationID == -1) { btnAdd.Visible = false; } else { btnAdd.Visible = true; }

            DateTime BeginDate = DateTime.Now;
            DateTime EndDate = DateTime.Now;
            if (DateTime.TryParse(txtBeginDate.Text, out BeginDate) == false) { BeginDate = DateTime.Now; }
            if (DateTime.TryParse(txtEndDate.Text, out EndDate) == false) { EndDate = DateTime.Now; }
            EndDate = EndDate.AddDays(1);


            DiscrepincyManager qm = new DiscrepincyManager(User.Identity.Name);
            using (clsLinqDataContext ctx = qm.GetDataContext(User.Identity.Name))
            {
                var locations = ctx.Discrepancies.Where(x => x.ClientID == ClientID
                                                         && (x.ClientLocationID == ClientLocationID || ClientLocationID == -1)
                    && (x.CreateDate >= BeginDate && x.CreateDate < EndDate)
                    && ((x.Resolved != null && x.Resolved == Resolved) || (Resolved == false && (x.Resolved == null || x.Resolved == false))))
                    .OrderByDescending(x => x.CreateDate).Take(200);
                //&& ((x.Resolved != null && x.Resolved == Resolved) || (Resolved == false && x.Resolved == null)
                //)
                //)
                //;

                var Data = from x in locations
                           select new
                           {
                               x.AttemptDate,
                               x.AttemptDate2,
                               x.AttemptDate3,
                               x.AttemptUser,
                               x.AttemptUser2,
                               x.AttemptUser3,
                               x.ClientID,
                               x.ClientLocationID,
                               x.CreateDate,
                               x.CreateUser,
                               x.DiscrepancyID,
                               x.DiscrepancyText,
                               x.Division,
                               x.IMEI,
                               x.LastUpdateDate,
                               x.LastUpdateUser,
                               x.OutCome,
                               x.ReceiveDetailID,
                               x.Resolved,
                               x.ReturnTransfer,
                               x.Transfer_WO,
                               x.Type,
                               x.ClientLocation.CompanyName,
                               x.ClientLocation.ScanKey
                           };

                MainGrid.DataSource = Data;
                MainGrid.DataBind();
            }
        }
    }
}