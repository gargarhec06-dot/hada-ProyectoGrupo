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
    public partial class DetallesEquipo : System.Web.UI.Page
    {
        private ENEquipo equipo;
        private string accionPendiente; //Lo uso para la condicion de unirse y crear
        private int idEquipo;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Email"] == null)
            {
                Response.Redirect("~/Public/Login.aspx");
                return;
            }
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    idEquipo = int.Parse(Request.QueryString["id"]);
                    CargarEquipo(idEquipo);
                    Session["EsAdmin"] = false;  // temporal para probar
                    if (Session["EsAdmin"] != null && (bool)Session["EsAdmin"] == false)//Pensandolmelo
                    {
                        pnlJugador.Visible = true;
                    }
                }
                else
                {
                    pnlJugador.Visible = true;
                    // Limpiar campos para un nuevo equipo
                    txtNombre.Text = "";
                    txtFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtDescripcion.Text = "";
                    txtCapitan.Text = "";
                    txtLogo.Text = "";
                }
            }
        }

        private void CargarEquipo(int id)
        {
            try
            {
                equipo = new ENEquipo();
                equipo.Id_equipo = id;
                if (equipo.Read())
                {
                    txtNombre.Text = equipo.Nombre;
                    txtFecha.Text = equipo.Fecha_creacion.ToString("dd/MM/yyyy");
                    txtDescripcion.Text = equipo.Descripcion;
                    txtCapitan.Text = equipo.Id_capitan.ToString();
                    txtLogo.Text = equipo.Logo_url;

                    // Validar que la URL del logo no esté vacía
                    if (!string.IsNullOrEmpty(equipo.Logo_url))
                    {
                        imgLogo.ImageUrl = equipo.Logo_url;
                    }
                    else
                    {
                        imgLogo.ImageUrl = "https://e7.pngegg.com/pngimages/779/61/png-clipart-logo-idea-cute-eagle-leaf-logo-thumbnail.png"; // Logo por defecto
                    }
                }
                else
                {
                    Console.WriteLine("No se pudo leer el equipo con ID: {0}", id);
                    Response.Write("<script>alert('Equipo no encontrado');</script>");
                    Response.Redirect("~/Public/Equipos.aspx");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cargar equipo: {0}", ex.Message);
                Response.Write("<script>alert('Error al cargar el equipo');</script>");
                Response.Redirect("~/Public/Equipos.aspx");
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Public/Equipos.aspx");
        }

        protected void btnCrear_Click(object sender, EventArgs e)
        {
            accionPendiente = "CREAR";
            CargarJugadoresDisponibles();
            pnlSeleccionJugador.Visible = true;
        }



        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                ENEquipo equipoEliminar = new ENEquipo();

                if (Request.QueryString["id"] != null)
                {
                    equipoEliminar.Id_equipo = int.Parse(Request.QueryString["id"]);
                }
                else
                {
                    lblMensaje.Text = "ID de equipo no válido";
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                if (!equipoEliminar.Read())
                {
                    lblMensaje.Text = "El equipo no existe";
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                }
                else if (equipoEliminar.Delete())
                {
                    lblMensaje.Text = "Equipo eliminado correctamente";
                    lblMensaje.ForeColor = System.Drawing.Color.Green;
                    Response.Redirect("~/Public/Equipos.aspx");
                }
                else
                {
                    lblMensaje.Text = "ERROR al eliminar el equipo";
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error: " + ex.Message;
                lblMensaje.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                ENEquipo equipoModificar = new ENEquipo();

                if (Request.QueryString["id"] != null)
                {
                    equipoModificar.Id_equipo = int.Parse(Request.QueryString["id"]);
                }
                else
                {
                    lblMensaje.Text = "ID de equipo no válido";
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                // Primero leer el equipo existente
                if (!equipoModificar.Read())
                {
                    lblMensaje.Text = "El equipo no existe";
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    // Actualizar con los nuevos valores
                    equipoModificar.Nombre = txtNombre.Text;
                    equipoModificar.Fecha_creacion = DateTime.Parse(txtFecha.Text);
                    equipoModificar.Logo_url = txtLogo.Text;
                    equipoModificar.Descripcion = txtDescripcion.Text;
                    equipoModificar.Id_capitan = int.Parse(txtCapitan.Text);

                    if (equipoModificar.Update())
                    {
                        lblMensaje.Text = "Equipo modificado correctamente";
                        lblMensaje.ForeColor = System.Drawing.Color.Green;
                        // Recargar la imagen actualizada
                        if (!string.IsNullOrEmpty(equipoModificar.Logo_url))
                        {
                            imgLogo.ImageUrl = equipoModificar.Logo_url;
                        }
                    }
                    else
                    {
                        lblMensaje.Text = "ERROR al modificar el equipo";
                        lblMensaje.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error: " + ex.Message;
                lblMensaje.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnUnirse_Click(object sender, EventArgs e)
        {
            accionPendiente = "UNIRSE";
            CargarJugadoresParaUnirse(idEquipo);
            pnlSeleccionJugador.Visible = true;
        }

        private void CargarJugadoresDisponibles()
        {
            string emailUsuario = Session["Email"].ToString();

            // DEBUG: Mostrar el email de sesión
            lblMensaje.Text = "Email sesión: " + emailUsuario;

            List<ENJugador> todosJugadores = new CADJugador().ReadAll();

            // DEBUG: Mostrar cuántos jugadores hay en total
            lblMensaje.Text += " - Total jugadores: " + todosJugadores.Count;

            List<ENJugador> jugadoresDisponibles = new List<ENJugador>();

            foreach (ENJugador j in todosJugadores)
            {
                // DEBUG: Mostrar cada jugador
                lblMensaje.Text += "<br/>Jugador: " + j.Email_usuario + " - Equipo: " + j.Equipo_actual;

                if (j.Email_usuario == emailUsuario && j.Equipo_actual == 0)
                {
                    jugadoresDisponibles.Add(j);
                    lblMensaje.Text += " ✅ SELECCIONADO";
                }
            }

            lblMensaje.Text += "<br/>Jugadores disponibles: " + jugadoresDisponibles.Count;

            ddlJugadores.DataSource = jugadoresDisponibles;
            ddlJugadores.DataTextField = "Apodo";
            ddlJugadores.DataValueField = "Codigo";
            ddlJugadores.DataBind();

            if (ddlJugadores.Items.Count == 0)
            {
                lblMensaje.Text += "<br/>No tienes jugadores disponibles. Crea un jugador primero.";
                pnlSeleccionJugador.Visible = false;
            }
        }

        private void CargarJugadoresParaUnirse(int idequipo)
        {
            string emailUsuario = Session["Email"].ToString();

            // Obtener el equipo y su capitán
            ENEquipo equipoActual = new ENEquipo();
            equipoActual.Id_equipo = idequipo;
            equipoActual.Read();

            // Obtener el videojuego del capitán
            ENJugador capitan = new ENJugador();
            capitan.Codigo = equipoActual.Id_capitan;
            capitan.Read();
            int juegoCapitan = capitan.Juego;

            // Obtener jugadores del usuario que no estén en equipo y jueguen el mismo juego
            List<ENJugador> todosJugadores = new CADJugador().ReadAll();
            List<ENJugador> jugadoresValidos = new List<ENJugador>();

            foreach (ENJugador j in todosJugadores)
            {
                if (j.Email_usuario == emailUsuario && j.Equipo_actual == 0 && j.Juego == juegoCapitan)
                {
                    jugadoresValidos.Add(j);
                }
            }

            ddlJugadores.DataSource = jugadoresValidos;
            ddlJugadores.DataTextField = "Apodo";
            ddlJugadores.DataValueField = "Codigo";
            ddlJugadores.DataBind();

            if (ddlJugadores.Items.Count == 0)
            {
                lblMensaje.Text = "No tienes jugadores disponibles que jueguen al mismo juego que el capitán.";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                pnlSeleccionJugador.Visible = false;
            }
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            int codigoJugador = int.Parse(ddlJugadores.SelectedValue);

            if (accionPendiente == "CREAR")
            {
                CrearEquipoConCapitan(codigoJugador);
            }
            else if (accionPendiente == "UNIRSE")
            {
                UnirseEquipo(codigoJugador, idEquipo);
            }

            pnlSeleccionJugador.Visible = false;
        }
        private void CrearEquipoConCapitan(int codigoCapitan)
        {
            try
            {
                ENJugador capitan = new ENJugador();
                capitan.Codigo = codigoCapitan;
                capitan.Read();

                ENEquipo nuevoEquipo = new ENEquipo();
                nuevoEquipo.Id_equipo = 0; // La BD lo generará automáticamente
                nuevoEquipo.Nombre = txtNombre.Text;
                nuevoEquipo.Fecha_creacion = DateTime.Now;
                nuevoEquipo.Logo_url = txtLogo.Text;
                nuevoEquipo.Descripcion = txtDescripcion.Text;
                nuevoEquipo.Id_capitan = codigoCapitan;

                if (nuevoEquipo.Create())
                {
                    // Obtener el equipo recién creado (por nombre y capitán)
                    ENEquipo equipoCreado = ObtenerEquipoPorCapitan(codigoCapitan);

                    if (equipoCreado != null)
                    {
                        // Actualizar el jugador con el equipo actual
                        capitan.Equipo_actual = equipoCreado.Id_equipo;
                        capitan.Update();

                        lblMensaje.Text = "Equipo creado correctamente";
                        lblMensaje.ForeColor = System.Drawing.Color.Green;

                        // Redirigir al nuevo equipo
                        Response.Redirect("~/Public/DetallesEquipo.aspx?id=" + equipoCreado.Id_equipo);
                    }
                    else
                    {
                        lblMensaje.Text = "ERROR: No se pudo obtener el ID del equipo";
                        lblMensaje.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    lblMensaje.Text = "ERROR al crear el equipo";
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error: " + ex.Message;
                lblMensaje.ForeColor = System.Drawing.Color.Red;
            }
        }

        private ENEquipo ObtenerEquipoPorCapitan(int codigoCapitan)
        {
            List<ENEquipo> equipos = new CADEquipo().ReadAll();
            foreach (ENEquipo eq in equipos)
            {
                if (eq.Id_capitan == codigoCapitan)
                {
                    return eq;
                }
            }
            return null;
        }
        private void UnirseEquipo(int codigoJugador, int idEquipo)
        {
            try
            {
                ENJugador jugador = new ENJugador();
                jugador.Codigo = codigoJugador;
                jugador.Read();

                // Verificar que no esté ya en un equipo
                if (jugador.Equipo_actual != 0)
                {
                    lblMensaje.Text = "Este jugador ya pertenece a un equipo";
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                jugador.Equipo_actual = idEquipo;

                if (jugador.Update())
                {
                    lblMensaje.Text = "Jugador unido al equipo correctamente";
                    lblMensaje.ForeColor = System.Drawing.Color.Green;
                    Response.Redirect("~/Public/DetallesEquipo.aspx?id=" + idEquipo);
                }
                else
                {
                    lblMensaje.Text = "ERROR al unir el jugador al equipo";
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error: " + ex.Message;
                lblMensaje.ForeColor = System.Drawing.Color.Red;
            }
        }
        protected void btnCancelarSeleccion_Click(object sender, EventArgs e)
        {
            pnlSeleccionJugador.Visible = false;
        }

    }
}