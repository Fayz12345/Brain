<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Maint_Client.aspx.cs" Inherits="BW_WebApp.Maintenance.Maint_Client" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Syncfusion.Shared.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89" Namespace="Syncfusion.Web.UI.WebControls.Shared" TagPrefix="syncfusion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server" />
<asp:Content ID="Content2" ContentPlaceHolderID="cMC" runat="server">

    <asp:Panel ID="pnlUpload" runat="server">
        <div class="row">
            <div class="col-sm">
                <div class="custom-file mb-2">
                    <asp:FileUpload ID="FileUploadXLS" class="custom-file-input" runat="server" />
                    <label class="custom-file-label">Choose file</label>
                </div>
            </div>
            
            <div class="col-sm text-sm-right">
                <asp:CheckBox ID="AutoGenerateClient" runat="server" Text="Auto Generate Client When uploading." ToolTip="One Client, One Sublocation When uploading." />
                <asp:Button ID="btnUpload" runat="server" Text="Upload" />
                <asp:Button ID="btnDownload" runat="server" Text="Download" />
            </div>
        </div>
        <asp:Label ID="lblMsgDetail" runat="server" Visible="False" />
    </asp:Panel>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="pnlMainView" runat="server">
                <asp:HiddenField ID="hdnSelectedClientID" runat="server" Value="xxxxxxx" />
                <asp:TabContainer ID="TabContainer1" CssClass="tab-container" runat="server">
                    <asp:TabPanel ID="TabClient" CssClass="tab-panel" runat="server" HeaderText="Client">
                        <ContentTemplate>
                            <asp:Panel ID="Panel3" runat="server">
                                <h1>
                                    <asp:Label ID="lblRecordTitle" runat="server" Text="Maintenance Client Status" /></h1>
                                <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" />
                                <asp:Button ID="btnEdit" runat="server" Text="Edit" Visible="False" OnClick="btnEdit_Click" />
                                <asp:Button ID="btnDelete" runat="server" Text="Delete" Visible="False" OnClick="btnDelete_Click" />
                                <asp:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" ConfirmText="Continue with Delete?"
                                    Enabled="True" TargetControlID="btnDelete" />
                                <asp:Label ID="lblSelect" runat="server" Text=""></asp:Label>
                            </asp:Panel>
                            <asp:Panel ID="pnlMainGrid" runat="server" Style="overflow: auto; max-height: 400px;">
                                <asp:GridView ID="MainGrid" CssClass="table" runat="server" AutoGenerateSelectButton="True"
                                    DataKeyNames="ClientID" AutoGenerateColumns="False">
                                    <SelectedRowStyle CssClass="srowstyle" />
                                    <Columns>
                                        <asp:BoundField DataField="ClientID" HeaderText="ID" ReadOnly="True" Visible="false" />
                                        <asp:BoundField DataField="CompanyName" HeaderText="Company Name" ReadOnly="True" />
                                        <asp:BoundField DataField="UserName" HeaderText="UserName" ReadOnly="True" />
                                        <asp:BoundField DataField="RMASuffix" HeaderText="RMA" ReadOnly="True" />
                                        <asp:BoundField DataField="ContactName" HeaderText="Contact Name" ReadOnly="True" />
                                        <asp:BoundField DataField="AddressLine1" HeaderText="Address Line1" ReadOnly="True" />
                                        <asp:BoundField DataField="AddressLine2" HeaderText="Address Line2" ReadOnly="True" />
                                        <asp:BoundField DataField="City" HeaderText="City" ReadOnly="True" />
                                        <asp:BoundField DataField="StateOrProvince" HeaderText="State Or Province" ReadOnly="True" />
                                        <asp:BoundField DataField="PostalCode" HeaderText="Postal Code" ReadOnly="True" />
                                        <asp:BoundField DataField="PhoneNumber" HeaderText="Phone" ReadOnly="True" />
                                        <asp:BoundField DataField="FaxNumber" HeaderText="Fax" ReadOnly="True" />
                                        <asp:BoundField DataField="isVendorGroup" HeaderText="Vendor Group" ReadOnly="True" />
                                        <asp:BoundField DataField="InWarrentyPricing" HeaderText="In Warranty Pricing" ReadOnly="True" />
                                        <asp:BoundField DataField="HideOnPreReceiveList" HeaderText="Hide from PreReceive"
                                            ReadOnly="True" />
                                        <asp:BoundField DataField="ProductTag" HeaderText="Product Tag" ReadOnly="True" />
                                        <asp:BoundField DataField="RepairForm" HeaderText="Repair From" ReadOnly="True" />
                                        <asp:TemplateField HeaderText="P">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="imgPrint" CssClass="btn btn-default" runat="server" ToolTip="Print entry">
                                        <span class="oi oi-print"></span>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="TabClientLocation" CssClass="tab-panel" runat="server" HeaderText="Locations">
                        <ContentTemplate>
                            <asp:TabContainer runat="server" ID="tabChild" CssClass="tab-container" ActiveTabIndex="0">
                                <asp:TabPanel runat="server" ID="tabLocations" CssClass="tab-panel" Enabled="true"
                                    HeaderText="Client Location">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlChild" runat="server" ScrollBars="Auto" Visible="false">
                                            <asp:Panel ID="Panel2" runat="server">
                                                <h2>
                                                    <asp:Label ID="Label1" runat="server" Text="Client Locations" /></h2>
                                                <asp:Button ID="btnAddLocation" runat="server" Text="Add" OnClick="btnAddLocation_Click" />
                                                <asp:Button ID="btnEditLocation" runat="server" Text="Edit" Visible="False" OnClick="btnEditLocation_Click" />
                                                <asp:Button ID="btnDeleteLocation" runat="server" Text="Delete" Visible="False" OnClick="btnDeleteLocation_Click" />
                                                <asp:Button ID="btnMoveLocation" runat="server" Text="Move Client to Other Location"
                                                    OnClientClick="OpenMoveClient(); return false;" Visible="False" UseSubmitBehavior="False" />
                                                <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" ConfirmText="Continue with Delete?"
                                                    Enabled="True" TargetControlID="btnDeleteLocation" />
                                                <asp:Label ID="lblSelectLocation" runat="server" Text=""></asp:Label>
                                            </asp:Panel>
                                            <asp:Panel ID="Panel1" runat="server" Style="overflow: auto; max-height: 350px;">
                                                <asp:GridView ID="ChildGrid" CssClass="table" runat="server" AutoGenerateSelectButton="True"
                                                    DataKeyNames="ClientLocationID" AutoGenerateColumns="False">
                                                    <Columns>
                                                        <asp:BoundField DataField="ClientLocationID" HeaderText="ID" ReadOnly="True" Visible="false" />
                                                        <asp:BoundField DataField="ScanKey" HeaderText="ScanKey" />
                                                        <asp:BoundField DataField="CompanyName" HeaderText="Company" ReadOnly="True" />
                                                        <asp:BoundField DataField="UserName" HeaderText="UserName" ReadOnly="True" />
                                                        <asp:BoundField DataField="ContactName" HeaderText="Contact" ReadOnly="True" />
                                                        <asp:BoundField DataField="AddressLine1" HeaderText="Address" ReadOnly="True" />
                                                        <asp:BoundField DataField="AddressLine2" HeaderText="" ReadOnly="True" />
                                                        <asp:BoundField DataField="City" HeaderText="City" ReadOnly="True" />
                                                        <asp:BoundField DataField="StateOrProvince" HeaderText="Prov" ReadOnly="True" />
                                                        <asp:BoundField DataField="PostalCode" HeaderText="P Code" ReadOnly="True" />
                                                        <asp:BoundField DataField="PhoneNumber" HeaderText="Phone" ReadOnly="True" />
                                                        <asp:BoundField DataField="FaxNumber" HeaderText="Fax" ReadOnly="True" />
                                                        <asp:BoundField DataField="BillingAddress" HeaderText="email" ReadOnly="True" />
                                                        <asp:BoundField DataField="OnSiteInventory" HeaderText="On Site Inv" ReadOnly="True" />
                                                        <%--<asp:BoundField DataField="IFSSite" HeaderText="IFS Site" ReadOnly="True" />
                                            <asp:BoundField DataField="IFSProject" HeaderText="IFS Project" ReadOnly="True" />--%>
                                                    </Columns>
                                                </asp:GridView>
                                            </asp:Panel>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:TabPanel>
                                <asp:TabPanel runat="server" ID="tabQRestrictionsxx" CssClass="tab-panel" Enabled="true"
                                    HeaderText="Restrictions" Visible="False">
                                    <ContentTemplate>
                                        <asp:Button ID="btnQRestrict" runat="server" Text="Open Restrictions" OnClientClick="OpenRestrictions(); return false;" />
                                    </ContentTemplate>
                                </asp:TabPanel>
                                <asp:TabPanel runat="server" ID="tabAllowedProjectsxx" CssClass="tab-panel" Enabled="true"
                                    HeaderText="Allowed Projects" Visible="True">
                                    <ContentTemplate>
                                        <asp:Button ID="btnAllowedProject" runat="server" Text="Open Allowed Projects" OnClientClick="OpenWindow('Maint_ClientAllowedProject.aspx'); return false;" />
                                    </ContentTemplate>
                                </asp:TabPanel>
                                <asp:TabPanel runat="server" ID="tabAllowedProcessesxx" CssClass="tab-panel" Enabled="true"
                                    HeaderText="Allowed Processes" Visible="True">
                                    <ContentTemplate>
                                        <asp:Button ID="btnAllowedProcess" runat="server" Text="Open Allowed Processes" OnClientClick="OpenWindow('Maint_ClientAlowedProcess.aspx'); return false;" />
                                    </ContentTemplate>
                                </asp:TabPanel>
                                <asp:TabPanel runat="server" ID="tabBillingPointsxx" CssClass="tab-panel" Enabled="true"
                                    HeaderText="Billing Points" Visible="False">
                                    <ContentTemplate>
                                        <asp:Button ID="btnBillingPoints" runat="server" Text="Open Billing Points" OnClientClick="OpenWindow('Maint_ClientBillingPoints.aspx'); return false;" />
                                    </ContentTemplate>
                                </asp:TabPanel>
                                <%--<asp:TabPanel runat="server" ID="tabQRestrictions" CssClass="tab-panel" Enabled="true" HeaderText="Q Restrictions">
                        <ContentTemplate>
                            Project:
                            <asp:DropDownList ID="drpProject" runat="server" ToolTip="Project" AutoPostBack="True">
                            </asp:DropDownList>
                            <asp:Button ID="btnUpdateQRestriction" runat="server" Text="Update Restrictions" />

                            <asp:GridView ID="grdQuestions" CssClass="table" runat="server" DataKeyNames="QuestionID" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:BoundField DataField="QuestionID" HeaderText="ID" ReadOnly="True" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkThisQuestion" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Description" HeaderText="ScanKey">
                                        <ItemStyle Wrap="False" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:TabPanel>
                   <asp:TabPanel runat="server" ID="tabARestrictions" Enabled="true" HeaderText="A Restrictions">
                        <ContentTemplate>
                            Question:
                            <asp:DropDownList ID="drpQuestion" runat="server" ToolTip="Question" AutoPostBack="True" />
                            <asp:Button ID="btnUpdateARestriction" runat="server" Text="Update Restrictions" />

                            <asp:GridView ID="grdAnswers" CssClass="table" runat="server" DataKeyNames="QuestionID" AutoGenerateColumns="False" ShowHeader="False">
                                <Columns>
                                    <asp:BoundField DataField="Name" HeaderText="Name" ReadOnly="True" />
                                    <asp:BoundField DataField="Description" HeaderText="Question" ReadOnly="True" />
                                    <asp:TemplateField ItemStyle-Wrap="False" ShowHeader="False" HeaderText="C<br />O<br />L<br />C<br />O<br />L<br />">
                                        <ItemTemplate>
                                            <asp:CheckBoxList ID="checkAnswer" CssClass="checklist-inline" RepeatLayout="UnorderedList" runat="server" RepeatDirection="Horizontal" />
                                            <asp:HiddenField ID="HiddenName" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:TabPanel>--%>
                                <%--<asp:TabPanel runat="server" ID="tabAllowedProjects" CssClass="tab-panel" Enabled="true" HeaderText="Allowed Projects">
                        <ContentTemplate>
                            <asp:Button ID="btnSaveAllowedProject" runat="server" Text="Update" />
                            <asp:GridView ID="grdProjectList" CssClass="table" runat="server" DataKeyNames="ProjectID" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:BoundField DataField="ProjectID" HeaderText="ID" ReadOnly="True" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkThisProject" runat="server" />
                                            <asp:HiddenField ID="hdnClientProjectDependenciesID" runat="server" />
                                            <asp:HiddenField ID="hdnProjectID" runat="server" />
                                            <asp:HiddenField ID="hdnClientID" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Name" HeaderText="Name">
                                        <ItemStyle Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Description" HeaderText="Description">
                                        <ItemStyle Wrap="False" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel runat="server" ID="tabAllowedProcesses" CssClass="tab-panel" Enabled="true" HeaderText="Allowed Processes">
                        <ContentTemplate>
                            <asp:Button ID="btnSaveAllowedProcesses" runat="server" Text="Update" />
                            <asp:GridView ID="grdProcessList" CssClass="table" runat="server" DataKeyNames="ProcessID" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:BoundField DataField="ProcessID" HeaderText="ID" ReadOnly="True" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkThisProcess" runat="server" />
                                            <asp:HiddenField ID="hdnClientProcessDependenciesID" runat="server" />
                                            <asp:HiddenField ID="hdnProcessID" runat="server" />
                                            <asp:HiddenField ID="hdnClientID" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ScanKey" HeaderText="ScanKey">
                                        <ItemStyle Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Name" HeaderText="Name">
                                        <ItemStyle Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Description" HeaderText="Description">
                                        <ItemStyle Wrap="False" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel runat="server" ID="tabBillingPoints" CssClass="tab-panel" Enabled="true" HeaderText="Billing Points">
                        <ContentTemplate>
                            <asp:Button ID="btnSaveBillingPoints" runat="server" Text="Update Billing Points" />
                            <asp:Button ID="btnLoadBillingPointsDefault" runat="server" Text="Load Default Billing Points" />
                            <asp:Label ID="lblDefault" runat="server" Text=""></asp:Label>

                            <asp:GridView ID="grdBillingPoints" CssClass="table" runat="server" DataKeyNames="ClientBillingPointID" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:TemplateField HeaderText='Billing Point'>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkisBillingPoint" runat="server" ToolTip="Is Billing Point" />
                                            <asp:TextBox ID="txtRateValue" runat="server" ToolTip="Rate/Value"></asp:TextBox>
                                            <asp:HiddenField ID="hdnClientBillingPointID" runat="server" />
                                            <asp:HiddenField ID="hdnProjectID" runat="server" />
                                            <asp:HiddenField ID="hdnProcessID" runat="server" />
                                            <asp:HiddenField ID="hdnClientID" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ProjectName" HeaderText="Project Name">
                                        <ItemStyle Wrap="True" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ProjectDescription" HeaderText="Project Description">
                                        <ItemStyle Wrap="True" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ProcessName" HeaderText="Process Name">
                                        <ItemStyle Wrap="True" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ProcessDescription" HeaderText="Process Description">
                                        <ItemStyle Wrap="True" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Description_Client" HeaderText="Description Client">
                                        <ItemStyle Wrap="True" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:TabPanel>--%>
                            </asp:TabContainer>
                        </ContentTemplate>
                    </asp:TabPanel>
                </asp:TabContainer>
            </asp:Panel>

            <asp:Panel ID="pnlAdd" runat="server">

                <h1>Add Client</h1>

                <div class="row">
                  <div class="col-md">
                        <label>RMA Suffix:</label>
                        <asp:TextBox ID="AddRMASuffix" runat="server" MaxLength="3" />

                        <label>Company Name:</label>
                        <asp:TextBox ID="AddCompanyName" runat="server" MaxLength="200" />

                        <label>Contact Name:</label>
                        <asp:TextBox ID="AddContactName" runat="server"  MaxLength="50"/>

                        <label>Billing Address:</label>
                        <asp:TextBox ID="AddBillingAddress" runat="server"  MaxLength="255"/>

                        <label>Address Line 1:</label>
                        <asp:TextBox ID="AddAddressLine1" runat="server"  MaxLength="50"/>

                        <label>Address Line 2:</label>
                        <asp:TextBox ID="AddAddressLine2" runat="server"  MaxLength="50"/>

                        <label>Address Line 3:</label>
                        <asp:TextBox ID="AddAddressLine3" runat="server"  MaxLength="50"/>

                        <label>Address Line 4:</label>
                        <asp:TextBox ID="AddAddressLine4" runat="server"  MaxLength="50"/>

                        <label>City:</label>
                        <asp:TextBox ID="AddCity" runat="server"  MaxLength="50"/>

                        <label>State / Province:</label>
                        <asp:TextBox ID="AddStateOrProvince" runat="server"  MaxLength="50"/>

                        <label>Zip / Postal Code:</label>
                        <asp:TextBox ID="AddPostalCode" runat="server"  MaxLength="20"/>

                        <label>Country:</label>
                        <asp:TextBox ID="AddCountry" runat="server" MaxLength="50" />

                        <label>Phone:</label>
                        <asp:TextBox ID="AddPhoneNumber" runat="server"  MaxLength="30" />
                    </div>
                  <div class="col-md">
                        <label>Fax:</label>
                        <asp:TextBox ID="AddFaxNumber" runat="server" />

                        <label>Login Name:</label>
                        <asp:TextBox ID="AddUserName" runat="server" MaxLength="50" />

                        <label>Email:</label>
                        <asp:TextBox ID="AddEmailAddress" runat="server" />

                        <label>Notes:</label>
                        <asp:TextBox ID="AddNotes" runat="server" />

                        <div class="d-block">
                            <asp:CheckBox ID="AddIsVendorGroup" Text="Vendor Group" runat="server" />
                            <asp:CheckBox ID="AddInWarrentyPricing" Text="In Warranty Pricing" runat="server" />
                            <asp:CheckBox ID="AddHideClientList" Text="Hide From PreReceive Client List" runat="server" />
                        </div>

                        <label>Product Tag:</label>
                        <asp:TextBox ID="AddProductTag" runat="server" MaxLength="20" />

                        <label>Warranty Day Limit:</label>
                        <asp:TextBox ID="AddWarrentyDayLimit" runat="server" MaxLength="3" />

                        <label>Repair Form:</label>
                        <asp:TextBox ID="AddRepairForm" runat="server" MaxLength="20" />

                        <label>Portal Name:</label>
                        <asp:DropDownList ID="drpAddPortalName" runat="server" ToolTip="Dealer Portal" />

                        <label>Status:</label>
                        <asp:DropDownList ID="drpAddStatus" runat="server" ToolTip="Question Status" />
                    </div>
                </div>

                <asp:Button ID="AddOK" runat="server" Text="OK" OnClick="AddOK_Click" />
                <asp:Button ID="AddCancel" runat="server" Text="Cancel" OnClick="AddCancel_Click1" />

            </asp:Panel>

            <asp:Panel ID="pnlEdit" runat="server">
                <h1>Edit Client</h1>

                <div class="row">
                  <div class="col-md">
                        <label>RMA Suffix:</label>
                        <asp:TextBox ID="EditRMASuffix" runat="server" MaxLength="3" />

                        <label>Company Name:</label>
                        <asp:TextBox ID="EditCompanyName" runat="server" MaxLength="200" />

                        <label>Contact Name:</label>
                        <asp:TextBox ID="EditContactName" runat="server" MaxLength="50" />

                        <label>Billing Address:</label>
                        <asp:TextBox ID="EditBillingAddress" runat="server" MaxLength="255" />

                        <label>Address Line 1:</label>
                        <asp:TextBox ID="EditAddressLine1" runat="server" MaxLength="50" />

                        <label>Address Line 2:</label>
                        <asp:TextBox ID="EditAddressLine2" runat="server" MaxLength="50" />

                        <label>Address Line 3:</label>
                        <asp:TextBox ID="EditAddressLine3" runat="server" MaxLength="50" />

                        <label>Address Line 4:</label>
                        <asp:TextBox ID="EditAddressLine4" runat="server" MaxLength="50" />

                        <label>City:</label>
                        <asp:TextBox ID="EditCity" runat="server" MaxLength="50" />

                        <label>State / Province:</label>
                        <asp:TextBox ID="EditStateOrProvince" runat="server" MaxLength="50" />

                        <label>Zip / Postal Code:</label>
                        <asp:TextBox ID="EditPostalCode" runat="server" MaxLength="20" />

                        <label>Country:</label>
                        <asp:TextBox ID="EditCountry" runat="server" MaxLength="20" />

                        <label>Phone:</label>
                        <asp:TextBox ID="EditPhoneNumber" runat="server" MaxLength="30" />
                    </div>
                    <div class="col-md">
                        <label>Fax:</label>
                        <asp:TextBox ID="EditFaxNumber" runat="server" />

                        <label>Login Name:</label>
                        <asp:TextBox ID="EditUserName" runat="server" MaxLength="50" />

                        <label>Email:</label>
                        <asp:TextBox ID="EditEmailAddress" runat="server" />

                        <label>Notes:</label>
                        <asp:TextBox ID="EditNotes" runat="server" TextMode="MultiLine" />

                        <div class="d-block">
                            <asp:CheckBox ID="EditIsVendorGroup" Text="Vendor Group" runat="server" />
                            <asp:CheckBox ID="EditInWarrentyPricing" Text="In Warranty Pricing" runat="server" />
                            <asp:CheckBox ID="EditHideClientList" Text="Hide From PreReceive Client List" runat="server" />
                        </div>

                        <label>Product Tag:</label>
                        <asp:TextBox ID="EditProductTag" runat="server" MaxLength="20" />

                        <label>Warranty Day Limit:</label>
                        <asp:TextBox ID="EditWarrentyDayLimit" runat="server" MaxLength="3" />

                        <label>Repair Form:</label>
                        <asp:TextBox ID="EditRepairForm" runat="server" MaxLength="20" />

                        <label>Portal Name:</label>
                        <asp:DropDownList ID="drpEditPortalName" runat="server" ToolTip="Dealer Portal" />

                        <label>Status:</label>
                        <asp:DropDownList ID="drpEditStatus" runat="server" ToolTip="Question Status" />

                    </div>
                </div>

                <asp:Button ID="EditOK" runat="server" Text="OK" OnClick="EditOK_Click" />
                <asp:Button ID="EditCancel" runat="server" Text="Cancel" OnClick="EditCancel_Click" />

            </asp:Panel>

            <asp:Panel ID="pnlAddLocation" runat="server">
                <h1>Add Store Location</h1>

                <div class="row">
                  <div class="col-md">
                        <asp:Label ID="AddLScanKeyLBL" runat="server" />

                        <label>ScanKey:</label>
                        <asp:TextBox ID="AddLSkanKey" runat="server" />

                        <label>Company Name:</label>
                        <asp:TextBox ID="AddLCompanyName" runat="server" />

                        <label>Store Number:</label>
                        <asp:TextBox ID="AddLStoreNumber" runat="server" />

                        <label>Store Suffix:</label>
                        <asp:TextBox ID="AddLStoreSuffix" runat="server" />

                        <label>Contact Name:</label>
                        <asp:TextBox ID="AddLContactName" runat="server" />

                        <label>Billing Address:</label>
                        <asp:TextBox ID="AddLBillingAddress" runat="server" />

                        <label>Address Line 1:</label>
                        <asp:TextBox ID="AddLAddressLine1" runat="server" />

                        <label>Address Line 2:</label>
                        <asp:TextBox ID="AddLAddressLine2" runat="server" />

                        <label>Address Line 3:</label>
                        <asp:TextBox ID="AddLAddressLine3" runat="server" />

                        <label>Address Line 4:</label>
                        <asp:TextBox ID="AddLAddressLine4" runat="server" />

                        <label>City:</label>
                        <asp:TextBox ID="AddLCity" runat="server" />

                        <label>State / Province:</label>
                        <asp:TextBox ID="AddLStateOrProvince" runat="server" />

                        <label>State / Country:</label>
                        <asp:TextBox ID="AddLCountry" runat="server" />
                    </div>
                    <div class="col-md">
                        <label>Zip / Postal Code:</label>
                        <asp:TextBox ID="AddLPostalCode" runat="server" />

                        <label>Status:</label>
                        <asp:DropDownList ID="drpAddLStatus" runat="server" ToolTip="Question Status" />

                        <label>Phone:</label>
                        <asp:TextBox ID="AddLPhoneNumber" runat="server" />

                        <label>Fax:</label>
                        <asp:TextBox ID="AddlFaxNumber" runat="server" />

                        <label>Login Name:</label>
                        <asp:TextBox ID="AddLUsername" runat="server" />

                        <label>Email:</label>
                        <asp:TextBox ID="AddLEmailAddress" runat="server" />

                        <label>Second Email:</label>
                        <asp:TextBox ID="AddLEmailAddress2" runat="server" />

                        <label>Authorization Approval PIN:</label>
                        <asp:TextBox ID="AddLApprovalPassword" runat="server" ToolTip="Numeric, enter new number to set client Pin Number (4 - 8 digits)" MaxLength="8" />
                        <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="AddLApprovalPassword" Mask="9{8}" MaskType="Number" InputDirection="RightToLeft" />

                        <asp:CheckBox ID="chkAddInventory" CssClass="d-block" Text="Onsite Inventory" runat="server" />

                        <%--<label>IFS Site:</label>
                        <asp:TextBox ID="AddIFSSite" runat="server" MaxLength="5" />

                        <label>IFS Project:</label>
                        <asp:TextBox ID="AddIFSProject" runat="server" MaxLength="10" />

                        <label>IFS Supplier:</label>
                        <asp:TextBox ID="AddIFSVendor" runat="server" MaxLength="50" />

                        <asp:CheckBox ID="AddLocationRequirePOToReceive" CssClass="d-block" Text="Does this location need a PO to receive devices?" runat="server" />

                        <label>IFS Location Segment - Remote Locations:</label>
                        <asp:TextBox ID="AddLocationSegment" runat="server" MaxLength="3" />--%>

                        <label>Notes:</label>
                        <asp:TextBox ID="AddLNotes" runat="server" />

                    </div>
                </div>
                <asp:Button ID="AddLocationOK" runat="server" Text="OK" OnClick="AddLocationOK_Click" />
                <asp:Button ID="AddLocationCancel" runat="server" Text="Cancel" OnClick="AddLocationCancel_Click" />
                <asp:Label ID="lblAddLocationMessage" runat="server" />
            </asp:Panel>

            <asp:Panel ID="pnlEditLocation" runat="server">
                <asp:HiddenField ID="hdnClientLocationID" runat="server" />

                <h1>Edit Store Location</h1>
                <asp:Label ID="EditLScanKeyLBL" runat="server" />

                <div class="row">
                  <div class="col-md">
                        <label>ScanKey:</label>
                        <asp:TextBox ID="EditLSkanKey" runat="server" />

                        <label>Company Name:</label>
                        <asp:TextBox ID="EditLCompanyName" runat="server" />

                        <label>Store Number:</label>
                        <asp:TextBox ID="EditLStoreNumber" runat="server" />

                        <label>Store Suffix:</label>
                        <asp:TextBox ID="EditLStoreSuffix" runat="server" />

                        <label>Contact Name:</label>
                        <asp:TextBox ID="EditLContactName" runat="server" />

                        <label>Billing Address:</label>
                        <asp:TextBox ID="EditLBillingAddress" runat="server" />

                        <label>Address Line 1:</label>
                        <asp:TextBox ID="EditLAddressLine1" runat="server" />

                        <label>Address Line 2:</label>
                        <asp:TextBox ID="EditLAddressLine2" runat="server" />

                        <label>Address Line 3:</label>
                        <asp:TextBox ID="EditLAddressLine3" runat="server" />

                        <label>Address Line 4:</label>
                        <asp:TextBox ID="EditLAddressLine4" runat="server" />

                        <label>City:</label>
                        <asp:TextBox ID="EditLCity" runat="server" />

                        <label>State / Province:</label>
                        <asp:TextBox ID="EditLStateOrProvince" runat="server" />
                    </div>
                    <div class="col-md">
                        <label>Zip / Postal Code:</label>
                        <asp:TextBox ID="EditLPostalCode" runat="server" />

                        <label>Country:</label>
                        <asp:TextBox ID="EditLCountry" runat="server" />

                        <label>Status:</label>
                        <asp:DropDownList ID="drpEditLStatus" runat="server" ToolTip="Question Status" />

                        <label>Phone:</label>
                        <asp:TextBox ID="EditLPhoneNumber" runat="server" />

                        <label>Fax:</label>
                        <asp:TextBox ID="EditLFaxNumber" runat="server" />

                        <label>Login Name:</label>
                        <asp:TextBox ID="EditLUserName" runat="server" />

                        <label>Email:</label>
                        <asp:TextBox ID="EditLEmailAddress" runat="server" />

                        <label>Second Email:</label>
                        <asp:TextBox ID="EditLEmailAddress2" runat="server" />

                        <label>Authorization Approval PIN:</label>
                        <asp:TextBox ID="EditLApprovalPassword" runat="server" ToolTip="Numeric, enter new number to reset client Pin Number (4 - 8 digits)" MaxLength="8" />
                        <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="EditLApprovalPassword" Mask="9{8}" MaskType="Number" InputDirection="RightToLeft" />

                        <asp:CheckBox ID="chkEditInventory" CssClass="d-block" Text="Onsite Inventory" runat="server" />

                        <%--<label>IFS Site:</label>
                        <asp:TextBox ID="EditIFSSite" runat="server" MaxLength="5" />

                        <label class="d-block">** See note below **</label>

                        <label>IFS Project:</label>
                        <asp:TextBox ID="EditIFSProject" runat="server" MaxLength="10" />

                        <div class="alert alert-info">
                            <strong>Note:</strong><br>
                            If this Client Location has devices attached to it, this change must be manually syncronized with IFS.<br>
                            If the IFS Project is changed on the client location, this would strand all inventory in IFS that matched the original Project.
                        </div>

                        <label>IFS Supplier:</label>
                        <asp:TextBox ID="EditIFSVendor" runat="server" MaxLength="50" />

                        <asp:CheckBox ID="EditLocationRequirePOToReceive" Text="Does this Location need a PO to receive Devices?" runat="server" />

                        <label>IFS Location Segment - Remote Locations:</label>
                        <asp:TextBox ID="EditLocationSegment" runat="server" MaxLength="3" />--%>

                        <label>Notes:</label>
                        <asp:TextBox ID="EditLNotes" runat="server" />
                    </div>
                </div>

                <asp:Button ID="EditLocationOK" runat="server" Text="OK" OnClick="EditLocationOK_Click" />
                <asp:Button ID="EditLocationCancel" runat="server" Text="Cancel" OnClick="EditLocationCancel_Click" />
                <asp:Label ID="lblEditLocationMessage" CssClass="d-block" runat="server" />
            </asp:Panel>


            <div id="wndMoveLocation" class="modal" tabindex="-1" role="dialog">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Move client to new Location</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <label><asp:Label ID="Label2" runat="server" Text="Client to Move Location to:" /></label>
                            <asp:HiddenField ID="hdnMoveClientID" runat="server" />
                            <asp:DropDownList ID="drpClientList" runat="server" />
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnMoveClientLocation" runat="server" Text="Move" OnClientClick="OpenMoveClient_OK();" />
                            <%--<input type="button" value="Move" onclick="OpenMoveClient_OK();return false;" />--%>
                            <input type="button" value="Cancel" onclick="OpenMoveClient_Cancel();return false;" />
                        </div>
                    </div>
                </div>
            </div>
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


        function IsScanKeyOK(From) {
            var service = new WebServer_01();
            var scankey = '';
            if (From == 'Edit') {
                var scankey = $get("<%= EditLSkanKey.ClientID %>").value
                var CLID = $get("<%= hdnClientLocationID.ClientID %>").value
                service.IsScanKeyOK(CLID, scankey, onSuccessEditScreen, null, null);
            }
            if (From == "Add") {
                var scankey = $get("<%= AddLSkanKey.ClientID %>").value
                service.IsScanKeyOK("-1", scankey, onSuccessAddScreen, null, null);
            }
        }

        function onSuccessAddScreen(result) {
            var B = result.split(':');
            $get("<%= AddLScanKeyLBL.ClientID %>").innerHTML = result;
            if (result.length > 0) {
                $get("<%= AddLSkanKey.ClientID %>").value = "";
                $get("<%= AddLSkanKey.ClientID %>").focus();
            }
            //alert(result);
            return;
        }

        function onSuccessEditScreen(result) {
            var B = result.split(':');
            $get("<%= EditLScanKeyLBL.ClientID %>").innerHTML = result;
            if (result.length > 0) {
                $get("<%= EditLSkanKey.ClientID %>").value = "";
                $get("<%= EditLSkanKey.ClientID %>").focus();
            }
            //alert(result);
            return;

        }


        function OpenMoveClient() {
            // $find('< %=this.wndMoveLocation.ClientID%>').Title = "Move client to new Location";
            // $find('< %=this.wndMoveLocation.ClientID%>').Open(null, null);
            $('#wndMoveLocation').modal('show');
            return false;
        }


        function OpenMoveClient_Cancel() {
            // var ID = $get("< %= wndMoveLocation.ClientID %>").value;
            // $find('< %=this.wndMoveLocation.ClientID%>').Close();
            $('#wndMoveLocation').modal('hide');
            alert("Canceled");
        }

        function OpenMoveClient_OK() {
            // var ID = $get("< %= wndMoveLocation.ClientID %>").value;
            // $find('< %=this.wndMoveLocation.ClientID%>').Close();
            $('#wndMoveLocation').modal('hide');
            var IndexValue = $get("<%= drpClientList.ClientID %>").selectedIndex;
            var ClientID = $get("<%= drpClientList.ClientID %>").options[IndexValue].value;
            $get("<%= hdnMoveClientID.ClientID %>").value = ClientID;
        }

        function PrintScanCodes(Table, ID) {
            //            var win = window.open("ViewDoc.aspx", "_blank", "status=no,toolbar=no,menubar=no,location=no,titlebar=no,width=600px,height=540px", true);
            //           var xDataList = {};
            //           xDataList["Table"] = "Client";
            //           xDataList["ID"] = ID;
            //           var pstring = GetParameterStream(xDataList);
            //           var WindowToOpen = "ViewDoc.aspx";
            //           if (pstring.length > 0) {
            //               WindowToOpen = WindowToOpen + "?" + pstring
            //           }
            //           //            var win = window.open(WindowToOpen, "_blank", "width=100,height=50,menubar", true);
            //           var win = window.open(WindowToOpen, "_blank", "", true);
            //           //var win = window.open(WindowToOpen, "_blank", "menubar", true);
            //           // win.focus();
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

        function OpenRestrictions() {


            var ClientID = $get("<%= hdnSelectedClientID.ClientID %>").value;

            if (ClientID.length == 0) { return; }
            var pstring = "ID=" + ClientID;        // + "&PID=" + PID + "&PName=" + ProcessName;

            // var WindowToOpen = "RPT_SpotCountReport.aspx";
            var WindowToOpen = "/Maintenance/Maint_ClientRestrictions.aspx";

            if (pstring.length > 0) {
                WindowToOpen = WindowToOpen + "?" + pstring
            }
            var win = window.open(WindowToOpen, "_blank", "", true);
            //var win = window.open(WindowToOpen, "_blank", "menubar", true);
            return;
        }

        function OpenWindow(xWindow) {


            var ClientID = $get("<%= hdnSelectedClientID.ClientID %>").value;

            if (ClientID.length == 0) { return; }
            var pstring = "ID=" + ClientID;        // + "&PID=" + PID + "&PName=" + ProcessName;

            // var WindowToOpen = "RPT_SpotCountReport.aspx";
            //var WindowToOpen = "Maint_ClientRestrictions.aspx";
            var WindowToOpen = xWindow;

            if (pstring.length > 0) {
                WindowToOpen = WindowToOpen + "?" + pstring
            }
            var win = window.open(WindowToOpen, "_blank", "", true);
            //var win = window.open(WindowToOpen, "_blank", "menubar", true);
            return;
        }

</script>
</asp:Content>
