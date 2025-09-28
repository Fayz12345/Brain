<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RPT_MasterCarrierScanCodeList.aspx.cs" Inherits="BW_WebApp.Reports.RPT_MasterCarrierScanCodeList" %>


<%@ Register Assembly="IDAutomation.LinearServerControl" Namespace="IDAutomation.LinearServerControl"
    TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        P.breakhere
        {
            page-break-after: always;
        }
    </style>
</head>


<body onload="printIt();" style="padding-left:10px; margin-left:20px">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
        <asp:Label ID="lProject" runat="server" Text="" Font-Size="X-Large"></asp:Label>
        <br />
        <br />
        <table runat="server" id="tblHeader">
            <tr>
                <td>
                    <asp:Panel runat="server" ID="pSave" HorizontalAlign="Center" Width="2.5in">
                        <asp:Label ID="lOption" runat="server" Text="Save" Font-Size="Small"></asp:Label><br />
                        <cc1:LinearBarcode ID="bcOptionScanCode" runat="server" SymbologyID="Code128" ShowText="False"
                            ImageAutoDelete="True" BarHeightCM=".5" LeftMarginCM="0.000" TopMarginCM=".15"
                            ImageResolution="300" DataToEncode="" CheckCharacter="False" CheckCharacterInText="False"
                            Height=".75in" ImageType="JPEG" Width="200px" XDimensionCM="0.0400" />
                        <br />
                        <br />
                        <br />
                    </asp:Panel>
                </td>
                <td>
                    <asp:Panel runat="server" ID="pBagTag" HorizontalAlign="Center" Width="2.5in">
                        <asp:Label ID="Label1" runat="server" Text="Bagtag" Font-Size="Small"></asp:Label><br />
                        <cc1:LinearBarcode ID="LinearBarcode1" runat="server" SymbologyID="Code128" ShowText="False"
                            ImageAutoDelete="True" BarHeightCM=".5" LeftMarginCM="0.000" TopMarginCM=".15"
                            ImageResolution="300" DataToEncode="" CheckCharacter="False" CheckCharacterInText="False"
                            Height=".75in" ImageType="JPEG" Width="200px" XDimensionCM="0.0400" />
                        <br />
                        <br />
                        <br />
                    </asp:Panel>
                </td>
                <td>
                    <asp:Panel runat="server" ID="pClear" HorizontalAlign="Center" Width="2.5in">
                        <asp:Label ID="Label2" runat="server" Text="Clear" Font-Size="Small"></asp:Label><br />
                        <cc1:LinearBarcode ID="LinearBarcode2" runat="server" SymbologyID="Code128" ShowText="False"
                            ImageAutoDelete="True" BarHeightCM=".5" LeftMarginCM="0.000" TopMarginCM=".15"
                            ImageResolution="300" DataToEncode="" CheckCharacter="False" CheckCharacterInText="False"
                            Height=".75in" ImageType="JPEG" Width="200px" XDimensionCM="0.0400" />
                        <br />
                        <br />
                        <br />
                    </asp:Panel>
                </td>
            </tr>
        </table>
         <br />
        <asp:Repeater ID="rCarrier" runat="server">
            <ItemTemplate>
                <asp:Panel runat="server" ID="valueHeader" Width="2.5in">
                    <asp:Label ID="lCarrier" runat="server" Text="ValueField" Font-Size="Small"></asp:Label><br />
                    <cc1:LinearBarcode ID="bcCarrier" runat="server" SymbologyID="Code128" ShowText="False"
                        ImageAutoDelete="True" BarHeightCM=".5" LeftMarginCM="0.000" TopMarginCM=".15"
                        ImageResolution="300" DataToEncode="" CheckCharacter="False" CheckCharacterInText="False"
                        Height=".75in" ImageType="JPEG" Width="200px" XDimensionCM="0.0400" />
                    <br />
                </asp:Panel>
                <br />
                <asp:DataList ID="dlCarrier" runat="server">
                    <ItemTemplate>
                        <table width="100%">
                        <tr>
                        <td colspan="2" style="border-style: solid none none none; border-top-width: thin;" width="100%">


                        </td>
                        </tr>
                            <tr>
                                <td width="1in" align="left" valign="top">
                                    <asp:Panel runat="server" ID="valueHeader" Width=".80in" Height="2.5in">
                                        <asp:Label ID="lOption" runat="server" Text="ValueField" Font-Size="Small"></asp:Label><br /><br /><br /><br />
                                        <cc1:LinearBarcode ID="bcOptionScanCode" runat="server" SymbologyID="Code128" ShowText="True"
                                            ImageAutoDelete="True" BarHeightCM=".5" LeftMarginCM="0.000" TopMarginCM=".15"
                                            ImageResolution="300" DataToEncode="" CheckCharacter="False" CheckCharacterInText="False"
                                            Height=".75in" ImageType="JPEG" Width="200px" XDimensionCM="0.0400" RotationAngle="Two_Hundred_Seventy_Degrees" />
                                    </asp:Panel>
                                </td>
                                <td align="left" valign="top">
                                    <asp:DataList ID="dlManufacturer" runat="server">
                                        <ItemTemplate>
                                            <asp:Panel runat="server" ID="valueHeader" Width="2.5in">
                                                <asp:Label ID="lModel" runat="server" Text="ValueField" Font-Size="Small"></asp:Label><br />
                                                <cc1:LinearBarcode ID="bcModelOptionScanCode" runat="server" SymbologyID="Code128"
                                                    ShowText="False" ImageAutoDelete="True" BarHeightCM=".5" LeftMarginCM="0.000"
                                                    TopMarginCM=".15" ImageResolution="300" DataToEncode="" CheckCharacter="False"
                                                    CheckCharacterInText="False" Height=".75in" ImageType="JPEG" Width="200px" XDimensionCM="0.0400" />
                                            </asp:Panel>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:DataList>
                <P CLASS="breakhere">
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
    }
</script>
</html>

