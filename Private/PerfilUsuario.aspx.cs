using System;
using System.Web.UI;
using hada_ProyectoGrupo.Library.CAD;
using hada_ProyectoGrupo.Library.EN;
using System.Collections.Generic;

namespace hada_ProyectoGrupo.Private
{
    public partial class PerfilUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Verificar sesión
            if (Session["Email"] == null)
            {
                Response.Redirect("~/Public/Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                CargarDatosUsuario();
            }
        }

        private void CargarDatosUsuario()
        {
            string email = Session["Email"].ToString();
            bool esAdmin = Session["EsAdmin"] != null && (bool)Session["EsAdmin"];

            // Datos desde la sesión
            string nombre = Session["Nombre"]?.ToString();
            lblNombre.Text = string.IsNullOrEmpty(nombre) ? "Usuario" : nombre;
            lblEmail.Text = email;
            lblRol.Text = esAdmin ? "Administrador" : "Jugador";

            // Ocultar botón "Mis Jugadores" si es administrador 
            pnlMisJugadores.Visible = !esAdmin;

            // Cargar datos adicionales desde la BD (fecha registro)
            try
            {
                CADUsuario cadUsuario = new CADUsuario();
                ENUsuario usuario = new ENUsuario();
                usuario.Email = email;

                if (cadUsuario.Read(usuario))
                {
                    lblFechaNacimiento.Text = usuario.Fecha_Nacimiento.ToString("dd/MM/yyyy");
                    lblSaldo.Text = usuario.Saldo_cartera.ToString("F2") + " €";
                }
                else
                {
                    lblFechaNacimiento.Text = "No disponible";
                }
            }
            catch (Exception)
            {
                lblFechaNacimiento.Text = "No disponible";
            }
        }

        protected void btnEditarPerfil_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Private/EditarPerfil.aspx");
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/Default.aspx");
        }

        protected void btnMisJugadores_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Public/Jugadores.aspx");
        }

        protected void btnSumarFondos_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Private/AnadirSaldo.aspx");
        }

        protected void btnEliminarCuenta_Click(object sender, EventArgs e)
        {
            string email = Session["Email"].ToString();

            try
            {
                CADJugador cadJugador = new CADJugador();
                CADEquipo cadEquipo = new CADEquipo();
                CADInscripcion cadInscripcion = new CADInscripcion();
                CADUsuario cadUsuario = new CADUsuario();

                List<ENJugador> jugadores = cadJugador.ReadAllByEmail(email);

                foreach (ENJugador jugador in jugadores)
                {
                    // Si es capitán de un equipo, eliminar inscripciones y equipo
                    ENEquipo equipo = cadEquipo.ReadByCapitan(jugador.Codigo);
                    if (equipo != null)
                    {
                        // 1. Eliminar inscripciones del equipo en torneos
                        cadInscripcion.DeleteByEquipo(equipo.Id_equipo);

                        // 2. Eliminar el equipo (esto expulsa a los demás jugadores automáticamente)
                        cadEquipo.Delete(equipo);
                    }
                    else if (jugador.Equipo_actual != 0)
                    {
                        // Si está en un equipo pero no es capitán, simplemente salir del equipo
                        cadJugador.QuitarDeEquipo(jugador.Codigo);
                    }

                    // 3. Eliminar el jugador
                    cadJugador.Delete(jugador);
                }

                // 4. Eliminar el usuario
                ENUsuario usuario = new ENUsuario();
                usuario.Email = email;

                if (cadUsuario.Read(usuario))
                {
                    if (cadUsuario.Delete(usuario))
                    {
                        Session.Clear();
                        Session.Abandon();
                        Response.Redirect("~/Default.aspx?mensaje=Cuenta eliminada correctamente");
                    }
                    else
                    {
                        Response.Write("<script>alert('No se pudo eliminar la cuenta');</script>");
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message.Replace("'", "\\'") + "');</script>");
            }
        }
    }
}