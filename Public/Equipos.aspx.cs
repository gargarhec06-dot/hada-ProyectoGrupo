using hada_ProyectoGrupo.Library.EN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace hada_ProyectoGrupo.Public
{
    public partial class Equipos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarEquipos();
                if (Session["EsAdmin"] != null && (bool)Session["EsAdmin"] == false)
                {
                    pnlJugador.Visible = true;
                }
            }
        }
        public void CargarEquipos()
        {
            try
            {
                ENEquipo equipo = new ENEquipo();
                List<ENEquipo> lista = equipo.ReadAll();  // Leer de la BD

                rptEquipos.DataSource = lista;
                rptEquipos.DataBind();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cargar equipos: {0}", ex.Message);
                Response.Write("<script>alert('Error al cargar los equipos');</script>");
            }
        }

        protected void btnCrear_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Public/DetallesEquipo.aspx");
        }
    }
}