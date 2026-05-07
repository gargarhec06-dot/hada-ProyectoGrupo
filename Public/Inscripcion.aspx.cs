using hada_ProyectoGrupo.Library.CAD;
using hada_ProyectoGrupo.Library.EN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace hada_ProyectoGrupo.Public
{
    public partial class Inscripcion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UsuarioActual"] == null)
                {
                    Response.Redirect("~/Public/Login.aspx");
                    return;
                }

                if (Request.QueryString["codigo"] == null)
                {
                    Response.Redirect("~/Public/Torneos.aspx");
                    return;
                }

                CargarPagina();
            }
        }

        private void CargarPagina()
        {
            int codigoTorneo = int.Parse(Request.QueryString["codigo"]);
            ENUsuario usuario = (ENUsuario)Session["UsuarioActual"];

            ENTorneo torneo = new ENTorneo();
            torneo.Codigo = codigoTorneo;
            if (torneo.Read())
                lblTorneo.Text = torneo.Nombre;

            CADJugador cadJugador = new CADJugador();
            List<ENJugador> jugadores = cadJugador.ReadAllByEmail(usuario.Email);

            if (jugadores.Count == 0)
            {
                MostrarError("No tienes jugadores creados.");
                return;
            }

            CADEquipo cadEquipo = new CADEquipo();
            List<ENEquipo> equiposCapitan = new List<ENEquipo>();

            foreach (ENJugador jugador in jugadores)
            {
                ENEquipo equipo = cadEquipo.ReadByCapitan(jugador.Codigo);
                if (equipo != null)
                {
                    equiposCapitan.Add(equipo);
                }
            }

            if (equiposCapitan.Count == 0)
            {
                MostrarError("No eres capitan de ningún equipo.");
                return;
            }

            // si el usuario tiene algún jugador que es capitán, aparecerá dicho equipo en el desplegable.
            // si tiene más de uno saldrán todos y si no tiene saltará un return de uno de los if anteriores.
            ddlEquipos.DataSource = equiposCapitan;
            ddlEquipos.DataTextField = "Nombre";
            ddlEquipos.DataValueField = "Id_equipo";
            ddlEquipos.DataBind();
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            int codigoTorneo = int.Parse(Request.QueryString["codigo"]);
            int idEquipo = int.Parse(ddlEquipos.SelectedValue);
            ENUsuario usuario = (ENUsuario)Session["UsuarioActual"];

            ENTorneo torneo = new ENTorneo();
            torneo.Codigo = codigoTorneo;
            torneo.Read();

            if (usuario.Saldo_cartera < torneo.PrecioInscripcion)
            {
                lblMensaje.Text = "Saldo insuficiente. Necesitas " + torneo.PrecioInscripcion + "€ y tienes " + usuario.Saldo_cartera + "€";
                return;
            }

            ENInscripcion inscripcion = new ENInscripcion();
            inscripcion.Id_equipo = idEquipo;
            inscripcion.Id_torneo = codigoTorneo;
            inscripcion.Fecha_inscripcion = DateTime.Now;
            inscripcion.Cuota_pagada = torneo.PrecioInscripcion;
            inscripcion.Moneda = "EUR";

            if (inscripcion.Create())
            {
                usuario.Saldo_cartera = usuario.Saldo_cartera - torneo.PrecioInscripcion;
                usuario.Update();
                Session["UsuarioActual"] = usuario;

                Response.Redirect("~/Public/Torneos.aspx");
            }
            else
            {
                lblMensaje.Text = "Error al inscribir. Es MUY posible que el equipo ya esté inscrito";
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            int codigoTorneo = int.Parse(Request.QueryString["codigo"]);
            Response.Redirect("~/Public/DetalleTorneo.aspx?codigo=" + codigoTorneo);
        }

        private void MostrarError(string mensaje)
        {
            pnlFormulario.Visible = false;
            pnlError.Visible = true;
            lblError.Text = mensaje;
        }
    }
}