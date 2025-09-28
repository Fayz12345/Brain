using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
//using Factory_DataModel;
using BW_WebApp.DataManagers;

namespace BW_WebApp.Account
{
    public partial class UserBasedAuthorization : System.Web.UI.Page
    {
        clsLog log;

        protected void Page_Load(object sender, EventArgs e)
        {

            log = new clsLog(Server.MapPath("~"), "WebServer_01_Log.txt", User.Identity.Name, System.Configuration.ConfigurationManager.AppSettings["WriteLogUserNameToLog"]);
            if (System.Configuration.ConfigurationManager.AppSettings["WriteLog"].ToUpper() == "TRUE")
            {
                log.writeLogData = true;
            }
            log.LogIt("**** User based Authorization");

            gvClient.RowDataBound += new GridViewRowEventHandler(gvClient_RowDataBound);
            //gvClientLocation.RowDataBound += new GridViewRowEventHandler(gvClient_RowDataBound);
            gvProject.RowDataBound += new GridViewRowEventHandler(gvClient_RowDataBound);
            gvProcess.RowDataBound += new GridViewRowEventHandler(gvClient_RowDataBound);
            gvQuestion.RowDataBound += new GridViewRowEventHandler(gvClient_RowDataBound);
            gvFunction.RowDataBound += new GridViewRowEventHandler(gvFunction_RowDataBound);

            UserGrid.RowEditing += new GridViewEditEventHandler(UserGrid_RowEditing);
            UserGrid.RowCancelingEdit += new GridViewCancelEditEventHandler(UserGrid_RowCancelingEdit);
            UserGrid.RowUpdating += new GridViewUpdateEventHandler(UserGrid_RowUpdating);
            UserGrid.RowDataBound += new GridViewRowEventHandler(UserGrid_RowDataBound);
            UserGrid.RowDeleting += new GridViewDeleteEventHandler(UserGrid_RowDeleting);                                                 //+= new GridViewDeleteEventHandler(UserGrid_RowDeleting);
            UserGrid.RowCommand += new GridViewCommandEventHandler(UserGrid_RowCommand);
            drpProjectList.SelectedIndexChanged += new EventHandler(drpProjectList_SelectedIndexChanged);


            btnBack.Click += new EventHandler(btnBack_Click);
            btnClient.Click += new EventHandler(btnClient_Click);
            btnClientLocation.Click += new EventHandler(btnClientLocation_Click);
            btnProcess.Click += new EventHandler(btnProcess_Click);
            btnQuestion.Click += new EventHandler(btnQuestion_Click);
            //btnBinLocation.Click += new EventHandler(btnBinLocation_Click);
            btnProject.Click += new EventHandler(btnProject_Click);
            btnFunction.Click += new EventHandler(btnFunction_Click);
            btnRefresh.Click += new EventHandler(btnRefresh_Click);
            btnCleanUserList.Click += new EventHandler(btnCleanUserList_Click);

            if (!IsPostBack)
            {
                ClientManager cm = new ClientManager(User.Identity.Name);

                drpUserFilter.DataValueField = "Name";
                drpUserFilter.DataTextField = "CompanyName";
                drpUserFilter.DataSource = cm.DropDownUserFilterList("", "", "", "").OrderBy(x => x.CompanyName);
                drpUserFilter.DataBind();
                drpUserFilter.SelectedIndex = 0;

                BindProjectList();
                BindUserGrid();
            }
        }

        void btnBack_Click(object sender, EventArgs e)
        {
            SetUserGridView(true);
        }

        void btnCleanUserList_Click(object sender, EventArgs e)
        {
            // Get list of users
            List<string> aList = new List<string>();
            MembershipUserCollection allUsers = Membership.GetAllUsers();
            // loop through list and delete.
            foreach (MembershipUser u in allUsers)
            {
                if (u.UserName.ToUpper() != "ADMIN" && u.UserName.ToUpper() != "JMCCOMB")
                {
                    aList.Add(u.UserName);
                }
            }
            foreach (string u in aList)
            {
                DeleteUser(u);
            }

            // Refresh the Grid
            UserGrid.EditIndex = -1;
            BindUserGrid();
        }

        void DeleteUser(string UserName)
        {
            Membership.DeleteUser(UserName);
            BasicUserUtilities buu = new BasicUserUtilities(User.Identity.Name, UserName);
            buu.DeleteUser();
        }

        void UserGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (!Page.IsValid)
                return;
            string UserName = UserGrid.DataKeys[e.RowIndex].Value.ToString();
            DeleteUser(UserName);
            UserGrid.EditIndex = -1;
            BindUserGrid();
        }


        void UserGrid_RowDeleting(string UserName)
        {
            DeleteUser(UserName);
            UserGrid.EditIndex = -1;
            BindUserGrid();
        }

        void UserGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                MembershipUserCollection_Item user = ((MembershipUserCollection_Item)e.Row.DataItem);
                if (user.UserName.Length > 0)
                {
                    AjaxControlToolkit.ConfirmButtonExtender chk = (AjaxControlToolkit.ConfirmButtonExtender)e.Row.FindControl("ConfirmButtonExtender2");
                    if (chk != null) { chk.ConfirmText = "Are you sure you wish to delete " + user.UserName + " ?"; }

                    chk = (AjaxControlToolkit.ConfirmButtonExtender)e.Row.FindControl("ConfirmButtonExtenderReset");
                    if (chk != null) { chk.ConfirmText = "Are you sure you want to Reset the password for " + user.UserName + " ?"; }

                }
            }
        }

        void btnRefresh_Click(object sender, EventArgs e)
        {
            SetUserGridView(true);
            BindUserGrid();
        }



        void SetIsolateView(bool On)
        {
            SetUserGridView(!On);
        }
        void SetUserGridView(bool On)
        {
            TurnRefreshPannel(On);
            TurnUserGridPannel(On);
            TurnUserAccessPannel(!On);
        }
        void TurnRefreshPannel(bool State)
        {
            drpUserFilter.Visible = State;
            btnRefresh.Visible = State;
            //btnCleanUserList.Visible = State;
        }
        void TurnUserGridPannel(bool State)
        {
            UserGrid.Visible = State;
        }
        void TurnUserAccessPannel(bool State)
        {
            btnBack.Visible = State;
            UserAccessTabs.Visible = State;
            lbluserPicked.Visible = State;
            lbluserPicked.Text = "";
        }

        void gvFunction_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                MasterTableAcccessList grda = ((MasterTableAcccessList)e.Row.DataItem);
                if (grda.ID < -10)
                {
                    CheckBox chk = (CheckBox)e.Row.FindControl("cbAddc");
                    if (chk != null) { chk.Enabled = false; }
                    chk = (CheckBox)e.Row.FindControl("cbSelectc");
                    if (chk != null) { chk.Enabled = false; }
                    chk = (CheckBox)e.Row.FindControl("cbUpdatec");
                    if (chk != null) { chk.Enabled = false; }
                    chk = (CheckBox)e.Row.FindControl("cbDeletec");
                    if (chk != null) { chk.Enabled = false; }
                }
            }
        }

        void gvClient_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                MasterTableAcccessList grda = ((MasterTableAcccessList)e.Row.DataItem);

                //log.LogIt("Grid Updated");

                if (grda.ID < -10)
                {
                    CheckBox chk = (CheckBox)e.Row.FindControl("cbAddc");
                    if (chk != null) { chk.Enabled = false; }
                    chk = (CheckBox)e.Row.FindControl("cbSelectc");
                    if (chk != null) { chk.Enabled = false; }
                    chk = (CheckBox)e.Row.FindControl("cbUpdatec");
                    if (chk != null) { chk.Enabled = false; }
                    chk = (CheckBox)e.Row.FindControl("cbDeletec");
                    if (chk != null) { chk.Enabled = false; }
                }
            }
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

            BasicUserUtilities buu = new BasicUserUtilities(User.Identity.Name, UserName);
            List<MasterTableAcccessList> al = buu.GetMasterTableAccessList("Process", ProjectID);
            gvProcess.DataSource = al;    // (from z in ctx.NextProcessSteps.Where(y => y.Process1.Name.ToUpper() == ProcessLevel).OrderBy(y => y.Sequence) select z);
            gvProcess.DataBind();

        }


        void btnQuestion_Click(object sender, EventArgs e)
        {
            string UserName = UserNameToUpdate.Value;
            BasicUserUtilities buu = new BasicUserUtilities(User.Identity.Name, UserName);
            foreach (GridViewRow row in gvQuestion.Rows)
            {
                HiddenField cid = (HiddenField)row.FindControl("hdnIDFieldq");
                decimal ID = 0;
                decimal.TryParse(cid.Value, out ID);
                if (ID > -2)
                {
                    CheckBox cbAdd = (CheckBox)row.FindControl("cbAddc");
                    CheckBox cbSelect = (CheckBox)row.FindControl("cbSelectc");
                    CheckBox cbUpdate = (CheckBox)row.FindControl("cbUpdatec");
                    CheckBox cbDelete = (CheckBox)row.FindControl("cbDeletec");
                    buu.UpdateUserAccessTable("Question", ID, cbSelect.Checked, cbAdd.Checked, cbUpdate.Checked, cbDelete.Checked, false);
                }
            }
            // We need to clean up a bit for any set to zero.
            buu.CleanUpUserAccessTable("Question");
            // UserAccessTabs.Visible = false;
        }

        void btnFunction_Click(object sender, EventArgs e)
        {
            string UserName = UserNameToUpdate.Value;
            BasicUserUtilities buu = new BasicUserUtilities(User.Identity.Name, UserName);
            foreach (GridViewRow row in gvFunction.Rows)
            {
                HiddenField cid = (HiddenField)row.FindControl("hdnIDFieldq");
                decimal ID = 0;
                decimal.TryParse(cid.Value, out ID);
                if (ID > -2)
                {
                    CheckBox cbAdd = (CheckBox)row.FindControl("cbAddc");
                    CheckBox cbSelect = (CheckBox)row.FindControl("cbSelectc");
                    CheckBox cbUpdate = (CheckBox)row.FindControl("cbUpdatec");
                    CheckBox cbDelete = (CheckBox)row.FindControl("cbDeletec");
                    buu.UpdateUserAccessTable("Function", ID, cbSelect.Checked, cbAdd.Checked, cbUpdate.Checked, cbDelete.Checked, false);
                }
            }
            // We need to clean up a bit for any set to zero.
            buu.CleanUpUserAccessTable("Function");
            ScriptManager.RegisterStartupScript(this, GetType(), "dataSaved", "alert('Data Updated!');", true);

            // UserAccessTabs.Visible = false;
        }

        void btnProject_Click(object sender, EventArgs e)
        {
            string UserName = UserNameToUpdate.Value;
            BasicUserUtilities buu = new BasicUserUtilities(User.Identity.Name, UserName);
            foreach (GridViewRow row in gvProject.Rows)
            {
                HiddenField cid = (HiddenField)row.FindControl("hdnIDFieldp");
                decimal ID = 0;
                decimal.TryParse(cid.Value, out ID);
                if (ID > -2)
                {
                    CheckBox cbAdd = (CheckBox)row.FindControl("cbAddc");
                    CheckBox cbSelect = (CheckBox)row.FindControl("cbSelectc");
                    CheckBox cbUpdate = (CheckBox)row.FindControl("cbUpdatec");
                    CheckBox cbDelete = (CheckBox)row.FindControl("cbDeletec");

                    buu.UpdateUserAccessTable("Project", ID, cbSelect.Checked, cbAdd.Checked, cbUpdate.Checked, cbDelete.Checked, false);
                }
            }
            // UserAccessTabs.Visible = false;
            buu.CleanUpUserAccessTable("Project");
            ScriptManager.RegisterStartupScript(this, GetType(), "dataSaved", "alert('Data Updated!');", true);

        }
        void btnProcess_Click(object sender, EventArgs e)
        {
            string UserName = UserNameToUpdate.Value;
            BasicUserUtilities buu = new BasicUserUtilities(User.Identity.Name, UserName);
            foreach (GridViewRow row in gvProcess.Rows)
            {
                HiddenField cid = (HiddenField)row.FindControl("hdnIDFieldp");
                decimal ID = 0;
                decimal.TryParse(cid.Value, out ID);
                if (ID > -2)
                {
                    CheckBox cbAdd = (CheckBox)row.FindControl("cbAddc");
                    CheckBox cbSelect = (CheckBox)row.FindControl("cbSelectc");
                    CheckBox cbUpdate = (CheckBox)row.FindControl("cbUpdatec");
                    CheckBox cbDelete = (CheckBox)row.FindControl("cbDeletec");
                    buu.UpdateUserAccessTable("Process", ID, cbSelect.Checked, cbAdd.Checked, cbUpdate.Checked, cbDelete.Checked, false);
                }
            }
            // UserAccessTabs.Visible = false;
            buu.CleanUpUserAccessTable("Process");
            ScriptManager.RegisterStartupScript(this, GetType(), "dataSaved", "alert('Data Updated!');", true);

        }
        void btnClient_Click(object sender, EventArgs e)
        {
            string UserName = UserNameToUpdate.Value;
            log.LogIt("btnClientSave - :" + UserName);
            BasicUserUtilities buu = new BasicUserUtilities(User.Identity.Name, UserName);
            log.LogIt("btnClientSave - :Loop through grid");
            foreach (GridViewRow row in gvClient.Rows)
            {
                HiddenField cid = (HiddenField)row.FindControl("hdnIDFieldc");
                decimal ID = 0;
                decimal.TryParse(cid.Value, out ID);
                if (ID > -2)
                {
                    CheckBox cbAdd = (CheckBox)row.FindControl("cbAddc");
                    CheckBox cbSelect = (CheckBox)row.FindControl("cbSelectc");
                    CheckBox cbUpdate = (CheckBox)row.FindControl("cbUpdatec");
                    CheckBox cbDelete = (CheckBox)row.FindControl("cbDeletec");
                    buu.UpdateUserAccessTable("Clientx", ID, cbSelect.Checked, cbAdd.Checked, cbUpdate.Checked, cbDelete.Checked, false);
                }
            }
            // UserAccessTabs.Visible = false;
            log.LogIt("btnClientSave - :Loop through grid Done");
            buu.CleanUpUserAccessTable("Clientx");
            log.LogIt("btnClientSave - :CleanUpUserAccessTable");
            ScriptManager.RegisterStartupScript(this, GetType(), "dataSaved", "alert('Data Updated!');", true);

        }
        void btnClientLocation_Click(object sender, EventArgs e)
        {
            string UserName = UserNameToUpdate.Value;
            log.LogIt("btnClientLocationSave - :" + UserName);
            BasicUserUtilities buu = new BasicUserUtilities(User.Identity.Name, UserName);
            buu.SetClientLocationRestrictions(txtLocationList.Text);

            //foreach (GridViewRow row in gvClientLocation.Rows)
            //{
            //    HiddenField cid = (HiddenField)row.FindControl("hdnIDFieldc");
            //    decimal ID = 0;
            //    decimal.TryParse(cid.Value, out ID);
            //    if (ID > -2)
            //    {
            //        CheckBox cbAdd = (CheckBox)row.FindControl("cbAddc");
            //        CheckBox cbSelect = (CheckBox)row.FindControl("cbSelectc");
            //        CheckBox cbUpdate = (CheckBox)row.FindControl("cbUpdatec");
            //        CheckBox cbDelete = (CheckBox)row.FindControl("cbDeletec");
            //        buu.UpdateUserAccessTable("Client", ID, cbSelect.Checked, cbAdd.Checked, cbUpdate.Checked, cbDelete.Checked, false);
            //    }
            //}
            //// UserAccessTabs.Visible = false;
            //log.LogIt("btnClientLocationSave - :Loop through grid Done");
            //buu.CleanUpUserAccessTable("Client");
            //log.LogIt("btnClientLocationSave - :CleanUpUserAccessTable");

            ScriptManager.RegisterStartupScript(this, GetType(), "dataSaved", "alert('Data Updated!');", true);

        }

        void UserGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string NewPassword = "";
            Int32 index = 0;
            if (Int32.TryParse(e.CommandArgument.ToString(), out index) == true)
            {
                if (e.CommandName.ToUpper() == "XDELETE")
                {
                    string UserName = UserGrid.DataKeys[index].Value.ToString();
                    UserGrid_RowDeleting(UserName);
                }
                if (e.CommandName.ToUpper() == "RESET")
                {
                    string UserName = UserGrid.DataKeys[index].Value.ToString();
                    MembershipUser mu = Membership.GetUser(UserName);
                    mu.UnlockUser();
                    // bool x = Membership.EnablePasswordReset;
                    NewPassword = "NewPass999";                    //Membership.GeneratePassword(10, 0);      // "ThisIsNew001";


                    if (mu.ChangePassword(Membership.GetUser(UserName).ResetPassword(), NewPassword) == true)
                    {
                        UserAccessControl uac = new UserAccessControl(UserName);
                        if (uac != null) { uac.ForcePasswordReset(); }
                        ScriptManager.RegisterStartupScript(this, GetType(), "Password", "alert('" + string.Format("Password Reset for user: {0} to: {1}", UserName, NewPassword) + "');", true);
                    }

                }
                if (e.CommandName.ToUpper() == "ISOLATE")
                {
                    string UserName = UserGrid.DataKeys[index].Value.ToString();

                    log.LogIt("ISOLATE - :" + UserName);

                    UserNameToUpdate.Value = UserName;
                    lblUserName_Client.Text = UserName;
                    lblUserName_ClientLocation.Text = UserName;
                    lblUserName_Process.Text = UserName;
                    lblUserName_Project.Text = UserName;
                    lblUserName_Question.Text = UserName;
                    //lblUserName_BinLocation.Text = UserName;
                    string sProjectID = drpProjectList.SelectedItem.Value;
                    decimal ProjectID = -1;
                    if (decimal.TryParse(sProjectID, out ProjectID) == false) { ProjectID = -1; }

                    BasicUserUtilities buu = new BasicUserUtilities(User.Identity.Name, UserName);

                    txtLocationList.Text = buu.GetClientLocationRestrictions;

                    List<MasterTableAcccessList> al = buu.GetMasterTableAccessList("Clientx", ProjectID);
                    gvClient.DataSource = al;    // (from z in ctx.NextProcessSteps.Where(y => y.Process1.Name.ToUpper() == ProcessLevel).OrderBy(y => y.Sequence) select z);
                    gvClient.DataBind();
                    log.LogIt("ISOLATE - :Clientx");

                    al = buu.GetMasterTableAccessList("Process", ProjectID);
                    gvProcess.DataSource = al;    // (from z in ctx.NextProcessSteps.Where(y => y.Process1.Name.ToUpper() == ProcessLevel).OrderBy(y => y.Sequence) select z);
                    gvProcess.DataBind();
                    log.LogIt("ISOLATE - :Process");

                    al = buu.GetMasterTableAccessList("Project", ProjectID);
                    gvProject.DataSource = al;    // (from z in ctx.NextProcessSteps.Where(y => y.Process1.Name.ToUpper() == ProcessLevel).OrderBy(y => y.Sequence) select z);
                    gvProject.DataBind();
                    log.LogIt("ISOLATE - :Project");

                    al = buu.GetMasterTableAccessList("Question", ProjectID);
                    gvQuestion.DataSource = al;    // (from z in ctx.NextProcessSteps.Where(y => y.Process1.Name.ToUpper() == ProcessLevel).OrderBy(y => y.Sequence) select z);
                    gvQuestion.DataBind();
                    log.LogIt("ISOLATE - :Question");

                    al = buu.GetMasterTableAccessList("Function", ProjectID);
                    gvFunction.DataSource = al;    // (from z in ctx.NextProcessSteps.Where(y => y.Process1.Name.ToUpper() == ProcessLevel).OrderBy(y => y.Sequence) select z);
                    gvFunction.DataBind();
                    log.LogIt("ISOLATE - :Function");

                    //al = buu.GetMasterTableAccessList("BinLocation", ProjectID);
                    //gvBinlocation.DataSource = al;    // (from z in ctx.NextProcessSteps.Where(y => y.Process1.Name.ToUpper() == ProcessLevel).OrderBy(y => y.Sequence) select z);
                    //gvBinlocation.DataBind();

                    SetIsolateView(true);
                    //UserAccessTabs.Visible = true;
                    //lbluserPicked.Visible = true;
                    //UserAccessTabs.Visible = false;
                    //UserGrid.Visible = false;
                    lbluserPicked.Text = "ISOLATE - :" + UserName;


                }
            }
        }


        //void UserGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        //{
        //    string UserName = UserGrid.DataKeys[e.RowIndex].Value.ToString();
        //    Membership.DeleteUser(UserName);
        //    BasicUserUtilities buu = new BasicUserUtilities(User.Identity.Name, UserName);
        //    buu.DeleteUser();
        //    UserGrid.EditIndex = -1;
        //    BindUserGrid();
        //}

        void UserGrid_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            if (!Page.IsValid)
                return;
            string UserName = UserGrid.DataKeys[e.RowIndex].Value.ToString();
            TextBox FriendlyName = UserGrid.Rows[e.RowIndex].FindControl("FriendlyName") as TextBox;
            TextBox Dept = UserGrid.Rows[e.RowIndex].FindControl("DepartmentName") as TextBox;
            TextBox EmailTextBox = UserGrid.Rows[e.RowIndex].FindControl("Email") as TextBox;
            TextBox CommentTextBox = UserGrid.Rows[e.RowIndex].FindControl("Comment") as TextBox;
            MembershipUser UserInfo = Membership.GetUser(UserName);
            UserInfo.Email = EmailTextBox.Text.Trim();
            UserInfo.Comment = CommentTextBox.Text.Trim();
            Membership.UpdateUser(UserInfo);
            BasicUserUtilities buu = new BasicUserUtilities(User.Identity.Name);
            buu.UpdateThisUserFriendlyName(UserName, FriendlyName.Text);
            buu.UpdateThisUserDepartment(UserName, Dept.Text);
            UserGrid.EditIndex = -1;
            BindUserGrid();
        }

        void UserGrid_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            UserGrid.EditIndex = -1;
            BindUserGrid();
        }

        void UserGrid_RowEditing(object sender, GridViewEditEventArgs e)
        {
            UserGrid.EditIndex = e.NewEditIndex;
            BindUserGrid();
        }

        private void BindUserGrid()
        {
            MembershipUserCollection allUsers = Membership.GetAllUsers();
            MembershipUserCollection_List alist = GetUserList();                                 //new MembershipUserCollection_List(allUsers, User.Identity.Name);
            string Filter = "";                 // "Bell,Goldiex";
            Filter = drpUserFilter.SelectedItem.Value;

            UserGrid.DataSource = alist.FilterClientLocationRestrictions(Filter);

            UserGrid.DataBind();
        }

        private MembershipUserCollection_List GetUserList()
        {
            MembershipUserCollection allUsers = Membership.GetAllUsers();
            MembershipUserCollection_List alist = new MembershipUserCollection_List(allUsers, User.Identity.Name);
            return alist;

        }
    }
}