using hada_ProyectoGrupo.Library.EN;
using System;
using System.Collections.Generic;
using System.Data;
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
            DataSet ds = en.LeerAccesoDesconectado();
            DataTable dt = ds.Tables[0];
            DataView dv = new DataView(dt);
            List<string> filtros = new List<string>();

            if (!string.IsNullOrEmpty(nivel))
            {
                filtros.Add("profesional = " + nivel);
            }

            if (precioMin.HasValue)
            {
                filtros.Add("precioInscripcion >= " + precioMin.Value.ToString(System.Globalization.CultureInfo.InvariantCulture));
            }

            if (precioMax.HasValue)
            {
                filtros.Add("precioInscripcion <= " + precioMax.Value.ToString(System.Globalization.CultureInfo.InvariantCulture));
            }

            if (!string.IsNullOrEmpty(ubicacion))
            {
                filtros.Add("ubicacion LIKE '%" + ubicacion.Replace("'", "''") + "%'");
            }

            if (filtros.Count > 0)
            {
                dv.RowFilter = string.Join(" AND ", filtros);
            }

            rptTorneos.DataSource = dv;
            rptTorneos.DataBind();
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(1000);

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
            System.Threading.Thread.Sleep(1000);

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