<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="hada_ProyectoGrupo.Public.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .login-bg {
            position: fixed;
            top: 0; left: 0;
            width: 100%; height: 100%;
            background-image: url('/Fondo/Fondo2.png');
            background-size: cover;
            background-position: center;
            opacity: 0.3;
            z-index: 0;
            pointer-events: none;
        }

        .login-wrapper {
            display: flex;
            align-items: center;
            justify-content: center;
            min-height: calc(100vh - 61px);
            position: relative;
            z-index: 1;
            padding: 40px 20px;
        }

        .login-card {
            background-color: #1a1a2e;
            border: 1px solid #00e5ff33;
            border-radius: 12px;
            padding: 40px 50px;
            width: 100%;
            max-width: 420px;
            box-shadow: 0 0 40px #00e5ff11;
        }

        .login-title {
            font-size: 1.5rem;
            font-weight: 800;
            color: #00e5ff;
            text-transform: uppercase;
            letter-spacing: 3px;
            text-align: center;
            margin-bottom: 8px;
            text-shadow: 0 0 10px #00e5ff44;
        }

        .login-line {
            width: 40px;
            height: 2px;
            background: linear-gradient(to right, transparent, #00e5ff, transparent);
            box-shadow: 0 0 8px #00e5ff;
            margin: 0 auto 30px auto;
        }

        .login-field {
            margin-bottom: 20px;
        }

        .login-field label {
            display: block;
            font-size: 0.75rem;
            color: #00e5ff;
            text-transform: uppercase;
            letter-spacing: 2px;
            margin-bottom: 6px;
        }

        .login-field input {
            width: 100%;
            background-color: #0f0f1a;
            border: 1px solid #00e5ff44;
            color: #e0e0e0;
            border-radius: 6px;
            padding: 10px 14px;
            font-size: 0.95rem;
            transition: border-color 0.2s, box-shadow 0.2s;
        }

        .login-field input:focus {
            outline: none;
            border-color: #00e5ff;
            box-shadow: 0 0 10px #00e5ff33;
        }

        .btn-login {
            width: 100%;
            background-color: #00e5ff;
            color: #0f0f1a;
            border: none;
            padding: 12px;
            border-radius: 6px;
            font-weight: 800;
            text-transform: uppercase;
            letter-spacing: 2px;
            font-size: 0.9rem;
            cursor: pointer;
            transition: all 0.2s;
            box-shadow: 0 0 20px #00e5ff33;
            margin-top: 10px;
        }

        .btn-login:hover {
            background-color: #00b8cc;
            box-shadow: 0 0 30px #00e5ff55;
        }

        .login-error {
            display: block;
            text-align: center;
            color: #ff4444;
            font-size: 0.8rem;
            margin-top: 12px;
            min-height: 20px;
        }

        .login-register {
            text-align: center;
            margin-top: 20px;
            font-size: 0.8rem;
            color: #666;
        }

        .login-register a {
            color: #00e5ff;
            text-decoration: none;
        }

        .login-register a:hover {
            text-decoration: underline;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="login-bg"></div>
    <div class="login-wrapper">
        <div class="login-card">
            <div class="login-title">Iniciar Sesión</div>
            <div class="login-line"></div>

            <div class="login-field">
                <asp:Label runat="server" Text="Email" AssociatedControlID="EmailBox" />
                <asp:TextBox ID="EmailBox" TextMode="Email" runat="server" CssClass="form-control" />
            </div>

            <div class="login-field">
                <asp:Label runat="server" Text="Contraseña" AssociatedControlID="PasswordBox" />
                <asp:TextBox ID="PasswordBox" TextMode="Password" runat="server" CssClass="form-control" />
            </div>

            <asp:Button ID="LogInButton" runat="server" Text="Iniciar Sesión"
                OnClick="LogInButton_Click" CssClass="btn-login" />

            <asp:Label ID="LogInError" runat="server" CssClass="login-error" />

            <div class="login-register">
                ¿No tienes cuenta? 
                <asp:HyperLink runat="server" NavigateUrl="~/Public/Registro.aspx">Regístrate aquí</asp:HyperLink>
            </div>
        </div>
    </div>
</asp:Content>