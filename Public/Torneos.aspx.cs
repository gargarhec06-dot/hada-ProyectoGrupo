using hada_ProyectoGrupo.Library.EN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace hada_ProyectoGrupo.Public
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarTorneos();

                if (Session["EsAdmin"] != null && (bool)Session["EsAdmin"] == true)
                {
                    pnlAdmin.Visible = true;
                }
            }
        }

        private void CargarTorneos()
        {
            ENTorneo en = new ENTorneo();
            List<ENTorneo> lista = en.ReadAll(); // en la siguiente entrega en vez de leer datos que se han puesto manualmente lo hará desde la base de datos.

            rptTorneos.DataSource = lista;
            rptTorneos.DataBind();
        }

        

        protected void btnCrear_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Private/GestionTorneo.aspx");
        }
    }
}