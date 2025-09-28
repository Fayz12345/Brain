using System;
using System.Collections.Generic;
using System.Data.Linq;
//using System.Web.Script.Serialization;            // Used to generate json for back and forth
using System.IO;
using System.Xml.Serialization;
//using Factory_DataModel;
using BW_WebApp.DataManagers;
// using DAL;


namespace BW_WebApp.BishopIntegration
{

    [Serializable]
    public class MasterCatalogue
    {
        public MasterCatalogue()
        {
        }
    }



    #region GetMethods
    [Serializable]
    public class GetMasterCatalogue
    {
        string template = "";
        List<BishopCatalogueSLog> catalogue = new List<BishopCatalogueSLog>();
        public GetMasterCatalogue()
        {
            GatherList();
        }
        public GetMasterCatalogue(string CommaSeperatedList)                  // This is a wild card partlial look see. (NOTING IS UPDATED ETC.)
        {
            template = CommaSeperatedList;
            GatherList();
            //using (clsLinqDataContext ctx = new clsLinqDataContext())
            //{
            //    var xxx = ctx.GetDeviceCataloguePartial(CommaSeperatedList);
            //    foreach (BishopCatalogueSendLog x in xxx)
            //    {
            //        catalogue.Add(new BishopCatalogueSLog(x.SKU, x.Qty, x.LastOnHandQTY, x.DifferenceQty, x.Allocated, x.ThisSendDate, x.LastSendDate));
            //    }
            //}
        }

        private void GatherList()
        {
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                ISingleResult<BishopCatalogueSendLog> xxx = null;
                if (template.Length == 0) { xxx = ctx.GetDeviceCatalogue(); }
                else { xxx = ctx.GetDeviceCatalogueChangedFilter(template); }
                foreach (BishopCatalogueSendLog x in xxx)
                {
                    catalogue.Add(new BishopCatalogueSLog(x.SKU, x.Qty, x.LastOnHandQTY, x.DifferenceQty, x.Allocated, x.ThisSendDate, x.LastSendDate, x.Price));
                }
            }
        }
        public string XMLData()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<BishopCatalogueSendLog>));
            using (StringWriter textwriter = new StringWriter())
            {
                serializer.Serialize(textwriter, catalogue);
                return textwriter.ToString();
            }
        }
        public string JSONData()
        {
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            return serializer.Serialize(catalogue);
        }
    }


    [Serializable]
    public class BishopCatalogueSLog
    {
        public DateTime Datetime { get; set; }
        public DateTime LastSendDate { get; set; }
        public string SKU { get; set; }
        public int QTY { get; set; }
        public int LastOnHandQTY { get; set; }
        public int DifferenceQTY { get; set; }
        public int Allocated { get; set; }
        public decimal Price { get; set; }


        public BishopCatalogueSLog()
        {
            SKU = "";
            QTY = 0;
            LastOnHandQTY = 0;
            DifferenceQTY = 0;
            Datetime = DateTime.Now;
            LastSendDate = DateTime.Now;
            Allocated = 0;
            Price = 0;
        }

        public BishopCatalogueSLog(string sku, int? qty, int? lastonhandQty, int? differenceQty, int? allocated, DateTime? thissenddate, DateTime? lastsenddate, decimal? price)
        {
            SKU = sku;
            QTY = qty ?? 0; ;
            Allocated = allocated ?? 0;
            LastOnHandQTY = lastonhandQty ?? 0;
            DifferenceQTY = differenceQty ?? 0;
            Datetime = thissenddate ?? DateTime.Now;
            LastSendDate = lastsenddate ?? DateTime.Now;
            Price = price ?? 0;
        }
    }

    #region BishopCatalogueSlim

    [Serializable]
    public class GetMasterCatalogueSlim
    {
        string template = "";
        List<BishopCatalogueSlim> catalogue = new List<BishopCatalogueSlim>();
        public GetMasterCatalogueSlim()
        {
            GatherList();
        }
        public GetMasterCatalogueSlim(string CommaSeperatedList)                  // This is a wild card partlial look see. (NOTING IS UPDATED ETC.)
        {
            template = CommaSeperatedList;
            GatherList();
            //using (clsLinqDataContext ctx = new clsLinqDataContext())
            //{
            //    var xxx = ctx.GetDeviceCataloguePartial(CommaSeperatedList);
            //    foreach (BishopCatalogueSendLog x in xxx)
            //    {
            //        catalogue.Add(new BishopCatalogueSLog(x.SKU, x.Qty, x.LastOnHandQTY, x.DifferenceQty, x.Allocated, x.ThisSendDate, x.LastSendDate));
            //    }
            //}
        }

        private void GatherList()
        {
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                ISingleResult<BishopCatalogueSendLog> xxx = null;
                if (template.Length == 0) { xxx = ctx.GetDeviceCatalogue(); }
                else { xxx = ctx.GetDeviceCatalogueChangedFilter(template); }
                foreach (BishopCatalogueSendLog x in xxx) { catalogue.Add(new BishopCatalogueSlim(x.SKU, x.Qty, x.LastOnHandQTY, x.DifferenceQty, x.Allocated, x.ThisSendDate, x.LastSendDate, x.Price)); }
            }
        }
        public string XMLData()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<BishopCatalogueSlim>));
            using (StringWriter textwriter = new StringWriter())
            {
                serializer.Serialize(textwriter, catalogue);
                return textwriter.ToString();
            }
        }
        public string JSONData()
        {
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            return serializer.Serialize(catalogue);
        }
    }
    [Serializable]
    public class BishopCatalogueSlim
    {
        public DateTime Datetime { get; set; }
        //public DateTime LastSendDate { get; set; }
        public string SKU { get; set; }
        public int QTY { get; set; }
        //public int LastOnHandQTY { get; set; }
        //public int DifferenceQTY { get; set; }
        //public int Allocated { get; set; }
        public decimal Price { get; set; }


        public BishopCatalogueSlim()
        {
            SKU = "";
            QTY = 0;
            //LastOnHandQTY = 0;
            //DifferenceQTY = 0;
            Datetime = DateTime.Now;
            //LastSendDate = DateTime.Now;
            //Allocated = 0;
            Price = 0;
        }

        public BishopCatalogueSlim(string sku, int? qty, int? lastonhandQty, int? differenceQty, int? allocated, DateTime? thissenddate, DateTime? lastsenddate, decimal? price)
        {
            SKU = sku;
            QTY = qty ?? 0; ;
            //Allocated = allocated ?? 0;
            //LastOnHandQTY = lastonhandQty ?? 0;
            //DifferenceQTY = differenceQty ?? 0;
            Datetime = thissenddate ?? DateTime.Now;
            //LastSendDate = lastsenddate ?? DateTime.Now;
            Price = price ?? 0;
        }
        public BishopCatalogueSlim(BishopCatalogueSLog Data)
        {
            SKU = Data.SKU;
            QTY = Data.QTY;
            //Allocated = Data.Allocated;
            //LastOnHandQTY = Data.LastOnHandQTY;
            //DifferenceQTY = Data.DifferenceQTY;
            Datetime = Data.Datetime;
            //LastSendDate = Data.LastSendDate;
            Price = Data.Price;
        }

    }
    #endregion

    //[Serializable]
    //public class GetPartialCatalogue
    //{
    //    List<BishopCatalogueSendLog> catalogue = new List<BishopCatalogueSendLog>();

    //    public GetPartialCatalogue()
    //    {
    //        using (clsLinqDataContext ctx = new clsLinqDataContext())
    //        {
    //            var xxx = ctx.GetDeviceCatalogue();
    //            foreach (var x in xxx)
    //            {
    //                catalogue.Add(new BishopCatalogueSendLog(x.SKU, x.Qty, x.Allocated));
    //            }
    //        }
    //    }

    //    public string XMLData()
    //    {
    //        XmlSerializer serializer = new XmlSerializer(typeof(BishopCatalogueSendLog));
    //        using (StringWriter textwriter = new StringWriter())
    //        {
    //            serializer.Serialize(textwriter, catalogue);
    //            return textwriter.ToString();
    //        }
    //    }

    //    public string JSONData()
    //    {
    //        var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
    //        return serializer.Serialize(catalogue);
    //    }

    //}
    #endregion

    #region PickListSold
    [Serializable]
    public class PickListSold
    {
        public int ID { get; set; }
        public string Status { get; set; }
        public string OrderNumber { get; set; }
        public string OtherOrderNumber { get; set; }
        public string CustomerNumber { get; set; }
        public string BishopCreateDate { get; set; }
        public string BishopWantedDeliveryDate { get; set; }
        public string CustomerPO { get; set; }
        public string CustomerRef { get; set; }
        public string LabelNote { get; set; }
        public int QTY { get; set; }
        public int QTYShipped { get; set; }
        public string RequestUser { get; set; }
        public string WaybillNumber { get; set; }
        public string Carrier { get; set; }



        public PickListAddress ClientAddress { get; set; }
        public PickListAddress ShipToAddress { get; set; }
        public List<PickListDetailSold> Detail { get; set; }

        public PickListSold()
        {

            List<string> SkuList = new List<string>();
            SkuList.Add("SAM-ATT-      G920A-     -BLK- -d-  - -           ");
            SkuList.Add("SAM-BEL-       A500-     -BLK- -d-  - -           ");
            SkuList.Add("SAM-BEL-       C414-     -BLK- -d-  - -           ");
            SkuList.Add("SAM-BEL-       I757-     -BLK- -C-  - -           ");
            SkuList.Add("SAM-BEL-       T669-     -BLK- -A-  - -           ");
            SkuList.Add("SAM-BEL-       T746-     -BLK- -d-  - -           ");

            List<int> SkuQTY = new List<int>();
            SkuQTY.Add(6);
            SkuQTY.Add(1);
            SkuQTY.Add(1);
            SkuQTY.Add(2);
            SkuQTY.Add(5);


            ID = 1;
            Status = "Shipped";
            OrderNumber = "20 Char";
            OtherOrderNumber = "20 Char";
            CustomerNumber = "10 Char";
            BishopCreateDate = "MM/DD/YYYY";
            BishopWantedDeliveryDate = "MM/DD/YYYY";
            CustomerPO = "20 Char";
            CustomerRef = "50 Char";
            LabelNote = "2000 Char";
            QTY = 15;
            QTYShipped = 6;
            RequestUser = "50 Char";
            WaybillNumber = "XSDFV99382828";
            Carrier = "Fedex";

            ClientAddress = new PickListAddress();
            ShipToAddress = new PickListAddress();
            Detail = new List<PickListDetailSold>();
            int buffer = 0;
            int q = 1;
            for (int i = 1; i < 6; i++)
            {
                q = 1;
                if (i == 4) { q = 2; }
                PickListDetailSold d = new PickListDetailSold(i, SkuQTY[i - 1], q, SkuList[i - 1]);
                d.AddIMEI(string.Format("1234567890123{0}", i + buffer));
                Detail.Add(d);
                if (i == 4)
                {
                    buffer = buffer + 1;
                    //PickListDetail e = new PickListDetail(i, i, string.Format("SKU {0} HERE", i));
                    d.AddIMEI(string.Format("3210987654321{0}", i + buffer));
                    //Detail.Add(e);
                }
            }
        }
        public PickListSold(PickList p, int quantityShipped, string waybillnumber, string carrier)
        {
            ID = p.ID;
            Status = p.Status;
            OrderNumber = p.OrderNumber;
            OtherOrderNumber = p.OtherOrderNumber;
            CustomerNumber = p.CustomerNumber;
            BishopCreateDate = p.BishopCreateDate;
            BishopWantedDeliveryDate = p.BishopWantedDeliveryDate;
            CustomerPO = p.CustomerPO;
            CustomerRef = CustomerRef;
            LabelNote = p.LabelNote;
            QTY = p.QTY;
            QTYShipped = quantityShipped;
            RequestUser = p.RequestUser;
            WaybillNumber = waybillnumber;
            Carrier = carrier;
            ClientAddress = p.ClientAddress;
            ShipToAddress = p.ShipToAddress;
            Detail = ToSoldDetail(p.Detail);
        }
        public string XMLData()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(PickListSold));
            using (StringWriter textwriter = new StringWriter())
            {
                serializer.Serialize(textwriter, this);
                return textwriter.ToString();
            }
        }
        public string JSONData()
        {
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            return serializer.Serialize(this);
        }
        public List<PickListDetailSold> ToSoldDetail(List<PickListDetail> d)
        {
            List<PickListDetailSold> ds = new List<PickListDetailSold>();
            foreach (PickListDetail a in d)
            {
                ds.Add(new PickListDetailSold(a));
            }
            return ds;
        }
    }
    #endregion
    #region PicklistSold
    //[Serializable]
    //public class PickListSold
    //{
    //    public int ID { get; set; }
    //    public string Status { get; set; }
    //    public string OrderNumber { get; set; }
    //    public string OtherOrderNumber { get; set; }
    //    public string CustomerNumber { get; set; }
    //    public string BishopCreateDate { get; set; }
    //    public string BishopWantedDeliveryDate { get; set; }
    //    public string CustomerPO { get; set; }
    //    public string CustomerRef { get; set; }
    //    public string LabelNote { get; set; }
    //    public int QTY { get; set; }
    //    public string RequestUser { get; set; }
    //    public string DoAction { get; set; }
    //    public string Pick_Cancel { get; set; }
    //    public PickListAddress ClientAddress { get; set; }
    //    public PickListAddress ShipToAddress { get; set; }
    //    public List<PickListDetail> Detail { get; set; }

    //    public PickListSold()
    //    {
    //        ID = 1;
    //        Status = "New";
    //        OrderNumber = "20 Char";
    //        OtherOrderNumber = "20 Char";
    //        CustomerNumber = "10 Char";
    //        BishopCreateDate = "MM/DD/YYYY";
    //        BishopWantedDeliveryDate = "MM/DD/YYYY";
    //        CustomerPO = "20 Char";
    //        CustomerRef = "50 Char";
    //        LabelNote = "2000 Char";
    //        QTY = 15;
    //        RequestUser = "50 Char";
    //        DoAction = "Add or Delete";
    //        Pick_Cancel = "N";

    //        ClientAddress = new PickListAddress();
    //        ShipToAddress = new PickListAddress();
    //        Detail = new List<PickListDetail>();
    //        Detail.Add(new PickListDetail(1, 6, "SKU 1 HERE"));
    //        Detail.Add(new PickListDetail(2, 1, "SKU 2 HERE"));
    //        Detail.Add(new PickListDetail(3, 1, "SKU 3 HERE"));
    //        Detail.Add(new PickListDetail(4, 2, "SKU 4 HERE"));
    //        Detail.Add(new PickListDetail(5, 5, "SKU 5 HERE"));
    //    }

    //    public PickListSold(PickList NewPickList)
    //    {
    //        ID = NewPickList.ID;
    //        Status = NewPickList.Status;
    //        OrderNumber = NewPickList.OrderNumber;
    //        OtherOrderNumber = NewPickList.OtherOrderNumber;
    //        CustomerNumber = NewPickList.CustomerNumber;
    //        BishopCreateDate = NewPickList.BishopCreateDate;
    //        BishopWantedDeliveryDate = NewPickList.BishopWantedDeliveryDate;
    //        CustomerPO = NewPickList.CustomerPO;
    //        CustomerRef = NewPickList.CustomerRef;
    //        LabelNote = NewPickList.LabelNote;
    //        QTY = NewPickList.QTY;
    //        RequestUser = NewPickList.RequestUser;
    //        DoAction = NewPickList.DoAction;
    //        Pick_Cancel = NewPickList.Pick_Cancel;
    //        ClientAddress = NewPickList.ClientAddress;
    //        ShipToAddress = NewPickList.ShipToAddress;
    //        Detail = NewPickList.Detail;

    //    }

    //    public string Save()
    //    {
    //        return "";
    //    }

    //    public string XMLData()
    //    {
    //        XmlSerializer serializer = new XmlSerializer(typeof(PickList));
    //        using (StringWriter textwriter = new StringWriter())
    //        {
    //            serializer.Serialize(textwriter, this);
    //            return textwriter.ToString();
    //        }
    //    }
    //    public string JSONData()
    //    {
    //        var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
    //        return serializer.Serialize(this);
    //    }

    //}
    [Serializable]
    public class PickListDetailSold
    {
        public int Line_No { get; set; }
        public int QTY { get; set; }
        public int QTYShipped { get; set; }
        public string SKU { get; set; }
        public List<string> IMEIList { get; set; }

        public PickListDetailSold()
        {
            Line_No = 0;
            QTY = 10;
            QTYShipped = 10;
            SKU = "50 Char";
            IMEIList = new List<string>();
        }

        public PickListDetailSold(int lineno, int qty, string sku)
        {
            Line_No = lineno;
            QTY = qty;
            QTYShipped = qty;
            SKU = sku;
            IMEIList = new List<string>();
        }

        public PickListDetailSold(int lineno, int qty, string sku, List<string> imeilist)
        {
            Line_No = lineno;
            QTY = qty;
            QTYShipped = qty;
            SKU = sku;
            IMEIList = new List<string>();
            IMEIList = imeilist;
        }

        public PickListDetailSold(int lineno, int qty, int qtysold, string sku, List<string> imeilist)
        {
            Line_No = lineno;
            QTY = qty;
            QTYShipped = qtysold;
            SKU = sku;
            IMEIList = new List<string>();
            IMEIList = imeilist;
        }

        public PickListDetailSold(int lineno, int qty, int qtysold, string sku)
        {
            Line_No = lineno;
            QTY = qty;
            QTYShipped = qtysold;
            SKU = sku;
            IMEIList = new List<string>();
        }


        public PickListDetailSold(PickListDetail d)
        {
            Line_No = d.Line_No;
            QTY = d.QTY;
            QTYShipped = d.QTY;
            SKU = d.SKU;
            IMEIList = new List<string>();
            IMEIList = d.IMEIList;
        }

        public void AddIMEI(string imei)
        {
            IMEIList.Add(imei);
        }
    }

    #endregion
    #region Picklist
    [Serializable]
    public class PickList
    {
        public int ID { get; set; }
        public string Status { get; set; }
        public string OrderNumber { get; set; }
        public string OtherOrderNumber { get; set; }
        public string CustomerNumber { get; set; }
        public string BishopCreateDate { get; set; }
        public string BishopWantedDeliveryDate { get; set; }
        public string CustomerPO { get; set; }
        public string CustomerRef { get; set; }
        public string LabelNote { get; set; }
        public int QTY { get; set; }
        public string RequestUser { get; set; }
        public string DoAction { get; set; }
        public string Pick_Cancel { get; set; }
        public PickListAddress ClientAddress { get; set; }
        public PickListAddress ShipToAddress { get; set; }
        public List<PickListDetail> Detail { get; set; }

        public PickList()
        {
            List<string> SkuList = new List<string>();
            SkuList.Add("SAM-ATT-      G920A-     -BLK- -d-  - -           ");
            SkuList.Add("SAM-BEL-       A500-     -BLK- -d-  - -           ");
            SkuList.Add("SAM-BEL-       C414-     -BLK- -d-  - -           ");
            SkuList.Add("SAM-BEL-       I757-     -BLK- -C-  - -           ");
            SkuList.Add("SAM-BEL-       T669-     -BLK- -A-  - -           ");
            SkuList.Add("SAM-BEL-       T746-     -BLK- -d-  - -           ");

            ID = 1;
            Status = "New";
            OrderNumber = "20 Char";
            OtherOrderNumber = "20 Char";
            CustomerNumber = "10 Char";
            BishopCreateDate = "MM/DD/YYYY";
            BishopWantedDeliveryDate = "MM/DD/YYYY";
            CustomerPO = "20 Char";
            CustomerRef = "50 Char";
            LabelNote = "2000 Char";
            QTY = 15;
            RequestUser = "50 Char";
            DoAction = "Add or Delete";
            Pick_Cancel = "N";

            ClientAddress = new PickListAddress();
            ShipToAddress = new PickListAddress();
            Detail = new List<PickListDetail>();
            Detail.Add(new PickListDetail(1, 6, SkuList[0]));
            Detail.Add(new PickListDetail(2, 1, SkuList[1]));
            Detail.Add(new PickListDetail(3, 1, SkuList[2]));
            Detail.Add(new PickListDetail(4, 2, SkuList[3]));
            Detail.Add(new PickListDetail(5, 5, SkuList[4]));
        }

        public PickList(PickList NewPickList)
        {
            ID = NewPickList.ID;
            Status = NewPickList.Status;
            OrderNumber = NewPickList.OrderNumber;
            OtherOrderNumber = NewPickList.OtherOrderNumber;
            CustomerNumber = NewPickList.CustomerNumber;
            BishopCreateDate = NewPickList.BishopCreateDate;
            BishopWantedDeliveryDate = NewPickList.BishopWantedDeliveryDate;
            CustomerPO = NewPickList.CustomerPO;
            CustomerRef = NewPickList.CustomerRef;
            LabelNote = NewPickList.LabelNote;
            QTY = NewPickList.QTY;
            RequestUser = NewPickList.RequestUser;
            DoAction = NewPickList.DoAction;
            Pick_Cancel = NewPickList.Pick_Cancel;
            ClientAddress = NewPickList.ClientAddress;
            ShipToAddress = NewPickList.ShipToAddress;
            Detail = NewPickList.Detail;

        }

        public string Save()
        {
            return "";
        }

        public string XMLData()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(PickList));
            using (StringWriter textwriter = new StringWriter())
            {
                serializer.Serialize(textwriter, this);
                return textwriter.ToString();
            }
        }
        public string JSONData()
        {
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            return serializer.Serialize(this);
        }

    }
    [Serializable]
    public class PickListDetail
    {
        public int Line_No { get; set; }
        public int QTY { get; set; }
        public string SKU { get; set; }
        public List<string> IMEIList { get; set; }

        public PickListDetail()
        {
            Line_No = 0;
            QTY = 10;
            SKU = "50 Char";
            IMEIList = new List<string>();
        }

        public PickListDetail(int lineno, int qty, string sku)
        {
            Line_No = lineno;
            QTY = qty;
            SKU = sku;
            IMEIList = new List<string>();
        }

        public PickListDetail(int lineno, int qty, string sku, List<string> imeilist)
        {
            Line_No = lineno;
            QTY = qty;
            SKU = sku;
            IMEIList = new List<string>();
            IMEIList = imeilist;
        }

        public void AddIMEI(string imei)
        {
            IMEIList.Add(imei);
        }
    }
    [Serializable]
    public class PickListAddress
    {
        public string CustomerName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string StateProvince { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
        public string FaxNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Notes { get; set; }

        public PickListAddress()
        {
            CustomerName = "50 Char";
            AddressLine1 = "50 Char";
            AddressLine2 = "50 Char";
            City = "50 Char";
            PostalCode = "20 Char";
            StateProvince = "20 Char";
            Country = "20 Char";
            PhoneNumber = "30 Char";
            FaxNumber = "30 Char";
            EmailAddress = "50 Char";
            Notes = "500 Char";
        }
    }
    #endregion

}