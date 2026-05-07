using System;
using System.Web.UI;
using hada_ProyectoGrupo.Library.CAD;
using hada_ProyectoGrupo.Library.EN;

namespace hada_ProyectoGrupo.Private
{
    public partial class EditarPerfil : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Email"] == null)
            {
                Response.Redirect("~/Public/Login.aspx");
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
                CADUsuario cad = new CADUsuario();
                ENUsuario u = new ENUsuario();
                u.Email = Session["Email"].ToString();

                if (cad.Read(u))
                {
                    txtNombre.Text = u.Nombre;
                    txtApellidos.Text = u.Apellidos;
                    txtPais.Text = u.Pais;
                }
                else
                {
                    lblMensaje.Text = "Error al cargar los datos del usuario.";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al cargar datos: " + ex.Message;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            try
            {
                CADUsuario cad = new CADUsuario();
                ENUsuario u = new ENUsuario();
                u.Email = Session["Email"].ToString();

                // Leer datos actuales para no perder los que no se editan
                if (!cad.Read(u))
                {
                    lblMensaje.Text = "Error al cargar el usuario.";
                    return;
                }

                // Actualizar campos
                u.Nombre = txtNombre.Text;
                u.Apellidos = txtApellidos.Text;
                u.Pais = txtPais.Text;

                // Solo cambiar contraseña si ha escrito algo
                if (!string.IsNullOrEmpty(txtPassword.Text))
                {
                    u.Password = txtPassword.Text;
                }

                if (cad.Update(u))
                {
                    // Actualizar la sesión con el nuevo nombre
                    Session["Nombre"] = u.Nombre;
                    Response.Redirect("~/Private/PerfilUsuario.aspx");
                }
                else
                {
                    lblMensaje.Text = "Error al guardar los cambios.";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error: " + ex.Message;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Private/PerfilUsuario.aspx");
        }
    }
}