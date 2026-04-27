using hada_ProyectoGrupo.Library.EN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hada_ProyectoGrupo.Library.CAD
{
    public class CADEquipo
    {
        public CADEquipo()
        {
        }

        public bool Create(ENEquipo en)
        {
            bool ok = true;
            return ok;
        }

        public bool Read(ENEquipo en)
        {
            bool ok = true;
            return ok;
        }

        public bool Update(ENEquipo en)
        {
            bool ok = true;
            return ok;
        }

        public bool Delete(ENEquipo en)
        {
            bool ok = true;
            return ok;
        }

        // Nuevo método siguiendo el estilo de CADTorneo
        public List<ENEquipo> ReadAll()
        {
            List<ENEquipo> lista = new List<ENEquipo>();
            return lista;
        }
    }
}
