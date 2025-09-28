using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using System.Web.Configuration;

//using Syncfusion.Web.UI.WebControls.Shared;
using Syncfusion.XlsIO;

//using Factory_DataModel;
using GMPI_WebApp.DataManagers;

namespace GMPI_WebApp.Maintenance
{
    public partial class Maint_MasterPartTable : System.Web.UI.Page
    {
        string Role_EditAccess = "PartsAdmin";
        string Manufacturer = "";
        string Model = "";
        //decimal PartNumberGridUpdateLimit = 0;
        //List<MasterPartsLinkTable> mParts = null;
        //List<MasterPartsLinkTablePriceList> mPartsPrice = null;
        //List<PartNumberBucketInventoryTransactionType> TypeList = new List<PartNumberBucketInventoryTransactionType>();
        //List<MasterPartsClassType> cList = new List<MasterPartsClassType>();

        protected void Page_Load(object sender, EventArgs e)
        {
            ////string Limit = System.Configuration.ConfigurationManager.AppSettings["PartNumberGridUpdateMax"];
            ////decimal nLimit = 100;
            ////if (decimal.TryParse(Limit, out nLimit) == false) { nLimit = 10000; }
            ////PartNumberGridUpdateLimit = nLimit;


            MainGrid.SelectedIndexChanged += new EventHandler(MainGrid_SelectedIndexChanged);



            MainGridPN.RowDataBound += new GridViewRowEventHandler(MainGridPN_RowDataBound);
            MainGridPN.RowCommand += new GridViewCommandEventHandler(MainGridPN_RowCommand);


            gvLocationEditParts.RowDataBound += new GridViewRowEventHandler(gvLocationEditParts_RowDataBound);
            gvLocationEditParts.RowCommand += new GridViewCommandEventHandler(gvLocationEditParts_RowCommand);

            gvLocationTransferPartsFrom.RowDataBound += new GridViewRowEventHandler(gvLocationTransferPartsFrom_RowDataBound);
            gvLocationTransferPartsFrom.RowCommand += new GridViewCommandEventHandler(gvLocationTransferPartsFrom_RowCommand);


            btnEditDetailSave.Click += new EventHandler(btnEditDetailSave_Click);
            btnEditDetailCancel.Click += new EventHandler(btnEditDetailCancel_Click);

            GridViewDetail.RowDataBound += new GridViewRowEventHandler(GridViewDetail_RowDataBound);
            GridViewDetail.RowCommand += new GridViewCommandEventHandler(GridViewDetail_RowCommand);

            EditModelCancel.Click += new EventHandler(EditModelCancel_Click);
            EditModelSave.Click += new EventHandler(EditModelSave_Click);
            EditCategoryCancel.Click += new EventHandler(EditCategoryCancel_Click);
            btnLocationViewClose.Click += new EventHandler(btnLocationViewClose_Click);
            EditCategorySave.Click += new EventHandler(EditCategorySave_Click);

            btnSavePartNumberse.Click += new EventHandler(btnSavePartNumberse_Click);


            btnAddTransactionCancel.Click += new EventHandler(btnAddTransactionCancel_Click);
            btnAddTransactionSave.Click += new EventHandler(btnAddTransactionSave_Click);
            btnCancelSavee.Click += new EventHandler(btnCancelSavee_Click);
            btnTransferSave.Click += new EventHandler(btnTransferSave_Click);
            btnTransferCancel.Click += new EventHandler(btnTransferCancel_Click);
            btnSavePartNumbers.Click += new EventHandler(btnAddPartNumbers_Click);
            btnRefresh.Click += new EventHandler(btnRefresh_Click);

            ////////chkModels.SelectedIndexChanged += new EventHandler(chkModels_SelectedIndexChanged);
            ////////drpManufacturer.SelectedIndexChanged += new EventHandler(drpManufacturer_SelectedIndexChanged);
            ////////drpDropPart.SelectedIndexChanged += new EventHandler(drpManufacturer_SelectedIndexChanged);
            ////////drpLocationList.SelectedIndexChanged += new EventHandler(drpManufacturer_SelectedIndexChanged);

            btnAddNew.Click += new EventHandler(btnAddNew_Click);
            btnCancelSave.Click += new EventHandler(btnCancelSave_Click);

            btnUpload.Click += new EventHandler(btnUpload_Click);
            btnDownload.Click += new EventHandler(btnDownload_Click);

            btnResetPartNumbers.Visible = false;
            if (User.IsInRole("Administrators")) { btnResetPartNumbers.Visible = false; }

            if (!IsPostBack)
            {


                txtPOReceiptDate.Text = String.Format("{0:MM/dd/yyyy}", DateTime.Now);

                pnlAdd.Visible = false;
                pnlEdit.Visible = false;
                hdnUserName.Value = User.Identity.Name;
                hdnManufacturerID.Value = drpManufacturer.ClientID;
 

                //if (IsPostBack == false)
                //{
                    using (clsLinqDataContext ctx = new clsLinqDataContext())
                    {
                        LoadDropDowns(ctx);
                        UpdateMainGrid(ctx);
                        LoadModelDistinct();
                        LoadClassType(drpAClassType);
                        LoadClassType(drpClassType);
                        LoadTransactionType_Restricted(drpTransType);
                        //UpdateMainGridPM();
                    }
                //}
            }
        }




        void btnLocationViewClose_Click(object sender, EventArgs e)
        {
            pnlLocationView.Visible = false;
            pnlMainGridPN.Visible = true;
            pnlHeader.Visible = true;
        }
        void btnAddTransactionSave_Click(object sender, EventArgs e)
        {
            decimal mpid = -1;
            decimal qty = 0;
            decimal typeid = -1;
            decimal Locationid = -1;
            decimal UnitPurchasePrice = 0;

            if (decimal.TryParse(hdnMasterPartLinkTableID.Value, out mpid) == false) { mpid = -1; }
            if (decimal.TryParse(txtTransQTY.Text, out qty) == false) { qty = 0; }
            if (decimal.TryParse(drpTransType.SelectedValue, out typeid) == false) { typeid = 0; }
            if (decimal.TryParse(drpLocationEditParts.SelectedValue, out Locationid) == false) { Locationid = 0; }

            if (decimal.TryParse(txtTransPurchasePrice.Text, out UnitPurchasePrice) == false) { UnitPurchasePrice = -1; }
            MasterPartManager mpm = new MasterPartManager(User.Identity.Name);
            if (qty > 0)
            {
                pnlAddTransaction.Visible = false;
                pnlMainGridPN.Visible = true;
                pnlHeader.Visible = true;

                drpLocationList.Enabled = true;
                drpDropPart.Enabled = true;
                drpManufacturer.Enabled = true;
                btnRefresh.Enabled = true;

                mpm.AddToInventory(mpid, Locationid, qty, UnitPurchasePrice, typeid, txtTransDesc.Text, txtPOVendor.Text, txtPONumber.Text, txtPOReceiptDate.Text, txtPOLine.Text);
                txtTransQTY.Text = "";
                txtTransDesc.Text = "";
                txtTransPurchasePrice.Text = "";

                UpdateMainGridPM();
            }
        }
        void btnAddTransactionCancel_Click(object sender, EventArgs e)
        {
            pnlAddTransaction.Visible = false;
            pnlMainGridPN.Visible = true;
            pnlHeader.Visible = true;

            drpLocationList.Enabled = true;
            drpDropPart.Enabled = true;
            drpManufacturer.Enabled = true;
            btnRefresh.Enabled = true;
            UpdateMainGridPM();
        }

        void btnSavePartNumberse_Click(object sender, EventArgs e)
        {
            pnlEditPart.Visible = false;
            pnlMainGridPN.Visible = true;
            pnlHeader.Visible = true;

            drpLocationList.Enabled = true;
            drpDropPart.Enabled = true;
            drpManufacturer.Enabled = true;
            btnRefresh.Enabled = true;


            decimal mpid = -1;
            decimal Classtypeid = -1;
            decimal UnitPrice = 0;
            decimal InWarrentyUnitPrice = 0;
            decimal QTYMin = 0;
            decimal QTYMax = 0;
            decimal QTYReorder = 0;

            if (decimal.TryParse(hdnMasterPartLinkTableID.Value, out mpid) == false) { mpid = -1; }

            if (decimal.TryParse(drpClassType.SelectedValue, out Classtypeid) == false) { Classtypeid = 0; }
            if (txtUnitPrice.Text.Length == 0) { txtUnitPrice.Text = "-1"; }
            if (decimal.TryParse(txtUnitPrice.Text, out UnitPrice) == false) { UnitPrice = -1; }
            if (decimal.TryParse(txtInWarrentyPrice.Text, out InWarrentyUnitPrice) == false) { InWarrentyUnitPrice = -1; }

            if (decimal.TryParse(txtInventoryMin.Text, out QTYMin) == false) { QTYMin = -1; }
            if (decimal.TryParse(txtInventoryMax.Text, out QTYMax) == false) { QTYMax = -1; }
            if (decimal.TryParse(txtReorderPoint.Text, out QTYReorder) == false) { QTYReorder = -1; }
            MasterPartManager mpm = new MasterPartManager(User.Identity.Name);

            mpm.AddToInventory(mpid, UnitPrice, InWarrentyUnitPrice, txtGMPPartNumbere.Text, txtMFGPartNumbere.Text, txtGMPDesce.Text, QTYMin, QTYMax, QTYReorder, Classtypeid);
            UpdateMainGridPM();
        }
        void btnCancelSavee_Click(object sender, EventArgs e)
        {
            pnlEditPart.Visible = false;
            pnlMainGridPN.Visible = true;
            pnlHeader.Visible = true;

            drpLocationList.Enabled = true;
            drpDropPart.Enabled = true;
            drpManufacturer.Enabled = true;
            btnRefresh.Enabled = true;
            UpdateMainGridPM();
        }

        void btnTransferCancel_Click(object sender, EventArgs e)
        {
            lblTransferMessage.Text = "";
            txtTransferReason.Text = "";
            txtTransferQTY.Text = "";
            chkAveragePurchasePrice.Checked = true;

            pnlTransfer.Visible = false;
            pnlMainGridPN.Visible = true;
            //btnSaveGridData.Visible = true;
            pnlHeader.Visible = true;

            drpLocationList.Enabled = true;
            drpDropPart.Enabled = true;
            drpManufacturer.Enabled = true;
            btnRefresh.Enabled = true;
            UpdateMainGridPM();
        }
        void btnTransferSave_Click(object sender, EventArgs e)
        {
            decimal QTY = 0;
            decimal MasterPartLinkTableID = -1;
            decimal NewlocationID = -1;
            lblTransferMessage.Text = "";


            decimal IFSFromlocationID = -1;
            decimal IFSTolocationID = -1;
            if (decimal.TryParse(drpLocationTransferPartFrom.SelectedValue, out IFSFromlocationID) == false) { IFSFromlocationID = -1; }
            if (decimal.TryParse(drpLocationTransferPartTo.SelectedValue, out IFSTolocationID) == false) { IFSTolocationID = -1; }



            if (decimal.TryParse(txtTransferQTY.Text, out QTY) == false)
            {
                lblTransferMessage.Text = "Transfer quantity not valid. Transfer aborted!";
                return;
            }
            if (QTY < 1)
            {
                lblTransferMessage.Text = "Transfer quantity must be more than zero. Transfer aborted!";
                return;
            }
            if (drpLocationList2.SelectedItem.Text == drpLocationList.SelectedItem.Text)
            {
                lblTransferMessage.Text = "Transfer location from (" + drpLocationList.SelectedItem.Text + ") and too (" + drpLocationList2.SelectedItem.Text + ") are the same. Transfer aborted!";
                return;
            }

            if (decimal.TryParse(hdnMasterPartLinkTableID.Value, out MasterPartLinkTableID) == false)
            {
                lblTransferMessage.Text = "Invalid PartID. Transfer aborted!";
                return;
            }
            if (decimal.TryParse(drpLocationList2.SelectedItem.Value, out NewlocationID) == false)
            {
                lblTransferMessage.Text = "Invalid New Location ID. Transfer aborted!";
                return;
            }

            MasterPartManager pm = new MasterPartManager(User.Identity.Name);
            lblTransferMessage.Text = pm.TransferInverntory(MasterPartLinkTableID, IFSFromlocationID, IFSTolocationID, QTY, NewlocationID, txtTransferReason.Text, chkAveragePurchasePrice.Checked);
            txtTransferQTY.Text = "";
        }

        void btnEditDetailCancel_Click(object sender, EventArgs e)
        {
            pnlEditDetail.Visible = false;
            pnlMainGridPN.Visible = true;
            pnlHeader.Visible = true;
        }
        void btnEditDetailSave_Click(object sender, EventArgs e)
        {
            decimal MasterPartsLinkTableID = -1;
            if (decimal.TryParse(hdnMasterPartLinkTableID.Value, out MasterPartsLinkTableID) == true)
            {
                MasterPartManager mpm = new MasterPartManager(User.Identity.Name);
                mpm.SetPartAveragePurchasePrice(MasterPartsLinkTableID);
            }
            pnlEditDetail.Visible = false;
            pnlMainGridPN.Visible = true;
            pnlHeader.Visible = true;
            UpdateMainGridPM();
        }
        void EditCategorySave_Click(object sender, EventArgs e)
        {
            decimal ID = -1;
            if (decimal.TryParse(hdnMasterPartLinkTableID.Value, out ID) == false) { ID = -1; }
            decimal mpid = -1;
            if (decimal.TryParse(drpChangeCategoryPart.SelectedItem.Value, out mpid) == false) { mpid = -1; }

            MasterPartManager mpm = new MasterPartManager(User.Identity.Name);
            using (clsLinqDataContext ctx = mpm.GetDataContext(User.Identity.Name))
            {
                mpm.ChangeCategory(ctx, ID, mpid);
            }

            pnlChangeCategory.Visible = false;
            pnlMainGridPN.Visible = true;
            pnlHeader.Visible = true;
            UpdateMainGridPM();
        }
        void EditCategoryCancel_Click(object sender, EventArgs e)
        {
            pnlChangeCategory.Visible = false;
            pnlMainGridPN.Visible = true;
            pnlHeader.Visible = true;
            //btnSaveGridData.Visible = true;
        }
        void EditModelSave_Click(object sender, EventArgs e)
        {
            decimal ID = -1;
            if (decimal.TryParse(hdnMasterPartLinkTableID.Value, out ID) == false) { ID = -1; }

            string Models = "";
            foreach (ListItem i in chkEditModels.Items)
            {
                if (i.Selected == true)
                {
                    if (Models.Length > 0) { Models += ","; }
                    Models += i.Value;
                }
            }
            MasterPartManager mpm = new MasterPartManager(User.Identity.Name);
            using (clsLinqDataContext ctx = mpm.GetDataContext(User.Identity.Name))
            {
                mpm.RestModelList(ctx, Models, ID);
            }
            pnlEditModels.Visible = false;
            pnlMainGridPN.Visible = true;
            pnlHeader.Visible = true;
            UpdateMainGridPM(MainGridPN.PageIndex);
        }
        void EditModelCancel_Click(object sender, EventArgs e)
        {
            pnlEditModels.Visible = false;
            pnlMainGridPN.Visible = true;
            pnlHeader.Visible = true;
        }



        void btnCancelSave_Click(object sender, EventArgs e)
        {
            ShowPartPanel("NOTNEW");
        }
        void btnAddNew_Click(object sender, EventArgs e)
        {
            ShowPartPanel("NEW");
            //pnlAddNewPart.Visible = true;
            //pnlMainGridPN.Visible = false;
        }
        //void chkModels_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    lblPickedModels.Text = "";
        //    foreach (ListItem x in chkModels.Items)
        //    {
        //        if (x.Selected == true)
        //        {
        //            lblPickedModels.Text += x.Text + " ";
        //        }
        //    }
        //}
        //void drpManufacturer_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    btnAddNew.Visible = true;
        //    if (drpDropPart.SelectedItem.Value == "-1") { btnAddNew.Visible = false; }
        //    LoadModelDistinct();
        //    UpdateMainGridPM();
        //}

        void btnAddPartNumbers_Click(object sender, EventArgs e)
        {
            FillCarrierManufacturerModelKeys();
            MasterPartManager mpm = new MasterPartManager(User.Identity.Name);

            string PartNumber = txtMFGPartNumber.Text;  // MFG Part #
            string GMPPartNumberKey = txtGMPPartNumber.Text;  // GMP Part #
            string GMPPartDescription = txtGMPDesc.Text;  // GMP Part Description
            decimal Classtypeid = -1;

            if (decimal.TryParse(drpAClassType.SelectedValue, out Classtypeid) == false) { Classtypeid = 0; }


            decimal mpid = -1;
            decimal ClientLocationID = -1;
            if (decimal.TryParse(drpLocationList.SelectedItem.Value, out ClientLocationID) == false) { ClientLocationID = -1; }
            if (decimal.TryParse(drpDropPart.SelectedItem.Value, out mpid) == false) { mpid = -1; }


            if (mpm.isPartNumberThere(PartNumber, ClientLocationID) == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "alert('Partnumber already on file, data NOT saved');", true);
                return;
            }
            if (PartNumber.Length > 0)
            {
                mpm.InsertPartNumber(mpid, -1, ClientLocationID, "-1", Manufacturer, Model, PartNumber, GMPPartNumberKey, GMPPartDescription, Classtypeid);
                UpdateMainGridPM();
            }
            txtMFGPartNumber.Text = "";
            txtGMPPartNumber.Text = "";
            txtGMPDesc.Text = "";
            ShowPartPanel("NOTNEW");
        }



        void ShowPartPanel(string P)
        {
            if (P == "NEW")
            {
                pnlAddNewPart.Visible = true;
                pnlMainGridPN.Visible = false;
                pnlHeader.Visible = false;
            }
            else
            {
                pnlAddNewPart.Visible = false;
                pnlMainGridPN.Visible = true;
                pnlHeader.Visible = true;
            }
        }

        string GetDropdownText(decimal Key)
        {
            foreach (ListItem i in drpDropPart.Items) { if (i.Value == Key.ToString()) { return i.Text; } }
            return "";
        }
        void LoadTransactionType(DropDownList drpType)
        {
            List<PartNumberBucketInventoryTransactionType> TypeList = new List<PartNumberBucketInventoryTransactionType>();
            if (TypeList.Count == 0)
            {
                MasterPartManager mpm = new MasterPartManager(User.Identity.Name);
                TypeList = mpm.GetMasterPartTransType();
            }
            drpType.Items.Clear();
            foreach (PartNumberBucketInventoryTransactionType o in TypeList)
            {
                ListItem li = new ListItem(o.Type, o.PartNumberBucketInventorysourceTypeID.ToString());
                if (o.Type.ToUpper() == "REPLENISH") { li.Selected = true; }        // set the default type.
                if (o.Type.ToUpper() != "USED BY TECH" && o.Type.ToUpper() != "REPLENISH" && o.Type.ToUpper() != "TRANSFER IN" && o.Type.ToUpper() != "TRANSFER OUT") { drpType.Items.Add(li); }
            }
        }
        void LoadTransactionType_Restricted(DropDownList drpType)
        {
            List<PartNumberBucketInventoryTransactionType> TypeList = new List<PartNumberBucketInventoryTransactionType>();
            if (TypeList.Count == 0)
            {
                MasterPartManager mpm = new MasterPartManager(User.Identity.Name);
                TypeList = mpm.GetMasterPartTransType();
            }
            drpType.Items.Clear();
            foreach (PartNumberBucketInventoryTransactionType o in TypeList)
            {
                ListItem li = new ListItem(o.Type, o.PartNumberBucketInventorysourceTypeID.ToString());
                if (o.Type.ToUpper() == "REPLENISH") { li.Selected = true; }        // set the default type.
                if (o.Type.ToUpper() != "USED BY TECH" && o.Type.ToUpper() != "TRANSFER IN" && o.Type.ToUpper() != "TRANSFER OUT") { drpType.Items.Add(li); }
            }
        }
        //void LoadTransactionTypeReplensihOnly(DropDownList drpType)
        //{
        //    if (TypeList.Count == 0)
        //    {
        //        MasterPartManager mpm = new MasterPartManager(User.Identity.Name);
        //        TypeList = mpm.GetMasterPartTransType();
        //    }
        //    drpType.Items.Clear();
        //    foreach (PartNumberBucketInventoryTransactionType o in TypeList)
        //    {
        //        if (o.Type.ToUpper() == "REPLENISH")
        //        {
        //            ListItem li = new ListItem(o.Type, o.PartNumberBucketInventorysourceTypeID.ToString());
        //            li.Selected = true;
        //            drpType.Items.Add(li); 
        //        }        // set the default type.
        //    }
        //}

        //void LoadClassType_cList()
        //{
        //    if (cList.Count == 0)
        //    {
        //        MasterPartManager mpm = new MasterPartManager(User.Identity.Name);
        //        cList = mpm.GetMasterPartClassType();
        //    }
        //}
        void LoadClassType(DropDownList drpType)
        {
            //LoadClassType_cList();

            List<MasterPartsClassType> cList = new List<MasterPartsClassType>();
            MasterPartManager mpm = new MasterPartManager(User.Identity.Name);
            cList = mpm.GetMasterPartClassType();

            drpType.Items.Clear();
            foreach (MasterPartsClassType o in cList)
            {
                ListItem li = new ListItem(o.Class, o.MasterPartsClassTypeID.ToString());
                if (o.Class.ToUpper() == "New") { li.Selected = true; }        // set the default type.
                drpType.Items.Add(li);
            }
        }
        void FillCarrierManufacturerModelKeys()
        {
            Manufacturer = drpManufacturer.SelectedItem.Value;
            Model = "";
            foreach (ListItem x in chkModels.Items) { if (x.Selected == true) { Model += x.Value + ","; } }
        }






        void btnRefresh_Click(object sender, EventArgs e)
        {
            UpdateMainGridPM();
        }




        void gvLocationTransferPartsFrom_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            ImageButton btnOpen = (ImageButton)e.CommandSource;
            string CommandArgument = btnOpen.CommandArgument;
            #region IMGDELETE
            if (btnOpen.ID.ToUpper() == "IMGSELECTLOCATION")
            {
                drpLocationTransferPartFrom.ClearSelection();
                drpLocationTransferPartFrom.Items.FindByText(btnOpen.CommandArgument).Selected = true;
            }
            #endregion
        }


        void gvLocationTransferPartsFrom_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GRDPartLocationView mpl = (GRDPartLocationView)e.Row.DataItem;
                ImageButton bLocation = (ImageButton)e.Row.FindControl("imgSelectLocation");
                if (bLocation != null) { bLocation.CommandArgument = "-1"; }
                if (mpl != null)
                {
                    if (bLocation != null) { bLocation.CommandArgument = mpl.IFSLocation; }
                }
            }
        }


        void gvLocationEditParts_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            ImageButton btnOpen = (ImageButton)e.CommandSource;
            string CommandArgument = btnOpen.CommandArgument;
            #region IMGDELETE
            if (btnOpen.ID.ToUpper() == "IMGSELECTLOCATION")
            {
                drpLocationEditParts.ClearSelection();
                drpLocationEditParts.Items.FindByText(btnOpen.CommandArgument).Selected = true;
            }
            #endregion
        }
        void gvLocationEditParts_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GRDPartLocationView mpl = (GRDPartLocationView)e.Row.DataItem;
                ImageButton bLocation = (ImageButton)e.Row.FindControl("imgSelectLocation");
                if (bLocation != null) { bLocation.CommandArgument = "-1"; }
                if (mpl != null)
                {
                    if (bLocation != null) { bLocation.CommandArgument = mpl.IFSLocation; }
                }
            }
        }
        void MainGridPN_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Model = "";
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                MasterPartsLinkTable mpl = (MasterPartsLinkTable)e.Row.DataItem;
                HiddenField PartLinkTableID = (HiddenField)e.Row.FindControl("hdnMasterPartLinkTableIDx");
                HiddenField MasterPartID = (HiddenField)e.Row.FindControl("hdnMasterPartID");

                Label ModelDescription = (Label)e.Row.FindControl("txtModelDescription");

                Label PartDesc = (Label)e.Row.FindControl("lblPartDesc");
                Label lClassType = (Label)e.Row.FindControl("lblClassType");
                
                ImageButton bPrint = (ImageButton)e.Row.FindControl("imgChangeCategory");
                ImageButton bLocation = (ImageButton)e.Row.FindControl("imgViewIFSLocations");
                ImageButton bModel = (ImageButton)e.Row.FindControl("imgChangeModel");
                //ImageButton bEdit = (ImageButton)e.Row.FindControl("imgEditDetail");
                ImageButton bTransfer = (ImageButton)e.Row.FindControl("imgTransfer");
                ImageButton bDelete = (ImageButton)e.Row.FindControl("imgDelete");


                ImageButton bEditDemographics = (ImageButton)e.Row.FindControl("imgEditDemographics");
                ImageButton bAddTransaction = (ImageButton)e.Row.FindControl("imgAddTransaction");
                
                if (bPrint != null) { bPrint.CommandArgument = "-1"; }
                if (bLocation != null) { bLocation.CommandArgument = "-1"; }
                if (bModel != null) { bModel.CommandArgument = "-1"; }
                //if (bEdit != null) { bEdit.CommandArgument = "-1"; }
                if (bTransfer != null) { bTransfer.CommandArgument = "-1"; }
                if (bDelete != null) { bDelete.CommandArgument = "-1"; }

                if (bEditDemographics != null) { bEditDemographics.CommandArgument = "-1"; }
                if (bAddTransaction != null) { bAddTransaction.CommandArgument = "-1"; }

                //bEdit.Visible = false;
                bTransfer.Visible = false;
                if (User.IsInRole(Role_EditAccess) == true)
                {
                    // Leave these off because of ISF, they should not be able to change
                    //  anything unless it is through the normal IFS PO way.
                    //bEdit.Visible = true;
                    bTransfer.Visible = true;
                }

                MasterPartID.Value = mpl.MasterPartsID.ToString();
                PartLinkTableID.Value = "-1";
                if (mpl != null)
                {

                    if (bPrint != null) { bPrint.CommandArgument = mpl.MasterPartsLinkTableID.ToString(); }
                    if (bLocation != null) { bLocation.CommandArgument = mpl.MasterPartsLinkTableID.ToString(); }
                    if (bModel != null) { bModel.CommandArgument = mpl.MasterPartsLinkTableID.ToString(); }
                    //if (bEdit != null) { bEdit.CommandArgument = mpl.MasterPartsLinkTableID.ToString(); }
                    if (bTransfer != null) { bTransfer.CommandArgument = mpl.MasterPartsLinkTableID.ToString(); }
                    if (bDelete != null) { bDelete.CommandArgument = mpl.MasterPartsLinkTableID.ToString(); }

                    if (bEditDemographics != null) { bEditDemographics.CommandArgument = mpl.MasterPartsLinkTableID.ToString(); }
                    if (bAddTransaction != null) { bAddTransaction.CommandArgument = mpl.MasterPartsLinkTableID.ToString(); }

                    ModelDescription.Text = mpl.Model;
                    PartLinkTableID.Value = mpl.MasterPartsLinkTableID.ToString();

                    //LoadClassType_cList();

                    List<MasterPartsClassType> cList = new List<MasterPartsClassType>();
                    MasterPartManager mpm = new MasterPartManager(User.Identity.Name);
                    cList = mpm.GetMasterPartClassType();

                    foreach (MasterPartsClassType o in cList)
                    {
                        if (o.MasterPartsClassTypeID == mpl.MasterPartsClassTypeID) { lClassType.Text = o.Class; break; }
                    }
                    PartDesc.Text = GetDropdownText(mpl.MasterPartsID);
                }
            }
        }
        void MainGridPN_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName != "Page")
            {

                ImageButton btnOpen = (ImageButton)e.CommandSource;
                string CommandArgument = btnOpen.CommandArgument;
                #region IMGDELETE
                if (btnOpen.ID.ToUpper() == "IMGDELETE")
                {
                    decimal ID = -1;
                    if (decimal.TryParse(CommandArgument, out ID) == false) { ID = -1; }
                    MasterPartManager mpm = new MasterPartManager(User.Identity.Name);
                    mpm.DeletePartNumber(ID);
                }
                #endregion
                #region IMGEDITDETAIL
                //if (btnOpen.ID.ToUpper() == "IMGEDITDETAIL")
                //{
                //    decimal ID = -1;
                //    if (decimal.TryParse(CommandArgument, out ID) == false) { ID = -1; }
                //    MasterPartManager mpm = new MasterPartManager(User.Identity.Name);

                //    using (clsLinqDataContext ctx = mpm.GetDataContext(User.Identity.Name))
                //    {
                //        MasterPartsLinkTable pt = ctx.MasterPartsLinkTables.FirstOrDefault(x => x.MasterPartsLinkTableID == ID);
                //        if (pt != null)
                //        {
                //            pnlEditDetail.Visible = true;
                //            pnlMainGridPN.Visible = false;
                //            pnlHeader.Visible = false;
                //            lblPartDescription.Text = pt.PartNumber + ":" + pt.GMPPartNumber + ":" + pt.GMPPartDescription + ": Average Purchase Price:" + pt.AveragePurchasePrice.ToString();
                //            hdnMasterPartLinkTableID.Value = pt.MasterPartsLinkTableID.ToString();
                //            UpdateGridViewDetail(pt.MasterPartsLinkTableID);
                //        }
                //    }
                //}
                #endregion
                #region IMGCHANGECATEGORY
                if (btnOpen.ID.ToUpper() == "IMGVIEWIFSLOCATIONS")
                {
                    decimal ID = -1;
                    if (decimal.TryParse(CommandArgument, out ID) == false) { ID = -1; }

                    MasterPartManager mpm = new MasterPartManager(User.Identity.Name);

                    using (clsLinqDataContext ctx = mpm.GetDataContext(User.Identity.Name))
                    {

                        var pt = from d in ctx.MasterPartsTableIFSLocationStorages
                                 where d.MasterPartsLinkTableID == ID && d.QTY != 0
                                 select new GRDPartLocationView
                            {
                                QTY = d.QTY,
                                Quantity = d.MasterPartsLinkTable.Quantity,
                                GMPPartNumber = d.MasterPartsLinkTable.GMPPartNumber,
                                GMPPartDescription = d.MasterPartsLinkTable.GMPPartDescription,
                                PartNumber = d.MasterPartsLinkTable.PartNumber,
                                Description = d.MasterPartsLinkTable.MasterPart.Description,
                                MasterPartsTableIFSLocationStorageID = d.MasterPartsTableIFSLocationStorageID,
                                MasterPartsLinkTableID = d.MasterPartsLinkTable.MasterPartsLinkTableID,
                                IFSLocation = d.MasterIFSLocation.IFSLocation
                            };

                        pnlLocationView.Visible = true;
                        pnlMainGridPN.Visible = false;
                        pnlHeader.Visible = false;
                        gvLoactions.DataSource = pt;
                        gvLoactions.DataBind();
                    }
                }

                #endregion
                #region IMGCHANGECATEGORY
                if (btnOpen.ID.ToUpper() == "IMGCHANGECATEGORY")
                {
                    decimal ID = -1;
                    if (decimal.TryParse(CommandArgument, out ID) == false) { ID = -1; }
                    MasterPartManager mpm = new MasterPartManager(User.Identity.Name);

                    using (clsLinqDataContext ctx = mpm.GetDataContext(User.Identity.Name))
                    {
                        MasterPartsLinkTable pt = ctx.MasterPartsLinkTables.FirstOrDefault(x => x.MasterPartsLinkTableID == ID);
                        if (pt != null)
                        {
                            pnlChangeCategory.Visible = true;
                            pnlMainGridPN.Visible = false;

                            EditCategoryMFGPart.Text = pt.PartNumber;
                            EditCategoryGMPPart.Text = pt.GMPPartNumber;
                            EditCategoryDesc.Text = pt.GMPPartDescription;
                            hdnMasterPartLinkTableID.Value = pt.MasterPartsLinkTableID.ToString();
                        }
                    }
                }

                #endregion
                #region IMGCHANGEMODEL
                if (btnOpen.ID.ToUpper() == "IMGCHANGEMODEL")
                {
                    decimal ID = -1;
                    if (decimal.TryParse(CommandArgument, out ID) == false) { ID = -1; }
                    MasterPartManager mpm = new MasterPartManager(User.Identity.Name);

                    using (clsLinqDataContext ctx = mpm.GetDataContext(User.Identity.Name))
                    {
                        MasterPartsLinkTable pt = ctx.MasterPartsLinkTables.FirstOrDefault(x => x.MasterPartsLinkTableID == ID);
                        if (pt != null)
                        {
                            pnlEditModels.Visible = true;
                            pnlMainGridPN.Visible = false;
                            EditMFGPart.Text = pt.PartNumber;
                            EditGMPPart.Text = pt.GMPPartNumber;
                            EditModelDesc.Text = pt.GMPPartDescription;
                            hdnMasterPartLinkTableID.Value = pt.MasterPartsLinkTableID.ToString();
                            foreach (ListItem x in chkEditModels.Items)
                            {
                                decimal xID = -1;
                                if (decimal.TryParse(x.Value, out xID) == false) { xID = -1; }
                                if (pt.MasterPartsLinkTableModelLists.Any(y => y.ModelID == xID) == true)
                                { x.Selected = true; }
                                else { x.Selected = false; }
                            }
                        }
                    }
                }
                #endregion
                #region IMGTRANSFER
                if (btnOpen.ID.ToUpper() == "IMGTRANSFER")
                {
                    decimal ID = -1;
                    if (decimal.TryParse(CommandArgument, out ID) == false) { ID = -1; }
                    MasterPartManager mpm = new MasterPartManager(User.Identity.Name);

                    using (clsLinqDataContext ctx = mpm.GetDataContext(User.Identity.Name))
                    {
                        MasterPartsLinkTable pt = ctx.MasterPartsLinkTables.FirstOrDefault(x => x.MasterPartsLinkTableID == ID);
                        if (pt != null)
                        {
                            pnlTransfer.Visible = true;
                            pnlMainGridPN.Visible = false;

                            drpLocationList.Enabled = false;
                            drpDropPart.Enabled = false;
                            drpManufacturer.Enabled = false;
                            btnRefresh.Enabled = false;

                            lblTransferPart.Text = pt.GMPPartDescription + " (" + pt.GMPPartNumber + " / " + pt.PartNumber + ")";
                            lblOriginalWarehouse.Text = "Source Warehouse:" + drpLocationList.SelectedItem.Text;
                            hdnMasterPartLinkTableID.Value = pt.MasterPartsLinkTableID.ToString();
                            var ptx = from d in ctx.MasterPartsTableIFSLocationStorages
                                     where d.MasterPartsLinkTableID == ID && d.QTY != 0
                                     select new GRDPartLocationView
                                     {
                                         QTY = d.QTY,
                                         Quantity = d.MasterPartsLinkTable.Quantity,
                                         GMPPartNumber = d.MasterPartsLinkTable.GMPPartNumber,
                                         GMPPartDescription = d.MasterPartsLinkTable.GMPPartDescription,
                                         PartNumber = d.MasterPartsLinkTable.PartNumber,
                                         Description = d.MasterPartsLinkTable.MasterPart.Description,
                                         MasterPartsTableIFSLocationStorageID = d.MasterPartsTableIFSLocationStorageID,
                                         MasterPartsLinkTableID = d.MasterPartsLinkTable.MasterPartsLinkTableID,
                                         IFSLocation = d.MasterIFSLocation.IFSLocation
                                     };

                            gvLocationTransferPartsFrom.DataSource = ptx;
                            gvLocationTransferPartsFrom.DataBind();




                        }
                    }
                }
                #endregion
                #region IMGEDITDEMOGRAPHICS
                if (btnOpen.ID.ToUpper() == "IMGEDITDEMOGRAPHICS")
                {
                    decimal ID = -1;
                    if (decimal.TryParse(CommandArgument, out ID) == false) { ID = -1; }
                    MasterPartManager mpm = new MasterPartManager(User.Identity.Name);

                    using (clsLinqDataContext ctx = mpm.GetDataContext(User.Identity.Name))
                    {
                        MasterPartsLinkTable pt = ctx.MasterPartsLinkTables.FirstOrDefault(x => x.MasterPartsLinkTableID == ID);
                        if (pt != null)
                        {

                            pnlEditPart.Visible = true;
                            pnlMainGridPN.Visible = false;


                            pnlHeader.Visible = false;
                            drpLocationList.Enabled = false;
                            drpDropPart.Enabled = false;
                            drpManufacturer.Enabled = false;
                            btnRefresh.Enabled = false;

                            txtGMPDesce.Text = pt.GMPPartDescription;
                            txtGMPPartNumbere.Text = pt.GMPPartNumber;
                            txtMFGPartNumbere.Text = pt.PartNumber;
                            txtUnitPrice.Text = pt.UnitPrice.ToString();
                            txtInWarrentyPrice.Text = pt.InWarrentyWorkPrice.ToString();
                            txtInventoryMin.Text = pt.QTYMin.ToString();
                            txtInventoryMax.Text = pt.QTYMax.ToString();
                            txtReorderPoint.Text = pt.QTYReOrder.ToString();
                            hdnMasterPartLinkTableID.Value = pt.MasterPartsLinkTableID.ToString();
                        }
                    }

                }
                #endregion
                #region IMGADDTRANSACTION
                if (btnOpen.ID.ToUpper() == "IMGADDTRANSACTION")
                {
                    decimal ID = -1;
                    if (decimal.TryParse(CommandArgument, out ID) == false) { ID = -1; }
                    MasterPartManager mpm = new MasterPartManager(User.Identity.Name);

                    using (clsLinqDataContext ctx = mpm.GetDataContext(User.Identity.Name))
                    {
                        MasterPartsLinkTable pt = ctx.MasterPartsLinkTables.FirstOrDefault(x => x.MasterPartsLinkTableID == ID);

                        if (pt != null)
                        {
                            pnlAddTransaction.Visible = true;
                            pnlMainGridPN.Visible = false;
                            pnlHeader.Visible = false;
                            drpLocationList.Enabled = false;
                            drpDropPart.Enabled = false;
                            drpManufacturer.Enabled = false;
                            btnRefresh.Enabled = false;
                            lblEditTransDesc.Text = pt.GMPPartDescription;
                            lblGMPDesce.Text = pt.GMPPartDescription;
                            lblGMPPartNumbere.Text = pt.GMPPartNumber;
                            lblMFGPartNumbere.Text = pt.PartNumber;
                            lblQTY.Text = pt.Quantity.ToString();
                            hdnMasterPartLinkTableID.Value = pt.MasterPartsLinkTableID.ToString();


                            var ptx = from d in ctx.MasterPartsTableIFSLocationStorages
                                     where d.MasterPartsLinkTableID == ID && d.QTY != 0
                                     select new GRDPartLocationView
                                     {
                                         QTY = d.QTY,
                                         Quantity = d.MasterPartsLinkTable.Quantity,
                                         GMPPartNumber = d.MasterPartsLinkTable.GMPPartNumber,
                                         GMPPartDescription = d.MasterPartsLinkTable.GMPPartDescription,
                                         PartNumber = d.MasterPartsLinkTable.PartNumber,
                                         Description = d.MasterPartsLinkTable.MasterPart.Description,
                                         MasterPartsTableIFSLocationStorageID = d.MasterPartsTableIFSLocationStorageID,
                                         MasterPartsLinkTableID = d.MasterPartsLinkTable.MasterPartsLinkTableID,
                                         IFSLocation = d.MasterIFSLocation.IFSLocation
                                     };

                            gvLocationEditParts.DataSource = ptx;
                            gvLocationEditParts.DataBind();




                        }
                    }
                }
                #endregion
            }
        }

        void GridViewDetail_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                MasterPartsLinkTablePriceList mpl = (MasterPartsLinkTablePriceList)e.Row.DataItem;
                HiddenField PartLinkTablePriceListID = (HiddenField)e.Row.FindControl("hdnMasterPartsLinkTablePriceListID");


            //    //Type type = e.Row.DataItem.GetType();               //.GetGenericArguments()[0]; 
            //    //GetMasterDetailInventoryList_TemplateRawData_SearchGrid_02Result Data = ((GetMasterDetailInventoryList_TemplateRawData_SearchGrid_02Result)e.Row.DataItem);
                ImageButton bEdit = (ImageButton)e.Row.FindControl("imgEditDetail");
                ImageButton bSave = (ImageButton)e.Row.FindControl("imgEditDetailSave");
                ImageButton bCancel = (ImageButton)e.Row.FindControl("imgEditDetailCancel");

                if (bEdit != null) { bEdit.Visible = User.IsInRole(Role_EditAccess); }
                if (bSave != null) { bSave.Visible = false; }
                if (bCancel != null) { bCancel.Visible = false; }


                TextBox txtQTY = (TextBox)e.Row.FindControl("txtCorrectedQTY");
                TextBox txtDispursed = (TextBox)e.Row.FindControl("txtCorrectedQTYDispursed");
                TextBox txtUnitPurchasePrice = (TextBox)e.Row.FindControl("txtCorrectedUnitPurchasePrice");
                //TextBox txtReason = (TextBox)e.Row.FindControl("txtAdjustmentReason");

                Label lblQty = (Label)e.Row.FindControl("lblQty");
                Label lblQTYDispursed = (Label)e.Row.FindControl("lblQTYDispursed");
                Label lblUnitPurchasePrice = (Label)e.Row.FindControl("lblUnitPurchasePrice");
                Label lblAdjustmentReason = (Label)e.Row.FindControl("lblAdjustmentReason");
                DropDownList drpType = (DropDownList)e.Row.FindControl("drpReason");
                if (lblQty != null) { lblQty.Visible = true; }
                if (lblQTYDispursed != null) { lblQTYDispursed.Visible = true; }
                if (lblUnitPurchasePrice != null) { lblUnitPurchasePrice.Visible = true; }
                if (lblAdjustmentReason != null) { lblAdjustmentReason.Visible = true; }


                HiddenField hdnMasterPartsLinkTablePriceListID = (HiddenField)e.Row.FindControl("hdnMasterPartsLinkTablePriceListID");
                if (txtQTY != null) { txtQTY.Visible = false; }
                if (txtDispursed != null) { txtDispursed.Visible = false; }
                if (txtUnitPurchasePrice != null) { txtUnitPurchasePrice.Visible = false; }

                if (drpType != null)
                {
                    drpType.Visible = false;
                    LoadTransactionType(drpType);
                }
                if (mpl != null)
                {
                    hdnMasterPartsLinkTablePriceListID.Value = mpl.MasterPartsLinkTablePriceListID.ToString();
                    txtQTY.Text = mpl.Quantity.ToString();
                    txtDispursed.Text = mpl.QTYDispursed.ToString();
                    txtUnitPurchasePrice.Text = mpl.UnitPurchasePrice.ToString();

                    lblQty.Text = mpl.Quantity.ToString();
                    lblQTYDispursed.Text = mpl.QTYDispursed.ToString();
                    lblUnitPurchasePrice.Text = mpl.UnitPurchasePrice.ToString();
                }


            }
        }
        void GridViewDetail_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            ImageButton btn = (ImageButton)e.CommandSource;

            #region EditDetail
            if (btn.ID.ToUpper() == "IMGEDITDETAIL")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridViewDetail.Rows[index];

                ImageButton bEdit = (ImageButton)row.FindControl("imgEditDetail");
                ImageButton bSave = (ImageButton)row.FindControl("imgEditDetailSave");
                ImageButton bCancel = (ImageButton)row.FindControl("imgEditDetailCancel");
                if (bEdit != null) { bEdit.Visible = false; }
                if (bSave != null) { bSave.Visible = true; }
                if (bCancel != null) { bCancel.Visible = true; }

                TextBox txtQTY = (TextBox)row.FindControl("txtCorrectedQTY");
                TextBox txtDispursed = (TextBox)row.FindControl("txtCorrectedQTYDispursed");
                TextBox txtUnitPurchasePrice = (TextBox)row.FindControl("txtCorrectedUnitPurchasePrice");
                DropDownList dReason = (DropDownList)row.FindControl("drpReason");
                if (txtQTY != null) { txtQTY.Visible = true; }
                if (txtDispursed != null) { txtDispursed.Visible = true; }
                if (txtUnitPurchasePrice != null) { txtUnitPurchasePrice.Visible = true; }
                if (dReason != null) { dReason.Visible = true; }

                Label lblQty = (Label)row.FindControl("lblQty");
                Label lblQTYDispursed = (Label)row.FindControl("lblQTYDispursed");
                Label lblUnitPurchasePrice = (Label)row.FindControl("lblUnitPurchasePrice");
                Label lblAdjustmentReason = (Label)row.FindControl("lblAdjustmentReason");
                if (lblQty != null) { lblQty.Visible = false; }
                if (lblQTYDispursed != null) { lblQTYDispursed.Visible = false; }
                if (lblUnitPurchasePrice != null) { lblUnitPurchasePrice.Visible = false; }
                if (lblAdjustmentReason != null) { lblAdjustmentReason.Visible = false; }

                btnEditDetailSave.Visible = false;
                btnEditDetailCancel.Visible = false;
            }
            #endregion
            #region Save
            if (btn.ID.ToUpper() == "IMGEDITDETAILSAVE")
            {
                //int index = Convert.ToInt32(e.CommandArgument);
                //GridViewRow row = GridViewDetail.Rows[index];

                ////MasterPartsLinkTablePriceList mpl = (MasterPartsLinkTablePriceList)row.DataItem;
                //HiddenField hdnPartLinkTablePriceListID = (HiddenField)row.FindControl("hdnMasterPartsLinkTablePriceListID");
                //ImageButton bEdit = (ImageButton)row.FindControl("imgEditDetail");
                //ImageButton bSave = (ImageButton)row.FindControl("imgEditDetailSave");
                //ImageButton bCancel = (ImageButton)row.FindControl("imgEditDetailCancel");
                //if (bEdit != null) { bEdit.Visible = User.IsInRole(Role_EditAccess); }
                //if (bSave != null) { bSave.Visible = false; }
                //if (bCancel != null) { bCancel.Visible = false; }
                //DropDownList dReason = (DropDownList)row.FindControl("drpReason");

                //TextBox txtQTY = (TextBox)row.FindControl("txtCorrectedQTY");
                //TextBox txtDispursed = (TextBox)row.FindControl("txtCorrectedQTYDispursed");
                //TextBox txtUnitPurchasePrice = (TextBox)row.FindControl("txtCorrectedUnitPurchasePrice");
                ////TextBox txtReason = (TextBox)row.FindControl("txtAdjustmentReason");
                //if (txtQTY != null) { txtQTY.Visible = false; }
                //if (txtDispursed != null) { txtDispursed.Visible = false; }
                //if (txtUnitPurchasePrice != null) { txtUnitPurchasePrice.Visible = false; }
                //if (dReason != null) { dReason.Visible = false; }

                //Label lblQty = (Label)row.FindControl("lblQty");
                //Label lblQTYDispursed = (Label)row.FindControl("lblQTYDispursed");
                //Label lblUnitPurchasePrice = (Label)row.FindControl("lblUnitPurchasePrice");
                //Label lblAdjustmentReason = (Label)row.FindControl("lblAdjustmentReason");
                //if (lblQty != null) { lblQty.Visible = true; }
                //if (lblQTYDispursed != null) { lblQTYDispursed.Visible = true; }
                //if (lblUnitPurchasePrice != null) { lblUnitPurchasePrice.Visible = true; }
                //if (lblAdjustmentReason != null) { lblAdjustmentReason.Visible = true; }

                //decimal PartLinkTablePriceListID = -1;
                //decimal Qty = 0;
                //decimal Dispursed = 0;
                //decimal UnitPurchasePrice = 0;
                ////decimal oQty = 0;
                ////decimal oDispursed = 0;
                ////decimal oUnitPurchasePrice = 0;


                ////decimal dQty = 0;
                ////decimal dDispursed = 0;
                ////decimal dUnitPurchasePrice = 0;
                //if (hdnPartLinkTablePriceListID != null) { if (decimal.TryParse(hdnPartLinkTablePriceListID.Value, out PartLinkTablePriceListID) == false) { PartLinkTablePriceListID = -1; } }
                //if (txtQTY != null) { if (decimal.TryParse(txtQTY.Text, out Qty) == false) { Qty = 0; } }
                //if (txtDispursed != null) { if (decimal.TryParse(txtDispursed.Text, out Dispursed) == false) { Dispursed = 0; } }
                //if (txtUnitPurchasePrice != null) { if (decimal.TryParse(txtUnitPurchasePrice.Text, out UnitPurchasePrice) == false) { UnitPurchasePrice = 0; } }

                ////if (lblQty != null) { if (decimal.TryParse(lblQty.Text, out oQty) == false) { oQty = 0; } }
                ////if (lblQTYDispursed != null) { if (decimal.TryParse(lblQTYDispursed.Text, out oDispursed) == false) { oDispursed = 0; } }
                ////if (lblUnitPurchasePrice != null) { if (decimal.TryParse(lblUnitPurchasePrice.Text, out oUnitPurchasePrice) == false) { oUnitPurchasePrice = 0; } }

                ////dQty = Qty - oQty;
                ////dDispursed = Dispursed - oDispursed;
                ////dUnitPurchasePrice = UnitPurchasePrice;

                //MasterPartManager mpm = new MasterPartManager(User.Identity.Name);
                //mpm.AddToInventoryEditDetailsOnly(PartLinkTablePriceListID, Qty, Dispursed, UnitPurchasePrice, dReason.SelectedItem.Text);

                //if (txtQTY != null && lblQty != null) { lblQty.Text = txtQTY.Text; }
                //if (txtDispursed != null && lblQTYDispursed != null) { lblQTYDispursed.Text = txtDispursed.Text; }
                //if (txtUnitPurchasePrice != null && lblUnitPurchasePrice != null) { lblUnitPurchasePrice.Text = txtUnitPurchasePrice.Text; }
                //if (dReason != null && lblAdjustmentReason != null) { lblAdjustmentReason.Text = dReason.SelectedItem.Text; }

                //btnEditDetailSave.Visible = true;
                //btnEditDetailCancel.Visible = true;


            }
            #endregion
            #region Cancel
            if (btn.ID.ToUpper() == "IMGEDITDETAILCANCEL")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridViewDetail.Rows[index];

                ImageButton bEdit = (ImageButton)row.FindControl("imgEditDetail");
                ImageButton bSave = (ImageButton)row.FindControl("imgEditDetailSave");
                ImageButton bCancel = (ImageButton)row.FindControl("imgEditDetailCancel");
                if (bEdit != null) { bEdit.Visible = User.IsInRole(Role_EditAccess); }
                if (bSave != null) { bSave.Visible = false; }
                if (bCancel != null) { bCancel.Visible = false; }

                TextBox txtQTY = (TextBox)row.FindControl("txtCorrectedQTY");
                TextBox txtDispursed = (TextBox)row.FindControl("txtCorrectedQTYDispursed");
                TextBox txtUnitPurchasePrice = (TextBox)row.FindControl("txtCorrectedUnitPurchasePrice");
                DropDownList dReason = (DropDownList)row.FindControl("drpReason");
                if (txtQTY != null) { txtQTY.Visible = false; }
                if (txtDispursed != null) { txtDispursed.Visible = false; }
                if (txtUnitPurchasePrice != null) { txtUnitPurchasePrice.Visible = false; }
                if (dReason != null) { dReason.Visible = false; }


                Label lblQty = (Label)row.FindControl("lblQty");
                Label lblQTYDispursed = (Label)row.FindControl("lblQTYDispursed");
                Label lblUnitPurchasePrice = (Label)row.FindControl("lblUnitPurchasePrice");
                Label lblAdjustmentReason = (Label)row.FindControl("lblAdjustmentReason");

                if (lblQty != null) { lblQty.Visible = true; }
                if (lblQTYDispursed != null) { lblQTYDispursed.Visible = true; }
                if (lblUnitPurchasePrice != null) { lblUnitPurchasePrice.Visible = true; }
                if (lblAdjustmentReason != null) { lblAdjustmentReason.Visible = true; }

                if (txtQTY != null && lblQty != null) { txtQTY.Text = lblQty.Text; }
                if (txtDispursed != null && lblQTYDispursed != null) { txtDispursed.Text = lblQTYDispursed.Text; }
                if (txtUnitPurchasePrice != null && lblUnitPurchasePrice != null) { txtUnitPurchasePrice.Text = lblUnitPurchasePrice.Text; }

                btnEditDetailSave.Visible = true;
                btnEditDetailCancel.Visible = true;
            }
            #endregion
        }

        //protected void OnPaging(object sender, GridViewPageEventArgs e)
        //{
        //    MainGridPN.PageIndex = e.NewPageIndex;
        //    UpdateMainGridPM(MainGridPN.PageIndex);
        //    //MainGridPN.DataBind();
        //}
        protected void UpdateMainGridPM()
        {
            UpdateMainGridPM(-1);
        }
        protected void UpdateMainGridPM(int Page)
        {
            FillCarrierManufacturerModelKeys();
            // Get the list of parts that match this condition.
            MasterPartManager mpm = new MasterPartManager(User.Identity.Name);
            //mParts = mpm.GetMasterPartNumbersThisPart(decimal.Parse(drpDropPart.SelectedItem.Value), -1, decimal.Parse(drpLocationList.SelectedItem.Value), "-1", drpManufacturer.SelectedItem.Value, Model, Page, MainGridPN.PageSize);
            hdnAllowGridUpdate.Value = "T";
            MainGridPN.DataSource = mpm.GetMasterPartNumbersThisPart(decimal.Parse(drpDropPart.SelectedItem.Value), -1, decimal.Parse(drpLocationList.SelectedItem.Value), "-1", drpManufacturer.SelectedItem.Value, Model, Page, MainGridPN.PageSize);
            MainGridPN.DataBind();
            btnSavePartNumbers.Visible = true;
            btnSavePartNumbers.Enabled = true;
        }
        protected void UpdateGridViewDetail(decimal MasterPartsLinkTableID)
        {
            MasterPartManager mpm = new MasterPartManager(User.Identity.Name);
            //mPartsPrice =
            GridViewDetail.DataSource = mpm.GetMasterPartPriceList(MasterPartsLinkTableID);
            GridViewDetail.DataBind();
        }

        protected void LoadDropDowns()
        {
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                LoadDropDowns(ctx);
            }
        }
        protected void LoadDropDowns(clsLinqDataContext ctx)
        {
            QuestionManager qm = new QuestionManager(User.Identity.Name);
            List<Option> LO = new List<Option>();

            IFS_InventoryPartsManager ipm = new IFS_InventoryPartsManager(User.Identity.Name);
            List<MasterIFSLocation> loc = ipm.GetLocationList();
            drpLocationEditParts.Items.Clear();
            drpLocationTransferPartFrom.Items.Clear();
            drpLocationTransferPartTo.Items.Clear();
            foreach (MasterIFSLocation cl in loc.OrderBy(x => x.IFSLocation))
            {
                ListItem li = new ListItem(cl.IFSLocation, cl.MasterIFSLocationID.ToString());
                drpLocationEditParts.Items.Add(li);
                ListItem li1 = new ListItem(cl.IFSLocation, cl.MasterIFSLocationID.ToString());
                drpLocationTransferPartFrom.Items.Add(li1);
                ListItem li2 = new ListItem(cl.IFSLocation, cl.MasterIFSLocationID.ToString());
                drpLocationTransferPartTo.Items.Add(li2);
            }


            ClientManager cm = new ClientManager(User.Identity.Name);
            List<ClientLocation> cls = cm.GetClientLocationsWithOnSiteInventory();

            drpLocationList2.Items.Clear();
            drpLocationList2.Items.Add(new ListItem("GMP WHS", "-1"));

            drpLocationList.Items.Clear();
            drpLocationList.Items.Add(new ListItem("GMP WHS", "-1"));

            foreach (ClientLocation cl in cls)
            {
                ListItem li = new ListItem(cl.CompanyName, cl.ClientLocationID.ToString());
                drpLocationList.Items.Add(li);
                ListItem li2 = new ListItem(cl.CompanyName, cl.ClientLocationID.ToString());
                drpLocationList2.Items.Add(li2);
            }
            drpLocationList.SelectedIndex = 0;
            drpLocationList2.SelectedIndex = 0;





            LO = qm.GetQuestionOptionList(ctx, "Manufacturer");
            drpManufacturer.Items.Clear();
            drpManufacturer.Items.Add(new ListItem("<None>", "-1"));
            foreach (Option o in LO)
            {
                ListItem li = new ListItem("(" + o.ScanKey + ") " + o.OptionText, o.OptionID.ToString());
                drpManufacturer.Items.Add(li);
            }
            drpManufacturer.SelectedIndex = 0;



            MasterPartManager mpm = new MasterPartManager(User.Identity.Name);
            List<MasterPart> Parts = mpm.GetMasterParts(ctx);
            drpDropPart.Items.Clear();
            drpChangeCategoryPart.Items.Clear();
            foreach (MasterPart o in Parts)
            {
                ListItem li = new ListItem(o.Description, o.MasterPartsID.ToString());
                drpDropPart.Items.Add(li);
                ListItem l1 = new ListItem(o.Description, o.MasterPartsID.ToString());
                drpChangeCategoryPart.Items.Add(l1);
            }
            drpDropPart.Items.Add(new ListItem("<All>", "-1"));
            drpDropPart.SelectedIndex = 0;
            drpChangeCategoryPart.Items.Add(new ListItem("<All>", "-1"));
            drpChangeCategoryPart.SelectedIndex = 0;


            LO = qm.GetQuestionOptionList(ctx, "Model");
            chkModels.Items.Clear();
            chkModels.Items.Add(new ListItem("None", "-1"));
            chkEditModels.Items.Clear();
            chkEditModels.Items.Add(new ListItem("None", "-1"));
            foreach (Option o in LO)
            {
                ListItem li = new ListItem(o.OptionText, o.OptionID.ToString());
                ListItem l1 = new ListItem(o.OptionText, o.OptionID.ToString());
                chkModels.Items.Add(li);
                chkEditModels.Items.Add(l1);
            }
            chkModels.SelectedIndex = 0;
            chkEditModels.SelectedIndex = 0;
        }
        protected void LoadModelDistinct()
        {
            QuestionManager qm = new QuestionManager(User.Identity.Name);
            decimal ID = -1;
            if (drpManufacturer.SelectedItem.Value == "-1")
            {
                List<Option> LO = new List<Option>();
                LO = qm.GetQuestionOptionList("Model");
                chkModels.Items.Clear();
                chkModels.Items.Add(new ListItem("None", "-1"));
                foreach (Option o in LO)
                {
                    ListItem li = new ListItem(o.OptionText, o.OptionID.ToString());
                    chkModels.Items.Add(li);
                }
                chkModels.SelectedIndex = 0;
            }
            else
            {
                AnswerManager am = new AnswerManager(User.Identity.Name);
                MasterCarrierManufacturerModelColourManager MM = new MasterCarrierManufacturerModelColourManager(User.Identity.Name);
                List<MasterCarrierManufacturerLookup> ml = MM.GetMasterModelList_NoCarrier("-1", drpManufacturer.SelectedItem.Value);
                chkModels.Items.Clear();
                chkModels.Items.Add(new ListItem("None", "-1"));
                foreach (MasterCarrierManufacturerLookup mm in ml)
                {
                    if (mm.OptionModelID != null) { ID = (decimal)mm.OptionModelID; }
                    var o = am.GetAnswer(ID);
                    if (o != null)
                    {
                        ListItem li = new ListItem("(" + o.ScanKey + ") " + o.OptionText, o.OptionID.ToString());
                        chkModels.Items.Add(li);
                    }
                }
                chkModels.SelectedIndex = 0;
            }
        }


        protected void UpdateMainGrid()
        {
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                UpdateMainGrid(ctx);
            }
        }
        protected void UpdateMainGrid(clsLinqDataContext ctx)
        {
            MasterPartManager mpm = new MasterPartManager(User.Identity.Name);
            MainGrid.DataSource = mpm.GetMasterParts(ctx);
            MainGrid.DataBind();
        }
        protected void MainGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (MainGrid.SelectedIndex >= 0)
            {
                decimal KeyID = decimal.Parse(MainGrid.SelectedValue.ToString());
                MasterPartManager mpm = new MasterPartManager(User.Identity.Name);
                MasterPart mp = mpm.GetMasterParts(KeyID);

                if (mp != null)
                {
                    EditKeyID.Text = mp.MasterPartsID.ToString();
                    EditName.Text = mp.Name;
                    EditDesc.Text = mp.Description;
                }

                btnEdit.Visible = true;
                btnDelete.Visible = true;

            }
            else
            {
                btnEdit.Visible = false;
                btnDelete.Visible = false;
            }
        }

        #region QuesionDetail
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            AddName.Text = "";
            AddDesc.Text = "";
            pnlMainView.Visible = false;
            pnlAdd.Visible = true;

        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            pnlMainView.Visible = false;
            pnlEdit.Visible = true;
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            // Delete the answers.
            if (MainGrid.SelectedIndex >= 0)
            {
                decimal KeyID = decimal.Parse(MainGrid.SelectedValue.ToString());
                MasterPartManager mpm = new MasterPartManager(User.Identity.Name);
                if (mpm.Delete(KeyID) == true)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "RestHeader", "alert('Part Deleted');", true);
                    UpdateMainGrid();
                    return;
                }

                ScriptManager.RegisterStartupScript(this, GetType(), "RestHeader", "alert('Error, Part NOT Deleted');", true);
                return;

            }
        }
        protected void EditOK_Click(object sender, EventArgs e)
        {
            if (EditName.Text.Length == 0 || EditDesc.Text.Length == 0) { return; }
            decimal KeyID = decimal.Parse(EditKeyID.Text);

            MasterPartManager mpm = new MasterPartManager(User.Identity.Name);
            MasterPart mp = new MasterPart();
            mp.Name = EditName.Text;
            mp.Description = EditDesc.Text;
            mp.MasterPartsID = KeyID;
            mpm.Update(mp);
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

        protected void AddOK_Click(object sender, EventArgs e)
        {
            if (AddName.Text.Length == 0 || AddDesc.Text.Length == 0) { return; }

            MasterPartManager mpm = new MasterPartManager(User.Identity.Name);
            MasterPart mp = new MasterPart();
            mp.Name = AddName.Text;
            mp.Description = AddDesc.Text;
            mpm.Insert(mp);
            UpdateMainGrid();
            pnlMainView.Visible = true;
            pnlAdd.Visible = false;
        }
        protected void AddCancel_Click1(object sender, EventArgs e)
        {
            pnlMainView.Visible = true;
            pnlAdd.Visible = false;
        }

        #region Upload
        void btnDownload_Click(object sender, EventArgs e)
        {
            ExportPartDetail("MasterPartsUploadTemplate.xls");
            //decimal KeyID = decimal.Parse(MainGrid.SelectedValue.ToString());
            //ExportClientLocationToExcel(KeyID);
        }
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            string TempDirectory = System.Configuration.ConfigurationManager.AppSettings["TempDirectory"];
            if (TempDirectory == null || TempDirectory.Length == 0)
            {
                TempDirectory = "~/IDAutomation";
            }
            UploadFile(TempDirectory, FileUploadXLS, lblMsgDetail, true);
        }

        #region UploadFile
        private void UploadFile(string PathName, FileUpload UploadTool, Label Message, bool AddMissingCategories)
        {
            //Message.Text = "Not Implemented Yet!";
            //Message.ForeColor = System.Drawing.Color.Red;
            //Message.Visible = true;
            //return;

            if (UploadTool.HasFile)
            {
                string strFileName = UploadTool.FileName + "_" + DateTime.Now.ToString("ddMMyyyy_HHmmss");
                string strFileType = System.IO.Path.GetExtension(UploadTool.FileName).ToString().ToLower();
                //Check file type
                if (strFileType == ".xls" || strFileType == ".xlsx")
                {
                    UploadTool.SaveAs(Server.MapPath(PathName + "/" + strFileName + strFileType));
                    if (rdUploadType.SelectedItem.Text == "Transaction")
                    {
                        ImportPartTransaction(Server.MapPath(PathName + "/" + strFileName + strFileType));         //, AddMissingCategories);
                        //Message.Text = "Upload transfer transactions not yet implemented";
                        //Message.ForeColor = System.Drawing.Color.Red;
                        //Message.Visible = true;
                        //return;
                    }
                    else
                    {
                        ImportPartDetail_02(Server.MapPath(PathName + "/" + strFileName + strFileType), AddMissingCategories);
                    }
                    Message.Text = "Data File Uploaded!";
                    Message.ForeColor = System.Drawing.Color.Green;
                    Message.Visible = true;
                    //UpdateChildGrid(ClientID);
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
        public class Modeldata
        {
            public string Model { get; set; }
            public decimal ID { get; set; }

            public Modeldata(string model, decimal id)
            {
                Model = model;
                ID = id;
            }
        }
        public class ExcelUploadData
        {
            public List<ExcelUploadDataDetail> RowData { get; set; }
            public ExcelUploadData()
            {
                RowData = new List<ExcelUploadDataDetail>();
            }
            public void Add(int Row, decimal MasterPartID, decimal MasterIFSLocationID, string Category, string GMP_PartNumber, string GMP_Description, string Make, string Model, string MFG_PartNumber, string sQuantity
                , string sUnitPurchasePrice, string sUnitPrice, string WarningMessage, string RecordMessage, decimal QTY
                , decimal UnitPurchasePrice, decimal UnitPrice, List<Modeldata> MasterModelData, bool HasError, decimal ClientLocationID, decimal inwarrentyPrice, string Class, decimal ClassID)
            {
                // Look to see if we already have this partnumber
                ExcelUploadDataDetail part = RowData.FirstOrDefault(x => x.GMP_PartNumber.ToUpper() == GMP_PartNumber.ToUpper() && x.ClientLocationID == ClientLocationID);
                if (part != null)
                {
                    // look at the other attributes and make sure they are the same.
                    if (part.Category != Category)
                    {
                        part.WarningMessage += "Category not consistent:" + part.Category + "/" + Category + ",";
                        part.HasErrors = true;
                    }
                    if (part.GMP_Description != GMP_Description)
                    {
                        part.WarningMessage += "IMM Descrption not consistent:" + part.GMP_Description + "/" + GMP_Description + ",";
                    }
                    if (part.MFG_Partnumber != MFG_PartNumber)
                    {
                        part.WarningMessage += "MFG_Partnumber not consistent:" + part.MFG_Partnumber + "/" + MFG_PartNumber + ",";
                        part.HasErrors = true;
                    }
                    if (part.Make != Make)
                    {
                        part.WarningMessage += "Make not consistent:" + part.Make + "/" + Make + ",";
                        part.HasErrors = true;
                    }
                    part.rowNumber.Add(Row);
                    if (MasterModelData.Count == 0)
                    {
                        part.MasterModelData.Add(new Modeldata(Model, -1));
                    }
                    else
                    {
                        foreach (Modeldata d in MasterModelData)
                        {
                            part.MasterModelData.Add(new Modeldata(d.Model, d.ID));
                        }
                    }
                    if (HasError == true)
                    {
                        part.HasErrors = true;
                    }
                }
                if (part == null)
                {
                    part = new ExcelUploadDataDetail();
                    part.MasterPartID = MasterPartID;
                    part.Category = Category;
                    part.Class = Class;
                    part.ClassID = ClassID;
                    part.GMP_Description = GMP_Description;
                    part.GMP_PartNumber = GMP_PartNumber;
                    part.Make = Make;
                    part.MFG_Partnumber = MFG_PartNumber;
                    part.Model = Model;
                    part.QTY = QTY;
                    part.RecordMessage = RecordMessage;
                    part.rowNumber.Add(Row);
                    part.sQuantity = sQuantity;
                    part.sUnitPurchasePrice = sUnitPurchasePrice;
                    part.sUnitPrice = sUnitPrice;
                    part.UnitPurchasePrice = UnitPurchasePrice;
                    part.UnitPrice = UnitPrice;
                    part.InWarrentyPrice = inwarrentyPrice;
                    part.WarningMessage = WarningMessage;
                    part.ClientLocationID = ClientLocationID;
                    if (MasterModelData.Count == 0)
                    {
                        part.MasterModelData.Add(new Modeldata(Model, -1));
                    }
                    else
                    {
                        foreach (Modeldata d in MasterModelData)
                        {
                            part.MasterModelData.Add(new Modeldata(d.Model, d.ID));
                        }
                    }
                    part.HasErrors = HasError;
                    RowData.Add(part);
                }
            }
        }
        public class ExcelUploadDataDetail
        {
            public decimal MasterPartID { get; set; }
            public decimal ClientLocationID { get; set; }
            public List<int> rowNumber { get; set; }
            public string Category { get; set; }
            public string Class { get; set; }
            public decimal ClassID { get; set; }
            public string GMP_PartNumber { get; set; }
            public string GMP_Description { get; set; }
            public string Make { get; set; }
            public string Model { get; set; }
            public string MFG_Partnumber { get; set; }
            public string sQuantity { get; set; }
            public string sUnitPrice { get; set; }
            public string sUnitPurchasePrice { get; set; }

            public string WarningMessage { get; set; }
            public string RecordMessage { get; set; }
            public decimal QTY { get; set; }
            public decimal UnitPrice { get; set; }
            public decimal UnitPurchasePrice { get; set; }
            public decimal InWarrentyPrice { get; set; }
            public decimal LocationID { get; set; }
            public bool HasErrors { get; set; }
            public List<Modeldata> MasterModelData { get; set; }

            public ExcelUploadData Data = new ExcelUploadData();

            public ExcelUploadDataDetail()
            {
                rowNumber = new List<int>();
                MasterModelData = new List<Modeldata>();
                HasErrors = false;
            }

            public ExcelUploadDataDetail ExcelCopyRangeOptions(ExcelUploadDataDetail From)
            {
                ExcelUploadDataDetail x = new ExcelUploadDataDetail();
                x.Category = From.Category;
                x.Class = From.Class;
                x.ClassID = From.ClassID;
                x.GMP_Description = From.GMP_Description;
                x.GMP_PartNumber = From.GMP_PartNumber;
                x.Make = From.Make;
                x.MFG_Partnumber = From.MFG_Partnumber;
                x.Model = From.Model;
                x.QTY = From.QTY;
                x.RecordMessage = From.RecordMessage;
                x.sQuantity = From.sQuantity;
                x.sUnitPrice = From.sUnitPrice;
                x.sUnitPurchasePrice = From.sUnitPurchasePrice;
                x.UnitPrice = From.UnitPrice;
                x.UnitPurchasePrice = From.UnitPurchasePrice;
                x.InWarrentyPrice = From.InWarrentyPrice;
                x.WarningMessage = From.WarningMessage;
                x.MasterPartID = From.MasterPartID;
                x.LocationID = From.LocationID;
                x.ClientLocationID = From.ClientLocationID;
                return x;
            }

        }


        private void ImportPartTransaction(string FileName)                 //, bool AddMissingCategories)
        {
            ExcelEngine excelEngine = new ExcelEngine();
            IApplication application = excelEngine.Excel;
            IWorkbook workbook = application.Workbooks.Open(FileName, ExcelOpenType.Automatic);
            IWorksheet sheet = workbook.Worksheets[0];
            int LocationCol = 1;
            int ClassCol = 2;
            int PartCol = 3;
            int GMPPartNumberCol = 4;
            int MiscDescCol = 14;
            int TransTypeCol = 6;
            int PriceCol = 7;
            int QTYCol = 13;
            int IFSLocationCol = 15;
            int MessageCol = 16;

            string LocationString = "";
            string IFSLocationString = "";
            string ClassString = "";
            string PartString = "";
            string GMPPartNumberString = "";
            string MiscDescString = "";
            string TransTypeString = "";
            string PriceString = "";
            string QTYString = "";

            bool hasError = false;
            string WarningMessage = "";

            decimal IFSLocationID = -1;
            decimal mpid = -1;
            decimal qty = 0;
            decimal typeid = -1;
            decimal UnitPurchasePrice = 0;
            decimal ClientLocationID = -1;
            decimal DefaultClientLocationID = -1;
            if (decimal.TryParse(drpLocationList.SelectedItem.Value, out DefaultClientLocationID) == false) { DefaultClientLocationID = -1; }
            int Row = 1;
            sheet.Range[1, MessageCol].Value = "Status";
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                MasterPartManager mpm = new MasterPartManager(User.Identity.Name);
                #region Read the excell
                while (sheet.Range[Row + 1, GMPPartNumberCol].Text != null && sheet.Range[Row + 1, GMPPartNumberCol].Text.Length > 0)          // Scankey 
                {
                    Row++;
                    LocationString = ""; ;
                    ClassString = "";
                    PartString = "";
                    GMPPartNumberString = "";
                    MiscDescString = "";
                    TransTypeString = "";
                    PriceString = "";
                    QTYString = "";
                    mpid = -1;
                    qty = 0;
                    typeid = -1;
                    UnitPurchasePrice = 0;
                    hasError = false;
                    WarningMessage = "";
                    LocationString = (sheet.Range[Row, LocationCol].Value == null ? "" : sheet.Range[Row, LocationCol].DisplayText);
                    IFSLocationString = (sheet.Range[Row, IFSLocationCol].Value == null ? "" : sheet.Range[Row, IFSLocationCol].DisplayText);
                    ClassString = (sheet.Range[Row, ClassCol].Value == null ? "" : sheet.Range[Row, ClassCol].DisplayText);
                    PartString = (sheet.Range[Row, PartCol].Value == null ? "" : sheet.Range[Row, PartCol].DisplayText);
                    GMPPartNumberString = (sheet.Range[Row, GMPPartNumberCol].Value == null ? "" : sheet.Range[Row, GMPPartNumberCol].DisplayText);
                    MiscDescString = (sheet.Range[Row, MiscDescCol].Value == null ? "" : sheet.Range[Row, MiscDescCol].DisplayText);
                    TransTypeString = (sheet.Range[Row, TransTypeCol].Value == null ? "" : sheet.Range[Row, TransTypeCol].DisplayText);
                    PriceString = (sheet.Range[Row, PriceCol].Value == null ? "" : sheet.Range[Row, PriceCol].DisplayText);
                    QTYString = (sheet.Range[Row, QTYCol].Value == null ? "" : sheet.Range[Row, QTYCol].DisplayText);
                    sheet.Range[Row, MessageCol].Value = "";

                    // Get the location for the part.
                    ClientLocationID = DefaultClientLocationID;
                    if (LocationString.Length != 0)
                    {
                        foreach (ListItem l in drpLocationList.Items)
                        {
                            if (l.Text.ToUpper() == LocationString.ToUpper())
                            {
                                if (decimal.TryParse(l.Value, out ClientLocationID) == false) { ClientLocationID = -1; }
                                break;
                            }
                        }
                    }
                    typeid = -1;
                    if (TransTypeString.Length != 0)
                    {
                        foreach (ListItem l in drpTransType.Items)
                        {
                            if (l.Text.ToUpper() == TransTypeString.ToUpper())
                            {
                                if (decimal.TryParse(l.Value, out typeid) == false) { typeid = -1; }
                                break;
                            }
                        }
                    }
                    qty = 0;
                    if (decimal.TryParse(QTYString, out qty) == false) { qty = 0; }
                    UnitPurchasePrice = 0;
                    if (decimal.TryParse(PriceString, out UnitPurchasePrice) == false) { UnitPurchasePrice = 0; }
                    mpid = -1;
                    MasterPartsLinkTable pnum = ctx.MasterPartsLinkTables.FirstOrDefault(x => x.GMPPartNumber.ToUpper() == GMPPartNumberString.ToUpper() && x.ClientLocationID == ClientLocationID);
                    if (pnum == null)
                    {
                        WarningMessage += "/IMM Partnumber Not Found:" + GMPPartNumberString;
                        hasError = true;
                    }
                    else { mpid = pnum.MasterPartsLinkTableID; }
                    if (typeid < 1)
                    {
                        WarningMessage += "/Transaction Type Not Found:" + TransTypeString;
                        hasError = true;
                    }
                    if (qty < 1)
                    {
                        WarningMessage += "/QTY = " + qty.ToString() + ", skipped";
                        hasError = true;
                    }
                    var ifslocation = ctx.MasterIFSLocations.FirstOrDefault(x => x.IFSLocation == IFSLocationString);
                    if (ifslocation != null) { IFSLocationID = ifslocation.MasterIFSLocationID; }



                    if (IFSLocationID < 1)
                    {
                        WarningMessage += "/Location Not given, invalid (col=15), skipped";
                        hasError = true;
                    }

                    if (hasError == false)
                    {
                        mpm.AddToInventory(mpid, IFSLocationID, qty, UnitPurchasePrice, typeid, MiscDescString, "", "", "", "");
                        WarningMessage += "Updated!";
                    }
                    sheet.Range[Row, MessageCol].Value = WarningMessage;
                }
                #endregion
                workbook.SaveAs("PartNumberTransaction_Uploaded.xls", Page.Response, ExcelDownloadType.Open);
                workbook.Close();
                excelEngine.Dispose();
                UpdateMainGridPM();
            }
        }

        private void ImportPartDetail_02(string FileName, bool AddMissingCategories)
        {
            ExcelEngine excelEngine = new ExcelEngine();
            IApplication application = excelEngine.Excel;
            IWorkbook workbook = application.Workbooks.Open(FileName, ExcelOpenType.Automatic);
            IWorksheet sheet = workbook.Worksheets[0];

            List<string> lGMPPartNumbers = new List<string>();
            List<string> lMFGPartNumbers = new List<string>();

            int CategoryCol = 1;
            int ClassCol = 2;

            int GMP_PartNumberCol = 3;
            int GMP_DescriptionCol = 4;
            int MakeCol = 5;
            int ModelCol = 6;
            int MFG_PartnumberCol = 7;
            int QuantityCol = 8;
            int UnitPurchasePriceCol = 9;
            int UnitPriceCol = 10;
            int InWarrentyPriceCol = 11;
            int ClientLocationCol = 12;
            int LocationCol = 13;
            //int IFSLocationCol = 14;
            int MessageCol = 14;

            // Used just for reporting, when uploading, this column is replaced by the on screen location dropdown.


            string Category = "";
            string Class = "";
            decimal ClassID = -1;
            string GMP_PartNumber = "";
            string GMP_Description = "";
            string Make = "";
            string Model = "";
            string MFG_Partnumber = "";
            string sQuantity = "";
            string sUnitPurchasePrice = "";
            string sUnitPrice = "";
            string sInWarrentyPrice = "";
            string sLocation = "";
            //string sIFSLocation = "";
            string WarningMessage = "";
            string RecordMessage = "";
            decimal QTY = 0;
            decimal UnitPrice = 0;
            decimal UnitPurchasePrice = 0;
            decimal inWarrentyPrice = 0;
            decimal LocationID = -1;
            //decimal IFSLocationID = -1;
            bool hasError = false;
            List<Modeldata> MasterModelData = new List<Modeldata>();

            decimal ClientLocationID = -1;
            if (decimal.TryParse(drpLocationList.SelectedItem.Value, out ClientLocationID) == false) { ClientLocationID = -1; }

            int Row = 1;

            sheet.Range[1, MessageCol].Value = "Status";

            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                MasterPartManager mpm = new MasterPartManager(User.Identity.Name);
                Question Manufacturers = ctx.Questions.FirstOrDefault(x => x.Name.ToUpper() == "MANUFACTURER");
                Question Models = ctx.Questions.FirstOrDefault(x => x.Name.ToUpper() == "MODEL");
                //List<PartNumberBucketInventoryTransactionType> TypeList = ctx.PartNumberBucketInventoryTransactionTypes.ToList();
                List<MasterPart> MasterParts = ctx.MasterParts.ToList();


                ExcelUploadData eData = new ExcelUploadData();
                #region Read the excell
                while (sheet.Range[Row + 1, 1].Text != null && sheet.Range[Row + 1, 1].Text.Length > 0)          // Scankey 
                {
                    Category = "";
                    Class = "";
                    ClassID = -1;
                    GMP_PartNumber = "";
                    GMP_Description = "";
                    Make = "";
                    Model = "";
                    MFG_Partnumber = "";
                    sQuantity = "";
                    sUnitPurchasePrice = "";
                    sUnitPrice = "";
                    sInWarrentyPrice = "";
                    sLocation = "";
                    //sIFSLocation = "";
                    WarningMessage = "";
                    RecordMessage = "";
                    QTY = 0;
                    UnitPurchasePrice = 0;
                    UnitPrice = 0;
                    inWarrentyPrice = 0;
                    LocationID = 0;
                    //IFSLocationID = 0;
                    hasError = false;
                    MasterModelData.Clear();
                    List<string> ModelList = new List<string>();
                    Row++;
                    Category = (sheet.Range[Row, CategoryCol].Value == null ? "" : sheet.Range[Row, CategoryCol].Value);
                    Class = (sheet.Range[Row, ClassCol].Value == null ? "" : sheet.Range[Row, ClassCol].Value);
                    GMP_PartNumber = (sheet.Range[Row, GMP_PartNumberCol].Value == null ? "" : sheet.Range[Row, GMP_PartNumberCol].Value);
                    GMP_Description = (sheet.Range[Row, GMP_DescriptionCol].Value == null ? "" : sheet.Range[Row, GMP_DescriptionCol].Value);
                    Make = (sheet.Range[Row, MakeCol].Value == null ? "" : sheet.Range[Row, MakeCol].Value);
                    Model = (sheet.Range[Row, ModelCol].Value == null ? "" : sheet.Range[Row, ModelCol].Value);
                    MFG_Partnumber = (sheet.Range[Row, MFG_PartnumberCol].Value == null ? "" : sheet.Range[Row, MFG_PartnumberCol].Value);
                    sQuantity = (sheet.Range[Row, QuantityCol].Value == null ? "" : sheet.Range[Row, QuantityCol].Value);
                    sUnitPrice = (sheet.Range[Row, UnitPriceCol].Value == null ? "" : sheet.Range[Row, UnitPriceCol].Value);
                    sUnitPurchasePrice = (sheet.Range[Row, UnitPurchasePriceCol].Value == null ? "" : sheet.Range[Row, UnitPurchasePriceCol].Value);
                    sInWarrentyPrice = (sheet.Range[Row, InWarrentyPriceCol].Value == null ? "" : sheet.Range[Row, InWarrentyPriceCol].Value);
                    sLocation = (sheet.Range[Row, LocationCol].Value == null ? "" : sheet.Range[Row, LocationCol].Value);


                    sheet.Range[Row, MessageCol].Value = "";


                    if (sLocation.Length == 0)
                    {
                        WarningMessage += "No IFS Location Given:";
                        hasError = true;
                    }
                    if (lGMPPartNumbers.Contains(GMP_PartNumber))
                    {
                        WarningMessage += "IMM Partnumber found already inside file:" + GMP_PartNumber;
                        hasError = true;
                    }
                    else { lGMPPartNumbers.Add(GMP_PartNumber); }

                    if (lMFGPartNumbers.Contains(MFG_Partnumber))
                    {
                        WarningMessage += "MFG_Partnumber found already inside file:" + MFG_Partnumber;
                        hasError = true;
                    }
                    else { lMFGPartNumbers.Add(MFG_Partnumber); }

                    MasterIFSLocation loc = ctx.MasterIFSLocations.FirstOrDefault(x => x.IFSLocation == sLocation);
                    if (loc == null)
                    {
                        WarningMessage += "IFS Location Invalid:" + sLocation;
                        hasError = true;
                    }


                    // Look to see if we have the part number already on file
                    MasterPartsLinkTable pnum = ctx.MasterPartsLinkTables.FirstOrDefault(x => x.GMPPartNumber.ToUpper() == GMP_PartNumber.ToUpper() && x.ClientLocationID == ClientLocationID);
                    if (pnum != null)
                    {
                        WarningMessage += "IMM Partnumber already on file:" + GMP_PartNumber;
                        hasError = true;
                        //sheet.Range[Row, MessageCol].Value = WarningMessage;
                        //continue;
                    }
                    // Look to see if we have the part number already on file
                    pnum = ctx.MasterPartsLinkTables.FirstOrDefault(x => x.PartNumber.ToUpper() == MFG_Partnumber.ToUpper() && x.ClientLocationID == ClientLocationID);
                    if (pnum != null)
                    {
                        WarningMessage += "MFG Partnumber already on file:" + GMP_PartNumber;
                        hasError = true;
                        //sheet.Range[Row, MessageCol].Value = WarningMessage;
                        //continue;
                    }

                    MasterPartsClassType classType = ctx.MasterPartsClassTypes.FirstOrDefault(x => x.Class.ToUpper() == Class.ToUpper());
                    if (classType == null)
                    {
                        WarningMessage += "Invalid Class Type:" + Class;
                        hasError = true;
                    }
                    else { ClassID = classType.MasterPartsClassTypeID; }


                    // Check to make sure the Make is Valid
                    //Option make = Manufacturers.Options.FirstOrDefault(x => x.OptionText.Trim().ToUpper() == Make.ToUpper() && x.Question.Name == "Manufacturer");
                    Option make = Manufacturers.Options.FirstOrDefault(x => x.Name.Trim().ToUpper() == Make.ToUpper() && x.Question.Name == "Manufacturer");
                    if (make == null)
                    {
                        WarningMessage += "Invalid Manufacturer:" + Make;
                        hasError = true;
                    }
                    else
                    {
                        Make = make.OptionID.ToString();
                    }
                    // Check for Valid Models.
                    ModelList = Model.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    foreach (string m in ModelList.Where(x => x.Length > 0))
                    {
                        //m = m.Trim();
                        Option o = Models.Options.FirstOrDefault(x => x.OptionText.Trim().ToUpper() == m.Trim().ToUpper() && x.Question.Name == "Model");
                        //Option o = Models.Options.FirstOrDefault(x => x.Name.Trim().ToUpper() == m.Trim().ToUpper() && x.Question.Name == "Model");
                        if (o == null)
                        {
                            WarningMessage += "Model Not found:" + m.Trim() + ",";
                        }
                        else
                        {
                            MasterModelData.Add(new Modeldata(m.Trim(), o.OptionID));
                        }
                    }
                    // check to make sure all models were found, if there are less found than uploaded, we have errors.
                    if (MasterModelData.Count() != ModelList.Count())
                    {
                        hasError = true;
                    }
                    if (decimal.TryParse(sQuantity, out QTY) == false)
                    {
                        QTY = 0;
                        WarningMessage += "QTY set to 0,";
                    }
                    if (decimal.TryParse(sUnitPrice, out UnitPrice) == false)
                    {
                        UnitPrice = 0;
                        WarningMessage += "Unit Price set to 0,";
                    }
                    if (decimal.TryParse(sUnitPurchasePrice, out UnitPurchasePrice) == false)
                    {
                        UnitPurchasePrice = 0;
                        WarningMessage += "Unit Purchase Price set to 0,";
                    }
                    if (decimal.TryParse(sInWarrentyPrice, out inWarrentyPrice) == false)
                    {
                        inWarrentyPrice = 0;
                        WarningMessage += "Warranty Price set to 0,";
                    }
                    // verify the category is a valid category.
                    MasterPart part = MasterParts.FirstOrDefault(x => x.Name.ToUpper() == Category.ToUpper());
                    decimal MasterPartID = -1;
                    if (part == null)
                    {
                        if (AddMissingCategories == true && hasError == false)
                        {
                            MasterPart p = new MasterPart();
                            p.Name = Category;
                            p.CreateUser = User.Identity.Name;
                            p.Description = Category;
                            p.LastUpdateDate = DateTime.Now;
                            p.LastUpdateUser = User.Identity.Name;
                            ctx.MasterParts.InsertOnSubmit(p);
                            ctx.SubmitChanges();
                            MasterParts.Add(p);
                            MasterPartID = p.MasterPartsID;
                            part = p;
                            WarningMessage += "Category added:" + Category + ",";
                        }
                        else
                        {
                            WarningMessage += "Category Not found:" + Category;
                            hasError = true;
                        }
                    }
                    else { MasterPartID = part.MasterPartsID; }
                    eData.Add(Row, MasterPartID, LocationID, Category, GMP_PartNumber, GMP_Description, Make, Model, MFG_Partnumber, sQuantity, sUnitPurchasePrice, sUnitPrice, WarningMessage, RecordMessage, QTY, UnitPurchasePrice, UnitPrice, MasterModelData, hasError, ClientLocationID, inWarrentyPrice, Class, ClassID);
                }
                #endregion
                #region Process Data
                //MasterPartManager mpm = new MasterPartManager(User.Identity.Name);

                foreach (ExcelUploadDataDetail ed in eData.RowData)
                {
                    if (ed.HasErrors == false)
                    {
                        string model = "";
                        foreach (var x in ed.MasterModelData)
                        {
                            model += x.ID.ToString() + ",";
                        }
                        PartNumberBucketInventoryTransactionType type = mpm.GetMasterPartTransType(ctx, "Replenish");
                        mpm.InsertPartNumber(ctx, ed.MasterPartID, -1, ClientLocationID, "-1", ed.Make, model, ed.MFG_Partnumber, ed.GMP_PartNumber, ed.GMP_Description, ed.ClassID);
                        if (type != null)
                        {
                            ed.WarningMessage += " " + mpm.AddToInventory(ctx, ed.MFG_Partnumber, ed.LocationID, ed.QTY, ed.UnitPurchasePrice, ed.UnitPrice, ed.InWarrentyPrice, type.PartNumberBucketInventorysourceTypeID, "Newpart Upload", ClientLocationID, "", "", "", "", "");
                        }
                    }
                    // Put the warning messages on the excel.. ready for output.
                    foreach (int r in ed.rowNumber)
                    {
                        if (ed.WarningMessage.Trim().Length > 0) { sheet.Range[r, MessageCol].Value = ed.WarningMessage.Trim(); }
                    }
                }
                #endregion

                ctx.SubmitChanges();
                //workbook.SaveAs("MasterCMMC_Uploaded.xls", Page.Response, ExcelDownloadType.Open);
                workbook.SaveAs("PartNumber_Uploaded.xls", Page.Response, ExcelDownloadType.Open);
                workbook.Close();
                excelEngine.Dispose();
                UpdateMainGridPM();
            }
        }
        #endregion
        private void ExportPartDetail(string FileName)
        {
            ExcelEngine excelEngine = new ExcelEngine();
            IApplication application = excelEngine.Excel;
            IWorkbook workbook = application.Workbooks.Create(1);
            IWorksheet sheet = workbook.Worksheets[0];
            int Row = 1;
            int Col = 1;
            int StartCol = Col;
            int StartRow = Row;

            //Add Header    
            sheet.Range[Row, 1].Text = "Location";

            sheet.Range[Row, 2].Text = "Class";


            sheet.Range[Row, 3].Text = "Part";
            sheet.Range[Row, 4].Text = "MFG Part Number";
            sheet.Range[Row, 5].Text = "IMM Part Number";
            sheet.Range[Row, 6].Text = "IMM Part Desc";
            sheet.Range[Row, 7].Text = "QTY";
            sheet.Range[Row, 8].Text = "Average Purchase Price";
            sheet.Range[Row, 9].Text = "Price";
            sheet.Range[Row, 10].Text = "Warranty Price";
            sheet.Range[Row, 11].Text = "Min";
            sheet.Range[Row, 12].Text = "Max";
            sheet.Range[Row, 13].Text = "Reorder";
            sheet.Range[Row, 14].Text = "Manufacturer";
            sheet.Range[Row, 15].Text = "Model";





            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                List<MasterPartsLinkTable> parts = null;
                MasterPartManager mpm = new MasterPartManager(User.Identity.Name);
                if (chkRestrict.Checked == true)
                {
                    FillCarrierManufacturerModelKeys();
                    parts = mpm.GetMasterPartNumbersThisPart(decimal.Parse(drpDropPart.SelectedItem.Value), -1, decimal.Parse(drpLocationList.SelectedItem.Value), "-1", Manufacturer, Model, -1, -1);
                }
                else
                {
                    parts = mpm.GetMasterPartNumbersAll();
                }

                if (parts != null)
                {

                    List<ClientLocation> Locations = mpm.GetDataContext(User.Identity.Name).ClientLocations.ToList();
                    List<MasterPart> mParts = mpm.GetDataContext(User.Identity.Name).MasterParts.ToList();
                    QuestionManager qm = new QuestionManager(User.Identity.Name);
                    List<MasterPartsClassType> PartsClassType = (from z in ctx.MasterPartsClassTypes select z).ToList();

                    List<Option> Manufacturers = qm.GetQuestionOptionList(ctx, "Manufacturer");
                    //Question Manufacturers = ctx.Questions.FirstOrDefault(x => x.Name.ToUpper() == "MANUFACTURER");
                    //List<PartNumberBucketInventoryTransactionType> TransType = ctx.PartNumberBucketInventoryTransactionTypes.ToList();
                    //foreach (var l in ctx.GetReport_MasterPartsInventoryTransactions().Where(x => x.Type.ToUpper() != "USED BY TECH").OrderBy(x => x.MFGPartNumber).ThenBy(x => x.Manufacturer).Take(100))
                    foreach (var l in parts.OrderBy(x => x.ClientLocationID).ThenBy(x => x.PartNumber).ThenBy(x => x.Manufacturer))
                    //foreach (MasterPartsLinkTable l in ctx.MasterPartsLinkTables.OrderBy(x => x.MasterPart.Description).ThenBy(x => x.PartNumber))
                    {
                        //foreach (var zz in l.
                        Row++;
                        sheet.Range[Row, 1].Text = "GMP WHS";
                        if (Locations.Any(x => x.ClientLocationID == l.ClientLocationID) == true)
                        {
                            sheet.Range[Row, 1].Text = Locations.FirstOrDefault(x => x.ClientLocationID == l.ClientLocationID).CompanyName;
                        }

                        sheet.Range[Row, 2].Text = "";
                        MasterPartsClassType cl = PartsClassType.FirstOrDefault(x => x.MasterPartsClassTypeID == l.MasterPartsClassTypeID);
                        if (cl != null)
                        {
                            sheet.Range[Row, 2].Text = cl.Class == null ? "" : cl.Class;
                        }
                        sheet.Range[Row, 3].Text = "";
                        if (mParts.Any(x => x.MasterPartsID == l.MasterPartsID) == true)
                        {
                            sheet.Range[Row, 3].Text = mParts.FirstOrDefault(x => x.MasterPartsID == l.MasterPartsID).Name;
                        }
                        sheet.Range[Row, 4].Text = l.PartNumber == null ? "" : l.PartNumber;
                        sheet.Range[Row, 5].Text = l.GMPPartNumber == null ? "" : l.GMPPartNumber;
                        sheet.Range[Row, 6].Text = l.GMPPartDescription == null ? "" : l.GMPPartDescription;


                        sheet.Range[Row, 7].Number = (double)l.Quantity;
                        sheet.Range[Row, 8].Number = l.AveragePurchasePrice == null ? 0 : (double)l.AveragePurchasePrice;
                        sheet.Range[Row, 9].Number = l.UnitPrice == null ? 0 : (double)l.UnitPrice;
                        sheet.Range[Row, 10].Number = l.InWarrentyWorkPrice == null ? 0 : (double)l.InWarrentyWorkPrice;
                        sheet.Range[Row, 11].Number = l.QTYMin == null ? 0 : (double)l.QTYMin;
                        sheet.Range[Row, 12].Number = l.QTYMax == null ? 0 : (double)l.QTYMax;
                        sheet.Range[Row, 13].Number = l.QTYReOrder == null ? 0 : (double)l.QTYReOrder;

                        decimal mid = -1;
                        if (decimal.TryParse(l.Manufacturer, out mid) == false)
                        {
                            sheet.Range[Row, 14].Text = "";
                        }
                        else if (Manufacturers.Any(x => x.OptionID == mid) == true)
                        {

                            sheet.Range[Row, 14].Text = Manufacturers.FirstOrDefault(x => x.OptionID == mid).OptionText;
                        }
                        else
                        {
                            sheet.Range[Row, 14].Text = "";
                        }
                        sheet.Range[Row, 15].Text = l.Model == null ? "" : l.Model;
                    }
                }
            }
            workbook.SaveAs(FileName, Page.Response, ExcelDownloadType.Open);
            workbook.Close();
            excelEngine.Dispose();
        }
        #endregion

    //    #region Paging section 

    //    protected override void InitializePager(GridViewRow row,
    //                                                    int columnSpan,
    //                                                    PagedDataSource pagedDataSource)
    //    {
    //        //if (this.TopPagerRow == null && 
    //        if (this.Controls[0].Controls.Count == 0 &&
    //           (this.PagerSettings.Position == PagerPosition.Top ||
    //            this.PagerSettings.Position == PagerPosition.TopAndBottom))
    //        {
    //            InitializeTopPager(row, columnSpan, pagedDataSource);
    //        }
    //        else
    //        {
    //            base.InitializePager(row, columnSpan, pagedDataSource);
    //            InitializeBottomPager(row, columnSpan, pagedDataSource);
    //        }
    //    }


    //    protected virtual void InitializeTopPager(GridViewRow row,
    //int columnSpan,
    //PagedDataSource pagedDataSource)
    //    {
    //        TableCell cell = new TableCell();
    //        if (columnSpan > 1)
    //        {
    //            cell.ColumnSpan = columnSpan;
    //        }
    //        Literal ltrlSpan = new Literal();
    //        ltrlSpan.Text = "<span style='float:left'> " +
    //            pagedDataSource.DataSourceCount.ToString() +
    //            " record(s) found.</span>";
    //        cell.Controls.Add(ltrlSpan);
    //        row.Cells.Add(cell);
    //    }

    //    protected virtual void InitializeBottomPager(GridViewRow row,
    //int columnSpan,
    //PagedDataSource pagedDataSource)
    //    {
    //        TableCell goToCell = new TableCell();
    //        goToCell.Style.Add(HtmlTextWriterStyle.Width, "100%");

    //        Table pagerTable = (Table)row.Cells[0].Controls[0];
    //        pagerTable.Rows[0].Cells.Add(goToCell);

    //        Literal ltrlSpanBegin = new Literal();
    //        ltrlSpanBegin.Text = "<span style='float:right'>Page ";

    //        if (m_txtPageNo == null)
    //        {
    //            m_txtPageNo = new TextBox();
    //            m_txtPageNo.Width = new Unit(20);
    //            m_txtPageNo.Style.Add("height", "10px");
    //            m_txtPageNo.Font.Size = new FontUnit("10px");
    //            m_txtPageNo.CssClass = this.PagerStyle.CssClass;
    //        }

    //        Literal ltrlText = new Literal();
    //        ltrlText.Text = " of " + PageCount.ToString();

    //        Button btnGo = new Button();
    //        btnGo.Text = "Go";
    //        btnGo.CommandName = "Page1";
    //        btnGo.CommandArgument = "2";
    //        btnGo.ID = "ctl_PageIndex";
    //        btnGo.Height = new Unit("16px");
    //        btnGo.Font.Size = new FontUnit("10px");
    //        btnGo.CssClass = this.PagerStyle.CssClass;
    //        if (this.PagerStyle.ForeColor != null)
    //        {
    //            btnGo.Style.Add(HtmlTextWriterStyle.Color,
    //                this.PagerStyle.ForeColor.ToString());
    //        }

    //        Literal ltrlSpanEnd = new Literal();
    //        ltrlSpanEnd.Text = "</span>";

    //        goToCell.Controls.Add(ltrlSpanBegin);
    //        goToCell.Controls.Add(m_txtPageNo);
    //        goToCell.Controls.Add(ltrlText);
    //        goToCell.Controls.Add(btnGo);
    //        goToCell.Controls.Add(ltrlSpanEnd);
    //    }


    //    protected override void OnRowCommand(GridViewCommandEventArgs e)
    //    {
    //        switch (e.CommandName)
    //        {
    //            case "Page1":
    //                HandlePageCommand(e);
    //                break;
    //            default:
    //                base.OnRowCommand(e);
    //                break;
    //        }
    //    }


    //    protected virtual void HandlePageCommand(GridViewCommandEventArgs e)
    //    {
    //        TextBox txtPageIndex;
    //        txtPageIndex =
    //          (TextBox)((System.Web.UI.Control)e.CommandSource).Parent.Controls[1];
    //        Button btnPageIndex =
    //          (Button)((System.Web.UI.Control)e.CommandSource).Parent.Controls[3];
    //        if (txtPageIndex.Text.Length > 0)
    //        {
    //            try
    //            {
    //                int ndx = int.Parse(txtPageIndex.Text);
    //                ndx = ndx - 1;
    //                if (ndx >= PageCount)
    //                    ndx = PageCount - 1;
    //                if (ndx < 0)
    //                    ndx = 0;
    //                this.PageIndex = ndx;
    //                btnPageIndex.CommandArgument = txtPageIndex.Text;
    //            }
    //            catch (Exception e1)
    //            {
    //                if (e1.Message.Length == 0)
    //                    return;
    //            }
    //        }
    //    }








    //    #endregion




    }
}