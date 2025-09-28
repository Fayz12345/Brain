using System;
using System.Linq;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BW_WebApp.Account
{
    public partial class UsersAndRoles : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UserList.SelectedIndexChanged += new EventHandler(UserList_SelectedIndexChanged);
            RoleList.SelectedIndexChanged += new EventHandler(RoleList_SelectedIndexChanged);
            RolesUserList.RowDeleting += new GridViewDeleteEventHandler(RolesUserList_RowDeleting);
            AddUserToRoleButton.Click += new EventHandler(AddUserToRoleButton_Click);
            btnUnlock.Click += new EventHandler(btnUnlock_Click);
            btnlock.Click += new EventHandler(btnlock_Click);
            btnUnlock.Enabled = false;
            btnlock.Enabled = true;
            lblLockedDate.Text = "";
            if (!Page.IsPostBack)
            {
                // Bind the users and roles
                BindUsersToUserList();
                BindRolesToList();
                CheckRolesForSelectedUser();
                DisplayUsersBelongingToRole();
            }
        }

        void btnlock_Click(object sender, EventArgs e)
        {
            string selectedUserName = UserList.SelectedValue;
            MembershipUser usr = Membership.GetUser(selectedUserName);
            usr.IsApproved = false;
            Membership.UpdateUser(usr);
            ActionStatus.Text = "The user account has been locked.";
            //btnLock.Enabled = true;
            btnUnlock.Enabled = true;
            btnlock.Enabled = false;
            lblLockedDate.Text = DateTime.Now.ToString();
        }


        void btnUnlock_Click(object sender, EventArgs e)
        {
            string selectedUserName = UserList.SelectedValue;
            MembershipUser usr = Membership.GetUser(selectedUserName);
            usr.UnlockUser();
            usr.IsApproved = true;
            Membership.UpdateUser(usr);
            ActionStatus.Text = "The user account has been unlocked.";
            //btnLock.Enabled = true;
            btnUnlock.Enabled = false;
            btnlock.Enabled = true;
            lblLockedDate.Text = "";
        }


        void AddUserToRoleButton_Click(object sender, EventArgs e)
        {
            string selectedRoleName = RoleList.SelectedValue;
            string userNameToAddToRole = UserNameToAddToRole.Text;
            if (userNameToAddToRole.Trim().Length == 0)
            {
                ActionStatus.Text = "You must enter a username in the textbox.";
                return;
            }
            MembershipUser userInfo = Membership.GetUser(userNameToAddToRole);
            if (userInfo == null)
            {
                ActionStatus.Text = string.Format("The user {0} does not exist in the system.", userNameToAddToRole);
                return;
            }
            if (Roles.IsUserInRole(userNameToAddToRole, selectedRoleName))
            {
                ActionStatus.Text = string.Format("User {0} already is a member of role {1}.", userNameToAddToRole, selectedRoleName);
                return;
            }
            Roles.AddUserToRole(userNameToAddToRole, selectedRoleName);
            UserNameToAddToRole.Text = string.Empty;
            CheckRolesForSelectedUser();
            DisplayUsersBelongingToRole();
            ActionStatus.Text = string.Format("User {0} was added to role {1}.", userNameToAddToRole, selectedRoleName);

        }

        void RolesUserList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string selectedRoleName = RoleList.SelectedValue;
            Label UserNameLabel = RolesUserList.Rows[e.RowIndex].FindControl("UserNameLabel") as Label;
            Roles.RemoveUserFromRole(UserNameLabel.Text, selectedRoleName);
            ActionStatus.Text = string.Format("User {0} was removed from role {1}.", UserNameLabel.Text, selectedRoleName);
            CheckRolesForSelectedUser();
            DisplayUsersBelongingToRole();
        }

        void RoleList_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayUsersBelongingToRole();
        }

        protected void RoleCheckBox_CheckChanged(object sender, EventArgs e)
        {
            CheckBox RoleCheckBox = sender as CheckBox;
            string selectedUserName = UserList.SelectedValue;
            string roleName = RoleCheckBox.Text;
            if (RoleCheckBox.Checked)
            {
                Roles.AddUserToRole(selectedUserName, roleName);
                ActionStatus.Text = string.Format("User {0} was added to role {1}.", selectedUserName, roleName);
            }
            else
            {
                Roles.RemoveUserFromRole(selectedUserName, roleName);
                ActionStatus.Text = string.Format("User {0} was removed from role {1}.", selectedUserName, roleName);
            }
            DisplayUsersBelongingToRole();
        }


        protected void UserList_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckRolesForSelectedUser();
        }

        protected void BindUsersToUserList()
        {
            // Get all the user accounts
            MembershipUserCollection users = Membership.GetAllUsers();
            UserList.DataSource = users;
            UserList.DataBind();
        }

        protected void BindRolesToList()
        {
            // Get all of the roles
            string[] roles = Roles.GetAllRoles();
            UsersRoleList.DataSource = roles;
            UsersRoleList.DataBind();

            RoleList.DataSource = roles;
            RoleList.DataBind();
        }

        protected void CheckRolesForSelectedUser()
        {
            // Determin what roles the selected user belongs to
            string selectedUserName = UserList.SelectedValue;
            string[] selectedusersRoles = Roles.GetRolesForUser(selectedUserName);
            MembershipUser usr = Membership.GetUser(selectedUserName);
            ActionStatus.Text = "";
            if (usr.IsLockedOut == true || usr.IsApproved == false)
            {
                btnUnlock.Enabled = true;
                btnlock.Enabled = false;
                lblLockedDate.Text = usr.LastLockoutDate.ToString();
            }
            else
            {
                btnUnlock.Enabled = false;
                btnlock.Enabled = true;
                lblLockedDate.Text = "";
            }

            foreach (RepeaterItem ri in UsersRoleList.Items)
            {
                CheckBox RoleCheckBox = ri.FindControl("RoleCheckBox") as CheckBox;
                if (selectedusersRoles.Contains<string>(RoleCheckBox.Text))
                    RoleCheckBox.Checked = true;
                else
                    RoleCheckBox.Checked = false;
            }
        }

        private void DisplayUsersBelongingToRole()
        {
            string selectedRoleName = RoleList.SelectedValue;
            string[] usersBelongingToRole = Roles.GetUsersInRole(selectedRoleName);
            RolesUserList.DataSource = usersBelongingToRole;
            RolesUserList.DataBind();
        }

    }
}