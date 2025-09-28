using System;

namespace BW_WebApp
{
    public partial class ReceiveDetailAuthorization : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            hdnUserName.Value = User.Identity.Name;
            if (!IsPostBack)
            {
            }
            txtWaybill.Focus();
            txtWaybill.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13) || (event.which == 9) || (event.keyCode == 9)) {SetWayBillBlur();return false;}} else {return true}; ");
            txtWaybill.Attributes.Add("onblur", "RecordScanKey();return false;");

            //txtCount.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13) || (event.which == 9) || (event.keyCode == 9)) {SetWayBillFocus();return false;}} else {return true}; ");
            //txtCount.Attributes.Add("onblur", "RecordScanKey();return false;");
        }
    }
}