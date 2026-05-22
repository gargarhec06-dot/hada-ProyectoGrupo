using hada_ProyectoGrupo.Library.CAD;
using hada_ProyectoGrupo.Library.EN;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace hada_ProyectoGrupo.Private
{
    public partial class EditarPatrocinador : System.Web.UI.Page
    {
        // Guardar idPatrocinador en ViewState para que sobreviva al postback
        private int IdPatrocinador
        {
            get { return ViewState["IdPatrocinador"] != null ? (int)ViewState["IdPatrocinador"] : 0; }
            set { ViewState["IdPatrocinador"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["EsAdmin"] == null || (bool)Session["EsAdmin"] == false)
            {
                Response.Redirect("~/Public/Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null && int.TryParse(Request.QueryString["id"], out int id))
                {
                    IdPatrocinador = id;
                    CargarPatrocinador(id);
                    CargarTorneos(id);
                }
                else
                {
                    Response.Redirect("~/Public/Patrocinadores.aspx");
                }
            }
            
        }

        private void CargarPatrocinador(int id)
        {
            CADPatrocinador cad = new CADPatrocinador();
            ENPatrocinador p = new ENPatrocinador();
            p.IdPatrocinador = id;

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

        private void CargarTorneos(int id)
        {
            try
            {
                CADTorneo cadTorneo = new CADTorneo();
                List<ENTorneo> torneos = cadTorneo.ReadAll();



                if (torneos.Count == 0)
                {
                    lblMensaje.Text += " - La BD no tiene torneos o hay error de conexión";
                    return;
                }

                rptTorneos.DataSource = torneos;
                rptTorneos.DataBind();

                CADPatrocinador cadPat = new CADPatrocinador();
                List<ENTorneoPatrocinador> patrocinios = cadPat.ReadPatrocinios(id);

                

                foreach (RepeaterItem item in rptTorneos.Items)
                {
                    HiddenField hf = (HiddenField)item.FindControl("hfCodigoTorneo");
                    CheckBox chk = (CheckBox)item.FindControl("chkPatrocina");
                    TextBox txt = (TextBox)item.FindControl("txtCantidad");

                    if (hf != null && int.TryParse(hf.Value, out int codigoTorneo))
                    {
                        ENTorneoPatrocinador pat = patrocinios.Find(p => p.CodigoTorneo == codigoTorneo);
                        if (pat != null)
                        {
                            chk.Checked = true;
                            txt.Text = pat.Cantidad.ToString();
                        }
                        else
                        {
                            chk.Checked = false;
                            txt.Text = "0";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error en CargarTorneos: " + ex.Message;
            }
        }

        


        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            try
            {
                ENPatrocinador p = new ENPatrocinador();
                p.IdPatrocinador = IdPatrocinador;
                p.Nombre = txtNombre.Text;
                p.Telefono = txtTelefono.Text;
                p.Email = txtEmail.Text;
                p.PaginaWeb = txtWeb.Text;
                p.InicioContrato = DateTime.Parse(txtInicioContrato.Text);
                p.FinContrato = DateTime.Parse(txtFinContrato.Text);

                CADPatrocinador cad = new CADPatrocinador();
                bool ok = cad.Update(p);

                if (!ok)
                {
                    lblMensaje.Text = "Error al actualizar el patrocinador.";
                    return;
                }

                cad.DeletePatrocinios(IdPatrocinador);

                foreach (RepeaterItem item in rptTorneos.Items)
                {
                    CheckBox chk = (CheckBox)item.FindControl("chkPatrocina");
                    TextBox txt = (TextBox)item.FindControl("txtCantidad");
                    HiddenField hf = (HiddenField)item.FindControl("hfCodigoTorneo");

                    if (chk != null && chk.Checked && hf != null)
                    {
                        int codigoTorneo = int.Parse(hf.Value);
                        if (int.TryParse(txt.Text, out int cantidad) && cantidad > 0)
                        {
                            cad.CreatePatrocinio(IdPatrocinador, codigoTorneo, cantidad);
                        }
                    }
                }

                Response.Redirect("~/Public/DetallePatrocinador.aspx?id=" + IdPatrocinador);
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error: " + ex.Message;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Public/Patrocinadores.aspx");
        }
    }
}