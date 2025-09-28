using System;
using System.Drawing;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using Syncfusion.Grouping;
using BW_WebApp.DataManagers;

namespace BW_WebApp
{
    public partial class ReceiveDetailEditParts : System.Web.UI.Page
    {
        //private bool _isAlreadySorted;
        //public bool IsAlreadySorted
        //{
        //    get
        //    {
        //        return _isAlreadySorted;
        //    }
        //    set
        //    {
        //        _isAlreadySorted = value;
        //    }
        //}
        //ReceiveDetailEditPartsData CurRec = null;
        decimal ReceiveDetailID = -1;
        string ESN = "";
        string Role_EditAccess = "PartsAdmin";


        protected void Page_Load(object sender, EventArgs e)
        {

            string sReceiveDetailID = Request.QueryString.Get("ID");
            ESN = Request.QueryString.Get("ESN");
            decimal.TryParse(sReceiveDetailID, out ReceiveDetailID);
            if (ReceiveDetailID < 1) { return; }
            lblRecordTitle.Text = "Parts for:" + ESN;
            hdnReceiveDetailID.Value = ReceiveDetailID.ToString();

            GridViewDetail.RowDataBound += new GridViewRowEventHandler(GridViewDetail_RowDataBound);
            GridViewDetail.RowCommand += new GridViewCommandEventHandler(GridViewDetail_RowCommand);


            //this.MainGrid_B.ShowGroupDropArea = false;
            ////this.MainGrid_B.TableDescriptor.Appearance.AlternateRecordFieldCell.BackColor = Color.AliceBlue;
            //this.MainGrid_B.Appearance.AlternateRecordFieldCell.Interior = new Syncfusion.Drawing.BrushInfo(System.Drawing.ColorTranslator.FromHtml("#E2E2E2"));
            ////this.MainGrid_B.
            ////MainGrid_B.CurrentTable.SelectedRecordsChanged += new SelectedRecordsChangedEventHandler(CurrentTable_SelectedRecordsChanged);
            ////MainGrid_B.Table.SelectedRecordsChanged += new SelectedRecordsChangedEventHandler(Table_SelectedRecordsChanged);
            if (!IsPostBack)
            {
                clsLinqDataContext ctx = new clsLinqDataContext();
                UpdateMainGrid();

            }
            //this.MainGrid_B.TableDescriptor.SortedColumns.Changed += new Syncfusion.Collections.ListPropertyChangedEventHandler(SortedColumns_Changed);

        }

        void GridViewDetail_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ReceiveDetailEditPartsData mpl = (ReceiveDetailEditPartsData)e.Row.DataItem;
                HiddenField hdnReceiveDetailItemID = (HiddenField)e.Row.FindControl("hdnReceiveDetailItemID");
                HiddenField hdnReceiveDetailPartsUsageID = (HiddenField)e.Row.FindControl("hdnReceiveDetailPartsUsageID");


                //    //Type type = e.Row.DataItem.GetType();               //.GetGenericArguments()[0]; 
                //    //GetMasterDetailInventoryList_TemplateRawData_SearchGrid_02Result Data = ((GetMasterDetailInventoryList_TemplateRawData_SearchGrid_02Result)e.Row.DataItem);
                ImageButton bEdit = (ImageButton)e.Row.FindControl("imgEditDetail");
                ImageButton bSave = (ImageButton)e.Row.FindControl("imgEditDetailSave");
                ImageButton bCancel = (ImageButton)e.Row.FindControl("imgEditDetailCancel");

                if (bEdit != null) { bEdit.Visible = (User.IsInRole(Role_EditAccess) || User.IsInRole("Admin") == true); }
                if (bSave != null) { bSave.Visible = false; }
                if (bCancel != null) { bCancel.Visible = false; }

                TextBox txtPrice = (TextBox)e.Row.FindControl("txtPrice");
                TextBox txtAveragePurchasePrice = (TextBox)e.Row.FindControl("txtAveragePurchasePrice");
                TextBox txtUnitPurchasePrice = (TextBox)e.Row.FindControl("txtUnitPurchasePrice");

                Label lblPrice = (Label)e.Row.FindControl("lblPrice");
                Label lblAveragePurchasePrice = (Label)e.Row.FindControl("lblAveragePurchasePrice");
                Label lblUnitPurchasePrice = (Label)e.Row.FindControl("lblUnitPurchasePrice");

                if (lblPrice != null) { lblPrice.Visible = true; }
                if (lblAveragePurchasePrice != null) { lblAveragePurchasePrice.Visible = true; }
                if (lblUnitPurchasePrice != null) { lblUnitPurchasePrice.Visible = true; }

                if (txtPrice != null) { txtPrice.Visible = false; }
                if (txtAveragePurchasePrice != null) { txtAveragePurchasePrice.Visible = false; }
                if (txtUnitPurchasePrice != null) { txtUnitPurchasePrice.Visible = false; }

                if (mpl != null)
                {
                    hdnReceiveDetailItemID.Value = mpl.ReceiveDetailItemID.ToString();
                    hdnReceiveDetailID.Value = mpl.ReceiveDetailID.ToString();
                    hdnReceiveDetailPartsUsageID.Value = mpl.ReceiveDetailPartsUsageID.ToString();

                    txtPrice.Text = mpl.UnitPrice.ToString();
                    txtAveragePurchasePrice.Text = mpl.AveragePurchasePrice.ToString();
                    txtUnitPurchasePrice.Text = mpl.UnitPurchasePrice.ToString();

                    lblPrice.Text = mpl.UnitPrice.ToString();
                    lblAveragePurchasePrice.Text = mpl.AveragePurchasePrice.ToString();
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

                //MasterPartsLinkTablePriceList mpl = (MasterPartsLinkTablePriceList)row.DataItem;
                //HiddenField PartLinkTablePriceListID = (HiddenField)row.FindControl("hdnMasterPartsLinkTablePriceListID");
                ImageButton bEdit = (ImageButton)row.FindControl("imgEditDetail");
                ImageButton bSave = (ImageButton)row.FindControl("imgEditDetailSave");
                ImageButton bCancel = (ImageButton)row.FindControl("imgEditDetailCancel");
                if (bEdit != null) { bEdit.Visible = false; }
                if (bSave != null) { bSave.Visible = true; }
                if (bCancel != null) { bCancel.Visible = true; }

                TextBox txtPrice = (TextBox)row.FindControl("txtPrice");
                TextBox txtUnitPurchasePrice = (TextBox)row.FindControl("txtUnitPurchasePrice");
                TextBox txtAveragePurchasePrice = (TextBox)row.FindControl("txtAveragePurchasePrice");

                if (txtPrice != null) { txtPrice.Visible = true; }
                if (txtUnitPurchasePrice != null) { txtUnitPurchasePrice.Visible = true; }
                if (txtAveragePurchasePrice != null) { txtAveragePurchasePrice.Visible = true; }

                Label lblPrice = (Label)row.FindControl("lblPrice");
                Label lblAveragePurchasePrice = (Label)row.FindControl("lblAveragePurchasePrice");
                Label lblUnitPurchasePrice = (Label)row.FindControl("lblUnitPurchasePrice");

                if (lblPrice != null) { lblPrice.Visible = false; }
                if (lblAveragePurchasePrice != null) { lblAveragePurchasePrice.Visible = false; }
                if (lblUnitPurchasePrice != null) { lblUnitPurchasePrice.Visible = false; }
            }
            #endregion
            #region Save
            if (btn.ID.ToUpper() == "IMGEDITDETAILSAVE")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridViewDetail.Rows[index];

                //MasterPartsLinkTablePriceList mpl = (MasterPartsLinkTablePriceList)row.DataItem;
                HiddenField hdnReceiveDetailItemID = (HiddenField)row.FindControl("hdnReceiveDetailItemID");
                HiddenField hdnReceiveDetailPartsUsageID = (HiddenField)row.FindControl("hdnReceiveDetailPartsUsageID");

                ImageButton bEdit = (ImageButton)row.FindControl("imgEditDetail");
                ImageButton bSave = (ImageButton)row.FindControl("imgEditDetailSave");
                ImageButton bCancel = (ImageButton)row.FindControl("imgEditDetailCancel");
                if (bEdit != null) { bEdit.Visible = (User.IsInRole(Role_EditAccess) || User.IsInRole("Admin") == true); }
                if (bSave != null) { bSave.Visible = false; }
                if (bCancel != null) { bCancel.Visible = false; }


                TextBox txtPrice = (TextBox)row.FindControl("txtPrice");
                TextBox txtAveragePurchasePrice = (TextBox)row.FindControl("txtAveragePurchasePrice");
                TextBox txtUnitPurchasePrice = (TextBox)row.FindControl("txtUnitPurchasePrice");
                //TextBox txtReason = (TextBox)row.FindControl("txtAdjustmentReason");
                if (txtPrice != null) { txtPrice.Visible = false; }
                if (txtAveragePurchasePrice != null) { txtAveragePurchasePrice.Visible = false; }
                if (txtUnitPurchasePrice != null) { txtUnitPurchasePrice.Visible = false; }

                Label lblPrice = (Label)row.FindControl("lblPrice");
                Label lblAveragePurchasePrice = (Label)row.FindControl("lblAveragePurchasePrice");
                Label lblUnitPurchasePrice = (Label)row.FindControl("lblUnitPurchasePrice");

                if (lblPrice != null) { lblPrice.Visible = true; }
                if (lblAveragePurchasePrice != null) { lblAveragePurchasePrice.Visible = true; }
                if (lblUnitPurchasePrice != null) { lblUnitPurchasePrice.Visible = true; }

                decimal ReceiveDetailPartsUsageID = -1;
                decimal Price = 0;
                decimal AveragePurchasePrice = 0;
                decimal PurchasePrice = 0;

                if (hdnReceiveDetailPartsUsageID != null) { if (decimal.TryParse(hdnReceiveDetailPartsUsageID.Value, out ReceiveDetailPartsUsageID) == false) { ReceiveDetailPartsUsageID = -1; } }
                if (txtPrice != null) { if (decimal.TryParse(txtPrice.Text, out Price) == false) { Price = 0; } }
                if (txtAveragePurchasePrice != null) { if (decimal.TryParse(txtAveragePurchasePrice.Text, out AveragePurchasePrice) == false) { AveragePurchasePrice = 0; } }
                if (txtUnitPurchasePrice != null) { if (decimal.TryParse(txtUnitPurchasePrice.Text, out PurchasePrice) == false) { PurchasePrice = 0; } }

                if (txtPrice != null && lblPrice != null) { lblPrice.Text = txtPrice.Text; }
                if (txtAveragePurchasePrice != null && lblAveragePurchasePrice != null) { lblAveragePurchasePrice.Text = txtAveragePurchasePrice.Text; }
                if (txtUnitPurchasePrice != null && lblUnitPurchasePrice != null) { lblUnitPurchasePrice.Text = txtUnitPurchasePrice.Text; }
                ReceiveDetailManager rdm = new ReceiveDetailManager(User.Identity.Name);
                rdm.UpdateUnitPartPricingDetail(ReceiveDetailPartsUsageID, Price, AveragePurchasePrice, PurchasePrice);
            }
            #endregion
            #region Cancel
            if (btn.ID.ToUpper() == "IMGEDITDETAILCANCEL")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridViewDetail.Rows[index];

                //MasterPartsLinkTablePriceList mpl = (MasterPartsLinkTablePriceList)row.DataItem;
                //HiddenField PartLinkTablePriceListID = (HiddenField)row.FindControl("hdnMasterPartsLinkTablePriceListID");
                ImageButton bEdit = (ImageButton)row.FindControl("imgEditDetail");
                ImageButton bSave = (ImageButton)row.FindControl("imgEditDetailSave");
                ImageButton bCancel = (ImageButton)row.FindControl("imgEditDetailCancel");
                if (bEdit != null) { bEdit.Visible = (User.IsInRole(Role_EditAccess) || User.IsInRole("Admin") == true); }
                if (bSave != null) { bSave.Visible = false; }
                if (bCancel != null) { bCancel.Visible = false; }

                TextBox txtPrice = (TextBox)row.FindControl("txtPrice");
                TextBox txtAveragePurchasePrice = (TextBox)row.FindControl("txtAveragePurchasePrice");
                TextBox txtUnitPurchasePrice = (TextBox)row.FindControl("txtUnitPurchasePrice");


                if (txtPrice != null) { txtPrice.Visible = false; }
                if (txtAveragePurchasePrice != null) { txtAveragePurchasePrice.Visible = false; }
                if (txtUnitPurchasePrice != null) { txtUnitPurchasePrice.Visible = false; }

                Label lblPrice = (Label)row.FindControl("lblPrice");
                Label lblAveragePurchasePrice = (Label)row.FindControl("lblAveragePurchasePrice");
                Label lblUnitPurchasePrice = (Label)row.FindControl("lblUnitPurchasePrice");

                if (lblPrice != null) { lblPrice.Visible = true; }
                if (lblAveragePurchasePrice != null) { lblAveragePurchasePrice.Visible = true; }
                if (lblUnitPurchasePrice != null) { lblUnitPurchasePrice.Visible = true; }

                if (txtPrice != null && lblPrice != null) { txtPrice.Text = lblPrice.Text; }
                if (txtAveragePurchasePrice != null && lblAveragePurchasePrice != null) { txtAveragePurchasePrice.Text = lblAveragePurchasePrice.Text; }
                if (txtUnitPurchasePrice != null && lblUnitPurchasePrice != null) { txtUnitPurchasePrice.Text = lblUnitPurchasePrice.Text; }
            }
            #endregion
        }


        //void Table_SelectedRecordsChanged(object sender, SelectedRecordsChangedEventArgs e)
        //{
        //    if (e.SelectedRecord.Record != null)
        //    {
        //        ReceiveDetailEditPartsData rec = (ReceiveDetailEditPartsData)e.SelectedRecord.Record.GetData();
        //        decimal KeyID = rec.ReceiveDetailItemID;
        //        clsLinqDataContext ctx = new clsLinqDataContext();
        //        //ReceiveDetailESNMessage qu = ctx.ReceiveDetailESNMessages.FirstOrDefault(x => x.ReceiveDetailESNMessageID == KeyID);
        //        //if (qu != null)
        //        //{
        //        //    EditKeyID.Text = qu.ReceiveDetailESNMessageID.ToString();
        //        //    EditStatusOpen.Checked = (qu.StatusOpen == 1 ? true : false);
        //        //    EditStatusStop.Checked = (qu.StatusStop == 1 ? true : false);
        //        //    EditESN.Text = qu.ESN;
        //        //    EditMessage.Text = qu.Message;
        //        //}
        //        btnEdit.Visible = true;
        //    }
        //    else
        //    {
        //        btnEdit.Visible = false;
        //    }
        //}

        //void CurrentTable_SelectedRecordsChanged(object sender, SelectedRecordsChangedEventArgs e)
        //{
        //    if (e.SelectedRecord.Record != null)
        //    {
        //        ReceiveDetailEditPartsData rec = (ReceiveDetailEditPartsData)e.SelectedRecord.Record.GetData();
        //        decimal KeyID = rec.ReceiveDetailItemID;
        //        clsLinqDataContext ctx = new clsLinqDataContext();
        //        //ReceiveDetailESNMessage qu = ctx.ReceiveDetailESNMessages.FirstOrDefault(x => x.ReceiveDetailESNMessageID == KeyID);
        //        //if (qu != null)
        //        //{
        //        //    EditKeyID.Text = qu.ReceiveDetailESNMessageID.ToString();
        //        //    EditStatusOpen.Checked = (qu.StatusOpen == 1 ? true : false);
        //        //    EditStatusStop.Checked = (qu.StatusStop == 1 ? true : false);
        //        //    EditESN.Text = qu.ESN;
        //        //    EditMessage.Text = qu.Message;
        //        //}
        //        btnEdit.Visible = true;
        //    }
        //    else
        //    {
        //        btnEdit.Visible = false;
        //    }
        //}
        //void MainGrid_B_CurrentRecordContextChange(object sender, CurrentRecordContextChangeEventArgs e)
        //{
        //    if (e.Record != null)
        //    {
        //        ReceiveDetailEditPartsData rec = (ReceiveDetailEditPartsData)e.Record.GetData();
        //        decimal KeyID = rec.ReceiveDetailItemID;
        //        clsLinqDataContext ctx = new clsLinqDataContext();
        //        //var data = ctx.GetReceiveDetail_Parts(
        //        //ReceiveDetailESNMessage qu = ctx.ReceiveDetailESNMessages.FirstOrDefault(x => x.ReceiveDetailESNMessageID == KeyID);
        //        //if (qu != null)
        //        //{
        //        //    EditKeyID.Text = qu.ReceiveDetailESNMessageID.ToString();
        //        //    EditStatusOpen.Checked = (qu.StatusOpen == 1 ? true : false);
        //        //    EditStatusStop.Checked = (qu.StatusStop == 1 ? true : false);
        //        //    EditESN.Text = qu.ESN;
        //        //    EditMessage.Text = qu.Message;
        //        //}
        //        btnEdit.Visible = true;
        //    }
        //    else
        //    {
        //        btnEdit.Visible = false;

        //    }
        //}


        protected void UpdateMainGrid()
        {
            decimal id = -1;
            if (decimal.TryParse(hdnReceiveDetailID.Value, out id) == false) { id = -1; }
            clsLinqDataContext ctx = new clsLinqDataContext();
            List<ReceiveDetailEditPartsData> d = (from x in ctx.GetReceiveDetail_Parts(id,-1)
                                                  select new ReceiveDetailEditPartsData(x.ReceiveDetailID,x.ReceiveDetailItemID
                                                      ,x.ReceiveDetailPartsUsageID, x.MasterPartsLinkTableID, x.MasterPartsLinkTablePriceListID, x.PartNumberBucketInventoryPlacementID
                                                      ,x.StatusID, x.CreateDate_AddedToUnit, x.CreateUser_AddedToUnit, x.LastUpdateDateUnit, x.LastUpdateUser
                                                      ,x.PartNumber, x.Description, x.GMPPartDescription, x.GMPPartNumber, x.Quantity, x.UnitPrice, x.AveragePurchasePrice, x.UnitPurchasePrice
                                                      ,x.CreateDate, x.CreateUser, x.LastUpdateDate, x.LastUpdateUser
                                                  )).ToList();
            GridViewDetail.DataSource = d;
            GridViewDetail.DataBind();
            //if (d.Count > 0)
            //{
            //    btnEdit.Visible = true;
            //}
        }

        //private void SetLastSelectedRecord()
        //{

        //    if (EditKeyID.Text.Length > 0)
        //    {
        //        decimal KeyID = decimal.Parse(EditKeyID.Text);
        //        foreach (Record x in MainGrid_B.CurrentTable.Records)
        //        {
        //            ReceiveDetailEditPartsData r = (ReceiveDetailEditPartsData)x.GetData();
        //            if (r.ReceiveDetailItemID == KeyID)
        //            {
        //                MainGrid_B.CurrentTable.CurrentRecord = x;
        //            }
        //        }
        //    }
        //}




    }

    [Serializable()]
    public class ReceiveDetailEditPartsData
    {
        public decimal ReceiveDetailID { get; set; }
        public decimal ReceiveDetailItemID { get; set; }
        public decimal ReceiveDetailPartsUsageID { get; set; }
        public decimal MasterPartsLinkTableID { get; set; }
        public decimal MasterPartsLinkTablePriceListID { get; set; }
        public decimal PartNumberBucketInventoryPlacementID { get; set; }
        public decimal StatusID { get; set; }
        public DateTime CreateDate_AddedToUnit { get; set; }
        public string CreateUser_AddedToUnit { get; set; }
        public DateTime LastUpdateDateUnit { get; set; }
        public string LastUpdateUserUnit { get; set; }
        public string PartNumber { get; set; }
        public string Description { get; set; }
        public string GMPPartNumber { get; set; }
        public string GMPPartDescription { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal AveragePurchasePrice { get; set; }
        public decimal UnitPurchasePrice { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateUser { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public string LastUpdateUser { get; set; }


        public ReceiveDetailEditPartsData(decimal receiveDetailID,decimal receiveDetailItemID,decimal receiveDetailPartsUsageID,
               decimal masterPartsLinkTableID,decimal? masterPartsLinkTablePriceListID,decimal? partNumberBucketInventoryPlacementID,decimal? statusID,
               DateTime createDate_AddedToUnit, string createUser_AddedToUnit, DateTime lastUpdateDateUnit, string lastUpdateUserUnit,
               string partNumber, string description, string gMPPartNumber, string gMPPartDescription,
               decimal quantity, decimal unitPrice, decimal? averagePurchasePrice, decimal? unitPurchasePrice,
               DateTime createdate, string createuser, DateTime lastUpdateDate, string lastUpdateUser)
        {
            ReceiveDetailID = receiveDetailID;
            ReceiveDetailItemID = receiveDetailItemID;
            ReceiveDetailPartsUsageID = receiveDetailPartsUsageID;
            MasterPartsLinkTableID = masterPartsLinkTableID;

            MasterPartsLinkTablePriceListID = masterPartsLinkTablePriceListID == null ? -1 : (decimal)masterPartsLinkTablePriceListID;
            PartNumberBucketInventoryPlacementID = partNumberBucketInventoryPlacementID == null ? -1 : (decimal)partNumberBucketInventoryPlacementID;
            StatusID = statusID == null ? -1 : (decimal)statusID;

            CreateDate_AddedToUnit = createDate_AddedToUnit;
            CreateUser_AddedToUnit = createUser_AddedToUnit;
            LastUpdateDateUnit = lastUpdateDateUnit;
            LastUpdateUserUnit = lastUpdateUserUnit;

            PartNumber = partNumber;
            Description = description;
            GMPPartNumber = gMPPartNumber;
            GMPPartDescription = gMPPartDescription;
            Quantity = 1;            // quantity;
            UnitPrice = unitPrice;

            AveragePurchasePrice = averagePurchasePrice == null ? 0 : (decimal)averagePurchasePrice;
            UnitPurchasePrice = unitPurchasePrice == null ? 0 : (decimal)unitPurchasePrice;

            CreateDate = createdate;
            CreateUser = createuser;
            LastUpdateDate = lastUpdateDate;
            LastUpdateUser = lastUpdateUser;
        }
    }

}