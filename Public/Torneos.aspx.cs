using hada_ProyectoGrupo.Library.EN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace hada_ProyectoGrupo.Public
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarTorneos();
                if (Session["EsAdmin"] != null && (bool)Session["EsAdmin"] == true)
                {
                    pnlAdmin.Visible = true;
                }
            }
        }

        private void CargarTorneos(string nivel = null, float? precioMin = null, float? precioMax = null, string ubicacion = null)
        {
            ENTorneo en = new ENTorneo();
            List<ENTorneo> lista = en.ReadAll();

            if (!string.IsNullOrEmpty(nivel))
            {
                bool esProfesional = nivel == "true";
                lista = lista.Where(t => t.Profesional == esProfesional).ToList();
            }

            if (precioMin.HasValue)
                lista = lista.Where(t => t.PrecioInscripcion >= precioMin.Value).ToList();

            if (precioMax.HasValue)
                lista = lista.Where(t => t.PrecioInscripcion <= precioMax.Value).ToList();

            if (!string.IsNullOrEmpty(ubicacion))
                lista = lista.Where(t => t.Ubicacion != null &&
                    t.Ubicacion.IndexOf(ubicacion, StringComparison.OrdinalIgnoreCase) >= 0).ToList();

            rptTorneos.DataSource = lista;
            rptTorneos.DataBind();
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            string nivel = ddlNivel.SelectedValue;

            float? precioMin = null;
            float? precioMax = null;

            if (float.TryParse(txtPrecioMin.Text, out float pMin))
                precioMin = pMin;

            if (float.TryParse(txtPrecioMax.Text, out float pMax))
                precioMax = pMax;

            string ubicacion = txtUbicacion.Text.Trim();

            CargarTorneos(nivel, precioMin, precioMax, ubicacion);

            if (Session["EsAdmin"] != null && (bool)Session["EsAdmin"] == true)
                pnlAdmin.Visible = true;
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            ddlNivel.SelectedIndex = 0;
            txtPrecioMin.Text = "";
            txtPrecioMax.Text = "";
            txtUbicacion.Text = "";
            CargarTorneos();

            if (Session["EsAdmin"] != null && (bool)Session["EsAdmin"] == true)
                pnlAdmin.Visible = true;
        }

        protected void btnCrear_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Private/GestionTorneo.aspx");
        }
    }
}