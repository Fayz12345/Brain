using System;
using System.Collections.Generic;
//using BusinessLayer;
// using DAL;

using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
//using SoftArtisans.OfficeWriter.ExcelWriter;
using System.Text;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
//using Factory_Businesslayer;
//using Factory_DataModel.Classes;
using BW_WebApp.DataManagers;
using Syncfusion.XlsIO;

namespace BW_WebApp.Reports
{
    public partial class ReportExternal : System.Web.UI.Page
    {


        clsLog log;

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
            TabTemplate.Visible = false;
            if (User.IsInRole("Administrators") || User.IsInRole("Supervisors"))
            {
                TabTemplate.Visible = true;
            }
            log = new clsLog(Server.MapPath("~"), "Reporting_Log.txt", User.Identity.Name);
            //log = new clsLog(Server.MapPath("~"), "Reporting_Log.txt", User.Identity.Name, System.Configuration.ConfigurationManager.AppSettings["WriteLogUserNameToLog"]);
            log.writeLogData = false;
            //ReadOnlyCollection<TimeZoneInfo> tzi;
            //tzi = TimeZoneInfo.GetSystemTimeZones();
            //int maxLength = 0;
            //foreach (TimeZoneInfo x in tzi)
            //{
            //    if (x.Id.Length > maxLength) { maxLength = x.Id.Length; }
            //}
            lblMessage.Text = "";
            btnRMASummary.Click += new EventHandler(btnRMASummary_Click);
            MainGrid.RowDataBound += new GridViewRowEventHandler(MainGrid_RowDataBound);
            MainGrid.RowCommand += new GridViewCommandEventHandler(MainGrid_RowCommand);
            btnRefresh.Click += new EventHandler(btnRefresh_Click);


            btnDownLoadPricing.Click += new EventHandler(btnDownLoadPricing_Click);

            btnDetailInventory25.Click += new EventHandler(btnDetailInventory25_Click);
            btnReportByBin.Click += new EventHandler(btnReportByBin_Click);
            btnReportXBINXKey.Click += new EventHandler(btnReportXBINXKey_Click);
            btnDeviceSKUAnalyze.Click += new EventHandler(btnDeviceSKUAnalyze_Click);
            btnPartSKUAnalyze.Click += new EventHandler(btnPartSKUAnalyze_Click);

            btnDeviceSKUAnalyzeeDetail.Click += new EventHandler(btnDeviceSKUAnalyzeeDetail_Click);
            btnPartSKUAnalyzeeDetail.Click += new EventHandler(btnPartSKUAnalyzeeDetail_Click);


            btnPartLocAnalyze.Click += new EventHandler(btnPartLocAnalyze_Click);
            btnPartLocAnalyzeDetail.Click += new EventHandler(btnPartLocAnalyzeDetail_Click);
            btnDeviceLocAnalyzeDetail.Click += new EventHandler(btnDeviceLocAnalyzeDetail_Click);


            btnDeviceLocAnalyze.Click += new EventHandler(btnDeviceLocAnalyze_Click);
            btnGetIFS_GatherLogSummary.Click += new EventHandler(btnGetIFS_GatherLogSummary_Click);
            btnOrderEntryData.Click += new EventHandler(btnOrderEntryData_Click);
            btnOrderEntryDetailData.Click += new EventHandler(btnOrderEntryDetailData_Click);
            btnPurchaseOrderData.Click += new EventHandler(btnPurchaseOrderData_Click);
            btnPurchaseOrderDetailData.Click += new EventHandler(btnPurchaseOrderDetailData_Click);
            btnOrderEntryDetailDataIFS.Click += new EventHandler(btnOrderEntryDetailDataIFS_Click);
            btnPurchaseOrderDetailDataIFS.Click += new EventHandler(btnPurchaseOrderDetailDataIFS_Click);
            btnIFSBatchDetailDataIFS.Click += new EventHandler(btnIFSBatchDetailDataIFS_Click);

            btnPasteParseIMEIList.Click += new EventHandler(btnPasteParseIMEIList_Click);

            btnPasteParse.Click += new EventHandler(btnPasteParse_Click);

            btnDeviceSummaryIFS.Click += new EventHandler(btnDeviceSummaryIFS_Click);
            btnPartSummaryIFS.Click += new EventHandler(btnPartSummaryIFS_Click);


            btnExtractSamsung.Click += new EventHandler(btnExtractSamsung_Click);
            btnDeviceErrorReport.Click += new EventHandler(btnDeviceErrorReport_Click);
            btnErrorReporting.Click += new EventHandler(btnErrorReporting_Click);
            btnExportScanCodeList.Click += new EventHandler(btnExportDataFile_Click);
            btnExportQuestionList.Click += new EventHandler(btnExportDataFile_Click);
            btnExportProcessList.Click += new EventHandler(btnExportDataFile_Click);
            btnExportProjectDefinition.Click += new EventHandler(btnExportProjectDefinition_Click);
            btnExportPartNumberInventory.Click += new EventHandler(btnExportPartNumberInventory_Click);
            btnDiscrepancyReport.Click += new EventHandler(btnDiscrepancyReport_Click);
            btnBilling.Click += new EventHandler(btnBilling_Click);
            btnStatistical.Click += new EventHandler(btnExportStatisticalList_Click);
            btnStatisticalRaw.Click += new EventHandler(btnExportStatisticalRawList_Click);
            btnStatisticalBucket.Click += new EventHandler(btnExportStatisticalBucketList_Click);
            btnStatisticalRawBucket.Click += new EventHandler(btnExportStatisticalRawBucketList_Click);
            btnStatisticalBucketDaily.Click += new EventHandler(btnStatisticalBucketDaily_Click);
            btnBucketCounters.Click += new EventHandler(btnBucketCounters_Click);
            btnMasterCarrier.Click += new EventHandler(btnMasterCarrier_Click);
            btnMasterCarrierFreq.Click += new EventHandler(btnMasterCarrierFreq_Click);
            btnAttributeHistory.Click += new EventHandler(btnAttributeHistory_Click);
            btnClientQuestionFreq.Click += new EventHandler(btnClientQuestionFreq_Click);
            btnTAT.Click += new EventHandler(btnTAT_Click);
            //btnGenerateBilling.Click += new EventHandler(btnGenerateBilling_Click);
            //btnDownloadBilling.Click += new EventHandler(btnDownloadBilling_Click);

            btnProcessWaitTime.Click += new EventHandler(btnProcessWaitTime_Click);

            //btnBoxReport.Attributes.Add("OnClick", "OpenBoxReport(); return false;");
            btnBinTag.Attributes.Add("OnClick", "OpenBinTag(); return false;");

            btnPartsInventoryTransaction.Click += new EventHandler(btnPartsInventoryTransaction_Click);
            btnUserFrequency.Click += new EventHandler(btnUserFrequency_Click);
            btnUserFrequency2.Click += new EventHandler(btnUserFrequency2_Click);





            //btnExportDetailInventoryList.Click += new EventHandler(btnExportFullInventoryList_Click);


            btnExportDetailInventoryList.Click += new EventHandler(btnExportFullInventoryList_B_Click);





            btnAvailableStock.Click += new EventHandler(btnAvailableStock_Click);
            btnAvailableStockLocation.Click += new EventHandler(btnAvailableStockLocation_Click);
            btnPreReceive.Click += new EventHandler(btnPreReceive_Click);



            //btnLocationUnitReport.Click += new EventHandler(btnLocationUnitReport_Click);
            //btnLocationUnitSummary.Click += new EventHandler(btnLocationUnitSummary_Click);
            //btnLocationReport.Click += new EventHandler(btnLocationReport_Click);


            // good         
            btnItemsInLocation.Click += new EventHandler(btnItemsInLocation_Click);
            btnLocationHistoryReport.Click += new EventHandler(btnLocationHistoryReport_Click);
            btnUnitLocationSummary.Click += new EventHandler(btnUnitLocationSummary_Click);





            btnExportDetailInventoryClientList.Click += new EventHandler(btnExportFullInventoryClientList_Click);
            btnExportDetailInventoryClientListBatched.Click += new EventHandler(btnExportDetailInventoryClientListBatched_Click);
            btnBinStorage.Click += new EventHandler(btnBinStorage_Click);
            btnBinStorageGrand.Click += new EventHandler(btnBinStorageGrand_Click);
            btnBinStorageSummary.Click += new EventHandler(btnBinStorageSummary_Click);
            btnBinStorageDetail.Click += new EventHandler(btnBinStorageDetail_Click);
            btnTest.Click += new EventHandler(btnTest_Click);
            //btnBinStorageGrandTest.Click += new EventHandler(btnBinStorageGrandTest_Click);


            btnRawData.Click += new EventHandler(btnRawData_Click);
            btnExportDetailInventoryClientListAbriviated.Click += new EventHandler(btnExportDetailInventoryClientListAbriviated_Click);
            btnExportDetailInventoryClientListAbriviatedB.Click += new EventHandler(btnExportDetailInventoryClientListAbriviatedB_Click);

            //btnExportDetailInventoryProcessingLog.Click += new EventHandler(btnExportDetailInventoryProcessingLog_Click);

            btnExportDockingReport.Click += new EventHandler(btnExportDockingReport_Click);


            btnProcessAverageTime.Click += new EventHandler(btnExportFullInventoryList_Click);
            btnOutFreq.Click += new EventHandler(btnExportFullInventoryList_Click);
            btnBulkQty.Click += new EventHandler(btnExportFullInventoryList_Click);
            btnQty.Click += new EventHandler(btnExportFullInventoryList_Click);


            grdTempDetail.RowDataBound += new GridViewRowEventHandler(grdTempDetail_RowDataBound);
            grdTempDetail.RowCommand += new GridViewCommandEventHandler(grdTempDetail_RowCommand);


            grdTempDetailClient.RowDataBound += new GridViewRowEventHandler(grdTempDetailClient_RowDataBound);
            grdTempDetailClient.RowCommand += new GridViewCommandEventHandler(grdTempDetailClient_RowCommand);

            grdTempBillingPoint.RowDataBound += new GridViewRowEventHandler(grdTempBillingPoint_RowDataBound);
            grdTempBillingPoint.RowCommand += new GridViewCommandEventHandler(grdTempBillingPoint_RowCommand);

            if (!IsPostBack)
            {
                Dashboard.Visible = false;
                //if (User.IsInRole("Administrators") == true || User.IsInRole("Supervisors") == true)
                //{
                //    //Dashboard.Visible = true;
                //}

                ClientManager cm = new ClientManager(User.Identity.Name);

                List<Client> cl = cm.SearchClientList("", "", "").OrderBy(x => x.CompanyName).ToList();
                drpClientList.Items.Clear();
                ListItem z = new ListItem("All", "-1");
                drpClientList.Items.Add(z);
                foreach (Client p in cl)
                {
                    ListItem x = new ListItem(p.CompanyName, p.ClientID.ToString());
                    drpClientList.Items.Add(x);
                }


                UserManager um = new UserManager(User.Identity.Name);
                var ul = um.MasterUserListAll();
                drpUserList.Items.Clear();
                z = new ListItem("All", "All");
                drpUserList.Items.Add(z);
                foreach (UserRecord p in ul)
                {
                    ListItem x = new ListItem(p.Name, p.UserName);
                    drpUserList.Items.Add(x);
                }
                drpUserList.SelectedIndex = 0;

                setupXLSDropdown();

                //drpClientList.DataValueField = "ClientID";
                //drpClientList.DataTextField = "CompanyName";
                //drpClientList.DataSource = cm.SearchClientList("", "", "").OrderBy(x => x.CompanyName);
                //drpClientList.DataBind();
                //drpClientList.SelectedIndex = 0;

                ProjectManager pm = new ProjectManager(User.Identity.Name);
                List<Project> pl = pm.GetProjectList();
                drpProjectList.Items.Clear();
                z = new ListItem("All", "-1");
                drpProjectList.Items.Add(z);
                foreach (Project p in pl)
                {
                    ListItem x = new ListItem(p.Name, p.ProjectID.ToString());
                    drpProjectList.Items.Add(x);
                }

                ReceiveDetailManager rdm = new ReceiveDetailManager(User.Identity.Name);
                List<ReceiveDetailStatus> rdl = rdm.GetReceiveDetailStatusList();
                drpStatus.Items.Clear();
                z = new ListItem("All", "-1");
                drpStatus.Items.Add(z);
                foreach (ReceiveDetailStatus o in rdl)
                {
                    ListItem x = new ListItem(o.Status, o.ReceiveDetailStatusID.ToString());
                    drpStatus.Items.Add(x);
                }

                QuestionManager qm = new QuestionManager(User.Identity.Name);
                List<Option> ol = qm.GetQuestionOptionList("Carrier");
                drpCarrier.Items.Clear();
                z = new ListItem("All", "-1");
                drpCarrier.Items.Add(z);
                foreach (Option o in ol)
                {
                    ListItem x = new ListItem(o.OptionText, o.OptionID.ToString());
                    drpCarrier.Items.Add(x);
                }

                ol = qm.GetQuestionOptionList("Manufacturer");
                drpManufacturer.Items.Clear();
                z = new ListItem("All", "-1");
                drpManufacturer.Items.Add(z);
                foreach (Option o in ol)
                {
                    ListItem x = new ListItem(o.OptionText, o.OptionID.ToString());
                    drpManufacturer.Items.Add(x);
                }

                ol = qm.GetQuestionOptionList("Model");
                drpModel.Items.Clear();
                z = new ListItem("All", "-1");
                drpModel.Items.Add(z);
                foreach (Option o in ol)
                {
                    ListItem x = new ListItem(o.OptionText, o.OptionID.ToString());
                    drpModel.Items.Add(x);
                }

                ol = qm.GetQuestionOptionList("Colour");
                drpColour.Items.Clear();
                z = new ListItem("All", "-1");
                drpColour.Items.Add(z);
                foreach (Option o in ol)
                {
                    ListItem x = new ListItem(o.OptionText, o.OptionID.ToString());
                    drpColour.Items.Add(x);
                }

                ol = qm.GetQuestionOptionList("Product Place");
                drpProductPlace.Items.Clear();
                z = new ListItem("All", "-1");
                drpProductPlace.Items.Add(z);
                foreach (Option o in ol)
                {
                    ListItem x = new ListItem(o.OptionText, o.OptionID.ToString());
                    drpProductPlace.Items.Add(x);
                }


                var loc = rdm.GetMasterLocationList();
                drpLocationList.Items.Clear();
                z = new ListItem("All", "-1");
                drpLocationList.Items.Add(z);
                foreach (var l in loc)
                {
                    ListItem x = new ListItem(l.Desc, l.ID.ToString());
                    drpLocationList.Items.Add(x);
                }
                //ReceiveDetailManager rd = new ReceiveDetailManager(User.Identity.Name);
                //drpLocationList.DataValueField = "ID";
                //drpLocationList.DataTextField = "Desc";
                //drpLocationList.DataSource = rd.GetMasterLocationList();
                //this.DataBind();

                txtPricingAsOfDate.Text = DateTime.Now.ToShortDateString();
                txtBeginDate.Text = DateTime.Now.AddDays(-7).ToShortDateString();
                txtEndDate.Text = DateTime.Now.ToShortDateString();

                txtLogDateBegin.Text = DateTime.Now.ToShortDateString();
                txtLogDateEnd.Text = DateTime.Now.ToShortDateString();

                txtBeginQC.Text = DateTime.Now.AddDays(-7).ToShortDateString();
                txtEndQC.Text = DateTime.Now.ToShortDateString();

                txtBeginQC.Text = DateTime.Now.AddDays(-7).ToShortDateString();
                txtEndQC.Text = DateTime.Now.ToShortDateString();

                txtBeginFunctionTest.Text = DateTime.Now.AddDays(-7).ToShortDateString();
                txtEndFunctionTest.Text = DateTime.Now.ToShortDateString();

                txtBeginShipped.Text = DateTime.Now.AddDays(-7).ToShortDateString();
                txtEndShipped.Text = DateTime.Now.ToShortDateString();
                //////UpdateTemplateGrid("~/templates/Detail", grdTempDetail);
                //////UpdateTemplateGrid("~/templates/DetailClient", grdTempDetailClient);
                ////////UpdateTemplateGrid("~/templates/DetailDate", grdTempDetailDate);
                ////////UpdateTemplateGrid("~/templates/Bulk", grdTempBulk);
                ////////UpdateTemplateGrid("~/templates/BSP", grdTempBSP);
                //////UpdateTemplateGrid("~/templates/BillingPoints", grdTempBillingPoint);

            }
        }

        void btnDownLoadPricing_Click(object sender, EventArgs e)
        {
            ExportDisposalToExcelxx();
        }



        #region
        void ExportDisposalToExcelxx()
        {
            string cnString = ConnectionString;
            string CommandText = "";
            int Row = 1;
            int Col = 1;
            try
            {
                string ReportUser = "Test";
                ReportUser = User.Identity.Name;

                //cmd.CommandText = "BSPSummarize_02_pg1 '" + User.Identity.Name + "',''";
                //cmd.CommandText = "BSPSummarize_02_pg1 'Test',''";

                //cmd.CommandTimeout = 120;
                //cmd.Connection = cn;
                //cn.Open();
                //SqlDataReader dr = cmd.ExecuteReader();


                ExcelEngine excelEngine = new ExcelEngine();
                IApplication application = excelEngine.Excel;
                IWorkbook workbook = application.Workbooks.Create(6);
                IWorksheet sheet = workbook.Worksheets[0];
                ////IWorksheet TimeDetail = workbook.Worksheets[6];
                //BSPManager bspm = new BSPManager(User.Identity.Name);
                //BSPRawData_02 criteria = bspm.GetReportParameters();
                string FromTo = "Unknown";
                //if (criteria != null)
                //{
                //    FromTo = criteria.Filter_ReceiveBeginDate + " to " + criteria.Filter_ReceiveEndDate;
                //}

                string ReportName = "Summary";
                string ReportTitle = "Bell Project - Summary as of " + FromTo;
                FillSheetDataxx(cnString, ref CommandText, ref Row, ref Col, ReportUser, sheet, ReportName, ReportTitle);

                // Green Sheet
                sheet = workbook.Worksheets[1];
                ReportName = "Green";
                ReportTitle = "Bell Project - Green Status as of " + FromTo;
                FillSheetDataxx(cnString, ref CommandText, ref Row, ref Col, ReportUser, sheet, ReportName, ReportTitle);

                // Yellow Sheet
                sheet = workbook.Worksheets[2];
                ReportName = "Yellow";
                ReportTitle = "Bell Project - Yellow Status as of " + FromTo;
                FillSheetDataxx(cnString, ref CommandText, ref Row, ref Col, ReportUser, sheet, ReportName, ReportTitle);

                // Red Sheet
                sheet = workbook.Worksheets[3];
                ReportName = "Red";
                ReportTitle = "Bell Project - Red Status as of " + FromTo;
                FillSheetDataxx(cnString, ref CommandText, ref Row, ref Col, ReportUser, sheet, ReportName, ReportTitle);

                // Red Sheet
                sheet = workbook.Worksheets[4];
                ReportName = "Other";
                ReportTitle = "Bell Project - Other Status as of " + FromTo;
                FillSheetDataxx(cnString, ref CommandText, ref Row, ref Col, ReportUser, sheet, ReportName, ReportTitle);

                // Data sheet
                Row = 1;
                Col = 1;
                //CommandText = "BSPSummarize_02_Detail '" + User.Identity.Name + "'";
                sheet = workbook.Worksheets[5];
                sheet.Name = "Data";
                CommandText = "BSPSummarize_02_Detail '" + ReportUser + "'";
                // PlaceData(sheet, ref Row, ref Col, CommandText, cnString, true);
                sheet.UsedRange.AutofitColumns();

                workbook.SaveAs("xxxition.xls", Page.Response, ExcelDownloadType.Open);
                workbook.Close();
                // Dispose the Excel engine
                excelEngine.Dispose();
            }

            catch (Exception ex)
            {
                //ex.
                //Response.Write(ex.Message);
            }
            finally
            {
                //cmd.Connection.Close();
                //cn.Close();
            }
        }

        private static void PlaceData(IWorksheet sheet, ref int Row, ref int Col, string CommandText, string cnString, Boolean ShowHeaders)
        {
            int StartRow = Row;
            int StartCol = Col;
            if (cnString != null)
            {
                //string cnString =  ;
                var cn = new SqlConnection(cnString);
                var cmd = new SqlCommand();
                try
                {
                    cmd.CommandText = CommandText;
                    cmd.CommandTimeout = 240;
                    cmd.Connection = cn;
                    cn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (ShowHeaders == true)
                    {
                        for (int count = 0; count < dr.FieldCount; count++)
                        {
                            if (dr.GetName(count) != null)
                            {
                                sheet.Range[Row, Col].Text = dr.GetName(count);
                            }
                            ++Col;
                        }
                    }
                    decimal dNum = 0;
                    double ddNum = 0;
                    while (dr.Read())
                    {
                        Col = StartCol;
                        ++Row;
                        for (int count = 0; count < dr.FieldCount; count++)
                        {
                            if (!dr.IsDBNull(count))
                            {
                                if (dr.GetValue(count).ToString() != "0")
                                {
                                    switch (dr.GetName(count).ToUpper())
                                    {
                                        case "POFFAILS":           // percent
                                            dNum = dr.GetDecimal(count);
                                            ddNum = (double)dNum;
                                            sheet.Range[Row, Col].Number = ddNum;
                                            break;
                                        //case "TOTAL FAILED":
                                        //case "FREQ":
                                        case "A":
                                        case "B":
                                        case "C":
                                        case "PASSC":
                                            //case "FAIL":
                                            //case "PASS":
                                            dNum = dr.GetDecimal(count);
                                            if (double.TryParse(dNum.ToString(), out ddNum) == false) { ddNum = 0; }
                                            sheet.Range[Row, Col].Number = ddNum;
                                            break;
                                        default:
                                            sheet.Range[Row, Col].Text = dr.GetValue(count).ToString();
                                            break;
                                    }
                                }
                            }
                            ++Col;
                        }
                    }
                    sheet.Range[StartRow, StartCol, Row, Col].AutofitColumns();
                }
                catch (Exception ex)
                {
                    //ex.
                    string xx = ex.Message;
                }
                finally
                {
                    cmd.Connection.Close();
                    cn.Close();
                }
            }
        }
        private void FillSheetDataxx(string cnString, ref string CommandText, ref int Row, ref int Col, string ReportUser, IWorksheet sheet, string ReportName, string ReportTitle)          //, IWorksheet TimeSheet, int TimeCol)
        {

            try
            {
                // Summary Sheet
                sheet.Name = ReportName;
                // set up the headers.
                Row = 1; Col = 1;
                SetExcelRange(sheet, ref Row, ref Col, Row, 27, ReportTitle, System.Drawing.Color.DarkBlue, System.Drawing.Color.White);

                Row = 3; Col = 2;
                SetExcelRange(sheet, ref Row, ref Col, Row, 7, "Status", System.Drawing.Color.Cyan, System.Drawing.Color.Black);
                Row = 5; Col = 2;

                //TimeSheet.Range[1, 1].Text = "Report 1 Start:";
                //TimeSheet.Range[1, TimeCol].Text = DateTime.Now.ToString();
                CommandText = "BSPSummarize_02_pg1 '" + ReportUser + "','','1','" + ReportName + "'";
                PlaceData(sheet, ref Row, ref Col, CommandText, cnString, true);
                //TimeSheet.Range[2, 1].Text = "Report 1 End:";
                //TimeSheet.Range[2, TimeCol].DateTime = DateTime.Now;

                //TimeSheet.Range[3, 1].Text = "Report 2 Start:";
                //TimeSheet.Range[3, TimeCol].DateTime = DateTime.Now;
                Row = 3; Col = 9;
                SetExcelRange(sheet, ref Row, ref Col, Row, 14, "Pass", System.Drawing.Color.Cyan, System.Drawing.Color.Black);
                Row = 5; Col = 9;
                CommandText = "BSPSummarize_02_pg1 '" + ReportUser + "','P','2','" + ReportName + "'";
                PlaceData(sheet, ref Row, ref Col, CommandText, cnString, true);
                //TimeSheet.Range[4, 1].Text = "Report 2 End:";
                //TimeSheet.Range[4, TimeCol].DateTime = DateTime.Now;


                //TimeSheet.Range[5, 1].Text = "Report 3 Start:";
                //TimeSheet.Range[5, TimeCol].DateTime = DateTime.Now;
                Row = 3; Col = 16;
                SetExcelRange(sheet, ref Row, ref Col, Row, 23, "Failures", System.Drawing.Color.Cyan, System.Drawing.Color.Black);
                Row = 5; Col = 16;
                CommandText = "BSPSummarize_02_pg1 '" + ReportUser + "','F','3','" + ReportName + "'";
                PlaceData(sheet, ref Row, ref Col, CommandText, cnString, true);
                //TimeSheet.Range[6, 1].Text = "Report 3 End:";
                //TimeSheet.Range[6, TimeCol].DateTime = DateTime.Now;



                //TimeSheet.Range[7, 1].Text = "Report 4 Start:";
                //TimeSheet.Range[7, TimeCol].DateTime = DateTime.Now;
                Row = 3; Col = 25;
                SetExcelRange(sheet, ref Row, ref Col, Row, 26, "Totals", System.Drawing.Color.Cyan, System.Drawing.Color.Black);
                Row = 5; Col = 25;
                CommandText = "BSPSummarize_02_pg1 '" + ReportUser + "','','4','" + ReportName + "'";
                PlaceData(sheet, ref Row, ref Col, CommandText, cnString, false);
                //TimeSheet.Range[8, 1].Text = "Report 4 End:";
                //TimeSheet.Range[8, TimeCol].DateTime = DateTime.Now;


                //TimeSheet.Range[9, 1].Text = "Report 5 Start:";
                //TimeSheet.Range[9, TimeCol].DateTime = DateTime.Now;
                Row += 4; Col = 25;
                SetExcelRange(sheet, ref Row, ref Col, Row, 27, "Failure Codes", System.Drawing.Color.Cyan, System.Drawing.Color.Black);
                Row += 2; Col = 25;
                CommandText = "BSPSummarize_02_FaultCodes '" + ReportUser + "','" + ReportName + "'";
                PlaceData(sheet, ref Row, ref Col, CommandText, cnString, true);
                //TimeSheet.Range[10, 1].Text = "Report 5 End:";
                //TimeSheet.Range[10, TimeCol].DateTime = DateTime.Now;
            }

            catch (Exception ex)
            {
                //ex.
                //Response.Write(ex.Message);
            }
            finally
            {
                //cmd.Connection.Close();
                //cn.Close();
            }

        }
        private void SetExcelRange(IWorksheet sheet, ref int Row1, ref int Col1, int row2, int col2, string Text, System.Drawing.Color BackColour, System.Drawing.Color ForeColour)
        {
            sheet.Range[Row1, Col1, row2, col2].Text = Text;
            sheet.MigrantRange[Row1, Col1, row2, col2].Merge();
            //sheet.Range[row1, col1, row2, col2].CellStyle.Color = BackColour;
            //sheet.Range[row1, col1, row2, col2]. = BackColour;

        }
        #endregion






        protected void btnUpload_Click(object sender, EventArgs e)
        {
            //if (System.Configuration.ConfigurationManager.AppSettings["WriteLog"].ToUpper() == "TRUE")
            //{
            //    log.writeLogData = true;
            //}
            //log.LogIt("**** btnUpload_Click -- Started");
            //decimal ProjID = -1;
            //lblUploadMSG.Text = "";
            //if (IMEIUploadFile.HasFile == false)
            //{
            //    lblUploadMSG.Text = "Please select an HTML file first";
            //    lblUploadMSG.ForeColor = System.Drawing.Color.Red;
            //    lblUploadMSG.Visible = true;
            //    btnIMEIUpload.Enabled = true;
            //    return;
            //}


            //string ifsType = IFSTransactionType.SelectedItem.Value;
            //IMEIUploadProcessor processor = new IMEIUploadProcessor(strNewPath, User.Identity.Name, ProjID, CL_Skcankey.Text, attributeclonelist, log, chkForce15IMEI.Checked, chkRunThreaded_Newa.Checked, ifsType);
            //if (processor.clientlocationid < 1)
            //{
            //    lblUploadMSG.Text = "You must supply a valid Scankey first";
            //    lblUploadMSG.ForeColor = System.Drawing.Color.Red;
            //    lblUploadMSG.Visible = true;
            //    btnIMEIUpload.Enabled = true;
            //    return;
            //}
            //lblUploadMSG.Text = IMEIUploadFile.FileName + "   " + processor.LoadIMEIData();
            //lblUploadMSG.Visible = true;
            //btnIMEIUpload.Text = "Upload";
            ////Page.Response.Flush();
            ////processor.workbook.SaveAs(strFileName + strFileType, Page.Response, ExcelDownloadType.Open);
            ////processor.workbook.Close();
            ////processor.excelEngine.Dispose();
            //lblUploadMSG.Visible = true;
            //btnIMEIUpload.Enabled = true;
        }



        void btnRefresh_Click(object sender, EventArgs e)
        {
            Location25TabMessage.Text = "";
            UpdateMainGrid();
        }

        protected void UpdateMainGrid()
        {
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                UpdateMainGrid(ctx);
            }
        }
        protected void UpdateMainGrid(clsLinqDataContext ctx)
        {
            decimal count = 0;
            Location25TabMessage.Text = "";
            if (chktxtSEG1.Checked == true && txtSEG1.Text.Length > 0) { count++; }
            if (chktxtSEG2.Checked == true && txtSEG2.Text.Length > 0) { count++; }
            if (chktxtSEG3.Checked == true && txtSEG3.Text.Length > 0) { count++; }
            if (chktxtSEG4.Checked == true && txtSEG4.Text.Length > 0) { count++; }
            if (count < 2) { Location25TabMessage.Text = "Two or more segments manditory."; return; }

            string SEG1 = txtSEG1.Text;
            string SEG2 = txtSEG2.Text;
            string SEG3 = txtSEG3.Text;
            string SEG4 = txtSEG4.Text;
            if (chktxtSEG1.Checked == false) { SEG1 = ""; }
            if (chktxtSEG2.Checked == false) { SEG2 = ""; }
            if (chktxtSEG3.Checked == false) { SEG3 = ""; }
            if (chktxtSEG4.Checked == false) { SEG4 = ""; }
            MainGrid.DataSource = (from x in ctx.MasterIFSLocations.OrderBy(y => y.IFSLocation)
                                   where (SEG1.Length == 0 || x.SEG1 == SEG1)
                                      && (SEG2.Length == 0 || x.SEG2 == SEG2)
                                      && (SEG3.Length == 0 || x.SEG3 == SEG3)
                                      && (SEG4.Length == 0 || x.SEG4 == SEG4)
                                   select x
            //new {
            //                           x.MasterIFSLocationID
            //                           , x.MasterIFSLocationPurpose.Purpose
            //                           , x.MasterIFSLocationStatus.Status
            //                           , x.IsWip
            //                           , x.IFSLocation
            //                           , x.Description
            //                           , x.DeviceRollup
            //                           , x.PartRollup
            //                           , x.PickLevel
            //                           , x.IsFrozen
            //                           , x.IFSLocationALT
            //                       }
            );
            MainGrid.DataBind();
        }
        void MainGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {

            }
            else if (e.CommandName != "Page")
            {
                LinkButton bbutton = (LinkButton)e.CommandSource;

                if (bbutton.ID.ToUpper() == "IMGFILTERONLOCATION")
                {
                    txtSEG1.Text = bbutton.CommandArgument.Substring(0, 3);
                    txtSEG2.Text = bbutton.CommandArgument.Substring(4, 3);
                    txtSEG3.Text = bbutton.CommandArgument.Substring(8, 3);
                    txtSEG4.Text = bbutton.CommandArgument.Substring(12, 3);
                }

                #region Download
                string script = "";
                if (bbutton.ID.ToUpper() == "IMGDOWNLOADDEVICES")
                {
                    script = "PrintReport('IFSLOCATIONDEVICES','" + bbutton.CommandArgument + "');";
                    //script = "alert('IFSLOCATIONDEVICES:'" + bbutton.CommandArgument + "');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Saved", script, true);
                }
                if (bbutton.ID.ToUpper() == "IMGDOWNLOADPARTS")
                {
                    script = "PrintReport('IFSLOCATIONPARTS','" + bbutton.CommandArgument + "');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Saved", script, true);
                }
                #endregion
            }
        }
        void MainGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                MasterIFSLocation data = (MasterIFSLocation)e.Row.DataItem;
                LinkButton bbutton = (LinkButton)e.Row.FindControl("imgFilterOnLocation");
                if (bbutton != null) { bbutton.CommandArgument = ""; }
                if (bbutton != null)
                {
                    if (bbutton != null) { bbutton.CommandArgument = data.IFSLocation; }
                }
                bbutton = (LinkButton)e.Row.FindControl("imgDownloadDevices");
                if (bbutton != null) { bbutton.CommandArgument = ""; }
                if (bbutton != null)
                {
                    if (bbutton != null) { bbutton.CommandArgument = data.IFSLocation; }
                }
                bbutton = (LinkButton)e.Row.FindControl("imgDownloadParts");
                if (bbutton != null) { bbutton.CommandArgument = ""; }
                if (bbutton != null)
                {
                    if (bbutton != null) { bbutton.CommandArgument = data.IFSLocation; }
                }
            }
        }


        void btnTest_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }



        void btnExtractSamsung_Click(object sender, EventArgs e)
        {

            ExtractManager em = new ExtractManager(User.Identity.Name);
            //string sBegin = "";
            //string sEnd = "";

            string BeginDateString = txtBeginDate.Text;
            string EndDateString = txtEndDate.Text;

            DateTime BeginDate = DateTime.Now;
            DateTime EndDate = DateTime.Now;

            if (chkReceived.Checked == false)
            {
                BeginDateString = string.Format("{0}", string.Format("{0:MM/dd/yyyy}", DateTime.Today.AddDays(-1)));
                EndDateString = string.Format("{0}", string.Format("{0:MM/dd/yyyy}", DateTime.Today.AddDays(-1)));
            }




            StringWriter oStringWriter = em.GetSamsungExtract(BeginDateString, EndDateString);

            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=" + string.Format("SECA_ASCID_{0}.txt", string.Format("{0:yyyyMMdd}", DateTime.Today)));
            Response.Charset = "";
            Response.ContentType = "text/plain";

            using (StreamWriter writer = new StreamWriter(Response.OutputStream, Encoding.UTF8))
            {
                writer.Write(oStringWriter.ToString());
            }
            Response.End();
            Response.Flush();

        }




        void setupXLSDropdown()
        {
            //drpXlsFormat.Items.Clear();
            //z = new ListItem("Excel2007", "Excel2007");
            //drpXlsFormat.Items.Add(z);
            //z = new ListItem("Excel2010", "Excel2010");
            //drpXlsFormat.Items.Add(z);
            //z = new ListItem("Excel97to2003", "Excel97to2003");
            //drpXlsFormat.Items.Add(z);
            //drpXlsFormat.SelectedIndex = 0;
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

        void grdTempBillingPoint_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            LinkButton btnAdd = (LinkButton)e.CommandSource;
            switch (btnAdd.CommandArgument.ToString().ToUpper())
            {
                case "BILLINGPOINT":
                    RunTemplatedReports("~/templates/BillingPoints", btnAdd.CommandName.ToString(), btnAdd.CommandArgument.ToString().ToUpper());
                    break;
                case "DELETEBILLING":
                    DeleteTemplatedReport("~/templates/BillingPoints", btnAdd.CommandName.ToString(), lblMsgBilling, grdTempBillingPoint);
                    break;

                case "DOWNLOADTEMPLATE":
                    DownloadTemplatedReport("~/templates/BillingPoints", btnAdd.CommandName.ToString(), lblMsgBilling, grdTempBillingPoint);
                    break;



                default:
                    break;
            }
        }

        void grdTempBillingPoint_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Type type = e.Row.DataItem.GetType();               //.GetGenericArguments()[0]; 
                LinkButton bPrint = (LinkButton)e.Row.FindControl("imgPrint");
                if (bPrint != null)
                {
                    bPrint.CommandArgument = "BillingPoint";
                    bPrint.CommandName = ((FileInfo)e.Row.DataItem).Name;
                }
                bPrint = (LinkButton)e.Row.FindControl("imgDelete");
                if (bPrint != null)
                {
                    bPrint.CommandArgument = "DeleteBilling";
                    bPrint.CommandName = ((FileInfo)e.Row.DataItem).Name;
                }
                bPrint = (LinkButton)e.Row.FindControl("imgDownLoad");
                if (bPrint != null)
                {
                    bPrint.CommandArgument = "DOWNLOADTEMPLATE";
                    bPrint.CommandName = ((FileInfo)e.Row.DataItem).Name;
                }
            }
        }

        private void UpdateTemplateGrid(string templatePath, GridView grdTemp)
        {
            DirectoryInfo di = new DirectoryInfo(Page.MapPath(templatePath));
            FileInfo[] rgFiles = di.GetFiles("*.*");
            grdTemp.DataSource = rgFiles;
            grdTemp.DataBind();
        }

        void grdTempDetail_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            LinkButton btnAdd = (LinkButton)e.CommandSource;
            switch (btnAdd.CommandArgument.ToString().ToUpper())
            {
                case "DETAIL":
                    RunTemplatedReports("~/templates/HTML", btnAdd.CommandName.ToString(), btnAdd.CommandArgument.ToString().ToUpper());
                    break;
                case "DELETEDETAIL":
                    DeleteTemplatedReport("~/templates/HTML", btnAdd.CommandName.ToString(), lblMsgDetail, grdTempDetail);
                    break;
                case "DOWNLOADTEMPLATE":
                    DownloadTemplateFile("~/templates/HTML", btnAdd.CommandName.ToString(), lblMsgBilling, grdTempDetail);
                    break;
                default:
                    break;
            }
        }
        void grdTempDetail_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Type type = e.Row.DataItem.GetType();               //.GetGenericArguments()[0]; 
                LinkButton bPrint = (LinkButton)e.Row.FindControl("imgPrint");
                if (bPrint != null)
                {
                    bPrint.CommandArgument = "Detail";
                    bPrint.CommandName = ((FileInfo)e.Row.DataItem).Name;
                }
                bPrint = (LinkButton)e.Row.FindControl("imgDelete");
                if (bPrint != null)
                {
                    bPrint.CommandArgument = "DeleteDetail";
                    bPrint.CommandName = ((FileInfo)e.Row.DataItem).Name;
                }
                bPrint = (LinkButton)e.Row.FindControl("imgDownLoad");
                if (bPrint != null)
                {
                    bPrint.CommandArgument = "DOWNLOADTEMPLATE";
                    bPrint.CommandName = ((FileInfo)e.Row.DataItem).Name;
                }
            }
        }


        void grdTempDetailClient_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            LinkButton btnAdd = (LinkButton)e.CommandSource;
            switch (btnAdd.CommandArgument.ToString().ToUpper())
            {
                case "DETAILCLIENT":
                    RunTemplatedReports("~/templates/HTML", btnAdd.CommandName.ToString(), btnAdd.CommandArgument.ToString().ToUpper());
                    break;
                case "DELETEDETAILCLIENT":
                    DeleteTemplatedReport("~/templates/HTML", btnAdd.CommandName.ToString(), lblMsgDetail, grdTempDetail);
                    break;
                case "DOWNLOADTEMPLATE":
                    DownloadTemplatedReport("~/templates/HTML", btnAdd.CommandName.ToString(), lblMsgBilling, grdTempBillingPoint);
                    break;
                default:
                    break;
            }
        }
        void grdTempDetailClient_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Type type = e.Row.DataItem.GetType();               //.GetGenericArguments()[0]; 
                LinkButton bPrint = (LinkButton)e.Row.FindControl("imgPrint");
                if (bPrint != null)
                {
                    bPrint.CommandArgument = "DetailClient";
                    bPrint.CommandName = ((FileInfo)e.Row.DataItem).Name;
                }
                bPrint = (LinkButton)e.Row.FindControl("imgDelete");
                if (bPrint != null)
                {
                    bPrint.CommandArgument = "DeleteDetailClient";
                    bPrint.CommandName = ((FileInfo)e.Row.DataItem).Name;
                }
                bPrint = (LinkButton)e.Row.FindControl("imgDownLoad");
                if (bPrint != null)
                {
                    bPrint.CommandArgument = "DOWNLOADTEMPLATE";
                    bPrint.CommandName = ((FileInfo)e.Row.DataItem).Name;
                }
            }
        }


        //void grdTempDetailDate_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        Type type = e.Row.DataItem.GetType();               //.GetGenericArguments()[0]; 
        //        LinkButton bPrint = (LinkButton)e.Row.FindControl("imgPrint");
        //        if (bPrint != null)
        //        {
        //            bPrint.CommandArgument = "DetailDate";
        //            bPrint.CommandName = ((FileInfo)e.Row.DataItem).Name;
        //        }
        //        bPrint = (LinkButton)e.Row.FindControl("imgDelete");
        //        if (bPrint != null)
        //        {
        //            bPrint.CommandArgument = "DeleteDetailDate";
        //            bPrint.CommandName = ((FileInfo)e.Row.DataItem).Name;
        //        }
        //    }
        //}
        //void grdTempBulk_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        Type type = e.Row.DataItem.GetType();               //.GetGenericArguments()[0]; 
        //        LinkButton bPrint = (LinkButton)e.Row.FindControl("imgPrint");
        //        if (bPrint != null)
        //        {
        //            bPrint.CommandArgument = "Bulk";
        //            bPrint.CommandName = ((FileInfo)e.Row.DataItem).Name;
        //        }
        //        bPrint = (LinkButton)e.Row.FindControl("imgDelete");
        //        if (bPrint != null)
        //        {
        //            bPrint.CommandArgument = "DeleteBulk";
        //            bPrint.CommandName = ((FileInfo)e.Row.DataItem).Name;
        //        }
        //    }
        //}
        //void grdTempBSP_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        Type type = e.Row.DataItem.GetType();               //.GetGenericArguments()[0]; 
        //        LinkButton bPrint = (LinkButton)e.Row.FindControl("imgPrint");
        //        if (bPrint != null)
        //        {
        //            bPrint.CommandArgument = "BSP";
        //            bPrint.CommandName = ((FileInfo)e.Row.DataItem).Name;
        //        }
        //        bPrint = (LinkButton)e.Row.FindControl("imgDelete");
        //        if (bPrint != null)
        //        {
        //            bPrint.CommandArgument = "DeleteBSP";
        //            bPrint.CommandName = ((FileInfo)e.Row.DataItem).Name;
        //        }
        //    }
        //}


        /// /////////////////////////////////////////////////////////

        //void btnDisp_02_Click(object sender, EventArgs e)
        //{
        //    //GenerateReportChart();
        //    //GenerateReport();

        //    string cnString = ConnectionString;
        //    if (cnString != null)
        //    {
        //        var cn = new SqlConnection(cnString);
        //        var cmd = new SqlCommand();
        //        {
        //            cmd.CommandText = "BSPSummarize_02 " + ParamaterString_01() + ",'" + User.Identity.Name + "'";
        //            cmd.CommandTimeout = 120;
        //            cmd.Connection = cn;
        //            cn.Open();
        //            cmd.ExecuteNonQuery();
        //            ScriptManager.RegisterStartupScript(this, GetType(), "LoadClient", "alert('Summary file is built');", true);
        //        }
        //    }
        //}

        //void btnDisp_02Report_Click(object sender, EventArgs e)
        //{
        //    ExportDisposalToExcel();
        //}
        /// /////////////////////////////////////////////////////////


        public string ParamaterString_03()
        {
            string rString = "";

            string ProjectName = drpProjectList.SelectedItem.Text;
            string ProjectID = drpProjectList.SelectedItem.Value;
            if (ProjectName.ToUpper() == "ALL") { ProjectName = ""; }
            string ClientKey = txtClient.Text;
            string ProjectTag = txtProjectTag.Text;
            string RMANumber = txtRMA.Text;

            string BeginDateString = txtBeginDate.Text;
            string EndDateString = txtEndDate.Text;
            string QCBeginDateString = txtBeginQC.Text;
            string QCEndDateString = txtEndQC.Text;
            string FunctionTestBeginDateString = txtBeginQC.Text;
            string FunctionTestEndDateString = txtEndQC.Text;
            string ShippedBeginDateString = txtBeginShipped.Text;
            string ShippedEndDateString = txtEndShipped.Text;

            if (chkReceived.Checked == false) { BeginDateString = ""; EndDateString = ""; }
            if (chkQC.Checked == false) { QCBeginDateString = ""; QCEndDateString = ""; }
            if (chkFunctionTest.Checked == false) { FunctionTestBeginDateString = ""; FunctionTestEndDateString = ""; }

            if (chkShipped.Checked == false) { ShippedBeginDateString = ""; ShippedEndDateString = ""; }
            //rString = " '" + BeginDateString + "',";
            //rString += "'" + EndDateString + "',";
            //rString += "'" + ProjectName + "'";
            rString += " '" + ProjectID + "'";

            //rString += "'" + ClientKey + "',";
            //rString += "'" + RMANumber + "',";
            //rString += "'" + ProjectTag + "',";
            //rString += "'" + ShippedBeginDateString + "',";
            //rString += "'" + ShippedEndDateString + "'";
            return rString;
        }
        public string ParamaterString_Raw()
        {
            return ParamaterString_Raw(User.Identity.Name);
        }
        public string ParamaterString_Raw(string Username)
        {
            string rString = "";
            string ProjectName = drpProjectList.SelectedItem.Text;

            BasicUserUtilities buu = new BasicUserUtilities(User.Identity.Name);
            string ClientKey = buu.GetUserValidClientKey(User.Identity.Name, txtClient.Text);
            //string ClientKey = "";
            string ProjectTag = txtProjectTag.Text;
            string RMANumber = txtRMA.Text;

            string BeginDateString = txtBeginDate.Text;
            string EndDateString = txtEndDate.Text;
            string QCBeginDateString = txtBeginQC.Text;
            string QCEndDateString = txtEndQC.Text;
            string FunctionTestBeginDateString = txtBeginQC.Text;
            string FunctionTestEndDateString = txtEndQC.Text;
            string ShippedBeginDateString = txtBeginShipped.Text;
            string ShippedEndDateString = txtEndShipped.Text;
            string BinNumber = txtBinNumber.Text;
            string Hobble = txtHobble.Text;

            string sStatus = drpStatus.SelectedItem.Text;
            string sClientID = drpClientList.SelectedItem.Value.ToString();
            string sClient = "";
            string sIMEI = txtIMEI.Text;
            string sCarrier = drpCarrier.SelectedItem.Text;
            string sManufacturer = drpManufacturer.SelectedItem.Text;
            string sModel = drpModel.SelectedItem.Text;
            string sColour = drpColour.SelectedItem.Text;
            string sSKU = txtSKU.Text;

            if (ProjectName.ToUpper() == "ALL") { ProjectName = ""; }
            if (sStatus.ToUpper() == "ALL") { sStatus = ""; }
            if (sCarrier.ToUpper() == "ALL") { sCarrier = ""; }
            if (sManufacturer.ToUpper() == "ALL") { sManufacturer = ""; }
            if (sModel.ToUpper() == "ALL") { sModel = ""; }
            if (sColour.ToUpper() == "ALL") { sColour = ""; }

            string ShowGraveYard = "N";


            // if (chkShowGraveyard.Checked == true) { ShowGraveYard = "Y"; }
            if (chkReceived.Checked == false) { BeginDateString = ""; EndDateString = ""; }
            if (chkQC.Checked == false) { QCBeginDateString = ""; QCEndDateString = ""; }
            if (chkFunctionTest.Checked == false) { FunctionTestBeginDateString = ""; FunctionTestEndDateString = ""; }

            if (chkShipped.Checked == false) { ShippedBeginDateString = ""; ShippedEndDateString = ""; }

            rString = " '" + sClientID + "',";
            rString += " '" + ProjectName + "',";
            rString += "'" + ClientKey + "',";
            rString += "'" + RMANumber + "',";
            rString += "'" + ProjectTag + "',";
            rString += "'" + BeginDateString + "',";
            rString += "'" + EndDateString + "',";
            rString += "'" + QCBeginDateString + "',";
            rString += "'" + QCEndDateString + "',";
            //rString += "'" + FunctionTestBeginDateString + "',";
            //rString += "'" + FunctionTestEndDateString + "',";


            rString += "'" + ShippedBeginDateString + "',";
            rString += "'" + ShippedEndDateString + "',";
            rString += "'" + BinNumber + "',";
            rString += "'" + Hobble + "',";

            rString += "'" + sStatus + "',";
            rString += "'" + sClient + "',";
            rString += "'" + sIMEI + "',";
            rString += "'" + sCarrier + "',";
            rString += "'" + sManufacturer + "',";
            rString += "'" + sModel + "',";
            rString += "'" + sColour + "',";
            rString += "'" + sSKU + "',";

            rString += "'" + ShowGraveYard + "',";
            rString += "'" + Username + "'";
            return rString;
        }

        public string ParamaterString_RawVersion()
        {
            string rstring = "";
            string version = txtIMEIVersion.Text;
            if (version.ToUpper() == "ALL") { version = "   "; }
            if (version.Length > 3) { version = version.Substring(0, 3); }
            if (version.Length < 3 & version.Length > 0) { version = version.PadLeft(3, '0'); }
            rstring = ParamaterString_Raw(User.Identity.Name) + ",'" + version + "'";
            return rstring;
        }

        public string ParamaterString_B_RawVersion()
        {
            string rstring = ParamaterString_RawVersion();
            string ProductPlace = drpProductPlace.SelectedItem.Text;
            if (ProductPlace.ToUpper() == "ALL") { ProductPlace = ""; }
            //if (ProductPlace.Length > 3) { ProductPlace = ProductPlace.Substring(0, 3); }
            //if (ProductPlace.Length < 3 & ProductPlace.Length > 0) { ProductPlace = ProductPlace.PadLeft(3, '0'); }
            rstring = rstring + ",'" + ProductPlace + "'";
            return rstring;
        }



        public string ParamaterString_Raw_ThisUser(string UserName)
        {
            if (UserName == "All") { UserName = ""; }


            string rString = "";
            string ProjectName = drpProjectList.SelectedItem.Text;

            BasicUserUtilities buu = new BasicUserUtilities(User.Identity.Name);
            string ClientKey = buu.GetUserValidClientKey(User.Identity.Name, txtClient.Text);
            //string ClientKey = "";
            string ProjectTag = txtProjectTag.Text;
            string RMANumber = txtRMA.Text;

            string BeginDateString = txtLogDateBegin.Text;
            string EndDateString = txtLogDateEnd.Text;

            string QCBeginDateString = txtBeginQC.Text;
            string QCEndDateString = txtEndQC.Text;
            string FunctionTestBeginDateString = txtBeginQC.Text;
            string FunctionTestEndDateString = txtEndQC.Text;


            string ShippedBeginDateString = txtBeginShipped.Text;
            string ShippedEndDateString = txtEndShipped.Text;
            string BinNumber = txtBinNumber.Text;
            string Hobble = txtHobble.Text;

            string sStatus = drpStatus.SelectedItem.Text;
            string sClientID = drpClientList.SelectedItem.Value.ToString();
            string sClient = "";
            string sIMEI = txtIMEI.Text;
            string sCarrier = drpCarrier.SelectedItem.Text;
            string sManufacturer = drpManufacturer.SelectedItem.Text;
            string sModel = drpModel.SelectedItem.Text;
            string sColour = drpColour.SelectedItem.Text;
            string sSKU = txtSKU.Text;

            if (ProjectName.ToUpper() == "ALL") { ProjectName = ""; }
            if (sStatus.ToUpper() == "ALL") { sStatus = ""; }
            if (sCarrier.ToUpper() == "ALL") { sCarrier = ""; }
            if (sManufacturer.ToUpper() == "ALL") { sManufacturer = ""; }
            if (sModel.ToUpper() == "ALL") { sModel = ""; }
            if (sColour.ToUpper() == "ALL") { sColour = ""; }

            string ShowGraveYard = "N";


            // if (chkShowGraveyard.Checked == true) { ShowGraveYard = "Y"; }
            if (chkLogDate.Checked == false) { BeginDateString = ""; EndDateString = ""; }
            if (chkQC.Checked == false) { QCBeginDateString = ""; QCEndDateString = ""; }
            if (chkFunctionTest.Checked == false) { FunctionTestBeginDateString = ""; FunctionTestEndDateString = ""; }
            if (chkShipped.Checked == false) { ShippedBeginDateString = ""; ShippedEndDateString = ""; }

            rString = " '" + sClientID + "',";
            rString += " '" + ProjectName + "',";
            rString += "'" + ClientKey + "',";
            rString += "'" + RMANumber + "',";
            rString += "'" + ProjectTag + "',";
            rString += "'" + BeginDateString + "',";
            rString += "'" + EndDateString + "',";
            rString += "'" + QCBeginDateString + "',";
            rString += "'" + QCEndDateString + "',";

            //rString += "'" + FunctionTestBeginDateString + "',";
            //rString += "'" + FunctionTestEndDateString + "',";

            rString += "'" + ShippedBeginDateString + "',";
            rString += "'" + ShippedEndDateString + "',";
            rString += "'" + BinNumber + "',";
            rString += "'" + Hobble + "',";

            rString += "'" + sStatus + "',";
            rString += "'" + sClient + "',";
            rString += "'" + sIMEI + "',";
            rString += "'" + sCarrier + "',";
            rString += "'" + sManufacturer + "',";
            rString += "'" + sModel + "',";
            rString += "'" + sColour + "',";
            rString += "'" + sSKU + "',";

            rString += "'" + ShowGraveYard + "',";
            rString += "'" + UserName + "'";
            return rString;
        }
        public string ParamaterString_BucketRaw(string Username)
        {
            string rString = "";

            string ProjectName = drpProjectList.SelectedItem.Text;

            BasicUserUtilities buu = new BasicUserUtilities(User.Identity.Name);
            string ClientKey = buu.GetUserValidClientKey(User.Identity.Name, txtClient.Text);
            //string ClientKey = "";
            string ProjectTag = txtProjectTag.Text;
            string RMANumber = txtRMA.Text;

            string BeginDateString = txtBeginDate.Text;
            string EndDateString = txtEndDate.Text;
            string QCBeginDateString = txtBeginQC.Text;
            string QCEndDateString = txtEndQC.Text;
            string FunctionTestBeginDateString = txtBeginQC.Text;
            string FunctionTestEndDateString = txtEndQC.Text;
            string ShippedBeginDateString = txtBeginShipped.Text;
            string ShippedEndDateString = txtEndShipped.Text;
            string BinNumber = txtBinNumber.Text;
            string Hobble = txtHobble.Text;

            string sStatus = drpStatus.SelectedItem.Text;
            string sClientID = drpClientList.SelectedItem.Value.ToString();
            string sClient = "";
            string sIMEI = txtIMEI.Text;
            string sCarrier = drpCarrier.SelectedItem.Text;
            string sManufacturer = drpManufacturer.SelectedItem.Text;
            string sModel = drpModel.SelectedItem.Text;
            string sColour = drpColour.SelectedItem.Text;
            string sSKU = txtSKU.Text;

            if (ProjectName.ToUpper() == "ALL") { ProjectName = ""; }
            if (sStatus.ToUpper() == "ALL") { sStatus = ""; }
            if (sCarrier.ToUpper() == "ALL") { sCarrier = ""; }
            if (sManufacturer.ToUpper() == "ALL") { sManufacturer = ""; }
            if (sModel.ToUpper() == "ALL") { sModel = ""; }
            if (sColour.ToUpper() == "ALL") { sColour = ""; }


            string Summarize = "N";
            string GroupProcess = "N";
            if (chkSummarize.Checked == true) { Summarize = "Y"; }
            if (chkGroupProcess.Checked == true) { GroupProcess = "Y"; }

            // if (chkShowGraveyard.Checked == true) { ShowGraveYard = "Y"; }
            if (chkReceived.Checked == false) { BeginDateString = ""; EndDateString = ""; }
            if (chkQC.Checked == false) { QCBeginDateString = ""; QCEndDateString = ""; }
            if (chkFunctionTest.Checked == false) { FunctionTestBeginDateString = ""; FunctionTestEndDateString = ""; }

            if (chkShipped.Checked == false) { ShippedBeginDateString = ""; ShippedEndDateString = ""; }

            rString = " '" + sClientID + "',";
            rString += " '" + ProjectName + "',";
            rString += "'" + ClientKey + "',";
            rString += "'" + RMANumber + "',";
            rString += "'" + ProjectTag + "',";
            rString += "'" + BeginDateString + "',";
            rString += "'" + EndDateString + "',";
            rString += "'" + QCBeginDateString + "',";
            rString += "'" + QCEndDateString + "',";

            //rString += "'" + FunctionTestBeginDateString + "',";
            //rString += "'" + FunctionTestEndDateString + "',";



            rString += "'" + ShippedBeginDateString + "',";
            rString += "'" + ShippedEndDateString + "',";
            rString += "'" + BinNumber + "',";
            rString += "'" + Hobble + "',";

            rString += "'" + sStatus + "',";
            rString += "'" + sClient + "',";
            rString += "'" + sIMEI + "',";
            rString += "'" + sCarrier + "',";
            rString += "'" + sManufacturer + "',";
            rString += "'" + sModel + "',";
            rString += "'" + sColour + "',";
            rString += "'" + sSKU + "',";

            rString += "'" + Summarize + "',";
            rString += "'" + GroupProcess + "',";
            rString += "'" + Username + "'";
            return rString;
        }


        #region btnClicks

        void btnExportProjectDefinition_Click(object sender, EventArgs e)
        {

            Button xSender = (Button)sender;
            string CommandText = xSender.CommandArgument;
            string cnString = ConnectionString;
            //GET Data From Database      
            if (cnString != null)
            {
                var cn = new SqlConnection(cnString);
                var cmd = new SqlCommand
                {
                    CommandText = xSender.CommandArgument + ParamaterString_03(),
                    Connection = cn
                };
                lblMessage.Text = ExportToExcel(cn, cmd, xSender.CommandName);
            }
        }
        void btnExportDataFile_Click(object sender, EventArgs e)
        {
            Button xSender = (Button)sender;
            string CommandText = xSender.CommandArgument;
            //System.Configuration.ConnectionStringSettingsCollection connectionString = WebConfigurationManager.ConnectionStrings;
            //string cnString = connectionString["GMP_DataEntities"].ConnectionString.ToString();

            string cnString = ConnectionString;
            //GET Data From Database      
            if (cnString != null)
            {
                var cn = new SqlConnection(cnString);
                var cmd = new SqlCommand
                {
                    CommandText = xSender.CommandArgument,
                    Connection = cn
                };
                lblMessage.Text = ExportToExcel(cn, cmd, xSender.CommandName);
            }
        }

        private string ExportToExcel(SqlConnection cn, SqlCommand cmd, string fileName)
        {
            List<string> fieldListToReport = new List<string>();
            return ExportToExcel(cn, cmd, fileName, fieldListToReport);
        }

        #region TemplateArea
        protected void btnUploadDetail_Click(object sender, EventArgs e)
        {
            string PathName = "~/templates/HTML";
            UploadFileHTML(PathName, FileUploadDetail, lblMsgDetail);
            UpdateTemplateGrid(PathName, grdTempDetail);
        }
        protected void btnRefreshDetail_Click(object sender, EventArgs e)
        {
            string PathName = "~/templates/HTML";
            UpdateTemplateGrid(PathName, grdTempDetail);
        }
        protected void btnUploadDetailClient_Click(object sender, EventArgs e)
        {
            string PathName = "~/templates/HTML";
            UploadFileHTML(PathName, FileUploadDetailClient, lblMsgDetailClient);
            UpdateTemplateGrid(PathName, grdTempDetailClient);
        }

        protected void btnUploadBillingPoint_Click(object sender, EventArgs e)
        {
            string PathName = "~/templates/HTML";
            UploadFileHTML(PathName, FileUploadBillingPoint, lblMsgBillingPoint);
            UpdateTemplateGrid(PathName, grdTempBillingPoint);
        }

        protected void btnUploadDetailDate_Click(object sender, EventArgs e)
        {
            string PathName = "~/templates/HTML";
            UploadFileHTML(PathName, FileUploadDetailDate, lblMsgDetailDate);
            UpdateTemplateGrid(PathName, grdTempDetailDate);
        }
        protected void btnUploadBulk_Click(object sender, EventArgs e)
        {
            string PathName = "~/templates/HTML";
            UploadFileHTML(PathName, FileUploadBulk, lblMsgBulk);
            UpdateTemplateGrid(PathName, grdTempBulk);
        }
        protected void btnUploadBSP_Click(object sender, EventArgs e)
        {
            string PathName = "~/templates/HTML";
            UploadFileHTML(PathName, FileUploadBSP, lblMsgBSP);
            UpdateTemplateGrid(PathName, grdTempBSP);
        }
        #endregion

        private string UploadFile(string PathName, FileUpload UploadTool, Label Message)
        {
            if ((UploadTool.HasFile))
            {
                string strFileName = UploadTool.FileName + "_" + DateTime.Now.ToString("ddMMyyyy_HHmmss");
                string strFileType = System.IO.Path.GetExtension(UploadTool.FileName).ToString().ToLower();
                //Check file type
                if (strFileType == ".xls" || strFileType == ".xlsx")
                {
                    UploadTool.SaveAs(Server.MapPath(PathName + "/" + strFileName + strFileType));
                    Message.Text = "Data File Uploaded!";
                    Message.ForeColor = System.Drawing.Color.Green;
                    Message.Visible = true;
                    return PathName + "/" + strFileName + strFileType;
                    //UpdateTemplateGrid(PathName, grdTemplate);
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
            return "";
        }


        private string UploadFileHTML(string PathName, FileUpload UploadTool, Label Message)
        {
            if ((UploadTool.HasFile))
            {
                string strFileName = UploadTool.FileName;              // +"_" + DateTime.Now.ToString("ddMMyyyy_HHmmss");
                string strFileType = System.IO.Path.GetExtension(UploadTool.FileName).ToString().ToLower();
                //Check file type
                if (strFileType.ToUpper() == ".HTM" || strFileType.ToUpper() == ".HTML")
                {
                    UploadTool.SaveAs(Server.MapPath(PathName + "/" + strFileName));
                    //UploadTool.SaveAs(Server.MapPath(PathName + "/" + strFileName + strFileType));
                    Message.Text = "Data File Uploaded!";
                    Message.ForeColor = System.Drawing.Color.Green;
                    Message.Visible = true;
                    return PathName + "/" + strFileName;
                    //UpdateTemplateGrid(PathName, grdTemplate);
                }
                else
                {
                    Message.Text = "Only HTM/HTML files allowed";
                    Message.ForeColor = System.Drawing.Color.Red;
                    Message.Visible = true;
                }
            }
            else
            {
                Message.Text = "Please select an HTM/HTML file first";
                Message.ForeColor = System.Drawing.Color.Red;
                Message.Visible = true;
            }
            return "";
        }


        private void DeleteTemplatedReport(string PathName, string FileName, Label Message, GridView grdTemplate)
        {
            string templatePath = Page.MapPath(PathName);

            File.Delete(templatePath + "/" + FileName);
            Message.Text = "Data File Deleted!";
            Message.ForeColor = System.Drawing.Color.Green;
            Message.Visible = true;
            UpdateTemplateGrid(PathName, grdTemplate);
        }
        private void DownloadTemplateFile(string PathName, string FileName, Label Message, GridView grdTemplate)
        {
            //string templatePath = Page.MapPath(PathName);

            string templatePath = Page.MapPath(PathName + "/" + FileName);


            string filecontent = "";
            using (StreamReader sr = File.OpenText(templatePath))
            {
                filecontent = sr.ReadToEnd();
            }



            //StringBuilder b = new StringBuilder();
            //b.
            //StringWriter oStringWriter = new StringWriter(

            Response.Clear();
            //Response.AddHeader("content-disposition", "attachment;filename=" + string.Format("SECA_ASCID_{0}.txt", string.Format("{0:yyyyMMdd}", DateTime.Today)));
            Response.AddHeader("content-disposition", "attachment;filename=" + FileName);
            Response.Charset = "";
            Response.ContentType = "text/plain";

            using (StreamWriter writer = new StreamWriter(Response.OutputStream, Encoding.UTF8))
            {
                writer.Write(filecontent);
            }
            Response.End();
            Response.Flush();





            //ExcelEngine excelEngine = new ExcelEngine();
            //IApplication application = excelEngine.Excel;
            //IWorkbook workbook = application.Workbooks.Open(templatePath, ExcelOpenType.Automatic);

            //foreach (IWorksheet sheet in workbook.Worksheets)
            //{
            //    sheet.UsedRange.AutofitColumns();
            //}
            //workbook.SaveAs(FileName, Page.Response, ExcelDownloadType.Open);
            //workbook.Close();
            //excelEngine.Dispose();
        }
        private void DownloadTemplatedReport(string PathName, string FileName, Label Message, GridView grdTemplate)
        {
            //string templatePath = Page.MapPath(PathName);

            string templatePath = Page.MapPath(PathName + "/" + FileName);
            ExcelEngine excelEngine = new ExcelEngine();
            IApplication application = excelEngine.Excel;
            IWorkbook workbook = application.Workbooks.Open(templatePath, ExcelOpenType.Automatic);

            foreach (IWorksheet sheet in workbook.Worksheets)
            {
                sheet.UsedRange.AutofitColumns();
            }
            workbook.SaveAs(FileName, Page.Response, ExcelDownloadType.Open);
            workbook.Close();
            excelEngine.Dispose();
        }
        private void RunTemplatedReports(string PathName, string TemplateName, string Data)
        {
            /* Query the database for report data */
            DataTable dt = null;
            string tName = "";
            switch (Data.ToUpper())
            {
                case "BILLINGPOINT":
                    //string cmdString = "";
                    string BeginDateString = txtBPStartDate_Template.Text;
                    string EndDateString = txtBPEndDate_Template.Text;
                    if (chkBillingpoint_Template.Checked == false) { BeginDateString = ""; EndDateString = ""; }
                    if (chkShowRecorded_Template.Checked == true)
                    {
                        dt = GetData("GetBillingPointTransactionBillingPoints_01 " + "'Y','" + BeginDateString + "','" + EndDateString + "'," + ParamaterString_Raw());
                    }
                    else
                    {
                        dt = GetData("GetBillingPointTransactionBillingPoints_01 " + "'N','" + BeginDateString + "','" + EndDateString + "'," + ParamaterString_Raw());
                    }
                    tName = "BillingPoint";
                    break;
                case "DETAIL":
                    dt = GetData("GetMasterDetailInventoryList_TemplateRawData_01 " + ParamaterString_Raw());
                    tName = "Detail";
                    break;
                case "DETAILCLIENT":
                    dt = GetData("GetMasterDetailInventoryClientList_TemplateRawData_01 " + ParamaterString_Raw());
                    tName = "DetailClient";
                    break;
                //case "BSP":
                //    ///* Pass the DataTable to ExcelTemplate */
                //    //dt = GetData("BSPSummarize_pg1 '" + User.Identity.Name + "'");
                //    //dt = GetData("BSPSummarize_pg1a '" + User.Identity.Name + "'");
                //    //dt = GetData("BSPSummarize_pg2 '" + User.Identity.Name + "'");
                //    //dt = GetData("BSPSummarize_pg3 '" + User.Identity.Name + "'");
                //    //dt = GetData("BSPSummarize_pg4 '" + User.Identity.Name + "'");
                //    break;
                default:
                    return;
            }

            if (dt == null) { return; }
            dt.TableName = tName;
            string templatePath = Page.MapPath(PathName + "/" + TemplateName);
            ExcelEngine excelEngine = new ExcelEngine();
            IApplication application = excelEngine.Excel;
            IWorkbook workbook = application.Workbooks.Open(templatePath, ExcelOpenType.Automatic);

            ITemplateMarkersProcessor marker = workbook.CreateTemplateMarkersProcessor();
            marker.AddVariable(tName, dt);
            marker.ApplyMarkers(UnknownVariableAction.Skip);
            //marker.ApplyMarkers(UnknownVariableAction.ReplaceBlank);

            foreach (IWorksheet sheet in workbook.Worksheets)
            {
                sheet.UsedRange.AutofitColumns();
            }
            workbook.SaveAs(TemplateName, Page.Response, ExcelDownloadType.Open);
            workbook.Close();
            excelEngine.Dispose();
        }

        #endregion

        private DataTable GetData(string SPName)
        {
            /* Queries for total unit sales for three types of products */
            string SQL = SPName;
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
                new SqlDataAdapter(SQL, conn).Fill(dt);

            return dt;
        }

        void btnDiscrepancyReport_Click(object sender, EventArgs e)
        {
            string cmdString = "";
            //cmdString = "GetDiscrepancyReport_01 " + ParamaterString_Raw_ThisUser(drpUserList.SelectedItem.Value);
            cmdString = "GetDiscrepancyReport_01 " + ParamaterString_Raw(drpUserList.SelectedItem.Value);
            ExportToExcel(cmdString, "DiscrepancyReport");
        }

        void btnUserFrequency_Click(object sender, EventArgs e)
        {
            string cmdString = "";
            cmdString = "GetMasterDetailUserFrequencyList_TemplateRawData_04 " + ParamaterString_Raw_ThisUser(drpUserList.SelectedItem.Value) + ",15";
            ExportToExcel(cmdString, "DetailInventory");
        }


        void btnUserFrequency2_Click(object sender, EventArgs e)
        {
            string cmdString = "";
            cmdString = "GetMasterDetailUserFrequencyList_TemplateRawData_03 " + ParamaterString_Raw_ThisUser(drpUserList.SelectedItem.Value) + ",15";
            ExportToExcel(cmdString, "DetailInventory2");
        }

        void btnErrorReporting_Click(object sender, EventArgs e)
        {
            string cmdString = "";
            string sIMEI = txtIMEI.Text;
            string BeginDateString = txtLogDateBegin.Text;
            string EndDateString = txtLogDateEnd.Text;
            string UserList = drpUserList.SelectedItem.Text;
            if (chkLogDate.Checked == false) { BeginDateString = ""; EndDateString = ""; }
            if (UserList.ToUpper() == "ALL") { UserList = ""; }
            cmdString = "GetMasterDetailErrorReporting '" + sIMEI + "','" + BeginDateString + "','" + EndDateString + "','" + UserList + "','',''";
            ExportToExcel(cmdString, "ErrorReportingList");
        }















        /// <summary>
        /// /////////////////////////////////////////////////////////////
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        void btnItemsInLocation_Click(object sender, EventArgs e)
        {
            string cmdString = "";
            cmdString = "GetMasterLocationUnitHistoryList_TemplateRawData_01_Summary_02 " + ParamaterString_Raw() + "," + drpLocationList.SelectedItem.Value + ",'n','n'";
            ExportToExcel(cmdString, "LocationSummary02");
        }
        void btnLocationHistoryReport_Click(object sender, EventArgs e)
        {
            string cmdString = "";
            cmdString = "GetLocationUnitHistory " + ParamaterString_Raw();
            ExportToExcel(cmdString, "UnitLocationHistory");
        }
        void btnUnitLocationSummary_Click(object sender, EventArgs e)
        {
            string cmdString = "";
            cmdString = "GetLocationUnitSummaryData " + ParamaterString_Raw();
            ExportToExcel(cmdString, "UnitLocationSummary");
        }


        //void btnLocationReport_Click(object sender, EventArgs e)
        //{
        //    string cmdString = "";
        //    cmdString = "GetMasterLocationHistoryList_TemplateRawData_01 " + ParamaterString_Raw() + "," + drpLocationList.SelectedItem.Value;
        //    ExportToExcel(cmdString, "LocationHistory");
        //}
        //void btnLocationUnitSummary_Click(object sender, EventArgs e)
        //{
        //    string cmdString = "";
        //    cmdString = "GetMasterLocationUnitHistoryList_TemplateRawData_01_Summary ";
        //    ExportToExcel(cmdString, "LocationUnitSummary");
        //}
        //void btnLocationUnitReport_Click(object sender, EventArgs e)
        //{
        //    string cmdString = "";
        //    cmdString = "GetMasterLocationUnitHistoryList_TemplateRawData_01 " + ParamaterString_Raw() + "," + drpLocationList.SelectedItem.Value;
        //    ExportToExcel(cmdString, "LocationHistory");
        //    //cmdString = "alert('" + cmdString.Replace("'","") + "');";
        //    //ScriptManager.RegisterStartupScript(this, GetType(), "RP", cmdString, true);
        //}





        /// <summary>
        /// /////////////////////////////////////////////////////////////////
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>



        void btnPreReceive_Click(object sender, EventArgs e)
        {
            string cmdString = "";
            string Parameters = "";
            string BeginDateString = txtBeginDate.Text;
            string EndDateString = txtEndDate.Text;
            if (chkReceived.Checked == false) { BeginDateString = ""; EndDateString = ""; }

            Parameters += "'" + txtIMEI.Text + "',";
            Parameters += "'" + txtDealerWayBill.Text + "',";
            Parameters += "'" + BeginDateString + "',";
            Parameters += "'" + EndDateString + "'";

            cmdString = "GetMasterPreReceive_TemplateRawData_01 " + Parameters;
            ExportToExcel(cmdString, "PreReceiveList");
        }


        void btnAvailableStockLocation_Click(object sender, EventArgs e)
        {
            string cmdString = "";
            cmdString = "GetMasterLocationAvailableUnits_TemplateRawData_01 " + ParamaterString_Raw() + "," + drpLocationList.SelectedItem.Value + ",1";
            ExportToExcel(cmdString, "AvailableStockLocation");
            //cmdString = "alert('" + cmdString.Replace("'","") + "');";
            //ScriptManager.RegisterStartupScript(this, GetType(), "RP", cmdString, true);
        }

        void btnAvailableStock_Click(object sender, EventArgs e)
        {
            string cmdString = "";
            cmdString = "GetMasterLocationAvailableUnits_TemplateRawData_01 " + ParamaterString_Raw() + "," + drpLocationList.SelectedItem.Value + ",0";
            ExportToExcel(cmdString, "AvailableStock");
            //cmdString = "alert('" + cmdString.Replace("'","") + "');";
            //ScriptManager.RegisterStartupScript(this, GetType(), "RP", cmdString, true);
        }

        void btnExportFullInventoryList_B_Click(object sender, EventArgs e)
        {
            string message = "";
            string cmdString = "";

            //if (txtIMEIVersion.Text.Length > 0)
            //{
            if (txtOrderEntryClientLocationKey.Text.Length > 0) { cmdString = "GetMasterDetailInventoryList_TemplateRawData_02Version " + ParamaterString_B_RawVersion() + "','" + txtOrderEntryClientLocationKey.Text + "'"; }
            else { cmdString = "GetMasterDetailInventoryList_TemplateRawData_02Version " + ParamaterString_B_RawVersion(); }
            //}
            //else
            //{
            //    if (txtOrderEntryClientLocationKey.Text.Length > 0) { cmdString = "GetMasterDetailInventoryList_TemplateRawData_01 " + ParamaterString_Raw() + "','" + txtOrderEntryClientLocationKey.Text + "'"; }
            //    else { cmdString = "GetMasterDetailInventoryList_TemplateRawData_01 " + ParamaterString_Raw(); }
            //}

            message = ExportToExcel(cmdString, "DetailInventory");
            if (message.Length > 0)
            {
                cmdString = "alert('" + message + "');";
                ScriptManager.RegisterStartupScript(this, GetType(), "RP", cmdString, true);
            }
        }
        void btnExportFullInventoryList_Click(object sender, EventArgs e)
        {
            string message = "";
            string cmdString = "";

            //if (txtIMEIVersion.Text.Length > 0)
            //{
            if (txtOrderEntryClientLocationKey.Text.Length > 0) { cmdString = "GetMasterDetailInventoryList_TemplateRawData_01Version " + ParamaterString_RawVersion() + "','" + txtOrderEntryClientLocationKey.Text + "'"; }
            else { cmdString = "GetMasterDetailInventoryList_TemplateRawData_01Version " + ParamaterString_RawVersion(); }
            //}
            //else
            //{
            //    if (txtOrderEntryClientLocationKey.Text.Length > 0) { cmdString = "GetMasterDetailInventoryList_TemplateRawData_01 " + ParamaterString_Raw() + "','" + txtOrderEntryClientLocationKey.Text + "'"; }
            //    else { cmdString = "GetMasterDetailInventoryList_TemplateRawData_01 " + ParamaterString_Raw(); }
            //}

            message = ExportToExcel(cmdString, "DetailInventory");
            if (message.Length > 0)
            {
                cmdString = "alert('" + message + "');";
                ScriptManager.RegisterStartupScript(this, GetType(), "RP", cmdString, true);
            }
        }
        void btnExportFullInventoryClientList_Click(object sender, EventArgs e)
        {


            string cmdString = "";
            if (txtOrderEntryClientLocationKey.Text.Length > 0) { cmdString = "GetMasterDetailInventoryClientList_TemplateRawData_01Version " + ParamaterString_RawVersion() + "','" + txtOrderEntryClientLocationKey.Text + "'"; }
            else { cmdString = "GetMasterDetailInventoryClientList_TemplateRawData_01Version " + ParamaterString_RawVersion(); }
            //if (txtOrderEntryClientLocationKey.Text.Length > 0) { cmdString = "GetMasterDetailInventoryClientList_TemplateRawData_01 " + ParamaterString_Raw() + "','" + txtOrderEntryClientLocationKey.Text + "'"; }
            //else { cmdString = "GetMasterDetailInventoryClientList_TemplateRawData_01 " + ParamaterString_Raw(); }
            //cmdString = "GetMasterDetailInventoryClientList_TemplateRawData_01 " + ParamaterString_Raw();

            //clsLog Log;
            //Log = new clsLog(Server.MapPath("~"), "UtilityUpload_01_Log.txt", User.Identity.Name, System.Configuration.ConfigurationManager.AppSettings["WriteLogUserNameToLog"]);
            //Log.writeLogData = true;
            //Log.LogIt("Location = " + System.Configuration.ConfigurationManager.AppSettings["WriteLogUserNameToLog"]);
            //Log.LogIt("btnExportFullInventoryClientList_Click: Report Parameters");
            //Log.LogIt(cmdString);
            //Log.LogIt("-----------------------------------------------------------");

            ExportToExcel(cmdString, "DetailClientInventory");
        }

        void btnDeviceErrorReport_Click(object sender, EventArgs e)
        {
            //string Parameters = "";
            //foreach (ListItem i in chkIFSParameters.Items)
            //{
            //    if (i.Selected == true)
            //    {
            //        Parameters += i.Value.ToString();
            //    }
            //}
            string cmdString = "";
            cmdString = "GetReport_IFSDeviceSummaryErrorDetail";
            ExportToExcel(cmdString, "IFSDeviceError");
        }

        void btnDeviceSummaryIFS_Click(object sender, EventArgs e)
        {
            string Parameters = "";
            foreach (ListItem i in chkIFSParameters.Items)
            {
                if (i.Selected == true)
                {
                    Parameters += i.Value.ToString();
                }
            }

            string cmdString = "";
            if (chkShowRollupLocations.Checked == true)
            {
                if (txtDeviceSummarySkuOnly.Text.Length == 0)
                {

                    cmdString = "GetReport_IFSDeviceSummaryRollup '" + Parameters + "'";
                }
                else
                {
                    string Parameters2 = PastedD(PasteDeliminatorSkuOnly, txtDeviceSummarySkuOnly);
                    cmdString = "GetReport_IFSDeviceSummary_TheseSKUsRollup '" + Parameters + "'," + Parameters2 + "'";
                }

            }
            else
            {
                if (txtDeviceSummarySkuOnly.Text.Length == 0)
                {

                    cmdString = "GetReport_IFSDeviceSummary '" + Parameters + "'";
                }
                else
                {
                    string Parameters2 = PastedD(PasteDeliminatorSkuOnly, txtDeviceSummarySkuOnly);
                    cmdString = "GetReport_IFSDeviceSummary_TheseSKUs '" + Parameters + "'," + Parameters2 + "'";
                }
            }
            ExportToExcel(cmdString, "IFSDeviceSummary");
        }
        void btnPartSummaryIFS_Click(object sender, EventArgs e)
        {
            string Parameters = "";
            foreach (ListItem i in chkIFSParameters.Items)
            {
                if (i.Selected == true)
                {
                    Parameters += i.Value.ToString();
                }
            }
            string cmdString = "";
            cmdString = "GetReport_IFSPartSummary '" + Parameters + "'";
            if (chkShowRollupLocations.Checked == true)
            {
                cmdString = "GetReport_IFSPartSummaryRollup '" + Parameters + "'";
            }
            ExportToExcel(cmdString, "IFSPartsSummary");
        }


        void btnPartLocAnalyze_Click(object sender, EventArgs e)
        {
            string Parameters = txtDeviceLocAnalyze.Text;
            if (Parameters.Length == 0) { return; }
            string cmdString = "";
            cmdString = "Report_AnalysisPartLocation '" + Parameters + "'";
            ExportToExcel(cmdString, "Report_IFSAnalysisPartLocation");
        }
        void btnPartLocAnalyzeDetail_Click(object sender, EventArgs e)
        {
            //string Parameters = txtDeviceLocAnalyze.Text;
            //if (Parameters.Length == 0) { return; }
            //string cmdString = "";
            //cmdString = "Report_AnalysisPartLocationDetail '" + Parameters + "'";
            //ExportToExcel(cmdString, "Report_IFSAnalysisPartLocation");
        }
        void btnDeviceLocAnalyze_Click(object sender, EventArgs e)
        {
            string Parameters = txtDeviceLocAnalyze.Text;
            if (Parameters.Length == 0) { return; }
            string cmdString = "";
            cmdString = "Report_AnalysisDeviceLocation '" + Parameters + "'";
            ExportToExcel(cmdString, "Report_IFSAnalysisDeviceLocation");
        }
        void btnDeviceLocAnalyzeDetail_Click(object sender, EventArgs e)
        {
            string Parameters = txtDeviceLocAnalyze.Text;
            if (Parameters.Length == 0) { return; }
            string cmdString = "";
            cmdString = "Report_AnalysisLocationMoves '" + Parameters + "'";
            ExportToExcel(cmdString, "Report_IFSAnalysisDeviceLocation");
        }


        void btnGetIFS_GatherLogSummary_Click(object sender, EventArgs e)
        {
            string Parameters = txtIFSBatchList.Text;
            if (Parameters.Length == 0) { return; }
            string cmdString = "";
            cmdString = "Report_IFS_GatherLogSummary '" + Parameters + "'";
            ExportToExcel(cmdString, "Report_IFS_GatherLogSummary");
        }





        void btnPartSKUAnalyze_Click(object sender, EventArgs e)
        {
            string Parameters = txtDeviceSKUAnalyze.Text;
            if (Parameters.Length == 0) { return; }
            string cmdString = "";
            cmdString = "Report_AnalysisSKUPartNum '" + Parameters + "'";
            ExportToExcel(cmdString, "Report_IFSAnalysisSKU");
        }



        void btnPartSKUAnalyzeeDetail_Click(object sender, EventArgs e)
        {
            //string Parameters = txtDeviceSKUAnalyze.Text;
            //if (Parameters.Length == 0) { return; }
            //string cmdString = "";
            //cmdString = "Report_AnalysisSKUPartNumDetail '" + Parameters + "'";
            //ExportToExcel(cmdString, "Report_IFSAnalysisSKU");
        }


        void btnDeviceSKUAnalyze_Click(object sender, EventArgs e)
        {
            string Parameters = txtDeviceSKUAnalyze.Text;
            if (Parameters.Length == 0) { return; }
            string cmdString = "";
            cmdString = "Report_AnalysisSKU '" + Parameters + "'";
            ExportToExcel(cmdString, "Report_IFSAnalysisSKU");
        }

        void btnDeviceSKUAnalyzeeDetail_Click(object sender, EventArgs e)
        {
            string Parameters = txtDeviceSKUAnalyze.Text;
            if (Parameters.Length == 0) { return; }
            string cmdString = "";
            cmdString = "Report_AnalysisSKUDetail '" + Parameters + "'";
            ExportToExcel(cmdString, "Report_IFSAnalysisSKU");
        }



        void btnPasteParse_Click(object sender, EventArgs e)
        {
            if (txtPasteParse.Text.Length == 0) { return; }
            string Parameters2 = PastedD(PasteDeliminator, txtPasteParse);
            string ParametersCondition = PastedD(PasteDeliminatorCondition, txtPasteParseCondition);
            string Site = rdbSite.SelectedItem.Value;

            ////char Delimitor = ',';
            ////if (PasteDeliminator.SelectedItem.Value.ToUpper() == "SPACE") { Delimitor = ' '; }
            //if (txtPasteParse.Text.Length > 0)
            //{
            //    if (PasteDeliminator.SelectedItem.Value.ToUpper() == "EXCEL")
            //    {
            //        //List<string> data = txtPasteParse.Text.Split(new string[] { "\r\n", "\n", " " }, StringSplitOptions.RemoveEmptyEntries).ToList();
            //        List<string> data = txtPasteParse.Text.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            //        foreach (string x in data) { if (x.Length > 0) { Parameters2 += ((Parameters2.Length > 0) ? "," : "") + x.Trim(); } }
            //    }
            //    if (PasteDeliminator.SelectedItem.Value.ToUpper() == "SPACE")
            //    {
            //        List<string> data = txtPasteParse.Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            //        foreach (string x in data) { if (x.Length > 0) { Parameters2 += ((Parameters2.Length > 0) ? "," : "") + x.Trim(); } }
            //    }
            //    if (PasteDeliminator.SelectedItem.Value.ToUpper() == "COMMA")
            //    {
            //        List<string> data = txtPasteParse.Text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            //        foreach (string x in data) { if (x.Length > 0) { Parameters2 += ((Parameters2.Length > 0) ? "," : "") + x.Trim(); } }
            //    }
            //}
            string cmdString = "";
            //cmdString = "GetDevicesInTheseLocation '" + Parameters2 + "'";
            cmdString = "GetDevicesInTheseLocationCondition '" + Site + "','" + Parameters2 + "','" + ParametersCondition + "'";
            ExportToExcel(cmdString, "DeviceLocations");
        }

        string PastedD(RadioButtonList Deliminator, TextBox Parse)
        {
            string Parameters2 = "";
            if (Parse.Text.Length > 0)
            {
                if (Deliminator.SelectedItem.Value.ToUpper() == "EXCEL")
                {
                    //List<string> data = txtPasteParse.Text.Split(new string[] { "\r\n", "\n", " " }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    List<string> data = Parse.Text.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    foreach (string x in data) { if (x.Length > 0) { Parameters2 += ((Parameters2.Length > 0) ? "," : "") + x.Trim(); } }
                }
                if (Deliminator.SelectedItem.Value.ToUpper() == "SPACE")
                {
                    List<string> data = Parse.Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    foreach (string x in data) { if (x.Length > 0) { Parameters2 += ((Parameters2.Length > 0) ? "," : "") + x.Trim(); } }
                }
                if (Deliminator.SelectedItem.Value.ToUpper() == "COMMA")
                {
                    List<string> data = Parse.Text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    foreach (string x in data) { if (x.Length > 0) { Parameters2 += ((Parameters2.Length > 0) ? "," : "") + x.Trim(); } }
                }
            }
            return Parameters2;
        }
        string PastedDWithQuotes(RadioButtonList Deliminator, TextBox Parse)
        {
            string Parameters2 = "";
            if (Parse.Text.Length > 0)
            {
                if (Deliminator.SelectedItem.Value.ToUpper() == "EXCEL")
                {
                    //List<string> data = txtPasteParse.Text.Split(new string[] { "\r\n", "\n", " " }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    List<string> data = Parse.Text.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    foreach (string x in data) { if (x.Length > 0) { Parameters2 += ((Parameters2.Length > 0) ? ",'" : "'") + x.Trim() + "'"; } }
                }
                if (Deliminator.SelectedItem.Value.ToUpper() == "SPACE")
                {
                    List<string> data = Parse.Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    foreach (string x in data) { if (x.Length > 0) { Parameters2 += ((Parameters2.Length > 0) ? ",'" : "'") + x.Trim() + "'"; } }
                }
                if (Deliminator.SelectedItem.Value.ToUpper() == "COMMA")
                {
                    List<string> data = Parse.Text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    foreach (string x in data) { if (x.Length > 0) { Parameters2 += ((Parameters2.Length > 0) ? ",'" : "'") + x.Trim() + "'"; } }
                }
            }
            return Parameters2;
        }

        void btnPasteParseIMEIList_Click(object sender, EventArgs e)
        {
            string Parameters2 = PastedDWithQuotes(PasteDeliminatorIMEIList, txtPasteParseIMEIList);
            //string Parameters = txtIFSBatchNumber.Text;
            if (Parameters2.Length == 0) { return; }
            string cmdString = "";
            cmdString = "Select * from vwReport_IFS_InvtTran where ESN in (" + Parameters2 + ")";
            ExportToExcel(cmdString, "DeviceIFSData");
        }


        void btnIFSBatchDetailDataIFS_Click(object sender, EventArgs e)
        {
            string Parameters = txtIFSBatchNumber.Text;
            if (Parameters.Length == 0) { return; }
            string cmdString = "";
            cmdString = "Select * from vwReport_IFS_InvtTran where RetrievedBatch = " + Parameters;
            ExportToExcel(cmdString, "DevicePOIFSData");
        }


        void btnRMASummary_Click(object sender, EventArgs e)
        {
            string Parameters = "'" + txtBeginDate25.Text + "',";
            Parameters += "'" + txtEndDate25.Text + "'";
            string cmdString = "";
            cmdString = "Exec GetRMASummary " + Parameters;
            ExportToExcel(cmdString, "RMASummary");
        }



        void btnDetailInventory25_Click(object sender, EventArgs e)
        {
            string Parameters = "'" + txtSupplier25.Text + "',";
            Parameters += "'" + txtRMANumber25.Text + "',";
            Parameters += "'" + txtProjectTag25.Text + "',";
            Parameters += "'" + txtPONumber25.Text + "',";
            Parameters += "'" + txtSONumber25.Text + "',";
            Parameters += "'" + txtBeginDate25.Text + "',";
            Parameters += "'" + txtEndDate25.Text + "',";
            Parameters += "'" + txtBeginShipped25.Text + "',";
            Parameters += "'" + txtEndShipped25.Text + "',";
            if (chkShowGraveyard25.Checked == true) { Parameters += "'" + "Y" + "',"; }
            else { Parameters += "'" + "N" + "',"; }
            Parameters += "'" + User.Identity.Name + "'";

            if (Parameters.Length == 0) { return; }
            string cmdString = "";
            cmdString = "Exec GetReport_Devices " + Parameters;
            ExportToExcel(cmdString, "DeviceData");
        }

        void btnReportByBin_Click(object sender, EventArgs e)
        {
            string Parameters = "'" + txtBinNumberx.Text + "'";
            if (Parameters.Length == 0) { return; }
            string cmdString = "";
            cmdString = "Exec GetReport_DevicesBIN " + Parameters;
            ExportToExcel(cmdString, "DeviceData");
        }

        void btnReportXBINXKey_Click(object sender, EventArgs e)
        {
            string Parameters = "'" + txtBinNumberx.Text + "'";
            if (Parameters.Length == 0) { return; }
            string cmdString = "";
            cmdString = "Exec GetReport_DevicesXBINXKey " + Parameters;
            ExportToExcel(cmdString, "XBINXDeviceData");
        }


        // Exec GetReport_Devices 'txtSupplier25','@mRMANumber','@mProjectTag','@mPONumber'
        //,'@mSONumber','@mReceiveBeginDate','@mReceiveEndDate',
        //'@mShippedBeginDate','@mShippedEndDate','Y','@mUserName'


        void btnPurchaseOrderDetailDataIFS_Click(object sender, EventArgs e)
        {
            string Parameters = txtPurchaseOrderNumber.Text;
            if (Parameters.Length == 0) { return; }
            string cmdString = "";
            cmdString = "Select * from vwReport_IFS_InvtTran where Directive = 10 and PONumber = '" + Parameters + "'";
            ExportToExcel(cmdString, "DevicePOIFSData");
        }
        void btnPurchaseOrderDetailData_Click(object sender, EventArgs e)
        {
            string Parameters = txtPurchaseOrderNumber.Text;
            if (Parameters.Length == 0) { return; }
            string cmdString = "";
            cmdString = "Select * from vwReceiveDetailPOData Where PONumber = '" + Parameters + "'";
            ExportToExcel(cmdString, "DevicePurchaseOrderEntryData");
        }
        void btnPurchaseOrderData_Click(object sender, EventArgs e)
        {
            string Parameters = txtPurchaseOrderNumber.Text;
            if (Parameters.Length == 0) { return; }
            string cmdString = "";
            cmdString = "Select * from vwIFSPurchaseOrderData Where PONumber = '" + Parameters + "'";
            ExportToExcel(cmdString, "DevicePurchaseOrderEntryData");
        }

        void btnOrderEntryDetailDataIFS_Click(object sender, EventArgs e)
        {
            string Parameters = txtOrderEntryNo.Text;
            if (Parameters.Length == 0) { return; }
            string cmdString = "";
            cmdString = "Select * from vwReport_IFS_InvtTran where Directive = 9 and PONumber = '" + Parameters + "'";
            ExportToExcel(cmdString, "DeviceOrderEntryData");
        }
        void btnOrderEntryDetailData_Click(object sender, EventArgs e)
        {
            string Parameters = txtOrderEntryNo.Text;
            if (Parameters.Length == 0) { return; }
            string cmdString = "";
            cmdString = "Select * from vwReceiveDetailSOData Where OrderNumber = '" + Parameters + "'";
            ExportToExcel(cmdString, "DeviceOrderEntryData");
        }
        void btnOrderEntryData_Click(object sender, EventArgs e)
        {
            string Parameters = txtOrderEntryNo.Text;
            if (Parameters.Length == 0) { return; }
            string cmdString = "";
            cmdString = "Select * from vwIFSSalesOrderData Where OrderNumber = '" + Parameters + "'";
            ExportToExcel(cmdString, "OrderEntryData");
        }

        void btnRawData_Click(object sender, EventArgs e)
        {
            string cmdString = "";
            cmdString = "GetMasterDetailInventoryClientList_RawData " + ParamaterString_Raw();
            ExportToExcel(cmdString, "InventoryRawData");
        }
        void btnExportDetailInventoryClientListBatched_Click(object sender, EventArgs e)
        {
            string cmdString = "";
            cmdString = "GetMasterDetailInventoryClientList_TemplateRawData_01_IMEIBatch";
            ExportToExcel(cmdString, "IMEIBatch_DetailClientInventory");
        }

        void btnBinStorage_Click(object sender, EventArgs e)
        {
            if (txtBinNumber.Text.Length == 0) { return; }
            string cmdString = "";
            cmdString = "GetReport_BinStorage";
            string BeginDateString = txtBeginDate.Text;
            if (chkReceived.Checked == false) { BeginDateString = string.Format("{0}", string.Format("{0:MM/dd/yyyy}", DateTime.Today.AddDays(-1))); }
            cmdString += " '" + txtBinNumber.Text + "','" + BeginDateString + "'";
            ExportToExcel(cmdString, "BinStorageRPT");
        }

        void btnBinStorageGrand_Click(object sender, EventArgs e)
        {
            if (txtBinNumber.Text.Length == 0) { return; }
            // GetReport_BinStorage_Freq '003', '03/01/2013', '03/30/2013',0,0
            string cmdString = "";
            cmdString = "GetReport_BinStorage_Freq_B";
            string BeginDateString = txtBeginDate.Text;
            string EndDateString = txtEndDate.Text;
            if (chkReceived.Checked == false) { BeginDateString = string.Format("{0}", string.Format("{0:MM/dd/yyyy}", DateTime.Today.AddDays(-1))); }
            if (chkReceived.Checked == false) { EndDateString = string.Format("{0}", string.Format("{0:MM/dd/yyyy}", DateTime.Today.AddDays(-1))); }
            cmdString += " '" + txtBinNumber.Text + "','" + BeginDateString + "','" + EndDateString + "',0,0";
            ExportToExcel(cmdString, "BinStorageGrand");
        }

        void btnProcessWaitTime_Click(object sender, EventArgs e)
        {

            // GetReport_BinStorage_Freq '003', '03/01/2013', '03/30/2013',0,0
            string cmdString = "";
            cmdString = "GetProcessWaitTime_CurrentProcess_01";
            string ProjectName = drpProjectList.SelectedItem.Text;
            string ClientName = "";
            string RMANumber = "";
            string BinNumber = "";
            string ProjectTag = "";
            if (ProjectName.ToUpper() == "ALL")
            {
                cmdString = "alert('This report required you to select a project.');";
                ScriptManager.RegisterStartupScript(this, GetType(), "RP", cmdString, true);
                return;
            }

            //string BeginDateString = txtBeginDate.Text;
            //string EndDateString = txtEndDate.Text;
            //if (chkReceived.Checked == false) { BeginDateString = string.Format("{0}", string.Format("{0:MM/dd/yyyy}", DateTime.Today.AddDays(-1))); }
            //if (chkReceived.Checked == false) { EndDateString = string.Format("{0}", string.Format("{0:MM/dd/yyyy}", DateTime.Today.AddDays(-1))); }
            cmdString += " '" + ProjectName + "','" + ClientName + "','" + RMANumber + "','" + BinNumber + "','" + ProjectTag + "'";
            ExportToExcel(cmdString, "ProcessWaitTime");
        }



        //void btnTest_Click(object sender, EventArgs e)
        //{
        //    //if (txtBinNumber.Text.Length == 0) { return; }
        //    //// GetReport_BinStorage_Freq '003', '03/01/2013', '03/30/2013',0,0
        //    //string cmdString = "";
        //    //cmdString = "GetReport_BinStorage_Freq_B";
        //    //string BeginDateString = txtBeginDate.Text;
        //    //string EndDateString = txtEndDate.Text;
        //    //if (chkReceived.Checked == false) { BeginDateString = string.Format("{0}", string.Format("{0:MM/dd/yyyy}", DateTime.Today.AddDays(-1))); }
        //    //if (chkReceived.Checked == false) { EndDateString = string.Format("{0}", string.Format("{0:MM/dd/yyyy}", DateTime.Today.AddDays(-1))); }
        //    //cmdString += " '" + txtBinNumber.Text + "','" + BeginDateString + "','" + EndDateString + "',0,0";
        //    //ExportToExcel(cmdString, "BinStorageGrandTest");
        //}

        //void btnxx_Click(object sender, EventArgs e)
        //{
        //    //if (txtBinNumber.Text.Length == 0) { return; }
        //    //// GetReport_BinStorage_Freq '003', '03/01/2013', '03/30/2013',0,0
        //    //string cmdString = "";
        //    //cmdString = "GetReport_BinStorage_Freq_Test";
        //    //string BeginDateString = txtBeginDate.Text;
        //    //string EndDateString = txtEndDate.Text;
        //    //if (chkReceived.Checked == false) { BeginDateString = string.Format("{0}", string.Format("{0:MM/dd/yyyy}", DateTime.Today.AddDays(-1))); }
        //    //if (chkReceived.Checked == false) { EndDateString = string.Format("{0}", string.Format("{0:MM/dd/yyyy}", DateTime.Today.AddDays(-1))); }
        //    //cmdString += " '" + txtBinNumber.Text + "','" + BeginDateString + "','" + EndDateString + "',0,0";
        //    //ExportToExcel(cmdString, "BinStorageGrandTest");
        //}
        void btnBinStorageSummary_Click(object sender, EventArgs e)
        {
            // GetReport_BinStorage_Freq '003', '03/01/2013', '03/30/2013',1,0
            if (txtBinNumber.Text.Length == 0) { return; }
            string cmdString = "";
            cmdString = "GetReport_BinStorage_Freq_B";
            string BeginDateString = txtBeginDate.Text;
            string EndDateString = txtEndDate.Text;
            if (chkReceived.Checked == false) { BeginDateString = string.Format("{0}", string.Format("{0:MM/dd/yyyy}", DateTime.Today.AddDays(-1))); }
            if (chkReceived.Checked == false) { EndDateString = string.Format("{0}", string.Format("{0:MM/dd/yyyy}", DateTime.Today.AddDays(-1))); }
            cmdString += " '" + txtBinNumber.Text + "','" + BeginDateString + "','" + EndDateString + "',1,0";
            ExportToExcel(cmdString, "BinStorageSummary");
        }
        void btnBinStorageDetail_Click(object sender, EventArgs e)
        {
            // GetReport_BinStorage_Freq '003', '03/01/2013', '03/30/2013',2,0
            if (txtBinNumber.Text.Length == 0) { return; }
            string cmdString = "";
            cmdString = "GetReport_BinStorage_Freq_B";
            string BeginDateString = txtBeginDate.Text;
            string EndDateString = txtEndDate.Text;
            if (chkReceived.Checked == false) { BeginDateString = string.Format("{0}", string.Format("{0:MM/dd/yyyy}", DateTime.Today.AddDays(-1))); }
            if (chkReceived.Checked == false) { EndDateString = string.Format("{0}", string.Format("{0:MM/dd/yyyy}", DateTime.Today.AddDays(-1))); }
            cmdString += " '" + txtBinNumber.Text + "','" + BeginDateString + "','" + EndDateString + "',2,0";
            ExportToExcel(cmdString, "BinStorageDetail");
        }

        void btnTAT_Click(object sender, EventArgs e)
        {
            string cmdString = "";
            cmdString = "GetMasterDetailInventoryList_TemplateRawData_TAT " + ParamaterString_Raw();
            ExportToExcel(cmdString, "TAT_Inventory");

            //string cmdString = "";
            //cmdString = "GetMasterLocationHistoryList_TemplateRawData_01 " + ParamaterString_Raw() + "," + drpLocationList.SelectedItem.Value;
            //ExportToExcel(cmdString, "LocationHistory");
        }


        void btnExportDetailInventoryClientListAbriviated_Click(object sender, EventArgs e)
        {
            string cmdString = "";
            if (txtOrderEntryClientLocationKey.Text.Length > 0) { cmdString = "GetMasterDetailInventoryClientList_TemplateRawData_01_Abriviated " + ParamaterString_Raw() + "','" + txtOrderEntryClientLocationKey.Text + "'"; }
            else { cmdString = "GetMasterDetailInventoryClientList_TemplateRawData_01_Abriviated " + ParamaterString_Raw(); }
            //cmdString = "GetMasterDetailInventoryClientList_TemplateRawData_01_Abriviated " + ParamaterString_Raw();
            ExportToExcel(cmdString, "DetailClientInventoryAbriviated");
        }

        void btnExportDetailInventoryClientListAbriviatedB_Click(object sender, EventArgs e)
        {
            string cmdString = "";
            if (txtOrderEntryClientLocationKey.Text.Length > 0) { cmdString = "GetMasterDetailInventoryClientList_TemplateRawData_01_Abriviated_B " + ParamaterString_Raw() + "','" + txtOrderEntryClientLocationKey.Text + "'"; }
            else { cmdString = "GetMasterDetailInventoryClientList_TemplateRawData_01_Abriviated_B " + ParamaterString_Raw(); }
            //cmdString = "GetMasterDetailInventoryClientList_TemplateRawData_01_Abriviated_B " + ParamaterString_Raw();
            ExportToExcel(cmdString, "DetailClientInventoryAbriviated_B");
        }







        //void btnGenerateBilling_Click(object sender, EventArgs e)
        //{
        //    string message = "";
        //    string cmdString = "";
        //    cmdString = "GetMasterDetailInventoryClientList_TemplateRawData_01_Abriviated_ToTable " + ParamaterString_Raw();
        //    SqlConnection cn = new SqlConnection(ConnectionString);
        //    SqlCommand cmd = new SqlCommand();
        //    List<int> o = new List<int>();
        //    try
        //    {
        //        AccessControlIDList ValidClientLocationIDs = null;
        //        AccessControlIDList ValidProjectIDs = null;
        //        using (clsLinqDataContext ctx = new clsLinqDataContext())
        //        {
        //            ValidClientLocationIDs = new AccessControlIDList(User.Identity.Name, "Client", ctx);
        //            ValidProjectIDs = new AccessControlIDList(User.Identity.Name, "Project", ctx);
        //        }
        //        message = ""; // "start:" + DateTime.Now.ToString() + Environment.NewLine;
        //        cmdString = cmdString.Replace('~', '\'');
        //        cmd.CommandText = cmdString;
        //        cmd.CommandTimeout = 240;
        //        cmd.Connection = cn;
        //        cn.Open();
        //        message += "Job Started:" + DateTime.Now.ToString() + Environment.NewLine;
        //        int Result = cmd.ExecuteNonQuery();
        //        message += "Ended:" + DateTime.Now.ToString() + Environment.NewLine;
        //        //message += "record effected:" + Result.ToString() + Environment.NewLine;

        //        // move the file out to the browser.
        //    }
        //    catch (Exception ex)
        //    {
        //        message += "----------------------------------------" + Environment.NewLine;
        //        message += "Error:" + DateTime.Now.ToString() + Environment.NewLine;
        //        message += ex.Message;
        //    }
        //    finally
        //    {
        //        cmd.Connection.Close();
        //        cn.Close();
        //    }
        //    rMessage.Text = message;
        //}
        //void btnDownloadBilling_Click(object sender, EventArgs e)
        //{
        //    string cmdString = "";
        //    cmdString = "Select * from ReceiveDetailBillingReport";
        //    ExportToExcel(cmdString, "DetailClientInventoryAbriviated");
        //}


        void btnExportPartNumberInventory_Click(object sender, EventArgs e)
        {
            string cmdString = "";
            cmdString = "GetMasterPartNumberInventoryList_TemplateRawData_01 " + ParamaterString_Raw();
            ExportToExcel(cmdString, "PartnumberInventory");
        }

        void btnExportDetailInventoryProcessingLog_Click(object sender, EventArgs e)
        {
            string cmdString = "";
            cmdString = "GetMasterDetailInventoryList_ProcessLog " + ParamaterString_Raw();
            ExportToExcel(cmdString, "DetailInventory");
        }


        void btnExportStatisticalList_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            string cmdString = "";
            cmdString = "GetMasterStatistical_SummarizedRawData_01 " + ParamaterString_Raw(txtUsername.Text);
            ExportToExcel(cmdString, b.CommandName);
        }
        void btnExportStatisticalRawList_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            string cmdString = "";
            cmdString = "GetMasterStatistical_TemplateRawData_01 " + ParamaterString_Raw(txtUsername.Text);
            ExportToExcel(cmdString, b.CommandName);
        }
        void btnExportStatisticalBucketList_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            string cmdString = "";
            cmdString = "GetMasterStatistical_SummarizedRawBucketData_02 " + ParamaterString_BucketRaw(txtUsername.Text);
            ExportToExcel(cmdString, b.CommandName);
        }
        void btnExportStatisticalRawBucketList_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            string cmdString = "";
            cmdString = "GetMasterStatistical_TemplateRawBucketData_02 " + ParamaterString_BucketRaw(txtUsername.Text);
            ExportToExcel(cmdString, b.CommandName);
        }

        void btnStatisticalBucketDaily_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            string cmdString = "";
            cmdString = "GetMasterStatistical_SummarizedDailyBucketData_02 " + ParamaterString_BucketRaw(txtUsername.Text);
            ExportToExcel(cmdString, b.CommandName);
        }

        void btnMasterCarrier_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            string cmdString = "";
            cmdString = "ReportCarrierManufacturerMasterCompare ";
            ExportToExcel(cmdString, b.CommandName);
        }

        void btnMasterCarrierFreq_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            string cmdString = "";
            cmdString = "Report_MasterCarrierManufacturerLookupFrequency ";
            ExportToExcel(cmdString, b.CommandName);
        }



        void btnAttributeHistory_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            string cmdString = "";
            if (txtIMEIVersion.Text.Length != 0) { txtIMEIVersion.Text = "000"; }
            cmdString = "GetReceiveDetail_AttributeHistory '" + txtIMEI.Text + "','" + txtIMEIVersion.Text + "'";
            ExportToExcel(cmdString, b.CommandName);
        }


        void btnClientQuestionFreq_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            string cmdString = "";
            string sClientID = drpClientList.SelectedItem.Value.ToString();
            cmdString = "GetReport_ClientProjectQuestionUsage " + sClientID + "";
            ExportToExcel(cmdString, b.CommandName);
        }


        void btnPartsInventoryTransaction_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            string cmdString = "";
            string sClientID = drpClientList.SelectedItem.Value.ToString();
            cmdString = "GetReport_MasterPartsInventoryTransactions";
            ExportToExcel(cmdString, b.CommandName);
            //ExportToExcel(cmdString, b.CommandName);
        }




        void btnBucketCounters_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            string cmdString = "";
            cmdString = "Report_MasterBucketTransactions ";
            ExportToExcel(cmdString, b.CommandName);
        }


        void btnExportDockingReport_Click(object sender, EventArgs e)
        {
            string cmdString = "";
            cmdString = "GetMasterDockingList_01 ";
            cmdString += " '" + txtDealerWayBill.Text + "',";
            cmdString += ParamaterString_Raw();
            ExportToExcel(cmdString, "MasterDockingList");
        }




        #region IMEIBulk

        protected void btnUploadIMEIBatch_Click(object sender, EventArgs e)
        {
            string PathName = "~/IDAutomation";
            string PathAndName = "";
            PathAndName = UploadFile(PathName, FileUploadIMEIBatch, lblMsgIMEIBatch);
            if (PathAndName.Length > 0)
            {
                btnParseIMEIBatchUpload(PathAndName);
                lblMsgIMEIBatch.Text = "File Parsed and Uploaded!";
                lblMsgIMEIBatch.ForeColor = System.Drawing.Color.Green;
                lblMsgIMEIBatch.Visible = true;
                System.IO.File.Delete(Server.MapPath(PathAndName));
            }
        }


        protected void btnParseIMEIBatchUpload(string PathAndName)
        {
            // ClientLocationManager clm = new ClientLocationManager(User.Identity.Name);

            ReceiveDetailManager rdm = new ReceiveDetailManager(User.Identity.Name);
            ExcelEngine excelEngine = new ExcelEngine();
            IApplication application = excelEngine.Excel;
            application.DefaultVersion = GetExcelVersion();
            IWorkbook workbook = application.Workbooks.Open(Page.MapPath(PathAndName), ExcelOpenType.Automatic);
            IWorksheet sheet = workbook.Worksheets[0];

            string IMEI = "";
            int Row = 2;

            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                // Delete everything that is there now.
                ctx.GetMasterDetailInventoryClientList_IMEIBatches.DeleteAllOnSubmit(ctx.GetMasterDetailInventoryClientList_IMEIBatches.Select(x => x));
                ctx.SubmitChanges();
                // Add any new ones
                while (sheet.Range[Row, 1].Value != null && sheet.Range[Row, 1].Value.Length > 0)          // Scankey 
                {
                    IMEI = sheet.Range[Row, 1].Value == null ? "" : sheet.Range[Row, 1].Value;
                    if (IMEI.Length > 0)
                    {
                        GetMasterDetailInventoryClientList_IMEIBatch data = new GetMasterDetailInventoryClientList_IMEIBatch();
                        data.Createuser = User.Identity.Name;
                        data.IMEI = IMEI;
                        ctx.GetMasterDetailInventoryClientList_IMEIBatches.InsertOnSubmit(data);
                    }
                    Row++;
                }
                ctx.SubmitChanges();
            }
            //workbook.SaveAs("BillingPoint_Uploaded.xlsx", Page.Response, ExcelDownloadType.Open);
            workbook.Close();
            excelEngine.Dispose();

        }

        #endregion


        #region Billing
        #region Billing Upload
        protected void btnUploadBilling_Click(object sender, EventArgs e)
        {
            string PathName = "~/IDAutomation";
            string PathAndName = "";
            PathAndName = UploadFile(PathName, FileUploadBilling, lblMsgBilling);
            if (PathAndName.Length > 0)
            {
                btnParseBillingUpload(PathAndName);
                lblMsgBilling.Text = "File Parsed and Updated!";
                lblMsgBilling.ForeColor = System.Drawing.Color.Green;
                lblMsgBilling.Visible = true;
                System.IO.File.Delete(Server.MapPath(PathAndName));
            }
        }
        protected void btnParseBillingUpload(string PathAndName)
        {
            // ClientLocationManager clm = new ClientLocationManager(User.Identity.Name);

            ReceiveDetailManager rdm = new ReceiveDetailManager(User.Identity.Name);
            ExcelEngine excelEngine = new ExcelEngine();
            IApplication application = excelEngine.Excel;
            application.DefaultVersion = GetExcelVersion();
            IWorkbook workbook = application.Workbooks.Open(Page.MapPath(PathAndName), ExcelOpenType.Automatic);
            IWorksheet sheet = workbook.Worksheets[0];

            int ActionColumn = -1;
            int ReceiveDetailBillingPointsIDColumn = -1;
            int GMPInvoiceColumn = -1;
            int GMPInvoiceDateColumn = -1;
            int ErrorColumn = -1;

            string Action = "";
            string sReceiveDetailBillingPointsID = "";
            string sGMPInvoice = "";
            string sGMPInvoiceDate = "";

            decimal ReceiveDetailBillingPointsID = -1;
            DateTime? GMPInvoiceDate = null;
            DateTime dt;

            int Row = 1;
            int Column = 1;

            while (sheet.Range[Row, Column].Text != null && sheet.Range[Row, Column].Text.Length > 0)
            {
                if (sheet.Range[Row, Column].Text == "Action") { ActionColumn = Column; }
                if (sheet.Range[Row, Column].Text == "ReceiveDetailBillingPointsID") { ReceiveDetailBillingPointsIDColumn = Column; }
                if (sheet.Range[Row, Column].Text == "GMPInvoice") { GMPInvoiceColumn = Column; }
                if (sheet.Range[Row, Column].Text == "GMPInvoiceDate") { GMPInvoiceDateColumn = Column; }
                Column++;
            }
            ErrorColumn = Column;
            sheet.Range[Row, ErrorColumn].Text = "Status Message";

            Row = 2;
            while (sheet.Range[Row, ReceiveDetailBillingPointsIDColumn].Value != null && sheet.Range[Row, ReceiveDetailBillingPointsIDColumn].Value.Length > 0)          // Scankey 
            {
                Action = (sheet.Range[Row, ActionColumn].Value == null ? "" : sheet.Range[Row, ActionColumn].Value);
                sReceiveDetailBillingPointsID = (sheet.Range[Row, ReceiveDetailBillingPointsIDColumn].Value == null ? "" : sheet.Range[Row, ReceiveDetailBillingPointsIDColumn].Value);
                sGMPInvoice = (sheet.Range[Row, GMPInvoiceColumn].Value == null ? "" : sheet.Range[Row, GMPInvoiceColumn].Value);
                sGMPInvoiceDate = (sheet.Range[Row, GMPInvoiceDateColumn].Value == null ? "" : sheet.Range[Row, GMPInvoiceDateColumn].Value);

                if (decimal.TryParse(sReceiveDetailBillingPointsID, out ReceiveDetailBillingPointsID) == false) { ReceiveDetailBillingPointsID = -1; }
                if (DateTime.TryParse(sGMPInvoiceDate, out dt) == false) { GMPInvoiceDate = null; } else { GMPInvoiceDate = dt; }

                sheet.Range[Row, ErrorColumn].Text = rdm.PostBillingPoint(Action, ReceiveDetailBillingPointsID, sGMPInvoice, GMPInvoiceDate, User.Identity.Name);
                Row++;
            }
            workbook.SaveAs("BillingPoint_Uploaded.xlsx", Page.Response, ExcelDownloadType.Open);
            workbook.Close();
            excelEngine.Dispose();

        }
        #endregion

        #region Export Billing Report
        void btnBilling_Click(object sender, EventArgs e)
        {
            ExportBillingToExcel();
        }
        private void ExportBillingToExcel()
        {
            string cmdString = "";
            string BeginDateString = txtBPStartDate.Text;
            string EndDateString = txtBPEndDate.Text;
            if (chkBillingpoint.Checked == false) { BeginDateString = ""; EndDateString = ""; }

            if (chkShowRecorded.Checked == true)
            {
                cmdString = "GetBillingPointTransactionBillingPoints_01 " + "'Y','" + BeginDateString + "','" + EndDateString + "'," + ParamaterString_Raw();
            }
            else
            {
                cmdString = "GetBillingPointTransactionBillingPoints_01 " + "'N','" + BeginDateString + "','" + EndDateString + "'," + ParamaterString_Raw();
            }
            ExportToExcel(cmdString, "BillingReport");
        }
        #endregion
        #endregion


        #region Excel Report

        private string ExportToExcel(SqlConnection cn, SqlCommand cmd, string fileName, List<string> fieldListToReport)
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
            if (System.Configuration.ConfigurationManager.AppSettings["WriteReportToLog"].ToUpper() == "TRUE")
            {
                log.UserNameToLog = "";
                log.writeLogData = true;
            }

            string message = "";
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand();
            scmd = scmd.Replace('~', '\'');
            cmd.CommandText = scmd;
            //cmd.CommandTimeout = 360;        //6 minutes   6 * 60 = 
            //cmd.CommandTimeout = 1200;       //20 minutes   20 * 60 = 
            cmd.CommandTimeout = SysUtil.ReportTimeOutInSeconds();
            cmd.Connection = cn;
            // if csv 
            log.LogIt("Report Started(" + fileName + "): " + scmd);
            if (chkCSV.Checked == true) { message = ExportCSVFile(cn, cmd, fileName, fieldListToReport); }
            else // or xls file
            {
                message = ExportXLSFile(cn, cmd, fileName, fieldListToReport);
            }
            log.writeLogData = false;
            log.LogIt("Report Ended(" + fileName + "): " + scmd);
            //
            return message;
        }
        //}
        //private string ExportToExcelBKUP(string scmd, string fileName, List<string> fieldListToReport)
        //{
        //    string message = "";
        //    string[] formatnumeric = {"TRANSACTIONQTY","TRANSACTIONUNITPRICE","QUANTITY","UNITPRICE","MONTHENDQTY", "MOVEDOUT","MOVEDIN","STARTING","ENDING","FREQIN","FREQOUT",
        //                              "REPAIR FEE", "REPAIR_FEE", "UNITPRICE01", "UNITPRICE02", "UNITPRICE03", "UNITPRICE04", "UNITPRICE05", "UNITPRICE06", "UNITPRICE07",
        //                              "UNITPRICE08", "UNITPRICE09", "UNITPRICE10", "TOTALUNITPRICE","REPAIR CAP", "DEVICECOST",
        //                              "INPROCESSSECONDS","INPROCESSMINUTES","INPROCESSHOURS","MINUTESTOYELLOW","MINUTESTORED", "FREQ"};
        //    List<int> formatnumericColumns = new List<int>();
        //    string[] formatDate = { "LASTUPDATEDATE", "MONTHENDDATE", "CREATEDATE", "RECEIVEDATE", "MOVEDDATE", "REPORTBEGINDATE", "REPORTENDDATE", "DATEMOVED", "DATEMOVEDOUT", "BININDATE", "BINOUTDATE" };
        //    List<int> formatDateColumns = new List<int>();


        //    SqlConnection cn = new SqlConnection(ConnectionString);
        //    SqlCommand cmd = new SqlCommand();
        //    List<int> o = new List<int>();
        //    try
        //    {
        //        AccessControlIDList ValidClientLocationIDs = null;
        //        AccessControlIDList ValidProjectIDs = null;
        //        using (clsLinqDataContext ctx = new clsLinqDataContext())
        //        {
        //            ValidClientLocationIDs = new AccessControlIDList(User.Identity.Name, "Client", ctx);
        //            ValidProjectIDs = new AccessControlIDList(User.Identity.Name, "Project", ctx);
        //        }
        //        message = "start:" + DateTime.Now.ToString() + Environment.NewLine;
        //        scmd = scmd.Replace('~', '\'');
        //        cmd.CommandText = scmd;
        //        cmd.CommandTimeout = 360;
        //        cmd.Connection = cn;
        //        cn.Open();

        //        // See if you can insert the CSV portion here if the csv checkbox is checked,
        //        //     Search CSV and go to ExportCSVFile to find the correct code to spit out csv records.

        //        //
        //        // otherwise send out the normal excel.

        //        int ClientLocationColumn = -1;
        //        int ProjectColumn = -1;
        //        message += "dbase opened:" + DateTime.Now.ToString() + Environment.NewLine;
        //        SqlDataReader dr = cmd.ExecuteReader();
        //        message += "data read:" + DateTime.Now.ToString() + Environment.NewLine;
        //        string fieldName = "";
        //        for (int count = 0; count < dr.FieldCount; count++)
        //        {
        //            fieldName = dr.GetName(count).ToUpper();
        //            if (formatnumeric.Contains(fieldName.ToUpper()) == true) { formatnumericColumns.Add(count); }
        //            if (formatDate.Contains(fieldName.ToUpper()) == true) { formatDateColumns.Add(count); }

        //            if (fieldName == "CLIENTLOCATIONID") { ClientLocationColumn = count; }
        //            if (fieldName == "PROJECTID") { ProjectColumn = count; }
        //            if (fieldListToReport.Count == 0)
        //            {
        //                //string ValueTypex = dr.GetFieldType(count).Name;
        //                o.Add(count);
        //            }
        //            else
        //            {
        //                if (fieldListToReport.Contains(dr.GetName(count)))
        //                {
        //                    //string ValueTypex = dr.GetFieldType(count).ToString();
        //                    o.Add(count);
        //                }
        //            }
        //        }


        //        ExcelEngine excelEngine = new ExcelEngine();
        //        IApplication application = excelEngine.Excel;
        //        application.DefaultVersion = GetExcelVersion();
        //        fileName = fileName.Replace(".xls", "");

        //        fileName = fileName + "." + GetExcelExtension();
        //        IWorkbook workbook = application.Workbooks.Create(1);
        //        //                workbook.Version = GetExcelVersion();
        //        IWorksheet sheet = workbook.Worksheets[0];
        //        int Row = 1;
        //        int Col = 1;
        //        int StartCol = Col;
        //        int StartRow = Row;
        //        lblMessage.Text = "";
        //        //Add Header    
        //        foreach (int count in o)
        //        {
        //            if (dr.GetName(count) != null)
        //            {
        //                sheet.Range[Row, Col].Text = dr.GetName(count);
        //            }
        //            ++Col;
        //        }

        //        string Value = "";
        //        double nValue = 0;
        //        DateTime dValue = DateTime.Now;
        //        while (dr.Read())
        //        {
        //            Col = 1;
        //            if ((ClientLocationColumn == -1 || ValidClientLocationIDs.GlobalSelect == true || ValidClientLocationIDs.IDs.Contains(dr.GetDecimal(ClientLocationColumn))) &&
        //                (ProjectColumn == -1 || ValidProjectIDs.GlobalSelect == true || ValidProjectIDs.IDs.Contains(dr.GetDecimal(ProjectColumn))))
        //            {
        //                ++Row;
        //                foreach (int count in o)
        //                {
        //                    if (!dr.IsDBNull(count) && dr.GetValue(count).ToString().Length > 0)
        //                    //if (!dr.IsDBNull(count))
        //                    {
        //                        Value = dr.GetValue(count).ToString();
        //                        if (formatnumericColumns.Contains(count) && double.TryParse(Value, out nValue) == true)
        //                        {
        //                            // format numeric.
        //                            sheet.Range[Row, Col].Number = nValue;
        //                            sheet.Range[Row, Col].NumberFormat = "@";
        //                        }

        //                        else if (formatDateColumns.Contains(count) && DateTime.TryParse(Value, out dValue) == true)
        //                        {
        //                            // format numeric.
        //                            sheet.Range[Row, Col].DateTime = dValue;
        //                            //sheet.Range[Row, Col].NumberFormat = "@";
        //                        }
        //                        else
        //                        {
        //                            sheet.Range[Row, Col].Text = Value;
        //                        }
        //                    }


        //                    ++Col;
        //                }
        //            }
        //        }
        //        //workbook.SaveAs(fileName, Page.Response, ExcelDownloadType.Open);
        //        workbook.SaveAs(fileName, ExcelSaveType.SaveAsXLS, Response, ExcelDownloadType.Open);
        //        workbook.Close();

        //        excelEngine.Dispose();

        //        // move the file out to the browser.
        //    }
        //    catch (Exception ex)
        //    {
        //        message += "----------------------------------------" + Environment.NewLine;
        //        message += "Error:" + DateTime.Now.ToString() + Environment.NewLine;
        //        message += ex.Message;
        //        lblMessage.Text = message;
        //    }
        //    finally
        //    {
        //        cmd.Connection.Close();
        //        cn.Close();

        //    }

        //    return message;
        //}
        #endregion

        //#region ExportToExcelNumeric      // this looks at the data and if it is numeric, then it writes it a numeric.
        //private string ExportToExcelNumeric(string cmd, string fileName)
        //{
        //    List<string> fieldListToReport = new List<string>();
        //    return ExportToExcelNumeric(cmd, fileName, fieldListToReport);
        //}
        //private string ExportToExcelNumeric(string cmd, string fileName, string FieldListToReport)
        //{
        //    List<string> fieldListToReport = new List<string>();
        //    if (FieldListToReport.Length > 0)
        //    {
        //        string[] fList = FieldListToReport.Split(',');
        //        fieldListToReport = new List<string>(fList);
        //    }
        //    return ExportToExcelNumeric(cmd, fileName, fieldListToReport);
        //}
        //private string ExportToExcelNumeric(string scmd, string fileName, List<string> fieldListToReport)
        //{
        //    string message = "";
        //    string[] formatnumeric = {"TRANSACTIONQTY","TRANSACTIONUNITPRICE","QUANTITY","UNITPRICE","MONTHENDQTY", 
        //                              "REPAIR FEE", "REPAIR_FEE", "UNITPRICE01", "UNITPRICE02", "UNITPRICE03", "UNITPRICE04", "UNITPRICE05", "UNITPRICE06", "UNITPRICE07", "UNITPRICE08", "UNITPRICE09", "UNITPRICE10", "TOTALUNITPRICE" };
        //    List<int> formatnumericColumns = new List<int>();


        //    SqlConnection cn = new SqlConnection(ConnectionString);
        //    SqlCommand cmd = new SqlCommand();
        //    List<int> o = new List<int>();
        //    try
        //    {
        //        AccessControlIDList ValidClientLocationIDs = null;
        //        AccessControlIDList ValidProjectIDs = null;
        //        using (clsLinqDataContext ctx = new clsLinqDataContext())
        //        {
        //            ValidClientLocationIDs = new AccessControlIDList(User.Identity.Name, "Client", ctx);
        //            ValidProjectIDs = new AccessControlIDList(User.Identity.Name, "Project", ctx);
        //        }
        //        message = "start:" + DateTime.Now.ToString() + Environment.NewLine;
        //        scmd = scmd.Replace('~', '\'');
        //        cmd.CommandText = scmd;
        //        cmd.CommandTimeout = 240;
        //        cmd.Connection = cn;
        //        cn.Open();
        //        int ClientLocationColumn = -1;
        //        int ProjectColumn = -1;
        //        message += "dbase opened:" + DateTime.Now.ToString() + Environment.NewLine;
        //        SqlDataReader dr = cmd.ExecuteReader();
        //        message += "data read:" + DateTime.Now.ToString() + Environment.NewLine;
        //        string fieldName = "";
        //        for (int count = 0; count < dr.FieldCount; count++)
        //        {
        //            fieldName = dr.GetName(count).ToUpper();
        //            if (formatnumeric.Contains(fieldName.ToUpper()) == true) { formatnumericColumns.Add(count); }

        //            if (fieldName == "CLIENTLOCATIONID") { ClientLocationColumn = count; }
        //            if (fieldName == "PROJECTID") { ProjectColumn = count; }
        //            if (fieldListToReport.Count == 0)
        //            {
        //                //string ValueTypex = dr.GetFieldType(count).Name;
        //                o.Add(count);
        //            }
        //            else
        //            {
        //                if (fieldListToReport.Contains(dr.GetName(count)))
        //                {
        //                    //string ValueTypex = dr.GetFieldType(count).ToString();
        //                    o.Add(count);
        //                }
        //            }
        //        }


        //        ExcelEngine excelEngine = new ExcelEngine();
        //        IApplication application = excelEngine.Excel;
        //        application.DefaultVersion = GetExcelVersion();
        //        fileName = fileName.Replace(".xls", "");

        //        fileName = fileName + "." + GetExcelExtension();
        //        IWorkbook workbook = application.Workbooks.Create(1);
        //        //                workbook.Version = GetExcelVersion();
        //        IWorksheet sheet = workbook.Worksheets[0];
        //        int Row = 1;
        //        int Col = 1;
        //        int StartCol = Col;
        //        int StartRow = Row;
        //        lblMessage.Text = "";
        //        //Add Header    
        //        foreach (int count in o)
        //        {
        //            if (dr.GetName(count) != null)
        //            {
        //                sheet.Range[Row, Col].Text = dr.GetName(count);
        //            }
        //            ++Col;
        //        }

        //        string Value = "";
        //        double nValue = 0;
        //        while (dr.Read())
        //        {
        //            Col = 1;
        //            if ((ClientLocationColumn == -1 || ValidClientLocationIDs.GlobalSelect == true || ValidClientLocationIDs.IDs.Contains(dr.GetDecimal(ClientLocationColumn))) &&
        //                (ProjectColumn == -1 || ValidProjectIDs.GlobalSelect == true || ValidProjectIDs.IDs.Contains(dr.GetDecimal(ProjectColumn))))
        //            {
        //                ++Row;
        //                foreach (int count in o)
        //                {
        //                    if (!dr.IsDBNull(count) && dr.GetValue(count).ToString().Length > 0)
        //                    //if (!dr.IsDBNull(count))
        //                    {
        //                        Value = dr.GetValue(count).ToString();
        //                        if (formatnumericColumns.Contains(count))
        //                        {
        //                            if (double.TryParse(Value, out nValue) == true)
        //                            {
        //                                // format numeric.
        //                                sheet.Range[Row, Col].Number = nValue;
        //                                sheet.Range[Row, Col].NumberFormat = "@";
        //                            }
        //                            else
        //                            {
        //                                sheet.Range[Row, Col].Text = Value;
        //                            }
        //                        }
        //                        else
        //                        {
        //                            sheet.Range[Row, Col].Text = Value;
        //                        }


        //                    }
        //                    ++Col;
        //                }
        //            }
        //        }
        //        //workbook.SaveAs(fileName, Page.Response, ExcelDownloadType.Open);
        //        workbook.SaveAs(fileName, ExcelSaveType.SaveAsXLS, Response, ExcelDownloadType.Open);
        //        workbook.Close();

        //        excelEngine.Dispose();

        //        // move the file out to the browser.
        //    }
        //    catch (Exception ex)
        //    {
        //        message += "----------------------------------------" + Environment.NewLine;
        //        message += "Error:" + DateTime.Now.ToString() + Environment.NewLine;
        //        message += ex.Message;
        //        lblMessage.Text = message;
        //    }
        //    finally
        //    {
        //        cmd.Connection.Close();
        //        cn.Close();

        //    }

        //    return message;
        //}

        //#endregion
        #endregion

        # region Generate CSV File
        private string ExportXLSFile(SqlConnection cn, SqlCommand cmd, string fileName, List<string> fieldListToReport)
        {
            string message = "";
            //string[] formatnumeric = {"TRANSACTIONQTY","TRANSACTIONUNITPRICE","QUANTITY","UNITPRICE","MONTHENDQTY", "MOVEDOUT","MOVEDIN","STARTING","ENDING","FREQIN","FREQOUT",
            //                          "REPAIR FEE", "REPAIR_FEE", "UNITPRICE01", "UNITPRICE02", "UNITPRICE03", "UNITPRICE04", "UNITPRICE05", "UNITPRICE06", "UNITPRICE07",
            //                          "UNITPRICE08", "UNITPRICE09", "UNITPRICE10", "TOTALUNITPRICE","REPAIR CAP", "DEVICECOST",
            //                          "INPROCESSSECONDS","INPROCESSMINUTES","INPROCESSHOURS","MINUTESTOYELLOW","MINUTESTORED"};
            //List<int> formatnumericColumns = new List<int>();
            //string[] formatDate = { "LASTUPDATEDATE", "MONTHENDDATE", "CREATEDATE", "RECEIVEDATE", "MOVEDDATE", "REPORTBEGINDATE", "REPORTENDDATE", 
            //                        "DATEMOVED", "DATEMOVEDOUT", "BININDATE", "BINOUTDATE", "ATTEMPTDATE", "ATTEMPTDATE2", "ATTEMPTDATE3", "ATTEMPTDATED", 
            //                        "ATTEMPTDATE2D", "ATTEMPTDATE3D", "LASTUPDATEDATED", "MONTHENDDATED", "CREATEDATED" };
            //List<int> formatDateColumns = new List<int>();
            ReportUtility ru = new ReportUtility();
            string[] formatnumeric = ru.ListALLNumericQuestionNames().ToArray();
            string[] formatDate = ru.ListDateQuestionNames().ToArray();

            string[] formatCalc = ru.ListCALCQuestionNames().ToArray();
            string[] formatNumeric = ru.ListNUMERICQuestionNames().ToArray();
            string[] format3Digit = ru.ListNUM3DIGITQuestionNames().ToArray();
            string[] formatCurrency = ru.ListCURRENCYQuestionNames().ToArray();



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
                        if (formatCalc.Contains(dr.GetName(count).ToUpper()) == true)
                        {
                            sheet.Columns[count].NumberFormat = "#.#";
                        }
                        if (formatNumeric.Contains(dr.GetName(count).ToUpper()) == true)
                        {
                            sheet.Columns[count].NumberFormat = "#.#";
                        }
                        if (format3Digit.Contains(dr.GetName(count).ToUpper()) == true)
                        {
                            sheet.Columns[count].NumberFormat = "###";
                        }
                        if (formatCurrency.Contains(dr.GetName(count).ToUpper()) == true)
                        {
                            sheet.Columns[count].NumberFormat = "$#,##0.00";
                        }
                        if (formatDate.Contains(dr.GetName(count).ToUpper()) == true)
                        {
                            // sheet.Columns[count].NumberFormat = "#";
                        }

                        //string[] formatCalc = ru.ListCALCQuestionNames().ToArray();
                        //string[] formatNumeric = ru.ListNUMERICQuestionNames().ToArray();
                        //string[] format3Digit = ru.ListNUM3DIGITQuestionNames().ToArray();
                        //string[] formatCurrency = ru.ListCURRENCYQuestionNames().ToArray();


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
                                    //sheet.Range[Row, Col].NumberFormat = "#";
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
            log.LogIt(message);
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
        #endregion

    }
}