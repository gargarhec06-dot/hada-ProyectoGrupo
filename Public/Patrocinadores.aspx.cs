using System;
using System.Collections.Generic;
using hada_ProyectoGrupo.Library.EN;

namespace hada_ProyectoGrupo.Public
{
    public partial class Patrocinadores : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarPatrocinadores();
            }
        }

        /* private void CargarPatrocinadores()
         {
             // De momento lista vacía, se implementará con la BD más adelante
             List<ENPatrocinador> lista = new List<ENPatrocinador>();
             gvPatrocinadores.DataSource = lista;
             gvPatrocinadores.DataBind();
         }*/
        //demo con datos de ejemplo
        private void CargarPatrocinadores()
        {
            List<ENPatrocinador> lista = new List<ENPatrocinador>
    {
        new ENPatrocinador { IdPatrocinador = 1, Nombre = "Red Bull", Email = "redbull@email.com", PaginaWeb = "https://www.redbull.com", Activo = true },
        new ENPatrocinador { IdPatrocinador = 2, Nombre = "Logitech", Email = "logitech@email.com", PaginaWeb = "https://www.logitech.com", Activo = true },
        new ENPatrocinador { IdPatrocinador = 3, Nombre = "Nvidia", Email = "nvidia@email.com", PaginaWeb = "https://www.nvidia.com", Activo = false }
    };
            gvPatrocinadores.DataSource = lista;
            gvPatrocinadores.DataBind();
        }
    }
}