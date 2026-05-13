<%@ Page Title="Detalle de Noticia" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DetallesNoticia.aspx.cs" Inherits="hada_ProyectoGrupo.Public.DetallesNoticia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container" style="margin-top:20px;">
        <h2>Detalle de la Noticia</h2>
        <hr />

        <div style="display:flex; align-items:flex-start; gap:40px;">
            <div>
                <p><strong>Título :</strong> <br />
                    <asp:TextBox ID="txtTitulo" runat="server" Width="300px" CssClass="form-control" /></p>
                
                <p><strong>Fecha de Publicación :</strong> <br />
                    <asp:TextBox ID="txtFecha" runat="server" CssClass="form-control" ReadOnly="true" /></p>
                
                <p><strong>Contenido :</strong> <br />
                    <asp:TextBox ID="txtContenido" runat="server" TextMode="MultiLine" Rows="5" Width="400px" CssClass="form-control" /></p>
                
                <p><strong>Autor (Email) :</strong> <br />
                    <asp:TextBox ID="txtAutor" runat="server" CssClass="form-control" ReadOnly="true" /></p>
                
                <p><strong>URL Imagen :</strong> <br />
                    <asp:TextBox ID="txtImagenUrl" runat="server" CssClass="form-control" placeholder="http://..." /></p>

                <asp:Panel ID="pnlSubidaImagen" runat="server" style="margin-top:10px; padding:10px; border:1px dashed #ccc; border-radius:5px;">
                    <p><strong>O subir archivo local:</strong></p>
                    <asp:FileUpload ID="fuImagen" runat="server" accept="image/jpeg,image/png,image/jpg" />
                    <asp:Button ID="btnSubirImagen" runat="server" Text="Subir" OnClick="btnSubirImagen_Click" CssClass="btn btn-secondary btn-sm" />
                    <asp:Label ID="lblSubidaInfo" runat="server" ForeColor="Blue" Font-Size="Small" />
                </asp:Panel>

                <asp:HiddenField ID="hfIdUsuario" runat="server" Value="" />
            </div>
            
            <div style="text-align:center;">
                <p><strong>Vista Previa Imagen :</strong></p>
                <asp:Image ID="imgNoticia" runat="server" Width="250px" style="border-radius:5px; border: 1px solid #ccc;" 
                    ImageUrl="https://via.placeholder.com/250x150?text=Sin+Imagen" />
            </div>
        </div>

        <div style="margin-top: 20px;">
            <asp:Button ID="btnVolver" runat="server" Text="Volver" OnClick="btnVolver_Click" CssClass="btn btn-secondary" />

            <asp:Panel ID="pnlAcciones" runat="server" Visible="false" style="display:inline-block; margin-left:10px;">
                <asp:Button ID="btnCrear" runat="server" Text="PUBLICAR NOTICIA" OnClick="btnCrear_Click" CssClass="btn btn-success" Visible="false" />
                <asp:Button ID="btnModificar" runat="server" Text="Guardar Cambios" OnClick="btnModificar_Click" CssClass="btn btn-warning" Visible="false" />
                <asp:Button ID="btnEliminar" runat="server" Text="Eliminar Noticia" OnClick="btnEliminar_Click" CssClass="btn btn-danger" OnClientClick="return confirm('¿Seguro que quieres borrar esta noticia?');" Visible="false" />
            </asp:Panel>
        </div>

        <asp:Label ID="lblMensaje" runat="server" style="display:block; margin-top:10px;" />
    </div>
</asp:Content>