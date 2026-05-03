using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace hada_ProyectoGrupo.Private
{
    public partial class PerfilUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Verificar sesión


            if (!IsPostBack)
            {
                // Cargar datos del usuario desde la sesión
                // lblNombre.Text = Session["Nombre"]?.ToString();
                // lblEmail.Text = Session["Email"]?.ToString();
            }
        }

        protected void btnMisJugadores_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Public/Jugadores.aspx");
        }

        protected void btnEditarPerfil_Click(object sender, EventArgs e)
        {
            // TODO: Redirigir a página de edición de perfil
            Response.Redirect("~/Private/EditarPerfil.aspx");
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/Public/Login.aspx");
        }
    }
}