using System;
using BW_WebApp.DataManagers;
//using Factory_Businesslayer;

namespace BW_WebApp.Utility
{
    public partial class UtilityChangeIMEI : System.Web.UI.Page
    {
        clsLog log;
        //List<string> attributeclonelist = new List<string>();

        protected void Page_Load(object sender, EventArgs e)
        {
            log = new clsLog(Server.MapPath("~"), "UtilityChangeIMEI_01_Log.txt", User.Identity.Name, System.Configuration.ConfigurationManager.AppSettings["WriteLogUserNameToLog"]);
            btnSave.Click += new EventHandler(btnSave_Click);


            if (System.Configuration.ConfigurationManager.AppSettings["WriteLog"].ToUpper() == "TRUE")
            {
                log.writeLogData = true;
            }
            log.LogIt("**** Utility Screen Page load Started");

            //btnIMEIUpload.Attributes.Add("OnLoad", "this.disabled='false'; this.value='Upload';alert('here');");
            if (!IsPostBack)
            {

            }
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            if (OriginalIMEI.Text.Length == 0)
            {
                lblMessage.Text = "You must supply an IMEI number (the incorrect one).";
                OriginalIMEI.Focus();
                return;
            }
            if (Version.Text.Length == 0)
            {
                lblMessage.Text = "You must supply a Version number (for the incorrect IMEI).";
                Version.Focus();
                return;
            }
            if (NewIMEI.Text.Length == 0)
            {
                lblMessage.Text = "You must supply a New IMEI number (to replace the incorrect IMEI).";
                NewIMEI.Focus();
                return;
            }

            ReceiveDetailManager rdm = new ReceiveDetailManager(User.Identity.Name);
            lblMessage.Text = rdm.UtilityChangeIMEI(OriginalIMEI.Text, Version.Text, NewIMEI.Text);
        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            if (log != null)
            {
                log.LogIt("**** Utility Screen Page Unload -- HTML sent to browser");
            }
        }

    }
}