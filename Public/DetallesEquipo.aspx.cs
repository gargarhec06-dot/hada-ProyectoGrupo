using hada_ProyectoGrupo.Library.CAD;
using hada_ProyectoGrupo.Library.EN;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace hada_ProyectoGrupo.Public
{
    public partial class DetallesEquipo : System.Web.UI.Page
    {
        private ENEquipo equipo;
        private string accionPendiente
        {
            get { return ViewState["accionPendiente"] as string; }
            set { ViewState["accionPendiente"] = value; }
        }
        private int idEquipo
        {
            get { return ViewState["idEquipo"] != null ? (int)ViewState["idEquipo"] : 0; }
            set { ViewState["idEquipo"] = value; }
        }

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

                    // Mostrar panel de acciones solo si NO es admin
                    if (Session["EsAdmin"] != null && (bool)Session["EsAdmin"] == false)
                    {
                        pnlJugador.Visible = true;
                    }
                }
                else
                {
                    // Modo creación: mostrar panel de acciones
                    pnlJugador.Visible = true;
                    pnlSeleccionJugador.Visible = false;

                    // Limpiar campos para un nuevo equipo
                    txtNombre.Text = "";
                    txtFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtDescripcion.Text = "";
                    txtLogo.Text = "";
                    lblCapitanNombre.Text = "No seleccionado";
                    hfIdCapitan.Value = "0";

                    // Ocultar botones de eliminar y modificar en modo creación
                    btnEliminar.Visible = false;
                    btnModificar.Visible = false;
                    btnUnirse.Visible = false;
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
                    txtLogo.Text = equipo.Logo_url;
                    hfIdCapitan.Value = equipo.Id_capitan.ToString();

                    // Cargar el nombre del capitán
                    if (equipo.Id_capitan > 0)
                    {
                        ENJugador capitan = new ENJugador();
                        capitan.Codigo = equipo.Id_capitan;
                        if (capitan.Read())
                        {
                            lblCapitanNombre.Text = capitan.Apodo;
                        }
                    }
                    else
                    {
                        lblCapitanNombre.Text = "Sin capitán";
                    }

                    if (!string.IsNullOrEmpty(equipo.Logo_url))
                    {
                        imgLogo.ImageUrl = equipo.Logo_url;
                    }
                    else
                    {
                        imgLogo.ImageUrl = "https://e7.pngegg.com/pngimages/779/61/png-clipart-logo-idea-cute-eagle-leaf-logo-thumbnail.png";
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
            // Validar campos obligatorios
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                lblMensaje.Text = "El nombre del equipo es obligatorio";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                return;
            }

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

                if (!equipoModificar.Read())
                {
                    lblMensaje.Text = "El equipo no existe";
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    equipoModificar.Nombre = txtNombre.Text;
                    equipoModificar.Fecha_creacion = DateTime.Parse(txtFecha.Text);
                    equipoModificar.Logo_url = txtLogo.Text;
                    equipoModificar.Descripcion = txtDescripcion.Text;
                    equipoModificar.Id_capitan = int.Parse(hfIdCapitan.Value);

                    if (equipoModificar.Update())
                    {
                        lblMensaje.Text = "Equipo modificado correctamente";
                        lblMensaje.ForeColor = System.Drawing.Color.Green;

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

            List<ENJugador> todosJugadores = new CADJugador().ReadAll();
            List<ENJugador> jugadoresDisponibles = new List<ENJugador>();

            foreach (ENJugador j in todosJugadores)
            {
                if (j.Email_usuario == emailUsuario && j.Equipo_actual == 0)
                {
                    jugadoresDisponibles.Add(j);
                }
            }

            ddlJugadores.DataSource = jugadoresDisponibles;
            ddlJugadores.DataTextField = "Apodo";
            ddlJugadores.DataValueField = "Codigo";
            ddlJugadores.DataBind();

            if (ddlJugadores.Items.Count == 0)
            {
                lblMensaje.Text = "No tienes jugadores disponibles. Crea un jugador primero.";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                pnlSeleccionJugador.Visible = false;
            }
        }

        private void CargarJugadoresParaUnirse(int idequipo)
        {
            string emailUsuario = Session["Email"].ToString();

            ENEquipo equipoActual = new ENEquipo();
            equipoActual.Id_equipo = idequipo;
            equipoActual.Read();

            ENJugador capitan = new ENJugador();
            capitan.Codigo = equipoActual.Id_capitan;
            capitan.Read();
            int juegoCapitan = capitan.Juego;

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
            Response.Write("Acción: " + accionPendiente);
            if (ddlJugadores.SelectedIndex < 0)
            {
                lblMensaje.Text = "Selecciona un jugador";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                return;
            }

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
                nuevoEquipo.Nombre = txtNombre.Text;
                nuevoEquipo.Fecha_creacion = DateTime.Now;
                nuevoEquipo.Logo_url = txtLogo.Text;
                nuevoEquipo.Descripcion = txtDescripcion.Text;
                nuevoEquipo.Id_capitan = codigoCapitan;

                bool creado = nuevoEquipo.Create();
                Response.Write("Creado: " + creado);

                if (creado)
                {
                    int idEquipoCreado = ObtenerUltimoIdEquipo();

                    if (idEquipoCreado > 0)
                    {
                        capitan.Equipo_actual = idEquipoCreado;
                        capitan.Update();

                        lblMensaje.Text = "Equipo creado correctamente";
                        lblMensaje.ForeColor = System.Drawing.Color.Green;

                        Response.Redirect("~/Public/DetallesEquipo.aspx?id=" + idEquipoCreado);
                    }
                    else
                    {
                        lblMensaje.Text = "ERROR: No se pudo obtener el ID del equipo";
                        lblMensaje.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    lblMensaje.Text = "ERROR al crear el equipo. ¿El nombre ya existe?";
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error: " + ex.Message;
                lblMensaje.ForeColor = System.Drawing.Color.Red;
            }
        }

        private int ObtenerUltimoIdEquipo()
        {
            CADEquipo cad = new CADEquipo();
            return cad.GetLastId();
        }

        private void UnirseEquipo(int codigoJugador, int idEquipo)
        {
            try
            {
                ENJugador jugador = new ENJugador();
                jugador.Codigo = codigoJugador;
                jugador.Read();

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