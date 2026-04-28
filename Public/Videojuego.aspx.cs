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

            ENVideojuego videojuego = new ENVideojuego(9, "Rivals of Ather 2", "Juego de lucha plataformero modero con luchadores inspirandose en elementos. Normalmente jugado en modalidad 1v1, 3 stocks, 8 minutos.", "Fighting", 12);

            NombreLabel.Text = videojuego.Nombre;
            CodigoLabel.Text = videojuego.Codigo.ToString();
            DescripcionLabel.Text = videojuego.Descripcion;
            TipoLabel.Text = videojuego.Tipo;
            EdadMinimaLabel.Text = videojuego.EdadMinima.ToString();
        }
    }
}