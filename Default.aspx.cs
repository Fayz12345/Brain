using System;
using System.Web.Security;
using BW_WebApp.DataManagers;

namespace BW_WebApp
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (User.Identity.Name.Length > 0)
            {
                BasicUserUtilities bu = new BasicUserUtilities(User.Identity.Name);
                UserTable ut = bu.GetUserTable();
                if (ut != null && ut.ForcePasswordReset != null)
                {
                    DateTime forcedate = (DateTime)ut.ForcePasswordReset;
                    MembershipUser mUser = Membership.GetUser(User.Identity.Name);
                    if (mUser.LastPasswordChangedDate < forcedate)
                    {
                        Response.Redirect("~/Account/ChangePassword.aspx");
                    }
                }
            }
            CompanyDemographics demo = new CompanyDemographics(User.Identity.Name);
            lblCompanyName.Text = demo.WelcomeText;
        }
    }
}