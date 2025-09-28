<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard_TechLab.aspx.cs" Inherits="BW_WebApp.Dashboard_TechLab" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Syncfusion.Shared.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89" Namespace="Syncfusion.Web.UI.WebControls.Shared" TagPrefix="syncfusion" %>
<asp:Content ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="cMC" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            
            
            <h1><asp:Label ID="lblName" runat="server" Text="Tech Lab Dashboard" /></h1>
            
            <label class="d-block">Client:</label>
            <asp:DropDownList ID="drpClientList" CssClass="w-md-50" runat="server" />
            <asp:Button ID="btnRefresh" runat="server" Text="Refresh" />

            <asp:TabContainer runat="server" ID="TabList" CssClass="tab-container" ActiveTabIndex="0">
                <asp:TabPanel runat="server" ID="TabWaiting" CssClass="tab-panel" HeaderText="Waiting" Enabled="true">
                    <ContentTemplate>
                        <h3><asp:Label runat="server" Text="ESN / IMEI Records" /></h3>

                        <asp:LinkButton ID="imgPrintListWaiting" CssClass="btn btn-default" runat="server" ToolTip="Print List" Enabled="False">
                            <span class="oi oi-print"></span>
                        </asp:LinkButton>

                        <asp:GridView ID="gvWaiting" CssClass="table" runat="server" AutoGenerateColumns="False">
                            <Columns>
                                <asp:BoundField DataField="AuthorizationStatus" HeaderText="Authorization Status" />
                                <asp:BoundField DataField="Name" HeaderText="Location" />
                                <asp:BoundField DataField="ProjectName" HeaderText="Project" />
                                <asp:BoundField DataField="UnitStatus" HeaderText="Unit Status" />
                                <asp:BoundField DataField="ProcessDays" HeaderText="Days" />
                                <asp:BoundField DataField="RMANumber" HeaderText="RMA/Client Ref #" />
                                <asp:BoundField DataField="ESN" HeaderText="ESN/IMEI" />
                                <asp:BoundField DataField="Bin" HeaderText="Bin" />
                                <asp:BoundField DataField="Location" HeaderText="Loc" />
                                <asp:BoundField DataField="ReceiveDate" HeaderText="Date Submitted" />
                                <%--<asp:BoundField DataField="Status" HeaderText="Authorization" />--%>
                                <%--<asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="imgPrint" CssClass="btn btn-default" runat="server" ToolTip="Print">
                                            <span class="oi oi-print"></span>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Open">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="imgOpen" CssClass="btn btn-default" runat="server" ToolTip="Open Process">
                                            <span class="oi oi-plus"></span>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="email">
                                    <ItemTemplate>
                                        <div runat="server" id="divEmail">
                                            <a href="mailto:xxxxxxxxx@gmpi.ca" id="href_Waiting">Email</a>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:TabPanel>

                <asp:TabPanel runat="server" ID="TabAWaiting" CssClass="tab-panel" HeaderText="Waiting Authorization" Enabled="true">
                    <ContentTemplate>
                        <h3><asp:Label runat="server" Text="ESN / IMEI Records" /></h3>

                        <asp:GridView ID="gvAWaiting" CssClass="table" runat="server" AutoGenerateColumns="False">
                            <Columns>
                                <asp:BoundField DataField="AuthorizationStatus" HeaderText="Authorization Status" />
                                <asp:BoundField DataField="Name" HeaderText="Location" />
                                <asp:BoundField DataField="ProjectName" HeaderText="Project" />
                                <asp:BoundField DataField="UnitStatus" HeaderText="Unit Status" />
                                <asp:BoundField DataField="ProcessDays" HeaderText="Days" />
                                <asp:BoundField DataField="RMANumber" HeaderText="RMA/Client Ref #" />
                                <asp:BoundField DataField="ESN" HeaderText="ESN/IMEI" />
                                <asp:BoundField DataField="Bin" HeaderText="Bin" />
                                <asp:BoundField DataField="Location" HeaderText="Loc" />
                                <asp:BoundField DataField="ReceiveDate" HeaderText="Date Submitted" />
                                <%--<asp:BoundField DataField="Status" HeaderText="Authorization" />--%>
                                <%--<asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="imgPrint" CssClass="btn btn-default" runat="server" ToolTip="Print">
                                            <span class="oi oi-print"></span>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Open">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="imgOpen" CssClass="btn btn-default" runat="server" ToolTip="Open Process">
                                            <span class="oi oi-plus"></span>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Auth/Decline" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="imgAuthorize" CssClass="btn btn-default" runat="server" ToolTip="Authorize">
                                            <span class="oi oi-circle-check"></span>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="imgDecline" CssClass="btn btn-default" runat="server" ToolTip="Decline">
                                            <span class="oi oi-circle-x"></span>
                                        </asp:LinkButton>
                                        <%--<asp:LinkButton ID="imgComplete" CssClass="btn btn-default" runat="server" ToolTip="Complete">
                                            <span class="oi oi-check"></span>
                                        </asp:LinkButton>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="email">
                                    <ItemTemplate>
                                        <div runat="server" id="divEmail">
                                            <a href="mailto:xxxxxxxxx@gmpi.ca" id="href_Waiting">Email</a>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:TabPanel>

                <asp:TabPanel runat="server" ID="TabADeclined" CssClass="tab-panel" HeaderText="Authorization Declined" Enabled="true">
                    <ContentTemplate>
                        <h3><asp:Label runat="server" Text="ESN / IMEI Records" /></h3>

                        <asp:GridView ID="vgADeclined" CssClass="table" runat="server" AutoGenerateColumns="False">
                            <Columns>
                                <asp:BoundField DataField="AuthorizationStatus" HeaderText="Authorization Status" />
                                <asp:BoundField DataField="Name" HeaderText="Location" />
                                <asp:BoundField DataField="ProjectName" HeaderText="Project" />
                                <asp:BoundField DataField="UnitStatus" HeaderText="Unit Status" />
                                <asp:BoundField DataField="ProcessDays" HeaderText="Days" />
                                <asp:BoundField DataField="RMANumber" HeaderText="RMA/Client Ref #" />
                                <asp:BoundField DataField="ESN" HeaderText="ESN/IMEI" />
                                <asp:BoundField DataField="Bin" HeaderText="Bin" />
                                <asp:BoundField DataField="Location" HeaderText="Loc" />
                                <asp:BoundField DataField="ReceiveDate" HeaderText="Date Submitted" />
                                <%--<asp:BoundField DataField="Status" HeaderText="Authorization" />--%>
                                <%--<asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="imgPrint" CssClass="btn btn-default" runat="server" ToolTip="Print">
                                            <span class="oi oi-print"></span>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Open">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="imgOpen" CssClass="btn btn-default" runat="server" ToolTip="Open Process">
                                            <span class="oi oi-plus"></span>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="email">
                                    <ItemTemplate>
                                        <div runat="server" id="divEmail">
                                            <a href="mailto:xxxxxxxxx@gmpi.ca" id="href_Waiting">Email</a>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:TabPanel>

                <asp:TabPanel runat="server" ID="TabAApproved" CssClass="tab-panel" HeaderText="Authorization Approved" Enabled="true">
                    <ContentTemplate>
                        <h3><asp:Label runat="server" Text="ESN / IMEI Records" /></h3>

                        <asp:GridView ID="gvAApproved" CssClass="table" runat="server" AutoGenerateColumns="False">
                            <Columns>
                                <asp:BoundField DataField="AuthorizationStatus" HeaderText="Authorization Status" />
                                <asp:BoundField DataField="Name" HeaderText="Location" />
                                <asp:BoundField DataField="ProjectName" HeaderText="Project" />
                                <asp:BoundField DataField="UnitStatus" HeaderText="Unit Status" />
                                <asp:BoundField DataField="ProcessDays" HeaderText="Days" />
                                <asp:BoundField DataField="RMANumber" HeaderText="RMA/Client Ref #" />
                                <asp:BoundField DataField="ESN" HeaderText="ESN/IMEI" />
                                <asp:BoundField DataField="Bin" HeaderText="Bin" />
                                <asp:BoundField DataField="Location" HeaderText="Loc" />
                                <asp:BoundField DataField="ReceiveDate" HeaderText="Date Submitted" />
                                <%--<asp:BoundField DataField="Status" HeaderText="Authorization" />--%>
                                <%--<asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="imgPrint" CssClass="btn btn-default" runat="server" ToolTip="Print">
                                            <span class="oi oi-print"></span>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Open">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="imgOpen" CssClass="btn btn-default" runat="server" ToolTip="Open Process">
                                            <span class="oi oi-plus"></span>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Auth Complete" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="imgComplete" CssClass="btn btn-default" runat="server" ToolTip="Complete">
                                            <span class="oi oi-check"></span>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="email">
                                    <ItemTemplate>
                                        <div runat="server" id="divEmail">
                                            <a href="mailto:xxxxxxxxx@gmpi.ca" id="href_Waiting">Email</a>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:TabPanel>

                <asp:TabPanel runat="server" ID="TabAcquired" CssClass="tab-panel" HeaderText="Acquired" Enabled="true">
                    <ContentTemplate>
                        <h3><asp:Label runat="server" Text="ESN / IMEI Records" /></h3>

                        <asp:GridView ID="gvAcquired" CssClass="table" runat="server" AutoGenerateColumns="False">
                            <Columns>
                                <asp:BoundField DataField="AuthorizationStatus" HeaderText="Authorization Status" />
                                <asp:BoundField DataField="Name" HeaderText="Location" />
                                <asp:BoundField DataField="ProjectName" HeaderText="Project" />
                                <asp:BoundField DataField="UnitStatus" HeaderText="Unit Status" />
                                <asp:BoundField DataField="ProcessDays" HeaderText="Days" />
                                <asp:BoundField DataField="RMANumber" HeaderText="RMA/Client Ref #" />
                                <asp:BoundField DataField="ESN" HeaderText="ESN/IMEI" />
                                <asp:BoundField DataField="Bin" HeaderText="Bin" />
                                <asp:BoundField DataField="Location" HeaderText="Loc" />
                                <asp:BoundField DataField="ReceiveDate" HeaderText="Date Submitted" />
                                <%--<asp:BoundField DataField="Status" HeaderText="Authorization" />--%>
                                <%--<asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="imgPrint" CssClass="btn btn-default" runat="server" ToolTip="Print">
                                            <span class="oi oi-print"></span>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Open">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="imgOpen" CssClass="btn btn-default" runat="server" ToolTip="Open Process">
                                            <span class="oi oi-plus"></span>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="email">
                                    <ItemTemplate>
                                        <div runat="server" id="divEmail">
                                            <a href="mailto:xxxxxxxxx@gmpi.ca" id="href_Waiting">Email</a>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:TabPanel>

                <asp:TabPanel runat="server" ID="TabAComplete" CssClass="tab-panel" HeaderText="Authorization Complete" Enabled="true">
                    <ContentTemplate>
                        <h3><asp:Label runat="server" Text="ESN / IMEI Records" /></h3>

                        <asp:GridView ID="gvAComplete" CssClass="table" runat="server" AutoGenerateColumns="False">
                            <Columns>
                                <asp:BoundField DataField="AuthorizationStatus" HeaderText="Authorization Status" />
                                <asp:BoundField DataField="Name" HeaderText="Location" />
                                <asp:BoundField DataField="ProjectName" HeaderText="Project" />
                                <asp:BoundField DataField="UnitStatus" HeaderText="Unit Status" />
                                <asp:BoundField DataField="ProcessDays" HeaderText="Days" />
                                <asp:BoundField DataField="RMANumber" HeaderText="RMA/Client Ref #" />
                                <asp:BoundField DataField="ESN" HeaderText="ESN/IMEI" />
                                <asp:BoundField DataField="Bin" HeaderText="Bin" />
                                <asp:BoundField DataField="Location" HeaderText="Loc" />
                                <asp:BoundField DataField="ReceiveDate" HeaderText="Date Submitted" />
                                <%--<asp:BoundField DataField="Status" HeaderText="Authorization" />--%>
                                <%--<asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="imgPrint" CssClass="btn btn-default" runat="server" ToolTip="Print">
                                            <span class="oi oi-print"></span>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Open">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="imgOpen" CssClass="btn btn-default" runat="server" ToolTip="Open Process">
                                            <span class="oi oi-plus"></span>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="email">
                                    <ItemTemplate>
                                        <div runat="server" id="divEmail">
                                            <a href="mailto:xxxxxxxxx@gmpi.ca" id="href_Waiting">Email</a>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:TabPanel>

                <asp:TabPanel runat="server" ID="TabWaitingParts" CssClass="tab-panel" HeaderText="Waiting for Parts" Enabled="true">
                    <ContentTemplate>
                        <h3><asp:Label runat="server" Text="ESN / IMEI Records" /></h3>

                        <asp:GridView ID="gvWaitingParts" CssClass="table" runat="server" AutoGenerateColumns="False">
                            <Columns>
                                <asp:BoundField DataField="AuthorizationStatus" HeaderText="Authorization Status" />
                                <asp:BoundField DataField="Name" HeaderText="Location" />
                                <asp:BoundField DataField="ProjectName" HeaderText="Project" />
                                <asp:BoundField DataField="UnitStatus" HeaderText="Unit Status" />
                                <asp:BoundField DataField="ProcessDays" HeaderText="Days" />
                                <asp:BoundField DataField="RMANumber" HeaderText="RMA/Client Ref #" />
                                <asp:BoundField DataField="ESN" HeaderText="ESN/IMEI" />
                                <asp:BoundField DataField="Bin" HeaderText="Bin" />
                                <asp:BoundField DataField="Location" HeaderText="Loc" />
                                <asp:BoundField DataField="ReceiveDate" HeaderText="Date Submitted" />
                                <%--<asp:BoundField DataField="Status" HeaderText="Authorization" />--%>
                                <%--<asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="imgPrint" CssClass="btn btn-default" runat="server" ToolTip="Print">
                                            <span class="oi oi-print"></span>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Open">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="imgOpen" CssClass="btn btn-default" runat="server" ToolTip="Open Process">
                                            <span class="oi oi-plus"></span>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="email">
                                    <ItemTemplate>
                                        <div runat="server" id="divEmail">
                                            <a href="mailto:xxxxxxxxx@gmpi.ca" id="href_Waiting">Email</a>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:TabPanel>

                <asp:TabPanel runat="server" ID="TabClosed" CssClass="tab-panel" HeaderText="Closed" Enabled="true">
                    <ContentTemplate>
                        <h3><asp:Label runat="server" Text="ESN / IMEI Records" /></h3>

                        <asp:GridView ID="gvClosed" CssClass="table" runat="server" AutoGenerateColumns="False">
                            <Columns>
                                <asp:BoundField DataField="AuthorizationStatus" HeaderText="Authorization Status" />
                                <asp:BoundField DataField="Name" HeaderText="Location" />
                                <asp:BoundField DataField="ProjectName" HeaderText="Project" />
                                <asp:BoundField DataField="UnitStatus" HeaderText="Unit Status" />
                                <asp:BoundField DataField="ProcessDays" HeaderText="Days" />
                                <asp:BoundField DataField="RMANumber" HeaderText="RMA/Client Ref #" />
                                <asp:BoundField DataField="ESN" HeaderText="ESN/IMEI" />
                                <asp:BoundField DataField="Bin" HeaderText="Bin" />
                                <asp:BoundField DataField="Location" HeaderText="Loc" />
                                <asp:BoundField DataField="ReceiveDate" HeaderText="Date Submitted" />
                                <%--<asp:BoundField DataField="Status" HeaderText="Authorization" />--%>
                                <%--<asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="imgPrint" CssClass="btn btn-default" runat="server" ToolTip="Print">
                                            <span class="oi oi-print"></span>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Open">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="imgOpen" CssClass="btn btn-default" runat="server" ToolTip="Open Process">
                                            <span class="oi oi-plus"></span>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="email">
                                    <ItemTemplate>
                                        <div runat="server" id="divEmail">
                                            <a href="mailto:xxxxxxxxx@gmpi.ca" id="href_Waiting">Email</a>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>

            <div id="wndAuthorizeRequired" class="modal" tabindex="-1" role="dialog">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Authorization Request</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <asp:HiddenField ID="HiddenField1" runat="server" />
                            <asp:HiddenField ID="HiddenField2" runat="server" />

                            <label>Estimate Fee:</label>
                            <asp:TextBox ID="txtEstimateFee" runat="server" />
                    
                            <label>Freight Fee:</label>
                            <asp:TextBox ID="txtFreightFee" runat="server" />
                    
                            <label>HST:</label>
                            <asp:TextBox ID="txtHST" runat="server" />
                    
                            <label>Total:</label>
                            <asp:TextBox ID="TxtTotal" runat="server" />
                    
                            <label>Note:</label>
                            <asp:TextBox ID="txtAuthorizeNote" runat="server" Wrap="True" TextMode="MultiLine" MaxLength="250" />
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnSaveAuthorize" runat="server" Text="Save" OnClientClick="Authorization_Save();" />
                            <asp:Button ID="btnCancelAuthorize" runat="server" Text="Cancel" OnClientClick="Authorization_Cancel(); return false;"/>
                        </div>
                    </div>
                </div>
            </div>
            <div id="wndAuthorizeApproved" class="modal" tabindex="-1" role="dialog">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Approve Authorization</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <asp:HiddenField ID="hdnAuthorizationNumberID" runat="server" />
                            <asp:HiddenField ID="hdnReceiveDetailID" runat="server" />
                            <asp:HiddenField ID="hdnUsername" runat="server" />

                            <label>Authoirzation Number:</label>
                            <asp:TextBox ID="txtAuthorizeNumber" runat="server" />
                    
                            <label>Authoirzation By:</label>
                            <asp:TextBox ID="txtAuthorizeBy" runat="server" />
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnAuthorizeOK" runat="server" Text="Switch" OnClientClick="Authorization_Save(); return false;" />
                            <asp:Button ID="btnAuthorizeCancel" runat="server" Text="Cancel" OnClientClick="Authorization_Cancel(); return false;" />
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ContentPlaceHolderID="js" runat="server">
    <script type="text/javascript">

        $('#wndAuthorizeApproved').modal('show');

        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

        function BeginRequestHandler(sender, args) {
            if (args._postBackElement.id != "SkinTable1") {
                //ConfigureWaitingPopup(Popup);
                $('#loading').show();
            }
        }

        function EndRequestHandler(sender, args) {

            $('#loading').hide();
        }

        function OpenAuthorizationRequired() {
            $('#wndAuthorizeRequired').modal('show');
        }

        function AuthorizationRequired_Cancel() {
            $('#wndAuthorizeRequired').modal('hide');
        }
        function AuthorizationRequired_Save() {
            $('#wndAuthorizeRequired').modal('hide');
        }
        
        function OpenAuthorization(AuthorizationNumberID, ReceiveDetailID, Username) {
            MCL('USERNAME').value = Username;
            MCL('HDNRECEIVEDETAILID').value = ReceiveDetailID;
            MCL('HDNAUTHORIZATIONNUMBERID').value = AuthorizationNumberID;

            $('#wndAuthorizeApproved').modal('show');
        }

        function Authorization_Cancel() {
            $('#wndAuthorizeApproved').modal('hide');
        }

        function Authorization_Save() {
            SetupToAuthorize(MCL('HDNAUTHORIZATIONNUMBERID').value, MCL('HDNRECEIVEDETAILID').value, MCL('USERNAME').value);
            $('#wndAuthorizeApproved').modal('hide');
        }

        function MCL(ControlName) {
            switch (ControlName.toUpperCase()) {
                case "USERNAME": return $get("<%= hdnUsername.ClientID %>"); break;
                case "HDNRECEIVEDETAILID": return $get("<%= hdnReceiveDetailID.ClientID %>"); break;
                case "HDNAUTHORIZATIONNUMBERID": return $get("<%= hdnAuthorizationNumberID.ClientID %>"); break;

                default: return null;
            }
        }

        function SetupToRequired(AuthorizationNumberID, ReceiveDetailID, Username) {
            var service = new WebServer_01();
            service.AuthorizeRequired(AuthorizationNumberID, ReceiveDetailID, Username, OnAuthorizeSuccess, null, null);
        }

        function SetupToComplete(AuthorizationNumberID, ReceiveDetailID, Username) {
            var service = new WebServer_01();
            service.AuthorizeRepair(AuthorizationNumberID, ReceiveDetailID, Username, OnAuthorizeSuccess, null, null);
        }

        function SetupToDecline(AuthorizationNumberID, ReceiveDetailID, Username) {
            var service = new WebServer_01();
            service.AuthorizeDecline(AuthorizationNumberID, ReceiveDetailID, Username, OnAuthorizeSuccess, null, null);
        }

        function SetupToAuthorize(AuthorizationNumberID, ReceiveDetailID, Username) {
            var service = new WebServer_01();
            service.AuthorizeRepairOnly(AuthorizationNumberID, ReceiveDetailID, Username, "XX", OnAuthorizeSuccess, null, null);
        }

        function OnAuthorizeSuccess(result) {
            var LineText = result;
            alert(result);
            //uppdateStatusPanel(LineText);
        }

        function OpenUnit(ID, PID, ProcessName) {

            if (ID.length == 0 || PID.length == 0) { return; }

            //var pstring = GetParameterStream(GetReportParameterList("CLIENTSUBMIT"));
            //var pstring = "ESN=A00000291E3942&ID=3251&PID=1"
            var pstring = "ID=" + ID + "&PID=" + PID + "&PName=" + ProcessName;
            //var pstring = "ESN=A00000291E3942&ID=3251&PID=5"
            // var WindowToOpen = "RPT_SpotCountReport.aspx";
            var WindowToOpen = "Receive.aspx";

            if (pstring.length > 0) {
                WindowToOpen = WindowToOpen + "?" + pstring
            }

            var win = window.open(WindowToOpen, "_blank", "", true);
            //var win = window.open(WindowToOpen, "_blank", "menubar", true);
            return;
        }

        function RecordContact(ID, Message, Note) {
            var service = new WebServer_01();
            service.RecordEmailContact(ID, Message, Note, MCL('Username').value, null, null, null);
        }

    </script>
</asp:Content>
