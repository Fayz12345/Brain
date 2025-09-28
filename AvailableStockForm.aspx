<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AvailableStockForm.aspx.cs" Inherits="BW_WebApp.AvailableStockForm" %>

<%@ Register Assembly="Syncfusion.Shared.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89" Namespace="Syncfusion.Web.UI.WebControls.Shared" TagPrefix="syncfusion" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" style="border-width: 0px; padding-top:0; margin-top:0">
    <div>
        <asp:Panel ID="pnlNew" runat="server">
            <table>
                <tr>
                    <td id="AddTemplateHeader" colspan="" style="border-style: none none solid none;
                        border-width: thick; border-color: #E2E2E2; text-align: left; vertical-align: top;">
                            <asp:Label ID="lblAddOrder" runat="server" Text="Available Stock Sales Order" Font-Size="X-Large"></asp:Label>
                    </td>
                    <td align="right">
                    
                        <asp:Button ID="btnPrint" runat="server" Text="Print" OnClientClick="printIt(); return true;" />

                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        <table>
                            <tr>
                                <td style="text-align: right; vertical-align: top;">
                                    Customer PO Number:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCustomerPONumber" runat="server" Width="100%" MaxLength="50"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right; vertical-align: top;">
                                    Restrict to Project Tag:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtProjectTag" runat="server" Width="100%"></asp:TextBox><br />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right; vertical-align: text-top;">
                                    <br />
                                    Client:
                                </td>
                                <td>
                                    <br />
                                    <asp:TextBox ID="txtBillClient" runat="server" ToolTip="Enter client location Key"
                                        Width="100%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right; vertical-align: top;">
                                    Name/Address:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtBillNameAddresstext" runat="server" Rows="6" TextMode="MultiLine"
                                        Width="100%"></asp:TextBox><br />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right; vertical-align: top;">
                                    Internal Note:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtInternalNote" runat="server" Rows="6" TextMode="MultiLine" Width="100%"
                                        MaxLength="500"></asp:TextBox><br />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right; vertical-align: top;">
                                    Project to pull Inventory:
                                </td>
                                <td>
                                    <asp:TextBox ID="tProject" runat="server" Rows="6" Width="100%" MaxLength="500"></asp:TextBox><br />
                                </td>
                            </tr>

                            <tr>
                                 <td style="text-align: right; vertical-align: top;">
                                 <br />
                                    Status:
                                </td>
                                <td>
                                <br />
                                    <asp:TextBox ID="txtStatus" runat="server" Width="100%"></asp:TextBox>
                                </td>                           
                            </tr>
                            <tr>
                                <td style="text-align: right; vertical-align: top;">
                                    Order Date:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtOrderDate" runat="server" Width="100%"></asp:TextBox>
                                </td>                          
                            </tr>

                        </table>
                    </td>
                    <td valign="top">
                        <table>
                            <tr>
                                <td style="text-align: right; vertical-align: top;">
                                    Purchase Order Number:
                                </td>
                                <td>
                                    <asp:TextBox ID="PurchaseOrderNumber" runat="server" Width="100%" MaxLength="500"></asp:TextBox>
                                    <%--<asp:Label ID="lblPurchaseOrderNumber" runat="server" Text=":" 
                                                Width="100%" BorderStyle="Outset" BorderWidth="1px"></asp:Label>--%>
                                </td>
                            </tr>
                            <tr id="WayBillRow1a">
                                <td style="text-align: right; vertical-align: top;">
                                    Waybill Number:
                                </td>
                                <td valign="top">
                                    <asp:TextBox ID="txtWaybillNumber" runat="server" Width="100%" MaxLength="500"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right; vertical-align: text-top;">
                                <br />
                                    Ship To:
                                </td>
                                <td>
                                <br />
                                    <asp:TextBox ID="txtShipClient" runat="server" ToolTip="Enter client location Key" Width="100%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right; vertical-align: top;">
                                    Name/Address:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtShipNameAddresstext" runat="server" Rows="6" TextMode="MultiLine"
                                        Width="100%"></asp:TextBox><br />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right; vertical-align: top;">
                                    Delivery Note:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDeliveryNote" runat="server" Rows="6" TextMode="MultiLine" Width="100%"
                                        MaxLength="500"></asp:TextBox><br />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right; vertical-align: top;">
                                </td>
                                <td>
                                    Paid:
                                    <input id="chkPaid" type="checkbox" runat="server" readonly="readonly" />
<%--                                    Post Paid:
                                    <input id="chkPostPaid" type="checkbox"  runat="server" readonly="readonly"/>--%>
                                </td>
                            </tr>

                            <tr>
                                <td style="text-align: right; vertical-align: top;">
                                <br />
                                    Pick/Pack Date:
                                </td>  
                                <td>
                                <br />
                                    <asp:TextBox ID="txtPickPackDate" runat="server" Width="100%"></asp:TextBox>
                                </td>                            
                            </tr>

                            <tr>
                                 <td style="text-align: right; vertical-align: top;">
                                    Ship Date:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtShipDate" runat="server" Width="100%"></asp:TextBox>
                                </td>                           
                            </tr>

                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="border-style: solid none none none; border-width: thick; border-color: #E2E2E2;
                        text-align: left; vertical-align: middle;">
                        <asp:GridView ID="grdData" runat="server" DataKeyField="AvailableStock_OrderNumber"
                            AutoGenerateColumns="False" AlternatingRowStyle-BackColor="#E2E2E2" Width="100%"
                            BackColor="#F5F5F5">
                            <Columns>
                                <asp:BoundField DataField="Quantity" HeaderText="QTY" ReadOnly="True" Visible="True">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="PurchaseUnitPrice" HeaderText="UPrice" ReadOnly="True" Visible="True">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Manufacturer" HeaderText="Manufacturer" ReadOnly="True"
                                    Visible="True">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Model" HeaderText="Model" ReadOnly="True" Visible="True">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Colour" HeaderText="Colour" ReadOnly="True" Visible="True">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Grade" HeaderText="Grade" ReadOnly="True" Visible="True">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Carrier" HeaderText="Carrier" ReadOnly="True" Visible="True">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
<%--                                <asp:BoundField DataField="AvailableStock_OrderNumber" HeaderText="Order Number"
                                    ReadOnly="True" Visible="True">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>--%>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>

                <tr>
                    <td colspan="4" style="border-style: solid none none none; border-width: thick; border-color: #E2E2E2;
                        text-align: left; vertical-align: middle;">
                        <br />
                        <asp:GridView ID="grdPickList" runat="server" DataKeyField="OrderDetailID"
                            AutoGenerateColumns="False" AlternatingRowStyle-BackColor="#E2E2E2" Width="100%"
                            BackColor="#F5F5F5">
                            <Columns>
                                <asp:BoundField DataField="QTY" HeaderText="QTY" ReadOnly="True" Visible="True" ItemStyle-VerticalAlign="Top">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>

                                <asp:TemplateField HeaderText="Pick" ItemStyle-VerticalAlign="Top">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPicked" runat="server" Text="0"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Pack" ItemStyle-VerticalAlign="Top">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPacked" runat="server" Text="0"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


<%--                                <asp:BoundField DataField="QtyPacked" HeaderText="Packed" ReadOnly="True" Visible="True" ItemStyle-VerticalAlign="Top">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>--%>
                                <asp:BoundField DataField="Desc_Text" HeaderText="Desc" ReadOnly="True"
                                    Visible="True" ItemStyle-VerticalAlign="Top">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="ESN List" ItemStyle-VerticalAlign="Top">
                                    <ItemTemplate>
                                        <asp:CheckBoxList ID="chkESN" runat="server" RepeatDirection="Horizontal" RepeatColumns="3">
                                        </asp:CheckBoxList>
                                    </ItemTemplate>
                                </asp:TemplateField>


<%--                                <asp:BoundField DataField="AvailableStock_OrderNumber" HeaderText="Order Number"
                                    ReadOnly="True" Visible="True">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>--%>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>



            </table>
        </asp:Panel>
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

