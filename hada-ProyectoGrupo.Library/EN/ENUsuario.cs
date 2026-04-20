using hada_ProyectoGrupo.Library.CAD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hada_ProyectoGrupo.Library.EN
{
    public class ENUsuario
    {
        private string _email;
        private string _password;
        private string _nombre;
        private string _apellidos;
        private DateTime _fecha_nacimiento;
        private string _pais;
        private float _saldo_cartera;
        private bool _verificado;


        public ENUsuario()
        {
            _email = "";
            _password = "";
            _nombre = "";
            _apellidos = "";
            _fecha_nacimiento = DateTime.Now;
            _pais = "";
            _saldo_cartera = 0;
            _verificado = false;
        }

        public string Email
        {
            get { return _email; }

            set { _email = value; }
        }

        public string Password
        {
            get { return _password; }

            set { _password = value; }
        }

        public string Nombre 
        {  
            get { return _nombre; } 
            
            set {  _nombre = value; } 
        }

        public string Apellidos
        {
            get { return _apellidos;}

            set { _apellidos = value;}
        }

        public DateTime Fecha_Nacimiento
        {
            get { return _fecha_nacimiento; }

            set { _fecha_nacimiento = value;}
        }

        public string Pais
        {
            get { return _pais; }

            set { _pais = value; }
        }


        public float Saldo_cartera
        {
            get { return _saldo_cartera;}

            set { _saldo_cartera = value;}
        }

        public bool Verificado
        {
            get { return _verificado; }

            set { _verificado = value; }
        }

        public bool Login()
        {
            CADUsuario cad = new CADUsuario();
            return cad.Login(this);
        }

        public bool Register()
        {
            CADUsuario cad = new CADUsuario();
            return cad.Create(this);
        }

        public bool Update()
        {
            CADUsuario cad = new CADUsuario();
            return cad.Update(this);
        }

        public bool Delete()
        {
            CADUsuario cad = new CADUsuario();
            return cad.Delete(this);
        }
    }
}



