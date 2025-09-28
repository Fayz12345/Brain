<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RPT_Authorize_01.aspx.cs" Inherits="BW_WebApp.RPT_Authorize_01" %>

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


    <asp:Table ID="Table1x" runat="server">
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Left" VerticalAlign="Top">
                <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Logo.jpg"></asp:Image>
                <br />
                <cc1:LinearBarcode ID="bcBagtag" runat="server" SymbologyID="Code128" ShowText="True"
                    ImageAutoDelete="True" BarHeightCM="1" LeftMarginCM="0.000" TopMarginCM=".15"
                    ImageResolution="300" DataToEncode="12345678910abc1" CheckCharacter="False" CheckCharacterInText="False"
                    Height="30px" ImageType="JPEG" Width="200px" XDimensionCM="0.0400" />
            </asp:TableCell>
            <asp:TableCell HorizontalAlign="Right">
        <p style="font-size: x-large">
        <asp:Label ID="lblcCompanyName" runat="server" Text="Company Name"></asp:Label>
        </p>
        <h2>
        
        <asp:Label ID="lblcAddressLines" runat="server" Text="Address"></asp:Label><br />
        <asp:Label ID="lblcCityProvincePostal" runat="server" Text="Address,Province,Postal"></asp:Label><br />
        <asp:Label ID="lblcPhone" runat="server" Text="Phone #: (   ) 999 9999"></asp:Label><br />
        <asp:Label ID="lblcFax" runat="server" Text="  Fax #: (   ) 999 9999"></asp:Label><br />
        <asp:Label ID="lblcWebsite" runat="server" Text="www.CompanyWebSite.com"></asp:Label><br />




        </h2>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell ColumnSpan="2">
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
            <asp:TableCell ColumnSpan="2">
                <asp:Table ID="Table2" runat="server" Width="100%" BorderStyle="Solid" BorderWidth="2">
                    <asp:TableRow BackColor="#CCCCCC">
                        <asp:TableCell ColumnSpan="2" Font-Bold="True">
                     Device Information
                        </asp:TableCell>

                        <asp:TableCell HorizontalAlign="Right">BIN #:
                        <asp:Label ID="lblBin" runat="server" Text="BIN"></asp:Label>
                        </asp:TableCell>

                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>ESN/IMEI #:
                        <asp:Label ID="lblESN" runat="server" Text="ESNIMEI"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>MSN #:
                        <asp:Label ID="lblMSN" runat="server" Text="MSN"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell HorizontalAlign="Right">RMA #:
                        <asp:Label ID="lblRMA" runat="server" Text="RMA"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>Manufacturer:
                        <asp:Label ID="ldlManufacturer" runat="server" Text="Manufacturer"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>Model #:
                        <asp:Label ID="lblModel" runat="server" Text="Model"></asp:Label>
                        </asp:TableCell>

                        <asp:TableCell HorizontalAlign="Right">Model Number:
                        <asp:Label ID="lblNickname" runat="server" Text="NickName"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                     <asp:CheckBoxList ID="chkInWarranty" runat="server" RepeatDirection="Horizontal" TextAlign="Left">
                     <asp:ListItem Text="In Warranty: Yes" Value="Yes"></asp:ListItem>
                     <asp:ListItem Text="No" Value="No"></asp:ListItem>                     
                     </asp:CheckBoxList>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:CheckBoxList ID="chkExtendedWarranty" runat="server" RepeatDirection="Horizontal"
                                TextAlign="Left">
                                <asp:ListItem Text="Extended Warranty: Yes" Value="Yes"></asp:ListItem>
                                <asp:ListItem Text="No" Value="No"></asp:ListItem>
                            </asp:CheckBoxList>
                        </asp:TableCell>
                        <asp:TableCell HorizontalAlign="Right">
                            <asp:CheckBoxList ID="chkOutOfWarranty" runat="server" RepeatDirection="Horizontal"
                                TextAlign="Left">
                                <asp:ListItem Text="Out of Warranty: Yes" Value="Yes"></asp:ListItem>
                                <asp:ListItem Text="No" Value="No"></asp:ListItem>
                            </asp:CheckBoxList>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell ColumnSpan="2">
                <asp:Table ID="Table3" runat="server" Width="100%" BorderStyle="Solid" BorderWidth="2">
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="2" Font-Bold="True">
                     CUSTOMER COMPLAINT:
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="2">
                            Fault Code:
                            <asp:Label ID="lblFaultCodes" runat="server" Text="FaultCodes"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell RowSpan="4" HorizontalAlign="Left" VerticalAlign="Top">
                        Notes:
                        <asp:Label ID="lblNote" runat="server" Text="Note" Font-Size="Small"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell HorizontalAlign="Right" Wrap="False">
                            Estimate Fee: $
                            <asp:Label ID="lblEstimateFee" runat="server" Text="E Fee"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
<%--                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="Right" Wrap="False">
                            Freight Fee: $
                            <asp:Label ID="lblFreightFee" runat="server" Text="F Fee"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="Right" Wrap="False">
                            Hst: $
                            <asp:Label ID="lblHST" runat="server" Text="Hst"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="Right" Wrap="False">
                            Total: $
                            <asp:Label ID="lblTotal" runat="server" Text="Total"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>--%>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>


        <asp:TableRow>
            <asp:TableCell ColumnSpan="2">
                <asp:Table ID="Table4" runat="server" Width="100%" BorderStyle="Solid" BorderWidth="2">
                    <asp:TableRow BackColor="#CCCCCC">
                        <asp:TableCell ColumnSpan="3" Font-Bold="True">
                     AUTHORIZATION INFORMATION 
                        <br />
                        </asp:TableCell>
                    </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <br />
                            </asp:TableCell>
                        </asp:TableRow>
 <%--                   <asp:TableRow>
                        <asp:TableCell ColumnSpan="2" HorizontalAlign="Center">
                        <br />
                            <asp:CheckBoxList ID="CheckBoxList4" runat="server" RepeatDirection="Horizontal"
                                TextAlign="Left">
                                <asp:ListItem Text="Must Select One:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Master Card" Value="MC"></asp:ListItem>
                                <asp:ListItem Text="Visa" Value="Visa"></asp:ListItem>
                                <asp:ListItem Text="American Express" Value="AE"></asp:ListItem>
                            </asp:CheckBoxList>
                            <br />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>Card Holder:</asp:TableCell>
                        <asp:TableCell HorizontalAlign="Right">(please print)</asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell><br />Expiry Date:</asp:TableCell>
                        <asp:TableCell HorizontalAlign="Right"><br />
                         Security Code
                         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        (3 digit code on back of card)</asp:TableCell>
                    </asp:TableRow>--%>
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="2" BorderStyle="Solid" BorderWidth="2" HorizontalAlign="Center">Estimate is valid for 10 business days. Any software, ringtones, or pictures provided by your retailer or installed by yourself will not be installed by Company Name.
                     In addition, we do not provide back-up service for your data. There is an automatic $25.00 estimate fee whether the estimate for repair is approved or denied, Estimates without a response after 10 days will be
                     considered rejected and discarded. It is the responsibility of the person who authorized the estimate to retain a copy of the estimate. There is an 90 day warranty on all repairs.
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="rowApprovedBy" runat="server">
                        <asp:TableCell>
                            <asp:Label ID="lblApprovedBy" runat="server" Text=""></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>


                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:CheckBox ID="chkApproved" runat="server" Text="APPROVED" TextAlign="Left" Checked="True" />
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:CheckBox ID="chkDenied" runat="server" Text="DENIED" TextAlign="Left" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell><br />Date:</asp:TableCell>
                        <asp:TableCell><br />Signature:</asp:TableCell>
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

