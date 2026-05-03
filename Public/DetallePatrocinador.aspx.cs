using hada_ProyectoGrupo.Library.CAD;
using hada_ProyectoGrupo.Library.EN;
using System;
using System.Collections.Generic;

namespace hada_ProyectoGrupo.Public
{
    // Clase auxiliar para mostrar torneos del patrocinador
    public class TorneoPatrocinador
    {
        public string NombreTorneo { get; set; }
        public decimal Cantidad { get; set; }
    }

    public partial class DetallePatrocinador : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    int id = int.Parse(Request.QueryString["id"]);
                    CargarPatrocinador(id);
                    CargarTorneos(id);
                }
                else
                {
                    Response.Redirect("~/Public/Patrocinadores.aspx");
                }
                if (Session["EsAdmin"] != null && (bool)Session["EsAdmin"] == true)
                {
                    pnlAdmin.Visible = true;
                }

            }
        }

        private void CargarPatrocinador(int id)
        {
            // Datos de ejemplo, se sustituirá por BD más adelante
            ENPatrocinador p = new ENPatrocinador();
            p.Nombre = "Red Bull";
            p.Email = "redbull@email.com";
            p.PaginaWeb = "https://www.redbull.com";
            p.InicioContrato = DateTime.Now;
            p.FinContrato = DateTime.Now.AddYears(1);
            p.Activo = true;

            lblNombre.Text = p.Nombre;
            lblEmail.Text = p.Email;
            hlWeb.Text = p.PaginaWeb;
            hlWeb.NavigateUrl = p.PaginaWeb;
            lblInicioContrato.Text = p.InicioContrato.ToShortDateString();
            lblFinContrato.Text = p.FinContrato.ToShortDateString();
            lblActivo.Text = p.Activo ? "Activo" : "Inactivo";
        }

        private void CargarTorneos(int id)
        {
            // Datos de ejemplo, se sustituirá por BD más adelante
            List<TorneoPatrocinador> torneos = new List<TorneoPatrocinador>
            {
                new TorneoPatrocinador { NombreTorneo = "Valorant Cup 2026", Cantidad = 5000 },
                new TorneoPatrocinador { NombreTorneo = "FIFA Championship", Cantidad = 3000 }
            };
            rptTorneos.DataSource = torneos;
            rptTorneos.DataBind();
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Public/Patrocinadores.aspx");
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            // Response.Redirect($"~/Private/EditarPatrocinador.aspx?id={idPatrocinador}"); para editar el patrocinador se debe tener la BD primero 
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            // Aquí se implementaría la lógica para eliminar el patrocinador de la base de datos
            Response.Redirect("~/Public/Patrocinadores.aspx");
        }
    }
}