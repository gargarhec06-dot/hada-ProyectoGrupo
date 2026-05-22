using hada_ProyectoGrupo.Library.CAD;
using System;
using System.Collections.Generic;

namespace hada_ProyectoGrupo.Library.EN
{
    public class ENEquipo
    {
        private int _id_equipo;
        private string _nombre;
        private DateTime _fecha_creacion;
        private string _logo_url;
        private string _descripcion;
        private int _id_capitan;
        private int _max_jugadores;
        private int _miembros_actuales;
        public int Id_equipo
        {
            get { return _id_equipo; }
            set { _id_equipo = value; }
        }

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        public DateTime Fecha_creacion
        {
            get { return _fecha_creacion; }
            set { _fecha_creacion = value; }
        }

        public string Logo_url
        {
            get { return _logo_url; }
            set { _logo_url = value; }
        }

        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }

        public int Id_capitan
        {
            get { return _id_capitan; }
            set { _id_capitan = value; }
        }

        public int Max_jugadores
        {
            get { return _max_jugadores; }
            set { _max_jugadores = value; }
        }

        public int MiembrosActuales
        {
            get { return _miembros_actuales; }
            set { _miembros_actuales = value; }
        }
        public ENEquipo()
        {
            _id_equipo = 0;
            _nombre = "";
            _fecha_creacion = DateTime.Now;
            _logo_url = "";
            _descripcion = "";
            _id_capitan = 0;
            _max_jugadores = 5;
            _miembros_actuales = 0;
        }

        public ENEquipo(int id, string nombre, DateTime fecha, string logo, string descripcion, int capitan, int maxJugadores, int miembrosActuales)
        {
            _id_equipo = id;
            _nombre = nombre;
            _fecha_creacion = fecha;
            _logo_url = logo;
            _descripcion = descripcion;
            _id_capitan = capitan;
            _max_jugadores = maxJugadores;
            _miembros_actuales = miembrosActuales;
        }
        public bool Create()
        {
            CADEquipo cad = new CADEquipo();
            return cad.Create(this);
        }

        public bool Read()
        {
            CADEquipo cad = new CADEquipo();
            return cad.Read(this);
        }

        public bool Update()
        {
            CADEquipo cad = new CADEquipo();
            return cad.Update(this);
        }

        public bool Delete()
        {
            CADEquipo cad = new CADEquipo();
            return cad.Delete(this);
        }

        public List<ENEquipo> ReadAll()
        {
            CADEquipo cad = new CADEquipo();
            return cad.ReadAll();
        }
    }
}