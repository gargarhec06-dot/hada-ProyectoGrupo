using hada_ProyectoGrupo.Library.CAD;
using hada_ProyectoGrupo.Library.EN;
using System;
using System.Collections.Generic;

namespace hada_ProyectoGrupo.Public
{
    public partial class DetallePatrocinador : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    int id = int.Parse(Request.QueryString["id"]);
                    CargarPatrocinador(id);
                    CargarTorneos(id);
                }
                else
                {
                    Response.Redirect("~/Public/Patrocinadores.aspx");
                }
                if (Session["EsAdmin"] != null && (bool)Session["EsAdmin"] == true)
                {
                    pnlAdmin.Visible = true;
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
                lblNombre.Text = p.Nombre;
                lblEmail.Text = p.Email;
                hlWeb.Text = p.PaginaWeb;
                hlWeb.NavigateUrl = p.PaginaWeb;
                lblInicioContrato.Text = p.InicioContrato.ToShortDateString();
                lblFinContrato.Text = p.FinContrato.ToShortDateString();
                lbltelefono.Text = p.Telefono;
            }
        }

        private void CargarTorneos(int id)
        {
            CADPatrocinador cad = new CADPatrocinador();
            List<ENTorneoPatrocinador> torneos = cad.ReadTorneos(id);
            rptTorneos.DataSource = torneos;
            rptTorneos.DataBind();
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Public/Patrocinadores.aspx");
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            int id = int.Parse(Request.QueryString["id"]);
            Response.Redirect("~/Private/EditarPatrocinador.aspx?id=" + id);
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            ENPatrocinador p = new ENPatrocinador();
            p.IdPatrocinador = int.Parse(Request.QueryString["id"]);
            CADPatrocinador cad = new CADPatrocinador();
            cad.Delete(p);
            Response.Redirect("~/Public/Patrocinadores.aspx");
        }
    }
}