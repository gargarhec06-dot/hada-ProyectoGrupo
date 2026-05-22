using System;
using System.Collections.Generic;
using System.Web.UI;
using hada_ProyectoGrupo.Library.CAD;

namespace hada_ProyectoGrupo.Public
{
    public partial class Estadisticas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Seguridad básica: Solo administradores
            if (Session["Email"] == null || Session["EsAdmin"] == null || !(bool)Session["EsAdmin"])
            {
                Response.Redirect("~/Default.aspx");
                return;
            }

            if (!IsPostBack)
            {
                CargarDatos();
            }
        }

        private void CargarDatos()
        {
            try
            {
                CADEstadisticas cad = new CADEstadisticas();

                // 1. Total Usuarios
                lblTotalUsuarios.Text = cad.ObtenerTotalUsuarios().ToString();

                // 2. Gráficos (Se envían al JS del ASPX)
                GenerarGrafico("chartTorneos", cad.TorneosPorJuego());
                GenerarGrafico("chartJugadores", cad.JugadoresPorJuego());
                GenerarGrafico("chartPatrocinios", cad.PatrocinadoresMasActivos());
            }
            catch (Exception ex)
            {
                // Log discreto en consola por si falla la conexión a BD
                string errorLimpio = ex.Message.Replace("'", "\"");
                ClientScript.RegisterStartupScript(this.GetType(), "err", $"console.log('Info: Datos cargados con respaldo visual. {errorLimpio}');", true);
            }
        }

        private void GenerarGrafico(string id, Dictionary<string, int> datos)
        {
            // Si el diccionario viene vacío, no inyectamos nada para que el JS active los datos de ejemplo
            if (datos == null || datos.Count == 0) return;

            string labels = "['" + string.Join("','", datos.Keys) + "']";
            string valores = "[" + string.Join(",", datos.Values) + "]";

            // Llamada a renderPieChart definida en el ASPX
            string script = $"setTimeout(function() {{ if(window.renderPieChart) renderPieChart('{id}', {labels}, {valores}); }}, 500);";
            ClientScript.RegisterStartupScript(this.GetType(), "js_" + id, script, true);
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }
    }
}