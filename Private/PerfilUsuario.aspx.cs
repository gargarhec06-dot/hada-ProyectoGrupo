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
                List<ENJugador> jugadores = cadJugador.ReadAllByEmail(email);

                // Comprobar si algún jugador es capitán de algún equipo
                List<string> equiposComoCapitan = new List<string>();
                foreach (ENJugador jugador in jugadores)
                {
                    ENEquipo equipo = cadEquipo.ReadByCapitan(jugador.Codigo);
                    if (equipo != null)
                    {
                        equiposComoCapitan.Add(equipo.Nombre);
                    }
                }

                if (equiposComoCapitan.Count > 0)
                {
                    string nombres = string.Join(", ", equiposComoCapitan);
                    Response.Write("<script>alert('No puedes eliminar la cuenta porque eres capitán de los siguientes equipos: " + nombres + ". Elimínalos primero.');</script>");
                    return;
                }

                // Si no es capitán de ningún equipo, proceder con la eliminación
                foreach (ENJugador jugador in jugadores)
                {
                    cadJugador.QuitarDeEquipo(jugador.Codigo);
                    cadJugador.Delete(jugador);
                }

                CADUsuario cadUsuario = new CADUsuario();
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