using hada_ProyectoGrupo.Library.CAD;
using hada_ProyectoGrupo.Library.EN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace hada_ProyectoGrupo.Private
{
    public partial class AnadirSaldo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Email"] == null)
            {
                Response.Redirect("~/Public/Login.aspx");
                return;
            }
        }

        protected void btnSumar_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            try
            {
                CADUsuario cad = new CADUsuario();
                ENUsuario u = new ENUsuario();
                u.Email = Session["Email"].ToString();

                cad.Read(u);
                u.Saldo_cartera = u.Saldo_cartera + float.Parse(txtFondos.Text);
                cad.Update(u);
                Session["UsuarioActual"] = u;
                Response.Redirect("~/Private/PerfilUsuario.aspx");
            }
            catch(Exception ex)
            {
                lblMensaje.Text = "Error: " + ex.Message;
            }
        }

        protected void btnCancelar_Click(Object sender, EventArgs e) 
        {
            Response.Redirect("~/Private/PerfilUsuario.aspx");
        }
    }
}