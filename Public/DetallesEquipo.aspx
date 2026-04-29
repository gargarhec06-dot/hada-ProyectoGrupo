<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DetallesEquipo.aspx.cs" Inherits="hada_ProyectoGrupo.Public.DetallesEquipo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Detalle del Equipo</h2>

<div style="display:flex; align-items:flex-start; gap:40px;">
    <div>
        <p><strong>Nombre :</strong> <asp:Label ID="lblNombre" runat="server"/></p>

        <p><strong>Fecha de Creación :</strong> <asp:Label ID="lblFecha" runat="server"/></p>

        <p><strong>Descripción :</strong> <asp:Label ID="lblDescripción" runat="server"/></p>

        <p><strong>ID Capitán :</strong> <asp:Label ID="lblCapitan" runat="server"/></p>
    </div>
    <div>
        <asp:Image ID="imgLogo" runat="server" Width="200px" />
    </div>

</div>

 <asp:Button ID="btnVolver" runat="server" Text="Volver" 
     OnClick="btnVolver_Click" CssClass="btn btn-secondary" />

</asp:Content>
