using System;
using System.Collections.Generic;
using System.Linq;

//using Factory_DataModel;
using BW_WebApp.DataManagers;
using Syncfusion.XlsIO;

namespace BW_WebApp.Classes
{
    public class UploadProcessor
    {

        clsLog log;
        public string filename { get; set; }
        public string username { get; set; }

        public int RowCount { get; set; }
        public bool isXLS { get; set; }

        public ExcelEngine excelEngine = new ExcelEngine();
        public IApplication application = null;
        public IWorkbook workbook = null;

        public UploadProcessor(string DrivePathFileNameXLS_toUpload, string UserName, clsLog log)
        {
            this.log = log;
            //cloneparentattribute = false;
            //attributeclonelist = AttributeCloneList;
            //attributeclonelist2.Clear();
            //foreach (string a in attributeclonelist)
            //{
            //    attributeclonelist2.Add(a);
            //}
            //if (AttributeCloneList.Count > 0) { cloneparentattribute = true; }
            ////attributeclonelist.Add("Carrier");
            ////attributeclonelist.Add("Manufacturer");
            ////attributeclonelist.Add("Model");
            ////attributeclonelist.Add("Colour");
            ////attributeclonelist.Add("Disposition");          // may need to remove this one.
            ////attributeclonelist.Add("QC");                   // may need to change this to another name when Sandbox becomes live.
            ////attributeclonelist.Add("Grade");                   // This is for when Sandbox becomes live.

            filename = DrivePathFileNameXLS_toUpload;
            isXLS = true;
            username = UserName;
            //clientlocationid = ClientLocationID;
            //projectID = ProjectID;
            //saveprocessName = "Save";
            //receiveprocessName = "Receive";
            //forceIMEI15 = ForceIMEI15;


            //ReceiveDetailManager rm = new ReceiveDetailManager(username);
            //StatusID = rm.GetStatusID("Active");

            //ProjectManager pm = new ProjectManager(UserName);
            //Project p = pm.GetProject(projectID);
            //projectname = p.Name;

            //ProcessManager prm = new ProcessManager(UserName);
            //saveprocessID = prm.GetProcessidFromName(saveprocessName);
            //receiveprocessID = prm.GetProcessidFromName(receiveprocessName, projectname);
        }


        public string LoadDiscrepancyData(bool VerifyOnly)
        {
            this.log.LogIt("   LoadDiscrepancyData Started:");
            string rvalue = "";
            if (isXLS == true) { rvalue = LoadDiscrepancyDataFromXLS(VerifyOnly); }
            return rvalue;
        }
        public string LoadDiscrepancyDataFromXLS(bool VerifyOnly)
        {
            this.log.LogIt("   LoadDiscrepancyDataFromXLS Started:");
            //LoadHeaderTranslationTable();
            RowCount = 0;
            string rvalue = "";
            DateTime starttime = DateTime.Now;
            excelEngine = new ExcelEngine();
            //Step 2 : Instantiate the excel application object.
            application = excelEngine.Excel;
            workbook = application.Workbooks.Open(filename, ExcelOpenType.Automatic);
            //The first worksheet object in the worksheets collection is accessed.
            IWorksheet sheet = workbook.Worksheets[0];
            //LoadLocationScanCodes();
            //rvalue = LoadHeaderData(sheet);

            rvalue = LoadData(sheet, VerifyOnly);

            // we should sent the excel back to the browser.

            //workbook.Save();
            //workbook.Close();
            //excelEngine.Dispose();
            DateTime endtime = DateTime.Now;
            TimeSpan diffResult = endtime.Subtract(starttime);
            rvalue = "(HH:MM:SS)" + diffResult.Hours.ToString() + ":" + diffResult.Minutes.ToString() + ":" + diffResult.Seconds.ToString() + "  -- Row Count:" + RowCount.ToString();
            return rvalue;
        }

        private string LoadData(IWorksheet sheet, bool VerifyOnly)
        {
            this.log.LogIt("   LoadData Started:");
            string rvalue = "";
            int ErrorColumn = -1;
            string ErrorText = "";

            ErrorColumn = 20;


            string sID = "";
            string sDate = "";
            string sType = "";
            string sDivision = "";
            string sStore = "";
            string sTransfer = "";
            string sReturnTransfer = "";
            string sIMEI = "";
            string sDiscrepancy = "";
            string sResolved = "";
            string sFirstAttempt = "";
            string sSecondAttempt = "";
            string sThirdAttempt = "";
            string sOutcome = "";
            string sStoreName = "";

            string ScanKey = "";

            DiscrepincyManager dm = new DiscrepincyManager(username);
            ClientManager cm = new ClientManager(username);
            ClientLocation cl = null;


            QuestionManager qm = new QuestionManager(username);
            List<Option> DiscrDesc = new List<Option>();
            DiscrDesc = qm.GetQuestionOptionList("Discr Desc").ToList();

            List<Option> DiscrType = new List<Option>();
            DiscrType = qm.GetQuestionOptionList("Discr Type").ToList();

            List<Option> DiscrOutCome = new List<Option>();
            DiscrOutCome = qm.GetQuestionOptionList("Discr OutCome").ToList();


            //string celldata = "";
            bool hasError = false;
            for (int row = 2; row < 100000; row++)
            {

                this.log.LogIt("     Reading Rows:" + row.ToString());
                if (sheet.Range[row, 1].Value == null || sheet.Range[row, 1].Value.Length == 0) { break; }
                RowCount += 1;
                ErrorText = "";
                hasError = false;

                sID = sheet.Range[row, 1].Value;
                sDate = sheet.Range[row, 2].Value;
                sType = sheet.Range[row, 3].Value;
                sDivision = sheet.Range[row, 4].Value;
                sStore = sheet.Range[row, 5].Value;
                sTransfer = sheet.Range[row, 6].Value;
                sReturnTransfer = sheet.Range[row, 7].Value;
                sIMEI = sheet.Range[row, 8].Value;
                sDiscrepancy = sheet.Range[row, 9].Value;
                sResolved = sheet.Range[row, 10].Value;
                sFirstAttempt = sheet.Range[row, 11].Value;
                sSecondAttempt = sheet.Range[row, 12].Value;
                sThirdAttempt = sheet.Range[row, 13].Value;
                sOutcome = sheet.Range[row, 14].Value;
                sStoreName = sheet.Range[row, 15].Value;


                // correct some entries we know are coming that need to be cleaned up a bit.
                if (sOutcome.Length == 0) { sOutcome = "None"; }
                if (sDiscrepancy.Length == 0) { sDiscrepancy = "None"; }
                if (sType.Length == 0) { sType = "None"; }

                //Remove any CRLF char that may be in the string... we don't want any                                
                //celldata = sheet.Range[row, 1].Value;
                //var newString = string.Join(" ", Regex.Split(celldata, @"(?:\r\n|\n|\r)")).Trim();

                Discrepancy d = new Discrepancy();
                decimal id = -1;
                DateTime date = DateTime.Now;
                d.ClientID = -1;
                d.ClientLocationID = -1;
                d.DiscrepancyID = -1;
                d.Resolved = false;
                d.Division = "";

                #region EditCheck
                // ID

                if (decimal.TryParse(sID, out id) == false)
                {
                    hasError = true;
                    ErrorText += "Invalid ID;";
                }
                else { d.DiscrepancyID = id; }



                // Client location
                ScanKey = sDivision + sStore;
                cl = cm.GetClientLocation(ScanKey);
                if (cl == null)
                {
                    // If we have not found the scankey, we are going to look for the storeName.
                    cl = cm.getClientLocationViaName(sStoreName);
                    if (cl == null)
                    {
                        hasError = true;
                        ErrorText += "Division+Store/Or Name not Valid;";
                    }
                    else { d.ClientID = cl.ClientID; d.ClientLocationID = cl.ClientLocationID; }
                }
                else { d.ClientID = cl.ClientID; d.ClientLocationID = cl.ClientLocationID; }


                // Create Date
                if (DateTime.TryParse(sDate, out date) == false)
                {
                    hasError = true;
                    ErrorText += "Date not Valid;";
                }
                else { d.CreateDate = date; d.CreateUser = username; }
                if (DiscrType.Any(x => x.OptionText == sType) == false)
                {
                    hasError = true;
                    ErrorText += "Type not Valid;";
                }
                else { d.Type = sType; }
                if (DiscrDesc.Any(x => x.OptionText == sDiscrepancy) == false)
                {
                    hasError = true;
                    ErrorText += "Discrepancy not Valid;";
                }
                else { d.DiscrepancyText = sDiscrepancy; }

                if (DiscrOutCome.Any(x => x.OptionText == sOutcome) == false)
                {
                    //hasError = true;
                    //ErrorText += "OutCome not Valid;";
                    d.OutCome = "None";
                }
                else { d.OutCome = sOutcome; }
                #endregion
                #region Load Non Edit Fields.
                d.LastUpdateUser = username;
                d.LastUpdateDate = DateTime.Now;
                d.ReturnTransfer = sTransfer;
                d.ReturnTransfer = sReturnTransfer;
                d.IMEI = sIMEI;

                if (DateTime.TryParse(sFirstAttempt, out date) == false)
                {   //hasError = true;                    //ErrorText += "Invalid Create Date:";
                }
                else { d.AttemptDate = date; d.AttemptUser = username; }
                if (DateTime.TryParse(sSecondAttempt, out date) == false)
                {   //hasError = true;                    //ErrorText += "Invalid Create Date:";
                }
                else { d.AttemptDate2 = date; d.AttemptUser3 = username; }
                if (DateTime.TryParse(sThirdAttempt, out date) == false)
                {   //hasError = true;                   //ErrorText += "Invalid Create Date:";
                }
                else { d.AttemptDate3 = date; d.AttemptUser3 = username; }
                if (sResolved.ToUpper() == "TRUE") { d.Resolved = true; }
                #endregion

                if (VerifyOnly == false && hasError == false)
                {
                    ErrorText = dm.SaveHistorical(d); ;
                }
                sheet.Range[row, ErrorColumn].Value = ErrorText;
            }
            return rvalue;
        }
    }
}