using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hada_ProyectoGrupo.Library.EN
{
    public class ENPremio
    {
        private int _codigo;
        /// <summary>
        /// Recordar que torneo es una clave ajena
        /// </summary>
        private int _torneo;
        private string _nombre;
        private int _posicion;
        private String _descripcion;
        /// <summary>
        /// Recordar que debe solo tener 3 caracteres en mayúscula
        /// </summary>
        private String _moneda;
        private int _monetario;
        private String _no_monetario;

        public ENPremio() {
            _torneo = 0;
            _nombre = "";
            _posicion = 0;
            _descripcion = "";
            _moneda = "XXX";
            _monetario = 0;
            _no_monetario = "";
        }

        public ENPremio(int codigo, int torneo, String nombre, int posicion, String descripcion, String moneda, int monetario, String no_monetario)
        {
            /// Check para evitar input invalido de moneda
            if (moneda.Length != 3)
            {
                throw new ArgumentException("moneda did not have 3 characters");
            }

            this._codigo = codigo;
            this._torneo = torneo;
            this._posicion = posicion;
            this._descripcion = descripcion;
            this._moneda = moneda;
            this._monetario = monetario;
            this._no_monetario = no_monetario;
        }

        public int Codigo {
            get { return _codigo; }
            set { _codigo = value; }
        }

        public int Torneo
        {
            get { return _torneo; }
            set { _torneo = value; }
        }

        public String Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        public int Posicion
        {
            get { return _posicion; }
            set { _posicion = value; }
        }

        public String Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }

        public String Moneda
        {
            get { return _moneda; }
            set { _moneda = value; }
        }

        public int Monetario
        {
            get { return _monetario; }
            set { _monetario = value; }
        }

        public String NoMonetario
        {
            get { return _no_monetario; }
            set { _no_monetario = value; }
        }
    }
}
