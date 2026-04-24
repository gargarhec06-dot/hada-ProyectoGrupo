<%@ Page Title="Registro" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="hada_ProyectoGrupo.Public.Registro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Registro de Usuario</h2>

    <div>
        <asp:Label runat="server" Text="Email:"/>
        <asp:TextBox ID="txtEmail" runat="server"/>
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtEmail" 
            ErrorMessage="El email es obligatorio" Display="Dynamic"/>
        <asp:RegularExpressionValidator runat="server" ControlToValidate="txtEmail" 
            ValidationExpression="^[\w\.-]+@[\w\.-]+\.\w+$" 
            ErrorMessage="Email no válido" Display="Dynamic"/>
    </div>

    <div>
        <asp:Label runat="server" Text="Contraseña:"/>
        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"/>
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPassword" 
            ErrorMessage="La contraseña es obligatoria" Display="Dynamic"/>
    </div>

    <div>
        <asp:Label runat="server" Text="Nombre:"/>
        <asp:TextBox ID="txtNombre" runat="server"/>
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtNombre" 
            ErrorMessage="El nombre es obligatorio" Display="Dynamic"/>
    </div>

    <div>
        <asp:Label runat="server" Text="Apellidos:"/>
        <asp:TextBox ID="txtApellidos" runat="server"/>
    </div>

    <div>
        <asp:Label runat="server" Text="Fecha de nacimiento:"/>
        <asp:TextBox ID="txtFecha" runat="server" TextMode="Date"/>
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtFecha" 
            ErrorMessage="La fecha es obligatoria" Display="Dynamic"/>
    </div>

    <div>
        <asp:Label runat="server" Text="País:"/>
        <asp:TextBox ID="txtPais" runat="server"/>
    </div>

    <div>
        <asp:Label runat="server" Text="Apodo (nickname):"/>
        <asp:TextBox ID="txtApodo" runat="server"/>
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtApodo" 
            ErrorMessage="El apodo es obligatorio" Display="Dynamic"/>
    </div>

    <asp:Button ID="btnRegistro" runat="server" Text="Registrarse" OnClick="btnRegistro_Click"/>

    <asp:Label ID="lblMensaje" runat="server" ForeColor="Red"/>

</asp:Content>