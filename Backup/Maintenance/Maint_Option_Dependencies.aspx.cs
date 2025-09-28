using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
//using Factory_DataModel;
using BW_WebApp.DataManagers;

namespace BW_WebApp.Maintenance
{
    public partial class Maint_Option_Dependencies : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            drpSourceQuestion.SelectedIndexChanged += new EventHandler(drpSourceQuestion_SelectedIndexChanged);
            drpTargetQuestion.SelectedIndexChanged += new EventHandler(drpSourceQuestion_SelectedIndexChanged);
            btnSave.Click += new EventHandler(btnSave_Click);

            GridView1.RowDataBound += new GridViewRowEventHandler(GridView1_RowDataBound);
            if (!IsPostBack)
            {
                drpSourceQuestion.Items.Clear();
                drpTargetQuestion.Items.Clear();
                QuestionManager qm = new QuestionManager(User.Identity.Name);
                List<string> types = new List<string> { "RADIALBUTTON", "DROPDOWN" };
                List<Question> ql = qm.GetQuestionsTheseTypes(types);
                foreach (Question q in ql)
                {
                    ListItem x = new ListItem(q.Description, q.QuestionID.ToString());
                    drpSourceQuestion.Items.Add(x);
                }


                types = new List<string> { "RADIALBUTTON", "DROPDOWN", "CHECKBOX" };
                ql.Clear();
                ql = qm.GetQuestionsTheseTypes(types);
                foreach (Question q in ql)
                {
                    ListItem x = new ListItem(q.Description, q.QuestionID.ToString());
                    drpTargetQuestion.Items.Add(x);
                }
                decimal ID = -1;
                if (decimal.TryParse(drpSourceQuestion.SelectedItem.Value, out ID) == false) { ID = -1; }
                //lblSource.Text = drpSourceQuestion.SelectedItem.Text;
                UpdatSourceGrid(ID);
            }
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            AnswerManager am = new AnswerManager(User.Identity.Name);

            decimal SourceID = -1;
            if (decimal.TryParse(drpSourceQuestion.SelectedItem.Value, out SourceID) == false) { SourceID = -1; }
            decimal TargetID = -1;
            if (decimal.TryParse(drpTargetQuestion.SelectedItem.Value, out TargetID) == false) { TargetID = -1; }

            GridView G = GridView1;
            foreach (GridViewRow r in G.Rows)
            {
                if (r.RowType == DataControlRowType.DataRow)
                {
                    CheckBoxList c1 = (CheckBoxList)r.FindControl("checkAnswer");
                    HiddenField HName = (HiddenField)r.FindControl("HiddenName");

                    decimal SourceOptionID = -1;
                    if (decimal.TryParse(HName.Value, out SourceOptionID) == false) { SourceOptionID = -1; }

                    List<decimal> targetIDsOn = new List<decimal>();
                    List<decimal> targetIDsOff = new List<decimal>();
                    foreach (ListItem i in c1.Items)
                    {
                        decimal id = -1;
                        string sid = "";
                        sid = i.Value;
                        if (decimal.TryParse(sid, out id) == false) { id = -1; }
                        if (id > 0 && i.Selected == true)
                        {
                            if (id > 0)
                            {
                                targetIDsOn.Add(id);
                            }
                        }
                        else
                        {
                            targetIDsOff.Add(id);
                        }
                    }
                    am.UpdateDependencies(SourceID, TargetID, SourceOptionID, targetIDsOn, targetIDsOff);
                }
            }

        }


        void drpSourceQuestion_SelectedIndexChanged(object sender, EventArgs e)
        {
            decimal ID = -1;
            if (decimal.TryParse(drpSourceQuestion.SelectedItem.Value, out ID) == false) { ID = -1; }
            UpdatSourceGrid(ID);

        }

        private void UpdatSourceGrid(decimal ID)
        {
            AnswerManager am = new AnswerManager(User.Identity.Name);
            var a = am.GetTheseAnswers(ID);
            GridView1.DataSource = a;
            GridView1.DataBind();
        }


        void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //writeTrace(true);
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // HiddenField HID = (HiddenField)e.Row.FindControl("HiddenID");
                HiddenField HName = (HiddenField)e.Row.FindControl("HiddenName");
                CheckBoxList c1 = (CheckBoxList)e.Row.FindControl("checkAnswer");
                Option rec = (Option)e.Row.DataItem;
                LoadData(rec.OptionID, HName, c1, true);
            }
            //writeTrace(false);
        }
        private void LoadData(decimal SourceOptionID, HiddenField HName, CheckBoxList c1, bool bEnabled)
        {
            //writeTrace(true);
            AnswerManager am = new AnswerManager(User.Identity.Name);
            decimal SourceID = -1;
            if (decimal.TryParse(drpSourceQuestion.SelectedItem.Value, out SourceID) == false) { SourceID = -1; }
            decimal TargetID = -1;
            if (decimal.TryParse(drpTargetQuestion.SelectedItem.Value, out TargetID) == false) { TargetID = -1; }

            List<Option> rec = am.GetTheseAnswers(TargetID);
            List<decimal> dl = am.GetDependencies(SourceID, TargetID, SourceOptionID);

            c1.Items.Clear();
            c1.Visible = false;
            //clsLinqDataContext ctx = new clsLinqDataContext();

            HName.Value = SourceOptionID.ToString(); // am.QuestionType(TargetID).ToUpper();

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
                // KeyData.AddValuePair(RemoveBadCharacters("Z" + x.Text), o.OptionID.ToString());
            }
            c1.RepeatColumns = xSet - 1;
            c1.Enabled = bEnabled;
        }


    }
}