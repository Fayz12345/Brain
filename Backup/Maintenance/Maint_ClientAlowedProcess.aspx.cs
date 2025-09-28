using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using Factory_DataModel;
using BW_WebApp.DataManagers;

namespace BW_WebApp.Maintenance
{
    public partial class Maint_ClientAlowedProcess : System.Web.UI.Page
    {
        //private string blank = "&nbsp;";

        protected void Page_Load(object sender, EventArgs e)
        {


            grdProcessList.RowDataBound += new GridViewRowEventHandler(grdProcessList_RowDataBound);
            btnSaveAllowedProcesses.Click += new EventHandler(btnSaveAllowedProcesses_Click);


            if (IsPostBack == false)
            {
                lblClientTest_01.Text = "Allowed Processes";
                string zID = Request.QueryString.Get("ID");
                hdnClientID.Value = zID;
                decimal ID = -1;
                if (decimal.TryParse(zID, out ID) == false) { ID = -1; };

                ClientManager cm = new ClientManager(User.Identity.Name);
                Client c = cm.GetClient(ID);
                if (c != null)
                {
                    lblClientTest_01.Text = "Allowed Processes for:" + c.CompanyName;
                }
                UpdateProcessGrid();
                UpdateProcessList(ID);
            }
        }




        protected void UpdateProcessList(decimal ClientID)
        {
            //if (ClientID < 1) { lblDefault.Text = "Default Loaded"; }
            ClientManager cm = new ClientManager(User.Identity.Name);
            List<PairIDValue> RQ = cm.ClientProcessAllowedList(ClientID);        // This needs to get ProcessAllowedList, not project
            PairIDValue pd = new PairIDValue();
            foreach (GridViewRow r in grdProcessList.Rows)
            {
                decimal ProcessID = -1;
                HiddenField hf = (HiddenField)r.FindControl("hdnProcessID");
                if (decimal.TryParse(hf.Value, out ProcessID) == false) { ProcessID = -1; }
                string sProcessID = hf.Value;
                hf = (HiddenField)r.FindControl("hdnClientID");
                if (hf != null) { hf.Value = ClientID.ToString(); }
                hf = (HiddenField)r.FindControl("hdnClientProcessDependenciesID");
                if (hf != null) { hf.Value = "-1"; }
                CheckBox CB = (CheckBox)r.FindControl("chkThisProcess");
                if (CB != null) { CB.Checked = false; }
                pd = RQ.FirstOrDefault(x => x.ID == ProcessID);
                if (pd != null)
                {
                    if (CB != null) { CB.Checked = true; hf.Value = pd.Desc; }
                }
            }
        }




        protected void UpdateProcessGrid()
        {
            ProcessManager pm = new ProcessManager(User.Identity.Name);
            grdProcessList.DataSource = pm.GetProcesssAll().OrderBy(x => x.ScanKey);
            grdProcessList.DataBind();
        }


        void grdProcessList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Type type = e.Row.DataItem.GetType();               //.GetGenericArguments()[0]; 
                HiddenField hf = (HiddenField)e.Row.FindControl("hdnClientProcessDependenciesID");
                if (hf != null) { hf.Value = "-1"; }
                hf = (HiddenField)e.Row.FindControl("hdnProcessID");
                if (hf != null) { hf.Value = ((Process)e.Row.DataItem).ProcessID.ToString(); }
                CheckBox cb = (CheckBox)e.Row.FindControl("chkThisProcess");
                if (cb != null)
                {
                    cb.Checked = false;
                }
            }
        }


        void btnSaveAllowedProcesses_Click(object sender, EventArgs e)
        {
            decimal ClientProcessDependenciesID = -1;
            decimal ClientID = -1;
            decimal ProcessID = -1;
            string sID = "";
            HiddenField hf;

            CheckBox cb;

            foreach (GridViewRow row in grdProcessList.Rows)
            {
                cb = (CheckBox)row.FindControl("chkThisProcess");
                hf = (HiddenField)row.FindControl("hdnClientProcessDependenciesID");
                if (hf != null) { sID = hf.Value; }
                if (decimal.TryParse(sID, out ClientProcessDependenciesID) == false) { ClientProcessDependenciesID = -1; }
                hf = (HiddenField)row.FindControl("hdnProcessID");
                if (hf != null) { sID = hf.Value; }
                if (decimal.TryParse(sID, out ProcessID) == false) { ProcessID = -1; }
                hf = (HiddenField)row.FindControl("hdnClientID");
                if (hf != null) { sID = hf.Value; }
                if (decimal.TryParse(sID, out ClientID) == false) { ClientID = -1; }


                if (cb.Checked == true || ClientProcessDependenciesID > 0)
                {
                    ClientManager cm = new ClientManager(User.Identity.Name);
                    if (cb.Checked == false)
                    {
                        cm.DeleteClientProcessDependencies(ClientProcessDependenciesID);
                        // delete the ClientBillingPoint Record
                    }
                    else
                    {
                        cm.AddUpdateDeleteClientProcessDependencies(ClientProcessDependenciesID, ClientID, ProcessID);
                        // Add a new ClientBillingPoint Record.
                    }
                }
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "xxx", "alert('Data Updated!');", true);
        }
    }
}