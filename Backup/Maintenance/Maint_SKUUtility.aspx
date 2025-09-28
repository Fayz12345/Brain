<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Maint_SKUUtility.aspx.cs" Inherits="BW_WebApp.Maintenance.Maint_SKUUtility" %>
<%@ Register Assembly="Syncfusion.Shared.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89" Namespace="Syncfusion.Web.UI.WebControls.Shared" TagPrefix="syncfusion" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cMC" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            
            <asp:HiddenField ID="hdnUserName" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnCurrentIMEI" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnCarrier3ID" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnManufacturer3ID" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnModel3ID" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnColour3ID" runat="server" ClientIDMode="Static" />

            <asp:HiddenField ID="hdnCarrierID" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnManufacturerID" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnModelID" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnColourID" runat="server" ClientIDMode="Static" />

            <asp:HiddenField ID="hdnCarrierTEXT" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnManufacturerTEXT" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnModelTEXT" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnColourTEXT" runat="server" ClientIDMode="Static" />
            <%--<asp:HiddenField ID="hdnMemoryID" runat="server" ClientIDMode="Static" />--%>

            <h1>SKU Utility</h1>

            <asp:TabContainer ID="TabContainer3" CssClass="tab-container" runat="server" AutoPostBack="True">
                <asp:TabPanel ID="TabSKUUComboSearch3" CssClass="tab-panel" runat="server" HeaderText="UPC Utility" ToolTip="UPC Utility">
                    <ContentTemplate>
                        <h3>SKU Lookup</h3>
                        <asp:Label ID="lblDeviceToResku3" runat="server" Text="" />
                        <table class="table">
                        	<tr>
                                <td></td>
                                <td>
                                    <asp:Button ID="btnSKUClear3" runat="server" Text="Clear" />
                                    <asp:Button ID="btnRefresh3" runat="server" Text="Refresh" />
                                </td>
                                <td></td>
                            </tr>
                        
                            <tr>
                                <td>UPC Code:</td>
                                <td colspan="2">
                                    <asp:TextBox ID="txtUPCCode" runat="server" Width="100%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                        	    <td>Carrier:</td>
                                <td>
                                    <asp:DropDownList ID="drpCarrier3" runat="server" AutoPostBack="True" />
                                </td>
                                <td>
                                    <asp:Label ID="lblCarrierABBR3" runat="server" Text="ABBR" />
                                </td>
                            </tr>
                            <tr>
                        	    <td>Manufacturer:</td>
                                <td>
                                    <asp:DropDownList ID="drpManufacturer3" runat="server" AutoPostBack="True" />
                                </td>
                                <td>
                                    <asp:Label ID="lblManufacturerABBR3" runat="server" Text="ABBR" />
                                </td>
                            </tr>
                            <tr>
                                <td>Model:</td>
                                <td>
                                    <asp:DropDownList ID="drpModel3" runat="server" AutoPostBack="True" />
                                </td>
                                <td>
                                    <asp:Label ID="lblModelABBR3" runat="server" Text="ABBR" />
                                </td>
                            </tr>
                            <tr>
                                <td>Colour:</td>
                                <td>
                                    <asp:DropDownList ID="drpColour3" runat="server" AutoPostBack="True" />
                                </td>
                                <td>
                                    <asp:Label ID="lblColourABBR3" runat="server" Text="ABBR" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Button ID="btnAdd" runat="server" Text="Add" />
                                </td>
                                <td>
                                    <asp:Button ID="btnDelete" runat="server" Text="Delete" />
                                </td>
                            </tr>



                        </table>

                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="TabSKUUComboSearch" CssClass="tab-panel" runat="server" HeaderText="Lookup" ToolTip="SKU Combo Lookup">
                    <ContentTemplate>
                        <h3>SKU Lookup</h3>
                        <asp:Label ID="lblDeviceToResku" runat="server" Text="" />
                        <table class="table">
                        	<tr>
                                <td></td>
                                <td>
                                    <asp:Button ID="btnSKUClear" runat="server" Text="Clear" />
                                    <asp:Button ID="btnRefresh" runat="server" Text="Refresh" />
                                </td>
                                <td></td>
                            </tr>
                        
                            <tr>
                        	    <td>Carrier:</td>
                                <td>
                                    <asp:DropDownList ID="drpCarrier" runat="server" AutoPostBack="True" />
                                </td>
                                <td>
                                    <asp:Label ID="lblCarrierABBR" runat="server" Text="ABBR" />
                                </td>
                            </tr>
                            <tr>
                        	    <td>Manufacturer:</td>
                                <td>
                                    <asp:DropDownList ID="drpManufacturer" runat="server" AutoPostBack="True" />
                                </td>
                                <td>
                                    <asp:Label ID="lblManufacturerABBR" runat="server" Text="ABBR" />
                                </td>
                            </tr>
                            <tr>
                                <td>Model:</td>
                                <td>
                                    <asp:DropDownList ID="drpModel" runat="server" AutoPostBack="True" />
                                </td>
                                <td>
                                    <asp:Label ID="lblModelABBR" runat="server" Text="ABBR" />
                                </td>
                            </tr>
                            <tr>
                                <td>Colour:</td>
                                <td>
                                    <asp:DropDownList ID="drpColour" runat="server" AutoPostBack="True" />
                                </td>
                                <td>
                                    <asp:Label ID="lblColourABBR" runat="server" Text="ABBR" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="MoveDownCombo" CssClass="btn btn-default" runat="server" ToolTip="Copy Data to lower grid.">
                                        <span class="oi oi-arrow-bottom"></span>
                                    </asp:LinkButton>
                                </td>
                                <td>
                                    <%--<asp:Button ID="btnToSKU" runat="server" Text="Never push this button... ever." />--%>
                                </td>
                                <td>
                                    <%--<asp:Button ID="btnCopyToClip" runat="server" Text="Copy to Clipboard" ToolTip="Copy to Clipboard" />--%>
                                </td>
                            </tr>
                        </table>

                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="TabUpdate" CssClass="tab-panel" runat="server" HeaderText="Segments" ToolTip="Add new Segment ABBR">
                    <ContentTemplate>
                        <asp:Panel ID="pnlAddOption" runat="server">
                            <table class="table">
                                <tr>
                                    <td>Type:</td>
                                    <td>
                                        <asp:RadioButtonList ID="rdlSegment" CssClass="radiolist-inline" runat="server" ToolTip="Choose the Segment you wish to work on."
                                            RepeatDirection="Horizontal">
                                            <asp:ListItem Text="Carrier" Selected="True" />
                                            <asp:ListItem Text="Manufacturer" />
                                            <asp:ListItem Text="Model" />
                                            <asp:ListItem Text="Colour" />
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Abbreviation:</td>
                                    <td>
                                        <asp:TextBox ID="AddNameOption" runat="server" MaxLength="20" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Text (Description):</td>
                                    <td>
                                        <asp:TextBox ID="AddDescriptionOption" runat="server" MaxLength="50" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:LinkButton ID="MoveDownQuestion" CssClass="btn btn-default" runat="server" ToolTip="Copy Data to lower grid.">
                                            <span class="oi oi-arrow-bottom"></span>
                                        </asp:LinkButton>
                                    </td>
                                    <td>
                                        <asp:Button ID="AddOptionOK" runat="server" Text="Add to question" />
                                        <asp:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" TargetControlID="AddOptionOK"
                                            ConfirmText="Are you sure you are adding to the correct Question! Continue with Add?" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Label ID="lblMessageBTM" runat="server" Text="" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>

            <asp:Panel ID="pnlHome" runat="server">
                <table class="table">
                    <tr>
                        <td>Carrier:</td>
                        <td>Manufacturer:</td>
                        <td>Model:</td>
                        <td>Colour:</td>
                        <td></td>
                    </tr>

                    <tr>
                        <td>
                            <asp:Label ID="lblCarrier" runat="server" Text="" />
                        </td>
                        <td>
                            <asp:Label ID="lblManufacturer" runat="server" Text="" />
                        </td>
                        <td>
                            <asp:Label ID="lblModel" runat="server" Text="" />
                        </td>
                        <td>
                            <asp:Label ID="lblColour" runat="server" Text="" />
                        </td>
                        <td></td>
                    </tr>

                    <tr>
                        <td>
                            <asp:TextBox ID="txtCarrier1" runat="server" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtManufacturer1" runat="server" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtModel1" runat="server" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtColour1" runat="server" />
                        </td>
                        <td>
                            <asp:Button ID="btnVerify" runat="server" Text="xxVerify" ToolTip="If the combo is not found, a window will open to allow you to make it a valid combo." />
                            <asp:Button ID="btnDisolve" runat="server" Text="Disolver" visible="false" />
                        </td>
                    </tr>

                    <tr>
                        <td colspan="4">
                            <asp:Panel ID="pnlAdd" runat="server" Visible="false" BackColor="#FFFFCC">
                                <table runat="server" bgcolor="#FFFFCC">
                                    <tr>
                                        <td id="AddTemplateHeader" colspan="2">
                                            <h3>Add Model Table Record</h3>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                <table id="Table2" runat="server" bgcolor="#FFFFCC">
                                                <tr>
                                                    <td>Carrier:</td>
                                                    <td>
                                                        <asp:TextBox ID="txtCarrier" runat="server" Enabled="False" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Manufacturer:</td>
                                                    <td>
                                                        <asp:TextBox ID="txtManufacturer" runat="server" Enabled="False" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Model:</td>
                                                    <td>
                                                        <asp:TextBox ID="txtModel" runat="server" Enabled="False" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Colour:</td>
                                                    <td>
                                                        <asp:TextBox ID="txtColour" runat="server" Enabled="False" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Device Handset:</td>
                                                    <td>
                                                        <asp:DropDownList ID="drpAddDeviceHandset" runat="server" ToolTip="Error scenario" />
                                                        <%--<asp:TextBox ID="AddDeviceHandset" runat="server" />--%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Unit OS:</td>
                                                    <td>
                                                        <asp:DropDownList ID="AddUnitOS" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Style:</td>
                                                    <td>
                                                        <asp:RadioButtonList ID="rdlAddStyle" CssClass="radiolist" runat="server">
                                                            <asp:ListItem Text="Bar" />
                                                            <asp:ListItem Text="Flip" />
                                                        </asp:RadioButtonList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>H/S Type:</td>
                                                    <td>
                                                        <asp:RadioButtonList ID="rdlAddHSType" CssClass="radiolist" runat="server">
                                                            <asp:ListItem Text="CDMA" />
                                                            <asp:ListItem Text="HSPA" />
                                                        </asp:RadioButtonList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                <table id="Table1" runat="server" bgcolor="#FFFFCC">
                                                <tr>
                                                    <td>NickName:</td>
                                                    <td>
                                                        <asp:TextBox ID="AddNickName" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Stock Colour Code:</td>
                                                    <td>
                                                        <asp:DropDownList ID="drpAddCondition" runat="server" />
                                                        <%--<asp:TextBox ID="AddCondition" runat="server" />--%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Grade A SKU:</td>
                                                    <td>
                                                        <asp:TextBox ID="AddSKU" runat="server" MaxLength="20" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Grade B SKU:</td>
                                                    <td>
                                                        <asp:TextBox ID="AddSKU_B" runat="server" MaxLength="20" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Grade C SKU:</td>
                                                    <td>
                                                        <asp:TextBox ID="AddSKU_C" runat="server" MaxLength="20" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>A grouping SKU for types of loaner product:</td>
                                                    <td>
                                                        <asp:TextBox ID="AddSKU_Loaner" runat="server" MaxLength="20" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Grade A UPC:</td>
                                                    <td>
                                                        <asp:TextBox ID="AddUPC" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Grade B UPC:</td>
                                                    <td>
                                                        <asp:TextBox ID="AddUPC2" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Grade C UPC:</td>
                                                    <td>
                                                        <asp:TextBox ID="AddUPC3" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Description:</td>
                                                    <td>
                                                        <asp:TextBox ID="AddDescription" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Warranty Sticker Placement:</td>
                                                    <td>
                                                        <asp:TextBox ID="AddWarrantyStickerPlacement" runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td colspan="2">
                                            <asp:Button ID="btnValidate" runat="server" Text="Validate/Add" ToolTip="This will add the new combination" />
                                        <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="btnValidate"
                                            ConfirmText="Are you sure you want to add this combination to the master model lookup table! Continue with Add?" />
                                            <asp:Button ID="AddCancel" runat="server" Text="Cancel" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>

                    <tr>
                        <td colspan="4">
            <asp:TextBox ID="MasterTableMessage" runat="server" ReadOnly="True" TextMode="MultiLine" Rows="4" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>

            
<%--            <asp:TextBox ID="TextBox1" runat="server" ReadOnly="True" TextMode="MultiLine" Rows="4" />--%>


        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

<asp:Content ContentPlaceHolderID="js" runat="server">

    <script type="text/javascript">

        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

        function BeginRequestHandler(sender, args) {
            if (args._postBackElement.id != "SkinTable1") {
                //                ConfigureWaitingPopup(Popup);
                //               alert(DTE.value);
                $('#loading').show();
            }
        }

        function EndRequestHandler(sender, args) {
            $('#loading').hide();
        }



        //----------------------------------------------------------------

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

        // Variable Functions ----------------------------------
        function UserName() {
            return $get("<%= hdnUserName.ClientID %>").value;
        }


        //-----------------------------------------------------------------------------------------------

        function SetupDropDown(DropDownName) {


            if (DropDownName == 'Lab Destination:' + DropDownName) {

            }
            else {

                FillDropDown(DropDownName);
            }
            return;
        }

        function FillDropDown(DropDownName) {

            var service = new WebServer_01();
            if (DropDownName == 'Carrier') {
                var x = $get("<%= hdnCarrierID.ClientID %>").value;

                if (x == null || x.length == 0) { return; }
                var ctr = $get($get("<%= hdnCarrierID.ClientID %>").value);
                if (ctr == null) { return; }
                var rValue = service.GetManufacturerDropDownData(GetDropDownValue($get("<%= hdnCarrierID.ClientID %>").value), UserName(), onFillManufacturerList, onFillManufacturerListError, null);
                return;
            }
            if (DropDownName == 'Manufacturer') {
                return;
                var x = $get("<%= hdnCarrierID.ClientID %>").value;
                if (x == null || x.length == 0) { return; }

                var ctr = $get($get("<%= hdnCarrierID.ClientID %>").value);
                if (ctr == null) { return; }
                ctr = $get($get("<%= hdnManufacturerID.ClientID %>").value);
                if (ctr == null) { return; }
                var rValue = service.GetModelDropDownData(GetDropDownValue($get("<%= hdnCarrierID.ClientID %>").value), GetDropDownValue($get("<%= hdnManufacturerID.ClientID %>").value), UserName(), onFillModelList, null, null);
                return;
            }
            if (DropDownName == 'Model') {
                var x = $get("<%= hdnCarrierID.ClientID %>").value;
                if (x == null || x.length == 0) { return; }
                var ctr = $get($get("<%= hdnCarrierID.ClientID %>").value);
                if (ctr == null) { return; }
                ctr = $get($get("<%= hdnManufacturerID.ClientID %>").value);
                if (ctr == null) { return; }
                ctr = $get($get("<%= hdnModelID.ClientID %>").value);
                if (ctr == null) { return; }
                var rValue = service.GetColourDropDownData(GetDropDownValue($get("<%= hdnCarrierID.ClientID %>").value), GetDropDownValue($get("<%= hdnManufacturerID.ClientID %>").value), GetDropDownValue($get("<%= hdnModelID.ClientID %>").value), UserName(), onFillColourList, null, null);
                return;
            }
        }

        function onFillManufacturerListError(Result) {
            alert('Error - onFillManufacturerListError:' + Result);
        }
        function onFillManufacturerList(Result) {
            //           if (MCL('hdnisMasterLinked').value != 'True') { return; }
            //           alert("FillManufacturerList:" + Result);
            var DropDown = $get($get("<%= hdnManufacturerID.ClientID %>").value);
            if (DropDown != null) {
                var CurrentValue = GetDropDownValue($get("<%= hdnManufacturerID.ClientID %>").value);
                while (DropDown.options.length > 0) DropDown.remove(0);
                if (Result.length > 0) {
                    ClientData = eval('({' + Result + '})');
                    for (var key in ClientData) {
                        var attrName = key;
                        var attrValue = ClientData[key];
                        addOption(DropDown, key, ClientData[key], CurrentValue)
                    }
                }
            }
            FillDropDown('Manufacturer');
            return;
        }
        function onFillModelList(Result) {
            //           if (MCL('hdnisMasterLinked').value != 'True') { return; }
            var DropDown = $get($get("<%= hdnModelID.ClientID %>").value)
            if (DropDown != null) {
                var CurrentValue = GetDropDownValue($get("<%= hdnModelID.ClientID %>").value);
                while (DropDown.options.length > 0) DropDown.remove(0);
                if (Result.length > 0) {
                    ClientData = eval('({' + Result + '})');
                    for (var key in ClientData) {
                        var attrName = key;
                        var attrValue = ClientData[key];
                        addOption(DropDown, key, ClientData[key], CurrentValue)
                    }
                }
            }
            FillDropDown('Model');
            return;
        }
        function onFillColourList(Result) {
            //           if (MCL('hdnisMasterLinked').value != 'True') { return; }
            var DropDown = $get($get("<%= hdnColourID.ClientID %>").value)
            if (DropDown != null) {
                var CurrentValue = GetDropDownValue($get("<%= hdnColourID.ClientID %>").value);
                while (DropDown.options.length > 0) DropDown.remove(0);
                if (Result.length > 0) {
                    ClientData = eval('({' + Result + '})');
                    for (var key in ClientData) {
                        var attrName = key;
                        var attrValue = ClientData[key];
                        addOption(DropDown, key, ClientData[key], CurrentValue)
                    }
                }
            }
            return;
        }

        function GetDropDownValue(Name) {
            var IndexValue = $get(Name).selectedIndex;
            var xValue = '';
            if (IndexValue > -1) { xValue = $get(Name).options[IndexValue].value; }
            return xValue;
        }
        function GetDropDownText(Name) {
            var IndexValue = $get(Name).selectedIndex;
            var xValue = '';
            if (IndexValue > -1) { xText = $get(Name).options[IndexValue].text; }
            return xText;
        }
        function addOption(selectbox, value, text, SelectedValue) {
            var optn = document.createElement('OPTION');
            optn.text = text;
            optn.value = value;
            if (value == SelectedValue) { optn.setAttribute('selected', 'selected'); }
            selectbox.options.add(optn);
        }

    </script>

</asp:Content>