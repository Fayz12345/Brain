using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
//using Factory_DataModel;
using BW_WebApp.DataManagers;

namespace BW_WebApp.Maintenance
{
    public partial class Maint_ClientRestrictions : System.Web.UI.Page
    {
        private string blank = "&nbsp;";

        protected void Page_Load(object sender, EventArgs e)
        {


            btnUpdateARestriction.Click += new EventHandler(btnUpdateARestriction_Click);
            btnUpdateQRestriction.Click += new EventHandler(btnUpdateQRestriction_Click);
            grdAnswers.RowDataBound += new GridViewRowEventHandler(grdAnswers_RowDataBound);
            drpQuestion.SelectedIndexChanged += new EventHandler(drpQuestion_SelectedIndexChanged);
            drpProject.SelectedIndexChanged += new EventHandler(drpProject_SelectedIndexChanged);


            if (IsPostBack == false)
            {
                lblClientTest_01.Text = "Question/Answer Restrictions";
                string zID = Request.QueryString.Get("ID");
                hdnClientID.Value = zID;
                decimal ID = -1;
                if (decimal.TryParse(zID, out ID) == false) { ID = -1; };

                ClientManager cm = new ClientManager(User.Identity.Name);
                Client c = cm.GetClient(ID);
                if (c != null)
                {
                    lblClientTest_01.Text = "Question/Answer Restrictions for:" + c.CompanyName;
                }
                UpdateProjectTabs();
                UpdateQuestionGrid();
                UpdateQuestionTabs();

                UpdateClientQuestionRestrictionGrid();
                UpdateClientAnswerRestrictionGrid();

            }
        }







        #region QuestionAnswerRestrictions



        void grdAnswers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // HiddenField HID = (HiddenField)e.Row.FindControl("HiddenID");
                HiddenField HName = (HiddenField)e.Row.FindControl("HiddenName");
                CheckBoxList c1 = (CheckBoxList)e.Row.FindControl("checkAnswer");
                Question rec = (Question)e.Row.DataItem;
                LoadData(rec.QuestionID, HName, c1, true);
            }
        }



        private void LoadData(decimal QuestionID, HiddenField HName, CheckBoxList c1, bool bEnabled)
        {
            AnswerManager am = new AnswerManager(User.Identity.Name);
            decimal ClientID = decimal.Parse(hdnClientID.Value);
            List<Option> rec = am.GetTheseAnswers(QuestionID);
            List<decimal> dl = am.GetClientAnswerRestrictionList(ClientID, QuestionID);

            c1.Items.Clear();
            c1.Visible = false;
            //clsLinqDataContext ctx = new clsLinqDataContext();

            HName.Value = QuestionID.ToString(); // am.QuestionType(TargetID).ToUpper();

            int xmLength = 0;
            int xCount = 0;
            int xSet = 7;
            c1.Visible = true;
            foreach (Option o in rec.OrderBy(x => x.Sequence))
            {
                ListItem x = new ListItem(o.OptionText, o.OptionID.ToString());
                if (o.MacroKey != null && o.MacroKey.Trim().Length > 0)
                    x.Text += "." + o.MacroKey;
                xCount += 1;
                xmLength += x.Text.Length;
                if (xmLength > 100)
                {
                    if (xCount < xSet) { xSet = xCount; }
                    xCount = 0;
                    xmLength = 0;
                }
                if (dl.Contains(o.OptionID))
                {
                    x.Selected = true;
                }
                c1.Items.Add(x);
                c1.Items[c1.Items.Count - 1].Attributes.Add("someValue", o.OptionID.ToString());
            }
            c1.RepeatColumns = xSet - 1;
            c1.Enabled = bEnabled;
        }





        void btnUpdateARestriction_Click(object sender, EventArgs e)
        {
            List<PairIDValue> Keys = new List<PairIDValue>();
            // Get ClientID
            decimal ClientID = decimal.Parse(hdnClientID.Value);
            // Get QuestionID
            decimal QuestionID = decimal.Parse(drpQuestion.SelectedValue.ToString());

            GridView G = grdAnswers;
            foreach (GridViewRow r in G.Rows)
            {
                if (r.RowType == DataControlRowType.DataRow)
                {
                    CheckBoxList c1 = (CheckBoxList)r.FindControl("checkAnswer");
                    // HiddenField HName = (HiddenField)r.FindControl("HiddenName");
                    foreach (ListItem i in c1.Items)
                    {
                        decimal id = -1;
                        string sid = "";
                        sid = i.Value;
                        if (decimal.TryParse(sid, out id) == false) { id = -1; }
                        if (id > 0 && i.Selected == true) { Keys.Add(new PairIDValue { ID = id, Desc = "1" }); }
                        else { Keys.Add(new PairIDValue { ID = id, Desc = "0" }); }
                    }
                    //am.UpdateDependencies(SourceID, TargetID, SourceOptionID, targetIDsOn, targetIDsOff);
                    if (ClientID > 0)
                    {
                        ClientManager cm = new ClientManager(User.Identity.Name);
                        cm.UpdateClientAnswerRestrictions(ClientID, QuestionID, Keys);
                    }
                }
            }
        }
        void btnUpdateQRestriction_Click(object sender, EventArgs e)
        {
            List<PairIDValue> Keys = new List<PairIDValue>();
            // Get ClientID
            decimal xID = decimal.Parse(hdnClientID.Value);
            // Get Project
            decimal PID = decimal.Parse(drpProject.SelectedValue.ToString());
            // Get all Questions for Update
            foreach (GridViewRow r in grdQuestions.Rows)
            {
                decimal qID = -1;

                string sID = (r.Cells[0].Text == blank ? "" : r.Cells[0].Text);
                if (decimal.TryParse(sID, out qID) == false) { qID = -1; }

                CheckBox CB = (CheckBox)r.FindControl("chkThisQuestion");
                if (CB != null && qID > 0)
                {
                    Keys.Add(new PairIDValue { ID = qID, Desc = (CB.Checked == true ? "1" : "0") });
                }
            }

            if (xID > 0)
            {
                ClientManager cm = new ClientManager(User.Identity.Name);
                cm.UpdateClientRestrictions(xID, PID, Keys);
            }
        }



        void drpProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateClientQuestionRestrictionGrid();
        }
        void drpQuestion_SelectedIndexChanged(object sender, EventArgs e)
        {

            UpdateClientAnswerRestrictionGrid();
        }
        protected void UpdateQuestionTabs()
        {
            QuestionManager qm = new QuestionManager(User.Identity.Name);
            List<string> types = new List<string> { "RADIALBUTTON", "DROPDOWN", "CHECKBOX" };
            List<Question> ql = qm.GetQuestionsTheseTypes(types);
            drpQuestion.Items.Clear();
            foreach (Question q in ql)
            {
                ListItem x = new ListItem(q.Description + "(" + q.Name + ")", q.QuestionID.ToString());
                drpQuestion.Items.Add(x);
            }
            drpQuestion.SelectedIndex = 0;
        }
        protected void UpdateProjectTabs()
        {
            ProjectManager pm = new ProjectManager(User.Identity.Name);
            var Projs = pm.GetProjectList();             // pm.GetMasterProjectList();
            drpProject.Items.Clear();
            ListItem z = new ListItem("All", "-1");
            drpProject.Items.Add(z);
            foreach (Project p in Projs)
            {
                ListItem x = new ListItem(p.Name, p.ProjectID.ToString());
                drpProject.Items.Add(x);
            }
            drpProject.SelectedIndex = 0;
        }
        protected void LoadQuestionAnswers(decimal QuestionID)
        {
            QuestionManager qm = new QuestionManager(User.Identity.Name);
            grdAnswers.DataSource = qm.GetQuestionsDefered().Where(x => x.QuestionID == QuestionID);
            grdAnswers.DataBind();
        }
        protected void UpdateQuestionGrid()
        {
            QuestionManager qm = new QuestionManager(User.Identity.Name);
            grdQuestions.DataSource = qm.GetQuestionsDefered();
            grdQuestions.DataBind();
        }
        protected void UpdateClientAnswerRestrictionGrid()
        {
            decimal xID = decimal.Parse(hdnClientID.Value);
            // Get Project
            decimal PID = decimal.Parse(drpQuestion.SelectedValue.ToString());
            // Get all Questions for Update

            LoadQuestionAnswers(PID);

            ClientManager cm = new ClientManager(User.Identity.Name);
            List<PairIDValue> RA = cm.ClientAnswerRestrictionList(xID, PID);

            ////////////////////////////////
            foreach (GridViewRow r in grdAnswers.Rows)
            {
                decimal qID = -1;

                string sID = (r.Cells[0].Text == blank ? "" : r.Cells[0].Text);
                if (decimal.TryParse(sID, out qID) == false) { qID = -1; }

                CheckBox CB = (CheckBox)r.FindControl("chkThisQuestion");
                if (CB != null && qID > 0)
                {
                    PairIDValue p = RA.FirstOrDefault(x => x.ID == qID);
                    if (p == null)
                    {
                        CB.Checked = false;
                    }
                    else
                    {
                        CB.Checked = true;
                    }
                }
            }
            ////////////////////////////////////
        }
        protected void UpdateClientQuestionRestrictionGrid()
        {

            decimal xID = decimal.Parse(hdnClientID.Value);
            // Get Project
            decimal PID = decimal.Parse(drpProject.SelectedValue.ToString());
            // Get all Questions for Update
            ClientManager cm = new ClientManager(User.Identity.Name);
            List<PairIDValue> RQ = cm.ClientRestrictionList(xID, PID);
            try
            {
                foreach (GridViewRow r in grdQuestions.Rows)
                {
                    decimal qID = -1;

                    string sID = (r.Cells[0].Text == blank ? "" : r.Cells[0].Text);
                    if (decimal.TryParse(sID, out qID) == false) { qID = -1; }

                    CheckBox CB = (CheckBox)r.FindControl("chkThisQuestion");
                    if (CB != null && qID > 0)
                    {

                        CB.Checked = false;
                        PairIDValue p = RQ.FirstOrDefault(x => x.ID == qID);
                        if (p == null)
                        {

                        }
                        else
                        {
                            CB.Checked = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                //throw;
            }

        }

        #endregion







    }
}