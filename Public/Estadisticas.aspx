<%@ Page Title="Panel de Estadísticas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Estadisticas.aspx.cs" Inherits="hada_ProyectoGrupo.Public.Estadisticas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
    <style>
        .stats-container { padding: 40px; background-color: rgba(15, 15, 26, 0.85); color: white; min-height: 100vh; backdrop-filter: blur(8px); }
        .stat-card-big { background: rgba(26, 26, 46, 0.9); border: 1px solid #00e5ff33; border-radius: 15px; padding: 30px; text-align: center; margin-bottom: 40px; }
        .stat-number { font-size: 3.5rem; font-weight: 800; color: #00e5ff; text-shadow: 0 0 20px rgba(0, 229, 255, 0.5); }
        .charts-grid { display: grid; grid-template-columns: repeat(auto-fit, minmax(320px, 1fr)); gap: 25px; }
        .chart-box { background: rgba(26, 26, 46, 0.9); border: 1px solid rgba(0, 229, 255, 0.15); border-radius: 12px; padding: 25px; }
        .chart-title { color: #00e5ff; text-transform: uppercase; letter-spacing: 2px; font-size: 0.85rem; margin-bottom: 20px; text-align: center; font-weight: bold; }
        canvas { max-height: 280px; margin: 0 auto; }
        .top-news-table { width: 100%; border-collapse: collapse; }
        .top-news-table th { color: #ff0055; text-transform: uppercase; font-size: 0.75rem; border-bottom: 2px solid rgba(255, 0, 85, 0.2); padding: 10px; text-align: left; }
        .top-news-table td { padding: 15px 10px; border-bottom: 1px solid rgba(255, 255, 255, 0.05); font-size: 0.9rem; }
        .badge-likes { background: linear-gradient(45deg, #ff0055, #ff4d88); color: white; padding: 4px 10px; border-radius: 6px; font-weight: bold; }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="stats-container">
        <div class="container">
            <h2 class="mb-1">📊 PANEL DE CONTROL</h2>
            <p class="text-muted mb-4">ESTADÍSTICAS GLOBALES (ACCESO ADMINISTRADOR)</p>
            
            <div class="stat-card-big">
                <div style="text-transform: uppercase; letter-spacing: 3px; color: #888; font-size: 0.9rem;">Comunidad Total</div>
                <div class="stat-number">
                    <asp:Label ID="lblTotalUsuarios" runat="server" Text="0" />
                </div>
            </div>

            <div class="charts-grid">
                <div class="chart-box"><div class="chart-title">Torneos por Videojuego</div><canvas id="chartTorneos"></canvas></div>
                <div class="chart-box"><div class="chart-title">Distribución de Jugadores</div><canvas id="chartJugadores"></canvas></div>
                <div class="chart-box"><div class="chart-title">Actividad de Patrocinadores</div><canvas id="chartPatrocinios"></canvas></div>
                <div class="chart-box">
                    <div class="chart-title" style="color:#ff0055;">🔥 Noticias Populares</div>
                    <asp:Repeater ID="rptTopNoticias" runat="server">
                        <HeaderTemplate><table class="top-news-table"><thead><tr><th>Noticia</th><th style="text-align:right;">Likes</th></tr></thead></HeaderTemplate>
                        <ItemTemplate><tr><td><%# Eval("titulo") %></td><td style="text-align:right;"><span class="badge-likes"><%# Eval("Likes") %> ❤</span></td></tr></ItemTemplate>
                        <FooterTemplate></table></FooterTemplate>
                    </asp:Repeater>
                </div>
            </div>
            <div class="text-center mt-5">
                <asp:Button ID="btnVolver" runat="server" Text="← VOLVER AL INICIO" OnClick="btnVolver_Click" CssClass="btn btn-outline-info" />
            </div>
        </div>
    </div>

    <script>
        function renderPieChart(canvasId, labelArray, dataArray) {
            const ctx = document.getElementById(canvasId).getContext('2d');
            new Chart(ctx, {
                type: 'pie',
                data: {
                    labels: labelArray,
                    datasets: [{
                        data: dataArray,
                        backgroundColor: ['#00e5ffcc', '#ff0055cc', '#00ff88cc', '#ffaa00cc', '#9d00ffcc'],
                        borderColor: '#0f0f1a',
                        borderWidth: 2
                    }]
                },
                options: { responsive: true, maintainAspectRatio: false, plugins: { legend: { position: 'bottom', labels: { color: '#aaa' } } } }
            });
        }
    </script>
</asp:Content>