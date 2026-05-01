using System;

namespace hada_ProyectoGrupo.Public
{
    public partial class DetallesNoticia : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarDetalle();
                if (Session["EsAdmin"] != null && (bool)Session["EsAdmin"] == true)
                {
                    pnlAdmin.Visible = true;
                }
            }
        }

        private void CargarDetalle()
        {
            string idUrl = Request.QueryString["id"] ?? "1";

            if (idUrl == "1")
            {
                lblTitulo.Text = "¡Nuevo Torneo!";
                lblContenido.Text = "Se ha abierto el plazo de inscripción para el gran torneo anual. Compite contra los mejores y gana premios exclusivos.";
                imgNoticia.ImageUrl = "https://images.unsplash.com/photo-1542751371-adc38448a05e?auto=format&fit=crop&w=800&q=80";
            }
            else if (idUrl == "2")
            {
                lblTitulo.Text = "Actualización de Sistema";
                lblContenido.Text = "Hemos implementado mejoras en los servidores para reducir el lag y optimizar la experiencia de usuario en la plataforma.";
                imgNoticia.ImageUrl = "https://images.unsplash.com/photo-1550745165-9bc0b252726f?auto=format&fit=crop&w=800&q=80";
            }
            else
            {
                lblTitulo.Text = "Resultados Finales";
                lblContenido.Text = "Tras una jornada intensa, ya tenemos los resultados de las clasificatorias. Revisa la tabla para ver quién pasa a la final.";
                imgNoticia.ImageUrl = "https://images.unsplash.com/photo-1511512578047-dfb367046420?auto=format&fit=crop&w=800&q=80";
            }

            lblFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
            lblAutor.Text = "staff@esports.com";
        }

        protected void btnVolver_Click(object sender, EventArgs e) { Response.Redirect("Noticias.aspx"); }
        protected void btnCrear_Click(object sender, EventArgs e) { Response.Redirect("Noticias.aspx"); }
        protected void btnModificar_Click(object sender, EventArgs e) { Response.Redirect("Noticias.aspx"); }
        protected void btnEliminar_Click(object sender, EventArgs e) { Response.Redirect("Noticias.aspx"); }
    }
}