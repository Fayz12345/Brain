using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BW_WebApp.DataManagers;

namespace BW_WebApp
{
    public partial class CC_Control : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            SpreadTabs.ActiveTabChanged += new EventHandler(SpreadTabs_ActiveTabChanged);

            TabRuns.ActiveTabChanged += new EventHandler(TabRuns_ActiveTabChanged);
            //-------------------------------------------
            btnAddTemplate.Click += new EventHandler(btnAddTemplate_Click);
            btnRefreshAddEditUpdate.Click += new EventHandler(btnRefreshAddEditUpdate_Click);
            btnAddTemplateClear.Click += new EventHandler(btnAddTemplateClear_Click);

            btnRefreshTemplateHeaders.Click += new EventHandler(btnRefreshTemplateHeaders_Click);
            grdTemplateHeaders.RowDataBound += new GridViewRowEventHandler(grdTemplateHeaders_RowDataBound);
            grdTemplateHeaders.RowCommand += new GridViewCommandEventHandler(grdTemplateHeaders_RowCommand);


            gvSpreadOther.RowDataBound += new GridViewRowEventHandler(gvSpreadOther_RowDataBound);
            gvSpreadOther.RowCommand += new GridViewCommandEventHandler(gvSpreadOther_RowCommand);




            btnRefreshTemplateHeadersInactive.Click += new EventHandler(btnRefreshTemplateHeadersInactive_Click);
            grdTemplateHeadersInactive.RowDataBound += new GridViewRowEventHandler(grdTemplateHeadersInactive_RowDataBound);
            grdTemplateHeadersInactive.RowCommand += new GridViewCommandEventHandler(grdTemplateHeadersInactive_RowCommand);

            gvRunSpread.RowDataBound += new GridViewRowEventHandler(gvRunSpread_RowDataBound);
            gvRunSpread.RowCommand += new GridViewCommandEventHandler(gvRunSpread_RowCommand);
            //gvRunSpread.SelectedIndexChanged += new EventHandler(gvRunSpread_SelectedIndexChanged);
            btnRefreshHeadersNew.Click += new EventHandler(btnRefreshHeadersNew_Click);
            btnRefreshHeadersNew0.Click += new EventHandler(btnRefreshHeadersNew_Click);
            btnRefreshHeadersNew1.Click += new EventHandler(btnRefreshHeadersNew_Click);
            btnRefreshHeadersNew2.Click += new EventHandler(btnRefreshHeadersNew_Click);
            btnRefreshHeadersNew3.Click += new EventHandler(btnRefreshHeadersNew_Click);
            btnRefreshHeadersNew4.Click += new EventHandler(btnRefreshHeadersNew_Click);

            grdRunsNew.RowDataBound += new GridViewRowEventHandler(grdRunsNew_RowDataBound);
            grdRunsNew.RowCommand += new GridViewCommandEventHandler(grdRunsNew_RowCommand);
            grdRunsNew.SelectedIndexChanged += new EventHandler(grdRunsNew_SelectedIndexChanged);

            imgDownloadGrid.Click += new ImageClickEventHandler(imgDownloadGrid_Click);
            PrintTemplateTabActive.Click += new ImageClickEventHandler(imgDownloadGrid_Click);
            PrintTemplateTabInActive.Click += new ImageClickEventHandler(imgDownloadGrid_Click);

            btnRefreshControlData.Click +=new EventHandler(btnRefreshControlData_Click);
            btnRefreshSpreadData.Click += new EventHandler(btnRefreshSpreadData_Click);
            btnRefreshScanResults.Click += new EventHandler(btnRefreshScanResults_Click);
            btnSpreadOtherRefresh.Click += new EventHandler(btnSpreadOtherRefresh_Click);
            ImageButton3.Click += new ImageClickEventHandler(ImageButton3_Click);
            ImageButton1.Click += new ImageClickEventHandler(ImageButton1_Click);
            ImageButton2.Click +=new ImageClickEventHandler(ImageButton2_Click);
            ImgdownloadTab01.Click +=new ImageClickEventHandler(ImgdownloadTab01_Click);

            if (IsPostBack == false)
            {
                LoadDropdowns();
                hdnUserName.Value = User.Identity.Name;
                //UpdateLockedGrid();
                UpdateTemplateGrid();
                UpdategrdRuns();
                lblHeaderRunMessageNew.Text = "Count Runs";
                //UpdateTemplateGridInactive();
            }

        }

        void ImageButton3_Click(object sender, ImageClickEventArgs e)
        {
            hdnReportThisData.Value = lblResultQuerry.Text;            // hdnReportThisData_Other.Value;
            imgDownloadGrid_Click(sender, e);
        }
        void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            hdnReportThisData.Value = lblOtherQuerry.Text;            // hdnReportThisData_Other.Value;
            imgDownloadGrid_Click(sender, e);
        }
        void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            hdnReportThisData.Value = lblControlQuerry.Text;                    // hdnReportThisData_Control.Value;
            //string xxx = "";
            //xxx = HdnControlQ.Value;
            imgDownloadGrid_Click(sender, e);
        }
        void ImgdownloadTab01_Click(object sender, ImageClickEventArgs e)
        {
            hdnReportThisData.Value = lblSpreadQuerry.Text;                     //hdnReportThisData_Spread.Value;
            imgDownloadGrid_Click(sender, e);
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
                    if (decimal.TryParse(btnOpen.CommandArgument, out ID) == false) { Message = "Invalid ID.";  return; }
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
        #region Misc
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
        #endregion
        #endregion

        
        
        #region Runs - New
        void btnRefreshHeadersNew_Click(object sender, EventArgs e)
        {
            UpdategrdRuns();
        }
        void btnRefreshControlData_Click(object sender, EventArgs e)
        {
            RefreshControl();
        }
        void btnRefreshScanResults_Click(object sender, EventArgs e)
        {
            RefreshScanResult();
        }
        private void UpdategrdRuns()
        {
            string QueryString = "";
            pnlGridViewRunNew.Visible = false;
            pnlGridViewRunSpread.Visible = false;
            string ActiveTab = TabRuns.ActiveTab.HeaderText.ToUpper();
            pnlGridViewRunNew.Visible = true;
            CycleCountManager im = new CycleCountManager(User.Identity.Name);
            grdRunsNew.DataSource = im.GridDataRun(TabRuns.ActiveTab.HeaderText, ref QueryString).OrderBy(x => x.CycleInventoryCountHeaderID);
            grdRunsNew.DataBind();
            hdnReportThisData.Value = QueryString;
            #region
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
            #endregion
        }
        private void RefreshControl()
        {
                decimal ID = -1;
                if (decimal.TryParse(hdnCycleInventoryCountHeaderID.Value, out ID) == false) { ID = -1; }
                if (grdRunsNew.SelectedDataKey != null && decimal.TryParse(grdRunsNew.SelectedDataKey.Value.ToString(), out ID) == true)
                {
                    string QueryString = "";
                    bool SummaryType = true;
                    if (ControlType.SelectedItem.Value != "Y") { SummaryType = false; }
                    CycleCountManager im = new CycleCountManager(User.Identity.Name);
                    grdControl.DataSource = im.GridDataBatchControl(ID, "New", SummaryType, true, false, ref QueryString).OrderBy(x => x.IFSLocation);
                    grdControl.DataBind();
                    lblControlQuerry.Text = QueryString;
                    //hdnReportThisData_Control.Value = QueryString;
                    //HdnControlQ.Value = QueryString;
                }
        }
        private void RefreshScanResult()
        {
            decimal ID = -1;
            if (decimal.TryParse(hdnCycleInventoryCountHeaderID.Value, out ID) == false) { ID = -1; }
            if (grdRunsNew.SelectedDataKey != null && decimal.TryParse(grdRunsNew.SelectedDataKey.Value.ToString(), out ID) == true)
            {
                string QueryString = "";
                //bool SummaryType = true;
                //if (ResultType.SelectedItem.Value != "Y") { SummaryType = false; }


                for (int i = 0; i < grdResultData.Columns.Count; i++)
                {
                    grdResultData.Columns[i].Visible = true;
                }
                string ReportLevel = (ShowLevel1.Checked == true ? "1" : "") + (ShowLevel2.Checked == true ? "2" : "") + (ShowLevel3.Checked == true ? "3" : "");
                CycleCountManager im = new CycleCountManager(User.Identity.Name);
                grdResultData.DataSource = im.GridDataBatchScanResult(ID, "New", ResultType.SelectedItem.Value, ReportLevel, true, false, ref QueryString).OrderBy(x => x.IFSLocation);
                grdResultData.DataBind();
                lblResultQuerry.Text = QueryString;
                 List<String> TurnColumnsOn = new List<string>(); 
                for (int i = 0; i < grdResultData.Columns.Count; i++)
                {
                    TurnColumnsOn.Add(grdResultData.Columns[i].HeaderText.ToUpper());
                    grdResultData.Columns[i].Visible = false;
                }
                TurnColumnsOn.Remove("RUN #");
                TurnColumnsOn.Remove("C_ID");
                TurnColumnsOn.Remove("S_ID");
                TurnColumnsOn.Remove("RDID");
                if (ResultType.SelectedItem.Value == "1")
                {
                }
                if (ResultType.SelectedItem.Value == "2")
                {
                    TurnColumnsOn.Remove("NOTE");
                    TurnColumnsOn.Remove("BATCH");
                    TurnColumnsOn.Remove("BATCH_STATUS");
                    TurnColumnsOn.Remove("ISBATCHLOCKED");
                    TurnColumnsOn.Remove("IMEI");
                    TurnColumnsOn.Remove("VERSION");
                    TurnColumnsOn.Remove("STATUSMESSAGE");
                }
                if (ResultType.SelectedItem.Value == "3")
                {
                    TurnColumnsOn.Remove("NOTE");
                    TurnColumnsOn.Remove("BATCH");
                    TurnColumnsOn.Remove("BATCH_STATUS");
                    TurnColumnsOn.Remove("ISBATCHLOCKED");
                    TurnColumnsOn.Remove("IMEI");
                    TurnColumnsOn.Remove("VERSION");
                    TurnColumnsOn.Remove("STATUSMESSAGE");
                    TurnColumnsOn.Remove("IFSSITE");
                    TurnColumnsOn.Remove("IFSPROJECT");
                    TurnColumnsOn.Remove("IMEI");
                    TurnColumnsOn.Remove("VERSION");
                    TurnColumnsOn.Remove("SKU");
                    TurnColumnsOn.Remove("IFSCONDITION");
                }
                if (ResultType.SelectedItem.Value == "4")
                {
                    TurnColumnsOn.Remove("NOTE");
                    TurnColumnsOn.Remove("BATCH");
                    TurnColumnsOn.Remove("BATCH_STATUS");
                    TurnColumnsOn.Remove("ISBATCHLOCKED");
                    TurnColumnsOn.Remove("IMEI");
                    TurnColumnsOn.Remove("VERSION");
                    TurnColumnsOn.Remove("STATUSMESSAGE");
                    TurnColumnsOn.Remove("IFSSITE");
                    TurnColumnsOn.Remove("IFSPROJECT");
                    TurnColumnsOn.Remove("IMEI");
                    TurnColumnsOn.Remove("VERSION");
                    TurnColumnsOn.Remove("SKU");
                    TurnColumnsOn.Remove("IFSCONDITION");
                    TurnColumnsOn.Remove("IFSLOCATION");
                    TurnColumnsOn.Remove("IFSZONELOCATION");
                }

                VisualizeGridColumn(grdResultData, TurnColumnsOn, true);

            }
        }

        private void VisualizeGridColumn(GridView Grid, List<String> Columns, bool Visible)
        {
            for (int i = 0; i < Grid.Columns.Count; i++)
            {
                String header = Grid.Columns[i].HeaderText;
                if (Columns.Contains(header.ToUpper())) { Grid.Columns[i].Visible = Visible; }
            }
        }


        void TabRuns_ActiveTabChanged(object sender, EventArgs e)
        {
            SetMainMessage("");
            UpdategrdRuns();
        }
        void grdRunsNew_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSpreadGridScreen();
            //UpdateSpreadGridData();
        }
        void grdRunsNew_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            SetMainMessage("");
            if (e.CommandName == "Select")
            {

            }
            else if (e.CommandName != "Page")
            {
                ImageButton btnOpen = (ImageButton)e.CommandSource;
                #region Download
                if (btnOpen.ID.ToUpper() == "IMGDOWNLOADRUNDETAIL" || btnOpen.ID.ToUpper() == "IMGDOWNLOADRUNSUMMARYDETAIL" || btnOpen.ID.ToUpper() == "IMGDOWNLOADRUNSUMMARY")
                {
                    string Message = "";
                    decimal ID = -1;
                    if (decimal.TryParse(btnOpen.CommandArgument, out ID) == false)
                    {
                        Message = "Invalid ID.";
                        SetMainMessage(Message);
                        return;
                    }

                    if (btnOpen.ID.ToUpper() == "IMGDOWNLOADRUNDETAIL")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "PrintReport('CCRUNDETAIL','" + btnOpen.CommandArgument + "');", true); 
                        //if (TabRuns.ActiveTab.HeaderText == "New") { }
                        //if (TabRuns.ActiveTab.HeaderText == "Active") { }
                        //if (TabRuns.ActiveTab.HeaderText == "SFC Ready") { }
                        //ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "PrintReport('CCTEMPLATESUMMARY','" + btnOpen.CommandArgument + "');", true);
                    }
                    if (btnOpen.ID.ToUpper() == "IMGDOWNLOADRUNSUMMARYDETAIL")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "PrintReport('CCRUNSUMMARYDETAIL','" + btnOpen.CommandArgument + "');", true);
                        //if (TabRuns.ActiveTab.HeaderText == "New") { }
                        //if (TabRuns.ActiveTab.HeaderText == "Active") { ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "PrintReport('CCRUNSUMMARYDETAIL','" + btnOpen.CommandArgument + "');", true); }
                        //if (TabRuns.ActiveTab.HeaderText == "SFC Ready") { }
                        //ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "PrintReport('CCTEMPLATESUMMARY','" + btnOpen.CommandArgument + "');", true);
                    }
                    if (btnOpen.ID.ToUpper() == "IMGDOWNLOADRUNSUMMARY")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "PrintReport('CCRUNSUMMARY','" + btnOpen.CommandArgument + "');", true);
                        //if (TabRuns.ActiveTab.HeaderText == "New") { }
                        //if (TabRuns.ActiveTab.HeaderText == "Active") { ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "PrintReport('CCRUNSUMMARY','" + btnOpen.CommandArgument + "');", true); }
                        //if (TabRuns.ActiveTab.HeaderText == "SFC Ready") { }
                        //ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "PrintReport('CCTEMPLATESUMMARY','" + btnOpen.CommandArgument + "');", true);
                    }
                }

                



                #endregion
                #region Open Batch List
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
                #region Move To CLose
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
                #region Move To InActive
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
                ImageButton bDownload = (ImageButton)e.Row.FindControl("imgDownLoadRunDetail");
                ImageButton bDownload2 = (ImageButton)e.Row.FindControl("imgDownLoadRunSummaryDetail");
                ImageButton bDownload3 = (ImageButton)e.Row.FindControl("imgDownLoadRunSummary");
                ImageButton bimgIFSReady = (ImageButton)e.Row.FindControl("imgIFSReady");
                //ImageButton bDownloadDetail = (ImageButton)e.Row.FindControl("imgDownLoadTemplateDetail");
                ImageButton bOPenBatchList = (ImageButton)e.Row.FindControl("imgOpenBatchList");
                ImageButton bActivate = (ImageButton)e.Row.FindControl("imgactivate");
                ImageButton bInActivate = (ImageButton)e.Row.FindControl("imgInactivate");
                ImageButton bCLosedActivate = (ImageButton)e.Row.FindControl("imgClose");
                ImageButton bHold = (ImageButton)e.Row.FindControl("imgHold");
                ImageButton bNew = (ImageButton)e.Row.FindControl("imgNew");
                //ImageButton bEdit = (ImageButton)e.Row.FindControl("ImageEdit");

                if (bDownload != null) { bDownload.CommandArgument = "-1"; bDownload.Visible = false; }
                if (bDownload2 != null) { bDownload2.CommandArgument = "-1"; bDownload2.Visible = false; }
                if (bDownload3 != null) { bDownload3.CommandArgument = "-1"; bDownload3.Visible = false; }
                if (bimgIFSReady != null) { bimgIFSReady.CommandArgument = "-1"; bimgIFSReady.Visible = false; }
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
                    if (bDownload2 != null) { bDownload2.CommandArgument = data.CycleInventoryCountHeaderID.ToString(); bDownload2.CommandName = "Run:" + data.CycleInventoryCountHeaderID.ToString() + " Name:" + data.Name + "(" + data.Status + ")"; }
                    if (bDownload3 != null) { bDownload3.CommandArgument = data.CycleInventoryCountHeaderID.ToString(); bDownload3.CommandName = "Run:" + data.CycleInventoryCountHeaderID.ToString() + " Name:" + data.Name + "(" + data.Status + ")"; }

                    if (bimgIFSReady != null) { bimgIFSReady.CommandArgument = data.CycleInventoryCountHeaderID.ToString(); bimgIFSReady.CommandName = "Run:" + data.CycleInventoryCountHeaderID.ToString() + " Name:" + data.Name + "(" + data.Status + ")"; }
                    //if (bDownloadDetail != null) { bDownloadDetail.CommandArgument = data.CycleInventoryCountHeaderID.ToString(); bDownloadDetail.CommandName = "Run:" + data.CycleInventoryCountHeaderID.ToString() + " Name:" + data.Name + "(" + data.Status + ")"; }
                    
                    
                    if (bOPenBatchList != null) { bOPenBatchList.CommandArgument = data.CycleInventoryCountHeaderID.ToString(); bOPenBatchList.CommandName = "Run:" + data.CycleInventoryCountHeaderID.ToString() + " Name:" + data.Name + "(" + data.Status + ")"; }
                    if (bHold != null) { bHold.CommandArgument = data.CycleInventoryCountHeaderID.ToString(); bHold.CommandName = "Run:" + data.CycleInventoryCountHeaderID.ToString() + " Name:" + data.Name + "(" + data.Status + ")"; }
                    if (bNew != null) { bNew.CommandArgument = data.CycleInventoryCountHeaderID.ToString(); bNew.CommandName = "Run:" + data.CycleInventoryCountHeaderID.ToString() + " Name:" + data.Name + "(" + data.Status + ")"; }
                    //if (bEdit != null) { bEdit.CommandArgument = data.CycleInventoryCountHeaderID.ToString(); bEdit.CommandName = "Run:" + data.CycleInventoryCountHeaderID.ToString() + " Name:" + data.Name + "(" + data.Status + ")"; }
                }

                if (TabRuns.ActiveTab.HeaderText == "New")
                {

                    if (bDownload != null) { bDownload.Visible = true; }
                    if (bDownload2 != null) { bDownload2.Visible = true; }
                    //if (bDownload3 != null) { bDownload3.Visible = true; }

                    if (bActivate != null) { bActivate.Visible = true; }
                    if (bInActivate != null) { bInActivate.Visible = true; }
                    if (bCLosedActivate != null) { bCLosedActivate.Visible = true; }
                    if (bHold != null) { bHold.Visible = true; }

                }
                if (TabRuns.ActiveTab.HeaderText == "Active")
                {
                    if (bDownload != null) { bDownload.Visible = true; }
                    if (bDownload2 != null) { bDownload2.Visible = true; }
                    //if (bDownload3 != null) { bDownload3.Visible = true; }

                    //if (bOPenBatchList != null) { bOPenBatchList.Visible = true; }
                    if (bCLosedActivate != null) { bCLosedActivate.Visible = true; }
                    if (bHold != null) { bHold.Visible = true; }
                    if (bimgIFSReady != null) { bimgIFSReady.Visible = false; }
                }
                if (TabRuns.ActiveTab.HeaderText == "Hold")
                {
                    if (bDownload != null) { bDownload.Visible = true; }
                    if (bDownload2 != null) { bDownload2.Visible = true; }
                    //if (bDownload3 != null) { bDownload3.Visible = true; }
                    //if (bOPenBatchList != null) { bOPenBatchList.Visible = true; }
                    if (bInActivate != null) { bInActivate.Visible = true; }
                    if (bCLosedActivate != null) { bCLosedActivate.Visible = true; }
                    if (bHold != null) { bHold.Visible = true; }
                    if (bimgIFSReady != null) { bimgIFSReady.Visible = false; }
                }
                if (TabRuns.ActiveTab.HeaderText == "SFC Ready")
                {
                    if (bDownload != null) { bDownload.Visible = true; }
                    if (bDownload2 != null) { bDownload2.Visible = true; }
                    //if (bDownload3 != null) { bDownload3.Visible = true; }
                    //if (bOPenBatchList != null) { bOPenBatchList.Visible = true; }
                    if (bInActivate != null) { bInActivate.Visible = true; }
                    if (bCLosedActivate != null) { bCLosedActivate.Visible = true; }
                    if (bHold != null) { bHold.Visible = true; }
                }
                if (TabRuns.ActiveTab.HeaderText == "Closed")
                {
                    if (bDownload != null) { bDownload.Visible = true; }
                    if (bDownload2 != null) { bDownload2.Visible = true; }
                    //if (bDownload3 != null) { bDownload3.Visible = true; }
                    if (bActivate != null) { bActivate.Visible = true; }
                    if (bInActivate != null) { bInActivate.Visible = true; }
                    //if (bCLosedActivate != null) { bCLosedActivate.Visible = true; }
                    if (bHold != null) { bHold.Visible = true; }
                }
                if (TabRuns.ActiveTab.HeaderText == "Inactive")
                {
                    if (bNew != null) { bNew.Visible = true; }
                }
            }
        }
        #endregion

        #region Spread
        void btnRefreshSpreadData_Click(object sender, EventArgs e)
        {
            UpdategrdSpread();
        }
        private void UpdateSpreadGridScreen()
        {
            if (grdRunsNew.SelectedIndex >= 0)
            {
                //string QueryString = "";
                pnlGridViewRunSpread.Visible = false;
                decimal ID = -1;
                if (decimal.TryParse(hdnCycleInventoryCountHeaderID.Value, out ID) == false) { ID = -1; }
                if (grdRunsNew.SelectedDataKey != null && decimal.TryParse(grdRunsNew.SelectedDataKey.Value.ToString(), out ID) == true)
                {
                    pnlGridViewRunSpread.Visible = true;
                    CycleCountManager im = new CycleCountManager(User.Identity.Name);
                    //gvRunSpread.DataSource = im.BatchGridData(ID, TabBatches.ActiveTab.HeaderText, ref QueryString).OrderBy(x => x.IFSLocation);
                    //gvRunSpread.DataSource = im.BatchGridData(ID, ref QueryString).OrderBy(x => x.IFSLocation).ThenBy(x => x.IFSSite).ThenBy(x => x.IFSProject).ThenBy(x => x.SKU).ThenBy(x => x.IFSCondition);
                    //gvRunSpread.DataBind();
                    //lblSpreadQuerry.Text = QueryString;
                    UpdategrdSpread();
                }
            }
        }

        private void UpdateSpreadGridData()
        {
            if (grdRunsNew.SelectedIndex >= 0)
            {
                string QueryString = "";
                //pnlGridViewRunSpread.Visible = false;
                ////string QueryString = "";
                decimal ID = -1;
                if (decimal.TryParse(hdnCycleInventoryCountHeaderID.Value, out ID) == false) { ID = -1; }
                if (grdRunsNew.SelectedDataKey != null && decimal.TryParse(grdRunsNew.SelectedDataKey.Value.ToString(), out ID) == true)
                {
                //    pnlGridViewRunSpread.Visible = true;
                    CycleCountManager im = new CycleCountManager(User.Identity.Name);
                    //gvRunSpread.DataSource = im.BatchGridData(ID, TabBatches.ActiveTab.HeaderText, ref QueryString).OrderBy(x => x.IFSLocation);
                    gvRunSpread.DataSource = im.BatchGridData(ID, ref QueryString).OrderBy(x => x.IFSLocation).ThenBy(x => x.IFSSite).ThenBy(x => x.IFSProject).ThenBy(x => x.SKU).ThenBy(x => x.IFSCondition);
                    gvRunSpread.DataBind();
                    lblSpreadQuerry.Text = QueryString;
                    //UpdategrdSpread();
                }
            }
        }


        private void UpdategrdSpread()
        {
            //string QueryString = "";
            pnlSpreadOther.Visible = false;
            if (SpreadTabs.ActiveTab.HeaderText.ToUpper() == "SPREAD") { UpdateSpreadGridData(); }
            else if (SpreadTabs.ActiveTab.HeaderText.ToUpper() == "CONTROL") { RefreshControl(); }
            else if (SpreadTabs.ActiveTab.HeaderText.ToUpper() == "SCAN RESULTS") { RefreshScanResult(); }
            else
            {
                pnlSpreadOther.Visible = true;
                UpdateSpreadOtherGrid();
            }
            //hdnReportThisData.Value = QueryString;
        }
        void SpreadTabs_ActiveTabChanged(object sender, EventArgs e)
        {
            SetMainMessage("");
            UpdategrdSpread();
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
                if (btnOpen.ID.ToUpper() == "IMGDOWNLOADSPREADSUMMARY" || btnOpen.ID.ToUpper() == "IMGDOWNLOADSPREADSUMMARYDETAIL" || btnOpen.ID.ToUpper() == "IMGDOWNLOADSPREADDETAIL")
                {
                    //string Message = "";
                    //decimal ID = -1;
                    //if (decimal.TryParse(btnOpen.CommandArgument, out ID) == false)
                    //{
                    //    Message = "Invalid ID.";
                    //    SetMainMessage(Message);
                    //    return;
                    //}
                    if (btnOpen.ID.ToUpper() == "IMGDOWNLOADSPREADSUMMARY")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "PrintReport('IMGDOWNLOADSPREADSUMMARY','" + btnOpen.CommandArgument + "');", true);
                    }
                    if (btnOpen.ID.ToUpper() == "IMGDOWNLOADSPREADSUMMARYDETAIL")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "PrintReport('IMGDOWNLOADSPREADSUMMARYDETAIL','" + btnOpen.CommandArgument + "');", true);
                    }
                    if (btnOpen.ID.ToUpper() == "IMGDOWNLOADSPREADDETAIL")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "PrintReport('IMGDOWNLOADSPREADDETAIL','" + btnOpen.CommandArgument + "');", true);
                    }
                }
                #endregion
                
            }
        }
        void gvRunSpread_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vwGetCCRunHeaderBatch data = (vwGetCCRunHeaderBatch)e.Row.DataItem;
                ImageButton b1 = (ImageButton)e.Row.FindControl("imgDownloadSpreadSummary");
                ImageButton b2 = (ImageButton)e.Row.FindControl("imgDownloadSpreadSummaryDetail");
                ImageButton b3 = (ImageButton)e.Row.FindControl("imgDownloadSpreadDetail");


                ////ImageButton bDownloadDetailSummary = (ImageButton)e.Row.FindControl("imgDownLoadTemplateSummaryDetail");
                ////ImageButton bDownloadDetail = (ImageButton)e.Row.FindControl("imgDownLoadTemplateDetail");
                //ImageButton bOPenBatchList = (ImageButton)e.Row.FindControl("imgOpenBatchList");
                //ImageButton bActivate = (ImageButton)e.Row.FindControl("imgactivate");
                //ImageButton bInActivate = (ImageButton)e.Row.FindControl("imgInactivate");
                //ImageButton bCLosedActivate = (ImageButton)e.Row.FindControl("imgClose");
                //ImageButton bHold = (ImageButton)e.Row.FindControl("imgHold");
                //ImageButton bNew = (ImageButton)e.Row.FindControl("imgNew");
                ////ImageButton bEdit = (ImageButton)e.Row.FindControl("ImageEdit");


                if (b1 != null) { b1.CommandArgument = "-1"; b1.Visible = false; }
                if (b2 != null) { b2.CommandArgument = "-1"; b2.Visible = true; }
                if (b3 != null) { b3.CommandArgument = "-1"; b3.Visible = true; }

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


                if (data != null)
                {
                    if (b1 != null) { b1.CommandArgument = data.CycleInventoryCountHeaderID.ToString() + "," + data.IFSSite + "," + data.IFSProject + "," + data.SKU + "," + data.IFSCondition + "," + data.IFSLocation; }
                    if (b2 != null) { b2.CommandArgument = data.CycleInventoryCountHeaderID.ToString() + "," + data.IFSSite + "," + data.IFSProject + "," + data.SKU + "," + data.IFSCondition + "," + data.IFSLocation; }
                    if (b3 != null) { b3.CommandArgument = data.CycleInventoryCountHeaderID.ToString() + "," + data.IFSSite + "," + data.IFSProject + "," + data.SKU + "," + data.IFSCondition + "," + data.IFSLocation; }
                }

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
        #endregion

        #region Spread Other
        void btnSpreadOtherRefresh_Click(object sender, EventArgs e)
        {
            UpdateSpreadOtherGrid();
        }
        private void UpdateSpreadOtherGrid()
        {
            string QueryString = "";
            decimal ID = -1;
            if (grdRunsNew.SelectedDataKey != null && decimal.TryParse(grdRunsNew.SelectedDataKey.Value.ToString(), out ID) == false) { ID = -1; }
            CycleCountManager im = new CycleCountManager(User.Identity.Name);
            //IFS_InventoryManager im = new IFS_InventoryManager(User.Identity.Name);
            gvSpreadOther.DataSource = im.GetCycleCountData(ID, SpreadTabs.ActiveTab.HeaderText, ref QueryString);
            gvSpreadOther.DataBind();
            lblOtherQuerry.Text = QueryString;
        }

        void gvSpreadOther_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string Message = "";
            //SetMainMessage("");
            if (e.CommandName == "Select")
            {


            }
            else if (e.CommandName != "Page")
            {




                decimal ID = -1;
                if (grdRunsNew.SelectedDataKey != null && decimal.TryParse(grdRunsNew.SelectedDataKey.Value.ToString(), out ID) == false) { ID = -1; }

                ImageButton btnOpen = (ImageButton)e.CommandSource;
                //string CommandArgument = btnOpen.CommandArgument;
                #region Invalidate
                if (btnOpen.ID.ToUpper() == "IMGINVALIDATE")
                {
                    CycleCountManager im = new CycleCountManager(User.Identity.Name);
                    Message = im.LogPhysicalInventoryBatchInvalid(ID, btnOpen.CommandArgument);
                    UpdateSpreadOtherGrid();
                }
                #endregion
                #region Download
                if (btnOpen.ID.ToUpper() == "IMGDOWNLOAD")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "PrintReportC('PHYSICALCOUNTCC','" + btnOpen.CommandArgument + "'," + "22" + ");", true);
                }
                #endregion
                #region Open
                if (btnOpen.ID.ToUpper() == "IMGOPEN")
                {
                    CycleCountManager im = new CycleCountManager(User.Identity.Name);
                    Message = im.LogPhysicalInventoryBatchOpen(ID, btnOpen.CommandArgument);
                    UpdateSpreadOtherGrid();
                }
                #endregion
                #region Clean
                if (btnOpen.ID.ToUpper() == "IMGCLEAN")
                {
                    CycleCountManager im = new CycleCountManager(User.Identity.Name);
                    Message = im.LogPhysicalInventoryBatchClean(ID, btnOpen.CommandArgument);
                    UpdateSpreadOtherGrid();
                }

                #endregion
                #region Hold
                if (btnOpen.ID.ToUpper() == "IMGHOLD")
                {
                    CycleCountManager im = new CycleCountManager(User.Identity.Name);
                    Message = im.LogPhysicalInventoryBatchHold(ID, btnOpen.CommandArgument);
                    UpdateSpreadOtherGrid();
                }
                if (btnOpen.ID.ToUpper() == "IMGCLOSE")
                {
                    CycleCountManager im = new CycleCountManager(User.Identity.Name);
                    Message = im.LogPhysicalInventoryBatchToClosed(ID, btnOpen.CommandArgument);
                    UpdateSpreadOtherGrid();
                }
                if (btnOpen.ID.ToUpper() == "IMGSYNC")
                {
                    CycleCountManager im = new CycleCountManager(User.Identity.Name);
                    Message = im.LogPhysicalInventoryBatchToSyncReady(ID, btnOpen.CommandArgument);
                    UpdateSpreadOtherGrid();
                }
                if (btnOpen.ID.ToUpper() == "IMGLOCK")
                {
                    CycleCountManager im = new CycleCountManager(User.Identity.Name);
                    Message = im.LogPhysicalInventoryBatchLocked(ID, btnOpen.CommandArgument);
                    UpdateSpreadOtherGrid();
                }
                #endregion
            }
        }

        void gvSpreadOther_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                vwGridCycleInventoryCount_B data = (vwGridCycleInventoryCount_B)e.Row.DataItem;
                ImageButton bInvalidate = (ImageButton)e.Row.FindControl("imgInvalidate");
                ImageButton bDownload = (ImageButton)e.Row.FindControl("imgDownload");
                ImageButton btnOpen = (ImageButton)e.Row.FindControl("imgOpen");
                ImageButton bClean = (ImageButton)e.Row.FindControl("imgClean");
                ImageButton bHold = (ImageButton)e.Row.FindControl("imgHold");

                ImageButton bClose = (ImageButton)e.Row.FindControl("imgClose");
                ImageButton bSync = (ImageButton)e.Row.FindControl("imgSync");
                ImageButton bLock = (ImageButton)e.Row.FindControl("imgLoc");

                if (bInvalidate != null) { bInvalidate.CommandArgument = "-1"; }
                if (bDownload != null) { bDownload.CommandArgument = "-1"; }
                if (btnOpen != null) { btnOpen.CommandArgument = "-1"; }
                if (bClean != null) { bClean.CommandArgument = "-1"; }
                if (bHold != null) { bHold.CommandArgument = "-1"; }
                if (bClose != null) { bClose.CommandArgument = "-1"; }
                if (bSync != null) { bSync.CommandArgument = "-1"; }
                if (bLock != null) { bLock.CommandArgument = "-1"; }
                if (data != null)
                {
                    if (bInvalidate != null) { bInvalidate.CommandArgument = data.Batch; }
                    if (bDownload != null) { bDownload.CommandArgument = data.Batch; }
                    if (btnOpen != null) { btnOpen.CommandArgument = data.Batch; }
                    if (bClean != null) { bClean.CommandArgument = data.Batch; }
                    if (bHold != null) { bHold.CommandArgument = data.Batch; }
                    if (bClose != null) { bClose.CommandArgument = data.Batch; }
                    if (bSync != null) { bSync.CommandArgument = data.Batch; }
                    if (bLock != null) { bLock.CommandArgument = data.Batch; }
                }
            }
        }

        #endregion
        #endregion

        
        
        #region MISC
        void SetMainMessage(string Message)
        {
            lblMainMessage.Text = Message;
        }
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
        #endregion


    }
}