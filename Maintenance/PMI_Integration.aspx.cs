using System;
using System.Web.UI;
using BW_IntegrationManager_PublicMobile;


namespace BW_WebApp.Maintenance
{
    public partial class PMI_Integration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnFile01.Click += new EventHandler(btnFile01_Click);
            btnReadFile01.Click += new EventHandler(btnReadFile01_Click);
        }

        void btnReadFile01_Click(object sender, EventArgs e)
        {
            string fName = @"C:\Users\Public\TestFolder\WriteText.txt";
            IntegrationManager_PublicMobile x = new IntegrationManager_PublicMobile(User.Identity.Name);
            x.ReadFile01(fName);
        }

        void btnFile01_Click(object sender, EventArgs e)
        {
            string fName = "WriteText.txt";

            //Server.MapPath("~"), "UtilityUpload_01_Log.txt", User.Identity.Name, System.Configuration.ConfigurationManager.AppSettings["WriteLogUserNameToLog"]
            string fPath = Server.MapPath("~/" + System.Configuration.ConfigurationManager.AppSettings["PMI_EDI_Directory"] + "/" + fName);

            IntegrationManager_PublicMobile x = new IntegrationManager_PublicMobile(User.Identity.Name);
            x.GenerateFile01(fPath);

            ScriptManager.RegisterStartupScript(this, GetType(), "Download", "ShowLogReport('" + fName + "');", true);

        }
    }
}