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
    public partial class Maint_Process_BKUP : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MainGrid.SelectedIndexChanged += new EventHandler(MainGrid_SelectedIndexChanged);
            grdMasterBucketTransaction.SelectedIndexChanged += new EventHandler(grdMasterBucketTransaction_SelectedIndexChanged);
            btnPrintScanCodes.Click += new EventHandler(btnPrintScanCodes_Click);
            drpProjectList.SelectedIndexChanged += new EventHandler(drpProjectList_SelectedIndexChanged);
            //btnCopyFrom.Click += new EventHandler(btnCopyFrom_Click);
            btnCopyFrom.Attributes.Add("OnClick", "MoveProcessIn(); return false;");

            //ChildGrid.SelectedIndexChanged += new EventHandler(ChildGrid_SelectedIndexChanged);
            if (!IsPostBack)
            {
                hdnUserName.Value = User.Identity.Name;
                clsLinqDataContext ctx = new clsLinqDataContext();
                // ProcessManager qm = new ProcessManager(User.Identity.Name);
                pnlAdd.Visible = false;
                pnlEdit.Visible = false;
                pnlProcessAnswer.Visible = false;
                pnlProcessNextMove.Visible = false;
                pnlProcessBinLocation.Visible = false;
                //pnlChild.Visible = false;
                //pnlAddOption.Visible = false;
                //pnlEditOption.Visible = false;

                drpAddStatus.DataValueField = "ProcessStatusID";
                drpAddStatus.DataTextField = "Status";
                drpEditStatus.DataValueField = "ProcessStatusID";
                drpEditStatus.DataTextField = "Status";
                drpAddStatus.DataSource = (from x in ctx.ProcessStatus.OrderBy(y => y.Status) select new { x.ProcessStatusID, x.Status });
                drpEditStatus.DataSource = (from x in ctx.ProcessStatus.OrderBy(y => y.Status) select new { x.ProcessStatusID, x.Status });
                drpAddStatus.DataBind();
                drpEditStatus.DataBind();
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
                //DataBind();
            }
            //btnAllRight.Attributes.Add("click", "MoveItem(lstSource,lstTarget);return false;");
            //btnRight.Attributes.Add("click", "MoveItem(lstSource,lstTarget);return false;");
            //btnAllLeft.Attributes.Add("click", "MoveItem(lstTarget,lstSource);return false;");
            //btnLeft.Attributes.Add("click", "MoveItem(lstTarget,lstSource);return false;");
        }

        void grdMasterBucketTransaction_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnEditBucket.Visible = false;
            btnAddBucket.Visible = false;
            if (grdMasterBucketTransaction.SelectedIndex >= 0)
            {
                decimal KeyID = decimal.Parse(grdMasterBucketTransaction.SelectedValue.ToString());
                btnAddBucket.Visible = true;
                btnEditBucket.Visible = true;
                btnDeleteBucket.Visible = true;
                btnPrintScanCodes.Visible = true;
                chkEditBucket.Checked = false;
                chkEditSingle.Checked = false;
                txtEditCount.Text = "";
                txtEditAction.Text = "";

                chkAddBucket.Checked = false;
                chkAddSingle.Checked = true;
                txtAddCount.Text = "1";
                txtAddAction.Text = "";

                MasterBucketTransactionManager btm = new MasterBucketTransactionManager(User.Identity.Name);
                MasterBucketTransaction bt = btm.Get(KeyID);
                if (bt != null)
                {
                    chkEditBucket.Checked = false;
                    chkEditSingle.Checked = false;
                    if (bt.Active == true) { chkEditBucket.Checked = true; }
                    if (bt.OneOnly == true) { chkEditSingle.Checked = true; }
                    txtEditCount.Text = bt.Count.ToString();
                    txtEditAction.Text = bt.Action;

                    //pnlMainBucket.Visible = true;
                }
            }
        }

        void drpProjectList_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateMainGrid();
        }
        protected void UpdateProcessDropDown()
        {

            ProcessManager prm = new ProcessManager(User.Identity.Name);
            decimal ProjectID = -1;
            decimal.TryParse(drpProjectList.SelectedItem.Value, out ProjectID);

            drpProcessList.Items.Clear();
            List<Process> Processes = prm.GetMasterProcessList(ProjectID);
            foreach (Process p in Processes)
            {
                ListItem li = new ListItem("(" + (p.ButtonText == null || p.ButtonText.Length == 0 ? p.Name : p.ButtonText) + ") " + p.Description, p.ProcessID.ToString());
                drpProcessList.Items.Add(li);
            }

            //drpProcessList.DataValueField = "ProcessID";
            //drpProcessList.DataTextField = "Description";
            //drpProcessList.DataSource = prm.GetMasterProcessList(ProjectID);
            //drpProcessList.DataBind();
            //drpProcessList.SelectedIndex = 0;
        }
        protected void UpdateMainGrid()
        {
            decimal ProjectID = -1;
            decimal.TryParse(drpProjectList.SelectedItem.Value, out ProjectID);
            ProcessManager qm = new ProcessManager(User.Identity.Name);
            //MainGrid.DataSource = (from x in qm.GetProcesssDeferedThisProject(ProjectID).OrderBy(y => y.Name).OrderBy(y => y.Sequence)
            MainGrid.DataSource = (from x in qm.GetMasterProcessDeferedList(ProjectID).OrderBy(y => y.Name).OrderBy(y => y.Sequence)
                                   select new
                                   {
                                       x.ScanKey,
                                       x.MacroKey,
                                       x.Name,
                                       x.ProcessID,
                                       x.Description,
                                       x.Description_Client,
                                       x.ProcessStatus.Status,
                                       x.Sequence,
                                       x.AllowAdd,
                                       x.AllowDelete,
                                       x.AllowScan,
                                       x.AllowSelect,
                                       x.AllowUpdate,
                                       x.ShowCompletedStatus,
                                       x.ButtonText,
                                       x.RMASuffix,
                                       x.ShowTAT,
                                       x.CanJumpProject,
                                       x.isReadOnly,
                                       x.TurnStickyOn,
                                       x.MinutesToYellow,
                                       x.MinutesToRed,
                                       x.DisablePrint,
                                       x.ForcePrintOnSave,
                                       x.IFSDirectiveType
                                   }).OrderBy(x=> x.Name);
            MainGrid.DataBind();
            MainGrid.SelectedIndex = -1;
            //pnlChild.Visible = false;
            //btnBinLocation.Visible = false;
            pnlMainBucket.Visible = false;
            btnEdit.Visible = false;
            btnDelete.Visible = false;
            btnPrintScanCodes.Visible = false;
            btnQuestion.Visible = false;
            btnNextMove.Visible = false;
            UpdateProcessDropDown();
        }

        protected void MainGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlMainBucket.Visible = false;
            btnEditBucket.Visible = false;
            btnDeleteBucket.Visible = false;
            btnAdd.Visible = false;
            btnEdit.Visible = false;
            btnDelete.Visible = false;
            btnPrintScanCodes.Visible = false;
            btnQuestion.Visible = false;
            btnNextMove.Visible = false;
            txtSelectChoice.Text = "";
            if (MainGrid.SelectedIndex >= 0)
            {
                decimal KeyID = decimal.Parse(MainGrid.SelectedValue.ToString());
                ProcessManager pm = new ProcessManager(User.Identity.Name);
                Process process = pm.GetProcesss(KeyID);
                if (process != null)
                {
                    pnlMainBucket.Visible = false;
                    btnAdd.Visible = false;
                    btnEdit.Visible = false;
                    btnDelete.Visible = false;
                    btnPrintScanCodes.Visible = false;
                    btnQuestion.Visible = false;
                    btnNextMove.Visible = false;
                    if (process.AllowAdd == true) { btnAdd.Visible = true; }
                    if (process.AllowUpdate == true) { btnEdit.Visible = true; }
                    if (process.AllowDelete == true) { btnDelete.Visible = true; }
                    if (process.AllowUpdate == true) { btnQuestion.Visible = true; }
                    //if (process.AllowUpdate == true) { btnNextMove.Visible = true; }
                    //if (process.AllowUpdate == true) { btnBinLocation.Visible = true; }
                    pnlMainBucket.Visible = true;
                    btnPrintScanCodes.Visible = true;
                    DisplaySelected();
                    UpdateMainGridBuckets();
                }
            }
        }

        protected void UpdateMainGridBuckets()
        {
            decimal KeyID = decimal.Parse(MainGrid.SelectedValue.ToString());
            MasterBucketTransactionManager tm = new MasterBucketTransactionManager(User.Identity.Name);
            grdMasterBucketTransaction.DataSource = tm.ProcessTransactions(KeyID);
            grdMasterBucketTransaction.DataBind();
            grdMasterBucketTransaction.SelectedIndex = -1;
            btnEditBucket.Visible = false;
            btnDeleteBucket.Visible = false;
        }

        #region ProcessBucket
        protected void btnAddBucket_Click(object sender, EventArgs e)
        {
            pnlMainView.Visible = false;
            pnlAddBucket.Visible = true;
        }
        protected void btnEditBucket_Click(object sender, EventArgs e)
        {
            pnlMainView.Visible = false;
            pnlEditBucket.Visible = true;
        }
        protected void btnDeleteBucket_Click(object sender, EventArgs e)
        {
            if (grdMasterBucketTransaction.SelectedIndex >= 0)
            {
                decimal BucketKeyID = decimal.Parse(grdMasterBucketTransaction.SelectedValue.ToString());
                MasterBucketTransactionManager tm = new MasterBucketTransactionManager(User.Identity.Name);
                try
                {
                    tm.Delete(BucketKeyID);
                    ScriptManager.RegisterStartupScript(this, GetType(), "Bucket Delete", "alert('Delete Success');", true);
                }
                catch (UserAccessControlException ex)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Bucket Delete", "alert('" + ex.Message + "');", true);
                }
                UpdateMainGridBuckets();
            }
        }
        ///------------------------------------------------------------------------------------
        protected void AddBucketOK_Click(object sender, EventArgs e)
        {
            int Count = 0;
            if (int.TryParse(txtAddCount.Text, out Count) == false) { Count = 0; }
            decimal KeyID = decimal.Parse(MainGrid.SelectedValue.ToString());
            MasterBucketTransactionManager btm = new MasterBucketTransactionManager(User.Identity.Name);
            btm.InsertProcess(KeyID, chkAddBucket.Checked, txtAddAction.Text, Count, chkAddSingle.Checked);
            UpdateMainGridBuckets();
            pnlMainView.Visible = true;
            pnlAddBucket.Visible = false;
            return;
        }
        protected void AddBucketCancel_Click(object sender, EventArgs e)
        {
            pnlMainView.Visible = true;
            pnlAddBucket.Visible = false;
            //GoTop();
        }
        protected void EditBucketOK_Click(object sender, EventArgs e)
        {
            int Count = 0;
            if (int.TryParse(txtEditCount.Text, out Count) == false) { Count = 0; }
            decimal KeyID = decimal.Parse(grdMasterBucketTransaction.SelectedValue.ToString());
            MasterBucketTransactionManager btm = new MasterBucketTransactionManager(User.Identity.Name);
            btm.Update(KeyID, chkEditBucket.Checked, txtEditAction.Text, Count, chkEditSingle.Checked);
            UpdateMainGridBuckets();
            pnlMainView.Visible = true;
            pnlEditBucket.Visible = false;
        }
        protected void EditBucketCancel_Click(object sender, EventArgs e)
        {
            pnlMainView.Visible = true;
            pnlEditBucket.Visible = false;
        }
        #endregion



        #region ProcessDetail

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            AddName.Text = "";
            AddButton.Text = "";
            AddRMASuffix.Text = "";
            AddDescription.Text = "";
            AddSequence.Text = "";
            AddDescription_Client.Text = "";
            AddScanKey.Text = "";
            AddMacroKey.Text = "";
            AddTurnStickyOn.Checked = false;
            AddAllowXBINX.Checked = false;
            chkAddDisablePrint.Checked = false;
            chkAddForcePrintOnSave.Checked = false;
            //AddBucketCount.Text = "";
            //AddBucketCountOffset.Text = "";
            AddIFSDirectiveType.Text = "";
            AddMinutesToRed.Text = "0";
            AddMinutesToYellow.Text = "0";
            AddisReadOnly.Checked = false;
            AddShowTat.Checked = false;
            AddCanJumpProject.Checked = false;
            AddShowCompletedStatus.Checked = false;
            pnlMainView.Visible = false;
            pnlAdd.Visible = true;
        }

        protected void DisplaySelected()
        {

            txtSelectChoice.Text = "";
            decimal KeyID = decimal.Parse(MainGrid.SelectedValue.ToString());
            ProcessManager pm = new ProcessManager(User.Identity.Name);
            // QuestionManager qm = new QuestionManager(User.Identity.Name);
            Process process = pm.GetProcesss(KeyID);
            if (process != null)
            {
                txtSelectChoice.Text = string.Format("{0} - {1}", process.Name, process.Description);
            }
        }


        protected void UpdateEditScreen()
        {
            decimal KeyID = decimal.Parse(MainGrid.SelectedValue.ToString());
            ProcessManager pm = new ProcessManager(User.Identity.Name);
            // QuestionManager qm = new QuestionManager(User.Identity.Name);
            Process process = pm.GetProcesss(KeyID);
            MasterProcessWaitTime wt = pm.GetProcesssWaitTime(-1, KeyID, -1);
            //lblMisc.Text = "Edit Setup - Start";

            if (process != null)
            {
                //lblMisc.Text = "Edit Setup - Move";

                lblNextMove.Text = "(" + process.Name + ")" + process.Description;
                EditKeyID.Text = process.ProcessID.ToString();
                EditName.Text = process.Name;
                EditRMASuffix.Text = process.RMASuffix;
                EditButton.Text = process.ButtonText;
                EditDescription.Text = process.Description;
                EditSequence.Text = process.Sequence.ToString();
                EditDescription_Client.Text = process.Description_Client;
                EditScanKey.Text = process.ScanKey;
                EditMacroKey.Text = process.MacroKey;




                //EditBucketCount.Text = ""; //process.BucketCount;
                //EditBucketCountOffset.Text = ""; // process.BucketCountOffset;
                if (process.TurnStickyOn == null || process.TurnStickyOn == false) { EditTurnStickyOn.Checked = false; } else { EditTurnStickyOn.Checked = true; }
                if (process.AllowXBINX == null || process.AllowXBINX == false) { EditAllowXBINX.Checked = false; } else { EditAllowXBINX.Checked = true; }

                
                if (process.DisablePrint == null || process.DisablePrint == false) { chkEditDisablePrint.Checked = false; } else { chkEditDisablePrint.Checked = true; }
                if (process.ForcePrintOnSave == null || process.ForcePrintOnSave == false) { chkEditForcePrintOnSave.Checked = false; } else { chkEditForcePrintOnSave.Checked = true; }

                //////////////////////////////////////

                EditMinutesToYellow.Text = "0";
                if (wt != null && wt.MinutesToYellow != null)
                {
                    EditMinutesToYellow.Text = wt.MinutesToYellow.ToString();
                }
                EditMinutesToRed.Text = "0";
                if (wt != null && wt.MinutesToRed != null)
                {
                    EditMinutesToRed.Text = wt.MinutesToRed.ToString();
                }
                EditIFSDirectiveType.Text = "";
                if (process != null && process.IFSDirectiveType != null)
                {
                    EditIFSDirectiveType.Text = process.IFSDirectiveType.ToString();
                }

                //////////////////////////////////////



                if (process.ShowCompletedStatus == null || process.ShowCompletedStatus == false)
                {
                    EditShowCompletedStatus.Checked = false;
                }
                else { EditShowCompletedStatus.Checked = true; }
                if (process.isReadOnly == null || process.isReadOnly == false)
                {
                    EditisReadOnly.Checked = false;
                }
                else { EditisReadOnly.Checked = true; }
                if (process.ShowTAT == null || process.ShowTAT == false)
                {
                    EditShowTat.Checked = false;
                }
                else { EditShowTat.Checked = true; }


                if (process.CanJumpProject == null || process.CanJumpProject == false)
                {
                    EditCanJumpProject.Checked = false;
                }
                else { EditCanJumpProject.Checked = true; }


                ListItem _ListItem = drpEditStatus.Items.FindByValue(process.StatusID.ToString());
                if (_ListItem == null) { drpEditStatus.SelectedIndex = 0; }
                else { drpAddStatus.SelectedIndex = drpAddStatus.Items.IndexOf(_ListItem); }
                //lblMisc.Text = "Edit Setup - Done";
            }

        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            //lblMisc.Text = "";
            UpdateEditScreen();
            pnlMainView.Visible = false;
            pnlEdit.Visible = true;
        }

        void btnPrintScanCodes_Click(object sender, EventArgs e)
        {
            if (MainGrid.SelectedIndex >= 0)
            {
                decimal MainGridKeyID = decimal.Parse(MainGrid.SelectedValue.ToString());
                ScriptManager.RegisterStartupScript(this, GetType(), "Print", "PrintScanCodes(" + MainGridKeyID.ToString() + ");", true);
            }
        }


        protected void btnDelete_Click(object sender, EventArgs e)
        {
            //// Delete the Process and all the attached answers.
            //// Delete the answers.
            if (MainGrid.SelectedIndex >= 0)
            {
                decimal MainGridKeyID = decimal.Parse(MainGrid.SelectedValue.ToString());

                ProcessManager pm = new ProcessManager(User.Identity.Name);
                Process OPS = pm.GetProcesss(MainGridKeyID);
                try
                {
                    pm.DeleteProcess(MainGridKeyID);
                    ScriptManager.RegisterStartupScript(this, GetType(), "Process Delete", "alert('Delete Success');", true);
                }
                catch (UserAccessControlException ex)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Process Delete", "alert('" + ex.Message + "');", true);
                }
                UpdateMainGrid();
            }
        }

        protected void UpdateTagQuestionScreen()
        {
            decimal KeyID = decimal.Parse(MainGrid.SelectedValue.ToString());
            ProcessManager pm = new ProcessManager(User.Identity.Name);
            QuestionManager qm = new QuestionManager(User.Identity.Name);
            Process process = pm.GetProcesss(KeyID);
            // We need to update the list of Questions
            lblAnswerList.Text = "(" + process.Name + ")" + process.Description;
            lstQuestionSource.Items.Clear();
            lstQuestionTarget.Items.Clear();
            HiddenQuestionIDs.Value = "";
            List<PairIDValue> fullList = qm.GetAllQuestionsPairIDPair(); ;   // get the full source list
            List<PairIDValue> ProcessList = pm.GetProcessQuestionPairIDValue(KeyID);   // get the target list of processes
            List<PairIDValue> _NotInList2 = PairIDValue.GetUniqueList(fullList, ProcessList);   // Get a clean SourceList
            foreach (PairIDValue x in _NotInList2.OrderBy(y => y.Desc))
            {
                ListItem li = new ListItem(x.Desc, x.ID.ToString());
                lstQuestionSource.Items.Add(li);
            }
            foreach (PairIDValue x in ProcessList.OrderBy(y => y.Desc))
            {
                ListItem li = new ListItem(x.Desc, x.ID.ToString());
                lstQuestionTarget.Items.Add(li);
            }

        }
        protected void btnProcesss_Click(object sender, EventArgs e)
        {
            UpdateTagQuestionScreen();
            pnlMainView.Visible = false;
            pnlProcessAnswer.Visible = true;
        }

        protected void UpdateNextMoveProcessScreen()
        {
            decimal KeyID = decimal.Parse(MainGrid.SelectedValue.ToString());
            ProcessManager pm = new ProcessManager(User.Identity.Name);
            Process process = pm.GetProcesss(KeyID);
            lblNextMove.Text = "(" + process.Name + ")" + process.Description;
            lstNextMoveFrom.Items.Clear();
            lstNextMoveTo.Items.Clear();
            HiddenProcessNextStepIDs.Value = "";
            List<PairIDValue> fullList = pm.GetProcesssAllPairIDValue(); ;   // get the full source list
            List<PairIDValue> ProcessList = pm.GetProcesssNextStepPairIDValue(KeyID);   // get the target list of processes
            List<PairIDValue> _NotInList2 = PairIDValue.GetUniqueList(fullList, ProcessList);   // Get a clean SourceList
            foreach (PairIDValue x in _NotInList2)
            {
                ListItem li = new ListItem(x.Desc, x.ID.ToString());
                lstNextMoveFrom.Items.Add(li);
            }
            foreach (PairIDValue x in ProcessList)
            {
                ListItem li = new ListItem(x.Desc, x.ID.ToString());
                lstNextMoveTo.Items.Add(li);
            }
        }
        protected void btnNextMove_Click(object sender, EventArgs e)
        {
            // Load the Move Data
            UpdateNextMoveProcessScreen();
            pnlMainView.Visible = false;
            pnlProcessNextMove.Visible = true;
        }

        //protected void UpdateBinLocationProcessScreen()
        //{
        //    decimal KeyID = decimal.Parse(MainGrid.SelectedValue.ToString());
        //    ProcessManager pm = new ProcessManager(User.Identity.Name);
        //    Process process = pm.GetProcesss(KeyID);
        //    BinLocationManager bm = new BinLocationManager(User.Identity.Name);
        //    lblBinLocation.Text = "(" + process.Name + ")" + process.Description;
        //    lstBinMoveFrom.Items.Clear();
        //    lstBinMoveTo.Items.Clear();
        //    //HiddenProcessBinLocationIDs.Value = "";
        //    List<PairIDValue> fullList = bm.GetBinLocationsPairIDPair();   // get the full source list
        //    List<PairIDValue> ProcessList = pm.GetProcessBinLocationsPairIDValue(KeyID);   // get the target list of processes
        //    List<PairIDValue> _NotInList2 = PairIDValue.GetUniqueList(fullList, ProcessList);   // Get a clean SourceList
        //    foreach (PairIDValue x in _NotInList2)
        //    {
        //        ListItem li = new ListItem(x.Desc, x.ID.ToString());
        //        lstBinMoveFrom.Items.Add(li);
        //    }
        //    foreach (PairIDValue x in ProcessList)
        //    {
        //        //if (qu.Contains.Contains(
        //        ListItem li = new ListItem(x.Desc, x.ID.ToString());
        //        lstBinMoveTo.Items.Add(li);
        //    }
        //}


        //protected void btnBinLocation_Click(object sender, EventArgs e)
        //{
        //    // Load the Move Data
        //    UpdateBinLocationProcessScreen();
        //    pnlMainView.Visible = false;
        //    pnlProcessBinLocation.Visible = true;
        //}


        // Main Process Stuff
        protected void AddProcessOK_Click(object sender, EventArgs e)
        {
            int Sequence = 0;
            if (AddName.Text.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "RestHeader", "alert('Please enter a name');", true);
                return;
            }
            if (AddDescription.Text.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "RestHeader", "alert('Please enter a description');", true);
                return;
            }
            if (drpAddStatus.SelectedIndex < 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "RestHeader", "alert('Please enter a status');", true);
                return;
            }
            if (AddSequence.Text.Length == 0 || int.TryParse(AddSequence.Text, out Sequence) == false) { Sequence = 100; }

            clsLinqDataContext ctx = new clsLinqDataContext();
            Process qu = new Process();
            qu.Name = AddName.Text;
            qu.ButtonText = AddButton.Text;
            qu.RMASuffix = AddRMASuffix.Text;
            qu.Description = AddDescription.Text;
            qu.StatusID = decimal.Parse(drpAddStatus.SelectedItem.Value);
            qu.Sequence = Sequence;
            qu.Description_Client = AddDescription_Client.Text;
            qu.ScanKey = AddScanKey.Text;
            qu.MacroKey = AddMacroKey.Text;
            qu.CreateDate = DateTime.Now;
            qu.CreateUser = User.Identity.Name;
            qu.LastUpdateDate = DateTime.Now;
            qu.LastUpdateUser = User.Identity.Name;
            qu.ShowCompletedStatus = AddShowCompletedStatus.Checked;
            qu.isReadOnly = AddisReadOnly.Checked;
            qu.ShowTAT = AddShowTat.Checked;
            qu.CanJumpProject = AddCanJumpProject.Checked;
            qu.TurnStickyOn = AddTurnStickyOn.Checked;
            qu.AllowXBINX = AddAllowXBINX.Checked;
            qu.DisablePrint = chkAddDisablePrint.Checked;
            qu.ForcePrintOnSave = chkAddForcePrintOnSave.Checked;
            qu.IFSDirectiveType = AddIFSDirectiveType.Text;
            qu.BucketCount = ""; // AddBucketCount.Text;
            qu.BucketCountOffset = "";  // AddBucketCountOffset.Text;


            decimal dta = 0;
            if (decimal.TryParse(AddMinutesToYellow.Text, out dta) == true) { qu.MinutesToYellow = dta; }
            if (decimal.TryParse(AddMinutesToRed.Text, out dta) == true) { qu.MinutesToRed = dta; }





            //qu.ProcessStatus.Status = drpAddStatus.SelectedItem.Text;
            //qu.ProcessType.Type = drpAddType.SelectedItem.Text;
            ctx.Processes.InsertOnSubmit(qu);
            ctx.SubmitChanges();

            MasterProcessWaitTime wt = new MasterProcessWaitTime();
            if (decimal.TryParse(AddMinutesToYellow.Text, out dta) == true) { wt.MinutesToYellow = dta; }
            if (decimal.TryParse(AddMinutesToRed.Text, out dta) == true) { wt.MinutesToRed = dta; }
            wt.ClientID = -1;
            wt.CreateUser = User.Identity.Name;
            wt.LastUpdateDate = DateTime.Now;
            wt.LastUpdateUser = User.Identity.Name;
            wt.ProcessID = qu.ProcessID;
            wt.ProjectID = -1;

            ctx.MasterProcessWaitTimes.InsertOnSubmit(wt);
            ctx.SubmitChanges();




            UpdateMainGrid();
            pnlMainView.Visible = true;
            pnlAdd.Visible = false;
        }
        protected void AddProcessCancel_Click(object sender, EventArgs e)
        {
            pnlMainView.Visible = true;
            pnlAdd.Visible = false;
        }
        protected void EditProcessOK_Click(object sender, EventArgs e)
        {
            int Sequence = 0;
            if (EditName.Text.Length == 0) { return; }
            if (EditDescription.Text.Length == 0) { return; }
            if (drpEditStatus.SelectedIndex < 0) { return; }
            if (EditSequence.Text.Length == 0 || int.TryParse(EditSequence.Text, out Sequence) == false) { Sequence = 100; }
            decimal KeyID = decimal.Parse(EditKeyID.Text);
            clsLinqDataContext ctx = new clsLinqDataContext();
            Process qu = ctx.Processes.FirstOrDefault(x => x.ProcessID == KeyID);
            if (qu != null)
            {
                qu.Name = EditName.Text;
                qu.ButtonText = EditButton.Text;
                qu.RMASuffix = EditRMASuffix.Text;
                qu.Description = EditDescription.Text;
                qu.StatusID = decimal.Parse(drpEditStatus.SelectedItem.Value);
                qu.Sequence = Sequence;

                qu.Description_Client = EditDescription_Client.Text;
                qu.ScanKey = EditScanKey.Text;
                qu.MacroKey = EditMacroKey.Text;
                qu.ShowCompletedStatus = EditShowCompletedStatus.Checked;
                qu.isReadOnly = EditisReadOnly.Checked;
                qu.ShowTAT = EditShowTat.Checked;
                qu.CanJumpProject = EditCanJumpProject.Checked;
                qu.CreateUser = User.Identity.Name;
                qu.LastUpdateDate = DateTime.Now;
                qu.LastUpdateUser = User.Identity.Name;
                qu.BucketCount = ""; // EditBucketCount.Text;
                qu.BucketCountOffset = "";  // EditBucketCountOffset.Text;
                qu.TurnStickyOn = EditTurnStickyOn.Checked;
                qu.AllowXBINX = EditAllowXBINX.Checked;
                qu.ForcePrintOnSave = chkEditForcePrintOnSave.Checked;
                qu.DisablePrint = chkEditDisablePrint.Checked;
                qu.IFSDirectiveType = EditIFSDirectiveType.Text;
                decimal dta = 0;
                if (decimal.TryParse(EditMinutesToYellow.Text, out dta) == true) { qu.MinutesToYellow = dta; }
                if (decimal.TryParse(EditMinutesToRed.Text, out dta) == true) { qu.MinutesToRed = dta; }


                MasterProcessWaitTime wt = ctx.MasterProcessWaitTimes.FirstOrDefault(x => x.ProjectID == -1 && x.ProcessID == qu.ProcessID && x.ClientID == -1);

                if (wt == null)
                {
                    wt = new MasterProcessWaitTime();
                    if (decimal.TryParse(EditMinutesToYellow.Text, out dta) == true) { wt.MinutesToYellow = dta; }
                    if (decimal.TryParse(EditMinutesToRed.Text, out dta) == true) { wt.MinutesToRed = dta; }
                    wt.ClientID = -1;
                    wt.CreateUser = User.Identity.Name;
                    wt.LastUpdateDate = DateTime.Now;
                    wt.LastUpdateUser = User.Identity.Name;
                    wt.ProcessID = qu.ProcessID;
                    wt.ProjectID = -1;

                    ctx.MasterProcessWaitTimes.InsertOnSubmit(wt);
                }
                else
                {
                    if (decimal.TryParse(EditMinutesToYellow.Text, out dta) == true) { wt.MinutesToYellow = dta; }
                    if (decimal.TryParse(EditMinutesToRed.Text, out dta) == true) { wt.MinutesToRed = dta; }
                    wt.LastUpdateDate = DateTime.Now;
                    wt.LastUpdateUser = User.Identity.Name;
                }
                ctx.SubmitChanges();
            }
            UpdateMainGrid();
            pnlMainView.Visible = true;
            pnlEdit.Visible = false;
        }
        protected void EditProcessCancel_Click(object sender, EventArgs e)
        {
            pnlMainView.Visible = true;
            pnlEdit.Visible = false;
        }

        #endregion


        #region ProcessAnswerList
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            pnlMainView.Visible = true;
            pnlProcessAnswer.Visible = false;
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            pnlMainView.Visible = true;
            pnlProcessAnswer.Visible = false;
            try
            {
                decimal KeyID = decimal.Parse(MainGrid.SelectedValue.ToString());
                string AnswerIDs = HiddenQuestionIDs.Value;
                clsLinqDataContext ctx = new clsLinqDataContext();
                ctx.RecordProcessDefinitionQuestions(KeyID, AnswerIDs, User.Identity.Name);
            }
            catch (Exception ex)
            {

                Console.WriteLine("Generic Exception Handler: {0}", ex.ToString());
            }
        }
        #endregion

        #region ProcessNextMoveList
        protected void btnNextMoveOK_Click(object sender, EventArgs e)
        {
            pnlMainView.Visible = true;
            pnlProcessNextMove.Visible = false;
            try
            {
                decimal KeyID = decimal.Parse(MainGrid.SelectedValue.ToString());
                string NextMoveIDs = HiddenProcessNextStepIDs.Value;
                clsLinqDataContext ctx = new clsLinqDataContext();
                ctx.RecordProcessDefinitionNextSteps(KeyID, NextMoveIDs, User.Identity.Name);
            }
            catch (Exception ex)
            {

                Console.WriteLine("Generic Exception Handler: {0}", ex.ToString());
            }
        }
        protected void btnNextMoveCancel_Click(object sender, EventArgs e)
        {
            pnlMainView.Visible = true;
            pnlProcessNextMove.Visible = false;
        }
        #endregion

        //#region ProcessBinLocationList
        //protected void btnBinLocationOK_Click(object sender, EventArgs e)
        //{
        //    pnlMainView.Visible = true;
        //    pnlProcessBinLocation.Visible = false;
        //    try
        //    {
        //        decimal KeyID = decimal.Parse(MainGrid.SelectedValue.ToString());
        //        string BinLocationIDs = HiddenProcessBinLocationIDs.Value;
        //        clsLinqDataContext ctx = new clsLinqDataContext();
        //        ctx.RecordProcessDefinitionBinLocation(KeyID, BinLocationIDs, User.Identity.Name);
        //    }
        //    catch (Exception ex)
        //    {

        //        Console.WriteLine("Generic Exception Handler: {0}", ex.ToString());
        //    }
        //}
        //protected void btnBinLocationCancel_Click(object sender, EventArgs e)
        //{
        //    pnlMainView.Visible = true;
        //    pnlProcessBinLocation.Visible = false;
        //}
        //#endregion
    }
}