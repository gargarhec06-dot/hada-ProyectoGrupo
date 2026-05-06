using hada_ProyectoGrupo.Library.CAD;
using hada_ProyectoGrupo.Library.EN;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace hada_ProyectoGrupo.Public
{
    public partial class DetallesJugador : System.Web.UI.Page
    {
        private int codigoJugador;
        private bool esPropietario;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarListaJuegos();

                if (Request.QueryString["codigo"] != null)
                {
                    codigoJugador = int.Parse(Request.QueryString["codigo"]);
                    CargarJugador(codigoJugador);

                    string emailLogueado = Session["Email"] as string;
                    if (!string.IsNullOrEmpty(emailLogueado) && lblEmail.Text == emailLogueado)
                    {
                        esPropietario = true;
                        btnModificar.Visible = true;
                        btnEliminar.Visible = true;
                    }
                }
                else
                {
                    Response.Redirect("~/Public/Jugadores.aspx");
                }
            }
        }

        private void CargarListaJuegos()
        {
            try
            {
                CADVideojuego cadVideojuego = new CADVideojuego();
                List<ENVideojuego> listaJuegos = cadVideojuego.ReadAll();

                ddlJuego.DataSource = listaJuegos;
                ddlJuego.DataTextField = "Nombre";
                ddlJuego.DataValueField = "Codigo";
                ddlJuego.DataBind();
                ddlJuego.Items.Insert(0, new ListItem("Seleccione un juego...", "0"));
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al cargar juegos: " + ex.Message;
            }
        }

        private void CargarJugador(int codigo)
        {
            try
            {
                ENJugador jugador = new ENJugador();
                jugador.Codigo = codigo;

                if (jugador.Read())
                {
                    lblCodigo.Text = jugador.Codigo.ToString();
                    lblEmail.Text = jugador.Email_usuario;
                    lblApodo.Text = jugador.Apodo;
                    txtApodo.Text = jugador.Apodo;
                    lblRol.Text = jugador.Rol_principal;
                    ddlRol.SelectedValue = jugador.Rol_principal;
                    lblKDA.Text = jugador.Kda_promedio.ToString();
                    txtKDA.Text = jugador.Kda_promedio.ToString();
                    lblWinrate.Text = jugador.Winrate.ToString();
                    txtWinrate.Text = jugador.Winrate.ToString();
                    lblNivel.Text = jugador.Nivel.ToString();
                    txtNivel.Text = jugador.Nivel.ToString();
                    lblHardware.Text = jugador.Hardware;
                    ddlHardware.SelectedValue = jugador.Hardware;
                    lblBuscandoEquipo.Text = jugador.Buscando_equipo ? "Sí" : "No";
                    chkBuscandoEquipo.Checked = jugador.Buscando_equipo;
                    lblEquipoActual.Text = jugador.Equipo_actual.ToString();

                    // Mostrar nombre del juego
                    CargarNombreJuego(jugador.Juego);

                    // Seleccionar juego en el DropDownList
                    if (ddlJuego.Items.FindByValue(jugador.Juego.ToString()) != null)
                    {
                        ddlJuego.SelectedValue = jugador.Juego.ToString();
                    }
                }
                else
                {
                    lblMensaje.Text = "Jugador no encontrado";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error: " + ex.Message;
            }
        }

        private void CargarNombreJuego(int codigoJuego)
        {
            try
            {
                ENVideojuego juego = new ENVideojuego();
                juego.Codigo = codigoJuego;
                if (juego.Read())
                {
                    lblJuego.Text = juego.Nombre;
                }
                else
                {
                    lblJuego.Text = "Juego no encontrado";
                }
            }
            catch
            {
                lblJuego.Text = codigoJuego.ToString();
            }
        }

        private void SetModoEdicion(bool edicion)
        {
            lblApodo.Visible = !edicion;
            lblRol.Visible = !edicion;
            lblKDA.Visible = !edicion;
            lblWinrate.Visible = !edicion;
            lblNivel.Visible = !edicion;
            lblHardware.Visible = !edicion;
            lblBuscandoEquipo.Visible = !edicion;
            lblJuego.Visible = !edicion;

            txtApodo.Visible = edicion;
            ddlRol.Visible = edicion;
            txtKDA.Visible = edicion;
            txtWinrate.Visible = edicion;
            txtNivel.Visible = edicion;
            ddlHardware.Visible = edicion;
            chkBuscandoEquipo.Visible = edicion;
            ddlJuego.Visible = edicion;

            btnModificar.Visible = !edicion && esPropietario;
            btnEliminar.Visible = !edicion && esPropietario;
            btnGuardar.Visible = edicion;
            btnCancelar.Visible = edicion;
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            SetModoEdicion(true);
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                ENJugador jugador = new ENJugador();
                jugador.Codigo = codigoJugador;
                jugador.Read();

                jugador.Apodo = txtApodo.Text;
                jugador.Rol_principal = ddlRol.SelectedValue;
                jugador.Kda_promedio = float.Parse(txtKDA.Text);
                jugador.Winrate = float.Parse(txtWinrate.Text);
                jugador.Nivel = int.Parse(txtNivel.Text);
                jugador.Hardware = ddlHardware.SelectedValue;
                jugador.Buscando_equipo = chkBuscandoEquipo.Checked;
                jugador.Juego = int.Parse(ddlJuego.SelectedValue);

                if (jugador.Update())
                {
                    lblMensaje.Text = "Jugador modificado correctamente";
                    lblMensaje.ForeColor = System.Drawing.Color.Green;
                    SetModoEdicion(false);
                    CargarJugador(codigoJugador);
                }
                else
                {
                    lblMensaje.Text = "Error al modificar el jugador";
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error: " + ex.Message;
                lblMensaje.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                ENJugador jugador = new ENJugador();
                jugador.Codigo = codigoJugador;

                if (jugador.Delete())
                {
                    lblMensaje.Text = "Jugador eliminado correctamente";
                    lblMensaje.ForeColor = System.Drawing.Color.Green;
                    Response.Redirect("~/Public/Jugadores.aspx");
                }
                else
                {
                    lblMensaje.Text = "Error al eliminar el jugador";
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error: " + ex.Message;
                lblMensaje.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            SetModoEdicion(false);
            CargarJugador(codigoJugador);
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Public/Jugadores.aspx");
        }
    }
}