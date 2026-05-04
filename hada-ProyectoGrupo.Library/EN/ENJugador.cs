using hada_ProyectoGrupo.Library.CAD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hada_ProyectoGrupo.Library.EN
{
    public class ENJugador
    {
        private int _codigo;
        private string _email_usuario;
        private string _apodo;
        private string _rol_principal;
        private float _kda_promedio;
        private float _winrate;
        private int _nivel;
        private string _hardware;
        private bool _buscando_equipo;
        private int _equipo_actual;
        private int _juego;

        public ENJugador()
        {
            _codigo = 0;
            _email_usuario = "";
            _apodo = "";
            _rol_principal = "";
            _kda_promedio = 0.0f;
            _winrate = 0.0f;
            _nivel = 1;
            _hardware = "";
            _buscando_equipo = false;
            _equipo_actual = 0;
            _juego = 0;
        }

        public ENJugador(int codigo, string email, string apodo, string rol, float kda, float winrate, int nivel, string hardware, bool buscando, int equipo,int juego)
        {
            _codigo = codigo;
            _email_usuario = email;
            _apodo = apodo;
            _rol_principal = rol;
            _kda_promedio = kda;
            _winrate = winrate;
            _nivel = nivel;
            _hardware = hardware;
            _buscando_equipo = buscando;
            _equipo_actual = equipo;
            _juego = juego;
        }

        // Constructor para los parametro obligatorios
        public ENJugador(string email, string apodo)
        {
            _email_usuario = email;
            _apodo = apodo;
            _nivel = 1; 
            _buscando_equipo = false;
            _kda_promedio = 0.0f;
            _winrate = 0.0f;
        }

        public int Codigo
        {
            get { return _codigo; }
            set { _codigo = value; }
        }

        public string Email_usuario
        {
            get { return _email_usuario; }
            set { _email_usuario = value; }
        }

        public string Apodo
        {
            get { return _apodo; }
            set { _apodo = value; }
        }

        public string Rol_principal
        {
            get { return _rol_principal; }
            set { _rol_principal = value; }
        }

        public float Kda_promedio
        {
            get { return _kda_promedio; }
            set { _kda_promedio = value; }
        }

        public float Winrate
        {
            get { return _winrate; }
            set { _winrate = value; }
        }

        public int Nivel
        {
            get { return _nivel; }
            set { _nivel = value; }
        }

        public string Hardware
        {
            get { return _hardware; }
            set { _hardware = value; }
        }

        public bool Buscando_equipo
        {
            get { return _buscando_equipo; }
            set { _buscando_equipo = value; }
        }

        public int Equipo_actual
        {
            get { return _equipo_actual; }
            set { _equipo_actual = value; }
        }

        public int Juego
        {
            get { return _juego; }
            set { _juego = value; }
        }

        public bool Create()
        {
            CADJugador cad = new CADJugador();
            return cad.Create(this);
        }

        public bool Read()
        {
            CADJugador cad = new CADJugador();
            return cad.Read(this);
        }

        public bool Update()
        {
            CADJugador cad = new CADJugador();
            return cad.Update(this);
        }

        public bool Delete()
        {
            CADJugador cad = new CADJugador();
            return cad.Delete(this);
        }

        public List<ENJugador> ReadAll()
        {
            CADJugador cad = new CADJugador();
            return cad.ReadAll();
        }

    }
}
