<%@ Page Title="Mi Perfil" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="PerfilUsuario.aspx.cs" Inherits="hada_ProyectoGrupo.Private.PerfilUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .perfil-wrapper {
            max-width: 700px;
            margin: 40px auto;
            padding: 0 20px;
        }

        .page-title {
            font-size: 2rem;
            font-weight: 700;
            text-transform: uppercase;
            letter-spacing: 3px;
            padding-left: 15px;
            margin-bottom: 30px;
            background: linear-gradient(to right, #00e5ff, #7b2fff);
            -webkit-background-clip: text;
            -webkit-text-fill-color: transparent;
            border-left: 4px solid #7b2fff;
            filter: drop-shadow(0 0 8px #7b2fff44);
        }

        .info-card {
            background: linear-gradient(135deg, #1a1a2e 60%, #1a0f2e 100%);
            border: 1px solid #7b2fff44;
            border-radius: 12px;
            padding: 30px 35px;
            margin-bottom: 25px;
            box-shadow: 0 0 30px #7b2fff11;
            position: relative;
            overflow: hidden;
        }

        .info-card::before {
            content: '';
            position: absolute;
            top: 0; left: 0;
            width: 60px; height: 60px;
            border-top: 2px solid #00e5ff;
            border-left: 2px solid #00e5ff;
            border-radius: 12px 0 0 0;
            box-shadow: -2px -2px 10px #00e5ff33;
        }

        .info-card::after {
            content: '';
            position: absolute;
            bottom: 0; right: 0;
            width: 60px; height: 60px;
            border-bottom: 2px solid #7b2fff;
            border-right: 2px solid #7b2fff;
            border-radius: 0 0 12px 0;
            box-shadow: 2px 2px 10px #7b2fff33;
        }

        .info-row {
            display: flex;
            align-items: center;
            padding: 12px 0;
            border-bottom: 1px solid #7b2fff11;
        }

        .info-row:last-child { border-bottom: none; }

        .info-label {
            font-size: 0.7rem;
            text-transform: uppercase;
            letter-spacing: 2px;
            width: 180px;
            min-width: 180px;
            background: linear-gradient(to right, #00e5ff, #7b2fff);
            -webkit-background-clip: text;
            -webkit-text-fill-color: transparent;
        }

        .info-value {
            color: #e0e0e0;
            font-size: 0.95rem;
        }

        .saldo-value {
            font-size: 1.2rem;
            font-weight: 800;
            background: linear-gradient(to right, #00e5ff, #7b2fff);
            -webkit-background-clip: text;
            -webkit-text-fill-color: transparent;
        }

        .botones-card {
            background: linear-gradient(135deg, #1a1a2e 60%, #1a0f2e 100%);
            border: 1px solid #7b2fff44;
            border-radius: 12px;
            padding: 20px 25px;
            display: flex;
            justify-content: space-between;
            align-items: center;
            flex-wrap: wrap;
            gap: 12px;
        }

        .botones-izq {
            display: flex;
            gap: 10px;
            flex-wrap: wrap;
        }

        .botones-der {
            display: flex;
            gap: 10px;
            flex-wrap: wrap;
        }

        .btn-jugadores {
            background-color: transparent;
            color: #00e5ff;
            border: 2px solid #00e5ff;
            padding: 9px 20px;
            border-radius: 6px;
            font-weight: 700;
            text-transform: uppercase;
            letter-spacing: 1px;
            font-size: 0.8rem;
            cursor: pointer;
            transition: all 0.2s;
        }

        .btn-jugadores:hover {
            background-color: #00e5ff;
            color: #0f0f1a;
        }

        .btn-saldo {
            background: linear-gradient(to right, #00e5ff, #7b2fff);
            color: #ffffff;
            border: none;
            padding: 9px 20px;
            border-radius: 6px;
            font-weight: 700;
            text-transform: uppercase;
            letter-spacing: 1px;
            font-size: 0.8rem;
            cursor: pointer;
            transition: all 0.2s;
            box-shadow: 0 0 15px #7b2fff33;
        }

        .btn-saldo:hover {
            box-shadow: 0 0 25px #7b2fff66;
            transform: translateY(-1px);
        }

        .btn-editar {
            background-color: transparent;
            color: #00e5ff;
            border: 2px solid #00e5ff;
            padding: 9px 20px;
            border-radius: 6px;
            font-weight: 700;
            text-transform: uppercase;
            letter-spacing: 1px;
            font-size: 0.8rem;
            cursor: pointer;
            transition: all 0.2s;
        }

        .btn-editar:hover {
            background: linear-gradient(to right, #00e5ff, #7b2fff);
            border-color: transparent;
            color: #ffffff;
        }

        .btn-cerrar {
            background-color: transparent;
            color: #888;
            border: 1px solid #444;
            padding: 9px 20px;
            border-radius: 6px;
            font-weight: 700;
            text-transform: uppercase;
            letter-spacing: 1px;
            font-size: 0.8rem;
            cursor: pointer;
            transition: all 0.2s;
        }

        .btn-cerrar:hover {
            border-color: #888;
            color: #e0e0e0;
        }

        .btn-eliminar {
            background-color: transparent;
            color: #ff4444;
            border: 2px solid #ff4444;
            padding: 9px 20px;
            border-radius: 6px;
            font-weight: 700;
            text-transform: uppercase;
            letter-spacing: 1px;
            font-size: 0.8rem;
            cursor: pointer;
            transition: all 0.2s;
        }

        .btn-eliminar:hover {
            background-color: #ff4444;
            color: #ffffff;
            box-shadow: 0 0 20px #ff444444;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="perfil-wrapper">
        <div class="page-title">Mi Perfil</div>

        <div class="info-card">
            <div class="info-row">
                <div class="info-label">Nombre</div>
                <div class="info-value"><asp:Label ID="lblNombre" runat="server" /></div>
            </div>
            <div class="info-row">
                <div class="info-label">Email</div>
                <div class="info-value"><asp:Label ID="lblEmail" runat="server" /></div>
            </div>
            <div class="info-row">
                <div class="info-label">Rol</div>
                <div class="info-value"><asp:Label ID="lblRol" runat="server" /></div>
            </div>
            <div class="info-row">
                <div class="info-label">Fecha de Nacimiento</div>
                <div class="info-value"><asp:Label ID="lblFechaNacimiento" runat="server" /></div>
            </div>
            <asp:Panel ID="pnlSaldoRow" runat="server">
                <div class="info-row">
                    <div class="info-label">Saldo</div>
                    <div class="info-value">
                        <span class="saldo-value"><asp:Label ID="lblSaldo" runat="server" /></span>
                    </div>
                </div>
            </asp:Panel>
        </div>

        <div class="botones-card">
            <div class="botones-izq">
                <asp:Panel ID="pnlMisJugadores" runat="server">
                    <asp:Button ID="btnMisJugadores" runat="server" Text="Mis Jugadores"
                        OnClick="btnMisJugadores_Click" CssClass="btn-jugadores" />
                </asp:Panel>
                <asp:Panel ID="pnlSumarFondos" runat="server">
                    <asp:Button ID="btnSumarFondos" runat="server" Text="Añadir Saldo"
                        OnClick="btnSumarFondos_Click" CssClass="btn-saldo" />
                </asp:Panel>
            </div>
            <div class="botones-der">
                <asp:Button ID="btnEditarPerfil" runat="server" Text="Editar Perfil"
                    OnClick="btnEditarPerfil_Click" CssClass="btn-editar" />
                <asp:Button ID="btnCerrarSesion" runat="server" Text="Cerrar Sesión"
                    OnClick="btnCerrarSesion_Click" CssClass="btn-cerrar" />
                <asp:Button ID="btnEliminarCuenta" runat="server" Text="Eliminar Cuenta"
                    OnClick="btnEliminarCuenta_Click" CssClass="btn-eliminar"
                    OnClientClick="return confirm('¿Estás seguro de que quieres eliminar tu cuenta? Esta acción no se puede deshacer.');" />
            </div>
        </div>
    </div>
</asp:Content>