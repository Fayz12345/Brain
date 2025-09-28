using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
//using Factory_DataModel;
using BW_WebApp.DataManagers;
using Syncfusion.XlsIO;

namespace BW_WebApp.Classes
{
    public class IMEIUploadProcessor
    {

        clsLog log;
        public string filename { get; set; }
        public string filenameNoPath { get; set; }
        public string username { get; set; }
        public string projectname { get; set; }
        public decimal projectID { get; set; }
        public decimal clientlocationid { get; set; }
        public string IFSSite { get; set; }
        public string IFSProject { get; set; }
        public string IFSVendor { get; set; }
        public bool isThreaded { get; set; }

        decimal saveprocessID { get; set; }
        string saveprocessName { get; set; }
        decimal receiveprocessID { get; set; }
        string receiveprocessName { get; set; }
        decimal StatusID { get; set; }
        public int RowCount { get; set; }
        public bool isXLS { get; set; }
        bool isAssessmentComplete { get; set; }
        bool cloneparentattribute { get; set; }
        bool forceIMEI15 { get; set; }
        //string _batchNumber { get; set; }
        string IFSTransactionType { get; set; }

        List<HeaderData> Header = new List<HeaderData>();
        List<string> attributeclonelist = new List<string>();
        List<string> attributeclonelist2 = new List<string>();
        List<CloneCellData> CloneList = new List<CloneCellData>();
        TranslationTable HeaderTranslationTable = new TranslationTable();
        List<PairIDValue> LocationScanCodes = new List<PairIDValue>();
        //HeaderData AssessmentComplete = new HeaderData(0, "Assessment");


        public ExcelEngine excelEngine = new ExcelEngine();
        public API_UploadDeviceBatch_Out api_batchdata { get; set; }
        public IApplication application = null;
        public IWorkbook workbook = null;
        RDExcelLogManagercs LogManager = null;



        //public IMEIUploadProcessor(string BatchNumber, string DrivePathFileNameXLS_toUpload, string UserName, decimal ProjectID, string Scankey, List<string> AttributeCloneList, clsLog log, bool ForceIMEI15, bool IsThreaded, string ifsTransType)
        public IMEIUploadProcessor(API_UploadDeviceBatch_Out Batch, string LogPath)         //string BatchNumber, string JSONString, string UserName, string Projectname, string ScankeyClient)
        {
            #region Setup Values
            isXLS = false;
            filenameNoPath = "";               // Path.GetFileName(DrivePathFileNameXLS_toUpload);
            filename = "";               // DrivePathFileNameXLS_toUpload;
            string BatchNumber;
            string ScankeyClient;
            BatchNumber = Batch.batch;
            username = Batch.username;
            projectname = Batch.project;
            ScankeyClient = Batch.client;
            api_batchdata = Batch;

            //decimal ProjectID = -1;
            string ifsTransType = "INV_RECEIPT";
            List<string> attributeclonelist = new List<string>();
            //if (chkCloneSeedData.Checked)       -- Usually set to false.
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
            log = new clsLog(LogPath, "Util_APIUpload_01_Log.txt", username, System.Configuration.ConfigurationManager.AppSettings["WriteLogUserNameToLog"]);

            //log = new clsLog(HttpContext.Current.Server.MapPath("~"), "Util_APIUpload_01_Log.txt", username, System.Configuration.ConfigurationManager.AppSettings["WriteLogUserNameToLog"]);
            if (System.Configuration.ConfigurationManager.AppSettings["WriteLog"].ToUpper() == "TRUE")
            {
                log.writeLogData = true;
            }
            #endregion
            //HttpContext.Current.Server.ScriptTimeout = 90000;
            log.LogIt("**** Bulk API Upload Started:" + Batch.batch + ":" + username);
            LogManager = new RDExcelLogManagercs(BatchNumber, username, GetUserIPAddress());
            log.LogIt("      RDExcelLogManagercs instantiated");
            //_batchNumber = BatchNumber;


            ReceiveDetailManager rm = new ReceiveDetailManager(username);
            log.LogIt("      ReceiveDetailManager instantiated");
            ClientLocation cl = rm.GetClientLocation(ScankeyClient);
            log.LogIt("      GetClientLocation instantiated");
            //decimal ClientLocationID = -1;
            ////////////////////////////////  --- NEED TO SET ID FROM SCANKEY
            isThreaded = false;
            cloneparentattribute = false;
            //attributeclonelist = AttributeCloneList;
            attributeclonelist2.Clear();
            foreach (string a in attributeclonelist)
            {
                attributeclonelist2.Add(a);
            }
            if (attributeclonelist.Count > 0) { cloneparentattribute = true; }
            //attributeclonelist.Add("Carrier");
            //attributeclonelist.Add("Manufacturer");
            //attributeclonelist.Add("Model");
            //attributeclonelist.Add("Colour");
            //attributeclonelist.Add("Disposition");          // may need to remove this one.
            //attributeclonelist.Add("QC");                   // may need to change this to another name when Sandbox becomes live.
            //attributeclonelist.Add("Grade");                   // This is for when Sandbox becomes live.
            //projectID = ProjectID;
            saveprocessName = "Save";
            receiveprocessName = "Receive";
            forceIMEI15 = true;
            IFSTransactionType = ifsTransType;
            StatusID = rm.GetStatusID("Active");

            IFSSite = "";
            IFSProject = "";
            IFSVendor = "";
            clientlocationid = -1;
            if (cl != null)
            {
                IFSSite = cl.IFSSite;
                IFSProject = cl.IFSProject;
                IFSVendor = cl.IFSPOVendor;
                clientlocationid = cl.ClientLocationID;
            }

            ProjectManager pm = new ProjectManager(username);
            log.LogIt("      ProjectManager instantiated:" + Batch.project);
            projectID = pm.GetProjectID(Batch.project);
            Project p = pm.GetProject(projectID);
            log.LogIt("      GetProject instantiated:" + username);
            projectname = p.Name;
            ProcessManager prm = new ProcessManager(username);
            log.LogIt("      ProcessManager instantiated");
            saveprocessID = prm.GetProcessidFromName(saveprocessName);
            receiveprocessID = prm.GetProcessidFromName(receiveprocessName, projectname);
        }
        public IMEIUploadProcessor(string BatchNumber, string DrivePathFileNameXLS_toUpload, string UserName, decimal ProjectID, string Scankey, List<string> AttributeCloneList, clsLog log, bool ForceIMEI15, bool IsThreaded, string ifsTransType)
        {
            LogManager = new RDExcelLogManagercs(BatchNumber, UserName, GetUserIPAddress());
            //_batchNumber = BatchNumber;


            ReceiveDetailManager rm = new ReceiveDetailManager(UserName);
            ClientLocation cl = rm.GetClientLocation(Scankey);
            //decimal ClientLocationID = -1;
            ////////////////////////////////  --- NEED TO SET ID FROM SCANKEY
            isThreaded = IsThreaded;
            this.log = log;
            cloneparentattribute = false;
            attributeclonelist = AttributeCloneList;
            attributeclonelist2.Clear();
            foreach (string a in attributeclonelist)
            {
                attributeclonelist2.Add(a);
            }
            if (AttributeCloneList.Count > 0) { cloneparentattribute = true; }
            //attributeclonelist.Add("Carrier");
            //attributeclonelist.Add("Manufacturer");
            //attributeclonelist.Add("Model");
            //attributeclonelist.Add("Colour");
            //attributeclonelist.Add("Disposition");          // may need to remove this one.
            //attributeclonelist.Add("QC");                   // may need to change this to another name when Sandbox becomes live.
            //attributeclonelist.Add("Grade");                   // This is for when Sandbox becomes live.
            filenameNoPath = Path.GetFileName(DrivePathFileNameXLS_toUpload);
            filename = DrivePathFileNameXLS_toUpload;
            isXLS = true;
            username = UserName;

            projectID = ProjectID;
            saveprocessName = "Save";
            receiveprocessName = "Receive";
            forceIMEI15 = ForceIMEI15;
            IFSTransactionType = ifsTransType;
            StatusID = rm.GetStatusID("Active");

            IFSSite = "";
            IFSProject = "";
            IFSVendor = "";
            clientlocationid = -1;
            if (cl != null)
            {
                IFSSite = cl.IFSSite;
                IFSProject = cl.IFSProject;
                IFSVendor = cl.IFSPOVendor;
                clientlocationid = cl.ClientLocationID;
            }

            ProjectManager pm = new ProjectManager(UserName);
            Project p = pm.GetProject(projectID);
            projectname = p.Name;

            ProcessManager prm = new ProcessManager(UserName);
            saveprocessID = prm.GetProcessidFromName(saveprocessName);
            receiveprocessID = prm.GetProcessidFromName(receiveprocessName, projectname);


        }

        public string LoadIMEIData()
        {
            this.log.LogIt("   LoadIMEIData Started:");
            LogManager.StartTimer();

            string rvalue = "";
            if (isXLS == true) { rvalue = LoadIMEIData_fromXLS(); } else { rvalue = LoadIMEIDatafromJSON(); }
            LogManager.StopTimer();
            return rvalue;
        }
        //public string LoadIMEIDataJSON()
        //{
        //    this.log.LogIt("   LoadIMEIData Started:");
        //    LogManager.StartTimer();

        //    string rvalue = "";
        //    if (isXLS == false) { rvalue = LoadIMEIDatafromJSON(); }

        //    LogManager.StopTimer();


        //    return rvalue;
        //}


        public string LoadIMEIData_fromXLS()
        {
            //if (IFSSite.ToUpper() == "C1CON")
            //{
            //    if (IFSTransactionType.ToUpper() != "INV_RECEIPT" && IFSTransactionType.ToUpper() != "DEVICEADJUSTIN") { return "Invalid transaction type for C1CON devices."; }
            //}
            //else if (IFSSite.ToUpper() == "C1NA")
            //{
            //    if (IFSTransactionType.ToUpper() != "PO_RECEIPT" && IFSTransactionType.ToUpper() != "PO_INITIATE" && IFSTransactionType.ToUpper() != "DEVICEADJUSTIN") { return "Invalid transaction type for C1NA devices."; }
            //}
            //else { return "Invalid IFSSite."; }
            string rvalue = "";

            LogManager.LogEntry("Upload-Start", "Start", "", -1, "", "Upload Started", false);
            this.log.LogIt("   LoadIMEIDatafromXLS Started:");
            LoadHeaderTranslationTable();
            RowCount = 0;

            DateTime starttime = DateTime.Now;
            excelEngine = new ExcelEngine();
            //Step 2 : Instantiate the excel application object.
            application = excelEngine.Excel;
            workbook = application.Workbooks.Open(filename, ExcelOpenType.Automatic);


            //The first worksheet object in the worksheets collection is accessed.
            IWorksheet sheet = workbook.Worksheets[0];


            if (clientlocationid < 1)
            {
                rvalue = "Invalid Client Location Scankey.";
            }
            else
            {
                LoadLocationScanCodes();
                rvalue = LoadHeaderData(sheet);
                LogManager.LogEntry("Upload-Data", "Progress", "", -1, "", "Starting to read row data.", true);
                rvalue = LoadData(sheet);
                //workbook.Save();
                //workbook.Close();
                //excelEngine.Dispose();
                DateTime endtime = DateTime.Now;
                TimeSpan diffResult = endtime.Subtract(starttime);
                rvalue = "(HH:MM:SS)" + diffResult.Hours.ToString() + ":" + diffResult.Minutes.ToString() + ":" + diffResult.Seconds.ToString() + "  -- Row Count:" + RowCount.ToString();
            }
            LogManager.StopTimer();
            LogManager.LogEntry("Upload-Done", "Summary", "", -1, "", rvalue, false);

            return rvalue;
        }
        public string LoadIMEIDatafromJSON()
        {
            string rvalue = "";

            LogManager.LogEntry("Upload-Start", "Start", "", -1, "", "Upload Started", false);
            this.log.LogIt("   LoadIMEIDatafromJSON Started:");
            LoadHeaderTranslationTable();
            RowCount = 0;

            DateTime starttime = DateTime.Now;
            excelEngine = new ExcelEngine();
            //Step 2 : Instantiate the excel application object.
            //application = excelEngine.Excel;
            //workbook = application.Workbooks.Open(filename, ExcelOpenType.Automatic);


            //The first worksheet object in the worksheets collection is accessed.
            //IWorksheet sheet = workbook.Worksheets[0];


            if (clientlocationid < 1)
            {
                rvalue = "Invalid Client Location Scankey.";
            }
            else
            {
                LoadLocationScanCodes();
                rvalue = LoadHeaderData(api_batchdata);
                LogManager.LogEntry("Upload-Data", "Progress", "", -1, "", "Starting to read row data.", true);
                rvalue = LoadData(api_batchdata);
                //workbook.Save();
                //workbook.Close();
                //excelEngine.Dispose();
                DateTime endtime = DateTime.Now;
                TimeSpan diffResult = endtime.Subtract(starttime);
                rvalue = api_batchdata.JSON();             // "(HH:MM:SS)" + diffResult.Hours.ToString() + ":" + diffResult.Minutes.ToString() + ":" + diffResult.Seconds.ToString() + "  -- Row Count:" + RowCount.ToString();
            }
            LogManager.StopTimer();
            LogManager.LogEntry("Upload-Done", "Summary", "", -1, "", rvalue, false);

            return rvalue;
        }
        private string LoadHeaderData(API_UploadDeviceBatch_Out Batch)
        {
            this.log.LogIt("   LoadHeaderData Started:");
            int NextFreeColumn = -1;
            QuestionManager qm = new QuestionManager(username);
            List<string> ESNDetail = new List<string>(new string[] { "ESN", "STARTPROCESSNAME", "CURRENTPROCESSNAME", "RECEIVEDETAILID", "CLIENTLOCATIONID", "CLIENTLOCATION", "RMANUMBER", "PROJECTTAG", "PROJECTNAME", "LOCATION DEALER CODE", "VERSION", "IFS CONDITION", "Site Location", "SKU" });
            //List<PairIDValue> QID_Name = qm.GetAllQuestionsPairIDName(projectID);
            List<PairIDValue> QID_Name = qm.GetAllQuestionsPairIDName();

            // First lets add finish setting up the AssessmentComplete property for this class.
            //AssessmentComplete.Fill(qm, projectID, username, ESNDetail, QID_Name);            //"Evaluation Complete" is the option we are interested in.


            string rvalue = "";
            int row = 1;
            Header.Clear();
            int col = 0;
            //for (int col = 1; col < 100000; col++)
            if (Batch.devices.Count > 0)
            {
                foreach (API_Device_Out d in Batch.devices)
                {
                    col = 0;
                    col++;
                    //API_DeviceAttribute esn = new API_DeviceAttribute("ESN", d.esn);
                    HeaderData esn = new HeaderData(col, "ESN");
                    //esn.Name = "ESN";
                    esn.text = HeaderTranslationTable.Translate(esn.text);
                    esn.Fill(qm, projectID, username, ESNDetail, QID_Name);
                    esn.isCloneColumn = false;
                    if (Header.Exists(x => x.Name == esn.Name) == false) { Header.Add(esn); }
                    foreach (API_DeviceAttribute_Out a in d.attributes)
                    {
                        col++;
                        if (a.attribute == null || a.attribute.Length == 0) { NextFreeColumn = col + 3; break; }
                        HeaderData hd = new HeaderData(col, a.attribute);
                        hd.text = HeaderTranslationTable.Translate(hd.text);
                        hd.Fill(qm, projectID, username, ESNDetail, QID_Name);
                        hd.isCloneColumn = false;
                        string acl = attributeclonelist.FirstOrDefault(x => (x.IndexOf(":") > -1 ? x.Substring(0, x.IndexOf(":")) : x.ToUpper()) == hd.text.ToUpper());
                        if (acl != null)
                        {
                            hd.isCloneColumn = true;
                            if (acl.IndexOf(":") > -1)
                            {
                                hd.isCloneTrasposeColumn = true;
                                hd.T_text = acl.Substring(acl.IndexOf(":") + 1);
                                hd.FillTranspose(qm, projectID, username, ESNDetail, QID_Name);
                                //sheet.Range[row, col].CellStyle.Color = System.Drawing.Color.Red;
                            }
                            attributeclonelist.Remove(acl);
                        }
                        if (Header.Exists(x => x.Name == hd.Name) == false) { Header.Add(hd); }
                    }
                    break;
                }
            }
            //// load up any remaining attributes.
            //foreach (string acl in attributeclonelist)
            //{
            //    sheet.Range[row, NextFreeColumn].Value = acl;
            //    sheet.Range[row, NextFreeColumn].CellStyle.Color = System.Drawing.Color.Red;
            //    string[] S = acl.Split(':');
            //    string s1 = S[0];
            //    string s2 = "";
            //    if (S.Length > 1 && S[1].Length > 0)
            //    {
            //        s2 = S[1];
            //    }
            //    HeaderData hd = new HeaderData(-1, acl);
            //    hd.text = s1;
            //    hd.Fill(qm, projectID, username, ESNDetail, QID_Name);
            //    hd.isCloneColumn = true;
            //    hd.col = NextFreeColumn;
            //    //hd
            //    NextFreeColumn++;
            //    if (s2.Length > 0)
            //    {
            //        hd.isCloneTrasposeColumn = true;
            //        hd.T_text = s2;
            //        hd.FillTranspose(qm, projectID, username, ESNDetail, QID_Name);
            //    }
            //    Header.Add(hd);
            //}

            return rvalue;
        }
        private string LoadHeaderData(IWorksheet sheet)
        {
            this.log.LogIt("   LoadHeaderData Started:");
            int NextFreeColumn = -1;
            QuestionManager qm = new QuestionManager(username);
            List<string> ESNDetail = new List<string>(new string[] { "ESN", "STARTPROCESSNAME", "CURRENTPROCESSNAME", "RECEIVEDETAILID", "CLIENTLOCATIONID", "CLIENTLOCATION", "RMANUMBER", "PROJECTTAG", "PROJECTNAME", "LOCATION DEALER CODE", "VERSION", "IFS CONDITION", "Site Location", "SKU" });
            //List<PairIDValue> QID_Name = qm.GetAllQuestionsPairIDName(projectID);
            List<PairIDValue> QID_Name = qm.GetAllQuestionsPairIDName();

            // First lets add finish setting up the AssessmentComplete property for this class.
            //AssessmentComplete.Fill(qm, projectID, username, ESNDetail, QID_Name);            //"Evaluation Complete" is the option we are interested in.


            string rvalue = "";
            int row = 1;
            Header.Clear();
            for (int col = 1; col < 100000; col++)
            {
                if (sheet.Range[row, col].Value == null || sheet.Range[row, col].Value.Length == 0) { NextFreeColumn = col + 3; break; }
                HeaderData hd = new HeaderData(col, sheet.Range[row, col].Value);
                hd.text = HeaderTranslationTable.Translate(hd.text);
                hd.Fill(qm, projectID, username, ESNDetail, QID_Name);
                hd.isCloneColumn = false;
                string acl = attributeclonelist.FirstOrDefault(x => (x.IndexOf(":") > -1 ? x.Substring(0, x.IndexOf(":")) : x.ToUpper()) == hd.text.ToUpper());
                if (acl != null)
                {
                    hd.isCloneColumn = true;
                    if (acl.IndexOf(":") > -1)
                    {
                        hd.isCloneTrasposeColumn = true;
                        hd.T_text = acl.Substring(acl.IndexOf(":") + 1);
                        hd.FillTranspose(qm, projectID, username, ESNDetail, QID_Name);
                        sheet.Range[row, col].CellStyle.Color = System.Drawing.Color.Red;
                    }
                    attributeclonelist.Remove(acl);
                }
                Header.Add(hd);
            }
            // load up any remaining attributes.
            foreach (string acl in attributeclonelist)
            {
                sheet.Range[row, NextFreeColumn].Value = acl;
                sheet.Range[row, NextFreeColumn].CellStyle.Color = System.Drawing.Color.Red;

                string[] S = acl.Split(':');
                string s1 = S[0];
                string s2 = "";
                if (S.Length > 1 && S[1].Length > 0)
                {
                    s2 = S[1];
                }
                HeaderData hd = new HeaderData(-1, acl);
                hd.text = s1;
                hd.Fill(qm, projectID, username, ESNDetail, QID_Name);
                hd.isCloneColumn = true;
                hd.col = NextFreeColumn;
                //hd
                NextFreeColumn++;
                if (s2.Length > 0)
                {
                    hd.isCloneTrasposeColumn = true;
                    hd.T_text = s2;
                    hd.FillTranspose(qm, projectID, username, ESNDetail, QID_Name);

                }
                Header.Add(hd);
            }

            return rvalue;
        }
        private string LoadData(API_UploadDeviceBatch_Out Batch)
        {
            this.log.LogIt("   LoadData Started:");
            string rvalue = "";
            int ErrorColumn = -1;
            string ErrorText = "";
            List<ReceiveHeader> rhList = new List<ReceiveHeader>();
            IFSLocationQueue LocationQueue = new IFSLocationQueue();

            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                ReceiveDetailManager rdm = new ReceiveDetailManager(username);
                int ESNCol = -1;
                ESNCol = Header.FirstOrDefault(x => x.text.ToUpper() == "ESN").col;
                ErrorColumn = Header.Count + 1;
                string celldata = "";
                short DirectiveIgnore = rdm.IFSDirective(ctx, "Ignore");

                string UploadESN = "";
                decimal uploadesnID = -1;
                string uploadStatus = "";
                string uploadLogMessage = "";
                #region Read in the data

                //int row = 0;
                foreach (API_Device_Out d in Batch.devices)
                {
                    //row++;
                    //this.log.LogIt("     Reading Rows:" + row.ToString());
                    //uploadLogMessage = "Row(" + row.ToString() + "):";

                    //if (sheet.Range[rowxx, 1].Value == null || sheet.Range[rowxx, 1].Value.Length == 0) { break; }

                    RowData rd = new RowData(projectID, clientlocationid, saveprocessID, receiveprocessID, projectname, saveprocessName, receiveprocessName, forceIMEI15, d.GetAttributeValue("Projecttag"), rdm);
                    d.SetAttributeSuccess("ProjectTag", "Header Attribute");
                    rd.UseSeedData = false;
                    if (attributeclonelist2.Count > 0) { rd.UseSeedData = true; } // We have seed data to look after.
                    ErrorText = "";
                    #region Read in this Rows Columns
                    foreach (HeaderData hd in Header)
                    {
                        if (hd.isCloneColumn == true)
                        {
                            //if (ESNCol > 0)
                            //{
                            //    // There are some problems with this. We now do the cloned list one at a time down at the bottom.
                            //    //rvalue = ctx.GetReceiveDetailItem_DataElement(sheet.Range[row, ESNCol].Value, hd.text);
                            //    //sheet.Range[row, hd.col].Value = rvalue;
                            //    //rd.AddCellData(rvalue, hd);
                            //}
                        }
                        else
                        {
                            if (hd.col > 0)
                            {
                                this.log.LogIt("       Adding Cell Data:" + hd.Name);
                                //Remove any CRLF char that may be in the string... we don't want any                                
                                if (hd.Name.Trim().Length == 0) { celldata = d.esn; }
                                else { celldata = d.GetAttributeValue(hd.Name); }
                                var newString = string.Join(" ", Regex.Split(celldata, @"(?:\r\n|\n|\r)")).Trim();
                                if (newString.Length > 199) { newString = newString.Substring(0, 199).Trim(); }
                                rd.AddCellData(newString, hd);
                                if (hd.Name.ToUpper() != "PROJECTTAG" && newString.Length > 0 && hd.Name.Length > 0 && rd.attributeList.Exists(x => x.QuestionName == hd.Name) == false)
                                {
                                    ErrorText += hd.Name + "/";
                                    d.SetAttributeError(hd.Name, "Unable to find attribute:" + hd.Name);
                                    //d.SetAttributeMessage(hd.Name, "Unable to find attribute:" + hd.Name, "Error");
                                    //sheet.Range[rowxx, hd.col].CellStyle.Font.Color = Syncfusion.XlsIO.ExcelKnownColors.Red;
                                }
                                else
                                {
                                    d.SetAttributeSuccess(hd.Name, "");
                                    //d.SetAttributeMessage(hd.Name, "Unable to find attribute:" + hd.Name, "Error");
                                }

                            }
                        }
                    }
                    #endregion
                }


                foreach (API_Device_Out d in Batch.devices)
                {
                    //this.log.LogIt("     Reading Rows:" + rowxx.ToString());
                    //uploadLogMessage = "Row(" + rowxx.ToString() + "):";
                    //if (sheet.Range[rowxx, 1].Value == null || sheet.Range[rowxx, 1].Value.Length == 0) { break; }
                    RowData rd = new RowData(projectID, clientlocationid, saveprocessID, receiveprocessID, projectname, saveprocessName, receiveprocessName, forceIMEI15, d.GetAttributeValue("Projecttag"), rdm);
                    d.SetAttributeSuccess("ProjectTag", "Header Attribute");
                    //RowData rd = new RowData(projectID, clientlocationid, saveprocessID, receiveprocessID, projectname, saveprocessName, receiveprocessName, forceIMEI15, rdm);
                    rd.UseSeedData = false;
                    if (attributeclonelist2.Count > 0) { rd.UseSeedData = true; } // We have seed data to look after.
                    ErrorText = "";
                    #region Read in this Rows Columns
                    foreach (HeaderData hd in Header)
                    {
                        if (hd.isCloneColumn == true)
                        {
                            //if (ESNCol > 0)
                            //{
                            //    // There are some problems with this. We now do the cloned list one at a time down at the bottom.
                            //    //rvalue = ctx.GetReceiveDetailItem_DataElement(sheet.Range[row, ESNCol].Value, hd.text);
                            //    //sheet.Range[row, hd.col].Value = rvalue;
                            //    //rd.AddCellData(rvalue, hd);
                            //}
                        }
                        else
                        {
                            if (hd.col > 0)
                            {
                                this.log.LogIt("       Adding Cell Data:" + hd.Name);
                                //Remove any CRLF char that may be in the string... we don't want any 
                                if (hd.Name.Trim().Length == 0) { celldata = d.esn; }
                                else { celldata = d.GetAttributeValue(hd.Name); }
                                //celldata = d.GetAttributeValue(hd.Name);
                                var newString = string.Join(" ", Regex.Split(celldata, @"(?:\r\n|\n|\r)")).Trim();
                                if (newString.Length > 199) { newString = newString.Substring(0, 199).Trim(); }
                                rd.AddCellData(newString, hd);
                                if (newString.Length > 0 && hd.Name.Length > 0 && rd.attributeList.Exists(x => x.QuestionName == hd.Name) == false)
                                {
                                    ErrorText += hd.Name + "/";
                                    d.SetAttributeMessage(hd.Name, "Unable to find attribute:" + hd.Name, "Error");
                                    //sheet.Range[rowxx, hd.col].CellStyle.Font.Color = Syncfusion.XlsIO.ExcelKnownColors.Red;
                                }

                            }
                        }
                    }
                    #endregion
                    /////////////////////////;
                    ReceiveDetail rdetail = null;
                    bool has000version = ctx.ReceiveDetails.Any(x => x.ReceiveDetailStatus.Status.ToUpper() != "GRAVEYARD" && x.ESN == rd.ESN && x.Version == "000");
                    if (has000version == true) { ErrorText = "IMEI found! " + ErrorText; }
                    UploadESN = rd.ESN;
                    #region If we don't have errors, Move forward.
                    if (ErrorText.Length == 0)
                    {
                        #region Some Edit Checks

                        // Change the client if the scancode is set
                        if (rd.LocationScanCode.Length > 0)
                        {
                            PairIDValue lsc = LocationScanCodes.FirstOrDefault(x => x.Desc.ToUpper() == rd.LocationScanCode.ToUpper());
                            if (lsc != null)
                            {
                                rd.LocationScanCodeLocationID = lsc.ID;
                                rd.clientlocationID = lsc.ID;
                            }
                        }
                        if (rd.IsCarrierManufacturerCombo(ctx, (attributeclonelist2.Count > 0)) == false) { ErrorText = "Invalid Carrier/Manufacturer/Model/Colour Combo. " + ErrorText; }
                        rd.POCondition = "NYT";
                        if (rd.SKU.Length == 0) { ErrorText = "Missing SKU. " + ErrorText; }
                        rd.IFSLocation = "LOC-001-001-001";

                        #endregion
                        #region Save the data to a ReceiveDetail Record.
                        uploadesnID = -1;

                        if (ErrorText.Length == 0)
                        {
                            rdetail = rd.Save(ctx, rdm, DirectiveIgnore, username);
                            if (rdetail == null) { ErrorText = "IMEI not found. " + ErrorText; }
                        }
                        else
                        {
                            ErrorText = "IMEI not saved! " + ErrorText;
                        }
                        #endregion
                        if (rdetail != null && rdetail.ESN.Trim().Length == 0) { ErrorText = "No IMEI number. " + ErrorText; }
                        #region Save the RD record to our Cache data to be saved.
                        if (rdetail != null && rdetail.ESN.Trim().Length > 0)
                        {
                            rdetail.StatusID = StatusID;
                            uploadesnID = rdetail.ReceiveDetailID;
                            ReceiveHeader rh = rhList.FirstOrDefault(x => x.ClientLocationID == rdetail.ClientLocationID);
                            ReceiveDetail dupRD = null;
                            if (rh != null)
                            {
                                // look to see if the ESN was already found inside the upload file.
                                dupRD = rh.ReceiveDetails.FirstOrDefault(x => x.ESN == rdetail.ESN);
                            }
                            if (dupRD == null)    // If nothing found, add it.
                            {
                                if (rh == null)
                                {
                                    this.log.LogIt("       Adding New ReceiveHeader:");
                                    rh = new ReceiveHeader();
                                    rh.ClientLocationID = rdetail.ClientLocationID;
                                    rh.CreateDate = rdetail.CreateDate;
                                    rh.CreateUser = rdetail.CreateUser;
                                    rh.LastUpdateDate = rdetail.LastUpdateDate;
                                    rh.LastUpdateUser = rdetail.LastUpdateUser;
                                    rh.MiscNote = rdetail.MiscNote;
                                    rh.ProjectID = rdetail.ProjectID;
                                    rh.ProjectName = rdetail.ProjectName;
                                    rh.QTYPaper = 0;
                                    rh.QTYRecorded = 0;
                                    rh.ReceiveDate = rdetail.ReceiveDate;
                                    rh.StatusID = rdetail.StatusID;
                                    rh.RMANumber = rdetail.RMANumber;
                                    rhList.Add(rh);
                                }
                                this.log.LogIt("       Adding Cell Data to header:" + rdetail.ESN);
                                rh.ReceiveDetails.Add(rdetail);
                                RowCount += 1;
                            }
                            else
                            {
                                ErrorText = "Dup IMEI number in file. " + ErrorText;
                            }
                        }
                        #endregion
                    }
                    #endregion
                    if (ErrorText.Length == 0)
                    {
                        rd.SetPriorSOData(ctx);
                        if (isThreaded == true) { ErrorText = "Uploaded - Threaded."; }
                        else { ErrorText = "Uploaded"; }
                        uploadStatus = "Success";
                    }
                    else { uploadStatus = "Error"; uploadLogMessage += "Error:"; }

                    d.SetMessageStatus(ErrorText, uploadStatus);
                    //sheet.Range[rowxx, ErrorColumn].Value = ErrorText;
                    uploadLogMessage += ErrorText;
                    LogManager.LogEntry(uploadStatus, "APIDetail", UploadESN, uploadesnID, "SourceData", uploadLogMessage, true);
                }
                #endregion Done Read
                LoadDataWrite(rhList, ctx, rdm);
            }
            return rvalue;
        }
        private string LoadData(IWorksheet sheet)
        {
            this.log.LogIt("   LoadData Started:");
            string rvalue = "";
            int ErrorColumn = -1;
            //int SOIFSNumberColumn = -1;
            //int SONumberColumn = -1;
            //int SOLineColumn = -1;
            string ErrorText = "";
            //List<ReceiveDetail> rds = new List<ReceiveDetail>();
            List<ReceiveHeader> rhList = new List<ReceiveHeader>();
            IFSLocationQueue LocationQueue = new IFSLocationQueue();

            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                //            ReceiveHeader rh = new ReceiveHeader();
                // start on the second row
                ReceiveDetailManager rdm = new ReceiveDetailManager(username);
                int ESNCol = -1;
                ESNCol = Header.FirstOrDefault(x => x.text.ToUpper() == "ESN").col;
                ErrorColumn = Header.Count + 1;
                //SOIFSNumberColumn = ErrorColumn + 1;
                //SONumberColumn = SOIFSNumberColumn + 1;
                //SOLineColumn = SONumberColumn + 1;
                sheet.Range[1, ErrorColumn].Value = "Upload Status";
                //sheet.Range[1, SOIFSNumberColumn].Value = "Last SO IFS Number";
                //sheet.Range[1, SONumberColumn].Value = "SO Number";
                //sheet.Range[1, SOLineColumn].Value = "SO Line";



                string celldata = "";
                short DirectiveIgnore = rdm.IFSDirective(ctx, "Ignore");

                string UploadESN = "";
                decimal uploadesnID = -1;
                string uploadStatus = "";
                string uploadLogMessage = "";
                //ReceiveDetail rdetail = null;
                #region Read in the data
                for (int row = 2; row < 100000; row++)
                {
                    this.log.LogIt("     Reading Rows:" + row.ToString());
                    uploadLogMessage = "Row(" + row.ToString() + "):";

                    if (sheet.Range[row, 1].Value == null || sheet.Range[row, 1].Value.Length == 0) { break; }
                    RowData rd = new RowData(projectID, clientlocationid, saveprocessID, receiveprocessID, projectname, saveprocessName, receiveprocessName, forceIMEI15, rdm);
                    rd.UseSeedData = false;
                    if (attributeclonelist2.Count > 0) { rd.UseSeedData = true; } // We have seed data to look after.
                    //for (int col = 1; col < Header.Count + 1; col++)
                    ErrorText = "";
                    #region Read in this Rows Columns
                    foreach (HeaderData hd in Header)
                    {
                        if (hd.isCloneColumn == true)
                        {
                            //if (ESNCol > 0)
                            //{
                            //    // There are some problems with this. We now do the cloned list one at a time down at the bottom.
                            //    //rvalue = ctx.GetReceiveDetailItem_DataElement(sheet.Range[row, ESNCol].Value, hd.text);
                            //    //sheet.Range[row, hd.col].Value = rvalue;
                            //    //rd.AddCellData(rvalue, hd);
                            //}
                        }
                        else
                        {
                            if (hd.col > 0)
                            {
                                this.log.LogIt("       Adding Cell Data:" + hd.Name);

                                //Remove any CRLF char that may be in the string... we don't want any                                
                                celldata = sheet.Range[row, hd.col].Value;
                                var newString = string.Join(" ", Regex.Split(celldata, @"(?:\r\n|\n|\r)")).Trim();
                                if (newString.Length > 199) { newString = newString.Substring(0, 199).Trim(); }
                                rd.AddCellData(newString, hd);
                                if (newString.Length > 0 && hd.Name.Length > 0 && rd.attributeList.Exists(x => x.QuestionName == hd.Name) == false)
                                {
                                    ErrorText += hd.Name + "/";
                                    sheet.Range[row, hd.col].CellStyle.Font.Color = Syncfusion.XlsIO.ExcelKnownColors.Red;
                                }

                            }
                        }
                    }
                    #endregion
                    /////////////////////////;
                    ReceiveDetail rdetail = null;
                    bool has000version = ctx.ReceiveDetails.Any(x => x.ReceiveDetailStatus.Status.ToUpper() != "GRAVEYARD" && x.ESN == rd.ESN && x.Version == "000");
                    if (has000version == true) { ErrorText = "IMEI found! " + ErrorText; }
                    UploadESN = rd.ESN;
                    #region If we don't have errors, Move forward.
                    if (ErrorText.Length == 0)
                    {
                        #region Some Edit Checks

                        // Change the client if the scancode is set
                        if (rd.LocationScanCode.Length > 0)
                        {
                            PairIDValue lsc = LocationScanCodes.FirstOrDefault(x => x.Desc.ToUpper() == rd.LocationScanCode.ToUpper());
                            if (lsc != null)
                            {
                                rd.LocationScanCodeLocationID = lsc.ID;
                                rd.clientlocationID = lsc.ID;
                            }
                        }
                        if (rd.IsCarrierManufacturerCombo(ctx, (attributeclonelist2.Count > 0)) == false) { ErrorText = "Invalid Carrier/Manufacturer/Model/Colour Combo. " + ErrorText; }
                        //if (rd.PONumber.Length == 0 && IFSSite.ToUpper() == "C1NA" && IFSTransactionType.ToUpper() == "PO_RECEIPT") { ErrorText = "Missing PONumber. " + ErrorText; }
                        //if (rd.POLine.Length == 0 && IFSSite.ToUpper() == "C1NA" && IFSTransactionType.ToUpper() == "PO_RECEIPT") { ErrorText = "Missing POLine. " + ErrorText; }
                        rd.POCondition = "NYT";
                        //if (rd.POCondition.Length == 0) { ErrorText = "Missing Condition. " + ErrorText; }
                        //if (rd.POCondition.Length > 0 && rd.POCondition.ToUpper() == "GEN" && IFSSite == "C1NA") { ErrorText = "Invalid Condition for C1NA device. " + ErrorText; }
                        //if (rd.POCondition.Length > 0 && rd.POCondition.ToUpper() != "GEN" && IFSSite == "C1CON") { ErrorText = "Invalid Condition for C1CON device. " + ErrorText; }
                        if (rd.SKU.Length == 0) { ErrorText = "Missing SKU. " + ErrorText; }
                        rd.IFSLocation = "LOC-001-001-001";
                        //if (rd.IFSLocation == null || rd.IFSLocation.Length == 0) { ErrorText = "Missing Site Location. " + ErrorText; }
                        //else
                        //{
                        //    IFSLocation location = LocationQueue.GetLocation(rd.IFSLocation);
                        //    if (location.isValid == false) { ErrorText = "Invalid Site Location. " + ErrorText; }
                        //    else { if (location.IsThisFrozen(username) == true) { ErrorText = "Site Location is frozen. " + ErrorText; } }
                        //}

                        #endregion
                        #region Get PO Data
                        //if (ErrorText.Length == 0 && IFSSite.ToUpper() == "C1NA" && IFSTransactionType.ToUpper() == "PO_RECEIPT")      // PO Required only if the client is a C1NA client location. Otherwise. Skip PO side.
                        //{
                        //    string poline = "";
                        //    string poCost = "";
                        //    string POCondition = "";
                        //    string Message = "";
                        //    decimal IFSPurchaseOrderDetailID = -1;
                        //    if (rdm.IsPODataValidForUpload(rd.IFSVendor, rd.PONumber, rd.SKU, rd.POLine, rd.POCondition, ref poline, ref poCost, ref POCondition, ref IFSPurchaseOrderDetailID, ref Message) == false)
                        //    {
                        //        if (Message.Length == 0)
                        //        {
                        //            Message = "You can only Use this upload for Redial PO";
                        //        }
                        //        ErrorText = Message + ErrorText;
                        //    }
                        //    else
                        //    {
                        //        #region Save PO Data
                        //        HeaderData hd = null;
                        //        hd = Header.FirstOrDefault(x => x.Name == "IFS PO Unit Cost");
                        //        if (hd != null)
                        //        {
                        //            rd.AddCellData(poCost, hd);
                        //            if (poline.Length > 0 && hd.Name.Length > 0 && rd.attributeList.Exists(x => x.QuestionName == hd.Name) == false)
                        //            {
                        //                ErrorText += hd.Name + "/";
                        //                sheet.Range[row, hd.col].CellStyle.Font.Color = Syncfusion.XlsIO.ExcelKnownColors.Red;
                        //            }
                        //        }
                        //        // This is coming in via the normal attribute stream, it is manditory
                        //        //hd = Header.FirstOrDefault(x => x.Name == "IFS PO Line Number");
                        //        //if (hd != null)
                        //        //{
                        //        //    rd.AddCellData(poline, hd);
                        //        //    if (poline.Length > 0 && hd.Name.Length > 0 && rd.attributeList.Exists(x => x.QuestionName == hd.Name) == false)
                        //        //    {
                        //        //        ErrorText += hd.Name + "/";
                        //        //        sheet.Range[row, hd.col].CellStyle.Font.Color = Syncfusion.XlsIO.ExcelKnownColors.Red;
                        //        //    }
                        //        //}
                        //        // This is coming in via the normal attribute stream, it is manditory
                        //        //hd = Header.FirstOrDefault(x => x.Name == "IFS Conditions");
                        //        //if (hd != null)
                        //        //{
                        //        //    rd.AddCellData(POCondition, hd);
                        //        //    if (poline.Length > 0 && hd.Name.Length > 0 && rd.attributeList.Exists(x => x.QuestionName == hd.Name) == false)
                        //        //    {
                        //        //        ErrorText += hd.Name + "/";
                        //        //        sheet.Range[row, hd.col].CellStyle.Font.Color = Syncfusion.XlsIO.ExcelKnownColors.Red;
                        //        //    }
                        //        //}
                        //        #endregion
                        //    }
                        //}
                        #endregion
                        #region Save the data to a ReceiveDetail Record.
                        uploadesnID = -1;

                        if (ErrorText.Length == 0)
                        {
                            rdetail = rd.Save(ctx, rdm, DirectiveIgnore, username);
                            if (rdetail == null) { ErrorText = "IMEI not found. " + ErrorText; }
                        }
                        else
                        {
                            ErrorText = "IMEI not saved! " + ErrorText;
                        }
                        #endregion
                        if (rdetail != null && rdetail.ESN.Trim().Length == 0) { ErrorText = "No IMEI number. " + ErrorText; }
                        #region Save the RD record to our Cache data to be saved.
                        if (rdetail != null && rdetail.ESN.Trim().Length > 0)
                        {
                            rdetail.StatusID = StatusID;
                            uploadesnID = rdetail.ReceiveDetailID;
                            ReceiveHeader rh = rhList.FirstOrDefault(x => x.ClientLocationID == rdetail.ClientLocationID);
                            ReceiveDetail dupRD = null;
                            if (rh != null)
                            {
                                // look to see if the ESN was already found inside the upload file.
                                dupRD = rh.ReceiveDetails.FirstOrDefault(x => x.ESN == rdetail.ESN);
                            }
                            if (dupRD == null)    // If nothing found, add it.
                            {
                                if (rh == null)
                                {
                                    this.log.LogIt("       Adding New ReceiveHeader:");
                                    rh = new ReceiveHeader();
                                    rh.ClientLocationID = rdetail.ClientLocationID;
                                    rh.CreateDate = rdetail.CreateDate;
                                    rh.CreateUser = rdetail.CreateUser;
                                    rh.LastUpdateDate = rdetail.LastUpdateDate;
                                    rh.LastUpdateUser = rdetail.LastUpdateUser;
                                    rh.MiscNote = rdetail.MiscNote;
                                    rh.ProjectID = rdetail.ProjectID;
                                    rh.ProjectName = rdetail.ProjectName;
                                    rh.QTYPaper = 0;
                                    rh.QTYRecorded = 0;
                                    rh.ReceiveDate = rdetail.ReceiveDate;
                                    rh.StatusID = rdetail.StatusID;
                                    rh.RMANumber = rdetail.RMANumber;
                                    rhList.Add(rh);
                                }
                                this.log.LogIt("       Adding Cell Data to header:" + rdetail.ESN);
                                rh.ReceiveDetails.Add(rdetail);
                                RowCount += 1;
                            }
                            else
                            {
                                ErrorText = "Dup IMEI number in file. " + ErrorText;
                            }
                        }
                        #endregion
                    }
                    #endregion
                    if (ErrorText.Length == 0)
                    {
                        rd.SetPriorSOData(ctx);
                        if (isThreaded == true) { ErrorText = "Uploaded - Threaded."; }
                        else { ErrorText = "Uploaded"; }
                        uploadStatus = "Success";
                    }
                    else { uploadStatus = "Error"; uploadLogMessage += "Error:"; }

                    sheet.Range[row, ErrorColumn].Value = ErrorText;
                    uploadLogMessage += ErrorText;
                    LogManager.LogEntry(uploadStatus, "Detail", UploadESN, uploadesnID, "SourceData", uploadLogMessage, true);
                }
                #endregion Done Read
                LoadDataWrite(rhList, ctx, rdm);
            }
            return rvalue;
        }



        private void LoadDataWrite(List<ReceiveHeader> rhList, clsLinqDataContext ctx, ReceiveDetailManager rdm)
        {
            #region Write out data
            short IgnoreDirective = rdm.IFSDirective(ctx, "Ignore");
            short PO_ReceiptDirective = rdm.IFSDirective(ctx, IFSTransactionType);
            if (isThreaded) { ThreadPool.QueueUserWorkItem(Report => SaveDataToDatabase(rhList.ToList(), IgnoreDirective, PO_ReceiptDirective)); }    // This should go off and do it's thing giving control back to the browser. (Apear as if faster save.)
            else { SaveDataToDatabase(rhList.ToList(), IgnoreDirective, PO_ReceiptDirective); }
            #endregion Write out data
        }
        private string LoadDataBKUP(IWorksheet sheet)
        {
            this.log.LogIt("   LoadData Started:");
            string rvalue = "";
            int ErrorColumn = -1;
            //int SOIFSNumberColumn = -1;
            //int SONumberColumn = -1;
            //int SOLineColumn = -1;
            string ErrorText = "";
            //List<ReceiveDetail> rds = new List<ReceiveDetail>();
            List<ReceiveHeader> rhList = new List<ReceiveHeader>();
            IFSLocationQueue LocationQueue = new IFSLocationQueue();

            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                //            ReceiveHeader rh = new ReceiveHeader();
                // start on the second row
                ReceiveDetailManager rdm = new ReceiveDetailManager(username);
                int ESNCol = -1;
                ESNCol = Header.FirstOrDefault(x => x.text.ToUpper() == "ESN").col;
                ErrorColumn = Header.Count + 1;
                //SOIFSNumberColumn = ErrorColumn + 1;
                //SONumberColumn = SOIFSNumberColumn + 1;
                //SOLineColumn = SONumberColumn + 1;
                sheet.Range[1, ErrorColumn].Value = "Upload Status";
                //sheet.Range[1, SOIFSNumberColumn].Value = "Last SO IFS Number";
                //sheet.Range[1, SONumberColumn].Value = "SO Number";
                //sheet.Range[1, SOLineColumn].Value = "SO Line";



                string celldata = "";
                short DirectiveIgnore = rdm.IFSDirective(ctx, "Ignore");

                string UploadESN = "";
                decimal uploadesnID = -1;
                string uploadStatus = "";
                string uploadLogMessage = "";
                //ReceiveDetail rdetail = null;
                #region Read in the data
                for (int row = 2; row < 100000; row++)
                {
                    this.log.LogIt("     Reading Rows:" + row.ToString());
                    uploadLogMessage = "Row(" + row.ToString() + "):";

                    if (sheet.Range[row, 1].Value == null || sheet.Range[row, 1].Value.Length == 0) { break; }
                    RowData rd = new RowData(projectID, clientlocationid, saveprocessID, receiveprocessID, projectname, saveprocessName, receiveprocessName, forceIMEI15, rdm);
                    rd.UseSeedData = false;
                    if (attributeclonelist2.Count > 0) { rd.UseSeedData = true; } // We have seed data to look after.
                    //for (int col = 1; col < Header.Count + 1; col++)
                    ErrorText = "";
                    #region Read in this Rows Columns
                    foreach (HeaderData hd in Header)
                    {
                        if (hd.isCloneColumn == true)
                        {
                            //if (ESNCol > 0)
                            //{
                            //    // There are some problems with this. We now do the cloned list one at a time down at the bottom.
                            //    //rvalue = ctx.GetReceiveDetailItem_DataElement(sheet.Range[row, ESNCol].Value, hd.text);
                            //    //sheet.Range[row, hd.col].Value = rvalue;
                            //    //rd.AddCellData(rvalue, hd);
                            //}
                        }
                        else
                        {
                            if (hd.col > 0)
                            {
                                this.log.LogIt("       Adding Cell Data:" + hd.Name);

                                //Remove any CRLF char that may be in the string... we don't want any                                
                                celldata = sheet.Range[row, hd.col].Value;
                                var newString = string.Join(" ", Regex.Split(celldata, @"(?:\r\n|\n|\r)")).Trim();
                                if (newString.Length > 199) { newString = newString.Substring(0, 199).Trim(); }
                                rd.AddCellData(newString, hd);
                                if (newString.Length > 0 && hd.Name.Length > 0 && rd.attributeList.Exists(x => x.QuestionName == hd.Name) == false)
                                {
                                    ErrorText += hd.Name + "/";
                                    sheet.Range[row, hd.col].CellStyle.Font.Color = Syncfusion.XlsIO.ExcelKnownColors.Red;
                                }

                            }
                        }
                    }
                    #endregion
                    /////////////////////////;
                    ReceiveDetail rdetail = null;
                    bool has000version = ctx.ReceiveDetails.Any(x => x.ReceiveDetailStatus.Status.ToUpper() != "GRAVEYARD" && x.ESN == rd.ESN && x.Version == "000");
                    if (has000version == true) { ErrorText = "IMEI found! " + ErrorText; }
                    UploadESN = rd.ESN;
                    #region If we don't have errors, Move forward.
                    if (ErrorText.Length == 0)
                    {
                        #region Some Edit Checks

                        // Change the client if the scancode is set
                        if (rd.LocationScanCode.Length > 0)
                        {
                            PairIDValue lsc = LocationScanCodes.FirstOrDefault(x => x.Desc.ToUpper() == rd.LocationScanCode.ToUpper());
                            if (lsc != null)
                            {
                                rd.LocationScanCodeLocationID = lsc.ID;
                                rd.clientlocationID = lsc.ID;
                            }
                        }
                        if (rd.IsCarrierManufacturerCombo(ctx, (attributeclonelist2.Count > 0)) == false) { ErrorText = "Invalid Carrier/Manufacturer/Model/Colour Combo. " + ErrorText; }
                        //if (rd.PONumber.Length == 0 && IFSSite.ToUpper() == "C1NA" && IFSTransactionType.ToUpper() == "PO_RECEIPT") { ErrorText = "Missing PONumber. " + ErrorText; }
                        //if (rd.POLine.Length == 0 && IFSSite.ToUpper() == "C1NA" && IFSTransactionType.ToUpper() == "PO_RECEIPT") { ErrorText = "Missing POLine. " + ErrorText; }
                        rd.POCondition = "NYT";
                        //if (rd.POCondition.Length == 0) { ErrorText = "Missing Condition. " + ErrorText; }
                        //if (rd.POCondition.Length > 0 && rd.POCondition.ToUpper() == "GEN" && IFSSite == "C1NA") { ErrorText = "Invalid Condition for C1NA device. " + ErrorText; }
                        //if (rd.POCondition.Length > 0 && rd.POCondition.ToUpper() != "GEN" && IFSSite == "C1CON") { ErrorText = "Invalid Condition for C1CON device. " + ErrorText; }
                        if (rd.SKU.Length == 0) { ErrorText = "Missing SKU. " + ErrorText; }
                        rd.IFSLocation = "LOC-001-001-001";
                        //if (rd.IFSLocation == null || rd.IFSLocation.Length == 0) { ErrorText = "Missing Site Location. " + ErrorText; }
                        //else
                        //{
                        //    IFSLocation location = LocationQueue.GetLocation(rd.IFSLocation);
                        //    if (location.isValid == false) { ErrorText = "Invalid Site Location. " + ErrorText; }
                        //    else { if (location.IsThisFrozen(username) == true) { ErrorText = "Site Location is frozen. " + ErrorText; } }
                        //}

                        #endregion
                        #region Get PO Data
                        //if (ErrorText.Length == 0 && IFSSite.ToUpper() == "C1NA" && IFSTransactionType.ToUpper() == "PO_RECEIPT")      // PO Required only if the client is a C1NA client location. Otherwise. Skip PO side.
                        //{
                        //    string poline = "";
                        //    string poCost = "";
                        //    string POCondition = "";
                        //    string Message = "";
                        //    decimal IFSPurchaseOrderDetailID = -1;
                        //    if (rdm.IsPODataValidForUpload(rd.IFSVendor, rd.PONumber, rd.SKU, rd.POLine, rd.POCondition, ref poline, ref poCost, ref POCondition, ref IFSPurchaseOrderDetailID, ref Message) == false)
                        //    {
                        //        if (Message.Length == 0)
                        //        {
                        //            Message = "You can only Use this upload for Redial PO";
                        //        }
                        //        ErrorText = Message + ErrorText;
                        //    }
                        //    else
                        //    {
                        //        #region Save PO Data
                        //        HeaderData hd = null;
                        //        hd = Header.FirstOrDefault(x => x.Name == "IFS PO Unit Cost");
                        //        if (hd != null)
                        //        {
                        //            rd.AddCellData(poCost, hd);
                        //            if (poline.Length > 0 && hd.Name.Length > 0 && rd.attributeList.Exists(x => x.QuestionName == hd.Name) == false)
                        //            {
                        //                ErrorText += hd.Name + "/";
                        //                sheet.Range[row, hd.col].CellStyle.Font.Color = Syncfusion.XlsIO.ExcelKnownColors.Red;
                        //            }
                        //        }
                        //        // This is coming in via the normal attribute stream, it is manditory
                        //        //hd = Header.FirstOrDefault(x => x.Name == "IFS PO Line Number");
                        //        //if (hd != null)
                        //        //{
                        //        //    rd.AddCellData(poline, hd);
                        //        //    if (poline.Length > 0 && hd.Name.Length > 0 && rd.attributeList.Exists(x => x.QuestionName == hd.Name) == false)
                        //        //    {
                        //        //        ErrorText += hd.Name + "/";
                        //        //        sheet.Range[row, hd.col].CellStyle.Font.Color = Syncfusion.XlsIO.ExcelKnownColors.Red;
                        //        //    }
                        //        //}
                        //        // This is coming in via the normal attribute stream, it is manditory
                        //        //hd = Header.FirstOrDefault(x => x.Name == "IFS Conditions");
                        //        //if (hd != null)
                        //        //{
                        //        //    rd.AddCellData(POCondition, hd);
                        //        //    if (poline.Length > 0 && hd.Name.Length > 0 && rd.attributeList.Exists(x => x.QuestionName == hd.Name) == false)
                        //        //    {
                        //        //        ErrorText += hd.Name + "/";
                        //        //        sheet.Range[row, hd.col].CellStyle.Font.Color = Syncfusion.XlsIO.ExcelKnownColors.Red;
                        //        //    }
                        //        //}
                        //        #endregion
                        //    }
                        //}
                        #endregion
                        #region Save the data to a ReceiveDetail Record.
                        uploadesnID = -1;

                        if (ErrorText.Length == 0)
                        {
                            rdetail = rd.Save(ctx, rdm, DirectiveIgnore, username);
                            if (rdetail == null) { ErrorText = "IMEI not found. " + ErrorText; }
                        }
                        else
                        {
                            ErrorText = "IMEI not saved! " + ErrorText;
                        }
                        #endregion
                        if (rdetail != null && rdetail.ESN.Trim().Length == 0) { ErrorText = "No IMEI number. " + ErrorText; }
                        #region Save the RD record to our Cache data to be saved.

                        if (rdetail != null && rdetail.ESN.Trim().Length > 0)
                        {
                            rdetail.StatusID = StatusID;
                            uploadesnID = rdetail.ReceiveDetailID;
                            ReceiveHeader rh = rhList.FirstOrDefault(x => x.ClientLocationID == rdetail.ClientLocationID);
                            ReceiveDetail dupRD = null;
                            if (rh != null)
                            {
                                // look to see if the ESN was already found inside the upload file.
                                dupRD = rh.ReceiveDetails.FirstOrDefault(x => x.ESN == rdetail.ESN);
                            }
                            if (dupRD == null)    // If nothing found, add it.
                            {
                                if (rh == null)
                                {
                                    this.log.LogIt("       Adding New ReceiveHeader:");
                                    rh = new ReceiveHeader();
                                    rh.ClientLocationID = rdetail.ClientLocationID;
                                    rh.CreateDate = rdetail.CreateDate;
                                    rh.CreateUser = rdetail.CreateUser;
                                    rh.LastUpdateDate = rdetail.LastUpdateDate;
                                    rh.LastUpdateUser = rdetail.LastUpdateUser;
                                    rh.MiscNote = rdetail.MiscNote;
                                    rh.ProjectID = rdetail.ProjectID;
                                    rh.ProjectName = rdetail.ProjectName;
                                    rh.QTYPaper = 0;
                                    rh.QTYRecorded = 0;
                                    rh.ReceiveDate = rdetail.ReceiveDate;
                                    rh.StatusID = rdetail.StatusID;
                                    rh.RMANumber = rdetail.RMANumber;
                                    rhList.Add(rh);
                                }
                                this.log.LogIt("       Adding Cell Data to header:" + rdetail.ESN);
                                rh.ReceiveDetails.Add(rdetail);
                                RowCount += 1;
                            }
                            else
                            {
                                ErrorText = "Dup IMEI number in file. " + ErrorText;
                            }
                        }
                        #endregion
                    }
                    #endregion
                    if (ErrorText.Length == 0)
                    {
                        rd.SetPriorSOData(ctx);
                        if (isThreaded == true) { ErrorText = "Uploaded - Threaded."; }
                        else { ErrorText = "Uploaded"; }
                        uploadStatus = "Success";
                    }
                    else { uploadStatus = "Error"; uploadLogMessage += "Error:"; }
                    sheet.Range[row, ErrorColumn].Value = ErrorText;
                    uploadLogMessage += ErrorText;
                    LogManager.LogEntry(uploadStatus, "Detail", UploadESN, uploadesnID, "SourceData", uploadLogMessage, true);
                    //sheet.Range[row, SOIFSNumberColumn].Value = rd.SOIFSNumber;
                    //sheet.Range[row, SONumberColumn].Value = rd.SONumber;
                    //sheet.Range[row, SOLineColumn].Value = rd.SOLine;
                }
                #endregion Done Read
                #region Write out data
                short IgnoreDirective = rdm.IFSDirective(ctx, "Ignore");
                short PO_ReceiptDirective = rdm.IFSDirective(ctx, IFSTransactionType);
                if (isThreaded) { ThreadPool.QueueUserWorkItem(Report => SaveDataToDatabase(rhList.ToList(), IgnoreDirective, PO_ReceiptDirective)); }    // This should go off and do it's thing giving control back to the browser. (Apear as if faster save.)
                else { SaveDataToDatabase(rhList.ToList(), IgnoreDirective, PO_ReceiptDirective); }
                #endregion Write out data
            }
            return rvalue;
        }

        private void SaveDataToDatabase(List<ReceiveHeader> rhList, short IgnoreDirective, short PO_ReceiptDirective)
        {
            #region Write out data
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                foreach (ReceiveHeader rh in rhList)
                {
                    ctx.ReceiveHeaders.InsertOnSubmit(rh);
                }

                //this.log.LogIt("       Starting to save CHanges");
                ctx.SubmitChanges();
                //this.log.LogIt("       Save Changes Done");
                #region Syncronize header to detail
                ReceiveDetailUtilityMoveLogManager logu = new ReceiveDetailUtilityMoveLogManager("Upload IMEI", username);

                foreach (ReceiveHeader rh in rhList)
                {
                    foreach (ReceiveDetail rdx in rh.ReceiveDetails.OrderByDescending(x => x.Version))
                    {
                        ctx.Utility_RebuildReceiveDetailHeaderAttributes_ThisIMEI_B(rdx.ESN, IgnoreDirective);
                        ctx.IFS_GenerateInvtTran_B(rdx.ReceiveDetailID, PO_ReceiptDirective, rdx.SKU, rdx.IFSLocation, rdx.IFSCondition, rdx.SKU, rdx.IFSLocation, rdx.IFSCondition, username, -1, "");
                        logu.Save(rdx.ReceiveDetailID, "Uploaded");
                        if (isXLS == false)
                        {
                            api_batchdata.UpdateReceiveDetailID(rdx.ESN, rdx.ReceiveDetailID);
                        }
                    }
                }
            }
            #endregion
            #endregion Write out data
        }


        private string GetUserIPAddress()
        {
            if (System.Web.HttpContext.Current == null) { return "WEBService"; }
            string ip = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(ip))
            {
                ip = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            return ip;
        }


        private void LoadLocationScanCodes()
        {
            this.log.LogIt("   LoadLocationScanCodes Started:");
            ClientLocationManager clm = new ClientLocationManager(username);
            LocationScanCodes = clm.GetClientLocationScanCodes();
        }
        public void LoadHeaderTranslationTable()
        {
            this.log.LogIt("   LoadHeaderTranslationTable Started:");
            HeaderTranslationTable.Add("SOURCE", "TARGET");
        }



        public string LoadIMEISeedData()
        {
            this.log.LogIt("   LoadIMEIData Started:");
            string rvalue = "";
            if (isXLS == true) { rvalue = LoadIMEISeedDatafromXLS(); }
            return rvalue;
        }
        public string LoadIMEISeedDatafromXLS()
        {
            this.log.LogIt("   LoadIMEIDatafromXLS Started:");
            LoadHeaderTranslationTable();
            RowCount = 0;
            string rvalue = "";
            DateTime starttime = DateTime.Now;
            ExcelEngine excelEngine = new ExcelEngine();
            //Step 2 : Instantiate the excel application object.
            IApplication application = excelEngine.Excel;
            IWorkbook workbook = application.Workbooks.Open(filename, ExcelOpenType.Automatic);
            //The first worksheet object in the worksheets collection is accessed.
            IWorksheet sheet = workbook.Worksheets[0];
            LoadLocationScanCodes();
            rvalue = LoadHeaderData(sheet);

            rvalue = LoadSeedData(sheet);
            workbook.Save();
            workbook.Close();
            excelEngine.Dispose();
            DateTime endtime = DateTime.Now;
            TimeSpan diffResult = endtime.Subtract(starttime);
            rvalue = "(HH:MM:SS)" + diffResult.Hours.ToString() + ":" + diffResult.Minutes.ToString() + ":" + diffResult.Seconds.ToString() + "  -- Row Count:" + RowCount.ToString();
            return rvalue;
        }
        private string LoadSeedData(IWorksheet sheet)
        {
            this.log.LogIt("   LoadSeedData Started:");
            string rvalue = "";
            List<ReceiveHeader> rhList = new List<ReceiveHeader>();
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                //            ReceiveHeader rh = new ReceiveHeader();
                // start on the second row
                ReceiveDetailManager rdm = new ReceiveDetailManager(username);
                int ESNCol = -1;
                ESNCol = Header.FirstOrDefault(x => x.text.ToUpper() == "ESN").col;


                string celldata = "";

                for (int row = 2; row < 100000; row++)
                {
                    this.log.LogIt("     Reading Rows:" + row.ToString());
                    if (sheet.Range[row, 1].Value == null || sheet.Range[row, 1].Value.Length == 0) { break; }
                    RowData rd = new RowData(projectID, clientlocationid, saveprocessID, receiveprocessID, projectname, saveprocessName, receiveprocessName, forceIMEI15, rdm);
                    //for (int col = 1; col < Header.Count + 1; col++)

                    foreach (HeaderData hd in Header)
                    {
                        if (hd.isCloneColumn == true)
                        {
                            if (ESNCol > 0)
                            {
                                // There are some problems with this. We now do the cloned list one at a time down at the bottom.
                                //rvalue = ctx.GetReceiveDetailItem_DataElement(sheet.Range[row, ESNCol].Value, hd.text);
                                //sheet.Range[row, hd.col].Value = rvalue;
                                //rd.AddCellData(rvalue, hd);
                            }
                        }
                        else
                        {
                            if (hd.col > 0)
                            {
                                this.log.LogIt("       Adding Cell Data:" + hd.Name);

                                //Remove any CRLF char that may be in the string... we don't want any                                
                                celldata = sheet.Range[row, hd.col].Value;
                                var newString = string.Join(" ", Regex.Split(celldata, @"(?:\r\n|\n|\r)")).Trim();
                                if (newString.Length > 199) { newString = newString.Substring(0, 199).Trim(); }
                                rd.AddCellData(newString, hd);
                            }
                        }
                    }
                    /////////////////////////;
                    ReceiveDetail rdetail = null;
                    // Change the client if the scancode is set
                    //if (rd.LocationScanCode.Length > 0)
                    //{
                    //    PairIDValue lsc = LocationScanCodes.FirstOrDefault(x => x.Desc.ToUpper() == rd.LocationScanCode.ToUpper());
                    //    if (lsc != null)
                    //    {
                    //        rd.LocationScanCodeLocationID = lsc.ID;
                    //        rd.clientlocationID = lsc.ID;
                    //    }
                    //}
                    rdetail = rd.SaveForSeedData(ctx, username);
                    if (rdetail != null && rdetail.ESN.Trim().Length > 0)
                    {
                        rdetail.StatusID = StatusID;
                        ReceiveHeader rh = rhList.FirstOrDefault(x => x.ClientLocationID == rdetail.ClientLocationID);
                        if (rh == null)
                        {
                            this.log.LogIt("       Adding New ReceiveHeader:");
                            rh = new ReceiveHeader();
                            rh.ClientLocationID = rdetail.ClientLocationID;
                            rh.CreateDate = rdetail.CreateDate;
                            rh.CreateUser = rdetail.CreateUser;
                            rh.LastUpdateDate = rdetail.LastUpdateDate;
                            rh.LastUpdateUser = rdetail.LastUpdateUser;
                            rh.MiscNote = rdetail.MiscNote;
                            rh.ProjectID = rdetail.ProjectID;
                            rh.ProjectName = rdetail.ProjectName;
                            rh.QTYPaper = 0;
                            rh.QTYRecorded = 0;
                            rh.ReceiveDate = rdetail.ReceiveDate;
                            rh.StatusID = rdetail.StatusID;
                            rh.RMANumber = rdetail.RMANumber;
                            rhList.Add(rh);
                        }
                        this.log.LogIt("       Adding Cell Data to header:" + rdetail.ESN);
                        rh.ReceiveDetails.Add(rdetail);
                        RowCount += 1;

                    }
                }

                //foreach (ReceiveHeader rh in rhList)
                //{
                //    string EList = "";
                //    foreach (ReceiveDetail rdx in rh.ReceiveDetails.OrderByDescending(x => x.Version))
                //    {
                //        if (EList.Length > 0) { EList += ","; }
                //        EList += rdx.ESN;
                //        // There is a limit of 8000 characters as the parameter for advanceESNVersion_List -- So it pulls off 7000 lengths.
                //        if (EList.Length > 7000)
                //        {
                //            ctx.AdvanceESNVersion_List(EList, 1, username); EList = "";
                //            this.log.LogIt("       Advancing prior ESN Numbers:");
                //        }
                //    }
                //    if (EList.Length > 0) { ctx.AdvanceESNVersion_List(EList, 1, username); }
                //    ///////////////////////////////////////////////////////
                //    ctx.ReceiveHeaders.InsertOnSubmit(rh);
                //}
                //this.log.LogIt("       Starting to save CHanges");
                //ctx.SubmitChanges();
                //this.log.LogIt("       Save Changes Done");

                if (attributeclonelist2.Count > 0)  // We have seed data to look after.
                {
                    ReceiveDetailManager RDM = new ReceiveDetailManager(username);
                    foreach (ReceiveHeader rh in rhList)
                    {
                        foreach (ReceiveDetail rdx in rh.ReceiveDetails.OrderByDescending(x => x.Version))
                        {
                            RDM.CloneAttribute(ctx, rdx.ESN, attributeclonelist2, false);
                        }
                    }
                }

            }
            return rvalue;
        }




    }
    public class RowData
    {
        string RECEIVEDETAILID { get; set; }
        string CLIENTLOCATIONID { get; set; }
        string CLIENTLOCATION { get; set; }
        string _ESN = "";

        public string IFSSite { get; set; }
        public string IFSProject { get; set; }
        public string IFSVendor { get; set; }


        string STARTPROCESSNAME { get; set; }
        public string CURRENTPROCESSNAME { get; set; }
        public decimal CURRENTPROCESSNAMEID { get; set; }

        public decimal projectID { get; set; }
        string PROJECTNAME { get; set; }
        public decimal clientlocationID { get; set; }
        public string PreReceiveType { get; set; }
        public string projectname { get; set; }
        //public string projecttag { get; set; }

        decimal saveprocessID { get; set; }
        string saveprocessName { get; set; }
        decimal receiveprocessID { get; set; }
        string receiveprocessName { get; set; }


        public string LocationScanCode { get; set; }
        public decimal LocationScanCodeLocationID { get; set; }

        public string ESN
        {
            get { return _ESN; }
            set
            {
                _ESN = value;
                // look for the SO data.
                SOIFSNumber = "SOIFSNumber";
                SONumber = "SONumber";
                SOLine = "SOLine";
            }
        }
        public string IMEIVersion { get; set; }
        string RMANUMBER { get; set; }
        string PROJECTTAG { get; set; }
        string MiscNote { get; set; }

        public string IFSLocation { get; set; }
        //public string IFSSKU { get; set; }
        public string SKU { get; set; }

        string Carrier { get; set; }
        string Manufacturer { get; set; }
        string Model { get; set; }
        string Colour { get; set; }
        public decimal CarrierID { get; set; }
        public decimal ManufacturerID { get; set; }
        public decimal ModelID { get; set; }
        public decimal ColourID { get; set; }


        public string SOIFSNumber { get; set; }
        public string SONumber { get; set; }
        public string SOLine { get; set; }

        public string POVendor { get; set; }
        public string PONumber { get; set; }
        public string POLine { get; set; }
        public string POCost { get; set; }
        public string POCondition { get; set; }
        public bool UseSeedData { get; set; }

        bool force15IMEI { get; set; }

        //string IFS_Location { get; set; }
        public void SetPriorSOData(clsLinqDataContext ctx)
        {
            SOIFSNumber = "";
            SONumber = "";
            SOLine = "";
            OrderDetailReceiveDetail odRD = ctx.OrderDetailReceiveDetails.OrderByDescending(x => x.CreateDate).FirstOrDefault(x => x.ESN == _ESN);
            if (odRD != null)
            {
                OrderDetail od = ctx.OrderDetails.FirstOrDefault(x => x.OrderDetailID == odRD.OrderDetailID);
                if (od != null)
                {
                    if (od.OrderHeader.IFSOrderNo != null)
                    {
                        SOIFSNumber = od.OrderHeader.IFSOrderNo;
                    }
                    SONumber = od.OrderHeader.OrderNumber + " (" + od.OrderHeader.OrderStatus.Status + ")";
                    if (od.Line_NO != null) { SOLine = od.Line_NO.ToString(); }
                }
            }


            return;
        }
        public bool IsCarrierManufacturerCombo(clsLinqDataContext ctx, bool UseSeedData)
        {
            SKU = "";
            if (CarrierID > 0 && ManufacturerID > 0 && ModelID > 0 && ColourID > 0)
            {
                var Combo = ctx.MasterCarrierManufacturerLookups.FirstOrDefault(x => x.OptionCarrierID == CarrierID && x.OptionManufacturerID == ManufacturerID && x.OptionModelID == ModelID && x.OptionColourID == ColourID);
                if (Combo == null) { return false; }
                SKU = ctx.GetSKU(CarrierID, ManufacturerID, ModelID, ColourID);
                return true;
            }
            // This needs to be true, there is an assumption that if any of the 4 keys are empty, then it will be using seed data.
            if (UseSeedData == true)
            {
                ReceiveDetail RD = ctx.ReceiveDetails.Where(x => x.ESN == ESN).OrderBy(x => x.Version).FirstOrDefault();
                if (RD != null)
                {
                    SKU = ctx.GetSKU(RD.CarrierID, RD.ManufacturerID, RD.ModelID, RD.ColourID);
                }
                return true;
            }
            return false;
        }

        public List<CellData> attributeList = new List<CellData>();


        public RowData(decimal ProjectID, decimal ClientLocationID, decimal SaveProcessID, decimal ReceiveProcessID, string ProjectName, string SaveProcessName, string ReceiveProcessName, bool Force15IMEI, ReceiveDetailManager rdm)
        {
            RowDatax(ProjectID, ClientLocationID, SaveProcessID, ReceiveProcessID, ProjectName, SaveProcessName, ReceiveProcessName, Force15IMEI, "", rdm);
        }
        public RowData(decimal ProjectID, decimal ClientLocationID, decimal SaveProcessID, decimal ReceiveProcessID, string ProjectName, string SaveProcessName, string ReceiveProcessName, bool Force15IMEI, string ProjectTag, ReceiveDetailManager rdm)
        {
            RowDatax(ProjectID, ClientLocationID, SaveProcessID, ReceiveProcessID, ProjectName, SaveProcessName, ReceiveProcessName, Force15IMEI, ProjectTag, rdm);
        }
        public void RowDatax(decimal ProjectID, decimal ClientLocationID, decimal SaveProcessID, decimal ReceiveProcessID, string ProjectName, string SaveProcessName, string ReceiveProcessName, bool Force15IMEI, string ProjectTag, ReceiveDetailManager rdm)
        {
            projectID = ProjectID;
            clientlocationID = ClientLocationID;
            saveprocessID = SaveProcessID;
            receiveprocessID = ReceiveProcessID;
            receiveprocessName = ReceiveProcessName;
            saveprocessName = SaveProcessName;
            ESN = "";
            IMEIVersion = "000";
            MiscNote = "";
            STARTPROCESSNAME = "";
            CURRENTPROCESSNAME = "";
            CURRENTPROCESSNAMEID = -1;
            RECEIVEDETAILID = "";
            CLIENTLOCATIONID = "";
            CLIENTLOCATION = "";
            RMANUMBER = "";
            PROJECTTAG = ProjectTag;
            LocationScanCode = "";
            LocationScanCodeLocationID = -1;
            PROJECTNAME = ProjectName;
            Carrier = "";
            CarrierID = -1;
            Manufacturer = "";
            ManufacturerID = -1;
            Model = "";
            ModelID = -1;
            Colour = "";
            ColourID = -1;
            force15IMEI = Force15IMEI;
            POVendor = "";
            PONumber = "";
            POLine = "";
            POCost = "";
            POCondition = "";

            IFSSite = "";
            IFSProject = "";
            IFSVendor = "";
            ClientLocation cl = rdm.GetClientLocation(ClientLocationID);
            if (cl != null)
            {
                IFSSite = cl.IFSSite;
                IFSProject = cl.IFSProject;
                IFSVendor = cl.IFSPOVendor;
            }


        }


        public ReceiveDetailPreReceive SavePreReceivePrevious(clsLinqDataContext ctx, string username, List<decimal> AttributesToDelete, clsLog log, out string Message)
        {
            Message = "";
            bool isESNThere = (from x in ctx.ReceiveDetails
                               where x.ESN == ESN && x.Version == "000"
                               select true).FirstOrDefault();
            if (isESNThere == true) { Message = "ESN already on file"; return new ReceiveDetailPreReceive(); }

            //isESNThere = (from x in ctx.ReceiveDetailPreReceives
            //                   where x.ESN == ESN && x.Status == "Open"
            //                   select true).FirstOrDefault();
            //if (isESNThere == true) { Message = "ESN already Queued"; return new ReceiveDetailPreReceive(); }

            // Make sure there is a Previous IMEI... get the attributes for it.
            var rds = from x in ctx.ReceiveDetails
                      where x.ESN == ESN && x.ReceiveDetailStatus.Status.ToUpper() == "ACTIVE"
                      orderby x.Version
                      select x;
            if (rds.Count() < 2)
            {
                { Message = "No Previous ESN on file!"; return new ReceiveDetailPreReceive(); }
            }


            int iCount = 0;
            string[] aList = ("Carrier/Manufacturer/Model/Colour/Fault Code 1/Fault Code 2/Complaint/Complaint 2").Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (ReceiveDetail r in rds)
            {
                iCount++;
                if (iCount == 2)
                {
                    foreach (string t in aList)
                    {
                        ReceiveDetailItem a = r.ReceiveDetailItems.Where(x => x.Option.Question.Name == t).FirstOrDefault();
                        if (a != null)
                        {
                            // add the attribute cell
                            // Carrier, Manufacturer, Model, Colour, Fault Code 1, Fault Code 2, Complaint, Complaint 2 
                            CellData aData = new CellData(0, "");
                            aData.OptionID = a.OptionID;
                            aData.QuestionID = a.Option.QuestionID;
                            aData.QuestionName = a.Option.Question.Name;
                            aData.QuestionType = a.Option.Question.QuestionType.Type;

                            if (a.Option.Question.QuestionType.Type.ToUpper() == "DROPDOWN" || a.Option.Question.QuestionType.Type.ToUpper() == "RADIALBUTTON") { aData.text = a.Option.OptionText; aData.Value = "1"; }
                            else { aData.text = a.Value; aData.Value = a.Value; }
                            aData.type = "";
                            // look to see if this attribute was given via upload.
                            // if there from the upload, don't bring in the Previous data.
                            CellData old = attributeList.FirstOrDefault(x => x.QuestionID == a.Option.QuestionID);
                            if (old == null) { attributeList.Add(aData); }
                        }
                    }
                    break;
                }
            }

            ReceiveDetailPreReceive rd = (from x in ctx.ReceiveDetailPreReceives
                                          where x.ESN == ESN && x.Status == "Open"
                                          select x).FirstOrDefault();
            if (rd == null)
            {
                rd = new ReceiveDetailPreReceive();
                rd.CreateDate = DateTime.Now;
                rd.CreateUser = username;
                rd.ESN = ESN;
                rd.Status = "Open";
                rd.OnSite = false;
                log.LogIt("New Add");
                Message = "New Add";
            }
            else
            {
                log.LogIt("ESN already Queued");
                Message = "ESN already Queued";
            }

            rd.LastUpdateDate = DateTime.Now;
            rd.LastUpdateUser = username;
            rd.ProjectTag = PROJECTTAG.Length > 0 ? PROJECTTAG : rd.ProjectTag;
            rd.RMANumber = RMANUMBER.Length > 0 ? RMANUMBER : rd.RMANumber;

            rd.CarrierID = CarrierID > 0 ? CarrierID : rd.CarrierID;
            rd.ManufacturerID = ManufacturerID > 0 ? ManufacturerID : rd.ManufacturerID;
            rd.ModelID = ModelID > 0 ? ModelID : rd.ModelID;
            rd.ColourID = ColourID > 0 ? ColourID : rd.ColourID;
            rd.Type = PreReceiveType;
            rd.ProjectName = projectname;
            //rd.Grade = Grade > 0 ? Grade : rd.Grade;

            foreach (CellData cd in attributeList)
            {
                if (cd.Value.Trim().Length > 0)
                {
                    ReceiveDetailPreReceiveAttribute rda = null;
                    if (cd.QuestionType.ToUpper() != "DROPDOWN" && cd.QuestionType.ToUpper() != "RADIALBUTTON")
                    {
                        rda = rd.ReceiveDetailPreReceiveAttributes.FirstOrDefault(x => x.OptionID == cd.OptionID);
                    }
                    if (cd.QuestionType.ToUpper() == "DROPDOWN" || cd.QuestionType.ToUpper() == "RADIALBUTTON")
                    {
                        // See if the drop down option is there. -- 
                        rda = rd.ReceiveDetailPreReceiveAttributes.FirstOrDefault(x => x.OptionID == cd.OptionID);
                        if (rda == null)          // it is not there, we want to delete any that might be there... Cause there can only be one.
                        {
                            var oid = from o in ctx.Options where o.QuestionID == cd.QuestionID select o.OptionID;
                            var Attr = from x in rd.ReceiveDetailPreReceiveAttributes where oid.Contains(x.OptionID) select x;

                            //List<ReceiveDetailPreReceiveAttribute> al = new List<ReceiveDetailPreReceiveAttribute>();
                            foreach (var a in Attr)
                            {
                                log.LogIt("rd.ReceiveDetailPreReceiveAttributes.Remove(" + rd.ReceiveDetailPreReceiveID.ToString() + ":" + a.OptionID.ToString() + ")");
                                ctx.ReceiveDetailPreReceiveAttributes.DeleteOnSubmit(a);
                                //AttributesToDelete.Add(a.ReceiveDetailPreReceiveAttributeID);
                            }
                            //foreach (var a in al)
                            //{
                            //    ctx.ReceiveDetailPreReceiveAttributes.DeleteOnSubmit(a);
                            //}
                        }
                    }

                    if (rda == null)
                    {
                        rda = new ReceiveDetailPreReceiveAttribute();
                        rda.CreateDate = rd.CreateDate;
                        rda.CreateUser = rd.CreateUser;
                        rda.OptionID = cd.OptionID;
                        rd.ReceiveDetailPreReceiveAttributes.Add(rda);
                    }

                    rda.LastUpdateDate = rd.LastUpdateDate;
                    rda.LastUpdateUser = rd.LastUpdateUser;
                    rda.OptionID = cd.OptionID;
                    rda.Value = cd.Value;
                    //rd.ReceiveDetailPreReceiveAttributes.Add(rda);
                }
            }
            // get the previous IMEI attributes and add those to this one.





            return rd;
        }
        public ReceiveDetailPreReceive SavePreReceive(clsLinqDataContext ctx, string username, List<decimal> AttributesToDelete, clsLog log, out string Message)
        {
            Message = "";
            bool isESNThere = (from x in ctx.ReceiveDetails
                               where x.ESN == ESN && x.Version == "000"
                               select true).FirstOrDefault();
            if (isESNThere == true) { Message = "ESN already on file"; return new ReceiveDetailPreReceive(); }

            //isESNThere = (from x in ctx.ReceiveDetailPreReceives
            //                   where x.ESN == ESN && x.Status == "Open"
            //                   select true).FirstOrDefault();
            //if (isESNThere == true) { Message = "ESN already Queued"; return new ReceiveDetailPreReceive(); }


            ReceiveDetailPreReceive rd = (from x in ctx.ReceiveDetailPreReceives
                                          where x.ESN == ESN && x.Status == "Open"
                                          select x).FirstOrDefault();
            if (rd == null)
            {
                rd = new ReceiveDetailPreReceive();
                rd.CreateDate = DateTime.Now;
                rd.CreateUser = username;
                rd.ESN = ESN;
                rd.Status = "Open";
                rd.OnSite = false;
                log.LogIt("New Add");
                Message = "New Add";
            }
            else
            {
                log.LogIt("ESN already Queued");
                Message = "ESN already Queued";
            }

            rd.LastUpdateDate = DateTime.Now;
            rd.LastUpdateUser = username;
            rd.ProjectTag = PROJECTTAG.Length > 0 ? PROJECTTAG : rd.ProjectTag;
            rd.RMANumber = RMANUMBER.Length > 0 ? RMANUMBER : rd.RMANumber;

            rd.CarrierID = CarrierID > 0 ? CarrierID : rd.CarrierID;
            rd.ManufacturerID = ManufacturerID > 0 ? ManufacturerID : rd.ManufacturerID;
            rd.ModelID = ModelID > 0 ? ModelID : rd.ModelID;
            rd.ColourID = ColourID > 0 ? ColourID : rd.ColourID;
            rd.Type = PreReceiveType;
            rd.ProjectName = projectname;
            //rd.Grade = Grade > 0 ? Grade : rd.Grade;

            foreach (CellData cd in attributeList)
            {
                if (cd.Value.Trim().Length > 0)
                {
                    ReceiveDetailPreReceiveAttribute rda = null;
                    if (cd.QuestionType.ToUpper() != "DROPDOWN" && cd.QuestionType.ToUpper() != "RADIALBUTTON")
                    {
                        rda = rd.ReceiveDetailPreReceiveAttributes.FirstOrDefault(x => x.OptionID == cd.OptionID);
                    }
                    if (cd.QuestionType.ToUpper() == "DROPDOWN" || cd.QuestionType.ToUpper() == "RADIALBUTTON")
                    {
                        // See if the drop down option is there. -- 
                        rda = rd.ReceiveDetailPreReceiveAttributes.FirstOrDefault(x => x.OptionID == cd.OptionID);
                        if (rda == null)          // it is not there, we want to delete any that might be there... Cause there can only be one.
                        {
                            var oid = from o in ctx.Options where o.QuestionID == cd.QuestionID select o.OptionID;
                            var Attr = from x in rd.ReceiveDetailPreReceiveAttributes where oid.Contains(x.OptionID) select x;

                            //List<ReceiveDetailPreReceiveAttribute> al = new List<ReceiveDetailPreReceiveAttribute>();
                            foreach (var a in Attr)
                            {
                                log.LogIt("rd.ReceiveDetailPreReceiveAttributes.Remove(" + rd.ReceiveDetailPreReceiveID.ToString() + ":" + a.OptionID.ToString() + ")");
                                ctx.ReceiveDetailPreReceiveAttributes.DeleteOnSubmit(a);
                                //AttributesToDelete.Add(a.ReceiveDetailPreReceiveAttributeID);
                            }
                            //foreach (var a in al)
                            //{
                            //    ctx.ReceiveDetailPreReceiveAttributes.DeleteOnSubmit(a);
                            //}
                        }
                    }

                    if (rda == null)
                    {
                        rda = new ReceiveDetailPreReceiveAttribute();
                        rda.CreateDate = rd.CreateDate;
                        rda.CreateUser = rd.CreateUser;
                        rda.OptionID = cd.OptionID;
                        rd.ReceiveDetailPreReceiveAttributes.Add(rda);
                    }

                    rda.LastUpdateDate = rd.LastUpdateDate;
                    rda.LastUpdateUser = rd.LastUpdateUser;
                    rda.OptionID = cd.OptionID;
                    rda.Value = cd.Value;
                    //rd.ReceiveDetailPreReceiveAttributes.Add(rda);
                }
            }
            return rd;
        }
        public ReceiveDetail SaveForSeedData(clsLinqDataContext ctx, string username)
        {

            bool has000version = ctx.ReceiveDetails.Any(x => x.ReceiveDetailStatus.Status.ToUpper() != "GRAVEYARD" && x.ESN == ESN && x.Version == "000");
            if (has000version != true) { return null; }

            ReceiveDetail rd = new ReceiveDetail();
            rd.CreateDate = DateTime.Now;
            rd.LastUpdateDate = DateTime.Now;
            rd.ReceiveDate = DateTime.Now;
            rd.CreateUser = username;
            rd.LastUpdateUser = username;
            rd.ESN = ESN;
            rd.ProjectTag = PROJECTTAG;
            rd.RMANumber = RMANUMBER;
            rd.ProjectName = PROJECTNAME;
            rd.ClientLocationID = clientlocationID;
            rd.MiscNote = MiscNote;
            rd.ProjectID = projectID;
            rd.ProcessID = saveprocessID;
            rd.Version = IMEIVersion;
            rd.Carrier = Carrier;
            rd.Manufacturer = Manufacturer;
            rd.Model = Model;
            rd.Colour = Colour;
            rd.QTYIntegrated = 1;
            rd.IFSCondition = POCondition;
            rd.SKU = SKU;
            //rd.CarrierID = -1;
            //rd.ManufacturerID = -1;
            //rd.ModelID = -1;
            //rd.ColourID = -1;


            if (CarrierID > 0) { rd.CarrierID = CarrierID; }
            if (ManufacturerID > 0) { rd.ManufacturerID = ManufacturerID; }
            if (ModelID > 0) { rd.ModelID = ModelID; }
            if (ColourID > 0) { rd.ColourID = ColourID; }


            rd.DealerSubmittedID = -1;

            ReceiveDetailProcessLog log = new ReceiveDetailProcessLog();
            log.CreateDate = DateTime.Now;
            log.CreateUser = username;
            log.ProcessID = receiveprocessID;
            log.ProcessText = receiveprocessName;
            log.MiscText = "";
            rd.ReceiveDetailProcessLogs.Add(log);

            foreach (CellData cd in attributeList)
            {
                if (cd.Value.Trim().Length > 0)
                {
                    // Check to see if this attribute is in the Attribute Clone Seed List.
                    // if Yes, then we want to replace it with what is on the current version out there right now.

                    //--------------------------------------------------------------------------------------------
                    ReceiveDetailItem rdi = new ReceiveDetailItem();
                    rdi.CreateDate = rd.CreateDate;
                    rdi.CreateUser = rd.CreateUser;
                    rdi.LastUpdateDate = rd.LastUpdateDate;
                    rdi.LastUpdateUser = rd.LastUpdateUser;
                    rdi.ReceiveDate = rd.ReceiveDate;
                    rdi.OptionID = cd.OptionID;
                    rdi.Value = cd.Value;
                    rdi.Version = 0;
                    rd.ReceiveDetailItems.Add(rdi);
                }
            }

            return rd;
        }


        public ReceiveDetail Save(clsLinqDataContext ctx, ReceiveDetailManager rdm, short DirectiveIgnore, string username)
        {

            //bool has000version = ctx.ReceiveDetails.Any(x => x.ReceiveDetailStatus.Status.ToUpper() != "GRAVEYARD" && x.ESN == ESN && x.Version == "000");
            //if (has000version == true) { return null; }
            // = new ReceiveDetailManager(username);
            ReceiveDetail rd = new ReceiveDetail();
            rd.CreateDate = DateTime.Now;
            rd.LastUpdateDate = DateTime.Now;
            rd.ReceiveDate = DateTime.Now;
            rd.CreateUser = username;
            rd.LastUpdateUser = username;
            rd.ESN = ESN;
            rd.ProjectTag = PROJECTTAG;
            rd.RMANumber = RMANUMBER;
            rd.ProjectName = PROJECTNAME;
            rd.ClientLocationID = clientlocationID;
            rd.MiscNote = MiscNote;
            rd.ProjectID = projectID;
            rd.ProcessID = saveprocessID;
            rd.Version = IMEIVersion;
            rd.Carrier = Carrier;
            rd.Manufacturer = Manufacturer;
            rd.Model = Model;
            rd.Colour = Colour;
            rd.QTYIntegrated = 1;
            rd.ISFTransactionDirective = DirectiveIgnore;

            //rd.CarrierID = -1;
            //rd.ManufacturerID = -1;
            //rd.ModelID = -1;
            //rd.ColourID = -1;
            rd.IFSCondition = POCondition;
            rd.SKU = SKU;                      //  rdm.GetSku(ManufacturerID, ModelID, CarrierID, ColourID);
            rd.IFSLocation = IFSLocation;
            //rd.IFSLocation = ctx.GetIFSLocation_B(-1, receiveprocessName,clientlocationID, "");

            if (CarrierID > 0) { rd.CarrierID = CarrierID; }
            if (ManufacturerID > 0) { rd.ManufacturerID = ManufacturerID; }
            if (ModelID > 0) { rd.ModelID = ModelID; }
            if (ColourID > 0) { rd.ColourID = ColourID; }


            rd.DealerSubmittedID = -1;

            ReceiveDetailProcessLog log = new ReceiveDetailProcessLog();
            log.CreateDate = DateTime.Now;
            log.CreateUser = username;
            log.ProcessID = receiveprocessID;
            log.ProcessText = receiveprocessName;
            log.MiscText = "";
            rd.ReceiveDetailProcessLogs.Add(log);

            foreach (CellData cd in attributeList)
            {
                if (cd.Value.Trim().Length > 0)
                {
                    // Check to see if this attribute is in the Attribute Clone Seed List.
                    // if Yes, then we want to replace it with what is on the current version out there right now.

                    //--------------------------------------------------------------------------------------------
                    ReceiveDetailItem rdi = new ReceiveDetailItem();
                    rdi.CreateDate = rd.CreateDate;
                    rdi.CreateUser = rd.CreateUser;
                    rdi.LastUpdateDate = rd.LastUpdateDate;
                    rdi.LastUpdateUser = rd.LastUpdateUser;
                    rdi.ReceiveDate = rd.ReceiveDate;
                    rdi.OptionID = cd.OptionID;
                    rdi.Value = cd.Value;
                    rdi.Version = 0;
                    rd.ReceiveDetailItems.Add(rdi);
                    //if (cd.QuestionName.ToUpper() == "CARRIER")
                    //{
                    //    rd.CarrierID = cd.OptionID;
                    //    rd.Carrier = cd.text;
                    //}
                }
            }

            return rd;
        }

        public void AddCellData(string RawText, HeaderData Header)
        {
            if (RawText == null) { RawText = ""; }
            #region IMEI
            if (Header.type == "IMEI")
            {
                switch (Header.text.ToUpper())
                {
                    case "ESN":
                        ESN = RawText;
                        if (force15IMEI == true && ESN.Length != 15)
                        {
                            if (ESN.Length < 15) { ESN = ESN.PadLeft(15, '0'); }
                            if (ESN.Length > 15) { ESN = ESN.Substring(0, 15); }
                        }
                        break;
                    case "VERSION":
                        if (RawText.Trim().Length > 0) { IMEIVersion = RawText; }
                        break;
                    case "TYPE":
                        if (RawText.Trim().Length > 0) { PreReceiveType = RawText; }
                        break;
                    case "PROJECTNAME":
                        if (RawText.Trim().Length > 0) { projectname = RawText; }
                        break;

                    //case "STARTPROCESSNAME":
                    //    STARTPROCESSNAME = RawText;
                    //    break;
                    case "CURRENTPROCESSNAME":
                        CURRENTPROCESSNAME = RawText;
                        break;
                    //case "RECEIVEDETAILID":
                    //    RECEIVEDETAILID = RawText;
                    //    break;
                    //case "CLIENTLOCATIONID":
                    //    CLIENTLOCATIONID = RawText;
                    //    break;
                    //case "CLIENTLOCATION":
                    //    CLIENTLOCATION = RawText;
                    //    break;
                    case "LOCATION DEALER CODE":
                        LocationScanCode = RawText;
                        break;
                    case "RMANUMBER":
                        RMANUMBER = RawText;
                        break;
                    case "PROJECTTAG":
                        PROJECTTAG = RawText;
                        break;
                    case "Site Location":
                        IFSLocation = RawText;
                        break;
                    //case "PROJECTNAME":
                    //    PROJECTNAME = RawText;
                    //    break;
                    default:
                        break;
                }
            }
            #endregion
            #region ATTRIBUTE
            if (Header.type == "ATTRIBUTE")
            {
                string[] aList = { RawText };
                if (Header.questiontype.ToUpper() == "CHECKBOX")
                {
                    // we need to parse out the attributes.
                    aList = RawText.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                }

                foreach (string t in aList)
                {
                    CellData cd = new CellData(Header.col, t);
                    PairIDValue o = null;
                    if (Header.isCloneTrasposeColumn == true)
                    {
                        o = Header.T_OptionList.FirstOrDefault(x => x.Desc.ToUpper() == cd.text.ToUpper());
                        if (Header.T_OptionList.Count == 1)
                        {
                            o = Header.T_OptionList[0];
                        }
                    }
                    else
                    {
                        o = Header.OptionList.FirstOrDefault(x => x.Desc.ToUpper() == cd.text.ToUpper());
                        if (Header.OptionList.Count == 1)
                        {
                            o = Header.OptionList[0];
                        }
                    }
                    if (o != null)
                    {
                        //if (Header.Name.ToUpper() == "CARRIER") { Carrier = cd.text; CarrierID = o.ID; }
                        cd.OptionID = o.ID;
                        cd.Value = cd.text;
                        cd.QuestionType = Header.questiontype;
                        cd.QuestionID = Header.QuestionID;
                        cd.QuestionName = Header.Name;
                        if (Header.Name.ToUpper() == "CARRIER") { Carrier = cd.text; CarrierID = o.ID; }
                        if (Header.Name.ToUpper() == "MANUFACTURER") { Manufacturer = cd.text; ManufacturerID = o.ID; }
                        if (Header.Name.ToUpper() == "MODEL") { Model = cd.text; ModelID = o.ID; }
                        if (Header.Name.ToUpper() == "COLOUR") { Colour = cd.text; ColourID = o.ID; }
                        if (Header.Name.ToUpper() == "IFS PO NUMBER") { PONumber = cd.text; }
                        if (Header.Name.ToUpper() == "IFS PO LINE NUMBER") { POLine = cd.text; }
                        if (Header.Name.ToUpper() == "IFS VENDOR" || Header.Name.ToUpper() == "IFS SUPPLIER") { POVendor = cd.text; }
                        if (Header.Name.ToUpper() == "IFS CONDITIONS") { POCondition = cd.text; }

                        if (Header.OptionList.Count > 1) { cd.Value = "1"; }       // this tells me it is a check box, radial button or drop down. (0/1)
                        attributeList.Add(cd);
                    }
                }
            }
            #endregion
        }
        //public void AddNewAttributes(clsLinqDataContext ctx, string username, string Name, string Value)
        //{
        //}
    }
    public class HeaderData
    {
        public int col { get; set; }
        public bool isCloneColumn { get; set; }
        public string type { get; set; }       // Attribute, IMEI, Dead
        public string questiontype { get; set; }
        public string rawtext { get; set; }
        public string text { get; set; }
        public decimal QuestionID { get; set; }
        public string Name { get; set; }
        public List<PairIDValue> OptionList = new List<PairIDValue>();

        public bool isCloneTrasposeColumn { get; set; }
        public string T_type { get; set; }       // Attribute, IMEI, Dead
        public string T_questiontype { get; set; }
        public string T_rawtext { get; set; }
        public string T_text { get; set; }
        public decimal T_QuestionID { get; set; }
        public string T_Name { get; set; }
        public List<PairIDValue> T_OptionList = new List<PairIDValue>();

        public HeaderData(int Col, string RawText)
        {
            if (RawText == null) { RawText = ""; }
            col = Col;
            rawtext = RawText;
            text = RawText.Replace("_", " ");
            questiontype = "Text";
            isCloneColumn = false;
            isCloneTrasposeColumn = false;
            QuestionID = -1;
            Name = "";

            T_rawtext = RawText;
            T_text = RawText.Replace("_", " ");
            T_questiontype = "Text";
            T_QuestionID = -1;
            T_Name = "";
        }

        public void Fill(QuestionManager qm, decimal ProjectID, string UserName, List<string> ESNDetail, List<PairIDValue> QID_Name)
        {
            QuestionID = -1;
            type = "Dead";
            if (ESNDetail.Contains(text.ToUpper()) == true)
            {
                type = "IMEI";
            }
            else
            {
                PairIDValue q = QID_Name.FirstOrDefault(x => x.Desc.ToUpper() == text.ToUpper());
                if (q != null)
                {
                    Name = q.Desc;
                    questiontype = qm.GetThisQuestionType(q.ID);
                    OptionList = qm.GetAllOptionPairIDNames(q.ID);
                    type = "ATTRIBUTE";
                    QuestionID = q.ID;
                }
            }
        }
        public void FillTranspose(QuestionManager qm, decimal ProjectID, string UserName, List<string> ESNDetail, List<PairIDValue> QID_Name)
        {
            T_QuestionID = -1;
            T_type = "Dead";
            if (ESNDetail.Contains(T_text.ToUpper()) == true)
            {
                T_type = "IMEI";
            }
            else
            {
                PairIDValue q = QID_Name.FirstOrDefault(x => x.Desc.ToUpper() == T_text.ToUpper());
                if (q != null)
                {
                    T_Name = q.Desc;
                    T_questiontype = qm.GetThisQuestionType(q.ID);
                    T_OptionList = qm.GetAllOptionPairIDNames(q.ID);
                    T_type = "ATTRIBUTE";
                    T_QuestionID = q.ID;
                }
            }
        }

    }
    public class CellData
    {
        public int col { get; set; }
        public string type { get; set; }       // Attribute, IMEI, Dead
        public string rawtext { get; set; }
        public string text { get; set; }
        public string Value { get; set; }
        public decimal OptionID { get; set; }
        public decimal QuestionID { get; set; }
        public string QuestionType { get; set; }
        public string QuestionName { get; set; }

        public CellData(int Col, string RawText)
        {
            col = Col;
            rawtext = RawText;
            text = RawText;
        }

    }

    public class CloneCellData
    {
        public int col { get; set; }
        public string Value { get; set; }
        public decimal QuestionID { get; set; }

        public CloneCellData(int Col, string RawText)
        {
            col = Col;
            Value = RawText;
            QuestionID = -1;
        }

    }

    public class TranslationTable
    {
        List<TranslationData> translist = new List<TranslationData>();

        public TranslationTable()
        {
        }

        public void Add(string Source, string Target)
        {
            TranslationData td = new TranslationData(Source, Target);
            translist.Add(td);
        }
        public string Translate(string Source)
        {
            TranslationData td = translist.FirstOrDefault(x => x.SourceValue.ToUpper() == Source.ToUpper());
            if (td == null) { return Source; }
            return td.TargetValue;
        }

    }

    public class TranslationData
    {
        public string SourceValue { get; set; }
        public string TargetValue { get; set; }

        public TranslationData(string Source, string Target)
        {
            SourceValue = Source;
            TargetValue = Target;
        }
    }
}