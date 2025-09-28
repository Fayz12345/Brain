<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RPT_ProjectScanCodeList.aspx.cs" Inherits="BW_WebApp.Reports.RPT_ProjectScanCodeList" %>

<%@ Register Assembly="IDAutomation.LinearServerControl" Namespace="IDAutomation.LinearServerControl"
    TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
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
                         ImageResolution="300" DataToEncode="" CheckCharacter="False"
                         CheckCharacterInText="False" Height=".75in" ImageType="JPEG" 
                         Width="200px" XDimensionCM="0.0400" />
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
                         ImageResolution="300" DataToEncode="" CheckCharacter="False"
                         CheckCharacterInText="False" Height=".75in" ImageType="JPEG" 
                         Width="200px" XDimensionCM="0.0400" />
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
                         ImageResolution="300" DataToEncode="" CheckCharacter="False"
                         CheckCharacterInText="False" Height=".75in" ImageType="JPEG" 
                         Width="200px" XDimensionCM="0.0400" />
                    <br />
                    <br />
                    <br />
                    </asp:Panel>
                </td>
            </tr>
        </table>

        <asp:Repeater ID="rQuestion" runat="server">
            <ItemTemplate>
                <asp:Panel runat="server" ID="valueHeader">
                    <asp:Label ID="lQuestion" runat="server" Text="ValueField" Font-Size="Larger" Font-Underline="True"></asp:Label>
                    <br />
                </asp:Panel>
                <asp:DataList ID="dList" runat="server">
                <ItemTemplate>
                <asp:Panel runat="server" ID="valueHeader" Width="2.5in">
                    <asp:Label ID="lOption" runat="server" Text="ValueField" Font-Size="Small" ></asp:Label><br />
                        <cc1:LinearBarcode ID="bcOptionScanCode" runat="server" SymbologyID="Code128" ShowText="False"
                         ImageAutoDelete="True" BarHeightCM=".5" LeftMarginCM="0.000" TopMarginCM=".15"
                         ImageResolution="300" DataToEncode="" CheckCharacter="False"
                         CheckCharacterInText="False" Height=".75in" ImageType="JPEG" 
                         Width="200px" XDimensionCM="0.0400" />

                </asp:Panel>

                </ItemTemplate>
                </asp:DataList>
                <br />
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

