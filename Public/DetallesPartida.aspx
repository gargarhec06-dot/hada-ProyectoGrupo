<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DetallesPartida.aspx.cs" Inherits="hada_ProyectoGrupo.Public.DetallesPartida" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Vista detallada de la partida</h2>
    <div class="d-flex flex-column">
    <div>
        <asp:Label Text="<b>Código:</b> " runat="server"/>
        <asp:Label Text="" ID="CodigoLabel" runat="server"/>
    </div>
    <div>
        <asp:Label Text="<b>Torneo:</b> " runat="server"/>
        <asp:Hyperlink Text="" ID="TorneoLabel" runat="server"/>
    </div>
    <div>
        <asp:Label Text="<b>Fecha:</b> " runat="server"/>
        <asp:Label Text="" ID="FechaLabel" runat="server"/>
        <asp:TextBox TextMode="Date" Visible="false" ID="FechaAdmin" runat="server"/>
    </div>
    <div>
        <asp:Label Text="<b>Repetición:</b> " runat="server"/>
        <asp:Hyperlink Text="" ID="EnlaceRepeticion" runat="server"/>
        <asp:TextBox TextMode="Url" Visible="false" ID="EnlaceRepeticionAdmin" runat="server"/>
    </div>
    <div>
        <asp:Label Text="<b>Videojuego:</b> " runat="server"/>
        <asp:Hyperlink Text="" ID="VideojuegoEnlace" runat="server"/>
    </div>
    <div>
        <asp:Label Text="<b>Equipo ganador:</b> " runat="server"/>
        <asp:Label Text="" ID="EquipoGanadorLabel" runat="server"/>
        <asp:DropDownList Visible="false" ID="EquipoGanadorAdmin" runat="server"/>
    </div>
    <div>
        <asp:Label Text="<b>Jugadores:</b> " runat="server"/>
        <asp:Label Text="" ID="JugadoresLabel" runat="server"/>
    </div>
    <div>
        <asp:Label Text="<b>Perdedores:</b> " runat="server"/>
        <asp:Label Text="" ID="PerdedoresLabel" runat="server"/>
    </div>
    <div>
        <asp:Label Text="" ID="DebugLabel" runat="server"/>
    </div>
    <div>
        <asp:Button Text="Actualizar entrada" ID="AdminUpdate" runat="server" Visible="false" CssClass="btn btn-info"/>
        <asp:Button Text="Añadir entrada" ID="AdminAdd" runat="server" Visible="false" CssClass="btn btn-info" OnClick="AdminAdd_Click"/>
        <asp:Button Text="Borrar entrada" ID="AdminDelete" runat="server" Visible="false" OnClientClick="return confirm('¿Estás seguro de eliminar este videojuego?')" CssClass="btn btn-danger"/>
    </div>
    </div>
</asp:Content>
