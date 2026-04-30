using hada_ProyectoGrupo.Library.EN;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace hada_ProyectoGrupo.Private
{
    public partial class Jugador : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Comprobar que hay sesión activa
            if (Session["Email"] == null)
            {
                Response.Redirect("~/Public/Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                CargarJugadores();
            }
        }

        private void CargarJugadores()
        {
            // TODO: sustituir por CADJugador cuando esté implementado
            List<ENJugador> lista = new List<ENJugador>
            {
                new ENJugador { Codigo = 1, Apodo = "ProPlayer1", Rol_principal = "Mid", Nivel = 5, Buscando_equipo = true },
                new ENJugador { Codigo = 2, Apodo = "ProPlayer2", Rol_principal = "Top", Nivel = 3, Buscando_equipo = false }
            };
            rptJugadores.DataSource = lista;
            rptJugadores.DataBind();
        }

        protected void btnCrear_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            try
            {
                ENJugador jugador = new ENJugador(
                    Session["Email"].ToString(),
                    txtApodo.Text
                );
                jugador.Rol_principal = ddlRol.SelectedValue;
                jugador.Hardware = ddlHardware.SelectedValue;
                jugador.Buscando_equipo = chkBuscandoEquipo.Checked;

                bool ok = jugador.Create();

                if (ok)
                {
                    lblMensaje.ForeColor = System.Drawing.Color.Green;
                    lblMensaje.Text = "Jugador creado correctamente.";
                    txtApodo.Text = "";
                    CargarJugadores();
                }
                else
                {
                    lblMensaje.Text = "Error al crear el jugador.";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error: " + ex.Message;
            }
        }

        protected void btnSeleccionar_Command(object sender, CommandEventArgs e)
        {
            // Guardar el jugador activo en sesión
            Session["JugadorActivo"] = int.Parse(e.CommandArgument.ToString());
            lblMensaje.ForeColor = System.Drawing.Color.Green;
            lblMensaje.Text = "Jugador seleccionado correctamente.";
        }

        protected void btnEliminar_Command(object sender, CommandEventArgs e)
        {
            // TODO: implementar con CADJugador cuando esté listo
            lblMensaje.ForeColor = System.Drawing.Color.Green;
            lblMensaje.Text = "Jugador eliminado correctamente.";
            CargarJugadores();
        }
    }
}