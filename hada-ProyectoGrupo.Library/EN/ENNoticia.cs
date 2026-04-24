using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hada_ProyectoGrupo.Library.CAD;


namespace hada_ProyectoGrupo.Library.EN
{
    public class ENNoticia
    {
        private int _idNoticia;
        private string _titulo;
        private string _contenido;
        private DateTime _fechaPublicacion;
        private string _emailUsuario;

        public ENNoticia()
        {
            _idNoticia = 0;
            _titulo = "";
            _contenido = "";
            _fechaPublicacion = DateTime.Now;
            _emailUsuario = "";
        }

        public ENNoticia(int idNoticia, string titulo, string contenido, DateTime fechaPublicacion, string emailUsuario)
        {
            _idNoticia = idNoticia;
            _titulo = titulo;
            _contenido = contenido;
            _fechaPublicacion = fechaPublicacion;
            _emailUsuario = emailUsuario;
        }

        // Constructor para los parámetros obligatorios
        public ENNoticia(string titulo, string contenido, string emailUsuario)
        {
            _titulo = titulo;
            _contenido = contenido;
            _emailUsuario = emailUsuario;
            _fechaPublicacion = DateTime.Now;
        }

        public int IdNoticia
        {
            get { return _idNoticia; }
            set { _idNoticia = value; }
        }

        public string Titulo
        {
            get { return _titulo; }
            set { _titulo = value; }
        }

        public string Contenido
        {
            get { return _contenido; }
            set { _contenido = value; }
        }

        public DateTime FechaPublicacion
        {
            get { return _fechaPublicacion; }
            set { _fechaPublicacion = value; }
        }

        public string EmailUsuario
        {
            get { return _emailUsuario; }
            set { _emailUsuario = value; }
        }

        public bool Create()
        {
            CADNoticia cad = new CADNoticia();
            return cad.Create(this);
        }

        public bool Read()
        {
            CADNoticia cad = new CADNoticia();
            return cad.Read(this);
        }

        public bool Update()
        {
            CADNoticia cad = new CADNoticia();
            return cad.Update(this);
        }

        public bool Delete()
        {
            CADNoticia cad = new CADNoticia();
            return cad.Delete(this);
        }

        public List<ENNoticia> ReadAll()
        {
            CADNoticia cad = new CADNoticia();
            return cad.ReadAll();
        }

    }
}