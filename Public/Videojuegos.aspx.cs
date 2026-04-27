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
            List<ENVideojuego> listado_demo = new List<ENVideojuego>();

            listado_demo.Add(new ENVideojuego(0, "Hello Kitty Island Adventure", "ninguna", "Speedrun", 3));
            listado_demo.Add(new ENVideojuego(0, "Quake", "ninguna", "Speedrun", 18));
            listado_demo.Add(new ENVideojuego(0, "Quake", "ninguna", "Shooter", 18));
            listado_demo.Add(new ENVideojuego(0, "Schedule1", "ninguna", "Speedrun", 21));
            listado_demo.Add(new ENVideojuego(0, "Mario64", "ninguna", "Speedrun", 3));
            listado_demo.Add(new ENVideojuego(0, "Call of Duty", "ninguna", "Shooter", 18));
            listado_demo.Add(new ENVideojuego(0, "Fortnite", "ninguna", "Shooter", 12));
            listado_demo.Add(new ENVideojuego(0, "Counter Strike 2", "ninguna", "Shooter", 18));
            listado_demo.Add(new ENVideojuego(0, "League Of Lengends", "ninguna", "Moba", 12));
            listado_demo.Add(new ENVideojuego(0, "Rivals of Ather 2", "ninguna", "Fighthing", 12));
            listado_demo.Add(new ENVideojuego(0, "Super Smash Brothers Ultimate", "ninguna", "Fighting", 12));

            tableGenerator.DataSource = listado_demo;
            tableGenerator.DataBind();
        }
    }
}