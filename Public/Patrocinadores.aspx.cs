using hada_ProyectoGrupo.Library.CAD;
using hada_ProyectoGrupo.Library.EN;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace hada_ProyectoGrupo.Public
{
    public partial class Patrocinadores : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarTorneos();
                CargarPatrocinadores(null, 0);
            }

            if (Session["EsAdmin"] != null && (bool)Session["EsAdmin"] == true)
            {
                pnlAdmin.Visible = true;
            }
        }

        private void CargarTorneos()
        {
            CADTorneo cadTorneo = new CADTorneo();
            List<ENTorneo> torneos = cadTorneo.ReadAll();
            ddlTorneo.Items.Clear();
            ddlTorneo.Items.Add(new ListItem("Todos los torneos", "0"));
            foreach (ENTorneo t in torneos)
            {
                ddlTorneo.Items.Add(new ListItem(t.Nombre, t.Codigo.ToString()));
            }
        }

        private void CargarPatrocinadores(string nombre, int codigoTorneo)
        {
            CADPatrocinador cad = new CADPatrocinador();
            List<ENPatrocinador> lista = cad.ReadFiltrado(nombre, codigoTorneo);
            rptPatrocinadores.DataSource = lista;
            rptPatrocinadores.DataBind();
            lblResultado.Text = lista.Count == 0 ? "No se encontraron patrocinadores." : "";
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text.Trim();
            int codigoTorneo = int.Parse(ddlTorneo.SelectedValue);
            CargarPatrocinadores(nombre, codigoTorneo);
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtNombre.Text = "";
            ddlTorneo.SelectedIndex = 0;
            CargarPatrocinadores(null, 0);
        }

        protected void btnCrear_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/private/GestionPatrocinador.aspx");
        }
    }
}