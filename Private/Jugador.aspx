<%@ Page Title="Mis Jugadores" Language="C#" MasterPageFile="~/Site.Master" 
    AutoEventWireup="true" CodeBehind="Jugador.aspx.cs" 
    Inherits="hada_ProyectoGrupo.Private.Jugador" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Mis Jugadores</h2>

    <%-- Lista de jugadores existentes --%>
    <asp:Repeater ID="rptJugadores" runat="server">
        <ItemTemplate>
            <div>
                <p><strong>Apodo:</strong> <%# Eval("Apodo") %></p>
                <p><strong>Rol:</strong> <%# Eval("Rol_principal") %></p>
                <p><strong>Nivel:</strong> <%# Eval("Nivel") %></p>
                <p><strong>Buscando equipo:</strong> <%# (bool)Eval("Buscando_equipo") ? "Sí" : "No" %></p>
                <asp:Button runat="server" Text="Seleccionar" CommandArgument='<%# Eval("Codigo") %>'
                    OnCommand="btnSeleccionar_Command"/>
                <asp:Button runat="server" Text="Eliminar" CommandArgument='<%# Eval("Codigo") %>'
                    OnCommand="btnEliminar_Command"/>
                <hr/>
            </div>
        </ItemTemplate>
    </asp:Repeater>

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
        <asp:DropDownList ID="ddlRol" runat="server">
            <asp:ListItem Text="Selecciona un rol" Value=""/>
            <asp:ListItem Text="Top" Value="Top"/>
            <asp:ListItem Text="Jungle" Value="Jungle"/>
            <asp:ListItem Text="Mid" Value="Mid"/>
            <asp:ListItem Text="ADC" Value="ADC"/>
            <asp:ListItem Text="Support" Value="Support"/>
        </asp:DropDownList>
    </div>

    <div>
        <asp:Label runat="server" Text="Hardware:"/>
        <asp:DropDownList ID="ddlHardware" runat="server">
            <asp:ListItem Text="Teclado" Value="teclado"/>
            <asp:ListItem Text="Mando" Value="mando"/>
        </asp:DropDownList>
    </div>

    <div>
        <asp:Label runat="server" Text="¿Buscando equipo?"/>
        <asp:CheckBox ID="chkBuscandoEquipo" runat="server"/>
    </div>

    <asp:Button ID="btnCrear" runat="server" Text="Crear jugador" OnClick="btnCrear_Click"/>
    <asp:Label ID="lblMensaje" runat="server" ForeColor="Red"/>

</asp:Content>
