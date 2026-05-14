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

                    
                    bool esAdmin = (Session["EsAdmin"] != null && (bool)Session["EsAdmin"] == true);
                    pnlAdminStats.Visible = esAdmin;

                   
                    try
                    {
                        // Número de jugadores del usuario
                        CADJugador cadJugador = new CADJugador();
                        var jugadores = cadJugador.ReadAllByEmail(Session["Email"].ToString());
                        lblNumJugadores.Text = jugadores != null ? jugadores.Count.ToString() : "0";

                        // Número de torneos
                        CADTorneo cadTorneo = new CADTorneo();
                        var torneos = cadTorneo.ReadAll();
                        lblNumTorneos.Text = torneos != null ? torneos.Count.ToString() : "0";

                        // Número de patrocinadores
                        CADPatrocinador cadPat = new CADPatrocinador();
                        var patrocinadores = cadPat.ReadAll();
                        lblNumPatrocinadores.Text = patrocinadores != null ? patrocinadores.Count.ToString() : "0";
                    }
                    catch (Exception)
                    {
                        // Si algo falla en la base de datos, ponemos 0 para que la web siga funcionando
                        lblNumJugadores.Text = "0";
                        lblNumTorneos.Text = "0";
                        lblNumPatrocinadores.Text = "0";
                    }
                }
                else
                {
                    pnlNoLogueado.Visible = true;
                    pnlLogueado.Visible = false;
                    pnlAdminStats.Visible = false;
                }
            }
        }
    }
}