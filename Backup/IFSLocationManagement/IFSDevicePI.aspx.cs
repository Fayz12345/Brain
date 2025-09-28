using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using Factory_DataModel;
using BW_WebApp.DataManagers;

namespace BW_WebApp
{
    public partial class IFSDevicePI : System.Web.UI.Page
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
                //setupDropDowns();
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
            txtIFSLocation.Text = "";
            lblPIMessage.Text = "";
            //txtBatch.Text = "";
            hdnBatchNumber.Value = "";
            //drpIFSCondition.SelectedIndex = 0;
        }



        void chkAutoReturn_CheckedChanged(object sender, EventArgs e)
        {
            btnPICRecord.Enabled = true;
            if (chkAutoReturn.Checked == true) { btnPICRecord.Enabled = false; }
        }

        void btnPICRecord_Click(object sender, EventArgs e)
        {
            decimal MasterIFSLocationID = -1;
            decimal MasterIFSCondtionID = -1;
            string ESN = "";
            string Batch = "";
            string IFSSite = "";
            string IFSProject = "";
            string SKU = "";
            string IFSLocation = "";
            string IFSCondition = "";
            string Grade = "";
            bool LogOnly;
            string UserName;

            LogOnly = false; // chkUpdateIMEI.Checked;
            //decimal.TryParse(drpIFSCondition.SelectedItem.Value, out MasterIFSCondtionID);
            ESN = ScanKey.Text;
            Batch = txtBatch.Text;
            IFSSite = "";       // drpIFSSite.SelectedItem.Text;
            IFSProject = "";           // drpIFSProject.SelectedItem.Value;
            Grade = "";             // drpGrade.SelectedItem.Text;
            SKU = "";
            IFSLocation = txtIFSLocation.Text;
            IFSCondition = "";         // drpIFSCondition.SelectedItem.Text;
            UserName = User.Identity.Name;
            LogPhysicalDeviceCount(MasterIFSLocationID, MasterIFSCondtionID, ESN, Batch, IFSSite, IFSProject, SKU, IFSLocation, IFSCondition, Grade, LogOnly, UserName);
        }

        private void LogPhysicalDeviceCount(decimal MasterIFSLocationID, decimal MasterIFSCondtionID, string ESN, string Batch, string IFSSite, string IFSProject, string SKU, string IFSLocation, string IFSCondition, string Grade, bool LogOnly, string UserName)
        {
            DeviceInventoryManager im = new DeviceInventoryManager(User.Identity.Name);
            im.LogPhysicalInventoryCount(MasterIFSLocationID, MasterIFSCondtionID, -1, ESN, Batch, IFSSite, IFSProject, SKU, IFSLocation, IFSCondition, Grade, false, false, LogOnly, UserName);
        }




        #endregion

        //void setupDropDowns()
        //{
        //    IFS_InventoryPartsManager ipm = new IFS_InventoryPartsManager(User.Identity.Name);
 

        //    QuestionManager qm = new QuestionManager(User.Identity.Name);
        //    List<Option> ol = qm.GetQuestionOptionList("IFS Conditions");
        //    drpIFSCondition.Items.Clear();
        //    foreach (Option o in ol.OrderBy(x => x.Sequence))
        //    {
        //        ListItem x = new ListItem(o.OptionText, o.OptionID.ToString());
        //        ListItem y = new ListItem(o.OptionText, o.Name);
        //        ListItem z = new ListItem(o.OptionText, o.Name);
        //        drpIFSCondition.Items.Add(x);
        //    }


        //    decimal Count = 0;
        //    List<string> s = ipm.GetSiteList();
        //    drpIFSSite.Items.Clear();
        //    foreach (string i in s)
        //    {
        //        Count++;
        //        ListItem x = new ListItem(i, Count.ToString());
        //        ListItem y = new ListItem(i, Count.ToString());
        //        if (x.Text == "C1NA") { x.Selected = true; }
        //        drpIFSSite.Items.Add(x);
        //    }


        //    Count = 0;
        //    List<PairStringValue> p = ipm.GetProjectList();
        //    drpIFSProject.Items.Clear();
        //    foreach (PairStringValue i in p)
        //    {
        //        Count++;
        //        ListItem x = new ListItem(i.Key + " - " + i.Value, i.Key);
        //        ListItem y = new ListItem(i.Key + " - " + i.Value, i.Key);
        //        if (x.Text == "BRCE") { x.Selected = true; }
        //        drpIFSProject.Items.Add(x);
        //    }



        //}

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