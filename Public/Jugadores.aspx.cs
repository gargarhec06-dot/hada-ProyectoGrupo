using hada_ProyectoGrupo.Library.EN;
using System;
using System.Collections.Generic;
using System.Web.UI;

namespace hada_ProyectoGrupo.Public
{
    public partial class Jugadores : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarJugadores();
            }
            if (Session["EsAdmin"] != null && (bool)Session["EsAdmin"] == false)
            {
                pnlAdmin3.Visible = true;
            }
        }

        private void CargarJugadores()
        {
            try
            {
                List<ENJugador> todosLosJugadores = new ENJugador().ReadAll();

                if (Session["Email"] != null)
                {
                    // Si está logueado, mostrar SOLO sus jugadores
                    string emailLogueado = Session["Email"].ToString();
                    List<ENJugador> misJugadores = new List<ENJugador>();

                    foreach (ENJugador j in todosLosJugadores)
                    {
                        if (j.Email_usuario == emailLogueado)
                        {
                            misJugadores.Add(j);
                        }
                    }

                    rptJugadores.DataSource = misJugadores;
                    rptJugadores.DataBind();

                    if (misJugadores.Count == 0)
                    {
                        lblMensaje.Text = "No tienes jugadores creados.";
                        lblMensaje.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    // Si NO está logueado, mostrar TODOS los jugadores
                    rptJugadores.DataSource = todosLosJugadores;
                    rptJugadores.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error: " + ex.Message;
                lblMensaje.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnCrear_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/private/Jugador.aspx");
        }
    }
}