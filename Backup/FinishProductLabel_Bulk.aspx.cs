using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using BW_WebApp.BarcodeUtils;
using System.Text.RegularExpressions;
using IDAutomation.LinearServerControl;
//using System.Collections;

//using Factory_DataModel;
using BW_WebApp.DataManagers;
// using DAL;

//using MathFunctions;

namespace BW_WebApp
{
    public partial class FinishProductLabel_Bulk : System.Web.UI.Page
    {
        clsLinqDataContext ctx = new clsLinqDataContext();
        //public decimal TranslateParameter(string field)
        //{
        //    //field = field.Replace('%', ' ').Trim();
        //    switch (field.ToUpper())
        //    {
        //        case "BOB":
        //            return 2;
        //        case "BILL":
        //            return 3;
        //        case "GEORGE":
        //            return 4; 
        //        default:
        //            return 0;
        //    }
        //}

        private ReceiveDetailManager rdm = null;
        private ClientLocationManager clm = null;
        private ProjectManager pm = null;
        private MasterCarrierManufacturerModelColourManager mcm = null;
        private ReceiveDetail rd = null;
        private Client cl= null;
        private Project proj = null;
        private MasterCarrierManufacturerLookup mcl = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            //string ReportName = Request.QueryString.Get("RPT");
            string RequestUser = User.Identity.Name;
            Lablels.ItemDataBound += new RepeaterItemEventHandler(Lablels_ItemDataBound);

            //LabelSetup(ReceiveDetailID);
            rdm = new ReceiveDetailManager(User.Identity.Name);
            clm = new ClientLocationManager(User.Identity.Name);
            pm = new ProjectManager(User.Identity.Name);
            mcm = new MasterCarrierManufacturerModelColourManager(User.Identity.Name);

            List<ReceiveDetailHobbleList> hobble = new List<ReceiveDetailHobbleList>();
            hobble = rdm.GetHobbleList("KL",RequestUser);
            Lablels.DataSource = hobble;
            Lablels.DataBind();
            rdm.DeleteHobbleList("KL", RequestUser);
        }

        void Lablels_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ReceiveDetailHobbleList data = (ReceiveDetailHobbleList)e.Item.DataItem;
                if (data.ReceiveDetailID == null || data.ReceiveDetailID < 1)
                {
                    return;
                }
                Panel KIT_FWDLOG = (Panel)e.Item.FindControl("KIT_FWDLOG");
                Panel KIT_WIND = (Panel)e.Item.FindControl("KIT_WIND");
                Panel FTP_WIND = (Panel)e.Item.FindControl("FTP_WIND");
                Panel FPL_APPLE = (Panel)e.Item.FindControl("FPL_APPLE");
                Panel FPL_Staple = (Panel)e.Item.FindControl("FPL_Staple");
                Panel FPL_01 = (Panel)e.Item.FindControl("FPL_01");
                Panel FPL_02 = (Panel)e.Item.FindControl("FPL_02");
                Panel FPL_03 = (Panel)e.Item.FindControl("FPL_03");
                Panel FPL_04 = (Panel)e.Item.FindControl("FPL_04");
                Panel FPL_05 = (Panel)e.Item.FindControl("FPL_05");
                Panel FPL_06 = (Panel)e.Item.FindControl("FPL_06");
                Panel FPL_07 = (Panel)e.Item.FindControl("FPL_07");
                Panel FPL_BB = (Panel)e.Item.FindControl("FPL_BB");
                KIT_FWDLOG.Visible = false;
                KIT_WIND.Visible = false;
                FTP_WIND.Visible = false;
                FPL_APPLE.Visible = false;
                FPL_01.Visible = false;
                FPL_02.Visible = false;
                FPL_03.Visible = false;
                FPL_04.Visible = false;
                FPL_05.Visible = false;
                FPL_06.Visible = false;
                FPL_07.Visible = false;
                FPL_BB.Visible = false;
                FPL_Staple.Visible = false;

                decimal RDID = -1;
                RDID = (decimal)data.ReceiveDetailID;
                //rdm = new ReceiveDetailManager(User.Identity.Name);
                rd = rdm.ReceiveDetail(RDID);
                if (rd != null)
                {
                    string Manufacturer = "";
                    // We have a special lable just for apple computers.
                    if (rd.Manufacturer == null || rd.Manufacturer.Length == 0) { Manufacturer = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Manufacturer").ToUpper(); }
                    else { Manufacturer = rd.Manufacturer.ToUpper(); }

                    cl = clm.GetClientThisLocation(ctx, rd.ClientLocationID);
                    proj = pm.Get((decimal)rd.ProjectID);
                    string TagName = "";
                    if (proj != null && proj.ProductTag != null && proj.ProductTag.Length > 0) { TagName = proj.ProductTag.ToUpper(); }
                    else if (cl != null && cl.ProductTag != null && cl.ProductTag.Length > 0) { TagName = cl.ProductTag.ToUpper(); }

                    // Jody asked to exclude client 91 from the Apple restriction.
                    if (Manufacturer == "APPLE" && TagName != "FPT_BB" && cl.ClientID != 91) { PrintTag_APPLE(RDID, e.Item); return; }

                    //TagName = "FPT_03";

                    //message.Text = "Tagname = " + TagName + ":" + RDID.ToString();

                    if (TagName == "FPT_01")
                    {
                        PrintTag_01(RDID, e.Item);
                    }
                    else if (TagName == "KIT_FWDLOG")
                    {
                        PrintKIT_FWDLOG(RDID, e.Item);
                    }
                    else if (TagName == "KIT_WIND")
                    {
                        PrintKIT_WIND(RDID, e.Item);
                    }
                    else if (TagName == "FPT_GMP")
                    {
                        PrintTag_GMP(RDID, e.Item);
                    }
                    else if (TagName == "FPT_02")
                    {
                        //PrintTag_04(RDID);
                        PrintTag_02(RDID, e.Item);
                    }
                    else if (TagName == "FPT_03")
                    {
                        PrintTag_03(RDID, e.Item);
                    }
                    else if (TagName == "FPT_04")
                    {
                        PrintTag_04(RDID, e.Item);
                    }
                    else if (TagName == "FPT_04A")
                    {
                        PrintTag_04a(RDID, e.Item);
                    }
                    else if (TagName == "FPT_05")
                    {
                        PrintTag_05(RDID, e.Item);
                    }
                    else if (TagName == "FPT_06")
                    {
                        PrintTag_06(RDID, e.Item);
                    }
                    else if (TagName == "FPT_07")
                    {
                        PrintTag_07(RDID, e.Item);
                    }

                    else if (TagName == "FPT_BB")
                    {
                        PrintTag_BB(RDID, e.Item);
                    }
                    else if (TagName == "FPT_WIND")
                    {
                        PrintTag_WIND(RDID, e.Item);
                    }
                    else if (TagName == "KIT_IMM")
                    {
                        PrintTag_Staple(RDID, e.Item);
                    }
                    else if (TagName.Length == 0)
                    {
                        PrintTag_04(RDID, e.Item);
                    }
                    else
                    {
                        TestLoadOther(TagName, e.Item);
                    }
                }
            }
        }


        //private void LabelSetup(string ReceiveDetailID)
        //{

        //    decimal RDID = -1;
        //    decimal.TryParse(ReceiveDetailID, out RDID);
        //    FTP_WIND.Visible = false;
        //    FPL_APPLE.Visible = false;
        //    FPL_01.Visible = false;
        //    FPL_02.Visible = false;
        //    FPL_03.Visible = false;
        //    FPL_04.Visible = false;
        //    FPL_05.Visible = false;
        //    FPL_06.Visible = false;
        //    FPL_07.Visible = false;
        //    FPL_BB.Visible = false;



        //    if (RDID < 1)
        //    {
        //        //RDID = 47977;
        //        //message.Text = "Returned 1: " + RDID.ToString();
        //        return;
        //    }

        //    rdm = new ReceiveDetailManager(User.Identity.Name);
        //    rd = rdm.ReceiveDetail(RDID);
        //    if (rd != null)
        //    {
        //        string Manufacturer = "";
        //        // We have a special lable just for apple computers.
        //        if (rd.Manufacturer == null || rd.Manufacturer.Length == 0) { Manufacturer = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Manufacturer").ToUpper(); }
        //        else { Manufacturer = rd.Manufacturer.ToUpper(); }



        //        ClientLocationManager clm = new ClientLocationManager(User.Identity.Name);
        //        Client cl = clm.GetClientThisLocation(ctx, rd.ClientLocationID);
        //        ProjectManager pm = new ProjectManager(User.Identity.Name);
        //        Project proj = pm.Get((decimal)rd.ProjectID);
        //        string TagName = "";
        //        if (proj != null && proj.ProductTag != null && proj.ProductTag.Length > 0) { TagName = proj.ProductTag.ToUpper(); }
        //        else if (cl != null && cl.ProductTag != null && cl.ProductTag.Length > 0) { TagName = cl.ProductTag.ToUpper(); }

        //        // Jody asked to exclude client 91 from the Apple restriction.
        //        if (Manufacturer == "APPLE" && TagName != "FPT_BB" && cl.ClientID != 91) { PrintTag_APPLE(RDID); return; }

        //        //TagName = "FPT_03";

        //        //message.Text = "Tagname = " + TagName + ":" + RDID.ToString();

        //        if (TagName == "FPT_01")
        //        {
        //            PrintTag_01(RDID);
        //        }
        //        else if (TagName == "FPT_GMP")
        //        {
        //            PrintTag_GMP(RDID);
        //        }
        //        else if (TagName == "FPT_02")
        //        {
        //            //PrintTag_04(RDID);
        //            PrintTag_02(RDID);
        //        }
        //        else if (TagName == "FPT_03")
        //        {
        //            PrintTag_03(RDID);
        //        }
        //        else if (TagName == "FPT_04")
        //        {
        //            PrintTag_04(RDID);
        //        }
        //        else if (TagName == "FPT_04A")
        //        {
        //            PrintTag_04a(RDID);
        //        }
        //        else if (TagName == "FPT_05")
        //        {
        //            PrintTag_05(RDID);
        //        }
        //        else if (TagName == "FPT_06")
        //        {
        //            PrintTag_06(RDID);
        //        }
        //        else if (TagName == "FPT_07")
        //        {
        //            PrintTag_07(RDID);
        //        }

        //        else if (TagName == "FPT_BB")
        //        {
        //            PrintTag_BB(RDID);
        //        }
        //        else if (TagName == "FPT_WIND")
        //        {
        //            PrintTag_WIND(RDID);
        //        }

        //        else if (TagName.Length == 0)
        //        {
        //            PrintTag_04(RDID);
        //        }
        //        else
        //        {
        //            //TestLoadOther(TagName);
        //        }


        //    }
        //    //else
        //    //{
        //    //    PrintTag_03(RDID);
        //    //}
        //}



        private void PrintTag_Staple(decimal RDID, RepeaterItem Item)
        {
            Panel FPL_Staple = (Panel)Item.FindControl("FPL_Staple");
            LinearBarcode bcUPC_Staple = (LinearBarcode)Item.FindControl("bcUPC_Staple");
            LinearBarcode IMEI_Staple = (LinearBarcode)Item.FindControl("IMEI_Staple");
            //LinearBarcode bcSku_02 = (LinearBarcode)Item.FindControl("bcSku_02");

            Label lblSKU_Staple = (Label)Item.FindControl("lblSKU_Staple");
            Label Label27 = (Label)Item.FindControl("Label27");
            //Label lblCarrierLockCode_Staple = (Label)Item.FindControl("lblCarrierLockCode_Staple");

            Label lblManufacturer_Staple = (Label)Item.FindControl("lblManufacturer_Staple");
            Label lblModel_Staple = (Label)Item.FindControl("lblModel_Staple");

            Label lblColour_Staple = (Label)Item.FindControl("lblColour_Staple");
            Label lblGrade_Staple = (Label)Item.FindControl("lblGrade_Staple");
            //Label lbl_WarrantyExpire_Staple = (Label)Item.FindControl("lbl_WarrantyExpire_Staple");


            FPL_Staple.Visible = true;
            bcUPC_Staple.Visible = false;
            lblSKU_Staple.Visible = false;
            Label27.Visible = false;
            if (RDID > 0)
            {
                rd = rdm.ReceiveDetail(ctx, RDID);
                IMEI_Staple.DataToEncode = rd.ESN;
                IMEI_Staple.Visible = true;
                string Carrier = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Carrier");
                string MaybeLockedCarrier = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "LOCKCARRIER");
                string Manufacturer = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Manufacturer");
                string Model = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Model");
                string Colour = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Colour");
                string Grade = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Grade");
                string ReplacementESN = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Replacement IMEI");
                string SN = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Serial Number");
                string WPerformed = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Work Performed");
                string clc = " ";
                string[] WorkPerformed = WPerformed.Split('/');
                if (MaybeLockedCarrier.Length > 0) { clc = MaybeLockedCarrier.Substring(0, 1); }
                if (WorkPerformed.Contains("Un-Locked") == true) { clc += "UL"; }
                //lblCarrierLockCode_Staple.Text = clc;

                string WarrantyExpire = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Warranty Expiry Date");
                DateTime WE = new DateTime(2000, 01, 01);
                if (DateTime.TryParse(WarrantyExpire, out WE) == false) { WE = new DateTime(2000, 01, 01); }

                lblManufacturer_Staple.Text = Manufacturer;
                lblModel_Staple.Text = Model;
                lblColour_Staple.Text = Colour;
                mcl = mcm.GetMasterCarrierManufacturerLookup(ctx, RDID, Carrier, Manufacturer, Model, Colour);
                string UPCCode = "";
                if (mcl != null)
                {
                    lblSKU_Staple.Text = ""; lblGrade_Staple.Text = "";
                    // The SKU, SKU_B and SKU_C were miss labeled.( A=b and B = c)
                    if (Grade.ToUpper() == "A") { lblSKU_Staple.Visible = true; Label27.Visible = true; lblSKU_Staple.Text = mcl.SKU; lblGrade_Staple.Text = ""; UPCCode = mcl.UPC; }
                    if (Grade.ToUpper() == "B") { lblSKU_Staple.Visible = true; Label27.Visible = true; lblSKU_Staple.Text = mcl.SKU_B; lblGrade_Staple.Text = ""; UPCCode = mcl.UPC_2; }
                    if (Grade.ToUpper() == "C") { lblSKU_Staple.Visible = true; Label27.Visible = true; lblSKU_Staple.Text = mcl.SKU_B; lblGrade_Staple.Text = ""; UPCCode = mcl.UPC_3; }
                    if (mcl.NickName != null) { lblModel_Staple.Text = mcl.NickName; }
                }
                if (ReplacementESN.Length > 0) { IMEI_Staple.Visible = true; IMEI_Staple.DataToEncode = ReplacementESN; }
                if (UPCCode.Length > 0)
                {
                    bcUPC_Staple.Visible = true;
                    bcUPC_Staple.DataToEncode = UPCCode;
                }
                //if (WarrantyExpire.Length > 0 && WE >= DateTime.Now) { lbl_WarrantyExpire_Staple.Text = "Warranty Expiry:" + "(" + WarrantyExpire + ")"; }
            }
        }

        private void PrintKIT_FWDLOG(decimal RDID, RepeaterItem Item)             // iphone
        {
            Panel KIT_FWDLOG = (Panel)Item.FindControl("KIT_FWDLOG");
            LinearBarcode IMEI_05N = (LinearBarcode)Item.FindControl("IMEI_05N");
            Label lblManufacturer_05N = (Label)Item.FindControl("lblManufacturer_05N");
            Label lblColour_05N = (Label)Item.FindControl("lblColour_05N");
            Label lblGrade_05N = (Label)Item.FindControl("lblGrade_05N");
            Label lblCarrierLockCode_05N = (Label)Item.FindControl("lblCarrierLockCode_05N");
            Label Label4 = (Label)Item.FindControl("Label4");
            KIT_FWDLOG.Visible = true;
            //string ESN = Request.QueryString.Get("ESN");
            //if (ESN == null || ESN.Length == 0) { ESN = Request.QueryString.Get("LESN"); }
            //if (ESN == null || ESN.Length == 0) { ESN = "123456789065432"; }
            if (RDID > 0)
            {
                ReceiveDetailManager rdm = new ReceiveDetailManager(User.Identity.Name);
                ReceiveDetail rd = rdm.ReceiveDetail(ctx, RDID);
                if (rd != null)
                {
                    IMEI_05N.DataToEncode = rd.ESN;
                    IMEI_05N.Visible = true;

                    string Carrier = rdm.GetReceiveDetailItem_DataElementName(ctx, RDID, "Carrier");
                    string MaybeLockedCarrier = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "LOCKCARRIER");
                    string Manufacturer = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Manufacturer");
                    string Model = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Model");
                    string Colour = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Colour");
                    string ColourABBR = rdm.GetReceiveDetailItem_DataElementName(ctx, RDID, "Colour");
                    string Grade = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Grade");
                    lblManufacturer_05N.Text = Manufacturer + " " + Model;
                    lblColour_05N.Text = "Colour:" + Colour;
                    lblGrade_05N.Text = "";
                    lblGrade_05N.Text = rdm.TranslateGrade(Grade);
                    lblCarrierLockCode_05N.Text = MaybeLockedCarrier + "-" + ColourABBR + "-" + rdm.TranslateGrade(Grade);
                }
            }
        }
        private void PrintKIT_WIND(decimal RDID, RepeaterItem Item)
        {
            Panel KIT_WIND = (Panel)Item.FindControl("KIT_WIND");
            LinearBarcode IMEI_WIndN = (LinearBarcode)Item.FindControl("IMEI_WIndN");
            LinearBarcode SKUA_WIndN = (LinearBarcode)Item.FindControl("SKUA_WIndN");
            Label lblDescription_WindN = (Label)Item.FindControl("lblDescription_WindN");

            //Label lblUnlockCode = (Label)Item.FindControl("lblUnlockCode");
            //Label lblManufacturer_05 = (Label)Item.FindControl("lblManufacturer_05");
            //Label lblModel_05 = (Label)Item.FindControl("lblModel_05");
            //Label lblColour_05 = (Label)Item.FindControl("lblColour_05");
            //Label Label4 = (Label)Item.FindControl("Label4");
            //Label lblGrade_05 = (Label)Item.FindControl("lblGrade_05");
            //Label lblKittingDateCoded_05 = (Label)Item.FindControl("lblKittingDateCoded_05");

            KIT_WIND.Visible = true;
            //string ESN = Request.QueryString.Get("ESN");
            //if (ESN == null || ESN.Length == 0) { ESN = Request.QueryString.Get("LESN"); }
            //if (ESN == null || ESN.Length == 0) { ESN = "123456789065432"; }
            if (RDID > 0)
            {
                ReceiveDetailManager rdm = new ReceiveDetailManager(User.Identity.Name);
                ReceiveDetail rd = rdm.ReceiveDetail(ctx, RDID);
                if (rd != null)
                {
                    IMEI_WIndN.DataToEncode = rd.ESN;
                    //lblIMEI_Wind.Text = ESN;
                    string Carrier = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Carrier");
                    string Manufacturer = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Manufacturer");
                    string Model = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Model");
                    string Colour = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Colour");
                    //string Grade = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Grade");
                    //string ReplacementESN = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Replacement IMEI");
                    MasterCarrierManufacturerModelColourManager mcm = new MasterCarrierManufacturerModelColourManager(User.Identity.Name);
                    MasterCarrierManufacturerLookup mcl = mcm.GetMasterCarrierManufacturerLookup(ctx, rd.ReceiveDetailID, Carrier, Manufacturer, Model, Colour);
                    if (mcl != null)
                    {
                        //// this may need to be adjusted to deal with Sku b, c etc.
                        //if (Grade.ToUpper() == "A") { lblSKU_01.Text = mcl.SKU; }
                        //if (Grade.ToUpper() == "B") { lblSKU_01.Text = mcl.SKU_B; }
                        //if (Grade.ToUpper() == "C") { lblSKU_01.Text = mcl.SKU_C; }

                        //lblVendorPart_01.Text = "BMBS" + lblSKU_01.Text;
                        lblDescription_WindN.Text = mcl.Description;

                        //lblSKUA_Wind.Text = mcl.SKU;
                        SKUA_WIndN.DataToEncode = mcl.SKU;
                    }
                    //lblReplaceESN_01.Text = ReplacementESN;
                }
            }
        }


        private void PrintTag_WIND(decimal RDID, RepeaterItem Item)
        {
            Panel FTP_WIND = (Panel)Item.FindControl("FTP_WIND");
            Label lblDescription_Wind = (Label)Item.FindControl("lblDescription_Wind");
            LinearBarcode IMEI_WInd = (LinearBarcode)Item.FindControl("IMEI_WInd");
            LinearBarcode SKUA_WInd = (LinearBarcode)Item.FindControl("SKUA_WInd");

            FTP_WIND.Visible = true;
            if (RDID > 0)
            {
                rd = rdm.ReceiveDetail(ctx, RDID);
                if (rd != null)
                {
                    IMEI_WInd.DataToEncode = rd.ESN;
                    string Carrier = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Carrier");
                    string Manufacturer = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Manufacturer");
                    string Model = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Model");
                    string Colour = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Colour");
                    mcl = mcm.GetMasterCarrierManufacturerLookup(ctx, rd.ReceiveDetailID, Carrier, Manufacturer, Model, Colour);
                    if (mcl != null)
                    {
                        lblDescription_Wind.Text = mcl.Description;
                        SKUA_WInd.DataToEncode = mcl.SKU;
                    }
                }
            }
        }
        private void PrintTag_APPLE(decimal RDID, RepeaterItem Item)
        {
            Panel FPL_APPLE = (Panel)Item.FindControl("FPL_APPLE");
            LinearBarcode IMEI_APPLE = (LinearBarcode)Item.FindControl("IMEI_APPLE");

            Label lblManMod_APPLE = (Label)Item.FindControl("lblManMod_APPLE");
            Label lblCarrier_Apple = (Label)Item.FindControl("lblCarrier_Apple");
            Label lblQuickCode_Apple = (Label)Item.FindControl("lblQuickCode_Apple");
            Label lblCarrierLockCode_Apple = (Label)Item.FindControl("lblCarrierLockCode_Apple");
            Label lblKittingDateCoded_Apple = (Label)Item.FindControl("lblKittingDateCoded_Apple");

            FPL_APPLE.Visible = true;
            if (RDID > 0)
            {
                rd = rdm.ReceiveDetail(ctx, RDID);
                if (rd != null)
                {
                    IMEI_APPLE.DataToEncode = rd.ESN;
                    string Carrier = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Carrier");
                    string MaybeLockedCarrier = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "LOCKCARRIER");
                    if (rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "PTG Product").ToUpper() == "YES")
                    {
                        MaybeLockedCarrier = MaybeLockedCarrier + "(PTG)";
                    }
                    string Manufacturer = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Manufacturer");
                    string Model = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Model");
                    string Colour = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Colour");
                    string Grade = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Grade");
                    lblManMod_APPLE.Text = Manufacturer + "  " + Model;
                    lblCarrier_Apple.Text = MaybeLockedCarrier;
                    lblQuickCode_Apple.Text = Colour + "     " + Grade;

                    string WPerformed = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Work Performed");
                    string clc = "";                                     // Carrier.Substring(0, 1);
                    string[] WorkPerformed = WPerformed.Split('/');
                    if (WorkPerformed.Contains("Un-Locked") == true)
                    {
                        clc += "UL";
                    }
                    lblCarrierLockCode_Apple.Text = clc;
                    lblKittingDateCoded_Apple.Text = rdm.GetKittingDateCoded(RDID);
                }
            }
        }
        private void PrintTag_GMP(decimal RDID, RepeaterItem Item)
        {
            Panel FPL_05 = (Panel)Item.FindControl("FPL_05");
            LinearBarcode IMEI_05 = (LinearBarcode)Item.FindControl("IMEI_05");
            Label lblUnlockCode = (Label)Item.FindControl("lblUnlockCode");
            Label lblCarrierLockCode_05 = (Label)Item.FindControl("lblCarrierLockCode_05");
            Label lblManufacturer_05 = (Label)Item.FindControl("lblManufacturer_05");
            Label lblModel_05 = (Label)Item.FindControl("lblModel_05");
            Label lblColour_05 = (Label)Item.FindControl("lblColour_05");
            Label Label4 = (Label)Item.FindControl("Label4");
            Label lblGrade_05 = (Label)Item.FindControl("lblGrade_05");
            Label lblKittingDateCoded_05 = (Label)Item.FindControl("lblKittingDateCoded_05");


            FPL_05.Visible = true;
            lblUnlockCode.Text = "";
            lblUnlockCode.Visible = false;
            if (RDID > 0)
            {
                rd = rdm.ReceiveDetail(ctx, RDID);
                IMEI_05.DataToEncode = rd.ESN;
                IMEI_05.Visible = true;
                string Carrier = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Carrier");
                string MaybeLockedCarrier = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "LOCKCARRIER");
                if (rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "PTG Product").ToUpper() == "YES") { MaybeLockedCarrier = MaybeLockedCarrier + "(PTG)"; }
                string Manufacturer = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Manufacturer");
                string Model = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Model");
                string Colour = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Colour");
                string Grade = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Grade");
                string ReplacementESN = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Replacement IMEI");
                string SN = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Serial Number");
                string UnlockCode = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Unlock Code");
                string WPerformed = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Work Performed");
                string clc = "";
                string clcx = "";
                if (UnlockCode.Length > 0)
                {
                    lblUnlockCode.Text = "Unlock: " + UnlockCode;
                    lblUnlockCode.Visible = true;
                }
                if (MaybeLockedCarrier.Length > 0) { clcx = MaybeLockedCarrier; }
                clcx = clcx.Replace("-", "").Replace(" ", "").Replace("_", "");
                if (clcx.Length > 0) { clc = clcx.Substring(0, 1); }
                if (clcx.Length > 2) { clc = clcx.Substring(0, 3); }
                clc = clc.ToUpper();
                string[] WorkPerformed = WPerformed.Split('/');
                if (WorkPerformed.Contains("Un-Locked") == true)
                {
                    clc += "UL";
                }
                clc = clc + "-" + Colour + "-" + rdm.TranslateGrade(Grade);
                lblCarrierLockCode_05.Text = clc;
                string WarrantyExpire = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Warranty Expiry Date");
                DateTime WE = new DateTime(2000, 01, 01);
                if (DateTime.TryParse(WarrantyExpire, out WE) == false) { WE = new DateTime(2000, 01, 01); }
                lblManufacturer_05.Text = Manufacturer;
                lblModel_05.Text = Model;
                lblColour_05.Text = Colour;
                Label4.Visible = false;
                lblGrade_05.Text = "";
                Label4.Visible = true;
                lblGrade_05.Text = rdm.TranslateGrade(Grade);
                if (ReplacementESN.Length > 0)
                {
                    IMEI_05.Visible = true;
                    IMEI_05.DataToEncode = ReplacementESN;
                }
                lblKittingDateCoded_05.Text = rdm.GetKittingDateCoded(RDID);
            }
        }
        private void PrintTag_01(decimal RDID, RepeaterItem Item)
        {
            Panel FPL_01 = (Panel)Item.FindControl("FPL_01");
            LinearBarcode IMEI_01 = (LinearBarcode)Item.FindControl("IMEI_01");
            Label lblSKU_01 = (Label)Item.FindControl("lblSKU_01");
            Label lblVendorPart_01 = (Label)Item.FindControl("lblVendorPart_01");
            Label lblDescription_01 = (Label)Item.FindControl("lblDescription_01");
            FPL_01.Visible = true;
            if (RDID > 0)
            {
                rd = rdm.ReceiveDetail(ctx, RDID);
                if (rd != null)
                {
                    IMEI_01.DataToEncode = rd.ESN;
                    string Carrier = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Carrier");
                    string Manufacturer = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Manufacturer");
                    string Model = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Model");
                    string Colour = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Colour");
                    string Grade = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Grade");
                    string ReplacementESN = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Replacement IMEI");
                    mcl = mcm.GetMasterCarrierManufacturerLookup(ctx, rd.ReceiveDetailID, Carrier, Manufacturer, Model, Colour);
                    if (mcl != null)
                    {
                        // this may need to be adjusted to deal with Sku b, c etc.
                        if (Grade.ToUpper() == "A") { lblSKU_01.Text = mcl.SKU; }
                        if (Grade.ToUpper() == "B") { lblSKU_01.Text = mcl.SKU_B; }
                        if (Grade.ToUpper() == "C") { lblSKU_01.Text = mcl.SKU_C; }

                        lblVendorPart_01.Text = "BMBS" + lblSKU_01.Text;
                        lblDescription_01.Text = mcl.Description;
                    }
                    if (ReplacementESN.Length > 0) { IMEI_01.DataToEncode = ReplacementESN; }
                }
            }
        }
        private void PrintTag_02(decimal RDID, RepeaterItem Item)
        {
            Panel FPL_02 = (Panel)Item.FindControl("FPL_02");
            LinearBarcode IMEI_02 = (LinearBarcode)Item.FindControl("IMEI_02");
            LinearBarcode bcUPC_02 = (LinearBarcode)Item.FindControl("bcUPC_02");
            LinearBarcode bcSku_02 = (LinearBarcode)Item.FindControl("bcSku_02");

            Label lblGrade_02 = (Label)Item.FindControl("lblGrade_02");
            Label lblGoodAsNew = (Label)Item.FindControl("lblGoodAsNew");
            Label lblSKU_02 = (Label)Item.FindControl("lblSKU_02");

            Label lblCondition_02 = (Label)Item.FindControl("lblCondition_02");
            Label lblDescription_02 = (Label)Item.FindControl("lblDescription_02");

            FPL_02.Visible = true;
            bcUPC_02.Visible = false;
            bcSku_02.Visible = false;
            if (RDID > 0)
            {
                rd = rdm.ReceiveDetail(ctx, RDID);
                if (rd != null)
                {
                    IMEI_02.DataToEncode = rd.ESN;
                    string Carrier = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Carrier");
                    string Manufacturer = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Manufacturer");
                    string Model = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Model");
                    string Colour = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Colour");
                    string Grade = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Grade");
                    string ReplacementESN = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Replacement IMEI");
                    lblGrade_02.Text = Grade.ToUpper();
                    lblGoodAsNew.Visible = false;
                    lblGoodAsNew.Text = "";
                    if (Grade.ToUpper() == "A") { lblGoodAsNew.Visible = true; lblGoodAsNew.Text = "Good as New "; }
                    if (Grade.ToUpper() == "C") { lblGoodAsNew.Visible = true; lblGoodAsNew.Text = "LOANER "; lblGrade_02.Visible = false; }
                    mcl = mcm.GetMasterCarrierManufacturerLookup(ctx, rd.ReceiveDetailID, Carrier, Manufacturer, Model, Colour);
                    if (mcl != null)
                    {
                        string UPCCode = "";
                        // this may need to be adjusted to deal with Sku b, c etc.
                        if (Grade.ToUpper() == "A") { lblSKU_02.Text = mcl.SKU; UPCCode = mcl.UPC; }
                        if (Grade.ToUpper() == "B") { lblSKU_02.Text = mcl.SKU_B; UPCCode = mcl.UPC_2; }
                        if (Grade.ToUpper() == "C") { lblSKU_02.Text = mcl.SKU_C; UPCCode = mcl.UPC_3; }
                        lblCondition_02.Text = Manufacturer + "                " + Model + " " + mcl.NickName + " " + Colour;
                        lblDescription_02.Text = mcl.Description;
                        if (lblSKU_02.Text.Length > 0)
                        {
                            bcSku_02.Visible = true;
                            bcSku_02.DataToEncode = lblSKU_02.Text;
                        }
                        if (UPCCode.Length > 0)
                        {
                            bcUPC_02.Visible = true;
                            bcUPC_02.DataToEncode = UPCCode;
                        }
                    }
                    if (ReplacementESN.Length > 0) { IMEI_02.DataToEncode = ReplacementESN; }
                }
            }
        }
        private void PrintTag_03(decimal RDID, RepeaterItem Item)
        {
            Panel FPL_03 = (Panel)Item.FindControl("FPL_03");
            LinearBarcode IMEI_03 = (LinearBarcode)Item.FindControl("IMEI_03");
            LinearBarcode HEX_03 = (LinearBarcode)Item.FindControl("HEX_03");
            LinearBarcode UPC_03 = (LinearBarcode)Item.FindControl("UPC_03");

            Label lblHex_03 = (Label)Item.FindControl("lblHex_03");
            Label lblCondition_03 = (Label)Item.FindControl("lblCondition_03");
            Label lblUPC_03 = (Label)Item.FindControl("UPC_03");

            Label lblSKU_03 = (Label)Item.FindControl("lblSKU_03");
            Label lblGrade_03 = (Label)Item.FindControl("lblGrade_03");

            Label lblManufacturer_03 = (Label)Item.FindControl("lblManufacturer_03");
            //Label lblGrade_04 = (Label)Item.FindControl("lblGrade_04");
            //Label lbl_WarrantyExpire_04 = (Label)Item.FindControl("lbl_WarrantyExpire_04");
            FPL_03.Visible = true;
            if (RDID > 0)
            {
                rd = rdm.ReceiveDetail(ctx, RDID);
                IMEI_03.DataToEncode = rd.ESN;
                string Carrier = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Carrier");
                string Manufacturer = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Manufacturer");
                string Model = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Model");
                string Colour = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Colour");
                string Grade = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Grade");
                string Hex = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "HEX ESN");
                string ReplacementESN = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Replacement IMEI");
                HEX_03.Visible = false;
                lblHex_03.Visible = false;
                if (Hex.Length > 0)
                {
                    HEX_03.Visible = true;
                    lblHex_03.Visible = true;
                    HEX_03.DataToEncode = Hex;
                }

                lblCondition_03.Text = "";
                UPC_03.Visible = false;
                mcl = mcm.GetMasterCarrierManufacturerLookup(ctx, RDID, Carrier, Manufacturer, Model, Colour);
                if (mcl != null)
                {
                    string UPCCode = "";
                    lblSKU_03.Text = "";
                    // The SKU, SKU_B and SKU_C were miss labeled.( A=b and B = c)
                    lblSKU_03.Text = Grade.ToUpper();
                    if (Grade.ToUpper() == "A" || Grade.ToUpper() == "B") { lblSKU_03.Text = Grade.ToUpper(); lblGrade_03.Text = Grade.ToUpper(); }
                    if (Grade.ToUpper() == "A" && mcl.SKU.Length > 0) { lblSKU_03.Text = mcl.SKU; }
                    if (Grade.ToUpper() == "B" && mcl.SKU_B.Length > 0) { lblSKU_03.Text = mcl.SKU_B; }
                    if (Grade.ToUpper() == "C" && mcl.SKU_C.Length > 0) { lblSKU_03.Text = mcl.SKU_C; }

                    if (Grade.ToUpper() == "A") { UPCCode = mcl.UPC; }
                    if (Grade.ToUpper() == "B") { UPCCode = mcl.UPC_2; }
                    if (Grade.ToUpper() == "C") { UPCCode = mcl.UPC_3; }
                    lblCondition_03.Text = "";        // mcl.Condition + " ";
                    lblManufacturer_03.Text = Manufacturer + " ";
                    if (mcl.NickName.Length > 0) { lblManufacturer_03.Text += mcl.NickName + " "; }
                    else { lblManufacturer_03.Text += Model + " "; }
                    lblManufacturer_03.Text += Colour;

                    if (UPCCode.Length > 0)
                    {
                        lblUPC_03.Visible = true;
                        UPC_03.DataToEncode = UPCCode;
                        UPC_03.Visible = true;
                    }
                }
                if (ReplacementESN.Length > 0) { IMEI_03.DataToEncode = ReplacementESN; }
            }
        }
        private void PrintTag_04a(decimal RDID, RepeaterItem Item)             // iphone
        {
            TableRow rowCarrierLockCode_04 = (TableRow)Item.FindControl("rowCarrierLockCode_04");
            PrintTag_04(RDID, Item);
            rowCarrierLockCode_04.Visible = false;
        }
        private void PrintTag_04(decimal RDID, RepeaterItem Item)
        {
            Panel FPL_04 = (Panel)Item.FindControl("FPL_04");
            LinearBarcode BC_SerialNumber_04 = (LinearBarcode)Item.FindControl("BC_SerialNumber_04");
            LinearBarcode IMEI_04 = (LinearBarcode)Item.FindControl("IMEI_04");
            //LinearBarcode bcSku_02 = (LinearBarcode)Item.FindControl("bcSku_02");

            Label lblSKU_04 = (Label)Item.FindControl("lblSKU_04");
            Label Label4 = (Label)Item.FindControl("Label4");
            Label lblCarrierLockCode_04 = (Label)Item.FindControl("lblCarrierLockCode_04");

            Label lblManufacturer_04 = (Label)Item.FindControl("lblManufacturer_04");
            Label lblModel_04 = (Label)Item.FindControl("lblModel_04");

            Label lblColour_04 = (Label)Item.FindControl("lblColour_04");
            Label lblGrade_04 = (Label)Item.FindControl("lblGrade_04");
            Label lbl_WarrantyExpire_04 = (Label)Item.FindControl("lbl_WarrantyExpire_04");


            FPL_04.Visible = true;
            BC_SerialNumber_04.Visible = false;
            lblSKU_04.Visible = false;
            Label4.Visible = false;
            if (RDID > 0)
            {
                rd = rdm.ReceiveDetail(ctx, RDID);
                IMEI_04.DataToEncode = rd.ESN; 
                IMEI_04.Visible = true;
                string Carrier = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Carrier");
                string MaybeLockedCarrier = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "LOCKCARRIER");
                string Manufacturer = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Manufacturer");
                string Model = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Model");
                string Colour = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Colour");
                string Grade = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Grade");
                string ReplacementESN = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Replacement IMEI");
                string SN = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Serial Number");
                string WPerformed = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Work Performed");
                string clc = " ";
                string[] WorkPerformed = WPerformed.Split('/');
                if (MaybeLockedCarrier.Length > 0) { clc = MaybeLockedCarrier.Substring(0, 1); }
                if (WorkPerformed.Contains("Un-Locked") == true) { clc += "UL"; }
                lblCarrierLockCode_04.Text = clc;

                string WarrantyExpire = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Warranty Expiry Date");
                DateTime WE = new DateTime(2000, 01, 01);
                if (DateTime.TryParse(WarrantyExpire, out WE) == false) { WE = new DateTime(2000, 01, 01); }

                lblManufacturer_04.Text = Manufacturer;
                lblModel_04.Text = Model;
                lblColour_04.Text = Colour;
                mcl = mcm.GetMasterCarrierManufacturerLookup(ctx, RDID, Carrier, Manufacturer, Model, Colour);
                if (mcl != null)
                {
                    lblSKU_04.Text = ""; lblGrade_04.Text = "";
                    // The SKU, SKU_B and SKU_C were miss labeled.( A=b and B = c)
                    if (Grade.ToUpper() == "A") { lblSKU_04.Visible = true; Label4.Visible = true; lblSKU_04.Text = mcl.SKU; lblGrade_04.Text = "A"; }
                    if (Grade.ToUpper() == "B") { lblSKU_04.Visible = true; Label4.Visible = true; lblSKU_04.Text = mcl.SKU_B; lblGrade_04.Text = "B"; }
                    if (Grade.ToUpper() == "C") { lblSKU_04.Visible = true; Label4.Visible = true; lblSKU_04.Text = mcl.SKU_B; lblGrade_04.Text = "C"; }
                }
                if (ReplacementESN.Length > 0) { IMEI_04.Visible = true; IMEI_04.DataToEncode = ReplacementESN; }
                if (SN.Length > 0) { BC_SerialNumber_04.Visible = true; BC_SerialNumber_04.DataToEncode = SN; }
                if (WarrantyExpire.Length > 0 && WE >= DateTime.Now) { lbl_WarrantyExpire_04.Text = "Warranty Expiry:" + "(" + WarrantyExpire + ")"; }
            }
        }
        private void PrintTag_05(decimal RDID, RepeaterItem Item)
        {
            Panel FPL_05 = (Panel)Item.FindControl("FPL_05");
            LinearBarcode IMEI_05 = (LinearBarcode)Item.FindControl("IMEI_05");
            //LinearBarcode IMEI_04 = (LinearBarcode)Item.FindControl("IMEI_04");
            //LinearBarcode bcSku_02 = (LinearBarcode)Item.FindControl("bcSku_02");

            Label lblCarrierLockCode_05 = (Label)Item.FindControl("lblCarrierLockCode_05");
            Label lblManufacturer_05 = (Label)Item.FindControl("lblManufacturer_05");
            Label lblModel_05 = (Label)Item.FindControl("lblModel_05");

            Label lblColour_05 = (Label)Item.FindControl("lblColour_05");
            Label Label4 = (Label)Item.FindControl("Label4");

            Label lblGrade_05 = (Label)Item.FindControl("lblGrade_05");
            //Label lblGrade_04 = (Label)Item.FindControl("lblGrade_04");
            //Label lbl_WarrantyExpire_04 = (Label)Item.FindControl("lbl_WarrantyExpire_04");

            FPL_05.Visible = true;
            if (RDID > 0)
            {
                rd = rdm.ReceiveDetail(ctx, RDID);
                IMEI_05.DataToEncode = rd.ESN;
                IMEI_05.Visible = true;
                string Carrier = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Carrier");
                string MaybeLockedCarrier = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "LOCKCARRIER");
                string Manufacturer = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Manufacturer");
                string Model = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Model");
                string Colour = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Colour");
                string Grade = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Grade");
                string ReplacementESN = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Replacement IMEI");
                string SN = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Serial Number");
                string WPerformed = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Work Performed");
                string clc = " ";
                if (MaybeLockedCarrier.Length > 0) { clc = MaybeLockedCarrier.Substring(0, 1); }
                string[] WorkPerformed = WPerformed.Split('/');
                if (WorkPerformed.Contains("Un-Locked") == true) { clc += "UL"; }
                lblCarrierLockCode_05.Text = clc;
                string WarrantyExpire = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Warranty Expiry Date");
                DateTime WE = new DateTime(2000, 01, 01);
                if (DateTime.TryParse(WarrantyExpire, out WE) == false) { WE = new DateTime(2000, 01, 01); }
                lblManufacturer_05.Text = Manufacturer;
                lblModel_05.Text = Model;
                lblColour_05.Text = Colour;
                Label4.Visible = false;
                lblGrade_05.Text = "";
                Label4.Visible = true;
                lblGrade_05.Text = rdm.TranslateGrade(Grade);
                if (ReplacementESN.Length > 0)
                {
                    IMEI_05.Visible = true;
                    IMEI_05.DataToEncode = ReplacementESN;
                }
            }
        }
        private void PrintTag_06(decimal RDID, RepeaterItem Item)
        {
            Panel FPL_06 = (Panel)Item.FindControl("FPL_06");
            LinearBarcode bcUPC_06 = (LinearBarcode)Item.FindControl("bcUPC_06");
            LinearBarcode bcSku_06 = (LinearBarcode)Item.FindControl("bcSku_06");
            LinearBarcode IMEI_06 = (LinearBarcode)Item.FindControl("IMEI_06");

            Label lblGrade_06 = (Label)Item.FindControl("lblGrade_06");
            Label lblSKU_06 = (Label)Item.FindControl("lblSKU_06");
            Label lblCondition_06 = (Label)Item.FindControl("lblCondition_06");

            Label lblDescription_06 = (Label)Item.FindControl("lblDescription_06");
            //Label Label4 = (Label)Item.FindControl("Label4");

            //Label lblGrade_05 = (Label)Item.FindControl("lblGrade_05");
            //Label lblGrade_04 = (Label)Item.FindControl("lblGrade_04");
            //Label lbl_WarrantyExpire_04 = (Label)Item.FindControl("lbl_WarrantyExpire_04");

            FPL_06.Visible = true;
            bcUPC_06.Visible = false;
            bcSku_06.Visible = false;
            if (RDID > 0)
            {
                rd = rdm.ReceiveDetail(ctx, RDID);
                if (rd != null)
                {
                    IMEI_06.DataToEncode = rd.ESN;
                    string Carrier = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Carrier");
                    string Manufacturer = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Manufacturer");
                    string Model = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Model");
                    string Colour = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Colour");
                    string Grade = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Grade");
                    string ReplacementESN = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Replacement IMEI");
                    lblGrade_06.Text = Grade.ToUpper();
                    mcl = mcm.GetMasterCarrierManufacturerLookup(ctx, rd.ReceiveDetailID, Carrier, Manufacturer, Model, Colour);
                    if (mcl != null)
                    {
                        string UPCCode = "";
                        // this may need to be adjusted to deal with Sku b, c etc.
                        if (Grade.ToUpper() == "A") { lblSKU_06.Text = mcl.SKU; UPCCode = mcl.UPC; }
                        if (Grade.ToUpper() == "B") { lblSKU_06.Text = mcl.SKU_B; UPCCode = mcl.UPC_2; }
                        if (Grade.ToUpper() == "C") { lblSKU_06.Text = mcl.SKU_C; UPCCode = mcl.UPC_3; }
                        lblCondition_06.Text = Manufacturer + "                " + Model + " " + mcl.NickName + " " + Colour;
                        lblDescription_06.Text = mcl.Description;
                        if (lblSKU_06.Text.Length > 0)
                        {
                            bcSku_06.Visible = true;
                            bcSku_06.DataToEncode = lblSKU_06.Text;
                        }
                        if (UPCCode.Length > 0)
                        {
                            bcUPC_06.Visible = true;
                            bcUPC_06.DataToEncode = UPCCode;
                        }
                    }
                    if (ReplacementESN.Length > 0) { IMEI_06.DataToEncode = ReplacementESN; }
                }
            }
        }
        private void PrintTag_07(decimal RDID, RepeaterItem Item)
        {
            Panel FPL_07 = (Panel)Item.FindControl("FPL_07");
            LinearBarcode bcUPC_07 = (LinearBarcode)Item.FindControl("bcUPC_07");
            LinearBarcode bcSku_07 = (LinearBarcode)Item.FindControl("bcSku_07");
            LinearBarcode IMEI_07 = (LinearBarcode)Item.FindControl("IMEI_07");

            Label lblGrade_07 = (Label)Item.FindControl("lblGrade_07");
            Label lblSKU_07 = (Label)Item.FindControl("lblSKU_07");
            Label lblCondition_07 = (Label)Item.FindControl("lblCondition_07");

            Label lbl_WarrantyExpire_07 = (Label)Item.FindControl("lbl_WarrantyExpire_07");
            Label lblDescription_07 = (Label)Item.FindControl("lblDescription_07");

            //Label lblGrade_05 = (Label)Item.FindControl("lblGrade_05");
            //Label lblGrade_04 = (Label)Item.FindControl("lblGrade_04");
            //Label lbl_WarrantyExpire_04 = (Label)Item.FindControl("lbl_WarrantyExpire_04");

            FPL_07.Visible = true;
            bcUPC_07.Visible = false;
            bcSku_07.Visible = false;
            if (RDID > 0)
            {
                rd = rdm.ReceiveDetail(ctx, RDID);
                if (rd != null)
                {
                    IMEI_07.DataToEncode = rd.ESN;
                    string Carrier = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Carrier");
                    string Manufacturer = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Manufacturer");
                    string Model = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Model");
                    string Colour = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Colour");
                    string Grade = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Grade");
                    string ReplacementESN = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Replacement IMEI");
                    lblGrade_07.Text = Grade.ToUpper();
                    mcl = mcm.GetMasterCarrierManufacturerLookup(ctx, rd.ReceiveDetailID, Carrier, Manufacturer, Model, Colour);
                    if (mcl != null)
                    {
                        string UPCCode = "";
                        // this may need to be adjusted to deal with Sku b, c etc.
                        if (Grade.ToUpper() == "A") { lblSKU_07.Text = mcl.SKU; UPCCode = mcl.UPC; }
                        if (Grade.ToUpper() == "B") { lblSKU_07.Text = mcl.SKU_B; UPCCode = mcl.UPC_2; }
                        if (Grade.ToUpper() == "C") { lblSKU_07.Text = mcl.SKU_C; UPCCode = mcl.UPC_3; }
                        lblCondition_07.Text = Manufacturer + "                " + Model + " " + mcl.NickName + " " + Colour;
                        lblDescription_07.Text = mcl.Description;
                        if (UPCCode.Length > 0)
                        {
                            bcUPC_07.Visible = true;
                            bcUPC_07.DataToEncode = UPCCode;
                        }
                    }
                    string WarrantyExpire = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Warranty Expiry Date");
                    DateTime WE = new DateTime(2000, 01, 01);
                    if (DateTime.TryParse(WarrantyExpire, out WE) == false) { WE = new DateTime(2000, 01, 01); }
                    if (WarrantyExpire.Length > 0 && WE >= DateTime.Now)
                    {
                        lbl_WarrantyExpire_07.Text = "Warranty Expiry:" + "(" + WarrantyExpire + ")";
                    }
                    if (ReplacementESN.Length > 0) { IMEI_07.DataToEncode = ReplacementESN; }
                }
            }
        }
        private void PrintTag_BB(decimal RDID, RepeaterItem Item)
        {
            Panel FPL_BB = (Panel)Item.FindControl("FPL_BB");
            LinearBarcode IMEI_BB = (LinearBarcode)Item.FindControl("IMEI_BB");
            //LinearBarcode bcSku_07 = (LinearBarcode)Item.FindControl("bcSku_07");
            //LinearBarcode IMEI_07 = (LinearBarcode)Item.FindControl("IMEI_07");

            Label lblsku_BB = (Label)Item.FindControl("lblsku_BB");
            Label lblID_BB = (Label)Item.FindControl("lblID_BB");
            Label lblManufacturer_BB = (Label)Item.FindControl("lblManufacturer_BB");

            Label lblGrade_BB = (Label)Item.FindControl("lblGrade_BB");
            //Label lblDescription_07 = (Label)Item.FindControl("lblDescription_07");

            //Label lblGrade_05 = (Label)Item.FindControl("lblGrade_05");
            //Label lblGrade_04 = (Label)Item.FindControl("lblGrade_04");
            //Label lbl_WarrantyExpire_04 = (Label)Item.FindControl("lbl_WarrantyExpire_04");

            FPL_BB.Visible = true;
            if (RDID > 0)
            {
                rd = rdm.ReceiveDetail(ctx, RDID);
                IMEI_BB.DataToEncode = rd.ESN;
                IMEI_BB.Visible = true;
                string Carrier = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Carrier");
                string Manufacturer = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "RQ4 Transfer #");
                string Grade = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Grade");
                string ReplacementESN = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Replacement IMEI");
                string SKU = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "SKU");
                string ID = rdm.GetReceiveDetailItem_DataElement(ctx, RDID, "Serial Number");

                lblsku_BB.Text = "SKU: " + SKU;
                lblID_BB.Text = "TAG #: " + ID;
                lblManufacturer_BB.Text = Manufacturer;
                lblGrade_BB.Text = "";
                lblGrade_BB.Text = rdm.TranslateGrade(Grade);
                if (ReplacementESN.Length > 0)
                {
                    IMEI_BB.Visible = true;
                    IMEI_BB.DataToEncode = ReplacementESN;
                }
            }
        }

        #region JodyBuiltHTMLFiles
        private void TestLoadOther(string Filename, RepeaterItem Item)
        {
            Panel P_OTHER = (Panel)Item.FindControl("P_OTHER");
            HtmlGenericControl P_OTHERDIV = (HtmlGenericControl)Item.FindControl("P_OTHERDIV");
            string Path = "";
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
            #region Sample
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
            #endregion

            string am = "";
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

        private string GetData(string Key)
        {
            if (Key.ToUpper() == "ESN") { return rd.ESN; }
            if (Key.ToUpper() == "IMEI") { return rd.ESN; }

            if (Key.ToUpper() == "RMANUMBER") { return rd.RMANumber; }
            if (Key.ToUpper() == "PROJECT") { return rd.Projects.Name; }
            if (Key.ToUpper() == "CLIENTLOCATION") { return rd.ClientLocation.CompanyName; }
            if (Key.ToUpper() == "CLIENT") { return rd.ClientLocation.Client.CompanyName; }
            if (Key.ToUpper() == "SCANKEY") { return rd.ClientLocation.ScanKey; }

            //if (isThreaded == true) { return GetThreadedSavedData(Key); }
            return GetDataFromDB(Key);
        }
        //private string GetThreadedSavedData(string Key)
        //{
        //    string rValue = jString.GetAllValues(Key, "Do GetDataFromDB");
        //    if (rValue != "Do GetDataFromDB") { return rValue; }
        //    return GetDataFromDB(Key);
        //}
        private string GetDataFromDB(string Key)
        {
            return rdm.GetReceiveDetailItem_DataElement(ctx, rd.ReceiveDetailID, Key);
        }
        private int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
        #endregion

        // ---------------------------------------------------
        //-------------------------------------------------
        public static string WrappableText(string source)
        {
            string nwln = Environment.NewLine;
            return "<p>" +
            source.Replace(nwln + nwln, "</p><p>")
            .Replace(nwln, "<br />") + "</p>";
        }




    }
}