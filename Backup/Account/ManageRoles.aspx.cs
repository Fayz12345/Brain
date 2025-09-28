using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
//using Factory_DataModel;
using BW_WebApp.DataManagers;

namespace BW_WebApp.Account
{
    public partial class ManageRoles : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CreateRoleButton.Click += new EventHandler(CreateRoleButton_Click);
            //RoleList.RowDeleting += new GridViewDeleteEventHandler(RoleList_RowDeleting);

            RoleList.RowCommand += new GridViewCommandEventHandler(RoleList_RowCommand);
            drpProjectList.SelectedIndexChanged += new EventHandler(drpProjectList_SelectedIndexChanged);
            btnProcess.Click += new EventHandler(btnProcess_Click);
            btnProject.Click += new EventHandler(btnProject_Click);
            btnClient.Click += new EventHandler(btnClient_Click);
            btnQuestion.Click += new EventHandler(btnQuestion_Click);
            btnAnswer.Click += new EventHandler(btnAnswer_Click);
            btnFunction.Click += new EventHandler(btnFunction_Click);
            btnSaveMenuRoles.Click += new EventHandler(btnSaveMenuRoles_Click);
            if (!Page.IsPostBack)
            {
                DisplayRolesInGrid();
                BindProjectList();
            }
        }




        void drpProjectList_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshIsolateProcessGrid();
        }

        void RefreshIsolateProcessGrid()
        {

            string UserName = lblUserName_Process.Text;
            string sProjectID = drpProjectList.SelectedItem.Value;
            decimal ProjectID = -1;
            if (decimal.TryParse(sProjectID, out ProjectID) == false) { ProjectID = -1; }

            BasicUserUtilities buu = new BasicUserUtilities(User.Identity.Name, hdn_Role.Value);
            List<MasterTableAcccessList> al = buu.GetMasterRoleAccessList(hdn_Role.Value, "Process", ProjectID);
            gvProcess.DataSource = al;    // (from z in ctx.NextProcessSteps.Where(y => y.Process1.Name.ToUpper() == ProcessLevel).OrderBy(y => y.Sequence) select z);
            gvProcess.DataBind();
        }

        void btnProject_Click(object sender, EventArgs e)
        {
            //string UserName = UserNameToUpdate.Value;
            BasicUserUtilities buu = new BasicUserUtilities(User.Identity.Name, User.Identity.Name);
            //BasicUserUtilities buu = new BasicUserUtilities(User.Identity.Name, UserName);
            foreach (GridViewRow row in gvProject.Rows)
            {
                HiddenField cid = (HiddenField)row.FindControl("hdnIDFieldp");
                decimal ID = 0;
                decimal.TryParse(cid.Value, out ID);
                CheckBox cbAdd = (CheckBox)row.FindControl("cbAddp");
                CheckBox cbSelect = (CheckBox)row.FindControl("cbSelectp");
                CheckBox cbUpdate = (CheckBox)row.FindControl("cbUpdatep");
                CheckBox cbDelete = (CheckBox)row.FindControl("cbDeletep");

                //if (cbSelect.Checked == true || cbAdd.Checked == true || cbUpdate.Checked == true || cbDelete.Checked == true)
                //{
                buu.UpdateRoleAccessTable(hdn_Role.Value, "Project", ID, cbSelect.Checked, cbAdd.Checked, cbUpdate.Checked, cbDelete.Checked, false);
                //}
                //                ScriptManager.RegisterStartupScript(this, GetType(), "xxxxxx", "alert('" + string.Format("IDField = : {0} - {1} - {2} - {3} - {4}", id.Value, cbAdd.Checked, cbSelect.Checked, cbUpdate.Checked, cbDelete.Checked) + "');", true);
            }
            // UserAccessTabs.Visible = false;
            buu.CleanUpRoleAccessTable(hdn_Role.Value, "Project");
        }

        void btnProcess_Click(object sender, EventArgs e)
        {
            BasicUserUtilities buu = new BasicUserUtilities(User.Identity.Name, User.Identity.Name);                                //UserName);
            foreach (GridViewRow row in gvProcess.Rows)
            {
                HiddenField cid = (HiddenField)row.FindControl("hdnIDFieldp");
                decimal ID = 0;
                decimal.TryParse(cid.Value, out ID);
                CheckBox cbAdd = (CheckBox)row.FindControl("cbAddp");
                CheckBox cbSelect = (CheckBox)row.FindControl("cbSelectp");
                CheckBox cbUpdate = (CheckBox)row.FindControl("cbUpdatep");
                CheckBox cbDelete = (CheckBox)row.FindControl("cbDeletep");

                buu.UpdateRoleAccessTable(hdn_Role.Value, "Process", ID, cbSelect.Checked, cbAdd.Checked, cbUpdate.Checked, cbDelete.Checked, false);
            }
            buu.CleanUpRoleAccessTable(hdn_Role.Value, "Process");

        }

        void btnClient_Click(object sender, EventArgs e)
        {
            BasicUserUtilities buu = new BasicUserUtilities(User.Identity.Name, User.Identity.Name);                                //UserName);
            foreach (GridViewRow row in gvClient.Rows)
            {
                HiddenField cid = (HiddenField)row.FindControl("hdnIDFieldc");
                decimal ID = 0;
                decimal.TryParse(cid.Value, out ID);
                CheckBox cbAdd = (CheckBox)row.FindControl("cbAddc");
                CheckBox cbSelect = (CheckBox)row.FindControl("cbSelectc");
                CheckBox cbUpdate = (CheckBox)row.FindControl("cbUpdatec");
                CheckBox cbDelete = (CheckBox)row.FindControl("cbDeletec");

                buu.UpdateRoleAccessTable(hdn_Role.Value, "Clientx", ID, cbSelect.Checked, cbAdd.Checked, cbUpdate.Checked, cbDelete.Checked, false);
            }
            buu.CleanUpRoleAccessTable(hdn_Role.Value, "Clientx");
        }


        void btnQuestion_Click(object sender, EventArgs e)
        {
            BasicUserUtilities buu = new BasicUserUtilities(User.Identity.Name, User.Identity.Name);                                //UserName);
            foreach (GridViewRow row in gvQuestion.Rows)
            {
                HiddenField cid = (HiddenField)row.FindControl("hdnIDFieldc");
                decimal ID = 0;
                decimal.TryParse(cid.Value, out ID);
                CheckBox cbAdd = (CheckBox)row.FindControl("cbAddc");
                CheckBox cbSelect = (CheckBox)row.FindControl("cbSelectc");
                CheckBox cbUpdate = (CheckBox)row.FindControl("cbUpdatec");
                CheckBox cbDelete = (CheckBox)row.FindControl("cbDeletec");

                buu.UpdateRoleAccessTable(hdn_Role.Value, "Question", ID, cbSelect.Checked, cbAdd.Checked, cbUpdate.Checked, cbDelete.Checked, false);
            }
            buu.CleanUpRoleAccessTable(hdn_Role.Value, "Question");
        }

        void btnAnswer_Click(object sender, EventArgs e)
        {
            BasicUserUtilities buu = new BasicUserUtilities(User.Identity.Name, User.Identity.Name);                                //UserName);
            foreach (GridViewRow row in gvAnswer.Rows)
            {
                HiddenField cid = (HiddenField)row.FindControl("hdnIDFieldc");
                decimal ID = 0;
                decimal.TryParse(cid.Value, out ID);
                CheckBox cbAdd = (CheckBox)row.FindControl("cbAddc");
                CheckBox cbSelect = (CheckBox)row.FindControl("cbSelectc");
                CheckBox cbUpdate = (CheckBox)row.FindControl("cbUpdatec");
                CheckBox cbDelete = (CheckBox)row.FindControl("cbDeletec");

                buu.UpdateRoleAccessTable(hdn_Role.Value, "Answer", ID, cbSelect.Checked, cbAdd.Checked, cbUpdate.Checked, cbDelete.Checked, false);
            }
            buu.CleanUpRoleAccessTable(hdn_Role.Value, "Answer");
        }

        void btnFunction_Click(object sender, EventArgs e)
        {
            BasicUserUtilities buu = new BasicUserUtilities(User.Identity.Name, User.Identity.Name);                                //UserName);
            foreach (GridViewRow row in gvFunction.Rows)
            {
                HiddenField cid = (HiddenField)row.FindControl("hdnIDFieldc");
                decimal ID = 0;
                decimal.TryParse(cid.Value, out ID);
                CheckBox cbAdd = (CheckBox)row.FindControl("cbAddc");
                CheckBox cbSelect = (CheckBox)row.FindControl("cbSelectc");
                CheckBox cbUpdate = (CheckBox)row.FindControl("cbUpdatec");
                CheckBox cbDelete = (CheckBox)row.FindControl("cbDeletec");

                buu.UpdateRoleAccessTable(hdn_Role.Value, "Function", ID, cbSelect.Checked, cbAdd.Checked, cbUpdate.Checked, cbDelete.Checked, false);
            }
            buu.CleanUpRoleAccessTable(hdn_Role.Value, "Function");
        }



        void BindProjectList()
        {
            ProjectManager pm = new ProjectManager(User.Identity.Name);
            List<Project> pl = pm.GetProjectList();
            drpProjectList.Items.Clear();
            ListItem z = new ListItem("All", "-1");
            drpProjectList.Items.Add(z);
            foreach (Project p in pl)
            {
                ListItem x = new ListItem(p.Name, p.ProjectID.ToString());
                drpProjectList.Items.Add(x);
            }
        }


        //void RoleList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        //{
        //    Label RoleNameLabel = RoleList.Rows[e.RowIndex].FindControl("RoleNameLabel") as Label;
        //    //Roles.DeleteRole(RoleNameLabel.Text, false);
        //    //DisplayRolesInGrid();
        //}

        void RoleList_RowDeleting(string Rolename)
        {
            //Label RoleNameLabel = RoleList.Rows[e.RowIndex].FindControl("RoleNameLabel") as Label;
            //Roles.DeleteRole(RoleNameLabel.Text, false);
            Roles.DeleteRole(Rolename, false);
            DisplayRolesInGrid();
        }

        void CreateRoleButton_Click(object sender, EventArgs e)
        {
            string newRoleName = RoleName.Text.Trim();
            if (newRoleName.Length == 0 || newRoleName.Contains(","))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Roles", "alert('New roles can not be blank or contain a comma!');", true);
            }
            else
            {
                if (!Roles.RoleExists(newRoleName))
                {
                    Roles.CreateRole(newRoleName);
                    DisplayRolesInGrid();
                }
            }
            RoleName.Text = "";
        }

        void RoleList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //string NewPassword = "";
            string tRole = e.CommandArgument.ToString();
            hdn_Role.Value = tRole;
            lblUserName_Process.Text = tRole;
            lblUserName_Project.Text = tRole;
            lblUserName_Client.Text = tRole;
            lblUserName_Question.Text = tRole;
            lblUserName_Answer.Text = tRole;
            lblUserName_Function.Text = tRole;
            lblMenuName.Text = tRole;

            if (e.CommandName.ToUpper() == "DELETE")
            {
                if (e.CommandArgument.ToString().Length > 0)
                {
                    RoleList_RowDeleting(e.CommandArgument.ToString());
                }
            }

            if (e.CommandName.ToUpper() == "ISOLATE")
            {
                string sProjectID = drpProjectList.SelectedItem.Value;
                decimal ProjectID = -1;
                if (decimal.TryParse(sProjectID, out ProjectID) == false) { ProjectID = -1; }
                BasicUserUtilities buu = new BasicUserUtilities(User.Identity.Name, tRole);
                List<MasterTableAcccessList> al = buu.GetMasterRoleAccessList(tRole, "Process", ProjectID);
                gvProcess.DataSource = al;    // (from z in ctx.NextProcessSteps.Where(y => y.Process1.Name.ToUpper() == ProcessLevel).OrderBy(y => y.Sequence) select z);
                gvProcess.DataBind();

                al = buu.GetMasterRoleAccessList(tRole, "Project", ProjectID);
                gvProject.DataSource = al;    // (from z in ctx.NextProcessSteps.Where(y => y.Process1.Name.ToUpper() == ProcessLevel).OrderBy(y => y.Sequence) select z);
                gvProject.DataBind();

                al = buu.GetMasterRoleAccessList(tRole, "Clientx", ProjectID);
                gvClient.DataSource = al;    // (from z in ctx.NextProcessSteps.Where(y => y.Process1.Name.ToUpper() == ProcessLevel).OrderBy(y => y.Sequence) select z);
                gvClient.DataBind();

                al = buu.GetMasterRoleAccessList(tRole, "Question", ProjectID);
                gvQuestion.DataSource = al;    // (from z in ctx.NextProcessSteps.Where(y => y.Process1.Name.ToUpper() == ProcessLevel).OrderBy(y => y.Sequence) select z);
                gvQuestion.DataBind();

                //al = buu.GetMasterRoleAccessList(tRole, "Answer", ProjectID);
                //gvAnswer.DataSource = al;    // (from z in ctx.NextProcessSteps.Where(y => y.Process1.Name.ToUpper() == ProcessLevel).OrderBy(y => y.Sequence) select z);
                //gvAnswer.DataBind(); 

                al = buu.GetMasterRoleAccessList(tRole, "Function", ProjectID);
                gvFunction.DataSource = al;    // (from z in ctx.NextProcessSteps.Where(y => y.Process1.Name.ToUpper() == ProcessLevel).OrderBy(y => y.Sequence) select z);
                gvFunction.DataBind();




                // Get Valid role Menu options.
                RoleMenuAccessManager rm = new RoleMenuAccessManager(User.Identity.Name);
                List<string> ValidOptions = rm.GetData(tRole, User.IsInRole("Admin"));

                LoadMenuData(ValidOptions);
                UserAccessTabs.Visible = true;
            }
        }

 







        //void btnSaveMenuRoles_Click(object sender, EventArgs e)
        //{
        //    List<string> keep = new List<string>();
        //    List<string> drop = new List<string>();
        //    foreach (TreeNode t in tvMenu.Nodes)
        //    {
        //        string Title = t.Text.ToUpper();
        //        string URL = t.Value.ToUpper();
        //        if (URL.Length > 0) { URL = URL.Replace('~', ' ').Trim(); }

        //        if (t.Checked == true) {
        //            keep.Add(Title + "," + URL); }
        //        else { drop.Add(Title + "," + URL); }

        //        if (t.ChildNodes.Count > 0)
        //        {
        //            Rundown(t, keep, drop);
        //        }
        //    }
        //    RoleMenuAccessManager rm = new RoleMenuAccessManager(User.Identity.Name);
        //    rm.SaveData(lblMenuName.Text, keep, drop);

        //}
        //void Rundown(TreeNode nodes, List<string> keep, List<string> drop)
        //{
        //    foreach (TreeNode t in nodes.ChildNodes)
        //    {
        //        string Title = t.Text.ToUpper();
        //        string URL = t.Value.ToUpper();
        //        if (URL.Length > 0) { URL = URL.Replace('~', ' ').Trim(); }

        //        if (t.Checked == true) { keep.Add(Title + "," + URL); }
        //        else { drop.Add(Title + "," + URL); }
        //        if (t.ChildNodes.Count > 0)
        //        {
        //            Rundown(t, keep, drop);
        //        }
        //    }
        //}
        //private void LoadMenuOptions(XElement element, TreeNode node, List<string> tRole)
        //{
        //    string Title = element.Attribute("title").Value.ToUpper();
        //    string URL = element.Attribute("url").Value.ToUpper();
        //    if (URL.Length > 0) { URL = URL.Replace('~', ' ').Trim(); }
        //    TreeNode n1 = new TreeNode(Title, URL);
        //    if (tRole.Contains(Title + "," + URL) == true) { n1.Checked = true; }
        //    //if (URL.Length == 0) { n1.ShowCheckBox = false; }
        //    node.ChildNodes.Add(n1);
        //    foreach (XElement x in element.Elements())
        //    {
        //        if (x.HasElements)
        //        {
        //            LoadMenuOptions(x, n1, tRole);
        //        }
        //        else
        //        {

        //            Title = x.Attribute("title").Value.ToUpper();
        //            URL = x.Attribute("url").Value.ToUpper();
        //            if (URL.Length > 0) { URL = URL.Replace('~', ' ').Trim(); }
        //            TreeNode n = new TreeNode(Title, URL);
        //            if (tRole.Contains(Title + "," + URL) == true) { n.Checked = true; }
        //            //if (URL.Length == 0) { n.ShowCheckBox = false; }


        //            n1.ChildNodes.Add(n);
        //        }
        //    }
        //}

        #region MenuStuff


        private void LoadMenuData(List<string> ValidOptions)
        {

            XElement xelement1 = XElement.Load(Server.MapPath("~/web.sitemap"));
            XElement axis = (from element in xelement1.Elements("siteMapNode")
                             where element.Attribute("title").Value == "Home"
                             select element).Single();

            tvMenu.Nodes.Clear();
            TreeNode n = new TreeNode(axis.Attribute("title").Value.ToUpper(), "");
            n.ShowCheckBox = false;
            tvMenu.Nodes.Add(n);

            foreach (XElement x in axis.Elements())
            {
                LoadMenuOptions(x, n, ValidOptions);
            }
        }


        private void LoadMenuOptions(XElement element, TreeNode node, List<string> tRole)
        {
            CleanURL Clean = new CleanURL();
            string Title = element.Attribute("title").Value.ToUpper();
            string URL = Clean.Clean(element.Attribute("url").Value.ToUpper());

            //if (URL.Length > 0) { URL = URL.Replace('~', ' ').Trim(); }
            TreeNode n1 = new TreeNode(Title, URL);
            if (tRole.Contains(Title + "," + URL) == true) { n1.Checked = true; }
            //if (URL.Length == 0) { n1.ShowCheckBox = false; }
            node.ChildNodes.Add(n1);
            foreach (XElement x in element.Elements())
            {
                if (x.HasElements)
                {
                    LoadMenuOptions(x, n1, tRole);
                }
                else
                {
                    Title = x.Attribute("title").Value.ToUpper();
                    URL = Clean.Clean(x.Attribute("url").Value.ToUpper());
                    //if (URL.Length > 0) { URL = URL.Replace('~', ' ').Trim(); }
                    TreeNode n = new TreeNode(Title, URL);
                    if (tRole.Contains(Title + "," + URL) == true) { n.Checked = true; }
                    //if (URL.Length == 0) { n.ShowCheckBox = false; }
                    n1.ChildNodes.Add(n);
                }
            }
        }
        void btnSaveMenuRoles_Click(object sender, EventArgs e)
        {
            CleanURL Clean = new CleanURL();
            List<string> keep = new List<string>();
            List<string> drop = new List<string>();
            foreach (TreeNode t in tvMenu.Nodes)
            {
                string Title = t.Text.ToUpper();
                string URL = Clean.Clean(t.Value.ToUpper());
                // if (URL.Length > 0) { URL = URL.Replace('~', ' ').Trim(); }

                if (t.Checked == true)
                {
                    keep.Add(Title + "," + URL);
                }
                else { drop.Add(Title + "," + URL); }

                if (t.ChildNodes.Count > 0)
                {
                    Rundown(t, keep, drop);
                }
            }

            //if (chkDownloadPortalApp.Checked == true) { keep.Add("CHKDOWNLOADPORTALAPP" + "," + "CHKDOWNLOADPORTALAPP"); }
            //else { drop.Add("CHKDOWNLOADPORTALAPP" + "," + "CHKDOWNLOADPORTALAPP"); }

            //if (chkDownloadPortal.Checked == true) { keep.Add("CHKDOWNLOADPORTAL" + "," + "CHKDOWNLOADPORTAL"); }
            //else { drop.Add("CHKDOWNLOADPORTAL" + "," + "CHKDOWNLOADPORTAL"); }

            RoleMenuAccessManager rm = new RoleMenuAccessManager(User.Identity.Name);
            rm.SaveData(lblMenuName.Text, keep, drop);
            ScriptManager.RegisterStartupScript(this, GetType(), "Save", "alert('Data Saved');", true);
        }
        void Rundown(TreeNode nodes, List<string> keep, List<string> drop)
        {
            CleanURL Clean = new CleanURL();
            foreach (TreeNode t in nodes.ChildNodes)
            {
                string Title = t.Text.ToUpper();
                string URL = Clean.Clean(t.Value.ToUpper());
                // if (URL.Length > 0) { URL = URL.Replace('~', ' ').Trim(); }

                if (t.Checked == true) { keep.Add(Title + "," + URL); }
                else { drop.Add(Title + "," + URL); }
                if (t.ChildNodes.Count > 0)
                {
                    Rundown(t, keep, drop);
                }
            }
        }
        #endregion


        private void DisplayRolesInGrid()
        {
            RoleList.DataSource = Roles.GetAllRoles();
            RoleList.DataBind();
        }
    }
}