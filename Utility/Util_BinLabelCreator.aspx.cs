using System;
using System.Linq;
using System.Web.UI;
using BW_WebApp.DataManagers;
//using Factory_Businesslayer;

namespace BW_WebApp.Utility
{
    public partial class Util_BinLabelCreator : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            btnPrintLabels.Click += new EventHandler(btnPrintLabels_Click);

            if (!IsPostBack)
            {

                int[] PrintNumber = new int[] { 1, 5, 10, 15, 20, 25 };

                rdlNumberToPrint.DataSource = PrintNumber;
                rdlNumberToPrint.DataBind();
                rdlNumberToPrint.SelectedIndex = 0;

                ClientManager cm = new ClientManager(User.Identity.Name);
                drpClientList_New.DataValueField = "ClientID";
                drpClientList_New.DataTextField = "CompanyName";
                // drpClientList_New.DataSource = cm.DropDownSearchLocationsList("", "", "", "").OrderBy(x => x.CompanyName);
                drpClientList_New.DataSource = cm.DropDownSearchClientList("", "", "").OrderBy(x => x.CompanyName);
                drpClientList_New.DataBind();
                drpClientList_New.SelectedIndex = 0;
            }
        }

        void btnPrintLabels_Click(object sender, EventArgs e)
        {
            clsLinqDataContext ctx = new clsLinqDataContext();
            decimal NumberRequired = 0;
            //string nr = rdlNumberToPrint.SelectedItem.Text;
            if (decimal.TryParse(rdlNumberToPrint.SelectedItem.Text, out NumberRequired) == false) { NumberRequired = 0; }
            if (NumberRequired < 1) { return; }
            decimal BinSeed = ctx.NextBinNumber(NumberRequired);
            ScriptManager.RegisterStartupScript(this, GetType(), "LoadUnit", "OpenBinTag('" + NumberRequired.ToString() + "','" + BinSeed.ToString() + "','" + drpClientList_New.SelectedItem.Text.Trim() + "');", true);
        }
    }
}