using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

using System.Data.Linq;
using System.Web.Configuration;
using System.Web.Security;

using System.Web.UI.WebControls;
using BM_WebApp.Account;
using BW_WebApp.DataManagers;

namespace BM_WebApp
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {

        List<string> restrictedList = new List<string>();
        protected void Page_Load(object sender, EventArgs e)
        {

            lnkUnitStatus.Click += new EventHandler(lnkUnitStatus_Click);
            // Reset My Password ---------------------

            //MembershipUser mu = Membership.GetUser("jmccomb");
            //mu.UnlockUser();
            //mu.ChangePassword(Membership.GetUser("jmccomb").ResetPassword(), "Jamesg01_James");

            //mu = Membership.GetUser("Admin");
            //mu.UnlockUser();
            //mu.ChangePassword(Membership.GetUser("Admin").ResetPassword(), "Admin01_Admin");




            //////// bool x = Membership.EnablePasswordReset;
            //////string NewPassword = "NewPass999";                    //Membership.GeneratePassword(10, 0);      // "ThisIsNew001";


            //////if (mu.ChangePassword(Membership.GetUser(UserName).ResetPassword(), NewPassword) == true)
            //////{
            //////    UserAccessControl uac = new UserAccessControl(UserName);
            //////    if (uac != null) { uac.ForcePasswordReset(); }
            //////    ScriptManager.RegisterStartupScript(this, GetType(), "Password", "alert('" + string.Format("Password Reset for user: {0} to: {1}", UserName, NewPassword) + "');", true);
            //////}

            // ----------------------------------------------------
            string User = ((Page)HttpContext.Current.Handler).User.Identity.Name;
            Menu2.DataBound += new EventHandler(Menu2_DataBound);
            //lnkShowTimeOutValue.Click += new EventHandler(lnkShowTimeOutValue_Click);
            //ProjectManager pm = new ProjectManager("jmccomb");
            ProjectManager pm = new ProjectManager("");
            lSystem.Text = "(" + pm.SystemDisplayText() + ")";

            CleanURL Clean = new CleanURL();
            if (((Page)HttpContext.Current.Handler).User.Identity.IsAuthenticated == false)
            {
                if (Clean.Clean(HttpContext.Current.Request.Url.AbsolutePath) != "Login.aspx"
                 && Clean.Clean(HttpContext.Current.Request.Url.AbsolutePath) != "Default.aspx"
                 && Clean.Clean(HttpContext.Current.Request.Url.AbsolutePath) != "About.aspx"
                 && Clean.Clean(HttpContext.Current.Request.Url.AbsolutePath) != "TestParseDataStreamData.aspx"
                 && Clean.Clean(HttpContext.Current.Request.Url.AbsolutePath) != "Search_UnitStatus.aspx"
                    //&& HttpContext.Current.Request.Url.AbsolutePath != "/Account/pwdReset.aspx"
                    )
                {
                    Response.Redirect(@"~/Account/Login.aspx");
                }
            }


            // This is used to force a new user to reset their password before continuing.
            if (((Page)HttpContext.Current.Handler).User.Identity.IsAuthenticated == true)
            {
                MembershipUser mUser = Membership.GetUser(((Page)HttpContext.Current.Handler).User.Identity.Name);
                //               HeadLoginView.Controls.




                if (mUser == null) { return; }
                if (mUser == null) { Response.Redirect(@"~/Account/Login.aspx"); }





                if (((Page)HttpContext.Current.Handler).User.IsInRole("Client") == false)
                {
                    System.Web.UI.HtmlControls.HtmlAnchor an = (System.Web.UI.HtmlControls.HtmlAnchor)HeadLoginView.FindControl("A1");
                    if (an != null)
                    {
                        an.Visible = false;
                    }
                }

                UserAccessControl uac = new UserAccessControl(((Page)HttpContext.Current.Handler).User.Identity.Name);
                Int32 iDaysSinceChangedPWD = Convert.ToInt32(DateTime.Now.Subtract(mUser.LastPasswordChangedDate).TotalDays);
                if (mUser.CreationDate == mUser.LastPasswordChangedDate || uac.User.ForcePasswordReset != null)
                {
                    if (HttpContext.Current.Request.Url.AbsolutePath != "/Account/ChangePassword.aspx")
                    {
                        Response.Redirect("~/Account/ChangePassword.aspx?R=1");
                    }
                }

                //RoleMenuAccessManager rm = new RoleMenuAccessManager(User);
                //rm.CleanRoleMenuAccessTable(Menu2);

            }




            //SiteMap.SiteMapResolve += new SiteMapResolveEventHandler(SiteMap_SiteMapResolve);

            //Session.Timeout = 1;
            // 
            //SiteMapDataSource2.DataBinding += new EventHandler(SiteMapDataSource2_DataBinding);
        }

        //void lnkShowTimeOutValue_Click(object sender, EventArgs e)
        //{
        //    decimal TimeoutValue = 0;
        //    TimeoutValue = HttpContext.Current.Server.ScriptTimeout;
        //    ScriptManager.RegisterStartupScript(this, GetType(), "LoadClient", "alert('TimeoutValue:" + TimeoutValue.ToString() + " seconds.');", true);
        //}

        void lnkUnitStatus_Click(object sender, EventArgs e)
        {
            // OPen the Search_UnitStatus.aspx form.
            Response.Redirect("~/Search_UnitStatus.aspx");
        }

        void Menu2_DataBound(object sender, EventArgs e)
        {
            MenuManager mm = new MenuManager(Menu2);

            if (((Page)HttpContext.Current.Handler).User.Identity.IsAuthenticated == false)
            {
                // Load all the items except "Home" and turn them off.
                // mm.LoadAllMenuItems();
                mm.RestrictMenu();

            }
            else
            {
                string User = ((Page)HttpContext.Current.Handler).User.Identity.Name;
                RoleMenuAccessManager rm = new RoleMenuAccessManager(User);
                string[] role = Roles.GetRolesForUser(User);
                mm.restrictedList = rm.GetData(role, ((Page)HttpContext.Current.Handler).User.IsInRole("Admin"));
                mm.RestrictMenu();
            }
        }
    }
}