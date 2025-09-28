<%@ Page Title="" Language="C#" MasterPageFile="~/Shane.Master" AutoEventWireup="true" CodeBehind="Shane.aspx.cs" Inherits="BW_WebApp.Shane1" %>
<asp:Content ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="HeaderContent" runat="server">
    <h1>Test Page</h1>
    <p>This page is for testing styles and components</p>
</asp:Content>
<asp:Content ContentPlaceHolderID="NavContent" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="BodyContent" runat="server">
    <div class="bg-light p-5">
        <div class="row">
    	    <div class="col-auto">
                <h1>BW ... Database <small>(Sandbox JM Version)</small></h1>
            </div>
    	    <div class="col text-right my-auto">
                <button class="btn">button</button>
            </div>
        </div>
    </div>
    
    <hr>

    <select class="custom-select">
        <option selected>Open this select menu</option>
        <option value="1">One</option>
        <option value="2">Two</option>
        <option value="3">Three</option>
    </select>

    <hr>

    <h1 id="temp" style="display:none">:)</h1>
    <button onclick="$('#temp').show(); return false;">Show :)</button>
    <button onclick="$('#temp').hide(); return false;">Hide :)</button>

</asp:Content>
<asp:Content ContentPlaceHolderID="FooterContent" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="js" runat="server">
    <script type="text/javascript">
        //$('#temp').show();
    </script>
</asp:Content>


