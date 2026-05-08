using hada_ProyectoGrupo.Library.EN;
using System;
using System.Web.UI;

namespace hada_ProyectoGrupo.Public
{
    public partial class DetallesNoticia : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    // MODO LECTURA
                    int id = int.Parse(Request.QueryString["id"]);
                    CargarDatos(id);
                }
                else
                {
                    // MODO CREACIÓN
                    if (Session["EsAdmin"] == null || (bool)Session["EsAdmin"] == false)
                    {
                        Response.Redirect("Login.aspx");
                    }
                    else
                    {
                        // Si es admin y va a crear una nueva, habilitamos los campos
                        ConfigurarInterfaz(true);
                    }
                }
            }
        }

        private void CargarDatos(int id)
        {
            ENNoticia en = new ENNoticia();
            en.IdNoticia = id;
            if (en.Read())
            {
                txtTitulo.Text = en.Titulo;
                txtContenido.Text = en.Contenido;
                txtFecha.Text = en.FechaPublicacion.ToString("yyyy-MM-dd");
                txtAutor.Text = en.EmailUsuario;

                VerificarPermisos(en.EmailUsuario);
            }
        }

        private void VerificarPermisos(string autorNoticia)
        {
            bool esAdmin = Session["EsAdmin"] != null && (bool)Session["EsAdmin"];
            string emailLogueado = Session["Email"]?.ToString();

            // ¿Tiene permiso para editar? (Es admin o es su propia noticia)
            bool puedeEditar = esAdmin || (emailLogueado != null && emailLogueado == autorNoticia);

            if (puedeEditar)
            {
                pnlAcciones.Visible = true;
                btnModificar.Visible = true;
                btnEliminar.Visible = true;
                btnCrear.Visible = esAdmin;
                ConfigurarInterfaz(true); // Habilitar escritura
            }
            else
            {
                pnlAcciones.Visible = false; // Oculta el panel de botones de edición
                ConfigurarInterfaz(false); // Bloquear escritura (Solo lectura)
            }
        }

        // Método auxiliar para bloquear o desbloquear los TextBox
        private void ConfigurarInterfaz(bool editable)
        {
            txtTitulo.ReadOnly = !editable;
            txtContenido.ReadOnly = !editable;
            // La fecha y el autor siempre deberían ser ReadOnly para evitar errores de integridad
            txtFecha.ReadOnly = true;
            txtAutor.ReadOnly = true;
        }

        protected void btnCrear_Click(object sender, EventArgs e)
        {
            if (Session["Email"] == null) return;

            ENNoticia en = new ENNoticia();
            en.Titulo = txtTitulo.Text;
            en.Contenido = txtContenido.Text;
            en.FechaPublicacion = DateTime.Now;
            en.EmailUsuario = Session["Email"].ToString();

            if (en.Create()) Response.Redirect("Noticias.aspx");
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            ENNoticia en = new ENNoticia();
            en.IdNoticia = int.Parse(Request.QueryString["id"]);
            en.Titulo = txtTitulo.Text;
            en.Contenido = txtContenido.Text;
            en.FechaPublicacion = DateTime.Parse(txtFecha.Text);
            en.EmailUsuario = txtAutor.Text;

            if (en.Update()) Response.Redirect("Noticias.aspx");
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            ENNoticia en = new ENNoticia();
            en.IdNoticia = int.Parse(Request.QueryString["id"]);
            if (en.Delete()) Response.Redirect("Noticias.aspx");
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Noticias.aspx");
        }
    }
}