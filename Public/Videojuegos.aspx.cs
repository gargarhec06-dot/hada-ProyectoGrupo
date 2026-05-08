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
            List<ENVideojuego> list = new ENVideojuego().ReadAll();

            tableGenerator.DataSource = list;
            tableGenerator.DataBind();

            if (Session["EsAdmin"] != null && (bool)Session["EsAdmin"])
            {
                CreateVideojuego.Visible = true;
            }
        }

        protected void CreateVideojuego_Click(object sender, EventArgs e)
        {
            Response.Redirect("Videojuego.aspx");
        }
    }
}