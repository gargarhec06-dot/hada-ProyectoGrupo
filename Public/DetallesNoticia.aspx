<%@ Page Title="Detalle de Noticia" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DetallesNoticia.aspx.cs" Inherits="hada_ProyectoGrupo.Public.DetallesNoticia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Detalle de la Noticia</h2>

    <div style="display:flex; align-items:flex-start; gap:40px;">
        <div>
            <p><strong>Título :</strong> <asp:TextBox ID="txtTitulo" runat="server" Width="300px" /></p>
            <p><strong>Fecha de Publicación :</strong> <asp:TextBox ID="txtFecha" runat="server" /></p>
            <p><strong>Contenido :</strong> <asp:TextBox ID="txtContenido" runat="server" TextMode="MultiLine" Rows="5" Width="400px" /></p>
            <p><strong>Autor (Email) :</strong> <asp:TextBox ID="txtAutor" runat="server" /></p>
            
          
            <asp:HiddenField ID="hfIdUsuario" runat="server" Value="" />
        </div>
        <div>
            <asp:Image ID="imgNoticia" runat="server" Width="250px" style="border-radius:5px; border: 1px solid #ccc;" />
        </div>
    </div>

    <asp:Button ID="btnVolver" runat="server" Text="Volver" OnClick="btnVolver_Click" CssClass="btn btn-secondary" />

    <asp:Panel ID="pnlAcciones" runat="server" Visible="false" style="margin-top: 20px;">
        <asp:Button ID="btnCrear" runat="server" Text="CREAR" OnClick="btnCrear_Click" CssClass="btn btn-success" />
        <asp:Button ID="btnModificar" runat="server" Text="Modificar" OnClick="btnModificar_Click" CssClass="btn btn-warning" />
        <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" OnClick="btnEliminar_Click" CssClass="btn btn-danger" />
    </asp:Panel>

    <asp:Label ID="lblMensaje" runat="server" ForeColor="Red" style="display:block; margin-top:10px;" />

</asp:Content>