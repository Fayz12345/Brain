using System;
using System.Collections.Generic;
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
using System.IO;
using Syncfusion.Pdf.Parsing;

using BW_WebApp.DataManagers;

namespace BW_WebApp.PDFReports
{
    public partial class DespatchNote : System.Web.UI.Page
    {
        # region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            //btnDespatch.Click +=new System.EventHandler(btnDespatch_Click);

        }
        #endregion
        //protected void btnDespatch_Click(object sender, EventArgs e)
        //{
        //    string PSlip = txtPSlip.Text;         //  "AS098";
        //    string LogoPath = Server.MapPath("~/Styles/images/logo.jpg");
        //    PdfDocument doc = new PdfDocument();
        //    rptDespatchNote rpt = new rptDespatchNote(doc, PSlip, LogoPath, User.Identity.Name);
        //    string Message = rpt.Message;
        //    lblResults.Text = rpt.Message;
        //    doc.Save(string.Format("DESPATCH_NOTE.{0}.pdf", PSlip.Trim()), Response, HttpReadType.Save);
        //}
    }

}