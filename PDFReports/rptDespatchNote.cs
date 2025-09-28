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
    public class rptDespatchNote
    {
        # region Private Members
        string UserName = "";
        string ParamField = "";
        string ParamValue = "";
        string _ESNValue = "";
        string _Message = "";
        string ReportType = "";
        string LogoPath = "";
        List<decimal> RDIDList = new List<decimal>();
        PdfGraphics g;
        CompanyDemographics SiteDemographics = null;
        PdfDocument doc = null;
        PdfPage xPage = null;
        clsBarcodeUtils util = new clsBarcodeUtils();
        string TempFolder = "";
        //PdfLoadedDocument doc;
        //PdfSignature signature;
        //PdfBitmap bmp;
        //PdfCertificate pdfCert;
        //Stream sfile1;
        string ReportTitle { get { return string.Format("{0} - {1}", SiteDemographics.Name, ReportType); } }
        public string DefaultFileName
        {
            get
            {
                string FileName = "";
                Regex containsABadCharacter = new Regex("[" + Regex.Escape(System.IO.Path.GetInvalidFileNameChars().ToString()) + "]");
                if (containsABadCharacter.IsMatch(ParamValue)) { FileName = "DispatchReport"; } else { FileName = string.Format("DispatchReport{0}", ParamValue); };
                return FileName;
            }
        }
        public string GetCommandString { get { return string.Format("exec Report_D_DespatchNote '{0}','{1}'", ParamField, ParamValue); } }
        public string GetCommandStringAllAttributes { get { return string.Format("exec Report_D_DespatchNoteDataDump '{0}','{1}'", ParamField, ParamValue); } }
        public string Message { get { return _Message; } }
        # endregion

        public rptDespatchNote(string pField, string PackSlip, string LogoLocation, string Username)
        {
            //doc = PDFDoc;
            ParamField = pField;
            ParamValue = PackSlip;
            UserName = Username;
            LogoPath = LogoLocation;
            //GenerateDESPATCH_NOTE();
        }
        public rptDespatchNote(PdfDocument PDFDoc, string pField, string PackSlip, string LogoLocation, string Username)
        {
            doc = PDFDoc;
            ParamField = pField;
            ParamValue = PackSlip;
            UserName = Username;
            LogoPath = LogoLocation;
            GenerateReport();
        }

        private void GenerateReport()
        {
            //string TempDirectory = System.Configuration.ConfigurationManager.AppSettings["TempDirectory"];
            //if (TempDirectory == null || TempDirectory.Length == 0)
            //{
            //    TempDirectory = "~/IDAutomation";
            //}
            string TempDirectory = "~/IDAutomation";
            TempFolder = HttpContext.Current.Server.MapPath(TempDirectory);

            ReportType = "Dispatch Note";
            DateTime StartDate = DateTime.Now;
            _Message = string.Format("Report Started:{0}", StartDate);
            ParamValue = ParamValue ?? "";
            #region Source the data
            SiteDemographics = new CompanyDemographics(UserName);
            if (ParamValue.Length == 0) { ParamValue = "_x"; _Message = "Packing slip set to _x"; }
            CustomReportManager crm = new CustomReportManager(UserName);
            List<Template_DespatchNote> dta = crm.GetDespatchNoteList(ParamField, ParamValue, ref _Message).OrderBy(x => x.Manufacturer).ThenBy(x => x.Model).ThenBy(x => x.Colour).ThenBy(x => x.Conditions).ToList();
            //List<Template_DespatchNote> dta = crm.GetDespatchNoteList(PSlip, ref _Message).Take(265).OrderBy(x => x.Manufacturer).ThenBy(x => x.Model).ThenBy(x => x.Colour).ThenBy(x => x.Conditions).ToList();

            if (dta == null || dta.Count == 0)
            {
                //_Message = "No Data found";
                //return;
            }

            string rshipto = "";
            string outboundwaybill = "";
            string PONumber = "";
            if (dta != null && dta.Count > 0)
            {
                Template_DespatchNote r = dta.FirstOrDefault();
                rshipto = r.ShipTo ?? "";
                outboundwaybill = r.Out_Bound_WayBill_S ?? "XXwwaayybbiillXX";
                //_Message = "No Data found";
                //return;
            }

            #endregion
            #region start the document
            doc.DocumentInformation.Author = SiteDemographics.Name;
            doc.DocumentInformation.CreationDate = DateTime.Now;
            doc.DocumentInformation.Creator = SiteDemographics.SystemName;
            doc.DocumentInformation.Producer = SiteDemographics.SystemName;
            doc.DocumentInformation.Keywords = string.Format("{0},{1},{2},{3},{4}", ReportType, ParamValue, UserName, rshipto, outboundwaybill);
            doc.DocumentInformation.Subject = string.Format("{0} ({1})", ReportType, ParamValue);
            doc.DocumentInformation.Title = ReportTitle;
            PdfPage page = null;
            PdfLightTableLayoutResult tableResult = null;
            DataTable dt = null;
            PdfLightTable pdfTable = null;
            //PdfGraphics graphics = null;
            doc.PageSettings.Margins.All = 10;
            doc.PageSettings.Margins.Bottom = 40;
            doc.PageSettings.Margins.Top = 40;
            //doc.PageSettings.Margins.Right = 3f;
            page = doc.Pages.Add();
            //xPage = page;
            #endregion

            //Adding Header
            dt = CreateDataTableDetail(dta);             // This needs to happen before the AddHeader cause the records need to be Known.
            PONumber = crm.GetPO_NoList(RDIDList);
            this.AddHeader(doc, ParamValue, rshipto, outboundwaybill, PONumber);
            //Adding Footer
            this.AddFooter(doc, "");

            #region Summary Section
            //dt = CreateDataTableSummary(dta);
            //pdfTable = CreatePdfTable(false);
            //pdfTable.DataSource = dt;
            //ResizeColumns(pdfTable, dt, page.GetClientSize().Width, "0,1,2,4");
            //pdfTable.Style.ShowHeader = true;
            //tableResult = (PdfLightTableLayoutResult)pdfTable.Draw(page, new PointF(0, 0));
            #endregion

            #region Detail Section
            //page = doc.Pages.Add();                    // Required if Summary Section above is printed.
            pdfTable = CreatePdfTable(true);
            pdfTable.DataSource = dt;
            SetColumnNames(pdfTable, dt);
            SetColumnSize(pdfTable, "ESN", 45);
            SetColumnSize(pdfTable, ".", 100);
            SetColumnSize(pdfTable, "Manufacturer", 50);
            SetColumnSize(pdfTable, "Model", 50);
            SetColumnSize(pdfTable, "Colour", 20);
            //SetColumnSize(pdfTable, "Condition", 50);
            SetColumnSize(pdfTable, "QTY", 20);

            //ResizeColumns(pdfTable, dt, page.GetClientSize().Width, "2,3,4,5");
            tableResult = (PdfLightTableLayoutResult)pdfTable.Draw(page, new PointF(0, 0));
            #endregion

            DateTime EndDate = DateTime.Now;
            TimeSpan span = new TimeSpan();
            span = EndDate - StartDate;
            _Message = string.Format("Report Finished {0}ms", (decimal)span.TotalMilliseconds);
        }
        #region GetTables


        private PdfLightTable CreatePdfTable(bool Printbarcode)
        {
            PdfPen borderPen = new PdfPen(Color.AntiqueWhite);
            //PdfPen borderPen = new PdfPen(Color.Transparent);
            borderPen.Width = 0.25f;
            PdfPen transPen = new PdfPen(PdfBrushes.Transparent);
            PdfFont normalFont = new PdfStandardFont(PdfFontFamily.Helvetica, 8);
            PdfFont normalBoldFont = new PdfStandardFont(PdfFontFamily.Helvetica, 8, PdfFontStyle.Bold);

            PdfCellStyle headerRowCellStyle = new PdfCellStyle();
            headerRowCellStyle.Font = normalBoldFont;
            headerRowCellStyle.BorderPen = transPen;
            headerRowCellStyle.BackgroundBrush = new PdfSolidBrush(new PdfColor(ColorTranslator.FromHtml(SiteDemographics.BrandingBackgroundColour)));

            PdfCellStyle defaultCellStyle = new PdfCellStyle();
            defaultCellStyle.Font = normalFont;
            defaultCellStyle.BorderPen = borderPen;                    //transPen;

            //PdfCellStyle altRowCellStyle = new PdfCellStyle();
            //altRowCellStyle.Font = normalFont;
            //altRowCellStyle.BorderPen = transPen;
            //altRowCellStyle.StringFormat = new PdfStringFormat();
            //altRowCellStyle.BackgroundBrush = new PdfSolidBrush(new PdfColor(ColorTranslator.FromHtml(SiteDemographics.BrandingBackgroundColour)));

            PdfLightTable pdfTable = new PdfLightTable();
            //pdfTable.Style.AlternateStyle = altRowCellStyle;
            pdfTable.Style.DefaultStyle = defaultCellStyle;
            pdfTable.Style.HeaderSource = PdfHeaderSource.ColumnCaptions;
            pdfTable.Style.HeaderStyle = headerRowCellStyle;
            pdfTable.Style.RepeatHeader = true;
            pdfTable.Style.ShowHeader = true;
            pdfTable.Style.CellPadding = 2;
            if (Printbarcode == true)
            {
                pdfTable.BeginCellLayout += new BeginCellLayoutEventHandler(PrintBarcodeImage);
                //pdfTable.BeginCellLayout += new BeginCellLayoutEventHandler(PrintBarcodeDirectToPage);
                pdfTable.BeginCellLayout += pdfLightTable_BeginCellLayout;
                //pdfTable.EndCellLayout += pdfLightTable_EndCellLayout;
            }
            return pdfTable;
        }
        private DataTable CreateDataTableDetail(List<Template_DespatchNote> dta)
        {
            DataTable stable = new DataTable();
            stable.TableName = "Detail";
            stable.Columns.Add("ESN");
            stable.Columns.Add(".");
            stable.Columns.Add("Manufacturer");
            stable.Columns.Add("Model");
            //stable.Columns.Add(col);
            stable.Columns.Add("Colour");
            //stable.Columns.Add("Condition");
            stable.Columns.Add("QTY");
            //Include rows to the DataTable.
            string key = "";
            int freq = 0;
            int Totfreq = 0;
            RDIDList = new List<decimal>();
            //foreach (var x in dta.OrderBy(y => y.Manufacturer).ThenBy(y => y.Model).ThenBy(y => y.Colour).ThenBy(y => y.Conditions))
            //{
            //    if (key.Length > 0 && key != string.Format("{0}{1}{2}{3}", x.Manufacturer, x.Model, x.Colour, x.Conditions))
            foreach (var x in dta.OrderBy(y => y.Manufacturer).ThenBy(y => y.Model).ThenBy(y => y.Colour))
            {
                if (key.Length > 0 && key != string.Format("{0}{1}{2}", x.Manufacturer, x.Model, x.Colour))
                {
                    stable.Rows.Add(new string[] { "", "", "", "", "Count:", freq.ToString() });
                    freq = 0;
                }
                freq += 1;
                Totfreq += 1;
                //key = string.Format("{0}{1}{2}{3}", x.Manufacturer, x.Model, x.Colour, x.Conditions);
                //stable.Rows.Add(new string[] { x.ESN, "", x.Manufacturer, x.Model, x.Colour, x.Conditions, "1" });
                key = string.Format("{0}{1}{2}", x.Manufacturer, x.Model, x.Colour);
                stable.Rows.Add(new string[] { x.ESN, "", x.Manufacturer, x.Model, x.Colour, "1" });
                RDIDList.Add(x.ReceiveDetailID);
            }
            stable.Rows.Add(new string[] { "", "", "", "", "Count", freq.ToString() });
            stable.Rows.Add(new string[] { "", "", "", "", "Total", Totfreq.ToString() });
            return stable;
        }
        private DataTable CreateDataTableSummary(List<Template_DespatchNote> dta)
        {

            var query = (from t in dta
                         group t by new { t.Manufacturer, t.Model, t.Colour }
                         //group t by new { t.Manufacturer, t.Model, t.Colour, t.Conditions }
                             into grp
                         select new
                         {
                             grp.Key.Manufacturer,
                             grp.Key.Model,
                             grp.Key.Colour,
                             //grp.Key.Conditions,
                             Quantity = grp.Sum(t => t.Freq)
                         }).ToList();



            DataTable stable = new DataTable();
            stable.TableName = "Summary";
            //stable.Columns.Add("ESN");
            //stable.Columns.Add(".");
            stable.Columns.Add("Manufacturer");
            stable.Columns.Add("Model");
            //stable.Columns.Add(col);
            stable.Columns.Add("Colour");
            //stable.Columns.Add("Condition");
            stable.Columns.Add("QTY");
            //Include rows to the DataTable.
            //string key = "";
            //int freq = 0;
            int Totfreq = 0;
            int GrandTotal = 0;
            GrandTotal = query.Sum(x => x.Quantity ?? 0);
            stable.Rows.Add(new string[] { "", "", "", "" });
            stable.Rows.Add(new string[] { "", "", "Total Devices", GrandTotal.ToString() });
            stable.Rows.Add(new string[] { "", "", "", "" });
            stable.Rows.Add(new string[] { "Summary", "", "", "" });

            foreach (var x in query.OrderBy(y => y.Manufacturer).ThenBy(y => y.Model).ThenBy(y => y.Colour))
            {
                //if (key.Length > 0 && key != string.Format("{0}{1}{2}{3}", x.Manufacturer, x.Model, x.Colour, x.Conditions))
                //{
                //    stable.Rows.Add(new string[] { "", "", "", "", "", "Count:", freq.ToString() });
                //    freq = 0;
                //}
                //freq += 1;
                Totfreq += x.Quantity ?? 0;
                //key = string.Format("{0}{1}{2}{3}", x.Manufacturer, x.Model, x.Colour, x.Conditions);
                stable.Rows.Add(new string[] { x.Manufacturer, x.Model, x.Colour, x.Quantity.ToString() });
            }
            //stable.Rows.Add(new string[] { "", "", "", "", "", "Count", freq.ToString() });
            stable.Rows.Add(new string[] { "", "", "Total", Totfreq.ToString() });
            return stable;
        }
        #endregion
        #region Header/Footer
        private void AddHeader(PdfDocument doc, string PackList, string ShipTo, string Waybill, string PONumber)
        {
            RectangleF rect = new RectangleF(0, 0, doc.Pages[0].GetClientSize().Width, 70);

            //Create page template
            PdfPageTemplateElement header = new PdfPageTemplateElement(rect);
            PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 24);
            float doubleHeight = font.Height * 2;
            Color activeColor = Color.FromArgb(44, 71, 120);
            SizeF imageSize = new SizeF(110f, 35f);
            if (LogoPath.Length > 0)
            {
                //string path = Server.MapPath("~/Styles/images/logo.jpg");
                PdfImage img = new PdfBitmap(LogoPath);
                header.Graphics.DrawImage(img, new PointF(header.Width - imageSize.Width, 5), new SizeF(imageSize.Width, imageSize.Height));
            }
            float HeaderTopStart = 0f;
            PointF location = new PointF(0f, 0f);
            PdfBrush brush = new PdfSolidBrush(Color.Black);
            font = new PdfStandardFont(PdfFontFamily.Helvetica, 12, PdfFontStyle.Bold);
            header.Graphics.DrawString(ReportTitle, font, brush, 0, HeaderTopStart + 0);
            font = new PdfStandardFont(PdfFontFamily.Helvetica, 9);
            header.Graphics.DrawString(SiteDemographics.CompanyNameAndAddress, font, brush, new RectangleF(0, HeaderTopStart + 15, header.Width / 2, header.Height));
            header.Graphics.DrawString("Ship To:   " + ShipTo, font, brush, new PointF((header.Width / 4), HeaderTopStart + 40));


            if (PONumber.Length > 0 && PONumber.Length <= 40) { header.Graphics.DrawString(string.Format("PO: {0}", PONumber), font, brush, new PointF((header.Width / 2), HeaderTopStart + 40)); }
            if (PONumber.Length > 40) { header.Graphics.DrawString(string.Format("PO: {0}...", PONumber.Substring(0, 37)), font, brush, new PointF((header.Width / 2), HeaderTopStart + 40)); }



            if (ParamField.ToUpper() == "ORDER")
            {
                header.Graphics.DrawString("Order Number: " + PackList, font, brush, new PointF((header.Width / 4), HeaderTopStart + 55));
            }
            else
            {
                header.Graphics.DrawString("Pack List: " + PackList, font, brush, new PointF((header.Width / 4), HeaderTopStart + 55));
            }

            header.Graphics.DrawString("WayBill: " + Waybill, font, brush, new PointF((header.Width / 2), HeaderTopStart + 55));

            string text = String.Format("{0:MM/dd/yyyy HH:mm}", doc.DocumentInformation.CreationDate);
            header.Graphics.DrawString(text, font, brush, new PointF(header.Width - 70, HeaderTopStart + 55));
            #region REM
            //maxLength = Math.Max(15, text.Length);

            //PdfSolidBrush brush = new PdfSolidBrush(activeColor);

            //font = new PdfStandardFont(PdfFontFamily.Helvetica, 16, PdfFontStyle.Bold);

            ////Set formattings for the text
            //PdfStringFormat format = new PdfStringFormat();
            //format.Alignment = PdfTextAlignment.Center;
            //format.LineAlignment = PdfVerticalAlignment.Middle;

            ////Draw title
            //header.Graphics.DrawString(title, font, brush, new RectangleF(0, 0, header.Width, header.Height), format);






            //brush = new PdfSolidBrush(Color.Gray);
            //font = new PdfStandardFont(PdfFontFamily.Helvetica, 6, PdfFontStyle.Bold);

            //format = new PdfStringFormat();
            //format.Alignment = PdfTextAlignment.Left;
            //format.LineAlignment = PdfVerticalAlignment.Bottom;

            ////Draw description
            //header.Graphics.DrawString(description, font, brush, new RectangleF(0, 0, header.Width, header.Height - 8), format);


            //PdfColor(ColorTranslator.FromHtml(SiteDemographics.BrandingBackgroundColour))
            #endregion

            //Draw some lines in the header
            //PdfPen pen = new PdfPen(ColorTranslator.FromHtml(SiteDemographics.BrandingBackgroundColour), 3f);
            ////pen = new PdfPen(ColorTranslator.FromHtml(SiteDemographics.BrandingBackgroundColour), 0.7f);
            ////header.Graphics.DrawLine(pen, 0, 0, header.Width, 0);
            ////pen = new PdfPen(ColorTranslator.FromHtml(SiteDemographics.BrandingBackgroundColour), 2f);
            ////header.Graphics.DrawLine(pen, 0, 03, header.Width + 3, 03);
            //pen = new PdfPen(ColorTranslator.FromHtml(SiteDemographics.BrandingBackgroundColour), 2f);
            //header.Graphics.DrawLine(pen, 0, header.Height - 3, header.Width, header.Height - 3);
            //header.Graphics.DrawLine(pen, 0, header.Height, header.Width, header.Height);

            //Add header template at the top.
            doc.Template.Top = header;
        }
        private void AddFooter(PdfDocument doc, string footerText)
        {
            RectangleF rect = new RectangleF(0, 0, doc.Pages[0].GetClientSize().Width, 20);

            //Create a page template
            PdfPageTemplateElement footer = new PdfPageTemplateElement(rect);
            PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 8);

            PdfSolidBrush brush = new PdfSolidBrush(Color.Gray);

            PdfPen pen = new PdfPen(Color.DarkBlue, 3f);
            font = new PdfStandardFont(PdfFontFamily.Helvetica, 7, PdfFontStyle.Bold);
            PdfStringFormat format = new PdfStringFormat();
            format.Alignment = PdfTextAlignment.Center;
            format.LineAlignment = PdfVerticalAlignment.Middle;
            footer.Graphics.DrawString(footerText, font, brush, new RectangleF(0, 18, footer.Width, footer.Height), format);

            format = new PdfStringFormat();
            format.Alignment = PdfTextAlignment.Right;
            format.LineAlignment = PdfVerticalAlignment.Bottom;

            //Create page number field
            PdfPageNumberField pageNumber = new PdfPageNumberField(font, brush);

            //Create page count field
            PdfPageCountField count = new PdfPageCountField(font, brush);

            //Create author field
            PdfDocumentAuthorField authorField = new PdfDocumentAuthorField(font, brush);

            //string pagenum = string.Format("{0} of {1}", pageNumber, count);
            //footer.Graphics.DrawString(pagenum, font, brush, new RectangleF(0, 0, footer.Width - 8, footer.Height - 8), format);




            authorField.Draw(footer.Graphics, new PointF(0, footer.Height - 8));
            //footer.Graphics.DrawString("Page of", font, brush, new RectangleF(0, footer.Height - 8, footer.Width - 25, footer.Height - 8), format);
            pageNumber.Draw(footer.Graphics, new PointF(footer.Width - 35, footer.Height - 8));
            footer.Graphics.DrawString("of", font, brush, new PointF(footer.Width - 18, footer.Height - 8));

            count.Draw(footer.Graphics, new PointF(footer.Width - 8, footer.Height - 8));


            //Draw current page number
            //pageNumber.Draw(footer.Graphics, new PointF(496, 35));



            //Draw number of pages
            //count.Draw(footer.Graphics, new PointF(510, 35));

            //Draw number of pages

            //Add the footer template at the bottom
            doc.Template.Bottom = footer;
        }
        #endregion
        #region Helper Functions & Events
        void PrintBarcodeDirectToPage(object sender, BeginCellLayoutEventArgs args)
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
        void PrintBarcodeImage(object sender, BeginCellLayoutEventArgs args)
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
        private PdfImage Barcode(string Value, RectangleF area)
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
        //private void pdfLightTable_EndCellLayout(object sender, EndCellLayoutEventArgs args)
        //{

        //    if (args.RowIndex == 0 && args.CellIndex == 0)
        //    {

        //        args.Graphics.DrawImage(new PdfBitmap(imageFileName), args.Bounds);

        //    }

        //}
        private void pdfLightTable_BeginCellLayout(object sender, BeginCellLayoutEventArgs args)
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




        private void ResizeColumns(PdfLightTable pdfTable, DataTable dt, float pageWidth, string Colindex)
        {
            int colIdnex = 0;
            List<string> indxi = Colindex.Split(',').ToList();
            foreach (string idx in indxi)
            {
                if (int.TryParse(idx, out colIdnex) == true) { ResizeColumn(pdfTable, dt, pageWidth, colIdnex); }
            }

        }
        private void ResizeColumn(PdfLightTable pdfTable, DataTable dt, float pageWidth, int colIndex)
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
        private void SetColumnSize(PdfLightTable pdfTable, string ColName, float Width)
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
        private void SetColumnSize(PdfLightTable pdfTable, int colIndex, float Width)
        {
            if (colIndex < pdfTable.Columns.Count) { pdfTable.Columns[colIndex].Width = Width; }
        }
        private void SetColumnNames(PdfLightTable pdfTable, DataTable dt)
        {
            foreach (DataColumn col in dt.Columns)
            {
                pdfTable.Columns[col.Ordinal].ColumnName = dt.Columns[col.Ordinal].ColumnName;
            }
        }
        #endregion
    }
}