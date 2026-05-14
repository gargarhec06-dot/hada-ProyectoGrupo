using hada_ProyectoGrupo.Library.CAD;
using hada_ProyectoGrupo.Library.EN;
using System;
using System.Collections.Generic;
using System.Drawing;
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

            if (IsPostBack)
            {
                return;
            }

            if (!IsPostBack)
            {
                foreach (ENEquipo equipo in new CADInscripcion().ReadEquiposByTorneo(int.Parse(torneo)))
                {
                    EquipoGanadorAdmin.Items.Add(new ListItem(equipo.Nombre, equipo.Id_equipo.ToString()));
                    PerdedorSelect.Items.Add(new ListItem(equipo.Nombre, equipo.Id_equipo.ToString()));
                }
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

                ENEquipo en_equipo = new ENEquipo();
                en_equipo.Id_equipo = partida.Ganador;
                en_equipo.Read();
                EquipoGanadorLabel.Text = en_equipo.Nombre;

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

                    if (Session["EsAdmin"] != null && (bool)Session["EsAdmin"])
                    {
                        PerdedoresLabel.Text = en.Id_equipo.ToString()+",";
                    }
                    else
                    {
                        PerdedoresLabel.Text += en.Nombre + ", ";
                    }
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

            PerdedorSelect.Visible = true;
            PerdedorAdd.Visible = true;
            PerdedorClear.Visible = true;

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

                List<int> jugadores_final = new List<int>();

                List<ENJugador> jugadores = new ENJugador().ReadAll();
                foreach (ENJugador jugador in jugadores)
                {
                    if (jugador.Equipo_actual == equipo_win.Id_equipo)
                    {
                        jugadores_final.Add(jugador.Codigo);
                    }
                }

                ENPartida partida = new ENPartida(
                    0,
                    EnlaceRepeticionAdmin.Text,
                    DateTime.Parse(FechaAdmin.Text),
                    torneo_local.IdVideojuego,
                    int.Parse(EquipoGanadorAdmin.SelectedValue),
                    GetLosers(),
                    jugadores_final.ToArray(),
                    torneo_local.Codigo
                    );

                if (partida.Create())
                {
                    Response.Redirect("DetalleTorneo.aspx?codigo=" + torneo);
                }
                else
                {
                    DebugLabel.Text = "Algo fue mal";
                }
            }
        }

        protected void PerdedorAdd_Click(object sender, EventArgs e)
        {
            PerdedoresLabel.Text += PerdedorSelect.SelectedValue + ",";
        }

        protected void PerdedorClear_Click(object sender, EventArgs e)
        {
            PerdedoresLabel.Text = "";
        }

        protected void AdminDelete_Click(object sender, EventArgs e)
        {
            if (Session["EsAdmin"] != null && (bool)Session["EsAdmin"])
            {
                string code = Request.QueryString["codigo"];
                string torneo = Request.QueryString["torneo"];
                ENPartida en = new ENPartida();
                en.Code = int.Parse (code);

                if (en.Delete())
                {
                    Response.Redirect("DetalleTorneo.aspx?codigo=" + torneo);
                }
                else
                {
                    DebugLabel.Text = "Algo fue mal";
                }
            }
        }

        protected int[] GetLosers()
        {
            List<int> perdedores = new List<int>();

            foreach (string perdedor in PerdedoresLabel.Text.Split(','))
            {
                int posible_perdedor = 0;

                if (int.TryParse(perdedor, out posible_perdedor)) perdedores.Add(posible_perdedor);
            }

            return perdedores.ToArray();
        }

        protected void AdminUpdate_Click(object sender, EventArgs e)
        {
            if (Session["EsAdmin"] != null && (bool)Session["EsAdmin"])
            {
                string code = Request.QueryString["codigo"];
                string torneo = Request.QueryString["torneo"];

                ENTorneo torneo_local = new ENTorneo();
                torneo_local.Codigo = int.Parse(torneo);
                torneo_local.Read();

                ENEquipo equipo_win = new ENEquipo();
                equipo_win.Id_equipo = int.Parse(EquipoGanadorAdmin.SelectedValue);
                equipo_win.Read();

                List<int> jugadores_final = new List<int>();

                List<ENJugador> jugadores = new ENJugador().ReadAll();
                foreach (ENJugador jugador in jugadores)
                {
                    if (jugador.Equipo_actual == equipo_win.Id_equipo)
                    {
                        jugadores_final.Add(jugador.Codigo);
                    }
                }

                ENPartida partida = new ENPartida(
                    int.Parse(code),
                    EnlaceRepeticionAdmin.Text,
                    DateTime.Parse(FechaAdmin.Text),
                    torneo_local.IdVideojuego,
                    int.Parse(EquipoGanadorAdmin.SelectedValue),
                    GetLosers(),
                    jugadores_final.ToArray(),
                    torneo_local.Codigo
                    );

                if (partida.Update())
                {
                    Response.Redirect("DetallesPartida.aspx?torneo=" + torneo + "&codigo=" + code);
                }
                else
                {
                    DebugLabel.Text = "Algo fue mal";
                }
            }
        }
    }
}