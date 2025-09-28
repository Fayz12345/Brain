using System;
using System.Collections.Generic;
using System.Linq;
//using System.Web.Security;

//using Factory_DataModel;
using BW_WebApp.DataManagers;
using Syncfusion.XlsIO;

namespace BW_WebApp.Classes
{




    public class ModelMemoryProcessor
    {
        public string filename { get; set; }
        public string username { get; set; }
        //public string orderentrynumber { get; set; }
        //string BinNumber { get; set; }
        //public decimal projectID { get; set; }
        public int RowCount { get; set; }
        public bool isXLS { get; set; }
        //public IFSLocation location { get; set; }
        //bool force15IMEI { get; set; }

        public ExcelEngine excelEngine = null;
        public IApplication application = null;
        public IWorkbook workbook = null;


        private List<HeaderData> Header = new List<HeaderData>();

        public ModelMemoryProcessor(string DrivePathFileNameXLS_toUpload, string UserName)
        {
            filename = DrivePathFileNameXLS_toUpload;
            isXLS = true;
            username = UserName;
        }

        public string LoadModelData()
        {
            string rvalue = "";
            if (isXLS == true) { rvalue = LoadModelDatafromXLS(); }
            return rvalue;
        }
        public string LoadModelDatafromXLS()
        {
            if (isXLS == false) { return "No File to Parse"; }
            string rvalue = "";
            //rvalue = "No Order Entry Number given.";
            //if (orderentrynumber.Trim().Length > 0)
            //{
            rvalue = "";
            RowCount = 0;
            DateTime starttime = DateTime.Now;
            excelEngine = new ExcelEngine();
            //Step 2 : Instantiate the excel application object.
            application = excelEngine.Excel;
            //Open an existing spreadsheet which will be used as a template for generating the new spreadsheet.
            //After opening, the workbook object represents the complete in-memory object model of the template spreadsheet.
            workbook = application.Workbooks.Open(filename, ExcelOpenType.Automatic);
            //The first worksheet object in the worksheets collection is accessed.
            IWorksheet sheet = workbook.Worksheets[0];
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                //rvalue = LoadHeaderData(sheet);
                rvalue = LoadData(ctx, sheet);
            }
            //workbook.Close();
            //excelEngine.Dispose();
            DateTime endtime = DateTime.Now;
            TimeSpan diffResult = endtime.Subtract(starttime);
            if (rvalue.Length == 0)
            {
                rvalue = "(HH:MM:SS)" + diffResult.Hours.ToString() + ":" + diffResult.Minutes.ToString() + ":" + diffResult.Seconds.ToString() + "  -- Row Count:" + RowCount.ToString();
            }
            //}
            return rvalue;
        }
        private string LoadData(clsLinqDataContext ctx, IWorksheet sheet)
        {
            //return "Inside Load Data";


            int ModelCol = 1;
            int MemoryListCol = 2;
            int ErrorCol = 3;
            string rvalue = "";
            List<ModelMemoryRowData> Data = new List<ModelMemoryRowData>();

            sheet.Range[1, ErrorCol].Text = "Status";

            for (int row = 2; row < 1000000; row++)
            {
                if (sheet.Range[row, 1].Value == null || sheet.Range[row, ModelCol].Value.Length == 0) { break; }
                ModelMemoryRowData rd = new ModelMemoryRowData(ctx, sheet.Range[row, ModelCol].Value, sheet.Range[row, MemoryListCol].Value, row, username);
                if (rd.ErrorMessage.Length == 0) { rd.LoadData(); }
                if (rd.ErrorMessage.Length > 0) { sheet.Range[row, ErrorCol].Text = rd.ErrorMessage; }
                else { Data.Add(rd); sheet.Range[row, ErrorCol].Text = "Loaded"; }
            }
            return rvalue;
        }

        class ModelMemoryRowData
        {
            public string ModelAbbr = "";
            public ReceiveDetail RD = null;
            public int row = 0;
            public string ErrorMessage = "";
            public string mMemoryList = "";
            public PairIDValue Model = new PairIDValue();
            public List<PairIDValue> MemoryList = new List<PairIDValue>();
            //public IFSLocation Location;
            //public decimal QTY = 0;
            public bool isFound = false;
            public clsLinqDataContext ctx = null;
            string userName = "";

            public ModelMemoryRowData(clsLinqDataContext ctxx, string MODELABBR, string mList, int Row, string UserName)
            {
                ctx = ctxx;
                ModelAbbr = MODELABBR;
                mMemoryList = mList;
                userName = UserName;

                // Verify the Model is valid
                var dta1 = ctx.Options.FirstOrDefault(x => x.Name == ModelAbbr && x.Question.Name == "Model");
                if (dta1 == null) { ErrorMessage += ("Model(" + ModelAbbr + ")invalid/"); }
                Model.Desc = ModelAbbr;
                Model.ID = -1;
                if (dta1 != null) { Model.ID = dta1.OptionID; }

                // parse the list
                List<string> values = mMemoryList.Split(',').Select(sValue => sValue.Trim()).ToList();
                // Verify each are valid
                foreach (string a in values)
                {
                    var dta = ctx.Options.FirstOrDefault(x => x.Name == a && x.Question.Name == "Memory");
                    if (dta == null) { ErrorMessage += ("Memory(" + a + ")invalid/"); }
                    else
                    {
                        PairIDValue m = new PairIDValue();
                        m.ID = dta.OptionID;
                        m.Desc = a;
                        MemoryList.Add(m);
                    }
                }
                row = Row;
                isFound = false;
                userName = UserName;
            }

            public void LoadData()
            {
                string MemoryIDs = "";
                foreach (PairIDValue v in MemoryList)
                {
                    MemoryIDs = MemoryIDs + (MemoryIDs.Length > 0 ? "," : "") + v.ID.ToString();
                }
                ctx.RecordModelDefinitionMemory(Model.ID, MemoryIDs, "", userName);
            }
        }
    }


    public class IMEIMoveUploadProcessor
    {
        public string filename { get; set; }
        public string username { get; set; }
        //public string orderentrynumber { get; set; }
        //string BinNumber { get; set; }
        //public decimal projectID { get; set; }
        public int RowCount { get; set; }
        public bool isXLS { get; set; }
        //public IFSLocation location { get; set; }
        //bool force15IMEI { get; set; }

        public ExcelEngine excelEngine = null;
        public IApplication application = null;
        public IWorkbook workbook = null;


        private List<HeaderData> Header = new List<HeaderData>();

        //public IMEIMoveUploadProcessor(string binNumber, string UserName, string OrderEntryNumber)
        //{
        //    orderentrynumber = "";
        //    projectID = -1;
        //    force15IMEI = false;
        //    OrderManager om = new OrderManager(UserName);
        //    OrderHeader oe = om.GetOrderHeader(OrderEntryNumber);
        //    if (oe != null)
        //    {
        //        projectID = oe.ProjectID;
        //        orderentrynumber = OrderEntryNumber;
        //    }
        //    BinNumber = binNumber;
        //    isXLS = false;
        //    username = UserName;
        //}
        public IMEIMoveUploadProcessor(string DrivePathFileNameXLS_toUpload, string UserName)
        {
            //orderentrynumber = "";
            //projectID = -1;
            //force15IMEI = Force15IMEI;
            //location = Location;
            //OrderManager om = new OrderManager(UserName);
            //OrderHeader oe = om.GetOrderHeader(OrderEntryNumber);
            //if (oe != null)
            //{
            //    projectID = oe.ProjectID;
            //    orderentrynumber = OrderEntryNumber;
            //}
            filename = DrivePathFileNameXLS_toUpload;
            isXLS = true;
            username = UserName;
        }

        public string LoadIMEIData()
        {
            string rvalue = "";
            if (isXLS == true) { rvalue = LoadIMEIDatafromXLS(); }
            return rvalue;
        }
        public string LoadIMEIDatafromXLS()
        {
            if (isXLS == false) { return "No File to Parse"; }
            string rvalue = "";
            //rvalue = "No Order Entry Number given.";
            //if (orderentrynumber.Trim().Length > 0)
            //{
            rvalue = "";
            RowCount = 0;
            DateTime starttime = DateTime.Now;
            excelEngine = new ExcelEngine();
            //Step 2 : Instantiate the excel application object.
            application = excelEngine.Excel;
            //Open an existing spreadsheet which will be used as a template for generating the new spreadsheet.
            //After opening, the workbook object represents the complete in-memory object model of the template spreadsheet.
            workbook = application.Workbooks.Open(filename, ExcelOpenType.Automatic);
            //The first worksheet object in the worksheets collection is accessed.
            IWorksheet sheet = workbook.Worksheets[0];
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                //rvalue = LoadHeaderData(sheet);
                rvalue = LoadData(ctx, sheet);
            }
            //workbook.Close();
            //excelEngine.Dispose();
            DateTime endtime = DateTime.Now;
            TimeSpan diffResult = endtime.Subtract(starttime);
            if (rvalue.Length == 0)
            {
                rvalue = "(HH:MM:SS)" + diffResult.Hours.ToString() + ":" + diffResult.Minutes.ToString() + ":" + diffResult.Seconds.ToString() + "  -- Row Count:" + RowCount.ToString();
            }
            //}
            return rvalue;
        }
        private string LoadData(clsLinqDataContext ctx, IWorksheet sheet)
        {
            //return "Inside Load Data";


            int ESNCol = 1;
            int LocationCol = 2;
            int ErrorCol = 3;
            //OrderManager OM = new OrderManager(username);
            string rvalue = "";
            List<ESNRowData> Data = new List<ESNRowData>();

            //OrderHeader OH = ctx.OrderHeaders.FirstOrDefault(x => x.OrderNumber == orderentrynumber);
            sheet.Range[1, ErrorCol].Text = "Status";
            //if (OH == null)
            //{
            //    sheet.Range[2, ErrorCol].Text = "Order Not Found:" + orderentrynumber;
            //    return "Order not found" + orderentrynumber;
            //}

            for (int row = 2; row < 1000000; row++)
            {
                if (sheet.Range[row, 1].Value == null || sheet.Range[row, ESNCol].Value.Length == 0) { break; }
                ESNRowData rd = new ESNRowData(sheet.Range[row, ESNCol].Value, sheet.Range[row, LocationCol].Value, row, username);
                if (rd.ErrorMessage.Length == 0) { rd.LoadData(ctx); }
                if (rd.ErrorMessage.Length > 0) { sheet.Range[row, ErrorCol].Text = rd.ErrorMessage; }
                else { Data.Add(rd); sheet.Range[row, ErrorCol].Text = "Moved"; }
            }
            //foreach (ESNRowData r in Data.Where(x => x.isFound == true))
            //{
            //    string Message = "";
            //    Message = OM.RecordIFSPickIMEI(OH.OrderHeaderID, location.Text, r.carton, r.Esn, username);
            //    if (Message.Length > 5 && Message.Substring(0, 5).ToUpper() == "ERROR") { r.ErrorMessage = Message; }
            //    else { r.ErrorMessage = "Uploaded."; }
            //}
            //foreach (ESNRowData r in Data)
            //{
            //    sheet.Range[r.row, ErrorCol].Text = r.ErrorMessage;
            //}
            return rvalue;
        }
        //private bool isESNAlreadyOnThisOrder(string ESN, OrderHeader OH)
        //{
        //    foreach (var x in OH.OrderDetails)
        //    {
        //        foreach (var y in x.OrderDetailReceiveDetails)
        //        {
        //            if (y.ESN == ESN) { return true; }
        //        }
        //    }

        //    //OrderDetailReceiveDetail odrd = OH.OrderDetailReceiveDetails.FirstOrDefault(x => x.OrderDetail.OrderHeader.OrderStatus.Status.ToUpper() != "TRASH" && x.ReceiveDetailID == rd.ReceiveDetailID);
        //    //if (odrd != null) { ErrorMessage = "ESN already on an Order (" + odrd.OrderDetail.OrderHeader.OrderNumber + ")"; return; }


        //    return false;
        //}
        //private string LoadHeaderData(IWorksheet sheet)
        //{
        //    string rvalue = "";
        //    List<string> ESNDetail = new List<string>(new string[] { "ESN", "STARTPROCESSNAME", "CURRENTPROCESSNAME", "RECEIVEDETAILID", "CLIENTLOCATIONID", "CLIENTLOCATION", "RMANUMBER", "PROJECTTAG", "PROJECTNAME" });
        //    QuestionManager qm = new QuestionManager(username);
        //    List<PairIDValue> QID_Name = qm.GetAllQuestionsPairIDName(projectID);
        //    int row = 1;
        //    Header.Clear();
        //    for (int col = 1; col < 100000; col++)
        //    {
        //        if (sheet.Range[row, col].Value == null || sheet.Range[row, col].Value.Length == 0) { break; }
        //        HeaderData hd = new HeaderData(col, sheet.Range[row, col].Value);
        //        hd.Fill(qm, projectID, username, ESNDetail, QID_Name);
        //        //if (attributeclonelist.Contains(hd.text))  { hd.isCloneColumn = true; }
        //        Header.Add(hd);
        //    }
        //    return rvalue;
        //}
        class ESNRowData
        {
            public string Esn = "";
            public ReceiveDetail RD = null;
            public int row = 0;
            public string ErrorMessage = "";
            public IFSLocation Location;
            //public decimal QTY = 0;
            public bool isFound = false;
            string userName = "";

            public ESNRowData(string ESN, string location, int Row, string UserName)
            {
                Esn = ESN;
                Location = new IFSLocation(location);
                if (Location.isValid == false) { ErrorMessage = "Location Not Valid"; }
                if (Location.IsThisFrozen(UserName) == true) { ErrorMessage = "Location is froozen"; }
                row = Row;
                isFound = false;
                userName = UserName;
            }

            public void LoadData(clsLinqDataContext ctx)
            {
                if (Location.isValid == true && Location.IsThisFrozen(userName) == false)
                {
                    ReceiveDetail rd = ctx.ReceiveDetails.FirstOrDefault(x => x.ESN == Esn && x.Version == "000");
                    if (rd == null) { ErrorMessage = "IMEI not Found"; return; }
                    rd.IFSLocation = Location.Text;
                    ctx.SubmitChanges();
                    isFound = true;
                    RD = rd;
                }
            }
        }
    }







    public class IMEIOrderUploadProcessor
    {
        public string filename { get; set; }
        public string username { get; set; }
        public string orderentrynumber { get; set; }
        string BinNumber { get; set; }
        public decimal projectID { get; set; }
        public int RowCount { get; set; }
        public bool isXLS { get; set; }
        public IFSLocation location { get; set; }
        bool force15IMEI { get; set; }

        public ExcelEngine excelEngine = null;
        public IApplication application = null;
        public IWorkbook workbook = null;


        private List<HeaderData> Header = new List<HeaderData>();

        public IMEIOrderUploadProcessor(string binNumber, string UserName, string OrderEntryNumber)
        {
            BinNumber = binNumber;
            isXLS = false;
            username = UserName;
            orderentrynumber = "";
            projectID = -1;
            force15IMEI = false;

            if (OrderEntryNumber.ToUpper() == "C1CON")
            {
                orderentrynumber = "C1CON";
            }
            else
            {
                OrderManager om = new OrderManager(UserName);
                OrderHeader oe = om.GetOrderHeader(OrderEntryNumber);
                if (oe != null)
                {
                    projectID = oe.ProjectID;
                    orderentrynumber = OrderEntryNumber;
                }
            }

        }
        public IMEIOrderUploadProcessor(string DrivePathFileNameXLS_toUpload, string UserName, string OrderEntryNumber, IFSLocation Location, bool Force15IMEI)
        {
            filename = DrivePathFileNameXLS_toUpload;
            location = Location;

            isXLS = true;
            username = UserName;
            orderentrynumber = "";
            projectID = -1;
            force15IMEI = Force15IMEI;


            if (OrderEntryNumber.ToUpper() == "C1CON")
            {
                orderentrynumber = "C1CON";
            }
            else
            {
                OrderManager om = new OrderManager(UserName);
                OrderHeader oe = om.GetOrderHeader(OrderEntryNumber);
                if (oe != null)
                {
                    projectID = oe.ProjectID;
                    orderentrynumber = OrderEntryNumber;
                }
            }
        }

        public string LoadIMEIDataQuerry()
        {
            string rvalue = "";
            if (isXLS == true) { rvalue = LoadIMEIDatafromXLSQuerry(); }
            return rvalue;
        }
        public string LoadIMEIDatafromXLSQuerry()
        {
            if (isXLS == false) { return "No File to Parse"; }
            string rvalue = "";
            //rvalue = "No Order Entry Number given.";
            //if (orderentrynumber.Trim().Length > 0)
            //{
            rvalue = "";
            RowCount = 0;
            DateTime starttime = DateTime.Now;
            excelEngine = new ExcelEngine();
            //Step 2 : Instantiate the excel application object.
            application = excelEngine.Excel;
            //Open an existing spreadsheet which will be used as a template for generating the new spreadsheet.
            //After opening, the workbook object represents the complete in-memory object model of the template spreadsheet.
            workbook = application.Workbooks.Open(filename, ExcelOpenType.Automatic);
            //The first worksheet object in the worksheets collection is accessed.
            IWorksheet sheet = workbook.Worksheets[0];
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                //rvalue = LoadHeaderData(sheet);
                rvalue = LoadDataQuerry(ctx, sheet);
            }
            //workbook.Close();
            //excelEngine.Dispose();
            DateTime endtime = DateTime.Now;
            TimeSpan diffResult = endtime.Subtract(starttime);
            if (rvalue.Length == 0)
            {
                rvalue = "(HH:MM:SS)" + diffResult.Hours.ToString() + ":" + diffResult.Minutes.ToString() + ":" + diffResult.Seconds.ToString() + "  -- Row Count:" + RowCount.ToString();
            }
            //}
            return rvalue;
        }
        private string LoadDataQuerry(clsLinqDataContext ctx, IWorksheet sheet)
        {
            //return "Inside Load Data";


            int ESNCol = 1;

            int ErrorCol = 3;
            OrderManager OM = new OrderManager(username);
            string rvalue = "";
            List<ESNRowData> Data = new List<ESNRowData>();

            //OrderHeader OH = ctx.OrderHeaders.FirstOrDefault(x => x.OrderNumber == orderentrynumber);
            //sheet.Range[1, ErrorCol].Text = "Status";
            //if (OH == null)
            //{
            //    sheet.Range[2, ErrorCol].Text = "Order Not Found:" + orderentrynumber;
            //    return "Order not found" + orderentrynumber;
            //}

            for (int row = 2; row < 1000000; row++)
            {
                if (sheet.Range[row, 1].Value == null || sheet.Range[row, ESNCol].Value.Length == 0) { break; }
                ESNRowData rd = new ESNRowData(sheet.Range[row, ESNCol].Value, "", row);
                rd.LoadData(ctx, -1);
                //if (rd.ErrorMessage.Length > 0) { sheet.Range[row, ErrorCol].Text = rd.ErrorMessage; }
                //else { 
                Data.Add(rd);
                //}
            }

            int CartonCol = 2;
            for (int row = 1; row < 5; row++)
            {
                sheet.Range[1, CartonCol++].Text = "IFS Order #";
                sheet.Range[1, CartonCol++].Text = "PickList #";
                sheet.Range[1, CartonCol++].Text = "Line #";
                sheet.Range[1, CartonCol++].Text = "Unit Price";
            }

            //foreach (ESNRowData r in Data.Where(x => x.isFound == true))  //we know the ESN most likely is not a 000 version.
            foreach (ESNRowData r in Data)
            {
                CartonCol = 2;
                var OrderList = ctx.OrderDetailReceiveDetails.Where(x => x.ESN == r.Esn && x.OrderDetail.OrderHeader.OrderStatus.Status.ToUpper() == "DONE");
                foreach (OrderDetailReceiveDetail rec in OrderList.OrderByDescending(x => x.OrderDetailReceiveDetailID))
                {
                    //sheet.Range[r.row, CartonCol++].Text = rec.OrderDetail.OrderHeader.IFSOrderNo;
                    //sheet.Range[r.row, CartonCol++].Text = rec.OrderDetail.OrderHeader.OrderNumber;
                    //sheet.Range[r.row, CartonCol++].Text = rec.OrderDetail.Line_NO.ToString();

                    if (rec.OrderDetail.OrderHeader.IFSOrderNo == null) { CartonCol++; } else { sheet.Range[r.row, CartonCol++].Text = rec.OrderDetail.OrderHeader.IFSOrderNo.ToString(); }
                    if (rec.OrderDetail.OrderHeader.OrderNumber == null) { CartonCol++; } else { sheet.Range[r.row, CartonCol++].Text = rec.OrderDetail.OrderHeader.OrderNumber.ToString(); }
                    if (rec.OrderDetail.Line_NO == null) { CartonCol++; } else { sheet.Range[r.row, CartonCol++].Text = rec.OrderDetail.Line_NO.ToString(); }
                    if (rec.OrderDetail.PurchaseUnitPrice == null) { CartonCol++; } else { sheet.Range[r.row, CartonCol++].Text = rec.OrderDetail.PurchaseUnitPrice.ToString(); }
                }
            }
            return rvalue;
        }



        public string LoadIMEIData()
        {
            string rvalue = "";
            if (isXLS == true) { rvalue = LoadIMEIDatafromXLS(); }
            return rvalue;
        }
        public string LoadIMEIDatafromXLS()
        {
            if (isXLS == false) { return "No File to Parse"; }
            string rvalue = "";
            rvalue = "No Order Entry Number given.";
            if (orderentrynumber.Trim().Length > 0)
            {
                rvalue = "";
                RowCount = 0;
                DateTime starttime = DateTime.Now;
                excelEngine = new ExcelEngine();
                //Step 2 : Instantiate the excel application object.
                application = excelEngine.Excel;
                //Open an existing spreadsheet which will be used as a template for generating the new spreadsheet.
                //After opening, the workbook object represents the complete in-memory object model of the template spreadsheet.
                workbook = application.Workbooks.Open(filename, ExcelOpenType.Automatic);
                //The first worksheet object in the worksheets collection is accessed.
                IWorksheet sheet = workbook.Worksheets[0];
                using (clsLinqDataContext ctx = new clsLinqDataContext())
                {
                    //rvalue = LoadHeaderData(sheet);
                    if (orderentrynumber.ToUpper() == "C1CON")
                    {
                        // C1CON - Data required:IMEI (A), Carton Number (B), Courier_Out (C), Out_Bound_Waybill_S (D), ShipTo (E), PO_No (F), Vendor_RMA (G), Unit_Destination (H), Replacement_SKU (I), RQ4_Transfer_Out (J) 

                        rvalue = LoadDataC1CON(ctx, sheet);
                    }
                    else
                    {
                        rvalue = LoadData(ctx, sheet);
                    }
                }
                //workbook.Close();
                //excelEngine.Dispose();
                DateTime endtime = DateTime.Now;
                TimeSpan diffResult = endtime.Subtract(starttime);
                if (rvalue.Length == 0)
                {
                    rvalue = "(HH:MM:SS)" + diffResult.Hours.ToString() + ":" + diffResult.Minutes.ToString() + ":" + diffResult.Seconds.ToString() + "  -- Row Count:" + RowCount.ToString();
                }
            }
            return rvalue;
        }

        private string LoadDataC1CON(clsLinqDataContext ctx, IWorksheet sheet)
        {
            //return "Inside Load Data";


            int ESNCol = 1;
            int CartonCol = 2;

            int Courier_OutCol = 3;
            int Out_Bound_Waybill_SCol = 4;
            int ShipToCol = 5;
            int PO_NoCol = 6;
            int Vendor_RMACol = 7;
            int Unit_DestinationCol = 8;
            int Replacement_SKUCol = 9;
            int RQ4_Transfer_OutCol = 10;

            int ErrorCol = 11;
            //OrderManager OM = new OrderManager(username);
            string rvalue = "";
            List<ESNRowData> Data = new List<ESNRowData>();
            ReceiveDetailManager rm = new ReceiveDetailManager(username);
            //OrderHeader OH = ctx.OrderHeaders.FirstOrDefault(x => x.OrderNumber == orderentrynumber);
            sheet.Range[1, ErrorCol].Text = "Status";
            //if (OH == null)
            //{
            //    sheet.Range[2, ErrorCol].Text = "Order Not Found:" + orderentrynumber;
            //    return "Order not found" + orderentrynumber;
            //}

            for (int row = 2; row < 1000000; row++)
            {
                if (sheet.Range[row, 1].Value == null || sheet.Range[row, ESNCol].Value.Length == 0) { break; }
                ESNRowData rd = new ESNRowData(sheet.Range[row, ESNCol].Value, sheet.Range[row, CartonCol].Value, row
                    , sheet.Range[row, Courier_OutCol].Value
                    , sheet.Range[row, Out_Bound_Waybill_SCol].Value
                    , sheet.Range[row, ShipToCol].Value
                    , sheet.Range[row, PO_NoCol].Value
                    , sheet.Range[row, Vendor_RMACol].Value
                    , sheet.Range[row, Unit_DestinationCol].Value
                    , sheet.Range[row, Replacement_SKUCol].Value
                    , sheet.Range[row, RQ4_Transfer_OutCol].Value
                    );
                rd.LoadDataC1CON(ctx);
                if (rd.ErrorMessage.Length > 0) { sheet.Range[row, ErrorCol].Text = rd.ErrorMessage; }
                else { Data.Add(rd); }
            }
            foreach (ESNRowData r in Data.Where(x => x.isFound == true))
            {
                string Message = "";
                bool Success = false;
                Success = rm.IFSMOVEShippedC1CONShipped(r.RD.ReceiveDetailID, r.Courier_Out, r.carton, r.Out_Bound_Waybill_S, r.ShipTo, r.PO_No, r.Vendor_RMA, r.Unit_Destination, r.Replacement_SKU, r.RQ4_Transfer_Out, ref Message);                                           // OM.RecordIFSPickIMEI(OH.OrderHeaderID, location.Text, r.carton, r.Esn, username);
                if (Message.Length > 5 && Message.Substring(0, 5).ToUpper() == "ERROR") { r.ErrorMessage = Message; }
                else { r.ErrorMessage = "C1CON Uploaded."; }
            }


            foreach (ESNRowData r in Data)
            {
                sheet.Range[r.row, ErrorCol].Text = r.ErrorMessage;
            }
            return rvalue;
        }


        private string LoadData(clsLinqDataContext ctx, IWorksheet sheet)
        {
            //return "Inside Load Data";


            int ESNCol = 1;
            int CartonCol = 2;
            int ErrorCol = 3;
            OrderManager OM = new OrderManager(username);
            string rvalue = "";
            List<ESNRowData> Data = new List<ESNRowData>();

            OrderHeader OH = ctx.OrderHeaders.FirstOrDefault(x => x.OrderNumber == orderentrynumber);
            sheet.Range[1, ErrorCol].Text = "Status";
            if (OH == null)
            {
                sheet.Range[2, ErrorCol].Text = "Order Not Found:" + orderentrynumber;
                return "Order not found" + orderentrynumber;
            }

            for (int row = 2; row < 1000000; row++)
            {
                if (sheet.Range[row, 1].Value == null || sheet.Range[row, ESNCol].Value.Length == 0) { break; }
                ESNRowData rd = new ESNRowData(sheet.Range[row, ESNCol].Value, sheet.Range[row, CartonCol].Value, row);
                rd.LoadData(ctx, OH.ProjectID);
                if (rd.ErrorMessage.Length > 0) { sheet.Range[row, ErrorCol].Text = rd.ErrorMessage; }
                else { Data.Add(rd); }
            }
            foreach (ESNRowData r in Data.Where(x => x.isFound == true))
            {
                string Message = "";
                Message = OM.RecordIFSPickIMEI(OH.OrderHeaderID, location.Text, r.carton, r.Esn, username);
                if (Message.Length > 5 && Message.Substring(0, 5).ToUpper() == "ERROR") { r.ErrorMessage = Message; }
                else { r.ErrorMessage = "Uploaded."; }
            }
            foreach (ESNRowData r in Data)
            {
                sheet.Range[r.row, ErrorCol].Text = r.ErrorMessage;
            }
            return rvalue;
        }


        private bool isESNAlreadyOnThisOrder(string ESN, OrderHeader OH)
        {
            foreach (var x in OH.OrderDetails)
            {
                foreach (var y in x.OrderDetailReceiveDetails)
                {
                    if (y.ESN == ESN) { return true; }
                }
            }

            //OrderDetailReceiveDetail odrd = OH.OrderDetailReceiveDetails.FirstOrDefault(x => x.OrderDetail.OrderHeader.OrderStatus.Status.ToUpper() != "TRASH" && x.ReceiveDetailID == rd.ReceiveDetailID);
            //if (odrd != null) { ErrorMessage = "ESN already on an Order (" + odrd.OrderDetail.OrderHeader.OrderNumber + ")"; return; }


            return false;
        }
        private string LoadHeaderData(IWorksheet sheet)
        {
            string rvalue = "";
            List<string> ESNDetail = new List<string>(new string[] { "ESN", "STARTPROCESSNAME", "CURRENTPROCESSNAME", "RECEIVEDETAILID", "CLIENTLOCATIONID", "CLIENTLOCATION", "RMANUMBER", "PROJECTTAG", "PROJECTNAME" });
            QuestionManager qm = new QuestionManager(username);
            List<PairIDValue> QID_Name = qm.GetAllQuestionsPairIDName(projectID);
            int row = 1;
            Header.Clear();
            for (int col = 1; col < 100000; col++)
            {
                if (sheet.Range[row, col].Value == null || sheet.Range[row, col].Value.Length == 0) { break; }
                HeaderData hd = new HeaderData(col, sheet.Range[row, col].Value);
                hd.Fill(qm, projectID, username, ESNDetail, QID_Name);
                //if (attributeclonelist.Contains(hd.text))  { hd.isCloneColumn = true; }
                Header.Add(hd);
            }
            return rvalue;
        }
        class ESNRowData
        {
            public string Esn = "";
            public ReceiveDetail RD = null;
            public int row = 0;
            public string ErrorMessage = "";
            public string carton = "";
            public decimal QTY = 0;
            public bool isFound = false;

            public string Courier_Out = "";
            public string Out_Bound_Waybill_S = "";
            public string ShipTo = "";
            public string PO_No = "";
            public string Vendor_RMA = "";
            public string Unit_Destination = "";
            public string Replacement_SKU = "";
            public string RQ4_Transfer_Out = "";



            public ESNRowData(string ESN, string Carton, int Row)
            {
                Esn = ESN;
                carton = Carton;
                row = Row;
                isFound = false;
            }

            public ESNRowData(string ESN, string Carton, int Row, string courier_Out, string out_Bound_Waybill_S, string shipTo, string pO_No, string vendor_RMA,
                   string unit_Destination,
                   string replacement_SKU, string rQ4_Transfer_Out)
            {
                Esn = ESN;
                carton = Carton;
                Courier_Out = courier_Out;
                Out_Bound_Waybill_S = out_Bound_Waybill_S;
                ShipTo = shipTo;
                PO_No = pO_No;
                Vendor_RMA = vendor_RMA;
                Unit_Destination = unit_Destination;
                Replacement_SKU = replacement_SKU;
                RQ4_Transfer_Out = rQ4_Transfer_Out;
                row = Row;
                isFound = false;

            }

            public void LoadData(clsLinqDataContext ctx, decimal ProjectID)
            {
                ReceiveDetail rd = ctx.ReceiveDetails.FirstOrDefault(x => x.ESN == Esn && x.Version == "000");
                if (rd == null) { ErrorMessage = "ESN not Found"; return; }
                if (ProjectID > 0 && rd.ProjectID != ProjectID) { ErrorMessage = "ESN under wrong Project (" + rd.ProjectName + ")"; return; }

                OrderDetailReceiveDetail odrd = ctx.OrderDetailReceiveDetails.FirstOrDefault(x => x.OrderDetail.OrderHeader.OrderStatus.Status.ToUpper() != "TRASH" && x.ReceiveDetailID == rd.ReceiveDetailID);
                if (odrd != null) { ErrorMessage = "ESN already on an Order (" + odrd.OrderDetail.OrderHeader.OrderNumber + ")"; return; }
                isFound = true;
                RD = rd;
                QTY = 1;
            }
            public void LoadDataC1CON(clsLinqDataContext ctx)
            {
                ReceiveDetail rd = ctx.ReceiveDetails.FirstOrDefault(x => x.ESN == Esn && x.Version == "000");
                if (rd == null) { ErrorMessage = "ESN not Found"; return; }
                if (rd.ClientLocation.IFSSite.ToUpper() != "C1CON") { ErrorMessage = "ESN not C1CON"; return; }


                //if (ProjectID > 0 && rd.ProjectID != ProjectID) { ErrorMessage = "ESN under wrong Project (" + rd.ProjectName + ")"; return; }
                //OrderDetailReceiveDetail odrd = ctx.OrderDetailReceiveDetails.FirstOrDefault(x => x.OrderDetail.OrderHeader.OrderStatus.Status.ToUpper() != "TRASH" && x.ReceiveDetailID == rd.ReceiveDetailID);
                //if (odrd != null) { ErrorMessage = "ESN already on an Order (" + odrd.OrderDetail.OrderHeader.OrderNumber + ")"; return; }

                isFound = true;
                RD = rd;
                QTY = 1;
            }
        }
    }


    public class IMEIPOSwitchUploadProcessor
    {
        public string filename { get; set; }
        public string username { get; set; }
        //public string orderentrynumber { get; set; }
        //string BinNumber { get; set; }
        //public decimal projectID { get; set; }
        public int RowCount { get; set; }
        public bool isXLS { get; set; }
        //public IFSLocation location { get; set; }
        //bool force15IMEI { get; set; }

        public ExcelEngine excelEngine = null;
        public IApplication application = null;
        public IWorkbook workbook = null;

        private List<HeaderData> Header = new List<HeaderData>();

        public IMEIPOSwitchUploadProcessor(string DrivePathFileNameXLS_toUpload, string UserName)
        {
            filename = DrivePathFileNameXLS_toUpload;
            isXLS = true;
            username = UserName;
        }

        #region Process Switched PO Data
        public string LoadIMEIPOSwitchData()
        {
            string rvalue = "";
            if (isXLS == true) { rvalue = LoadIMEIPOSwitchDatafromXLS(); }
            return rvalue;
        }
        public string LoadIMEIPOSwitchDatafromXLS()
        {
            if (isXLS == false) { return "No File to Parse"; }
            string rvalue = "";
            RowCount = 0;
            DateTime starttime = DateTime.Now;
            excelEngine = new ExcelEngine();
            //Step 2 : Instantiate the excel application object.
            application = excelEngine.Excel;
            //Open an existing spreadsheet which will be used as a template for generating the new spreadsheet.
            //After opening, the workbook object represents the complete in-memory object model of the template spreadsheet.
            workbook = application.Workbooks.Open(filename, ExcelOpenType.Automatic);
            //The first worksheet object in the worksheets collection is accessed.
            IWorksheet sheet = workbook.Worksheets[0];
            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                //rvalue = LoadHeaderData(sheet);
                rvalue = LoadPOSwitchData(ctx, sheet);
            }
            DateTime endtime = DateTime.Now;
            TimeSpan diffResult = endtime.Subtract(starttime);
            if (rvalue.Length == 0)
            {
                rvalue = "(HH:MM:SS)" + diffResult.Hours.ToString() + ":" + diffResult.Minutes.ToString() + ":" + diffResult.Seconds.ToString() + "  -- Row Count:" + RowCount.ToString();
            }
            return rvalue;
        }
        private string LoadPOSwitchData(clsLinqDataContext ctx, IWorksheet sheet)
        {
            //return "Inside Load Data";
            int ESNCol = 1;
            int POFromCol = 2;
            int POToCol = 3;
            int POLineToCol = 4;
            int ErrorCol = 5;
            string rvalue = "";
            List<ESNPOSwitchRowData> Data = new List<ESNPOSwitchRowData>();
            sheet.Range[1, ErrorCol].Text = "Status";
            ReceiveDetailManager rdm = new ReceiveDetailManager(username);
            for (int row = 2; row < 1000000; row++)
            {
                if (sheet.Range[row, 1].Value == null || sheet.Range[row, ESNCol].Value.Length == 0) { break; }
                ESNPOSwitchRowData rd = new ESNPOSwitchRowData(sheet.Range[row, ESNCol].Value, sheet.Range[row, POFromCol].Value, sheet.Range[row, POToCol].Value, sheet.Range[row, POLineToCol].Value, row);
                Data.Add(rd);
                //rd.LoadData(ctx, OH.ProjectID);
                //if (rd.ErrorMessage.Length > 0) { sheet.Range[row, ErrorCol].Text = rd.ErrorMessage; }
                //else { Data.Add(rd); }
            }
            foreach (ESNPOSwitchRowData r in Data.Where(x => x.isFound == true))
            {
                string Message = "";
                Message = rdm.SwitchPO(r.Esn, r.PONumberFrom, r.PONumberTo, r.POLineTo);
                if (Message.Length > 5 && Message.Substring(0, 5).ToUpper() == "ERROR") { r.ErrorMessage = Message; }
                else { r.ErrorMessage = "PO Changed:" + Message; }
            }
            foreach (ESNPOSwitchRowData r in Data)
            {
                sheet.Range[r.row, ErrorCol].Text = r.ErrorMessage;
            }
            return rvalue;
        }
        class ESNPOSwitchRowData
        {
            public string Esn = "";
            public ReceiveDetail RD = null;
            public int row = 0;
            public string ErrorMessage = "";
            public string PONumberFrom = "";
            public string PONumberTo = "";
            public string POLineTo = "";

            public bool isFound = false;

            public ESNPOSwitchRowData(string ESN, string POnumberFrom, string POnumberTo, string POlineTo, int Row)
            {
                Esn = ESN;
                PONumberFrom = POnumberFrom;
                PONumberTo = POnumberTo;
                POLineTo = POlineTo;
                row = Row;
                isFound = true;
            }
        }
        #endregion

    }

    public class IMEIRollIMEIUploadProcessor
    {
        public string filename { get; set; }
        public string username { get; set; }
        //public string orderentrynumber { get; set; }
        //public decimal projectID { get; set; }
        public int CountUploaded { get; set; }
        public int CountUpdated { get; set; }

        public bool isXLS { get; set; }
        bool forceIMEI15 { get; set; }



        //private List<HeaderData> Header = new List<HeaderData>();

        public IMEIRollIMEIUploadProcessor(string DrivePathFileNameXLS_toUpload, string UserName, bool ForceIMEI15)
        {
            filename = DrivePathFileNameXLS_toUpload;
            isXLS = true;
            forceIMEI15 = ForceIMEI15;
            username = UserName;
        }

        public string LoadIMEIData()
        {
            string rvalue = "";
            if (isXLS == true) { rvalue = LoadIMEIDatafromXLS(); }
            return rvalue;
        }

        public string LoadIMEIDatafromXLS()
        {
            string rvalue = "";
            //if (orderentrynumber.Trim().Length > 0)
            //{
            CountUploaded = 0;
            CountUpdated = 0;
            DateTime starttime = DateTime.Now;
            ExcelEngine excelEngine = new ExcelEngine();
            //Step 2 : Instantiate the excel application object.
            IApplication application = excelEngine.Excel;
            //Open an existing spreadsheet which will be used as a template for generating the new spreadsheet.
            //After opening, the workbook object represents the complete in-memory object model of the template spreadsheet.
            IWorkbook workbook = application.Workbooks.Open(filename, ExcelOpenType.Automatic);
            //The first worksheet object in the worksheets collection is accessed.
            IWorksheet sheet = workbook.Worksheets[0];
            //rvalue = LoadHeaderData(sheet);
            rvalue = LoadData(sheet);
            workbook.Close();
            excelEngine.Dispose();
            DateTime endtime = DateTime.Now;
            TimeSpan diffResult = endtime.Subtract(starttime);
            rvalue = "(HH:MM:SS)" + diffResult.Hours.ToString() + ":" + diffResult.Minutes.ToString() + ":" + diffResult.Seconds.ToString() + "  -- Row Count Loaded:" + CountUploaded.ToString() + ", Updated:" + CountUpdated.ToString();
            //}
            return rvalue;
        }


        private string LoadData(IWorksheet sheet)
        {
            string esn = "";
            string rvalue = "";
            List<string> ESNList = new List<string>();
            //ReceiveHeader rh = new ReceiveHeader();
            //// start on the second row
            for (int row = 2; row < 100000; row++)
            {
                if (sheet.Range[row, 1].Value == null || sheet.Range[row, 1].Value.Length == 0) { break; }

                esn = sheet.Range[row, 1].Value;
                if (forceIMEI15 == true && esn.Length != 15)
                {
                    if (esn.Length < 15) { esn = esn.PadLeft(15, '0'); }
                    if (esn.Length > 15) { esn = esn.Substring(0, 15); }

                }
                ESNList.Add(esn);


                //RowData rd = new RowData(projectID, -1, -1, -1, "", "", "");
                //for (int col = 1; col <= Header.Count; col++)
                //{
                //    rd.AddCellData(sheet.Range[row, col].Value, Header.FirstOrDefault(x => x.col == col));
                //}
                ////    /////////////////////////;
                //ReceiveDetail rdetail = null;
                //rdetail = rd.Save(username);
                //if (rdetail != null && rdetail.ESN.Trim().Length > 0)
                //{
                //    rdetail.StatusID = -1;
                //    if (RowCount == 0)
                //    {
                //        rh.ClientLocationID = rdetail.ClientLocationID;
                //        rh.CreateDate = rdetail.CreateDate;
                //        rh.CreateUser = rdetail.CreateUser;
                //        rh.LastUpdateDate = rdetail.LastUpdateDate;
                //        rh.LastUpdateUser = rdetail.LastUpdateUser;
                //        rh.MiscNote = rdetail.MiscNote;
                //        rh.ProjectID = rdetail.ProjectID;
                //        rh.ProjectName = rdetail.ProjectName;
                //        rh.QTYPaper = 0;
                //        rh.QTYRecorded = 0;
                //        rh.ReceiveDate = rdetail.ReceiveDate;
                //        rh.StatusID = rdetail.StatusID;
                //        rh.RMANumber = rdetail.RMANumber;
                //    }
                //    rh.ReceiveDetails.Add(rdetail);
                CountUploaded += 1;
                //}
            }

            using (clsLinqDataContext ctx = new clsLinqDataContext())
            {
                ReceiveDetailManager rm = new ReceiveDetailManager(username);
                foreach (string ESN in ESNList)
                {
                    if (ESN.Length > 0)
                    {
                        ctx.AdvanceESNVersion_CleanUpAndLog(ESN, "Excel Role", 1, username);
                        //ctx.AdvanceESNVersion_02(ESN, 1, username);
                        //decimal rid = rm.ReceiveDetailID_LastVersion(ctx, rdx.ESN);
                        //if (rid > 0)
                        //{
                        //    ctx.Utility_AddIMEIToOrderEntry(orderentrynumber, "", null, rid, rdx.ESN, username);
                        //}
                        CountUpdated += 1;
                    }
                }
            }
            return rvalue;
        }

        //private string LoadHeaderData(IWorksheet sheet)
        //{
        //    string rvalue = "";
        //    List<string> ESNDetail = new List<string>(new string[] { "ESN"});
        //    QuestionManager qm = new QuestionManager(username);
        //    List<PairIDValue> QID_Name = qm.GetAllQuestionsPairIDName(projectID);
        //    int row = 1;
        //    Header.Clear();
        //    for (int col = 1; col < 100000; col++)
        //    {
        //        if (sheet.Range[row, col].Value == null || sheet.Range[row, col].Value.Length == 0) { break; }
        //        HeaderData hd = new HeaderData(col, sheet.Range[row, col].Value);
        //        hd.Fill(qm, projectID, username, ESNDetail, QID_Name);
        //        //if (attributeclonelist.Contains(hd.text))  { hd.isCloneColumn = true; }
        //        Header.Add(hd);
        //    }
        //    return rvalue;
        //}
    }
}