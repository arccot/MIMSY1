<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Details.aspx.cs" Inherits="WebApplication2._Details" EnableEventValidation = "false"%>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Details</h2>
    <asp:GridView ID="GridView3" runat="server" >
    </asp:GridView>
    <h2>Documents:</h2>
    <asp:GridView ID="GridView4" runat="server" >
    </asp:GridView>
</asp:Content>
