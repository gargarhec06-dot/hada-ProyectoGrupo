using hada_ProyectoGrupo.Library.EN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace hada_ProyectoGrupo.Public
{
    public partial class Videojuegos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadVideojuego(new ENVideojuego().ReadAll());

            if (Session["EsAdmin"] != null && (bool)Session["EsAdmin"])
            {
                CreateVideojuego.Visible = true;
            }
        }

        protected void LoadVideojuego(List<ENVideojuego> list)
        {
            foreach (ENVideojuego en in list)
            {
                en.Tipo = ENVideojuego.GetVideojuegoTipoToNombreLegible(ENVideojuego.GetVideojuegoTipoFromCode(en.Tipo));
            }
            tableGenerator.DataSource = list;
            tableGenerator.DataBind();
        }

        protected void CreateVideojuego_Click(object sender, EventArgs e)
        {
            Response.Redirect("Videojuego.aspx");
        }
    }
}