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
    public class EquipoViewModel
    {
        public int Id_equipo { get; set; }
        public string Nombre { get; set; }
        public string Logo_url { get; set; }
        public int MiembrosActuales { get; set; }
        public int MaxJugadores { get; set; }
        public string EstadoFiltro { get; set; } // "mio" | "unirse" | "lleno"
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
                // Detectar estado del usuario
                bool estaLogueado = Session["Email"] != null;
                bool esAdmin = Session["EsAdmin"] != null && (bool)Session["EsAdmin"];
                bool esJugador = estaLogueado && !esAdmin;

                CargarEquipos();

                // Configurar visibilidad de controles de filtrado
                pnlJugador.Visible = esJugador;
                pnlBtnUnirse.Visible = estaLogueado;

                if (esJugador)
                {
                    int idJugador = ObtenerIdJugadorSesion();
                    pnlBtnMiEquipo.Visible = idJugador > 0 && JugadorTieneEquipo(idJugador);
                }
            }
        }

        public void CargarEquipos()
        {
            try
            {
                CADEquipo cad = new CADEquipo();
                List<EquipoConMiembros> listaRaw = cad.ReadAllConMiembros();

                // 1. Obtener datos del usuario actual (no solo del jugador)
                string emailUsuario = Session["Email"]?.ToString();
                bool estaLogueado = !string.IsNullOrEmpty(emailUsuario);
                bool esAdmin = Session["EsAdmin"] != null && (bool)Session["EsAdmin"];
                bool esJugador = estaLogueado && !esAdmin;

                // 2. Obtener la lista de IDs de equipos donde este USUARIO ya tiene algún jugador
                // Esto evita que sus otros jugadores se unan al mismo equipo.
                List<int> equiposOcupadosPorUsuario = obtenerEquiposDelUsuario(emailUsuario);

                // ID del jugador que está "activo" en la sesión actualmente
                int idJugadorSesion = ObtenerIdJugadorSesion();
                int idEquipoJugadorActual = (idJugadorSesion > 0) ? ObtenerEquipoDeJugador(idJugadorSesion) : 0;

                var lista = listaRaw.Select(e =>
                {
                    bool lleno = e.MiembrosActuales >= e.Max_jugadores;

                    // ¿El jugador actual de la sesión ya está en este equipo?
                    bool esMiEquipoActual = idEquipoJugadorActual > 0 && e.Id_equipo == idEquipoJugadorActual;

                    // ¿El usuario (dueño) ya tiene ALGÚN otro jugador en este equipo?
                    bool usuarioYaEstaAqui = equiposOcupadosPorUsuario.Contains(e.Id_equipo);

                    // Definir estado para el filtro
                    string estado = esMiEquipoActual ? "mio" : (lleno ? "lleno" : "unirse");

                    string badgeClass, badgeTexto, accionTexto;

                    if (esMiEquipoActual)
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
                    else if (usuarioYaEstaAqui)
                    {
                        // RESTRICCIÓN: El usuario ya tiene otro jugador aquí
                        badgeClass = "badge-full"; // Usamos estilo de lleno/bloqueado
                        badgeTexto = "Ya tienes un jugador aquí";
                        accionTexto = "Bloqueado";
                    }
                    else
                    {
                        badgeClass = "badge-open";
                        badgeTexto = "Abierto";
                        // Solo permite "Unirse" si el jugador actual no tiene equipo Y el usuario no tiene a nadie aquí
                        accionTexto = (esJugador && idEquipoJugadorActual == 0) ? "Unirse" : "Ver detalle";
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
            catch (Exception ex) { Console.WriteLine("Error al cargar equipo: {0}", ex.Message); }
        }

        // Nueva función de apoyo
        private List<int> obtenerEquiposDelUsuario(string email)
        {
            List<int> ids = new List<int>();
            if (string.IsNullOrEmpty(email)) return ids;

            try
            {
                // Aquí debes llamar a tu CAD o EN para obtener todos los jugadores 
                // asociados a ese Email y retornar sus IDs de equipo.
                // Ejemplo hipotético:
                // ENJugador j = new ENJugador();
                // ids = j.ObtenerEquiposPorEmailUsuario(email); 
            }
            catch { }

            return ids;
        }

        private int ObtenerIdJugadorSesion()
        {
            if (Session["CodigoJugador"] != null) return Convert.ToInt32(Session["CodigoJugador"]);

            // Si no está el código, intentamos buscarlo por Email (Fallback)
            string email = Session["Email"]?.ToString();
            if (!string.IsNullOrEmpty(email))
            {
                // Aquí podrías llamar a un método de tu CAD para obtener el ID por email
            }
            return 0;
        }

        private bool JugadorTieneEquipo(int idJugador) => ObtenerEquipoDeJugador(idJugador) > 0;

        private int ObtenerEquipoDeJugador(int idJugador)
        {
            try
            {
                ENJugador j = new ENJugador { Codigo = idJugador };
                return j.Read() ? j.Equipo_actual : 0;
            }
            catch { return 0; }
        }

        protected void rptEquipos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var data = (EquipoViewModel)e.Item.DataItem;
                int porcentaje = (data.MiembrosActuales * 100) / data.MaxJugadores;
                HtmlGenericControl barra = (HtmlGenericControl)e.Item.FindControl("barra");
                if (barra != null) barra.Style["width"] = porcentaje + "%";
            }
        }

        protected void btnCrear_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Public/DetallesEquipo.aspx");
        }
    }
}