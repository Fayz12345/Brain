<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Maint_MasterPartTable_B.aspx.cs" Inherits="GMPI_WebApp.Maintenance.Maint_MasterPartTable_B" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Syncfusion.Shared.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89" Namespace="Syncfusion.Web.UI.WebControls.Shared" TagPrefix="syncfusion" %>
<%@ Register Assembly="Syncfusion.Tools.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89"  Namespace="Syncfusion.Web.UI.WebControls.Tools" TagPrefix="syncfusion" %>


<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="cMC" runat="server">
                
    <asp:Panel ID="pnlUpload" runat="server" Visible="True">
        <div style="background: url(~/Images/hline.gif) repeat-x bottom #F2F2F2; padding: 8px 5px;
            border-bottom: 1px solid #ccc;">
            <asp:FileUpload ID="FileUploadXLS" runat="server" Width="80%" size="100"></asp:FileUpload>&nbsp;&nbsp;
            <asp:Button ID="btnUpload" runat="server" Text="Upload" />
            <asp:Button ID="btnDownload" runat="server" Text="DownLoad" />
            <asp:CheckBox ID="chkRestrict" runat="server" ToolTip="Check if you wish the report to use the Part Number Parameters." />
            <br />
            <asp:RadioButtonList ID="rdUploadType" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Transaction" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Part Numbers"></asp:ListItem>

            </asp:RadioButtonList>
            <br />
            <asp:Label ID="lblMsgDetail" runat="server" Visible="False" Font-Bold="True" ForeColor="#009933"></asp:Label>
        </div>
    </asp:Panel>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
<%--            --%>
            <asp:HiddenField ID="hdnUserName" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnCarrierID" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnClientLocationID" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnManufacturerID" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnModelID" runat="server" ClientIDMode="Static" />

            <asp:HiddenField ID="hdnMasterPartLinkTableID" runat="server" ClientIDMode="Static" />

            <%--<asp:HiddenField ID="hdnColourID" runat="server" ClientIDMode="Static" />--%>
            <asp:TabContainer ID="TabContainer1" runat="server">
                <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="Master Parts">
                    <ContentTemplate>
                        <asp:Panel ID="pnlMainView" runat="server">
                            <asp:Panel ID="Panel3" runat="server" HorizontalAlign="Left" Width="100%">
                                <br />
                                <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" />
                                <asp:Button ID="btnEdit" runat="server" Text="Edit" Visible="False" OnClick="btnEdit_Click" />
                                <asp:Button ID="btnDelete" runat="server" Text="Delete" Visible="False" OnClick="btnDelete_Click" />
                                <asp:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" ConfirmText="Continue with Delete?"
                                    Enabled="True" TargetControlID="btnDelete">
                                </asp:ConfirmButtonExtender>
                                <asp:Label ID="lblRecordTitle" runat="server" Text="Maintenance Part Table"></asp:Label>
                            </asp:Panel>
                            <asp:Panel ID="pnlMainGrid" runat="server" Style="overflow: auto; max-height: 500px;
                            width: auto;" HorizontalAlign="Left">
                                <asp:GridView ID="MainGrid" runat="server" Width="100%" AutoGenerateSelectButton="True"
                                    DataKeyNames="MasterPartsID" AutoGenerateColumns="False" CssClass="mGrid" PagerStyle-CssClass="pgr"
                                    AlternatingRowStyle-CssClass="alt">
                                    <SelectedRowStyle CssClass="srowstyle" />
                                    <Columns>
                                        <asp:BoundField DataField="MasterPartsID" HeaderText="ID" ReadOnly="True" Visible="false">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Name" HeaderText="Category" HeaderStyle-HorizontalAlign="Left">
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Description" HeaderText="Description" HeaderStyle-HorizontalAlign="Left">
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                        </asp:Panel>
                        <asp:Panel ID="pnlAdd" runat="server">
                            <table>
                                <tr>
                                    <td id="AddTemplateHeader" colspan="2" style="border-style: none none solid none;
                                        border-width: thick; border-color: #F5F5F5; text-align: center; vertical-align: middle;">
                                        <h1>
                                            Add Part</h1>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; vertical-align: top;">
                                        Category:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="AddName" runat="server" MaxLength="50"></asp:TextBox><br />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; vertical-align: top;">
                                        Description:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="AddDesc" runat="server" MaxLength="50"></asp:TextBox><br />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="border-style: solid none none none; border-width: thick; border-color: #F5F5F5;
                                        text-align: left; vertical-align: middle;">
                                        <asp:Button ID="AddOK" runat="server" Text="OK" OnClick="AddOK_Click" />
                                        <asp:Button ID="AddCancel" runat="server" Text="Cancel" OnClick="AddCancel_Click1" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="pnlEdit" runat="server">
                            <table>
                                <tr>
                                    <td id="Td1" colspan="2" style="border-style: none none solid none; border-width: thick;
                                        border-color: #F5F5F5; text-align: center; vertical-align: middle;">
                                        <h1>
                                            Edit Part</h1>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; vertical-align: top;" maxlength="50">
                                        Category:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="EditName" runat="server" BackColor="#FFFF66"></asp:TextBox><br />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; vertical-align: top;">
                                        Description:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="EditDesc" runat="server" BackColor="#FFFF66" MaxLength="50"></asp:TextBox><br />
                                        <asp:TextBox ID="EditKeyID" runat="server" BackColor="#FFFF66" ReadOnly="True" Visible="False"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="border-style: solid none none none; border-width: thick; border-color: #FFCC66;
                                        text-align: left; vertical-align: middle;">
                                        <asp:Button ID="EditOK" runat="server" Text="OK" OnClick="EditOK_Click" />
                                        <asp:Button ID="EditCancel" runat="server" Text="Cancel" OnClick="EditCancel_Click" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="Part Numbers">
                    <ContentTemplate>
                        <asp:Panel ID="pnlHeader" runat="server">
                            <asp:Label ID="Label3" runat="server" Text="Location" AssociatedControlID="drpLocationList"></asp:Label>
                            <asp:DropDownList ID="drpLocationList" runat="server" ToolTip="Location where these parts are located"
                                AutoPostBack="true">
                            </asp:DropDownList>
                            <asp:Label ID="Label2" runat="server" Text="Category" AssociatedControlID="drpDropPart"></asp:Label>
                            <asp:DropDownList ID="drpDropPart" runat="server" ToolTip="Drop Part" AutoPostBack="false">
                            </asp:DropDownList>
                            <asp:Label ID="Label1" runat="server" Text="Manufacturer" AssociatedControlID="drpManufacturer"></asp:Label>
                            <asp:DropDownList ID="drpManufacturer" runat="server" ToolTip="Manufacturer" AutoPostBack="false">
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
                            <asp:Button ID="btnRefresh" runat="server" Text="Refresh" />
                            <asp:Button ID="btnAddNew" runat="server" Text="Add" />
                            <asp:Button ID="btnResetPartNumbers" runat="server" Text="Reset Part Numbers Back To Blank" />
                            <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="btnResetPartNumbers"
                                ConfirmText="Are you sure you want to Reset the ENTIRE Part Number Database to NOTHING?">
                            </asp:ConfirmButtonExtender>
                            <br />
                            <%--<asp:Button ID="btnSaveGridData" runat="server" Text="Save" />--%>
                            <br />
                        </asp:Panel>
                        <asp:Panel ID="pnlMainGridPN" runat="server" Style="overflow: auto; max-height: 500px;
                            width: auto;" HorizontalAlign="Left">
                            <asp:HiddenField ID="hdnAllowGridUpdate" runat="server" />
                            <%--<asp:Panel ID="Panel2" runat="server" Width="100%" ScrollBars="Auto" HorizontalAlign="Left">--%>
                            <asp:GridView ID="MainGridPN" runat="server" Width="100%" DataKeyNames="MasterPartsLinkTableID"
                                AutoGenerateColumns="False" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" AllowPaging="true" PageSize="45" OnPageIndexChanging = "OnPaging">
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

                                    <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>

                                            <asp:ImageButton ID="imgEditDetail" runat="server" HeaderText="" ImageUrl="~/Images/edit.gif"
                                                ToolTip="Edit QTY/Unit Price" Width="15px" Enabled="False"></asp:ImageButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="Quantity" HeaderText="QTY" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="AveragePurchasePrice" HeaderText="Average Purchase Price" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="UnitPrice" HeaderText="Price" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="InWarrentyWorkPrice" HeaderText="Warranty Price" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="QTYMin" HeaderText="Min" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="QTYMax" HeaderText="Max" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="QTYReorder" HeaderText="Reorder" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>

                                    <asp:TemplateField HeaderText="Models" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgChangeModel" runat="server" HeaderText="" ImageUrl="~/Images/closed.gif"
                                                ToolTip="Update Model List" Width="15px"></asp:ImageButton>

                                            <asp:Label ID="txtModelDescription" runat="server" Text=""></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </asp:Panel>

                        <asp:Panel ID="pnlAddTransaction" runat="server" Visible="false">
                            <asp:Table ID="Table5" runat="server" Width="100%">

                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="5" Font-Bold="True" HorizontalAlign="Center">
                                    Edit Parts - Add/Adjust inventory<br />
                                    <asp:Label ID="lblEditTransDesc" runat="server" Text="Part Description goes here."></asp:Label>
                                    <br />
                                    <br />
                                    </asp:TableCell>
                                </asp:TableRow>


                                <asp:TableRow>
                                    <asp:TableCell>
                                    MFG. Part #  
                                    </asp:TableCell>
                                    <asp:TableCell>
                                    GMP. Part # 
                                    </asp:TableCell>
                                    <asp:TableCell>
                                    GMP. Part Desc
                                    </asp:TableCell>
                                    <asp:TableCell>
                                    QTY  
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell>
                                    <asp:Label ID="lblMFGPartNumbere" runat="server" Text="MGF Part Number."></asp:Label>  
                                    </asp:TableCell>
                                    <asp:TableCell>
                                    <asp:Label ID="lblGMPPartNumbere" runat="server" Text="IMM Part Number."></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                    <asp:Label ID="lblGMPDesce" runat="server" Text="IMM Description."></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                    <asp:Label ID="lblQTY" runat="server" Text="Quantity."></asp:Label>
                                    </asp:TableCell>

                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                    <br />

                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                    Location  
                                    </asp:TableCell>
                                    <asp:TableCell>
                                            <asp:DropDownList ID="drpLocationEditParts" runat="server">
                                            </asp:DropDownList>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                    Transaction Type  
                                    </asp:TableCell>
                                    <asp:TableCell>
                                            <asp:DropDownList ID="drpTransType" runat="server">
                                            </asp:DropDownList>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                    QTY  
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="txtTransQTY" runat="server" ToolTip="QTY - Must be a positive number"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell>
                                       PO Vendor 
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="txtPOVendor" runat="server" ToolTip="PO Vendor" MaxLength="50"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                       PO Number 
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="txtPONumber" runat="server" ToolTip="PO Number" MaxLength="12"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                       PO Line 
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="txtPOLine" runat="server" ToolTip="PO Line" MaxLength="4"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                       PO Receipt Date 
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="txtPOReceiptDate" runat="server" ToolTip="PO Receipt Date"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtPOReceiptDate"
                                            Format="MM/dd/yyyy">
                                        </asp:CalendarExtender>
                                    </asp:TableCell>
                                </asp:TableRow>



                                <asp:TableRow>
                                    <asp:TableCell>
                                       Part Purchase Price 
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="txtTransPurchasePrice" runat="server" ToolTip="Purchase Price"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                    Misc Description
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="txtTransDesc" runat="server" ToolTip="Misc Description"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow Width="100%">
                                    <asp:TableCell ColumnSpan="2">
                                        <asp:Button ID="btnAddTransactionSave" runat="server" Text="Save" />
                                        <asp:Button ID="btnAddTransactionCancel" runat="server" Text="Cancel" />
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
                                    Class  
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:DropDownList ID="drpClassType" runat="server">
                                        </asp:DropDownList>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell>
                                    MFG. Part #  
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="txtMFGPartNumbere" runat="server" ToolTip="MFG. Part #"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                    GMP. Part # 
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="txtGMPPartNumbere" runat="server" ToolTip="GMP. Part #"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                    GMP. Part Desc
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="txtGMPDesce" runat="server" ToolTip="GMP. Part Desc"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                    Unit Price
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="txtUnitPrice" runat="server" ToolTip="Unit Price"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                    InWarrenty Price
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="txtInWarrentyPrice" runat="server" ToolTip="In Warrenty Unit Price"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                    Min
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="txtInventoryMin" runat="server" ToolTip="Suggested Minimum inventory total."></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                    Max
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="txtInventoryMax" runat="server" ToolTip="Suggested Maximum inventory total"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                    Reorder point
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
                        <asp:Panel ID="pnlAddNewPart" runat="server" Visible="false">
                            <asp:Table ID="Table1" runat="server">
                                <asp:TableHeaderRow>

                                    <asp:TableHeaderCell>
                                    Class                        
                                    </asp:TableHeaderCell>


                                    <asp:TableHeaderCell>
                                    MFG. Part #                        
                                    </asp:TableHeaderCell>
                                    <asp:TableHeaderCell>
                                     GMP. Part #                       
                                    </asp:TableHeaderCell>
                                    <asp:TableHeaderCell>
                                 GMP. Part Desc                   
                                    </asp:TableHeaderCell>
                                    <asp:TableHeaderCell>
                          Models
                                    </asp:TableHeaderCell>
                                </asp:TableHeaderRow>
                                <asp:TableRow Width="100%" ID="EditCells">


                                    <asp:TableCell>
                                         <asp:DropDownList ID="drpAClassType" runat="server"></asp:DropDownList>
                                    </asp:TableCell>

                                    <asp:TableCell Width="15%">
                                        <asp:TextBox ID="txtMFGPartNumber" runat="server" ToolTip="MFG. Part #"></asp:TextBox>
                                    </asp:TableCell>
                                    <asp:TableCell Width="15%">
                                        <asp:TextBox ID="txtGMPPartNumber" runat="server" ToolTip="GMP. Part #"></asp:TextBox>
                                    </asp:TableCell>
                                    <asp:TableCell Width="15%">
                                        <asp:TextBox ID="txtGMPDesc" runat="server" ToolTip="GMP. Part Desc"></asp:TextBox>
                                    </asp:TableCell>
                                    <%--                                    <asp:TableCell Width="5%">
                                        <asp:TextBox ID="txtQTY" runat="server" ToolTip="QTY"></asp:TextBox>
                                    </asp:TableCell>
                                    <asp:TableCell Width="10%">
                                        <asp:TextBox ID="txtUnitPrice" runat="server" ToolTip="Unit Price"></asp:TextBox>
                                    </asp:TableCell>--%>
                                    <asp:TableCell Width="45%">
                                        <asp:Label ID="lblPickedModels" runat="server" Text="" Width="100%"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Button ID="btnSavePartNumbers" runat="server" Text="Save" />
                                        <asp:Button ID="btnCancelSave" runat="server" Text="Cancel" />
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                            <br />
                            <asp:Panel ID="Panel1" runat="server" Width="100%" Height="200px" ScrollBars="Auto">
                                <asp:CheckBoxList ID="chkModels" runat="server" Width="100%" RepeatColumns="6" RepeatDirection="Horizontal"
                                    AutoPostBack="True">
                                </asp:CheckBoxList>
                            </asp:Panel>
                        </asp:Panel>
                        <asp:Panel ID="pnlEditModels" runat="server" Visible="false">
                            <asp:Table ID="Table2" runat="server">
                                <asp:TableHeaderRow>
                                    <asp:TableHeaderCell>
                                    MFG. Part #                        
                                    </asp:TableHeaderCell>
                                    <asp:TableHeaderCell>
                                     GMP. Part #                       
                                    </asp:TableHeaderCell>
                                    <asp:TableHeaderCell>
                                 GMP. Part Desc                   
                                    </asp:TableHeaderCell>
                                    <asp:TableHeaderCell>
                          Models
                                    </asp:TableHeaderCell>
                                </asp:TableHeaderRow>
                                <asp:TableRow Width="100%" ID="TableRow1">
                                    <asp:TableCell Width="15%">
                                        <asp:TextBox ID="EditMFGPart" runat="server" ToolTip="MFG. Part #" ReadOnly="True"></asp:TextBox>
                                    </asp:TableCell>
                                    <asp:TableCell Width="15%">
                                        <asp:TextBox ID="EditGMPPart" runat="server" ToolTip="GMP. Part #" ReadOnly="True"></asp:TextBox>
                                    </asp:TableCell>
                                    <asp:TableCell Width="15%">
                                        <asp:TextBox ID="EditModelDesc" runat="server" ToolTip="GMP. Part Desc" ReadOnly="True"></asp:TextBox>
                                    </asp:TableCell>
                                    <asp:TableCell Width="45%">
                                        <asp:Label ID="Label4" runat="server" Text="" Width="100%"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Button ID="EditModelSave" runat="server" Text="Save" />
                                        <asp:Button ID="EditModelCancel" runat="server" Text="Cancel" />
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                            <br />
                            <asp:Panel ID="Panel4" runat="server" Width="100%" Height="200px" ScrollBars="Auto">
                                <asp:CheckBoxList ID="chkEditModels" runat="server" Width="100%" RepeatColumns="6"
                                    RepeatDirection="Horizontal">
                                </asp:CheckBoxList>
                            </asp:Panel>
                        </asp:Panel>
                        <asp:Panel ID="pnlChangeCategory" runat="server" Visible="false">
                            <asp:Table ID="Table3" runat="server">
                                <asp:TableHeaderRow>
                                    <asp:TableHeaderCell>
                                    MFG. Part #                        
                                    </asp:TableHeaderCell>
                                    <asp:TableHeaderCell>
                                     GMP. Part #                       
                                    </asp:TableHeaderCell>
                                    <asp:TableHeaderCell>
                                 GMP. Part Desc                   
                                    </asp:TableHeaderCell>
                                    <asp:TableHeaderCell>
                          Models
                                    </asp:TableHeaderCell>
                                </asp:TableHeaderRow>
                                <asp:TableRow Width="100%" ID="TableRow2">
                                    <asp:TableCell Width="15%">
                                        <asp:TextBox ID="EditCategoryMFGPart" runat="server" ToolTip="MFG. Part #" ReadOnly="True"></asp:TextBox>
                                    </asp:TableCell>
                                    <asp:TableCell Width="15%">
                                        <asp:TextBox ID="EditCategoryGMPPart" runat="server" ToolTip="GMP. Part #" ReadOnly="True"></asp:TextBox>
                                    </asp:TableCell>
                                    <asp:TableCell Width="15%">
                                        <asp:TextBox ID="EditCategoryDesc" runat="server" ToolTip="GMP. Part Desc" ReadOnly="True"></asp:TextBox>
                                    </asp:TableCell>
                                    <asp:TableCell Width="45%">
                                        <asp:Label ID="Label5" runat="server" Text="" Width="100%"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow Width="100%" ID="TableRow3">
                                    <asp:TableCell ColumnSpan="3">
                                        <asp:Label ID="Label6" runat="server" Text="NEW Category" AssociatedControlID="drpChangeCategoryPart"></asp:Label>
                                        <asp:DropDownList ID="drpChangeCategoryPart" runat="server" ToolTip="Drop Part" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Button ID="EditCategorySave" runat="server" Text="Save" />
                                        <asp:Button ID="EditCategoryCancel" runat="server" Text="Cancel" />
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:Panel>

                        <asp:Panel ID="pnlEditDetail" runat="server" Visible="false" Style="overflow: auto; max-height: 500px;
                            width: auto;" HorizontalAlign="Left">
                            <asp:Table ID="Table4" runat="server">
                                <asp:TableRow>
                                    <asp:TableCell Width="100%">
                                        <asp:Label ID="Label7" runat="server" Text="Unit Price Transaction Detail" Width="100%" Font-Bold="True" Font-Size="Large"></asp:Label>
                                        <br />
                                        <br />
                                        <asp:Label ID="lblPartDescription" runat="server" Text="" Width="100%"></asp:Label>

                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Width="100%">
                                        <asp:GridView ID="GridViewDetail" runat="server" Width="100%" DataKeyNames="MasterPartsLinkTablePriceListID"
                                            AutoGenerateColumns="False" CssClass="mGrid" AlternatingRowStyle-CssClass="alt"
                                            AllowPaging="false">
                                            <SelectedRowStyle CssClass="srowstyle" />
                                            <Columns>
                                                <%--                                                <asp:BoundField DataField="MasterPartsLinkTablePriceListID" HeaderText="ID" ReadOnly="True"
                                                    Visible="false">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Quantity" HeaderText="QTY" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="QTYDispursed" HeaderText="Dispersed" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="UnitPurchasePrice" HeaderText="Unit Purchase Price" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:BoundField>--%>
                                                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hdnMasterPartsLinkTablePriceListID" runat="server" />
<%--                                                        <asp:HiddenField ID="hdnQTY" runat="server" />
                                                        <asp:HiddenField ID="hdnDispursed" runat="server" />
                                                        <asp:HiddenField ID="hdnUnitPrice" runat="server" />--%>
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
                                                <asp:TemplateField HeaderText="QTY" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <ControlStyle Width="30px" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQTY" runat="server" Visible="true"></asp:Label>
                                                        <asp:TextBox ID="txtCorrectedQTY" runat="server" ToolTip="Enter the value the Quantity should be"
                                                            Visible="False" Width="100%" MaxLength="10"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Dispersed" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <ControlStyle Width="30px" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQTYDispursed" runat="server" Visible="true"></asp:Label>
                                                        <asp:TextBox ID="txtCorrectedQTYDispursed" runat="server" ToolTip="Enter the value the Quantity Dispersed should be"
                                                            Visible="False" Width="100%" MaxLength="10"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Unit Purchase Price" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-HorizontalAlign="Left">
                                                    <ControlStyle Width="50px" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUnitPurchasePrice" runat="server" Visible="true"></asp:Label>
                                                        <asp:TextBox ID="txtCorrectedUnitPurchasePrice" runat="server" ToolTip="Enter the Correct Unit Purchase Price"
                                                            Visible="False" Width="100%" MaxLength="10"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <ControlStyle Width="90px" />
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="drpReason" runat="server">
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
<%--                                                <asp:TemplateField HeaderText="Reason for adjustment" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-HorizontalAlign="Left">
                                                    <ControlStyle Width="60%" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAdjustmentReason" runat="server" Visible="true"></asp:Label>
                                                        <asp:TextBox ID="txtAdjustmentReason" runat="server" ToolTip="Enter the reason a correction was required"
                                                            Visible="False" Width="100%" MaxLength="100"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                            </Columns>
                                        </asp:GridView>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <br />
                                        <br />
                                        <asp:Button ID="btnEditDetailSave" runat="server" Text="Update Average" ToolTip="Update Average Price" />
                                        <asp:Button ID="btnEditDetailCancel" runat="server" Text="Close" />
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:Panel>





                        <asp:Panel ID="pnlTransfer" runat="server" Visible="false">
                            <h1>
                                Parts Transfer
                                <br />
                            </h1>


<%--                                <asp:TableRow>
                                    <asp:TableCell>
                                    Location  
                                    </asp:TableCell>
                                    <asp:TableCell>
                                            <asp:DropDownList ID="drpLocationEditPart" runat="server">
                                            </asp:DropDownList>
                                    </asp:TableCell>
                                </asp:TableRow>--%>

                            <asp:Label ID="Label10" runat="server" Text="IFS Location FROM" AssociatedControlID="drpLocationTransferPartFrom"></asp:Label>
                            <asp:DropDownList ID="drpLocationTransferPartFrom" runat="server" ToolTip="IFS Location FROM">
                            </asp:DropDownList>
                            <br />
                            <asp:Label ID="Label10hh" runat="server" Text="IFS Location TO" AssociatedControlID="drpLocationTransferPartTo"></asp:Label>
                            <asp:DropDownList ID="drpLocationTransferPartTo" runat="server" ToolTip="IFS Location TO">
                            </asp:DropDownList>
                            <br />


                            <asp:Label ID="lblTransferPart" runat="server" Text="Transfer Part"></asp:Label><br />
                            <asp:Label ID="lblOriginalWarehouse" runat="server" Text="Original Warehouse"></asp:Label><br />
                            <br />
                            <asp:Label ID="Label8" runat="server" Text="QTY to Transfer:" AssociatedControlID="txtTransferQTY"></asp:Label>
                            <asp:TextBox ID="txtTransferQTY" runat="server" ToolTip="QTY To Transfer"></asp:TextBox>
                            <br />


                            <asp:Label ID="Label10x" runat="server" Text="New Location" AssociatedControlID="drpLocationList2"></asp:Label>
                            <asp:DropDownList ID="drpLocationList2" runat="server" ToolTip="Location where these parts are to Go">
                            </asp:DropDownList>
                            <br />

                            <asp:Label ID="Label11" runat="server" Text="Calculate Average Purchase Price:" AssociatedControlID="chkAveragePurchasePrice"></asp:Label>
                            <asp:CheckBox ID="chkAveragePurchasePrice" runat="server" Checked="True" />
                            <br />

                            <asp:Label ID="Label9" runat="server" Text="Reason for Transfer:" AssociatedControlID="txtTransferReason"></asp:Label>
                            <asp:TextBox ID="txtTransferReason" runat="server" ToolTip="Reason for Transfer:" Width="80%" MaxLength="50"></asp:TextBox>



                            <br />
                            <br />
                            <asp:Label ID="lblTransferMessage" runat="server" Text=""></asp:Label>

                            <br />
                            <asp:Button ID="btnTransferSave" runat="server" Text="GO" ToolTip="Record the transfer." />
                            <asp:Button ID="btnTransferCancel" runat="server" Text="Close" />
                        </asp:Panel>




                        <asp:Panel ID="pnlLocationView" runat="server" Visible="false" Style="overflow: auto; max-height: 500px;
                            width: auto;" HorizontalAlign="Left">
                            <asp:Table ID="Table6" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell Width="100%">
                                        <asp:Label ID="Label12" runat="server" Text="View Locations" Width="100%" Font-Bold="True" Font-Size="Large"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Width="100%">
                                        <asp:GridView ID="gvLoactions" runat="server" Width="100%" DataKeyNames="MasterPartsTableIFSLocationStorageID"
                                            AutoGenerateColumns="False" CssClass="mGrid" AlternatingRowStyle-CssClass="alt"
                                            AllowPaging="false">
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
                                                <asp:BoundField DataField="Quantity" HeaderText="Quantity" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:BoundField>
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
