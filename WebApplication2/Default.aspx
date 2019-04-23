<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication2._Default" EnableEventValidation = "false"%>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="types" runat="server" Text='<%# Eval ("objtype") %>' Visible="false" />
    <asp:DropDownList ID="DropDownList1" runat="server" >
    </asp:DropDownList>
    <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
    <asp:Button ID="button" runat="server" Text="Search" OnClick="Btn_Click" />
<%--    <asp:GridView ID="GridView1" runat="server" OnRowDataBound = "OnRowDataBound" OnSelectedIndexChanged = "OnSelectedIndexChanged">
    </asp:GridView>
    <asp:LinkButton ID="lnkDummy" runat="server"></asp:LinkButton>--%>
</asp:Content>
