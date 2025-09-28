using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
//using Factory_DataModel;
using BW_WebApp.DataManagers;
namespace BW_WebApp.Maintenance
{
    public partial class Maint_SKUUtility : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnValidate.Click += new EventHandler(btnValidate_Click);
            btnVerify.Click += new EventHandler(btnVerify_Click);
            AddCancel.Click += new EventHandler(AddCancel_Click);

            btnAdd.Click += new EventHandler(btnAdd_Click);
            btnDelete.Click += new EventHandler(btnDelete_Click);
            //btnToSKUPrior.Click += new EventHandler(btnToSKUPrior_Click);
            //btnToSKUNext.Click += new EventHandler(btnToSKUNext_Click);
            AddOptionOK.Click += new EventHandler(AddOptionOK_Click);
            btnSKUClear.Click += new EventHandler(btnSKUClear_Click);
            btnRefresh.Click += new EventHandler(btnRefresh_Click);

            //MoveDownQuestion.Click += new ImageClickEventHandler(MoveDownQuestion_Click);
            MoveDownQuestion.Click += new EventHandler(MoveDownQuestion_Click);
            MoveDownCombo.Click += new EventHandler(MoveDownCombo_Click);
            //btrRecord.Click += new EventHandler(btrRecord_Click);
            //btnRecordBin.Click += new EventHandler(btnRecordBin_Click);
            //btrRecordLocation.Click += new EventHandler(btrRecordLocation_Click);
            //btnRecordSite.Click += new EventHandler(btnRecordSite_Click);
            //btnPasteParse.Click += new EventHandler(btnPasteParse_Click);
            //imgbtnClear.Click += new ImageClickEventHandler(imgbtnClear_Click);
            //imgbtnDeleteIMIE.Click += new ImageClickEventHandler(imgbtnDeleteIMIE_Click);
            //-----------------------------------------------------------------------
            //btnUpdateLocation.Click += new EventHandler(btnUpdateLocation_Click);
            //btnToSKU.Click += new EventHandler(btnToSKU_Click);
            //btnTOCondition.Click += new EventHandler(btnTOCondition_Click);
            //btnTOProjectTag.Click += new EventHandler(btnTOProjectTag_Click);
            //btnSearchRefresh.Click += new EventHandler(btnSearchRefresh_Click);

            //btnUnShip.Click += new EventHandler(btnUnShip_Click);


            //grdSearchSuggest.RowDataBound += new GridViewRowEventHandler(grdSearchSuggest_RowDataBound);
            //grdSearchSuggest.RowCommand += new GridViewCommandEventHandler(grdSearchSuggest_RowCommand);

            drpCarrier.SelectedIndexChanged += new EventHandler(drpCarrier_SelectedIndexChanged);
            drpManufacturer.SelectedIndexChanged += new EventHandler(drpManufacturer_SelectedIndexChanged);
            drpModel.SelectedIndexChanged += new EventHandler(drpModel_SelectedIndexChanged);
            drpColour.SelectedIndexChanged += new EventHandler(drpColour_SelectedIndexChanged);


            drpCarrier3.SelectedIndexChanged += new EventHandler(drpCarrier3_SelectedIndexChanged);
            drpManufacturer3.SelectedIndexChanged += new EventHandler(drpManufacturer3_SelectedIndexChanged);
            drpModel3.SelectedIndexChanged += new EventHandler(drpModel3_SelectedIndexChanged);
            drpColour3.SelectedIndexChanged += new EventHandler(drpColour3_SelectedIndexChanged);
            //TabContainer3.OnClientActiveTabChanged += new EventHandler(a_OnClientActiveTabChanged);
            //TabContainer3.OnClientActiveTabChanged +=     

            //TabContainer3.OnClientActiveTabChanged +=    
            //drpMemory.SelectedIndexChanged += new EventHandler(drpMemory_SelectedIndexChanged);


            if (!IsPostBack)
            {
                hdnCarrierID.Value = drpCarrier.ClientID;
                hdnManufacturerID.Value = drpManufacturer.ClientID;
                hdnModelID.Value = drpModel.ClientID;
                hdnColourID.Value = drpColour.ClientID;

                hdnCarrier3ID.Value = drpCarrier3.ClientID;
                hdnManufacturer3ID.Value = drpManufacturer3.ClientID;
                hdnModel3ID.Value = drpModel3.ClientID;
                hdnColour3ID.Value = drpColour3.ClientID;

                hdnUserName.Value = User.Identity.Name;
                //hdnMemoryID.Value = drpMemory.ClientID;

                //TabDIDMToLoc.Visible = (User.IsInRole("DIDMToLoc") == true || User.IsInRole("Admin") == true) ? true : false;
                //TabDIDMToCond.Visible = (User.IsInRole("DIDMToCond") == true || User.IsInRole("Admin") == true) ? true : false;
                //TabDIDMFromESN.Visible = (User.IsInRole("DIDMFromESN") == true || User.IsInRole("Admin") == true) ? true : false;
                //TabDIDMFromBin.Visible = (User.IsInRole("DIDMFromBin") == true || User.IsInRole("Admin") == true) ? true : false;
                //TabDIDMFromLocation.Visible = (User.IsInRole("DIDMFromLocation") == true || User.IsInRole("Admin") == true) ? true : false;
                //TabDIDMFromSite.Visible = (User.IsInRole("DIDMFromSite") == true || User.IsInRole("Admin") == true) ? true : false;
                //TabDIDMFromPaste.Visible = (User.IsInRole("DIDMFromPaste") == true || User.IsInRole("Admin") == true) ? true : false;


                TabSKUUComboSearch.Visible = (User.IsInRole("SKUUCombo") == true || User.IsInRole("Admin") == true) ? true : false;
                //TabSKUUComboSearch3.Visible = (User.IsInRole("SKUUCombo3") == true || User.IsInRole("Admin") == true) ? true : false;
                TabSKUUComboSearch3.Visible = false;
                //TabUnShipDevices.Visible = (User.IsInRole("DIDUnShip") == true || User.IsInRole("Admin") == true) ? true : false;
                //TabToProjectTag.Visible = true;
                TabSKUUComboSearch.Visible = true;
                //TabSKUUComboSearch3.Visible = true;

                //TabDIDMToLoc.Visible = false;

                //TabDIDMToCond.Visible = false;
                //TabDIDMFromESN.Visible = true;
                //TabDIDMFromBin.Visible = true;
                //TabDIDMFromLocation.Visible = false;           // (User.IsInRole("DIDMFromLocation") == true || User.IsInRole("Admin") == true) ? true : false;
                //TabDIDMFromSite.Visible = false;               // (User.IsInRole("DIDMFromSite") == true || User.IsInRole("Admin") == true) ? true : false;
                //TabDIDMFromPaste.Visible = true;
                setupDropDowns();
            }
        }

        void btnSaveSKUSegments_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        void btnDelete_Click(object sender, EventArgs e)
        {
            MasterTableMessage.Text = "btnDelete_Click";
        }

        void btnAdd_Click(object sender, EventArgs e)
        {
            MasterTableMessage.Text = "btnAdd_Click";
        }


        void TabContainer1_ActiveTabChanged(object sender, EventArgs e)
        {
            pnlHome.Visible = true;
            if (TabContainer3.ActiveTab.ID == "TabSKUUComboSearch3") { pnlHome.Visible = false; }
            MasterTableMessage.Text = "ddd:" + TabContainer3.ActiveTab.ID;
            //FillMMCCDropDowns3("Colour");
        }


        void btnRefresh_Click(object sender, EventArgs e)
        {
            setupDropDowns();
        }

        void btnValidate_Click(object sender, EventArgs e)
        {
            MasterTableMessage.Text = "";
            MasterCarrierManufacturerModelColourManager mlm = new MasterCarrierManufacturerModelColourManager(User.Identity.Name);
            MasterCarrierManufacturerLookup ml = mlm.NewMasterCarrierManufacturerLookup();

            ml.Bar_Flip = "";
            if (rdlAddStyle.SelectedIndex == 0) { ml.Bar_Flip = "Bar"; }
            if (rdlAddStyle.SelectedIndex == 1) { ml.Bar_Flip = "Flip"; }

            ml.CDMA_HSPA = "";
            if (rdlAddHSType.SelectedIndex == 0) { ml.CDMA_HSPA = "CDMA"; }
            if (rdlAddHSType.SelectedIndex == 1) { ml.CDMA_HSPA = "HSPA"; }

            ml.Carrier = txtCarrier.Text;
            ml.Colour = txtColour.Text;

            ml.Condition = drpAddCondition.SelectedItem.Text;
            ml.Unit_OS = AddUnitOS.SelectedItem.Text;
            ml.Description = AddDescription.Text;
            ml.Device_Handset = drpAddDeviceHandset.SelectedItem.Text;
            ml.Manufacturer = txtManufacturer.Text;
            ml.Model = txtModel.Text;
            ml.OptionCarrierID = -1; //decimal.Parse(drpAddCarrier.SelectedItem.Value.ToString());
            ml.OptionColourID = -1; //decimal.Parse(drpAddColour.SelectedItem.Value.ToString());
            ml.OptionManufacturerID = -1; //decimal.Parse(drpAddManufacturer.SelectedItem.Value.ToString());
            ml.OptionModelID = -1; //decimal.Parse(drpAddModel.SelectedItem.Value.ToString());
            ml.Retire = "";
            ml.SKU = AddSKU.Text;
            ml.SKU_B = AddSKU_B.Text;
            ml.SKU_C = AddSKU_C.Text;
            ml.SKU_Loaner = AddSKU_Loaner.Text;
            ml.UPC = AddUPC.Text;
            ml.UPC_2 = AddUPC2.Text;
            ml.UPC_3 = AddUPC3.Text;
            ml.WarrantyStickerPlacement = AddWarrantyStickerPlacement.Text;
            ml.NickName = AddNickName.Text;
            MasterTableMessage.Text = MasterTableMessage.Text + Environment.NewLine + mlm.InsertMasterCarrierManufacturerLookup(ml);
            //UpdateMainGrid();
            //pnlMainView.Visible = true;
            pnlAdd.Visible = false;
        }


        void AddCancel_Click(object sender, EventArgs e)
        {
            pnlAdd.Visible = false;
        }

        void btnVerify_Click(object sender, EventArgs e)
        {
            List<string> response = new List<string>();
            MasterTableMessage.Text = "";
            int Sequence = 100;
            txtCarrier.Text = txtCarrier1.Text;
            txtManufacturer.Text = txtManufacturer1.Text;
            txtModel.Text = txtModel1.Text;
            txtColour.Text = txtColour1.Text;
            //if (AddScanKeyOption.Text.Length == 0) { return; }
            if (txtCarrier.Text.Length == 0) { response.Add("You must supply an Carrier before you can add."); }
            if (txtManufacturer.Text.Length == 0) { response.Add("You must supply a Manufacturer before you can add."); }
            if (txtModel.Text.Length == 0) { response.Add("You must supply an Model before you can add."); }
            if (txtColour.Text.Length == 0) { response.Add("You must supply a Colour before you can add."); }
            if (response.Count > 0) { LoadResponsesBottom(response); return; }
            try
            {
                MasterCarrierManufacturerModelColourManager am = new MasterCarrierManufacturerModelColourManager(User.Identity.Name);
                //Edit Checks
                response = am.EditCheckToAdd(txtCarrier.Text, txtManufacturer.Text, txtModel.Text, txtColour.Text);
                LoadResponsesBottom(response);
                if (response[0].Substring(0, 6).ToUpper() == "ERROR:")
                {
                    MasterTableMessage.Text = MasterTableMessage.Text + Environment.NewLine + "RELATIONSHIP NOT ADDED!";
                }
                else
                {
                    pnlAdd.Visible = true;
                    MasterTableMessage.Text = "Not a valid combination!" + Environment.NewLine + "Hit the Validate/Add button to make the new combo valid.";
                    // Physical add attempt.
                    //response = am.AddNewAttributeSegment(rdlSegment.SelectedItem.Text, AddNameOption.Text, AddNameOption.Text, AddDescriptionOption.Text, "100");
                    //LoadResponses(response);
                    //if (response[0].Substring(0, 6).ToUpper() == "ERROR:")
                    //{
                    //    MasterTableMessage.Text = MasterTableMessage.Text + Environment.NewLine + "RELATIONSHIP NOT ADDED!";
                    //}
                    //else
                    //{
                    //    MasterTableMessage.Text = MasterTableMessage.Text + Environment.NewLine + "RELATIONSHIP ADDED!";
                    //}
                    // add the attribute...
                }
            }
            catch (UserAccessControlException ex)
            {
                //MasterTableMessage.Text = lblMessageBTM.Text + Environment.NewLine + "ATTRIBUTE SEGMENT NOT ADDED!  -- really bad error!!!!";
                //ScriptManager.RegisterStartupScript(this, GetType(), "Answer Add", "alert('" + ex.Message + "');", true);
            }
        }

        protected void AddOptionOK_Click(object sender, EventArgs e)
        {
            lblMessageBTM.Text = "";
            int Sequence = 100;
            //if (AddScanKeyOption.Text.Length == 0) { return; }
            if (AddNameOption.Text.Length == 0) { lblMessageBTM.Text = "You must supply an Abbriviation before you can add."; return; }
            if (AddDescriptionOption.Text.Length == 0) { lblMessageBTM.Text = "You must supply a Description before you can add."; return; }
            //if (drpAddStatusOption.SelectedIndex < 0) { return; }
            //if (drpAddTypeOption.SelectedIndex < 0) { return; }
            //if (AddSequenceOption.Text.Length == 0 || int.TryParse(AddSequenceOption.Text, out Sequence) == false) { Sequence = 100; }
            try
            {
                AnswerManager am = new AnswerManager(User.Identity.Name);
                //Edit Checks
                List<string> response = am.VerifyOptionOKToAdd(rdlSegment.SelectedItem.Text, AddNameOption.Text, AddDescriptionOption.Text);
                LoadResponses(response);
                if (response[0].Substring(0, 6).ToUpper() == "ERROR:")
                {
                    lblMessageBTM.Text = lblMessageBTM.Text + Environment.NewLine + "ATTRIBUTE SEGMENT NOT ADDED!";
                }
                else
                {
                    // Physical add attempt.
                    response = am.AddNewAttributeSegment(rdlSegment.SelectedItem.Text, AddNameOption.Text, AddNameOption.Text, AddDescriptionOption.Text, "100");
                    LoadResponses(response);
                    if (response[0].Substring(0, 6).ToUpper() == "ERROR:")
                    {
                        lblMessageBTM.Text = lblMessageBTM.Text + Environment.NewLine + "ATTRIBUTE SEGMENT NOT ADDED!";
                    }
                    else
                    {
                        lblMessageBTM.Text = lblMessageBTM.Text + Environment.NewLine + "ATTRIBUTE SEGMENT ADDED!";
                    }
                    // add the attribute...
                }
            }
            catch (UserAccessControlException ex)
            {
                lblMessageBTM.Text = lblMessageBTM.Text + Environment.NewLine + "ATTRIBUTE SEGMENT NOT ADDED!  -- really bad error!!!!";
                //ScriptManager.RegisterStartupScript(this, GetType(), "Answer Add", "alert('" + ex.Message + "');", true);
            }
        }

        private void LoadResponses(List<string> response)
        {
            foreach (string s in response)
            {
                lblMessageBTM.Text = lblMessageBTM.Text + Environment.NewLine + s;
            }
        }
        private void LoadResponsesBottom(List<string> response)
        {
            foreach (string s in response)
            {
                MasterTableMessage.Text = MasterTableMessage.Text + Environment.NewLine + s;
            }
        }






        void MoveDownCombo_Click(object sender, EventArgs e)
        {
            txtCarrier.Text = lblCarrierABBR.Text.ToUpper();
            txtManufacturer.Text = lblManufacturerABBR.Text.ToUpper();
            txtModel.Text = lblModelABBR.Text.ToUpper();
            txtColour.Text = lblColourABBR.Text.ToUpper();

            txtCarrier1.Text = lblCarrierABBR.Text.ToUpper();
            txtManufacturer1.Text = lblManufacturerABBR.Text.ToUpper();
            txtModel1.Text = lblModelABBR.Text.ToUpper();
            txtColour1.Text = lblColourABBR.Text.ToUpper();


            lblCarrier.Text = hdnCarrierTEXT.Value;
            lblManufacturer.Text = hdnManufacturerTEXT.Value;
            lblModel.Text = hdnModelTEXT.Value;
            lblColour.Text = hdnColourTEXT.Value;
        }

        void MoveDownQuestion_Click(object sender, EventArgs e)
        {
            if (rdlSegment.SelectedItem.Text == "Carrier")
            {
                txtCarrier.Text = AddNameOption.Text.ToUpper();
                txtCarrier1.Text = AddNameOption.Text.ToUpper();
                lblCarrier.Text = AddDescriptionOption.Text;
            }
            if (rdlSegment.SelectedItem.Text == "Manufacturer")
            {
                txtManufacturer.Text = AddNameOption.Text.ToUpper();
                txtManufacturer1.Text = AddNameOption.Text.ToUpper();
                lblManufacturer.Text = AddDescriptionOption.Text;
            }

            if (rdlSegment.SelectedItem.Text == "Model")
            {
                txtModel.Text = AddNameOption.Text.ToUpper();
                txtModel1.Text = AddNameOption.Text.ToUpper();
                lblModel.Text = AddDescriptionOption.Text;
            }

            if (rdlSegment.SelectedItem.Text == "Colour")
            {
                txtColour.Text = AddNameOption.Text.ToUpper();
                txtColour1.Text = AddNameOption.Text.ToUpper();
                lblColour.Text = AddDescriptionOption.Text;
            }
        }

        //protected void TabSegments_ActiveTabChanged(object sender, EventArgs e)
        //{
        //    pnlAddOption.Visible = true;
        //    if (TabSegmentsx.ActiveTab.ID == "TabCombo") { pnlAddOption.Visible = false; }
        //    if (TabSegmentsx.ActiveTab.ID == "TabCarrier") { AddOptionOK.Text = "Add Carrier Segment"; }
        //    if (TabSegmentsx.ActiveTab.ID == "TabManufacturer") { AddOptionOK.Text = "Add Manufacturer Segment"; }
        //    if (TabSegmentsx.ActiveTab.ID == "TabModel") { AddOptionOK.Text = "Add Model Segment"; }
        //    if (TabSegmentsx.ActiveTab.ID == "TabColour") { AddOptionOK.Text = "Add Colour Segment"; }
        //}


        void SetMessage(string Message)
        {
            //lblMessage.Text = Message;          //Top of screen
            lblMessageBTM.Text = Message;       //Bottom of screen
        }



        #region Grid Version
        //void grdVersion_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    System.Web.UI.WebControls.ImageButton btnAdd = (System.Web.UI.WebControls.ImageButton)e.CommandSource;
        //    ReceiveDetailManager rdm = null;
        //    decimal id = -1;
        //    if (decimal.TryParse(btnAdd.CommandArgument, out id) == false) { id = -1; }
        //    switch (btnAdd.CommandName.ToString().ToUpper())
        //    {
        //        //case "SETVERSIONTOZERO":
        //        //    rdm = new ReceiveDetailManager(User.Identity.Name);
        //        //    rdm.AdvanceESNVersion_ToZero(ctx, id);
        //        //    RefreshVersion();
        //        //    ScriptManager.RegisterStartupScript(this, GetType(), "RestHeader", "RestoreHeaderData(); SetUpScreen('');", true);
        //        //    break;
        //        //case "ADVANCEVERSIONNUMBERS":
        //        //    rdm = new ReceiveDetailManager(User.Identity.Name);
        //        //    rdm.AdvanceESNVersion_FromThisOne(ctx, id);
        //        //    RefreshVersion();
        //        //    ScriptManager.RegisterStartupScript(this, GetType(), "RestHeader", "RestoreHeaderData(); SetUpScreen('');", true);
        //        //    break;
        //        //case "DELETERECEIVEDETAILPROCESSLOG":
        //        //    rdm = new ReceiveDetailManager(User.Identity.Name);
        //        //    //rdm.DeleteReceiveDetailProcessLogThisID(id);
        //        //    RefreshVersion();
        //        //    ScriptManager.RegisterStartupScript(this, GetType(), "RestHeader", "RestoreHeaderData(); SetUpScreen('');", true);
        //        //    break;
        //        default:
        //            break;
        //    }
        //}
        //void grdVersion_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        System.Web.UI.WebControls.CheckBox chkPicked = (System.Web.UI.WebControls.CheckBox)e.Row.FindControl("chkVersionGo");
        //        if (chkPicked != null)
        //        {
        //            chkPicked.Enabled = true;
        //            if (((pxlist)e.Row.DataItem).Version == "000")
        //            {
        //                chkPicked.Enabled = false;
        //                chkPicked.Checked = false;
        //                if (User.IsInRole("VER2000"))
        //                {
        //                    chkPicked.Enabled = true;
        //                }
        //            }
        //        }
        //    }
        //}




        //private void RefreshSKUData()
        //{
        //    using (clsLinqDataContext ctx = new clsLinqDataContext())
        //    {
        //        string ESN = hdnCurrentIMEI.Value;
        //        //grdVersion.DataSource = null;
        //        ReceiveDetail rd = ctx.ReceiveDetails.OrderByDescending(x => x.ReceiveDetailID).FirstOrDefault(x => x.ESN == ESN && x.Version == "000");
        //        if (rd != null)
        //        {
        //            ListItem carrier = drpCarrier.Items.FindByValue(rd.CarrierID.ToString());
        //            if (carrier != null) { drpCarrier.SelectedIndex = drpCarrier.Items.IndexOf(carrier); }
        //            FillMMCCDropDowns("Carrier");


        //            ListItem manufacturer = drpManufacturer.Items.FindByValue(rd.ManufacturerID.ToString());
        //            if (manufacturer != null) { drpManufacturer.SelectedIndex = drpManufacturer.Items.IndexOf(manufacturer); }
        //            FillMMCCDropDowns("Manufacturer");


        //            ListItem model = drpModel.Items.FindByValue(rd.ModelID.ToString());
        //            if (model != null) { drpModel.SelectedIndex = drpModel.Items.IndexOf(model); }
        //            FillMMCCDropDowns("Model");

        //            //ReceiveDetailItem rdi = ctx.ReceiveDetailItems.FirstOrDefault(x => x.ReceiveDetailID == rd.ReceiveDetailID && x.Option.Question.Name.ToUpper() == "MEMORY");
        //            //if (rdi != null)
        //            //{
        //            //    ListItem memory = drpMemory.Items.FindByValue(rdi.OptionID.ToString());
        //            //    if (memory != null) { drpMemory.SelectedIndex = drpMemory.Items.IndexOf(model); }
        //            //}
        //            //FillMMCCDropDowns("Memory");

        //            ListItem colour = drpColour.Items.FindByValue(rd.ColourID.ToString());
        //            if (colour != null) { drpColour.SelectedIndex = drpColour.Items.IndexOf(colour); }
        //        }
        //    }
        //}
        //private void RefreshVersion()
        //{
        //    using (clsLinqDataContext ctx = new clsLinqDataContext())
        //    {
        //        string ESN = lblCurrentIMEI.Text;
        //        grdVersion.DataSource = null;
        //        ReceiveDetail rd = ctx.ReceiveDetails.OrderByDescending(x => x.ReceiveDetailID).FirstOrDefault(x => x.ESN == ESN);
        //        if (rd != null)
        //        {
        //            decimal RDID = rd.ReceiveDetailID;
        //            //string sRDID = hdnReceiveDetailID.Value;
        //            //if (decimal.TryParse(sRDID, out RDID) != true) { RDID = -1; }
        //            //if (RDID < 1) { return; }
        //            ReceiveDetailManager rdm = new ReceiveDetailManager(User.Identity.Name);
        //            //List<ReceiveDetail> blist = null;
        //            var blist = from x in rdm.GetReceiveDetailVersionHistory(ctx, RDID)
        //                        let PName = rdm.ProjectName(x.ProjectID)
        //                        let SName = rdm.StatusName(x.StatusID)
        //                        orderby x.Version
        //                        select new pxlist
        //                        {
        //                            ReceiveDetailID = x.ReceiveDetailID,
        //                            ProjectID = decimal.Parse(x.ProjectID.ToString()),
        //                            StatusID = x.StatusID,
        //                            Version = x.Version,
        //                            ESN = x.ESN,
        //                            CreateDate = x.CreateDate,
        //                            ProjectName = PName.ToString(),
        //                            CreateUser = x.CreateUser,
        //                            StatusName = SName.ToString(),
        //                            IFSSite = "NYI",
        //                            IFSProject = "NYI",
        //                            IFSLocation = x.IFSLocation == null ? "" : x.IFSLocation,
        //                            IFSCondition = x.IFSCondition == null ? "" : x.IFSCondition,
        //                            SKU = x.SKU == null ? "" : x.SKU,
        //                            isIFSLocked = x.isIFSLocked == null ? false : (bool)x.isIFSLocked,
        //                        };

        //            //string[] DataKeys = new string[] { "ReceiveDetailID"};
        //            //grdVersion.DataKeyNames = DataKeys;
        //            lblVersionMessage.Text = "";
        //            if (blist.Count() == 0)
        //            {
        //                lblVersionMessage.Text = "No Records Found for this IMEI(" + ESN + ")";
        //            }
        //            grdVersion.DataSource = blist;
        //        }
        //        grdVersion.DataBind();
        //    }

        //}
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
        //    RefreshVersion();


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
        //    RefreshVersion();


        //    //lblCurrentIMEI
        //}


        void btnSKUClear_Click(object sender, EventArgs e)
        {
            hdnCurrentIMEI.Value = "";
            lblDeviceToResku.Text = "";
            ClearSKUDropDowns();
        }
        #endregion


        #region  xxxx
        void setupDropDowns()
        {
            IFS_InventoryPartsManager ipm = new IFS_InventoryPartsManager(User.Identity.Name);
            QuestionManager qm = new QuestionManager(User.Identity.Name);
            List<Option> ol = qm.GetQuestionOptionList("Carrier");
            drpCarrier.Items.Clear();
            drpCarrier3.Items.Clear();
            foreach (Option o in ol.OrderBy(x => x.Sequence))
            {
                ListItem x = new ListItem(o.OptionText, o.OptionID.ToString());
                drpCarrier.Items.Add(x);
                drpCarrier3.Items.Add(x);
            }

            ol = qm.GetQuestionOptionList("DeviceHandset");
            //drpEditDeviceHandset.Items.Clear();
            drpAddDeviceHandset.Items.Clear();
            foreach (Option o in ol.OrderBy(x => x.Sequence))
            {
                //ListItem x = new ListItem(o.OptionText, o.OptionID.ToString());
                //drpEditDeviceHandset.Items.Add(x);

                ListItem y = new ListItem(o.OptionText, o.OptionID.ToString());
                drpAddDeviceHandset.Items.Add(y);
            }

            ol = qm.GetQuestionOptionList("Stock Colour Code");
            //drpEditCondition.Items.Clear();
            drpAddCondition.Items.Clear();
            foreach (Option o in ol.OrderBy(x => x.Sequence))
            {
                //ListItem x = new ListItem(o.OptionText, o.OptionID.ToString());
                //drpEditCondition.Items.Add(x);

                ListItem y = new ListItem(o.OptionText, o.OptionID.ToString());
                drpAddCondition.Items.Add(y);
            }

            ol = qm.GetQuestionOptionList("Unit OS");
            AddUnitOS.Items.Clear();
            //EditUnitOS.Items.Clear();
            foreach (Option o in ol.OrderBy(x => x.Sequence))
            {
                ListItem x = new ListItem(o.OptionText, o.OptionID.ToString());
                AddUnitOS.Items.Add(x);

                //ListItem y = new ListItem(o.OptionText, o.OptionID.ToString());
                //EditUnitOS.Items.Add(x);
            }


            // turn this on to make drop down dependent. Also make the dropdowns auto post back.
            //FillMMCCDropDowns("Carrier");
            //
            // Turn this off if you want drop down dependence.
            ol = qm.GetQuestionOptionList("Manufacturer");
            drpManufacturer.Items.Clear();
            //drpManufacturer.Items.Clear();
            foreach (Option o in ol.OrderBy(x => x.Sequence))
            {
                ListItem x = new ListItem(o.OptionText, o.OptionID.ToString());
                drpManufacturer.Items.Add(x);
                //drpManufacturer.Items.Add(x);
            }
            ol = qm.GetQuestionOptionList("Model");
            drpModel.Items.Clear();
            //drpModel.Items.Clear();
            string a = "";
            foreach (Option o in ol.OrderBy(x => x.Sequence))
            {
                if (o.Name == "NEWMOD")
                {
                    a = o.OptionText;

                }
                ListItem x = new ListItem(o.OptionText, o.OptionID.ToString());
                drpModel.Items.Add(x);
                //drpModel.Items.Add(x);
            }
            ol = qm.GetQuestionOptionList("Colour");
            drpColour.Items.Clear();
            //drpColour.Items.Clear();
            foreach (Option o in ol.OrderBy(x => x.Sequence))
            {
                ListItem x = new ListItem(o.OptionText, o.OptionID.ToString());
                drpColour.Items.Add(x);
                //drpColour.Items.Add(x);
            }
            FillMMCCDropDowns("Carrier");
            //////////////////////////////////////////////////////////////////////////////////
            drpCarrier.Focus();
        }

        #region 03...............Control Carrier Manufacturer Model Colour Dropdowns
        void ClearSKUDropDowns3()
        {
            drpManufacturer3.Items.Clear();
            lblManufacturerABBR3.Text = "";
            drpModel3.Items.Clear();
            lblModelABBR3.Text = "";
            //drpMemory.Items.Clear();
            drpColour3.Items.Clear();
            lblColourABBR3.Text = "";
        }
        void drpCarrier3_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillMMCCDropDowns3("Carrier");
            drpManufacturer3.Focus();
        }
        void drpManufacturer3_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillMMCCDropDowns3("Manufacturer");
            drpModel3.Focus();
        }
        void drpModel3_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillMMCCDropDowns3("Model");
            drpColour3.Focus();
        }
        void drpColour3_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillMMCCDropDowns3("Colour");
        }

        void FillMMCCDropDowns3(string DropDownName)
        {
            decimal CarrierID = -1;
            decimal ManufacturerID = -1;
            decimal ModelID = -1;
            //decimal MemoryID = -1;
            decimal ColourID = -1;
            string CarrierKey = "";
            string ManufacturerKey = "";
            string ModelKey = "";
            string ColourKey = "";
            //string MemoryKey = "";
            //btnToSKU.Enabled = false;
            MasterCarrierManufacturerModelColourManager MM = new MasterCarrierManufacturerModelColourManager(User.Identity.Name);
            if (DropDownName == "Colour")
            {
                ColourKey = "-1";
                if (drpColour3.SelectedItem != null) { ColourKey = drpColour3.SelectedItem.Value; }
                if (decimal.TryParse(ColourKey, out ColourID) == true) { }
                lblColourABBR3.Text = MM.GetQuestionABBR(ColourID);
                hdnColourTEXT.Value = drpColour3.SelectedItem.Text;
                return;
            }
            if (DropDownName == "Carrier")
            {
                CarrierKey = "-1";
                if (drpCarrier3.SelectedItem != null) { CarrierKey = drpCarrier3.SelectedItem.Value; }
                if (decimal.TryParse(CarrierKey, out CarrierID) == true) { FillManufacturer3(CarrierID); }
                lblCarrierABBR3.Text = MM.GetQuestionABBR(CarrierID);
                hdnCarrierTEXT.Value = drpCarrier3.SelectedItem.Text;
                return;
            }
            if (DropDownName == "Manufacturer")
            {
                CarrierKey = "-1";
                ManufacturerKey = "-1";
                if (drpCarrier3.SelectedItem != null) { CarrierKey = drpCarrier3.SelectedItem.Value; }
                if (drpManufacturer3.SelectedItem != null) { ManufacturerKey = drpManufacturer3.SelectedItem.Value; }

                if (decimal.TryParse(CarrierKey, out CarrierID) == true
                    && decimal.TryParse(ManufacturerKey, out ManufacturerID) == true)
                { FillModel3(CarrierID, ManufacturerID); }
                lblManufacturerABBR3.Text = MM.GetQuestionABBR(ManufacturerID);
                hdnManufacturerTEXT.Value = drpManufacturer3.SelectedItem.Text;
                return;
            }

            if (DropDownName == "Model")
            {
                CarrierKey = "-1";
                ManufacturerKey = "-1";
                ModelKey = "-1";
                if (drpCarrier3.SelectedItem != null) { CarrierKey = drpCarrier3.SelectedItem.Value; }
                if (drpManufacturer3.SelectedItem != null) { ManufacturerKey = drpManufacturer3.SelectedItem.Value; }
                if (drpModel3.SelectedItem != null) { ModelKey = drpModel3.SelectedItem.Value; }

                if (decimal.TryParse(CarrierKey, out CarrierID) == true
                    && decimal.TryParse(ManufacturerKey, out ManufacturerID) == true
                    && decimal.TryParse(ModelKey, out ModelID) == true)
                { FillColour3(CarrierID, ManufacturerID, ModelID); }
                lblModelABBR3.Text = MM.GetQuestionABBR(ModelID);
                hdnModelTEXT.Value = drpModel3.SelectedItem.Text;
                return;
            }
        }
        void FillManufacturer3(decimal CarrierID)
        {
            MasterCarrierManufacturerModelColourManager MM = new MasterCarrierManufacturerModelColourManager(User.Identity.Name);
            List<MasterCarrierManufacturerLookup> ml = MM.GetMasterManufacturerList(CarrierID.ToString());
            drpManufacturer3.Items.Clear();
            foreach (MasterCarrierManufacturerLookup o in ml.OrderBy(x => x.Manufacturer))
            {
                ListItem x = new ListItem(o.Manufacturer, o.OptionManufacturerID.ToString());
                drpManufacturer3.Items.Add(x);
            }
            if (drpManufacturer3.Items.Count > 0) { drpManufacturer3.SelectedIndex = 0; }
            FillMMCCDropDowns3("Manufacturer");
        }
        void FillModel3(decimal CarrierID, decimal ManufacturerID)
        {
            MasterCarrierManufacturerModelColourManager MM = new MasterCarrierManufacturerModelColourManager(User.Identity.Name);
            List<MasterCarrierManufacturerLookup> ml = MM.GetMasterModelList(CarrierID.ToString(), ManufacturerID.ToString());
            drpModel3.Items.Clear();
            foreach (MasterCarrierManufacturerLookup o in ml.OrderBy(x => x.Model))
            {
                ListItem x = new ListItem(o.Model, o.OptionModelID.ToString());
                drpModel3.Items.Add(x);
            }
            if (drpModel3.Items.Count > 0) { drpModel3.SelectedIndex = 0; }
            FillMMCCDropDowns3("Model");
        }
        void FillColour3(decimal CarrierID, decimal ManufacturerID, decimal ModelID)
        {
            MasterCarrierManufacturerModelColourManager MM = new MasterCarrierManufacturerModelColourManager(User.Identity.Name);
            List<MasterCarrierManufacturerLookup> ml = MM.GetMasterColourList(CarrierID.ToString(), ManufacturerID.ToString(), ModelID.ToString());
            drpColour3.Items.Clear();
            foreach (MasterCarrierManufacturerLookup o in ml.OrderBy(x => x.Colour))
            {
                ListItem x = new ListItem(o.Colour, o.OptionColourID.ToString());
                drpColour3.Items.Add(x);
            }
            //if (drpColour.Items.Count > 0) { drpColour.SelectedIndex = 0; btnToSKU.Enabled = true; }
            FillMMCCDropDowns3("Colour");
        }
        //void FillMemory(decimal ModelID)
        //{
        //    MasterCarrierManufacturerModelColourManager MM = new MasterCarrierManufacturerModelColourManager(User.Identity.Name);
        //    List<PairIDValue> ml = MM.GetMasterMemoryList(ModelID.ToString());
        //    drpMemory.Items.Clear();
        //    foreach (PairIDValue o in ml.OrderBy(x => x.Desc))
        //    {
        //        ListItem x = new ListItem(o.Desc, o.ID.ToString());
        //        drpMemory.Items.Add(x);
        //    }
        //    if (drpMemory.Items.Count > 0) { drpMemory.SelectedIndex = 0; }
        //    FillMMCCDropDowns("Memory");
        //}
        #endregion
        #region Control Carrier Manufacturer Model Colour Dropdowns
        void ClearSKUDropDowns()
        {
            drpManufacturer.Items.Clear();
            lblManufacturerABBR.Text = "";
            drpModel.Items.Clear();
            lblModelABBR.Text = "";
            //drpMemory.Items.Clear();
            drpColour.Items.Clear();
            lblColourABBR.Text = "";
        }
        void drpCarrier_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillMMCCDropDowns("Carrier");
            drpManufacturer.Focus();
        }
        void drpManufacturer_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillMMCCDropDowns("Manufacturer");
            drpModel.Focus();
        }
        void drpModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillMMCCDropDowns("Model");
            drpColour.Focus();
        }
        void drpColour_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillMMCCDropDowns("Colour");
        }

        void FillMMCCDropDowns(string DropDownName)
        {
            decimal CarrierID = -1;
            decimal ManufacturerID = -1;
            decimal ModelID = -1;
            //decimal MemoryID = -1;
            decimal ColourID = -1;
            string CarrierKey = "";
            string ManufacturerKey = "";
            string ModelKey = "";
            string ColourKey = "";
            //string MemoryKey = "";
            //btnToSKU.Enabled = false;
            MasterCarrierManufacturerModelColourManager MM = new MasterCarrierManufacturerModelColourManager(User.Identity.Name);
            if (DropDownName == "Colour")
            {
                ColourKey = "-1";
                if (drpColour.SelectedItem != null) { ColourKey = drpColour.SelectedItem.Value; }
                if (decimal.TryParse(ColourKey, out ColourID) == true) { }
                lblColourABBR.Text = MM.GetQuestionABBR(ColourID);
                hdnColourTEXT.Value = drpColour.SelectedItem.Text;
                return;
            }
            if (DropDownName == "Carrier")
            {
                CarrierKey = "-1";
                if (drpCarrier.SelectedItem != null) { CarrierKey = drpCarrier.SelectedItem.Value; }
                //if (decimal.TryParse(CarrierKey, out CarrierID) == true) { FillManufacturer(CarrierID); }
                if (decimal.TryParse(CarrierKey, out CarrierID) == false) { CarrierID = -1; }
                lblCarrierABBR.Text = MM.GetQuestionABBR(CarrierID);
                hdnCarrierTEXT.Value = drpCarrier.SelectedItem.Text;
                FillMMCCDropDowns("Manufacturer");
                return;
            }
            if (DropDownName == "Manufacturer")
            {
                CarrierKey = "-1";
                ManufacturerKey = "-1";
                if (drpCarrier.SelectedItem != null) { CarrierKey = drpCarrier.SelectedItem.Value; }
                if (drpManufacturer.SelectedItem != null) { ManufacturerKey = drpManufacturer.SelectedItem.Value; }
                //if (decimal.TryParse(CarrierKey, out CarrierID) == true && decimal.TryParse(ManufacturerKey, out ManufacturerID) == true) { FillModel(CarrierID, ManufacturerID); }
                if (decimal.TryParse(ManufacturerKey, out ManufacturerID) == false) { ManufacturerID = -1; }
                lblManufacturerABBR.Text = MM.GetQuestionABBR(ManufacturerID);
                hdnManufacturerTEXT.Value = drpManufacturer.SelectedItem.Text;
                FillMMCCDropDowns("Model");
                return;
            }

            if (DropDownName == "Model")
            {
                CarrierKey = "-1";
                ManufacturerKey = "-1";
                ModelKey = "-1";
                if (drpCarrier.SelectedItem != null) { CarrierKey = drpCarrier.SelectedItem.Value; }
                if (drpManufacturer.SelectedItem != null) { ManufacturerKey = drpManufacturer.SelectedItem.Value; }
                if (drpModel.SelectedItem != null) { ModelKey = drpModel.SelectedItem.Value; }
                //if (decimal.TryParse(CarrierKey, out CarrierID) == true && decimal.TryParse(ManufacturerKey, out ManufacturerID) == true && decimal.TryParse(ModelKey, out ModelID) == true) { FillColour(CarrierID, ManufacturerID, ModelID); }
                if (decimal.TryParse(ModelKey, out ModelID) == false) { ModelID = -1; }
                lblModelABBR.Text = MM.GetQuestionABBR(ModelID);
                hdnModelTEXT.Value = drpModel.SelectedItem.Text;
                FillMMCCDropDowns("Colour");
                return;
            }
        }
        void FillManufacturer(decimal CarrierID)
        {
            MasterCarrierManufacturerModelColourManager MM = new MasterCarrierManufacturerModelColourManager(User.Identity.Name);
            List<MasterCarrierManufacturerLookup> ml = MM.GetMasterManufacturerList(CarrierID.ToString());
            drpManufacturer.Items.Clear();
            foreach (MasterCarrierManufacturerLookup o in ml.OrderBy(x => x.Manufacturer))
            {
                ListItem x = new ListItem(o.Manufacturer, o.OptionManufacturerID.ToString());
                drpManufacturer.Items.Add(x);
            }
            if (drpManufacturer.Items.Count > 0) { drpManufacturer.SelectedIndex = 0; }
            FillMMCCDropDowns("Manufacturer");
        }
        void FillModel(decimal CarrierID, decimal ManufacturerID)
        {
            MasterCarrierManufacturerModelColourManager MM = new MasterCarrierManufacturerModelColourManager(User.Identity.Name);
            List<MasterCarrierManufacturerLookup> ml = MM.GetMasterModelList(CarrierID.ToString(), ManufacturerID.ToString());
            drpModel.Items.Clear();
            foreach (MasterCarrierManufacturerLookup o in ml.OrderBy(x => x.Model))
            {
                ListItem x = new ListItem(o.Model, o.OptionModelID.ToString());
                drpModel.Items.Add(x);
            }
            if (drpModel.Items.Count > 0) { drpModel.SelectedIndex = 0; }
            FillMMCCDropDowns("Model");
        }
        void FillColour(decimal CarrierID, decimal ManufacturerID, decimal ModelID)
        {
            MasterCarrierManufacturerModelColourManager MM = new MasterCarrierManufacturerModelColourManager(User.Identity.Name);
            List<MasterCarrierManufacturerLookup> ml = MM.GetMasterColourList(CarrierID.ToString(), ManufacturerID.ToString(), ModelID.ToString());
            drpColour.Items.Clear();
            foreach (MasterCarrierManufacturerLookup o in ml.OrderBy(x => x.Colour))
            {
                ListItem x = new ListItem(o.Colour, o.OptionColourID.ToString());
                drpColour.Items.Add(x);
            }
            //if (drpColour.Items.Count > 0) { drpColour.SelectedIndex = 0; btnToSKU.Enabled = true; }
            FillMMCCDropDowns("Colour");
        }
        //void FillMemory(decimal ModelID)
        //{
        //    MasterCarrierManufacturerModelColourManager MM = new MasterCarrierManufacturerModelColourManager(User.Identity.Name);
        //    List<PairIDValue> ml = MM.GetMasterMemoryList(ModelID.ToString());
        //    drpMemory.Items.Clear();
        //    foreach (PairIDValue o in ml.OrderBy(x => x.Desc))
        //    {
        //        ListItem x = new ListItem(o.Desc, o.ID.ToString());
        //        drpMemory.Items.Add(x);
        //    }
        //    if (drpMemory.Items.Count > 0) { drpMemory.SelectedIndex = 0; }
        //    FillMMCCDropDowns("Memory");
        //}
        #endregion
        #endregion

        #region UtilityExecute

        //void btnUnShip_Click(object sender, EventArgs e)
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
        //        ReceiveDetailUtilityMoveLogManager log = new ReceiveDetailUtilityMoveLogManager("ToSKU", User.Identity.Name);
        //        decimal ReceiveDetailID = -1;
        //        foreach (ListItem i in lstHistory.Items)
        //        {
        //            //rMessage = rd.IFSSKUUpdate_ESN(i.Text, drpCarrier.SelectedItem.Text, drpManufacturer.SelectedItem.Text, drpModel.SelectedItem.Text, drpColour.SelectedItem.Text, IP, ref ReceiveDetailID);
        //            rMessage = rd.UnShipDevice(i.Text, IP, ref ReceiveDetailID);
        //            if (rMessage.Substring(0, 6) != "Error:") { count++; }
        //            if (rMessage.Substring(0, 6) == "Error:") { Errorcount++; ErrorMessage += (ErrorMessage.Length > 0 ? "/" : "") + rMessage; }
        //            log.Save(ReceiveDetailID, rMessage);
        //        }
        //        Clear();
        //        string FullMessage = count.ToString() + " Devices Unshipped";
        //        if (Errorcount > 0) { FullMessage += " - " + Errorcount.ToString() + " Devices Not Unshipped"; }
        //        if (Errorcount > 0) { FullMessage += " - " + ErrorMessage; }
        //        SetMessage(FullMessage);
        //    }
        //}



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
        void btnToSKU_Click(object sender, EventArgs e)
        {
            //if (lstHistory.Items.Count < 1)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "alert('Error, no from data given!');", true);
            //    SetMessage("No From Data given!");
            //    return;
            //}

            string sCarrierID = drpCarrier.SelectedItem.Value;
            string sManufacturerID = drpManufacturer.SelectedItem.Value;
            string sModelID = drpModel.SelectedItem.Value;
            string sColour = drpColour.SelectedItem.Value;
            //string sMemory = drpMemory.SelectedItem.Value;


            decimal CarrierID = -1;
            decimal ManufacturerID = -1;
            decimal ModelID = -1;
            decimal ColourID = -1;
            //decimal MemoryID = -1;

            if (decimal.TryParse(sCarrierID, out CarrierID) == false) { SetMessage("Invalid Carrier"); return; }
            if (decimal.TryParse(sManufacturerID, out ManufacturerID) == false) { SetMessage("Invalid Manufacturer"); return; }
            if (decimal.TryParse(sModelID, out ModelID) == false) { SetMessage("Invalid Model"); return; }
            //if (decimal.TryParse(sMemory, out MemoryID) == false) { SetMessage("Invalid Memory"); return; }
            if (decimal.TryParse(sColour, out ColourID) == false) { SetMessage("Invalid Colour"); return; }

            ClearSKUDropDowns();
            string IP = GetUserIPAddress();
            ReceiveDetailManager rd = new ReceiveDetailManager(User.Identity.Name);
            //if (lstHistory.Items.Count > 0)
            //{
            //    string ErrorMessage = "";
            //    int count = 0;
            //    int Errorcount = 0;
            //    string rMessage = "";
            //    ReceiveDetailUtilityMoveLogManager log = new ReceiveDetailUtilityMoveLogManager("ToSKU", User.Identity.Name);
            //    decimal ReceiveDetailID = -1;
            //    foreach (ListItem i in lstHistory.Items)
            //    {
            //        //rMessage = rd.IFSSKUUpdate_ESN(i.Text, drpCarrier.SelectedItem.Text, drpManufacturer.SelectedItem.Text, drpModel.SelectedItem.Text, drpColour.SelectedItem.Text, IP, ref ReceiveDetailID);
            //        rMessage = rd.IFSSKUUpdate_ESN02(i.Text, CarrierID, ManufacturerID, ModelID, -1, ColourID, IP, ref ReceiveDetailID);
            //        if (rMessage.Substring(0, 6) != "Error:") { count++; }
            //        if (rMessage.Substring(0, 6) == "Error:") { Errorcount++; ErrorMessage += (ErrorMessage.Length > 0 ? "/" : "") + rMessage; }
            //        log.Save(ReceiveDetailID, rMessage);
            //    }
            //    Clear();
            //    string FullMessage = count.ToString() + " Devices SKU Changed";
            //    if (Errorcount > 0) { FullMessage += " - " + Errorcount.ToString() + " Devices Not SKU Changed"; }
            //    if (Errorcount > 0) { FullMessage += " - " + ErrorMessage; }
            //    SetMessage(FullMessage);
            //}
        }
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

        //void btnTOProjectTag_Click(object sender, EventArgs e)
        //{
        //    if (lstHistory.Items.Count < 1)
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "alert('Error, no from data given!');", true);
        //        SetMessage("No From Data given!");
        //        return;
        //    }
        //    if (txtTOProjectTag.Text.Length < 1)
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "alert('Error, To Project Tag Blank!');", true);
        //        SetMessage("No To Data given!");
        //        return;
        //    }
        //    string toProjectTag = txtTOProjectTag.Text;
        //    string IP = GetUserIPAddress();

        //    ReceiveDetailManager rd = new ReceiveDetailManager(User.Identity.Name);
        //    if (lstHistory.Items.Count > 0)
        //    {
        //        string ErrorMessage = "";
        //        int count = 0;
        //        int Errorcount = 0;
        //        string rMessage = "";
        //        ReceiveDetailUtilityMoveLogManager log = new ReceiveDetailUtilityMoveLogManager("ToProjectTag", User.Identity.Name);
        //        decimal ReceiveDetailID = -1;
        //        foreach (ListItem i in lstHistory.Items)
        //        {

        //            rMessage = rd.UpdateProjectTag(i.Text, toProjectTag, ref ReceiveDetailID);
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
        //        string FullMessage = count.ToString() + " Devices Project Tag Changed";
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
        //void btnUpdateLocation_Click(object sender, EventArgs e)
        //{
        //    if (txtLocation.Text.Length == 0)
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "alert('No to location given!');", true);
        //        SetMessage("No to location given!");
        //        return;
        //    }
        //    //#region the Old Way
        //    //#endregion
        //    #region The New Way
        //    IFSLocation location = new IFSLocation(txtLocation.Text);
        //    //IFS_InventoryManager im = new IFS_InventoryManager(User.Identity.Name);
        //    if (location.isValid == false)
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "alert('Invalid TO location given!');", true);
        //        SetMessage("Invalid TO location given!");
        //        return;
        //    }
        //    if (location.IsThisFrozen(User.Identity.Name) == true)
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "alert('" + location.FrozemMessage + "');", true);
        //        SetMessage(location.FrozemMessage);
        //        return;
        //    }
        //    if (lstHistory.Items.Count < 1)
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "alert('Error, no from data given!');", true);
        //        SetMessage("No From Data given!");
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
        //        ReceiveDetailUtilityMoveLogManager log = new ReceiveDetailUtilityMoveLogManager("UpdateLocation", User.Identity.Name);
        //        decimal ReceiveDetailID = -1;
        //        foreach (ListItem i in lstHistory.Items)
        //        {
        //            rMessage = rd.IFSLocationUpdate_ESN(i.Text, location.AssignThisLocationText(), IP, ref ReceiveDetailID);
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
        //        string FullMessage = count.ToString() + " Devices Moved";
        //        if (Errorcount > 0) { FullMessage += " - " + Errorcount.ToString() + " Devices Not Moved"; }
        //        if (Errorcount > 0) { FullMessage += " - " + ErrorMessage; }
        //        SetMessage(FullMessage);
        //    }

        //    #endregion
        //}
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