using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using Syncfusion.XlsIO;
using Syncfusion.Web.UI.WebControls.Shared;

//using Factory_DataModel;
using BW_WebApp.DataManagers;

namespace BW_WebApp
{
    public partial class Dashboard_TechLab : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            hdnUsername.Value = User.Identity.Name;
            btnRefresh.Click += new EventHandler(btnRefresh_Click);
            imgPrintListWaiting.Click += new EventHandler(imgPrintListWaiting_Click);
            gvWaiting.RowDataBound += new GridViewRowEventHandler(gvWaiting_RowDataBound);
            gvWaiting.RowCommand += new GridViewCommandEventHandler(gvWaiting_RowCommand);

            gvAWaiting.RowDataBound += new GridViewRowEventHandler(gvWaiting_RowDataBound);
            gvAWaiting.RowCommand += new GridViewCommandEventHandler(gvWaiting_RowCommand);

            vgADeclined.RowDataBound += new GridViewRowEventHandler(gvWaiting_RowDataBound);
            vgADeclined.RowCommand += new GridViewCommandEventHandler(gvWaiting_RowCommand);

            gvAApproved.RowDataBound += new GridViewRowEventHandler(gvWaiting_RowDataBound);
            gvAApproved.RowCommand += new GridViewCommandEventHandler(gvWaiting_RowCommand);

            gvAComplete.RowDataBound += new GridViewRowEventHandler(gvWaiting_RowDataBound);
            gvAComplete.RowCommand += new GridViewCommandEventHandler(gvWaiting_RowCommand);

            gvAcquired.RowDataBound += new GridViewRowEventHandler(gvWaiting_RowDataBound);
            gvAcquired.RowCommand += new GridViewCommandEventHandler(gvWaiting_RowCommand);

            gvWaitingParts.RowDataBound += new GridViewRowEventHandler(gvWaiting_RowDataBound);
            gvWaitingParts.RowCommand += new GridViewCommandEventHandler(gvWaiting_RowCommand);

            gvClosed.RowDataBound += new GridViewRowEventHandler(gvWaiting_RowDataBound);
            gvClosed.RowCommand += new GridViewCommandEventHandler(gvWaiting_RowCommand);

            if (!IsPostBack)
            {
                clsLinqDataContext ctx = new clsLinqDataContext();


                ClientManager cm = new ClientManager(User.Identity.Name);
                List<Client> cl = cm.SearchClientList("", "", "").OrderBy(x => x.CompanyName).ToList();
                drpClientList.Items.Clear();
                //ListItem z = new ListItem("All", "-1");
                //drpClientList.Items.Add(z);
                foreach (Client p in cl)
                {
                    ListItem x = new ListItem(p.CompanyName, p.ClientID.ToString());
                    drpClientList.Items.Add(x);
                }
                ///////////////////////////////////////////////////////////////////////
                //ESNFound.Visible = false;
            }

        }

        void gvWaiting_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            System.Web.UI.WebControls.LinkButton btn = (System.Web.UI.WebControls.LinkButton)e.CommandSource;
            string CommandArgument = btn.CommandArgument;
            string CommandName = btn.CommandName;
            string[] data = CommandArgument.Split(',');

            if (CommandName.ToUpper() == "OPEN")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Open Unit", "OpenUnit(" + data[0] + "," + data[1] + ",'" + data[2] + "');", true);
            }
            if (CommandName.ToUpper() == "AUTHORIZE")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Authorize", "SetupToAuthorize(" + data[0] + "," + data[1] + ",'" + data[2] + "');", true);
                //ScriptManager.RegisterStartupScript(this, GetType(), "Authorize", "OpenAuthorization(" + data[0] + "," + data[1] + ",'" + data[2] + "');", true);
            }
            if (CommandName.ToUpper() == "DECLINE")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Authorize", "SetupToDecline(" + data[0] + "," + data[1] + ",'" + data[2] + "');", true);
            }
            if (CommandName.ToUpper() == "COMPLETE")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Authorize", "SetupToComplete(" + data[0] + "," + data[1] + ",'" + data[2] + "');", true);
            }
        }

        void gvWaiting_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Type type = e.Row.DataItem.GetType();               //.GetGenericArguments()[0]; 
                GridTechLabDashboardReceiveDetail_Client Data = ((GridTechLabDashboardReceiveDetail_Client)e.Row.DataItem);
                System.Web.UI.WebControls.LinkButton bPrint = (System.Web.UI.WebControls.LinkButton)e.Row.FindControl("imgOpen");
                if (bPrint != null)
                {
                    bPrint.CommandName = "Open";
                    bPrint.CommandArgument = Data.ReceiveDetailID.ToString() + "," + Data.ProjectID.ToString() + "," + Data.ProcessName;
                }
                bPrint = (System.Web.UI.WebControls.LinkButton)e.Row.FindControl("imgComplete");
                if (bPrint != null)
                {
                    bPrint.CommandName = "Complete";
                    bPrint.CommandArgument = Data.ReceiveDetailAuthorizationLogID.ToString() + "," + Data.ReceiveDetailID.ToString() + "," + User.Identity.Name;
                }
                bPrint = (System.Web.UI.WebControls.LinkButton)e.Row.FindControl("imgAuthorize");
                if (bPrint != null)
                {
                    bPrint.CommandName = "Authorize";
                    bPrint.CommandArgument = Data.ReceiveDetailAuthorizationLogID.ToString() + "," + Data.ReceiveDetailID.ToString() + "," + User.Identity.Name;
                }
                bPrint = (System.Web.UI.WebControls.LinkButton)e.Row.FindControl("imgDecline");
                if (bPrint != null)
                {
                    bPrint.CommandName = "Decline";
                    bPrint.CommandArgument = Data.ReceiveDetailAuthorizationLogID.ToString() + "," + Data.ReceiveDetailID.ToString() + "," + User.Identity.Name;
                }
                System.Web.UI.HtmlControls.HtmlGenericControl email = (System.Web.UI.HtmlControls.HtmlGenericControl)e.Row.FindControl("divEmail");
                if (email != null)
                {
                    email.InnerHtml = Data.SendEmailURL;
                }
            }
        }

        #region PrintGrid to XLS


        void imgPrintListWaiting_Click(object sender, EventArgs e)
        {

            return;


            //GridView gv;
            //// get the client.
            //string ClientName = drpClientList.SelectedItem.Text;
            //// Get the Tab 
            //string ListName = TabList.ActiveTab.HeaderText;
            //if (TabList.ActiveTab.ID.ToUpper() == "TABWAITING") { gv = gvWaiting; }
            //else if (TabList.ActiveTab.ID.ToUpper() == "TABAWAITING") { gv = gvAWaiting; }
            //else if (TabList.ActiveTab.ID.ToUpper() == "TABADECLINE") { gv = vgADeclined; }
            //else if (TabList.ActiveTab.ID.ToUpper() == "TABAAPPROVED") { gv = gvAApproved; }
            //else if (TabList.ActiveTab.ID.ToUpper() == "TABACOMPLETE") { gv = gvAComplete; }
            //else if (TabList.ActiveTab.ID.ToUpper() == "TABACQUIRED") { gv = gvAcquired; }
            //else if (TabList.ActiveTab.ID.ToUpper() == "TABWAITINGPARTS") { gv = gvWaitingParts; }
            //else if (TabList.ActiveTab.ID.ToUpper() == "TABCLOSED") { gv = gvClosed; }
            //else { return; }

            //// Create a new xls file.
            //ExcelEngine excelEngine = new ExcelEngine();
            //IApplication application = excelEngine.Excel;
            //IWorkbook workbook = application.Workbooks.Create(1);
            //IWorksheet sheet = workbook.Worksheets[0];

            //int i = 1;

            //sheet.Range[i, 1, i, gv.Columns.Count].Text = ClientName;
            //sheet.MigrantRange[i, 1, i, gv.Columns.Count].Merge();

            //i++;
            //sheet.Range[i, 1, i, gv.Columns.Count].Text = ListName;
            //sheet.MigrantRange[i, 1, i, gv.Columns.Count].Merge();

            //i++;
            //sheet.Range[i, 1, i, gv.Columns.Count].Text = "As of " + DateTime.Now.ToLocalTime();
            //sheet.MigrantRange[i, 1, i, gv.Columns.Count].Merge();

            //i+= 3;

            //foreach (GridViewRow r in gv.Rows)
            //{
            //    for (int c = 1; c <= gv.Columns.Count;c++ )
            //    {
            //        sheet.Range[i, c].Text = r.Cells[c-1].Text;
            //    }
            //    i++;
            //}

            //// load client to xls
            //// load tab and date and time to xls
            //// loop through the grid and fill each xls cell to match the grid cells.


            //// Send the xls file to the user.
            //workbook.SaveAs("TechDashboard.xls", Page.Response, ExcelDownloadType.Open);
            //workbook.Close();
            //// Dispose the Excel engine
            //excelEngine.Dispose();
            //// Done.

        }
        //private void SetExcelRange(IWorksheet sheet, ref int Row1, ref int Col1, int row2, int col2, string Text)
        //{
        //    sheet.Range[Row1, Col1, row2, col2].Text = Text;
        //    sheet.MigrantRange[Row1, Col1, row2, col2].Merge();
        //}
        #endregion


        void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadDashboard();
            //LoadAthorization();
        }


        void LoadDashboard()
        {
            decimal ClientID = -1;
            decimal ClientLocationID = -1;
            if (drpClientList.SelectedItem != null)
            {
                if (decimal.TryParse(drpClientList.SelectedItem.Value, out ClientID) == false) { ClientID = -1; }
            }

            //ClientLocationID = 34;

            ReceiveDetailManager rm = new ReceiveDetailManager(User.Identity.Name);
            var RawData = rm.GetTechLabDashboardClientView(ClientID, ClientLocationID);

            string[] DataKeys = new string[] { "ReceiveDetailID" };
            gvWaiting.DataKeyNames = DataKeys;
            gvWaiting.DataSource = RawData.Where(x => x.AuthorizationStatus.ToUpper() == "NONE" || x.AuthorizationStatus.Length == 0).OrderByDescending(x => x.ProcessDays).ThenBy(x => x.ProjectName).ThenBy(x => x.Name);
            gvWaiting.DataBind();

            gvAWaiting.DataKeyNames = DataKeys;
            gvAWaiting.DataSource = RawData.Where(x => x.AuthorizationStatus.ToUpper() == "REQUIRED").OrderByDescending(x => x.ProcessDays).ThenBy(x => x.ProjectName).ThenBy(x => x.Name);
            gvAWaiting.DataBind();

            vgADeclined.DataKeyNames = DataKeys;
            vgADeclined.DataSource = RawData.Where(x => x.AuthorizationStatus.ToUpper() == "DECLINED").OrderByDescending(x => x.ProcessDays).ThenBy(x => x.ProjectName).ThenBy(x => x.Name);
            vgADeclined.DataBind();

            gvAApproved.DataKeyNames = DataKeys;
            gvAApproved.DataSource = RawData.Where(x => x.AuthorizationStatus.ToUpper() == "APPROVED").OrderByDescending(x => x.ProcessDays).ThenBy(x => x.ProjectName).ThenBy(x => x.Name);
            gvAApproved.DataBind();

            gvAComplete.DataKeyNames = DataKeys;
            gvAComplete.DataSource = RawData.Where(x => x.AuthorizationStatus.ToUpper() == "RP-COMPLETE").OrderByDescending(x => x.ProcessDays).ThenBy(x => x.ProjectName).ThenBy(x => x.Name);
            gvAComplete.DataBind();

            gvAcquired.DataKeyNames = DataKeys;
            gvAcquired.DataSource = RawData.Where(x => x.AuthorizationStatus.ToUpper() == "ACQUIRED").OrderByDescending(x => x.ProcessDays).ThenBy(x => x.ProjectName).ThenBy(x => x.Name);
            gvAcquired.DataBind();


            gvWaitingParts.DataKeyNames = DataKeys;
            gvWaitingParts.DataSource = RawData.Where(x => x.AuthorizationStatus.ToUpper() == "WAITING FOR PARTS").OrderByDescending(x => x.ProcessDays).ThenBy(x => x.ProjectName).ThenBy(x => x.Name);
            gvWaitingParts.DataBind();

            gvClosed.DataKeyNames = DataKeys;
            gvClosed.DataSource = rm.GetTechLabDashboardClientViewClosed(ClientID, ClientLocationID).OrderByDescending(x => x.ProcessDays).ThenBy(x => x.ProjectName).ThenBy(x => x.Name);
            gvClosed.DataBind();
        }
    }
}