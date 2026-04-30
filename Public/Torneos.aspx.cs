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
            //ENTorneo en = new ENTorneo();
            //List<ENTorneo> lista = en.ReadAll(); 

            List<ENTorneo> lista = new List<ENTorneo>
            {
                new ENTorneo(101, 1, 20.0f, "Valorant Cup 2026",
                    "Torneo élite de estrategia.", true, 800.0f,
                    DateTime.Now.AddDays(10)),

                new ENTorneo(102, 2, 5.0f, "FIFA championship",
                    "Torneo abierto para todos.", false, 150.0f,
                    DateTime.Now.AddDays(25)),

                new ENTorneo(103, 1, 10.0f, "Torneo Invitacional",
                    "Solo jugadores invitados.", true, 400.0f,
                    DateTime.Now.AddDays(40))
            };

            rptTorneos.DataSource = lista;
            rptTorneos.DataBind();
        }

        

        protected void btnCrear_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Private/GestionTorneo.aspx");
        }
    }
}