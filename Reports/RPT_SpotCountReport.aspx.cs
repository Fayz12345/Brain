using System;
using System.Collections.Generic;
using System.Web.UI;
//using Factory_DataModel;
using BW_WebApp.DataManagers;

// using DAL;

using Syncfusion.XlsIO;

namespace BW_WebApp.Reports
{
    public partial class RPT_SpotCountReport : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            string BinNumber = Request.QueryString.Get("Bin");
            if (BinNumber == null || BinNumber.Length == 0)
            {
                BinNumber = "10";
                // return;
            }
            //lblBinNumber.Text = "Bin Number:" + BinNumber;
            ExportSpotCountReportToExcel(BinNumber);

            //lblRunDate.Text = "Date:" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
            //ReceiveDetailManager rdm = new ReceiveDetailManager(User.Identity.Name);
            //List<SpotCountReport> Spot = rdm.GetSpotCountReport(BinNumber);
            //BinData.DataSource = Spot;
            //BinData.DataBind();
        }


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
            sheet.Range[Row, Col].Text = "Make";
            sheet.Range[Row, Col + 2].Text = "Model";
            sheet.Range[Row, Col + 4].Text = "Colour";
            sheet.Range[Row, Col].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignRight;
            sheet.Range[Row, Col + 2].HorizontalAlignment = ExcelHAlign.HAlignRight;
            sheet.Range[Row, Col + 4].HorizontalAlignment = ExcelHAlign.HAlignRight;
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
            sheet.Range[Row, Col + 5].Text = sr.QCed.ToString();
            sheet.Range[Row, Col + 1].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignLeft;
            sheet.Range[Row, Col + 5].HorizontalAlignment = ExcelHAlign.HAlignLeft;

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

            Row++;
            sheet.Range[Row, Col].Text = "DOA";
            sheet.Range[Row, Col + 2].Text = "NFF";
            sheet.Range[Row, Col + 4].Text = "Customer Abused";

            sheet.Range[Row, Col].HorizontalAlignment = ExcelHAlign.HAlignRight;
            sheet.Range[Row, Col + 2].HorizontalAlignment = ExcelHAlign.HAlignRight;
            sheet.Range[Row, Col + 4].HorizontalAlignment = ExcelHAlign.HAlignRight;

            sheet.Range[Row, Col + 1].Text = sr.DOA.ToString();
            sheet.Range[Row, Col + 3].Text = sr.NFF.ToString();
            sheet.Range[Row, Col + 5].Text = sr.CustAbuse.ToString();

            sheet.Range[Row, Col + 1].HorizontalAlignment = ExcelHAlign.HAlignLeft;
            sheet.Range[Row, Col + 3].HorizontalAlignment = ExcelHAlign.HAlignLeft;
            sheet.Range[Row, Col + 5].HorizontalAlignment = ExcelHAlign.HAlignLeft;
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
        private void SetExcelRange(IWorksheet sheet, ref int Row1, ref int Col1, int row2, int col2, string Text)
        {
            sheet.Range[Row1, Col1, row2, col2].Text = Text;
            sheet.MigrantRange[Row1, Col1, row2, col2].Merge();
            //sheet.MigrantRange[Row1, Col1, row2, col2].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
            //sheet.Range[Row1, Col1, row2, col2].CellStyle.Color = BackColour;
            //sheet.Range[Row1, Col1, row2, col2].CellStyle.Color = BackColour;
        }
        private void SetExcelRangeBackColour(IWorksheet sheet, ref int Row1, ref int Col1, int row2, int col2, System.Drawing.Color BackColour, ExcelHAlign Alignment)
        {
            sheet.MigrantRange[Row1, Col1, row2, col2].CellStyle.HorizontalAlignment = Alignment;
            sheet.Range[Row1, Col1, row2, col2].CellStyle.Color = BackColour;
            //sheet.Range[Row1, Col1, row2, col2].CellStyle.Color = BackColour;
        }

        #endregion





    }
}