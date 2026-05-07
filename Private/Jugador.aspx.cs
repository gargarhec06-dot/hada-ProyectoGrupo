using hada_ProyectoGrupo.Library.CAD;
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

            }
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
                    Response.Redirect("~/Public/Jugadores.aspx");

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

        

        
    }
}