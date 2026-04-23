using hada_ProyectoGrupo.Library.EN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hada_ProyectoGrupo.Library.CAD
{
    public class CADPremio
    {
        public CADPremio() { }

        public bool Create(ENPremio en)
        {
            bool ok = true;

            return ok;
        }

        public bool Read(ENPremio en)
        {
            bool ok = true;

            return ok;
        }

        public bool Update(ENPremio en)
        {
            bool ok = true;

            return ok;
        }

        public bool Delete(ENPremio en)
        {
            bool ok = true;

            return ok;
        }

        /// <summary>
        /// Devuelve todos los premios salvo que ocurra un error
        /// Disponibilidad por si tenemos que filtrarlas por algún criterio
        /// </summary>
        /// <returns>Lista con todas los premios</returns>
        public List<ENPremio> ReadAll()
        {
            List<ENPremio> partidas = new List<ENPremio>();

            return partidas;
        }
    }
}
