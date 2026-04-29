using hada_ProyectoGrupo.Library.EN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace hada_ProyectoGrupo.Public
{
    public partial class DetallesEquipo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    int id = int.Parse(Request.QueryString["id"]);
                    CargarEquipo(id);
                }
                else
                {
                    Response.Redirect("~/Public/Patrocinadores.aspx");
                }
            }
        }

        private void CargarEquipo(int id)
        {
            // De momento datos de ejemplo, se implementará con la BD más adelante
            ENEquipo p = new ENEquipo();
            p.Id_equipo = id;
            p.Nombre = "Equipo ejemplo";
            p.Fecha_creacion = DateTime.Now;
            p.Logo_url = "https://upload.wikimedia.org/wikipedia/commons/thumb/c/c7/TSM_Logo.svg/500px-TSM_Logo.svg.png";
            p.Descripcion = "Especializado en shooters";
            p.Id_capitan = id;

            lblNombre.Text = p.Nombre;
            lblFecha.Text = p.Fecha_creacion.ToShortDateString();
            lblDescripción.Text = p.Descripcion;
            lblCapitan.Text = p.Id_capitan.ToString();
            imgLogo.ImageUrl = p.Logo_url;
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Public/Equipos.aspx");
        }
    }
}