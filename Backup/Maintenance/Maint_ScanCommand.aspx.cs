using System;
using System.Linq;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

//using Factory_DataModel;
using BW_WebApp.DataManagers;

namespace BW_WebApp.Maintenance
{
    public partial class Maint_ScanCommand : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //btnValidate.Click += new EventHandler(btnValidate_Click);
            //btnVerify.Click += new EventHandler(btnVerify_Click);
            //AddCancel.Click += new EventHandler(AddCancel_Click);

            //btnAdd.Click += new EventHandler(btnAdd_Click);
            //btnDelete.Click += new EventHandler(btnDelete_Click);

            btnSelectCarrier.Click += new EventHandler(btnSelectCarrier_Click);
            btnRefresh3.Click += new EventHandler(btnRefresh3_Click);

            btnRefresh2.Click += new EventHandler(btnRefresh3_Click);

            btnSaveSKUSegments.Click += new EventHandler(btnSaveSKUSegments_Click);
            

            btnSelectManufacturer.Click += new EventHandler(btnSelectManufacturer_Click);
            btnSelectModel.Click += new EventHandler(btnSelectModel_Click);
            btnAddColour.Click += new EventHandler(btnAddColour_Click);
            btnAddQuestion.Click += new EventHandler(btnAddQuestion_Click);
            btnSKUClear3.Click += new EventHandler(btnSKUClear3_Click);
            btnDeleteCommand.Click += new EventHandler(btnDeleteCommand_Click);

            MainGrid.RowCommand += new GridViewCommandEventHandler(MainGrid_RowCommand);
            MainGrid.RowDataBound +=new GridViewRowEventHandler(MainGrid_RowDataBound);

            //btnToSKUPrior.Click += new EventHandler(btnToSKUPrior_Click);
            //btnToSKUNext.Click += new EventHandler(btnToSKUNext_Click);
            //AddOptionOK.Click += new EventHandler(AddOptionOK_Click);
            //btnSKUClear.Click += new EventHandler(btnSKUClear_Click);
            //btnRefresh.Click += new EventHandler(btnRefresh_Click);
            ////MoveDownQuestion.Click += new ImageClickEventHandler(MoveDownQuestion_Click);
            //MoveDownQuestion.Click += new EventHandler(MoveDownQuestion_Click);
            
            //MoveDownCombo.Click += new EventHandler(MoveDownCombo_Click);
            ////btrRecord.Click += new EventHandler(btrRecord_Click);
            ////btnRecordBin.Click += new EventHandler(btnRecordBin_Click);
            ////btrRecordLocation.Click += new EventHandler(btrRecordLocation_Click);
            ////btnRecordSite.Click += new EventHandler(btnRecordSite_Click);
            ////btnPasteParse.Click += new EventHandler(btnPasteParse_Click);
            ////imgbtnClear.Click += new ImageClickEventHandler(imgbtnClear_Click);
            ////imgbtnDeleteIMIE.Click += new ImageClickEventHandler(imgbtnDeleteIMIE_Click);
            ////-----------------------------------------------------------------------
            ////btnUpdateLocation.Click += new EventHandler(btnUpdateLocation_Click);
            ////btnToSKU.Click += new EventHandler(btnToSKU_Click);
            ////btnTOCondition.Click += new EventHandler(btnTOCondition_Click);
            ////btnTOProjectTag.Click += new EventHandler(btnTOProjectTag_Click);
            ////btnSearchRefresh.Click += new EventHandler(btnSearchRefresh_Click);

            ////btnUnShip.Click += new EventHandler(btnUnShip_Click);


            ////grdSearchSuggest.RowDataBound += new GridViewRowEventHandler(grdSearchSuggest_RowDataBound);
            ////grdSearchSuggest.RowCommand += new GridViewCommandEventHandler(grdSearchSuggest_RowCommand);
            //drpCarrier.SelectedIndexChanged += new EventHandler(drpCarrier_SelectedIndexChanged);
            //drpManufacturer.SelectedIndexChanged += new EventHandler(drpManufacturer_SelectedIndexChanged);
            //drpModel.SelectedIndexChanged += new EventHandler(drpModel_SelectedIndexChanged);
            //drpColour.SelectedIndexChanged += new EventHandler(drpColour_SelectedIndexChanged);


            drpCarrier3.SelectedIndexChanged += new EventHandler(drpCarrier3_SelectedIndexChanged);
            drpManufacturer3.SelectedIndexChanged += new EventHandler(drpManufacturer3_SelectedIndexChanged);
            drpModel3.SelectedIndexChanged += new EventHandler(drpModel3_SelectedIndexChanged);
            drpColour3.SelectedIndexChanged += new EventHandler(drpColour3_SelectedIndexChanged);

            drpQuestion.SelectedIndexChanged += new EventHandler(drpQuestion_SelectedIndexChanged);


            //TabContainer3.OnClientActiveTabChanged += new EventHandler(a_OnClientActiveTabChanged);
            //TabContainer3.OnClientActiveTabChanged +=     

            //TabContainer3.OnClientActiveTabChanged +=    
            //drpMemory.SelectedIndexChanged += new EventHandler(drpMemory_SelectedIndexChanged);


            if (!IsPostBack)
            {
                //hdnCarrierID.Value = drpCarrier.ClientID;
                //hdnManufacturerID.Value = drpManufacturer.ClientID;
                //hdnModelID.Value = drpModel.ClientID;
                //hdnColourID.Value = drpColour.ClientID;

                hdnCarrier3ID.Value = drpCarrier3.ClientID;
                hdnManufacturer3ID.Value = drpManufacturer3.ClientID;
                hdnModel3ID.Value = drpModel3.ClientID;
                hdnColour3ID.Value = drpColour3.ClientID;

                hdnUserName.Value = User.Identity.Name;
                ////hdnMemoryID.Value = drpMemory.ClientID;

                ////TabDIDMToLoc.Visible = (User.IsInRole("DIDMToLoc") == true || User.IsInRole("Admin") == true) ? true : false;
                ////TabDIDMToCond.Visible = (User.IsInRole("DIDMToCond") == true || User.IsInRole("Admin") == true) ? true : false;
                ////TabDIDMFromESN.Visible = (User.IsInRole("DIDMFromESN") == true || User.IsInRole("Admin") == true) ? true : false;
                ////TabDIDMFromBin.Visible = (User.IsInRole("DIDMFromBin") == true || User.IsInRole("Admin") == true) ? true : false;
                ////TabDIDMFromLocation.Visible = (User.IsInRole("DIDMFromLocation") == true || User.IsInRole("Admin") == true) ? true : false;
                ////TabDIDMFromSite.Visible = (User.IsInRole("DIDMFromSite") == true || User.IsInRole("Admin") == true) ? true : false;
                ////TabDIDMFromPaste.Visible = (User.IsInRole("DIDMFromPaste") == true || User.IsInRole("Admin") == true) ? true : false;


                //TabSKUUComboSearch.Visible = (User.IsInRole("SKUUCombo") == true || User.IsInRole("Admin") == true) ? true : false;
                //TabSKUUComboSearch3.Visible = (User.IsInRole("SKUUCombo3") == true || User.IsInRole("Admin") == true) ? true : false;
                ////TabUnShipDevices.Visible = (User.IsInRole("DIDUnShip") == true || User.IsInRole("Admin") == true) ? true : false;
                ////TabToProjectTag.Visible = true;
                //TabSKUUComboSearch.Visible = true;
                ////TabSKUUComboSearch3.Visible = true;

                ////TabDIDMToLoc.Visible = false;

                ////TabDIDMToCond.Visible = false;
                ////TabDIDMFromESN.Visible = true;
                ////TabDIDMFromBin.Visible = true;
                ////TabDIDMFromLocation.Visible = false;           // (User.IsInRole("DIDMFromLocation") == true || User.IsInRole("Admin") == true) ? true : false;
                ////TabDIDMFromSite.Visible = false;               // (User.IsInRole("DIDMFromSite") == true || User.IsInRole("Admin") == true) ? true : false;
                ////TabDIDMFromPaste.Visible = true;
                ClearScreen();
                setupDropDowns();
            }
        }

        void btnSaveSKUSegments_Click(object sender, EventArgs e)
        {
            //// aaa-boukbbb-ccc
            //SetMessage("");
            //if (txtUPCCode.Text.Length < 5) { SetMessage("Code not a valid SKU"); return; }
            //string Manufacturer = "";
            //string Model = "";
            //string Colour = "";

            //string mManufacturer = "";
            //string mModel = "";
            //string mColour = "";

            //decimal ManufacturerID = -1;
            //decimal ModelID = -1;
            //decimal ColourID = -1;
            //string rOptionID = "";
            //string[] threeSegments = txtUPCCode.Text.Split('-');
            //if (threeSegments.Count() != 3) { SetMessage("Code not a valid SKU (missing segments)"); return; }
            //if (threeSegments[0].Length < 1) { SetMessage("Code not a valid SKU (missing manufacturer)"); return; }
            //if (threeSegments[1].Length < 1) { SetMessage("Code not a valid SKU (missing model)"); return; }
            //if (threeSegments[2].Length < 1) { SetMessage("Code not a valid SKU (missing colour"); return; }
            //Manufacturer = threeSegments[0];
            //Model = threeSegments[1];
            //Colour = threeSegments[2];

            //rOptionID = drpManufacturer3.SelectedItem.Value;
            //ManufacturerID = decimal.Parse(rOptionID);
            //rOptionID = drpModel3.SelectedItem.Value;
            //ModelID = decimal.Parse(rOptionID);
            //rOptionID = drpColour3.SelectedItem.Value;
            //ColourID = decimal.Parse(rOptionID);

            //QuestionManager qm = new QuestionManager(User.Identity.Name);

            //mManufacturer = "Manufacturer:" + qm.SetSKUSegment(ManufacturerID, Manufacturer);
            //mModel = "Model:" + qm.SetSKUSegment(ModelID, Model);
            //mColour = "Colour:" + qm.SetSKUSegment(ColourID, Colour);
            //SetMessage(mManufacturer + Environment.NewLine + mModel + Environment.NewLine + mColour);
        }


        void btnDeleteCommand_Click(object sender, EventArgs e)
        {
            if (txtUPCCode.Text.Length == 0) { return; }
            QuestionManager qm = new QuestionManager(User.Identity.Name);
            SetMessage(qm.DelCommandLookup(txtUPCCode.Text));
            ClearScreen();
            RefreshSelectedList();
        }

        void btnSKUClear3_Click(object sender, EventArgs e)
        {
            ClearScreen();
        }

        void ClearScreen()
        {
            txtResponceText.Text = "";
            MasterTableMessage.Text = "";
            txtUPCCode.Text = "";
            RefreshSelectedList();
        }

        void btnAddQuestion_Click(object sender, EventArgs e)
        {
            if (txtUPCCode.Text.Length == 0) { return; }
            decimal OptionID = -1;
            string SetValue = "";
            //QuestionManager qm = new QuestionManager(User.Identity.Name);
            string rOptionID = drpOption.SelectedItem.Value;
            OptionID = decimal.Parse(rOptionID);
            SetValue = txtResponceText.Text;
            AddSelectedValue(OptionID, SetValue);
        }

        void btnAddColour_Click(object sender, EventArgs e)
        {
            if (txtUPCCode.Text.Length == 0) { return; }
            decimal OptionID = -1;
            string SetValue = "";
            //QuestionManager qm = new QuestionManager(User.Identity.Name);
            string rOptionID = drpColour3.SelectedItem.Value;
            OptionID = decimal.Parse(rOptionID);
            AddSelectedValue(OptionID, SetValue);
        }

        void btnSelectModel_Click(object sender, EventArgs e)
        {
            if (txtUPCCode.Text.Length == 0) { return; }
            decimal OptionID = -1;
            string SetValue = "";
            //QuestionManager qm = new QuestionManager(User.Identity.Name);
            string rOptionID = drpModel3.SelectedItem.Value;
            OptionID = decimal.Parse(rOptionID);
            AddSelectedValue(OptionID, SetValue);
        }

        void btnSelectManufacturer_Click(object sender, EventArgs e)
        {
            if (txtUPCCode.Text.Length == 0) { return; }
            decimal OptionID = -1;
            string SetValue = "";
            //QuestionManager qm = new QuestionManager(User.Identity.Name);
            string rOptionID = drpManufacturer3.SelectedItem.Value;
            OptionID = decimal.Parse(rOptionID);
            AddSelectedValue(OptionID, SetValue);
        }

        void btnSelectCarrier_Click(object sender, EventArgs e)
        {
            if (txtUPCCode.Text.Length == 0) { return; }
            decimal OptionID = -1;
            string SetValue = "";
            //QuestionManager qm = new QuestionManager(User.Identity.Name);
            string rOptionID = drpCarrier3.SelectedItem.Value;
            OptionID = decimal.Parse(rOptionID);
            AddSelectedValue(OptionID, SetValue);
        }

        void AddSelectedValue(decimal OptionID, string SetValue)
        {
            if (txtUPCCode.Text.Length == 0) { return; }
            decimal? ScanCommandLookupID = -1;
            decimal? ScanCommandLookupAttributeID = -1;
            string Scancode = "";
            QuestionManager qm = new QuestionManager(User.Identity.Name);
            Scancode = txtUPCCode.Text;
            SetMessage(qm.AddCommandLookup(Scancode, OptionID, SetValue, ref ScanCommandLookupID, ref ScanCommandLookupAttributeID));
            RefreshSelectedList();
            //MasterTableMessage.Text = rMessage;
        }



        void btnRefresh3_Click(object sender, EventArgs e)
        {
            RefreshSelectedList();
        }
        void RefreshSelectedList()
        {
            decimal id = -1;
            //if (txtUPCCode.Text.Length == 0) { return; }
            QuestionManager qm = new QuestionManager(User.Identity.Name);
            if (id < 1) { id = qm.GetComandLookupID(txtUPCCode.Text); }
            List<vwScanComandLookupChain> xx = qm.GetComandLookupChain(id).OrderBy(x => x.QuestionSequence).ThenBy(x => x.OptionSequence).ToList();
            MainGrid.DataSource = xx;
            MainGrid.DataBind();
        }

        //void btnDelete_Click(object sender, EventArgs e)
        //{

        //    MasterTableMessage.Text = "btnDelete_Click";
        //}

        //void btnAdd_Click(object sender, EventArgs e)
        //{
        //    RefreshSelectedList();

        //    MasterTableMessage.Text = "btnAdd_Click";
        //}



        void SetMessage(string Message)
        {
            //lblMessage.Text = Message;          //Top of screen
            MasterTableMessage.Text = Message;       //Bottom of screen
        }



        #region Grid Version


        void MainGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            SetMessage("");
            System.Web.UI.WebControls.LinkButton btnDel = (System.Web.UI.WebControls.LinkButton)e.CommandSource;
            //ReceiveDetailManager rdm = null;
            decimal id = -1;
            if (decimal.TryParse(btnDel.CommandArgument, out id) == false) { id = -1; }
            string message = "";
            if (id > 0)
            {
                QuestionManager qm = new QuestionManager(User.Identity.Name);
                message = qm.DelCommandLookupAttribute(id);
                SetMessage(message);
                btnDel.Enabled = false;
                btnDel.Text = "* Deleted *";
                btnDel.ToolTip = "* Deleted *";
                btnDel.CssClass = "";

                GridViewRow gvr = (GridViewRow)(btnDel.NamingContainer);
                //GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                int RowIndex = gvr.RowIndex; 

                MainGrid.Rows[RowIndex].Cells[2].Text = "Deleted";
                MainGrid.Rows[RowIndex].Cells[3].Text = "Deleted"; 

                //(vwScanComandLookupChain)e.Row.DataItem)
                RefreshSelectedList();
                //MainGrid.DataBind();
            }



            //switch (btnDel.CommandName.ToString().ToUpper())
            //{
            //    //case "SETVERSIONTOZERO":
            //    //    rdm = new ReceiveDetailManager(User.Identity.Name);
            //    //    rdm.AdvanceESNVersion_ToZero(ctx, id);
            //    //    RefreshVersion();
            //    //    ScriptManager.RegisterStartupScript(this, GetType(), "RestHeader", "RestoreHeaderData(); SetUpScreen('');", true);
            //    //    break;
            //    //case "ADVANCEVERSIONNUMBERS":
            //    //    rdm = new ReceiveDetailManager(User.Identity.Name);
            //    //    rdm.AdvanceESNVersion_FromThisOne(ctx, id);
            //    //    RefreshVersion();
            //    //    ScriptManager.RegisterStartupScript(this, GetType(), "RestHeader", "RestoreHeaderData(); SetUpScreen('');", true);
            //    //    break;
            //    //case "DELETERECEIVEDETAILPROCESSLOG":
            //    //    rdm = new ReceiveDetailManager(User.Identity.Name);
            //    //    //rdm.DeleteReceiveDetailProcessLogThisID(id);
            //    //    RefreshVersion();
            //    //    ScriptManager.RegisterStartupScript(this, GetType(), "RestHeader", "RestoreHeaderData(); SetUpScreen('');", true);
            //    //    break;
            //    default:
            //        break;
            //}
        }
        void MainGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                System.Web.UI.WebControls.LinkButton btnDELete = (System.Web.UI.WebControls.LinkButton)e.Row.FindControl("imgDelete");
                if (btnDELete != null)
                {
                    btnDELete.Enabled = true;
                    //if (((vwScanComandLookupChain)e.Row.DataItem).ScanComandLookupID < 1)
                    //{
                    btnDELete.CommandName = "Delete";
                    btnDELete.CommandArgument = ((vwScanComandLookupChain)e.Row.DataItem).ScanComandLookupAttributeListID.ToString();
                    //}
                }
            }
        }


        #endregion


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


        //void btnSKUClear_Click(object sender, EventArgs e)
        //{
        //    //hdnCurrentIMEI.Value = "";
        //    ////lblDeviceToResku.Text = "";
        //    //ClearSKUDropDowns();
        //}


        #region  xxxx
        void setupDropDowns()
        {
            FillQuestion();


            IFS_InventoryPartsManager ipm = new IFS_InventoryPartsManager(User.Identity.Name);
            QuestionManager qm = new QuestionManager(User.Identity.Name);
            List<Option> ol = qm.GetQuestionOptionList("Carrier");
            //drpCarrier.Items.Clear();
            drpCarrier3.Items.Clear();
            foreach (Option o in ol.OrderBy(x => x.Sequence))
            {
                ListItem x = new ListItem(o.OptionText, o.OptionID.ToString());
                //drpCarrier.Items.Add(x);
                drpCarrier3.Items.Add(x);
            }

            //ol = qm.GetQuestionOptionList("DeviceHandset");
            ////drpEditDeviceHandset.Items.Clear();
            //drpAddDeviceHandset.Items.Clear();
            //foreach (Option o in ol.OrderBy(x => x.Sequence))
            //{
            //    //ListItem x = new ListItem(o.OptionText, o.OptionID.ToString());
            //    //drpEditDeviceHandset.Items.Add(x);

            //    ListItem y = new ListItem(o.OptionText, o.OptionID.ToString());
            //    drpAddDeviceHandset.Items.Add(y);
            //}

            //ol = qm.GetQuestionOptionList("Stock Colour Code");
            ////drpEditCondition.Items.Clear();
            //drpAddCondition.Items.Clear();
            //foreach (Option o in ol.OrderBy(x => x.Sequence))
            //{
            //    //ListItem x = new ListItem(o.OptionText, o.OptionID.ToString());
            //    //drpEditCondition.Items.Add(x);

            //    ListItem y = new ListItem(o.OptionText, o.OptionID.ToString());
            //    drpAddCondition.Items.Add(y);
            //}

            //ol = qm.GetQuestionOptionList("Unit OS");
            //AddUnitOS.Items.Clear();
            ////EditUnitOS.Items.Clear();
            //foreach (Option o in ol.OrderBy(x => x.Sequence))
            //{
            //    ListItem x = new ListItem(o.OptionText, o.OptionID.ToString());
            //    AddUnitOS.Items.Add(x);

            //    //ListItem y = new ListItem(o.OptionText, o.OptionID.ToString());
            //    //EditUnitOS.Items.Add(x);
            //}

            FillMMCCDropDowns3("Carrier");
            //drpCarrier.Focus();


        }

        #region Question Stuff
        void drpQuestion_SelectedIndexChanged(object sender, EventArgs e)
        {
            EstablishQuestion();
        }

        void EstablishQuestion()
        {
            txtResponceText.Visible = false;
            string bAllowResponce;
            if (drpQuestion.SelectedItem != null && drpQuestion.SelectedItem.Text.Length > 0)
            {
                txtResponceText.Visible = true;
                bAllowResponce = drpQuestion.SelectedItem.Text.Substring(drpQuestion.SelectedItem.Text.Length - 1, 1);
                if (bAllowResponce != "*") { txtResponceText.Visible = false; }
                FillOptions();
                drpOption.Focus();
            }
        }



        void FillQuestion()
        {
           string bAllowResponce = "*";
            QuestionManager qm = new QuestionManager(User.Identity.Name);
            List<Question> questions = qm.GetQuestions();
            drpQuestion.Items.Clear();
            foreach (Question Q in questions.OrderBy(x => x.Name))
            {
                bAllowResponce = "*";
                if (Q.QuestionType.Type.ToUpper() == "CHECKBOX" || Q.QuestionType.Type.ToUpper() == "DROPDOWN" || Q.QuestionType.Type.ToUpper() == "RADIALBUTTON")
                {
                    bAllowResponce = "";
                }
                ListItem x = new ListItem(string.Format("{0}: {1}{2}", Q.Name, Q.Description, bAllowResponce), Q.QuestionID.ToString());
                drpQuestion.Items.Add(x);
            }
            if (drpQuestion.Items.Count > 0) { drpQuestion.SelectedIndex = 0; }
            EstablishQuestion();
        }

        void FillOptions()
        {
            string KeyID = drpQuestion.SelectedItem.Value;
            decimal ID = -1;
            decimal.TryParse(KeyID, out ID);
            //if (ID == null) { ID = -1; }
            QuestionManager qm = new QuestionManager(User.Identity.Name);
            List<Option> options = qm.GetQuestionOptionList(ID);
            drpOption.Items.Clear();
            foreach (Option o in options.OrderBy(x => x.Name))
            {
                ListItem x = new ListItem(string.Format("{0}", o.OptionText), o.OptionID.ToString());
                drpOption.Items.Add(x);
            }
            if (drpOption.Items.Count > 0) { drpOption.SelectedIndex = 0; }
        }


        #endregion



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
        #endregion

        #endregion

        private string GetUserIPAddress()
        {
            string ip = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(ip))
            {
                ip = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            return ip;
        }

        //void TabContainer1_ActiveTabChanged(object sender, EventArgs e)
        //{
        //    pnlHome.Visible = true;
        //    if (TabContainer3.ActiveTab.ID == "TabSKUUComboSearch3") { pnlHome.Visible = false; }
        //    MasterTableMessage.Text = "ddd:" + TabContainer3.ActiveTab.ID;
        //    //FillMMCCDropDowns3("Colour");
        //}


        //void btnRefresh_Click(object sender, EventArgs e)
        //{
        //    setupDropDowns();
        //}

        //void btnValidate_Click(object sender, EventArgs e)
        //{
        //    MasterCarrierManufacturerModelColourManager mlm = new MasterCarrierManufacturerModelColourManager(User.Identity.Name);
        //    MasterCarrierManufacturerLookup ml = mlm.NewMasterCarrierManufacturerLookup();

        //    ml.Bar_Flip = "";
        //    if (rdlAddStyle.SelectedIndex == 0) { ml.Bar_Flip = "Bar"; }
        //    if (rdlAddStyle.SelectedIndex == 1) { ml.Bar_Flip = "Flip"; }

        //    ml.CDMA_HSPA = "";
        //    if (rdlAddHSType.SelectedIndex == 0) { ml.CDMA_HSPA = "CDMA"; }
        //    if (rdlAddHSType.SelectedIndex == 1) { ml.CDMA_HSPA = "HSPA"; }

        //    ml.Carrier = txtCarrier.Text;
        //    ml.Colour = txtColour.Text;

        //    ml.Condition = drpAddCondition.SelectedItem.Text;
        //    ml.Unit_OS = AddUnitOS.SelectedItem.Text;
        //    ml.Description = AddDescription.Text;
        //    ml.Device_Handset = drpAddDeviceHandset.SelectedItem.Text;
        //    ml.Manufacturer = txtManufacturer.Text;
        //    ml.Model = txtModel.Text;
        //    ml.OptionCarrierID = -1; //decimal.Parse(drpAddCarrier.SelectedItem.Value.ToString());
        //    ml.OptionColourID = -1; //decimal.Parse(drpAddColour.SelectedItem.Value.ToString());
        //    ml.OptionManufacturerID = -1; //decimal.Parse(drpAddManufacturer.SelectedItem.Value.ToString());
        //    ml.OptionModelID = -1; //decimal.Parse(drpAddModel.SelectedItem.Value.ToString());
        //    ml.Retire = "";
        //    ml.SKU = AddSKU.Text;
        //    ml.SKU_B = AddSKU_B.Text;
        //    ml.SKU_C = AddSKU_C.Text;
        //    ml.SKU_Loaner = AddSKU_Loaner.Text;
        //    ml.UPC = AddUPC.Text;
        //    ml.UPC_2 = AddUPC2.Text;
        //    ml.UPC_3 = AddUPC3.Text;
        //    ml.WarrantyStickerPlacement = AddWarrantyStickerPlacement.Text;
        //    ml.NickName = AddNickName.Text;
        //    MasterTableMessage.Text = MasterTableMessage.Text + Environment.NewLine + mlm.InsertMasterCarrierManufacturerLookup(ml);
        //    //UpdateMainGrid();
        //    //pnlMainView.Visible = true;
        //    pnlAdd.Visible = false;
        //}


        //void AddCancel_Click(object sender, EventArgs e)
        //{
        //    pnlAdd.Visible = false;
        //}

        //void btnVerify_Click(object sender, EventArgs e)
        //{
        //    List<string> response = new List<string>();
        //    MasterTableMessage.Text = "";
        //    int Sequence = 100;
        //    txtCarrier.Text = txtCarrier1.Text;
        //    txtManufacturer.Text = txtManufacturer1.Text;
        //    txtModel.Text = txtModel1.Text;
        //    txtColour.Text = txtColour1.Text;
        //    //if (AddScanKeyOption.Text.Length == 0) { return; }
        //    if (txtCarrier.Text.Length == 0) { response.Add("You must supply an Carrier before you can add."); }
        //    if (txtManufacturer.Text.Length == 0) { response.Add("You must supply a Manufacturer before you can add."); }
        //    if (txtModel.Text.Length == 0) { response.Add("You must supply an Model before you can add."); }
        //    if (txtColour.Text.Length == 0) { response.Add("You must supply a Colour before you can add."); }
        //    if (response.Count > 0) { LoadResponsesBottom(response); return; }
        //    try
        //    {
        //        MasterCarrierManufacturerModelColourManager am = new MasterCarrierManufacturerModelColourManager(User.Identity.Name);
        //        //Edit Checks
        //        response = am.EditCheckToAdd(txtCarrier.Text, txtManufacturer.Text, txtModel.Text, txtColour.Text);
        //        LoadResponsesBottom(response);
        //        if (response[0].Substring(0, 6).ToUpper() == "ERROR:")
        //        {
        //            MasterTableMessage.Text = MasterTableMessage.Text + Environment.NewLine + "RELATIONSHIP NOT ADDED!";
        //        }
        //        else
        //        {
        //            pnlAdd.Visible = true;
        //            // Physical add attempt.
        //            //response = am.AddNewAttributeSegment(rdlSegment.SelectedItem.Text, AddNameOption.Text, AddNameOption.Text, AddDescriptionOption.Text, "100");
        //            //LoadResponses(response);
        //            //if (response[0].Substring(0, 6).ToUpper() == "ERROR:")
        //            //{
        //            //    MasterTableMessage.Text = MasterTableMessage.Text + Environment.NewLine + "RELATIONSHIP NOT ADDED!";
        //            //}
        //            //else
        //            //{
        //            //    MasterTableMessage.Text = MasterTableMessage.Text + Environment.NewLine + "RELATIONSHIP ADDED!";
        //            //}
        //            // add the attribute...
        //        }
        //    }
        //    catch (UserAccessControlException ex)
        //    {
        //        //MasterTableMessage.Text = lblMessageBTM.Text + Environment.NewLine + "ATTRIBUTE SEGMENT NOT ADDED!  -- really bad error!!!!";
        //        //ScriptManager.RegisterStartupScript(this, GetType(), "Answer Add", "alert('" + ex.Message + "');", true);
        //    }
        //}

        //protected void AddOptionOK_Click(object sender, EventArgs e)
        //{
        //    lblMessageBTM.Text = "";
        //    int Sequence = 100;
        //    //if (AddScanKeyOption.Text.Length == 0) { return; }
        //    if (AddNameOption.Text.Length == 0) { lblMessageBTM.Text = "You must supply an Abbriviation before you can add."; return; }
        //    if (AddDescriptionOption.Text.Length == 0) { lblMessageBTM.Text = "You must supply a Description before you can add."; return; }
        //    //if (drpAddStatusOption.SelectedIndex < 0) { return; }
        //    //if (drpAddTypeOption.SelectedIndex < 0) { return; }
        //    //if (AddSequenceOption.Text.Length == 0 || int.TryParse(AddSequenceOption.Text, out Sequence) == false) { Sequence = 100; }
        //    try
        //    {
        //        AnswerManager am = new AnswerManager(User.Identity.Name);
        //        //Edit Checks
        //        List<string> response = am.VerifyOptionOKToAdd(rdlSegment.SelectedItem.Text, AddNameOption.Text, AddDescriptionOption.Text);
        //        LoadResponses(response);
        //        if (response[0].Substring(0, 6).ToUpper() == "ERROR:")
        //        {
        //            lblMessageBTM.Text = lblMessageBTM.Text + Environment.NewLine + "ATTRIBUTE SEGMENT NOT ADDED!";
        //        }
        //        else
        //        {
        //            // Physical add attempt.
        //            response = am.AddNewAttributeSegment(rdlSegment.SelectedItem.Text, AddNameOption.Text, AddNameOption.Text, AddDescriptionOption.Text, "100");
        //            LoadResponses(response);
        //            if (response[0].Substring(0, 6).ToUpper() == "ERROR:")
        //            {
        //                lblMessageBTM.Text = lblMessageBTM.Text + Environment.NewLine + "ATTRIBUTE SEGMENT NOT ADDED!";
        //            }
        //            else
        //            {
        //                lblMessageBTM.Text = lblMessageBTM.Text + Environment.NewLine + "ATTRIBUTE SEGMENT ADDED!";
        //            }
        //            // add the attribute...
        //        }
        //    }
        //    catch (UserAccessControlException ex)
        //    {
        //        lblMessageBTM.Text = lblMessageBTM.Text + Environment.NewLine + "ATTRIBUTE SEGMENT NOT ADDED!  -- really bad error!!!!";
        //        //ScriptManager.RegisterStartupScript(this, GetType(), "Answer Add", "alert('" + ex.Message + "');", true);
        //    }
        //}

        //private void LoadResponses(List<string> response)
        //{
        //    foreach (string s in response)
        //    {
        //        lblMessageBTM.Text = lblMessageBTM.Text + Environment.NewLine + s;
        //    }
        //}
        //private void LoadResponsesBottom(List<string> response)
        //{
        //    foreach (string s in response)
        //    {
        //        MasterTableMessage.Text = MasterTableMessage.Text + Environment.NewLine + s;
        //    }
        //}






        //void MoveDownCombo_Click(object sender, EventArgs e)
        //{
        //    //txtCarrier.Text = lblCarrierABBR.Text.ToUpper();
        //    //txtManufacturer.Text = lblManufacturerABBR.Text.ToUpper();
        //    //txtModel.Text = lblModelABBR.Text.ToUpper();
        //    //txtColour.Text = lblColourABBR.Text.ToUpper();

        //    //txtCarrier1.Text = lblCarrierABBR.Text.ToUpper();
        //    //txtManufacturer1.Text = lblManufacturerABBR.Text.ToUpper();
        //    //txtModel1.Text = lblModelABBR.Text.ToUpper();
        //    //txtColour1.Text = lblColourABBR.Text.ToUpper();


        //    //lblCarrier.Text = hdnCarrierTEXT.Value;
        //    //lblManufacturer.Text = hdnManufacturerTEXT.Value;
        //    //lblModel.Text = hdnModelTEXT.Value;
        //    //lblColour.Text = hdnColourTEXT.Value;
        //}

        //void MoveDownQuestion_Click(object sender, EventArgs e)
        //{
        //    if (rdlSegment.SelectedItem.Text == "Carrier")
        //    {
        //        //txtCarrier.Text = AddNameOption.Text.ToUpper();
        //        txtCarrier1.Text = AddNameOption.Text.ToUpper();
        //        lblCarrier.Text = AddDescriptionOption.Text;
        //    }
        //    if (rdlSegment.SelectedItem.Text == "Manufacturer")
        //    {
        //        //txtManufacturer.Text = AddNameOption.Text.ToUpper();
        //        txtManufacturer1.Text = AddNameOption.Text.ToUpper();
        //        lblManufacturer.Text = AddDescriptionOption.Text;
        //    }

        //    if (rdlSegment.SelectedItem.Text == "Model")
        //    {
        //        //txtModel.Text = AddNameOption.Text.ToUpper();
        //        txtModel1.Text = AddNameOption.Text.ToUpper();
        //        lblModel.Text = AddDescriptionOption.Text;
        //    }

        //    if (rdlSegment.SelectedItem.Text == "Colour")
        //    {
        //        //txtColour.Text = AddNameOption.Text.ToUpper();
        //        txtColour1.Text = AddNameOption.Text.ToUpper();
        //        lblColour.Text = AddDescriptionOption.Text;
        //    }
        //}

        //protected void TabSegments_ActiveTabChanged(object sender, EventArgs e)
        //{
        //    pnlAddOption.Visible = true;
        //    if (TabSegmentsx.ActiveTab.ID == "TabCombo") { pnlAddOption.Visible = false; }
        //    if (TabSegmentsx.ActiveTab.ID == "TabCarrier") { AddOptionOK.Text = "Add Carrier Segment"; }
        //    if (TabSegmentsx.ActiveTab.ID == "TabManufacturer") { AddOptionOK.Text = "Add Manufacturer Segment"; }
        //    if (TabSegmentsx.ActiveTab.ID == "TabModel") { AddOptionOK.Text = "Add Model Segment"; }
        //    if (TabSegmentsx.ActiveTab.ID == "TabColour") { AddOptionOK.Text = "Add Colour Segment"; }
        //}


    }
}