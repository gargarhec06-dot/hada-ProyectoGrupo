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

                
                if (Session["EsAdmin"] != null && (bool)Session["EsAdmin"] == true)
                {
                    pnlAdmin.Visible = true;
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
                Response.Write("<script>alert('Error al cargar las noticias');</script>");
            }
        }

        protected void btnCrear_Click(object sender, EventArgs e)
        {
            // Redirigimos a la página de detalles (modo creación) 
            Response.Redirect("~/Public/DetallesNoticia.aspx");
        }
    }
}