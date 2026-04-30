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
                if (int.TryParse(Request.QueryString["codigo"], out int codigo))
                {
                    // Usamos el constructor y Read() igual que el patrón EN→CAD
                    ENTorneo en = new ENTorneo();
                    en.Codigo = codigo;

                    if (en.Read()) // CADTorneo.Read() rellena el objeto con los datos
                    {
                        MostrarTorneo(en);
                    }
                    else
                    {
                        MostrarError();
                    }
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
            lblProfesional.Text = t.Profesional ? "🏆 Profesional" : "🎮 Amateur";
            lblDescripcion.Text = string.IsNullOrEmpty(t.Descripcion)
                                            ? "Sin descripción" : t.Descripcion;
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
    }
}