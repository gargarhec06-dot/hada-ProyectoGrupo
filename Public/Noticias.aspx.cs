using hada_ProyectoGrupo.Library.EN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace hada_ProyectoGrupo.Public
{
    public partial class Noticias : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarNoticias();

                // Lógica de panel de administración
                if (Session["EsAdmin"] != null && (bool)Session["EsAdmin"] == true)
                {
                    pnlAdmin.Visible = true;
                }
                else
                {
                    pnlAdmin.Visible = false;
                }
            }
        }

       
        public void CargarNoticias(string autor = null, DateTime? fechaDesde = null, DateTime? fechaHasta = null)
        {
            try
            {
                ENNoticia noticia = new ENNoticia();
                List<ENNoticia> lista = noticia.ReadAll();

                // LÓGICA DE FILTRADO 

                // Filtro por Autor (Email)
                if (!string.IsNullOrEmpty(autor))
                {
                    lista = lista.Where(n => n.EmailUsuario != null &&
                        n.EmailUsuario.IndexOf(autor, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
                }

                // Filtro por Fecha Desde
                if (fechaDesde.HasValue)
                {
                    lista = lista.Where(n => n.FechaPublicacion.Date >= fechaDesde.Value.Date).ToList();
                }

                // Filtro por Fecha Hasta
                if (fechaHasta.HasValue)
                {
                    lista = lista.Where(n => n.FechaPublicacion.Date <= fechaHasta.Value.Date).ToList();
                }

                //  PROCESAMIENTO DE IMÁGENES
                foreach (ENNoticia n in lista)
                {
                    string rutaImagen = !string.IsNullOrWhiteSpace(n.ImagenUrl)
                                        ? n.ImagenUrl
                                        : "~/Images/Noticias/default-news.png";

                    n.ImagenUrl = ResolveUrl(rutaImagen);
                }

                rptNoticias.DataSource = lista;
                rptNoticias.DataBind();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error al cargar noticias: " + ex.Message);
            }
        }

        // Evento del botón Filtrar
        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            string autor = txtFiltroAutor.Text.Trim();
            DateTime? fechaDesde = null;
            DateTime? fechaHasta = null;

            if (DateTime.TryParse(txtFechaDesde.Text, out DateTime fDesde))
                fechaDesde = fDesde;

            if (DateTime.TryParse(txtFechaHasta.Text, out DateTime fHasta))
                fechaHasta = fHasta;

            CargarNoticias(autor, fechaDesde, fechaHasta);
        }

        // Evento del botón Limpiar
        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtFiltroAutor.Text = "";
            txtFechaDesde.Text = "";
            txtFechaHasta.Text = "";
            CargarNoticias();
        }

        protected void btnCrear_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Public/DetallesNoticia.aspx");
        }
    }
}