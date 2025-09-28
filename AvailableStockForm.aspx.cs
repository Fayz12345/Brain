using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
//using Factory_DataModel;
using BW_WebApp.DataManagers;

namespace BW_WebApp
{
    public partial class AvailableStockForm : System.Web.UI.Page
    {

        clsLinqDataContext ctx = null;


        protected void Page_Load(object sender, EventArgs e)
        {
            //string ReportName = Request.QueryString.Get("Order");
            grdPickList.RowDataBound += new GridViewRowEventHandler(grdPickList_RowDataBound);
            grdPickList.Visible = false;
            SetData();
        }

        void grdPickList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                OrderDetail rec = (OrderDetail)e.Row.DataItem;
                Label Picked = (Label)e.Row.FindControl("lblPicked");
                Label Packed = (Label)e.Row.FindControl("lblPacked");

                Picked.Text = rec.OrderDetailReceiveDetails.Count().ToString();
                Packed.Text = rec.OrderDetailReceiveDetails.Where(x => x.ReceiveDetailID != null).Count().ToString();
                if (rec.OrderDetailReceiveDetails.Any() == true)
                {
                    grdPickList.Visible = true;
                    CheckBoxList l = (CheckBoxList)e.Row.FindControl("chkESN");
                    l.DataValueField = "OrderDetailReceiveDetailID";
                    l.DataTextField = "ESN";
                    List<OrderDetailReceiveDetail> detail = rec.OrderDetailReceiveDetails.OrderBy(x => x.ESN).ToList();
                    foreach (OrderDetailReceiveDetail d in detail)
                    {
                        d.ESN += d.SKU != null && d.SKU.Length > 0 ? " (" + d.SKU + ")" : "";
                    }
                    l.DataSource = detail.OrderBy(x => x.ESN).ToList();
                    l.DataBind();
                }
            }
        }

        private void SetData()
        {
            string ReportName = Request.QueryString.Get("Order");
            if (ReportName == null || ReportName.Length == 0) { ReportName = "GMP000000083"; }
            using (ctx = new clsLinqDataContext())
            {
                OrderHeader Order = ctx.OrderHeaders.FirstOrDefault(x => x.OrderNumber == ReportName);
                if (Order != null)
                {
                    LoadOrderHeaderData(Order);
                    LoadPickListData(Order);
                }
                //var data = from x in ctx.ReservedAvailableStocks where x.AvailableStock_OrderNumber == ReportName orderby x.Manufacturer orderby x.Model orderby x.Colour orderby x.Grade orderby x.Carrier select x;
                LoadOrderDetail(ReportName);
            }
        }


        private void LoadOrderHeaderData(OrderHeader Order)
        {
            tProject.Text = Config.ProjectStockForSaleFrom;

            txtCustomerPONumber.ReadOnly = true;
            txtCustomerPONumber.ReadOnly = true;
            txtProjectTag.ReadOnly = true;
            txtBillClient.ReadOnly = true;
            txtInternalNote.ReadOnly = false;
            tProject.ReadOnly = true;
            PurchaseOrderNumber.ReadOnly = true;
            txtWaybillNumber.ReadOnly = true;
            txtDeliveryNote.ReadOnly = true;
            chkPaid.Disabled = true;
            //chkPostPaid.Disabled = true;
            //chkPostPaid.ReadOnly = ';
            txtShipClient.ReadOnly = true;
            txtShipNameAddresstext.ReadOnly = true;
            txtBillClient.ReadOnly = true;
            txtBillNameAddresstext.ReadOnly = true;
            txtOrderDate.ReadOnly = true;
            txtPickPackDate.ReadOnly = true;
            txtShipDate.ReadOnly = true;
            txtStatus.ReadOnly = true;

            txtCustomerPONumber.Text = Order.CustomerPO;
            txtProjectTag.Text = Order.ProjectTag;
            txtBillClient.Text = "Unknown";
            txtInternalNote.Text = Order.InternalNote;
            tProject.Text = Order.ProjectID.ToString();
            PurchaseOrderNumber.Text = Order.OrderNumber;
            txtWaybillNumber.Text = Order.WayBillNumber;
            txtDeliveryNote.Text = Order.DeliveryNote;
            chkPaid.Checked = false;
            //chkPostPaid.Checked = false;

            txtOrderDate.Text = Order.OrderDate.ToShortDateString();
            txtPickPackDate.Text = Order.PickPackDate.ToString();
            txtShipDate.Text = Order.ShippedDate.ToString();
            txtStatus.Text = Order.OrderStatus.Status;

            if (Order.Paid == true) { chkPaid.Checked = true; }
            //if (Order.PostPaid == true) { chkPostPaid.Checked = true; }

            OrderCompany Client = Order.OrderCompanies.FirstOrDefault(x => x.CompanyType == "Client");
            OrderCompany ShipTo = Order.OrderCompanies.FirstOrDefault(x => x.CompanyType == "ShipTo");
            if (ShipTo != null)
            {
                txtShipClient.Text = ShipTo.CompanyName;
                txtShipNameAddresstext.Text = GetAddressString(ShipTo);

            }
            if (Client != null)
            {
                txtBillClient.Text = Client.CompanyName;
                txtBillNameAddresstext.Text = GetAddressString(Client);
            }
        }
        private void LoadPickListData(OrderHeader Order)
        {
            grdPickList.DataSource = Order.OrderDetails.OrderBy(x => x.Desc_Text).ThenBy(x => x.QTY).ToList();
            grdPickList.DataBind();
        }


        private void LoadOrderDetail(string ReportName)
        {
            //var summary = from p in ctx.ReservedAvailableStocks
            //              where p.AvailableStock_OrderNumber == ReportName
            //              let k = new
            //              {
            //                  //try this if you need a date field 
            //                  //   p.SaleDate.Date.AddDays(-1 *p.SaleDate.Day - 1)
            //                  Manufacturer = p.Manufacturer,
            //                  Model = p.Model,
            //                  Colour = p.Colour,
            //                  Grade = p.Grade,
            //                  Carrier = p.Carrier,
            //                  AvailableStock_OrderNumber = p.AvailableStock_OrderNumber
            //              }
            //              group p by k into t
            //              select new
            //              {
            //                  Manufacturer = t.Key.Manufacturer,
            //                  Model = t.Key.Model,
            //                  Colour = t.Key.Colour,
            //                  Grade = t.Key.Grade,
            //                  Carrier = t.Key.Carrier,
            //                  AvailableStock_OrderNumber = t.Key.AvailableStock_OrderNumber,
            //                  Quantity = t.Sum(p => p.Quantity)
            //              };
            var summary = from p in ctx.OrderDetails
                          where p.OrderHeader.OrderNumber == ReportName
                          let k = new
                          {
                              //try this if you need a date field 
                              //   p.SaleDate.Date.AddDays(-1 *p.SaleDate.Day - 1)
                              PurchaseUnitPrice = p.PurchaseUnitPrice,
                              Manufacturer = p.Manufacturer,
                              Model = p.Model,
                              Colour = p.Colour,
                              Grade = p.Grade,
                              Carrier = p.Carrier,
                              AvailableStock_OrderNumber = p.OrderHeader.OrderNumber
                          }
                          group p by k into t
                          select new
                          {
                              PurchaseUnitPrice = t.Key.PurchaseUnitPrice,
                              Manufacturer = t.Key.Manufacturer,
                              Model = t.Key.Model,
                              Colour = t.Key.Colour,
                              Grade = t.Key.Grade,
                              Carrier = t.Key.Carrier,
                              AvailableStock_OrderNumber = t.Key.AvailableStock_OrderNumber,
                              Quantity = t.Sum(p => p.QTY)
                          };

            grdData.DataSource = summary.OrderBy(x => x.Manufacturer).ThenBy(x => x.Model).ThenBy(x => x.Colour).ThenBy(x => x.Grade).ThenBy(x => x.Carrier).ThenBy(x => x.Quantity).ToList();
            grdData.DataBind();
        }
        private string GetAddressString(OrderCompany Company)
        {
            string rString = "";
            rString = Company.CompanyName;
            if (Company.AddressLine1.Length > 0) { rString += Environment.NewLine; }
            rString += Company.AddressLine1;
            if (Company.AddressLine2.Length > 0) { rString += Environment.NewLine; }
            rString += Company.AddressLine2;
            if (Company.City.Length > 0) { rString += Environment.NewLine; }
            rString += Company.City;
            if (Company.StateOrProvince.Length > 0) { rString += Environment.NewLine; }
            rString += Company.StateOrProvince;
            if (Company.PostalCode.Length > 0) { rString += Environment.NewLine; }
            rString += Company.PostalCode;
            return rString;
        }





    }
}