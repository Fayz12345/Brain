using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using Factory_DataModel;
using GMPI_WebApp.DataManagers;

namespace GMPI_WebApp
{
    public partial class IFSCycleCount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            btnFindBatch.Click += new EventHandler(btnFindBatch_Click);
            btnResetCount.Click += new EventHandler(btnResetCount_Click);
            chkAutoReturn.CheckedChanged += new EventHandler(chkAutoReturn_CheckedChanged);
            //btnPICRecord.Click += new EventHandler(btnPICRecord_Click);
            btnLockBatch.Click += new EventHandler(btnLockBatch_Click);
            TabDevicePart.ActiveTabChanged += new EventHandler(TabDevicePart_ActiveTabChanged);
            TabPartFlow.ActiveTabChanged += new EventHandler(TabPartFlow_ActiveTabChanged);
            if (!IsPostBack)
            {
                hdnUserName.Value = User.Identity.Name;
                //setupDropDowns();
                SetLastOpenBatch();
               txtIFSLocation.Focus();
            }


            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "SetKeydownEvents();", true);
        }

        void TabPartFlow_ActiveTabChanged(object sender, EventArgs e)
        {
            if (TabPartFlow.ActiveTab.HeaderText == "QTY/SKU") { hdnActiveTab.Value = "A"; }
            else { hdnActiveTab.Value = "B"; }
        }

        void TabDevicePart_ActiveTabChanged(object sender, EventArgs e)
        {
            hdnDeviceOrPart.Value = TabDevicePart.ActiveTab.HeaderText;
        }

        void btnFindBatch_Click(object sender, EventArgs e)
        {
            txtIFSLocation.Focus();
            ScanKey.Enabled = false;
            hdnCycleInventoryCountIterationHeaderID.Value = "-1";
            lblResponceMessage.Text = "";
            if (txtIFSLocation.Text.Length == 0)
            {
                lblResponceMessage.Text = "No Location Given";
                return;
            }
            IFSLocation Location = new IFSLocation(txtIFSLocation.Text);
            if (Location.isValid == false)
            {
                lblResponceMessage.Text = "Not a valid location";
                return;
            }
            if (Location.IsThisFrozen(User.Identity.Name) == false)
            {
                lblResponceMessage.Text = "Location is Not frozen, check location.";
                return;
            }
            CycleCountManager CM = new CycleCountManager(User.Identity.Name);
            string Message = "";
            string Batch = "";
            Batch = CM.GetCycleCountBatch(Location.Text, ref Message);

            if (Message.Length > 4 && Message.Substring(0, 5) != "Error")
            {
                string[] Keys = Message.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                hdnCycleInventoryCountIterationHeaderID.Value = Keys[1];
                txtBatch.Text = Keys[2];
                ScanKey.Enabled = true;
                ScanKeyB.Enabled = true;
                ScanKeyP.Enabled = true;
                txtQTYx.Enabled = true;
                txtQTYxB.Enabled = true;
                ScanKey.Focus();
            }
            lblResponceMessage.Text = Message;
        }

        //void btnVersionPrior_Click(object sender, EventArgs e)
        //{
        //    ListItem Current = lstHistory.Items.FindByText(lblCurrentIMEI.Text);
        //    lblCurrentIMEI.Text = "";
        //    if (Current == null)
        //    {
        //        if (lstHistory.Items.Count < 1) { lblCurrentIMEI.Text = ""; }
        //        lblCurrentIMEI.Text = lstHistory.Items[0].Text;
        //    }
        //    if (Current != null)
        //    {
        //        if (lstHistory.Items.IndexOf(Current) != 0)
        //        {
        //            lblCurrentIMEI.Text = lstHistory.Items[lstHistory.Items.IndexOf(Current) - 1].Text;
        //        }
        //    }
        //    // Load the grid.



        //    //lblCurrentIMEI
        //}

        //void btnVersionNext_Click(object sender, EventArgs e)
        //{
        //    ListItem Current = lstHistory.Items.FindByText(lblCurrentIMEI.Text);
        //    lblCurrentIMEI.Text = "";
        //    if (Current == null)
        //    {
        //        if (lstHistory.Items.Count < 1) { lblCurrentIMEI.Text = ""; }
        //        lblCurrentIMEI.Text = lstHistory.Items[0].Text;
        //    }
        //    if (Current != null)
        //    {
        //        int tCount = lstHistory.Items.Count;
        //        int mcount = lstHistory.Items.IndexOf(Current);
        //        if (tCount > mcount + 1)
        //        {
        //            lblCurrentIMEI.Text = lstHistory.Items[mcount + 1].Text;
        //        }
        //    }
        //    // Load the grid.



        //    //lblCurrentIMEI
        //}





        //void drpModel_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    FillMMCCDropDowns("Model");
        //}

        //void drpManufacturer_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    FillMMCCDropDowns("Manufacturer");
        //}

        //void drpCarrier_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    FillMMCCDropDowns("Carrier");
        //}

        //void grdSearchSuggest_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    Button btn = (Button)e.CommandSource;
        //    if (btn != null) { txtLocation.Text = btn.CommandArgument; }
        //}
        //void grdSearchSuggest_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        GridIFSLocationSuggestion rda = ((GridIFSLocationSuggestion)e.Row.DataItem);
        //        Button btn = (Button)e.Row.FindControl("btnPick");
        //        if (btn != null)
        //        {
        //            btn.CommandArgument = rda.IFSLocation;
        //        }
        //    }
        //}
        //void btnSearchRefresh_Click(object sender, EventArgs e)
        //{
        //    IFS_InventoryManager m = new IFS_InventoryManager(User.Identity.Name);
        //    string SearchIMEI = "";
        //    if (lstHistory.Items.Count > 0)
        //    {
        //        if (lstHistory.SelectedIndex > 0)
        //        {
        //            SearchIMEI = lstHistory.SelectedItem.Text;
        //        }
        //        else
        //        {
        //            SearchIMEI = lstHistory.Items[0].Text;
        //        }
        //    }
        //    grdSearchSuggest.DataSource = m.GetSuggestedLocation(SearchIMEI.Trim());
        //    grdSearchSuggest.DataBind();
        //}
        //void imgbtnDeleteIMIE_Click(object sender, ImageClickEventArgs e)
        //{
        //    List<ListItem> itms = new List<ListItem>();
        //    foreach (ListItem i in lstHistory.Items)
        //    {
        //        if (i.Selected == true)
        //        {
        //            itms.Add(i);
        //        }
        //    }
        //    foreach (ListItem i in itms)
        //    {
        //        lstHistory.Items.Remove(i);
        //    }
        //}

        //void imgbtnClear_Click(object sender, ImageClickEventArgs e)
        //{
        //    Clear();
        //}

        //private void Clear()
        //{
        //    //IFS_InventoryManager m = new IFS_InventoryManager(User.Identity.Name);
        //    List<GridIFSLocationSuggestion> blank = new List<GridIFSLocationSuggestion>();
        //    lblMessage.Text = "";
        //    grdSearchSuggest.DataSource = blank;
        //    grdSearchSuggest.DataBind();
        //    lstHistory.Items.Clear();
        //    txtCount.Text = lstHistory.Items.Count.ToString();

        //    txtESN.Text = "";
        //    lblBinNumber.Text = "";
        //    IFSMoveFromLocationOnly.Text = "";
        //    txtFromSku.Text = "";
        //    txtFromLocation.Text = "";

        //    txtESN.Focus();
        //}

        //void btrRecord_Click(object sender, EventArgs e)
        //{
        //    if (txtESN.Text.Length > 0) { RecordESN(); }
        //    else if (lblBinNumber.Text.Length > 0) { RecordBin(); }
        //    else if (IFSMoveFromLocationOnly.Text.Length > 0) { RecordLocation(); }
        //    else if (txtFromLocation.Text.Length > 0 && txtFromSku.Text.Length > 0) { RecordSite(); }

        //}
        //void btnRecordSite_Click(object sender, EventArgs e)
        //{
        //    RecordSite();
        //}
        //void btrRecordLocation_Click(object sender, EventArgs e)
        //{
        //    RecordLocation();
        //}
        //void btnRecordBin_Click(object sender, EventArgs e)
        //{
        //    RecordBin();
        //}
        //void btnPasteParse_Click(object sender, EventArgs e)
        //{
        //    RecordPasteParse();
        //}

        //private void RecordESN()
        //{
        //    if (txtESN.Text.Length > 0 && lstHistory.Items.FindByText(txtESN.Text) == null)
        //    {
        //        lstHistory.Items.Add(new ListItem(txtESN.Text));
        //    }
        //    txtCount.Text = lstHistory.Items.Count.ToString();
        //    txtESN.Text = "";
        //    txtESN.Focus();
        //}
        //private void RecordSite()
        //{
        //    if (txtFromLocation.Text.Length > 0 && txtFromSku.Text.Length > 0)
        //    {
        //        ReceiveDetailManager rd = new ReceiveDetailManager(User.Identity.Name);
        //        List<string> dta = rd.getIMEIsInLocation_FromSitProjectSkuLocationCondition(drpMoveIFSSite.SelectedItem.Text
        //                                                                                  , drpMoveIFSProject.SelectedItem.Value
        //                                                                                  , txtFromSku.Text
        //                                                                                  , txtFromLocation.Text
        //                                                                                  , drpMoveIFSCondition.SelectedItem.Text
        //                                                                                  , txtLocation.Text);
        //        foreach (string x in dta) { if (x.Length > 0 && lstHistory.Items.FindByText(x) == null) { lstHistory.Items.Add(new ListItem(x)); } }
        //    }
        //    txtCount.Text = lstHistory.Items.Count.ToString();
        //    txtFromSku.Text = "";
        //    txtFromLocation.Text = "";
        //    txtFromSku.Focus();
        //}
        //private void RecordLocation()
        //{
        //    if (IFSMoveFromLocationOnly.Text.Length > 0)
        //    {
        //        ReceiveDetailManager rd = new ReceiveDetailManager(User.Identity.Name);
        //        List<string> dta = rd.getIMEIsInLocation(IFSMoveFromLocationOnly.Text);
        //        foreach (string x in dta) { if (x.Length > 0 && lstHistory.Items.FindByText(x) == null) { lstHistory.Items.Add(new ListItem(x)); } }
        //    }
        //    txtCount.Text = lstHistory.Items.Count.ToString();
        //    IFSMoveFromLocationOnly.Text = "";
        //    IFSMoveFromLocationOnly.Focus();
        //}
        //private void RecordBin()
        //{
        //    if (lblBinNumber.Text.Length > 0)
        //    {
        //        ReceiveDetailManager rd = new ReceiveDetailManager(User.Identity.Name);
        //        List<string> dta = rd.getIMEIsInBin(lblBinNumber.Text);
        //        foreach (string x in dta) { if (x.Length > 0 && lstHistory.Items.FindByText(x) == null) { lstHistory.Items.Add(new ListItem(x)); } }
        //    }
        //    txtCount.Text = lstHistory.Items.Count.ToString();
        //    lblBinNumber.Text = "";
        //    lblBinNumber.Focus();
        //}
        //private void RecordPasteParse()
        //{
        //    //char Delimitor = ',';
        //    //if (PasteDeliminator.SelectedItem.Value.ToUpper() == "SPACE") { Delimitor = ' '; }
        //    if (txtPasteParse.Text.Length > 0)
        //    {
        //        if (PasteDeliminator.SelectedItem.Value.ToUpper() == "EXCEL")
        //        {
        //            //List<string> data = txtPasteParse.Text.Split(new string[] { "\r\n", "\n", " " }, StringSplitOptions.RemoveEmptyEntries).ToList();
        //            List<string> data = txtPasteParse.Text.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
        //            foreach (string x in data) { if (x.Length > 0 && lstHistory.Items.FindByText(x) == null) { lstHistory.Items.Add(new ListItem(x)); } }
        //        }
        //        if (PasteDeliminator.SelectedItem.Value.ToUpper() == "SPACE")
        //        {
        //            List<string> data = txtPasteParse.Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        //            foreach (string x in data) { if (x.Length > 0 && lstHistory.Items.FindByText(x) == null) { lstHistory.Items.Add(new ListItem(x)); } }
        //        }
        //        if (PasteDeliminator.SelectedItem.Value.ToUpper() == "COMMA")
        //        {
        //            List<string> data = txtPasteParse.Text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        //            foreach (string x in data) { if (x.Length > 0 && lstHistory.Items.FindByText(x) == null) { lstHistory.Items.Add(new ListItem(x)); } }
        //        }
        //    }
        //    txtCount.Text = lstHistory.Items.Count.ToString();
        //    txtPasteParse.Text = "";
        //    txtPasteParse.Focus();
        //}

        ////void btnInvalidate_Click(object sender, EventArgs e)
        ////{
        ////    lblPIMessage.Text = "";

        ////    if (txtBatch.Text.Length == 0)
        ////    {
        ////        lblPIMessage.Text = "No Batch number Given.";
        ////        return;
        ////    }
        ////    IFS_InventoryManager im = new IFS_InventoryManager(User.Identity.Name);
        ////    string rmessage = im.LogPhysicalInventoryBatchInvalid(txtBatch.Text);
        ////    ClearPIData();
        ////    lblPIMessage.Text = rmessage;
        ////}

        ////void btnGenerateIFS_Click(object sender, EventArgs e)
        ////{
        ////    lblPIMessage.Text = "";
        ////    if (txtBatch.Text.Length == 0)
        ////    {
        ////        lblPIMessage.Text = "No Batch number Given.";
        ////        return;
        ////    }
        ////    IFS_InventoryManager im = new IFS_InventoryManager(User.Identity.Name);
        ////    lblPIMessage.Text = im.LogPhysicalInventoryBatchToIFS(txtBatch.Text);
        ////}

        ////void txtBatch_TextChanged(object sender, EventArgs e)
        ////{
        ////    lblPIMessage.Text = "";
        ////    ScanKey.Enabled = true;
        ////    if (txtBatch.Text.Length == 0) { return; }
        ////    IFS_InventoryManager im = new IFS_InventoryManager(User.Identity.Name);
        ////    if (im.IsPhysicalInventoryBatchLocked(txtBatch.Text) == true)
        ////    {
        ////        ScanKey.Enabled = false;
        ////        lblPIMessage.Text = "This batch (" + txtBatch.Text + ") is locked!";
        ////    }
        ////}


        #region Physical Inventory Count
        void btnLockBatch_Click(object sender, EventArgs e)
        {
            IFS_InventoryManager im = new IFS_InventoryManager(User.Identity.Name);
            lblResponceMessage.Text = "";
            if (txtBatch.Text.Length == 0)
            {
                lblResponceMessage.Text = "No Batch number Given.";
                //txtBatch.Text = im.GetNextPIBatchNumber();
                return;
            }
            string rmessage = im.LogPhysicalInventoryBatchLocked(txtBatch.Text);
            ClearPIData();
            //txtBatch.Text = im.GetNextPIBatchNumber();
            lblResponceMessage.Text = rmessage;
        }

        void SetLastOpenBatch()
        {
            ClearPIData();
            IFS_InventoryManager im = new IFS_InventoryManager(User.Identity.Name);
            hdnBatchNumber.Value = im.LastOpenBatchNumber;
            txtBatch.Text = hdnBatchNumber.Value;
            txtPMCount.Text = im.LastOpenBatchCount;
            txtPMCountError.Text = im.LastOpenBatchErrorCount;
        }


        void btnResetCount_Click(object sender, EventArgs e)
        {
            ClearPIData();
        }

        void ClearPIData()
        {
            txtPMCount.Text = "0";
            txtPMCountError.Text = "0";
            //chkKitted.Checked = false;
            //chkUnlocked.Checked = false;
            txtIFSLocation.Text = "";
            lblResponceMessage.Text = "";
            txtBatch.Text = "";
            hdnBatchNumber.Value = "";
            //drpIFSCondition.SelectedIndex = 0;
            txtQTYx.Text = "";
            ScanKeyP.Text = "";
            ScanKeyB.Text = "";
            txtQTYxB.Text = "";
        }



        void chkAutoReturn_CheckedChanged(object sender, EventArgs e)
        {
            btnPICRecord.Enabled = true;
            if (chkAutoReturn.Checked == true) { btnPICRecord.Enabled = false; }
        }

        //void btnPICRecord_Click(object sender, EventArgs e)
        //{
        //    //decimal MasterIFSLocationID = -1;
        //    //decimal MasterIFSCondtionID = -1;
        //    //string ESN = "";
        //    //string Batch = "";
        //    //string IFSSite = "";
        //    //string IFSProject = "";
        //    //string SKU = "";
        //    //string IFSLocation = "";
        //    //string IFSCondition = "";
        //    //string Grade = "";
        //    ////string POReceiptDate = "";
        //    //bool LogOnly;
        //    //string UserName;

        //    //LogOnly = false; chkUpdateIMEI.Checked;
        //    ////decimal.TryParse(drpIFSLocation.SelectedItem.Value, out MasterIFSLocationID);
        //    //decimal.TryParse(drpIFSCondition.SelectedItem.Value, out MasterIFSCondtionID);
        //    //ESN = ScanKey.Text;
        //    //Batch = txtBatch.Text;
        //    //IFSSite = drpIFSSite.SelectedItem.Text;
        //    //IFSProject = drpIFSProject.SelectedItem.Value;
        //    //Grade = "";             // drpGrade.SelectedItem.Text;
        //    ////POReceiptDate = CalAnswer.Text;
        //    //SKU = "";
        //    //IFSLocation = txtIFSLocation.Text;
        //    //IFSCondition = drpIFSCondition.SelectedItem.Text;
        //    //UserName = User.Identity.Name;
        //    //LogPhysicalDeviceCount(MasterIFSLocationID, MasterIFSCondtionID, ESN, Batch, IFSSite, IFSProject, SKU, IFSLocation, IFSCondition, Grade, LogOnly, UserName);
        //}

        private void LogPhysicalDeviceCount(decimal MasterIFSLocationID, decimal MasterIFSCondtionID, string ESN, string Batch, string IFSSite, string IFSProject, string SKU, string IFSLocation, string IFSCondition, string Grade, bool LogOnly, string UserName)
        {
            //IFS_InventoryManager im = new IFS_InventoryManager(User.Identity.Name);
            //im.LogPhysicalInventoryCount(MasterIFSLocationID, MasterIFSCondtionID, ESN, Batch, IFSSite, IFSProject, SKU, IFSLocation, IFSCondition, Grade, chkKitted.Checked, chkUnlocked.Checked, LogOnly, UserName);
        }




        #endregion

        //void setupDropDowns()
        //{
        //    IFS_InventoryPartsManager ipm = new IFS_InventoryPartsManager(User.Identity.Name);

        //    //IFSLocation xxx = new IFSLocation("");


        //    //List<MasterIFSLocation> loc = ipm.GetLocationList();
        //    //drpIFSLocation.Items.Clear();
        //    //foreach (MasterIFSLocation cl in loc)
        //    //{
        //    //    ListItem li = new ListItem(cl.IFSLocation, cl.MasterIFSLocationID.ToString());
        //    //    drpIFSLocation.Items.Add(li);
        //    //}


        //    //QuestionManager qm = new QuestionManager(User.Identity.Name);
        //    //List<Option> ol = qm.GetQuestionOptionList("IFS Conditions");
        //    //drpIFSCondition.Items.Clear();
        //    //foreach (Option o in ol.OrderBy(x => x.Sequence))
        //    //{
        //    //    ListItem x = new ListItem(o.OptionText, o.OptionID.ToString());
        //    //    ListItem y = new ListItem(o.OptionText, o.Name);
        //    //    ListItem z = new ListItem(o.OptionText, o.Name);
        //    //    drpIFSCondition.Items.Add(x);
        //    //}


        //    //decimal Count = 0;
        //    //List<string> s = ipm.GetSiteList();
        //    //drpIFSSite.Items.Clear();
        //    //foreach (string i in s)
        //    //{
        //    //    Count++;
        //    //    ListItem x = new ListItem(i, Count.ToString());
        //    //    ListItem y = new ListItem(i, Count.ToString());
        //    //    if (x.Text == "C1NA") { x.Selected = true; }
        //    //    drpIFSSite.Items.Add(x);
        //    //}


        //    //Count = 0;
        //    //List<PairStringValue> p = ipm.GetProjectList();
        //    //drpIFSProject.Items.Clear();
        //    //foreach (PairStringValue i in p)
        //    //{
        //    //    Count++;
        //    //    ListItem x = new ListItem(i.Key + " - " + i.Value, i.Key);
        //    //    ListItem y = new ListItem(i.Key + " - " + i.Value, i.Key);
        //    //    if (x.Text == "BRCE") { x.Selected = true; }
        //    //    drpIFSProject.Items.Add(x);
        //    //}


        //    //FillMMCCDropDowns("Carrier");


        //}

        #region Control Carrier Manufacturer Model Colour Dropdowns
        //void FillMMCCDropDowns(string DropDownName)
        //{
        //    decimal CarrierID = -1;
        //    decimal ManufacturerID = -1;
        //    decimal ModelID = -1;
        //    //decimal ColourID = -1;
        //    string CarrierKey = "";
        //    string ManufacturerKey = "";
        //    string ModelKey = "";
        //    if (DropDownName == "Carrier")
        //    {
        //        CarrierKey = "-1";
        //        if (decimal.TryParse(CarrierKey, out CarrierID) == true) { FillManufacturer(CarrierID); }

        //    }
        //    if (DropDownName == "Manufacturer")
        //    {
        //        CarrierKey = "-1";
        //        ManufacturerKey = "-1";
        //        if (decimal.TryParse(CarrierKey, out CarrierID) == true
        //            && decimal.TryParse(ManufacturerKey, out ManufacturerID) == true)
        //        { FillModel(CarrierID, ManufacturerID); }

        //    }

        //    if (DropDownName == "Model")
        //    {
        //        CarrierKey = "-1";
        //        ManufacturerKey = "-1";
        //        ModelKey = "-1";

        //        if (decimal.TryParse(CarrierKey, out CarrierID) == true
        //            && decimal.TryParse(ManufacturerKey, out ManufacturerID) == true
        //            && decimal.TryParse(ModelKey, out ModelID) == true)
        //        { FillColour(CarrierID, ManufacturerID, ModelID); }
        //    }
        //    //if (DropDownName == "Carrier")
        //    //{
        //    //}
        //}
        //void FillManufacturer(decimal CarrierID)
        //{
        //    MasterCarrierManufacturerModelColourManager MM = new MasterCarrierManufacturerModelColourManager(User.Identity.Name);
        //    List<MasterCarrierManufacturerLookup> ml = MM.GetMasterManufacturerList(CarrierID.ToString());
        //    drpManufacturer.Items.Clear();
        //    foreach (MasterCarrierManufacturerLookup o in ml.OrderBy(x => x.Manufacturer))
        //    {
        //        ListItem x = new ListItem(o.Manufacturer, o.OptionManufacturerID.ToString());
        //        drpManufacturer.Items.Add(x);
        //    }
        //    if (drpManufacturer.Items.Count > 0) { drpManufacturer.SelectedIndex = 0; }
        //    FillMMCCDropDowns("Manufacturer");
        //}
        //void FillModel(decimal CarrierID, decimal ManufacturerID)
        //{
        //    MasterCarrierManufacturerModelColourManager MM = new MasterCarrierManufacturerModelColourManager(User.Identity.Name);
        //    List<MasterCarrierManufacturerLookup> ml = MM.GetMasterModelList(CarrierID.ToString(), ManufacturerID.ToString());
        //    drpModel.Items.Clear();
        //    foreach (MasterCarrierManufacturerLookup o in ml.OrderBy(x => x.Model))
        //    {
        //        ListItem x = new ListItem(o.Model, o.OptionModelID.ToString());
        //        drpModel.Items.Add(x);
        //    }
        //    if (drpModel.Items.Count > 0) { drpModel.SelectedIndex = 0; }
        //    FillMMCCDropDowns("Model");
        //}
        //void FillColour(decimal CarrierID, decimal ManufacturerID, decimal ModelID)
        //{
        //    MasterCarrierManufacturerModelColourManager MM = new MasterCarrierManufacturerModelColourManager(User.Identity.Name);
        //    List<MasterCarrierManufacturerLookup> ml = MM.GetMasterColourList(CarrierID.ToString(), ManufacturerID.ToString(), ModelID.ToString());
        //    drpColour.Items.Clear();
        //    foreach (MasterCarrierManufacturerLookup o in ml.OrderBy(x => x.Colour))
        //    {
        //        ListItem x = new ListItem(o.Colour, o.OptionColourID.ToString());
        //        drpColour.Items.Add(x);
        //    }
        //    if (drpColour.Items.Count > 0) { drpColour.SelectedIndex = 0; btnToSKU.Enabled = true; }

        //}
        #endregion





        //void btnToSKU_Click(object sender, EventArgs e)
        //{
        //    if (lstHistory.Items.Count < 1)
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "alert('Error, no from data given!');", true);
        //        lblMessage.Text = "No From Data given!";
        //        return;
        //    }

        //    string sCarrierID = drpCarrier.SelectedItem.Value;
        //    string sManufacturerID = drpManufacturer.SelectedItem.Value;
        //    string sModelID = drpModel.SelectedItem.Value;
        //    string sColour = drpColour.SelectedItem.Value;


        //    decimal CarrierID = -1;
        //    decimal ManufacturerID = -1;
        //    decimal ModelID = -1;
        //    decimal ColourID = -1;

        //    if (decimal.TryParse(sCarrierID, out CarrierID) == false) { lblMessage.Text = "Invalid Carrier"; return; }
        //    if (decimal.TryParse(sManufacturerID, out ManufacturerID) == false) { lblMessage.Text = "Invalid Manufacturer"; return; }
        //    if (decimal.TryParse(sModelID, out ModelID) == false) { lblMessage.Text = "Invalid Model"; return; }
        //    if (decimal.TryParse(sColour, out ColourID) == false) { lblMessage.Text = "Invalid Colour"; return; }


        //    string IP = GetUserIPAddress();
        //    ReceiveDetailManager rd = new ReceiveDetailManager(User.Identity.Name);
        //    if (lstHistory.Items.Count > 0)
        //    {
        //        string ErrorMessage = "";
        //        int count = 0;
        //        int Errorcount = 0;
        //        string rMessage = "";
        //        foreach (ListItem i in lstHistory.Items)
        //        {

        //            rMessage = rd.IFSSKUUpdate_ESN(i.Text, drpCarrier.SelectedItem.Text, drpManufacturer.SelectedItem.Text, drpModel.SelectedItem.Text, drpColour.SelectedItem.Text, IP);
        //            if (rMessage.Substring(0, 6) != "Error:") { count++; }
        //            if (rMessage.Substring(0, 6) == "Error:") { Errorcount++; ErrorMessage += (ErrorMessage.Length > 0 ? "/" : "") + rMessage; }
        //        }
        //        Clear();
        //        string FullMessage = count.ToString() + " Devices SKU Changed";
        //        if (Errorcount > 0) { FullMessage += " - " + Errorcount.ToString() + " Devices Not SKU Changed"; }
        //        if (Errorcount > 0) { FullMessage += " - " + ErrorMessage; }
        //        lblMessage.Text = FullMessage;
        //    }
        //}
        //void btnTOCondition_Click(object sender, EventArgs e)
        //{
        //    if (lstHistory.Items.Count < 1)
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "alert('Error, no from data given!');", true);
        //        lblMessage.Text = "No From Data given!";
        //        return;
        //    }
        //    string CondtionCode = drpChangeToCondition.SelectedItem.Value;
        //    string ConditionText = drpChangeToCondition.SelectedItem.Text;
        //    string IP = GetUserIPAddress();
        //    ReceiveDetailManager rd = new ReceiveDetailManager(User.Identity.Name);
        //    if (lstHistory.Items.Count > 0)
        //    {
        //        string ErrorMessage = "";
        //        int count = 0;
        //        int Errorcount = 0;
        //        string rMessage = "";
        //        foreach (ListItem i in lstHistory.Items)
        //        {
        //            rMessage = rd.IFSConditionUpdate_ESN(i.Text, CondtionCode, ConditionText, IP);
        //            if (rMessage.Substring(0, 6) != "Error:") { count++; }
        //            if (rMessage.Substring(0, 6) == "Error:") { Errorcount++; ErrorMessage += (ErrorMessage.Length > 0 ? "/" : "") + rMessage; }
        //        }
        //        Clear();
        //        string FullMessage = count.ToString() + " Devices Condition Changed";
        //        if (Errorcount > 0) { FullMessage += " - " + Errorcount.ToString() + " Devices Not Changed"; }
        //        if (Errorcount > 0) { FullMessage += " - " + ErrorMessage; }
        //        lblMessage.Text = FullMessage;
        //    }

        //}
        //void btnUpdateLocation_Click(object sender, EventArgs e)
        //{
        //    if (txtLocation.Text.Length == 0)
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "alert('No to location given!');", true);
        //        lblMessage.Text = "No to location given!";
        //        return;
        //    }
        //    #region the Old Way
        //    //IFS_InventoryManager im = new IFS_InventoryManager(User.Identity.Name);
        //    //if (im.IsLocationValid(txtLocation.Text) == false)
        //    //{
        //    //    ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "alert('Invalid TO location given!');", true);
        //    //    lblMessage.Text = "Invalid TO location given!";
        //    //    return;
        //    //}
        //    //if (lstHistory.Items.Count < 1)
        //    //{
        //    //    ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "alert('Error, no from data given!');", true);
        //    //    lblMessage.Text = "No From Data given!";
        //    //    return;
        //    //}

        //    //txtLocation.Text = txtLocation.Text.Trim().ToUpper();
        //    //string IP = GetUserIPAddress();
        //    //ReceiveDetailManager rd = new ReceiveDetailManager(User.Identity.Name);
        //    //if (lstHistory.Items.Count > 0)
        //    //{
        //    //    string ErrorMessage = "";
        //    //    int count = 0;
        //    //    int Errorcount = 0;
        //    //    string rMessage = "";
        //    //    foreach (ListItem i in lstHistory.Items)
        //    //    {
        //    //        rMessage = rd.IFSLocationUpdate_ESN(i.Text, txtLocation.Text, IP);
        //    //        if (rMessage.Substring(0, 6) != "Error:") { count++; }
        //    //        if (rMessage.Substring(0, 6) == "Error:") { Errorcount++; ErrorMessage += (ErrorMessage.Length > 0 ? "/" : "") + rMessage; }
        //    //    }

        //    //    Clear();
        //    //    string FullMessage = count.ToString() + " Devices Moved";
        //    //    if (Errorcount > 0) { FullMessage += " - " + Errorcount.ToString() + " Devices Not Moved"; }
        //    //    if (Errorcount > 0) { FullMessage += " - " + ErrorMessage; }
        //    //    lblMessage.Text = FullMessage;
        //    //}

        //    #endregion
        //    #region The New Way
        //    IFSLocation location = new IFSLocation(txtLocation.Text);
        //    //IFS_InventoryManager im = new IFS_InventoryManager(User.Identity.Name);
        //    if (location.isValid == false)
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "alert('Invalid TO location given!');", true);
        //        lblMessage.Text = "Invalid TO location given!";
        //        return;
        //    }
        //    if (lstHistory.Items.Count < 1)
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "alert('Error, no from data given!');", true);
        //        lblMessage.Text = "No From Data given!";
        //        return;
        //    }

        //    txtLocation.Text = location.Text;
        //    string IP = GetUserIPAddress();
        //    ReceiveDetailManager rd = new ReceiveDetailManager(User.Identity.Name);
        //    if (lstHistory.Items.Count > 0)
        //    {
        //        string ErrorMessage = "";
        //        int count = 0;
        //        int Errorcount = 0;
        //        string rMessage = "";
        //        foreach (ListItem i in lstHistory.Items)
        //        {
        //            rMessage = rd.IFSLocationUpdate_ESN(i.Text, location.AssignThisLocationText(), IP);
        //            if (rMessage.Substring(0, 6) != "Error:") { count++; }
        //            if (rMessage.Substring(0, 6) == "Error:") { Errorcount++; ErrorMessage += (ErrorMessage.Length > 0 ? "/" : "") + rMessage; }
        //        }

        //        Clear();
        //        string FullMessage = count.ToString() + " Devices Moved";
        //        if (Errorcount > 0) { FullMessage += " - " + Errorcount.ToString() + " Devices Not Moved"; }
        //        if (Errorcount > 0) { FullMessage += " - " + ErrorMessage; }
        //        lblMessage.Text = FullMessage;
        //    }

        //    #endregion
        //}
        //void btnGOReasonCodes_Click(object sender, EventArgs e)
        //{
        //    if (lstHistory.Items.Count < 1)
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "alert('Error, no IMEI data given!');", true);
        //        lblMessage.Text = "No IMEI Data given!";
        //        return;
        //    }
        //    ReceiveDetailManager rd = new ReceiveDetailManager(User.Identity.Name);
        //    if (lstHistory.Items.Count > 0)
        //    {
        //        string ReasonCode = drpIFSReasonCodes.SelectedItem.Value;
        //        string ErrorMessage = "";
        //        int count = 0;
        //        int Errorcount = 0;
        //        //string rMessage = "";
        //        bool rValue = false;
        //        foreach (ListItem i in lstHistory.Items)
        //        {
        //            rValue = rd.IFSMOVEShipped(i.Text, ReasonCode);
        //            if (rValue == true) { count++; }
        //            if (rValue == false) { Errorcount++; ErrorMessage += (ErrorMessage.Length > 0 ? "/" : "") + i.Text + " Not found"; }
        //        }
        //        Clear();
        //        string FullMessage = count.ToString() + " Devices User Disposed";
        //        if (Errorcount > 0) { FullMessage += " - " + Errorcount.ToString() + " Devices Not User Disposed"; }
        //        if (Errorcount > 0) { FullMessage += " - " + ErrorMessage; }
        //        lblMessage.Text = FullMessage;
        //    }
        //}

        //void btnReactivate_Click(object sender, EventArgs e)
        //{
        //    string Location = "";
        //    Location = rdReactivateToLocation.SelectedItem.Value;
        //    if (lstHistory.Items.Count < 1)
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "alert('Error, no IMEI data given!');", true);
        //        lblMessage.Text = "No IMEI Data given!";
        //        return;
        //    }
        //    if (Location.Length == 0)
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "alert('No to location given!');", true);
        //        lblMessage.Text = "No to location given!";
        //        return;
        //    }
        //    IFS_InventoryManager im = new IFS_InventoryManager(User.Identity.Name);
        //    if (im.IsLocationValid(Location) == false)
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "alert('Invalid TO location given!');", true);
        //        lblMessage.Text = "Invalid TO location given!";
        //        return;
        //    }
        //    ReceiveDetailManager rd = new ReceiveDetailManager(User.Identity.Name);
        //    if (lstHistory.Items.Count > 0)
        //    {
        //        string ErrorMessage = "";
        //        string NewLocation = Location;
        //        int count = 0;
        //        int Errorcount = 0;
        //        //string rMessage = "";
        //        bool rValue = false;
        //        foreach (ListItem i in lstHistory.Items)
        //        {
        //            rValue = rd.VersionBackFrom001(i.Text, NewLocation);
        //            if (rValue == true) { count++; }
        //            if (rValue == false) { Errorcount++; ErrorMessage += (ErrorMessage.Length > 0 ? "/" : "") + i.Text + " Not found"; }
        //        }
        //        Clear();
        //        string FullMessage = count.ToString() + " Devices returned from 001";
        //        if (Errorcount > 0) { FullMessage += " - " + Errorcount.ToString() + " Devices Not returned."; }
        //        if (Errorcount > 0) { FullMessage += " - " + ErrorMessage; }
        //        lblMessage.Text = FullMessage;
        //    }
        //}

        //void btnGraveYard_Click(object sender, EventArgs e)
        //{
        //    if (lstHistory.Items.Count < 1)
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "alert('Error, no IMEI data given!');", true);
        //        lblMessage.Text = "No IMEI Data given!";
        //        return;
        //    }
        //    ReceiveDetailManager rd = new ReceiveDetailManager(User.Identity.Name);
        //    if (lstHistory.Items.Count > 0)
        //    {
        //        string ErrorMessage = "";
        //        string ReasonCode = drpIFSReasonCodesGYard.SelectedItem.Value;
        //        int count = 0;
        //        int Errorcount = 0;
        //        //string rMessage = "";
        //        bool rValue = false;
        //        foreach (ListItem i in lstHistory.Items)
        //        {
        //            rValue = rd.MoveToGraveYard(i.Text, ReasonCode);
        //            if (rValue == true) { count++; }
        //            if (rValue == false)
        //            {
        //                string thereason = rd.OKToGraveYardReason(i.Text);
        //                Errorcount++; ErrorMessage += (ErrorMessage.Length > 0 ? "/" : "") + i.Text + thereason;
        //            }
        //        }
        //        Clear();
        //        string FullMessage = count.ToString() + " Devices grave yarded";
        //        if (Errorcount > 0) { FullMessage += " - " + Errorcount.ToString() + " Devices Not grave yarded."; }
        //        if (Errorcount > 0) { FullMessage += " - " + ErrorMessage; }
        //        lblMessage.Text = FullMessage;
        //    }
        //}
        //void btnPrintKittingLabels_Click(object sender, EventArgs e)
        //{
        //    if (lstHistory.Items.Count < 1)
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "alert('Error, no IMEI data given!');", true);
        //        lblMessage.Text = "No IMEI Data given!";
        //        return;
        //    }
        //    Hobble hobble = new Hobble(User.Identity.Name);
        //    if (lstHistory.Items.Count > 0)
        //    {
        //        string ErrorMessage = "";
        //        short Seq = 0;
        //        int count = 0;
        //        int Errorcount = 0;
        //        //string rMessage = "";
        //        bool rValue = false;
        //        foreach (ListItem i in lstHistory.Items.Cast<ListItem>().OrderBy(item => item.Text))
        //        {
        //            Seq++;
        //            rValue = hobble.LoadHobbleList(i.Text, "KL", "", Seq, User.Identity.Name);
        //            if (rValue == true) { count++; }
        //            if (rValue == false) { Errorcount++; ErrorMessage += (ErrorMessage.Length > 0 ? "/" : "") + i.Text + " Not found"; }
        //        }
        //        string FullMessage = count.ToString() + " Devices sent to Label";
        //        if (Errorcount > 0) { FullMessage += " - " + Errorcount.ToString() + " Devices Not sent to Label"; }
        //        if (Errorcount > 0) { FullMessage += " - " + ErrorMessage; }
        //        lblMessage.Text = FullMessage;
        //        if (count > 0)
        //        {
        //            // print the labels.
        //            ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "OpenFinishProductLabel();", true);
        //        }
        //    }
        //}



        private string GetUserIPAddress()
        {
            string ip = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(ip))
            {
                ip = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            return ip;
        }


    }
}