using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using BW_WebApp.DataManagers;

namespace BW_WebApp
{
    public partial class Dashboard_ProdPlace_01 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnRefreshINVQTY.Click += new EventHandler(btnRefreshINVQTY_Click);
            if (IsPostBack == false)
            {
                SetupParameters();
                //UpdateStats();
            }
        }

        void btnRefreshINVQTY_Click(object sender, EventArgs e)
        {
            LoadInventoryQTY();
        }
        void grdTabRepair01_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {

            }
        }

        protected void UpdateStats(object sender, EventArgs e)
        {
            UpdateStats();
        }
        protected void UpdateStats()
        {
            if (MainTab.ActiveTab.HeaderText == "Inventory QTY")
            {
                LoadInventoryQTY();
            }
        }

         private void LoadInventoryQTY()
        {
            decimal ProjectID = -1;
            string Product_Place = drpProductPlacement.SelectedItem.Text;
            string ProjectName = drpProjectList.SelectedItem.Text;
            string ProjectIDs = drpProjectList.SelectedItem.Value;
            if (decimal.TryParse(drpProjectList.SelectedItem.Value, out ProjectID) == false) { ProjectID = -1; }

            DashboardManager ms = new DashboardManager();
            ////grdTabRepair01.DataSource = ms.Get_Repair01DataGridValues(txtBeginDate.Text, txtWorkDaysToReport.Text);
            grdInventoryQTY.DataSource = ms.GetDashboardInventoryQTY_01_GridValueFiltered(ProjectID, Product_Place, txtRoleFilter.Text);
            grdInventoryQTY.DataBind();
        }

        private void SetupParameters()
        {
            #region
            #endregion

            ProjectManager pm = new ProjectManager(User.Identity.Name);
            List<Project> pl = pm.GetProjectList();
            drpProjectList.Items.Clear();
            ListItem z = new ListItem("All", "-1");
            //drpProjectList.Items.Add(z);
            foreach (Project p in pl)
            {
                ListItem x = new ListItem(p.Name, p.ProjectID.ToString());
                drpProjectList.Items.Add(x);
            }

            QuestionManager qm = new QuestionManager(User.Identity.Name);
            List<Option> ol = qm.GetQuestionOptionList("Product Place");
            drpProductPlacement.Items.Clear();
            z = new ListItem("All", "-1");
            //drpProductPlacement.Items.Add(z);
            foreach (Option o in ol)
            {
                ListItem x = new ListItem(o.OptionText, o.OptionID.ToString());
                drpProductPlacement.Items.Add(x);
            }
        }
    }
}