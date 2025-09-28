using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Web.Security;
// using ScanKey;
using BW_WebApp.DataManagers;
//using Factory_Businesslayer;

namespace BW_WebApp
{
    public partial class ReceiveHeaderForm : System.Web.UI.Page
    {
        string _ConnectionString = string.Empty;
        // JsonString KeyData = new JsonString();
        //string _ProcessLevel = "RECEIVED";
        //public string ProcessLevel { 
        //    get {
        //        if (_ProcessLevel.Length == 0)
        //        { _ProcessLevel = "RECEIVE"; }
        //        return _ProcessLevel; } 
        //    set { _ProcessLevel = value; } }
        public string ConnectionString
        {
            get
            {
                if (_ConnectionString.Length == 0)
                {
                    System.Configuration.ConnectionStringSettingsCollection xconnectionString = WebConfigurationManager.ConnectionStrings;
                    //if (xconnectionString != null) { _ConnectionString = xconnectionString["GMP_DataEntities"].ConnectionString.ToString(); }
                    if (xconnectionString != null) { ConnectionString = xconnectionString["DefaultConnectionString"].ConnectionString.ToString(); }
                }

                return _ConnectionString;
            }
            set { _ConnectionString = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            btnSave.OnClientClick = "";
            btnClearData.Attributes.Add("OnClick", "ClearData(); return false;");
            btnBagTag.Attributes.Add("OnClick", "GenerateBagTag(); return false;");
            btnSave.Attributes.Add("OnClick", "return DoSave();");
            hdnUserName.Value = User.Identity.Name;
            if (!IsPostBack)
            {
                //                LoadProcessLevelData("");
                BasicUserUtilities buu = new BasicUserUtilities(User.Identity.Name);
                ReceiveDetailManager rm = new ReceiveDetailManager(User.Identity.Name);
                ProjectManager pm = new ProjectManager(User.Identity.Name);
                drpStatus.DataValueField = "ReceiveDetailStatusID";
                drpStatus.DataTextField = "Status";
                drpStatus.DataSource = rm.GetReceiveDetailStatusList();
                drpStatus.DataBind();
                drpStatus.SelectedIndex = 0;

                drpProjectListNew.DataValueField = "ProjectID";
                drpProjectListNew.DataTextField = "Name";
                drpProjectListNew.DataSource = pm.GetProjectList();         //pm.GetMasterProjectList();
                drpProjectListNew.DataBind();
                drpProjectListNew.SelectedIndex = 0;

                //string Status = drpProjectList.SelectedItem.Text;
                decimal projectID = 0;
                decimal.TryParse(drpProjectListNew.SelectedItem.Value, out projectID);
                HdnProjSetup.Value = pm.GetSetUpFieldDef(projectID);
                //LoadProcessLevelData("");
               // txtDateReceived.Text = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
            }


            ScanKey.Focus();
            ScanKey.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {RecordScanKey();return false;}} else {return true}; ");
            ScanKey.Attributes.Add("onblur", "RecordScanKey();return false;");
        }



        //void drpProjectList_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    string project = drpProjectList.SelectedItem.Text;
        //    decimal projectID = 0;
        //    decimal.TryParse(drpProjectList.SelectedItem.Value, out projectID);
        //    ProjectManager pm = new ProjectManager(User.Identity.Name);
        //    HdnProjSetup.Value = pm.GetSetUpFieldDef(projectID);
        //    LoadProcessLevelData("");

        //    //txtProject.Text = drpProjectList.SelectedItem.Text;
        //    //ScriptManager.RegisterStartupScript(this, GetType(), "RestHeader", "RestoreHeaderData(); SetUpScreen('');", true);
        //}



        //private void LoadProcessLevelData(string ProcessLevel)
        //{
        //    string project = txtProject.Text;
        //    BasicUserUtilities buu = new BasicUserUtilities(User.Identity.Name);
        //    ProcessLevel = buu.GetUserDefaultProcess(ProcessLevel, project);
        //    //txtDateReceived.Enabled = false;
        //    txtRMA.Enabled = false;
        //    //txtQTY.Enabled = false;
        //    txtProjectTag.Enabled = false;

        //    if (ProcessLevel.ToUpper() == "RECEIVE" || ProcessLevel.ToUpper() == "BULKRECEIVE" || ProcessLevel.ToUpper() == "RECEIVEFROMBULK")
        //    {
        //        //txtQTY.Enabled = true;
        //        txtRMA.Enabled = true;
        //        txtProjectTag.Enabled = true;
        //    }
        //    SetupProcessCheckBoxes();
        //    ScriptManager.RegisterStartupScript(this, GetType(), "SetUP", "SetUpScreen('" + ProcessLevel + "');", true);
        //}

 

        void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // HiddenField HID = (HiddenField)e.Row.FindControl("HiddenID");
                HiddenField HName = (HiddenField)e.Row.FindControl("HiddenName");
                Question rec = (Question)e.Row.DataItem;
                decimal ID = rec.QuestionID;
                Label l1 = (Label)e.Row.FindControl("Description");
                CheckBoxList c1 = (CheckBoxList)e.Row.FindControl("checkAnswer");
                TextBox t1 = (TextBox)e.Row.FindControl("TextAnswer");
                RadioButtonList R1 = (RadioButtonList)e.Row.FindControl("RadioAnswer");
                TextBox Cal = (TextBox)e.Row.FindControl("CalAnswer");
                DropDownList drp = (DropDownList)e.Row.FindControl("drpAnswer");
            }
        }



        private string RemoveBadCharacters(string Value)
        {
            return Value.Replace(" ", "").Replace("/", "").Replace("*", "").Replace(".", "").Replace("#", "");
        }

        private void SetupProcessCheckBoxes()
        {
            chkProcessCheckList.Items.Clear();
            ProcessManager pm = new ProcessManager(User.Identity.Name);
            List<Process> pl = pm.GetProcesssThisProjectForCompletion(txtProject.Text);
            foreach (Process p in pl.OrderBy(x => x.Sequence))
            {
                ListItem x = new ListItem(p.Name, p.ProcessID.ToString());
                chkProcessCheckList.Items.Add(x);
                chkProcessCheckList.Items[chkProcessCheckList.Items.Count - 1].Attributes.Add("someValue", p.ProcessID.ToString());
            }
            chkProcessCheckList.Enabled = false;
        }


    }
}