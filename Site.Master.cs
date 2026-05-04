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
            var itemLogin = Menu1.FindItem("Login");
            var itemPerfil = Menu1.FindItem("Perfil");
            var itemRegistro = Menu1.FindItem("Registro");

            if (Session["Email"] != null)
            {
                if (itemLogin != null)
                    Menu1.Items.Remove(itemLogin);

                if (itemRegistro != null)
                    Menu1.Items.Remove(itemRegistro);
            }
            else
            {
                if (itemPerfil != null)
                    Menu1.Items.Remove(itemPerfil);
            }
        }
    }
}