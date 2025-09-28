using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BW_WebApp.BarcodeUtils;
//using Factory_DataModel;
using BW_WebApp.DataManagers;
using Syncfusion.XlsIO;

namespace BW_WebApp.Reports
{
    public partial class RPT_UnitView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string ESNNumber = Request.QueryString.Get("ESN");
            string sReceiveDetailID = Request.QueryString.Get("RID");
            decimal ReceiveDetailID = -1;
            if (decimal.TryParse(sReceiveDetailID, out ReceiveDetailID) == false) { ReceiveDetailID = -1; }
            lblESN.Text = ESNNumber;
            ReceiveDetailManager rdm = new ReceiveDetailManager(User.Identity.Name);
            List<ReceiveDetail_UnitView> Spot = rdm.Report_UnitView(ESNNumber, 'Y');
            UnitView.DataSource = Spot;
            UnitView.DataBind();
        }
    }
}