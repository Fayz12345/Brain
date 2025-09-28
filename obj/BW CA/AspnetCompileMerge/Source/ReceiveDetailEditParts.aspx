<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReceiveDetailEditParts.aspx.cs" Inherits="BW_WebApp.ReceiveDetailEditParts" %>

<%@ Register Assembly="Syncfusion.Grid.Grouping.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89"
    Namespace="Syncfusion.Web.UI.WebControls.Grid.Grouping" TagPrefix="syncfusion" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Syncfusion.Shared.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89" Namespace="Syncfusion.Web.UI.WebControls.Shared" TagPrefix="syncfusion" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="cMC" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

        

            <asp:Panel ID="pnlMainView" runat="server">
                <asp:Panel ID="Panel3" runat="server" HorizontalAlign="Left" Width="100%">
                    <asp:HiddenField ID="hdnReceiveDetailID" runat="server" />
                    <asp:Label ID="lblRecordTitle" runat="server" Text="Edit Parts Screen" Font-Size="Larger"></asp:Label>
                    <br />
                </asp:Panel>
                <asp:Panel ID="pnlMainGrid" runat="server" Width="100%" ScrollBars="Auto" HorizontalAlign="Left">
                    <asp:GridView ID="GridViewDetail" runat="server" Width="100%" DataKeyNames="MasterPartsLinkTablePriceListID"
                        AutoGenerateColumns="False" CssClass="mGrid" AlternatingRowStyle-CssClass="alt"
                        AllowPaging="false">
                        <SelectedRowStyle CssClass="srowstyle" />
                        <Columns>
                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdnReceiveDetailItemID" runat="server" />
                                    <asp:HiddenField ID="hdnReceiveDetailPartsUsageID" runat="server" />
                                    <asp:ImageButton ID="imgEditDetail" runat="server" HeaderText="" ImageUrl="~/Images/edit_inline.gif"
                                        ToolTip="Edit QTY/Unit Price" Width="20px" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>">
                                    </asp:ImageButton>
                                    <asp:ImageButton ID="imgEditDetailSave" runat="server" HeaderText="" ImageUrl="~/Images/CreateTasks.gif"
                                        ToolTip="Save" Width="20px" Visible="False" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>">
                                    </asp:ImageButton>
                                    <asp:ImageButton ID="imgEditDetailCancel" runat="server" HeaderText="" ImageUrl="~/Images/close_inline.gif"
                                        ToolTip="Cancel" Width="20px" Visible="False" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>">
                                    </asp:ImageButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ReceiveDetailItemID" HeaderText="ID" ReadOnly="True" Visible="false">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PartNumber" HeaderText="Part Number" ReadOnly="True" Visible="true">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Description" HeaderText="Description" ReadOnly="True"
                                Visible="true">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="GMPPartNumber" HeaderText="IMM PartNumber" ReadOnly="True"
                                Visible="true">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="GMPPartDescription" HeaderText="IMM Description" ReadOnly="True"
                                Visible="true">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Quantity" HeaderText="Quantity" ReadOnly="True" Visible="true">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Purchase Price" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <%--<ControlStyle Width="50px" />--%>
                                <ItemTemplate>
                                    <asp:Label ID="lblUnitPurchasePrice" runat="server" Visible="true"></asp:Label>
                                    <asp:TextBox ID="txtUnitPurchasePrice" runat="server" ToolTip="Enter the Correct Purchase Price"
                                        Visible="False" Width="95%" MaxLength="10"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Avg Purchase Price" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left">
                                <%--<ControlStyle Width="30px" />--%>
                                <ItemTemplate>
                                    <asp:Label ID="lblAveragePurchasePrice" runat="server" Visible="true"></asp:Label>
                                    <asp:TextBox ID="txtAveragePurchasePrice" runat="server" ToolTip="Enter the Average part Purchase Price"
                                        Visible="False" Width="95%" MaxLength="10"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Price" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                <%--<ControlStyle Width="50px" />--%>
                                <ItemTemplate>
                                    <asp:Label ID="lblPrice" runat="server" Visible="true"></asp:Label>
                                    <asp:TextBox ID="txtPrice" runat="server" ToolTip="Enter the Correct Selling Price"
                                        Visible="False" Width="95%" MaxLength="10"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="CreateDate" HeaderText="Create Date" ReadOnly="True" Visible="true">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CreateUser" HeaderText="Create User" ReadOnly="True" Visible="true">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="LastUpdateDate" HeaderText="Last Update Date" ReadOnly="True"
                                Visible="true">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="LastUpdateUser" HeaderText="Last Update User" ReadOnly="True"
                                Visible="true">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>





    <script type="text/javascript">

        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

        function BeginRequestHandler(sender, args) {
            if (args._postBackElement.id != "SkinTable1") {
                //                ConfigureWaitingPopup(Popup);
                $('#loading').show();
            }
        }
        function EndRequestHandler(sender, args) {

            $('#loading').hide();
        }



</script>







</asp:Content>


