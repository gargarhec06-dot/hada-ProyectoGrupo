using hada_ProyectoGrupo.Library.EN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace hada_ProyectoGrupo.Public
{
    public partial class DetallesEquipo : System.Web.UI.Page
    {
        private ENEquipo equipo;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    int id = int.Parse(Request.QueryString["id"]);
                    CargarEquipo(id);
                    if (Session["EsAdmin"] != null && (bool)Session["EsAdmin"] == false)
                    {
                        pnlJugador.Visible = true;
                    }
                }
                else
                {
                    Response.Redirect("~/Public/Equipos.aspx");
                }
            }
        }

        private void CargarEquipo(int id)
        {
            try
            {
                equipo = new ENEquipo();
                equipo.Id_equipo = id;
                if (equipo.Read())
                {
                    txtNombre.Text = equipo.Nombre;
                    txtFecha.Text = equipo.Fecha_creacion.ToString("dd/MM/yyyy");
                    txtDescripcion.Text = equipo.Descripcion;
                    txtCapitan.Text = equipo.Id_capitan.ToString();
                    txtLogo.Text = equipo.Logo_url;

                    // Validar que la URL del logo no esté vacía
                    if (!string.IsNullOrEmpty(equipo.Logo_url))
                    {
                        imgLogo.ImageUrl = equipo.Logo_url;
                    }
                    else
                    {
                        imgLogo.ImageUrl = "https://e7.pngegg.com/pngimages/779/61/png-clipart-logo-idea-cute-eagle-leaf-logo-thumbnail.png"; // Logo por defecto
                    }
                }
                else
                {
                    Console.WriteLine("No se pudo leer el equipo con ID: {0}", id);
                    Response.Write("<script>alert('Equipo no encontrado');</script>");
                    Response.Redirect("~/Public/Equipos.aspx");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cargar equipo: {0}", ex.Message);
                Response.Write("<script>alert('Error al cargar el equipo');</script>");
                Response.Redirect("~/Public/Equipos.aspx");
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Public/Equipos.aspx");
        }

        protected void btnCrear_Click(object sender, EventArgs e)
        {
            try
            {
                ENEquipo equipo = new ENEquipo();

                equipo.Nombre = txtNombre.Text;
                equipo.Fecha_creacion = DateTime.Now;
                equipo.Logo_url = txtLogo.Text;
                equipo.Descripcion = txtDescripcion.Text;
                equipo.Id_capitan = int.Parse(txtCapitan.Text);

                if (equipo.Read())
                {
                    lblMensaje.Text = "El equipo ya existe";
                    lblMensaje.ForeColor = System.Drawing.Color.Green;
                }
                else if (equipo.Create())
                {
                    lblMensaje.Text = "Equipo creado correctamente";
                    lblMensaje.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lblMensaje.Text = "ERROR";
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                ENEquipo equipoEliminar = new ENEquipo();

                if (Request.QueryString["id"] != null)
                {
                    equipoEliminar.Id_equipo = int.Parse(Request.QueryString["id"]);
                }
                else
                {
                    lblMensaje.Text = "ID de equipo no válido";
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                if (!equipoEliminar.Read())
                {
                    lblMensaje.Text = "El equipo no existe";
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                }
                else if (equipoEliminar.Delete())
                {
                    lblMensaje.Text = "Equipo eliminado correctamente";
                    lblMensaje.ForeColor = System.Drawing.Color.Green;
                    Response.Redirect("~/Public/Equipos.aspx");
                }
                else
                {
                    lblMensaje.Text = "ERROR al eliminar el equipo";
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error: " + ex.Message;
                lblMensaje.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                ENEquipo equipoModificar = new ENEquipo();

                if (Request.QueryString["id"] != null)
                {
                    equipoModificar.Id_equipo = int.Parse(Request.QueryString["id"]);
                }
                else
                {
                    lblMensaje.Text = "ID de equipo no válido";
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                // Primero leer el equipo existente
                if (!equipoModificar.Read())
                {
                    lblMensaje.Text = "El equipo no existe";
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    // Actualizar con los nuevos valores
                    equipoModificar.Nombre = txtNombre.Text;
                    equipoModificar.Fecha_creacion = DateTime.Parse(txtFecha.Text);
                    equipoModificar.Logo_url = txtLogo.Text;
                    equipoModificar.Descripcion = txtDescripcion.Text;
                    equipoModificar.Id_capitan = int.Parse(txtCapitan.Text);

                    if (equipoModificar.Update())
                    {
                        lblMensaje.Text = "Equipo modificado correctamente";
                        lblMensaje.ForeColor = System.Drawing.Color.Green;
                        // Recargar la imagen actualizada
                        if (!string.IsNullOrEmpty(equipoModificar.Logo_url))
                        {
                            imgLogo.ImageUrl = equipoModificar.Logo_url;
                        }
                    }
                    else
                    {
                        lblMensaje.Text = "ERROR al modificar el equipo";
                        lblMensaje.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error: " + ex.Message;
                lblMensaje.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnUnirse_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Public/Equipos.aspx");
        }
    }
}