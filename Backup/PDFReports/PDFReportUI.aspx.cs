using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Web.Security;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Barcode;
using Syncfusion.Pdf.Security;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using Syncfusion.Pdf.Tables;
using Syncfusion.Pdf.Parsing;
using Syncfusion.XlsIO;

using BW_WebApp.DataManagers;

namespace BW_WebApp.PDFReports
{
    public partial class PDFReportUI : System.Web.UI.Page
    {
        # region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            btnDespatch.Click += new System.EventHandler(btnDespatch_Click);
            btnOrderInvoice.Click += new EventHandler(btnOrderInvoice_Click);
            btnDespatchExcel.Click += new System.EventHandler(btnDespatchExcel_Click);
            btnDespatchExcelAll.Click += new EventHandler(btnDespatchExcelAll_Click);

            btnBarcodeSample.Click += new EventHandler(btnBarcodeSample_Click);

            btnAnalyzePDF.Click += new EventHandler(btnAnalyzePDF_Click);
            btnAnalyzeEXCEL.Click += new EventHandler(btnAnalyzeEXCEL_Click);
            if (IsPostBack == false)
            {
                btnBarcodeSample.Visible = false;
                TabAdminRpt.Visible = false;
                if (User.Identity.Name.ToUpper() == "JMCCOMB")
                {
                    TabAdminRpt.Visible = true;
                }
            }
        }

        void btnOrderInvoice_Click(object sender, EventArgs e)
        {
            string PSlip = txtPSlip.Text.Trim();         //  "AS098";
            if (PSlip.Length == 0) { lblResults.Text = "Packing slip/Order Number must contain a value"; return; }
            PdfDocument doc = new PdfDocument();
            rptOrderInvoice_02 rpt = new rptOrderInvoice_02(doc, rdlReportParameter.SelectedItem.Value, PSlip, LogoPath(), User.Identity.Name);
            string Message = rpt.Message;
            lblResults.Text = Message;
            doc.Save(string.Format("{0}.pdf", rpt.DefaultFileName), Response, HttpReadType.Save);
        }


        #endregion
        # region PDF
        protected void btnDespatch_Click(object sender, EventArgs e)
        {
            string PSlip = txtPSlip.Text.Trim();         //  "AS098";
            if (PSlip.Length == 0) { lblResults.Text = "Packing slip/Order Number must contain a value"; return; }
            PdfDocument doc = new PdfDocument();
            rptDespatchNote rpt = new rptDespatchNote(doc, rdlReportParameter.SelectedItem.Value, PSlip, LogoPath(), User.Identity.Name);
            string Message = rpt.Message;
            lblResults.Text = Message;
            doc.Save(string.Format("{0}.pdf", rpt.DefaultFileName), Response, HttpReadType.Save); 
        }
        void btnAnalyzePDF_Click(object sender, EventArgs e)
        {
            PdfDocument doc = new PdfDocument();
            rptAnalyzeSystem rpt = new rptAnalyzeSystem(doc, LogoPath(), User.Identity.Name);
            string Message = rpt.Message;
            lblResults.Text = Message;
            doc.Save(string.Format("{0}.pdf", rpt.DefaultFileName), Response, HttpReadType.Save); 
        }

        void btnBarcodeSample_Click(object sender, EventArgs e)
        {
            string PSlip = txtPSlip.Text.Trim();         //  "AS098";
            if (PSlip.Length == 0) { lblResults.Text = "Packing slip must contain a value"; return; }
            PdfDocument doc = new PdfDocument();
            rptBarcodeSample rpt = new rptBarcodeSample(doc, PSlip, LogoPath(), User.Identity.Name);
            string Message = rpt.Message;
            lblResults.Text = Message;
            doc.Save(string.Format("{0}.pdf", rpt.DefaultFileName), Response, HttpReadType.Save);
        }
        #endregion
        # region EXCEL
        protected void btnDespatchExcel_Click(object sender, EventArgs e)
        {
            string PSlip = txtPSlip.Text.Trim();         //  "AS098";
            if (PSlip.Length == 0) { lblResults.Text = "Packing slip must contain a value"; return; }
            rptDespatchNote rpt = new rptDespatchNote(rdlReportParameter.SelectedItem.Value, PSlip, LogoPath(), User.Identity.Name);
            DownloadThisExcel(rpt.GetCommandString, rpt.DefaultFileName);
        }
        void btnDespatchExcelAll_Click(object sender, EventArgs e)
        {
            string PSlip = txtPSlip.Text.Trim();         //  "AS098";
            if (PSlip.Length == 0) { lblResults.Text = "Packing slip must contain a value"; return; }
            rptDespatchNote rpt = new rptDespatchNote(rdlReportParameter.SelectedItem.Value, PSlip, LogoPath(), User.Identity.Name);
            DownloadThisExcel(rpt.GetCommandStringAllAttributes, rpt.DefaultFileName);
        }
        void btnAnalyzeEXCEL_Click(object sender, EventArgs e)
        {
            rptAnalyzeSystem rpt = new rptAnalyzeSystem(LogoPath(), User.Identity.Name);
            DownloadThisExcel(rpt.GetCommandString, rpt.DefaultFileName);
        }




        private void DownloadThisExcel(string cmdString, string FileName)
        {
            //string PSlip = txtPSlip.Text.Trim();         //  "AS098";
            //if (PSlip.Length == 0) { lblResults.Text = "Packing slip must contain a value"; return; }
            string Message = "";
            //string FileName = "";
            ExcelEngine excelEngine = new ExcelEngine();
            IApplication application = excelEngine.Excel;
            application.DefaultVersion = GetExcelVersion();
            IWorkbook workbook = application.Workbooks.Create(1);
            //if (containsABadCharacter.IsMatch(PSlip)) { FileName = "DespatchReport{0}"; } else { FileName = string.Format("DespatchReport{0}", PSlip); };
            ExcelManager em = new ExcelManager(User.Identity.Name);
            workbook = em.ExportToExcel(workbook, cmdString, FileName, ref Message);
            FileName = em.FileName;
            if (Message.Length > 0)
            {
                lblResults.Text = Message;
            }
            workbook.SaveAs(FileName, ExcelSaveType.SaveAsXLS, Response, ExcelDownloadType.Open);
            workbook.Close();
            excelEngine.Dispose();
        }
        #endregion
        #region Helper Functions
       private string LogoPath()
        {
            return Server.MapPath(SysUtil.LogoPath());
            //return Server.MapPath("~/Styles/images/logo.jpg");
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
        #endregion
    }

}