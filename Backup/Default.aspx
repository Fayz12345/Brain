<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BW_WebApp.Default" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content runat="server" ContentPlaceHolderID="HeadContent"></asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="cMC">
    <div class="jumbotron">
        <h1><asp:Label ID="lblCompanyName" runat="server" Text="Label"/></h1>
    </div>
</asp:Content>
