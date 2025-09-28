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
    public partial class RPT_MasterCarrierScanCodeList : System.Web.UI.Page
    {

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
                sID = "";
                Table = "MasterCarrier";
            }


            decimal ID = -1;
            if (decimal.TryParse(sID, out ID) != true) { ID = -1; }


            if (Table.ToUpper() == "MASTERCARRIER")
            {
                pSave.Visible = false;
                pBagTag.Visible = false;
                pClear.Visible = false;
                if (Project == null) { Project = "?"; }
                lProject.Text = "Master Carrier Manufacturer Scan Codes";
                rCarrier.ItemDataBound += new RepeaterItemEventHandler(rCarrier_ItemDataBound);

                QuestionManager qm = new QuestionManager(User.Identity.Name);
                rCarrier.DataSource = qm.ViewMasterCarrierManufacturerLookup_Carriers();    // (from z in ctx.NextProcessSteps.Where(y => y.Process1.Name.ToUpper() == ProcessLevel).OrderBy(y => y.Sequence) select z);
                rCarrier.DataBind();
                return;
            }

            //if (Table.ToUpper() == "QUESTIONS")
            //{
            //    pSave.Visible = false;
            //    pBagTag.Visible = false;
            //    pClear.Visible = false;
            //    if (Project == null) { Project = "?"; }
            //    lProject.Text = "Question: " + Project;
            //    rQuestion.ItemDataBound += new RepeaterItemEventHandler(rQuestion_ItemDataBound);
            //    QuestionManager qm = new QuestionManager(User.Identity.Name);
            //    rQuestion.DataSource = qm.GetReport_QuestionScanList_Question(sID);    // (from z in ctx.NextProcessSteps.Where(y => y.Process1.Name.ToUpper() == ProcessLevel).OrderBy(y => y.Sequence) select z);
            //    rQuestion.DataBind();
            //    return;
            //}

            //if (Table.ToUpper() == "QUESTION")
            //{
            //    pSave.Visible = false;
            //    pBagTag.Visible = false;
            //    pClear.Visible = false;
            //    if (Project == null) { Project = "?"; }
            //    lProject.Text = "Question: " + Project;
            //    rQuestion.ItemDataBound += new RepeaterItemEventHandler(rQuestion_ItemDataBound);
            //    QuestionManager qm = new QuestionManager(User.Identity.Name);
            //    rQuestion.DataSource = qm.GetReport_QuestionScanList_Question(sID);    // (from z in ctx.NextProcessSteps.Where(y => y.Process1.Name.ToUpper() == ProcessLevel).OrderBy(y => y.Sequence) select z);
            //    rQuestion.DataBind();
            //    return;
            //}
            //if (Table.ToUpper() == "PROJECT")
            //{
            //    if (Project == null) { Project = "iPHone"; }
            //    lProject.Text = "Project: " + Project;
            //    rQuestion.ItemDataBound += new RepeaterItemEventHandler(rProject_ItemDataBound);
            //    QuestionManager qm = new QuestionManager(User.Identity.Name);
            //    rQuestion.DataSource = qm.GetReport_ProjectScanList_Question(Project);    // (from z in ctx.NextProcessSteps.Where(y => y.Process1.Name.ToUpper() == ProcessLevel).OrderBy(y => y.Sequence) select z);
            //    rQuestion.DataBind();
            //    return;
            //}
            //if (Table.ToUpper() == "PROCESS")
            //{
            //    //tblHeader.Visible = false;
            //    //rQuestion.Visible = false;
            //    if (Project == null || Project.Length == 0) { Project = "Process"; }
            //    lProject.Text = Project + " Screen Scan List";
            //    rQuestion.ItemDataBound += new RepeaterItemEventHandler(rProcess_ItemDataBound);
            //    QuestionManager qm = new QuestionManager(User.Identity.Name);
            //    rQuestion.DataSource = qm.GetReport_ProcessScanList_Question(ID);    // (from z in ctx.NextProcessSteps.Where(y => y.Process1.Name.ToUpper() == ProcessLevel).OrderBy(y => y.Sequence) select z);
            //    rQuestion.DataBind();
            //    return;
            //}
        }




        void rCarrier_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            ViewMasterCarrierManufacturerLookup_Carrier Carrier = (ViewMasterCarrierManufacturerLookup_Carrier)e.Item.DataItem;
            if (Carrier != null)
            {
                Panel ph = (Panel)e.Item.FindControl("valueHeader");
                Label lQ = (Label)e.Item.FindControl("lCarrier");


                DataList rOp = (DataList)e.Item.FindControl("dlCarrier");
                LinearBarcode bcCarrier = (LinearBarcode)e.Item.FindControl("bcCarrier");
                //ph.Visible = false;
                lQ.Text = "Carrier: " + Carrier.Carrier;
                bcCarrier.DataToEncode = Carrier.CarrierKey;
                //ph.Visible = true;
                rOp.RepeatDirection = RepeatDirection.Horizontal;
                rOp.RepeatColumns = 1;
                rOp.ItemDataBound += new DataListItemEventHandler(rManufacturer_ItemDataBound);

                QuestionManager qm = new QuestionManager(User.Identity.Name);
                rOp.DataSource = qm.ViewMasterCarrierManufacturerLookup_Manufacturer(Carrier.CarrierID);    // (from z in ctx.NextProcessSteps.Where(y => y.Process1.Name.ToUpper() == ProcessLevel).OrderBy(y => y.Sequence) select z);



                rOp.DataBind();
            }
        }

        void rManufacturer_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            ViewMasterCarrierManufacturerLookup_Manufacturer Manufacturer = (ViewMasterCarrierManufacturerLookup_Manufacturer)e.Item.DataItem;
            if (Manufacturer != null)
            {
                //Panel ph = (Panel)e.Item.FindControl("valueHeader");
                Label lO = (Label)e.Item.FindControl("lOption");
                DataList rOp = (DataList)e.Item.FindControl("dlManufacturer");
                LinearBarcode bc = (LinearBarcode)e.Item.FindControl("bcOptionScanCode");
                lO.Text = Manufacturer.Manufacturer;
                //if (Manufacturer.MacroKey.Trim().Length > 0) { lO.Text = "(" + Manufacturer.MacroKey + ") " + Manufacturer.Name + ""; }
                //if (Manufacturer.TYPE == "DD" || Manufacturer.TYPE == "CB" || Manufacturer.TYPE == "RB")
                //{
                if (IncludeComma == true) { bc.DataToEncode = Manufacturer.ManufacturerKey + ","; }
                else { bc.DataToEncode = Manufacturer.ManufacturerKey; }
                //}
                //else
                //{
                //    bc.DataToEncode = "/" + Manufacturer.MacroKey;
                //}


                rOp.RepeatDirection = RepeatDirection.Horizontal;
                rOp.RepeatColumns = 3;
                rOp.ItemDataBound += new DataListItemEventHandler(rModel_ItemDataBound);

                QuestionManager qm = new QuestionManager(User.Identity.Name);
                rOp.DataSource = qm.ViewMasterCarrierManufacturerLookup_Model(Manufacturer.CarrierID, Manufacturer.ManufacturerID);    // (from z in ctx.NextProcessSteps.Where(y => y.Process1.Name.ToUpper() == ProcessLevel).OrderBy(y => y.Sequence) select z);
                rOp.DataBind();
            }
        }


        void rModel_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            ViewMasterCarrierManufacturerLookup_Model Model = (ViewMasterCarrierManufacturerLookup_Model)e.Item.DataItem;
            if (Model != null)
            {
                //Panel ph = (Panel)e.Item.FindControl("valueHeader");
                Label lO = (Label)e.Item.FindControl("lModel");
                LinearBarcode bc = (LinearBarcode)e.Item.FindControl("bcModelOptionScanCode");
                lO.Text = Model.model + "";
                //if (Manufacturer.MacroKey.Trim().Length > 0) { lO.Text = "(" + Manufacturer.MacroKey + ") " + Manufacturer.Name + ""; }
                //if (Manufacturer.TYPE == "DD" || Manufacturer.TYPE == "CB" || Manufacturer.TYPE == "RB")
                //{
                if (IncludeComma == true) { bc.DataToEncode = Model.ModelKey + ","; }
                else { bc.DataToEncode = Model.ModelKey; }
                //}
                //else
                //{
                //    bc.DataToEncode = "/" + Manufacturer.MacroKey;
                //}

            }
        }
    }
}