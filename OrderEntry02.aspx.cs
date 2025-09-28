using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using BW_WebApp.Classes;
//using ScanKey;
//using Factory_DataModel;

using BW_WebApp.DataManagers;
using Syncfusion.Web.UI.WebControls.Shared;


namespace BW_WebApp
{
    public partial class OrderEntry02 : System.Web.UI.Page
    {
        private string blank = "&nbsp;";
        clsLinqDataContext ctx = new clsLinqDataContext();
        TimeLogManager log;
        //clsLog log;

        private string[] OrderDetailGridcols = { "btnEdit", "QTY", "UnitPrice", "QTYPacked", "QTYInventoryLinked", "SKU", "Desc_Code", "Desc_Text", "OrderDetailID", "chkIsDeleted", "btnPack", "Manufacturer", "Model", "Colour", "Grade", "Carrier", "UnitPrice" };


        protected void Page_Load(object sender, EventArgs e)
        {
            //log = new clsLog(Server.MapPath("~"), "WebServer_01_Log.txt", User.Identity.Name, System.Configuration.ConfigurationManager.AppSettings["WriteLogUserNameToLog"]);
            log = new TimeLogManager(User.Identity.Name, "");
            //if (System.Configuration.ConfigurationManager.AppSettings["WriteLog"].ToUpper() == "TRUE")
            //{
            //    log.writeLogData = true;
            //}
            log.LogIt("**** Order Entry Screen Page Load Event started");

            hdnUserName.Value = User.Identity.Name;
            hdnCarrierID.Value = drpCarrier.ClientID;
            hdnManufacturerID.Value = drpManufacturer.ClientID;
            hdnModelID.Value = drpModel.ClientID;
            hdnColourID.Value = drpColour.ClientID;
            hdnGradeID.Value = drpGrade.ClientID;
            btnPanelMove.Click += new EventHandler(btnPanelMove_Click);
            GridView2.RowDataBound += new GridViewRowEventHandler(GridView2_RowDataBound);
            GridView2.RowCommand += new GridViewCommandEventHandler(GridView2_RowCommand);

            grdNewOrderDetailGrid.RowDataBound += new GridViewRowEventHandler(grdNewOrderDetailGrid_RowDataBound);
            grdNewOrderDetailGrid.RowCommand += new GridViewCommandEventHandler(grdNewOrderDetailGrid_RowCommand);

            btnFromBin.Click += new EventHandler(btnFromBin_Click);
            //btnFromBin_B.Click += new EventHandler(btnFromBin_B_Click);
            //btnFromAvailableStock.Click += new EventHandler(btnFromAvailableStock_Click);
            btnAuthorize.Click += new EventHandler(btnAuthorize_Click);

            btnNew.Click += new EventHandler(btnNew_Click);
            btnNewOK.Click += new EventHandler(btnNewOK_Click);
            btnNewCancel.Click += new EventHandler(btnNewCancel_Click);
            btnAddDetailLine.Click += new EventHandler(btnAddDetailLine_Click);
            AddDetailCancel.Click += new EventHandler(AddDetailCancel_Click);
            AddDetailOK.Click += new EventHandler(AddDetailOK_Click);
            //btnAddPostInventory.Click += new EventHandler(btnAddPostInventory_Click);
            btnPostInventory.Click += new EventHandler(btnPostInventory_Click);
            btnPostInventory_NoLock.Click += new EventHandler(btnPostInventory_NoLock_Click);
            btnPostBulkInventory.Click += new EventHandler(btnPostBulkInventory_Click);

            tabMain.ActiveTabChanged += new EventHandler(tabMain_ActiveTabChanged);

            btnBillClientSearch.Click += new EventHandler(btnBillClientSearch_Click);
            btnShipClientSearch.Click += new EventHandler(btnShipClientSearch_Click);
            btnBillClientEdit.Click += new EventHandler(btnBillClientEdit_Click);
            btnShipClientEdit.Click += new EventHandler(btnShipClientEdit_Click);
            drpProjectList_New.SelectedIndexChanged += new EventHandler(drpProjectList_New_SelectedIndexChanged);

            btnEditNameAddressOK.Click += new EventHandler(btnEditNameAddressOK_Click);
            btnEditNameAddressCancel.Click += new EventHandler(btnEditNameAddressCancel_Click);

            //btnDetailRecieve_OK.OnClientClick = "DisableButton('" + btnDetailRecieve_OK.ClientID + "');";
            hdnIsDisabled.Value = "No";
            btnDetailRecieve_Cancel.Click += new EventHandler(btnDetailRecieve_Cancel_Click);
            btnDetailRecieve_OK.Click += new EventHandler(btnDetailRecieve_OK_Click);
            btnDetailRecieve_OK.Enabled = true;


            btnSearch_Order.Click += new EventHandler(btnSearch_Click);
            btnEmail_Details.Click += new EventHandler(btnEmail_Details_Click);
            grdTempDetail.RowDataBound += new GridViewRowEventHandler(grdTempDetail_RowDataBound);
            grdTempDetail.RowCommand += new GridViewCommandEventHandler(grdTempDetail_RowCommand);



            if (!IsPostBack)
            {
                RestrictFunctions();

                OrderStartDate.Text = string.Format("{0:MM/dd/yyyy}", DateTime.Now);
                OrderEndDate.Text = string.Format("{0:MM/dd/yyyy}", DateTime.Now);

                string zID = Request.QueryString.Get("ID");
                string zStatus = Request.QueryString.Get("Status");
                //string zPName = Request.QueryString.Get("PName");
                if (zStatus != null)
                {
                    foreach (AjaxControlToolkit.TabPanel c in tabMain.Tabs)
                    {
                        if (c.HeaderText.ToUpper() == zStatus.ToUpper())
                        {
                            tabMain.ActiveTab = c; break;
                        }

                    }
                }
                ///////////////////////////////////////////////////////////////////////
                this.winGetPassword.Modal = true;
                this.winGetPassword.RightToLeft = RightToLeft.No;
                this.winGetPassword.BackColor = Color.FromName("Red");
                this.winGetPassword.Height = 250;
                this.winGetPassword.Width = 300;
                this.winGetPassword.ResizeMode = WindowResizeModeType.FreeStyle;
                ///////////////////////////////////////////////////////////////////////
                ///////////////////////////////////////////////////////////////////////
                this.wndSelectClientLocation.Modal = true;
                this.wndSelectClientLocation.RightToLeft = RightToLeft.No;
                this.wndSelectClientLocation.BackColor = Color.FromName("Red");
                this.wndSelectClientLocation.Height = 550;
                this.wndSelectClientLocation.Width = 500;
                this.wndSelectClientLocation.ResizeMode = WindowResizeModeType.FreeStyle;
                ///////////////////////////////////////////////////////////////////////

                pnlmainentry.Visible = true;
                pnlNew.Visible = false;
                pnlNewDetailLine.Visible = false;
                pnlEditNameAddress.Visible = false;
                pnlPackDetail.Visible = false;
                Trace.Write("Not Postback - Begin");

                BasicUserUtilities buu = new BasicUserUtilities(User.Identity.Name);

                //if (User.IsInRole("Orderer") == true || User.IsInRole("Administrators") || User.IsInRole("Supervisors")) { TabPanelNew.Visible = true; TabPanelDone.Visible = true; TabPanelArchive.Visible = true; }
                //if (User.IsInRole("Picker") == true || User.IsInRole("Administrators") || User.IsInRole("Supervisors")) { TabPanelPick.Visible = true; TabPanelDone.Visible = true; TabPanelArchive.Visible = true; }
                //if (User.IsInRole("Shipper") == true || User.IsInRole("Administrators") || User.IsInRole("Supervisors")) { TabPanelShip.Visible = true; TabPanelDone.Visible = true; TabPanelArchive.Visible = true; }
                //if (User.IsInRole("Biller") == true || User.IsInRole("Administrators") || User.IsInRole("Supervisors")) { TabPanelBill.Visible = true; TabPanelDone.Visible = true; TabPanelArchive.Visible = true; }


                if (User.IsInRole("Orderer") == true || User.IsInRole("Administrators") || User.IsInRole("Supervisors")) { TabPanelNew.Visible = true; TabPanelSearch.Visible = true; }
                if (User.IsInRole("Picker") == true || User.IsInRole("Administrators") || User.IsInRole("Supervisors")) { TabPanelPick.Visible = true; TabPanelSearch.Visible = true; }
                if (User.IsInRole("Shipper") == true || User.IsInRole("Administrators") || User.IsInRole("Supervisors")) { TabPanelShip.Visible = true; TabPanelSearch.Visible = true; }
                //if (User.IsInRole("Biller") == true || User.IsInRole("Administrators") || User.IsInRole("Supervisors")) { TabPanelBill.Visible = true; TabPanelDone.Visible = true; TabPanelArchive.Visible = true; }


                ProjectManager pm = new ProjectManager(User.Identity.Name);
                List<Project> LP = pm.GetMasterProjectList(ctx);
                drpProjectList_New.Items.Clear();
                drpProjectList_New.Items.Add(new ListItem("All", "-1"));
                foreach (Project o in LP)
                {
                    ListItem li = new ListItem(o.Name, o.ProjectID.ToString());
                    drpProjectList_New.Items.Add(li);
                }
                drpProjectList_New.SelectedIndex = 0;
                ResetCarrierManufacturerModelColourGrade();
                LoadOrderData();


                OrderManager om = new OrderManager(User.Identity.Name);
                List<OrderStatus> os = om.GetStatusList();
                drpStatus.Items.Clear();
                ListItem z = new ListItem("All", "-1");
                drpStatus.Items.Add(z);
                foreach (OrderStatus o in os)
                {
                    ListItem x = new ListItem(o.Status, o.OrderStatusID.ToString());
                    drpStatus.Items.Add(x);
                }

                drpMoveList.Items.Clear();
                foreach (OrderStatus o in os)
                {
                    ListItem x = new ListItem(o.Status, o.OrderStatusID.ToString());
                    drpMoveList.Items.Add(x);
                }
                //drpMoveList.Items.Clear();
                //foreach (OrderStatus o in os)
                //{
                //    ListItem x = new ListItem(o.Status, o.OrderStatusID.ToString());
                //    drpMoveList.Items.Add(x);
                //}




                if (zID != null)
                {
                    decimal id = -1;
                    if (decimal.TryParse(zID, out id) == false) { id = -1; }
                    if (id > 0) { EditOrder(id); }
                }

                Trace.Write("Not Postback - Done");
            }
            //txtSku.Focus();
            ScanKey.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13) || (event.which == 9) || (event.keyCode == 9)) {RecordScanKey();return false;}} else {return true}; ");
            ScanKey.Attributes.Add("onblur", "RecordScanKey();return false;");
        }

        void btnAuthorize_Click(object sender, EventArgs e)
        {
            //Membership nm = Membership();
            if ((txtFromBin.Text.Length > 0) && Membership.ValidateUser(txtAuthUserName.Text, CurrentPassword.Text))
            {
                MembershipUser mUser = Membership.GetUser(txtAuthUserName.Text);
                if (Roles.IsUserInRole(txtAuthUserName.Text, "BinAutoShip") == true)
                {
                    // Save the order
                    SaveData();
                    IMEIOrderUploadProcessor processor = new IMEIOrderUploadProcessor(txtFromBin.Text, User.Identity.Name, lblPurchaseOrderNumber.Text);
                    // JIM ERROR HERE processor.LoadIMEIData_FromBinNumber();
                    //pnlmainentry.Visible = true;
                    //pnlNew.Visible = false;
                    //// Reload the order
                    decimal id = -1;
                    if (decimal.TryParse(hdnOrderHeaderID.Value, out id) == false) { id = -1; }
                    EditOrder(id);
                    //ScriptManager.RegisterStartupScript(this, GetType(), "RunReport", "alert('authorized!" + User.Identity.Name + "');", true);
                    return;
                }
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "RunReport", "alert('User Not Authorized!');", true);
        }


        #region Order Entry Search
        void grdTempDetail_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            System.Web.UI.WebControls.ImageButton btnOpen = (System.Web.UI.WebControls.ImageButton)e.CommandSource;
            string id = "-1";
            string Status = "";
            //string processName = "";
            string CommandArgument = btnOpen.CommandArgument;
            string[] data = CommandArgument.Split(',');
            id = data[0];
            Status = data[1];
            if (btnOpen.ID == "imgOpen")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Open Unit", "OpenOrder(" + id + ",'" + Status + "');", true);
            }
            if (btnOpen.ID == "imgPrint")
            {
                switch (Status.ToUpper())
                {
                    case "NEW":
                    case "PICK/PACK":
                        ScriptManager.RegisterStartupScript(this, GetType(), "RunReport", "ExportPickPackingShipReport('PIC'," + id + ");", true);
                        break;
                    case "SHIP":
                        ScriptManager.RegisterStartupScript(this, GetType(), "RunReport", "ExportPickPackingShipReport('PAC'," + id + ");", true);
                        break;
                    case "BILL":
                        ScriptManager.RegisterStartupScript(this, GetType(), "RunReport", "ExportPickPackingShipReport('SHIP'," + id + ");", true);
                        break;
                    case "DONE":
                    case "ARCHIVE":
                        ScriptManager.RegisterStartupScript(this, GetType(), "RunReport", "ExportPickPackingShipReport('DON'," + id + ");", true);
                        break;

                    default:
                        ScriptManager.RegisterStartupScript(this, GetType(), "RunReport", "ExportPickPackingShipReport('MIS'," + id + ");", true);
                        break;
                }
            }

        }
        void grdTempDetail_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Type type = e.Row.DataItem.GetType();               //.GetGenericArguments()[0]; 
                GetOrderEntryList_TemplateRawData_SearchGrid_01Result Data = ((GetOrderEntryList_TemplateRawData_SearchGrid_01Result)e.Row.DataItem);
                System.Web.UI.WebControls.ImageButton bOpen = (System.Web.UI.WebControls.ImageButton)e.Row.FindControl("imgOpen");
                if (bOpen != null)
                {
                    bOpen.CommandArgument = Data.OrderHeaderID.ToString() + "," + Data.Status;
                }
                System.Web.UI.WebControls.ImageButton bPrint = (System.Web.UI.WebControls.ImageButton)e.Row.FindControl("imgPrint");
                if (bPrint != null)
                {
                    //ImageButton bPrint = (ImageButton)e.Row.FindControl("imgPrint");
                    //if (bPrint != null)
                    //{
                    //    bPrint.Attributes.Add("onclick", "PrintScanCodes('" + ((Project)e.Row.DataItem).ProjectID + "', '" + ((Project)e.Row.DataItem).Name + "'); return false;");
                    //}
                    bPrint.CommandArgument = Data.OrderHeaderID.ToString() + "," + Data.Status;
                }
            }
        }

        void btnEmail_Details_Click(object sender, EventArgs e)
        {

            if (txtEmailResultsAddress.Text.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "RunReport", "alert('No email address supplied');", true);
                return;
            }

            string[] url = HttpContext.Current.Request.Url.AbsoluteUri.Split('/');
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < url.Count() - 1; i++)   // This should stop short of picking up the "current" aspx and replace it with the proper form.
            {
                sb.Append(url[i]);
                sb.Append("/");
            }

            StringBuilder Body = new StringBuilder();
            List<GetOrderEntryList_TemplateRawData_SearchGrid_01Result> Data = GetDataSet();

            if (Data != null)
            {
                Body.Append("<br/><br>");
                Body.Append("<table>");

                Body.Append("<tr>");

                Body.Append("<td style='border-style: none none solid none;border-width: thick; border-color: #E2E2E2; text-align: left; vertical-align: top;'>");
                Body.Append("Status");
                Body.Append("</td>");

                Body.Append("<td style='border-style: none none solid none;border-width: thick; border-color: #E2E2E2; text-align: left; vertical-align: top;'>");
                Body.Append("QTY");
                Body.Append("</td>");
                Body.Append("<td style='border-style: none none solid none;border-width: thick; border-color: #E2E2E2; text-align: left; vertical-align: top;'>");
                Body.Append("QTY Picked");
                Body.Append("</td>");

                Body.Append("<td style='border-style: none none solid none;border-width: thick; border-color: #E2E2E2; text-align: left; vertical-align: top;'>");
                Body.Append("Order");
                Body.Append("</td>");

                Body.Append("<td style='border-style: none none solid none;border-width: thick; border-color: #E2E2E2; text-align: left; vertical-align: top;'>");
                Body.Append("Pick List");
                Body.Append("</td>");

                Body.Append("<td style='border-style: none none solid none;border-width: thick; border-color: #E2E2E2; text-align: left; vertical-align: top;'>");
                Body.Append("Client");
                Body.Append("</td>");

                Body.Append("<td style='border-style: none none solid none;border-width: thick; border-color: #E2E2E2; text-align: left; vertical-align: top;'>");
                Body.Append("Order Date");
                Body.Append("</td>");

                Body.Append("<td style='border-style: none none solid none;border-width: thick; border-color: #E2E2E2; text-align: left; vertical-align: top;'>");
                Body.Append("Pick Pack");
                Body.Append("</td>");

                Body.Append("<td style='border-style: none none solid none;border-width: thick; border-color: #E2E2E2; text-align: left; vertical-align: top;'>");
                Body.Append("Ship Date");
                Body.Append("</td>");

                Body.Append("<td style='border-style: none none solid none;border-width: thick; border-color: #E2E2E2; text-align: left; vertical-align: top;'>");
                Body.Append("Customer PO");
                Body.Append("</td>");

                Body.Append("<td style='border-style: none none solid none;border-width: thick; border-color: #E2E2E2; text-align: left; vertical-align: top;'>");
                Body.Append("Waybill");
                Body.Append("</td>");

                Body.Append("<td style='border-style: none none solid none;border-width: thick; border-color: #E2E2E2; text-align: left; vertical-align: top;'>");
                Body.Append("Contact");
                Body.Append("</td>");
                Body.Append("<td style='border-style: none none solid none;border-width: thick; border-color: #E2E2E2; text-align: left; vertical-align: top;'>");
                Body.Append("PHone");
                Body.Append("</td>");
                Body.Append("<td style='border-style: none none solid none;border-width: thick; border-color: #E2E2E2; text-align: left; vertical-align: top;'>");
                Body.Append("Fax");
                Body.Append("</td>");
                Body.Append("<td style='border-style: none none solid none;border-width: thick; border-color: #E2E2E2; text-align: left; vertical-align: top;'>");
                Body.Append("Email");
                Body.Append("</td>");

                Body.Append("</tr>");

                foreach (GetOrderEntryList_TemplateRawData_SearchGrid_01Result r in Data.OrderBy(x => x.OrderDate).ThenBy(x => x.OrderNumber))
                {
                    Body.Append("<tr>");

                    string aStock = "AvailableStockForm.aspx?Order=" + r.OrderNumber;
                    string Pick = "/Reports/RPT_EXCEL_Out.aspx?RPT=PIC&ID=" + r.OrderHeaderID.ToString();

                    Body.Append("<td style='border-style: none none solid none;border-width: thick; border-color: #E2E2E2; text-align: left; vertical-align: top;'>");
                    Body.Append(r.Status);
                    Body.Append("</td>");

                    Body.Append("<td style='border-style: none none solid none;border-width: thick; border-color: #E2E2E2; text-align: left; vertical-align: top;'>");
                    Body.Append(r.QTY.ToString());
                    Body.Append("</td>");

                    Body.Append("<td style='border-style: none none solid none;border-width: thick; border-color: #E2E2E2; text-align: left; vertical-align: top;'>");
                    Body.Append(r.QTYPacked.ToString());
                    Body.Append("</td>");

                    Body.Append("<td style='border-style: none none solid none;border-width: thick; border-color: #E2E2E2; text-align: left; vertical-align: top;'>");
                    Body.Append("<a href=" + Uri.EscapeUriString(sb.ToString() + aStock) + ">" + HttpUtility.HtmlEncode(r.OrderNumber) + "</a><br/><br>");
                    Body.Append("</td>");

                    Body.Append("<td style='border-style: none none solid none;border-width: thick; border-color: #E2E2E2; text-align: left; vertical-align: top;'>");
                    Body.Append("<a href=" + Uri.EscapeUriString(sb.ToString() + Pick) + ">" + HttpUtility.HtmlEncode(r.OrderNumber) + "</a><br/>");
                    Body.Append("</td>");

                    Body.Append("<td style='border-style: none none solid none;border-width: thick; border-color: #E2E2E2; text-align: left; vertical-align: top;'>");
                    Body.Append(r.CompanyName);
                    Body.Append("</td>");
                    Body.Append("<td style='border-style: none none solid none;border-width: thick; border-color: #E2E2E2; text-align: left; vertical-align: top;'>");
                    Body.Append(r.OrderDate.ToShortDateString());
                    Body.Append("</td>");

                    Body.Append("<td style='border-style: none none solid none;border-width: thick; border-color: #E2E2E2; text-align: left; vertical-align: top;'>");
                    if (r.PickPackDate != null) { Body.Append(((DateTime)r.PickPackDate).ToShortDateString()); }
                    Body.Append("</td>");

                    Body.Append("<td style='border-style: none none solid none;border-width: thick; border-color: #E2E2E2; text-align: left; vertical-align: top;'>");
                    if (r.ShippedDate != null) { Body.Append(((DateTime)r.ShippedDate).ToShortDateString()); }
                    Body.Append("</td>");


                    Body.Append("<td style='border-style: none none solid none;border-width: thick; border-color: #E2E2E2; text-align: left; vertical-align: top;'>");
                    Body.Append(r.CustomerPO);
                    Body.Append("</td>");
                    Body.Append("<td style='border-style: none none solid none;border-width: thick; border-color: #E2E2E2; text-align: left; vertical-align: top;'>");
                    Body.Append(r.WayBillNumber);
                    Body.Append("</td>");


                    Body.Append("<td style='border-style: none none solid none;border-width: thick; border-color: #E2E2E2; text-align: left; vertical-align: top;'>");
                    Body.Append(r.ContactName);
                    Body.Append("</td>");
                    Body.Append("<td style='border-style: none none solid none;border-width: thick; border-color: #E2E2E2; text-align: left; vertical-align: top;'>");
                    Body.Append(r.PhoneNumber);
                    Body.Append("</td>");
                    Body.Append("<td style='border-style: none none solid none;border-width: thick; border-color: #E2E2E2; text-align: left; vertical-align: top;'>");
                    Body.Append(r.FaxNumber);
                    Body.Append("</td>");
                    Body.Append("<td style='border-style: none none solid none;border-width: thick; border-color: #E2E2E2; text-align: left; vertical-align: top;'>");
                    Body.Append(r.EmailAddress);
                    Body.Append("</td>");


                    Body.Append("</tr>");
                }

                Body.Append("</table>");

                SendEmail(txtEmailResultsAddress.Text, "Order Entry Search Results", Body);
                ScriptManager.RegisterStartupScript(this, GetType(), "RunReport", "alert('Email Sent!');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "RunReport", "alert('No Data to report!');", true);
            }
        }

        private void SendEmail(string ToAddress, string Subject, StringBuilder Body)
        {
            if (ToAddress != null && ToAddress.Length > 0)
            {
                string[] url = HttpContext.Current.Request.Url.AbsoluteUri.Split('/');
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < url.Count() - 1; i++)   // This should stop short of picking up the "current" aspx and replace it with the proper form.
                {
                    sb.Append(url[i]);
                    sb.Append("/");
                }
                SendEmail eMail = new SendEmail();
                eMail.Email(ToAddress, Body.ToString(), Subject);
            }
        }



        void btnSearch_Click(object sender, EventArgs e)
        {
            List<GetOrderEntryList_TemplateRawData_SearchGrid_01Result> Data = GetDataSet();
            grdTempDetail.DataSource = Data;
            grdTempDetail.DataBind();
        }

        private List<GetOrderEntryList_TemplateRawData_SearchGrid_01Result> GetDataSet()
        {
            string Status = "";
            if (drpStatus.SelectedIndex > 0) { Status = drpStatus.SelectedItem.Text; }
            string IFSOrderNumber = "";
            string OrderNumber = txtOrderNumber_s.Text;
            string CustomerPO = txtCustomerPO_s.Text;
            string WayBillNumber = txtWaybillNumber_s.Text;
            string ProjectTag = txtProjectTag_s.Text;
            string CompanyName = txtClient_s.Text;
            string City = txtCity_s.Text;
            string PostalCode = txtPostalCode_s.Text;
            string Country = "";         // txtCountry_s.Text;
            string PhoneNumber = txtPhoneNumber_s.Text;
            string EmailAddress = txtEmailAddress_s.Text;
            string OrderBeginDate = txtBeginDate_s.Text;
            string OrderEndDate = txtEndDate_s.Text;
            if (chkReceived.Checked == false) { OrderBeginDate = ""; OrderEndDate = ""; }
            OrderManager rdm = new OrderManager(User.Identity.Name);
            return rdm.GetOrderEntryList(Status, IFSOrderNumber, OrderNumber, CustomerPO, WayBillNumber, ProjectTag, CompanyName, City, PostalCode, Country, PhoneNumber, EmailAddress, OrderBeginDate, OrderEndDate);
        }
        #endregion



        private void ResetCarrierManufacturerModelColourGrade()
        {
            ResetCarrierManufacturerModelColourGrade("", "", "", "", "");
        }
        private void ResetCarrierManufacturerModelColourGrade(string Manufacturer, string Model, string Colour, string Grade, string Carrier)
        {
            int selectedIndex = 0;

            QuestionManager qm = new QuestionManager(User.Identity.Name);
            List<Option> LO = new List<Option>();
            LO = qm.GetQuestionOptionList(ctx, "Carrier");
            drpCarrier.Items.Clear();
            drpCarrier.Items.Add(new ListItem("All", "-1"));
            selectedIndex = 0;
            foreach (Option o in LO)
            {
                ListItem li = new ListItem("(" + o.ScanKey + ") " + o.OptionText, o.OptionID.ToString());
                drpCarrier.Items.Add(li);
                if (Carrier.Length > 0 && Carrier == o.OptionText) { selectedIndex = drpCarrier.Items.Count - 1; }
            }
            drpCarrier.SelectedIndex = selectedIndex;


            LO = qm.GetQuestionOptionList(ctx, "Courier Out");
            drpCourier.Items.Clear();
            selectedIndex = 0;
            foreach (Option o in LO)
            {
                ListItem li = new ListItem(o.OptionText, o.OptionID.ToString());
                drpCourier.Items.Add(li);
            }
            drpCourier.SelectedIndex = selectedIndex;

            LO = qm.GetQuestionOptionList(ctx, "Manufacturer");
            drpManufacturer.Items.Clear();
            drpManufacturer.Items.Add(new ListItem("All", "-1"));
            selectedIndex = 0;
            foreach (Option o in LO)
            {
                ListItem li = new ListItem("(" + o.ScanKey + ") " + o.OptionText, o.OptionID.ToString());
                drpManufacturer.Items.Add(li);
                if (Manufacturer.Length > 0 && Manufacturer == o.OptionText) { selectedIndex = drpManufacturer.Items.Count - 1; }
            }
            drpManufacturer.SelectedIndex = selectedIndex;

            LO = qm.GetQuestionOptionList(ctx, "Model");
            drpModel.Items.Clear();
            drpModel.Items.Add(new ListItem("All", "-1"));
            selectedIndex = 0;
            foreach (Option o in LO)
            {
                ListItem li = new ListItem("(" + o.ScanKey + ") " + o.OptionText, o.OptionID.ToString());
                drpModel.Items.Add(li);
                if (Model.Length > 0 && Model == o.OptionText) { selectedIndex = drpModel.Items.Count - 1; }
            }
            drpModel.SelectedIndex = selectedIndex;

            LO = qm.GetQuestionOptionList(ctx, "Colour");
            drpColour.Items.Clear();
            drpColour.Items.Add(new ListItem("All", "-1"));
            selectedIndex = 0;
            foreach (Option o in LO)
            {
                ListItem li = new ListItem("(" + o.ScanKey + ") " + o.OptionText, o.OptionID.ToString());
                drpColour.Items.Add(li);
                if (Colour.Length > 0 && Colour == o.OptionText) { selectedIndex = drpColour.Items.Count - 1; }
            }
            drpColour.SelectedIndex = selectedIndex;

            LO = qm.GetQuestionOptionList(ctx, "Grade");
            drpGrade.Items.Clear();
            drpGrade.Items.Add(new ListItem("None", "-1"));
            selectedIndex = 0;
            foreach (Option o in LO)
            {
                ListItem li = new ListItem("(" + o.ScanKey + ") " + o.OptionText, o.OptionID.ToString());
                drpGrade.Items.Add(li);
                if (Grade.Length > 0 && Grade == o.OptionText) { selectedIndex = drpGrade.Items.Count - 1; }
            }
            drpGrade.SelectedIndex = selectedIndex;

            LO = qm.GetQuestionOptionList(ctx, "Disposition");
            drpDisposition.Items.Clear();
            drpDisposition.Items.Add(new ListItem("None", "-1"));
            selectedIndex = 0;
            foreach (Option o in LO)
            {
                ListItem li = new ListItem("(" + o.ScanKey + ") " + o.OptionText, o.OptionID.ToString());
                drpDisposition.Items.Add(li);
            }
            drpDisposition.SelectedIndex = selectedIndex;

            drpCarrier.Attributes.Add("onchange", "javascript:FillDropDown('Carrier');");
            drpManufacturer.Attributes.Add("onchange", "javascript:FillDropDown('Manufacturer');");
            drpModel.Attributes.Add("onchange", "javascript:FillDropDown('Model');");
        }

        void RestrictFunctions()
        {
            //TabPanelNew.Visible = false;
            //TabPanelPick.Visible = false;
            //TabPanelShip.Visible = false;
            //TabPanelBill.Visible = false;
            //TabPanelDone.Visible = false;
            //TabPanelArchive.Visible = false;
            //OrderManager om = new OrderManager(User.Identity.Name);
            //using (clsLinqDataContext ctx = om.GetDataContext(User.Identity.Name))
            //{
            //    if (om.UserRestrict.AllowSelectFunctionExecute("OrderEntry.Tab.New", ctx) == true) { TabPanelNew.Visible = true; }
            //    if (om.UserRestrict.AllowSelectFunctionExecute("OrderEntry.Tab.Pick/Pack", ctx) == true) { TabPanelPick.Visible = true; }
            //    if (om.UserRestrict.AllowSelectFunctionExecute("OrderEntry.Tab.Ship", ctx) == true) { TabPanelShip.Visible = true; }
            //    if (om.UserRestrict.AllowSelectFunctionExecute("OrderEntry.Tab.Bill", ctx) == true) { TabPanelBill.Visible = true; }
            //    if (om.UserRestrict.AllowSelectFunctionExecute("OrderEntry.Tab.Done", ctx) == true) { TabPanelDone.Visible = true; }
            //    if (om.UserRestrict.AllowSelectFunctionExecute("OrderEntry.Tab.Archive", ctx) == true) { TabPanelArchive.Visible = true; }
            //}
        }

        string[] CodeDetailLine(clsLinqDataContext ctx, string Manufacturer, string Model, string Colour, string Grade, string Carrier)
        {
            string[] rValue = { "", "" };

            StringBuilder sb = new StringBuilder();
            string Code = (from x in ctx.Options where x.Question.Name.ToUpper() == "CARRIER" && x.OptionText.ToUpper() == Carrier.ToUpper() select x.ScanKey).FirstOrDefault();
            if (Code != null && Code.Length > 0) { sb.Append(Code); sb.Append(" "); }
            Code = (from x in ctx.Options where x.Question.Name.ToUpper() == "MANUFACTURER" && x.OptionText.ToUpper() == Manufacturer.ToUpper() select x.ScanKey).FirstOrDefault();
            if (Code != null && Code.Length > 0) { sb.Append(Code); sb.Append(" "); }
            Code = (from x in ctx.Options where x.Question.Name.ToUpper() == "MODEL" && x.OptionText.ToUpper() == Model.ToUpper() select x.ScanKey).FirstOrDefault();
            if (Code != null && Code.Length > 0) { sb.Append(Code); sb.Append(" "); }
            Code = (from x in ctx.Options where x.Question.Name.ToUpper() == "COLOUR" && x.OptionText.ToUpper() == Colour.ToUpper() select x.ScanKey).FirstOrDefault();
            if (Code != null && Code.Length > 0) { sb.Append(Code); sb.Append(" "); }
            Code = (from x in ctx.Options where x.Question.Name.ToUpper() == "GRADE" && x.OptionText.ToUpper() == Grade.ToUpper() select x.ScanKey).FirstOrDefault();
            if (Code != null && Code.Length > 0) { sb.Append(Code); sb.Append(" "); }
            rValue[0] = sb.ToString();

            sb.Clear();
            if (Carrier.Length > 0) { sb.Append(Carrier); sb.Append(" "); }
            if (Manufacturer.Length > 0) { sb.Append(Manufacturer); sb.Append(" "); }
            if (Model.Length > 0) { sb.Append(Model); sb.Append(" "); }
            if (Colour.Length > 0) { sb.Append(Colour); sb.Append(" "); }
            if (Grade.Length > 0) { sb.Append(Grade); sb.Append(" "); }
            rValue[1] = sb.ToString();
            return rValue;
        }


        //void btnFromBin_B_Click(object sender, EventArgs e)
        //{
        //    if (txtFromBin.Text.Length == 0) { return; }
        //    // Save the order
        //    SaveData();
        //    IMEIOrderUploadProcessor processor = new IMEIOrderUploadProcessor(txtFromBin.Text, User.Identity.Name, lblPurchaseOrderNumber.Text);
        //    processor.LoadIMEIData_FromBinNumber();


        //    //pnlmainentry.Visible = true;
        //    //pnlNew.Visible = false;

        //    //// Reload the order
        //    decimal id = -1;
        //    if (decimal.TryParse(hdnOrderHeaderID.Value, out id) == false) { id = -1; }
        //    EditOrder(id);
        //}


        void btnFromBin_Click(object sender, EventArgs e)
        {
            if (txtFromBin.Text.Length == 0) { return; }

            decimal ProjID = -1;
            if (decimal.TryParse(drpProjectList_New.SelectedValue, out ProjID) == false) { ProjID = -1; }

            List<clsOrderDetailLine> DataLines = new List<clsOrderDetailLine>();
            // get the data in the grid first.
            foreach (GridViewRow r in grdNewOrderDetailGrid.Rows)
            {
                clsOrderDetailLine l = new clsOrderDetailLine();
                decimal num = 0;
                string str = (r.Cells[ColIndex("OrderDetailID", OrderDetailGridcols)].Text == blank ? "-1" : r.Cells[ColIndex("OrderDetailID", OrderDetailGridcols)].Text);
                if (decimal.TryParse(str, out num) == false) { num = -1; }
                l.OrderDetailID = num;

                str = (r.Cells[ColIndex("QTY", OrderDetailGridcols)].Text == blank ? "0" : r.Cells[ColIndex("QTY", OrderDetailGridcols)].Text);
                if (decimal.TryParse(str, out num) == false) { num = 0; }
                l.QTY = num;

                str = (r.Cells[ColIndex("UNITPRICE", OrderDetailGridcols)].Text == blank ? "0" : r.Cells[ColIndex("UNITPRICE", OrderDetailGridcols)].Text);
                if (decimal.TryParse(str, out num) == false) { num = 0; }
                l.QTY = num;

                l.SKU = (r.Cells[ColIndex("SKU", OrderDetailGridcols)].Text == blank ? "" : r.Cells[ColIndex("SKU", OrderDetailGridcols)].Text);
                l.Desc_Code = (r.Cells[ColIndex("Desc_Code", OrderDetailGridcols)].Text == blank ? "" : r.Cells[ColIndex("Desc_Code", OrderDetailGridcols)].Text);
                l.Desc_Text = (r.Cells[ColIndex("Desc_Text", OrderDetailGridcols)].Text == blank ? "" : r.Cells[ColIndex("Desc_Text", OrderDetailGridcols)].Text);
                DataLines.Add(l);
            }
            // Add the new line here from the Bin Search..
            OrderManager om = new OrderManager(User.Identity.Name);


            // todo This needs to be changed to allow for the CarrierID, ManufactuerID, ModelID, ColourID and GradieID.
            // GetOrderEntryBinRecordsResult needs to be changed to carrier these attributes.
            List<GetOrderEntryBinRecordsResult> orL = om.ReceiveDetailBinOrderDetailLines(txtFromBin.Text);

            decimal ODID = (decimal)grdNewOrderDetailGrid.Rows.Count;
            foreach (GetOrderEntryBinRecordsResult or in orL)
            {
                clsOrderDetailLine ld = new clsOrderDetailLine();
                ld.OrderDetailID = ODID;
                ld.QTY = (decimal)or.Quantity;
                ld.SKU = txtFromBinSku.Text;
                ld.Desc_Code = or.Desc_Code;
                ld.Desc_Text = or.Desc_Text;

                DataLines.Add(ld);
                ODID += 1;
            }

            grdNewOrderDetailGrid.DataSource = DataLines;
            grdNewOrderDetailGrid.DataBind();

            lblAddOrder.Text = "Add Order";
            pnlNew.Visible = true;
            pnlNewDetailLine.Visible = false;
        }
        void btnPostBulkInventory_Click(object sender, EventArgs e)
        {
            clsOrderHeader OH = GatherSheetData();
            OH.SaveHeaderData();
            OH.Refresh(ctx);
            OH.PostBulkData(ctx);
            LoadOrderHeader(OH.OrderHeaderID);
        }
        void btnPostInventory_NoLock_Click(object sender, EventArgs e)
        {
            // process all of the items.
            // get the header id, go down through all the detail
            //     for each detail, go through all the ESN numbers and locate them in ReceiveDetail
            //
            clsOrderHeader OH = GatherSheetData();
            OH.SaveHeaderData();
            OH.Refresh(ctx);
            OH.PostData(ctx, false);
            LoadOrderHeader(OH.OrderHeaderID);
        }
        void btnPostInventory_Click(object sender, EventArgs e)
        {
            // process all of the items.
            // get the header id, go down through all the detail
            //     for each detail, go through all the ESN numbers and locate them in ReceiveDetail
            //
            clsOrderHeader OH = GatherSheetData();
            OH.SaveHeaderData();
            OH.Refresh(ctx);
            OH.PostData(ctx, true);
            LoadOrderHeader(OH.OrderHeaderID);
        }
        void btnDetailRecieve_OK_Click(object sender, EventArgs e)
        {
            decimal OrderHeaderID = -1;
            decimal OrderDetailID = -1;

            string ESNText = txtESNScan.Text;
            if (decimal.TryParse(hdnOrderHeaderID.Value, out OrderHeaderID) == false) { OrderHeaderID = -1; }
            if (decimal.TryParse(hdnOrderDetailID.Value, out OrderDetailID) == false) { OrderDetailID = -1; }


            // first go through and delete any that have been identified as such.
            OrderManager om = new OrderManager(User.Identity.Name);
            foreach (GridViewRow r in grdOrderDetailRecieve.Rows)
            {
                decimal ID = -1;
                string sID = (r.Cells[2].Text == blank ? "" : r.Cells[2].Text);
                if (decimal.TryParse(sID, out ID) == false) { ID = -1; }
                CheckBox CB = (CheckBox)r.FindControl("chkIsDeleted");
                if (CB != null)
                {
                    if (CB.Checked == true)
                    {
                        clsOrderDetailLineReceive ODL = new clsOrderDetailLineReceive();
                        ODL.OrderDetailReceiveDetailID = ID;
                        ODL.Delete(ctx);

                    }

                }
            }



            // Save the data
            om.ApplyReceiveDetail(ctx, OrderDetailID, ESNText);
            // Refresh the screen
            LoadOrderHeader(OrderHeaderID);

            pnlNew.Visible = true;
            pnlPackDetail.Visible = false;
        }
        void btnDetailRecieve_Cancel_Click(object sender, EventArgs e)
        {
            pnlNew.Visible = true;
            pnlPackDetail.Visible = false;
        }





        void BindOrderReceiveDetail(decimal ID)
        {
            OrderManager om = new OrderManager(User.Identity.Name);
            grdOrderDetailRecieve.DataSource = om.GetOrderDetailRecieveList(ctx, ID);
            grdOrderDetailRecieve.DataBind();
        }
        void drpProjectList_New_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow r in grdNewOrderDetailGrid.Rows)
            {
                decimal ProjID = -1;
                if (decimal.TryParse(drpProjectList_New.SelectedValue, out ProjID) == false) { ProjID = -1; }
                string SourceCodes = (r.Cells[ColIndex("Desc_Code", OrderDetailGridcols)].Text == blank ? "" : r.Cells[ColIndex("Desc_Code", OrderDetailGridcols)].Text);
                QuestionManager QM = new QuestionManager(User.Identity.Name);
                r.Cells[ColIndex("Desc_Text", OrderDetailGridcols)].Text = QM.TranslateOptionScanCodes(ctx, ProjID, (SourceCodes.Length == 0 ? "" : SourceCodes));
            }
        }

        void btnNew_Click(object sender, EventArgs e)
        {
            lblAddOrder.Text = "Add Order";
            LoadOrderHeader(-1);
        }
        void btnNewOK_Click(object sender, EventArgs e)
        {
            // Save the Data
            SaveData();
            pnlmainentry.Visible = true;
            pnlNew.Visible = false;
            LoadOrderData();
        }
        void SaveData()
        {
            clsOrderHeader OH = GatherSheetData();
            OH.SaveHeaderData();
            lblPurchaseOrderNumber.Text = OH.OrderNumber;
            hdnOrderHeaderID.Value = OH.OrderHeaderID.ToString();
        }
        void btnNewCancel_Click(object sender, EventArgs e)
        {
            pnlmainentry.Visible = true;
            pnlNew.Visible = false;
        }

        void btnEditNameAddressOK_Click(object sender, EventArgs e)
        {
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
        void btnBillClientSearch_Click(object sender, EventArgs e)
        {
            // search and get the data from the database
            clsOrderHeaderCompany Company = new clsOrderHeaderCompany();
            Company.LoadCompanyFromClientLocation(txtBillClient.Text);

            //            hdnClientType.Value = "Client";
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
        void btnShipClientSearch_Click(object sender, EventArgs e)
        {
            clsOrderHeaderCompany Company = new clsOrderHeaderCompany();
            Company.LoadCompanyFromClientLocation(txtShipClient.Text);
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

        void btnAddDetailLine_Click(object sender, EventArgs e)
        {
            ResetCarrierManufacturerModelColourGrade();
            OrderDetailLineID.Value = "-1";
            NewDetailQTY.Text = "0";
            NewDetailSKU.Text = txtFromBinSku.Text;
            NewDetailAttributeCode.Text = "";
            NewDetailAttributeText.Text = "";
            NewDetailAttributeCode.Visible = false;
            NewDetailAttributeText.Visible = false;
            lblNewOrderDetailLine.Text = "New Detail Line";
            AddDetailOK.Text = "Add";
            pnlNew.Visible = false;
            pnlNewDetailLine.Visible = true;
        }
        void AddDetailCancel_Click(object sender, EventArgs e)
        {
            pnlNew.Visible = true;
            pnlNewDetailLine.Visible = false;
        }

        int ColIndex(string Key, string[] cols)
        {
            int i = 0;
            Key = Key.ToUpper();
            foreach (string c in cols)
            {
                if (Key == c.ToUpper()) { return i; }
                i++;
            }
            return -1;
        }
        void AddDetailOK_Click(object sender, EventArgs e)
        {
            //string blank = "&nbsp;";

            KeyAttributes Keys = GatherKeys();

            HiddenField Manufacturer = null;
            HiddenField Model = null;
            HiddenField Colour = null;
            HiddenField Grade = null;
            HiddenField Carrier = null;
            HiddenField aON = null;
            HiddenField aQ = null;
            HiddenField aID = null;

            int index = -1;
            int _i = 0;
            decimal ProjID = -1;
            if (decimal.TryParse(drpProjectList_New.SelectedValue, out ProjID) == false) { ProjID = -1; }
            if (int.TryParse(OrderDetailLineID.Value.ToString(), out index) == false) { index = -1; }

            if (index >= 0)
            {
                GridViewRow row = grdNewOrderDetailGrid.Rows[index];
                //row.Cells[1].Text = (NewDetailQTY.Text.Length == 0 ? "" : NewDetailQTY.Text);
                //row.Cells[4].Text = (NewDetailSKU.Text.Length == 0 ? "" : NewDetailSKU.Text);
                row.Cells[ColIndex("QTY", OrderDetailGridcols)].Text = (NewDetailQTY.Text.Length == 0 ? "" : NewDetailQTY.Text);
                row.Cells[ColIndex("SKU", OrderDetailGridcols)].Text = (NewDetailSKU.Text.Length == 0 ? "" : NewDetailSKU.Text);
                row.Cells[ColIndex("UNITPRICE", OrderDetailGridcols)].Text = (NewPriceUnit.Text.Length == 0 ? "" : NewPriceUnit.Text);


                string Codes = GetDetailAttributeCodes();

                if (Codes.Length > 0)
                {
                    QuestionManager QM = new QuestionManager(User.Identity.Name);
                    //row.Cells[5].Text = Codes;
                    //row.Cells[6].Text = QM.TranslateOptionScanCodes(ctx, ProjID, Codes);
                    row.Cells[ColIndex("Desc_Code", OrderDetailGridcols)].Text = Codes;
                    row.Cells[ColIndex("Desc_Text", OrderDetailGridcols)].Text = QM.TranslateOptionScanCodes(ctx, ProjID, Codes);

                    Manufacturer = (HiddenField)row.FindControl("hdnManufacturer");
                    Model = (HiddenField)row.FindControl("hdnModel");
                    Colour = (HiddenField)row.FindControl("hdnColour");
                    Grade = (HiddenField)row.FindControl("hdnGrade");
                    Carrier = (HiddenField)row.FindControl("hdnCarrier");
                    Manufacturer.Value = Keys.Manufacturer;
                    Model.Value = Keys.Model;
                    Colour.Value = Keys.Colour;
                    Grade.Value = Keys.Grade;
                    Carrier.Value = Keys.Carrier;
                }
            }
            else
            {
                List<clsOrderDetailLine> DataLines = new List<clsOrderDetailLine>();
                // get the data in the grid first.
                foreach (GridViewRow r in grdNewOrderDetailGrid.Rows)
                {
                    clsOrderDetailLine l = new clsOrderDetailLine();
                    decimal num = 0;
                    //string str = (r.Cells[7].Text == blank ? "-1" : r.Cells[7].Text);
                    string str = (r.Cells[ColIndex("OrderDetailID", OrderDetailGridcols)].Text == blank ? "-1" : r.Cells[ColIndex("OrderDetailID", OrderDetailGridcols)].Text);
                    if (decimal.TryParse(str, out num) == false) { num = -1; }
                    l.OrderDetailID = num;

                    str = (r.Cells[ColIndex("QTY", OrderDetailGridcols)].Text == blank ? "0" : r.Cells[ColIndex("QTY", OrderDetailGridcols)].Text);
                    if (decimal.TryParse(str, out num) == false) { num = 0; }
                    l.QTY = num;

                    str = (r.Cells[ColIndex("UNITPRICE", OrderDetailGridcols)].Text == blank ? "0" : r.Cells[ColIndex("UNITPRICE", OrderDetailGridcols)].Text);
                    if (decimal.TryParse(str, out num) == false) { num = 0; }
                    l.UnitPrice = num;


                    //l.SKU = (r.Cells[4].Text == blank ? "" : r.Cells[4].Text);
                    //l.Desc_Code = (r.Cells[5].Text == blank ? "" : r.Cells[5].Text);
                    //l.Desc_Text = (r.Cells[6].Text == blank ? "" : r.Cells[6].Text);
                    l.SKU = (r.Cells[ColIndex("SKU", OrderDetailGridcols)].Text == blank ? "" : r.Cells[ColIndex("SKU", OrderDetailGridcols)].Text);
                    l.Desc_Code = (r.Cells[ColIndex("Desc_Code", OrderDetailGridcols)].Text == blank ? "" : r.Cells[ColIndex("Desc_Code", OrderDetailGridcols)].Text);
                    l.Desc_Text = (r.Cells[ColIndex("Desc_Text", OrderDetailGridcols)].Text == blank ? "" : r.Cells[ColIndex("Desc_Text", OrderDetailGridcols)].Text);


                    aON = (HiddenField)r.FindControl("hdnOrderNumber");
                    aQ = (HiddenField)r.FindControl("hdnQTY");
                    aID = (HiddenField)r.FindControl("hdnStockID");

                    Manufacturer = (HiddenField)r.FindControl("hdnManufacturer");
                    Model = (HiddenField)r.FindControl("hdnModel");
                    Colour = (HiddenField)r.FindControl("hdnColour");
                    Grade = (HiddenField)r.FindControl("hdnGrade");
                    Carrier = (HiddenField)r.FindControl("hdnCarrier");


                    l.Manufacturer = Manufacturer.Value;
                    l.Model = Model.Value;
                    l.Colour = Colour.Value;
                    l.Grade = Grade.Value;
                    l.Carrier = Carrier.Value;

                    l.AvailableStock_OrderNumber = aON.Value;
                    str = aQ.Value;
                    if (int.TryParse(str, out _i) == false) { num = 0; }
                    l.AvailableStock_QTY = _i;

                    str = aID.Value;
                    if (decimal.TryParse(str, out num) == false) { num = -1; }
                    l.ReservedAvailableStockID = num;

                    DataLines.Add(l);
                }
                // Add the new line here.
                decimal n = 0;
                clsOrderDetailLine ld = new clsOrderDetailLine();
                ld.OrderDetailID = (decimal)grdNewOrderDetailGrid.Rows.Count;

                if (decimal.TryParse((NewDetailQTY.Text.Length == 0 ? "" : NewDetailQTY.Text), out n) == false) { n = 0; }
                ld.QTY = n;
                ld.SKU = (NewDetailSKU.Text.Length == 0 ? "" : NewDetailSKU.Text);
                ld.Desc_Code = GetDetailAttributeCodes();
                QuestionManager QM = new QuestionManager(User.Identity.Name);
                ld.Desc_Text = QM.TranslateOptionScanCodes(ctx, ProjID, GetDetailAttributeCodes());
                ld.Colour = Keys.Colour;
                ld.Carrier = Keys.Carrier;
                ld.Manufacturer = Keys.Manufacturer;
                ld.Model = Keys.Model;
                ld.Grade = Keys.Grade;

                //if (decimal.TryParse((NewDetailQTY.Text.Length == 0 ? blank : NewDetailQTY.Text), out n) == false) { n = 0; }
                //ld.QTY = n;
                //ld.SKU = (NewDetailSKU.Text.Length == 0 ? blank : NewDetailSKU.Text);
                //ld.Desc_Code = (NewDetailAttributeCode.Text.Length == 0 ? blank : NewDetailAttributeCode.Text);
                //ld.Desc_Text = (NewDetailAttributeText.Text.Length == 0 ? blank : NewDetailAttributeText.Text);
                DataLines.Add(ld);
                grdNewOrderDetailGrid.DataSource = DataLines;
                grdNewOrderDetailGrid.DataBind();
            }
            lblAddOrder.Text = "Add Order";
            pnlNew.Visible = true;
            pnlNewDetailLine.Visible = false;

        }

        private KeyAttributes GatherKeys()
        {
            KeyAttributes keys = new KeyAttributes();

            //string NewDetailAttributeCodes = "";
            string tValue = "";

            tValue = drpCarrier.SelectedItem.Text;
            if (tValue.IndexOf("(") > -1)
            {
                keys.Carrier = tValue.Substring(tValue.IndexOf(")") + 1);
            }

            tValue = drpManufacturer.SelectedItem.Text;
            if (tValue.IndexOf("(") > -1)
            {
                keys.Manufacturer = tValue.Substring(tValue.IndexOf(")") + 1);
            }

            tValue = drpModel.SelectedItem.Text;
            if (tValue.IndexOf("(") > -1)
            {
                keys.Model = tValue.Substring(tValue.IndexOf(")") + 1);
            }

            tValue = drpColour.SelectedItem.Text;
            if (tValue.IndexOf("(") > -1)
            {
                keys.Colour = tValue.Substring(tValue.IndexOf(")") + 1);
            }

            tValue = drpGrade.SelectedItem.Text;
            if (tValue.IndexOf("(") > -1)
            {
                keys.Grade = tValue.Substring(tValue.IndexOf(")") + 1);
            }

            tValue = drpDisposition.SelectedItem.Text;
            if (tValue.IndexOf("(") > -1)
            {
                keys.Disposition = tValue.Substring(tValue.IndexOf(")") + 1);
            }
            return keys;
        }



        private string GetDetailAttributeCodes()
        {
            string NewDetailAttributeCodes = "";
            string tValue = "";

            tValue = drpCarrier.SelectedItem.Text;
            if (tValue.IndexOf("(") > -1)
            {
                tValue = tValue.Substring(1, tValue.IndexOf(")") - 1);
            }
            if (tValue.Length > 0 && tValue.ToUpper() != "ALL")
            {
                NewDetailAttributeCodes = NewDetailAttributeCodes + tValue + " ";
            }

            tValue = drpManufacturer.SelectedItem.Text;
            if (tValue.IndexOf("(") > -1)
            {
                tValue = tValue.Substring(1, tValue.IndexOf(")") - 1);
            }
            if (tValue.Length > 0 && tValue.ToUpper() != "ALL") { NewDetailAttributeCodes = NewDetailAttributeCodes + tValue + " "; }

            tValue = drpModel.SelectedItem.Text;
            if (tValue.IndexOf("(") > -1) { tValue = tValue.Substring(1, tValue.IndexOf(")") - 1); }
            if (tValue.Length > 0 && tValue.ToUpper() != "ALL") { NewDetailAttributeCodes = NewDetailAttributeCodes + tValue + " "; }

            tValue = drpColour.SelectedItem.Text;
            if (tValue.IndexOf("(") > -1) { tValue = tValue.Substring(1, tValue.IndexOf(")") - 1); }
            if (tValue.Length > 0 && tValue.ToUpper() != "ALL") { NewDetailAttributeCodes = NewDetailAttributeCodes + tValue + " "; }

            tValue = drpGrade.SelectedItem.Text;
            if (tValue.IndexOf("(") > -1) { tValue = tValue.Substring(1, tValue.IndexOf(")") - 1); }
            if (tValue.Length > 0 && tValue.ToUpper() != "NONE") { NewDetailAttributeCodes = NewDetailAttributeCodes + tValue + " "; }

            tValue = drpDisposition.SelectedItem.Text;
            if (tValue.IndexOf("(") > -1) { tValue = tValue.Substring(1, tValue.IndexOf(")") - 1); }
            if (tValue.Length > 0 && tValue.ToUpper() != "NONE") { NewDetailAttributeCodes = NewDetailAttributeCodes + tValue + " "; }



            return NewDetailAttributeCodes;
        }

        void tabMain_ActiveTabChanged(object sender, EventArgs e)
        {

            TabFilter.HeaderText = "Filter";
            if (chkFilterOrderDate.Checked == true) { TabFilter.HeaderText = "Filter (Set)"; }

            string status = tabMain.ActiveTab.HeaderText;
            if (status.ToUpper() == "FILTER" || status.ToUpper() == "FILTER (SET)")
            {
                GridView2.Visible = false;
            }
            else
            {
                GridView2.Visible = true;
                LoadOrderData();
            }
        }

        void LoadOrderData()
        {
            string status = tabMain.ActiveTab.HeaderText;
            if (status.Length > 0 && tabMain.ActiveTab.Visible == true)
            {
                OrderManager om = new OrderManager(User.Identity.Name);
                if (chkFilterOrderDate.Checked == true)
                {
                    DateTime StartDate = DateTime.Now;
                    DateTime EndDate = DateTime.Now;
                    EndDate.AddDays(1);
                    if (DateTime.TryParse(OrderStartDate.Text, out StartDate) == false) { StartDate = DateTime.Now; }
                    if (DateTime.TryParse(OrderEndDate.Text, out EndDate) == false) { EndDate = DateTime.Now; }
                    //    var dta = om.GetMasterOrderList(ctx, status);
                    //    GridView2.DataSource = from d in dta where d.OrderDate >= StartDate && d.OrderDate < EndDate select d;
                    //}
                    //else
                    //{
                    //    GridView2.DataSource = om.GetMasterOrderList(ctx, status);
                    //}
                    var dta = om.GetMasterOrderListDevices(ctx, status);
                    GridView2.DataSource = (from d in dta
                                            where d.OrderDate >= StartDate
                                               && d.OrderDate < EndDate
                                            //&& d.OrderDetails.Any(x=> x.Condition != null && x.Condition.Length > 0) == true        // This is placed here to only show those orders that are Device specific.
                                            select d).OrderBy(x => x.CreateDate);
                }
                else
                {
                    GridView2.DataSource = om.GetMasterOrderListDevices(ctx, status).OrderBy(x => x.CreateDate);
                }
                GridView2.DataBind();
            }
        }
        void LoadOrderHeader(decimal HeaderID)
        {
            btnAddDetailLine.Enabled = true;
            txtWaybillNumber.Visible = true;
            btnPostInventory.Visible = false;
            btnPostBulkInventory.Visible = false;
            //OrderManager om = new OrderManager(User.Identity.Name);
            clsOrderHeader OH = new clsOrderHeader(ctx, HeaderID);

            if (OH.Status.ToUpper() != "DONE")
            {
                if (tabMain.ActiveTab.ID.ToUpper() != "TABPANELNEW") { btnAddDetailLine.Enabled = false; }
                if (tabMain.ActiveTab.ID.ToUpper() == "TABPANELNEW" || tabMain.ActiveTab.ID.ToUpper() == "TABPANELPICK") { txtWaybillNumber.Visible = false; }
                if (tabMain.ActiveTab.ID.ToUpper() == "TABPANELPICK") { btnPostInventory.Visible = true; btnPostBulkInventory.Visible = true; }
            }

            hdnOrderHeaderID.Value = OH.OrderHeaderID.ToString();
            txtCustomerPONumber.Text = OH.CustomerPO;
            lblPurchaseOrderNumber.Text = OH.OrderNumber;
            lblIFSOrderNumber.Text = OH.IFSOrderNumber;
            txtWaybillNumber.Text = OH.WaybillNumber;
            txtProjectTag.Text = OH.ProjectTag;
            chkPaid.Checked = OH.Paid;
            chkPostPaid.Checked = OH.PostPaid;

            // Set the project 
            ListItem li = null;
            li = drpProjectList_New.Items.FindByValue(OH.ProjectID.ToString());
            if (li != null)
            {
                drpProjectList_New.SelectedIndex = -1;
                li.Selected = true;
            }
            txtBillClient.Text = "";
            // This fills the CLient and Ship to hidden fields.
            hdnBillClientLocationID.Value = OH.ClientCompany.ClientLocationID.ToString();
            hdnBillCompanyName.Value = OH.ClientCompany.CompanyName;
            hdnBillContactName.Value = OH.ClientCompany.ContactName;
            hdnBillAddressLine1.Value = OH.ClientCompany.AddressLine1;
            hdnBillAddressLine2.Value = OH.ClientCompany.AddressLine2;
            hdnBillCity.Value = OH.ClientCompany.City;
            hdnBillStateOrProvince.Value = OH.ClientCompany.StateOrProvince;
            hdnBillPostalCode.Value = OH.ClientCompany.PostalCode;
            hdnBillPhoneNumber.Value = OH.ClientCompany.PhoneNumber;
            hdnBillFaxNumber.Value = OH.ClientCompany.FaxNumber;
            hdnBillNotes.Value = OH.ClientCompany.Notes;
            txtBillNameAddresstext.Text = GetAddressString("Client");

            txtInternalNote.Text = OH.InternalNote;
            txtDeliveryNote.Text = OH.DeliveryNote;

            txtShipClient.Text = "";
            hdnShipClientLocationID.Value = OH.ShipToCompany.ClientLocationID.ToString();
            hdnShipCompanyName.Value = OH.ShipToCompany.CompanyName;
            hdnShipContactName.Value = OH.ShipToCompany.ContactName;
            hdnShipAddressLine1.Value = OH.ShipToCompany.AddressLine1;
            hdnShipAddressLine2.Value = OH.ShipToCompany.AddressLine2;
            hdnShipCity.Value = OH.ShipToCompany.City;
            hdnShipStateOrProvince.Value = OH.ShipToCompany.StateOrProvince;
            hdnShipPostalCode.Value = OH.ShipToCompany.PostalCode;
            hdnShipPhoneNumber.Value = OH.ShipToCompany.PhoneNumber;
            hdnShipFaxNumber.Value = OH.ShipToCompany.FaxNumber;
            hdnShipNotes.Value = OH.ShipToCompany.Notes;
            txtShipNameAddresstext.Text = GetAddressString("ShipTo");

            grdNewOrderDetailGrid.DataSource = OH.OrderDetailLines;
            grdNewOrderDetailGrid.DataBind();
            pnlmainentry.Visible = false;

            if (OH.Status.ToUpper() == "DONE")
            {
                btnBillClientSearch.Visible = false;
                btnSearchClient.Visible = false;
                btnBillClientEdit.Visible = false;
                btnShipClientSearch.Visible = false;
                ImageButton1.Visible = false;
                btnShipClientEdit.Visible = false;
                btnAddDetailLine.Visible = false;
                txtFromBinSku.Visible = false;
                txtFromBin.Visible = false;
                btnFromBin.Visible = false;
                btnNewOK.Visible = false;
                btnPostInventory.Visible = false;
                btnPostInventory_NoLock.Visible = false;
                btnPostBulkInventory.Visible = false;
                btnNewCancel.Visible = false;
            }

            pnlNew.Visible = true;
        }


        void btnPanelMove_Click(object sender, EventArgs e)
        {


            try
            {
                //log.LogIt("Panel Move Item Started");
                OrderManager OM = new OrderManager(User.Identity.Name);
                //log.Report();
                string rValue = OM.MoveOrder(ctx, txtOrderNumber.Text, drpMoveList.SelectedItem.Text, drpCourier.SelectedItem.Text, log);
                //log.LogIt("Move all done.");
                ScriptManager.RegisterStartupScript(this, GetType(), "RestHeader", "alert('" + rValue + "');", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "RestHeader", "alert('" + "Error:" + ex.Message + "');", true);
            }




            //}
        }

        void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('inside GridView2_RowCommand');", true);
            //return;
            //log = new clsLog(Server.MapPath("~"), "WebServer_01_Log.txt", User.Identity.Name, System.Configuration.ConfigurationManager.AppSettings["WriteLogUserNameToLog"]);
            //if (System.Configuration.ConfigurationManager.AppSettings["WriteLog"].ToUpper() == "TRUE")
            //{
            //    log.writeLogData = true;
            //}
            log.LogIt("Inside GridView2_RowCommand");
            LinkButton btn = (LinkButton)e.CommandSource;
            decimal id = -1;
            int index = -1;
            switch (btn.CommandName.ToString().ToUpper())
            {
                case "EDITITEM":
                    if (decimal.TryParse(btn.CommandArgument, out id) == false) { id = -1; }
                    EditOrder(id);
                    //ScriptManager.RegisterStartupScript(this, GetType(), "RestHeader", "alert('EDIT:" + btn.CommandArgument + "');", true);
                    break;
                case "DELETEITEM":
                    if (decimal.TryParse(btn.CommandArgument, out id) == false) { id = -1; }
                    OrderManager oMan = new OrderManager(User.Identity.Name);
                    oMan.DeleteOrder(id, log);
                    LoadOrderData();
                    // ScriptManager.RegisterStartupScript(this, GetType(), "RestHeader", "alert('Delete:" + btn.CommandArgument + "');", true);
                    break;
                case "PRINTITEM":
                    if (decimal.TryParse(btn.CommandArgument, out id) == false) { id = -1; }
                    if (tabMain.ActiveTab.ID.ToUpper() == "TABPANELNEW" || tabMain.ActiveTab.ID.ToUpper() == "TABPANELPICK")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "RunReport", "ExportPickPackingShipReport('PIC'," + id.ToString() + ");", true);
                    }
                    else if (tabMain.ActiveTab.ID.ToUpper() == "TABPANELSHIP")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "RunReport", "ExportPickPackingShipReport('PAC'," + id.ToString() + ");", true);
                        //ExportPackingSlipToExcel(id); 
                    }
                    else if (tabMain.ActiveTab.ID.ToUpper() == "TABPANELBILL")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "RunReport", "ExportPickPackingShipReport('SHIP'," + id.ToString() + ");", true);
                        //ExportPackingSlipToExcel(id); 
                    }
                    else if (tabMain.ActiveTab.ID.ToUpper() == "TABPANELSEARCH")
                    {
                        //ScriptManager.RegisterStartupScript(this, GetType(), "RunReport", "ExportPickPackingShipReport('DON'," + id.ToString() + ");", true);
                        //ExportPackingSlipToExcel(id); 
                    }
                    else if (tabMain.ActiveTab.ID.ToUpper() == "TABPANELARCHIVE")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "RunReport", "ExportPickPackingShipReport('ARC'," + id.ToString() + ");", true);
                        //ExportPackingSlipToExcel(id); 
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "RunReport", "ExportPickPackingShipReport('MIS'," + id.ToString() + ");", true);
                        //ExportPackingSlipToExcel(id); 
                    }
                    break;
                case "MOVEITEM":
                    log.LogIt("Move Item Started" + btn.CommandArgument.ToString());
                    if (int.TryParse(btn.CommandArgument.ToString(), out index) == false) { index = -1; }
                    if (index >= 0)
                    {
                        GridViewRow row = GridView2.Rows[index];
                        DropDownList dl = (DropDownList)row.FindControl("drpMoveTo");
                        if (dl.SelectedIndex < 1) { break; }

                        LinkButton lb = (LinkButton)row.FindControl("btnEdit");
                        string lineID = lb.CommandArgument.ToString();
                        string tx = dl.SelectedItem.Text;
                        string tv = dl.SelectedValue.ToString();
                        OrderManager OM = new OrderManager(User.Identity.Name);

                        if (decimal.TryParse(lineID, out id) == false) { id = -1; }
                        //log.Report();
                        log.LogIt("Starting the Move Processes for id:" + index.ToString());
                        string rValue = OM.MoveOrder(ctx, id, tx, tabMain.ActiveTab.HeaderText, drpCourier.SelectedItem.Text, log);
                        log.LogIt("Move all done:" + rValue);
                        LoadOrderData();
                        // ScriptManager.RegisterStartupScript(this, GetType(), "RestHeader", "alert('Move:" + lineID + "');", true);
                    }
                    break;
                default:
                    break;
            }
        }

        private void EditOrder(decimal id)
        {
            lblAddOrder.Text = "Edit Order";
            LoadOrderHeader(id);
        }
        void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            OrderHeader oh = (OrderHeader)e.Row.DataItem;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btn = (LinkButton)e.Row.FindControl("btnEdit");
                if (btn != null)
                {
                    //if (oh.Status.ToString() == "DONE")
                    btn.CommandName = "EditItem";
                    btn.CommandArgument = oh.OrderHeaderID.ToString();
                    //bPrint.Attributes.Add("OnClick", "StoreHeaderData();");
                }
                btn = (LinkButton)e.Row.FindControl("btnDelete");
                if (btn != null)
                {
                    btn.CommandName = "DeleteItem";
                    btn.CommandArgument = oh.OrderHeaderID.ToString();
                    //bPrint.Attributes.Add("OnClick", "StoreHeaderData();");
                }

                btn = (LinkButton)e.Row.FindControl("imgPrint");
                if (btn != null)
                {
                    btn.CommandName = "PrintItem";
                    btn.CommandArgument = oh.OrderHeaderID.ToString();
                    //bPrint.Attributes.Add("OnClick", "StoreHeaderData();");
                }

                btn = (LinkButton)e.Row.FindControl("btnMove");
                if (btn != null)
                {
                    btn.CommandName = "MoveItem";

                }
                btn = (LinkButton)e.Row.FindControl("btnInvoice");
                if (btn != null)
                {
                    btn.Visible = false;
                    btn.CommandName = "GMPINVOICE";
                    btn.CommandArgument = oh.OrderHeaderID.ToString();
                    if (tabMain.ActiveTab.ID.ToUpper() == "TABPANELBILL")
                    {
                        btn.Visible = true;
                    }
                }
            }
        }
        void grdNewOrderDetailGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //ScriptManager.RegisterStartupScript(this, GetType(), "Message", "alert('Inside GridView2_RowCommand');", true);
            LinkButton btn = (LinkButton)e.CommandSource;
            //string blank = "&nbsp;";
            //decimal id = -1;
            switch (btn.CommandName.ToString().ToUpper())
            {
                case "EDITITEM":
                    OrderDetailLineID.Value = btn.CommandArgument.ToString();
                    int index = -1;
                    if (int.TryParse(btn.CommandArgument.ToString(), out index) == false) { index = -1; }
                    if (index >= 0)
                    {
                        lblNewOrderDetailLine.Text = "Edit Detail Line";
                        GridViewRow row = grdNewOrderDetailGrid.Rows[index];

                        NewDetailQTY.Text = (row.Cells[ColIndex("QTY", OrderDetailGridcols)].Text == blank ? "" : row.Cells[ColIndex("QTY", OrderDetailGridcols)].Text);
                        //NewDetailQTY.Text = (row.Cells[1].Text == blank ? "" : row.Cells[1].Text);
                        NewPriceUnit.Text = (row.Cells[ColIndex("UNITPRICE", OrderDetailGridcols)].Text == blank ? "0" : row.Cells[ColIndex("UNITPRICE", OrderDetailGridcols)].Text);
                        NewDetailSKU.Text = (row.Cells[ColIndex("SKU", OrderDetailGridcols)].Text == blank ? "" : row.Cells[ColIndex("SKU", OrderDetailGridcols)].Text);
                        NewDetailAttributeCode.Text = (row.Cells[ColIndex("Desc_Code", OrderDetailGridcols)].Text == blank ? "" : row.Cells[ColIndex("Desc_Code", OrderDetailGridcols)].Text);
                        NewDetailAttributeText.Text = (row.Cells[ColIndex("Desc_Text", OrderDetailGridcols)].Text == blank ? "" : row.Cells[ColIndex("Desc_Text", OrderDetailGridcols)].Text);
                        NewDetailAttributeCode.Visible = true;
                        NewDetailAttributeText.Visible = true;
                        AddDetailOK.Text = "Save";
                        pnlNew.Visible = false;

                        HiddenField Manufacturer = (HiddenField)row.FindControl("hdnManufacturer");
                        HiddenField Model = (HiddenField)row.FindControl("hdnModel");
                        HiddenField Colour = (HiddenField)row.FindControl("hdnColour");
                        HiddenField Grade = (HiddenField)row.FindControl("hdnGrade");
                        HiddenField Carrier = (HiddenField)row.FindControl("hdnCarrier");
                        ResetCarrierManufacturerModelColourGrade(Manufacturer.Value, Model.Value, Colour.Value, Grade.Value, Carrier.Value);

                        pnlNewDetailLine.Visible = true;
                    }
                    break;
                case "PACKITEM":
                    OrderDetailLineID.Value = btn.CommandArgument.ToString();
                    index = -1;
                    if (int.TryParse(btn.CommandArgument.ToString(), out index) == false) { index = -1; }
                    if (index >= 0)
                    {
                        hdnProjtagESNlist.Value = "";
                        if (txtProjectTag.Text.Length > 0)
                        {
                            ReceiveDetailManager rdm = new ReceiveDetailManager(User.Identity.Name);
                            hdnProjtagESNlist.Value = rdm.GetProjectTagESNListString(txtProjectTag.Text);
                        }
                        txtESNScan.Text = "";
                        txtSku.Text = "";
                        ScanKey.Text = "";
                        GridViewRow row = grdNewOrderDetailGrid.Rows[index];
                        hdnCountRequired.Value = row.Cells[ColIndex("QTY", OrderDetailGridcols)].Text;
                        lblOrderNumber.Text = "Purchase Order Number:" + lblPurchaseOrderNumber.Text;
                        lblPackDetail.Text = row.Cells[ColIndex("QTY", OrderDetailGridcols)].Text + ", " + row.Cells[ColIndex("Desc_Text", OrderDetailGridcols)].Text;

                        if (int.TryParse(row.Cells[ColIndex("OrderDetailID", OrderDetailGridcols)].Text, out index) == false) { index = -1; }
                        hdnOrderDetailID.Value = index.ToString();
                        BindOrderReceiveDetail(index);

                        pnlNew.Visible = false;
                        pnlPackDetail.Visible = true;
                    }
                    break;
                case "DELETEITEM":
                //if (decimal.TryParse(btn.CommandArgument, out id) == false) { id = -1; }
                //ScriptManager.RegisterStartupScript(this, GetType(), "RestHeader", "alert('Delete:" + btn.CommandArgument + "');", true);
                //break;
                case "PRINTITEM":
                //if (decimal.TryParse(btn.CommandArgument, out id) == false) { id = -1; }
                //ScriptManager.RegisterStartupScript(this, GetType(), "RestHeader", "alert('Print:" + btn.CommandArgument + "');", true);
                //break;
                default:
                    break;
            }
        }
        void grdNewOrderDetailGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //ScriptManager.RegisterStartupScript(this, GetType(), "Message", "Alert('Inside GridView2_RowDataBound');", true);
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton bEdit = (LinkButton)e.Row.FindControl("btnEdit");
                LinkButton bPack = (LinkButton)e.Row.FindControl("btnPack");
                LinkButton bDelete = (LinkButton)e.Row.FindControl("btnDelete");

                HiddenField ON = (HiddenField)e.Row.FindControl("hdnOrderNumber");
                HiddenField Q = (HiddenField)e.Row.FindControl("hdnQTY");
                HiddenField ID = (HiddenField)e.Row.FindControl("hdnStockID");
                HiddenField Manufacturer = (HiddenField)e.Row.FindControl("hdnManufacturer");
                HiddenField Model = (HiddenField)e.Row.FindControl("hdnModel");
                HiddenField Colour = (HiddenField)e.Row.FindControl("hdnColour");
                HiddenField Grade = (HiddenField)e.Row.FindControl("hdnGrade");
                HiddenField Carrier = (HiddenField)e.Row.FindControl("hdnCarrier");

                clsOrderDetailLine dl = (clsOrderDetailLine)e.Row.DataItem;
                ON.Value = dl.AvailableStock_OrderNumber;
                Q.Value = dl.AvailableStock_QTY.ToString();
                ID.Value = dl.ReservedAvailableStockID.ToString();
                Manufacturer.Value = dl.Manufacturer;
                Model.Value = dl.Model;
                Colour.Value = dl.Colour;
                Grade.Value = dl.Grade;
                Carrier.Value = dl.Carrier;

                switch (tabMain.ActiveTab.ID.ToUpper())
                {
                    case "TABPANELNEW":
                        if (bEdit != null) { bEdit.CommandName = "EditItem"; }
                        bPack.Visible = false;
                        if (bPack != null) { bPack.CommandName = "PackItem"; }
                        if (bDelete != null)
                        {
                            bDelete.CommandName = "DeleteItem";
                            bDelete.CommandArgument = dl.OrderDetailID.ToString();
                        }
                        break;
                    case "TabPanelPick":
                        if (bEdit != null) { bEdit.CommandName = "EditItem"; }
                        if (bPack != null) { bPack.CommandName = "PackItem"; }
                        if (bDelete != null)
                        {
                            bDelete.CommandName = "DeleteItem";
                            bDelete.CommandArgument = dl.OrderDetailID.ToString();
                        }
                        break;
                    case "TabPanelShip":
                        if (bEdit != null) { bEdit.CommandName = "EditItem"; }
                        if (bPack != null) { bPack.CommandName = "PackItem"; }
                        if (bDelete != null)
                        {
                            bDelete.CommandName = "DeleteItem";
                            bDelete.CommandArgument = dl.OrderDetailID.ToString();
                        }
                        break;
                    //case "TabPanelBill":
                    //    if (bEdit != null) { bEdit.CommandName = "EditItem"; }
                    //    if (bPack != null) { bPack.CommandName = "PackItem"; }
                    //    if (bDelete != null)
                    //    {
                    //        bDelete.CommandName = "DeleteItem";
                    //        bDelete.CommandArgument = dl.OrderDetailID.ToString();
                    //    }
                    //    break;
                    case "TabPanelSearch":
                        //if (bEdit != null) { bEdit.CommandName = "EditItem"; }
                        //if (bPack != null) { bPack.CommandName = "PackItem"; }
                        //if (bDelete != null)
                        //{
                        //    bDelete.CommandName = "DeleteItem";
                        //    bDelete.CommandArgument = dl.OrderDetailID.ToString();
                        //}
                        break;



                    //case "TabPanelArchive":
                    //    if (bEdit != null) { bEdit.CommandName = "EditItem"; }
                    //    if (bPack != null) { bPack.CommandName = "PackItem"; }
                    //    if (bDelete != null)
                    //    {
                    //        bDelete.CommandName = "DeleteItem";
                    //        bDelete.CommandArgument = dl.OrderDetailID.ToString();
                    //    }
                    //break;
                    default:
                        if (bEdit != null) { bEdit.CommandName = "EditItem"; }
                        if (bPack != null) { bPack.CommandName = "PackItem"; }
                        if (bDelete != null)
                        {
                            bDelete.CommandName = "DeleteItem";
                            bDelete.CommandArgument = dl.OrderDetailID.ToString();
                        }
                        break;
                }


                //btn = (LinkButton)e.Row.FindControl("btnPack");
                //if (btn != null)
                //{
                //    btnPack.CommandName = "PackItem";
                //}

                //btn = (LinkButton)e.Row.FindControl("btnDelete");
                //if (btn != null)
                //{
                //    btn.CommandName = "DeleteItem";
                //    btn.CommandArgument = ((clsOrderDetailLine)e.Row.DataItem).OrderDetailID.ToString();
                //    //bPrint.Attributes.Add("OnClick", "StoreHeaderData();");
                //}
            }
        }
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
        void ScatterSheetData(clsOrderHeader OH)
        {
        }
        clsOrderHeader GatherSheetData()
        {
            decimal ID = -1;

            // Header Data.
            if (decimal.TryParse(hdnOrderHeaderID.Value, out ID) == false) { ID = -1; }
            clsOrderHeader OH = new clsOrderHeader(User.Identity.Name);
            OH.OrderHeaderID = ID;
            OH.UserName = User.Identity.Name;
            OH.CustomerPO = txtCustomerPONumber.Text;
            OH.OrderNumber = lblPurchaseOrderNumber.Text;
            OH.WaybillNumber = txtWaybillNumber.Text;
            OH.ProjectTag = txtProjectTag.Text;
            OH.Paid = chkPaid.Checked;
            OH.PostPaid = chkPostPaid.Checked;
            if (decimal.TryParse(drpProjectList_New.SelectedValue, out ID) == false) { ID = -1; }
            OH.ProjectID = ID;

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

            //Get Line Detail
            // get the data in the grid first.
            foreach (GridViewRow r in grdNewOrderDetailGrid.Rows)
            {
                clsOrderDetailLine l = new clsOrderDetailLine();
                decimal num = 0;
                int _i = 0;
                string str = (r.Cells[ColIndex("OrderDetailID", OrderDetailGridcols)].Text == blank ? "-1" : r.Cells[ColIndex("OrderDetailID", OrderDetailGridcols)].Text);
                if (decimal.TryParse(str, out num) == false) { num = -1; }
                l.OrderDetailID = num;


                str = (r.Cells[ColIndex("QTY", OrderDetailGridcols)].Text == blank ? "0" : r.Cells[ColIndex("QTY", OrderDetailGridcols)].Text);
                if (decimal.TryParse(str, out num) == false) { num = 0; }
                l.QTY = num;

                str = (r.Cells[ColIndex("UNITPRICE", OrderDetailGridcols)].Text == blank ? "0" : r.Cells[ColIndex("UNITPRICE", OrderDetailGridcols)].Text);
                if (decimal.TryParse(str, out num) == false) { num = 0; }
                l.UnitPrice = num;



                l.SKU = (r.Cells[ColIndex("SKU", OrderDetailGridcols)].Text == blank ? "" : r.Cells[ColIndex("SKU", OrderDetailGridcols)].Text);
                l.Desc_Code = (r.Cells[ColIndex("Desc_Code", OrderDetailGridcols)].Text == blank ? "" : r.Cells[ColIndex("Desc_Code", OrderDetailGridcols)].Text);
                l.Desc_Text = (r.Cells[ColIndex("Desc_Text", OrderDetailGridcols)].Text == blank ? "" : r.Cells[ColIndex("Desc_Text", OrderDetailGridcols)].Text);

                HiddenField aON = (HiddenField)r.FindControl("hdnOrderNumber");
                HiddenField aQ = (HiddenField)r.FindControl("hdnQTY");
                HiddenField aID = (HiddenField)r.FindControl("hdnStockID");
                HiddenField Manufacturer = (HiddenField)r.FindControl("hdnManufacturer");
                HiddenField Model = (HiddenField)r.FindControl("hdnModel");
                HiddenField Colour = (HiddenField)r.FindControl("hdnColour");
                HiddenField Grade = (HiddenField)r.FindControl("hdnGrade");
                HiddenField Carrier = (HiddenField)r.FindControl("hdnCarrier");


                l.Manufacturer = Manufacturer.Value;
                l.Model = Model.Value;
                l.Colour = Colour.Value;
                l.Grade = Grade.Value;
                l.Carrier = Carrier.Value;
                l.AvailableStock_OrderNumber = aON.Value;

                str = aQ.Value;
                if (int.TryParse(str, out _i) == false) { num = 0; }
                l.AvailableStock_QTY = _i;

                str = aID.Value;
                if (decimal.TryParse(str, out num) == false) { num = -1; }
                l.ReservedAvailableStockID = num;


                l.isDeleted = false;
                CheckBox btn = (CheckBox)r.FindControl("chkIsDeleted");
                if (btn.Checked == true) { l.isDeleted = true; }
                // don't add any lines that are deleted and have not already been saved.
                if (l.isDeleted == false || (l.isDeleted && l.OrderDetailID > 0)) { OH.OrderDetailLine = l; }
            }


            return OH;
        }
    }
}