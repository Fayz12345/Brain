<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PartUpdate.aspx.cs" Inherits="BW_WebApp.Maintenance.PartUpdate" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Syncfusion.Shared.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89" Namespace="Syncfusion.Web.UI.WebControls.Shared" TagPrefix="syncfusion" %>
<%@ Register Assembly="Syncfusion.Tools.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89"  Namespace="Syncfusion.Web.UI.WebControls.Tools" TagPrefix="syncfusion" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cMC" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            
            <asp:HiddenField ID="hdnUserName" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnMasterPartLinkTableID" runat="server" ClientIDMode="Static" />
            <asp:TabContainer ID="TabContainer1" runat="server">
                <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="SKU Specific">
                    <ContentTemplate>
                        <asp:Panel ID="pnlHeader" runat="server">
                            <table>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="Label3" runat="server" Text="Location"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="drpLocationList" runat="server" ToolTip="Location where these parts are located"
                                            AutoPostBack="false">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="Label2" runat="server" Text="Category"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="drpDropPart" runat="server" ToolTip="Drop Part" AutoPostBack="false">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="Label1" runat="server" Text="Manufacturer"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="drpManufacturer" runat="server" ToolTip="Manufacturer" AutoPostBack="false" Width="95%">
                                            <asp:ListItem Text="A" Value="A">
                                            </asp:ListItem>
                                            <asp:ListItem Text="A" Value="A">
                                            </asp:ListItem>
                                            <asp:ListItem Text="A" Value="A">
                                            </asp:ListItem>
                                            <asp:ListItem Text="A" Value="A">
                                            </asp:ListItem>
                                            <asp:ListItem Text="A" Value="A">
                                            </asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td align="right">
                                        <asp:Button ID="btnRefresh" runat="server" Text="Refresh"  Width="100%"/>
                                        <%--<asp:Button ID="btnAddNew" runat="server" Text="Add" />--%>
<%--                                        <asp:Button ID="btnResetPartNumbers" runat="server" Text="Reset Part Numbers Back To Blank" />
                                        <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="btnResetPartNumbers"
                                            ConfirmText="Are you sure you want to Reset the ENTIRE Part Number Database to NOTHING?">
                                        </asp:ConfirmButtonExtender>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="Label5" runat="server" Text="IMM Part Number:"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtIMMPartNumber" runat="server" Width="95%"></asp:TextBox>
                                    </td>
                                    <td align="right">
                                        <asp:Button ID="btnSearchPartNumber" runat="server" Text="Search"  Width="100%"/>
                                    </td>
                                </tr>
                            </table>

                            <%--<asp:Button ID="btnSaveGridData" runat="server" Text="Save" />--%>
                            <br />
                        </asp:Panel>
                        <asp:Panel ID="pnlMainGridPN" runat="server" Style="overflow: auto; max-height: 500px;
                            width: auto;" HorizontalAlign="Left">
                            <asp:HiddenField ID="hdnAllowGridUpdate" runat="server" />
                            <%--<asp:Panel ID="Panel2" runat="server" Width="100%" ScrollBars="Auto" HorizontalAlign="Left">--%>
                            <asp:GridView ID="MainGridPN" runat="server" Width="100%" DataKeyNames="MasterPartsLinkTableID"
                                AutoGenerateColumns="False" CssClass="mGrid" AlternatingRowStyle-CssClass="alt">
                                <%--                                <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NextPreviousFirstLast"
                                    NextPageText="Next" PreviousPageText="Previous" />--%>
                                <SelectedRowStyle CssClass="srowstyle" />
                                <Columns>
                                    <asp:BoundField DataField="MasterPartsID" HeaderText="ID" ReadOnly="True" Visible="false">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgDelete" runat="server" HeaderText="" ImageUrl="~/Images/delete-pro.bmp"
                                                ToolTip="Delete this partnumber." Width="15px"></asp:ImageButton>
                                            <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="imgDelete"
                                                ConfirmText="Are you sure you want to Delete this partnumber?">
                                            </asp:ConfirmButtonExtender>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgAddTransaction" runat="server" HeaderText="" ImageUrl="~/Images/ADD.BMP"
                                                ToolTip="Add New Parts, remove damaged parts etc." Width="15px"></asp:ImageButton>
                                            <asp:ImageButton ID="imgTransfer" runat="server" HeaderText="" ImageUrl="~/Images/uparrow_inline.gif"
                                                ToolTip="Transfer part to another warehouse." Width="15px"></asp:ImageButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgChangeCategory" runat="server" HeaderText="" ImageUrl="~/Images/Files.png"
                                                ToolTip="Change Category" Width="15px"></asp:ImageButton>
                                            <asp:ImageButton ID="imgViewIFSLocations" runat="server" HeaderText="" ImageUrl="~/Images/folders.gif"
                                                ToolTip="View IFS Locations" Width="15px"></asp:ImageButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Class" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle Width="100px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblClassType" runat="server" Text=""></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Part" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle Width="100px" />
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdnMasterPartLinkTableIDx" runat="server" />
                                            <asp:HiddenField ID="hdnMasterPartID" runat="server" />
                                            <asp:Label ID="lblPartDesc" runat="server" Text=""></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgEditDemographics" runat="server" HeaderText="" ImageUrl="~/Images/expand.gif"
                                                ToolTip="Edit Part number, description etc." Width="15px"></asp:ImageButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="PartNumber" HeaderText="MFG. Part #" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="GMPPartNumber" HeaderText="GMP. Part #" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="GMPPartDescription" HeaderText="GMP. Part Desc" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <%--                                    <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgEditDetail" runat="server" HeaderText="" ImageUrl="~/Images/edit.gif"
                                                ToolTip="Edit QTY/Unit Price" Width="15px" Enabled="False"></asp:ImageButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:BoundField DataField="Quantity" HeaderText="Grand QTY" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                       <%--             <asp:BoundField DataField="AveragePurchasePrice" HeaderText="Average Purchase Price"
                                        HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="UnitPrice" HeaderText="Price" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="InWarrentyWorkPrice" HeaderText="Warranty Price" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>--%>
                                  <%--  <asp:BoundField DataField="QTYMin" HeaderText="Min" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="QTYMax" HeaderText="Max" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="QTYReorder" HeaderText="Reorder" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>--%>
                                    <asp:TemplateField HeaderText="Models" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <%--                                <asp:ImageButton ID="imgChangeModel" runat="server" HeaderText="" ImageUrl="~/Images/closed.gif"
                                    ToolTip="Update Model List" Width="15px"></asp:ImageButton>--%>
                                            <asp:Label ID="txtModelDescription" runat="server" Text=""></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </asp:Panel>
                        <asp:Panel ID="pnlAddTransaction" runat="server" Visible="false">
                            <asp:Table ID="Table5" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="5" Font-Bold="True" HorizontalAlign="Left">
                        <h1>
                            Edit Parts - Add/Adjust inventory</h1>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Width="15%">
                                    </asp:TableCell>
                                    <asp:TableCell>
                                    MFG. Part #  
                                    </asp:TableCell>
                                    <asp:TableCell>
                                    GMP. Part # 
                                    </asp:TableCell>
                                    <asp:TableCell>
                                    GMP. Part Desc
                                    </asp:TableCell>
                                   <%-- <asp:TableCell>
                                    QTY  
                                    </asp:TableCell>--%>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Width="15%">
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="lblMFGPartNumbere" runat="server" Text="MGF Part Number."></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="lblGMPPartNumbere" runat="server" Text="IMM Part Number."></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="lblGMPDesce" runat="server" Text="IMM Description."></asp:Label>
                                    </asp:TableCell>
                                   <%-- <asp:TableCell>
                                        <asp:Label ID="lblQTY" runat="server" Text="Quantity."></asp:Label>
                                    </asp:TableCell>--%>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Left">
                                  <br />
                                    <br />
                                    <br />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Right">
                                    Transaction Type:  
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Left">
                                        <asp:DropDownList ID="drpTransType" runat="server" AutoPostBack="True">
                                        </asp:DropDownList>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell VerticalAlign="Top" HorizontalAlign="Right">
                                    IFS Location:  
                                    </asp:TableCell>
                                    <asp:TableCell ColumnSpan="3" HorizontalAlign="Left">
                                        <asp:TextBox ID="IFSToLocation" runat="server" ClientIDMode="Static" BackColor="#A5EBC5"
                                            ForeColor="Black"></asp:TextBox>
                                        <asp:GridView ID="gvLocationEditParts" runat="server" Width="100%" DataKeyNames="MasterPartsTableIFSLocationStorageID"
                                            AutoGenerateColumns="False" CssClass="mGrid" AlternatingRowStyle-CssClass="alt"
                                            AllowPaging="false">
                                            <SelectedRowStyle CssClass="srowstyle" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="S" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgSelectLocation" runat="server" HeaderText="" ImageUrl="~/Images/details_open.png"
                                                            ToolTip="Select This Location" Width="15px"></asp:ImageButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="MasterPartsTableIFSLocationStorageID" HeaderText="ID"
                                                    ReadOnly="True" Visible="false">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="QTY" HeaderText="QTY" ReadOnly="True">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="IFSLocation" HeaderText="IFSLocation" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:BoundField>
<%--                                                <asp:BoundField DataField="Quantity" HeaderText="Quantity" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:BoundField>--%>
                                                <asp:BoundField DataField="GMPPartNumber" HeaderText="GMPPartNumber" ReadOnly="True">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="GMPPartDescription" HeaderText="GMPPartDescription" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PartNumber" HeaderText="PartNumber" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Description" HeaderText="Description" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:BoundField>
                                            </Columns>
                                        </asp:GridView>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Right">
                                    QTY:  
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Left">
                                        <asp:TextBox ID="txtTransQTY" runat="server" ToolTip="QTY - Must be a positive number"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server" ID="RowPOVendor">
                                    <asp:TableCell HorizontalAlign="Right">
                                       PO Vendor: 
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Left">
                                        <asp:TextBox ID="txtPOVendor" runat="server" ToolTip="PO Vendor" MaxLength="50"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server" ID="RowPONumber">
                                    <asp:TableCell HorizontalAlign="Right">
                                       PO Number: 
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Left">
                                        <asp:TextBox ID="txtPONumber" runat="server" ToolTip="PO Number" MaxLength="12"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server" ID="RowPOLine">
                                    <asp:TableCell HorizontalAlign="Right">
                                       PO Line: 
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Left">
                                        <asp:TextBox ID="txtPOLine" runat="server" ToolTip="PO Line" MaxLength="4"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server" ID="RowPOReceipt">
                                    <asp:TableCell HorizontalAlign="Right">
                                       PO Receipt Date: 
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Left">
                                        <asp:TextBox ID="txtPOReceiptDate" runat="server" ToolTip="PO Receipt Date"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtPOReceiptDate"
                                            Format="MM/dd/yyyy">
                                        </asp:CalendarExtender>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server" ID="RowPOPrice">
                                    <asp:TableCell HorizontalAlign="Right">
                                       Part Purchase Price: 
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Left">
                                        <asp:TextBox ID="txtTransPurchasePrice" runat="server" ToolTip="Purchase Price"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Right">
                                    Misc Description:
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Left">
                                        <asp:TextBox ID="txtTransDesc" runat="server" ToolTip="Misc Description"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow HorizontalAlign="Left">
                                    <asp:TableCell>
                                        <asp:Button ID="btnAddTransactionSave" runat="server" Text="Save" />
                                        <asp:Button ID="btnAddTransactionCancel" runat="server" Text="Cancel" />
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Left" ColumnSpan="4">
                                        <br />
                                        <asp:Label ID="lblEditMessage" runat="server" Text=""></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:Panel>
                        <asp:Panel ID="pnlEditPart" runat="server" Visible="false">
                            <asp:Table ID="Table1e" runat="server">
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="2" Font-Bold="True" HorizontalAlign="Center">
                                    Edit Parts - Demographics
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Label ID="Label7" runat="server" Text="Manufacturer"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:DropDownList ID="drpManufacturerEdit" runat="server" ToolTip="Manufacturer" AutoPostBack="false"
                                            Width="95%">
                                            <asp:ListItem Text="A" Value="A">
                                            </asp:ListItem>
                                            <asp:ListItem Text="A" Value="A">
                                            </asp:ListItem>
                                            <asp:ListItem Text="A" Value="A">
                                            </asp:ListItem>
                                            <asp:ListItem Text="A" Value="A">
                                            </asp:ListItem>
                                            <asp:ListItem Text="A" Value="A">
                                            </asp:ListItem>
                                        </asp:DropDownList>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                    Class:  
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:DropDownList ID="drpClassType" runat="server">
                                        </asp:DropDownList>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                    MFG. Part #:  
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="txtMFGPartNumbere" runat="server" ToolTip="MFG. Part #"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                    GMP. Part #: 
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="txtGMPPartNumbere" runat="server" ToolTip="GMP. Part #"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                    GMP. Part Desc:
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="txtGMPDesce" runat="server" ToolTip="GMP. Part Desc"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                    Unit Price:
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="txtUnitPrice" runat="server" ToolTip="Unit Price"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                    InWarrenty Price:
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="txtInWarrentyPrice" runat="server" ToolTip="In Warrenty Unit Price"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                    Min:
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="txtInventoryMin" runat="server" ToolTip="Suggested Minimum inventory total."></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                    Max:
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="txtInventoryMax" runat="server" ToolTip="Suggested Maximum inventory total"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                    Reorder point:
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="txtReorderPoint" runat="server" ToolTip="Reorder Point"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow Width="100%">
                                    <asp:TableCell ColumnSpan="2">
                                        <asp:Button ID="btnSavePartNumberse" runat="server" Text="Save" />
                                        <asp:Button ID="btnCancelSavee" runat="server" Text="Cancel" />
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:Panel>
                        <asp:Panel ID="pnlChangeCategory" runat="server" Visible="false">
                            <asp:Table ID="Table2" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="5" Font-Bold="True" HorizontalAlign="Left">
                        <h1>
                            Change Category</h1>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Width="15%">
                                    </asp:TableCell>
                                    <asp:TableCell>
                                    MFG. Part #  
                                    </asp:TableCell>
                                    <asp:TableCell>
                                    GMP. Part # 
                                    </asp:TableCell>
                                    <asp:TableCell>
                                    GMP. Part Desc
                                    </asp:TableCell>
                                  <%--  <asp:TableCell>
                                    QTY  
                                    </asp:TableCell>--%>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Width="15%">
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="CClblMFGPartNumbere" runat="server" Text="MGF Part Number."></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="CClblGMPPartNumbere" runat="server" Text="IMM Part Number."></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="CClblGMPDesce" runat="server" Text="IMM Description."></asp:Label>
                                    </asp:TableCell>
                                 <%--   <asp:TableCell>
                                        <asp:Label ID="CClblQTY" runat="server" Text="Quantity."></asp:Label>
                                    </asp:TableCell>--%>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Left">
                                    <br />
                                    <br />
                                    <br />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Right">
                                        <asp:Label ID="Label6" runat="server" Text="NEW Category"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Left">
                                        <asp:DropDownList ID="drpChangeCategoryPart" runat="server" ToolTip="Drop Part" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Left">
                                        <br />
                                        <asp:Button ID="EditCategorySave" runat="server" Text="Save" />
                                        <asp:Button ID="EditCategoryCancel" runat="server" Text="Cancel" />
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Left" ColumnSpan="4">
                                        <br />
                                        <asp:Label ID="txtTransferReasonCC" runat="server" Text=""></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:Panel>
                        <asp:Panel ID="pnlTransfer" runat="server" Visible="false">
                            <asp:Table ID="Table1" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="5" Font-Bold="True" HorizontalAlign="Left">
                        <h1>
                            Parts Transfer</h1>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Width="15%">
                                    </asp:TableCell>
                                    <asp:TableCell>
                                    MFG. Part #  
                                    </asp:TableCell>
                                    <asp:TableCell>
                                    GMP. Part # 
                                    </asp:TableCell>
                                    <asp:TableCell>
                                    GMP. Part Desc
                                    </asp:TableCell>
                                  <%--  <asp:TableCell>
                                    QTY  
                                    </asp:TableCell>--%>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Width="15%">
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="PTlblMFGPartNumbere" runat="server" Text="MGF Part Number."></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="PTlblGMPPartNumbere" runat="server" Text="IMM Part Number."></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="PTlblGMPDesce" runat="server" Text="IMM Description."></asp:Label>
                                    </asp:TableCell>
                                  <%--  <asp:TableCell>
                                        <asp:Label ID="PTlblQTY" runat="server" Text="Quantity."></asp:Label>
                                    </asp:TableCell>--%>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Left">
                                    <br />
                                    <br />
                                    <br />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Right">
                                        <asp:Label ID="lblOriginalWarehouse" runat="server" Text="Original Warehouse"></asp:Label><br />
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Left">
                                        <asp:Label ID="lblOriginalWarehouse_A" runat="server" Text="Original Warehouse"></asp:Label><br />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Right" VerticalAlign="Top">
                                        <asp:Label ID="Label10" runat="server" Text="IFS Location FROM:" AssociatedControlID="PTIFSFromLocation"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Left" ColumnSpan="4">
                                        <asp:TextBox ID="PTIFSFromLocation" runat="server" ClientIDMode="Static" BackColor="#A5EBC5"
                                            ForeColor="Black"></asp:TextBox>
                                        <asp:GridView ID="gvLocationTransferPartsFrom" runat="server" Width="100%" DataKeyNames="MasterPartsTableIFSLocationStorageID"
                                            AutoGenerateColumns="False" CssClass="mGrid" AlternatingRowStyle-CssClass="alt">
                                            <SelectedRowStyle CssClass="srowstyle" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="S" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgSelectLocation" runat="server" HeaderText="" ImageUrl="~/Images/opened_up.gif"
                                                        
                                                            ToolTip="Select From Location" Width="15px"></asp:ImageButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="S" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgSelectLocationDown" runat="server" HeaderText="" ImageUrl="~/Images/opened.gif"
                                                            ToolTip="Select To Location" Width="15px"></asp:ImageButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:BoundField DataField="MasterPartsTableIFSLocationStorageID" HeaderText="ID"
                                                    ReadOnly="True" Visible="false">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="QTY" HeaderText="QTY" ReadOnly="True">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="IFSLocation" HeaderText="IFSLocation" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:BoundField>
<%--                                                <asp:BoundField DataField="Quantity" HeaderText="Quantity" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:BoundField>--%>
                                                <asp:BoundField DataField="GMPPartNumber" HeaderText="GMPPartNumber" ReadOnly="True">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="GMPPartDescription" HeaderText="GMPPartDescription" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PartNumber" HeaderText="PartNumber" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Description" HeaderText="Description" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:BoundField>
                                            </Columns>
                                        </asp:GridView>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Right">
                                        <asp:Label ID="Label10x" runat="server" Text="New Location:" AssociatedControlID="drpLocationList2"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Left">
                                        <asp:DropDownList ID="drpLocationList2" runat="server" ToolTip="Location where these parts are to Go">
                                        </asp:DropDownList>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Right">
                                        <asp:Label ID="Label10hh" runat="server" Text="IFS Location TO:" AssociatedControlID="PTIFSToLocation"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Left">
                                        <asp:TextBox ID="PTIFSToLocation" runat="server" ClientIDMode="Static" BackColor="#A5EBC5"
                                            ForeColor="Black"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Right">
                                        <asp:Label ID="Label8" runat="server" Text="QTY to Transfer:" AssociatedControlID="txtTransferQTY"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Left">
                                        <asp:TextBox ID="txtTransferQTY" runat="server" ToolTip="QTY To Transfer"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                             <%--   <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Right">
                                        <asp:CheckBox ID="chkAveragePurchasePrice" runat="server" Checked="True" />
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Left">
                                        <asp:Label ID="Label11" runat="server" Text="Calculate Average Purchase Price:" AssociatedControlID="chkAveragePurchasePrice"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>--%>
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Right">
                                        <asp:Label ID="Label9" runat="server" Text="Reason for Transfer:" AssociatedControlID="txtTransferReason"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Left">
                                        <asp:TextBox ID="txtTransferReason" runat="server" ToolTip="Reason for Transfer:"
                                            Width="80%" MaxLength="50"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Left">
                                        <br />
                                        <asp:Button ID="btnTransferSave" runat="server" Text="GO" ToolTip="Record the transfer." />
                                        <asp:Button ID="btnTransferCancel" runat="server" Text="Close" />
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Left" ColumnSpan="4">
                                        <br />
                                        <asp:Label ID="lblTransferMessage" runat="server" Text=""></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:Panel>
                        <asp:Panel ID="pnlLocationView" runat="server" Visible="false" Style="overflow: auto;
                            max-height: 500px; width: auto;" HorizontalAlign="Left">
                            <asp:Table ID="Table6" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell Width="100%">
                                        <asp:Label ID="Label12" runat="server" Text="View Locations" Width="100%" Font-Bold="True"
                                            Font-Size="Large"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Width="100%">
                                        <asp:GridView ID="gvLoactions" runat="server" Width="100%" DataKeyNames="MasterPartsTableIFSLocationStorageID"
                                            AutoGenerateColumns="False" CssClass="mGrid" AlternatingRowStyle-CssClass="alt">
                                            <SelectedRowStyle CssClass="srowstyle" />
                                            <Columns>
                                                <asp:BoundField DataField="MasterPartsTableIFSLocationStorageID" HeaderText="ID"
                                                    ReadOnly="True" Visible="false">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="QTY" HeaderText="QTY" ReadOnly="True">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="IFSLocation" HeaderText="IFSLocation" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:BoundField>
<%--                                                <asp:BoundField DataField="Quantity" HeaderText="Quantity" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:BoundField>--%>
                                                <asp:BoundField DataField="GMPPartNumber" HeaderText="GMPPartNumber" ReadOnly="True">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="GMPPartDescription" HeaderText="GMPPartDescription" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PartNumber" HeaderText="PartNumber" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Description" HeaderText="Description" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:BoundField>
                                            </Columns>
                                        </asp:GridView>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <br />
                                        <br />
                                        <%--<asp:Button ID="Button1" runat="server" Text="Update Average" ToolTip="Update Average Price" />--%>
                                        <asp:Button ID="btnLocationViewClose" runat="server" Text="Close" />
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="TabPOReceive" runat="server" HeaderText="PO Receive">
                    <ContentTemplate>
                        <asp:Panel ID="Panel1" runat="server">
                            <asp:Label ID="Label4" runat="server" Text="Location" AssociatedControlID="drpLocationListPOReceive"></asp:Label>
                            <asp:DropDownList ID="drpLocationListPOReceive" runat="server" ToolTip="Location where these parts are Received Into"
                                AutoPostBack="True">
                            </asp:DropDownList>
                            <asp:TabContainer ID="TabContainer2" runat="server">
                                <asp:TabPanel ID="TabPanel3" runat="server" HeaderText="Open Purchase Orders">
                                    <ContentTemplate>
                                        <asp:Panel ID="Panel2" runat="server" Style="overflow: auto; max-height: 500px; width: auto;"
                                            HorizontalAlign="Left">
                                            <asp:HiddenField ID="HiddenField1" runat="server" />


                                            <asp:Button ID="RefreshOpenPOGrid" runat="server" Text="Refresh" Width="100%" />







                                            <asp:GridView ID="GridOpenPO" runat="server" Width="100%" DataKeyNames="PONumberOrderNo"
                                                AutoGenerateColumns="False" CssClass="mGrid" AlternatingRowStyle-CssClass="alt"
                                                AutoGenerateSelectButton="True">
                                                <SelectedRowStyle CssClass="srowstyle" />
                                                <Columns>
                                                    <asp:BoundField DataField="IFSPurchaseOrderHeaderID" HeaderText="ID" ReadOnly="True"
                                                        Visible="false">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="SUPPLIER_NAME" HeaderText="Supplier" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemStyle HorizontalAlign="left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="PONumberOrderNo" HeaderText="PO Number" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemStyle HorizontalAlign="left" />
                                                    </asp:BoundField>

                                                    <asp:BoundField DataField="QTY" HeaderText="QTY" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemStyle HorizontalAlign="left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="QTYOpen" HeaderText="QTY Open" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemStyle HorizontalAlign="left" />
                                                    </asp:BoundField>


                                                    <asp:BoundField DataField="SupplierOrderNo" HeaderText="Supplier Order #" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemStyle HorizontalAlign="left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="OrderDateDate" HeaderText="Order Date" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemStyle HorizontalAlign="left" />
                                                    </asp:BoundField>

                                                    <asp:BoundField DataField="SUPP_ADDR_1" HeaderText="Addr 1" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemStyle HorizontalAlign="left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="SUPP_ADDR_2" HeaderText="Addr 2" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemStyle HorizontalAlign="left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="SUPP_CITY" HeaderText="City" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemStyle HorizontalAlign="left" />
                                                    </asp:BoundField>
                                                </Columns>
                                            </asp:GridView>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:TabPanel>
                                <asp:TabPanel ID="TabPOLines" runat="server" HeaderText="Detail" Visible="False">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlPOReceiveGrid" runat="server" Style="overflow: auto; max-height: 500px; width: auto;"
                                            HorizontalAlign="Left">
                                            <asp:HiddenField ID="HiddenField2" runat="server" />
                                            <asp:GridView ID="GridOpenPODetail" runat="server" Width="100%" DataKeyNames="IFSPurchaseOrderDetailID"
                                                AutoGenerateColumns="False" CssClass="mGrid" AlternatingRowStyle-CssClass="alt"
                                                AutoGenerateSelectButton="True">
                                                <SelectedRowStyle CssClass="srowstyle" />
                                                <Columns>
                                                    <asp:BoundField DataField="IFSPurchaseOrderDetailID" HeaderText="ID" ReadOnly="True"
                                                        Visible="false">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="PONumberOrderNo" HeaderText="PO Number" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemStyle HorizontalAlign="left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="POLineLineNo" HeaderText="PO Line" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemStyle HorizontalAlign="left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="SKUPartNo" HeaderText="SKU" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemStyle HorizontalAlign="left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="SupplierPartNo" HeaderText="Supplier SKU" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemStyle HorizontalAlign="left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="QTYOrderQty" HeaderText="QTY Ordered" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemStyle HorizontalAlign="left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="QTYRemainingQty" HeaderText="Remaining" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemStyle HorizontalAlign="left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="QTYPPicked" HeaderText="Picked" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemStyle HorizontalAlign="left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="POCostPrice" HeaderText="Cost" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemStyle HorizontalAlign="left" />
                                                    </asp:BoundField>
                                                </Columns>
                                            </asp:GridView>
                                        </asp:Panel>

                                    </ContentTemplate>
                                </asp:TabPanel>
                                <asp:TabPanel ID="TabPOLineReceive" runat="server" HeaderText="Receive" Visible="False">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlPOReceive" runat="server" Visible="true">
                                            <asp:HiddenField ID="hdnPOReceiveMasterPartLinkTableID" runat="server" />



                                        
                                            <asp:Table ID="Table3" runat="server" Width="100%">
                                                <asp:TableRow>
                                                    <asp:TableCell ColumnSpan="5" Font-Bold="True" HorizontalAlign="Left">
                        <h1>
                            PO Receive</h1>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow runat="server" ID="TableRow1">
                                                    <asp:TableCell HorizontalAlign="Right">
                                       PO Vendor: 
                                                    </asp:TableCell>
                                                    <asp:TableCell HorizontalAlign="Left">
                                                        <asp:TextBox ID="txtPOLineReceiptVendor" runat="server" ToolTip="PO Vendor" MaxLength="50" ReadOnly="True"></asp:TextBox>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow runat="server" ID="TableRow2">
                                                    <asp:TableCell HorizontalAlign="Right">
                                       PO Number: 
                                                    </asp:TableCell>
                                                    <asp:TableCell HorizontalAlign="Left">
                                                        <asp:TextBox ID="txtPOLineReceiptNumber" runat="server" ToolTip="PO Number" MaxLength="12" ReadOnly="True"></asp:TextBox>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow runat="server" ID="TableRow3">
                                                    <asp:TableCell HorizontalAlign="Right">
                                       PO Line: 
                                                    </asp:TableCell>
                                                    <asp:TableCell HorizontalAlign="Left">
                                                        <asp:TextBox ID="txtPOLineReceiptLine" runat="server" ToolTip="PO Line" MaxLength="4" ReadOnly="True"></asp:TextBox>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow runat="server" ID="TableRow4">
                                                    <asp:TableCell HorizontalAlign="Right">
                                       QTY Ordered: 
                                                    </asp:TableCell>
                                                    <asp:TableCell HorizontalAlign="Left">
                                                        <asp:TextBox ID="txtPOLineQTYOrdered" runat="server" ToolTip="QTY Ordered" ReadOnly="True"></asp:TextBox>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow runat="server" ID="TableRow6">
                                                    <asp:TableCell HorizontalAlign="Right">
                                       QTY Remaining: 
                                                    </asp:TableCell>
                                                    <asp:TableCell HorizontalAlign="Left">
                                                        <asp:TextBox ID="txtPOLineQTYRemaining" runat="server" ToolTip="QTY Remaining" ReadOnly="True"></asp:TextBox>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow runat="server" ID="TableRow5">
                                                    <asp:TableCell HorizontalAlign="Right">
                                       Part Purchase Price: 
                                                    </asp:TableCell>
                                                    <asp:TableCell HorizontalAlign="Left">
                                                        <asp:TextBox ID="txtPOLineReceiptPrice" runat="server" ToolTip="Purchase Price" ReadOnly="True"></asp:TextBox>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow>
                                                    <asp:TableCell VerticalAlign="Top" HorizontalAlign="Right">
                                    Location:  
                                                    </asp:TableCell>
                                                    <asp:TableCell ColumnSpan="3" HorizontalAlign="Left">
                                                        <asp:TextBox ID="IFSToLocationPOLineReceive" runat="server" ClientIDMode="Static"
                                                            BackColor="#A5EBC5" ForeColor="Black"></asp:TextBox>
                                                        <asp:GridView ID="gvLocationPOReceiveParts" runat="server" Width="100%" DataKeyNames="MasterPartsTableIFSLocationStorageID"
                                                            AutoGenerateColumns="False" CssClass="mGrid" AlternatingRowStyle-CssClass="alt"
                                                            AllowPaging="false">
                                                            <SelectedRowStyle CssClass="srowstyle" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="S" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="imgSelectLocation" runat="server" HeaderText="" ImageUrl="~/Images/details_open.png"
                                                                            ToolTip="Select This Location" Width="15px"></asp:ImageButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="MasterPartsTableIFSLocationStorageID" HeaderText="ID"
                                                                    ReadOnly="True" Visible="false">
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="QTY" HeaderText="QTY" ReadOnly="True">
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="IFSLocation" HeaderText="IFSLocation" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemStyle HorizontalAlign="left" />
                                                                </asp:BoundField>
<%--                                                                <asp:BoundField DataField="Quantity" HeaderText="Quantity" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemStyle HorizontalAlign="left" />
                                                                </asp:BoundField>--%>
                                                                <asp:BoundField DataField="GMPPartNumber" HeaderText="GMPPartNumber" ReadOnly="True">
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="GMPPartDescription" HeaderText="GMPPartDescription" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemStyle HorizontalAlign="left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="PartNumber" HeaderText="PartNumber" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemStyle HorizontalAlign="left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Description" HeaderText="Description" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemStyle HorizontalAlign="left" />
                                                                </asp:BoundField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow>
                                                    <asp:TableCell HorizontalAlign="Right">
                                    QTY:  
                                                    </asp:TableCell>
                                                    <asp:TableCell HorizontalAlign="Left">
                                                        <asp:TextBox ID="txtPOReceiveQTY" runat="server" ToolTip="QTY - Must be a positive number"></asp:TextBox>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow>
                                                    <asp:TableCell HorizontalAlign="Right">
                                    Misc Description:
                                                    </asp:TableCell>
                                                    <asp:TableCell HorizontalAlign="Left">
                                                        <asp:TextBox ID="txtPOReceiveDesc" runat="server" ToolTip="Misc Description"></asp:TextBox>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow HorizontalAlign="Left">
                                                    <asp:TableCell>
                                                        <asp:Button ID="btnPOReceiveSave" runat="server" Text="Save" />
                                                        <asp:Button ID="btnPOReceiveCancel" runat="server" Text="Cancel" />
                                                    </asp:TableCell>
                                                    <asp:TableCell HorizontalAlign="Left" ColumnSpan="4">
                                                        <br />
                                                        <asp:Label ID="Label15" runat="server" Text=""></asp:Label>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                            </asp:Table>
                                        </asp:Panel>

                                    </ContentTemplate>
                                </asp:TabPanel>
                            </asp:TabContainer>






                        </asp:Panel>
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>

        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        // Setup Global Variables
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
