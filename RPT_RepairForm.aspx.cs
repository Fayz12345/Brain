using System;
//using Factory_DataModel;
using BW_WebApp.DataManagers;

namespace BW_WebApp
{
    public partial class RPT_RepairForm : System.Web.UI.Page
    {
        clsLinqDataContext ctx = new clsLinqDataContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            string sReceiveDetailID = Request.QueryString.Get("A");
            string sReceiveDetailAuthorizationLogID = Request.QueryString.Get("B");
            string sReport = Request.QueryString.Get("C");

            // July 30, 2014 Jody asked to have the window stay not auto print and close, just stay open.
            string StopAutoExecute = Request.QueryString.Get("SAX");
            StopAutoExit.Value = "";
            if (StopAutoExecute != null && StopAutoExecute.ToUpper() == "Y") { StopAutoExit.Value = "Y"; }
            ////////////////////////////////////////////////////////////////////////////////////////////////



            decimal ReceiveDetailID = -1;
            if (decimal.TryParse(sReceiveDetailID, out ReceiveDetailID) == false) { ReceiveDetailID = -1; }

            decimal ReceiveDetailAuthorizationLogID = -1;
            if (decimal.TryParse(sReceiveDetailAuthorizationLogID, out ReceiveDetailAuthorizationLogID) == false) { ReceiveDetailAuthorizationLogID = -1; }

            if (ReceiveDetailID < 0 && ReceiveDetailAuthorizationLogID > 0)
            {
                ReceiveDetailAuthohrizationManager rdam = new ReceiveDetailAuthohrizationManager(User.Identity.Name);
                ReceiveDetailAuthorizationLog rda = rdam.GetAuthorizationLog(ReceiveDetailAuthorizationLogID);
                ReceiveDetailID = rda.ReceiveDetailID;
                //ScriptManager.RegisterStartupScript(this, GetType(), "LoadUnit", "alert('RDID=" + ReceiveDetailID.ToString() + "';", true);
            }


            Repair_01.Visible = false;
            Repair_02.Visible = false;
            PnlPackingSlip.Visible = false;
            if (ReceiveDetailID < 1) { return; }

            ReceiveDetailManager rdm = new ReceiveDetailManager(User.Identity.Name);
            ReceiveDetail rd = rdm.ReceiveDetailNoRestrict(ReceiveDetailID);
            if (rd != null)
            {
                ClientLocationManager clm = new ClientLocationManager(User.Identity.Name);
                Client cl = clm.GetClientThisLocation(ctx, rd.ClientLocationID);
                //ProjectManager pm = new ProjectManager(User.Identity.Name);

                //ProjectManager pm = new ProjectManager(User.Identity.Name);
                //decimal? pid = rd.ProjectID;
                //if (pid == null) { pid = -1; }
                //Project proj = pm.Get((decimal)rd.ProjectID);
                string TagName = "";
                //if (proj != null && proj.ProductTag != null && proj.ProductTag.Trim().Length > 0) { TagName = proj.ProductTag.ToUpper(); }
                //else if (cl != null && cl.ProductTag != null && cl.ProductTag.Trim().Length > 0) { TagName = cl.ProductTag.ToUpper(); }

                if (cl.RepairForm != null && cl.RepairForm.Trim().Length > 0) { TagName = cl.RepairForm.ToUpper(); }
                if (TagName.Trim().Length == 0) { TagName = "REPAIR_01"; }


                if (TagName == "REPAIR_01")
                {
                    if (sReport == "E") { LoadData_01(rdm, rd); }             // Estimate
                    if (sReport == "R") { LoadData_02(rdm, rd); }             // Repair
                    if (sReport == "P") { LoadData_03(rdm, rd); }             // PackingSlip
                }
                else if (TagName == "REPAIR_02")
                {
                    //PrintTag_02(RDID);
                }
                else if (TagName == "REPAIR_03")
                {
                    //PrintTag_03(RDID);
                }
                else if (TagName == "REPAIR_04")
                {
                    //PrintTag_04(RDID);
                }
                else if (TagName == "REPAIR_05")
                {
                    //PrintTag_05(RDID);
                }
                else { LoadData_01(rdm, rd); }   // Default
            }
            if (IsPostBack == false)
            {
                CompanyDemographics c = new CompanyDemographics(User.Identity.Name);
                lblcCityProvincePostal.Text = c.CityProvincePostal;
                lblcAddressLines.Text = c.AddressLines;
                lblcCompanyName.Text = c.Name.ToUpper();
                lblcPhone.Text = "Phone #: " + c.Phone;
                lblcFax.Text = "  Fax #: " + c.Fax;
                lblcWebsite.Text = c.Website;

                lblcCityProvincePostal_02.Text = c.CityProvincePostal;
                lblcAddressLines_02.Text = c.AddressLines;
                lblcCompanyName_02.Text = c.Name.ToUpper();
                lblcPhone_02.Text = "Phone #: " + c.Phone;
                lblcFax_02.Text = "  Fax #: " + c.Fax;
                lblcWebsite_02.Text = c.Website;

                lblcCityProvincePostal_03.Text = c.CityProvincePostal;
                lblcAddressLines_03.Text = c.AddressLines;
                lblcCompanyName_03.Text = c.Name.ToUpper();



            }
        }


        public void LoadData_01(ReceiveDetailManager rdm, ReceiveDetail rd)
        {
            Repair_01.Visible = true;
            if (rd != null)
            {
                ClientLocation cl = rdm.GetClientLocation(rd.ClientLocationID);
                bcBagtag.DataToEncode = rd.ESN.ToString();
                lblDealerName.Text = cl.CompanyName;
                lblDealerID.Text = cl.ScanKey;
                lblPhone.Text = cl.PhoneNumber;
                lblFax.Text = cl.FaxNumber;
                lblAddress.Text = cl.AddressLine1 + " " + cl.AddressLine2;
                lblCity.Text = cl.City;
                lblProvince.Text = cl.StateOrProvince;
                lblPostalCode.Text = cl.PostalCode;


                string b = "";
                string pathname = bcBagtag.ImageFileName;

                string Manufacturer = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Manufacturer");
                string Model = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Model");
                string Colour = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Colour");
                string Carrier = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Carrier");
                string NickName = rdm.GetCarrierMakeModelColour_NickName(Carrier, Manufacturer, Model, Colour);



                string WarantyType = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Warranty Type");

                b = pathname;

                //string DealerID = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "DealerID");
                DealerID_01.Visible = true;
                //DealerID_01.Text = "" + DealerID;

                DealerID_01.Text = cl.ScanKey.ToUpper();



                lblESN.Text = rd.ESN;
                lblRMA.Text = rd.RMANumber;
                //lblModel.Text = Model;
                ldlManufacturer.Text = Manufacturer + " " + Model + " (Nickname:" + NickName + ")";
                //lblNickname.Text = NickName;

                lblWarrantyType.Text = WarantyType;
                lblMSN.Visible = false;
                string sn = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Serial Number");
                if (sn.Trim().Length > 0)
                {
                    lblMSN.Visible = true;
                    lblMSN.Text = "MSN #:" + sn;
                }

                lblClientReference_01.Visible = false;
                lblClientReference.Visible = false;
                sn = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Client No");
                if (sn.Trim().Length > 0)
                {
                    lblClientReference_01.Visible = true;
                    lblClientReference_01.Text = "Customer Reference #:" + sn;
                }
                sn = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Client Reference #");
                if (sn.Trim().Length > 0)
                {
                    lblClientReference.Visible = true;
                    lblClientReference.Text = "Service Request #:" + sn;
                }


                //////////////////
                lblWorkPreformed.Visible = false;
                sn = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Work Preformed");
                if (sn.Trim().Length > 0)
                {
                    lblWorkPreformed.Visible = true;
                    lblWorkPreformed.Text = "Work Preformed: " + sn;
                }
                lblReplacementIMEI.Visible = false;
                bcBagtag_01ReplacementIMEI.Visible = false;
                lblReplacedWith_01.Visible = false;
                lblReplacedWith_01a.Visible = false;
                sn = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Replacement IMEI");
                if (sn.Trim().Length > 0)
                {
                    lblReplacementIMEI.Visible = true;
                    lblReplacedWith_01.Visible = true;
                    lblReplacedWith_01a.Visible = true;
                    lblReplacementIMEI.Text = "Replacement IMEI: " + sn;
                    bcBagtag.BarHeightCM = "0.5";
                    bcBagtag_01ReplacementIMEI.Visible = true;
                    bcBagtag_01ReplacementIMEI.DataToEncode = sn;

                }
                lblComponent.Visible = false;
                sn = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Component");
                if (sn.Trim().Length > 0)
                {
                    lblComponent.Visible = true;
                    lblComponent.Text = "Component: " + sn;
                }
                lblCosmetic.Visible = false;
                sn = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Cosmetic");
                if (sn.Trim().Length > 0)
                {
                    lblCosmetic.Visible = true;
                    lblCosmetic.Text = "Cosmetic Fault(s): " + sn;
                }
                //lblModule.Visible = false;
                //sn = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Module");
                //if (sn.Trim().Trim().Length > 0)
                //{
                //    lblModule.Visible = true;
                //    lblModule.Text = "Module: " + sn;
                //}


                lblRepairType.Visible = false;
                lblEstimateFee.Visible = false;
                lblSalesOrderFee.Visible = false;
                //lblDisclosure.Visible = true;
                //lblLegal.Visible = true;
                //lblLegal1.Visible = true;
                //lblLegal2.Visible = true;
                SignatureSection.Visible = true;
                lblEstimateFee.Text = "Estimate Fee:";

                sn = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Estimate");
                if (sn.Trim().Trim().Length >= 0)
                {
                    lblRepairType.Visible = true;
                    lblRepairType.Text = "Estimate";
                    lblEstimateFee.Visible = true;
                    lblEstimateFee.Text = "Estimate Fee: $" + sn;

                }

                sn = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Repair Fee");
                if (sn.Trim().Trim().Length > 0 && sn != "0.00")
                {
                    lblRepairType.Visible = true;
                    lblRepairType.Text = "Repair";
                    lblEstimateFee.Visible = true;
                    lblEstimateFee.Text = "Repair Fee: $" + sn;

                    //lblDisclosure.Visible = false;
                    //lblLegal.Visible = false;
                    //lblLegal1.Visible = false;
                    //lblLegal2.Visible = false;
                    SignatureSection.Visible = false;
                }
                lblSalesOrderFee.Text = "SO Fee:";
                sn = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Sales Order Fee");
                if (sn.Trim().Trim().Length > 0 && sn != "0.00")
                {
                    lblRepairType.Visible = true;
                    lblRepairType.Text = "Repair";
                    lblSalesOrderFee.Visible = true;
                    lblSalesOrderFee.Text = "SO Fee: $" + sn;

                    //lblDisclosure.Visible = false;
                    //lblLegal.Visible = false;
                    //lblLegal1.Visible = false;
                    //lblLegal2.Visible = false;
                    SignatureSection.Visible = false;
                }

                lblClaimStatement.Visible = false;
                sn = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Claim Reason");
                sn += " " + rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Claim Location");
                sn += " " + rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Claim Action");
                if (sn.Trim().Length > 0)
                {
                    lblClaimStatement.Visible = true;
                    lblClaimStatement.Text = sn;
                }

                //chkInWarranty.SelectedIndex = 1;
                //chkExtendedWarranty.SelectedIndex = 1;
                //chkOutOfWarranty.SelectedIndex = 1;

                //string rType = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Receipt Type");
                //if (rType.Trim().ToUpper() == "IN WARRANTY") { chkInWarranty.SelectedIndex = 0; }
                //if (rType.Trim().ToUpper() == "EXTENDED WARRANTY") { chkExtendedWarranty.SelectedIndex = 0; }
                //if (rType.Trim().ToUpper() == "OUT OF WARRANTY") { chkOutOfWarranty.SelectedIndex = 0; }

                string fCode1 = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Complaint");
                string fCode2 = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Complaint 2");
                // there is a discrepency between Sandbox and Production
                //if (fCode1.Trim().Length == 0) { fCode1 = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Fault"); }
                //if (fCode2.Trim().Length == 0) { fCode2 = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Second Fault"); }
                if (fCode1.ToUpper() == "NONE") { fCode1 = ""; }
                if (fCode2.ToUpper() == "NONE") { fCode2 = ""; }
                lblComplaintCodes.Text = fCode1;
                lblComplaintCodes_02.Text = fCode2;
                lblCustomerComment.Text = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Customer Notes");

                fCode1 = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Fault Code 1");
                fCode2 = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Fault Code 2");
                string fCode3 = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Fault Code 3");
                string fCode4 = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Fault Code 4");
                if (fCode1.ToUpper() == "NONE") { fCode1 = ""; }
                if (fCode2.ToUpper() == "NONE") { fCode2 = ""; }
                if (fCode3.ToUpper() == "NONE") { fCode3 = ""; }
                if (fCode4.ToUpper() == "NONE") { fCode4 = ""; }
                lblFaultCodes.Text = fCode1 + " / " + fCode2 + " / " + fCode3 + " / " + fCode4;

                fCode1 = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Fault Code 1");
                fCode2 = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Fault Code 2");
                fCode3 = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Fault Code 3");
                fCode4 = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Fault Code 4");
                if (fCode1.ToUpper() == "NONE") { fCode1 = ""; }
                if (fCode2.ToUpper() == "NONE") { fCode2 = ""; }
                if (fCode3.ToUpper() == "NONE") { fCode3 = ""; }
                if (fCode4.ToUpper() == "NONE") { fCode4 = ""; }
                lblFaultCodes2.Text = fCode1 + " / " + fCode2 + " / " + fCode3 + " / " + fCode4;






                lblLegal.Text = "";
                //chkApproved.Checked = false;
                //chkDenied.Checked = false;
                //if (rl.AuthorizedDate != null) { chkApproved.Checked = true; }
                //if (rl.DeclinedDate != null) { chkDenied.Checked = true; }

                //lblEstimateFee.Text = rl.EstimateFee.ToString();
                //lblFreightFee.Text = rl.FreightFee.ToString();
                //lblHST.Text = rl.HST.ToString();
                //lblTotal.Text = rl.Total.ToString();
                DivNote.InnerHtml = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Repair Notes");
                //lblNote.Text = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Repair Notes");

            }
        }

        public void LoadData_02(ReceiveDetailManager rdm, ReceiveDetail rd)
        {
            Repair_02.Visible = true;
            if (rd != null)
            {
                ClientLocation cl = rdm.GetClientLocation(rd.ClientLocationID);
                bcBagtag_02.DataToEncode = rd.ESN.ToString();
                lblDealerName_02.Text = cl.CompanyName;
                lblDealerID_02.Text = cl.ScanKey;
                lblPhone_02.Text = cl.PhoneNumber;
                lblFax_02.Text = cl.FaxNumber;
                lblAddress_02.Text = cl.AddressLine1 + " " + cl.AddressLine2;
                lblCity_02.Text = cl.City;
                lblProvince_02.Text = cl.StateOrProvince;
                lblPostalCode_02.Text = cl.PostalCode;


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


                //string DealerID = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "DealerID");
                DealerID_02.Visible = true;
                //DealerID_02.Text = "" + DealerID;
                DealerID_02.Text = cl.ScanKey.ToUpper();

                lblESN_02.Text = rd.ESN;

                lblRMA_02.Text = rd.RMANumber;
                //lblModel_02.Text = Model;
                ldlManufacturer_02.Text = Manufacturer + " " + Model + " (Nickname:" + NickName + ")";
                //lblNickname_02.Text = NickName;

                lblMSN_02.Visible = false;
                string sn = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Serial Number");
                if (sn.Trim().Length > 0)
                {
                    lblMSN_02.Visible = true;
                    lblMSN_02.Text = "MSN #:" + sn;
                }

                lblClientReference_02_02.Visible = false;
                lblClientReference_02.Visible = false;
                sn = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Client No");
                if (sn.Trim().Length > 0)
                {
                    lblClientReference_02_02.Visible = true;
                    lblClientReference_02_02.Text = "Customer Reference #:" + sn;
                }
                sn = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Client Reference #");
                if (sn.Trim().Length > 0)
                {
                    lblClientReference_02.Visible = true;
                    lblClientReference_02.Text = "Service Request #:" + sn;
                }


                //////////////////
                lblWorkPreformed_02.Visible = false;
                sn = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Work Preformed");
                if (sn.Trim().Length > 0)
                {
                    lblWorkPreformed_02.Visible = true;
                    lblWorkPreformed_02.Text = "Work Preformed: " + sn;
                }
                lblReplacementIMEI_02.Visible = false;
                bcBagtag_02ReplacementIMEI.Visible = false;
                lblReplacedWith_02.Visible = false;
                lblReplacedWith_02a.Visible = false;
                sn = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Replacement IMEI");
                if (sn.Trim().Length > 0)
                {
                    lblReplacementIMEI_02.Visible = true;
                    lblReplacedWith_02.Visible = true;
                    lblReplacedWith_02a.Visible = true;
                    lblReplacementIMEI_02.Text = "Replacement IMEI: " + sn;
                    bcBagtag_02ReplacementIMEI.Visible = true;
                    bcBagtag_02ReplacementIMEI.DataToEncode = sn;
                    bcBagtag_02.BarHeightCM = "0.5";
                }


                lblReplacementModel_02.Visible = false;
                sn = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Replacement Model #");
                if (sn.Trim().Length > 0)
                {
                    lblReplacementModel_02.Visible = true;
                    lblReplacementModel_02.Text = "Replacement Model: " + sn;
                }



                lblComponent_02.Visible = false;
                sn = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Component");
                if (sn.Trim().Length > 0)
                {
                    lblComponent_02.Visible = true;
                    lblComponent_02.Text = "Component: " + sn;
                }
                lblCosmetic_02.Visible = false;
                sn = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Cosmetic");
                if (sn.Trim().Length > 0)
                {
                    lblCosmetic_02.Visible = true;
                    lblCosmetic_02.Text = "Cosmetic Fault(s): " + sn;
                }
                //lblModule_02.Visible = false;
                //sn = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Module");
                //if (sn.Trim().Length > 0)
                //{
                //    lblModule_02.Visible = true;
                //    lblModule_02.Text = "Module: " + sn;
                //}


                lblRepairType_02.Visible = false;
                lblEstimateFee_02.Visible = false;
                lblSalesOrderFee_02.Visible = false;
                //lblDisclosure.Visible = true;
                //lblLegal.Visible = true;
                //lblLegal1.Visible = true;
                //lblLegal2.Visible = true;
                SignatureSection_02.Visible = true;
                lblEstimateFee_02.Text = "Estimate Fee:";
                sn = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Estimate");
                if (sn.Trim().Length >= 0)
                {
                    lblRepairType_02.Visible = true;
                    lblRepairType_02.Text = "Estimate";
                    lblEstimateFee_02.Visible = true;
                    lblEstimateFee_02.Text = "Estimate Fee: $" + sn;

                }

                sn = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Repair Fee");
                if (sn.Trim().Length > 0 && sn != "0.00")
                {
                    lblRepairType_02.Visible = true;
                    lblRepairType_02.Text = "Repair";
                    lblEstimateFee_02.Visible = true;
                    lblEstimateFee_02.Text = "Repair Fee: $" + sn;

                    //lblDisclosure.Visible = false;
                    //lblLegal.Visible = false;
                    //lblLegal1.Visible = false;
                    //lblLegal2.Visible = false;
                    SignatureSection_02.Visible = false;
                }

                lblSalesOrderFee_02.Text = "SO Fee:";
                sn = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Sales Order Fee");
                if (sn.Trim().Length > 0 && sn != "0.00")
                {
                    lblRepairType.Visible = true;
                    lblRepairType.Text = "Repair";
                    lblSalesOrderFee.Visible = true;
                    lblSalesOrderFee.Text = "SO Fee: $" + sn;

                    //lblDisclosure.Visible = false;
                    //lblLegal.Visible = false;
                    //lblLegal1.Visible = false;
                    //lblLegal2.Visible = false;
                    SignatureSection.Visible = false;
                }
                lblClaimStatement_02.Visible = false;
                sn = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Claim Reason");
                sn += " " + rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Claim Location");
                sn += " " + rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Claim Action");
                if (sn.Trim().Length > 0)
                {
                    lblClaimStatement_02.Visible = true;
                    lblClaimStatement_02.Text = sn;
                }

                chkInWarranty_02.SelectedIndex = 0;
                chkExtendedWarranty_02.SelectedIndex = 0;
                chkOutOfWarranty_02.SelectedIndex = 0;

                string rType = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Receipt Type");
                if (rType.ToUpper() == "IN WARRANTY") { chkInWarranty_02.SelectedIndex = 0; }
                if (rType.ToUpper() == "EXTENDED WARRANTY") { chkExtendedWarranty_02.SelectedIndex = 0; }
                if (rType.ToUpper() == "OUT OF WARRANTY") { chkOutOfWarranty_02.SelectedIndex = 0; }

                string fCode1 = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Complaint");
                string fCode2 = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Complaint 2");
                // there is a discrepency between Sandbox and Production
                //if (fCode1.Trim().Length == 0) { fCode1 = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Fault"); }
                //if (fCode2.Trim().Length == 0) { fCode2 = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Second Fault"); }
                if (fCode1.ToUpper() == "NONE") { fCode1 = ""; }
                if (fCode2.ToUpper() == "NONE") { fCode2 = ""; }
                lblComplaintCodes_02.Text = fCode1;
                lblComplaintCodes_02_02.Text = fCode2;
                lblCustomerComment_02.Text = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Customer Notes");

                fCode1 = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Fault Code 1");
                fCode2 = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Fault Code 2");
                string fCode3 = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Fault Code 3");
                string fCode4 = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Fault Code 4");


                if (fCode1.ToUpper() == "NONE") { fCode1 = ""; }
                if (fCode2.ToUpper() == "NONE") { fCode2 = ""; }
                if (fCode3.ToUpper() == "NONE") { fCode3 = ""; }
                if (fCode4.ToUpper() == "NONE") { fCode4 = ""; }
                lblFaultCodes_02.Text = fCode1 + " / " + fCode2 + " / " + fCode3 + " / " + fCode4;

                fCode1 = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Fault Code 1");
                fCode2 = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Fault Code 2");
                fCode3 = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Fault Code 3");
                fCode4 = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Fault Code 4");
                if (fCode1.ToUpper() == "NONE") { fCode1 = ""; }
                if (fCode2.ToUpper() == "NONE") { fCode2 = ""; }
                if (fCode3.ToUpper() == "NONE") { fCode3 = ""; }
                if (fCode4.ToUpper() == "NONE") { fCode4 = ""; }
                lblFaultCodes_022.Text = fCode1 + " / " + fCode2 + " / " + fCode3 + " / " + fCode4;


                lblLegal_02.Text = "";
                //chkApproved.Checked = false;
                //chkDenied.Checked = false;
                //if (rl.AuthorizedDate != null) { chkApproved.Checked = true; }
                //if (rl.DeclinedDate != null) { chkDenied.Checked = true; }

                //lblEstimateFee.Text = rl.EstimateFee.ToString();
                //lblFreightFee.Text = rl.FreightFee.ToString();
                //lblHST.Text = rl.HST.ToString();
                //lblTotal.Text = rl.Total.ToString();
                DivNote_02.InnerHtml = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Repair Notes");
                //lblNote.Text = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Repair Notes");

            }
        }

        public void LoadData_03(ReceiveDetailManager rdm, ReceiveDetail rd)
        {
            PnlPackingSlip.Visible = true;
            if (rd != null)
            {
                ClientLocation cl = rdm.GetClientLocation(rd.ClientLocationID);
                //bcBagtag_02.DataToEncode = rd.ESN.ToString();
                //lblDealerName_02.Text = cl.CompanyName;
                //lblDealerID_02.Text = cl.ScanKey;


                string sn = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "MSC Sent To");
                lblShipTo.Text = sn;
                sn = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "SWO No");
                lblWorkOrder.Text = sn;

                //sn = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "SWO #");
                lblCustRef.Text = cl.ScanKey;
                sn = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "SKU") + " / " + rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Receipt Type");
                lblRMA_03.Text = sn;
                sn = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Client Reference #");
                lblWarranty.Text = sn;

                string Manufacturer = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Manufacturer");
                string Model = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Model");
                string Colour = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Colour");
                string Carrier = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Carrier");
                lblMMCC.Text = Manufacturer + " " + Model + " " + Colour + " " + Carrier;
                lblESN_03.Text = rd.ESN;
                sn = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Parts Sent Out");
                lblParts.Text = sn;

                sn = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Customer Notes");
                lblComplaint_03.Text = sn;
                sn = rdm.GetReceiveDetailItem_DataElement(rd.ReceiveDetailID, "Repair Notes");
                lblTechComments.Text = sn;
            }
        }




    }
}