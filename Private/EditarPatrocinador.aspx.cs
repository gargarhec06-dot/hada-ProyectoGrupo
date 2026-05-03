using hada_ProyectoGrupo.Library.CAD;
using hada_ProyectoGrupo.Library.EN;
using System;
using System.Web.UI;

namespace hada_ProyectoGrupo.Private
{
    public partial class EditarPatrocinador : System.Web.UI.Page
    {
        private int idPatrocinador = 0;

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
                if (Request.QueryString["id"] != null && int.TryParse(Request.QueryString["id"], out idPatrocinador))
                {
                    CargarPatrocinador(idPatrocinador);
                }
                else
                {
                    // No hay ID, volver a la lista
                    Response.Redirect("~/Public/Patrocinadores.aspx");
                }
            }
        }

        private void CargarPatrocinador(int id)
        {
            // TODO: Implementar con PatrocinadorCAD cuando esté la BD
            // Por ahora datos de ejemplo
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

            try
            {
                ENPatrocinador patrocinador = new ENPatrocinador();
                patrocinador.IdPatrocinador = idPatrocinador;
                patrocinador.Nombre = txtNombre.Text;
                patrocinador.Email = txtEmail.Text;
                patrocinador.PaginaWeb = txtWeb.Text;
                patrocinador.InicioContrato = DateTime.Parse(txtInicioContrato.Text);
               // patrocinador.FinContrato = string.IsNullOrEmpty(txtFinContrato.Text) ? (DateTime?)null : DateTime.Parse(txtFinContrato.Text);
                patrocinador.Activo = chkActivo.Checked;

                // TODO: Implementar con PatrocinadorCAD.Update() cuando esté la BD
                // PatrocinadorCAD cad = new PatrocinadorCAD();
                // cad.Update(patrocinador);

                lblMensaje.Text = "Patrocinador actualizado correctamente (demo)";
                lblMensaje.ForeColor = System.Drawing.Color.Green;

                // Redirigir después de guardar
                Response.Redirect("~/Public/DetallePatrocinador.aspx?id=" + idPatrocinador);
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error: " + ex.Message;
                lblMensaje.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Public/DetallePatrocinador.aspx?id=" + idPatrocinador);
        }
    }
}