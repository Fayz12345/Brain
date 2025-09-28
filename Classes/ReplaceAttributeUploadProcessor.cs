using System;
using System.Collections.Generic;
using System.Linq;
//using System.Web.Security;

//using Factory_DataModel;
using BW_WebApp.DataManagers;
using Syncfusion.XlsIO;

namespace BW_WebApp.Classes
{
    public class ReplaceAttributeUploadProcessor
    {
        public string filename { get; set; }
        public string username { get; set; }
        //public string orderentrynumber { get; set; }
        //string BinNumber { get; set; }
        //public decimal projectID { get; set; }
        public int RowCount { get; set; }
        public bool isXLS { get; set; }
        //public IFSLocation location { get; set; }
        //bool force15IMEI { get; set; }

        public ExcelEngine excelEngine = null;
        public IApplication application = null;
        public IWorkbook workbook = null;


        private List<HeaderData> Header = new List<HeaderData>();

        public ReplaceAttributeUploadProcessor(string DrivePathFileNameXLS_toUpload, string UserName)
        {
            filename = DrivePathFileNameXLS_toUpload;
            isXLS = true;
            username = UserName;
        }

        public string LoadData()
        {
            string rvalue = "";
            if (isXLS == true) { rvalue = LoadDatafromXLS(); }
            return rvalue;
        }
        public string LoadDatafromXLS()
        {
            if (isXLS == false) { return "No File to Parse"; }
            string rvalue = "";
            //rvalue = "No Order Entry Number given.";
            //if (orderentrynumber.Trim().Length > 0)
            //{
            rvalue = "";
            RowCount = 0;
            DateTime starttime = DateTime.Now;
            excelEngine = new ExcelEngine();
            //Step 2 : Instantiate the excel application object.
            application = excelEngine.Excel;
            //Open an existing spreadsheet which will be used as a template for generating the new spreadsheet.
            //After opening, the workbook object represents the complete in-memory object model of the template spreadsheet.
            workbook = application.Workbooks.Open(filename, ExcelOpenType.Automatic);
            //The first worksheet object in the worksheets collection is accessed.
            IWorksheet sheet = workbook.Worksheets[0];
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                //rvalue = LoadHeaderData(sheet);
                rvalue = LoadData(ctx, sheet);
            }
            //workbook.Close();
            //excelEngine.Dispose();
            DateTime endtime = DateTime.Now;
            TimeSpan diffResult = endtime.Subtract(starttime);
            if (rvalue.Length == 0)
            {
                rvalue = "(HH:MM:SS)" + diffResult.Hours.ToString() + ":" + diffResult.Minutes.ToString() + ":" + diffResult.Seconds.ToString() + "  -- Row Count:" + RowCount.ToString();
            }
            //}
            return rvalue;
        }
        private string LoadData(clsLinqDataContext ctx, IWorksheet sheet)
        {
            //return "Inside Load Data";


            int NameCol = 1;
            int SourceCol = 2;
            int TargetCol = 3;
            int SourceIDCol = 4;
            int TargetIDCol = 5;
            int RecordsCol = 6;
            int Records = 0;
            int ErrorCol = 7;
            string rvalue = "";
            List<ReplaceAttributeRowData> Data = new List<ReplaceAttributeRowData>();

            sheet.Range[1, ErrorCol].Text = "Status";
            sheet.Range[1, SourceIDCol].Text = "SourceID";
            sheet.Range[1, TargetIDCol].Text = "TargetID";
            sheet.Range[1, RecordsCol].Text = "Records Affected";

            for (int row = 2; row < 1000000; row++)
            {
                if (sheet.Range[row, 1].Value == null || sheet.Range[row, NameCol].Value.Length == 0) { break; }

                ReplaceAttributeRowData rd = new ReplaceAttributeRowData(ctx, sheet.Range[row, NameCol].Value
                                                                            , sheet.Range[row, SourceCol].Value
                                                                            , sheet.Range[row, TargetCol].Value, row, username);
                if (rd.ErrorMessage.Length == 0) { Records = rd.LoadData(); }
                if (rd.ErrorMessage.Length > 0) { sheet.Range[row, ErrorCol].Text = rd.ErrorMessage; }
                else { Data.Add(rd); sheet.Range[row, ErrorCol].Text = "Loaded"; ; }

                if (rd.isFound == true) { Records = rd.LoadData(); }
                if (rd.isFound == false) { sheet.Range[row, ErrorCol].Text = rd.ErrorMessage; }
                else { Data.Add(rd); sheet.Range[row, ErrorCol].Text = "Loaded"; ; }

                sheet.Range[row, SourceIDCol].Text = rd.SourceID.ToString();
                sheet.Range[row, TargetIDCol].Text = rd.TargetID.ToString();
                sheet.Range[row, RecordsCol].Text = rd.RecordsAffected.ToString();
            }
            return rvalue;
        }

        class ReplaceAttributeRowData
        {
            public string AttributeName = "";
            public string SourceABBR = "";
            public decimal? SourceID = -1;
            public string TargetABBR = "";
            public decimal? TargetID = -1;

            public int? RecordsAffected = 0;



            //public string ModelAbbr = "";
            //public ReceiveDetail RD = null;
            public int row = 0;
            public string ErrorMessage = "";
            //public string mMemoryList = "";
            //public PairIDValue Model = new PairIDValue();
            //public List<PairIDValue> MemoryList = new List<PairIDValue>();
            //public IFSLocation Location;
            //public decimal QTY = 0;
            public bool isFound = false;
            public clsLinqDataContext ctx = null;
            string userName = "";

            public ReplaceAttributeRowData(clsLinqDataContext ctxx, string Attribname, string sourceabbr, string targetabbr, int Row, string UserName)
            {
                ctx = ctxx;
                AttributeName = Attribname;
                SourceABBR = sourceabbr;
                TargetABBR = targetabbr;
                userName = UserName;
                row = Row;
                isFound = true;
                userName = UserName;

                // Verify the Data is valid
                if (SourceABBR == TargetABBR) { ErrorMessage += ("Source (" + SourceABBR + ")/Source (" + TargetABBR + ") equal"); isFound = false; }
                if (SourceABBR.Length == 0) { ErrorMessage += ("Source blank! (" + SourceABBR + ")"); isFound = false; }
                if (TargetABBR.Length == 0) { ErrorMessage += ("Target blank! (" + TargetABBR + ")"); isFound = false; }
                if (Attribname.Length == 0) { ErrorMessage += ("Attrib Name blank! (" + Attribname + ")"); isFound = false; }
                if (isFound == false) { return; }

                var S1 = ctx.Options.FirstOrDefault(x => x.Name == SourceABBR && x.Question.Name == AttributeName);
                if (S1 == null) { ErrorMessage += ("Source (" + SourceABBR + ")invalid/"); }
                var T1 = ctx.Options.FirstOrDefault(x => x.Name == TargetABBR && x.Question.Name == AttributeName);
                if (T1 == null) { ErrorMessage += ("Target (" + TargetABBR + ")invalid/"); }

                if (S1 != null) { SourceID = S1.OptionID; isFound = false; }
                if (T1 != null) { TargetID = T1.OptionID; isFound = false; }

                // parse the list
                //List<string> values = mMemoryList.Split(',').Select(sValue => sValue.Trim()).ToList();
                //// Verify each are valid
                //foreach (string a in values)
                //{
                //    var dta = ctx.Options.FirstOrDefault(x => x.Name == a && x.Question.Name == "Memory");
                //    if (dta == null) { ErrorMessage += ("Memory(" + a + ")invalid/"); }
                //    else
                //    {
                //        PairIDValue m = new PairIDValue();
                //        m.ID = dta.OptionID;
                //        m.Desc = a;
                //        MemoryList.Add(m);
                //    }
                //}

            }

            public int LoadData()
            {
                int? RecordsAffected = 0;
                return ctx.Utility_ReplaceOptionAttributeID(SourceID, TargetID, userName, ref RecordsAffected);
            }
        }
    }
}