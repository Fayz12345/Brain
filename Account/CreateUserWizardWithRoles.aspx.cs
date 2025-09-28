using System;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
//using Factory_DataModel;
using BW_WebApp.DataManagers;

namespace BW_WebApp.Account
{
    public partial class CreateUserWizardWithRoles : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RegisterUserWithRoles.ActiveStepChanged += new EventHandler(RegisterUserWithRoles_ActiveStepChanged);
            if (!Page.IsPostBack)
            {
                WizardStep SpecifyRolesStep = RegisterUserWithRoles.FindControl("SpecifyRolesStep") as WizardStep;
                CheckBoxList roleList = SpecifyRolesStep.FindControl("RoleList") as CheckBoxList;
                if (roleList != null)
                {
                    roleList.DataSource = Roles.GetAllRoles();
                    roleList.DataBind();
                }
            }
        }

        void RegisterUserWithRoles_ActiveStepChanged(object sender, EventArgs e)
        {
            if (RegisterUserWithRoles.ActiveStep.Title == "Specify Roles")
            {
                // we need to add the new user to the User Table and assign any default user access stuff.
                //BasicUserUtilities bu = new BasicUserUtilities(User.Identity.Name, RegisterUserWithRoles.UserName, txtLocationList.Text, txtFriendlyName.Text);
                BasicUserUtilities bu = new BasicUserUtilities(User.Identity.Name, RegisterUserWithRoles.UserName, txtLocationList.Text, txtFriendlyName.Text, txtDepartment.Text);
                bu.AddToUserTable();
                bu.AddToUserAccessTable_NoTableAccess();
            }

            if (RegisterUserWithRoles.ActiveStep.Title == "Complete")
            {
                WizardStep SpecifyRolesStep = RegisterUserWithRoles.FindControl("SpecifyRolesStep") as WizardStep;
                CheckBoxList roleList = SpecifyRolesStep.FindControl("RoleList") as CheckBoxList;
                if (roleList != null)
                {
                    foreach (ListItem li in roleList.Items)
                    {
                        if (li.Selected)
                            Roles.AddUserToRole(RegisterUserWithRoles.UserName, li.Text);
                    }
                }
            }
        }

    }
}