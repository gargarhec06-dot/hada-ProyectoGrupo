using hada_ProyectoGrupo.Library.EN;
using hada_ProyectoGrupo.Library.CAD;
using System;
using System.Collections.Generic;
using System.Web.UI;

namespace hada_ProyectoGrupo.Public
{
    public class JugadorViewModel
    {
        public int Codigo { get; set; }
        public string Apodo { get; set; }
        public string Rol_principal { get; set; }
        public string Email_usuario { get; set; }
        public string NombreEquipo { get; set; }
        public string LogoEquipo { get; set; }

        // Valor leído por el JS del cliente para los botones de filtro:
        // "mio" | "con-equipo" | "sin-equipo"
        public string EstadoFiltro { get; set; }
    }

    public partial class Jugadores : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarJugadores();

            // Botón crear: solo para jugadores autenticados
            if (Session["EsAdmin"] != null && (bool)Session["EsAdmin"] == false)
                pnlAdmin3.Visible = true;
        }

        private void CargarJugadores()
        {
            try
            {
                List<ENJugador> todosLosJugadores = new ENJugador().ReadAll();

                // Mapa de equipos para lookup O(1)
                CADEquipo cadEquipo = new CADEquipo();
                List<ENEquipo> todosEquipos = cadEquipo.ReadAll();
                Dictionary<int, ENEquipo> mapEquipos = new Dictionary<int, ENEquipo>();
                foreach (ENEquipo eq in todosEquipos)
                    mapEquipos[eq.Id_equipo] = eq;

                bool esJugador = Session["EsAdmin"] != null && (bool)Session["EsAdmin"] == false;
                string emailLogueado = Session["Email"]?.ToString() ?? "";

                // Mostrar botón "Mis jugadores" solo si hay sesión de jugador
                pnlBtnMisJugadores.Visible = esJugador && !string.IsNullOrEmpty(emailLogueado);

                List<JugadorViewModel> vista = new List<JugadorViewModel>();

                foreach (ENJugador j in todosLosJugadores)
                {
                    // ── CAMBIO RESPECTO A LA VERSIÓN ANTERIOR ──
                    // Antes se filtraba aquí y el jugador solo veía los suyos.
                    // Ahora se muestran TODOS; el filtro "Mis jugadores" se hace
                    // en cliente con data-estado="mio".

                    bool esMio = esJugador && j.Email_usuario == emailLogueado;
                    bool tieneEquipo = j.Equipo_actual > 0;

                    string estadoFiltro;
                    if (esMio) estadoFiltro = "mio";          // propio (cuenta como con-equipo)
                    else if (tieneEquipo) estadoFiltro = "con-equipo";
                    else estadoFiltro = "sin-equipo";

                    JugadorViewModel vm = new JugadorViewModel
                    {
                        Codigo = j.Codigo,
                        Apodo = j.Apodo,
                        Rol_principal = string.IsNullOrEmpty(j.Rol_principal) ? "Sin rol" : j.Rol_principal,
                        Email_usuario = j.Email_usuario,
                        EstadoFiltro = estadoFiltro
                    };

                    // Resolver logo del equipo
                    if (tieneEquipo && mapEquipos.ContainsKey(j.Equipo_actual))
                    {
                        ENEquipo equipo = mapEquipos[j.Equipo_actual];
                        vm.NombreEquipo = equipo.Nombre;

                        string logo = equipo.Logo_url ?? "";
                        if (string.IsNullOrEmpty(logo))
                        {
                            vm.LogoEquipo = "";
                        }
                        else if (logo.StartsWith("http://") || logo.StartsWith("https://"))
                        {
                            vm.LogoEquipo = logo;
                        }
                        else
                        {
                            if (!logo.StartsWith("~/"))
                                logo = "~/" + logo.TrimStart('/');
                            vm.LogoEquipo = logo;
                        }
                    }
                    else
                    {
                        vm.NombreEquipo = "Sin equipo";
                        vm.LogoEquipo = "";
                    }

                    vista.Add(vm);
                }

                rptJugadores.DataSource = vista;
                rptJugadores.DataBind();

                if (vista.Count == 0)
                {
                    lblMensaje.Text = "No hay jugadores registrados.";
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error: " + ex.Message;
                lblMensaje.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnCrear_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/private/Jugador.aspx");
        }
    }
}