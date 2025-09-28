using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using Factory_DataModel;
using BW_WebApp.DataManagers;

namespace BW_WebApp
{
    public partial class ReceiveDetail_XBinLocation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnUpdateLocation.Click += new EventHandler(btnUpdateLocation_Click);
            if (!IsPostBack)
            {
                ReceiveDetailManager rd = new ReceiveDetailManager(User.Identity.Name);
                drpLocationList.DataValueField = "ID";
                drpLocationList.DataTextField = "Desc";
                drpLocationList.DataSource = rd.GetMasterLocationList();
                drpLocationList.DataBind();
            }
        }

        void btnUpdateLocation_Click(object sender, EventArgs e)
        {
            if (lblBinNumber.Text.Length == 0 && lblESN.Text.Length == 0) { lblMessage.Text = "No Bin number or ESN/IMEI given"; return; }
            ReceiveDetailManager rd = new ReceiveDetailManager(User.Identity.Name);
            decimal LocationID = -1;
            if (decimal.TryParse(drpLocationList.SelectedItem.Value, out LocationID) == false) { LocationID = -1; }

            if (lblESN.Text.Length > 0) { lblMessage.Text = rd.UpdateESN_LocationValue_ByID(lblESN.Text, LocationID, drpLocationList.SelectedItem.Text); }
            else { lblMessage.Text = rd.UpdateBin_LocationValue_ByID(lblBinNumber.Text, LocationID); }
        }
    }
}