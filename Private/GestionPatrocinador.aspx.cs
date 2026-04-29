using System;
using hada_ProyectoGrupo.Library.EN;

namespace hada_ProyectoGrupo.Private
{
    public partial class GestionPatrocinador : System.Web.UI.Page
    {
        private int idPatrocinador = 0;
        private bool esNuevo = true;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Verificar que es administrador
            if (Session["EsAdmin"] == null || (bool)Session["EsAdmin"] == false)
            {
                Response.Redirect("~/Public/Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    idPatrocinador = int.Parse(Request.QueryString["id"]);
                    esNuevo = false;
                    tituloPagina.InnerText = "Editar Patrocinador";
                    CargarPatrocinador(idPatrocinador);
                }
                else
                {
                    tituloPagina.InnerText = "Nuevo Patrocinador";
                }
            }
        }

        private void CargarPatrocinador(int id)
        {
            // TODO: Implementar con PatrocinadorCAD cuando esté listo
            // Datos de ejemplo
            txtNombre.Text = "Red Bull";
            txtEmail.Text = "redbull@email.com";
            txtWeb.Text = "https://www.redbull.com";
            txtInicioContrato.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtFinContrato.Text = DateTime.Now.AddYears(1).ToString("yyyy-MM-dd");
            chkActivo.Checked = true;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            // TODO: Implementar con PatrocinadorCAD cuando esté listo
            lblMensaje.Text = esNuevo ? "Patrocinador creado (ejemplo)" : "Patrocinador actualizado (ejemplo)";

            // Redirigir a la lista después de guardar
            Response.Redirect("~/Public/Patrocinadores.aspx");
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Public/Patrocinadores.aspx");
        }
    }
}