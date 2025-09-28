using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
//using Factory_DataModel;
using BW_WebApp.DataManagers;
using Syncfusion.XlsIO;

namespace BW_WebApp.Maintenance
{
    public partial class Maint_MasterCarrer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //btnAddProcess.Click += new EventHandler(btnAddProcess_Click);
            MainGrid.SelectedIndexChanged += new EventHandler(MainGrid_SelectedIndexChanged);
            MainGrid.RowDataBound += new GridViewRowEventHandler(MainGrid_RowDataBound);
            btnUpload.Click += new EventHandler(btnUpload_Click);
            btnDownload.Click += new EventHandler(btnDownload_Click);
            btnRefresh.Click += new EventHandler(btnRefresh_Click);

            if (!IsPostBack)
            {

                ProjectManager cm = new ProjectManager(User.Identity.Name);
                pnlAdd.Visible = false;
                pnlEdit.Visible = false;
                //pnlUpload.Visible = false;
                //drpAddStatus.DataValueField = "ProjectStatusID";
                //drpAddStatus.DataTextField = "Status";
                //drpEditStatus.DataValueField = "ProjectStatusID";
                //drpEditStatus.DataTextField = "Status";
                //drpAddStatus.DataSource = cm.GetProjectStatusList();
                //drpEditStatus.DataSource = cm.GetProjectStatusList();

                MasterCarrierManufacturerModelColourManager ml = new MasterCarrierManufacturerModelColourManager(User.Identity.Name);
                drpAddCarrier.DataValueField = "ID";
                drpAddCarrier.DataTextField = "Desc";
                drpAddCarrier.DataSource = ml.GetMasterCarrierList();
                drpAddCarrier.DataBind();

                drpEditCarrier.DataValueField = "ID";
                drpEditCarrier.DataTextField = "Desc";
                drpEditCarrier.DataSource = ml.GetMasterCarrierList();
                drpEditCarrier.DataBind();

                drpAddColour.DataValueField = "ID";
                drpAddColour.DataTextField = "Desc";
                drpAddColour.DataSource = ml.GetMasterColourList();
                drpAddColour.DataBind();

                drpEditColour.DataValueField = "ID";
                drpEditColour.DataTextField = "Desc";
                drpEditColour.DataSource = ml.GetMasterColourList();
                drpEditColour.DataBind();

                drpAddManufacturer.DataValueField = "ID";
                drpAddManufacturer.DataTextField = "Desc";
                drpAddManufacturer.DataSource = ml.GetMasterManufacturerList();
                drpAddManufacturer.DataBind();

                drpEditManufacturer.DataValueField = "ID";
                drpEditManufacturer.DataTextField = "Desc";
                drpEditManufacturer.DataSource = ml.GetMasterManufacturerList();
                drpEditManufacturer.DataBind();

                drpAddModel.DataValueField = "ID";
                drpAddModel.DataTextField = "Desc";
                drpAddModel.DataSource = ml.GetMasterModelList().OrderBy(x => x.Desc);
                drpAddModel.DataBind();

                drpEditModel.DataValueField = "ID";
                drpEditModel.DataTextField = "Desc";
                drpEditModel.DataSource = ml.GetMasterModelList().OrderBy(x => x.Desc);
                drpEditModel.DataBind();

                //UpdateMainGrid();
                //this.DataBind();
                BindQuestionsToList();

                string Mode = Request.QueryString.Get("Mode");
                if (Mode == "Add")
                {
                    OpenAddWindow();
                    //ScriptManager.RegisterStartupScript(this, GetType(), "ADD MODE", "alert('Inside Add Mode');", true);
                }
            }
        }

        void btnRefresh_Click(object sender, EventArgs e)
        {
            UpdateMainGrid();
        }


        void BindQuestionsToList()
        {
            QuestionManager qm = new QuestionManager(User.Identity.Name);
            List<Option> ol = qm.GetQuestionOptionList("DeviceHandset");
            drpEditDeviceHandset.Items.Clear();
            drpAddDeviceHandset.Items.Clear();
            foreach (Option o in ol.OrderBy(x => x.Sequence))
            {
                ListItem x = new ListItem(o.OptionText, o.OptionID.ToString());
                drpEditDeviceHandset.Items.Add(x);

                ListItem y = new ListItem(o.OptionText, o.OptionID.ToString());
                drpAddDeviceHandset.Items.Add(x);
            }

            ol = qm.GetQuestionOptionList("Stock Colour Code");
            drpEditCondition.Items.Clear();
            drpAddCondition.Items.Clear();
            foreach (Option o in ol.OrderBy(x => x.Sequence))
            {
                ListItem x = new ListItem(o.OptionText, o.OptionID.ToString());
                drpEditCondition.Items.Add(x);

                ListItem y = new ListItem(o.OptionText, o.OptionID.ToString());
                drpAddCondition.Items.Add(x);
            }

            ol = qm.GetQuestionOptionList("Unit OS");
            AddUnitOS.Items.Clear();
            EditUnitOS.Items.Clear();
            foreach (Option o in ol.OrderBy(x => x.Sequence))
            {
                ListItem x = new ListItem(o.OptionText, o.OptionID.ToString());
                AddUnitOS.Items.Add(x);

                ListItem y = new ListItem(o.OptionText, o.OptionID.ToString());
                EditUnitOS.Items.Add(x);
            }
        }



        void btnDownload_Click(object sender, EventArgs e)
        {
            ExportMCMMCToExcel();
        }

        //void btnUpload_Click(object sender, EventArgs e)
        //{

        //    throw new NotImplementedException();

        //}




        protected void btnUpload_Click(object sender, EventArgs e)
        {
            UploadFile("~/IDAutomation", FileUploadXLS, lblMsgDetail);
        }

        private void UploadFile(string PathName, FileUpload UploadTool, Label Message)
        {
            if (UploadTool.HasFile)
            {

                string strFileName = UploadTool.FileName;
                int idx = strFileName.IndexOf(".");
                if (idx > -1)
                {
                    strFileName = strFileName.Substring(0, idx);
                }
                strFileName = UploadTool.FileName + "_" + DateTime.Now.ToString("ddMMyyyy_HHmmss");
                string strFileType = System.IO.Path.GetExtension(UploadTool.FileName).ToString().ToLower();
                //Check file type
                if (strFileType == ".xls" || strFileType == ".xlsx")
                {
                    UploadTool.SaveAs(Server.MapPath(PathName + "/" + strFileName + strFileType));
                    ImportMasterCarrierManufacturerModelColour(Server.MapPath(PathName + "/" + strFileName + strFileType));

                    Message.Text = "Data File Uploaded!";
                    Message.ForeColor = System.Drawing.Color.Green;
                    Message.Visible = true;

                    UpdateMainGrid();
                }
                else
                {
                    Message.Text = "Only excel files allowed";
                    Message.ForeColor = System.Drawing.Color.Red;
                    Message.Visible = true;
                }
            }
            else
            {
                Message.Text = "Please select an excel file first";
                Message.ForeColor = System.Drawing.Color.Red;
                Message.Visible = true;
            }
        }


        //void btnAddProcess_Click(object sender, EventArgs e)
        //{
        //    UpdateTagProcessScreen();
        //    pnlMainView.Visible = false;
        //    pnlProjectProcess.Visible = true;
        //}

        //protected void UpdateTagProcessScreen()
        //{
        //    decimal KeyID = decimal.Parse(MainGrid.SelectedValue.ToString());
        //    ProcessManager pm = new ProcessManager(User.Identity.Name);
        //    ProjectManager qm = new ProjectManager(User.Identity.Name);
        //    Project Project = qm.Get(KeyID);
        //    // We need to update the list of Questions
        //    lblProcessList.Text = "(" + Project.Name + ")" + Project.Description;
        //    lstProcessSource.Items.Clear();
        //    lstProcessTarget.Items.Clear();
        //    HiddenProcessIDs.Value = "";
        //    List<PairIDValue> fullList = pm.GetProcesssAllPairIDValue();  // get the full source list
        //    List<PairIDValue> ProcessList = qm.GetProjectProcessPairIDValue(KeyID);   // get the target list of processes
        //    List<PairIDValue> _NotInList2 = PairIDValue.GetUniqueList(fullList, ProcessList);   // Get a clean SourceList
        //    foreach (PairIDValue x in _NotInList2)
        //    {
        //        ListItem li = new ListItem(x.Desc, x.ID.ToString());
        //        lstProcessSource.Items.Add(li);
        //    }
        //    foreach (PairIDValue x in ProcessList)
        //    {
        //        ListItem li = new ListItem(x.Desc, x.ID.ToString());
        //        lstProcessTarget.Items.Add(li);
        //    }

        //}

        void MainGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    Type type = e.Row.DataItem.GetType();               //.GetGenericArguments()[0]; 
            //    ImageButton bPrint = (ImageButton)e.Row.FindControl("imgPrint");
            //    if (bPrint != null)
            //    {
            //        bPrint.Attributes.Add("onclick", "PrintScanCodes('" + ((Project)e.Row.DataItem).ProjectID + "', '" + ((Project)e.Row.DataItem).Name + "'); return false;");
            //    }
            //}
        }

        protected void UpdateMainGrid()
        {
            MasterCarrierManufacturerModelColourManager cm = new MasterCarrierManufacturerModelColourManager(User.Identity.Name);
            string ManABBR = "++";
            string ModABBR = "++";
            string CarABBR = "++";
            string ColABBR = "++";
            if (chkManufacturerABBR.Checked == true) { ManABBR = txtManufacturerABBR.Text; }
            if (chkModelABBR.Checked == true) { ModABBR = txtModelABBR.Text; }
            if (chkCarrierABBR.Checked == true) { CarABBR = txtCarrierABBR.Text; }
            if (chkColourABBR.Checked == true) { ColABBR = txtColourABBR.Text; }
            if (ManABBR.Trim().Length == 0) { ManABBR = "++"; }
            if (ModABBR.Trim().Length == 0) { ModABBR = "++"; }
            if (CarABBR.Trim().Length == 0) { CarABBR = "++"; }
            if (ColABBR.Trim().Length == 0) { ColABBR = "++"; }
            var QData = cm.GetMasterCarrierManufacturerModelList(ManABBR, ModABBR, CarABBR, ColABBR);               //.Take(1000);
            MainGrid.DataSource = QData;
            MainGrid.DataBind();
        }

        protected void MainGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
            decimal KeyID = decimal.Parse(MainGrid.SelectedValue.ToString());
            if (MainGrid.SelectedIndex >= 0)
            {
                btnEdit.Visible = true;
                btnDelete.Visible = true;
                //btnUpload.Visible = true;
                //btnDownload.Visible = true;
                //pnlUpload.Visible = true;
            }
            else
            {
                btnEdit.Visible = false;
                btnDelete.Visible = false;
                //btnUpload.Visible = false;
                //btnDownload.Visible = false;
                //pnlUpload.Visible = false;
            }
        }
        #region ClientArea


        protected void ReadyAdd()
        {
            //AddCondition.Text = "";
            AddSKU.Text = "";
            AddSKU_B.Text = "";
            AddSKU_C.Text = "";
            AddSKU_Loaner.Text = "";
            AddUPC.Text = "";
            AddUPC2.Text = "";
            AddUPC3.Text = "";
            AddDescription.Text = "";
            AddWarrantyStickerPlacement.Text = "";
            //AddDeviceHandset.Text = "";
            AddNickName.Text = "";
        }
        protected void ReadyEdit()
        {
            decimal KeyID = decimal.Parse(MainGrid.SelectedValue.ToString());
            MasterCarrierManufacturerModelColourManager m = new MasterCarrierManufacturerModelColourManager(User.Identity.Name);
            MasterCarrierManufacturerLookup ml = m.GetMasterCarrierManufacturerModel(KeyID);
            if (ml != null)
            {

                ListItem _ListItem = drpEditCarrier.Items.FindByValue(ml.OptionCarrierID.ToString());
                if (_ListItem == null) { drpEditCarrier.SelectedIndex = -1; }
                else { drpEditCarrier.SelectedIndex = drpEditCarrier.Items.IndexOf(_ListItem); }

                _ListItem = drpEditManufacturer.Items.FindByValue(ml.OptionManufacturerID.ToString());
                if (_ListItem == null) { drpEditManufacturer.SelectedIndex = -1; }
                else { drpEditManufacturer.SelectedIndex = drpEditManufacturer.Items.IndexOf(_ListItem); }

                _ListItem = drpEditModel.Items.FindByValue(ml.OptionModelID.ToString());
                if (_ListItem == null) { drpEditModel.SelectedIndex = -1; }
                else { drpEditModel.SelectedIndex = drpEditModel.Items.IndexOf(_ListItem); }

                _ListItem = drpEditColour.Items.FindByValue(ml.OptionColourID.ToString());
                if (_ListItem == null) { drpEditColour.SelectedIndex = -1; }
                else { drpEditColour.SelectedIndex = drpEditColour.Items.IndexOf(_ListItem); }

                // EditCondition.Text = ml.Condition;
                _ListItem = drpEditCondition.Items.FindByText(ml.Condition);
                if (_ListItem == null) { drpEditCondition.SelectedIndex = -1; }
                else { drpEditCondition.SelectedIndex = drpEditCondition.Items.IndexOf(_ListItem); }


                _ListItem = EditUnitOS.Items.FindByText(ml.Unit_OS);
                if (_ListItem == null) { EditUnitOS.SelectedIndex = -1; }
                else { EditUnitOS.SelectedIndex = EditUnitOS.Items.IndexOf(_ListItem); }


                EditSKU.Text = ml.SKU;
                EditSKU_B.Text = ml.SKU_B;
                EditSKU_C.Text = ml.SKU_C;
                EditUPC.Text = ml.UPC;
                EditUPC2.Text = ml.UPC_2;
                EditUPC3.Text = ml.UPC_3;
                EditSKU_Loaner.Text = ml.SKU_Loaner;
                EditDescription.Text = ml.Description;
                EditWarrantyStickerPlacement.Text = ml.WarrantyStickerPlacement;

                _ListItem = drpEditDeviceHandset.Items.FindByText(ml.Device_Handset);
                if (_ListItem == null) { drpEditDeviceHandset.SelectedIndex = -1; }
                else { drpEditDeviceHandset.SelectedIndex = drpEditDeviceHandset.Items.IndexOf(_ListItem); }

                EditNickName.Text = ml.NickName;
                rdlEditStyle.SelectedIndex = -1;
                if (ml.Bar_Flip == "Bar") { rdlEditStyle.SelectedIndex = 0; }
                if (ml.Bar_Flip == "Flip") { rdlEditStyle.SelectedIndex = 1; }
                rdlEditHSType.SelectedIndex = -1;
                if (ml.CDMA_HSPA == "CDMA") { rdlEditHSType.SelectedIndex = 0; }
                if (ml.CDMA_HSPA == "HSPA") { rdlEditHSType.SelectedIndex = 1; }
                //EditBarFlip.Text = ml.Bar_Flip;
                //EditCDMAHSPA.Text = ml.CDMA_HSPA;
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            OpenAddWindow();
        }

        private void OpenAddWindow()
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
                MasterCarrierManufacturerModelColourManager mlm = new MasterCarrierManufacturerModelColourManager(User.Identity.Name);
                mlm.DeleteMasterCarrierManufacturerLookup(KeyID);
                UpdateMainGrid();
            }
        }

        protected void AddOK_Click(object sender, EventArgs e)
        {
            MasterCarrierManufacturerModelColourManager mlm = new MasterCarrierManufacturerModelColourManager(User.Identity.Name);
            MasterCarrierManufacturerLookup ml = mlm.NewMasterCarrierManufacturerLookup();

            ml.Bar_Flip = "";
            if (rdlAddStyle.SelectedIndex == 0) { ml.Bar_Flip = "Bar"; }
            if (rdlAddStyle.SelectedIndex == 1) { ml.Bar_Flip = "Flip"; }

            ml.CDMA_HSPA = "";
            if (rdlAddHSType.SelectedIndex == 0) { ml.CDMA_HSPA = "CDMA"; }
            if (rdlAddHSType.SelectedIndex == 1) { ml.CDMA_HSPA = "HSPA"; }

            ml.Carrier = drpAddCarrier.SelectedItem.Text;
            ml.Colour = drpAddColour.SelectedItem.Text;

            ml.Condition = drpAddCondition.SelectedItem.Text;
            ml.Unit_OS = AddUnitOS.SelectedItem.Text;
            ml.Description = AddDescription.Text;
            ml.Device_Handset = drpAddDeviceHandset.SelectedItem.Text;
            ml.Manufacturer = drpAddManufacturer.SelectedItem.Text;
            ml.Model = drpAddModel.SelectedItem.Text;
            ml.OptionCarrierID = decimal.Parse(drpAddCarrier.SelectedItem.Value.ToString());
            ml.OptionColourID = decimal.Parse(drpAddColour.SelectedItem.Value.ToString());
            ml.OptionManufacturerID = decimal.Parse(drpAddManufacturer.SelectedItem.Value.ToString());
            ml.OptionModelID = decimal.Parse(drpAddModel.SelectedItem.Value.ToString());
            ml.Retire = "";
            ml.SKU = AddSKU.Text;
            ml.SKU_B = AddSKU_B.Text;
            ml.SKU_C = AddSKU_C.Text;
            ml.SKU_Loaner = AddSKU_Loaner.Text;
            ml.UPC = AddUPC.Text;
            ml.UPC_2 = AddUPC2.Text;
            ml.UPC_3 = AddUPC3.Text;
            ml.WarrantyStickerPlacement = AddWarrantyStickerPlacement.Text;
            ml.NickName = AddNickName.Text;
            mlm.InsertMasterCarrierManufacturerLookup(ml);
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

            MasterCarrierManufacturerModelColourManager mlm = new MasterCarrierManufacturerModelColourManager(User.Identity.Name);
            MasterCarrierManufacturerLookup ml = mlm.NewMasterCarrierManufacturerLookup();
            ml.MasterCarrierManufacturerLookupID = KeyID;

            ml.Bar_Flip = "";
            if (rdlEditStyle.SelectedIndex == 0) { ml.Bar_Flip = "Bar"; }
            if (rdlEditStyle.SelectedIndex == 1) { ml.Bar_Flip = "Flip"; }

            ml.CDMA_HSPA = "";
            if (rdlEditHSType.SelectedIndex == 0) { ml.CDMA_HSPA = "CDMA"; }
            if (rdlEditHSType.SelectedIndex == 1) { ml.CDMA_HSPA = "HSPA"; }

            ml.Carrier = drpEditCarrier.SelectedItem.Text;
            ml.Colour = drpEditColour.SelectedItem.Text;
            //ml.Condition = EditCondition.Text;
            ml.Condition = drpEditCondition.SelectedItem.Text;
            ml.Unit_OS = EditUnitOS.SelectedItem.Text;
            ml.Description = EditDescription.Text;
            ml.Device_Handset = drpEditDeviceHandset.SelectedItem.Text;
            ml.Manufacturer = drpEditManufacturer.SelectedItem.Text;
            ml.Model = drpEditModel.SelectedItem.Text;
            ml.OptionCarrierID = decimal.Parse(drpEditCarrier.SelectedItem.Value.ToString());
            ml.OptionColourID = decimal.Parse(drpEditColour.SelectedItem.Value.ToString());
            ml.OptionManufacturerID = decimal.Parse(drpEditManufacturer.SelectedItem.Value.ToString());
            ml.OptionModelID = decimal.Parse(drpEditModel.SelectedItem.Value.ToString());
            ml.Retire = "";
            ml.SKU = EditSKU.Text;
            ml.SKU_B = EditSKU_B.Text;
            ml.SKU_C = EditSKU_C.Text;
            ml.SKU_Loaner = EditSKU_Loaner.Text;
            ml.UPC = EditUPC.Text;
            ml.UPC_2 = EditUPC2.Text;
            ml.UPC_3 = EditUPC3.Text;
            ml.WarrantyStickerPlacement = EditWarrantyStickerPlacement.Text;
            ml.NickName = EditNickName.Text;
            mlm.UpdateMasterCarrierManufacturerLookup(ml);
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


        //#region ProcessAnswerList
        //protected void btnCancel_Click(object sender, EventArgs e)
        //{
        //    pnlMainView.Visible = true;
        //    pnlProjectProcess.Visible = false;
        //}
        //protected void btnSave_Click(object sender, EventArgs e)
        //{
        //    pnlMainView.Visible = true;
        //    pnlProjectProcess.Visible = false;
        //    try
        //    {
        //        decimal KeyID = decimal.Parse(MainGrid.SelectedValue.ToString());
        //        string ProcessIDs = HiddenProcessIDs.Value;
        //        clsLinqDataContext ctx = new clsLinqDataContext();
        //        ctx.RecordProjectDefinitionProcess(KeyID, ProcessIDs, "", User.Identity.Name);
        //    }
        //    catch (Exception ex)
        //    {

        //        Console.WriteLine("Generic Exception Handler: {0}", ex.ToString());
        //    }
        //}
        //#endregion

        //protected void imgPrintDef_Click(object sender, ImageClickEventArgs e)
        //{

        //    var cn = new SqlConnection(ConnectionString);
        //    var cmd = new SqlCommand();
        //    ImageButton xSender = (ImageButton)sender;
        //    cmd.CommandText = "GetMasterProjectDefinition " + xSender.CommandArgument;
        //    cmd.Connection = cn;
        //    ExportToExcel(cn, cmd, xSender.CommandName);
        //}

        ///// <summary>
        ///// Export data from datareader to excel file
        ///// </summary>
        ///// <param name="cn">sqlconnection</param>
        ///// <param name="cmd">sqlcommand</param>
        //private void ExportToExcel(SqlConnection cn, SqlCommand cmd, string fileName)
        //{
        //    //Add Response header 
        //    //const string fileName = "AddData";
        //    Response.Clear();
        //    Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.csv", fileName));
        //    Response.Charset = "";
        //    Response.ContentType = "application/vnd.xls";

        //    try
        //    {
        //        cn.Open();
        //        SqlDataReader dr = cmd.ExecuteReader();
        //        var sb = new StringBuilder();
        //        //Add Header          
        //        for (int count = 0; count < dr.FieldCount; count++)
        //        {
        //            if (dr.GetName(count) != null)
        //                sb.Append(dr.GetName(count));
        //            if (count < dr.FieldCount - 1)
        //            {
        //                sb.Append(",");
        //            }
        //        }
        //        Response.Write(sb.ToString() + "\n");
        //        Response.Flush();
        //        //Append Data
        //        while (dr.Read())
        //        {
        //            sb = new StringBuilder();

        //            for (int col = 0; col < dr.FieldCount - 1; col++)
        //            {
        //                if (!dr.IsDBNull(col))
        //                    sb.Append(dr.GetValue(col).ToString().Replace(",", " "));
        //                sb.Append(",");
        //            }
        //            if (!dr.IsDBNull(dr.FieldCount - 1))
        //                sb.Append(dr.GetValue(dr.FieldCount - 1).ToString().Replace(",", " "));
        //            Response.Write(sb.ToString() + "\n");
        //            Response.Flush();
        //        }
        //        // dr.Dispose();
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.Write(ex.Message);
        //    }
        //    finally
        //    {
        //        cmd.Connection.Close();
        //        cn.Close();
        //    }
        //    Response.End();
        //}

        #region  ExportTemplateFile

        private void ImportMasterCarrierManufacturerModelColour(string FileName)
        {
            MasterCarrierManufacturerModelColourManager mlm = new MasterCarrierManufacturerModelColourManager(User.Identity.Name);
            ExcelEngine excelEngine = new ExcelEngine();
            IApplication application = excelEngine.Excel;
            IWorkbook workbook = application.Workbooks.Open(FileName, ExcelOpenType.Automatic);
            IWorksheet sheet = workbook.Worksheets[0];
            int Row = 2;
            string DCommand = "";
            decimal ID = -1;

            decimal OptionCarrierID = -1;
            decimal OptionManufacturerID = -1;
            decimal OptionModelID = -1;
            decimal OptionColourID = -1;
            int Message = 32;

            sheet.Range[1, Message].Value = "Upload Status";
            while (sheet.Range[Row, 7].Value.Length > 0)          // Carrier Text Description
            {
                DCommand = sheet.Range[Row, 1].Value;
                if (decimal.TryParse(sheet.Range[Row, 2].Value, out ID) == false) { ID = -1; }
                if (DCommand != null && DCommand.Length > 0 && ID > 0)
                {
                    sheet.Range[Row, Message].Value = mlm.DeleteMasterCarrierManufacturerLookup(ID);
                }
                else
                {
                    if (decimal.TryParse(sheet.Range[Row, 3].Value, out OptionCarrierID) == false) { OptionCarrierID = -1; }
                    if (decimal.TryParse(sheet.Range[Row, 4].Value, out OptionManufacturerID) == false) { OptionManufacturerID = -1; }
                    if (decimal.TryParse(sheet.Range[Row, 5].Value, out OptionModelID) == false) { OptionModelID = -1; }
                    if (decimal.TryParse(sheet.Range[Row, 6].Value, out OptionColourID) == false) { OptionColourID = -1; }

                    MasterCarrierManufacturerLookup ml = mlm.NewMasterCarrierManufacturerLookup();

                    ml.Manufacturer = "";
                    ml.Model = "";
                    ml.Carrier = "";
                    ml.Colour = "";


                    ml.OptionCarrierID = OptionCarrierID;
                    ml.OptionManufacturerID = OptionManufacturerID;
                    ml.OptionModelID = OptionModelID;
                    ml.OptionColourID = OptionColourID;

                    ml.Carrier = sheet.Range[Row, 7].Value;
                    ml.Manufacturer = sheet.Range[Row, 8].Value;
                    ml.Model = sheet.Range[Row, 9].Value;
                    ml.Colour = sheet.Range[Row, 10].Value;
                    if (ml.Colour.Length == 0) { ml.Colour = "Black"; }

                    //ml.Carrier = (sheet.Range[Row, 7].Value == null ? "" : sheet.Range[Row, 7].Value);
                    //ml.Manufacturer = (sheet.Range[Row, 8].Value == null ? "" : sheet.Range[Row, 8].Text);
                    //if (sheet.Range[Row, 9].Value != null) { ml.Model = sheet.Range[Row, 9].Value; }
                    //else { ml.Model = sheet.Range[Row, 9].Value.ToString(); }
                    //ml.Colour = (sheet.Range[Row, 10].Value == null ? "" : sheet.Range[Row, 10].Value);

                    ml.Condition = (sheet.Range[Row, 15].Value == null ? "" : sheet.Range[Row, 15].Value);

                    ml.SKU = (sheet.Range[Row, 16].Value == null ? "" : sheet.Range[Row, 16].Value);
                    ml.SKU_B = (sheet.Range[Row, 17].Value == null ? "" : sheet.Range[Row, 17].Value);
                    ml.SKU_C = (sheet.Range[Row, 18].Value == null ? "" : sheet.Range[Row, 18].Value);
                    ml.SKU_Loaner = (sheet.Range[Row, 19].Value == null ? "" : sheet.Range[Row, 19].Value);

                    ml.UPC = (sheet.Range[Row, 20].Value == null ? "" : sheet.Range[Row, 20].Value);
                    ml.UPC_2 = (sheet.Range[Row, 21].Value == null ? "" : sheet.Range[Row, 21].Value);
                    ml.UPC_3 = (sheet.Range[Row, 22].Value == null ? "" : sheet.Range[Row, 22].Value);


                    ml.Description = (sheet.Range[Row, 23].Value == null ? "" : sheet.Range[Row, 23].Value);
                    ml.WarrantyStickerPlacement = (sheet.Range[Row, 24].Value == null ? "" : sheet.Range[Row, 24].Value);
                    ml.Device_Handset = (sheet.Range[Row, 25].Value == null ? "" : sheet.Range[Row, 25].Value);
                    ml.Bar_Flip = (sheet.Range[Row, 26].Value == null ? "" : sheet.Range[Row, 26].Value);
                    ml.CDMA_HSPA = (sheet.Range[Row, 27].Value == null ? "" : sheet.Range[Row, 27].Value);
                    ml.NickName = (sheet.Range[Row, 28].Value == null ? "" : sheet.Range[Row, 28].Value);
                    ml.Unit_OS = (sheet.Range[Row, 29].Value == null ? "" : sheet.Range[Row, 29].Value);
                    //ml.Retire = (sheet.Range[Row, 30].Value == null ? "" : sheet.Range[Row, 30].Value);
                    ml.Retire = "";
                    if (ID > 0)
                    {
                        ml.MasterCarrierManufacturerLookupID = ID;
                        sheet.Range[Row, Message].Value = mlm.UpdateMasterCarrierManufacturerLookup(ml);
                    }
                    else
                    {
                        sheet.Range[Row, Message].Value = mlm.InsertMasterCarrierManufacturerLookup(ml);
                    }
                }
                Row++;
            }


            workbook.SaveAs("MasterCMMC_Uploaded.xls", Page.Response, ExcelDownloadType.Open);
            workbook.Close();
            excelEngine.Dispose();
        }

        private string RangeValue(string text, string value)
        {
            string rValue = "";
            if (text != null) { rValue = text.ToString(); }
            else if (value != null)
            {
                rValue = value;
            }
            return rValue;
        }

        private void ExportMCMMCToExcel()
        {

            MasterCarrierManufacturerModelColourManager mlm = new MasterCarrierManufacturerModelColourManager(User.Identity.Name);
            List<vwMasterCarrierManufacturerLookupList_ABBR> ml = mlm.GetMasterCarrierManufacturerLookupList_ABBR();

            ExcelEngine excelEngine = new ExcelEngine();
            IApplication application = excelEngine.Excel;
            IWorkbook workbook = application.Workbooks.Create(1);
            IWorksheet sheet = workbook.Worksheets[0];
            int Row = 1;
            int Col = 1;
            int StartCol = Col;
            int StartRow = Row;

            //Add Header    
            sheet.Range[Row, 1].Value = "Delete";
            sheet.Range[Row, 2].Value = "MasterCarrierManufacturerLookupID";
            sheet.Range[Row, 3].Value = "OptionCarrierID";
            sheet.Range[Row, 4].Value = "OptionManufacturerID";
            sheet.Range[Row, 5].Value = "OptionModelID";
            sheet.Range[Row, 6].Value = "OptionColourID";
            sheet.Range[Row, 7].Value = "Carrier";
            sheet.Range[Row, 8].Value = "Manufacturer";
            sheet.Range[Row, 9].Value = "Model";
            sheet.Range[Row, 10].Value = "Colour";

            sheet.Range[Row, 11].Value = "CarrierText";
            sheet.Range[Row, 12].Value = "ManufacturerText";
            sheet.Range[Row, 13].Value = "ModelText";
            sheet.Range[Row, 14].Value = "ColourText";



            sheet.Range[Row, 15].Value = "Condition";
            sheet.Range[Row, 16].Value = "Clients NEW product SKU";
            sheet.Range[Row, 17].Value = "Second life A condition (b)";
            sheet.Range[Row, 18].Value = "Second Life, Minor scratches (c)";
            sheet.Range[Row, 19].Value = "A grouping SKU for types of loaner product";



            sheet.Range[Row, 20].Value = "UPC";
            sheet.Range[Row, 21].Value = "UPC 2";
            sheet.Range[Row, 22].Value = "UPC 3";



            sheet.Range[Row, 23].Value = "Description";
            sheet.Range[Row, 24].Value = "WarrantyStickerPlacement";
            sheet.Range[Row, 25].Value = "Device_Handset";
            sheet.Range[Row, 26].Value = "Style";
            sheet.Range[Row, 27].Value = "H/S Type";
            sheet.Range[Row, 28].Value = "Nickname";
            sheet.Range[Row, 29].Value = "Unit OS";
            sheet.Range[Row, 30].Value = "Retire";


            foreach (var x in ml)
            {
                Row++;
                sheet.Range[Row, 1].Value = "";
                sheet.Range[Row, 2].Value = x.MasterCarrierManufacturerLookupID.ToString();
                sheet.Range[Row, 3].Value = (x.OptionCarrierID == null ? "" : x.OptionCarrierID.ToString());
                sheet.Range[Row, 4].Value = (x.OptionManufacturerID == null ? "" : x.OptionManufacturerID.ToString());
                sheet.Range[Row, 5].Value = (x.OptionModelID == null ? "" : x.OptionModelID.ToString());
                sheet.Range[Row, 6].Value = (x.OptionColourID == null ? "" : x.OptionColourID.ToString());
                sheet.Range[Row, 7].Value = (x.Carrier == null ? "" : x.Carrier.ToString());
                sheet.Range[Row, 8].Value = (x.Manufacturer == null ? "" : x.Manufacturer.ToString());
                sheet.Range[Row, 9].Value = (x.Model == null ? "" : x.Model.ToString());
                sheet.Range[Row, 10].Value = (x.Colour == null ? "" : x.Colour.ToString());


                sheet.Range[Row, 11].Value = (x.CarrierText == null ? "" : x.CarrierText.ToString());
                sheet.Range[Row, 12].Value = (x.ManufacturerText == null ? "" : x.ManufacturerText.ToString());
                sheet.Range[Row, 13].Value = (x.ModelText == null ? "" : x.ModelText.ToString());
                sheet.Range[Row, 14].Value = (x.ColourText == null ? "" : x.ColourText.ToString());




                sheet.Range[Row, 15].Value = (x.Condition == null ? "" : x.Condition.ToString());
                sheet.Range[Row, 16].Value = (x.SKU == null ? "" : x.SKU.ToString());

                sheet.Range[Row, 17].Value = (x.SKU_B == null ? "" : x.SKU_B.ToString());
                sheet.Range[Row, 18].Value = (x.SKU_C == null ? "" : x.SKU_C.ToString());

                sheet.Range[Row, 19].Value = (x.SKU_Loaner == null ? "" : x.SKU_Loaner.ToString());

                sheet.Range[Row, 20].Value = (x.UPC == null ? "" : x.UPC.ToString());
                sheet.Range[Row, 21].Value = (x.UPC_2 == null ? "" : x.UPC_2.ToString());
                sheet.Range[Row, 22].Value = (x.UPC_3 == null ? "" : x.UPC_3.ToString());

                sheet.Range[Row, 23].Value = (x.Description == null ? "" : x.Description.ToString());
                sheet.Range[Row, 24].Value = (x.WarrantyStickerPlacement == null ? "" : x.WarrantyStickerPlacement.ToString());
                sheet.Range[Row, 25].Value = (x.Device_Handset == null ? "" : x.Device_Handset.ToString());
                sheet.Range[Row, 26].Value = (x.Bar_Flip == null ? "" : x.Bar_Flip.ToString());
                sheet.Range[Row, 27].Value = (x.CDMA_HSPA == null ? "" : x.CDMA_HSPA.ToString());
                sheet.Range[Row, 28].Value = (x.NickName == null ? "" : x.NickName.ToString());
                sheet.Range[Row, 29].Value = (x.Unit_OS == null ? "" : x.Unit_OS.ToString());
                sheet.Range[Row, 30].Value = (x.Retire == null ? "" : x.Retire.ToString());
            }
            workbook.SaveAs("MasterCMMC.xls", Page.Response, ExcelDownloadType.Open);
            workbook.Close();
            excelEngine.Dispose();
        }

        #endregion

    }
}