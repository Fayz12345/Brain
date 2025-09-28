using System;
using System.Drawing;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
//using Factory_DataModel;
using BW_WebApp.DataManagers;
// using DAL;
using Syncfusion.Web.UI.WebControls.Shared;
using Syncfusion.XlsIO;

using System.Text;

namespace BW_WebApp.Maintenance
{
    public partial class Maint_Client : System.Web.UI.Page
    {
        //private string blank = "&nbsp;";

        ClientManager CM = null;
        clsLinqDataContext ctx = null;

        protected void Page_Load(object sender, EventArgs e)
        {

            CM = new ClientManager(User.Identity.Name);



            btnMoveClientLocation.Click += new EventHandler(btnMoveClientLocation_Click);

            MainGrid.SelectedIndexChanged += new EventHandler(MainGrid_SelectedIndexChanged);
            ChildGrid.SelectedIndexChanged += new EventHandler(ChildGrid_SelectedIndexChanged);
            MainGrid.RowDataBound += new GridViewRowEventHandler(MainGrid_RowDataBound);


            btnUpload.Click += new EventHandler(btnUpload_Click);
            btnDownload.Click += new EventHandler(btnDownload_Click);
            //btnBillingPoint.Click += new EventHandler(btnBillingPoint_Click);


            #region OtherTabs
            //grdBillingPoints.RowDataBound += new GridViewRowEventHandler(grdBillingPoints_RowDataBound);
            //btnSaveBillingPoints.Click += new EventHandler(btnSaveBillingPoints_Click);
            //btnLoadBillingPointsDefault.Click += new EventHandler(btnLoadBillingPointsDefault_Click);


            //grdProjectList.RowDataBound += new GridViewRowEventHandler(grdProjectList_RowDataBound);
            //btnSaveAllowedProject.Click += new EventHandler(btnSaveAllowedProject_Click);

            //grdProcessList.RowDataBound += new GridViewRowEventHandler(grdProcessList_RowDataBound);
            //btnSaveAllowedProcesses.Click += new EventHandler(btnSaveAllowedProcesses_Click);


            #endregion

            #region QuestionAnswerRestrictions
            //btnUpdateARestriction.Click += new EventHandler(btnUpdateARestriction_Click);
            //btnUpdateQRestriction.Click += new EventHandler(btnUpdateQRestriction_Click);
            //grdAnswers.RowDataBound += new GridViewRowEventHandler(grdAnswers_RowDataBound);
            //drpQuestion.SelectedIndexChanged += new EventHandler(drpQuestion_SelectedIndexChanged);
            //drpProject.SelectedIndexChanged += new EventHandler(drpProject_SelectedIndexChanged);
            #endregion

            if (!IsPostBack)
            {

                //ESNFound.Visible = false;

                ClientManager cm = new ClientManager(User.Identity.Name);
                pnlAdd.Visible = false;
                pnlEdit.Visible = false;
                pnlChild.Visible = false;
                TabClientLocation.Visible = false;
                tabChild.Visible = false;
                pnlAddLocation.Visible = false;
                pnlEditLocation.Visible = false;

                drpAddStatus.DataValueField = "ClientStatusID";
                drpAddStatus.DataTextField = "Status";

                drpEditStatus.DataValueField = "ClientStatusID";
                drpEditStatus.DataTextField = "Status";
                drpAddStatus.DataSource = cm.GetClientStatusList();
                drpEditStatus.DataSource = cm.GetClientStatusList();
                drpAddStatus.DataBind();
                drpEditStatus.DataBind();


                drpAddLStatus.DataValueField = "ClientLocationStatusID";
                drpAddLStatus.DataTextField = "Status";
                drpEditLStatus.DataValueField = "ClientLocationStatusID";
                drpEditLStatus.DataTextField = "Status";
                drpAddLStatus.DataSource = cm.GetClientLocationStatusList();
                drpEditLStatus.DataSource = cm.GetClientLocationStatusList();
                drpAddLStatus.DataBind();
                drpEditLStatus.DataBind();


                drpClientList.DataValueField = "ClientID";
                drpClientList.DataTextField = "CompanyName";
                drpClientList.DataSource = cm.SearchClientList("", "", "");
                drpClientList.DataBind();



                drpEditPortalName.Items.Add("Default");
                drpEditPortalName.Items.Add("DP_01");
                drpEditPortalName.Items.Add("DP_02");

                drpAddPortalName.Items.Add("Default");
                drpAddPortalName.Items.Add("DP_01");
                drpAddPortalName.Items.Add("DP_02");

                UpdateMainGrid();
                #region QuestionAnswerRestrictions
                //UpdateProjectTabs();
                //UpdateQuestionGrid();
                //UpdateQuestionTabs();
                #endregion
                #region otherTabs
                //UpdateProjectGrid();
                //UpdateProcessGrid();
                #endregion
                //this.DataBind();
            }

            //AddLSkanKey.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13) || (event.which == 9) || (event.keyCode == 9)) {IsScanKeyOK(this);return false;}} else {return true}; ");
            AddLSkanKey.Attributes.Add("onblur", "IsScanKeyOK('Add');return false;");
            EditLSkanKey.Attributes.Add("onblur", "IsScanKeyOK('Edit');return false;");
        }




        #region otherTabs
        //protected void UpdateProcessGrid()
        //{
        //    ProcessManager pm = new ProcessManager(User.Identity.Name);
        //    grdProcessList.DataSource = pm.GetProcesssAll().OrderBy(x => x.ScanKey);
        //    grdProcessList.DataBind();
        //    ////   UpdateClientQuestionRestrictionGrid();
        //}
        //protected void UpdateProcessList(decimal ClientID)
        //{
        //    if (ClientID < 1) { lblDefault.Text = "Default Loaded"; }
        //    ClientManager cm = new ClientManager(User.Identity.Name);
        //    List<PairIDValue> RQ = cm.ClientProcessAllowedList(ClientID);        // This needs to get ProcessAllowedList, not project
        //    PairIDValue pd = new PairIDValue();
        //    foreach (GridViewRow r in grdProcessList.Rows)
        //    {
        //        decimal ProcessID = -1;
        //        HiddenField hf = (HiddenField)r.FindControl("hdnProcessID");
        //        if (decimal.TryParse(hf.Value, out ProcessID) == false) { ProcessID = -1; }
        //        string sProcessID = hf.Value;
        //        hf = (HiddenField)r.FindControl("hdnClientID");
        //        if (hf != null) { hf.Value = ClientID.ToString(); }
        //        hf = (HiddenField)r.FindControl("hdnClientProcessDependenciesID");
        //        if (hf != null) { hf.Value = "-1"; }
        //        CheckBox CB = (CheckBox)r.FindControl("chkThisProcess");
        //        if (CB != null) { CB.Checked = false; }
        //        pd = RQ.FirstOrDefault(x => x.ID == ProcessID);
        //        if (pd != null)
        //        {
        //            if (CB != null) { CB.Checked = true; hf.Value = pd.Desc; }
        //        }
        //    }
        //}
        //void grdProcessList_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        Type type = e.Row.DataItem.GetType();               //.GetGenericArguments()[0];
        //        HiddenField hf = (HiddenField)e.Row.FindControl("hdnClientProcessDependenciesID");
        //        if (hf != null) { hf.Value = "-1"; }
        //        hf = (HiddenField)e.Row.FindControl("hdnProcessID");
        //        if (hf != null) { hf.Value = ((Process)e.Row.DataItem).ProcessID.ToString(); }
        //        CheckBox cb = (CheckBox)e.Row.FindControl("chkThisProcess");
        //        if (cb != null)
        //        {
        //            cb.Checked = false;
        //        }
        //    }
        //}
        //void btnSaveAllowedProcesses_Click(object sender, EventArgs e)
        //{
        //    decimal ClientProcessDependenciesID = -1;
        //    decimal ClientID = -1;
        //    decimal ProcessID = -1;
        //    string sID = "";
        //    HiddenField hf;

        //    CheckBox cb;

        //    foreach (GridViewRow row in grdProcessList.Rows)
        //    {
        //        cb = (CheckBox)row.FindControl("chkThisProcess");
        //        hf = (HiddenField)row.FindControl("hdnClientProcessDependenciesID");
        //        if (hf != null) { sID = hf.Value; }
        //        if (decimal.TryParse(sID, out ClientProcessDependenciesID) == false) { ClientProcessDependenciesID = -1; }
        //        hf = (HiddenField)row.FindControl("hdnProcessID");
        //        if (hf != null) { sID = hf.Value; }
        //        if (decimal.TryParse(sID, out ProcessID) == false) { ProcessID = -1; }
        //        hf = (HiddenField)row.FindControl("hdnClientID");
        //        if (hf != null) { sID = hf.Value; }
        //        if (decimal.TryParse(sID, out ClientID) == false) { ClientID = -1; }


        //        if (cb.Checked == true || ClientProcessDependenciesID > 0)
        //        {
        //            ClientManager cm = new ClientManager(User.Identity.Name);
        //            if (cb.Checked == false)
        //            {
        //                cm.DeleteClientProcessDependencies(ClientProcessDependenciesID);
        //                // delete the ClientBillingPoint Record
        //            }
        //            else
        //            {
        //                cm.AddUpdateDeleteClientProcessDependencies(ClientProcessDependenciesID, ClientID, ProcessID);
        //                // Add a new ClientBillingPoint Record.
        //            }
        //        }
        //    }
        //}

        //protected void UpdateProjectGrid()
        //{
        //    ProjectManager pm = new ProjectManager(User.Identity.Name);
        //    grdProjectList.DataSource = pm.GetProjectList();
        //    grdProjectList.DataBind();
        //    ////   UpdateClientQuestionRestrictionGrid();
        //}
        //protected void UpdateProjectList(decimal ClientID)
        //{
        //    if (ClientID < 1) { lblDefault.Text = "Default Loaded"; }
        //    ClientManager cm = new ClientManager(User.Identity.Name);
        //    List<PairIDValue> RQ = cm.ClientProjectAllowedList(ClientID);
        //    PairIDValue pd = new PairIDValue();
        //    foreach (GridViewRow r in grdProjectList.Rows)
        //    {
        //        decimal ProjectID = -1;
        //        HiddenField hf = (HiddenField)r.FindControl("hdnProjectID");
        //        if (decimal.TryParse(hf.Value, out ProjectID) == false) { ProjectID = -1; }
        //        string sProjectID = hf.Value;
        //        hf = (HiddenField)r.FindControl("hdnClientID");
        //        if (hf != null) { hf.Value = ClientID.ToString(); }
        //        hf = (HiddenField)r.FindControl("hdnClientProjectDependenciesID");
        //        if (hf != null) { hf.Value = "-1"; }
        //        CheckBox CB = (CheckBox)r.FindControl("chkThisProject");
        //        if (CB != null) { CB.Checked = false; }
        //        pd = RQ.FirstOrDefault(x => x.ID == ProjectID);
        //        if (pd != null)
        //        {
        //            if (CB != null) { CB.Checked = true; hf.Value = pd.Desc; }
        //        }
        //    }
        //}
        //void btnSaveAllowedProject_Click(object sender, EventArgs e)
        //{
        //    decimal ClientProjectDependenciesID = -1;
        //    decimal ClientID = -1;
        //    decimal ProjectID = -1;
        //    string sID = "";
        //    HiddenField hf;

        //    CheckBox cb;

        //    foreach (GridViewRow row in grdProjectList.Rows)
        //    {
        //        cb = (CheckBox)row.FindControl("chkThisProject");
        //        hf = (HiddenField)row.FindControl("hdnClientProjectDependenciesID");
        //        if (hf != null) { sID = hf.Value; }
        //        if (decimal.TryParse(sID, out ClientProjectDependenciesID) == false) { ClientProjectDependenciesID = -1; }
        //        hf = (HiddenField)row.FindControl("hdnProjectID");
        //        if (hf != null) { sID = hf.Value; }
        //        if (decimal.TryParse(sID, out ProjectID) == false) { ProjectID = -1; }
        //        hf = (HiddenField)row.FindControl("hdnClientID");
        //        if (hf != null) { sID = hf.Value; }
        //        if (decimal.TryParse(sID, out ClientID) == false) { ClientID = -1; }


        //        if (cb.Checked == true || ClientProjectDependenciesID > 0)
        //        {
        //            ClientManager cm = new ClientManager(User.Identity.Name);
        //            if (cb.Checked == false)
        //            {
        //                cm.DeleteClientProjectDependencies(ClientProjectDependenciesID);
        //                // delete the ClientBillingPoint Record
        //            }
        //            else
        //            {
        //                cm.AddUpdateDeleteClientProjectDependencies(ClientProjectDependenciesID, ClientID, ProjectID);
        //                // Add a new ClientBillingPoint Record.
        //            }
        //        }
        //    }
        //    //UpdateBillingPoints(ClientID);

        //}
        //void btnLoadBillingPointsDefault_Click(object sender, EventArgs e)
        //{
        //    UpdateBillingPoints(-1);
        //}
        //void btnSaveBillingPoints_Click(object sender, EventArgs e)
        //{
        //    decimal ClientBillingPointID = -1;
        //    decimal ProjectID = -1;
        //    decimal ProcessID = -1;
        //    decimal ClientID = -1;


        //    string sID = "";
        //    decimal RateValue = 0;
        //    CheckBox cb;
        //    HiddenField hf;

        //    foreach (GridViewRow row in grdBillingPoints.Rows)
        //    {
        //        cb = (CheckBox)row.FindControl("chkisBillingPoint");
        //        hf = (HiddenField)row.FindControl("hdnClientBillingPointID");
        //        if (hf != null) { sID = hf.Value; }
        //        if (decimal.TryParse(sID, out ClientBillingPointID) == false) { ClientBillingPointID = -1; }
        //        hf = (HiddenField)row.FindControl("hdnClientID");
        //        if (hf != null) { sID = hf.Value; }
        //        if (decimal.TryParse(sID, out ClientID) == false) { ClientID = -1; }

        //        if (cb.Checked == true || ClientBillingPointID > 0)
        //        {
        //            ClientManager cm = new ClientManager(User.Identity.Name);
        //            if (cb.Checked == false)
        //            {
        //                cm.DeleteBillingPoint(ClientBillingPointID);
        //                // delete the ClientBillingPoint Record
        //            }
        //            else
        //            {
        //                hf = (HiddenField)row.FindControl("hdnProjectID");
        //                if (hf != null) { sID = hf.Value; }
        //                if (decimal.TryParse(sID, out ProjectID) == false) { ProjectID = -1; }

        //                hf = (HiddenField)row.FindControl("hdnProcessID");
        //                if (hf != null) { sID = hf.Value; }
        //                if (decimal.TryParse(sID, out ProcessID) == false) { ProcessID = -1; }

        //                TextBox tb = (TextBox)row.FindControl("txtRateValue");

        //                if (decimal.TryParse(tb.Text, out RateValue) == false) { RateValue = 0; }

        //                cm.AddUpdateClientBillingPoint(ClientBillingPointID, ClientID, ProjectID, ProcessID, RateValue);
        //                // Add a new ClientBillingPoint Record.
        //            }
        //        }
        //    }
        //    UpdateBillingPoints(ClientID);
        //}

        //void grdProjectList_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        Type type = e.Row.DataItem.GetType();               //.GetGenericArguments()[0];
        //        HiddenField hf = (HiddenField)e.Row.FindControl("hdnClientProjectDependenciesID");
        //        if (hf != null) { hf.Value = "-1"; }
        //        hf = (HiddenField)e.Row.FindControl("hdnProjectID");
        //        if (hf != null) { hf.Value = ((Project)e.Row.DataItem).ProjectID.ToString(); }
        //        CheckBox cb = (CheckBox)e.Row.FindControl("chkThisProject");
        //        if (cb != null)
        //        {
        //            cb.Checked = false;
        //        }
        //    }
        //}
        //void grdBillingPoints_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        Type type = e.Row.DataItem.GetType();               //.GetGenericArguments()[0];
        //        HiddenField hf = (HiddenField)e.Row.FindControl("hdnClientBillingPointID");
        //        if (hf != null) { hf.Value = ((GetMasterBillingPointsResult)e.Row.DataItem).ClientBillingPointID.ToString(); }
        //        hf = (HiddenField)e.Row.FindControl("hdnProjectID");
        //        if (hf != null) { hf.Value = ((GetMasterBillingPointsResult)e.Row.DataItem).ProjectID.ToString(); }
        //        hf = (HiddenField)e.Row.FindControl("hdnProcessID");
        //        if (hf != null) { hf.Value = ((GetMasterBillingPointsResult)e.Row.DataItem).ProcessID.ToString(); }
        //        hf = (HiddenField)e.Row.FindControl("hdnClientID");
        //        if (hf != null) { hf.Value = ((GetMasterBillingPointsResult)e.Row.DataItem).ClientID.ToString(); }

        //        CheckBox cb = (CheckBox)e.Row.FindControl("chkisBillingPoint");
        //        TextBox tb = (TextBox)e.Row.FindControl("txtRateValue");
        //        if (cb != null)
        //        {
        //            cb.Checked = false;
        //            if (((GetMasterBillingPointsResult)e.Row.DataItem).BillingPoint == true)
        //            {
        //                cb.Checked = true;
        //            }
        //        }
        //        tb.Text = "";
        //        if (tb != null)
        //        {
        //            tb.Text = ((GetMasterBillingPointsResult)e.Row.DataItem).RateValue.ToString();
        //        }
        //    }
        //}
        //protected void UpdateBillingPoints(decimal ClientID)
        //{
        //    lblDefault.Text = "";
        //    if (ClientID < 1) { lblDefault.Text = "Default Loaded"; }
        //    ClientManager cm = new ClientManager(User.Identity.Name);
        //    grdBillingPoints.DataSource = cm.GetClientBillingPoints(ClientID);
        //    grdBillingPoints.DataBind();
        //}
        #endregion






        void btnMoveClientLocation_Click(object sender, EventArgs e)
        {
            string xClientID = hdnMoveClientID.Value;
            decimal ClientID = -1;
            if (decimal.TryParse(xClientID, out ClientID) == false) { ClientID = -1; }
            decimal ChildLocationID = decimal.Parse(ChildGrid.SelectedValue.ToString());
            if (ClientID > 0 && ChildLocationID > 0)
            {
                ClientManager cm = new ClientManager(User.Identity.Name);
                cm.MoveClientLocation(ChildLocationID, ClientID);
                decimal KeyID = decimal.Parse(MainGrid.SelectedValue.ToString());
                UpdateChildGrid(KeyID);
            }
        }
        void MainGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Type type = e.Row.DataItem.GetType();               //.GetGenericArguments()[0];
                System.Web.UI.WebControls.LinkButton bPrint = (System.Web.UI.WebControls.LinkButton)e.Row.FindControl("imgPrint");
                if (bPrint != null)
                {
                    bPrint.Attributes.Add("onclick", "PrintScanCodes('Client'," + ((Client)e.Row.DataItem).ClientID + "); return false;");
                }
            }
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    User selUser = e.Row.DataItem as User;
            //    e.Row.Attributes.Add("onclick", "userSelected('" + selUser.Id + "')");
            //}

        }
        protected void UpdateMainGrid()
        {
            ClientManager cm = new ClientManager(User.Identity.Name);
            MainGrid.DataSource = cm.GetClientsDefered();
            MainGrid.DataBind();
        }
        protected void MainGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
            //hdnSelectedClientID.Value = "";
            if (MainGrid.SelectedIndex >= 0)
            {
                hdnSelectedClientID.Value = MainGrid.SelectedValue.ToString();
                decimal KeyID = decimal.Parse(MainGrid.SelectedValue.ToString());
                //btnEdit.Visible = true;
                //btnDelete.Visible = true;

                if (ctx == null) { ctx = CM.GetDataContext(User.Identity.Name); }
                if (CM.UserRestrict.AllowDelete("Clientx", KeyID, ctx) == true) { btnDelete.Visible = true; }
                //if (CM.UserRestrict.AllowAdd("Clientx", KeyID, ctx) == true) { btnAddProcess.Visible = true; }
                if (CM.UserRestrict.AllowUpdate("Clientx", KeyID, ctx) == true) { btnEdit.Visible = true; }
                ClientManager cm = new ClientManager(User.Identity.Name);
                var d = cm.GetClient(KeyID);
                lblSelect.Text = "";
                TabClientLocation.HeaderText = "Locations";
                if (d != null)
                {
                    lblSelect.Text = d.CompanyName;
                    //TabClient.HeaderText = d.CompanyName;
                    TabClientLocation.HeaderText = d.CompanyName;
                    //lblQuestionText.Text = " (" + qu.Name + ") " + qu.Description;
                }              
                pnlChild.Visible = true;
                tabChild.Visible = true;
                TabClientLocation.Visible = true;
                UpdateChildGrid(KeyID);

                //UpdateBillingPoints(KeyID);
                //UpdateProjectList(KeyID);
                //UpdateProcessList(KeyID);


            }
            else
            {
                lblSelect.Text = "";
                btnEdit.Visible = false;
                btnDelete.Visible = false;
                pnlChild.Visible = false;
                tabChild.Visible = false;
                TabClientLocation.Visible = false;
            }
        }
        protected void ChildGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ChildGrid.SelectedIndex >= 0)
            {
                decimal KeyID = decimal.Parse(ChildGrid.SelectedValue.ToString());
                btnEditLocation.Visible = btnEdit.Visible;
                btnDeleteLocation.Visible = btnDelete.Visible;
                btnAddLocation.Visible = btnAdd.Visible;
                btnMoveLocation.Visible = btnDelete.Visible;
                lblSelectLocation.Text = "";
                ClientLocationManager cm = new ClientLocationManager(User.Identity.Name);
                var d = cm.Description(KeyID);
                if (d != null)
                {
                    lblSelectLocation.Text = d;
                }  
            }
            else
            {
                lblSelectLocation.Text = "";
                btnAddLocation.Visible = btnAdd.Visible;
                btnEditLocation.Visible = false;
                btnDeleteLocation.Visible = false;
                btnMoveLocation.Visible = false;
            }
        }
        protected void UpdateChildGrid(decimal KeyID)
        {
            ClientManager cm = new ClientManager(User.Identity.Name);
            ChildGrid.DataSource = cm.GetClientLocationssDefered(KeyID);
            ChildGrid.DataBind();
            ChildGrid.SelectedIndex = -1;
            btnEditLocation.Visible = false;
            btnDeleteLocation.Visible = false;
            btnMoveLocation.Visible = false;
            #region QuestionAnswerRestrictions
            //UpdateClientQuestionRestrictionGrid();
            //UpdateClientAnswerRestrictionGrid();
            #endregion

        }

        #region ClientArea
        protected void ReadyAdd()
        {
            //AddName.Text = "";
            //AddSkanKey.Text = "";
            AddCompanyName.Text = "";
            AddRMASuffix.Text = "";
            AddContactName.Text = "";
            AddBillingAddress.Text = "";
            AddUserName.Text = "";
            AddEmailAddress.Text = "";
            AddAddressLine1.Text = "";
            AddAddressLine2.Text = "";
            AddAddressLine3.Text = "";
            AddAddressLine4.Text = "";
            AddCity.Text = "";
            AddCountry.Text = "";
            AddStateOrProvince.Text = "";
            AddPostalCode.Text = "";
            AddPhoneNumber.Text = "";
            AddNotes.Text = "";
            AddWarrentyDayLimit.Text = "0";
            drpAddStatus.SelectedIndex = 0;
            AddIsVendorGroup.Checked = false;
            AddProductTag.Text = "";
            AddRepairForm.Text = "";
            drpAddPortalName.SelectedIndex = 0;
            AddInWarrentyPricing.Checked = false;
            //AddchkOOW_WHS.Checked = false;
            AddHideClientList.Checked = false;

        }
        protected void ReadyEdit()
        {
            decimal KeyID = decimal.Parse(MainGrid.SelectedValue.ToString());
            ClientManager cm = new ClientManager(User.Identity.Name);
            Client cl = cm.GetClient(KeyID);
            if (cl != null)
            {
                //EditName.Text = cl.Name;
                //EditSkanKey.Text = cl.ScanKey;

                EditWarrentyDayLimit.Text = "0";
                if (cl.WarrentyDayLimit != null) { EditWarrentyDayLimit.Text = cl.WarrentyDayLimit.ToString(); }

                EditRMASuffix.Text = cl.RMASuffix;
                EditCompanyName.Text = cl.CompanyName;
                EditContactName.Text = cl.ContactName;
                EditBillingAddress.Text = cl.BillingAddress;
                EditUserName.Text = cl.Username;
                EditEmailAddress.Text = cl.EmailAddress;

                EditAddressLine1.Text = cl.AddressLine1;
                EditAddressLine2.Text = cl.AddressLine2;
                EditAddressLine3.Text = cl.AddressLine3;
                EditAddressLine4.Text = cl.AddressLine4;
                EditCity.Text = cl.City;
                EditCountry.Text = cl.Country;
                EditStateOrProvince.Text = cl.StateOrProvince;
                EditPostalCode.Text = cl.PostalCode;
                EditPhoneNumber.Text = cl.PhoneNumber;
                EditFaxNumber.Text = cl.FaxNumber;
                EditNotes.Text = cl.Notes;
                EditIsVendorGroup.Checked = false;
                if (cl.isVendorGroup != null && cl.isVendorGroup == true) { EditIsVendorGroup.Checked = true; }
                EditInWarrentyPricing.Checked = false;
                if (cl.InWarrentyPricing != null && cl.InWarrentyPricing == true) { EditInWarrentyPricing.Checked = true; }

                //EditchkOOW_WHS.Checked = false;
                //if (cl.OOW_WHS != null && cl.OOW_WHS == true) { EditchkOOW_WHS.Checked = true; }

                EditHideClientList.Checked = false;
                if (cl.HideOnPreReceiveList != null && cl.HideOnPreReceiveList == true) { EditHideClientList.Checked = true; }
                EditProductTag.Text = cl.ProductTag;
                EditRepairForm.Text = cl.RepairForm;


                ListItem _ListItem = drpEditPortalName.Items.FindByText(cl.PortalName);
                if (_ListItem == null) { drpEditPortalName.SelectedIndex = 0; }
                else { drpEditPortalName.SelectedIndex = drpEditPortalName.Items.IndexOf(_ListItem); }

                _ListItem = drpEditStatus.Items.FindByValue(cl.StatusID.ToString());
                if (_ListItem == null) { drpEditStatus.SelectedIndex = 0; }
                else { drpEditStatus.SelectedIndex = drpEditStatus.Items.IndexOf(_ListItem); }
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ////AddStatus.Text = "";
            ReadyAdd();
            pnlMainView.Visible = false;
            pnlAdd.Visible = true;
        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            //return;
            ReadyEdit();
            pnlMainView.Visible = false;
            pnlEdit.Visible = true;
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            // Delete the answers.
            if (MainGrid.SelectedIndex >= 0)
            {
                decimal KeyID = decimal.Parse(MainGrid.SelectedValue.ToString());
                ClientManager cm = new ClientManager(User.Identity.Name);
                //cm.DeleteClient(KeyID);
                UpdateMainGrid();
                KeyID = decimal.Parse(MainGrid.SelectedValue.ToString());
                UpdateChildGrid(KeyID);
            }
        }

        protected void AddOK_Click(object sender, EventArgs e)
        {
            decimal warrentydaylimit = 0;
            if (decimal.TryParse(AddWarrentyDayLimit.Text, out warrentydaylimit) == false) { warrentydaylimit = 0; };
            ClientManager cm = new ClientManager(User.Identity.Name);
            Client cl = cm.NewClient();
            //cl.Name = AddName.Text;
            //cl.ScanKey = AddSkanKey.Text;
            cl.CompanyName = AddCompanyName.Text;
            cl.RMASuffix = AddRMASuffix.Text;
            cl.ContactName = AddContactName.Text;
            cl.BillingAddress = AddBillingAddress.Text;
            cl.WarrentyDayLimit = warrentydaylimit;
            cl.Username = AddUserName.Text;
            cl.EmailAddress = AddEmailAddress.Text;
            cl.AddressLine1 = AddAddressLine1.Text;
            cl.AddressLine2 = AddAddressLine2.Text;
            cl.AddressLine3 = AddAddressLine3.Text;
            cl.AddressLine4 = AddAddressLine4.Text;
            cl.City = AddCity.Text;
            cl.Country = AddCountry.Text;
            cl.StateOrProvince = AddStateOrProvince.Text;
            cl.PostalCode = AddPostalCode.Text;
            cl.PhoneNumber = AddPhoneNumber.Text;
            cl.Notes = AddNotes.Text;
            cl.StatusID = decimal.Parse(drpAddStatus.SelectedItem.Value);
            cl.ProductTag = AddProductTag.Text;
            cl.RepairForm = AddRepairForm.Text;
            cl.PortalName = drpAddPortalName.SelectedItem.Text;
            cl.isVendorGroup = AddIsVendorGroup.Checked;
            cl.InWarrentyPricing = AddInWarrentyPricing.Checked;
            cl.OOW_WHS = false;                      // AddchkOOW_WHS.Checked;
            cl.HideOnPreReceiveList = AddHideClientList.Checked;
            cm.InsertClient(cl);

            UpdateMainGrid();
            pnlMainView.Visible = true;
            pnlAdd.Visible = false;
        }
        protected void AddCancel_Click1(object sender, EventArgs e)
        {
            pnlMainView.Visible = true;
            pnlAdd.Visible = false;
        }
        protected void EditOK_Click(object sender, EventArgs e)
        {
            decimal KeyID = decimal.Parse(MainGrid.SelectedValue.ToString());
            decimal warrentydaylimit = 0;
            if (decimal.TryParse(EditWarrentyDayLimit.Text, out warrentydaylimit) == false) { warrentydaylimit = 0; };
            ClientManager cm = new ClientManager(User.Identity.Name);
            Client cl = cm.NewClient();
            cl.ClientID = KeyID;
            //cl.Name = EditName.Text;
            //cl.ScanKey = EditSkanKey.Text;
            cl.CompanyName = EditCompanyName.Text;
            cl.RMASuffix = EditRMASuffix.Text;
            cl.WarrentyDayLimit = warrentydaylimit;
            cl.ContactName = EditContactName.Text;
            cl.BillingAddress = EditBillingAddress.Text;
            cl.EmailAddress = EditEmailAddress.Text;
            cl.AddressLine1 = EditAddressLine1.Text;
            cl.AddressLine2 = EditAddressLine2.Text;
            cl.AddressLine3 = EditAddressLine3.Text;
            cl.AddressLine4 = EditAddressLine4.Text;
            cl.City = EditCity.Text;
            cl.Country = EditCountry.Text;
            cl.StateOrProvince = EditStateOrProvince.Text;
            cl.PostalCode = EditPostalCode.Text;
            cl.PhoneNumber = EditPhoneNumber.Text;
            cl.FaxNumber = EditFaxNumber.Text;
            cl.Notes = EditNotes.Text;
            cl.StatusID = decimal.Parse(drpAddStatus.SelectedItem.Value);
            cl.ProductTag = EditProductTag.Text;
            cl.RepairForm = EditRepairForm.Text;
            cl.PortalName = drpEditPortalName.SelectedItem.Text;

            cl.Username = EditUserName.Text;
            cl.isVendorGroup = EditIsVendorGroup.Checked;
            cl.InWarrentyPricing = EditInWarrentyPricing.Checked;
            cl.OOW_WHS = false;                         // EditchkOOW_WHS.Checked;
            cl.HideOnPreReceiveList = EditHideClientList.Checked;
            cm.UpdateClient(cl);

            UpdateMainGrid();
            pnlMainView.Visible = true;
            pnlEdit.Visible = false;


        }
        protected void EditCancel_Click(object sender, EventArgs e)
        {
            pnlMainView.Visible = true;
            pnlEdit.Visible = false;
        }
        #endregion

        #region ClientLocationDetail
        protected void ReadyAddLocation()
        {
            AddLScanKeyLBL.Text = "";
            AddLSkanKey.Text = "";
            AddLCompanyName.Text = "";
            AddLStoreSuffix.Text = "";
            AddLStoreNumber.Text = "";
            AddLContactName.Text = "";
            AddLBillingAddress.Text = "";
            AddLAddressLine1.Text = "";
            AddLAddressLine2.Text = "";
            AddLAddressLine3.Text = "";
            AddLAddressLine4.Text = "";
            AddLCountry.Text = "";
            AddLCity.Text = "";
            AddLStateOrProvince.Text = "";
            AddLPostalCode.Text = "";
            AddLPhoneNumber.Text = "";
            AddlFaxNumber.Text = "";
            AddLNotes.Text = "";
            AddLUsername.Text = "";
            AddLEmailAddress.Text = "";
            drpAddLStatus.SelectedIndex = 0;
            chkAddInventory.Checked = false;
            //AddIFSProject.Text = "";
            //AddIFSVendor.Text = "";
            //AddLocationSegment.Text = "";
            //AddLocationRequirePOToReceive.Checked = false;
            //AddIFSSite.Text = "";
        }
        protected void ReadyEditLocation()
        {
            decimal KeyID = decimal.Parse(ChildGrid.SelectedValue.ToString());
            ClientManager cm = new ClientManager(User.Identity.Name);
            ClientLocation cl = cm.GetClientLocation(KeyID);
            if (cl != null)
            {
                hdnClientLocationID.Value = KeyID.ToString();
                EditLApprovalPassword.Text = "";
                //EditName.Text = cl.Name;
                EditLScanKeyLBL.Text = "";
                EditLSkanKey.Text = cl.ScanKey;
                EditLCompanyName.Text = cl.CompanyName;
                EditLStoreSuffix.Text = cl.StoreSuffix;
                EditLStoreNumber.Text = cl.StoreNumber;
                EditLContactName.Text = cl.ContactName;
                EditLBillingAddress.Text = cl.BillingAddress;
                EditLUserName.Text = cl.Username;
                EditLEmailAddress.Text = cl.EmailAddress;
                EditLEmailAddress2.Text = cl.EmailAddress2;
                // EditLApprovalPassword.Text = cl.ApprovalPassword;
                EditLAddressLine1.Text = cl.AddressLine1;
                EditLAddressLine2.Text = cl.AddressLine2;
                EditLAddressLine3.Text = cl.AddressLine3;
                EditLAddressLine4.Text = cl.AddressLine4;
                EditLCountry.Text = cl.Country;
                EditLCity.Text = cl.City;
                EditLStateOrProvince.Text = cl.StateOrProvince;
                EditLPostalCode.Text = cl.PostalCode;
                EditLPhoneNumber.Text = cl.PhoneNumber;
                EditLFaxNumber.Text = cl.FaxNumber;
                EditLNotes.Text = cl.Notes;



                //EditIFSProject.Text = cl.IFSProject;
                //EditIFSVendor.Text = cl.IFSPOVendor;
                //EditLocationSegment.Text = cl.LocationSegment;
                //EditLocationRequirePOToReceive.Checked = false;
                //if (cl.LocationRequirePOToReceive != null && cl.LocationRequirePOToReceive == true)
                //{
                //    EditLocationRequirePOToReceive.Checked = true;
                //}

                //EditIFSSite.Text = cl.IFSSite;
                chkEditInventory.Checked = true;
                if (cl.OnSiteInventory == null || cl.OnSiteInventory == false) { chkEditInventory.Checked = false; }
                ListItem _ListItem = drpEditLStatus.Items.FindByValue(cl.StatusID.ToString());
                if (_ListItem == null) { drpEditLStatus.SelectedIndex = 0; }
                else { drpEditLStatus.SelectedIndex = drpEditStatus.Items.IndexOf(_ListItem); }
            }
        }

        protected void btnAddLocation_Click(object sender, EventArgs e)
        {
            ReadyAddLocation();
            pnlMainView.Visible = false;
            pnlAddLocation.Visible = true;
            lblAddLocationMessage.Text = "";
        }
        protected void btnEditLocation_Click(object sender, EventArgs e)
        {
            ReadyEditLocation();
            pnlMainView.Visible = false;
            pnlEditLocation.Visible = true;
            lblEditLocationMessage.Text = "";
        }
        protected void btnDeleteLocation_Click(object sender, EventArgs e)
        {
            // Delete the answers.
            if (ChildGrid.SelectedIndex >= 0)
            {
                try
                {
                    //  Delete Location Not Implemented
                    ////decimal MainGridKeyID = decimal.Parse(MainGrid.SelectedValue.ToString());
                    ////decimal KeyID = decimal.Parse(ChildGrid.SelectedValue.ToString());
                    ////AnswerManager am = new AnswerManager(User.Identity.Name);
                    ////am.DeleteAnswer(KeyID);
                    UpdateChildGrid(decimal.Parse(MainGrid.SelectedValue.ToString()));
                }
                catch (UserAccessControlException ex)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Client Location Delete", "alert('" + ex.Message + "');", true);
                }
            }
        }

        protected void btnMoveLocation_Click(object sender, EventArgs e)
        {
            // Delete the answers.
            if (ChildGrid.SelectedIndex >= 0)
            {
                try
                {
                    // Delete Location Not Implemented
                    ////decimal MainGridKeyID = decimal.Parse(MainGrid.SelectedValue.ToString());
                    ////decimal KeyID = decimal.Parse(ChildGrid.SelectedValue.ToString());
                    ////AnswerManager am = new AnswerManager(User.Identity.Name);
                    ////am.DeleteAnswer(KeyID);
                    UpdateChildGrid(decimal.Parse(MainGrid.SelectedValue.ToString()));
                }
                catch (UserAccessControlException ex)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Client Location Moved", "alert('" + ex.Message + "');", true);
                }
            }
        }

        protected void AddLocationOK_Click(object sender, EventArgs e)
        {
            try
            {
                lblAddLocationMessage.Text = "";
                //if (AddIFSSite.Text.Length == 0 || AddIFSProject.Text.Length == 0)
                //{
                //    lblAddLocationMessage.Text = "IFS Site and Project are required!";
                //    return;
                //}
                //if (AddIFSSite.Text.ToUpper() == "C1NA" && AddIFSVendor.Text.Length == 0)
                //{
                //    lblAddLocationMessage.Text = "C1NA Sites require an IFSVendor.";
                //    return;
                //}
                //if (AddIFSSite.Text.ToUpper() == "C1CON" && AddIFSProject.Text.ToUpper() == "BRCE")
                //{
                //    lblAddLocationMessage.Text = "Project BRCE can not be paired with Site C1CON.";
                //    return;
                //}
                lblAddLocationMessage.Text = "";

                decimal KeyID = decimal.Parse(MainGrid.SelectedValue.ToString());
                ClientManager cm = new ClientManager(User.Identity.Name);
                ClientLocation cl = cm.NewClientLocation();
                //cl.Name = AddName.Text;
                cl.ClientID = KeyID;
                cl.StoreNumber = AddLStoreNumber.Text;
                cl.StoreSuffix = AddLStoreSuffix.Text;
                cl.ScanKey = AddLSkanKey.Text;
                cl.CompanyName = AddLCompanyName.Text;
                cl.ContactName = AddLContactName.Text;
                cl.BillingAddress = AddLBillingAddress.Text;
                cl.Username = AddLUsername.Text;
                cl.EmailAddress = AddLEmailAddress.Text;
                cl.EmailAddress2 = AddLEmailAddress2.Text;
                cl.ApprovalPassword = AddLApprovalPassword.Text;
                cl.AddressLine1 = AddLAddressLine1.Text;
                cl.AddressLine2 = AddLAddressLine2.Text;
                cl.AddressLine3 = AddLAddressLine3.Text;
                cl.AddressLine4 = AddLAddressLine4.Text;
                cl.City = AddLCity.Text;
                cl.Country = AddLCountry.Text;
                cl.StateOrProvince = AddLStateOrProvince.Text;
                cl.PostalCode = AddLPostalCode.Text;
                cl.PhoneNumber = AddLPhoneNumber.Text;
                cl.FaxNumber = AddlFaxNumber.Text;
                cl.Notes = AddLNotes.Text;

                cl.IFSSite = "C1CON";       // AddIFSSite.Text.ToUpper();
                cl.IFSProject = "C1CON";          // AddIFSProject.Text.ToUpper();
                cl.IFSPOVendor = "";          // AddIFSVendor.Text.ToUpper();
                cl.LocationSegment = "";               // AddLocationSegment.Text.ToUpper();
                cl.LocationRequirePOToReceive = false;          // AddLocationRequirePOToReceive.Checked;
                cl.StatusID = decimal.Parse(drpAddLStatus.SelectedItem.Value);
                cl.OnSiteInventory = chkAddInventory.Checked;
                cm.InsertClientLocation(cl);
                UpdateChildGrid(KeyID);
                pnlMainView.Visible = true;
                pnlAddLocation.Visible = false;
            }
            catch (UserAccessControlException ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Location Add", "alert('" + ex.Message + "');", true);
            }
        }
        protected void AddLocationCancel_Click(object sender, EventArgs e)
        {
            pnlMainView.Visible = true;
            pnlAddLocation.Visible = false;
        }
        protected void EditLocationOK_Click(object sender, EventArgs e)
        {
            try
            {
                lblAddLocationMessage.Text = "";
                //if (EditIFSSite.Text.Length == 0 || EditIFSProject.Text.Length == 0)
                //{
                //    lblEditLocationMessage.Text = "IFS Site and Project are required!";
                //    return;
                //}
                //if (EditIFSSite.Text.ToUpper() == "C1NA" && EditIFSVendor.Text.Length == 0)
                //{
                //    lblEditLocationMessage.Text = "C1NA Sites require an IFSVendor.";
                //    return;
                //}
                //if (EditIFSSite.Text.ToUpper() == "C1CON" && EditIFSProject.Text.ToUpper() == "BRCE")
                //{
                //    lblEditLocationMessage.Text = "Project BRCE can not be paired with Site C1CON.";
                //    return;
                //}
                lblEditLocationMessage.Text = "";
                decimal KeyID = decimal.Parse(MainGrid.SelectedValue.ToString());
                decimal ChildKeyID = decimal.Parse(ChildGrid.SelectedValue.ToString());
                ClientManager cm = new ClientManager(User.Identity.Name);
                ClientLocation cl = cm.NewClientLocation();
                //cl.Name = AddName.Text;
                cl.ClientID = KeyID;
                cl.ClientLocationID = ChildKeyID;
                cl.StoreNumber = EditLStoreNumber.Text;
                cl.StoreSuffix = EditLStoreSuffix.Text;
                cl.ScanKey = EditLSkanKey.Text;
                cl.CompanyName = EditLCompanyName.Text;
                cl.ContactName = EditLContactName.Text;
                cl.BillingAddress = EditLBillingAddress.Text;
                cl.Username = EditLUserName.Text;
                cl.EmailAddress = EditLEmailAddress.Text;
                cl.EmailAddress2 = EditLEmailAddress2.Text;
                if (EditLApprovalPassword.Text.Length > 0)
                {
                    cl.ApprovalPassword = EditLApprovalPassword.Text;
                }
                cl.AddressLine1 = EditLAddressLine1.Text;
                cl.AddressLine2 = EditLAddressLine2.Text;
                cl.AddressLine3 = EditLAddressLine3.Text;
                cl.AddressLine4 = EditLAddressLine4.Text;
                cl.City = EditLCity.Text;
                cl.StateOrProvince = EditLStateOrProvince.Text;

                cl.Country = EditLCountry.Text;
                cl.PostalCode = EditLPostalCode.Text;
                cl.PhoneNumber = EditLPhoneNumber.Text;
                cl.FaxNumber = EditLFaxNumber.Text;
                cl.Notes = EditLNotes.Text;


                cl.IFSSite = "C1CON";       // EditIFSSite.Text.ToUpper();
                cl.IFSProject = "C1CON";          // EditIFSProject.Text.ToUpper();
                cl.IFSPOVendor = "";          // EditIFSVendor.Text.ToUpper();
                cl.LocationSegment = "";               // EditLocationSegment.Text.ToUpper();
                cl.LocationRequirePOToReceive = false;          // EditLocationRequirePOToReceive.Checked;

                cl.StatusID = decimal.Parse(drpEditLStatus.SelectedItem.Value);
                cl.OnSiteInventory = chkEditInventory.Checked;
                cm.UpdateClientLocation(cl);
                UpdateChildGrid(KeyID);
                pnlMainView.Visible = true;
                pnlEditLocation.Visible = false;
            }
            catch (UserAccessControlException ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Client Location Update", "alert('" + ex.Message + "');", true);
            }
        }
        protected void EditLocationCancel_Click(object sender, EventArgs e)
        {
            pnlMainView.Visible = true;
            pnlEditLocation.Visible = false;

        }

        #region Upload
        void btnDownload_Click(object sender, EventArgs e)
        {
            if (MainGrid.SelectedValue == null) { return; }
            decimal KeyID = decimal.Parse(MainGrid.SelectedValue.ToString());
            ExportClientLocationToExcel(KeyID);
        }


        protected void btnUpload_Click(object sender, EventArgs e)
        {
            UploadFile("~/IDAutomation", FileUploadXLS, lblMsgDetail);
        }
        private void UploadFile(string PathName, FileUpload UploadTool, Label Message)
        {
            decimal ClientID = -1;
            if (MainGrid.SelectedValue == null || decimal.TryParse(MainGrid.SelectedValue.ToString(), out ClientID) == false) { ClientID = -1; };
            if (ClientID < 1 && AutoGenerateClient.Checked == false)
            {
                Message.Text = "Please select a Client first";
                Message.ForeColor = System.Drawing.Color.Red;
                Message.Visible = true;
                return;
            }

            if (UploadTool.HasFile)
            {
                string strFileName = UploadTool.FileName + "_" + DateTime.Now.ToString("ddMMyyyy_HHmmss");
                string strFileType = System.IO.Path.GetExtension(UploadTool.FileName).ToString().ToLower();
                //Check file type
                if (strFileType == ".xls" || strFileType == ".xlsx")
                {
                    //decimal ClientID = decimal.Parse(MainGrid.SelectedValue.ToString());
                    UploadTool.SaveAs(Server.MapPath(PathName + "/" + strFileName + strFileType));
                    ImportClientLocation(Server.MapPath(PathName + "/" + strFileName + strFileType), ClientID, AutoGenerateClient.Checked);

                    Message.Text = "Data File Uploaded!";
                    Message.ForeColor = System.Drawing.Color.Green;
                    Message.Visible = true;
                    UpdateChildGrid(ClientID);
                    //UpdateMainGrid();
                }
                else
                {
                    Message.Text = "Only excel files allowed";
                    Message.ForeColor = System.Drawing.Color.Red;
                    Message.Visible = true;
                }
            }
            else
            {
                Message.Text = "Please select an excel file first";
                Message.ForeColor = System.Drawing.Color.Red;
                Message.Visible = true;
            }
        }


        private void ImportClientLocation(string FileName, decimal ClientID, bool GenerateClient)
        {
            //MasterCarrierManufacturerModelColourManager mlm = new MasterCarrierManufacturerModelColourManager(User.Identity.Name);
            ClientLocationManager clm = new ClientLocationManager(User.Identity.Name);

            ExcelEngine excelEngine = new ExcelEngine();
            IApplication application = excelEngine.Excel;
            IWorkbook workbook = application.Workbooks.Open(FileName, ExcelOpenType.Automatic);
            IWorksheet sheet = workbook.Worksheets[0];

            string ScanKey = "";
            string Name = "";
            string StoreNumber = "";
            string StoreSuffix = "";
            string Sequence = "";
            string CompanyName = "";
            string ContactName = "";
            string BillingAddress = "";
            string AddressLine1 = "";
            string AddressLine2 = "";
            string AddressLine3 = "";
            string AddressLine4 = "";
            string City = "";
            string StateOrProvince = "";
            string PostalCode = "";
            string Country = "";
            string PhoneNumber = "";
            string Notes = "";
            string FaxNumber = "";
            string EmailAddress = "";
            string ClientLocationIDs = "";
            decimal ClientLocationID = -1;
            int Row = 2;

            int ID = -1;
            int StatusColumn = 22;
            sheet.Range[1, StatusColumn].Value = "Status";
            while (sheet.Range[Row, 1].Text != null && sheet.Range[Row, 1].Text.Length > 0)          // Scankey
            {
                if (GenerateClient == true) { ClientID = -1; }

                ScanKey = (sheet.Range[Row, 1].Value == null ? "" : sheet.Range[Row, 1].Value);
                Name = (sheet.Range[Row, 2].Value == null ? "" : sheet.Range[Row, 2].Value);
                StoreNumber = (sheet.Range[Row, 3].Value == null ? "" : sheet.Range[Row, 3].Value);
                StoreSuffix = (sheet.Range[Row, 4].Value == null ? "" : sheet.Range[Row, 4].Value);
                Sequence = (sheet.Range[Row, 5].Value == null ? "" : sheet.Range[Row, 5].Value);
                CompanyName = (sheet.Range[Row, 6].Value == null ? "" : sheet.Range[Row, 6].Value);
                ContactName = (sheet.Range[Row, 7].Value == null ? "" : sheet.Range[Row, 7].Value);
                BillingAddress = (sheet.Range[Row, 8].Value == null ? "" : sheet.Range[Row, 8].Value);
                AddressLine1 = (sheet.Range[Row, 9].Value == null ? "" : sheet.Range[Row, 9].Value);
                AddressLine2 = (sheet.Range[Row, 10].Value == null ? "" : sheet.Range[Row, 10].Value);
                AddressLine3 = (sheet.Range[Row, 11].Value == null ? "" : sheet.Range[Row, 11].Value);
                AddressLine4 = (sheet.Range[Row, 12].Value == null ? "" : sheet.Range[Row, 12].Value);
                City = (sheet.Range[Row, 13].Value == null ? "" : sheet.Range[Row, 13].Value);
                StateOrProvince = (sheet.Range[Row, 14].Value == null ? "" : sheet.Range[Row, 14].Value);
                PostalCode = (sheet.Range[Row, 15].Value == null ? "" : sheet.Range[Row, 15].Value);
                Country = (sheet.Range[Row, 16].Value == null ? "" : sheet.Range[Row, 16].Value);
                PhoneNumber = (sheet.Range[Row, 17].Value == null ? "" : sheet.Range[Row, 17].Value);
                Notes = (sheet.Range[Row, 18].Value == null ? "" : sheet.Range[Row, 18].Value);
                FaxNumber = (sheet.Range[Row, 19].Value == null ? "" : sheet.Range[Row, 19].Value);
                EmailAddress = (sheet.Range[Row, 20].Value == null ? "" : sheet.Range[Row, 20].Value);
                ClientLocationIDs = (sheet.Range[Row, 21].Value == null ? "" : sheet.Range[Row, 21].Value);
                sheet.Range[Row, StatusColumn].Value = "";
                if (decimal.TryParse(ClientLocationIDs, out ClientLocationID) == false) { ClientLocationID = -1; }
                if (Name.Length > 50) { Name = Name.Substring(0, 50); }

                if (GenerateClient == true)
                {
                    ClientManager CM = new ClientManager(User.Identity.Name);

                    // We need to add a new client and get their ClientID.
                    Client c = new Client();
                    c.StatusID = CM.GetClientStatusID("Active");
                    c.Name = Name;
                    c.ScanKey = ScanKey;
                    c.AddressLine1 = AddressLine1;
                    c.AddressLine2 = AddressLine2;
                    c.AddressLine3 = AddressLine3;
                    c.AddressLine4 = AddressLine4;
                    c.Country = Country;
                    c.BillingAddress = "";
                    c.City = City;
                    c.CompanyName = CompanyName;
                    c.ContactName = ContactName;
                    c.CreateUser = User.Identity.Name;
                    c.EmailAddress = EmailAddress;
                    c.FaxNumber = FaxNumber;
                    c.Notes = Notes;
                    c.PhoneNumber = PhoneNumber;
                    c.PortalName = "";
                    c.PostalCode = PostalCode;
                    c.ProductTag = "";
                    c.RepairForm = "";
                    c.RMASuffix = "";
                    c.StateOrProvince = StateOrProvince;
                    //c.StatusID = -1;
                    c.Username = User.Identity.Name;
                    c.HideOnPreReceiveList = false;
                    c.InWarrentyPricing = false;
                    c.isVendorGroup = false;
                    c.LastUpdateDate = DateTime.Now;
                    c.LastUpdateUser = User.Identity.Name;
                    //ctx.Clients.InsertOnSubmit(c);
                    //ctx.SubmitChanges();
                    ClientID = CM.InsertClient(c);;
                }


                ClientLocation cl = null;
                if (ClientLocationID > 0)
                {
                    cl = clm.GetClientLocation(ClientLocationID);
                    if (cl.ScanKey.ToUpper() != ScanKey.ToUpper())
                    {
                        // We have to verify there is not a different scankey out there.
                        ClientLocation clx = clm.GetClientLocationFromScanCode(ScanKey, ClientID);
                        if (clx != null)
                        {
                            // we have found a unit where the Scankey is being changed... but is already out there.,
                            sheet.Range[Row, 20].Value = "Scankey is duplicate... Not added/updated.";
                            continue;
                        }
                    }
                }
                if (cl == null) { cl = clm.GetClientLocationFromScanCode(ScanKey, ClientID); }

                if (cl == null)
                {
                    cl = new ClientLocation();
                    cl.ClientID = ClientID;
                    cl.ScanKey = ScanKey;
                    cl.Name = Name;
                    cl.StoreNumber = StoreNumber;
                    cl.StoreSuffix = StoreSuffix;
                    if (int.TryParse(Sequence, out ID) == false) { ID = 10; }
                    cl.Sequence = ID;
                    cl.CompanyName = CompanyName;
                    cl.ContactName = ContactName;
                    cl.BillingAddress = BillingAddress;
                    if (AddressLine1.Length > 50) { if (BillingAddress.Length == 0) { cl.BillingAddress = AddressLine1; } }
                    else { cl.AddressLine1 = AddressLine1; }
                    cl.AddressLine2 = AddressLine2;
                    cl.AddressLine3 = AddressLine3;
                    cl.AddressLine4 = AddressLine4;
                    cl.City = City;
                    cl.StateOrProvince = StateOrProvince;
                    cl.PostalCode = PostalCode;
                    cl.Country = Country;
                    cl.PhoneNumber = PhoneNumber;
                    cl.Notes = Notes;
                    cl.FaxNumber = FaxNumber;
                    cl.EmailAddress = EmailAddress;
                    cl.StatusID = clm.GetClientLocationStatusID("Active");
                    clm.InsertClientLocation(cl);
                    sheet.Range[Row, StatusColumn].Value = "Added";
                }
                else if (GenerateClient == false)         // we don't want to update is we are doing a "Generate Client also" run.
                {
                    cl.Name = Name;
                    cl.ScanKey = ScanKey;   // This i here because Jody wanted the user to be able to update this field. there were complications because this is the key used to find the data
                    // I adjusted this to use the ClientLocationID first.
                    cl.StoreNumber = StoreNumber;
                    cl.StoreSuffix = StoreSuffix;
                    //if (int.TryParse(Sequence, out ID) == false) { ID = 10; }
                    //cl.Sequence = ID;
                    cl.CompanyName = CompanyName;
                    cl.ContactName = ContactName;
                    cl.BillingAddress = BillingAddress;
                    if (AddressLine1.Length > 50) { if (BillingAddress.Length == 0) { cl.BillingAddress = AddressLine1; } }
                    else { cl.AddressLine1 = AddressLine1; }
                    cl.AddressLine2 = AddressLine2;
                    cl.AddressLine3 = AddressLine3;
                    cl.AddressLine4 = AddressLine4;
                    cl.Country = Country;
                    cl.City = City;
                    cl.StateOrProvince = StateOrProvince;
                    cl.PostalCode = PostalCode;
                    cl.PhoneNumber = PhoneNumber;
                    cl.Notes = Notes;
                    cl.FaxNumber = FaxNumber;
                    cl.EmailAddress = EmailAddress;
                    clm.UpdateClientLocation(cl);
                    sheet.Range[Row, StatusColumn].Value = "Updated";
                }
                else
                {
                    sheet.Range[Row, StatusColumn].Value = "Scankey found, Record not updated or added.";
                }
                Row++;
            }
            //workbook.SaveAs("MasterCMMC_Uploaded.xls", Page.Response, ExcelDownloadType.Open);
            workbook.SaveAs("Client_Uploaded.xls", Page.Response, ExcelDownloadType.Open);
            workbook.Close();
            excelEngine.Dispose();
        }


        private void ExportClientLocationToExcel(decimal ClientID)
        {

            ClientLocationManager clm = new ClientLocationManager(User.Identity.Name);
            List<ClientLocation> clList = clm.GetClientLocations(ClientID);

            ExcelEngine excelEngine = new ExcelEngine();
            IApplication application = excelEngine.Excel;
            IWorkbook workbook = application.Workbooks.Create(1);
            IWorksheet sheet = workbook.Worksheets[0];
            int Row = 1;
            int Col = 1;
            int StartCol = Col;
            int StartRow = Row;

            //Add Header
            sheet.Range[Row, 1].Text = "ScanKey";
            sheet.Range[Row, 2].Text = "Name";
            sheet.Range[Row, 3].Text = "StoreNumber";
            sheet.Range[Row, 4].Text = "StoreSuffix";
            sheet.Range[Row, 5].Text = "Sequence";
            sheet.Range[Row, 6].Text = "CompanyName";
            sheet.Range[Row, 7].Text = "ContactName";
            sheet.Range[Row, 8].Text = "BillingAddress";
            sheet.Range[Row, 9].Text = "AddressLine1";
            sheet.Range[Row, 10].Text = "AddressLine2";
            sheet.Range[Row, 11].Text = "AddressLine3";
            sheet.Range[Row, 12].Text = "AddressLine4";
            sheet.Range[Row, 13].Text = "City";
            sheet.Range[Row, 14].Text = "StateOrProvince";
            sheet.Range[Row, 15].Text = "PostalCode";
            sheet.Range[Row, 16].Text = "Country";
            sheet.Range[Row, 17].Text = "PhoneNumber";
            sheet.Range[Row, 18].Text = "Notes";
            sheet.Range[Row, 19].Text = "FaxNumber";
            sheet.Range[Row, 20].Text = "EmailAddress";
            sheet.Range[Row, 21].Text = "ClientLocationID";
            foreach (var x in clList)
            {
                Row++;
                sheet.Range[Row, 1].Text = (x.ScanKey == null ? "" : x.ScanKey.ToString());
                sheet.Range[Row, 2].Text = (x.Name == null ? "" : x.Name.ToString());
                sheet.Range[Row, 3].Text = (x.StoreNumber == null ? "" : x.StoreNumber.ToString());
                sheet.Range[Row, 4].Text = (x.StoreSuffix == null ? "" : x.StoreSuffix.ToString());
                sheet.Range[Row, 5].Text = (x.Sequence == null ? "" : x.Sequence.ToString());
                sheet.Range[Row, 6].Text = (x.CompanyName == null ? "" : x.CompanyName.ToString());
                sheet.Range[Row, 7].Text = (x.ContactName == null ? "" : x.ContactName.ToString());
                sheet.Range[Row, 8].Text = (x.BillingAddress == null ? "" : x.BillingAddress.ToString());
                sheet.Range[Row, 9].Text = (x.AddressLine1 == null ? "" : x.AddressLine1.ToString());
                sheet.Range[Row, 10].Text = (x.AddressLine2 == null ? "" : x.AddressLine2.ToString());
                sheet.Range[Row, 11].Text = (x.AddressLine3 == null ? "" : x.AddressLine3.ToString());
                sheet.Range[Row, 12].Text = (x.AddressLine4 == null ? "" : x.AddressLine4.ToString());
                sheet.Range[Row, 13].Text = (x.City == null ? "" : x.City.ToString());

                sheet.Range[Row, 14].Text = (x.StateOrProvince == null ? "" : x.StateOrProvince.ToString());

                sheet.Range[Row, 15].Text = (x.PostalCode == null ? "" : x.PostalCode.ToString());
                sheet.Range[Row, 16].Text = (x.Country == null ? "" : x.Country.ToString());
                sheet.Range[Row, 17].Text = (x.PhoneNumber == null ? "" : x.PhoneNumber.ToString());
                sheet.Range[Row, 18].Text = (x.Notes == null ? "" : x.Notes.ToString());
                sheet.Range[Row, 19].Text = (x.FaxNumber == null ? "" : x.FaxNumber.ToString());
                sheet.Range[Row, 20].Text = (x.EmailAddress == null ? "" : x.EmailAddress.ToString());
                sheet.Range[Row, 21].Text = (x.ClientLocationID.ToString());

            }
            workbook.SaveAs("MasterClientLocations.xls", Page.Response, ExcelDownloadType.Open);
            workbook.Close();
            excelEngine.Dispose();
        }
        #endregion

        #endregion



        #region QuestionAnswerRestrictions



        //private void LoadData(decimal QuestionID, HiddenField HName, CheckBoxList c1, bool bEnabled)
        //{
        //    AnswerManager am = new AnswerManager(User.Identity.Name);
        //    decimal ClientID = decimal.Parse(MainGrid.SelectedValue.ToString());
        //    List<Option> rec = am.GetTheseAnswers(QuestionID);
        //    List<decimal> dl = am.GetClientAnswerRestrictionList(ClientID, QuestionID);

        //    c1.Items.Clear();
        //    c1.Visible = false;
        //    //clsLinqDataContext ctx = new clsLinqDataContext();

        //    HName.Value = QuestionID.ToString(); // am.QuestionType(TargetID).ToUpper();

        //    int xmLength = 0;
        //    int xCount = 0;
        //    int xSet = 7;
        //    c1.Visible = true;
        //    foreach (Option o in rec.OrderBy(x => x.Sequence))
        //    {
        //        ListItem x = new ListItem(o.OptionText, o.OptionID.ToString());
        //        if (o.MacroKey != null && o.MacroKey.Trim().Length > 0)
        //            x.Text += "." + o.MacroKey;
        //        xCount += 1;
        //        xmLength += x.Text.Length;
        //        if (xmLength > 100)
        //        {
        //            if (xCount < xSet) { xSet = xCount; }
        //            xCount = 0;
        //            xmLength = 0;
        //        }
        //        if (dl.Contains(o.OptionID))
        //        {
        //            x.Selected = true;
        //        }
        //        c1.Items.Add(x);
        //        c1.Items[c1.Items.Count - 1].Attributes.Add("someValue", o.OptionID.ToString());
        //    }
        //    c1.RepeatColumns = xSet - 1;
        //    c1.Enabled = bEnabled;
        //}




        //void grdAnswers_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        // HiddenField HID = (HiddenField)e.Row.FindControl("HiddenID");
        //        HiddenField HName = (HiddenField)e.Row.FindControl("HiddenName");
        //        CheckBoxList c1 = (CheckBoxList)e.Row.FindControl("checkAnswer");
        //        Question rec = (Question)e.Row.DataItem;
        //        LoadData(rec.QuestionID, HName, c1, true);
        //    }
        //}

        //void btnUpdateARestriction_Click(object sender, EventArgs e)
        //{
        //    List<PairIDValue> Keys = new List<PairIDValue>();
        //    // Get ClientID
        //    decimal ClientID = decimal.Parse(MainGrid.SelectedValue.ToString());
        //    // Get QuestionID
        //    decimal QuestionID = decimal.Parse(drpQuestion.SelectedValue.ToString());

        //    GridView G = grdAnswers;
        //    foreach (GridViewRow r in G.Rows)
        //    {
        //        if (r.RowType == DataControlRowType.DataRow)
        //        {
        //            CheckBoxList c1 = (CheckBoxList)r.FindControl("checkAnswer");
        //            // HiddenField HName = (HiddenField)r.FindControl("HiddenName");
        //            foreach (ListItem i in c1.Items)
        //            {
        //                decimal id = -1;
        //                string sid = "";
        //                sid = i.Value;
        //                if (decimal.TryParse(sid, out id) == false) { id = -1; }
        //                if (id > 0 && i.Selected == true) { Keys.Add(new PairIDValue { ID = id, Desc = "1" }); }
        //                else { Keys.Add(new PairIDValue { ID = id, Desc = "0" }); }
        //            }
        //            //am.UpdateDependencies(SourceID, TargetID, SourceOptionID, targetIDsOn, targetIDsOff);
        //            if (ClientID > 0)
        //            {
        //                ClientManager cm = new ClientManager(User.Identity.Name);
        //                cm.UpdateClientAnswerRestrictions(ClientID, QuestionID, Keys);
        //            }
        //        }
        //    }
        //}
        //void btnUpdateQRestriction_Click(object sender, EventArgs e)
        //{
        //    List<PairIDValue> Keys = new List<PairIDValue>();
        //    // Get ClientID
        //    decimal xID = decimal.Parse(MainGrid.SelectedValue.ToString());
        //    // Get Project
        //    decimal PID = decimal.Parse(drpProject.SelectedValue.ToString());
        //    // Get all Questions for Update
        //    foreach (GridViewRow r in grdQuestions.Rows)
        //    {
        //        decimal qID = -1;

        //        string sID = (r.Cells[0].Text == blank ? "" : r.Cells[0].Text);
        //        if (decimal.TryParse(sID, out qID) == false) { qID = -1; }

        //        CheckBox CB = (CheckBox)r.FindControl("chkThisQuestion");
        //        if (CB != null && qID > 0)
        //        {
        //            Keys.Add(new PairIDValue { ID = qID, Desc = (CB.Checked == true ? "1" : "0") });
        //        }
        //    }

        //    if (xID > 0)
        //    {
        //        ClientManager cm = new ClientManager(User.Identity.Name);
        //        cm.UpdateClientRestrictions(xID, PID, Keys);
        //    }
        //}



        //void drpProject_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    UpdateClientQuestionRestrictionGrid();
        //}
        //void drpQuestion_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //    UpdateClientAnswerRestrictionGrid();
        //}
        //protected void UpdateQuestionTabs()
        //{
        //    QuestionManager qm = new QuestionManager(User.Identity.Name);
        //    List<string> types = new List<string> { "RADIALBUTTON", "DROPDOWN", "CHECKBOX" };
        //    List<Question> ql = qm.GetQuestionsTheseTypes(types);
        //    drpQuestion.Items.Clear();
        //    foreach (Question q in ql)
        //    {
        //        ListItem x = new ListItem(q.Description + "(" + q.Name + ")", q.QuestionID.ToString());
        //        drpQuestion.Items.Add(x);
        //    }
        //    drpQuestion.SelectedIndex = 0;
        //}
        //protected void UpdateProjectTabs()
        //{
        //    ProjectManager pm = new ProjectManager(User.Identity.Name);
        //    var Projs = pm.GetMasterProjectList();
        //    drpProject.Items.Clear();
        //    ListItem z = new ListItem("All", "-1");
        //    drpProject.Items.Add(z);
        //    foreach (Project p in Projs)
        //    {
        //        ListItem x = new ListItem(p.Name, p.ProjectID.ToString());
        //        drpProject.Items.Add(x);
        //    }
        //    drpProject.SelectedIndex = 0;
        //}
        //protected void LoadQuestionAnswers(decimal QuestionID)
        //{
        //    QuestionManager qm = new QuestionManager(User.Identity.Name);
        //    grdAnswers.DataSource = qm.GetQuestionsDefered().Where(x => x.QuestionID == QuestionID);
        //    grdAnswers.DataBind();
        //}
        //protected void UpdateQuestionGrid()
        //{
        //    QuestionManager qm = new QuestionManager(User.Identity.Name);
        //    grdQuestions.DataSource = qm.GetQuestionsDefered();
        //    grdQuestions.DataBind();
        //}
        //protected void UpdateClientAnswerRestrictionGrid()
        //{
        //    decimal xID = decimal.Parse(MainGrid.SelectedValue.ToString());
        //    // Get Project
        //    decimal PID = decimal.Parse(drpQuestion.SelectedValue.ToString());
        //    // Get all Questions for Update

        //    LoadQuestionAnswers(PID);

        //    ClientManager cm = new ClientManager(User.Identity.Name);
        //    List<PairIDValue> RA = cm.ClientAnswerRestrictionList(xID, PID);


        //    //foreach (GridViewRow r in grdAnswers.Rows)
        //    //{
        //    //    decimal qID = -1;

        //    //    string sID = (r.Cells[0].Text == blank ? "" : r.Cells[0].Text);
        //    //    if (decimal.TryParse(sID, out qID) == false) { qID = -1; }

        //    //    CheckBox CB = (CheckBox)r.FindControl("chkThisQuestion");
        //    //    if (CB != null && qID > 0)
        //    //    {
        //    //        PairIDValue p = RA.FirstOrDefault(x => x.ID == qID);
        //    //        if (p == null)
        //    //        {
        //    //            CB.Checked = false;
        //    //        }
        //    //        else
        //    //        {
        //    //            CB.Checked = true;
        //    //        }
        //    //    }
        //    //}
        //}
        //protected void UpdateClientQuestionRestrictionGrid()
        //{

        //    decimal xID = decimal.Parse(MainGrid.SelectedValue.ToString());
        //    // Get Project
        //    decimal PID = decimal.Parse(drpProject.SelectedValue.ToString());
        //    // Get all Questions for Update
        //    ClientManager cm = new ClientManager(User.Identity.Name);
        //    List<PairIDValue> RQ = cm.ClientRestrictionList(xID, PID);
        //    try
        //    {
        //        foreach (GridViewRow r in grdQuestions.Rows)
        //        {
        //            decimal qID = -1;

        //            string sID = (r.Cells[0].Text == blank ? "" : r.Cells[0].Text);
        //            if (decimal.TryParse(sID, out qID) == false) { qID = -1; }

        //            CheckBox CB = (CheckBox)r.FindControl("chkThisQuestion");
        //            if (CB != null && qID > 0)
        //            {

        //                CB.Checked = false;
        //                PairIDValue p = RQ.FirstOrDefault(x => x.ID == qID);
        //                if (p == null)
        //                {

        //                }
        //                else
        //                {
        //                 //   CB.Checked = true;
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        //throw;
        //    }

        //}

        #endregion



    }
}
