using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using BW_WebApp.BarcodeUtils;
//using Factory_DataModel;
using BW_WebApp.DataManagers;

namespace BW_WebApp
{
    public partial class BagTag : System.Web.UI.Page
    {
        private bool isTest = false;
        clsLinqDataContext ctx = new clsLinqDataContext();
        string ReplacementESN = "";
        //private string ESN = "";
        decimal ReceiveDetailID = -1;
        private bool isThreaded = false;
        private bool bPrintMake = true;
        private bool bPrintDate = true;
        private bool bPrintWarranty = false;
        private bool bPrintClient = true;
        private bool bPrintRMA = false;
        private bool bPrintProjectTag = false;
        private bool bPrintCarrier = false;

        private bool bPrintDefective = false;
        private bool bPrintLoanerMasterClient = false;
        private bool bPrintLoanerStatus = false;
        private bool bPrintLoanerBin = false;
        private bool bPrintUnLockStatus = false;

        private bool bPrintProject = false;
        private bool bPrintType = true;

        private bool bPrintSerialNumber = false;
        private bool bPrintStartProcess = false;
        private bool bPrintStockColourCode = false;

        private bool bPrintClientReferenceNO = false;
        private JsonString jString = new JsonString();
        //private Hashtable dList = new Hashtable();
        //private List<JsonStringOptionKeyList> OptionKeyList = null;

        private ReceiveDetailManager rdm = null;
        private ReceiveDetail rd = null;

        private string MainStringData = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            RegularBagTag.Visible = false;
            RegularBagTag_02.Visible = false;
            RegularBagTag_03.Visible = false;
            RegularBagTag_03SKU.Visible = false;
            RegularBagTag_01UK.Visible = false;
            RegularBagTag_02UK.Visible = false;
            TestBagTag.Visible = false;
            RegularBagTag_03_02.Visible = false;
            RegularBagTag_03_03.Visible = false;
            RegularBagTag_03_04.Visible = false;

            LoanderBagTag.Visible = false;
            bPrintMake = true;
            bPrintDate = true;
            bPrintClient = true;
            bcSerialNumber.Visible = false;
            bPrintWarranty = false;
            bPrintRMA = false;
            bPrintProjectTag = false;
            bPrintProject = false;
            bPrintSerialNumber = false;
            bPrintStartProcess = false;
            bPrintType = true;
            lblFullText.Text = "";
            MainStringData = "";


            // July 30, 2014 Jody asked to have the window stay not auto print and close, just stay open.
            string StopAutoExecute = Request.QueryString.Get("SAX");
            StopAutoExit.Value = "";
            if (StopAutoExecute != null && StopAutoExecute.ToUpper() == "Y") { StopAutoExit.Value = "Y"; }
            ////////////////////////////////////////////////////////////////////////////////////////////////


            string ReportName = Request.QueryString.Get("RPT");

            if (ReportName == "RETURNPART")
            {
                string IsGoodText = Request.QueryString.Get("ISGOOD");
                ReturnedPart.Visible = true;
                PrintReturnPartTag(IsGoodText);
                return;
            }
            if (ReportName == "DEFECTIVERETURNPART")
            {
                //string IsGoodText = Request.QueryString.Get("ISGOOD");
                ReturnedPart.Visible = true;
                PrintDefectiveReturnPartTag();
                return;
            }

            if (ReportName == "BINTAG")
            {
                RegularBagTag.Visible = true;
                bPrintMake = false;
                bPrintDate = false;
                bPrintClient = false;
                PrintBinTag_01();
                return;
            }



            string sReceiveDetailID = Request.QueryString.Get("RDID");


            ////////////////////////////////////////////
            //ReportName = "RECEIVEBRIDGE";
            if (isTest == true)
            {
                //ReportName = "RECEIVETELUS";
                sReceiveDetailID = "2921";
            }

            //////////////////////////////////////////
            decimal RDID = -1;
            decimal.TryParse(sReceiveDetailID, out RDID);
            if (RDID < 1) { return; }
            ReceiveDetailID = RDID;
            if (Request.QueryString.Get("ISTHREAD") == "Y") { LoadThreadedData(); }

            rdm = new ReceiveDetailManager(User.Identity.Name);
            rd = rdm.ReceiveDetail(ctx, RDID);
            if (rd != null)
            {
                ReplacementESN = GetData("Replacement IMEI");
                lblReplaceESN_01.Text = ReplacementESN;
                ProjectManager pm = new ProjectManager(User.Identity.Name);
                decimal? pid = rd.ProjectID;
                if (pid == null) { pid = -1; }
                Project proj = pm.Get((decimal)pid);
                string bagTag = "";
                if (proj == null) { bagTag = "GENERAL"; }
                if (proj != null) { bagTag = proj.BagTagName.ToUpper(); }
                if (proj.Gather_RMANumber == true) { bPrintRMA = true; }
                if (proj.Gather_ProjectTag == true) { bPrintProjectTag = true; }
                //bagTag = "GENERAL";            
                if (isTest == true)
                {
                    bagTag = "RECEIVESKU";
                }
                if (bagTag == "IPHONE")
                {
                    RegularBagTag.Visible = true;
                    bPrintSerialNumber = true;
                    bPrintWarranty = true;
                    PrintTag_01();
                }
                else if (bagTag == "TAGTEST_01")
                {
                    TestBagTag.Visible = true;
                    PrintTag_BGTEST_01(1);
                }
                else if (bagTag == "TAGTEST_02")
                {
                    TestBagTag.Visible = true;
                    PrintTag_BGTEST_01(2);
                }
                else if (bagTag == "TAGTEST_03")
                {
                    TestBagTag.Visible = true;
                    PrintTag_BGTEST_01(3);
                }
                else if (bagTag == "TAGTEST_04")
                {
                    TestBagTag.Visible = true;
                    PrintTag_BGTEST_01(4);
                }
                else if (bagTag == "TAGTEST_05")
                {
                    TestBagTag.Visible = true;
                    PrintTag_BGTEST_01(5);
                }
                else if (bagTag == "CLIENT")
                {
                    RegularBagTag.Visible = true;
                    PrintClientTag();
                }

                else if (bagTag == "BRIDGE")
                {
                    RegularBagTag.Visible = true;
                    bPrintWarranty = true;
                    bPrintCarrier = true;
                    PrintTag_01();
                }
                else if (bagTag == "xx1")
                {
                    RegularBagTag.Visible = true;
                    PrintTag_01();
                }
                else if (bagTag == "PUBLIC")
                {
                    RegularBagTag.Visible = true;
                    bPrintRMA = true;
                    bPrintProject = true;
                    bPrintStartProcess = true;
                    bPrintDefective = true;
                    PrintTag_01();
                }
                else if (bagTag == "xx1")
                {
                    RegularBagTag.Visible = true;
                    bPrintRMA = true;
                    bPrintProject = true;
                    bPrintStartProcess = true;
                    bPrintMake = false;
                    bPrintClientReferenceNO = true;
                    PrintTag_01();
                }
                else if (bagTag == "LOANER")
                {
                    LoanderBagTag.Visible = true;
                    lblTagVersion_L.Text = "2.0     LOANER PRODUCT";
                    bPrintRMA = true;
                    bPrintProject = true;
                    bPrintStartProcess = true;
                    bPrintLoanerMasterClient = true;
                    bPrintLoanerStatus = true;
                    bPrintLoanerBin = true;
                    bPrintMake = true;
                    bPrintCarrier = true;
                    bPrintUnLockStatus = true;
                    PrintTag_Loaner();
                }
                else if (bagTag == "RECEIVEGENERAL")
                {
                    bPrintClientReferenceNO = true;
                    RegularBagTag.Visible = true;
                    bPrintCarrier = true;
                    PrintTag_01();
                }


                ////////////////////////////
                ///////////////
                else if (bagTag == "RECEIVEREDIAL")
                {
                    bPrintClientReferenceNO = true;
                    RegularBagTag_02.Visible = true;
                    bPrintCarrier = true;
                    PrintTag_02();
                }
                else if (bagTag == "RECEIVEBRIDGE")
                {
                    bPrintClientReferenceNO = true;
                    RegularBagTag_03.Visible = true;
                    bPrintCarrier = true;
                    PrintTag_03();
                }
                else if (bagTag == "RECEIVESKU")
                {
                    bPrintClientReferenceNO = true;
                    RegularBagTag_03SKU.Visible = true;
                    bPrintCarrier = true;
                    PrintTag_03SKU();
                }
                else if (bagTag == "RECEIVEUK")
                {
                    bPrintClientReferenceNO = true;
                    RegularBagTag_01UK.Visible = true;
                    bPrintCarrier = true;
                    PrintTag_uk_01();
                }
                else if (bagTag == "RECEIVEUK2")
                {
                    bPrintClientReferenceNO = true;
                    RegularBagTag_02UK.Visible = true;
                    bPrintCarrier = true;
                    PrintTag_uk_02();
                }
                else if (bagTag == "RECEIVETELUS")
                {
                    bPrintClientReferenceNO = true;
                    RegularBagTag_03_02.Visible = true;
                    bPrintCarrier = true;
                    PrintTag_03_02();
                }
                else if (bagTag == "RECEIVETELUS01")
                {
                    bPrintClientReferenceNO = true;
                    RegularBagTag_03_03.Visible = true;
                    bPrintCarrier = true;
                    PrintTag_03_03();
                }
                else if (bagTag == "RECEIVETELUS02")
                {
                    bPrintClientReferenceNO = true;
                    RegularBagTag_03_04.Visible = true;
                    bPrintCarrier = true;
                    PrintTag_03_04();
                }
                ///////////////////////////////
                //////////////////////////////


                else if (bagTag == "RECEIVEWITHCOLOUR")
                {
                    bPrintStockColourCode = true;
                    bPrintClientReferenceNO = true;
                    RegularBagTag.Visible = true;
                    bPrintCarrier = true;
                    PrintTag_01();
                }
                else if (bagTag.Length == 0)
                {
                    bPrintClientReferenceNO = true;
                    RegularBagTag.Visible = true;
                    bPrintCarrier = true;
                    PrintTag_01();
                }
                else
                {
                    TestLoadOther(bagTag);
                }
            }
        }
        protected void Page_Unload(object sender, EventArgs e)
        {
            ctx.Dispose();
        }

        #region AdjustedForThreaded
        private void LoadThreadedData()
        {
            // If it is older than two minutes, we don't want to use it. The threaded save has most likely finished.
            ReceiveDetail_BagtagPreSaveData r = ctx.ReceiveDetail_BagtagPreSaveDatas.FirstOrDefault(x => x.ReceiveDetailID == ReceiveDetailID && x.CreateDate > DateTime.Now.AddMinutes(-2));
            if (r != null)
            {
                isThreaded = true;
                jString = new JsonString(r.Data, true);
            }
        }
        private string GetData(string Key)
        {
            if (Key.ToUpper() == "CARRIER") { Key = "Provider"; }
            if (Key.ToUpper() == "SKU") { return rd.SKU; }
            if (Key.ToUpper() == "ESN") { return rd.ESN; }
            if (Key.ToUpper() == "IMEI") { return rd.ESN; }
            if (Key.ToUpper() == "RMANUMBER") { return rd.RMANumber; }
            if (Key.ToUpper() == "PROJECT") { return rd.Projects.Name; }
            if (Key.ToUpper() == "RECEIVEDATE") { return string.Format("{0:MM/dd/yyyy}", rd.ReceiveDate.ToString()); }

            if (Key.ToUpper() == "LASTUPDATEDATE") { return string.Format("{0:MM/dd/yyyy}", rd.LastUpdateDate.ToString()); }
            if (Key.ToUpper() == "LASTUPDATEUSER") { return rd.LastUpdateUser; }

            //string.Format("{0}", string.Format("{0:MM/dd/yyyy}", DateTime.Today.AddDays(-1)));
            if (Key.ToUpper() == "PROJECTTAG") { return rd.ProjectTag; }
            if (Key.ToUpper() == "CLIENTLOCATION") { return rd.ClientLocation.CompanyName; }
            if (Key.ToUpper() == "CLIENT") { return rd.ClientLocation.Client.CompanyName; }
            if (Key.ToUpper() == "SCANKEY") { return rd.ClientLocation.ScanKey; }

            if (isThreaded == true) { return GetThreadedSavedData(Key); }
            return GetDataFromDB(Key);
        }
        private string GetThreadedSavedData(string Key)
        {
            string rValue = jString.GetAllValues(Key, "Do GetDataFromDB");
            if (rValue != "Do GetDataFromDB") { return rValue; }
            return GetDataFromDB(Key);
        }
        private string GetDataFromDB(string Key)
        {
            return rdm.GetReceiveDetailItem_DataElement(ctx, ReceiveDetailID, Key);
        }
        #endregion





        #region TagTypes
        private void PrintBinTag_01()
        {
            string BinNumber = Request.QueryString.Get("BinNumber");
            if (BinNumber == null || BinNumber.Length == 0) { BinNumber = "TEST"; }
            bcBagtag.DataToEncode = "XBINX" + BinNumber;
            lblFullText.Visible = true;
            lblFullText.Text = WrappableText(MainStringData);
        }
        private void PrintClientTag()
        {
            if (ReceiveDetailID > 0)
            {
                if (rd != null)
                {
                    if (bPrintSerialNumber == true) { PrintSerialNumber(GetData("Serial Number")); }
                    string Make = "";
                    if (bPrintMake == true) { Make = GetData("Manufacturer") + " " + GetData("Model") + " " + GetData("Colour"); }
                    if (bPrintCarrier == true) { if (Make.Length > 0) { Make += " "; } Make += GetData("CARRIER"); }
                    if (bPrintMake == true || bPrintCarrier == true) { PrintData(Make); }
                    if (bPrintDate == true) { PrintData("Received:" + rd.CreateDate.ToShortDateString() + " " + rd.CreateDate.ToShortTimeString()); }
                    if (bPrintClient == true) { PrintClient(rd.ClientLocationID); }

                    if (bPrintWarranty == true) { PrintWarranty(GetData("WarrantyExpiry")); }
                    if (bPrintRMA == true) { PrintData(rd.RMANumber); }
                    if (bPrintProjectTag == true) { PrintData(rd.ProjectTag); }
                    if (bPrintProject == true) { PrintData(rdm.GetReceiveDetailProjectName(ReceiveDetailID)); }

                    if (bPrintDefective == true) { PrintData(GetData("Defective")); }
                    if (bPrintStartProcess == true) { PrintData(rdm.GetReceiveDetailStartProcessName(ctx, ReceiveDetailID)); }
                }
            }
            bcBagtag.DataToEncode = GetData("ESN");
            lblFullText.Visible = true;
            lblFullText.Text = WrappableText(MainStringData);
        }
        private void PrintTag_Loaner()
        {
            if (ReceiveDetailID > 0)
            {
                if (rd != null)
                {
                    string Make = "";
                    if (bPrintMake == true) { Make = GetData("Manufacturer") + " " + GetData("Model") + " " + GetData("Colour"); }
                    if (bPrintCarrier == true) { if (Make.Length > 0) { Make += " "; } Make += GetData("CARRIER"); }
                    if (bPrintMake == true || bPrintCarrier == true) { PrintData(Make); }
                    if (bPrintDate == true) { PrintData("Received:" + rd.CreateDate.ToShortDateString() + " " + rd.CreateDate.ToShortTimeString()); }

                    if (bPrintLoanerMasterClient == true) { PrintLoanerClient(rd.ClientLocationID); }
                    if (bPrintLoanerStatus == true) { PrintData("Loaner Status:" + GetData("Loaner Status")); }
                    if (bPrintUnLockStatus == true) { PrintData("Lock Status:" + GetData("Unlocked Status")); }
                    if (bPrintLoanerBin == true) { PrintData("Bin:" + GetData("Bin")); }

                    lblCondition01_L.Text = "Condition:" + rd.IFSCondition;
                    lblCarrier_L.Text = GetData("CARRIER");
                    lblLoanerType_L.Text = GetData("Loaner Type");
                    Location.Visible = false;
                }
            }
            bcBagtag_L.DataToEncode = GetData("ESN");
            lblFullText_L.Visible = true;
            lblFullText_L.Text = WrappableText(MainStringData);
        }
        private void PrintTag_01()
        {
            string ProjectName = "";
            if (ReceiveDetailID > 0)
            {
                if (rd != null)
                {
                    ProjectName = rdm.GetReceiveDetailProjectName(ctx, ReceiveDetailID);
                    lblTagVersion.Text = ProjectName;
                    lblReceiveDetail.Text = rd.ReceiveDetailID.ToString();
                    lblCondition01.Text = "Condition:" + rd.IFSCondition;
                    if (bPrintSerialNumber == true) { PrintSerialNumber(GetData("iSerial")); }
                    string Make = "";
                    if (bPrintMake == true) { Make = GetData("Manufacturer") + " " + GetData("Model") + " " + GetData("Colour"); }
                    if (bPrintCarrier == true) { if (Make.Length > 0) { Make += " "; } Make += GetData("CARRIER"); }
                    if (bPrintMake == true || bPrintCarrier == true) { PrintData(Make); }
                    if (bPrintDate == true) { PrintData("Received:" + rd.CreateDate.ToShortDateString() + " " + rd.CreateDate.ToShortTimeString()); }

                    if (bPrintLoanerMasterClient == true) { PrintLoanerClient(rd.ClientLocationID); }
                    if (bPrintLoanerStatus == true) { PrintData("Loaner Status:" + GetData("Loaner Status")); }
                    if (bPrintClient == true) { PrintClient(rd.ClientLocationID); }
                    if (bPrintType == true) { PrintData(GetData("Receipt Type")); }
                    if (bPrintWarranty == true) { PrintWarranty(GetData("WarrantyExpiry")); }
                    if (bPrintRMA == true) { PrintData(rd.RMANumber); }
                    if (bPrintProjectTag == true) { PrintData(rd.ProjectTag); }
                    if (bPrintProject == true) { PrintData(ProjectName); }
                    if (bPrintDefective == true) { PrintData(GetData("Defective")); }
                    if (bPrintStartProcess == true) { PrintData(rdm.GetReceiveDetailStartProcessName(ctx, ReceiveDetailID)); }
                    if (bPrintClientReferenceNO == true) { PrintData("" + GetData("Client Reference #")); }

                    if (bPrintStockColourCode == true)
                    {
                        MasterCarrierManufacturerModelColourManager mcm = new MasterCarrierManufacturerModelColourManager(User.Identity.Name);
                        MasterCarrierManufacturerLookup mcl = mcm.GetMasterCarrierManufacturerLookup(ctx, rd.ReceiveDetailID, rd.Carrier, rd.Manufacturer, rd.Model, rd.Colour);
                        if (mcl != null && mcl.Condition != null) { PrintData("Colour Code:" + mcl.Condition); }
                    }

                    Location.Visible = false;
                    ClientLocation cl = rdm.GetClientLocation(ctx, rd.ClientLocationID);
                    lblMasterClient.Text = cl.Client.CompanyName;
                    if (cl.ScanKey.Trim().Length > 0)
                    {
                        Location.Visible = true;
                        Location.DataToEncode = cl.ScanKey;
                    }
                }
            }
            bcBagtag.DataToEncode = GetData("ESN");
            lblFullText.Visible = true;
            lblFullText.Text = WrappableText(MainStringData);
        }
        private void PrintTag_02()
        {
            string ProjectName = "";
            if (ReceiveDetailID > 0)
            {
                if (rd != null)
                {
                    ProjectName = rdm.GetReceiveDetailProjectName(ctx, ReceiveDetailID);
                    lblTagVersion_02.Text = "2.0 " + ProjectName;
                    lblReceiveDetail_02.Text = rd.ReceiveDetailID.ToString();
                    lblCondition_02.Text = "Condition:" + rd.IFSCondition;
                    if (bPrintSerialNumber == true) { PrintSerialNumber(GetData("iSerial")); }
                    string Make = "";
                    if (bPrintMake == true) { Make = GetData("Manufacturer") + " " + GetData("Model") + " " + GetData("Colour"); }
                    if (bPrintCarrier == true) { if (Make.Length > 0) { Make += " "; } Make += GetData("CARRIER"); }
                    if (bPrintMake == true || bPrintCarrier == true) { PrintData(Make); }
                    if (bPrintDate == true) { PrintData("Received:" + rd.CreateDate.ToShortDateString() + " " + rd.CreateDate.ToShortTimeString()); }

                    if (bPrintLoanerMasterClient == true) { PrintLoanerClient(rd.ClientLocationID); }
                    if (bPrintLoanerStatus == true) { PrintData("Loaner Status:" + GetData("Loaner Status")); }
                    if (bPrintClient == true) { PrintClient(rd.ClientLocationID); }
                    if (bPrintType == true) { PrintData(GetData("Receipt Type")); }
                    if (bPrintWarranty == true) { PrintWarranty(GetData("WarrantyExpiry")); }
                    if (bPrintRMA == true) { PrintData(rd.RMANumber); }
                    if (bPrintProjectTag == true) { PrintData(rd.ProjectTag); }
                    if (bPrintProject == true) { PrintData(ProjectName); }
                    if (bPrintDefective == true) { PrintData(GetData("Defective")); }
                    if (bPrintStartProcess == true) { PrintData(rdm.GetReceiveDetailStartProcessName(ctx, ReceiveDetailID)); }
                    if (bPrintClientReferenceNO == true) { PrintData("" + GetData("Client Reference #")); }

                    //if (bPrintStockColourCode == true)
                    //{
                    //    MasterCarrierManufacturerModelColourManager mcm = new MasterCarrierManufacturerModelColourManager(User.Identity.Name);
                    //    MasterCarrierManufacturerLookup mcl = mcm.GetMasterCarrierManufacturerLookup(ctx, rd.ReceiveDetailID, rd.Carrier, rd.Manufacturer, rd.Model, rd.Colour);
                    //    if (mcl != null && mcl.Condition != null) { PrintData("Colour Code:" + mcl.Condition); }
                    //}

                    Location.Visible = false;
                    ClientLocation cl = rdm.GetClientLocation(ctx, rd.ClientLocationID);
                    lblMasterClient_02.Text = cl.Client.CompanyName;
                    //if (cl.ScanKey.Trim().Length > 0)
                    //{
                    MasterCarrierManufacturerModelColourManager mcm = new MasterCarrierManufacturerModelColourManager(User.Identity.Name);
                    MasterCarrierManufacturerLookup mcl = mcm.GetMasterCarrierManufacturerLookup(ctx, rd.ReceiveDetailID, rd.Carrier, rd.Manufacturer, rd.Model, rd.Colour);
                    if (mcl != null && mcl.Condition != null) { ML_STOCK_COLOUR_CODE_02.Text = mcl.Condition; }
                    Redial_GMP_Condition_02.Text = GetData("Redial GMP Condition");
                    //Location_02.Visible = true;
                    //Location_02.DataToEncode = cl.ScanKey;
                    //}
                }
            }
            bcBagtag_02.DataToEncode = GetData("ESN");
            lblFullText_02.Visible = true;
            lblFullText_02.Text = WrappableText(MainStringData);
        }






        //private void PrintTag_03_02()
        //{
        //    string ProjectName = "";

        //    string UPC_Code = "";
        //    string Telus_SKU = "";

        //    string Make = "";
        //    if (ReceiveDetailID > 0)
        //    {
        //        if (rd != null)
        //        {
        //            ProjectName = rdm.GetReceiveDetailProjectName(ctx, ReceiveDetailID);
        //            lblTagVersion_03_02.Text = "2.0 " + ProjectName;
        //            lblReceiveDetail_03_02.Text = ReceiveDetailID.ToString();
        //            if (bPrintSerialNumber == true) { PrintSerialNumber(GetData("iSerial")); }
        //            if (bPrintMake == true) { Make = GetData("Manufacturer") + " " + GetData("Model") + " " + GetData("Colour"); }
        //            if (bPrintCarrier == true) { if (Make.Length > 0) { Make += " "; } Make += GetData("CARRIER"); }
        //            if (bPrintMake == true || bPrintCarrier == true) { PrintData(Make); }
        //            if (bPrintDate == true) { PrintData("Received:" + rd.CreateDate.ToShortDateString() + " " + rd.CreateDate.ToShortTimeString()); }

        //            if (bPrintLoanerMasterClient == true) { PrintLoanerClient(rd.ClientLocationID); }
        //            if (bPrintLoanerStatus == true) { PrintData("Loaner Status:" + GetData("Loaner Status")); }
        //            if (bPrintClient == true) { PrintClient(rd.ClientLocationID); }
        //            if (bPrintType == true) { PrintData(GetData("Receipt Type")); }
        //            if (bPrintWarranty == true) { PrintWarranty(GetData("WarrantyExpiry")); }
        //            if (bPrintRMA == true) { PrintData(rd.RMANumber); }
        //            if (bPrintProjectTag == true) { PrintData(rd.ProjectTag); }
        //            if (bPrintProject == true) { PrintData(ProjectName); }
        //            if (bPrintDefective == true) { PrintData(GetData("Defective")); }
        //            if (bPrintStartProcess == true) { PrintData(rdm.GetReceiveDetailStartProcessName(ctx, ReceiveDetailID)); }
        //            if (bPrintClientReferenceNO == true) { PrintData("" + GetData("Client Reference #")); }

        //            //if (bPrintStockColourCode == true)
        //            //{
        //            //    MasterCarrierManufacturerModelColourManager mcm = new MasterCarrierManufacturerModelColourManager(User.Identity.Name);
        //            //    MasterCarrierManufacturerLookup mcl = mcm.GetMasterCarrierManufacturerLookup(ctx, rd.ReceiveDetailID, rd.Carrier, rd.Manufacturer, rd.Model, rd.Colour);
        //            //    if (mcl != null && mcl.Condition != null) { PrintData("Colour Code:" + mcl.Condition); }
        //            //}

        //            Location.Visible = false;
        //            ClientLocation cl = rdm.GetClientLocation(ctx, rd.ClientLocationID);
        //            lblMasterClient_03_02.Text = cl.Client.CompanyName;



        //            //Label1_03_02.Text = cl.ScanKey;
        //            MasterCarrierManufacturerModelColourManager mcm = new MasterCarrierManufacturerModelColourManager(User.Identity.Name);
        //            MasterCarrierManufacturerLookup mcl = mcm.GetMasterCarrierManufacturerLookup(ctx, rd.ReceiveDetailID, rd.Carrier, rd.Manufacturer, rd.Model, rd.Colour);
        //            //if (mcl != null && mcl.Condition != null) { ML_STOCK_COLOUR_CODE_03_02.Text = mcl.Condition; }


        //            RowlblFullText_03_02.Visible = true;
        //            //RowLabel1_03_02.Visible = true;
        //            //RowML_STOCK_COLOUR_CODE_03_02.Visible = true;
        //            RowlblReplaceESN_01_03_02.Visible = true;
        //            if (lblFullText_03_02.Text.Length == 0) { RowlblFullText_03_02.Visible = false; }
        //            //if (Label1_03_02.Text.Length == 0) { RowLabel1_03_02.Visible = false; }
        //            //if (ML_STOCK_COLOUR_CODE_03_02.Text.Length == 0) { RowML_STOCK_COLOUR_CODE_03_02.Visible = false; }
        //            if (lblReplaceESN_01_03_02.Text.Length == 0) { RowlblReplaceESN_01_03_02.Visible = false; }
        //            //Redial_GMP_Condition_03_02.Text = GetData("Redial GMP Condition");
        //        }
        //    }
        //    bcBagtag_03_02.DataToEncode = GetData("ESN");
        //    bcBagtag_bottom_03_02.DataToEncode = GetData("ESN");
        //    UPC_Code = GetData("UPC");
        //    Telus_SKU = GetData("Telus SKU");

        //    //if (UPC_Code.Length == 0) { UPC_Code = "12345678910";}
        //    //if (Telus_SKU.Length == 0) { Telus_SKU = "Telus SKU"; }

        //    UPC_bottom_03_02.DataToEncode = UPC_Code;
        //    lblTelusSKU_03_02.Text = Telus_SKU;
        //    lblTelusSKU_03_02.Visible = true;


        //    lblFullText_03_02.Visible = true;
        //    lblFullText_03_02.Text = WrappableText(MainStringData);
        //    lblFullText_bottom_03_02.Visible = true;
        //    lblFullText_bottom_03_02.Text = Make;
        //}
        private void PrintTag_BGTEST_01(int version)
        {
            string ProjectName = "";
            string UPC_Code = "";
            string Telus_SKU = "";
            string Make = "";
            string ESN = GetData("ESN");

            TableRow13.Visible = false;
            TableRow14.Visible = false;
            TableRow15.Visible = false;

            if (version > 0) { TableRow13.Visible = true; }
            if (version == 2) { TestBagTag.Height = new Unit(20, UnitType.Mm); }
            if (version > 2) { TableRow14.Visible = true; }
            if (version > 3) { TableRow15.Visible = true; }

            //TableRow13.Visible = false;
            UPC_Code = GetData("UPC");
            Telus_SKU = GetData("Telus SKU");

            if (isTest == true)
            {
                if (UPC_Code.Length == 0) { UPC_Code = "UPC"; }
                if (Telus_SKU.Length == 0) { Telus_SKU = "Telus SKU"; }
            }

            lblTestBagTag.Text = "Telus SKU:" + Telus_SKU;
            lblTestBagTag.Visible = true;

            lblUPC_TestBagTag.Text = "UPC:" + UPC_Code;
            UPC_TestBagTag.DataToEncode = UPC_Code;
            UPC_TestBagTag.Visible = true;
            if (UPC_Code.Length == 0)
            {
                UPC_TestBagTag.Visible = false;
            }
            lblIMEI_TestBagTag.Text = "IMEI:" + ESN;
            IMEI_TestBagTag.DataToEncode = ESN;
        }


        private void PrintTag_03_02()
        {
            string ProjectName = "";
            string UPC_Code = "";
            string Telus_SKU = "";
            string Make = "";
            string ESN = GetData("ESN");
            UPC_Code = GetData("UPC");
            Telus_SKU = GetData("Telus SKU");

            if (isTest == true)
            {
                if (UPC_Code.Length == 0) { UPC_Code = "PrintTag_03_02"; }
                if (Telus_SKU.Length == 0) { Telus_SKU = "Telus SKU"; }
            }

            lblTelusSKU_03_02.Text = "Telus SKU:" + Telus_SKU;
            lblTelusSKU_03_02.Visible = true;

            UPC_03_02.Text = "UPC:" + UPC_Code;
            UPC_bottom_03_02.DataToEncode = UPC_Code;
            UPC_bottom_03_02.Visible = true;
            if (UPC_Code.Length == 0)
            {
                UPC_bottom_03_02.Visible = false;
            }

            bcBagtag_03_02.DataToEncode = ESN;
            IMEI_03_02.Text = "IMEI:" + ESN;

        }



        private void PrintTag_uk_01()
        {
            string ProjectName = "";
            string Grade = "";
            string Data_Wiped = "";
            string Make = "";
            string Model = "";
            string Colour = "";
            string Memory = "";
            string Defects_1 = "";
            string Defects_2 = "";
            string Defects_3 = "";
            string Project_Tag = "";
            string ESN = GetData("ESN");
            Grade = GetData("Grade");
            Make = GetData("Manufacturer");
            Model = GetData("Model");
            Colour = GetData("Colour");
            Data_Wiped = GetData("Data Wiped");
            Memory = GetData("Memory");
            Defects_1 = GetData("Defects 1");
            Defects_2 = GetData("Defects 2");
            Defects_3 = GetData("Defects 3");
            Project_Tag = GetData("ProjectTag");

            if (isTest == true)
            {
                //if (UPC_Code.Length == 0) { UPC_Code = "PrintTag_03_02"; }
                //if (Telus_SKU.Length == 0) { Telus_SKU = "Telus SKU"; }
            }


            lblGrade_UK_01.Text = Grade;
            lblMake_UK_01.Text = Make;
            lblModel_UK_01.Text = Model;
            lblMemory_UK_01.Text = Memory;
            lblColour_UK_01.Text = Colour;


            bcBagtag_UK_01.DataToEncode = ESN;
            //IMEI_UK_01.Text = "IMEI:" + ESN;

            lblDefects_1_UK_01.Text = Defects_1;
            lblDefects_2_UK_01.Text = Defects_2;
            lblDefects_3_UK_01.Text = Defects_3;

            lblData_Wiped_UK_01.Text = "Data Wiped - " + Data_Wiped;

            lblProject_Tag_UK_01.Text = Project_Tag;

        }



        private void PrintTag_uk_02()
        {
            string ProjectName = "";
            string Grade = "";
            string Data_Wiped = "";
            string Make = "";
            string Model = "";
            string Colour = "";
            string Memory = "";
            string Model_Notes = "";
            string Defects_1 = "";
            string Defects_2 = "";
            string Defects_3 = "";
            string RECDATE = "";
            string Analysis = "";
            string Project_Tag = "";
            string ESN = GetData("ESN");
            string LastUpdateUser = "";
            string LUD = "";

            Grade = GetData("Grade");
            Make = GetData("Manufacturer");
            Model = GetData("Model");
            Colour = GetData("Colour");
            Data_Wiped = GetData("Data Wiped");
            Model_Notes = GetData("Model Notes");
            RECDATE = GetData("RECEIVEDATE");
            Memory = GetData("Memory");
            Analysis = GetData("Analysis");
            Defects_1 = GetData("Defects 1");
            Defects_2 = GetData("Defects 2");
            Defects_3 = GetData("Defects 3");
            Project_Tag = GetData("ProjectTag");
            LastUpdateUser = GetData("LASTUPDATEUSER");
            LUD = GetData("LASTUPDATEDATE");

            var RECEIVEDATEPARSE = DateTime.Parse(RECDATE);
            var RECEIVEDATE = RECEIVEDATEPARSE.ToString("MM/dd/yyyy");

            var LUDPARSE = DateTime.Parse(LUD);
            var LastUpdateDate = LUDPARSE.ToString("MM/dd/yyyy");

            if (isTest == true)
            {
                //if (UPC_Code.Length == 0) { UPC_Code = "PrintTag_03_02"; }
                //if (Telus_SKU.Length == 0) { Telus_SKU = "Telus SKU"; }
            }


            lblGrade_UK_02.Text = Grade;
            lblMake_UK_02.Text = Make;
            lblModel_UK_02.Text = Model;
            lblMemory_UK_02.Text = Memory;
            lblColour_UK_02.Text = Colour;
            lblModel_Notes_UK_02.Text = Model_Notes;


            bcBagtag_UK_02.DataToEncode = ESN;
            //IMEI_UK_02.Text = "IMEI:" + ESN;

            lblDefects_1_UK_02.Text = Defects_1;
            lblDefects_2_UK_02.Text = Defects_2;
            lblDefects_3_UK_02.Text = Defects_3;

            lblData_Wiped_UK_02.Text = "Data Wiped: " + Data_Wiped;
            lblAnalysis_UK_02.Text = "Analysis: " + Analysis;
            lblRECEIVEDATE_UK_02.Text = RECEIVEDATE;
            lblLastUpdateUser_UK_02.Text = LastUpdateUser;
            lblLastUpdateDate_UK_02.Text = LastUpdateDate;

            lblProject_Tag_UK_02.Text = Project_Tag;

        }



        private void PrintTag_03_03()
        {


            // RegularBagTag_03_02.pa

            string ProjectName = "";

            string UPC_Code = "";
            string Telus_SKU = "";

            string Make = "";
            string ESN = GetData("ESN");
            UPC_Code = GetData("UPC");
            Telus_SKU = GetData("Telus SKU");
            if (isTest == true)
            {
                if (UPC_Code.Length == 0) { UPC_Code = "PrintTag_03_03"; }
                if (Telus_SKU.Length == 0) { Telus_SKU = "Telus SKU"; }
            }
            lblTelusSKU_03_03.Text = "Telus SKU:" + Telus_SKU;
            lblTelusSKU_03_03.Visible = true;

            UPC_03_03.Text = "UPC:" + UPC_Code;
            UPC_bottom_03_03.DataToEncode = UPC_Code;
            UPC_bottom_03_03.Visible = true;
            if (UPC_Code.Length == 0)
            {
                UPC_bottom_03_03.Visible = false;
            }
            bcBagtag_03_03.DataToEncode = ESN;
            IMEI_03_03.Text = "IMEI:" + ESN;
        }
        private void PrintTag_03_04()
        {
            string UPC_Code = "";
            string Telus_SKU = "";
            string ESN = GetData("ESN");
            UPC_Code = GetData("UPC");
            Telus_SKU = GetData("Telus SKU");
            if (isTest == true)
            {
                if (UPC_Code.Length == 0) { UPC_Code = "PrintTag_03_04"; }
                if (Telus_SKU.Length == 0) { Telus_SKU = "Telus SKU"; }
            }

            lblTelusSKU_03_04.Text = "Telus SKU:" + Telus_SKU;
            lblTelusSKU_03_04.Visible = true;

            UPC_03_04.Text = "UPC:" + UPC_Code;
            UPC_bottom_03_04.DataToEncode = UPC_Code;
            UPC_bottom_03_04.Visible = true;
            if (UPC_Code.Length == 0)
            {
                UPC_bottom_03_04.Visible = false;
            }
            bcBagtag_03_04.DataToEncode = ESN;
            IMEI_03_04.Text = "IMEI:" + ESN;
        }

        private void PrintTag_03SKU()
        {
            string ProjectName = "";
            string Make = "";
            string ProviderPrductName = "";
            if (ReceiveDetailID > 0)
            {
                if (rd != null)
                {
                    ProjectName = rdm.GetReceiveDetailProjectName(ctx, ReceiveDetailID);
                    //lblTagVersion_03SKU.Text = "2.0 " + ProjectName;
                    //lblReceiveDetail_03SKU.Text = ReceiveDetailID.ToString();

                    //lblProjectName_03SKU.Text = rdm.GetReceiveDetailProjectName(ctx, ReceiveDetailID);
                    //bcBagtag_bottom_03SKU.TextAbove = "XXX-YYYddddd-YEG";
                    lblManModCol_03SKU.Text = GetData("Provider") + " " + GetData("Manufacturer") + " " + GetData("Model") + " " + GetData("Colour");


                    //ProviderPrductName = GetData("Provider");
                    //ProviderProject = "     " + GetData("Project");

                    //lblProvider_03SKU.Text = GetData("Provider");
                    lblProjectName_03SKU.Text = GetData("Project");
                    //lblProvider_03SKU.Text = ProviderPrductName;


                    lblvendor_03SKU.Text = GetData("Receipt Type");
                    lblProjectTag_03SKU.Text = GetData("ProjectTag");
                    lblReceiveDate_03SKU.Text = GetData("ReceiveDate");

                    if (isTest == true)
                    {
                        //if (lblProvider_03SKU.Text.Length == 0) { lblProvider_03SKU.Text = "Provider"; }
                        //if (lblProjectName_03SKU.Text.Length == 0) { lblProjectName_03SKU.Text = "Project"; }
                        if (lblvendor_03SKU.Text.Length == 0) { lblvendor_03SKU.Text = "Receipt Type"; }
                        if (lblProjectTag_03SKU.Text.Length == 0) { lblProjectTag_03SKU.Text = "Project Tag"; }
                        if (lblReceiveDate_03SKU.Text.Length == 0) { lblReceiveDate_03SKU.Text = "ReceiveDate"; }
                    }


                    //if (bPrintSerialNumber == true) { PrintSerialNumber(GetData("iSerial")); }
                    //if (bPrintMake == true) { Make = GetData("Manufacturer") + " " + GetData("Model") + " " + GetData("Colour"); }
                    //if (bPrintCarrier == true) { if (Make.Length > 0) { Make += " "; } Make += GetData("CARRIER"); }
                    //if (bPrintMake == true || bPrintCarrier == true) { PrintData(Make); }
                    //if (bPrintDate == true) { PrintData("Received:" + rd.CreateDate.ToShortDateString() + " " + rd.CreateDate.ToShortTimeString()); }

                    //if (bPrintLoanerMasterClient == true) { PrintLoanerClient(rd.ClientLocationID); }
                    //if (bPrintLoanerStatus == true) { PrintData("Loaner Status:" + GetData("Loaner Status")); }
                    //if (bPrintClient == true) { PrintClient(rd.ClientLocationID); }
                    //if (bPrintType == true) { PrintData(GetData("Receipt Type")); }
                    //if (bPrintWarranty == true) { PrintWarranty(GetData("WarrantyExpiry")); }
                    //if (bPrintRMA == true) { PrintData(rd.RMANumber); }
                    //if (bPrintProjectTag == true) { PrintData(rd.ProjectTag); }
                    //if (bPrintProject == true) { PrintData(ProjectName); }
                    //if (bPrintDefective == true) { PrintData(GetData("Defective")); }
                    //if (bPrintStartProcess == true) { PrintData(rdm.GetReceiveDetailStartProcessName(ctx, ReceiveDetailID)); }
                    //if (bPrintClientReferenceNO == true) { PrintData("" + GetData("Client Reference #")); }

                    //if (bPrintStockColourCode == true)
                    //{
                    //    MasterCarrierManufacturerModelColourManager mcm = new MasterCarrierManufacturerModelColourManager(User.Identity.Name);
                    //    MasterCarrierManufacturerLookup mcl = mcm.GetMasterCarrierManufacturerLookup(ctx, rd.ReceiveDetailID, rd.Carrier, rd.Manufacturer, rd.Model, rd.Colour);
                    //    if (mcl != null && mcl.Condition != null) { PrintData("Colour Code:" + mcl.Condition); }
                    //}

                    //Location.Visible = false;
                    //ClientLocation cl = rdm.GetClientLocation(ctx, rd.ClientLocationID);
                    //lblMasterClient_03SKU.Text = cl.Client.CompanyName;
                    //Label1_03SKU.Text = cl.ScanKey;
                    //MasterCarrierManufacturerModelColourManager mcm = new MasterCarrierManufacturerModelColourManager(User.Identity.Name);
                    //MasterCarrierManufacturerLookup mcl = mcm.GetMasterCarrierManufacturerLookup(ctx, rd.ReceiveDetailID, rd.Carrier, rd.Manufacturer, rd.Model, rd.Colour);
                    //if (mcl != null && mcl.Condition != null) { ML_STOCK_COLOUR_CODE_03SKU.Text = mcl.Condition; }


                    //RowlblFullText_03SKU.Visible = true;
                    //RowLabel1_03SKU.Visible = true;
                    //RowML_STOCK_COLOUR_CODE_03SKU.Visible = true;
                    //RowlblReplaceESN_01_03SKU.Visible = true;
                    //if (lblFullText_03SKU.Text.Length == 0) { RowlblFullText_03SKU.Visible = false; }
                    //if (Label1_03SKU.Text.Length == 0) { RowLabel1_03SKU.Visible = false; }
                    //if (ML_STOCK_COLOUR_CODE_03SKU.Text.Length == 0) { RowML_STOCK_COLOUR_CODE_03SKU.Visible = false; }
                    //if (lblReplaceESN_01_03SKU.Text.Length == 0) { RowlblReplaceESN_01_03SKU.Visible = false; }
                    //Redial_GMP_Condition_03SKU.Text = GetData("Redial GMP Condition");
                }
            }
            bcBagtag_03SKU.DataToEncode = GetData("ESN").Trim();
            bcBagtag_bottom_03SKU.DataToEncode = GetData("SKU").Trim();

            //lblFullText_03SKU.Visible = true;
            //lblFullText_03SKU.Text = WrappableText(MainStringData);
            //lblFullText_bottom_03SKU.Visible = true;
            //lblFullText_bottom_03SKU.Text = Make;
        }

        private void PrintTag_03()
        {
            string ProjectName = "";
            string Make = "";
            if (ReceiveDetailID > 0)
            {
                if (rd != null)
                {
                    ProjectName = rdm.GetReceiveDetailProjectName(ctx, ReceiveDetailID);
                    lblTagVersion_03.Text = "2.0 " + ProjectName;
                    lblReceiveDetail_03.Text = ReceiveDetailID.ToString();
                    if (bPrintSerialNumber == true) { PrintSerialNumber(GetData("iSerial")); }
                    if (bPrintMake == true) { Make = GetData("Manufacturer") + " " + GetData("Model") + " " + GetData("Colour"); }
                    if (bPrintCarrier == true) { if (Make.Length > 0) { Make += " "; } Make += GetData("CARRIER"); }
                    if (bPrintMake == true || bPrintCarrier == true) { PrintData(Make); }
                    if (bPrintDate == true) { PrintData("Received:" + rd.CreateDate.ToShortDateString() + " " + rd.CreateDate.ToShortTimeString()); }

                    if (bPrintLoanerMasterClient == true) { PrintLoanerClient(rd.ClientLocationID); }
                    if (bPrintLoanerStatus == true) { PrintData("Loaner Status:" + GetData("Loaner Status")); }
                    if (bPrintClient == true) { PrintClient(rd.ClientLocationID); }
                    if (bPrintType == true) { PrintData(GetData("Receipt Type")); }
                    if (bPrintWarranty == true) { PrintWarranty(GetData("WarrantyExpiry")); }
                    if (bPrintRMA == true) { PrintData(rd.RMANumber); }
                    if (bPrintProjectTag == true) { PrintData(rd.ProjectTag); }
                    if (bPrintProject == true) { PrintData(ProjectName); }
                    if (bPrintDefective == true) { PrintData(GetData("Defective")); }
                    if (bPrintStartProcess == true) { PrintData(rdm.GetReceiveDetailStartProcessName(ctx, ReceiveDetailID)); }
                    if (bPrintClientReferenceNO == true) { PrintData("" + GetData("Client Reference #")); }

                    //if (bPrintStockColourCode == true)
                    //{
                    //    MasterCarrierManufacturerModelColourManager mcm = new MasterCarrierManufacturerModelColourManager(User.Identity.Name);
                    //    MasterCarrierManufacturerLookup mcl = mcm.GetMasterCarrierManufacturerLookup(ctx, rd.ReceiveDetailID, rd.Carrier, rd.Manufacturer, rd.Model, rd.Colour);
                    //    if (mcl != null && mcl.Condition != null) { PrintData("Colour Code:" + mcl.Condition); }
                    //}

                    Location.Visible = false;
                    ClientLocation cl = rdm.GetClientLocation(ctx, rd.ClientLocationID);
                    lblMasterClient_03.Text = cl.Client.CompanyName;
                    Label1_03.Text = cl.ScanKey;
                    MasterCarrierManufacturerModelColourManager mcm = new MasterCarrierManufacturerModelColourManager(User.Identity.Name);
                    MasterCarrierManufacturerLookup mcl = mcm.GetMasterCarrierManufacturerLookup(ctx, rd.ReceiveDetailID, rd.Carrier, rd.Manufacturer, rd.Model, rd.Colour);
                    if (mcl != null && mcl.Condition != null) { ML_STOCK_COLOUR_CODE_03.Text = mcl.Condition; }


                    RowlblFullText_03.Visible = true;
                    RowLabel1_03.Visible = true;
                    RowML_STOCK_COLOUR_CODE_03.Visible = true;
                    RowlblReplaceESN_01_03.Visible = true;
                    if (lblFullText_03.Text.Length == 0) { RowlblFullText_03.Visible = false; }
                    if (Label1_03.Text.Length == 0) { RowLabel1_03.Visible = false; }
                    if (ML_STOCK_COLOUR_CODE_03.Text.Length == 0) { RowML_STOCK_COLOUR_CODE_03.Visible = false; }
                    if (lblReplaceESN_01_03.Text.Length == 0) { RowlblReplaceESN_01_03.Visible = false; }
                    //Redial_GMP_Condition_03.Text = GetData("Redial GMP Condition");
                }
            }
            bcBagtag_03.DataToEncode = GetData("ESN");
            bcBagtag_bottom_03.DataToEncode = GetData("ESN");
            lblFullText_03.Visible = true;
            lblFullText_03.Text = WrappableText(MainStringData);
            lblFullText_bottom_03.Visible = true;
            lblFullText_bottom_03.Text = Make;
        }

        private void PrintDefectiveReturnPartTag()
        {
            string sMasterPartsTechAssignedID = Request.QueryString.Get("ID");
            decimal MasterPartsTechAssignedID = -1;
            if (decimal.TryParse(sMasterPartsTechAssignedID, out MasterPartsTechAssignedID) == false) { MasterPartsTechAssignedID = -1; }
            MasterPartManager pm = new MasterPartManager(User.Identity.Name);
            using (clsLinqDataContext ctx = pm.GetDataContext(User.Identity.Name))
            {
                MasterPartsTechAssignedLog ta = ctx.MasterPartsTechAssignedLogs.FirstOrDefault(x => x.MasterPartsTechAssignedLogID == MasterPartsTechAssignedID);
                if (ta != null)
                {
                    ReceiveDetail rd = ctx.ReceiveDetails.FirstOrDefault(x => x.ReceiveDetailID == ta.ReceiveDetailID);
                    if (rd != null)
                    {
                        bcReturnedPartIMEI.DataToEncode = rd.ESN;
                        //lblReturnedPartIMEI.Text = rd.ESN;
                        //lblReturnedPartPartNumber.Text = ta.GMPPartNumber;
                        bcReturnedPartPartNumber.DataToEncode = ta.GMPPartNumber;
                        lblReturnedPartTech.Text = ta.TechName;
                        lblReturnedPartDate.Text = DateTime.Now.ToString("MM/dd/yyyy @ HH:mm:ss");
                        bcReturnedPartTechAssignedID.DataToEncode = ta.MasterPartsTechAssignedLogID.ToString();
                        lblReturnType.Text = "DEFECTIVE";
                    }
                }
            }
        }
        private void PrintReturnPartTag(string IsGoodText)
        {
            string sMasterPartsTechAssignedID = Request.QueryString.Get("ID");
            decimal MasterPartsTechAssignedID = -1;
            if (decimal.TryParse(sMasterPartsTechAssignedID, out MasterPartsTechAssignedID) == false) { MasterPartsTechAssignedID = -1; }
            MasterPartManager pm = new MasterPartManager(User.Identity.Name);
            using (clsLinqDataContext ctx = pm.GetDataContext(User.Identity.Name))
            {
                MasterPartsTechAssignedLog ta = ctx.MasterPartsTechAssignedLogs.FirstOrDefault(x => x.MasterPartsTechAssignedLogID == MasterPartsTechAssignedID);
                if (ta != null)
                {
                    ReceiveDetail rd = ctx.ReceiveDetails.FirstOrDefault(x => x.ReceiveDetailID == ta.ReceiveDetailID);
                    if (rd != null)
                    {
                        bcReturnedPartIMEI.DataToEncode = rd.ESN;
                        //lblReturnedPartIMEI.Text = rd.ESN;
                        //lblReturnedPartPartNumber.Text = ta.GMPPartNumber;
                        bcReturnedPartPartNumber.DataToEncode = ta.GMPPartNumber;
                        lblReturnedPartTech.Text = ta.TechName;
                        lblReturnedPartDate.Text = DateTime.Now.ToString("MM/dd/yyyy @ HH:mm:ss");
                        bcReturnedPartTechAssignedID.DataToEncode = ta.MasterPartsTechAssignedLogID.ToString();
                        lblReturnType.Text = IsGoodText;
                    }
                }
            }



        }

        #region JodyBuiltHTMLFiles
        private void TestLoadOther(string Filename)
        {
            //string Path = "";
            string filePath = Page.MapPath(@"~/Templates/HTML/" + Filename + ".htm"); ;
            string xhtml = "";
            if (File.Exists(filePath) == true)
            {
                StreamReader streamReader = new StreamReader(filePath);
                xhtml = streamReader.ReadToEnd();
                streamReader.Close();
            }
            else
            {
                xhtml = "TEMPLATE NOT FOUND:" + Filename;
            }

            P_OTHER.Visible = true;
            ////////////string xhtml = "";
            ////////////xhtml = " <div id='Div1'>";
            ////////////xhtml += "<table id='Table3' cellspacing='0' cellpadding='0' border='0' style='width: 62mm;";
            ////////////xhtml += "    border-collapse: collapse;'>";
            ////////////xhtml += "    <tr id='Tr1'>";
            ////////////xhtml += "        <td colspan='2'>";
            ////////////xhtml += "            <span id='Span1' style='font-size: Smaller;'>2.0 Stock for Sale</span><br />";
            ////////////xhtml += "            <span id='Span2' style='font-size: Smaller;'>Goldie Group</span>";
            ////////////xhtml += "            <br />";
            ////////////xhtml += " <!--AttBar:ESN--><br />";
            ////////////xhtml += "        </td>";
            ////////////xhtml += "    </tr>";
            ////////////xhtml += "    <tr id='Tr2'>";
            ////////////xhtml += "        <td colspan='2'>";
            ////////////xhtml += "        </td>";
            ////////////xhtml += "    </tr>";
            ////////////xhtml += "    <tr>";
            ////////////xhtml += "        <td align='left' valign='top'>";
            ////////////xhtml += "        </td>";
            ////////////xhtml += "        <td>";
            ////////////xhtml += "            <span id='Span3' style='font-size: Smaller;'>";
            ////////////xhtml += "                <p>";
            ////////////xhtml += " Make:<!--Att:Manufacturer--><br />";
            ////////////xhtml += " Model:<!--Att:Model--><br />";
            ////////////xhtml += " Colour:<!--Att:Colour--><br />";
            ////////////xhtml += " Carrier:<!--Att:CARRIER--><br />";
            ////////////xhtml += " Process:<!--Att:Process--><br />";
            //////////////xhtml += "                    Nokia Black<br />";
            //////////////xhtml += "                    Received:06/15/2012 9:15 PM<br />";
            //////////////xhtml += "                    Goldie Group<br />";
            //////////////xhtml += "                    JIM LG TEST<br />";
            //////////////xhtml += "                    Stock for Sale<br />";
            ////////////xhtml += "</p>";
            ////////////xhtml += "            </span>";
            ////////////xhtml += " <!--AttBar:Project--><br />";
            ////////////xhtml += "        </td>";
            ////////////xhtml += "    </tr>";
            ////////////xhtml += "    <tr>";
            ////////////xhtml += "        <td colspan='2'>";
            ////////////xhtml += "            <span id='Span4' style='font-size: X-Small;'>ClientID</span><br />";
            ////////////xhtml += " <!--AttBar:ESN--><br />";
            ////////////xhtml += "        </td>";
            ////////////xhtml += "    </tr>";
            ////////////xhtml += "    <tr>";
            ////////////xhtml += "        <td colspan='2'>";
            ////////////xhtml += "            <br />";
            ////////////xhtml += "            <br />";
            ////////////xhtml += "            <span id='Span5' style='font-size: Small;'></span>";
            ////////////xhtml += "            <br />";
            ////////////xhtml += "        </td>";
            ////////////xhtml += "    </tr>";
            ////////////xhtml += "</table>";
            ////////////xhtml += "</div>";


            //string am = "";
            // Get a list of all the <!--xxx--> elements.
            string Pattern = @"\<![ \r\n\t]*(--([^\-]|[\r\n]|-[^\-])*--[ \r\n\t]*)\>"; // Patern to find Comments
            MatchCollection match = Regex.Matches(xhtml, Pattern, RegexOptions.IgnoreCase);
            foreach (Match m in match)
            {
                string s = "";
                if (m.Value.Contains("AttBar:") == true)
                {
                    s = m.Value.Replace("<!--", "").Replace("-->", "").Replace("AttBar:", "");
                    s = GetData(s);
                    s = "<img height='47px' width='144px' src='" + GetBarcodeFileName(s) + "' id='Img2' style='background-color: White; font-family: Times New Roman; font-size: 10pt; font-weight: normal; font-style: normal; text-decoration: none; height: 47px; width: 144px;' />";
                }
                else if (m.Value.Contains("Att:") == true)
                {
                    s = m.Value.Replace("<!--", "").Replace("-->", "").Replace("Att:", "");
                    s = GetData(s);
                }

                xhtml = xhtml.Replace(m.Value, s);
                //am += m.Value.Replace("<!--","GG").Replace("-->","gg");
            }
            //xhtml += "<br /><br />zzzzzzzzz<br />";
            //xhtml += am;
            //xhtml += "</div>";

            P_OTHERDIV.InnerHtml = xhtml;

        }
        #endregion
        #endregion


        // ---------------------------------------------------
        private void PrintData(string DataString)
        {
            if (DataString.Trim().Length > 0)
            {
                if (MainStringData.Length > 0)
                {
                    MainStringData += Environment.NewLine;
                }
                MainStringData += DataString;
            }
        }
        private void PrintWarranty(string DateString)
        {
            DateTime xDate = DateTime.Parse("01/01/1950");
            DateTime date = DateTime.Now;
            if (DateTime.TryParse(DateString, out date) == true)
            {
                if (date != xDate)
                {
                    if (MainStringData.Length > 0)
                    {
                        MainStringData += Environment.NewLine;
                    }
                    MainStringData += "Warranty expiry:" + DateString;
                }
            }
        }
        private void PrintLoanerClient(decimal ClientLocationID)
        {
            Client cli = rdm.GetClientFromLocation(ClientLocationID);
            //ClientLocation cl = rdm.GetClientLocation(ClientLocationID);
            if (MainStringData.Length > 0)
            {
                MainStringData += Environment.NewLine;
            }
            MainStringData += "Loaner for:" + cli.CompanyName.Trim();
        }
        private void PrintClient(decimal ClientLocationID)
        {
            //Client cli = rdm.GetClientFromLocation(ClientLocationID);
            ClientLocation cl = rdm.GetClientLocation(ClientLocationID);
            if (MainStringData.Length > 0)
            {
                MainStringData += Environment.NewLine;
            }
            MainStringData += cl.CompanyName.Trim();
        }
        private void PrintSerialNumber(string SerialString)
        {
            if (SerialString.Trim().Length > 0)
            {
                bcSerialNumber.Visible = true;
                bcSerialNumber.DataToEncode = SerialString;
            }

        }
        //-------------------------------------------------
        private string GetBarcodeFileName(string Value)
        {
            clsBarcodeUtils bc = new clsBarcodeUtils();
            // bc.
            string FileName = "IDAutomation/BC" + "_" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + RandomNumber(0, 9).ToString() + ".jpg";
            string templatePath = Page.MapPath(@"~/" + FileName);

            bc.SaveBarcodeToFile(Value, templatePath);
            // IDAutomation/BC56352185261567235311082328971.jpg
            //System.Drawing.Image IMG = bc.SaveBarcodeToImage_Base_02(Value, templatePath);
            return FileName;
        }
        private int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
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