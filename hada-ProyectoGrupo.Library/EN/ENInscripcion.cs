using hada_ProyectoGrupo.Library.CAD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hada_ProyectoGrupo.Library.EN
{
    public class ENInscripcion
    {
        // Atributos privados
        private int _id_inscripcion;
        private int _id_equipo;
        private int _id_torneo;
        private DateTime _fecha_inscripcion;
        private string _estado;
        private float _cuota_pagada;
        private string _moneda;

        // Constructor por defecto 
        public ENInscripcion()
        {
            _id_inscripcion = 0;
            _id_equipo = 0;
            _id_torneo = 0;
            _fecha_inscripcion = DateTime.Now;
            _estado = "Pendiente";
            _cuota_pagada = 0;
            _moneda = "EUR";
        }

        // Constructor completo
        public ENInscripcion(int id, int equipo, int torneo, DateTime fecha, string estado, float cuota, string moneda)
        {
            _id_inscripcion = id;
            _id_equipo = equipo;
            _id_torneo = torneo;
            _fecha_inscripcion = fecha;
            _estado = estado;
            _cuota_pagada = cuota;
            _moneda = moneda;
        }

        // Constructor para parámetros obligatorios
        public ENInscripcion(int equipo, int torneo, float cuota, string moneda)
        {
            _id_equipo = equipo;
            _id_torneo = torneo;
            _cuota_pagada = cuota;
            _moneda = moneda;
            _fecha_inscripcion = DateTime.Now;
            _estado = "Pendiente";
        }

        // Propiedades públicas
        public int Id_inscripcion
        {
            get { return _id_inscripcion; }
            set { _id_inscripcion = value; }
        }

        public int Id_equipo
        {
            get { return _id_equipo; }
            set { _id_equipo = value; }
        }

        public int Id_torneo
        {
            get { return _id_torneo; }
            set { _id_torneo = value; }
        }

        public DateTime Fecha_inscripcion
        {
            get { return _fecha_inscripcion; }
            set { _fecha_inscripcion = value; }
        }

        public string Estado
        {
            get { return _estado; }
            set { _estado = value; }
        }

        public float Cuota_pagada
        {
            get { return _cuota_pagada; }
            set { _cuota_pagada = value; }
        }

        public string Moneda
        {
            get { return _moneda; }
            set { _moneda = value; }
        }

        // Métodos CRUD llamando al CAD
        public bool Create()
        {
            CADInscripcion cad = new CADInscripcion();
            return cad.Create(this);
        }

        public bool Read()
        {
            CADInscripcion cad = new CADInscripcion();
            return cad.Read(this);
        }

        public bool Update()
        {
            CADInscripcion cad = new CADInscripcion();
            return cad.Update(this);
        }

        public bool Delete()
        {
            CADInscripcion cad = new CADInscripcion();
            return cad.Delete(this);
        }
    }
}

