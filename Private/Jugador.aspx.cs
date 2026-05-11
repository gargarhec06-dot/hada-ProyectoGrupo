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
                CADVideojuego cadVj = new CADVideojuego();
                ddlVideojuego.DataSource = cadVj.ReadAll();
                ddlVideojuego.DataTextField = "Nombre";
                ddlVideojuego.DataValueField = "Codigo";
                ddlVideojuego.DataBind();
                ddlVideojuego.Items.Insert(0, new ListItem("Selecciona un videojuego", "0"));


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
                jugador.Rol_principal = txtRol.Text;
                jugador.Hardware = ddlHardware.SelectedValue;
                jugador.Buscando_equipo = chkBuscandoEquipo.Checked;
                jugador.Juego = int.Parse(ddlVideojuego.SelectedValue);

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