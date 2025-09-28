<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BinLabel.aspx.cs" Inherits="BW_WebApp.BinLabel" %>

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


        <asp:Repeater ID="rLabels" runat="server">
            <ItemTemplate>
                <asp:Table ID="Table1" runat="server" CellPadding="0" CellSpacing="0" Width="62mm" Font-Size="Large">
                    <asp:TableRow runat="server" ID="TableRow1">
                        <asp:TableCell HorizontalAlign="Left" ColumnSpan="2">
                            <asp:Label ID="lblClient" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Client") %>'></asp:Label>
                            <br />
                        </asp:TableCell>
                    </asp:TableRow>



                    <asp:TableRow runat="server" ID="TableRow3">
                        <asp:TableCell HorizontalAlign="Left" ColumnSpan="2">
                            <asp:Label ID="Label2" runat="server" Text="Bin Number" Font-Size="X-Large"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow runat="server" ID="TableRow4">
                        <asp:TableCell ColumnSpan="2" HorizontalAlign="Left">
                            <cc1:LinearBarcode ID="bcBinNumber" runat="server" SymbologyID="Code128" ShowText="False"
                                ImageAutoDelete="True" BarHeightCM=".75" LeftMarginCM="0.000" TopMarginCM=".15"
                                ImageResolution="300" DataToEncode='<%# DataBinder.Eval(Container.DataItem, "BinNumber") %>'
                                CheckCharacter="False" CheckCharacterInText="False" Height="15px" ImageType="JPEG"
                                Width="200px" XDimensionCM="0.0400" Visible="True" /><br />
                            <asp:Label ID="lblBinNumber" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "BinNumber") %>'
                                Font-Size="X-Large"></asp:Label>


                        </asp:TableCell>
                    </asp:TableRow>


                    <asp:TableRow runat="server" ID="TableRow2">
                        <asp:TableCell HorizontalAlign="Left" ColumnSpan="2">
                            <asp:Label ID="Label3" runat="server" Text="Scan Bin" Font-Size="X-Large"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow runat="server" ID="SerialNumberBar">
                        <asp:TableCell ColumnSpan="2" HorizontalAlign="Left">
                            <cc1:LinearBarcode ID="bcBINID" runat="server" SymbologyID="Code128" ShowText="False"
                                ImageAutoDelete="True" BarHeightCM=".75" LeftMarginCM="0.000" TopMarginCM=".15"
                                ImageResolution="300" DataToEncode='<%# DataBinder.Eval(Container.DataItem, "BN") %>'
                                CheckCharacter="False" CheckCharacterInText="False" Height="15px" ImageType="JPEG"
                                Width="200px" XDimensionCM="0.0400" Visible="True" /><br />

                        </asp:TableCell>
                    </asp:TableRow>


                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="2" HorizontalAlign="Left">
                            <asp:Label ID="Label1" runat="server" Text="PROCESS SCAN" Font-Size="X-Large"></asp:Label><br />
                            <cc1:LinearBarcode ID="bcProcessScan" runat="server" SymbologyID="Code128" ShowText="False"
                                ImageAutoDelete="True" BarHeightCM=".75" LeftMarginCM="0.000" TopMarginCM=".15"
                                ImageResolution="300" DataToEncode='<%# DataBinder.Eval(Container.DataItem, "XBIBX") %>'
                                CheckCharacter="False" CheckCharacterInText="False" Height=".75in" ImageType="JPEG"
                                Width="200px" XDimensionCM="0.0400" Visible="True" />
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                <br style="page-break-after:always;">
            </ItemTemplate>
        </asp:Repeater>



    </div>
    </form>
</body>

<script type="text/javascript">

    function printIt() {
        window.print();
        window.close();
    }
</script>




</html>
