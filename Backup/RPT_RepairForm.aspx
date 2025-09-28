<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RPT_RepairForm.aspx.cs" Inherits="BW_WebApp.RPT_RepairForm" %>

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
    <asp:HiddenField ID="StopAutoExit" runat="server" />
    <asp:Panel runat="server" ID="Repair_01" Visible="false">
        <asp:Table ID="Table1x" runat="server">
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Left" VerticalAlign="Top">
                    <asp:Label ID="DealerID_01" runat="server" Text="" Font-Size="X-Large" Visible="False"></asp:Label>
                    <br />
                    <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/Logo.jpg"></asp:Image>
                    <br />
                    <asp:Table ID="Table8" runat="server" Width="100%">
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Left" VerticalAlign="Top">
                                <asp:Label ID="lblReplacedWith_01a" runat="server" Text="ESN:" Font-Size="Small"></asp:Label>
                                <cc1:LinearBarcode ID="bcBagtag" runat="server" SymbologyID="Code128" ShowText="True"
                                    ImageAutoDelete="True" BarHeightCM="1" LeftMarginCM="0.000" TopMarginCM=".15"
                                    ImageResolution="300" DataToEncode="12345678910abc1" CheckCharacter="False" CheckCharacterInText="False"
                                    Height="20px" ImageType="JPEG" Width="200px" XDimensionCM="0.0400" />
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="Right" VerticalAlign="Top">
                                <asp:Label ID="lblReplacedWith_01" runat="server" Text="Replaced with:" Font-Size="Small"></asp:Label>
                                <cc1:LinearBarcode ID="bcBagtag_01ReplacementIMEI" runat="server" SymbologyID="Code128"
                                    ShowText="True" ImageAutoDelete="True" BarHeightCM=".5" LeftMarginCM="0.000"
                                    TopMarginCM=".15" ImageResolution="300" DataToEncode="12345678910abc1" CheckCharacter="False"
                                    CheckCharacterInText="False" Height="20px" ImageType="JPEG" Width="200px" XDimensionCM="0.0400"
                                    Visible="False" />
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                    <br />
                    <asp:Label ID="lblRepairType" runat="server" Text="Estimate/Repair"></asp:Label>
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="Right">
                    <p style="font-size: x-large">
                        <asp:Label ID="lblcCompanyName" runat="server" Text="Company Name"></asp:Label>
                    </p>
                    <h4>
        <asp:Label ID="lblcAddressLines" runat="server" Text="Address"></asp:Label><br />
        <asp:Label ID="lblcCityProvincePostal" runat="server" Text="Address,Province,Postal"></asp:Label><br />
        <asp:Label ID="lblcPhone" runat="server" Text="Phone #: (   ) 999 9999"></asp:Label><br />
        <asp:Label ID="lblcFax" runat="server" Text="  Fax #: (   ) 999 9999"></asp:Label><br />
        <asp:Label ID="lblcWebsite" runat="server" Text="www.CompanyWebSite.com"></asp:Label><br />
                    </h4>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="2">
                    <asp:Table ID="Table1" runat="server" Width="100%" BorderStyle="Solid" BorderWidth="2">
                        <asp:TableRow BackColor="#CCCCCC">
                            <asp:TableCell ColumnSpan="2" Font-Bold="True">
                     Dealer Information:
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="Right">
                                <asp:Label ID="lblClientReference_01" runat="server" Text=""></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="2">
                                Dealer Name:
                                <asp:Label ID="lblDealerName" runat="server" Text="DealerName"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="Right">
                                Dealer ID:
                                <asp:Label ID="lblDealerID" runat="server" Text="ID"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="2">
                                Phone #:
                                <asp:Label ID="lblPhone" runat="server" Text="PHone"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="Right">
                                <asp:Label ID="lblClientReference" runat="server" Text="Fax"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="2">
                                Address:
                                <asp:Label ID="lblAddress" runat="server" Text="Address"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="Right">
                                Fax #:
                                <asp:Label ID="lblFax" runat="server" Text="Fax"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                City:
                                <asp:Label ID="lblCity" runat="server" Text="City"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                Province:
                                <asp:Label ID="lblProvince" runat="server" Text="Province"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="Right">
                                Postal Code:
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
                            <asp:TableCell ColumnSpan="3" Font-Bold="True">
                     Device Information:
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                ESN/IMEI #:
                                <asp:Label ID="lblESN" runat="server" Text="ESNIMEI"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Label ID="lblMSN" runat="server" Text="MSN"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="Right">
                                RMA #:
                                <asp:Label ID="lblRMA" runat="server" Text="RMA"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="3">
                                Manufacturer:
                                <asp:Label ID="ldlManufacturer" runat="server" Text="Manufacturer"></asp:Label>
                            </asp:TableCell>
<%--                            <asp:TableCell>
                                Model #:
                                <asp:Label ID="lblModel" runat="server" Text="Model"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="Right">
                                Nickname:
                                <asp:Label ID="lblNickname" runat="server" Text="NickName"></asp:Label>
                            </asp:TableCell>--%>
                        </asp:TableRow>


                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="3">
                                Warranty Type:
                                <asp:Label ID="lblWarrantyType" runat="server" Text=""></asp:Label>
<%--                                <asp:CheckBoxList ID="chkInWarranty" runat="server" RepeatDirection="Horizontal"
                                    TextAlign="Left">
                                    <asp:ListItem Text="In Warranty: Yes" Value="Yes"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                </asp:CheckBoxList>--%>
                            </asp:TableCell>
<%--                            <asp:TableCell>
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
                            </asp:TableCell>--%>
                        </asp:TableRow>
                    </asp:Table>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="2">
                    <asp:Table ID="Table3" runat="server" Width="100%" BorderStyle="Solid" BorderWidth="2">
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="2" Font-Bold="True">
                     Customer Complaint:
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="2">
                                First Complaint:
                                <asp:Label ID="lblComplaintCodes" runat="server" Text="First Complaint"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="2">
                                Second Complaint:
                                <asp:Label ID="Label1" runat="server" Text="Second Complaint"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="2">
                                Customer Comment:
                                <asp:Label ID="lblCustomerComment" runat="server" Text="Customer Comment"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="2" HorizontalAlign="Right" Wrap="False">
                                <asp:Label ID="lblEstimateFee" runat="server" Text="E Fee"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="2" HorizontalAlign="Right" Wrap="False">
                                <asp:Label ID="lblSalesOrderFee" runat="server" Text="SO Fee"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Right" Wrap="False" Font-Size="Small">
                            Plus Taxes and Freight where applicable
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="2">
                    <asp:Table ID="Table4" runat="server" Width="100%" BorderStyle="Solid" BorderWidth="2">
                        <asp:TableRow BackColor="#CCCCCC">
                            <asp:TableCell ColumnSpan="3" Font-Bold="True">
                     Repair Information:
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="3" HorizontalAlign="Center">
                                <asp:Label ID="lblClaimStatement" runat="server" Text="Claim Reason + Claim Location + Claim Action"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="3" HorizontalAlign="Left">
                                <asp:Label ID="lblWorkPreformed" runat="server" Text="Work Preformed"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="3">
                                Fault(s) Found:
                                <asp:Label ID="lblFaultCodes" runat="server" Text="FaultCodes"></asp:Label><br>
Repair Code(s):
                                <asp:Label ID="lblFaultCodes2" runat="server" Text="FaultCodes"></asp:Label><br>

                            </asp:TableCell>
                        </asp:TableRow>



                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Left" VerticalAlign="Top" ColumnSpan="3">
                                <table>
                                    <tr>
                                        <td>
                                            Technician Comment(s):
                                        </td>
                                        <td>
                                            <%--<div runat="server" id="Div1" style="font-size: small">--%>
                                            <div runat="server" id="DivNote">
                                                <%--<asp:Label ID="lblNote" runat="server" Text="Note" Font-Size="Small"></asp:Label>--%>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="1" HorizontalAlign="Left">
                                <asp:Label ID="lblReplacementIMEI" runat="server" Text="ReplacementIMEI"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="1" HorizontalAlign="Left">
                                <asp:Label ID="lblComponent" runat="server" Text="Component"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="1" HorizontalAlign="Left">
                                <asp:Label ID="lblCosmetic" runat="server" Text="Cosmetic"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
<%--                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="1" HorizontalAlign="Left">
                                <asp:Label ID="lblModule" runat="server" Text="Module"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>--%>
                    </asp:Table>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="SignatureSection" runat="server">
                <asp:TableCell ColumnSpan="2">
                    <asp:Table ID="Table6" runat="server" Width="100%" BorderStyle="Solid" BorderWidth="2">
                        <%--                    <asp:TableRow BackColor="#CCCCCC">
                        <asp:TableCell ColumnSpan="3" Font-Bold="True">
                     PAYMENT INFORMATION
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
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
                        <%--                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="2" BorderStyle="Solid" BorderWidth="2" HorizontalAlign="Center">Estimate is valid for 10 business days. Any software, ringtones, or pictures provided by your retailer or installed by yourself will not be installed by .
                     In addition, we do not provide back-up service for your data. There is an automatic $25.00 estimate fee whether the estimate for repair is approved or denied, Estimates without a response after 10 days will be
                     considered rejected and discarded. It is the responsibility of the person who authorized the estimate to retain a copy of the estimate. There is an 90 day warranty on all repairs.
                        </asp:TableCell>
                    </asp:TableRow>--%>
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" Font-Size="Small">
                                <asp:Label ID="lblDisclosure" runat="server" Text="In order to have the unit repaired, the customer is responsible to pay the amount indicated.
The store will need to seek approval from the customer for the repair or decline the quote.  Upon seeking direction from the customer please indicate the approve or decline of this quote."></asp:Label>
                                <br />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Right">
                                <asp:CheckBox ID="chkApproved" runat="server" Text="ACCEPT ESTIMATE" TextAlign="Left" />
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:CheckBox ID="chkDenied" runat="server" Text="ESTIMATE DECLINED ($25.00 ESTIMATE FEE APPLIES)"
                                    TextAlign="Right" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Right">
                                <asp:Label ID="Label2" runat="server" Text="Store Use:" Font-Size="Large"></asp:Label>
                                <br />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Right">
                                <asp:Label ID="Label4x" runat="server" Text="PRINT NAME:"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Label ID="Label5" runat="server" Text="________________________________________"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Right">
                                <br />
                                <br />
                                <asp:Label ID="Label3" runat="server" Text="SIGNATURE:"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <br />
                                <br />
                                <asp:Label ID="Label4" runat="server" Text="________________________________________"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <%--                    <asp:TableRow>
                        <asp:TableCell><br />Date:</asp:TableCell>
                        <asp:TableCell><br />Signature:</asp:TableCell>
                    </asp:TableRow>--%>
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" Font-Size="Small">
                                <asp:Label ID="lblLegal1" runat="server" Text="*	PRINTED NAME AND SIGNATURE OF PERSON ACCEPTING OR REFUSING ESTIMATE IS REQUIRED ON ALL RESPONSES"
                                    Font-Size="Smaller"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" Font-Size="Small">
                                <asp:Label ID="lblLegal2" runat="server" Text="*	ESTIMATE NOT REPLIED TO WITHIN 5 BUSINESS DAYS WILL BE RETURNED TO THE DEALER 'AS IS' AND THE $25 ESTIMATE FEE WILL BE AUTOMATICALLY APPLIED TO YOUR ACCOUNT"
                                    Font-Size="Smaller"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="2">
                    <asp:Table ID="Table5" runat="server" Width="100%">
                        <asp:TableRow BackColor="#CCCCCC">
                            <asp:TableCell ColumnSpan="3" Font-Bold="True">
                                <asp:Label ID="lblLegal" runat="server" Text="" Width="100%"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </asp:Panel>

    <asp:Panel runat="server" ID="Repair_02" Visible="false">
        <asp:Table ID="Table_02" runat="server">
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Left" VerticalAlign="Top">
                    <asp:Label ID="DealerID_02" runat="server" Text="" Font-Size="X-Large" Visible="False"></asp:Label>
                    <br />
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Logo.jpg"></asp:Image>
                    <br />
                    <asp:Table ID="Table9" runat="server" Width="100%">
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Left" VerticalAlign="Top">
                                <asp:Label ID="lblReplacedWith_02a" runat="server" Text="ESN:"></asp:Label>
                                <cc1:LinearBarcode ID="bcBagtag_02" runat="server" SymbologyID="Code128" ShowText="True"
                                    ImageAutoDelete="True" BarHeightCM="1" LeftMarginCM="0.000" TopMarginCM=".15"
                                    ImageResolution="300" DataToEncode="12345678910abc1" CheckCharacter="False" CheckCharacterInText="False"
                                    Height="20px" ImageType="JPEG" Width="200px" XDimensionCM="0.0400" />
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="Right" VerticalAlign="Top">
                                <asp:Label ID="lblReplacedWith_02" runat="server" Text="Replaced with:"></asp:Label>
                                <cc1:LinearBarcode ID="bcBagtag_02ReplacementIMEI" runat="server" SymbologyID="Code128"
                                    ShowText="True" ImageAutoDelete="True" BarHeightCM=".5" LeftMarginCM="0.000"
                                    TopMarginCM=".15" ImageResolution="300" DataToEncode="12345678910abc1" CheckCharacter="False"
                                    CheckCharacterInText="False" Height="20px" ImageType="JPEG" Width="200px" XDimensionCM="0.0400"
                                    Visible="False" />
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                    <br />
<%--                    <cc1:LinearBarcode ID="bcBagtag_02" runat="server" SymbologyID="Code128" ShowText="True"
                        ImageAutoDelete="True" BarHeightCM="1" LeftMarginCM="0.000" TopMarginCM=".15"
                        ImageResolution="300" DataToEncode="12345678910abc1" CheckCharacter="False" CheckCharacterInText="False"
                        Height="20px" ImageType="JPEG" Width="200px" XDimensionCM="0.0400" />

                    <cc1:LinearBarcode ID="bcBagtag_02ReplacementIMEI" runat="server" SymbologyID="Code128"
                        ShowText="True" ImageAutoDelete="True" BarHeightCM=".5" LeftMarginCM="0.000"
                        TopMarginCM=".15" ImageResolution="300" DataToEncode="12345678910abc1" CheckCharacter="False"
                        CheckCharacterInText="False" Height="20px" ImageType="JPEG" Width="200px" XDimensionCM="0.0400"
                        Visible="False" /><br />--%>

                    <asp:Label ID="lblRepairType_02" runat="server" Text="Estimate/Repair"></asp:Label>
                </asp:TableCell>



                <asp:TableCell HorizontalAlign="Right">
                    <p style="font-size: x-large">
                        <asp:Label ID="lblcCompanyName_02" runat="server" Text="Company Name"></asp:Label>
                    </p>
                    <h4>

        <asp:Label ID="lblcAddressLines_02" runat="server" Text="Address"></asp:Label><br />
        <asp:Label ID="lblcCityProvincePostal_02" runat="server" Text="Address,Province,Postal"></asp:Label><br />
        <asp:Label ID="lblcPhone_02" runat="server" Text="Phone #: (   ) 999 9999"></asp:Label><br />
        <asp:Label ID="lblcFax_02" runat="server" Text="  Fax #: (   ) 999 9999"></asp:Label><br />
        <asp:Label ID="lblcWebsite_02" runat="server" Text="www.CompanyWebSite.com"></asp:Label><br />

                 </h4>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="2">
                    <asp:Table ID="Table2_02" runat="server" Width="100%" BorderStyle="Solid" BorderWidth="2">
                        <asp:TableRow BackColor="#CCCCCC">
                            <asp:TableCell ColumnSpan="2" Font-Bold="True">
                     Dealer Information:
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="Right">
                                <asp:Label ID="lblClientReference_02" runat="server" Text=""></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="2">
                                Dealer Name:
                                <asp:Label ID="lblDealerName_02" runat="server" Text="DealerName"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="Right">
                                Dealer ID:
                                <asp:Label ID="lblDealerID_02" runat="server" Text="ID"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="2">
                                Phone #:
                                <asp:Label ID="lblPhone_02" runat="server" Text="PHone"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="Right">
                                <asp:Label ID="lblClientReference_02_02" runat="server" Text="Fax"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="2">
                                Address:
                                <asp:Label ID="lblAddress_02" runat="server" Text="Address"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="Right">
                                Fax #:
                                <asp:Label ID="lblFax_02" runat="server" Text="Fax"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                City:
                                <asp:Label ID="lblCity_02" runat="server" Text="City"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                Province:
                                <asp:Label ID="lblProvince_02" runat="server" Text="Province"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="Right">
                                Postal Code:
                                <asp:Label ID="lblPostalCode_02" runat="server" Text="Postal Code"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="2">
                    <asp:Table ID="Table3_02" runat="server" Width="100%" BorderStyle="Solid" BorderWidth="2">
                        <asp:TableRow BackColor="#CCCCCC">
                            <asp:TableCell ColumnSpan="3" Font-Bold="True">
                     Device Information:
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                ESN/IMEI #:
                                <asp:Label ID="lblESN_02" runat="server" Text="ESNIMEI"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Label ID="lblMSN_02" runat="server" Text="MSN"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="Right">
                                RMA #:
                                <asp:Label ID="lblRMA_02" runat="server" Text="RMA"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                Manufacturer:
                                <asp:Label ID="ldlManufacturer_02" runat="server" Text="Manufacturer"></asp:Label>
                            </asp:TableCell>
<%--                            <asp:TableCell>
                                Model #:
                                <asp:Label ID="lblModel_02" runat="server" Text="Model"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="Right">
                                Nickname:
                                <asp:Label ID="lblNickname_02" runat="server" Text="NickName"></asp:Label>
                            </asp:TableCell>--%>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:CheckBoxList ID="chkInWarranty_02" runat="server" RepeatDirection="Horizontal"
                                    TextAlign="Left">
                                    <asp:ListItem Text="In Warranty: Yes" Value="Yes"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                </asp:CheckBoxList>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:CheckBoxList ID="chkExtendedWarranty_02" runat="server" RepeatDirection="Horizontal"
                                    TextAlign="Left">
                                    <asp:ListItem Text="Extended Warranty: Yes" Value="Yes"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                </asp:CheckBoxList>
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="Right">
                                <asp:CheckBoxList ID="chkOutOfWarranty_02" runat="server" RepeatDirection="Horizontal"
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
                    <asp:Table ID="Table4_02" runat="server" Width="100%" BorderStyle="Solid" BorderWidth="2">
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="2" Font-Bold="True">
                     Customer Complaint:
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="2">
                                First Complaint:
                                <asp:Label ID="lblComplaintCodes_02" runat="server" Text="First Complaint"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="2">
                                Second Complaint:
                                <asp:Label ID="lblComplaintCodes_02_02" runat="server" Text="Second Complaint"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="2">
                                Customer Comment:
                                <asp:Label ID="lblCustomerComment_02" runat="server" Text="Customer Comment"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="2" HorizontalAlign="Right" Wrap="False">
                                <asp:Label ID="lblEstimateFee_02" runat="server" Text="E Fee"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="2" HorizontalAlign="Right" Wrap="False">
                                <asp:Label ID="lblSalesOrderFee_02" runat="server" Text="SO Fee"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Right" Wrap="False" Font-Size="Small">
                            Plus Taxes Where Applicable
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="2">
                    <asp:Table ID="Table7_02" runat="server" Width="100%" BorderStyle="Solid" BorderWidth="2">
                        <asp:TableRow BackColor="#CCCCCC">
                            <asp:TableCell ColumnSpan="3" Font-Bold="True">
                     Repair Information:
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="3" HorizontalAlign="Center">
                                <asp:Label ID="lblClaimStatement_02" runat="server" Text="Claim Reason + Claim Location + Claim Action"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="3" HorizontalAlign="Left">
                                <asp:Label ID="lblWorkPreformed_02" runat="server" Text="Work Preformed"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="3">
                                Fault(s) Found:
                                <asp:Label ID="lblFaultCodes_02" runat="server" Text="FaultCodes"></asp:Label><br>
Repair Code(s):
                                <asp:Label ID="lblFaultCodes_022" runat="server" Text="FaultCodes"></asp:Label><br>

                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Left" VerticalAlign="Top" ColumnSpan="3">
                                <table>
                                    <tr>
                                        <td>
                                            Technician Comment(s):
                                        </td>
                                        <td>
                                            <div runat="server" id="DivNote_02" style="font-size: small">
                                                <%--<asp:Label ID="lblNote" runat="server" Text="Note" Font-Size="Small"></asp:Label>--%>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="1" HorizontalAlign="Left">
                                <asp:Label ID="lblReplacementIMEI_02" runat="server" Text="ReplacementIMEI"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="1" HorizontalAlign="Left">
                                <asp:Label ID="lblReplacementModel_02" runat="server" Text="ReplacementModel"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="1" HorizontalAlign="Left">
                                <asp:Label ID="lblComponent_02" runat="server" Text="Component"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="1" HorizontalAlign="Left">
                                <asp:Label ID="lblCosmetic_02" runat="server" Text="Cosmetic"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
<%--                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="1" HorizontalAlign="Left">
                                <asp:Label ID="lblModule_02" runat="server" Text="Module"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>--%>
                    </asp:Table>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="SignatureSection_02" runat="server">
                <asp:TableCell ColumnSpan="2">
                    <asp:Table ID="Table6_02" runat="server" Width="100%" BorderStyle="Solid" BorderWidth="2">
                        <%--                    <asp:TableRow BackColor="#CCCCCC">
                        <asp:TableCell ColumnSpan="3" Font-Bold="True">
                     PAYMENT INFORMATION
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
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
                        <%--                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="2" BorderStyle="Solid" BorderWidth="2" HorizontalAlign="Center">Estimate is valid for 10 business days. Any software, ringtones, or pictures provided by your retailer or installed by yourself will not be installed by .
                     In addition, we do not provide back-up service for your data. There is an automatic $25.00 estimate fee whether the estimate for repair is approved or denied, Estimates without a response after 10 days will be
                     considered rejected and discarded. It is the responsibility of the person who authorized the estimate to retain a copy of the estimate. There is an 90 day warranty on all repairs.
                        </asp:TableCell>
                    </asp:TableRow>--%>
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" Font-Size="Small">
                                <asp:Label ID="lblDisclosure_02" runat="server" Text="In order to have the unit repaired, the customer is responsible to pay the amount indicated.
The store will need to seek approval from the customer for the repair or decline the quote.  Upon seeking direction from the customer please indicate the approve or decline of this quote."></asp:Label>
                                <br />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Right">
                                <asp:CheckBox ID="chkApproved_02" runat="server" Text="ACCEPT ESTIMATE" TextAlign="Left" />
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:CheckBox ID="chkDenied_02" runat="server" Text="ESTIMATE DECLINED ($25.00 ESTIMATE FEE APPLIES)"
                                    TextAlign="Right" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Right">
                                <asp:Label ID="Label1_02" runat="server" Text="Store Use:" Font-Size="Large"></asp:Label>
                                <br />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Right">
                                <asp:Label ID="Label4_02" runat="server" Text="PRINT NAME:"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Label ID="Label5_02" runat="server" Text="________________________________________"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Right">
                                <br />
                                <br />
                                <asp:Label ID="Label2_02" runat="server" Text="SIGNATURE:"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <br />
                                <br />
                                <asp:Label ID="Label3_02" runat="server" Text="________________________________________"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <%--                    <asp:TableRow>
                        <asp:TableCell><br />Date:</asp:TableCell>
                        <asp:TableCell><br />Signature:</asp:TableCell>
                    </asp:TableRow>--%>
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" Font-Size="Small">
                                <asp:Label ID="lblLegal1_02" runat="server" Text="*	PRINTED NAME AND SIGNATURE OF PERSON ACCEPTING OR REFUSING ESTIMATE IS REQUIRED ON ALL RESPONSES"
                                    Font-Size="Smaller"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" Font-Size="Small">
                                <asp:Label ID="lblLegal2_02" runat="server" Text="*	ESTIMATE NOT REPLIED TO WITHIN 5 BUSINESS DAYS WILL BE RETURNED TO THE DEALER 'AS IS' AND THE $25 ESTIMATE FEE WILL BE AUTOMATICALLY APPLIED TO YOUR ACCOUNT"
                                    Font-Size="Smaller"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="2">
                    <asp:Table ID="Table5_02" runat="server" Width="100%">
                        <asp:TableRow BackColor="#CCCCCC">
                            <asp:TableCell ColumnSpan="3" Font-Bold="True">
                                <asp:Label ID="lblLegal_02" runat="server" Text="" Width="100%"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </asp:Panel>

    <asp:Panel runat="server" ID="PnlPackingSlip" Visible="false">
        <asp:Table ID="Table7" runat="server">
            <asp:TableRow>
                <asp:TableCell ColumnSpan="2">
                 <p style="font-size: x-large">REPAIR PACKING SLIP
                 </p>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Width="35%">
                SHIP TO: 
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label runat="server" Text="lblShipTo" ID="lblShipTo" Font-Bold="True"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                WORK ORDER #: 
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label runat="server" Text="lblWorkOrder" ID="lblWorkOrder"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell ColumnSpan="2">
                 <br />
                 <p style="font-size: large">CUSTOMER DETAILS:
                 </p>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                CUSTOMER REFERENCE #: 
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label runat="server" Text="lblCustRef" ID="lblCustRef"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                RMA NUMBER: 
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label runat="server" Text="lblRMA" ID="lblRMA_03"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                WARRANTY #: 
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label runat="server" Text="lblWarranty" ID="lblWarranty"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>


            <asp:TableRow>
                <asp:TableCell ColumnSpan="2">
                 <br />
                 <p style="font-size: large">PHONE DETAILS:
                 </p>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="2">
                    <asp:Label runat="server" Text="" ID="lblMMCC"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                IMEI/ESN: 
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label runat="server" Text="lblRMA" ID="lblESN_03"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                PART(S) SENT: 
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label runat="server" Text="lblWarranty" ID="lblParts"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>



            <asp:TableRow>
                <asp:TableCell ColumnSpan="2">
                 <br />
                 <p style="font-size: large">NOTES:
                 </p>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                CUSTOMER COMPLAINT: 
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label runat="server" Text="lblWarranty" ID="lblComplaint_03"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                TECH COMMENTS: 
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label runat="server" Text="lblWarranty" ID="lblTechComments"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>




            <asp:TableRow>
                <asp:TableCell ColumnSpan="2">
                <br />
                <br />
                <br />


                    <p style="font-size: x-large">
                        <asp:Label ID="lblcCompanyName_03" runat="server" Text="Company Name"></asp:Label>
                    </p>
                    <h3>
                        <asp:Label ID="lblcAddressLines_03" runat="server" Text="Address"></asp:Label><br />
                        <asp:Label ID="lblcCityProvincePostal_03" runat="server" Text="City, Province, Postal"></asp:Label><br />
                 </h3>
                </asp:TableCell>
            </asp:TableRow>


        </asp:Table>
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


