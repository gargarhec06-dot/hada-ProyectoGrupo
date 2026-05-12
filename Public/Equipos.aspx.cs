using hada_ProyectoGrupo.Library.CAD;
using hada_ProyectoGrupo.Library.EN;
using System;
using System.Collections.Generic;
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
        public string EstadoFiltro { get; set; }
        public string BadgeClass { get; set; }
        public string BadgeTexto { get; set; }
    }

    public partial class Equipos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarEquipos();
            }
        }

        private void CargarEquipos()
        {
            try
            {
                // *** CAMBIO CLAVE: usamos ReadAllConMiembros() en lugar de ReadAll() ***
                // Así MiembrosActuales viene calculado directamente desde la BD con COUNT
                List<EquipoConMiembros> listaEquipos = new CADEquipo().ReadAllConMiembros();

                bool esAdmin = Session["EsAdmin"] != null && (bool)Session["EsAdmin"];
                bool estaLogueado = Session["Email"] != null;
                string emailLogueado = estaLogueado ? Session["Email"].ToString() : null;

                pnlJugador.Visible = estaLogueado && !esAdmin;

                // Recogemos todos los códigos de jugadores del usuario logueado
                List<int> misCodigosJugador = new List<int>();
                if (emailLogueado != null)
                {
                    List<ENJugador> todosJugadores = new CADJugador().ReadAll();
                    foreach (ENJugador j in todosJugadores)
                    {
                        if (j.Email_usuario == emailLogueado)
                        {
                            misCodigosJugador.Add(j.Codigo);
                        }
                    }
                }

                List<EquipoViewModel> vista = new List<EquipoViewModel>();

                foreach (EquipoConMiembros eq in listaEquipos)
                {
                    bool soyElCapitan = misCodigosJugador.Contains(eq.Id_capitan);
                    bool estaLleno = eq.MiembrosActuales >= eq.Max_jugadores;

                    string estado = "abierto";
                    if (soyElCapitan) estado = "mio";
                    else if (estaLleno) estado = "lleno";

                    string finalLogoUrl = !string.IsNullOrWhiteSpace(eq.Logo_url)
                        ? eq.Logo_url
                        : "~/Images/Equipos/default-team.png";

                    vista.Add(new EquipoViewModel
                    {
                        Id_equipo = eq.Id_equipo,
                        Nombre = eq.Nombre,
                        Logo_url = ResolveUrl(finalLogoUrl),
                        MiembrosActuales = eq.MiembrosActuales, // Ahora viene el valor real de la BD
                        MaxJugadores = eq.Max_jugadores,
                        EstadoFiltro = estado,
                        BadgeClass = soyElCapitan ? "badge-mine" : (estaLleno ? "badge-full" : "badge-open"),
                        BadgeTexto = soyElCapitan ? "Mi Equipo" : (estaLleno ? "Lleno" : "Abierto")
                    });
                }

                rptEquipos.DataSource = vista;
                rptEquipos.DataBind();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error en CargarEquipos: " + ex.Message);
            }
        }

        protected void rptEquipos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var data = (EquipoViewModel)e.Item.DataItem;
                if (data.MaxJugadores > 0)
                {
                    double porcentaje = (double)data.MiembrosActuales * 100 / data.MaxJugadores;
                    HtmlGenericControl barra = (HtmlGenericControl)e.Item.FindControl("barra");
                    if (barra != null)
                    {
                        barra.Style["width"] = porcentaje.ToString(System.Globalization.CultureInfo.InvariantCulture) + "%";
                    }
                }
            }
        }

        protected void btnCrear_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Public/DetallesEquipo.aspx");
        }
    }
}