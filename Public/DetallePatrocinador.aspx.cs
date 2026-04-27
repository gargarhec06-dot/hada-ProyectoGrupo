using System;
using hada_ProyectoGrupo.Library.EN;

namespace hada_ProyectoGrupo.Public
{
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
                }
                else
                {
                    Response.Redirect("~/Public/Patrocinadores.aspx");
                }
            }
        }

        private void CargarPatrocinador(int id)
        {
            // De momento datos de ejemplo, se implementará con la BD más adelante
            ENPatrocinador p = new ENPatrocinador();
            p.IdPatrocinador = id;
            p.Nombre = "Patrocinador ejemplo";
            p.Email = "ejemplo@email.com";
            p.PaginaWeb = "https://www.ejemplo.com";
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

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Public/Patrocinadores.aspx");
        }
    }
}