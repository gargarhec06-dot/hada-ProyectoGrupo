using hada_ProyectoGrupo.Library.CAD;
using hada_ProyectoGrupo.Library.EN;
using System;
using System.Collections.Generic;

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
            CADPatrocinador cad = new CADPatrocinador();
            List<ENPatrocinador> lista = cad.ReadAll();
            rptPatrocinadores.DataSource = lista;
            rptPatrocinadores.DataBind();
        }

        protected void btnCrear_Click(object sender, EventArgs e)
        {
                        Response.Redirect("~/private/GestionPatrocinadore.aspx");
        }

    }
}