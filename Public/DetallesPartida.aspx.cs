using hada_ProyectoGrupo.Library.EN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace hada_ProyectoGrupo.Public
{
    public partial class DetallesPartida : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string code = Request.QueryString["codigo"];
            string torneo = Request.QueryString["torneo"];

            if (string.IsNullOrEmpty(torneo))
            {
                DebugLabel.Text = "No hay parametro de torneo, input invalido";
                return;
            }

            if (string.IsNullOrEmpty(code))
            {
                if (Session["EsAdmin"] != null && (bool)Session["EsAdmin"])
                {
                    activate_admin();

                    AdminDelete.Visible = false;
                    AdminUpdate.Visible = false;
                    AdminAdd.Visible = true;

                    return;
                }
                else
                {
                    DebugLabel.Text = "No se encontró ningún argumento para el código del juego";
                    return;
                }
            }

            ENPartida partida = new ENPartida();
            partida.Code = int.Parse(code);
            bool result = partida.Read();

            if (result)
            {
                CodigoLabel.Text = code;
                FechaLabel.Text = partida.Fecha.ToString();
                EnlaceRepeticion.Text = partida.EnlaceDeRepeticion;
                EnlaceRepeticion.NavigateUrl = partida.EnlaceDeRepeticion;

                ENTorneo torneo_local = new ENTorneo();
                torneo_local.Codigo = int.Parse(torneo);
                torneo_local.Read();

                VideojuegoEnlace.Text = torneo_local.Nombre;
                VideojuegoEnlace.NavigateUrl = "DetallesTorneo?codigo="+torneo;

                JugadoresLabel.Text = "";
                PerdedoresLabel.Text = "";

                foreach (int jugador in partida.Jugadores)
                {
                    ENJugador en = new ENJugador();
                    en.Codigo = jugador;
                    en.Read();

                    JugadoresLabel.Text += en.Apodo + ", ";
                }

                foreach (int perdedor in partida.Perdedores)
                {
                    ENEquipo en = new ENEquipo();
                    en.Id_equipo = perdedor;
                    en.Read();

                    PerdedoresLabel.Text += en.Nombre + ", ";
                }

                if (Session["EsAdmin"] != null && (bool)Session["EsAdmin"])
                {
                    activate_admin();

                    return;
                }
            }
            else
            {
                DebugLabel.Text = "No se encontró";
            }
        }

        private void activate_admin()
        {
            FechaAdmin.Visible = true;
            EnlaceRepeticionAdmin.Visible = true;
            EquipoGanadorAdmin.Visible = true;

            AdminDelete.Visible = true;
            AdminUpdate.Visible = true;
        }

        protected void AdminAdd_Click(object sender, EventArgs e)
        {
            if (Session["EsAdmin"] != null && (bool)Session["EsAdmin"])
            {
                string torneo = Request.QueryString["torneo"];
                ENTorneo torneo_local = new ENTorneo();
                torneo_local.Codigo = int.Parse(torneo);
                torneo_local.Read();

                ENEquipo equipo_win = new ENEquipo();
                equipo_win.Id_equipo = int.Parse(EquipoGanadorAdmin.SelectedValue);
                equipo_win.Read();

                /*ENPartida partida = new ENPartida(
                    0,
                    EnlaceRepeticionAdmin.Text,
                    DateTime.Parse(FechaAdmin.Text),
                    torneo_local.IdVideojuego,
                    int.Parse(EquipoGanadorAdmin.SelectedValue),
                    new [],
                    new [],
                    0
                    );
                */
            }
        }
    }
}