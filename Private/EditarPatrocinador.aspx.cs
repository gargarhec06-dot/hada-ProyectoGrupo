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
        private int idPatrocinador = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["EsAdmin"] == null || (bool)Session["EsAdmin"] == false)
            {
                Response.Redirect("~/Public/Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null && int.TryParse(Request.QueryString["id"], out idPatrocinador))
                {
                    CargarPatrocinador(idPatrocinador);
                    CargarTorneosConPatrocinios(idPatrocinador);
                }
                else
                {
                    Response.Redirect("~/Public/Patrocinadores.aspx");
                }
            }
        }

        private void CargarPatrocinador(int id)
        {
            try
            {
                CADPatrocinador cad = new CADPatrocinador();
                ENPatrocinador patrocinador = new ENPatrocinador();
                patrocinador.IdPatrocinador = id;

                if (cad.Read(patrocinador))
                {
                    txtNombre.Text = patrocinador.Nombre;
                    txtTelefono.Text = patrocinador.Telefono;
                    txtEmail.Text = patrocinador.Email;
                    txtWeb.Text = patrocinador.PaginaWeb;
                    txtInicioContrato.Text = patrocinador.InicioContrato.ToString("yyyy-MM-dd");
                    txtFinContrato.Text = patrocinador.FinContrato.ToString("yyyy-MM-dd");
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al cargar: " + ex.Message;
            }
        }

        private void CargarTorneosConPatrocinios(int idPatrocinador)
        {
            try
            {
                // 1. Obtener TODOS los torneos de la BD
                CADTorneo cadTorneo = new CADTorneo();
                List<ENTorneo> todosLosTorneos = cadTorneo.ReadAll();

                // 2. Obtener los torneos que YA patrocina este patrocinador (guardar en ViewState o Session)
                CADPatrocinador cadPat = new CADPatrocinador();
                List<ENTorneoPatrocinador> torneosPatrocinados = cadPat.ReadTorneos(idPatrocinador);

                // Guardar en Session para usarlos después del DataBind
                Session["TorneosPatrocinados"] = torneosPatrocinados;

                // 3. PRIMERO: Hacer el DataBind (esto crea los controles)
                rptTorneos.DataSource = todosLosTorneos;
                rptTorneos.DataBind();

                // 4. SEGUNDO: Recorrer las filas y marcar los checkboxes
                foreach (RepeaterItem item in rptTorneos.Items)
                {
                    HiddenField hf = (HiddenField)item.FindControl("hfCodigoTorneo");
                    CheckBox chk = (CheckBox)item.FindControl("chkPatrocina");
                    TextBox txt = (TextBox)item.FindControl("txtCantidad");

                    if (hf != null && int.TryParse(hf.Value, out int codigoTorneo))
                    {
                        ENTorneoPatrocinador existente = torneosPatrocinados.Find(t => t.CodigoTorneo == codigoTorneo);

                        if (existente != null)
                        {
                            chk.Checked = true;
                            txt.Text = existente.Cantidad.ToString();
                            txt.Enabled = true;
                        }
                        else
                        {
                            chk.Checked = false;
                            txt.Text = "0";
                            txt.Enabled = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al cargar torneos: " + ex.Message;
            }
        }

        protected void chkPatrocina_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            RepeaterItem item = (RepeaterItem)chk.NamingContainer;
            TextBox txt = (TextBox)item.FindControl("txtCantidad");

            if (txt != null)
            {
                txt.Enabled = chk.Checked;
                if (!chk.Checked)
                {
                    txt.Text = "0";
                }
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            try
            {
                if (idPatrocinador == 0 && Request.QueryString["id"] != null)
                {
                    idPatrocinador = int.Parse(Request.QueryString["id"]);
                }

                // 1. Actualizar datos del patrocinador
                ENPatrocinador patrocinador = new ENPatrocinador();
                patrocinador.IdPatrocinador = idPatrocinador;
                patrocinador.Nombre = txtNombre.Text;
                patrocinador.Telefono = txtTelefono.Text;
                patrocinador.Email = txtEmail.Text;
                patrocinador.PaginaWeb = txtWeb.Text;
                patrocinador.InicioContrato = DateTime.Parse(txtInicioContrato.Text);

                if (string.IsNullOrEmpty(txtFinContrato.Text))
                {
                    patrocinador.FinContrato = patrocinador.InicioContrato.AddYears(1);
                }
                else
                {
                    patrocinador.FinContrato = DateTime.Parse(txtFinContrato.Text);
                }

                CADPatrocinador cad = new CADPatrocinador();
                bool exitoUpdate = cad.Update(patrocinador);

                if (!exitoUpdate)
                {
                    lblMensaje.Text = "Error al actualizar los datos";
                    return;
                }

                // 2. Actualizar torneos patrocinados
                // Primero, eliminar TODOS los patrocinios actuales
                cad.DeletePatrocinios(idPatrocinador);

                // Después, insertar SOLO los que están marcados
                foreach (RepeaterItem item in rptTorneos.Items)
                {
                    CheckBox chk = (CheckBox)item.FindControl("chkPatrocina");
                    TextBox txt = (TextBox)item.FindControl("txtCantidad");
                    HiddenField hf = (HiddenField)item.FindControl("hfCodigoTorneo");

                    if (chk != null && chk.Checked && hf != null && int.TryParse(hf.Value, out int codigoTorneo))
                    {
                        if (int.TryParse(txt.Text, out int cantidad) && cantidad > 0)
                        {
                            cad.CreatePatrocinio(idPatrocinador, codigoTorneo, cantidad);
                        }
                    }
                }

                Response.Redirect("~/Public/DetallePatrocinador.aspx?id=" + idPatrocinador);
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error: " + ex.Message;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            if (idPatrocinador == 0 && Request.QueryString["id"] != null)
            {
                idPatrocinador = int.Parse(Request.QueryString["id"]);
            }
            Response.Redirect("~/Public/DetallePatrocinador.aspx?id=" + idPatrocinador);
        }
    }
}