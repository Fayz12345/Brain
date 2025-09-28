using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BW_WebApp.DataManagers;

namespace BW_WebApp.Utility
{
    public partial class CycleCount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                ListItem z = new ListItem("All", "-1");
                QuestionManager qm = new QuestionManager(User.Identity.Name);

                List<Option> ol = qm.GetQuestionOptionList("IFSLocation");
                chkLocation.Items.Clear();
                //z = new ListItem("All", "-1");
                //chkLocation.RepeatColumns = 5;
                //chkLocation.RepeatDirection = RepeatDirection.Horizontal;
                //chkLocation.Items.Add(z);
                foreach (Option o in ol)
                {
                    ListItem x = new ListItem(o.OptionText, o.OptionID.ToString());
                    chkLocation.Items.Add(x);
                }

                ol = qm.GetQuestionOptionList("IFS Conditions");
                chkCondition.Items.Clear();
                //z = new ListItem("All", "-1");
                //chkCondition.RepeatColumns = 3;
                //chkCondition.RepeatDirection = RepeatDirection.Horizontal;
                //chkCondition.Items.Add(z);
                foreach (Option o in ol)
                {
                    ListItem x = new ListItem(o.OptionText, o.OptionID.ToString());
                    chkCondition.Items.Add(x);
                }


                ol = qm.GetQuestionOptionList("Carrier");
                chkCarrier.Items.Clear();
                //z = new ListItem("All", "-1");
                //chkCarrier.RepeatColumns = 8;
                //chkCarrier.RepeatDirection = RepeatDirection.Horizontal;
                //chkCarrier.Items.Add(z);
                foreach (Option o in ol)
                {
                    ListItem x = new ListItem(o.OptionText, o.OptionID.ToString());
                    chkCarrier.Items.Add(x);
                }

                ol = qm.GetQuestionOptionList("Manufacturer");
                chkManufacturer.Items.Clear();
                //z = new ListItem("All", "-1");
                //chkManufacturer.RepeatColumns = 8;
                //chkManufacturer.RepeatDirection = RepeatDirection.Horizontal;
                //chkManufacturer.Items.Add(z);
                foreach (Option o in ol)
                {
                    ListItem x = new ListItem(o.OptionText, o.OptionID.ToString());
                    chkManufacturer.Items.Add(x);
                }

                ol = qm.GetQuestionOptionList("Model");
                chkModel.Items.Clear();
                //z = new ListItem("All", "-1");
                //chkModel.RepeatColumns = 6;
                //chkModel.RepeatDirection = RepeatDirection.Horizontal;
                //chkModel.Items.Add(z);
                foreach (Option o in ol)
                {
                    ListItem x = new ListItem(o.OptionText, o.OptionID.ToString());
                    chkModel.Items.Add(x);
                }

                ol = qm.GetQuestionOptionList("Colour");
                chkColour.Items.Clear();
                //z = new ListItem("All", "-1");
                //chkColour.RepeatColumns = 8;
                //chkColour.RepeatDirection = RepeatDirection.Horizontal;
                //chkColour.Items.Add(z);
                foreach (Option o in ol)
                {
                    ListItem x = new ListItem(o.OptionText, o.OptionID.ToString());
                    chkColour.Items.Add(x);
                }
            }

            //btnClear.Click += new EventHandler(btnClear_Click);
            //hdnUserName.Value = User.Identity.Name;
            //if (!IsPostBack)
            //{
            //    ClientManager cm = new ClientManager(User.Identity.Name);

            //    drpMasterClient.DataValueField = "ClientID";
            //    drpMasterClient.DataTextField = "CompanyName";
            //    drpMasterClient.DataSource = cm.DropDownUserFilterMasterClientList("", "", "", "").OrderBy(x => x.CompanyName);
            //    drpMasterClient.DataBind();
            //    drpMasterClient.SelectedIndex = 0;
            //}
            //txtWaybill.Focus();
            //txtWaybill.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13) || (event.which == 9) || (event.keyCode == 9)) {;SetESNFocus();return false;}} else {return true}; ");
            //txtWaybill.Attributes.Add("onblur", "IsMessage();return false;");

            //txtESN.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13) || (event.which == 9) || (event.keyCode == 9)) {SetWayBillFocus();return false;}} else {return true}; ");
            //txtESN.Attributes.Add("onblur", "RecordScanKey();return false;");
        }

        //void btnClear_Click(object sender, EventArgs e)
        //{
        //    txtWaybill.Text = "";
        //    lastWayBill.Value = "";

        //    txtESN.Text = "";
        //    txtCount.Text = "0";
        //    lblWarningMessage.Text = "";
        //    txtWaybill.Focus();
        //    //txtWaybill.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13) || (event.which == 9) || (event.keyCode == 9)) {CleanData();SetESNFocus();return false;}} else {return true}; ");
        //    //txtWaybill.Attributes.Add("onblur", "IsMessage();return false;");

        //    //txtESN.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13) || (event.which == 9) || (event.keyCode == 9)) {SetWayBillFocus();return false;}} else {return true}; ");
        //    //txtESN.Attributes.Add("onblur", "RecordScanKey();return false;");

        //}
    }
}