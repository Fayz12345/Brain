using System;
using System.Linq;
using BW_WebApp.DataManagers;

namespace BW_WebApp
{
    public partial class ReceiveWaybill : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnClear.Click += new EventHandler(btnClear_Click);
            hdnUserName.Value = User.Identity.Name;
            if (!IsPostBack)
            {
                ClientManager cm = new ClientManager(User.Identity.Name);

                drpMasterClient.DataValueField = "ClientID";
                drpMasterClient.DataTextField = "CompanyName";
                drpMasterClient.DataSource = cm.DropDownUserFilterMasterClientList("", "", "", "").OrderBy(x => x.CompanyName);
                drpMasterClient.DataBind();
                drpMasterClient.SelectedIndex = 0;
            }
            txtWaybill.Focus();
            txtWaybill.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13) || (event.which == 9) || (event.keyCode == 9)) {;SetESNFocus();return false;}} else {return true}; ");
            txtWaybill.Attributes.Add("onblur", "IsMessage();return false;");

            txtESN.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13) || (event.which == 9) || (event.keyCode == 9)) {SetWayBillFocus();return false;}} else {return true}; ");
            txtESN.Attributes.Add("onblur", "RecordScanKey();return false;");
        }

        void btnClear_Click(object sender, EventArgs e)
        {
            txtWaybill.Text = "";
            lastWayBill.Value = "";

            txtESN.Text = "";
            txtCount.Text = "0";
            lblWarningMessage.Text = "";
            txtWaybill.Focus();
            //txtWaybill.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13) || (event.which == 9) || (event.keyCode == 9)) {CleanData();SetESNFocus();return false;}} else {return true}; ");
            //txtWaybill.Attributes.Add("onblur", "IsMessage();return false;");

            //txtESN.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13) || (event.which == 9) || (event.keyCode == 9)) {SetWayBillFocus();return false;}} else {return true}; ");
            //txtESN.Attributes.Add("onblur", "RecordScanKey();return false;");

        }
    }
}