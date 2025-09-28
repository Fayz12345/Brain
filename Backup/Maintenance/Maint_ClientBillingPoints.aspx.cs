using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using Factory_DataModel;
using BW_WebApp.DataManagers;

namespace BW_WebApp.Maintenance
{
    public partial class Maint_ClientBillingPoints : System.Web.UI.Page
    {
        //private string blank = "&nbsp;";

        protected void Page_Load(object sender, EventArgs e)
        {
            grdBillingPoints.RowDataBound += new GridViewRowEventHandler(grdBillingPoints_RowDataBound);
            btnSaveBillingPoints.Click += new EventHandler(btnSaveBillingPoints_Click);
            btnLoadBillingPointsDefault.Click += new EventHandler(btnLoadBillingPointsDefault_Click);

            if (IsPostBack == false)
            {
                lblClientTest_01.Text = "Billing Points";
                string zID = Request.QueryString.Get("ID");
                hdnClientID.Value = zID;
                decimal ID = -1;
                if (decimal.TryParse(zID, out ID) == false) { ID = -1; };

                ClientManager cm = new ClientManager(User.Identity.Name);
                Client c = cm.GetClient(ID);
                if (c != null)
                {
                    lblClientTest_01.Text = "Billing Points for:" + c.CompanyName;
                }
                UpdateBillingPoints(ID);
            }
        }


        void grdBillingPoints_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Type type = e.Row.DataItem.GetType();               //.GetGenericArguments()[0]; 
                HiddenField hf = (HiddenField)e.Row.FindControl("hdnClientBillingPointID");
                if (hf != null) { hf.Value = ((GetMasterBillingPointsResult)e.Row.DataItem).ClientBillingPointID.ToString(); }
                hf = (HiddenField)e.Row.FindControl("hdnProjectID");
                if (hf != null) { hf.Value = ((GetMasterBillingPointsResult)e.Row.DataItem).ProjectID.ToString(); }
                hf = (HiddenField)e.Row.FindControl("hdnProcessID");
                if (hf != null) { hf.Value = ((GetMasterBillingPointsResult)e.Row.DataItem).ProcessID.ToString(); }
                hf = (HiddenField)e.Row.FindControl("hdnClientID");
                if (hf != null) { hf.Value = ((GetMasterBillingPointsResult)e.Row.DataItem).ClientID.ToString(); }

                CheckBox cb = (CheckBox)e.Row.FindControl("chkisBillingPoint");
                TextBox tb = (TextBox)e.Row.FindControl("txtRateValue");
                if (cb != null)
                {
                    cb.Checked = false;
                    if (((GetMasterBillingPointsResult)e.Row.DataItem).BillingPoint == true)
                    {
                        cb.Checked = true;
                    }
                }
                tb.Text = "";
                if (tb != null)
                {
                    tb.Text = ((GetMasterBillingPointsResult)e.Row.DataItem).RateValue.ToString();
                }
            }
        }
        protected void UpdateBillingPoints(decimal ClientID)
        {
            lblDefault.Text = "";
            if (ClientID < 1) { lblDefault.Text = "Default Loaded"; }
            ClientManager cm = new ClientManager(User.Identity.Name);
            grdBillingPoints.DataSource = cm.GetClientBillingPoints(ClientID);
            grdBillingPoints.DataBind();
        }

        void btnLoadBillingPointsDefault_Click(object sender, EventArgs e)
        {
            UpdateBillingPoints(-1);
        }
        void btnSaveBillingPoints_Click(object sender, EventArgs e)
        {
            decimal ClientBillingPointID = -1;
            decimal ProjectID = -1;
            decimal ProcessID = -1;
            decimal ClientID = -1;


            string sID = "";
            decimal RateValue = 0;
            CheckBox cb;
            HiddenField hf;

            foreach (GridViewRow row in grdBillingPoints.Rows)
            {
                cb = (CheckBox)row.FindControl("chkisBillingPoint");
                hf = (HiddenField)row.FindControl("hdnClientBillingPointID");
                if (hf != null) { sID = hf.Value; }
                if (decimal.TryParse(sID, out ClientBillingPointID) == false) { ClientBillingPointID = -1; }
                hf = (HiddenField)row.FindControl("hdnClientID");
                if (hf != null) { sID = hf.Value; }
                if (decimal.TryParse(sID, out ClientID) == false) { ClientID = -1; }

                if (cb.Checked == true || ClientBillingPointID > 0)
                {
                    ClientManager cm = new ClientManager(User.Identity.Name);
                    if (cb.Checked == false)
                    {
                        cm.DeleteBillingPoint(ClientBillingPointID);
                        // delete the ClientBillingPoint Record
                    }
                    else
                    {
                        hf = (HiddenField)row.FindControl("hdnProjectID");
                        if (hf != null) { sID = hf.Value; }
                        if (decimal.TryParse(sID, out ProjectID) == false) { ProjectID = -1; }

                        hf = (HiddenField)row.FindControl("hdnProcessID");
                        if (hf != null) { sID = hf.Value; }
                        if (decimal.TryParse(sID, out ProcessID) == false) { ProcessID = -1; }

                        TextBox tb = (TextBox)row.FindControl("txtRateValue");

                        if (decimal.TryParse(tb.Text, out RateValue) == false) { RateValue = 0; }

                        cm.AddUpdateClientBillingPoint(ClientBillingPointID, ClientID, ProjectID, ProcessID, RateValue);
                        // Add a new ClientBillingPoint Record.
                    }
                }
            }
            UpdateBillingPoints(ClientID);
        }

    }
}