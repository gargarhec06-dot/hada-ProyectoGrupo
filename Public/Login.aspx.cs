using hada_ProyectoGrupo.Library.EN;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace hada_ProyectoGrupo.Public
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LogInButton_Click(object sender, EventArgs e)
        {
            if (EmailBox.Text == "")
            {
                LogInError.Text = "Error, el email no puede estar vacío";
                LogInError.ForeColor = Color.Red;
                return;
            }
            if (PasswordBox.Text == "")
            {
                LogInError.Text = "Error, la contraseñano no puede estar vacía";
                LogInError.ForeColor = Color.Red;
                return;
            }

            ENUsuario en_usuario = new ENUsuario();

            en_usuario.Email = EmailBox.Text;
            en_usuario.Password = PasswordBox.Text;

            if (en_usuario.Login())
            {
                LogInError.Text = "Accedido correctamente, bienvenido " + en_usuario.Nombre;
                LogInError.ForeColor = Color.Green;
                Session["EsAdmin"] = en_usuario.Verificado;
                Session["Email"] = en_usuario.Email;
                Session["Nombre"] = en_usuario.Nombre;
                Session["UsuarioActual"] = en_usuario;
                Response.Redirect("~/Default.aspx");

            }
            else
            {
                LogInError.Text = "Error, la contraseña probablemente sea erronea";
                LogInError.ForeColor = Color.Red;
            }
        }
    }
}