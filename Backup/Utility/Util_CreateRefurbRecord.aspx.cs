using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BW_WebApp.DataManagers;
//using Factory_Businesslayer;

namespace BW_WebApp.Utility
{
    public partial class Util_CreateRefurbRecord : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnProcess.Click += new EventHandler(btnProcess_Click);
            hdnUserName.Value = User.Identity.Name;
            if (!IsPostBack)
            {

                //ProjectManager pm = new ProjectManager(User.Identity.Name);
                //drpProjectList.DataValueField = "ProjectID";
                //drpProjectList.DataTextField = "Name";
                //drpProjectList.DataSource = pm.GetMasterActiveProjectList();
                //drpProjectList.DataBind();
                //drpProjectList.SelectedIndex = 0;


            }
            txtESN.Focus();
            //txtWaybill.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13) || (event.which == 9) || (event.keyCode == 9)) {SetESNFocus();return false;}} else {return true}; ");
            //txtWaybill.Attributes.Add("onblur", "IsMessage();return false;");



            //txtESN.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13) || (event.which == 9) || (event.keyCode == 9)) {SetESNFocus();return false;}} else {return true}; ");
            //txtESN.Attributes.Add("onblur", "RecordScanKey();return false;");
        }

        void btnProcess_Click(object sender, EventArgs e)
        {
            if (txtESN.Text.Length > 0)
            {
                CreateRefurbRecord();
            }
            txtESN.Text = "";
            txtESN.Focus();
        }

        void AddHistory(string Message, bool ShowPopup)
        {
            //lstHistory.Items.Add(Message);
            lstHistory.Items.Insert(0, Message);
            if (ShowPopup == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Error", "alert('" + Message + "');", true);
            }
        }

        void CreateRefurbRecord()
        {
            ReceiveDetailManager rm = new ReceiveDetailManager(User.Identity.Name);
            clsLinqDataContext ctx = rm.GetDataContext();
            decimal ProjectID = -1;
            decimal ProcessID = -1;
            decimal ClientLocationID = -1;

            #region Edit Checks
            ReceiveDetail rd = rm.ReceiveDetail(ctx, txtESN.Text);
            if (rd == null)
            {
                AddHistory(txtESN.Text + " Not Found!", true);
                return;
            }
            if (rd.Projects.Name.Trim().ToUpper() != "HARVEST")
            {
                AddHistory(txtESN.Text + "This unit cannot be refurbished at this time, unit is not in the Harvest project!", true);
                return;
            }
            Project pr = ctx.Projects.FirstOrDefault(x => x.Name.Trim().ToUpper() == "BUILD FOR SALE");
            if (pr == null)
            {
                AddHistory(txtESN.Text + " Project build for sale not found!", true);
                return;
            }
            ProjectProcess p = ctx.ProjectProcesses.OrderBy(x => x.Process.Sequence).FirstOrDefault(x => x.ProjectID == pr.ProjectID && x.Process.Name.Substring(0, 7).ToUpper() == "RECEIVE");
            if (p == null)
            {
                AddHistory(txtESN.Text + " ESN found, Invalid Process (No Receive Process Set up)!", true);
                return;
            }
            ClientLocation cl = ctx.ClientLocations.FirstOrDefault(x => x.ScanKey.ToUpper() == "GMP01");
            if (cl == null)
            {
                AddHistory(" Client Location 'GMP01' not found!", true);
                return;
            }
            #endregion

            ProjectID = pr.ProjectID;
            ProcessID = p.ProcessID;
            ClientLocationID = cl.ClientLocationID;

            rm.UpdateESNAttribute(ctx, rd.ReceiveDetailID, "BIN", "0");
            rm.UpdateESNAttribute(ctx, rd.ReceiveDetailID, "Location", "None");
            rm.UpdateESNAttribute(ctx, rd.ReceiveDetailID, "Out-Bound Waybill-S", "Sent To Build For Sale For Refurbishing");
            rm.Clone(ctx, "5", rd.ReceiveDetailID, ProjectID, ClientLocationID, ProcessID);

            // Get the newly cloned record.
            rd = rm.ReceiveDetail(txtESN.Text);
            if (rd == null)
            {
                AddHistory("New " + txtESN.Text + " Not Found!", true);
                return;
            }
            rm.UpdateESNAttribute(rd.ReceiveDetailID, "BIN", "007");
            rm.UpdateESNAttribute(rd.ReceiveDetailID, "Location", "Tech Lab");
            AddHistory(txtESN.Text + " Created!", false);
            ScriptManager.RegisterStartupScript(this, GetType(), "BagTag", "OpenBagTag(" + rd.ReceiveDetailID + ");", true);



        }
    }
}