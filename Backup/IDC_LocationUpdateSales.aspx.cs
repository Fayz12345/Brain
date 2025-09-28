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
    public partial class IDC_LocationUpdateSales : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnUpdateLocation.Click += new EventHandler(btnUpdateLocation_Click);
            if (!IsPostBack)
            {
                //ReceiveDetailManager rd = new ReceiveDetailManager(User.Identity.Name);
                //drpLocationList.DataValueField = "ID";
                //drpLocationList.DataTextField = "Desc";
                //drpLocationList.DataSource = rd.GetMasterLocationList();
                //drpLocationList.DataBind();
            }
        }




        //void btnUpdateLocation_Click(object sender, EventArgs e)
        //{
        //    if (lblESN.Text.Length == 0 && lblBinNumber.Text.Length == 0) { lblMessage.Text = "No ESN/IMEI/Bin given"; return; }
        //    //if (lblBinNumber.Text.Length == 0) { lblMessage.Text = "No Bin number given"; return; }
        //    if (txtLocation.Text.Length == 0) { lblMessage.Text = "No Location given"; return; }

        //    ReceiveDetailManager rd = new ReceiveDetailManager(User.Identity.Name);

        //    if (lblESN.Text.Length > 0)
        //    {
        //        // because we have been given an ESN, not a BIn, the only way to know if this IMEI is Active is to look at the version
        //        // of the IMEI. IF 000, then moving active units, otherwise, unit should be in PreReceive.
        //        string ESNVersion = rd.GetCurrentVersion(lblESN.Text);
        //        if (ESNVersion == "000")
        //        {
        //            lblMessage.Text = rd.UpdateESN_LocationAndBinValue_IDC(lblESN.Text, txtLocation.Text, lblBinNumber.Text);
        //        }
        //        else
        //        {
        //            lblMessage.Text = rd.UpdateESN_LocationAndBin_Prereceive_IDC(lblESN.Text, txtLocation.Text, lblBinNumber.Text);
        //        }
        //    }
        //    else if (lblBinNumber.Text.Length > 5 && lblBinNumber.Text.Substring(0, 6).ToUpper() == "IDCBIN")
        //    {
        //        // this is a pre-receive record. Go find it and update it.
        //        lblMessage.Text = rd.UpdateESN_LocationAndBin_Prereceive_IDC(lblESN.Text, txtLocation.Text, lblBinNumber.Text);

        //    }
        //    else
        //    {
        //        lblMessage.Text = rd.UpdateESN_LocationAndBinValue_IDC(lblESN.Text, txtLocation.Text, lblBinNumber.Text);
        //    }

        //    //decimal LocationID = -1;
        //    ////if (decimal.TryParse(drpLocationList.SelectedItem.Value, out LocationID) == false) { LocationID = -1; }

        //    //if (lblESN.Text.Length > 0) { lblMessage.Text = rd.UpdateESN_LocationValue_ByID(lblESN.Text, LocationID, drpLocationList.SelectedItem.Text); }
        //    //else { lblMessage.Text = rd.UpdateBin_LocationValue_ByID(lblBinNumber.Text, LocationID); }
        //    //lblMessage.Text = lblESN.Text + " Updated";
        //    lblESN.Text = "";
        //    lblESN.Focus();
        //}





        void btnUpdateLocation_Click(object sender, EventArgs e)
        {
            if (lblESN.Text.Length == 0 && lblBinNumber.Text.Length == 0) { lblMessage.Text = "No ESN/IMEI/Bin given"; return; }
            if (txtLocation.Text.Length == 0) { lblMessage.Text = "No Location given"; return; }


            
            
            ReceiveDetailManager rd = new ReceiveDetailManager(User.Identity.Name);

            if (lblESN.Text.Length > 0)
            {
                lblMessage.Text = rd.UpdateESN_IDC(lblESN.Text, txtLocation.Text, lblBinNumber.Text);
            }
            else if (lblBinNumber.Text.Length > 5 && lblBinNumber.Text.Substring(0, 6).ToUpper() == "IDCBIN")
            {
                // this is a pre-receive record. Go find it and update it.
                lblMessage.Text = rd.UpdateESN_IDC(lblESN.Text, txtLocation.Text, lblBinNumber.Text);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "alert('Invalid BIN move detected. Only IDCBINs can be transferred using this utility. Please scan each unit individually.');", true);
                lblMessage.Text = "Invalid BIN move detected. \n \n Only IDCBINs can be transferred using this utility.\n Please scan each unit individually.";
            }
            lblESN.Text = "";
            lblESN.Focus();
        }
    }
}