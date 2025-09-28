using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using Factory_DataModel;
using BW_WebApp.DataManagers;
namespace BM_WebApp.Account
{
    public partial class ChangePin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ChangePasswordPushButton.Click += new EventHandler(ChangePasswordPushButton_Click);
            lblUserName.Text = User.Identity.Name;
            //CurrentPassword.Text = "";
            //NewPassword.Text = "";
            //ConfirmNewPassword.Text = "";
        }

        void ChangePasswordPushButton_Click(object sender, EventArgs e)
        {
            string StatusText = "";
            if (CurrentPassword.Text.Length == 0)
            {
                StatusText = "Current PIN Not set";
                lblStatus.Text = StatusText;
                CurrentPassword.Focus();
                return;
            }
            if (NewPassword.Text.Length == 0)
            {
                StatusText = "New PIN Not set";
                lblStatus.Text = StatusText;
                CurrentPassword.Focus();
                return;
            }
            if (ConfirmNewPassword.Text.Length == 0)
            {
                StatusText = "Confirm PIN Not set";
                lblStatus.Text = StatusText;
                CurrentPassword.Focus();
                return;
            }





            if (NewPassword.Text != ConfirmNewPassword.Text)
            {
                StatusText = "New PIN Does not match your Confirm PIN";
                lblStatus.Text = StatusText;
                CurrentPassword.Text = "";
                NewPassword.Text = "";
                ConfirmNewPassword.Text = "";
                CurrentPassword.Focus();
                return;
            }
            if (NewPassword.Text.Length < 4)
            {
                StatusText = "PIN must be at least 4 digits long";
                lblStatus.Text = StatusText;
                CurrentPassword.Text = "";
                NewPassword.Text = "";
                ConfirmNewPassword.Text = "";
                CurrentPassword.Focus();
                return;
            }
            decimal x = 0;
            if (decimal.TryParse(NewPassword.Text, out x) == false)
            {
                StatusText = "PIN must be numeric";
                lblStatus.Text = StatusText;
                CurrentPassword.Text = "";
                NewPassword.Text = "";
                ConfirmNewPassword.Text = "";
                CurrentPassword.Focus();
                return;
            }

            if (NewPassword.Text == CurrentPassword.Text)
            {
                StatusText = "New PIN can not be the same as the old PIN.";
                lblStatus.Text = StatusText;
                CurrentPassword.Text = "";
                NewPassword.Text = "";
                ConfirmNewPassword.Text = "";
                CurrentPassword.Focus();
                return;
            }


            BasicUserUtilities UU = new BasicUserUtilities(User.Identity.Name);
            clsLinqDataContext ctx = UU.GetDataContext(User.Identity.Name);
            List<decimal> clList = UU.GetUserDefaultClientIDList(ctx, User.Identity.Name);
            if (clList.Count() == 0)
            {
                StatusText = "User not linked, call support";
                lblStatus.Text = StatusText;
                return;
            }

            ClientManager cm = new ClientManager(User.Identity.Name);
            int s = 0;
            int c = 0;
            foreach (decimal clientid in clList)
            {
                s++;
                ClientLocation cl = cm.GetClientLocation(clientid);
                if (cl != null && cl.ApprovalPassword == CurrentPassword.Text)
                {
                    cl.ApprovalPassword = NewPassword.Text;
                    cm.UpdateClientLocationPin(cl);
                    c++;
                }
            }

            if (c > 0)
            {
                StatusText = "Successfully Changed Pin:Locations scanned(" + s.ToString() + "), updated(" + c.ToString() + ").";
            }
            else
            {
                StatusText = "Pin Not found/Changed.";
            }
            lblStatus.Text = StatusText;
            CurrentPassword.Text = "";
            NewPassword.Text = "";
            ConfirmNewPassword.Text = "";
            CurrentPassword.Focus();
            return;
        }
    }
}