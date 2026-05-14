using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using hada_ProyectoGrupo.Library.CAD;

namespace hada_ProyectoGrupo.Public
{
    public partial class Estadisticas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (Session["Email"] == null || Session["EsAdmin"] == null || !(bool)Session["EsAdmin"])
            {
                // Si entra aquí, nos devuelve al inicio. 
                Response.Redirect("~/Default.aspx");
                return;
            }

            if (!IsPostBack)
            {
                CargarEstadisticas();
            }
        }

        private void CargarEstadisticas()
        {
            try
            {
                CADEstadisticas cad = new CADEstadisticas();

                // Intentamos obtener el total, si falla ponemos 0
                int total = 0;
                try { total = cad.ObtenerTotalUsuarios(); } catch { }
                lblTotalUsuarios.Text = total.ToString();

                // Carga de gráficos
                RegistrarGrafico("chartTorneos", cad.TorneosPorJuego());
                RegistrarGrafico("chartJugadores", cad.JugadoresPorJuego());
                RegistrarGrafico("chartPatrocinios", cad.PatrocinadoresMasActivos());

                // Carga de repetidor
                rptTopNoticias.DataSource = cad.Top3NoticiasLikes();
                rptTopNoticias.DataBind();
            }
            catch (Exception ex)
            {
                // Si hay un error grave, lo mostramos para saber qué pasa
                Response.Write("<script>alert('Error al cargar datos: " + ex.Message + "');</script>");
            }
        }

        private void RegistrarGrafico(string canvasId, Dictionary<string, int> datos)
        {
            if (datos == null || datos.Count == 0) return;

            string labels = "['" + string.Join("','", datos.Keys) + "']";
            string valores = "[" + string.Join(",", datos.Values) + "]";
            string script = $"window.addEventListener('load', function() {{ renderPieChart('{canvasId}', {labels}, {valores}); }});";
            ClientScript.RegisterStartupScript(this.GetType(), "script_" + canvasId, script, true);
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }
    }
}