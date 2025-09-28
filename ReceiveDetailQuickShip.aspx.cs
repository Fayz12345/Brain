using System;
using BW_WebApp.DataManagers;

namespace BW_WebApp
{
    public partial class ReceiveDetailQuickShip : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnClear.Click += new EventHandler(btnClear_Click);
            //btnSave.Click += new EventHandler(btnSave_Click);
            hdnUserName.Value = User.Identity.Name;


            if (!IsPostBack)
            {
                ReceiveDetailManager rd = new ReceiveDetailManager(User.Identity.Name);
                drpCourier.DataValueField = "Desc";
                drpCourier.DataTextField = "Desc";
                drpCourier.DataSource = rd.GetMasterCourier_OutList();
                drpCourier.DataBind();
            }

            txtWaybill.Focus();
            txtWaybill.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13) || (event.which == 9) || (event.keyCode == 9)) {;SetESNFocus();return false;}} else {return true}; ");
            //txtWaybill.Attributes.Add("onblur", "IsMessage();return false;");

            txtESN.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13) || (event.which == 9) || (event.keyCode == 9)) {SetWayBillFocus();return false;}} else {return true}; ");
            txtESN.Attributes.Add("onblur", "RecordScanKey();return false;");

        }

        //void btnSave_Click(object sender, EventArgs e)
        //{

        //    List<string> ESNS = ESNList.Value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        //    lstHistory.Items.Clear();
        //    foreach (string esn in ESNS)
        //    {
        //        ListItem x = new ListItem(esn + " SAVE...Not yet Implemented!");
        //        lstHistory.Items.Add(x);
        //    }

        //    ListItemCollection ll = lstHistory.Items;
        //    txtESN.Text = "";
        //    txtCount.Text = "0";
        //    lblWarningMessage.Text = "Save Not yet completed";
        //    ESNList.Value = "";

        //    //throw new NotImplementedException();
        //}




        //void btnUpdateLocation_Click(object sender, EventArgs e)
        //{
        //    if (lblBinNumber.Text.Length == 0 && lblESN.Text.Length == 0) { lblMessage.Text = "No Bin number or ESN/IMEI given"; return; }
        //    ReceiveDetailManager rd = new ReceiveDetailManager(User.Identity.Name);
        //    decimal LocationID = -1;
        //    if (decimal.TryParse(drpLocationList.SelectedItem.Value, out LocationID) == false) { LocationID = -1; }

        //    if (lblESN.Text.Length > 0) { lblMessage.Text = rd.UpdateESN_LocationValue_ByID(lblESN.Text, LocationID, drpLocationList.SelectedItem.Text); }
        //    else { lblMessage.Text = rd.UpdateBin_LocationValue_ByID(lblBinNumber.Text, LocationID); }
        //}



        void btnClear_Click(object sender, EventArgs e)
        {
            ESNList.Value = "";
            txtWaybill.Text = "";
            lastWayBill.Value = "";

            txtESN.Text = "";
            txtCount.Text = "0";
            lblWarningMessage.Text = "";
            drpCourier.SelectedIndex = 0;
            lstHistory.Items.Clear();
            txtWaybill.Focus();
            //txtWaybill.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13) || (event.which == 9) || (event.keyCode == 9)) {CleanData();SetESNFocus();return false;}} else {return true}; ");
            //txtWaybill.Attributes.Add("onblur", "IsMessage();return false;");

            //txtESN.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13) || (event.which == 9) || (event.keyCode == 9)) {SetWayBillFocus();return false;}} else {return true}; ");
            //txtESN.Attributes.Add("onblur", "RecordScanKey();return false;");

        }
    }
}