using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hada_ProyectoGrupo.Library.EN
{
    public class EquipoEN
    {
        public int id_equipo { get; set; } // PK
        public string nombre { get; set; } // NOT NULL, UNIQUE
        public DateTime fecha_creacion { get; set; } // NOT NULL
        public string logo_url { get; set; }
        public string descripcion { get; set; }
        public int id_capitan { get; set; } // FK a Jugador
    }       
}
