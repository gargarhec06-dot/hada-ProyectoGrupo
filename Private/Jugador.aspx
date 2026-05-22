<%@ Page Title="Mis Jugadores" Language="C#" MasterPageFile="~/Site.Master" 
    AutoEventWireup="true" CodeBehind="Jugador.aspx.cs" 
    Inherits="hada_ProyectoGrupo.Private.Jugador" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    

    <%-- Formulario para crear nuevo jugador --%>
    <h3>Crear nuevo jugador</h3>
    <div>
        <asp:Label runat="server" Text="Apodo:"/>
        <asp:TextBox ID="txtApodo" runat="server"/>
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtApodo"
            ErrorMessage="El apodo es obligatorio" Display="Dynamic"/>
    </div>

    <div>
        <asp:Label runat="server" Text="Rol principal:"/>
        <asp:TextBox ID="txtRol" runat="server"/>
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtApodo" ErrorMessage="El Rol es obligatorio" Display="Dynamic"/>
    </div>

    <div>
        <asp:Label runat="server" Text="Hardware:"/>
        <asp:DropDownList ID="ddlHardware" runat="server">
            <asp:ListItem Text="Teclado" Value="teclado"/>
            <asp:ListItem Text="Mando" Value="mando"/>
        </asp:DropDownList>
    </div>

    <div>
    <asp:Label runat="server" Text="Videojuego:"/>
    <asp:DropDownList ID="ddlVideojuego" runat="server">
        <asp:ListItem Text="Selecciona un videojuego" Value="0"/>
    </asp:DropDownList>
    </div>
    

    <div>
        <asp:Label runat="server" Text="¿Buscando equipo?"/>
        <asp:CheckBox ID="chkBuscandoEquipo" runat="server"/>
    </div>

    <asp:Button ID="btnCrear" runat="server" Text="Crear jugador" OnClick="btnCrear_Click"/>
    <asp:Label ID="lblMensaje" runat="server" ForeColor="Red"/>

</asp:Content>
