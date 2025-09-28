<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Maint_MasterPartTable.aspx.cs" Inherits="BW_WebApp.Maintenance.Maint_MasterPartTable" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Syncfusion.Shared.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89" Namespace="Syncfusion.Web.UI.WebControls.Shared" TagPrefix="syncfusion" %>
<%@ Register Assembly="Syncfusion.Tools.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89"  Namespace="Syncfusion.Web.UI.WebControls.Tools" TagPrefix="syncfusion" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="cMC" runat="server">
    <asp:Panel ID="pnlUpload" runat="server" Visible="True">
        <div class="row">
            <div class="col col-12 col-md-6">
                <div class="input-group w-auto">
                    <div class="custom-file">
                        <asp:FileUpload ID="FileUploadXLS" CssClass="custom-file-input" runat="server" />
                        <label class="custom-file-label">Choose file</label>
                    </div>
                </div>
            </div>
            <div class="col col-12 col-md-6 text-md-right">
                <asp:Button ID="btnUpload" runat="server" Text="Upload" />
                <asp:Button ID="btnDownload" runat="server" Text="Download" />
                <div class="form-check-inline">
                    <asp:CheckBox ID="chkRestrict" runat="server" Text="Restrict"
                        ToolTip="Check if you wish the report to use the Part Number Parameters." />
                </div>
            </div>
        </div>

        <asp:RadioButtonList ID="rdUploadType" CssClass="radiolist-inline" runat="server" RepeatDirection="Horizontal">
            <asp:ListItem Text="Transaction" Enabled="False"></asp:ListItem>
            <asp:ListItem Text="Part Numbers" Selected="True"></asp:ListItem>
        </asp:RadioButtonList>

        <asp:Label ID="lblMsgDetail" runat="server" Visible="False" />
    </asp:Panel>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            

            <asp:HiddenField ID="hdnUserName" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnCarrierID" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnClientLocationID" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnManufacturerID" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnModelID" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnMasterPartLinkTableID" runat="server" ClientIDMode="Static" />
            <%--<asp:HiddenField ID="hdnColourID" runat="server" ClientIDMode="Static" />--%>

            <asp:TabContainer ID="TabContainer1" CssClass="tab-container" runat="server">

                <asp:TabPanel ID="TabPanel1" CssClass="tab-panel" runat="server" HeaderText="Master Parts">
                    <ContentTemplate>
                        <asp:Panel ID="pnlMainView" runat="server">

                            <h3><asp:Label ID="lblRecordTitle" runat="server" Text="Maintenance Part Table" /></h3>

                            <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" />
                            <asp:Button ID="btnEdit" runat="server" Text="Edit" Visible="False" OnClick="btnEdit_Click" />
                            <asp:Button ID="btnDelete" runat="server" Text="Delete" Visible="False" OnClick="btnDelete_Click" />
                            <asp:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" ConfirmText="Continue with Delete?"
                                Enabled="True" TargetControlID="btnDelete" />



                            <asp:TextBox ID="txtSelectMessage" runat="server"></asp:TextBox>

                            <asp:Panel ID="pnlMainGrid" runat="server" ScrollBars="Auto">
                                <asp:GridView ID="MainGrid" CssClass="table table-striped" runat="server" AutoGenerateSelectButton="True"
                                    DataKeyNames="MasterPartsID" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:BoundField DataField="MasterPartsID" HeaderText="ID" ReadOnly="True" Visible="false" />
                                        <asp:BoundField DataField="Name" HeaderText="Category" />
                                        <asp:BoundField DataField="Description" HeaderText="Description" />
                                        <asp:BoundField DataField="ClassName" HeaderText="Class" />
                                        <asp:BoundField DataField="ClassDesc" HeaderText="Class Description" />
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>

                        </asp:Panel>

                        <asp:Panel ID="pnlAdd" CssClass="w-md-50" runat="server">
                            <h3>Add Part</h3>

                            <label>Category:</label>
                            <asp:TextBox ID="AddName" runat="server" MaxLength="50" />

                            <label>Description:</label>
                            <asp:TextBox ID="AddDesc" runat="server" MaxLength="50" />
                            
                            <label>Class:</label>
                            <asp:DropDownList ID="drpAddClass" runat="server" ToolTip="Class" AutoPostBack="false" />

                            <asp:Button ID="AddOK" runat="server" Text="OK" OnClick="AddOK_Click" />
                            <asp:Button ID="AddCancel" runat="server" Text="Cancel" OnClick="AddCancel_Click1" />
                        </asp:Panel>

                        <asp:Panel ID="pnlEdit" CssClass="w-md-50" runat="server">
                            <h3>Edit Part</h3>
                            
                            <label>Category:</label>
                            <asp:TextBox ID="EditName" runat="server" />
                            
                            <label>Description:</label>
                            <asp:TextBox ID="EditDesc" runat="server" MaxLength="50" />
                            <asp:TextBox ID="EditKeyID" runat="server" ReadOnly="True" Visible="False" />
                            
                            <label>Class:</label>
                            <asp:DropDownList ID="drpEditClass" runat="server" ToolTip="Class" AutoPostBack="false" />
                            
                            <asp:Button ID="EditOK" runat="server" Text="OK" OnClick="EditOK_Click" />
                            <asp:Button ID="EditCancel" runat="server" Text="Cancel" OnClick="EditCancel_Click" />
                        </asp:Panel>

                    </ContentTemplate>
                </asp:TabPanel>

                <asp:TabPanel ID="TabPanel2" CssClass="tab-panel" runat="server" HeaderText="Part Numbers">
                    <ContentTemplate>

                        <asp:Panel ID="pnlHeader" CssClass="w-md-50" runat="server">
                            <asp:Label ID="Label3" runat="server" Text="Location" AssociatedControlID="drpLocationList" />
                            <asp:DropDownList ID="drpLocationList" runat="server" ToolTip="Location where these parts are located" AutoPostBack="false" />

                            <asp:Label ID="Label2" runat="server" Text="Category" AssociatedControlID="drpDropPart" />
                            <asp:DropDownList ID="drpDropPart" runat="server" ToolTip="Drop Part" AutoPostBack="false" />

                            <asp:Label ID="Label1" runat="server" Text="Manufacturer" AssociatedControlID="drpManufacturer" />
                            <asp:DropDownList ID="drpManufacturer" runat="server" ToolTip="Manufacturer" AutoPostBack="false">
                                <asp:ListItem Text="A" Value="A" />
                                <asp:ListItem Text="A" Value="A" />
                                <asp:ListItem Text="A" Value="A" />
                                <asp:ListItem Text="A" Value="A" />
                                <asp:ListItem Text="A" Value="A" />
                            </asp:DropDownList>

                            <asp:Button ID="btnRefresh" runat="server" Text="Refresh" />
                            <asp:Button ID="btnAddNew" runat="server" Text="Add" />
                            <asp:Button ID="btnResetPartNumbers" runat="server" Text="Reset Part Numbers Back To Blank" />
                            <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="btnResetPartNumbers"
                                ConfirmText="Are you sure you want to Reset the ENTIRE Part Number Database to NOTHING?">
                            </asp:ConfirmButtonExtender>
                            <%--<asp:Button ID="btnSaveGridData" runat="server" Text="Save" />--%>
                        </asp:Panel>

                        <!-- NOTE: unable to view changes made to this -->
                        <asp:Panel ID="pnlMainGridPN" runat="server">
                            <asp:HiddenField ID="hdnAllowGridUpdate" runat="server" />
                            <asp:GridView ID="MainGridPN" CssClass="table" runat="server" DataKeyNames="MasterPartsLinkTableID" AutoGenerateColumns="False"
                                AllowPaging="false">
                                <Columns>
                                    <asp:BoundField DataField="MasterPartsID" HeaderText="ID" ReadOnly="True" Visible="false" />

                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="imgDelete" runat="server" ToolTip="Delete this partnumber">
                                                <span class="oi oi-trash"></span>
                                            </asp:LinkButton>
                                            <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="imgDelete"
                                                ConfirmText="Are you sure you want to Delete this partnumber?">
                                            </asp:ConfirmButtonExtender>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="imgAddTransaction" runat="server"  ToolTip="Add New Parts, remove damaged parts etc">
                                                <span class="oi oi-plus"></span>
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="imgTransfer" runat="server" ToolTip="Transfer part to another warehouse">
                                                <span class="oi oi-transfer"></span>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <%--<asp:LinkButton ID="imgChangeCategory" runat="server" ToolTip="Change Category">
                                                <span class="oi oi-file"></span>
                                            </asp:LinkButton>--%>
                                            <asp:LinkButton ID="imgViewIFSLocations" runat="server" ToolTip="View BRG Locations">
                                                <span class="oi oi-folder"></span>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Class">
                                        <ItemTemplate>
                                            <asp:Label ID="lblClassType" runat="server" Text="" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Part">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdnMasterPartLinkTableIDx" runat="server" />
                                            <asp:HiddenField ID="hdnMasterPartID" runat="server" />
                                            <asp:Label ID="lblPartDesc" runat="server" Text="" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="imgEditDemographics" runat="server" ToolTip="Edit Part number, description etc">
                                                <span class="oi oi-pencil"></span>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="PartNumber" HeaderText="MFG. Part #" />
                                    <asp:BoundField DataField="GMPPartNumber" HeaderText="BRG. Part #" />
                                    <asp:BoundField DataField="GMPPartDescription" HeaderText="BRG. Part Desc" />

                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="imgEditDetail" runat="server" ToolTip="Edit QTY/Unit Price">
                                                <span class="oi oi-pencil"></span>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="Quantity" HeaderText="QTY" />
                                    <asp:BoundField DataField="AveragePurchasePrice" HeaderText="Average Purchase Price" />
                                    <asp:BoundField DataField="UnitPrice" HeaderText="Price" />
                                    <asp:BoundField DataField="InWarrentyWorkPrice" HeaderText="Warranty Price" />
                                    <asp:BoundField DataField="QTYMin" HeaderText="Min" />
                                    <asp:BoundField DataField="QTYMax" HeaderText="Max" />
                                    <asp:BoundField DataField="QTYReorder" HeaderText="Reorder" />

                                    <asp:TemplateField HeaderText="Models">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="imgChangeModel" runat="server" ToolTip="Update Model List">
                                                <span class="oi oi-task"></span>
                                            </asp:LinkButton>
                                            <asp:Label ID="txtModelDescription" runat="server" Text="" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </asp:Panel>

                        <asp:Panel ID="pnlAddTransaction" runat="server" Visible="false">

                            <h3>Edit Parts - Add/Adjust inventory</h3>
                            <asp:Label ID="lblEditTransDesc" runat="server" Text="" />

                            <table class="table table-sm">
                                <tr>
                                    <td>MFG. Part #:</td>
                                    <td>BRG. Part #:</td>
                                    <td>BRG. Part Description:</td>
                                    <td>Quantity:</td>
                                </tr>
                                <tr>
                                	<td>
                                        <asp:Label ID="lblMFGPartNumbere" runat="server" Text="..." />
                                    </td>
                                	<td>
                                        <asp:Label ID="lblGMPPartNumbere" runat="server" Text="..." />
                                    </td>
                                	<td>
                                        <asp:Label ID="lblGMPDesce" runat="server" Text="..." />
                                    </td>
                                	<td>
                                        <asp:Label ID="lblQTY" runat="server" Text="..." />
                                    </td>
                                </tr>
                            </table>

                            <hr>

                            <div class="w-md-50">
                                <label>Transaction Type:</label>
                                <asp:DropDownList ID="drpTransType" runat="server" />

                                <label>Location:</label>
                                <asp:DropDownList ID="drpLocationEditParts" runat="server" />

                                <asp:GridView ID="gvLocationEditParts" CssClass="table" runat="server" DataKeyNames="MasterPartsTableIFSLocationStorageID"
                                    AutoGenerateColumns="False" AllowPaging="false">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="imgSelectLocation" runat="server" ToolTip="Select This Location">
                                                    <span class="oi oi-location"></span>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="MasterPartsTableIFSLocationStorageID" HeaderText="ID"
                                            ReadOnly="True" Visible="false">
                                        </asp:BoundField>
                                        <asp:BoundField DataField="QTY" HeaderText="QTY" ReadOnly="True" />
                                        <asp:BoundField DataField="IFSLocation" HeaderText="IFSLocation" />
                                        <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                                        <asp:BoundField DataField="GMPPartNumber" HeaderText="BRGPartNumber" ReadOnly="True" />
                                        <asp:BoundField DataField="GMPPartDescription" HeaderText="BRGPartDescription" />
                                        <asp:BoundField DataField="PartNumber" HeaderText="PartNumber" />
                                        <asp:BoundField DataField="Description" HeaderText="Description" />
                                    </Columns>
                                </asp:GridView>

                                <label>Quantity:</label>
                                <asp:TextBox ID="txtTransQTY" runat="server" ToolTip="QTY - Must be a positive number" />
                
                                <label>PO Vendor:</label>
                                <asp:TextBox ID="txtPOVendor" runat="server" ToolTip="PO Vendor" MaxLength="50" />
  
                                <label>PO Number:</label>
                                <asp:TextBox ID="txtPONumber" runat="server" ToolTip="PO Number" MaxLength="12" />

                                <label>PO Line:</label>
                                <asp:TextBox ID="txtPOLine" runat="server" ToolTip="PO Line" MaxLength="4" />

                                <label>PO Receipt Date:</label>
                                <asp:TextBox ID="txtPOReceiptDate" runat="server" ToolTip="PO Receipt Date" />
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtPOReceiptDate" Format="MM/dd/yyyy" />

                                <label>Part Purchase Price:</label>
                                <asp:TextBox ID="txtTransPurchasePrice" runat="server" ToolTip="Purchase Price" />

                                <label>Misc Description:</label>
                                <asp:TextBox ID="txtTransDesc" runat="server" ToolTip="Misc Description" />

                                <asp:Button ID="btnAddTransactionSave" runat="server" Text="Save" />
                                <asp:Button ID="btnAddTransactionCancel" runat="server" Text="Cancel" />
                            </div>

                        </asp:Panel>

                        <asp:Panel ID="pnlEditPart" runat="server" Visible="false">

                            <h3>Edit Parts - Demographics</h3>
                                 
                            <label>Class:</label>
                            <asp:DropDownList ID="drpClassType" runat="server" />
                                    
                            <label>MFG. Part #:</label>
                            <asp:TextBox ID="txtMFGPartNumbere" runat="server" ToolTip="MFG. Part #"></asp:TextBox>
                             
                            <label>BRG. Part #:</label>
                            <asp:TextBox ID="txtGMPPartNumbere" runat="server" ToolTip="BRG. Part #"></asp:TextBox>
                               
                            <label>BRG. Part Description:</label>
                            <asp:TextBox ID="txtGMPDesce" runat="server" ToolTip="BRG. Part Desc"></asp:TextBox>
                                
                            <label>Unit Price:</label>   
                            <asp:TextBox ID="txtUnitPrice" runat="server" ToolTip="Unit Price"></asp:TextBox>
                                  
                            <label>InWarrenty Price:</label>
                            <asp:TextBox ID="txtInWarrentyPrice" runat="server" ToolTip="In Warrenty Unit Price"></asp:TextBox>
                                
                            <label>Min:</label>
                            <asp:TextBox ID="txtInventoryMin" runat="server" ToolTip="Suggested Minimum inventory total."></asp:TextBox>
                                
                            <label>Max:</label>
                            <asp:TextBox ID="txtInventoryMax" runat="server" ToolTip="Suggested Maximum inventory total"></asp:TextBox>
                               
                            <label>Reorder point:</label>
                            <asp:TextBox ID="txtReorderPoint" runat="server" ToolTip="Reorder Point"></asp:TextBox>
                            
                            <asp:Button ID="btnSavePartNumberse" runat="server" Text="Save" />
                            <asp:Button ID="btnCancelSavee" runat="server" Text="Cancel" />
                                  
                        </asp:Panel>

                        <asp:Panel ID="pnlAddNewPart" runat="server" Visible="false">

                            <div class="w-md-50">
                                <label>Class</label>
                                <asp:DropDownList ID="drpAClassType" runat="server" />
                                <label>MFG. Part #:</label>
                                <asp:TextBox ID="txtMFGPartNumber" runat="server" ToolTip="MFG. Part #" />
                                <label>BRG. Part #:</label>
                                    <asp:TextBox ID="txtGMPPartNumber" runat="server" ToolTip="BRG. Part #" />
                                <label>BRG. Part Description:</label>
                                <asp:TextBox ID="txtGMPDesc" runat="server" ToolTip="BRG. Part Description" />
                                <label>Models:</label>
                                <asp:Label ID="lblPickedModels" runat="server" Text="" />
                            </div>

                            <asp:Panel CssClass="mb-3" runat="server" Height="250" ScrollBars="Auto">
                                    <asp:CheckBoxList ID="chkModels" CssClass="checklist-inline" runat="server" AutoPostBack="True" 
                                        RepeatLayout="Table" RepeatDirection="Horizontal" RepeatColumns="4" />
                            </asp:Panel>

                            <asp:Button ID="btnCancelSave" runat="server" Text="Cancel" />
                            <asp:Button ID="btnSavePartNumbers" runat="server" Text="Save" />

                        </asp:Panel>

                        <asp:Panel ID="pnlEditModels" runat="server" Visible="false">

                            <div class="w-md-50">
                            <label>MFG. Part #:</label>
                            <asp:TextBox ID="EditMFGPart" runat="server" ToolTip="MFG. Part #" ReadOnly="True" />                       
                            <label>BRG. Part #:</label>
                            <asp:TextBox ID="EditGMPPart" runat="server" ToolTip="BRG. Part #" ReadOnly="True" />
                            <label>BRG. Part Description:</label>
                            <asp:TextBox ID="EditModelDesc" runat="server" ToolTip="BRG. Part Desc" ReadOnly="True" />
                            <label>Models:</label>
                            <asp:Label ID="Label4" runat="server" Text="" />

                            <asp:Panel CssClass="mb-3" runat="server" Height="250" ScrollBars="Auto">
                                <asp:CheckBoxList ID="chkEditModels" CssClass="checklist-inline" runat="server"
                                    RepeatColumns="4" RepeatDirection="Horizontal" />
                            </asp:Panel>

                            <asp:Button ID="EditModelSave" runat="server" Text="Save" />
                            <asp:Button ID="EditModelCancel" runat="server" Text="Cancel" />

                        </asp:Panel>

                        <asp:Panel ID="pnlChangeCategory" runat="server" Visible="false">
                            
                            <div class="w-md-50">
                                <label>MFG. Part #:</label>
                                <asp:TextBox ID="EditCategoryMFGPart" runat="server" ToolTip="MFG. Part #" ReadOnly="True" />
                                  
                                <label>BRG. Part #:</label>
                                <asp:TextBox ID="EditCategoryGMPPart" runat="server" ToolTip="BRG. Part #" ReadOnly="True" />
                                  
                                <label>BRG. Part Description:</label>
                                <asp:TextBox ID="EditCategoryDesc" runat="server" ToolTip="BRG. Part Desc" ReadOnly="True" />
                               
                                <label>Models:</label>
                                <asp:Label ID="Label5" runat="server" Text="" />
                           
                                <asp:Label ID="Label6" runat="server" Text="NEW Category" AssociatedControlID="drpChangeCategoryPart" />
                                <asp:DropDownList ID="drpChangeCategoryPart" runat="server" ToolTip="Drop Part" AutoPostBack="true" />
                            </div>

                            <asp:Button ID="EditCategorySave" runat="server" Text="Save" />
                            <asp:Button ID="EditCategoryCancel" runat="server" Text="Cancel" />
  
                        </asp:Panel>

                        <asp:Panel ID="pnlEditDetail" runat="server" Visible="false">
                            
                            <h3><asp:Label ID="Label7" runat="server" Text="Unit Price Transaction Detail" /></h3>
                            <asp:Label ID="lblPartDescription" runat="server" Text="" />

                            <asp:GridView ID="GridViewDetail" CssClass="table" runat="server" DataKeyNames="MasterPartsLinkTablePriceListID"
                                AutoGenerateColumns="False" AllowPaging="false">
                                 <Columns>

                                    <%--<asp:BoundField DataField="MasterPartsLinkTablePriceListID" HeaderText="ID" ReadOnly="True"
                                        Visible="false">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Quantity" HeaderText="QTY">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="QTYDispursed" HeaderText="Dispersed">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="UnitPurchasePrice" HeaderText="Unit Purchase Price">
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:BoundField>--%>

                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdnMasterPartsLinkTablePriceListID" runat="server" />

                                            <%--<asp:HiddenField ID="hdnQTY" runat="server" />
                                            <asp:HiddenField ID="hdnDispursed" runat="server" />
                                            <asp:HiddenField ID="hdnUnitPrice" runat="server" />--%>
                                            <asp:LinkButton ID="imgEditDetail" runat="server" ToolTip="Edit QTY/Unit Price"
                                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>">
                                                <span class="oi oi-pencil"></span>
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="imgEditDetailSave" runat="server" ToolTip="Save" Visible="False"
                                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>">
                                                <span class="oi oi-check"></span>
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="imgEditDetailCancel" runat="server" ToolTip="Cancel" Visible="False"
                                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>">
                                                <span class="oi oi-x"></span>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Quantity">
                                        <ItemTemplate>
                                            <asp:Label ID="lblQTY" runat="server" Visible="true" />
                                            <asp:TextBox ID="txtCorrectedQTY" runat="server" ToolTip="Enter the value the Quantity should be"
                                                Visible="False" MaxLength="10" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Dispersed">
                                        <ItemTemplate>
                                            <asp:Label ID="lblQTYDispursed" runat="server" Visible="true" />
                                            <asp:TextBox ID="txtCorrectedQTYDispursed" runat="server" ToolTip="Enter the value the Quantity Dispersed should be"
                                                Visible="False" MaxLength="10" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Unit Purchase Price">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUnitPurchasePrice" runat="server" Visible="true" />
                                            <asp:TextBox ID="txtCorrectedUnitPurchasePrice" runat="server" ToolTip="Enter the Correct Unit Purchase Price"
                                                Visible="False" MaxLength="10" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="drpReason" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <%--<asp:TemplateField HeaderText="Reason for adjustment">
                                        <ControlStyle Width="60%" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblAdjustmentReason" runat="server" Visible="true"></asp:Label>
                                            <asp:TextBox ID="txtAdjustmentReason" runat="server" ToolTip="Enter the reason a correction was required"
                                                Visible="False" Width="100%" MaxLength="100"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>

                                </Columns>
                            </asp:GridView>

                            <asp:Button ID="btnEditDetailSave" runat="server" Text="Update Average" ToolTip="Update Average Price" />
                            <asp:Button ID="btnEditDetailCancel" runat="server" Text="Close" />
 
                        </asp:Panel>

                        <asp:Panel ID="pnlTransfer" runat="server" Visible="false">
                            <h3>Parts Transfer</h3>
                            
                            <asp:Label ID="lblTransferPart" CssClass="d-block" runat="server" Text="Transfer Part" />
                            <asp:Label ID="lblOriginalWarehouse" CssClass="d-block" runat="server" Text="Original Warehouse" />
                            <hr>
                            <asp:Label ID="Label10" runat="server" Text="IFS Location FROM" AssociatedControlID="drpLocationTransferPartFrom" />
                            <asp:DropDownList ID="drpLocationTransferPartFrom" runat="server" ToolTip="IFS Location FROM" />

                            <asp:GridView ID="gvLocationTransferPartsFrom" CssClass="table" runat="server" DataKeyNames="MasterPartsTableIFSLocationStorageID"
                                AutoGenerateColumns="False" AllowPaging="false">
                                <Columns>
                                    <asp:TemplateField HeaderText="S">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="imgSelectLocation" runat="server" ToolTip="Select This Location">
                                                <span class="oi oi-location"></span>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="MasterPartsTableIFSLocationStorageID" HeaderText="ID" ReadOnly="True" Visible="false" />
                                    <asp:BoundField DataField="QTY" HeaderText="QTY" ReadOnly="True" />
                                    <asp:BoundField DataField="IFSLocation" HeaderText="IFSLocation" />
                                    <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                                    <asp:BoundField DataField="GMPPartNumber" HeaderText="BRGPartNumber" ReadOnly="True" />
                                    <asp:BoundField DataField="GMPPartDescription" HeaderText="BRGPartDescription" />
                                    <asp:BoundField DataField="PartNumber" HeaderText="PartNumber" />
                                    <asp:BoundField DataField="Description" HeaderText="Description" />
                                </Columns>
                            </asp:GridView>

                            <asp:Label ID="Label10x" runat="server" Text="New Location:" AssociatedControlID="drpLocationList2" />
                            <asp:DropDownList ID="drpLocationList2" runat="server" ToolTip="Location where these parts are to go" />

                            <asp:Label ID="Label10hh" runat="server" Text="IFS Location TO:" AssociatedControlID="drpLocationTransferPartTo" />
                            <asp:DropDownList ID="drpLocationTransferPartTo" runat="server" ToolTip="BRG Location TO" />

                            <%--<asp:GridView ID="gvLocationTransferPartsTo" runat="server" Width="100%" DataKeyNames="MasterPartsTableIFSLocationStorageID"
                                AutoGenerateColumns="False" CssClass="mGrid" AlternatingRowStyle-CssClass="alt"
                                AllowPaging="false">
                                <SelectedRowStyle CssClass="srowstyle" />
                                <Columns>
                                    <asp:TemplateField HeaderText="S">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="imgSelectLocation" runat="server" ToolTip="Select This Location">
                                                <spam class="oi oi-location"></spam>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="MasterPartsTableIFSLocationStorageID" HeaderText="ID" ReadOnly="True" Visible="false" />
                                    <asp:BoundField DataField="QTY" HeaderText="QTY" ReadOnly="True" />
                                    <asp:BoundField DataField="IFSLocation" HeaderText="IFSLocation" />
                                    <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                                    <asp:BoundField DataField="GMPPartNumber" HeaderText="BRGPartNumber" ReadOnly="True" />
                                    <asp:BoundField DataField="GMPPartDescription" HeaderText="BRGPartDescription" />
                                    <asp:BoundField DataField="PartNumber" HeaderText="PartNumber" />
                                    <asp:BoundField DataField="Description" HeaderText="Description" />
                                </Columns>
                            </asp:GridView>--%>

                            <asp:Label ID="Label8" runat="server" Text="QTY to Transfer:" AssociatedControlID="txtTransferQTY" />
                            <asp:TextBox ID="txtTransferQTY" runat="server" ToolTip="QTY To Transfer" />

                            <div class="form-check">
                                <asp:CheckBox ID="chkAveragePurchasePrice" runat="server" Checked="True" />
                                <asp:Label ID="Label11" runat="server" Text="Calculate Average Purchase Price" AssociatedControlID="chkAveragePurchasePrice" />
                            </div>

                            <asp:Label ID="Label9" runat="server" Text="Reason for Transfer:" AssociatedControlID="txtTransferReason" />
                            <asp:TextBox ID="txtTransferReason" runat="server" ToolTip="Reason for Transfer:" MaxLength="50" />

                            <asp:Label ID="lblTransferMessage" runat="server" Text="" />

                            <asp:Button ID="btnTransferSave" runat="server" Text="Save" ToolTip="Record the transfer" />
                            <asp:Button ID="btnTransferCancel" runat="server" Text="Close" />
                        </asp:Panel>

                        <!-- NOTE: unable to view changes made to this -->
                        <asp:Panel ID="pnlLocationView" runat="server" Visible="false">
                            <h3><asp:Label ID="Label12" runat="server" Text="View Locations" /></h3>

                            <asp:GridView ID="gvLoactions" CssClass="table" runat="server" DataKeyNames="MasterPartsTableIFSLocationStorageID"
                                AutoGenerateColumns="False" AllowPaging="false">
                                <Columns>
                                    <asp:BoundField DataField="MasterPartsTableIFSLocationStorageID" HeaderText="ID" ReadOnly="True" Visible="false" />
                                    <asp:BoundField DataField="QTY" HeaderText="QTY" ReadOnly="True" />
                                    <asp:BoundField DataField="IFSLocation" HeaderText="BRGLocation" />
                                    <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                                    <asp:BoundField DataField="GMPPartNumber" HeaderText="BRGPartNumber" ReadOnly="True" />
                                    <asp:BoundField DataField="GMPPartDescription" HeaderText="BRGPartDescription" />
                                    <asp:BoundField DataField="PartNumber" HeaderText="PartNumber" />
                                    <asp:BoundField DataField="Description" HeaderText="Description" />
                                </Columns>
                            </asp:GridView>

                            <%--<asp:Button ID="Button1" runat="server" Text="Update Average" ToolTip="Update Average Price" />--%>
                            <asp:Button ID="btnLocationViewClose" runat="server" Text="Close" />
                        </asp:Panel>

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
