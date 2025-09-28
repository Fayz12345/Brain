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
    public partial class RPT_MSCRepair : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string sReceiveDetailID = Request.QueryString.Get("A");
            string sReceiveDetailAuthorizationLogID = Request.QueryString.Get("B");
            decimal ReceiveDetailID = -1;
            if (decimal.TryParse(sReceiveDetailID, out ReceiveDetailID) == false) { ReceiveDetailID = -1; }

            decimal ReceiveDetailAuthorizationLogID = -1;
            if (decimal.TryParse(sReceiveDetailAuthorizationLogID, out ReceiveDetailAuthorizationLogID) == false) { ReceiveDetailAuthorizationLogID = -1; }

            LoadData(ReceiveDetailAuthorizationLogID);
            if (IsPostBack == false)
            {
                CompanyDemographics c = new CompanyDemographics(User.Identity.Name);
                lblcCityProvincePostal.Text = c.CityProvincePostal;
                lblcAddressLines.Text = c.AddressLines;
                lblcCompanyName.Text = c.Name.ToUpper();
                lblcPhone.Text = "Phone #: " + c.Phone;
                lblcFax.Text = "  Fax #: " + c.Fax;
                lblcWebsite.Text = c.Website;
            }
        }

        public void LoadData(decimal ReceiveDetailAuthorizationLogID)
        {
            bcBagtag.DataToEncode = ReceiveDetailAuthorizationLogID.ToString();

            ReceiveDetailManager rdm = new ReceiveDetailManager(User.Identity.Name);
            ReceiveDetailAuthorizationLog rl = rdm.GetThisReceiveDetailAuthorizationLog(ReceiveDetailAuthorizationLogID);
            if (rl != null)
            {
                ReceiveDetail rd = rdm.ReceiveDetail(rl.ReceiveDetailID);
                if (rd != null)
                {
                    ClientLocation cl = rdm.GetClientLocation(rd.ClientLocationID);

                    lblDealerName.Text = cl.CompanyName;
                    lblDealerID.Text = cl.ScanKey;
                    lblPhone.Text = cl.PhoneNumber;
                    lblFax.Text = cl.FaxNumber;
                    lblAddress.Text = cl.AddressLine1 + " " + cl.AddressLine2;
                    lblCity.Text = cl.City;
                    lblProvince.Text = cl.StateOrProvince;
                    lblPostalCode.Text = cl.PostalCode;



                    //sheet.Range[20, 2].Text = "Type:" + rdm.GetReceiveDetailItem_DataElement(RD.ReceiveDetailID, "Receipt Type");
                    //sheet.Range[20, 4].Text = "Second type of tracking#:" + rdm.GetReceiveDetailItem_DataElement(RD.ReceiveDetailID, "SecondType");
                    //sheet.Range[20, 7].Text = "Reference Number:" + rdm.GetReceiveDetailItem_DataElement(RD.ReceiveDetailID, "ReferenceNumber");

                    //sheet.Range[22, 2].Text = "Manufacturer:" + rdm.GetReceiveDetailItem_DataElement(RD.ReceiveDetailID, "Manufacturer");
                    //sheet.Range[23, 2].Text = "Model:" + rdm.GetReceiveDetailItem_DataElement(RD.ReceiveDetailID, "Model");
                    //sheet.Range[24, 2].Text = "Colour:" + rdm.GetReceiveDetailItem_DataElement(RD.ReceiveDetailID, "Colour");
                    //sheet.Range[25, 2].Text = "Carrier:" + rdm.GetReceiveDetailItem_DataElement(RD.ReceiveDetailID, "Carrier");

                    //sheet.Range[27, 2].Text = "Parts Returning:" + rdm.GetReceiveDetailItem_DataElement(RD.ReceiveDetailID, "Parts Returning");


                    string Manufacturer = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Manufacturer");
                    string Model = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Model");
                    string Colour = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Colour");
                    string Carrier = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Carrier");
                    string NickName = rdm.GetCarrierMakeModelColour_NickName(Carrier, Manufacturer, Model, Colour);


                    lblESN.Text = rd.ESN;

                    lblRMA.Text = rd.RMANumber;
                    lblModel.Text = Model;
                    ldlManufacturer.Text = Manufacturer;
                    lblNickname.Text = NickName;

                    lblMSN.Text = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Serial Number");

                    chkInWarranty.SelectedIndex = 0;
                    chkExtendedWarranty.SelectedIndex = 0;
                    chkOutOfWarranty.SelectedIndex = 0;

                    string rType = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Receipt Type");
                    if (rType.ToUpper() == "IN WARRANTY") { chkInWarranty.SelectedIndex = 0; }
                    if (rType.ToUpper() == "EXTENDED WARRANTY") { chkExtendedWarranty.SelectedIndex = 0; }
                    if (rType.ToUpper() == "OUT OF WARRANTY") { chkOutOfWarranty.SelectedIndex = 0; }

                    lblFaultCodes.Text = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Fault Code 1") + " " + rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Fault Code 2");

                    chkApproved.Checked = false;
                    chkDenied.Checked = false;
                    if (rl.AuthorizedDate != null) { chkApproved.Checked = true; }
                    if (rl.DeclinedDate != null) { chkDenied.Checked = true; }
                    lblEstimateFee.Text = rl.EstimateFee.ToString();
                    lblFreightFee.Text = rl.FreightFee.ToString();
                    lblHST.Text = rl.HST.ToString();
                    lblTotal.Text = rl.Total.ToString();

                    lblNote.Text = rl.Note;






                }
            }
        }
    }
}