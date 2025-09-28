using System;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Linq;
using System.Data.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.IO;

using BW_WebApp.Classes;

namespace BW_WebApp.DataManagers
{
    public class SalesOrderManager
    {
        string _UserName = string.Empty;
        public string[] UserRoles { get; set; }
        public BL_UserDataRestrictor UserRestrict = null;

        public clsLinqDataContext GetDataContext(string UserName)
        {
            clsLinqDataContext ctx = new clsLinqDataContext();
            ctx.UserName = UserName;
            return ctx;
        }
        public SalesOrderManager(string UserName)
        {
            _UserName = UserName;
            UserRoles = Roles.GetRolesForUser(UserName);
            if (_UserName.Trim().Length == 0)
            {
                _UserName = "NullUser";
            }
            UserRestrict = new BL_UserDataRestrictor(_UserName);
            //SM = new StatisticalManager(_UserName);
        }
        public String UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
        public string UserRoleString
        {
            get
            {
                string[] selectedusersRoles = Roles.GetRolesForUser(UserName);
                string sRoles = "";
                foreach (string r in selectedusersRoles)
                {
                    sRoles += r + ",";
                }
                return sRoles;
                // Returns a string link "Admin,Supervisor,Other"
            }
        }
    }






    #region OrderSection
    public class clsSOHeader
    {
        List<clsSODetailLine> _SODetailLines = new List<clsSODetailLine>();
        public clsSOHeaderCompany ClientCompany = new clsSOHeaderCompany();
        public clsSOHeaderCompany ShipToCompany = new clsSOHeaderCompany();
        public decimal SOHeaderID { get; internal set; }
        public decimal ProjectID { get; set; }
        public string OrderNumber { get; set; }
        public string IFSOrderNumber { get; set; }
        public string Site { get; set; }
        public string CustomerPO { get; set; }
        public string MiscReference { get; set; }
        public string WaybillNumber { get; set; }
        public string ProjectTag { get; set; }

        public bool Paid { get; set; }
        public bool PostPaid { get; set; }
        public string MiscDesc { get; set; }
        public string Courier { get; set; }

        public string Status { get; set; }

        public string InternalNote { get; set; }
        public string DeliveryNote { get; set; }

        public clsSODetailLine SODetailLine { set { _SODetailLines.Add(value); } }
        public List<clsSODetailLine> SODetailLines { get { return _SODetailLines; } }
        public string UserName { get; set; }

        public clsSOHeader(string Username)
        {
            UserName = Username;
        }
        public clsSOHeader(decimal HeaderID)
        {
            LoadHeaderData(HeaderID);
        }
        public clsSOHeader(clsLinqDataContext ctx, decimal HeaderID)
        {
            LoadHeaderData(ctx, HeaderID);
        }

        public void Refresh()
        {
            decimal HeaderID = SOHeaderID;
            _SODetailLines.Clear();
            ClientCompany = new clsSOHeaderCompany();
            ShipToCompany = new clsSOHeaderCompany();
            LoadHeaderData(SOHeaderID);
        }
        public void Refresh(clsLinqDataContext ctx)
        {
            decimal HeaderID = SOHeaderID;
            _SODetailLines.Clear();
            ClientCompany = new clsSOHeaderCompany();
            ShipToCompany = new clsSOHeaderCompany();
            LoadHeaderData(ctx, SOHeaderID);
        }

        private void LoadHeaderData(decimal HeaderID)
        {
            SOHeaderID = -1;
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                LoadHeaderData(ctx, HeaderID);
            }
        }
        private void LoadHeaderData(clsLinqDataContext ctx, decimal HeaderID)
        {
            SOHeaderID = -1;
            SOHeader OH = ctx.SOHeaders.FirstOrDefault(x => x.SOHeaderID == HeaderID);
            if (OH != null)
            {
                SOHeaderID = HeaderID;
                ProjectID = OH.ProjectID;
                OrderNumber = OH.OrderNumber;
                if (OH.MiscReference == null) { MiscReference = ""; }
                else { MiscReference = OH.MiscReference; }
                //IFSOrderNumber = OH.IFSOrderNo;
                Site = OH.Site;
                CustomerPO = OH.CustomerPO;
                //WaybillNumber = OH.WayBillNumber;
                ProjectTag = OH.ProjectTag;
                MiscDesc = OH.MiscDesc;
                InternalNote = OH.InternalNote;
                DeliveryNote = OH.DeliveryNote;
                //Status = OH.OrderStatus.Status;
                PostPaid = (OH.PostPaid == null || OH.PostPaid == false) ? false : true;
                Paid = (OH.Paid == null || OH.Paid == false) ? false : true;

            }
            else
            {
                SOHeaderID = -1;
                ProjectID = -1;
                OrderNumber = "";
                IFSOrderNumber = "";
                Site = "";
                CustomerPO = "";
                MiscReference = "";
                MiscDesc = "";
                WaybillNumber = "";
                InternalNote = "";
                DeliveryNote = "";
                Status = "";
                PostPaid = false;
                Paid = false;
            }
            ClientCompany = new clsSOHeaderCompany();
            ClientCompany.LoadCompany("Client", SOHeaderID);
            ShipToCompany = new clsSOHeaderCompany();
            ShipToCompany.LoadCompany("ShipTo", SOHeaderID);
            LoadLineDetailData(ctx);
        }

        private void LoadLineDetailData()
        {
            if (SOHeaderID < 1) { _SODetailLines.Clear(); return; }
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                LoadLineDetailData(ctx);
            }
        }
        private void LoadLineDetailData(clsLinqDataContext ctx)
        {
            if (SOHeaderID < 1) { _SODetailLines.Clear(); return; }
            var Data = from x in ctx.SODetails
                       where x.SOHeaderID == SOHeaderID
                       select x;
            foreach (SODetail od in Data)
            {
                clsSODetailLine OL = new clsSODetailLine();
                //OL.Desc_Code = od.Desc_Code;
                //OL.Desc_Text = od.Desc_Text;
                OL.isDirty = false;
                OL.isNew = false;
                OL.SODetailID = od.SODetailID;
                OL.QTY = (decimal)od.QTY;
                //if (od.QTYPacked == null) { OL.QTYPacked = 0; }
                //else { OL.QTYPacked = (decimal)od.QTYPacked; }
                //OL.QTYLeft = OL.QTY - OL.QTYPacked;
                //OL.SKU = od.SKU;                   // OL.SKU;
                decimal UP = 0;
                //if (od.PurchaseUnitPrice != null) { UP = (decimal)od.PurchaseUnitPrice; }
                OL.UnitPrice = UP;
                OL.Manufacturer = od.Manufacturer;
                OL.Model = od.Model;
                OL.Colour = od.Colour;
                OL.Carrier = od.Carrier;
                //OL.Grade = od.Grade;
                OL.Location = od.Location;
                OL.Condition = od.Condition;
                //OL.Project_ID = od.Project_ID;
                //OL.IFSSKU = od.IFSSku;
                if (od.Line_NO == null)
                {
                    OL.Line_No = 0;
                }
                else
                {
                    OL.Line_No = (short)od.Line_NO;
                }
                //if (od.ReservedAvailableStockID != null) { OL.ReservedAvailableStockID = (decimal)od.ReservedAvailableStockID; }
                //OL.AvailableStock_OrderNumber = od.AvailableStock_OrderNumber;
                //if (od.AvailableStock_QTY != null) { OL.AvailableStock_QTY = (int)od.AvailableStock_QTY; }
                OL.LoadReceiveDetail();
                SODetailLine = OL;
            }
        }

        public string PostData(bool Lock)
        {
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                return PostData(ctx, Lock);
            }
        }
        public string PostData(clsLinqDataContext ctx, bool Lock)
        {
            string msg = "Not Posted";
            if (SOHeaderID > 0)   // We have a new one to save.
            {
                PostData(ctx, msg, Lock);
                msg = "Saved";
            }
            return msg;
        }

        private string PostData(string msg, bool Lock)
        {
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                return PostData(ctx, msg, Lock);
            }
        }
        private string PostData(clsLinqDataContext ctx, string msg, bool Lock)
        {
            msg = "";
            bool doSave = false;
            foreach (clsSODetailLine dl in _SODetailLines)
            {
                foreach (clsSODetailLineReceive rl in dl._SODetailReceiveLines)
                {
                    if (rl.ReceiveDetailID == null || rl.ReceiveDetailID < 1)
                    {
                        doSave = PullSerializedIMEI(ctx, doSave, dl, rl, Lock);
                    }
                }
            }
            //if (doSave == true)
            //{
            string MSG = "";
            SaveCurrent(ctx, MSG);
            msg = "Posted";
            //}
            return msg;
        }
        private bool PullSerializedIMEI(clsLinqDataContext ctx, bool doSave, clsSODetailLine dl, clsSODetailLineReceive rl, bool Lock)
        {
            doSave = false;
            rl.Message = "xx:" + rl.ESN + ":";
            ReceiveDetail rd = ctx.ReceiveDetails.FirstOrDefault(x => x.ESN == rl.ESN && x.Version == "000" && x.ReceiveDetailStatus.Status.ToUpper() != "GRAVEYARD");         // && (ProjectID < 1 || x.ProjectID == ProjectID)
            if (rd != null)
            {
                if (ProjectID < 1 || rd.ProjectID == ProjectID)
                {
                    if (Lock == true)
                    {
                        if (dl.isAttributeLocked == true && dl.Desc_Code.Length > 0)
                        {
                            List<string> attributes = dl.Desc_Code.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                            // List<ReceiveDetailItem> rdList = rd.ReceiveDetailItems.Where(x => attributes.Contains(x.Option.ScanKey.ToUpper()) && x.Version == 0).ToList();
                            List<ReceiveDetailItem> rdList = rd.ReceiveDetailItems.Where(x => attributes.Contains(x.Option.ScanKey.ToUpper()) && x.Version == 0).ToList();
                            if (rdList.Count != attributes.Count)
                            {
                                // Remove all the Attributes that are found on the Item that are in the Lock list.
                                foreach (ReceiveDetailItem rdi in rdList)
                                {
                                    Option op = ctx.Options.FirstOrDefault(x => x.OptionID == rdi.OptionID);
                                    if (op != null)
                                    {
                                        attributes.Remove(op.ScanKey);
                                    }
                                }
                                rl.Message = "";
                                if (attributes.Count > 0)
                                {
                                    rl.Message = "Attribute Missmatched (";
                                    foreach (string s in attributes)
                                    {
                                        Option op = ctx.Options.FirstOrDefault(x => x.ScanKey == s);
                                        if (op != null)
                                        {
                                            rl.Message += op.Question.Name + " ";
                                        }
                                    }
                                    rl.Message += ")";
                                    return doSave;
                                }
                            }
                            // if rdlist.length == attributes.length (then everything matches, move forward)
                        }
                    }

                    SODetailReceiveDetail odrd = ctx.SODetailReceiveDetails.FirstOrDefault(x => x.ReceiveDetailID == rd.ReceiveDetailID);    // we want the check that one unit does not show up on more than one order.
                    if (odrd == null)
                    {
                        doSave = true;
                        if (ctx.IsOrderESNMatched(dl.SODetailID, rd.ReceiveDetailID) > 0) { rl.ReceiveDetailID = rd.ReceiveDetailID; rl.Message = ""; }
                        else { rl.Message = "ESN/Order Client Missmatched or missing!"; }
                    }
                    else
                    {
                        rl.Message = "Already on Order:" + odrd.SODetail.SOHeader.OrderNumber + " ID=" + odrd.SODetailID.ToString() + "!";
                    }
                }
                else { rl.Message = "Incorrect Project!"; }
            }
            else { rl.Message = "ESN version zero not found!"; }
            return doSave;
        }

        private string PostBulkData(string msg)
        {
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                return PostBulkData(ctx, msg);
            }

        }
        private string PostBulkData(clsLinqDataContext ctx, string msg)
        {
            msg = "";
            bool doSave = false;
            bool PullBulk = false;
            foreach (clsSODetailLine dl in _SODetailLines)
            {
                foreach (clsSODetailLineReceive rl in dl._SODetailReceiveLines)
                {
                    if (rl.ReceiveDetailID == null || rl.ReceiveDetailID < 1)
                    {
                        PullBulk = false;
                        // First look to see if there is a valid ESN already out there with this number.
                        PullBulk = PullSerializedIMEI(ctx, PullBulk, dl, rl, true);
                        // if there was a valid ESN Found above, don't pull from Bulk
                        if (PullBulk == true) { doSave = true; }
                        if (PullBulk == false)
                        {
                            ///////////////////////////////////////////////////////
                            string AttributeList = dl.Desc_Code.Replace(System.Environment.NewLine, ",");
                            ReceiveDetailManager bm = new ReceiveDetailManager(UserName);
                            ReceiveDetailBulk rd = bm.IsBulkDetailThere_All4Shipment(ProjectID, AttributeList);
                            if (rd == null)   // Proper detail line not found. see what to do
                            {
                                return msg;
                            }
                            if (rd != null)
                            {
                                using (clsLinqDataContext ctx2 = new clsLinqDataContext())
                                {

                                    ReceiveDetail rec1 = bm.GetBulkReceiveDetail(rd.ReceiveDetailBulkID);
                                    if (rec1 != null)
                                    {
                                        ReceiveDetail rec = ctx2.ReceiveDetails.FirstOrDefault(x => x.ReceiveDetailID == rec1.ReceiveDetailID);
                                        bm.TakeOneAwayFromBulk(rd.ReceiveDetailBulkID);
                                        rec.ESN = rl.ESN;
                                        ctx2.SubmitChanges();
                                        rl.ReceiveDetailID = rec.ReceiveDetailID;
                                        doSave = true;
                                    }
                                }

                            }
                            ///////////////////////////////////////////////////////
                        }
                    }
                }
            }
            if (doSave == true)
            {
                string MSG = "";
                SaveCurrent(MSG);
                msg = "Posted";
            }
            return msg;
        }
        public string PostBulkData()
        {
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                return PostBulkData(ctx);
            }
        }
        public string PostBulkData(clsLinqDataContext ctx)
        {
            string msg = "Not Posted";
            if (SOHeaderID > 0)   // We have a new one to save.
            {
                PostBulkData(ctx, msg);
                msg = "Saved";
            }
            return msg;
        }
        private void DeleteReservedAvailableStock(clsLinqDataContext ctx, SODetail ODLine)
        {
            //if (ODLine.ReservedAvailableStockID != null && ODLine.ReservedAvailableStockID > 0)
            //{
            //    ReservedAvailableStock r = ctx.ReservedAvailableStocks.FirstOrDefault(x => x.ReservedAvailableStockID == ODLine.ReservedAvailableStockID);
            //    if (r != null)
            //    {
            //        ctx.ReservedAvailableStocks.DeleteOnSubmit(r);
            //    }
            //}
        }
        private void BalanceReservedAvailableStock(clsLinqDataContext ctx, SODetail ODLine)
        {
            //if (ODLine.ReservedAvailableStockID != null && ODLine.ReservedAvailableStockID > 0)
            //{
            //    // Change the Value to match the new Order QTY
            //    ReservedAvailableStock r = ctx.ReservedAvailableStocks.FirstOrDefault(x => x.ReservedAvailableStockID == ODLine.ReservedAvailableStockID);
            //    if (r != null && ODLine.QTY != null && r.Quantity != (int)ODLine.QTY)
            //    {
            //        r.Quantity = (int)ODLine.QTY;
            //        ctx.SubmitChanges();
            //    }
            //}
            //else
            //{
            //    // We need to add a new record.
            //    ReservedAvailableStock r = new ReservedAvailableStock();
            //    string Manufacturer = "";
            //    string Model = "";
            //    string Colour = "";
            //    string Grade = "";
            //    string Carrier = "";
            //    // Get the Appropriate Key.
            //}

            ////var xx = SOHeader.SODetails.Where(x => x.ReservedAvailableStockID != null && x.ReservedAvailableStockID > 0);
            ////foreach (var r in xx.ToList())
            ////{
            ////    var AvailableStock = ctx.ReservedAvailableStocks.Where(x => x.ReservedAvailableStockID == r.ReservedAvailableStockID).FirstOrDefault();
            ////    if (AvailableStock != null)
            ////    {
            ////        AvailableStock.SODetailID = r.SODetailID;
            ////        AvailableStock.QTYAssigned = 0;                             // (int)r.QTY;
            ////        AvailableStock.LastUpdateDate = DateTime.Now;
            ////        AvailableStock.LastUpdateUser = UserName;
            ////        AvailableStock.AssignedDate = DateTime.Now;
            ////        AvailableStock.AssignedUser = UserName;
            ////        AvailableStock.isOpen = true;
            ////    }
            ////}
        }


        
        
        public string SaveHeaderData()
        {
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                return SaveHeaderData(ctx);
            }
        }
        public string SaveHeaderData(clsLinqDataContext ctx)
        {
            string msg = "Not Saved";
            if (SOHeaderID < 1)   // We have a new one to save.
            {
                msg = SaveNew(ctx, msg);
            }
            else
            {
                SaveCurrent(ctx, msg);
                msg = "Saved";
            }
            return msg;
        }










        private string SaveNew(string msg)
        {
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                return SaveNew(ctx, msg);
            }
        }
        private string SaveNew(clsLinqDataContext ctx, string msg)
        {
            SOHeader OH = new SOHeader();
            OH.ProjectID = ProjectID;
            ///  This is set down where i  have a ctx.    OH.OrderNumber = OrderNumber; 
            OH.CustomerPO = CustomerPO;
            OH.MiscReference = MiscReference;
            //OH.WayBillNumber = WaybillNumber;
            OH.ProjectTag = ProjectTag;
            OH.MiscDesc = MiscDesc;
            OH.Status = "New";
            //OH.StatusID = GetOrderStatusID(ctx, "New");
            OH.OrderDate = DateTime.Now;
            OH.LastUpdateDate = DateTime.Now;
            OH.LastUpdateUser = UserName;
            OH.CreateDate = DateTime.Now;
            OH.CreateUser = UserName;
            OH.PostPaid = PostPaid;
            OH.Paid = Paid;
            OH.DeliveryNote = DeliveryNote;
            OH.InternalNote = InternalNote;

            SOCompany OCClient = new SOCompany();
            OCClient.Name = "";
            OCClient.SOHeaderID = ClientCompany.SOHeaderID;
            OCClient.SOCompanyID = ClientCompany.SOCompanyID;
            OCClient.ClientLocationID = ClientCompany.ClientLocationID;
            OCClient.AddressLine1 = ClientCompany.AddressLine1;
            OCClient.AddressLine2 = ClientCompany.AddressLine2;
            OCClient.City = ClientCompany.City;
            OCClient.CompanyName = ClientCompany.CompanyName;
            OCClient.CompanyType = ClientCompany.CompanyType;
            OCClient.EmailAddress = ClientCompany.EmailAddress;
            OCClient.FaxNumber = ClientCompany.FaxNumber;
            OCClient.Notes = ClientCompany.Notes;
            OCClient.PhoneNumber = ClientCompany.PhoneNumber;
            OCClient.PostalCode = ClientCompany.PostalCode;
            OCClient.StateOrProvince = ClientCompany.StateOrProvince;
            OCClient.LastUpdateDate = DateTime.Now;
            OCClient.LastUpdateUser = UserName;
            OCClient.CreateDate = DateTime.Now;
            OCClient.CreateUser = UserName;
            OCClient.SOPickListHeaderID = -1;
            OH.SOCompanies.Add(OCClient);

            SOCompany OCShip = new SOCompany();
            OCShip.Name = "";
            OCShip.ClientLocationID = ShipToCompany.ClientLocationID;
            OCShip.AddressLine1 = ShipToCompany.AddressLine1;
            OCShip.AddressLine2 = ShipToCompany.AddressLine2;
            OCShip.City = ShipToCompany.City;
            OCShip.CompanyName = ShipToCompany.CompanyName;
            OCShip.CompanyType = ShipToCompany.CompanyType;
            OCShip.EmailAddress = ShipToCompany.EmailAddress;
            OCShip.FaxNumber = ShipToCompany.FaxNumber;
            OCShip.Notes = ShipToCompany.Notes;
            OCShip.PhoneNumber = ShipToCompany.PhoneNumber;
            OCShip.PostalCode = ShipToCompany.PostalCode;
            OCShip.StateOrProvince = ShipToCompany.StateOrProvince;
            OCShip.LastUpdateDate = DateTime.Now;
            OCShip.LastUpdateUser = UserName;
            OCShip.CreateDate = DateTime.Now;
            OCShip.CreateUser = UserName;
            OCClient.SOPickListHeaderID = -1;
            OH.SOCompanies.Add(OCShip);


            foreach (clsSODetailLine od in _SODetailLines)
            {
                SODetail ol = OH.SODetails.FirstOrDefault(x => x.SODetailID == od.SODetailID);
                if (od.isDeleted == true && ol != null)
                {
                    //var odlist = ctx.SODetailReceiveDetails.Where(x => x.SODetailID == ol.SODetailID);
                    foreach (SODetailReceiveDetail rd in ol.SODetailReceiveDetails)
                    {
                        ctx.SODetailReceiveDetails.DeleteOnSubmit(rd);
                    }
                    DeleteReservedAvailableStock(ctx, ol);
                    ctx.SODetails.DeleteOnSubmit(ol);
                }
                else
                {
                    if (ol == null)
                    {
                        ol = new SODetail();
                        ol.SOHeaderID = SOHeaderID;
                        ol.CreateDate = DateTime.Now;
                        ol.CreateUser = UserName;
                        //ol.QTYPacked = 0;
                    }
                    //ol.Desc_Code = od.Desc_Code;
                    //ol.Desc_Text = od.Desc_Text;
                    ol.QTY = od.QTY;
                    //ol.PurchaseUnitPrice = od.UnitPrice;
                    //ol.PurchaseUnitPrice = od.UnitPrice;
                    //ol.PurchasePrice = od.UnitPrice * od.QTY;
                    //ol.SKU = od.SKU;
                    ol.LastUpdateDate = DateTime.Now;
                    ol.LastUpdateUser = UserName;
                    //ol.AvailableStock_OrderNumber = od.AvailableStock_OrderNumber;
                    //ol.AvailableStock_QTY = od.AvailableStock_QTY;
                    //ol.ReservedAvailableStockID = od.ReservedAvailableStockID;
                    ol.Manufacturer = od.Manufacturer;
                    ol.Model = od.Model;
                    ol.Carrier = od.Carrier;
                    ol.Colour = od.Colour;
                    //ol.Grade = od.Grade;
                    if (ol.SODetailID < 1) { OH.SODetails.Add(ol); }
                }
            }
            string po = "";
            ctx.GetNextPurchaseOrderNumber(ref po);
            OrderNumber = po;
            OH.OrderNumber = po;
            ctx.SOHeaders.InsertOnSubmit(OH);
            ctx.SubmitChanges();

            foreach (SODetail x in OH.SODetails)
            {
                BalanceReservedAvailableStock(ctx, x);
            }
            ctx.SubmitChanges();
            SOHeaderID = OH.SOHeaderID;
            msg = "Added New";
            return msg;
        }
        private string SaveCurrent(string msg)
        {
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                return SaveCurrent(ctx, msg);
            }
        }
        private string SaveCurrent(clsLinqDataContext ctx, string msg)
        {
            SOHeader SOHeader = ctx.SOHeaders.FirstOrDefault(x => x.SOHeaderID == SOHeaderID);
            SOHeader.ProjectID = ProjectID;
            SOHeader.OrderNumber = OrderNumber;
            SOHeader.CustomerPO = CustomerPO;
            SOHeader.MiscReference = MiscReference;
            //SOHeader.WayBillNumber = WaybillNumber;
            SOHeader.ProjectTag = ProjectTag;
            SOHeader.MiscDesc = MiscDesc;
            SOHeader.LastUpdateDate = DateTime.Now;
            SOHeader.LastUpdateUser = UserName;
            SOHeader.Paid = Paid;
            SOHeader.PostPaid = PostPaid;
            SOHeader.InternalNote = InternalNote;
            SOHeader.DeliveryNote = DeliveryNote;
            if (SOHeader.OrderNumber.Length == 0)
            {
                string po = "";
                ctx.GetNextPurchaseOrderNumber(ref po);
                SOHeader.OrderNumber = po;
            }

            #region Client company Detail
            SOCompany OCClient = SOHeader.SOCompanies.FirstOrDefault(x => x.CompanyType == ClientCompany.CompanyType);
            if (OCClient == null)
            {
                OCClient = new SOCompany();
                OCClient.SOHeaderID = SOHeaderID;
                OCClient.CreateDate = DateTime.Now;
                OCClient.CreateUser = UserName;
            }
            OCClient.Name = "";
            OCClient.ClientLocationID = ClientCompany.ClientLocationID;
            OCClient.AddressLine1 = ClientCompany.AddressLine1;
            OCClient.AddressLine2 = ClientCompany.AddressLine2;
            OCClient.City = ClientCompany.City;
            OCClient.CompanyName = ClientCompany.CompanyName;
            OCClient.CompanyType = ClientCompany.CompanyType;
            OCClient.EmailAddress = ClientCompany.EmailAddress;
            OCClient.FaxNumber = ClientCompany.FaxNumber;
            OCClient.Notes = ClientCompany.Notes;
            OCClient.PhoneNumber = ClientCompany.PhoneNumber;
            OCClient.PostalCode = ClientCompany.PostalCode;
            OCClient.StateOrProvince = ClientCompany.StateOrProvince;
            OCClient.LastUpdateDate = DateTime.Now;
            OCClient.LastUpdateUser = UserName;
            if (OCClient.SOCompanyID < 1) { SOHeader.SOCompanies.Add(OCClient); }


            SOCompany OCShip = SOHeader.SOCompanies.FirstOrDefault(x => x.CompanyType == ShipToCompany.CompanyType);
            if (OCShip == null)
            {
                OCShip = new SOCompany();
                OCShip.SOHeaderID = SOHeaderID;
                OCShip.CreateDate = DateTime.Now;
                OCShip.CreateUser = UserName;
            }
            OCShip.Name = "";
            OCShip.ClientLocationID = ShipToCompany.ClientLocationID;
            OCShip.AddressLine1 = ShipToCompany.AddressLine1;
            OCShip.AddressLine2 = ShipToCompany.AddressLine2;
            OCShip.City = ShipToCompany.City;
            OCShip.CompanyName = ShipToCompany.CompanyName;
            OCShip.CompanyType = ShipToCompany.CompanyType;
            OCShip.EmailAddress = ShipToCompany.EmailAddress;
            OCShip.FaxNumber = ShipToCompany.FaxNumber;
            OCShip.Notes = ShipToCompany.Notes;
            OCShip.PhoneNumber = ShipToCompany.PhoneNumber;
            OCShip.PostalCode = ShipToCompany.PostalCode;
            OCShip.StateOrProvince = ShipToCompany.StateOrProvince;
            OCShip.LastUpdateDate = DateTime.Now;
            OCShip.LastUpdateUser = UserName;
            if (OCShip.SOCompanyID < 1) { SOHeader.SOCompanies.Add(OCShip); }
            #endregion
            #region Order Detail
            //clsSODetailLineReceive odlr = new clsSODetailLineReceive();
            //foreach (clsSODetailLine SODetailLine in _SODetailLines)
            //{
            //    SODetail SODetail = SOHeader.SODetails.FirstOrDefault(x => x.SODetailID == SODetailLine.SODetailID);
            //    if (SODetailLine.isDeleted == true)
            //    {
            //        if (SODetail != null)
            //        {
            //            var odlist = ctx.SODetailReceiveDetails.Where(x => x.SODetailID == SODetailLine.SODetailID);
            //            foreach (SODetailReceiveDetail rd in odlist)
            //            {
            //                odlr.Delete(ctx, rd.SODetailReceiveDetailID);
            //            }
            //            DeleteReservedAvailableStock(ctx, SODetail);
            //            ctx.SODetails.DeleteOnSubmit(SODetail);
            //            // SOHeader.SODetails.Remove(SODetail);
            //        }
            //    }
            //    else
            //    {
            //        if (SODetail == null)
            //        {
            //            SODetail = new SODetail();
            //            SODetail.SOHeaderID = SOHeaderID;
            //            SODetail.CreateDate = DateTime.Now;
            //            SODetail.CreateUser = UserName;
            //            SODetail.AvailableStock_OrderNumber = SODetailLine.AvailableStock_OrderNumber;
            //            SODetail.AvailableStock_QTY = SODetailLine.AvailableStock_QTY;
            //            SODetail.ReservedAvailableStockID = SODetailLine.ReservedAvailableStockID;
            //            SODetail.QTYPacked = 0;
            //        }
            //        SODetail.Desc_Code = SODetailLine.Desc_Code;
            //        SODetail.Desc_Text = SODetailLine.Desc_Text;
            //        SODetail.QTY = SODetailLine.QTY;
            //        SODetail.SKU = SODetailLine.SKU;
            //        SODetail.QTYPacked = SODetailLine.QTYPacked;
            //        SODetail.PurchaseUnitPrice = SODetailLine.UnitPrice;
            //        SODetail.LastUpdateDate = DateTime.Now;

            //        SODetail.Manufacturer = SODetailLine.Manufacturer;
            //        SODetail.Model = SODetailLine.Model;
            //        SODetail.Carrier = SODetailLine.Carrier;
            //        SODetail.Colour = SODetailLine.Colour;
            //        SODetail.Grade = SODetailLine.Grade;
            //        SODetail.IFSSku = SODetailLine.IFSSKU;
            //        SODetail.Location = SODetailLine.Location;
            //        SODetail.Condition = SODetailLine.Condition;


            //        if (SODetailLine.UserName == null) { SODetail.LastUpdateUser = UserName; } else { SODetail.LastUpdateUser = SODetailLine.UserName; }
            //        foreach (clsSODetailLineReceive SODetailLineReceive in SODetailLine._SODetailReceiveLines)
            //        {
            //            SODetailReceiveDetail SODetailReceiveDetail = SODetail.SODetailReceiveDetails.FirstOrDefault(x => x.SODetailReceiveDetailID == SODetailLineReceive.SODetailReceiveDetailID);
            //            if (SODetailLineReceive.isDeleted == true)
            //            {
            //                if (SODetailReceiveDetail != null)
            //                {
            //                    odlr.Delete(ctx, SODetailReceiveDetail.SODetailReceiveDetailID);
            //                }
            //            }
            //            else
            //            {
            //                if (SODetailReceiveDetail == null)
            //                {
            //                    SODetailReceiveDetail = new SODetailReceiveDetail();
            //                    SODetailReceiveDetail.CreateDate = DateTime.Now;
            //                    SODetailReceiveDetail.CreateUser = UserName;
            //                }
            //                SODetailReceiveDetail.ESN = SODetailLineReceive.ESN;
            //                SODetailReceiveDetail.SODetailID = SODetailLineReceive.SODetailID;
            //                SODetailReceiveDetail.ReceiveDetailID = SODetailLineReceive.ReceiveDetailID;
            //                SODetailReceiveDetail.SKU = SODetailLineReceive.SKU;
            //                SODetailReceiveDetail.Message = SODetailLineReceive.Message;
            //                SODetailReceiveDetail.LastUpdateDate = DateTime.Now;
            //                if (SODetailReceiveDetail.SODetailReceiveDetailID < 1)
            //                {
            //                    SODetail.SODetailReceiveDetails.Add(SODetailReceiveDetail);
            //                }
            //            }
            //        }


            //        if (SODetail.SODetailID < 1) { SOHeader.SODetails.Add(SODetail); }
            //    }
            //}
            #endregion
            ctx.SubmitChanges();

            msg = "Added New";
            return msg;
        }


        private Client GetClientFromSODetailID(clsLinqDataContext ctx, decimal SODetailID)
        {
            SODetail OD = ctx.SODetails.FirstOrDefault(x => x.SODetailID == SODetailID);
            SOHeader OH = ctx.SOHeaders.FirstOrDefault(x => x.SOHeaderID == OD.SOHeaderID);
            SOCompany OC = ctx.SOCompanies.FirstOrDefault(x => x.SOHeaderID == OH.SOHeaderID && x.CompanyType == "Client");
            ClientLocation CL = ctx.ClientLocations.FirstOrDefault(x => x.ClientLocationID == OC.ClientLocationID);
            Client C = ctx.Clients.FirstOrDefault(x => x.ClientID == CL.ClientID);
            return C;
        }

        //public decimal GetOrderStatusID(string Value)
        //{
        //    using (clsLinqDataContext ctx = new clsLinqDataContext())
        //    {
        //        return GetOrderStatusID(ctx, Value);
        //    }
        //}
        //public decimal GetOrderStatusID(clsLinqDataContext ctx, string Value)
        //{
        //    OrderStatus OS = ctx.OrderStatus.FirstOrDefault(x => x.Status == Value);
        //    if (OS != null)
        //    {
        //        return OS.OrderStatusID;
        //    }
        //    return 2;    // NEW
        //}
    }
    public class clsSOHeaderCompany
    {
        public decimal SOCompanyID { get; set; }
        public decimal SOHeaderID { get; set; }
        public decimal ClientLocationID { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string StateOrProvince { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string PhoneNumber { get; set; }
        public string FaxNumber { get; set; }
        public string Notes { get; set; }
        public string CompanyType { get; set; }
        public string EmailAddress { get; set; }
        public string UserName { get; set; }

        public clsSOHeaderCompany()
        {
        }
        public void LoadCompany(string companytype, decimal SOHeaderID)
        {
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                SOCompany OC = ctx.SOCompanies.FirstOrDefault(x => x.CompanyType == companytype && x.SOHeaderID == SOHeaderID);
                if (OC == null)
                {
                    SOCompanyID = -1;
                    SOHeaderID = SOHeaderID;
                    CompanyType = companytype;
                }
                else
                {
                    SOCompanyID = -1;
                    SOHeaderID = -1;
                    if (OC.ClientLocationID == null)
                    {
                        ClientLocationID = -1;
                    }
                    else
                    {
                        ClientLocationID = (decimal)OC.ClientLocationID;
                    }
                    SOCompanyID = OC.SOCompanyID;
                    SOHeaderID = OC.SOHeaderID;
                    CompanyType = OC.CompanyType;
                    EmailAddress = OC.EmailAddress;
                    CompanyName = OC.CompanyName;
                    ContactName = OC.ContactName;
                    AddressLine1 = OC.AddressLine1;
                    AddressLine2 = OC.AddressLine2;
                    City = OC.City;
                    StateOrProvince = OC.StateOrProvince;
                    Country = OC.Country;
                    PostalCode = OC.PostalCode;
                    PhoneNumber = OC.PhoneNumber;
                    FaxNumber = OC.FaxNumber;
                    Notes = OC.Notes;
                }
            }
        }
        public void LoadCompanyFromClientLocation(string ScanKey)
        {
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                ClientLocation ClientLocation = ctx.ClientLocations.FirstOrDefault(x => x.ScanKey == ScanKey);
                if (ClientLocation != null)
                {
                    SOCompanyID = -1;
                    SOHeaderID = -1;
                    ClientLocationID = ClientLocation.ClientLocationID;
                    CompanyName = ClientLocation.CompanyName;
                    ContactName = ClientLocation.ContactName;
                    AddressLine1 = ClientLocation.AddressLine1;
                    AddressLine2 = ClientLocation.AddressLine2;
                    City = ClientLocation.City;
                    StateOrProvince = ClientLocation.StateOrProvince;
                    Country = "";
                    PostalCode = ClientLocation.PostalCode;
                    PhoneNumber = ClientLocation.PhoneNumber;
                    FaxNumber = ClientLocation.FaxNumber;
                    Notes = ClientLocation.Notes;
                }
            }
        }
        public void LoadCompanyFromClientLocation(decimal ClientLocationID)
        {
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                ClientLocation ClientLocation = ctx.ClientLocations.FirstOrDefault(x => x.ClientLocationID == ClientLocationID);
                if (ClientLocation != null)
                {
                    SOCompanyID = -1;
                    SOHeaderID = -1;
                    ClientLocationID = ClientLocation.ClientLocationID;
                    CompanyName = ClientLocation.CompanyName;
                    ContactName = ClientLocation.ContactName;
                    AddressLine1 = ClientLocation.AddressLine1;
                    AddressLine2 = ClientLocation.AddressLine2;
                    City = ClientLocation.City;
                    StateOrProvince = ClientLocation.StateOrProvince;
                    Country = "";
                    PostalCode = ClientLocation.PostalCode;
                    PhoneNumber = ClientLocation.PhoneNumber;
                    FaxNumber = ClientLocation.FaxNumber;
                    Notes = ClientLocation.Notes;
                }
            }
        }
    }
    public class clsSODetailLine
    {
        public List<clsSODetailLineReceive> _SODetailReceiveLines = new List<clsSODetailLineReceive>();
        public decimal SODetailID { get; set; }
        public bool isNew { get; set; }
        public bool isDirty { get; set; }
        public bool isDeleted { get; set; }

        public bool isAttributeLocked { get; set; }

        public string SKU { get; set; }
        public string IFSSKU { get; set; }
        public string Project_ID { get; set; }
        public string Location { get; set; }
        public string Condition { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal QTY { get; set; }
        public decimal QTYPacked { get; set; }
        public decimal QTYLeft { get; set; }
        public decimal QTYInventoryLinked { get; set; }
        public short Line_No { get; set; }


        public string Desc_Code { get; set; }
        public string Desc_Text { get; set; }
        public string UserName { get; set; }

        public string AvailableStock_OrderNumber { get; set; }
        public int AvailableStock_QTY { get; set; }
        public decimal ReservedAvailableStockID { get; set; }
        public string AvailableStock_LevelKeys { get; set; }

        public string Model { get; set; }
        public string Manufacturer { get; set; }
        public string Colour { get; set; }
        public string Grade { get; set; }
        public string Carrier { get; set; }

        public clsSODetailLine()
        {
            SODetailID = -1;
            isNew = true;
            isDeleted = false;
            isDirty = false;
            isAttributeLocked = true;
            QTY = 0;
            QTYPacked = 0;
            QTYLeft = 0;
            QTYInventoryLinked = 0;
            UnitPrice = 0;
            AvailableStock_OrderNumber = "";
            AvailableStock_QTY = 0;
            ReservedAvailableStockID = -1;
            Manufacturer = "";
            Model = "";
            Colour = "";
            Grade = "";
            Carrier = "";
            Line_No = 0;
            Condition = "";
            IFSSKU = "";
            Location = "";
            Project_ID = "";

        }
        public void LoadReceiveDetail()
        {
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                var RD = from x in ctx.SODetailReceiveDetails
                         where x.SODetailID == SODetailID
                         //orderby x.SKU, x.ESN
                         select x;
                QTYPacked = 0;
                QTYInventoryLinked = 0;
                foreach (SODetailReceiveDetail rd in RD)
                {
                    clsSODetailLineReceive rl = new clsSODetailLineReceive();
                    rl.ESN = rd.ESN;
                    rl.isDirty = false;
                    rl.SODetailID = rd.SODetailID;
                    rl.SODetailReceiveDetailID = rd.SODetailReceiveDetailID;
                    rl.ReceiveDetailID = rd.ReceiveDetailID;
                    //rl.SKU = rd.SKU;
                    rl.UserName = UserName;
                    _SODetailReceiveLines.Add(rl);
                    if (rd.ReceiveDetailID != null && rd.ReceiveDetailID > 0) { QTYInventoryLinked++; }
                    QTYPacked++;
                }
            }
        }



    }
    public class clsSODetailLineReceive
    {
        public decimal SODetailReceiveDetailID { get; set; }
        public decimal SODetailID { get; set; }
        public decimal? ReceiveDetailID { get; set; }
        public bool isNew { get; set; }
        public bool isDirty { get; set; }
        public bool isDeleted { get; set; }
        public string ESN { get; set; }
        public string SKU { get; set; }
        public string UserName { get; set; }
        public string Message { get; set; }
        public clsSODetailLineReceive()
        {
            SODetailReceiveDetailID = -1;
            SODetailID = -1;
            ReceiveDetailID = null;
            ESN = "";
            SKU = "";
            Message = "";
            isNew = true;
            isDeleted = false;
            isDirty = false;
        }


        public bool Delete()
        {
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                return Delete(ctx);
            }
        }
        public bool Delete(clsLinqDataContext ctx)
        {
            SODetailReceiveDetail RD = ctx.SODetailReceiveDetails.FirstOrDefault(x => x.SODetailReceiveDetailID == SODetailReceiveDetailID);
            SODetail OD = RD.SODetail;
            if (RD != null)
            {
                //var rdpl = ctx.ReceiveDetailProcessLogs.Where(x => x.ReceiveDetailID == RD.ReceiveDetailID && x.Process.Name == "Shipped");
                //foreach (ReceiveDetailProcessLog pl in rdpl)
                //{
                //    ctx.ReceiveDetailProcessLogs.DeleteOnSubmit(pl);
                //}
                //OD.QTYPacked += -1;
                ctx.SODetailReceiveDetails.DeleteOnSubmit(RD);
                ctx.SubmitChanges();
            }
            return true;
        }
        public bool Delete(clsLinqDataContext ctx, decimal SODetailReceiveDetailID)
        {
            SODetailReceiveDetail RD = ctx.SODetailReceiveDetails.FirstOrDefault(x => x.SODetailReceiveDetailID == SODetailReceiveDetailID);
            if (RD != null)
            {
                var rdpl = ctx.ReceiveDetailProcessLogs.Where(x => x.ReceiveDetailID == RD.ReceiveDetailID && x.Process.Name == "Shipped");
                foreach (ReceiveDetailProcessLog pl in rdpl)
                {
                    ctx.ReceiveDetailProcessLogs.DeleteOnSubmit(pl);
                }
                ctx.SODetailReceiveDetails.DeleteOnSubmit(RD);
            }
            return true;
        }
        public bool Save(clsLinqDataContext ctx)
        {
            //using (clsLinqDataContext ctx = new clsLinqDataContext())
            //{
            SODetailReceiveDetail RD = ctx.SODetailReceiveDetails.FirstOrDefault(x => x.SODetailReceiveDetailID == SODetailReceiveDetailID);
            if (RD == null)
            {
                // look to see if the ESN is already used. If there, stop...
                RD = new SODetailReceiveDetail();
                RD.SODetailID = SODetailID;
                RD.ReceiveDetailID = null;
                RD.CreateDate = DateTime.Now;
                RD.CreateUser = UserName;
            }
            RD.Message = Message;
            RD.ESN = ESN;
            //RD.SKU = SKU;
            RD.LastUpdateDate = DateTime.Now;
            RD.LastUpdateUser = UserName;
            if (RD.SODetailReceiveDetailID < 1)
            {
                ctx.SODetailReceiveDetails.InsertOnSubmit(RD);
            }
            //ctx.SubmitChanges();
            return true;
            //}
        }

    }
    #endregion


}