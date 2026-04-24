using hada_ProyectoGrupo.Library.EN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hada_ProyectoGrupo.Library.CAD
{
    public class CADPartida
    {
        public CADPartida () { }

        public bool Create(ENPartida en)
        {
            bool ok = true;

            return ok;
        }

        public bool Read(ENPartida en)
        {
            bool ok = true;

            return ok;
        }

        public bool Update(ENPartida en)
        {
            bool ok = true;

            return ok;
        }

        public bool Delete(ENPartida en)
        {
            bool ok = true;

            return ok;
        }

        /// <summary>
        /// Devuelve todas las partidas salvo que ocurra un error
        /// Disponibilidad por si tenemos que filtrarlas por algún criterio
        /// </summary>
        /// <returns>Lista con todas las partidas</returns>
        public List<ENPartida> ReadAll()
        {
            List<ENPartida> partidas = new List<ENPartida>();

            return partidas;
        }
    }
}
