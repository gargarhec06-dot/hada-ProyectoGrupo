<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DetallesJugador.aspx.cs" Inherits="hada_ProyectoGrupo.Public.DetallesJugador" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Detalles del Jugador</h2>

    <div>
        <p><strong>Código:</strong> <asp:Label ID="lblCodigo" runat="server" /></p>
        <p><strong>Email Usuario:</strong> <asp:Label ID="lblEmail" runat="server" /></p>
        <p><strong>Apodo:</strong> 
            <asp:Label ID="lblApodo" runat="server" />
            <asp:TextBox ID="txtApodo" runat="server" Visible="false" />
        </p>
        <p><strong>Rol Principal:</strong> 
        <asp:Label ID="lblRol" runat="server" />
        <asp:TextBox ID="txtRol" runat="server" Visible="false" />
        </p>
        <p><strong>KDA Promedio:</strong> 
            <asp:Label ID="lblKDA" runat="server" />
            <asp:TextBox ID="txtKDA" runat="server" Visible="false" TextMode="Number" step="0.1" />
        </p>
        <p> pENE </p>
        <p><strong>Winrate:</strong> 
            <asp:Label ID="lblWinrate" runat="server" />
            <asp:TextBox ID="txtWinrate" runat="server" Visible="false" TextMode="Number" step="0.1" />
        </p>
        <p><strong>Nivel:</strong> 
            <asp:Label ID="lblNivel" runat="server" />
            <asp:TextBox ID="txtNivel" runat="server" Visible="false" TextMode="Number" />
        </p>
        <p><strong>Hardware:</strong> 
            <asp:Label ID="lblHardware" runat="server" />
            <asp:DropDownList ID="ddlHardware" runat="server" Visible="false">
                <asp:ListItem Text="teclado" Value="teclado" />
                <asp:ListItem Text="mando" Value="mando" />
            </asp:DropDownList>
        </p>
        <p><strong>Buscando Equipo:</strong> 
            <asp:Label ID="lblBuscandoEquipo" runat="server" />
            <asp:CheckBox ID="chkBuscandoEquipo" runat="server" Visible="false" />
        </p>
        <p><strong>Equipo Actual:</strong> <asp:Label ID="lblEquipoActual" runat="server" /></p>
        <p><strong>Juego:</strong> 
            <asp:Label ID="lblJuego" runat="server" />
            <asp:DropDownList ID="ddlJuego" runat="server" Visible="false" DataTextField="Nombre" DataValueField="Codigo">
            </asp:DropDownList>
        </p>
    </div>

    <div style="margin-top: 20px;">
        <asp:Button ID="btnVolver" runat="server" Text="Volver" OnClick="btnVolver_Click" CssClass="btn btn-secondary" />
        <asp:Button ID="btnModificar" runat="server" Text="Modificar" OnClick="btnModificar_Click" CssClass="btn btn-warning" Visible="false" />
        <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" OnClick="btnEliminar_Click" CssClass="btn btn-danger" Visible="false" />
        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" CssClass="btn btn-success" Visible="false" />
        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" CssClass="btn btn-secondary" Visible="false" />
    </div>

    <asp:Label ID="lblMensaje" runat="server" ForeColor="Red" />

</asp:Content>