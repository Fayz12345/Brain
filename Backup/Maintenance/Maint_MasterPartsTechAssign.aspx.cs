using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BW_WebApp.DataManagers;


namespace BW_WebApp.Maintenance
{
    public partial class Maint_MasterPartsTechAssign : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnClear.Click += new EventHandler(btnClear_Click);
            btnRefreshSummary.Click += new EventHandler(btnRefreshSummary_Click);
            btnRefreshDetail.Click += new EventHandler(btnRefreshDetail_Click);
            btnRefresh.Click += new EventHandler(btnRefresh_Click);
            //btnSave.Click += new EventHandler(btnSave_Click);
            hdnUserName.Value = User.Identity.Name;

            btnAssign.Click += new EventHandler(btnAssign_Click);
            MainGridPN.RowCommand += new GridViewCommandEventHandler(MainGridPN_RowCommand);
            MainGridPN.RowDataBound += new GridViewRowEventHandler(MainGridPN_RowDataBound);
            if (!IsPostBack)
            {
                LoadDropDowns();
            }

            txtPartNumber.Focus();
            //txtWaybill.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13) || (event.which == 9) || (event.keyCode == 9)) {;SetESNFocus();return false;}} else {return true}; ");
            ////txtWaybill.Attributes.Add("onblur", "IsMessage();return false;");



            //txtPartNumber.Attributes.Add("onkeydown", "alert('xxxxxxx')");
            //txtPartNumber.Attributes.Add("onblur", "alter('bbbbbbbb')");


            //txtPartNumber.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {RecordScanKey();return false;}} else {return true}; ");
            //txtPartNumber.Attributes.Add("onblur", "RecordScanKey();return false;");
            this.grdSearchSummary.Appearance.AlternateRecordFieldCell.Interior = new Syncfusion.Drawing.BrushInfo(System.Drawing.ColorTranslator.FromHtml("#E2E2E2"));
            this.grdSearchDetail.Appearance.AlternateRecordFieldCell.Interior = new Syncfusion.Drawing.BrushInfo(System.Drawing.ColorTranslator.FromHtml("#E2E2E2"));


        }

        void btnAssign_Click(object sender, EventArgs e)
        {
            //RecordPartNumberAssigned(txtPartNumber.Text);
            //txtPartNumber.Text = "";
            //txtPartNumber.Focus();
        }

        void RecordPartNumberAssigned(string PartNumber)
        {
            //if (txtIMEI.Text.Length == 0)
            //{
            //    ListItem item = new ListItem("ERROR:Must include IMEI");
            //    item.Attributes.Add("style", "color:RED;font-weight:bold;");
            //    lstHistory.Items.Add(item);
            //    return;
            //}
            //decimal ReceiveDetailID = -1;
            //ReceiveDetailManager rdm = new ReceiveDetailManager(User.Identity.Name);
            //ReceiveDetailID = rdm.ReceiveDetailID_Version000(txtIMEI.Text);
            //if (ReceiveDetailID < 1)
            //{
            //    ListItem item = new ListItem("ERROR:IMEI Not found(" + txtIMEI.Text + ")");
            //    item.Attributes.Add("style", "color:RED;font-weight:bold;");
            //    lstHistory.Items.Add(item);
            //    return;
            //}



            ////string Tech = drpTechList.SelectedItem.Text;
            //string Tech = drpTechList.SelectedItem.Value;
            //string Location = drpLocationList.SelectedItem.Value;
            //string Return = "N";
            ////if (chkReturn.Checked == true) { Return = "T"; }
            //string rValue = AssignPartNumber(ReceiveDetailID, PartNumber, Tech, Location, Return, User.Identity.Name);
            //if (rValue.Contains("ERROR") == true)
            //{
            //    ListItem item = new ListItem(rValue);
            //    item.Attributes.Add("style", "color:RED;font-weight:bold;");
            //    lstHistory.Items.Add(item);
            //}
            //else
            //{
            //    decimal count = 0;
            //    if (decimal.TryParse(txtCount.Text, out count) == true) { count++; txtCount.Text = count.ToString(); }
            //    ListItem item = new ListItem(rValue);
            //    item.Attributes.Add("style", "color:blue;");
            //    lstHistory.Items.Add(item);
            //}
        }

        public string AssignPartNumber(decimal ReceiveDetailID, string PartNumber, string Tech, string Location, string Return, string UserName)
        {
            ////if (ESN.Trim().Length == 0 || WayBill.Trim().Length == 0) { return "0"; }
            //decimal LocationID = -1;
            //bool isReturned = false;
            //if (Return == "T") { isReturned = true; }
            //if (decimal.TryParse(Location, out LocationID) == false) { LocationID = -1; }
            //MasterPartManager MPM = new MasterPartManager(UserName);

            //return MPM.MasterPartsTechAssignedLog_Add(ReceiveDetailID, PartNumber, Tech, LocationID, isReturned);
            return "";
        }


        void MainGridPN_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                MasterPartsLinkTable mpl = (MasterPartsLinkTable)e.Row.DataItem;
                ImageButton bModel = (ImageButton)e.Row.FindControl("imgAssignPart");
                if (bModel != null) { bModel.CommandArgument = "-1"; }
                if (mpl != null)
                {
                    if (bModel != null) { bModel.CommandArgument = mpl.MasterPartsLinkTableID.ToString(); }
                    if (mpl.Quantity < 1) { bModel.Enabled = false; }
                }
            }
        }


        void MainGridPN_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            ImageButton btnOpen = (ImageButton)e.CommandSource;
            string CommandArgument = btnOpen.CommandArgument;

            if (btnOpen.ID.ToUpper() == "IMGASSIGNPART")
            {
                decimal ID = -1;
                if (decimal.TryParse(CommandArgument, out ID) == false) { ID = -1; }
                MasterPartManager mpm = new MasterPartManager(User.Identity.Name);

                using (clsLinqDataContext ctx = mpm.GetDataContext(User.Identity.Name))
                {
                    MasterPartsLinkTable pt = ctx.MasterPartsLinkTables.FirstOrDefault(x => x.MasterPartsLinkTableID == ID);
                    if (pt != null)
                    {
                        RecordPartNumberAssigned(pt.GMPPartNumber);
                        txtPartNumber.Text = "";
                        txtPartNumber.Focus();

                        ////foreach (MasterPartsLinkTableModelList ml in pt.MasterPartsLinkTableModelLists)
                        ////{
                        ////    ModelList += ml.MasterPartsLinkTableModelListID.ToString() + ";";
                        ////}
                        //pnlEditModels.Visible = true;
                        //pnlMainGridPN.Visible = false;

                        ////string[] data = CommandArgument.Split(',');
                        //EditMFGPart.Text = pt.PartNumber;
                        //EditGMPPart.Text = pt.GMPPartNumber;
                        //EditModelDesc.Text = pt.GMPPartDescription;
                        //hdnMasterPartLinkTableID.Value = pt.MasterPartsLinkTableID.ToString();
                        //foreach (ListItem x in chkEditModels.Items)
                        //{
                        //    decimal xID = -1;
                        //    if (decimal.TryParse(x.Value, out xID) == false) { xID = -1; }
                        //    if (pt.MasterPartsLinkTableModelLists.Any(y => y.ModelID == xID) == true)
                        //    { x.Selected = true; }
                        //    else { x.Selected = false; }
                        //}
                    }


                }
                //ScriptManager.RegisterStartupScript(this, GetType(), "Open Unit", "alert('imgChangeModel:" + CommandArgument + "');", true);
                //ScriptManager.RegisterStartupScript(this, GetType(), "Open Unit", "OpenUnitAnalysisRPT(" + CommandArgument + ");", true);
            }
        }





        void btnRefreshDetail_Click(object sender, EventArgs e)
        {
            MasterPartManager mpm = new MasterPartManager(User.Identity.Name);
            grdSearchDetail.DataSource = mpm.GetTechAssignedDetail(txtPartNumber_02.Text, drpTechList_02.SelectedItem.Text, DateTime.Now, DateTime.Now);
        }

        void btnRefreshSummary_Click(object sender, EventArgs e)
        {
            MasterPartManager mpm = new MasterPartManager(User.Identity.Name);
            grdSearchSummary.DataSource = mpm.GetTechAssignedSummary(txtPartNumber_02.Text, drpTechList_02.SelectedItem.Text, DateTime.Now, DateTime.Now);
        }

        void btnRefresh_Click(object sender, EventArgs e)
        {
            UpdateMainGridPM();
        }

        protected void UpdateMainGridPM()
        {
            // Get the list of parts that match this condition.
            MasterPartManager mpm = new MasterPartManager(User.Identity.Name);
            List<MasterPartsLinkTable> mParts = mpm.GetMasterPartNumbersThisPart(decimal.Parse(drpDropPart_03.SelectedItem.Value), -1, decimal.Parse(drpLocationList.SelectedItem.Value), "-1", drpManufacturer_03.SelectedItem.Value, "",-1,-1);
            //mParts = mpm.GetMasterPartNumbers(-1, "-1", Manufacturer, Model);
            MainGridPN.DataSource = mParts;                   // mpm.GetMasterParts();
            MainGridPN.DataBind();
        }


        protected void LoadDropDowns()
        {
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                LoadDropDowns(ctx);
            }
        }
        protected void LoadDropDowns(clsLinqDataContext ctx)
        {
            QuestionManager qm = new QuestionManager(User.Identity.Name);
            List<Option> LO = new List<Option>();
            //LO = qm.GetQuestionOptionList(ctx,"Carrier");
            //drpCarrier.Items.Clear();
            //drpCarrier.Items.Add(new ListItem("<None>", "-1"));
            //foreach (Option o in LO)
            //{
            //    ListItem li = new ListItem("(" + o.ScanKey + ") " + o.OptionText, o.OptionID.ToString());
            //    drpCarrier.Items.Add(li);
            //}
            //drpCarrier.SelectedIndex = 0;


            ClientManager cm = new ClientManager(User.Identity.Name);
            List<ClientLocation> cls = cm.GetClientLocationsWithOnSiteInventory();
            drpLocationList.Items.Clear();
            drpLocationList.Items.Add(new ListItem("WHS 001", "-1"));
            drpLocationList_02.Items.Clear();
            drpLocationList_02.Items.Add(new ListItem("WHS 001", "-1"));
            foreach (ClientLocation cl in cls)
            {
                ListItem li = new ListItem(cl.CompanyName, cl.ClientLocationID.ToString());
                ListItem li2 = new ListItem(cl.CompanyName, cl.ClientLocationID.ToString());
                ListItem li3 = new ListItem(cl.CompanyName, cl.ClientLocationID.ToString());
                drpLocationList.Items.Add(li);
                drpLocationList_02.Items.Add(li2);
            }
            drpLocationList.SelectedIndex = 0;
            drpLocationList_02.SelectedIndex = 0;



            LO = qm.GetQuestionOptionList(ctx, "Manufacturer");
            drpManufacturer_03.Items.Clear();
            drpManufacturer_03.Items.Add(new ListItem("<None>", "-1"));
            foreach (Option o in LO)
            {
                ListItem li = new ListItem("(" + o.ScanKey + ") " + o.OptionText, o.OptionID.ToString());
                drpManufacturer_03.Items.Add(li);
            }
            drpManufacturer_03.SelectedIndex = 0;



            BasicUserUtilities buu = new BasicUserUtilities(User.Identity.Name);
            List<ListItem> Techs = buu.GetTechnicionList();


            //LO = qm.GetQuestionOptionList(ctx, "Tech Finished Unit");
            drpTechList.Items.Clear();
            drpTechList_02.Items.Clear();
            drpTechList_02.Items.Add(new ListItem("All", "-1"));
            drpTechList_02.Items.Add(new ListItem("Un-Assigned", "Un-Assigned"));
            drpTechList.Items.Add(new ListItem("Un-Assigned", "Un-Assigned"));
            foreach (ListItem o in Techs)
            {
                ListItem li = new ListItem(o.Text, o.Value);
                drpTechList.Items.Add(li);
                ListItem li2 = new ListItem(o.Text, o.Value);
                drpTechList_02.Items.Add(li2);
            }
            //foreach (Option o in LO)
            //{
            //    ListItem li = new ListItem(o.OptionText, o.OptionID.ToString());
            //    drpTechList.Items.Add(li);
            //    ListItem li2 = new ListItem(o.OptionText, o.OptionID.ToString());
            //    drpTechList_02.Items.Add(li2);
            //}
            drpTechList.SelectedIndex = 0;
            drpTechList_02.SelectedIndex = 0;

           
            MasterPartManager mpm = new MasterPartManager(User.Identity.Name);
            List<MasterPart> Parts = mpm.GetMasterParts(ctx);
            drpDropPart_03.Items.Clear();
            foreach (MasterPart o in Parts)
            {
                ListItem li = new ListItem(o.Description, o.MasterPartsID.ToString());
                drpDropPart_03.Items.Add(li);
            }
            drpDropPart_03.Items.Add(new ListItem("<All>", "-1"));
            drpDropPart_03.SelectedIndex = 0;

            //MasterPartManager mpm = new MasterPartManager(User.Identity.Name);
            //List<MasterPart> Parts = mpm.GetMasterParts(ctx);
            //drpDropPart.Items.Clear();
            //drpChangeCategoryPart.Items.Clear();
            //foreach (MasterPart o in Parts)
            //{
            //    ListItem li = new ListItem(o.Description, o.MasterPartsID.ToString());
            //    drpDropPart.Items.Add(li);
            //    ListItem l1 = new ListItem(o.Description, o.MasterPartsID.ToString());
            //    drpChangeCategoryPart.Items.Add(l1);
            //}
            //drpDropPart.Items.Add(new ListItem("<All>", "-1"));
            //drpDropPart.SelectedIndex = 0;
            //drpChangeCategoryPart.Items.Add(new ListItem("<All>", "-1"));
            //drpChangeCategoryPart.SelectedIndex = 0;



            ////LO = qm.GetQuestionOptionList(ctx,"Model");
            ////drpModel.Items.Clear();
            //////drpModel.Items.Add(new ListItem("<None>", "-1"));
            ////foreach (Option o in LO)
            ////{
            ////    ListItem li = new ListItem("(" + o.ScanKey + ") " + o.OptionText, o.OptionID.ToString());
            ////    drpModel.Items.Add(li);
            ////}
            ////drpModel.SelectedIndex = 0;



            ////MultiModelDropDown.Items.Clear();
            ////MultiModelDropDown.Items.Add(new ListItem("None", "-1"));
            ////foreach (Option o in LO)
            ////{
            ////    ListItem li = new ListItem("(" + o.ScanKey + ") " + o.OptionText, o.OptionID.ToString());
            ////    MultiModelDropDown.Items.Add(li);
            ////}
            ////MultiModelDropDown.SelectedItems.Clear();
            ////MultiModelDropDown.SelectedItems.Add(MultiModelDropDown.Items[0]);



            //LO = qm.GetQuestionOptionList(ctx, "Model");
            //chkModels.Items.Clear();
            //chkModels.Items.Add(new ListItem("None", "-1"));
            //chkEditModels.Items.Clear();
            //chkEditModels.Items.Add(new ListItem("None", "-1"));
            //foreach (Option o in LO)
            //{
            //    ListItem li = new ListItem(o.OptionText, o.OptionID.ToString());
            //    ListItem l1 = new ListItem(o.OptionText, o.OptionID.ToString());
            //    chkModels.Items.Add(li);
            //    chkEditModels.Items.Add(l1);
            //}
            //chkModels.SelectedIndex = 0;
            //chkEditModels.SelectedIndex = 0;
        }



        //void btnSave_Click(object sender, EventArgs e)
        //{

        //    List<string> ESNS = PartNumberList.Value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        //    lstHistory.Items.Clear();
        //    foreach (string esn in ESNS)
        //    {
        //        ListItem x = new ListItem(esn + " SAVE...Not yet Implemented!");
        //        lstHistory.Items.Add(x);
        //    }

        //    ListItemCollection ll = lstHistory.Items;
        //    txtPartNumber.Text = "";
        //    txtCount.Text = "0";
        //    lblWarningMessage.Text = "Save Not yet completed";
        //    PartNumberList.Value = "";

        //    //throw new NotImplementedException();
        //}




        //void btnUpdateLocation_Click(object sender, EventArgs e)
        //{
        //    if (lblBinNumber.Text.Length == 0 && lblESN.Text.Length == 0) { lblMessage.Text = "No Bin number or ESN/IMEI given"; return; }
        //    ReceiveDetailManager rd = new ReceiveDetailManager(User.Identity.Name);
        //    decimal LocationID = -1;
        //    if (decimal.TryParse(drpLocationList.SelectedItem.Value, out LocationID) == false) { LocationID = -1; }

        //    if (lblESN.Text.Length > 0) { lblMessage.Text = rd.UpdateESN_LocationValue_ByID(lblESN.Text, LocationID, drpLocationList.SelectedItem.Text); }
        //    else { lblMessage.Text = rd.UpdateBin_LocationValue_ByID(lblBinNumber.Text, LocationID); }
        //}



        void btnClear_Click(object sender, EventArgs e)
        {
            PartNumberList.Value = "";
            //txtWaybill.Text = "";
            //lastWayBill.Value = "";
            //chkReturn.Checked = false;
            txtPartNumber.Text = "";
            txtCount.Text = "0";
            lblWarningMessage.Text = "";
            //drpCourier.SelectedIndex = 0;
            lstHistory.Items.Clear();
            txtPartNumber.Focus();
            ////txtWaybill.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13) || (event.which == 9) || (event.keyCode == 9)) {CleanData();SetESNFocus();return false;}} else {return true}; ");
            ////txtWaybill.Attributes.Add("onblur", "IsMessage();return false;");

            ////txtESN.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13) || (event.which == 9) || (event.keyCode == 9)) {SetWayBillFocus();return false;}} else {return true}; ");
            ////txtESN.Attributes.Add("onblur", "RecordScanKey();return false;");

        }
    }







}