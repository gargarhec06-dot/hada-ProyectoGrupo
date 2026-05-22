using hada_ProyectoGrupo.Library.CAD;
using hada_ProyectoGrupo.Library.EN;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace hada_ProyectoGrupo.Public
{
    public partial class DetalleTorneo : System.Web.UI.Page
    {
        private int codigoTorneo
        {
            get { return ViewState["codigoTorneo"] != null ? (int)ViewState["codigoTorneo"] : 0; }
            set { ViewState["codigoTorneo"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["codigo"] == null)
                {
                    MostrarError();
                    return;
                }

                codigoTorneo = int.Parse(Request.QueryString["codigo"]);
                ENTorneo en = new ENTorneo();
                en.Codigo = codigoTorneo;
                CADTorneo cad = new CADTorneo();
                string nombreVJ;

                if (cad.ReadWithVideojuego(en, out nombreVJ))
                {
                    MostrarTorneo(en);
                    lblVideojuego.Text = nombreVJ;
                    CargarEquipos(codigoTorneo);
                    CargarPartidas(codigoTorneo);

                    // Mostrar botones solo si es admin
                    if (Session["EsAdmin"] != null && (bool)Session["EsAdmin"] == true)
                    {
                        btnModificar.Visible = true;
                        btnEliminar.Visible = true;
                        btnCreatePartida.Visible = true;
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
            lblProfesional.Text = t.Profesional ? "Profesional" : "Amateur";
            lblDescripcion.Text = string.IsNullOrEmpty(t.Descripcion) ? "Sin descripción" : t.Descripcion;
            lblCapacidad.Text = t.Capacidad.ToString();
            lblUbicacion.Text = string.IsNullOrEmpty(t.Ubicacion) ? "Sin ubicacion" : t.Ubicacion;
            lblPrecioInscripcion.Text = $"{t.PrecioInscripcion:F2} €";
            lblCosteOrganizacion.Text = $"{t.CosteOrganizacion:F2} €";
            lblPremio.Text = $"{t.Premio:F2} €";
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
            Response.Redirect("~/Public/Inscripcion.aspx?codigo=" + codigoTorneo);
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Private/GestionTorneo.aspx?id=" + codigoTorneo);
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            ENTorneo en = new ENTorneo();
            en.Codigo = codigoTorneo;

            if (en.Delete())
                Response.Redirect("~/Public/Torneos.aspx");
            else
                lblMensaje.Text = "Error al eliminar el torneo.";
        }

        private void CargarEquipos(int codigo)
        {
            CADInscripcion cad = new CADInscripcion();
            List<ENEquipo> equipos = cad.ReadEquiposByTorneo(codigo);
            if (equipos != null && equipos.Count > 0)
            {
                rptEquipos.DataSource = equipos;
                rptEquipos.DataBind();
                rptEquipos.Visible = true;
                lblSinEquipos.Visible = false;
            }
            else
            {
                rptEquipos.Visible = false;
                lblSinEquipos.Visible = true;
            }
        }

        private void CargarPartidas(int codigoTorneo)
        {
            CADPartida cad = new CADPartida();
            List<ENPartida> partidas = cad.ReadByTorneo(codigoTorneo);

            if (partidas != null && partidas.Count > 0)
            {
                rptPartidas.DataSource = partidas;
                rptPartidas.DataBind();
                rptPartidas.Visible = true;
                lblSinPartidas.Visible = false;
            }
            else
            {
                rptPartidas.Visible = false;
                lblSinPartidas.Visible = true;
            }
        }

        protected void btnCreatePartida_Click(object sender, EventArgs e)
        {
            Response.Redirect("DetallesPartida.aspx?torneo=" + codigoTorneo);
        }
    }
}