using System;
using BW_WebApp.Classes;

namespace BW_WebApp.Utility
{
    public partial class TestSendEmail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            btnSend.Click += new EventHandler(btnSend_Click);


        }


        void btnSend_Click(object sender, EventArgs e)
        {
            SendEmail se = new SendEmail();
            lblResult.Text = se.SendPartsRequestedEmail(69536, GetUserIPAddress(), User.Identity.Name);
            //lblResult.Text = se.Email(txtTo.Text, txtBody.Text, txtSubject.Text);
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