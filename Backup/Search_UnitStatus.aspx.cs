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
    public partial class Search_UnitStatus : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string ESN = Request.QueryString.Get("IMEI");
            btnSearch.Click += new EventHandler(btnSearch_Click);
            if (IsPostBack == false)
            {
                SearchData.Visible = false;
                txtESNSearch.Focus();
            }

            if (ESN != null && ESN.Length > 0)
            {
                SearchData.Visible = true;
                GetUnitStatusUpdate(ESN, User.Identity.Name);
                txtESNSearch.Focus();
            }
        }

        void btnSearch_Click(object sender, EventArgs e)
        {
            SearchData.Visible = true;
            GetUnitStatusUpdate(txtESNSearch.Text, User.Identity.Name);
            txtESNSearch.Focus();
        }

        public void ClearFields()
        {
            lblRepairFee.Text = "Estimate:";
            txtServiceRequestNumber.Text = "";
            txtESN.Text = "";
            txtOriginalIMEI.Text = "";
            txtWarrantyType.Text = "";
            txtFaultCode.Text = "";
            txtDateSubmitted.Text = "";
            txtFaultCode2.Text = "";
            txtGMPReceivedDate.Text = "";
            txtRepairDate.Text = "";
            txtCurrentProcess.Text = "";

            txtRepairFee.Text = "";
            txtRepairNotes.Text = "";
            //txtStoreComments.Text = "";

            txtAssessment.Text = "";
            txtGMPMSCShippedDate.Text = "";
            txtOutBoundWayBill_S.Text = "";
            txtCourier.Text = "";

            txtServiceRequestNumber.Text = "";

            txtOriginalIMEI.Text = "";
            txtWarrantyType.Text = "";
            txtFaultCode.Text = "";
            txtDateSubmitted.Text = "";
            txtFaultCode2.Text = "";
            txtGMPReceivedDate.Text = "";
            txtRepairDate.Text = "";
            txtCurrentProcess.Text = "";

            //txtRepairFee.Text = "";
            //txtRepairNotes.Text = "";
            //txtStoreComments.Text = "";

            txtAssessment.Text = "";
            txtGMPMSCShippedDate.Text = "";
            txtOutBoundWayBill_S.Text = "";
            txtCourier.Text = "";
        }


        public void GetUnitStatusUpdate(string ESN, string UserName)
        {
            bool WasSwitched = false;
            string OriginalESN = ESN;
            ClearFields();
            lblPath.Text = "";
            decimal ReceiveDetailID = -1;
            string RepairFee = "";
            decimal nRepairFee = 0;
            ReceiveDetailManager rdm = new ReceiveDetailManager(UserName);
            using (clsLinqDataContext ctx = rdm.GetDataContext(UserName))
            {
                GridDashboardReceiveDetail_Client GDRD = null;

                string NewESN = rdm.GetSwitchedifSwappedIMEI(ctx, ESN);
                if (NewESN != ESN) { lblPath.Text = " Switched from (" + ESN + ") to (" + NewESN + ")"; ESN = NewESN; WasSwitched = true; }
                ReceiveDetail rd = rdm.ReceiveDetail(ctx, ESN);
                //ReceiveDetail rd = rdm.ReceiveDetail(ctx, ReceiveDetailID);
                if (rd == null)
                {
                    rd = ctx.ReceiveDetails.Where(x => x.ESN == ESN && x.ReceiveDetailStatus.Status.ToUpper() != "GRAVEYARD").OrderBy(y => y.Version).Select(y => y).FirstOrDefault();
                    if (rd == null)
                    {
                        // look to see if sitting in PreReceive
                        ReceiveDetailPreReceive pr = ctx.ReceiveDetailPreReceives.Where(x => x.ESN == ESN && x.Status == "Open").FirstOrDefault();
                        if (pr != null)
                        {
                            txtServiceRequestNumber.Text = "";
                            txtCurrentProcess.Text = "";
                            txtDateSubmitted.Text = "";
                            Option sku = ctx.Options.Where(y=> y.Question.Name.ToUpper()== "SKU").FirstOrDefault();
                            if (sku != null)
                            {
                                ReceiveDetailPreReceiveAttribute pra = pr.ReceiveDetailPreReceiveAttributes.Where(x => x.OptionID == sku.OptionID).FirstOrDefault();
                                if (pra != null)
                                {
                                    txtServiceRequestNumber.Text = pra.Value;
                                }
                            }
                            txtDateSubmitted.Text = string.Format("{0:MM/dd/yyyy}", pr.CreateDate); 
                            txtCurrentProcess.Text = "Pre-Receive";
                            txtESN.Text = ESN;
                        }
                        else
                        {
                            txtESN.Text = "Not found";
                        }
                        return;
                    }
                }

                ReceiveDetailID = rd.ReceiveDetailID;
                txtESN.Text = rd.ESN + ":" + rd.Version;

                decimal ProjectClientPortalID = ctx.Projects.Where(x => x.Name.ToUpper() == "CLIENT PORTAL").Select(x => x.ProjectID).FirstOrDefault();
                decimal ProjectClientRepairID = ctx.Projects.Where(x => x.Name.ToUpper() == "CLIENT REPAIR").Select(x => x.ProjectID).FirstOrDefault();

                try
                {
                    GDRD = new GridDashboardReceiveDetail_Client(ctx, rd, ProjectClientPortalID, ProjectClientRepairID);
                    if (GDRD == null || GDRD.GMPIReceivedRD == null || GDRD.ClientSubmittedRD == null)
                    {
                        //lblPath.Text = "No Client Submitted Record:" + ReceiveDetailID.ToString();
                        txtGMPReceivedDate.Text = GDRD.ReceiveDate;
                        txtServiceRequestNumber.Text = rdm.GetReceiveDetailItem_DataElement(ctx, ReceiveDetailID, "Service Request Num");
                        txtWarrantyType.Text = rdm.GetReceiveDetailItem_DataElement(ctx, ReceiveDetailID, "Warranty Type");
                        txtDateSubmitted.Text = GDRD.Dealer_ShipDate;

                        if (WasSwitched == true) { txtOriginalIMEI.Text = OriginalESN; }
                        else { txtOriginalIMEI.Text = rdm.GetReceiveDetailItem_DataElement(ctx, ReceiveDetailID, "Original IMEI"); }


                        //txtStoreComments.Text = rdm.GetReceiveDetailItem_DataElement(ctx, ReceiveDetailID, "Store Comments");

                        txtCurrentProcess.Text = rdm.GetReceiveDetailCurrentProcessNameFriendly(ctx, ReceiveDetailID);
                        txtAssessment.Text = rdm.GetReceiveDetailItem_DataElement(ctx, ReceiveDetailID, "Unit Assessment");

                        txtFaultCode.Text = rdm.GetReceiveDetailItem_DataElement(ctx, ReceiveDetailID, "Complaint");
                        txtFaultCode2.Text = rdm.GetReceiveDetailItem_DataElement(ctx, ReceiveDetailID, "Complaint 2");
                        txtGMPMSCShippedDate.Text = rdm.GetReceiveDetailItem_DataElement(ctx, ReceiveDetailID, "Shipping_Created");
                        txtRepairNotes.Text = rdm.GetReceiveDetailItem_DataElement(ctx, ReceiveDetailID, "Repair Notes");

                        txtRepairDate.Text = rdm.GetReceiveDetailItem_DataElement(ctx, ReceiveDetailID, "Repair Date");

                        if (WasSwitched == true)
                        {
                            txtOutBoundWayBill_S.Text = "Please reference RQ4";
                            txtCourier.Text = "Please reference RQ4";
                        }
                        else
                        {
                            txtOutBoundWayBill_S.Text = rdm.GetReceiveDetailItem_DataElement(ctx, ReceiveDetailID, "Out-Bound Waybill-S");
                            txtCourier.Text = rdm.GetReceiveDetailItem_DataElement(ctx, ReceiveDetailID, "Courier Out");
                        }

                        //string auth = rdm.GetReceiveDetailItem_DataElement(ctx, ReceiveDetailID, "Authorization");
                        //if (auth.ToUpper() == "APPROVAL REQUIRED") { txtCurrentProcess.Text = "Approval Required"; }
                        string auth = rdm.GetReceiveDetailItem_DataElement(ctx, ReceiveDetailID, "Authorization Status");
                        if (auth.ToUpper() == "REQUIRED") { txtCurrentProcess.Text = "Approval Required"; }


                        if (rdm.HasESNPassedThroughThisProcess(ctx, ReceiveDetailID, "Lab Billing") == true)
                        {
                            lblRepairFee.Text = "Repair Fee";
                            RepairFee = rdm.GetReceiveDetailItem_DataElement(ctx, ReceiveDetailID, "Repair Fee");
                            nRepairFee = 0;
                            if (decimal.TryParse(RepairFee, out nRepairFee) == false) { nRepairFee = 0; }
                            txtRepairFee.Text = nRepairFee.ToString();
                        }
                        else
                        {
                            RepairFee = rdm.GetReceiveDetailItem_DataElement(ctx, ReceiveDetailID, "Estimate");
                            nRepairFee = 0;
                            if (decimal.TryParse(RepairFee, out nRepairFee) == false) { nRepairFee = 0; }
                            txtRepairFee.Text = nRepairFee.ToString();
                        }


                    }
                    else
                    {
                        //lblPath.Text = "YES, Client Submitted Record:" + GDRD.ClientSubmittedRD.ReceiveDetailID.ToString() + ":" + GDRD.GMPIReceivedRD.ReceiveDetailID.ToString();
                        txtGMPReceivedDate.Text = GDRD.ReceiveDate;
                        txtServiceRequestNumber.Text = rdm.GetReceiveDetailItem_DataElement(ctx, GDRD.ClientSubmittedRD.ReceiveDetailID, "Service Request Num");
                        txtWarrantyType.Text = rdm.GetReceiveDetailItem_DataElement(ctx, GDRD.ClientSubmittedRD.ReceiveDetailID, "Warranty Type");
                        txtDateSubmitted.Text = GDRD.Dealer_ShipDate;

                        if (WasSwitched == true) { txtOriginalIMEI.Text = OriginalESN; }
                        else { txtOriginalIMEI.Text = rdm.GetReceiveDetailItem_DataElement(ctx, GDRD.ClientSubmittedRD.ReceiveDetailID, "Original IMEI"); }

                        //txtStoreComments.Text = rdm.GetReceiveDetailItem_DataElement(ctx, GDRD.GMPIReceivedRD.ReceiveDetailID, "Store Comments");
                        txtAssessment.Text = rdm.GetReceiveDetailItem_DataElement(ctx, GDRD.GMPIReceivedRD.ReceiveDetailID, "Unit Assessment");
                        txtCurrentProcess.Text = rdm.GetReceiveDetailCurrentProcessNameFriendly(ctx, GDRD.GMPIReceivedRD.ReceiveDetailID);

                        txtFaultCode.Text = rdm.GetReceiveDetailItem_DataElement(ctx, GDRD.GMPIReceivedRD.ReceiveDetailID, "Complaint");
                        txtFaultCode2.Text = rdm.GetReceiveDetailItem_DataElement(ctx, GDRD.GMPIReceivedRD.ReceiveDetailID, "Complaint 2");
                        txtGMPMSCShippedDate.Text = rdm.GetReceiveDetailItem_DataElement(ctx, GDRD.GMPIReceivedRD.ReceiveDetailID, "Shipping_Created");
                        txtRepairNotes.Text = rdm.GetReceiveDetailItem_DataElement(ctx, GDRD.GMPIReceivedRD.ReceiveDetailID, "Repair Notes");
                        txtRepairDate.Text = rdm.GetReceiveDetailItem_DataElement(ctx, GDRD.GMPIReceivedRD.ReceiveDetailID, "Repair Date");


                        if (WasSwitched == true)
                        {
                            txtOutBoundWayBill_S.Text = "Please reference RQ4";
                            txtCourier.Text = "Please reference RQ4";
                        }
                        else
                        {
                            txtOutBoundWayBill_S.Text = rdm.GetReceiveDetailItem_DataElement(ctx, GDRD.GMPIReceivedRD.ReceiveDetailID, "Out-Bound Waybill-S");
                            txtCourier.Text = rdm.GetReceiveDetailItem_DataElement(ctx, GDRD.GMPIReceivedRD.ReceiveDetailID, "Courier Out");
                        }

                        //string auth = rdm.GetReceiveDetailItem_DataElement(ctx, GDRD.GMPIReceivedRD.ReceiveDetailID, "Authorization");
                        //if (auth.ToUpper() == "APPROVAL REQUIRED") { txtCurrentProcess.Text = "Approval Required"; }

                        string auth = rdm.GetReceiveDetailItem_DataElement(ctx, GDRD.GMPIReceivedRD.ReceiveDetailID, "Authorization Status");
                        //lblPath.Text = "YES, Client Submitted Record:" + GDRD.ClientSubmittedRD.ReceiveDetailID.ToString() + ":" + GDRD.GMPIReceivedRD.ReceiveDetailID.ToString() + ":" + auth;
                        if (auth.ToUpper() == "REQUIRED") { txtCurrentProcess.Text = "Approval Required"; }


                        if (rdm.HasESNPassedThroughThisProcess(ctx, GDRD.GMPIReceivedRD.ReceiveDetailID, "Lab Billing") == true)
                        {
                            lblRepairFee.Text = "Repair Fee";
                            // If repair fee < = 0 Do Below
                            RepairFee = rdm.GetReceiveDetailItem_DataElement(ctx, GDRD.GMPIReceivedRD.ReceiveDetailID, "Repair Fee");
                            nRepairFee = 0;
                            if (decimal.TryParse(RepairFee, out nRepairFee) == false) { nRepairFee = 0; }
                            txtRepairFee.Text = nRepairFee.ToString();
                        }
                        else
                        {
                            RepairFee = rdm.GetReceiveDetailItem_DataElement(ctx, GDRD.GMPIReceivedRD.ReceiveDetailID, "Estimate");
                            nRepairFee = 0;
                            if (decimal.TryParse(RepairFee, out nRepairFee) == false) { nRepairFee = 0; }
                            txtRepairFee.Text = nRepairFee.ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    ErrorLine.Visible = true;
                    txtMiscError.Text = ex.Message;
                }
                return;



                #region Misc
                

                #endregion
            }
        }

    }
}