using hada_ProyectoGrupo.Library.EN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace hada_ProyectoGrupo.Public
{
    public partial class Jugadores : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CargarJugadores();
        }

        public void CargarJugadores()
        {
            List<ENJugador> lista = new List<ENJugador>
            {
                new ENJugador(1, "ana.garcia@gmail.com", "MidMaster", "Mid Laner", 4.2f, 58.5f, 45, "PC Gaming - RTX 3080", true, 101),
               new ENJugador(2, "carlos.lopez@hotmail.com", "TopGod", "Top Laner", 3.8f, 55.2f, 38, "Laptop Gaming - RTX 3060", false, 102),
               new ENJugador(3, "lucia.martinez@gmail.com", "JungleQueen", "Jungler", 5.1f, 62.3f, 52, "PC Ultra - RTX 4090", true, 103),
               new ENJugador(4, "mario.rodriguez@yahoo.com", "ADCPro", "AD Carry", 4.5f, 60.1f, 47, "PC Gaming - RTX 4070", false, 101),
                new ENJugador(5, "elena.sanchez@gmail.com", "SupportLover", "Support", 3.5f, 53.7f, 41, "PC Standard - RTX 3060 Ti", true, 0),
                new ENJugador(6, "david.fernandez@outlook.com", "FlexPlayer", "Fill", 4.0f, 56.8f, 35, "Laptop Gaming - RTX 3050", true, 104)
            };
            rptJugadores.DataSource = lista;
            rptJugadores.DataBind();
        }

        protected void btnCrear_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Private/Jugador.aspx");
        }
    }
}