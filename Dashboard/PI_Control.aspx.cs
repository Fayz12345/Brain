using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using BW_WebApp.DataManagers;

namespace BW_WebApp
{
    public partial class PI_Control : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GRDLockedBatches.RowDataBound += new GridViewRowEventHandler(GRDLockedBatches_RowDataBound);
            GRDLockedBatches.RowCommand += new GridViewCommandEventHandler(GRDLockedBatches_RowCommand);

            imgDownloadGrid.Click += new ImageClickEventHandler(imgDownloadGrid_Click);
            GRDInvalidBatches.RowDataBound += new GridViewRowEventHandler(GRDInvalidBatches_RowDataBound);
            GRDInvalidBatches.RowCommand += new GridViewCommandEventHandler(GRDInvalidBatches_RowCommand);

            GRDHoldBatches.RowCommand += new GridViewCommandEventHandler(GRDHoldBatches_RowCommand);
            GRDHoldBatches.RowDataBound += new GridViewRowEventHandler(GRDHoldBatches_RowDataBound);

            GRDOpenBatches.RowDataBound += new GridViewRowEventHandler(GRDOpenBatches_RowDataBound);
            GRDOpenBatches.RowCommand += new GridViewCommandEventHandler(GRDOpenBatches_RowCommand);

            GRDSentBatches.RowDataBound += new GridViewRowEventHandler(GRDSentBatches_RowDataBound);
            GRDSentBatches.RowCommand += new GridViewCommandEventHandler(GRDSentBatches_RowCommand);

            btnRefreshLocked.Click += new EventHandler(btnRefreshLocked_Click);
            btnRefreshHold.Click += new EventHandler(btnRefreshHold_Click);
            btnRefreshInvalid.Click += new EventHandler(btnRefreshInvalid_Click);
            btnRefreshOpen.Click += new EventHandler(btnRefreshOpen_Click);
            btnRefreshSent.Click += new EventHandler(btnRefreshSent_Click);

            btnSummaryHold.Click += new EventHandler(btnSummary_Click);
            btnSummaryLocked.Click += new EventHandler(btnSummary_Click);
            btnSummaryOpen.Click += new EventHandler(btnSummary_Click);

            if (IsPostBack == false)
            {
                hdnUserName.Value = User.Identity.Name;
                UpdateLockedGrid();

                var dateNow = DateTime.Now;

                BeginDate.Value = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day, 8, 0, 0);
                EndDate.Value = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day, DateTime.Now.TimeOfDay.Hours, DateTime.Now.Minute, 0);
            }
        }

        void btnSummary_Click(object sender, EventArgs e)
        {
            Button btnOpen = (Button)sender;
            #region Download
            ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "PrintReportSummary('" + btnOpen.CommandArgument + "');", true);
            #endregion
        }





        void imgDownloadGrid_Click(object sender, ImageClickEventArgs e)
        {
            string Tab = "Locked";
            if (TabContainerListName.ActiveTab.HeaderText == "Locked Batches") { Tab = "Locked"; }
            if (TabContainerListName.ActiveTab.HeaderText == "Invalid Batches") { Tab = "Invalid"; }
            if (TabContainerListName.ActiveTab.HeaderText == "Open Batches") { Tab = "Open"; }
            if (TabContainerListName.ActiveTab.HeaderText == "Hold") { Tab = "Hold"; }
            if (TabContainerListName.ActiveTab.HeaderText == "Sent") { Tab = "Sent"; }

            if (chkReceived.Checked == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "PrintTabReport('" + Tab + "');", true);
            }
            else
            {
                string BeginD = string.Format("{0:MM/dd/yyyy HH:mm:00}", BeginDate.Value);
                string EndD = string.Format("{0:MM/dd/yyyy HH:mm:00}", EndDate.Value); ;
                ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "PrintTabTimeReport('" + Tab + "', '" + BeginD + "','" + EndD + "');", true);
            }
        }
        //

        void btnRefreshSent_Click(object sender, EventArgs e)
        {
            UpdateSentGrid();
        }
        void btnRefreshOpen_Click(object sender, EventArgs e)
        {
            UpdateOpenGrid();
            lblMessageOpen.Text = "";
        }
        void btnRefreshInvalid_Click(object sender, EventArgs e)
        {
            UpdateInvalidGrid();
            lblMessageInvalid.Text = "";
        }
        void btnRefreshLocked_Click(object sender, EventArgs e)
        {
            UpdateLockedGrid();
            lblMessageLock.Text = "";
        }
        void btnRefreshHold_Click(object sender, EventArgs e)
        {
            UpdateHoldGrid();
            lblMessageHold.Text = "";
        }

        void GRDLockedBatches_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName != "Page")
            {
                ImageButton btnOpen = (ImageButton)e.CommandSource;
                //string CommandArgument = btnOpen.CommandArgument;
                #region Invalidate
                if (btnOpen.ID.ToUpper() == "IMGINVALIDATE")
                {
                    DeviceInventoryManager im = new DeviceInventoryManager(User.Identity.Name);
                    lblMessageLock.Text = im.LogPhysicalInventoryBatchInvalid(btnOpen.CommandArgument);
                    UpdateLockedGrid();
                }
                #endregion
                #region Download
                if (btnOpen.ID.ToUpper() == "IMGDOWNLOAD")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "PrintReport('" + btnOpen.CommandArgument + "');", true);
                }
                #endregion
                #region Open
                if (btnOpen.ID.ToUpper() == "IMGOPEN")
                {
                    DeviceInventoryManager im = new DeviceInventoryManager(User.Identity.Name);
                    lblMessageLock.Text = im.LogPhysicalInventoryBatchOpen(btnOpen.CommandArgument);
                    UpdateLockedGrid();
                }
                #endregion
                #region Clean
                if (btnOpen.ID.ToUpper() == "IMGCLEAN")
                {
                    DeviceInventoryManager im = new DeviceInventoryManager(User.Identity.Name);
                    lblMessageLock.Text = im.LogPhysicalInventoryBatchClean(btnOpen.CommandArgument);
                    UpdateLockedGrid();
                }

                #endregion
                #region Hold
                if (btnOpen.ID.ToUpper() == "IMGHOLD")
                {
                    DeviceInventoryManager im = new DeviceInventoryManager(User.Identity.Name);
                    lblMessageLock.Text = im.LogPhysicalInventoryBatchHold(btnOpen.CommandArgument);
                    UpdateLockedGrid();
                }

                #endregion
            }
        }
        void GRDLockedBatches_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                vwGridPhysicalInventoryCount_B data = (vwGridPhysicalInventoryCount_B)e.Row.DataItem;
                ImageButton bInvalidate = (ImageButton)e.Row.FindControl("imgInvalidate");
                ImageButton bDownload = (ImageButton)e.Row.FindControl("imgDownload");
                ImageButton btnOpen = (ImageButton)e.Row.FindControl("imgOpen");
                ImageButton bClean = (ImageButton)e.Row.FindControl("imgClean");
                ImageButton bHold = (ImageButton)e.Row.FindControl("imgHold");

                if (bInvalidate != null) { bInvalidate.CommandArgument = "-1"; }
                if (bDownload != null) { bDownload.CommandArgument = "-1"; }
                if (btnOpen != null) { btnOpen.CommandArgument = "-1"; }
                if (bClean != null) { bClean.CommandArgument = "-1"; }
                if (bHold != null) { bHold.CommandArgument = "-1"; }
                if (data != null)
                {
                    if (bInvalidate != null) { bInvalidate.CommandArgument = data.Batch; }
                    if (bDownload != null) { bDownload.CommandArgument = data.Batch; }
                    if (btnOpen != null) { btnOpen.CommandArgument = data.Batch; }
                    if (bClean != null) { bClean.CommandArgument = data.Batch; }
                    if (bHold != null) { bHold.CommandArgument = data.Batch; }

                }
            }
        }

        void GRDInvalidBatches_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName != "Page")
            {
                ImageButton btnOpen = (ImageButton)e.CommandSource;
                #region Open
                if (btnOpen.ID.ToUpper() == "IMGOPEN")
                {
                    DeviceInventoryManager im = new DeviceInventoryManager(User.Identity.Name);
                    lblMessageInvalid.Text = im.LogPhysicalInventoryBatchOpen(btnOpen.CommandArgument);
                    UpdateInvalidGrid();
                }
                #endregion
                #region Download
                if (btnOpen.ID.ToUpper() == "IMGDOWNLOAD")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "PrintReport('" + btnOpen.CommandArgument + "');", true);
                }
                #endregion
                //UpdateInvalidGrid();
            }
        }
        void GRDInvalidBatches_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vwGridPhysicalInventoryCount_B data = (vwGridPhysicalInventoryCount_B)e.Row.DataItem;
                ImageButton bOpen = (ImageButton)e.Row.FindControl("imgOpen");
                ImageButton bDownload = (ImageButton)e.Row.FindControl("imgDownload");
                if (bOpen != null) { bOpen.CommandArgument = "-1"; }
                if (bDownload != null) { bDownload.CommandArgument = "-1"; }
                if (data != null)
                {

                    if (bOpen != null) { bOpen.CommandArgument = data.Batch; }
                    if (bDownload != null) { bDownload.CommandArgument = data.Batch; }
                }
            }

        }

        void GRDOpenBatches_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName != "Page")
            {
                ImageButton btnOpen = (ImageButton)e.CommandSource;
                #region Lock
                if (btnOpen.ID.ToUpper() == "IMGLOCK")
                {
                    DeviceInventoryManager im = new DeviceInventoryManager(User.Identity.Name);
                    lblMessageOpen.Text = im.LogPhysicalInventoryBatchLocked(btnOpen.CommandArgument);
                    UpdateOpenGrid();
                }
                #endregion
                #region Invalidate
                if (btnOpen.ID.ToUpper() == "IMGINVALIDATE")
                {
                    DeviceInventoryManager im = new DeviceInventoryManager(User.Identity.Name);
                    lblMessageOpen.Text = im.LogPhysicalInventoryBatchInvalid(btnOpen.CommandArgument);
                    UpdateOpenGrid();
                }
                #endregion
                #region Download
                if (btnOpen.ID.ToUpper() == "IMGDOWNLOAD")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "PrintReport('" + btnOpen.CommandArgument + "');", true);
                }
                #endregion
                //UpdateOpenGrid();
            }
        }
        void GRDOpenBatches_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vwGridPhysicalInventoryCount_B data = (vwGridPhysicalInventoryCount_B)e.Row.DataItem;
                ImageButton bInvalidate = (ImageButton)e.Row.FindControl("imgInvalidate");
                ImageButton bLock = (ImageButton)e.Row.FindControl("imgLock");
                ImageButton bDownload = (ImageButton)e.Row.FindControl("imgDownload");
                if (bLock != null) { bLock.CommandArgument = "-1"; }
                if (bDownload != null) { bDownload.CommandArgument = "-1"; }
                if (bInvalidate != null) { bInvalidate.CommandArgument = "-1"; }
                if (data != null)
                {
                    if (bInvalidate != null) { bInvalidate.CommandArgument = data.Batch; }
                    if (bLock != null) { bLock.CommandArgument = data.Batch; }
                    if (bDownload != null) { bDownload.CommandArgument = data.Batch; }
                }
            }
        }

        void GRDHoldBatches_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName != "Page")
            {
                ImageButton btnOpen = (ImageButton)e.CommandSource;
                #region Lock
                if (btnOpen.ID.ToUpper() == "IMGLOCK")
                {
                    DeviceInventoryManager im = new DeviceInventoryManager(User.Identity.Name);
                    lblMessageLock.Text = im.LogPhysicalInventoryBatchLocked(btnOpen.CommandArgument);
                    UpdateHoldGrid();
                }
                #endregion
                #region Transfer
                if (btnOpen.ID.ToUpper() == "IMGTRANSFER")
                {
                    DeviceInventoryManager im = new DeviceInventoryManager(User.Identity.Name);
                    lblMessageLock.Text = im.LogPhysicalInventoryBatchToIFS(btnOpen.CommandArgument);
                    UpdateHoldGrid();
                }
                #endregion
                #region Download
                if (btnOpen.ID.ToUpper() == "IMGDOWNLOAD")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "PrintReport('" + btnOpen.CommandArgument + "');", true);
                    //UpdateOpenGrid();
                }
                #endregion
;
            }
        }
        void GRDHoldBatches_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            vwGridPhysicalInventoryCount_B data = (vwGridPhysicalInventoryCount_B)e.Row.DataItem;
            ImageButton bLock = (ImageButton)e.Row.FindControl("imgLock");
            ImageButton bDownload = (ImageButton)e.Row.FindControl("imgDownload");
            ImageButton bTransferIFS = (ImageButton)e.Row.FindControl("imgTransfer");

            //ImageButton bHold = (ImageButton)e.Row.FindControl("imgHold");

            if (bLock != null) { bLock.CommandArgument = "-1"; }
            if (bDownload != null) { bDownload.CommandArgument = "-1"; }
            if (bTransferIFS != null) { bTransferIFS.CommandArgument = "-1"; }
            //if (bClean != null) { bClean.CommandArgument = "-1"; }
            //if (bHold != null) { bHold.CommandArgument = "-1"; }
            if (data != null)
            {
                if (bLock != null) { bLock.CommandArgument = data.Batch; }
                if (bDownload != null) { bDownload.CommandArgument = data.Batch; }
                if (bTransferIFS != null) { bTransferIFS.CommandArgument = data.Batch; }
                //if (bClean != null) { bClean.CommandArgument = data.Batch; }
                //if (bHold != null) { bHold.CommandArgument = data.Batch; }

            }
        }


        void GRDSentBatches_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName != "Page")
            {
                ImageButton btnOpen = (ImageButton)e.CommandSource;
                #region Download
                if (btnOpen.ID.ToUpper() == "IMGDOWNLOAD")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "PrintReport('" + btnOpen.CommandArgument + "');", true);
                }
                #endregion
            }
        }
        void GRDSentBatches_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vwGridPhysicalInventoryCount_B data = (vwGridPhysicalInventoryCount_B)e.Row.DataItem;
                ImageButton bDownload = (ImageButton)e.Row.FindControl("imgDownload");
                if (bDownload != null) { bDownload.CommandArgument = "-1"; }
                if (data != null)
                {
                    if (bDownload != null) { bDownload.CommandArgument = data.Batch; }
                }
            }
        }

        private void UpdateLockedGrid()
        {
            DeviceInventoryManager im = new DeviceInventoryManager(User.Identity.Name);
            GRDLockedBatches.DataSource = im.GetPhysicalCountData("Locked");
            GRDLockedBatches.DataBind();
        }

        private void UpdateHoldGrid()
        {
            DeviceInventoryManager im = new DeviceInventoryManager(User.Identity.Name);
            GRDHoldBatches.DataSource = im.GetPhysicalCountData("Hold");
            GRDHoldBatches.DataBind();
        }



        private void UpdateInvalidGrid()
        {
            DeviceInventoryManager im = new DeviceInventoryManager(User.Identity.Name);
            GRDInvalidBatches.DataSource = im.GetPhysicalCountData("Invalid");
            GRDInvalidBatches.DataBind();
        }
        private void UpdateOpenGrid()
        {
            DeviceInventoryManager im = new DeviceInventoryManager(User.Identity.Name);
            GRDOpenBatches.DataSource = im.GetPhysicalCountData("Open");
            GRDOpenBatches.DataBind();
        }
        private void UpdateSentGrid()
        {
            DeviceInventoryManager im = new DeviceInventoryManager(User.Identity.Name);
            GRDSentBatches.DataSource = im.GetPhysicalCountData("Sent");
            GRDSentBatches.DataBind();
        }
    }
}