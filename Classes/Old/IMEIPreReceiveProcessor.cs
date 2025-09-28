using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Text.RegularExpressions;

//using Factory_DataModel;
using BW_WebApp.DataManagers;
using Syncfusion.XlsIO;

namespace BW_WebApp.Classes
{
    public class IMEIPreReceiveProcessor
    {

        clsLog log;
        public string filename { get; set; }
        public string username { get; set; }
        public string projectname { get; set; }
        public decimal projectID { get; set; }
        public decimal clientlocationid { get; set; }

        decimal saveprocessID { get; set; }
        string saveprocessName { get; set; }
        decimal receiveprocessID { get; set; }
        string receiveprocessName { get; set; }
        decimal StatusID { get; set; }
        public int RowCount { get; set; }
        public bool isXLS { get; set; }
        bool isAssessmentComplete { get; set; }
        bool forceIMEI15 { get; set; }

        List<HeaderData> Header = new List<HeaderData>();
        List<string> attributeclonelist = new List<string>();
        List<CloneCellData> CloneList = new List<CloneCellData>();
        TranslationTable HeaderTranslationTable = new TranslationTable();
        List<PairIDValue> LocationScanCodes = new List<PairIDValue>();
        //HeaderData AssessmentComplete = new HeaderData(0, "Assessment");

        public ExcelEngine excelEngine = new ExcelEngine();
        public IApplication application = null;
        public IWorkbook workbook = null;

        public IMEIPreReceiveProcessor(string DrivePathFileNameXLS_toUpload, string UserName, decimal ClientLocationID, clsLog log, bool ForceIMEI15)
        {
            this.log = log;
            //attributeclonelist = AttributeCloneList;
            //attributeclonelist.Add("Carrier");
            //attributeclonelist.Add("Manufacturer");
            //attributeclonelist.Add("Model");
            //attributeclonelist.Add("Colour");
            //attributeclonelist.Add("Disposition");          // may need to remove this one.
            //attributeclonelist.Add("QC");                   // may need to change this to another name when Sandbox becomes live.
            //attributeclonelist.Add("Grade");                   // This is for when Sandbox becomes live.

            filename = DrivePathFileNameXLS_toUpload;
            isXLS = true;
            username = UserName;
            clientlocationid = ClientLocationID;
            //projectID = ProjectID;
            saveprocessName = "Save";
            receiveprocessName = "Receive";

            forceIMEI15 = ForceIMEI15;

            ReceiveDetailManager rm = new ReceiveDetailManager(username);
            StatusID = rm.GetStatusID("Active");

            //ProjectManager pm = new ProjectManager(UserName);
            //Project p = pm.GetProject(projectID);
            //projectname = p.Name;

            //ProcessManager prm = new ProcessManager(UserName);
            //saveprocessID = prm.GetProcessidFromName(saveprocessName);
            //receiveprocessID = prm.GetProcessidFromName(receiveprocessName,projectname);
        }

        #region PreviousPreReceive
        public string LoadIMEIDataPreviousRecord()
        {
            string rvalue = "";
            if (isXLS == true) { rvalue = LoadIMEIDatafromXLS_Previous(); }
            return rvalue;
        }
        public string LoadIMEIDatafromXLS_Previous()
        {
            this.log.LogIt("   LoadIMEIData Started:");


            LoadHeaderTranslationTable();
            RowCount = 0;
            string rvalue = "";
            DateTime starttime = DateTime.Now;
            //Step 2 : Instantiate the excel application object.
            application = excelEngine.Excel;
            //Open an existing spreadsheet which will be used as a template for generating the new spreadsheet.
            //After opening, the workbook object represents the complete in-memory object model of the template spreadsheet.
            workbook = application.Workbooks.Open(filename, ExcelOpenType.Automatic);
            //The first worksheet object in the worksheets collection is accessed.
            IWorksheet sheet = workbook.Worksheets[0];
            LoadLocationScanCodes();
            rvalue = LoadHeaderData(sheet);
            rvalue = LoadData_Previous(sheet);

            workbook.Save();
            //workbook.Close();
            //excelEngine.Dispose();
            DateTime endtime = DateTime.Now;
            TimeSpan diffResult = endtime.Subtract(starttime);
            rvalue = "(HH:MM:SS)" + diffResult.Hours.ToString() + ":" + diffResult.Minutes.ToString() + ":" + diffResult.Seconds.ToString() + "  -- Row Count:" + RowCount.ToString();
            return rvalue;
        }
        private string LoadData_Previous(IWorksheet sheet)
        {
            string rvalue = "";
            string Message = "";
            int eColumn = Header.Count + 3;
            sheet.Range[1, eColumn].Value = "Upload status";
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                ReceiveDetailManager rdm = new ReceiveDetailManager(username);
                string cellData = "";
                for (int row = 2; row < 100000; row++)
                {
                    try
                    {
                        if (sheet.Range[row, 1].Value == null || sheet.Range[row, 1].Value.Length == 0) { break; }
                        this.log.LogIt("  Reading Rows:" + row.ToString());
                        RowData rd = new RowData(projectID, clientlocationid, saveprocessID, receiveprocessID, projectname, saveprocessName, receiveprocessName, forceIMEI15, rdm);
                        for (int col = 1; col < Header.Count + 1; col++)
                        {
                            cellData = sheet.Range[row, col].Value;
                            var newString = string.Join(" ", Regex.Split(cellData, @"(?:\r\n|\n|\r)"));
                            if (newString.Length > 199) { newString = newString.Substring(0, 199).Trim(); }
                            this.log.LogIt("     Reading Cols:" + col.ToString() + ":" + newString);
                            rd.AddCellData(newString, Header.FirstOrDefault(x => x.col == col));
                        }
                        /////////////////////////;

                        //this.log.LogIt("Opening ReceiveDetailPreReceive");

                        ReceiveDetailPreReceive rdetail = null;
                        // Change the client if the scancode is set
                        if (rd.LocationScanCode.Length > 0)
                        {
                            //this.log.LogIt("if (rd.LocationScanCode.Length > 0)");
                            PairIDValue lsc = LocationScanCodes.FirstOrDefault(x => x.Desc.ToUpper() == rd.LocationScanCode.ToUpper());
                            if (lsc != null)
                            {
                                //this.log.LogIt("if (lsc != null)");
                                rd.LocationScanCodeLocationID = lsc.ID;
                                rd.clientlocationID = lsc.ID;
                            }
                        }

                        this.log.LogIt("Getting Ready to Save");
                        List<decimal> AttributesToDelete = new List<decimal>();
                        rdetail = rd.SavePreReceivePrevious(ctx, username, AttributesToDelete, log, out Message);
                        if (Message != "ESN already on file" && Message != "ESN already Queued")
                        {
                            //this.log.LogIt("ctx.ReceiveDetailPreReceives.InsertOnSubmit(rdetail)");
                            ctx.ReceiveDetailPreReceives.InsertOnSubmit(rdetail);

                        }
                        else
                        {
                            this.log.LogIt("     *** ESN Already on file:" + rdetail.ESN);
                        }
                        sheet.Range[row, eColumn].Value = Message;
                        RowCount += 1;
                        this.log.LogIt("ctx.SubmitChanges()");
                        ctx.SubmitChanges();
                        //foreach (decimal a in AttributesToDelete)
                        //{
                        //    rd.ReceiveDetailPreReceiveAttributes.Remove(a);
                        //}
                        ctx.SubmitChanges();
                        AttributesToDelete.Clear();

                        this.log.LogIt("ctx.SubmitChanges() - Done");
                    }
                    catch (Exception theException)
                    {
                        String errorMessage;
                        errorMessage = "Error: ";
                        errorMessage = String.Concat(errorMessage, theException.Message);
                        errorMessage = String.Concat(errorMessage, " Line: ");
                        errorMessage = String.Concat(errorMessage, theException.Source);
                        this.log.LogIt(errorMessage);
                        sheet.Range[row, eColumn].Value = errorMessage;
                        RowCount += 1;

                        ctx.SubmitChanges(System.Data.Linq.ConflictMode.ContinueOnConflict);

                    }




                }
                //ctx.SubmitChanges();
            }
            return rvalue;
        }

        #endregion

        #region NormalPrereceive
        public string LoadIMEIData()
        {
            string rvalue = "";
            if (isXLS == true) { rvalue = LoadIMEIDatafromXLS(); }
            return rvalue;
        }
        public string LoadIMEIDatafromXLS()
        {
            this.log.LogIt("   LoadIMEIData Started:");


            LoadHeaderTranslationTable();
            RowCount = 0;
            string rvalue = "";
            DateTime starttime = DateTime.Now;
            //Step 2 : Instantiate the excel application object.
            application = excelEngine.Excel;
            //Open an existing spreadsheet which will be used as a template for generating the new spreadsheet.
            //After opening, the workbook object represents the complete in-memory object model of the template spreadsheet.
            workbook = application.Workbooks.Open(filename, ExcelOpenType.Automatic);
            //The first worksheet object in the worksheets collection is accessed.
            IWorksheet sheet = workbook.Worksheets[0];
            LoadLocationScanCodes();
            rvalue = LoadHeaderData(sheet);
            rvalue = LoadData(sheet);

            workbook.Save();
            //workbook.Close();
            //excelEngine.Dispose();
            DateTime endtime = DateTime.Now;
            TimeSpan diffResult = endtime.Subtract(starttime);
            rvalue = "(HH:MM:SS)" + diffResult.Hours.ToString() + ":" + diffResult.Minutes.ToString() + ":" + diffResult.Seconds.ToString() + "  -- Row Count:" + RowCount.ToString();
            return rvalue;
        }
        private string LoadData(IWorksheet sheet)
        {
            string rvalue = "";
            string Message = "";
            int eColumn = Header.Count + 3;
            sheet.Range[1, eColumn].Value = "Upload status";
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                ReceiveDetailManager rdm = new ReceiveDetailManager(username);
                string cellData = "";
                for (int row = 2; row < 100000; row++)
                {
                    try
                    {
                        if (sheet.Range[row, 1].Value == null || sheet.Range[row, 1].Value.Length == 0) { break; }
                        this.log.LogIt("  Reading Rows:" + row.ToString());
                        RowData rd = new RowData(projectID, clientlocationid, saveprocessID, receiveprocessID, projectname, saveprocessName, receiveprocessName, forceIMEI15, rdm);
                        for (int col = 1; col < Header.Count + 1; col++)
                        {
                            cellData = sheet.Range[row, col].Value;
                            var newString = string.Join(" ", Regex.Split(cellData, @"(?:\r\n|\n|\r)"));
                            if (newString.Length > 199) { newString = newString.Substring(0, 199).Trim(); }
                            this.log.LogIt("     Reading Cols:" + col.ToString() + ":" + newString);
                            rd.AddCellData(newString, Header.FirstOrDefault(x => x.col == col));
                        }
                        /////////////////////////;

                        //this.log.LogIt("Opening ReceiveDetailPreReceive");

                        ReceiveDetailPreReceive rdetail = null;
                        // Change the client if the scancode is set
                        if (rd.LocationScanCode.Length > 0)
                        {
                            //this.log.LogIt("if (rd.LocationScanCode.Length > 0)");
                            PairIDValue lsc = LocationScanCodes.FirstOrDefault(x => x.Desc.ToUpper() == rd.LocationScanCode.ToUpper());
                            if (lsc != null)
                            {
                                //this.log.LogIt("if (lsc != null)");
                                rd.LocationScanCodeLocationID = lsc.ID;
                                rd.clientlocationID = lsc.ID;
                            }
                        }

                        this.log.LogIt("Getting Ready to Save");
                        List<decimal> AttributesToDelete = new List<decimal>();
                        rdetail = rd.SavePreReceive(ctx, username, AttributesToDelete, log, out Message);
                        if (Message != "ESN already on file" && Message != "ESN already Queued")
                        {
                            //this.log.LogIt("ctx.ReceiveDetailPreReceives.InsertOnSubmit(rdetail)");
                            ctx.ReceiveDetailPreReceives.InsertOnSubmit(rdetail);

                        }
                        else
                        {
                            this.log.LogIt("     *** ESN Already on file:" + rdetail.ESN);
                        }
                        sheet.Range[row, eColumn].Value = Message;
                        RowCount += 1;
                        this.log.LogIt("ctx.SubmitChanges()");
                        ctx.SubmitChanges();
                        //foreach (decimal a in AttributesToDelete)
                        //{
                        //    rd.ReceiveDetailPreReceiveAttributes.Remove(a);
                        //}
                        ctx.SubmitChanges();
                        AttributesToDelete.Clear();

                        this.log.LogIt("ctx.SubmitChanges() - Done");
                    }
                    catch (Exception theException)
                    {
                        String errorMessage;
                        errorMessage = "Error: ";
                        errorMessage = String.Concat(errorMessage, theException.Message);
                        errorMessage = String.Concat(errorMessage, " Line: ");
                        errorMessage = String.Concat(errorMessage, theException.Source);
                        this.log.LogIt(errorMessage);
                        sheet.Range[row, eColumn].Value = errorMessage;
                        RowCount += 1;

                        ctx.SubmitChanges(System.Data.Linq.ConflictMode.ContinueOnConflict);

                    }




                }
                //ctx.SubmitChanges();
            }
            return rvalue;
        }
        #endregion



        private string CleanText(string strValue)
        {
            strValue = strValue.Replace('"', ' ');
            strValue = strValue.Replace(" ", "");
            strValue = strValue.Replace(" ", "");
            strValue = strValue.Replace("/", "");
            strValue = strValue.Replace("*", "");
            strValue = strValue.Replace("#", "");
            strValue = strValue.Replace(".", "");
            return strValue;
        }

        private string LoadHeaderData(IWorksheet sheet)
        {
            QuestionManager qm = new QuestionManager(username);
            List<string> ESNDetail = new List<string>(new string[] { "ESN", "STARTPROCESSNAME", "CURRENTPROCESSNAME", "RECEIVEDETAILID", "CLIENTLOCATIONID", "CLIENTLOCATION", "RMANUMBER", "PROJECTTAG", "PROJECTNAME", "LOCATION DEALER CODE", "VERSION", "TYPE" });
            List<PairIDValue> QID_Name = qm.GetAllQuestionsPairIDName();

            // First lets add finish setting up the AssessmentComplete property for this class.
            //AssessmentComplete.Fill(qm, projectID, username, ESNDetail, QID_Name);            //"Evaluation Complete" is the option we are interested in.


            string rvalue = "";
            int row = 1;
            Header.Clear();
            for (int col = 1; col < 100000; col++)
            {
                if (sheet.Range[row, col].Value == null || sheet.Range[row, col].Value.Length == 0) { break; }
                HeaderData hd = new HeaderData(col, sheet.Range[row, col].Value);
                hd.text = HeaderTranslationTable.Translate(hd.text);
                hd.Fill(qm, projectID, username, ESNDetail, QID_Name);
                //if (attributeclonelist.Contains(hd.text))  { hd.isCloneColumn = true; }

                Header.Add(hd);
            }
            return rvalue;
        }
        private void LoadLocationScanCodes()
        {
            ClientLocationManager clm = new ClientLocationManager(username);
            LocationScanCodes = clm.GetClientLocationScanCodes();
        }
        public void LoadHeaderTranslationTable()
        {
            HeaderTranslationTable.Add("SOURCE", "TARGET");
        }
    }
}