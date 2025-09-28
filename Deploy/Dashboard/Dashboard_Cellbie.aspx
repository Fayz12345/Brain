<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard_Cellbie.aspx.cs" Inherits="BW_WebApp.Dashboard_Cellbie" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Syncfusion.Shared.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89" Namespace="Syncfusion.Web.UI.WebControls.Shared" TagPrefix="syncfusion" %>
<%@ Register Assembly="Syncfusion.Tools.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89"  Namespace="Syncfusion.Web.UI.WebControls.Tools" TagPrefix="syncfusion" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cMC" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="hdnUserName" runat="server" ClientIDMode="Static" />
            
            <asp:Label ID="Label1" runat="server" Text="Cellbie Dashboard" Font-Bold="True" Font-Size="X-Large" ></asp:Label>
            <br /><br />
            <asp:Table ID="CellbieDownloadGrid" runat="server">
                <asp:TableRow ID="rowReceivedDate">
                    <asp:TableCell VerticalAlign="Top" HorizontalAlign="Left">
                    Download Tab Data --
                        <asp:ImageButton ID="imgDownloadGrid" runat="server" HeaderText="Download Tab data" ImageUrl="~/Images/arrow_down.png"
                            ToolTip="Download Tab" Width="25px" ></asp:ImageButton><br /><br />
                    </asp:TableCell>
                    <asp:TableCell VerticalAlign="Top">
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <asp:TabContainer ID="CellbieTabContainer" runat="server" CssClass="tab-container" AutoPostBack="True">
                <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="Send" CssClass="tab-panel" >
                    <ContentTemplate>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="Sent" CssClass="tab-panel" >
                    <ContentTemplate>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="TabPanel3" runat="server" HeaderText="Error" CssClass="tab-panel" >
                    <ContentTemplate>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="TabPanel4" runat="server" HeaderText="Success" CssClass="tab-panel" >
                    <ContentTemplate>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="TabPanel5" runat="server" HeaderText="Archive" CssClass="tab-panel" >
                    <ContentTemplate>
                    </ContentTemplate>
                </asp:TabPanel>
        <asp:TabPanel ID="TestSendCellbie" CssClass="tab-panel" HeaderText="Test Send Cellbie"
            runat="server" Visible="True">
            <ContentTemplate>
                <strong class="d-block mb-3">This Utility is for testing of CELLBIE API only.</strong>
                <div class="row">
                    <div class="col-md">
                        <label>
                            Device IMEI:</label>
                    </div>
                    <div class="col-md">
                        <asp:TextBox ID="txtCellbieIMEI" runat="server" ToolTip="Cellbie IMEI Number" Text="356160070900412" />
                    </div>
                </div>

                <div class="row">
                    <div class="col-md">
                        <label>
                            Received as Expected:</label>
                    </div>
                    <div class="col-md">
                        <asp:RadioButtonList ID="rdlReceivedOK" runat="server">
                        <asp:ListItem Selected="True" Value="1" Text="Received as Expected"></asp:ListItem>
                        <asp:ListItem Value="0" Text="We have an Issue"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md">
                        <label>
                            Comment:</label>
                    </div>
                    <div class="col-md">
                        <asp:TextBox ID="txtCellbieComment" runat="server" ToolTip="Cellbie Receive Detail" Text="" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md">
                        <br />
                    </div>
                    <div class="col-md">
                    </div>
                </div>
                <div class="row">
                    <div class="col-md">
                        <label>
                            Transaction Info JSON:</label>
                    </div>
                    <div class="col-md">
                        <asp:Label ID="lblTransactionInfo" runat="server" Text=""></asp:Label>
                        <%--   <asp:TextBox ID="txtCellbieIMEIOutput" runat="server" ToolTip="Cellbie IMEI Output" />--%>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md">
                        <label>
                            Parameter JSON:</label>
                    </div>
                    <div class="col-md">
                        <asp:Label ID="lblCellbieParameterJSON" runat="server" Text=""></asp:Label>
                        <%--   <asp:TextBox ID="txtCellbieIMEIOutput" runat="server" ToolTip="Cellbie IMEI Output" />--%>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md">
                        <br />
                    </div>
                    <div class="col-md">
                    </div>
                </div>
                <div class="row">
                    <div class="col-md">
                        <label>
                            Output:</label>
                    </div>
                    <div class="col-md">
                        <asp:Label ID="lblCellbieIMEIOutput" runat="server" Text=""></asp:Label>
                     <%--   <asp:TextBox ID="txtCellbieIMEIOutput" runat="server" ToolTip="Cellbie IMEI Output" />--%>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md">
                        <label>
                            Message:</label>
                    </div>
                    <div class="col-md">
                        <asp:Label ID="lblCellbieIMEIMessage" runat="server" Text=""></asp:Label>
                        <%--<asp:TextBox ID="txtCellbieIMEIMessage" runat="server" ToolTip="Cellbie IMEI Message" />--%>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md">
                        <label>
                            Error:</label>
                    </div>
                    <div class="col-md">
                        <asp:Label ID="lblCellbieError" runat="server" Text=""></asp:Label>
                        <%--<asp:TextBox ID="txtCellbieIMEIMessage" runat="server" ToolTip="Cellbie IMEI Message" />--%>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md">
                        <label>
                            Internal Error:</label>
                    </div>
                    <div class="col-md">
                        <asp:Label ID="lblCellbieErrorInternal" runat="server" Text=""></asp:Label>
                        <%--<asp:TextBox ID="txtCellbieIMEIMessage" runat="server" ToolTip="Cellbie IMEI Message" />--%>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md">
                        <asp:Button ID="btnCellbieIMEI" runat="server" Text="Get Device Info" ToolTip="Download Device Info" Visible="false" />
                        <asp:Button ID="btnCellbieReceive" runat="server" Text="Cellbie Receive Device" ToolTip="Send Cellbie IMEI Received" />
                        <asp:Button ID="btnPlayBeep" runat="server" Text="Beep" ToolTip="" OnClientClick="jmbeep(3); false;" Visible="false" />
                        <asp:Button ID="btnGetBeep" runat="server" Text="GetBeep" ToolTip="" Visible="false" />
                        <br>

                        <asp:Label ID="lblGetBeep" runat="server" Text="Label"></asp:Label>


                    </div>
                </div>
                <hr>
                <p class="text-muted">
                    NOTE: This Utility sends a request to Cellbie Server for Device Information<br>
                </p>
            </ContentTemplate>
        </asp:TabPanel>


            </asp:TabContainer>
            <asp:Panel ID="Panel5" CssClass="overflow" runat="server">
                <asp:GridView ID="grdTempDetail" CssClass="table" runat="server" DataKeyField="Name"
                    AutoGenerateColumns="False">
                    <Columns>
                        <asp:TemplateField HeaderText="P" ItemStyle-Wrap="False" HeaderStyle-Wrap="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="imgSendCellbie" CssClass="btn btn-default p-1 oi oi-spreadsheet" runat="server"
                                    HeaderText="" ToolTip="Send to Cellbie" />
                                            <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="imgSendCellbie"
                                                ConfirmText="Are you sure you want to send this back to Cellbie?">
                                            </asp:ConfirmButtonExtender>
                                <asp:LinkButton ID="imgMoveToSend" CssClass="btn btn-default p-1 oi oi-spreadsheet" runat="server"
                                    HeaderText="" ToolTip="Move To Send" />
                                            <asp:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" TargetControlID="imgMoveToSend"
                                                ConfirmText="Are you sure you want to Move To Send?">
                                            </asp:ConfirmButtonExtender>
                                <asp:LinkButton ID="imgMoveToSent" CssClass="btn btn-default p-1 oi oi-spreadsheet" runat="server"
                                    HeaderText="" ToolTip="Move To Sent" />
                                            <asp:ConfirmButtonExtender ID="ConfirmButtonExtender4" runat="server" TargetControlID="imgMoveToSent"
                                                ConfirmText="Are you sure you want to Move To Sent?">
                                            </asp:ConfirmButtonExtender>
                                <asp:LinkButton ID="imgMoveToError" CssClass="btn btn-default p-1 oi oi-spreadsheet" runat="server"
                                    HeaderText="" ToolTip="Move To Error" />
                                            <asp:ConfirmButtonExtender ID="ConfirmButtonExtender5" runat="server" TargetControlID="imgMoveToError"
                                                ConfirmText="Are you sure you want to Move To Error?">
                                            </asp:ConfirmButtonExtender>
                                <asp:LinkButton ID="imgMoveToSuccess" CssClass="btn btn-default p-1 oi oi-spreadsheet" runat="server"
                                    HeaderText="" ToolTip="Move To Success" />
                                            <asp:ConfirmButtonExtender ID="ConfirmButtonExtender6" runat="server" TargetControlID="imgMoveToSuccess"
                                                ConfirmText="Are you sure you want to Move To Success?">
                                            </asp:ConfirmButtonExtender>
                                <asp:LinkButton ID="imgMoveArchive" CssClass="btn btn-default p-1 oi oi-spreadsheet" runat="server"
                                    HeaderText="" ToolTip="Send to Archive" />
                                            <asp:ConfirmButtonExtender ID="ConfirmButtonExtender3" runat="server" TargetControlID="imgMoveToSend"
                                                ConfirmText="Are you sure you want to Move To Archive?">
                                            </asp:ConfirmButtonExtender>



                                <asp:LinkButton ID="imgOpen" CssClass="btn btn-default p-1 oi oi-spreadsheet" runat="server"
                                    HeaderText="" ToolTip="Open" />
<%--                                <asp:LinkButton ID="imgOpenProcess" CssClass="btn btn-default p-1 oi oi-spreadsheet"
                                    runat="server" HeaderText="" ToolTip="Open Proccess" />--%>
                                <%--<asp:LinkButton ID="imgOpenQC" CssClass="btn btn-default p-1 oi oi-spreadsheet runat="server" HeaderText="" ToolTip="Open QC Assessment" />--%>
                                <asp:LinkButton ID="imgLastUser" CssClass="btn btn-default p-1 oi oi-person" runat="server"
                                    HeaderText="" ToolTip="Last User" OnClientClick="return false;" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ESN" HeaderText="ESN" ReadOnly="True" HeaderStyle-Wrap="False">
                            <ItemStyle Wrap="false" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Version" HeaderText="Version" ReadOnly="True" HeaderStyle-Wrap="False">
                            <ItemStyle Wrap="false" />
                        </asp:BoundField>
<%--                        <asp:BoundField DataField="CellbieStatus" HeaderText="CellbieStatus" ReadOnly="True"
                            HeaderStyle-Wrap="False">
                            <ItemStyle Wrap="false" />
                        </asp:BoundField>--%>
                        <asp:BoundField DataField="SendParamAgree" HeaderText="SendParamAgree" ReadOnly="True"
                            HeaderStyle-Wrap="False">
                            <ItemStyle Wrap="false" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SendParamMessage" HeaderText="SendParamMessage" ReadOnly="True"
                            HeaderStyle-Wrap="False">
                            <ItemStyle Wrap="false" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SendReturnMessage" HeaderText="SendReturnMessage" ReadOnly="True"
                            HeaderStyle-Wrap="False">
                            <ItemStyle Wrap="false" />
                        </asp:BoundField>




                        <asp:BoundField DataField="MiscText" HeaderText="MiscText" ReadOnly="True"
                            HeaderStyle-Wrap="False">
                            <ItemStyle Wrap="false" />
                        </asp:BoundField>
                        <asp:BoundField DataField="LastUpdateDate_Cellbie" HeaderText="LastUpdateDate_Cellbie" ReadOnly="True"
                            HeaderStyle-Wrap="False">
                            <ItemStyle Wrap="false" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ProjectName" HeaderText="Project Name" ReadOnly="True"
                            HeaderStyle-Wrap="False">
                            <ItemStyle Wrap="false" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Status" HeaderText="Status" ReadOnly="True" HeaderStyle-Wrap="False">
                            <ItemStyle Wrap="false" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CurrentProcessName" HeaderText="Current Process Name"
                            ReadOnly="True" HeaderStyle-Wrap="False">
                            <ItemStyle Wrap="false" />
                        </asp:BoundField>

<%--                        <asp:BoundField DataField="SwappedESN" HeaderText="Swapped ESN" ReadOnly="True" HeaderStyle-Wrap="False">
                            <ItemStyle Wrap="false" />
                        </asp:BoundField>--%>
                        <asp:BoundField DataField="IFSLocation" HeaderText="Location" ReadOnly="True" HeaderStyle-Wrap="False">
                            <ItemStyle Wrap="false" />
                        </asp:BoundField>
<%--                        <asp:BoundField DataField="IFSCondition" HeaderText="Condition" ReadOnly="True" HeaderStyle-Wrap="False">
                            <ItemStyle Wrap="false" />
                        </asp:BoundField>--%>
                        <asp:BoundField DataField="SKU" HeaderText="SKU" ReadOnly="True" HeaderStyle-Wrap="False">
                            <ItemStyle Wrap="false" />
                        </asp:BoundField>
                        <asp:BoundField DataField="MakeModelString" HeaderText="Make Model" ReadOnly="True"
                            HeaderStyle-Wrap="False">
                            <ItemStyle Wrap="false" />
                        </asp:BoundField>
                        <%--<asp:BoundField DataField="RMANumber" HeaderText="RMA Number" ReadOnly="True" HeaderStyle-Wrap="False">
                            <ItemStyle Wrap="false" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ProjectTag" HeaderText="Project Tag" ReadOnly="True" HeaderStyle-Wrap="False">
                            <ItemStyle Wrap="false" />
                        </asp:BoundField>--%>
                        <asp:BoundField DataField="ReceiveDate" HeaderText="Receive Date" ReadOnly="True"
                            HeaderStyle-Wrap="False">
                            <ItemStyle Wrap="false" />
                        </asp:BoundField>
                       <%-- <asp:BoundField DataField="ShipDate" HeaderText="Ship Date" ReadOnly="True" HeaderStyle-Wrap="False">
                            <ItemStyle Wrap="false" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Date_QC" HeaderText="QC Date" ReadOnly="True" HeaderStyle-Wrap="False">
                            <ItemStyle Wrap="false" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CompanyName" HeaderText="Company Name" ReadOnly="True"
                            HeaderStyle-Wrap="False">
                            <ItemStyle Wrap="false" />
                        </asp:BoundField>
                        <asp:BoundField DataField="StoreNumber" HeaderText="Store Number" ReadOnly="True"
                            HeaderStyle-Wrap="False">
                            <ItemStyle Wrap="false" />
                        </asp:BoundField>--%>
                        <asp:TemplateField HeaderText="Open" ItemStyle-Wrap="False" HeaderStyle-Wrap="False" Visible="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="imgAnalyze" CssClass="btn btn-default p-1 oi oi-info" runat="server"
                                    HeaderText="*" ToolTip="Download Unit Summary" />
                                <asp:LinkButton ID="imgBagTag" CssClass="btn btn-default p-1 oi oi-print" runat="server"
                                    HeaderText="*" ToolTip="Print Bag Tag" />
                               <%-- <asp:LinkButton ID="ImageKitting" CssClass="btn btn-default p-1 oi oi-print" runat="server"
                                    HeaderText="*" ToolTip="Print Kitting Label" />
                                <asp:LinkButton ID="ImgPartDetails" CssClass="btn btn-default p-1 oi oi-pencil" runat="server"
                                    HeaderText="*" ToolTip="Edit Unit Part Details" />
                                <asp:LinkButton ID="ImageResetBin" CssClass="btn btn-default p-1 oi oi-action-undo"
                                    runat="server" HeaderText="*" ToolTip="Reset the Bin to blank" />
                                <asp:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" TargetControlID="ImageResetBin"
                                    ConfirmText="Are you sure you want to reset the bin to blank?" />
                                <asp:LinkButton ID="ImageCorrect" CssClass="btn btn-default p-1 oi oi-wrench" runat="server"
                                    HeaderText="*" ToolTip="Run Utility to correct known consistency issues" />--%>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </asp:Panel>
            <asp:TabContainer ID="TabContainerListName" runat="server" CssClass="tab-container" Visible="false">
                <asp:TabPanel ID="TabLocked" runat="server" HeaderText="Locked Batches" CssClass="tab-panel" >
                    <ContentTemplate>
                        <asp:Label ID="lblMessageLock" runat="server" Text=""></asp:Label>
                        <asp:Button ID="btnRefreshLocked" runat="server" Text="Refresh" Width="100%"  CssClass="w-100"/>
                        <asp:Panel ID="pnlMainGridPN" runat="server" Style="overflow: auto; max-height: 500px; width: auto;" HorizontalAlign="Left">
                            <asp:GridView ID="GRDLockedBatches" runat="server" Width="100%" DataKeyNames="Batch"
                                AutoGenerateColumns="False" CssClass="table" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
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
                                            <asp:ConfirmButtonExtender ID="ConfirmButtonExtender12xyx" runat="server" TargetControlID="imgHold"
                                                ConfirmText="Are you sure you want to move this batch to hold?">
                                            </asp:ConfirmButtonExtender>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Batch" HeaderText="Batch" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="freq" HeaderText="#" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Good" HeaderText="G" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="#009900">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Warnings" HeaderText="W" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="#996633">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Errors" HeaderText="E" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="#CC0000">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DDS" HeaderText="DD" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="#CC0000">
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
                                    <asp:BoundField DataField="GEN" HeaderText="GEN" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="Blue">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="New" HeaderText="New" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="Blue">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CPA" HeaderText="CPA" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="Blue">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CPB" HeaderText="CPB" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="Blue">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CPC" HeaderText="CPC" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="Blue">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PTG" HeaderText="PTG" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="Blue">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PTC" HeaderText="PTC" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="Blue">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PWR" HeaderText="PWR" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="Blue">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="BER" HeaderText="BER" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="Blue">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="BOA" HeaderText="BOA" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="Blue">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="REC" HeaderText="REC" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="Blue">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="C12" HeaderText="C12" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="Blue">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="C13" HeaderText="C13" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="Blue">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="C14" HeaderText="C14" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="Blue">
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
                        <asp:Button ID="btnSummaryLocked" runat="server" Text="Summary" Width="100%"  ToolTip="Download a Summary of all Open, Hold, Locked batches" CommandArgument="PIFullSummary" CssClass="w-100" />
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="TabInvalid" runat="server" HeaderText="Invalid Batches" CssClass="tab-panel" >
                    <ContentTemplate>
                        <asp:Label ID="lblMessageInvalid" runat="server" Text=""></asp:Label>
                        <asp:Button ID="btnRefreshInvalid" runat="server" Text="Refresh" Width="100%"  CssClass="w-100"/>
                        <asp:Panel ID="Panel1" runat="server" Style="overflow: auto; max-height: 500px; width: auto;"
                            HorizontalAlign="Left">
                            <asp:GridView ID="GRDInvalidBatches" runat="server" Width="100%" DataKeyNames="Batch"
                                AutoGenerateColumns="False" CssClass="table" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                AllowPaging="false" Font-Size="Smaller">
                                <SelectedRowStyle CssClass="srowstyle" />
                                <Columns>
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
                                    <asp:BoundField DataField="Batch" HeaderText="Batch" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                     <asp:BoundField DataField="freq" HeaderText="#" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Good" HeaderText="G" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="#009900">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Warnings" HeaderText="W" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="#996633">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Errors" HeaderText="E" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="#CC0000">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DDS" HeaderText="DD" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="#CC0000">
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
                                    <asp:BoundField DataField="GEN" HeaderText="GEN" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="Blue">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="New" HeaderText="New" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="Blue">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CPA" HeaderText="CPA" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="Blue">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CPB" HeaderText="CPB" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="Blue">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CPC" HeaderText="CPC" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="Blue">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PTG" HeaderText="PTG" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="Blue">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PTC" HeaderText="PTC" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="Blue">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PWR" HeaderText="PWR" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="Blue">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="BER" HeaderText="BER" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="Blue">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="BOA" HeaderText="BOA" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="Blue">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="REC" HeaderText="REC" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="Blue">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="C12" HeaderText="C12" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="Blue">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="C13" HeaderText="C13" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="Blue">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="C14" HeaderText="C14" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="Blue">
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
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="TabOpen" runat="server" HeaderText="Open Batches" CssClass="tab-panel" >
                    <ContentTemplate>
                        <asp:Label ID="lblMessageOpen" runat="server" Text=""></asp:Label>
                        <asp:Button ID="btnRefreshOpen" runat="server" Text="Refresh" Width="100%"  CssClass="w-100"/>
                        <asp:Panel ID="Panel2" runat="server" Style="overflow: auto; max-height: 500px; width: auto;"
                            HorizontalAlign="Left">
                            <asp:GridView ID="GRDOpenBatches" runat="server" Width="100%" DataKeyNames="Batch"
                                AutoGenerateColumns="False" CssClass="table" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
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
                                            <asp:ImageButton ID="imgLock" runat="server" HeaderText="" ImageUrl="~/Images/lock.png"
                                                ToolTip="Move to Lock." Width="15px"></asp:ImageButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgDownload" runat="server" HeaderText="" ImageUrl="~/Images/arrow_down.png"
                                                ToolTip="Download this batch" Width="15px"></asp:ImageButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Batch" HeaderText="Batch" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="freq" HeaderText="#" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Good" HeaderText="G" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="#009900">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Warnings" HeaderText="W" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="#996633">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Errors" HeaderText="E" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="#CC0000">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DDS" HeaderText="DD" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="#CC0000">
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
                                    <asp:BoundField DataField="GEN" HeaderText="GEN" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="Blue">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="New" HeaderText="New" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="Blue">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CPA" HeaderText="CPA" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="Blue">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CPB" HeaderText="CPB" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="Blue">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CPC" HeaderText="CPC" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="Blue">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PTG" HeaderText="PTG" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="Blue">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PTC" HeaderText="PTC" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="Blue">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PWR" HeaderText="PWR" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="Blue">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="BER" HeaderText="BER" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="Blue">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="BOA" HeaderText="BOA" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="Blue">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="REC" HeaderText="REC" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="Blue">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="C12" HeaderText="C12" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="Blue">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="C13" HeaderText="C13" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="Blue">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="C14" HeaderText="C14" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="Blue">
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
                        <asp:Button ID="btnSummaryOpen" runat="server" Text="Summary" Width="100%" ToolTip="Download a Summary of all Open, Hold, Locked batches"  CommandArgument="PIFullSummary" CssClass="w-100"/>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="TabHold" runat="server" HeaderText="Hold">
                    <ContentTemplate>
                        <asp:Label ID="lblMessageHold" runat="server" Text=""></asp:Label>
                        <asp:Button ID="btnRefreshHold" runat="server" Text="Refresh" Width="100%" CssClass="w-100" />
                        <asp:Panel ID="pnlMainGridPNHold" runat="server" Style="overflow: auto; max-height: 500px;
                            width: auto;" HorizontalAlign="Left">
                            <asp:GridView ID="GRDHoldBatches" runat="server" Width="100%" DataKeyNames="Batch"
                                AutoGenerateColumns="False" CssClass="table" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                AllowPaging="false" Font-Size="Smaller">
                                <SelectedRowStyle CssClass="srowstyle" />
                                <Columns>
                                    <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgLock" runat="server" HeaderText="" ImageUrl="~/Images/lock.png"
                                                ToolTip="Move to Locked." Width="15px"></asp:ImageButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgDownload" runat="server" HeaderText="" ImageUrl="~/Images/arrow_down.png"
                                                ToolTip="Download this batch" Width="15px"></asp:ImageButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
<%--                                    <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgOpen" runat="server" HeaderText="" ImageUrl="~/Images/Files.png"
                                                ToolTip="Return to Open Status." Width="15px"></asp:ImageButton>
                                            <asp:ConfirmButtonExtender ID="ConfirmButtonExtender11h" runat="server" TargetControlID="imgOpen"
                                                ConfirmText="Are you sure you want to return this batch to open?">
                                            </asp:ConfirmButtonExtender>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgClean" runat="server" HeaderText="" ImageUrl="~/Images/Clean.PNG"
                                                ToolTip="Clean" Width="15px"></asp:ImageButton>
                                            <asp:ConfirmButtonExtender ID="ConfirmButtonExtender12xhx" runat="server" TargetControlID="imgClean"
                                                ConfirmText="Are you sure you want to Clean this batch?">
                                            </asp:ConfirmButtonExtender>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgTransfer" runat="server" HeaderText="" ImageUrl="~/Images/uparrow_inline.gif"
                                                ToolTip="Send to IFS." Width="15px"></asp:ImageButton>
                                            <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1h2" runat="server" TargetControlID="imgTransfer"
                                                ConfirmText="Are you sure you want to Send this batch to IFS?">
                                            </asp:ConfirmButtonExtender>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Batch" HeaderText="Batch" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="freq" HeaderText="#" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Good" HeaderText="G" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="#009900">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Warnings" HeaderText="W" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="#996633">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Errors" HeaderText="E" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="#CC0000">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DDS" HeaderText="DD" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="#CC0000">
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
                                    <asp:BoundField DataField="GEN" HeaderText="GEN" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="Blue">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="New" HeaderText="New" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="Blue">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CPA" HeaderText="CPA" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="Blue">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CPB" HeaderText="CPB" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="Blue">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CPC" HeaderText="CPC" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="Blue">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PTG" HeaderText="PTG" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="Blue">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PTC" HeaderText="PTC" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="Blue">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PWR" HeaderText="PWR" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="Blue">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="BER" HeaderText="BER" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="Blue">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="BOA" HeaderText="BOA" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="Blue">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="REC" HeaderText="REC" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="Blue">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="C12" HeaderText="C12" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="Blue">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="C13" HeaderText="C13" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="Blue">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="C14" HeaderText="C14" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="Blue">
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
                        <asp:Button ID="btnSummaryHold" runat="server" Text="Summary" Width="100%"  ToolTip="Download a Summary of all Open, Hold, Locked batches" CommandArgument="PIFullSummary" CssClass="w-100"/>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="TabSent" runat="server" HeaderText="Sent" CssClass="tab-panel" >
                    <ContentTemplate>
                        <asp:Label ID="lblMessageSent" runat="server" Text=""></asp:Label>
                        <asp:Button ID="btnRefreshSent" runat="server" Text="Refresh" Width="100%"  CssClass="w-100"/>
                        <asp:Panel ID="Panel3" runat="server" Style="overflow: auto; max-height: 500px; width: auto;"
                            HorizontalAlign="Left">
                            <asp:GridView ID="GRDSentBatches" runat="server" Width="100%" DataKeyNames="Batch"
                                AutoGenerateColumns="False" CssClass="table" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                AllowPaging="false" Font-Size="Smaller">
                                <SelectedRowStyle CssClass="srowstyle" />
                                <Columns>
                                    <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgDownload" runat="server" HeaderText="" ImageUrl="~/Images/arrow_down.png"
                                                ToolTip="Download this batch" Width="15px"></asp:ImageButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Batch" HeaderText="Batch" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                     <asp:BoundField DataField="freq" HeaderText="#" HeaderStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Good" HeaderText="G" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="#009900">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Warnings" HeaderText="W" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="#996633">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Errors" HeaderText="E" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="#CC0000">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DDS" HeaderText="DD" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="#CC0000">
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
                                    <asp:BoundField DataField="GEN" HeaderText="GEN" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="Blue">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="New" HeaderText="New" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="Blue">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CPA" HeaderText="CPA" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="Blue">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CPB" HeaderText="CPB" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="Blue">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CPC" HeaderText="CPC" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="Blue">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PTG" HeaderText="PTG" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="Blue">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PTC" HeaderText="PTC" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="Blue">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PWR" HeaderText="PWR" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="Blue">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="BER" HeaderText="BER" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="Blue">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="BOA" HeaderText="BOA" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="Blue">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="REC" HeaderText="REC" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="Blue">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="C12" HeaderText="C12" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="Blue">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="C13" HeaderText="C13" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="Blue">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="C14" HeaderText="C14" HeaderStyle-HorizontalAlign="Left" ItemStyle-ForeColor="Blue">
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
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="js" runat="server">

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


        function OpenUnit(ID, PID, ProcessName) {

            if (ID.length == 0 || PID.length == 0) { return; }

            //var pstring = GetParameterStream(GetReportParameterList("CLIENTSUBMIT"));
            //var pstring = "ESN=A00000291E3942&ID=3251&PID=1"

            var pstring = "ID=" + ID + "&PID=" + PID + "&PName=" + ProcessName;
            //var pstring = "ESN=A00000291E3942&ID=3251&PID=5"


            // var WindowToOpen = "RPT_SpotCountReport.aspx";
            var WindowToOpen = "/Receive.aspx";

            if (pstring.length > 0) {
                WindowToOpen = WindowToOpen + "?" + pstring
            }
            var win = window.open(WindowToOpen, "_blank", "", true);
            //var win = window.open(WindowToOpen, "_blank", "menubar", true);
            return;
        }

        function OpenUnitPartScreen(ID, ESN) {
            if (ID.length == 0) { return; }

            //var pstring = GetParameterStream(GetReportParameterList("CLIENTSUBMIT"));
            //var pstring = "ESN=A00000291E3942&ID=3251&PID=1"

            var pstring = "ID=" + ID + "&ESN=" + ESN;
            //var pstring = "ESN=A00000291E3942&ID=3251&PID=5"
            // var WindowToOpen = "RPT_SpotCountReport.aspx";
            var WindowToOpen = "/ReceiveDetailEditParts.aspx";

            if (pstring.length > 0) {
                WindowToOpen = WindowToOpen + "?" + pstring
            }
            var win = window.open(WindowToOpen, "_blank", "", true);
            //var win = window.open(WindowToOpen, "_blank", "menubar", true);
            return;
        }

        ///////////////////////////////////////////////////////////////////

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

        function OpenUnitAnalysisRPT(cmdText) {
            var xDataList = {};
            xDataList["RPT"] = "UNITANALYSIS";
            xDataList["ID"] = cmdText;
            var pstring = GetParameterStream(xDataList);
            // var WindowToOpen = "RPT_SpotCountReport.aspx";
            var WindowToOpen = "/Reports/RPT_EXCEL_Out.aspx";
            if (pstring.length > 0) {
                WindowToOpen = WindowToOpen + "?" + pstring
            }
            var win = window.open(WindowToOpen, "_blank", "menubar", true);
            return;
        }

        function OpenbagTag(cmdText) {
            var xDataList = {};
            xDataList["RPT"] = "Bagtag";
            xDataList["RDID"] = cmdText;
            xDataList["ISTHREAD"] = "N";
            var pstring = GetParameterStream(xDataList);

            var WindowToOpen = '/BagTag.aspx';
            if (pstring.length > 0) {
                WindowToOpen = WindowToOpen + '?' + pstring
            }
            var win = window.open(WindowToOpen, '_blank', 'menubar', true);
        }

        function OpenKitting(cmdText) {
            var xDataList = {};
            xDataList["RPT"] = "PRODUCTLABEL";
            xDataList["RDID"] = cmdText;
            xDataList["ISTHREAD"] = "N";
            var pstring = GetParameterStream(xDataList);
            var WindowToOpen = '/FinishProductLabel.aspx';
            if (pstring.length > 0) {
                WindowToOpen = WindowToOpen + '?' + pstring
            }
            var win = window.open(WindowToOpen, '_blank', 'menubar', true);

        }

        //        function ResetBin(cmdText) {
        //            alert(cmdText);
        ////            var xDataList = {};
        ////            xDataList["RPT"] = "UNITANALYSIS";
        ////            xDataList["ID"] = cmdText;
        ////            var pstring = GetParameterStream(xDataList);
        ////            // var WindowToOpen = "RPT_SpotCountReport.aspx";
        ////            var WindowToOpen = "RPT_EXCEL_Out.aspx";
        ////            if (pstring.length > 0) {
        ////                WindowToOpen = WindowToOpen + "?" + pstring
        ////            }
        ////            var win = window.open(WindowToOpen, "_blank", "menubar", true);
        ////            return;
        //        }


    </script>

</asp:Content>

