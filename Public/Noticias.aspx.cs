using hada_ProyectoGrupo.Library.EN;
using System;
using System.Collections.Generic;
using System.Web.UI;

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
            List<ENNoticia> lista = new List<ENNoticia>
            {
                new ENNoticia(1, "¡Nuevo Torneo!", "Se anuncia la competición de verano.", DateTime.Now, "admin@hada.com"),
                new ENNoticia(2, "Actualización", "Nuevos parches y mejoras de rendimiento.", DateTime.Now, "admin@hada.com"),
                new ENNoticia(3, "Resultados", "Ya tenemos a los campeones del split.", DateTime.Now, "admin@hada.com")
            };

            rptNoticias.DataSource = lista;
            rptNoticias.DataBind();
        }

        protected void btnCrear_Click(object sender, EventArgs e)
        {
            Response.Redirect("CrearNoticia.aspx");
        }
    }
}