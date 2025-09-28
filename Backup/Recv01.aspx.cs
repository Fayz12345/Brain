using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Drawing;

// using ScanKey;

using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.Services;
using Syncfusion.Web.UI.WebControls.Shared;
using Syncfusion.Web.UI.WebControls.Tools;
//using Factory_DataModel;
using BW_WebApp.DataManagers;


namespace BW_WebApp
{
    public partial class Recv01 : System.Web.UI.Page
    {

        clsLog log;
        //private bool WriteLog = false;
        decimal HideQuestionID = -1;
        decimal HideOptionID = -1;
        decimal ShowQuestionID = -1;
        decimal ShowOptionID = -1;
        clsLinqDataContext ctx = new clsLinqDataContext();

        List<decimal> HideList = new List<decimal>();
        List<decimal> ShowList = new List<decimal>();

        private List<decimal> GetHideList(decimal QuestionID, decimal OptionID)
        {
            if (QuestionID == HideQuestionID)                           // && OptionID == HideOptionID)
            {
                return HideList;
            }
            else
            {
                HideQuestionID = QuestionID;
                HideOptionID = OptionID;
                //AnswerManager am = new AnswerManager(User.Identity.Name);
                //HideList = am.GetDependencyHideLists(QuestionID, OptionID);
                return HideList;
            }
        }
        private List<decimal> GetShowList(decimal QuestionID, decimal OptionID)
        {
            if (QuestionID == ShowQuestionID && OptionID == ShowOptionID)
            {
                return ShowList;
            }
            else
            {
                ShowQuestionID = QuestionID;
                ShowOptionID = OptionID;
                //AnswerManager am = new AnswerManager(User.Identity.Name);
                //ShowList = am.GetDependencyShowLists(QuestionID, OptionID);
                return ShowList;
            }
        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            ctx.Dispose();
            if (log != null)
            {
                //log.LogIt("**** Work Screen Page Unload -- HTML sent to browser");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string zID = Request.QueryString.Get("ID");
            string zPID = Request.QueryString.Get("PID");
            string zPName = Request.QueryString.Get("PName");

            log = new clsLog(Server.MapPath("~"), "WebServer_01_Log.txt", User.Identity.Name, System.Configuration.ConfigurationManager.AppSettings["WriteLogUserNameToLog"]);
            if (System.Configuration.ConfigurationManager.AppSettings["WriteLog"].ToUpper() == "TRUE")
            {
                log.writeLogData = true;
            }
            //log.LogIt("**** Work Screen Page Load Event started");
            hdnIsIMEIBulk.Value = "";
            ProcessManager processm = new ProcessManager(User.Identity.Name);
            decimal SaveProcessID = processm.GetProcessidFromName(ctx, "Save");
            hdnSaveProcessID.Value = SaveProcessID.ToString();
            btnSave.OnClientClick = "";
            btnTSave.OnClientClick = "";
            btnHistoryRefresh.Attributes.Add("OnClick", "StoreHeaderData();");
            btnVersionRefresh.Attributes.Add("OnClick", "StoreHeaderData();");
            btnLocationRefresh.Attributes.Add("OnClick", "StoreHeaderData();");
            btnIDCLocationRefresh.Attributes.Add("OnClick", "StoreHeaderData();");

            btnSKURefresh.Attributes.Add("OnClick", "StoreHeaderData();");
            btnConditionRefresh.Attributes.Add("OnClick", "StoreHeaderData();");

            btnBillingRefresh.Attributes.Add("OnClick", "StoreHeaderData();");
            btnAuthorizeLogRefresh.Attributes.Add("OnClick", "StoreHeaderData();");


            drpProjectList.Attributes.Add("OnClick", "StoreHeaderData();");

            if (Request.QueryString.Get("x") != null && Request.QueryString.Get("x") == "Client")
            {
                btnClearData.Attributes.Add("OnClick", "ClearDataKeepClient(); return false;");
            }
            else
            {
                btnClearData.Attributes.Add("OnClick", "ClearData(); return false;");
            }


            //TabPanelItemVersion.Attributes.Add("OnClick", "xRefresh('btnVersionRefresh'); return false;");


            btnBagTag.Attributes.Add("OnClick", "GenerateBagTag(); return false;");
            btnSave.Attributes.Add("OnClick", "return OKToNextStep('Save'," + SaveProcessID.ToString() + ",'bSaveClick');");
            btnTSave.Attributes.Add("OnClick", "return OKToNextStep('TSave'," + SaveProcessID.ToString() + ",'bSaveClick');");

            //btnClearData_b.Attributes.Add("OnClick", "ClearData(); return false;");
            btnBagTag_b.Attributes.Add("OnClick", "GenerateBagTag(); return false;");
            btnSave_b.Attributes.Add("OnClick", "return OKToNextStep('Save'," + SaveProcessID.ToString() + ",'bSaveClick');");
            //btnTSave_b.Attributes.Add("OnClick", "return OKToNextStep('TSave'," + SaveProcessID.ToString() + ",'bSaveClick');");

            isClientScreen.Value = "";

            btnSaveAuthorize.Click += new EventHandler(btnSaveAuthorize_Click);
            btnJumpProject.Click += new EventHandler(btnJumpProject_Click);
            btnNextProcess.Click += new EventHandler(btnNextProcess_Click);
            btnHistoryRefresh.Click += new EventHandler(btnHistoryRefresh_Click);
            btnVersionRefresh.Click += new EventHandler(btnVersionRefresh_Click);
            btnLocationRefresh.Click += new EventHandler(btnLocationRefresh_Click);
            btnIDCLocationRefresh.Click += new EventHandler(btnIDCLocationRefresh_Click);




            btnSKURefresh.Click += new EventHandler(btnSKURefresh_Click);
            btnConditionRefresh.Click += new EventHandler(btnConditionRefresh_Click);
            btnBillingRefresh.Click += new EventHandler(btnBillingRefresh_Click);

            btnAuthorizeLogRefresh.Click += new EventHandler(btnAuthorizeLogRefresh_Click);

            gd1.RowDataBound += new GridViewRowEventHandler(gd1_RowDataBound);
            GridView4.RowDataBound += new GridViewRowEventHandler(gd1_RowDataBound);

            GridView2.RowDataBound += new GridViewRowEventHandler(GridView2_RowDataBound);

            grdAuthorizationLog.RowDataBound += new GridViewRowEventHandler(grdAuthorizationLog_RowDataBound);
            grdAuthorizationLog.RowCommand += new GridViewCommandEventHandler(grdAuthorizationLog_RowCommand);

            grdHistory.RowDataBound += new GridViewRowEventHandler(grdHistory_RowDataBound);
            grdHistory.RowCommand += new GridViewCommandEventHandler(grdHistory_RowCommand);

            grdVersion.RowDataBound += new GridViewRowEventHandler(grdVersion_RowDataBound);
            grdVersion.RowCommand += new GridViewCommandEventHandler(grdVersion_RowCommand);

            NextStep.ItemDataBound += new RepeaterItemEventHandler(NextStep_ItemDataBound);
            drpProjectList.SelectedIndexChanged += new EventHandler(drpProjectList_SelectedIndexChanged);
            NextStepBulk.ItemDataBound += new RepeaterItemEventHandler(NextStepBulk_ItemDataBound);

            grdBillingPoints.RowDataBound += new GridViewRowEventHandler(grdBillingPoints_RowDataBound);
            ViewProcess.ItemDataBound += new RepeaterItemEventHandler(ViewProcess_ItemDataBound);

            hdnUserName.Value = User.Identity.Name;
            if (IsPostBack == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "onLoad", "SetSessionTimeout();", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "onLoad", "DisplaySessionTimeout();", true);
                wndUnitNote.InitiallyShown = false;
                hdnDoAuthorize.Value = "-1";

                //string zID = Request.QueryString.Get("ID");
                //string zPID = Request.QueryString.Get("PID");
                //string zPName = Request.QueryString.Get("PName");


                string[] s = Roles.GetRolesForUser(User.Identity.Name);
                hdnRoleList.Value = "," + string.Join(",", s) + ",";

                hdnAllowProjectPassThrough.Value = "";
                hdnDealerPortal.Value = "";
                Trace.Write("Not Postback - Begin");
                HdnKeepUnitActive.Value = "";   // Nothing means False.  an Asteric (*) means yes.
                resetList();

                ProjectManager pm = new ProjectManager(User.Identity.Name);
                drpProjectList.DataValueField = "ProjectID";
                drpProjectList.DataTextField = "Name";
                drpProjectList.DataSource = pm.GetMasterActiveProjectList(ctx);
                drpProjectList.DataBind();
                drpProjectList.SelectedIndex = 0;

                if (zPID != null)
                {
                    foreach (ListItem l in drpProjectList.Items)
                    {
                        if (l.Value == zPID) { drpProjectList.SelectedIndex = -1; l.Selected = true; break; }
                    }
                }
                InitializeProject(true);



                if (Request.QueryString.Get("x") != null && Request.QueryString.Get("x") == "Client")
                {
                    isClientScreen.Value = "Y";
                    drpProjectList.Enabled = false;
                }

                txtDateReceived.Text = DateTime.Now.ToString("MM/dd/yyyy") + " " + DateTime.Now.ToString("hh:mm tt");        //DateTime.Now.ToShortTimeString();
                if (zID != null)
                {
                    if (zPName.Length > 0)
                    {
                        LoadProcessLevelData(zPName, true);
                    }

                    ScriptManager.RegisterStartupScript(this, GetType(), "LoadUnit", "LoadSheetDataDetail(" + zID + ");", true);
                }
                else
                {
                    BasicUserUtilities buu = new BasicUserUtilities(User.Identity.Name);
                    decimal UserClientID = buu.GetUserDefaultClientID(ctx, User.Identity.Name);
                    if (UserClientID > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "LoadClient", "LoadClientLocation(" + UserClientID.ToString() + ");", true);
                    }
                }


                ///////////////////////////////////////////////////////////////////////
                //ESNFound.Visible = false;
                this.wndSelectRepairReport.Modal = true;
                this.wndSelectRepairReport.RightToLeft = RightToLeft.No;
                this.wndSelectRepairReport.BackColor = Color.FromName("Blue");
                this.wndSelectRepairReport.Height = 300;
                this.wndSelectRepairReport.Width = 400;
                this.wndSelectRepairReport.ResizeMode = WindowResizeModeType.None;
                ///////////////////////////////////////////////////////////////////////
                //ESNFound.Visible = false;
                this.wndSwitchIMEI.Modal = true;
                this.wndSwitchIMEI.RightToLeft = RightToLeft.No;
                this.wndSwitchIMEI.Height = 300;
                this.wndSwitchIMEI.Width = 400;
                this.wndSwitchIMEI.ResizeMode = WindowResizeModeType.None;
                ///////////////////////////////////////////////////////////////////////
                ///////////////////////////////////////////////////////////////////////
                //ESNFound.Visible = false;
                this.wndESNFoundClient.Modal = true;
                this.wndESNFoundClient.RightToLeft = RightToLeft.No;
                this.wndESNFoundClient.Height = 300;
                this.wndESNFoundClient.Width = 400;
                this.wndESNFoundClient.ResizeMode = WindowResizeModeType.None;
                ///////////////////////////////////////////////////////////////////////

                //ESNFound.Visible = false;
                this.wndSendEmailWindow.Modal = true;
                this.wndSendEmailWindow.RightToLeft = RightToLeft.No;
                this.wndSendEmailWindow.Height = 225;
                this.wndSendEmailWindow.Width = 500;
                this.wndSendEmailWindow.ResizeMode = WindowResizeModeType.None;
                ///////////////////////////////////////////////////////////////////////
                //ESNFound.Visible = false;
                this.wndESNFound.Modal = true;
                this.wndESNFound.RightToLeft = RightToLeft.No;
                this.wndESNFound.Height = 300;
                this.wndESNFound.Width = 500;
                this.wndESNFound.ResizeMode = WindowResizeModeType.None;
                ///////////////////////////////////////////////////////////////////////
                this.wndAuthorize.Modal = true;
                this.wndAuthorize.RightToLeft = RightToLeft.No;
                this.wndAuthorize.BackColor = Color.FromName("Red");
                this.wndAuthorize.Height = 400;
                this.wndAuthorize.Width = 300;
                this.wndAuthorize.ResizeMode = WindowResizeModeType.None;
                ///////////////////////////////////////////////////////////////////////
                this.wndSelectProcess.Modal = true;
                this.wndSelectProcess.RightToLeft = RightToLeft.No;
                this.wndSelectProcess.BackColor = Color.FromName("Red");
                this.wndSelectProcess.Height = 300;
                this.wndSelectProcess.Width = 400;
                this.wndSelectProcess.ResizeMode = WindowResizeModeType.None;
                ///////////////////////////////////////////////////////////////////////
                this.wndIMEIBulk.Modal = true;
                this.wndIMEIBulk.RightToLeft = RightToLeft.No;
                this.wndIMEIBulk.BackColor = Color.FromName("Red");
                this.wndIMEIBulk.Height = 650;
                this.wndIMEIBulk.Width = 500;
                this.wndIMEIBulk.ResizeMode = WindowResizeModeType.FreeStyle;
                ///////////////////////////////////////////////////////////////////////
                this.wndPartList.Modal = true;
                this.wndPartList.RightToLeft = RightToLeft.No;
                this.wndPartList.BackColor = Color.FromName("Red");
                this.wndPartList.Height = 550;
                this.wndPartList.Width = 500;
                this.wndPartList.ResizeMode = WindowResizeModeType.FreeStyle;
                ///////////////////////////////////////////////////////////////////////
                this.wndPurchaseOrderList.Modal = true;
                this.wndPurchaseOrderList.RightToLeft = RightToLeft.No;
                this.wndPurchaseOrderList.BackColor = Color.FromName("Red");
                this.wndPurchaseOrderList.Height = 550;
                this.wndPurchaseOrderList.Width = 500;
                this.wndPurchaseOrderList.ResizeMode = WindowResizeModeType.FreeStyle;
                ///////////////////////////////////////////////////////////////////////
                this.wndPartReturnList.Modal = true;
                this.wndPartReturnList.RightToLeft = RightToLeft.No;
                this.wndPartReturnList.BackColor = Color.FromName("Red");
                this.wndPartReturnList.Height = 550;
                this.wndPartReturnList.Width = 500;
                this.wndPartReturnList.ResizeMode = WindowResizeModeType.FreeStyle;
                ///////////////////////////////////////////////////////////////////////
                this.wndSelectClientLocation.Modal = true;
                this.wndSelectClientLocation.RightToLeft = RightToLeft.No;
                this.wndSelectClientLocation.BackColor = Color.FromName("Red");
                this.wndSelectClientLocation.Height = 550;
                this.wndSelectClientLocation.Width = 500;
                this.wndSelectClientLocation.ResizeMode = WindowResizeModeType.FreeStyle;
                ///////////////////////////////////////////////////////////////////////
                this.wndUnitNote.Modal = true;
                this.wndUnitNote.RightToLeft = RightToLeft.No;
                //this.wndUnitNote.Height = 300;
                //this.wndUnitNote.Width = 500;
                //this.wndUnitNote.ResizeMode = WindowResizeModeType.None;
                ///////////////////////////////////////////////////////////////////////

            }


            // This is used to load a new project/process initiated from the browser.  ------------------------------
            //if (hdnForceLoadID.Value.Length > 0)
            //{
            //    if (hdnForceLoadPID.Value != null) { foreach (ListItem l in drpProjectList.Items) { if (l.Value == hdnForceLoadPID.Value) { drpProjectList.SelectedIndex = -1; l.Selected = true; break; } } }
            //    InitializeProject(true);
            //    if (hdnForceLoadID.Value != null)
            //    {
            //        if (hdnForceLoadProcessName.Value.Length > 0) { LoadProcessLevelData(hdnForceLoadProcessName.Value, true); }
            //        ScriptManager.RegisterStartupScript(this, GetType(), "LoadUnit", "LoadSheetDataDetail(" + hdnForceLoadID.Value + ");", true);
            //    }
            //    hdnForceLoadID.Value = "";
            //    hdnForceLoadPID.Value = "";
            //    hdnForceLoadProcessName.Value = "";
            //}
            //////////////////////////// ------------------------------------------------------------------------------

            SetTabs();

            ScanKey.Focus();
            ScanKey.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13) || (event.which == 9) || (event.keyCode == 9)) {RecordScanKey();return false;}} else {return true}; ");
            ScanKey.Attributes.Add("onblur", "RecordScanKey();return false;");

            txtRMA.Attributes.Add("onblur", "ScanFocus();");
            txtProjectTag.Attributes.Add("onblur", "ScanFocus();");
            txtESN.Attributes.Add("onblur", "ScanFocus();");
            //log.LogIt("**** Work Screen Page Load Event Finished");
            //writeTrace(false);

        }






        void SetTabs()
        {
            string[] RolesList = Roles.GetRolesForUser(User.Identity.Name);
            foreach (string r in RolesList)
            {
                if (r.ToUpper() == "ADMINISTRATORS" ||
                    r.ToUpper() == "SUPERVISORS" ||
                    r.ToUpper() == "RECEIVER" ||
                    r.ToUpper() == "PROCESSOR")
                {
                    return;
                }
            }
            chkSticky.Checked = false;
            chkSticky.Visible = false;
            tabBulkAdvance.Visible = false;
            TabPanelClientDetailx.Visible = false;
            TabPanelClientDetail.Visible = false;
            TabPanelItemVersion.Visible = false;
            TabPanelLocationHistory.Visible = false;
            tabBillingPoints.Visible = false;
            TabAuthorization.Visible = false;
            btnBagTag.Visible = false;
            btnTSave.Visible = false;
            btnCheckBin.Visible = false;
            btnUnitView.Visible = false;
            btnBagTag_b.Visible = false;
            btnSwitch.Visible = false;
            btnShowTimeLog.Visible = false;
            btnClearLog.Visible = false;
            txtHistoryCount.Visible = false;
            btnDelete.Visible = false;
            //btnDelete.Visible = false;
            btnRemoveAll.Visible = false;
            lstHistory.Visible = false;
            btnClearLog.Visible = false;
        }

        /////////////////////////////////////////////
        void InitializeProject(bool RunSetup)
        {

            decimal projectID = 0;
            ////writeTrace(true);
            chkSticky.Checked = false;
            chkSticky.Visible = false;

            hdnStickyData.Value = "";
            chkAutoSaveOnScan.Checked = false;
            chkAutoPrintBagTag.Checked = false;
            hdnAllowProjectPassThrough.Value = "";
            hdnDealerPortal.Value = "";
            hdnisSecondaryProjectOverride.Value = "N";

            lblScanFieldText.Text = "Scan Field:";

            ProjectManager pm = new ProjectManager(User.Identity.Name);
            if (drpProjectList.SelectedItem == null)
            {
                HdnProjSetup.Value = "";
            }
            else
            {

                string project = drpProjectList.SelectedItem.Text;

                decimal.TryParse(drpProjectList.SelectedItem.Value, out projectID);
                HdnProjSetup.Value = pm.GetSetUpFieldDef(ctx, projectID);
                Project pj = pm.Get(projectID);
                if (pj != null)
                {
                    hdnisMasterLinked.Value = pj.isMasterCarrierManufactuerLinked.ToString();
                    if (pj.isSecondaryProjectOverride == true) { hdnisSecondaryProjectOverride.Value = "Y"; }
                    if (pj.BagTagName.ToUpper() == "CLIENT")
                    {
                        hdnAllowProjectPassThrough.Value = "1";
                        ClientManager cm = new ClientManager(User.Identity.Name);
                        hdnDealerPortal.Value = cm.GetUserDealerPortalName().ToUpper();
                    }
                }
                //log.LogIt("InitializeProject");
                LoadProcessLevelData("", RunSetup);

                if (project.ToUpper() == "CLIENT PORTAL")
                {
                    lblScanFieldText.Text = "ESN/IMEI:";
                }

            }

            HdnNextProcess.Value = "";
            txtESN.Text = "";
            hdnLastESN.Value = "";
            hdnLastESNVersion.Value = "";

            //ProcessManager prm = new ProcessManager(User.Identity.Name);
            drpProcessList.DataValueField = "ProcessID";
            drpProcessList.DataTextField = "Name";

            //ButtonText

            drpProcessList.DataSource = from x in pm.GetMasterProcessList(ctx, projectID)
                                        let xText = (x.ButtonText != null && x.ButtonText.Length > 0) ? x.ButtonText : x.Name
                                        where x.Name != "Save"
                                        orderby x.Sequence, x.Name
                                        select new { ProcessID = x.ProcessID, Name = xText };
            drpProcessList.DataBind();
            //log.LogIt("Bound drpProcessList");

            drpProcessList.SelectedIndex = -1;
            if (drpProcessList.Items.Count > 0) { drpProcessList.SelectedIndex = 0; }
        }

        void RestrictFunctions()
        {
            tb1x.Visible = false;
            tabBulkAdvance.Visible = false;
            TabPanelClientDetail.Visible = false;
            TabPanelClientDetailx.Visible = false;
            TabPanelItemVersion.Visible = false;
            tabBillingPoints.Visible = false;
            TabPanelLocationHistory.Visible = false;

            TabAuthorization.Visible = false;
            ReceiveDetailManager rdm = new ReceiveDetailManager(User.Identity.Name);
            //using (clsLinqDataContext ctx = rdm.GetDataContext(User.Identity.Name))
            //{
            if (rdm.UserRestrict.AllowSelectFunctionExecute("Work.Tab.Data", ctx) == true) { tb1x.Visible = true; }
            //if (rdm.UserRestrict.AllowSelectFunctionExecute("Work.Tab.Bulk Advance", ctx) == true) { tabBulkAdvance.Visible = true; }
            if (rdm.UserRestrict.AllowSelectFunctionExecute("Work.Tab.History", ctx) == true) { TabPanelClientDetail.Visible = true; }
            if (rdm.UserRestrict.AllowSelectFunctionExecute("Work.Tab.LocationHistory", ctx) == true) { TabPanelLocationHistory.Visible = true; }
            //if (rdm.UserRestrict.AllowSelectFunctionExecute("Work.Tab.Search", ctx) == true) { TabPanelClientDetailx.Visible = true; }
            if (rdm.UserRestrict.AllowSelectFunctionExecute("Work.Tab.Version", ctx) == true) { TabPanelItemVersion.Visible = true; }
            if (rdm.UserRestrict.AllowSelectFunctionExecute("Work.Tab.Billing Points", ctx) == true) { tabBillingPoints.Visible = true; }
            if (rdm.UserRestrict.AllowSelectFunctionExecute("Work.Tab.Authorization", ctx) == true) { TabAuthorization.Visible = true; }
            //}
        }
        private void resetList()
        {
            hdnQuestionIDList.Value = "";
            hdnQuestionClientIDList.Value = "";
        }

        void btnBillingRefresh_Click(object sender, EventArgs e)
        {
            //writeTrace(true);
            RefreshBillingPoint();
            ScriptManager.RegisterStartupScript(this, GetType(), "RestHeader", "RestoreHeaderData(); SetUpScreen('');", true);
            //writeTrace(false);
        }
        private void RefreshBillingPoint()
        {
            //writeTrace(true);
            decimal RDID = 0;
            string sRDID = hdnReceiveDetailID.Value;
            if (decimal.TryParse(sRDID, out RDID) != true) { RDID = -1; }
            ReceiveDetailManager rdm = new ReceiveDetailManager(User.Identity.Name);
            List<ReceiveDetailBillingPoint> blist = null;
            blist = rdm.GetReceiveDetailBillingPoints(ctx, RDID);

            string[] DataKeys = new string[] { "ReceiveDetailID", "ReceiveDetailBillingPointsID" };
            grdBillingPoints.DataKeyNames = DataKeys;
            grdBillingPoints.DataSource = blist;
            grdBillingPoints.DataBind();
            //writeTrace(false);
        }
        void grdBillingPoints_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //string javascriptcode = "";
                Label lbl = (Label)e.Row.FindControl("lblProcessName");
                if (lbl != null)
                {
                    decimal ProcessID = ((ReceiveDetailBillingPoint)e.Row.DataItem).ProcessID;
                    ProcessManager pm = new ProcessManager(User.Identity.Name);
                    Process p = pm.GetProcesss(ctx, ProcessID);
                    lbl.Text = p.Description;
                }

            }
        }

        void btnSaveAuthorize_Click(object sender, EventArgs e)
        {
            decimal RDID = 0;
            decimal EF = 0;
            decimal FF = 0;
            decimal HST = 0;
            decimal Total = 0;
            string sRDID = hdnReceiveDetailID.Value;
            if (decimal.TryParse(sRDID, out RDID) != true) { RDID = -1; }

            if (decimal.TryParse(txtEstimateFee.Text, out EF) != true) { EF = 0; }
            if (decimal.TryParse(txtFreightFee.Text, out FF) != true) { FF = 0; }
            if (decimal.TryParse(txtHST.Text, out HST) != true) { HST = 0; }
            if (decimal.TryParse(TxtTotal.Text, out Total) != true) { Total = 0; }

            if (RDID > 0)
            {
                ReceiveDetailAuthohrizationManager rm = new ReceiveDetailAuthohrizationManager(User.Identity.Name);
                rm.AddNewRequest(ctx, RDID, EF, FF, HST, Total, txtAuthorizeNote.Text, "AUX");
                RefreshAuthorizeLog();
            }
        }
        void btnAuthorizeLogRefresh_Click(object sender, EventArgs e)
        {
            //writeTrace(true);
            RefreshAuthorizeLog();
            ScriptManager.RegisterStartupScript(this, GetType(), "RestHeader", "RestoreHeaderData(); SetUpScreen('');", true);
            //writeTrace(false);
        }
        private void RefreshAuthorizeLog()
        {
            //writeTrace(true);
            decimal RDID = 0;
            string sRDID = hdnReceiveDetailID.Value;
            if (decimal.TryParse(sRDID, out RDID) != true) { RDID = -1; }
            ReceiveDetailManager rdm = new ReceiveDetailManager(User.Identity.Name);
            List<ReceiveDetailAuthorizationLog> blist = null;
            blist = rdm.GetReceiveDetailAuthorizationLog(ctx, RDID).OrderBy(x => x.ReceivedDate)
                                                              .ThenBy(x => x.AuthorizedDate)
                                                              .ThenBy(x => x.DeclinedDate)
                                                              .ThenBy(x => x.RejectedDate)
                                                              .ThenBy(x => x.CreateDate)
                                                              .ToList();

            string[] DataKeys = new string[] { "ReceiveDetailID", "ReceiveDetailAuthorizationLogID" };
            grdAuthorizationLog.DataKeyNames = DataKeys;
            grdAuthorizationLog.DataSource = blist;
            grdAuthorizationLog.DataBind();
            //writeTrace(false);
        }
        void grdAuthorizationLog_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            decimal id = -1;
            ReceiveDetailAuthohrizationManager am = null;
            LinkButton btnAdd = (LinkButton)e.CommandSource;
            switch (btnAdd.CommandName.ToString().ToUpper())
            {
                case "REVIVE":
                    id = -1;
                    if (decimal.TryParse(btnAdd.CommandArgument, out id) == false) { id = -1; }
                    if (id > 0)
                    {
                        am = new ReceiveDetailAuthohrizationManager(User.Identity.Name);
                        am.Revive(ctx, id);
                        RefreshAuthorizeLog();
                    }
                    ScriptManager.RegisterStartupScript(this, GetType(), "RestHeader", "RestoreHeaderData(); SetUpScreen('');", true);
                    break;
                case "REJECT":
                    id = -1;
                    if (decimal.TryParse(btnAdd.CommandArgument, out id) == false) { id = -1; }
                    if (id > 0)
                    {
                        am = new ReceiveDetailAuthohrizationManager(User.Identity.Name);
                        am.Reject(ctx, id);
                        RefreshAuthorizeLog();
                    }
                    ScriptManager.RegisterStartupScript(this, GetType(), "RestHeader", "RestoreHeaderData(); SetUpScreen('');", true);
                    break;
                case "DELETERECEIVEDETAILPROCESSLOG":
                //ReceiveDetailManager rdm = new ReceiveDetailManager(User.Identity.Name);
                //id = -1;
                //if (decimal.TryParse(btnAdd.CommandArgument, out id) == false) { id = -1; }
                //rdm.DeleteReceiveDetailProcessLogThisID(id);
                //ScriptManager.RegisterStartupScript(this, GetType(), "RestHeader", "RestoreHeaderData(); SetUpScreen('');", true);
                //break;
                default:
                    break;
            }
        }
        void grdAuthorizationLog_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ReceiveDetailAuthorizationLog rda = ((ReceiveDetailAuthorizationLog)e.Row.DataItem);
                LinkButton lb = (LinkButton)e.Row.FindControl("lnkbtnRevive");
                if (lb != null)
                {
                    lb.Visible = false;
                    if (rda.AuthorizedDate == null && rda.RejectedDate != null)
                    {
                        lb.Visible = true;
                        lb.CommandName = "Revive";
                        lb.CommandArgument = rda.ReceiveDetailAuthorizationLogID.ToString();
                        lb.Attributes.Add("OnClick", "StoreHeaderData();");

                    }
                }
                LinkButton lb1 = (LinkButton)e.Row.FindControl("lnkbtnExpire");
                if (lb1 != null)
                {
                    lb1.Visible = false;
                    //lb1.Visible = true;
                    if (rda.AuthorizedDate == null && rda.RejectedDate == null)
                    {
                        lb1.Visible = true;
                        lb1.CommandName = "Reject";
                        lb1.CommandArgument = rda.ReceiveDetailAuthorizationLogID.ToString();
                        lb1.Attributes.Add("OnClick", "StoreHeaderData();");
                    }
                }
                Label lab = (Label)e.Row.FindControl("lblDelcined");
                if (lab != null)
                {
                    lab.Text = "";
                    if (rda.DeclinedDate != null)
                    {
                        lab.Text = rda.DeclinedBy.ToString() + " " + rda.DeclinedDate.ToString();
                    }
                }
                Label lab2 = (Label)e.Row.FindControl("lblRejected");
                if (lab2 != null)
                {
                    lab2.Text = "";
                    if (rda.RejectedDate != null)
                    {
                        lab2.Text = rda.RejectedBy.ToString() + " " + rda.RejectedDate.ToString();
                    }
                }
                Label lab3 = (Label)e.Row.FindControl("lblRequested");
                if (lab3 != null)
                {
                    lab3.Text = "";
                    if (rda.RequestedDate != null)
                    {
                        lab3.Text = rda.RequestedBy.ToString() + " " + rda.RequestedDate.ToString();
                    }
                }

                Label lab4 = (Label)e.Row.FindControl("lblAuthorized");
                if (lab4 != null)
                {
                    lab4.Text = "";
                    if (rda.AuthorizedDate != null)
                    {
                        lab4.Text = rda.AuthorizedBy.ToString() + " " + rda.AuthorizedDate.ToString();
                    }
                }

                Label lab5 = (Label)e.Row.FindControl("lblReceived");
                if (lab5 != null)
                {
                    lab5.Text = "";
                    if (rda.ReceivedDate != null)
                    {
                        lab5.Text = rda.ReceivedBy.ToString() + " " + rda.ReceivedDate.ToString();
                    }
                }

                Label Status = (Label)e.Row.FindControl("lblStatus");
                if (Status != null)
                {
                    ReceiveDetailAuthohrizationManager rdam = new ReceiveDetailAuthohrizationManager(User.Identity.Name);
                    Status.Text = rdam.Status(ctx, rda.StatusID);
                }
            }
        }

        void btnHistoryRefresh_Click(object sender, EventArgs e)
        {
            RefreshHistory();
            ScriptManager.RegisterStartupScript(this, GetType(), "RestHeader", "RestoreHeaderData(); SetUpScreen('');", true);
        }
        private void RefreshHistory()
        {
            decimal RDID = 0;
            string sRDID = hdnReceiveDetailID.Value;
            if (decimal.TryParse(sRDID, out RDID) != true) { RDID = -1; }
            if (RDID < 1) { return; }
            ReceiveDetailManager rdm = new ReceiveDetailManager(User.Identity.Name);
            List<ReceiveDetailProcessLog> blist = null;
            blist = rdm.GetLogData(ctx, RDID);
            string[] DataKeys = new string[] { "ReceiveDetailID", "ReceiveDetailProcessLogID" };
            grdHistory.DataKeyNames = DataKeys;
            grdHistory.DataSource = blist;
            grdHistory.DataBind();
        }
        void grdHistory_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            System.Web.UI.WebControls.ImageButton btnAdd = (System.Web.UI.WebControls.ImageButton)e.CommandSource;
            switch (btnAdd.CommandName.ToString().ToUpper())
            {
                case "DELETERECEIVEDETAILPROCESSLOG":
                    ReceiveDetailManager rdm = new ReceiveDetailManager(User.Identity.Name);
                    decimal id = -1;
                    if (decimal.TryParse(btnAdd.CommandArgument, out id) == false) { id = -1; }
                    rdm.DeleteReceiveDetailProcessLogThisID(ctx, id);
                    RefreshHistory();
                    ScriptManager.RegisterStartupScript(this, GetType(), "RestHeader", "RestoreHeaderData(); SetUpScreen('');", true);
                    break;
                default:
                    break;
            }
        }
        void grdHistory_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                System.Web.UI.WebControls.ImageButton bPrint = (System.Web.UI.WebControls.ImageButton)e.Row.FindControl("imgDelete");
                //bPrint = (ImageButton)e.Row.FindControl("imgDelete");
                if (User.IsInRole("Administrators") == false && User.IsInRole("Supervisors") == false && User.IsInRole("Admin") == false)
                {
                    ((System.Web.UI.WebControls.ImageButton)e.Row.FindControl("imgDelete")).Visible = false;
                    ((System.Web.UI.WebControls.ImageButton)e.Row.FindControl("imgMoveProcessUp")).Visible = false;
                    ((System.Web.UI.WebControls.ImageButton)e.Row.FindControl("imgMoveProcessDown")).Visible = false;
                    ((System.Web.UI.WebControls.ImageButton)e.Row.FindControl("imgChangeProcess")).Visible = false;
                    return;
                }


                if (bPrint != null)
                {
                    bPrint.CommandName = "DeleteReceiveDetailProcessLog";
                    bPrint.CommandArgument = ((ReceiveDetailProcessLog)e.Row.DataItem).ReceiveDetailProcessLogID.ToString();
                    bPrint.Attributes.Add("OnClick", "StoreHeaderData();");
                }
                bPrint = (System.Web.UI.WebControls.ImageButton)e.Row.FindControl("imgMoveProcessUp");
                if (bPrint != null)
                {
                    bPrint.CommandName = "MoveProcessUp";
                    bPrint.CommandArgument = ((ReceiveDetailProcessLog)e.Row.DataItem).ReceiveDetailProcessLogID.ToString();
                    bPrint.Attributes.Add("OnClick", "alert('Not Yet Implemented'); return false;");
                }
                bPrint = (System.Web.UI.WebControls.ImageButton)e.Row.FindControl("imgMoveProcessDown");
                if (bPrint != null)
                {
                    bPrint.CommandName = "MoveProcessDown";
                    bPrint.CommandArgument = ((ReceiveDetailProcessLog)e.Row.DataItem).ReceiveDetailProcessLogID.ToString();
                    bPrint.Attributes.Add("OnClick", "alert('Not Yet Implemented'); return false;");
                }

                bPrint = (System.Web.UI.WebControls.ImageButton)e.Row.FindControl("imgChangeProcess");
                if (bPrint != null)
                {
                    bPrint.CommandName = "ChangeProcess";
                    bPrint.CommandArgument = ((ReceiveDetailProcessLog)e.Row.DataItem).ReceiveDetailProcessLogID.ToString();
                    bPrint.Attributes.Add("OnClick", "OpenSelectProcessWindowCtrl(" + bPrint.CommandArgument + "); return false;");
                }
            }
        }

        void btnVersionRefresh_Click(object sender, EventArgs e)
        {
            RefreshVersion();
            ScriptManager.RegisterStartupScript(this, GetType(), "RestHeader", "RestoreHeaderData(); SetUpScreen('');", true);
        }
        private void RefreshVersion()
        {
            decimal RDID = 0;
            string sRDID = hdnReceiveDetailID.Value;
            if (decimal.TryParse(sRDID, out RDID) != true) { RDID = -1; }
            if (RDID < 1) { return; }
            ReceiveDetailManager rdm = new ReceiveDetailManager(User.Identity.Name);
            //List<ReceiveDetail> blist = null;
            var blist = from x in rdm.GetReceiveDetailVersionHistory(ctx, RDID)
                        let PName = rdm.ProjectName(x.ProjectID)
                        let SName = rdm.StatusName(x.StatusID)
                        orderby x.Version
                        select new pxlist
                        {
                            ReceiveDetailID = x.ReceiveDetailID,
                            ProjectID = decimal.Parse(x.ProjectID.ToString()),
                            StatusID = x.StatusID,
                            Version = x.Version,
                            ESN = x.ESN,
                            CreateDate = x.CreateDate,
                            ProjectName = PName.ToString(),
                            CreateUser = x.CreateUser,
                            StatusName = SName.ToString()
                        };

            //string[] DataKeys = new string[] { "ReceiveDetailID"};
            //grdVersion.DataKeyNames = DataKeys;
            grdVersion.DataSource = blist;
            grdVersion.DataBind();
        }
        void btnLocationRefresh_Click(object sender, EventArgs e)
        {
            RefreshLocation();
            ScriptManager.RegisterStartupScript(this, GetType(), "RestHeader", "RestoreHeaderData(); SetUpScreen('');", true);
        }
        void btnIDCLocationRefresh_Click(object sender, EventArgs e)
        {
            RefreshIDCLocation();
            ScriptManager.RegisterStartupScript(this, GetType(), "RestHeader", "RestoreHeaderData(); SetUpScreen('');", true);
        }
        void btnConditionRefresh_Click(object sender, EventArgs e)
        {
            decimal RDID = 0;
            string sRDID = hdnReceiveDetailID.Value;
            if (decimal.TryParse(sRDID, out RDID) != true) { RDID = -1; }
            if (RDID < 1) { return; }
            ReceiveDetailManager rdm = new ReceiveDetailManager(User.Identity.Name);
            //var blist = from x in rdm.GetReceiveDetailLocationHistory_IDC(ctx, RDID) select x;
            var blist = from x in rdm.GetReceiveDetailConditionHistory_IFS(ctx, RDID) select x;
            grdConditionHistory.DataSource = blist.OrderByDescending(x => x.CreateDate);
            grdConditionHistory.DataBind();
        }




        void btnSKURefresh_Click(object sender, EventArgs e)
        {
            decimal RDID = 0;
            string sRDID = hdnReceiveDetailID.Value;
            if (decimal.TryParse(sRDID, out RDID) != true) { RDID = -1; }
            if (RDID < 1) { return; }
            ReceiveDetailManager rdm = new ReceiveDetailManager(User.Identity.Name);
            //var blist = from x in rdm.GetReceiveDetailLocationHistory_IDC(ctx, RDID) select x;
            var blist = from x in rdm.GetReceiveDetailSKUHistory_IFS(ctx, RDID) select x;

            grdSKUHistory.DataSource = blist.OrderByDescending(x => x.CreateDate);
            grdSKUHistory.DataBind();
        }

        private void RefreshIDCLocation()
        {
            decimal RDID = 0;
            string sRDID = hdnReceiveDetailID.Value;
            if (decimal.TryParse(sRDID, out RDID) != true) { RDID = -1; }
            if (RDID < 1) { return; }
            ReceiveDetailManager rdm = new ReceiveDetailManager(User.Identity.Name);
            var blist = from x in rdm.GetReceiveDetailLocationHistory_IDC(ctx, RDID) select x;
            //var blist = from x in rdm.GetReceiveDetailLocationHistory_IFS(ctx, RDID) select x;

            grdIDCLocationHistory.DataSource = blist.OrderByDescending(x => x.CreateDate);
            grdIDCLocationHistory.DataBind();


            //decimal RDID = 0;
            //string sRDID = hdnReceiveDetailID.Value;
            //if (decimal.TryParse(sRDID, out RDID) != true) { RDID = -1; }
            //if (RDID < 1) { return; }
            //ReceiveDetailManager rdm = new ReceiveDetailManager(User.Identity.Name);
            //var blist = from x in rdm.GetReceiveDetailLocationHistory_IFS(ctx, RDID) select x;
            //grdLocationHistory.DataSource = blist.OrderBy(x => x.CreateDate);
            //grdLocationHistory.DataBind();
        }
        private void RefreshLocation()
        {
            decimal RDID = 0;
            string sRDID = hdnReceiveDetailID.Value;
            if (decimal.TryParse(sRDID, out RDID) != true) { RDID = -1; }
            if (RDID < 1) { return; }
            ReceiveDetailManager rdm = new ReceiveDetailManager(User.Identity.Name);
            //var blist = from x in rdm.GetReceiveDetailLocationHistory_IDC(ctx, RDID) select x;
            var blist = from x in rdm.GetReceiveDetailLocationHistory_IFS(ctx, RDID) select x;

            grdLocationHistory.DataSource = blist.OrderByDescending(x => x.CreateDate);
            grdLocationHistory.DataBind();


            //decimal RDID = 0;
            //string sRDID = hdnReceiveDetailID.Value;
            //if (decimal.TryParse(sRDID, out RDID) != true) { RDID = -1; }
            //if (RDID < 1) { return; }
            //ReceiveDetailManager rdm = new ReceiveDetailManager(User.Identity.Name);
            //var blist = from x in rdm.GetReceiveDetailLocationHistory_IFS(ctx, RDID) select x;
            //grdLocationHistory.DataSource = blist.OrderBy(x => x.CreateDate);
            //grdLocationHistory.DataBind();
        }

        void grdVersion_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            System.Web.UI.WebControls.ImageButton btnAdd = (System.Web.UI.WebControls.ImageButton)e.CommandSource;
            //ReceiveDetailManager rdm = null;
            decimal id = -1;
            if (decimal.TryParse(btnAdd.CommandArgument, out id) == false) { id = -1; }
            switch (btnAdd.CommandName.ToString().ToUpper())
            {
                case "SETVERSIONTOZERO":
                    //rdm = new ReceiveDetailManager(User.Identity.Name);
                    //rdm.AdvanceESNVersion_ToZero(ctx, id);
                    //RefreshVersion();
                    //ScriptManager.RegisterStartupScript(this, GetType(), "RestHeader", "RestoreHeaderData(); SetUpScreen('');", true);
                    break;
                case "ADVANCEVERSIONNUMBERS":
                    //rdm = new ReceiveDetailManager(User.Identity.Name);
                    //rdm.AdvanceESNVersion_FromThisOne(ctx, id);
                    //RefreshVersion();
                    //ScriptManager.RegisterStartupScript(this, GetType(), "RestHeader", "RestoreHeaderData(); SetUpScreen('');", true);
                    break;
                //case "DELETERECEIVEDETAILPROCESSLOG":
                //    rdm = new ReceiveDetailManager(User.Identity.Name);
                //    //rdm.DeleteReceiveDetailProcessLogThisID(id);
                //    RefreshVersion();
                //    ScriptManager.RegisterStartupScript(this, GetType(), "RestHeader", "RestoreHeaderData(); SetUpScreen('');", true);
                //    break;
                default:
                    break;
            }
        }
        void grdVersion_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //string javascriptcode = "";
                System.Web.UI.WebControls.ImageButton bPrint = (System.Web.UI.WebControls.ImageButton)e.Row.FindControl("imgOpen");
                if (bPrint != null)
                {
                    bPrint.CommandName = "Open";
                    bPrint.CommandArgument = ((pxlist)e.Row.DataItem).ReceiveDetailID.ToString();
                    //javascriptcode = "LoadSheetDataDetail(" + bPrint.CommandArgument + "); return false;";
                    //bPrint.Attributes.Add("OnClick", javascriptcode);
                }
                if (User.IsInRole("Administrators") == false && User.IsInRole("Supervisors") == false && User.IsInRole("AddRollBack") == false && User.IsInRole("Admin") == false)
                {
                    ////((System.Web.UI.WebControls.ImageButton)e.Row.FindControl("imgGraveYardBack")).Visible = false;
                    ////((System.Web.UI.WebControls.ImageButton)e.Row.FindControl("imgGraveYard")).Visible = false;
                    //((System.Web.UI.WebControls.ImageButton)e.Row.FindControl("imgVersion")).Visible = false;
                    //((System.Web.UI.WebControls.ImageButton)e.Row.FindControl("imgVerUp")).Visible = false;
                    return;
                }

                //bPrint = (System.Web.UI.WebControls.ImageButton)e.Row.FindControl("imgGraveYardBack");
                //if (bPrint != null)
                //{
                //    bPrint.CommandName = "GraveYardBack";
                //    bPrint.CommandArgument = ((pxlist)e.Row.DataItem).ReceiveDetailID.ToString();
                //    //bPrint.Attributes.Add("OnClick", "alert('Not Yet Implemented'); return false;");
                //}
                //bPrint = (System.Web.UI.WebControls.ImageButton)e.Row.FindControl("imgGraveYard");
                //if (bPrint != null)
                //{
                //    bPrint.CommandName = "GraveYard";
                //    bPrint.CommandArgument = ((pxlist)e.Row.DataItem).ReceiveDetailID.ToString();
                //    //bPrint.Attributes.Add("OnClick", "alert('Not Yet Implemented'); return false;");
                //}

                //bPrint = (System.Web.UI.WebControls.ImageButton)e.Row.FindControl("imgVersion");
                //if (bPrint != null)
                //{
                //    bPrint.CommandName = "SetVersionToZero";
                //    bPrint.CommandArgument = ((pxlist)e.Row.DataItem).ReceiveDetailID.ToString();
                //    bPrint.Attributes.Add("OnClick", "StoreHeaderData();");
                //}
                //bPrint = (System.Web.UI.WebControls.ImageButton)e.Row.FindControl("imgVerUp");
                //if (bPrint != null)
                //{
                //    bPrint.CommandName = "AdvanceVersionNumbers";
                //    bPrint.CommandArgument = ((pxlist)e.Row.DataItem).ReceiveDetailID.ToString();
                //    bPrint.Attributes.Add("OnClick", "StoreHeaderData();");
                //}
            }
        }


        ///// ///////////////////////////////////////////
        //public class pxlist
        //{
        //    public decimal ReceiveDetailID { get; set; }
        //    public decimal ProjectID { get; set; }
        //    public decimal StatusID { get; set; }
        //    public decimal ProcessID { get; set; }
        //    public DateTime CreateDate { get; set; }
        //    public string Version { get; set; }
        //    public string ESN { get; set; }
        //    public string ProjectName { get; set; }
        //    public string StatusName { get; set; }
        //    public string CreateUser { get; set; }
        //}
        ///// /////////////////////////////////////////////
        /// 

        void btnJumpProject_Click(object sender, EventArgs e)
        {
            //This is used to load a new project/process initiated from the browser.  ------------------------------
            if (hdnForceLoadID.Value.Length > 0)
            {
                if (hdnForceLoadPID.Value != null) { foreach (ListItem l in drpProjectList.Items) { if (l.Value == hdnForceLoadPID.Value) { drpProjectList.SelectedIndex = -1; l.Selected = true; break; } } }
                InitializeProject(true);
                if (hdnForceLoadID.Value != null)
                {
                    if (hdnForceLoadProcessName.Value.Length > 0) { LoadProcessLevelData(hdnForceLoadProcessName.Value, true); }
                    ScriptManager.RegisterStartupScript(this, GetType(), "LoadUnit", "LoadSheetDataDetail(" + hdnForceLoadID.Value + ");", true);
                }
                hdnForceLoadID.Value = "";
                hdnForceLoadPID.Value = "";
                hdnForceLoadProcessName.Value = "";
            }
        }



        void drpProjectList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //log.LogIt("(Project Changed) drpProjectList_SelectedIndexChanged - " + drpProjectList.SelectedItem.Text);
            InitializeProject(true);
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "RestHeader", "RestoreHeaderData(false, false, false, false); SetUpScreen('');", true);
            }
            ////////////////////////// ------------------------------------------------------------------------------
        }

        private string RemoveBadCharacters(string Value)
        {
            return Value.Replace(" ", "").Replace("/", "").Replace("*", "").Replace(".", "").Replace("#", "");
        }

        void btnNextProcess_Click(object sender, EventArgs e)
        {
            ////writeTrace(true);
            chkSticky.Checked = false;
            chkSticky.Visible = false;
            hdnStickyData.Value = "";

            //log.LogIt("(Process Button) btnNextProcess_Click - " + HdnNextProcess.Value);
            LoadProcessLevelData(HdnNextProcess.Value, true);

            t1x.Tabs[0].HeaderText = HdnNextProcess.Value;
            btnSave.Visible = true;



            if (HdnNextProcess.Value.ToUpper() == "SEARCH")
            {
                btnSave.Visible = false;
            }

            if (HdnKeepUnitActive.Value.Length == 0)
            {
                hdnLastESN.Value = "";
                hdnLastESNVersion.Value = "";
                ScriptManager.RegisterStartupScript(this, GetType(), "RestHeader", "RestoreHeaderData(false, false, false, false);", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "RestHeader", "RecordScanKey('" + hdnLastESN.Value + "'); ", true);
            }
            ////writeTrace(false);
        }
        private void LoadProcessLevelData(string ProcessLevel, bool RunSetup)
        {

            // Reset the dropdown link references to blank.
            hdnCarrierID.Value = "";
            hdnManufacturerID.Value = "";
            hdnModelID.Value = "";
            hdnColourID.Value = "";
            hdnBinID.Value = "";
            hdnLabDestinationID.Value = "";

            ////writeTrace(true);
            //log.LogIt("(Change Processes) - Start LoadProcessLevelData:" + drpProjectList.SelectedItem.Text + ":" + ProcessLevel);
            resetList();
            string project = drpProjectList.SelectedItem.Text;
            decimal projectID = 0;
            if (decimal.TryParse(drpProjectList.SelectedItem.Value, out projectID) == false) { projectID = -1; }

            BasicUserUtilities buu = new BasicUserUtilities(User.Identity.Name);
            ProcessLevel = buu.GetUserDefaultProcess(ctx, ProcessLevel, project);
            //log.LogIt("GetUserDefaultProcess - Done");



            if (ProcessLevel.Length == 0) { return; }
            txtDateReceived.Enabled = false;
            txtRMA.Enabled = false;
            txtQTY.Enabled = false;
            txtProjectTag.Enabled = false;

            imgShowPartList.Visible = false;
            imgShowPartReturnList.Visible = false;
            imgShowIFSPONumbers.Visible = false;
            //if (HdnNextProcess.Value.ToUpper() == "GMP REPAIR")
            //{
            //    imgShowPartList.Visible = true;
            //}

            if ((ProcessLevel.Length > 6 && ProcessLevel.Substring(0, 7).ToUpper() == "RECEIVE")
             || ProcessLevel.ToUpper() == "BULKRECEIVE"
             || ProcessLevel.ToUpper() == "BULKMOVE"
             || ProcessLevel.ToUpper() == "RECEIVEFROMBULK"

             || ProcessLevel.ToUpper() == "RECEIVEDOAB"
             || ProcessLevel.ToUpper() == "RECEIVEWARRANTYB"

             || ProcessLevel.ToUpper() == "RECEIVEDOA"
             || ProcessLevel.ToUpper() == "RECEIVEDEFECTIVE"
             || ProcessLevel.ToUpper() == "RECEIVEREPAIRED"
             || ProcessLevel.ToUpper() == "RECEIVEGENERAL"
             || ProcessLevel.ToUpper() == "RECEIVEINWARRANTY"
             || ProcessLevel.ToUpper() == "RECEIVEEXWARRANTY"
             || ProcessLevel.ToUpper() == "RECEIVEOOWARRANTY")
            {
                txtQTY.Enabled = true;
                if (HdnProjSetup.Value.ToUpper().Contains("ZRMAZZEDITZ") == true)
                {
                    txtRMA.Enabled = true;
                }
                if (HdnProjSetup.Value.ToUpper().Contains("ZPTAGZZEDITZ") == true)
                {
                    txtProjectTag.Enabled = true;
                }
            }

            if (ProcessLevel.ToUpper() == "RECEIVEDOA"

             || ProcessLevel.ToUpper() == "RECEIVEDOAB"
             || ProcessLevel.ToUpper() == "RECEIVEWARRANTYB"
             || ProcessLevel.ToUpper() == "RECEIVEDEFECTIVE"
             || ProcessLevel.ToUpper() == "RECEIVEREPAIRED"
             || ProcessLevel.ToUpper() == "RECEIVEGENERAL"
             || ProcessLevel.ToUpper() == "RECEIVEINWARRANTY"
             || ProcessLevel.ToUpper() == "RECEIVEEXWARRANTY"
             || ProcessLevel.ToUpper() == "RECEIVEOOWARRANTY")
            {
                TextBoxWatermarkExtender9.WatermarkText = "Work Order Number";
                txtRMA.ToolTip = "Work Order Number";
            }
            SetupProcessCheckBoxes(ctx);
            //log.LogIt("SetupProcessCheckBoxes Done");
            lblProcessHeader.Text = project + ":" + ProcessLevel;
            lblActiveProcess.Text = ProcessLevel;
            HdnCurrentProcess.Value = ProcessLevel;
            ProcessManager qm = new ProcessManager(User.Identity.Name, Roles.GetRolesForUser(User.Identity.Name));
            HdnCurrentProcess.Value = ProcessLevel;
            decimal pID = qm.GetProcessidFromName(ctx, ProcessLevel, project);
            //log.LogIt("GetProcessidFromName Done");
            HdnCurrentProcessID.Value = pID.ToString();



            Process p = qm.GetProcesssNoRestrict(pID);
            //Process p = ctx.Processes.FirstOrDefault(x => x.ProcessID == pID);
            if ((Roles.IsUserInRole(User.Identity.Name, "AddKeepStatic") == true) || (p != null && p.TurnStickyOn == true))
            {
                //chkSticky.Checked = true;
                chkSticky.Visible = true;
            }
            ////log.LogIt("qm.GetProcesss Done");



            hdnForcePrintOnSave.Value = "";          //We don't want to force the print.... normally
            btnBagTag.Enabled = true; btnBagTag_b.Enabled = true;
            if (p.DisablePrint != null && p.DisablePrint == true) { btnBagTag.Enabled = false; btnBagTag_b.Enabled = false; }
            if (p.ForcePrintOnSave != null && p.ForcePrintOnSave == true) { hdnForcePrintOnSave.Value = "Y"; }


            hdnManditoryFields.Value = "";
            hdnIsProcessReadOnly.Value = "0";   //--- False
            hdnAllowXBINX.Value = "0";   //--- False
            if (p != null)
            {
                lblProcessHeader.Text = project + ":" + p.ButtonText;
                if (p.isReadOnly != null && p.isReadOnly == true) { hdnIsProcessReadOnly.Value = "1"; }
                if (p.AllowXBINX != null && p.AllowXBINX == true) { hdnAllowXBINX.Value = "1"; }
                if (p.Name.Length > 6 && p.Name.Substring(0, 7).ToUpper() == "RECEIVE") { hdnAllowXBINX.Value = "0"; }  //  You can not XBINX on a receive screen.
            }
            NextStep.DataSource = qm.GetNextProcessSteps(ctx, ProcessLevel, project);    // (from z in ctx.NextProcessSteps.Where(y => y.Process1.Name.ToUpper() == ProcessLevel).OrderBy(y => y.Sequence) select z);
            NextStep.DataBind();
            //log.LogIt("Bind NextStep");

            //NextStepBulk.DataSource = qm.GetNextProcessSteps(ctx, ProcessLevel, project);    // (from z in ctx.NextProcessSteps.Where(y => y.Process1.Name.ToUpper() == ProcessLevel).OrderBy(y => y.Sequence) select z);
            //NextStepBulk.DataBind();
            //log.LogIt("Bind NextStepBulk");

            ViewProcess.DataSource = qm.GetProcesssButtonsIncludeRole(ctx, ProcessLevel, project);
            ViewProcess.DataBind();
            //log.LogIt("Bind ViewProcess");

            gd1.DataSource = qm.GetQuestionsThisProcess(ctx, ProcessLevel, project);       // (from x in ctx.Questions.OrderBy(y => y.Sequence) select x);
            gd1.DataBind();
            ////log.LogIt("Bind gd1");

            GridView2.DataSource = qm.GetQuestionsThisProcessHeader(ctx, ProcessLevel, projectID).OrderBy(x => x.Sequence).ThenBy(x => x.Name);
            GridView2.DataBind();
            //log.LogIt("Bind GridView2");


            hdnOptionKeyReplaceIMEI.Value = "";
            //if (ProcessLevel.ToUpper() == "GMP REPAIR")
            if (ProcessLevel.Length > 6 && ProcessLevel.Substring(0, 7).ToUpper() == "_REPAIR")
            {
                // We need to set this hidden field with the "ReplacementIMEI Option Key Code.  TX_999 (999 is the optionID for the question.
                // "TX_" + ox.OptionID.ToString() + " "
                Option o = ctx.Options.FirstOrDefault(x => x.Question.Name.ToUpper() == "Replacement IMEI");
                if (o != null)
                {
                    hdnOptionKeyReplaceIMEI.Value = "TX_" + o.OptionID.ToString();
                }
            }


            if (ProcessLevel.ToUpper() == "BULKMOVE")
            {
                GridView4.DataSource = qm.GetQuestionsThisProcess(ctx, ProcessLevel, project);       // (from x in ctx.Questions.OrderBy(y => y.Sequence) select x);
                GridView4.DataBind();
                //log.LogIt("Bind GridView4");
                pnlInputTargetArea.Visible = true;
            }
            else { pnlInputTargetArea.Visible = false; }
            if (RunSetup == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "SetUP", "SetUpScreen('" + ProcessLevel + "');", true);
            }
            //log.LogIt("Exit LoadProcessLevelData");
        }

        void ViewProcess_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            ////writeTrace(true);
            Process ns = (Process)e.Item.DataItem;
            if (ns != null)
            {
                Button b1 = (Button)e.Item.FindControl("btnViewProcess");
                if (b1 != null)
                {
                    b1.CommandArgument = ns.ProcessID.ToString();
                    b1.CommandName = ns.Name;
                    if (ns.ButtonText == null)
                    {
                        b1.Text = ns.Name;
                    }
                    else
                    {
                        b1.Text = ns.ButtonText;
                    }
                    if (ns.MacroKey != null && ns.MacroKey.Trim().Length > 0)
                    {
                        b1.Text = b1.Text;               // +'.' + ns.MacroKey;
                    }
                    b1.Attributes.Add("OnClick", "StoreHeaderData(); return OKToProceed('" + b1.CommandName + "'," + b1.CommandArgument + ");");
                }
            }
            ////writeTrace(false);
        }
        void NextStepBulk_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            ////writeTrace(true);
            NextProcessStep ns = (NextProcessStep)e.Item.DataItem;
            if (ns != null)
            {
                Button b1 = (Button)e.Item.FindControl("btnNextStepBulk");
                if (b1 != null)
                {
                    b1.CommandArgument = ns.NextProcessStepID.ToString();
                    b1.CommandName = ns.Process.Name;
                    b1.Text = ns.Process.Name;
                    if (ns.MacroKey != null && ns.MacroKey.Length > 0)
                    {
                        b1.Text = b1.Text + '.' + ns.MacroKey;
                    }
                    b1.Attributes.Add("OnClick", "return OKToNextStepBulk('" + b1.CommandName + "'," + b1.CommandArgument + ",'bProcessClick');");

                }
            }
            ////writeTrace(false);
        }
        void NextStep_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            ////writeTrace(true);
            NextProcessStep ns = (NextProcessStep)e.Item.DataItem;
            if (ns != null)
            {
                Button b1 = (Button)e.Item.FindControl("btnNextStep");
                if (b1 != null)
                {
                    b1.CommandArgument = ns.NextProcessStepID.ToString();
                    b1.CommandName = ns.Process.Name;
                    b1.Text = ns.Process.Name;
                    if (ns.MacroKey != null && ns.MacroKey.Length > 0)
                    {
                        b1.Text = b1.Text + '.' + ns.MacroKey;
                    }
                    b1.Attributes.Add("OnClick", "return OKToNextStep('" + b1.CommandName + "'," + b1.CommandArgument + ",'bNextStepClick');");
                }
            }
            ////writeTrace(false);
        }
        void gd1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            ////writeTrace(true);
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //log.LogIt("gd1_RowDataBound");
                string ValidString = "";
                HiddenField HID = (HiddenField)e.Row.FindControl("HiddenID");
                //HiddenField isManditory = (HiddenField)e.Row.FindControl("isManditory");
                HiddenField HName = (HiddenField)e.Row.FindControl("HiddenName");
                Question rec = (Question)e.Row.DataItem;
                //isManditory.Value = rec.isManditory.ToString();
                decimal ID = rec.QuestionID;
                Label l1 = (Label)e.Row.FindControl("Description");


                CheckBoxList c1 = (CheckBoxList)e.Row.FindControl("checkAnswer");
                //MultiSelectionDropDown c1 = (MultiSelectionDropDown)e.Row.FindControl("checkAnswer");

                TextBox tn = (TextBox)e.Row.FindControl("NumericAnswer");
                TextBox Currencyn = (TextBox)e.Row.FindControl("CurrencyAnswer");

                TextBox t1 = (TextBox)e.Row.FindControl("TextAnswer");
                RadioButtonList R1 = (RadioButtonList)e.Row.FindControl("RadioAnswer");
                TextBox Cal = (TextBox)e.Row.FindControl("CalAnswer");

                TextBox n3 = (TextBox)e.Row.FindControl("Num3Digit");
                TextBox t20 = (TextBox)e.Row.FindControl("Text20Digit");

                TextBox t3 = (TextBox)e.Row.FindControl("Text3Digit");
                TextBox t10 = (TextBox)e.Row.FindControl("Text10Digit");
                TextBox t18 = (TextBox)e.Row.FindControl("Text18Digit");
                TextBox t50 = (TextBox)e.Row.FindControl("Text50Digit");

                //|| QuestionType == "Text3Digit"
                //|| QuestionType == "Text10Digit"
                //|| QuestionType == "Text18Digit"
                //|| QuestionType == "Text50Digit"


                DropDownList drp = (DropDownList)e.Row.FindControl("drpAnswer");
                try
                {
                    ValidString = LoadData(rec, HName, HID, l1, c1, t1, tn, Currencyn, R1, Cal, drp, n3, t20, t3, t10, t18, t50, true);
                }
                catch (Exception ex)
                {
                    //log.LogIt("gd1_RowDataBound:" + ex.Message);
                }
                if (rec.isManditory == true && ValidString.Length > 0)
                {
                    hdnManditoryFields.Value = hdnManditoryFields.Value + (hdnManditoryFields.Value.Length == 0 ? "" : ",") + ValidString;
                }
            }
            ////writeTrace(false);
        }
        void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            ////writeTrace(true);
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //log.LogIt("GridView2_RowDataBound");
                string ValidString = "";
                //HiddenField isManditory = (HiddenField)e.Row.FindControl("isManditoryh");
                HiddenField HID = (HiddenField)e.Row.FindControl("HiddenIDh");
                HiddenField HName = (HiddenField)e.Row.FindControl("HiddenNameh");
                Question rec = (Question)e.Row.DataItem;
                //isManditory.Value = rec.isManditory.ToString();
                Label l1 = (Label)e.Row.FindControl("Descriptionh");


                CheckBoxList c1 = (CheckBoxList)e.Row.FindControl("checkAnswerh");

                //MultiSelectionDropDown c1 = (MultiSelectionDropDown)e.Row.FindControl("checkAnswerh");


                TextBox tn = (TextBox)e.Row.FindControl("NumericAnswerh");
                TextBox Currencyn = (TextBox)e.Row.FindControl("CurrencyAnswerh");

                TextBox t1 = (TextBox)e.Row.FindControl("TextAnswerh");
                RadioButtonList R1 = (RadioButtonList)e.Row.FindControl("RadioAnswerh");
                TextBox Cal = (TextBox)e.Row.FindControl("CalAnswerh");
                TextBox n3 = (TextBox)e.Row.FindControl("Num3Digith");
                TextBox t20 = (TextBox)e.Row.FindControl("Text20Digith");

                TextBox t3 = (TextBox)e.Row.FindControl("Text3Digith");
                TextBox t10 = (TextBox)e.Row.FindControl("Text10Digith");
                TextBox t18 = (TextBox)e.Row.FindControl("Text18Digith");
                TextBox t50 = (TextBox)e.Row.FindControl("Text50Digith");

                DropDownList drp = (DropDownList)e.Row.FindControl("drpAnswerh");
                try
                {
                    ValidString = LoadData(rec, HName, HID, l1, c1, t1, tn, Currencyn, R1, Cal, drp, n3, t20, t3, t10, t18, t50, false);
                }
                catch (Exception ex)
                {
                    //log.LogIt("GridView2_RowDataBound:" + ex.Message);
                }
            }
        }

        private string LoadData(Question Quest, HiddenField HName, HiddenField HID, Label l1, CheckBoxList c1, TextBox t1, TextBox tn, TextBox Currencyn, RadioButtonList R1, TextBox Cal, DropDownList D1, TextBox n3, TextBox t20,
 TextBox t3, TextBox t10, TextBox t18, TextBox t50, bool bEnabled)
        {
            //log.LogIt("LoadData started");
            string ValidString = l1.ClientID + ":";              // rec.QuestionID.ToString() + ":"; 
            decimal ID = Quest.QuestionID;
            R1.Items.Clear();
            c1.Items.Clear();
            tn.Visible = false;
            c1.Visible = false;
            t1.Visible = false;
            Currencyn.Visible = false;
            R1.Visible = false;
            Cal.Visible = false;
            D1.Visible = false;
            n3.Visible = false;
            t20.Visible = false;
            t3.Visible = false;
            t10.Visible = false;
            t18.Visible = false;
            t50.Visible = false;


            if (hdnIsProcessReadOnly.Value == "1") { bEnabled = false; }
            Cal.Text = DateTime.Now.ToShortDateString();
            string xy = DateTime.Now.TimeOfDay.ToString();
            t1.Text = "";
            l1.Text = Quest.Description + ":";
            HID.Value = Quest.QuestionID.ToString();
            HName.Value = Quest.QuestionType.Type.ToUpper();
            //log.LogIt("LoadData Question:" + Quest.Description + "(" + Quest.QuestionType.Type + ")");
            if (bEnabled == true)
            {
                if (Quest.Name == "Carrier") { hdnCarrierID.Value = D1.ClientID; }
                if (Quest.Name == "Manufacturer") { hdnManufacturerID.Value = D1.ClientID; }
                if (Quest.Name == "Model") { hdnModelID.Value = D1.ClientID; }
                if (Quest.Name == "Colour") { hdnColourID.Value = D1.ClientID; }
                #region Grab Bin specific Stuff
                if (Quest.Name == "Bin")
                {
                    switch (Quest.QuestionType.Type.ToUpper())
                    {
                        case "DROPDOWN":
                            hdnBinID.Value = D1.ClientID;
                            break;
                        case "RADIALBUTTON":
                            hdnBinID.Value = R1.ClientID;
                            break;
                        case "CALC":
                            hdnBinID.Value = t1.ClientID;
                            break;
                        case "NUMERIC":
                            hdnBinID.Value = tn.ClientID;
                            break;
                        case "CURRENCY":
                            hdnBinID.Value = Currencyn.ClientID;
                            break;
                        case "READONLY":
                            hdnBinID.Value = t1.ClientID;
                            break;
                        case "KEYBOARD":
                            hdnBinID.Value = t1.ClientID;
                            break;
                        case "NUM3DIGIT":
                            hdnBinID.Value = n3.ClientID;
                            break;
                        case "TEXT20DIGIT":
                            hdnBinID.Value = t20.ClientID;
                            break;
                        case "TEXT3DIGIT":
                            hdnBinID.Value = t3.ClientID;
                            break;
                        case "TEXT10DIGIT":
                            hdnBinID.Value = t10.ClientID;
                            break;
                        case "TEXT18DIGIT":
                            hdnBinID.Value = t18.ClientID;
                            break;
                        case "TEXT50DIGIT":
                            hdnBinID.Value = t50.ClientID;
                            break;
                        case "CALENDAR":
                            hdnBinID.Value = Cal.ClientID;
                            break;
                        case "CHECKBOX":
                            hdnBinID.Value = c1.ClientID;
                            break;
                    }
                }
                #endregion
                #region Grab "Lab Destination" specific Stuff
                if (Quest.Name == "Lab Destination")
                {
                    switch (Quest.QuestionType.Type.ToUpper())
                    {
                        case "DROPDOWN":
                            hdnLabDestinationID.Value = D1.ClientID;
                            break;
                        case "RADIALBUTTON":
                            hdnLabDestinationID.Value = R1.ClientID;
                            break;
                        case "CALC":
                            hdnLabDestinationID.Value = t1.ClientID;
                            break;
                        case "NUMERIC":
                            hdnLabDestinationID.Value = tn.ClientID;
                            break;
                        case "CURRENCY":
                            hdnLabDestinationID.Value = Currencyn.ClientID;
                            break;
                        case "READONLY":
                            hdnLabDestinationID.Value = t1.ClientID;
                            break;
                        case "KEYBOARD":
                            hdnLabDestinationID.Value = t1.ClientID;
                            break;
                        case "NUM3DIGIT":
                            hdnLabDestinationID.Value = n3.ClientID;
                            break;
                        case "TEXT20DIGIT":
                            hdnLabDestinationID.Value = t20.ClientID;
                            break;
                        case "TEXT3DIGIT":
                            hdnLabDestinationID.Value = t3.ClientID;
                            break;
                        case "TEXT10DIGIT":
                            hdnLabDestinationID.Value = t10.ClientID;
                            break;
                        case "TEXT18DIGIT":
                            hdnLabDestinationID.Value = t18.ClientID;
                            break;
                        case "TEXT50DIGIT":
                            hdnLabDestinationID.Value = t50.ClientID;
                            break;
                        case "CALENDAR":
                            hdnLabDestinationID.Value = Cal.ClientID;
                            break;
                        case "CHECKBOX":
                            hdnLabDestinationID.Value = c1.ClientID;
                            break;
                    }
                }
                #endregion
            }
            Option ox = new Option();

            switch (Quest.QuestionType.Type.ToUpper())
            {
                #region Calc
                case "CALC":
                    t1.Visible = true;
                    t1.Enabled = false;
                    t1.ToolTip = Quest.HelpText;
                    t1.Attributes["onMouseOver"] = "return ShowTooTip('" + t1.ClientID + "', true);";
                    t1.Attributes["onMouseOut"] = "return ShowTooTip('" + t1.ClientID + "', false);";
                    ox = Quest.Options.FirstOrDefault();
                    if (ox != null)
                    {
                        t1.Attributes.Add("someValue", ox.OptionID.ToString());
                        ValidString += "TX_" + ox.OptionID.ToString() + " ";
                    }
                    //if (bEnabled == true)
                    //{
                    hdnQuestionIDList.Value += "," + Quest.QuestionID.ToString();
                    hdnQuestionClientIDList.Value += "," + t1.ClientID;

                    //}
                    break;
                #endregion
                #region Numeric
                case "NUMERIC":
                    tn.Visible = true;
                    tn.ToolTip = Quest.HelpText;
                    tn.Attributes["onMouseOver"] = "return ShowTooTip('" + tn.ClientID + "', true);";
                    tn.Attributes["onMouseOut"] = "return ShowTooTip('" + tn.ClientID + "', false);";
                    ox = Quest.Options.FirstOrDefault();
                    if (ox != null)
                    {
                        tn.Attributes.Add("someValue", ox.OptionID.ToString());
                        ValidString += "TX_" + ox.OptionID.ToString() + " ";
                    }
                    tn.Enabled = bEnabled;
                    if (Quest.isReadOnly != null && Quest.isReadOnly == true) { tn.Enabled = false; }
                    //if (bEnabled == true)
                    //{
                    hdnQuestionIDList.Value += "," + Quest.QuestionID.ToString();
                    hdnQuestionClientIDList.Value += "," + tn.ClientID;
                    //}
                    break;
                #endregion
                #region Currency
                case "CURRENCY":
                    Currencyn.Visible = true;
                    Currencyn.ToolTip = Quest.HelpText;
                    Currencyn.Attributes["onMouseOver"] = "return ShowTooTip('" + Currencyn.ClientID + "', true);";
                    Currencyn.Attributes["onMouseOut"] = "return ShowTooTip('" + Currencyn.ClientID + "', false);";
                    ox = Quest.Options.FirstOrDefault();
                    if (ox != null)
                    {
                        Currencyn.Attributes.Add("someValue", ox.OptionID.ToString());
                        ValidString += "TX_" + ox.OptionID.ToString() + " ";
                    }
                    Currencyn.Enabled = bEnabled;
                    if (Quest.isReadOnly != null && Quest.isReadOnly == true) { Currencyn.Enabled = false; }
                    //if (bEnabled == true)
                    //{
                    hdnQuestionIDList.Value += "," + Quest.QuestionID.ToString();
                    hdnQuestionClientIDList.Value += "," + Currencyn.ClientID;
                    //}
                    break;
                #endregion
                #region ReadOnly
                case "READONLY":
                    t1.Visible = true;
                    t1.ToolTip = Quest.HelpText;
                    t1.Attributes["onMouseOver"] = "return ShowTooTip('" + t1.ClientID + "', true);";
                    t1.Attributes["onMouseOut"] = "return ShowTooTip('" + t1.ClientID + "', false);";
                    ox = Quest.Options.FirstOrDefault();
                    if (Quest.Name.Length > 5 && Quest.Name.Substring(0, 5).ToUpper() == "PART ")
                    {
                        hdnPartNumberIDs.Value += ox.OptionID.ToString() + ",";
                        //imgShowPartList.Visible = true;
                    }
                    if (Quest.Name.ToUpper() == "IFS PO NUMBER")
                    {
                        hdnPONumberIDs.Value += ox.OptionID.ToString() + ",";
                        //imgShowIFSPONumbers.Visible = true;
                    }
                    if (ox != null)
                    {
                        t1.Attributes.Add("someValue", ox.OptionID.ToString());
                        ValidString += "TX_" + ox.OptionID.ToString() + " ";
                    }
                    t1.Enabled = false;                //bEnabled;
                    if (Quest.isReadOnly != null && Quest.isReadOnly == true) { t1.Enabled = false; }
                    //if (bEnabled == true)
                    //{
                    hdnQuestionIDList.Value += "," + Quest.QuestionID.ToString();
                    hdnQuestionClientIDList.Value += "," + t1.ClientID;
                    break;
                #endregion
                #region Keyboard
                case "KEYBOARD":
                    t1.Visible = true;
                    t1.ToolTip = Quest.HelpText;
                    t1.Attributes["onMouseOver"] = "return ShowTooTip('" + t1.ClientID + "', true);";
                    t1.Attributes["onMouseOut"] = "return ShowTooTip('" + t1.ClientID + "', false);";
                    ox = Quest.Options.FirstOrDefault();

                    if (Quest.Name.Length > 5 && Quest.Name.Substring(0, 5).ToUpper() == "PART ")
                    {
                        hdnPartNumberIDs.Value += ox.OptionID.ToString() + ",";
                        imgShowPartList.Visible = true;
                        imgShowPartReturnList.Visible = true;
                    }


                    if (Quest.Name.ToUpper() == "IFS PO NUMBER")
                    {
                        hdnPONumberIDs.Value += ox.OptionID.ToString() + ",";
                        imgShowIFSPONumbers.Visible = true;
                    }
                    if (Quest.Name.ToUpper() == "IFS VENDOR")
                    {
                        hdnPOVendorIDs.Value += ox.OptionID.ToString() + ",";
                        //imgShowIFSPONumbers.Visible = true;
                    }
                    if (Quest.Name.ToUpper() == "IFS PO LINE NUMBER")
                    {
                        hdnPOLineNumberIDs.Value += ox.OptionID.ToString() + ",";
                        //imgShowIFSPONumbers.Visible = true;
                    }
                    if (Quest.Name.ToUpper() == "IFS PO UNIT COST")
                    {
                        hdnPOUnnitCostIDs.Value += ox.OptionID.ToString() + ",";
                        //imgShowIFSPONumbers.Visible = true;
                    }


                    if (ox != null)
                    {
                        t1.Attributes.Add("someValue", ox.OptionID.ToString());
                        ValidString += "TX_" + ox.OptionID.ToString() + " ";
                    }
                    t1.Enabled = bEnabled;
                    if (Quest.isReadOnly != null && Quest.isReadOnly == true) { t1.Enabled = false; }
                    //if (bEnabled == true)
                    //{
                    hdnQuestionIDList.Value += "," + Quest.QuestionID.ToString();
                    hdnQuestionClientIDList.Value += "," + t1.ClientID;

                    //// Turn on the wrench to allow picking of parts.
                    //if (Quest.Description.ToUpper() == "PART #")
                    //{
                    //    imgShowPartList.Visible = true;
                    //}

                    //}
                    break;
                #endregion
                //////////////////////////////////////
                #region Num30Digit
                case "NUM3DIGIT":
                    n3.Visible = true;
                    n3.ToolTip = Quest.HelpText;
                    n3.Attributes["onMouseOver"] = "return ShowTooTip('" + n3.ClientID + "', true);";
                    n3.Attributes["onMouseOut"] = "return ShowTooTip('" + n3.ClientID + "', false);";
                    ox = Quest.Options.FirstOrDefault();

                    if (Quest.Name.Length > 5 && Quest.Name.Substring(0, 5).ToUpper() == "PART ")
                    {
                        hdnPartNumberIDs.Value += ox.OptionID.ToString() + ",";
                    }


                    if (ox != null)
                    {
                        n3.Attributes.Add("someValue", ox.OptionID.ToString());
                        ValidString += "TX_" + ox.OptionID.ToString() + " ";
                    }
                    n3.Enabled = bEnabled;
                    if (Quest.isReadOnly != null && Quest.isReadOnly == true) { n3.Enabled = false; }
                    //if (bEnabled == true)
                    //{
                    hdnQuestionIDList.Value += "," + Quest.QuestionID.ToString();
                    hdnQuestionClientIDList.Value += "," + n3.ClientID;
                    //}
                    break;
                #endregion
                #region Text20Digit
                case "TEXT20DIGIT":
                    t20.Visible = true;
                    t20.ToolTip = Quest.HelpText;
                    t20.Attributes["onMouseOver"] = "return ShowTooTip('" + t20.ClientID + "', true);";
                    t20.Attributes["onMouseOut"] = "return ShowTooTip('" + t20.ClientID + "', false);";
                    ox = Quest.Options.FirstOrDefault();

                    if (Quest.Name.Length > 5 && Quest.Name.Substring(0, 5).ToUpper() == "PART ")
                    {
                        hdnPartNumberIDs.Value += ox.OptionID.ToString() + ",";
                    }


                    if (ox != null)
                    {
                        t20.Attributes.Add("someValue", ox.OptionID.ToString());
                        ValidString += "TX_" + ox.OptionID.ToString() + " ";
                    }
                    t20.Enabled = bEnabled;
                    if (Quest.isReadOnly != null && Quest.isReadOnly == true) { t20.Enabled = false; }
                    //if (bEnabled == true)
                    //{
                    hdnQuestionIDList.Value += "," + Quest.QuestionID.ToString();
                    hdnQuestionClientIDList.Value += "," + t20.ClientID;
                    //}
                    break;
                #endregion
                #region Text30Digit
                case "TEXT3DIGIT":
                    t3.Visible = true;
                    t3.ToolTip = Quest.HelpText;
                    t3.Attributes["onMouseOver"] = "return ShowTooTip('" + t3.ClientID + "', true);";
                    t3.Attributes["onMouseOut"] = "return ShowTooTip('" + t3.ClientID + "', false);";
                    ox = Quest.Options.FirstOrDefault();

                    if (Quest.Name.Length > 5 && Quest.Name.Substring(0, 5).ToUpper() == "PART ")
                    {
                        hdnPartNumberIDs.Value += ox.OptionID.ToString() + ",";
                    }


                    if (ox != null)
                    {
                        t3.Attributes.Add("someValue", ox.OptionID.ToString());
                        ValidString += "TX_" + ox.OptionID.ToString() + " ";
                    }
                    t3.Enabled = bEnabled;
                    if (Quest.isReadOnly != null && Quest.isReadOnly == true) { t3.Enabled = false; }
                    hdnQuestionIDList.Value += "," + Quest.QuestionID.ToString();
                    hdnQuestionClientIDList.Value += "," + t3.ClientID;
                    break;
                #endregion
                #region Test10Digit
                case "TEXT10DIGIT":
                    t10.Visible = true;
                    t10.ToolTip = Quest.HelpText;
                    t10.Attributes["onMouseOver"] = "return ShowTooTip('" + t10.ClientID + "', true);";
                    t10.Attributes["onMouseOut"] = "return ShowTooTip('" + t10.ClientID + "', false);";
                    ox = Quest.Options.FirstOrDefault();

                    if (Quest.Name.Length > 5 && Quest.Name.Substring(0, 5).ToUpper() == "PART ")
                    {
                        hdnPartNumberIDs.Value += ox.OptionID.ToString() + ",";
                    }
                    if (ox != null)
                    {
                        t10.Attributes.Add("someValue", ox.OptionID.ToString());
                        ValidString += "TX_" + ox.OptionID.ToString() + " ";
                    }
                    t10.Enabled = bEnabled;
                    if (Quest.isReadOnly != null && Quest.isReadOnly == true) { t10.Enabled = false; }
                    hdnQuestionIDList.Value += "," + Quest.QuestionID.ToString();
                    hdnQuestionClientIDList.Value += "," + t10.ClientID;
                    break;
                #endregion
                #region Text18Digit
                case "TEXT18DIGIT":
                    t18.Visible = true;
                    t18.ToolTip = Quest.HelpText;
                    t18.Attributes["onMouseOver"] = "return ShowTooTip('" + t18.ClientID + "', true);";
                    t18.Attributes["onMouseOut"] = "return ShowTooTip('" + t18.ClientID + "', false);";
                    ox = Quest.Options.FirstOrDefault();

                    if (Quest.Name.Length > 5 && Quest.Name.Substring(0, 5).ToUpper() == "PART ")
                    {
                        hdnPartNumberIDs.Value += ox.OptionID.ToString() + ",";
                    }


                    if (ox != null)
                    {
                        t18.Attributes.Add("someValue", ox.OptionID.ToString());
                        ValidString += "TX_" + ox.OptionID.ToString() + " ";
                    }
                    t18.Enabled = bEnabled;
                    if (Quest.isReadOnly != null && Quest.isReadOnly == true) { t18.Enabled = false; }
                    hdnQuestionIDList.Value += "," + Quest.QuestionID.ToString();
                    hdnQuestionClientIDList.Value += "," + t18.ClientID;
                    break;
                #endregion
                #region Text50Digit
                case "TEXT50DIGIT":
                    t50.Visible = true;
                    t50.ToolTip = Quest.HelpText;
                    t50.Attributes["onMouseOver"] = "return ShowTooTip('" + t50.ClientID + "', true);";
                    t50.Attributes["onMouseOut"] = "return ShowTooTip('" + t50.ClientID + "', false);";
                    ox = Quest.Options.FirstOrDefault();

                    if (Quest.Name.Length > 5 && Quest.Name.Substring(0, 5).ToUpper() == "PART ")
                    {
                        hdnPartNumberIDs.Value += ox.OptionID.ToString() + ",";
                    }
                    if (ox != null)
                    {
                        t50.Attributes.Add("someValue", ox.OptionID.ToString());
                        ValidString += "TX_" + ox.OptionID.ToString() + " ";
                    }
                    t50.Enabled = bEnabled;
                    if (Quest.isReadOnly != null && Quest.isReadOnly == true) { t50.Enabled = false; }
                    hdnQuestionIDList.Value += "," + Quest.QuestionID.ToString();
                    hdnQuestionClientIDList.Value += "," + t50.ClientID;
                    break;
                #endregion
                ///////////////////////////////////////
                #region Calendar
                case "CALENDAR":
                    Cal.Visible = true;
                    Cal.ToolTip = Quest.HelpText;
                    Cal.Attributes["onMouseOver"] = "return ShowTooTip('" + Cal.ClientID + "', true);";
                    Cal.Attributes["onMouseOut"] = "return ShowTooTip('" + Cal.ClientID + "', false);";
                    ox = Quest.Options.FirstOrDefault();
                    if (ox != null)
                    {
                        Cal.Attributes.Add("someValue", ox.OptionID.ToString());
                        ValidString += "TX_" + ox.OptionID.ToString() + " ";
                    }
                    Cal.Enabled = bEnabled;
                    if (Quest.isReadOnly != null && Quest.isReadOnly == true) { Cal.Enabled = false; }
                    hdnQuestionIDList.Value += "," + Quest.QuestionID.ToString();
                    hdnQuestionClientIDList.Value += "," + Cal.ClientID;
                    break;
                #endregion
                #region DropDown
                case "DROPDOWN":
                    D1.Visible = true;
                    D1.Items.Clear();
                    D1.ToolTip = Quest.HelpText;
                    D1.Attributes["onMouseOver"] = "return ShowTooTip('" + D1.ClientID + "', true);";
                    D1.Attributes["onMouseOut"] = "return ShowTooTip('" + D1.ClientID + "', false);";

                    //if (rec.Name == "Carrier" ||
                    //    rec.Name == "Manufacturer" ||
                    //    rec.Name == "Model")
                    //{
                    D1.Attributes.Add("onchange", "javascript:SetupDropDown('" + Quest.Name + "')");
                    //}
                    foreach (Option o in Quest.Options.Where(x => x.OptionStatus.Status.ToUpper() != "INACTIVE").OrderBy(x => x.Sequence))
                    {
                        ListItem x = new ListItem(o.OptionText, o.OptionID.ToString());
                        D1.Items.Add(x);
                        ValidString += "DD_" + o.OptionID.ToString() + " ";
                    }
                    D1.Enabled = bEnabled;
                    if (Quest.isReadOnly != null && Quest.isReadOnly == true) { D1.Enabled = false; }
                    hdnQuestionIDList.Value += "," + Quest.QuestionID.ToString();
                    hdnQuestionClientIDList.Value += "," + D1.ClientID;
                    break;
                #endregion
                #region RadialButton
                case "RADIALBUTTON":
                    R1.Visible = true;
                    R1.ToolTip = Quest.HelpText;
                    R1.Attributes["onMouseOver"] = "return ShowTooTip('" + R1.ClientID + "', true);";
                    R1.Attributes["onMouseOut"] = "return ShowTooTip('" + R1.ClientID + "', false);";

                    int xmLength = 0;
                    int xCount = 0;
                    int xSet = 4;

                    R1.Attributes.Add("onchange", "javascript:SetupDropDown('" + Quest.Name + "');");
                    foreach (Option o in Quest.Options.Where(x => x.OptionStatus.Status.ToUpper() != "INACTIVE").OrderBy(x => x.Sequence))
                    {
                        ListItem x = new ListItem(o.OptionText, o.OptionID.ToString());
                        xCount += 1;
                        xmLength += x.Text.Length;
                        if (xmLength > 45)
                        {
                            if (xCount < xSet) { xSet = xCount; }
                            xCount = 0;
                            xmLength = 0;
                        }
                        if (Quest.Name.ToUpper() == "LAB DESTINATION")
                        {
                            x.Attributes.Add("onclick", "javascript:SetLabDestination(this);");
                        }
                        R1.Items.Add(x);
                        ValidString += "RD_" + o.OptionID.ToString() + " ";
                    }

                    if (Quest.ShowVertical == true) { R1.RepeatDirection = RepeatDirection.Vertical; R1.RepeatColumns = 1; }
                    else { R1.RepeatDirection = RepeatDirection.Horizontal; R1.RepeatColumns = xSet; }

                    R1.Enabled = bEnabled;
                    if (Quest.isReadOnly != null && Quest.isReadOnly == true) { R1.Enabled = false; }

                    hdnQuestionIDList.Value += "," + Quest.QuestionID.ToString();
                    hdnQuestionClientIDList.Value += "," + R1.ClientID;
                    break;
                #endregion
                #region Checkbox
                case "CHECKBOX":
                    c1.Visible = true;
                    c1.ToolTip = Quest.HelpText;
                    c1.Attributes["onMouseOver"] = "return ShowTooTip('" + c1.ClientID + "', true);";
                    c1.Attributes["onMouseOut"] = "return ShowTooTip('" + c1.ClientID + "', false);";
                    xmLength = 0;
                    xCount = 0;
                    xSet = 5;
                    c1.Attributes.Add("onchange", "javascript:SetupDropDown('" + Quest.Name + "')");
                    foreach (Option o in Quest.Options.Where(x => x.OptionStatus.Status.ToUpper() != "INACTIVE").OrderBy(x => x.Sequence))
                    {
                        ListItem x = new ListItem(o.OptionText, o.OptionID.ToString());
                        xCount += 1;
                        xmLength += x.Text.Length;
                        if (xmLength > 45)
                        {
                            if (xCount < xSet) { xSet = xCount; }
                            xCount = 0;
                            xmLength = 0;
                        }
                        x.Attributes.Add("someValue", o.OptionID.ToString());
                        c1.Items.Add(x);
                        ValidString += "CB_" + o.OptionID.ToString() + " ";
                    }

                    if (Quest.ShowVertical == true) { c1.RepeatDirection = RepeatDirection.Vertical; c1.RepeatColumns = 1; }
                    else { c1.RepeatDirection = RepeatDirection.Horizontal; c1.RepeatColumns = xSet; }
                    c1.Enabled = bEnabled;
                    if (Quest.isReadOnly != null && Quest.isReadOnly == true) { c1.Enabled = false; }

                    hdnQuestionIDList.Value += "," + Quest.QuestionID.ToString();
                    hdnQuestionClientIDList.Value += "," + c1.ClientID;
                    //}
                    break;
                #endregion
                default:
                    //log.LogIt("Inside LoadData Default");
                    c1.Visible = true;
                    c1.ToolTip = Quest.HelpText;
                    c1.Attributes["onMouseOver"] = "return ShowTooTip('" + c1.ClientID + "', true);";
                    c1.Attributes["onMouseOut"] = "return ShowTooTip('" + c1.ClientID + "', false);";
                    xmLength = 0;
                    xCount = 0;
                    xSet = 5;
                    foreach (Option o in Quest.Options.OrderBy(x => x.Sequence))
                    {
                        ListItem x = new ListItem(o.OptionText, o.OptionID.ToString());
                        xCount += 1;
                        xmLength += x.Text.Length;
                        if (xmLength > 45)
                        {
                            if (xCount < xSet) { xSet = xCount; }
                            xCount = 0;
                            xmLength = 0;
                        }
                        c1.Items.Add(x);
                        ValidString += "TX_" + o.OptionID.ToString() + " ";       ///// unsure if this should be "TX_", possibly should be "CB_"
                    }

                    if (Quest.ShowVertical == true) { c1.RepeatDirection = RepeatDirection.Vertical; c1.RepeatColumns = 1; }
                    else { c1.RepeatDirection = RepeatDirection.Horizontal; c1.RepeatColumns = xSet - 1; }
                    c1.Enabled = bEnabled;
                    if (Quest.isReadOnly != null && Quest.isReadOnly == true) { c1.Enabled = false; }
                    hdnQuestionIDList.Value += "," + Quest.QuestionID.ToString();
                    hdnQuestionClientIDList.Value += "," + c1.ClientID;
                    break;
            }
            //log.LogIt("LoadData ended");
            return ValidString;
        }

        private void SetupProcessCheckBoxes(clsLinqDataContext ctx)
        {
            chkProcessCheckList.Items.Clear();
            ProcessManager pm = new ProcessManager(User.Identity.Name);
            List<Process> pl = pm.GetProcesssThisProjectForCompletion(ctx, drpProjectList.SelectedItem.Text);
            foreach (Process p in pl.OrderBy(x => x.Sequence))
            {
                ListItem x = new ListItem(p.Name, p.ProcessID.ToString());
                chkProcessCheckList.Items.Add(x);
                chkProcessCheckList.Items[chkProcessCheckList.Items.Count - 1].Attributes.Add("someValue", p.ProcessID.ToString());
            }
            chkProcessCheckList.Enabled = false;
        }
        private void writeTrace(bool enteringFunction)
        {
            if (!Trace.IsEnabled)
                return;
            string callingFunctionName = "Undetermined method";
            string action = enteringFunction ? "Entering" : "Exiting";
            try
            {
                //Determine the name of the calling function. 
                System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace();
                callingFunctionName = stackTrace.GetFrame(1).GetMethod().Name;
            }
            catch { }
            Trace.Write(action, callingFunctionName);
        }
    }
}