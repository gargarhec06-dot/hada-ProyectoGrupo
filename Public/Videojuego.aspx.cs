using hada_ProyectoGrupo.Library.EN;
using System;
using System.Collections.Generic;
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
            // Se debe de obtener el videojuego en un futuro con esta variable
            string code = Request.QueryString["codigo"];

            if (string.IsNullOrEmpty(code)) {
                DebugLabel.Text = "No se encontró ningún argumento para el código del juego";
                return;
            }

            ENVideojuego videojuego = new ENVideojuego(int.Parse(code), "", "", "", 0);
            bool result = videojuego.Read();

            if (result) {
                NombreLabel.Text = videojuego.Nombre;
                CodigoLabel.Text = videojuego.Codigo.ToString();
                DescripcionLabel.Text = videojuego.Descripcion;
                TipoLabel.Text = videojuego.Tipo;
                EdadMinimaLabel.Text = videojuego.EdadMinima.ToString();

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
                        TipoAdminBox.Text = TipoLabel.Text;
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
            AdminAdd.Visible = true;
        }

        protected void AdminUpdate_Click(object sender, EventArgs e)
        {
            if (Session["EsAdmin"] != null && (bool)Session["EsAdmin"])
            {
                ENVideojuego entry = new ENVideojuego(
                    int.Parse(CodigoAdminBox.Text),
                    NombreAdminBox.Text,
                    DescripcionAdminBox.Text,
                    TipoAdminBox.Text,
                    int.Parse(EdadMinimaAdminBox.Text)
                );

                entry.Update();
            }
        }

        protected void AdminDelete_Click(object sender, EventArgs e)
        {
            if (Session["EsAdmin"] != null && (bool)Session["EsAdmin"])
            {
                ENVideojuego entry = new ENVideojuego();
                entry.Codigo = int.Parse(CodigoAdminBox.Text);

                entry.Delete();
            }
        }
    }
}