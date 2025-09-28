using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
//using Factory_DataModel;
using BW_WebApp.DataManagers;

namespace BW_WebApp.IFSLocationManagement
{
    public partial class IFSDeviceUtility : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {


            //btnToSKUPrior.Click += new EventHandler(btnToSKUPrior_Click);
            //btnToSKUNext.Click += new EventHandler(btnToSKUNext_Click);
            //btnSKUClear.Click += new EventHandler(btnSKUClear_Click);
            btrRecord.Click += new EventHandler(btrRecord_Click);
            btnRecordBin.Click += new EventHandler(btnRecordBin_Click);
            btrRecordLocation.Click += new EventHandler(btrRecordLocation_Click);
            btnRecordSite.Click += new EventHandler(btnRecordSite_Click);
            btnPasteParse.Click += new EventHandler(btnPasteParse_Click);
            imgbtnClear.Click += new ImageClickEventHandler(imgbtnClear_Click);
            imgbtnDeleteIMIE.Click += new ImageClickEventHandler(imgbtnDeleteIMIE_Click);
            //-----------------------------------------------------------------------
            btnIFSLock.Click += new EventHandler(btnIFSLock_Click);
            //btnToSKU.Click += new EventHandler(btnToSKU_Click);
            //btnTOCondition.Click += new EventHandler(btnTOCondition_Click);

            //btnSearchRefresh.Click += new EventHandler(btnSearchRefresh_Click);

            //grdSearchSuggest.RowDataBound += new GridViewRowEventHandler(grdSearchSuggest_RowDataBound);
            //grdSearchSuggest.RowCommand += new GridViewCommandEventHandler(grdSearchSuggest_RowCommand);
            //drpCarrier.SelectedIndexChanged += new EventHandler(drpCarrier_SelectedIndexChanged);
            //drpManufacturer.SelectedIndexChanged += new EventHandler(drpManufacturer_SelectedIndexChanged);
            //drpModel.SelectedIndexChanged += new EventHandler(drpModel_SelectedIndexChanged);


            if (!IsPostBack)
            {
                //hdnCarrierID.Value = drpCarrier.ClientID;
                //hdnManufacturerID.Value = drpManufacturer.ClientID;
                //hdnModelID.Value = drpModel.ClientID;
                //hdnColourID.Value = drpColour.ClientID;
                hdnUserName.Value = User.Identity.Name;

                TabDIDMToLoc.Visible = (User.IsInRole("DIDMToLoc") == true || User.IsInRole("Admin") == true) ? true : false;
                //TabDIDMToSku.Visible = (User.IsInRole("DIDMToSku") == true || User.IsInRole("Admin") == true) ? true : false;
                //TabDIDMToCond.Visible = (User.IsInRole("DIDMToCond") == true || User.IsInRole("Admin") == true) ? true : false;
                TabDIDMFromESN.Visible = (User.IsInRole("DIDMFromESN") == true || User.IsInRole("Admin") == true) ? true : false;
                TabDIDMFromBin.Visible = (User.IsInRole("DIDMFromBin") == true || User.IsInRole("Admin") == true) ? true : false;
                TabDIDMFromLocation.Visible = (User.IsInRole("DIDMFromLocation") == true || User.IsInRole("Admin") == true) ? true : false;
                TabDIDMFromSite.Visible = (User.IsInRole("DIDMFromSite") == true || User.IsInRole("Admin") == true) ? true : false;
                TabDIDMFromPaste.Visible = (User.IsInRole("DIDMFromPaste") == true || User.IsInRole("Admin") == true) ? true : false;


                TabDIDMToLoc.Visible = true;
                //TabDIDMToSku.Visible = true;
                //TabDIDMToCond.Visible = true;
                TabDIDMFromESN.Visible = true;
                TabDIDMFromBin.Visible = true;
                TabDIDMFromLocation.Visible = (User.IsInRole("DIDMFromLocation") == true || User.IsInRole("Admin") == true) ? true : false;
                TabDIDMFromSite.Visible = (User.IsInRole("DIDMFromSite") == true || User.IsInRole("Admin") == true) ? true : false;
                TabDIDMFromPaste.Visible = true;

                //setupDropDowns();
            }


        }

        //void btnSKUClear_Click(object sender, EventArgs e)
        //{
        //    ClearSKUDropDowns();
        //}




        void SetMessage(string Message)
        {
            lblMessage.Text = Message;          //Top of screen
            lblMessageBTM.Text = Message;       //Bottom of screen
        }



        //#region Grid Version
        ////void grdVersion_RowCommand(object sender, GridViewCommandEventArgs e)
        ////{
        ////    System.Web.UI.WebControls.ImageButton btnAdd = (System.Web.UI.WebControls.ImageButton)e.CommandSource;
        ////    ReceiveDetailManager rdm = null;
        ////    decimal id = -1;
        ////    if (decimal.TryParse(btnAdd.CommandArgument, out id) == false) { id = -1; }
        ////    switch (btnAdd.CommandName.ToString().ToUpper())
        ////    {
        ////        //case "SETVERSIONTOZERO":
        ////        //    rdm = new ReceiveDetailManager(User.Identity.Name);
        ////        //    rdm.AdvanceESNVersion_ToZero(ctx, id);
        ////        //    RefreshVersion();
        ////        //    ScriptManager.RegisterStartupScript(this, GetType(), "RestHeader", "RestoreHeaderData(); SetUpScreen('');", true);
        ////        //    break;
        ////        //case "ADVANCEVERSIONNUMBERS":
        ////        //    rdm = new ReceiveDetailManager(User.Identity.Name);
        ////        //    rdm.AdvanceESNVersion_FromThisOne(ctx, id);
        ////        //    RefreshVersion();
        ////        //    ScriptManager.RegisterStartupScript(this, GetType(), "RestHeader", "RestoreHeaderData(); SetUpScreen('');", true);
        ////        //    break;
        ////        //case "DELETERECEIVEDETAILPROCESSLOG":
        ////        //    rdm = new ReceiveDetailManager(User.Identity.Name);
        ////        //    //rdm.DeleteReceiveDetailProcessLogThisID(id);
        ////        //    RefreshVersion();
        ////        //    ScriptManager.RegisterStartupScript(this, GetType(), "RestHeader", "RestoreHeaderData(); SetUpScreen('');", true);
        ////        //    break;
        ////        default:
        ////            break;
        ////    }
        ////}
        ////void grdVersion_RowDataBound(object sender, GridViewRowEventArgs e)
        ////{
        ////    if (e.Row.RowType == DataControlRowType.DataRow)
        ////    {
        ////        System.Web.UI.WebControls.CheckBox chkPicked = (System.Web.UI.WebControls.CheckBox)e.Row.FindControl("chkVersionGo");
        ////        if (chkPicked != null)
        ////        {
        ////            chkPicked.Enabled = true;
        ////            if (((pxlist)e.Row.DataItem).Version == "000")
        ////            {
        ////                chkPicked.Enabled = false;
        ////                chkPicked.Checked = false;
        ////                if (User.IsInRole("VER2000"))
        ////                {
        ////                    chkPicked.Enabled = true;
        ////                }
        ////            }
        ////        }
        ////    }
        ////}




        ////private void RefreshSKUData()
        ////{
        ////    using (clsLinqDataContext ctx = new clsLinqDataContext())
        ////    {
        ////        string ESN = hdnCurrentIMEI.Value;
        ////        //grdVersion.DataSource = null;
        ////        ReceiveDetail rd = ctx.ReceiveDetails.OrderByDescending(x => x.ReceiveDetailID).FirstOrDefault(x => x.ESN == ESN && x.Version == "000");
        ////        if (rd != null)
        ////        {
        ////            ListItem carrier = drpCarrier.Items.FindByValue(rd.CarrierID.ToString());
        ////            if (carrier != null) { drpCarrier.SelectedIndex = drpCarrier.Items.IndexOf(carrier); }
        ////            FillMMCCDropDowns("Carrier");
        ////            ListItem manufacturer = drpManufacturer.Items.FindByValue(rd.ManufacturerID.ToString());
        ////            if (manufacturer != null) { drpManufacturer.SelectedIndex = drpManufacturer.Items.IndexOf(manufacturer); }
        ////            FillMMCCDropDowns("Manufacturer");
        ////            ListItem model = drpModel.Items.FindByValue(rd.ModelID.ToString());
        ////            if (model != null) { drpModel.SelectedIndex = drpModel.Items.IndexOf(model); }
        ////            FillMMCCDropDowns("Model");
        ////            ListItem colour = drpColour.Items.FindByValue(rd.ColourID.ToString());
        ////            if (colour != null) { drpColour.SelectedIndex = drpColour.Items.IndexOf(colour); }
        ////        }
        ////    }
        ////}
        ////private void RefreshVersion()
        ////{
        ////    using (clsLinqDataContext ctx = new clsLinqDataContext())
        ////    {
        ////        string ESN = lblCurrentIMEI.Text;
        ////        grdVersion.DataSource = null;
        ////        ReceiveDetail rd = ctx.ReceiveDetails.OrderByDescending(x => x.ReceiveDetailID).FirstOrDefault(x => x.ESN == ESN);
        ////        if (rd != null)
        ////        {
        ////            decimal RDID = rd.ReceiveDetailID;
        ////            //string sRDID = hdnReceiveDetailID.Value;
        ////            //if (decimal.TryParse(sRDID, out RDID) != true) { RDID = -1; }
        ////            //if (RDID < 1) { return; }
        ////            ReceiveDetailManager rdm = new ReceiveDetailManager(User.Identity.Name);
        ////            //List<ReceiveDetail> blist = null;
        ////            var blist = from x in rdm.GetReceiveDetailVersionHistory(ctx, RDID)
        ////                        let PName = rdm.ProjectName(x.ProjectID)
        ////                        let SName = rdm.StatusName(x.StatusID)
        ////                        orderby x.Version
        ////                        select new pxlist
        ////                        {
        ////                            ReceiveDetailID = x.ReceiveDetailID,
        ////                            ProjectID = decimal.Parse(x.ProjectID.ToString()),
        ////                            StatusID = x.StatusID,
        ////                            Version = x.Version,
        ////                            ESN = x.ESN,
        ////                            CreateDate = x.CreateDate,
        ////                            ProjectName = PName.ToString(),
        ////                            CreateUser = x.CreateUser,
        ////                            StatusName = SName.ToString(),
        ////                            IFSSite = "NYI",
        ////                            IFSProject = "NYI",
        ////                            IFSLocation = x.IFSLocation == null ? "" : x.IFSLocation,
        ////                            IFSCondition = x.IFSCondition == null ? "" : x.IFSCondition,
        ////                            SKU = x.SKU == null ? "" : x.SKU,
        ////                            isIFSLocked = x.isIFSLocked == null ? false : (bool)x.isIFSLocked,
        ////                        };

        ////            //string[] DataKeys = new string[] { "ReceiveDetailID"};
        ////            //grdVersion.DataKeyNames = DataKeys;
        ////            lblVersionMessage.Text = "";
        ////            if (blist.Count() == 0)
        ////            {
        ////                lblVersionMessage.Text = "No Records Found for this IMEI(" + ESN + ")";
        ////            }
        ////            grdVersion.DataSource = blist;
        ////        }
        ////        grdVersion.DataBind();
        ////    }

        ////}
        ////void btnVersionPrior_Click(object sender, EventArgs e)
        ////{
        ////    ListItem Current = lstHistory.Items.FindByText(lblCurrentIMEI.Text);
        ////    lblCurrentIMEI.Text = "";
        ////    if (Current == null)
        ////    {
        ////        if (lstHistory.Items.Count < 1) { lblCurrentIMEI.Text = ""; }
        ////        lblCurrentIMEI.Text = lstHistory.Items[0].Text;
        ////    }
        ////    if (Current != null)
        ////    {
        ////        if (lstHistory.Items.IndexOf(Current) != 0)
        ////        {
        ////            lblCurrentIMEI.Text = lstHistory.Items[lstHistory.Items.IndexOf(Current) - 1].Text;
        ////        }
        ////    }
        ////    // Load the grid.
        ////    RefreshVersion();


        ////    //lblCurrentIMEI
        ////}
        ////void btnVersionNext_Click(object sender, EventArgs e)
        ////{
        ////    ListItem Current = lstHistory.Items.FindByText(lblCurrentIMEI.Text);
        ////    lblCurrentIMEI.Text = "";
        ////    if (Current == null)
        ////    {
        ////        if (lstHistory.Items.Count < 1) { lblCurrentIMEI.Text = ""; }
        ////        lblCurrentIMEI.Text = lstHistory.Items[0].Text;
        ////    }
        ////    if (Current != null)
        ////    {
        ////        int tCount = lstHistory.Items.Count;
        ////        int mcount = lstHistory.Items.IndexOf(Current);
        ////        if (tCount > mcount + 1)
        ////        {
        ////            lblCurrentIMEI.Text = lstHistory.Items[mcount + 1].Text;
        ////        }
        ////    }
        ////    // Load the grid.
        ////    RefreshVersion();


        ////    //lblCurrentIMEI
        ////}



        ////void btnToSKUPrior_Click(object sender, EventArgs e)
        ////{
        ////    ListItem Current = lstHistory.Items.FindByText(hdnCurrentIMEI.Value);
        ////    hdnCurrentIMEI.Value = "";
        ////    if (Current == null)
        ////    {
        ////        if (lstHistory.Items.Count < 1) { hdnCurrentIMEI.Value = ""; }
        ////        hdnCurrentIMEI.Value = lstHistory.Items[0].Text;
        ////    }
        ////    if (Current != null)
        ////    {
        ////        if (lstHistory.Items.IndexOf(Current) != 0)
        ////        {
        ////            hdnCurrentIMEI.Value = lstHistory.Items[lstHistory.Items.IndexOf(Current) - 1].Text;
        ////        }
        ////    }
        ////    // Load the grid.
        ////    RefreshSKUData();


        ////    //lblCurrentIMEI
        ////}
        ////void btnToSKUNext_Click(object sender, EventArgs e)
        ////{
        ////    ListItem Current = lstHistory.Items.FindByText(hdnCurrentIMEI.Value);
        ////    hdnCurrentIMEI.Value = "";
        ////    if (Current == null)
        ////    {
        ////        if (lstHistory.Items.Count < 1) { hdnCurrentIMEI.Value = ""; }
        ////        hdnCurrentIMEI.Value = lstHistory.Items[0].Text;
        ////    }
        ////    if (Current != null)
        ////    {
        ////        int tCount = lstHistory.Items.Count;
        ////        int mcount = lstHistory.Items.IndexOf(Current);
        ////        if (tCount > mcount + 1)
        ////        {
        ////            hdnCurrentIMEI.Value = lstHistory.Items[mcount + 1].Text;
        ////        }
        ////    }
        ////    if (drpCarrier.Items.Count == 0)
        ////    {
        ////        FillMMCCDropDowns("Carrier");
        ////    }
        ////    // Load the grid.
        ////    RefreshSKUData();
        ////    //lblCurrentIMEI
        ////}



        //#endregion

        //void ClearSKUDropDowns()
        //{
        //    drpManufacturer.Items.Clear();
        //    drpModel.Items.Clear();
        //    drpColour.Items.Clear();
        //}


        #region  xxxx
        //void drpCarrier_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    FillMMCCDropDowns("Carrier");
        //    drpManufacturer.Focus();
        //}
        //void drpManufacturer_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    FillMMCCDropDowns("Manufacturer");
        //    drpModel.Focus();
        //}
        //void drpModel_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    FillMMCCDropDowns("Model");
        //    drpColour.Focus();
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
        void imgbtnDeleteIMIE_Click(object sender, ImageClickEventArgs e)
        {
            List<ListItem> itms = new List<ListItem>();
            foreach (ListItem i in lstHistory.Items)
            {
                if (i.Selected == true)
                {
                    itms.Add(i);
                }
            }
            foreach (ListItem i in itms)
            {
                lstHistory.Items.Remove(i);
            }
        }
        void imgbtnClear_Click(object sender, ImageClickEventArgs e)
        {
            Clear();
        }
        private void Clear()
        {
            //IFS_InventoryManager m = new IFS_InventoryManager(User.Identity.Name);
            List<GridIFSLocationSuggestion> blank = new List<GridIFSLocationSuggestion>();
            SetMessage("");
            //grdSearchSuggest.DataSource = blank;
            //grdSearchSuggest.DataBind();
            lstHistory.Items.Clear();
            txtCount.Text = lstHistory.Items.Count.ToString();

            txtESN.Text = "";
            lblBinNumber.Text = "";
            IFSMoveFromLocationOnly.Text = "";
            txtFromSku.Text = "";
            txtFromLocation.Text = "";

            txtESN.Focus();
        }

        void btrRecord_Click(object sender, EventArgs e)
        {
            if (txtESN.Text.Length > 0) { RecordESN(); }
            else if (lblBinNumber.Text.Length > 0) { RecordBin(); }
            else if (IFSMoveFromLocationOnly.Text.Length > 0) { RecordLocation(); }
            else if (txtFromLocation.Text.Length > 0 && txtFromSku.Text.Length > 0) { RecordSite(); }

        }
        void btnRecordSite_Click(object sender, EventArgs e)
        {
            RecordSite();
        }
        void btrRecordLocation_Click(object sender, EventArgs e)
        {
            RecordLocation();
        }
        void btnRecordBin_Click(object sender, EventArgs e)
        {
            RecordBin();
        }
        void btnPasteParse_Click(object sender, EventArgs e)
        {
            RecordPasteParse();
        }

        private void RecordESN()
        {
            if (txtESN.Text.Length > 0 && lstHistory.Items.FindByText(txtESN.Text) == null)
            {
                lstHistory.Items.Add(new ListItem(txtESN.Text));
            }
            txtCount.Text = lstHistory.Items.Count.ToString();
            txtESN.Text = "";
            txtESN.Focus();
        }
        private void RecordSite()
        {
            if (txtFromLocation.Text.Length > 0 && txtFromSku.Text.Length > 0)
            {
                ReceiveDetailManager rd = new ReceiveDetailManager(User.Identity.Name);
                List<string> dta = rd.getIMEIsInLocation_FromSitProjectSkuLocationCondition(drpMoveIFSSite.SelectedItem.Text
                                                                                          , drpMoveIFSProject.SelectedItem.Value
                                                                                          , txtFromSku.Text
                                                                                          , txtFromLocation.Text
                                                                                          , drpMoveIFSCondition.SelectedItem.Text
                                                                                          , "");
                foreach (string x in dta) { if (x.Length > 0 && lstHistory.Items.FindByText(x) == null) { lstHistory.Items.Add(new ListItem(x)); } }
            }
            txtCount.Text = lstHistory.Items.Count.ToString();
            txtFromSku.Text = "";
            txtFromLocation.Text = "";
            txtFromSku.Focus();
        }
        private void RecordLocation()
        {
            if (IFSMoveFromLocationOnly.Text.Length > 0)
            {
                ReceiveDetailManager rd = new ReceiveDetailManager(User.Identity.Name);
                List<string> dta = rd.getIMEIsInLocation(IFSMoveFromLocationOnly.Text);
                foreach (string x in dta) { if (x.Length > 0 && lstHistory.Items.FindByText(x) == null) { lstHistory.Items.Add(new ListItem(x)); } }
            }
            txtCount.Text = lstHistory.Items.Count.ToString();
            IFSMoveFromLocationOnly.Text = "";
            IFSMoveFromLocationOnly.Focus();
        }
        private void RecordBin()
        {
            if (lblBinNumber.Text.Length > 0)
            {
                ReceiveDetailManager rd = new ReceiveDetailManager(User.Identity.Name);
                List<string> dta = rd.getIMEIsInBin(lblBinNumber.Text);
                foreach (string x in dta) { if (x.Length > 0 && lstHistory.Items.FindByText(x) == null) { lstHistory.Items.Add(new ListItem(x)); } }
            }
            txtCount.Text = lstHistory.Items.Count.ToString();
            lblBinNumber.Text = "";
            lblBinNumber.Focus();
        }
        private void RecordPasteParse()
        {
            //char Delimitor = ',';
            //if (PasteDeliminator.SelectedItem.Value.ToUpper() == "SPACE") { Delimitor = ' '; }
            if (txtPasteParse.Text.Length > 0)
            {
                if (PasteDeliminator.SelectedItem.Value.ToUpper() == "EXCEL")
                {
                    //List<string> data = txtPasteParse.Text.Split(new string[] { "\r\n", "\n", " " }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    List<string> data = txtPasteParse.Text.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    foreach (string x in data) { if (x.Length > 0 && lstHistory.Items.FindByText(x) == null) { lstHistory.Items.Add(new ListItem(x)); } }
                }
                if (PasteDeliminator.SelectedItem.Value.ToUpper() == "SPACE")
                {
                    List<string> data = txtPasteParse.Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    foreach (string x in data) { if (x.Length > 0 && lstHistory.Items.FindByText(x) == null) { lstHistory.Items.Add(new ListItem(x)); } }
                }
                if (PasteDeliminator.SelectedItem.Value.ToUpper() == "COMMA")
                {
                    List<string> data = txtPasteParse.Text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    foreach (string x in data) { if (x.Length > 0 && lstHistory.Items.FindByText(x) == null) { lstHistory.Items.Add(new ListItem(x)); } }
                }
            }
            txtCount.Text = lstHistory.Items.Count.ToString();
            txtPasteParse.Text = "";
            txtPasteParse.Focus();
        }

        //void btnInvalidate_Click(object sender, EventArgs e)
        //{
        //    lblPIMessage.Text = "";

        //    if (txtBatch.Text.Length == 0)
        //    {
        //        lblPIMessage.Text = "No Batch number Given.";
        //        return;
        //    }
        //    IFS_InventoryManager im = new IFS_InventoryManager(User.Identity.Name);
        //    string rmessage = im.LogPhysicalInventoryBatchInvalid(txtBatch.Text);
        //    ClearPIData();
        //    lblPIMessage.Text = rmessage;
        //}

        //void btnGenerateIFS_Click(object sender, EventArgs e)
        //{
        //    lblPIMessage.Text = "";
        //    if (txtBatch.Text.Length == 0)
        //    {
        //        lblPIMessage.Text = "No Batch number Given.";
        //        return;
        //    }
        //    IFS_InventoryManager im = new IFS_InventoryManager(User.Identity.Name);
        //    lblPIMessage.Text = im.LogPhysicalInventoryBatchToIFS(txtBatch.Text);
        //}

        //void txtBatch_TextChanged(object sender, EventArgs e)
        //{
        //    lblPIMessage.Text = "";
        //    ScanKey.Enabled = true;
        //    if (txtBatch.Text.Length == 0) { return; }
        //    IFS_InventoryManager im = new IFS_InventoryManager(User.Identity.Name);
        //    if (im.IsPhysicalInventoryBatchLocked(txtBatch.Text) == true)
        //    {
        //        ScanKey.Enabled = false;
        //        lblPIMessage.Text = "This batch (" + txtBatch.Text + ") is locked!";
        //    }
        //}


        //#region Physical Inventory Count
        //void btnLockBatch_Click(object sender, EventArgs e)
        //{
        //    IFS_InventoryManager im = new IFS_InventoryManager(User.Identity.Name);
        //    lblPIMessage.Text = "";
        //    if (txtBatch.Text.Length == 0)
        //    {
        //        lblPIMessage.Text = "No Batch number Given.";
        //        //txtBatch.Text = im.GetNextPIBatchNumber();
        //        return;
        //    }
        //    string rmessage = im.LogPhysicalInventoryBatchLocked(txtBatch.Text);
        //    ClearPIData();
        //    //txtBatch.Text = im.GetNextPIBatchNumber();
        //    lblPIMessage.Text = rmessage;
        //}

        //void SetLastOpenBatch()
        //{
        //    ClearPIData();
        //    IFS_InventoryManager im = new IFS_InventoryManager(User.Identity.Name);
        //    hdnBatchNumber.Value = im.LastOpenBatchNumber;
        //    txtBatch.Text = hdnBatchNumber.Value;
        //    txtPMCount.Text = im.LastOpenBatchCount;
        //    txtPMCountError.Text = im.LastOpenBatchErrorCount;
        //}


        //void btnResetCount_Click(object sender, EventArgs e)
        //{
        //    ClearPIData();
        //}

        //void ClearPIData()
        //{
        //    txtPMCount.Text = "0";
        //    txtPMCountError.Text = "0";
        //    chkKitted.Checked = false;
        //    chkUnlocked.Checked = false;
        //    txtIFSLocation.Text = "";
        //    lblPIMessage.Text = "";
        //    txtBatch.Text = "";
        //    hdnBatchNumber.Value = "";
        //    drpIFSCondition.SelectedIndex = 0;
        //}



        //void chkAutoReturn_CheckedChanged(object sender, EventArgs e)
        //{
        //    btnPICRecord.Enabled = true;
        //    if (chkAutoReturn.Checked == true) { btnPICRecord.Enabled = false; }
        //}

        //void btnPICRecord_Click(object sender, EventArgs e)
        //{
        //    decimal MasterIFSLocationID = -1;
        //    decimal MasterIFSCondtionID = -1;
        //    string ESN = "";
        //    string Batch = "";
        //    string IFSSite = "";
        //    string IFSProject = "";
        //    string SKU = "";
        //    string IFSLocation = "";
        //    string IFSCondition = "";
        //    string Grade = "";
        //    //string POReceiptDate = "";
        //    bool LogOnly;
        //    string UserName;

        //    LogOnly = chkUpdateIMEI.Checked;
        //    //decimal.TryParse(drpIFSLocation.SelectedItem.Value, out MasterIFSLocationID);
        //    decimal.TryParse(drpIFSCondition.SelectedItem.Value, out MasterIFSCondtionID);
        //    ESN = ScanKey.Text;
        //    Batch = txtBatch.Text;
        //    IFSSite = drpIFSSite.SelectedItem.Text;
        //    IFSProject = drpIFSProject.SelectedItem.Value;
        //    Grade = "";             // drpGrade.SelectedItem.Text;
        //    //POReceiptDate = CalAnswer.Text;
        //    SKU = "";
        //    IFSLocation = txtIFSLocation.Text;
        //    IFSCondition = drpIFSCondition.SelectedItem.Text;
        //    UserName = User.Identity.Name;
        //    LogPhysicalDeviceCount(MasterIFSLocationID, MasterIFSCondtionID, ESN, Batch, IFSSite, IFSProject, SKU, IFSLocation, IFSCondition, Grade, LogOnly, UserName);
        //}

        //private void LogPhysicalDeviceCount(decimal MasterIFSLocationID, decimal MasterIFSCondtionID, string ESN, string Batch, string IFSSite, string IFSProject, string SKU, string IFSLocation, string IFSCondition, string Grade, bool LogOnly, string UserName)
        //{
        //    IFS_InventoryManager im = new IFS_InventoryManager(User.Identity.Name);
        //    im.LogPhysicalInventoryCount(MasterIFSLocationID, MasterIFSCondtionID, ESN, Batch, IFSSite, IFSProject, SKU, IFSLocation, IFSCondition, Grade,chkKitted.Checked,chkUnlocked.Checked, LogOnly, UserName);
        //}




        //#endregion

        //void setupDropDowns()
        //{
        //    IFS_InventoryPartsManager ipm = new IFS_InventoryPartsManager(User.Identity.Name);
        //    //List<MasterIFSLocation> loc = ipm.GetLocationList();
        //    //drpIFSLocation.Items.Clear();
        //    //foreach (MasterIFSLocation cl in loc)
        //    //{
        //    //    ListItem li = new ListItem(cl.IFSLocation, cl.MasterIFSLocationID.ToString());
        //    //    drpIFSLocation.Items.Add(li);
        //    //}


        //    QuestionManager qm = new QuestionManager(User.Identity.Name);
        //    List<Option> ol = qm.GetQuestionOptionList("IFS Conditions");
        //    //drpIFSCondition.Items.Clear();
        //    drpMoveIFSCondition.Items.Clear();
        //    drpChangeToCondition.Items.Clear();
        //    foreach (Option o in ol.OrderBy(x=> x.Sequence))
        //    {
        //        //ListItem x = new ListItem(o.OptionText, o.OptionID.ToString());
        //        ListItem y = new ListItem(o.OptionText, o.Name);
        //        ListItem z = new ListItem(o.OptionText, o.Name);
        //        ListItem zz = new ListItem(o.OptionText, o.Name);
        //        //drpIFSCondition.Items.Add(x);
        //        drpMoveIFSCondition.Items.Add(y);
        //        drpChangeToCondition.Items.Add(z);
        //    }

        //    ol = qm.GetQuestionOptionList("Carrier");
        //    drpCarrier.Items.Clear();
        //    foreach (Option o in ol.OrderBy(x => x.Sequence))
        //    {
        //        ListItem x = new ListItem(o.OptionText, o.OptionID.ToString());
        //        drpCarrier.Items.Add(x);
        //    }


        //    decimal Count = 0;
        //    List<string> s = ipm.GetSiteList();
        //    //drpIFSSite.Items.Clear();
        //    drpMoveIFSSite.Items.Clear();
        //    foreach (string i in s)
        //    {
        //        Count++;
        //        //ListItem x = new ListItem(i, Count.ToString());
        //        ListItem y = new ListItem(i, Count.ToString());
        //        //if (x.Text == "C1NA") { x.Selected = true; }
        //        //drpIFSSite.Items.Add(x);
        //        drpMoveIFSSite.Items.Add(y);
        //    }


        //    Count = 0;
        //    List<PairStringValue> p = ipm.GetProjectList();
        //    //drpIFSProject.Items.Clear();
        //    drpMoveIFSProject.Items.Clear();
        //    foreach (PairStringValue i in p)
        //    {
        //        Count++;
        //        //ListItem x = new ListItem(i.Key + " - " + i.Value, i.Key);
        //        ListItem y = new ListItem(i.Key + " - " + i.Value, i.Key);
        //        //if (x.Text == "BRCE") { x.Selected = true; }
        //        //drpIFSProject.Items.Add(x);
        //        drpMoveIFSProject.Items.Add(y);
        //    }


        //    // FillMMCCDropDowns("Carrier");


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
        //    btnToSKU.Enabled = false;
        //    if (DropDownName == "Carrier")
        //    {
        //        CarrierKey = "-1";
        //        if (drpCarrier.SelectedItem != null) { CarrierKey = drpCarrier.SelectedItem.Value; }
        //        if (decimal.TryParse(CarrierKey, out CarrierID) == true) { FillManufacturer(CarrierID); }

        //    }
        //    if (DropDownName == "Manufacturer")
        //    {
        //        CarrierKey = "-1";
        //        ManufacturerKey = "-1";
        //        if (drpCarrier.SelectedItem != null) { CarrierKey = drpCarrier.SelectedItem.Value; }
        //        if (drpManufacturer.SelectedItem != null) { ManufacturerKey = drpManufacturer.SelectedItem.Value; }

        //        if (decimal.TryParse(CarrierKey, out CarrierID) == true
        //            && decimal.TryParse(ManufacturerKey, out ManufacturerID) == true)
        //        { FillModel(CarrierID, ManufacturerID); }

        //    }

        //    if (DropDownName == "Model")
        //    {
        //        CarrierKey = "-1";
        //        ManufacturerKey = "-1";
        //        ModelKey = "-1";

        //        if (drpCarrier.SelectedItem != null) { CarrierKey = drpCarrier.SelectedItem.Value; }
        //        if (drpManufacturer.SelectedItem != null) { ManufacturerKey = drpManufacturer.SelectedItem.Value; }
        //        if (drpModel.SelectedItem != null) { ModelKey = drpModel.SelectedItem.Value; }

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
        #endregion


        #region UtilityExecute
        //void btnBringBackNotation_Click(object sender, EventArgs e)
        //{
        //    List<string> RDIDs = new List<string>();
        //    List<string> Versions = new List<string>();
        //    foreach (GridViewRow line in grdVersion.Rows)
        //    {
        //        CheckBox selected = (CheckBox)line.FindControl("chkVersionGo");
        //        if (selected != null)
        //        {
        //            if (selected.Checked == true)
        //            {
        //                RDIDs.Add(line.Cells[13].Text);
        //                Versions.Add(line.Cells[2].Text);
        //            }
        //        }
        //    }
        //    if (RDIDs.Count() != 1)
        //    {
        //        SetMessage("You can only select 1 record.");
        //        return;
        //    }
        //    decimal ID = -1;
        //    bool rValue = false;
        //    ReceiveDetailManager rdm = new ReceiveDetailManager(User.Identity.Name);
        //    if (decimal.TryParse(RDIDs[0], out ID) == true)
        //    {
        //        ReceiveDetail rd = rdm.ReceiveDetail(ID);
        //        if (rd == null)
        //        {
        //            SetMessage("Record not found.");
        //            return;
        //        }
        //        //if (rd.Version == "000")
        //        //{
        //        //    lblVersionMessage.Text = "You can not change a 000 record here. It must be a switch or reactivate.";
        //        //    return;
        //        //}
        //        ClientLocation cl = rdm.GetClientLocation(rd.ClientLocationID);
        //        string NewLocation = "RC1-001-001-001";
        //        if (cl != null && cl.IFSSite == "C1NA")
        //        {
        //            NewLocation = "RC2-001-001-001";
        //        }
        //        rValue = rdm.VersionBackForNotation(ID, NewLocation, txtNotation.Text);
        //        //rdm.SetVersion(ID, txtToVersion.Text);
        //    }
        //    ReceiveDetailUtilityMoveLogManager log = new ReceiveDetailUtilityMoveLogManager("Open for Notration", User.Identity.Name);
        //    if (rValue == true)
        //    {
        //        SetMessage("Version Set for NOTATION - abt 30min.");
        //        log.Save(ID, "Set for Notation:" + txtNotation.Text);
        //    }
        //    else
        //    {
        //        SetMessage("Error, unable to Version for Notation.");
        //        if (ID > 0)
        //        {
        //            log.Save(ID, "Unable to Set for Notation:" + txtNotation.Text);
        //        }
        //    }
        //    txtToVersion.Text = "";
        //    RefreshVersion();
        //}
        //void btnVersionGo_Click(object sender, EventArgs e)
        //{
        //    List<string> RDIDs = new List<string>();
        //    List<string> Versions = new List<string>();
        //    foreach (GridViewRow line in grdVersion.Rows)
        //    {
        //        CheckBox selected = (CheckBox)line.FindControl("chkVersionGo");
        //        if (selected != null)
        //        {
        //            if (selected.Checked == true)
        //            {
        //                RDIDs.Add(line.Cells[13].Text);
        //                Versions.Add(line.Cells[2].Text);
        //            }
        //        }
        //    }
        //    if (RDIDs.Count() != 2)
        //    {
        //        SetMessage("You can only select 2 records.");
        //        return;
        //    }
        //    decimal ID_01 = -1;
        //    decimal ID_02 = -1;
        //    ReceiveDetailManager rdm = new ReceiveDetailManager(User.Identity.Name);
        //    if (decimal.TryParse(RDIDs[0], out ID_01) == false) { return; }
        //    if (decimal.TryParse(RDIDs[1], out ID_02) == false) { return; }
        //    rdm.AdvanceESNVersion_SetFlip(ID_01, ID_02);
        //    SetMessage("Versions Switched.");
        //    ReceiveDetailUtilityMoveLogManager log = new ReceiveDetailUtilityMoveLogManager("Switch Version", User.Identity.Name);
        //    log.Save(ID_01, ID_02, "Source:Versions Switched.");
        //    log.Save(ID_02, ID_01, "Target:Versions Switched.");
        //    RefreshVersion();
        //}
        //void btnChangeIMEI_Click(object sender, EventArgs e)
        //{
        //    List<string> RDIDs = new List<string>();
        //    List<string> Versions = new List<string>();
        //    foreach (GridViewRow line in grdVersion.Rows)
        //    {
        //        CheckBox selected = (CheckBox)line.FindControl("chkVersionGo");
        //        if (selected != null)
        //        {
        //            if (selected.Checked == true)
        //            {
        //                RDIDs.Add(line.Cells[13].Text);
        //                Versions.Add(line.Cells[2].Text);
        //            }
        //        }
        //    }
        //    if (RDIDs.Count() != 1)
        //    {
        //        SetMessage("You can only select 1 record.");
        //        return;
        //    }
        //    decimal ID = -1;
        //    ReceiveDetailManager rdm = new ReceiveDetailManager(User.Identity.Name);
        //    if (decimal.TryParse(RDIDs[0], out ID) == true)
        //    {
        //        ReceiveDetail rd = rdm.ReceiveDetail(ID);
        //        if (rd == null)
        //        {
        //            SetMessage("Record not found.");
        //            return;
        //        }
        //        ReceiveDetailUtilityMoveLogManager log = new ReceiveDetailUtilityMoveLogManager("Change IMEI", User.Identity.Name);
        //        string rMessage = rdm.UtilityChangeIMEI(rd.ReceiveDetailID, txtNewIMEI.Text.RemoveCRLFTab());
        //        SetMessage(rMessage);
        //        log.Save(rd.ReceiveDetailID, rMessage);
        //    }
        //    RefreshVersion();
        //}
        //void btnAdjustMSCIn_Click(object sender, EventArgs e)
        //{

        //    lblVersionMessage.Text = "xxxxxx";
        //    List<string> RDIDs = new List<string>();
        //    List<string> Versions = new List<string>();
        //    foreach (GridViewRow line in grdVersion.Rows)
        //    {
        //        CheckBox selected = (CheckBox)line.FindControl("chkVersionGo");
        //        if (selected != null)
        //        {
        //            if (selected.Checked == true)
        //            {
        //                RDIDs.Add(line.Cells[13].Text);
        //                Versions.Add(line.Cells[2].Text);
        //            }
        //        }
        //    }
        //    if (RDIDs.Count() != 2)
        //    {
        //        SetMessage("You can select 2 records. The MSC version and the original version you want brought back to 000.");
        //        return;
        //    }
        //    decimal ID = -1;
        //    decimal ID2 = -1;
        //    lblVersionMessage.Text = "Error:";
        //    ReceiveDetailManager rdm = new ReceiveDetailManager(User.Identity.Name);
        //    if (decimal.TryParse(RDIDs[0], out ID) == true && decimal.TryParse(RDIDs[1], out ID2) == true)
        //    {
        //        string V1 = Versions[0];
        //        string V2 = Versions[1];
        //        //int Value = 0;
        //        //Value = V1.CompareTo(V2);
        //        //lblVersionMessage.Text = V1 + ":" + V2 + ":" + Value.ToString();
        //        //V1 = "901";
        //        //V2 = "001";
        //        //Value = V1.CompareTo(V2);
        //        //lblVersionMessage.Text = V1 + ":" + V2 + ":" + Value.ToString();
        //        //V1 = "001";
        //        //V2 = "001";
        //        //Value = V1.CompareTo(V2);
        //        //lblVersionMessage.Text = V1 + ":" + V2 + ":" + Value.ToString();
        //        //return;

        //        if (V1.CompareTo(V2) > 0)
        //        {
        //            decimal idx = ID;
        //            ID = ID2;
        //            ID2 = idx;
        //        }
        //        ReceiveDetail rd = rdm.ReceiveDetail(ID);
        //        if (rdm.isDetail000There(rd.ESN) == true)
        //        {
        //            SetMessage("There is already a version 000 record.");
        //            return;
        //        }

        //        string rMessage = "";
        //        bool rValue = false;
        //        SetMessage("Device returned from MSC");
        //        ReceiveDetailUtilityMoveLogManager log = new ReceiveDetailUtilityMoveLogManager("Return From MSC", User.Identity.Name);
        //        rValue = rdm.SyncIFSDevices(ID, ID2, drpIFSReasonCodesMSCIn.SelectedItem.Value, chkIFSInformIFSMSCIn.Checked, ref rMessage);
        //        if (rValue == false)
        //        {
        //            SetMessage("Error:" + rMessage);
        //            log.Save(ID, ID2, "Error:" + rMessage);
        //        }
        //        else
        //        {
        //            log.Save(ID, ID2, "Source:" + rMessage);
        //            log.Save(ID2, ID, "Target:" + rMessage);
        //        }

        //    }
        //    RefreshVersion();
        //}
        //void btnAdjustIn_Click(object sender, EventArgs e)
        //{
        //    if (txtAdjustInLocation.Text.Length == 0)
        //    {
        //        SetMessage("You must supply a Location.");
        //        return;
        //    }
        //    IFSLocation loc = new IFSLocation(txtAdjustInLocation.Text);
        //    if (loc.isValid == false)
        //    {
        //        SetMessage("Location is not valid.");
        //        return;
        //    }
        //    if (loc.IsThisFrozen(User.Identity.Name) == true)
        //    {
        //        SetMessage("Location is Frozen.");
        //        return;
        //    }
        //    string Reason = txtAdjustInReason.Text;


        //    List<string> RDIDs = new List<string>();
        //    List<string> Versions = new List<string>();
        //    foreach (GridViewRow line in grdVersion.Rows)
        //    {
        //        CheckBox selected = (CheckBox)line.FindControl("chkVersionGo");
        //        if (selected != null)
        //        {
        //            if (selected.Checked == true)
        //            {
        //                RDIDs.Add(line.Cells[13].Text);
        //                Versions.Add(line.Cells[2].Text);
        //            }
        //        }
        //    }
        //    if (RDIDs.Count() != 1)
        //    {
        //        SetMessage("You can only select 1 record.");
        //        return;
        //    }
        //    decimal ID = -1;
        //    lblVersionMessage.Text = "Error:";
        //    ReceiveDetailManager rdm = new ReceiveDetailManager(User.Identity.Name);
        //    if (decimal.TryParse(RDIDs[0], out ID) == true)
        //    {
        //        ReceiveDetail rd = rdm.ReceiveDetail(ID);
        //        if (rd == null)
        //        {
        //            SetMessage("Record not found.");
        //            return;
        //        }
        //        if (rdm.isDetail000There(rd.ESN) == true)
        //        {
        //            SetMessage("There is already a version 000 record.");
        //            return;
        //        }

        //        string rMessage = "";
        //        bool rValue = false;
        //        SetMessage("Device Adjusted In");
        //        ReceiveDetailUtilityMoveLogManager log = new ReceiveDetailUtilityMoveLogManager("Version Adjust In", User.Identity.Name);
        //        rValue = rdm.AdjustIn(rd.ReceiveDetailID, loc.Text, drpIFSConditionCodesIN.SelectedItem.Value, drpIFSReasonCodesIN.SelectedItem.Value, Reason, chkIFSInformIFSIN.Checked, ref rMessage);
        //        if (rValue == false)
        //        {
        //            SetMessage("Error:" + rMessage);
        //            log.Save(rd.ReceiveDetailID, "Error:" + rMessage);
        //        }
        //        else
        //        {
        //            log.Save(rd.ReceiveDetailID, rMessage);
        //        }

        //    }
        //    RefreshVersion();
        //}
        //void btnVersionChange_Click(object sender, EventArgs e)
        //{
        //    if (txtToVersion.Text.Length == 0)
        //    {
        //        SetMessage("You must supply a replacement Version.");
        //        return;
        //    }
        //    txtToVersion.Text = txtToVersion.Text.Trim();
        //    if (txtToVersion.Text.Length < 3)
        //    {
        //        txtToVersion.Text = txtToVersion.Text.Trim().PadRight(3, '0');
        //    }
        //    if (txtToVersion.Text == "000")
        //    {
        //        SetMessage("You can not change to 000 here. It must be a switch or reactivate.");
        //        return;
        //    }


        //    List<string> RDIDs = new List<string>();
        //    List<string> Versions = new List<string>();
        //    foreach (GridViewRow line in grdVersion.Rows)
        //    {
        //        CheckBox selected = (CheckBox)line.FindControl("chkVersionGo");
        //        if (selected != null)
        //        {
        //            if (selected.Checked == true)
        //            {
        //                RDIDs.Add(line.Cells[13].Text);
        //                Versions.Add(line.Cells[2].Text);
        //            }
        //        }
        //    }
        //    if (RDIDs.Count() != 1)
        //    {
        //        SetMessage("You can only select 1 record.");
        //        return;
        //    }
        //    decimal ID = -1;
        //    ReceiveDetailManager rdm = new ReceiveDetailManager(User.Identity.Name);
        //    if (decimal.TryParse(RDIDs[0], out ID) == true)
        //    {
        //        ReceiveDetail rd = rdm.ReceiveDetail(ID);
        //        if (rd == null)
        //        {
        //            lblVersionMessage.Text = "Record not found.";
        //            return;
        //        }
        //        if (rd.Version == "000")
        //        {
        //            SetMessage("You can not change a 000 record here. It must be a switch or reactivate.");
        //            return;
        //        }
        //        ReceiveDetailUtilityMoveLogManager log = new ReceiveDetailUtilityMoveLogManager("Version Change", User.Identity.Name);
        //        log.Save(ID, "From:" + rd.Version + " To:" + txtToVersion.Text);
        //        rdm.SetVersion(ID, txtToVersion.Text);
        //    }
        //    SetMessage("Version Set.");
        //    txtToVersion.Text = "";
        //    RefreshVersion();
        //}
        //--------------------------------------------------------------------------------------------------
        //void btnToSKU_Click(object sender, EventArgs e)
        //{
        //    if (lstHistory.Items.Count < 1)
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "alert('Error, no from data given!');", true);
        //        SetMessage("No From Data given!");
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

        //    if (decimal.TryParse(sCarrierID, out CarrierID) == false) { SetMessage("Invalid Carrier"); return;}
        //    if (decimal.TryParse(sManufacturerID, out ManufacturerID) == false) { SetMessage("Invalid Manufacturer"); return; }
        //    if (decimal.TryParse(sModelID, out ModelID) == false) { SetMessage("Invalid Model"); return; }
        //    if (decimal.TryParse(sColour, out ColourID) == false) { SetMessage("Invalid Colour"); return; }

        //    ClearSKUDropDowns();
        //    string IP = GetUserIPAddress();
        //    ReceiveDetailManager rd = new ReceiveDetailManager(User.Identity.Name);
        //    if (lstHistory.Items.Count > 0)
        //    {
        //        string ErrorMessage = "";
        //        int count = 0;
        //        int Errorcount = 0;
        //        string rMessage = "";
        //        ReceiveDetailUtilityMoveLogManager log = new ReceiveDetailUtilityMoveLogManager("ToSKU", User.Identity.Name);
        //        decimal ReceiveDetailID = -1;
        //        foreach (ListItem i in lstHistory.Items)
        //        {
        //            //rMessage = rd.IFSSKUUpdate_ESN(i.Text, drpCarrier.SelectedItem.Text, drpManufacturer.SelectedItem.Text, drpModel.SelectedItem.Text, drpColour.SelectedItem.Text, IP, ref ReceiveDetailID);
        //            rMessage = rd.IFSSKUUpdate_ESN01(i.Text, CarrierID, ManufacturerID, ModelID, ColourID, IP, ref ReceiveDetailID);
        //            if (rMessage.Substring(0, 6) != "Error:") { count++; }
        //            if (rMessage.Substring(0, 6) == "Error:") { Errorcount++; ErrorMessage += (ErrorMessage.Length > 0 ? "/" : "") + rMessage; }
        //            log.Save(ReceiveDetailID, rMessage);
        //        }
        //        Clear();
        //        string FullMessage = count.ToString() + " Devices SKU Changed";
        //        if (Errorcount > 0) { FullMessage += " - " + Errorcount.ToString() + " Devices Not SKU Changed"; }
        //        if (Errorcount > 0) { FullMessage += " - " + ErrorMessage; }
        //        SetMessage(FullMessage);
        //    }
        //}
        //void btnToBin_Click(object sender, EventArgs e)
        //{
        //    if (lstHistory.Items.Count < 1)
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "alert('Error, no from data given!');", true);
        //        SetMessage("No From Data given!");
        //        return;
        //    }
        //    if (txtToBin.Text.Length == 0)
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "alert('Error, no Bin Number given!');", true);
        //        SetMessage("No Bin Number given!");
        //        return;
        //    }
        //    string ReportBin = txtToBin.Text;
        //    if (ReportBin.ToUpper() == "BLANK") { ReportBin = ""; }
        //    string IP = GetUserIPAddress();
        //    ReceiveDetailManager rd = new ReceiveDetailManager(User.Identity.Name);
        //    if (lstHistory.Items.Count > 0)
        //    {
        //        string ErrorMessage = "";
        //        int count = 0;
        //        int Errorcount = 0;
        //        string rMessage = "";
        //        ReceiveDetailUtilityMoveLogManager log = new ReceiveDetailUtilityMoveLogManager("ToBin", User.Identity.Name);
        //        decimal ReceiveDetailID = -1;
        //        foreach (ListItem i in lstHistory.Items)
        //        {
        //            rMessage = rd.IFSBinUpdate_ESN(i.Text, ReportBin, IP, ref ReceiveDetailID);
        //            if (rMessage.Substring(0, 6) != "Error:") { count++; }
        //            if (rMessage.Substring(0, 6) == "Error:") { Errorcount++; ErrorMessage += (ErrorMessage.Length > 0 ? "/" : "") + rMessage; }
        //            log.Save(ReceiveDetailID, rMessage);
        //        }
        //        Clear();
        //        string FullMessage = count.ToString() + " Devices Bin Changed";
        //        if (Errorcount > 0) { FullMessage += " - " + Errorcount.ToString() + " Devices Not Changed"; }
        //        if (Errorcount > 0) { FullMessage += " - " + ErrorMessage; }
        //        SetMessage(FullMessage);
        //    }
        //}
        //void btnTOCondition_Click(object sender, EventArgs e)
        //{
        //    if (lstHistory.Items.Count < 1)
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "alert('Error, no from data given!');", true);
        //        SetMessage("No From Data given!");
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
        //        ReceiveDetailUtilityMoveLogManager log = new ReceiveDetailUtilityMoveLogManager("ToCondition", User.Identity.Name);
        //        decimal ReceiveDetailID = -1;
        //        foreach (ListItem i in lstHistory.Items)
        //        {
        //            rMessage = rd.IFSConditionUpdate_ESN(i.Text, CondtionCode, ConditionText, IP, ref ReceiveDetailID);
        //            if (rMessage.Substring(0, 6) != "Error:") {
        //                count++; 

        //            }
        //            if (rMessage.Substring(0, 6) == "Error:") { 
        //                Errorcount++; ErrorMessage += (ErrorMessage.Length > 0 ? "/" : "") + rMessage; 
        //            }
        //            log.Save(ReceiveDetailID, rMessage);
        //        }
        //        Clear();
        //        string FullMessage = count.ToString() + " Devices Condition Changed";
        //        if (Errorcount > 0) { FullMessage += " - " + Errorcount.ToString() + " Devices Not Changed"; }
        //        if (Errorcount > 0) { FullMessage += " - " + ErrorMessage; }
        //        SetMessage(FullMessage);
        //    }

        //}
        //void btnXCLX_Click(object sender, EventArgs e)
        //{
        //    if (lstHistory.Items.Count < 1)
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "alert('Error, no from data given!');", true);
        //        SetMessage("No From Data given!");
        //        return;
        //    }
        //    string IP = GetUserIPAddress();
        //    ReceiveDetailManager rd = new ReceiveDetailManager(User.Identity.Name);
        //    if (lstHistory.Items.Count > 0)
        //    {
        //        string ErrorMessage = "";
        //        int count = 0;
        //        int Errorcount = 0;
        //        string rMessage = "";
        //        ReceiveDetailUtilityMoveLogManager log = new ReceiveDetailUtilityMoveLogManager("XCLX", User.Identity.Name);
        //        decimal ReceiveDetailID = 1;
        //        foreach (ListItem i in lstHistory.Items)
        //        {
        //            rMessage = rd.XCLXProcess(txtClientLocationScanKey.Text, i.Text, ref ReceiveDetailID);
        //            if (rMessage.Substring(0, 6) != "Error:") { 
        //                count++;
        //            }
        //            if (rMessage.Substring(0, 6) == "Error:") {
        //                Errorcount++;
        //                ErrorMessage += (ErrorMessage.Length > 0 ? "/" : "") + rMessage;
        //            }
        //            log.Save(ReceiveDetailID, rMessage);
        //        }
        //        Clear();
        //        string FullMessage = count.ToString() + " Devices Client Location Changed";
        //        if (Errorcount > 0) { FullMessage += " - " + Errorcount.ToString() + " Devices Not Changed"; }
        //        if (Errorcount > 0) { FullMessage += " - " + ErrorMessage; }
        //        SetMessage(FullMessage);
        //    }
        //}
        //void btnSetProjectTag_Click(object sender, EventArgs e)
        //{
        //    if (lstHistory.Items.Count < 1)
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "alert('Error, no from data given!');", true);
        //        SetMessage("No From Data given!");
        //        return;
        //    }
        //    string IP = GetUserIPAddress();
        //    ReceiveDetailManager rd = new ReceiveDetailManager(User.Identity.Name);
        //    if (lstHistory.Items.Count > 0)
        //    {
        //        string ErrorMessage = "";
        //        int count = 0;
        //        int Errorcount = 0;
        //        string rMessage = "";
        //        ReceiveDetailUtilityMoveLogManager log = new ReceiveDetailUtilityMoveLogManager("SetProjectTag", User.Identity.Name);
        //        decimal ReceiveDetailID = -1;
        //        foreach (ListItem i in lstHistory.Items)
        //        {
        //            rMessage = rd.UpdateProjectTag(i.Text, txtProjectTag.Text, ref ReceiveDetailID);
        //            if (rMessage.Substring(0, 6) != "Error:") {
        //                count++;
        //            }
        //            if (rMessage.Substring(0, 6) == "Error:") {
        //                Errorcount++; ErrorMessage += (ErrorMessage.Length > 0 ? "/" : "") + rMessage;
        //            }
        //            log.Save(ReceiveDetailID, rMessage);
        //        }
        //        Clear();
        //        string FullMessage = count.ToString() + " Devices Project Tag Changed";
        //        if (Errorcount > 0) { FullMessage += " - " + Errorcount.ToString() + " Devices Not Changed"; }
        //        if (Errorcount > 0) { FullMessage += " - " + ErrorMessage; }
        //        SetMessage(FullMessage);
        //    }
        //}
        void btnIFSLock_Click(object sender, EventArgs e)
        {
            //if (txtLocation.Text.Length == 0)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "alert('No to location given!');", true);
            //    SetMessage("No to location given!");
            //    return;
            //}
            //#region the Old Way
            //#endregion
            #region The New Way
            //IFSLocation location = new IFSLocation(txtLocation.Text);
            ////IFS_InventoryManager im = new IFS_InventoryManager(User.Identity.Name);
            //if (location.isValid == false)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "alert('Invalid TO location given!');", true);
            //    SetMessage("Invalid TO location given!");
            //    return;
            //}
            //if (location.IsThisFrozen(User.Identity.Name) == true)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "alert('" + location.FrozemMessage + "');", true);
            //    SetMessage(location.FrozemMessage);
            //    return;
            //}
            //if (lstHistory.Items.Count < 1)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "alert('Error, no from data given!');", true);
            //    SetMessage("No From Data given!");
            //    return;
            //}

            //txtLocation.Text = location.Text;
            string IP = GetUserIPAddress();
            ReceiveDetailManager rd = new ReceiveDetailManager(User.Identity.Name);
            if (lstHistory.Items.Count > 0)
            {
                string ErrorMessage = "";
                int count = 0;
                int Errorcount = 0;
                string rMessage = "";
                ReceiveDetailUtilityMoveLogManager log = new ReceiveDetailUtilityMoveLogManager("UpdateLocation", User.Identity.Name);
                decimal ReceiveDetailID = -1;
                foreach (ListItem i in lstHistory.Items)
                {
                    bool Lock = true;
                    if (rdlTransactionOnOff.SelectedItem.Value == "On") { Lock = false; }
                    rMessage = rd.IFSLock_ESN(i.Text, Lock, IP, ref ReceiveDetailID);
                    if (rMessage.Substring(0, 6) != "Error:")
                    {
                        count++;
                    }
                    if (rMessage.Substring(0, 6) == "Error:")
                    {
                        Errorcount++; ErrorMessage += (ErrorMessage.Length > 0 ? "/" : "") + rMessage;
                    }
                    log.Save(ReceiveDetailID, rMessage);
                }
                Clear();
                string FullMessage = count.ToString() + " Devices Set";
                if (Errorcount > 0) { FullMessage += " - " + Errorcount.ToString() + " Devices Not Set"; }
                if (Errorcount > 0) { FullMessage += " - " + ErrorMessage; }
                SetMessage(FullMessage);
            }

            #endregion
        }
        //void btnGOReasonCodesIN_Click(object sender, EventArgs e)
        //{
        //    throw new NotImplementedException();
        //}
        //void btnGOReasonCodes_Click(object sender, EventArgs e)
        //{
        //    if (lstHistory.Items.Count < 1)
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "alert('Error, no IMEI data given!');", true);
        //        SetMessage("No IMEI Data given!");
        //        return;
        //    }
        //    ReceiveDetailManager rd = new ReceiveDetailManager(User.Identity.Name);
        //    if (lstHistory.Items.Count > 0)
        //    {
        //        string ReasonCode = drpIFSReasonCodes.SelectedItem.Value;
        //        string ReasonMessage = txtAdjustOutReason.Text;
        //        string ErrorMessage = "";
        //        int count = 0;
        //        int Errorcount = 0;
        //        //string rMessage = "";
        //        bool rValue = false;
        //        string rMessage = "";
        //        using (clsLinqDataContext ctx = new clsLinqDataContext())
        //        {
        //            ReceiveDetailUtilityMoveLogManager log = new ReceiveDetailUtilityMoveLogManager("MoveShipped", User.Identity.Name);
        //            decimal ReceiveDetailID = -1;
        //            foreach (ListItem i in lstHistory.Items)
        //            {
        //                rMessage = "";
        //                rValue = rd.IFSMOVEShipped(ctx, i.Text, ReasonCode, ReasonMessage, ref rMessage, ref ReceiveDetailID);
        //                if (rValue == true)
        //                {
        //                    count++;
        //                    log.Save(ReceiveDetailID, "Success");
        //                }
        //                if (rValue == false)
        //                {
        //                    Errorcount++; ErrorMessage += (ErrorMessage.Length > 0 ? "/" : "") + i.Text + rMessage;
        //                    log.Save(ReceiveDetailID, i.Text + rMessage);
        //                }
        //            }
        //        }
        //        Clear();
        //        string FullMessage = count.ToString() + " Devices User Disposed";
        //        if (Errorcount > 0) { FullMessage += " - " + Errorcount.ToString() + " Devices Not User Disposed"; }
        //        if (Errorcount > 0) { FullMessage += " - " + ErrorMessage; }
        //        ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "alert('" + FullMessage + "');", true);
        //        SetMessage(FullMessage);
        //    }
        //}

        //void btnReactivate_Click(object sender, EventArgs e)
        //{
        //    string Location = "";
        //    Location = rdReactivateToLocation.SelectedItem.Value;
        //    if (lstHistory.Items.Count < 1)
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "alert('Error, no IMEI data given!');", true);
        //        SetMessage("No IMEI Data given!");
        //        return;
        //    }
        //    if (Location.Length == 0)
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "alert('No to location given!');", true);
        //        SetMessage("No to location given!");
        //        return;
        //    }
        //    IFS_InventoryManager im = new IFS_InventoryManager(User.Identity.Name);
        //    if (im.IsLocationValid(Location) == false)
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "alert('Invalid TO location given!');", true);
        //        SetMessage("Invalid TO location given!");
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
        //        string rMessage = "";
        //        ReceiveDetailUtilityMoveLogManager log = new ReceiveDetailUtilityMoveLogManager("Reactivate", User.Identity.Name);
        //        decimal ReceiveDetailID = -1;
        //        foreach (ListItem i in lstHistory.Items)
        //        {
        //            rValue = rd.VersionBackFrom001(i.Text, NewLocation, chkXCLXx00.Checked, ref rMessage, ref ReceiveDetailID);
        //            if (rValue == true) { 
        //                count++;
        //                log.Save(ReceiveDetailID, "Success");
        //            }
        //            if (rValue == false) { 
        //                Errorcount++; ErrorMessage += (ErrorMessage.Length > 0 ? "/" : "") + i.Text + rMessage;
        //                log.Save(ReceiveDetailID, i.Text + rMessage);
        //            }
        //        }
        //        Clear();
        //        string FullMessage = count.ToString() + " Devices returned from 001";
        //        if (Errorcount > 0) { FullMessage += " - " + Errorcount.ToString() + " Devices Not returned."; }
        //        if (Errorcount > 0) { FullMessage += " - " + ErrorMessage; }
        //        SetMessage(FullMessage);
        //    }
        //}
        //void btnGraveYard_Click(object sender, EventArgs e)
        //{
        //    if (lstHistory.Items.Count < 1)
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "alert('Error, no IMEI data given!');", true);
        //        SetMessage("No IMEI Data given!");
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
        //        ReceiveDetailUtilityMoveLogManager log = new ReceiveDetailUtilityMoveLogManager("GraveYard", User.Identity.Name);
        //        decimal ReceiveDetailID = -1;
        //        foreach (ListItem i in lstHistory.Items)
        //        {
        //            rValue = rd.MoveToGraveYard(i.Text, ReasonCode, ref ReceiveDetailID);
        //            if (rValue == true) { 
        //                count++;
        //                log.Save(ReceiveDetailID, "Success");
        //            }
        //            if (rValue == false) {
        //                string thereason = rd.OKToGraveYardReason(i.Text);
        //                Errorcount++; ErrorMessage += (ErrorMessage.Length > 0 ? "/" : "") + i.Text + thereason;
        //                log.Save(ReceiveDetailID, i.Text + thereason);
        //            }
        //        }
        //        Clear();
        //        string FullMessage = count.ToString() + " Devices grave yarded";
        //        if (Errorcount > 0) { FullMessage += " - " + Errorcount.ToString() + " Devices Not grave yarded."; }
        //        if (Errorcount > 0) { FullMessage += " - " + ErrorMessage; }
        //        SetMessage(FullMessage);
        //    }
        //}

        //void btnRemoveFromPO_Click(object sender, EventArgs e)
        //{
        //    if (lstHistory.Items.Count < 1)
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "alert('Error, no IMEI data given!');", true);
        //        SetMessage("No IMEI Data given!");
        //        return;
        //    }
        //    ReceiveDetailManager rd = new ReceiveDetailManager(User.Identity.Name);
        //    if (lstHistory.Items.Count > 0)
        //    {
        //        string ErrorMessage = "";
        //        int count = 0;
        //        int Errorcount = 0;
        //        //string rMessage = "";
        //        bool rValue = false;
        //        string IP = GetUserIPAddress();
        //        string rMessage = "";
        //        ReceiveDetailUtilityMoveLogManager log = new ReceiveDetailUtilityMoveLogManager("RemovePO", User.Identity.Name);
        //        decimal ReceiveDetailID = -1;
        //        foreach (ListItem i in lstHistory.Items)
        //        {
        //            rMessage = rd.RemoveFromPO_ESN(i.Text, IP, ref ReceiveDetailID);
        //            if (rMessage.Substring(0, 6) != "Error:")
        //            {
        //                count++;
        //            }
        //            if (rMessage.Substring(0, 6) == "Error:")
        //            {
        //                Errorcount++; ErrorMessage += (ErrorMessage.Length > 0 ? "/" : "") + rMessage;
        //            }
        //            log.Save(ReceiveDetailID, rMessage);
        //        }
        //        Clear();
        //        string FullMessage = count.ToString() + " Devices Removed from PO";
        //        if (Errorcount > 0) { FullMessage += " - " + Errorcount.ToString() + " Devices Not Removed from PO."; }
        //        if (Errorcount > 0) { FullMessage += " - " + ErrorMessage; }
        //        SetMessage(FullMessage);
        //    }
        //}

        //void btnPrintKittingLabels_Click(object sender, EventArgs e)
        //{
        //    if (lstHistory.Items.Count < 1)
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "alert('Error, no IMEI data given!');", true);
        //        SetMessage("No IMEI Data given!");
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
        //        hobble.DeleteHobbleList("KL", User.Identity.Name);
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
        //        SetMessage(FullMessage);
        //        if (count > 0)
        //        {
        //            // print the labels.
        //            ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "OpenFinishProductLabel();", true);
        //        }
        //    }
        //}


        //void btnForceClosePO_Click(object sender, EventArgs e)
        //{
        //    if (txtPONumber.Text.Length == 0)
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "alert('Error, no PONumber given!');", true);
        //        SetMessage("No PONumber given!");
        //        return;
        //    }
        //    string IP = GetUserIPAddress();
        //    PurchaseOrderManager rd = new PurchaseOrderManager(User.Identity.Name);
        //    if (txtPONumber.Text.Length > 0)
        //    {
        //        string ErrorMessage = "";
        //        int count = 0;
        //        int Errorcount = 0;
        //        string rMessage = "";
        //        ReceiveDetailUtilityMoveLogManager log = new ReceiveDetailUtilityMoveLogManager("PO FClose", User.Identity.Name);
        //        //decimal ReceiveDetailID = 1;
        //        //foreach (ListItem i in lstHistory.Items)
        //        //{
        //        //    rMessage = rd.XCLXProcess(txtClientLocationScanKey.Text, i.Text, ref ReceiveDetailID);
        //        //    if (rMessage.Substring(0, 6) != "Error:")
        //        //    {
        //        //        count++;
        //        //    }
        //        //    if (rMessage.Substring(0, 6) == "Error:")
        //        //    {
        //        //        Errorcount++;
        //        //        ErrorMessage += (ErrorMessage.Length > 0 ? "/" : "") + rMessage;
        //        //    }
        //        //    log.Save(ReceiveDetailID, rMessage);
        //        //}

        //        if (rd.ForceClosePurchaseOrder(txtPONumber.Text, ref rMessage) == true)
        //        {
        //            count++;
        //        }
        //        else
        //        {
        //            Errorcount++;
        //            ErrorMessage += (ErrorMessage.Length > 0 ? "/" : "") + rMessage;
        //        }

        //        Clear();
        //        string FullMessage = count.ToString() + " PO number:" + txtPONumber.Text + " forced Closed.";
        //        if (Errorcount > 0) { FullMessage += " - " + Errorcount.ToString() + " Purchase orders not closed."; }
        //        if (Errorcount > 0) { FullMessage += " - " + ErrorMessage; }
        //        SetMessage(FullMessage);
        //    }
        //}
        //void btnSetPORecQTY_Click(object sender, EventArgs e)
        //{
        //    if (txtAdjustPONumber.Text.Length == 0)
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "alert('Error, no PONumber given!');", true);
        //        SetMessage("No PONumber given!");
        //        return;
        //    }
        //    if (txtAdjustPOLineNumber.Text.Length == 0)
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "alert('Error, no PO Line Number given!');", true);
        //        SetMessage("No PO Line Number given!");
        //        return;
        //    }
        //    if (txtAdjustPOLineQTY.Text.Length == 0)
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "alert('Error, no PO Line QTY given!');", true);
        //        SetMessage("No PO Line QTY given!");
        //        return;
        //    }

        //    decimal QTY = -1;
        //    if (decimal.TryParse(txtAdjustPOLineQTY.Text, out QTY) == false) {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "alert('Error, PO Line QTY Invalid!');", true);
        //        SetMessage("No PO Line QTY Invalid!");
        //        return;
        //    }

        //    string IP = GetUserIPAddress();
        //    PurchaseOrderManager rd = new PurchaseOrderManager(User.Identity.Name);
        //    if (txtAdjustPONumber.Text.Length > 0)
        //    {
        //        string ErrorMessage = "";
        //        int count = 0;
        //        int Errorcount = 0;
        //        string rMessage = "";
        //        ReceiveDetailUtilityMoveLogManager log = new ReceiveDetailUtilityMoveLogManager("PO FClose", User.Identity.Name);
        //        if (rd.ForceSetPurchaseOrderLineReceivedCount(txtAdjustPONumber.Text, txtAdjustPOLineNumber.Text, QTY, ref rMessage) == true)
        //        {
        //            count++;
        //        }
        //        else
        //        {
        //            Errorcount++;
        //            ErrorMessage += (ErrorMessage.Length > 0 ? "/" : "") + rMessage;
        //        }

        //        Clear();
        //        string FullMessage = count.ToString() + " PO:" + txtAdjustPONumber.Text + ":" + txtAdjustPOLineNumber.Text + " Set.";
        //        if (Errorcount > 0) { FullMessage += " - " + Errorcount.ToString() + " Not Set."; }
        //        if (Errorcount > 0) { FullMessage += " - " + ErrorMessage; }
        //        SetMessage(FullMessage);
        //    }
        //}

        //string CleanText(string value)
        //{
        //    value = value.Replace("\r\n", "");
        //    value = value.Replace("\n", "");
        //    value = value.Replace("\r", "");
        //    return value.Trim();
        //}

        #endregion



        private void RecordUtilityUpdate(string Function, string Keys)
        {

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


    }
}





