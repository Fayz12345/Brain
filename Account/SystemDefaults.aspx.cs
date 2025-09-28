using System;
using System.Web.UI;
using BW_WebApp.DataManagers;

namespace BW_WebApp.Account
{
    public partial class SystemDefaults : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //btnReset.Click += new EventHandler(btnReset_Click);
            EditOK.Click += new EventHandler(EditOK_Click);
            if (IsPostBack == false)
            {
                LoadData();
            }
        }


        void EditOK_Click(object sender, EventArgs e)
        {
            CompanyDemographics c = new CompanyDemographics(User.Identity.Name);
            c.Name = EditName.Text;
            c.AddressLine1 = EditAddressLine1.Text;
            c.AddressLine2 = EditAddressLine2.Text;
            c.City = EditCity.Text;
            c.Province = EditProvince.Text;
            c.Postal = EditPostal.Text;
            c.Phone = EditPhone.Text;
            c.Fax = EditFax.Text;
            c.Website = EditWebsite.Text;
            //c.OrderEntryEmail = EditOrderEntry.Text;
            //c.RedTagEmail = EditRedTagEmail.Text;
            //c.YellowTagEmail = EditYellowTagEmail.Text;
            //c.SupportEmail = EditSupportEmail.Text;
            //c.NormalDayStartTime = EditStartTime.SelectedItem.Text;
            //c.NormalDayEndTime = EditEndTime.SelectedItem.Text;
            //c.TodayBecomesTomorrowTime = EditTodayBecomesTomorrow.Text;
            //c.BackgroundEmailProcessorIntrval = EditEmailProcessorInterval.Text;
            c.SupportEmail = EditSupportEmail.Text;
            c.PartReqEmailAddress = EditPartRequestEmail.Text;
            c.PartReturnEmailAddress = EditPartReturnEmail.Text;
            c.AllowMasterTableDelete = EditCheckDeleteMasterOK.Checked;
            c.WelcomeText = EditWelcome.Text;

            //c.DiscrepancyMasterClientID = EditDiscrepancyMasterClientID.Text;
            c.Save();
            ScriptManager.RegisterStartupScript(this, GetType(), "x", "alert('Data Updated');", true);
        }

        void LoadData()
        {
            CompanyDemographics c = new CompanyDemographics(User.Identity.Name);
            EditName.Text = c.Name.ToUpper();
            EditAddressLine1.Text = c.AddressLine1;
            EditAddressLine2.Text = c.AddressLine2;
            EditCity.Text = c.City;
            EditProvince.Text = c.Province;
            EditPostal.Text = c.Postal;
            EditPhone.Text = c.Phone;
            EditFax.Text = c.Fax;
            EditWebsite.Text = c.Website;
            //EditOrderEntry.Text = c.OrderEntryEmail;
            //EditRedTagEmail.Text = c.RedTagEmail;
            //EditYellowTagEmail.Text = c.YellowTagEmail;
            EditSupportEmail.Text = c.SupportEmail;
            EditPartRequestEmail.Text = c.PartReqEmailAddress;
            EditPartReturnEmail.Text = c.PartReturnEmailAddress;
            EditCheckDeleteMasterOK.Checked = c.AllowMasterTableDelete;
            EditWelcome.Text = c.WelcomeText;
            //EditDiscrepancyMasterClientID.Text = c.DiscrepancyMasterClientID;

            //EditEmailProcessorInterval.Text = c.BackgroundEmailProcessorIntrval;
            //ListItem _ListItem = EditStartTime.Items.FindByText(c.NormalDayStartTime);
            //if (_ListItem == null) { EditStartTime.SelectedIndex = 0; }
            //else { EditStartTime.SelectedIndex = EditStartTime.Items.IndexOf(_ListItem); }

            //_ListItem = EditEndTime.Items.FindByText(c.NormalDayEndTime);
            //if (_ListItem == null) { EditEndTime.SelectedIndex = 0; }
            //else { EditEndTime.SelectedIndex = EditEndTime.Items.IndexOf(_ListItem); }

            //_ListItem = EditTodayBecomesTomorrow.Items.FindByText(c.TodayBecomesTomorrowTime);
            //if (_ListItem == null) { EditTodayBecomesTomorrow.SelectedIndex = 0; }
            //else { EditTodayBecomesTomorrow.SelectedIndex = EditTodayBecomesTomorrow.Items.IndexOf(_ListItem); }
        }


    }
}