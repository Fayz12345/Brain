using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
//using Factory_Businesslayer;
using BW_WebApp.Classes;
using BW_WebApp.DataManagers;
using System.Reflection;

namespace BW_WebApp
{
    public partial class SearchOrder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnSearch.Click += new EventHandler(btnSearch_Click);
            grdTempDetail.RowDataBound += new GridViewRowEventHandler(grdTempDetail_RowDataBound);
            grdTempDetail.RowCommand += new GridViewCommandEventHandler(grdTempDetail_RowCommand);
            if (!IsPostBack)
            {
                //ProjectManager pm = new ProjectManager(User.Identity.Name);
                //List<Project> pl = pm.GetProjectList();
                //drpProjectList.Items.Clear();
                //ListItem z = new ListItem("All", "-1");
                //drpProjectList.Items.Add(z);
                //foreach (Project p in pl)
                //{
                //    ListItem x = new ListItem(p.Name, p.ProjectID.ToString());
                //    drpProjectList.Items.Add(x);
                //}


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

                //QuestionManager qm = new QuestionManager(User.Identity.Name);
                //List<Option> ol = qm.GetQuestionOptionList("Carrier");
                //drpCarrier.Items.Clear();
                //z = new ListItem("All", "-1");
                //drpCarrier.Items.Add(z);
                //foreach (Option o in ol)
                //{
                //    ListItem x = new ListItem(o.OptionText, o.OptionID.ToString());
                //    drpCarrier.Items.Add(x);
                //}

                //ol = qm.GetQuestionOptionList("Manufacturer");
                //drpManufacturer.Items.Clear();
                //z = new ListItem("All", "-1");
                //drpManufacturer.Items.Add(z);
                //foreach (Option o in ol)
                //{
                //    ListItem x = new ListItem(o.OptionText, o.OptionID.ToString());
                //    drpManufacturer.Items.Add(x);
                //}

                //ol = qm.GetQuestionOptionList("Model");
                //drpModel.Items.Clear();
                //z = new ListItem("All", "-1");
                //drpModel.Items.Add(z);
                //foreach (Option o in ol)
                //{
                //    ListItem x = new ListItem(o.OptionText, o.OptionID.ToString());
                //    drpModel.Items.Add(x);
                //}

                //ol = qm.GetQuestionOptionList("Colour");
                //drpColour.Items.Clear();
                //z = new ListItem("All", "-1");
                //drpColour.Items.Add(z);
                //foreach (Option o in ol)
                //{
                //    ListItem x = new ListItem(o.OptionText, o.OptionID.ToString());
                //    drpColour.Items.Add(x);
                //}

                //txtBeginDate.Text = DateTime.Now.AddDays(-7).ToShortDateString();
                //txtEndDate.Text = DateTime.Now.ToShortDateString();

                //txtBeginQC.Text = DateTime.Now.AddDays(-7).ToShortDateString();
                //txtEndQC.Text = DateTime.Now.ToShortDateString();

                //txtBeginShipped.Text = DateTime.Now.AddDays(-7).ToShortDateString();
                //txtEndShipped.Text = DateTime.Now.ToShortDateString();
                ////UpdateTemplateGrid("~/templates/Detail", grdTempDetail);
                ////UpdateTemplateGrid("~/templates/DetailDate", grdTempDetailDate);
                ////UpdateTemplateGrid("~/templates/Bulk", grdTempBulk);
                ////UpdateTemplateGrid("~/templates/BSP", grdTempBSP);
            }
        }

        void grdTempDetail_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            LinkButton btnOpen = (LinkButton)e.CommandSource;
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



            if (btnOpen.ID == "imgAnalyze")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "NYI", "alert('Not Yet Implemented:" + id + "," + Status + "');", true);
            }   

            //if (btnOpen.ID.ToUpper() == "IMGVERSION")
            //{
            //    OrderManager om = new OrderManager(User.Identity.Name);
            //    decimal ID = -1;
            //    if (decimal.TryParse(id, out ID) == true)
            //    {
            //        om.Redo_OrderShipped(ID);
            //    }

            //    ScriptManager.RegisterStartupScript(this, GetType(), "NYI", "alert('Updated:" + id + "," + Status + "');", true);
            //}
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
                LinkButton bOpen = (LinkButton)e.Row.FindControl("imgOpen");
                if (bOpen != null)
                {
                    bOpen.CommandArgument = Data.OrderHeaderID.ToString() + "," + Data.Status;
                }
                LinkButton bPrint = (LinkButton)e.Row.FindControl("imgPrint");
                if (bPrint != null)
                {
                    bPrint.CommandArgument = Data.OrderHeaderID.ToString() + "," + Data.Status;
                }

                //LinkButton bimgVersion = (LinkButton)e.Row.FindControl("imgVersion");
                //if (bimgVersion != null)
                //{
                //    bimgVersion.CommandArgument = Data.OrderHeaderID.ToString() + "," + Data.Status;
                //}
                LinkButton bimgAnalyze = (LinkButton)e.Row.FindControl("imgAnalyze");
                if (bimgAnalyze != null)
                {
                    bimgAnalyze.CommandArgument = Data.OrderHeaderID.ToString() + "," + Data.Status;
                }
            }
        }
        void btnSearch_Click(object sender, EventArgs e)
        {
            string Status = "";
            if (drpStatus.SelectedIndex > 0) { Status = drpStatus.SelectedItem.Text; }
            string OrderNumber = txtOrderNumber.Text;
            string IFSOrderNumber = txtIFSOrderNumber.Text;
            string CustomerPO = txtCustomerPO.Text;
            string WayBillNumber = txtWaybillNumber.Text;
            string ProjectTag = txtProjectTag.Text;
            string CompanyName = txtClient.Text;
            string City = txtCity.Text;
            string PostalCode = txtPostalCode.Text;
            string PhoneNumber = txtPhoneNumber.Text;
            string EmailAddress = txtEmailAddress.Text;
            string OrderBeginDate = txtBeginDate.Text;
            string OrderEndDate = txtEndDate.Text;
            if (chkReceived.Checked == false) { OrderBeginDate = ""; OrderEndDate = ""; }

            OrderManager rdm = new OrderManager(User.Identity.Name);
            List<GetOrderEntryList_TemplateRawData_SearchGrid_01Result> Data = rdm.GetOrderEntryList(Status, IFSOrderNumber, OrderNumber, CustomerPO, WayBillNumber, ProjectTag, CompanyName, City, PostalCode, PhoneNumber, EmailAddress, OrderBeginDate, OrderEndDate);
            grdTempDetail.DataSource = Data;
            grdTempDetail.DataBind();
        }
        public string ParamaterString_Raw()
        {
            string rString = "";

            //string ProjectName = drpProjectList.SelectedItem.Text;

            //BasicUserUtilities buu = new BasicUserUtilities(User.Identity.Name);
            //string ClientKey = buu.GetUserDefaultClientKey(User.Identity.Name);
            //string ProjectTag = "";      // txtProjectTag.Text;
            //string RMANumber = "";          // txtRMA.Text;

            //string BeginDateString = txtBeginDate.Text;
            //string EndDateString = txtEndDate.Text;
            //string QCBeginDateString = txtBeginQC.Text;
            //string QCEndDateString = txtEndQC.Text;
            //string ShippedBeginDateString = txtBeginShipped.Text;
            //string ShippedEndDateString = txtEndShipped.Text;
            //string BinNumber = "";           // txtBinNumber.Text;
            //string Hobble = "";           // txtHobble.Text;

            //string sStatus = drpStatus.SelectedItem.Text;
            //string sClient = "";
            //string sIMEI = txtIMEI.Text;
            //string sCarrier = drpCarrier.SelectedItem.Text;
            //string sManufacturer = drpManufacturer.SelectedItem.Text;
            //string sModel = drpModel.SelectedItem.Text;
            //string sColour = drpColour.SelectedItem.Text;
            //string sSKU = txtSKU.Text;

            //if (ProjectName.ToUpper() == "ALL") { ProjectName = ""; }
            //if (sStatus.ToUpper() == "ALL") { sStatus = ""; }
            //if (sCarrier.ToUpper() == "ALL") { sCarrier = ""; }
            //if (sManufacturer.ToUpper() == "ALL") { sManufacturer = ""; }
            //if (sModel.ToUpper() == "ALL") { sModel = ""; }
            //if (sColour.ToUpper() == "ALL") { sColour = ""; }

            //string ShowGraveYard = "N";


            //// if (chkShowGraveyard.Checked == true) { ShowGraveYard = "Y"; }
            //if (chkReceived.Checked == false) { BeginDateString = ""; EndDateString = ""; }
            //if (chkQC.Checked == false) { QCBeginDateString = ""; QCEndDateString = ""; }
            //if (chkShipped.Checked == false) { ShippedBeginDateString = ""; ShippedEndDateString = ""; }

            //rString = " '" + ProjectName + "',";
            //rString += "'" + ClientKey + "',";
            //rString += "'" + RMANumber + "',";
            //rString += "'" + ProjectTag + "',";
            //rString += "'" + BeginDateString + "',";
            //rString += "'" + EndDateString + "',";
            //rString += "'" + QCBeginDateString + "',";
            //rString += "'" + QCEndDateString + "',";
            //rString += "'" + ShippedBeginDateString + "',";
            //rString += "'" + ShippedEndDateString + "',";
            //rString += "'" + BinNumber + "',";
            //rString += "'" + Hobble + "',";

            //rString += "'" + sStatus + "',";
            //rString += "'" + sClient + "',";
            //rString += "'" + sIMEI + "',";
            //rString += "'" + sCarrier + "',";
            //rString += "'" + sManufacturer + "',";
            //rString += "'" + sModel + "',";
            //rString += "'" + sColour + "',";
            //rString += "'" + sSKU + "',";


            //rString += "'" + ShowGraveYard + "'";
            return rString;

            //// @mProjectName nvarchar(50) = '',
            //// @mClientCode nvarchar(50) = '',        -- Location/Dealer	
            //// @mRMANumber nvarchar(50) = '',
            //// @mProjectTag nvarchar(50) = '',
            //// @mReceiveBeginDate nvarchar(10) = '',  -- Rx Date	
            //// @mReceiveEndDate nvarchar(10) = '',
            //// @mQCBeginDate nvarchar(10) = '',    
            //// @mQCEndDate nvarchar(10) = '',  
            //// @mShippedBeginDate nvarchar(10) = '',    
            //// @mShippedEndDate nvarchar(10) = '',
            //// @mBinNumber nvarchar(50) = '',  
            //// @mHobble nvarchar(50) = '',  
            //// -- Aditional Template fields.
            //// @mStatus nvarchar(50) = '',
            //// @mClient nvarchar(50) = '',  
            //// @mIMEI nvarchar(50) = '',  	
            //// @mCarrier nvarchar(50) = '',  
            //// @mManufacturer nvarchar(50) = '',  	
            //// @mModel nvarchar(50) = '',  
            //// @mColour nvarchar(50) = '',  
            //// @mSKU nvarchar(50) = '',  
            //// @mShowGraveyard char(1) = 'N'   


        }




    }
}