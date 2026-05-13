using hada_ProyectoGrupo.Library.EN;
using hada_ProyectoGrupo.Library.CAD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace hada_ProyectoGrupo.Private
{
    public partial class GestionTorneo : System.Web.UI.Page
    {
        private int idTorneo = 0;
        private bool esNuevo = true;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["EsAdmin"] == null || (bool)Session["EsAdmin"] == false)
            {
                Response.Redirect("~/Public/Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                CADVideojuego vj = new CADVideojuego();
                ddlVideojuego.DataSource = vj.ReadAll();
                ddlVideojuego.DataTextField = "Nombre";
                ddlVideojuego.DataValueField = "Codigo";
                ddlVideojuego.DataBind();
                ddlVideojuego.Items.Insert(0, new ListItem("Selecciona un videojuego", "0"));

                if (Request.QueryString["id"] != null)
                {
                    idTorneo = int.Parse(Request.QueryString["id"]);
                    esNuevo = false;
                    tituloPagina.InnerText = "Editar Torneo";
                    CargarTorneo(idTorneo);
                }
                else
                {
                    tituloPagina.InnerText = "Nuevo Torneo";
                }
            }
        }

        private void CargarTorneo(int id)
        {
            ENTorneo en = new ENTorneo();
            en.Codigo = id;

            if (en.Read())
            {
                ddlVideojuego.SelectedValue = en.IdVideojuego.ToString();
                txtNombre.Text = en.Nombre;
                txtDescripcion.Text = en.Descripcion;
                txtFecha.Text = en.Fecha.ToString("yyyy-MM-dd");
                txtInscripcion.Text = en.PrecioInscripcion.ToString();
                txtOrganizacion.Text = en.CosteOrganizacion.ToString();
                chkProfesional.Checked = en.Profesional;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            ENTorneo en = new ENTorneo();

            if (Request.QueryString["id"] != null)
            {
                en.Codigo = int.Parse(Request.QueryString["id"]);
                esNuevo = false;
            }

            en.IdVideojuego = int.Parse(ddlVideojuego.SelectedValue);
            en.Nombre = txtNombre.Text;
            en.Descripcion = txtDescripcion.Text;
            en.Fecha = DateTime.Parse(txtFecha.Text);
            en.PrecioInscripcion = float.Parse(txtInscripcion.Text);
            en.CosteOrganizacion = float.Parse(txtOrganizacion.Text);
            en.Profesional = chkProfesional.Checked;
            en.Ubicacion = txtUbicacion.Text;
            en.Premio = float.Parse(txtPremio.Text);
            en.Capacidad = int.Parse(txtCapacidad.Text);

            bool operacionOk = esNuevo ? en.Create() : en.Update();

            if (operacionOk)
            {
                Response.Redirect("~/Public/Torneos.aspx");
            }
            else
            {
                lblMensaje.Text = "Error al procesar el torneo.";
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Public/Torneos.aspx");
        }
    }
}