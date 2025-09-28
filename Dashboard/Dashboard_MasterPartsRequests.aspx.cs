using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
//using Syncfusion.Web.UI.WebControls.Tools;
using BW_WebApp.DataManagers;

namespace BW_WebApp
{
    public partial class Dashboard_MasterPartsRequests : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //btnClear.Click += new EventHandler(btnClear_Click);
            btnRefreshNewList.Click += new EventHandler(btnRefreshNewList_Click);
            btnRefreshDetail.Click += new EventHandler(btnRefreshDetail_Click);
            //btnAssign.Click += new EventHandler(btnAssign_Click);
            btnCancel.Click += new EventHandler(btnCancel_Click);
            btnRefresh.Click += new EventHandler(btnRefresh_Click);
            //btnRefreshO.Click += new EventHandler(btnRefreshO_Click);
            //drpDropPart_03O.SelectedIndexChanged += new EventHandler(drpDropPart_03O_SelectedIndexChanged);  



            btnReturnRefresh.Click += new EventHandler(btnReturnRefresh_Click);

            btnRefreshOutOfStock.Click += new EventHandler(btnRefreshOutOfStock_Click);
            //tabMain.ActiveTabChanged += new EventHandler(tabMain_ActiveTabChanged);
            imgbuttonOutOfStock.Click += new EventHandler(imgbuttonOutOfStock_Click);
            imgButtonCancel.Click += new EventHandler(imgButtonCancel_Click);
            drpLocationList.SelectedIndexChanged += new EventHandler(drpLocationList_SelectedIndexChanged);
            drpDropPart_03.SelectedIndexChanged += new EventHandler(drpDropPart_03_SelectedIndexChanged);

            //btnRefreshDetail.Click += new EventHandler(btnRefreshDetail_Click);
            //btnSave.Click += new EventHandler(btnSave_Click);
            hdnUserName.Value = User.Identity.Name;
            btnProcessReturnCode.Click += new EventHandler(btnProcessReturnCode_Click);

            //imgbuttonOutOfStock.Visible = false;
            //imgButtonCancel.Visible = false;

            //btnAssign.Click += new EventHandler(btnAssign_Click);
            grdNewData.RowCommand += new GridViewCommandEventHandler(grdNewData_RowCommand);
            grdNewData.RowDataBound += new GridViewRowEventHandler(grdNewData_RowDataBound);

            MainGridPN.RowCommand += new GridViewCommandEventHandler(MainGridPN_RowCommand);
            MainGridPN.RowDataBound += new GridViewRowEventHandler(MainGridPN_RowDataBound);

            MainGridPNOther.RowCommand += new GridViewCommandEventHandler(MainGridPNOther_RowCommand);
            MainGridPNOther.RowDataBound += new GridViewRowEventHandler(MainGridPNOther_RowDataBound);





            grdAssignedParts.RowCommand += new GridViewCommandEventHandler(grdAssignedParts_RowCommand);
            grdAssignedParts.RowDataBound += new GridViewRowEventHandler(grdAssignedParts_RowDataBound);

            grdReturnRecords.RowCommand += new GridViewCommandEventHandler(grdReturnRecords_RowCommand);
            grdReturnRecords.RowDataBound += new GridViewRowEventHandler(grdReturnRecords_RowDataBound);

            grdOutOfStockParts.RowCommand += new GridViewCommandEventHandler(grdOutOfStockParts_RowCommand);
            grdOutOfStockParts.RowDataBound += new GridViewRowEventHandler(grdOutOfStockParts_RowDataBound);
            if (!IsPostBack)
            {
                UpdateDropDowns();
                RefreshAllGrids();
                //UpdateMainGridPM();
            }
        }




        void grdOutOfStockParts_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridMasterPartsRequested mpl = (GridMasterPartsRequested)e.Row.DataItem;
                System.Web.UI.WebControls.LinkButton bModel = (System.Web.UI.WebControls.LinkButton)e.Row.FindControl("imgFill");
                if (bModel != null) { bModel.CommandArgument = "-1"; }
                if (mpl != null)
                {
                    if (bModel != null)
                    {
                        bModel.CommandArgument = mpl.MasterPartsRequestedLogID.ToString();
                    }
                }
                //bModel = (System.Web.UI.WebControls.LinkButton)e.Row.FindControl("imgOutOfStock");
                //if (bModel != null) { bModel.CommandArgument = "-1"; }
                //if (mpl != null)
                //{
                //    if (bModel != null) { bModel.CommandArgument = mpl.MasterPartsRequestedLogID.ToString(); }
                //}
                bModel = (System.Web.UI.WebControls.LinkButton)e.Row.FindControl("imgCancel");
                if (bModel != null) { bModel.CommandArgument = "-1"; }
                if (mpl != null)
                {
                    if (bModel != null) { bModel.CommandArgument = mpl.MasterPartsRequestedLogID.ToString(); }
                }
            }
        }
        void grdOutOfStockParts_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            System.Web.UI.WebControls.LinkButton btnOpen = (System.Web.UI.WebControls.LinkButton)e.CommandSource;
            string CommandArgument = btnOpen.CommandArgument;

            if (btnOpen.ID.ToUpper() == "IMGFILL")
            {
                decimal MasterPartsRequestedLogID = -1;
                if (decimal.TryParse(CommandArgument, out MasterPartsRequestedLogID) == false) { MasterPartsRequestedLogID = -1; }
                MasterPartManager mpm = new MasterPartManager(User.Identity.Name);
                mpm.SetPartRequestedBackToPick(MasterPartsRequestedLogID);
                RefreshAllGrids();
                ScriptManager.RegisterStartupScript(this, GetType(), "xxxx", "alert('Request moved. Refresh list for visual update!');", true);
                return;
                //mpm.SetPartRequestedAsFilled(ID);
                //RefreshAllGrids();
            }
            if (btnOpen.ID.ToUpper() == "IMGCANCEL")
            {
                decimal ID = -1;
                if (decimal.TryParse(CommandArgument, out ID) == false) { ID = -1; }
                MasterPartManager mpm = new MasterPartManager(User.Identity.Name);
                mpm.SetPartRequestedAsCanceled(ID);
                RefreshAllGrids();
                ScriptManager.RegisterStartupScript(this, GetType(), "xxxx", "alert('Request canceled. Refresh list for visual update!');", true);
            }
        }

        void btnRefreshOutOfStock_Click(object sender, EventArgs e)
        {
            UpdateOutOfStockGrid();
        }

        void imgButtonCancel_Click(object sender, EventArgs e)
        {
            decimal ID = -1;
            if (decimal.TryParse(KeyID.Value, out ID) == false) { ID = -1; }
            MasterPartManager mpm = new MasterPartManager(User.Identity.Name);
            mpm.SetPartRequestedAsCanceled(ID);
            RefreshAllGrids();
            //txtPartNumber.Text = "";
            AssignScreen.Visible = false;
            GridData.Visible = true;
        }
        void imgbuttonOutOfStock_Click(object sender, EventArgs e)
        {
            decimal ID = -1;
            if (decimal.TryParse(KeyID.Value, out ID) == false) { ID = -1; }
            MasterPartManager mpm = new MasterPartManager(User.Identity.Name);
            mpm.SetPartRequestedAsOutOfStock(ID);
            RefreshAllGrids();
            //txtPartNumber.Text = "";
            AssignScreen.Visible = false;
            GridData.Visible = true;
        }

        //-----------------------------
        void drpLocationList_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateMainGridPM();
            UpdateMainGridPMOther();
        }
        void drpDropPart_03_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateMainGridPM();
            UpdateMainGridPMOther();
        }
        //void drpDropPart_03O_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    UpdateMainGridPM();
        //    UpdateMainGridPMOther();
        //}
        void btnRefresh_Click(object sender, EventArgs e)
        {
            UpdateMainGridPM();
            UpdateMainGridPMOther();
        }
        //void btnRefreshO_Click(object sender, EventArgs e)
        //{
        //    UpdateMainGridPM();
        //    UpdateMainGridPMOther();
        //}

        protected void UpdateMainGridPMOther()
        {

            if (hdnManufacturerID.Value.Trim().Length == 0)
            {
                MainGridPN.DataSource = null;                   // mpm.GetMasterParts();
                MainGridPN.DataBind();
                return;
            }

            // Get the list of parts that match this condition.
            MasterPartManager mpm = new MasterPartManager(User.Identity.Name);
            //lblMessagex.Text = "GetMasterPartNumbersThisPart(" + drpDropPart_03.SelectedItem.Value + "," + drpLocationList.SelectedItem.Value + ",-1," + hdnManufacturerID.Value + "," + hdnModelID.Value + ");";
            List<MasterPartsLinkTable> mParts = mpm.GetMasterPartNumbersThisPart(decimal.Parse(drpDropPart_03.SelectedItem.Value), -1, decimal.Parse(drpLocationList.SelectedItem.Value), "-1", hdnManufacturerID.Value, hdnModelID.Value, -1, -1);
            //List<GRDPartLocationView> mParts = mpm.GetMasterPartNumbersThisPartLocationView(decimal.Parse(drpDropPart_03.SelectedItem.Value), -1, decimal.Parse(drpLocationList.SelectedItem.Value), "-1", hdnManufacturerID.Value, hdnModelID.Value, -1, -1);
            MainGridPNOther.DataSource = mParts.Where(x => x.Quantity > 0);                   // mpm.GetMasterParts();
            MainGridPNOther.DataBind();
        }
        protected void UpdateMainGridPM()
        {
            if (hdnManufacturerID.Value.Trim().Length == 0)
            {
                MainGridPN.DataSource = null;                   // mpm.GetMasterParts();
                MainGridPN.DataBind();
                return;
            }
            // Get the list of parts that match this condition.
            MasterPartManager mpm = new MasterPartManager(User.Identity.Name);
            //lblMessagex.Text = "GetMasterPartNumbersThisPart(" + drpDropPart_03.SelectedItem.Value + "," + drpLocationList.SelectedItem.Value + ",-1," + hdnManufacturerID.Value + "," + hdnModelID.Value + ");";
            //List<MasterPartsLinkTable> mPartsx = mpm.GetMasterPartNumbersThisPart(decimal.Parse(drpDropPart_03.SelectedItem.Value), -1, decimal.Parse(drpLocationList.SelectedItem.Value), "-1", hdnManufacturerID.Value, hdnModelID.Value, -1, -1);
            List<GRDPartLocationView> mParts = mpm.GetMasterPartNumbersThisPartLocationView(decimal.Parse(drpDropPart_03.SelectedItem.Value), -1, decimal.Parse(drpLocationList.SelectedItem.Value), "-1", hdnManufacturerID.Value, hdnModelID.Value, -1, -1);
            MainGridPN.DataSource = mParts.Where(x => x.QTY > 0);                   // mpm.GetMasterParts();
            MainGridPN.DataBind();
        }
        //-------------------------------------

        void MainGridPNOther_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //MasterPartsLinkTable mpl = (MasterPartsLinkTable)e.Row.DataItem;
                //System.Web.UI.WebControls.LinkButton bModel = (System.Web.UI.WebControls.LinkButton)e.Row.FindControl("imgAssignPart");
                //if (bModel != null) { bModel.CommandArgument = "-1"; }
                //if (mpl != null)
                //{
                //    if (bModel != null) { 
                //        bModel.CommandArgument = mpl.MasterPartsLinkTableID.ToString();
                //        if (mpl.Quantity < 1) { bModel.Enabled = false; }
                //    }
                //}
                MasterPartsLinkTable mpl = (MasterPartsLinkTable)e.Row.DataItem;
                System.Web.UI.WebControls.LinkButton bModel = (System.Web.UI.WebControls.LinkButton)e.Row.FindControl("imgAssignPart");
                if (bModel != null) { bModel.CommandArgument = "-1"; }
                if (mpl != null)
                {
                    if (bModel != null)
                    {
                        bModel.CommandArgument = mpl.MasterPartsLinkTableID.ToString();
                        if (mpl.Quantity < 1) { bModel.Enabled = false; }
                    }
                }


            }
        }
        void MainGridPNOther_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            System.Web.UI.WebControls.LinkButton btnOpen = (System.Web.UI.WebControls.LinkButton)e.CommandSource;
            string CommandArgument = btnOpen.CommandArgument;

            IFSLocation LocFrom = new IFSLocation(IFSFromLocation.Text);
            IFSLocation LocTo = new IFSLocation(IFSToLocation.Text);

            if (btnOpen.ID.ToUpper() == "IMGASSIGNPART")
            {

                if (IFSToLocation.Text.Length == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "xxxx", "alert('No IFS TO Location Given');", true);
                    return;
                }
                if (IFSFromLocation.Text.Length == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "xxxx", "alert('No IFS Fom Location Given');", true);
                    return;
                }
                if (LocFrom.isValid == false)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "xxxx", "alert('Location From Not Valid');", true);
                    return;
                }
                if (LocTo.isValid == false)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "xxxx", "alert('Location To Not Valid');", true);
                    return;
                }
                decimal ID = -1;
                if (decimal.TryParse(CommandArgument, out ID) == false) { ID = -1; }
                MasterPartManager mpm = new MasterPartManager(User.Identity.Name);
                using (clsLinqDataContext ctx = mpm.GetDataContext(User.Identity.Name))
                {
                    MasterPartsLinkTable pt = ctx.MasterPartsLinkTables.FirstOrDefault(x => x.MasterPartsLinkTableID == ID);
                    if (pt != null)
                    {
                        AssignPart(pt.GMPPartNumber, IFSFromLocation.Text, LocTo.Text);
                        //ScriptManager.RegisterStartupScript(this, GetType(), "xxxx", "alert('Part Picked!');", true);
                    }
                }
            }
        }

        void MainGridPN_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //MasterPartsLinkTable mpl = (MasterPartsLinkTable)e.Row.DataItem;
                //System.Web.UI.WebControls.LinkButton bModel = (System.Web.UI.WebControls.LinkButton)e.Row.FindControl("imgAssignPart");
                //if (bModel != null) { bModel.CommandArgument = "-1"; }
                //if (mpl != null)
                //{
                //    if (bModel != null) { 
                //        bModel.CommandArgument = mpl.MasterPartsLinkTableID.ToString();
                //        if (mpl.Quantity < 1) { bModel.Enabled = false; }
                //    }
                //}

                GRDPartLocationView mpl = (GRDPartLocationView)e.Row.DataItem;
                System.Web.UI.WebControls.LinkButton bModel = (System.Web.UI.WebControls.LinkButton)e.Row.FindControl("imgAssignPart");
                if (bModel != null) { bModel.CommandArgument = "-1"; }
                if (mpl != null)
                {
                    if (bModel != null)
                    {
                        bModel.CommandArgument = mpl.MasterPartsLinkTableID.ToString() + "|" + mpl.IFSLocation;
                        if (mpl.Quantity < 1) { bModel.Enabled = false; }
                    }
                }


            }
        }
        void MainGridPN_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            System.Web.UI.WebControls.LinkButton btnOpen = (System.Web.UI.WebControls.LinkButton)e.CommandSource;
            string CommandArgument = btnOpen.CommandArgument;

            if (btnOpen.ID.ToUpper() == "IMGASSIGNPART")
            {
                if (IFSToLocation.Text.Length == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "xxxx", "alert('No IFS TO Location Given');", true);
                    return;
                }


                decimal ID = -1;
                string[] keys = CommandArgument.Split('|');
                if (decimal.TryParse(keys[0], out ID) == false) { ID = -1; }
                MasterPartManager mpm = new MasterPartManager(User.Identity.Name);
                using (clsLinqDataContext ctx = mpm.GetDataContext(User.Identity.Name))
                {
                    MasterPartsLinkTable pt = ctx.MasterPartsLinkTables.FirstOrDefault(x => x.MasterPartsLinkTableID == ID);
                    if (pt != null)
                    {

                        AssignPart(pt.GMPPartNumber, keys[1], IFSToLocation.Text);
                        //ScriptManager.RegisterStartupScript(this, GetType(), "xxxx", "alert('Part Picked!');", true);
                    }
                }
            }

        }



        private void AssignPart(string Part, string FromLocation, string ToLocation)
        {

            if (Part.Trim().Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "xxxx", "alert('No Partnumber given!');", true);
                return;
            }
            IFSLocation LocTo = new IFSLocation(ToLocation);
            ToLocation = LocTo.C1NA();
            //decimal TimeInMilliSeconds = 0;
            decimal ReceiveDetailID = -1;
            TimeLogManager tlm = new TimeLogManager(User.Identity.Name, GetUserIPAddress());
            tlm.StartTimer();
            string rValue = "";
            decimal ID = -1;
            if (decimal.TryParse(KeyID.Value, out ID) == false) { ID = -1; }
            MasterPartManager mpm = new MasterPartManager(User.Identity.Name);
            if (ID > 0 && Part.Length > 0)
            {
                rValue = mpm.SetPartRequestedAsFilled(ID, Part, drpLocationList.SelectedItem.Value, FromLocation, ToLocation);
                //rValue = mpm.SetPartRequestedAsFilled(ID, Part, drpLocationList.SelectedItem.Value, drpLocationEditParts.SelectedItem.Value, drpLocationEditPartsTo.SelectedItem.Text);
                //TimeInMilliSeconds = tlm.TimeInMilliSeconds();
                if (rValue.Trim().Length > 0)
                {
                    rValue += " (" + tlm.TimeInMilliSeconds().ToString() + "ms)";
                    ScriptManager.RegisterStartupScript(this, GetType(), "xxxx", "alert('" + rValue + "');", true);
                    if (rValue.ToUpper().Contains("ERROR") == true)
                    {
                        tlm.SaveTimeLogAssignPartsScreen(ID, ReceiveDetailID, rValue);
                        return;
                    }
                }
            }
            RefreshAllGrids();
            //txtPartNumber.Text = "";
            AssignScreen.Visible = false;
            GridData.Visible = true;
            rValue = "Part Assigned. (" + tlm.TimeInMilliSeconds().ToString() + "ms)";

            tlm.SaveTimeLogAssignPartsScreen(ID, ReceiveDetailID, rValue);

            ScriptManager.RegisterStartupScript(this, GetType(), "xxxx", "alert('" + rValue + "');", true);
        }

        void btnCancel_Click(object sender, EventArgs e)
        {
            //txtPartNumber.Text = "";
            AssignScreen.Visible = false;
            GridData.Visible = true;
        }

        void UpdateDropDowns()
        {
            ClientManager cm = new ClientManager(User.Identity.Name);
            List<ClientLocation> cls = cm.GetClientLocationsWithOnSiteInventory();
            drpLocationList.Items.Clear();
            drpLocationList.Items.Add(new ListItem("WHS 001", "-1"));
            foreach (ClientLocation cl in cls)
            {
                ListItem li = new ListItem(cl.CompanyName, cl.ClientLocationID.ToString());
                ListItem li2 = new ListItem(cl.CompanyName, cl.ClientLocationID.ToString());
                ListItem li3 = new ListItem(cl.CompanyName, cl.ClientLocationID.ToString());
                drpLocationList.Items.Add(li);
            }
            drpLocationList.SelectedIndex = 0;



            //IFS_InventoryPartsManager ipm = new IFS_InventoryPartsManager(User.Identity.Name);
            //List<MasterIFSLocation> loc = ipm.GetLocationList();
            //drpLocationEditParts.Items.Clear();
            //drpLocationEditPartsTo.Items.Clear();
            ////drpLocationTransferPartTo.Items.Clear();
            //foreach (MasterIFSLocation cl in loc)
            //{
            //    ListItem li = new ListItem(cl.IFSLocation, cl.IFSLocation);
            //    drpLocationEditParts.Items.Add(li);
            //    ListItem li1 = new ListItem(cl.IFSLocation, cl.MasterIFSLocationID.ToString());
            //    drpLocationEditPartsTo.Items.Add(li1);
            //    //ListItem li2 = new ListItem(cl.IFSLocation, cl.MasterIFSLocationID.ToString());
            //    //drpLocationTransferPartTo.Items.Add(li2);
            //}



            ////QuestionManager qm = new QuestionManager(User.Identity.Name);
            ////List<Option> LO = new List<Option>();
            ////LO = qm.GetQuestionOptionList("Manufacturer");
            ////drpManufacturer_03.Items.Clear();
            ////drpManufacturer_03.Items.Add(new ListItem("<None>", "-1"));
            ////foreach (Option o in LO)
            ////{
            ////    ListItem li = new ListItem("(" + o.ScanKey + ") " + o.OptionText, o.OptionID.ToString());
            ////    drpManufacturer_03.Items.Add(li);
            ////}
            ////drpManufacturer_03.SelectedIndex = 0;


        }

        private void UpdateCatagoryDropDown_03(decimal ModelID)
        {
            MasterPartManager mpm = new MasterPartManager(User.Identity.Name);
            List<MasterPart> Parts = mpm.GetMasterPartsForThisModel(ModelID);
            drpDropPart_03.Items.Clear();
            //drpDropPart_03O.Items.Clear();
            foreach (MasterPart o in Parts)
            {
                ListItem li = new ListItem(o.Description, o.MasterPartsID.ToString());
                drpDropPart_03.Items.Add(li);
                //ListItem lio = new ListItem(o.Description, o.MasterPartsID.ToString());
                //drpDropPart_03O.Items.Add(lio);
            }
            drpDropPart_03.Items.Add(new ListItem("<All>", "-1"));
            drpDropPart_03.SelectedIndex = 0;
            //drpDropPart_03O.Items.Add(new ListItem("<All>", "-1"));
            //drpDropPart_03O.SelectedIndex = 0;

        }


        //void btnAssign_Click(object sender, EventArgs e)
        //{
        //    RecordPartNumberAssigned(txtPartNumber.Text);
        //    txtPartNumber.Text = "";
        //    txtPartNumber.Focus();
        //}

        //void RecordPartNumberAssigned(string PartNumber)
        //{
        //    //string Tech = drpTechList.SelectedItem.Text;
        //    string Tech = drpTechList.SelectedItem.Value;
        //    string Location = drpLocationList.SelectedItem.Value;
        //    string Return = "N";
        //    if (chkReturn.Checked == true) { Return = "T"; }
        //    string rValue = AssignPartNumber(PartNumber, Tech, Location, Return, User.Identity.Name);
        //    if (rValue.Contains("ERROR") == true)
        //    {
        //        ListItem item = new ListItem(rValue);
        //        item.Attributes.Add("style", "color:RED;font-weight:bold;");
        //        lstHistory.Items.Add(item);
        //    }
        //    else
        //    {
        //        decimal count = 0;
        //        if (decimal.TryParse(txtCount.Text, out count) == true) { count++; txtCount.Text = count.ToString(); }
        //        ListItem item = new ListItem(rValue);
        //        item.Attributes.Add("style", "color:blue;");
        //        lstHistory.Items.Add(item);
        //    }


        //}
        //public string AssignPartNumber(string PartNumber, string Tech, string Location, string Return, string UserName)
        //{
        //    //if (ESN.Trim().Length == 0 || WayBill.Trim().Length == 0) { return "0"; }
        //    decimal LocationID = -1;
        //    bool isReturned = false;
        //    if (Return == "T") { isReturned = true; }
        //    if (decimal.TryParse(Location, out LocationID) == false) { LocationID = -1; }
        //    MasterPartManager MPM = new MasterPartManager(UserName);
        //    return MPM.MasterPartsTechAssignedLog_Add(PartNumber, Tech, LocationID, isReturned);
        //}




        void btnRefreshDetail_Click(object sender, EventArgs e)
        {
            RefreshAllGrids();
        }
        void btnRefreshNewList_Click(object sender, EventArgs e)
        {
            RefreshAllGrids();
        }
        void btnReturnRefresh_Click(object sender, EventArgs e)
        {
            lblReturningMessage.Text = "";
            UpdateReturningGrid();
        }

        void btnProcessReturnCode_Click(object sender, EventArgs e)
        {
            decimal ID = -1;
            if (decimal.TryParse(txtReturnIDCode.Text, out ID) == false) { ID = -1; }
            if (ID > 0)
            {
                ProcessPartReturn(ID);
                UpdateReturningGrid();
            }
        }

        void RefreshAllGrids()
        {
            UpdateMainGrid();
            UpdateAssignedGrid();
            //UpdateOutOfStockGrid();
        }
        protected void UpdateMainGrid()
        {
            //// Get the list of parts that match this condition.
            MasterPartManager mpm = new MasterPartManager(User.Identity.Name);
            List<GridMasterPartsRequested> mParts = mpm.GetPartsRequestedUnassignedList();
            grdNewData.DataSource = mParts.OrderByDescending(x => x.CreateDate);                   // mpm.GetMasterParts();
            grdNewData.DataBind();
        }
        protected void UpdateAssignedGrid()
        {
            //// Get the list of parts that match this condition.
            MasterPartManager mpm = new MasterPartManager(User.Identity.Name);
            List<GridMasterPartsRequested> mParts = mpm.GetPartsRequestedAssignedList();
            grdAssignedParts.DataSource = mParts.OrderBy(x => x.PickUser).ThenBy(x => x.CreateDate);                   // mpm.GetMasterParts();
            grdAssignedParts.DataBind();
        }

        protected void UpdateOutOfStockGrid()
        {
            //// Get the list of parts that match this condition.
            MasterPartManager mpm = new MasterPartManager(User.Identity.Name);
            List<GridMasterPartsRequested> mParts = mpm.GetPartsRequesteOutOfStockList();

            if (txtIMEIToSearch.Text.Length > 0)
            {
                grdOutOfStockParts.DataSource = mParts.Where(x => x.ESN == txtIMEIToSearch.Text).OrderBy(x => x.PickUser).ThenBy(x => x.CreateDate);                   // mpm.GetMasterParts();
            }
            else if (txtRequestedpartToSearch.Text.Length > 0)
            {
                grdOutOfStockParts.DataSource = mParts.Where(x => x.RequestedPart.ToUpper() == txtRequestedpartToSearch.Text.ToUpper()).OrderBy(x => x.PickUser).ThenBy(x => x.CreateDate);                   // mpm.GetMasterParts();
            }
            else if (txtModelToSearch.Text.Length > 0)
            {
                grdOutOfStockParts.DataSource = mParts.Where(x => x.Model.ToUpper() == txtModelToSearch.Text.ToUpper()).OrderBy(x => x.PickUser).ThenBy(x => x.CreateDate);                   // mpm.GetMasterParts();
            }
            else
            {
                grdOutOfStockParts.DataSource = mParts.OrderBy(x => x.PickUser).ThenBy(x => x.CreateDate);                   // mpm.GetMasterParts();
            }
            grdOutOfStockParts.DataBind();
        }




        protected void UpdateReturningGrid()
        {
            //// Get the list of parts that match this condition.
            MasterPartManager mpm = new MasterPartManager(User.Identity.Name);
            List<PartNumberReturnGridData> mParts = mpm.GetPartsRequestedReturningList(txtReturnESN.Text);
            grdReturnRecords.DataSource = mParts.Where(x => x.Status == drpListToShow.SelectedValue).OrderBy(x => x.ESN).ThenBy(x => x.ReturningDate);                   // mpm.GetMasterParts();
            grdReturnRecords.DataBind();
        }

        void grdNewData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridMasterPartsRequested mpl = (GridMasterPartsRequested)e.Row.DataItem;
                System.Web.UI.WebControls.LinkButton bModel = (System.Web.UI.WebControls.LinkButton)e.Row.FindControl("imgAssignPart");
                if (bModel != null) { bModel.CommandArgument = "-1"; }
                if (mpl != null)
                {
                    if (bModel != null) { bModel.CommandArgument = mpl.MasterPartsRequestedLogID.ToString(); }
                }
            }
        }
        void grdNewData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            System.Web.UI.WebControls.LinkButton btnOpen = (System.Web.UI.WebControls.LinkButton)e.CommandSource;
            string CommandArgument = btnOpen.CommandArgument;

            if (btnOpen.ID.ToUpper() == "IMGASSIGNPART")
            {

                //ScriptManager.RegisterStartupScript(this, GetType(), "xxxx", "alert('trying to Picked! " + "');", true);
                //return;

                decimal ID = -1;
                if (decimal.TryParse(CommandArgument, out ID) == false) { ID = -1; }
                MasterPartManager mpm = new MasterPartManager(User.Identity.Name);

                using (clsLinqDataContext ctx = mpm.GetDataContext(User.Identity.Name))
                {
                    GridMasterPartsRequested pt = mpm.GetGridMasterPartsRequested(ID);
                    if (pt != null)
                    {
                        string rValue = "";
                        rValue = mpm.SetPartRequestedAsPicked(ID);
                        if (rValue.Trim().Length > 0)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "Already Picked", "alert('" + rValue + "');", true);
                        }
                        //else
                        //{
                        //    ScriptManager.RegisterStartupScript(this, GetType(), "xxxx", "alert('Part Picked! " + "');", true);
                        //}
                        //RecordPartNumberAssigned(pt.GMPPartNumber);
                        //txtPartNumber.Text = "";
                        //txtPartNumber.Focus();
                    }
                    RefreshAllGrids();

                }


                //ScriptManager.RegisterStartupScript(this, GetType(), "Open Unit", "alert('imgChangeModel:" + CommandArgument + "');", true);
                //ScriptManager.RegisterStartupScript(this, GetType(), "Open Unit", "OpenUnitAnalysisRPT(" + CommandArgument + ");", true);
            }
            RefreshAllGrids();
        }


        void grdReturnRecords_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                PartNumberReturnGridData mpl = (PartNumberReturnGridData)e.Row.DataItem;
                System.Web.UI.WebControls.LinkButton bModel = (System.Web.UI.WebControls.LinkButton)e.Row.FindControl("imgFill");
                if (bModel != null)
                {
                    bModel.CommandArgument = "-1";
                    bModel.CommandArgument = mpl.MasterPartsTechAssignedLogID.ToString();
                }
            }
        }
        void grdReturnRecords_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            System.Web.UI.WebControls.LinkButton btnOpen = (System.Web.UI.WebControls.LinkButton)e.CommandSource;
            string CommandArgument = btnOpen.CommandArgument;

            if (btnOpen.ID.ToUpper() == "IMGFILL")
            {
                decimal ID = -1;
                if (decimal.TryParse(CommandArgument, out ID) == false) { ID = -1; }
                if (ID > 0)
                {
                    ProcessPartReturn(ID);
                }
                UpdateReturningGrid();
                return;

            }
        }


        void grdAssignedParts_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridMasterPartsRequested mpl = (GridMasterPartsRequested)e.Row.DataItem;
                System.Web.UI.WebControls.LinkButton bModel = (System.Web.UI.WebControls.LinkButton)e.Row.FindControl("imgFill");
                if (bModel != null) { bModel.CommandArgument = "-1"; }
                if (mpl != null)
                {
                    if (bModel != null)
                    {
                        bModel.CommandArgument = mpl.MasterPartsRequestedLogID.ToString();
                    }
                }
                bModel = (System.Web.UI.WebControls.LinkButton)e.Row.FindControl("imgCancel");
                if (bModel != null) { bModel.CommandArgument = "-1"; }
                if (mpl != null)
                {
                    if (bModel != null) { bModel.CommandArgument = mpl.MasterPartsRequestedLogID.ToString(); }
                }
                bModel = (System.Web.UI.WebControls.LinkButton)e.Row.FindControl("imgCancel");
                if (bModel != null) { bModel.CommandArgument = "-1"; }
                if (mpl != null)
                {
                    if (bModel != null) { bModel.CommandArgument = mpl.MasterPartsRequestedLogID.ToString(); }
                }
            }
        }
        void grdAssignedParts_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            System.Web.UI.WebControls.LinkButton btnOpen = (System.Web.UI.WebControls.LinkButton)e.CommandSource;
            string CommandArgument = btnOpen.CommandArgument;
            //lblMessagey.Text = CommandArgument;
            if (btnOpen.ID.ToUpper() == "IMGFILL")
            {
                //lblMessagey.Text = "AAAAAA";

                decimal ID = -1;
                if (decimal.TryParse(CommandArgument, out ID) == false) { ID = -1; }
                MasterPartManager mpm = new MasterPartManager(User.Identity.Name);
                GridMasterPartsRequested part = mpm.GetGridMasterPartsRequested(ID);
                KeyID.Value = "";
                lblPartNote.Text = "";
                lblRequestUser.Text = "";
                lblPartRequested.Text = "";
                lblManufacturer.Text = "";
                hdnManufacturerID.Value = "";
                lblModel.Text = "";
                lblColour.Text = "";
                lblDeviceLocation.Text = "";

                //lblMessagey.Text = "BBBB";


                if (part != null)
                {
                    //lblMessagey.Text = "CCCC";

                    lblPartNote.Text = part.PartNote;
                    lblPartRequested.Text = part.RequestedPart;
                    lblRequestUser.Text = part.TechUser;
                    lblManufacturer.Text = part.Manufacturer;
                    lblModel.Text = part.Model;              // +part.ModelID;
                    lblColour.Text = part.Colour;
                    hdnManufacturerID.Value = part.ManufacturerID.ToString();
                    hdnModelID.Value = part.ModelID.ToString();
                    lblDeviceLocation.Text = part.IFSLocation;
                    // This may need to be set to a part wip location
                    IFSToLocation.Text = part.IFSLocation;
                    lblMessagex.Text = part.IFSLocation;


                    //IFS_InventoryPartsManager ipm = new IFS_InventoryPartsManager(User.Identity.Name);
                    //List<MasterIFSLocation> loc = ipm.GetLocationList();
                    //drpLocationEditParts.Items.Clear();
                    ////drpLocationEditPartsTo.Items.Clear();
                    ////drpLocationTransferPartTo.Items.Clear();
                    //foreach (MasterIFSLocation cl in loc)
                    //{
                    //    ListItem li = new ListItem(cl.IFSLocation, cl.IFSLocation);
                    //    drpLocationEditParts.Items.Add(li);
                    //    //ListItem li1 = new ListItem(cl.IFSLocation, cl.MasterIFSLocationID.ToString());
                    //    //drpLocationEditPartsTo.Items.Add(li1);
                    //    //ListItem li2 = new ListItem(cl.IFSLocation, cl.MasterIFSLocationID.ToString());
                    //    //drpLocationTransferPartTo.Items.Add(li2);
                    //}

                    //if (part.IFSLocation != null && part.IFSLocation.Length > 0)
                    //{
                    //    ListItem li = drpLocationEditPartsTo.Items.FindByText(part.IFSLocation);
                    //    if (li != null && li.Enabled == true) { li.Selected = true; }
                    //}


                    KeyID.Value = CommandArgument;
                    GridData.Visible = false;
                    AssignScreen.Visible = true;
                    UpdateCatagoryDropDown_03((decimal)part.ModelID);
                    UpdateMainGridPM();
                    UpdateMainGridPMOther();
                }
                //ScriptManager.RegisterStartupScript(this, GetType(), "Open Unit", "OpenAssignPart();", true);
                return;
                //mpm.SetPartRequestedAsFilled(ID);
                //RefreshAllGrids();
            }
            if (btnOpen.ID.ToUpper() == "IMGOUTOFSTOCK")
            {
                decimal ID = -1;
                if (decimal.TryParse(CommandArgument, out ID) == false) { ID = -1; }
                MasterPartManager mpm = new MasterPartManager(User.Identity.Name);
                mpm.SetPartRequestedAsOutOfStock(ID);
                RefreshAllGrids();

            }
            if (btnOpen.ID.ToUpper() == "IMGCANCEL")
            {
                decimal ID = -1;
                if (decimal.TryParse(CommandArgument, out ID) == false) { ID = -1; }
                MasterPartManager mpm = new MasterPartManager(User.Identity.Name);
                mpm.SetPartRequestedAsCanceled(ID);
                RefreshAllGrids();
            }
        }

        string ProcessPartReturn(decimal MasterPartsTechAssignedLog)
        {
            MasterPartManager pm = new MasterPartManager(User.Identity.Name);
            // Get the new location
            decimal NewLocatioNID = pm.GetIFSLocationID(txtReturnToIFSLocation.Text);
            if (NewLocatioNID < 1) { lblReturningMessage.Text = "IFS Location Not Valid"; return ""; }
            lblReturningMessage.Text = pm.MasterPartsTechAssignedLog_Return(MasterPartsTechAssignedLog, NewLocatioNID);
            return "";
        }

        private string GetUserIPAddress()
        {
            string ip = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(ip))
            {
                ip = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            return ip;
        }
        //protected void LoadDropDowns()
        //{
        //    using (clsLinqDataContext ctx = new clsLinqDataContext())
        //    {
        //        LoadDropDowns(ctx);
        //    }
        //}
        //protected void LoadDropDowns(clsLinqDataContext ctx)
        //{
        //    QuestionManager qm = new QuestionManager(User.Identity.Name);
        //    List<Option> LO = new List<Option>();
        //    //LO = qm.GetQuestionOptionList(ctx,"Carrier");
        //    //drpCarrier.Items.Clear();
        //    //drpCarrier.Items.Add(new ListItem("<None>", "-1"));
        //    //foreach (Option o in LO)
        //    //{
        //    //    ListItem li = new ListItem("(" + o.ScanKey + ") " + o.OptionText, o.OptionID.ToString());
        //    //    drpCarrier.Items.Add(li);
        //    //}
        //    //drpCarrier.SelectedIndex = 0;


        //    ClientManager cm = new ClientManager(User.Identity.Name);
        //    List<ClientLocation> cls = cm.GetClientLocationsWithOnSiteInventory();
        //    drpLocationList.Items.Clear();
        //    drpLocationList.Items.Add(new ListItem("WHS 001", "-1"));
        //    drpLocationList_02.Items.Clear();
        //    drpLocationList_02.Items.Add(new ListItem("WHS 001", "-1"));
        //    foreach (ClientLocation cl in cls)
        //    {
        //        ListItem li = new ListItem(cl.CompanyName, cl.ClientLocationID.ToString());
        //        ListItem li2 = new ListItem(cl.CompanyName, cl.ClientLocationID.ToString());
        //        ListItem li3 = new ListItem(cl.CompanyName, cl.ClientLocationID.ToString());
        //        drpLocationList.Items.Add(li);
        //        drpLocationList_02.Items.Add(li2);
        //    }
        //    drpLocationList.SelectedIndex = 0;
        //    drpLocationList_02.SelectedIndex = 0;



        //    LO = qm.GetQuestionOptionList(ctx, "Manufacturer");
        //    drpManufacturer_03.Items.Clear();
        //    drpManufacturer_03.Items.Add(new ListItem("<None>", "-1"));
        //    foreach (Option o in LO)
        //    {
        //        ListItem li = new ListItem("(" + o.ScanKey + ") " + o.OptionText, o.OptionID.ToString());
        //        drpManufacturer_03.Items.Add(li);
        //    }
        //    drpManufacturer_03.SelectedIndex = 0;





        //    BasicUserUtilities buu = new BasicUserUtilities(User.Identity.Name);
        //    List<ListItem> Techs = buu.GetTechnicionList();


        //    //LO = qm.GetQuestionOptionList(ctx, "Tech Finished Unit");
        //    drpTechList.Items.Clear();
        //    drpTechList_02.Items.Clear();
        //    drpTechList_02.Items.Add(new ListItem("All", "-1"));
        //    foreach (ListItem o in Techs)
        //    {
        //        ListItem li = new ListItem(o.Text, o.Value);
        //        drpTechList.Items.Add(li);
        //        ListItem li2 = new ListItem(o.Text, o.Value);
        //        drpTechList_02.Items.Add(li2);
        //    }
        //    //foreach (Option o in LO)
        //    //{
        //    //    ListItem li = new ListItem(o.OptionText, o.OptionID.ToString());
        //    //    drpTechList.Items.Add(li);
        //    //    ListItem li2 = new ListItem(o.OptionText, o.OptionID.ToString());
        //    //    drpTechList_02.Items.Add(li2);
        //    //}
        //    drpTechList.SelectedIndex = 0;
        //    drpTechList_02.SelectedIndex = 0;



        //    MasterPartManager mpm = new MasterPartManager(User.Identity.Name);
        //    List<MasterPart> Parts = mpm.GetMasterParts(ctx);
        //    drpDropPart_03.Items.Clear();
        //    foreach (MasterPart o in Parts)
        //    {
        //        ListItem li = new ListItem(o.Description, o.MasterPartsID.ToString());
        //        drpDropPart_03.Items.Add(li);
        //    }
        //    drpDropPart_03.Items.Add(new ListItem("<All>", "-1"));
        //    drpDropPart_03.SelectedIndex = 0;

        //    //MasterPartManager mpm = new MasterPartManager(User.Identity.Name);
        //    //List<MasterPart> Parts = mpm.GetMasterParts(ctx);
        //    //drpDropPart.Items.Clear();
        //    //drpChangeCategoryPart.Items.Clear();
        //    //foreach (MasterPart o in Parts)
        //    //{
        //    //    ListItem li = new ListItem(o.Description, o.MasterPartsID.ToString());
        //    //    drpDropPart.Items.Add(li);
        //    //    ListItem l1 = new ListItem(o.Description, o.MasterPartsID.ToString());
        //    //    drpChangeCategoryPart.Items.Add(l1);
        //    //}
        //    //drpDropPart.Items.Add(new ListItem("<All>", "-1"));
        //    //drpDropPart.SelectedIndex = 0;
        //    //drpChangeCategoryPart.Items.Add(new ListItem("<All>", "-1"));
        //    //drpChangeCategoryPart.SelectedIndex = 0;



        //    ////LO = qm.GetQuestionOptionList(ctx,"Model");
        //    ////drpModel.Items.Clear();
        //    //////drpModel.Items.Add(new ListItem("<None>", "-1"));
        //    ////foreach (Option o in LO)
        //    ////{
        //    ////    ListItem li = new ListItem("(" + o.ScanKey + ") " + o.OptionText, o.OptionID.ToString());
        //    ////    drpModel.Items.Add(li);
        //    ////}
        //    ////drpModel.SelectedIndex = 0;



        //    ////MultiModelDropDown.Items.Clear();
        //    ////MultiModelDropDown.Items.Add(new ListItem("None", "-1"));
        //    ////foreach (Option o in LO)
        //    ////{
        //    ////    ListItem li = new ListItem("(" + o.ScanKey + ") " + o.OptionText, o.OptionID.ToString());
        //    ////    MultiModelDropDown.Items.Add(li);
        //    ////}
        //    ////MultiModelDropDown.SelectedItems.Clear();
        //    ////MultiModelDropDown.SelectedItems.Add(MultiModelDropDown.Items[0]);



        //    //LO = qm.GetQuestionOptionList(ctx, "Model");
        //    //chkModels.Items.Clear();
        //    //chkModels.Items.Add(new ListItem("None", "-1"));
        //    //chkEditModels.Items.Clear();
        //    //chkEditModels.Items.Add(new ListItem("None", "-1"));
        //    //foreach (Option o in LO)
        //    //{
        //    //    ListItem li = new ListItem(o.OptionText, o.OptionID.ToString());
        //    //    ListItem l1 = new ListItem(o.OptionText, o.OptionID.ToString());
        //    //    chkModels.Items.Add(li);
        //    //    chkEditModels.Items.Add(l1);
        //    //}
        //    //chkModels.SelectedIndex = 0;
        //    //chkEditModels.SelectedIndex = 0;
        //}

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



        //void btnClear_Click(object sender, EventArgs e)
        //{
        //    PartNumberList.Value = "";
        //    //txtWaybill.Text = "";
        //    //lastWayBill.Value = "";
        //    chkReturn.Checked = false;
        //    txtPartNumber.Text = "";
        //    txtCount.Text = "0";
        //    lblWarningMessage.Text = "";
        //    //drpCourier.SelectedIndex = 0;
        //    lstHistory.Items.Clear();
        //    txtPartNumber.Focus();
        //    ////txtWaybill.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13) || (event.which == 9) || (event.keyCode == 9)) {CleanData();SetESNFocus();return false;}} else {return true}; ");
        //    ////txtWaybill.Attributes.Add("onblur", "IsMessage();return false;");

        //    ////txtESN.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13) || (event.which == 9) || (event.keyCode == 9)) {SetWayBillFocus();return false;}} else {return true}; ");
        //    ////txtESN.Attributes.Add("onblur", "RecordScanKey();return false;");

        //}
    }







}