<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BagTag_Client.aspx.cs" Inherits="BW_WebApp.BagTag_Client" %>

<%@ Register Assembly="IDAutomation.LinearServerControl" Namespace="IDAutomation.LinearServerControl" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">  
<head id="Head1" runat="server">
    <title></title>
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="page">
        <asp:Table ID="Table1" runat="server" Width="100%" Height="266px">
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Right" ColumnSpan="2">
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Logo.jpg" />

                </asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">
                    <cc1:LinearBarcode ID="bcBagtag" runat="server" SymbologyID="Code128" ShowText="True"
                        ImageAutoDelete="True" BarHeightCM="1" LeftMarginCM="0.000" TopMarginCM=".15"
                        ImageResolution="300" DataToEncode="" CheckCharacter="False" CheckCharacterInText="False"
                        Height="30px" ImageType="JPEG" Width="200px" XDimensionCM="0.0400" />
                </asp:TableCell>
            </asp:TableRow>


            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Center" ColumnSpan="3">
                    <asp:Table ID="Table2" runat="server" Width="100%">
                    <asp:TableRow>
                    <asp:TableCell HorizontalAlign="Center" ColumnSpan="5">
                      <br />
                    <h1>
                        Submission Form</h1>                  
                    </asp:TableCell>
                    </asp:TableRow>



                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Right" Width="20%">
                Ship To:
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="Left" Width="20%">

                             <asp:Label ID="lblCompanyName" runat="server" Text=""></asp:Label>


                
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="Right" Width="20%">
                Repair Work Order: 
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="Left" Width="20%">
                x
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="Center" Width="20%">
                x
                            </asp:TableCell>
                        </asp:TableRow>






                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Right">
                x
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="Left">
                             <asp:Label ID="lblAddressLines" runat="server" Text="1185 Corporate Dr., Unit 1"></asp:Label>

                
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="Right">
                RMA Number :
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="Left">
                x
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="Center">
                x
                            </asp:TableCell>
                        </asp:TableRow>




                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Right">
                x
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="Left">
                            <asp:Label ID="lblCityProvincePostal" runat="server" Text="Burlington, Ontario, L7L 5V5"></asp:Label>

                
                
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="Right">
                Submission Date: 
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="Left">
                x
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="Center">
                x
                            </asp:TableCell>


                        </asp:TableRow>
                    </asp:Table>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </div>
    </form>
</body>
</html>
