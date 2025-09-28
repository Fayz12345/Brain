using System;
using System.Web.Security;
using System.Web.UI.WebControls;
using BW_WebApp.DataManagers;

namespace BW_WebApp.Account
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //LoginUser.Authenticate += new AuthenticateEventHandler(LoginUser_Authenticate);
            CompanyDemographics demo = new CompanyDemographics("Admin");
            //lblCompanyName.Text = demo.WelcomeText;
            //lblSupportEmail.Text = demo.SupportEmailVerbose;
            //RegisterHyperLink.NavigateUrl = "Register.aspx?ReturnUrl=" + HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
            TextBox t = new TextBox();
            t = (TextBox)LoginUser.FindControl("UserName");
            if (t != null)
            {
                t.Focus();
            }
        }

        void LoginUser_Authenticate(object sender, AuthenticateEventArgs e)
        {
            if (Membership.ValidateUser(LoginUser.UserName, LoginUser.Password))
            {
                MembershipUser mUser = Membership.GetUser(LoginUser.UserName);
                Int32 iDaysSinceChangedPWD = Convert.ToInt32(DateTime.Now.Subtract(mUser.LastPasswordChangedDate).TotalDays);
                if (mUser.CreationDate == mUser.LastPasswordChangedDate)
                {
                    e.Authenticated = true;
                    Response.Redirect("~/Account/ChangePassword.aspx?R=1");
                    //Response.Redirect("~/Account/ChangePassword.aspx?i=" + Server.UrlEncode(LoginUser.UserName));
                    //Response.Redirect("~/Admin/pwdReset.aspx?i=" + Server.UrlEncode(LoginUser.UserName));
                }
                else
                {
                    e.Authenticated = true;
                }
            }
            else
            {
                e.Authenticated = false;
            }

            TextBox t = new TextBox();
            t = (TextBox)LoginUser.FindControl("UserName");
            if (t != null)
            {
                t.Focus();
            }


            //throw new NotImplementedException();
        }


    }
}