<%@ Page Title="Detalle Patrocinador" Language="C#" MasterPageFile="~/Site.Master" 
    AutoEventWireup="true" CodeFile="DetallePatrocinador.aspx.cs" 
    Inherits="hada_ProyectoGrupo.Public.DetallePatrocinador" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .detalle-wrapper {
            max-width: 800px;
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
            margin-bottom: 30px;
            box-shadow: 0 0 30px #7b2fff11, inset 0 0 30px #00e5ff05;
            position: relative;
            overflow: hidden;
        }

        /* Esquina decorativa */
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
            width: 160px;
            min-width: 160px;
            background: linear-gradient(to right, #00e5ff, #7b2fff);
            -webkit-background-clip: text;
            -webkit-text-fill-color: transparent;
        }

        .info-value {
            color: #e0e0e0;
            font-size: 0.95rem;
        }

        .info-value a {
            color: #00e5ff;
            text-decoration: none;
        }

        .info-value a:hover { 
            color: #7b2fff;
            text-decoration: underline; 
        }

        .section-title {
            font-size: 1rem;
            font-weight: 700;
            text-transform: uppercase;
            letter-spacing: 2px;
            margin-bottom: 15px;
            background: linear-gradient(to right, #00e5ff, #7b2fff);
            -webkit-background-clip: text;
            -webkit-text-fill-color: transparent;
        }

        .torneos-table {
            width: 100%;
            border-collapse: collapse;
            background: linear-gradient(135deg, #1a1a2e 60%, #1a0f2e 100%);
            border: 1px solid #7b2fff44;
            border-radius: 10px;
            overflow: hidden;
            margin-bottom: 30px;
            box-shadow: 0 0 20px #7b2fff11;
        }

        .torneos-table th {
            background: linear-gradient(to right, #0f0f1a, #1a0f2e);
            font-size: 0.7rem;
            text-transform: uppercase;
            letter-spacing: 2px;
            padding: 14px 16px;
            text-align: left;
            border-bottom: 1px solid #7b2fff44;
            background-clip: unset;
            color: #00e5ff;
        }

        .torneos-table th:last-child { color: #7b2fff; }

        .torneos-table td {
            padding: 12px 16px;
            color: #e0e0e0;
            border-bottom: 1px solid #7b2fff11;
            font-size: 0.9rem;
        }

        .torneos-table tr:last-child td { border-bottom: none; }

        .torneos-table tr:hover td { 
            background: linear-gradient(to right, #00e5ff08, #7b2fff08);
        }

        .cantidad-badge {
            background: linear-gradient(to right, #00e5ff, #7b2fff);
            -webkit-background-clip: text;
            -webkit-text-fill-color: transparent;
            font-weight: 700;
        }

        .botones-row {
            display: flex;
            gap: 12px;
            align-items: center;
            flex-wrap: wrap;
        }

        .btn-volver {
            background-color: transparent;
            color: #888;
            border: 1px solid #444;
            padding: 10px 22px;
            border-radius: 6px;
            font-weight: 700;
            text-transform: uppercase;
            letter-spacing: 1px;
            font-size: 0.8rem;
            cursor: pointer;
            transition: all 0.2s;
        }

        .btn-volver:hover {
            border-color: #888;
            color: #e0e0e0;
        }

        .btn-editar {
            background-color: transparent;
            color: #00e5ff;
            border: 2px solid #00e5ff;
            padding: 10px 22px;
            border-radius: 6px;
            font-weight: 700;
            text-transform: uppercase;
            letter-spacing: 1px;
            font-size: 0.8rem;
            cursor: pointer;
            transition: all 0.2s;
            box-shadow: 0 0 10px #00e5ff22;
        }

        .btn-editar:hover {
            background: linear-gradient(to right, #00e5ff, #7b2fff);
            border-color: transparent;
            color: #ffffff;
            box-shadow: 0 0 20px #7b2fff44;
        }

        .btn-eliminar {
            background-color: transparent;
            color: #ff4444;
            border: 2px solid #ff4444;
            padding: 10px 22px;
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
    <div class="detalle-wrapper">

        <div class="page-title">Detalle del Patrocinador</div>

        <div class="info-card">
            <div class="info-row">
                <div class="info-label">Nombre</div>
                <div class="info-value"><asp:Label ID="lblNombre" runat="server"/></div>
            </div>
            <div class="info-row">
                <div class="info-label">Email</div>
                <div class="info-value"><asp:Label ID="lblEmail" runat="server"/></div>
            </div>
            <div class="info-row">
                <div class="info-label">Página Web</div>
                <div class="info-value"><asp:HyperLink ID="hlWeb" runat="server"/></div>
            </div>
            <div class="info-row">
                <div class="info-label">Inicio Contrato</div>
                <div class="info-value"><asp:Label ID="lblInicioContrato" runat="server"/></div>
            </div>
            <div class="info-row">
                <div class="info-label">Fin Contrato</div>
                <div class="info-value"><asp:Label ID="lblFinContrato" runat="server"/></div>
            </div>
            <div class="info-row">
                <div class="info-label">Teléfono</div>
                <div class="info-value"><asp:Label ID="lbltelefono" runat="server"/></div>
            </div>
        </div>

        <div class="section-title">Torneos patrocinados</div>

        <asp:Repeater ID="rptTorneos" runat="server">
            <HeaderTemplate>
                <table class="torneos-table">
                    <tr>
                        <th>Torneo</th>
                        <th>Cantidad aportada</th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td><%# Eval("NombreTorneo") %></td>
                    <td><span class="cantidad-badge"><%# Eval("Cantidad") %> €</span></td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>

        <div class="botones-row">
            <asp:Button ID="btnVolver" runat="server" Text="Volver" 
                OnClick="btnVolver_Click" CssClass="btn-volver" />
            <asp:Panel ID="pnlAdmin" runat="server" Visible="false" style="display:contents;">
                <asp:Button ID="btnEditar" runat="server" Text="Editar" 
                    OnClick="btnEditar_Click" CssClass="btn-editar" />
                <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" 
                    OnClick="btnEliminar_Click" 
                    OnClientClick="return confirm('¿Estás seguro de eliminar este patrocinador?');"
                    CssClass="btn-eliminar" />
            </asp:Panel>
        </div>

    </div>
</asp:Content>