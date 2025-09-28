<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RPT_Submission_01.aspx.cs" Inherits="BW_WebApp.RPT_Submission_01" %>

<%@ Register Assembly="IDAutomation.LinearServerControl" Namespace="IDAutomation.LinearServerControl" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>


<body onload="printIt();" style="width: 8in">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>


    <asp:Table ID="Table1x" runat="server" Width="100%">
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Center" VerticalAlign="Top" ColumnSpan="3">
                <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Logo.jpg"></asp:Image>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Center" VerticalAlign="Top" ColumnSpan="3">
            <h1>
                Submission Form
            </h1>
            <br />
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Right" VerticalAlign="Top">
             To:
            </asp:TableCell>
            <asp:TableCell HorizontalAlign="Left" VerticalAlign="Top">
                        <asp:Label ID="lblcCompanyName" runat="server" Text="Company Name"></asp:Label>
                        <asp:Label ID="lblcAddressLines" runat="server" Text="Address"></asp:Label><br />
                        <asp:Label ID="lblcCityProvincePostal" runat="server" Text="City Province Postal"></asp:Label><br />

            </asp:TableCell>
            <asp:TableCell HorizontalAlign="Right" VerticalAlign="Top">
                Service Request #:
                <asp:Label ID="lblG10" runat="server" Text=""></asp:Label><br />
                Warranty Type:
                <asp:Label ID="lblG11" runat="server" Text=""></asp:Label><br />
                SubmissionDate:
                <asp:Label ID="lblG12" runat="server" Text="G12"></asp:Label><br />
<%--                <cc1:LinearBarcode ID="bcProjectTag" runat="server" SymbologyID="Code128" ShowText="True"
                    ImageAutoDelete="True" BarHeightCM=".5" LeftMarginCM="0.000" TopMarginCM=".15"
                    ImageResolution="300" DataToEncode="" CheckCharacter="False" CheckCharacterInText="False"
                    Height="10px" ImageType="JPEG" Width="200px" XDimensionCM="0.0400" /><br />
                <br />
                <cc1:LinearBarcode ID="brRMA" runat="server" SymbologyID="Code128" ShowText="True"
                    ImageAutoDelete="True" BarHeightCM=".5" LeftMarginCM="0.000" TopMarginCM=".15"
                    ImageResolution="300" DataToEncode="" CheckCharacter="False" CheckCharacterInText="False"
                    Height="15px" ImageType="JPEG" Width="200px" XDimensionCM="0.0400" /><br />--%>
            </asp:TableCell>
        </asp:TableRow>




        <asp:TableRow>
            <asp:TableCell ColumnSpan="3">
                     <asp:Table ID="Table1" runat="server" Width="100%" BorderStyle="Solid" BorderWidth="2">
                     <asp:TableRow BackColor="#CCCCCC">
                     <asp:TableCell ColumnSpan="3" Font-Bold="True">
                     Dealer Information

                     </asp:TableCell>
                     </asp:TableRow>
                     <asp:TableRow>
                     <asp:TableCell ColumnSpan="2">Dealer Name:
                        <asp:Label ID="lblDealerName" runat="server" Text="DealerName"></asp:Label>
                        </asp:TableCell>
                     <asp:TableCell HorizontalAlign="Right">Dealer ID:
                        <asp:Label ID="lblDealerID" runat="server" Text="ID"></asp:Label>
                        </asp:TableCell>
                     </asp:TableRow>

                     <asp:TableRow>
                     <asp:TableCell ColumnSpan="2">Phone #:
                        <asp:Label ID="lblPhone" runat="server" Text="PHone"></asp:Label>
                        </asp:TableCell>
                     <asp:TableCell HorizontalAlign="Right">Fax #:
                        <asp:Label ID="lblFax" runat="server" Text="Fax"></asp:Label>
                        </asp:TableCell>
                     </asp:TableRow>

                     <asp:TableRow>
                     <asp:TableCell ColumnSpan="3">Address:
                        <asp:Label ID="lblAddress" runat="server" Text="Address"></asp:Label>
                        </asp:TableCell>
                     </asp:TableRow>

                     <asp:TableRow>
                     <asp:TableCell>City:
                        <asp:Label ID="lblCity" runat="server" Text="City"></asp:Label>
                        </asp:TableCell>
                     <asp:TableCell>Province:
                        <asp:Label ID="lblProvince" runat="server" Text="Province"></asp:Label>
                        </asp:TableCell>
                     <asp:TableCell HorizontalAlign="Right">Postal Code:
                        <asp:Label ID="lblPostalCode" runat="server" Text="Postal Code"></asp:Label>
                        </asp:TableCell>
                     </asp:TableRow>
                     </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell ColumnSpan="3">
                <asp:Table ID="Table2" runat="server" Width="100%" BorderStyle="Solid" BorderWidth="2">
                    <asp:TableRow BackColor="#CCCCCC">
                        <asp:TableCell ColumnSpan="3" Font-Bold="True">
<br />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="Left">
                            <asp:Label ID="lblB20" runat="server" Text=""></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell HorizontalAlign="Center">
                            <asp:Label ID="lblD20" runat="server" Text=""></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell HorizontalAlign="Right">
                            <asp:Label ID="lblG20" runat="server" Text=""></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="Left" ColumnSpan="1" VerticalAlign="Top">
                            IMEI:
                            <asp:Label ID="lblB34" runat="server" Text=""></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell HorizontalAlign="Center" ColumnSpan="2" VerticalAlign="Top">
                            <cc1:LinearBarcode ID="IMEI_04" runat="server" SymbologyID="Code128" ShowText="True"
                                ImageAutoDelete="True" BarHeightCM=".75" LeftMarginCM="0.000" TopMarginCM=".15"
                                ImageResolution="300" DataToEncode="" CheckCharacter="False" CheckCharacterInText="False"
                                Height="15px" ImageType="JPEG" Width="200px" XDimensionCM="0.0400" />
                            <br />
                            <br />

                        </asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="Left" ColumnSpan="3">
                            <br />
                            Manufacturer:
                            <asp:Label ID="lblB22" runat="server" Text=""></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="Left" ColumnSpan="3">
                            Model:
                            <asp:Label ID="lblB23" runat="server" Text=""></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="Left" ColumnSpan="3">
                            Colour:
                            <asp:Label ID="lblB24" runat="server" Text=""></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="Left" ColumnSpan="3">
                            Carrier:
                            <asp:Label ID="lblB25" runat="server" Text=""></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="Left" ColumnSpan="3">
                            Store Comments:
                            <asp:Label ID="lblB27" runat="server" Text=""></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>


                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="Left" ColumnSpan="3">
                            Fault:
                            <asp:Label ID="lblFault" runat="server" Text=""></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="Left" ColumnSpan="3">
                            Fault2:
                            <asp:Label ID="lblFault2" runat="server" Text=""></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="Left" ColumnSpan="3">
                            Customer Name:
                            <asp:Label ID="lblCustomerName" runat="server" Text=""></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="Left" ColumnSpan="3">
                            Original IMEI:
                            <asp:Label ID="lblActivationDate" runat="server" Text=""></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="Left" ColumnSpan="3">
                            Customer Notes:
                            <asp:Label ID="lblB36" runat="server" Text=""></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="Left" ColumnSpan="3">
                            Dealer Waybill:
                            <asp:Label ID="lblB38" runat="server" Text=""></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>

   </asp:Table>
    </form>
</body>


<script type="text/javascript">
    function printIt() {
        window.print();
        window.close();
    }
</script>
</html>


