using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using BW_WebApp.DataManagers;


namespace BW_WebApp
{
    public partial class CC_Control_BKUP : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            bRefreshBatches.Click += new EventHandler(bRefreshBatches_Click);

            TabBatches.ActiveTabChanged += new EventHandler(TabBatches_ActiveTabChanged);
            TabRuns.ActiveTabChanged += new EventHandler(TabRuns_ActiveTabChanged);
            grdBatches.RowDataBound += new GridViewRowEventHandler(grdBatches_RowDataBound);
            grdBatches.RowCommand += new GridViewCommandEventHandler(grdBatches_RowCommand);
            //-------------------------------------------
            btnAddTemplate.Click += new EventHandler(btnAddTemplate_Click);
            btnRefreshAddEditUpdate.Click += new EventHandler(btnRefreshAddEditUpdate_Click);
            btnAddTemplateClear.Click += new EventHandler(btnAddTemplateClear_Click);



            //btnPasteParseDel.Click += new EventHandler(btnPasteParseDel_Click);
            //btnPasteParseAdd.Click += new EventHandler(btnPasteParseAdd_Click);

            btnRefreshTemplateHeaders.Click += new EventHandler(btnRefreshTemplateHeaders_Click);
            grdTemplateHeaders.RowDataBound += new GridViewRowEventHandler(grdTemplateHeaders_RowDataBound);
            grdTemplateHeaders.RowCommand += new GridViewCommandEventHandler(grdTemplateHeaders_RowCommand);

            TabRunControlDetail.ActiveTabChanged += new EventHandler(TabRunControlDetail_ActiveTabChanged);
            bRefreshBatchesControlDetail.Click += new EventHandler(bRefreshBatchesControlDetail_Click);
            gvRunControlDetail.RowDataBound += new GridViewRowEventHandler(gvRunControlDetail_RowDataBound);
            gvRunControlDetail.RowCommand += new GridViewCommandEventHandler(gvRunControlDetail_RowCommand);


            TabRunScanDetail.ActiveTabChanged += new EventHandler(TabRunScanDetail_ActiveTabChanged);
            bRefreshBatchesScanDetail.Click += new EventHandler(bRefreshBatchesScanDetail_Click);
            gvRunScanDetail.RowDataBound += new GridViewRowEventHandler(gvRunScanDetail_RowDataBound);
            gvRunScanDetail.RowCommand += new GridViewCommandEventHandler(gvRunScanDetail_RowCommand);



            btnRefreshTemplateHeadersInactive.Click += new EventHandler(btnRefreshTemplateHeadersInactive_Click);
            grdTemplateHeadersInactive.RowDataBound += new GridViewRowEventHandler(grdTemplateHeadersInactive_RowDataBound);
            grdTemplateHeadersInactive.RowCommand += new GridViewCommandEventHandler(grdTemplateHeadersInactive_RowCommand);

            gvRunSpread.RowDataBound += new GridViewRowEventHandler(gvRunSpread_RowDataBound);
            gvRunSpread.RowCommand += new GridViewCommandEventHandler(gvRunSpread_RowCommand);
            gvRunSpread.SelectedIndexChanged += new EventHandler(gvRunSpread_SelectedIndexChanged);
            btnRefreshHeadersNew.Click += new EventHandler(btnRefreshHeadersNew_Click);
            grdRunsNew.RowDataBound += new GridViewRowEventHandler(grdRunsNew_RowDataBound);
            grdRunsNew.RowCommand += new GridViewCommandEventHandler(grdRunsNew_RowCommand);
            imgHeaderRunNewOpenBatchListBack.Click += new ImageClickEventHandler(imgHeaderRunNewOpenBatchListBack_Click);
            imgHeaderRunNewOpenControlListBack.Click += new ImageClickEventHandler(imgHeaderRunNewOpenControlListBack_Click);
            imgHeaderRunNewOpenScanListBack.Click += new ImageClickEventHandler(imgHeaderRunNewOpenScanListBack_Click);



            ////btnRefreshHeadersActive.Click += new EventHandler(btnRefreshHeadersActive_Click);
            ////grdRunsActive.RowDataBound += new GridViewRowEventHandler(grdRunsActive_RowDataBound);
            ////grdRunsActive.RowCommand += new GridViewCommandEventHandler(grdRunsActive_RowCommand);
            ////btnRefreshHeadersClosed.Click += new EventHandler(btnRefreshHeadersClosed_Click);
            ////grdRunsClosed.RowDataBound += new GridViewRowEventHandler(grdRunsClosed_RowDataBound);
            ////grdRunsClosed.RowCommand += new GridViewCommandEventHandler(grdRunsClosed_RowCommand);
            ////btnRefreshHeadersInactive.Click += new EventHandler(btnRefreshHeadersInactive_Click);
            ////grdRunsInactive.RowDataBound += new GridViewRowEventHandler(grdRunsInactive_RowDataBound);
            ////grdRunsInactive.RowCommand += new GridViewCommandEventHandler(grdRunsInactive_RowCommand);



            //////GRDLockedBatches.RowDataBound += new GridViewRowEventHandler(GRDLockedBatches_RowDataBound);
            //////GRDLockedBatches.RowCommand += new GridViewCommandEventHandler(GRDLockedBatches_RowCommand);

            imgDownloadGrid.Click += new ImageClickEventHandler(imgDownloadGrid_Click);
            //////GRDInvalidBatches.RowDataBound += new GridViewRowEventHandler(GRDInvalidBatches_RowDataBound);
            //////GRDInvalidBatches.RowCommand += new GridViewCommandEventHandler(GRDInvalidBatches_RowCommand);

            //////GRDHoldBatches.RowCommand += new GridViewCommandEventHandler(GRDHoldBatches_RowCommand);
            //////GRDHoldBatches.RowDataBound += new GridViewRowEventHandler(GRDHoldBatches_RowDataBound);

            //////GRDOpenBatches.RowDataBound += new GridViewRowEventHandler(GRDOpenBatches_RowDataBound);
            //////GRDOpenBatches.RowCommand += new GridViewCommandEventHandler(GRDOpenBatches_RowCommand);

            //////GRDSentIFSBatches.RowDataBound += new GridViewRowEventHandler(GRDSentIFSBatches_RowDataBound);
            //////GRDSentIFSBatches.RowCommand += new GridViewCommandEventHandler(GRDSentIFSBatches_RowCommand);

            //////btnRefreshLocked.Click += new EventHandler(btnRefreshLocked_Click);
            //////btnRefreshHold.Click += new EventHandler(btnRefreshHold_Click);
            //////btnRefreshInvalid.Click += new EventHandler(btnRefreshInvalid_Click);
            //////btnRefreshOpen.Click += new EventHandler(btnRefreshOpen_Click);
            //////btnRefreshSentIFS.Click += new EventHandler(btnRefreshSentIFS_Click);
            if (IsPostBack == false)
            {
                LoadDropdowns();
                hdnUserName.Value = User.Identity.Name;
                //UpdateLockedGrid();
                UpdateTemplateGrid();
                UpdategrdRuns();
                lblHeaderRunMessageNew.Text = "Run Cycles";
                //UpdateTemplateGridInactive();
            }

        }


        #region CycleCounts
        #region Templates
        #region Inactive Templates
        void btnRefreshTemplateHeadersInactive_Click(object sender, EventArgs e)
        {
            UpdateTemplateGridInactive();
        }
        private void UpdateTemplateGridInactive()
        {

            string QueryString = "";
            SetMainMessage("");
            CycleCountManager im = new CycleCountManager(User.Identity.Name);
            grdTemplateHeadersInactive.DataSource = im.TemplateGridData_Inactive(ref QueryString);
            grdTemplateHeadersInactive.DataBind();
            hdnReportThisData.Value = QueryString;
        }
        void grdTemplateHeadersInactive_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            SetMainMessage("");
            if (e.CommandName != "Page")
            {
                ImageButton btnOpen = (ImageButton)e.CommandSource;
                //string CommandArgument = btnOpen.CommandArgument;
                #region Invalidate
                if (btnOpen.ID.ToUpper() == "IMGACTIVATE")
                {
                    string Message = "";
                    decimal ID = -1;
                    if (decimal.TryParse(btnOpen.CommandArgument, out ID) == false) { Message = "Invalid ID."; return; }
                    CycleCountManager ccm = new CycleCountManager(User.Identity.Name);
                    ccm.SetTemplateStatus_Active(ID, ref Message);
                    lblHeaderTemplateMessage.Text = Message;
                    UpdateTemplateGridInactive();
                }
                #endregion
                #region Download
                if (btnOpen.ID.ToUpper() == "IMGDOWNLOADLOCATIONS")
                {
                    string Message = "";
                    decimal ID = -1;
                    if (decimal.TryParse(btnOpen.CommandArgument, out ID) == false)
                    {
                        Message = "Invalid ID.";
                        lblHeaderTemplateMessage.Text = Message;
                        lblHeaderTemplateMessage.Visible = true;
                        return;
                    }
                    ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "PrintReport('CCTEMPLATE','" + btnOpen.CommandArgument + "');", true);
                }
                #endregion
                #region Open
                //if (btnOpen.ID.ToUpper() == "IMGOPEN")
                //{
                //    IFS_InventoryManager im = new IFS_InventoryManager(User.Identity.Name);
                //    lblMessageLock.Text = im.LogPhysicalInventoryBatchOpen(btnOpen.CommandArgument);
                //    UpdateLockedGrid();
                //}
                //#endregion
                //#region Clean
                //if (btnOpen.ID.ToUpper() == "IMGCLEAN")
                //{
                //    IFS_InventoryManager im = new IFS_InventoryManager(User.Identity.Name);
                //    lblMessageLock.Text = im.LogPhysicalInventoryBatchClean(btnOpen.CommandArgument);
                //    UpdateLockedGrid();
                //}

                //#endregion
                //#region Hold
                //if (btnOpen.ID.ToUpper() == "IMGHOLD")
                //{
                //    IFS_InventoryManager im = new IFS_InventoryManager(User.Identity.Name);
                //    lblMessageLock.Text = im.LogPhysicalInventoryBatchHold(btnOpen.CommandArgument);
                //    UpdateLockedGrid();
                //}
                #endregion
            }
        }
        void grdTemplateHeadersInactive_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                vwGetCCTemplateHeader data = (vwGetCCTemplateHeader)e.Row.DataItem;
                ImageButton bInvalidate = (ImageButton)e.Row.FindControl("imgActivate");
                ImageButton bDownload = (ImageButton)e.Row.FindControl("imgDownLoadLocations");
                if (bDownload != null) { bDownload.CommandArgument = "-1"; }
                if (bInvalidate != null) { bInvalidate.CommandArgument = "-1"; }
                if (data != null)
                {
                    if (bInvalidate != null) { bInvalidate.CommandArgument = data.CycleInventoryCountTemplateHeaderID.ToString(); }
                    if (bDownload != null) { bDownload.CommandArgument = data.CycleInventoryCountTemplateHeaderID.ToString(); }
                }
            }
        }
        #endregion
        #region Active Templates
        void btnRefreshTemplateHeaders_Click(object sender, EventArgs e)
        {
            UpdateTemplateGrid();
        }
        private void UpdateTemplateGrid()
        {
            string QueryString = "";
            SetMainMessage("");
            CycleCountManager im = new CycleCountManager(User.Identity.Name);
            grdTemplateHeaders.DataSource = im.TemplateGridData_Active(ref QueryString);
            grdTemplateHeaders.DataBind();
            hdnReportThisData.Value = QueryString;
        }
        void grdTemplateHeaders_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            SetMainMessage("");
            if (e.CommandName != "Page")
            {
                ImageButton btnOpen = (ImageButton)e.CommandSource;
                //string CommandArgument = btnOpen.CommandArgument;
                #region Invalidate
                if (btnOpen.ID.ToUpper() == "IMGINACTIVATE")
                {
                    string Message = "";
                    decimal ID = -1;
                    if (decimal.TryParse(btnOpen.CommandArgument, out ID) == false) { Message = "Invalid ID."; return; }
                    CycleCountManager ccm = new CycleCountManager(User.Identity.Name);
                    ccm.SetTemplateStatus_Inactive(ID, ref Message);
                    lblHeaderTemplateMessage.Text = Message;
                    UpdateTemplateGrid();
                }
                #endregion
                #region Generate Run Data
                if (btnOpen.ID.ToUpper() == "IMGLOADRUNDATA")
                {
                    string Message = "";
                    decimal ID = -1;
                    if (decimal.TryParse(btnOpen.CommandArgument, out ID) == false)
                    {
                        Message = "Invalid ID.";
                        lblHeaderTemplateMessage.Text = Message;
                        lblHeaderTemplateMessage.Visible = true;
                        return;
                    }
                    CycleCountManager ccm = new CycleCountManager(User.Identity.Name);
                    ccm.GenerateTemplateCycle(ID, ref Message);
                    lblHeaderTemplateMessage.Text = Message;
                    UpdateTemplateGrid();
                }
                #endregion
                #region Download
                if (btnOpen.ID.ToUpper() == "IMGDOWNLOADTEMPLATESUMMARY")
                {
                    string Message = "";
                    decimal ID = -1;
                    if (decimal.TryParse(btnOpen.CommandArgument, out ID) == false)
                    {
                        Message = "Invalid ID.";
                        lblHeaderTemplateMessage.Text = Message;
                        lblHeaderTemplateMessage.Visible = true;
                        return;
                    }
                    ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "PrintReport('CCTEMPLATESUMMARY','" + btnOpen.CommandArgument + "');", true);
                }
                if (btnOpen.ID.ToUpper() == "IMGDOWNLOADTEMPLATESUMMARYDETAIL")
                {
                    string Message = "";
                    decimal ID = -1;
                    if (decimal.TryParse(btnOpen.CommandArgument, out ID) == false)
                    {
                        Message = "Invalid ID.";
                        lblHeaderTemplateMessage.Text = Message;
                        lblHeaderTemplateMessage.Visible = true;
                        return;
                    }
                    ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "PrintReport('CCTEMPLATESUMMARYDETAIL','" + btnOpen.CommandArgument + "');", true);
                }

                if (btnOpen.ID.ToUpper() == "IMGDOWNLOADTEMPLATEDETAIL")
                {
                    string Message = "";
                    decimal ID = -1;
                    if (decimal.TryParse(btnOpen.CommandArgument, out ID) == false)
                    {
                        Message = "Invalid ID.";
                        lblHeaderTemplateMessage.Text = Message;
                        lblHeaderTemplateMessage.Visible = true;
                        return;
                    }
                    ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "PrintReport('CCTEMPLATEDETAIL','" + btnOpen.CommandArgument + "');", true);
                }
                if (btnOpen.ID.ToUpper() == "IMAGEEDIT")
                {
                    string Message = "";
                    decimal ID = -1;
                    if (decimal.TryParse(btnOpen.CommandArgument, out ID) == false)
                    {
                        Message = "Invalid ID.";
                        lblHeaderTemplateMessage.Text = Message;
                        lblHeaderTemplateMessage.Visible = true;
                        return;
                    }
                    txtTemplateName.Text = btnOpen.CommandName;
                    UpdateTemplateEditPage();
                    TabContainer2.ActiveTabIndex = 2;// TabDIDMFromSite.TabIndex;
                }



                #endregion
                #region Open
                //if (btnOpen.ID.ToUpper() == "IMGOPEN")
                //{
                //    IFS_InventoryManager im = new IFS_InventoryManager(User.Identity.Name);
                //    lblMessageLock.Text = im.LogPhysicalInventoryBatchOpen(btnOpen.CommandArgument);
                //    UpdateLockedGrid();
                //}
                //#endregion
                //#region Clean
                //if (btnOpen.ID.ToUpper() == "IMGCLEAN")
                //{
                //    IFS_InventoryManager im = new IFS_InventoryManager(User.Identity.Name);
                //    lblMessageLock.Text = im.LogPhysicalInventoryBatchClean(btnOpen.CommandArgument);
                //    UpdateLockedGrid();
                //}

                //#endregion
                //#region Hold
                //if (btnOpen.ID.ToUpper() == "IMGHOLD")
                //{
                //    IFS_InventoryManager im = new IFS_InventoryManager(User.Identity.Name);
                //    lblMessageLock.Text = im.LogPhysicalInventoryBatchHold(btnOpen.CommandArgument);
                //    UpdateLockedGrid();
                //}
                #endregion
            }
        }
        void grdTemplateHeaders_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                vwGetCCTemplateHeader data = (vwGetCCTemplateHeader)e.Row.DataItem;
                ImageButton bInvalidate = (ImageButton)e.Row.FindControl("imgInactivate");
                ImageButton bGenerateRunCycle = (ImageButton)e.Row.FindControl("imgloadRunData");
                ImageButton bDownload = (ImageButton)e.Row.FindControl("imgDownLoadTemplateSummary");

                ImageButton bDownLoadTemplateSummaryDetail = (ImageButton)e.Row.FindControl("imgDownLoadTemplateSummaryDetail");
                ImageButton bDownLoadTemplateDetail = (ImageButton)e.Row.FindControl("imgDownLoadTemplateDetail");

                ImageButton bImageEdit = (ImageButton)e.Row.FindControl("ImageEdit");

                //ImageButton bClean = (ImageButton)e.Row.FindControl("imgClean");
                //ImageButton bHold = (ImageButton)e.Row.FindControl("imgHold");

                if (bInvalidate != null) { bInvalidate.CommandArgument = "-1"; }
                if (bGenerateRunCycle != null) { bGenerateRunCycle.CommandArgument = "-1"; }
                if (bDownload != null) { bDownload.CommandArgument = "-1"; }
                if (bDownLoadTemplateSummaryDetail != null) { bDownLoadTemplateSummaryDetail.CommandArgument = "-1"; }
                if (bDownload != null) { bDownload.CommandArgument = "-1"; }
                if (bImageEdit != null) { bImageEdit.CommandArgument = "-1"; }
                //if (bClean != null) { bClean.CommandArgument = "-1"; }
                //if (bHold != null) { bHold.CommandArgument = "-1"; }
                if (data != null)
                {
                    if (bInvalidate != null) { bInvalidate.CommandArgument = data.CycleInventoryCountTemplateHeaderID.ToString(); }
                    if (bGenerateRunCycle != null) { bGenerateRunCycle.CommandArgument = data.CycleInventoryCountTemplateHeaderID.ToString(); }
                    if (bDownload != null) { bDownload.CommandArgument = data.CycleInventoryCountTemplateHeaderID.ToString(); }
                    if (bDownLoadTemplateSummaryDetail != null) { bDownLoadTemplateSummaryDetail.CommandArgument = data.CycleInventoryCountTemplateHeaderID.ToString(); }
                    if (bDownLoadTemplateDetail != null) { bDownLoadTemplateDetail.CommandArgument = data.CycleInventoryCountTemplateHeaderID.ToString(); }
                    if (bImageEdit != null) { bImageEdit.CommandArgument = data.CycleInventoryCountTemplateHeaderID.ToString(); bImageEdit.CommandName = data.Name; }
                    //if (bClean != null) { bClean.CommandArgument = data.Batch; }
                    //if (bHold != null) { bHold.CommandArgument = data.Batch; }

                }
            }
        }
        #endregion
        #endregion


        #region Runs - New
        void TabRuns_ActiveTabChanged(object sender, EventArgs e)
        {
            SetMainMessage("");
            UpdategrdRuns();
        }
        void imgHeaderRunNewOpenBatchListBack_Click(object sender, ImageClickEventArgs e)
        {
            pnlGridViewRunNewOpenBatchList.Visible = false;
            pnlGridViewRunNew.Visible = true;
        }
        void btnRefreshHeadersNew_Click(object sender, EventArgs e)
        {
            UpdategrdRuns();
        }
        private void UpdategrdRuns()
        {
            string QueryString = "";
            pnlGridViewRunNew.Visible = false;
            pnlGridViewRunNewOpenBatchList.Visible = false;
            pnlGridViewBatchConroleList.Visible = false;
            pnlGridViewBatchScanList.Visible = false;
            pnlGridViewRunSpread.Visible = false;

            string ActiveTab = TabRuns.ActiveTab.HeaderText.ToUpper();
            if (ActiveTab.Length > 2 && ActiveTab.Substring(0, 3).ToUpper() == "NEW")
            {
                pnlGridViewRunNew.Visible = true;
                CycleCountManager im = new CycleCountManager(User.Identity.Name);
                grdRunsNew.DataSource = im.GridDataRun(TabRuns.ActiveTab.HeaderText, ref QueryString).OrderBy(x => x.CycleInventoryCountHeaderID);
                grdRunsNew.DataBind();
            }
            if (ActiveTab.Length > 5 && ActiveTab.Substring(0, 6).ToUpper() == "SPREAD")
            {
                pnlGridViewRunSpread.Visible = true;
                //string QueryString = "";
                decimal ID = -1;
                if (decimal.TryParse(hdnCycleInventoryCountHeaderID.Value, out ID) == false) { ID = -1; }

                if (grdRunsNew.SelectedDataKey != null && decimal.TryParse(grdRunsNew.SelectedDataKey.Value.ToString(), out ID) == true)
                {
                    //TabRunsNew.HeaderText = "New (" + ID.ToString() + ")";
                    CycleCountManager im = new CycleCountManager(User.Identity.Name);
                    gvRunSpread.DataSource = im.BatchGridData(ID, ref QueryString).OrderBy(x => x.IFSLocation);
                    gvRunSpread.DataBind();
                }

            }
            //if (TabRuns.ActiveTab.HeaderText.ToUpper() == "ACTIVE")
            //{
            //    pnlGridViewBatchConroleList.Visible = true;
            //    //string QueryString = "";
            //    decimal ID = -1;
            //    if (decimal.TryParse(hdnCycleInventoryCountHeaderID.Value, out ID) == false) { ID = -1; }

            //    ID = 22;

            //    CycleCountManager im = new CycleCountManager(User.Identity.Name);
            //    grdBatches.DataSource = im.BatchGridData(ID, TabBatches.ActiveTab.HeaderText, ref QueryString).OrderBy(x => x.IFSLocation);
            //    grdBatches.DataBind();
            //}
            //if (TabRuns.ActiveTab.HeaderText.ToUpper() == "HOLD")
            //{
            //    //pnlGridViewBatchScanList.Visible = false;
            //    //CycleCountManager im = new CycleCountManager(User.Identity.Name);
            //    //grdRunsNew.DataSource = im.GridDataRun(TabRuns.ActiveTab.HeaderText, ref QueryString).OrderBy(x => x.CycleInventoryCountHeaderID);
            //    //grdRunsNew.DataBind();
            //}
            //if (TabRuns.ActiveTab.HeaderText.ToUpper() == "CLOSED")
            //{
            //    //pnlGridViewRunNewOpenBatchList.Visible = false;
            //    //CycleCountManager im = new CycleCountManager(User.Identity.Name);
            //    //grdRunsNew.DataSource = im.GridDataRun(TabRuns.ActiveTab.HeaderText, ref QueryString).OrderBy(x => x.CycleInventoryCountHeaderID);
            //    //grdRunsNew.DataBind();
            //}
            //if (TabRuns.ActiveTab.HeaderText.ToUpper() == "INACTIVE")
            //{
            //    //pnlGridViewRunNew.Visible = true;
            //    //CycleCountManager im = new CycleCountManager(User.Identity.Name);
            //    //grdRunsNew.DataSource = im.GridDataRun(TabRuns.ActiveTab.HeaderText, ref QueryString).OrderBy(x => x.CycleInventoryCountHeaderID);
            //    //grdRunsNew.DataBind();
            //}
            hdnReportThisData.Value = QueryString;
        }



        void grdRunsNew_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            SetMainMessage("");
            if (e.CommandName == "Select")
            {
                if (grdRunsNew.SelectedIndex >= 0)
                {
                    //decimal ID = 
                    //vwGetCCRunHeaderBatch Data = (vwGetCCRunHeaderBatch) gvRunSpread.SelectedRow.DataItem;
                    //SpreadTabData.HeaderText = "Data - (" + Data.CycleInventoryCountHeaderID + ")" + Data.Name;
                    TabRunsNew.HeaderText = "New - (" + grdRunsNew.SelectedDataKey.Value + ")" + "";
                }
            }
            else if (e.CommandName != "Page")
            {
                ImageButton btnOpen = (ImageButton)e.CommandSource;
                #region Download
                if (btnOpen.ID.ToUpper() == "IMGDOWNLOADTEMPLATESUMMARY")
                {
                    string Message = "";
                    decimal ID = -1;
                    if (decimal.TryParse(btnOpen.CommandArgument, out ID) == false)
                    {
                        Message = "Invalid ID.";
                        SetMainMessage(Message);
                        return;
                    }
                    //ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "PrintReport('CCTEMPLATESUMMARY','" + btnOpen.CommandArgument + "');", true);
                }

                //if (btnOpen.ID.ToUpper() == "IMGDOWNLOADTEMPLATESUMMARYDETAIL")
                //{
                //    string Message = "";
                //    decimal ID = -1;
                //    if (decimal.TryParse(btnOpen.CommandArgument, out ID) == false)
                //    {
                //        Message = "Invalid ID.";
                //        SetMainMessage(Message);
                //        return;
                //    }
                //    //ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "PrintReport('CCTEMPLATESUMMARYDETAIL','" + btnOpen.CommandArgument + "');", true);
                //}
                //if (btnOpen.ID.ToUpper() == "IMGDOWNLOADTEMPLATEDETAIL")
                //{
                //    string Message = "";
                //    decimal ID = -1;
                //    if (decimal.TryParse(btnOpen.CommandArgument, out ID) == false)
                //    {
                //        Message = "Invalid ID.";
                //        SetMainMessage(Message);
                //        return;
                //    }
                //    //ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "PrintReport('CCTEMPLATEDETAIL','" + btnOpen.CommandArgument + "');", true);
                //}






                #endregion
                #region Open Batch List
                if (btnOpen.ID.ToUpper() == "IMGOPENBATCHLIST")
                {
                    string Message = "";
                    decimal ID = -1;
                    if (decimal.TryParse(btnOpen.CommandArgument, out ID) == false)
                    {
                        Message = "Invalid ID.";
                        SetMainMessage(Message);
                        return;
                    }
                    hdnCycleInventoryCountHeaderID.Value = btnOpen.CommandArgument;
                    lblHeaderRunNewOpenBatchList.Text = "Batch List:" + btnOpen.CommandName;
                    pnlGridViewRunNewOpenBatchList.Visible = true;
                    pnlGridViewRunNew.Visible = false;
                    UpdateBatchGrid();
                    //ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "PrintReport('CCRUNHEADER','" + btnOpen.CommandArgument + "');", true);
                }
                #endregion
                //------------------------------------------
                #region Move To Active
                if (btnOpen.ID.ToUpper() == "IMGACTIVATE")
                {
                    string Message = "";
                    decimal ID = -1;
                    if (decimal.TryParse(btnOpen.CommandArgument, out ID) == false) { Message = "Invalid ID."; return; }
                    CycleCountManager ccm = new CycleCountManager(User.Identity.Name);
                    ccm.SetRunStatus(ID, "Active", ref Message);
                    SetMainMessage(Message);
                    UpdategrdRuns();
                }
                #endregion


                #region Move To Hold
                if (btnOpen.ID.ToUpper() == "IMGHOLD")
                {
                    string Message = "";
                    decimal ID = -1;
                    if (decimal.TryParse(btnOpen.CommandArgument, out ID) == false) { Message = "Invalid ID."; return; }
                    CycleCountManager ccm = new CycleCountManager(User.Identity.Name);
                    ccm.SetRunStatus(ID, "Hold", ref Message);
                    SetMainMessage(Message);
                    UpdategrdRuns();
                }
                #endregion
                #region Move To New
                if (btnOpen.ID.ToUpper() == "IMGNEW")
                {
                    string Message = "";
                    decimal ID = -1;
                    if (decimal.TryParse(btnOpen.CommandArgument, out ID) == false) { Message = "Invalid ID."; return; }
                    CycleCountManager ccm = new CycleCountManager(User.Identity.Name);
                    ccm.SetRunStatus(ID, "New", ref Message);
                    SetMainMessage(Message);
                    UpdategrdRuns();
                }
                #endregion
                #region Move To Active
                if (btnOpen.ID.ToUpper() == "IMGCLOSE")
                {
                    string Message = "";
                    decimal ID = -1;
                    if (decimal.TryParse(btnOpen.CommandArgument, out ID) == false) { Message = "Invalid ID."; return; }
                    CycleCountManager ccm = new CycleCountManager(User.Identity.Name);
                    ccm.SetRunStatus(ID, "Closed", ref Message);
                    SetMainMessage(Message);
                    UpdategrdRuns();
                }
                #endregion
                #region Move To Active
                if (btnOpen.ID.ToUpper() == "IMGINACTIVATE")
                {
                    string Message = "";
                    decimal ID = -1;
                    if (decimal.TryParse(btnOpen.CommandArgument, out ID) == false) { Message = "Invalid ID."; return; }
                    CycleCountManager ccm = new CycleCountManager(User.Identity.Name);
                    ccm.SetRunStatus(ID, "Inactive", ref Message);
                    SetMainMessage(Message);
                    UpdategrdRuns();
                }
                #endregion
            }
        }
        void grdRunsNew_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vwGetCCRunHeader data = (vwGetCCRunHeader)e.Row.DataItem;
                ImageButton bDownload = (ImageButton)e.Row.FindControl("imgDownLoadTemplateSummary");
                //ImageButton bDownloadDetailSummary = (ImageButton)e.Row.FindControl("imgDownLoadTemplateSummaryDetail");
                //ImageButton bDownloadDetail = (ImageButton)e.Row.FindControl("imgDownLoadTemplateDetail");
                ImageButton bOPenBatchList = (ImageButton)e.Row.FindControl("imgOpenBatchList");
                ImageButton bActivate = (ImageButton)e.Row.FindControl("imgactivate");
                ImageButton bInActivate = (ImageButton)e.Row.FindControl("imgInactivate");
                ImageButton bCLosedActivate = (ImageButton)e.Row.FindControl("imgClose");
                ImageButton bHold = (ImageButton)e.Row.FindControl("imgHold");
                ImageButton bNew = (ImageButton)e.Row.FindControl("imgNew");
                //ImageButton bEdit = (ImageButton)e.Row.FindControl("ImageEdit");




                if (bDownload != null) { bDownload.CommandArgument = "-1"; bDownload.Visible = false; }
                //if (bDownloadDetailSummary != null) { bDownloadDetailSummary.CommandArgument = "-1"; bDownloadDetailSummary.Visible = false; }
                //if (bDownloadDetail != null) { bDownloadDetail.CommandArgument = "-1"; bDownloadDetail.Visible = false; }
                if (bOPenBatchList != null) { bOPenBatchList.CommandArgument = "-1"; bOPenBatchList.Visible = false; }
                if (bActivate != null) { bActivate.CommandArgument = "-1"; bActivate.Visible = false; }
                if (bInActivate != null) { bInActivate.CommandArgument = "-1"; bInActivate.Visible = false; }
                if (bCLosedActivate != null) { bCLosedActivate.CommandArgument = "-1"; bCLosedActivate.Visible = false; }
                if (bHold != null) { bHold.CommandArgument = "-1"; bHold.Visible = false; }
                if (bNew != null) { bNew.CommandArgument = "-1"; bNew.Visible = false; }
                //if (bEdit != null) { bEdit.CommandArgument = "-1"; bEdit.Visible = false; }


                if (data != null)
                {
                    if (bActivate != null) { bActivate.CommandArgument = data.CycleInventoryCountHeaderID.ToString(); bActivate.CommandName = "Run:" + data.CycleInventoryCountHeaderID.ToString() + " Name:" + data.Name + "(" + data.Status + ")"; }
                    if (bInActivate != null) { bInActivate.CommandArgument = data.CycleInventoryCountHeaderID.ToString(); bInActivate.CommandName = "Run:" + data.CycleInventoryCountHeaderID.ToString() + " Name:" + data.Name + "(" + data.Status + ")"; }
                    if (bCLosedActivate != null) { bCLosedActivate.CommandArgument = data.CycleInventoryCountHeaderID.ToString(); bCLosedActivate.CommandName = "Run:" + data.CycleInventoryCountHeaderID.ToString() + " Name:" + data.Name + "(" + data.Status + ")"; }
                    if (bDownload != null) { bDownload.CommandArgument = data.CycleInventoryCountHeaderID.ToString(); bDownload.CommandName = "Run:" + data.CycleInventoryCountHeaderID.ToString() + " Name:" + data.Name + "(" + data.Status + ")"; }

                    //if (bDownloadDetailSummary != null) { bDownloadDetailSummary.CommandArgument = data.CycleInventoryCountHeaderID.ToString(); bDownloadDetailSummary.CommandName = "Run:" + data.CycleInventoryCountHeaderID.ToString() + " Name:" + data.Name + "(" + data.Status + ")"; }
                    //if (bDownloadDetail != null) { bDownloadDetail.CommandArgument = data.CycleInventoryCountHeaderID.ToString(); bDownloadDetail.CommandName = "Run:" + data.CycleInventoryCountHeaderID.ToString() + " Name:" + data.Name + "(" + data.Status + ")"; }


                    if (bOPenBatchList != null) { bOPenBatchList.CommandArgument = data.CycleInventoryCountHeaderID.ToString(); bOPenBatchList.CommandName = "Run:" + data.CycleInventoryCountHeaderID.ToString() + " Name:" + data.Name + "(" + data.Status + ")"; }
                    if (bHold != null) { bHold.CommandArgument = data.CycleInventoryCountHeaderID.ToString(); bHold.CommandName = "Run:" + data.CycleInventoryCountHeaderID.ToString() + " Name:" + data.Name + "(" + data.Status + ")"; }
                    if (bNew != null) { bNew.CommandArgument = data.CycleInventoryCountHeaderID.ToString(); bNew.CommandName = "Run:" + data.CycleInventoryCountHeaderID.ToString() + " Name:" + data.Name + "(" + data.Status + ")"; }
                    //if (bEdit != null) { bEdit.CommandArgument = data.CycleInventoryCountHeaderID.ToString(); bEdit.CommandName = "Run:" + data.CycleInventoryCountHeaderID.ToString() + " Name:" + data.Name + "(" + data.Status + ")"; }
                }

                if (TabRuns.ActiveTab.HeaderText == "New")
                {
                    if (bActivate != null) { bActivate.Visible = true; }
                    if (bInActivate != null) { bInActivate.Visible = true; }
                    if (bCLosedActivate != null) { bCLosedActivate.Visible = true; }
                    //if (bEdit != null) { bEdit.Visible = false; }
                }
                if (TabRuns.ActiveTab.HeaderText == "Active")
                {
                    if (bOPenBatchList != null) { bOPenBatchList.Visible = true; }
                    if (bInActivate != null) { bInActivate.Visible = true; }
                    if (bCLosedActivate != null) { bCLosedActivate.Visible = true; }
                    if (bHold != null) { bHold.Visible = true; }
                }
                if (TabRuns.ActiveTab.HeaderText == "Closed")
                {
                    //if (bOPenBatchList != null) { bOPenBatchList.Visible = true; }
                }
                if (TabRuns.ActiveTab.HeaderText == "Inactive")
                {
                    if (bNew != null) { bNew.Visible = true; }
                }
            }
        }
        #endregion

        void gvRunSpread_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (gvRunSpread.SelectedIndex >= 0)
            {
                //decimal ID = 
                //vwGetCCRunHeaderBatch Data = (vwGetCCRunHeaderBatch) gvRunSpread.SelectedRow.DataItem;
                //SpreadTabData.HeaderText = "Data - (" + Data.CycleInventoryCountHeaderID + ")" + Data.Name;
                SpreadTabData.HeaderText = "Data - (" + gvRunSpread.SelectedDataKey.Value + ")" + "";
            }
        }

        void gvRunSpread_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            SetMainMessage("");
            if (e.CommandName == "Select")
            {


            }
            else if (e.CommandName != "Page")
            {
                ImageButton btnOpen = (ImageButton)e.CommandSource;
                #region Download
                if (btnOpen.ID.ToUpper() == "IMGDOWNLOADTEMPLATESUMMARY")
                {
                    string Message = "";
                    decimal ID = -1;
                    if (decimal.TryParse(btnOpen.CommandArgument, out ID) == false)
                    {
                        Message = "Invalid ID.";
                        SetMainMessage(Message);
                        return;
                    }
                    //ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "PrintReport('CCTEMPLATESUMMARY','" + btnOpen.CommandArgument + "');", true);
                }

                #endregion
                ////if (btnOpen.ID.ToUpper() == "IMGDOWNLOADTEMPLATESUMMARYDETAIL")
                ////{
                ////    string Message = "";
                ////    decimal ID = -1;
                ////    if (decimal.TryParse(btnOpen.CommandArgument, out ID) == false)
                ////    {
                ////        Message = "Invalid ID.";
                ////        SetMainMessage(Message);
                ////        return;
                ////    }
                ////    //ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "PrintReport('CCTEMPLATESUMMARYDETAIL','" + btnOpen.CommandArgument + "');", true);
                ////}
                ////if (btnOpen.ID.ToUpper() == "IMGDOWNLOADTEMPLATEDETAIL")
                ////{
                ////    string Message = "";
                ////    decimal ID = -1;
                ////    if (decimal.TryParse(btnOpen.CommandArgument, out ID) == false)
                ////    {
                ////        Message = "Invalid ID.";
                ////        SetMainMessage(Message);
                ////        return;
                ////    }
                ////    //ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "PrintReport('CCTEMPLATEDETAIL','" + btnOpen.CommandArgument + "');", true);
                ////}






                //#region Open Batch List
                //if (btnOpen.ID.ToUpper() == "IMGOPENBATCHLIST")
                //{
                //    string Message = "";
                //    decimal ID = -1;
                //    if (decimal.TryParse(btnOpen.CommandArgument, out ID) == false)
                //    {
                //        Message = "Invalid ID.";
                //        SetMainMessage(Message);
                //        return;
                //    }
                //    hdnCycleInventoryCountHeaderID.Value = btnOpen.CommandArgument;
                //    lblHeaderRunNewOpenBatchList.Text = "Batch List:" + btnOpen.CommandName;
                //    pnlGridViewRunNewOpenBatchList.Visible = true;
                //    pnlGridViewRunNew.Visible = false;
                //    UpdateBatchGrid();
                //    //ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "PrintReport('CCRUNHEADER','" + btnOpen.CommandArgument + "');", true);
                //}
                //#endregion
                ////------------------------------------------
                //#region Move To Active
                //if (btnOpen.ID.ToUpper() == "IMGACTIVATE")
                //{
                //    string Message = "";
                //    decimal ID = -1;
                //    if (decimal.TryParse(btnOpen.CommandArgument, out ID) == false) { Message = "Invalid ID."; return; }
                //    CycleCountManager ccm = new CycleCountManager(User.Identity.Name);
                //    ccm.SetRunStatus(ID, "Active", ref Message);
                //    SetMainMessage(Message);
                //    UpdategrdRuns();
                //}
                //#endregion


                //#region Move To Hold
                //if (btnOpen.ID.ToUpper() == "IMGHOLD")
                //{
                //    string Message = "";
                //    decimal ID = -1;
                //    if (decimal.TryParse(btnOpen.CommandArgument, out ID) == false) { Message = "Invalid ID."; return; }
                //    CycleCountManager ccm = new CycleCountManager(User.Identity.Name);
                //    ccm.SetRunStatus(ID, "Hold", ref Message);
                //    SetMainMessage(Message);
                //    UpdategrdRuns();
                //}
                //#endregion
                //#region Move To New
                //if (btnOpen.ID.ToUpper() == "IMGNEW")
                //{
                //    string Message = "";
                //    decimal ID = -1;
                //    if (decimal.TryParse(btnOpen.CommandArgument, out ID) == false) { Message = "Invalid ID."; return; }
                //    CycleCountManager ccm = new CycleCountManager(User.Identity.Name);
                //    ccm.SetRunStatus(ID, "New", ref Message);
                //    SetMainMessage(Message);
                //    UpdategrdRuns();
                //}
                //#endregion
                //#region Move To Active
                //if (btnOpen.ID.ToUpper() == "IMGCLOSE")
                //{
                //    string Message = "";
                //    decimal ID = -1;
                //    if (decimal.TryParse(btnOpen.CommandArgument, out ID) == false) { Message = "Invalid ID."; return; }
                //    CycleCountManager ccm = new CycleCountManager(User.Identity.Name);
                //    ccm.SetRunStatus(ID, "Closed", ref Message);
                //    SetMainMessage(Message);
                //    UpdategrdRuns();
                //}
                //#endregion
                //#region Move To Active
                //if (btnOpen.ID.ToUpper() == "IMGINACTIVATE")
                //{
                //    string Message = "";
                //    decimal ID = -1;
                //    if (decimal.TryParse(btnOpen.CommandArgument, out ID) == false) { Message = "Invalid ID."; return; }
                //    CycleCountManager ccm = new CycleCountManager(User.Identity.Name);
                //    ccm.SetRunStatus(ID, "Inactive", ref Message);
                //    SetMainMessage(Message);
                //    UpdategrdRuns();
                //}
                //#endregion
            }
        }
        void gvRunSpread_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vwGetCCRunHeaderBatch data = (vwGetCCRunHeaderBatch)e.Row.DataItem;
                //ImageButton bDownload = (ImageButton)e.Row.FindControl("imgDownLoadTemplateSummary");
                ////ImageButton bDownloadDetailSummary = (ImageButton)e.Row.FindControl("imgDownLoadTemplateSummaryDetail");
                ////ImageButton bDownloadDetail = (ImageButton)e.Row.FindControl("imgDownLoadTemplateDetail");
                //ImageButton bOPenBatchList = (ImageButton)e.Row.FindControl("imgOpenBatchList");
                //ImageButton bActivate = (ImageButton)e.Row.FindControl("imgactivate");
                //ImageButton bInActivate = (ImageButton)e.Row.FindControl("imgInactivate");
                //ImageButton bCLosedActivate = (ImageButton)e.Row.FindControl("imgClose");
                //ImageButton bHold = (ImageButton)e.Row.FindControl("imgHold");
                //ImageButton bNew = (ImageButton)e.Row.FindControl("imgNew");
                ////ImageButton bEdit = (ImageButton)e.Row.FindControl("ImageEdit");




                //if (bDownload != null) { bDownload.CommandArgument = "-1"; bDownload.Visible = false; }
                ////if (bDownloadDetailSummary != null) { bDownloadDetailSummary.CommandArgument = "-1"; bDownloadDetailSummary.Visible = false; }
                ////if (bDownloadDetail != null) { bDownloadDetail.CommandArgument = "-1"; bDownloadDetail.Visible = false; }
                //if (bOPenBatchList != null) { bOPenBatchList.CommandArgument = "-1"; bOPenBatchList.Visible = false; }
                //if (bActivate != null) { bActivate.CommandArgument = "-1"; bActivate.Visible = false; }
                //if (bInActivate != null) { bInActivate.CommandArgument = "-1"; bInActivate.Visible = false; }
                //if (bCLosedActivate != null) { bCLosedActivate.CommandArgument = "-1"; bCLosedActivate.Visible = false; }
                //if (bHold != null) { bHold.CommandArgument = "-1"; bHold.Visible = false; }
                //if (bNew != null) { bNew.CommandArgument = "-1"; bNew.Visible = false; }
                ////if (bEdit != null) { bEdit.CommandArgument = "-1"; bEdit.Visible = false; }


                //if (data != null)
                //{
                //    if (bActivate != null) { bActivate.CommandArgument = data.CycleInventoryCountHeaderID.ToString(); bActivate.CommandName = "Run:" + data.CycleInventoryCountHeaderID.ToString() + " Name:" + data.Name + "(" + data.Status + ")"; }
                //    if (bInActivate != null) { bInActivate.CommandArgument = data.CycleInventoryCountHeaderID.ToString(); bInActivate.CommandName = "Run:" + data.CycleInventoryCountHeaderID.ToString() + " Name:" + data.Name + "(" + data.Status + ")"; }
                //    if (bCLosedActivate != null) { bCLosedActivate.CommandArgument = data.CycleInventoryCountHeaderID.ToString(); bCLosedActivate.CommandName = "Run:" + data.CycleInventoryCountHeaderID.ToString() + " Name:" + data.Name + "(" + data.Status + ")"; }
                //    if (bDownload != null) { bDownload.CommandArgument = data.CycleInventoryCountHeaderID.ToString(); bDownload.CommandName = "Run:" + data.CycleInventoryCountHeaderID.ToString() + " Name:" + data.Name + "(" + data.Status + ")"; }

                //    //if (bDownloadDetailSummary != null) { bDownloadDetailSummary.CommandArgument = data.CycleInventoryCountHeaderID.ToString(); bDownloadDetailSummary.CommandName = "Run:" + data.CycleInventoryCountHeaderID.ToString() + " Name:" + data.Name + "(" + data.Status + ")"; }
                //    //if (bDownloadDetail != null) { bDownloadDetail.CommandArgument = data.CycleInventoryCountHeaderID.ToString(); bDownloadDetail.CommandName = "Run:" + data.CycleInventoryCountHeaderID.ToString() + " Name:" + data.Name + "(" + data.Status + ")"; }


                //    if (bOPenBatchList != null) { bOPenBatchList.CommandArgument = data.CycleInventoryCountHeaderID.ToString(); bOPenBatchList.CommandName = "Run:" + data.CycleInventoryCountHeaderID.ToString() + " Name:" + data.Name + "(" + data.Status + ")"; }
                //    if (bHold != null) { bHold.CommandArgument = data.CycleInventoryCountHeaderID.ToString(); bHold.CommandName = "Run:" + data.CycleInventoryCountHeaderID.ToString() + " Name:" + data.Name + "(" + data.Status + ")"; }
                //    if (bNew != null) { bNew.CommandArgument = data.CycleInventoryCountHeaderID.ToString(); bNew.CommandName = "Run:" + data.CycleInventoryCountHeaderID.ToString() + " Name:" + data.Name + "(" + data.Status + ")"; }
                //    //if (bEdit != null) { bEdit.CommandArgument = data.CycleInventoryCountHeaderID.ToString(); bEdit.CommandName = "Run:" + data.CycleInventoryCountHeaderID.ToString() + " Name:" + data.Name + "(" + data.Status + ")"; }
                //}

                //if (TabRuns.ActiveTab.HeaderText == "New")
                //{
                //    if (bActivate != null) { bActivate.Visible = true; }
                //    if (bInActivate != null) { bInActivate.Visible = true; }
                //    if (bCLosedActivate != null) { bCLosedActivate.Visible = true; }
                //    //if (bEdit != null) { bEdit.Visible = false; }
                //}
                //if (TabRuns.ActiveTab.HeaderText == "Active")
                //{
                //    if (bOPenBatchList != null) { bOPenBatchList.Visible = true; }
                //    if (bInActivate != null) { bInActivate.Visible = true; }
                //    if (bCLosedActivate != null) { bCLosedActivate.Visible = true; }
                //    if (bHold != null) { bHold.Visible = true; }
                //}
                //if (TabRuns.ActiveTab.HeaderText == "Closed")
                //{
                //    //if (bOPenBatchList != null) { bOPenBatchList.Visible = true; }
                //}
                //if (TabRuns.ActiveTab.HeaderText == "Inactive")
                //{
                //    if (bNew != null) { bNew.Visible = true; }
                //}
            }

        }







        #region Batches
        void imgHeaderRunNewOpenControlListBack_Click(object sender, ImageClickEventArgs e)
        {
            pnlGridViewBatchConroleList.Visible = false;
            pnlGridViewRunNewOpenBatchList.Visible = true;
        }
        void imgHeaderRunNewOpenScanListBack_Click(object sender, ImageClickEventArgs e)
        {
            pnlGridViewBatchScanList.Visible = false;
            pnlGridViewRunNewOpenBatchList.Visible = true;
        }
        void TabBatches_ActiveTabChanged(object sender, EventArgs e)
        {
            SetMainMessage("");
            UpdateBatchGrid();
        }
        void bRefreshBatches_Click(object sender, EventArgs e)
        {
            UpdateBatchGrid();
        }
        private void UpdateBatchGrid()
        {
            string QueryString = "";
            decimal ID = -1;
            if (decimal.TryParse(hdnCycleInventoryCountHeaderID.Value, out ID) == false) { ID = -1; }
            pnlGridViewRunNewOpenBatchList.Visible = true;
            pnlGridViewBatchConroleList.Visible = false;
            pnlGridViewBatchScanList.Visible = false;
            CycleCountManager im = new CycleCountManager(User.Identity.Name);
            grdBatches.DataSource = im.BatchGridData(ID, ref QueryString).OrderBy(x => x.IFSLocation);
            grdBatches.DataBind();
            hdnReportThisData.Value = QueryString;
        }
        void grdBatches_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            SetMainMessage("");
            if (e.CommandName != "Page")
            {
                ImageButton btnOpen = (ImageButton)e.CommandSource;
                #region Generate Run Data
                if (btnOpen.ID.ToUpper() == "IMGLOADRUNDATA")
                {
                    string Message = "";
                    decimal ID = -1;
                    if (decimal.TryParse(btnOpen.CommandArgument, out ID) == false)
                    {
                        Message = "Invalid ID.";
                        SetMainMessage(Message);
                        return;
                    }
                    CycleCountManager ccm = new CycleCountManager(User.Identity.Name);
                    ccm.GenerateTemplateCycle(ID, ref Message);
                    lblHeaderRunNewOpenBatchList.Text = Message;
                    UpdateTemplateGrid();
                }
                #endregion
                #region Download
                if (btnOpen.ID.ToUpper() == "IMGDOWNLOADLOCATIONS")
                {
                    string Message = "";
                    decimal ID = -1;
                    if (decimal.TryParse(btnOpen.CommandArgument, out ID) == false)
                    {
                        Message = "Invalid ID.";
                        SetMainMessage(Message);
                        return;
                    }
                    ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "PrintReport('CCBatch','" + btnOpen.CommandArgument + "');", true);
                }
                #endregion
                #region Open Control List
                if (btnOpen.ID.ToUpper() == "IMGOPENRUNCONTROLSUMMARY")
                {
                    string Message = "";
                    decimal ID = -1;
                    if (decimal.TryParse(btnOpen.CommandArgument, out ID) == false)
                    {
                        Message = "Invalid ID.";
                        lblHeaderRunNewOpenBatchList.Text = Message;
                        lblHeaderRunNewOpenBatchList.Visible = true;
                        return;
                    }
                    hdnCycleInventoryCountIterationHeaderID.Value = btnOpen.CommandArgument;
                    lblHeaderRunNewOpenControlList.Text = "Batch Control List: " + btnOpen.CommandName;
                    pnlGridViewBatchConroleList.Visible = true;
                    pnlGridViewRunNewOpenBatchList.Visible = false;
                    UpdateRunControlDetail();
                    //ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "PrintReport('CCRUNHEADER','" + btnOpen.CommandArgument + "');", true);
                }
                #endregion
                #region Open Physical Scan List
                if (btnOpen.ID.ToUpper() == "IMGOPENRUNPHYSICALSCAN")
                {
                    string Message = "";
                    decimal ID = -1;
                    if (decimal.TryParse(btnOpen.CommandArgument, out ID) == false)
                    {
                        Message = "Invalid ID.";
                        SetMainMessage(Message);
                        return;
                    }
                    hdnCycleInventoryCountIterationHeaderID.Value = btnOpen.CommandArgument;
                    lblHeaderRunNewOpenScanList.Text = "Batch Scan List: " + btnOpen.CommandName;
                    pnlGridViewBatchScanList.Visible = true;
                    pnlGridViewRunNewOpenBatchList.Visible = false;
                    //ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "PrintReport('CCRUNHEADER','" + btnOpen.CommandArgument + "');", true);
                }
                #endregion
                #region IMGOpen
                if (btnOpen.ID.ToUpper() == "IMGOPEN")
                {
                    string Message = "";
                    decimal ID = -1;
                    if (decimal.TryParse(btnOpen.CommandArgument, out ID) == false) { Message = "Invalid ID."; return; }
                    CycleCountManager ccm = new CycleCountManager(User.Identity.Name);
                    ccm.CC_ActivateBatchControl(ID, ref Message);
                    SetMainMessage(Message);
                    UpdateBatchGrid();
                }
                #endregion
                #region Invalidate
                if (btnOpen.ID.ToUpper() == "IMGINVALID")
                {
                    string Message = "";
                    decimal ID = -1;
                    if (decimal.TryParse(btnOpen.CommandArgument, out ID) == false) { Message = "Invalid ID."; return; }
                    CycleCountManager ccm = new CycleCountManager(User.Identity.Name);
                    ccm.SetBatchStatus(ID, "Invalid", ref Message);
                    SetMainMessage(Message);
                    UpdateBatchGrid();
                }
                #endregion
                #region imgLocked
                if (btnOpen.ID.ToUpper() == "IMGLOCKED")
                {
                    string Message = "";
                    decimal ID = -1;
                    if (decimal.TryParse(btnOpen.CommandArgument, out ID) == false) { Message = "Invalid ID."; return; }
                    CycleCountManager ccm = new CycleCountManager(User.Identity.Name);
                    ccm.SetBatchStatus(ID, "Locked", ref Message);
                    SetMainMessage(Message);
                    UpdateBatchGrid();
                }
                #endregion
                #region imgHold
                if (btnOpen.ID.ToUpper() == "IMGHOLD")
                {
                    string Message = "";
                    decimal ID = -1;
                    if (decimal.TryParse(btnOpen.CommandArgument, out ID) == false) { Message = "Invalid ID."; return; }
                    CycleCountManager ccm = new CycleCountManager(User.Identity.Name);
                    ccm.SetBatchStatus(ID, "Hold", ref Message);
                    SetMainMessage(Message);
                    UpdateBatchGrid();
                }
                #endregion
                #region imgCloneIntoNew
                if (btnOpen.ID.ToUpper() == "IMGCLONEINTONEW")
                {
                    string Message = "";
                    decimal ID = -1;
                    if (decimal.TryParse(btnOpen.CommandArgument, out ID) == false) { Message = "Invalid ID."; return; }
                    CycleCountManager ccm = new CycleCountManager(User.Identity.Name);
                    ccm.BatchCloneToNew(ID, ref Message);
                    SetMainMessage(Message);
                    UpdateBatchGrid();
                }
                #endregion
            }
        }
        void grdBatches_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                vwGetCCRunHeaderBatch data = (vwGetCCRunHeaderBatch)e.Row.DataItem;

                ImageButton bIOpen = (ImageButton)e.Row.FindControl("imgOpen");
                ImageButton bILocked = (ImageButton)e.Row.FindControl("imgLocked");
                ImageButton bInvalid = (ImageButton)e.Row.FindControl("imgInvalid");
                ImageButton bIHold = (ImageButton)e.Row.FindControl("imgHold");

                ImageButton bInvalidate = (ImageButton)e.Row.FindControl("imgInactivate");
                ImageButton bGenerateRunCycle = (ImageButton)e.Row.FindControl("imgloadRunData");
                //ImageButton bDownload = (ImageButton)e.Row.FindControl("imgDownLoadLocations");
                ImageButton bOPenControlList = (ImageButton)e.Row.FindControl("imgOpenRunControlSummary");
                ImageButton bOPenScanList = (ImageButton)e.Row.FindControl("imgOpenRunPhysicalScan");
                ImageButton bCloneIntoNew = (ImageButton)e.Row.FindControl("imgCloneIntoNew");

                if (bIOpen != null) { bIOpen.CommandArgument = "-1"; bIOpen.Visible = false; }
                if (bILocked != null) { bILocked.CommandArgument = "-1"; bILocked.Visible = false; }
                if (bInvalid != null) { bInvalid.CommandArgument = "-1"; bInvalid.Visible = false; }
                if (bIHold != null) { bIHold.CommandArgument = "-1"; bIHold.Visible = false; }

                if (bInvalidate != null) { bInvalidate.CommandArgument = "-1"; bInvalidate.Visible = false; }
                if (bGenerateRunCycle != null) { bGenerateRunCycle.CommandArgument = "-1"; bGenerateRunCycle.Visible = false; }
                //if (bDownload != null) { bDownload.CommandArgument = "-1"; bDownload.Visible = false; }
                if (bOPenControlList != null) { bOPenControlList.CommandArgument = "-1"; bOPenControlList.Visible = false; }
                if (bOPenScanList != null) { bOPenScanList.CommandArgument = "-1"; bOPenScanList.Visible = false; }
                if (bCloneIntoNew != null) { bCloneIntoNew.CommandArgument = "-1"; bCloneIntoNew.Visible = false; }


                //if (bClean != null) { bClean.CommandArgument = "-1"; }
                //if (bHold != null) { bHold.CommandArgument = "-1"; }
                //if (data != null)
                //{
                //    if (bILocked != null) { bILocked.CommandArgument = data.CycleInventoryCountIterationHeaderID.ToString(); bILocked.CommandName = "Run:" + data.CycleInventoryCountHeaderID.ToString() + " Name:" + data.Name + "(" + data.Status + ") Batch:" + data.Batch; }
                //    if (bIOpen != null) { bIOpen.CommandArgument = data.CycleInventoryCountIterationHeaderID.ToString(); bIOpen.CommandName = "Run:" + data.CycleInventoryCountHeaderID.ToString() + " Name:" + data.Name + "(" + data.Status + ")"; }
                //    if (bInvalid != null) { bInvalid.CommandArgument = data.CycleInventoryCountIterationHeaderID.ToString(); bInvalid.CommandName = "Run:" + data.CycleInventoryCountHeaderID.ToString() + " Name:" + data.Name + "(" + data.Status + ") Batch:" + data.Batch; }
                //    if (bIHold != null) { bIHold.CommandArgument = data.CycleInventoryCountIterationHeaderID.ToString(); bInvalid.CommandName = "Run:" + data.CycleInventoryCountHeaderID.ToString() + " Name:" + data.Name + "(" + data.Status + ") Batch:" + data.Batch; }


                //    if (bInvalidate != null) { bInvalidate.CommandArgument = data.CycleInventoryCountIterationHeaderID.ToString(); bInvalidate.CommandName = "Run:" + data.CycleInventoryCountHeaderID.ToString() + " Name:" + data.Name + "(" + data.Status + ") Batch:" + data.Batch; }
                //    if (bGenerateRunCycle != null) { bGenerateRunCycle.CommandArgument = data.CycleInventoryCountIterationHeaderID.ToString(); bGenerateRunCycle.CommandName = "Run:" + data.CycleInventoryCountHeaderID.ToString() + " Name:" + data.Name + "(" + data.Status + ") Batch:" + data.Batch; }
                //    //if (bDownload != null) { bDownload.CommandArgument = data.CycleInventoryCountIterationHeaderID.ToString(); bDownload.CommandName = "Run:" + data.CycleInventoryCountHeaderID.ToString() + " Name:" + data.Name + "(" + data.Status + ") Batch:" + data.Batch; }
                //    if (bOPenControlList != null) { bOPenControlList.CommandArgument = data.CycleInventoryCountIterationHeaderID.ToString(); bOPenControlList.CommandName = "Run:" + data.Batch + " Name:" + data.Name + "(" + data.Status + ") Batch:" + data.Batch; }
                //    if (bOPenScanList != null) { bOPenScanList.CommandArgument = data.CycleInventoryCountIterationHeaderID.ToString(); bOPenScanList.CommandName = "Run:" + data.Batch + " Name:" + data.Name + "(" + data.Status + ") Batch:" + data.Batch; }
                //    if (bCloneIntoNew != null) { bCloneIntoNew.CommandArgument = data.CycleInventoryCountIterationHeaderID.ToString(); bCloneIntoNew.CommandName = "Run:" + data.Batch + " Name:" + data.Name + "(" + data.Status + ") Batch:" + data.Batch; }
                //}

                if (TabBatches.ActiveTab.HeaderText == "New")
                {
                    if (bIOpen != null) { bIOpen.Visible = true; }
                    if (bGenerateRunCycle != null) { bGenerateRunCycle.Visible = true; }
                    if (bInvalid != null) { bInvalid.Visible = true; }
                    if (bIHold != null) { bIHold.Visible = true; }
                }
                if (TabBatches.ActiveTab.HeaderText == "Open")
                {
                    if (bIHold != null) { bIHold.Visible = true; }
                    if (bInvalid != null) { bInvalid.Visible = true; }
                    if (bOPenControlList != null) { bOPenControlList.Visible = true; }
                    if (bOPenScanList != null) { bOPenScanList.Visible = true; }
                }
                if (TabBatches.ActiveTab.HeaderText == "Locked")
                {
                    if (bInvalid != null) { bInvalid.Visible = true; }
                    if (bOPenControlList != null) { bOPenControlList.Visible = true; }
                    if (bOPenScanList != null) { bOPenScanList.Visible = true; }
                    //if (bCloneIntoNew != null) { bCloneIntoNew.Visible = true; }
                }
                if (TabBatches.ActiveTab.HeaderText == "Invalid")
                {
                    if (bCloneIntoNew != null) { bCloneIntoNew.Visible = true; }
                }
                if (TabBatches.ActiveTab.HeaderText == "Hold")
                {
                    //if (bNew != null) { bNew.Visible = true; }
                }
                if (TabBatches.ActiveTab.HeaderText == "Sent IFS")
                {
                    //if (bNew != null) { bNew.Visible = true; }
                }
            }
        }
        #endregion





        #region BatchControls
        void TabRunControlDetail_ActiveTabChanged(object sender, EventArgs e)
        {
            UpdateRunControlDetail();
        }
        void bRefreshBatchesControlDetail_Click(object sender, EventArgs e)
        {
            UpdateRunControlDetail();
        }
        private void UpdateRunControlDetail()
        {
            string QueryString = "";
            decimal ID = -1;
            if (decimal.TryParse(hdnCycleInventoryCountIterationHeaderID.Value, out ID) == false) { ID = -1; }
            CycleCountManager im = new CycleCountManager(User.Identity.Name);
            gvRunControlDetail.DataSource = im.GridDataBatchControl(ID, TabRunControlDetail.ActiveTab.HeaderText, chkShowSummary_Control.Checked,
                chkShowDevices_Control.Checked,
                chkShowParts_Control.Checked, ref QueryString).OrderBy(x => x.IFSLocation);
            gvRunControlDetail.DataBind();
            hdnReportThisData.Value = QueryString;
        }
        void gvRunControlDetail_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            SetMainMessage("");
            if (e.CommandName != "Page")
            {
                //ImageButton btnOpen = (ImageButton)e.CommandSource;
                //#region Generate Run Data
                //if (btnOpen.ID.ToUpper() == "IMGLOADRUNDATA")
                //{
                //    string Message = "";
                //    decimal ID = -1;
                //    if (decimal.TryParse(btnOpen.CommandArgument, out ID) == false)
                //    {
                //        Message = "Invalid ID.";
                //        SetMainMessage(Message);
                //        return;
                //    }
                //    CycleCountManager ccm = new CycleCountManager(User.Identity.Name);
                //    ccm.GenerateTemplateCycle(ID, ref Message);
                //    lblHeaderRunNewOpenBatchList.Text = Message;
                //    UpdateTemplateGrid();
                //}
                //#endregion
                //#region Download
                //if (btnOpen.ID.ToUpper() == "IMGDOWNLOADLOCATIONS")
                //{
                //    string Message = "";
                //    decimal ID = -1;
                //    if (decimal.TryParse(btnOpen.CommandArgument, out ID) == false)
                //    {
                //        Message = "Invalid ID.";
                //        SetMainMessage(Message);
                //        return;
                //    }
                //    ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "PrintReport('CCBatch','" + btnOpen.CommandArgument + "');", true);
                //}
                //#endregion
                //#region Open Control List
                //if (btnOpen.ID.ToUpper() == "IMGOPENRUNCONTROLSUMMARY")
                //{
                //    string Message = "";
                //    decimal ID = -1;
                //    if (decimal.TryParse(btnOpen.CommandArgument, out ID) == false)
                //    {
                //        Message = "Invalid ID.";
                //        lblHeaderRunNewOpenBatchList.Text = Message;
                //        lblHeaderRunNewOpenBatchList.Visible = true;
                //        return;
                //    }


                //    hdnCycleInventoryCountIterationHeaderID.Value = btnOpen.CommandArgument;
                //    lblHeaderRunNewOpenControlList.Text = "Batch Control List: " + btnOpen.CommandName;
                //    pnlGridViewBatchConroleList.Visible = true;
                //    pnlGridViewRunNewOpenBatchList.Visible = false;
                //    //ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "PrintReport('CCRUNHEADER','" + btnOpen.CommandArgument + "');", true);
                //}
                //#endregion
                //#region Open Physical Scan List
                //if (btnOpen.ID.ToUpper() == "IMGOPENRUNPHYSICALSCAN")
                //{
                //    string Message = "";
                //    decimal ID = -1;
                //    if (decimal.TryParse(btnOpen.CommandArgument, out ID) == false)
                //    {
                //        Message = "Invalid ID.";
                //        SetMainMessage(Message);
                //        return;
                //    }
                //    hdnCycleInventoryCountIterationHeaderID.Value = btnOpen.CommandArgument;
                //    lblHeaderRunNewOpenScanList.Text = "Batch Scan List: " + btnOpen.CommandName;
                //    pnlGridViewBatchScanList.Visible = true;
                //    pnlGridViewRunNewOpenBatchList.Visible = false;
                //    //ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "PrintReport('CCRUNHEADER','" + btnOpen.CommandArgument + "');", true);
                //}
                //#endregion
                //#region IMGOpen
                //if (btnOpen.ID.ToUpper() == "IMGOPEN")
                //{
                //    string Message = "";
                //    decimal ID = -1;
                //    if (decimal.TryParse(btnOpen.CommandArgument, out ID) == false) { Message = "Invalid ID."; return; }
                //    CycleCountManager ccm = new CycleCountManager(User.Identity.Name);
                //    ccm.CC_ActivateBatchControl(ID, ref Message);
                //    SetMainMessage(Message);
                //    UpdateBatchGrid();
                //}
                //#endregion
                //#region Invalidate
                //if (btnOpen.ID.ToUpper() == "IMGINVALID")
                //{
                //    string Message = "";
                //    decimal ID = -1;
                //    if (decimal.TryParse(btnOpen.CommandArgument, out ID) == false) { Message = "Invalid ID."; return; }
                //    CycleCountManager ccm = new CycleCountManager(User.Identity.Name);
                //    ccm.SetBatchStatus(ID, "Invalid", ref Message);
                //    SetMainMessage(Message);
                //    UpdateBatchGrid();
                //}
                //#endregion
                //#region imgLocked
                //if (btnOpen.ID.ToUpper() == "IMGLOCKED")
                //{
                //    string Message = "";
                //    decimal ID = -1;
                //    if (decimal.TryParse(btnOpen.CommandArgument, out ID) == false) { Message = "Invalid ID."; return; }
                //    CycleCountManager ccm = new CycleCountManager(User.Identity.Name);
                //    ccm.SetBatchStatus(ID, "Locked", ref Message);
                //    SetMainMessage(Message);
                //    UpdateBatchGrid();
                //}
                //#endregion
                //#region imgHold
                //if (btnOpen.ID.ToUpper() == "IMGHOLD")
                //{
                //    string Message = "";
                //    decimal ID = -1;
                //    if (decimal.TryParse(btnOpen.CommandArgument, out ID) == false) { Message = "Invalid ID."; return; }
                //    CycleCountManager ccm = new CycleCountManager(User.Identity.Name);
                //    ccm.SetBatchStatus(ID, "Hold", ref Message);
                //    SetMainMessage(Message);
                //    UpdateBatchGrid();
                //}
                //#endregion
                //#region imgCloneIntoNew
                //if (btnOpen.ID.ToUpper() == "IMGCLONEINTONEW")
                //{
                //    string Message = "";
                //    decimal ID = -1;
                //    if (decimal.TryParse(btnOpen.CommandArgument, out ID) == false) { Message = "Invalid ID."; return; }
                //    CycleCountManager ccm = new CycleCountManager(User.Identity.Name);
                //    ccm.BatchCloneToNew(ID, ref Message);
                //    SetMainMessage(Message);
                //    UpdateBatchGrid();
                //}
                //#endregion
            }
        }
        void gvRunControlDetail_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                //    GridCCRunBatchesControl data = (GridCCRunBatchesControl)e.Row.DataItem;

                //    ImageButton bIOpen = (ImageButton)e.Row.FindControl("imgOpen");
                //    ImageButton bILocked = (ImageButton)e.Row.FindControl("imgLocked");
                //    ImageButton bInvalid = (ImageButton)e.Row.FindControl("imgInvalid");
                //    ImageButton bIHold = (ImageButton)e.Row.FindControl("imgHold");

                //    ImageButton bInvalidate = (ImageButton)e.Row.FindControl("imgInactivate");
                //    ImageButton bGenerateRunCycle = (ImageButton)e.Row.FindControl("imgloadRunData");
                //    ImageButton bDownload = (ImageButton)e.Row.FindControl("imgDownLoadLocations");
                //    ImageButton bOPenControlList = (ImageButton)e.Row.FindControl("imgOpenRunControlSummary");
                //    ImageButton bOPenScanList = (ImageButton)e.Row.FindControl("imgOpenRunPhysicalScan");
                //    ImageButton bCloneIntoNew = (ImageButton)e.Row.FindControl("imgCloneIntoNew");




                //    if (bIOpen != null) { bIOpen.CommandArgument = "-1"; }
                //    if (bILocked != null) { bILocked.CommandArgument = "-1"; }
                //    if (bInvalid != null) { bInvalid.CommandArgument = "-1"; }
                //    if (bIHold != null) { bIHold.CommandArgument = "-1"; }

                //    if (bInvalidate != null) { bInvalidate.CommandArgument = "-1"; }
                //    if (bGenerateRunCycle != null) { bGenerateRunCycle.CommandArgument = "-1"; }
                //    if (bDownload != null) { bDownload.CommandArgument = "-1"; }
                //    if (bOPenControlList != null) { bOPenControlList.CommandArgument = "-1"; }
                //    if (bOPenScanList != null) { bOPenScanList.CommandArgument = "-1"; }
                //    if  != null) { bIOpen.CommandArgument = data.CycleInventoryCountIterationHeaderID.ToString(); bIOpen.CommandName = "Run:" + data.CycleInventoryCountHeaderID.ToString() + " Name:" + data.Name + "(" + data.Status + ")"; }
                //        if (bInvalid != null) { bInvalid.CommandArgument = data.CycleInventoryCountIterationHeaderID.ToString(); bInvalid.CommandName = "Run:" + data.CycleInventoryCountHeaderID.ToString() + " Name:" + data.Name + "(" + data.Status + ") Batch:" + data.Batch; }
                //        if (bIHold != (bCloneIntoNew != null) { bCloneIntoNew.CommandArgument = "-1"; }


                //    //if (bClean != null) { bClean.CommandArgument = "-1"; }
                //    //if (bHold != null) { bHold.CommandArgument = "-1"; }
                //    if (data != null)
                //    {
                //        if (bILocked != null) { bILocked.CommandArgument = data.CycleInventoryCountIterationHeaderID.ToString(); bILocked.CommandName = "Run:" + data.CycleInventoryCountHeaderID.ToString() + " Name:" + data.Name + "(" + data.Status + ") Batch:" + data.Batch; }
                //        if (bIOpennull) { bIHold.CommandArgument = data.CycleInventoryCountIterationHeaderID.ToString(); bInvalid.CommandName = "Run:" + data.CycleInventoryCountHeaderID.ToString() + " Name:" + data.Name + "(" + data.Status + ") Batch:" + data.Batch; }


                //        if (bInvalidate != null) { bInvalidate.CommandArgument = data.CycleInventoryCountIterationHeaderID.ToString(); bInvalidate.CommandName = "Run:" + data.CycleInventoryCountHeaderID.ToString() + " Name:" + data.Name + "(" + data.Status + ") Batch:" + data.Batch; }
                //        if (bGenerateRunCycle != null) { bGenerateRunCycle.CommandArgument = data.CycleInventoryCountIterationHeaderID.ToString(); bGenerateRunCycle.CommandName = "Run:" + data.CycleInventoryCountHeaderID.ToString() + " Name:" + data.Name + "(" + data.Status + ") Batch:" + data.Batch; }
                //        if (bDownload != null) { bDownload.CommandArgument = data.CycleInventoryCountIterationHeaderID.ToString(); bDownload.CommandName = "Run:" + data.CycleInventoryCountHeaderID.ToString() + " Name:" + data.Name + "(" + data.Status + ") Batch:" + data.Batch; }
                //        if (bOPenControlList != null) { bOPenControlList.CommandArgument = data.CycleInventoryCountIterationHeaderID.ToString(); bOPenControlList.CommandName = "Run:" + data.Batch + " Name:" + data.Name + "(" + data.Status + ") Batch:" + data.Batch; }
                //        if (bOPenScanList != null) { bOPenScanList.CommandArgument = data.CycleInventoryCountIterationHeaderID.ToString(); bOPenScanList.CommandName = "Run:" + data.Batch + " Name:" + data.Name + "(" + data.Status + ") Batch:" + data.Batch; }
                //        if (bCloneIntoNew != null) { bCloneIntoNew.CommandArgument = data.CycleInventoryCountIterationHeaderID.ToString(); bCloneIntoNew.CommandName = "Run:" + data.Batch + " Name:" + data.Name + "(" + data.Status + ") Batch:" + data.Batch; }
                //    }
            }
        }
        #endregion

        #region BatchScans
        void TabRunScanDetail_ActiveTabChanged(object sender, EventArgs e)
        {
            UpdateRunScanDetail();
        }
        void bRefreshBatchesScanDetail_Click(object sender, EventArgs e)
        {
            UpdateRunScanDetail();
        }
        private void UpdateRunScanDetail()
        {
            string QueryString = "";
            decimal ID = -1;
            if (decimal.TryParse(hdnCycleInventoryCountIterationHeaderID.Value, out ID) == false) { ID = -1; }
            //pnlGridViewRunNewOpenBatchList.Visible = true;
            //pnlGridViewBatchConroleList.Visible = false;
            //pnlGridViewBatchScanList.Visible = false;
            CycleCountManager im = new CycleCountManager(User.Identity.Name);
            //bool showSummary = false;
            //if (TabRunScanDetail.ActiveTab.HeaderText.ToUpper() == "SUMMARY") { showSummary = true; }
            gvRunScanDetail.DataSource = im.GridDataBatchScan(ID, TabRunScanDetail.ActiveTab.HeaderText, chkShowSummary_Scan.Checked,
                chkShowDevices_Scan.Checked,
                chkShowParts_Scan.Checked, ref QueryString).OrderBy(x => x.IFSLocation);
            gvRunScanDetail.DataBind();
            hdnReportThisData.Value = QueryString;
        }
        void gvRunScanDetail_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            SetMainMessage("");
            if (e.CommandName != "Page")
            {
                //ImageButton btnOpen = (ImageButton)e.CommandSource;
                //#region Generate Run Data
                //if (btnOpen.ID.ToUpper() == "IMGLOADRUNDATA")
                //{
                //    string Message = "";
                //    decimal ID = -1;
                //    if (decimal.TryParse(btnOpen.CommandArgument, out ID) == false)
                //    {
                //        Message = "Invalid ID.";
                //        SetMainMessage(Message);
                //        return;
                //    }
                //    CycleCountManager ccm = new CycleCountManager(User.Identity.Name);
                //    ccm.GenerateTemplateCycle(ID, ref Message);
                //    lblHeaderRunNewOpenBatchList.Text = Message;
                //    UpdateTemplateGrid();
                //}
                //#endregion
                //#region Download
                //if (btnOpen.ID.ToUpper() == "IMGDOWNLOADLOCATIONS")
                //{
                //    string Message = "";
                //    decimal ID = -1;
                //    if (decimal.TryParse(btnOpen.CommandArgument, out ID) == false)
                //    {
                //        Message = "Invalid ID.";
                //        SetMainMessage(Message);
                //        return;
                //    }
                //    ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "PrintReport('CCBatch','" + btnOpen.CommandArgument + "');", true);
                //}
                //#endregion
                //#region Open Scan List
                //if (btnOpen.ID.ToUpper() == "IMGOPENRUNScanSUMMARY")
                //{
                //    string Message = "";
                //    decimal ID = -1;
                //    if (decimal.TryParse(btnOpen.CommandArgument, out ID) == false)
                //    {
                //        Message = "Invalid ID.";
                //        lblHeaderRunNewOpenBatchList.Text = Message;
                //        lblHeaderRunNewOpenBatchList.Visible = true;
                //        return;
                //    }


                //    hdnCycleInventoryCountIterationHeaderID.Value = btnOpen.CommandArgument;
                //    lblHeaderRunNewOpenScanList.Text = "Batch Scan List: " + btnOpen.CommandName;
                //    pnlGridViewBatchConroleList.Visible = true;
                //    pnlGridViewRunNewOpenBatchList.Visible = false;
                //    //ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "PrintReport('CCRUNHEADER','" + btnOpen.CommandArgument + "');", true);
                //}
                //#endregion
                //#region Open Physical Scan List
                //if (btnOpen.ID.ToUpper() == "IMGOPENRUNPHYSICALSCAN")
                //{
                //    string Message = "";
                //    decimal ID = -1;
                //    if (decimal.TryParse(btnOpen.CommandArgument, out ID) == false)
                //    {
                //        Message = "Invalid ID.";
                //        SetMainMessage(Message);
                //        return;
                //    }
                //    hdnCycleInventoryCountIterationHeaderID.Value = btnOpen.CommandArgument;
                //    lblHeaderRunNewOpenScanList.Text = "Batch Scan List: " + btnOpen.CommandName;
                //    pnlGridViewBatchScanList.Visible = true;
                //    pnlGridViewRunNewOpenBatchList.Visible = false;
                //    //ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "PrintReport('CCRUNHEADER','" + btnOpen.CommandArgument + "');", true);
                //}
                //#endregion
                //#region IMGOpen
                //if (btnOpen.ID.ToUpper() == "IMGOPEN")
                //{
                //    string Message = "";
                //    decimal ID = -1;
                //    if (decimal.TryParse(btnOpen.CommandArgument, out ID) == false) { Message = "Invalid ID."; return; }
                //    CycleCountManager ccm = new CycleCountManager(User.Identity.Name);
                //    ccm.CC_ActivateBatchScan(ID, ref Message);
                //    SetMainMessage(Message);
                //    UpdateBatchGrid();
                //}
                //#endregion
                //#region Invalidate
                //if (btnOpen.ID.ToUpper() == "IMGINVALID")
                //{
                //    string Message = "";
                //    decimal ID = -1;
                //    if (decimal.TryParse(btnOpen.CommandArgument, out ID) == false) { Message = "Invalid ID."; return; }
                //    CycleCountManager ccm = new CycleCountManager(User.Identity.Name);
                //    ccm.SetBatchStatus(ID, "Invalid", ref Message);
                //    SetMainMessage(Message);
                //    UpdateBatchGrid();
                //}
                //#endregion
                //#region imgLocked
                //if (btnOpen.ID.ToUpper() == "IMGLOCKED")
                //{
                //    string Message = "";
                //    decimal ID = -1;
                //    if (decimal.TryParse(btnOpen.CommandArgument, out ID) == false) { Message = "Invalid ID."; return; }
                //    CycleCountManager ccm = new CycleCountManager(User.Identity.Name);
                //    ccm.SetBatchStatus(ID, "Locked", ref Message);
                //    SetMainMessage(Message);
                //    UpdateBatchGrid();
                //}
                //#endregion
                //#region imgHold
                //if (btnOpen.ID.ToUpper() == "IMGHOLD")
                //{
                //    string Message = "";
                //    decimal ID = -1;
                //    if (decimal.TryParse(btnOpen.CommandArgument, out ID) == false) { Message = "Invalid ID."; return; }
                //    CycleCountManager ccm = new CycleCountManager(User.Identity.Name);
                //    ccm.SetBatchStatus(ID, "Hold", ref Message);
                //    SetMainMessage(Message);
                //    UpdateBatchGrid();
                //}
                //#endregion
                //#region imgCloneIntoNew
                //if (btnOpen.ID.ToUpper() == "IMGCLONEINTONEW")
                //{
                //    string Message = "";
                //    decimal ID = -1;
                //    if (decimal.TryParse(btnOpen.CommandArgument, out ID) == false) { Message = "Invalid ID."; return; }
                //    CycleCountManager ccm = new CycleCountManager(User.Identity.Name);
                //    ccm.BatchCloneToNew(ID, ref Message);
                //    SetMainMessage(Message);
                //    UpdateBatchGrid();
                //}
                //#endregion
            }
        }
        void gvRunScanDetail_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                //    GridCCRunBatchesScan data = (GridCCRunBatchesScan)e.Row.DataItem;

                //    ImageButton bIOpen = (ImageButton)e.Row.FindScan("imgOpen");
                //    ImageButton bILocked = (ImageButton)e.Row.FindScan("imgLocked");
                //    ImageButton bInvalid = (ImageButton)e.Row.FindScan("imgInvalid");
                //    ImageButton bIHold = (ImageButton)e.Row.FindScan("imgHold");

                //    ImageButton bInvalidate = (ImageButton)e.Row.FindControl("imgInactivate");
                //    ImageButton bGenerateRunCycle = (ImageButton)e.Row.FindControl("imgloadRunData");
                //    ImageButton bDownload = (ImageButton)e.Row.FindControl("imgDownLoadLocations");
                //    ImageButton bOPenControlList = (ImageButton)e.Row.FindControl("imgOpenRunControlSummary");
                //    ImageButton bOPenScanList = (ImageButton)e.Row.FindControl("imgOpenRunPhysicalScan");
                //    ImageButton bCloneIntoNew = (ImageButton)e.Row.FindControl("imgCloneIntoNew");




                //    if (bIOpen != null) { bIOpen.CommandArgument = "-1"; }
                //    if (bILocked != null) { bILocked.CommandArgument = "-1"; }
                //    if (bInvalid != null) { bInvalid.CommandArgument = "-1"; }
                //    if (bIHold != null) { bIHold.CommandArgument = "-1"; }

                //    if (bInvalidate != null) { bInvalidate.CommandArgument = "-1"; }
                //    if (bGenerateRunCycle != null) { bGenerateRunCycle.CommandArgument = "-1"; }
                //    if (bDownload != null) { bDownload.CommandArgument = "-1"; }
                //    if (bOPenControlList != null) { bOPenControlList.CommandArgument = "-1"; }
                //    if (bOPenScanList != null) { bOPenScanList.CommandArgument = "-1"; }
                //    if  != null) { bIOpen.CommandArgument = data.CycleInventoryCountIterationHeaderID.ToString(); bIOpen.CommandName = "Run:" + data.CycleInventoryCountHeaderID.ToString() + " Name:" + data.Name + "(" + data.Status + ")"; }
                //        if (bInvalid != null) { bInvalid.CommandArgument = data.CycleInventoryCountIterationHeaderID.ToString(); bInvalid.CommandName = "Run:" + data.CycleInventoryCountHeaderID.ToString() + " Name:" + data.Name + "(" + data.Status + ") Batch:" + data.Batch; }
                //        if (bIHold != (bCloneIntoNew != null) { bCloneIntoNew.CommandArgument = "-1"; }


                //    //if (bClean != null) { bClean.CommandArgument = "-1"; }
                //    //if (bHold != null) { bHold.CommandArgument = "-1"; }
                //    if (data != null)
                //    {
                //        if (bILocked != null) { bILocked.CommandArgument = data.CycleInventoryCountIterationHeaderID.ToString(); bILocked.CommandName = "Run:" + data.CycleInventoryCountHeaderID.ToString() + " Name:" + data.Name + "(" + data.Status + ") Batch:" + data.Batch; }
                //        if (bIOpennull) { bIHold.CommandArgument = data.CycleInventoryCountIterationHeaderID.ToString(); bInvalid.CommandName = "Run:" + data.CycleInventoryCountHeaderID.ToString() + " Name:" + data.Name + "(" + data.Status + ") Batch:" + data.Batch; }


                //        if (bInvalidate != null) { bInvalidate.CommandArgument = data.CycleInventoryCountIterationHeaderID.ToString(); bInvalidate.CommandName = "Run:" + data.CycleInventoryCountHeaderID.ToString() + " Name:" + data.Name + "(" + data.Status + ") Batch:" + data.Batch; }
                //        if (bGenerateRunCycle != null) { bGenerateRunCycle.CommandArgument = data.CycleInventoryCountIterationHeaderID.ToString(); bGenerateRunCycle.CommandName = "Run:" + data.CycleInventoryCountHeaderID.ToString() + " Name:" + data.Name + "(" + data.Status + ") Batch:" + data.Batch; }
                //        if (bDownload != null) { bDownload.CommandArgument = data.CycleInventoryCountIterationHeaderID.ToString(); bDownload.CommandName = "Run:" + data.CycleInventoryCountHeaderID.ToString() + " Name:" + data.Name + "(" + data.Status + ") Batch:" + data.Batch; }
                //        if (bOPenControlList != null) { bOPenControlList.CommandArgument = data.CycleInventoryCountIterationHeaderID.ToString(); bOPenControlList.CommandName = "Run:" + data.Batch + " Name:" + data.Name + "(" + data.Status + ") Batch:" + data.Batch; }
                //        if (bOPenScanList != null) { bOPenScanList.CommandArgument = data.CycleInventoryCountIterationHeaderID.ToString(); bOPenScanList.CommandName = "Run:" + data.Batch + " Name:" + data.Name + "(" + data.Status + ") Batch:" + data.Batch; }
                //        if (bCloneIntoNew != null) { bCloneIntoNew.CommandArgument = data.CycleInventoryCountIterationHeaderID.ToString(); bCloneIntoNew.CommandName = "Run:" + data.Batch + " Name:" + data.Name + "(" + data.Status + ") Batch:" + data.Batch; }
                //    }
            }
        }
        #endregion
        //void btnPasteParseAdd_Click(object sender, EventArgs e)
        //{
        //    decimal ID = -1;
        //    string Message = "";
        //    if (decimal.TryParse(hdnCCTemplateID.Value, out ID) == false) { ID = -1; }
        //    CycleCountManager ccm = new CycleCountManager(User.Identity.Name);
        //    List<IFSLocation> LList = new List<IFSLocation>();
        //    string Name = txtTemplateName.Text;
        //    string Note = txtTemplateNote.Text;
        //    string LocationList = new ParseHelper().Parse(PasteDeliminator, txtPasteParse);
        //    List<string> data = LocationList.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        //    foreach (string x in data)
        //    {
        //        if (x.Length > 0)
        //        {
        //            IFSLocation L = new IFSLocation(x.Trim());
        //            LList.Add(L);
        //        }
        //    }
        //    ID = ccm.AddTemplateLocations(ID, drpTemplateStatus.SelectedItem.Value, Name, Note, LList, ref Message);
        //    hdnCCTemplateID.Value = ID.ToString();
        //    lblAddTemplateMessage.Text = Message;
        //}
        //void btnPasteParseDel_Click(object sender, EventArgs e)
        //{
        //    decimal ID = -1;
        //    string Message = "";
        //    if (decimal.TryParse(hdnCCTemplateID.Value, out ID) == false) { ID = -1; }
        //    CycleCountManager ccm = new CycleCountManager(User.Identity.Name);
        //    List<IFSLocation> LList = new List<IFSLocation>();
        //    string Name = txtTemplateName.Text;
        //    string Note = txtTemplateNote.Text;
        //    string LocationList = new ParseHelper().Parse(PasteDeliminator, txtPasteParse);
        //    List<string> data = LocationList.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        //    foreach (string x in data)
        //    {
        //        if (x.Length > 0)
        //        {
        //            IFSLocation L = new IFSLocation(x.Trim());
        //            LList.Add(L);
        //        }
        //    }
        //    ID = ccm.DeleteTemplateLocations(ID, drpTemplateStatus.SelectedItem.Value, Name, Note, LList, ref Message);
        //    hdnCCTemplateID.Value = ID.ToString();
        //    lblAddTemplateMessage.Text = Message;
        //}
        void btnAddTemplate_Click(object sender, EventArgs e)
        {
            decimal ID = -1;
            string Message = "";
            if (decimal.TryParse(hdnCCTemplateID.Value, out ID) == false) { ID = -1; }
            CycleCountManager ccm = new CycleCountManager(User.Identity.Name);
            List<IFSLocation> LList = new List<IFSLocation>();
            string Name = txtTemplateName.Text;
            string Note = txtTemplateNote.Text;
            string IFSSite = drpMoveIFSSite.SelectedItem.Text;
            if (Name.Length == 0)
            {
                lblAddTemplateMessage.Text = "You need to supply a Name";
                return;
            }
            //string LocationList = new ParseHelper().Parse(PasteDeliminator, txtPasteParse);
            //List<string> data = LocationList.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            //foreach (string x in data)
            //{
            //    if (x.Length > 0)
            //    {
            //        IFSLocation L = new IFSLocation(x.Trim());
            //        LList.Add(L);
            //    }
            //}
            ID = ccm.AddTemplate(ID, drpTemplateStatus.SelectedItem.Value, Name, Note, IFSSite, txtFromLocation.Text, txtFromCondition.Text, txtFromSkuCarrier.Text, txtFromSkuManufacturer.Text, txtFromSkuModel.Text, txtFromSkuColour.Text, ref Message);
            hdnCCTemplateID.Value = ID.ToString();
            lblAddTemplateMessage.Text = Message;
        }


        void btnRefreshAddEditUpdate_Click(object sender, EventArgs e)
        {
            UpdateTemplateEditPage();
        }

        private void UpdateTemplateEditPage()
        {
            lblAddTemplateMessage.Text = "";
            if (txtTemplateName.Text.Length == 0)
            {
                lblAddTemplateMessage.Text = "Template Name not set.";
                return;
            }
            CycleCountManager ccm = new CycleCountManager(User.Identity.Name);
            CycleInventoryCountTemplateHeader Data = ccm.GetTemplate(txtTemplateName.Text);
            txtFromSkuCarrier.Text = "";
            txtFromSkuManufacturer.Text = "";
            txtFromSkuModel.Text = "";
            txtFromSkuColour.Text = "";
            txtFromLocation.Text = "";
            txtFromCondition.Text = "";
            txtTemplateNote.Text = "";
            if (Data != null)
            {
                ListItem _ListItem = drpTemplateStatus.Items.FindByText(Data.Status);
                if (_ListItem == null) { drpTemplateStatus.SelectedIndex = 0; }
                else { drpTemplateStatus.SelectedIndex = drpTemplateStatus.Items.IndexOf(_ListItem); }

                txtTemplateName.Text = Data.Name;
                txtTemplateNote.Text = Data.Note;

                _ListItem = drpMoveIFSSite.Items.FindByText(Data.IFSSite);
                if (_ListItem == null) { drpMoveIFSSite.SelectedIndex = 0; }
                else { drpMoveIFSSite.SelectedIndex = drpMoveIFSSite.Items.IndexOf(_ListItem); }

                txtFromSkuCarrier.Text = Data.Carriers;
                txtFromSkuManufacturer.Text = Data.Manufacturers;
                txtFromSkuModel.Text = Data.Models;
                txtFromSkuColour.Text = Data.Colours;
                txtFromLocation.Text = Data.IFSLocation;
                txtFromCondition.Text = Data.IFSCondition;
            }
        }
        void btnAddTemplateClear_Click(object sender, EventArgs e)
        {
            ClearTemplateEditPage();
        }

        private void ClearTemplateEditPage()
        {
            lblAddTemplateMessage.Text = "";
            txtTemplateNote.Text = "";
            txtTemplateName.Text = "";
            txtFromSkuCarrier.Text = "";
            txtFromSkuManufacturer.Text = "";
            txtFromSkuModel.Text = "";
            txtFromSkuColour.Text = "";
            txtFromLocation.Text = "";
            txtFromCondition.Text = "";
        }







        void SetMainMessage(string Message)
        {
            lblMainMessage.Text = Message;
        }
        #endregion









        #region MISC
        void LoadDropdowns()
        {

            IFS_InventoryPartsManager ipm = new IFS_InventoryPartsManager(User.Identity.Name);
            int Count = 0;
            List<string> s = ipm.GetSiteList();
            //drpIFSSite.Items.Clear();
            drpMoveIFSSite.Items.Clear();
            foreach (string i in s)
            {
                Count++;
                //ListItem x = new ListItem(i, Count.ToString());
                ListItem y = new ListItem(i, Count.ToString());
                //if (x.Text == "C1NA") { x.Selected = true; }
                //drpIFSSite.Items.Add(x);
                drpMoveIFSSite.Items.Add(y);
            }
        }

        void imgDownloadGrid_Click(object sender, ImageClickEventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "PrintReportB(\"THISONE\",\"" + hdnReportThisData.Value + "\");", true);
        }
        ////

        //void btnRefreshSentIFS_Click(object sender, EventArgs e)
        //{
        //    UpdateSentIFSGrid();
        //}
        //void btnRefreshOpen_Click(object sender, EventArgs e)
        //{
        //    UpdateOpenGrid();
        //    lblMessageOpen.Text = "";
        //}
        //void btnRefreshInvalid_Click(object sender, EventArgs e)
        //{
        //    UpdateInvalidGrid();
        //    lblMessageInvalid.Text = "";
        //}
        //void btnRefreshLocked_Click(object sender, EventArgs e)
        //{
        //    UpdateLockedGrid();
        //    lblMessageLock.Text = "";
        //}
        //void btnRefreshHold_Click(object sender, EventArgs e)
        //{
        //    UpdateHoldGrid();
        //    lblMessageHold.Text = "";
        //}

        //void GRDLockedBatches_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    if (e.CommandName != "Page")
        //    {
        //        ImageButton btnOpen = (ImageButton)e.CommandSource;
        //        //string CommandArgument = btnOpen.CommandArgument;
        //        #region Invalidate
        //        if (btnOpen.ID.ToUpper() == "IMGINVALIDATE")
        //        {
        //            IFS_InventoryManager im = new IFS_InventoryManager(User.Identity.Name);
        //            lblMessageLock.Text = im.LogPhysicalInventoryBatchInvalid(btnOpen.CommandArgument);
        //            UpdateLockedGrid();
        //        }
        //        #endregion
        //        #region Download
        //        if (btnOpen.ID.ToUpper() == "IMGDOWNLOAD")
        //        {
        //            ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "PrintReport('" + btnOpen.CommandArgument + "');", true);
        //        }
        //        #endregion
        //        #region Open
        //        if (btnOpen.ID.ToUpper() == "IMGOPEN")
        //        {
        //            IFS_InventoryManager im = new IFS_InventoryManager(User.Identity.Name);
        //            lblMessageLock.Text = im.LogPhysicalInventoryBatchOpen(btnOpen.CommandArgument);
        //            UpdateLockedGrid();
        //        }
        //        #endregion
        //        #region Clean
        //        if (btnOpen.ID.ToUpper() == "IMGCLEAN")
        //        {
        //            IFS_InventoryManager im = new IFS_InventoryManager(User.Identity.Name);
        //            lblMessageLock.Text = im.LogPhysicalInventoryBatchClean(btnOpen.CommandArgument);
        //            UpdateLockedGrid();
        //        }

        //        #endregion
        //        #region Hold
        //        if (btnOpen.ID.ToUpper() == "IMGHOLD")
        //        {
        //            IFS_InventoryManager im = new IFS_InventoryManager(User.Identity.Name);
        //            lblMessageLock.Text = im.LogPhysicalInventoryBatchHold(btnOpen.CommandArgument);
        //            UpdateLockedGrid();
        //        }

        //        #endregion
        //    }
        //}
        //void GRDLockedBatches_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {

        //        vwGridPhysicalInventoryCount_B data = (vwGridPhysicalInventoryCount_B)e.Row.DataItem;
        //        ImageButton bInvalidate = (ImageButton)e.Row.FindControl("imgInvalidate");
        //        ImageButton bDownload = (ImageButton)e.Row.FindControl("imgDownload");
        //        ImageButton btnOpen = (ImageButton)e.Row.FindControl("imgOpen");
        //        ImageButton bClean = (ImageButton)e.Row.FindControl("imgClean");
        //        ImageButton bHold = (ImageButton)e.Row.FindControl("imgHold");

        //        if (bInvalidate != null) { bInvalidate.CommandArgument = "-1"; }
        //        if (bDownload != null) { bDownload.CommandArgument = "-1"; }
        //        if (btnOpen != null) { btnOpen.CommandArgument = "-1"; }
        //        if (bClean != null) { bClean.CommandArgument = "-1"; }
        //        if (bHold != null) { bHold.CommandArgument = "-1"; }
        //        if (data != null)
        //        {
        //            if (bInvalidate != null) { bInvalidate.CommandArgument = data.Batch; }
        //            if (bDownload != null) { bDownload.CommandArgument = data.Batch; }
        //            if (btnOpen != null) { btnOpen.CommandArgument = data.Batch; }
        //            if (bClean != null) { bClean.CommandArgument = data.Batch; }
        //            if (bHold != null) { bHold.CommandArgument = data.Batch; }

        //        }
        //    }
        //}

        //void GRDInvalidBatches_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    if (e.CommandName != "Page")
        //    {
        //        ImageButton btnOpen = (ImageButton)e.CommandSource;
        //        #region Open
        //        if (btnOpen.ID.ToUpper() == "IMGOPEN")
        //        {
        //            IFS_InventoryManager im = new IFS_InventoryManager(User.Identity.Name);
        //            lblMessageInvalid.Text = im.LogPhysicalInventoryBatchOpen(btnOpen.CommandArgument);
        //            UpdateInvalidGrid();
        //        }
        //        #endregion
        //        #region Download
        //        if (btnOpen.ID.ToUpper() == "IMGDOWNLOAD")
        //        {
        //            ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "PrintReport('" + btnOpen.CommandArgument + "');", true);
        //        }
        //        #endregion
        //        //UpdateInvalidGrid();
        //    }
        //}
        //void GRDInvalidBatches_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        vwGridPhysicalInventoryCount_B data = (vwGridPhysicalInventoryCount_B)e.Row.DataItem;
        //        ImageButton bOpen = (ImageButton)e.Row.FindControl("imgOpen");
        //        ImageButton bDownload = (ImageButton)e.Row.FindControl("imgDownload");
        //        if (bOpen != null) { bOpen.CommandArgument = "-1"; }
        //        if (bDownload != null) { bDownload.CommandArgument = "-1"; }
        //        if (data != null)
        //        {

        //            if (bOpen != null) { bOpen.CommandArgument = data.Batch; }
        //            if (bDownload != null) { bDownload.CommandArgument = data.Batch; }
        //        }
        //    }

        //}

        //void GRDOpenBatches_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    if (e.CommandName != "Page")
        //    {
        //        ImageButton btnOpen = (ImageButton)e.CommandSource;
        //        #region Lock
        //        if (btnOpen.ID.ToUpper() == "IMGLOCK")
        //        {
        //            IFS_InventoryManager im = new IFS_InventoryManager(User.Identity.Name);
        //            lblMessageOpen.Text = im.LogPhysicalInventoryBatchLocked(btnOpen.CommandArgument);
        //            UpdateOpenGrid();
        //        }
        //        #endregion
        //        #region Invalidate
        //        if (btnOpen.ID.ToUpper() == "IMGINVALIDATE")
        //        {
        //            IFS_InventoryManager im = new IFS_InventoryManager(User.Identity.Name);
        //            lblMessageOpen.Text = im.LogPhysicalInventoryBatchInvalid(btnOpen.CommandArgument);
        //            UpdateOpenGrid();
        //        }
        //        #endregion
        //        #region Download
        //        if (btnOpen.ID.ToUpper() == "IMGDOWNLOAD")
        //        {
        //            ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "PrintReport('" + btnOpen.CommandArgument + "');", true);
        //        }
        //        #endregion
        //        //UpdateOpenGrid();
        //    }
        //}
        //void GRDOpenBatches_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        vwGridPhysicalInventoryCount_B data = (vwGridPhysicalInventoryCount_B)e.Row.DataItem;
        //        ImageButton bInvalidate = (ImageButton)e.Row.FindControl("imgInvalidate");
        //        ImageButton bLock = (ImageButton)e.Row.FindControl("imgLock");
        //        ImageButton bDownload = (ImageButton)e.Row.FindControl("imgDownload");
        //        if (bLock != null) { bLock.CommandArgument = "-1"; }
        //        if (bDownload != null) { bDownload.CommandArgument = "-1"; }
        //        if (bInvalidate != null) { bInvalidate.CommandArgument = "-1"; }
        //        if (data != null)
        //        {
        //            if (bInvalidate != null) { bInvalidate.CommandArgument = data.Batch; }
        //            if (bLock != null) { bLock.CommandArgument = data.Batch; }
        //            if (bDownload != null) { bDownload.CommandArgument = data.Batch; }
        //        }
        //    }
        //}

        //void GRDHoldBatches_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    if (e.CommandName != "Page")
        //    {
        //        ImageButton btnOpen = (ImageButton)e.CommandSource;
        //        #region Lock
        //        if (btnOpen.ID.ToUpper() == "IMGLOCK")
        //        {
        //            IFS_InventoryManager im = new IFS_InventoryManager(User.Identity.Name);
        //            lblMessageLock.Text = im.LogPhysicalInventoryBatchLocked(btnOpen.CommandArgument);
        //            UpdateHoldGrid();
        //        }
        //        #endregion
        //        #region Transfer
        //        if (btnOpen.ID.ToUpper() == "IMGTRANSFER")
        //        {
        //            IFS_InventoryManager im = new IFS_InventoryManager(User.Identity.Name);
        //            lblMessageLock.Text = im.LogPhysicalInventoryBatchToIFS(btnOpen.CommandArgument);
        //            UpdateHoldGrid();
        //        }
        //        #endregion
        //        #region Download
        //        if (btnOpen.ID.ToUpper() == "IMGDOWNLOAD")
        //        {
        //            ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "PrintReport('" + btnOpen.CommandArgument + "');", true);
        //            //UpdateOpenGrid();
        //        }
        //        #endregion
        //        ;
        //    }
        //}
        //void GRDHoldBatches_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    vwGridPhysicalInventoryCount_B data = (vwGridPhysicalInventoryCount_B)e.Row.DataItem;
        //    ImageButton bLock = (ImageButton)e.Row.FindControl("imgLock");
        //    ImageButton bDownload = (ImageButton)e.Row.FindControl("imgDownload");
        //    ImageButton bTransferIFS = (ImageButton)e.Row.FindControl("imgTransfer");

        //    //ImageButton bHold = (ImageButton)e.Row.FindControl("imgHold");

        //    if (bLock != null) { bLock.CommandArgument = "-1"; }
        //    if (bDownload != null) { bDownload.CommandArgument = "-1"; }
        //    if (bTransferIFS != null) { bTransferIFS.CommandArgument = "-1"; }
        //    //if (bClean != null) { bClean.CommandArgument = "-1"; }
        //    //if (bHold != null) { bHold.CommandArgument = "-1"; }
        //    if (data != null)
        //    {
        //        if (bLock != null) { bLock.CommandArgument = data.Batch; }
        //        if (bDownload != null) { bDownload.CommandArgument = data.Batch; }
        //        if (bTransferIFS != null) { bTransferIFS.CommandArgument = data.Batch; }
        //        //if (bClean != null) { bClean.CommandArgument = data.Batch; }
        //        //if (bHold != null) { bHold.CommandArgument = data.Batch; }

        //    }
        //}
        //void GRDSentIFSBatches_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    if (e.CommandName != "Page")
        //    {
        //        ImageButton btnOpen = (ImageButton)e.CommandSource;
        //        #region Download
        //        if (btnOpen.ID.ToUpper() == "IMGDOWNLOAD")
        //        {
        //            ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "PrintReport('" + btnOpen.CommandArgument + "');", true);
        //        }
        //        #endregion
        //    }
        //}
        //void GRDSentIFSBatches_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        vwGridPhysicalInventoryCount_B data = (vwGridPhysicalInventoryCount_B)e.Row.DataItem;
        //        ImageButton bDownload = (ImageButton)e.Row.FindControl("imgDownload");
        //        if (bDownload != null) { bDownload.CommandArgument = "-1"; }
        //        if (data != null)
        //        {
        //            if (bDownload != null) { bDownload.CommandArgument = data.Batch; }
        //        }
        //    }
        //}

        //private void UpdateLockedGrid()
        //{
        //    IFS_InventoryManager im = new IFS_InventoryManager(User.Identity.Name);
        //    GRDLockedBatches.DataSource = im.GetPhysicalCountData("Locked");
        //    GRDLockedBatches.DataBind();
        //}
        //private void UpdateHoldGrid()
        //{
        //    IFS_InventoryManager im = new IFS_InventoryManager(User.Identity.Name);
        //    GRDHoldBatches.DataSource = im.GetPhysicalCountData("Hold");
        //    GRDHoldBatches.DataBind();
        //}
        //private void UpdateInvalidGrid()
        //{
        //    IFS_InventoryManager im = new IFS_InventoryManager(User.Identity.Name);
        //    GRDInvalidBatches.DataSource = im.GetPhysicalCountData("Invalid");
        //    GRDInvalidBatches.DataBind();
        //}
        //private void UpdateOpenGrid()
        //{
        //    IFS_InventoryManager im = new IFS_InventoryManager(User.Identity.Name);
        //    GRDOpenBatches.DataSource = im.GetPhysicalCountData("Open");
        //    GRDOpenBatches.DataBind();
        //}
        //private void UpdateSentIFSGrid()
        //{
        //    IFS_InventoryManager im = new IFS_InventoryManager(User.Identity.Name);
        //    GRDSentIFSBatches.DataSource = im.GetPhysicalCountData("SentIFS");
        //    GRDSentIFSBatches.DataBind();
        //}
        #endregion


    }
}