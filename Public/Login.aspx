<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="hada_ProyectoGrupo.Public.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Login de Usuario</h2>

    <div>
        <div>
            <asp:Label ID="EmailLabel" Text="Email: " runat="server"/>
            <asp:TextBox ID="EmailBox" TextMode="Email" runat="server" />
        </div>

        <div>
            <asp:Label ID="PasswordLabel" Text="Password: " runat="server"/>
            <asp:TextBox ID="PasswordBox" TextMode="Password" runat="server" />
        </div>
        <br/>
        <div>
            <asp:Button Text="Log in" runat="server" Width="100px" ID="LogInButton" OnClick="LogInButton_Click"/>
            <asp:Label Text="" runat="server" ID="LogInError"/>
        </div>
    </div>
</asp:Content>
