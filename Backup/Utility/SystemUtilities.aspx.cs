using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BW_WebApp.DataManagers;

namespace BW_WebApp.Utility
{
    public partial class SystemUtilities : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnUpdateHeaderLevelKeys.Click += new EventHandler(btnUpdateHeaderLevelKeys_Click);
            btnCleanMenu.Click += new EventHandler(btnCleanMenu_Click);
            btnRemove.Click += new EventHandler(btnRemove_Click);
        }

        void btnRemove_Click(object sender, EventArgs e)
        {
            RoleMenuAccessManager rm = new RoleMenuAccessManager(User.Identity.Name);
            lblCleanMenuMessage.Text = rm.CleanRoleMenuAccessTable_Delete();
        }

        void btnCleanMenu_Click(object sender, EventArgs e)
        {
            RoleMenuAccessManager rm = new RoleMenuAccessManager(User.Identity.Name);
            lblCleanMenuMessage.Text = rm.CleanRoleMenuAccessTable();
        }

        void btnUpdateHeaderLevelKeys_Click(object sender, EventArgs e)
        {
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                ctx.CommandTimeout = 960;        // 8 minutes = 8 * 60 = 480 seconds
                ctx.Util_RebuildReceiveDetailHeaderAttributes();
                lblmessage.Text = "Update Complete";
                //ScriptManager.RegisterStartupScript(this, GetType(), "Done", "alert('Update Complete!');", true);
            }
        }
    }
}