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
            listado_demo.Add(new ENVideojuego(1, "Quake", "ninguna", "Speedrun", 18));
            listado_demo.Add(new ENVideojuego(2, "Quake", "ninguna", "Shooter", 18));
            listado_demo.Add(new ENVideojuego(3, "Schedule 1", "ninguna", "Speedrun", 21));
            listado_demo.Add(new ENVideojuego(4, "Mario64", "ninguna", "Speedrun", 3));
            listado_demo.Add(new ENVideojuego(5, "Call of Duty", "ninguna", "Shooter", 18));
            listado_demo.Add(new ENVideojuego(6, "Fortnite", "ninguna", "Shooter", 12));
            listado_demo.Add(new ENVideojuego(7, "Counter Strike 2", "ninguna", "Shooter", 18));
            listado_demo.Add(new ENVideojuego(8, "League Of Lengends", "ninguna", "Moba", 12));
            listado_demo.Add(new ENVideojuego(9, "Rivals of Ather 2", "ninguna", "Fighthing", 12));
            listado_demo.Add(new ENVideojuego(10, "Super Smash Brothers Ultimate", "ninguna", "Fighting", 12));

            tableGenerator.DataSource = listado_demo;
            tableGenerator.DataBind();
        }
    }
}