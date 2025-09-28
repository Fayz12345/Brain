using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using Factory_DataModel;
using BW_WebApp.DataManagers;

namespace BW_WebApp.LocationManagement
{
    public partial class DevicePI : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnResetCount.Click += new EventHandler(btnResetCount_Click);
            chkAutoReturn.CheckedChanged += new EventHandler(chkAutoReturn_CheckedChanged);
            //btnPICRecord.Click += new EventHandler(btnPICRecord_Click);
            btnLockBatch.Click += new EventHandler(btnLockBatch_Click);

            if (!IsPostBack)
            {
                hdnUserName.Value = User.Identity.Name;
                setupDropDowns();
                SetLastOpenBatch();
            }

            ScanKey.Focus();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "KeydownLocation();", true);

            //ScanKey.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13) || (event.which == 9) || (event.keyCode == 9)) {RecordScanAuto();return false;}} else {return true}; ");
            //ScanKey.Attributes.Add("onblur", "RecordScanAuto();return false;");

            //txtESN.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13) || (event.which == 9) || (event.keyCode == 9)) {RecordScanKey();return false;}} else {return true}; ");
            //txtESN.Attributes.Add("onblur", "RecordScanKey();return false;");

            //Page.GetPostBackEventReference(btrRecord);

        }

        private void setupDropDowns()
        {
            ProjectManager pm = new ProjectManager(User.Identity.Name);
            drpProject.DataValueField = "ProjectID";
            drpProject.DataTextField = "Name";
            drpProject.DataSource = pm.GetMasterActiveProjectList();
            drpProject.DataBind();
            drpProject.SelectedIndex = 0;
        }

        #region Physical Inventory Count
        void btnLockBatch_Click(object sender, EventArgs e)
        {
            DeviceInventoryManager im = new DeviceInventoryManager(User.Identity.Name);
            lblPIMessage.Text = "";
            if (txtBatch.Text.Length == 0)
            {
                lblPIMessage.Text = "No Batch number Given.";
                //txtBatch.Text = im.GetNextPIBatchNumber();
                return;
            }
            string rmessage = im.LogPhysicalInventoryBatchLocked(txtBatch.Text);
            ClearPIData();
            //txtBatch.Text = im.GetNextPIBatchNumber();
            lblPIMessage.Text = rmessage;
        }

        void SetLastOpenBatch()
        {
            ClearPIData();
            DeviceInventoryManager im = new DeviceInventoryManager(User.Identity.Name);
            hdnBatchNumber.Value = im.LastOpenBatchNumber;
            txtBatch.Text = hdnBatchNumber.Value;
            txtPMCount.Text = "0"; // im.LastOpenBatchCount;
            txtPMCountError.Text = "0";  // im.LastOpenBatchErrorCount;
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
            //txtLocation.Text = "LOC-001-001-001";
            txtLocation.Text = "";
            lblPIMessage.Text = "";
            //txtBatch.Text = "";
            hdnBatchNumber.Value = "";
            //drpCondition.SelectedIndex = 0;
        }



        void chkAutoReturn_CheckedChanged(object sender, EventArgs e)
        {
            btnPICRecord.Enabled = true;
            if (chkAutoReturn.Checked == true) { btnPICRecord.Enabled = false; }
        }

        void btnPICRecord_Click(object sender, EventArgs e)
        {
            decimal MasterLocationID = -1;
            decimal MasterCondtionID = -1;
            decimal ProjectID = -1;
            string ESN = "";
            string Batch = "";
            string Site = "";
            string Project = "";
            string SKU = "";
            string Location = "";
            string Condition = "";
            string Grade = "";
            bool LogOnly;
            string UserName;
            LogOnly = false; // chkUpdateIMEI.Checked;
            //decimal.TryParse(drpCondition.SelectedItem.Value, out MasterCondtionID);
            ESN = ScanKey.Text;
            Batch = txtBatch.Text;
            Site = "";       // drpSite.SelectedItem.Text;
            Project = drpProject.SelectedItem.Value;
            decimal.TryParse(drpProject.SelectedItem.Value, out ProjectID);
            Grade = "";             // drpGrade.SelectedItem.Text;
            SKU = "";
            Location = txtLocation.Text;
            Condition = "";         // drpCondition.SelectedItem.Text;
            UserName = User.Identity.Name;
            LogPhysicalDeviceCount(MasterLocationID, MasterCondtionID, ProjectID, ESN, Batch, Site, Project, SKU, Location, Condition, Grade, LogOnly, UserName);
        }

        private void LogPhysicalDeviceCount(decimal MasterLocationID, decimal MasterCondtionID, decimal ProjectID, string ESN, string Batch, string Site, string Project, string SKU, string Location, string Condition, string Grade, bool LogOnly, string UserName)
        {
            DeviceInventoryManager im = new DeviceInventoryManager(User.Identity.Name);
            im.LogPhysicalInventoryCount(MasterLocationID, MasterCondtionID, ProjectID, ESN, Batch, Site, Project, SKU, Location, Condition, Grade, false, false, LogOnly, UserName);
        }




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


    }
}