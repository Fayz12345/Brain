<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard_MasterPartsRequests.aspx.cs" Inherits="BW_WebApp.Dashboard_MasterPartsRequests" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Syncfusion.Shared.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89" Namespace="Syncfusion.Web.UI.WebControls.Shared" TagPrefix="syncfusion" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>

<asp:Content ContentPlaceHolderID="cMC" runat="server">
    <asp:HiddenField ID="hdnUserName" runat="server" ClientIDMode="Static" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            

            <asp:Label ID="lblMessagex" runat="server" Text="" />

            <h1>Requested Parts - Dashboard</h1>

            <asp:TabContainer runat="server" ID="tabMain" CssClass="tab-container" ActiveTabIndex="0" AutoPostBack="False">

                <asp:TabPanel runat="server" ID="TabPanelNew" CssClass="tab-panel" Enabled="true" HeaderText="Pending" Visible="True">
                    <ContentTemplate>
                        <asp:Panel ID="pnlMainGridPN" runat="server">
                            <asp:Button ID="btnRefreshNewList" runat="server" Text="Refresh" />
                            <asp:Panel ID="pnlSearchSummary" runat="server">

                                <asp:GridView ID="grdNewData" CssClass="table" runat="server" DataKeyNames="MasterPartsRequestedLogID" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:BoundField DataField="MasterPartsRequestedLogID" HeaderText="ID" ReadOnly="True" Visible="false" />
                                        <asp:BoundField DataField="ReceiveDetailID" HeaderText="ID" ReadOnly="True" Visible="false" />
                                        <asp:TemplateField HeaderText="P">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="imgAssignPart" runat="server" ToolTip="Pick Part">
                                                    <span class="oi oi-arrow-circle-right"></span>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="TechUser" HeaderText="Tech Name" ReadOnly="True" />
                                        <asp:BoundField DataField="CreateDate" HeaderText="Requested" ReadOnly="True" />
                                        <asp:BoundField DataField="ESN" HeaderText="ESN" ReadOnly="True" />
                                        <asp:BoundField DataField="RequestedPart" HeaderText="RequestedPart" ReadOnly="True" />
                                        <asp:BoundField DataField="PartNote" HeaderText="PartNote" ReadOnly="True" />
                                        <asp:BoundField DataField="Manufacturer" HeaderText="Manufacturer" ReadOnly="True" />
                                        <asp:BoundField DataField="Model" HeaderText="Model" ReadOnly="True" />
                                        <asp:BoundField DataField="Colour" HeaderText="Colour" ReadOnly="True" />
                                        <asp:BoundField DataField="Carrier" HeaderText="Carrier" ReadOnly="True" />
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:TabPanel>

                <asp:TabPanel runat="server" ID="TabPanelPick" CssClass="tab-panel" Enabled="true" HeaderText="Picker Assigned" Visible="True">
                    <ContentTemplate>
                        <asp:Panel ID="GridData" runat="server">
                            <asp:Button ID="btnRefreshDetail" runat="server" Text="Refresh" />
                            <asp:Panel ID="pnlSearchDetail" runat="server">
                                <asp:GridView ID="grdAssignedParts" CssClass="table" runat="server" DataKeyNames="MasterPartsRequestedLogID" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:BoundField DataField="MasterPartsRequestedLogID" HeaderText="ID" ReadOnly="True" Visible="false" />
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>

                                                <asp:LinkButton ID="imgFill" runat="server" ToolTip="Fill Order">
                                                    <span class="oi oi-arrow-circle-right"></span>
                                                </asp:LinkButton>

                                                <asp:LinkButton ID="imgOutOfStock" runat="server" ToolTip="Out of Stock">
                                                    <span class="oi oi-warning"></span>
                                                </asp:LinkButton>

                                                <asp:ConfirmButtonExtender runat="server" TargetControlID="imgOutOfStock"
                                                    ConfirmText="Are you sure you want to set this request as out of stock?" />

                                                <asp:LinkButton ID="imgCancel" runat="server" ToolTip="Cancel">
                                                    <span class="oi oi-x"></span>
                                                </asp:LinkButton>

                                                <asp:ConfirmButtonExtender runat="server" TargetControlID="imgCancel"
                                                    ConfirmText="Are you sure you want to cancel this request?" />

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Status" HeaderText="Status" ReadOnly="True" />
                                        <asp:BoundField DataField="TechUser" HeaderText="Tech Name" ReadOnly="True" />
                                        <asp:BoundField DataField="ESN" HeaderText="ESN" ReadOnly="True" />
                                        <asp:BoundField DataField="RequestedPart" HeaderText="RequestedPart" ReadOnly="True" />
                                        <asp:BoundField DataField="PartNote" HeaderText="PartNote" ReadOnly="True" />
                                        <asp:BoundField DataField="ReceiveDetailID" HeaderText="ID" ReadOnly="True" Visible="false" />
                                        <asp:BoundField DataField="Manufacturer" HeaderText="Manufacturer" ReadOnly="True" />
                                        <asp:BoundField DataField="Model" HeaderText="Model" ReadOnly="True" />
                                        <asp:BoundField DataField="Colour" HeaderText="Colour" ReadOnly="True" />
                                        <asp:BoundField DataField="Carrier" HeaderText="Carrier" ReadOnly="True" />
                                        <asp:BoundField DataField="CreateDate" HeaderText="Requested" ReadOnly="True" />
                                        <asp:BoundField DataField="OutOfStockDate" HeaderText="Out Of Stock Date" ReadOnly="True" />
                                        <asp:BoundField DataField="OutOfStockUser" HeaderText="Out Of Stock User" ReadOnly="True" />
                                        <asp:BoundField DataField="PickUser" HeaderText="PickUser" ReadOnly="True" />
                                        <asp:BoundField DataField="PickDate" HeaderText="PickDate" ReadOnly="True" />
                                        <asp:BoundField DataField="LastUpdateDate" HeaderText="LastUpdateDate" ReadOnly="True" />
                                        <asp:BoundField DataField="LastUpdateUser" HeaderText="LastUpdateUser" ReadOnly="True" />
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                        </asp:Panel>

                        <asp:Panel ID="AssignScreen" runat="server" Visible="false">
                            <asp:HiddenField ID="KeyID" runat="server" />
                            <asp:HiddenField ID="hdnManufacturerID" runat="server" />
                            <asp:HiddenField ID="hdnModelID" runat="server" />

                            <asp:Label runat="server" Text="Manufacturer:" />
                            <asp:Label ID="lblManufacturer" runat="server" Text="Manufacturer:" />

                            <asp:LinkButton ID="imgbuttonOutOfStock" runat="server" ToolTip="Set as Out of Stock">
                                <span class="oi oi-warning"></span>
                            </asp:LinkButton>

                            <asp:ConfirmButtonExtender runat="server" TargetControlID="imgbuttonOutOfStock"
                                ConfirmText="Are you sure you want to set this request as out of stock?" />

                            <asp:LinkButton ID="imgButtonCancel" runat="server" ToolTip="Cancel Request Order">
                                <span class="oi oi-x"></span>
                            </asp:LinkButton>

                            <asp:ConfirmButtonExtender runat="server" TargetControlID="imgButtonCancel"
                                ConfirmText="Are you sure you want to cancel this request?" />
                            
                            <asp:Label runat="server" Text="Model:" />
                            <asp:Label ID="lblModel" runat="server" Text="Model" />
                            
                            <asp:Label runat="server" Text="Colour:" />
                            <asp:Label ID="lblColour" runat="server" Text="Colour" />
                            
                            <asp:Label runat="server" Text="Part:" />
                            <asp:Label ID="lblPartRequested" runat="server" Text="Part" />
                            
                            <asp:Label runat="server" Text="Note:" />
                            <asp:Label ID="lblPartNote" runat="server" Text="Note" />
                            
                            <asp:Label runat="server" Text="Technician:" />
                            <asp:Label ID="lblRequestUser" runat="server" Text="Tech:" />
                            
                            <asp:Label runat="server" Text="Device Location:" />
                            <asp:Label ID="lblDeviceLocation" runat="server" Text="" />

                            <%--<asp:Label runat="server" Text="Part Number:" AssociatedControlID="txtPartNumber" />
                            <asp:TextBox ID="txtPartNumber" runat="server" ClientIDMode="Static" />
                            
                            <asp:Label runat="server" Text="From IFS Location" AssociatedControlID="drpLocationEditParts" />
                            <asp:DropDownList ID="drpLocationEditParts" runat="server" ToolTip="IFS Location where these parts are located Now"/>
                            
                            <asp:Label runat="server" Text="To IFSsLocation" AssociatedControlID="drpLocationEditPartsTo" />
                            <asp:DropDownList ID="drpLocationEditPartsTo" runat="server" ToolTip="IFS Location where these parts are going" />--%>
                            
                            <%--<asp:Button ID="btnAssign" runat="server" Text="Assign" />--%>
                            <asp:Button ID="btnCancel" runat="server" Text="Back" />

                            <asp:Label runat="server" Text="Warehouse Location" AssociatedControlID="drpLocationList" />
                            <asp:DropDownList ID="drpLocationList" runat="server" ToolTip="Warehouse Location where these parts are located" AutoPostBack="true" />
                            
                            <asp:Label runat="server" Text="To Location" AssociatedControlID="IFSToLocation" />
                            <asp:TextBox ID="IFSToLocation" runat="server" ClientIDMode="Static" />

                            <asp:Label runat="server" Text="Search Category" />
                            <asp:DropDownList ID="drpDropPart_03" runat="server" ToolTip="Drop Part" AutoPostBack="true" />
                            <asp:Button ID="btnRefresh" runat="server" Text="Go" />
                            
                            <asp:Panel runat="server">
                                <asp:TabContainer runat="server" CssClass="tab-container" ActiveTabIndex="0" AutoPostBack="False">

                                    <asp:TabPanel runat="server" CssClass="tab-panel" Enabled="true" HeaderText="Location" Visible="True">
                                        <ContentTemplate>
                                            <asp:GridView ID="MainGridPN" CssClass="table" runat="server" DataKeyNames="MasterPartsLinkTableID" AutoGenerateColumns="False">
                                                <Columns>
                                                    <asp:BoundField DataField="MasterPartsID" HeaderText="ID" ReadOnly="True" Visible="false">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="P" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>

                                                        <asp:LinkButton ID="imgAssignPart" runat="server" ToolTip="Assign Part">
                                                            <span class="oi oi-arrow-circle-right"></span>
                                                        </asp:LinkButton>

                                                        <%--<asp:LinkButton ID="imgOutOfStock" runat="server" ToolTip="Out of Stock">
                                                            <span class="oi oi-warning"></span>
                                                        </asp:LinkButton>

                                                        <asp:LinkButton ID="imgCancel" runat="server" ToolTip="Cancel">
                                                            <span class="oi oi-x"></span>
                                                        </asp:LinkButton>--%>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="QTY" HeaderText="QTY" ReadOnly="True" />
                                                    <asp:BoundField DataField="IFSLocation" HeaderText="Location" ReadOnly="True" />
                                                    <asp:BoundField DataField="PartNumber" HeaderText="MFG Part Number" ReadOnly="True" />
                                                    <asp:BoundField DataField="GMPPartNumber" HeaderText="IMM Part Number" ReadOnly="True" />
                                                    <asp:BoundField DataField="GMPPartDescription" HeaderText="Desc" ReadOnly="True" />
                                                    <%--<asp:BoundField DataField="Quantity" HeaderText="Bulk QTY" ReadOnly="True" />--%>
                                                    <%--<asp:BoundField DataField="Model" HeaderText="Model" ReadOnly="True" />--%>
                                                </Columns>
                                            </asp:GridView>
                                        </ContentTemplate>
                                    </asp:TabPanel>

                                    <asp:TabPanel runat="server" CssClass="tab-panel" Enabled="true" HeaderText="Other" Visible="False">
                                        <ContentTemplate>
                                            <asp:Label runat="server" Text="From Location" AssociatedControlID="IFSFromLocation" />
                                            <asp:TextBox ID="IFSFromLocation" runat="server" ClientIDMode="Static" />

                                            <%--<asp:DropDownList ID="drpDropPart_03O" runat="server" ToolTip="Drop Part" AutoPostBack="true" />
                                            <asp:Button ID="btnRefreshO" runat="server" Text="Go" />--%>

                                            <asp:GridView ID="MainGridPNOther" CssClass="table" runat="server" DataKeyNames="MasterPartsLinkTableID"
                                                AutoGenerateColumns="False">
                                                <Columns>
                                                    <asp:BoundField DataField="MasterPartsID" HeaderText="ID" ReadOnly="True" Visible="false" />
                                                    <asp:TemplateField HeaderText="P">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="imgAssignPart" runat="server" ToolTip="Pick Part">
                                                                <span class="oi oi-arrow-circle-right"></span>
                                                            </asp:LinkButton>
                                                            <%--<asp:LinkButton ID="imgOutOfStock" runat="server" ToolTip="Out of Stock">
                                                                <span class="oi oi-warning"></span>
                                                            </asp:LinkButton>
                                                            <asp:LinkButton ID="imgCancel" runat="server" ToolTip="Cancel">
                                                                <span class="oi oi-x"></span>
                                                            </asp:LinkButton>--%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Quantity" HeaderText="Bulk QTY" ReadOnly="True" />
                                                    <%--<asp:BoundField DataField="QTY" HeaderText="QTY" ReadOnly="True" />--%>
                                                    <%--<asp:BoundField DataField="IFSLocation" HeaderText="Location" ReadOnly="True" />--%>
                                                    <asp:BoundField DataField="PartNumber" HeaderText="MFG Part Number" ReadOnly="True" />
                                                    <asp:BoundField DataField="GMPPartNumber" HeaderText="IMM Part Number" ReadOnly="True" />
                                                    <asp:BoundField DataField="GMPPartDescription" HeaderText="Desc" ReadOnly="True" />
                                                    <%--<asp:BoundField DataField="Model" HeaderText="Model" ReadOnly="True" />--%>
                                                </Columns>
                                            </asp:GridView>
                                        </ContentTemplate>
                                    </asp:TabPanel>

                                </asp:TabContainer>
                            </asp:Panel>
                                
                        </asp:Panel>
                    </ContentTemplate>
                </asp:TabPanel>

                <asp:TabPanel runat="server" ID="TabPanel1" CssClass="tab-panel" Enabled="true" HeaderText="Out of Stock Requests" Visible="True">
                    <ContentTemplate>
                        <div class="row">
                        	<div class="col-md-6">
                                <asp:Label runat="server" Text="Search IMEI:" AssociatedControlID="txtIMEIToSearch" />
                                <asp:TextBox ID="txtIMEIToSearch" runat="server" />
                        
                                <asp:Label runat="server" Text="or Requested Part:" AssociatedControlID="txtIMEIToSearch" />
                                <asp:TextBox ID="txtRequestedpartToSearch" runat="server" />
                        
                                <asp:Label runat="server" Text="or Model:" AssociatedControlID="txtIMEIToSearch" />
                                <asp:TextBox ID="txtModelToSearch" runat="server" />
                        
                                <label class="d-block">Leave blank for all records.</label>
                        
                                <asp:Button ID="btnRefreshOutOfStock" runat="server" Text="Refresh" />
                            </div>
                        </div>
                        
                        <asp:GridView ID="grdOutOfStockParts" CssClass="table" runat="server" DataKeyNames="MasterPartsRequestedLogID"
                            AutoGenerateColumns="False">
                            <Columns>
                                <asp:BoundField DataField="MasterPartsRequestedLogID" HeaderText="ID" ReadOnly="True" Visible="false" />
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="imgFill" runat="server" ToolTip="Return to picked!">
                                            <span class="oi oi-arrow-circle-right"></span>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="imgCancel" runat="server" ToolTip="Cancel">
                                            <span class="oi oi-x"></span>
                                        </asp:LinkButton>
                                        <asp:ConfirmButtonExtender runat="server" TargetControlID="imgCancel"
                                            ConfirmText="Are you sure you want to cancel this request?" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Status" HeaderText="Status" ReadOnly="True" />
                                <asp:BoundField DataField="TechUser" HeaderText="Tech Name" ReadOnly="True" />
                                <asp:BoundField DataField="ESN" HeaderText="ESN" ReadOnly="True" />
                                <asp:BoundField DataField="RequestedPart" HeaderText="RequestedPart" ReadOnly="True" />
                                <asp:BoundField DataField="PartNote" HeaderText="PartNote" ReadOnly="True" />
                                <asp:BoundField DataField="ReceiveDetailID" HeaderText="ID" ReadOnly="True" Visible="false" />
                                <asp:BoundField DataField="Manufacturer" HeaderText="Manufacturer" ReadOnly="True" />
                                <asp:BoundField DataField="Model" HeaderText="Model" ReadOnly="True" />
                                <asp:BoundField DataField="Colour" HeaderText="Colour" ReadOnly="True" />
                                <asp:BoundField DataField="Carrier" HeaderText="Carrier" ReadOnly="True" />
                                <asp:BoundField DataField="CreateDate" HeaderText="Requested" ReadOnly="True" />
                                <asp:BoundField DataField="OutOfStockDate" HeaderText="Out Of Stock Date" ReadOnly="True" />
                                <asp:BoundField DataField="OutOfStockUser" HeaderText="Out Of Stock User" ReadOnly="True" />
                                <asp:BoundField DataField="PickUser" HeaderText="PickUser" ReadOnly="True" />
                                <asp:BoundField DataField="PickDate" HeaderText="PickDate" ReadOnly="True" />
                                <asp:BoundField DataField="LastUpdateDate" HeaderText="LastUpdateDate" ReadOnly="True" />
                                <asp:BoundField DataField="LastUpdateUser" HeaderText="LastUpdateUser" ReadOnly="True" />
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:TabPanel>

                <asp:TabPanel runat="server" ID="TabPanelReturned" CssClass="tab-panel" Enabled="true" HeaderText="Tech Returned" Visible="True">
                    <ContentTemplate>
                        <div class="row">
                        	<div class="col-md-6">
                                <asp:Label runat="server" Text="Return to IFS Location:" />
                                <asp:TextBox ID="txtReturnToIFSLocation" runat="server" ClientIDMode="Static" />
                        
                                <asp:Label runat="server" Text="ESN:" />
                                <asp:TextBox ID="txtReturnESN" runat="server" ClientIDMode="Static" />
                       
                                <asp:Label runat="server" Text="Return ID Code:" />
                                <asp:TextBox ID="txtReturnIDCode" runat="server" ClientIDMode="Static" />
                        
                                <asp:Label runat="server" Text="Status:" />
                                <asp:DropDownList ID="drpListToShow" runat="server">
                                    <asp:ListItem Text="Tech Returning" Value="Tech Returning" Selected="True" />
                                    <asp:ListItem Text="Tech Assigned" Value="Tech Assigned" />
                                </asp:DropDownList>
                        
                                <asp:Button ID="btnReturnRefresh" runat="server" Text="Refresh" />
                                <asp:Button ID="btnProcessReturnCode" runat="server" Text="Process Part Return" />
                        
                                <asp:Label ID="lblReturningMessage" runat="server" />
                            </div>
                        </div>

                        <asp:GridView ID="grdReturnRecords" CssClass="table" runat="server" DataKeyNames="MasterPartsTechAssignedLogID"
                            AutoGenerateColumns="False">
                            <Columns>
                                <asp:TemplateField HeaderText="P" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="imgFill" runat="server" ToolTip="Process Part Return">
                                            <span class="oi oi-arrow-circle-right"></span>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="ESN" HeaderText="IMEI" ReadOnly="True" />
                                <asp:BoundField DataField="TechUser" HeaderText="Tech Name" ReadOnly="True" />
                                <asp:BoundField DataField="GMPPartNumber" HeaderText="IMM Partnumber" ReadOnly="True" />
                                <asp:BoundField DataField="PartNumber" HeaderText="Partnumber" ReadOnly="True" />
                                <asp:BoundField DataField="ReturningUser" HeaderText="Returning User" ReadOnly="True" Visible="false" />
                                <asp:BoundField DataField="ReturningDate" HeaderText="Returning Date" ReadOnly="True" />
                                <asp:BoundField DataField="AssignedUser" HeaderText="Assigned User" ReadOnly="True" Visible="false" />
                                <asp:BoundField DataField="AssignedDate" HeaderText="Assigned Date" ReadOnly="True" />
                                <asp:BoundField DataField="Status" HeaderText="Status" ReadOnly="True" />
                                <asp:BoundField DataField="MasterPartsTechAssignedLogID" HeaderText="Return ID" ReadOnly="True" />
                            </Columns>
                        </asp:GridView>

                    </ContentTemplate>
                </asp:TabPanel>

            </asp:TabContainer>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

<asp:Content ContentPlaceHolderID="js" runat="server">
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
