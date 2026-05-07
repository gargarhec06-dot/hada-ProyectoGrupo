using hada_ProyectoGrupo.Library.CAD;
using hada_ProyectoGrupo.Library.EN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace hada_ProyectoGrupo.Public
{
    public partial class DetalleTorneo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["codigo"] == null)
                {
                    MostrarError();
                    return;
                }

                int codigo = int.Parse(Request.QueryString["codigo"]);

                ENTorneo enTemp = new ENTorneo();
                List<ENTorneo> lista = enTemp.ReadAll();
                ENTorneo en = lista.FirstOrDefault(t => t.Codigo == codigo);

                if (en != null)
                {
                    MostrarTorneo(en);
                    CargarEquipos(codigo);
                }
                else
                {
                    MostrarError();
                }
            }
        }

        private void MostrarTorneo(ENTorneo t)
        {
            lblNombre.Text = t.Nombre;
            lblCodigo.Text = t.Codigo.ToString();
            lblProfesional.Text = t.Profesional ? " Profesional" : " Amateur";
            lblDescripcion.Text = string.IsNullOrEmpty(t.Descripcion)
                                            ? "Sin descripción" : t.Descripcion;
            lblUbicacion.Text = string.IsNullOrEmpty(t.Ubicacion) ? "Sin ubicacion" : t.Ubicacion;
            lblPrecioInscripcion.Text = $"{t.PrecioInscripcion:F2} €";
            lblCosteOrganizacion.Text = $"{t.CosteOrganizacion:F2} €";

            pnlDetalle.Visible = true;
            pnlError.Visible = false;
        }

        private void MostrarError()
        {
            pnlDetalle.Visible = false;
            pnlError.Visible = true;
        }


        protected void btnInscribirse_Click(object sender, EventArgs e)
        {
            int codigo = int.Parse(Request.QueryString["codigo"]);
            Response.Redirect("~/Public/Inscripcion.aspx?codigo=" + codigo);
        }

        private void CargarEquipos(int codigoTorneo)
        {
            CADInscripcion cad = new CADInscripcion();
            List<ENEquipo> equipos = cad.ReadEquiposByTorneo(codigoTorneo);

            if (equipos.Count > 0)
            {
                rptEquipos.DataSource = equipos;
                rptEquipos.DataBind();
            }
            else
            {
                rptEquipos.Visible = false;
                lblSinEquipos.Visible = true;
            }
        }
    }
}