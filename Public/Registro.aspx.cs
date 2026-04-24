using hada_ProyectoGrupo.Library.EN;
using System;

namespace hada_ProyectoGrupo.Public
{
    public partial class Registro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnRegistro_Click(object sender, EventArgs e)
        {
            try
            {
                ENUsuario usuario = new ENUsuario(
                    txtEmail.Text,
                    txtPassword.Text,
                    txtNombre.Text,
                    DateTime.Parse(txtFecha.Text)
                );
                usuario.Apellidos = txtApellidos.Text;
                usuario.Pais = txtPais.Text;

                ENJugador jugador = new ENJugador(
                    txtEmail.Text,
                    txtApodo.Text
                );

                bool okUsuario = usuario.Register();
                bool okJugador = jugador.Create();

                if (okUsuario && okJugador)
                {
                    lblMensaje.ForeColor = System.Drawing.Color.Green;
                    lblMensaje.Text = "Registro exitoso. Ya puedes iniciar sesión.";
                }
                else
                {
                    lblMensaje.Text = "Error al registrar. Inténtalo de nuevo.";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error: " + ex.Message;
            }
        }
    }
}