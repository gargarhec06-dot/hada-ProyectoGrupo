using hada_ProyectoGrupo.Library.EN;
using System;
using System.Collections.Generic;
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

        public void CargarNoticias()
        {
            try
            {
                ENNoticia noticia = new ENNoticia();
                List<ENNoticia> lista = noticia.ReadAll();

                // --- PROCESAMIENTO DE IMÁGENES 
                foreach (ENNoticia n in lista)
                {
                    // Si no tiene imagen, ponemos la por defecto
                    string rutaImagen = !string.IsNullOrWhiteSpace(n.ImagenUrl)
                                        ? n.ImagenUrl
                                        : "~/Images/Noticias/default-news.png";

                    // Resolvemos la URL para que el navegador la encuentre siempre
                    n.ImagenUrl = ResolveUrl(rutaImagen);
                }

                rptNoticias.DataSource = lista;
                rptNoticias.DataBind();
            }
            catch (Exception ex)
            {
                // Cambiado a System.Diagnostics para verlo en la consola de salida de VS
                System.Diagnostics.Debug.WriteLine("Error al cargar noticias: " + ex.Message);
            }
        }

        protected void btnCrear_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Public/DetallesNoticia.aspx");
        }
    }
}