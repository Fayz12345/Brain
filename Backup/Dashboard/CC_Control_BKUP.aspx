<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CC_Control_BKUP.aspx.cs" Inherits="BW_WebApp.CC_Control_BKUP" %>

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
            <asp:HiddenField ID="hdnCycleInventoryCountHeaderID" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnReportThisData" runat="server" ClientIDMode="Static" />
            CycleInventoryCountIterationHeaderID
            
            <h1>
               Cycle Count Inventory Dashboard</h1>
            <br />
            <asp:Label ID="lblMainMessage" runat="server" Text=""></asp:Label>
            <asp:TabContainer ID="TabContainer1" runat="server">
                <asp:TabPanel ID="TabPanelRuns" runat="server" HeaderText="Runs">
                    <ContentTemplate>
<%--                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>--%>
                                <asp:ImageButton ID="imgDownloadGrid" runat="server" HeaderText="" ImageUrl="~/Images/arrow_down.png"
                                    ToolTip="Download Tab" Width="25px"></asp:ImageButton>
                                <asp:Label ID="lblRunmasterMessage" runat="server" Text=""></asp:Label>
                                <asp:TabContainer ID="TabRuns" runat="server" AutoPostBack="True">
                                    <asp:TabPanel ID="TabRunsNew" runat="server" HeaderText="New">
                                        <ContentTemplate>
                                        </ContentTemplate>
                                    </asp:TabPanel>
                                    <asp:TabPanel ID="TabPanelSpread" runat="server" HeaderText="Spread">
                                        <ContentTemplate>
                                        </ContentTemplate>
                                    </asp:TabPanel>

                              <%--      <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="Active">
                                        <ContentTemplate>
                                    
                                        </ContentTemplate>
                                    </asp:TabPanel>
                                    <asp:TabPanel ID="TabPanel13" runat="server" HeaderText="Hold">
                                        <ContentTemplate>
                                        </ContentTemplate>
                                    </asp:TabPanel>
                                    <asp:TabPanel ID="TabInvalid" runat="server" HeaderText="Closed">
                                        <ContentTemplate>
                                        </ContentTemplate>
                                    </asp:TabPanel>
                                    <asp:TabPanel ID="TabOpen" runat="server" HeaderText="Inactive">
                                        <ContentTemplate>
                                        </ContentTemplate>
                                    </asp:TabPanel>--%>
                                </asp:TabContainer>
                                <asp:Label ID="lblHeaderRunMessageNew" runat="server" Text=""></asp:Label>



                                <asp:Panel ID="pnlGridViewRunNew" runat="server" Style="width: auto;" HorizontalAlign="Left">
                                    <asp:Button ID="btnRefreshHeadersNew" runat="server" Text="Refresh" Width="100%" />
                                    <asp:Panel ID="Panel1" runat="server" Style="overflow: auto; max-height: 500px; width: auto;"
                                        HorizontalAlign="Left">
                                        
                                        <asp:GridView ID="grdRunsNew" runat="server" Width="100%" DataKeyNames="CycleInventoryCountHeaderID"
                                            AutoGenerateColumns="False" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                            AllowPaging="false" Font-Size="Smaller" AutoGenerateSelectButton="True">
                                            <SelectedRowStyle CssClass="srowstyle" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                    ItemStyle-Width="90px" ItemStyle-Wrap="False">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgDownLoadTemplateSummary" runat="server" HeaderText="" ImageUrl="~/Images/arrow_down.png"
                                                            ToolTip="Download Summary" Width="15px"></asp:ImageButton>
                                                        <asp:ImageButton ID="imgDownLoadTemplateSummaryDetail" runat="server" HeaderText="" ImageUrl="~/Images/arrow_down.png"
                                                            ToolTip="Download Summary Detail" Width="15px"></asp:ImageButton>
                                                        <asp:ImageButton ID="imgDownLoadTemplateDetail" runat="server" HeaderText="" ImageUrl="~/Images/arrow_down.png"
                                                            ToolTip="Download Detail" Width="15px"></asp:ImageButton>
                                                        <asp:ImageButton ID="imgOpenBatchList" runat="server" HeaderText="" ImageUrl="~/Images/closed.gif"
                                                            ToolTip="View/Activate Batches" Width="15px"></asp:ImageButton>
                                                        <asp:ImageButton ID="imgHold" runat="server" HeaderText="" ImageUrl="~/Images/details_close.png"
                                                            ToolTip="Move to Hold" Width="15px"></asp:ImageButton>
                                                        <asp:ConfirmButtonExtender ID="ConfirmButtonExtender12axyx" runat="server" TargetControlID="imgHold"
                                                            ConfirmText="Are you sure you want to move this to hold?">
                                                        </asp:ConfirmButtonExtender>
                                                        <asp:ImageButton ID="imgNew" runat="server" HeaderText="" ImageUrl="~/Images/details_close.png"
                                                            ToolTip="Move to New" Width="15px"></asp:ImageButton>
                                                        <asp:ConfirmButtonExtender ID="ConfirmButtonExtendercf2" runat="server" TargetControlID="imgNew"
                                                            ConfirmText="Are you sure you want to move this to New?">
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
                                                        <asp:ImageButton ID="imgactivate" runat="server" HeaderText="" ImageUrl="~/Images/closed.gif"
                                                            ToolTip="Move to Activate" Width="15px"></asp:ImageButton>
                                                        <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1po" runat="server" TargetControlID="imgactivate"
                                                            ConfirmText="Are you sure you want to make this Run Active?">
                                                        </asp:ConfirmButtonExtender>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="CycleInventoryCountHeaderID" HeaderText="Run #" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Status" HeaderText="Status" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="left"  VerticalAlign="Top"/>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Name" HeaderText="Name" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Note" HeaderText="Note" HeaderStyle-HorizontalAlign="Left">
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
                                                <asp:BoundField DataField="ClosedBatches" HeaderText="Closed Batches" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-ForeColor="#009900">
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="InvalidBatches" HeaderText="Invalid Batches" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-ForeColor="#009900">
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="HoldBatches" HeaderText="Hold Batches" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-ForeColor="#009900">
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CreateUser" HeaderText="Create User" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="left"  VerticalAlign="Top"/>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CreateDate" HeaderText="Create Date" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-Wrap="False">
                                                    <ItemStyle HorizontalAlign="left"  VerticalAlign="Top"/>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="LastUpdateUser" HeaderText="LastUpdate User" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="left"  VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="LastUpdateDate" HeaderText="LastUpdate Date" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-Wrap="False">
                                                    <ItemStyle HorizontalAlign="left"  VerticalAlign="Top" />
                                                </asp:BoundField>
                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel>
                                </asp:Panel>
                                <asp:Panel ID="pnlGridViewRunSpread" runat="server" Style="width: auto;" HorizontalAlign="Left">
                                    <asp:Button ID="Button1" runat="server" Text="Refresh" Width="100%" />
                                    <asp:Panel ID="Panel7" runat="server" Style="overflow: auto; max-height: 500px; width: auto;"
                                        HorizontalAlign="Left">
                                        <asp:TabContainer ID="SpreadTabs" runat="server" AutoPostBack="True">
                                            <asp:TabPanel ID="SpreadTabData" runat="server" HeaderText="Data">
                                                <ContentTemplate>
                                                    <asp:GridView ID="gvRunSpread" runat="server" Width="100%" DataKeyNames="CycleInventoryCountHeaderID"
                                                        AutoGenerateColumns="False" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                        AllowPaging="false" Font-Size="Smaller" AutoGenerateSelectButton="True">
                                                        <SelectedRowStyle CssClass="srowstyle" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                ItemStyle-Width="90px" ItemStyle-Wrap="False">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="imgDownLoadTemplateSummary" runat="server" HeaderText="" ImageUrl="~/Images/arrow_down.png"
                                                                        ToolTip="Download Summary" Width="15px"></asp:ImageButton>
                                                                    <asp:ImageButton ID="imgDownLoadTemplateSummaryDetail" runat="server" HeaderText=""
                                                                        ImageUrl="~/Images/arrow_down.png" ToolTip="Download Summary Detail" Width="15px">
                                                                    </asp:ImageButton>
                                                                    <asp:ImageButton ID="imgDownLoadTemplateDetail" runat="server" HeaderText="" ImageUrl="~/Images/arrow_down.png"
                                                                        ToolTip="Download Detail" Width="15px"></asp:ImageButton>
                                                                    <asp:ImageButton ID="imgOpenBatchList" runat="server" HeaderText="" ImageUrl="~/Images/closed.gif"
                                                                        ToolTip="View/Activate Batches" Width="15px"></asp:ImageButton>
                                                                    <asp:ImageButton ID="imgactivate" runat="server" HeaderText="" ImageUrl="~/Images/tasks.gif"
                                                                        ToolTip="Move to Activate" Width="15px"></asp:ImageButton>
                                                                    <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1po" runat="server" TargetControlID="imgactivate"
                                                                        ConfirmText="Are you sure you want to make this Run Active?">
                                                                    </asp:ConfirmButtonExtender>

                                                                    <asp:ImageButton ID="imgInactivate" runat="server" HeaderText="" ImageUrl="~/Images/close_inline.gif"
                                                                        ToolTip="Move To Inactive" Width="15px"></asp:ImageButton>
                                                                    <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1pn" runat="server" TargetControlID="imgInactivate"
                                                                        ConfirmText="Are you sure you want to make this Run inactive?">
                                                                    </asp:ConfirmButtonExtender>
                                                                    <asp:ImageButton ID="imgClose" runat="server" HeaderText="" ImageUrl="~/Images/delete-pro.bmp"
                                                                        ToolTip="Move To Close" Width="15px"></asp:ImageButton>
                                                                    <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1p" runat="server" TargetControlID="imgClose"
                                                                        ConfirmText="Are you sure you want to make this Run Closed?">
                                                                    </asp:ConfirmButtonExtender>
                                                                    <asp:ImageButton ID="imgHold" runat="server" HeaderText="" ImageUrl="~/Images/details_close.png"
                                                                        ToolTip="Move to Hold" Width="15px"></asp:ImageButton>
                                                                    <asp:ConfirmButtonExtender ID="ConfirmButtonExtender12axyx" runat="server" TargetControlID="imgHold"
                                                                        ConfirmText="Are you sure you want to move this to hold?">
                                                                    </asp:ConfirmButtonExtender>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="CycleInventoryCountHeaderID" HeaderText="Run #" HeaderStyle-HorizontalAlign="Left">
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
                                                            <asp:BoundField DataField="Batch" HeaderText="Batch" HeaderStyle-HorizontalAlign="Left"
                                                                ItemStyle-ForeColor="#009900">
                                                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="IFSLocation" HeaderText="Location" HeaderStyle-HorizontalAlign="Left"
                                                                ItemStyle-ForeColor="#009900">
                                                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="IFSZoneLocationDevice" HeaderText="IFSZoneLocationDevice"
                                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="#009900">
                                                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="IFSZoneLocationPart" HeaderText="IFSZoneLocationPart"
                                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="#009900">
                                                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="IsFrozen" HeaderText="IsFrozen" HeaderStyle-HorizontalAlign="Left"
                                                                ItemStyle-ForeColor="#009900">
                                                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="IsLocationLocked" HeaderText="IsLocationLocked" HeaderStyle-HorizontalAlign="Left"
                                                                ItemStyle-ForeColor="#009900">
                                                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Freq" HeaderText="Freq" HeaderStyle-HorizontalAlign="Left"
                                                                ItemStyle-ForeColor="#009900">
                                                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Good" HeaderText="Good" HeaderStyle-HorizontalAlign="Left"
                                                                ItemStyle-ForeColor="#009900">
                                                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Warnings" HeaderText="Warnings" HeaderStyle-HorizontalAlign="Left"
                                                                ItemStyle-ForeColor="#009900">
                                                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Errors" HeaderText="Errors" HeaderStyle-HorizontalAlign="Left"
                                                                ItemStyle-ForeColor="#009900">
                                                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="CountControl" HeaderText="CountControl" HeaderStyle-HorizontalAlign="Left"
                                                                ItemStyle-ForeColor="#009900">
                                                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="CountBatch" HeaderText="CountBatch" HeaderStyle-HorizontalAlign="Left"
                                                                ItemStyle-ForeColor="#009900">
                                                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="CountVariance" HeaderText="CountVariance" HeaderStyle-HorizontalAlign="Left"
                                                                ItemStyle-ForeColor="#009900">
                                                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="ValueControl" HeaderText="ValueControl" HeaderStyle-HorizontalAlign="Left"
                                                                ItemStyle-ForeColor="#009900">
                                                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="ValueBatch" HeaderText="ValueBatch" HeaderStyle-HorizontalAlign="Left"
                                                                ItemStyle-ForeColor="#009900">
                                                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="ValueVariance" HeaderText="ValueVariance" HeaderStyle-HorizontalAlign="Left"
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
                                                </ContentTemplate>
                                            </asp:TabPanel>
                                            <asp:TabPanel ID="SpreadTabActive" runat="server" HeaderText="Active">
                                                <ContentTemplate>
                                                </ContentTemplate>
                                            </asp:TabPanel>
                                            <asp:TabPanel ID="SpreadTabHold" runat="server" HeaderText="Hold">
                                                <ContentTemplate>
                                                </ContentTemplate>
                                            </asp:TabPanel>
                                            <asp:TabPanel ID="SpreadTabClosed" runat="server" HeaderText="Closed">
                                                <ContentTemplate>
                                                </ContentTemplate>
                                            </asp:TabPanel>
                                            <asp:TabPanel ID="SpreadTabInactive" runat="server" HeaderText="Inactive">
                                                <ContentTemplate>
                                                </ContentTemplate>
                                            </asp:TabPanel>
                                        </asp:TabContainer>









                                    </asp:Panel>
                                </asp:Panel>
                                <asp:Panel ID="pnlGridViewRunNewOpenBatchList" runat="server" Style="width: auto;"
                                    HorizontalAlign="Left" Visible="false">
                                    <asp:ImageButton ID="imgHeaderRunNewOpenBatchListBack" runat="server" HeaderText=""
                                        ImageUrl="~/Images/Left.gif" ToolTip="Back" Width="25px"></asp:ImageButton>
                                    <asp:Label ID="lblHeaderRunNewOpenBatchList" runat="server" Text=""></asp:Label>
                                    <asp:TabContainer ID="TabBatches" runat="server" AutoPostBack="True">
                                        <asp:TabPanel ID="bTabNew" runat="server" HeaderText="New">
                                            <ContentTemplate>
                                            </ContentTemplate>
                                        </asp:TabPanel>
                                        <asp:TabPanel ID="bTabOpen" runat="server" HeaderText="Open">
                                            <ContentTemplate>

                                            </ContentTemplate>
                                        </asp:TabPanel>
                                        <asp:TabPanel ID="bTabLocked" runat="server" HeaderText="Locked">
                                            <ContentTemplate>

                                            </ContentTemplate>
                                        </asp:TabPanel>
                                        <asp:TabPanel ID="bTabInvalid" runat="server" HeaderText="Invalid">
                                            <ContentTemplate>
                                            </ContentTemplate>
                                        </asp:TabPanel>
                                        <asp:TabPanel ID="bTabHold" runat="server" HeaderText="Hold">
                                            <ContentTemplate>
                                            </ContentTemplate>
                                        </asp:TabPanel>
                                        <asp:TabPanel ID="bTabSentIFS" runat="server" HeaderText="Sent IFS" Visible="false">
                                            <ContentTemplate>
                                            </ContentTemplate>
                                        </asp:TabPanel>
                                    </asp:TabContainer>
                                    <asp:Button ID="bRefreshBatches" runat="server" Text="Refresh" Width="100%" />
                                    <asp:Panel ID="Panel11" runat="server" Style="overflow: auto; max-height: 500px;
                                        width: auto;" HorizontalAlign="Left">
                                        <asp:GridView ID="grdBatches" runat="server" Width="100%" DataKeyNames="CycleInventoryCountIterationHeaderID"
                                            AutoGenerateColumns="False" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                            AllowPaging="false" Font-Size="Smaller">
                                            <SelectedRowStyle CssClass="srowstyle" />
                                            <Columns>
<%--                                                <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgDownLoadLocations" runat="server" HeaderText="" ImageUrl="~/Images/arrow_down.png"
                                                            ToolTip="Download this batch" Width="15px"></asp:ImageButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgOpenRunPhysicalScan" runat="server" HeaderText="" ImageUrl="~/Images/closed.gif"
                                                            ToolTip="View Physical Scan Batch" Width="15px"></asp:ImageButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgOpenRunControlSummary" runat="server" HeaderText="" ImageUrl="~/Images/closed.gif"
                                                            ToolTip="View Run Control" Width="15px"></asp:ImageButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgOpen" runat="server" HeaderText="" ImageUrl="~/Images/tasks.gif"
                                                            ToolTip="Activate batch/Lock location, Assign Batch Number." Width="15px"></asp:ImageButton>
                                                        <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1v" runat="server" TargetControlID="imgOpen"
                                                            ConfirmText="Are you sure you want to Activate/Open this batch?">
                                                        </asp:ConfirmButtonExtender>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgLocked" runat="server" HeaderText="" ImageUrl="~/Images/lock_-_pink.png"
                                                            ToolTip="Move to Locked" Width="15px"></asp:ImageButton>
                                                        <asp:ConfirmButtonExtender ID="ConfirmButtonExtender12cxyx" runat="server" TargetControlID="imgLocked"
                                                            ConfirmText="Are you sure you want to move this batch to Locked?">
                                                        </asp:ConfirmButtonExtender>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgInvalid" runat="server" HeaderText="" ImageUrl="~/Images/close_inline.gif"
                                                            ToolTip="Move to Invalid" Width="15px"></asp:ImageButton>
                                                        <asp:ConfirmButtonExtender ID="ConfirmButtonExtender12bxyx" runat="server" TargetControlID="imgInvalid"
                                                            ConfirmText="Are you sure you want to move this batch to Invalid?">
                                                        </asp:ConfirmButtonExtender>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgHold" runat="server" HeaderText="" ImageUrl="~/Images/details_close.png"
                                                            ToolTip="Move to Hold" Width="15px"></asp:ImageButton>
                                                        <asp:ConfirmButtonExtender ID="ConfirmButtonExtender12axyx" runat="server" TargetControlID="imgHold"
                                                            ConfirmText="Are you sure you want to move this batch to hold?">
                                                        </asp:ConfirmButtonExtender>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgCloneIntoNew" runat="server" HeaderText="" ImageUrl="~/Images/ChangeProcess.png"
                                                            ToolTip="Clone Into New" Width="15px"></asp:ImageButton>
                                                        <asp:ConfirmButtonExtender ID="ConfirmButtonExtenderc1zz" runat="server" TargetControlID="imgCloneIntoNew"
                                                            ConfirmText="Are you sure you want to generate a Run Cycle?">
                                                        </asp:ConfirmButtonExtender>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--                                        <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemStyleWidth=" 10%" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgDownload" runat="server" HeaderText="" ImageUrl="~/Images/arrow_down.png"
                                                    ToolTip="Download this batch" Width="15px"></asp:ImageButton>
                                                <asp:ImageButton ID="imgOpenRunPhysicalScan" runat="server" HeaderText="" ImageUrl="~/Images/closed.gif" ToolTip="View Physical Scan Batch" Width="15px"></asp:ImageButton>
                                                <asp:ImageButton ID="imgOpenRunControlSummary" runat="server" HeaderText="" ImageUrl="~/Images/closed.gif" ToolTip="View Run Control" Width="15px"></asp:ImageButton>
                                                <asp:ImageButton ID="imgOpen" runat="server" HeaderText="" ImageUrl="~/Images/tasks.gif"  ToolTip="Activate batch/Lock location, Assign Batch Number." Width="15px"></asp:ImageButton>
                                                <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1v" runat="server" TargetControlID="imgOpen"
                                                    ConfirmText="Are you sure you want to Activate/Open this batch?">
                                                </asp:ConfirmButtonExtender>
                                                <asp:ImageButton ID="imgLocked" runat="server" HeaderText="" ImageUrl="~/Images/lock_-_pink.png" ToolTip="Move to Locked" Width="15px"></asp:ImageButton>
                                                <asp:ConfirmButtonExtender ID="ConfirmButtonExtender12cxyx" runat="server" TargetControlID="imgLocked"
                                                    ConfirmText="Are you sure you want to move this batch to Locked?">
                                                </asp:ConfirmButtonExtender>
                                                <asp:ImageButton ID="imgInvalid" runat="server" HeaderText="" ImageUrl="~/Images/close_inline.gif" ToolTip="Move to Invalid" Width="15px"></asp:ImageButton>
                                                <asp:ConfirmButtonExtender ID="ConfirmButtonExtender12bxyx" runat="server" TargetControlID="imgInvalid"
                                                    ConfirmText="Are you sure you want to move this batch to Invalid?">
                                                </asp:ConfirmButtonExtender>
                                                <asp:ImageButton ID="imgHold" runat="server" HeaderText="" ImageUrl="~/Images/details_close.png" ToolTip="Move to Hold" Width="15px"></asp:ImageButton>
                                                <asp:ConfirmButtonExtender ID="ConfirmButtonExtender12axyx" runat="server" TargetControlID="imgHold"
                                                    ConfirmText="Are you sure you want to move this batch to hold?">
                                                </asp:ConfirmButtonExtender>
                                                <asp:ImageButton ID="imgCloneIntoNew" runat="server" HeaderText="" ImageUrl="~/Images/ChangeProcess.png" ToolTip="Clone Into New" Width="15px"></asp:ImageButton>
                                                <asp:ConfirmButtonExtender ID="ConfirmButtonExtenderc1zz" runat="server" TargetControlID="imgCloneIntoNew"
                                                    ConfirmText="Are you sure you want to generate a Run Cycle?">
                                                </asp:ConfirmButtonExtender>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                                <asp:BoundField DataField="Status" HeaderText="Status" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top"/>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Name" HeaderText="Name" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CycleInventoryCountIterationHeaderID" HeaderText="ID" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="left"  VerticalAlign="Top"/>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Batch" HeaderText="Batch" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="left"  VerticalAlign="Top"/>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="IFSLocation" HeaderText="Location" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="left"  VerticalAlign="Top"/>
                                                </asp:BoundField>

                                                <asp:BoundField DataField="IFSZoneLocationDevice" HeaderText="Zone Location D" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="left"  VerticalAlign="Top"/>
                                                </asp:BoundField>

                                                <asp:BoundField DataField="IFSZoneLocationPart" HeaderText="Zone Location P" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="left"  VerticalAlign="Top"/>
                                                </asp:BoundField>

                                                <asp:BoundField DataField="IsFrozen" HeaderText="Frozen" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="left"  VerticalAlign="Top"/>
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
                                                <asp:BoundField DataField="CountControl" HeaderText="No. Control" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-ForeColor="#009900">
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CountBatch" HeaderText="No. Batch" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-ForeColor="#009900">
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CountVariance" HeaderText="No. Variance" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-ForeColor="#009900">
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ValueControl" HeaderText="Value Control" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-ForeColor="#009900">
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ValueBatch" HeaderText="Value Batch" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-ForeColor="#009900">
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                </asp:BoundField>

                                                <asp:BoundField DataField="ValueVariance" HeaderText="Value Variance" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-ForeColor="#009900">
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                </asp:BoundField>


                                                <asp:BoundField DataField="ValueVariance" HeaderText="Invalid Batches" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-ForeColor="#009900">
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                </asp:BoundField>

                                                <asp:BoundField DataField="Note" HeaderText="Note" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                </asp:BoundField>

                                                <asp:BoundField DataField="CreateUser" HeaderText="Create User" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="left"  VerticalAlign="Top"/>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CreateDate" HeaderText="Create Date" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-Wrap="False">
                                                    <ItemStyle HorizontalAlign="left"  VerticalAlign="Top"/>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="LastUpdateUser" HeaderText="LastUpdate User" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="left"  VerticalAlign="Top"/>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="LastUpdateDate" HeaderText="LastUpdate Date" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-Wrap="False">
                                                    <ItemStyle HorizontalAlign="left"  VerticalAlign="Top"/>
                                                </asp:BoundField>
                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel>
                                </asp:Panel>
                                <asp:Panel ID="pnlGridViewBatchConroleList" runat="server" Style="width: auto;" HorizontalAlign="Left"
                                    Visible="false">
                                    <asp:ImageButton ID="imgHeaderRunNewOpenControlListBack" runat="server" HeaderText=""
                                        ImageUrl="~/Images/Left.gif" ToolTip="Back" Width="25px"></asp:ImageButton>
                                    <asp:Label ID="lblHeaderRunNewOpenControlList" runat="server" Text=""></asp:Label>
                                    <asp:TabContainer ID="TabRunControlDetail" runat="server" AutoPostBack="True">
                                        <asp:TabPanel ID="TabPanel5" runat="server" HeaderText="New">
                                            <ContentTemplate>
                                            </ContentTemplate>
                                        </asp:TabPanel>
                                        <asp:TabPanel ID="TabPanel6" runat="server" HeaderText="Match">
                                            <ContentTemplate>
                                            </ContentTemplate>
                                        </asp:TabPanel>
                                        <asp:TabPanel ID="TabPanel12" runat="server" HeaderText="Error">
                                            <ContentTemplate>
                                            </ContentTemplate>
                                        </asp:TabPanel>
                                    </asp:TabContainer>
                                    <asp:Panel ID="Panel3" runat="server" Style="width: auto;" HorizontalAlign="Left">
                                        <asp:CheckBox runat="server" ID="chkShowSummary_Control" Text="Summary"
                                            Checked="True"></asp:CheckBox>
                                        <asp:CheckBox runat="server" ID="chkShowDevices_Control" Text="Devices" Checked="True">
                                        </asp:CheckBox>
                                        <asp:CheckBox runat="server" ID="chkShowParts_Control" Text="Parts" Checked="True">
                                        </asp:CheckBox>
                                        <asp:Button ID="bRefreshBatchesControlDetail" runat="server" Text="Refresh" Width="100%" />
                                        <asp:Panel ID="Panel12" runat="server" Style="overflow: auto; max-height: 500px;
                                            width: auto;" HorizontalAlign="Left">
                                            <asp:GridView ID="gvRunControlDetail" runat="server" Width="100%" DataKeyNames="CycleInventoryCountControlDetailID"
                                                AutoGenerateColumns="False" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                AllowPaging="false" Font-Size="Smaller" ViewStateMode="Disabled">
                                                <SelectedRowStyle CssClass="srowstyle" />
                                                <Columns>
                                                    <asp:BoundField DataField="Status" HeaderText="Status" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemStyle HorizontalAlign="left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Name" HeaderText="Name" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CycleInventoryCountControlDetailID" HeaderText="ID" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemStyle HorizontalAlign="left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Batch" HeaderText="Batch" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemStyle HorizontalAlign="left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Quantity" HeaderText="QTY" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemStyle HorizontalAlign="left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ESN" HeaderText="IMEI" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemStyle HorizontalAlign="left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Version" HeaderText="Ver" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemStyle HorizontalAlign="left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CountType" HeaderText="CountType" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemStyle HorizontalAlign="left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="IFSSite" HeaderText="IFSSite" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemStyle HorizontalAlign="left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="IFSProject" HeaderText="IFSProject" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="IFSLocation" HeaderText="IFSLocation" HeaderStyle-HorizontalAlign="Left"
                                                        ItemStyle-ForeColor="#CC0000">
                                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="SKU" HeaderText="SKU" HeaderStyle-HorizontalAlign="Left"
                                                        ItemStyle-ForeColor="#009900">
                                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="IFSCondition" HeaderText="IFSCondition" HeaderStyle-HorizontalAlign="Left"
                                                        ItemStyle-ForeColor="#996633">
                                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Value" HeaderText="Value" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ValueVariance" HeaderText="ValueVariance" HeaderStyle-HorizontalAlign="Left"
                                                        ItemStyle-ForeColor="#009900">
                                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="StatusMessage" HeaderText="StatusMessage" HeaderStyle-HorizontalAlign="Left"
                                                        ItemStyle-ForeColor="#009900">
                                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CreateUser" HeaderText="Create User" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemStyle HorizontalAlign="left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CreateDate" HeaderText="Create Date" HeaderStyle-HorizontalAlign="Left"
                                                        ItemStyle-Wrap="False">
                                                        <ItemStyle HorizontalAlign="left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="LastUpdateUser" HeaderText="LastUpdate User" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemStyle HorizontalAlign="left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="LastUpdateDate" HeaderText="LastUpdate Date" HeaderStyle-HorizontalAlign="Left"
                                                        ItemStyle-Wrap="False">
                                                        <ItemStyle HorizontalAlign="left" />
                                                    </asp:BoundField>
                                                </Columns>
                                            </asp:GridView>
                                        </asp:Panel>
                                    </asp:Panel>
                                </asp:Panel>
                                <asp:Panel ID="pnlGridViewBatchScanList" runat="server" Style="width: auto;" HorizontalAlign="Left"
                                    Visible="false">
                                    <asp:ImageButton ID="imgHeaderRunNewOpenScanListBack" runat="server" HeaderText=""
                                        ImageUrl="~/Images/Left.gif" ToolTip="Back" Width="25px"></asp:ImageButton>
                                    <asp:Label ID="lblHeaderRunNewOpenScanList" runat="server" Text=""></asp:Label>
                                    <asp:TabContainer ID="TabRunScanDetail" runat="server" AutoPostBack="True">
                                        <asp:TabPanel ID="TabPanel7" runat="server" HeaderText="Match">
                                            <ContentTemplate>
                                            </ContentTemplate>
                                        </asp:TabPanel>
                                        <asp:TabPanel ID="TabPanel8" runat="server" HeaderText="Stray">
                                            <ContentTemplate>
                                            </ContentTemplate>
                                        </asp:TabPanel>
                                        <asp:TabPanel ID="TabPanel10" runat="server" HeaderText="Error">
                                            <ContentTemplate>
                                            </ContentTemplate>
                                        </asp:TabPanel>
                                        <asp:TabPanel ID="TabPanel11" runat="server" HeaderText="Deleted">
                                            <ContentTemplate>
                                            </ContentTemplate>
                                        </asp:TabPanel>
                                    </asp:TabContainer>
                                    <asp:Panel ID="Panel8" runat="server" Style="width: auto;" HorizontalAlign="Left">
                                        <asp:CheckBox runat="server" ID="chkShowSummary_Scan" Text="Summary" Checked="True">
                                        </asp:CheckBox>
                                        <asp:CheckBox runat="server" ID="chkShowDevices_Scan" Text="Devices" Checked="True">
                                        </asp:CheckBox>
                                        <asp:CheckBox runat="server" ID="chkShowParts_Scan" Text="Parts" Checked="True">
                                        </asp:CheckBox>
                                        <asp:Button ID="bRefreshBatchesScanDetail" runat="server" Text="Refresh" Width="100%" />
                                        <asp:Panel ID="Panel9" runat="server" Style="overflow: auto; max-height: 500px; width: auto;"
                                            HorizontalAlign="Left">
                                            <asp:GridView ID="gvRunScanDetail" runat="server" Width="100%" DataKeyNames="CycleInventoryCountControlDetailID"
                                                AutoGenerateColumns="False" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                AllowPaging="false" Font-Size="Smaller" ViewStateMode="Disabled">
                                                <SelectedRowStyle CssClass="srowstyle" />
                                                <Columns>
                                                    <asp:BoundField DataField="Status" HeaderText="Status" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemStyle HorizontalAlign="left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Name" HeaderText="Name" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CycleInventoryCountControlDetailID" HeaderText="ID" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemStyle HorizontalAlign="left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Batch" HeaderText="Batch" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemStyle HorizontalAlign="left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Quantity" HeaderText="QTY" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemStyle HorizontalAlign="left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ESN" HeaderText="IMEI" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemStyle HorizontalAlign="left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Version" HeaderText="Ver" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemStyle HorizontalAlign="left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CountType" HeaderText="CountType" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemStyle HorizontalAlign="left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="IFSSite" HeaderText="IFSSite" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemStyle HorizontalAlign="left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="IFSProject" HeaderText="IFSProject" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="IFSLocation" HeaderText="IFSLocation" HeaderStyle-HorizontalAlign="Left"
                                                        ItemStyle-ForeColor="#CC0000">
                                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="SKU" HeaderText="SKU" HeaderStyle-HorizontalAlign="Left"
                                                        ItemStyle-ForeColor="#009900">
                                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="IFSCondition" HeaderText="IFSCondition" HeaderStyle-HorizontalAlign="Left"
                                                        ItemStyle-ForeColor="#996633">
                                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Value" HeaderText="Value" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ValueVariance" HeaderText="ValueVariance" HeaderStyle-HorizontalAlign="Left"
                                                        ItemStyle-ForeColor="#009900">
                                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="StatusMessage" HeaderText="StatusMessage" HeaderStyle-HorizontalAlign="Left"
                                                        ItemStyle-ForeColor="#009900">
                                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CreateUser" HeaderText="Create User" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemStyle HorizontalAlign="left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CreateDate" HeaderText="Create Date" HeaderStyle-HorizontalAlign="Left"
                                                        ItemStyle-Wrap="False">
                                                        <ItemStyle HorizontalAlign="left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="LastUpdateUser" HeaderText="LastUpdate User" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemStyle HorizontalAlign="left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="LastUpdateDate" HeaderText="LastUpdate Date" HeaderStyle-HorizontalAlign="Left"
                                                        ItemStyle-Wrap="False">
                                                        <ItemStyle HorizontalAlign="left" />
                                                    </asp:BoundField>
                                                </Columns>
                                            </asp:GridView>
                                        </asp:Panel>
                                    </asp:Panel>
                                </asp:Panel>
<%--                            </ContentTemplate>
                        </asp:UpdatePanel>--%>



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
                                        <asp:GridView ID="grdTemplateHeaders" runat="server" Width="100%" DataKeyNames="CycleInventoryCountTemplateHeaderID"
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
                                                <asp:BoundField DataField="IFSSite" HeaderText="IFSSite" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="IFSLocation" HeaderText="IFSLocation" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="IFSCondition" HeaderText="IFSCondition" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Carriers" HeaderText="Carriers" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Manufacturers" HeaderText="Manufacturers" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Models" HeaderText="Models" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Colours" HeaderText="Colours" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
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
                                        <asp:GridView ID="grdTemplateHeadersInactive" runat="server" Width="100%" DataKeyNames="CycleInventoryCountTemplateHeaderID"
                                            AutoGenerateColumns="False" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                            AllowPaging="false" Font-Size="Smaller">
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
                                                <asp:BoundField DataField="Status" HeaderText="Status" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Name" HeaderText="Name" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Note" HeaderText="Note" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                </asp:BoundField>

                                                <asp:BoundField DataField="QuantityLocations" HeaderText="# Locations" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-ForeColor="#009900">
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                </asp:BoundField>

                                                <asp:BoundField DataField="CreateUser" HeaderText="Create User" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CreateDate" HeaderText="Create Date" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-Wrap="False">
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:BoundField>

                                                <asp:BoundField DataField="LastUpdateUser" HeaderText="LastUpdate User" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="LastUpdateDate" HeaderText="LastUpdate Date" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-Wrap="False">
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
                                                    <asp:TextBox ID="txtTemplateNote" runat="server" TextMode="MultiLine" Rows="6" Width="98%" BackColor="#CCFFFF" ForeColor="Black"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="20%" align="right" valign="top">
                                                    Site:
                                                </td>
                                                <td align="left" valign="top">
                                                    <asp:DropDownList ID="drpMoveIFSSite" runat="server" BackColor="#CCFFFF" ForeColor="Black"
                                                        Font-Size="Larger">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" valign="top">
                                                </td>
                                                <td align="left" valign="top">
                                                    <br />
                                                    (Comma delimited Segments eg:APL,BEL, etc.)
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" valign="top">
                                                    Carrier Segment:
                                                </td>
                                                <td align="left" valign="top">
                                                    <asp:TextBox ID="txtFromSkuCarrier" runat="server" ToolTip="Carrier Segments." MaxLength="100"
                                                        Width="98%" BackColor="#CCFFFF" ForeColor="Black" Font-Size="Larger"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" valign="top">
                                                    Manufacturer Segment:
                                                </td>
                                                <td align="left" valign="top">
                                                    <asp:TextBox ID="txtFromSkuManufacturer" runat="server" ToolTip="Manufacturer." MaxLength="100"
                                                        Width="98%" BackColor="#CCFFFF" ForeColor="Black" Font-Size="Larger"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" valign="top">
                                                    Model Segment:
                                                </td>
                                                <td align="left" valign="top">
                                                    <asp:TextBox ID="txtFromSkuModel" runat="server" ToolTip="Model." MaxLength="100"
                                                        Width="98%" BackColor="#CCFFFF" ForeColor="Black" Font-Size="Larger"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" valign="top">
                                                    Colour Segment:
                                                </td>
                                                <td align="left" valign="top">
                                                    <asp:TextBox ID="txtFromSkuColour" runat="server" ToolTip="Colour." MaxLength="100"
                                                        Width="98%" BackColor="#CCFFFF" ForeColor="Black" Font-Size="Larger"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" valign="top">
                                                    <br />
                                                    Location:
                                                </td>
                                                <td align="left" valign="top">
                                                    <br />
                                                    <asp:TextBox ID="txtFromLocation" runat="server" ToolTip="IFS Location." MaxLength="100"
                                                        Width="98%" BackColor="#CCFFFF" ForeColor="Black" Font-Size="Larger"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" valign="top">
                                                    Condition:
                                                </td>
                                                <td align="left" valign="top">
                                                    <asp:TextBox ID="txtFromCondition" runat="server" ToolTip="IFS Condition." MaxLength="100"
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
            alert(Command);
            var xDataList = {};
            xDataList["RPT"] = Report;
            xDataList["COMMAND"] = Command;
            xDataList["USERNAME"] = UserName();
            var pstring = GetParameterStream(xDataList);
            // var WindowToOpen = "RPT_SpotCountReport.aspx";
            var WindowToOpen = "/Report_Excel_OUTX.aspx";
            //var WindowToOpen = "/Reports/RPT_EXCEL_Out.aspx";
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
            var WindowToOpen = "/Report_Excel_OUTX.aspx";
            //var WindowToOpen = "/Reports/RPT_EXCEL_Out.aspx";
            if (pstring.length > 0) {
                WindowToOpen = WindowToOpen + "?" + pstring
            }
            var win = window.open(WindowToOpen, "_blank", "menubar", true);
            return;
        }

        function PrintTabReport(xBatch) {
            //            var xBatch = Batch();
            if (xBatch.length == 0) {
                alert('You must supply a Tab');
                return;
            }
            var xDataList = {};
            xDataList["RPT"] = "PHYSICALTABCOUNT";
            xDataList["KEY"] = xBatch;
            xDataList["USERNAME"] = UserName();
            var pstring = GetParameterStream(xDataList);
            // var WindowToOpen = "RPT_SpotCountReport.aspx";
            var WindowToOpen = "/Report_Excel_OUTX.aspx";
            //var WindowToOpen = "/Reports/RPT_EXCEL_Out.aspx";
            if (pstring.length > 0) {
                WindowToOpen = WindowToOpen + "?" + pstring
            }
            var win = window.open(WindowToOpen, "_blank", "menubar", true);
            return;
        }
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

