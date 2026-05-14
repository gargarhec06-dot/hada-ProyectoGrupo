<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="hada_ProyectoGrupo.Default" %>
<%@ OutputCache Duration="1" VaryByParam="none" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .admin-box { margin-top: 30px; padding: 20px; border: 1px solid #ff005533; background-color: #1a0a1a; border-radius: 12px; display: inline-block; }
        .btn-hero-admin { background-color: #ff0055; color: #ffffff; border: none; padding: 12px 35px; border-radius: 6px; font-weight: 800; text-transform: uppercase; letter-spacing: 2px; font-size: 0.9rem; cursor: pointer; text-decoration: none; display: inline-block; transition: all 0.2s; box-shadow: 0 0 20px #ff005544; }
        .btn-hero-admin:hover { background-color: #cc0044; box-shadow: 0 0 30px #ff005566; color: #ffffff; }
        .hero-wrapper { display: flex; flex-direction: column; align-items: center; justify-content: center; min-height: calc(100vh - 61px); text-align: center; padding: 40px 20px; position: relative; }
        .hero-line { width: 60px; height: 2px; background: linear-gradient(to right, transparent, #00e5ff, transparent); box-shadow: 0 0 8px #00e5ff; margin: 0 auto 40px auto; }
        .hero-buttons { display: flex; gap: 20px; justify-content: center; flex-wrap: wrap; margin-bottom: 60px; }
        .btn-hero-primary { background-color: #00e5ff; color: #0f0f1a; border: none; padding: 12px 35px; border-radius: 6px; font-weight: 800; text-transform: uppercase; letter-spacing: 2px; font-size: 0.9rem; cursor: pointer; text-decoration: none; transition: all 0.2s; box-shadow: 0 0 20px #00e5ff44; }
        .btn-hero-secondary { background-color: transparent; color: #00e5ff; border: 2px solid #00e5ff; padding: 12px 35px; border-radius: 6px; font-weight: 800; text-transform: uppercase; letter-spacing: 2px; font-size: 0.9rem; cursor: pointer; text-decoration: none; transition: all 0.2s; }
        .welcome-box { background-color: #1a1a2e; border: 1px solid #00e5ff33; border-radius: 12px; padding: 25px 40px; margin-bottom: 40px; display: inline-block; }
        .welcome-greeting { font-size: 0.75rem; color: #00e5ff; text-transform: uppercase; letter-spacing: 3px; margin-bottom: 8px; }
        .welcome-name { font-size: 1.5rem; font-weight: 700; color: #ffffff; }
        .stats-row { display: flex; gap: 20px; justify-content: center; flex-wrap: wrap; margin-bottom: 40px; }
        .stat-card { background-color: #1a1a2e; border: 1px solid #00e5ff22; border-radius: 10px; padding: 20px 30px; text-align: center; min-width: 140px; transition: border-color 0.2s; }
        .stat-card:hover { border-color: #00e5ff; }
        .stat-card .stat-label { font-size: 0.65rem; color: #00e5ff; text-transform: uppercase; letter-spacing: 2px; margin-bottom: 8px; }
        .stat-card .stat-value { font-size: 1.8rem; font-weight: 800; color: #ffffff; }
        .hero-footer { font-size: 0.7rem; color: #444; text-transform: uppercase; letter-spacing: 3px; }
        .hud-dot-hero { width: 5px; height: 5px; background-color: #00e5ff; border-radius: 50%; display: inline-block; margin: 0 8px; box-shadow: 0 0 6px #00e5ff; animation: pulse 2s infinite; }
        @keyframes pulse { 0%, 100% { opacity: 1; } 50% { opacity: 0.3; } }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="hero-wrapper">
        <div class="hero-line"></div>

        <asp:Panel ID="pnlNoLogueado" runat="server" Visible="false">
            <div class="hero-buttons">
                <asp:HyperLink ID="hlLogin" runat="server" NavigateUrl="~/Public/Login.aspx" CssClass="btn-hero-primary">Iniciar Sesión</asp:HyperLink>
                <asp:HyperLink ID="hlRegistro" runat="server" NavigateUrl="~/Public/Registro.aspx" CssClass="btn-hero-secondary">Registrarse</asp:HyperLink>
            </div>
        </asp:Panel>

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
                    <div class="stat-value"><asp:Label ID="lblNumJugadores" runat="server" Text="0" /></div>
                </div>
                <div class="stat-card">
                    <div class="stat-label">Torneos activos</div>
                    <div class="stat-value"><asp:Label ID="lblNumTorneos" runat="server" Text="0" /></div>
                </div>
                <div class="stat-card">
                    <div class="stat-label">Patrocinadores</div>
                    <div class="stat-value"><asp:Label ID="lblNumPatrocinadores" runat="server" Text="0" /></div>
                </div>
            </div>

            <asp:Panel ID="pnlAdminStats" runat="server" Visible="false" CssClass="admin-box">
                <div class="welcome-greeting" style="color: #ff0055;">Panel de Control</div>
                <div style="margin-top:15px;">
                    <asp:HyperLink ID="hlEstadisticas" runat="server" 
                        NavigateUrl="~/Public/Estadisticas.aspx" 
                        CssClass="btn-hero-admin">📊 Ver Estadísticas Globales</asp:HyperLink>
                </div>
            </asp:Panel>
        </asp:Panel>

        <div class="hero-footer">
            <span class="hud-dot-hero"></span> Sistema Online <span class="hud-dot-hero"></span>
        </div>
    </div>
</asp:Content>