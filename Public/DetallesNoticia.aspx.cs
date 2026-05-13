using hada_ProyectoGrupo.Library.EN;
using System;
using System.IO; 
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
                    int id = int.Parse(Request.QueryString["id"]);
                    CargarDatos(id);
                }
                else
                {
                    if (Session["EsAdmin"] == null || (bool)Session["EsAdmin"] == false)
                    {
                        Response.Redirect("Login.aspx");
                    }
                    else
                    {
                        ConfigurarInterfaz(true);
                        txtFecha.Text = DateTime.Now.ToString("yyyy-MM-dd");
                        txtAutor.Text = Session["Email"].ToString();

                        // Imagen por defecto en modo creación
                        imgNoticia.ImageUrl = ResolveUrl("~/Images/Noticias/default-news.png");

                        pnlAcciones.Visible = true;
                        btnCrear.Visible = true;
                        btnModificar.Visible = false;
                        btnEliminar.Visible = false;
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
                txtImagenUrl.Text = en.ImagenUrl; // Cargamos la URL de la BD

                
                string noticiaUrl = !string.IsNullOrWhiteSpace(en.ImagenUrl)
                    ? en.ImagenUrl
                    : "~/Images/Noticias/default-news.png";

                imgNoticia.ImageUrl = ResolveUrl(noticiaUrl);
                imgNoticia.Visible = true;

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
                btnCrear.Visible = false;
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
            txtImagenUrl.ReadOnly = !editable;
            pnlSubidaImagen.Visible = editable; 
            txtFecha.ReadOnly = true;
            txtAutor.ReadOnly = true;
        }

        // GESTIÓN DE IMAGEN
        protected void btnSubirImagen_Click(object sender, EventArgs e)
        {
            if (fuImagen.HasFile)
            {
                try
                {
                    string extension = Path.GetExtension(fuImagen.FileName).ToLower();
                    if (extension != ".jpg" && extension != ".jpeg" && extension != ".png")
                    {
                        lblSubidaInfo.Text = "Solo JPG o PNG";
                        lblSubidaInfo.ForeColor = System.Drawing.Color.Red;
                        return;
                    }

                    string nombreArchivo = "noticia_" + DateTime.Now.Ticks + extension;
                    string ruta = Server.MapPath("~/Images/Noticias/");

                    if (!Directory.Exists(ruta))
                        Directory.CreateDirectory(ruta);

                    fuImagen.SaveAs(ruta + nombreArchivo);

                    string rutaRelativa = "~/Images/Noticias/" + nombreArchivo;

                    // Actualizamos el TextBox para que los métodos Create/Update lo guarden
                    txtImagenUrl.Text = rutaRelativa;

                    imgNoticia.ImageUrl = ResolveUrl(rutaRelativa);
                    imgNoticia.Visible = true;

                    lblSubidaInfo.Text = "Imagen lista. Pulsa Guardar.";
                    lblSubidaInfo.ForeColor = System.Drawing.Color.Green;
                }
                catch (Exception ex)
                {
                    lblSubidaInfo.Text = "Error: " + ex.Message;
                }
            }
        }

        protected void btnCrear_Click(object sender, EventArgs e)
        {
            if (Session["Email"] == null) return;

            ENNoticia en = new ENNoticia();
            en.Titulo = txtTitulo.Text;
            en.Contenido = txtContenido.Text;
            en.ImagenUrl = txtImagenUrl.Text; // Guardamos la ruta generada
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
            en.ImagenUrl = txtImagenUrl.Text; // Guardamos la ruta (nueva o antigua)
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