<%@ Page Title="Detalle de Noticia" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DetallesNoticia.aspx.cs" Inherits="hada_ProyectoGrupo.Public.DetallesNoticia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 style="margin-bottom:20px;">Detalle de la Noticia</h2>

    <div style="display:flex; align-items:flex-start; gap:40px; margin-bottom:30px;">
        <div style="flex: 1;">
            <p><strong>Título:</strong> <asp:Label ID="lblTitulo" runat="server" /></p>
            <p><strong>Fecha:</strong> <asp:Label ID="lblFecha" runat="server" /></p>
            <p><strong>Autor:</strong> <asp:Label ID="lblAutor" runat="server" /></p>
            <hr />
            <p><strong>Contenido:</strong></p>
            <p><asp:Label ID="lblContenido" runat="server" /></p>
        </div>
        <div style="flex: 0 0 300px;">
            <asp:Image ID="imgNoticia" runat="server" Width="300px" 
                style="border-radius:8px; box-shadow: 0 4px 12px rgba(0,0,0,0.15);" />
        </div>
    </div>

    <asp:Button ID="btnVolver" runat="server" Text="Volver" 
        OnClick="btnVolver_Click" CssClass="btn btn-secondary" />

    <asp:Panel ID="pnlAdmin" runat="server" Visible="false" style="margin-top: 20px; border-top: 1px solid #eee; padding-top: 20px;">
        <asp:Button ID="btnCrear" runat="server" Text="CREAR" OnClick="btnCrear_Click" CssClass="btn btn-success" style="margin-right:10px;" />
        <asp:Button ID="btnModificar" runat="server" Text="Modificar" OnClick="btnModificar_Click" CssClass="btn btn-success" style="margin-right:10px;" />
        <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" OnClick="btnEliminar_Click" CssClass="btn btn-danger" />
    </asp:Panel>
</asp:Content>