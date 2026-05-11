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
            get { return ViewState["AccionPendiente"] as string; }
            set { ViewState["AccionPendiente"] = value; }
        }

        private int idEquipo
        {
            get { return ViewState["IdEquipo"] != null ? (int)ViewState["IdEquipo"] : 0; }
            set { ViewState["IdEquipo"] = value; }
        }

        private string emailLogueado;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Email"] != null)
            {
                emailLogueado = Session["Email"].ToString();
            }
            else
            {
                emailLogueado = null;
            }

            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    idEquipo = int.Parse(Request.QueryString["id"]);
                    CargarEquipo(idEquipo);

                    if (emailLogueado != null)
                    {
                        VerificarPermisos();
                    }
                    else
                    {
                        pnlAcciones.Visible = false;
                        pnlSeleccionJugador.Visible = false;
                        btnModificar.Visible = false;
                        btnEliminar.Visible = false;
                        btnUnirse.Visible = false;
                        btnCrear.Visible = false;
                        lblMensaje.Text = "Inicia sesión para poder crear equipos, unirte o modificar.";
                        lblMensaje.ForeColor = System.Drawing.Color.Blue;
                    }
                }
                else
                {
                    if (emailLogueado == null)
                    {
                        Response.Redirect("~/Public/Login.aspx");
                        return;
                    }

                    pnlAcciones.Visible = true;
                    pnlSeleccionJugador.Visible = false;

                    txtNombre.Text = "";
                    txtFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtDescripcion.Text = "";
                    txtLogo.Text = "";
                    lblCapitanNombre.Text = "No seleccionado";
                    hfIdCapitan.Value = "0";

                    btnModificar.Visible = false;
                    btnEliminar.Visible = false;
                    btnUnirse.Visible = false;
                    btnCrear.Visible = true;
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
                    ddlMaxJugadores.SelectedValue = equipo.Max_jugadores.ToString();
                    ddlMaxJugadores.Enabled = false; // No se puede modificar después de crear

                    // También mostrar en la sección de miembros
                    lblMaxJugadores.Text = $"Límite: {equipo.Max_jugadores} jugadores";

                    CargarMiembrosEquipo(id);

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

                    // Cargar miembros del equipo
                    CargarMiembrosEquipo(id);
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

        private void CargarMiembrosEquipo(int idEquipo)
        {
            try
            {
                List<ENJugador> todosJugadores = new CADJugador().ReadAll();
                List<dynamic> miembros = new List<dynamic>();
                int contador = 0;

                foreach (ENJugador j in todosJugadores)
                {
                    if (j.Equipo_actual == idEquipo)
                    {
                        contador++;
                        miembros.Add(new
                        {
                            j.Apodo,
                            j.Rol_principal,
                            j.Nivel,
                            Kda_promedio = j.Kda_promedio.ToString("0.00"),
                            Winrate = j.Winrate.ToString("0.0"),
                            EsCapitan = (j.Codigo == equipo.Id_capitan)
                        });
                    }
                }

                if (contador > 0)
                {
                    rptMiembros.DataSource = miembros;
                    rptMiembros.DataBind();
                    rptMiembros.Visible = true;
                    lblNoMiembros.Visible = false;
                    lblNumMiembros.Text = $"Miembros: {contador} / {equipo.Max_jugadores}";
                }
                else
                {
                    rptMiembros.Visible = false;
                    lblNoMiembros.Visible = true;
                    lblNumMiembros.Text = $"Miembros: 0 / {equipo.Max_jugadores}";
                }

                // Mostrar advertencia si está completo
                if (contador >= equipo.Max_jugadores)
                {
                    lblMensaje.Text = "Este equipo ya ha alcanzado su límite de jugadores.";
                    lblMensaje.ForeColor = System.Drawing.Color.Orange;
                    btnUnirse.Visible = false; // Ocultar botón unirse
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cargar miembros: {0}", ex.Message);
            }
        }

        private void VerificarPermisos()
        {
            if (string.IsNullOrEmpty(emailLogueado))
            {
                pnlAcciones.Visible = false;
                return;
            }

            pnlAcciones.Visible = true;

            if (equipo.Id_capitan == 0)
            {
                btnModificar.Visible = false;
                btnEliminar.Visible = false;
                btnUnirse.Visible = true;
                btnCrear.Visible = false;
                return;
            }

            ENJugador capitan = new ENJugador();
            capitan.Codigo = equipo.Id_capitan;

            if (!capitan.Read())
            {
                btnModificar.Visible = false;
                btnEliminar.Visible = false;
                btnUnirse.Visible = true;
                btnCrear.Visible = false;
                return;
            }

            bool esCapitan = (capitan.Email_usuario == emailLogueado);

            btnModificar.Visible = esCapitan;
            btnEliminar.Visible = esCapitan;
            btnCrear.Visible = false;
            btnUnirse.Visible = !esCapitan;
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Public/Equipos.aspx");
        }

        protected void btnCrear_Click(object sender, EventArgs e)
        {
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

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            if (!VerificarEsCapitan())
            {
                lblMensaje.Text = "No tienes permiso para modificar este equipo. Solo el capitán puede hacerlo.";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                return;
            }

            try
            {
                ENEquipo equipoModificar = new ENEquipo();
                equipoModificar.Id_equipo = idEquipo;

                if (!equipoModificar.Read())
                {
                    lblMensaje.Text = "El equipo no existe";
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                equipoModificar.Nombre = txtNombre.Text;
                equipoModificar.Fecha_creacion = DateTime.Parse(txtFecha.Text);
                equipoModificar.Logo_url = txtLogo.Text;
                equipoModificar.Descripcion = txtDescripcion.Text;

                if (equipoModificar.Update())
                {
                    lblMensaje.Text = "Equipo modificado correctamente";
                    lblMensaje.ForeColor = System.Drawing.Color.Green;

                    if (!string.IsNullOrEmpty(equipoModificar.Logo_url))
                    {
                        imgLogo.ImageUrl = equipoModificar.Logo_url;
                    }

                    CargarMiembrosEquipo(idEquipo);
                }
                else
                {
                    lblMensaje.Text = "ERROR al modificar el equipo";
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
            if (!VerificarEsCapitan())
            {
                lblMensaje.Text = "No tienes permiso para eliminar este equipo. Solo el capitán puede hacerlo.";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                return;
            }

            try
            {
                ENEquipo equipoEliminar = new ENEquipo();
                equipoEliminar.Id_equipo = idEquipo;

                if (!equipoEliminar.Read())
                {
                    lblMensaje.Text = "El equipo no existe";
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                if (equipoEliminar.Delete())
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

        protected void btnUnirse_Click(object sender, EventArgs e)
        {
            accionPendiente = "UNIRSE";
            CargarJugadoresParaUnirse(idEquipo);
            pnlSeleccionJugador.Visible = true;
        }

        private bool VerificarEsCapitan()
        {
            ENEquipo equipoActual = new ENEquipo();
            equipoActual.Id_equipo = idEquipo;

            if (!equipoActual.Read())
            {
                return false;
            }

            if (equipoActual.Id_capitan == 0)
            {
                return false;
            }

            ENJugador capitan = new ENJugador();
            capitan.Codigo = equipoActual.Id_capitan;

            if (!capitan.Read())
            {
                return false;
            }

            return (capitan.Email_usuario == emailLogueado);
        }

        private void CargarJugadoresDisponibles()
        {
            List<ENJugador> todosJugadores = new CADJugador().ReadAll();
            List<ENJugador> jugadoresDisponibles = new List<ENJugador>();

            foreach (ENJugador j in todosJugadores)
            {
                if (j.Email_usuario == emailLogueado && j.Equipo_actual == 0)
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

        private void CargarJugadoresParaUnirse(int idEquipo)
        {
            try
            {
                ENEquipo equipoActual = new ENEquipo();
                equipoActual.Id_equipo = idEquipo;
                equipoActual.Read();

                ENJugador capitan = new ENJugador();
                capitan.Codigo = equipoActual.Id_capitan;
                capitan.Read();
                int juegoCapitan = capitan.Juego;

                ENVideojuego videojuego = new ENVideojuego();
                videojuego.Codigo = juegoCapitan;
                videojuego.Read();
                int edadMinima = videojuego.EdadMinima;

                List<ENJugador> todosJugadores = new CADJugador().ReadAll();
                List<ENJugador> jugadoresValidos = new List<ENJugador>();

                foreach (ENJugador j in todosJugadores)
                {
                    if (j.Email_usuario == emailLogueado && j.Equipo_actual == 0 && j.Juego == juegoCapitan)
                    {
                        ENUsuario usuario = new ENUsuario();
                        usuario.Email = j.Email_usuario;
                        usuario.Read();
                        int edadJugador = CalcularEdad(usuario.Fecha_Nacimiento);

                        if (edadJugador >= edadMinima)
                        {
                            jugadoresValidos.Add(j);
                        }
                    }
                }

                ddlJugadores.DataSource = jugadoresValidos;
                ddlJugadores.DataTextField = "Apodo";
                ddlJugadores.DataValueField = "Codigo";
                ddlJugadores.DataBind();

                if (ddlJugadores.Items.Count == 0)
                {
                    lblMensaje.Text = "No tienes jugadores disponibles. Requisitos: mismo juego y edad mínima " + edadMinima + " años.";
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                    pnlSeleccionJugador.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error: " + ex.Message;
                lblMensaje.ForeColor = System.Drawing.Color.Red;
            }
        }

        private int CalcularEdad(DateTime fechaNacimiento)
        {
            DateTime hoy = DateTime.Today;
            int edad = hoy.Year - fechaNacimiento.Year;
            if (fechaNacimiento.Date > hoy.AddYears(-edad)) edad--;
            return edad;
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
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
                nuevoEquipo.Max_jugadores = int.Parse(ddlMaxJugadores.SelectedValue); // AÑADIR ESTO

                if (nuevoEquipo.Create())
                {
                    int idEquipoCreado = ObtenerIdEquipoPorNombre(txtNombre.Text);

                    if (idEquipoCreado > 0)
                    {
                        if (capitan.Equipo_actual != 0)
                        {
                            lblMensaje.Text = "Este jugador ya pertenece a un equipo";
                            return;
                        }
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

        private int ObtenerIdEquipoPorNombre(string nombre)
        {
            List<ENEquipo> equipos = new CADEquipo().ReadAll();
            foreach (ENEquipo eq in equipos)
            {
                if (eq.Nombre.ToLower() == nombre.ToLower())
                {
                    return eq.Id_equipo;
                }
            }
            return 0;
        }

        private void UnirseEquipo(int codigoJugador, int idEquipo)
        {
            try
            {
                // Verificar límite de jugadores
                ENEquipo equipoActual = new ENEquipo();
                equipoActual.Id_equipo = idEquipo;
                equipoActual.Read();

                // Contar miembros actuales
                List<ENJugador> todosJugadores = new CADJugador().ReadAll();
                int miembrosActuales = 0;
                foreach (ENJugador j in todosJugadores)
                {
                    if (j.Equipo_actual == idEquipo)
                    {
                        miembrosActuales++;
                    }
                }

                if (miembrosActuales >= equipoActual.Max_jugadores)
                {
                    lblMensaje.Text = $"No puedes unirte. El equipo ya alcanzó su límite de {equipoActual.Max_jugadores} jugadores.";
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                ENJugador jugador = new ENJugador();
                jugador.Codigo = codigoJugador;
                jugador.Read();

                if (jugador.Email_usuario != emailLogueado)
                {
                    lblMensaje.Text = "No puedes unir un jugador que no es tuyo";
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                if (jugador.Equipo_actual != 0)
                {
                    lblMensaje.Text = "Este jugador ya pertenece a un equipo";
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                foreach (ENJugador j in todosJugadores)
                {
                    if (j.Equipo_actual == idEquipo &&
                        j.Email_usuario == jugador.Email_usuario)
                    {
                        lblMensaje.Text = "Ya tienes otro jugador en este equipo";
                        lblMensaje.ForeColor = System.Drawing.Color.Red;
                        return;
                    }
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
        protected void rptMiembros_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                dynamic miembro = e.Item.DataItem;
                bool esCapitan = miembro.EsCapitan;

                System.Web.UI.HtmlControls.HtmlGenericControl divMiembro =
                    (System.Web.UI.HtmlControls.HtmlGenericControl)e.Item.FindControl("divMiembro");
                System.Web.UI.HtmlControls.HtmlGenericControl spanCapitan =
                    (System.Web.UI.HtmlControls.HtmlGenericControl)e.Item.FindControl("spanCapitan");

                if (esCapitan)
                {
                    divMiembro.Attributes["style"] = "border:1px solid #28a745; padding:10px; margin-bottom:10px; border-radius:5px; background-color:#d4edda;";
                    spanCapitan.Style["display"] = "inline-block";
                }
                else
                {
                    divMiembro.Attributes["style"] = "border:1px solid #ccc; padding:10px; margin-bottom:10px; border-radius:5px;";
                }
            }
        }
    }
}
