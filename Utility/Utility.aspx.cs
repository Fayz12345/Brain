using System;
using System.Collections.Generic;
//using Factory_Businesslayer;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
//using SoftArtisans.OfficeWriter.ExcelWriter;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using BW_WebApp.Classes;
using BW_WebApp.DataManagers;
using Syncfusion.XlsIO;
//using Syncfusion.XlsIO;

namespace BW_WebApp.Utility
{
    public partial class Utility : System.Web.UI.Page
    {

        clsLog log;
        //List<string> attributeclonelist = new List<string>();

        string _ConnectionString = string.Empty;
        public string ConnectionString
        {
            get
            {
                if (_ConnectionString.Length == 0)
                {
                    System.Configuration.ConnectionStringSettingsCollection xconnectionString = WebConfigurationManager.ConnectionStrings;
                    if (xconnectionString != null) { _ConnectionString = xconnectionString["DefaultConnectionString"].ConnectionString.ToString(); }

                }

                return _ConnectionString;
            }
            set { _ConnectionString = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            log = new clsLog(Server.MapPath("~"), "UtilityUpload_01_Log.txt", User.Identity.Name, System.Configuration.ConfigurationManager.AppSettings["WriteLogUserNameToLog"]);
            if (System.Configuration.ConfigurationManager.AppSettings["WriteLog"].ToUpper() == "TRUE")
            {
                log.writeLogData = true;
            }
            //HttpContext.Current.Server.ScriptTimeout = 90000;
            log.LogIt("**** Utility Screen Page load Started");
            lblMessage.Text = "Please select an excel file first";
            lblMessage.Visible = false;


            btnUploadDiscrepancy.Click += new EventHandler(btnUploadDiscrepancy_Click);
            btnVerifyDiscrepancy.Click += new EventHandler(btnVerifyDiscrepancy_Click);
            //btnResetDiscrepancy.Click += new EventHandler(btnResetDiscrepancy_Click);
            btnCleanBinGT0.Click += new EventHandler(btnCleanBinGT0_Click);

            btnIMEIUpload.Attributes.Add("OnClick", "this.value='Processing...'; this.disabled='true';" + ClientScript.GetPostBackEventReference(btnIMEIUpload, null) + ";");
            //btnIMEIUploadSeedData.Attributes.Add("OnClick", "this.value='Processing...'; this.disabled='true';" + ClientScript.GetPostBackEventReference(btnIMEIUploadSeedData, null) + ";");
            //btnIMEIUpload.Attributes.Add("OnLoad", "this.disabled='false'; this.value='Upload';alert('here');");
            //-------------------------------------------------
            // Leaving these turned off.
            //-------------------------------------------------

            tabIMEI.Visible = true;

            TabOrderEntryXLSUpload.Visible = false;
            TabSalesOrderIMEIQuerry.Visible = false;
            TabPurchaseOrderRefinement.Visible = false;
            TabOther1.Visible = false;
            TabMoveLocation.Visible = false;
            TabPrereceive.Visible = false;
            TabPanel3z.Visible = false;
            TabPanel4.Visible = false;
            TabPanel1.Visible = false;
            TabSetVersion000.Visible = false;

            TabUploadAttributes.Visible = false;
            TabUploadSingleAttributes.Visible = false;
            TabUploaddAdjustOut.Visible = false;
            TabUploadModelMemory.Visible = false;
            TabUploadAttributeReplace.Visible = false;
            tabAttribute.Visible = false;
            btnShowTimeLog_P.Visible = false;
            btnClearLog_P.Visible = false;
            TabFullAttributeUpload.Visible = false;

            if (User.IsInRole("DatabaseMaster") == true)
            {
                TabUploadAttributes.Visible = true;
                TabUploadSingleAttributes.Visible = true;
                TabPrereceive.Visible = true;
                //TabUploaddAdjustOut.Visible = true;
                //TabUploadModelMemory.Visible = true;
                //TabUploadAttributeReplace.Visible = true;
                //tabAttribute.Visible = true;
                TabFullAttributeUpload.Visible = true;
            }
            if (User.IsInRole("SetDevAttr") == true)
            {
                TabUploadAttributes.Visible = true;
                TabUploadSingleAttributes.Visible = true;
            }
            if (User.IsInRole("Prereceive") == true)
            {
                TabPrereceive.Visible = true;
            }
            if (User.IsInRole("SetQuAttr") == true)
            {
                TabUploadAttributes.Visible = true;
                TabUploadSingleAttributes.Visible = true;
                //tabAttribute.Visible = true;
                //TabUploaddAdjustOut.Visible = true;
                //TabUploadModelMemory.Visible = true;
                //TabUploadAttributeReplace.Visible = true;
            }
            if (User.IsInRole("AddQuAttr") == true)
            {
                TabFullAttributeUpload.Visible = true;
            }
            if (User.IsInRole("AddQuAttrP") == true)
            {
                tabAttribute.Visible = true;
            }
            if (!IsPostBack)
            {

                //if (User.IsInRole("Administrators") == true || User.IsInRole("Supervisors") == true || User.IsInRole("AddRollBack") == true)
                //{
                //    TabSetVersion000.Visible = true;
                //}

                tbBatchNumber.Text = User.Identity.Name;

                ProjectManager pm = new ProjectManager(User.Identity.Name);
                drpProjectList_New.DataValueField = "ProjectID";
                drpProjectList_New.DataTextField = "Name";
                drpProjectList_New.DataSource = pm.GetProjectList();          // pm.GetMasterProjectList();
                drpProjectList_New.DataBind();
                drpProjectList_New.SelectedIndex = 0;

                //ClientManager cm = new ClientManager(User.Identity.Name);
                //drpClientList_New.DataValueField = "ClientLocationID";
                //drpClientList_New.DataTextField = "CompanyName";
                //drpClientList_New.DataSource = cm.DropDownSearchLocationsList("", "", "", "").OrderBy(x => x.CompanyName);
                //drpClientList_New.DataBind();
                //drpClientList_New.SelectedIndex = 0;

                //drpClientList_PRI.DataValueField = "ClientLocationID";
                //drpClientList_PRI.DataTextField = "CompanyName";
                //drpClientList_PRI.DataSource = cm.DropDownSearchLocationsList("", "", "", "").OrderBy(x => x.CompanyName);
                //drpClientList_PRI.DataBind();
                //drpClientList_PRI.SelectedIndex = 0;
                // 
            }
        }

        protected void btnFullAttributeUpload_Click(object sender, EventArgs e)
        {

            //This Utility will load Attribute values to existing IMEI version zero records
            if ((FullAttributeUpload.HasFile))
            {
                string strFileName = DateTime.Now.ToString("ddMMyyyy_HHmmss");
                string strFileType = System.IO.Path.GetExtension(FullAttributeUpload.FileName).ToString().ToLower();
                //Check file type
                if (strFileType == ".xls" || strFileType == ".xlsx")
                {
                    FullAttributeUpload.SaveAs(Server.MapPath("~/UploadedExcel/" + strFileName + strFileType));
                }
                else
                {
                    MSGFullAttributeUpload.Text = "Only excel files allowed";
                    MSGFullAttributeUpload.ForeColor = System.Drawing.Color.Red;
                    MSGFullAttributeUpload.Visible = true;
                    return;
                }
                string strNewPath = Server.MapPath("~/UploadedExcel/" + strFileName + strFileType);

                //Step 1 : Instantiate the spreadsheet creation engine.
                ExcelEngine excelEngine = new ExcelEngine();

                //Step 2 : Instantiate the excel application object.
                IApplication application = excelEngine.Excel;

                //Open an existing spreadsheet which will be used as a template for generating the new spreadsheet.
                //After opening, the workbook object represents the complete in-memory object model of the template spreadsheet.
                IWorkbook workbook = application.Workbooks.Open(strNewPath, ExcelOpenType.Automatic);
                //The first worksheet object in the worksheets collection is accessed.
                IWorksheet sheet = workbook.Worksheets[0];

                List<string[]> al = new List<string[]>();


                for (int row = 2; row < 1000000; row++)
                {
                    string[] data = { "", "", "", "", "", "", "" };
                    if (sheet.Range[row, 1].Value == null || sheet.Range[row, 1].Value.Length == 0) { break; }

                    data[0] = sheet.Range[row, 1].Value;      // Question Name
                    data[1] = sheet.Range[row, 2].Value;      // Delete
                    data[2] = sheet.Range[row, 3].Value;      // Scankey
                    data[3] = sheet.Range[row, 4].Value;      // Answer
                    data[4] = sheet.Range[row, 5].Value;      // Name
                    data[5] = sheet.Range[row, 6].Value;      // Seq
                    data[6] = row.ToString();                 // XLS Row data pulled from.
                    if (data[0].Trim().Length > 0
                     //&& data[1].Trim().Length > 0 
                     //&& data[2].Trim().Length > 0 
                     && data[3].Trim().Length > 0
                     && data[4].Trim().Length > 0
                     && data[5].Trim().Length > 0)
                    {
                        al.Add(data);
                    }
                }
                workbook.Close();
                // Load the attributes

                int count = 0;
                using (clsLinqDataContext ctx = new clsLinqDataContext())
                {
                    int del = 0;
                    int Seq = 0;
                    foreach (string[] optionvalue in al)
                    {
                        count++;
                        if (int.TryParse(optionvalue[5], out Seq) == false) { optionvalue[5] = "100"; }
                        //ctx.Utility_LoadAttributeValue(txtQuestionName.Text.Trim(), optionvalue[0]);
                        string rValue = "";
                        if (optionvalue[1].Length > 0 && optionvalue[1].ToUpper() != "FALSE" && optionvalue[1].ToUpper() != "F" && optionvalue[1].ToUpper() != "NO" && optionvalue[1].ToUpper() != "N") { del = 1; }
                        ctx.Utility_LoadAttributeValue_WithDelete(optionvalue[0], del, optionvalue[2], optionvalue[4], optionvalue[3], optionvalue[5], User.Identity.Name, ref rValue);
                    }
                }
                MSGFullAttributeUpload.Text = count.ToString() + " Attributes Loaded.";
                MSGFullAttributeUpload.ForeColor = System.Drawing.Color.Green;
                MSGFullAttributeUpload.Visible = true;

                //excelEngine.Dispose();
                //grvExcelData.DataSource = al;
                //grvExcelData.DataBind();
            }
            else
            {
                MSGFullAttributeUpload.Text = "Please select an excel file first or Enter a Question Name.";
                MSGFullAttributeUpload.ForeColor = System.Drawing.Color.Red;
                MSGFullAttributeUpload.Visible = true;
            }
        }


        protected void btnAttributeReplaceUpload_Click(object sender, EventArgs e)
        {
            log.LogIt("**** btnAttributeReplaceUpload_Click -- Started");


            if (AttributeReplaceFileUpload.HasFile == false)
            {
                lblAttributeReplaceUploadMSG.Text = "Please select an excel file first";
                lblAttributeReplaceUploadMSG.ForeColor = System.Drawing.Color.Red;
                lblAttributeReplaceUploadMSG.Visible = true;
                btnAttributeReplaceUpload.Enabled = true;
                return;
            }
            #region Verify proper file
            string strFileName = DateTime.Now.ToString("ddMMyyyy_HHmmss");
            string strFileType = System.IO.Path.GetExtension(ModelMemoryFileUpload.FileName).ToString().ToLower();
            //Check file type
            if (strFileType == ".xls" || strFileType == ".xlsx")
            {
                AttributeReplaceFileUpload.SaveAs(Server.MapPath("~/UploadedExcel/" + strFileName + strFileType));
            }
            else
            {
                lblAttributeReplaceUploadMSG.Text = "Only excel files allowed";
                lblAttributeReplaceUploadMSG.ForeColor = System.Drawing.Color.Red;
                lblAttributeReplaceUploadMSG.Visible = true;
                btnAttributeReplaceUpload.Enabled = true;
                return;
            }
            string strNewPath = Server.MapPath("~/UploadedExcel/" + strFileName + strFileType);
            #endregion

            ReplaceAttributeUploadProcessor processor = new ReplaceAttributeUploadProcessor(strNewPath, User.Identity.Name);

            //IMEIUploadProcessor processor = new IMEIUploadProcessor(strNewPath, User.Identity.Name, ProjID, CL_Skcankey.Text, attributeclonelist, log, chkForce15IMEI.Checked, chkRunThreaded_Newa.Checked, ifsType);

            lblAttributeReplaceUploadMSG.Text = AttributeReplaceFileUpload.FileName + "   " + processor.LoadData();
            lblAttributeReplaceUploadMSG.Visible = true;
            btnAttributeReplaceUpload.Text = "Upload";
            //Page.Response.Flush();
            processor.workbook.SaveAs(strFileName + strFileType, Page.Response, ExcelDownloadType.Open);
            processor.workbook.Close();
            processor.excelEngine.Dispose();
            lblAttributeReplaceUploadMSG.Visible = true;
            btnAttributeReplaceUpload.Enabled = true;
        }

        protected void btnModelMemoryUpload_Click(object sender, EventArgs e)
        {
            log.LogIt("**** btnModelMemoryUpload_Click -- Started");


            if (ModelMemoryFileUpload.HasFile == false)
            {
                lblModelMemoryUploadMSG.Text = "Please select an excel file first";
                lblModelMemoryUploadMSG.ForeColor = System.Drawing.Color.Red;
                lblModelMemoryUploadMSG.Visible = true;
                btnModelMemoryUpload.Enabled = true;
                return;
            }
            #region Verify proper file
            string strFileName = DateTime.Now.ToString("ddMMyyyy_HHmmss");
            string strFileType = System.IO.Path.GetExtension(ModelMemoryFileUpload.FileName).ToString().ToLower();
            //Check file type
            if (strFileType == ".xls" || strFileType == ".xlsx")
            {
                ModelMemoryFileUpload.SaveAs(Server.MapPath("~/UploadedExcel/" + strFileName + strFileType));
            }
            else
            {
                lblModelMemoryUploadMSG.Text = "Only excel files allowed";
                lblModelMemoryUploadMSG.ForeColor = System.Drawing.Color.Red;
                lblModelMemoryUploadMSG.Visible = true;
                btnModelMemoryUpload.Enabled = true;
                return;
            }
            string strNewPath = Server.MapPath("~/UploadedExcel/" + strFileName + strFileType);
            #endregion

            ModelMemoryProcessor processor = new ModelMemoryProcessor(strNewPath, User.Identity.Name);

            //IMEIUploadProcessor processor = new IMEIUploadProcessor(strNewPath, User.Identity.Name, ProjID, CL_Skcankey.Text, attributeclonelist, log, chkForce15IMEI.Checked, chkRunThreaded_Newa.Checked, ifsType);

            lblModelMemoryUploadMSG.Text = ModelMemoryFileUpload.FileName + "   " + processor.LoadModelData();
            lblModelMemoryUploadMSG.Visible = true;
            btnModelMemoryUpload.Text = "Upload";
            //Page.Response.Flush();
            processor.workbook.SaveAs(strFileName + strFileType, Page.Response, ExcelDownloadType.Open);
            processor.workbook.Close();
            processor.excelEngine.Dispose();
            lblModelMemoryUploadMSG.Visible = true;
            btnModelMemoryUpload.Enabled = true;
        }





        void btnCleanBinGT0_Click(object sender, EventArgs e)
        {
            ReceiveDetailManager rm = new ReceiveDetailManager(User.Identity.Name);
            lblCleanBinMessage.Text = rm.UtilityCleanNonZeroBins();
        }

        //void btnResetDiscrepancy_Click(object sender, EventArgs e)
        //{
        //    DiscrepincyManager dm = new DiscrepincyManager(User.Identity.Name);
        //    lblDiscrepancy.Text = dm.KillAllRecords();
        //    lblDiscrepancy.ForeColor = System.Drawing.Color.Red;
        //    lblDiscrepancy.Visible = true;
        //}


        void btnVerifyDiscrepancy_Click(object sender, EventArgs e)
        {
            decimal ProjID = -1;
            //decimal ClientLocationID = -1;
            if (decimal.TryParse(drpProjectList_New.SelectedValue, out ProjID) == false) { ProjID = -1; }
            //if (decimal.TryParse(drpClientList_New.SelectedValue, out ClientLocationID) == false) { ClientLocationID = -1; }
            if ((DiscrepancyFileUpload.HasFile))
            {
                string strFileName = DateTime.Now.ToString("ddMMyyyy_HHmmss");
                string strFileType = System.IO.Path.GetExtension(DiscrepancyFileUpload.FileName).ToString().ToLower();
                //Check file type
                if (strFileType == ".xls" || strFileType == ".xlsx")
                {
                    DiscrepancyFileUpload.SaveAs(Server.MapPath("~/UploadedExcel/" + strFileName + strFileType));
                }
                else
                {
                    lblDiscrepancy.Text = "Only excel files allowed";
                    lblDiscrepancy.ForeColor = System.Drawing.Color.Red;
                    lblDiscrepancy.Visible = true;
                    return;
                }
                string strNewPath = Server.MapPath("~/UploadedExcel/" + strFileName + strFileType);
                lblDiscrepancy.Text = "";
                UploadProcessor processor = new UploadProcessor(strNewPath, User.Identity.Name, log);
                lblDiscrepancy.Text = processor.LoadDiscrepancyData(true);
                lblDiscrepancy.ForeColor = System.Drawing.Color.Red;
                lblDiscrepancy.Visible = true;

                string FullFileName = strFileName + strFileType;
                processor.workbook.SaveAs("XXXX.xls", Page.Response, ExcelDownloadType.Open);
                processor.workbook.Close();
                processor.excelEngine.Dispose();

            }

            else
            {
                lblDiscrepancy.Text = "Please select an excel file first";
                lblDiscrepancy.ForeColor = System.Drawing.Color.Red;
                lblDiscrepancy.Visible = true;
            }
        }

        void btnUploadDiscrepancy_Click(object sender, EventArgs e)
        {
            {
                decimal ProjID = -1;
                //decimal ClientLocationID = -1;
                if (decimal.TryParse(drpProjectList_New.SelectedValue, out ProjID) == false) { ProjID = -1; }
                //if (decimal.TryParse(drpClientList_New.SelectedValue, out ClientLocationID) == false) { ClientLocationID = -1; }
                if ((DiscrepancyFileUpload.HasFile))
                {
                    string strFileName = DateTime.Now.ToString("ddMMyyyy_HHmmss");
                    string strFileType = System.IO.Path.GetExtension(DiscrepancyFileUpload.FileName).ToString().ToLower();
                    //Check file type
                    if (strFileType == ".xls" || strFileType == ".xlsx")
                    {
                        DiscrepancyFileUpload.SaveAs(Server.MapPath("~/UploadedExcel/" + strFileName + strFileType));
                    }
                    else
                    {
                        lblDiscrepancy.Text = "Only excel files allowed";
                        lblDiscrepancy.ForeColor = System.Drawing.Color.Red;
                        lblDiscrepancy.Visible = true;
                        return;
                    }
                    string strNewPath = Server.MapPath("~/UploadedExcel/" + strFileName + strFileType);
                    lblDiscrepancy.Text = "";
                    UploadProcessor processor = new UploadProcessor(strNewPath, User.Identity.Name, log);
                    lblDiscrepancy.Text = processor.LoadDiscrepancyData(false);
                    lblDiscrepancy.ForeColor = System.Drawing.Color.Red;
                    lblDiscrepancy.Visible = true;

                    string FullFileName = strFileName + strFileType;
                    processor.workbook.SaveAs("XXXX.xls", Page.Response, ExcelDownloadType.Open);
                    processor.workbook.Close();
                    processor.excelEngine.Dispose();

                }

                else
                {
                    lblDiscrepancy.Text = "Please select an excel file first";
                    lblDiscrepancy.ForeColor = System.Drawing.Color.Red;
                    lblDiscrepancy.Visible = true;
                }
            }
        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            if (log != null)
            {
                log.LogIt("**** Utility Screen Page Unload");
            }
        }

        protected void btnIMEIRollUpload_Click(object sender, EventArgs e)
        {
            decimal ProjID = -1;
            //decimal ClientLocationID = -1;
            if (decimal.TryParse(drpProjectList_New.SelectedValue, out ProjID) == false) { ProjID = -1; }
            //if (decimal.TryParse(drpClientList_New.SelectedValue, out ClientLocationID) == false) { ClientLocationID = -1; }
            if ((IMEIRollUploadFile.HasFile))
            {
                string strFileName = DateTime.Now.ToString("ddMMyyyy_HHmmss");
                string strFileType = System.IO.Path.GetExtension(IMEIRollUploadFile.FileName).ToString().ToLower();
                //Check file type
                if (strFileType == ".xls" || strFileType == ".xlsx")
                {
                    IMEIRollUploadFile.SaveAs(Server.MapPath("~/UploadedExcel/" + strFileName + strFileType));
                }
                else
                {
                    lblRollMessage.Text = "Only excel files allowed";
                    lblRollMessage.ForeColor = System.Drawing.Color.Red;
                    lblRollMessage.Visible = true;
                    return;
                }
                string strNewPath = Server.MapPath("~/UploadedExcel/" + strFileName + strFileType);
                lblRollMessage.Text = "";
                //List<string> attributeclonelist = new List<string>();
                ////attributeclonelist.Add("Carrier");
                ////attributeclonelist.Add("Manufacturer");
                ////attributeclonelist.Add("Model");
                ////attributeclonelist.Add("Colour");
                ////attributeclonelist.Add("Disposition");          // may need to remove this one.
                ////attributeclonelist.Add("QC");                   // may need to change this to another name when Sandbox becomes live.
                ////attributeclonelist.Add("Grade");  


                IMEIRollIMEIUploadProcessor processor = new IMEIRollIMEIUploadProcessor(strNewPath, User.Identity.Name, chnForce15IMEIRoll.Checked);
                lblRollMessage.Text = processor.LoadIMEIData();
                lblIMEIUploadMSG.ForeColor = System.Drawing.Color.Red;
                lblRollMessage.Visible = true;
            }

            else
            {
                lblRollMessage.Text = "Please select an excel file first";
                lblRollMessage.ForeColor = System.Drawing.Color.Red;
                lblRollMessage.Visible = true;
            }
        }


        protected void btnIMEIPOSwitch_Click(object sender, EventArgs e)
        {

            if ((IMEIPOSwitchUploadFile.HasFile))
            {
                string strFileName = txtOrderEntryNumber.Text.Trim() + "_Upload_" + DateTime.Now.ToString("ddMMyyyy_HHmmss");
                string strFileType = System.IO.Path.GetExtension(IMEIPOSwitchUploadFile.FileName).ToString().ToLower();
                //Check file type
                if (strFileType == ".xls" || strFileType == ".xlsx")
                {
                    IMEIPOSwitchUploadFile.SaveAs(Server.MapPath("~/UploadedExcel/" + strFileName + strFileType));
                }
                else
                {
                    ShowUploadErrorMessage("Only excel files allowed");
                    return;
                }
                string strNewPath = Server.MapPath("~/UploadedExcel/" + strFileName + strFileType);
                List<string> attributeclonelist = new List<string>();
                //attributeclonelist.Add("Carrier");
                //attributeclonelist.Add("Manufacturer");
                //attributeclonelist.Add("Model");
                //attributeclonelist.Add("Colour");
                //attributeclonelist.Add("Disposition");          // may need to remove this one.
                //attributeclonelist.Add("QC");                   // may need to change this to another name when Sandbox becomes live.
                //attributeclonelist.Add("Grade");  
                IMEIPOSwitchUploadProcessor processor = new IMEIPOSwitchUploadProcessor(strNewPath, User.Identity.Name);

                lblPOSwitchMessage.Text = processor.LoadIMEIPOSwitchData();
                processor.workbook.SaveAs(strFileName + strFileType, Page.Response, ExcelDownloadType.Open);
                processor.workbook.Close();
                processor.excelEngine.Dispose();
                lblPOSwitchMessage.Visible = true;
            }
            else
            {
                ShowUploadErrorMessage("Please select an excel file first");
            }
        }

        protected void btnIMEIOrderUpload_Click(object sender, EventArgs e)
        {
            if (txtOrderEntryNumber.Text.Length == 0)
            {
                ShowUploadErrorMessage("You must supply an IFS Order Number first.");
                return;
            }
            if (txtIFSLocation.Text.Length == 0)
            {
                ShowUploadErrorMessage("You must supply an IFS Location first.");
                return;
            }
            IFSLocation Location = new IFSLocation(txtIFSLocation.Text);
            if (Location.isValid == false)
            {
                ShowUploadErrorMessage("IFS Location is not valid.");
                return;
            }
            if (Location.IsThisFrozen(User.Identity.Name) == true)
            {
                ShowUploadErrorMessage("IFS Location is Frozen.");
                return;
            }


            decimal ProjID = -1;
            //decimal ClientLocationID = -1;
            if (decimal.TryParse(drpProjectList_New.SelectedValue, out ProjID) == false) { ProjID = -1; }
            //if (decimal.TryParse(drpClientList_New.SelectedValue, out ClientLocationID) == false) { ClientLocationID = -1; }
            if ((IMEIOrderUploadFile.HasFile))
            {
                string strFileName = txtOrderEntryNumber.Text.Trim() + "_Upload_" + DateTime.Now.ToString("ddMMyyyy_HHmmss");
                string strFileType = System.IO.Path.GetExtension(IMEIOrderUploadFile.FileName).ToString().ToLower();
                //Check file type
                if (strFileType == ".xls" || strFileType == ".xlsx")
                {
                    IMEIOrderUploadFile.SaveAs(Server.MapPath("~/UploadedExcel/" + strFileName + strFileType));
                }
                else
                {
                    ShowUploadErrorMessage("Only excel files allowed");
                    return;
                }
                string strNewPath = Server.MapPath("~/UploadedExcel/" + strFileName + strFileType);
                List<string> attributeclonelist = new List<string>();
                IMEIOrderUploadProcessor processor = new IMEIOrderUploadProcessor(strNewPath, User.Identity.Name, txtOrderEntryNumber.Text, Location, false);

                lblOrderMessage.Text = processor.LoadIMEIData();
                processor.workbook.SaveAs(strFileName + strFileType, Page.Response, ExcelDownloadType.Open);
                processor.workbook.Close();
                processor.excelEngine.Dispose();
                lblOrderMessage.Visible = true;
            }
            else
            {
                ShowUploadErrorMessage("Please select an excel file first");
            }
        }



        protected void btnIMEISalesOrderUploadQuerry_Click(object sender, EventArgs e)
        {
            decimal ProjID = -1;
            //decimal ClientLocationID = -1;
            if (decimal.TryParse(drpProjectList_New.SelectedValue, out ProjID) == false) { ProjID = -1; }
            //if (decimal.TryParse(drpClientList_New.SelectedValue, out ClientLocationID) == false) { ClientLocationID = -1; }
            if ((IMEISalesOrderUploadQuerry.HasFile))
            {
                string strFileName = txtOrderEntryNumber.Text.Trim() + "_Upload_" + DateTime.Now.ToString("ddMMyyyy_HHmmss");
                string strFileType = System.IO.Path.GetExtension(IMEISalesOrderUploadQuerry.FileName).ToString().ToLower();
                //Check file type
                if (strFileType == ".xls" || strFileType == ".xlsx")
                {
                    IMEISalesOrderUploadQuerry.SaveAs(Server.MapPath("~/UploadedExcel/" + strFileName + strFileType));
                }
                else
                {
                    ShowUploadErrorMessage(lblIMEISalesOrderUploadQuerry, "Only excel files allowed");
                    return;
                }
                string strNewPath = Server.MapPath("~/UploadedExcel/" + strFileName + strFileType);
                List<string> attributeclonelist = new List<string>();
                IMEIOrderUploadProcessor processor = new IMEIOrderUploadProcessor(strNewPath, User.Identity.Name, "", null, false);

                lblIMEISalesOrderUploadQuerry.Text = processor.LoadIMEIDataQuerry();
                processor.workbook.SaveAs(strFileName + strFileType, Page.Response, ExcelDownloadType.Open);
                processor.workbook.Close();
                processor.excelEngine.Dispose();
                lblIMEISalesOrderUploadQuerry.Visible = true;
            }
            else
            {
                ShowUploadErrorMessage(lblIMEISalesOrderUploadQuerry, "Please select an excel file first");
            }
        }


        protected void btnIMEILocationMoveUpload_Click(object sender, EventArgs e)
        {
            //    decimal ProjID = -1;
            //    decimal ClientLocationID = -1;
            //    if (decimal.TryParse(drpProjectList_New.SelectedValue, out ProjID) == false) { ProjID = -1; }
            //    if (decimal.TryParse(drpClientList_New.SelectedValue, out ClientLocationID) == false) { ClientLocationID = -1; }
            if ((upFile_LocationMove.HasFile))
            {
                string strFileName = "LocMove_Upload_" + DateTime.Now.ToString("ddMMyyyy_HHmmss");
                string strFileType = System.IO.Path.GetExtension(upFile_LocationMove.FileName).ToString().ToLower();
                //Check file type
                if (strFileType == ".xls" || strFileType == ".xlsx")
                {
                    upFile_LocationMove.SaveAs(Server.MapPath("~/UploadedExcel/" + strFileName + strFileType));
                }
                else
                {
                    ShowUploadErrorMessage("Only excel files allowed");
                    return;
                }
                string strNewPath = Server.MapPath("~/UploadedExcel/" + strFileName + strFileType);
                List<string> attributeclonelist = new List<string>();

                IMEIMoveUploadProcessor processor = new IMEIMoveUploadProcessor(strNewPath, User.Identity.Name);
                lblOrderMessage.Text = processor.LoadIMEIData();
                processor.workbook.SaveAs(strFileName + strFileType, Page.Response, ExcelDownloadType.Open);
                processor.workbook.Close();
                processor.excelEngine.Dispose();
                lblOrderMessage.Visible = true;
            }
            else
            {
                ShowUploadErrorMessage("Please select an excel file first");
            }
        }


        void ShowUploadErrorMessage(string Message)
        {
            lblOrderMessage.Text = Message;
            lblOrderMessage.ForeColor = System.Drawing.Color.Red;
            lblOrderMessage.Visible = true;
        }

        void ShowUploadErrorMessage(Label lbl, string Message)
        {
            lbl.Text = Message;
            lbl.ForeColor = System.Drawing.Color.Red;
            lbl.Visible = true;
        }

        protected void btnIMEIUpload_PRIz_Click(object sender, EventArgs e)
        {

            //decimal ProjID = -1;
            decimal ClientLocationID = -1;
            //if (decimal.TryParse(drpProjectList_New.SelectedValue, out ProjID) == false) { ProjID = -1; }
            //if (decimal.TryParse(drpClientList_PRI.SelectedValue, out ClientLocationID) == false) { ClientLocationID = -1; }
            if ((IMEIUploadFile_PRIz.HasFile))
            {
                log.LogIt("**** btnIMEIUpload_PRIz_Click -- Started");
                string strFileName = DateTime.Now.ToString("ddMMyyyy_HHmmss");
                string strFileType = System.IO.Path.GetExtension(IMEIUploadFile_PRIz.FileName).ToString().ToLower();
                //Check file type
                if (strFileType == ".xls" || strFileType == ".xlsx")
                {
                    IMEIUploadFile_PRIz.SaveAs(Server.MapPath("~/UploadedExcel/" + strFileName + strFileType));
                }
                else
                {
                    lblIMEIUploadMSG_PRIz.Text = "Only excel files allowed";
                    lblIMEIUploadMSG_PRIz.ForeColor = System.Drawing.Color.Red;
                    lblIMEIUploadMSG_PRIz.Visible = true;
                    return;
                }
                string strNewPath = Server.MapPath("~/UploadedExcel/" + strFileName + strFileType);


                IMEIPreReceiveProcessor processor = new IMEIPreReceiveProcessor(strNewPath, User.Identity.Name, ClientLocationID, log, chkForce15IMEI_Prez.Checked);

                lblIMEIUploadMSG_PRIz.Text = IMEIUploadFile_PRIz.FileName + "   " + processor.LoadIMEIDataPreviousRecord();
                ////lblIMEIUploadMSG.ForeColor = System.Drawing.Color.Red;
                lblIMEIUploadMSG_PRIz.Visible = true;

                //// If the user wants to see the output, then here it is.
                //ExcelEngine excelEngine = new ExcelEngine();
                //IApplication application = excelEngine.Excel;
                //IWorkbook workbook = application.Workbooks.Open(strNewPath, ExcelOpenType.Automatic);

                processor.workbook.SaveAs(strFileName + strFileType, Page.Response, ExcelDownloadType.Open);
                processor.workbook.Close();
                processor.excelEngine.Dispose();
                ////////////////////////////////////////////////////
                log.LogIt("**** btnIMEIUpload_PRIz_Click -- Ended");
            }
            else
            {
                lblIMEIUploadMSG_PRIz.Text = "Please select an excel file first";
                lblIMEIUploadMSG_PRIz.ForeColor = System.Drawing.Color.Red;
                lblIMEIUploadMSG_PRIz.Visible = true;
            }

        }



        protected void btnIMEIUpload_PRI_Click(object sender, EventArgs e)
        {

            //decimal ProjID = -1;
            decimal ClientLocationID = -1;
            //if (decimal.TryParse(drpProjectList_New.SelectedValue, out ProjID) == false) { ProjID = -1; }
            //if (decimal.TryParse(drpClientList_PRI.SelectedValue, out ClientLocationID) == false) { ClientLocationID = -1; }
            if ((IMEIUploadFile_PRI.HasFile))
            {
                log.LogIt("**** btnIMEIUpload_PRI_Click -- Started");
                string strFileName = DateTime.Now.ToString("ddMMyyyy_HHmmss");
                string strFileType = System.IO.Path.GetExtension(IMEIUploadFile_PRI.FileName).ToString().ToLower();
                //Check file type
                if (strFileType == ".xls" || strFileType == ".xlsx")
                {
                    IMEIUploadFile_PRI.SaveAs(Server.MapPath("~/UploadedExcel/" + strFileName + strFileType));
                }
                else
                {
                    lblIMEIUploadMSG_PRI.Text = "Only excel files allowed";
                    lblIMEIUploadMSG_PRI.ForeColor = System.Drawing.Color.Red;
                    lblIMEIUploadMSG_PRI.Visible = true;
                    return;
                }
                string strNewPath = Server.MapPath("~/UploadedExcel/" + strFileName + strFileType);


                IMEIPreReceiveProcessor processor = new IMEIPreReceiveProcessor(strNewPath, User.Identity.Name, ClientLocationID, log, chkForce15IMEI_Pre.Checked);

                lblIMEIUploadMSG_PRI.Text = IMEIUploadFile_PRI.FileName + "   " + processor.LoadIMEIData();
                ////lblIMEIUploadMSG.ForeColor = System.Drawing.Color.Red;
                lblIMEIUploadMSG_PRI.Visible = true;

                //// If the user wants to see the output, then here it is.
                //ExcelEngine excelEngine = new ExcelEngine();
                //IApplication application = excelEngine.Excel;
                //IWorkbook workbook = application.Workbooks.Open(strNewPath, ExcelOpenType.Automatic);

                processor.workbook.SaveAs(strFileName + strFileType, Page.Response, ExcelDownloadType.Open);
                processor.workbook.Close();
                processor.excelEngine.Dispose();
                ////////////////////////////////////////////////////
                log.LogIt("**** btnIMEIUpload_PRI_Click -- Ended");
            }
            else
            {
                lblIMEIUploadMSG_PRI.Text = "Please select an excel file first";
                lblIMEIUploadMSG_PRI.ForeColor = System.Drawing.Color.Red;
                lblIMEIUploadMSG_PRI.Visible = true;
            }

        }


        protected void btnIMEIVersionUpload_Click(object sender, EventArgs e)
        {
            //log.LogIt("**** btnIMEIVersionUpload_Click -- Started");
            //decimal ProjID = -1;
            //decimal ClientLocationID = -1;
            //if (decimal.TryParse(drpProjectList_New.SelectedValue, out ProjID) == false) { ProjID = -1; }
            //if (decimal.TryParse(drpClientList_New.SelectedValue, out ClientLocationID) == false) { ClientLocationID = -1; }


            if ((IMEIVERSION000.HasFile))
            {
                string strFileName = DateTime.Now.ToString("ddMMyyyy_HHmmss");
                string strFileType = System.IO.Path.GetExtension(IMEIVERSION000.FileName).ToString().ToLower();
                //Check file type
                if (strFileType == ".xls" || strFileType == ".xlsx")
                {
                    IMEIVERSION000.SaveAs(Server.MapPath("~/UploadedExcel/" + strFileName + strFileType));
                }
                else
                {
                    lblIMEIVersionUploadMSG.Text = "Only excel files allowed";
                    lblIMEIVersionUploadMSG.ForeColor = System.Drawing.Color.Red;
                    lblIMEIVersionUploadMSG.Visible = true;
                    btnIMEIUpload.Enabled = true;
                    return;
                }
                string strNewPath = Server.MapPath("~/UploadedExcel/" + strFileName + strFileType);
                //List<string> attributeclonelist = new List<string>();
                //if (chkCloneSeedData.Checked)
                //{
                //    attributeclonelist.Add("Carrier");
                //    attributeclonelist.Add("Manufacturer");
                //    attributeclonelist.Add("Model");
                //    attributeclonelist.Add("Colour");
                //    attributeclonelist.Add("Disposition");          // may need to remove this one.
                //    attributeclonelist.Add("QC");                   // may need to change this to another name when Sandbox becomes live.
                //    //attributeclonelist.Add("QC Assessment");                   // may need to change this to another name when Sandbox becomes live.
                //    attributeclonelist.Add("Fault Code 1");
                //    //attributeclonelist.Add("Grade:in-bound Grade");   // put "in-bound Grade"
                //}

                IMEIVersionUploadProcessor processor = new IMEIVersionUploadProcessor(strNewPath, User.Identity.Name, log);

                lblIMEIVersionUploadMSG.Text = IMEIVERSION000.FileName + "   " + processor.LoadIMEIData();
                //lblIMEIVersionUploadMSG.ForeColor = System.Drawing.Color.Red;
                lblIMEIVersionUploadMSG.Visible = true;
                log.LogIt("**** btnIMEIVersionUpload_Click -- Downloading XLS");

                btnIMEIUpload.Enabled = true;
                btnIMEIUpload.Text = "Upload";


                processor.workbook.SaveAs(strFileName + strFileType, Page.Response, ExcelDownloadType.Open);
                processor.workbook.Close();
                processor.excelEngine.Dispose();


                //DownloadXLS(strNewPath);
            }
            else
            {
                lblIMEIVersionUploadMSG.Text = "Please select an excel file first";
                lblIMEIVersionUploadMSG.ForeColor = System.Drawing.Color.Red;
                lblIMEIVersionUploadMSG.Visible = true;
            }
            //btnIMEIUpload.Enabled = true;
        }

        protected void btnIMEIDownload_Click(object sender, EventArgs e)
        {
            if (tbBatchNumber.Text.Length == 0)
            {
                lblIMEIUploadMSG.Text = "You must supply a Batch number first";
                lblIMEIUploadMSG.ForeColor = System.Drawing.Color.Red;
                lblIMEIUploadMSG.Visible = true;
                btnIMEIUpload.Enabled = true;
                return;
            }
            string cmdString = "";
            cmdString = "Select * from ReceiveDetailExcelUploadLog where KeyIdentifier = '" + tbBatchNumber.Text + "' order by CreateDate";
            ExportToExcel(cmdString, "IMEIUploadReport");
        }
        protected void btnIMEIUpload_Click(object sender, EventArgs e)
        {
            log.LogIt("**** btnIMEIUpload_Click -- Started");
            if (tbBatchNumber.Text.Length == 0)
            {
                lblIMEIUploadMSG.Text = "You must supply a Batch number first";
                lblIMEIUploadMSG.ForeColor = System.Drawing.Color.Red;
                lblIMEIUploadMSG.Visible = true;
                btnIMEIUpload.Enabled = true;
                return;
            }
            if (CL_Skcankey.Text.Length == 0)
            {
                lblIMEIUploadMSG.Text = "You must supply a valid Scankey first";
                lblIMEIUploadMSG.ForeColor = System.Drawing.Color.Red;
                lblIMEIUploadMSG.Visible = true;
                btnIMEIUpload.Enabled = true;
                return;
            }
            // lookup the scankey to make sure it is valid
            ReceiveDetailManager rm = new ReceiveDetailManager(User.Identity.Name);
            ClientLocation cl = rm.GetClientLocation(CL_Skcankey.Text);
            if (cl == null)
            {
                lblIMEIUploadMSG.Text = "You must supply a valid Scankey first. (" + CL_Skcankey.Text + ") is not found/invalid:";
                lblIMEIUploadMSG.ForeColor = System.Drawing.Color.Red;
                lblIMEIUploadMSG.Visible = true;
                btnIMEIUpload.Enabled = true;
                return;
            }


            decimal ProjID = -1;
            //decimal ClientLocationID = -1;
            if (decimal.TryParse(drpProjectList_New.SelectedValue, out ProjID) == false) { ProjID = -1; }
            //if (decimal.TryParse(drpClientList_New.SelectedValue, out ClientLocationID) == false) { ClientLocationID = -1; }
            if (IMEIUploadFile.HasFile == false)
            {
                lblIMEIUploadMSG.Text = "Please select an excel file first";
                lblIMEIUploadMSG.ForeColor = System.Drawing.Color.Red;
                lblIMEIUploadMSG.Visible = true;
                btnIMEIUpload.Enabled = true;
                return;
            }
            #region Verify proper file
            string strFileName = DateTime.Now.ToString("ddMMyyyy_HHmmss");
            string strFileType = System.IO.Path.GetExtension(IMEIUploadFile.FileName).ToString().ToLower();
            //Check file type
            if (strFileType == ".xls" || strFileType == ".xlsx")
            {
                IMEIUploadFile.SaveAs(Server.MapPath("~/UploadedExcel/" + strFileName + strFileType));
            }
            else
            {
                lblIMEIUploadMSG.Text = "Only excel files allowed";
                lblIMEIUploadMSG.ForeColor = System.Drawing.Color.Red;
                lblIMEIUploadMSG.Visible = true;
                btnIMEIUpload.Enabled = true;
                return;
            }
            string strNewPath = Server.MapPath("~/UploadedExcel/" + strFileName + strFileType);
            #endregion
            #region Setup Values
            List<string> attributeclonelist = new List<string>();
            if (chkCloneSeedData.Checked)
            {
                attributeclonelist.Add("Carrier");
                attributeclonelist.Add("Manufacturer");
                attributeclonelist.Add("Model");
                attributeclonelist.Add("Colour");
                attributeclonelist.Add("Disposition");          // may need to remove this one.
                attributeclonelist.Add("QC");                   // may need to change this to another name when Sandbox becomes live.
                //attributeclonelist.Add("QC Assessment");                   // may need to change this to another name when Sandbox becomes live.
                attributeclonelist.Add("Fault Code 1");
                //attributeclonelist.Add("Grade:in-bound Grade");   // put "in-bound Grade"
            }
            #endregion
            //string ifsType = IFSTransactionType.SelectedItem.Value;
            IMEIUploadProcessor processor = new IMEIUploadProcessor(tbBatchNumber.Text, strNewPath, User.Identity.Name, ProjID, CL_Skcankey.Text, attributeclonelist, log, chkForce15IMEI.Checked, chkRunThreaded_Newa.Checked, "INV_RECEIPT");
            //if (processor.clientlocationid < 1)
            //{
            //    lblIMEIUploadMSG.Text = "You must supply a valid Scankey first";
            //    lblIMEIUploadMSG.ForeColor = System.Drawing.Color.Red;
            //    lblIMEIUploadMSG.Visible = true;
            //    btnIMEIUpload.Enabled = true;
            //    return;
            //}
            lblIMEIUploadMSG.Text = IMEIUploadFile.FileName + "   " + processor.LoadIMEIData();
            lblIMEIUploadMSG.Visible = true;
            btnIMEIUpload.Text = "Upload";
            //Page.Response.Flush();
            processor.workbook.SaveAs(strFileName + strFileType, Page.Response, ExcelDownloadType.Open);
            //processor.workbook.SaveAs(strFileName + strFileType, Page.Response, ExcelDownloadType.Open);
            processor.workbook.Close();
            processor.excelEngine.Dispose();
            lblIMEIUploadMSG.Visible = true;
            btnIMEIUpload.Enabled = true;
        }

        //protected void IMEIUploadFileSeedData_Click(object sender, EventArgs e)
        //{
        //    log.LogIt("**** IMEIUploadFileSeedData_Click -- Started");
        //    decimal ProjID = -1;
        //    decimal ClientLocationID = -1;
        //    if (decimal.TryParse(drpProjectList_New.SelectedValue, out ProjID) == false) { ProjID = -1; }
        //    if (decimal.TryParse(drpClientList_New.SelectedValue, out ClientLocationID) == false) { ClientLocationID = -1; }
        //    if ((IMEIUploadFileSeedData.HasFile))
        //    {
        //        string strFileName = DateTime.Now.ToString("ddMMyyyy_HHmmss");
        //        string strFileType = System.IO.Path.GetExtension(IMEIUploadFileSeedData.FileName).ToString().ToLower();
        //        //Check file type
        //        if (strFileType == ".xls" || strFileType == ".xlsx")
        //        {
        //            IMEIUploadFileSeedData.SaveAs(Server.MapPath("~/UploadedExcel/" + strFileName + strFileType));
        //        }
        //        else
        //        {
        //            lblIMEIUploadMSGSeedData.Text = "Only excel files allowed";
        //            lblIMEIUploadMSGSeedData.ForeColor = System.Drawing.Color.Red;
        //            lblIMEIUploadMSGSeedData.Visible = true;
        //            btnIMEIUploadSeedData.Enabled = true;
        //            return;
        //        }
        //        string strNewPath = Server.MapPath("~/UploadedExcel/" + strFileName + strFileType);
        //        List<string> attributeclonelist = new List<string>();
        //        attributeclonelist.Add("Carrier");
        //        attributeclonelist.Add("Manufacturer");
        //        attributeclonelist.Add("Model");
        //        attributeclonelist.Add("Colour");
        //        attributeclonelist.Add("Disposition");          // may need to remove this one.

        //        attributeclonelist.Add("QC");                   // may need to change this to another name when Sandbox becomes live.
        //        //attributeclonelist.Add("QC Assessment");                   // may need to change this to another name when Sandbox becomes live.

        //        attributeclonelist.Add("Fault Code 1");

        //        IMEIUploadProcessor processor = new IMEIUploadProcessor(strNewPath, User.Identity.Name, ProjID, ClientLocationID, attributeclonelist, log, chkForce15IMEI_ResetSeed.Checked);

        //        lblIMEIUploadMSGSeedData.Text = IMEIUploadFileSeedData.FileName + "   " + processor.LoadIMEISeedData();
        //        //lblIMEIUploadMSGSeedData.ForeColor = System.Drawing.Color.Red;
        //        lblIMEIUploadMSGSeedData.Visible = true;
        //        log.LogIt("**** IMEIUploadFileSeedData_Click -- Downloading XLS");

        //        btnIMEIUploadSeedData.Enabled = true;
        //        btnIMEIUploadSeedData.Text = "Upload";

        //        //DownloadXLS(strNewPath);
        //    }
        //    else
        //    {
        //        lblIMEIUploadMSGSeedData.Text = "Please select an excel file first";
        //        lblIMEIUploadMSGSeedData.ForeColor = System.Drawing.Color.Red;
        //        lblIMEIUploadMSGSeedData.Visible = true;
        //    }
        //    btnIMEIUploadSeedData.Enabled = true;
        //}


        private void DownloadXLS(string PathFileName)
        {
            //string templatePath = Page.MapPath(PathName);
            string templatePath = PathFileName;
            ExcelEngine excelEngine = new ExcelEngine();
            IApplication application = excelEngine.Excel;
            IWorkbook workbook = application.Workbooks.Open(templatePath, ExcelOpenType.Automatic);

            foreach (IWorksheet sheet in workbook.Worksheets)
            {
                sheet.UsedRange.AutofitColumns();
            }
            workbook.SaveAs(PathFileName, Page.Response, ExcelDownloadType.Open);
            workbook.Close();
            excelEngine.Dispose();
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {

            //This Utility will load Attribute values to existing IMEI version zero records
            if ((txtFilePath.HasFile && txtQuestionName.Text.Trim().Length > 0))
            {
                string strFileName = DateTime.Now.ToString("ddMMyyyy_HHmmss");
                string strFileType = System.IO.Path.GetExtension(txtFilePath.FileName).ToString().ToLower();
                //Check file type
                if (strFileType == ".xls" || strFileType == ".xlsx")
                {
                    txtFilePath.SaveAs(Server.MapPath("~/UploadedExcel/" + strFileName + strFileType));
                }
                else
                {
                    lblMessage.Text = "Only excel files allowed";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    lblMessage.Visible = true;
                    return;
                }
                string strNewPath = Server.MapPath("~/UploadedExcel/" + strFileName + strFileType);

                //Step 1 : Instantiate the spreadsheet creation engine.
                ExcelEngine excelEngine = new ExcelEngine();

                //Step 2 : Instantiate the excel application object.
                IApplication application = excelEngine.Excel;

                //Open an existing spreadsheet which will be used as a template for generating the new spreadsheet.
                //After opening, the workbook object represents the complete in-memory object model of the template spreadsheet.
                IWorkbook workbook = application.Workbooks.Open(strNewPath, ExcelOpenType.Automatic);
                //The first worksheet object in the worksheets collection is accessed.
                IWorksheet sheet = workbook.Worksheets[0];

                List<string[]> al = new List<string[]>();

                // New way

                #region NEWWay
                int count = 0;
                int Seq = 0;
                using (clsLinqDataContext ctx = new clsLinqDataContext())
                {
                    for (int row = 2; row < 1000000; row++)
                    {
                        string[] data = { "", "", "", "" };
                        if (sheet.Range[row, 1].Value == null || sheet.Range[row, 1].Value.Length == 0) { break; }
                        data[0] = sheet.Range[row, 1].Value;      // Scankey
                        data[1] = sheet.Range[row, 2].Value;      // Answer
                        data[2] = sheet.Range[row, 3].Value;      // Name
                        data[3] = sheet.Range[row, 4].Value;      // Seq
                        if (int.TryParse(data[3], out Seq) == false) { data[3] = "100"; }
                        if (data[0].Trim().Length > 0 || data[1].Trim().Length > 0 || data[2].Trim().Length > 0 || data[3].Trim().Length > 0)
                        {
                            count++;
                            sheet.Range[row, 4].Value = ctx.HelperAddAttributeValue(txtQuestionName.Text.Trim(), data[0], data[2], data[1], data[3], User.Identity.Name);
                        }
                        else
                        {
                            sheet.Range[row, 4].Value = "Error:Missing data elements";
                        }
                    }

                }
                workbook.Close();
                #endregion
                #region OldWay
                // OLD way ////////////////////////////////////////////////////////////////////////////////////////
                //for (int row = 2; row < 1000000; row++)
                //{
                //    string[] data = { "", "", "", "" };
                //    if (sheet.Range[row, 1].Value == null || sheet.Range[row, 1].Value.Length == 0) { break; }
                //    data[0] = sheet.Range[row, 1].Value;      // Scankey
                //    data[1] = sheet.Range[row, 2].Value;      // Answer
                //    data[2] = sheet.Range[row, 3].Value;      // Name
                //    data[3] = sheet.Range[row, 4].Value;      // Seq
                //    if (data[0].Trim().Length > 0 || data[1].Trim().Length > 0 || data[2].Trim().Length > 0 || data[3].Trim().Length > 0)
                //    {
                //        al.Add(data);
                //    }
                //}
                //workbook.Close();
                //// Load the attributes

                //int count = 0;
                //using (clsLinqDataContext ctx = new clsLinqDataContext())
                //{
                //    foreach (string[] optionvalue in al)
                //    {
                //        count++;
                //        //ctx.Utility_LoadAttributeValue(txtQuestionName.Text.Trim(), optionvalue[0]);
                //        ctx.Utility_LoadAttributeValue_02(txtQuestionName.Text.Trim(), optionvalue[0], optionvalue[2], optionvalue[1], optionvalue[3], User.Identity.Name);
                //    }
                //}
                //////////////////////////////////////////////////////////
                #endregion
                lblMessage.Text = count.ToString() + " Attributes Loaded.";
                lblMessage.ForeColor = System.Drawing.Color.Green;
                lblMessage.Visible = true;

                //excelEngine.Dispose();
                //grvExcelData.DataSource = al;
                //grvExcelData.DataBind();
            }
            else
            {
                lblMessage.Text = "Please select an excel file first or Enter a Question Name.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Visible = true;
            }
        }

        protected void btnUploadIMEIAttribute_Click(object sender, EventArgs e)
        {
            bool UploadToLastProcessedVersion = cbUploadToLastVersion.Checked;
            if (UploadToLastProcessedVersion == true)
            {
                log.LogIt("**** btnUploadIMEIAttribute_Click -- Started -- Last Version Processed");
            }
            else
            {
                log.LogIt("**** btnUploadIMEIAttribute_Click -- Started");
            }
            if ((FileUploadIMEIAttribute.HasFile))
            {
                string strFileName = DateTime.Now.ToString("ddMMyyyy_HHmmss");
                string strFileType = System.IO.Path.GetExtension(FileUploadIMEIAttribute.FileName).ToString().ToLower();
                //Check file type
                if (strFileType == ".xls" || strFileType == ".xlsx")
                {
                    FileUploadIMEIAttribute.SaveAs(Server.MapPath("~/UploadedExcel/" + strFileName + strFileType));
                }
                else
                {
                    lblMessageIMEIAttribute.Text = "Only excel files allowed";
                    lblMessageIMEIAttribute.ForeColor = System.Drawing.Color.Red;
                    lblMessageIMEIAttribute.Visible = true;
                    btnUploadIMEIAttribute.Enabled = true;
                    return;
                }
                string strNewPath = Server.MapPath("~/UploadedExcel/" + strFileName + strFileType);

                //Step 1 : Instantiate the spreadsheet creation engine.
                ExcelEngine excelEngine = new ExcelEngine();
                //Step 2 : Instantiate the excel application object.
                IApplication application = excelEngine.Excel;
                //Open an existing spreadsheet which will be used as a template for generating the new spreadsheet.
                //After opening, the workbook object represents the complete in-memory object model of the template spreadsheet.
                IWorkbook workbook = application.Workbooks.Open(strNewPath, ExcelOpenType.Automatic);
                //The first worksheet object in the worksheets collection is accessed.
                IWorksheet sheet = workbook.Worksheets[0];

                string IMEI = "";
                string AttributeName = "";
                string AttributeValue = "";
                int StatusCol = 0;




                int CarrierCol = 0;
                int ManufacturerCol = 0;
                int ModelCol = 0;
                int ColourCol = 0;
                int MemoryCol = 0;
                int UnlockedStatusCol = 0;
                int GradeCol = 0;
                int IsKittedCol = 0;
                int RefurbCol = 0;
                int CountryCol = 0;


                //int ConditionCol = 0;

                string Carrier = "";
                string Manufacturer = "";
                string Model = "";
                string Colour = "";

                string Memory = "";
                string UnlockedStatus = "";
                string Grade = "";
                string isKitted = "";
                string Refurb = "";
                string Country = "";





                //string Condition = "";
                string ErrorMessage = "";

                //bool TellIFS = false;
                IFSKeyValues Original = null;
                IFSKeyValues Current = null;
                short DirectiveNormalMove = -1;

                bool TestSKU = false;
                bool TestCondition = false;


                // Get the Pertinent columns that need to be edited... as well as the last column + 1 for the Status Results.
                for (int col = 2; col < 1000000; col++)
                {
                    if (sheet.Range[1, col].Value == null || sheet.Range[1, col].Value.Trim().Length == 0) { sheet.Range[1, col].Value = "Status"; StatusCol = col + 1; break; }
                    if (sheet.Range[1, col].Value.ToUpper() == "CARRIER") { CarrierCol = col; TestSKU = true; }
                    if (sheet.Range[1, col].Value.ToUpper() == "MANUFACTURER") { ManufacturerCol = col; TestSKU = true; }
                    if (sheet.Range[1, col].Value.ToUpper() == "MODEL") { ModelCol = col; TestSKU = true; }
                    if (sheet.Range[1, col].Value.ToUpper() == "COLOUR") { ColourCol = col; TestSKU = true; }
                    //if (sheet.Range[1, col].Value.ToUpper() == "IFS CONDITIONS") { ConditionCol = col; TestCondition = true; }

                    if (sheet.Range[1, col].Value.ToUpper() == "MEMORY") { MemoryCol = col; TestSKU = true; }
                    if (sheet.Range[1, col].Value.ToUpper() == "UNLOCKED STATUS") { UnlockedStatusCol = col; TestSKU = true; }
                    if (sheet.Range[1, col].Value.ToUpper() == "GRADE") { GradeCol = col; TestSKU = true; }
                    if (sheet.Range[1, col].Value.ToUpper() == "ISKITTED") { IsKittedCol = col; TestSKU = true; }
                    if (sheet.Range[1, col].Value.ToUpper() == "REFURB") { RefurbCol = col; TestSKU = true; }
                    if (sheet.Range[1, col].Value.ToUpper() == "COUNTRY") { CountryCol = col; TestSKU = true; }

                }
                using (clsLinqDataContext ctx = new clsLinqDataContext())
                {

                    ReceiveDetailUtilityMoveLogManager logu = new ReceiveDetailUtilityMoveLogManager("Upload IMEI Attribute", User.Identity.Name);
                    ReceiveDetailManager rm = new ReceiveDetailManager(User.Identity.Name);
                    DirectiveNormalMove = rm.IFSDirective(ctx, "Normal");
                    List<Question> Questions = ctx.Questions.Where(x => x.QuestionStatus.Status == "Active").ToList();
                    for (int row = 2; row < 1000000; row++)
                    {
                        //TellIFS = false;
                        if (sheet.Range[row, 1].Value == null || sheet.Range[row, 1].Value.Length == 0) { break; }
                        IMEI = sheet.Range[row, 1].Value;
                        ErrorMessage = "";
                        //ReceiveDetail rd = ctx.ReceiveDetails.FirstOrDefault(x => x.ESN == IMEI && x.Version == "000");
                        Original = new IFSKeyValues(IMEI, UploadToLastProcessedVersion);
                        if (Original.ReceiveDetailID < 1)
                        {
                            sheet.Range[row, StatusCol].Value = "IMEI Not found";
                        }
                        else
                        {
                            // if TestSKU or TestCondition is set, we have a change that needs to be told to IFS.
                            // Get the original data.
                            // 
                            Carrier = "";
                            Manufacturer = "";
                            Model = "";
                            Colour = "";
                            Memory = "";
                            UnlockedStatus = "";
                            Grade = "";
                            isKitted = "";
                            Refurb = "";
                            Country = "";
                            if (CarrierCol > 0) { Carrier = sheet.Range[row, CarrierCol].Value; }
                            if (ManufacturerCol > 0) { Manufacturer = sheet.Range[row, ManufacturerCol].Value; }
                            if (ModelCol > 0) { Model = sheet.Range[row, ModelCol].Value; }
                            if (ColourCol > 0) { Colour = sheet.Range[row, ColourCol].Value; }

                            if (MemoryCol > 0) { Memory = sheet.Range[row, MemoryCol].Value; }
                            if (UnlockedStatusCol > 0) { UnlockedStatus = sheet.Range[row, UnlockedStatusCol].Value; }
                            if (GradeCol > 0) { Grade = sheet.Range[row, GradeCol].Value; }
                            if (IsKittedCol > 0) { isKitted = sheet.Range[row, IsKittedCol].Value; }
                            if (RefurbCol > 0) { Refurb = sheet.Range[row, RefurbCol].Value; }
                            if (CountryCol > 0) { Country = sheet.Range[row, CountryCol].Value; }




                            //if (ConditionCol > 0) { Condition = sheet.Range[row, ConditionCol].Value; }

                            //if (TestCondition == true && ((Original.IFSSite.ToUpper() == "C1NA" && Condition.ToUpper() == "GEN") || (Original.IFSSite.ToUpper() != "C1NA" && Condition.ToUpper() != "GEN")))
                            //{
                            //    sheet.Range[row, StatusCol].Value = "Invalid IFS Conditions.";
                            //}
                            //else
                            //{
                            if (TestSKU == true && ctx.IsSkuValid(IMEI, Carrier, Manufacturer, Model, Colour) == false) { sheet.Range[row, StatusCol].Value = "Invalid SKU Combination."; }
                            else
                            {
                                int count = 0;
                                for (int col = 2; col < 1000000; col++)
                                {
                                    if (sheet.Range[row, col].Value == null || sheet.Range[row, col].Value.Trim().Length == 0) { break; }
                                    AttributeName = sheet.Range[1, col].Value;
                                    AttributeValue = sheet.Range[row, col].Value;
                                    if (AttributeValue.Trim().Length > 0 && IMEI.Length > 0 && AttributeName.Length > 0)
                                    {
                                        count = count + 1;
                                        if (AttributeValue.ToUpper() == "BLANK") { AttributeValue = ""; }
                                        AttributeName = AttributeName.Replace('_', ' ');
                                        //ctx.Utility_LoadIMEIAttributeValue(IMEI, AttributeName, AttributeValue);
                                        //if (Questions.Any(x => x.Name.ToUpper() == AttributeName.ToUpper()) == true) { ctx.UpdateESNAttribute(IMEI, AttributeName, AttributeValue, User.Identity.Name); }
                                        if (Questions.Any(x => x.Name.ToUpper() == AttributeName.ToUpper()) == true)
                                        {
                                            rm.UpdateESNAttribute(ctx, Original.ReceiveDetailID, AttributeName, AttributeValue);
                                            //if (TestSKU == true || TestCondition == true) { TellIFS = true; }
                                        }
                                        else { ErrorMessage = ErrorMessage + ";" + AttributeName + "(Invalid)"; }
                                    }
                                }
                                //if (TellIFS == true)
                                //{
                                //    Current = new IFSKeyValues(Original.ReceiveDetailID);
                                //    // we need to get the current values, the updates may have changed something.
                                //    if (Current.isIFSKeysEqual(Original) == false)
                                //    {
                                //        ctx.IFS_GenerateInvtTran_B(Original.ReceiveDetailID, DirectiveNormalMove, Original.IFSSKU, Original.IFSLocation, Original.IFSCondition, Current.IFSSKU, Current.IFSLocation, Current.IFSCondition, User.Identity.Name, -1, "");
                                //    }
                                //}
                                sheet.Range[row, StatusCol].Value = ErrorMessage + "; Attributes Updated (" + count.ToString() + ")";
                                logu.Save(Original.ReceiveDetailID, "Attributes Updated (" + count.ToString() + ")");
                            }
                            //}
                        }
                    }
                }

                workbook.SaveAs("AttributeUpload.xls", Page.Response, ExcelDownloadType.Open);
                workbook.Close();
                excelEngine.Dispose();

                lblMessageIMEIAttribute.Text = "File Loaded.";
                lblMessageIMEIAttribute.ForeColor = System.Drawing.Color.Red;
                lblMessageIMEIAttribute.Visible = true;
            }
            else
            {
                lblMessageIMEIAttribute.Text = "Please select an excel file first.";
                lblMessageIMEIAttribute.ForeColor = System.Drawing.Color.Red;
                lblMessageIMEIAttribute.Visible = true;
            }
            btnUploadIMEIAttribute.Enabled = true;
            log.LogIt("**** btnUploadIMEIAttribute_Click -- Finished");
        }

        protected void btnUploadSingleIMEIAttribute_Click_B(object sender, EventArgs e)
        {
            log.LogIt("**** btnUploadSingleIMEIAttribute_Click -- Started");
            //if ((FileUploadSingleIMEIAttribute.HasFile))
            //{
            //    string strFileName = DateTime.Now.ToString("ddMMyyyy_HHmmss");
            //    string strFileType = System.IO.Path.GetExtension(FileUploadSingleIMEIAttribute.FileName).ToString().ToLower();
            //    string OriginalName = System.IO.Path.GetFileNameWithoutExtension(FileUploadSingleIMEIAttribute.FileName).ToString().ToLower();
            //    string NameAndPath = Server.MapPath("~/UploadedExcel/" + strFileName + strFileType);
            //    //Check file type
            //    if (strFileType == ".xls" || strFileType == ".xlsx")
            //    {
            //        FileUploadSingleIMEIAttribute.SaveAs(Server.MapPath("~/UploadedExcel/" + strFileName + strFileType));
            //    }
            //    else
            //    {
            //        lblMessageSingleIMEIAttribute.Text = "Only excel files allowed";
            //        lblMessageSingleIMEIAttribute.ForeColor = System.Drawing.Color.Red;
            //        lblMessageSingleIMEIAttribute.Visible = true;
            //        lblMessageSingleIMEIAttribute.Enabled = true;
            //        return;
            //    }
            //    string strNewPath = Server.MapPath("~/UploadedExcel/" + strFileName + strFileType);

            //    //Step 1 : Instantiate the spreadsheet creation engine.
            //    ExcelEngine excelEngine = new ExcelEngine();
            //    //Step 2 : Instantiate the excel application object.
            //    IApplication application = excelEngine.Excel;
            //    //Open an existing spreadsheet which will be used as a template for generating the new spreadsheet.
            //    //After opening, the workbook object represents the complete in-memory object model of the template spreadsheet.
            //    IWorkbook workbook = application.Workbooks.Open(strNewPath, ExcelOpenType.Automatic);
            //    //The first worksheet object in the worksheets collection is accessed.
            //    IWorksheet sheet = workbook.Worksheets[0];

            //    string IMEI = "";
            //    string AttributeName = "";
            //    string AttributeValue = "";
            //    int StatusCol = 3;
            //    //int CarrierCol = 0;
            //    //int ManufacturerCol = 0;
            //    //int ModelCol = 0;
            //    //int ColourCol = 0;
            //    //int ConditionCol = 0;

            //    //string Carrier = "";
            //    //string Manufacturer = "";
            //    //string Model = "";
            //    //string Colour = "";
            //    //string Condition = "";

            //    int CarrierCol = 0;
            //    int ManufacturerCol = 0;
            //    int ModelCol = 0;
            //    int ColourCol = 0;
            //    int MemoryCol = 0;
            //    int UnlockedStatusCol = 0;
            //    int GradeCol = 0;
            //    int IsKittedCol = 0;
            //    int RefurbCol = 0;
            //    int CountryCol = 0;


            //    //int ConditionCol = 0;

            //    string Carrier = "";
            //    string Manufacturer = "";
            //    string Model = "";
            //    string Colour = "";

            //    string Memory = "";
            //    string UnlockedStatus = "";
            //    string Grade = "";
            //    string isKitted = "";
            //    string Refurb = "";
            //    string Country = "";


            //    string ErrorMessage = "";

            //    //bool TellIFS = false;
            //    List<IFSKeyValues> Original = new List<IFSKeyValues>();
            //    IFSKeyValues Current = null;
            //    short DirectiveNormalMove = -1;
            //    bool TestSKU = false;
            //    bool TestCondition = false;


            //    // Get the Pertinent columns that need to be edited... as well as the last column + 1 for the Status Results.
            //    int col = 2;
            //    if (sheet.Range[1, col].Value.ToUpper() == "CARRIER") { CarrierCol = col; TestSKU = true; }
            //    if (sheet.Range[1, col].Value.ToUpper() == "MANUFACTURER") { ManufacturerCol = col; TestSKU = true; }
            //    if (sheet.Range[1, col].Value.ToUpper() == "MODEL") { ModelCol = col; TestSKU = true; }
            //    if (sheet.Range[1, col].Value.ToUpper() == "COLOUR") { ColourCol = col; TestSKU = true; }
            //    //if (sheet.Range[1, col].Value.ToUpper() == "IFS CONDITIONS") { ConditionCol = col; TestCondition = true; }

            //    if (sheet.Range[1, col].Value.ToUpper() == "MEMORY") { MemoryCol = col; TestSKU = true; }
            //    if (sheet.Range[1, col].Value.ToUpper() == "UNLOCKED STATUS") { UnlockedStatusCol = col; TestSKU = true; }
            //    if (sheet.Range[1, col].Value.ToUpper() == "GRADE") { GradeCol = col; TestSKU = true; }
            //    if (sheet.Range[1, col].Value.ToUpper() == "ISKITTED") { IsKittedCol = col; TestSKU = true; }
            //    if (sheet.Range[1, col].Value.ToUpper() == "REFURB") { RefurbCol = col; TestSKU = true; }
            //    if (sheet.Range[1, col].Value.ToUpper() == "COUNTRY") { CountryCol = col; TestSKU = true; }
            //    try
            //    {
            //        using (clsLinqDataContext ctx = new clsLinqDataContext())
            //        {

            //            ReceiveDetailUtilityMoveLogManager logu = new ReceiveDetailUtilityMoveLogManager("Upload IMEI Attribute", User.Identity.Name);
            //            ReceiveDetailManager rm = new ReceiveDetailManager(User.Identity.Name);
            //            DirectiveNormalMove = rm.IFSDirective(ctx, "Normal");
            //            List<Question> Questions = ctx.Questions.Where(x => x.QuestionStatus.Status == "Active").ToList();

            //            AttributeValueList aList = new AttributeValueList();
            //            #region Travers all the rows
            //            for (int row = 2; row < 1000000; row++)
            //            {
            //                //TellIFS = false;
            //                if (sheet.Range[row, 1].Value == null || sheet.Range[row, 1].Value.Length == 0) { break; }
            //                IMEI = sheet.Range[row, 1].Value;
            //                ErrorMessage = "";
            //                //ReceiveDetail rd = ctx.ReceiveDetails.FirstOrDefault(x => x.ESN == IMEI && x.Version == "000");
            //                Current = new IFSKeyValues(IMEI);
            //                if (Current.ReceiveDetailID < 1)
            //                {
            //                    sheet.Range[row, StatusCol].Value = "IMEI Not found";
            //                }
            //                else
            //                {
            //                    Carrier = "";
            //                    Manufacturer = "";
            //                    Model = "";
            //                    Colour = "";
            //                    Memory = "";
            //                    UnlockedStatus = "";
            //                    Grade = "";
            //                    isKitted = "";
            //                    Refurb = "";
            //                    Country = "";
            //                    if (CarrierCol > 0) { Carrier = sheet.Range[row, CarrierCol].Value; }
            //                    if (ManufacturerCol > 0) { Manufacturer = sheet.Range[row, ManufacturerCol].Value; }
            //                    if (ModelCol > 0) { Model = sheet.Range[row, ModelCol].Value; }
            //                    if (ColourCol > 0) { Colour = sheet.Range[row, ColourCol].Value; }
            //                    if (MemoryCol > 0) { Memory = sheet.Range[row, MemoryCol].Value; }
            //                    if (UnlockedStatusCol > 0) { UnlockedStatus = sheet.Range[row, UnlockedStatusCol].Value; }
            //                    if (GradeCol > 0) { Grade = sheet.Range[row, GradeCol].Value; }
            //                    if (IsKittedCol > 0) { isKitted = sheet.Range[row, IsKittedCol].Value; }
            //                    if (RefurbCol > 0) { Refurb = sheet.Range[row, RefurbCol].Value; }
            //                    if (CountryCol > 0) { Country = sheet.Range[row, CountryCol].Value; }

            //                    //if (TestCondition == true && ((Current.IFSSite.ToUpper() == "C1NA" && Condition.ToUpper() == "GEN") || (Current.IFSSite.ToUpper() != "C1NA" && Condition.ToUpper() != "GEN")))
            //                    //{
            //                    //    sheet.Range[row, StatusCol].Value = "Invalid IFS Conditions.";
            //                    //}
            //                    //else
            //                    //{
            //                        if (TestSKU == true && ctx.IsSkuValid(IMEI, Carrier, Manufacturer, Model, Colour) == false) { sheet.Range[row, StatusCol].Value = "Invalid SKU Combination."; }
            //                        else
            //                        {
            //                            int count = 0;
            //                            //for (int col = 2; col < 1000000; col++)
            //                            //{
            //                            if (sheet.Range[row, col].Value == null || sheet.Range[row, col].Value.Trim().Length == 0) { break; }
            //                            AttributeName = sheet.Range[1, col].Value;
            //                            AttributeValue = sheet.Range[row, col].Value;
            //                            if (AttributeValue.Trim().Length > 0 && IMEI.Length > 0 && AttributeName.Length > 0)
            //                            {
            //                                count = count + 1;
            //                                if (AttributeValue.ToUpper() == "BLANK") { AttributeValue = ""; }
            //                                AttributeName = AttributeName.Replace('_', ' ');
            //                                //ctx.Utility_LoadIMEIAttributeValue(IMEI, AttributeName, AttributeValue);
            //                                //if (Questions.Any(x => x.Name.ToUpper() == AttributeName.ToUpper()) == true) { ctx.UpdateESNAttribute(IMEI, AttributeName, AttributeValue, User.Identity.Name); }
            //                                if (Questions.Any(x => x.Name.ToUpper() == AttributeName.ToUpper()) == true)
            //                                {
            //                                    aList.Add(AttributeName, AttributeValue, Current.ReceiveDetailID);
            //                                    //if (TestSKU == true || TestCondition == true) { Original.Add(Current.Clone()); }  // If we are dealing with a SKU or Condition Change, then we need to tell IFS.
            //                                    //rm.UpdateESNAttribute(ctx, rd.ReceiveDetailID, AttributeName, AttributeValue); 
            //                                }
            //                                else { ErrorMessage = ErrorMessage + ";" + AttributeName + "(Invalid)"; }
            //                            }
            //                            //}
            //                            sheet.Range[row, StatusCol].Value = ErrorMessage + "; Attribute: " + AttributeName + " Set (" + AttributeValue + ")";
            //                            logu.Save(Current.ReceiveDetailID, "Attribute: " + AttributeName + " Set (" + AttributeValue + ")");
            //                            log.LogIt("Row:" + row.ToString() + " Attribute: " + AttributeName + " Set (" + AttributeValue + ")");
            //                        }
            //                    //}
            //                }
            //            }
            //            #endregion
            //            // Time to update the database
            //            foreach (AttributeIMEIList a in aList.AttributeList)
            //            {
            //                ctx.ExecuteCommand("UpdateESNAttribute_ByID_InBulk {0}, {1}, {2}, {3}", a.ReceiveDetailIDListString, a.Attribute, a.Value, User.Identity.Name);
            //                log.LogIt("UpdateESNAttribute_ByID_InBulk: #Rows:" + a.ReceiveDetailIDList.Count().ToString() + " Attribute: " + a.Attribute + " Set (" + a.Value + ")");
            //            }
            //            //foreach (IFSKeyValues a in Original)
            //            //{
            //            //    Current = new IFSKeyValues(a.ReceiveDetailID);
            //            //    // we need to get the current values, the updates may have changed something.
            //            //    if (Current.isIFSKeysEqual(a) == false)
            //            //    {
            //            //        ctx.IFS_GenerateInvtTran_B(a.ReceiveDetailID, DirectiveNormalMove, a.IFSSKU, a.IFSLocation, a.IFSCondition, Current.IFSSKU, Current.IFSLocation, Current.IFSCondition, User.Identity.Name, -1, "");
            //            //    }
            //            //}
            //        }

            //    }
            //    catch (Exception ex)
            //    {
            //        sheet.Range[2, 4].Value =  "Error:" + ex.Message;
            //        log.LogIt("Error(a):" + ex.Message);
            //    }
            //    finally
            //    {
            //        try
            //        {
            //            workbook.Save();
            //            //workbook.SaveAs(strFileName + strFileType, Page.Response, ExcelDownloadType.Open);
            //            //workbook.SaveAs("SingleAttributeUpload.xls", Page.Response, ExcelDownloadType.Open);
            //            workbook.Close();
            //            excelEngine.Dispose();
            //            log.LogIt("Workbood Closed");
            //            SendFileToBrowser send = new SendFileToBrowser();
            //            send.StreamFileToBrowser(OriginalName + "_Uploaded" + strFileType, NameAndPath);
            //            log.LogIt("Sent file to Browser");
            //            lblMessageSingleIMEIAttribute.Text = "File Loaded.";
            //            lblMessageSingleIMEIAttribute.ForeColor = System.Drawing.Color.Red;
            //            lblMessageSingleIMEIAttribute.Visible = true;
            //            btnUploadSingleIMEIAttribute.Enabled = true;
            //        }
            //        catch (Exception ex)
            //        {
            //            ScriptManager.RegisterStartupScript(this, GetType(), "LoadClient", "alert(Error:" + ex.Message + ");", true);
            //            log.LogIt("Error(b):" + ex.Message);
            //        }
            //    }
            //}
            //else
            //{
            //    lblMessageSingleIMEIAttribute.Text = "Please select an excel file first.";
            //    lblMessageSingleIMEIAttribute.ForeColor = System.Drawing.Color.Red;
            //    lblMessageSingleIMEIAttribute.Visible = true;
            //}
            //btnUploadSingleIMEIAttribute.Enabled = true;
            log.LogIt("**** btnUploadSingleIMEIAttribute -- Finished");
        }

        protected void btnUploadSingleIMEIAttribute_Click(object sender, EventArgs e)
        {
            log.LogIt("**** btnUploadSingleIMEIAttribute_Click -- Started");
            //if ((FileUploadSingleIMEIAttribute.HasFile))
            //{
            //    string strFileName = DateTime.Now.ToString("ddMMyyyy_HHmmss");
            //    string strFileType = System.IO.Path.GetExtension(FileUploadSingleIMEIAttribute.FileName).ToString().ToLower();
            //    string OriginalName = System.IO.Path.GetFileNameWithoutExtension(FileUploadSingleIMEIAttribute.FileName).ToString().ToLower();
            //    string NameAndPath = Server.MapPath("~/UploadedExcel/" + strFileName + strFileType);
            //    //Check file type
            //    if (strFileType == ".xls" || strFileType == ".xlsx")
            //    {
            //        FileUploadSingleIMEIAttribute.SaveAs(Server.MapPath("~/UploadedExcel/" + strFileName + strFileType));
            //    }
            //    else
            //    {
            //        lblMessageSingleIMEIAttribute.Text = "Only excel files allowed";
            //        lblMessageSingleIMEIAttribute.ForeColor = System.Drawing.Color.Red;
            //        lblMessageSingleIMEIAttribute.Visible = true;
            //        lblMessageSingleIMEIAttribute.Enabled = true;
            //        return;
            //    }
            //    string strNewPath = Server.MapPath("~/UploadedExcel/" + strFileName + strFileType);

            //    //Step 1 : Instantiate the spreadsheet creation engine.
            //    ExcelEngine excelEngine = new ExcelEngine();
            //    //Step 2 : Instantiate the excel application object.
            //    IApplication application = excelEngine.Excel;
            //    //Open an existing spreadsheet which will be used as a template for generating the new spreadsheet.
            //    //After opening, the workbook object represents the complete in-memory object model of the template spreadsheet.
            //    IWorkbook workbook = application.Workbooks.Open(strNewPath, ExcelOpenType.Automatic);
            //    //The first worksheet object in the worksheets collection is accessed.
            //    IWorksheet sheet = workbook.Worksheets[0];

            //    string IMEI = "";
            //    string AttributeName = "";
            //    string AttributeValue = "";
            //    int StatusCol = 3;
            //    //int CarrierCol = 0;
            //    //int ManufacturerCol = 0;
            //    //int ModelCol = 0;
            //    //int ColourCol = 0;
            //    //int ConditionCol = 0;

            //    //string Carrier = "";
            //    //string Manufacturer = "";
            //    //string Model = "";
            //    //string Colour = "";
            //    //string Condition = "";


            //    //int IMEICol = 0;
            //    //int AttributeValueCol = 1;
            //    //string aValue = "";



            //    int CarrierCol = 0;
            //    int ManufacturerCol = 0;
            //    int ModelCol = 0;
            //    int ColourCol = 0;
            //    int MemoryCol = 0;
            //    int UnlockedStatusCol = 0;
            //    int GradeCol = 0;
            //    int IsKittedCol = 0;
            //    int RefurbCol = 0;
            //    int CountryCol = 0;
            //    //int ConditionCol = 0;
            //    string Carrier = "";
            //    string Manufacturer = "";
            //    string Model = "";
            //    string Colour = "";
            //    string Memory = "";
            //    string UnlockedStatus = "";
            //    string Grade = "";
            //    string isKitted = "";
            //    string Refurb = "";
            //    string Country = "";


            //    string ErrorMessage = "";

            //    //bool TellIFS = false;
            //    List<IFSKeyValues> Original = new List<IFSKeyValues>();
            //    IFSKeyValues Current = null;
            //    short DirectiveNormalMove = -1;
            //    bool TestSKU = false;
            //    bool TestCondition = false;


            //    // Get the Pertinent columns that need to be edited... as well as the last column + 1 for the Status Results.
            //    int col = 2;
            //    if (sheet.Range[1, col].Value.ToUpper() == "CARRIER") { CarrierCol = col; TestSKU = true; }
            //    if (sheet.Range[1, col].Value.ToUpper() == "MANUFACTURER") { ManufacturerCol = col; TestSKU = true; }
            //    if (sheet.Range[1, col].Value.ToUpper() == "MODEL") { ModelCol = col; TestSKU = true; }
            //    if (sheet.Range[1, col].Value.ToUpper() == "COLOUR") { ColourCol = col; TestSKU = true; }
            //    //if (sheet.Range[1, col].Value.ToUpper() == "IFS CONDITIONS") { ConditionCol = col; TestCondition = true; }

            //    if (sheet.Range[1, col].Value.ToUpper() == "MEMORY") { MemoryCol = col; TestSKU = true; }
            //    if (sheet.Range[1, col].Value.ToUpper() == "UNLOCKED STATUS") { UnlockedStatusCol = col; TestSKU = true; }
            //    if (sheet.Range[1, col].Value.ToUpper() == "GRADE") { GradeCol = col; TestSKU = true; }
            //    if (sheet.Range[1, col].Value.ToUpper() == "ISKITTED") { IsKittedCol = col; TestSKU = true; }
            //    if (sheet.Range[1, col].Value.ToUpper() == "REFURB") { RefurbCol = col; TestSKU = true; }
            //    if (sheet.Range[1, col].Value.ToUpper() == "COUNTRY") { CountryCol = col; TestSKU = true; }
            //    try
            //    {
            //        using (clsLinqDataContext ctx = new clsLinqDataContext())
            //        {

            //            ReceiveDetailUtilityMoveLogManager logu = new ReceiveDetailUtilityMoveLogManager("Upload IMEI Attribute", User.Identity.Name);
            //            ReceiveDetailManager rm = new ReceiveDetailManager(User.Identity.Name);
            //            DirectiveNormalMove = rm.IFSDirective(ctx, "Normal");
            //            List<Question> Questions = ctx.Questions.Where(x => x.QuestionStatus.Status == "Active").ToList();

            //            AttributeValueList aList = new AttributeValueList();
            //            #region Travers all the rows
            //            for (int row = 2; row < 1000000; row++)
            //            {
            //                //TellIFS = false;
            //                if (sheet.Range[row, 1].Value == null || sheet.Range[row, 1].Value.Length == 0) { break; }
            //                IMEI = sheet.Range[row, 1].Value.Trim();
            //                ErrorMessage = "";
            //                //ReceiveDetail rd = ctx.ReceiveDetails.FirstOrDefault(x => x.ESN == IMEI && x.Version == "000");
            //                Current = new IFSKeyValues(IMEI);
            //                if (Current.ReceiveDetailID < 1)
            //                {
            //                    sheet.Range[row, StatusCol].Value = "IMEI Not found";
            //                }
            //                else
            //                {
            //                    Carrier = "";
            //                    Manufacturer = "";
            //                    Model = "";
            //                    Colour = "";
            //                    Memory = "";
            //                    UnlockedStatus = "";
            //                    Grade = "";
            //                    isKitted = "";
            //                    Refurb = "";
            //                    Country = "";
            //                    if (CarrierCol > 0) { Carrier = sheet.Range[row, CarrierCol].Value; }
            //                    if (ManufacturerCol > 0) { Manufacturer = sheet.Range[row, ManufacturerCol].Value; }
            //                    if (ModelCol > 0) { Model = sheet.Range[row, ModelCol].Value; }
            //                    if (ColourCol > 0) { Colour = sheet.Range[row, ColourCol].Value; }
            //                    if (MemoryCol > 0) { Memory = sheet.Range[row, MemoryCol].Value; }
            //                    if (UnlockedStatusCol > 0) { UnlockedStatus = sheet.Range[row, UnlockedStatusCol].Value; }
            //                    if (GradeCol > 0) { Grade = sheet.Range[row, GradeCol].Value; }
            //                    if (IsKittedCol > 0) { isKitted = sheet.Range[row, IsKittedCol].Value; }
            //                    if (RefurbCol > 0) { Refurb = sheet.Range[row, RefurbCol].Value; }
            //                    if (CountryCol > 0) { Country = sheet.Range[row, CountryCol].Value; }

            //                    //if (TestCondition == true && ((Current.IFSSite.ToUpper() == "C1NA" && Condition.ToUpper() == "GEN") || (Current.IFSSite.ToUpper() != "C1NA" && Condition.ToUpper() != "GEN")))
            //                    //{
            //                    //    sheet.Range[row, StatusCol].Value = "Invalid IFS Conditions.";
            //                    //}
            //                    //else
            //                    //{
            //                    if (TestSKU == true && ctx.IsSkuValid(IMEI, Carrier, Manufacturer, Model, Colour) == false) { sheet.Range[row, StatusCol].Value = "Invalid SKU Combination."; }
            //                    else
            //                    {
            //                        int count = 0;
            //                        //for (int col = 2; col < 1000000; col++)
            //                        //{
            //                        if (sheet.Range[row, col].Value == null || sheet.Range[row, col].Value.Trim().Length == 0) { break; }
            //                        AttributeName = sheet.Range[1, col].Value.Trim();
            //                        AttributeValue = sheet.Range[row, col].Value.Trim();
            //                        if (AttributeValue.Trim().Length > 0 && IMEI.Length > 0 && AttributeName.Length > 0)
            //                        {
            //                            count = count + 1;
            //                            if (AttributeValue.ToUpper() == "BLANK") { AttributeValue = ""; }
            //                            AttributeName = AttributeName.Replace('_', ' ');
            //                            //ctx.Utility_LoadIMEIAttributeValue(IMEI, AttributeName, AttributeValue);
            //                            //if (Questions.Any(x => x.Name.ToUpper() == AttributeName.ToUpper()) == true) { ctx.UpdateESNAttribute(IMEI, AttributeName, AttributeValue, User.Identity.Name); }
            //                            if (Questions.Any(x => x.Name.ToUpper() == AttributeName.ToUpper()) == true)
            //                            {
            //                                aList.Add(AttributeName, AttributeValue, Current.ReceiveDetailID);
            //                                //if (TestSKU == true || TestCondition == true) { Original.Add(Current.Clone()); }  // If we are dealing with a SKU or Condition Change, then we need to tell IFS.
            //                                //rm.UpdateESNAttribute(ctx, rd.ReceiveDetailID, AttributeName, AttributeValue); 
            //                            }
            //                            else { ErrorMessage = ErrorMessage + ";" + AttributeName + "(Invalid)"; }
            //                        }
            //                        //}
            //                        sheet.Range[row, StatusCol].Value = ErrorMessage + "; Attribute: " + AttributeName + " Set (" + AttributeValue + ")";
            //                        logu.Save(Current.ReceiveDetailID, "Attribute: " + AttributeName + " Set (" + AttributeValue + ")");
            //                        log.LogIt("Row:" + row.ToString() + " Attribute: " + AttributeName + " Set (" + AttributeValue + ")");
            //                    }
            //                    //}
            //                }
            //            }
            //            #endregion
            //            // Time to update the database
            //            string cmdString = "";
            //            foreach (AttributeIMEIList a in aList.AttributeList)
            //            {
            //                cmdString = string.Format("UpdateESNAttribute_NoProjectRestriction_BYID {0}, '{1}', '{2}', '{3}'", a.TopReceiveDetailID(), a.Attribute, a.Value, User.Identity.Name);
            //                ctx.ExecuteCommand(cmdString);
            //                //ctx.ExecuteCommand("UpdateESNAttribute_ByID_InBulk {0}, {1}, {2}, {3}", a.ReceiveDetailIDListString, a.Attribute, a.Value, User.Identity.Name);
            //                log.LogIt("UpdateESNAttribute_NoProjectRestriction_BYID: #Rows:" + a.ReceiveDetailIDList.Count().ToString() + " Attribute: " + a.Attribute + " Set (" + a.Value + ")");
            //            }
            //            //foreach (IFSKeyValues a in Original)
            //            //{
            //            //    Current = new IFSKeyValues(a.ReceiveDetailID);
            //            //    // we need to get the current values, the updates may have changed something.
            //            //    if (Current.isIFSKeysEqual(a) == false)
            //            //    {
            //            //        ctx.IFS_GenerateInvtTran_B(a.ReceiveDetailID, DirectiveNormalMove, a.IFSSKU, a.IFSLocation, a.IFSCondition, Current.IFSSKU, Current.IFSLocation, Current.IFSCondition, User.Identity.Name, -1, "");
            //            //    }
            //            //}
            //        }

            //    }
            //    catch (Exception ex)
            //    {
            //        sheet.Range[2, 4].Value = "Error:" + ex.Message;
            //        log.LogIt("Error(a):" + ex.Message);
            //    }
            //    finally
            //    {
            //        try
            //        {
            //            workbook.Save();
            //            //workbook.SaveAs(strFileName + strFileType, Page.Response, ExcelDownloadType.Open);
            //            //workbook.SaveAs("SingleAttributeUpload.xls", Page.Response, ExcelDownloadType.Open);
            //            workbook.Close();
            //            excelEngine.Dispose();
            //            log.LogIt("Workbood Closed");
            //            SendFileToBrowser send = new SendFileToBrowser();
            //            send.StreamFileToBrowser(OriginalName + "_Uploaded" + strFileType, NameAndPath);
            //            log.LogIt("Sent file to Browser");
            //            lblMessageSingleIMEIAttribute.Text = "File Loaded.";
            //            lblMessageSingleIMEIAttribute.ForeColor = System.Drawing.Color.Red;
            //            lblMessageSingleIMEIAttribute.Visible = true;
            //            btnUploadSingleIMEIAttribute.Enabled = true;
            //        }
            //        catch (Exception ex)
            //        {
            //            ScriptManager.RegisterStartupScript(this, GetType(), "LoadClient", "alert(Error:" + ex.Message + ");", true);
            //            log.LogIt("Error(b):" + ex.Message);
            //        }
            //    }
            //}
            //else
            //{
            //    lblMessageSingleIMEIAttribute.Text = "Please select an excel file first.";
            //    lblMessageSingleIMEIAttribute.ForeColor = System.Drawing.Color.Red;
            //    lblMessageSingleIMEIAttribute.Visible = true;
            //}
            //btnUploadSingleIMEIAttribute.Enabled = true;
            log.LogIt("**** btnUploadSingleIMEIAttribute -- Finished");
        }






        protected void btnUploadAdjustOutData_Click(object sender, EventArgs e)
        {
            log.LogIt("**** btnUploadAdjustOutData_Click -- Started");
            if ((FileUploadAdjustOut.HasFile))
            {
                string strFileName = DateTime.Now.ToString("ddMMyyyy_HHmmss");
                string strFileType = System.IO.Path.GetExtension(FileUploadAdjustOut.FileName).ToString().ToLower();
                string OriginalName = System.IO.Path.GetFileNameWithoutExtension(FileUploadAdjustOut.FileName).ToString().ToLower();
                string NameAndPath = Server.MapPath("~/UploadedExcel/" + strFileName + strFileType);
                //Check file type
                if (strFileType == ".xls" || strFileType == ".xlsx")
                {
                    FileUploadAdjustOut.SaveAs(Server.MapPath("~/UploadedExcel/" + strFileName + strFileType));
                }
                else
                {
                    lblMessageAdjustOut.Text = "Only excel files allowed";
                    lblMessageAdjustOut.ForeColor = System.Drawing.Color.Red;
                    lblMessageAdjustOut.Visible = true;
                    lblMessageAdjustOut.Enabled = true;
                    return;
                }
                string strNewPath = Server.MapPath("~/UploadedExcel/" + strFileName + strFileType);

                //Step 1 : Instantiate the spreadsheet creation engine.
                ExcelEngine excelEngine = new ExcelEngine();
                //Step 2 : Instantiate the excel application object.
                IApplication application = excelEngine.Excel;
                //Open an existing spreadsheet which will be used as a template for generating the new spreadsheet.
                //After opening, the workbook object represents the complete in-memory object model of the template spreadsheet.
                IWorkbook workbook = application.Workbooks.Open(strNewPath, ExcelOpenType.Automatic);
                //The first worksheet object in the worksheets collection is accessed.
                IWorksheet sheet = workbook.Worksheets[0];

                string IMEI = "";
                string ReasonCode = "";
                string ReasonMessage = "";
                string rMessage = "";
                string ErrorMessage = "";
                //decimal ReceiveDetailID = -1;
                bool rValue = false;
                int StatusCol = 4;
                try
                {
                    using (clsLinqDataContext ctx = new clsLinqDataContext())
                    {

                        ReceiveDetailUtilityMoveLogManager logu = new ReceiveDetailUtilityMoveLogManager("Upload IMEI Adjust Out", User.Identity.Name);
                        ReceiveDetailManager rm = new ReceiveDetailManager(User.Identity.Name);
                        #region Travers all the rows
                        for (int row = 2; row < 1000000; row++)
                        {
                            if (sheet.Range[row, 1].Value == null || sheet.Range[row, 1].Value.Length == 0) { break; }
                            IMEI = sheet.Range[row, 1].Value;
                            ReasonCode = sheet.Range[row, 2].Value;
                            ReasonMessage = sheet.Range[row, 3].Value;

                            ErrorMessage = "";
                            ReceiveDetail rd = ctx.ReceiveDetails.FirstOrDefault(x => x.ESN == IMEI && x.Version == "000");
                            if (rd == null)
                            {
                                sheet.Range[row, StatusCol].Value = "IMEI Not found";
                            }
                            else
                            {
                                rMessage = "";
                                rValue = rm.IFSMOVEShippedInv_Adj_out(ctx, rd, ReasonCode, ReasonMessage, ref rMessage);
                                log.LogIt("IFSMOVEShipped:" + rd.ESN + ":" + ReasonCode + ":" + ReasonMessage + ":RM:" + rMessage);
                                if (rValue == true)
                                {
                                    ErrorMessage = "Success";
                                }
                                if (rValue == false)
                                {
                                    ErrorMessage += (ErrorMessage.Length > 0 ? "/" : "") + rMessage;
                                }
                                sheet.Range[row, StatusCol].Value = ErrorMessage;
                            }
                        }
                        #endregion
                        //// Time to update the database
                        //foreach (AttributeIMEIList a in aList.AttributeList)
                        //{
                        //    ctx.ExecuteCommand("UpdateESNAttribute_ByID_InBulk {0}, {1}, {2}, {3}", a.ReceiveDetailIDListString, a.Attribute, a.Value, User.Identity.Name);
                        //    log.LogIt("UpdateESNAttribute_ByID_InBulk: #Rows:" + a.ReceiveDetailIDList.Count().ToString() + " Attribute: " + a.Attribute + " Set (" + a.Value + ")");
                        //}
                    }

                }
                catch (Exception ex)
                {
                    sheet.Range[2, 5].Value = "Error:" + ex.Message;
                    log.LogIt("Error(a):" + ex.Message);
                }
                finally
                {
                    try
                    {
                        workbook.Save();
                        //workbook.SaveAs(strFileName + strFileType, Page.Response, ExcelDownloadType.Open);
                        //workbook.SaveAs("SingleAttributeUpload.xls", Page.Response, ExcelDownloadType.Open);
                        workbook.Close();
                        excelEngine.Dispose();
                        log.LogIt("Workbood Closed");
                        SendFileToBrowser send = new SendFileToBrowser();
                        send.StreamFileToBrowser(OriginalName + "_Uploaded" + strFileType, NameAndPath);
                        log.LogIt("Sent file to Browser");
                        lblMessageAdjustOut.Text = "File Loaded.";
                        lblMessageAdjustOut.ForeColor = System.Drawing.Color.Red;
                        lblMessageAdjustOut.Visible = true;
                        btnUploadAdjustOutData.Enabled = true;
                    }
                    catch (Exception ex)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "LoadClient", "alert(Error:" + ex.Message + ");", true);
                        log.LogIt("Error(b):" + ex.Message);
                    }
                }
            }
            else
            {
                lblMessageAdjustOut.Text = "Please select an excel file first.";
                lblMessageAdjustOut.ForeColor = System.Drawing.Color.Red;
                lblMessageAdjustOut.Visible = true;
            }
            btnUploadAdjustOutData.Enabled = true;
            log.LogIt("**** btnUploadAdjustOutData_Click -- Finished");
        }



        [Serializable()]
        public class AttributeIMEIList
        {
            List<decimal> _ReceiveDetailIDList = new List<decimal>();
            public string Attribute { get; set; }
            public string Value { get; set; }
            public List<decimal> ReceiveDetailIDList { get { return _ReceiveDetailIDList; } }

            public string ReceiveDetailIDListString
            {
                get
                {
                    string idlist = "";
                    foreach (decimal id in ReceiveDetailIDList)
                    {
                        if (idlist.Length > 0) { idlist += ","; }
                        idlist += id.ToString();
                    }
                    return idlist;
                }
            }


            public decimal TopReceiveDetailID()
            {
                foreach (decimal id in ReceiveDetailIDList)
                {
                    return id;
                }
                return -1;
            }


            public AttributeIMEIList(string attribute, string value)
            {
                Attribute = attribute;
                Value = value;
            }

            public void Add(decimal ReceiveDetailID)
            {
                bool isThere = false;
                foreach (decimal x in _ReceiveDetailIDList)
                {
                    if (x == ReceiveDetailID) { isThere = true; }
                }
                if (isThere == false) { _ReceiveDetailIDList.Add(ReceiveDetailID); }
            }
        }

        [Serializable()]
        public class AttributeValueList
        {
            List<AttributeIMEIList> _AttributeValueList = new List<AttributeIMEIList>();
            public List<AttributeIMEIList> AttributeList { get { return _AttributeValueList; } }

            public AttributeValueList()
            {
            }
            public AttributeValueList(string attribute, string value, decimal ReceiveDetailID)
            {
                Add(attribute, value, ReceiveDetailID);
            }

            public void Add(string attribute, string value, decimal ReceiveDetailID)
            {
                AttributeIMEIList obj = _AttributeValueList.FirstOrDefault(x => x.Attribute.ToUpper() == attribute.ToUpper() && x.Value.ToUpper() == value);
                if (obj == null)
                {
                    obj = new AttributeIMEIList(attribute, value);
                    _AttributeValueList.Add(obj);
                }
                obj.Add(ReceiveDetailID);
            }
        }


        public class SendFileToBrowser
        {

            #region Browser Streaming

            public SendFileToBrowser()
            {
            }

            public SendFileToBrowser(string FileName, string NameAndPath)
            {
                StreamFileToBrowser(FileName, NameAndPath);
            }

            public void StreamFileToBrowser(string sFileName, string NameAndPath)
            {
                using (FileStream file = new FileStream(NameAndPath, FileMode.Open, FileAccess.Read))
                {
                    byte[] bytes = new byte[file.Length];
                    file.Read(bytes, 0, (int)file.Length);
                    file.Flush();
                    file.Close();
                    StreamFileToBrowser(sFileName, bytes);
                    //if (File.Exists(PDFName) == true) { File.Delete(PDFName); }
                    return;
                }
            }
            protected void StreamFileToBrowser(string fileName, byte[] fileBytes)
            {
                //HttpResponse Response = HttpContext.Current.Response;
                //Response.Clear();
                //Response.ClearHeaders();
                //Response.ClearContent();
                //Response.AppendHeader("content-length", fileBytes.Length.ToString());
                //Response.ContentType = GetMimeTypeByFileName(fileName);
                //Response.AppendHeader("content-disposition", "attachment; filename=\"" + ContentDispositionEncode(HttpContext.Current.Request.Browser, fileName));          //fileName + "\"");
                //Response.BinaryWrite(fileBytes);
                //Response.End();

                //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";  //"application/vnd.ms-excel";
                //// 08/06/2008 yxy21969.  Make sure to encode all URLs. 
                //// 12/20/2009 Paul.  Use our own encoding so that a space does not get converted to a +. 
                //Response.AddHeader("Content-Disposition", "attachment;filename=" + Utils.ContentDispositionEncode(HttpContext.Current.Request.Browser, sModuleName + ".xlsx"));
                //ExportExcelOpenXML(Response.OutputStream, vw, sModuleName.ToLower(), nStartRecord, nEndRecord);
                //Response.End();

                System.Web.HttpContext context = System.Web.HttpContext.Current;
                context.Response.Clear();
                context.Response.ClearHeaders();
                context.Response.ClearContent();
                context.Response.AppendHeader("content-length", fileBytes.Length.ToString());
                context.Response.ContentType = GetMimeTypeByFileName(fileName);
                context.Response.AppendHeader("content-disposition", "attachment; filename=\"" + fileName + "\"");
                context.Response.BinaryWrite(fileBytes);

                // use this instead of response.end to avoid thread aborted exception (known issue):
                // http://support.microsoft.com/kb/312629/EN-US
                context.ApplicationInstance.CompleteRequest();

            }
            public string GetMimeTypeByFileName(string sFileName)
            {
                string sMime = "application/octet-stream";

                string sExtension = System.IO.Path.GetExtension(sFileName);
                if (!string.IsNullOrEmpty(sExtension))
                {
                    sExtension = sExtension.Replace(".", "");
                    sExtension = sExtension.ToLower();

                    if (sExtension == "xls" || sExtension == "xlsx")
                    {
                        sMime = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        //sMime = "application/ms-excel";
                    }
                    else if (sExtension == "doc" || sExtension == "docx")
                    {
                        sMime = "application/msword";
                    }
                    else if (sExtension == "ppt" || sExtension == "pptx")
                    {
                        sMime = "application/ms-powerpoint";
                    }
                    else if (sExtension == "rtf")
                    {
                        sMime = "application/rtf";
                    }
                    else if (sExtension == "zip")
                    {
                        sMime = "application/zip";
                    }
                    else if (sExtension == "mp3")
                    {
                        sMime = "audio/mpeg";
                    }
                    else if (sExtension == "bmp")
                    {
                        sMime = "image/bmp";
                    }
                    else if (sExtension == "gif")
                    {
                        sMime = "image/gif";
                    }
                    else if (sExtension == "jpg" || sExtension == "jpeg")
                    {
                        sMime = "image/jpeg";
                    }
                    else if (sExtension == "png")
                    {
                        sMime = "image/png";
                    }
                    else if (sExtension == "tiff" || sExtension == "tif")
                    {
                        sMime = "image/tiff";
                    }
                    else if (sExtension == "txt")
                    {
                        sMime = "text/plain";
                    }
                }

                return sMime;
            }
            public static string ContentDispositionEncode(HttpBrowserCapabilities Browser, string sURL)
            {
                // 01/27/2011 Paul.  Don't use GetFileName as the name may contain reserved directory characters, but expect them to be removed in Utils.ContentDispositionEncode. 
                sURL = sURL.Replace('\\', '_');
                sURL = sURL.Replace(':', '_');
                // 12/20/2009 Paul.  Make sure that the URL is not null. 
                sURL = ToString(sURL);
                if (Browser != null)
                {
                    if (Browser.Browser == "IE")
                    {
                        sURL = HttpUtility.UrlPathEncode(sURL);
                    }
                }
                sURL = "\"" + sURL + "\"";
                return sURL;
            }
            public static string ToString(string str)
            {
                if (str == null)
                    return String.Empty;
                return str;
            }
            #endregion

        }



        #region SPecial Stuff for generating the report download because of Corruption problem with return of excel.
        #region ExportToExcel
        private string ExportToExcel(string cmd, string fileName)
        {
            List<string> fieldListToReport = new List<string>();
            return ExportToExcel(cmd, fileName, fieldListToReport);
        }
        private string ExportToExcel(string cmd, string fileName, string FieldListToReport)
        {
            List<string> fieldListToReport = new List<string>();
            if (FieldListToReport.Length > 0)
            {
                string[] fList = FieldListToReport.Split(',');
                fieldListToReport = new List<string>(fList);
            }
            return ExportToExcel(cmd, fileName, fieldListToReport);
        }
        private string ExportToExcel(string scmd, string fileName, List<string> fieldListToReport)
        {
            string message = "";
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand();
            scmd = scmd.Replace('~', '\'');
            cmd.CommandText = scmd;
            cmd.CommandTimeout = 360;
            cmd.Connection = cn;

            // if csv 
            if (chkCSV.Checked == true)
            {
                message = ExportCSVFile(cn, cmd, fileName, fieldListToReport);
            }
            else // or xls file
            {
                message = ExportXLSFile(cn, cmd, fileName, fieldListToReport);
            }
            //
            return message;
        }

        #endregion
        private string ExportXLSFile(SqlConnection cn, SqlCommand cmd, string fileName, List<string> fieldListToReport)
        {
            string message = "";
            //string[] formatnumeric = {"TRANSACTIONQTY","TRANSACTIONUNITPRICE","QUANTITY","UNITPRICE","MONTHENDQTY", "MOVEDOUT","MOVEDIN","STARTING","ENDING","FREQIN","FREQOUT",
            //                          "REPAIR FEE", "REPAIR_FEE", "UNITPRICE01", "UNITPRICE02", "UNITPRICE03", "UNITPRICE04", "UNITPRICE05", "UNITPRICE06", "UNITPRICE07",
            //                          "UNITPRICE08", "UNITPRICE09", "UNITPRICE10", "TOTALUNITPRICE","REPAIR CAP", "SAVETIMEMS", "DEVICECOST",
            //                          "INPROCESSSECONDS","INPROCESSMINUTES","INPROCESSHOURS","MINUTESTOYELLOW","MINUTESTORED"};
            //List<int> formatnumericColumns = new List<int>();
            //string[] formatDate = { "LASTUPDATEDATE", "MONTHENDDATE", "CREATEDATE", "RECEIVEDATE", "MOVEDDATE", "REPORTBEGINDATE", 
            //                          "REPORTENDDATE", "DATEMOVED", "DATEMOVEDOUT", "BININDATE", "BINOUTDATE", "ATTEMPTDATE", "ATTEMPTDATE2", 
            //                          "ATTEMPTDATE3", "ATTEMPTDATED", "ATTEMPTDATE2D", "ATTEMPTDATE3D", "LASTUPDATEDATED", "MONTHENDDATED", 
            //                          "STARTTIMEDATE", "ENDTIMEDATE", 
            //                          "CREATEDATED" };
            //List<int> formatDateColumns = new List<int>();
            ReportUtility ru = new ReportUtility();
            string[] formatnumeric = ru.ListALLNumericQuestionNames().ToArray();
            string[] formatDate = ru.ListDateQuestionNames().ToArray();
            List<int> formatnumericColumns = new List<int>();
            List<int> formatDateColumns = new List<int>();

            List<int> o = new List<int>();
            try
            {
                AccessControlIDList ValidClientLocationIDs = null;
                AccessControlIDList ValidProjectIDs = null;
                using (clsLinqDataContext ctx = new clsLinqDataContext())
                {
                    ValidClientLocationIDs = new AccessControlIDList(User.Identity.Name, "Client", ctx);
                    ValidProjectIDs = new AccessControlIDList(User.Identity.Name, "Project", ctx);
                }
                message = "start:" + DateTime.Now.ToString() + Environment.NewLine;


                cn.Open();

                // See if you can insert the CSV portion here if the csv checkbox is checked,
                //     Search CSV and go to ExportCSVFile to find the correct code to spit out csv records.

                //
                // otherwise send out the normal excel.

                int ClientLocationColumn = -1;
                int ProjectColumn = -1;
                message += "dbase opened:" + DateTime.Now.ToString() + Environment.NewLine;
                SqlDataReader dr = cmd.ExecuteReader();
                message += "data read:" + DateTime.Now.ToString() + Environment.NewLine;
                string fieldName = "";
                for (int count = 0; count < dr.FieldCount; count++)
                {
                    fieldName = dr.GetName(count).ToUpper();
                    if (formatnumeric.Contains(fieldName.ToUpper()) == true) { formatnumericColumns.Add(count); }
                    if (formatDate.Contains(fieldName.ToUpper()) == true) { formatDateColumns.Add(count); }

                    if (fieldName == "CLIENTLOCATIONID") { ClientLocationColumn = count; }
                    if (fieldName == "PROJECTID") { ProjectColumn = count; }
                    if (fieldListToReport.Count == 0)
                    {
                        //string ValueTypex = dr.GetFieldType(count).Name;
                        o.Add(count);
                    }
                    else
                    {
                        if (fieldListToReport.Contains(dr.GetName(count)))
                        {
                            //string ValueTypex = dr.GetFieldType(count).ToString();
                            o.Add(count);
                        }
                    }
                }


                ExcelEngine excelEngine = new ExcelEngine();
                IApplication application = excelEngine.Excel;
                application.DefaultVersion = GetExcelVersion();
                fileName = fileName.Replace(".xls", "");

                fileName = fileName + "." + GetExcelExtension();
                IWorkbook workbook = application.Workbooks.Create(1);
                //                workbook.Version = GetExcelVersion();
                IWorksheet sheet = workbook.Worksheets[0];
                int Row = 1;
                int Col = 1;
                int StartCol = Col;
                int StartRow = Row;
                lblMessage.Text = "";
                //Add Header    
                foreach (int count in o)
                {
                    if (dr.GetName(count) != null)
                    {
                        sheet.Range[Row, Col].Text = dr.GetName(count);
                    }
                    ++Col;
                }

                string Value = "";
                double nValue = 0;
                DateTime dValue = DateTime.Now;
                while (dr.Read())
                {
                    Col = 1;
                    if ((ClientLocationColumn == -1 || ValidClientLocationIDs.GlobalSelect == true || ValidClientLocationIDs.IDs.Contains(dr.GetDecimal(ClientLocationColumn))) &&
                        (ProjectColumn == -1 || ValidProjectIDs.GlobalSelect == true || ValidProjectIDs.IDs.Contains(dr.GetDecimal(ProjectColumn))))
                    {
                        ++Row;
                        foreach (int count in o)
                        {
                            if (!dr.IsDBNull(count) && dr.GetValue(count).ToString().Length > 0)
                            //if (!dr.IsDBNull(count))
                            {
                                Value = dr.GetValue(count).ToString();
                                if (formatnumericColumns.Contains(count) && double.TryParse(Value, out nValue) == true)
                                {
                                    // format numeric.
                                    sheet.Range[Row, Col].Number = nValue;
                                    sheet.Range[Row, Col].NumberFormat = "@";
                                }

                                else if (formatDateColumns.Contains(count) && DateTime.TryParse(Value, out dValue) == true)
                                {
                                    // format numeric.
                                    sheet.Range[Row, Col].DateTime = dValue;
                                    //sheet.Range[Row, Col].NumberFormat = "@";
                                }
                                else
                                {
                                    sheet.Range[Row, Col].Text = Value;
                                }
                            }


                            ++Col;
                        }
                    }
                }
                //workbook.SaveAs(fileName, Page.Response, ExcelDownloadType.Open);
                workbook.SaveAs(fileName, ExcelSaveType.SaveAsXLS, Response, ExcelDownloadType.Open);
                workbook.Close();

                excelEngine.Dispose();

                // move the file out to the browser.
            }
            catch (Exception ex)
            {
                message += "----------------------------------------" + Environment.NewLine;
                message += "Error:" + DateTime.Now.ToString() + Environment.NewLine;
                message += ex.Message;
                lblMessage.Text = message;
            }
            finally
            {
                cmd.Connection.Close();
                cn.Close();

            }
            return message;
        }
        private string ExportCSVFile(SqlConnection cn, SqlCommand cmd, string fileName, List<string> fieldListToReport)
        {
            //Add Response header 
            //const string fileName = "AddData";
            List<int> o = new List<int>();
            Response.Clear();
            Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.csv", fileName));
            Response.Charset = "";
            Response.ContentType = "application/vnd.xls";
            string message = "";
            try
            {
                // "START HERE" 
                cn.Open();
                message += "dbase opened:" + DateTime.Now.ToString() + Environment.NewLine;
                SqlDataReader dr = cmd.ExecuteReader();
                message += "data read:" + DateTime.Now.ToString() + Environment.NewLine;
                for (int count = 0; count < dr.FieldCount; count++)
                {
                    if (fieldListToReport.Count == 0)
                    {
                        o.Add(count);
                    }
                    else
                    {
                        if (fieldListToReport.Contains(dr.GetName(count)))
                        {
                            o.Add(count);
                        }
                    }
                }
                var sb = new StringBuilder();
                //Add Header    
                foreach (int count in o)
                {
                    if (dr.GetName(count) != null)
                    {
                        sb.Append(dr.GetName(count));
                    }
                    if (count != o[o.Count - 1])
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

                    foreach (int col in o)
                    {
                        if (!dr.IsDBNull(col))
                        {
                            sb.Append(dr.GetValue(col).ToString().Replace(",", " "));
                        }
                        if (col != o[o.Count - 1])
                        {
                            sb.Append(",");
                        }
                    }
                    Response.Write(sb.ToString() + "\n");
                    Response.Flush();
                }
                // dr.Dispose();
            }
            catch (Exception ex)
            {
                message += "----------------------------------------" + Environment.NewLine;
                message += "Error:" + DateTime.Now.ToString() + Environment.NewLine;
                message += ex.Message;
                Response.Write(ex.Message);
            }
            finally
            {
                cmd.Connection.Close();
                cn.Close();
            }
            Response.End();
            return message;
        }
        Syncfusion.XlsIO.ExcelVersion GetExcelVersion()
        {

            //string v = drpXlsFormat.SelectedItem.Text;
            string v = "Excel2007";
            if (v == "Excel2007") { return ExcelVersion.Excel2007; }
            if (v == "Excel2010") { return ExcelVersion.Excel2010; }
            if (v == "Excel97to2003") { return ExcelVersion.Excel97to2003; }
            return ExcelVersion.Excel2007;
        }
        string GetExcelExtension()
        {
            //string v = drpXlsFormat.SelectedItem.Text;
            string v = "Excel2007";
            if (v == "Excel2007") { return "XLSX"; }
            if (v == "Excel2010") { return "XLSX"; }
            if (v == "Excel97to2003") { return "XLS"; }
            return "XLS";
        }

        #endregion
    }
}