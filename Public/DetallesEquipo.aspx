<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DetallesEquipo.aspx.cs" Inherits="hada_ProyectoGrupo.Public.DetallesEquipo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Detalle del Equipo</h2>

    <div style="display:flex; align-items:flex-start; gap:40px;">
        <div>
            <p><strong>Nombre :</strong> <asp:TextBox ID="txtNombre" runat="server" /></p>
            <p><strong>Fecha de Creación :</strong> <asp:TextBox ID="txtFecha" runat="server" /></p>
            <p><strong>Descripción :</strong> <asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine" /></p>
            <p><strong>URL Logo :</strong> <asp:TextBox ID="txtLogo" runat="server" /></p>
            <asp:HiddenField ID="hfIdCapitan" runat="server" Value="0" />
            <p><strong>Capitán :</strong> <asp:Label ID="lblCapitanNombre" runat="server" Text="No seleccionado" /></p>
        </div>
        <div>
            <asp:Image ID="imgLogo" runat="server" Width="200px" />
        </div>
    </div>

    <asp:Button ID="btnVolver" runat="server" Text="Volver" OnClick="btnVolver_Click" CssClass="btn btn-secondary" />

    <asp:Panel ID="pnlAcciones" runat="server" Visible="false" style="margin-top: 20px;">
        <asp:Button ID="btnCrear" runat="server" Text="CREAR" OnClick="btnCrear_Click" CssClass="btn btn-success" />
        <asp:Button ID="btnModificar" runat="server" Text="Modificar" OnClick="btnModificar_Click" CssClass="btn btn-warning" />
        <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" OnClick="btnEliminar_Click" CssClass="btn btn-danger" />
        <asp:Button ID="btnUnirse" runat="server" Text="Unirse" OnClick="btnUnirse_Click" CssClass="btn btn-info" />
    </asp:Panel>

    <asp:Panel ID="pnlSeleccionJugador" runat="server" Visible="false" style="margin-top: 20px; padding: 15px; border: 1px solid #ccc; border-radius: 5px;">
        <h3>Selecciona un jugador como capitán</h3>
        <p>
            <asp:DropDownList ID="ddlJugadores" runat="server" CssClass="form-control">
            </asp:DropDownList>
        </p>
        <p>
            <asp:Button ID="btnConfirmar" runat="server" Text="Confirmar y Crear Equipo" OnClick="btnConfirmar_Click" CssClass="btn btn-primary" />
            <asp:Button ID="btnCancelarSeleccion" runat="server" Text="Cancelar" OnClick="btnCancelarSeleccion_Click" CssClass="btn btn-secondary" />
        </p>
    </asp:Panel>

    <asp:Label ID="lblMensaje" runat="server" ForeColor="Red" />

</asp:Content>