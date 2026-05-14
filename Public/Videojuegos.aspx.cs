using hada_ProyectoGrupo.Library.EN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace hada_ProyectoGrupo.Public
{
    public partial class Videojuegos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadVideojuego(new ENVideojuego().ReadAll());

            // Cargar filtros posibles
            if (!IsPostBack)
            {
                ddlTipo.Items.Add(new ListItem("Todos", "%"));
                foreach (ENVideojuego.ENVideojuegoTipo tipo_no_undefined in ENVideojuego.GetAllVideojuegoTipo().Keys)
                {
                    ddlTipo.Items.Add(new ListItem(
                            ENVideojuego.GetVideojuegoTipoToNombreLegible(tipo_no_undefined),
                            tipo_no_undefined.ToString()
                        ));
                }
            }

            if (Session["EsAdmin"] != null && (bool)Session["EsAdmin"])
            {
                CreateVideojuego.Visible = true;
            }
        }

        protected void LoadVideojuego(List<ENVideojuego> list)
        {
            foreach (ENVideojuego en in list)
            {
                en.IconUrl = ResolveUrl(en.IconUrl);
                en.Tipo = ENVideojuego.GetVideojuegoTipoToNombreLegible(ENVideojuego.GetVideojuegoTipoFromCode(en.Tipo));
            }
            tableGenerator.DataSource = list;
            tableGenerator.DataBind();
        }

        protected void CreateVideojuego_Click(object sender, EventArgs e)
        {
            Response.Redirect("Videojuego.aspx");
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            LoadVideojuego(new ENVideojuego().ReadAll());
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            ENVideojuego filter = new ENVideojuego();


            // Prevención básica contra SQL injection
            if (txtNombre.Text != null && !txtNombre.Text.Contains("'"))
            {
                filter.Nombre = txtNombre.Text;
            }
            else
            {
                filter.Nombre = "";
            }
            filter.Tipo = ddlTipo.SelectedItem.Value;
            int min_age = 0;
            if (!int.TryParse(txtEDMin.Text, out min_age)) min_age = 0;
            filter.EdadMinima = min_age;
            int max_age = 100;
            if (!int.TryParse(txtEDMax.Text, out max_age)) max_age = 100;

            LoadVideojuego(filter.ReadAllFiltered(max_age));

            // Debug para ver el query
            //txtNombre.Text = "SELECT * FROM Videojuego WHERE nombre LIKE '%" + filter.Nombre + "%' AND tipo LIKE '" + filter.Tipo + "' AND " + min_age.ToString() + " <= edadminima AND edadminima <= " + max_age.ToString();
        }
    }
}