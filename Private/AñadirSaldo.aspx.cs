using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace hada_ProyectoGrupo.Private
{
    public partial class AñadirSaldo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSumar_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancelar_Click(Object sender, EventArgs e) 
        {
            Response.Redirect("~/Private/PerfilUsuario");
        }
    }
}