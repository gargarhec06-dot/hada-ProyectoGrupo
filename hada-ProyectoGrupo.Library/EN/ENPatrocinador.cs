using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hada_ProyectoGrupo.Library.CAD;

namespace hada_ProyectoGrupo.Library.EN
{
    public class ENPatrocinador
    {
        private int _idPatrocinador;
        private string _nombre;
        private string _email;
        private string _paginaWeb;
        private DateTime _inicioContrato;
        private DateTime _finContrato;
        private bool _activo;
        private string _telefono;

        public ENPatrocinador()
        {
            _idPatrocinador = 0;
            _nombre = "";
            _email = "";
            _paginaWeb = "";
            _inicioContrato = DateTime.Now;
            _finContrato = DateTime.Now;
            _activo = true;
            _telefono = "";
        }

        public ENPatrocinador(int idPatrocinador, string nombre, string email, string paginaWeb, DateTime inicioContrato, DateTime finContrato, bool activo,string telefono)
        {
            _idPatrocinador = idPatrocinador;
            _nombre = nombre;
            _email = email;
            _paginaWeb = paginaWeb;
            _inicioContrato = inicioContrato;
            _finContrato = finContrato;
            _activo = activo;
            _telefono = telefono;
        }

        // Constructor para los parámetros obligatorios
        public ENPatrocinador(string nombre, string email, DateTime inicioContrato, DateTime finContrato, string telefono)
        {
            _nombre = nombre;
            _email = email;
            _inicioContrato = inicioContrato;
            _finContrato = finContrato;
            _activo = true;
            _paginaWeb = "";
            _telefono = telefono;
        }

        public int IdPatrocinador
        {
            get { return _idPatrocinador; }
            set { _idPatrocinador = value; }
        }

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public string Telefono
        {
            get { return _telefono; }
            set { _telefono = value; }
        }

        public string PaginaWeb
        {
            get { return _paginaWeb; }
            set { _paginaWeb = value; }
        }

        public DateTime InicioContrato
        {
            get { return _inicioContrato; }
            set { _inicioContrato = value; }
        }

        public DateTime FinContrato
        {
            get { return _finContrato; }
            set { _finContrato = value; }
        }

        public bool Activo
        {
            get { return _activo; }
            set { _activo = value; }
        }

        public bool Create()
        {
            CADPatrocinador cad = new CADPatrocinador();
            return cad.Create(this);
        }

        public bool Read()
        {
            CADPatrocinador cad = new CADPatrocinador();
            return cad.Read(this);
        }

        public bool Update()
        {
            CADPatrocinador cad = new CADPatrocinador();
            return cad.Update(this);
        }

        public bool Delete()
        {
            CADPatrocinador cad = new CADPatrocinador();
            return cad.Delete(this);
        }

        public List<ENPatrocinador> ReadAll()
        {
            CADPatrocinador cad = new CADPatrocinador();
            return cad.ReadAll();
        }
    }
}
