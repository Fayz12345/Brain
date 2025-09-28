using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Syncfusion.Web.UI.WebControls.Shared;

//using Factory_DataModel;
using BW_WebApp.DataManagers;

namespace BW_WebApp
{
    public partial class Dashboard_Dealer : System.Web.UI.Page
    {
        clsLog log;

        protected void Page_Load(object sender, EventArgs e)
        {
            log = new clsLog(Server.MapPath("~"), "WebServer_01_Log.txt", User.Identity.Name, System.Configuration.ConfigurationManager.AppSettings["WriteLogUserNameToLog"]);
            if (System.Configuration.ConfigurationManager.AppSettings["WriteLog"].ToUpper() == "TRUE")
            {
                log.writeLogData = true;
            }
            log.LogIt("**** Dashboard Dealer Page Load Event started");


            string[] UserIPAddress;
            UserIPAddress = Request.ServerVariables.GetValues("HTTP_X_FORWARDED_FOR");
            if (UserIPAddress == null || UserIPAddress.Length == 0) { UserIPAddress = Request.ServerVariables.GetValues("REMOTE_ADDR"); }
            if (UserIPAddress != null && UserIPAddress.Count() > 0) { hdnUserIPAddress.Value = UserIPAddress[0]; }

            hdnUserName.Value = User.Identity.Name;
            gvDashboardClosed.RowDataBound += new GridViewRowEventHandler(gvDashboard_RowDataBound);
            gvDashboard.RowDataBound += new GridViewRowEventHandler(gvDashboard_RowDataBound);
            //gvDashboard.RowCommand += new GridViewCommandEventHandler(gvDashboard_RowCommand);

            txtAuthorizationName.Attributes.Add("onkeyup", "NameChange();");
            //txtAuthorizationName.Attributes.Add("onchange", "NameChange();");

            //grdAuthorization.RowDataBound += new GridViewRowEventHandler(grdAuthorization_RowDataBound);
            //grdAuthorization.RowCommand += new GridViewCommandEventHandler(grdAuthorization_RowCommand);
            btnRefresh.Click += new EventHandler(btnRefresh_Click);

            drpClientLocations.SelectedIndexChanged += new EventHandler(drpClientLocations_SelectedIndexChanged);
            lblName.Text = "Dashboard:" + User.Identity.Name;
            if (!IsPostBack)
            {

                ClientManager cm = new ClientManager(User.Identity.Name);

                string PortalName = cm.GetUserDealerPortalName().ToUpper();
                // Get the Master Client Dealer Dashboard PortalName 
                //     
                //
                //
                //
                if (PortalName == "DP_01")
                {
                    // Open New Page.
                    Response.Redirect("~/Dashboard/Dashboard_Dealer_01.aspx");
                }
                if (PortalName == "DP_02")
                {
                    // Open New Page.
                    Response.Redirect("~/Dashboard/Dashboard_Dealer_02.aspx");
                }

                drpClientLocations.DataValueField = "ClientLocationID";
                drpClientLocations.DataTextField = "CompanyName";
                drpClientLocations.DataSource = cm.DropDownSearchLocationsList("", "", "", "").OrderBy(x => x.CompanyName);
                drpClientLocations.DataBind();
                drpClientLocations.SelectedIndex = 0;

                LoadDashboard();
                //LoadAthorization();
            }
        }

        void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadDashboard();
            //LoadAthorization();
        }




        //void grdAuthorization_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    //throw new NotImplementedException();
        //}


        //void grdAuthorization_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        ReceiveDetailAuthorizationLog rda = ((ReceiveDetailAuthorizationLog)e.Row.DataItem);
        //        System.Web.UI.WebControls.ImageButton lb = (System.Web.UI.WebControls.ImageButton)e.Row.FindControl("imgAuthorize");
        //        if (lb != null)
        //        {
        //            lb.Visible = false;
        //            if (rda.AuthorizedDate == null && rda.DeclinedDate == null && rda.RejectedDate == null)
        //            {
        //                string AllowAuthorization = "true";
        //                if (rda.AuthorizedDate != null || rda.DeclinedDate != null) { AllowAuthorization = "false"; }
        //                lb.Visible = true;
        //                lb.CommandName = "Authorize";
        //                lb.CommandArgument = rda.ReceiveDetailAuthorizationLogID.ToString();
        //                lb.OnClientClick = "OpenUnitSummary(" +
        //                    rda.ReceiveDetailID.ToString() + "," +
        //                    rda.ReceiveDetailAuthorizationLogID.ToString() +
        //                    "," + AllowAuthorization + ");return false";
        //            }
        //        }
        //        System.Web.UI.WebControls.ImageButton lb1 = (System.Web.UI.WebControls.ImageButton)e.Row.FindControl("imgPrint");
        //        if (lb1 != null)
        //        {
        //            lb1.Visible = false;
        //            if ((rda.AuthorizedDate != null || rda.DeclinedDate != null) && rda.RejectedDate == null)
        //            {
        //                lb1.Visible = true;
        //                lb1.CommandName = "Print";
        //                lb1.CommandArgument = rda.ReceiveDetailAuthorizationLogID.ToString();
        //                lb1.OnClientClick = "OpenAuthorizeReport(" + rda.ReceiveDetailAuthorizationLogID.ToString() + ");return false";
        //            }
        //        }
        //        Label lab = (Label)e.Row.FindControl("lblDelcined");
        //        if (lab != null)
        //        {
        //            lab.Text = "";
        //            if (rda.DeclinedDate != null)
        //            {
        //                lab.Text = rda.DeclinedBy.ToString() + " " + rda.DeclinedDate.ToString();
        //            }
        //        }
        //        Label lab4 = (Label)e.Row.FindControl("lblAuthorized");
        //        if (lab4 != null)
        //        {
        //            lab4.Text = "";
        //            if (rda.AuthorizedDate != null)
        //            {
        //                lab4.Text = rda.AuthorizedBy.ToString() + " " + rda.AuthorizedDate.ToString();
        //            }
        //        }

        //        Label lab5 = (Label)e.Row.FindControl("lblReceived");
        //        if (lab5 != null)
        //        {
        //            lab5.Text = "";
        //            if (rda.ReceivedDate != null)
        //            {
        //                lab5.Text = rda.ReceivedBy.ToString() + " " + rda.ReceivedDate.ToString();
        //            }
        //        }

        //        Label Status = (Label)e.Row.FindControl("lblStatus");
        //        if (Status != null)
        //        {
        //            if (rda.AuthorizedDate != null) { Status.Text = "Authorized"; }
        //            else if (rda.RejectedDate != null) { Status.Text = "Rejected"; }
        //            else if (rda.ReceivedDate != null) { Status.Text = "Authorization Received"; }
        //            else if (rda.DeclinedDate != null) { Status.Text = "Declined"; }
        //            else { Status.Text = "Current"; }
        //        }


        //    }
        //}




        void gvDashboard_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //System.Web.UI.WebControls.ImageButton bPrint = (System.Web.UI.WebControls.ImageButton)e.CommandSource;
            //ReceiveDetailManager rdm = new ReceiveDetailManager(User.Identity.Name);
            //decimal id = -1;
            //if (decimal.TryParse(bPrint.CommandArgument, out id) == false) { id = -1; }
            //grdAuthorize.DataSource = rdm.AuthorizationLog(id);
            //grdAuthorize.DataBind();

            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //sb.Append(@"<script language='javascript'>");
            //sb.Append(@"OpenUnitSummary(" + id.ToString() + ",-1);");
            //sb.Append(@"</script>");
            //ScriptManager.RegisterStartupScript(gvDashboard, this.GetType(), "OpenUnit", sb.ToString(), false);
        }
        void gvDashboard_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridDashboardReceiveDetail_Client grda = ((GridDashboardReceiveDetail_Client)e.Row.DataItem);
                //Type type = e.Row.DataItem.GetType();               //.GetGenericArguments()[0]; 
                System.Web.UI.WebControls.ImageButton bPrint = (System.Web.UI.WebControls.ImageButton)e.Row.FindControl("imgOpen");
                if (bPrint != null)
                {

                    //bPrint.ToolTip = grda.ReceiveDetailAuthorizationLogID.ToString();

                    bPrint.CommandName = "OpenSummary";
                    bPrint.CommandArgument = ((GridDashboardReceiveDetail_Client)e.Row.DataItem).ReceiveDetailID.ToString();
                    bPrint.OnClientClick = "OpenUnitSummary(" +
                        ((GridDashboardReceiveDetail_Client)e.Row.DataItem).ReceiveDetailID.ToString() + "," +
                        ((GridDashboardReceiveDetail_Client)e.Row.DataItem).AuthorizationLogID.ToString() +
                        //",true,'jim');return false";
                    ",false,'');return false";
                }

                System.Web.UI.WebControls.ImageButton lb = (System.Web.UI.WebControls.ImageButton)e.Row.FindControl("imgAuthorize");
                ReceiveDetailAuthohrizationManager rdam = new ReceiveDetailAuthohrizationManager(User.Identity.Name);
                ReceiveDetailAuthorizationLog rda = rdam.GetAuthorizationLog(grda.AuthorizationLogID);

                if (lb != null)
                {
                    lb.Visible = false;
                    if (rda != null)
                    {
                        if (rda.AuthorizedDate == null && rda.DeclinedDate == null && rda.RejectedDate == null)
                        {


                            decimal ClientLocationID = -1;
                            if (drpClientLocations.SelectedItem != null)
                            {
                                if (decimal.TryParse(drpClientLocations.SelectedItem.Value, out ClientLocationID) == false) { ClientLocationID = -1; }
                            }
                            ClientLocation cl = rdam.GetClientLocation(ClientLocationID);
                            string AllowAuthorization = "true";
                            if (rda.AuthorizedDate != null || rda.DeclinedDate != null) { AllowAuthorization = "false"; }
                            lb.Visible = true;
                            lb.CommandName = "Authorize";
                            lb.CommandArgument = rda.ReceiveDetailAuthorizationLogID.ToString();
                            lb.ToolTip = "Open Unit Summary";


                            //lb.OnClientClick = "OpenUnitSummary(" +

                            //    //rda.ReceiveDetailID.ToString() + "," +
                            //    //rda.ReceiveDetailAuthorizationLogID.ToString() +

                            //  ((GridDashboardReceiveDetail_Client)e.Row.DataItem).ReceiveDetailID.ToString() + "," +
                            //  ((GridDashboardReceiveDetail_Client)e.Row.DataItem).AuthorizationLogID.ToString() +

                            //    "," + AllowAuthorization + ",'" + cl.ApprovalPassword.ToString() + "');return false";


                            string PWord = "xFb567345";
                            if (cl.ApprovalPassword != null)
                            {
                                PWord = cl.ApprovalPassword;
                            }
                            lb.OnClientClick = "OpenUnitSummary(" +

                                //rda.ReceiveDetailID.ToString() + "," +
                                //rda.ReceiveDetailAuthorizationLogID.ToString() +

                              ((GridDashboardReceiveDetail_Client)e.Row.DataItem).ReceiveDetailID.ToString() + "," +
                              ((GridDashboardReceiveDetail_Client)e.Row.DataItem).AuthorizationLogID.ToString() +

                                "," + AllowAuthorization + ",'" + PWord + "');return false";


                            btnAuthorize.CommandArgument = PWord;
                            btnDecline.CommandArgument = PWord;
                        }
                    }

                }
                System.Web.UI.WebControls.ImageButton lb1 = (System.Web.UI.WebControls.ImageButton)e.Row.FindControl("imgPrint");
                if (lb1 != null)
                {
                    lb1.Visible = false;
                    if (rda != null)
                    {
                        if ((rda.AuthorizedDate != null || rda.DeclinedDate != null) && rda.RejectedDate == null)
                        {
                            lb1.Visible = true;
                            lb1.CommandName = "Print";
                            lb1.CommandArgument = ((GridDashboardReceiveDetail_Client)e.Row.DataItem).AuthorizationLogID.ToString();
                            //lb1.ToolTip = ((GridDashboardReceiveDetail_Client)e.Row.DataItem).ReceiveDetailAuthorizationLogID.ToString();
                            //GMPIAUTXDone
                            if (((GridDashboardReceiveDetail_Client)e.Row.DataItem).GMPIAUTXDone.Length == 0)
                            {
                                lb1.ToolTip = "Print Authorization Form";
                                lb1.OnClientClick = "OpenAuthorizeReport(" + ((GridDashboardReceiveDetail_Client)e.Row.DataItem).AuthorizationLogID.ToString() + ");return false";
                            }
                            else
                            {
                                lb1.ToolTip = "Print Repair Form";
                                lb1.OnClientClick = "OpenRepairForm('R'," + ((GridDashboardReceiveDetail_Client)e.Row.DataItem).AuthorizationLogID.ToString() + ");return false";
                            }
                        }
                    }
                    else
                    {
                        lb1.Visible = true;
                        lb1.CommandName = "Print";
                        lb1.CommandArgument = ((GridDashboardReceiveDetail_Client)e.Row.DataItem).ReceiveDetailID.ToString();
                        lb1.ToolTip = "Print Submission Form";
                        lb1.OnClientClick = "OpenClientbagTag(" + ((GridDashboardReceiveDetail_Client)e.Row.DataItem).ReceiveDetailID.ToString() + ");return false";
                    }
                }
            }
        }


        void drpClientLocations_SelectedIndexChanged(object sender, EventArgs e)
        {
            //LoadDashboard();
            //LoadAthorization();
        }


        void LoadDashboard()
        {
            decimal ClientID = -1;
            decimal ClientLocationID = -1;
            if (drpClientLocations.SelectedItem != null)
            {
                if (decimal.TryParse(drpClientLocations.SelectedItem.Value, out ClientLocationID) == false) { ClientLocationID = -1; }
            }
            ReceiveDetailManager rm = new ReceiveDetailManager(User.Identity.Name);
            string[] DataKeys = new string[] { "ReceiveDetailID" };
            gvDashboard.DataKeyNames = DataKeys;
            gvDashboard.DataSource = rm.GetDashboardClientView(ClientID, ClientLocationID).OrderByDescending(x => x.UnitStatus);
            gvDashboard.DataBind();

            gvDashboardClosed.DataKeyNames = DataKeys;
            gvDashboardClosed.DataSource = rm.GetDashboardClientViewClosed(ClientID, ClientLocationID).OrderByDescending(x => x.UnitStatus); ;
            gvDashboardClosed.DataBind();

        }
    }
}