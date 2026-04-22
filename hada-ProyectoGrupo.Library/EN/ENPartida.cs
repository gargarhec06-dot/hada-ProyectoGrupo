using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hada_ProyectoGrupo.Library.EN
{
    public class ENPartida
    {
        private int _code;
        private string _enlace_repeticion;
        private DateTime _fecha;
        private int _videojuego;
        private int _ganador;
        private int[] _perdedores;
        private int[] _jugadores;


        public ENPartida()
        {
            _code = 0;
            _enlace_repeticion = "";
            _fecha = DateTime.MinValue;
            _videojuego = 0;
            _ganador = 0;
            _perdedores = new int[0];
        }

        public ENPartida(int code, string enlace_repeticion, DateTime fecha, int videojuego, int ganador, int[] perdedores, int[] jugadores)
        {
            this._code = code;
            this._enlace_repeticion = enlace_repeticion;
            this._fecha = fecha;
            this._videojuego = videojuego;
            this._ganador = ganador;
            this._perdedores = new int[perdedores.Length];
            for (int i = 0; i < perdedores.Length; i++)
            {
                this._perdedores[i] = perdedores[i];
            }
            this._jugadores = new int[jugadores.Length];
            for (int i = 0; i < jugadores.Length; i++)
            {
                this._jugadores[i] = jugadores[i];
            }
        }

        public int Code
        {
            get { return _code; }
            set { _code = value; }
        }

        public String EnlaceDeRepeticion
        {
            get { return _enlace_repeticion; }
            set { _enlace_repeticion = value; }
        }

        public DateTime Fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }

        public int Videojuego
        {
            get { return _videojuego; }
            set { _videojuego = value; }
        }

        public int Ganador
        {
            get { return _ganador; }
            set { _ganador = value; }
        }

        /// <summary>
        /// Proxy para la variable privada _perdedores
        /// Los arrays como tal solo copian una referencia, así que es modificable externamente
        /// </summary>
        public int[] Perdedores
        {
            get { return _perdedores; }
            set { _perdedores = value; }
        }

        /// <summary>
        /// Proxy para la variable privada _jugadores
        /// Los arrays como tal solo copian una referencia, así que es modificable externamente
        /// </summary>
        public int[] Jugadores
        {
            get { return _jugadores; }
            set { _jugadores = value; }
        }
    }
}
