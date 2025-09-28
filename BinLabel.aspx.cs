using System;
using System.Collections.Generic;
//using Factory_DataModel;
using BW_WebApp.DataManagers;

namespace BW_WebApp
{
    public partial class BinLabel : System.Web.UI.Page
    {
        clsLinqDataContext ctx = new clsLinqDataContext();

        protected void Page_Unload(object sender, EventArgs e)
        {
            ctx.Dispose();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string QTY = Request.QueryString.Get("QTY");
            string Seed = Request.QueryString.Get("SEED");
            string Client = Request.QueryString.Get("CLIENT");
            //lblClient.Text = Client;
            //lblBinNumber.Text = Seed;
            //bcBINID.DataToEncode = "/BN" + Seed;
            //bcProcessScan.DataToEncode = "XBINX" + Seed;
            decimal NumberRequired = 0;
            decimal BinSeed = 0;
            if (decimal.TryParse(QTY, out NumberRequired) == false) { return; }
            if (decimal.TryParse(Seed, out BinSeed) == false) { return; }

            List<LabelData> data = new List<LabelData>();

            for (decimal i = 0; i < NumberRequired; i++)
            {
                LabelData d = new LabelData();
                d.Client = Client;
                d.BinNumber = (BinSeed + i).ToString();



                d.BN = "/BN" + (BinSeed + i).ToString();
                d.XBIBX = "XBINX" + (BinSeed + i).ToString();
                data.Add(d);
            }

            rLabels.DataSource = data;
            rLabels.DataBind();
        }
    }


    public class LabelData
    {
        public string Client { get; set; }
        public string BinNumber { get; set; }
        public string BN { get; set; }
        public string XBIBX { get; set; }
    }
}