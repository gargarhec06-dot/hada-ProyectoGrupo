using hada_ProyectoGrupo.Library.CAD;
using hada_ProyectoGrupo.Library.EN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace hada_ProyectoGrupo.Public
{
    // DTO enriquecido para la vista — se construye en el code-behind
    // y se usa como DataSource del Repeater.
    public class EquipoViewModel
    {
        public int Id_equipo { get; set; }
        public string Nombre { get; set; }
        public string Logo_url { get; set; }
        public int MiembrosActuales { get; set; }
        public int MaxJugadores { get; set; }

        // Lógica de filtrado para el JS del cliente:
        // valores posibles: "todos" | "mio" | "unirse" | "lleno"
        public string EstadoFiltro { get; set; }

        // Clases y textos dinámicos calculados aquí para mantener
        // el .aspx libre de lógica compleja.
        public string BadgeClass { get; set; }
        public string BadgeTexto { get; set; }
        public string AccionTexto { get; set; }
    }

    public partial class Equipos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarEquipos();

                // Mostrar controles solo para jugadores autenticados
                bool esJugador = Session["EsAdmin"] != null && (bool)Session["EsAdmin"] == false;
                pnlJugador.Visible = esJugador;
                pnlBtnUnirse.Visible = esJugador;

                // Mostrar "Mi equipo" solo si el jugador pertenece a alguno
                if (esJugador)
                {
                    int idJugador = ObtenerIdJugadorSesion();
                    pnlBtnMiEquipo.Visible = idJugador > 0 && JugadorTieneEquipo(idJugador);
                }
            }
        }

        // ----------------------------------------------------------------
        // Carga y transforma la lista de equipos en EquipoViewModel
        // ----------------------------------------------------------------
        public void CargarEquipos()
        {
            try
            {
                CADEquipo cad = new CADEquipo();
                List<EquipoConMiembros> listaRaw = cad.ReadAllConMiembros();  // ← Usar este método

                int idJugador = ObtenerIdJugadorSesion();
                bool esJugador = Session["EsAdmin"] != null && (bool)Session["EsAdmin"] == false;

                int idEquipoDelJugador = esJugador && idJugador > 0
                    ? ObtenerEquipoDeJugador(idJugador)
                    : 0;

                var lista = listaRaw.Select(e =>
                {
                    bool lleno = e.MiembrosActuales >= e.Max_jugadores;
                    bool esMio = idEquipoDelJugador > 0 && e.Id_equipo == idEquipoDelJugador;

                    string estado;
                    if (esMio) estado = "mio";
                    else if (lleno) estado = "lleno";
                    else estado = "unirse";

                    string badgeClass, badgeTexto, accionTexto;
                    if (esMio)
                    {
                        badgeClass = "badge-mine";
                        badgeTexto = "Mi equipo";
                        accionTexto = "Ver mi equipo";
                    }
                    else if (lleno)
                    {
                        badgeClass = "badge-full";
                        badgeTexto = "Lleno";
                        accionTexto = "Ver detalle";
                    }
                    else
                    {
                        badgeClass = "badge-open";
                        badgeTexto = "Abierto";
                        accionTexto = (esJugador && idEquipoDelJugador == 0) ? "Unirse" : "Ver detalle";
                    }

                    return new EquipoViewModel
                    {
                        Id_equipo = e.Id_equipo,
                        Nombre = e.Nombre,
                        Logo_url = e.Logo_url,
                        MiembrosActuales = e.MiembrosActuales,
                        MaxJugadores = e.Max_jugadores,
                        EstadoFiltro = estado,
                        BadgeClass = badgeClass,
                        BadgeTexto = badgeTexto,
                        AccionTexto = accionTexto
                    };
                }).ToList();

                rptEquipos.DataSource = lista;
                rptEquipos.DataBind();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error al cargar equipos: " + ex.Message);
                Response.Write("<script>alert('Error al cargar los equipos');</script>");
            }
        }

        private int ObtenerIdJugadorSesion()
        {
            if (Session["CodigoJugador"] == null)
            {
                // Intentar obtener el jugador por email
                string email = Session["Email"]?.ToString();
                if (!string.IsNullOrEmpty(email))
                {
                    ENJugador jugador = new ENJugador();
                    jugador.Email_usuario = email;
                    // Necesitas un método ReadByEmail en CADJugador
                    // Por ahora, devolvemos 0
                }
                return 0;
            }
            return Convert.ToInt32(Session["CodigoJugador"]);
        }

        private bool JugadorTieneEquipo(int idJugador)
        {
            return ObtenerEquipoDeJugador(idJugador) > 0;
        }

        private int ObtenerEquipoDeJugador(int idJugador)
        {
            try
            {
                ENJugador j = new ENJugador();
                j.Codigo = idJugador;
                if (j.Read())  // ← Usar Read() en lugar de ReadOne()
                {
                    return j.Equipo_actual;
                }
                return 0;
            }
            catch
            {
                return 0;
            }
        }
        protected void rptEquipos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item ||
                e.Item.ItemType == ListItemType.AlternatingItem)
            {
                dynamic data = e.Item.DataItem;

                int actuales = Convert.ToInt32(DataBinder.Eval(data, "MiembrosActuales"));
                int maximos = Convert.ToInt32(DataBinder.Eval(data, "MaxJugadores"));

                int porcentaje = (actuales * 100) / maximos;

                HtmlGenericControl barra =
                    (HtmlGenericControl)e.Item.FindControl("barra");

                barra.Style["width"] = porcentaje + "%";
            }
        }

        // ----------------------------------------------------------------
        // Eventos
        // ----------------------------------------------------------------
        protected void btnCrear_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Public/DetallesEquipo.aspx");
        }
    }
}