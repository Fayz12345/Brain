<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BagTag.aspx.cs" Inherits="BW_WebApp.BagTag" %>

<%@ Register Assembly="IDAutomation.LinearServerControl" Namespace="IDAutomation.LinearServerControl" 
    TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml"> 
<head id="Head1" runat="server">
    <title></title>
</head>
<body onload="printIt();" style="padding-left:0px; margin-left:0px; padding-top: 0px; margin-top: 0px;">
    <form id="form1" runat="server">
    <asp:HiddenField ID="StopAutoExit" runat="server" />

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>




        <asp:Panel ID="TestBagTag" runat="server" Height="29mm" >
        <asp:Table ID="Table5" runat="server" CellPadding="0" CellSpacing="0" Width="62mm">
            <asp:TableRow ID="TableRow13">
                <asp:TableCell ColumnSpan="2">
                    <asp:Label ID="lblTestBagTag" runat="server" Text="" Font-Underline="true" Font-Size="Small" ></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow14">
                <asp:TableCell ColumnSpan="2">
                    <asp:Label ID="lblUPC_TestBagTag" runat="server" Text="UPC:" Font-Size="Small"></asp:Label><br />
                    <cc1:LinearBarcode ID="UPC_TestBagTag" runat="server" SymbologyID="Code128"
                        ShowText="False" ImageAutoDelete="True" BarHeightCM=".40" LeftMarginCM="0.000"
                        TopMarginCM=".0" ImageResolution="300" DataToEncode="" CheckCharacter="False"
                        CheckCharacterInText="False" Height=".40in" ImageType="JPEG" Width="200px" XDimensionCM="0.0400" /><br />
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow15">
                <asp:TableCell ColumnSpan="2">
                   <asp:Label ID="lblIMEI_TestBagTag" runat="server" Text="IMEI:" Font-Overline="true" Font-Size="Small"></asp:Label><br />
                    <cc1:LinearBarcode ID="IMEI_TestBagTag" runat="server" SymbologyID="Code128" ShowText="False"
                        ImageAutoDelete="True" BarHeightCM=".40" LeftMarginCM="0.000" TopMarginCM=".0"
                        ImageResolution="300" DataToEncode="" CheckCharacter="False" CheckCharacterInText="False"
                        Height=".40in" ImageType="JPEG" Width="200px" XDimensionCM="0.0400" />
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </asp:Panel>

<%--
    <asp:Panel ID="RegularBagTag_03_02" runat="server" Height="29mm" >--%>
    <asp:Panel ID="RegularBagTag_03_02" runat="server" Height="20mm" >
        <asp:Table ID="Table2_03_02" runat="server" CellPadding="0" CellSpacing="0" Width="62mm">
            <asp:TableRow ID="TableRow10">
                <asp:TableCell ColumnSpan="2">
                    <asp:Label ID="lblTelusSKU_03_02" runat="server" Text="" Font-Underline="true" Font-Size="Small" ></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow11">
                <asp:TableCell ColumnSpan="2">
                    <asp:Label ID="UPC_03_02" runat="server" Text="UPC:" Font-Size="Small"></asp:Label><br />
                    <cc1:LinearBarcode ID="UPC_bottom_03_02" runat="server" SymbologyID="Code128"
                        ShowText="False" ImageAutoDelete="True" BarHeightCM=".40" LeftMarginCM="0.000"
                        TopMarginCM=".0" ImageResolution="300" DataToEncode="" CheckCharacter="False"
                        CheckCharacterInText="False" Height=".40in" ImageType="JPEG" Width="200px" XDimensionCM="0.0400" /><br />
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow12">
                <asp:TableCell ColumnSpan="2">
                   <asp:Label ID="IMEI_03_02" runat="server" Text="IMEI:" Font-Overline="true" Font-Size="Small"></asp:Label><br />
                    <cc1:LinearBarcode ID="bcBagtag_03_02" runat="server" SymbologyID="Code128" ShowText="False"
                        ImageAutoDelete="True" BarHeightCM=".40" LeftMarginCM="0.000" TopMarginCM=".0"
                        ImageResolution="300" DataToEncode="" CheckCharacter="False" CheckCharacterInText="False"
                        Height=".40in" ImageType="JPEG" Width="200px" XDimensionCM="0.0400" />
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </asp:Panel>


    <asp:Panel ID="RegularBagTag_03_03" runat="server" Height="23mm">
        <asp:Table ID="Table2_03_03" runat="server" CellPadding="0" CellSpacing="0" Width="62mm">
            <asp:TableRow ID="TableRow5">
                <asp:TableCell ColumnSpan="2">
                    <asp:Label ID="lblTelusSKU_03_03" runat="server" Text="" Font-Underline="true" Font-Size="Small"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow8">
                <asp:TableCell ColumnSpan="2">
                    <asp:Label ID="UPC_03_03" runat="server" Text="UPC:" Font-Size="Small"></asp:Label><br />
                    <cc1:LinearBarcode ID="UPC_bottom_03_03" runat="server" SymbologyID="Code128"
                        ShowText="False" ImageAutoDelete="True" BarHeightCM=".45" LeftMarginCM="0.000"
                        TopMarginCM=".0" ImageResolution="300" DataToEncode="" CheckCharacter="False"
                        CheckCharacterInText="False" Height=".450in" ImageType="JPEG" Width="200px" XDimensionCM="0.0400" /><br />
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow7">
                <asp:TableCell ColumnSpan="2">
                   <asp:Label ID="IMEI_03_03" runat="server" Text="IMEI:" Font-Overline="true" Font-Size="Small"></asp:Label><br />
                    <cc1:LinearBarcode ID="bcBagtag_03_03" runat="server" SymbologyID="Code128" ShowText="False"
                        ImageAutoDelete="True" BarHeightCM=".45" LeftMarginCM="0.000" TopMarginCM=".0"
                        ImageResolution="300" DataToEncode="" CheckCharacter="False" CheckCharacterInText="False"
                        Height=".450in" ImageType="JPEG" Width="200px" XDimensionCM="0.0400" />
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </asp:Panel>
    <asp:Panel ID="RegularBagTag_03_04" runat="server" Height="20mm">
        <asp:Table ID="Table2_03_04" runat="server" CellPadding="0" CellSpacing="0" Width="62mm">
            <asp:TableRow ID="TableRow4">
                <asp:TableCell ColumnSpan="2">
                    <asp:Label ID="lblTelusSKU_03_04" runat="server" Text="" Font-Underline="true" Font-Size="Small"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow6">
                <asp:TableCell ColumnSpan="2">
                    <asp:Label ID="UPC_03_04" runat="server" Text="UPC:" Font-Size="Small"></asp:Label><br />
                    <cc1:LinearBarcode ID="UPC_bottom_03_04" runat="server" SymbologyID="Code128"
                        ShowText="False" ImageAutoDelete="True" BarHeightCM=".45" LeftMarginCM="0.000"
                        TopMarginCM=".0" ImageResolution="300" DataToEncode="" CheckCharacter="False"
                        CheckCharacterInText="False" Height=".450in" ImageType="JPEG" Width="200px" XDimensionCM="0.0400" /><br />
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow9">
                <asp:TableCell ColumnSpan="2">
                   <asp:Label ID="IMEI_03_04" runat="server" Text="IMEI:" Font-Overline="true" Font-Size="Small"></asp:Label><br />
                    <cc1:LinearBarcode ID="bcBagtag_03_04" runat="server" SymbologyID="Code128" ShowText="False"
                        ImageAutoDelete="True" BarHeightCM=".45" LeftMarginCM="0.000" TopMarginCM=".0"
                        ImageResolution="300" DataToEncode="" CheckCharacter="False" CheckCharacterInText="False"
                        Height=".450in" ImageType="JPEG" Width="200px" XDimensionCM="0.0400" />
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </asp:Panel>






























    <asp:Panel ID="RegularBagTag" runat="server" Height="86mm">
         <asp:Table ID="Table2" runat="server" CellPadding="0" CellSpacing="0" Width="62mm">
             <asp:TableRow ID="BagTagBar">
                 <asp:TableCell ColumnSpan="2">
                     <asp:Table runat="server">
                         <asp:TableRow Width="200px">
                             <asp:TableCell HorizontalAlign="Left">
                                 <asp:Label ID="lblTagVersion" runat="server" Text="" Font-Size="Smaller"></asp:Label>
                             </asp:TableCell>
                             <asp:TableCell HorizontalAlign="Left">
                                 <asp:Label ID="lblReceiveDetail" runat="server" Text="" Font-Size="Smaller"></asp:Label>
                             </asp:TableCell>
                         </asp:TableRow>
                     </asp:Table>
                     <br />
                     <asp:Label ID="lblMasterClient" runat="server" Text="Master Client" Font-Size="Smaller"></asp:Label>
                     <br />
                     <cc1:LinearBarcode ID="bcBagtag" runat="server" SymbologyID="Code128" ShowText="True"
                         ImageAutoDelete="True" BarHeightCM=".5" LeftMarginCM="0.000" TopMarginCM=".15"
                         ImageResolution="300" DataToEncode="" CheckCharacter="False" CheckCharacterInText="False"
                         Height=".75in" ImageType="JPEG" Width="200px" XDimensionCM="0.0400" />
                 </asp:TableCell>
             </asp:TableRow>
             <asp:TableRow runat="server" ID="SerialNumberBar">
                 <asp:TableCell ColumnSpan="2">
                     <cc1:LinearBarcode ID="bcSerialNumber" runat="server" SymbologyID="Code128" ShowText="True"
                         ImageAutoDelete="True" BarHeightCM=".5" LeftMarginCM="0.000" TopMarginCM=".15"
                         ImageResolution="300" DataToEncode="" CheckCharacter="False"
                         CheckCharacterInText="False" Height="15px" ImageType="JPEG"
                         Width="200px" XDimensionCM="0.0400" Visible="True" />
                 </asp:TableCell>
             </asp:TableRow>
             <asp:TableRow>
                 <asp:TableCell HorizontalAlign="Left" VerticalAlign="Top">
                     <asp:Image ID="imgGMPLogo" runat="server" ImageUrl="Images/Logo.jpg" Height=".25in"
                         Visible="False" />
                 </asp:TableCell>
                 <asp:TableCell>
                     <asp:Label ID="lblFullText" runat="server" Text="Full" Font-Size="Smaller"></asp:Label>
                 </asp:TableCell>
             </asp:TableRow>


             <asp:TableRow>
                 <asp:TableCell ColumnSpan="2">
                     <asp:Label ID="Label1" runat="server" Text="ClientID" Font-Size="X-Small"></asp:Label><br />
                     <cc1:LinearBarcode ID="Location" runat="server" SymbologyID="Code128" ShowText="True"
                         ImageAutoDelete="True" BarHeightCM=".5" LeftMarginCM="0.000" TopMarginCM=".15"
                         ImageResolution="300" DataToEncode="" CheckCharacter="False"
                         CheckCharacterInText="False" Height=".75in" ImageType="JPEG"
                         Width="200px" XDimensionCM="0.0400" Visible="True" />
                 </asp:TableCell>
             </asp:TableRow>

             <asp:TableRow>
                 <asp:TableCell ColumnSpan="2">
                     <asp:Label ID="lblCondition01" runat="server" Text="" Font-Size="Small"></asp:Label><br />
                 </asp:TableCell>
             </asp:TableRow>
             <asp:TableRow>
                 <asp:TableCell ColumnSpan="2">
                 <br />
                 <br />
                     <asp:Label ID="lblReplaceESN_01" runat="server" Text="" Font-Size="Small"></asp:Label><br />
                 </asp:TableCell>
             </asp:TableRow>

         </asp:Table>
    </asp:Panel>


    <asp:Panel ID="RegularBagTag_02" runat="server" Height="86mm">
         <asp:Table ID="Table2_02" runat="server" CellPadding="0" CellSpacing="0" Width="62mm">
             <asp:TableRow ID="BagTagBar_02">
                 <asp:TableCell ColumnSpan="2">

                     <asp:Table ID="Table4" runat="server">
                         <asp:TableRow Width="200px">
                             <asp:TableCell HorizontalAlign="Left">
                                 <asp:Label ID="lblTagVersion_02" runat="server" Text="2.0" Font-Size="Smaller"></asp:Label>
                             </asp:TableCell>
                             <asp:TableCell HorizontalAlign="Left">
                                 <asp:Label ID="lblReceiveDetail_02" runat="server" Text="" Font-Size="Smaller"></asp:Label>
                             </asp:TableCell>
                         </asp:TableRow>
                     </asp:Table>
                     <asp:Label ID="lblMasterClient_02" runat="server" Text="Master Client" Font-Size="Smaller"></asp:Label>
                     <br />
                     <cc1:LinearBarcode ID="bcBagtag_02" runat="server" SymbologyID="Code128" ShowText="True"
                         ImageAutoDelete="True" BarHeightCM=".5" LeftMarginCM="0.000" TopMarginCM=".15"
                         ImageResolution="300" DataToEncode="" CheckCharacter="False"
                         CheckCharacterInText="False" Height=".75in" ImageType="JPEG" 
                         Width="200px" XDimensionCM="0.0400" />

                 </asp:TableCell>
             </asp:TableRow>
<%--             <asp:TableRow runat="server" ID="SerialNumberBar_02">
                 <asp:TableCell ColumnSpan="2">
                     <cc1:LinearBarcode ID="bcSerialNumber_02" runat="server" SymbologyID="Code128" ShowText="True"
                         ImageAutoDelete="True" BarHeightCM=".5" LeftMarginCM="0.000" TopMarginCM=".15"
                         ImageResolution="300" DataToEncode="" CheckCharacter="False"
                         CheckCharacterInText="False" Height="15px" ImageType="JPEG"
                         Width="200px" XDimensionCM="0.0400" Visible="True" />
                 </asp:TableCell>
             </asp:TableRow>--%>
             <asp:TableRow>
                 <asp:TableCell HorizontalAlign="Left" VerticalAlign="Top">
                     <asp:Image ID="imgGMPLogo_02" runat="server" ImageUrl="Images/Logo.jpg" Height=".25in"
                         Visible="False" />
                 </asp:TableCell>
                 <asp:TableCell>
                     <asp:Label ID="lblFullText_02" runat="server" Text="Full" Font-Size="Smaller"></asp:Label>
                 </asp:TableCell>
             </asp:TableRow>


             <asp:TableRow>
                 <asp:TableCell ColumnSpan="2">
                 <asp:Label ID="ML_STOCK_COLOUR_CODE_02" runat="server" Text="" Font-Bold="True"></asp:Label><br />
                 <asp:Label ID="Redial_GMP_Condition_02" runat="server" Text="" Font-Size="Smaller"></asp:Label>
<%--                     <asp:Label ID="Label1_02" runat="server" Text="ClientID" Font-Size="X-Small"></asp:Label><br />
                     <cc1:LinearBarcode ID="Location_02" runat="server" SymbologyID="Code128" ShowText="True"
                         ImageAutoDelete="True" BarHeightCM=".5" LeftMarginCM="0.000" TopMarginCM=".15"
                         ImageResolution="300" DataToEncode="" CheckCharacter="False"
                         CheckCharacterInText="False" Height=".75in" ImageType="JPEG"
                         Width="200px" XDimensionCM="0.0400" Visible="True" />--%>
                 </asp:TableCell>
             </asp:TableRow>

             <asp:TableRow>
                 <asp:TableCell ColumnSpan="2">
                     <asp:Label ID="lblCondition_02" runat="server" Text="" Font-Size="Small"></asp:Label><br />
                 </asp:TableCell>
             </asp:TableRow>
             <asp:TableRow>
                 <asp:TableCell ColumnSpan="2">
                 <br />
                 <br />
                     <asp:Label ID="lblReplaceESN_01_02" runat="server" Text="" Font-Size="Small"></asp:Label><br />
                 </asp:TableCell>
             </asp:TableRow>

         </asp:Table>
    </asp:Panel>



    <asp:Panel ID="RegularBagTag_03SKU" runat="server" Height="86mm">
        <asp:Table ID="Table2_03SKU" runat="server" CellPadding="0" CellSpacing="0" Width="62mm">
            <asp:TableRow ID="BagTagBar_03SKU">
                <asp:TableCell ColumnSpan="2">
                <br /><br />
                    <cc1:LinearBarcode ID="bcBagtag_03SKU" runat="server" SymbologyID="Code128" ShowText="True"
                        ImageAutoDelete="True" BarHeightCM=".5" LeftMarginCM="0.000" TopMarginCM=".15"
                        ImageResolution="300" DataToEncode="" CheckCharacter="False" CheckCharacterInText="False"
                        Height=".75in" ImageType="JPEG" Width="200px" XDimensionCM="0.0400" /><br />
                    <asp:Label ID="lblManModCol_03SKU" runat="server" Text="Manufacturer, Model, Colour" ></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
      <%--      <asp:TableRow ID="RowLabel1_03SKU">
                <asp:TableCell ColumnSpan="2">
                    <asp:Label ID="lblProvider_03SKU" runat="server" Text="Provider"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>--%>
            <asp:TableRow ID="TableRow1">
                <asp:TableCell ColumnSpan="2">
                    <asp:Label ID="Label6" runat="server" Text="" Font-Size="Smaller">--------------------------------------</asp:Label><br />
                    <cc1:LinearBarcode ID="bcBagtag_bottom_03SKU" runat="server" SymbologyID="Code128" ShowText="True"
                        ImageAutoDelete="True" BarHeightCM=".5" LeftMarginCM="0.000" TopMarginCM=".15"
                        ImageResolution="300" DataToEncode="" CheckCharacter="False" CheckCharacterInText="False"
                        Height=".75in" ImageType="JPEG" Width="200px" /><br />
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="RowML_STOCK_COLOUR_CODE_03SKU">
                <asp:TableCell ColumnSpan="2">
                    <asp:Label ID="lblProjectName_03SKU" runat="server" Text="ProjectName" ></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="RowlblReplaceESN_01_03SKU">
                <asp:TableCell ColumnSpan="2">
                    <asp:Label ID="lblvendor_03SKU" runat="server" Text="Vendor"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow17">
                <asp:TableCell ColumnSpan="2">
                    <asp:Label ID="lblProjectTag_03SKU" runat="server" Text="ProjectTag"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow18">
                <asp:TableCell ColumnSpan="2">
                    <asp:Label ID="lblReceiveDate_03SKU" runat="server" Text="ReceiveDate"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>

        </asp:Table>
    </asp:Panel>

    <asp:Panel ID="RegularBagTag_03" runat="server" Height="86mm">
        <asp:Table ID="Table2_03" runat="server" CellPadding="0" CellSpacing="0" Width="62mm">
            <asp:TableRow ID="BagTagBar_03">
                <asp:TableCell ColumnSpan="2">
                    <asp:Table ID="Table7" runat="server">
                        <asp:TableRow Width="200px">
                            <asp:TableCell HorizontalAlign="Left">
                                <asp:Label ID="lblTagVersion_03" runat="server" Text="2.0" Font-Size="Smaller"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="Left">
                                <asp:Label ID="lblReceiveDetail_03" runat="server" Text="" Font-Size="Smaller"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                    <asp:Label ID="lblMasterClient_03" runat="server" Text="Master Client" Font-Size="Smaller"></asp:Label>
                    <br />
                    <cc1:LinearBarcode ID="bcBagtag_03" runat="server" SymbologyID="Code128" ShowText="True"
                        ImageAutoDelete="True" BarHeightCM=".5" LeftMarginCM="0.000" TopMarginCM=".15"
                        ImageResolution="300" DataToEncode="" CheckCharacter="False" CheckCharacterInText="False"
                        Height=".75in" ImageType="JPEG" Width="200px" XDimensionCM="0.0400" />
                </asp:TableCell>
            </asp:TableRow>
            <%--             <asp:TableRow runat="server" ID="SerialNumberBar_03">
                 <asp:TableCell ColumnSpan="2">
                     <cc1:LinearBarcode ID="bcSerialNumber_03" runat="server" SymbologyID="Code128" ShowText="True"
                         ImageAutoDelete="True" BarHeightCM=".5" LeftMarginCM="0.000" TopMarginCM=".15"
                         ImageResolution="300" DataToEncode="" CheckCharacter="False"
                         CheckCharacterInText="False" Height="15px" ImageType="JPEG"
                         Width="200px" XDimensionCM="0.0400" Visible="True" />
                 </asp:TableCell>
             </asp:TableRow>--%>
            <asp:TableRow ID="RowlblFullText_03">
                <asp:TableCell HorizontalAlign="Left" VerticalAlign="Top">
                    <asp:Image ID="imgGMPLogo_03" runat="server" ImageUrl="Images/Logo.jpg" Height=".25in"
                        Visible="False" />
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="lblFullText_03" runat="server" Text="Full" Font-Size="Smaller"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="RowLabel1_03">
                <asp:TableCell ColumnSpan="2">
                    <asp:Label ID="Label1_03" runat="server" Text="" Font-Size="Smaller"></asp:Label><br />
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="RowML_STOCK_COLOUR_CODE_03">
                <asp:TableCell ColumnSpan="2">
                    <asp:Label ID="ML_STOCK_COLOUR_CODE_03" runat="server" Text="" Font-Bold="True"></asp:Label><br />
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="RowlblReplaceESN_01_03">
                <asp:TableCell ColumnSpan="2">
                    <asp:Label ID="lblReplaceESN_01_03" runat="server" Text="" Font-Size="Small"></asp:Label><br />
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow16">
                <asp:TableCell ColumnSpan="2">
                    <asp:Label ID="Label3" runat="server" Text="" Font-Size="Smaller">--------------------------------------</asp:Label><br />
                    <cc1:LinearBarcode ID="bcBagtag_bottom_03" runat="server" SymbologyID="Code128" ShowText="True"
                        ImageAutoDelete="True" BarHeightCM=".5" LeftMarginCM="0.000" TopMarginCM=".15"
                        ImageResolution="300" DataToEncode="" CheckCharacter="False" CheckCharacterInText="False"
                        Height=".75in" ImageType="JPEG" Width="200px" XDimensionCM="0.0400" /><br />
                    <asp:Label ID="lblFullText_bottom_03" runat="server" Text="" Font-Size="Smaller"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </asp:Panel>

    <asp:Panel ID="LoanderBagTag" runat="server" Visible="False" Height="86mm">
         <asp:Table ID="Table1" runat="server" CellPadding="0" CellSpacing="0" Width="62mm">
             <asp:TableRow ID="BagTagBar_L">

                 <asp:TableCell RowSpan="3">
                     <cc1:LinearBarcode ID="bcBagtag_L" runat="server" SymbologyID="Code128" ShowText="True"
                         ImageAutoDelete="True" BarHeightCM=".5" LeftMarginCM="0.000" TopMarginCM=".15"
                         ImageResolution="300" DataToEncode="" CheckCharacter="False"
                         CheckCharacterInText="False" Height=".75in" ImageType="JPEG" 
                         Width="200px" XDimensionCM="0.0400" RotationAngle="Two_Hundred_Seventy_Degrees" />
                 </asp:TableCell>

                 <asp:TableCell ColumnSpan="2">
                     <asp:Label ID="lblTagVersion_L" runat="server" Text="2.0" Font-Size="Smaller"></asp:Label>
                 </asp:TableCell>

             </asp:TableRow>
<%--             <asp:TableRow runat="server" ID="SerialNumberBar_L">
                 <asp:TableCell ColumnSpan="2">
                     <cc1:LinearBarcode ID="bcSerialNumber_L" runat="server" SymbologyID="Code128" ShowText="True"
                         ImageAutoDelete="True" BarHeightCM=".5" LeftMarginCM="0.000" TopMarginCM=".15"
                         ImageResolution="300" DataToEncode="" CheckCharacter="False"
                         CheckCharacterInText="False" Height="15px" ImageType="JPEG"
                         Width="200px" XDimensionCM="0.0400" Visible="True" />
                 </asp:TableCell>
             </asp:TableRow>--%>
             <asp:TableRow>
                 <asp:TableCell HorizontalAlign="Left" VerticalAlign="Top">
                     <asp:Image ID="imgGMPLogo_L" runat="server" ImageUrl="Images/Logo.jpg" Height=".25in"
                         Visible="False" />
                 </asp:TableCell>
                 <asp:TableCell>
                     <asp:Label ID="lblFullText_L" runat="server" Text="Full" Font-Size="Smaller"></asp:Label>

                     <br />
                     <asp:Label ID="lblCarrier_L" runat="server" Text=""></asp:Label>
                     <br />
                     <asp:Label ID="lblLoanerType_L" runat="server" Text=""></asp:Label>

                 </asp:TableCell>
             </asp:TableRow>

             <asp:TableRow>
                 <asp:TableCell ColumnSpan="2">
                     <asp:Label ID="lblCondition01_L" runat="server" Text="" Font-Size="Small"></asp:Label><br />
                 </asp:TableCell>
             </asp:TableRow>


<%--             <asp:TableRow>
                 <asp:TableCell ColumnSpan="2">
                     <asp:Label ID="Label1_L" runat="server" Text="ClientID" Font-Size="X-Small"></asp:Label><br />
                     <cc1:LinearBarcode ID="Location_L" runat="server" SymbologyID="Code128" ShowText="True"
                         ImageAutoDelete="True" BarHeightCM=".5" LeftMarginCM="0.000" TopMarginCM=".15"
                         ImageResolution="300" DataToEncode="" CheckCharacter="False"
                         CheckCharacterInText="False" Height=".75in" ImageType="JPEG"
                         Width="200px" XDimensionCM="0.0400" Visible="True" RotationAngle="Ninety_Degrees" />
                 </asp:TableCell>
             </asp:TableRow>--%>

         </asp:Table>
    </asp:Panel>


    <asp:Panel ID="ReturnedPart" runat="server" Visible="False" Height="86mm">
        <asp:Table ID="Table3" runat="server" CellPadding="0" CellSpacing="0" Width="62mm">

             <asp:TableRow>
                 <asp:TableCell ColumnSpan="2">
                     <asp:Label ID="Label2" runat="server" Text="Returned Part" Font-Size="Large"></asp:Label>
                 </asp:TableCell>
             </asp:TableRow>

             <asp:TableRow runat="server" ID="TableRow2">
                 <asp:TableCell ColumnSpan="2">
                     <cc1:LinearBarcode ID="bcReturnedPartIMEI" runat="server" SymbologyID="Code128" ShowText="True"
                         ImageAutoDelete="True" BarHeightCM=".5" LeftMarginCM="0.000" TopMarginCM=".15"
                         ImageResolution="300" DataToEncode="" CheckCharacter="False"
                         CheckCharacterInText="False" Height="15px" ImageType="JPEG"
                         Width="200px" XDimensionCM="0.0400" Visible="True" />
                 </asp:TableCell>
             </asp:TableRow>

             <asp:TableRow>
                 <asp:TableCell ColumnSpan="2">
                     <cc1:LinearBarcode ID="bcReturnedPartPartNumber" runat="server" SymbologyID="Code128" ShowText="True"
                         ImageAutoDelete="True" BarHeightCM=".5" LeftMarginCM="0.000" TopMarginCM=".15"
                         ImageResolution="300" DataToEncode="" CheckCharacter="False"
                         CheckCharacterInText="False" Height="15px" ImageType="JPEG"
                         Width="200px" XDimensionCM="0.0400" Visible="True" />
<%--                     <asp:Label ID="Label3" runat="server" Text="Part Number: " Font-Size="Smaller"></asp:Label>
                     <asp:Label ID="lblReturnedPartPartNumber" runat="server" Text="Part Number" Font-Size="Smaller" Font-Bold="True"></asp:Label>--%>
                 </asp:TableCell>
             </asp:TableRow>


             <asp:TableRow>
                 <asp:TableCell ColumnSpan="2">
                     <asp:Label ID="Label4" runat="server" Text="Technician: " Font-Size="Smaller"></asp:Label>
                     <asp:Label ID="lblReturnedPartTech" runat="server" Text="Tech Friendly Name" Font-Size="Smaller" Font-Bold="True"></asp:Label>
                 </asp:TableCell>
             </asp:TableRow>

             <asp:TableRow>
                 <asp:TableCell ColumnSpan="2">
                     <asp:Label ID="Label5" runat="server" Text="Date Returned: " Font-Size="Smaller"></asp:Label>
                     <asp:Label ID="lblReturnedPartDate" runat="server" Text="Date Returned" Font-Size="Smaller" Font-Bold="True"></asp:Label>
                 </asp:TableCell>
             </asp:TableRow>
             <asp:TableRow runat="server" ID="TableRow3">
                 <asp:TableCell ColumnSpan="2">
                     <cc1:LinearBarcode ID="bcReturnedPartTechAssignedID" runat="server" SymbologyID="Code128" ShowText="True"
                         ImageAutoDelete="True" BarHeightCM=".5" LeftMarginCM="0.000" TopMarginCM=".15"
                         ImageResolution="300" DataToEncode="" CheckCharacter="False"
                         CheckCharacterInText="False" Height="15px" ImageType="JPEG"
                         Width="200px" XDimensionCM="0.0400" Visible="True" />
                 </asp:TableCell>
             </asp:TableRow>


             <asp:TableRow>
                 <asp:TableCell ColumnSpan="2">
                 <br />
                 <br />
                     <asp:Label ID="lblReturnType" runat="server" Text="" Font-Size="Large"></asp:Label>
                 </asp:TableCell>
             </asp:TableRow>



         </asp:Table>
    </asp:Panel>




    <asp:Panel ID="P_OTHER" runat="server" Visible="false" Height="86mm">
        <div id="P_OTHERDIV" runat="server">

        </div>
    </asp:Panel>
    </form>
</body>

<script type="text/javascript">
    function printIt() {
        window.print();
        window.close();
//        var StopAutoExit = $get("<%= StopAutoExit.ClientID %>").value
//        if (StopAutoExit != "Y") {
//            window.print();
//            window.close();
//        }
    }
</script>




</html>

