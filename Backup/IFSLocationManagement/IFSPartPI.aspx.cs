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
    public partial class IFSPartPI : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnUpdateLocation.Click += new EventHandler(btnUpdateLocation_Click);
            btnLockBatch.Click += new EventHandler(btnLockBatch_Click);
            btnResetCount.Click += new EventHandler(btnResetCount_Click);
            if (!IsPostBack)
            {
                hdnUserName.Value = User.Identity.Name;
                ClientManager cm = new ClientManager(User.Identity.Name);
                List<ClientLocation> cls = cm.GetClientLocationsWithOnSiteInventory();
                drpLocationList.Items.Clear();
                drpLocationList.Items.Add(new ListItem("WHS 001", "-1"));
                drpPILocationList.Items.Clear();
                drpPILocationList.Items.Add(new ListItem("WHS 001", "-1"));
                foreach (ClientLocation cl in cls)
                {
                    ListItem li = new ListItem(cl.CompanyName, cl.ClientLocationID.ToString());
                    drpLocationList.Items.Add(li);
                    ListItem li2 = new ListItem(cl.CompanyName, cl.ClientLocationID.ToString());
                    drpPILocationList.Items.Add(li2);
                }
                drpLocationList.SelectedIndex = 0;
                drpPILocationList.SelectedIndex = 0;

                SetLastOpenBatch();


            }

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), Guid.NewGuid().ToString(), "KeydownLocation();", true);


            //ScanKey.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13) || (event.which == 9) || (event.keyCode == 9)) {RecordScanAuto();return false;}} else {return true}; ");
            //ScanKey.Attributes.Add("onblur", "RecordScanAuto();return false;");






            //ClientScript.RegisterStartupScript
            //        (GetType(), Guid.NewGuid().ToString(), "KeydownLocation();", true);


            //txtIFSLocation.Attributes.Add("onkeydown", "alert('inside onkeydown event' + e.KeyData);  if (e.KeyData == Keys.Enter) { alert('inside onkeydown event'); e.SuppressKeyPress = true; SelectNextControl(ActiveControl, true, true, true, true); }");
            //txtQTYx.Attributes.Add("onkeydown", "if (e.KeyData == Keys.Enter) { e.SuppressKeyPress = true; SelectNextControl(ActiveControl, true, true, true, true); } ");

            //txtIFSLocation.Attributes.Add("onkeydown", "alert('inside onkeydown event' + e.KeyData);");
            //txtIFSLocation.Attributes.Add("onkeypress", "alert('inside keypress event' + e.KeyData);");

            //txtIFSLocation.Attributes.Add("onkeydown", "alert('inside onkeydown event' + e.KeyData);  var k = e.keyCode || e.which; if (k == 13) { SelectNextControl(ActiveControl, true, true, true, true); return false; }");
            //txtQTYx.Attributes.Add("onkeydown", "var k = e.keyCode || e.which; if (k == 13) { SelectNextControl(ActiveControl, true, true, true, true); return false; }");

        }

        void SetLastOpenBatch()
        {
            ClearPIData();
            DeviceInventoryManager im = new DeviceInventoryManager(User.Identity.Name);
            //hdnBatchNumber.Value = im.LastOpenBatchNumber;
            txtBatch.Text = im.LastOpenBatchNumberPart;
            txtPMCount.Text = im.LastOpenBatchCount;
            txtPMCountError.Text = im.LastOpenBatchErrorCount;
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
            txtQTYx.Text = "";
            lblPIMessage.Text = "";
            txtBatch.Text = "";
            ScanKey.Text = "";
            //hdnBatchNumber.Value = "";
            //drpIFSCondition.SelectedIndex = 0;
            txtIFSLocation.Focus();
        }


        void btnLockBatch_Click(object sender, EventArgs e)
        {
            lblPIMessage.Text = "";
            if (txtBatch.Text.Length == 0)
            {
                lblPIMessage.Text = "No Batch number Given.";

                return;
            }
            IFS_InventoryPartsManager im = new IFS_InventoryPartsManager(User.Identity.Name);
            lblPIMessage.Text = im.LogPhysicalInventoryBatchLocked(txtBatch.Text);
            txtBatch.Text = "";
        }

        void btnUpdateLocation_Click(object sender, EventArgs e)
        {


            if (IFSLocationFrom.Text.Length == 0) { lblMessage.Text = "No From Location Given"; return; }
            if (IFSLocationTo.Text.Length == 0) { lblMessage.Text = "No To Location Given"; return; }
            if (txtGMPPartNumber.Text.Length == 0) { lblMessage.Text = "No Part Number Given"; return; }
            if (txtQTY.Text.Length == 0) { lblMessage.Text = "No Quantity Given"; return; }

            decimal QTY = 0;
            if (decimal.TryParse(txtQTY.Text, out QTY) == false) { lblMessage.Text = "Quantity not valid"; return; }

            decimal WareHouse = -1;
            if (decimal.TryParse(drpLocationList.SelectedValue, out WareHouse) == false) { lblMessage.Text = "Invalid warehouse"; return; }

            IFS_InventoryPartsManager ifs = new IFS_InventoryPartsManager(User.Identity.Name);
            string rMessage = ifs.MovePartLocation(txtGMPPartNumber.Text, WareHouse, QTY, IFSLocationFrom.Text, IFSLocationTo.Text);
            lblMessage.Text = rMessage;
        }


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