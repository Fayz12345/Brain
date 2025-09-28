<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Maint_MasterCarrer_ColourUpdate.aspx.cs" Inherits="BW_WebApp.Maintenance.Maint_MasterCarrer_ColourUpdate" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Syncfusion.Shared.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89" Namespace="Syncfusion.Web.UI.WebControls.Shared" TagPrefix="syncfusion" %>
<%@ Register Assembly="Syncfusion.Tools.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89"  Namespace="Syncfusion.Web.UI.WebControls.Tools" TagPrefix="syncfusion" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="cMC" runat="server">

    

    <asp:HiddenField ID="hdnKeys" runat="server" />

    <asp:HiddenField ID="hdnLastTreeSelectKeys" runat="server" />
    <asp:HiddenField ID="hdnUserName" runat="server" ClientIDMode="Static" />

    <h1><asp:Label ID="lblRecordTitle" runat="server" Text="Model Table - Update Stock Colour Code" /></h1>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="row">
            	<div class="col-md-8">

                    <div class="card mb-3">
                        <syncfusion:TreeView ID="TreeData" CssClass="tree-view"  runat="server" OnNodeExpanded="TreeView1_NodeExpanded"
                            ClientSideOnContextMenu="NodeOnContextMenu(this)" EditNode="False" ClientSideOnNodeSelect="NodeOnSelect(this)" />
                    </div>

                    <hr class="d-md-none">

                </div>

            	<div class="col-md-4">

                    <h4><asp:Label runat="server" Text="Update Stock Colour Code" /></h4>

                    <label>Manufacturer:</label>
                    <asp:DropDownList ID="drpManufacturer" runat="server" />

                    <label>Model:</label>
                    <asp:DropDownList ID="drpModel" runat="server" />

                    <label>New Model:</label>
                    <asp:TextBox ID="txtNewModel" runat="server" ToolTip="Add a new model to the model table. Enter the new model here." />

                    <label>Stock Colour Code:</label>
                    <asp:DropDownList ID="drpEditStockColourCode" runat="server" />

                    <asp:Button ID="btnSave" runat="server" Text="Save" />
                    <asp:Label ID="lblMessage" runat="server" />
                </div>

            </div>

            <%--<syncfusion:Splitter ID="Splitter1" runat="server" Height="500px" Width="100%">
                <syncfusion:SplitPane ID="SplitPane1" runat="server" Height="100%" Width="50%">
                    <syncfusion:TreeView ID="TreeData" runat="server" Font-Names="Trebuchet MS" Font-Size="15px"
                        OnNodeExpanded="TreeView1_NodeExpanded" CssClass="TreeView " CustomCSS="Styles/TreeStyle.css"  ClientSideOnContextMenu="NodeOnContextMenu(this)"
                        EditNode="False" AutoFormat="Office2007 Blue" Width="100%" Height="100%" ClientSideOnNodeSelect="NodeOnSelect(this)">
                        <DefaultItemLookDisabled>
                            <StateDataDefault LeftImageCellCSSClass="tvImgCell" ItemRowCSSClass="tvItemRow" LeftImageCSSClass="tvImg"
                                ItemCSSClass="tvItemDisabled" CheckBoxCellCSSClass="tvCheckCell" RightImageCSSClass="tvArr"
                                TextCellCSSClass="tvTextCell" CheckBoxCSSClass="tvCheck" RightImageContainerCSSClass="tvArrCont"
                                RightImageCellCSSClass="tvArrCell" LeftImageContainerCSSClass="tvImgCont" TextContainerCSSClass="Def_TextContCss">
                            </StateDataDefault>
                        </DefaultItemLookDisabled>
                        <DefaultItemLook>
                            <StateDataExpanded LeftImageCellCSSClass="tvImgCell" ItemRowCSSClass="tvItemRow"
                                LeftImageCSSClass="tvImg" ItemCSSClass="tvItem" CheckBoxCellCSSClass="tvCheckCell"
                                RightImageCSSClass="tvArr" TextCellCSSClass="tvTextCell" CheckBoxCSSClass="tvCheck"
                                RightImageContainerCSSClass="tvArrCont" RightImageCellCSSClass="tvArrCell" LeftImageContainerCSSClass="tvImgCont"
                                TextContainerCSSClass="Def_TextContCss"></StateDataExpanded>
                            <StateDataActive LeftImageCellCSSClass="tvImgCell" ItemRowCSSClass="tvItemRow" LeftImageCSSClass="tvImg"
                                ItemCSSClass="tvItem" CheckBoxCellCSSClass="tvCheckCell" RightImageCSSClass="tvArr"
                                TextCellCSSClass="tvTextCell" CheckBoxCSSClass="tvCheck" RightImageContainerCSSClass="tvArrCont"
                                RightImageCellCSSClass="tvArrCell" LeftImageContainerCSSClass="tvImgCont" TextContainerCSSClass="Act_TextContCss">
                            </StateDataActive>
                            <StateDataHover LeftImageCellCSSClass="tvImgCell" ItemRowCSSClass="tvItemRow" LeftImageCSSClass="tvImg"
                                ItemCSSClass="tvItem" CheckBoxCellCSSClass="tvCheckCell" RightImageCSSClass="tvArr"
                                TextCellCSSClass="tvTextCell" CheckBoxCSSClass="tvCheck" RightImageContainerCSSClass="tvArrCont"
                                RightImageCellCSSClass="tvArrCell" LeftImageContainerCSSClass="tvImgCont" TextContainerCSSClass="Hov_TextContCss">
                            </StateDataHover>
                            <StateDataDefault LeftImageCellCSSClass="tvImgCell" ItemRowCSSClass="tvItemRow" LeftImageCSSClass="tvImg"
                                ItemCSSClass="tvItem" CheckBoxCellCSSClass="tvCheckCell" RightImageCSSClass="tvArr"
                                TextCellCSSClass="tvTextCell" CheckBoxCSSClass="tvCheck" RightImageContainerCSSClass="tvArrCont"
                                RightImageCellCSSClass="tvArrCell" LeftImageContainerCSSClass="tvImgCont" TextContainerCSSClass="Def_TextContCss">
                            </StateDataDefault>
                        </DefaultItemLook>
                        <Items>
                        </Items>
                    </syncfusion:TreeView>
                </syncfusion:SplitPane>

                <syncfusion:SplitterBar ID="SplitterBar1" runat="server" CollapseMode="both" />

                <syncfusion:SplitPane ID="SplitPane2" runat="server" Height="100%" Width="50%" ScrollMode="None">
                    <asp:Panel ID="Panel1" runat="server" Width="100%" Height="100%">
                        <table width="100%">
                            <tr>
                                <td colspan="3" align="center">
                                    <asp:Label ID="Label1" runat="server" Text="Update Stock Colour Code" Font-Size="Large"></asp:Label>
                                    <br />
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Manufacturer:
                                </td>
                                <td>
                                    <asp:DropDownList ID="drpManufacturer" runat="server" BackColor="#FFFF66">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:Label ID="Label2" runat="server" Width="100%" Text="New Model"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Model:
                                </td>
                                <td>
                                    <asp:DropDownList ID="drpModel" runat="server" BackColor="#FFFF66">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNewModel" runat="server" ToolTip="Add a new Model to the Model Table. Enter the new model here."></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Stock Colour Code:
                                </td>
                                <td colspan="2">
                                    <asp:DropDownList ID="drpEditStockColourCode" runat="server" BackColor="#FFFF66">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td colspan="2">
                                    <br />
                                    <asp:Button ID="btnSave" runat="server" Text="Save" Width="100%" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" align="center">
                                    <asp:Label ID="lblMessage" runat="server" Width="100%" Text=""></asp:Label>
                                    <br />
                                    <br />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                       
                </syncfusion:SplitPane>
            </syncfusion:Splitter>--%>

        </ContentTemplate>
    </asp:UpdatePanel>
    
</asp:Content>

<asp:Content ContentPlaceHolderID="js" runat="server">
    <script type="text/javascript">

        //        function PrintReport() {
        //            var xDataList = {};
        //            xDataList["RPT"] = "AVSTOCK";
        //            xDataList["KEY"] = MCL('TREEKEYS').value;
        //            xDataList["USERNAME"] = MCL('USERNAME').value;
        //            var pstring = GetParameterStream(xDataList);
        //            // var WindowToOpen = "RPT_SpotCountReport.aspx";
        //            var WindowToOpen = "RPT_EXCEL_Out.aspx";
        //            if (pstring.length > 0) {
        //                WindowToOpen = WindowToOpen + "?" + pstring
        //            }
        //            var win = window.open(WindowToOpen, "_blank", "menubar", true);
        //            return;
        //        }


        //        function showScreen(show) {
        //            var Header = MCL('pnlHeader');
        //            var Detail = MCL('TreeData');
        //            if (Header == null || Detail == null) { return; }
        //            if (show == "Header") {
        //                ControlShow(Header);
        //                ControlHide(Detail);
        //            }
        //            else {
        //                ControlShow(Detail);
        //                ControlHide(Header);
        //            }
        //        }

        function ControlShow(cntrl) {
            cntrl.style.display = '';
        }
        function ControlHide(cntrl) {
            cntrl.style.display = 'none';
        }

        function ResetHistory() {
//            MCL('HDNKEYS').value = "";
//            MCL('TARGETADDRESS').value = "";
//            MCL('TXTBILLCLIENT').value = "";
//            //MCL('BTNBILLCLIENTSEARCH').value = "";
//            MCL('TXTSHIPCLIENT').value = "";

//            MCL('txtCustomerPONumber').value = "";
//            MCL('txtProjectTag').value = "";
//            MCL('txtInternalNote').value = "";
//            MCL('txtOrderNumber').value = "New";
//            MCL('txtWaybillNumber').value = "";
//            MCL('txtShipNameAddresstext').value = "";
//            MCL('txtBillNameAddresstext').value = "";

//            MCL('hdnBillClientLocationID').value = "";
//            MCL('hdnBillCompanyName').value = "";
//            MCL('hdnBillContactName').value = "";
//            MCL('hdnBillAddressLine1').value = "";
//            MCL('hdnBillAddressLine2').value = "";
//            MCL('hdnBillCity').value = "";
//            MCL('hdnBillStateOrProvince').value = "";
//            MCL('hdnBillPostalCode').value = "";
//            MCL('hdnBillPhoneNumber').value = "";
//            MCL('hdnBillFaxNumber').value = "";
//            MCL('hdnBillNotes').value = "";

//            MCL('hdnShipClientLocationID').value = "";
//            MCL('hdnShipCompanyName').value = "";
//            MCL('hdnShipContactName').value = "";
//            MCL('hdnShipAddressLine1').value = "";
//            MCL('hdnShipAddressLine2').value = "";
//            MCL('hdnShipCity').value = "";
//            MCL('hdnShipStateOrProvince').value = "";
//            MCL('hdnShipPostalCode').value = "";
//            MCL('hdnShipPhoneNumber').value = "";
//            MCL('hdnShipFaxNumber').value = "";
//            MCL('hdnShipNotes').value = "";

//            if (MCL('lstHistory') == null) { return; }
//            var Source = MCL('lstHistory');
//            var Count = MCL('txtHistoryCount');
//            Count.value = '0';
//            if (Source != null) {
//                var xc = Source.getElementsByTagName('option').length;
//                for (var i = 0; i < xc; i++) {
//                    Source.remove(0);
//                    var count = Count.value;
//                    Count.value = count.toString();
//                    count--
//                }
//            }

//            showScreen("Detail");
//            ControlShow(MCL('btnDetail'));
//            ControlHide(MCL('btnSaveSale'));
//            UpdateBackEndData();
        }

//        function DeleteHistory() {
//            if (MCL('lstHistory') == null) { return; }
//            var Source = MCL('lstHistory');
//            var Count = MCL('txtHistoryCount');
//            if (Source != null) {
//                if (Source.options.selectedIndex >= 0) {
//                    var Count = MCL('txtHistoryCount');
//                    var count = Number(Count.value);

//                    var AdjustValue = Source.options[Source.options.selectedIndex].value
//                    var b = AdjustValue.split(':')
//                    count -= Number(b[0]);


//                    Count.value = count.toString();
//                    Source.remove(Source.options.selectedIndex);
//                }
//            }
//            UpdateBackEndData();
//        }

//        function RecordHistory(Value, qty, key) {
//            if (MCL('lstHistory') == null) { return; }
//            var Source = MCL('lstHistory');
//            // Check to see if the item is already there.
//            var xc = Source.getElementsByTagName('option').length;

//            if (Source != null) {
//                var newOption = new Option();
//                newOption.text = Value;
//                newOption.value = qty.toString() + "," + key;
//                Source.options[Source.length] = newOption;

//                var Count = MCL('txtHistoryCount');
//                var count = Number(Count.value);
//                count += Number(qty);
//                Count.value = count.toString();
//                ControlShow(MCL('btnSaveSale'));
//            }
//            UpdateBackEndData();
//        }

//        function SellButton_Click(o, value) {
//            // hobble = o.previousElementSibling;
//            var qty = prompt("Quantity to Pick", "1")
//            qty = Number(qty);
//            if (isNaN(qty) == true) { alert('You must enter a quantity.'); return; }
//            if (qty < 1) { alert('Quanity must be greater than 0.'); return; }
//            RecordHistory(qty + '/' + value, qty, value);
//            qty.value = '';
//            //            var PopUpPannel = MCL('GradeMenu');
//            //            ControlHide(PopUpPannel);
//        }

        function NodeOnContextMenu(o) {
            //            var PopUpPannel = MCL('GradeMenu');
            //            ControlShow(PopUpPannel);
            //            PopUpPannel.style.position = 'absolute';
            //            PopUpPannel.style.top = o.Event.clientY + 'px';
            //            PopUpPannel.style.left = o.Event.clientX + 'px';
            //OpenClientSearch('Bill');
            //          var PopupContainer1 = MCL('PopupControlContainer1');
            //          PopupContainer1.ShowPopup(10, 10, true);
            if (o.Value.length > 0) { SellButton_Click(o, o.Value); }
            return false;
        }

        function NodeOnSelect(o) {
            MCL('TREEKEYS').value = o.Value;
        }

        function MCL(ControlName) {
            switch (ControlName.toUpperCase()) {
                case "HDNLSTHISTORY": return $get("< %= hdnLstHistory.ClientID %>"); break;
//                case "HDNLISTHISTORYVALUE": return $get("< %= hdnListHistoryValue.ClientID %>"); break;  
//                case "HDNTXTHISTORYCOUNT": return $get("< %= hdntxtHistoryCount.ClientID %>"); break;  

                case "MANUFACTURER": return $get("<%= drpManufacturer.ClientID %>"); break;
                case "MODEL": return $get("<%= drpModel.ClientID %>"); break;
                case "DRPEDITSTOCKCOLOURCODE": return $get("<%= drpEditStockColourCode.ClientID %>"); break;
                case "HDNKEYS": return $get("<%= hdnKeys.ClientID %>"); break;
                case "PNLHEADER": return $get("< %= pnlHeader.ClientID %>"); break;
                case "TREEDATA": return $get("<%= TreeData.ClientID %>"); break;

                case "USERNAME": return $get("<%= hdnUserName.ClientID %>"); break;
                case "BTNSAVE": return $get("<%= btnSave.ClientID %>"); break;

//                case "TXTBILLCLIENT": return $get("< %= txtBillClient.ClientID %>"); break;  
//                case "BTNBILLCLIENTSEARCH": return $get("< %= btnBillClientSearch.ClientID %>"); break;  
//                case "TXTSHIPCLIENT": return $get("< %= txtShipClient.ClientID %>"); break;  
//                case "BTNSHIPCLIENTSEARCH": return $get("< %= btnShipClientSearch.ClientID %>"); break;  

//                case "TXTCUSTOMERPONUMBER": return $get("< %= txtCustomerPONumber.ClientID %>"); break;  
//                case "TXTPROJECTTAG": return $get("< %= txtProjectTag.ClientID %>"); break;  
//                case "TXTINTERNALNOTE": return $get("< %= txtInternalNote.ClientID %>"); break;  
//                case "TXTORDERNUMBER": return $get("< %= txtOrderNumber.ClientID %>"); break;  
//                case "TXTWAYBILLNUMBER": return $get("< %= txtWaybillNumber.ClientID %>"); break;  

//                case "HDNBILLCLIENTLOCATIONID": return $get("< %= hdnBillClientLocationID.ClientID %>"); break;  
//                case "HDNBILLCOMPANYNAME": return $get("< %= hdnBillCompanyName.ClientID %>"); break;  
//                case "HDNBILLCONTACTNAME": return $get("< %= hdnBillContactName.ClientID %>"); break;  
//                case "HDNBILLADDRESSLINE1": return $get("< %= hdnBillAddressLine1.ClientID %>"); break;  
//                case "HDNBILLADDRESSLINE2": return $get("< %= hdnBillAddressLine2.ClientID %>"); break;  
//                case "HDNBILLCITY": return $get("< %= hdnBillCity.ClientID %>"); break;  
//                case "HDNBILLSTATEORPROVINCE": return $get("< %= hdnBillStateOrProvince.ClientID %>"); break;  
//                case "HDNBILLPOSTALCODE": return $get("< %= hdnBillPostalCode.ClientID %>"); break;  
//                case "HDNBILLPHONENUMBER": return $get("< %= hdnBillPhoneNumber.ClientID %>"); break;  
//                case "HDNBILLFAXNUMBER": return $get("< %= hdnBillFaxNumber.ClientID %>"); break;  
//                case "HDNBILLNOTES": return $get("< %= hdnBillNotes.ClientID %>"); break;  

//                case "HDNSHIPCLIENTLOCATIONID": return $get("< %= hdnShipClientLocationID.ClientID %>"); break;  
//                case "HDNSHIPCOMPANYNAME": return $get("< %= hdnShipCompanyName.ClientID %>"); break;  
//                case "HDNSHIPCONTACTNAME": return $get("< %= hdnShipContactName.ClientID %>"); break;  
//                case "HDNSHIPADDRESSLINE1": return $get("< %= hdnShipAddressLine1.ClientID %>"); break;  
//                case "HDNSHIPADDRESSLINE2": return $get("< %= hdnShipAddressLine2.ClientID %>"); break;  
//                case "HDNSHIPCITY": return $get("< %= hdnShipCity.ClientID %>"); break;  
//                case "HDNSHIPSTATEORPROVINCE": return $get("< %= hdnShipStateOrProvince.ClientID %>"); break;  
//                case "HDNSHIPPOSTALCODE": return $get("< %= hdnShipPostalCode.ClientID %>"); break;  
//                case "HDNSHIPPHONENUMBER": return $get("< %= hdnShipPhoneNumber.ClientID %>"); break;  
//                case "HDNSHIPFAXNUMBER": return $get("< %= hdnShipFaxNumber.ClientID %>"); break;  
//                case "HDNSHIPNOTES": return $get("< %= hdnShipNotes.ClientID %>"); break;  


//                case "TXTSHIPNAMEADDRESSTEXT": return $get("< %= txtShipNameAddresstext.ClientID %>"); break;  
//                case "TXTBILLNAMEADDRESSTEXT": return $get("< %= txtBillNameAddresstext.ClientID %>"); break;  

//                case "BTNSAVESALE": return $get("< %= btnSaveSale.ClientID %>"); break;  
//                case "BTNHEADER": return $get("< %= btnHeader.ClientID %>"); break;  
//                case "BTNDETAIL": return $get("< %= btnDetail.ClientID %>"); break;  
                case "HDNLASTTREESELECTKEYS": return $get("<%= hdnLastTreeSelectKeys.ClientID %>"); break;
                case "TREEKEYS": return $get("<%= hdnLastTreeSelectKeys.ClientID %>"); break;



                default: return null;
            }
        }


        function RestoreBackEndData() {
            MCL('txtHistoryCount').value = MCL('HDNTXTHISTORYCOUNT').value;

            var Value = MCL('HDNLISTHISTORYVALUE').value.split("/");
            var Text = MCL('HDNLSTHISTORY').value.split("|");

            if (MCL('lstHistory') == null) { return; }
            var Source = MCL('lstHistory');

            // Clear the list... Incase any stayed behind
            if (Source != null) {
                var xc = Source.getElementsByTagName('option').length;
                for (var i = 0; i < xc; i++) {
                    Source.remove(0);
                }
            }

            for (i = 0; i < Text.length; i++) {
                if (Text[i].length > 0) {
                    var newOption = new Option();
                    newOption.text = Text[i];
                    newOption.value = Value[i];
                    Source.options[Source.length] = newOption;
                }
            }
        }

        function UpdateBackEndData() {
            var dataValue = "";
            var dataText = "";
            if (MCL('lstHistory') == null) { return; }
            var Source = MCL('lstHistory');
            if (Source != null) {
                for (var n = 0; n < Source.options.length; n++) {
                    dataValue += Source.options[n].value + "/";
                    dataText += Source.options[n].text + "|";       // The back slash is used inside the original string between the qty and the text string.
                }
            }
            MCL('hdnKeys').value = dataValue;
            MCL('HDNLISTHISTORYVALUE').value = dataValue;
            MCL('HDNLSTHISTORY').value = dataText;
            MCL('HDNTXTHISTORYCOUNT').value = MCL('txtHistoryCount').value;
        }

        function SaveButtonClick(o) {
////            var Key = MCL('txtHobbleName');
////            if (Key.value.length == 0 || Key.value.toUpperCase() == "ORDER KEY") {
////                alert('You must enter an ORDER KEY before saving!');
////                return false;
////            }
//            var data = "";
//            if (MCL('lstHistory') == null) { return; }
//            var Source = MCL('lstHistory');
//            if (Source != null) {
//                for (var n = 0; n < Source.options.length; n++) {
//                    data += Source.options[n].value + "/";
//                }
//            }
//            MCL('hdnKeys').value = data;
//            return true;
        }

//        ////////////////////////////////////////////////////////////

//        // *****************************************************************************
////        function OpenClientSearch(Address) {
////            MCL("TargetAddress").value = Address;
////            $find('< %=this.wndSelectClientLocation.ClientID%>').Title = "Client Search";
////            $find('< %=this.wndSelectClientLocation.ClientID%>').Open(null, null);
////        }

////        function selx(ID) {
////            $find('< %=wndSelectClientLocation.ClientID%>').Close();
////            if (MCL("TargetAddress").value == "Bill") {
////                MCL("txtBillClient").value = ID;
////                MCL("btnBillClientSearch").click();
////            }
////            if (MCL("TargetAddress").value == "Ship") {
////                MCL("txtShipClient").value = ID;
////                MCL("btnShipClientSearch").click();
////            }

////            // LoadClientLocation(ID);
////        }

//        function SearchClient() {
//            //            var SearchClientName = $get("< %= txtsClientName.ClientID %>").value;
//            //            var SearchLocationName = $get("< %= txtsLocationName.ClientID %>").value;
//            //            var SearchStreet = $get("< %= txtsStreet.ClientID %>").value;
//            //            var SearchPostalCode = $get("< %= txtsPostalCode.ClientID %>").value;
//            //            var service = new WebServer_01();
//            //            var rValue = service.GetSearchClientLocationData(MCL("UserName").value, SearchClientName, SearchLocationName, SearchStreet, SearchPostalCode, onSearchClientSuccess, onWebServerError);


//            var SearchClientName = $get("< %= txtsClientName.ClientID %>").value;
//            var SearchLocationName = $get("< %= txtsLocationName.ClientID %>").value;

//            var SearchStreet = $get("< %= txtsStreet.ClientID %>").value;
//            var SearchPostalCode = $get("< %= txtsPostalCode.ClientID %>").value;
//            var service = new WebServer_01();
//            var rValue = service.GetSearchClientLocationData(MCL("UserName").value, SearchClientName, SearchLocationName, SearchStreet, SearchPostalCode, onSearchClientSuccess, onWebServerError);

//        }

//        function onWebServerError(Result) {
//            alert("Error:" + Result.get_message());
//        }

//        function onSearchClientSuccess(Result) {
//            var OutputHTML = "";
//            var HeaderText = "<tr><td>Select</td> <td>ID</td>   <td>Client</td>  <td>Location Name</td>    <td>Location</td></tr>";
//            var BodyText = "";

//            //           ClientData = eval('({' + Result + '})');
//            ClientData = eval('[' + Result + ']');               // Square brackets to denote an array of elements.
//            var Quote = "'";
//            for (var i = 0; i < ClientData.length; i++) {
//                BodyText = BodyText + "<tr><td>"
//                               + '<button id="btn" name="btn" onClick="selx(' + Quote
//                               + ClientData[i].ScanKey + Quote
//                               + '); return false;">Select</button>'
//                               + "</td> <td>"
//                               + ClientData[i].ClientLocationID
//                               + "</td> <td>"
//                               + ClientData[i].txtClientName
//                               + "</td>   <td>"

//                               + ClientData[i].txtLocationName
//                               + "</td>   <td>"

//                               + ClientData[i].txtStoreNumber + " " + ClientData[i].txtStoreSuffix + " " + ClientData[i].txtClientAddress
//                               + "</td></tr>";
//            }
//            OutputHTML = "<table id='XX'>" + HeaderText + BodyText + "</table>"
//            var SearchResults = $get("< %= pnlSearchResult.ClientID %>");
//            SearchResults.innerHTML = OutputHTML;
//        }

    </script>
</asp:Content>



