<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CC_ControlPart.aspx.cs" Inherits="BW_WebApp.CC_ControlPart" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Syncfusion.Shared.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89" Namespace="Syncfusion.Web.UI.WebControls.Shared" TagPrefix="syncfusion" %>
<%@ Register Assembly="Syncfusion.Tools.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89"  Namespace="Syncfusion.Web.UI.WebControls.Tools" TagPrefix="syncfusion" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cMC" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="hdnUserName" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnCCTemplateID" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnCycleInventoryCountIterationHeaderID" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnCycleInventoryCountHeaderPartsID" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnReportThisData" runat="server" ClientIDMode="Static" />
           <%-- <asp:HiddenField ID="hdnReportThisData_Other" runat="server" ClientIDMode="Static" />--%>
<%--            <asp:HiddenField ID="hdnReportThisData_Spread" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnReportThisData_Control" runat="server" ClientIDMode="Static" />--%>

<%--            <asp:HiddenField ID="HdnSpreadQ" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="HdnControlQ" runat="server" ClientIDMode="Static" />--%>
            
            <h1>
               Cycle Count Inventory Dashboard - Parts</h1>
            <br />
            <asp:TabContainer ID="TabContainer1" runat="server">
                <asp:TabPanel ID="TabPanelRuns" runat="server" HeaderText="Counts">
                    <ContentTemplate>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Label ID="lblMainMessage" runat="server" Text=""></asp:Label><br />
                                <asp:ImageButton ID="imgDownloadGrid" runat="server" HeaderText="" ImageUrl="~/Images/arrow_down.png"
                                    ToolTip="Download Tab" Width="25px"></asp:ImageButton>
                                <asp:Label ID="lblRunmasterMessage" runat="server" Text=""></asp:Label>
                                <asp:TabContainer ID="TabRuns" runat="server" AutoPostBack="True">
                                    <asp:TabPanel ID="TabRunsNew" runat="server" HeaderText="New">
                                        <ContentTemplate>
                                            <asp:Button ID="btnRefreshHeadersNew" runat="server" Text="Refresh" Width="100%" />
                                        </ContentTemplate>
                                    </asp:TabPanel>
                                    <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="Active">
                                        <ContentTemplate>
                                            <asp:Button ID="btnRefreshHeadersNew0" runat="server" Text="Refresh" Width="100%" />
                                        </ContentTemplate>
                                    </asp:TabPanel>
                                    <asp:TabPanel ID="TabPanel13" runat="server" HeaderText="Hold">
                                        <ContentTemplate>
                                            <asp:Button ID="btnRefreshHeadersNew1" runat="server" Text="Refresh" Width="100%" />
                                        </ContentTemplate>
                                    </asp:TabPanel>
                                    <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="SYNC Ready">
                                        <ContentTemplate>
                                            <asp:Button ID="btnRefreshHeadersNew2" runat="server" Text="Refresh" Width="100%" />
                                        </ContentTemplate>
                                    </asp:TabPanel>
                                    <asp:TabPanel ID="TabInvalid" runat="server" HeaderText="Closed">
                                        <ContentTemplate>
                                            <asp:Button ID="btnRefreshHeadersNew3" runat="server" Text="Refresh" Width="100%" />
                                        </ContentTemplate>
                                    </asp:TabPanel>
                                    <asp:TabPanel ID="TabOpen" runat="server" HeaderText="Invalid">
                                        <ContentTemplate>
                                            <asp:Button ID="btnRefreshHeadersNew4" runat="server" Text="Refresh" Width="100%" />
                                        </ContentTemplate>
                                    </asp:TabPanel>
                                </asp:TabContainer>
                                <asp:Label ID="lblHeaderRunMessageNew" runat="server" Text=""></asp:Label>
                                <asp:Panel ID="pnlGridViewRunNew" runat="server" Style="width: auto;" HorizontalAlign="Left">
                                    <asp:Panel ID="Panel1" runat="server" Style="overflow: auto; max-height: 500px; width: auto;"
                                        HorizontalAlign="Left">
                                        <asp:GridView ID="grdRunsNew" runat="server" Width="100%" DataKeyNames="CycleInventoryCountHeaderPartsID"
                                            AutoGenerateColumns="False" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                            AllowPaging="false" Font-Size="Smaller" AutoGenerateSelectButton="True">
                                            <SelectedRowStyle CssClass="srowstyle" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                    ItemStyle-Width="90px" ItemStyle-Wrap="False">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgDownLoadRunSummary" runat="server" HeaderText="" ImageUrl="~/Images/arrow_down.png"
                                                            ToolTip="Download Summary" Width="15px"></asp:ImageButton>
                                                        <asp:ImageButton ID="imgDownLoadRunSummaryDetail" runat="server" HeaderText=""
                                                            ImageUrl="~/Images/arrow_down.png" ToolTip="Download Summary Detail" Width="15px">
                                                        </asp:ImageButton>
                                                        <asp:ImageButton ID="imgDownLoadRunDetail" runat="server" HeaderText="" ImageUrl="~/Images/arrow_down.png"
                                                            ToolTip="Download Detail" Width="15px"></asp:ImageButton>
                                                        <asp:ImageButton ID="imgOpenBatchList" runat="server" HeaderText="" ImageUrl="~/Images/closed.gif"
                                                            ToolTip="View/Activate Batches" Width="15px"></asp:ImageButton>
                                                        <asp:ImageButton ID="imgNew" runat="server" HeaderText="" ImageUrl="~/Images/details_close.png"
                                                            ToolTip="Move to New" Width="15px"></asp:ImageButton>
                                                        <asp:ConfirmButtonExtender ID="ConfirmButtonExtendercf2" runat="server" TargetControlID="imgNew"
                                                            ConfirmText="Are you sure you want to move this to New?">
                                                        </asp:ConfirmButtonExtender>
                                                        <asp:ImageButton ID="imgIFSReady" runat="server" HeaderText="" ImageUrl="~/Images/Info.png"
                                                            ToolTip="Move To SYNC Ready" Width="15px"></asp:ImageButton>
                                                        <asp:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" TargetControlID="imgClose"
                                                            ConfirmText="Are you sure you want to make this Run Closed?">
                                                        </asp:ConfirmButtonExtender>
                                                        <asp:ImageButton ID="imgInactivate" runat="server" HeaderText="" ImageUrl="~/Images/delete-pro.bmp"
                                                            ToolTip="Move To Inactive" Width="15px"></asp:ImageButton>
                                                        <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1pn" runat="server" TargetControlID="imgInactivate"
                                                            ConfirmText="Are you sure you want to make this Run inactive?">
                                                        </asp:ConfirmButtonExtender>
                                                        <asp:ImageButton ID="imgClose" runat="server" HeaderText="" ImageUrl="~/Images/close_inline.gif"
                                                            ToolTip="Move To Close" Width="15px"></asp:ImageButton>
                                                        <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1p" runat="server" TargetControlID="imgClose"
                                                            ConfirmText="Are you sure you want to make this Run Closed?">
                                                        </asp:ConfirmButtonExtender>
                                                        <asp:ImageButton ID="imgHold" runat="server" HeaderText="" ImageUrl="~/Images/details_close.png"
                                                            ToolTip="Move to Hold" Width="15px"></asp:ImageButton>
                                                        <asp:ConfirmButtonExtender ID="ConfirmButtonExtender12axyx" runat="server" TargetControlID="imgHold"
                                                            ConfirmText="Are you sure you want to move this to hold?">
                                                        </asp:ConfirmButtonExtender>
                                                        <asp:ImageButton ID="imgactivate" runat="server" HeaderText="" ImageUrl="~/Images/closed.gif"
                                                            ToolTip="Move to Activate" Width="15px"></asp:ImageButton>
                                                        <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1po" runat="server" TargetControlID="imgactivate"
                                                            ConfirmText="Are you sure you want to make this Run Active? It will lock all locations!">
                                                        </asp:ConfirmButtonExtender>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="CycleInventoryCountHeaderPartsID" HeaderText="Run #" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Status" HeaderText="Status" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Name" HeaderText="Name" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Note" HeaderText="Note" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="WareHouse" HeaderText="WareHouse" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="IFSSite" HeaderText="IFSSite" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="UniqueLocations" HeaderText="# Batches" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-ForeColor="#009900">
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="SummarzedLocations" HeaderText="# Keyed Locations" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-ForeColor="#009900">
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="OpenBatches" HeaderText="Open Batches" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-ForeColor="#009900">
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="HoldBatches" HeaderText="Hold Batches" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-ForeColor="#009900">
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="SyncBatches" HeaderText="Sync Batches" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-ForeColor="#009900">
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ClosedBatches" HeaderText="Closed Batches" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-ForeColor="#009900">
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="InvalidBatches" HeaderText="Invalid Batches" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-ForeColor="#009900">
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CreateUser" HeaderText="Create User" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CreateDate" HeaderText="Create Date" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-Wrap="False">
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="LastUpdateUser" HeaderText="LastUpdate User" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="LastUpdateDate" HeaderText="LastUpdate Date" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-Wrap="False">
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                </asp:BoundField>
                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlGridViewRunSpread" runat="server" Style="width: auto;" HorizontalAlign="Left"
                                        Visible="False">
                                        <asp:Label ID="Label4" runat="server" Text="Batches" Font-Bold="True"></asp:Label>
                                        <asp:TabContainer ID="SpreadTabs" runat="server" AutoPostBack="True">
                                            <asp:TabPanel ID="SpreadTabActive" runat="server" HeaderText="Open">
                                                <ContentTemplate>
                                                </ContentTemplate>
                                            </asp:TabPanel>
                                            <asp:TabPanel ID="SpreadTabHold" runat="server" HeaderText="Hold">
                                                <ContentTemplate>
                                                </ContentTemplate>
                                            </asp:TabPanel>
                                            <asp:TabPanel ID="TabPanel5" runat="server" HeaderText="SYNC Ready">
                                                <ContentTemplate>
                                                </ContentTemplate>
                                            </asp:TabPanel>
                                            <asp:TabPanel ID="SpreadTabClosed" runat="server" HeaderText="Closed">
                                                <ContentTemplate>
                                                </ContentTemplate>
                                            </asp:TabPanel>
                                            <asp:TabPanel ID="SpreadTabInactive" runat="server" HeaderText="Invalid">
                                                <ContentTemplate>
                                                </ContentTemplate>
                                            </asp:TabPanel>
                                            <asp:TabPanel ID="SpreadTabData" runat="server" HeaderText="Spread">
                                                <ContentTemplate>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:ImageButton ID="ImgdownloadTab01" runat="server" HeaderText="" ImageUrl="~/Images/arrow_down.png"
                                                                    ToolTip="Download Tab" Width="25px"></asp:ImageButton>
                                                            </td>
                                                            <%--                                                            <td>
                                                                <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Selected="True" Text="Summary" Value="Y"></asp:ListItem>
                                                                    <asp:ListItem Selected="False" Text="Detail" Value="N"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>--%>
                                                            <td>
                                                                <asp:Button ID="btnRefreshSpreadData" runat="server" Text="Refresh" />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblSpreadQuerry" runat="server" Text="" Width="100%"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <asp:Panel ID="Panel3" runat="server" Style="overflow: auto; max-height: 500px; width: auto;"
                                                        HorizontalAlign="Left">
                                                        <asp:GridView ID="gvRunSpread" runat="server" Width="100%" DataKeyNames="CycleInventoryCountHeaderPartsID"
                                                            AutoGenerateColumns="False" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                            AllowPaging="false" Font-Size="Smaller" EnableViewState="False">
                                                            <SelectedRowStyle CssClass="srowstyle" />
                                                            <Columns>
                                                                <asp:BoundField DataField="CycleInventoryCountHeaderPartsID" HeaderText="Run #" HeaderStyle-HorizontalAlign="Left" ItemStyle-Wrap="False">
                                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Status" HeaderText="Status" HeaderStyle-HorizontalAlign="Left" ItemStyle-Wrap="False">
                                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Name" HeaderText="Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-Wrap="False">
                                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Note" HeaderText="Note" HeaderStyle-HorizontalAlign="Left" ItemStyle-Wrap="False">
                                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                                </asp:BoundField>

                                                                <asp:BoundField DataField="PartsCount" HeaderText="# Parts" HeaderStyle-HorizontalAlign="Left"
                                                                    ItemStyle-ForeColor="#009900" ItemStyle-Wrap="False">
                                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                                </asp:BoundField>


                                                                <asp:BoundField DataField="IFSSite" HeaderText="IFSSite" HeaderStyle-HorizontalAlign="Left"
                                                                    ItemStyle-ForeColor="#009900" ItemStyle-Wrap="False">
                                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="IFSProject" HeaderText="IFSProject" HeaderStyle-HorizontalAlign="Left"
                                                                    ItemStyle-ForeColor="#009900" ItemStyle-Wrap="False">
                                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Partnumber" HeaderText="SKU" HeaderStyle-HorizontalAlign="Left"
                                                                    ItemStyle-ForeColor="#009900" ItemStyle-Wrap="False">
                                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="IFSLocation" HeaderText="Location" HeaderStyle-HorizontalAlign="Left"
                                                                    ItemStyle-ForeColor="#009900" ItemStyle-Wrap="False">
                                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="IFSZoneLocationParts" HeaderText="IFSZoneLocationPart"
                                                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="#009900" ItemStyle-Wrap="False">
                                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="IsFrozen" HeaderText="IsFrozen" HeaderStyle-HorizontalAlign="Left"
                                                                    ItemStyle-ForeColor="#009900" ItemStyle-Wrap="False">
                                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Freq" HeaderText="Freq" HeaderStyle-HorizontalAlign="Left"
                                                                    ItemStyle-ForeColor="#009900" ItemStyle-Wrap="False">
                                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Good" HeaderText="Good" HeaderStyle-HorizontalAlign="Left"
                                                                    ItemStyle-ForeColor="#009900" ItemStyle-Wrap="False">
                                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Warnings" HeaderText="Warnings" HeaderStyle-HorizontalAlign="Left"
                                                                    ItemStyle-ForeColor="#009900" ItemStyle-Wrap="False">
                                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Errors" HeaderText="Errors" HeaderStyle-HorizontalAlign="Left"
                                                                    ItemStyle-ForeColor="#009900" ItemStyle-Wrap="False">
                                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="CountControl" HeaderText="CountControl" HeaderStyle-HorizontalAlign="Left"
                                                                    ItemStyle-ForeColor="#009900" ItemStyle-Wrap="False">
                                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="CountBatch" HeaderText="CountBatch" HeaderStyle-HorizontalAlign="Left"
                                                                    ItemStyle-ForeColor="#009900" ItemStyle-Wrap="False">
                                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="CountVariance" HeaderText="CountVariance" HeaderStyle-HorizontalAlign="Left"
                                                                    ItemStyle-ForeColor="#009900" ItemStyle-Wrap="False">
                                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ValueControl" HeaderText="ValueControl" HeaderStyle-HorizontalAlign="Left"
                                                                    ItemStyle-ForeColor="#009900" ItemStyle-Wrap="False">
                                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ValueBatch" HeaderText="ValueBatch" HeaderStyle-HorizontalAlign="Left"
                                                                    ItemStyle-ForeColor="#009900" ItemStyle-Wrap="False">
                                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ValueVariance" HeaderText="ValueVariance" HeaderStyle-HorizontalAlign="Left"
                                                                    ItemStyle-ForeColor="#009900" ItemStyle-Wrap="False">
                                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="CreateUser" HeaderText="Create User" HeaderStyle-HorizontalAlign="Left" ItemStyle-Wrap="False">
                                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="CreateDate" HeaderText="Create Date" HeaderStyle-HorizontalAlign="Left"
                                                                    ItemStyle-Wrap="False">
                                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="LastUpdateUser" HeaderText="LastUpdate User" HeaderStyle-HorizontalAlign="Left" ItemStyle-Wrap="False">
                                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="LastUpdateDate" HeaderText="LastUpdate Date" HeaderStyle-HorizontalAlign="Left" ItemStyle-Wrap="False">
                                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                                </asp:BoundField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </asp:Panel>
                                                </ContentTemplate>
                                            </asp:TabPanel>
                                            <asp:TabPanel ID="TabPanel6" runat="server" HeaderText="Control">
                                                <ContentTemplate>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:ImageButton ID="ImageButton2" runat="server" HeaderText="" ImageUrl="~/Images/arrow_down.png"
                                                                    ToolTip="Download Tab" Width="25px"></asp:ImageButton>
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="ControlType" runat="server" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Selected="True" Text="Summary" Value="Y"></asp:ListItem>
                                                                    <asp:ListItem Selected="False" Text="Detail" Value="N"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="btnRefreshControlData" runat="server" Text="Refresh" />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblControlQuerry" runat="server" Text="" Width="100%"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <asp:Panel ID="Panel6" runat="server" Style="overflow: auto; max-height: 500px; width: auto;"
                                                         HorizontalAlign="Left">
                                                        <asp:GridView ID="grdControl" runat="server" Width="100%" DataKeyNames="CycleInventoryCountControlDetailPartsID"
                                                            AutoGenerateColumns="False" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                            AllowPaging="false" Font-Size="Smaller" AutoGenerateSelectButton="True" EnableViewState="False">
                                                            <SelectedRowStyle CssClass="srowstyle" />
                                                            <Columns>
                                                                <asp:BoundField DataField="CycleInventoryCountHeaderPartsID" HeaderText="Run #" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="CycleInventoryCountControlDetailPartsID" HeaderText="ID" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="MasterPartsTableIFSLocationStorageID" HeaderText="RDID" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="CountType" HeaderText="CountType" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Quantity" HeaderText="Quantity" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="IFSLocation" HeaderText="IFSLocation" HeaderStyle-HorizontalAlign="Left"
                                                                    ItemStyle-ForeColor="#009900">
                                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="IFSSite" HeaderText="IFSSite" HeaderStyle-HorizontalAlign="Left"
                                                                    ItemStyle-ForeColor="#009900">
                                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="IFSProject" HeaderText="IFSProject" HeaderStyle-HorizontalAlign="Left"
                                                                    ItemStyle-ForeColor="#009900">
                                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Partnumber" HeaderText="Partnumber" HeaderStyle-HorizontalAlign="Left"
                                                                    ItemStyle-ForeColor="#009900">
                                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Value" HeaderText="Value" HeaderStyle-HorizontalAlign="Left"
                                                                    ItemStyle-ForeColor="#009900">
                                                                    <ItemStyle HorizontalAlign="right" VerticalAlign="Top" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="StatusMessage" HeaderText="StatusMessage" HeaderStyle-HorizontalAlign="Left"
                                                                    ItemStyle-ForeColor="#009900">
                                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="CreateUser" HeaderText="Create User" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="CreateDate" HeaderText="Create Date" HeaderStyle-HorizontalAlign="Left"
                                                                    ItemStyle-Wrap="False">
                                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="LastUpdateUser" HeaderText="LastUpdate User" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="LastUpdateDate" HeaderText="LastUpdate Date" HeaderStyle-HorizontalAlign="Left"
                                                                    ItemStyle-Wrap="False">
                                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                                </asp:BoundField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </asp:Panel>
                                                </ContentTemplate>
                                            </asp:TabPanel>
                                            <asp:TabPanel ID="TabScanResults" runat="server" HeaderText="Scan Results">
                                                <ContentTemplate>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:ImageButton ID="ImageButton3" runat="server" HeaderText="" ImageUrl="~/Images/arrow_down.png"
                                                                    ToolTip="Download Tab" Width="25px"></asp:ImageButton>
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="ResultType" runat="server" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Selected="True" Text="Summary" Value="4"></asp:ListItem>
                                                                    <asp:ListItem Selected="False" Text="Summary Location" Value="3"></asp:ListItem>
                                                                    <asp:ListItem Selected="False" Text="Summay Partnumber" Value="2"></asp:ListItem>
                                                                    <asp:ListItem Selected="False" Text="Detail" Value="1"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="btnRefreshScanResults" runat="server" Text="Refresh" />
                                                            </td>
                                                            <td>


                                                                <asp:CheckBox ID="ShowLevel1" runat="server" Text="Level 1" Checked="True" />
                                                                <asp:CheckBox ID="ShowLevel2" runat="server" Text="Level 2" />
                                                                <asp:CheckBox ID="ShowLevel3" runat="server" Text="Level 3" />



                                                             <%--       <asp:ListItem Selected="True" Text="Level 1" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Selected="False" Text="Level 2" Value="2"></asp:ListItem>
                                                                    <asp:ListItem Selected="False" Text="Level 3" Value="3"></asp:ListItem>
                                                                </asp:CheckBoxList>--%>
                                                            </td>

                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                <asp:Label ID="lblResultQuerry" runat="server" Text="" Width="100%"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <asp:Panel ID="Panel7" runat="server" Style="overflow: auto; max-height: 500px; width: auto;"
                                                        HorizontalAlign="Left">
                                                        <asp:GridView ID="grdResultData" runat="server" Width="100%" DataKeyNames="CycleInventoryCountControlDetailPartsID"
                                                            AutoGenerateColumns="False" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                            AllowPaging="false" Font-Size="Smaller" AutoGenerateSelectButton="True" EnableViewState="False">
                                                            <SelectedRowStyle CssClass="srowstyle" />
                                                            <Columns>
                                                                <asp:BoundField DataField="CycleInventoryCountHeaderPartsID" HeaderText="Run #" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="CycleInventoryCountControlDetailPartsID" HeaderText="C_ID" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="CycleCountInventoryCountID" HeaderText="S_ID" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                                </asp:BoundField>

                                                                <asp:BoundField DataField="MasterPartsTableIFSLocationStorageID" HeaderText="RDID" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                                </asp:BoundField>


                                                                <asp:BoundField DataField="Name" HeaderText="Name" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Note" HeaderText="Note" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Batch" HeaderText="Batch" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Batch_Status" HeaderText="Batch_Status" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="isBatchLocked" HeaderText="isBatchLocked" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                                </asp:BoundField>

                                                                <asp:BoundField DataField="CountType" HeaderText="CountType" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                                </asp:BoundField>

                                                                <asp:BoundField DataField="Scan_StatusMessage" HeaderText="StatusMessage" HeaderStyle-HorizontalAlign="Left"
                                                                    ItemStyle-ForeColor="#009900">
                                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                                </asp:BoundField>

                                                                <asp:BoundField DataField="Quantity" HeaderText="Quantity" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemStyle HorizontalAlign="right" VerticalAlign="Top" />
                                                                </asp:BoundField>

                                                                <asp:BoundField DataField="ValueControl" HeaderText="ValueControl" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemStyle HorizontalAlign="right" VerticalAlign="Top" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ValueScan" HeaderText="ValueScan" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemStyle HorizontalAlign="right" VerticalAlign="Top" />
                                                                </asp:BoundField>

                                                                <asp:BoundField DataField="Variance" HeaderText="Variance" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemStyle HorizontalAlign="right" VerticalAlign="Top" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="VariancePercent" HeaderText="VariancePercent" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemStyle HorizontalAlign="right" VerticalAlign="Top" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ScanFound" HeaderText="ScanFound" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemStyle HorizontalAlign="right" VerticalAlign="Top" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ControlFound" HeaderText="ControlFound" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemStyle HorizontalAlign="right" VerticalAlign="Top" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="OKSite" HeaderText="OKSite" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemStyle HorizontalAlign="right" VerticalAlign="Top" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="OKProject" HeaderText="OKProject" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemStyle HorizontalAlign="right" VerticalAlign="Top" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="OKSKU" HeaderText="OKSKU" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemStyle HorizontalAlign="right" VerticalAlign="Top" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="OKLocation" HeaderText="OKLocation" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemStyle HorizontalAlign="right" VerticalAlign="Top" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="IFSLocation" HeaderText="IFSLocation" HeaderStyle-HorizontalAlign="Left"
                                                                    ItemStyle-ForeColor="#009900">
                                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="IFSSite" HeaderText="IFSSite" HeaderStyle-HorizontalAlign="Left"
                                                                    ItemStyle-ForeColor="#009900">
                                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="IFSProject" HeaderText="IFSProject" HeaderStyle-HorizontalAlign="Left"
                                                                    ItemStyle-ForeColor="#009900">
                                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="IFSZoneLocation" HeaderText="IFSZoneLocation" HeaderStyle-HorizontalAlign="Left"
                                                                    ItemStyle-ForeColor="#009900">
                                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Partnumber" HeaderText="Partnumber" HeaderStyle-HorizontalAlign="Left"
                                                                    ItemStyle-ForeColor="#009900">
                                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="CreateUser" HeaderText="Create User" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="CreateDate" HeaderText="Create Date" HeaderStyle-HorizontalAlign="Left"
                                                                    ItemStyle-Wrap="False">
                                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="LastUpdateUser" HeaderText="LastUpdate User" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="LastUpdateDate" HeaderText="LastUpdate Date" HeaderStyle-HorizontalAlign="Left"
                                                                    ItemStyle-Wrap="False">
                                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                                </asp:BoundField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </asp:Panel>
                                                </ContentTemplate>
                                            </asp:TabPanel>
                                        </asp:TabContainer>

                                        <asp:Panel ID="pnlSpreadOther" runat="server" Style="width: auto;" HorizontalAlign="Left"
                                            Visible="False">
                                            <asp:Table ID="Table1" runat="server">
                                                <asp:TableRow ID="rowReceivedDate">
                                                    <asp:TableCell VerticalAlign="Top">
                                                        <asp:ImageButton ID="ImageButton1" runat="server" HeaderText="" ImageUrl="~/Images/arrow_down.png"
                                                            ToolTip="Download Tab" Width="25px"></asp:ImageButton>
                                                    </asp:TableCell>

                                                    <asp:TableCell VerticalAlign="Top" Wrap="False">
                                                    <asp:Button ID="btnSpreadOtherRefresh" runat="server" Text="Refresh" Width="100%" />
                                                    </asp:TableCell>

                                                    <asp:TableCell VerticalAlign="Top" Wrap="False">
                                                        <asp:CheckBox ID="chkReceived" runat="server" ToolTip="If checked, all Batches will be reported, scanned between the dates given." />
                                                    </asp:TableCell>
                                                    <asp:TableCell VerticalAlign="Top">
                                            Scan Begin/End Date
                                                    </asp:TableCell>
                                                    <asp:TableCell VerticalAlign="Top" Wrap="False">
                                                        <syncfusion:DropDownCalendarControl ID="BeginDate" runat="server" CustomFormat="MM/dd/yyyy HH:mm"
                                                            Format="CustomString">
                                                        </syncfusion:DropDownCalendarControl>
                                                    </asp:TableCell>
                                                    <asp:TableCell VerticalAlign="Top">
                                                        <syncfusion:DropDownCalendarControl ID="EndDate" runat="server" CustomFormat="MM/dd/yyyy hh:mm:ss"
                                                            Format="CustomString">
                                                        </syncfusion:DropDownCalendarControl>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow ID="TableRow1">
                                                    <asp:TableCell VerticalAlign="Top" ColumnSpan="7">
                                                    <asp:Label ID="lblOtherQuerry" runat="server" Text="" Width="100%"></asp:Label><br />
                                                    </asp:TableCell>
                                                </asp:TableRow>

                                            </asp:Table>
                                            <asp:Panel ID="pnlOther" runat="server" Style="overflow: auto; max-height: 500px;
                                                width: auto;" HorizontalAlign="Left">
                                                <asp:GridView ID="gvSpreadOther" runat="server" Width="100%" DataKeyNames="Batch"
                                                    AutoGenerateColumns="False" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                    AllowPaging="false" Font-Size="Smaller">
                                                    <SelectedRowStyle CssClass="srowstyle" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="imgInvalidate" runat="server" HeaderText="" ImageUrl="~/Images/delete-pro.bmp"
                                                                    ToolTip="Invalidate." Width="15px"></asp:ImageButton>
                                                                <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="imgInvalidate"
                                                                    ConfirmText="Are you sure you want to Invalidate this batch?">
                                                                </asp:ConfirmButtonExtender>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="imgDownload" runat="server" HeaderText="" ImageUrl="~/Images/arrow_down.png"
                                                                    ToolTip="Download this batch" Width="15px"></asp:ImageButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="imgOpen" runat="server" HeaderText="" ImageUrl="~/Images/Files.png"
                                                                    ToolTip="Return to Open Status." Width="15px"></asp:ImageButton>
                                                                <asp:ConfirmButtonExtender ID="ConfirmButtonExtender11" runat="server" TargetControlID="imgOpen"
                                                                    ConfirmText="Are you sure you want to return this batch to open?">
                                                                </asp:ConfirmButtonExtender>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="imgClean" runat="server" HeaderText="" ImageUrl="~/Images/Clean.PNG"
                                                                    ToolTip="Clean" Width="15px"></asp:ImageButton>
                                                                <asp:ConfirmButtonExtender ID="ConfirmButtonExtender12xx" runat="server" TargetControlID="imgClean"
                                                                    ConfirmText="Are you sure you want to Clean this batch?">
                                                                </asp:ConfirmButtonExtender>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="imgHold" runat="server" HeaderText="" ImageUrl="~/Images/thumb_up.gif"
                                                                    ToolTip="Move to Hold" Width="15px"></asp:ImageButton>
                                                                <asp:ConfirmButtonExtender ID="ConfirmButtonExtender12ayx" runat="server" TargetControlID="imgHold"
                                                                    ConfirmText="Are you sure you want to move this batch to hold?">
                                                                </asp:ConfirmButtonExtender>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="imgClose" runat="server" HeaderText="" ImageUrl="~/Images/Folders.gif"
                                                                    ToolTip="Move to Closed" Width="15px"></asp:ImageButton>
                                                                <asp:ConfirmButtonExtender ID="ConfirmButtonExtender12byx" runat="server" TargetControlID="imgClose"
                                                                    ConfirmText="Are you sure you want to move this batch to Closed?">
                                                                </asp:ConfirmButtonExtender>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="imgSync" runat="server" HeaderText="" ImageUrl="~/Images/closed.gif"
                                                                    ToolTip="Move to SYnc Ready" Width="15px"></asp:ImageButton>
                                                                <asp:ConfirmButtonExtender ID="ConfirmButtonExtender12cyx" runat="server" TargetControlID="imgSync"
                                                                    ConfirmText="Are you sure you want to move this batch to Sync?">
                                                                </asp:ConfirmButtonExtender>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="imgLock" runat="server" HeaderText="" ImageUrl="~/Images/lock.png"
                                                                    ToolTip="Lock Batch" Width="15px"></asp:ImageButton>
                                                                <asp:ConfirmButtonExtender ID="ConfirmButtonExtender12dyx" runat="server" TargetControlID="imgLock"
                                                                    ConfirmText="Are you sure you want to Lock this batch?">
                                                                </asp:ConfirmButtonExtender>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="CountType" HeaderText="CountType" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemStyle HorizontalAlign="left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Batch" HeaderText="Batch" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemStyle HorizontalAlign="left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="freq" HeaderText="#" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Good" HeaderText="G" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-ForeColor="#009900">
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Warnings" HeaderText="W" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-ForeColor="#996633">
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Errors" HeaderText="E" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-ForeColor="#CC0000">
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="DDS" HeaderText="DD" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-ForeColor="#CC0000">
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="NumLoc" HeaderText="Loc" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="NumSite" HeaderText="Site" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="NumProject" HeaderText="Proj" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Locked" HeaderText="L" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="UnLocked" HeaderText="UL" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Kitted" HeaderText="K" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="UnKitted" HeaderText="UK" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="GEN" HeaderText="GEN" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-ForeColor="Blue">
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="New" HeaderText="New" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-ForeColor="Blue">
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="CPA" HeaderText="CPA" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-ForeColor="Blue">
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="CPB" HeaderText="CPB" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-ForeColor="Blue">
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="CPC" HeaderText="CPC" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-ForeColor="Blue">
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="PTG" HeaderText="PTG" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-ForeColor="Blue">
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="PTC" HeaderText="PTC" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-ForeColor="Blue">
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="PWR" HeaderText="PWR" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-ForeColor="Blue">
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="BER" HeaderText="BER" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-ForeColor="Blue">
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="BOA" HeaderText="BOA" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-ForeColor="Blue">
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="REC" HeaderText="REC" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-ForeColor="Blue">
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="C12" HeaderText="C12" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-ForeColor="Blue">
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="C13" HeaderText="C13" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-ForeColor="Blue">
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="C14" HeaderText="C14" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-ForeColor="Blue">
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="CreateUser" HeaderText="Create User" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemStyle HorizontalAlign="left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="StartDate" HeaderText="Start Date" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-Wrap="False">
                                                            <ItemStyle HorizontalAlign="left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="EndDate" HeaderText="End Date" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-Wrap="False">
                                                            <ItemStyle HorizontalAlign="left" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                </asp:GridView>
                                            </asp:Panel>
                                        </asp:Panel>





                                    </asp:Panel>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>


                    </ContentTemplate>
                </asp:TabPanel>

                <asp:TabPanel ID="TabTemplates" runat="server" HeaderText="Templates">
                    <ContentTemplate>
                    <br />
                        <asp:TabContainer ID="TabContainer2" runat="server" Width="100%">
                            <asp:TabPanel ID="TabPanel3" runat="server" HeaderText="Active">
                                <ContentTemplate>
                                    <asp:Label ID="lblHeaderTemplateMessage" runat="server" Text=""></asp:Label>
                                    <asp:Button ID="btnRefreshTemplateHeaders" runat="server" Text="Refresh" Width="100%" />
                                    <asp:Panel ID="Panel4" runat="server" Style="overflow: auto; max-height: 500px;
                                        width: auto;" HorizontalAlign="Left">
                                         <asp:ImageButton ID="PrintTemplateTabActive" runat="server" HeaderText="" ImageUrl="~/Images/arrow_down.png" ToolTip="Download Tab" Width="25px"></asp:ImageButton>
                                        <asp:GridView ID="grdTemplateHeaders" runat="server" Width="100%" DataKeyNames="CycleInventoryCountTemplateHeaderPartsID"
                                            AutoGenerateColumns="False" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                            AllowPaging="false" Font-Size="Smaller">
                                            <SelectedRowStyle CssClass="srowstyle" />
                                            <Columns>

                                                <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ImageEdit" runat="server" HeaderText="" ImageUrl="~/Images/expand.gif"
                                                            ToolTip="Edit" Width="15px"></asp:ImageButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgDownLoadTemplateSummary" runat="server" HeaderText="" ImageUrl="~/Images/arrow_down.png"
                                                            ToolTip="Download Summary" Width="15px"></asp:ImageButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgDownLoadTemplateSummaryDetail" runat="server" HeaderText=""
                                                            ImageUrl="~/Images/arrow_down.png" ToolTip="Download Summary Detail" Width="15px">
                                                        </asp:ImageButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgDownLoadTemplateDetail" runat="server" HeaderText="" ImageUrl="~/Images/arrow_down.png"
                                                            ToolTip="Download Detail" Width="15px"></asp:ImageButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>                                                                                            

                                                <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgInactivate" runat="server" HeaderText="" ImageUrl="~/Images/delete-pro.bmp"
                                                            ToolTip="Inactivate." Width="15px"></asp:ImageButton>
                                                        <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="imgInactivate"
                                                            ConfirmText="Are you sure you want to make this template inactive?">
                                                        </asp:ConfirmButtonExtender>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgloadRunData" runat="server" HeaderText="" ImageUrl="~/Images/closed.gif"
                                                            ToolTip="Generate Run Cycle" Width="15px"></asp:ImageButton>
                                                        <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1zz" runat="server" TargetControlID="imgloadRunData"
                                                            ConfirmText="Are you sure you want to generate a Run Cycle?">
                                                        </asp:ConfirmButtonExtender>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Status" HeaderText="Status" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Name" HeaderText="Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="WareHouse" HeaderText="WareHouse" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="IFSSite" HeaderText="IFSSite" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="IFSLocation" HeaderText="IFSLocation" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PartNumber" HeaderText="PartNumbers" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Note" HeaderText="Note" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CreateUser" HeaderText="Create User" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CreateDate" HeaderText="Create Date" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-Wrap="False" ItemStyle-VerticalAlign="Top">
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="LastUpdateUser" HeaderText="LastUpdate User" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="LastUpdateDate" HeaderText="LastUpdate Date" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-Wrap="False" ItemStyle-VerticalAlign="Top">
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:BoundField>

                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:TabPanel>
                            <asp:TabPanel ID="TabPanel4" runat="server" HeaderText="Inactive">
                                <ContentTemplate>
                                   <asp:Label ID="lblHeaderTemplateMessageInactive" runat="server" Text=""></asp:Label>
                                    <asp:Button ID="btnRefreshTemplateHeadersInactive" runat="server" Text="Refresh" Width="100%" />
                                    <asp:Panel ID="Panel5" runat="server" Style="overflow: auto; max-height: 500px;
                                        width: auto;" HorizontalAlign="Left">
                                        <asp:ImageButton ID="PrintTemplateTabInActive" runat="server" HeaderText="" ImageUrl="~/Images/arrow_down.png" ToolTip="Download Tab" Width="25px"></asp:ImageButton>
                                        <asp:GridView ID="grdTemplateHeadersInactive" runat="server" Width="100%" DataKeyNames="CycleInventoryCountTemplateHeaderPartsID"
                                            AutoGenerateColumns="False" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                            AllowPaging="false" Font-Size="Smaller" EnableViewState="False">
                                            <SelectedRowStyle CssClass="srowstyle" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgDownLoadLocations" runat="server" HeaderText="" ImageUrl="~/Images/arrow_down.png"
                                                            ToolTip="Download Location List" Width="15px"></asp:ImageButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgActivate" runat="server" HeaderText="" ImageUrl="~/Images/thumb_up.gif"
                                                            ToolTip="Activate" Width="15px"></asp:ImageButton>
                                                        <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="imgActivate"
                                                            ConfirmText="Are you sure you want to activate this Template?">
                                                        </asp:ConfirmButtonExtender>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgloadRunData" runat="server" HeaderText="" ImageUrl="~/Images/ADD.BMP"
                                                            ToolTip="Generate Run Cycle" Width="15px"></asp:ImageButton>
                                                        <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1zz" runat="server" TargetControlID="imgloadRunData"
                                                            ConfirmText="Are you sure you want to generate a Run Cycle?">
                                                        </asp:ConfirmButtonExtender>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Status" HeaderText="Status" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Name" HeaderText="Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="WareHouse" HeaderText="WareHouse" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="IFSSite" HeaderText="IFSSite" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="IFSLocation" HeaderText="IFSLocation" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PartNumber" HeaderText="PartNumbers" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Note" HeaderText="Note" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CreateUser" HeaderText="Create User" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CreateDate" HeaderText="Create Date" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-Wrap="False" ItemStyle-VerticalAlign="Top">
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="LastUpdateUser" HeaderText="LastUpdate User" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="LastUpdateDate" HeaderText="LastUpdate Date" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-Wrap="False" ItemStyle-VerticalAlign="Top">
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:BoundField>

                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:TabPanel>
                            <asp:TabPanel ID="TabDIDMFromSite" runat="server" HeaderText="Add/Edit Template"
                                ToolTip="Add/Edit Template">
                                <ContentTemplate>
                                    Select by site.<br />
                                    <br />
                                    <asp:Panel ID="Panel2" runat="server" Style="overflow: auto; max-height: 700px; min-height: 360px;"
                                        HorizontalAlign="Left" Width="100%">
                                        <table id="Table4" runat="server" width="100%">
                                            <tr>
                                                <td align="right" valign="top">
                                                    <asp:Label ID="Label1" runat="server" Text="Status:"></asp:Label>
                                                </td>
                                                <td valign="top">
                                                    <asp:DropDownList ID="drpTemplateStatus" runat="server" ToolTip="Template Status" BackColor="#CCFFFF" ForeColor="Black">
                                                        <asp:ListItem Text="Active" Selected="True" Value="Active"></asp:ListItem>
                                                        <asp:ListItem Text="Inactive" Value="Inactive"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" valign="top">
                                                    <asp:Label ID="Label2" runat="server" Text="Name:"></asp:Label>
                                                </td>
                                                <td valign="top">
                                                    <asp:TextBox ID="txtTemplateName" runat="server" MaxLength="10" BackColor="#CCFFFF" ForeColor="Black"></asp:TextBox>

                                                    <asp:Button ID="btnRefreshAddEditUpdate" runat="server" Text="Refresh" />

                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" valign="top">
                                                    <asp:Label ID="Label3" runat="server" Text="Note:"></asp:Label>
                                                </td>
                                                <td valign="top">
                                                    <asp:TextBox ID="txtTemplateNote" runat="server" TextMode="MultiLine" Rows="4" Width="98%"
                                                        BackColor="#CCFFFF" ForeColor="Black"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label5" runat="server" Text="Warehouse"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:DropDownList ID="drpLocationList" runat="server" ToolTip="Warehouse to count." BackColor="#CCFFFF" ForeColor="Black"
                                                        Font-Size="Larger" AutoPostBack="false">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="20%" align="right" valign="top">
                                                    Site:
                                                </td>
                                                <td align="left" valign="top">
                                                    <asp:DropDownList ID="drpMoveIFSSite" runat="server" BackColor="#CCFFFF" ForeColor="Black"
                                                        Font-Size="Larger" AutoPostBack="false">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                 <td align="right" valign="top">
                                                    Part Number:
                                                </td>
                                                <td>
                                                    <asp:Panel ID="Panel10" runat="server" Style="overflow: auto;" HorizontalAlign="Left" Width="100%">
                                                        <table id="Table9" runat="server" width="100%">
                                                            <tr>
                                                                <td>
                                                                    Paste list, (Wildcards or a list of Comma delimited part numbers)
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="border-style: solid none none none; border-width: thin; border-color: #FFCC66;
                                                                    text-align: left; vertical-align: top;">
                                                                    <asp:RadioButtonList ID="PasteDeliminator" runat="server" RepeatDirection="Horizontal">
                                                                        <asp:ListItem Text="Excel" Value="Excel" Selected="True"> </asp:ListItem>
                                                                        <asp:ListItem Text="Comma" Value="Comma"></asp:ListItem>
                                                                        <asp:ListItem Text="Space" Value="Space"></asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="border-style: solid none none none; border-width: thin; border-color: #FFCC66;
                                                                    text-align: left; vertical-align: top;">
                                                                    <asp:TextBox ID="txtPasteParse" runat="server" BackColor="#CCFFFF" ForeColor="Black"
                                                                        Font-Size="Larger" ToolTip="" Rows="7" Height="100%" Width="97%" TextMode="MultiLine"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" valign="top">
                                                    Location:
                                                </td>
                                                <td align="left" valign="top">
                                                    <asp:TextBox ID="txtFromLocation" runat="server" ToolTip="IFS Location." MaxLength="100"
                                                        Width="98%" BackColor="#CCFFFF" ForeColor="Black" Font-Size="Larger"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top">
                                                    <asp:Button ID="btnAddTemplate" runat="server" Text="Add/Update Template" ToolTip="" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnAddTemplateClear" runat="server" Text="Clear" ToolTip="" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td  colspan="2" valign="top">
                                                    <asp:Label ID="lblAddTemplateMessage" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:TabPanel>

 <%--                           <asp:TabPanel ID="TabPanel15" HeaderText="Add/Edit Template" runat="server"
                                Visible="True" Width="100%" Height="100%" Style="overflow: auto; max-height: 460px; min-height: 460px;">
                                <ContentTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2" valign="top">
                                                Add/Edit Template
                                            </td>
                                            <td rowspan="4" valign="top">
                                                <asp:Panel ID="Panel10" runat="server" Style="overflow: auto; max-height: 370px;
                                                    min-height: 370px; max-width: 500px;" HorizontalAlign="Left" Width="100%" DefaultButton="btnPasteParseAdd">
                                                    Paste Location list below<br />
                                                    <table id="Table10" runat="server" width="100%">
                                                        <tr>
                                                            <td style="border-style: solid none none none; border-width: thin; border-color: #FFCC66;
                                                                text-align: left; vertical-align: top;">
                                                                <asp:RadioButtonList ID="PasteDeliminator" runat="server" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Text="Excel" Value="Excel" Selected="True"> </asp:ListItem>
                                                                    <asp:ListItem Text="Comma" Value="Comma"></asp:ListItem>
                                                                    <asp:ListItem Text="Space" Value="Space"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="border-style: solid none none none; border-width: thin; border-color: #FFCC66;
                                                                text-align: left; vertical-align: top;">
                                                                <asp:TextBox ID="txtPasteParse" runat="server" BackColor="#CCFFFF" ForeColor="Black"
                                                                    Font-Size="Larger" ToolTip="" Rows="14" Height="100%" Width="97%" TextMode="MultiLine"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top">
                                                <asp:Label ID="Label1" runat="server" Text="Status:"></asp:Label>
                                            </td>
                                            <td valign="top">
                                                <asp:DropDownList ID="drpTemplateStatus" runat="server" ToolTip="Template Status">
                                                    <asp:ListItem Text="Active" Selected="True" Value="Active"></asp:ListItem>
                                                    <asp:ListItem Text="Inactive" Value="Inactive"></asp:ListItem>
                                                </asp:DropDownList>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top">
                                                <asp:Label ID="Label2" runat="server" Text="Name:"></asp:Label>
                                            </td>
                                            <td valign="top">
                                                <asp:TextBox ID="txtTemplateName" runat="server" MaxLength="10"></asp:TextBox><br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top">
                                                <asp:Label ID="Label3" runat="server" Text="Note:"></asp:Label>
                                            </td>
                                            <td valign="top">
                                                <asp:TextBox ID="txtTemplateNote" runat="server" TextMode="MultiLine" Rows="6" Width="90%"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" valign="top">
                                                <asp:Button ID="btnAddTemplate" runat="server" Text="Add/Update Template" ToolTip="" />
                                            </td>

                                            <td>
                                                <asp:Button ID="btnPasteParseAdd" runat="server" Text="Add Locations to template"
                                                    ToolTip="" />
                                                <asp:Button ID="btnPasteParseDel" runat="server" Text="Remove Locations from template"
                                                    ToolTip="" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" valign="top">
                                                <asp:Label ID="lblAddTemplateMessage" runat="server" Text=""></asp:Label>
                                            </td>

                                        </tr>
                                    </table>

                                </ContentTemplate>
                            </asp:TabPanel>--%>
                        </asp:TabContainer>
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


        function PrintReportC(Report, Batch, CountID) {
            //            var xBatch = Batch();
            if (Report.length == 0) {
                alert('You must supply a Report Name');
                return;
            }
            if (CountID.length == 0) {
                alert('You must supply a ID');
                return;
            }
            var xDataList = {};
            xDataList["RPT"] = Report;
            xDataList["BATCH"] = Batch;
            xDataList["KEY"] = CountID;
            xDataList["USERNAME"] = UserName();
            var pstring = GetParameterStream(xDataList);
            // var WindowToOpen = "RPT_SpotCountReport.aspx";
            //var WindowToOpen = "/Reports/Report_Excel_OUT.aspx";
            var WindowToOpen = "/Reports/RPT_EXCEL_Out.aspx";
            if (pstring.length > 0) {
                WindowToOpen = WindowToOpen + "?" + pstring
            }
            var win = window.open(WindowToOpen, "_blank", "menubar", true);
            return;
        }


        function PrintReportB(Report, Command) {
            //            var xBatch = Batch();
            if (Report.length == 0) {
                alert('You must supply a Report Name');
                return;
            }
            if (Command.length == 0) {
                alert('You must supply a Command');
                return;
            }
            //            alert(Command);
            var xDataList = {};
            xDataList["RPT"] = Report;
            xDataList["COMMAND"] = Command;
            xDataList["USERNAME"] = UserName();
            var pstring = GetParameterStream(xDataList);
            // var WindowToOpen = "RPT_SpotCountReport.aspx";
            //var WindowToOpen = "/Reports/Report_Excel_OUT.aspx";
            var WindowToOpen = "/Reports/RPT_EXCEL_Out.aspx";
            if (pstring.length > 0) {
                WindowToOpen = WindowToOpen + "?" + pstring
            }
            var win = window.open(WindowToOpen, "_blank", "menubar", true);
            return;
        }

        function PrintReport(Report, ID) {
            //            var xBatch = Batch();
            if (Report.length == 0) {
                alert('You must supply a Report Name');
                return;
            }
            if (ID.length == 0) {
                alert('You must supply a ID');
                return;
            }
            var xDataList = {};
            xDataList["RPT"] = Report;
            xDataList["KEY"] = ID;
            xDataList["USERNAME"] = UserName();
            var pstring = GetParameterStream(xDataList);
            // var WindowToOpen = "RPT_SpotCountReport.aspx";
            //var WindowToOpen = "/Reports/Report_Excel_OUT.aspx";
            var WindowToOpen = "/Reports/RPT_EXCEL_Out.aspx";
            if (pstring.length > 0) {
                WindowToOpen = WindowToOpen + "?" + pstring
            }
            var win = window.open(WindowToOpen, "_blank", "menubar", true);
            return;
        }

        //        function PrintTabReport(xBatch) {
        //            //            var xBatch = Batch();
        //            if (xBatch.length == 0) {
        //                alert('You must supply a Tab');
        //                return;
        //            }
        //            var xDataList = {};
        //            xDataList["RPT"] = "PHYSICALTABCOUNT";
        //            xDataList["KEY"] = xBatch;
        //            xDataList["USERNAME"] = UserName();
        //            var pstring = GetParameterStream(xDataList);
        //            // var WindowToOpen = "RPT_SpotCountReport.aspx";
        //            var WindowToOpen = "/Report_Excel_OUTX.aspx";
        //            //var WindowToOpen = "/Reports/RPT_EXCEL_Out.aspx";
        //            if (pstring.length > 0) {
        //                WindowToOpen = WindowToOpen + "?" + pstring
        //            }
        //            var win = window.open(WindowToOpen, "_blank", "menubar", true);
        //            return;
        //        }
        function GetParameterStream(ParmameterList) {
            var count = 0;
            var sb = new Sys.StringBuilder();
            for (var property in ParmameterList) {
                if (count > 0) { sb.append("&"); }
                sb.append(property + "=" + ParmameterList[property]);
                count += 1;
            }
            return sb.toString();
        }



        function UserName() {
            return $get("<%= hdnUserName.ClientID %>").value;
        }







    </script>
</asp:Content>

