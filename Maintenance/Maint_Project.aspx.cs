using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
//using Factory_DataModel;
using BW_WebApp.DataManagers;

namespace BW_WebApp.Maintenance
{
    public partial class Maint_Project : System.Web.UI.Page
    {

        ProjectManager PM = null;
        clsLinqDataContext ctx = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            PM = new ProjectManager(User.Identity.Name);
            btnAddProcess.Click += new EventHandler(btnAddProcess_Click);
            MainGrid.SelectedIndexChanged += new EventHandler(MainGrid_SelectedIndexChanged);
            MainGrid.RowDataBound += new GridViewRowEventHandler(MainGrid_RowDataBound);
            btnSave.Click += new EventHandler(btnSave_Click);

            if (!IsPostBack)
            {
                //ProjectManager PM = new ProjectManager(User.Identity.Name);
                pnlAdd.Visible = false;
                pnlEdit.Visible = false;
                pnlProjectProcess.Visible = false;
                drpAddStatus.DataValueField = "ProjectStatusID";
                drpAddStatus.DataTextField = "Status";
                drpEditStatus.DataValueField = "ProjectStatusID";
                drpEditStatus.DataTextField = "Status";
                drpAddStatus.DataSource = PM.GetProjectStatusList();
                drpEditStatus.DataSource = PM.GetProjectStatusList();
                drpAddStatus.DataBind();
                drpEditStatus.DataBind();

                UpdateMainGrid();
                //this.DataBind();

            }

        }

        void btnAddProcess_Click(object sender, EventArgs e)
        {
            UpdateTagProcessScreen();
            pnlMainView.Visible = false;
            pnlProjectProcess.Visible = true;
        }

        protected void UpdateTagProcessScreen()
        {
            decimal KeyID = decimal.Parse(MainGrid.SelectedValue.ToString());
            ProcessManager pm = new ProcessManager(User.Identity.Name);
            //ProjectManager qm = new ProjectManager(User.Identity.Name);
            Project Project = PM.Get(KeyID);
            // We need to update the list of Questions
            lblProcessList.Text = "(" + Project.Name + ")" + Project.Description;
            lstProcessSource.Items.Clear();
            lstProcessTarget.Items.Clear();
            HiddenProcessIDs.Value = "";
            List<PairIDValue> fullList = pm.GetProcesssAllPairIDValue();  // get the full source list
            List<PairIDValue> ProcessList = PM.GetProjectProcessPairIDValue(KeyID);   // get the target list of processes
            List<PairIDValue> _NotInList2 = PairIDValue.GetUniqueList(fullList, ProcessList);   // Get a clean SourceList
            foreach (PairIDValue x in _NotInList2.OrderBy(x => x.Desc))
            {
                ListItem li = new ListItem(x.Desc, x.ID.ToString());
                lstProcessSource.Items.Add(li);
            }
            foreach (PairIDValue x in ProcessList.OrderBy(x => x.Desc))
            {
                ListItem li = new ListItem(x.Desc, x.ID.ToString());
                lstProcessTarget.Items.Add(li);
            }

        }
        void MainGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Type type = e.Row.DataItem.GetType();               //.GetGenericArguments()[0];
                LinkButton bPrint = (LinkButton)e.Row.FindControl("imgPrint");
                if (bPrint != null)
                {
                    bPrint.Attributes.Add("onclick", "PrintScanCodes('" + ((Project)e.Row.DataItem).ProjectID + "', '" + ((Project)e.Row.DataItem).Name + "'); return false;");
                }
            }
        }

        protected void UpdateMainGrid()
        {
            //ProjectManager pm = new ProjectManager(User.Identity.Name);
            //var QData = (from x in cm.Get().OrderBy(y => y.Name) select new { x.Name, x.ProjectID, x.Description, x.ProjectStatus.Status});
            //var QData = PM.Get();
            var QData = PM.GetRaw();
            MainGrid.DataSource = QData;
            MainGrid.DataBind();
        }

        protected void MainGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
            decimal KeyID = decimal.Parse(MainGrid.SelectedValue.ToString());
            btnEdit.Visible = false;
            btnDelete.Visible = false;
            if (MainGrid.SelectedIndex >= 0)
            {
                btnAddProcess.Visible = false;
                if (ctx == null) { ctx = PM.GetDataContext(User.Identity.Name); }
                if (PM.UserRestrict.AllowDelete("Project", KeyID, ctx) == true) { btnDelete.Visible = true; }
                if (PM.UserRestrict.AllowAdd("Project", KeyID, ctx) == true) { btnAddProcess.Visible = true; }
                if (PM.UserRestrict.AllowUpdate("Project", KeyID, ctx) == true) { btnEdit.Visible = true; }
                DisplaySelected();
            }
        }


        protected void DisplaySelected()
        {

            txtSelectChoice.Text = "";
            decimal KeyID = decimal.Parse(MainGrid.SelectedValue.ToString());
            //ProcessManager pm = new ProcessManager(User.Identity.Name);
            // QuestionManager qm = new QuestionManager(User.Identity.Name);
            Project cl = PM.Get(KeyID);
            if (cl != null)
            {
                EditProjectName.Text = cl.Name;
                txtSelectChoice.Text = string.Format("{0} - {1}", cl.Name, cl.Description);
            }
        }

        #region ClientArea


        protected void ReadyAdd()
        {
            AddProjectName.Text = "";
            AddBagTagName.Text = "";
            AddProjectDescription.Text = "";
            drpAddStatus.SelectedIndex = 0;
            AddGatherProjectTag.Checked = false;
            AddGatherRMA.Checked = false;
            AddGatherAutoProjectTag.Checked = false;
            AddAllowProjectPassThrough.Checked = false;
            AddGatherAutoRMA.Checked = false;
            AddAutoRMANumber.Checked = false;
            AddAutoWorkOrder.Checked = false;
            AddProductTag.Text = "";
            AddisMasterCarrierManufacturerLinked.Checked = false;
            AddisSecondaryProjectOverride.Checked = false;
        }
        protected void ReadyEdit()
        {
            decimal KeyID = decimal.Parse(MainGrid.SelectedValue.ToString());
            //ProjectManager pm = new ProjectManager(User.Identity.Name);
            Project cl = PM.Get(KeyID);
            if (cl != null)
            {
                EditProjectName.Text = cl.Name;
                EditProjectDescription.Text = cl.Description;
                EditBagTagName.Text = cl.BagTagName;
                EditProductTag.Text = cl.ProductTag;

                EditGatherProjectTag.Checked = true;
                if (cl.Gather_ProjectTag == null || cl.Gather_ProjectTag == false) { EditGatherProjectTag.Checked = false; }

                EditGatherAutoProjectTag.Checked = true;
                if (cl.GatherAuto_ProjectTag == null || cl.GatherAuto_ProjectTag == false) { EditGatherAutoProjectTag.Checked = false; }

                EditAllowProjectPassThrough.Checked = true;
                if (cl.AllowProjectPassThrough == null || cl.AllowProjectPassThrough == false) { EditAllowProjectPassThrough.Checked = false; }

                EditGatherRMA.Checked = true;
                if (cl.Gather_RMANumber == null || cl.Gather_RMANumber == false) { EditGatherRMA.Checked = false; }

                EditGatherAutoRMA.Checked = true;
                if (cl.GatherAuto_RMANumber == null || cl.GatherAuto_RMANumber == false) { EditGatherAutoRMA.Checked = false; }

                EditAutoRMANumber.Checked = true;
                if (cl.Auto_RMANumber == null || cl.Auto_RMANumber == false) { EditAutoRMANumber.Checked = false; }

                EditAutoWorkOrder.Checked = true;
                if (cl.Auto_WorkOrder == null || cl.Auto_WorkOrder == false) { EditAutoWorkOrder.Checked = false; }

                EditisMasterCarrierManufacturerLinked.Checked = true;
                if (cl.isMasterCarrierManufactuerLinked == null || cl.isMasterCarrierManufactuerLinked == false) { EditisMasterCarrierManufacturerLinked.Checked = false; }

                EditisSecondaryProjectOverride.Checked = true;
                if (cl.isSecondaryProjectOverride == null || cl.isSecondaryProjectOverride == false) { EditisSecondaryProjectOverride.Checked = false; }


                ListItem _ListItem = drpEditStatus.Items.FindByValue(cl.StatusID.ToString());
                if (_ListItem == null) { drpEditStatus.SelectedIndex = 0; }
                else { drpEditStatus.SelectedIndex = drpEditStatus.Items.IndexOf(_ListItem); }
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ////AddStatus.Text = "";
            ReadyAdd();
            pnlMainView.Visible = false;
            pnlAdd.Visible = true;
        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            ReadyEdit();
            pnlMainView.Visible = false;
            pnlEdit.Visible = true;
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            // Delete the answers.
            if (MainGrid.SelectedIndex >= 0)
            {
                decimal KeyID = decimal.Parse(MainGrid.SelectedValue.ToString());
                //ProjectManager pm = new ProjectManager(User.Identity.Name);
                PM.DeleteProject(KeyID);
                UpdateMainGrid();
            }
        }

        protected void AddOK_Click(object sender, EventArgs e)
        {
            //ProjectManager pm = new ProjectManager(User.Identity.Name);
            Project cl = PM.NewProject();
            //cl.Name = AddName.Text;
            //cl.ScanKey = AddSkanKey.Text;
            cl.Name = AddProjectName.Text;
            cl.BagTagName = AddBagTagName.Text;
            cl.Description = AddProjectDescription.Text;
            cl.StatusID = decimal.Parse(drpAddStatus.SelectedItem.Value);
            cl.Gather_RMANumber = AddGatherRMA.Checked;
            cl.Gather_ProjectTag = AddGatherProjectTag.Checked;
            cl.GatherAuto_ProjectTag = AddGatherAutoProjectTag.Checked;
            cl.AllowProjectPassThrough = AddAllowProjectPassThrough.Checked;
            cl.GatherAuto_RMANumber = AddGatherAutoRMA.Checked;
            cl.Auto_RMANumber = AddAutoRMANumber.Checked;
            cl.Auto_WorkOrder = AddAutoWorkOrder.Checked;
            cl.isMasterCarrierManufactuerLinked = AddisMasterCarrierManufacturerLinked.Checked;
            cl.isSecondaryProjectOverride = AddisSecondaryProjectOverride.Checked;
            cl.ProductTag = AddProductTag.Text;
            PM.InsertProject(cl);
            UpdateMainGrid();
            pnlMainView.Visible = true;
            pnlAdd.Visible = false;
        }
        protected void AddCancel_Click1(object sender, EventArgs e)
        {
            pnlMainView.Visible = true;
            pnlAdd.Visible = false;
        }
        protected void EditOK_Click(object sender, EventArgs e)
        {
            decimal KeyID = decimal.Parse(MainGrid.SelectedValue.ToString());
            //ProjectManager pm = new ProjectManager(User.Identity.Name);
            Project cl = PM.NewProject();
            cl.ProjectID = KeyID;
            //cl.Name = EditName.Text;
            //cl.ScanKey = EditSkanKey.Text;
            cl.Name = EditProjectName.Text;
            cl.Description = EditProjectDescription.Text;
            cl.BagTagName = EditBagTagName.Text;
            cl.StatusID = decimal.Parse(drpEditStatus.SelectedItem.Value);
            cl.Gather_RMANumber = EditGatherRMA.Checked;
            cl.Gather_ProjectTag = EditGatherProjectTag.Checked;
            cl.GatherAuto_ProjectTag = EditGatherAutoProjectTag.Checked;
            cl.AllowProjectPassThrough = EditAllowProjectPassThrough.Checked;
            cl.GatherAuto_RMANumber = EditGatherAutoRMA.Checked;
            cl.Auto_RMANumber = EditAutoRMANumber.Checked;
            cl.Auto_WorkOrder = EditAutoWorkOrder.Checked;
            cl.ProductTag = EditProductTag.Text;
            cl.isMasterCarrierManufactuerLinked = EditisMasterCarrierManufacturerLinked.Checked;
            cl.isSecondaryProjectOverride = EditisSecondaryProjectOverride.Checked;
            PM.UpdateProject(cl);
            UpdateMainGrid();
            pnlMainView.Visible = true;
            pnlEdit.Visible = false;
        }

        protected void EditCancel_Click(object sender, EventArgs e)
        {
            pnlMainView.Visible = true;
            pnlEdit.Visible = false;
        }
        #endregion

        protected void btnBillingPointCancel_Click(object sender, EventArgs e)
        {
            pnlMainView.Visible = true;
        }

        #region ProcessAnswerList
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            pnlMainView.Visible = true;
            pnlProjectProcess.Visible = false;

        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            pnlMainView.Visible = true;
            pnlProjectProcess.Visible = false;
            try
            {
                decimal KeyID = decimal.Parse(MainGrid.SelectedValue.ToString());
                string ProcessIDs = HiddenProcessIDs.Value;
                using (clsLinqDataContext ctx = new clsLinqDataContext())
                {
                    ctx.RecordProjectDefinitionProcess(KeyID, ProcessIDs, "", User.Identity.Name);
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("Generic Exception Handler: {0}", ex.ToString());
            }
        }
        #endregion

        //protected void imgPrintDef_Click(object sender, ImageClickEventArgs e)
        //{

        //    var cn = new SqlConnection(ConnectionString);
        //    var cmd = new SqlCommand();
        //    LinkButton xSender = (LinkButton)sender;
        //    cmd.CommandText = "GetMasterProjectDefinition " + xSender.CommandArgument;
        //    cmd.Connection = cn;
        //    ExportToExcel(cn, cmd, xSender.CommandName);
        //}

        /// <summary>
        /// Export data from datareader to excel file
        /// </summary>
        /// <param name="cn">sqlconnection</param>
        /// <param name="cmd">sqlcommand</param>
        private void ExportToExcel(SqlConnection cn, SqlCommand cmd, string fileName)
        {
            //Add Response header
            //const string fileName = "AddData";
            Response.Clear();
            Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.csv", fileName));
            Response.Charset = "";
            Response.ContentType = "application/vnd.xls";

            try
            {
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                var sb = new StringBuilder();
                //Add Header
                for (int count = 0; count < dr.FieldCount; count++)
                {
                    if (dr.GetName(count) != null)
                        sb.Append(dr.GetName(count));
                    if (count < dr.FieldCount - 1)
                    {
                        sb.Append(",");
                    }
                }
                Response.Write(sb.ToString() + "\n");
                Response.Flush();
                //Append Data
                while (dr.Read())
                {
                    sb = new StringBuilder();

                    for (int col = 0; col < dr.FieldCount - 1; col++)
                    {
                        if (!dr.IsDBNull(col))
                            sb.Append(dr.GetValue(col).ToString().Replace(",", " "));
                        sb.Append(",");
                    }
                    if (!dr.IsDBNull(dr.FieldCount - 1))
                        sb.Append(dr.GetValue(dr.FieldCount - 1).ToString().Replace(",", " "));
                    Response.Write(sb.ToString() + "\n");
                    Response.Flush();
                }
                // dr.Dispose();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                cmd.Connection.Close();
                cn.Close();
            }
            Response.End();
        }



    }
}
