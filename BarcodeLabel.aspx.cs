using System;
using System.Collections.Generic;
//using Factory_DataModel;

namespace BW_WebApp
{
    public partial class BarcodeLabel : System.Web.UI.Page
    {
        // clsLinqDataContext ctx = new clsLinqDataContext();

        protected void Page_Unload(object sender, EventArgs e)
        {
            // ctx.Dispose();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            string TextToEncode = Request.QueryString.Get("CodeText");
            string Desc = Request.QueryString.Get("TEXT");
            string QTY = Request.QueryString.Get("QTY");

            decimal NumberRequired = 0;
            if (decimal.TryParse(QTY, out NumberRequired) == false) { return; }

            List<LabelData> data = new List<LabelData>();

            for (decimal i = 0; i < NumberRequired; i++)
            {
                LabelData d = new LabelData();
                d.Desc = Desc;
                d.TextToEncode = TextToEncode;
                data.Add(d);
            }
            Lablels.DataSource = data;
            Lablels.DataBind();
        }


        public class LabelData
        {
            public string Desc { get; set; }
            public string TextToEncode { get; set; }

        }
    }
}