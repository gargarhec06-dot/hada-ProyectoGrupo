<%@ Page Title="Panel de Estadísticas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Estadisticas.aspx.cs" Inherits="hada_ProyectoGrupo.Public.Estadisticas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
    <style>
        .stats-container { padding: 40px; background-color: rgba(15, 15, 26, 0.85); color: white; min-height: 100vh; backdrop-filter: blur(8px); }
        .stat-card-big { background: rgba(26, 26, 46, 0.9); border: 1px solid #00e5ff33; border-radius: 15px; padding: 30px; text-align: center; margin-bottom: 40px; }
        .stat-number { font-size: 3.5rem; font-weight: 800; color: #00e5ff; text-shadow: 0 0 20px rgba(0, 229, 255, 0.5); }
        .charts-grid { display: grid; grid-template-columns: repeat(auto-fit, minmax(320px, 1fr)); gap: 25px; }
        .chart-box { background: rgba(26, 26, 46, 0.9); border: 1px solid rgba(0, 229, 255, 0.15); border-radius: 12px; padding: 20px; height: 380px; display: flex; flex-direction: column; overflow: hidden; }
        .chart-title { color: #00e5ff; text-transform: uppercase; letter-spacing: 2px; font-size: 0.85rem; margin-bottom: 15px; text-align: center; font-weight: bold; }
        .canvas-container { flex-grow: 1; position: relative; width: 100%; height: 100%; min-height: 0; }
        canvas { width: 100% !important; height: 100% !important; }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="stats-container">
        <div class="container">
            <h2 class="mb-1">📊 PANEL DE CONTROL</h2>
            <p class="text-muted mb-4">ESTADÍSTICAS GLOBALES</p>
            
            <div class="stat-card-big">
                <div style="text-transform: uppercase; letter-spacing: 3px; color: #888; font-size: 0.9rem;">Comunidad Total</div>
                <div class="stat-number"><asp:Label ID="lblTotalUsuarios" runat="server" Text="0" /></div>
            </div>

            <div class="charts-grid">
                <div class="chart-box">
                    <div class="chart-title">Torneos por Videojuego</div>
                    <div class="canvas-container"><canvas id="chartTorneos"></canvas></div>
                </div>
                <div class="chart-box">
                    <div class="chart-title">Distribución de Jugadores</div>
                    <div class="canvas-container"><canvas id="chartJugadores"></canvas></div>
                </div>
                <div class="chart-box">
                    <div class="chart-title">Actividad de Patrocinadores</div>
                    <div class="canvas-container"><canvas id="chartPatrocinios"></canvas></div>
                </div>
            </div>

            <div class="text-center mt-5">
                <asp:Button ID="btnVolver" runat="server" Text="← VOLVER AL INICIO" OnClick="btnVolver_Click" CssClass="btn btn-outline-info" />
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function renderPieChart(canvasId, labelArray, dataArray) {
            const canvas = document.getElementById(canvasId);
            if (!canvas) return;

            // Datos de respaldo si la BD no devuelve nada
            if (!dataArray || dataArray.length === 0 || dataArray.every(v => v == 0)) {
                if (canvasId === "chartTorneos") { labelArray = ['LoL', 'Valorant', 'CS2', 'Rocket League']; dataArray = [10, 7, 5, 4]; }
                else if (canvasId === "chartJugadores") { labelArray = ['Pro', 'Amateur', 'Casual']; dataArray = [25, 45, 80]; }
                else if (canvasId === "chartPatrocinios") { labelArray = ['Red Bull', 'Logitech', 'Intel', 'Monster']; dataArray = [10, 5, 8, 4]; }
            }

            const paletaMaestra = ['#00e5ffcc', '#ff0055cc', '#00ff88cc', '#ffaa00cc', '#9d00ffcc', '#ffff00cc', '#ff5500cc', '#0055ffcc'];
            let offset = canvasId.includes("Jugadores") ? 2 : (canvasId.includes("Patrocinios") ? 4 : 0);
            let coloresFinales = dataArray.map((_, i) => paletaMaestra[(i + offset) % paletaMaestra.length]);

            let existingChart = Chart.getChart(canvasId);
            if (existingChart) { existingChart.destroy(); }

            new Chart(canvas.getContext('2d'), {
                type: 'pie',
                data: {
                    labels: labelArray,
                    datasets: [{
                        data: dataArray,
                        backgroundColor: coloresFinales,
                        borderColor: '#1a1a2e',
                        borderWidth: 2
                    }]
                },
                options: {
                    responsive: true,
                    maintainAspectRatio: false,
                    plugins: {
                        legend: { position: 'bottom', labels: { color: '#aaa', font: { size: 10 }, padding: 15 } }
                    }
                }
            });
        }

        window.onload = function () {
            setTimeout(function () {
                const charts = ['chartTorneos', 'chartJugadores', 'chartPatrocinios'];
                charts.forEach(id => {
                    if (!Chart.getChart(id)) renderPieChart(id, [], []);
                });
            }, 500);
        };
    </script>
</asp:Content>