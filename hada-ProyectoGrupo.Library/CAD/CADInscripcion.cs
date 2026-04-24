using hada_ProyectoGrupo.Library.EN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hada_ProyectoGrupo.Library.CAD
{
    public class CADInscripcion
    {
        public CADInscripcion()
        {
        }

        public bool Create(ENInscripcion en)
        {
            bool ok = true;
            return ok;
        }

        public bool Read(ENInscripcion en)
        {
            bool ok = true;
            return ok;
        }

        public bool Update(ENInscripcion en)
        {
            bool ok = true;
            return ok;
        }

        public bool Delete(ENInscripcion en)
        {
            bool ok = true;
            return ok;
        }

        // Nuevo método siguiendo el estilo de CADTorneo
        public List<ENInscripcion> ReadAll()
        {
            List<ENInscripcion> lista = new List<ENInscripcion>();
            return lista;
        }
    }
}
