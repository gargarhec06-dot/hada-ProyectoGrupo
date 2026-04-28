<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DetallesEquipo.aspx.cs" Inherits="hada_ProyectoGrupo.Public.DetallesEquipo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Detalle del Equipo</h2>

<div>
    <asp:Label runat="server" Text="Nombre: "/>
    <asp:Label ID="lblNombre" runat="server"/>
</div>

<div>
    <asp:Label runat="server" Text="Fecha de creación : "/>
    <asp:Label ID="lblFecha" runat="server"/>
</div>

<div>
    <asp:Label runat="server" Text="Logo : "/>
    <asp:HyperLink ID="hlLogo" runat="server"/>
</div>

<div>
    <asp:Label runat="server" Text="Descripción: "/>
    <asp:Label ID="lblDescripción" runat="server"/>
</div>

<div>
    <asp:Label runat="server" Text="ID Capitán: "/>
    <asp:Label ID="lblCapitan" runat="server"/>
</div>

<asp:Button ID="btnVolver" runat="server" Text="Volver" 
    OnClick="btnVolver_Click"/>
</asp:Content>
