using System;
using System.Collections.Generic;
using hada_ProyectoGrupo.Library.EN;

namespace hada_ProyectoGrupo.Public
{
    public partial class Patrocinadores : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarPatrocinadores();
                if (Session["EsAdmin"] != null && (bool)Session["EsAdmin"] == true) {
                    pnlAdmin.Visible = true;
                }
            }
        }

        private void CargarPatrocinadores()
        {
            List<ENPatrocinador> lista = new List<ENPatrocinador>
            {
                new ENPatrocinador { IdPatrocinador = 1, Nombre = "Red Bull", Email = "redbull@email.com", PaginaWeb = "https://www.redbull.com", InicioContrato = DateTime.Now, FinContrato = DateTime.Now.AddYears(1), Activo = true },
                new ENPatrocinador { IdPatrocinador = 2, Nombre = "Logitech", Email = "logitech@email.com", PaginaWeb = "https://www.logitech.com", InicioContrato = DateTime.Now, FinContrato = DateTime.Now.AddYears(1), Activo = true },
                new ENPatrocinador { IdPatrocinador = 3, Nombre = "Nvidia", Email = "nvidia@email.com", PaginaWeb = "https://www.nvidia.com", InicioContrato = DateTime.Now, FinContrato = DateTime.Now.AddMonths(6), Activo = false }
            };
            rptPatrocinadores.DataSource = lista;
            rptPatrocinadores.DataBind();
        }

        protected void btnCrear_Click(object sender, EventArgs e)
        {
                        Response.Redirect("~/private/GestionPatrocinadore.aspx");
        }

    }
}