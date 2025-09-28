using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security; 

using BW_WebApp.DataManagers;


namespace BW_WebApp.Maintenance
{
    public partial class Maint_ErrorReporting : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnClear.Click += new EventHandler(btnClear_Click);
            btnSave.Click += new EventHandler(btnSave_Click);
            txtIMEI.Focus();
            if (IsPostBack == false)
            {
                BindQuestionsToList();
                BindUsersToUserList();
            }
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            string LastIMEI = txtIMEI.Text;

            if (LastIMEI.Length == 0)
            {
                lblMessage.Text = "IMEI Not set. Data Not Saved";
                return;
            }

            ReceiveDetailManager rdm = new ReceiveDetailManager(User.Identity.Name);
            decimal ReceiveDetailID = -1;
            ReceiveDetail RD = rdm.ReceiveDetail(LastIMEI);
            if (RD != null) { ReceiveDetailID = RD.ReceiveDetailID; }

            if (ReceiveDetailID == -1 && hdnForceSave.Value != LastIMEI)
            {
                hdnForceSave.Value = txtIMEI.Text;
                lblMessage.Text = "No Current Version of IMEI found (" + LastIMEI + "), Hit Save again to bypass warning and complete the save.";
                return;
            }

            DateTime issueDate = DateTime.Now;
            if (DateTime.TryParse(txtDate.Text, out issueDate) == false) { issueDate = DateTime.Now; }
            rdm.InsertReceiveDetailErrorReporting(LastIMEI, ReceiveDetailID, drpUserList.SelectedItem.Text, drpERR_Error_Found.SelectedItem.Text, txtERR_Error_Details.Text, issueDate, txtERR_Action_Taken.Text, drpActionTakenBy.SelectedItem.Text);
            Clear();

            lblMessage.Text = "Data Saved (" + LastIMEI + ")";
        }

        void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        void Clear()
        {
            drpERR_Error_Found.SelectedIndex = 0;
            drpUserList.SelectedIndex = 0;
            drpActionTakenBy.SelectedIndex = 0;
            txtERR_Action_Taken.Text = "";
            txtERR_Error_Details.Text = "";
            txtDate.Text = "";
            txtIMEI.Text = "";
            lblMessage.Text = "";
            hdnForceSave.Value = "";
        }

        void BindQuestionsToList()
        {
            QuestionManager qm = new QuestionManager(User.Identity.Name);
            List<Option> ol = qm.GetQuestionOptionList("ERR_Error_Found");
            drpERR_Error_Found.Items.Clear();
            foreach (Option o in ol.OrderBy(x => x.Sequence))
            {
                ListItem x = new ListItem(o.OptionText, o.OptionID.ToString());
                drpERR_Error_Found.Items.Add(x);
            }
        }

        protected void BindUsersToUserList()
        {
            // Get all the user accounts
            MembershipUserCollection users = Membership.GetAllUsers();

            string[] Clients = Roles.GetUsersInRole("Client");

            foreach (MembershipUser o in users)
            {
                if (Clients.Contains(o.UserName) == false)
                {
                    ListItem x = new ListItem(o.UserName, o.UserName);
                    drpUserList.Items.Add(x);

                    ListItem y = new ListItem(o.UserName, o.UserName);
                    drpActionTakenBy.Items.Add(y);
                }
            }
        }
    }
}