<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FinishProductLabel_Bulk.aspx.cs" Inherits="BW_WebApp.FinishProductLabel_Bulk" %>


<%@ Register Assembly="IDAutomation.LinearServerControl" Namespace="IDAutomation.LinearServerControl" TagPrefix="cc1" %> 

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">  
<head id="Head1" runat="server">
    <title></title>
</head>
<body onload="printIt();" style="padding-left:0; margin-left:0">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
        <%--<asp:Label ID="message" runat="server" Text=""></asp:Label>--%>
        <%--<asp:HiddenField ID="StopAutoExit" runat="server" />--%>
        <asp:Repeater ID="Lablels" runat="server">
            <ItemTemplate>

        <asp:Panel runat="server" ID="KIT_FWDLOG" Width="70mm">
            <asp:Table ID="Table11N" runat="server" CellPadding="0" CellSpacing="0" Width="68mm">
                <asp:TableRow>
<%--                    <asp:TableCell HorizontalAlign="Left" VerticalAlign="Top">
                        <asp:Label ID="Label23" runat="server" Text="IMEI:"></asp:Label>
                    </asp:TableCell>--%>
                    <asp:TableCell HorizontalAlign="Left" VerticalAlign="Top">
                        <cc1:LinearBarcode ID="IMEI_05N" runat="server" SymbologyID="Code128" ShowText="True"
                            ImageAutoDelete="True" BarHeightCM=".75" LeftMarginCM="0.000" TopMarginCM=".15"
                            ImageResolution="300" DataToEncode="" CheckCharacter="False" CheckCharacterInText="False"
                            Height=".125in" ImageType="JPEG" Width="200px" XDimensionCM="0.0400" />
                            <br />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell Wrap="False" HorizontalAlign="Left">
                        <asp:Label ID="lblManufacturer_05N" runat="server" Text="Manufacturer" Font-Bold="True"
                            Font-Size="Small"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell Wrap="False" HorizontalAlign="Left">
                        <asp:Label ID="lblColour_05N" runat="server" Text="Colour" Font-Bold="True" Width="100%"
                            Font-Size="Small"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell Wrap="False" HorizontalAlign="Left">
                        <asp:Label ID="lblGrade_05N" runat="server" Text="" Font-Bold="True" Width="100%" 
                            Font-Size="Small"></asp:Label>
                            <br />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell Wrap="False" HorizontalAlign="Left">
                        <asp:Label ID="lblCarrierLockCode_05N" runat="server" Text="" Font-Size="Small"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </asp:Panel>




       <asp:Panel runat="server" ID="KIT_WIND" Width="70mm" Height="48mm">
            <asp:Table ID="Table14" runat="server" CellPadding="0" CellSpacing="0" Width="65mm">
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign="Left" Wrap="False" ColumnSpan="2">
                        <asp:Label ID="lblDescription_WindN" runat="server" Text="xxxxxxxxxxxxxxxxxxxxxxxxx" Font-Size="Small"></asp:Label><br />
                        <asp:Label ID="Label24" runat="server" Text="___________________________________" Font-Bold="True"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign="Right" VerticalAlign="Top">
                    IMEI:
                    </asp:TableCell>
                    <asp:TableCell HorizontalAlign="Left">
                        <cc1:LinearBarcode ID="IMEI_WIndN" runat="server" SymbologyID="Code128" ShowText="True"
                            ImageAutoDelete="True" BarHeightCM=".5" LeftMarginCM="0.000" TopMarginCM=".15"
                            ImageResolution="300" DataToEncode="352480046354039" CheckCharacter="False" CheckCharacterInText="False"
                            Height=".25in" ImageType="JPEG" Width="200px" XDimensionCM="0.0400" />
                            <br />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign="Right" VerticalAlign="Top">
                    SKU:
                    </asp:TableCell>
                    <asp:TableCell HorizontalAlign="Left">
                        <cc1:LinearBarcode ID="SKUA_WIndN" runat="server" SymbologyID="Code128" ShowText="True"
                            ImageAutoDelete="True" BarHeightCM=".5" LeftMarginCM="0.000" TopMarginCM=".15"
                            ImageResolution="300" DataToEncode="352480046354039" CheckCharacter="False" CheckCharacterInText="False"
                            Height=".25in" ImageType="JPEG" Width="200px" XDimensionCM="0.0400" />
                            <br />

                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </asp:Panel>



                <asp:Panel runat="server" ID="FTP_WIND">
                    <asp:Table ID="Table12" runat="server" CellPadding="0" CellSpacing="0" Width="85mm">
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label ID="Label22" runat="server" Text="Description:" Font-Size="Small"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="Right" Wrap="False">
                                <asp:Label ID="lblDescription_Wind" runat="server" Text="xxxxxxxxxxxxxxxxxxxxxxxxx"
                                    Font-Size="Small"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="2">
                                <asp:Label ID="Label17x" runat="server" Text="____________________________________________"
                                    Font-Bold="True"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Left" VerticalAlign="Top">
                    IMEI:
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="Left">
                                <cc1:LinearBarcode ID="IMEI_WInd" runat="server" SymbologyID="Code128" ShowText="True"
                                    ImageAutoDelete="True" BarHeightCM=".5" LeftMarginCM="0.000" TopMarginCM=".15"
                                    ImageResolution="300" DataToEncode="352480046354039" CheckCharacter="False" CheckCharacterInText="False"
                                    Height=".25in" ImageType="JPEG" Width="200px" XDimensionCM="0.0400" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <%--                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Label ID="Label23" runat="server" Text="" Font-Size="Small"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell HorizontalAlign="Right" Wrap="False">
                        <asp:Label ID="lblIMEI_Wind" runat="server" Text="" Font-Size="Small"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>--%>
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Left" VerticalAlign="Top">
                    SKU:
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="Left">
                                <cc1:LinearBarcode ID="SKUA_WInd" runat="server" SymbologyID="Code128" ShowText="True"
                                    ImageAutoDelete="True" BarHeightCM=".5" LeftMarginCM="0.000" TopMarginCM=".15"
                                    ImageResolution="300" DataToEncode="352480046354039" CheckCharacter="False" CheckCharacterInText="False"
                                    Height=".25in" ImageType="JPEG" Width="200px" XDimensionCM="0.0400" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <%--                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Label ID="Label24" runat="server" Text="" Font-Size="Small"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell HorizontalAlign="Right" Wrap="False">
                        <asp:Label ID="lblSKUA_Wind" runat="server" Text="" Font-Size="Small"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>--%>
                    </asp:Table>
                </asp:Panel>
                <asp:Panel runat="server" ID="FPL_APPLE" Style="padding-left: 80px; margin-left: 0">
                    <asp:Table ID="Table10" runat="server" CellPadding="0" CellSpacing="0" Width="85mm">
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Left">
                                <cc1:LinearBarcode ID="IMEI_APPLE" runat="server" SymbologyID="Code128" ShowText="True"
                                    ImageAutoDelete="True" BarHeightCM="1" LeftMarginCM="0.000" TopMarginCM=".15"
                                    ImageResolution="300" DataToEncode="352480046354039" CheckCharacter="False" CheckCharacterInText="False"
                                    Height=".25in" ImageType="JPEG" Width="200px" XDimensionCM="0.0400" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell Wrap="False">
                                <asp:Label ID="lblManMod_APPLE" runat="server" Text="" Font-Size="Large"></asp:Label>
                                <br />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell Wrap="False">
                                <asp:Label ID="lblCarrier_Apple" runat="server" Text="" Font-Size="Large"></asp:Label>
                                <br />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell Wrap="False">
                                <br />
                                <asp:Label ID="lblQuickCode_Apple" runat="server" Text="" Font-Size="Large" Font-Bold="True"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow ID="TableRow1" runat="server">
                            <asp:TableCell HorizontalAlign="Left" VerticalAlign="Top">
                                <asp:Label ID="lblCarrierLockCode_Apple" runat="server" Text="" Font-Size="Small"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow ID="TableRow2" runat="server">
                            <asp:TableCell HorizontalAlign="Left" VerticalAlign="Top">
                                <asp:Label ID="lblKittingDateCoded_Apple" runat="server" Text="" Font-Size="Small"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </asp:Panel>
                <asp:Panel runat="server" ID="FPL_01">
                    <asp:Table ID="Table6" runat="server" CellPadding="0" CellSpacing="0" Width="85mm">
                        <asp:TableRow>
                            <asp:TableCell Wrap="False">
                                <asp:Label ID="Label14" runat="server" Text="SKU:" Font-Size="Small"></asp:Label>
                                <asp:Label ID="lblSKU_01" runat="server" Text="72121" Font-Bold="True"></asp:Label>
                                <br />
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="Right" Wrap="False">
                                <asp:Label ID="Label15" runat="server" Text="Vendor Part#:" Font-Size="Small"></asp:Label>
                                <asp:Label ID="lblVendorPart_01" runat="server" Text="BMBS 72121" Font-Bold="True"></asp:Label>
                                <br />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label ID="Label16" runat="server" Text="Description:" Font-Size="Small"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="Right" Wrap="False">
                                <asp:Label ID="lblDescription_01" runat="server" Text="Device, RIM Bold 9700, Blk, REFB-UNLK"
                                    Font-Size="Small"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="2" HorizontalAlign="Center">
                                <asp:Label ID="Label17" runat="server" Text="____________________________________________"
                                    Font-Bold="True"></asp:Label>
                                <br />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Left" VerticalAlign="Top">
                    IMEI:
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="Left">
                                <cc1:LinearBarcode ID="IMEI_01" runat="server" SymbologyID="Code128" ShowText="True"
                                    ImageAutoDelete="True" BarHeightCM="1" LeftMarginCM="0.000" TopMarginCM=".15"
                                    ImageResolution="300" DataToEncode="352480046354039" CheckCharacter="False" CheckCharacterInText="False"
                                    Height=".25in" ImageType="JPEG" Width="200px" XDimensionCM="0.0400" />
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </asp:Panel>
                <asp:Panel runat="server" ID="FPL_02">
                    <asp:Table ID="Table2" runat="server" CellPadding="0" CellSpacing="0" Width="75mm">
                        <asp:TableRow>
                            <asp:TableCell Wrap="False" HorizontalAlign="Left">
                                <asp:Label ID="lblGoodAsNew" runat="server" Text="" Font-Bold="True"></asp:Label>
                                <br />
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="Right" Wrap="False">
                                <asp:Label ID="lblCondition_02" runat="server" Text="72121" Font-Bold="True"></asp:Label>
                                <asp:Label ID="lblDescription_02" runat="server" Text="BMBS 72121" Font-Bold="True"></asp:Label>
                                <br />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="2" HorizontalAlign="Center">
                                <asp:Label ID="Label3" runat="server" Text="____________________________________________"
                                    Font-Bold="True"></asp:Label>
                                <br />
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                    <asp:Table ID="Table7" runat="server" CellPadding="0" CellSpacing="0" Width="75mm">
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Left" VerticalAlign="Top">
                    IMEI:
                            </asp:TableCell>
                            <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" VerticalAlign="Top">
                                <cc1:LinearBarcode ID="IMEI_02" runat="server" SymbologyID="Code128" ShowText="True"
                                    ImageAutoDelete="True" BarHeightCM=".5" LeftMarginCM="0.000" TopMarginCM=".15"
                                    ImageResolution="300" DataToEncode="" CheckCharacter="False" CheckCharacterInText="False"
                                    Height=".25in" ImageType="JPEG" Width="200px" XDimensionCM="0.0400" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell Wrap="False" VerticalAlign="Top">
                                <asp:Label ID="Label7" runat="server" Text="SKU:"></asp:Label>
                                <asp:Label ID="lblSKU_02" runat="server" Text="" Font-Bold="True"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="Left" VerticalAlign="Top">
                                <cc1:LinearBarcode ID="bcSku_02" runat="server" SymbologyID="Code128" ShowText="True"
                                    ImageAutoDelete="True" BarHeightCM=".5" LeftMarginCM="0.000" TopMarginCM=".15"
                                    ImageResolution="300" DataToEncode="" CheckCharacter="False" CheckCharacterInText="False"
                                    Height=".25in" ImageType="JPEG" Width="200px" XDimensionCM="0.0400" />
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="Right" VerticalAlign="Top">
                                <asp:Label ID="lblGrade_02" runat="server" Text="Grade" Font-Bold="True"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell Wrap="False" VerticalAlign="Top">
                                <asp:Label ID="Label8" runat="server" Text="UPC:"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" VerticalAlign="Top">
                                <cc1:LinearBarcode ID="bcUPC_02" runat="server" SymbologyID="Code128" ShowText="True"
                                    ImageAutoDelete="True" BarHeightCM=".5" LeftMarginCM="0.000" TopMarginCM=".15"
                                    ImageResolution="300" DataToEncode="" CheckCharacter="False" CheckCharacterInText="False"
                                    Height=".25in" ImageType="JPEG" Width="200px" XDimensionCM="0.0400" />
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </asp:Panel>
                <asp:Panel runat="server" ID="FPL_03">
                    <asp:Table ID="Table1" runat="server" CellPadding="0" CellSpacing="0" Width="85mm">
                        <asp:TableRow>
                            <asp:TableCell Wrap="False" HorizontalAlign="Right">
                                <asp:Label ID="lblCondition_03" runat="server" Text="Condition" Font-Bold="True"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="Right" Wrap="False">
                                <asp:Label ID="lblManufacturer_03" runat="server" Text="Manufacturer" Font-Bold="True"></asp:Label>
                                <%--                        <asp:Label ID="lblModel_03" runat="server" Text="Model" Font-Bold="True" Width="100%"></asp:Label>
                        <asp:Label ID="lblColour_03" runat="server" Text="Colour" Font-Bold="True" Width="100%"></asp:Label>--%>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="2" HorizontalAlign="Center">
                                <asp:Label ID="Label2" runat="server" Text="____________________________________________"
                                    Font-Bold="True"></asp:Label>
                                <br />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Left" VerticalAlign="Top">
                    IMEI:
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="Left" ColumnSpan="1">
                                <cc1:LinearBarcode ID="IMEI_03" runat="server" SymbologyID="Code128" ShowText="True"
                                    ImageAutoDelete="True" BarHeightCM=".5" LeftMarginCM="0.000" TopMarginCM=".15"
                                    ImageResolution="300" DataToEncode="" CheckCharacter="False" CheckCharacterInText="False"
                                    Height=".25in" ImageType="JPEG" Width="200px" XDimensionCM="0.0400" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell Wrap="False">
                                <asp:Label ID="Label11" runat="server" Text="SKU:" Font-Size="Small"></asp:Label>
                                <asp:Label ID="lblSKU_03" runat="server" Text="" Font-Bold="True"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell Wrap="False" HorizontalAlign="Right">
                                <asp:Label ID="lblGrade_03" runat="server" Text="" Font-Bold="True"></asp:Label>
                            </asp:TableCell>
                            <%--                    <asp:TableCell Wrap="False" ColumnSpan="2" HorizontalAlign="Right">
                        <asp:Label ID="lblCondition_03" runat="server" Text="" Font-Bold="True"></asp:Label>
                    </asp:TableCell>--%>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Left" VerticalAlign="Top">
                                <asp:Label ID="lblHex_03" runat="server" Text="HEX:" Font-Size="Small"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="Left" ColumnSpan="1">
                                <cc1:LinearBarcode ID="HEX_03" runat="server" SymbologyID="Code128" ShowText="True"
                                    ImageAutoDelete="True" BarHeightCM=".5" LeftMarginCM="0.000" TopMarginCM=".15"
                                    ImageResolution="300" DataToEncode="" CheckCharacter="False" CheckCharacterInText="False"
                                    Height=".25in" ImageType="JPEG" Width="200px" XDimensionCM="0.0400" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Left" VerticalAlign="Top">
                                <asp:Label ID="lblUPC_03" runat="server" Text="UPC:" Font-Size="Small" Visible="false"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="Left" ColumnSpan="1">
                                <cc1:LinearBarcode ID="UPC_03" runat="server" SymbologyID="Code128" ShowText="True"
                                    ImageAutoDelete="True" BarHeightCM=".5" LeftMarginCM="0.000" TopMarginCM=".15"
                                    ImageResolution="300" DataToEncode="" CheckCharacter="False" CheckCharacterInText="False"
                                    Height=".25in" ImageType="JPEG" Width="200px" XDimensionCM="0.0400" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <%--                <asp:TableRow>
                    <asp:TableCell HorizontalAlign="Left" VerticalAlign="Top">
                        <br />
                        <br />
                        <br />
                        <asp:Label ID="lblCarrierLockCode_03" runat="server" Text="" Font-Size="Small"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>--%>
                    </asp:Table>
                </asp:Panel>

                <asp:Panel runat="server" ID="FPL_04">
                    <asp:Table ID="Table4" runat="server" CellPadding="0" CellSpacing="0" Width="85mm">
                        <asp:TableRow>
                            <asp:TableCell Wrap="False" HorizontalAlign="Center">
                                <asp:Label ID="lblManufacturer_04" runat="server" Text="Manufacturer" Font-Bold="True"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell Wrap="False" HorizontalAlign="Center">
                                <asp:Label ID="lblModel_04" runat="server" Text="Model" Font-Bold="True" Width="100%"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="Center">
                                <asp:Label ID="lblColour_04" runat="server" Text="Colour" Font-Bold="True" Width="100%"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <%--                <asp:TableRow>
                    <asp:TableCell ColumnSpan="3" HorizontalAlign="Center">
                        <asp:Label ID="lblNickName_04" runat="server" Text="" Font-Bold="True"></asp:Label>
                        <br />
                    </asp:TableCell>
                </asp:TableRow>--%>
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="3" HorizontalAlign="Center">
                                <asp:Label ID="Label4" runat="server" Text="____________________________________________"
                                    Font-Bold="True"></asp:Label>
                                <br />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Left" VerticalAlign="Top">
                    IMEI:
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="Left" ColumnSpan="1">
                                <cc1:LinearBarcode ID="IMEI_04" runat="server" SymbologyID="Code128" ShowText="True"
                                    ImageAutoDelete="True" BarHeightCM=".75" LeftMarginCM="0.000" TopMarginCM=".15"
                                    ImageResolution="300" DataToEncode="" CheckCharacter="False" CheckCharacterInText="False"
                                    Height=".25in" ImageType="JPEG" Width="200px" XDimensionCM="0.0400" />
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="Center">
                                <asp:Label ID="lblGrade_04" runat="server" Text="" Font-Bold="True" Width="100%"></asp:Label><br />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell Wrap="False" ColumnSpan="2">
                                <asp:Label ID="Label5" runat="server" Text="SKU:" Font-Size="Small"></asp:Label>
                                <asp:Label ID="lblSKU_04" runat="server" Text="" Font-Bold="True"></asp:Label>
                            </asp:TableCell>
                            <%--                    <asp:TableCell Wrap="False" ColumnSpan="2" HorizontalAlign="Right">
                        <asp:Label ID="lblCondition_03" runat="server" Text="" Font-Bold="True"></asp:Label>
                    </asp:TableCell>--%>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell Wrap="False" ColumnSpan="3">
                                <cc1:LinearBarcode ID="BC_SerialNumber_04" runat="server" SymbologyID="Code128" ShowText="True"
                                    ImageAutoDelete="True" BarHeightCM=".75" LeftMarginCM="0.000" TopMarginCM=".15"
                                    ImageResolution="300" DataToEncode="" CheckCharacter="False" CheckCharacterInText="False"
                                    Height=".25in" ImageType="JPEG" Width="200px" XDimensionCM="0.0400" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <%--                <asp:TableRow>
                    <asp:TableCell Wrap="False">            
                        <asp:Label ID="Label4" runat="server" Text="" Font-Size="Small"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell ColumnSpan="2">
                        <asp:Label ID="lblSerialNumber_04" runat="server" Text="(SN)" Font-Size="Small"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>--%>
                        <asp:TableRow>
                            <asp:TableCell Wrap="False">
                                <asp:Label ID="Label13" runat="server" Text="" Font-Size="Small"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell Wrap="False" ColumnSpan="2">
                                <asp:Label ID="lbl_WarrantyExpire_04" runat="server" Text="" Font-Bold="True"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow ID="rowCarrierLockCode_04" runat="server">
                            <asp:TableCell HorizontalAlign="Left" VerticalAlign="Top">
                                <br />
                                <br />
                                <br />
                                <asp:Label ID="lblCarrierLockCode_04" runat="server" Text="" Font-Size="Small"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </asp:Panel>

        <asp:Panel runat="server" ID="FPL_Staple" Height="98mm">
           <asp:Table ID="Table15" runat="server" CellPadding="0" CellSpacing="0" Width="85mm">
                <asp:TableRow>
                    <asp:TableCell Wrap="False" HorizontalAlign="Center">
                        <asp:Label ID="lblManufacturer_Staple" runat="server" Text="Manufacturer" Font-Bold="True"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell Wrap="False" HorizontalAlign="Center">
                        <asp:Label ID="lblModel_Staple" runat="server" Text="Model" Font-Bold="True" Width="100%"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell HorizontalAlign="Center">
                        <asp:Label ID="lblColour_Staple" runat="server" Text="Colour" Font-Bold="True" Width="100%"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="3" HorizontalAlign="Center">
                        <asp:Label ID="Label27" runat="server" Text="____________________________________________" Font-Bold="True"></asp:Label>
                        <br /> 
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell HorizontalAlign="Left" VerticalAlign="Top">
                    IMEI:
                    </asp:TableCell>
                    <asp:TableCell HorizontalAlign="Left" ColumnSpan="1">
                        <cc1:LinearBarcode ID="IMEI_Staple" runat="server" SymbologyID="Code128" ShowText="True"
                            ImageAutoDelete="True" BarHeightCM=".75" LeftMarginCM="0.000" TopMarginCM=".15"
                            ImageResolution="300" DataToEncode="" CheckCharacter="False" CheckCharacterInText="False"
                            Height=".25in" ImageType="JPEG" Width="200px" XDimensionCM="0.0400" />
                    </asp:TableCell>
                    <asp:TableCell HorizontalAlign="Center">
                       <asp:Label ID="lblGrade_Staple" runat="server" Text="" Font-Bold="True" Width="100%"></asp:Label><br />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Wrap="False" ColumnSpan="2">
                        <asp:Label ID="Label29" runat="server" Text="SKU:" Font-Size="Small"></asp:Label>
                        <asp:Label ID="lblSKU_Staple" runat="server" Text="" Font-Bold="True"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>



                <asp:TableRow>
                    <asp:TableCell Wrap="False" ColumnSpan="3" HorizontalAlign="Center">
                        <cc1:LinearBarcode ID="bcUPC_Staple" runat="server" SymbologyID="Code128" ShowText="True"
                            ImageAutoDelete="True" BarHeightCM=".75" LeftMarginCM="0.000" TopMarginCM=".15"
                            ImageResolution="300" DataToEncode="" CheckCharacter="False" CheckCharacterInText="False"
                            Height=".25in" ImageType="JPEG" Width="200px" XDimensionCM="0.0400" />
                    </asp:TableCell>
                </asp:TableRow>
<%--                <asp:TableRow>
                    <asp:TableCell Wrap="False">            
                        <asp:Label ID="Label31" runat="server" Text="" Font-Size="Small"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell Wrap="False" ColumnSpan="2">
                        <asp:Label ID="lbl_WarrantyExpire_Staple" runat="server" Text="" Font-Bold="True"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="rowCarrierLockCode_Staple" runat="server">
                    <asp:TableCell HorizontalAlign="Left" VerticalAlign="Top">
                        <br />
                        <br />
                        <br />
                        <asp:Label ID="lblCarrierLockCode_Staple" runat="server" Text="" Font-Size="Small"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>--%>

            </asp:Table>
        </asp:Panel>



                <asp:Panel runat="server" ID="FPL_05">
                    <asp:Table ID="Table11" runat="server" CellPadding="0" CellSpacing="0" Width="85mm">
                        <asp:TableRow>
                            <asp:TableCell Wrap="False" HorizontalAlign="Center">
                                <asp:Label ID="lblManufacturer_05" runat="server" Text="Manufacturer" Font-Bold="True"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell Wrap="False" HorizontalAlign="Center">
                                <asp:Label ID="lblModel_05" runat="server" Text="Model" Font-Bold="True" Width="100%"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="Center">
                                <asp:Label ID="lblColour_05" runat="server" Text="Colour" Font-Bold="True" Width="100%"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="3" HorizontalAlign="Center">
                                <asp:Label ID="Label21" runat="server" Text="____________________________________________"
                                    Font-Bold="True"></asp:Label>
                                <br />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Left" VerticalAlign="Top">
                    IMEI:
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="Left" ColumnSpan="1">
                                <cc1:LinearBarcode ID="IMEI_05" runat="server" SymbologyID="Code128" ShowText="True"
                                    ImageAutoDelete="True" BarHeightCM=".75" LeftMarginCM="0.000" TopMarginCM=".15"
                                    ImageResolution="300" DataToEncode="" CheckCharacter="False" CheckCharacterInText="False"
                                    Height=".25in" ImageType="JPEG" Width="200px" XDimensionCM="0.0400" />
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="Center">
                                <asp:Label ID="lblGrade_05" runat="server" Text="" Font-Bold="True" Width="100%"></asp:Label><br />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Left" VerticalAlign="Top" ColumnSpan="3">
                                <asp:Label ID="lblUnlockCode" runat="server" Text="" Font-Size="Small"></asp:Label>
                                <br />
                                <asp:Label ID="lblCarrierLockCode_05" runat="server" Text="" Font-Size="Small"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow ID="TableRow3" runat="server">
                            <asp:TableCell HorizontalAlign="Left" VerticalAlign="Top">
                                <asp:Label ID="lblKittingDateCoded_05" runat="server" Text="" Font-Size="Small"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </asp:Panel>
                <asp:Panel runat="server" ID="FPL_BB">
                    <asp:Table ID="Table5" runat="server" CellPadding="0" CellSpacing="0" Width="85mm">
                        <asp:TableRow>
                            <asp:TableCell Wrap="False" HorizontalAlign="Left" ColumnSpan="3">
                                <asp:Label ID="lblManufacturer_BB" runat="server" Text="Manufacturer" Font-Bold="True"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="3" HorizontalAlign="Center">
                                <asp:Label ID="Label12" runat="server" Text="____________________________________________"
                                    Font-Bold="True"></asp:Label>
                                <br />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Left" VerticalAlign="Top">
                    IMEI:
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="Left" ColumnSpan="1">
                                <cc1:LinearBarcode ID="IMEI_BB" runat="server" SymbologyID="Code128" ShowText="True"
                                    ImageAutoDelete="True" BarHeightCM=".75" LeftMarginCM="0.000" TopMarginCM=".15"
                                    ImageResolution="300" DataToEncode="" CheckCharacter="False" CheckCharacterInText="False"
                                    Height=".25in" ImageType="JPEG" Width="200px" XDimensionCM="0.0400" />
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="Center">
                                <asp:Label ID="lblGrade_BB" runat="server" Text="" Font-Bold="True" Width="100%"></asp:Label><br />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Left" VerticalAlign="Top">
                                <br />
                                <asp:Label ID="lblsku_BB" runat="server" Text="" Font-Size="Small"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Left" VerticalAlign="Top">
                                <br />
                                <asp:Label ID="lblID_BB" runat="server" Text="" Font-Size="Small"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </asp:Panel>
                <asp:Panel runat="server" ID="FPL_06">
                    <asp:Table ID="Table8" runat="server" CellPadding="0" CellSpacing="0" Width="75mm">
                        <asp:TableRow>
                            <asp:TableCell Wrap="False" HorizontalAlign="Left">
                        <br />
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="Right" Wrap="False">
                                <asp:Label ID="lblCondition_06" runat="server" Text="72121" Font-Bold="True"></asp:Label>
                                <asp:Label ID="lblDescription_06" runat="server" Text="BMBS 72121" Font-Bold="True"></asp:Label>
                                <br />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="2" HorizontalAlign="Center">
                                <asp:Label ID="Label1" runat="server" Text="____________________________________________"
                                    Font-Bold="True"></asp:Label>
                                <br />
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                    <asp:Table ID="Table9" runat="server" CellPadding="0" CellSpacing="0" Width="75mm">
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Left" VerticalAlign="Top">
                    IMEI:
                            </asp:TableCell>
                            <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" VerticalAlign="Top">
                                <cc1:LinearBarcode ID="IMEI_06" runat="server" SymbologyID="Code128" ShowText="True"
                                    ImageAutoDelete="True" BarHeightCM=".5" LeftMarginCM="0.000" TopMarginCM=".15"
                                    ImageResolution="300" DataToEncode="" CheckCharacter="False" CheckCharacterInText="False"
                                    Height=".25in" ImageType="JPEG" Width="200px" XDimensionCM="0.0400" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell Wrap="False" VerticalAlign="Top">
                                <asp:Label ID="Label18" runat="server" Text="SKU:"></asp:Label>
                                <asp:Label ID="lblSKU_06" runat="server" Text="" Font-Bold="True"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="Left" VerticalAlign="Top">
                                <cc1:LinearBarcode ID="bcSku_06" runat="server" SymbologyID="Code128" ShowText="True"
                                    ImageAutoDelete="True" BarHeightCM=".5" LeftMarginCM="0.000" TopMarginCM=".15"
                                    ImageResolution="300" DataToEncode="" CheckCharacter="False" CheckCharacterInText="False"
                                    Height=".25in" ImageType="JPEG" Width="200px" XDimensionCM="0.0400" />
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="Right" VerticalAlign="Top">
                                <asp:Label ID="lblGrade_06" runat="server" Text="Grade" Font-Bold="True"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell Wrap="False" VerticalAlign="Top">
                                <asp:Label ID="Label19" runat="server" Text="UPC:"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" VerticalAlign="Top">
                                <cc1:LinearBarcode ID="bcUPC_06" runat="server" SymbologyID="Code128" ShowText="True"
                                    ImageAutoDelete="True" BarHeightCM=".5" LeftMarginCM="0.000" TopMarginCM=".15"
                                    ImageResolution="300" DataToEncode="" CheckCharacter="False" CheckCharacterInText="False"
                                    Height=".25in" ImageType="JPEG" Width="200px" XDimensionCM="0.0400" />
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </asp:Panel>
                <asp:Panel runat="server" ID="FPL_07">
                    <asp:Table ID="Table3" runat="server" CellPadding="0" CellSpacing="0" Width="75mm">
                        <asp:TableRow>
                            <asp:TableCell Wrap="False" HorizontalAlign="Left">
                        <br />
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="Right" Wrap="False">
                                <asp:Label ID="lblCondition_07" runat="server" Text="72121" Font-Bold="True"></asp:Label>
                                <asp:Label ID="lblDescription_07" runat="server" Text="BMBS 72121" Font-Bold="True"></asp:Label>
                                <br />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="2" HorizontalAlign="Center">
                                <asp:Label ID="Label6" runat="server" Text="____________________________________________"
                                    Font-Bold="True"></asp:Label>
                                <br />
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                    <asp:Table ID="Table13" runat="server" CellPadding="0" CellSpacing="0" Width="75mm">
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Left" VerticalAlign="Top">
                    IMEI:
                            </asp:TableCell>
                            <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" VerticalAlign="Top">
                                <cc1:LinearBarcode ID="IMEI_07" runat="server" SymbologyID="Code128" ShowText="True"
                                    ImageAutoDelete="True" BarHeightCM=".5" LeftMarginCM="0.000" TopMarginCM=".15"
                                    ImageResolution="300" DataToEncode="" CheckCharacter="False" CheckCharacterInText="False"
                                    Height=".25in" ImageType="JPEG" Width="200px" XDimensionCM="0.0400" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell Wrap="False" VerticalAlign="Top">
                                <asp:Label ID="Label9" runat="server" Text="SKU:"></asp:Label>
                                <asp:Label ID="lblSKU_07" runat="server" Text="" Font-Bold="True"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="Left" VerticalAlign="Top">
                                <cc1:LinearBarcode ID="bcSku_07" runat="server" SymbologyID="Code128" ShowText="True"
                                    ImageAutoDelete="True" BarHeightCM=".5" LeftMarginCM="0.000" TopMarginCM=".15"
                                    ImageResolution="300" DataToEncode="" CheckCharacter="False" CheckCharacterInText="False"
                                    Height=".25in" ImageType="JPEG" Width="200px" XDimensionCM="0.0400" />
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="Right" VerticalAlign="Top">
                                <asp:Label ID="lblGrade_07" runat="server" Text="Grade" Font-Bold="True"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell Wrap="False" VerticalAlign="Top">
                                <asp:Label ID="Label10" runat="server" Text="UPC:"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" VerticalAlign="Top">
                                <cc1:LinearBarcode ID="bcUPC_07" runat="server" SymbologyID="Code128" ShowText="True"
                                    ImageAutoDelete="True" BarHeightCM=".5" LeftMarginCM="0.000" TopMarginCM=".15"
                                    ImageResolution="300" DataToEncode="" CheckCharacter="False" CheckCharacterInText="False"
                                    Height=".25in" ImageType="JPEG" Width="200px" XDimensionCM="0.0400" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell Wrap="False">
                                <asp:Label ID="Label20" runat="server" Text="" Font-Size="Small"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell Wrap="False" ColumnSpan="2">
                                <asp:Label ID="lbl_WarrantyExpire_07" runat="server" Text="" Font-Bold="True"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </asp:Panel>
                <asp:Panel ID="P_OTHER" runat="server" Visible="false">
                    <div id="P_OTHERDIV" runat="server">
                    </div>
                </asp:Panel>
                <br style="page-break-after: always;">
            </ItemTemplate>
        </asp:Repeater>
    </div>
    </form>
</body>

<script type="text/javascript">

    //    function PrintReportView() {
    ////             $find('= rvBagTag.ClientID ').invokePrintDialog();
    //    }

    function printIt() {
        window.print();
        window.close();
        //        var StopAutoExit = $get("< %= StopAutoExit.ClientID %>").value
        //        if (StopAutoExit != "Y") {
        //            window.print();
        //            window.close();
        //        }
    }
</script>

</html>


