using hada_ProyectoGrupo.Library.CAD;
using hada_ProyectoGrupo.Library.EN;
using System;
using System.Collections.Generic;

namespace hada_ProyectoGrupo
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Email"] != null)
                {
                    pnlLogueado.Visible = true;
                    pnlNoLogueado.Visible = false;

                    lblNombre.Text = Session["Nombre"]?.ToString() ?? Session["Email"].ToString();

                    // Número de jugadores del usuario
                    CADJugador cadJugador = new CADJugador();
                    List<ENJugador> jugadores = cadJugador.ReadAllByEmail(Session["Email"].ToString());
                    lblNumJugadores.Text = jugadores.Count.ToString();

                    // Número de torneos
                    CADTorneo cadTorneo = new CADTorneo();
                    lblNumTorneos.Text = cadTorneo.ReadAll().Count.ToString();

                    // Número de patrocinadores
                    CADPatrocinador cadPat = new CADPatrocinador();
                    lblNumPatrocinadores.Text = cadPat.ReadAll().Count.ToString();
                }
                else
                {
                    pnlNoLogueado.Visible = true;
                    pnlLogueado.Visible = false;
                }
            }
        }
    }
}