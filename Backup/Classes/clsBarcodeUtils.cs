using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IDAutomation.LinearServerControl;
using System.Drawing;
using BW_WebApp.DataManagers;

using BW_WebApp;

namespace BW_WebApp.BarcodeUtils
{
    public class clsBarcodeUtils
    {

        public void SaveBarcodeToFile(string Data, string FileName)
        {
            //Create an instance of the Linear barcode server control
            //IDAutomation.LinearServerControl.LinearBarcode MyBarCode = new IDAutomation.LinearServerControl.LinearBarcode();
            LinearBarcode MyBarCode = new LinearBarcode();
            //Set the symbology
            MyBarCode.SymbologyID = IDAutomation.LinearServerControl.LinearBarcode.Symbologies.Code128;
            MyBarCode.ShowText = true;
            MyBarCode.ImageAutoDelete = true;
            MyBarCode.BarHeightCM = ".5";
            MyBarCode.LeftMarginCM = "0.000";
            MyBarCode.TopMarginCM = "0.15";
            //MyBarCode.WhiteBarIncrease = "0.50";
            MyBarCode.ImageResolution = 300;
            MyBarCode.Height = System.Web.UI.WebControls.Unit.Pixel(30);
            MyBarCode.Width = System.Web.UI.WebControls.Unit.Pixel(200);
            //MyBarCode.NarrowToWideRatio = "1.5";
            //MyBarCode.NarrowToWideRatio = "2.5";
            MyBarCode.XDimensionCM = "0.0400";
            MyBarCode.CheckCharacter = false;
            MyBarCode.CheckCharacterInText = false;
            //MyBarCode.SuppSeparationCM = "0.250";
            MyBarCode.DataToEncode = Data;


            //Set the DataToEncode
            //MyBarCode.ShowText = true;
            //MyBarCode.ImageResolution = 300;
            //MyBarCode.ImageAutoDelete = true;
            //MyBarCode.BarHeightCM = "0.5";
            //MyBarCode.LeftMarginCM = "0.000";
            //MyBarCode.TopMarginCM = ".15";
            //MyBarCode.WhiteBarIncrease = "0.75";
            //MyBarCode.ImageResolution = 300;
            //MyBarCode.CheckCharacter = false;
            //MyBarCode.CheckCharacterInText = false;
            //MyBarCode.Height = new System.Web.UI.WebControls.Unit(20, System.Web.UI.WebControls.UnitType.Pixel);
            //MyBarCode.ImageType = LinearBarcode.ImageTypes.JPEG;
            //MyBarCode.NarrowToWideRatio = "2.5";
            //MyBarCode.Width = new System.Web.UI.WebControls.Unit(2, System.Web.UI.WebControls.UnitType.Inch);
            //MyBarCode.XDimensionCM = "0.0200";
            //MyBarCode.DataToEncode = Data;
            //Save the image. The first parameter is the full path and file name of the image. The @ sign preceding the parameter allows 
            //the slash characters in the file name. The second parameter is the type of file to save; specify .bmp, gif, .jpeg, .png, etc.
            MyBarCode.SaveImageAs(@FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
        }

        public Image SaveBarcodeToImage_Base(string Data, float HeightCM, string TempFilePathAndName)
        {
            //if (pixelHeight < 15) { pixelHeight = 15; }
            //if (pixelWidth < 200) { pixelWidth = 200; }
            //Create an instance of the Linear barcode server control
            IDAutomation.LinearServerControl.LinearBarcode MyBarCode = new IDAutomation.LinearServerControl.LinearBarcode();
            MyBarCode.SymbologyID = IDAutomation.LinearServerControl.LinearBarcode.Symbologies.Code128;
            //Set the DataToEncode
            MyBarCode.ShowText = false;
            MyBarCode.ImageAutoDelete = true;
            MyBarCode.BarHeightCM = HeightCM.ToString();
            MyBarCode.LeftMarginCM = "0.000";
            MyBarCode.TopMarginCM = "0.15";
            MyBarCode.TopMarginCM = "0";
            //MyBarCode.WhiteBarIncrease = "0.50";
            MyBarCode.ImageResolution = 300;
            MyBarCode.Height = System.Web.UI.WebControls.Unit.Pixel(15);
            MyBarCode.Width = System.Web.UI.WebControls.Unit.Pixel(200);
            //MyBarCode.NarrowToWideRatio = "1.5";
            //MyBarCode.NarrowToWideRatio = "2.5";
            MyBarCode.XDimensionCM = "0.0400";
            MyBarCode.CheckCharacter = false;
            MyBarCode.CheckCharacterInText = false;
            //MyBarCode.SuppSeparationCM = "0.250";
            MyBarCode.DataToEncode = Data;
            //MyBarCode.StreamImage;
            MyBarCode.SaveImageAs(TempFilePathAndName, System.Drawing.Imaging.ImageFormat.Jpeg);
            Image xx = Image.FromFile(TempFilePathAndName);

            return xx;                              //bc.GetImage(new SizeF(386, 122), new MarginF(20, 10, 20, 5));
        }


        public Image SaveBarcodeToImage_Base(string Data)
        {
            //Create an instance of the Linear barcode server control
            IDAutomation.LinearServerControl.LinearBarcode MyBarCode = new IDAutomation.LinearServerControl.LinearBarcode();
            //Set the symbology
            //MyBarCode.SymbologyID = IDAutomation.LinearServerControl.LinearBarcode.Symbologies.Code39;
            //MyBarCode.SymbologyID = IDAutomation.LinearServerControl.LinearBarcode.Symbologies.Code93;
            //MyBarCode.SymbologyID = IDAutomation.LinearServerControl.LinearBarcode.Symbologies.Code93;
            //MyBarCode.SymbologyID = IDAutomation.LinearServerControl.LinearBarcode.Symbologies.Code93;

            MyBarCode.SymbologyID = IDAutomation.LinearServerControl.LinearBarcode.Symbologies.Code128;
            //Set the DataToEncode
            MyBarCode.ShowText = false;
            MyBarCode.ImageAutoDelete = true;
            MyBarCode.BarHeightCM = "1";
            MyBarCode.LeftMarginCM = "0.000";
            MyBarCode.TopMarginCM = "0.15";
            //MyBarCode.WhiteBarIncrease = "0.50";
            MyBarCode.ImageResolution = 300;
            MyBarCode.Height = System.Web.UI.WebControls.Unit.Pixel(15);
            MyBarCode.Width = System.Web.UI.WebControls.Unit.Pixel(200);
            //MyBarCode.NarrowToWideRatio = "1.5";
            //MyBarCode.NarrowToWideRatio = "2.5";
            MyBarCode.XDimensionCM = "0.0400";
            MyBarCode.CheckCharacter = false;
            MyBarCode.CheckCharacterInText = false;
            //MyBarCode.SuppSeparationCM = "0.250";
            MyBarCode.DataToEncode = Data;

            string TempDirectory = System.Configuration.ConfigurationManager.AppSettings["TempDirectory"];
            if (TempDirectory == null || TempDirectory.Length == 0)
            {
                TempDirectory = "~/IDAutomation";
            }


            MyBarCode.SaveImageAs(@"C:\Temp\Images\Test39.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            Image xx = Image.FromFile(@"C:\Temp\Images\Test39.jpg");
            return xx;                              //bc.GetImage(new SizeF(386, 122), new MarginF(20, 10, 20, 5));
        }

        public Image SaveBarcodeToImage_Base_02(string Data, string FileName)
        {
            //Create an instance of the Linear barcode server control
            IDAutomation.LinearServerControl.LinearBarcode MyBarCode = new IDAutomation.LinearServerControl.LinearBarcode();
            //Set the symbology
            //MyBarCode.SymbologyID = IDAutomation.LinearServerControl.LinearBarcode.Symbologies.Code39;
            //MyBarCode.SymbologyID = IDAutomation.LinearServerControl.LinearBarcode.Symbologies.Code93;
            //MyBarCode.SymbologyID = IDAutomation.LinearServerControl.LinearBarcode.Symbologies.Code93;
            //MyBarCode.SymbologyID = IDAutomation.LinearServerControl.LinearBarcode.Symbologies.Code93;

            MyBarCode.SymbologyID = IDAutomation.LinearServerControl.LinearBarcode.Symbologies.Code128;

            //            MyBarCode.SymbologyID = IDAutomation.LinearServerControl.LinearBarcode.Symbologies.Code93;
            //MyBarCode.SymbologyID = IDAutomation.LinearServerControl.LinearBarcode.Symbologies.Code39Ext;

            //<cc1:LinearBarcode ID="bcBagtag" runat="server" SymbologyID="Code128" ShowText="True"
            //    ImageAutoDelete="True" BarHeightCM="1" LeftMarginCM="0.000" TopMarginCM=".15"
            //    ImageResolution="300" DataToEncode="" CheckCharacter="False"
            //    CheckCharacterInText="False" Height="30px" ImageType="JPEG" 
            //    Width="200px" XDimensionCM="0.0400" />



            //Set the DataToEncode
            MyBarCode.ImageAutoDelete = true;

            MyBarCode.BarHeightCM = "1";
            MyBarCode.LeftMarginCM = "0.000";
            MyBarCode.TopMarginCM = "0.15";
            //MyBarCode.WhiteBarIncrease = "0.50";
            MyBarCode.ImageResolution = 300;
            MyBarCode.Height = System.Web.UI.WebControls.Unit.Pixel(30);
            MyBarCode.Width = System.Web.UI.WebControls.Unit.Pixel(200);
            //MyBarCode.NarrowToWideRatio = "1.5";
            //MyBarCode.NarrowToWideRatio = "2.5";
            MyBarCode.XDimensionCM = "0.0400";
            MyBarCode.CheckCharacter = false;
            MyBarCode.CheckCharacterInText = false;
            //MyBarCode.SuppSeparationCM = "0.250";
            MyBarCode.DataToEncode = Data;


            //<cc1:LinearBarcode ID="bcBagtag" runat="server" SymbologyID="Code39" ShowText="True"
            //    ImageAutoDelete="True" BarHeightCM="1" LeftMarginCM="0.000" TopMarginCM=".15"
            //    WhiteBarIncrease="0.75" ImageResolution="300" DataToEncode="" CheckCharacter="False"
            //    CheckCharacterInText="False" Height="30px" ImageType="JPEG" NarrowToWideRatio="2.5"
            //    SuppSeparationCM="0.450" Width="200px" XDimensionCM="0.0200" />




            //Save the image. The first parameter is the full path and file name of the image. The @ sign preceding the parameter allows 
            //the slash characters in the file name. The second parameter is the type of file to save; specify .bmp, gif, .jpeg, .png, etc.
            MyBarCode.SaveImageAs(FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
            Image xx = Image.FromFile(FileName);
            return xx;                              //bc.GetImage(new SizeF(386, 122), new MarginF(20, 10, 20, 5));
        }

        public Image SaveBarcodeToImage(string Data)
        {
            //Create an instance of the Linear barcode server control
            IDAutomation.LinearServerControl.LinearBarcode MyBarCode = new IDAutomation.LinearServerControl.LinearBarcode();

            MyBarCode.SymbologyID = IDAutomation.LinearServerControl.LinearBarcode.Symbologies.Code128;
            MyBarCode.ImageAutoDelete = true;

            MyBarCode.BarHeightCM = "1";
            MyBarCode.LeftMarginCM = "0.000";
            MyBarCode.TopMarginCM = "0.15";
            //MyBarCode.WhiteBarIncrease = "0.50";
            MyBarCode.ImageResolution = 300;
            MyBarCode.Height = System.Web.UI.WebControls.Unit.Pixel(30);
            MyBarCode.Width = System.Web.UI.WebControls.Unit.Pixel(200);
            //MyBarCode.NarrowToWideRatio = "1.5";
            //MyBarCode.NarrowToWideRatio = "2.5";
            MyBarCode.XDimensionCM = "0.0400";
            MyBarCode.CheckCharacter = false;
            MyBarCode.CheckCharacterInText = false;
            //MyBarCode.SuppSeparationCM = "0.250";
            MyBarCode.DataToEncode = Data;


            //<cc1:LinearBarcode ID="bcBagtag" runat="server" SymbologyID="Code39" ShowText="True"
            //    ImageAutoDelete="True" BarHeightCM="1" LeftMarginCM="0.000" TopMarginCM=".15"
            //    WhiteBarIncrease="0.75" ImageResolution="300" DataToEncode="" CheckCharacter="False"
            //    CheckCharacterInText="False" Height="30px" ImageType="JPEG" NarrowToWideRatio="2.5"
            //    SuppSeparationCM="0.450" Width="200px" XDimensionCM="0.0200" />



            //Stream y = MyBarCode.;
            //Save the image. The first parameter is the full path and file name of the image. The @ sign preceding the parameter allows 
            //the slash characters in the file name. The second parameter is the type of file to save; specify .bmp, gif, .jpeg, .png, etc.
            MyBarCode.SaveImageAs(@"C:\Temp\Images\Test39.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            Image xx = Image.FromFile(@"C:\Temp\Images\Test39.jpg");
            return xx;                              //bc.GetImage(new SizeF(386, 122), new MarginF(20, 10, 20, 5));
        }

        private string NewFileName(string UserName, string extension)
        {
            return string.Format("{0}.{1}", UserName.Trim(), extension);
        }

        ///// <summary>
        ///// Checks whether the current symbology can code the value entered in the text box.
        ///// </summary>
        //private bool SetBarcodeValue(BarcodeLabel bc, string value)
        //{
        //    try
        //    {
        //        // Set the value to code with the barcode. The property setter can throw the IncorrectBarcodeValueException if the current barcode symbology cannot code this value.
        //        bc.Value = value;
        //        //Since no exceptions were thrown there is no errors and we can hide the error message and enable button.
        //        return true;
        //    }
        //    // Catch IncorrectBarcodeValueException which means that the current symbology cannot code this value.
        //    catch (IncorrectBarcodeValueException e)
        //    {
        //        return false;
        //    }
        //}


        //        public Bitmap GetBarcode(string Data, string SecondayText)
        //        {
        //            //Linear barcode = new Linear();
        //            //barcode.Type = BarcodeType.CODE39;
        //            //barcode.Data = Data;
        //            ////barcode.Resolution =
        //            //barcode.ShowText = true;
        //            //barcode.drawBarcode(FileName);
        //            Image ximg;
        //            Bitmap barcodeInBitmap = new Bitmap(ximg); //= barcode.drawBarcode();

        ////            Graphics graphicsObject = Graphics.FromImage(barcodeInBitmap);

        //            return barcodeInBitmap;

        //        }




        //Linear barcode = new Linear();
        //barcode.Type = BarcodeType.CODE39;

        //// Set barcode value
        //barcode.Data = "123456789";

        //// Set barcode bar width (X module) and bar height (Y module)
        //barcode.X = 1;
        //barcode.Y = 60;

        //// Generate barcode and encode barcode to gif format
        //barcode.Format = ImageFormat.Gif;
        ////barcode.drawBarcode("c#-barcode.gif");

        //barcode.drawBarcode("c://Temp//TestBITMAP.bmp");
        ////LinearWebForm1 = barcode;

        //// Image1 = new Bitmap("c://Temp//TestBITMAP.bmp");

    }

}