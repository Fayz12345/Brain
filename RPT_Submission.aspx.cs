using System;
//using Factory_DataModel;
using BW_WebApp.DataManagers;

namespace BW_WebApp
{
    public partial class RPT_Submission : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string sReceiveDetailID = Request.QueryString.Get("RDID");
            //string sReceiveDetailAuthorizationLogID = Request.QueryString.Get("B");
            decimal ReceiveDetailID = -1;
            if (decimal.TryParse(sReceiveDetailID, out ReceiveDetailID) == false) { ReceiveDetailID = -1; }

            //decimal ReceiveDetailAuthorizationLogID = -1;
            //if (decimal.TryParse(sReceiveDetailAuthorizationLogID, out ReceiveDetailAuthorizationLogID) == false) { ReceiveDetailAuthorizationLogID = -1; }

            LoadData(ReceiveDetailID);

            if (IsPostBack == false)
            {
                CompanyDemographics c = new CompanyDemographics(User.Identity.Name);
                lblcCityProvincePostal.Text = c.CityProvincePostal;
                lblcAddressLines.Text = c.AddressLines;
                lblcCompanyName.Text = c.Name.ToUpper();
            }


        }

        public void LoadData(decimal ReceiveDetailID)
        {
            //bcBagtag.DataToEncode = ReceiveDetailAuthorizationLogID.ToString();
            //bcProjectTag.Visible = false;
            //brRMA.Visible = false;
            IMEI_04.Visible = false;
            ReceiveDetailManager rdm = new ReceiveDetailManager(User.Identity.Name);

            ReceiveDetail rd = rdm.ReceiveDetailNoRestrict(ReceiveDetailID);
            if (rd != null)
            {

                //lblG10.Text = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Client Reference #");
                //lblG11.Text = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Warranty No.");

                lblG10.Text = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Service Request Num");
                lblG11.Text = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Warranty Type");

                lblG12.Text = rd.ReceiveDate.ToShortDateString();

                ClientLocation cl = rdm.GetClientLocation(rd.ClientLocationID);
                if (cl != null)
                {
                    lblDealerName.Text = cl.CompanyName;
                    lblDealerID.Text = cl.ScanKey;
                    lblPhone.Text = cl.PhoneNumber;
                    lblFax.Text = cl.FaxNumber;
                    lblAddress.Text = cl.AddressLine1 + " " + cl.AddressLine2;
                    lblCity.Text = cl.City;
                    lblProvince.Text = cl.StateOrProvince;
                    lblPostalCode.Text = cl.PostalCode;
                }

                //if (rd.ProjectTag.Length > 0) { bcProjectTag.Visible = true; bcProjectTag.DataToEncode = rd.ProjectTag; }
                //if (rd.RMANumber.Length > 0) { brRMA.Visible = true; brRMA.DataToEncode = rd.RMANumber; }

                string Type = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Receipt Type");
                string SecondType = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "SecondType");
                string ReferenceNumber = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "ReferenceNumber");

                if (Type.Length > 0) { lblB20.Text = "Type:" + Type; }
                if (SecondType.Length > 0) { lblD20.Text = "Second type of tracking#:" + SecondType; }
                if (ReferenceNumber.Length > 0) { lblG20.Text = "Reference Number:" + ReferenceNumber; }


                lblB22.Text = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Manufacturer");
                lblB23.Text = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Model");
                lblB24.Text = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Colour");
                lblB25.Text = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Carrier");
                //lblB27.Text = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Parts Returning");
                lblB27.Text = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Store Comments");
                //lblB27.Text = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Accessories Shipping");

                if (rd.ESN.Length > 0) { IMEI_04.Visible = true; IMEI_04.DataToEncode = rd.ESN; }

                lblB34.Text = rd.ESN;


                lblFault.Text = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Complaint");
                lblFault2.Text = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Complaint 2");
                lblCustomerName.Text = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Customer Name");
                lblActivationDate.Text = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Activation Date");
                lblActivationDate.Text = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Original IMEI");


                lblB36.Text = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Customer Notes");
                lblB38.Text = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Dealer Waybill");
                //lblB36.Text = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Complaint") + " " + rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Complaint 2");
                //lblB38.Text = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Receipt Type");

            }
        }
    }
}