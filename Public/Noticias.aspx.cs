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

                // Lógica segura: Si es null o no es admin, el panel se queda oculto
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

                rptNoticias.DataSource = lista;
                rptNoticias.DataBind();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cargar noticias: {0}", ex.Message);
                // No lanzamos alert para no molestar al usuario anónimo, 
                // solo si es un error crítico de conexión.
            }
        }

        protected void btnCrear_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Public/DetallesNoticia.aspx");
        }
    }
}