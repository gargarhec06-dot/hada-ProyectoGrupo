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
        private List<ENPremio> _premios;
        private string _ubicacion;

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
            _premios = new List<ENPremio>();
            _ubicacion = string.Empty;
        }

        public ENTorneo(int codigo, int id_videojuego, float precioInscripcion, string nombre,
            string descripcion, bool profesional, float costeOrganizacion, DateTime fecha, string ubicacion)
        {
            _codigo = codigo;
            _id_videojuego = id_videojuego;
            _precioInscripcion = precioInscripcion;
            _nombre = nombre;
            _descripcion = descripcion;
            _profesional = profesional;
            _costeOrganizacion = costeOrganizacion;
            _fecha = fecha;
            _premios = new List<ENPremio>();
            _ubicacion = ubicacion;
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
            _premios = new List<ENPremio>();
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
            _premios = new List<ENPremio>();
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
        public List<ENPremio> Premios
        {
            get { return _premios; }
            set { _premios = value; }
        }

        public string Ubicacion
        {
            get { return _ubicacion; }
            set { _ubicacion = value; }
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

        public void CargarPremios()
        {
            ENPremio enPremio = new ENPremio();
            List<ENPremio> todos = enPremio.ReadAll();
            _premios = todos.Where(p => p.Torneo == _codigo).ToList();
        }
    }
}