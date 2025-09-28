using System;
using System.Text;
using System.Drawing;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Web.Security;
using BW_WebApp.Classes;

using BW_WebApp.DataManagers;
using BW_WebApp.BarcodeUtils;
using Syncfusion.Web.UI.WebControls.Shared;

using Syncfusion.XlsIO;
using System.Data;
using System.Data.SqlClient;

namespace BW_WebApp.SalesOrder
{
    public partial class DashBoard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnBillClientEdit.Click +=new EventHandler(btnBillClientEdit_Click);
            btnShipClientEdit.Click +=new EventHandler(btnShipClientEdit_Click);
            btnShipClientSearch.Click += new EventHandler(btnShipClientSearch_Click);
            btnBillClientSearch.Click += new EventHandler(btnBillClientSearch_Click);
            btnSetShipToFromClient.Click += new EventHandler(btnSetShipToFromClient_Click);
            btnGenerateSalesOrder.Click += new EventHandler(btnGenerateSalesOrder_Click);
            btnNew.Click += new EventHandler(btnNew_Click);
            btnNewOK.Click += new EventHandler(btnNewOK_Click);
            btnEditNameAddressCancel.Click += new EventHandler(btnEditNameAddressCancel_Click);
            btnEditNameAddressOK.Click += new EventHandler(btnEditNameAddressOK_Click);
            if (!IsPostBack)
            {
                lblPageTitle.Text = "Sales Order";
                //pnlmainentry.Visible = false;
                pnlNew.Visible = false;
                pnlEditNameAddress.Visible = false;
            }

        }

        #region SO Header

//        Sample Scankeys
//            wll5v2a7
//wll5w0a7
//wll5w1g5
//wll5w1l3
//wll5w1l5
//wll5w1l5a
//wll6a3b8
//wll6b0g3
//wll6b0l7
//wll6c2z6
//wll6e1j1
//wll6g0b5
//wll6g1b7
//wll6h1a7
//wll6h1m3



        void btnSetShipToFromClient_Click(object sender, EventArgs e)
        {
            hdnShipClientLocationID.Value = hdnBillClientLocationID.Value;
            hdnShipCompanyName.Value = hdnBillCompanyName.Value;
            hdnShipContactName.Value = hdnBillContactName.Value;
            hdnShipAddressLine1.Value = hdnBillAddressLine1.Value;
            hdnShipAddressLine2.Value = hdnBillAddressLine2.Value;
            hdnShipCity.Value = hdnBillCity.Value;
            hdnShipStateOrProvince.Value = hdnBillStateOrProvince.Value;
            hdnShipPostalCode.Value = hdnBillPostalCode.Value;
            hdnShipPhoneNumber.Value = hdnBillPhoneNumber.Value;
            hdnShipFaxNumber.Value = hdnBillFaxNumber.Value;
            hdnShipNotes.Value = hdnBillNotes.Value;
            txtShipNameAddresstext.Text = GetAddressString("ShipTo");
        }
        void btnBillClientSearch_Click(object sender, EventArgs e)
        {
            // search and get the data from the database
            clsSOHeaderCompany Company = new clsSOHeaderCompany();
            Company.LoadCompanyFromClientLocation(txtBillClient.Text);
            if (Company.ClientLocationID < 1)
            {
                hdnBillCompanyName.Value = txtBillClient.Text + " Not found";
                hdnBillContactName.Value = "";
                hdnBillClientLocationID.Value = "-1";
                hdnBillAddressLine1.Value = "";
                hdnBillAddressLine2.Value = ""; ;
                hdnBillCity.Value = "";
                hdnBillStateOrProvince.Value = "";
                hdnBillPostalCode.Value = "";
                hdnBillPhoneNumber.Value = "";
                hdnBillFaxNumber.Value = "";
                hdnBillNotes.Value = "";
                txtBillNameAddresstext.Text = GetAddressString("CLIENT");
            }
            else
            {
                txtBillClient.Text = "";
                hdnBillClientLocationID.Value = Company.ClientLocationID.ToString();
                hdnBillCompanyName.Value = Company.CompanyName;
                hdnBillContactName.Value = Company.ContactName;
                hdnBillAddressLine1.Value = Company.AddressLine1;
                hdnBillAddressLine2.Value = Company.AddressLine2;
                hdnBillCity.Value = Company.City;
                hdnBillStateOrProvince.Value = Company.StateOrProvince;
                hdnBillPostalCode.Value = Company.PostalCode;
                hdnBillPhoneNumber.Value = Company.PhoneNumber;
                hdnBillFaxNumber.Value = Company.FaxNumber;
                hdnBillNotes.Value = Company.Notes;

                txtBillNameAddresstext.Text = GetAddressString("CLIENT");
            }

        }
        void btnShipClientSearch_Click(object sender, EventArgs e)
        {
            clsSOHeaderCompany Company = new clsSOHeaderCompany();
            Company.LoadCompanyFromClientLocation(txtShipClient.Text);
            if (Company.ClientLocationID < 1)
            {
                hdnShipCompanyName.Value = txtShipClient.Text + " Not found";
                txtShipClient.Text = "";
                hdnShipClientLocationID.Value = "-1";
                hdnShipContactName.Value = "";
                hdnShipAddressLine1.Value = "";
                hdnShipAddressLine2.Value = "";;
                hdnShipCity.Value = "";
                hdnShipStateOrProvince.Value = "";
                hdnShipPostalCode.Value = "";
                hdnShipPhoneNumber.Value = "";
                hdnShipFaxNumber.Value = "";
                hdnShipNotes.Value = "";
                txtShipNameAddresstext.Text = GetAddressString("ShipTo");
            }
            else
            {
                txtShipClient.Text = "";
                // search and get the data from the database
                hdnShipClientLocationID.Value = Company.ClientLocationID.ToString();
                hdnShipCompanyName.Value = Company.CompanyName;
                hdnShipContactName.Value = Company.ContactName;
                hdnShipAddressLine1.Value = Company.AddressLine1;
                hdnShipAddressLine2.Value = Company.AddressLine2;
                hdnShipCity.Value = Company.City;
                hdnShipStateOrProvince.Value = Company.StateOrProvince;
                hdnShipPostalCode.Value = Company.PostalCode;
                hdnShipPhoneNumber.Value = Company.PhoneNumber;
                hdnShipFaxNumber.Value = Company.FaxNumber;
                hdnShipNotes.Value = Company.Notes;
                txtShipNameAddresstext.Text = GetAddressString("ShipTo");
            }
        }



        void btnNew_Click(object sender, EventArgs e)
        {
            pnlEditNameAddress.Visible = false;
            pnlmainentry.Visible = false;
            pnlNew.Visible = true;
            ShowGenerateSalesOrder();
        }

        private void ShowGenerateSalesOrder()
        {
            btnGenerateSalesOrder.Visible = false;
            if (_SOHeaderID() < 1) { btnGenerateSalesOrder.Visible = true; }
        }
        void btnGenerateSalesOrder_Click(object sender, EventArgs e)
        {
            SaveHeaderData();
        }
        void SaveHeaderData()
        {
            clsSOHeader OH = GatherSheetData();
            OH.SaveHeaderData();
            lblPurchaseOrderNumber.Text = OH.OrderNumber;
            hdnSOHeaderID.Value = OH.SOHeaderID.ToString();
            ShowGenerateSalesOrder();
            //hdnSOHeaderID.Value = OH.SOHeaderID.ToString();
        }


        void btnNewOK_Click(object sender, EventArgs e)
        {
            pnlEditNameAddress.Visible = false;
            pnlmainentry.Visible = true;
            pnlNew.Visible = false;
            Refresh_grdSODetail();
        }

        clsSOHeader GatherSheetData()
        {
            decimal ID = -1;
            ID = _SOHeaderID();
            // Header Data.

            clsSOHeader OH = new clsSOHeader(User.Identity.Name);
            OH.SOHeaderID = ID;
            OH.UserName = User.Identity.Name;
            OH.CustomerPO = txtCustomerPONumber.Text;
            OH.MiscReference = txtReference.Text;
            OH.OrderNumber = lblPurchaseOrderNumber.Text;
            //OH.WaybillNumber = txtWaybillNumber.Text;
            OH.ProjectTag = "";  //  txtProjectTag.Text;
            OH.Paid = false; // chkPaid.Checked;
            OH.PostPaid = false; // chkPostPaid.Checked;
            //if (decimal.TryParse(drpProjectList_New.SelectedValue, out ID) == false) { ID = -1; }
            //OH.ProjectID = ID;

            // Company Data
            if (decimal.TryParse(hdnBillClientLocationID.Value, out ID) == false) { ID = -1; }
            OH.ClientCompany.ClientLocationID = ID;
            OH.ClientCompany.CompanyName = hdnBillCompanyName.Value;
            OH.ClientCompany.ContactName = hdnBillContactName.Value;
            OH.ClientCompany.AddressLine1 = hdnBillAddressLine1.Value;
            OH.ClientCompany.AddressLine2 = hdnBillAddressLine2.Value;
            OH.ClientCompany.City = hdnBillCity.Value;
            OH.ClientCompany.StateOrProvince = hdnBillStateOrProvince.Value;
            OH.ClientCompany.PostalCode = hdnBillPostalCode.Value;
            OH.ClientCompany.PhoneNumber = hdnBillPhoneNumber.Value;
            OH.ClientCompany.FaxNumber = hdnBillFaxNumber.Value;
            OH.ClientCompany.Notes = hdnBillNotes.Value;
            OH.ClientCompany.CompanyType = "Client";

            if (decimal.TryParse(hdnShipClientLocationID.Value, out ID) == false) { ID = -1; }
            OH.ShipToCompany.ClientLocationID = ID;
            OH.ShipToCompany.CompanyName = hdnShipCompanyName.Value;
            OH.ShipToCompany.ContactName = hdnShipContactName.Value;
            OH.ShipToCompany.AddressLine1 = hdnShipAddressLine1.Value;
            OH.ShipToCompany.AddressLine2 = hdnShipAddressLine2.Value;
            OH.ShipToCompany.City = hdnShipCity.Value;
            OH.ShipToCompany.StateOrProvince = hdnShipStateOrProvince.Value;
            OH.ShipToCompany.PostalCode = hdnShipPostalCode.Value;
            OH.ShipToCompany.PhoneNumber = hdnShipPhoneNumber.Value;
            OH.ShipToCompany.FaxNumber = hdnShipFaxNumber.Value;
            OH.ShipToCompany.Notes = hdnShipNotes.Value;
            OH.ShipToCompany.CompanyType = "ShipTo";

            OH.DeliveryNote = txtDeliveryNote.Text;
            OH.InternalNote = txtInternalNote.Text;

            ////////////////////////////////////////////////////////
            #region Line Detail
            //Get Line Detail
            //// get the data in the grid first.
            //foreach (GridViewRow r in grdNewOrderDetailGrid.Rows)
            //{
            //    clsSODetailLine l = new clsSODetailLine();
            //    decimal num = 0;
            //    int _i = 0;
            //    string str = (r.Cells[ColIndex("OrderDetailID", OrderDetailGridcols)].Text == blank ? "-1" : r.Cells[ColIndex("OrderDetailID", OrderDetailGridcols)].Text);
            //    if (decimal.TryParse(str, out num) == false) { num = -1; }
            //    l.OrderDetailID = num;


            //    str = (r.Cells[ColIndex("QTY", OrderDetailGridcols)].Text == blank ? "0" : r.Cells[ColIndex("QTY", OrderDetailGridcols)].Text);
            //    if (decimal.TryParse(str, out num) == false) { num = 0; }
            //    l.QTY = num;

            //    str = (r.Cells[ColIndex("UNITPRICE", OrderDetailGridcols)].Text == blank ? "0" : r.Cells[ColIndex("UNITPRICE", OrderDetailGridcols)].Text);
            //    if (decimal.TryParse(str, out num) == false) { num = 0; }
            //    l.UnitPrice = num;



            //    l.SKU = (r.Cells[ColIndex("SKU", OrderDetailGridcols)].Text == blank ? "" : r.Cells[ColIndex("SKU", OrderDetailGridcols)].Text);
            //    l.Desc_Code = (r.Cells[ColIndex("Desc_Code", OrderDetailGridcols)].Text == blank ? "" : r.Cells[ColIndex("Desc_Code", OrderDetailGridcols)].Text);
            //    l.Desc_Text = (r.Cells[ColIndex("Desc_Text", OrderDetailGridcols)].Text == blank ? "" : r.Cells[ColIndex("Desc_Text", OrderDetailGridcols)].Text);

            //    HiddenField aON = (HiddenField)r.FindControl("hdnOrderNumber");
            //    HiddenField aQ = (HiddenField)r.FindControl("hdnQTY");
            //    HiddenField aID = (HiddenField)r.FindControl("hdnStockID");
            //    HiddenField Manufacturer = (HiddenField)r.FindControl("hdnManufacturer");
            //    HiddenField Model = (HiddenField)r.FindControl("hdnModel");
            //    HiddenField Colour = (HiddenField)r.FindControl("hdnColour");
            //    HiddenField Grade = (HiddenField)r.FindControl("hdnGrade");
            //    HiddenField Carrier = (HiddenField)r.FindControl("hdnCarrier");


            //    l.Manufacturer = Manufacturer.Value;
            //    l.Model = Model.Value;
            //    l.Colour = Colour.Value;
            //    l.Grade = Grade.Value;
            //    l.Carrier = Carrier.Value;
            //    l.AvailableStock_OrderNumber = aON.Value;

            //    str = aQ.Value;
            //    if (int.TryParse(str, out _i) == false) { num = 0; }
            //    l.AvailableStock_QTY = _i;

            //    str = aID.Value;
            //    if (decimal.TryParse(str, out num) == false) { num = -1; }
            //    l.ReservedAvailableStockID = num;


            //    l.isDeleted = false;
            //    //CheckBox btn = (CheckBox)r.FindControl("chkIsDeleted");
            //    //if (btn.Checked == true) { l.isDeleted = true; }
            //    // don't add any lines that are deleted and have not already been saved.
            //    if (l.isDeleted == false || (l.isDeleted && l.OrderDetailID > 0)) { OH.OrderDetailLine = l; }
            //}
            #endregion


            return OH;
        }

        private decimal _SOHeaderID()
        {
            if (hdnSOHeaderID.Value == null) { return -1; }
            decimal ID = -1;
            if (decimal.TryParse(hdnSOHeaderID.Value, out ID) == false) { ID = -1; }
            return ID;
        }


        #endregion
        void Refresh_grdSODetail()
        {

        }



        void btnBillClientEdit_Click(object sender, EventArgs e)
        {
            pnlNew.Visible = false;
            lblEditAddress.Text = "Client Address";
            hdnClientType.Value = "Client";
            txtCompanyName.Text = hdnBillCompanyName.Value;
            txtContactName.Text = hdnBillContactName.Value;
            txtAddressLine1.Text = hdnBillAddressLine1.Value;
            txtAddressLine2.Text = hdnBillAddressLine2.Value;
            txtCity.Text = hdnBillCity.Value;
            txtStateOrProvince.Text = hdnBillStateOrProvince.Value;
            txtPostalCode.Text = hdnBillPostalCode.Value;
            txtPhoneNumber.Text = hdnBillPhoneNumber.Value;
            txtFaxNumber.Text = hdnBillFaxNumber.Value;
            txtNotes.Text = hdnBillNotes.Value;
            pnlNew.Visible = false;
            pnlEditNameAddress.Visible = true;
        }
        void btnShipClientEdit_Click(object sender, EventArgs e)
        {
            pnlNew.Visible = false;
            lblEditAddress.Text = "Ship to Address";
            hdnClientType.Value = "ShipTo";
            //hdnShipClientLocationID.Value = "-1";
            txtCompanyName.Text = hdnShipCompanyName.Value;
            txtContactName.Text = hdnShipContactName.Value;
            txtAddressLine1.Text = hdnShipAddressLine1.Value;
            txtAddressLine2.Text = hdnShipAddressLine2.Value;
            txtCity.Text = hdnShipCity.Value;
            txtStateOrProvince.Text = hdnShipStateOrProvince.Value;
            txtPostalCode.Text = hdnShipPostalCode.Value;
            txtPhoneNumber.Text = hdnShipPhoneNumber.Value;
            txtFaxNumber.Text = hdnShipFaxNumber.Value;
            txtNotes.Text = hdnShipNotes.Value;
            pnlNew.Visible = false;
            pnlEditNameAddress.Visible = true;
        }

        void btnEditNameAddressOK_Click(object sender, EventArgs e)
        {
            hdnAddressUpdated.Value = "T";
            if (hdnClientType.Value.ToUpper() == "CLIENT")
            {
                hdnBillCompanyName.Value = txtCompanyName.Text;
                hdnBillContactName.Value = txtContactName.Text;
                hdnBillAddressLine1.Value = txtAddressLine1.Text;
                hdnBillAddressLine2.Value = txtAddressLine2.Text;
                hdnBillCity.Value = txtCity.Text;
                hdnBillStateOrProvince.Value = txtStateOrProvince.Text;
                hdnBillPostalCode.Value = txtPostalCode.Text;
                hdnBillPhoneNumber.Value = txtPhoneNumber.Text;
                hdnBillFaxNumber.Value = txtFaxNumber.Text;
                hdnBillNotes.Value = txtNotes.Text;
                txtBillNameAddresstext.Text = GetAddressString(hdnClientType.Value);
            }
            if (hdnClientType.Value.ToUpper() == "SHIPTO")
            {
                hdnShipCompanyName.Value = txtCompanyName.Text;
                hdnShipContactName.Value = txtContactName.Text;
                hdnShipAddressLine1.Value = txtAddressLine1.Text;
                hdnShipAddressLine2.Value = txtAddressLine2.Text;
                hdnShipCity.Value = txtCity.Text;
                hdnShipStateOrProvince.Value = txtStateOrProvince.Text;
                hdnShipPostalCode.Value = txtPostalCode.Text;
                hdnShipPhoneNumber.Value = txtPhoneNumber.Text;
                hdnShipFaxNumber.Value = txtFaxNumber.Text;
                hdnShipNotes.Value = txtNotes.Text;
                txtShipNameAddresstext.Text = GetAddressString("ShipTo");
                //hdnPaid.Value = (chkPaid.Checked == true)? "1":"0";
                //hdnPostPaid.Value = (chkPostPaid.Checked == true) ? "1" : "0";
            }
            pnlNew.Visible = true;
            pnlEditNameAddress.Visible = false;
        }
        void btnEditNameAddressCancel_Click(object sender, EventArgs e)
        {
            pnlNew.Visible = true;
            pnlEditNameAddress.Visible = false;
        }

        #region Misc
        private string GetAddressString(string CompanyType)
        {
            string rString = "";
            if (CompanyType.ToUpper() == "SHIPTO")
            {
                rString = hdnShipCompanyName.Value;
                if (hdnShipAddressLine1.Value.Length > 0) { rString += Environment.NewLine; }
                rString += hdnShipAddressLine1.Value;
                if (hdnShipAddressLine2.Value.Length > 0) { rString += Environment.NewLine; }
                rString += hdnShipAddressLine2.Value;
                if (hdnShipCity.Value.Length > 0) { rString += Environment.NewLine; }
                rString += hdnShipCity.Value;
                if (hdnShipStateOrProvince.Value.Length > 0) { rString += Environment.NewLine; }
                rString += hdnShipStateOrProvince.Value;
                if (hdnShipPostalCode.Value.Length > 0) { rString += Environment.NewLine; }
                rString += hdnShipPostalCode.Value;
            }
            if (CompanyType.ToUpper() == "CLIENT")
            {
                rString = hdnBillCompanyName.Value;
                if (hdnBillAddressLine1.Value.Length > 0) { rString += Environment.NewLine; }
                rString += hdnBillAddressLine1.Value;
                if (hdnBillAddressLine2.Value.Length > 0) { rString += Environment.NewLine; }
                rString += hdnBillAddressLine2.Value;
                if (hdnBillCity.Value.Length > 0) { rString += Environment.NewLine; }
                rString += hdnBillCity.Value;
                if (hdnBillStateOrProvince.Value.Length > 0) { rString += Environment.NewLine; }
                rString += hdnBillStateOrProvince.Value;
                if (hdnBillPostalCode.Value.Length > 0) { rString += Environment.NewLine; }
                rString += hdnBillPostalCode.Value;
            }
            return rString;
        }
        #endregion

    }
}