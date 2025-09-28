using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
//using Factory_DataModel;
using BW_WebApp.DataManagers;


namespace BW_WebApp.LocationManagement
{
    public partial class DeviceCycleCount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnResetCount.Click += new EventHandler(btnResetCount_Click);
            chkAutoReturn.CheckedChanged += new EventHandler(chkAutoReturn_CheckedChanged);
            //btnPICRecord.Click += new EventHandler(btnPICRecord_Click);
            btnLockBatch.Click += new EventHandler(btnLockBatch_Click);
            drpCycleCountName.SelectedIndexChanged += new EventHandler(drpCycleCountName_SelectedIndexChanged);
            drpCycleCountType.SelectedIndexChanged += new EventHandler(drpCycleCountName_SelectedIndexChanged);
            if (!IsPostBack)
            {
                hdnUserName.Value = User.Identity.Name;
                //setupDropDowns();
                //SetLastOpenBatch();
                LoadDropdowns();
            }

            ScanKey.Focus();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "KeydownLocation();", true);

            //ScanKey.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13) || (event.which == 9) || (event.keyCode == 9)) {RecordScanAuto();return false;}} else {return true}; ");
            //ScanKey.Attributes.Add("onblur", "RecordScanAuto();return false;");

            //txtESN.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13) || (event.which == 9) || (event.keyCode == 9)) {RecordScanKey();return false;}} else {return true}; ");
            //txtESN.Attributes.Add("onblur", "RecordScanKey();return false;");

            //Page.GetPostBackEventReference(btrRecord);

        }

        void LoadDropdowns()
        {

            DeviceInventoryManager im = new DeviceInventoryManager(User.Identity.Name);
            drpCycleCountName.Items.Clear();
            List<vwGetCCRunHeader> Data = im.GetActiveCycleCountNames();
            ListItem li = new ListItem("-- Select --", "-1");
            drpCycleCountName.Items.Add(li);
            foreach (vwGetCCRunHeader x in Data.OrderBy(x => x.Note))
            {
                ListItem I = new ListItem(x.Name, x.CycleInventoryCountHeaderID.ToString());
                drpCycleCountName.Items.Add(I);
            }
            if (drpCycleCountName.Items.Count > 0)
            {
                drpCycleCountName.SelectedIndex = 0;
                SetCycleParameters();
            }







            drpCycleCountType.Items.Clear();
            ListItem It = new ListItem("Level 1", "1");
            drpCycleCountType.Items.Add(It);
            drpCycleCountType.SelectedIndex = 0;
            drpCycleCountType.Enabled = false;
            if (User.IsInRole("Supervisors") == true || User.IsInRole("Admin") == true)
            {
                It = new ListItem("Level 2", "2");
                drpCycleCountType.Items.Add(It);
                It = new ListItem("Level 3", "3");
                drpCycleCountType.Items.Add(It);
                drpCycleCountType.Enabled = true;
            }
        }

        void drpCycleCountName_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetCycleParameters();
        }

        private void SetCycleParameters()
        {
            lblSite.Text = "";
            lblCarrier.Text = "";
            lblManufacturer.Text = "";
            lblModel.Text = "";
            lblColour.Text = "";
            lblCondition.Text = "";
            lblLocation.Text = "";
            DeviceInventoryManager im = new DeviceInventoryManager(User.Identity.Name);
            decimal ID = -1;
            if (decimal.TryParse(drpCycleCountName.SelectedItem.Value, out ID) == false) { ID = -1; }
            CycleInventoryCountTemplateHeader data = im.GetActiveCycleCountTemplateData(ID);
            if (data != null)
            {
                lblSite.Text = data.IFSSite;
                lblCarrier.Text = data.Carriers;
                lblManufacturer.Text = data.Manufacturers;
                lblModel.Text = data.Models;
                lblColour.Text = data.Colours;
                lblCondition.Text = data.IFSCondition;
                lblLocation.Text = data.IFSLocation;
            }

            SuggestedLocationList.Items.Clear();
            string QueryString = "";
            CycleCountManager cm = new CycleCountManager(User.Identity.Name);
            //var locations = (from x in cm.BatchGridData(ID, ref QueryString) select ((x.IsFrozen == true) ? "Frozen:" : "  Whip:") + x.IFSLocation).Distinct().OrderBy(x => x).ToList();
            var locations = (from x in cm.BatchGridData(ID, ref QueryString) select x.IFSLocation + ((x.IsFrozen == true) ? "*" : "")).Distinct().OrderBy(y => y).ToList();
            foreach (string l in locations)
            {
                ListItem I = new ListItem(l, l);
                SuggestedLocationList.Items.Add(I);
            }
            ClearPIData();
        }

        #region Physical Inventory Count
        void btnLockBatch_Click(object sender, EventArgs e)
        {
            CycleCountManager cm = new CycleCountManager(User.Identity.Name);
            lblPIMessage.Text = "";
            if (txtBatch.Text.Length == 0)
            {
                lblPIMessage.Text = "No Batch number Given.";
                return;
            }
            string rmessage = cm.LogCCBatchLocked(txtBatch.Text);
            ClearPIData();
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
            decimal ProjectID = -1;
            decimal.TryParse(DrpProject.SelectedItem.Value, out ProjectID);

            LogOnly = false; // chkUpdateIMEI.Checked;
            //decimal.TryParse(drpCondition.SelectedItem.Value, out MasterCondtionID);
            ESN = ScanKey.Text;
            Batch = txtBatch.Text;
            Site = "";       // drpSite.SelectedItem.Text;
            Project = "";           // drpProject.SelectedItem.Value;
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