using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using BW_WebApp.DataManagers;

namespace BW_WebApp
{
    public partial class PI_ControlParts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GRDLockedBatches.RowDataBound += new GridViewRowEventHandler(GRDLockedBatches_RowDataBound);
            GRDLockedBatches.RowCommand += new GridViewCommandEventHandler(GRDLockedBatches_RowCommand);

            imgDownloadGrid.Click += new ImageClickEventHandler(imgDownloadGrid_Click);
            GRDInvalidBatches.RowDataBound += new GridViewRowEventHandler(GRDInvalidBatches_RowDataBound);
            GRDInvalidBatches.RowCommand += new GridViewCommandEventHandler(GRDInvalidBatches_RowCommand);

            GRDOpenBatches.RowDataBound += new GridViewRowEventHandler(GRDOpenBatches_RowDataBound);
            GRDOpenBatches.RowCommand += new GridViewCommandEventHandler(GRDOpenBatches_RowCommand);

            GRDSentIFSBatches.RowDataBound += new GridViewRowEventHandler(GRDSentIFSBatches_RowDataBound);
            GRDSentIFSBatches.RowCommand += new GridViewCommandEventHandler(GRDSentIFSBatches_RowCommand);

            btnRefreshLocked.Click += new EventHandler(btnRefreshLocked_Click);
            btnRefreshInvalid.Click += new EventHandler(btnRefreshInvalid_Click);
            btnRefreshOpen.Click += new EventHandler(btnRefreshOpen_Click);
            btnRefreshSentIFS.Click += new EventHandler(btnRefreshSentIFS_Click);
            if (IsPostBack == false)
            {
                hdnUserName.Value = User.Identity.Name;
                UpdateLockedGrid();
            }

        }


        void imgDownloadGrid_Click(object sender, ImageClickEventArgs e)
        {
            string Tab = "Hold";
            if (TabContainerListName.ActiveTab.HeaderText == "Locked Batches") { Tab = "Locked"; }
            if (TabContainerListName.ActiveTab.HeaderText == "Invalid Batches") { Tab = "Invalid"; }
            if (TabContainerListName.ActiveTab.HeaderText == "Open Batches") { Tab = "Open"; }
            if (TabContainerListName.ActiveTab.HeaderText == "Sent IFS") { Tab = "SentIFS"; }
            ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "PrintTabReport('" + Tab + "');", true);
        }


        void btnRefreshSentIFS_Click(object sender, EventArgs e)
        {
            UpdateSentIFSGrid();
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
                    lblMessageLock.Text = im.LogPhysicalInventoryBatchInvalidPart(btnOpen.CommandArgument);
                    UpdateLockedGrid();
                }
                #endregion
                #region Download
                if (btnOpen.ID.ToUpper() == "IMGDOWNLOAD")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "PrintReport('" + btnOpen.CommandArgument + "');", true);
                }
                #endregion
                #region Transfer
                if (btnOpen.ID.ToUpper() == "IMGTRANSFER")
                {
                    DeviceInventoryManager im = new DeviceInventoryManager(User.Identity.Name);
                    lblMessageLock.Text = im.LogPhysicalInventoryBatchToIFSPart(btnOpen.CommandArgument);
                }
                UpdateLockedGrid();
                #endregion
            }
        }
        void GRDLockedBatches_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                vwGridPhysicalPartInventoryCount data = (vwGridPhysicalPartInventoryCount)e.Row.DataItem;
                ImageButton bInvalidate = (ImageButton)e.Row.FindControl("imgInvalidate");
                ImageButton bDownload = (ImageButton)e.Row.FindControl("imgDownload");
                ImageButton bTransferIFS = (ImageButton)e.Row.FindControl("imgTransfer");

                if (bInvalidate != null) { bInvalidate.CommandArgument = "-1"; }
                if (bDownload != null) { bDownload.CommandArgument = "-1"; }
                if (bTransferIFS != null) { bTransferIFS.CommandArgument = "-1"; }
                if (data != null)
                {
                    if (bInvalidate != null) { bInvalidate.CommandArgument = data.Batch; }
                    if (bDownload != null) { bDownload.CommandArgument = data.Batch; }
                    if (bTransferIFS != null) { bTransferIFS.CommandArgument = data.Batch; }

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
                    lblMessageInvalid.Text = im.LogPhysicalInventoryBatchOpenPart(btnOpen.CommandArgument);
                    UpdateLockedGrid();
                }
                #endregion
                #region Download
                if (btnOpen.ID.ToUpper() == "IMGDOWNLOAD")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "PrintReport('" + btnOpen.CommandArgument + "');", true);
                }
                #endregion
                UpdateInvalidGrid();
            }
        }
        void GRDInvalidBatches_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vwGridPhysicalPartInventoryCount data = (vwGridPhysicalPartInventoryCount)e.Row.DataItem;
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
                    lblMessageOpen.Text = im.LogPhysicalInventoryBatchLockedPart(btnOpen.CommandArgument);
                    UpdateLockedGrid();
                }
                #endregion
                #region Invalidate
                if (btnOpen.ID.ToUpper() == "IMGINVALIDATE")
                {
                    DeviceInventoryManager im = new DeviceInventoryManager(User.Identity.Name);
                    lblMessageOpen.Text = im.LogPhysicalInventoryBatchInvalidPart(btnOpen.CommandArgument);
                    UpdateLockedGrid();
                }
                #endregion
                #region Download
                if (btnOpen.ID.ToUpper() == "IMGDOWNLOAD")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "PrintReport('" + btnOpen.CommandArgument + "');", true);
                }
                #endregion
                UpdateOpenGrid();
            }
        }
        void GRDOpenBatches_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vwGridPhysicalPartInventoryCount data = (vwGridPhysicalPartInventoryCount)e.Row.DataItem;
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

        void GRDSentIFSBatches_RowCommand(object sender, GridViewCommandEventArgs e)
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
        void GRDSentIFSBatches_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vwGridPhysicalPartInventoryCount data = (vwGridPhysicalPartInventoryCount)e.Row.DataItem;
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
            GRDLockedBatches.DataSource = im.GetPhysicalCountDataPart("Locked");
            GRDLockedBatches.DataBind();
        }
        private void UpdateInvalidGrid()
        {
            DeviceInventoryManager im = new DeviceInventoryManager(User.Identity.Name);
            GRDInvalidBatches.DataSource = im.GetPhysicalCountDataPart("Invalid");
            GRDInvalidBatches.DataBind();
        }
        private void UpdateOpenGrid()
        {
            DeviceInventoryManager im = new DeviceInventoryManager(User.Identity.Name);
            GRDOpenBatches.DataSource = im.GetPhysicalCountDataPart("Open");
            GRDOpenBatches.DataBind();
        }
        private void UpdateSentIFSGrid()
        {
            DeviceInventoryManager im = new DeviceInventoryManager(User.Identity.Name);
            GRDSentIFSBatches.DataSource = im.GetPhysicalCountDataPart("SentIFS");
            GRDSentIFSBatches.DataBind();
        }

    }
}