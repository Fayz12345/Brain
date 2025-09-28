<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Maint_MasterCarrer.aspx.cs" Inherits="BW_WebApp.Maintenance.Maint_MasterCarrer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Syncfusion.Shared.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89" Namespace="Syncfusion.Web.UI.WebControls.Shared" TagPrefix="syncfusion" %>
<asp:Content ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="cMC" runat="server">

    <asp:Panel ID="pnlUpload" CssClass="card p-3" runat="server">
        <div class="maintupload">
            
            <div class="row">
            	<div class="col-sm">
                    <div class="custom-file mb-2">
                        <asp:FileUpload ID="FileUploadXLS" class="custom-file-input" runat="server" />
                        <label class="custom-file-label">Choose file</label>
                    </div>
                </div>
            	<div class="col-sm text-sm-right">
                    <asp:Button ID="btnUpload" runat="server" Text="Upload" />
                    <asp:Button ID="btnDownload" runat="server" Text="Download" />
                </div>
            </div>
            
            <asp:Label ID="lblMsgDetail" runat="server" Visible="False" />

            <div class="alert alert-info mb-0">
                <p>Format required for upload is equal to the XLS downloaded</p>
                
                <p>NOTE: Columns B to F can be blank if the values are not known. 
                If Column B is given, an update will happen. If B is not supplied but the other ID columns
                or the ABBR Columns match with an existing record, it is updated. Otherwise added.</p>
                
                <p>NOTE: it is best to have column B if you are deleting that row.</p>
            </div>
        </div>
    </asp:Panel>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            

            <asp:Panel ID="pnlMainView" runat="server">
                <div class="row mt-3">
                	<div class="col">
                        <h1><asp:Label ID="lblRecordTitle" runat="server" Text="Model Table" /></h1>
                    </div>
                    <div class="col text-right">
                        <asp:Button ID="btnAdd" runat="server" Text="Add New" OnClick="btnAdd_Click" ToolTip="This will open the Add new window" />
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" Visible="False" OnClick="btnEdit_Click" />
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" Visible="False" OnClick="btnDelete_Click" />
                        <asp:ConfirmButtonExtender runat="server" ConfirmText="Continue with Delete?" Enabled="True" TargetControlID="btnDelete" />
                        <asp:Button ID="btnPrintScanCode" runat="server" Text="Print Scancodes" Visible="True" OnClientClick="PrintScanCodes();return false;" />
                    </div>
                </div>
                
                <h3><asp:Label runat="server" Text="Search / SKU Definition:" /></h3>
                <div class="form-row">
                	<div class="col-sm-6 col-md">
                        <asp:CheckBox ID="chkManufacturerABBR" runat="server" Checked="True" Text="Manufacturer:" />
                        <asp:TextBox ID="txtManufacturerABBR" runat="server" MaxLength="3" TabIndex="1" ToolTip="Manufacturer ABBR" />
                    </div>
                	<div class="col-sm-6 col-md">
                        <asp:CheckBox ID="chkModelABBR" runat="server" Checked="True" Text="Model:" />
                        <asp:TextBox ID="txtModelABBR" runat="server" MaxLength="20" TabIndex="2" ToolTip="Model ABBR" />
                    </div>
                	<div class="col-sm-6 col-md">
                        <asp:CheckBox ID="chkCarrierABBR" runat="server" Checked="True" Text="Carrier:" />
                        <asp:TextBox ID="txtCarrierABBR" runat="server" MaxLength="3" TabIndex="3" ToolTip="Carrier ABBR" />
                    </div>
                	<div class="col-sm-6 col-md">
                        <asp:CheckBox ID="chkColourABBR" runat="server" Checked="True" Text="Colour:" />
                        <asp:TextBox ID="txtColourABBR" runat="server" MaxLength="3" TabIndex="4" ToolTip="Colour ABBR" />
                    </div>
                </div>
                
                <asp:Button ID="btnRefresh" runat="server" Text="Search" TabIndex="4" />
                <asp:Button ID="btnViewDetail" runat="server" Text="Detail" TabIndex="5" Visible="False" />

                <asp:Panel CssClass="card" runat="server" ScrollBars="Auto">
                    <asp:GridView ID="MainGrid" CssClass="table" runat="server" AutoGenerateSelectButton="True" DataKeyNames="MasterCarrierManufacturerLookupID"
                        AutoGenerateColumns="False" SelectedRowStyle-Wrap="False" SortedAscendingCellStyle-Wrap="False" SortedAscendingHeaderStyle-Wrap="False"
                        SortedDescendingCellStyle-Wrap="False" RowStyle-Wrap="False" PagerStyle-Wrap="False" HeaderStyle-Wrap="False" FooterStyle-Wrap="False"
                        EmptyDataRowStyle-Wrap="False" EditRowStyle-Wrap="False" AlternatingRowStyle-Wrap="False">
                        <Columns>
                            <asp:BoundField DataField="MasterCarrierManufacturerLookupID" HeaderText="ID" ReadOnly="True" Visible="false" />
                            <asp:BoundField DataField="Carrier" HeaderText="Carrier" ReadOnly="True" />
                            <asp:BoundField DataField="Manufacturer" HeaderText="Manufacturer" ReadOnly="True" />
                            <asp:BoundField DataField="Model" HeaderText="Model" ReadOnly="True" />
                            <asp:BoundField DataField="Colour" HeaderText="Colour" ReadOnly="True" />
                            <asp:BoundField DataField="Condition" HeaderText="Stock Colour Code" ReadOnly="True" />
                            <asp:BoundField DataField="SKU" HeaderText="SKU" ReadOnly="True" />
                            <asp:BoundField DataField="SKU_B" HeaderText="SKU B" ReadOnly="True" />
                            <asp:BoundField DataField="SKU_C" HeaderText="SKU C" ReadOnly="True" />
                            <asp:BoundField DataField="SKU_Loaner" HeaderText="SKU Loaner" ReadOnly="True" />
                            <asp:BoundField DataField="UPC" HeaderText="Grade A UPC" ReadOnly="True" />
                            <asp:BoundField DataField="UPC_2" HeaderText="Grade B UPC" ReadOnly="True" />
                            <asp:BoundField DataField="UPC_3" HeaderText="Grade C UPC" ReadOnly="True" />
                            <asp:BoundField DataField="Description" HeaderText="Description" ReadOnly="True" />
                            <asp:BoundField DataField="WarrantyStickerPlacement" HeaderText="Warranty Sticker Placement" ReadOnly="True" />
                            <asp:BoundField DataField="Device_Handset" HeaderText="Device Handset" ReadOnly="True" />
                            <asp:BoundField DataField="Bar_Flip" HeaderText="Bar Flip" ReadOnly="True" />
                            <asp:BoundField DataField="CDMA_HSPA" HeaderText="CDMA HSPA" ReadOnly="True" />
                            <asp:BoundField DataField="NickName" HeaderText="Nickname" ReadOnly="True" />
                            <asp:BoundField DataField="Unit_OS" HeaderText="OS" ReadOnly="True" />
                            <asp:BoundField DataField="OptionCarrierID" HeaderText="CarrierID" ReadOnly="True" Visible="false" />
                            <asp:BoundField DataField="OptionManufacturerID" HeaderText="ManuID" ReadOnly="True" Visible="false" />
                            <asp:BoundField DataField="OptionModelID" HeaderText="ModelID" ReadOnly="True" Visible="false" />
                            <asp:BoundField DataField="OptionColourID" HeaderText="ColourID" ReadOnly="True" Visible="false" />
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
            </asp:Panel>

            <asp:Panel ID="pnlAdd" runat="server">
                <h1>Add Model Table Record</h1>
                <div class="row">
                	<div class="col-md">
                        <label>Carrier:</label>
                        <asp:DropDownList ID="drpAddCarrier" runat="server" />

                        <label>Manufacturer:</label>
                        <asp:DropDownList ID="drpAddManufacturer" runat="server" />

                        <label>Model:</label>
                        <asp:DropDownList ID="drpAddModel" runat="server" />

                        <label>Colour:</label>
                        <asp:DropDownList ID="drpAddColour" runat="server" />

                        <label>Device Handset:</label>
                        <asp:DropDownList ID="drpAddDeviceHandset" runat="server" ToolTip="Error scenario" />
                        <%--<asp:TextBox ID="AddDeviceHandset" runat="server" />--%>

                        <label>Unit OS:</label>
                        <asp:DropDownList ID="AddUnitOS" runat="server" />

                        <label>Style:</label>
                        <asp:RadioButtonList ID="rdlAddStyle" CssClass="radiolist-inline" RepeatDirection="Horizontal" runat="server">
                            <asp:ListItem Text="Bar" />
                            <asp:ListItem Text="Flip" />
                        </asp:RadioButtonList>

                        <label>H/S Type:</label>
                        <asp:RadioButtonList ID="rdlAddHSType" CssClass="radiolist-inline" RepeatDirection="Horizontal" runat="server">
                            <asp:ListItem Text="CDMA" />
                            <asp:ListItem Text="HSPA" />
                        </asp:RadioButtonList>
                    </div>

                	<div class="col-md">
                        <label>Nickname:</label>
                        <asp:TextBox ID="AddNickName" runat="server" />

                        <label>Stock Colour Code:</label>
                        <asp:DropDownList ID="drpAddCondition" runat="server" />
                        <%--<asp:TextBox ID="AddCondition" runat="server" />--%>

                        <label>Grade A SKU:</label>
                        <asp:TextBox ID="AddSKU" runat="server" MaxLength="20" />

                        <label>Grade B SKU:</label>
                        <asp:TextBox ID="AddSKU_B" runat="server" MaxLength="20" />

                        <label>Grade C SKU:</label>
                        <asp:TextBox ID="AddSKU_C" runat="server" MaxLength="20" />

                        <label>A grouping SKU for types of loaner product:</label>
                        <asp:TextBox ID="AddSKU_Loaner" runat="server" MaxLength="20" />

                        <label>Grade A UPC:</label>
                        <asp:TextBox ID="AddUPC" runat="server" />

                        <label>Grade B UPC:</label>
                        <asp:TextBox ID="AddUPC2" runat="server" />

                        <label>Grade C UPC:</label>
                        <asp:TextBox ID="AddUPC3" runat="server" />

                        <label>Description:</label>
                        <asp:TextBox ID="AddDescription" runat="server" />

                        <label>Warranty Sticker Placement:</label>
                        <asp:TextBox ID="AddWarrantyStickerPlacement" runat="server" />
                    </div>
                </div>
                <asp:Button ID="AddOK" runat="server" Text="OK" OnClick="AddOK_Click" />
                <asp:Button ID="AddCancel" runat="server" Text="Cancel" OnClick="AddCancel_Click1" />
            </asp:Panel>

            <asp:Panel ID="pnlEdit" runat="server">
                <h1>Edit Model Table Record</h1>
                <div class="row">
                	<div class="col">
                        <label>Carrier:</label>
                        <asp:DropDownList ID="drpEditCarrier" runat="server" />

                        <label>Manufacturer:</label>
                        <asp:DropDownList ID="drpEditManufacturer" runat="server" />
                        <%--<asp:Button ID="btnSyncManufacturer" runat="server" Text="Sync" />--%>

                        <label>Model:</label>
                        <asp:DropDownList ID="drpEditModel" runat="server" />
                        <%--<asp:Button ID="btnSyncModel" runat="server" Text="Sync" />--%>

                        <label>Colour:</label>
                        <asp:DropDownList ID="drpEditColour" runat="server" />
                        <%--<asp:Button ID="btnSyncColour" runat="server" Text="Sync" />--%>

                        <label>Device Handset:</label>
                        <asp:DropDownList ID="drpEditDeviceHandset" runat="server" ToolTip="Error scenario" />
                        <%--<asp:TextBox ID="EditDeviceHandset" runat="server" />--%>

                        <label>Unit OS:</label>
                        <asp:DropDownList ID="EditUnitOS" runat="server" />

                        <label>Style:</label>
                        <asp:RadioButtonList ID="rdlEditStyle" CssClass="radiolist-inline" RepeatDirection="Horizontal" runat="server">
                            <asp:ListItem Text="Bar" />
                            <asp:ListItem Text="Flip" />
                        </asp:RadioButtonList>

                        <label>H/S Type:</label>
                        <asp:RadioButtonList ID="rdlEditHSType" CssClass="radiolist-inline" RepeatDirection="Horizontal" runat="server">
                            <asp:ListItem Text="CDMA" />
                            <asp:ListItem Text="HSPA" />
                        </asp:RadioButtonList>
                    </div>

                	<div class="col">
                        <label>Nickname:</label>
                        <asp:TextBox ID="EditNickName" runat="server" />
                    
                        <label>Stock Colour Code:</label>
                        <asp:DropDownList ID="drpEditCondition" runat="server" />
                        <%--<asp:TextBox ID="EditCondition" runat="server" />--%>

                        <label>Grade A SKU:</label>
                        <asp:TextBox ID="EditSKU" runat="server" MaxLength="20" />

                        <label>Grade B SKU:</label>
                        <asp:TextBox ID="EditSKU_B" runat="server" MaxLength="20" />

                        <label>Grade C SKU:</label>
                        <asp:TextBox ID="EditSKU_C" runat="server" MaxLength="20" />

                        <label>A grouping SKU for types of loaner product:</label>
                        <asp:TextBox ID="EditSKU_Loaner" runat="server" MaxLength="20" />

                        Grade A UPC:
                        <asp:TextBox ID="EditUPC" runat="server" />

                        <label>Grade B UPC:</label>
                        <asp:TextBox ID="EditUPC2" runat="server" />

                        <label>Grade C UPC:</label>
                        <asp:TextBox ID="EditUPC3" runat="server" />

                        <label>Description:</label>
                        <asp:TextBox ID="EditDescription" runat="server" />

                        <label>Warranty Sticker Placement:</label>
                        <asp:TextBox ID="EditWarrantyStickerPlacement" runat="server" />
                    </div>
                </div>
                <asp:Button ID="EditOK" runat="server" Text="OK" OnClick="EditOK_Click" />
                <asp:Button ID="EditCancel" runat="server" Text="Cancel" OnClick="EditCancel_Click" />
            </asp:Panel>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

<asp:Content ContentPlaceHolderID="js" runat="server">
    <script type="text/javascript">

        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

        function BeginRequestHandler(sender, args) {
            if (args._postBackElement.id != "SkinTable1") {
                // ConfigureWaitingPopup(Popup);
                $('#loading').show();
            }
        }

        function EndRequestHandler(sender, args) {

            $('#loading').hide();
        }

        function PrintScanCodes() {
            // var win = window.open("ViewDoc.aspx", "_blank", "status=no,toolbar=no,menubar=no,location=no,titlebar=no,width=600px,height=540px", true);
            var xDataList = {};
            xDataList["Table"] = "MASTERCARRIER";
            // xDataList["ID"] = "";
            // xDataList["PROJECT"] = "";
            var pstring = ""; // GetParameterStream(xDataList);
            var WindowToOpen = "/Reports/RPT_MasterCarrierScanCodeList.aspx";
            if (pstring.length > 0) {
                WindowToOpen = WindowToOpen + "?" + pstring
            }
            // var win = window.open(WindowToOpen, "_blank", "width=100,height=50,menubar", true);
            var win = window.open(WindowToOpen, "_blank", "", true);
            // var win = window.open(WindowToOpen, "_blank", "menubar", true);
            // win.focus();
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
    </script>
</asp:Content>

