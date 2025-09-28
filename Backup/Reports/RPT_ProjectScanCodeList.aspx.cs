using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BW_WebApp.BarcodeUtils;
//using Factory_DataModel;
using BW_WebApp.DataManagers;
using IDAutomation.LinearServerControl;

namespace BW_WebApp.Reports
{
    public partial class RPT_ProjectScanCodeList : System.Web.UI.Page
    {
        //string LastValue = "";
        bool IncludeComma = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            //string ProjectTag = Request.QueryString.Get("PROJECTTAG");
            string sID = Request.QueryString.Get("ID");
            string Table = Request.QueryString.Get("Table");
            string Project = Request.QueryString.Get("PROJECT");
            string ShowComma = Request.QueryString.Get("ShowComma");
            IncludeComma = (ShowComma == "1");
            if (sID == null)
            {
                sID = ",27,26,13,14,3,4,9,24,";
                Table = "Questions";
            }


            decimal ID = -1;
            if (decimal.TryParse(sID, out ID) != true) { ID = -1; }


            if (Table.ToUpper() == "QUESTIONS")
            {
                pSave.Visible = false;
                pBagTag.Visible = false;
                pClear.Visible = false;
                if (Project == null) { Project = "?"; }
                lProject.Text = "Question: " + Project;
                rQuestion.ItemDataBound += new RepeaterItemEventHandler(rQuestion_ItemDataBound);
                QuestionManager qm = new QuestionManager(User.Identity.Name);
                rQuestion.DataSource = qm.GetReport_QuestionScanList_Question(sID);    // (from z in ctx.NextProcessSteps.Where(y => y.Process1.Name.ToUpper() == ProcessLevel).OrderBy(y => y.Sequence) select z);
                rQuestion.DataBind();
                return;
            }

            if (Table.ToUpper() == "QUESTION")
            {
                pSave.Visible = false;
                pBagTag.Visible = false;
                pClear.Visible = false;
                if (Project == null) { Project = "?"; }
                lProject.Text = "Question: " + Project;
                rQuestion.ItemDataBound += new RepeaterItemEventHandler(rQuestion_ItemDataBound);
                QuestionManager qm = new QuestionManager(User.Identity.Name);
                rQuestion.DataSource = qm.GetReport_QuestionScanList_Question(sID);    // (from z in ctx.NextProcessSteps.Where(y => y.Process1.Name.ToUpper() == ProcessLevel).OrderBy(y => y.Sequence) select z);
                rQuestion.DataBind();
                return;
            }


            if (Table.ToUpper() == "PROJECT")
            {
                if (Project == null) { Project = "iPHone"; }
                lProject.Text = "Project: " + Project;
                rQuestion.ItemDataBound += new RepeaterItemEventHandler(rProject_ItemDataBound);
                QuestionManager qm = new QuestionManager(User.Identity.Name);
                rQuestion.DataSource = qm.GetReport_ProjectScanList_Question(Project);    // (from z in ctx.NextProcessSteps.Where(y => y.Process1.Name.ToUpper() == ProcessLevel).OrderBy(y => y.Sequence) select z);
                rQuestion.DataBind();
                return;
            }
            if (Table.ToUpper() == "PROCESS")
            {
                //tblHeader.Visible = false;
                //rQuestion.Visible = false;
                if (Project == null || Project.Length == 0) { Project = "Process"; }
                lProject.Text = Project + " Screen Scan List";

                rQuestion.ItemDataBound += new RepeaterItemEventHandler(rProcess_ItemDataBound);
                QuestionManager qm = new QuestionManager(User.Identity.Name);
                rQuestion.DataSource = qm.GetReport_ProcessScanList_Question(ID);    // (from z in ctx.NextProcessSteps.Where(y => y.Process1.Name.ToUpper() == ProcessLevel).OrderBy(y => y.Sequence) select z);
                rQuestion.DataBind();

                return;
            }

        }


        void rProcess_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            GetReport_ProcessScanList_QuestionResult ns = (GetReport_ProcessScanList_QuestionResult)e.Item.DataItem;
            if (ns != null)
            {
                lProject.Text = ns.Process + " Screen Scan List";
                Panel ph = (Panel)e.Item.FindControl("valueHeader");
                Label lQ = (Label)e.Item.FindControl("lQuestion");
                DataList rOp = (DataList)e.Item.FindControl("dList");
                ph.Visible = false;
                lQ.Text = ns.Name;
                if (ns.Type == "Checkbox") { lQ.Text += "+"; }
                if (ns.Type != "Checkbox" && ns.Type != "DropDown" && ns.Type != "RadialButton") { lQ.Text = "/" + lQ.Text; }
                ph.Visible = true;
                rOp.RepeatDirection = RepeatDirection.Horizontal;
                rOp.RepeatColumns = 3;
                rOp.ItemDataBound += new DataListItemEventHandler(rOp_ItemDataBound);

                QuestionManager qm = new QuestionManager(User.Identity.Name);
                rOp.DataSource = qm.GetReport_ProjectScanList_QuestionOption(ns.QuestionID);    // (from z in ctx.NextProcessSteps.Where(y => y.Process1.Name.ToUpper() == ProcessLevel).OrderBy(y => y.Sequence) select z);
                rOp.DataBind();
            }
        }

        void rQuestion_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            GetReport_QuestionScanList_QuestionResult ns = (GetReport_QuestionScanList_QuestionResult)e.Item.DataItem;
            if (ns != null)
            {
                Panel ph = (Panel)e.Item.FindControl("valueHeader");
                Label lQ = (Label)e.Item.FindControl("lQuestion");
                DataList rOp = (DataList)e.Item.FindControl("dList");
                ph.Visible = false;
                lQ.Text = ns.Name + " ***********";
                ph.Visible = true;
                rOp.RepeatDirection = RepeatDirection.Horizontal;
                rOp.RepeatColumns = 3;
                rOp.ItemDataBound += new DataListItemEventHandler(rOp_ItemDataBound);

                QuestionManager qm = new QuestionManager(User.Identity.Name);
                rOp.DataSource = qm.GetReport_ProjectScanList_QuestionOption(ns.QuestionID);    // (from z in ctx.NextProcessSteps.Where(y => y.Process1.Name.ToUpper() == ProcessLevel).OrderBy(y => y.Sequence) select z);
                rOp.DataBind();
            }
        }



        void rProject_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            GetReport_ProjectScanList_QuestionResult ns = (GetReport_ProjectScanList_QuestionResult)e.Item.DataItem;
            if (ns != null)
            {
                Panel ph = (Panel)e.Item.FindControl("valueHeader");
                Label lQ = (Label)e.Item.FindControl("lQuestion");
                DataList rOp = (DataList)e.Item.FindControl("dList");
                ph.Visible = false;
                lQ.Text = ns.Name + " ***********";
                ph.Visible = true;
                rOp.RepeatDirection = RepeatDirection.Horizontal;
                rOp.RepeatColumns = 5;
                rOp.ItemDataBound += new DataListItemEventHandler(rOp_ItemDataBound);

                QuestionManager qm = new QuestionManager(User.Identity.Name);
                rOp.DataSource = qm.GetReport_ProjectScanList_QuestionOption(ns.QuestionID);    // (from z in ctx.NextProcessSteps.Where(y => y.Process1.Name.ToUpper() == ProcessLevel).OrderBy(y => y.Sequence) select z);
                rOp.DataBind();
            }
        }
        void rOp_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            GetReport_ProjectScanList_QuestionOptionResult ns = (GetReport_ProjectScanList_QuestionOptionResult)e.Item.DataItem;
            if (ns != null)
            {
                //Panel ph = (Panel)e.Item.FindControl("valueHeader");
                Label lO = (Label)e.Item.FindControl("lOption");
                LinearBarcode bc = (LinearBarcode)e.Item.FindControl("bcOptionScanCode");
                lO.Text = ns.Name + "";
                if (ns.MacroKey.Trim().Length > 0) { lO.Text = "(" + ns.MacroKey + ") " + ns.Name + ""; }
                if (ns.TYPE == "DD" || ns.TYPE == "CB" || ns.TYPE == "RB")
                {
                    if (IncludeComma == true) { bc.DataToEncode = ns.ScanKey + ","; }
                    else { bc.DataToEncode = ns.ScanKey; }
                }
                else
                {
                    bc.DataToEncode = "/" + ns.MacroKey;
                }
            }
        }
    }
}