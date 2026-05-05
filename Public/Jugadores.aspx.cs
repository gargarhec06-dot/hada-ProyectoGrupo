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
            if (!IsPostBack)
            {
                CargarJugadores();
                if (Session["EsAdmin"] != null && (bool)Session["EsAdmin"] == false)
                {
                    pnlJugador.Visible = true;
                }
            }
        }

        public void CargarJugadores()
        {
            try
            {
                ENJugador jugador = new ENJugador();
                List<ENJugador> lista = jugador.ReadAll();  // Leer de la BD

                rptJugadores.DataSource = lista;
                rptJugadores.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message);

            }
        }

        protected void btnCrear_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Private/Jugador.aspx");
        }
    }
}