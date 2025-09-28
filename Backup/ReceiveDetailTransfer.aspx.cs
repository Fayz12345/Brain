using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Drawing;

using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Web.Security;
using Syncfusion.Web.UI.WebControls.Shared;
// using ScanKey;
//using Factory_DataModel;
using BW_WebApp.DataManagers;

namespace BW_WebApp
{
    public partial class ReceiveDetailTransfer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            hdnUserName.Value = User.Identity.Name;
            if (ScanKey.Text.Length > 0) { ProcessScanKey(); }
            if (txtClientScanKey.Text.Length > 0) { ProcessClientCode(); }
            btnTransfer.Click += new EventHandler(btnTransfer_Click);
            btnClientSearch.Click += new EventHandler(btnClientSearch_Click);
            if (!IsPostBack)
            {

                ///////////////////////////////////////////////////////////////////////
                //this.wndSelectClientLocation.Modal = true;
                //this.wndSelectClientLocation.RightToLeft = RightToLeft.No;
                //this.wndSelectClientLocation.BackColor = Color.FromName("Red");
                //this.wndSelectClientLocation.Height = 550;
                //this.wndSelectClientLocation.Width = 500;
                //this.wndSelectClientLocation.ResizeMode = WindowResizeModeType.FreeStyle;
                ///////////////////////////////////////////////////////////////////////


                //lblMakeModelColour.Text = "Make, Model, Colour";
                //lblFromClient.Text = "Client Address";
                //lblToClient.Text = "Client Address";

                ProjectManager pm = new ProjectManager(User.Identity.Name);

                drpProjectList.DataValueField = "ProjectID";
                drpProjectList.DataTextField = "Name";
                drpProjectList.DataSource = pm.GetProjectList();         // pm.GetMasterProjectList();
                drpProjectList.DataBind();
                drpProjectList.SelectedIndex = 0;
            }

        }
        void btnClientSearch_Click(object sender, EventArgs e)
        {
            ProcessClientCode();
        }
        void btnTransfer_Click(object sender, EventArgs e)
        {
            if (drpTransferType.SelectedValue == "3")
            {
                //GraveYard(); 
            }
            else if (drpTransferType.SelectedValue == "0") { ProjectTransfer(); }
            //else { Transfer(); }
        }
        //void GraveYard()
        //{
        //    decimal ReceiveDetailID = -1;
        //    if (decimal.TryParse(hdnReceiveDetailID.Value, out ReceiveDetailID) == false) { ReceiveDetailID = -1; }
        //    ReceiveDetailManager rm = new ReceiveDetailManager(User.Identity.Name);
        //    int xCount = 0;
        //    if (hdnScanKey.Value.Length > 5 && hdnScanKey.Value.Substring(0, 5).ToUpper() == "XBINX")
        //    {
        //        List<ReceiveDetail> rdl = rm.GetReceiveDetail_XBINX(hdnScanKey.Value);
        //        foreach (ReceiveDetail rd in rdl) { rm.MoveToGraveYard(rd.ReceiveDetailID); xCount++; }
        //        lblMakeModelColour.Text = hdnScanKey.Value + " - Graveyard";
        //    }
        //    else
        //    {
        //        ReceiveDetail rd = rm.ReceiveDetail(hdnScanKey.Value);
        //        if (rd != null) { rm.MoveToGraveYard(rd.ReceiveDetailID); xCount++; }
        //    }
        //    ScriptManager.RegisterStartupScript(this, GetType(), "Graveyard", "alert('Number of units graveyard:" + xCount.ToString() + "');", true);
        //}

        void ProjectTransfer()
        {
            decimal ReceiveDetailID = -1;

            decimal ClientLocationID = decimal.Parse(hdnToClientID.Value);
            if (ClientLocationID == null || ClientLocationID < 1)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "LoadClient", "alert('Units not transfered, invalid Client Location');", true);
                return;
            }
            decimal ProjectID = decimal.Parse(drpProjectList.SelectedItem.Value);
            if (ProjectID == null || ProjectID < 1)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "LoadClient", "alert('Units not transfered, invalid Project');", true);
                return;
            }
            string ProjectString = drpProjectList.SelectedItem.Text;

            //decimal ProcessID = -1;
            if (decimal.TryParse(hdnReceiveDetailID.Value, out ReceiveDetailID) == false) { ReceiveDetailID = -1; }
            bool IsXBINX = false;
            if (hdnScanKey.Value.Length > 5 && hdnScanKey.Value.Substring(0, 5).ToUpper() == "XBINX") { IsXBINX = true; }
            if (ReceiveDetailID < 1 && IsXBINX == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "LoadClient", "alert('IMEI is not a valid Unit IMEI.');", true);
            }
            else
            {
                int xCount = 0;
                using (clsLinqDataContext ctx = new clsLinqDataContext())
                {
                    ReceiveDetailManager rm = new ReceiveDetailManager(User.Identity.Name);
                    // bool isDone = false;

                    if (hdnScanKey.Value.Length > 5 && hdnScanKey.Value.Substring(0, 5).ToUpper() == "XBINX")
                    {
                        List<ReceiveDetail> rdl = rm.GetReceiveDetail_XBINX(hdnScanKey.Value);
                        foreach (ReceiveDetail rd in rdl)
                        {

                            xCount += rm.TransferUnitToNewProjecct(ctx, rd.ReceiveDetailID, ProjectID, ProjectString);
                            rm.UpdateReceiveDetailClientLocation(ctx, rd.ReceiveDetailID, -1, ClientLocationID);
                            //rd.ProcessID = ProjectID;
                            //rd.ProjectName = ProjectString;
                            //rm.UpdateReceiveDetail(rd, -1);
                            ////isCloned = CloneReceiveDetail(isCloned, rm, rd);
                            //if (isCloned == true)
                            //{
                            //xCount++;
                            //}
                        }
                        lblMakeModelColour.Text = hdnScanKey.Value + " - Project Transfer";
                    }
                    else
                    {
                        ReceiveDetail rd = rm.ReceiveDetail(hdnScanKey.Value);
                        if (rd != null)
                        {
                            xCount = rm.TransferUnitToNewProjecct(ctx, rd.ReceiveDetailID, ProjectID, ProjectString);
                            rm.UpdateReceiveDetailClientLocation(ctx, rd.ReceiveDetailID, -1, ClientLocationID);
                            //rd.ProcessID = ProjectID;
                            //rd.ProjectName = ProjectString;
                            //rm.UpdateReceiveDetail(rd, -1);
                        }
                    }

                }
                ScriptManager.RegisterStartupScript(this, GetType(), "LoadClient", "alert('Number of units Project transfered: " + xCount.ToString() + "');", true);
            }
        }


        void Transfer()
        {
            decimal ReceiveDetailID = -1;
            decimal ClientLocationID = -1;
            //decimal ProcessID = -1;
            if (decimal.TryParse(hdnReceiveDetailID.Value, out ReceiveDetailID) == false) { ReceiveDetailID = -1; }
            if (decimal.TryParse(hdnToClientID.Value, out ClientLocationID) == false) { ClientLocationID = -1; }

            bool IsXBINX = false;
            if (hdnScanKey.Value.Length > 5 && hdnScanKey.Value.Substring(0, 5).ToUpper() == "XBINX") { IsXBINX = true; }
            if (ClientLocationID < 1)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "LoadClient", "alert('You must select a valid Client Location.');", true);
                return;
            }
            if (ReceiveDetailID < 1 && IsXBINX == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "LoadClient", "alert('IMEI is not a valid Unit IMEI.');", true);
                return;
            }

            ReceiveDetailManager rm = new ReceiveDetailManager(User.Identity.Name);
            bool isCloned = false;
            int xCount = 0;
            if (hdnScanKey.Value.Length > 5 && hdnScanKey.Value.Substring(0, 5).ToUpper() == "XBINX")
            {
                List<ReceiveDetail> rdl = rm.GetReceiveDetail_XBINX(hdnScanKey.Value);
                foreach (ReceiveDetail rd in rdl)
                {
                    isCloned = CloneReceiveDetail(isCloned, rm, rd);
                    if (isCloned == true)
                    {
                        xCount++;
                    }
                }
                lblMakeModelColour.Text = hdnScanKey.Value + " - Transfer";
            }
            else
            {
                ReceiveDetail rd = rm.ReceiveDetail(hdnScanKey.Value);
                if (rd != null)
                {
                    isCloned = CloneReceiveDetail(isCloned, rm, rd);
                }
                if (isCloned == true)
                {
                    ProcessClientCode();
                    ScanKey.Text = hdnScanKey.Value;
                    ProcessScanKey();
                    xCount++;
                }
            }

            ScriptManager.RegisterStartupScript(this, GetType(), "LoadClient", "alert('Number of units transfered:" + xCount.ToString() + "');", true);
        }



        private bool CloneReceiveDetail(bool isCloned, ReceiveDetailManager rm, ReceiveDetail rd)
        {
            decimal ProjectID = -1;
            decimal ClientLocationID = -1;
            if (decimal.TryParse(hdnToClientID.Value, out ClientLocationID) == false) { ClientLocationID = rd.ClientLocationID; }
            if (decimal.TryParse(drpProjectList.SelectedValue, out ProjectID) == false) { ProjectID = (decimal)rd.ProjectID; }
            ProjectManager pm = new ProjectManager(User.Identity.Name);
            Process p = pm.GetReceiveProcessID(ProjectID);
            isCloned = rm.Clone(drpTransferType.SelectedValue, rd.ReceiveDetailID, ProjectID, ClientLocationID, p.ProcessID);
            return isCloned;
        }
        void ProcessScanKey()
        {
            if (ScanKey.Text.Length == 0) { return; }
            hdnReceiveDetailID.Value = "";
            hdnFromClientID.Value = "";
            txtClientNamea.Text = "";
            txtStoreNumbera.Text = "";
            txtStoreSuffixa.Text = "";
            txtClientAddressa.Text = "";
            lblMakeModelColour.Text = "";
            lblProject.Text = "";
            hdnScanKey.Value = "";
            if (ScanKey.Text.Length > 5 && ScanKey.Text.Substring(0, 5).ToUpper() == "XBINX") { lblMakeModelColour.Text = ScanKey.Text + " - Transfer"; hdnScanKey.Value = ScanKey.Text; }
            else
            {
                ReceiveDetailManager rm = new ReceiveDetailManager(User.Identity.Name);
                ReceiveDetail rd = rm.ReceiveDetail(ScanKey.Text);
                if (rd != null)
                {
                    hdnReceiveDetailID.Value = rd.ReceiveDetailID.ToString();
                    ProjectManager pr = new ProjectManager(User.Identity.Name);
                    lblProject.Text = pr.ProjectName(rd.ProjectID);

                    ClientManager cm = new ClientManager(User.Identity.Name);
                    ClientLocation cl = cm.GetClientLocation(rd.ClientLocationID);
                    if (cl != null)
                    {
                        hdnFromClientID.Value = cl.ClientLocationID.ToString();
                        txtClientNamea.Text = cl.Name + " " + cl.CompanyName;
                        txtStoreNumbera.Text = cl.StoreNumber;
                        txtStoreSuffixa.Text = cl.StoreSuffix;
                        txtClientAddressa.Text = cl.AddressLine1 + Environment.NewLine + cl.AddressLine2 + Environment.NewLine + cl.City + Environment.NewLine + cl.StateOrProvince + Environment.NewLine + cl.PostalCode;
                    }
                    ListItem li;
                    li = drpProjectList.Items.FindByValue(rd.ProjectID.ToString());
                    if (li != null) { drpProjectList.SelectedIndex = -1; li.Selected = true; }
                    lblMakeModelColour.Text = rd.ESN + " - " + rm.MakeModelColour(rd.ReceiveDetailID);
                    hdnScanKey.Value = ScanKey.Text;
                }
                else { lblMakeModelColour.Text = ScanKey.Text + " - Not Found"; hdnScanKey.Value = ""; }
            }
            ScanKey.Text = "";
            drpProjectList.Focus();
        }
        void ProcessClientCode()
        {
            if (txtClientScanKey.Text.Length == 0) { return; }
            hdnToClientID.Value = "";
            txtClientName.Text = txtClientScanKey.Text + " - Client Not Found";
            txtStoreNumber.Text = "";
            txtStoreSuffix.Text = "";
            txtClientAddress.Text = "";

            ClientManager cm = new ClientManager(User.Identity.Name);
            using (clsLinqDataContext ctx = cm.GetDataContext(User.Identity.Name))
            {
                ClientLocation cl = cm.GetClientLocation(ctx, txtClientScanKey.Text);
                if (cl != null)
                {
                    hdnToClientID.Value = cl.ClientLocationID.ToString();
                    txtClientName.Text = cl.Client.Name + " " + cl.CompanyName;
                    txtStoreNumber.Text = cl.StoreNumber;
                    txtStoreSuffix.Text = cl.StoreSuffix;
                    txtClientAddress.Text = cl.AddressLine1 + Environment.NewLine + cl.AddressLine2 + Environment.NewLine + cl.City + Environment.NewLine + cl.StateOrProvince + Environment.NewLine + cl.PostalCode;
                }
            }

            txtClientScanKey.Text = "";
            ScanKey.Focus();
        }
        public static string WrappableText(string source)
        {
            string nwln = Environment.NewLine;
            return "<p>" +
            source.Replace(nwln + nwln, "</p><p>")
            .Replace(nwln, "<br />") + "</p>";
        }


    }
}