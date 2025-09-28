<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PI_ControlParts.aspx.cs" Inherits="BW_WebApp.PI_ControlParts" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Syncfusion.Shared.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89" Namespace="Syncfusion.Web.UI.WebControls.Shared" TagPrefix="syncfusion" %>
<%@ Register Assembly="Syncfusion.Tools.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89"  Namespace="Syncfusion.Web.UI.WebControls.Tools" TagPrefix="syncfusion" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cMC" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">

        <ContentTemplate>
        <asp:HiddenField ID="hdnUserName" runat="server" ClientIDMode="Static" />

            
                                            <h1>
                                                Part Physical Inventory Dashboard</h1>
            <br />
            <asp:ImageButton ID="imgDownloadGrid" runat="server" HeaderText="" ImageUrl="~/Images/arrow_down.png"
                ToolTip="Download Tab" Width="25px"></asp:ImageButton>


            <asp:TabContainer ID="TabContainerListName" runat="server">
                <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="Locked Batches">
                    <ContentTemplate>
                        <asp:Panel ID="pnlMainGridPN" runat="server" Style="overflow: auto; max-height: 500px;
                            width: auto;" HorizontalAlign="Left">
                            <asp:Label ID="lblMessageLock" runat="server" Text=""></asp:Label>
                            <asp:Button ID="btnRefreshLocked" runat="server" Text="Refresh" Width="100%" />

                           <asp:GridView ID="GRDLockedBatches" runat="server" Width="100%" DataKeyNames="Batch"
                                AutoGenerateColumns="False" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" AllowPaging="false">
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
                                            <asp:ImageButton ID="imgDownload" runat="server" HeaderText="" ImageUrl="~/Images/ADD.BMP"
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
                                            <asp:ImageButton ID="imgTransfer" runat="server" HeaderText="" ImageUrl="~/Images/uparrow_inline.gif"
                                                ToolTip="Send to IFS." Width="15px"></asp:ImageButton>
                                            <asp:ConfirmButtonExtender ID="ConfirmButtonExtender12" runat="server" TargetControlID="imgTransfer"
                                                ConfirmText="Are you sure you want to Send this batch to IFS?">
                                            </asp:ConfirmButtonExtender>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="Batch" HeaderText="Batch" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="freq" HeaderText="#" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Warnings" HeaderText="W" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Errors" HeaderText="E" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CreateUser" HeaderText="Create User" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="StartDate" HeaderText="Start Date" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="EndDate" HeaderText="End Date" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="TabPanel4" runat="server" HeaderText="Invalid Batches">
                    <ContentTemplate>
                        <asp:Panel ID="Panel1" runat="server" Style="overflow: auto; max-height: 500px;
                            width: auto;" HorizontalAlign="Left">
                            <asp:Label ID="lblMessageInvalid" runat="server" Text=""></asp:Label>
                           <asp:Button ID="btnRefreshInvalid" runat="server" Text="Refresh" Width="100%" />
                           <asp:GridView ID="GRDInvalidBatches" runat="server" Width="100%" DataKeyNames="Batch"
                                AutoGenerateColumns="False" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" AllowPaging="false">
                                <SelectedRowStyle CssClass="srowstyle" />
                                <Columns>
                                    <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgDownload" runat="server" HeaderText="" ImageUrl="~/Images/ADD.BMP"
                                                ToolTip="Download this batch" Width="15px"></asp:ImageButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgOpen" runat="server" HeaderText="" ImageUrl="~/Images/Files.png"
                                                ToolTip="Return to Open Status." Width="15px"></asp:ImageButton>
                                            <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="imgOpen"
                                                ConfirmText="Are you sure you want to return this batch to open?">
                                            </asp:ConfirmButtonExtender>
                                        </ItemTemplate>
                                    </asp:TemplateField>

<%--                                    <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgTransfer" runat="server" HeaderText="" ImageUrl="~/Images/uparrow_inline.gif"
                                                ToolTip="Send to IFS." Width="15px"></asp:ImageButton>
                                            <asp:ConfirmButtonExtender ID="ConfirmButtonExtender12" runat="server" TargetControlID="imgTransfer"
                                                ConfirmText="Are you sure you want to Send this batch to IFS?">
                                            </asp:ConfirmButtonExtender>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>

                                    <asp:BoundField DataField="Batch" HeaderText="Batch" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="freq" HeaderText="#" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Warnings" HeaderText="W" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Errors" HeaderText="E" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CreateUser" HeaderText="Create User" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="StartDate" HeaderText="Start Date" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="EndDate" HeaderText="End Date" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="TabPanel5" runat="server" HeaderText="Open Batches">
                    <ContentTemplate>
                        <asp:Panel ID="Panel2" runat="server" Style="overflow: auto; max-height: 500px;
                            width: auto;" HorizontalAlign="Left">
                           <asp:Label ID="lblMessageOpen" runat="server" Text=""></asp:Label>
                           <asp:Button ID="btnRefreshOpen" runat="server" Text="Refresh" Width="100%" />
                           <asp:GridView ID="GRDOpenBatches" runat="server" Width="100%" DataKeyNames="Batch"
                                AutoGenerateColumns="False" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" AllowPaging="false">
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
                                            <asp:ImageButton ID="imgDownload" runat="server" HeaderText="" ImageUrl="~/Images/ADD.BMP"
                                                ToolTip="Download this batch" Width="15px"></asp:ImageButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgLock" runat="server" HeaderText="" ImageUrl="~/Images/lock.png"
                                                ToolTip="Lock batch" Width="15px"></asp:ImageButton>
                                            <asp:ConfirmButtonExtender ID="ConfirmButtonExtender12" runat="server" TargetControlID="imgLock"
                                                ConfirmText="Are you sure you want to Lock this batch?">
                                            </asp:ConfirmButtonExtender>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="Batch" HeaderText="Batch" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="freq" HeaderText="#" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Warnings" HeaderText="W" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Errors" HeaderText="E" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CreateUser" HeaderText="Create User" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="StartDate" HeaderText="Start Date" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="EndDate" HeaderText="End Date" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </asp:Panel>

                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="Sent IFS">
                    <ContentTemplate>

                       <asp:Panel ID="Panel3" runat="server" Style="overflow: auto; max-height: 500px;
                            width: auto;" HorizontalAlign="Left">
                           <asp:Button ID="btnRefreshSentIFS" runat="server" Text="Refresh" Width="100%" />
                           <asp:GridView ID="GRDSentIFSBatches" runat="server" Width="100%" DataKeyNames="Batch"
                                AutoGenerateColumns="False" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" AllowPaging="false">
                                <SelectedRowStyle CssClass="srowstyle" />
                                <Columns>
                                    <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgDownload" runat="server" HeaderText="" ImageUrl="~/Images/ADD.BMP"
                                                ToolTip="Download this batch" Width="15px"></asp:ImageButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
<%--                                    <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgLock" runat="server" HeaderText="" ImageUrl="~/Images/lock.png"
                                                ToolTip="Lock batch" Width="15px"></asp:ImageButton>
                                            <asp:ConfirmButtonExtender ID="ConfirmButtonExtender12" runat="server" TargetControlID="imgLock"
                                                ConfirmText="Are you sure you want to Lock this batch?">
                                            </asp:ConfirmButtonExtender>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:BoundField DataField="Batch" HeaderText="Batch" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="freq" HeaderText="#" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Warnings" HeaderText="W" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Errors" HeaderText="E" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CreateUser" HeaderText="Create User" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="StartDate" HeaderText="Start Date" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="EndDate" HeaderText="End Date" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
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



        function PrintReport(xBatch) {
            //            var xBatch = Batch();
            if (xBatch.length == 0) {
                alert('You must supply a batch');
                return;
            }
            var xDataList = {};
            xDataList["RPT"] = "PARTPHYSICALCOUNT";
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

        function PrintTabReport(xBatch) {
            //            var xBatch = Batch();
            if (xBatch.length == 0) {
                alert('You must supply a Tab');
                return;
            }
            var xDataList = {};
            xDataList["RPT"] = "PHYSICALTABCOUNTPART";
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

