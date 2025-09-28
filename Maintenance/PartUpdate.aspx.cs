using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using BW_WebApp.DataManagers;

namespace BW_WebApp.Maintenance
{
    public partial class PartUpdate : System.Web.UI.Page
    {

        string Role_EditAccess = "PartsAdmin";
        string Manufacturer = "";
        string Model = "";

        protected void Page_Load(object sender, EventArgs e)
        {

            MainGridPN.RowDataBound += new GridViewRowEventHandler(MainGridPN_RowDataBound);
            MainGridPN.RowCommand += new GridViewCommandEventHandler(MainGridPN_RowCommand);
            GridOpenPO.SelectedIndexChanged += new EventHandler(GridOpenPO_SelectedIndexChanged);
            GridOpenPODetail.SelectedIndexChanged += new EventHandler(GridOpenPODetail_SelectedIndexChanged);
            gvLocationEditParts.RowDataBound += new GridViewRowEventHandler(gvLocationEditParts_RowDataBound);
            gvLocationEditParts.RowCommand += new GridViewCommandEventHandler(gvLocationEditParts_RowCommand);

            gvLocationPOReceiveParts.RowDataBound += new GridViewRowEventHandler(gvLocationPOReceiveParts_RowDataBound);
            gvLocationPOReceiveParts.RowCommand += new GridViewCommandEventHandler(gvLocationPOReceiveParts_RowCommand);

            gvLocationTransferPartsFrom.RowDataBound += new GridViewRowEventHandler(gvLocationTransferPartsFrom_RowDataBound);
            gvLocationTransferPartsFrom.RowCommand += new GridViewCommandEventHandler(gvLocationTransferPartsFrom_RowCommand);
            drpLocationListPOReceive.SelectedIndexChanged += new EventHandler(drpLocationListPOReceive_SelectedIndexChanged);
            //GridViewDetail.RowDataBound += new GridViewRowEventHandler(GridViewDetail_RowDataBound);
            //GridViewDetail.RowCommand += new GridViewCommandEventHandler(GridViewDetail_RowCommand);

            btnRefresh.Click += new EventHandler(btnRefresh_Click);
            btnSearchPartNumber.Click += new EventHandler(btnSearchPartNumber_Click);
            RefreshOpenPOGrid.Click += new EventHandler(RefreshOpenPOGrid_Click);
            //btnEditDetailSave.Click += new EventHandler(btnEditDetailSave_Click);
            //btnEditDetailCancel.Click += new EventHandler(btnEditDetailCancel_Click);
            //EditModelCancel.Click += new EventHandler(EditModelCancel_Click);
            //EditModelSave.Click += new EventHandler(EditModelSave_Click);
            btnTransferSave.Click += new EventHandler(btnTransferSave_Click);
            btnTransferCancel.Click += new EventHandler(btnTransferCancel_Click);
            btnAddTransactionCancel.Click += new EventHandler(btnAddTransactionCancel_Click);
            btnAddTransactionSave.Click += new EventHandler(btnAddTransactionSave_Click);
            drpTransType.SelectedIndexChanged += new EventHandler(drpTransType_SelectedIndexChanged);

            btnLocationViewClose.Click += new EventHandler(btnLocationViewClose_Click);

            EditCategorySave.Click += new EventHandler(EditCategorySave_Click);
            EditCategoryCancel.Click += new EventHandler(EditCategoryCancel_Click);

            btnSavePartNumberse.Click += new EventHandler(btnSavePartNumberse_Click);
            btnCancelSavee.Click += new EventHandler(btnCancelSavee_Click);
            btnPOReceiveSave.Click += new EventHandler(btnPOReceiveSave_Click);
            btnPOReceiveCancel.Click += new EventHandler(btnPOReceiveCancel_Click);
            //btnSavePartNumbers.Click += new EventHandler(btnAddPartNumbers_Click);


            ////////chkModels.SelectedIndexChanged += new EventHandler(chkModels_SelectedIndexChanged);
            ////////drpManufacturer.SelectedIndexChanged += new EventHandler(drpManufacturer_SelectedIndexChanged);
            ////////drpDropPart.SelectedIndexChanged += new EventHandler(drpManufacturer_SelectedIndexChanged);
            ////////drpLocationList.SelectedIndexChanged += new EventHandler(drpManufacturer_SelectedIndexChanged);

            //btnAddNew.Click += new EventHandler(btnAddNew_Click);
            //btnCancelSave.Click += new EventHandler(btnCancelSave_Click);


            //btnResetPartNumbers.Visible = false;
            //if (User.IsInRole("Administrators")) { btnResetPartNumbers.Visible = false; }

            if (!IsPostBack)
            {
                txtPOReceiptDate.Text = String.Format("{0:MM/dd/yyyy}", DateTime.Now);
                //pnlAdd.Visible = false;
                //pnlEdit.Visible = false;
                hdnUserName.Value = User.Identity.Name;
                //hdnManufacturerID.Value = drpManufacturer.ClientID;
                //if (IsPostBack == false)
                //{
                using (clsLinqDataContext ctx = new clsLinqDataContext())
                {
                    LoadDropDowns(ctx);
                    //UpdateMainGrid(ctx);
                    //LoadModelDistinct();
                    //LoadClassType(drpAClassType);
                    LoadClassType(drpClassType);
                    LoadTransactionType_Restricted(drpTransType);
                    //UpdateMainGridPM();
                }
                //}
            }
        }



        void drpLocationListPOReceive_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshOPDetail();
        }



        void btnPOReceiveSave_Click(object sender, EventArgs e)
        {
            if (IFSToLocationPOLineReceive.Text.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "xxxx", "alert('No IFS Location Given.');", true);
                return;
            }
            DateTime poReceiveDate = DateTime.Now;
            decimal mpid = -1;
            decimal qty = 0;
            decimal typeid = -1;
            //decimal Locationid = -1;
            decimal UnitPurchasePrice = 0;

            if (decimal.TryParse(hdnPOReceiveMasterPartLinkTableID.Value, out mpid) == false) { mpid = -1; }
            if (decimal.TryParse(txtPOReceiveQTY.Text, out qty) == false) { qty = 0; }
            if (qty <= 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "xxxx", "alert('QTY must be greater than 0!');", true);
                return;
            }
            //if (decimal.TryParse(drpLocationEditParts.SelectedValue, out Locationid) == false) { Locationid = 0; }

            if (decimal.TryParse(txtPOLineReceiptPrice.Text, out UnitPurchasePrice) == false) { UnitPurchasePrice = -1; }
            MasterPartManager mpm = new MasterPartManager(User.Identity.Name);
            PartNumberBucketInventoryTransactionType ptype = mpm.GetMasterPartTransType("Replenish");
            if (ptype != null) { typeid = ptype.PartNumberBucketInventorysourceTypeID; }
            MasterIFSLocation loc = mpm.GetIFSLocation(IFSToLocationPOLineReceive.Text);
            if (loc == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "xxxx", "alert('IFS Location Not Valid!');", true);
                return;
            }


            if (qty > 0)
            {
                HidePNLPOReceive();
                mpm.AddToInventory(mpid, loc.MasterIFSLocationID, qty, UnitPurchasePrice, typeid, txtPOReceiveDesc.Text, txtPOLineReceiptVendor.Text, txtPOLineReceiptNumber.Text, String.Format("{0:MM/dd/yyyy}", poReceiveDate), txtPOLineReceiptLine.Text);
                txtPOReceiveQTY.Text = "";
                txtPOReceiveDesc.Text = "";
                RefreshOpenPO();
            }
        }

        private void HidePNLPOReceive()
        {
            //pnlPOReceiveGrid.Visible = true;
            //TabPOLines.
            TabContainer2.ActiveTabIndex = TabPOLines.TabIndex + 1;
            //pnlPOReceive.Visible = false;
        }

        void btnPOReceiveCancel_Click(object sender, EventArgs e)
        {
            HidePNLPOReceive();
        }
        private void ShowPNLPOReceive()
        {
            //pnlPOReceiveGrid.Visible = false;
            pnlPOReceive.Visible = true;
        }


        void GridOpenPO_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshOpenPO();
        }
        void GridOpenPODetail_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshOPDetail();
        }


        void RefreshOpenPOGrid_Click(object sender, EventArgs e)
        {
            DeviceInventoryManager im = new DeviceInventoryManager(User.Identity.Name);
            GridOpenPO.DataSource = im.GetOpenPOListParts().OrderBy(x => x.SUPPLIER_NAME).ThenBy(x => x.PONumberOrderNo);              // im.GetOpenPOListParts();
            GridOpenPO.DataBind();
        }
        private void RefreshOpenPO()
        {
            TabPOLineReceive.Visible = false;
            if (GridOpenPO.SelectedIndex >= 0)
            {
                string KeyID = GridOpenPO.SelectedValue.ToString();
                DeviceInventoryManager im = new DeviceInventoryManager(User.Identity.Name);
                var qu = im.GetOpenPODetailLines(KeyID);
                if (qu != null)
                {
                    TabPOLines.HeaderText = "" + KeyID + "";
                    TabPOLines.Visible = true;
                    GridOpenPODetail.DataSource = qu.OrderBy(x => x.POLineLineNo);
                    GridOpenPODetail.DataBind();
                }
            }
            else
            {
                TabPOLines.Visible = false;
            }
        }
        private void RefreshOPDetail()
        {
            if (GridOpenPODetail.SelectedIndex >= 0)
            {
                TabPOLineReceive.Visible = false;
                pnlPOReceive.Visible = true;
                string KeyID = GridOpenPODetail.SelectedValue.ToString();
                decimal IFSPurchaseOrderDetailID = -1;
                decimal WarehouseID = -1;

                if (decimal.TryParse(KeyID, out IFSPurchaseOrderDetailID) == false) { IFSPurchaseOrderDetailID = -1; }
                if (decimal.TryParse(drpLocationListPOReceive.SelectedItem.Value, out WarehouseID) == false) { WarehouseID = -1; }
                using (clsLinqDataContext ctx = new clsLinqDataContext())
                {
                    string SKU = "xxx";
                    txtPOLineReceiptVendor.Text = "";
                    txtPOLineReceiptNumber.Text = "";
                    txtPOLineReceiptLine.Text = "";
                    txtPOLineQTYOrdered.Text = "";
                    txtPOLineQTYRemaining.Text = "";
                    txtPOLineReceiptPrice.Text = "";
                    decimal ID = -1;
                    TabPOLineReceive.HeaderText = "";
                    hdnPOReceiveMasterPartLinkTableID.Value = "-1";
                    IFSPurchaseOrderDetail ifsd = ctx.IFSPurchaseOrderDetails.FirstOrDefault(x => x.IFSPurchaseOrderDetailID == IFSPurchaseOrderDetailID);
                    if (ifsd != null)
                    {
                        SKU = ifsd.SKUPartNo; TabPOLineReceive.HeaderText = ifsd.SKUPartNo;
                        txtPOLineReceiptVendor.Text = ifsd.IFSPurchaseOrderHeader.POVendorSupplierID;
                        txtPOLineReceiptNumber.Text = ifsd.PONumberOrderNo;
                        txtPOLineReceiptLine.Text = ifsd.POLineLineNo;
                        txtPOLineQTYOrdered.Text = ifsd.QTYOrderQty.ToString();
                        txtPOLineQTYRemaining.Text = ifsd.QTYRemainingQty.ToString();
                        txtPOLineReceiptPrice.Text = ifsd.POCostPrice.ToString();

                    }
                    MasterPartsLinkTable part = ctx.MasterPartsLinkTables.FirstOrDefault(x => x.ClientLocationID == WarehouseID && x.GMPPartNumber.ToUpper() == SKU.ToUpper());
                    if (part != null)
                    {
                        hdnPOReceiveMasterPartLinkTableID.Value = part.MasterPartsLinkTableID.ToString();
                        ID = part.MasterPartsLinkTableID;
                        TabPOLineReceive.Visible = true;
                    }
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

                    gvLocationPOReceiveParts.DataSource = ptx;
                    gvLocationPOReceiveParts.DataBind();
                }
            }
        }



        void drpTransType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReadyTransTypeFields();
        }

        private void ReadyTransTypeFields()
        {
            TurnReplenish(false);
            if (drpTransType.SelectedItem != null && drpTransType.SelectedItem.Text.ToUpper() == "REPLENISH NOPO")
            {
                TurnReplenish(true);
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
                //ImageButton bModel = (ImageButton)e.Row.FindControl("imgChangeModel");
                //ImageButton bEdit = (ImageButton)e.Row.FindControl("imgEditDetail");
                ImageButton bTransfer = (ImageButton)e.Row.FindControl("imgTransfer");
                ImageButton bDelete = (ImageButton)e.Row.FindControl("imgDelete");


                ImageButton bEditDemographics = (ImageButton)e.Row.FindControl("imgEditDemographics");
                ImageButton bAddTransaction = (ImageButton)e.Row.FindControl("imgAddTransaction");

                if (bPrint != null) { bPrint.CommandArgument = "-1"; }
                if (bLocation != null) { bLocation.CommandArgument = "-1"; }
                //if (bModel != null) { bModel.CommandArgument = "-1"; }
                //if (bEdit != null) { bEdit.CommandArgument = "-1"; }
                if (bTransfer != null) { bTransfer.CommandArgument = "-1"; }
                if (bDelete != null) { bDelete.CommandArgument = "-1"; }

                if (bEditDemographics != null) { bEditDemographics.CommandArgument = "-1"; }
                if (bAddTransaction != null) { bAddTransaction.CommandArgument = "-1"; }
                if (bAddTransaction != null) { bAddTransaction.Visible = false; }
                if (bAddTransaction != null && drpTransType.Items.Count > 0) { bAddTransaction.Visible = true; }

                //bEdit.Visible = false;
                bTransfer.Visible = false;
                if (User.IsInRole(Role_EditAccess) == true || User.IsInRole("Admin") == true)
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
                    //if (bModel != null) { bModel.CommandArgument = mpl.MasterPartsLinkTableID.ToString(); }
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
                            pnlHeader.Visible = false;
                            CClblMFGPartNumbere.Text = pt.PartNumber;
                            //CClblQTY.Text = pt.Quantity.ToString();
                            CClblGMPPartNumbere.Text = pt.GMPPartNumber;
                            CClblGMPDesce.Text = pt.GMPPartDescription;
                            hdnMasterPartLinkTableID.Value = pt.MasterPartsLinkTableID.ToString();
                        }
                    }
                }

                #endregion
                #region IMGCHANGEMODEL
                //if (btnOpen.ID.ToUpper() == "IMGCHANGEMODEL")
                //{
                //    decimal ID = -1;
                //    if (decimal.TryParse(CommandArgument, out ID) == false) { ID = -1; }
                //    MasterPartManager mpm = new MasterPartManager(User.Identity.Name);

                //    using (clsLinqDataContext ctx = mpm.GetDataContext(User.Identity.Name))
                //    {
                //        MasterPartsLinkTable pt = ctx.MasterPartsLinkTables.FirstOrDefault(x => x.MasterPartsLinkTableID == ID);
                //        if (pt != null)
                //        {
                //            pnlEditModels.Visible = true;
                //            pnlMainGridPN.Visible = false;
                //            EditMFGPart.Text = pt.PartNumber;
                //            EditGMPPart.Text = pt.GMPPartNumber;
                //            EditModelDesc.Text = pt.GMPPartDescription;
                //            hdnMasterPartLinkTableID.Value = pt.MasterPartsLinkTableID.ToString();
                //            foreach (ListItem x in chkEditModels.Items)
                //            {
                //                decimal xID = -1;
                //                if (decimal.TryParse(x.Value, out xID) == false) { xID = -1; }
                //                if (pt.MasterPartsLinkTableModelLists.Any(y => y.ModelID == xID) == true)
                //                { x.Selected = true; }
                //                else { x.Selected = false; }
                //            }
                //        }
                //    }
                //}
                #endregion
                #region IMGTRANSFER
                if (btnOpen.ID.ToUpper() == "IMGTRANSFER")
                {
                    decimal ID = -1;
                    if (decimal.TryParse(CommandArgument, out ID) == false) { ID = -1; }
                    RefreshTransferPart(ID);
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
                            drpManufacturerEdit.Enabled = true;
                            btnRefresh.Enabled = false;
                            drpManufacturerEdit.SelectedIndex = 0;
                            ListItem _ListItem = drpManufacturerEdit.Items.FindByValue(pt.Manufacturer);
                            if (_ListItem != null) { drpManufacturerEdit.SelectedIndex = drpManufacturerEdit.Items.IndexOf(_ListItem); }


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
                            //TurnReplenish(false);
                            pnlAddTransaction.Visible = true;
                            pnlMainGridPN.Visible = false;
                            pnlHeader.Visible = false;
                            drpLocationList.Enabled = false;
                            drpDropPart.Enabled = false;
                            drpManufacturer.Enabled = false;
                            //drpManufacturerEdit.Enabled = false;
                            btnRefresh.Enabled = false;
                            //lblEditTransDesc.Text = pt.GMPPartDescription;
                            lblGMPDesce.Text = pt.GMPPartDescription;
                            lblGMPPartNumbere.Text = pt.GMPPartNumber;
                            lblMFGPartNumbere.Text = pt.PartNumber;
                            //lblQTY.Text = pt.Quantity.ToString();
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

        private void RefreshTransferPart(decimal ID)
        {
            MasterPartManager mpm = new MasterPartManager(User.Identity.Name);
            using (clsLinqDataContext ctx = mpm.GetDataContext(User.Identity.Name))
            {
                MasterPartsLinkTable pt = ctx.MasterPartsLinkTables.FirstOrDefault(x => x.MasterPartsLinkTableID == ID);
                if (pt != null)
                {
                    pnlTransfer.Visible = true;
                    pnlMainGridPN.Visible = false;
                    pnlHeader.Visible = false;
                    drpLocationList.Enabled = false;
                    drpDropPart.Enabled = false;
                    drpManufacturer.Enabled = false;
                    //drpManufacturerEdit.Enabled = false;
                    btnRefresh.Enabled = false;

                    //PTlblEditTransDesc.Text = pt.GMPPartDescription;
                    PTlblGMPDesce.Text = pt.GMPPartDescription;
                    PTlblGMPPartNumbere.Text = pt.GMPPartNumber;
                    PTlblMFGPartNumbere.Text = pt.PartNumber;
                    //PTlblQTY.Text = pt.Quantity.ToString();

                    //lblTransferPart.Text = pt.GMPPartDescription + " (" + pt.GMPPartNumber + " / " + pt.PartNumber + ")";
                    lblOriginalWarehouse.Text = "Source Warehouse: ";
                    lblOriginalWarehouse_A.Text = drpLocationList.SelectedItem.Text;
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
        //void GridViewDetail_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        MasterPartsLinkTablePriceList mpl = (MasterPartsLinkTablePriceList)e.Row.DataItem;
        //        HiddenField PartLinkTablePriceListID = (HiddenField)e.Row.FindControl("hdnMasterPartsLinkTablePriceListID");


        //        //    //Type type = e.Row.DataItem.GetType();               //.GetGenericArguments()[0]; 
        //        //    //GetMasterDetailInventoryList_TemplateRawData_SearchGrid_02Result Data = ((GetMasterDetailInventoryList_TemplateRawData_SearchGrid_02Result)e.Row.DataItem);
        //        ImageButton bEdit = (ImageButton)e.Row.FindControl("imgEditDetail");
        //        ImageButton bSave = (ImageButton)e.Row.FindControl("imgEditDetailSave");
        //        ImageButton bCancel = (ImageButton)e.Row.FindControl("imgEditDetailCancel");

        //        if (bEdit != null) { bEdit.Visible = User.IsInRole(Role_EditAccess); }
        //        if (bSave != null) { bSave.Visible = false; }
        //        if (bCancel != null) { bCancel.Visible = false; }


        //        TextBox txtQTY = (TextBox)e.Row.FindControl("txtCorrectedQTY");
        //        TextBox txtDispursed = (TextBox)e.Row.FindControl("txtCorrectedQTYDispursed");
        //        TextBox txtUnitPurchasePrice = (TextBox)e.Row.FindControl("txtCorrectedUnitPurchasePrice");
        //        //TextBox txtReason = (TextBox)e.Row.FindControl("txtAdjustmentReason");

        //        Label lblQty = (Label)e.Row.FindControl("lblQty");
        //        Label lblQTYDispursed = (Label)e.Row.FindControl("lblQTYDispursed");
        //        Label lblUnitPurchasePrice = (Label)e.Row.FindControl("lblUnitPurchasePrice");
        //        Label lblAdjustmentReason = (Label)e.Row.FindControl("lblAdjustmentReason");
        //        DropDownList drpType = (DropDownList)e.Row.FindControl("drpReason");
        //        if (lblQty != null) { lblQty.Visible = true; }
        //        if (lblQTYDispursed != null) { lblQTYDispursed.Visible = true; }
        //        if (lblUnitPurchasePrice != null) { lblUnitPurchasePrice.Visible = true; }
        //        if (lblAdjustmentReason != null) { lblAdjustmentReason.Visible = true; }


        //        HiddenField hdnMasterPartsLinkTablePriceListID = (HiddenField)e.Row.FindControl("hdnMasterPartsLinkTablePriceListID");
        //        if (txtQTY != null) { txtQTY.Visible = false; }
        //        if (txtDispursed != null) { txtDispursed.Visible = false; }
        //        if (txtUnitPurchasePrice != null) { txtUnitPurchasePrice.Visible = false; }

        //        if (drpType != null)
        //        {
        //            drpType.Visible = false;
        //            LoadTransactionType(drpType);
        //        }
        //        if (mpl != null)
        //        {
        //            hdnMasterPartsLinkTablePriceListID.Value = mpl.MasterPartsLinkTablePriceListID.ToString();
        //            txtQTY.Text = mpl.Quantity.ToString();
        //            txtDispursed.Text = mpl.QTYDispursed.ToString();
        //            txtUnitPurchasePrice.Text = mpl.UnitPurchasePrice.ToString();

        //            lblQty.Text = mpl.Quantity.ToString();
        //            lblQTYDispursed.Text = mpl.QTYDispursed.ToString();
        //            lblUnitPurchasePrice.Text = mpl.UnitPurchasePrice.ToString();
        //        }


        //    }
        //}
        //void GridViewDetail_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    ImageButton btn = (ImageButton)e.CommandSource;

        //    #region EditDetail
        //    if (btn.ID.ToUpper() == "IMGEDITDETAIL")
        //    {
        //        int index = Convert.ToInt32(e.CommandArgument);
        //        GridViewRow row = GridViewDetail.Rows[index];

        //        ImageButton bEdit = (ImageButton)row.FindControl("imgEditDetail");
        //        ImageButton bSave = (ImageButton)row.FindControl("imgEditDetailSave");
        //        ImageButton bCancel = (ImageButton)row.FindControl("imgEditDetailCancel");
        //        if (bEdit != null) { bEdit.Visible = false; }
        //        if (bSave != null) { bSave.Visible = true; }
        //        if (bCancel != null) { bCancel.Visible = true; }

        //        TextBox txtQTY = (TextBox)row.FindControl("txtCorrectedQTY");
        //        TextBox txtDispursed = (TextBox)row.FindControl("txtCorrectedQTYDispursed");
        //        TextBox txtUnitPurchasePrice = (TextBox)row.FindControl("txtCorrectedUnitPurchasePrice");
        //        DropDownList dReason = (DropDownList)row.FindControl("drpReason");
        //        if (txtQTY != null) { txtQTY.Visible = true; }
        //        if (txtDispursed != null) { txtDispursed.Visible = true; }
        //        if (txtUnitPurchasePrice != null) { txtUnitPurchasePrice.Visible = true; }
        //        if (dReason != null) { dReason.Visible = true; }

        //        Label lblQty = (Label)row.FindControl("lblQty");
        //        Label lblQTYDispursed = (Label)row.FindControl("lblQTYDispursed");
        //        Label lblUnitPurchasePrice = (Label)row.FindControl("lblUnitPurchasePrice");
        //        Label lblAdjustmentReason = (Label)row.FindControl("lblAdjustmentReason");
        //        if (lblQty != null) { lblQty.Visible = false; }
        //        if (lblQTYDispursed != null) { lblQTYDispursed.Visible = false; }
        //        if (lblUnitPurchasePrice != null) { lblUnitPurchasePrice.Visible = false; }
        //        if (lblAdjustmentReason != null) { lblAdjustmentReason.Visible = false; }

        //        btnEditDetailSave.Visible = false;
        //        btnEditDetailCancel.Visible = false;
        //    }
        //    #endregion
        //    #region Save
        //    if (btn.ID.ToUpper() == "IMGEDITDETAILSAVE")
        //    {
        //        //int index = Convert.ToInt32(e.CommandArgument);
        //        //GridViewRow row = GridViewDetail.Rows[index];

        //        ////MasterPartsLinkTablePriceList mpl = (MasterPartsLinkTablePriceList)row.DataItem;
        //        //HiddenField hdnPartLinkTablePriceListID = (HiddenField)row.FindControl("hdnMasterPartsLinkTablePriceListID");
        //        //ImageButton bEdit = (ImageButton)row.FindControl("imgEditDetail");
        //        //ImageButton bSave = (ImageButton)row.FindControl("imgEditDetailSave");
        //        //ImageButton bCancel = (ImageButton)row.FindControl("imgEditDetailCancel");
        //        //if (bEdit != null) { bEdit.Visible = User.IsInRole(Role_EditAccess); }
        //        //if (bSave != null) { bSave.Visible = false; }
        //        //if (bCancel != null) { bCancel.Visible = false; }
        //        //DropDownList dReason = (DropDownList)row.FindControl("drpReason");

        //        //TextBox txtQTY = (TextBox)row.FindControl("txtCorrectedQTY");
        //        //TextBox txtDispursed = (TextBox)row.FindControl("txtCorrectedQTYDispursed");
        //        //TextBox txtUnitPurchasePrice = (TextBox)row.FindControl("txtCorrectedUnitPurchasePrice");
        //        ////TextBox txtReason = (TextBox)row.FindControl("txtAdjustmentReason");
        //        //if (txtQTY != null) { txtQTY.Visible = false; }
        //        //if (txtDispursed != null) { txtDispursed.Visible = false; }
        //        //if (txtUnitPurchasePrice != null) { txtUnitPurchasePrice.Visible = false; }
        //        //if (dReason != null) { dReason.Visible = false; }

        //        //Label lblQty = (Label)row.FindControl("lblQty");
        //        //Label lblQTYDispursed = (Label)row.FindControl("lblQTYDispursed");
        //        //Label lblUnitPurchasePrice = (Label)row.FindControl("lblUnitPurchasePrice");
        //        //Label lblAdjustmentReason = (Label)row.FindControl("lblAdjustmentReason");
        //        //if (lblQty != null) { lblQty.Visible = true; }
        //        //if (lblQTYDispursed != null) { lblQTYDispursed.Visible = true; }
        //        //if (lblUnitPurchasePrice != null) { lblUnitPurchasePrice.Visible = true; }
        //        //if (lblAdjustmentReason != null) { lblAdjustmentReason.Visible = true; }

        //        //decimal PartLinkTablePriceListID = -1;
        //        //decimal Qty = 0;
        //        //decimal Dispursed = 0;
        //        //decimal UnitPurchasePrice = 0;
        //        ////decimal oQty = 0;
        //        ////decimal oDispursed = 0;
        //        ////decimal oUnitPurchasePrice = 0;


        //        ////decimal dQty = 0;
        //        ////decimal dDispursed = 0;
        //        ////decimal dUnitPurchasePrice = 0;
        //        //if (hdnPartLinkTablePriceListID != null) { if (decimal.TryParse(hdnPartLinkTablePriceListID.Value, out PartLinkTablePriceListID) == false) { PartLinkTablePriceListID = -1; } }
        //        //if (txtQTY != null) { if (decimal.TryParse(txtQTY.Text, out Qty) == false) { Qty = 0; } }
        //        //if (txtDispursed != null) { if (decimal.TryParse(txtDispursed.Text, out Dispursed) == false) { Dispursed = 0; } }
        //        //if (txtUnitPurchasePrice != null) { if (decimal.TryParse(txtUnitPurchasePrice.Text, out UnitPurchasePrice) == false) { UnitPurchasePrice = 0; } }

        //        ////if (lblQty != null) { if (decimal.TryParse(lblQty.Text, out oQty) == false) { oQty = 0; } }
        //        ////if (lblQTYDispursed != null) { if (decimal.TryParse(lblQTYDispursed.Text, out oDispursed) == false) { oDispursed = 0; } }
        //        ////if (lblUnitPurchasePrice != null) { if (decimal.TryParse(lblUnitPurchasePrice.Text, out oUnitPurchasePrice) == false) { oUnitPurchasePrice = 0; } }

        //        ////dQty = Qty - oQty;
        //        ////dDispursed = Dispursed - oDispursed;
        //        ////dUnitPurchasePrice = UnitPurchasePrice;

        //        //MasterPartManager mpm = new MasterPartManager(User.Identity.Name);
        //        //mpm.AddToInventoryEditDetailsOnly(PartLinkTablePriceListID, Qty, Dispursed, UnitPurchasePrice, dReason.SelectedItem.Text);

        //        //if (txtQTY != null && lblQty != null) { lblQty.Text = txtQTY.Text; }
        //        //if (txtDispursed != null && lblQTYDispursed != null) { lblQTYDispursed.Text = txtDispursed.Text; }
        //        //if (txtUnitPurchasePrice != null && lblUnitPurchasePrice != null) { lblUnitPurchasePrice.Text = txtUnitPurchasePrice.Text; }
        //        //if (dReason != null && lblAdjustmentReason != null) { lblAdjustmentReason.Text = dReason.SelectedItem.Text; }

        //        //btnEditDetailSave.Visible = true;
        //        //btnEditDetailCancel.Visible = true;


        //    }
        //    #endregion
        //    #region Cancel
        //    if (btn.ID.ToUpper() == "IMGEDITDETAILCANCEL")
        //    {
        //        int index = Convert.ToInt32(e.CommandArgument);
        //        GridViewRow row = GridViewDetail.Rows[index];

        //        ImageButton bEdit = (ImageButton)row.FindControl("imgEditDetail");
        //        ImageButton bSave = (ImageButton)row.FindControl("imgEditDetailSave");
        //        ImageButton bCancel = (ImageButton)row.FindControl("imgEditDetailCancel");
        //        if (bEdit != null) { bEdit.Visible = User.IsInRole(Role_EditAccess); }
        //        if (bSave != null) { bSave.Visible = false; }
        //        if (bCancel != null) { bCancel.Visible = false; }

        //        TextBox txtQTY = (TextBox)row.FindControl("txtCorrectedQTY");
        //        TextBox txtDispursed = (TextBox)row.FindControl("txtCorrectedQTYDispursed");
        //        TextBox txtUnitPurchasePrice = (TextBox)row.FindControl("txtCorrectedUnitPurchasePrice");
        //        DropDownList dReason = (DropDownList)row.FindControl("drpReason");
        //        if (txtQTY != null) { txtQTY.Visible = false; }
        //        if (txtDispursed != null) { txtDispursed.Visible = false; }
        //        if (txtUnitPurchasePrice != null) { txtUnitPurchasePrice.Visible = false; }
        //        if (dReason != null) { dReason.Visible = false; }


        //        Label lblQty = (Label)row.FindControl("lblQty");
        //        Label lblQTYDispursed = (Label)row.FindControl("lblQTYDispursed");
        //        Label lblUnitPurchasePrice = (Label)row.FindControl("lblUnitPurchasePrice");
        //        Label lblAdjustmentReason = (Label)row.FindControl("lblAdjustmentReason");

        //        if (lblQty != null) { lblQty.Visible = true; }
        //        if (lblQTYDispursed != null) { lblQTYDispursed.Visible = true; }
        //        if (lblUnitPurchasePrice != null) { lblUnitPurchasePrice.Visible = true; }
        //        if (lblAdjustmentReason != null) { lblAdjustmentReason.Visible = true; }

        //        if (txtQTY != null && lblQty != null) { txtQTY.Text = lblQty.Text; }
        //        if (txtDispursed != null && lblQTYDispursed != null) { txtDispursed.Text = lblQTYDispursed.Text; }
        //        if (txtUnitPurchasePrice != null && lblUnitPurchasePrice != null) { txtUnitPurchasePrice.Text = lblUnitPurchasePrice.Text; }

        //        btnEditDetailSave.Visible = true;
        //        btnEditDetailCancel.Visible = true;
        //    }
        //    #endregion
        //}



        void gvLocationPOReceiveParts_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            ImageButton btnOpen = (ImageButton)e.CommandSource;
            string CommandArgument = btnOpen.CommandArgument;
            #region IMGDELETE
            if (btnOpen.ID.ToUpper() == "IMGSELECTLOCATION")
            {
                IFSToLocationPOLineReceive.Text = btnOpen.CommandArgument;
                //drpLocationEditParts.ClearSelection();
                //drpLocationEditParts.Items.FindByText(btnOpen.CommandArgument).Selected = true;
            }
            #endregion
        }

        void gvLocationPOReceiveParts_RowDataBound(object sender, GridViewRowEventArgs e)
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
                IFSToLocation.Text = btnOpen.CommandArgument;
                //drpLocationEditParts.ClearSelection();
                //drpLocationEditParts.Items.FindByText(btnOpen.CommandArgument).Selected = true;
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
        void gvLocationTransferPartsFrom_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            ImageButton btnOpen = (ImageButton)e.CommandSource;
            string CommandArgument = btnOpen.CommandArgument;
            #region IMGDELETE
            if (btnOpen.ID.ToUpper() == "IMGSELECTLOCATION")
            {
                PTIFSFromLocation.Text = btnOpen.CommandArgument;
                //drpLocationTransferPartFrom.ClearSelection();
                //drpLocationTransferPartFrom.Items.FindByText(btnOpen.CommandArgument).Selected = true;
            }
            if (btnOpen.ID.ToUpper() == "IMGSELECTLOCATIONDOWN")
            {
                PTIFSToLocation.Text = btnOpen.CommandArgument;
                //drpLocationTransferPartFrom.ClearSelection();
                //drpLocationTransferPartFrom.Items.FindByText(btnOpen.CommandArgument).Selected = true;
            }




            #endregion
        }
        void gvLocationTransferPartsFrom_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GRDPartLocationView mpl = (GRDPartLocationView)e.Row.DataItem;
                ImageButton bLocation = (ImageButton)e.Row.FindControl("imgSelectLocation");
                ImageButton bLocation2 = (ImageButton)e.Row.FindControl("imgSelectLocationDown");
                if (bLocation != null) { bLocation.CommandArgument = "-1"; }
                if (bLocation2 != null) { bLocation2.CommandArgument = "-1"; }
                if (mpl != null)
                {
                    if (bLocation != null) { bLocation.CommandArgument = mpl.IFSLocation; }
                    if (bLocation2 != null) { bLocation2.CommandArgument = mpl.IFSLocation; }
                }
            }
        }

        void btnRefresh_Click(object sender, EventArgs e)
        {
            UpdateMainGridPM();
        }

        void btnSearchPartNumber_Click(object sender, EventArgs e)
        {
            UpdateMainGridPMSearch(-1);
        }


        protected void UpdateMainGridPM()
        {
            UpdateMainGridPM(-1);
        }
        protected void UpdateMainGridPM(int Page)
        {
            FillCarrierManufacturerModelKeys();
            // Get the list of parts that match this condition.
            MasterPartManager mpm = new MasterPartManager(User.Identity.Name);
            ////mParts = mpm.GetMasterPartNumbersThisPart(decimal.Parse(drpDropPart.SelectedItem.Value), -1, decimal.Parse(drpLocationList.SelectedItem.Value), "-1", drpManufacturer.SelectedItem.Value, Model, Page, MainGridPN.PageSize);
            hdnAllowGridUpdate.Value = "T";
            MainGridPN.DataSource = mpm.GetMasterPartNumbersThisPart(decimal.Parse(drpDropPart.SelectedItem.Value), -1, decimal.Parse(drpLocationList.SelectedItem.Value), "-1", drpManufacturer.SelectedItem.Value, Model, Page, MainGridPN.PageSize);
            MainGridPN.DataBind();
            //btnSavePartNumbers.Visible = true;
            //btnSavePartNumbers.Enabled = true;
        }
        protected void UpdateMainGridPMSearch(int Page)
        {
            //FillCarrierManufacturerModelKeys();
            // Get the list of parts that match this condition.
            MasterPartManager mpm = new MasterPartManager(User.Identity.Name);
            ////mParts = mpm.GetMasterPartNumbersThisPart(decimal.Parse(drpDropPart.SelectedItem.Value), -1, decimal.Parse(drpLocationList.SelectedItem.Value), "-1", drpManufacturer.SelectedItem.Value, Model, Page, MainGridPN.PageSize);
            hdnAllowGridUpdate.Value = "T";


            MainGridPN.DataSource = mpm.GetMasterPartNumbersThisPart(decimal.Parse(drpLocationList.SelectedItem.Value), txtIMMPartNumber.Text);
            //MainGridPN.DataSource = mpm.GetMasterPartNumbersThisPart(decimal.Parse(drpDropPart.SelectedItem.Value), -1, decimal.Parse(drpLocationList.SelectedItem.Value), "-1", drpManufacturer.SelectedItem.Value, Model, Page, MainGridPN.PageSize);
            MainGridPN.DataBind();
            //btnSavePartNumbers.Visible = true;
            //btnSavePartNumbers.Enabled = true;
        }

        //void btnEditDetailCancel_Click(object sender, EventArgs e)
        //{
        //    //pnlEditDetail.Visible = false;
        //    pnlMainGridPN.Visible = true;
        //    pnlHeader.Visible = true;
        //}
        //void btnEditDetailSave_Click(object sender, EventArgs e)
        //{
        //    decimal MasterPartsLinkTableID = -1;
        //    if (decimal.TryParse(hdnMasterPartLinkTableID.Value, out MasterPartsLinkTableID) == true)
        //    {
        //        MasterPartManager mpm = new MasterPartManager(User.Identity.Name);
        //        mpm.SetPartAveragePurchasePrice(MasterPartsLinkTableID);
        //    }
        //    //pnlEditDetail.Visible = false;
        //    pnlMainGridPN.Visible = true;
        //    pnlHeader.Visible = true;
        //    UpdateMainGridPM();
        //}
        //void EditModelSave_Click(object sender, EventArgs e)
        //{
        //    decimal ID = -1;
        //    if (decimal.TryParse(hdnMasterPartLinkTableID.Value, out ID) == false) { ID = -1; }

        //    string Models = "";
        //    foreach (ListItem i in chkEditModels.Items)
        //    {
        //        if (i.Selected == true)
        //        {
        //            if (Models.Length > 0) { Models += ","; }
        //            Models += i.Value;
        //        }
        //    }
        //    MasterPartManager mpm = new MasterPartManager(User.Identity.Name);
        //    using (clsLinqDataContext ctx = mpm.GetDataContext(User.Identity.Name))
        //    {
        //        mpm.RestModelList(ctx, Models, ID);
        //    }
        //    pnlEditModels.Visible = false;
        //    pnlMainGridPN.Visible = true;
        //    pnlHeader.Visible = true;
        //    UpdateMainGridPM(MainGridPN.PageIndex);
        //}
        //void EditModelCancel_Click(object sender, EventArgs e)
        //{
        //    pnlEditModels.Visible = false;
        //    pnlMainGridPN.Visible = true;
        //    pnlHeader.Visible = true;
        //}













        void btnAddTransactionSave_Click(object sender, EventArgs e)
        {
            if (IFSToLocation.Text.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "xxxx", "alert('No IFS Location Given.');", true);
                return;
            }
            decimal mpid = -1;
            decimal qty = 0;
            decimal typeid = -1;
            //decimal Locationid = -1;
            decimal UnitPurchasePrice = 0;

            if (decimal.TryParse(hdnMasterPartLinkTableID.Value, out mpid) == false) { mpid = -1; }
            if (decimal.TryParse(txtTransQTY.Text, out qty) == false) { qty = 0; }
            if (decimal.TryParse(drpTransType.SelectedValue, out typeid) == false) { typeid = 0; }
            //if (decimal.TryParse(drpLocationEditParts.SelectedValue, out Locationid) == false) { Locationid = 0; }

            if (decimal.TryParse(txtTransPurchasePrice.Text, out UnitPurchasePrice) == false) { UnitPurchasePrice = -1; }
            MasterPartManager mpm = new MasterPartManager(User.Identity.Name);
            MasterIFSLocation loc = mpm.GetIFSLocation(IFSToLocation.Text);
            if (loc == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "xxxx", "alert('IFS Location Not Valid!');", true);
                return;
            }
            if (qty > 0)
            {
                pnlAddTransaction.Visible = false;
                pnlMainGridPN.Visible = true;
                pnlHeader.Visible = true;

                drpLocationList.Enabled = true;
                drpDropPart.Enabled = true;
                drpManufacturer.Enabled = true;
                btnRefresh.Enabled = true;

                mpm.AddToInventory(mpid, loc.MasterIFSLocationID, qty, UnitPurchasePrice, typeid, txtTransDesc.Text, txtPOVendor.Text, txtPONumber.Text, txtPOReceiptDate.Text, txtPOLine.Text);
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
        void btnTransferCancel_Click(object sender, EventArgs e)
        {
            lblTransferMessage.Text = "";
            txtTransferReason.Text = "";
            txtTransferQTY.Text = "";
            //chkAveragePurchasePrice.Checked = true;

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


            //decimal IFSFromlocationID = -1;
            //decimal IFSTolocationID = -1;
            //if (decimal.TryParse(drpLocationTransferPartFrom.SelectedValue, out IFSFromlocationID) == false) { IFSFromlocationID = -1; }
            //if (decimal.TryParse(drpLocationTransferPartTo.SelectedValue, out IFSTolocationID) == false) { IFSTolocationID = -1; }

            if (PTIFSFromLocation.Text.Length == 0)
            {
                lblTransferMessage.Text = "No From IFS Location Given!";
                return;
            }
            if (PTIFSToLocation.Text.Length == 0)
            {
                lblTransferMessage.Text = "No To IFS Location Given!";
                return;
            }

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
            //if (drpLocationList2.SelectedItem.Text == drpLocationList.SelectedItem.Text)
            //{
            //    lblTransferMessage.Text = "Transfer location from (" + drpLocationList.SelectedItem.Text + ") and too (" + drpLocationList2.SelectedItem.Text + ") are the same. Transfer aborted!";
            //    return;
            //}

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
            MasterIFSLocation loc = pm.GetIFSLocation(PTIFSFromLocation.Text);
            if (loc == null)
            {
                lblTransferMessage.Text = "Invalid From IFS Location!";
                return;
            }
            MasterIFSLocation locto = pm.GetIFSLocation(PTIFSToLocation.Text);
            if (locto == null)
            {
                lblTransferMessage.Text = "Invalid To IFS Location!";
                return;
            }

            lblTransferMessage.Text = pm.TransferInverntory(MasterPartLinkTableID, loc.MasterIFSLocationID, locto.MasterIFSLocationID, QTY, NewlocationID, txtTransferReason.Text, false);
            txtTransferQTY.Text = "";
            RefreshTransferPart(MasterPartLinkTableID);

        }
        //void btnCancelSave_Click(object sender, EventArgs e)
        //{
        //    ShowPartPanel("NOTNEW");
        //}
        //void btnAddNew_Click(object sender, EventArgs e)
        //{
        //    ShowPartPanel("NEW");
        //}
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


        void btnSavePartNumberse_Click(object sender, EventArgs e)
        {
            pnlEditPart.Visible = false;
            pnlMainGridPN.Visible = true;
            pnlHeader.Visible = true;

            drpLocationList.Enabled = true;
            drpDropPart.Enabled = true;
            drpManufacturer.Enabled = true;
            btnRefresh.Enabled = true;

            decimal ManufacturerID = -1;
            decimal mpid = -1;
            decimal Classtypeid = -1;
            decimal UnitPrice = 0;
            decimal InWarrentyUnitPrice = 0;
            decimal QTYMin = 0;
            decimal QTYMax = 0;
            decimal QTYReorder = 0;

            if (decimal.TryParse(hdnMasterPartLinkTableID.Value, out mpid) == false) { mpid = -1; }
            if (decimal.TryParse(drpManufacturerEdit.SelectedItem.Value, out ManufacturerID) == false) { ManufacturerID = -1; }

            if (decimal.TryParse(drpClassType.SelectedValue, out Classtypeid) == false) { Classtypeid = 0; }
            if (txtUnitPrice.Text.Length == 0) { txtUnitPrice.Text = "-1"; }
            if (decimal.TryParse(txtUnitPrice.Text, out UnitPrice) == false) { UnitPrice = -1; }
            if (decimal.TryParse(txtInWarrentyPrice.Text, out InWarrentyUnitPrice) == false) { InWarrentyUnitPrice = -1; }

            if (decimal.TryParse(txtInventoryMin.Text, out QTYMin) == false) { QTYMin = -1; }
            if (decimal.TryParse(txtInventoryMax.Text, out QTYMax) == false) { QTYMax = -1; }
            if (decimal.TryParse(txtReorderPoint.Text, out QTYReorder) == false) { QTYReorder = -1; }
            MasterPartManager mpm = new MasterPartManager(User.Identity.Name);

            mpm.AddToInventory(mpid, UnitPrice, InWarrentyUnitPrice, txtGMPPartNumbere.Text, txtMFGPartNumbere.Text, txtGMPDesce.Text, QTYMin, QTYMax, QTYReorder, Classtypeid);
            mpm.UpdateInventoryDemographicData_Manufacturer(mpid, ManufacturerID);
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


        void btnLocationViewClose_Click(object sender, EventArgs e)
        {
            pnlLocationView.Visible = false;
            pnlMainGridPN.Visible = true;
            pnlHeader.Visible = true;
        }
        //void btnAddPartNumbers_Click(object sender, EventArgs e)
        //{
        //    FillCarrierManufacturerModelKeys();
        //    MasterPartManager mpm = new MasterPartManager(User.Identity.Name);

        //    string PartNumber = txtMFGPartNumber.Text;  // MFG Part #
        //    string GMPPartNumberKey = txtGMPPartNumber.Text;  // GMP Part #
        //    string GMPPartDescription = txtGMPDesc.Text;  // GMP Part Description
        //    decimal Classtypeid = -1;

        //    if (decimal.TryParse(drpAClassType.SelectedValue, out Classtypeid) == false) { Classtypeid = 0; }


        //    decimal mpid = -1;
        //    decimal ClientLocationID = -1;
        //    if (decimal.TryParse(drpLocationList.SelectedItem.Value, out ClientLocationID) == false) { ClientLocationID = -1; }
        //    if (decimal.TryParse(drpDropPart.SelectedItem.Value, out mpid) == false) { mpid = -1; }


        //    if (mpm.isPartNumberThere(PartNumber, ClientLocationID) == true)
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "Saved", "alert('Partnumber already on file, data NOT saved');", true);
        //        return;
        //    }
        //    if (PartNumber.Length > 0)
        //    {
        //        mpm.InsertPartNumber(mpid, -1, ClientLocationID, "-1", Manufacturer, Model, PartNumber, GMPPartNumberKey, GMPPartDescription, Classtypeid);
        //        UpdateMainGridPM();
        //    }
        //    txtMFGPartNumber.Text = "";
        //    txtGMPPartNumber.Text = "";
        //    txtGMPDesc.Text = "";
        //    ShowPartPanel("NOTNEW");
        //}







        protected string GetDropdownText(decimal Key)
        {
            foreach (ListItem i in drpDropPart.Items) { if (i.Value == Key.ToString()) { return i.Text; } }
            return "";
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

            //IFS_InventoryPartsManager ipm = new IFS_InventoryPartsManager(User.Identity.Name);
            //List<MasterIFSLocation> loc = ipm.GetLocationList();
            //drpLocationEditParts.Items.Clear();
            //drpLocationTransferPartFrom.Items.Clear();
            //drpLocationTransferPartTo.Items.Clear();
            //foreach (MasterIFSLocation cl in loc.OrderBy(x => x.IFSLocation))
            //{
            //    ListItem li = new ListItem(cl.IFSLocation, cl.MasterIFSLocationID.ToString());
            //    drpLocationEditParts.Items.Add(li);
            //    ListItem li1 = new ListItem(cl.IFSLocation, cl.MasterIFSLocationID.ToString());
            //    drpLocationTransferPartFrom.Items.Add(li1);
            //    ListItem li2 = new ListItem(cl.IFSLocation, cl.MasterIFSLocationID.ToString());
            //    drpLocationTransferPartTo.Items.Add(li2);
            //}


            ClientManager cm = new ClientManager(User.Identity.Name);
            List<ClientLocation> cls = cm.GetClientLocationsWithOnSiteInventory();

            drpLocationList2.Items.Clear();
            drpLocationList2.Items.Add(new ListItem("WHS 001", "-1"));

            drpLocationList.Items.Clear();
            drpLocationList.Items.Add(new ListItem("WHS 001", "-1"));

            drpLocationListPOReceive.Items.Clear();
            drpLocationListPOReceive.Items.Add(new ListItem("WHS 001", "-1"));



            foreach (ClientLocation cl in cls)
            {
                ListItem li = new ListItem(cl.CompanyName, cl.ClientLocationID.ToString());
                drpLocationList.Items.Add(li);
                ListItem li2 = new ListItem(cl.CompanyName, cl.ClientLocationID.ToString());
                drpLocationList2.Items.Add(li2);

                ListItem li3 = new ListItem(cl.CompanyName, cl.ClientLocationID.ToString());
                drpLocationListPOReceive.Items.Add(li3);

            }
            drpLocationList.SelectedIndex = 0;
            drpLocationList2.SelectedIndex = 0;
            drpLocationListPOReceive.SelectedIndex = 0;





            LO = qm.GetQuestionOptionList(ctx, "Manufacturer");
            drpManufacturer.Items.Clear();
            drpManufacturer.Items.Add(new ListItem("<None>", "-1"));

            drpManufacturerEdit.Items.Clear();
            drpManufacturerEdit.Items.Add(new ListItem("<None>", "-1"));

            foreach (Option o in LO)
            {
                ListItem li = new ListItem("(" + o.ScanKey + ") " + o.OptionText, o.OptionID.ToString());
                drpManufacturer.Items.Add(li);
                ListItem li2 = new ListItem("(" + o.ScanKey + ") " + o.OptionText, o.OptionID.ToString());
                drpManufacturerEdit.Items.Add(li2);

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
            //chkModels.Items.Clear();
            //chkModels.Items.Add(new ListItem("None", "-1"));
            //chkEditModels.Items.Clear();
            //chkEditModels.Items.Add(new ListItem("None", "-1"));
            //foreach (Option o in LO)
            //{
            //    ListItem li = new ListItem(o.OptionText, o.OptionID.ToString());
            //    ListItem l1 = new ListItem(o.OptionText, o.OptionID.ToString());
            //    chkModels.Items.Add(li);
            //    chkEditModels.Items.Add(l1);
            //}
            //chkModels.SelectedIndex = 0;
            //chkEditModels.SelectedIndex = 0;
        }
        //protected void LoadModelDistinct()
        //{
        //    QuestionManager qm = new QuestionManager(User.Identity.Name);
        //    decimal ID = -1;
        //    if (drpManufacturer.SelectedItem.Value == "-1")
        //    {
        //        List<Option> LO = new List<Option>();
        //        LO = qm.GetQuestionOptionList("Model");
        //        chkModels.Items.Clear();
        //        chkModels.Items.Add(new ListItem("None", "-1"));
        //        foreach (Option o in LO)
        //        {
        //            ListItem li = new ListItem(o.OptionText, o.OptionID.ToString());
        //            chkModels.Items.Add(li);
        //        }
        //        chkModels.SelectedIndex = 0;
        //    }
        //    else
        //    {
        //        AnswerManager am = new AnswerManager(User.Identity.Name);
        //        MasterCarrierManufacturerModelColourManager MM = new MasterCarrierManufacturerModelColourManager(User.Identity.Name);
        //        List<MasterCarrierManufacturerLookup> ml = MM.GetMasterModelList_NoCarrier("-1", drpManufacturer.SelectedItem.Value);
        //        chkModels.Items.Clear();
        //        chkModels.Items.Add(new ListItem("None", "-1"));
        //        foreach (MasterCarrierManufacturerLookup mm in ml)
        //        {
        //            if (mm.OptionModelID != null) { ID = (decimal)mm.OptionModelID; }
        //            var o = am.GetAnswer(ID);
        //            if (o != null)
        //            {
        //                ListItem li = new ListItem("(" + o.ScanKey + ") " + o.OptionText, o.OptionID.ToString());
        //                chkModels.Items.Add(li);
        //            }
        //        }
        //        chkModels.SelectedIndex = 0;
        //    }
        //}

        //void ShowPartPanel(string P)
        //{
        //    if (P == "NEW")
        //    {
        //        //pnlAddNewPart.Visible = true;
        //        pnlMainGridPN.Visible = false;
        //        pnlHeader.Visible = false;
        //    }
        //    else
        //    {
        //        //pnlAddNewPart.Visible = false;
        //        pnlMainGridPN.Visible = true;
        //        pnlHeader.Visible = true;
        //    }
        //}

        protected void LoadClassType(DropDownList drpType)
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
        protected void FillCarrierManufacturerModelKeys()
        {
            Manufacturer = drpManufacturer.SelectedItem.Value;
            Model = "";
            //foreach (ListItem x in chkModels.Items) { if (x.Selected == true) { Model += x.Value + ","; } }
        }
        //void LoadTransactionType(DropDownList drpType)
        //{
        //    List<PartNumberBucketInventoryTransactionType> TypeList = new List<PartNumberBucketInventoryTransactionType>();
        //    if (TypeList.Count == 0)
        //    {
        //        MasterPartManager mpm = new MasterPartManager(User.Identity.Name);
        //        TypeList = mpm.GetMasterPartTransType();
        //    }
        //    drpType.Items.Clear();
        //    foreach (PartNumberBucketInventoryTransactionType o in TypeList)
        //    {
        //        ListItem li = new ListItem(o.Type, o.PartNumberBucketInventorysourceTypeID.ToString());
        //        if (o.Type.ToUpper() == "REPLENISH") { li.Selected = true; }        // set the default type.
        //        if (o.Type.ToUpper() != "USED BY TECH" && o.Type.ToUpper() != "REPLENISH" && o.Type.ToUpper() != "TRANSFER IN" && o.Type.ToUpper() != "TRANSFER OUT") { drpType.Items.Add(li); }
        //    }
        //}
        protected void LoadTransactionType_Restricted(DropDownList drpType)
        {
            List<PartNumberBucketInventoryTransactionType> TypeList = new List<PartNumberBucketInventoryTransactionType>();
            if (TypeList.Count == 0)
            {
                MasterPartManager mpm = new MasterPartManager(User.Identity.Name);
                TypeList = mpm.GetMasterPartTransType();
            }
            drpType.Items.Clear();
            //TurnReplenishTab(false);
            foreach (PartNumberBucketInventoryTransactionType o in TypeList)
            {
                if (o.Role == null || o.Role.Length == 0 || User.IsInRole(o.Role) == true || User.IsInRole("Admin") == true)
                {
                    ListItem li = new ListItem(o.Type, o.PartNumberBucketInventorysourceTypeID.ToString());
                    if (o.Type.ToUpper() != "REPLENISH" && o.Type.ToUpper() != "USED BY TECH" && o.Type.ToUpper() != "TRANSFER IN" && o.Type.ToUpper() != "TRANSFER OUT")
                    {
                        drpType.Items.Add(li);
                        if (o.Type.ToUpper() == "REPLENISH" || o.Type.ToUpper() == "REPLENISH NOPO") { li.Selected = true; }        // set the default type.
                    }
                    //if (o.Type.ToUpper() == "REPLENISH" || o.Type.ToUpper() == "REPLENISH NOPO") { TurnReplenishTab(true); }        // set the default type.
                }
            }
            ReadyTransTypeFields();
        }

        private void TurnReplenishTab(bool On)
        {
            TabPOReceive.Visible = On;
            TurnReplenish(On);

        }
        private void TurnReplenish(bool On)
        {
            RowPOVendor.Visible = On;
            RowPONumber.Visible = On;
            RowPOLine.Visible = On;
            RowPOReceipt.Visible = On;
            RowPOPrice.Visible = On;
        }
    }
}