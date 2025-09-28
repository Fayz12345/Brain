<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReceiveDetailTransfer.aspx.cs" Inherits="BW_WebApp.ReceiveDetailTransfer" %>

<%@ Register Assembly="Syncfusion.Shared.Web, Version=10.404.0.53, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89" Namespace="Syncfusion.Web.UI.WebControls.Shared" TagPrefix="syncfusion" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %> 

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <%--<link href="../Javascripts/jquery-ui-1.8.1.custom.css" rel="stylesheet" type="text/css" />
    <script src="../Javascripts/jquery-1.4.1-vsdoc.js" type="text/javascript"></script>
    <script src="../Javascripts/jquery-ui-1.8.1.custom.min.js" type="text/javascript"></script>--%>
</asp:Content>

<asp:Content ContentPlaceHolderID="cMC" runat="server"> 
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hdnUserName" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnReceiveDetailID" runat="server" />
            <asp:HiddenField ID="hdnFromClientID" runat="server" />
            <asp:HiddenField ID="hdnToClientID" runat="server" />
            <asp:HiddenField ID="hdnScanKey" runat="server" />

            <h1>Unit/Bin Transfers:</h1>

            <label class="d-block">Transfer Type:</label>
            <asp:DropDownList ID="drpTransferType" CssClass="w-50" runat="server">
                <asp:ListItem Text="Project Transfer" Value="0" />
                <%--<asp:ListItem Text="Product Re-assessment" Value="0" />
                <asp:ListItem Text="GMP Buy" Value="1" />
                <asp:ListItem Text="Grave Yard" Value="3" />--%>
            </asp:DropDownList>

            <asp:Button ID="btnTransfer" CssClass="float-right" runat="server" Text="Transfer"/>

            <hr>

            <div class="row">
                <div class="col col-12 col-md-6">
                    <h2>From:</h2>
                    <label>IMEI or XBINX Number:</label>
                    <asp:TextBox ID="ScanKey" runat="server" ClientIDMode="Static" ToolTip="Enter ESN or XBINXBOX" AutoPostBack="True" />
                    <label>Make/Model/Colour:</label>
            	    <asp:TextBox ID="lblMakeModelColour" runat="server" Text="" ToolTip="Make, Model, Colour" Enabled="false" placeholder="Make, Model, Colour" />
                    <label>Project:</label>
            	    <asp:TextBox ID="lblProject" runat="server" Text="" Enabled="false" ToolTip="Project" placeholder="Project" />
                    <label>Client:</label>
                    <div class="form-row">
                    	<div class="col col-6">
                            <asp:TextBox ID="lblFromClient" runat="server" Text="" ToolTip="Client Address" Enabled="False" placeholder="Client Address" />
                        </div>
                    	<div class="col col-6">
                            <asp:TextBox ID="txtClientNamea" runat="server" ClientIDMode="Static" Text="" ToolTip="Store Name" Enabled="False" placeholder="Store Name" />
                        </div>
                        <div class="col col-6">
                            <asp:TextBox ID="txtStoreNumbera" runat="server" ClientIDMode="Static" Text="" ToolTip="Store Number" Enabled="False" placeholder="Store Number" />
                        </div>
                        <div class="col col-6">
                            <asp:TextBox ID="txtStoreSuffixa" runat="server" ClientIDMode="Static" Text="" ToolTip="Store Suffix" Enabled="False" placeholder="Store Suffix" />
                        </div>
                    </div>
                    <asp:TextBox ID="txtClientAddressa" runat="server" ClientIDMode="Static" Text="" TextMode="MultiLine" Enabled="False" ToolTip="Location Address"
                        Rows="4" placeholder="Location Address" />
                </div>

                <div class="col col-12 col-md-6">
                    <h2>To:</h2>
                    <label>Project:</label>
                    <asp:DropDownList ID="drpProjectList" runat="server" ToolTip="Project" />
                    <label>Client:</label>
                    <div class="input-group">
                        <div class="input-group-prepend">
                            <asp:LinkButton ID="btnSearchClient" CssClass="btn btn-default" runat="server" OnClientClick="OpenClientSearch('Bill');return false;" ToolTip="Search Clients">
                                <span class="oi oi-magnifying-glass"></span>
                            </asp:LinkButton>
                        </div>
                        <asp:TextBox ID="txtClientScanKey" runat="server" ToolTip="Enter Client Scan key here." AutoPostBack="True" />
                        <div class="input-group-append">
                            <asp:Button ID="btnClientSearch" runat="server" Text="Go" />
                        </div>
                    </div>
                    <div class="form-row">
                    	<div class="col col-6">
                            <asp:TextBox ID="lblToClient" runat="server" Text="" ToolTip="Client Address" Enabled="False"
                                placeholder="Client Address" />
                        </div>
                        <div class="col col-6">
                            <asp:TextBox ID="txtClientName" runat="server" ClientIDMode="Static" Text="" ToolTip="Store Name" Enabled="False"
                                placeholder="Store Name" />
                        </div>
                        <div class="col col-6">
                            <asp:TextBox ID="txtStoreNumber" runat="server" ClientIDMode="Static" Text="" ToolTip="Store Number" Enabled="False"
                                placeholder="Store Number" />
                        </div>
                        <div class="col col-6">
                            <asp:TextBox ID="txtStoreSuffix" runat="server" ClientIDMode="Static" Text="" ToolTip="Store Suffix" Enabled="False"
                                placeholder="Store Suffix" />
                        </div>
                    </div>
                    <asp:TextBox ID="txtClientAddress" runat="server" ClientIDMode="Static" Text="" TextMode="MultiLine" Enabled="False"
                        ToolTip="Location Address" Rows="4" placeholder="Location Address" />
                </div>
            </div>

            <div id="wndSelectClientLocation" class="modal" tabindex="-1" role="dialog">
                <div class="modal-dialog modal-lg" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Select Client Location</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <asp:HiddenField ID="TargetAddress" runat="server" />
                            Master Client Name:
                            <asp:TextBox ID="txtsClientName" runat="server"></asp:TextBox>
                            Location Name or Scankey:
                            <asp:TextBox ID="txtsLocationName" runat="server"></asp:TextBox>
                            Street:
                            <asp:TextBox ID="txtsStreet" runat="server"></asp:TextBox>
                            Postal Code:
                            <asp:TextBox ID="txtsPostalCode" runat="server"></asp:TextBox>

                            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClientClick="SearchClient();return false;" />

                            <asp:Panel ID="pnlSearchResult" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

<asp:Content ContentPlaceHolderID="js" runat="server">
    <script type="text/javascript">

        // *****************************************************************************
        function OpenClientSearch() {
            //$find('< %=this.wndSelectClientLocation.ClientID%>').Title = "Client Search";
            //$find('< %=this.wndSelectClientLocation.ClientID%>').Open(null, null);
            $('#wndSelectClientLocation').modal('show');
        }

        function selx(ID) {
            //$find('< %=wndSelectClientLocation.ClientID%>').Close();
            $('#wndSelectClientLocation').modal('hide');

            MCL("txtClientScanKey").value = ID;
            //MCL('txtClientScanKey').focus();
            MCL("btnClientSearch").click();
            // LoadClientLocation(ID);
        }

        function SearchClient() {
            var SearchClientName = $get("<%= txtsClientName.ClientID %>").value;
            var SearchLocationName = $get("<%= txtsLocationName.ClientID %>").value;
            var SearchStreet = $get("<%= txtsStreet.ClientID %>").value;
            var SearchPostalCode = $get("<%= txtsPostalCode.ClientID %>").value;
            var service = new WebServer_01();
            var rValue = service.GetSearchClientLocationData(MCL("UserName").value, SearchClientName, SearchLocationName, SearchStreet, SearchPostalCode, onSearchClientSuccess, onWebServerError);
        }

        function onWebServerError(Result) {
            alert("Error:" + Result.get_message());
        }

        function onSearchClientSuccess(Result) {
            var OutputHTML = "";
            var HeaderText = "<tr><th>Select</th><th>ID</th><th>Client</th><th>Location Name</th><th>Location</th></tr>";
            var BodyText = "";

            //           ClientData = eval('({' + Result + '})');
            ClientData = eval('[' + Result + ']');               // Square brackets to denote an array of elements.
            var Quote = "'";
            for (var i = 0; i < ClientData.length; i++) {
                BodyText = BodyText + "<tr><td>"
                    + '<button id="btn" name="btn" onClick="selx(' + Quote
                    + ClientData[i].ScanKey + Quote
                    + '); return false;">Select</button>'
                    + "</td><td>"
                    + ClientData[i].ClientLocationID
                    + "</td><td>"
                    + ClientData[i].txtClientName
                    + "</td><td>"

                    + ClientData[i].txtLocationName
                    + "</td><td>"

                    + ClientData[i].txtStoreNumber + " " + ClientData[i].txtStoreSuffix + " " + ClientData[i].txtClientAddress
                    + "</td></tr>";
            }
            OutputHTML = "<hr><table id='XX' class='table'>" + HeaderText + BodyText + "</table>"
            var SearchResults = $get("<%= pnlSearchResult.ClientID %>");
            SearchResults.innerHTML = OutputHTML;
        }
        // *****************************************************************************

        function MCL(ControlName) {
            switch (ControlName.toUpperCase()) {
                case "USERNAME": return $get("<%= hdnUserName.ClientID %>"); break;
                case "TXTCLIENTSCANKEY": return $get("<%= txtClientScanKey.ClientID %>"); break;
                case "BTNCLIENTSEARCH": return $get("< %= btnClientSearch.ClientID %>"); break; 
                default: return null;
            }
        }
    </script>
</asp:Content>

