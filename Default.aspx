<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="hada_ProyectoGrupo.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p>
    <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="~/Public/Login.aspx">Login</asp:LinkButton>
    </p>
    <asp:LinkButton ID="LinkButton2" runat="server" PostBackUrl="~/Public/Registro.aspx">Registro</asp:LinkButton>

</asp:Content>
