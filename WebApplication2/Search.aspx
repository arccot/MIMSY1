<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="WebApplication2._Search" EnableEventValidation = "false"%>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Button ID="Button1" runat="server" Text="Show Details" OnClick="DisplayFullRecord" />
    <asp:GridView ID="GridView2" runat="server" OnRowDataBound = "OnRowDataBound" OnSelectedIndexChanged = "OnSelectedIndexChanged">
    </asp:GridView>
    <asp:LinkButton ID="lnkDummy" runat="server"></asp:LinkButton>
</asp:Content>
