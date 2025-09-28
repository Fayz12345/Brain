using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Configuration;
using System.Web.UI;
using BW_WebApp.BarcodeUtils;
//using Factory_DataModel;
using BW_WebApp.DataManagers;
//using BusinessLayer;
using Syncfusion.XlsIO;

namespace BW_WebApp
{
    public partial class Report_Excel_OUTX : System.Web.UI.Page
    {
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

            string ReportRequest = Request.QueryString.Get("RPT");
            if (ReportRequest == null || ReportRequest.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CloseWindow", "window.close();", true);
                return;
            }


            if (IsPostBack == false)
            {
                string CommandString = "";
                string Batch = "";
                string Status = "";
                string sID = "";
                decimal ID = -1;
                string sFieldList = "";
                string[] sdata;
                string fName = "";
                string UserName = Request.QueryString.Get("USERNAME");

                string Parameters = "";
                bool datafound = false;
                CycleCountManager im = new CycleCountManager(UserName);
                CycleInventoryCountTemplateHeader H = null;
                switch (ReportRequest.ToUpper())
                {

                    case "IFSTRANS":
                        Batch = Request.QueryString.Get("KEY");
                        //UserName = Request.QueryString.Get("USERNAME");
                        ExportIFSTransDetail();
                        break;
                    case "THISONE":
                        sID = Request.QueryString.Get("COMMAND");
                        //UserName = Request.QueryString.Get("USERNAME");
                        ExportCCTemplateToExcel(sID, "CCReport");
                        break;
                    //case "CCTEMPLATE":
                    //    sID = Request.QueryString.Get("KEY");
                    //    if (sID == null || sID.Length == 0) { sID = "-1"; }
                    //    if (decimal.TryParse(sID, out ID) == false) { ID = -1; }
                    //    UserName = Request.QueryString.Get("USERNAME");
                    //    ExportCCTemplateToExcel("Select * from vwGetCCTemplateHeaderWithDetail where CycleInventoryCountTemplateHeaderID = " + ID.ToString(), "CCTemplateRpt");
                    //    break;


                    #region Cycle count
                    case "IMGDOWNLOADSPREADSUMMARY":
                    //sFieldList = Request.QueryString.Get("KEY");
                    //sdata = sFieldList.Split(',');
                    //sID = sdata[0];
                    //if (sID == null || sID.Length == 0) { sID = "-1"; }
                    //if (decimal.TryParse(sID, out ID) == false) { ID = -1; }
                    //break;
                    case "IMGDOWNLOADSPREADSUMMARYDETAIL":
                        sFieldList = Request.QueryString.Get("KEY");
                        sdata = sFieldList.Split(',');
                        Parameters = "\'" + sdata[0] + "\',";
                        Parameters += "\'Y\',";
                        Parameters += "\'" + sdata[1] + "\',";
                        Parameters += "\'" + sdata[2] + "\',";
                        Parameters += "\'" + sdata[3] + "\',";
                        Parameters += "\'" + sdata[4] + "\',";
                        Parameters += "\'" + sdata[5] + "\'";
                        //ExportCCTemplateToExcel("exec GetReport_CCRunBatchesControlSummaryDetail " + Parameters, "CCSpreadSUMRpt");
                        ExportCCTemplateToExcel("exec GetReport_CCRunsSummaryDetail " + Parameters, "CCSpreadSUMRpt");
                        //exec GetReport_CCRunsSummaryDetail '22','Y','','','','',''
                        break;
                    case "IMGDOWNLOADSPREADDETAIL":
                        sFieldList = Request.QueryString.Get("KEY");
                        sdata = sFieldList.Split(',');
                        Parameters = "\'" + sdata[0] + "\',";
                        Parameters += "\'N\',";
                        Parameters += "\'" + sdata[1] + "\',";
                        Parameters += "\'" + sdata[2] + "\',";
                        Parameters += "\'" + sdata[3] + "\',";
                        Parameters += "\'" + sdata[4] + "\',";
                        Parameters += "\'" + sdata[5] + "\'";
                        //ExportCCTemplateToExcel("exec GetReport_CCRunBatchesControlSummaryDetail " + Parameters, "CCSpreadDetailRpt");
                        ExportCCTemplateToExcel("exec GetReport_CCRunsSummaryDetail " + Parameters, "CCSpreadDetailRpt");
                        break;



                    case "CCRUNDETAIL":
                        sFieldList = Request.QueryString.Get("KEY");
                        sdata = sFieldList.Split(',');
                        Parameters = "\'" + sdata[0] + "\',";
                        Parameters += "\'N\',";
                        Parameters += "\'\',";
                        Parameters += "\'\',";
                        Parameters += "\'\',";
                        Parameters += "\'\',";
                        Parameters += "\'\'";
                        //ExportCCTemplateToExcel("exec GetReport_CCRunBatchesControlSummaryDetail " + Parameters, "CCSpreadDetailRpt");
                        ExportCCTemplateToExcel("exec GetReport_CCRunsSummaryDetail " + Parameters, "CCRunDetailRpt");
                        break;
                    case "CCRUNSUMMARYDETAIL":
                        sFieldList = Request.QueryString.Get("KEY");
                        sdata = sFieldList.Split(',');
                        Parameters = "\'" + sdata[0] + "\',";
                        Parameters += "\'Y\',";
                        Parameters += "\'\',";
                        Parameters += "\'\',";
                        Parameters += "\'\',";
                        Parameters += "\'\',";
                        Parameters += "\'\'";
                        //ExportCCTemplateToExcel("exec GetReport_CCRunBatchesControlSummaryDetail " + Parameters, "CCSpreadDetailRpt");
                        ExportCCTemplateToExcel("exec GetReport_CCRunsSummaryDetail " + Parameters, "CCRunSummaryDetailRpt");
                        break;
                    case "CCRUNSUMMARY":
                        sFieldList = Request.QueryString.Get("KEY");
                        sdata = sFieldList.Split(',');
                        Parameters = "\'" + sdata[0] + "\',";
                        Parameters += "\'Y\',";
                        Parameters += "\'\',";
                        Parameters += "\'\',";
                        Parameters += "\'\',";
                        Parameters += "\'\',";
                        Parameters += "\'\'";
                        //ExportCCTemplateToExcel("exec GetReport_CCRunBatchesControlSummaryDetail " + Parameters, "CCSpreadDetailRpt");
                        ExportCCTemplateToExcel("exec GetReport_CCRunsSummaryDetail " + Parameters, "CCRunSummaryRpt");
                        break;




                    case "CCTEMPLATESUMMARY":
                        sID = Request.QueryString.Get("KEY");
                        if (sID == null || sID.Length == 0) { sID = "-1"; }
                        if (decimal.TryParse(sID, out ID) == false) { ID = -1; }
                        //UserName = Request.QueryString.Get("USERNAME");
                        H = im.GetTemplate(ID);
                        if (H != null)
                        {
                            datafound = true;
                            Parameters = "\'" + H.IFSSite + "\',";
                            Parameters += "\'" + H.Carriers + "\',";
                            Parameters += "\'" + H.Manufacturers + "\',";
                            Parameters += "\'" + H.Models + "\',";
                            Parameters += "\'" + H.Colours + "\',";
                            Parameters += "\'" + H.IFSLocation + "\',";
                            Parameters += "\'" + H.IFSCondition + "\'";
                        }
                        if (datafound == true)
                        {
                            ExportCCTemplateToExcel("exec GetReceiveDetailsInTheseSKULocationCondition_Summary " + Parameters, "CCTemplateSUMRpt");
                        }
                        break;
                    case "CCTEMPLATESUMMARYDETAIL":
                        sID = Request.QueryString.Get("KEY");
                        if (sID == null || sID.Length == 0) { sID = "-1"; }
                        if (decimal.TryParse(sID, out ID) == false) { ID = -1; }
                        //UserName = Request.QueryString.Get("USERNAME");
                        H = im.GetTemplate(ID);
                        if (H != null)
                        {
                            datafound = true;
                            Parameters = "\'" + H.IFSSite + "\',";
                            Parameters += "\'" + H.Carriers + "\',";
                            Parameters += "\'" + H.Manufacturers + "\',";
                            Parameters += "\'" + H.Models + "\',";
                            Parameters += "\'" + H.Colours + "\',";
                            Parameters += "\'" + H.IFSLocation + "\',";
                            Parameters += "\'" + H.IFSCondition + "\'";
                        }

                        if (datafound == true)
                        {
                            ExportCCTemplateToExcel("exec GetReceiveDetailsInTheseSKULocationCondition " + Parameters, "CCTemplateSUMDetailRpt");
                        }
                        break;
                    case "CCTEMPLATEDETAIL":
                        sID = Request.QueryString.Get("KEY");
                        if (sID == null || sID.Length == 0) { sID = "-1"; }
                        if (decimal.TryParse(sID, out ID) == false) { ID = -1; }
                        //UserName = Request.QueryString.Get("USERNAME");
                        H = im.GetTemplate(ID);
                        if (H != null)
                        {
                            datafound = true;
                            Parameters = "\'" + H.IFSSite + "\',";
                            Parameters += "\'" + H.Carriers + "\',";
                            Parameters += "\'" + H.Manufacturers + "\',";
                            Parameters += "\'" + H.Models + "\',";
                            Parameters += "\'" + H.Colours + "\',";
                            Parameters += "\'" + H.IFSLocation + "\',";
                            Parameters += "\'" + H.IFSCondition + "\'";
                        }
                        if (datafound == true)
                        {
                            ExportCCTemplateToExcel("exec GetDevicesInTheseSKULocationCondition " + Parameters, "CCTemplateDetailRpt");
                        }
                        break;

                    #endregion

                    //case "PARTPHYSICALCOUNT":
                    //    Batch = Request.QueryString.Get("KEY");
                    //    UserName = Request.QueryString.Get("USERNAME");
                    //    ExportPartPhysicalCount(UserName, Batch);
                    //    break;

                    case "PHYSICALTABTIMECOUNT":
                        Batch = Request.QueryString.Get("KEY");
                        string Startime = Request.QueryString.Get("BEGIN");
                        string Endtime = Request.QueryString.Get("END");

                        //UserName = Request.QueryString.Get("USERNAME");
                        ExportPhysicalCountByTabTime(UserName, Batch, Startime, Endtime);
                        break;


                    case "PHYSICALTABCOUNTPART":
                        Batch = Request.QueryString.Get("KEY");
                        //UserName = Request.QueryString.Get("USERNAME");
                        ExportPartPhysicalCount(UserName, Batch);
                        break;
                    //case "PARTPHYSICALCOUNT":
                    //    Batch = Request.QueryString.Get("KEY");
                    //    UserName = Request.QueryString.Get("USERNAME");
                    //    ExportPartPhysicalCount(UserName, Batch);
                    //    break;
                    case "PHYSICALTABCOUNT":
                        Status = Request.QueryString.Get("KEY");
                        //UserName = Request.QueryString.Get("USERNAME");
                        ExportPhysicalTabCount(UserName, Status);
                        break;
                    case "PHYSICALCOUNT":
                        Batch = Request.QueryString.Get("KEY");
                        //UserName = Request.QueryString.Get("USERNAME");
                        ExportPhysicalCount(UserName, Batch);
                        break;
                    case "PIFULLSUMMARY":
                        //Batch = Request.QueryString.Get("KEY");
                        //UserName = Request.QueryString.Get("USERNAME");
                        ExportPhysicalCountPI(UserName);
                        break;
                    case "PHYSICALCOUNTCC":
                        Batch = Request.QueryString.Get("BATCH");
                        sID = Request.QueryString.Get("KEY");
                        //UserName = Request.QueryString.Get("USERNAME");
                        ExportPhysicalCountCC(UserName, Batch, sID);
                        break;
                    case "AVSTOCK":
                        sID = Request.QueryString.Get("KEY");
                        //UserName = Request.QueryString.Get("USERNAME");
                        ExportAvailableStock_B(UserName, sID);
                        break;
                    case "AVSTOCKA":
                        sID = Request.QueryString.Get("KEY");
                        //UserName = Request.QueryString.Get("USERNAME");
                        ExportAvailableStock_A(UserName, sID);
                        break;
                    case "UNITANALYSIS":
                        sID = Request.QueryString.Get("ID");
                        if (sID == null || sID.Length == 0) { sID = "-1"; }
                        if (decimal.TryParse(sID, out ID) == false) { ID = -1; }
                        ExportUnitAnalysisToExcel("exec Report_UnitAnalysis " + ID.ToString(), "UnitAnalysisRpt");
                        break;
                    case "LOG":
                        btnShowTimeLog();
                        break;
                    case "UNITDATA":
                        string ESN = Request.QueryString.Get("ESN");
                        string Version = Request.QueryString.Get("VERSION");
                        btnShowUnitData(ESN, Version);
                        break;
                    case "PMIFILE01":
                        pmiGenerateFile01();
                        break;
                    case "LOGUTILITY":
                        btnShowTimeLog("UtilityUpload_01_Log.txt");
                        break;
                    case "PIC":
                        sID = Request.QueryString.Get("ID");
                        if (sID == null || sID.Length == 0) { sID = "-1"; }
                        if (decimal.TryParse(sID, out ID) == false) { ID = -1; }
                        ExportPickingSlipToExcel(ID);
                        break;
                    case "PAC":
                    case "SHIP":
                    case "DON":
                    case "ARC":
                    case "MIS":
                        sID = Request.QueryString.Get("ID");
                        if (sID == null || sID.Length == 0) { sID = "-1"; }
                        if (decimal.TryParse(sID, out ID) == false) { ID = -1; }
                        ExportPackingSlipToExcel(ID); break;
                    case "INV":
                        sFieldList = Request.QueryString.Get("fList");
                        fName = Request.QueryString.Get("fName");
                        CommandString = Request.QueryString.Get("CMD");
                        ExportInventoryToExcel(CommandString, fName, sFieldList);
                        break;
                    case "BSP":
                        sFieldList = Request.QueryString.Get("fList");
                        fName = Request.QueryString.Get("fName");
                        CommandString = Request.QueryString.Get("CMD");
                        ExportBSPInventoryToExcel(CommandString, fName);
                        break;
                    case "BIN":
                        string BinNumber = Request.QueryString.Get("Bin");
                        if (BinNumber == null || BinNumber.Length == 0) { BinNumber = "10"; }
                        ExportSpotCountReportToExcel(BinNumber);
                        break;
                    case "QUESTION":
                        sID = Request.QueryString.Get("ID");

                        sFieldList = "";
                        fName = "QuestionReport.xls";            //Request.QueryString.Get("fName");
                        CommandString = Request.QueryString.Get("CMD");
                        ExportToExcel(CommandString, fName);
                        break;


                    case "CLIENTSUBMIT":
                        sID = Request.QueryString.Get("RDID");
                        if (sID == null || sID.Length == 0) { sID = "-1"; }
                        if (decimal.TryParse(sID, out ID) == false) { ID = -1; }
                        ExportClientSubmitFormToExcel(ID);
                        break;
                    default:
                        ScriptManager.RegisterStartupScript(this, GetType(), "CloseWindow", "window.close();", true);
                        break;
                }
            }
        }


        #region ExportIFSTransDetail
        private void ExportIFSTransDetail()
        {
            string cmdString = "";
            string Esn = Request.QueryString.Get("ESN");
            string Batch = Request.QueryString.Get("Batch");
            string StartDate = Request.QueryString.Get("StartDate");
            string EndDate = Request.QueryString.Get("EndDate");
            string IncludeDone = Request.QueryString.Get("IncludeDone");
            string RecordDone = Request.QueryString.Get("RecordDone");
            string UserName = Request.QueryString.Get("USERNAME");
            string ShowDetail = Request.QueryString.Get("Detail");

            DateTime eDate = DateTime.Now;
            if (DateTime.TryParse(EndDate, out eDate) == true)
            {
                eDate = eDate.AddDays(1);
                EndDate = String.Format("{0:MM/dd/yyyy}", eDate);
            }
            if (RecordDone.ToUpper() == "Y")           //  && Batch.Length > 0)
            {
                DeviceInventoryManager cm = new DeviceInventoryManager(UserName);
                int batch = -1;
                //int.TryParse(Batch, out batch);
                //List<vwIFS_InvtTran> dta = cm.GetInvtTran_IFSList(batch);
                //if (dta.Count == 0)
                //{
                batch = cm.LogAsTaken(Esn, StartDate, EndDate, UserName);
                cmdString = "GetIFSInventoryTransactions '" + Esn + "','" + batch.ToString() + "','" + StartDate + "','" + EndDate + "','" + IncludeDone + "','" + ShowDetail + "'";
                ExportToExcel(cmdString, "GetIFSInventoryTransactions" + batch.ToString());
                //}

                return;
            }
            cmdString = "GetIFSInventoryTransactions '" + Esn + "','" + Batch + "','" + StartDate + "','" + EndDate + "','" + IncludeDone + "','" + ShowDetail + "'";
            ExportToExcel(cmdString, "GetIFSInventoryTransactions" + Batch);
        }

        #endregion





        #region ExportPartPhysicalCount
        private void ExportPartPhysicalCount(string UserName, string Batch)
        {
            string cmdString = "";
            if (Batch.Length == 0)
            {
                return;
            }

            cmdString = "GetPhysicalPartInventoryCountList '" + Batch + "'";
            ExportToExcel(cmdString, "GetPhysicalPartInventoryCountList" + Batch);
        }

        #endregion


        #region DownloadPhysicalInventoryCount
        private void ExportPhysicalCountCC(string UserName, string Batch, string CycleInventoryCountHeaderID)
        {
            string cmdString = "";
            if (Batch.Length == 0)
            {
                return;
            }

            cmdString = "GetPhysicalInventoryCountListCC " + CycleInventoryCountHeaderID.ToString() + ",'" + Batch + "'";
            ExportToExcel(cmdString, "InventoryCount" + Batch);
        }
        private void ExportPhysicalCountPI(string UserName)
        {
            string cmdString = "";
            cmdString = "GetPhysicalInventoryCountListFullCompare02 ";
            ExportToExcel(cmdString, "PISummary");
        }
        private void ExportPhysicalCount(string UserName, string Batch)
        {
            string cmdString = "";
            if (Batch.Length == 0)
            {
                return;
            }

            cmdString = "GetPhysicalInventoryCountList '" + Batch + "'";
            ExportToExcel(cmdString, "GetPhysicalInventoryCountList" + Batch);
        }
        private void ExportPhysicalCountByTabTime(string UserName, string Tab, string StartTime, string EndTime)
        {
            string cmdString = "";
            if (Tab.Length == 0)
            {
                return;
            }
            cmdString = "GetPhysicalInventoryCountListByTabTime '" + Tab + "','" + StartTime + "','" + EndTime + "'";
            ExportToExcel(cmdString, "GetPhysicalInventoryCountList" + Tab);
        }

        #endregion
        #region DownloadPhysicalTabInventoryCount
        private void ExportPhysicalTabCount(string UserName, string Status)
        {
            string cmdString = "";
            if (Status.Length == 0)
            {
                return;
            }

            //cmdString = "GetPhysicalInventoryCountList '" + Status + "'";
            cmdString = "Select * from vwGridPhysicalInventoryCount_B where ListName = '" + Status + "'";
            ExportToExcel(cmdString, "GetPhysicalInventoryStatusCountList" + Status);
        }

        #endregion
        #region Download Log File


        //void pmiGenerateFile01()
        //{
        //    pmiGenerateFile01("WebServer_01_Log.txt");
        //}

        void pmiGenerateFile01()
        {
            //string ReportRequest = Request.QueryString.Get("FileName");

            string FileName = Request.QueryString.Get("FileName");
            string fPath = Server.MapPath("~/" + System.Configuration.ConfigurationManager.AppSettings["PMI_EDI_Directory"] + "/" + FileName);

            System.IO.FileStream fs = null;

            fs = System.IO.File.Open(fPath, System.IO.FileMode.Open);
            byte[] btFile = new byte[fs.Length];
            fs.Read(btFile, 0, Convert.ToInt32(fs.Length));
            fs.Close();
            Response.AddHeader("Content-disposition", "attachment; filename=PMIFile01.txt");
            Response.ContentType = "application/octet-stream";
            Response.BinaryWrite(btFile);
            Response.End();
            //btnShowTimeLog("WebServer_01_Log.txt");



            //using (var memoryStream = new MemoryStream())
            //{
            //    using (var writer = new StreamWriter(memoryStream))
            //    {





            //        //// Various for loops etc as necessary that will ultimately do this:
            //        //writer.WriteLine("This is the data here and now");
            //        //writer.WriteLine("Another one here");

            //        ////FileName = Server.MapPath(FileName);
            //        ////System.IO.FileStream fs = null;
            //        ////fs = System.IO.File.Open(FileName, System.IO.FileMode.Open);
            //        //byte[] btFile = new byte[memoryStream.Length];
            //        //memoryStream.Read(btFile, 0, Convert.ToInt32(memoryStream.Length));
            //        //Response.AddHeader("Content-disposition", "attachment; filename=PMIFile01.txt");
            //        //Response.ContentType = "application/octet-stream";
            //        //Response.BinaryWrite(btFile);
            //        //Response.End();
            //        //memoryStream.Close();

            //    }
            //}
        }



        void btnShowUnitData(string ESN, string Version)
        {
            //string FileName = Server.MapPath("WebServer_01_Log.txt");
            //System.IO.FileStream fs = null;
            //fs = System.IO.File.Open(FileName, System.IO.FileMode.Open);
            //byte[] btFile = new byte[fs.Length];
            //fs.Read(btFile, 0, Convert.ToInt32(fs.Length));
            //fs.Close();
            //Response.AddHeader("Content-disposition", "attachment; filename=WebServer_01_Log.txt");
            //Response.ContentType = "application/octet-stream";
            //Response.BinaryWrite(btFile);
            //Response.End();
            string rData = "";
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                rData = ctx.GETUnitData(ESN, Version, User.Identity.Name, "");
            }

            btnShowUnitDatax("WebServer_01_Log.txt", rData);

        }
        void btnShowUnitDatax(string FileName, string Data)
        {
            //FileName = Server.MapPath(FileName);
            //System.IO.FileStream fs = null;
            //fs = System.IO.File.Open(FileName, System.IO.FileMode.Open);
            //byte[] btFile = new byte[Data.Length];
            //btFile = Data.;
            //fs.Read(btFile, 0, Convert.ToInt32(fs.Length));
            //fs.Close();
            Response.AddHeader("Content-disposition", "attachment; filename=UnitData.txt");
            Response.ContentType = "application/octet-stream";
            Response.Write(Data);
            Response.End();
        }

        void btnShowTimeLog()
        {
            //string FileName = Server.MapPath("WebServer_01_Log.txt");
            //System.IO.FileStream fs = null;
            //fs = System.IO.File.Open(FileName, System.IO.FileMode.Open);
            //byte[] btFile = new byte[fs.Length];
            //fs.Read(btFile, 0, Convert.ToInt32(fs.Length));
            //fs.Close();
            //Response.AddHeader("Content-disposition", "attachment; filename=WebServer_01_Log.txt");
            //Response.ContentType = "application/octet-stream";
            //Response.BinaryWrite(btFile);
            //Response.End();
            btnShowTimeLog("WebServer_01_Log.txt");

        }

        void btnShowTimeLog(string FileName)
        {

            string path = Server.MapPath("~");
            if (!path.EndsWith("\\"))
            {
                path += "\\";
            }
            FileName = path + FileName;
            System.IO.FileStream fs = null;
            fs = System.IO.File.Open(FileName, System.IO.FileMode.Open);
            byte[] btFile = new byte[fs.Length];
            fs.Read(btFile, 0, Convert.ToInt32(fs.Length));
            fs.Close();
            Response.AddHeader("Content-disposition", "attachment; filename=WebServer_01_Log.txt");
            Response.ContentType = "application/octet-stream";
            Response.BinaryWrite(btFile);
            Response.End();
        }




        #endregion

        #region CCTemplate

        private void ExportCCTemplateToExcel(string scmd, string fileName)
        {

            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand();
            List<int> o = new List<int>();
            ExcelEngine excelEngine = new ExcelEngine();
            IApplication application = excelEngine.Excel;
            IWorkbook workbook = application.Workbooks.Create(1);
            workbook.Version = ExcelVersion.Excel2013;
            IWorksheet sheet = workbook.Worksheets[0];
            try
            {

                scmd = scmd.Replace('~', '\'');
                cmd.CommandText = scmd;
                cmd.CommandTimeout = 240;
                cmd.Connection = cn;
                cn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                for (int count = 0; count < dr.FieldCount; count++)
                {
                    //string ValueTypex = dr.GetFieldType(count).Name;
                    o.Add(count);
                }



                int Row = 1;
                int Col = 1;
                int StartCol = Col;
                int StartRow = Row;
                //Add Header    
                foreach (int count in o)
                {
                    if (Col > 256) { break; }
                    if (dr.GetName(count) != null)
                    {
                        sheet.Range[Row, Col].Text = dr.GetName(count);
                    }
                    ++Col;
                }

                while (dr.Read())
                {
                    Col = 1;
                    ++Row;
                    foreach (int count in o)
                    {
                        if (Col > 256) { break; }
                        if (!dr.IsDBNull(count) && dr.GetValue(count).ToString().Length > 0)
                        //if (!dr.IsDBNull(count))
                        {
                            sheet.Range[Row, Col].Text = dr.GetValue(count).ToString();
                            if (Col == 1)
                            {
                                sheet.Range[Row, Col].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignRight;
                            }
                        }
                        ++Col;
                    }
                }
                //sheet.Range[StartRow, StartCol, Row, Col].AutofitColumns();
                //sheet.Range[StartRow, StartCol, StartRow, Col].AutofitColumns();
                //workbook.SaveAs("Sample.xls", Page.Response, ExcelDownloadType.Open);
                //string templatePath = Page.MapPath("Temp/XXXXXXX.xls");




                //string templatePath = Page.MapPath("Temp/Inventory" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".xls");
                //workbook.SaveAs(templatePath);
                //workbook.Close();
                //workbook = application.Workbooks.Open(templatePath);

                //workbook.SaveAs(fileName + ".xlsx", Page.Response, ExcelDownloadType.Open);
                workbook.SaveAs(fileName + ".xlsx", Page.Response, ExcelDownloadType.Open);
                workbook.Close();

                excelEngine.Dispose();

                //return excelEngine.SaveAsActionResult(workbook, "Sample.xlsx", HttpContext.ApplicationInstance.Response, ExcelDownloadType.PromptDialog, ExcelHttpContentType.Excel2013);


                // move the file out to the browser.
            }
            catch (Exception ex)
            {
                sheet.Range[2, 1].Text = ex.Message;
            }
            finally
            {
                cmd.Connection.Close();
                cn.Close();
                workbook.SaveAs(fileName + ".xlsx", Page.Response, ExcelDownloadType.Open);
                workbook.Close();

                excelEngine.Dispose();
            }
        }
        #endregion

        #region Excel Unit Analysis
        private void ExportUnitAnalysisToExcel(string scmd, string fileName)
        {

            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand();
            List<int> o = new List<int>();
            try
            {

                scmd = scmd.Replace('~', '\'');
                cmd.CommandText = scmd;
                cmd.CommandTimeout = 240;
                cmd.Connection = cn;
                cn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                for (int count = 0; count < dr.FieldCount; count++)
                {
                    //string ValueTypex = dr.GetFieldType(count).Name;
                    o.Add(count);
                }


                ExcelEngine excelEngine = new ExcelEngine();
                IApplication application = excelEngine.Excel;
                IWorkbook workbook = application.Workbooks.Create(1);
                IWorksheet sheet = workbook.Worksheets[0];
                int Row = 1;
                int Col = 1;
                int StartCol = Col;
                int StartRow = Row;
                //Add Header    
                foreach (int count in o)
                {
                    if (dr.GetName(count) != null)
                    {
                        sheet.Range[Row, Col].Text = dr.GetName(count);
                    }
                    ++Col;
                }

                while (dr.Read())
                {
                    Col = 1;
                    ++Row;
                    foreach (int count in o)
                    {
                        if (!dr.IsDBNull(count) && dr.GetValue(count).ToString().Length > 0)
                        //if (!dr.IsDBNull(count))
                        {
                            sheet.Range[Row, Col].Text = dr.GetValue(count).ToString();
                            if (Col == 1)
                            {
                                sheet.Range[Row, Col].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignRight;
                            }
                        }
                        ++Col;
                    }
                }
                //sheet.Range[StartRow, StartCol, Row, Col].AutofitColumns();
                //sheet.Range[StartRow, StartCol, StartRow, Col].AutofitColumns();
                //workbook.SaveAs("Sample.xls", Page.Response, ExcelDownloadType.Open);
                //string templatePath = Page.MapPath("Temp/XXXXXXX.xls");




                //string templatePath = Page.MapPath("Temp/Inventory" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".xls");
                //workbook.SaveAs(templatePath);
                //workbook.Close();
                //workbook = application.Workbooks.Open(templatePath);


                workbook.SaveAs(fileName + ".xls", Page.Response, ExcelDownloadType.Open);
                workbook.Close();

                excelEngine.Dispose();

                // move the file out to the browser.
            }
            catch (Exception ex)
            {
            }
            finally
            {
                cmd.Connection.Close();
                cn.Close();
            }
        }
        #endregion

        #region Export to Excel
        //private void ExportToExcel(string cmd, string fileName)
        //{
        //    List<string> fieldListToReport = new List<string>();
        //    ExportToExcel(cmd, fileName, fieldListToReport);
        //}
        //private void ExportToExcel(string cmd, string fileName, string FieldListToReport)
        //{
        //    List<string> fieldListToReport = new List<string>();
        //    if (FieldListToReport.Length > 0)
        //    {
        //        string[] fList = FieldListToReport.Split(',');
        //        fieldListToReport = new List<string>(fList);
        //    }
        //    ExportToExcel(cmd, fileName, fieldListToReport);
        //}
        //private void ExportToExcel(string scmd, string fileName, List<string> fieldListToReport)
        //{

        //    SqlConnection cn = new SqlConnection(ConnectionString);
        //    SqlCommand cmd = new SqlCommand();
        //    List<int> o = new List<int>();
        //    try
        //    {

        //        scmd = scmd.Replace('~', '\'');
        //        cmd.CommandText = scmd;
        //        cmd.CommandTimeout = 240;
        //        cmd.Connection = cn;
        //        cn.Open();

        //        SqlDataReader dr = cmd.ExecuteReader();
        //        for (int count = 0; count < dr.FieldCount; count++)
        //        {
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
        //        IWorkbook workbook = application.Workbooks.Create(1);
        //        IWorksheet sheet = workbook.Worksheets[0];
        //        int Row = 1;
        //        int Col = 1;
        //        int StartCol = Col;
        //        int StartRow = Row;
        //        //Add Header    
        //        foreach (int count in o)
        //        {
        //            if (dr.GetName(count) != null)
        //            {
        //                sheet.Range[Row, Col].Text = dr.GetName(count);
        //            }
        //            ++Col;
        //        }

        //        while (dr.Read())
        //        {
        //            Col = 1;
        //            ++Row;
        //            foreach (int count in o)
        //            {
        //                if (!dr.IsDBNull(count) && dr.GetValue(count).ToString().Length > 0)
        //                //if (!dr.IsDBNull(count))
        //                {
        //                    sheet.Range[Row, Col].Text = dr.GetValue(count).ToString();
        //                }
        //                ++Col;
        //            }
        //        }
        //        //sheet.Range[StartRow, StartCol, Row, Col].AutofitColumns();
        //        //sheet.Range[StartRow, StartCol, StartRow, Col].AutofitColumns();
        //        //workbook.SaveAs("Sample.xls", Page.Response, ExcelDownloadType.Open);
        //        //string templatePath = Page.MapPath("Temp/XXXXXXX.xls");




        //        //string templatePath = Page.MapPath("Temp/Inventory" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".xls");
        //        //workbook.SaveAs(templatePath);
        //        //workbook.Close();
        //        //workbook = application.Workbooks.Open(templatePath);


        //        workbook.SaveAs(fileName, Page.Response, ExcelDownloadType.Open);
        //        workbook.Close();

        //        excelEngine.Dispose();

        //        // move the file out to the browser.
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    finally
        //    {
        //        cmd.Connection.Close();
        //        cn.Close();
        //    }
        //}

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
            //string[] formatnumeric = {"TRANSACTIONQTY","TRANSACTIONUNITPRICE","QUANTITY","UNITPRICE","MONTHENDQTY","MONTHENDUNITPRICE", "ORIGINAL COST","SELLING PRICE","INTERNAL COST", "DEVICECOST",
            //                          "REPAIR FEE", "REPAIR_FEE", "UNITPRICE01", "UNITPRICE02", "UNITPRICE03", "UNITPRICE04", "UNITPRICE05", "UNITPRICE06", "UNITPRICE07", "UNITPRICE08", "UNITPRICE09", "UNITPRICE10", "TOTALUNITPRICE", "HOLDQUANTITY",
            //                          "FREQ", "GOOD", "WARNINGS", "ERRORS", "NUMLOC", "NUMSITE", "NUMPROJECT", "LOCKED", "UNLOCKED", "KITTED", "UNKITTED", "GEN", "NEW", "CPA", "CPB", "CPC", "PTG", "PTC", "PWR", "BER", "BOA", "REC", "C12", "C13", "C14"};
            //List<int> formatnumericColumns = new List<int>();
            //string[] formatDate = { "LASTUPDATEDATE", "MONTHENDDATE", "CREATEDATE", "RECEIVEDATE","STARTDATE","ENDDATE"};
            //List<int> formatDateColumns = new List<int>();
            ReportUtility ru = new ReportUtility();
            string[] formatnumeric = ru.ListALLNumericQuestionNames().ToArray();
            string[] formatDate = ru.ListDateQuestionNames().ToArray();

            List<int> formatnumericColumns = new List<int>();
            List<int> formatDateColumns = new List<int>();


            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand();
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
                scmd = scmd.Replace('~', '\'');
                cmd.CommandText = scmd;
                cmd.CommandTimeout = 360;
                cmd.Connection = cn;
                cn.Open();
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
                //lblMessage.Text = "";
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
                //lblMessage.Text = message;
            }
            finally
            {
                cmd.Connection.Close();
                cn.Close();

            }

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


        #endregion

        #region Excel Inventory Report
        private void ExportInventoryToExcel(string cmd, string fileName)
        {
            List<string> fieldListToReport = new List<string>();
            ExportInventoryToExcel(cmd, fileName, fieldListToReport);
        }
        private void ExportInventoryToExcel(string cmd, string fileName, string FieldListToReport)
        {
            List<string> fieldListToReport = new List<string>();
            if (FieldListToReport.Length > 0)
            {
                string[] fList = FieldListToReport.Split(',');
                fieldListToReport = new List<string>(fList);
            }
            ExportInventoryToExcel(cmd, fileName, fieldListToReport);
        }
        private void ExportInventoryToExcel(string scmd, string fileName, List<string> fieldListToReport)
        {

            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand();
            List<int> o = new List<int>();
            try
            {

                scmd = scmd.Replace('~', '\'');
                cmd.CommandText = scmd;
                cmd.CommandTimeout = 240;
                cmd.Connection = cn;
                cn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                for (int count = 0; count < dr.FieldCount; count++)
                {
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
                IWorkbook workbook = application.Workbooks.Create(1);
                IWorksheet sheet = workbook.Worksheets[0];
                int Row = 1;
                int Col = 1;
                int StartCol = Col;
                int StartRow = Row;
                //Add Header    
                foreach (int count in o)
                {
                    if (dr.GetName(count) != null)
                    {
                        sheet.Range[Row, Col].Text = dr.GetName(count);
                    }
                    ++Col;
                }

                while (dr.Read())
                {
                    Col = 1;
                    ++Row;
                    foreach (int count in o)
                    {
                        if (!dr.IsDBNull(count) && dr.GetValue(count).ToString().Length > 0)
                        //if (!dr.IsDBNull(count))
                        {
                            sheet.Range[Row, Col].Text = dr.GetValue(count).ToString();
                        }
                        ++Col;
                    }
                }
                //sheet.Range[StartRow, StartCol, Row, Col].AutofitColumns();
                //sheet.Range[StartRow, StartCol, StartRow, Col].AutofitColumns();
                //workbook.SaveAs("Sample.xls", Page.Response, ExcelDownloadType.Open);
                //string templatePath = Page.MapPath("Temp/XXXXXXX.xls");




                //string templatePath = Page.MapPath("Temp/Inventory" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".xls");
                //workbook.SaveAs(templatePath);
                //workbook.Close();
                //workbook = application.Workbooks.Open(templatePath);


                workbook.SaveAs("Inventory.xls", Page.Response, ExcelDownloadType.Open);
                workbook.Close();

                excelEngine.Dispose();

                // move the file out to the browser.
            }
            catch (Exception ex)
            {
            }
            finally
            {
                cmd.Connection.Close();
                cn.Close();
            }
        }
        #endregion

        #region AvailableStock
        private void ExportAvailableStock_B(string UserName, string ReportingKeys)
        {
            double TotalQTY = 0;
            double TotalQTYHold = 0;
            string fileName = "AvailableStockDetailRPT";
            string Index = "";
            string Manufacturer = "";
            string Model = "";
            string Colour = "";
            string Grade = "";
            string Carrier = "";
            if (ReportingKeys.Length > 0)
            {
                string[] Keys = ReportingKeys.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                Index = Keys[Keys.Length - 1];

                for (int i = 0; i < Index.Length; i++)
                {
                    switch (Index.Substring(i, 1))
                    {
                        case "M":
                            Manufacturer = Keys[i];
                            break;
                        case "m":
                            Model = Keys[i];
                            break;
                        case "C":
                            Colour = Keys[i];
                            break;
                        case "G":
                            Grade = Keys[i];
                            break;
                        case "c":
                            Carrier = Keys[i];
                            break;
                        default:
                            break;
                    }
                }
            }

            ReceiveDetailManager rm = new ReceiveDetailManager(User.Identity.Name);
            List<AvailableStockDataDetail_Report> Data = rm.GetAvailableStock_Linq_ReortDetail(Manufacturer, Model, Colour, Grade);

            ExcelEngine excelEngine = new ExcelEngine();
            IApplication application = excelEngine.Excel;
            application.DefaultVersion = GetExcelVersion();
            fileName = fileName.Replace(".xls", "");

            fileName = fileName + "." + GetExcelExtension();
            IWorkbook workbook = application.Workbooks.Create(1);
            //                workbook.Version = GetExcelVersion();
            IWorksheet sheet = workbook.Worksheets[0];
            //int columns = 17;
            int Row = 1;
            int Col = 1;
            int StartCol = Col;
            int StartRow = Row;

            sheet.Range[Row, 1].Text = "ReceiveDetailID";
            sheet.Range[Row, 2].Text = "OrderDetailID";
            sheet.Range[Row, 3].Text = "ESN";
            sheet.Range[Row, 4].Text = "Version";
            sheet.Range[Row, 5].Text = "OrderNumber";
            sheet.Range[Row, 6].Text = "Quantity";
            sheet.Range[Row, 7].Text = "HoldQuantity";
            sheet.Range[Row, 8].Text = "H_Manufacturer";
            sheet.Range[Row, 9].Text = "Manufacturer";
            sheet.Range[Row, 10].Text = "H_Model";
            sheet.Range[Row, 11].Text = "Model";
            sheet.Range[Row, 12].Text = "H_Colour";
            sheet.Range[Row, 13].Text = "Colour";
            sheet.Range[Row, 14].Text = "H_Grade";
            sheet.Range[Row, 15].Text = "Grade";
            sheet.Range[Row, 16].Text = "H_Carrier";
            sheet.Range[Row, 17].Text = "Carrier";
            sheet.Range[Row, 18].Text = "Receive_Date_Date";

            foreach (AvailableStockDataDetail_Report x in Data)
            {
                ++Row;
                sheet.Range[Row, 1].Text = x.ReceiveDetailID.ToString();
                sheet.Range[Row, 2].Text = x.OrderDetailID.ToString();
                sheet.Range[Row, 3].Text = x.ESN;
                sheet.Range[Row, 4].Text = x.Version;
                sheet.Range[Row, 5].Text = x.OrderNumber;
                sheet.Range[Row, 6].Number = x.Quantity;
                sheet.Range[Row, 6].NumberFormat = "@";
                sheet.Range[Row, 7].Number = x.HoldQuantity;
                sheet.Range[Row, 7].NumberFormat = "@";
                sheet.Range[Row, 8].Text = x.H_Manufacturer;
                sheet.Range[Row, 9].Text = x.Manufacturer;
                sheet.Range[Row, 10].Text = x.H_Model;
                sheet.Range[Row, 11].Text = x.Model;
                sheet.Range[Row, 12].Text = x.H_Colour;
                sheet.Range[Row, 13].Text = x.Colour;
                sheet.Range[Row, 14].Text = x.H_Grade;
                sheet.Range[Row, 15].Text = x.Grade;
                sheet.Range[Row, 16].Text = x.H_Carrier;
                sheet.Range[Row, 17].Text = x.Carrier;
                sheet.Range[Row, 18].Text = x.ReceiveDateDate;
                TotalQTY += x.Quantity;
                TotalQTYHold += x.HoldQuantity;
            }
            ++Row;
            sheet.Range[Row, 5].Text = "Total";
            sheet.Range[Row, 6].Number = TotalQTY;
            sheet.Range[Row, 6].NumberFormat = "@";
            sheet.Range[Row, 7].Number = TotalQTYHold;
            sheet.Range[Row, 7].NumberFormat = "@";

            //workbook.SaveAs(fileName, Page.Response, ExcelDownloadType.Open);
            workbook.SaveAs(fileName, ExcelSaveType.SaveAsXLS, Response, ExcelDownloadType.Open);
            workbook.Close();
            excelEngine.Dispose();
        }

        private void ExportAvailableStock_A(string UserName, string ReportingKeys)
        {
            string cmdString = "";
            string Index = "";
            string Manufacturer = "";
            string Model = "";
            string Colour = "";
            string Grade = "";
            string Carrier = "";
            if (ReportingKeys.Length > 0)
            {
                string[] Keys = ReportingKeys.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                Index = Keys[Keys.Length - 1];

                for (int i = 0; i < Index.Length; i++)
                {
                    switch (Index.Substring(i, 1))
                    {
                        case "M":
                            Manufacturer = Keys[i];
                            break;
                        case "m":
                            Model = Keys[i];
                            break;
                        case "C":
                            Colour = Keys[i];
                            break;
                        case "G":
                            Grade = Keys[i];
                            break;
                        case "c":
                            Carrier = Keys[i];
                            break;
                        default:
                            break;
                    }
                }
            }
            cmdString = "GetMasterDetailInventory_AvailableStock_01 '" + Manufacturer + "','" + Model + "','" + Colour + "','" + Grade + "','" + Carrier + "'";
            ExportToExcel(cmdString, "IMEIBatch_DetailClientInventory");
        }

        #endregion

        #region BSPReport
        private void ExportBSPInventoryToExcel(string scmd, string fileName)
        {

            //BSPManager BSM = new BSPManager("JIM");
            //var xxx = BSM.GetNewDataContext("JIM").ExecuteQuery<XhtmlConformanceMode>(scmd,"");

            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand();
            List<int> o = new List<int>();
            try
            {
                scmd = scmd.Replace('~', '\'');
                cmd.CommandText = scmd;
                cmd.CommandTimeout = 240;
                cmd.Connection = cn;
                cn.Open();



                SqlDataReader dr = cmd.ExecuteReader();
                for (int count = 0; count < dr.FieldCount; count++)
                {
                    o.Add(count);
                }


                ExcelEngine excelEngine = new ExcelEngine();
                IApplication application = excelEngine.Excel;
                IWorkbook workbook = application.Workbooks.Create(1);
                IWorksheet sheet = workbook.Worksheets[0];
                int Row = 1;
                int Col = 1;
                int StartCol = Col;
                int StartRow = Row;
                //Add Header    
                foreach (int count in o)
                {
                    if (dr.GetName(count) != null)
                    {
                        sheet.Range[Row, Col].Text = dr.GetName(count);
                    }
                    ++Col;
                }
                while (dr.Read())
                {
                    Col = 1;
                    ++Row;
                    foreach (int count in o)
                    {
                        if (!dr.IsDBNull(count) && dr.GetValue(count).ToString().Length > 0)
                        //if (!dr.IsDBNull(count))
                        {
                            sheet.Range[Row, Col].Text = dr.GetValue(count).ToString();
                        }
                        ++Col;
                    }
                }
                workbook.SaveAs("BSPInventory.xls", Page.Response, ExcelDownloadType.Open);
                workbook.Close();
                excelEngine.Dispose();
                // move the file out to the browser.
            }
            catch (Exception ex)
            {
            }
            finally
            {
                cmd.Connection.Close();
                cn.Close();
            }
        }




        #endregion

        #region Excel Client Submit Form
        string ExportClientSubmitFormToExcel(decimal ReceiveDetailID)
        {
            string msg = "";
            try
            {
                string ReportUser = "Test";
                ReportUser = User.Identity.Name;

                string templatePath = Page.MapPath("~/Templates/OrderShipping/Client_SubmissionForm.xls");
                ExcelEngine excelEngine = new ExcelEngine();
                IApplication application = excelEngine.Excel;
                IWorkbook workbook = application.Workbooks.Open(templatePath, ExcelOpenType.Automatic);
                IWorksheet sheet = workbook.Worksheets[0];

                // Get ReceiveDetail.
                ReceiveDetailManager rdm = new ReceiveDetailManager(User.Identity.Name);
                ReceiveDetail RD = rdm.ReceiveDetail(ReceiveDetailID);
                if (RD != null)
                {
                    ExportSubmissionSlip(RD, sheet);
                }
                workbook.SaveAs("SubmissionForm.xls", Page.Response, ExcelDownloadType.Open);
                workbook.Close();
                // Dispose the Excel engine
                excelEngine.Dispose();
            }

            catch (Exception ex)
            {
                msg = ex.Message;
                //ex.
                //Response.Write(ex.Message);
            }
            finally
            {
                //cmd.Connection.Close();
                //cn.Close();
            }
            return msg;
        }
        void ExportSubmissionSlip(ReceiveDetail RD, IWorksheet sheet)
        {
            ReceiveDetailManager rdm = new ReceiveDetailManager(User.Identity.Name);
            using (clsLinqDataContext ctx = rdm.GetDataContext(User.Identity.Name))
            {
                //System.Drawing.Image BarcodeImage = GetBarcodeImage_Base(rdm.GetReceiveDetailItem_DataElement(RD.ReceiveDetailID, "RepairWorkOrder"));
                //sheet.Pictures.AddPicture(2, 8, BarcodeImage);

                //BarcodeImage = GetBarcodeImage_Base(rdm.GetReceiveDetailItem_DataElement(RD.ReceiveDetailID, "RMANumber"));
                //sheet.Pictures.AddPicture(5, 8, BarcodeImage);

                //sheet.Range[10, 7].Text = "Repair Work Order:" + rdm.GetReceiveDetailItem_DataElement(RD.ReceiveDetailID, "RepairWorkOrder");
                //sheet.Range[11, 7].Text = "RMA Number:" + rdm.GetReceiveDetailItem_DataElement(RD.ReceiveDetailID, "RMANumber");
                //sheet.Range[12, 7].Text = "Submission Date:" + rdm.GetReceiveDetailItem_DataElement(RD.ReceiveDetailID, "SubmissionDate");

                System.Drawing.Image BarcodeImage;
                if (RD.ProjectTag.Length > 0)
                {
                    BarcodeImage = GetBarcodeImage_Base(RD.ProjectTag);
                    sheet.Pictures.AddPicture(2, 8, BarcodeImage);
                }
                //BarcodeImage = GetBarcodeImage_Base("RMA00222");
                //sheet.Pictures.AddPicture(6, 8, BarcodeImage);

                if (RD.RMANumber.Length > 0)
                {
                    BarcodeImage = GetBarcodeImage_Base(RD.RMANumber);
                    sheet.Pictures.AddPicture(6, 8, BarcodeImage);
                }

                sheet.Range[10, 7].Text = "Repair Work Order:" + RD.ProjectTag;
                sheet.Range[11, 7].Text = "RMA Number:" + RD.RMANumber;
                sheet.Range[12, 7].Text = "Submission Date:" + RD.ReceiveDate.ToShortDateString();



                ClientLocationManager clm = new ClientLocationManager(User.Identity.Name);
                ClientLocation cl = clm.GetClientLocation(ctx, RD.ClientLocationID);
                if (cl != null)
                {
                    sheet.Range[16, 2].Text = "Client #:" + cl.ScanKey;                                                //rdm.GetReceiveDetailItem_DataElement(RD.ReceiveDetailID, "ClientNumber");
                    sheet.Range[16, 5].Text = cl.Name + " " + cl.CompanyName;
                    sheet.Range[17, 5].Text = cl.AddressLine1 + Environment.NewLine + cl.AddressLine2 + Environment.NewLine + cl.City + Environment.NewLine + cl.StateOrProvince + Environment.NewLine + cl.PostalCode;
                }


                sheet.Range[20, 2].Text = "Type:" + rdm.GetReceiveDetailItem_DataElement(ctx, RD.ReceiveDetailID, "Receipt Type");
                sheet.Range[20, 4].Text = "Second type of tracking#:" + rdm.GetReceiveDetailItem_DataElement(ctx, RD.ReceiveDetailID, "SecondType");
                sheet.Range[20, 7].Text = "Reference Number:" + rdm.GetReceiveDetailItem_DataElement(ctx, RD.ReceiveDetailID, "ReferenceNumber");

                sheet.Range[22, 2].Text = "Manufacturer:" + rdm.GetReceiveDetailItem_DataElement(ctx, RD.ReceiveDetailID, "Manufacturer");
                sheet.Range[23, 2].Text = "Model:" + rdm.GetReceiveDetailItem_DataElement(ctx, RD.ReceiveDetailID, "Model");
                sheet.Range[24, 2].Text = "Colour:" + rdm.GetReceiveDetailItem_DataElement(ctx, RD.ReceiveDetailID, "Colour");
                sheet.Range[25, 2].Text = "Carrier:" + rdm.GetReceiveDetailItem_DataElement(ctx, RD.ReceiveDetailID, "Carrier");

                sheet.Range[27, 2].Text = "Accessories Shipping:" + rdm.GetReceiveDetailItem_DataElement(ctx, RD.ReceiveDetailID, "Accessories Shipping");

                BarcodeImage = GetBarcodeImage_Base(RD.ESN);
                sheet.Pictures.AddPicture(29, 2, BarcodeImage);

                sheet.Range[34, 2].Text = "IMEI:" + RD.ESN;

                sheet.Range[36, 2].Text = "Customer Complaint:" + rdm.GetReceiveDetailItem_DataElement(ctx, RD.ReceiveDetailID, "Complaint") + " " + rdm.GetReceiveDetailItem_DataElement(ctx, RD.ReceiveDetailID, "Complaint 2");
                sheet.Range[38, 2].Text = "Return Reason:" + rdm.GetReceiveDetailItem_DataElement(ctx, RD.ReceiveDetailID, "Receipt Type");


                //int row = 15;
                //ExportPickingSlip_PG1_Header(OH, sheet);
                //foreach (clsOrderDetailLine dl in OH.OrderDetailLines)
                //{
                //   //  dl.LoadReceiveDetail();
                //    ExportPickingSlip_PG1_Detail(dl, sheet,ref row, 36);
                //    row++;
                //}
            }
        }


        //void ExportPickingSlip_PG1_Header(clsOrderHeader OH, IWorksheet sheet)
        //{
        //    sheet.Range[5, 6].Text = "Order Number:" + OH.OrderNumber;
        //    sheet.Range[7, 6].Text = "Customer PO:" + OH.CustomerPO;
        //    sheet.Range[9, 6].Text = "Date:" + DateTime.Now.ToShortDateString();

        //    int row = 12;
        //    sheet.Range[row, 1].Text = "Ordered";
        //    sheet.Range[row + 1, 1].Text = "QTY";
        //    sheet.Range[row, 2].Text = "Pick";
        //    sheet.Range[row + 1, 2].Text = "QTY";
        //    sheet.Range[row + 1, 3].Text = "Description";

        //}
        //void ExportPickingSlip_PG1_Detail(clsOrderDetailLine ODL, IWorksheet sheet,ref int RowStart, int RowEnd)
        //{
        //    int row = RowStart - 1;
        //    decimal left = ODL.QTY - ODL._OrderDetailReceiveLines.Count;
        //    sheet.Range[row, 1].Text = ODL.QTY.ToString();
        //    sheet.Range[row, 2].Text = left.ToString();
        //   //sheet.Range[row, 3].Text = ODL.Desc_Text;
        //    sheet.Range[row, 3].Text = ODL.Desc_Text.Replace(System.Environment.NewLine, " ");
        //}


        #endregion

        #region Excel SpotCountReport

        string ExportSpotCountReportToExcel(string BinNumber)
        {
            string msg = "";
            try
            {
                string ReportUser = "Test";
                ReportUser = User.Identity.Name;

                ExcelEngine excelEngine = new ExcelEngine();
                IApplication application = excelEngine.Excel;
                IWorkbook workbook = application.Workbooks.Create(1);
                IWorksheet sheet = workbook.Worksheets[0];

                // Get Order Header Records.
                //clsOrderHeader OH = new clsOrderHeader(OrderHeaderID);
                //ExportPickingSlip(OH, sheet);
                bool ShowHeader = true;
                ReceiveDetailManager rdm = new ReceiveDetailManager(User.Identity.Name);
                List<SpotCountReport> Spot = rdm.GetSpotCountReport(BinNumber);
                List<SpotCountReport_B> SpotSummary = rdm.GetSpotCountReportSummary(BinNumber);
                int Row = 1;
                int Col = 1;
                foreach (SpotCountReport sr in Spot)
                {
                    if (ShowHeader == true)
                    {
                        Row = Spot_Count_Header(sheet, Row, Col, BinNumber, sr.GrandTotal.ToString(), sr.Project);
                        ShowHeader = false;
                    }
                    Row++;
                    SetExcelRangeBackColour(sheet, ref Row, ref Col, Row, Col + 5, System.Drawing.Color.PaleGreen, ExcelHAlign.HAlignCenter);
                    Row++;
                    Row = Spot_Count_Detail(sheet, Row, Col, sr);
                }
                // Print out the Summar Side of the report
                //

                Row = Spot_Count_SummaryHeader(sheet);
                foreach (SpotCountReport_B sr in SpotSummary.OrderBy(x => x.Make).ThenBy(x => x.Model).ThenBy(x => x.Colour).ThenBy(x => x.Grade))
                {
                    Row++;
                    //SetExcelRangeBackColour(sheet, ref Row, ref Col, Row, Col + 5, System.Drawing.Color.PaleGreen, ExcelHAlign.HAlignCenter);
                    //Row++;
                    Row = Spot_Count_SummaryDetail(sheet, Row, 8, sr);
                }
                //sheet.UsedRange.AutofitColumns();
                sheet.SetColumnWidth(1, 8.35);
                sheet.SetColumnWidth(2, 7.5);
                sheet.SetColumnWidth(3, 7.35);
                sheet.SetColumnWidth(4, 10.0);
                sheet.SetColumnWidth(5, 7.5);
                sheet.SetColumnWidth(6, 5.5);
                sheet.SetColumnWidth(7, 2.0);
                sheet.SetColumnWidth(8, 10.5);
                sheet.SetColumnWidth(9, 10.5);
                sheet.SetColumnWidth(10, 5.5);
                sheet.SetColumnWidth(11, 5.5);
                sheet.SetColumnWidth(12, 5);

                //
                workbook.SaveAs("Spot.xls", Page.Response, ExcelDownloadType.Open);
                workbook.Close();
                // Dispose the Excel engine
                excelEngine.Dispose();
            }

            catch (Exception ex)
            {
                msg = ex.Message;
                //ex.
                //Response.Write(ex.Message);
            }
            finally
            {
                //cmd.Connection.Close();
                //cn.Close();
            }
            return msg;
        }
        private int Spot_Count_Detail(IWorksheet sheet, int Row, int Col, SpotCountReport sr)
        {
            int xcol = 0;

            sheet.Range[Row, Col].Text = "Make";
            sheet.Range[Row, Col + 2].Text = "Model";
            sheet.Range[Row, Col + 4].Text = "Colour";

            sheet.Range[Row, Col].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignRight;
            sheet.Range[Row, Col + 2].HorizontalAlignment = ExcelHAlign.HAlignRight;
            sheet.Range[Row, Col + 4].HorizontalAlignment = ExcelHAlign.HAlignRight;


            xcol = Col + 1; SetExcelRangeBackColour(sheet, ref Row, ref xcol, Row, xcol, System.Drawing.Color.LightBlue, ExcelHAlign.HAlignCenter);
            xcol = Col + 3; SetExcelRangeBackColour(sheet, ref Row, ref xcol, Row, xcol, System.Drawing.Color.LightBlue, ExcelHAlign.HAlignCenter);
            xcol = Col + 5; SetExcelRangeBackColour(sheet, ref Row, ref xcol, Row, xcol, System.Drawing.Color.LightBlue, ExcelHAlign.HAlignCenter);

            sheet.Range[Row, Col + 1].Text = sr.Make;
            sheet.Range[Row, Col + 3].Text = sr.Model;
            sheet.Range[Row, Col + 5].Text = sr.Colour;
            sheet.Range[Row, Col + 1].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignLeft;
            sheet.Range[Row, Col + 3].HorizontalAlignment = ExcelHAlign.HAlignLeft;
            sheet.Range[Row, Col + 5].HorizontalAlignment = ExcelHAlign.HAlignLeft;

            Row++;
            sheet.Range[Row, Col].Text = "Received";
            sheet.Range[Row, Col + 4].Text = "QC'd";
            sheet.Range[Row, Col].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignRight;
            sheet.Range[Row, Col + 4].HorizontalAlignment = ExcelHAlign.HAlignRight;
            sheet.Range[Row, Col + 1].Text = sr.Received.ToString();
            xcol = Col + 1; SetExcelRangeBackColour(sheet, ref Row, ref xcol, Row, xcol, System.Drawing.Color.LightPink, ExcelHAlign.HAlignCenter);

            sheet.Range[Row, Col + 5].Text = sr.QCed.ToString();
            sheet.Range[Row, Col + 1].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignLeft;
            sheet.Range[Row, Col + 5].HorizontalAlignment = ExcelHAlign.HAlignLeft;


            //---------------------------------------------------
            Row++;
            sheet.Range[Row, Col].Text = "A Stock";
            sheet.Range[Row, Col + 2].Text = "B Stock";
            sheet.Range[Row, Col + 4].Text = "C Stock";
            sheet.Range[Row, Col].HorizontalAlignment = ExcelHAlign.HAlignRight;
            sheet.Range[Row, Col + 2].HorizontalAlignment = ExcelHAlign.HAlignRight;
            sheet.Range[Row, Col + 4].HorizontalAlignment = ExcelHAlign.HAlignRight;
            sheet.Range[Row, Col + 1].Text = sr.aStock.ToString();
            sheet.Range[Row, Col + 3].Text = sr.bStock.ToString();
            sheet.Range[Row, Col + 5].Text = sr.cStock.ToString();
            sheet.Range[Row, Col + 1].HorizontalAlignment = ExcelHAlign.HAlignLeft;
            sheet.Range[Row, Col + 3].HorizontalAlignment = ExcelHAlign.HAlignLeft;
            sheet.Range[Row, Col + 5].HorizontalAlignment = ExcelHAlign.HAlignLeft;

            //---------------------------------------------------
            Row++;
            sheet.Range[Row, Col].Text = "Defective";
            sheet.Range[Row, Col].HorizontalAlignment = ExcelHAlign.HAlignRight;
            sheet.Range[Row, Col + 1].Text = sr.DOA.ToString();
            sheet.Range[Row, Col + 1].HorizontalAlignment = ExcelHAlign.HAlignLeft;

            sheet.Range[Row, Col + 2].Text = "NFF";
            sheet.Range[Row, Col + 2].HorizontalAlignment = ExcelHAlign.HAlignRight;
            sheet.Range[Row, Col + 3].Text = sr.NFF.ToString();
            sheet.Range[Row, Col + 3].HorizontalAlignment = ExcelHAlign.HAlignLeft;

            sheet.Range[Row, Col + 4].Text = "Customer Abused";
            sheet.Range[Row, Col + 4].HorizontalAlignment = ExcelHAlign.HAlignRight;
            sheet.Range[Row, Col + 5].Text = sr.CustAbuse.ToString();
            sheet.Range[Row, Col + 5].HorizontalAlignment = ExcelHAlign.HAlignLeft;

            //---------------------------------------------------
            Row++;
            sheet.Range[Row, Col].Text = "Type Code Rejected";
            sheet.Range[Row, Col].HorizontalAlignment = ExcelHAlign.HAlignRight;
            sheet.Range[Row, Col + 1].Text = sr.TypeCodeRejected.ToString();
            sheet.Range[Row, Col + 1].HorizontalAlignment = ExcelHAlign.HAlignLeft;

            sheet.Range[Row, Col + 2].Text = "Not Assessed";
            sheet.Range[Row, Col + 2].HorizontalAlignment = ExcelHAlign.HAlignRight;
            sheet.Range[Row, Col + 3].Text = sr.NotAssessed.ToString();
            sheet.Range[Row, Col + 3].HorizontalAlignment = ExcelHAlign.HAlignLeft;

            //--------------------------------------------------------
            Row++;
            sheet.Range[Row, Col].Text = "Repaired";
            sheet.Range[Row, Col].HorizontalAlignment = ExcelHAlign.HAlignRight;
            sheet.Range[Row, Col + 1].Text = sr.Repaired.ToString();
            sheet.Range[Row, Col + 1].HorizontalAlignment = ExcelHAlign.HAlignLeft;

            sheet.Range[Row, Col + 2].Text = "Replaced";
            sheet.Range[Row, Col + 2].HorizontalAlignment = ExcelHAlign.HAlignRight;
            sheet.Range[Row, Col + 3].Text = sr.Replaced.ToString();
            sheet.Range[Row, Col + 3].HorizontalAlignment = ExcelHAlign.HAlignLeft;


            return Row;
        }
        private int Spot_Count_Header(IWorksheet sheet, int Row, int Col, string BinNumber, string TotalUnits, string Project)
        {

            SetExcelRangeBackColour(sheet, ref Row, ref Col, Row + 3, Col + 5, System.Drawing.Color.PaleGreen, ExcelHAlign.HAlignCenter);
            //SetExcelRangeBackColour(sheet, ref Row, ref Col, Row+3, Col + 5, System.Drawing.Color.FromArgb(234, 241, 221), ExcelHAlign.HAlignCenter);

            SetExcelRange(sheet, ref Row, ref Col, Row, Col + 5, "Spot Count Report - Bin " + BinNumber);
            Row += 2; Col = 1;
            SetExcelRange(sheet, ref Row, ref Col, Row, Col + 1, "Project");
            Col = 3;
            SetExcelRange(sheet, ref Row, ref Col, Row, Col + 1, "Date");
            Col = 5;
            SetExcelRange(sheet, ref Row, ref Col, Row, Col + 1, "Units");

            Row += 1; Col = 1;
            SetExcelRange(sheet, ref Row, ref Col, Row, Col + 1, Project);
            Col = 3;
            SetExcelRange(sheet, ref Row, ref Col, Row, Col + 1, DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
            Col = 5;
            SetExcelRange(sheet, ref Row, ref Col, Row, Col + 1, TotalUnits);
            return Row;
        }
        private int Spot_Count_SummaryHeader(IWorksheet sheet)
        {
            int Row = 1;
            int Col = 8;
            SetExcelRangeBackColour(sheet, ref Row, ref Col, Row + 1, Col + 4, System.Drawing.Color.PaleGreen, ExcelHAlign.HAlignCenter);
            //SetExcelRangeBackColour(sheet, ref Row, ref Col, Row+3, Col + 5, System.Drawing.Color.FromArgb(234, 241, 221), ExcelHAlign.HAlignCenter);
            SetExcelRange(sheet, ref Row, ref Col, Row + 1, Col + 4, "Quick Summary");
            Row += 2; //Col = 1;
            SetExcelRange(sheet, ref Row, ref Col, Row, Col, "Manufacturer");
            Col++;
            SetExcelRange(sheet, ref Row, ref Col, Row, Col, "Model");
            Col++;
            SetExcelRange(sheet, ref Row, ref Col, Row, Col, "Colour");

            Col++;
            SetExcelRange(sheet, ref Row, ref Col, Row, Col, "Grade");
            Col++;
            SetExcelRange(sheet, ref Row, ref Col, Row, Col, "Total");
            return Row;
        }
        private int Spot_Count_SummaryDetail(IWorksheet sheet, int Row, int Col, SpotCountReport_B sr)
        {
            int xcol = 0;
            sheet.Range[Row, Col].Text = sr.Make;
            sheet.Range[Row, Col + 1].Text = sr.Model;
            sheet.Range[Row, Col + 2].Text = sr.Colour;
            sheet.Range[Row, Col + 3].Text = sr.Grade;
            sheet.Range[Row, Col + 4].Text = sr.Received.ToString();

            xcol = Col + 1; SetExcelRangeBackColour(sheet, ref Row, ref xcol, Row, xcol, System.Drawing.Color.LightBlue, ExcelHAlign.HAlignCenter);
            xcol = Col + 4; SetExcelRangeBackColour(sheet, ref Row, ref xcol, Row, xcol, System.Drawing.Color.LightPink, ExcelHAlign.HAlignCenter);
            return Row;
        }



        #endregion

        #region Excel Picking SLip
        string ExportPickingSlipToExcel(decimal OrderHeaderID)
        {
            string msg = "";
            try
            {
                string ReportUser = "Test";
                ReportUser = User.Identity.Name;

                string templatePath = Page.MapPath("~/Templates/OrderShipping/Picking_Slip.xls");
                ExcelEngine excelEngine = new ExcelEngine();
                IApplication application = excelEngine.Excel;
                IWorkbook workbook = application.Workbooks.Open(templatePath, ExcelOpenType.Automatic);
                IWorksheet sheet = workbook.Worksheets[0];

                // Get Order Header Records.
                clsOrderHeader OH = new clsOrderHeader(OrderHeaderID);
                ExportPickingSlip(OH, sheet);
                workbook.SaveAs("Picking.xls", Page.Response, ExcelDownloadType.Open);
                workbook.Close();
                // Dispose the Excel engine
                excelEngine.Dispose();
            }

            catch (Exception ex)
            {
                msg = ex.Message;
                //ex.
                //Response.Write(ex.Message);
            }
            finally
            {
                //cmd.Connection.Close();
                //cn.Close();
            }
            return msg;
        }
        void ExportPickingSlip(clsOrderHeader OH, IWorksheet sheet)
        {
            int row = 17;
            ExportPickingSlip_PG1_Header(OH, sheet);
            foreach (clsOrderDetailLine dl in OH.OrderDetailLines)
            {
                //  dl.LoadReceiveDetail();
                ExportPickingSlip_PG1_Detail(dl, sheet, ref row, 36);
                row++;
            }
        }
        void ExportPickingSlip_PG1_Header(clsOrderHeader OH, IWorksheet sheet)
        {
            sheet.Range[5, 6].Text = "Order Number:" + OH.OrderNumber;
            sheet.Range[7, 6].Text = "Customer PO:" + OH.CustomerPO;
            sheet.Range[9, 6].Text = "Date:" + DateTime.Now.ToShortDateString();

            if (OH.InternalNote != null) { sheet.Range[10, 1].Text = "Internal Note:" + OH.InternalNote; }
            if (OH.DeliveryNote != null) { sheet.Range[11, 1].Text = "Delivery Note:" + OH.DeliveryNote; }

            sheet.Range[6, 1].Text = OH.ClientCompany.CompanyName;
            sheet.Range[7, 1].Text = "Attention:" + OH.ClientCompany.ContactName;
            sheet.Range[8, 1].Text = OH.ClientCompany.AddressLine1;
            sheet.Range[9, 1].Text = OH.ClientCompany.AddressLine2;
            sheet.Range[10, 1].Text = OH.ClientCompany.City + "," + OH.ClientCompany.StateOrProvince;
            sheet.Range[11, 1].Text = OH.ClientCompany.PostalCode;

            //// Ship To
            //sheet.Range[12, 6].Text = OH.ShipToCompany.CompanyName;
            //sheet.Range[13, 6].Text = "Attention:" + OH.ShipToCompany.ContactName;
            //sheet.Range[14, 6].Text = OH.ShipToCompany.AddressLine1;
            //sheet.Range[15, 6].Text = OH.ShipToCompany.AddressLine2;
            //sheet.Range[16, 6].Text = OH.ShipToCompany.City + "," + OH.ShipToCompany.StateOrProvince;
            //sheet.Range[17, 6].Text = OH.ShipToCompany.PostalCode;



            int row = 14;
            sheet.Range[row, 1].Text = "Ordered";
            sheet.Range[row + 1, 1].Text = "QTY";
            sheet.Range[row, 2].Text = "Pick";
            sheet.Range[row + 1, 2].Text = "QTY";
            sheet.Range[row + 1, 3].Text = "Description";

        }
        void ExportPickingSlip_PG1_Detail(clsOrderDetailLine ODL, IWorksheet sheet, ref int RowStart, int RowEnd)
        {
            int row = RowStart - 1;
            decimal left = ODL.QTY - ODL._OrderDetailReceiveLines.Count;
            sheet.Range[row, 1].Text = ODL.QTY.ToString();
            sheet.Range[row, 2].Text = left.ToString();
            //sheet.Range[row, 3].Text = ODL.Desc_Text;
            sheet.Range[row, 3].Text = ODL.Desc_Text.Replace(System.Environment.NewLine, " ");
        }
        #endregion

        #region Excel Packing SLip
        string ExportPackingSlipToExcel(decimal OrderHeaderID)
        {
            string msg = "";
            try
            {
                string ReportUser = "Test";
                ReportUser = User.Identity.Name;

                string templatePath = Page.MapPath("~/Templates/OrderShipping/Packing_Slip.xls");
                ExcelEngine excelEngine = new ExcelEngine();
                IApplication application = excelEngine.Excel;
                IWorkbook workbook = application.Workbooks.Open(templatePath, ExcelOpenType.Automatic);
                IWorksheet sheet = workbook.Worksheets[0];
                // Get Order Header Records.
                clsOrderHeader OH = new clsOrderHeader(OrderHeaderID);
                ExportPackingSlip(OH, sheet);
                workbook.SaveAs("Packing.xls", Page.Response, ExcelDownloadType.Open);
                workbook.Close();
                // Dispose the Excel engine
                excelEngine.Dispose();
            }

            catch (Exception ex)
            {
                msg = ex.Message;
                //ex.
                //Response.Write(ex.Message);
            }
            finally
            {
                //cmd.Connection.Close();
                //cn.Close();
            }
            return msg;
        }


        void ExportPackingSlip(clsOrderHeader OH, IWorksheet sheet)
        {
            int currentRow = 21;

            ExportPackingSlip_PG1_Header(OH, sheet);
            foreach (clsOrderDetailLine dl in OH.OrderDetailLines)
            {
                // dl.LoadReceiveDetail();
                ExportPackingSlip_PG1_Detail(dl, sheet, ref currentRow, 36);

                currentRow += 2;
            }
        }




        void ExportPackingSlip_PG1_Header(clsOrderHeader OH, IWorksheet sheet)
        {
            //            System.Drawing.Image BarcodeImage = GetBarcodeImage(OH.OrderNumber);


            System.Drawing.Image BarcodeImage = GetBarcodeImage_Base(OH.OrderNumber);
            sheet.Pictures.AddPicture(2, 6, BarcodeImage);

            //System.Drawing.Image BarcodeImagex = GetBarcodeImage("GMP12345678");
            //sheet.Pictures.AddPicture(4, 4, BarcodeImagex);

            sheet.Range[5, 6].Text = "Order Number:" + OH.OrderNumber;
            sheet.Range[7, 6].Text = "Customer PO:" + OH.CustomerPO;
            sheet.Range[8, 6].Text = "Waybill Number:" + OH.WaybillNumber;
            sheet.Range[9, 6].Text = "Date:" + DateTime.Now.ToShortDateString();
            // Bill To
            sheet.Range[12, 2].Text = OH.ClientCompany.CompanyName;
            sheet.Range[13, 2].Text = "Attention:" + OH.ClientCompany.ContactName;
            sheet.Range[14, 2].Text = OH.ClientCompany.AddressLine1;
            sheet.Range[15, 2].Text = OH.ClientCompany.AddressLine2;
            sheet.Range[16, 2].Text = OH.ClientCompany.City + "," + OH.ClientCompany.StateOrProvince;
            sheet.Range[17, 2].Text = OH.ClientCompany.PostalCode;

            // Ship To
            sheet.Range[12, 6].Text = OH.ShipToCompany.CompanyName;
            sheet.Range[13, 6].Text = "Attention:" + OH.ShipToCompany.ContactName;
            sheet.Range[14, 6].Text = OH.ShipToCompany.AddressLine1;
            sheet.Range[15, 6].Text = OH.ShipToCompany.AddressLine2;
            sheet.Range[16, 6].Text = OH.ShipToCompany.City + "," + OH.ShipToCompany.StateOrProvince;
            sheet.Range[17, 6].Text = OH.ShipToCompany.PostalCode;

            if (OH.DeliveryNote != null) { sheet.Range[19, 1].Text = "Delivery Note:" + OH.DeliveryNote; }


        }
        int ExportPackingSlip_PG2_Header(clsOrderHeader OH, IWorksheet sheet, int RowStart)
        {
            sheet.Range[RowStart + 1, 6].Text = "Order Number:" + OH.OrderNumber;
            sheet.Range[RowStart + 3, 6].Text = "Customer PO:" + OH.CustomerPO;
            sheet.Range[RowStart + 4, 6].Text = "Waybill Number:" + OH.WaybillNumber;
            sheet.Range[RowStart + 5, 6].Text = "Date:" + DateTime.Now.ToShortDateString();
            return RowStart + 5;
        }
        void ExportPackingSlip_PG1_Detail(clsOrderDetailLine ODL, IWorksheet sheet, ref int RowStart, int RowEnd)
        {
            int row = RowStart - 1;

            int col = 1;
            string LastSku = "badsku";
            foreach (clsOrderDetailLineReceive rl in ODL._OrderDetailReceiveLines)
            {
                if (LastSku.ToUpper() != rl.SKU.ToUpper())
                {
                    var xcount = ODL._OrderDetailReceiveLines.Where(x => x.SKU.ToUpper() == rl.SKU.ToUpper()).Count();
                    row += 2;
                    LastSku = rl.SKU;
                    sheet.Range[row, 1].Text = "BOX";
                    sheet.Range[row, 3, row, 5].Text = "Description";
                    sheet.MigrantRange[row, 3, row, 5].Merge();
                    sheet.Range[row, 6].Text = "QTY";

                    row++;
                    sheet.Range[row, 1].Text = rl.SKU;
                    sheet.Range[row, 3, row, 5].Text = ODL.Desc_Text.Replace(System.Environment.NewLine, " "); ;
                    sheet.MigrantRange[row, 3, row, 5].Merge();
                    sheet.Range[row, 6].Text = xcount.ToString();                   // ODL.QTY.ToString();

                    row += 2;
                    sheet.Range[row, 1].Text = "IMEI";

                    col = 1;
                    row += 1;
                }

                sheet.Range[row, col].Text = rl.ESN;
                switch (col)
                {
                    case 1:
                        col = 4;
                        break;
                    case 4:
                        col = 6;
                        break;
                    case 6:
                        col = 1;
                        row++;
                        break;
                    default:
                        break;
                }
            }
            RowStart = row;
        }








        //private void FillSheetData(string cnString, ref string CommandText, ref int Row, ref int Col, string ReportUser, IWorksheet sheet, string ReportName, string ReportTitle)          //, IWorksheet TimeSheet, int TimeCol)
        //{

        //    try
        //    {
        //        // Summary Sheet
        //        sheet.Name = ReportName;
        //        // set up the headers.
        //        Row = 1; Col = 1;
        //        SetExcelRange(sheet, ref Row, ref Col, Row, 27, ReportTitle, System.Drawing.Color.DarkBlue, System.Drawing.Color.White);

        //        Row = 3; Col = 2;
        //        SetExcelRange(sheet, ref Row, ref Col, Row, 7, "Status", System.Drawing.Color.Cyan, System.Drawing.Color.Black);
        //        Row = 5; Col = 2;

        //        //TimeSheet.Range[1, 1].Text = "Report 1 Start:";
        //        //TimeSheet.Range[1, TimeCol].Text = DateTime.Now.ToString();
        //        CommandText = "BSPSummarize_02_pg1 '" + ReportUser + "','','1','" + ReportName + "'";
        //        PlaceData(sheet, ref Row, ref Col, CommandText, cnString, true);
        //        //TimeSheet.Range[2, 1].Text = "Report 1 End:";
        //        //TimeSheet.Range[2, TimeCol].DateTime = DateTime.Now;

        //        //TimeSheet.Range[3, 1].Text = "Report 2 Start:";
        //        //TimeSheet.Range[3, TimeCol].DateTime = DateTime.Now;
        //        Row = 3; Col = 9;
        //        SetExcelRange(sheet, ref Row, ref Col, Row, 14, "Pass", System.Drawing.Color.Cyan, System.Drawing.Color.Black);
        //        Row = 5; Col = 9;
        //        CommandText = "BSPSummarize_02_pg1 '" + ReportUser + "','P','2','" + ReportName + "'";
        //        PlaceData(sheet, ref Row, ref Col, CommandText, cnString, true);
        //        //TimeSheet.Range[4, 1].Text = "Report 2 End:";
        //        //TimeSheet.Range[4, TimeCol].DateTime = DateTime.Now;


        //        //TimeSheet.Range[5, 1].Text = "Report 3 Start:";
        //        //TimeSheet.Range[5, TimeCol].DateTime = DateTime.Now;
        //        Row = 3; Col = 16;
        //        SetExcelRange(sheet, ref Row, ref Col, Row, 23, "Failures", System.Drawing.Color.Cyan, System.Drawing.Color.Black);
        //        Row = 5; Col = 16;
        //        CommandText = "BSPSummarize_02_pg1 '" + ReportUser + "','F','3','" + ReportName + "'";
        //        PlaceData(sheet, ref Row, ref Col, CommandText, cnString, true);
        //        //TimeSheet.Range[6, 1].Text = "Report 3 End:";
        //        //TimeSheet.Range[6, TimeCol].DateTime = DateTime.Now;



        //        //TimeSheet.Range[7, 1].Text = "Report 4 Start:";
        //        //TimeSheet.Range[7, TimeCol].DateTime = DateTime.Now;
        //        Row = 3; Col = 25;
        //        SetExcelRange(sheet, ref Row, ref Col, Row, 26, "Totals", System.Drawing.Color.Cyan, System.Drawing.Color.Black);
        //        Row = 5; Col = 25;
        //        CommandText = "BSPSummarize_02_pg1 '" + ReportUser + "','','4','" + ReportName + "'";
        //        PlaceData(sheet, ref Row, ref Col, CommandText, cnString, false);
        //        //TimeSheet.Range[8, 1].Text = "Report 4 End:";
        //        //TimeSheet.Range[8, TimeCol].DateTime = DateTime.Now;


        //        //TimeSheet.Range[9, 1].Text = "Report 5 Start:";
        //        //TimeSheet.Range[9, TimeCol].DateTime = DateTime.Now;
        //        Row += 4; Col = 25;
        //        SetExcelRange(sheet, ref Row, ref Col, Row, 27, "Failure Codes", System.Drawing.Color.Cyan, System.Drawing.Color.Black);
        //        Row += 2; Col = 25;
        //        CommandText = "BSPSummarize_02_FaultCodes '" + ReportUser + "','" + ReportName + "'";
        //        PlaceData(sheet, ref Row, ref Col, CommandText, cnString, true);
        //        //TimeSheet.Range[10, 1].Text = "Report 5 End:";
        //        //TimeSheet.Range[10, TimeCol].DateTime = DateTime.Now;
        //    }

        //    catch (Exception ex)
        //    {
        //        //ex.
        //        //Response.Write(ex.Message);
        //    }
        //    finally
        //    {
        //        //cmd.Connection.Close();
        //        //cn.Close();
        //    }
        //}

        //private void SetExcelRange(IWorksheet sheet, ref int Row1, ref int Col1, int row2, int col2, string Text, System.Drawing.Color BackColour, System.Drawing.Color ForeColour)
        //{
        //    sheet.Range[Row1, Col1, row2, col2].Text = Text;
        //    sheet.MigrantRange[Row1, Col1, row2, col2].Merge();
        //    //sheet.Range[row1, col1, row2, col2].CellStyle.Color = BackColour;
        //    //sheet.Range[row1, col1, row2, col2]. = BackColour;

        //}

        //private static void PlaceData(IWorksheet sheet, ref int Row, ref int Col, string CommandText, string cnString, Boolean ShowHeaders)
        //{
        //    int StartRow = Row;
        //    int StartCol = Col;
        //    if (cnString != null)
        //    {
        //        //string cnString =  ;
        //        var cn = new SqlConnection(cnString);
        //        var cmd = new SqlCommand();
        //        try
        //        {
        //            cmd.CommandText = CommandText;
        //            cmd.CommandTimeout = 120;
        //            cmd.Connection = cn;
        //            cn.Open();
        //            SqlDataReader dr = cmd.ExecuteReader();

        //            if (ShowHeaders == true)
        //            {
        //                for (int count = 0; count < dr.FieldCount; count++)
        //                {
        //                    if (dr.GetName(count) != null)
        //                    {
        //                        sheet.Range[Row, Col].Text = dr.GetName(count);
        //                    }
        //                    ++Col;
        //                }
        //            }
        //            decimal dNum = 0;
        //            double ddNum = 0;
        //            while (dr.Read())
        //            {
        //                Col = StartCol;
        //                ++Row;
        //                for (int count = 0; count < dr.FieldCount; count++)
        //                {
        //                    if (!dr.IsDBNull(count))
        //                    {
        //                        if (dr.GetValue(count).ToString() != "0")
        //                        {
        //                            switch (dr.GetName(count).ToUpper())
        //                            {
        //                                case "POFFAILS":           // percent
        //                                    dNum = dr.GetDecimal(count);
        //                                    ddNum = (double)dNum;
        //                                    sheet.Range[Row, Col].Number = ddNum;
        //                                    break;
        //                                //case "TOTAL FAILED":
        //                                //case "FREQ":
        //                                case "A":
        //                                case "B":
        //                                case "C":
        //                                case "PASSC":
        //                                    //case "FAIL":
        //                                    //case "PASS":
        //                                    dNum = dr.GetDecimal(count);
        //                                    if (double.TryParse(dNum.ToString(), out ddNum) == false) { ddNum = 0; }
        //                                    sheet.Range[Row, Col].Number = ddNum;
        //                                    break;
        //                                default:
        //                                    sheet.Range[Row, Col].Text = dr.GetValue(count).ToString();
        //                                    break;
        //                            }
        //                        }
        //                    }
        //                    ++Col;
        //                }
        //            }
        //            sheet.Range[StartRow, StartCol, Row, Col].AutofitColumns();
        //        }
        //        catch (Exception ex)
        //        {
        //            //ex.
        //            string xx = ex.Message;
        //        }
        //        finally
        //        {
        //            cmd.Connection.Close();
        //            cn.Close();
        //        }
        //    }
        //}
        #endregion

        #region Excel Misc Utility
        private void SetExcelRange(IWorksheet sheet, ref int Row1, ref int Col1, int row2, int col2, string Text)
        {
            sheet.Range[Row1, Col1, row2, col2].Text = Text;
            sheet.MigrantRange[Row1, Col1, row2, col2].Merge();
        }
        private void SetExcelRange(IWorksheet sheet, ref int Row1, ref int Col1, int row2, int col2, string Text, System.Drawing.Color BackColour, ExcelHAlign Alignment)
        {
            sheet.Range[Row1, Col1, row2, col2].Text = Text;
            sheet.MigrantRange[Row1, Col1, row2, col2].Merge();
            SetExcelRangeBackColour(sheet, ref Row1, ref Col1, row2, col2, BackColour, Alignment);
        }
        private void SetExcelRangeBackColour(IWorksheet sheet, ref int Row1, ref int Col1, int row2, int col2, System.Drawing.Color BackColour, ExcelHAlign Alignment)
        {
            sheet.MigrantRange[Row1, Col1, row2, col2].CellStyle.HorizontalAlignment = Alignment;
            sheet.Range[Row1, Col1, row2, col2].CellStyle.Color = BackColour;
        }
        #endregion

        #region BarcodeUtlitity
        public System.Drawing.Image GetBarcodeImage(string Value)
        {
            clsBarcodeUtils bc = new clsBarcodeUtils();
            // bc.
            return bc.SaveBarcodeToImage(Value);
        }
        public System.Drawing.Image GetBarcodeImage_Base(string Value)
        {
            clsBarcodeUtils bc = new clsBarcodeUtils();
            // bc.
            string templatePath = Page.MapPath(@"..\IDAutomation/BC" + "_" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + RandomNumber(0, 9).ToString() + ".jpg");
            System.Drawing.Image IMG = bc.SaveBarcodeToImage_Base_02(Value, templatePath);

            return IMG;
        }

        private int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }


        #endregion
    }
}