using System;
using System.IO;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BW_WebApp.DataManagers;
using BW_WebApp.Classes;

namespace BW_WebApp
{
    public partial class Dashboard_Cellbie : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            //btnGetBeep.Click += new EventHandler(btnGetBeep_Click);
            btnCellbieReceive.Click += new EventHandler(btnCellbieReceive_Click);
            btnCellbieIMEI.Click += new EventHandler(btnCellbieIMEI_Click);
            grdTempDetail.RowDataBound += new GridViewRowEventHandler(grdTempDetail_RowDataBound);
            grdTempDetail.RowCommand += new GridViewCommandEventHandler(grdTempDetail_RowCommand);

            CellbieTabContainer.ActiveTabChanged += new EventHandler(CellbieTabContainer_ActiveTabChanged);

            //GRDLockedBatches.RowDataBound += new GridViewRowEventHandler(GRDLockedBatches_RowDataBound);
            //GRDLockedBatches.RowCommand += new GridViewCommandEventHandler(GRDLockedBatches_RowCommand);

            //imgDownloadGrid.Click += new ImageClickEventHandler(imgDownloadGrid_Click);
            //GRDInvalidBatches.RowDataBound += new GridViewRowEventHandler(GRDInvalidBatches_RowDataBound);
            //GRDInvalidBatches.RowCommand += new GridViewCommandEventHandler(GRDInvalidBatches_RowCommand);

            //GRDHoldBatches.RowCommand += new GridViewCommandEventHandler(GRDHoldBatches_RowCommand);
            //GRDHoldBatches.RowDataBound += new GridViewRowEventHandler(GRDHoldBatches_RowDataBound);

            //GRDOpenBatches.RowDataBound += new GridViewRowEventHandler(GRDOpenBatches_RowDataBound);
            //GRDOpenBatches.RowCommand += new GridViewCommandEventHandler(GRDOpenBatches_RowCommand);

            //GRDSentBatches.RowDataBound += new GridViewRowEventHandler(GRDSentBatches_RowDataBound);
            //GRDSentBatches.RowCommand += new GridViewCommandEventHandler(GRDSentBatches_RowCommand);

            //btnRefreshLocked.Click += new EventHandler(btnRefreshLocked_Click);
            //btnRefreshHold.Click += new EventHandler(btnRefreshHold_Click);
            //btnRefreshInvalid.Click += new EventHandler(btnRefreshInvalid_Click);
            //btnRefreshOpen.Click += new EventHandler(btnRefreshOpen_Click);
            //btnRefreshSent.Click += new EventHandler(btnRefreshSent_Click);

            //btnSummaryHold.Click += new EventHandler(btnSummary_Click);
            //btnSummaryLocked.Click += new EventHandler(btnSummary_Click);
            //btnSummaryOpen.Click += new EventHandler(btnSummary_Click);

            if (IsPostBack == false)
            {
                hdnUserName.Value = User.Identity.Name;
                UpdateGrid();
                CellbieDownloadGrid.Visible = false;
                ////var dateNow = DateTime.Now;

                ////BeginDate.Value = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day, 8, 0, 0);
                ////EndDate.Value = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day, DateTime.Now.TimeOfDay.Hours, DateTime.Now.Minute, 0);
            }
        }

        void btnGetBeep_Click(object sender, EventArgs e)
        {
            lblGetBeep.Text = ConvertToBase64("");
        }
        //void btnCellbieReceive_Clickx(object sender, EventArgs e)
        //{
        //    //Cellbie_Rest_Client cellbie = new Cellbie_Rest_Client(User.Identity.Name);


        //    // 2957	358040080423290     LG V30
        //    // 2958	355458060568814     Google Nexus 6
        //    // 2959	356160070900412     Samsung Galaxy S6 Edge
        //    // 2960	990004600575546     BB10


        //    Cellbie cellbie = new Cellbie(User.Identity.Name);


        //    string imei = txtCellbieIMEI.Text;
        //    string Message = "";

        //    decimal rID = 2959;

        //    BrainDeviceTransaction info = cellbie.SendReceiveTransaction(rID, imei, true, "reason", SysUtil.CellbieAPISimulate());
        //    lblCellbieParameterJSON.Text = info.CellbieParameterJSON;
        //    lblCellbieIMEIOutput.Text = info.CellbieDataJSON;
        //    lblCellbieError.Text = info.CellbieStatus.ExceptionMessage;
        //    lblCellbieErrorInternal.Text = info.CellbieStatus.InnerExceptionMessage;
        //    lblCellbieIMEIMessage.Text = Message;
        //    //lblReplyJSON.Text = info.ToJSON();
        //}
        void btnCellbieReceive_Click(object sender, EventArgs e)
        {
            //Cellbie_Rest_Client cellbie = new Cellbie_Rest_Client(User.Identity.Name);
            // curl -v -i -X POST -F "token=zi9zk2rvv6ag5qj64mk6" -F "IMEI=358040080423290" "testmode=true" https://bridgetest.wbapp.ca/apiv1/receivedevice
            // 2957	358040080423290     LG V30
            // 2958	355458060568814     Google Nexus 6
            // 2959	356160070900412     Samsung Galaxy S6 Edge
            // 2960	990004600575546     BB10


            Cellbie cellbie = new Cellbie(User.Identity.Name);
            string reason = txtCellbieComment.Text;
            bool isOK = true;
            if (rdlReceivedOK.SelectedItem.Value != "1") { isOK = false; }
            string imei = txtCellbieIMEI.Text;
            string Message = "";
            decimal rID = -1;
            BrainDeviceTransaction info = cellbie.SendReceiveTransaction(rID, imei, isOK, reason, SysUtil.CellbieAPISimulate());
            lblTransactionInfo.Text = info.ToJSON();
            lblCellbieParameterJSON.Text = info.CellbieParameterJSON;
            lblCellbieIMEIOutput.Text = info.CellbieDataJSON;
            lblCellbieError.Text = info.CellbieStatus.ExceptionMessage;
            lblCellbieErrorInternal.Text = info.CellbieStatus.InnerExceptionMessage;
            lblCellbieIMEIMessage.Text = Message;
            //lblReplyJSON.Text = info.ToJSON();
        }



        string ConvertToBase64(string Path)
        {
            if (Path.Length == 0)
            {
                Path = @"C:\Users\JimHome\Documents\Visual Studio 2010\Projects\Shane_BW\BW_WebApp\Styles\Sounds\erro.wav";
            }
            Byte[] bytes = File.ReadAllBytes(Path);
            String file = Convert.ToBase64String(bytes);
            return file;
        }
        string ConvertFromBase64(string Path, string b64Str)
        {
            if (Path.Length == 0)
            {
                Path = @"C:\Users\JimHome\Documents\Visual Studio 2010\Projects\Shane_BW\BW_WebApp\Styles\Sounds\erro.wav";
            }
            Byte[] bytes = Convert.FromBase64String(b64Str);
            File.WriteAllBytes(Path, bytes);
            return Path;
        }

        void btnCellbieIMEI_Click(object sender, EventArgs e)
        {
            //Cellbie_Rest_Client cellbie = new Cellbie_Rest_Client(User.Identity.Name);
            // 2957	358040080423290     LG V30
            // 2958	355458060568814     Google Nexus 6
            // 2959	356160070900412     Samsung Galaxy S6 Edge
            // 2960	990004600575546     BB10

            Cellbie cellbie = new Cellbie(User.Identity.Name);
            string imei = txtCellbieIMEI.Text;
            string Message = "";
            decimal rID = -1;

            BrainDeviceTransaction info = cellbie.SendReceiveTransaction(rID, imei, true, "reason", SysUtil.CellbieAPISimulate());
            lblCellbieParameterJSON.Text = info.CellbieParameterJSON;
            lblCellbieIMEIOutput.Text = info.CellbieDataJSON;
            lblCellbieError.Text = info.CellbieStatus.ExceptionMessage;
            lblCellbieErrorInternal.Text = info.CellbieStatus.InnerExceptionMessage;
            lblCellbieIMEIMessage.Text = Message;
            //lblReplyJSON.Text = info.ToJSON();
        }
        void btnCellbieIMEI_Clicky(object sender, EventArgs e)
        {
            //Cellbie_Rest_Client cellbie = new Cellbie_Rest_Client(User.Identity.Name);



            // 2957	358040080423290     LG V30
            // 2958	355458060568814     Google Nexus 6
            // 2959	356160070900412     Samsung Galaxy S6 Edge
            // 2960	990004600575546     BB10


            Cellbie_Rest_Client cellbie = new Cellbie_Rest_Client(User.Identity.Name);


            string imei = txtCellbieIMEI.Text;
            string Message = "";

            decimal rID = 2959;

            BrainDeviceTransaction info = cellbie.Getinventoryinfo(rID, imei);
            lblCellbieParameterJSON.Text = info.CellbieParameterJSON;
            lblCellbieIMEIOutput.Text = info.CellbieDataJSON;
            lblCellbieError.Text = info.CellbieStatus.ExceptionMessage;
            lblCellbieErrorInternal.Text = info.CellbieStatus.InnerExceptionMessage;
            lblCellbieIMEIMessage.Text = Message;
            //lblReplyJSON.Text = info.ToJSON();
        }

        void CellbieTabContainer_ActiveTabChanged(object sender, EventArgs e)
        {
            UpdateGrid();
        }

        void grdTempDetail_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            LinkButton btnOpen = (LinkButton)e.CommandSource;
            string ESN = "";
            string id = "-1";
            string pid = "-1";
            string processName = "";
            string CommandArgument = btnOpen.CommandArgument;
            string[] data = CommandArgument.Split(',');

            if (btnOpen.ID.ToUpper() == "IMGANALYZE")
            {
                id = data[0];
                ScriptManager.RegisterStartupScript(this, GetType(), "Open Unit", "OpenUnitAnalysisRPT(" + id + ");", true);
            }

            else if (btnOpen.ID.ToUpper() == "IMGBAGTAG")
            {
                id = data[0];
                ScriptManager.RegisterStartupScript(this, GetType(), "Open BagTag", "OpenbagTag(" + id + ");", true);
            }
            else if (btnOpen.ID.ToUpper() == "IMGSENDCELLBIE")
            {
                id = data[0];
                decimal rID = -1;
                if (decimal.TryParse(id, out rID) == false) {rID = -1;}
                string Message = "";
                Cellbie cellbie = new Cellbie(User.Identity.Name);
                cellbie.SendReceiveTransaction(rID, "", true, "reason", SysUtil.CellbieAPISimulate());
                //ReceiveDetailManager rdm = new ReceiveDetailManager(User.Identity.Name);
                //rdm.Cellbie_Send(rID, "", "", "");
                //rdm.Cellbie_Sent(rID, "", "", "");
                //rdm.Cellbie_Success(rID, "", "", "");
                ////rdm.Cellbie_Send(rID, "", "", "");
                UpdateGrid();
            }
            else if (btnOpen.ID.ToUpper() == "IMGMOVETOSEND")
            {
                id = data[0];
                decimal rID = -1;
                if (decimal.TryParse(id, out rID) == false) { rID = -1; }
                ReceiveDetailManager rdm = new ReceiveDetailManager(User.Identity.Name);
                rdm.Cellbie_Send(rID,"Yes", "", "");
                UpdateGrid();
            }
            else if (btnOpen.ID.ToUpper() == "IMGMOVETOSENT")
            {
                id = data[0];
                decimal rID = -1;
                if (decimal.TryParse(id, out rID) == false) { rID = -1; }
                ReceiveDetailManager rdm = new ReceiveDetailManager(User.Identity.Name);
                rdm.Cellbie_Sent(rID, "..", "..", "..");
                UpdateGrid();
            }
            else if (btnOpen.ID.ToUpper() == "IMGMOVETOERROR")
            {
                id = data[0];
                decimal rID = -1;
                if (decimal.TryParse(id, out rID) == false) { rID = -1; }
                ReceiveDetailManager rdm = new ReceiveDetailManager(User.Identity.Name);
                rdm.Cellbie_Error(rID, "..", "..", "Bad Error Comment Here");
                UpdateGrid();
            }
            else if (btnOpen.ID.ToUpper() == "IMGMOVETOSUCCESS")
            {
                id = data[0];
                decimal rID = -1;
                if (decimal.TryParse(id, out rID) == false) { rID = -1; }
                ReceiveDetailManager rdm = new ReceiveDetailManager(User.Identity.Name);
                rdm.Cellbie_Success(rID, "..", "..", "");
                UpdateGrid();
            }
            else if (btnOpen.ID.ToUpper() == "IMGMOVEARCHIVE")
            {
                id = data[0];
                decimal rID = -1;
                if (decimal.TryParse(id, out rID) == false) { rID = -1; }
                ReceiveDetailManager rdm = new ReceiveDetailManager(User.Identity.Name);
                rdm.Cellbie_Archive(rID, "..", "..", "..");
                UpdateGrid();
            }


            //else if (btnOpen.ID.ToUpper() == "IMAGEKITTING")
            //{

            //    //'KITTING' || 'SHIPPING GMP SALES'


            //    id = data[0];
            //    ScriptManager.RegisterStartupScript(this, GetType(), "Open Kitting", "OpenKitting(" + id + ");", true);
            //}
            //else if (btnOpen.ID.ToUpper() == "IMGCORRECT")
            //{

            //    //id = data[0];
            //    //decimal ID = -1;
            //    //decimal.TryParse(id, out ID);
            //    ESN = data[1];
            //    if (ESN.Length > 0)
            //    {
            //        ReceiveDetailManager dm = new ReceiveDetailManager(User.Identity.Name);
            //        dm.Utility_Maintenance(ESN);
            //        ScriptManager.RegisterStartupScript(this, GetType(), "Data Reset", "alert('Cleanup done');", true);
            //    }
            //    else
            //    {
            //        ScriptManager.RegisterStartupScript(this, GetType(), "Data Reset", "alert('Cleanup done');", true);
            //    }
            //}
            //else if (btnOpen.ID.ToUpper() == "IMAGERESETBIN")
            //{
            //    id = data[0];
            //    decimal ID = -1;
            //    if (decimal.TryParse(id, out ID) == true)
            //    {
            //        ReceiveDetailManager rdm = new ReceiveDetailManager(User.Identity.Name);
            //        rdm.UpdateESNAttribute_Blank(ID, "Bin");
            //        ScriptManager.RegisterStartupScript(this, GetType(), "Reset Bin", "alert('Bin Reset');", true);
            //    }
            //}
            //else if (btnOpen.ID.ToUpper() == "IMGPARTDETAILS")
            //{
            //    id = data[0];
            //    ESN = data[1];
            //    decimal ID = -1;
            //    if (decimal.TryParse(id, out ID) == true)
            //    {
            //        //ReceiveDetailManager rdm = new ReceiveDetailManager(User.Identity.Name);
            //        //rdm.UpdateESNAttribute_Blank(ID, "Bin");
            //        ScriptManager.RegisterStartupScript(this, GetType(), "Open Unit", "OpenUnitPartScreen(" + id + ",'" + ESN + "');", true);
            //        //ScriptManager.RegisterStartupScript(this, GetType(), "Reset Bin", "alert('Edit Part Detail');", true);
            //    }
            //}



            else
            {
                id = data[0];
                pid = data[1];
                processName = data[2];
                ScriptManager.RegisterStartupScript(this, GetType(), "Open Unit", "OpenUnit(" + id + "," + pid + ",'" + processName + "');", true);
            }
        }
        void grdTempDetail_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            string GridTab = CellbieTabContainer.ActiveTab.HeaderText.ToUpper();
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                decimal id = -1;
                Type type = e.Row.DataItem.GetType();               //.GetGenericArguments()[0]; 
                vwReceiveDetailCellbie_Grid Data = ((vwReceiveDetailCellbie_Grid)e.Row.DataItem);
                LinkButton bPrint = (LinkButton)e.Row.FindControl("imgOpen");
                #region Front End Buttons


                bPrint = (LinkButton)e.Row.FindControl("imgSendCellbie");
                if (bPrint != null)
                {
                    bPrint.Visible = false;
                    if (GridTab == "ERROR") { bPrint.Visible = true; }
                    bPrint.CommandArgument = Data.ReceiveDetailID.ToString() + "," + Data.ProjectID.ToString() + "," + "";
                }
                bPrint = (LinkButton)e.Row.FindControl("imgMoveToSend");
                if (bPrint != null)
                {
                    bPrint.Visible = false;
                   // if (GridTab == "ERROR" || GridTab == "SUCCESS") { bPrint.Visible = true; }
                    bPrint.CommandArgument = Data.ReceiveDetailID.ToString() + "," + Data.ProjectID.ToString() + "," + "";
                }
                bPrint = (LinkButton)e.Row.FindControl("imgMoveToSent");
                if (bPrint != null)
                {
                    bPrint.Visible = false;
                   // if (GridTab == "SEND") { bPrint.Visible = true; }
                    bPrint.CommandArgument = Data.ReceiveDetailID.ToString() + "," + Data.ProjectID.ToString() + "," + "";
                }
                bPrint = (LinkButton)e.Row.FindControl("imgMoveToError");
                if (bPrint != null)
                {
                    bPrint.Visible = false;
                    //if (GridTab == "ERROR" || GridTab == "SUCCESS") { bPrint.Visible = true; }
                    bPrint.CommandArgument = Data.ReceiveDetailID.ToString() + "," + Data.ProjectID.ToString() + "," + "";
                }
                bPrint = (LinkButton)e.Row.FindControl("imgMoveToSuccess");
                if (bPrint != null)
                {
                    bPrint.Visible = false;
                    if (GridTab == "ERROR" || GridTab == "ARCHIVE") { bPrint.Visible = true; }
                    bPrint.CommandArgument = Data.ReceiveDetailID.ToString() + "," + Data.ProjectID.ToString() + "," + "";
                }
                bPrint = (LinkButton)e.Row.FindControl("imgMoveArchive");
                if (bPrint != null)
                {
                    bPrint.Visible = false;
                    if (GridTab == "ERROR" || GridTab == "SUCCESS") { bPrint.Visible = true; }
                    bPrint.CommandArgument = Data.ReceiveDetailID.ToString() + "," + Data.ProjectID.ToString() + "," + "";
                }


                #region General
                bPrint = (LinkButton)e.Row.FindControl("imgOpen");
                if (bPrint != null)
                {
                    bPrint.Visible = false;
                    //bPrint.Visible = true;
                    //if ((Data.CurrentProcessName.Length >= 7 && Data.CurrentProcessName.Substring(0, 7).ToUpper() == "RECEIVE") // may have to include here "isIFSLocked. If Yes, then can open.
                    //    || (Data.CurrentProcessName.Length >= 8 && Data.CurrentProcessName.Substring(0, 8).ToUpper() == "SHIPPING"))
                    //{
                    //    bPrint.Visible = false;
                    //}

                    bPrint.CommandArgument = Data.ReceiveDetailID.ToString() + "," + Data.ProjectID.ToString() + "," + "";
                }
                bPrint = (LinkButton)e.Row.FindControl("imgOpenProcess");
                if (bPrint != null)
                {
                    bPrint.Visible = false;
                    //bPrint.Visible = true;
                    //if ((Data.CurrentProcessName.Length >= 7 && Data.CurrentProcessName.Substring(0, 7).ToUpper() == "RECEIVE")
                    //    || (Data.CurrentProcessName.Length >= 8 && Data.CurrentProcessName.Substring(0, 8).ToUpper() == "SHIPPING"))
                    //{
                    //    bPrint.Visible = false;
                    //}
                    //bPrint.CommandArgument = Data.ReceiveDetailID.ToString() + "," + Data.ProjectID.ToString() + "," + Data.CurrentProcessName;
                }
                bPrint = (LinkButton)e.Row.FindControl("imgBagTag");
                if (bPrint != null)
                {
                    bPrint.Visible = false;
                    //if (GridTab == "ERROR" || GridTab == "SUCCESS") { bPrint.Visible = true; }
                    bPrint.CommandArgument = Data.ReceiveDetailID.ToString() + "," + Data.ProjectID.ToString() + "," + "";
                }
                bPrint = (LinkButton)e.Row.FindControl("imgAnalyze");
                if (bPrint != null)
                {
                    bPrint.Visible = false;
                    //if (GridTab == "ERROR" || GridTab == "SUCCESS") { bPrint.Visible = true; }
                    bPrint.CommandArgument = Data.ReceiveDetailID.ToString() + "," + Data.ProjectID.ToString() + "," + "";
                }



                bPrint = (LinkButton)e.Row.FindControl("imgLastUser");
                if (bPrint != null)
                {
                    bPrint.ToolTip = "Last Update User: " + Data.LastUpdateUser + " (" + Data.LastUpdateDate.ToString() + ")";
                    bPrint.CommandArgument = Data.ReceiveDetailID.ToString() + "," + Data.ProjectID.ToString() + ",LastUserData";
                }
                #endregion
                #region No Longer Valid
                bPrint = (LinkButton)e.Row.FindControl("imgOpenQC");
                if (bPrint != null)
                {
                    bPrint.Visible = false;
                    bPrint.CommandArgument = Data.ReceiveDetailID.ToString() + "," + Data.ProjectID.ToString() + ",QC Assessment";
                }
                #endregion
                #endregion
                #region Tail End Buttons
                bPrint = (LinkButton)e.Row.FindControl("imgAnalyze");
                if (bPrint != null)
                {
                    //bPrint.Visible = false;
                    bPrint.CommandArgument = Data.ReceiveDetailID.ToString();
                }

                bPrint = (LinkButton)e.Row.FindControl("imgBagTag");
                if (bPrint != null)
                {
                    //bPrint.Visible = false;
                    bPrint.CommandArgument = Data.ReceiveDetailID.ToString();
                }
                #region No Longer Used
                //bPrint = (LinkButton)e.Row.FindControl("ImageKitting");
                //if (bPrint != null)
                //{
                //    ProcessManager pm = new ProcessManager(User.Identity.Name);
                //    decimal.TryParse(Data.ReceiveDetailID.ToString(), out id);
                //    if (pm.HasUnitEnteredAtleasetOneofTheseProcesses(id, "KITTING,SHIPPING GMP SALES") == false) { bPrint.Visible = false; }
                //    else { bPrint.CommandArgument = Data.ReceiveDetailID.ToString(); }
                //}
                //bPrint = (LinkButton)e.Row.FindControl("ImageResetBin");
                //if (bPrint != null)
                //{
                //    bPrint.CommandArgument = Data.ReceiveDetailID.ToString();
                //}
                //bPrint = (LinkButton)e.Row.FindControl("imgCorrect");
                //if (bPrint != null)
                //{
                //    bPrint.CommandArgument = Data.ReceiveDetailID.ToString() + "," + Data.ESN;
                //}
                #endregion
                #endregion
            }
        }
        private void UpdateGrid()
        {
            grdTempDetail.Visible = true;
            //CellbieDownloadGrid.Visible = true;
            string Status = CellbieTabContainer.ActiveTab.HeaderText;
            if (Status == "Test Send Cellbie")
            {
                CellbieDownloadGrid.Visible = false;
                grdTempDetail.Visible = false;
            }
            else
            {
                ReceiveDetailManager rdm = new ReceiveDetailManager(User.Identity.Name);
                List<vwReceiveDetailCellbie_Grid> Data = rdm.GetMasterDetailCellbieList(Status);
                grdTempDetail.DataSource = Data;
                grdTempDetail.DataBind();
            }
        }

        //void btnSummary_Click(object sender, EventArgs e)
        //{
        //    Button btnOpen = (Button)sender;
        //    #region Download
        //    ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "PrintReportSummary('" + btnOpen.CommandArgument + "');", true);
        //    #endregion
        //}
        //void imgDownloadGrid_Click(object sender, ImageClickEventArgs e)
        //{
        //    string Tab = "Send";
        //    if (CellbieTabContainer.ActiveTab.HeaderText == "Send") { Tab = "Send"; }
        //    if (CellbieTabContainer.ActiveTab.HeaderText == "Sent") { Tab = "Sent"; }
        //    if (CellbieTabContainer.ActiveTab.HeaderText == "Error") { Tab = "Error"; }
        //    if (CellbieTabContainer.ActiveTab.HeaderText == "Success") { Tab = "Success"; }
        //    //if (CellbieTabContainer.ActiveTab.HeaderText == "Sent") { Tab = "Sent"; }
        //    if (true)            //(chkReceived.Checked == false)
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "PrintTabReport('" + Tab + "');", true);
        //    }
        //    //else
        //    //{
        //    //    string BeginD = string.Format("{0:MM/dd/yyyy HH:mm:00}", BeginDate.Value);
        //    //    string EndD = string.Format("{0:MM/dd/yyyy HH:mm:00}", EndDate.Value); ;
        //    //    ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "PrintTabTimeReport('" + Tab + "', '" + BeginD + "','" + EndD + "');", true);
        //    //}
        //}
        ////
        //void btnRefreshSent_Click(object sender, EventArgs e)
        //{
        //    UpdateSentGrid();
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
        //            DeviceInventoryManager im = new DeviceInventoryManager(User.Identity.Name);
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
        //            DeviceInventoryManager im = new DeviceInventoryManager(User.Identity.Name);
        //            lblMessageLock.Text = im.LogPhysicalInventoryBatchOpen(btnOpen.CommandArgument);
        //            UpdateLockedGrid();
        //        }
        //        #endregion
        //        #region Clean
        //        if (btnOpen.ID.ToUpper() == "IMGCLEAN")
        //        {
        //            DeviceInventoryManager im = new DeviceInventoryManager(User.Identity.Name);
        //            lblMessageLock.Text = im.LogPhysicalInventoryBatchClean(btnOpen.CommandArgument);
        //            UpdateLockedGrid();
        //        }

        //        #endregion
        //        #region Hold
        //        if (btnOpen.ID.ToUpper() == "IMGHOLD")
        //        {
        //            DeviceInventoryManager im = new DeviceInventoryManager(User.Identity.Name);
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
        //            DeviceInventoryManager im = new DeviceInventoryManager(User.Identity.Name);
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
        //            DeviceInventoryManager im = new DeviceInventoryManager(User.Identity.Name);
        //            lblMessageOpen.Text = im.LogPhysicalInventoryBatchLocked(btnOpen.CommandArgument);
        //            UpdateOpenGrid();
        //        }
        //        #endregion
        //        #region Invalidate
        //        if (btnOpen.ID.ToUpper() == "IMGINVALIDATE")
        //        {
        //            DeviceInventoryManager im = new DeviceInventoryManager(User.Identity.Name);
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
        //            DeviceInventoryManager im = new DeviceInventoryManager(User.Identity.Name);
        //            lblMessageLock.Text = im.LogPhysicalInventoryBatchLocked(btnOpen.CommandArgument);
        //            UpdateHoldGrid();
        //        }
        //        #endregion
        //        #region Transfer
        //        if (btnOpen.ID.ToUpper() == "IMGTRANSFER")
        //        {
        //            DeviceInventoryManager im = new DeviceInventoryManager(User.Identity.Name);
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
        //void GRDSentBatches_RowCommand(object sender, GridViewCommandEventArgs e)
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
        //void GRDSentBatches_RowDataBound(object sender, GridViewRowEventArgs e)
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
        //    DeviceInventoryManager im = new DeviceInventoryManager(User.Identity.Name);
        //    GRDLockedBatches.DataSource = im.GetPhysicalCountData("Locked");
        //    GRDLockedBatches.DataBind();
        //}
        //private void UpdateHoldGrid()
        //{
        //    DeviceInventoryManager im = new DeviceInventoryManager(User.Identity.Name);
        //    GRDHoldBatches.DataSource = im.GetPhysicalCountData("Hold");
        //    GRDHoldBatches.DataBind();
        //}
        //private void UpdateInvalidGrid()
        //{
        //    DeviceInventoryManager im = new DeviceInventoryManager(User.Identity.Name);
        //    GRDInvalidBatches.DataSource = im.GetPhysicalCountData("Invalid");
        //    GRDInvalidBatches.DataBind();
        //}
        //private void UpdateOpenGrid()
        //{
        //    DeviceInventoryManager im = new DeviceInventoryManager(User.Identity.Name);
        //    GRDOpenBatches.DataSource = im.GetPhysicalCountData("Open");
        //    GRDOpenBatches.DataBind();
        //}
        //private void UpdateSentGrid()
        //{
        //    DeviceInventoryManager im = new DeviceInventoryManager(User.Identity.Name);
        //    GRDSentBatches.DataSource = im.GetPhysicalCountData("Sent");
        //    GRDSentBatches.DataBind();
        //}
    }
}