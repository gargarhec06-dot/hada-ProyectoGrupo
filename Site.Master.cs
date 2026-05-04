using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace hada_ProyectoGrupo
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var itemAcceso = Menu1.FindItem("Acceso");
            var itemPerfil = Menu1.FindItem("Perfil");

            if (Session["Email"] != null)
            {
                // Usuario logueado
                if (itemAcceso != null)
                    Menu1.Items.Remove(itemAcceso);

               
            }
            else
            {
                // Usuario NO logueado
                if (itemPerfil != null)
                    Menu1.Items.Remove(itemPerfil);
            }
        }
    }
}