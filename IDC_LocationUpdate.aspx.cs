using System;
using System.Web.UI;
//using Factory_DataModel;
using BW_WebApp.DataManagers;

namespace BW_WebApp
{
    public partial class IDC_LocationUpdate : System.Web.UI.Page
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

        void btnUpdateLocation_Click(object sender, EventArgs e)
        {
            if (lblESN.Text.Length == 0 && lblBinNumber.Text.Length == 0) { lblMessage.Text = "No ESN/IMEI/Bin given"; return; }
            if (txtLocation.Text.Length == 0) { lblMessage.Text = "No Location given"; return; }
            ReceiveDetailManager rd = new ReceiveDetailManager(User.Identity.Name);
            string IP = GetUserIPAddress();
            if (lblESN.Text.Length > 0)
            {
                // because we have been given an ESN, not a BIn, the only way to know if this IMEI is Active is to look at the version
                // of the IMEI. IF 000, then moving active units, otherwise, unit should be in PreReceive.
                string ESNVersion = rd.GetCurrentVersion(lblESN.Text);
                if (ESNVersion == "000")
                {

                    //JimErrorLogManager logManager = new JimErrorLogManager(User.Identity.Name, "IDC location update:000");
                    //logManager.IsActive = true;
                    //logManager.ReportMessage("e(" + lblESN.Text + "):b(" + lblBinNumber.Text + "):b" + txtLocation.Text);
                    lblMessage.Text = rd.UpdateESN_LocationAndBinValue_IDC(lblESN.Text, txtLocation.Text, lblBinNumber.Text, IP);
                }
                else
                {
                    //JimErrorLogManager logManager = new JimErrorLogManager(User.Identity.Name, "IDC location update:xxx");
                    //logManager.IsActive = true;
                    //logManager.ReportMessage("e(" + lblESN.Text + "):b(" + lblBinNumber.Text + "):b" + txtLocation.Text);
                    lblMessage.Text = rd.UpdateESN_LocationAndBin_Prereceive_IDC(lblESN.Text, txtLocation.Text, lblBinNumber.Text, IP);
                }
            }
            else if (lblBinNumber.Text.Length > 5 && lblBinNumber.Text.Substring(0, 6).ToUpper() == "IDCBIN")
            {
                // this is a pre-receive record. Go find it and update it.
                //JimErrorLogManager logManager = new JimErrorLogManager(User.Identity.Name, "IDC location update:IDCBIN");
                //logManager.IsActive = true;
                //logManager.ReportMessage("e(" + lblESN.Text + "):b(" + lblBinNumber.Text + "):b" + txtLocation.Text);
                lblMessage.Text = rd.UpdateESN_LocationAndBin_Prereceive_IDC(lblESN.Text, txtLocation.Text, lblBinNumber.Text, IP);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "alert('Invalid BIN move detected. Only IDCBINs can be transferred using this utility. Please scan each unit individually.');", true);
                lblMessage.Text = "Invalid BIN move detected. \n \n Only IDCBINs can be transferred using this utility.\n Please scan each unit individually.";
            }
            lblESN.Text = "";
            lblESN.Focus();
        }


        private string GetUserIPAddress()
        {
            string ip = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(ip))
            {
                ip = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            return ip;
        }


    }
}