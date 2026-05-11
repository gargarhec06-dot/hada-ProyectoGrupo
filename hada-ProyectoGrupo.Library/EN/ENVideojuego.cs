using hada_ProyectoGrupo.Library.CAD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace hada_ProyectoGrupo.Library.EN
{
    public class ENVideojuego
    {
        private int _codigo;
        private string _nombre;
        private string _descripcion;
        private string _tipo;   //El tipo puede ser : SH (Shooter) , SU(Supervivencia), ST(Estrategia) , FG(Fighter), SP(Speedrun), MO (Moba)
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
        public List<ENVideojuego> ReadAllFiltered(int ed_max)
        {
            CADVideojuego cad = new CADVideojuego();
            return cad.ReadAllFiltered(this, ed_max);
        }


        public enum ENVideojuegoTipo
        {
            undefined, // FAILSAFE
            MO, // MOBA
            SH, // Shooter
            SP, // Speedrun
            SU, // Supervivencia
            ST, // Estrategia
            FG, // Fighting
        }

        public static string GetVideojuegoTipoToNombreLegible(ENVideojuegoTipo tipo)
        {
            switch (tipo)
            {
                case ENVideojuegoTipo.MO:
                    return "Moba";
                case ENVideojuegoTipo.SH:
                    return "Shooter";
                case ENVideojuegoTipo.SP:
                    return "Speedrun";
                case ENVideojuegoTipo.SU:
                    return "Supervivencia";
                case ENVideojuegoTipo.ST:
                    return "Estrategia";
                case ENVideojuegoTipo.FG:
                    return "Fighter";
            }

            return "Desconocido";
        }

        public static ENVideojuegoTipo GetVideojuegoTipoFromCode(string code)
        {
            try
            {
                ENVideojuegoTipo tipo = (ENVideojuegoTipo)Enum.Parse(typeof(ENVideojuegoTipo), code);
                return tipo;
            }
            catch (Exception) {
                return ENVideojuegoTipo.undefined;
            }
        }

        public static Dictionary<ENVideojuegoTipo, string> GetAllVideojuegoTipo()
        {
            Dictionary<ENVideojuegoTipo, string> tipos_videojuegos = new Dictionary<ENVideojuegoTipo, string>();

            foreach (string code in Enum.GetNames(typeof(ENVideojuegoTipo)))
            {
                if (code == "undefined") continue;

                tipos_videojuegos.Add((ENVideojuegoTipo)Enum.Parse(typeof(ENVideojuegoTipo), code), code);
            }

            return tipos_videojuegos;
        }
    }
}
