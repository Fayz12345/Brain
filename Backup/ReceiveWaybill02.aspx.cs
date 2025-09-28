using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BW_WebApp.DataManagers;

namespace BW_WebApp
{
    public partial class ReceiveWaybill02 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnClear.Click += new EventHandler(btnClear_Click);
            drpMasterUnitType.SelectedIndexChanged += new EventHandler(drpMasterUnitType_SelectedIndexChanged);
            btnRefreshBinList.Click += new EventHandler(btnRefreshBinList_Click);
            //btnOK.Click += new EventHandler(btnOK_Click);
            hdnUserName.Value = User.Identity.Name;
            if (!IsPostBack)
            {
                FillMasterUnitTypeDropDown();
                FillMasterUnitIDCBinDropDown();
            }
            txtWaybill.Focus();
            txtWaybill.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13) || (event.which == 9) || (event.keyCode == 9)) {SetESNFocus();return false;}} else {return true}; ");
            txtWaybill.Attributes.Add("onblur", "IsMessage();return false;");

            txtESN.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13) || (event.which == 9) || (event.keyCode == 9)) {RecordScanKey();return false;}} else {return true}; ");
            txtESN.Attributes.Add("onblur", "RecordScanKey();return false;");

            //txtWaybill.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13) || (event.which == 9) || (event.keyCode == 9)) {SetWayBillFocus();return false;}} else {return true}; ");
            ////txtWaybill.Attributes.Add("onblur", "IsMessage();return false;");
        }

        void btnRefreshBinList_Click(object sender, EventArgs e)
        {
            // we need to find the real name, not the friendly name.
            string[] y = drpIDCBinList.SelectedItem.Value.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries).Distinct().ToArray();
            //var dta = drpIDCBinList.SelectedItem.Value.Split(':');
            string IDC_BIN_TOSearch = y[2];
            //string IDC_BIN_TOSearch_FriendlyName = drpIDCBinList.SelectedItem.Text;

            lblBinReport.Text = "IDC Bin Report:" + drpIDCBinList.SelectedItem.Text + " (" + IDC_BIN_TOSearch + ")";
            IDC_LocationLogManager manager = new IDC_LocationLogManager(User.Identity.Name);
            List<LocationLog> log = manager.GetPreReleaseIDCBinDetails(IDC_BIN_TOSearch);
            lstBinReportList.Items.Clear();
            decimal count = 0;
            foreach (LocationLog l in log)
            {
                ListItem i = new ListItem(l.ESN,l.LocationLogID.ToString());
                lstBinReportList.Items.Add(i);
                count++;
            }
            txtBinCount.Text = count.ToString();
        }

        void drpMasterUnitType_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillMasterUnitIDCBinDropDown();
        }




        void FillMasterUnitTypeDropDown()
        {

            //ClientManager cm = new ClientManager(User.Identity.Name);
            //drpMasterUnitType.DataValueField = "ClientID";
            //drpMasterUnitType.DataTextField = "CompanyName";
            //drpMasterUnitType.DataSource = cm.DropDownUserFilterMasterClientPreReceiveList("", "", "", "").OrderBy(x => x.CompanyName);
            //drpMasterUnitType.DataBind();

            QuestionManager qm = new QuestionManager(User.Identity.Name);
            List<Option> LO = new List<Option>();
            LO = qm.GetQuestionOptionList("PreRecv Product Type");
            drpMasterUnitType.Items.Clear();
            drpMasterUnitType.Items.Add(new ListItem("* Select *", "-1"));
            foreach (Option o in LO)
            {
                ListItem li = new ListItem(o.OptionText, o.OptionID.ToString() + "," + o.IDC_ClientID.ToString());
                drpMasterUnitType.Items.Add(li);
            }
            drpMasterUnitType.SelectedIndex = 0;
        }
        void FillMasterUnitIDCBinDropDown()
        {

            string Keys = drpMasterUnitType.SelectedItem.Value;
            ////var x = Keys.Split(
            string[] y = Keys.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Distinct().ToArray();
            string sClientID = "";
            string sOptionID = "";
            if (y.Count() > 0)
            {
                sOptionID = y[0];
                if (y.Count() > 1) { sClientID = y[1]; }
            }
            decimal id = 0;
            decimal.TryParse(sClientID, out id);



            lblCompanyName.Text = "";
            hdnClientID.Value = sClientID;
            hdnClientName.Value = "";
            hdnAttributType.Value = sOptionID;

            ClientManager cm = new ClientManager(User.Identity.Name);
            Client c = cm.GetClient(id);
            if (c != null)
            {
                hdnClientName.Value = c.CompanyName;
                lblCompanyName.Text = c.CompanyName;
            }



            QuestionManager qm = new QuestionManager(User.Identity.Name);
            List<Option> LO = new List<Option>();
            LO = qm.GetQuestionOptionList("PreReceive IDC bins").Where(x => x.IDC_ClientID == id).ToList();
            drpIDCBinList.Items.Clear();
            drpIDCBinList.Items.Add(new ListItem("* Select *", "-1"));
            foreach (Option o in LO)
            {
                string text = (o.IDC_FriendlyName == null || o.IDC_FriendlyName.Length == 0) ? "*" + o.OptionText : o.IDC_FriendlyName;
                ListItem li = new ListItem(text, o.OptionID.ToString() + ":" + o.IDC_ClientID.ToString() + ":" + o.OptionText);
                drpIDCBinList.Items.Add(li);
            }
            drpIDCBinList.SelectedIndex = 0;
        }

        void btnClear_Click(object sender, EventArgs e)
        {
            txtWaybill.Text = "";
            lastWayBill.Value = "";
            //txtIDCBIN.Text = "";
            txtESN.Text = "";
            txtCount.Text = "0";
            lblWarningMessage.Text = "";
            FillMasterUnitTypeDropDown();
            FillMasterUnitIDCBinDropDown();
            txtWaybill.Focus();
            //txtWaybill.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13) || (event.which == 9) || (event.keyCode == 9)) {CleanData();SetESNFocus();return false;}} else {return true}; ");
            //txtWaybill.Attributes.Add("onblur", "IsMessage();return false;");

            //txtESN.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13) || (event.which == 9) || (event.keyCode == 9)) {SetWayBillFocus();return false;}} else {return true}; ");
            //txtESN.Attributes.Add("onblur", "RecordScanKey();return false;");

        }
    }
}