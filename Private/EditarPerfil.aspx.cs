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
                string email = Session["Email"]?.ToString();

                // Debug temporal
                lblMensaje.Text = "Email en sesión: " + (email ?? "NULL");

                if (string.IsNullOrEmpty(email))
                {
                    lblMensaje.Text = "No hay sesión activa.";
                    return;
                }

                CADUsuario cad = new CADUsuario();
                ENUsuario u = new ENUsuario();
                u.Email = email;

                if (cad.Read(u))
                {
                    txtNombre.Text = u.Nombre;
                    txtApellidos.Text = u.Apellidos;
                    txtPais.Text = u.Pais;
                    lblMensaje.Text = ""; // limpiar debug
                }
                else
                {
                    lblMensaje.Text = "Read devolvió false para email: " + email;
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error: " + ex.Message;
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