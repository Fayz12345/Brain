using System;
using BW_WebApp.DataManagers;

namespace BM_WebApp.Account
{
    public partial class ChangePasswordSuccess : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UserAccessControl uac = new UserAccessControl(User.Identity.Name);
            if (uac != null) { uac.ClearPasswordReset(); }
        }
    }
}