using hada_ProyectoGrupo.Library.CAD;
using hada_ProyectoGrupo.Library.EN;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace hada_ProyectoGrupo.Private
{
    public partial class GestionPatrocinador : System.Web.UI.Page
    {
        private int idPatrocinador = 0;
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
                CargarTorneos();
                

                if (Request.QueryString["id"] != null)
                {
                    idPatrocinador = int.Parse(Request.QueryString["id"]);
                    esNuevo = false;
                    tituloPagina.InnerText = "Editar Patrocinador";
                    CargarPatrocinador(idPatrocinador);
                }
                else
                {
                    tituloPagina.InnerText = "Nuevo Patrocinador";
                }
            }
        }

        private void CargarTorneos()
        {
            CADTorneo cadTorneo = new CADTorneo();
            List<ENTorneo> torneos = cadTorneo.ReadAll();
            rptTorneos.DataSource = torneos;
            rptTorneos.DataBind();

            // Si estamos editando, marcar los torneos ya patrocinados
            if (!esNuevo)
            {
                CADPatrocinador cadPat = new CADPatrocinador();
                List<ENTorneoPatrocinador> patrocinios = cadPat.ReadPatrocinios(idPatrocinador);

                foreach (RepeaterItem item in rptTorneos.Items)
                {
                    HiddenField hf = (HiddenField)item.FindControl("hfCodigoTorneo");
                    CheckBox chk = (CheckBox)item.FindControl("chkTorneo");
                    TextBox txt = (TextBox)item.FindControl("txtCantidad");

                    if (hf != null)
                    {
                        int codigo = int.Parse(hf.Value);
                        ENTorneoPatrocinador pat = patrocinios.Find(p => p.CodigoTorneo == codigo);
                        if (pat != null)
                        {
                            chk.Checked = true;
                            txt.Text = pat.Cantidad.ToString();
                        }
                    }
                }
            }
        }

        private void CargarPatrocinador(int id)
        {
            ENPatrocinador p = new ENPatrocinador();
            p.IdPatrocinador = id;
            CADPatrocinador cad = new CADPatrocinador();
            if (cad.Read(p))
            {
                txtNombre.Text = p.Nombre;
                txtTelefono.Text = p.Telefono;
                txtEmail.Text = p.Email;
                txtWeb.Text = p.PaginaWeb;
                txtInicioContrato.Text = p.InicioContrato.ToString("yyyy-MM-dd");
                txtFinContrato.Text = p.FinContrato.ToString("yyyy-MM-dd");
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            try
            {
                ENPatrocinador p = new ENPatrocinador();
                p.Nombre = txtNombre.Text;
                p.Telefono = txtTelefono.Text;
                p.Email = txtEmail.Text;
                p.PaginaWeb = txtWeb.Text;
                p.InicioContrato = DateTime.Parse(txtInicioContrato.Text);
                p.FinContrato = DateTime.Parse(txtFinContrato.Text);

                CADPatrocinador cad = new CADPatrocinador();

                if (esNuevo)
                {
                    int nuevoId = cad.Create(p);
                    if (nuevoId > 0)
                    {
                        p.IdPatrocinador = nuevoId;
                        GuardarPatrocinios(p);
                        Response.Redirect("~/Public/Patrocinadores.aspx");
                    }
                    else
                    {
                        lblMensaje.Text = "Error al crear el patrocinador.";
                    }
                }
                else
                {
                    p.IdPatrocinador = idPatrocinador;
                    bool ok = cad.Update(p);
                    if (ok)
                        Response.Redirect("~/Public/Patrocinadores.aspx");
                    else
                        lblMensaje.Text = "Error al actualizar el patrocinador.";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error: " + ex.Message;
            }
        }

        private void GuardarPatrocinios(ENPatrocinador patrocinador)
        {
            CADPatrocinador cad = new CADPatrocinador();

            foreach (RepeaterItem item in rptTorneos.Items)
            {
                CheckBox chk = (CheckBox)item.FindControl("chkTorneo");
                TextBox txt = (TextBox)item.FindControl("txtCantidad");

                if (chk.Checked && txt != null)
                {
                    // Obtener el codigo del torneo desde el DataKey
                    HiddenField hf = (HiddenField)item.FindControl("hfCodigoTorneo");
                    if (hf != null)
                    {
                        int codigoTorneo = int.Parse(hf.Value);
                        int cantidad = int.Parse(txt.Text);
                        cad.CreatePatrocinio(patrocinador.IdPatrocinador, codigoTorneo, cantidad);
                    }
                }
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Public/Patrocinadores.aspx");
        }
    }
}