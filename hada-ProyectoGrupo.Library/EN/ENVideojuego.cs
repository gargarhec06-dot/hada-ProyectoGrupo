using hada_ProyectoGrupo.Library.CAD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hada_ProyectoGrupo.Library.EN
{
    public class ENVideojuego
    {
        private int _codigo;
        private string _nombre;
        private string _descripcion;
        private string _tipo;   //El tipo puede ser : SH (Shooter) , SU(Supervivencia), ST(Estrategia) , FG(Fighter)
        private int _edadMinima;


        public ENVideojuego()
        {
           _codigo = 0;
            _nombre = string.Empty;
            _descripcion = string.Empty;
            _tipo = string.Empty;
            _edadMinima = 0;
        }
        //Como todos los parametros son obligatorios basta con este constructor para inicializar
        public ENVideojuego(int codigo, string nombre, string descripcion, string tipo, int edadMinima)
        {
            _codigo = codigo;
            _nombre = nombre;
            _descripcion = descripcion;
            _tipo = tipo;
            _edadMinima = edadMinima;
        }
        public int Codigo
        {
            get { return _codigo; }
            set { _codigo = value; }
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
        public string Tipo
        {
            get { return _tipo; }
            set { _tipo = value; }
        }
        public int EdadMinima
        {
            get { return _edadMinima; }
            set { _edadMinima = value;}
        }
        public bool Create()
        {
            CADVideojuego cad = new CADVideojuego();
            return cad.Create(this);
        }

        public bool Read()
        {
            CADVideojuego cad = new CADVideojuego();
            return cad.Read(this);
        }

        public bool Update()
        {
            CADVideojuego cad = new CADVideojuego();
            return cad.Update(this);
        }

        public bool Delete()
        {
            CADVideojuego cad = new CADVideojuego();
            return cad.Delete(this);
        }
        public List<ENVideojuego> ReadAll()
        {
            CADVideojuego cad = new CADVideojuego();
            return cad.ReadAll();
        }
    }
}
