using hada_ProyectoGrupo.Library.EN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hada_ProyectoGrupo.Library.CAD
{
    public class CADTorneo
    {
        public CADTorneo()
        {

        }

        public bool Create(ENTorneo en)
        {
            bool ok = true;


            return ok;
        }

        public bool Read(ENTorneo en)
        {
            bool ok = true;


            return ok;
        }

        public bool Update(ENTorneo en)
        {
            bool ok = true;


            return ok;
        }

        public bool Delete(ENTorneo en)
        {
            bool ok = true;


            return ok;
        }
        public List<ENTorneo> ReadAll()
        {
            List<ENTorneo> lista = new List<ENTorneo>
            {
                new ENTorneo(101, 1, 20.0f, "Valorant Cup 2026",
                    "Torneo elite de estrategia.", true, 800.0f,
                    new DateTime(2026, 5, 10)),

                new ENTorneo(102, 2, 5.0f, "FIFA championship",
                    "Torneo abierto para todos.", false, 150.0f,
                    new DateTime(2026, 5, 25)),

                new ENTorneo(103, 1, 10.0f, "Torneo Invitacional",
                    "Solo jugadores invitados.", true, 400.0f,
                    new DateTime(2026, 6, 9))
            };

            return lista;
        }
    }
}
