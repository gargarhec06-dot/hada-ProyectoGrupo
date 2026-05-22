using System;
using System.Web.UI;
using System.Collections.Generic;
using hada_ProyectoGrupo.Library.CAD;
using hada_ProyectoGrupo.Library.EN;

namespace hada_ProyectoGrupo.Private
{
    public partial class PerfilUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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

            string nombre = Session["Nombre"]?.ToString();
            lblNombre.Text = string.IsNullOrEmpty(nombre) ? "Usuario" : nombre;
            lblEmail.Text = email;
            lblRol.Text = esAdmin ? "Administrador" : "Jugador";

            bool esUsuarioNormal = !esAdmin;
            pnlMisJugadores.Visible = esUsuarioNormal;
            pnlSumarFondos.Visible = esUsuarioNormal;
            pnlSaldoRow.Visible = esUsuarioNormal;

            try
            {
                CADUsuario cadUsuario = new CADUsuario();
                ENUsuario usuario = new ENUsuario();
                usuario.Email = email;

                if (cadUsuario.Read(usuario))
                {
                    lblFechaNacimiento.Text = usuario.Fecha_Nacimiento.ToString("dd/MM/yyyy");
                    if (esUsuarioNormal)
                        lblSaldo.Text = usuario.Saldo_cartera.ToString("F2") + " €";
                }
                else
                {
                    lblFechaNacimiento.Text = "No disponible";
                    if (esUsuarioNormal)
                        lblSaldo.Text = "0,00 €";
                }
            }
            catch (Exception)
            {
                lblFechaNacimiento.Text = "No disponible";
                if (!esAdmin)
                    lblSaldo.Text = "0,00 €";
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
                    ENEquipo equipo = cadEquipo.ReadByCapitan(jugador.Codigo);
                    if (equipo != null)
                    {
                        cadInscripcion.DeleteByEquipo(equipo.Id_equipo);
                        cadEquipo.Delete(equipo);
                    }
                    else if (jugador.Equipo_actual != 0)
                    {
                        cadJugador.QuitarDeEquipo(jugador.Codigo);
                    }
                    cadJugador.Delete(jugador);
                }

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