<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IFS_ReSKU.aspx.cs" Inherits="BW_WebApp.IFSLocationManagement.IFS_ReSKU" %>
<%@ Register Assembly="Syncfusion.Shared.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89" Namespace="Syncfusion.Web.UI.WebControls.Shared" TagPrefix="syncfusion" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="cMC" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            

            <asp:HiddenField ID="hdnUserName" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnCurrentIMEI" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnCarrierID" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnManufacturerID" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnModelID" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnColourID" runat="server" ClientIDMode="Static" />

            <asp:TabContainer CssClass="tab-container" runat="server">
                <asp:TabPanel CssClass="tab-panel" runat="server" HeaderText="Device Inventory Distribution (DID)">
                    <ContentTemplate>
                        <h1>Device SKU/Project Tag Change, Utility screen</h1>                        
                        <asp:Label ID="lblMessage" CssClass="d-block" runat="server" />
                        <div class="row">
                        	<div class="col-md">
                                <h3>From:</h3>
                                <asp:TabContainer CssClass="tab-container" runat="server">
                                    <asp:TabPanel ID="TabDIDMFromESN" CssClass="tab-panel" runat="server" HeaderText="ESN" ToolTip="ESN">
                                        <ContentTemplate>
                                            <h5>Select by ESN IMEI</h5>

                                            <label>ESN:</label>
                                            <div class="input-group">
                                                <asp:TextBox ID="txtESN" runat="server" MaxLength="20" ToolTip="ESN List Number." />
                                                <div class="input-group-append">
                                                    <asp:Button ID="btrRecord" runat="server" Text=">" />
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:TabPanel>
                                    <asp:TabPanel ID="TabDIDMFromBin" CssClass="tab-panel" runat="server" HeaderText="Bin" ToolTip="Bin">
                                        <ContentTemplate>
                                            <h5>Select by bin</h5>

                                            <label>BIN:</label>
                                            <div class="input-group">
                                                <asp:TextBox ID="lblBinNumber" runat="server" ToolTip="Bin Number for which the Location number is updated." />
                                                <div class="input-group-append">
                                                    <asp:Button ID="btnRecordBin" runat="server" Text=">" />
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:TabPanel>
                                    <asp:TabPanel ID="TabDIDMFromLocation" CssClass="tab-panel" runat="server" HeaderText="Location" ToolTip="IFS Location">
                                        <ContentTemplate>
                                            <h5>Select by IFS location</h5>

                                            <label>Location:</label>
                                            <div class="input-group">
                                                <asp:TextBox ID="IFSMoveFromLocationOnly" runat="server" ToolTip="IFS Location for which the Location number is updated." />
                                                <div class="input-group-append">
                                                    <asp:Button ID="btrRecordLocation" runat="server" Text=">" />
                                                </div>
                                            </div>s
                                        </ContentTemplate>
                                    </asp:TabPanel>
                                    <asp:TabPanel ID="TabDIDMFromSite" CssClass="tab-panel" runat="server" HeaderText="Site" ToolTip="Site">
                                        <ContentTemplate>
                                            <h5>Select by site</h5>

                                            <label>Site:</label>
                                            <div class="input-group">
                                                <asp:DropDownList ID="drpMoveIFSSite" runat="server" />
                                                <div class="input-group-append">
                                                    <asp:Button ID="btnRecordSite" runat="server" Text=">" />
                                                </div>
                                            </div>
                                    
                                            <label>Project:</label>
                                            <asp:DropDownList ID="drpMoveIFSProject" runat="server" />
                                     
                                            <label>SKU:</label>
                                            <asp:TextBox ID="txtFromSku" runat="server" ToolTip="IFS Sku" MaxLength="25" />
                                    
                                            <label>Location:</label>
                                            <asp:TextBox ID="txtFromLocation" runat="server" ToolTip="IFS Location" MaxLength="20" />
                                    
                                            <label>Condition:</label>
                                            <asp:DropDownList ID="drpMoveIFSCondition" runat="server" />
                                        </ContentTemplate>
                                    </asp:TabPanel>
                                    <asp:TabPanel ID="TabDIDMFromPaste" CssClass="tab-panel" runat="server" HeaderText="Paste" ToolTip="Paste">
                                        <ContentTemplate>
                                            <h5>Paste list</h5>

                                            <asp:RadioButtonList ID="PasteDeliminator" CssClass="radiolist-inline" runat="server" RepeatLayout="UnorderedList">
                                                <asp:ListItem Text="Excel" Value="Excel" Selected="True" />
                                                <asp:ListItem Text="Comma" Value="Comma" />
                                                <asp:ListItem Text="Space" Value="Space" />           
                                            </asp:RadioButtonList>
                                    
                                            <asp:TextBox ID="txtPasteParse" runat="server" TextMode="MultiLine" />
                                            <asp:Button ID="btnPasteParse" runat="server" Text=">" />
                                        </ContentTemplate>
                                    </asp:TabPanel>
                                </asp:TabContainer>
                            </div>

                            <div class="col-md">
                                <h3>List:</h3>
                                <div class="mb-2">
                                    <asp:LinkButton ID="imgbtnClear" CssClass="btn btn-default" runat="server" ToolTip="Clear Screen">
                                        <span class="oi oi-x"></span>
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="imgbtnDeleteIMIE" CssClass="btn btn-default" runat="server" ToolTip="Delete IMEI from List">
                                        <span class="oi oi-trash"></span>
                                    </asp:LinkButton>
                                </div>
                                <asp:TextBox ID="txtCount" runat="server" ClientIDMode="Static" Enabled="False" Text="0" ToolTip="Count" />
                                <asp:ListBox ID="lstHistory" runat="server" ToolTip="List of ESN/IMEI Numbers for which the Location number is updated." SelectionMode="Multiple" />
                            </div>

                            <div class="col-md">
                                <h3>To:</h3>
                                <asp:TabContainer CssClass="tab-container" runat="server">
                                    <asp:TabPanel ID="TabDIDMToLoc" CssClass="tab-panel" runat="server" HeaderText="To Loc" ToolTip="Set Device IFS Location">
                                        <ContentTemplate>
                                            <h5>Move Device to New Location</h5>

                                            <div class="input-group">
                                                <asp:TextBox ID="txtLocation" runat="server" MaxLength="20" ToolTip="IFS Location." />
                                                <div class="input-group-append">
                                                    <asp:Button ID="btnUpdateLocation" runat="server" Text="Go" UseSubmitBehavior="False" />
                                                </div>
                                            </div>

                                            <asp:Button ID="btnSearchRefresh" runat="server" Text="Suggest" />

                                            <asp:GridView ID="grdSearchSuggest" CssClass="table" runat="server" AutoGenerateColumns="False" >
                                                <Columns>
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnPick" runat="server" Text="Pick" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Freq" HeaderText="Freq" />
                                                    <asp:BoundField DataField="IFSLocation" HeaderText="Location" />
                                                    <asp:BoundField DataField="IFSSite" HeaderText="Site" />
                                                    <asp:BoundField DataField="IFSProject" HeaderText="Project" />
                                                    <asp:BoundField DataField="SKU" HeaderText="SKU" />
                                                    <asp:BoundField DataField="IFSCondition" HeaderText="Condition" />
                                                </Columns>
                                            </asp:GridView>
                                        </ContentTemplate>
                                    </asp:TabPanel>
                                    <asp:TabPanel ID="TabDIDMToSku" CssClass="tab-panel" runat="server" HeaderText="To SKU" ToolTip="Set Device IFS SKU">
                                        <ContentTemplate>
                                            <h5>Change Device SKU</h5>
                                            <div class="alert alert-warning">Note: Do NOT unlock or kit devices here!</div>

                                            <div>
                                                <asp:Button ID="btnToSKUPrior" runat="server" Text="<---"/>
                                                <asp:Button ID="btnSKUClear" runat="server" Text="Clear" />
                                                <asp:Button ID="btnToSKUNext" runat="server" Text="--->" />
                                            </div>

                                            <label>Carrier:</label>
                                            <asp:DropDownList ID="drpCarrier" runat="server" AutoPostBack="True" />
                                    
                                            <label>Manufacturer:</label>
                                            <asp:DropDownList ID="drpManufacturer" runat="server" AutoPostBack="True" />
                                        
                                            <label>Model:</label>
                                            <asp:DropDownList ID="drpModel" runat="server" AutoPostBack="True" />
                                    
                                            <label>Colour:</label>
                                            <asp:DropDownList ID="drpColour" runat="server" AutoPostBack="False" />
                                    
                                            <asp:Button ID="btnToSKU" runat="server" Text="Go" />
                                        </ContentTemplate>
                                    </asp:TabPanel>
                                    <asp:TabPanel ID="TabDIDMToCond" CssClass="tab-panel" runat="server" HeaderText="To Cond" ToolTip="Set Device IFS Condition">
                                        <ContentTemplate>
                                            <h5>Change Device Condition</h5>

                                            <div class="input-group">
                                                <asp:DropDownList ID="drpChangeToCondition" runat="server" />
                                                <div class="input-group-append">
                                                    <asp:Button ID="btnTOCondition" runat="server" Text="Go" />
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:TabPanel>
                                    <asp:TabPanel ID="TabToProjectTag" CssClass="tab-panel" runat="server" HeaderText="To Project Tag" ToolTip="Set Device Project Tag">
                                        <ContentTemplate>
                                            <h5>Change Device Project Tag</h5>

                                            <label>To Project Tag:</label>
                                            <div class="input-group">
                                                <asp:TextBox ID="txtTOProjectTag" runat="server" />
                                                <div class="input-group-append">
                                                    <asp:Button ID="btnTOProjectTag" runat="server" Text="Go" />
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:TabPanel>
                                    <asp:TabPanel ID="TabUnShipDevices" CssClass="tab-panel" runat="server" HeaderText="Unship" ToolTip="Unship devices">
                                        <ContentTemplate>
                                            <h5>Change Device Project Tag</h5>

                                            <asp:Button ID="btnUnShip" runat="server" Text="Go" />

                                            <p class="text-muted">This utility will look for an IMEI version 001 and reset that varsion back to 000.
                                            It will also remove data from Shipto, PSlip and Out-Bound Waybill-S attribute.</p>
                                        </ContentTemplate>
                                    </asp:TabPanel>
                                </asp:TabContainer>
                            </div>
                        </div>

                        <asp:Label ID="lblMessageBTM" CssClass="d-block" runat="server" />

                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
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

        function RecordHistory(Value) {
            var Source = $get("<%= lstHistory.ClientID %>");
            // Check to see if the item is already there.
            var xc = Source.getElementsByTagName('option').length;
            for (var i = 0; i < xc; i++) {
                if (Source.options[i].value == Value)
                    return;
            }
            if (Source != null) {
                var newOption = new Option(); // Create a new instance of ListItem
                newOption.text = Value;
                Source.options[Source.length] = newOption; //Append the item in Target
            }
        }

        function SetESNFocus() {
            SetFocus("<%= txtESN.ClientID %>");
            return;
        }

        function SetFocus(Name) {
            $get(Name).focus();
            return;
        }


        function OpenFinishProductLabel() {
            var WindowToOpen = 'FinishProductLabel_Bulk.aspx';
            var win = window.open(WindowToOpen, '_blank', 'menubar', true);
        }

        // -----------------------------------------------------------------

        function PrintReport() {
            var xBatch = Batch();
            if (xBatch.length == 0) {
                alert('You must supply a batch');
                return;
            }
            var xDataList = {};
            xDataList["RPT"] = "PHYSICALCOUNT";
            xDataList["KEY"] = xBatch;
            xDataList["USERNAME"] = UserName();
            var pstring = GetParameterStream(xDataList);
            // var WindowToOpen = "RPT_SpotCountReport.aspx";
            var WindowToOpen = "/Reports/RPT_EXCEL_Out.aspx";
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

        //-----------------------------------------------------------------------------------------
        //       function RecordScanAuto() {
        //           if (AutoReturn() == false) { return; }
        //           RecordScan();
        //       }


        //       function RecordScan() {
        //           var xESN = ESN();
        //           if (xESN.length == 0) { return; }
        //           var xBatch = Batch();
        //           var xUserName = UserName();

        //           if (xBatch.length == 0) {
        //               var service = new WebServer_01();
        //               service.GetNewPhysicalDeviceBatch(xUserName, onSuccessNewBatch, onServerError);
        //           } else { SaveScanData(); }
        //       }

        //       function onSuccessNewBatch(result) {
        //           SetBatchNumber(result);
        //           SaveScanData();
        //       }

        //       function SaveScanData() {
        //           var xESN = ESN();
        //           var xBatch = Batch();
        //           if (xESN.length == 0) { return; }
        //           var xUserName = UserName();

        //           var xLocationID = LocationID().toString(); ;
        //           var xIFSConditionID = ConditionID().toString(); ;
        //           var xIFSSite = Site();
        //           var xIFSProject = Project();
        //           var xIFSLocation = Location();
        //           var xIFSCondition = Condition();
        //           //           var xGrade = Grade();
        //           var xUpdateIMEI = UpdateIMEI();
        //           var sUpdateIMEI = '0';
        //           if (xUpdateIMEI == true) { sUpdateIMEI = '1'; }

        //           var xKitted = Kitted();
        //           var sKitted = '0';
        //           if (xKitted == true) { sKitted = '1'; }
        //           var xUnlocked = Unlocked();
        //           var sUnlocked = '0';
        //           if (xUnlocked == true) { sUnlocked = '1'; }
        //           var service = new WebServer_01();
        //           //           alert('Going In' + POReceiptDatex);
        //           service.LogPhysicalDeviceCount(xLocationID
        //                             , xIFSConditionID
        //                             , xESN
        //                             , xBatch
        //                             , xIFSSite
        //                             , xIFSProject
        //                             , ''
        //                             , xIFSLocation
        //                             , xIFSCondition
        //                             , ''
        //                             , sKitted
        //                             , sUnlocked
        //                             , sUpdateIMEI
        //                             , xUserName, onSuccessDeviceRecorded, onServerError);
        //       }

        //       function onSuccessDeviceRecorded(result) {
        //           $get("< %= lblPIMessage.ClientID %>").innerHTML = result;
        //           if (result.substring(0, 6) != 'Error:') {
        //               var c = Number(IMEICount());
        //               var d = c + 1;
        //               $get("< %= txtPMCount.ClientID %>").value = d;
        //           }
        //           else {
        //               var c = Number(IMEIErrorCount());
        //               var d = c + 1;
        //               $get("< %= ScanKey.ClientID %>").value = '';
        //               alert(result);
        //               $get("< %= txtPMCountError.ClientID %>").value = d;
        //           }
        //           $get("< %= ScanKey.ClientID %>").value = '';
        //           $get("< %= ScanKey.ClientID %>").focus()
        //           return;
        //       }

        //       function onServerError(Result) {
        //           alert('Error:' + Result.get_message());
        //       }

        // Variable Functions ----------------------------------
        function UserName() {
            return $get("<%= hdnUserName.ClientID %>").value;
        }

        //       function hdnBatchNumber() {
        //           return $get("< %= hdnBatchNumber.ClientID %>").value;
        //       }

        //       function SetBatchNumber(Value) {
        //           $get("< %= hdnBatchNumber.ClientID %>").value = Value;
        //           $get("< %= txtBatch.ClientID %>").value = Value;
        //       }       


        //       function ESN() {
        //           return $get("< %= ScanKey.ClientID %>").value;
        //       }
        //       function ScanKey() {
        //           var value = $get("< %= ScanKey.ClientID %>").value;
        //           return value;
        //       }
        //       function IMEICount() {
        //           var value = $get("< %= txtPMCount.ClientID %>").value;
        //           return value;
        //       }
        //       function IMEIErrorCount() {
        //           var value = $get("< %= txtPMCountError.ClientID %>").value;
        //           return value;
        //       }

        //       function Batch() {
        //           var value = $get("< %= txtBatch.ClientID %>").value;
        //           return value;
        //       }

        //       function Site() {
        //           var IndexValue = $get("< %= drpIFSSite.ClientID %>").selectedIndex;
        //           var value = $get("< %= drpIFSSite.ClientID %>").options[IndexValue].text;
        //           return value;
        //       }
        //       function Project() {
        //           var IndexValue = $get("< %= drpIFSProject.ClientID %>").selectedIndex;
        //           var value = $get("< %= drpIFSProject.ClientID %>").options[IndexValue].value;
        //           return value;
        //       }

        //       function Location() {
        //           var value = $get("< %= txtIFSLocation.ClientID %>").value;
        //           return value;
        //       }

        //       function LocationID() {
        //           return -1;
        //       }

        //       function Condition() {
        //           var IndexValue = $get("< %= drpIFSCondition.ClientID %>").selectedIndex;
        //           var value = $get("< %= drpIFSCondition.ClientID %>").options[IndexValue].text;
        //           return value;
        //       }
        //       function ConditionID() {
        //           var IndexValue = $get("< %= drpIFSCondition.ClientID %>").selectedIndex;
        //           var value = $get("< %= drpIFSCondition.ClientID %>").options[IndexValue].value;
        //           return value;
        //       }
        ////       function Grade() {
        ////           var IndexValue = $get("< %= drpGrade.ClientID %>").selectedIndex;
        ////           var value = $get("< %= drpGrade.ClientID %>").options[IndexValue].text;
        ////           return value;
        ////       }
        ////       

        //       function UpdateIMEI() {
        //           return $get("< %= chkUpdateIMEI.ClientID %>").checked;
        //       }
        //       function AutoReturn() {
        //           return $get("< %= chkAutoReturn.ClientID %>").checked;
        //       }


        //       function Kitted() {
        //           return $get("< %= chkKitted.ClientID %>").checked;
        //       }
        //       function Unlocked() {
        //           return $get("< %= chkUnlocked.ClientID %>").checked;
        //       }


        //       function beep() {
        //           var snd = new Audio("data:audio/wav;base64,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qJrx4KOQjahZxWbcZgztj2c49nKmkId44S71j0c8eV9yDK6uPRzx5X18eDvjvQ6yKo9ZSS6l//8elePK/Lf//IInrOF/FvDoADYAGBMGb7FtErm5MXMlmPAJQVgWta7Zx2go+8xJ0UiCb8LHHdftWyLJE0QIAIsI+UbXu67dZMjmgDGCGl1H+vpF4NSDckSIkk7Vd+sxEhBQMRU8j/12UIRhzSaUdQ+rQU5kGeFxm+hb1oh6pWWmv3uvmReDl0UnvtapVaIzo1jZbf/pD6ElLqSX+rUmOQNpJFa/r+sa4e/pBlAABoAAAAA3CUgShLdGIxsY7AUABPRrgCABdDuQ5GC7DqPQCgbbJUAoRSUj+NIEig0YfyWUho1VBBBA//uQZB4ABZx5zfMakeAAAAmwAAAAF5F3P0w9GtAAACfAAAAAwLhMDmAYWMgVEG1U0FIGCBgXBXAtfMH10000EEEEEECUBYln03TTTdNBDZopopYvrTTdNa325mImNg3TTPV9q3pmY0xoO6bv3r00y+IDGid/9aaaZTGMuj9mpu9Mpio1dXrr5HERTZSmqU36A3CumzN/9Robv/Xx4v9ijkSRSNLQhAWumap82WRSBUqXStV/YcS+XVLnSS+WLDroqArFkMEsAS+eWmrUzrO0oEmE40RlMZ5+ODIkAyKAGUwZ3mVKmcamcJnMW26MRPgUw6j+LkhyHGVGYjSUUKNpuJUQoOIAyDvEyG8S5yfK6dhZc0Tx1KI/gviKL6qvvFs1+bWtaz58uUNnryq6kt5RzOCkPWlVqVX2a/EEBUdU1KrXLf40GoiiFXK///qpoiDXrOgqDR38JB0bw7SoL+ZB9o1RCkQjQ2CBYZKd/+VJxZRRZlqSkKiws0WFxUyCwsKiMy7hUVFhIaCrNQsKkTIsLivwKKigsj8XYlwt/WKi2N4d//uQRCSAAjURNIHpMZBGYiaQPSYyAAABLAAAAAAAACWAAAAApUF/Mg+0aohSIRobBAsMlO//Kk4soosy1JSFRYWaLC4qZBYWFRGZdwqKiwkNBVmoWFSJkWFxX4FFRQWR+LsS4W/rFRb/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////VEFHAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAU291bmRib3kuZGUAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAMjAwNGh0dHA6Ly93d3cuc291bmRib3kuZGUAAAAAAAAAACU=");
        //           snd.play();
        //       }


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
