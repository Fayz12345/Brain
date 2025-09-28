using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
//using Factory_DataModel;
using BW_WebApp.DataManagers;

namespace BW_WebApp.Maintenance
{
    public partial class BlackBelt : System.Web.UI.Page
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


            ////btnPrint.Attributes.Add("onclick", "PrintScanCodes(); return false;");
            ////btnPrintQuestion.Attributes.Add("onclick", "PrintQuestionCodes(); return false;");
            ////btnSetScanKey.Click += new EventHandler(btnSetScanKey_Click);
            //btnMergeClear.Click += new EventHandler(btnMergeClear_Click);
            //btnMergeGo.Click += new EventHandler(btnMergeGo_Click);
            MainGrid.SelectedIndexChanged += new EventHandler(MainGrid_SelectedIndexChanged);
            ChildGrid.SelectedIndexChanged += new EventHandler(ChildGrid_SelectedIndexChanged);
            drpProjectList.SelectedIndexChanged += new EventHandler(drpProjectList_SelectedIndexChanged);
            //grdMasterBucketTransaction.SelectedIndexChanged += new EventHandler(grdMasterBucketTransaction_SelectedIndexChanged);
            MainGrid.RowDataBound += new GridViewRowEventHandler(MainGrid_RowDataBound);
            EditOptionOK.Click += new EventHandler(EditOptionOK_Click);
            if (!IsPostBack)
            {
                TabMerge.Visible = false;
                if (User.IsInRole("Administrators") == true || User.IsInRole("Admin") == true)
                {
                    TabMerge.Visible = true;
                }

                FillOptionClientdropdowns();

                QuestionManager qm = new QuestionManager(User.Identity.Name);
                clsLinqDataContext ctx = qm.GetDataContext(User.Identity.Name);
                //clsLinqDataContext ctx = new clsLinqDataContext(User.Identity.Name);
                //clsLinqDataContext ctx = new clsLinqDataContext(User.Identity.Name);


                string Connect = ctx.Connection.ToString();
                string bconnect = Connect;

                pnlAdd.Visible = false;
                pnlEdit.Visible = false;
                pnlChild.Visible = false;
                pnlMainBucket.Visible = false;
                pnlMergeUtility.Visible = false;
                TabAnswer.Visible = false;
                pnlAddOption.Visible = false;
                pnlEditOption.Visible = false;

                drpAddStatus.DataValueField = "QuestionStatusID";
                drpAddStatus.DataTextField = "Status";
                drpEditStatus.DataValueField = "QuestionStatusID";
                drpEditStatus.DataTextField = "Status";
                drpAddStatus.DataSource = qm.GetQuestionStatusList();  // (from x in ctx.QuestionStatus.OrderBy(y => y.Status) select new { x.QuestionStatusID, x.Status });
                drpEditStatus.DataSource = qm.GetQuestionStatusList(); // (from x in ctx.QuestionStatus.OrderBy(y => y.Status) select new { x.QuestionStatusID, x.Status });
                drpAddStatus.DataBind();
                drpEditStatus.DataBind();

                drpAddType.DataValueField = "QuestionTypeID";
                drpAddType.DataTextField = "Type";
                drpEditType.DataValueField = "QuestionTypeID";
                drpEditType.DataTextField = "Type";
                drpAddType.DataSource = (from x in ctx.QuestionTypes.OrderBy(y => y.Type) select new { x.QuestionTypeID, x.Type });
                drpEditType.DataSource = (from x in ctx.QuestionTypes.OrderBy(y => y.Type) select new { x.QuestionTypeID, x.Type });
                drpAddType.DataBind();
                drpEditType.DataBind();


                drpAddStatusOption.DataValueField = "OptionStatusID";
                drpAddStatusOption.DataTextField = "Status";
                drpEditStatusOption.DataValueField = "OptionStatusID";
                drpEditStatusOption.DataTextField = "Status";
                drpAddStatusOption.DataSource = qm.GetOptionStatusList();                 // (from x in ctx.OptionStatus.OrderBy(y => y.Status) select new { x.OptionStatusID, x.Status });
                drpEditStatusOption.DataSource = qm.GetOptionStatusList();                // (from x in ctx.OptionStatus.OrderBy(y => y.Status) select new { x.OptionStatusID, x.Status });
                drpAddStatusOption.DataBind();
                drpEditStatusOption.DataBind();

                drpAddTypeOption.DataValueField = "OptionTypeID";
                drpAddTypeOption.DataTextField = "Type";
                drpEditTypeOption.DataValueField = "OptionTypeID";
                drpEditTypeOption.DataTextField = "Type";
                drpAddTypeOption.DataSource = (from x in ctx.OptionTypes.OrderBy(y => y.Type) select new { x.OptionTypeID, x.Type });
                drpEditTypeOption.DataSource = (from x in ctx.OptionTypes.OrderBy(y => y.Type) select new { x.OptionTypeID, x.Type });
                drpAddTypeOption.DataBind();
                drpEditTypeOption.DataBind();


                //chkProcessCheckList.Items.Clear();
                //ProcessManager pm = new ProcessManager(User.Identity.Name);
                //List<Process> pl = pm.GetProcesssThisProject(drpProjectList.SelectedItem.Text);
                //foreach (Process p in pl.OrderBy(x => x.Sequence))
                //{
                //    ListItem x = new ListItem(p.Name, p.ProcessID.ToString());
                //    chkProcessCheckList.Items.Add(x);
                //}
                //chkProcessCheckList.Enabled = false;

                BasicUserUtilities buu = new BasicUserUtilities(User.Identity.Name);
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

                UpdateMainGrid();
                //this.DataBind();
            }
        }


        void FillOptionClientdropdowns()
        {

            ClientManager cm = new ClientManager(User.Identity.Name);
            List<Client> cl = cm.DropDownUserFilterMasterClientPreReceiveList("", "", "", "").OrderBy(x => x.CompanyName).ToList();

            drpAddIDCMasterClient.Items.Clear();
            ListItem z = new ListItem("", "-1");
            drpAddIDCMasterClient.Items.Add(z);
            foreach (Client c in cl)
            {
                ListItem x = new ListItem(c.CompanyName, c.ClientID.ToString());
                drpAddIDCMasterClient.Items.Add(x);
            }

            drpEditIDCMasterClient.Items.Clear();
            ListItem z1 = new ListItem("", "-1");
            drpEditIDCMasterClient.Items.Add(z1);
            foreach (Client c in cl)
            {
                ListItem x = new ListItem(c.CompanyName, c.ClientID.ToString());
                drpEditIDCMasterClient.Items.Add(x);
            }
            //drpAddIDCMasterClient.DataValueField = "ClientID";
            //drpAddIDCMasterClient.DataTextField = "CompanyName";
            //drpAddIDCMasterClient.DataSource = cm.DropDownUserFilterMasterClientPreReceiveList("", "", "", "").OrderBy(x => x.CompanyName);
            //drpAddIDCMasterClient.DataBind();

            //drpEditIDCMasterClient.DataValueField = "ClientID";
            //drpEditIDCMasterClient.DataTextField = "CompanyName";
            //drpEditIDCMasterClient.DataSource = cm.DropDownUserFilterMasterClientPreReceiveList("", "", "", "").OrderBy(x => x.CompanyName);
            //drpEditIDCMasterClient.DataBind();
        }

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





        void MainGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Type type = e.Row.DataItem.GetType();               //.GetGenericArguments()[0];
                //LinkButton bPrint = (LinkButton)e.Row.FindControl("imgPrint");
                CheckBox cb = (CheckBox)e.Row.FindControl("chkSelect");
                GridBlackBeltHeader rec = (GridBlackBeltHeader)e.Row.DataItem;
                //if (bPrint != null)
                //{
                //    bPrint.Attributes.Add("onclick", "PrintScanCodes('" + rec.QuestionID + "','" + rec.Description + "'); return false;");
                //}
                if (cb != null)
                {
                    cb.Attributes.Add("onclick", "CheckboxChecked('" + rec.BlackbeltTransHeaderID + "');");
                }
            }
        }

        void drpProjectList_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateMainGrid();
            //txtProject.Text = drpProjectList.SelectedItem.Text;
        }

        protected void UpdateMainGrid()
        {
            decimal ProjectID = -1;
            decimal.TryParse(drpProjectList.SelectedItem.Value, out ProjectID);
            //clsLinqDataContext ctx = new clsLinqDataContext();
            BlackBeltDashboardManager qm = new BlackBeltDashboardManager(User.Identity.Name);
            //using (clsLinqDataContext ctx = new clsLinqDataContext(User.Identity.Name))
            //{
            //var QData = (from x in qm.GetQuestionsFieldSpecificThisProject(ProjectID).OrderBy(y => y.Name).OrderBy(y => y.Sequence)
            var QData = (from x in qm.GetHeaderList("Error").OrderBy(y => y.ESN).ThenBy(y => y.ESN)
                         select new GridBlackBeltHeader
                         {
                             BlackbeltTransHeaderID = x.BlackbeltTransHeaderID,
                             XMLFileHeaderID = x.XMLFileHeaderID,
                             CreateDate = x.CreateDate,
                             LastUpdateDate = x.LastUpdateDate,
                             ESN = x.ESN,
                             ProjectTag = x.ProjectTag,
                             ProjectName = x.ProjectName,
                             Message = x.Message,
                             Status = x.Status,
                             ProcessStatus = x.ProcessStatus,
                             ReceiveDetailID = x.ReceiveDetailID,
                             ClientLocationID = x.ClientLocationID,
                             ClientLocationScanKey = x.ClientLocationScanKey,
                             ProcessScanKey = x.ProcessScanKey,
                             ProjectID = x.ProjectID,
                             ProcessID = x.ProcessID,
                             CarrierID = x.CarrierID,
                             ManufacturerID = x.ManufacturerID,
                             ModelID = x.ModelID,
                             ColourID = x.ColourID,
                             GradeID = x.GradeID
                         }).ToList();
            MainGrid.DataSource = QData;
            //}
            ////MainGrid.DataSource = (from x in ctx.Questions.OrderBy(y => y.Name).OrderBy(y => y.Sequence) select new { x.Name, x.QuestionID, x.Description, x.QuestionStatus.Status, x.QuestionType.Type, x.Sequence });
            MainGrid.DataBind();

            MainGrid.SelectedIndex = -1;
            pnlChild.Visible = false;
            pnlMainBucket.Visible = false;
            pnlMergeUtility.Visible = false;
            TabAnswer.Visible = false;
            btnEdit.Visible = false;
            btnDelete.Visible = false;
        }
        protected void UpdateChildGrid(decimal KeyID)
        {

            //decimal ProjectID = -1;
            //decimal.TryParse(drpProjectList.SelectedItem.Value, out ProjectID);
            //clsLinqDataContext ctx = new clsLinqDataContext();
            BlackBeltDashboardManager qm = new BlackBeltDashboardManager(User.Identity.Name);
            //using (clsLinqDataContext ctx = new clsLinqDataContext(User.Identity.Name))
            //{
            //var QData = (from x in qm.GetQuestionsFieldSpecificThisProject(ProjectID).OrderBy(y => y.Name).OrderBy(y => y.Sequence)
            var QData = (from x in qm.GetHeaderList("Error").OrderBy(y => y.ESN).ThenBy(y => y.ESN)
                         select new GridBlackBeltHeader
                         {
                             BlackbeltTransHeaderID = x.BlackbeltTransHeaderID,
                             XMLFileHeaderID = x.XMLFileHeaderID,
                             CreateDate = x.CreateDate,
                             LastUpdateDate = x.LastUpdateDate,
                             ESN = x.ESN,
                             ProjectTag = x.ProjectTag,
                             ProjectName = x.ProjectName,
                             Message = x.Message,
                             Status = x.Status,
                             ProcessStatus = x.ProcessStatus,
                             ReceiveDetailID = x.ReceiveDetailID,
                             ClientLocationID = x.ClientLocationID,
                             ClientLocationScanKey = x.ClientLocationScanKey,
                             ProcessScanKey = x.ProcessScanKey,
                             ProjectID = x.ProjectID,
                             ProcessID = x.ProcessID,
                             CarrierID = x.CarrierID,
                             ManufacturerID = x.ManufacturerID,
                             ModelID = x.ModelID,
                             ColourID = x.ColourID,
                             GradeID = x.GradeID
                         }).ToList();
            MainGrid.DataSource = QData;
            //}
            ////MainGrid.DataSource = (from x in ctx.Questions.OrderBy(y => y.Name).OrderBy(y => y.Sequence) select new { x.Name, x.QuestionID, x.Description, x.QuestionStatus.Status, x.QuestionType.Type, x.Sequence });
            MainGrid.DataBind();

            MainGrid.SelectedIndex = -1;
            pnlChild.Visible = false;
            pnlMainBucket.Visible = false;
            pnlMergeUtility.Visible = false;
            TabAnswer.Visible = false;
            btnEdit.Visible = false;
            btnDelete.Visible = false;
















            AnswerManager am = new AnswerManager(User.Identity.Name);
            var ds = (from x in am.GetAnswers().Where(y => y.QuestionID == KeyID).OrderBy(y => y.Name).OrderBy(y => y.Sequence) select new { x.ScanKey, x.MicroKey, x.MacroKey, x.Name, x.OptionID, x.OptionText, x.OptionStatus.Status, x.OptionType.Type, x.Sequence });
            ChildGrid.DataSource = ds;
            ChildGrid.DataBind();
            ChildGrid.SelectedIndex = -1;
            btnEditOption.Visible = false;
            btnDeleteOption.Visible = false;
            //var ds1 = (from x in am.GetAnswers().Where(y => y.QuestionID == KeyID).OrderBy(y => y.Name).OrderBy(y => y.Sequence) select new { x.ScanKey, x.MicroKey, x.MacroKey, x.Name, x.OptionID, x.OptionText, x.OptionStatus.Status, x.OptionType.Type, x.Sequence });
            ChildGrid_Merge.DataSource = ds;
            ChildGrid_Merge.DataBind();
            ChildGrid_Merge.SelectedIndex = -1;
        }

        //void btnSetScanKey_Click(object sender, EventArgs e)
        //{
        //    QuestionManager qm = new QuestionManager(User.Identity.Name);
        //    qm.SetAll_OptionBlankScanCodes();
        //    ScriptManager.RegisterStartupScript(this, GetType(), "OKMessage", "alert('Scancodes updated');", true);

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


        protected void MainGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (MainGrid.SelectedIndex >= 0)
            {
                decimal KeyID = decimal.Parse(MainGrid.SelectedValue.ToString());
                QuestionManager qm = new QuestionManager(User.Identity.Name);
                //var qu = qm.GetQuestions(KeyID);
                //if (qu != null)
                //{
                //    TabAnswer.HeaderText = " (" + qu.Name + ") " + qu.Description;
                //    lblQuestionText.Text = " (" + qu.Name + ") " + qu.Description;
                //    EditKeyID.Text = qu.QuestionID.ToString();
                //    EditName.Text = qu.Name;
                //    EditDescription.Text = qu.Description;
                //    EditSequence.Text = qu.Sequence.ToString();
                //    //EditIFSConditionSequence.Text = qu.IFS_Condition_Sequence.ToString();
                //    EditTableName.Text = qu.TableName;
                //    EditToolTip.Text = qu.HelpText;
                //    //EditBucketCount.Text = qu.BucketCount;
                //    //EditBucketCountOffset.Text = qu.BucketCountOffset;
                //    if (qu.IsHeaderQuestion == true) { EditIsHeaderQuestion.Checked = true; }
                //    else { EditIsHeaderQuestion.Checked = false; }
                //    if (qu.isManditory == true) { EditIsManditory.Checked = true; }
                //    else { EditIsManditory.Checked = false; }

                //    //if (qu.isKeyQuestion == true) { EditIsKeyQuestion.Checked = true; }
                //    //else { EditIsKeyQuestion.Checked = false; }
                //    if (qu.isSearchable == true) { EditIsSearchable.Checked = true; }
                //    else { EditIsSearchable.Checked = false; }

                //    //if (qu.IFS_Condition == true) { EditIFSCondition.Checked = true; }
                //    //else { EditIFSCondition.Checked = false; }



                //    if (qu.ShowVertical == true) { EditShowVertical.Checked = true; }
                //    else { EditShowVertical.Checked = false; }

                //    if (qu.isTimeSpread == true) { EditisTimeSpread.Checked = true; }
                //    else { EditisTimeSpread.Checked = false; }

                //    if (qu.isReadOnly == true) { EditReadOnly.Checked = true; }
                //    else { EditReadOnly.Checked = false; }

                //    if (qu.DisplayColumn != null) { EditDisplayColumn.Text = qu.DisplayColumn.ToString(); }
                //    else { EditDisplayColumn.Text = "1"; }


                //    ListItem _ListItem = drpEditStatus.Items.FindByValue(qu.QuestionStatusID.ToString());
                //    //if (_ListItem == null) { drpEditStatus.SelectedIndex = 0;}
                //    //else { drpAddStatus.SelectedIndex = drpAddStatus.Items.IndexOf(_ListItem);}
                //    //_ListItem = drpEditType.Items.FindByValue(qu.QuestionTypeID.ToString());
                //    //if (_ListItem == null) { drpEditType.SelectedIndex = 0;}
                //    //else { drpEditType.SelectedIndex = drpEditType.Items.IndexOf(_ListItem);}

                //    if (_ListItem == null) { drpEditStatus.SelectedIndex = 0; }
                //    else { drpAddStatus.SelectedIndex = drpAddStatus.Items.IndexOf(_ListItem); }
                //    _ListItem = drpEditType.Items.FindByValue(qu.QuestionTypeID.ToString());
                //    if (_ListItem == null) { drpEditType.SelectedIndex = 0; }
                //    else { drpEditType.SelectedIndex = drpEditType.Items.IndexOf(_ListItem); }
                //}

                //UpdateChildGrid(KeyID);
                pnlChild.Visible = true;
                pnlMainBucket.Visible = false;
                pnlMergeUtility.Visible = false;
                TabAnswer.Visible = true;
                btnEdit.Visible = false;
                btnDelete.Visible = false;
                btnAdd.Visible = false;
                //btnSetScanKey.Visible = false;
                //btnPrintQuestion.Visible = false;

                using (clsLinqDataContext ctx = qm.GetDataContext(User.Identity.Name))
                {
                    if (qm.UserRestrict.AllowUpdate("BlackBelt", KeyID, ctx) == true) { btnEdit.Visible = true; }
                    if (qm.UserRestrict.AllowDelete("BlackBelt", KeyID, ctx) == true) { btnDelete.Visible = true; }
                    if (qm.UserRestrict.AllowAdd("BlackBelt", KeyID, ctx) == true) { btnAdd.Visible = true; }
                    //btnSetScanKey.Visible = btnAdd.Visible;
                    //btnPrintQuestion.Visible = btnAdd.Visible;

                }
            }
            else
            {
                pnlChild.Visible = false;
                pnlMainBucket.Visible = false;
                pnlMergeUtility.Visible = false;
                TabAnswer.Visible = false;
                btnEdit.Visible = false;
                btnDelete.Visible = false;
                //btnSetScanKey.Visible = false;
                //btnPrintQuestion.Visible = false;
            }
        }

        protected void ChildGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnEditBucket.Visible = false;
            btnDeleteBucket.Visible = false;
            btnAddOption.Visible = btnAdd.Visible;
            btnEditOption.Visible = false;
            btnDeleteOption.Visible = false;
            if (ChildGrid.SelectedIndex >= 0)
            {
                decimal KeyID = decimal.Parse(ChildGrid.SelectedValue.ToString());
                //                clsLinqDataContext ctx = new clsLinqDataContext();
                //                Option qu = ctx.Options.FirstOrDefault(x => x.OptionID == KeyID);
                AnswerManager am = new AnswerManager(User.Identity.Name);
                Option qu = am.GetAnswer(KeyID);

                if (qu != null)
                {
                    EditScanKey.Text = qu.ScanKey;
                    EditMacroKey.Text = qu.MacroKey;
                    EditMicroKey.Text = qu.MicroKey;
                    EditKeyIDOption.Text = qu.OptionID.ToString();
                    EditNameOption.Text = qu.Name;
                    EditDescriptionOption.Text = qu.OptionText;
                    EditSequenceOption.Text = qu.Sequence.ToString();
                    //EditBucketCountOption.Text = qu.BucketCount;
                    //EditBucketCountOffsetOption.Text = qu.BucketCountOffset;

                    ListItem _ListItem = drpEditStatusOption.Items.FindByValue(qu.OptionStatusID.ToString());
                    if (_ListItem == null) { drpEditStatusOption.SelectedIndex = 0; }
                    else { drpEditStatusOption.SelectedIndex = drpEditStatusOption.Items.IndexOf(_ListItem); }


                    _ListItem = drpEditTypeOption.Items.FindByValue(qu.OptionTypeID.ToString());
                    if (_ListItem == null) { drpEditTypeOption.SelectedIndex = 0; }
                    else { drpEditTypeOption.SelectedIndex = drpEditTypeOption.Items.IndexOf(_ListItem); }

                    _ListItem = drpEditIDCMasterClient.Items.FindByValue(qu.IDC_ClientID.ToString());
                    if (_ListItem == null) { drpEditIDCMasterClient.SelectedIndex = 0; }
                    else { drpEditIDCMasterClient.SelectedIndex = drpEditIDCMasterClient.Items.IndexOf(_ListItem); }

                    EditFriendlyName.Text = qu.IDC_FriendlyName;

                }
                //UpdateAnswerGridBuckets();
                pnlMainBucket.Visible = true;
                //pnlMergeUtility.Visible = true;
                ResetMergeUtility();
                btnEditOption.Visible = btnEdit.Visible;
                btnDeleteOption.Visible = btnDelete.Visible;
                btnAddOption.Visible = btnAdd.Visible;
            }

        }
        private void ResetMergeUtility()
        {
            pnlMergeUtility.Visible = true;
            foreach (System.Web.UI.WebControls.GridViewRow Row in ChildGrid_Merge.Rows)
            {
                CheckBox cb = (CheckBox)Row.FindControl("chkSelect");
                cb.Checked = false;
                Label l = (Label)Row.FindControl("lblMergeInto");
                l.Text = "";
            }
        }


        #region HeaderDetail
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            //AddName.Text = "";
            //AddDescription.Text = "";
            //AddSequence.Text = "";
            ////AddIFSConditionSequence.Text = "";
            //AddIsHeaderQuestion.Checked = false;
            //AddIsManditory.Checked = false;
            ////AddIsKeyQuestion.Checked = false;
            //AddIsSearchable.Checked = false;
            ////AddIFSCondition.Checked = false;
            //AddShowVertical.Checked = false;
            //AddisTimeSpread.Checked = false;
            //AddReadOnly.Checked = false;
            //AddDisplayColumn.Text = "1";
            //pnlMainView.Visible = false;
            //pnlAdd.Visible = true;
            ////AddBucketCount.Text = "";
            ////AddBucketCountOffset.Text = "";
            GoTop();

        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            //pnlMainView.Visible = false;
            //pnlEdit.Visible = true;
            //GoTop();
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            //// Delete the Question and all the attached answers.
            //// Delete the answers.
            if (MainGrid.SelectedIndex >= 0)
            {
                decimal MainGridKeyID = decimal.Parse(MainGrid.SelectedValue.ToString());
                //QuestionManager qm = new QuestionManager(User.Identity.Name);
                //try
                //{
                //    qm.DeleteQuestion(MainGridKeyID);
                //    ScriptManager.RegisterStartupScript(this, GetType(), "Question Delete", "alert('Delete Success');", true);
                //}
                //catch (UserAccessControlException ex)
                //{
                //    ScriptManager.RegisterStartupScript(this, GetType(), "Question Delete", "alert('" + ex.Message + "');", true);
                //}
                //UpdateMainGrid();
                //UpdateChildGrid(-1);
                //}
            }
        }
        ///------------------------------------------------------------------------------------
        //protected void AddQuestionOK_Click(object sender, EventArgs e)
        //{
        //    int Sequence = 0;
        //    int IFSConditionSequebnce = 0;
        //    if (AddName.Text.Length == 0) { return; }
        //    if (AddDescription.Text.Length == 0) { return; }
        //    if (drpAddStatus.SelectedIndex < 0) { return; }
        //    if (drpAddType.SelectedIndex < 0) { return; }
        //    if (AddSequence.Text.Length == 0 || int.TryParse(AddSequence.Text, out Sequence) == false) { Sequence = 100; }
        //    IFSConditionSequebnce = 100;


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
        //        qu.IFS_Condition = false;
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
        //    IFSConditionSequence = 100;
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
        //        qu.IFS_Condition = false;
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
        #endregion


        #region ProcessBucket
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
        #endregion


        //#region AnswerBucket
        //protected void btnAddBucket_Click(object sender, EventArgs e)
        //{
        //    //AddName.Text = "";
        //    //AddDescription.Text = "";
        //    //AddSequence.Text = "";
        //    //AddIsHeaderQuestion.Checked = false;
        //    //AddIsManditory.Checked = false;
        //    //AddIsKeyQuestion.Checked = false;
        //    //AddIsSearchable.Checked = false;
        //    //AddDisplayColumn.Text = "1";
        //    pnlMainView.Visible = false;
        //    pnlAddBucket.Visible = true;
        //    //AddBucketCount.Text = "";
        //    //AddBucketCountOffset.Text = "";
        //    //GoTop();

        //}
        //protected void btnEditBucket_Click(object sender, EventArgs e)
        //{
        //    pnlMainView.Visible = false;
        //    pnlEditBucket.Visible = true;
        //    //GoTop();
        //}
        //protected void btnDeleteBucket_Click(object sender, EventArgs e)
        //{
        //    ////// Delete the Question and all the attached answers.
        //    ////// Delete the answers.
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
        //        decimal KeyID = decimal.Parse(MainGrid.SelectedValue.ToString());
        //        UpdateAnswerGridBuckets(KeyID);
        //        //}
        //    }
        //}
        /////------------------------------------------------------------------------------------
        //protected void AddBucketOK_Click(object sender, EventArgs e)
        //{
        //    //int Sequence = 0;
        //    //if (AddName.Text.Length == 0) { return; }
        //    //if (AddDescription.Text.Length == 0) { return; }
        //    //if (drpAddStatus.SelectedIndex < 0) { return; }
        //    //if (drpAddType.SelectedIndex < 0) { return; }
        //    //if (AddSequence.Text.Length == 0 || int.TryParse(AddSequence.Text, out Sequence) == false) { Sequence = 100; }


        //    //try
        //    //{
        //    //    QuestionManager qm = new QuestionManager(User.Identity.Name);
        //    //    Question qu = qm.NewQuestion();
        //    //    //clsLinqDataContext ctx = new clsLinqDataContext();
        //    //    //Question qu = new Question();
        //    //    qu.Name = AddName.Text;
        //    //    qu.TableName = AddTableName.Text;
        //    //    qu.HelpText = AddToolTip.Text;
        //    //    qu.Description = AddDescription.Text;
        //    //    qu.QuestionStatusID = decimal.Parse(drpAddStatus.SelectedItem.Value);
        //    //    qu.QuestionTypeID = decimal.Parse(drpAddType.SelectedItem.Value);
        //    //    qu.Sequence = Sequence;
        //    //    qu.IsHeaderQuestion = AddIsHeaderQuestion.Checked;
        //    //    qu.isManditory = AddIsManditory.Checked;
        //    //    qu.isKeyQuestion = AddIsKeyQuestion.Checked;
        //    //    qu.isSearchable = AddIsSearchable.Checked;
        //    //    decimal dCol = 1;
        //    //    if (decimal.TryParse(AddDisplayColumn.Text, out dCol) == false) { dCol = 1; }
        //    //    qu.DisplayColumn = dCol;
        //    //    qu.BucketCount = AddBucketCount.Text;
        //    //    qu.BucketCountOffset = AddBucketCountOffset.Text;
        //    //    qm.InsertQuestion(qu);

        //    //    UpdateMainGrid();
        //    pnlMainView.Visible = true;
        //    pnlAddBucket.Visible = false;
        //    //    GoTop();

        //    //}
        //    //catch (UserAccessControlException ex)
        //    //{
        //    //    ScriptManager.RegisterStartupScript(this, GetType(), "Question Delete", "alert('" + ex.Message + "');", true);
        //    //}
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
        //    //int Sequence = 0;
        //    //if (EditName.Text.Length == 0) { return; }
        //    //if (EditDescription.Text.Length == 0) { return; }
        //    //if (drpEditStatus.SelectedIndex < 0) { return; }
        //    //if (drpEditType.SelectedIndex < 0) { return; }
        //    //if (EditSequence.Text.Length == 0 || int.TryParse(EditSequence.Text, out Sequence) == false) { Sequence = 100; }
        //    //decimal KeyID = decimal.Parse(EditKeyID.Text);
        //    //try
        //    //{
        //    //    QuestionManager qm = new QuestionManager(User.Identity.Name);
        //    //    Question qu = qm.NewQuestion();
        //    //    qu.QuestionID = KeyID;
        //    //    qu.Name = EditName.Text;
        //    //    qu.TableName = EditTableName.Text;
        //    //    qu.HelpText = EditToolTip.Text;
        //    //    qu.Description = EditDescription.Text;
        //    //    qu.QuestionStatusID = decimal.Parse(drpEditStatus.SelectedItem.Value);
        //    //    qu.QuestionTypeID = decimal.Parse(drpEditType.SelectedItem.Value);
        //    //    //qu.HelpText = "";
        //    //    qu.Sequence = Sequence;
        //    //    qu.IsHeaderQuestion = EditIsHeaderQuestion.Checked;
        //    //    qu.isManditory = EditIsManditory.Checked;

        //    //    qu.isKeyQuestion = EditIsKeyQuestion.Checked;
        //    //    qu.isSearchable = EditIsSearchable.Checked;
        //    //    qu.BucketCount = EditBucketCount.Text;
        //    //    qu.BucketCountOffset = EditBucketCountOffset.Text;
        //    //    decimal dCol = 1;
        //    //    if (decimal.TryParse(EditDisplayColumn.Text, out dCol) == false) { dCol = 1; }
        //    //    qu.DisplayColumn = dCol;


        //    //    qm.UpdateQuestion(qu);
        //    //    UpdateMainGrid();
        //    pnlMainView.Visible = true;
        //    pnlEditBucket.Visible = false;
        //    //    GoTop();
        //    //}
        //    //catch (UserAccessControlException ex)
        //    //{
        //    //    ScriptManager.RegisterStartupScript(this, GetType(), "Question Update", "alert('" + ex.Message + "');", true);
        //    //}
        //}
        //protected void EditBucketCancel_Click(object sender, EventArgs e)
        //{
        //    pnlMainView.Visible = true;
        //    pnlEditBucket.Visible = false;
        //}
        //#endregion

        #region OptionDetail

        protected void btnAddOption_Click(object sender, EventArgs e)
        {
            AddScanKeyOption.Text = "";
            AddNameOption.Text = "";
            AddDescriptionOption.Text = "";
            AddSequenceOption.Text = "";
            AddMacroKey.Text = "";
            AddMicroKey.Text = "";
            AddTableName.Text = "";
            AddToolTip.Text = "";
            pnlMainView.Visible = false;
            pnlAddOption.Visible = true;
            //AddBucketCountOption.Text = "";
            //AddBucketCountOffsetOption.Text = "";
            GoTop();
        }
        protected void btnEditOption_Click(object sender, EventArgs e)
        {
            pnlMainView.Visible = false;
            pnlEditOption.Visible = true;
            GoTop();
        }
        protected void btnDeleteOption_Click(object sender, EventArgs e)
        {
            //// Delete the answers.
            //if (ChildGrid.SelectedIndex >= 0)
            //{
            //    try
            //    {
            //        decimal MainGridKeyID = decimal.Parse(MainGrid.SelectedValue.ToString());
            //        decimal KeyID = decimal.Parse(ChildGrid.SelectedValue.ToString());
            //        AnswerManager am = new AnswerManager(User.Identity.Name);
            //        am.DeleteAnswer(KeyID);
            //        UpdateChildGrid(decimal.Parse(MainGrid.SelectedValue.ToString()));
            //    }
            //    catch (UserAccessControlException ex)
            //    {
            //        ScriptManager.RegisterStartupScript(this, GetType(), "Answer Delete", "alert('" + ex.Message + "');", true);
            //    }
            //    GoTop();
            //}
        }



        #region Option AddEdit
        protected void AddOptionOK_Click(object sender, EventArgs e)
        {
            int Sequence = 0;
            //if (AddScanKeyOption.Text.Length == 0) { return; }
            if (AddNameOption.Text.Length == 0) { return; }
            if (AddDescriptionOption.Text.Length == 0) { return; }
            if (drpAddStatusOption.SelectedIndex < 0) { return; }
            if (drpAddTypeOption.SelectedIndex < 0) { return; }
            if (AddSequenceOption.Text.Length == 0 || int.TryParse(AddSequenceOption.Text, out Sequence) == false) { Sequence = 100; }
            try
            {
                AnswerManager am = new AnswerManager(User.Identity.Name);
                Option qu = am.NewAnswer();

                //clsLinqDataContext ctx = new clsLinqDataContext();
                //Option qu = new Option();
                qu.QuestionID = decimal.Parse(MainGrid.SelectedValue.ToString());
                qu.ScanKey = AddScanKeyOption.Text;
                qu.MacroKey = AddMacroKey.Text;
                qu.MicroKey = AddMicroKey.Text;
                qu.Name = AddNameOption.Text.ToUpper();
                qu.OptionText = AddDescriptionOption.Text;
                qu.OptionStatusID = decimal.Parse(drpAddStatusOption.SelectedItem.Value);
                qu.OptionTypeID = decimal.Parse(drpAddTypeOption.SelectedItem.Value);
                qu.IDC_ClientID = decimal.Parse(drpAddIDCMasterClient.SelectedItem.Value);
                qu.IDC_FriendlyName = AddFriendlyName.Text;

                qu.Sequence = Sequence;
                qu.BucketCount = "";// AddBucketCountOption.Text;
                qu.BucketCountOffset = "";// AddBucketCountOffsetOption.Text;
                am.InsertAnswer(qu);
                //ctx.Options.InsertOnSubmit(qu);
                //ctx.SubmitChanges();
                UpdateChildGrid(decimal.Parse(MainGrid.SelectedValue.ToString()));

                pnlMainView.Visible = true;
                pnlAddOption.Visible = false;
                GoTop();
            }
            catch (UserAccessControlException ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Answer Add", "alert('" + ex.Message + "');", true);
            }
        }
        protected void AddOptionCancel_Click(object sender, EventArgs e)
        {
            pnlMainView.Visible = true;
            pnlAddOption.Visible = false;
            GoTop();
        }
        protected void EditOptionOK_Click(object sender, EventArgs e)
        {
            log.LogIt("**** Edit Option OK Click");
            int Sequence = 0;
            //   if (EditScanKey.Text.Length == 0) { return; }
            if (EditNameOption.Text.Length == 0) { return; }
            if (EditDescriptionOption.Text.Length == 0) { return; }
            if (drpEditStatusOption.SelectedIndex < 0) { return; }
            if (drpEditTypeOption.SelectedIndex < 0) { return; }
            log.LogIt("**** Maint Question After Return Edits");
            if (EditSequenceOption.Text.Length == 0 || int.TryParse(EditSequenceOption.Text, out Sequence) == false) { Sequence = 100; }

            decimal KeyID = decimal.Parse(EditKeyIDOption.Text);
            decimal QKeyID = decimal.Parse(MainGrid.SelectedValue.ToString());
            try
            {
                AnswerManager am = new AnswerManager(User.Identity.Name);
                Option qu = am.NewAnswer();
                //            clsLinqDataContext ctx = new clsLinqDataContext();
                //            Option qu = ctx.Options.FirstOrDefault(x => x.OptionID == KeyID);
                if (qu != null)
                {
                    qu.QuestionID = QKeyID;
                    qu.OptionID = KeyID;
                    qu.ScanKey = EditScanKey.Text;
                    qu.MacroKey = EditMacroKey.Text;
                    qu.MicroKey = EditMicroKey.Text;
                    qu.Name = EditNameOption.Text.ToUpper();
                    qu.OptionText = EditDescriptionOption.Text;
                    qu.OptionStatusID = decimal.Parse(drpEditStatusOption.SelectedItem.Value);
                    qu.OptionTypeID = decimal.Parse(drpEditTypeOption.SelectedItem.Value);
                    qu.IDC_ClientID = decimal.Parse(drpEditIDCMasterClient.SelectedItem.Value);
                    qu.IDC_FriendlyName = EditFriendlyName.Text;
                    qu.Sequence = Sequence;
                    qu.BucketCount = ""; //EditBucketCountOption.Text;
                    qu.BucketCountOffset = "";  // EditBucketCountOffsetOption.Text;
                    am.UpdateAnswer(qu);
                    log.LogIt("**** Update Answer Executed:" + QKeyID.ToString());
                    //qu.CreateUser = User.Identity.Name;
                    //qu.LastUpdateDate = DateTime.Now;
                    //qu.LastUpdateUser = User.Identity.Name;
                    //ctx.SubmitChanges();
                }
                UpdateChildGrid(decimal.Parse(MainGrid.SelectedValue.ToString()));
                pnlMainView.Visible = true;
                pnlEditOption.Visible = false;
                GoTop();
            }
            catch (UserAccessControlException ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Question Update", "alert('" + ex.Message + "');", true);
            }
        }
        protected void EditOptionCancel_Click(object sender, EventArgs e)
        {
            pnlMainView.Visible = true;
            pnlEditOption.Visible = false;
            GoTop();
        }
        #endregion




        protected void GoTop()
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "GoTop", "goTopofScreen();", true);
        }
        #endregion

    }
}
