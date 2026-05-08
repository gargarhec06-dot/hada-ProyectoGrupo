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
                    // MODO LECTURA/EDICIÓN (Noticia existente)
                    int id = int.Parse(Request.QueryString["id"]);
                    CargarDatos(id);
                }
                else
                {
                    // MODO CREACIÓN (Noticia nueva)
                    if (Session["EsAdmin"] == null || (bool)Session["EsAdmin"] == false)
                    {
                        Response.Redirect("Login.aspx");
                    }
                    else
                    {
                        // 1. Habilitamos escritura en Título y Contenido
                        ConfigurarInterfaz(true);

                        // 2. RELLENO AUTOMÁTICO (Para que el admin vea qué se va a guardar)
                        txtFecha.Text = DateTime.Now.ToString("yyyy-MM-dd");
                        txtAutor.Text = Session["Email"].ToString();

                        // 3. HACER VISIBLE EL BOTÓN DE GUARDAR/CREAR
                        pnlAcciones.Visible = true;
                        btnCrear.Visible = true;     // Este es el botón para noticias nuevas
                        btnModificar.Visible = false; // Ocultamos modificar (no existe aún)
                        btnEliminar.Visible = false;  // Ocultamos eliminar
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

            bool puedeEditar = esAdmin || (emailLogueado != null && emailLogueado == autorNoticia);

            if (puedeEditar)
            {
                pnlAcciones.Visible = true;
                btnModificar.Visible = true;
                btnEliminar.Visible = true;
                btnCrear.Visible = false; // False porque estamos editando una vieja, no creando nueva
                ConfigurarInterfaz(true);
            }
            else
            {
                pnlAcciones.Visible = false;
                ConfigurarInterfaz(false);
            }
        }

        private void ConfigurarInterfaz(bool editable)
        {
            txtTitulo.ReadOnly = !editable;
            txtContenido.ReadOnly = !editable;
            txtFecha.ReadOnly = true;
            txtAutor.ReadOnly = true;
        }

        protected void btnCrear_Click(object sender, EventArgs e)
        {
            if (Session["Email"] == null) return;

            ENNoticia en = new ENNoticia();
            en.Titulo = txtTitulo.Text;
            en.Contenido = txtContenido.Text;
            en.FechaPublicacion = DateTime.Now; // Fecha del servidor por seguridad
            en.EmailUsuario = Session["Email"].ToString(); // Usuario real de la sesión

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