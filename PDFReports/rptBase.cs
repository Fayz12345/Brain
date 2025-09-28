using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using BW_WebApp.BarcodeUtils;
using BW_WebApp.DataManagers;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Barcode;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Tables;

namespace BW_WebApp.PDFReports
{
    public class rptBase
    {
        # region Private Members
        protected string UserName = "";
        //string ParamValue = "";
        //string _ESNValue = "";
        protected string _Message = "";
        protected string ReportType = "";
        protected string LogoPath = "";
        protected string ParamValue = "";
        protected string ParamField = "";

        string _ESNValue = "";

        //clsOrderHeader Data = null;
        protected PdfGraphics g;
        protected CompanyDemographics SiteDemographics = null;
        protected PdfDocument doc = null;
        protected PdfPage xPage = null;
        protected clsBarcodeUtils util = new clsBarcodeUtils();
        protected string TempFolder = "";
        protected string ReportTitle { get { return string.Format("{0} - {1}", SiteDemographics.Name, ReportType); } }
        public string Message { get { return _Message; } }

        public string DefaultFileName
        {
            get
            {
                string FileName = "";
                Regex containsABadCharacter = new Regex("[" + Regex.Escape(System.IO.Path.GetInvalidFileNameChars().ToString()) + "]");
                if (containsABadCharacter.IsMatch(ParamValue)) { FileName = ReportType; } else { FileName = string.Format("{0}{1}", ReportType, ParamValue); };
                return FileName;
            }
        }

        # endregion

        public rptBase(string paramField, string paramValue, string reporttype, string LogoLocation, string Username)
        {
            ParamField = paramField;
            ParamValue = paramValue;
            ReportType = reporttype;
            UserName = Username;
            LogoPath = LogoLocation;
            SetDefaults();
        }
        public rptBase(PdfDocument PDFDoc, string paramField, string paramValue, string reporttype, string LogoLocation, string Username)
        {
            doc = PDFDoc;
            UserName = Username;
            LogoPath = LogoLocation;
            ParamField = paramField;
            ParamValue = paramValue;
            SetDefaults();
        }

        void SetDefaults()
        {
            string TempDirectory = "~/IDAutomation";
            TempFolder = HttpContext.Current.Server.MapPath(TempDirectory);
            SiteDemographics = new CompanyDemographics(UserName);
            doc.DocumentInformation.Author = SiteDemographics.Name;
            doc.DocumentInformation.CreationDate = DateTime.Now;
            doc.DocumentInformation.Creator = SiteDemographics.SystemName;
            doc.DocumentInformation.Producer = SiteDemographics.SystemName;
            //doc.DocumentInformation.Keywords = string.Format("{0},{1},{2},{3},{4}", ReportType, ParamValue, UserName, rshipto, outboundwaybill);
            doc.DocumentInformation.Subject = string.Format("{0} ({1})", ReportType, ParamValue);
            doc.DocumentInformation.Title = ReportTitle;
        }

        #region Helper Functions & Events
        protected void PrintBarcodeDirectToPage(object sender, BeginCellLayoutEventArgs args)
        {
            int rowIndex = args.RowIndex;
            int cellIndex = args.CellIndex;
            if (rowIndex > -1 && cellIndex == 0) { _ESNValue = args.Value; }
            _ESNValue = _ESNValue ?? "";
            if (rowIndex > -1 && cellIndex == 1 && _ESNValue.Length > 0)   // This will print the ENS as a barcode.
            {
                try
                {
                    RectangleF area = args.Bounds;
                    area.Inflate(-2, -2);
                    PdfCode128ABarcode barcode = new PdfCode128ABarcode();
                    barcode.TextDisplayLocation = TextLocation.None;
                    barcode.Bounds = area;
                    barcode.BarHeight = area.Height;
                    barcode.Text = _ESNValue;
                    barcode.Draw(doc.Pages[doc.Pages.Count - 1], new PointF(area.X, area.Y));
                    //barcode.Draw(doc.Pages[doc.Pages.Count - 1], new PointF(area.X, area.Y));
                }
                catch (System.Exception a) { }
            }
        }
        protected void PrintBarcodeImage(object sender, BeginCellLayoutEventArgs args)
        {
            int rowIndex = args.RowIndex;
            int cellIndex = args.CellIndex;
            if (rowIndex > -1 && cellIndex == 0) { _ESNValue = args.Value; }
            _ESNValue = _ESNValue ?? "";
            if (rowIndex > -1 && cellIndex == 1 && _ESNValue.Length > 0)   // This will print the ENS as a barcode.
            {
                PdfGraphics g = args.Graphics;
                RectangleF area = args.Bounds;
                area.Inflate(-1, -1);
                try
                {
                    PdfImage img = Barcode(_ESNValue, area);
                    area.Width = Math.Min(area.Width, img.Width - 1);
                    g.DrawImage(img, area);

                    //PdfCode128ABarcode barcode = new PdfCode128ABarcode();
                    ////barcode.BarHeight = area.Height;
                    //barcode.Bounds = new RectangleF(area.X, area.Y, area.Width, area.Height);
                    //barcode.TextDisplayLocation = TextLocation.None;
                    //barcode.Text = _ESNValue;
                    //barcode.Draw(doc.Pages[doc.Pages.Count -1], new PointF(area.X, area.Y));
                }
                catch (System.Exception a) { }
            }
            // Now we need to highlight the 

        }
        protected PdfImage Barcode(string Value, RectangleF area)
        {

            Image i = util.SaveBarcodeToImage_Base(Value, .25f, Path.Combine(TempFolder, string.Format("{0}.jpg", Guid.NewGuid())));
            PdfImage img = new PdfBitmap(i);
            return img;
            //page.Graphics.DrawImage(img, new PointF(25, y));




            ////PdfCode39Barcode barcode = new PdfCode39Barcode();
            //PdfCode128ABarcode barcode = new PdfCode128ABarcode();
            //barcode.BarHeight = area.Height;
            //barcode.TextDisplayLocation = TextLocation.None;
            //barcode.Text = Value;
            //return new PdfBitmap(barcode.ToImage());
        }
        protected void pdfLightTable_BeginCellLayout(object sender, BeginCellLayoutEventArgs args)
        {
            int rowIndex = args.RowIndex;
            int cellIndex = args.CellIndex;
            if (rowIndex > -1 && cellIndex == 0) { _ESNValue = args.Value; }
            _ESNValue = _ESNValue ?? "";
            if (rowIndex > -1 && (cellIndex == 5 || cellIndex == 6) && _ESNValue.Length == 0)   // This will print the ENS as a barcode.
            {
                //args.
                //PdfGraphics g = args.Graphics;
                //RectangleF area = args.Bounds;
                //area.Inflate(-1, -1);
                //try { g.DrawImage(Barcode(_ESNValue, area), area); }
                //catch (System.Exception a) { }
                //args.Graphics.DrawEllipse(PdfBrushes.Red, args.Bounds);
                args.Graphics.DrawRectangle(new PdfSolidBrush(new PdfColor(ColorTranslator.FromHtml(SiteDemographics.BrandingBackgroundColour))), args.Bounds);
                //args.Graphics.DrawRectangle(new PdfRectangleArea(new PdfColor(ColorTranslator.FromHtml(SiteDemographics.BrandingBackgroundColour))), args.Bounds);
            }
        }
        protected void ResizeColumns(PdfLightTable pdfTable, DataTable dt, float pageWidth, string Colindex)
        {
            int colIdnex = 0;
            List<string> indxi = Colindex.Split(',').ToList();
            foreach (string idx in indxi)
            {
                if (int.TryParse(idx, out colIdnex) == true) { ResizeColumn(pdfTable, dt, pageWidth, colIdnex); }
            }

        }
        protected void ResizeColumn(PdfLightTable pdfTable, DataTable dt, float pageWidth, int colIndex)
        {

            if (colIndex >= dt.Columns.Count) { return; }

            // This method should resize one column in a table 
            // to the width of the longest string in the column.
            // In the example below, row2 of col4 contains the longest string
            // and col4 is resized such that the long string fits in the table cell without wrapping

            // |col0|col1|col2|col3|col4   |col5|
            // ----------------------------------
            // |abc |abcd|abcd|abcd|abc    |abcd| // row1 
            // |    |    |e   |ef  |       |efgh| //
            // ----------------------------------
            // |abc |abcd|abcd|abcd|abcdefg|abcd| // row2
            // |    |    |e   |ef  |       |efgh| //
            // ----------------------------------
            // |abc |abcd|abcd|abcd|abcde  |abcd| // row3
            // |    |    |e   |ef  |       |efgh| //

            float cellSpacing = pdfTable.Style.CellSpacing;
            float cellPadding = pdfTable.Style.CellPadding;
            float borderWidth = (pdfTable.Style.BorderPen != null ? pdfTable.Style.BorderPen.Width : 0);

            PdfFont df = pdfTable.Style.DefaultStyle.Font;
            PdfStringFormat dsf = pdfTable.Style.DefaultStyle.StringFormat;

            // find out the longest string
            float maxLength = 0;
            foreach (DataRow row in dt.Rows)
            {
                try
                {
                    string text = row[colIndex].ToString();
                    maxLength = Math.Max(maxLength, text.Length);
                }
                catch { }
            }

            pdfTable.Columns[colIndex].Width = maxLength;
        }
        protected void SetColumnSize(PdfLightTable pdfTable, string ColName, float Width)
        {
            foreach (PdfColumn c in pdfTable.Columns)
            {
                if (c.ColumnName.ToUpper() == ColName.ToUpper())
                {
                    c.Width = Width;
                    break;
                }
            }
        }
        protected void SetColumnSize(PdfLightTable pdfTable, int colIndex, float Width)
        {
            if (colIndex < pdfTable.Columns.Count) { pdfTable.Columns[colIndex].Width = Width; }
        }
        protected void SetColumnNames(PdfLightTable pdfTable, DataTable dt)
        {
            foreach (DataColumn col in dt.Columns)
            {
                pdfTable.Columns[col.Ordinal].ColumnName = dt.Columns[col.Ordinal].ColumnName;
            }
        }
        #endregion
    }
}