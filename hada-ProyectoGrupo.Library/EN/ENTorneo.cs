using hada_ProyectoGrupo.Library.CAD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hada_ProyectoGrupo.Library.EN
{
    public class ENTorneo
    {
        private int _codigo;
        private int _id_videojuego;
        private float _precioInscripcion;
        private string _nombre;
        private string _descripcion;
        private bool _profesional;
        private float _costeOrganizacion;
        private DateTime _fecha;
        private string _ubicacion;
        private float _premio;
        private int _capacidad;
        private string _url_logo;

        public ENTorneo()
        {
            _codigo = 0;
            _id_videojuego = 0;
            _precioInscripcion = 0.0f;
            _nombre = string.Empty;
            _descripcion = string.Empty;
            _profesional = false;
            _costeOrganizacion = 0.0f;
            _fecha = DateTime.MinValue;
            _ubicacion = string.Empty;
            _premio = 0.0f;
            _capacidad = 32;
            _url_logo = "";
        }

        public ENTorneo(int codigo, int id_videojuego, float precioInscripcion, string nombre,
            string descripcion, bool profesional, float costeOrganizacion, DateTime fecha, string ubicacion, float premio, int capacidad, string url_logo)
        {
            _codigo = codigo;
            _id_videojuego = id_videojuego;
            _precioInscripcion = precioInscripcion;
            _nombre = nombre;
            _descripcion = descripcion;
            _profesional = profesional;
            _costeOrganizacion = costeOrganizacion;
            _fecha = fecha;
            _ubicacion = ubicacion;
            _premio = premio;
            _capacidad = capacidad;
            _url_logo = url_logo;
        }

        // Para los parámetros obligatorios
        public ENTorneo(int codigo, int id_videojuego, float precioInscripcion,
            string nombre, bool profesional, float costeOrganizacion,string  ubicacion)
        {
            _codigo = codigo;
            _id_videojuego = id_videojuego;
            _precioInscripcion = precioInscripcion;
            _nombre = nombre;
            _descripcion = "";
            _profesional = profesional;
            _costeOrganizacion = costeOrganizacion;
            _fecha = DateTime.MinValue;
            _ubicacion = ubicacion;
        }

        // Para solo lo obligatorio
        public ENTorneo(int codigo, float precioInscripcion, string nombre,
            bool profesional, float costeOrganizacion, string ubicacion)
        {
            _codigo = codigo;
            _precioInscripcion = precioInscripcion;
            _nombre = nombre;
            _descripcion = string.Empty;
            _profesional = profesional;
            _costeOrganizacion = costeOrganizacion;
            _fecha = DateTime.MinValue;
            _ubicacion = ubicacion;
        }

        // ── Propiedades ──────────────────────────────────────────

        public int Codigo
        {
            get { return _codigo; }
            set { _codigo = value; }
        }
        public int IdVideojuego
        {
            get { return _id_videojuego; }
            set { _id_videojuego = value; }
        }
        public float PrecioInscripcion
        {
            get { return _precioInscripcion; }
            set { _precioInscripcion = value; }
        }
        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }
        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }
        public bool Profesional
        {
            get { return _profesional; }
            set { _profesional = value; }
        }
        public float CosteOrganizacion
        {
            get { return _costeOrganizacion; }
            set { _costeOrganizacion = value; }
        }
        public DateTime Fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }

        public string Ubicacion
        {
            get { return _ubicacion; }
            set { _ubicacion = value; }
        }

        public float Premio
        {
            get { return _premio; }
            set { _premio = value; }
        }

        public int Capacidad
        {
            get { return _capacidad;}
            set { _capacidad = value;}
        }

        public string Url_logo
        {
            get { return _url_logo; }
            set { _url_logo = value; }
        }

        public bool Create()
        {
            CADTorneo cad = new CADTorneo();
            return cad.Create(this);
        }
        public bool Read()
        {
            CADTorneo cad = new CADTorneo();
            return cad.Read(this);
        }
        public bool Update()
        {
            CADTorneo cad = new CADTorneo();
            return cad.Update(this);
        }
        public bool Delete()
        {
            CADTorneo cad = new CADTorneo();
            return cad.Delete(this);
        }
        public List<ENTorneo> ReadAll()
        {
            CADTorneo cad = new CADTorneo();
            return cad.ReadAll();
        }

    }
}