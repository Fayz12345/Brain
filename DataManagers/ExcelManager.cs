using System;
using System.Collections.Generic;
//using Factory_Businesslayer;
//using Factory_DataModel.Classes;
//using BusinessLayer;
// using DAL;

using System.Data.SqlClient;
using System.Linq;
using System.Web.Configuration;
//using SoftArtisans.OfficeWriter.ExcelWriter;
using Syncfusion.XlsIO;

namespace BW_WebApp.DataManagers
{
    public class ExcelManager
    {
        string _UserName = "";
        public string Message = "";
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
        public string FileName = "";
        public ExcelManager(string UserName)
        {
            _UserName = UserName;

        }

        public IWorkbook ExportToExcel(IWorkbook workbook, string cmd, string fileName, ref string message)
        {
            List<string> fieldListToReport = new List<string>();
            return ExportToExcel(workbook, cmd, fileName, fieldListToReport, ref message);
        }
        public IWorkbook ExportToExcel(IWorkbook workbook, string scmd, string fileName, List<string> fieldListToReport, ref string message)
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand();
            scmd = scmd.Replace('~', '\'');
            cmd.CommandText = scmd;
            cmd.CommandTimeout = 360;
            cmd.Connection = cn;

            // if csv 
            //if (chkCSV.Checked == true) { message = ExportCSVFile(cn, cmd, fileName, fieldListToReport); }
            //else // or xls file
            //{
            return ExportXLSFile(workbook, cn, cmd, fileName, fieldListToReport, ref message);
            //}
            //
            //return message;
        }
        private IWorkbook ExportXLSFile(IWorkbook workbook, SqlConnection cn, SqlCommand cmd, string fileName, List<string> fieldListToReport, ref string message)
        {
            FileName = fileName;
            //string message = "";
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
                    ValidClientLocationIDs = new AccessControlIDList(_UserName, "Client", ctx);
                    ValidProjectIDs = new AccessControlIDList(_UserName, "Project", ctx);
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


                //ExcelEngine excelEngine = new ExcelEngine();
                //IApplication application = excelEngine.Excel;
                //application.DefaultVersion = GetExcelVersion();
                FileName = fileName.Replace(".xls", "");
                FileName = FileName + "." + GetExcelExtension();
                //IWorkbook workbook = application.Workbooks.Create(1);
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
                return workbook;
                ////workbook.SaveAs(fileName, Page.Response, ExcelDownloadType.Open);
                //workbook.SaveAs(fileName, ExcelSaveType.SaveAsXLS, Response, ExcelDownloadType.Open);
                //workbook.Close();

                //excelEngine.Dispose();

                // move the file out to the browser.
            }
            catch (Exception ex)
            {
                message += "----------------------------------------" + Environment.NewLine;
                message += "Error:" + DateTime.Now.ToString() + Environment.NewLine;
                message += ex.Message;
                Message = message;
            }
            finally
            {
                cmd.Connection.Close();
                cn.Close();

            }
            return null;
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

    }
}