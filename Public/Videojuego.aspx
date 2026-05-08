<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Videojuego.aspx.cs" Inherits="hada_ProyectoGrupo.Public.Videojuego" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Vista detallada del videojuego</h2>
    <div class="d-flex flex-column">
        <div>
            <asp:Label Text="<b>Nombre:</b> " runat="server"/>
            <asp:Label Text="" ID="NombreLabel" runat="server"/>
            <asp:TextBox ID="NombreAdminBox" Visible="false" runat="server"/>
        </div>
        <div>
            <asp:Label Text="<b>Código:</b> " runat="server"/>
            <asp:Label Text="" ID="CodigoLabel" runat="server"/>
            <asp:TextBox TextMode="Number" Visible="false" ID="CodigoAdminBox" runat="server"/>
        </div>
        <div>
            <asp:Label Text="<b>Tipo:</b> " runat="server"/>
            <asp:Label Text="" ID="TipoLabel" runat="server"/>
            <asp:TextBox ID="TipoAdminBox" Visible="false" runat="server"/>
        </div>
        <div class="d-flex flex-column">
            <asp:Label Text="<b>Descripción:</b> " runat="server"/>
            <asp:Label Text="" ID="DescripcionLabel" runat="server"/>
            <asp:TextBox TextMode="MultiLine" Visible="false" ID="DescripcionAdminBox" runat="server"/>
        </div>
        <div>
            <asp:Label Text="<b>Edad mínima:</b> " runat="server"/>
            <asp:Label Text="" ID="EdadMinimaLabel" runat="server"/>
            <asp:TextBox TextMode="Number" Visible="false" ID="EdadMinimaAdminBox" runat="server"/>
        </div>
        <div>
            <asp:Label Text="" ID="DebugLabel" runat="server"/>
        </div>
        <div>
            <asp:Button Text="Actualizar entrada" ID="AdminUpdate" runat="server" Visible="false" OnClick="AdminUpdate_Click" CssClass="btn btn-info"/>
            <asp:Button Text="Añadir entrada" ID="AdminAdd" runat="server" Visible="false" CssClass="btn btn-info" OnClick="AdminAdd_Click"/>
            <asp:Button Text="Borrar entrada" ID="AdminDelete" runat="server" Visible="false" OnClick="AdminDelete_Click" OnClientClick="return confirm('¿Estás seguro de eliminar este videojuego?')" CssClass="btn btn-danger"/>
        </div>
        <div>
            <a href="Videojuegos.aspx" class="btn btn-secondary">Volver</a>
        </div>
    </div>
</asp:Content>
