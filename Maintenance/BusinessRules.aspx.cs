using System;
using System.Linq;
using System.Web.UI.WebControls;
//using Factory_DataModel;
using BW_WebApp.DataManagers;

namespace BW_WebApp.Maintenance
{
    public partial class BusinessRules : System.Web.UI.Page
    {
        clsLog log;

        protected void Page_Load(object sender, EventArgs e)
        {


            log = new clsLog(Server.MapPath("~"), "WebServer_01_Log.txt", User.Identity.Name, System.Configuration.ConfigurationManager.AppSettings["WriteLogUserNameToLog"]);
            if (System.Configuration.ConfigurationManager.AppSettings["WriteLog"].ToUpper() == "TRUE")
            {
                log.writeLogData = true;
            }
            log.LogIt("**** Maint Question Started");

            GridView4.RowDataBound += new GridViewRowEventHandler(gd1_RowDataBound);
            grdPriorConditions.RowDataBound += new GridViewRowEventHandler(gd1_RowDataBound);
            grdResults.RowDataBound += new GridViewRowEventHandler(gd1_RowDataBound);

            rdlEventType.SelectedIndexChanged += new EventHandler(rdlEventType_SelectedIndexChanged);

            btnRefresh1.Click += new EventHandler(btnRefresh1_Click);
            btnRefreshP.Click += new EventHandler(btnRefreshP_Click);
            btnRefreshR.Click += new EventHandler(btnRefreshR_Click);
            btnHide.Click += new EventHandler(btnHide_Click);
            btnUnhide.Click += new EventHandler(btnUnhide_Click);

            btnHideP.Click += new EventHandler(btnHideP_Click);
            btnUnhideP.Click += new EventHandler(btnUnhideP_Click);


            btnHideR.Click += new EventHandler(btnHideR_Click);
            btnUnHideR.Click += new EventHandler(btnUnhideR_Click);


            //chkHide    
            if (!IsPostBack)
            {


                QuestionManager qm = new QuestionManager(User.Identity.Name);
                clsLinqDataContext ctx = qm.GetDataContext(User.Identity.Name);


                //drpClient.DataValueField = "ClientID";
                //drpClient.DataTextField = "CompanyName";

                drpProcess.DataValueField = "ProcessID";
                drpProcess.DataTextField = "Name";


                drpPriorConditionProcess.DataValueField = "ProcessID";
                drpPriorConditionProcess.DataTextField = "Name";

                //drpClientLocation.DataValueField = "ClientLocationID";
                //drpClientLocation.DataTextField = "CompanyName";

                drpModel.DataValueField = "OptionID";
                drpModel.DataTextField = "DisplayText";

                drpProject.DataValueField = "ProjectID";
                drpProject.DataTextField = "Name";


                //drpClient.DataSource = (from x in ctx.Clients.OrderBy(y => y.CompanyName) select new { x.ClientID, x.CompanyName });
                //drpClientLocation.DataSource = (from x in ctx.ClientLocations.OrderBy(y => y.CompanyName) select new { x.ClientLocationID, CompanyName = "(" + x.ScanKey + ") " + x.CompanyName  });
                drpProcess.DataSource = (from x in ctx.Processes.OrderBy(y => y.Sequence) where x.Name != "Save" select new { x.ProcessID, x.Name });
                drpPriorConditionProcess.DataSource = (from x in ctx.Processes.OrderBy(y => y.Sequence) where x.Name != "Save" select new { x.ProcessID, x.Name });
                drpProject.DataSource = (from x in ctx.Projects.OrderBy(y => y.Name) where x.Name != "Save" select new { x.ProjectID, x.Name });

                //    drpModel.DataSource = (from x in ctx.QuestionTypes.OrderBy(y => y.Type) select new { x.QuestionTypeID, x.Type });
                //drpClient.DataBind();
                drpProcess.DataBind();
                drpPriorConditionProcess.DataBind();
                //drpClientLocation.DataBind();
                drpProject.DataBind();
                //    drpModel.DataBind();
            }


        }

        void rdlEventType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //tabEvent.HeaderText = "Event:" + rdlEventType.SelectedItem.Text;
            //TabContainer1.ActiveTab = TabConditions;
        }

        void btnUnhide_Click(object sender, EventArgs e)
        {
            Hide(GridView4.Rows, false);
        }
        void btnHide_Click(object sender, EventArgs e)
        {
            Hide(GridView4.Rows, true);
        }

        void btnUnhideP_Click(object sender, EventArgs e)
        {
            Hide(grdPriorConditions.Rows, false);
        }
        void btnHideP_Click(object sender, EventArgs e)
        {
            Hide(grdPriorConditions.Rows, true);
        }

        void btnUnhideR_Click(object sender, EventArgs e)
        {
            Hide(grdResults.Rows, false);
        }
        void btnHideR_Click(object sender, EventArgs e)
        {
            Hide(grdResults.Rows, true);
        }

        void Hide(GridViewRowCollection gridRows, bool Hide)
        {
            foreach (GridViewRow row in gridRows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox important = (CheckBox)row.FindControl("chkSelected");
                    if (important != null && important.Checked == true)
                    {
                        row.Visible = Hide == true ? false : true;
                    }
                }
            }
        }









        void btnRefresh1_Click(object sender, EventArgs e)
        {
            LoadQuestions();
            LoadConditions();
        }
        private void LoadQuestions()
        {
            BusinessRulesManager qm = new BusinessRulesManager(User.Identity.Name);
            clsLinqDataContext ctx = qm.GetDataContext(User.Identity.Name);
            GridView4.DataSource = qm.GetDataQuestions(ctx, drpProcess.SelectedItem.Text, drpProject.SelectedItem.Text);       // (from x in ctx.Questions.OrderBy(y => y.Sequence) select x);
            GridView4.DataBind();
            log.LogIt("Bind GridView4");
        }


        private void LoadConditions()
        {
            BusinessRulesManager qm = new BusinessRulesManager(User.Identity.Name);
            clsLinqDataContext ctx = qm.GetDataContext(User.Identity.Name);
            grdConditionSegments.DataSource = qm.GetDataQuestions(ctx, drpProcess.SelectedItem.Text, drpProject.SelectedItem.Text);       // (from x in ctx.Questions.OrderBy(y => y.Sequence) select x);
            grdConditionSegments.DataBind();
            log.LogIt("Bind GridView4");


        }

        void btnRefreshR_Click(object sender, EventArgs e)
        {
            LoadResults();
        }
        private void LoadResults()
        {
            BusinessRulesManager qm = new BusinessRulesManager(User.Identity.Name);
            clsLinqDataContext ctx = qm.GetDataContext(User.Identity.Name);
            grdResults.DataSource = qm.GetDataResults(ctx, drpProcess.SelectedItem.Text, drpProject.SelectedItem.Text);       // (from x in ctx.Questions.OrderBy(y => y.Sequence) select x);
            grdResults.DataBind();
            log.LogIt("Bind grdResults");
        }
        void btnRefreshP_Click(object sender, EventArgs e)
        {
            LoadPriorConditions();
        }
        private void LoadPriorConditions()
        {
            BusinessRulesManager qm = new BusinessRulesManager(User.Identity.Name);
            clsLinqDataContext ctx = qm.GetDataContext(User.Identity.Name);
            grdPriorConditions.DataSource = qm.GetDataPriorConditions(ctx, drpPriorConditionProcess.SelectedItem.Text, drpProject.SelectedItem.Text);       // (from x in ctx.Questions.OrderBy(y => y.Sequence) select x);
            grdPriorConditions.DataBind();
            log.LogIt("Bind grdPriorConditions");
        }




        void gd1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            ////writeTrace(true);
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //log.LogIt("gd1_RowDataBound");
                string ValidString = "";
                HiddenField HID = (HiddenField)e.Row.FindControl("HiddenID");
                //HiddenField isManditory = (HiddenField)e.Row.FindControl("isManditory");
                HiddenField HName = (HiddenField)e.Row.FindControl("HiddenName");
                Question rec = (Question)e.Row.DataItem;
                //isManditory.Value = rec.isManditory.ToString();
                decimal ID = rec.QuestionID;
                Label l1 = (Label)e.Row.FindControl("Description");


                CheckBoxList c1 = (CheckBoxList)e.Row.FindControl("checkAnswer");
                //MultiSelectionDropDown c1 = (MultiSelectionDropDown)e.Row.FindControl("checkAnswer");

                TextBox tn = (TextBox)e.Row.FindControl("NumericAnswer");
                TextBox Currencyn = (TextBox)e.Row.FindControl("CurrencyAnswer");

                TextBox t1 = (TextBox)e.Row.FindControl("TextAnswer");
                RadioButtonList R1 = (RadioButtonList)e.Row.FindControl("RadioAnswer");
                TextBox Cal = (TextBox)e.Row.FindControl("CalAnswer");



                TextBox n3 = (TextBox)e.Row.FindControl("Num3Digit");
                TextBox t20 = (TextBox)e.Row.FindControl("Text20Digit");

                TextBox t3 = (TextBox)e.Row.FindControl("Text3Digit");
                TextBox t10 = (TextBox)e.Row.FindControl("Text10Digit");
                TextBox t18 = (TextBox)e.Row.FindControl("Text18Digit");
                TextBox t50 = (TextBox)e.Row.FindControl("Text50Digit");

                //|| QuestionType == "Text3Digit"
                //|| QuestionType == "Text10Digit"
                //|| QuestionType == "Text18Digit"
                //|| QuestionType == "Text50Digit"


                DropDownList drp = (DropDownList)e.Row.FindControl("drpAnswer");
                try
                {
                    ValidString = LoadData(rec, HName, HID, l1, c1, t1, tn, Currencyn, R1, Cal, drp, n3, t20, t3, t10, t18, t50, true);
                }
                catch (Exception ex)
                {
                    log.LogIt("gd1_RowDataBound:" + ex.Message);
                }
                //if (rec.isManditory == true && ValidString.Length > 0)
                //{
                //    hdnManditoryFields.Value = hdnManditoryFields.Value + (hdnManditoryFields.Value.Length == 0 ? "" : ",") + ValidString;
                //}
            }
            ////writeTrace(false);
        }

        private string LoadData(Question Quest, HiddenField HName, HiddenField HID, Label l1, CheckBoxList c1, TextBox t1, TextBox tn, TextBox Currencyn, RadioButtonList R1, TextBox Cal, DropDownList D1, TextBox n3, TextBox t20,
 TextBox t3, TextBox t10, TextBox t18, TextBox t50, bool bEnabled)
        {
            //log.LogIt("LoadData started");
            string ValidString = l1.ClientID + ":";              // rec.QuestionID.ToString() + ":"; 
            decimal ID = Quest.QuestionID;
            R1.Items.Clear();
            c1.Items.Clear();
            tn.Visible = false;
            c1.Visible = false;
            t1.Visible = false;
            Currencyn.Visible = false;
            R1.Visible = false;
            Cal.Visible = false;
            D1.Visible = false;
            n3.Visible = false;
            t20.Visible = false;
            t3.Visible = false;
            t10.Visible = false;
            t18.Visible = false;
            t50.Visible = false;


            if (hdnIsProcessReadOnly.Value == "1") { bEnabled = false; }
            Cal.Text = DateTime.Now.ToShortDateString();
            string xy = DateTime.Now.TimeOfDay.ToString();
            t1.Text = "";
            l1.Text = Quest.Description + ":";
            HID.Value = Quest.QuestionID.ToString();
            HName.Value = Quest.QuestionType.Type.ToUpper();
            log.LogIt("LoadData Question:" + Quest.Description + "(" + Quest.QuestionType.Type + ")");
            if (bEnabled == true)
            {
                if (Quest.Name == "Carrier") { hdnCarrierID.Value = D1.ClientID; }
                if (Quest.Name == "Manufacturer") { hdnManufacturerID.Value = D1.ClientID; }
                if (Quest.Name == "Model") { hdnModelID.Value = D1.ClientID; }
                if (Quest.Name == "Colour") { hdnColourID.Value = D1.ClientID; }
                #region Grab Bin specific Stuff
                //if (Quest.Name == "Bin")
                //{
                //    switch (Quest.QuestionType.Type.ToUpper())
                //    {
                //        case "DROPDOWN":
                //            hdnBinID.Value = D1.ClientID;
                //            break;
                //        case "RADIALBUTTON":
                //            hdnBinID.Value = R1.ClientID;
                //            break;
                //        case "CALC":
                //            hdnBinID.Value = t1.ClientID;
                //            break;
                //        case "NUMERIC":
                //            hdnBinID.Value = tn.ClientID;
                //            break;
                //        case "CURRENCY":
                //            hdnBinID.Value = Currencyn.ClientID;
                //            break;
                //        case "READONLY":
                //            hdnBinID.Value = t1.ClientID;
                //            break;
                //        case "KEYBOARD":
                //            hdnBinID.Value = t1.ClientID;
                //            break;
                //        case "NUM3DIGIT":
                //            hdnBinID.Value = n3.ClientID;
                //            break;
                //        case "TEXT20DIGIT":
                //            hdnBinID.Value = t20.ClientID;
                //            break;
                //        case "TEXT3DIGIT":
                //            hdnBinID.Value = t3.ClientID;
                //            break;
                //        case "TEXT10DIGIT":
                //            hdnBinID.Value = t10.ClientID;
                //            break;
                //        case "TEXT18DIGIT":
                //            hdnBinID.Value = t18.ClientID;
                //            break;
                //        case "TEXT50DIGIT":
                //            hdnBinID.Value = t50.ClientID;
                //            break;
                //        case "CALENDAR":
                //            hdnBinID.Value = Cal.ClientID;
                //            break;
                //        case "CHECKBOX":
                //            hdnBinID.Value = c1.ClientID;
                //            break;
                //    }
                //}
                //#endregion
                //#region Grab "Lab Destination" specific Stuff
                //if (Quest.Name == "Lab Destination")
                //{
                //    switch (Quest.QuestionType.Type.ToUpper())
                //    {
                //        case "DROPDOWN":
                //            hdnLabDestinationID.Value = D1.ClientID;
                //            break;
                //        case "RADIALBUTTON":
                //            hdnLabDestinationID.Value = R1.ClientID;
                //            break;
                //        case "CALC":
                //            hdnLabDestinationID.Value = t1.ClientID;
                //            break;
                //        case "NUMERIC":
                //            hdnLabDestinationID.Value = tn.ClientID;
                //            break;
                //        case "CURRENCY":
                //            hdnLabDestinationID.Value = Currencyn.ClientID;
                //            break;
                //        case "READONLY":
                //            hdnLabDestinationID.Value = t1.ClientID;
                //            break;
                //        case "KEYBOARD":
                //            hdnLabDestinationID.Value = t1.ClientID;
                //            break;
                //        case "NUM3DIGIT":
                //            hdnLabDestinationID.Value = n3.ClientID;
                //            break;
                //        case "TEXT20DIGIT":
                //            hdnLabDestinationID.Value = t20.ClientID;
                //            break;
                //        case "TEXT3DIGIT":
                //            hdnLabDestinationID.Value = t3.ClientID;
                //            break;
                //        case "TEXT10DIGIT":
                //            hdnLabDestinationID.Value = t10.ClientID;
                //            break;
                //        case "TEXT18DIGIT":
                //            hdnLabDestinationID.Value = t18.ClientID;
                //            break;
                //        case "TEXT50DIGIT":
                //            hdnLabDestinationID.Value = t50.ClientID;
                //            break;
                //        case "CALENDAR":
                //            hdnLabDestinationID.Value = Cal.ClientID;
                //            break;
                //        case "CHECKBOX":
                //            hdnLabDestinationID.Value = c1.ClientID;
                //            break;
                //    }
                //}
                #endregion
            }
            Option ox = new Option();
            string PigionHole = "";
            PigionHole = Quest.QuestionType.Type.ToUpper();
            // we want to display them all as check boxes.
            PigionHole = "CHECKBOX";
            switch (PigionHole)
            {
                #region Calc
                case "CALC":
                    t1.Visible = true;
                    t1.Enabled = false;
                    t1.ToolTip = Quest.HelpText;
                    t1.Attributes["onMouseOver"] = "return ShowTooTip('" + t1.ClientID + "', true);";
                    t1.Attributes["onMouseOut"] = "return ShowTooTip('" + t1.ClientID + "', false);";
                    ox = Quest.Options.FirstOrDefault();
                    if (ox != null)
                    {
                        t1.Attributes.Add("someValue", ox.OptionID.ToString());
                        ValidString += "TX_" + ox.OptionID.ToString() + " ";
                    }
                    //if (bEnabled == true)
                    //{
                    hdnQuestionIDList.Value += "," + Quest.QuestionID.ToString();
                    hdnQuestionClientIDList.Value += "," + t1.ClientID;

                    //}
                    break;
                #endregion
                #region Numeric
                case "NUMERIC":
                    tn.Visible = true;
                    tn.ToolTip = Quest.HelpText;
                    tn.Attributes["onMouseOver"] = "return ShowTooTip('" + tn.ClientID + "', true);";
                    tn.Attributes["onMouseOut"] = "return ShowTooTip('" + tn.ClientID + "', false);";
                    ox = Quest.Options.FirstOrDefault();
                    if (ox != null)
                    {
                        tn.Attributes.Add("someValue", ox.OptionID.ToString());
                        ValidString += "TX_" + ox.OptionID.ToString() + " ";
                    }
                    tn.Enabled = bEnabled;
                    if (Quest.isReadOnly != null && Quest.isReadOnly == true) { tn.Enabled = false; }
                    //if (bEnabled == true)
                    //{
                    hdnQuestionIDList.Value += "," + Quest.QuestionID.ToString();
                    hdnQuestionClientIDList.Value += "," + tn.ClientID;
                    //}
                    break;
                #endregion
                #region Currency
                case "CURRENCY":
                    Currencyn.Visible = true;
                    Currencyn.ToolTip = Quest.HelpText;
                    Currencyn.Attributes["onMouseOver"] = "return ShowTooTip('" + Currencyn.ClientID + "', true);";
                    Currencyn.Attributes["onMouseOut"] = "return ShowTooTip('" + Currencyn.ClientID + "', false);";
                    ox = Quest.Options.FirstOrDefault();
                    if (ox != null)
                    {
                        Currencyn.Attributes.Add("someValue", ox.OptionID.ToString());
                        ValidString += "TX_" + ox.OptionID.ToString() + " ";
                    }
                    Currencyn.Enabled = bEnabled;
                    if (Quest.isReadOnly != null && Quest.isReadOnly == true) { Currencyn.Enabled = false; }
                    //if (bEnabled == true)
                    //{
                    hdnQuestionIDList.Value += "," + Quest.QuestionID.ToString();
                    hdnQuestionClientIDList.Value += "," + Currencyn.ClientID;
                    //}
                    break;
                #endregion
                #region ReadOnly
                case "READONLY":
                    t1.Visible = true;
                    t1.ToolTip = Quest.HelpText;
                    t1.Attributes["onMouseOver"] = "return ShowTooTip('" + t1.ClientID + "', true);";
                    t1.Attributes["onMouseOut"] = "return ShowTooTip('" + t1.ClientID + "', false);";
                    ox = Quest.Options.FirstOrDefault();
                    //if (Quest.Name.Length > 5 && Quest.Name.Substring(0, 5).ToUpper() == "PART ")
                    //{
                    //    hdnPartNumberIDs.Value += ox.OptionID.ToString() + ",";
                    //    //imgShowPartList.Visible = true;
                    //}
                    if (ox != null)
                    {
                        t1.Attributes.Add("someValue", ox.OptionID.ToString());
                        ValidString += "TX_" + ox.OptionID.ToString() + " ";
                    }
                    t1.Enabled = false;                //bEnabled;
                    if (Quest.isReadOnly != null && Quest.isReadOnly == true) { t1.Enabled = false; }
                    //if (bEnabled == true)
                    //{
                    hdnQuestionIDList.Value += "," + Quest.QuestionID.ToString();
                    hdnQuestionClientIDList.Value += "," + t1.ClientID;
                    break;
                #endregion
                #region Keyboard
                case "KEYBOARD":
                    t1.Visible = true;
                    t1.ToolTip = Quest.HelpText;
                    t1.Attributes["onMouseOver"] = "return ShowTooTip('" + t1.ClientID + "', true);";
                    t1.Attributes["onMouseOut"] = "return ShowTooTip('" + t1.ClientID + "', false);";
                    ox = Quest.Options.FirstOrDefault();

                    //if (Quest.Name.Length > 5 && Quest.Name.Substring(0, 5).ToUpper() == "PART ")
                    //{
                    //    hdnPartNumberIDs.Value += ox.OptionID.ToString() + ",";
                    //    imgShowPartList.Visible = true;
                    //    imgShowPartReturnList.Visible = true;
                    //}


                    if (ox != null)
                    {
                        t1.Attributes.Add("someValue", ox.OptionID.ToString());
                        ValidString += "TX_" + ox.OptionID.ToString() + " ";
                    }
                    t1.Enabled = bEnabled;
                    if (Quest.isReadOnly != null && Quest.isReadOnly == true) { t1.Enabled = false; }
                    //if (bEnabled == true)
                    //{
                    hdnQuestionIDList.Value += "," + Quest.QuestionID.ToString();
                    hdnQuestionClientIDList.Value += "," + t1.ClientID;

                    //// Turn on the wrench to allow picking of parts.
                    //if (Quest.Description.ToUpper() == "PART #")
                    //{
                    //    imgShowPartList.Visible = true;
                    //}

                    //}
                    break;
                #endregion
                //////////////////////////////////////
                #region Num30Digit
                case "NUM3DIGIT":
                    n3.Visible = true;
                    n3.ToolTip = Quest.HelpText;
                    n3.Attributes["onMouseOver"] = "return ShowTooTip('" + n3.ClientID + "', true);";
                    n3.Attributes["onMouseOut"] = "return ShowTooTip('" + n3.ClientID + "', false);";
                    ox = Quest.Options.FirstOrDefault();

                    //if (Quest.Name.Length > 5 && Quest.Name.Substring(0, 5).ToUpper() == "PART ")
                    //{
                    //    hdnPartNumberIDs.Value += ox.OptionID.ToString() + ",";
                    //}


                    if (ox != null)
                    {
                        n3.Attributes.Add("someValue", ox.OptionID.ToString());
                        ValidString += "TX_" + ox.OptionID.ToString() + " ";
                    }
                    n3.Enabled = bEnabled;
                    if (Quest.isReadOnly != null && Quest.isReadOnly == true) { n3.Enabled = false; }
                    //if (bEnabled == true)
                    //{
                    hdnQuestionIDList.Value += "," + Quest.QuestionID.ToString();
                    hdnQuestionClientIDList.Value += "," + n3.ClientID;
                    //}
                    break;
                #endregion
                #region Text20Digit
                case "TEXT20DIGIT":
                    t20.Visible = true;
                    t20.ToolTip = Quest.HelpText;
                    t20.Attributes["onMouseOver"] = "return ShowTooTip('" + t20.ClientID + "', true);";
                    t20.Attributes["onMouseOut"] = "return ShowTooTip('" + t20.ClientID + "', false);";
                    ox = Quest.Options.FirstOrDefault();

                    //if (Quest.Name.Length > 5 && Quest.Name.Substring(0, 5).ToUpper() == "PART ")
                    //{
                    //    hdnPartNumberIDs.Value += ox.OptionID.ToString() + ",";
                    //}


                    if (ox != null)
                    {
                        t20.Attributes.Add("someValue", ox.OptionID.ToString());
                        ValidString += "TX_" + ox.OptionID.ToString() + " ";
                    }
                    t20.Enabled = bEnabled;
                    if (Quest.isReadOnly != null && Quest.isReadOnly == true) { t20.Enabled = false; }
                    //if (bEnabled == true)
                    //{
                    hdnQuestionIDList.Value += "," + Quest.QuestionID.ToString();
                    hdnQuestionClientIDList.Value += "," + t20.ClientID;
                    //}
                    break;
                #endregion
                #region Text30Digit
                case "TEXT3DIGIT":
                    t3.Visible = true;
                    t3.ToolTip = Quest.HelpText;
                    t3.Attributes["onMouseOver"] = "return ShowTooTip('" + t3.ClientID + "', true);";
                    t3.Attributes["onMouseOut"] = "return ShowTooTip('" + t3.ClientID + "', false);";
                    ox = Quest.Options.FirstOrDefault();

                    //if (Quest.Name.Length > 5 && Quest.Name.Substring(0, 5).ToUpper() == "PART ")
                    //{
                    //    hdnPartNumberIDs.Value += ox.OptionID.ToString() + ",";
                    //}


                    if (ox != null)
                    {
                        t3.Attributes.Add("someValue", ox.OptionID.ToString());
                        ValidString += "TX_" + ox.OptionID.ToString() + " ";
                    }
                    t3.Enabled = bEnabled;
                    if (Quest.isReadOnly != null && Quest.isReadOnly == true) { t3.Enabled = false; }
                    hdnQuestionIDList.Value += "," + Quest.QuestionID.ToString();
                    hdnQuestionClientIDList.Value += "," + t3.ClientID;
                    break;
                #endregion
                #region Test10Digit
                case "TEXT10DIGIT":
                    t10.Visible = true;
                    t10.ToolTip = Quest.HelpText;
                    t10.Attributes["onMouseOver"] = "return ShowTooTip('" + t10.ClientID + "', true);";
                    t10.Attributes["onMouseOut"] = "return ShowTooTip('" + t10.ClientID + "', false);";
                    ox = Quest.Options.FirstOrDefault();

                    //if (Quest.Name.Length > 5 && Quest.Name.Substring(0, 5).ToUpper() == "PART ")
                    //{
                    //    hdnPartNumberIDs.Value += ox.OptionID.ToString() + ",";
                    //}
                    if (ox != null)
                    {
                        t10.Attributes.Add("someValue", ox.OptionID.ToString());
                        ValidString += "TX_" + ox.OptionID.ToString() + " ";
                    }
                    t10.Enabled = bEnabled;
                    if (Quest.isReadOnly != null && Quest.isReadOnly == true) { t10.Enabled = false; }
                    hdnQuestionIDList.Value += "," + Quest.QuestionID.ToString();
                    hdnQuestionClientIDList.Value += "," + t10.ClientID;
                    break;
                #endregion
                #region Text18Digit
                case "TEXT18DIGIT":
                    t18.Visible = true;
                    t18.ToolTip = Quest.HelpText;
                    t18.Attributes["onMouseOver"] = "return ShowTooTip('" + t18.ClientID + "', true);";
                    t18.Attributes["onMouseOut"] = "return ShowTooTip('" + t18.ClientID + "', false);";
                    ox = Quest.Options.FirstOrDefault();

                    //if (Quest.Name.Length > 5 && Quest.Name.Substring(0, 5).ToUpper() == "PART ")
                    //{
                    //    hdnPartNumberIDs.Value += ox.OptionID.ToString() + ",";
                    //}


                    if (ox != null)
                    {
                        t18.Attributes.Add("someValue", ox.OptionID.ToString());
                        ValidString += "TX_" + ox.OptionID.ToString() + " ";
                    }
                    t18.Enabled = bEnabled;
                    if (Quest.isReadOnly != null && Quest.isReadOnly == true) { t18.Enabled = false; }
                    hdnQuestionIDList.Value += "," + Quest.QuestionID.ToString();
                    hdnQuestionClientIDList.Value += "," + t18.ClientID;
                    break;
                #endregion
                #region Text50Digit
                case "TEXT50DIGIT":
                    t50.Visible = true;
                    t50.ToolTip = Quest.HelpText;
                    t50.Attributes["onMouseOver"] = "return ShowTooTip('" + t50.ClientID + "', true);";
                    t50.Attributes["onMouseOut"] = "return ShowTooTip('" + t50.ClientID + "', false);";
                    ox = Quest.Options.FirstOrDefault();

                    //if (Quest.Name.Length > 5 && Quest.Name.Substring(0, 5).ToUpper() == "PART ")
                    //{
                    //    hdnPartNumberIDs.Value += ox.OptionID.ToString() + ",";
                    //}
                    if (ox != null)
                    {
                        t50.Attributes.Add("someValue", ox.OptionID.ToString());
                        ValidString += "TX_" + ox.OptionID.ToString() + " ";
                    }
                    t50.Enabled = bEnabled;
                    if (Quest.isReadOnly != null && Quest.isReadOnly == true) { t50.Enabled = false; }
                    hdnQuestionIDList.Value += "," + Quest.QuestionID.ToString();
                    hdnQuestionClientIDList.Value += "," + t50.ClientID;
                    break;
                #endregion
                ///////////////////////////////////////
                #region Calendar
                case "CALENDAR":
                    Cal.Visible = true;
                    Cal.ToolTip = Quest.HelpText;
                    Cal.Attributes["onMouseOver"] = "return ShowTooTip('" + Cal.ClientID + "', true);";
                    Cal.Attributes["onMouseOut"] = "return ShowTooTip('" + Cal.ClientID + "', false);";
                    ox = Quest.Options.FirstOrDefault();
                    if (ox != null)
                    {
                        Cal.Attributes.Add("someValue", ox.OptionID.ToString());
                        ValidString += "TX_" + ox.OptionID.ToString() + " ";
                    }
                    Cal.Enabled = bEnabled;
                    if (Quest.isReadOnly != null && Quest.isReadOnly == true) { Cal.Enabled = false; }
                    hdnQuestionIDList.Value += "," + Quest.QuestionID.ToString();
                    hdnQuestionClientIDList.Value += "," + Cal.ClientID;
                    break;
                #endregion
                #region DropDown
                case "DROPDOWN":
                    D1.Visible = true;
                    D1.Items.Clear();
                    D1.ToolTip = Quest.HelpText;
                    D1.Attributes["onMouseOver"] = "return ShowTooTip('" + D1.ClientID + "', true);";
                    D1.Attributes["onMouseOut"] = "return ShowTooTip('" + D1.ClientID + "', false);";

                    //if (rec.Name == "Carrier" ||
                    //    rec.Name == "Manufacturer" ||
                    //    rec.Name == "Model")
                    //{
                    D1.Attributes.Add("onchange", "javascript:SetupDropDown('" + Quest.Name + "')");
                    //}
                    foreach (Option o in Quest.Options.Where(x => x.OptionStatus.Status.ToUpper() != "INACTIVE").OrderBy(x => x.Sequence))
                    {
                        ListItem x = new ListItem(o.OptionText, o.OptionID.ToString());
                        D1.Items.Add(x);
                        ValidString += "DD_" + o.OptionID.ToString() + " ";
                    }
                    D1.Enabled = bEnabled;
                    if (Quest.isReadOnly != null && Quest.isReadOnly == true) { D1.Enabled = false; }
                    hdnQuestionIDList.Value += "," + Quest.QuestionID.ToString();
                    hdnQuestionClientIDList.Value += "," + D1.ClientID;
                    break;
                #endregion
                #region RadialButton
                case "RADIALBUTTON":
                    R1.Visible = true;
                    R1.ToolTip = Quest.HelpText;
                    R1.Attributes["onMouseOver"] = "return ShowTooTip('" + R1.ClientID + "', true);";
                    R1.Attributes["onMouseOut"] = "return ShowTooTip('" + R1.ClientID + "', false);";

                    int xmLength = 0;
                    int xCount = 0;
                    int xSet = 4;

                    R1.Attributes.Add("onchange", "javascript:SetupDropDown('" + Quest.Name + "');");
                    foreach (Option o in Quest.Options.Where(x => x.OptionStatus.Status.ToUpper() != "INACTIVE").OrderBy(x => x.Sequence))
                    {
                        ListItem x = new ListItem(o.OptionText, o.OptionID.ToString());
                        xCount += 1;
                        xmLength += x.Text.Length;
                        if (xmLength > 45)
                        {
                            if (xCount < xSet) { xSet = xCount; }
                            xCount = 0;
                            xmLength = 0;
                        }
                        if (Quest.Name.ToUpper() == "LAB DESTINATION")
                        {
                            x.Attributes.Add("onclick", "javascript:SetLabDestination(this);");
                        }
                        R1.Items.Add(x);
                        ValidString += "RD_" + o.OptionID.ToString() + " ";
                    }

                    if (Quest.ShowVertical == true) { R1.RepeatDirection = RepeatDirection.Vertical; R1.RepeatColumns = 1; }
                    else { R1.RepeatDirection = RepeatDirection.Horizontal; R1.RepeatColumns = xSet; }

                    R1.Enabled = bEnabled;
                    if (Quest.isReadOnly != null && Quest.isReadOnly == true) { R1.Enabled = false; }

                    hdnQuestionIDList.Value += "," + Quest.QuestionID.ToString();
                    hdnQuestionClientIDList.Value += "," + R1.ClientID;
                    break;
                #endregion
                #region Checkbox
                case "CHECKBOX":
                    c1.Visible = true;
                    c1.ToolTip = Quest.HelpText;
                    c1.Attributes["onMouseOver"] = "return ShowTooTip('" + c1.ClientID + "', true);";
                    c1.Attributes["onMouseOut"] = "return ShowTooTip('" + c1.ClientID + "', false);";
                    xmLength = 0;
                    xCount = 0;
                    xSet = 5;
                    c1.Attributes.Add("onchange", "javascript:SetupDropDown('" + Quest.Name + "')");
                    foreach (Option o in Quest.Options.Where(x => x.OptionStatus.Status.ToUpper() != "INACTIVE").OrderBy(x => x.Sequence))
                    {
                        ListItem x = new ListItem(o.OptionText, o.OptionID.ToString());
                        xCount += 1;
                        xmLength += x.Text.Length;
                        if (xmLength > 45)
                        {
                            if (xCount < xSet) { xSet = xCount; }
                            xCount = 0;
                            xmLength = 0;
                        }
                        x.Attributes.Add("someValue", o.OptionID.ToString());
                        c1.Items.Add(x);
                        ValidString += "CB_" + o.OptionID.ToString() + " ";
                    }

                    //if (Quest.ShowVertical == true) { c1.RepeatDirection = RepeatDirection.Vertical; c1.RepeatColumns = 1; }
                    //else {
                    c1.RepeatDirection = RepeatDirection.Horizontal; c1.RepeatColumns = xSet;
                    //}
                    c1.Enabled = bEnabled;
                    if (Quest.isReadOnly != null && Quest.isReadOnly == true) { c1.Enabled = false; }

                    hdnQuestionIDList.Value += "," + Quest.QuestionID.ToString();
                    hdnQuestionClientIDList.Value += "," + c1.ClientID;
                    //}
                    break;
                #endregion
                default:
                    log.LogIt("Inside LoadData Default");
                    c1.Visible = true;
                    c1.ToolTip = Quest.HelpText;
                    c1.Attributes["onMouseOver"] = "return ShowTooTip('" + c1.ClientID + "', true);";
                    c1.Attributes["onMouseOut"] = "return ShowTooTip('" + c1.ClientID + "', false);";
                    xmLength = 0;
                    xCount = 0;
                    xSet = 5;
                    foreach (Option o in Quest.Options.OrderBy(x => x.Sequence))
                    {
                        ListItem x = new ListItem(o.OptionText, o.OptionID.ToString());
                        xCount += 1;
                        xmLength += x.Text.Length;
                        if (xmLength > 45)
                        {
                            if (xCount < xSet) { xSet = xCount; }
                            xCount = 0;
                            xmLength = 0;
                        }
                        c1.Items.Add(x);
                        ValidString += "TX_" + o.OptionID.ToString() + " ";       ///// unsure if this should be "TX_", possibly should be "CB_"
                    }

                    if (Quest.ShowVertical == true) { c1.RepeatDirection = RepeatDirection.Vertical; c1.RepeatColumns = 1; }
                    else { c1.RepeatDirection = RepeatDirection.Horizontal; c1.RepeatColumns = xSet - 1; }
                    c1.Enabled = bEnabled;
                    if (Quest.isReadOnly != null && Quest.isReadOnly == true) { c1.Enabled = false; }
                    hdnQuestionIDList.Value += "," + Quest.QuestionID.ToString();
                    hdnQuestionClientIDList.Value += "," + c1.ClientID;
                    break;
            }
            //log.LogIt("LoadData ended");
            return ValidString;
        }

        //void FillOptionClientdropdowns()
        //{

        //    ClientManager cm = new ClientManager(User.Identity.Name);
        //    List<Client> cl = cm.DropDownUserFilterMasterClientPreReceiveList("", "", "", "").OrderBy(x => x.CompanyName).ToList();

        //    drpAddIDCMasterClient.Items.Clear();
        //    ListItem z = new ListItem("", "-1");
        //    drpAddIDCMasterClient.Items.Add(z);
        //    foreach (Client c in cl)
        //    {
        //        ListItem x = new ListItem(c.CompanyName, c.ClientID.ToString());
        //        drpAddIDCMasterClient.Items.Add(x);
        //    }

        //    drpEditIDCMasterClient.Items.Clear();
        //    ListItem z1 = new ListItem("", "-1");
        //    drpEditIDCMasterClient.Items.Add(z1);
        //    foreach (Client c in cl)
        //    {
        //        ListItem x = new ListItem(c.CompanyName, c.ClientID.ToString());
        //        drpEditIDCMasterClient.Items.Add(x);
        //    }
        //    //drpAddIDCMasterClient.DataValueField = "ClientID";
        //    //drpAddIDCMasterClient.DataTextField = "CompanyName";
        //    //drpAddIDCMasterClient.DataSource = cm.DropDownUserFilterMasterClientPreReceiveList("", "", "", "").OrderBy(x => x.CompanyName);
        //    //drpAddIDCMasterClient.DataBind();

        //    //drpEditIDCMasterClient.DataValueField = "ClientID";
        //    //drpEditIDCMasterClient.DataTextField = "CompanyName";
        //    //drpEditIDCMasterClient.DataSource = cm.DropDownUserFilterMasterClientPreReceiveList("", "", "", "").OrderBy(x => x.CompanyName);
        //    //drpEditIDCMasterClient.DataBind();
        //}

        //void btnMergeGo_Click(object sender, EventArgs e)
        //{
        //    if (ChildGrid.SelectedIndex >= 0)
        //    {
        //        decimal TargetID = decimal.Parse(ChildGrid.SelectedValue.ToString());
        //        int Count = 0;
        //        decimal SourceID = -1;
        //        QuestionManager qm = new QuestionManager(User.Identity.Name);
        //        foreach (System.Web.UI.WebControls.GridViewRow Row in ChildGrid_Merge.Rows)
        //        {
        //            CheckBox cb = (CheckBox)Row.FindControl("chkSelect");
        //            if (cb.Checked == true)
        //            {
        //                SourceID = (decimal)ChildGrid_Merge.DataKeys[Row.RowIndex]["OptionID"];
        //                Label l = (Label)Row.FindControl("lblMergeInto");
        //                if (TargetID == SourceID) { cb.Checked = false; l.Text = "Target"; }
        //                else
        //                {
        //                    Count = qm.Utility_ReplaceOptionAttributeID(SourceID, TargetID);
        //                    //ctx.Utility_ReplaceOptionAttributeID(SourceID, TargetID, ref Count);
        //                    //l.Text = "Merged Source:" + SourceID.ToString() + "  Target:" + TargetID.ToString() + "    Updated:" + Count.ToString();
        //                    l.Text = "Merged :" + Count.ToString();
        //                }
        //            }
        //        }
        //    }
        //}

        //void btnMergeClear_Click(object sender, EventArgs e)
        //{
        //    ResetMergeUtility();
        //}

        //void grdMasterBucketTransaction_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    btnEditBucket.Visible = false;
        //    btnAddBucket.Visible = false;
        //    if (grdMasterBucketTransaction.SelectedIndex >= 0)
        //    {
        //        decimal KeyID = decimal.Parse(grdMasterBucketTransaction.SelectedValue.ToString());
        //        btnEditBucket.Visible = true;
        //        btnAddBucket.Visible = true;
        //        btnDeleteBucket.Visible = true;

        //        chkEditBucket.Checked = false;
        //        chkEditSingle.Checked = false;
        //        txtEditCount.Text = "";
        //        txtEditAction.Text = "";

        //        chkAddBucket.Checked = false;
        //        chkAddSingle.Checked = true;
        //        txtAddCount.Text = "1";
        //        txtAddAction.Text = "";

        //        MasterBucketTransactionManager btm = new MasterBucketTransactionManager(User.Identity.Name);
        //        MasterBucketTransaction bt = btm.Get(KeyID);
        //        if (bt != null)
        //        {
        //            if (bt.Active == true) { chkEditBucket.Checked = true; }
        //            if (bt.OneOnly == true) { chkEditSingle.Checked = true; }
        //            txtEditCount.Text = bt.Count.ToString();
        //            txtEditAction.Text = bt.Action;

        //            //pnlMainBucket.Visible = true;
        //        }
        //    }
        //}

        //void MainGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        Type type = e.Row.DataItem.GetType();               //.GetGenericArguments()[0]; 
        //        //LinkButton bPrint = (LinkButton)e.Row.FindControl("imgPrint");
        //        CheckBox cb = (CheckBox)e.Row.FindControl("chkSelect");
        //        GridQuestionDetail rec = (GridQuestionDetail)e.Row.DataItem;
        //        //if (bPrint != null)
        //        //{
        //        //    bPrint.Attributes.Add("onclick", "PrintScanCodes('" + rec.QuestionID + "','" + rec.Description + "'); return false;");
        //        //}
        //        if (cb != null)
        //        {
        //            cb.Attributes.Add("onclick", "CheckboxChecked('" + rec.QuestionID + "');");
        //        }
        //    }
        //}

        //void btnSetScanKey_Click(object sender, EventArgs e)
        //{
        //    QuestionManager qm = new QuestionManager(User.Identity.Name);
        //    qm.SetAll_OptionBlankScanCodes();
        //    ScriptManager.RegisterStartupScript(this, GetType(), "OKMessage", "alert('Scancodes updated');", true);

        //}

        //void drpProjectList_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    UpdateMainGrid();
        //    //txtProject.Text = drpProjectList.SelectedItem.Text;
        //}

        //protected void UpdateMainGrid()
        //{
        //    decimal ProjectID = -1;
        //    decimal.TryParse(drpProjectList.SelectedItem.Value, out ProjectID);
        //    //clsLinqDataContext ctx = new clsLinqDataContext();
        //    QuestionManager qm = new QuestionManager(User.Identity.Name);
        //    //using (clsLinqDataContext ctx = new clsLinqDataContext(User.Identity.Name))
        //    //{
        //    var QData = (from x in qm.GetQuestionsFieldSpecificThisProject(ProjectID).OrderBy(y => y.Name).OrderBy(y => y.Sequence)
        //                 select new GridQuestionDetail
        //                 {
        //                     Name = x.Name,
        //                     QuestionID = x.QuestionID,
        //                     Description = x.Description,
        //                     Status = x.QuestionStatus.Status,
        //                     Type = x.QuestionType.Type,
        //                     Sequence = x.Sequence,
        //                     AllowAdd = x.AllowAdd,
        //                     AllowDelete = x.AllowDelete,
        //                     AllowScan = x.AllowScan,
        //                     AllowSelect = x.AllowSelect,
        //                     AllowUpdate = x.AllowUpdate,
        //                     IsHeaderQuestion = x.IsHeaderQuestion,
        //                     isManditory = x.isManditory,
        //                     isReadOnly = x.isReadOnly,
        //                     IFS_Condition = x.IFS_Condition,
        //                     IFS_Condition_Sequence = x.IFS_Condition_Sequence
        //                 }).ToList();
        //    MainGrid.DataSource = QData;
        //    //}
        //    ////MainGrid.DataSource = (from x in ctx.Questions.OrderBy(y => y.Name).OrderBy(y => y.Sequence) select new { x.Name, x.QuestionID, x.Description, x.QuestionStatus.Status, x.QuestionType.Type, x.Sequence });
        //    MainGrid.DataBind();

        //    MainGrid.SelectedIndex = -1;
        //    pnlChild.Visible = false;
        //    pnlMainBucket.Visible = false;
        //    pnlMergeUtility.Visible = false;
        //    TabAnswer.Visible = false;
        //    btnEdit.Visible = false;
        //    btnDelete.Visible = false;
        //}


        //protected void UpdateChildGrid(decimal KeyID)
        //{
        //    AnswerManager am = new AnswerManager(User.Identity.Name);
        //    var ds = (from x in am.GetAnswers().Where(y => y.QuestionID == KeyID).OrderBy(y => y.Name).OrderBy(y => y.Sequence) select new { x.ScanKey, x.MicroKey, x.MacroKey, x.Name, x.OptionID, x.OptionText, x.OptionStatus.Status, x.OptionType.Type, x.Sequence });
        //    ChildGrid.DataSource = ds;
        //    ChildGrid.DataBind();
        //    ChildGrid.SelectedIndex = -1;
        //    btnEditOption.Visible = false;
        //    btnDeleteOption.Visible = false;
        //    //var ds1 = (from x in am.GetAnswers().Where(y => y.QuestionID == KeyID).OrderBy(y => y.Name).OrderBy(y => y.Sequence) select new { x.ScanKey, x.MicroKey, x.MacroKey, x.Name, x.OptionID, x.OptionText, x.OptionStatus.Status, x.OptionType.Type, x.Sequence });
        //    ChildGrid_Merge.DataSource = ds;
        //    ChildGrid_Merge.DataBind();
        //    ChildGrid_Merge.SelectedIndex = -1;
        //}

        //protected void UpdateAnswerGridBuckets()
        //{
        //    decimal KeyID = decimal.Parse(ChildGrid.SelectedValue.ToString());
        //    MasterBucketTransactionManager tm = new MasterBucketTransactionManager(User.Identity.Name);
        //    grdMasterBucketTransaction.DataSource = tm.OptionTransactions(KeyID);
        //    grdMasterBucketTransaction.DataBind();
        //    grdMasterBucketTransaction.SelectedIndex = -1;
        //    btnEditBucket.Visible = false;
        //    btnDeleteBucket.Visible = false;
        //}


        //protected void MainGrid_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (MainGrid.SelectedIndex >= 0)
        //    {
        //        decimal KeyID = decimal.Parse(MainGrid.SelectedValue.ToString());
        //        QuestionManager qm = new QuestionManager(User.Identity.Name);
        //        var qu = qm.GetQuestions(KeyID);
        //        if (qu != null)
        //        {
        //            TabAnswer.HeaderText = " (" + qu.Name + ") " + qu.Description;
        //            lblQuestionText.Text = " (" + qu.Name + ") " + qu.Description;
        //            EditKeyID.Text = qu.QuestionID.ToString();
        //            EditName.Text = qu.Name;
        //            EditDescription.Text = qu.Description;
        //            EditSequence.Text = qu.Sequence.ToString();
        //            EditIFSConditionSequence.Text = qu.IFS_Condition_Sequence.ToString();
        //            EditTableName.Text = qu.TableName;
        //            EditToolTip.Text = qu.HelpText;
        //            //EditBucketCount.Text = qu.BucketCount;
        //            //EditBucketCountOffset.Text = qu.BucketCountOffset;
        //            if (qu.IsHeaderQuestion == true) { EditIsHeaderQuestion.Checked = true; }
        //            else { EditIsHeaderQuestion.Checked = false; }
        //            if (qu.isManditory == true) { EditIsManditory.Checked = true; }
        //            else { EditIsManditory.Checked = false; }

        //            //if (qu.isKeyQuestion == true) { EditIsKeyQuestion.Checked = true; }
        //            //else { EditIsKeyQuestion.Checked = false; }
        //            if (qu.isSearchable == true) { EditIsSearchable.Checked = true; }
        //            else { EditIsSearchable.Checked = false; }

        //            if (qu.IFS_Condition == true) { EditIFSCondition.Checked = true; }
        //            else { EditIFSCondition.Checked = false; }



        //            if (qu.ShowVertical == true) { EditShowVertical.Checked = true; }
        //            else { EditShowVertical.Checked = false; }

        //            if (qu.isTimeSpread == true) { EditisTimeSpread.Checked = true; }
        //            else { EditisTimeSpread.Checked = false; }

        //            if (qu.isReadOnly == true) { EditReadOnly.Checked = true; }
        //            else { EditReadOnly.Checked = false; }

        //            if (qu.DisplayColumn != null) { EditDisplayColumn.Text = qu.DisplayColumn.ToString(); }
        //            else { EditDisplayColumn.Text = "1"; }


        //            ListItem _ListItem = drpEditStatus.Items.FindByValue(qu.QuestionStatusID.ToString());
        //            //if (_ListItem == null) { drpEditStatus.SelectedIndex = 0;}
        //            //else { drpAddStatus.SelectedIndex = drpAddStatus.Items.IndexOf(_ListItem);}
        //            //_ListItem = drpEditType.Items.FindByValue(qu.QuestionTypeID.ToString());
        //            //if (_ListItem == null) { drpEditType.SelectedIndex = 0;}
        //            //else { drpEditType.SelectedIndex = drpEditType.Items.IndexOf(_ListItem);}

        //            if (_ListItem == null) { drpEditStatus.SelectedIndex = 0; }
        //            else { drpAddStatus.SelectedIndex = drpAddStatus.Items.IndexOf(_ListItem); }
        //            _ListItem = drpEditType.Items.FindByValue(qu.QuestionTypeID.ToString());
        //            if (_ListItem == null) { drpEditType.SelectedIndex = 0; }
        //            else { drpEditType.SelectedIndex = drpEditType.Items.IndexOf(_ListItem); }
        //        }

        //        UpdateChildGrid(KeyID);
        //        pnlChild.Visible = true;
        //        pnlMainBucket.Visible = false;
        //        pnlMergeUtility.Visible = false;
        //        TabAnswer.Visible = true;
        //        btnEdit.Visible = false;
        //        btnDelete.Visible = false;
        //        btnAdd.Visible = false;
        //        btnSetScanKey.Visible = false;
        //        //btnPrintQuestion.Visible = false;

        //        using (clsLinqDataContext ctx = qm.GetDataContext(User.Identity.Name))
        //        {
        //            if (qm.UserRestrict.AllowUpdate("Question", qu.QuestionID, ctx) == true) { btnEdit.Visible = true; }
        //            if (qm.UserRestrict.AllowDelete("Question", qu.QuestionID, ctx) == true) { btnDelete.Visible = true; }
        //            if (qm.UserRestrict.AllowAdd("Question", qu.QuestionID, ctx) == true) { btnAdd.Visible = true; }
        //            btnSetScanKey.Visible = btnAdd.Visible;
        //            //btnPrintQuestion.Visible = btnAdd.Visible;

        //        }
        //    }
        //    else
        //    {
        //        pnlChild.Visible = false;
        //        pnlMainBucket.Visible = false;
        //        pnlMergeUtility.Visible = false;
        //        TabAnswer.Visible = false;
        //        btnEdit.Visible = false;
        //        btnDelete.Visible = false;
        //        btnSetScanKey.Visible = false;
        //        //btnPrintQuestion.Visible = false;
        //    }
        //}

        //protected void ChildGrid_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    btnEditBucket.Visible = false;
        //    btnDeleteBucket.Visible = false;
        //    btnAddOption.Visible = btnAdd.Visible;
        //    btnEditOption.Visible = false;
        //    btnDeleteOption.Visible = false;
        //    if (ChildGrid.SelectedIndex >= 0)
        //    {
        //        decimal KeyID = decimal.Parse(ChildGrid.SelectedValue.ToString());
        //        //                clsLinqDataContext ctx = new clsLinqDataContext();
        //        //                Option qu = ctx.Options.FirstOrDefault(x => x.OptionID == KeyID);
        //        AnswerManager am = new AnswerManager(User.Identity.Name);
        //        Option qu = am.GetAnswer(KeyID);

        //        if (qu != null)
        //        {
        //            EditScanKey.Text = qu.ScanKey;
        //            EditMacroKey.Text = qu.MacroKey;
        //            EditMicroKey.Text = qu.MicroKey;
        //            EditKeyIDOption.Text = qu.OptionID.ToString();
        //            EditNameOption.Text = qu.Name;
        //            EditDescriptionOption.Text = qu.OptionText;
        //            EditSequenceOption.Text = qu.Sequence.ToString();
        //            //EditBucketCountOption.Text = qu.BucketCount;
        //            //EditBucketCountOffsetOption.Text = qu.BucketCountOffset;

        //            ListItem _ListItem = drpEditStatusOption.Items.FindByValue(qu.OptionStatusID.ToString());
        //            if (_ListItem == null) { drpEditStatusOption.SelectedIndex = 0; }
        //            else { drpEditStatusOption.SelectedIndex = drpEditStatusOption.Items.IndexOf(_ListItem); }


        //            _ListItem = drpEditTypeOption.Items.FindByValue(qu.OptionTypeID.ToString());
        //            if (_ListItem == null) { drpEditTypeOption.SelectedIndex = 0; }
        //            else { drpEditTypeOption.SelectedIndex = drpEditTypeOption.Items.IndexOf(_ListItem); }

        //            _ListItem = drpEditIDCMasterClient.Items.FindByValue(qu.IDC_ClientID.ToString());
        //            if (_ListItem == null) { drpEditIDCMasterClient.SelectedIndex = 0; }
        //            else { drpEditIDCMasterClient.SelectedIndex = drpEditIDCMasterClient.Items.IndexOf(_ListItem); }

        //            EditFriendlyName.Text = qu.IDC_FriendlyName;

        //        }
        //        UpdateAnswerGridBuckets();
        //        pnlMainBucket.Visible = true;
        //        //pnlMergeUtility.Visible = true;
        //        ResetMergeUtility();
        //        btnEditOption.Visible = btnEdit.Visible;
        //        btnDeleteOption.Visible = btnDelete.Visible;
        //        btnAddOption.Visible = btnAdd.Visible;
        //    }

        //}

        //private void ResetMergeUtility()
        //{
        //    pnlMergeUtility.Visible = true;
        //    foreach (System.Web.UI.WebControls.GridViewRow Row in ChildGrid_Merge.Rows)
        //    {
        //        CheckBox cb = (CheckBox)Row.FindControl("chkSelect");
        //        cb.Checked = false;
        //        Label l = (Label)Row.FindControl("lblMergeInto");
        //        l.Text = "";
        //    }
        //}


        //#region QuesionDetail

        //protected void btnAdd_Click(object sender, EventArgs e)
        //{
        //    AddName.Text = "";
        //    AddDescription.Text = "";
        //    AddSequence.Text = "";
        //    AddIFSConditionSequence.Text = "";
        //    AddIsHeaderQuestion.Checked = false;
        //    AddIsManditory.Checked = false;
        //    //AddIsKeyQuestion.Checked = false;
        //    AddIsSearchable.Checked = false;
        //    AddIFSCondition.Checked = false;
        //    AddShowVertical.Checked = false;
        //    AddisTimeSpread.Checked = false;
        //    AddReadOnly.Checked = false;
        //    AddDisplayColumn.Text = "1";
        //    pnlMainView.Visible = false;
        //    pnlAdd.Visible = true;
        //    //AddBucketCount.Text = "";
        //    //AddBucketCountOffset.Text = "";
        //    GoTop();

        //}
        //protected void btnEdit_Click(object sender, EventArgs e)
        //{
        //    pnlMainView.Visible = false;
        //    pnlEdit.Visible = true;
        //    GoTop();
        //}
        //protected void btnDelete_Click(object sender, EventArgs e)
        //{
        //    //// Delete the Question and all the attached answers.
        //    //// Delete the answers.
        //    if (MainGrid.SelectedIndex >= 0)
        //    {
        //        decimal MainGridKeyID = decimal.Parse(MainGrid.SelectedValue.ToString());
        //        QuestionManager qm = new QuestionManager(User.Identity.Name);
        //        try
        //        {
        //            qm.DeleteQuestion(MainGridKeyID);
        //            ScriptManager.RegisterStartupScript(this, GetType(), "Question Delete", "alert('Delete Success');", true);
        //        }
        //        catch (UserAccessControlException ex)
        //        {
        //            ScriptManager.RegisterStartupScript(this, GetType(), "Question Delete", "alert('" + ex.Message + "');", true);
        //        }
        //        UpdateMainGrid();
        //        UpdateChildGrid(-1);
        //        //}
        //    }
        //}
        /////------------------------------------------------------------------------------------
        //protected void AddQuestionOK_Click(object sender, EventArgs e)
        //{
        //    int Sequence = 0;
        //    int IFSConditionSequebnce = 0;
        //    if (AddName.Text.Length == 0) { return; }
        //    if (AddDescription.Text.Length == 0) { return; }
        //    if (drpAddStatus.SelectedIndex < 0) { return; }
        //    if (drpAddType.SelectedIndex < 0) { return; }
        //    if (AddSequence.Text.Length == 0 || int.TryParse(AddSequence.Text, out Sequence) == false) { Sequence = 100; }
        //    if (AddIFSConditionSequence.Text.Length == 0 || int.TryParse(AddIFSConditionSequence.Text, out IFSConditionSequebnce) == false) { IFSConditionSequebnce = 100; }


        //    try
        //    {
        //        QuestionManager qm = new QuestionManager(User.Identity.Name);
        //        Question qu = qm.NewQuestion();
        //        //clsLinqDataContext ctx = new clsLinqDataContext();
        //        //Question qu = new Question();
        //        qu.Name = AddName.Text;
        //        qu.TableName = AddTableName.Text;
        //        qu.HelpText = AddToolTip.Text;
        //        qu.Description = AddDescription.Text;
        //        qu.QuestionStatusID = decimal.Parse(drpAddStatus.SelectedItem.Value);
        //        qu.QuestionTypeID = decimal.Parse(drpAddType.SelectedItem.Value);
        //        qu.Sequence = Sequence;
        //        qu.IFS_Condition_Sequence = IFSConditionSequebnce;
        //        qu.IsHeaderQuestion = AddIsHeaderQuestion.Checked;
        //        qu.isManditory = AddIsManditory.Checked;
        //        qu.isKeyQuestion = false; // AddIsKeyQuestion.Checked;
        //        qu.isSearchable = AddIsSearchable.Checked;
        //        qu.IFS_Condition = AddIFSCondition.Checked;
        //        qu.ShowVertical = AddShowVertical.Checked;
        //        qu.isTimeSpread = AddisTimeSpread.Checked;
        //        qu.isReadOnly = AddReadOnly.Checked;
        //        decimal dCol = 1;
        //        if (decimal.TryParse(AddDisplayColumn.Text, out dCol) == false) { dCol = 1; }
        //        qu.DisplayColumn = dCol;
        //        qu.BucketCount = ""; // AddBucketCount.Text;
        //        qu.BucketCountOffset = "";   // AddBucketCountOffset.Text;
        //        qm.InsertQuestion(qu);

        //        UpdateMainGrid();
        //        pnlMainView.Visible = true;
        //        pnlAdd.Visible = false;
        //        GoTop();

        //    }
        //    catch (UserAccessControlException ex)
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "Question Delete", "alert('" + ex.Message + "');", true);
        //    }
        //    return;


        //}
        //protected void AddQuestionCancel_Click(object sender, EventArgs e)
        //{
        //    pnlMainView.Visible = true;
        //    pnlAdd.Visible = false;
        //    GoTop();
        //}
        //protected void EditQuestionOK_Click(object sender, EventArgs e)
        //{
        //    log.LogIt("**** Edit Question OK Click");
        //    int Sequence = 0;
        //    int IFSConditionSequence = 0;
        //    if (EditName.Text.Length == 0) { return; }
        //    if (EditDescription.Text.Length == 0) { return; }
        //    if (drpEditStatus.SelectedIndex < 0) { return; }
        //    if (drpEditType.SelectedIndex < 0) { return; }
        //    log.LogIt("**** Edit Question OK Past Edits");
        //    if (EditSequence.Text.Length == 0 || int.TryParse(EditSequence.Text, out Sequence) == false) { Sequence = 100; }
        //    if (EditIFSConditionSequence.Text.Length == 0 || int.TryParse(EditIFSConditionSequence.Text, out IFSConditionSequence) == false) { IFSConditionSequence = 100; }
        //    decimal KeyID = decimal.Parse(EditKeyID.Text);
        //    try
        //    {
        //        log.LogIt("**** Edit Question OK Inside Try");
        //        QuestionManager qm = new QuestionManager(User.Identity.Name);
        //        Question qu = qm.NewQuestion();
        //        qu.QuestionID = KeyID;
        //        qu.Name = EditName.Text;
        //        qu.TableName = EditTableName.Text;
        //        qu.HelpText = EditToolTip.Text;
        //        qu.Description = EditDescription.Text;
        //        qu.QuestionStatusID = decimal.Parse(drpEditStatus.SelectedItem.Value);
        //        qu.QuestionTypeID = decimal.Parse(drpEditType.SelectedItem.Value);
        //        //qu.HelpText = "";
        //        qu.Sequence = Sequence;
        //        qu.IFS_Condition_Sequence = IFSConditionSequence;
        //        qu.IsHeaderQuestion = EditIsHeaderQuestion.Checked;
        //        qu.isManditory = EditIsManditory.Checked;

        //        qu.isKeyQuestion = false; // EditIsKeyQuestion.Checked;
        //        qu.isSearchable = EditIsSearchable.Checked;
        //        qu.IFS_Condition = EditIFSCondition.Checked;
        //        qu.ShowVertical = EditShowVertical.Checked;
        //        qu.isTimeSpread = EditisTimeSpread.Checked;
        //        qu.isReadOnly = EditReadOnly.Checked;
        //        qu.BucketCount = ""; // EditBucketCount.Text;
        //        qu.BucketCountOffset = "";   // EditBucketCountOffset.Text;
        //        decimal dCol = 1;
        //        if (decimal.TryParse(EditDisplayColumn.Text, out dCol) == false) { dCol = 1; }
        //        qu.DisplayColumn = dCol;

        //        qm.UpdateQuestion(qu);
        //        log.LogIt("**** Edit Question OK Updated");

        //        UpdateMainGrid();
        //        pnlMainView.Visible = true;
        //        pnlEdit.Visible = false;
        //        GoTop();
        //    }
        //    catch (UserAccessControlException ex)
        //    {
        //        log.LogIt("**** Edit Question OK Error:" + ex.Message);
        //        ScriptManager.RegisterStartupScript(this, GetType(), "Question Update", "alert('" + ex.Message + "');", true);
        //    }
        //}
        //protected void EditQuestionCancel_Click(object sender, EventArgs e)
        //{
        //    pnlMainView.Visible = true;
        //    pnlEdit.Visible = false;
        //}






        //#endregion


        //#region ProcessBucket
        //protected void btnAddBucket_Click(object sender, EventArgs e)
        //{
        //    pnlMainView.Visible = false;
        //    pnlAddBucket.Visible = true;
        //}
        //protected void btnEditBucket_Click(object sender, EventArgs e)
        //{
        //    pnlMainView.Visible = false;
        //    pnlEditBucket.Visible = true;
        //}
        //protected void btnDeleteBucket_Click(object sender, EventArgs e)
        //{
        //    if (grdMasterBucketTransaction.SelectedIndex >= 0)
        //    {
        //        decimal BucketKeyID = decimal.Parse(grdMasterBucketTransaction.SelectedValue.ToString());
        //        MasterBucketTransactionManager tm = new MasterBucketTransactionManager(User.Identity.Name);
        //        try
        //        {
        //            tm.Delete(BucketKeyID);
        //            ScriptManager.RegisterStartupScript(this, GetType(), "Bucket Delete", "alert('Delete Success');", true);
        //        }
        //        catch (UserAccessControlException ex)
        //        {
        //            ScriptManager.RegisterStartupScript(this, GetType(), "Bucket Delete", "alert('" + ex.Message + "');", true);
        //        }
        //        UpdateAnswerGridBuckets();
        //    }
        //}
        /////------------------------------------------------------------------------------------
        //protected void AddBucketOK_Click(object sender, EventArgs e)
        //{
        //    int Count = 0;
        //    if (int.TryParse(txtAddCount.Text, out Count) == false) { Count = 0; }
        //    decimal KeyID = decimal.Parse(ChildGrid.SelectedValue.ToString());
        //    MasterBucketTransactionManager btm = new MasterBucketTransactionManager(User.Identity.Name);
        //    btm.InsertOption(KeyID, chkAddBucket.Checked, txtAddAction.Text, Count, chkAddSingle.Checked);
        //    UpdateAnswerGridBuckets();
        //    pnlMainView.Visible = true;
        //    pnlAddBucket.Visible = false;
        //    return;
        //}
        //protected void AddBucketCancel_Click(object sender, EventArgs e)
        //{
        //    pnlMainView.Visible = true;
        //    pnlAddBucket.Visible = false;
        //    //GoTop();
        //}
        //protected void EditBucketOK_Click(object sender, EventArgs e)
        //{
        //    int Count = 0;
        //    if (int.TryParse(txtEditCount.Text, out Count) == false) { Count = 0; }
        //    decimal KeyID = decimal.Parse(grdMasterBucketTransaction.SelectedValue.ToString());
        //    MasterBucketTransactionManager btm = new MasterBucketTransactionManager(User.Identity.Name);
        //    btm.Update(KeyID, chkEditBucket.Checked, txtEditAction.Text, Count, chkEditSingle.Checked);
        //    UpdateAnswerGridBuckets();
        //    pnlMainView.Visible = true;
        //    pnlEditBucket.Visible = false;
        //}
        //protected void EditBucketCancel_Click(object sender, EventArgs e)
        //{
        //    pnlMainView.Visible = true;
        //    pnlEditBucket.Visible = false;
        //}
        //#endregion


        ////#region AnswerBucket
        ////protected void btnAddBucket_Click(object sender, EventArgs e)
        ////{
        ////    //AddName.Text = "";
        ////    //AddDescription.Text = "";
        ////    //AddSequence.Text = "";
        ////    //AddIsHeaderQuestion.Checked = false;
        ////    //AddIsManditory.Checked = false;
        ////    //AddIsKeyQuestion.Checked = false;
        ////    //AddIsSearchable.Checked = false;
        ////    //AddDisplayColumn.Text = "1";
        ////    pnlMainView.Visible = false;
        ////    pnlAddBucket.Visible = true;
        ////    //AddBucketCount.Text = "";
        ////    //AddBucketCountOffset.Text = "";
        ////    //GoTop();

        ////}
        ////protected void btnEditBucket_Click(object sender, EventArgs e)
        ////{
        ////    pnlMainView.Visible = false;
        ////    pnlEditBucket.Visible = true;
        ////    //GoTop();
        ////}
        ////protected void btnDeleteBucket_Click(object sender, EventArgs e)
        ////{
        ////    ////// Delete the Question and all the attached answers.
        ////    ////// Delete the answers.
        ////    if (grdMasterBucketTransaction.SelectedIndex >= 0)
        ////    {
        ////        decimal BucketKeyID = decimal.Parse(grdMasterBucketTransaction.SelectedValue.ToString());
        ////        MasterBucketTransactionManager tm = new MasterBucketTransactionManager(User.Identity.Name);
        ////        try
        ////        {
        ////            tm.Delete(BucketKeyID);
        ////            ScriptManager.RegisterStartupScript(this, GetType(), "Bucket Delete", "alert('Delete Success');", true);
        ////        }
        ////        catch (UserAccessControlException ex)
        ////        {
        ////            ScriptManager.RegisterStartupScript(this, GetType(), "Bucket Delete", "alert('" + ex.Message + "');", true);
        ////        }
        ////        decimal KeyID = decimal.Parse(MainGrid.SelectedValue.ToString());
        ////        UpdateAnswerGridBuckets(KeyID);
        ////        //}
        ////    }
        ////}
        ///////------------------------------------------------------------------------------------
        ////protected void AddBucketOK_Click(object sender, EventArgs e)
        ////{
        ////    //int Sequence = 0;
        ////    //if (AddName.Text.Length == 0) { return; }
        ////    //if (AddDescription.Text.Length == 0) { return; }
        ////    //if (drpAddStatus.SelectedIndex < 0) { return; }
        ////    //if (drpAddType.SelectedIndex < 0) { return; }
        ////    //if (AddSequence.Text.Length == 0 || int.TryParse(AddSequence.Text, out Sequence) == false) { Sequence = 100; }


        ////    //try
        ////    //{
        ////    //    QuestionManager qm = new QuestionManager(User.Identity.Name);
        ////    //    Question qu = qm.NewQuestion();
        ////    //    //clsLinqDataContext ctx = new clsLinqDataContext();
        ////    //    //Question qu = new Question();
        ////    //    qu.Name = AddName.Text;
        ////    //    qu.TableName = AddTableName.Text;
        ////    //    qu.HelpText = AddToolTip.Text;
        ////    //    qu.Description = AddDescription.Text;
        ////    //    qu.QuestionStatusID = decimal.Parse(drpAddStatus.SelectedItem.Value);
        ////    //    qu.QuestionTypeID = decimal.Parse(drpAddType.SelectedItem.Value);
        ////    //    qu.Sequence = Sequence;
        ////    //    qu.IsHeaderQuestion = AddIsHeaderQuestion.Checked;
        ////    //    qu.isManditory = AddIsManditory.Checked;
        ////    //    qu.isKeyQuestion = AddIsKeyQuestion.Checked;
        ////    //    qu.isSearchable = AddIsSearchable.Checked;
        ////    //    decimal dCol = 1;
        ////    //    if (decimal.TryParse(AddDisplayColumn.Text, out dCol) == false) { dCol = 1; }
        ////    //    qu.DisplayColumn = dCol;
        ////    //    qu.BucketCount = AddBucketCount.Text;
        ////    //    qu.BucketCountOffset = AddBucketCountOffset.Text;
        ////    //    qm.InsertQuestion(qu);

        ////    //    UpdateMainGrid();
        ////    pnlMainView.Visible = true;
        ////    pnlAddBucket.Visible = false;
        ////    //    GoTop();

        ////    //}
        ////    //catch (UserAccessControlException ex)
        ////    //{
        ////    //    ScriptManager.RegisterStartupScript(this, GetType(), "Question Delete", "alert('" + ex.Message + "');", true);
        ////    //}
        ////    return;


        ////}
        ////protected void AddBucketCancel_Click(object sender, EventArgs e)
        ////{
        ////    pnlMainView.Visible = true;
        ////    pnlAddBucket.Visible = false;
        ////    //GoTop();
        ////}
        ////protected void EditBucketOK_Click(object sender, EventArgs e)
        ////{
        ////    //int Sequence = 0;
        ////    //if (EditName.Text.Length == 0) { return; }
        ////    //if (EditDescription.Text.Length == 0) { return; }
        ////    //if (drpEditStatus.SelectedIndex < 0) { return; }
        ////    //if (drpEditType.SelectedIndex < 0) { return; }
        ////    //if (EditSequence.Text.Length == 0 || int.TryParse(EditSequence.Text, out Sequence) == false) { Sequence = 100; }
        ////    //decimal KeyID = decimal.Parse(EditKeyID.Text);
        ////    //try
        ////    //{
        ////    //    QuestionManager qm = new QuestionManager(User.Identity.Name);
        ////    //    Question qu = qm.NewQuestion();
        ////    //    qu.QuestionID = KeyID;
        ////    //    qu.Name = EditName.Text;
        ////    //    qu.TableName = EditTableName.Text;
        ////    //    qu.HelpText = EditToolTip.Text;
        ////    //    qu.Description = EditDescription.Text;
        ////    //    qu.QuestionStatusID = decimal.Parse(drpEditStatus.SelectedItem.Value);
        ////    //    qu.QuestionTypeID = decimal.Parse(drpEditType.SelectedItem.Value);
        ////    //    //qu.HelpText = "";
        ////    //    qu.Sequence = Sequence;
        ////    //    qu.IsHeaderQuestion = EditIsHeaderQuestion.Checked;
        ////    //    qu.isManditory = EditIsManditory.Checked;

        ////    //    qu.isKeyQuestion = EditIsKeyQuestion.Checked;
        ////    //    qu.isSearchable = EditIsSearchable.Checked;
        ////    //    qu.BucketCount = EditBucketCount.Text;
        ////    //    qu.BucketCountOffset = EditBucketCountOffset.Text;
        ////    //    decimal dCol = 1;
        ////    //    if (decimal.TryParse(EditDisplayColumn.Text, out dCol) == false) { dCol = 1; }
        ////    //    qu.DisplayColumn = dCol;


        ////    //    qm.UpdateQuestion(qu);
        ////    //    UpdateMainGrid();
        ////    pnlMainView.Visible = true;
        ////    pnlEditBucket.Visible = false;
        ////    //    GoTop();
        ////    //}
        ////    //catch (UserAccessControlException ex)
        ////    //{
        ////    //    ScriptManager.RegisterStartupScript(this, GetType(), "Question Update", "alert('" + ex.Message + "');", true);
        ////    //}
        ////}
        ////protected void EditBucketCancel_Click(object sender, EventArgs e)
        ////{
        ////    pnlMainView.Visible = true;
        ////    pnlEditBucket.Visible = false;
        ////}
        ////#endregion

        //#region OptionDetail

        //protected void btnAddOption_Click(object sender, EventArgs e)
        //{
        //    AddScanKeyOption.Text = "";
        //    AddNameOption.Text = "";
        //    AddDescriptionOption.Text = "";
        //    AddSequenceOption.Text = "";
        //    AddMacroKey.Text = "";
        //    AddMicroKey.Text = "";
        //    AddTableName.Text = "";
        //    AddToolTip.Text = "";
        //    pnlMainView.Visible = false;
        //    pnlAddOption.Visible = true;
        //    //AddBucketCountOption.Text = "";
        //    //AddBucketCountOffsetOption.Text = "";
        //    GoTop();
        //}
        //protected void btnEditOption_Click(object sender, EventArgs e)
        //{
        //    pnlMainView.Visible = false;
        //    pnlEditOption.Visible = true;
        //    GoTop();
        //}
        //protected void btnDeleteOption_Click(object sender, EventArgs e)
        //{
        //    // Delete the answers.
        //    if (ChildGrid.SelectedIndex >= 0)
        //    {
        //        try
        //        {
        //            decimal MainGridKeyID = decimal.Parse(MainGrid.SelectedValue.ToString());
        //            decimal KeyID = decimal.Parse(ChildGrid.SelectedValue.ToString());
        //            AnswerManager am = new AnswerManager(User.Identity.Name);
        //            am.DeleteAnswer(KeyID);
        //            UpdateChildGrid(decimal.Parse(MainGrid.SelectedValue.ToString()));
        //        }
        //        catch (UserAccessControlException ex)
        //        {
        //            ScriptManager.RegisterStartupScript(this, GetType(), "Answer Delete", "alert('" + ex.Message + "');", true);
        //        }
        //        GoTop();
        //    }
        //}



        //#region Option AddEdit
        //protected void AddOptionOK_Click(object sender, EventArgs e)
        //{
        //    int Sequence = 0;
        //    //if (AddScanKeyOption.Text.Length == 0) { return; }
        //    if (AddNameOption.Text.Length == 0) { return; }
        //    if (AddDescriptionOption.Text.Length == 0) { return; }
        //    if (drpAddStatusOption.SelectedIndex < 0) { return; }
        //    if (drpAddTypeOption.SelectedIndex < 0) { return; }
        //    if (AddSequenceOption.Text.Length == 0 || int.TryParse(AddSequenceOption.Text, out Sequence) == false) { Sequence = 100; }
        //    try
        //    {
        //        AnswerManager am = new AnswerManager(User.Identity.Name);
        //        Option qu = am.NewAnswer();

        //        //clsLinqDataContext ctx = new clsLinqDataContext();
        //        //Option qu = new Option();
        //        qu.QuestionID = decimal.Parse(MainGrid.SelectedValue.ToString());
        //        qu.ScanKey = AddScanKeyOption.Text;
        //        qu.MacroKey = AddMacroKey.Text;
        //        qu.MicroKey = AddMicroKey.Text;
        //        qu.Name = AddNameOption.Text;
        //        qu.OptionText = AddDescriptionOption.Text;
        //        qu.OptionStatusID = decimal.Parse(drpAddStatusOption.SelectedItem.Value);
        //        qu.OptionTypeID = decimal.Parse(drpAddTypeOption.SelectedItem.Value);
        //        qu.IDC_ClientID = decimal.Parse(drpAddIDCMasterClient.SelectedItem.Value);
        //        qu.IDC_FriendlyName = AddFriendlyName.Text;

        //        qu.Sequence = Sequence;
        //        qu.BucketCount = "";// AddBucketCountOption.Text;
        //        qu.BucketCountOffset = "";// AddBucketCountOffsetOption.Text;
        //        am.InsertAnswer(qu);
        //        //ctx.Options.InsertOnSubmit(qu);
        //        //ctx.SubmitChanges();
        //        UpdateChildGrid(decimal.Parse(MainGrid.SelectedValue.ToString()));

        //        pnlMainView.Visible = true;
        //        pnlAddOption.Visible = false;
        //        GoTop();
        //    }
        //    catch (UserAccessControlException ex)
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "Answer Add", "alert('" + ex.Message + "');", true);
        //    }
        //}
        //protected void AddOptionCancel_Click(object sender, EventArgs e)
        //{
        //    pnlMainView.Visible = true;
        //    pnlAddOption.Visible = false;
        //    GoTop();
        //}
        //protected void EditOptionOK_Click(object sender, EventArgs e)
        //{
        //    log.LogIt("**** Edit Option OK Click");
        //    int Sequence = 0;
        //    //   if (EditScanKey.Text.Length == 0) { return; }
        //    if (EditNameOption.Text.Length == 0) { return; }
        //    if (EditDescriptionOption.Text.Length == 0) { return; }
        //    if (drpEditStatusOption.SelectedIndex < 0) { return; }
        //    if (drpEditTypeOption.SelectedIndex < 0) { return; }
        //    log.LogIt("**** Maint Question After Return Edits");
        //    if (EditSequenceOption.Text.Length == 0 || int.TryParse(EditSequenceOption.Text, out Sequence) == false) { Sequence = 100; }

        //    decimal KeyID = decimal.Parse(EditKeyIDOption.Text);
        //    decimal QKeyID = decimal.Parse(MainGrid.SelectedValue.ToString());
        //    try
        //    {
        //        AnswerManager am = new AnswerManager(User.Identity.Name);
        //        Option qu = am.NewAnswer();
        //        //            clsLinqDataContext ctx = new clsLinqDataContext();
        //        //            Option qu = ctx.Options.FirstOrDefault(x => x.OptionID == KeyID);
        //        if (qu != null)
        //        {
        //            qu.QuestionID = QKeyID;
        //            qu.OptionID = KeyID;
        //            qu.ScanKey = EditScanKey.Text;
        //            qu.MacroKey = EditMacroKey.Text;
        //            qu.MicroKey = EditMicroKey.Text;
        //            qu.Name = EditNameOption.Text;
        //            qu.OptionText = EditDescriptionOption.Text;
        //            qu.OptionStatusID = decimal.Parse(drpEditStatusOption.SelectedItem.Value);
        //            qu.OptionTypeID = decimal.Parse(drpEditTypeOption.SelectedItem.Value);
        //            qu.IDC_ClientID = decimal.Parse(drpEditIDCMasterClient.SelectedItem.Value);
        //            qu.IDC_FriendlyName = EditFriendlyName.Text;
        //            qu.Sequence = Sequence;
        //            qu.BucketCount = ""; //EditBucketCountOption.Text;
        //            qu.BucketCountOffset = "";  // EditBucketCountOffsetOption.Text;
        //            am.UpdateAnswer(qu);
        //            log.LogIt("**** Update Answer Executed:" + QKeyID.ToString());
        //            //qu.CreateUser = User.Identity.Name;
        //            //qu.LastUpdateDate = DateTime.Now;
        //            //qu.LastUpdateUser = User.Identity.Name;
        //            //ctx.SubmitChanges();
        //        }
        //        UpdateChildGrid(decimal.Parse(MainGrid.SelectedValue.ToString()));
        //        pnlMainView.Visible = true;
        //        pnlEditOption.Visible = false;
        //        GoTop();
        //    }
        //    catch (UserAccessControlException ex)
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "Question Update", "alert('" + ex.Message + "');", true);
        //    }
        //}
        //protected void EditOptionCancel_Click(object sender, EventArgs e)
        //{
        //    pnlMainView.Visible = true;
        //    pnlEditOption.Visible = false;
        //    GoTop();
        //}
        //#endregion




        //protected void GoTop()
        //{
        //    ScriptManager.RegisterStartupScript(this, GetType(), "GoTop", "goTopofScreen();", true);
        //}
        //#endregion

    }
}