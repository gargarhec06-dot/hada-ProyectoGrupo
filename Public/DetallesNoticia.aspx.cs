using hada_ProyectoGrupo.Library.EN;
using System;
using System.Collections.Generic;
using System.Web.UI;

namespace hada_ProyectoGrupo.Public
{
    public partial class DetallesNoticia : System.Web.UI.Page
    {
        private ENNoticia noticia;
        private string emailLogueado;

        // Propiedad para persistir el ID en el ViewState
        private int idNoticia
        {
            get { return ViewState["IdNoticia"] != null ? (int)ViewState["IdNoticia"] : 0; }
            set { ViewState["IdNoticia"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Verificación de sesión obligatoria
            if (Session["Email"] == null)
            {
                Response.Redirect("~/Public/Login.aspx");
                return;
            }

            emailLogueado = Session["Email"].ToString();

            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    idNoticia = int.Parse(Request.QueryString["id"]);
                    CargarNoticia(idNoticia);
                    VerificarPermisos();
                }
                else
                {
                    // Modo Creación: Limpiamos campos y configuramos botones
                    pnlAcciones.Visible = true;
                    txtTitulo.Text = "";
                    txtContenido.Text = "";
                    txtFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtAutor.Text = emailLogueado; // El autor por defecto es el usuario actual

                    btnModificar.Visible = false;
                    btnEliminar.Visible = false;
                    btnCrear.Visible = true;
                }
            }
        }

        private void CargarNoticia(int id)
        {
            try
            {
                noticia = new ENNoticia();
                noticia.IdNoticia = id;
                if (noticia.Read())
                {
                    txtTitulo.Text = noticia.Titulo;
                    txtContenido.Text = noticia.Contenido;
                    txtFecha.Text = noticia.FechaPublicacion.ToString("dd/MM/yyyy");
                    txtAutor.Text = noticia.EmailUsuario;
                    hfIdUsuario.Value = noticia.EmailUsuario;

                    // Imagen por defecto 
                    imgNoticia.ImageUrl = "https://images.unsplash.com/photo-1542751371-adc38448a05e?auto=format&fit=crop&w=800&q=80";
                }
                else
                {
                    lblMensaje.Text = "Noticia no encontrada.";
                    Response.Redirect("Noticias.aspx");
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al cargar: " + ex.Message;
            }
        }

        private void VerificarPermisos()
        {
            pnlAcciones.Visible = true;

            // Solo el autor original puede modificar o eliminar
            bool esAutor = (txtAutor.Text.Trim().ToLower() == emailLogueado.ToLower());

            btnModificar.Visible = esAutor;
            btnEliminar.Visible = esAutor;
            btnCrear.Visible = false; // Estamos en modo lectura/edición
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Noticias.aspx");
        }

        protected void btnCrear_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitulo.Text) || string.IsNullOrWhiteSpace(txtContenido.Text))
            {
                lblMensaje.Text = "El título y el contenido son obligatorios.";
                return;
            }

            try
            {
                ENNoticia nueva = new ENNoticia();
                nueva.Titulo = txtTitulo.Text;
                nueva.Contenido = txtContenido.Text;
                nueva.FechaPublicacion = DateTime.Now;
                nueva.EmailUsuario = emailLogueado;

                if (nueva.Create())
                {
                    Response.Redirect("Noticias.aspx");
                }
                else
                {
                    lblMensaje.Text = "Error al crear la noticia.";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error: " + ex.Message;
            }
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                ENNoticia mod = new ENNoticia();
                mod.IdNoticia = idNoticia;
                mod.Titulo = txtTitulo.Text;
                mod.Contenido = txtContenido.Text;
                mod.FechaPublicacion = DateTime.Parse(txtFecha.Text);
                mod.EmailUsuario = txtAutor.Text;

                if (mod.Update())
                {
                    lblMensaje.Text = "Noticia actualizada correctamente.";
                    lblMensaje.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lblMensaje.Text = "No se pudo actualizar.";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error: " + ex.Message;
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                ENNoticia del = new ENNoticia();
                del.IdNoticia = idNoticia;
                if (del.Delete())
                {
                    Response.Redirect("Noticias.aspx");
                }
                else
                {
                    lblMensaje.Text = "Error al eliminar.";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error: " + ex.Message;
            }
        }
    }
}