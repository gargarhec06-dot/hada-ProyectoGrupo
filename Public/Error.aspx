<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="hada_ProyectoGrupo.Public.Error" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="text-center py-5" style="margin-top: 200px;">
        <h2 class="text-danger">Ha sucedido un error</h2>
        <p class="text-muted">Por favor, vuelve a intentarlo más tarde.</p>
        <a href="/Default.aspx" class="btn btn-primary">Volver al inicio</a>
    </div>
</asp:Content>
