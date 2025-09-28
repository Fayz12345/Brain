using System;
using System.Data;
using System.Drawing;
using System.Linq;
using BW_WebApp.DataManagers;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Tables;



namespace BW_WebApp.PDFReports
{
    public class rptOrderInvoice_02 : rptBase
    {
        # region Private Members
        //string UserName = "";
        //string ParamField = "";
        //string ParamValue = "";
        //string _ESNValue = "";
        //string _Message = "";
        //string ReportType = "";
        //string LogoPath = "";

        clsOrderHeader Data = null;
        //PdfGraphics g;
        //CompanyDemographics SiteDemographics = null;
        //PdfDocument doc = null;
        //PdfPage xPage = null;
        //clsBarcodeUtils util = new clsBarcodeUtils();
        //string TempFolder = "";
        //PdfLoadedDocument doc;
        //PdfSignature signature;
        //PdfBitmap bmp;
        //PdfCertificate pdfCert;
        //Stream sfile1;
        //string ReportTitle { get { return string.Format("{0} - {1}", SiteDemographics.Name, ReportType); } }

        //public string DefaultFileName { 
        //    get {
        //        string FileName = "";
        //        Regex containsABadCharacter = new Regex("[" + Regex.Escape(System.IO.Path.GetInvalidFileNameChars().ToString()) + "]");
        //        if (containsABadCharacter.IsMatch(ParamValue)) { FileName = "DespatchReport"; } else { FileName = string.Format("DespatchReport{0}", ParamValue); };
        //        return FileName; 
        //    }
        //}
        public string GetCommandString { get { return string.Format("exec Report_D_DespatchNote '{0}','{1}'", ParamField, ParamValue); } }
        public string GetCommandStringAllAttributes { get { return string.Format("exec Report_D_DespatchNoteDataDump '{0}','{1}'", ParamField, ParamValue); } }
        //public string Message { get { return _Message; } }
        # endregion

        public rptOrderInvoice_02(string pField, string PackSlip, string LogoLocation, string Username)
            : base(pField, PackSlip, "OrderInvoice", LogoLocation, Username)
        {
            //doc = PDFDoc;
            ParamField = pField;
            ParamValue = PackSlip;
            UserName = Username;
            LogoPath = LogoLocation;
            //GenerateDESPATCH_NOTE();
        }
        public rptOrderInvoice_02(PdfDocument PDFDoc, string pField, string PackSlip, string LogoLocation, string Username)
            : base(PDFDoc, pField, PackSlip, "OrderInvoice", LogoLocation, Username)
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
            //string TempDirectory = "~/IDAutomation";
            //TempFolder = HttpContext.Current.Server.MapPath(TempDirectory);

            //ReportType = "Order Invoice";
            DateTime StartDate = DateTime.Now;
            _Message = string.Format("Report Started:{0}", StartDate);
            #region Source the data
            if (ParamValue.Length == 0) { ParamValue = "_x"; _Message = "Order Number set to _x"; }


            CustomReportManager crm = new CustomReportManager(UserName);
            //clsOrderHeader dta = new clsOrderHeader(UserName, ParamValue);
            Data = new clsOrderHeader(UserName, ParamValue);
            //List<Template_DespatchNote> dta = crm.GetDespatchNoteList(ParamField, ParamValue, ref _Message).OrderBy(x => x.Manufacturer).ThenBy(x => x.Model).ThenBy(x => x.Colour).ThenBy(x => x.Conditions).ToList();

            //clsOrderHeader dta = crm.GetDespatchNoteList(ParamField, ParamValue, ref _Message).OrderBy(x => x.Manufacturer).ThenBy(x => x.Model).ThenBy(x => x.Colour).ThenBy(x => x.Conditions).ToList();

            //if (dta == null || dta.Count == 0)
            //{
            //    //_Message = "No Data found";
            //    //return;
            //}

            string rshipto = "";
            string outboundwaybill = Data.WaybillNumber;
            //////////////////////////////////////////////////////////////////////////////////////<----
            //if (dta != null && dta.Count > 0)
            //{
            //    Template_DespatchNote r = dta.FirstOrDefault();
            //    rshipto = r.ShipTo ?? "";
            outboundwaybill = Data.WaybillNumber;
            //    outboundwaybill = r.Out_Bound_WayBill_S ?? "";
            //    //_Message = "No Data found";
            //    //return;
            //}

            #endregion
            #region start the document
            //doc.DocumentInformation.Author = SiteDemographics.Name;
            //doc.DocumentInformation.CreationDate = DateTime.Now;
            //doc.DocumentInformation.Creator = SiteDemographics.SystemName;
            //doc.DocumentInformation.Producer = SiteDemographics.SystemName;
            doc.DocumentInformation.Keywords = string.Format("{0},{1},{2},{3},{4}", ReportType, ParamValue, UserName, rshipto, outboundwaybill);
            //doc.DocumentInformation.Subject = string.Format("{0} ({1})", ReportType, ParamValue);
            //doc.DocumentInformation.Title = ReportTitle;
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
            this.AddHeader(doc, ParamValue, rshipto, outboundwaybill);
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
            dt = CreateDataTableDetail();
            pdfTable = CreatePdfTable(true);
            pdfTable.DataSource = dt;
            SetColumnNames(pdfTable, dt);

            SetColumnSize(pdfTable, "ProductID", 20);
            SetColumnSize(pdfTable, "Description", 100);
            SetColumnSize(pdfTable, "LK", 10);
            SetColumnSize(pdfTable, "Kit", 10);
            SetColumnSize(pdfTable, "QTY", 15);
            SetColumnSize(pdfTable, "UnitPrice", 20);
            SetColumnSize(pdfTable, "Discount", 20);
            SetColumnSize(pdfTable, "Price", 30);


            //Create a new string format

            PdfStringFormat format = new PdfStringFormat();
            //Set the text Alignment
            format.Alignment = PdfTextAlignment.Right;
            //Set the line Alignment
            format.LineAlignment = PdfVerticalAlignment.Middle;
            //Set right to left is true
            //Add the string format to PdfLightTable column

            pdfTable.Columns[4].StringFormat = format;
            pdfTable.Columns[5].StringFormat = format;
            pdfTable.Columns[6].StringFormat = format;
            pdfTable.Columns[7].StringFormat = format;


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
            //if (Printbarcode == true)
            //{
            //    pdfTable.BeginCellLayout += new BeginCellLayoutEventHandler(PrintBarcodeImage);
            //    //pdfTable.BeginCellLayout += new BeginCellLayoutEventHandler(PrintBarcodeDirectToPage);
            //    pdfTable.BeginCellLayout += pdfLightTable_BeginCellLayout;
            //    //pdfTable.EndCellLayout += pdfLightTable_EndCellLayout;
            //}
            return pdfTable;
        }
        private DataTable CreateDataTableDetail()
        {
            decimal QTYTotal = 0;
            decimal QTYGrandTotal = 0;
            decimal PriceTotal = 0;
            decimal DiscountTotal = 0;
            decimal TotalTax = 0;
            decimal GrandTotal = 0;
            DataTable stable = new DataTable();
            stable.TableName = "OrderInvoice";
            stable.Columns.Add("ProductID");
            stable.Columns.Add("Description");
            stable.Columns.Add("LK");
            stable.Columns.Add("Kit");
            stable.Columns.Add("QTY");
            //stable.Columns.Add(col);
            stable.Columns.Add("UnitPrice");
            //stable.Columns.Add("Condition");
            stable.Columns.Add("Discount");
            stable.Columns.Add("Price");
            //Include rows to the DataTable.
            string key = "";
            int freq = 0;
            int Totfreq = 0;
            //foreach (var x in dta.OrderBy(y => y.Manufacturer).ThenBy(y => y.Model).ThenBy(y => y.Colour).ThenBy(y => y.Conditions))
            //{
            //    if (key.Length > 0 && key != string.Format("{0}{1}{2}{3}", x.Manufacturer, x.Model, x.Colour, x.Conditions))
            foreach (var x in Data.OrderDetailLines.OrderBy(y => y.Desc_Text).ThenBy(y => y.Lock).ThenBy(y => y.Kit))
            {
                if (key.Length > 0 && key != string.Format("{0}{1}{2}", x.Desc_Text, x.Lock, x.Kit))
                {
                    //stable.Rows.Add(new string[] { "", "", "", "", "", "", "Count:", QTYTotal.ToString() });
                    freq = 0;
                    QTYTotal = 0;

                }
                freq += 1;
                Totfreq += 1;
                QTYTotal += x.QTY;
                QTYGrandTotal += x.QTY;
                PriceTotal += x.Total;
                DiscountTotal += x.DiscountAmount;
                //key = string.Format("{0}{1}{2}{3}", x.Manufacturer, x.Model, x.Colour, x.Conditions);
                //stable.Rows.Add(new string[] { x.ESN, "", x.Manufacturer, x.Model, x.Colour, x.Conditions, "1" });
                key = string.Format("{0}{1}{2}", x.Desc_Text, x.Lock, x.Kit);
                stable.Rows.Add(new string[] { "Devices", x.Desc_Text, x.Lock, x.Kit, string.Format("{0:N0}", x.QTY), string.Format("{0:N2}", x.UnitPrice), string.Format("{0:N6}", x.Discount * 100) + "%", string.Format("{0:N2}", x.Total) });
            }


            stable.Rows.Add(new string[] { "", "", "", "Qty Sub", string.Format("{0:N0}", QTYTotal), "", "Subtotal $", string.Format("{0:N2}", PriceTotal) });
            stable.Rows.Add(new string[] { "", "", "", "", "", "", "Freight", string.Format("{0:N2}", Data.Freight) });
            TotalTax = (PriceTotal + Data.Freight) * Data.TaxRate;
            stable.Rows.Add(new string[] { "", "", "", "Tax Rate", string.Format("{0:N2}", Data.TaxRate * 100) + "%", "", "Tax", string.Format("{0:N2}", TotalTax) });
            GrandTotal = PriceTotal + Data.Freight + TotalTax;
            stable.Rows.Add(new string[] { "", "", "", "", "", "", "Invoice Total", string.Format("{0:N2}", GrandTotal) });
            stable.Rows.Add(new string[] { "", "", "", "", "", "", "Currency", Data.Currency });
            return stable;
        }
        //private DataTable CreateDataTableSummary(clsOrderHeader dta)
        //{

        //    var query = (from t in dta
        //                 group t by new { t.Manufacturer, t.Model, t.Colour }
        //                 //group t by new { t.Manufacturer, t.Model, t.Colour, t.Conditions }
        //                     into grp
        //                     select new
        //                     {
        //                         grp.Key.Manufacturer,
        //                         grp.Key.Model,
        //                         grp.Key.Colour,
        //                         //grp.Key.Conditions,
        //                         Quantity = grp.Sum(t => t.Freq)
        //                     }).ToList();



        //    DataTable stable = new DataTable();
        //    stable.TableName = "Summary";
        //    //stable.Columns.Add("ESN");
        //    //stable.Columns.Add(".");
        //    stable.Columns.Add("Manufacturer");
        //    stable.Columns.Add("Model");
        //    //stable.Columns.Add(col);
        //    stable.Columns.Add("Colour");
        //    //stable.Columns.Add("Condition");
        //    stable.Columns.Add("QTY");
        //    //Include rows to the DataTable.
        //    //string key = "";
        //    //int freq = 0;
        //    int Totfreq = 0;
        //    int GrandTotal = 0;
        //    GrandTotal = query.Sum(x => x.Quantity ?? 0);
        //    stable.Rows.Add(new string[] { "", "", "", "" });
        //    stable.Rows.Add(new string[] { "", "", "Total Devices", GrandTotal.ToString() });
        //    stable.Rows.Add(new string[] { "", "", "", "" });
        //    stable.Rows.Add(new string[] { "Summary", "", "", "" });

        //    foreach (var x in query.OrderBy(y => y.Manufacturer).ThenBy(y => y.Model).ThenBy(y => y.Colour))
        //    {
        //        //if (key.Length > 0 && key != string.Format("{0}{1}{2}{3}", x.Manufacturer, x.Model, x.Colour, x.Conditions))
        //        //{
        //        //    stable.Rows.Add(new string[] { "", "", "", "", "", "Count:", freq.ToString() });
        //        //    freq = 0;
        //        //}
        //        //freq += 1;
        //        Totfreq += x.Quantity ?? 0;
        //        //key = string.Format("{0}{1}{2}{3}", x.Manufacturer, x.Model, x.Colour, x.Conditions);
        //        stable.Rows.Add(new string[] { x.Manufacturer, x.Model, x.Colour, x.Quantity.ToString() });
        //    }
        //    //stable.Rows.Add(new string[] { "", "", "", "", "", "Count", freq.ToString() });
        //    stable.Rows.Add(new string[] { "", "", "Total", Totfreq.ToString() });
        //    return stable;
        //}
        #endregion
        #region Header/Footer
        private void AddHeader(PdfDocument doc, string PackList, string ShipTo, string Waybill)
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
    }
}