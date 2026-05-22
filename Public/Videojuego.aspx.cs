using hada_ProyectoGrupo.Library.EN;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace hada_ProyectoGrupo.Public
{
    public partial class Videojuego : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Cargar tipos posibles
            if (!IsPostBack)
            {
                foreach (ENVideojuego.ENVideojuegoTipo tipo_no_undefined in ENVideojuego.GetAllVideojuegoTipo().Keys)
                {
                    TipoAdminBox.Items.Add(new ListItem(
                            ENVideojuego.GetVideojuegoTipoToNombreLegible(tipo_no_undefined),
                            tipo_no_undefined.ToString()
                        ));
                }
            }

            // Se debe de obtener el videojuego en un futuro con esta variable
            string code = Request.QueryString["codigo"];

            if (string.IsNullOrEmpty(code)) {
                if (Session["EsAdmin"] != null && (bool)Session["EsAdmin"])
                {
                    activate_admin();

                    AdminDelete.Visible = false;
                    AdminUpdate.Visible = false;
                    AdminAdd.Visible = true;

                    return;
                }
                else
                {
                      DebugLabel.Text = "No se encontró ningún argumento para el código del juego";
                      return;
                }
            }

            ENVideojuego videojuego = new ENVideojuego(int.Parse(code), "", "", "", 0, "~/Images/Equipos/default-team.png", "~/Images/Equipos/default-team.png");
            bool result = videojuego.Read();

            if (result) {
                NombreLabel.Text = videojuego.Nombre;
                CodigoLabel.Text = videojuego.Codigo.ToString();
                DescripcionLabel.Text = videojuego.Descripcion;
                TipoLabel.Text = ENVideojuego.GetVideojuegoTipoToNombreLegible(ENVideojuego.GetVideojuegoTipoFromCode(videojuego.Tipo));
                EdadMinimaLabel.Text = videojuego.EdadMinima.ToString();
                imgIcon.ImageUrl = ResolveUrl(videojuego.IconUrl);
                imgCaratula.ImageUrl = ResolveUrl(videojuego.CaratulaUrl);

                // Para observar el panel de admin
                if (Session["EsAdmin"] != null && (bool)Session["EsAdmin"])
                {
                    //DebugLabel.Text = "Admin detectado";

                    activate_admin();

                    if (!IsPostBack)
                    {
                        NombreAdminBox.Text = NombreLabel.Text;
                        CodigoAdminBox.Text = CodigoLabel.Text;
                        DescripcionAdminBox.Text = DescripcionLabel.Text;
                        TipoAdminBox.SelectedValue = videojuego.Tipo;
                        EdadMinimaAdminBox.Text = EdadMinimaLabel.Text;
                    }
                }
            }
            else
            {
                DebugLabel.Text = "No se encontró";
            }
        }
        private void activate_admin()
        {
            NombreAdminBox.Visible = true;
            CodigoAdminBox.Visible = true;
            DescripcionAdminBox.Visible = true;
            TipoAdminBox.Visible = true;
            EdadMinimaAdminBox.Visible = true;

            AdminDelete.Visible = true;
            AdminUpdate.Visible = true;
            //AdminAdd.Visible = true;

            lblIconUploadStatic.Visible = true;
            IconUpload.Visible = true;
            btnIconUpload.Visible = true;
            lblIconUpload.Visible = true;
            lblRouteIcon.Visible = true;

            lblCaratulaUploadStatic.Visible = true;
            CaratulaUpload.Visible = true;
            btnCaratulaUpload.Visible = true;
            lblCaratulaUpload.Visible = true;
            lblRouteCaratula.Visible = true;
        }

        protected void AdminUpdate_Click(object sender, EventArgs e)
        {
            if (Session["EsAdmin"] != null && (bool)Session["EsAdmin"])
            {
                // Podría actualizarse el código para SOLO usar la URI pero esto puede introducir problemas
                int code = int.Parse(CodigoAdminBox.Text);

                ENVideojuego old = new ENVideojuego();
                old.Codigo = code;
                old.Read();

                string ruta_icono = lblRouteIcon.Text;
                string ruta_caratula = lblRouteCaratula.Text;
                if (ruta_icono == "")
                {
                    ruta_icono = old.IconUrl;
                }
                if (ruta_caratula == "")
                {
                    ruta_caratula = old.CaratulaUrl;
                }

                ENVideojuego entry = new ENVideojuego(
                    int.Parse(CodigoAdminBox.Text),
                    NombreAdminBox.Text,
                    DescripcionAdminBox.Text,
                    TipoAdminBox.SelectedValue,
                    int.Parse(EdadMinimaAdminBox.Text),
                    ruta_icono,
                    ruta_caratula
                );

                if (entry.Update())
                {
                    Response.Redirect(Request.Url.ToString());
                }
                else
                {
                    DebugLabel.Text = "No se pudo actualizar la entrada";
                }
            }
        }

        protected void AdminDelete_Click(object sender, EventArgs e)
        {
            if (Session["EsAdmin"] != null && (bool)Session["EsAdmin"])
            {
                ENVideojuego entry = new ENVideojuego();
                entry.Codigo = int.Parse(CodigoAdminBox.Text);

                if (entry.Delete())
                {
                    Response.Redirect("Videojuegos.aspx");
                }
                else
                {
                    DebugLabel.Text = "Algo fue mal y no se pudo borrar";
                }
            }
        }

        protected void AdminAdd_Click(object sender, EventArgs e)
        {
            if (Session["EsAdmin"] != null && (bool)Session["EsAdmin"])
            {
                ENVideojuego entry = new ENVideojuego(
                    0,
                    NombreAdminBox.Text,
                    DescripcionAdminBox.Text,
                    TipoAdminBox.SelectedValue,
                    int.Parse(EdadMinimaAdminBox.Text),
                    lblRouteIcon.Text,
                    lblCaratulaUpload.Text
                );

                if (entry.Create())
                {
                    // Obetener último videojuego añadido
                    ENVideojuego new_entry = entry.ReadAll().Last();
                    int new_code = new_entry.Codigo;

                    Response.Redirect("Videojuego.aspx?codigo=" + new_code.ToString());
                }
                else
                {
                    DebugLabel.Text = "Algo fue mal a la hora de crear la entrada";
                }
            }
        }

        protected void btnIconUpload_Click(object sender, EventArgs e)
        {
            if (!(Session["EsAdmin"] != null && (bool)Session["EsAdmin"])) { return; }
            if (IconUpload.HasFile)
            {
                try
                {
                    string extension = System.IO.Path.GetExtension(IconUpload.FileName).ToLower();
                    if (extension != ".jpg" && extension != ".jpeg" && extension != ".png")
                    {
                        lblIconUpload.Text = "Solo JPG o PNG";
                        lblIconUpload.ForeColor = System.Drawing.Color.Red;
                        return;
                    }

                    if (IconUpload.PostedFile.ContentLength > 2 * 1024 * 1024)
                    {
                        lblIconUpload.Text = "Máximo 2MB";
                        lblIconUpload.ForeColor = System.Drawing.Color.Red;
                        return;
                    }

                    string nombreArchivo = "icono_juego_" + DateTime.Now.Ticks + extension;
                    // UNIFICAMOS la carpeta: siempre "Videojuegos" con S
                    string ruta = Server.MapPath("~/Images/Videojuegos/");

                    if (!Directory.Exists(ruta))
                        Directory.CreateDirectory(ruta);

                    IconUpload.SaveAs(ruta + nombreArchivo);

                    // Guardamos la ruta relativa
                    string rutaRelativa = "~/Images/Videojuegos/" + nombreArchivo;

                    // CRÍTICO: actualizamos el TextBox para que btnModificar guarde esto en la BD
                    lblRouteIcon.Text = rutaRelativa;

                    imgIcon.ImageUrl = ResolveUrl(rutaRelativa);

                    lblIconUpload.Text = "Imagen subida. Pulsa 'Guardar Cambios' para confirmar.";
                    lblIconUpload.ForeColor = System.Drawing.Color.Green;
                }
                catch (Exception ex)
                {
                    lblIconUpload.Text = "Error " + ex.Message;
                    lblIconUpload.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                lblIconUpload.Text = "Selecciona una imagen";
                lblIconUpload.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnCaratulaUpload_Click(object sender, EventArgs e)
        {
            if (!(Session["EsAdmin"] != null && (bool)Session["EsAdmin"])) { return; }
            if (CaratulaUpload.HasFile)
            {
                try
                {
                    string extension = System.IO.Path.GetExtension(CaratulaUpload.FileName).ToLower();
                    if (extension != ".jpg" && extension != ".jpeg" && extension != ".png")
                    {
                        lblCaratulaUpload.Text = "Solo JPG o PNG";
                        lblCaratulaUpload.ForeColor = System.Drawing.Color.Red;
                        return;
                    }

                    if (IconUpload.PostedFile.ContentLength > 2 * 1024 * 1024)
                    {
                        lblCaratulaUpload.Text = "Máximo 2MB";
                        lblCaratulaUpload.ForeColor = System.Drawing.Color.Red;
                        return;
                    }

                    string nombreArchivo = "caratula_juego_" + DateTime.Now.Ticks + extension;
                    // UNIFICAMOS la carpeta: siempre "Videojuegos" con S
                    string ruta = Server.MapPath("~/Images/Videojuegos/");

                    if (!Directory.Exists(ruta))
                        Directory.CreateDirectory(ruta);

                    CaratulaUpload.SaveAs(ruta + nombreArchivo);

                    // Guardamos la ruta relativa
                    string rutaRelativa = "~/Images/Videojuegos/" + nombreArchivo;

                    // CRÍTICO: actualizamos el TextBox para que btnModificar guarde esto en la BD
                    lblRouteCaratula.Text = rutaRelativa;

                    imgCaratula.ImageUrl = ResolveUrl(rutaRelativa);

                    lblCaratulaUpload.Text = "Imagen subida. Pulsa 'Guardar Cambios' para confirmar.";
                    lblCaratulaUpload.ForeColor = System.Drawing.Color.Green;
                }
                catch (Exception ex)
                {
                    lblCaratulaUpload.Text = "Error " + ex.Message;
                    lblCaratulaUpload.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                lblCaratulaUpload.Text = "Selecciona una imagen";
                lblCaratulaUpload.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}