using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
//using Factory_DataModel;
using BW_WebApp.DataManagers;

namespace BW_WebApp.Maintenance
{
    public partial class Maint_ClientAllowedProject : System.Web.UI.Page
    {
        //private string blank = "&nbsp;";

        protected void Page_Load(object sender, EventArgs e)
        {


            grdProjectList.RowDataBound += new GridViewRowEventHandler(grdProjectList_RowDataBound);
            btnSaveAllowedProject.Click += new EventHandler(btnSaveAllowedProject_Click);


            if (IsPostBack == false)
            {
                lblClientTest_01.Text = "Allowed Projects";
                string zID = Request.QueryString.Get("ID");
                hdnClientID.Value = zID;
                decimal ID = -1;
                if (decimal.TryParse(zID, out ID) == false) { ID = -1; };

                ClientManager cm = new ClientManager(User.Identity.Name);
                Client c = cm.GetClient(ID);
                if (c != null)
                {
                    lblClientTest_01.Text = "Allowed Projects for:" + c.CompanyName;
                }

                UpdateProjectGrid();
                UpdateProjectList(ID);


            }
        }


        protected void UpdateProjectGrid()
        {
            ProjectManager pm = new ProjectManager(User.Identity.Name);
            grdProjectList.DataSource = pm.GetProjectList();
            grdProjectList.DataBind();
            ////   UpdateClientQuestionRestrictionGrid();
        }
        protected void UpdateProjectList(decimal ClientID)
        {
            //if (ClientID < 1) { lblDefault.Text = "Default Loaded"; }
            ClientManager cm = new ClientManager(User.Identity.Name);
            List<PairIDValue> RQ = cm.ClientProjectAllowedList(ClientID);
            PairIDValue pd = new PairIDValue();
            foreach (GridViewRow r in grdProjectList.Rows)
            {
                decimal ProjectID = -1;
                HiddenField hf = (HiddenField)r.FindControl("hdnProjectID");
                if (decimal.TryParse(hf.Value, out ProjectID) == false) { ProjectID = -1; }
                string sProjectID = hf.Value;
                hf = (HiddenField)r.FindControl("hdnClientID");
                if (hf != null) { hf.Value = ClientID.ToString(); }
                hf = (HiddenField)r.FindControl("hdnClientProjectDependenciesID");
                if (hf != null) { hf.Value = "-1"; }
                CheckBox CB = (CheckBox)r.FindControl("chkThisProject");
                if (CB != null) { CB.Checked = false; }
                pd = RQ.FirstOrDefault(x => x.ID == ProjectID);
                if (pd != null)
                {
                    if (CB != null) { CB.Checked = true; hf.Value = pd.Desc; }
                }
            }
        }
        void btnSaveAllowedProject_Click(object sender, EventArgs e)
        {
            decimal ClientProjectDependenciesID = -1;
            decimal ClientID = -1;
            decimal ProjectID = -1;
            string sID = "";
            HiddenField hf;

            CheckBox cb;

            foreach (GridViewRow row in grdProjectList.Rows)
            {
                cb = (CheckBox)row.FindControl("chkThisProject");
                hf = (HiddenField)row.FindControl("hdnClientProjectDependenciesID");
                if (hf != null) { sID = hf.Value; }
                if (decimal.TryParse(sID, out ClientProjectDependenciesID) == false) { ClientProjectDependenciesID = -1; }
                hf = (HiddenField)row.FindControl("hdnProjectID");
                if (hf != null) { sID = hf.Value; }
                if (decimal.TryParse(sID, out ProjectID) == false) { ProjectID = -1; }
                hf = (HiddenField)row.FindControl("hdnClientID");
                if (hf != null) { sID = hf.Value; }
                if (decimal.TryParse(sID, out ClientID) == false) { ClientID = -1; }


                if (cb.Checked == true || ClientProjectDependenciesID > 0)
                {
                    ClientManager cm = new ClientManager(User.Identity.Name);
                    if (cb.Checked == false)
                    {
                        cm.DeleteClientProjectDependencies(ClientProjectDependenciesID);
                        // delete the ClientBillingPoint Record
                    }
                    else
                    {
                        cm.AddUpdateDeleteClientProjectDependencies(ClientProjectDependenciesID, ClientID, ProjectID);
                        // Add a new ClientBillingPoint Record.
                    }
                }
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "xxx", "alert('Data Updated!');", true);

            //UpdateBillingPoints(ClientID);

        }
        void grdProjectList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Type type = e.Row.DataItem.GetType();               //.GetGenericArguments()[0]; 
                HiddenField hf = (HiddenField)e.Row.FindControl("hdnClientProjectDependenciesID");
                if (hf != null) { hf.Value = "-1"; }
                hf = (HiddenField)e.Row.FindControl("hdnProjectID");
                if (hf != null) { hf.Value = ((Project)e.Row.DataItem).ProjectID.ToString(); }
                CheckBox cb = (CheckBox)e.Row.FindControl("chkThisProject");
                if (cb != null)
                {
                    cb.Checked = false;
                }
            }
        }


    }
}