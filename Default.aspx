<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="hada_ProyectoGrupo.Default" %>
<%@ OutputCache Duration="1" VaryByParam="none" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .hero-wrapper {
            display: flex;
            flex-direction: column;
            align-items: center;
            justify-content: center;
            min-height: calc(100vh - 61px);
            text-align: center;
            padding: 40px 20px;
            position: relative;
        }

        .hero-tag {
            font-size: 0.75rem;
            color: #00e5ff;
            text-transform: uppercase;
            letter-spacing: 4px;
            margin-bottom: 15px;
            opacity: 0.8;
        }

        .hero-title {
            font-size: 4rem;
            font-weight: 900;
            color: #ffffff;
            text-transform: uppercase;
            letter-spacing: 6px;
            line-height: 1.1;
            text-shadow: 0 0 30px #00e5ff44;
            margin-bottom: 10px;
        }

        .hero-title span {
            color: #00e5ff;
            text-shadow: 0 0 20px #00e5ff;
        }

        .hero-subtitle {
            font-size: 1rem;
            color: #888;
            letter-spacing: 2px;
            margin-bottom: 50px;
            text-transform: uppercase;
        }

        .hero-line {
            width: 60px;
            height: 2px;
            background: linear-gradient(to right, transparent, #00e5ff, transparent);
            box-shadow: 0 0 8px #00e5ff;
            margin: 0 auto 40px auto;
        }

        .hero-buttons {
            display: flex;
            gap: 20px;
            justify-content: center;
            flex-wrap: wrap;
            margin-bottom: 60px;
        }

        .btn-hero-primary {
            background-color: #00e5ff;
            color: #0f0f1a;
            border: none;
            padding: 12px 35px;
            border-radius: 6px;
            font-weight: 800;
            text-transform: uppercase;
            letter-spacing: 2px;
            font-size: 0.9rem;
            cursor: pointer;
            text-decoration: none;
            transition: all 0.2s;
            box-shadow: 0 0 20px #00e5ff44;
        }

        .btn-hero-primary:hover {
            background-color: #00b8cc;
            box-shadow: 0 0 30px #00e5ff66;
            color: #0f0f1a;
        }

        .btn-hero-secondary {
            background-color: transparent;
            color: #00e5ff;
            border: 2px solid #00e5ff;
            padding: 12px 35px;
            border-radius: 6px;
            font-weight: 800;
            text-transform: uppercase;
            letter-spacing: 2px;
            font-size: 0.9rem;
            cursor: pointer;
            text-decoration: none;
            transition: all 0.2s;
        }

        .btn-hero-secondary:hover {
            background-color: #00e5ff22;
            color: #00e5ff;
        }

        .welcome-box {
            background-color: #1a1a2e;
            border: 1px solid #00e5ff33;
            border-radius: 12px;
            padding: 25px 40px;
            margin-bottom: 40px;
            display: inline-block;
        }

        .welcome-box .welcome-greeting {
            font-size: 0.75rem;
            color: #00e5ff;
            text-transform: uppercase;
            letter-spacing: 3px;
            margin-bottom: 8px;
        }

        .welcome-box .welcome-name {
            font-size: 1.5rem;
            font-weight: 700;
            color: #ffffff;
        }

        .stats-row {
            display: flex;
            gap: 20px;
            justify-content: center;
            flex-wrap: wrap;
            margin-bottom: 40px;
        }

        .stat-card {
            background-color: #1a1a2e;
            border: 1px solid #00e5ff22;
            border-radius: 10px;
            padding: 20px 30px;
            text-align: center;
            min-width: 140px;
            transition: border-color 0.2s;
        }

        .stat-card:hover { border-color: #00e5ff; }

        .stat-card .stat-label {
            font-size: 0.65rem;
            color: #00e5ff;
            text-transform: uppercase;
            letter-spacing: 2px;
            margin-bottom: 8px;
        }

        .stat-card .stat-value {
            font-size: 1.8rem;
            font-weight: 800;
            color: #ffffff;
        }

        .hero-footer {
            font-size: 0.7rem;
            color: #444;
            text-transform: uppercase;
            letter-spacing: 3px;
        }

        .hud-dot-hero {
            width: 5px;
            height: 5px;
            background-color: #00e5ff;
            border-radius: 50%;
            display: inline-block;
            margin: 0 8px;
            box-shadow: 0 0 6px #00e5ff;
            animation: pulse 2s infinite;
        }

        @keyframes pulse {
            0%, 100% { opacity: 1; }
            50% { opacity: 0.3; }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="hero-wrapper">
        <div class="hero-line"></div>

        <!-- Panel para usuario NO logueado -->
        <asp:Panel ID="pnlNoLogueado" runat="server" Visible="false">
       <div class="hero-buttons">
           <asp:HyperLink ID="hlLogin" runat="server" 
               NavigateUrl="~/Public/Login.aspx" 
               CssClass="btn-hero-primary">Iniciar Sesión</asp:HyperLink>
           <asp:HyperLink ID="hlRegistro" runat="server" 
               NavigateUrl="~/Public/Registro.aspx" 
               CssClass="btn-hero-secondary">Registrarse</asp:HyperLink>
       </div>
        </asp:Panel>

        <!-- Panel para usuario logueado -->
        <asp:Panel ID="pnlLogueado" runat="server" Visible="false">
            <div class="welcome-box">
                <div class="welcome-greeting">Bienvenido de nuevo</div>
                <div class="welcome-name">
                    <asp:Label ID="lblNombre" runat="server" />
                </div>
            </div>

            <div class="stats-row">
                <div class="stat-card">
                    <div class="stat-label">Mis Jugadores</div>
                    <div class="stat-value">
                        <asp:Label ID="lblNumJugadores" runat="server" Text="0" />
                    </div>
                </div>
                <div class="stat-card">
                    <div class="stat-label">Torneos activos</div>
                    <div class="stat-value">
                        <asp:Label ID="lblNumTorneos" runat="server" Text="0" />
                    </div>
                </div>
                <div class="stat-card">
                    <div class="stat-label">Patrocinadores</div>
                    <div class="stat-value">
                        <asp:Label ID="lblNumPatrocinadores" runat="server" Text="0" />
                    </div>
                </div>
            </div>
        </asp:Panel>

        <div class="hero-footer">
            <span class="hud-dot-hero"></span>
            Sistema Online
            <span class="hud-dot-hero"></span>
        </div>

    </div>
</asp:Content>