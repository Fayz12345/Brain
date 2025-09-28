using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Drawing;

using System.Text.RegularExpressions;
using BW_WebApp.DataManagers;
using Syncfusion.XlsIO;

namespace BW_WebApp.Classes
{
    public class IMEIVersionUploadProcessor
    {

        clsLog log;
        public string filename { get; set; }
        public string username { get; set; }
        public int RowCount { get; set; }
        public bool isXLS { get; set; }
        public ExcelEngine excelEngine = new ExcelEngine();
        public IApplication application = null;
        public IWorkbook workbook = null;


        public IMEIVersionUploadProcessor(string DrivePathFileNameXLS_toUpload, string UserName, clsLog log)
        {
            this.log = log;
            filename = DrivePathFileNameXLS_toUpload;
            isXLS = true;
            username = UserName;
        }

        public string LoadIMEIData()
        {
            this.log.LogIt("   LoadIMEIData Started:");
            string rvalue = "";
            if (isXLS == true) { rvalue = LoadIMEIDatafromXLS(); }
            return rvalue;
        }
        public string LoadIMEIDatafromXLS()
        {
            this.log.LogIt("   LoadIMEIDatafromXLS Started:");
            RowCount = 0;
            string rvalue = "";
            DateTime starttime = DateTime.Now;
            excelEngine = new ExcelEngine();
            //Step 2 : Instantiate the excel application object.
            application = excelEngine.Excel;
            workbook = application.Workbooks.Open(filename, ExcelOpenType.Automatic);
            //The first worksheet object in the worksheets collection is accessed.
            IWorksheet sheet = workbook.Worksheets[0];
            rvalue = LoadData(sheet);
            DateTime endtime = DateTime.Now;
            TimeSpan diffResult = endtime.Subtract(starttime);
            rvalue = "(HH:MM:SS)" + diffResult.Hours.ToString() + ":" + diffResult.Minutes.ToString() + ":" + diffResult.Seconds.ToString() + "  -- Row Count:" + RowCount.ToString();
            return rvalue;
        }
        private string LoadData(IWorksheet sheet)
        {
            this.log.LogIt("   LoadData Started:");
            string rvalue = "";
            int ErrorColumn = -1;
            string ErrorText = "";
            string ESN = "";
            string Version = "";

            List<ReceiveHeader> rhList = new List<ReceiveHeader>();
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                //            ReceiveHeader rh = new ReceiveHeader();
                // start on the second row
                ErrorColumn = 3;
                for (int row = 2; row < 100000; row++)
                {
                    this.log.LogIt("     Reading Rows:" + row.ToString());
                    if (sheet.Range[row, 1].Value == null || sheet.Range[row, 1].Value.Length == 0) { break; }
                    if (sheet.Range[row, 2].Value == null || sheet.Range[row, 2].Value.Length == 0) { break; }


                    ErrorText = "";
                    ESN = sheet.Range[row, 1].Value;
                    Version = sheet.Range[row, 2].Value;

                    if (ESN.Length == 0)
                    {
                        ErrorText = "No IMEI Given";
                    }
                    if (Version.Length == 0)
                    {
                        if (ErrorText.Length > 0) { ErrorText += "/"; }
                        ErrorText += "No Version Given";
                    }
                    if (ErrorText.Length == 0)
                    {
                        ctx.Update_ReceiveDetailESNVersionToVersionZero(ESN, Version, username, ref ErrorText);
                    }
                    sheet.Range[row, ErrorColumn].Value = ErrorText;
                }
                return rvalue;
            }
        }
    }
}
