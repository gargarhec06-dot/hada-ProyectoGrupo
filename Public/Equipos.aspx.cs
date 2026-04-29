using hada_ProyectoGrupo.Library.EN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace hada_ProyectoGrupo.Public
{
    public partial class Equipos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarEquipos();
                if (Session["EsJugador"] != null && (bool)Session["EsJugador"] == false)
                {
                    pnlJugador.Visible = true;
                }
            }
        }
        public void CargarEquipos()
        {
            List<ENEquipo> lista = new List<ENEquipo>
            {
                new ENEquipo (1,"TSM", DateTime.Now, "https://upload.wikimedia.org/wikipedia/commons/thumb/c/c7/TSM_Logo.svg/500px-TSM_Logo.svg.png" , "Especializado en shooters", 1),
                new ENEquipo (2,"KOI", DateTime.Now, "https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/a/a5/KOI_%28Spanish_Team%29logo_square.png/revision/latest?cb=20221224091735" , "Especializado en survivals", 2),
                new ENEquipo (3,"FAZE", DateTime.Now, "https://cdn.shopify.com/s/files/1/0667/9547/1031/files/logo6_2.png?v=1770079941" , "Especializado en speedruns", 3)
            };
            rptEquipos.DataSource = lista;
            rptEquipos.DataBind();
        }

        protected void btnCrear_Click(object sender, EventArgs e)
        {
            //Falta por implementar base de datos
            Response.Redirect("~/Public/Equipos.aspx");
        }
    }
}