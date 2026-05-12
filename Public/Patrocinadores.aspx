<%@ Page Title="Patrocinadores" Language="C#" MasterPageFile="~/Site.Master" 
    AutoEventWireup="true" CodeFile="Patrocinadores.aspx.cs" 
    Inherits="hada_ProyectoGrupo.Public.Patrocinadores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .page-wrapper {
            display: flex;
            min-height: calc(100vh - 61px);
            position: relative;
        }

        /* ── Columnas laterales ── */
        .side-panel {
            width: 160px;
            min-width: 160px;
            display: flex;
            flex-direction: column;
            align-items: center;
            padding-top: 40px;
            gap: 20px;
            position: relative;
        }

        .side-line {
            position: absolute;
            top: 0;
            bottom: 0;
            width: 1px;
            background: linear-gradient(to bottom, transparent, #00e5ff, transparent);
            box-shadow: 0 0 8px #00e5ff;
        }

        .side-panel.left .side-line { right: 0; }
        .side-panel.right .side-line { left: 0; }

        .hud-block {
            background-color: #1a1a2e;
            border: 1px solid #00e5ff33;
            border-radius: 8px;
            padding: 10px 14px;
            width: 130px;
            text-align: center;
        }

        .hud-block .hud-label {
            font-size: 0.6rem;
            color: #00e5ff;
            text-transform: uppercase;
            letter-spacing: 2px;
            margin-bottom: 4px;
        }

        .hud-block .hud-value {
            font-size: 1.1rem;
            font-weight: 700;
            color: #ffffff;
        }

        .hud-graph-container {
            background-color: #1a1a2e;
            border: 1px solid #00e5ff33;
            border-radius: 8px;
            padding: 10px;
            width: 130px;
            height: 70px;
            overflow: hidden;
            position: relative;
        }

        .hud-graph-container .hud-label {
            position: relative;
            z-index: 2;
            margin-bottom: 0;
            font-size: 0.55rem;
            color: #00e5ff;
            text-transform: uppercase;
            letter-spacing: 2px;
        }

        .wave-svg {
            position: absolute;
            bottom: 5px;
            left: 0;
            width: 200%;
            height: 40px;
            stroke: #00e5ff;
            stroke-width: 2;
            fill: none;
            filter: drop-shadow(0 0 4px #00e5ff);
            animation: wave-move 3s linear infinite;
        }

        .wave-svg-slow {
            position: absolute;
            bottom: 5px;
            left: 0;
            width: 200%;
            height: 40px;
            stroke: #00e5ff;
            stroke-width: 2;
            fill: none;
            filter: drop-shadow(0 0 4px #00e5ff);
            animation: wave-move 5s linear infinite;
        }

        @keyframes wave-move {
            0% { transform: translateX(0); }
            100% { transform: translateX(-50%); }
        }

        .grid-bg {
            position: absolute;
            top: 0; left: 0; width: 100%; height: 100%;
            background-image: 
                linear-gradient(rgba(0, 229, 255, 0.1) 1px, transparent 1px),
                linear-gradient(90deg, rgba(0, 229, 255, 0.1) 1px, transparent 1px);
            background-size: 10px 10px;
        }

        .hud-graph-footer {
            font-size: 0.55rem;
            color: #00e5ff;
            text-align: right;
            margin-top: 2px;
            position: relative;
            z-index: 2;
        }

        .hud-corner {
            width: 130px;
            height: 60px;
            position: relative;
        }

        .hud-corner::before,
        .hud-corner::after {
            content: '';
            position: absolute;
            background-color: #00e5ff;
        }

        .hud-corner::before {
            width: 30px;
            height: 1px;
            top: 0; left: 0;
            box-shadow: 0 0 6px #00e5ff;
        }

        .hud-corner::after {
            width: 1px;
            height: 30px;
            top: 0; left: 0;
            box-shadow: 0 0 6px #00e5ff;
        }

        .hud-dot {
            width: 6px;
            height: 6px;
            background-color: #00e5ff;
            border-radius: 50%;
            box-shadow: 0 0 8px #00e5ff;
            margin: 5px auto;
            animation: pulse 2s infinite;
        }

        @keyframes pulse {
            0%, 100% { opacity: 1; box-shadow: 0 0 8px #00e5ff; }
            50% { opacity: 0.4; box-shadow: 0 0 3px #00e5ff; }
        }

        /* ── Contenido central ── */
        .patrocinadores-container {
            flex: 1;
            max-width: 900px;
            margin: 40px auto;
            padding: 0 20px;
        }

        .page-title {
            font-size: 2rem;
            font-weight: 700;
            color: #00e5ff;
            text-transform: uppercase;
            letter-spacing: 3px;
            border-left: 4px solid #00e5ff;
            padding-left: 15px;
            margin-bottom: 30px;
            text-shadow: 0 0 10px #00e5ff44;
        }

        .filtros-panel {
            background-color: #1a1a2e;
            border: 1px solid #00e5ff33;
            border-radius: 10px;
            padding: 20px 25px;
            margin-bottom: 30px;
        }

        .filtros-panel label {
            color: #00e5ff;
            font-size: 0.85rem;
            text-transform: uppercase;
            letter-spacing: 1px;
            margin-bottom: 5px;
            display: block;
        }

        .filtros-panel input[type=text],
        .filtros-panel select {
            background-color: #0f0f1a !important;
            border: 1px solid #00e5ff55 !important;
            color: #e0e0e0 !important;
            border-radius: 6px;
        }

        .filtros-panel input[type=text]:focus,
        .filtros-panel select:focus {
            outline: none;
            border-color: #00e5ff !important;
            box-shadow: 0 0 8px #00e5ff44 !important;
        }

        .btn-filtrar {
            background-color: #00e5ff;
            color: #0f0f1a;
            border: none;
            padding: 8px 20px;
            border-radius: 6px;
            font-weight: 700;
            text-transform: uppercase;
            letter-spacing: 1px;
            cursor: pointer;
            transition: background-color 0.2s;
        }

        .btn-filtrar:hover { background-color: #00b8cc; }

        .btn-limpiar {
            background-color: transparent;
            color: #00e5ff;
            border: 1px solid #00e5ff;
            padding: 8px 20px;
            border-radius: 6px;
            font-weight: 700;
            text-transform: uppercase;
            letter-spacing: 1px;
            cursor: pointer;
            margin-left: 10px;
            transition: all 0.2s;
        }

        .btn-limpiar:hover { background-color: #00e5ff22; }

        .patrocinador-card {
            background-color: #1a1a2e;
            border: 1px solid #00e5ff22;
            border-radius: 10px;
            padding: 18px 25px;
            margin-bottom: 15px;
            transition: border-color 0.2s, transform 0.2s;
        }

        .patrocinador-card:hover {
            border-color: #00e5ff;
            transform: translateX(5px);
        }

        .patrocinador-card a {
            color: #ffffff;
            font-size: 1.1rem;
            font-weight: 600;
            text-decoration: none;
            letter-spacing: 1px;
        }

        .patrocinador-card a:hover { color: #00e5ff; }

        .btn-crear {
            background-color: transparent;
            color: #00e5ff;
            border: 2px solid #00e5ff;
            padding: 10px 25px;
            border-radius: 6px;
            font-weight: 700;
            text-transform: uppercase;
            letter-spacing: 1px;
            cursor: pointer;
            transition: all 0.2s;
            margin-top: 20px;
        }

        .btn-crear:hover {
            background-color: #00e5ff;
            color: #0f0f1a;
        }

        .lbl-resultado {
            color: #888;
            font-size: 0.9rem;
            margin-bottom: 15px;
            display: block;
        }

        @media (max-width: 768px) {
            .side-panel { display: none; }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-wrapper">

        <!-- Panel izquierdo -->
        <div class="side-panel left">
            <div class="side-line"></div>
            <div class="hud-corner"></div>
            <div class="hud-dot"></div>
            <div class="hud-block">
                <div class="hud-label">Sistema</div>
                <div class="hud-value">ONLINE</div>
            </div>
            <div class="hud-dot"></div>
            <div class="hud-graph-container">
                <div class="hud-label">Contratos</div>
                <div class="grid-bg"></div>
                <svg class="wave-svg" viewBox="0 0 200 40" preserveAspectRatio="none">
                    <path d="M0,20 Q10,5 20,20 T40,20 T60,20 T80,20 T100,20 T120,20 T140,20 T160,20 T180,20 T200,20" />
                </svg>
            </div>
            <div class="hud-graph-footer">ACTIVOS: --</div>
            <div class="hud-dot"></div>
            <div class="hud-block">
                <div class="hud-label">Estado</div>
                <div class="hud-value" style="color:#00e5ff; font-size:0.8rem;">ACTIVO</div>
            </div>
        </div>

        <!-- Contenido central -->
        <div class="patrocinadores-container">
            <div class="page-title">Patrocinadores</div>

            <div class="filtros-panel">
                <div class="row">
                    <div class="col-md-6">
                        <label>Buscar por nombre</label>
                        <asp:TextBox ID="txtNombre" runat="server" placeholder="Ej: Samsung" CssClass="form-control" />
                    </div>
                    <div class="col-md-6">
                        <label>Filtrar por torneo</label>
                        <asp:DropDownList ID="ddlTorneo" runat="server" CssClass="form-select" />
                    </div>
                </div>
                <div style="margin-top: 10px;">
                    <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" 
                        OnClick="btnFiltrar_Click" CssClass="btn-filtrar" />
                    <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" 
                        OnClick="btnLimpiar_Click" CssClass="btn-limpiar" />
                </div>
            </div>

            <asp:Label ID="lblResultado" runat="server" CssClass="lbl-resultado" />

            <asp:Repeater ID="rptPatrocinadores" runat="server">
                <ItemTemplate>
                    <div class="patrocinador-card">
                        <asp:HyperLink runat="server" 
                            NavigateUrl='<%# "~/Public/DetallePatrocinador.aspx?id=" + Eval("IdPatrocinador") %>'
                            Text='<%# Eval("Nombre") %>'/>
                    </div>
                </ItemTemplate>
            </asp:Repeater>

            <asp:Panel ID="pnlAdmin" runat="server" Visible="false">
                <asp:Button ID="btnCrear" runat="server" Text="+ Crear Nuevo Patrocinador" 
                    OnClick="btnCrear_Click" CssClass="btn-crear" />
            </asp:Panel>
        </div>

        <!-- Panel derecho -->
        <div class="side-panel right">
            <div class="side-line"></div>
            <div class="hud-corner" style="transform: scaleX(-1);"></div>
            <div class="hud-dot"></div>
            <div class="hud-block">
                <div class="hud-label">Red</div>
                <div class="hud-value" style="font-size:0.85rem;">SEGURA</div>
            </div>
            <div class="hud-dot"></div>
            <div class="hud-graph-container">
                <div class="hud-label">Inversión</div>
                <div class="grid-bg"></div>
                <svg class="wave-svg-slow" viewBox="0 0 200 40" preserveAspectRatio="none">
                    <path d="M0,25 Q15,10 30,25 T60,25 T90,25 T120,25 T150,25 T180,25 T200,25" />
                </svg>
            </div>
            <div class="hud-graph-footer">GLOBAL: --</div>
            <div class="hud-dot"></div>
            <div class="hud-block">
                <div class="hud-label">Versión</div>
                <div class="hud-value" style="font-size:0.85rem;">v1.0.0</div>
            </div>
        </div>

    </div>
</asp:Content>