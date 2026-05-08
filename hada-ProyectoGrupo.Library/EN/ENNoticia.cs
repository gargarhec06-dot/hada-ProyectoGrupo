using System;
using System.Collections.Generic;
using hada_ProyectoGrupo.Library.CAD;

namespace hada_ProyectoGrupo.Library.EN
{
    public class ENNoticia
    {
        // Propiedades privadas
        private int _idNoticia;
        private string _titulo;
        private string _contenido;
        private DateTime _fecha;
        private string _emailUsuario;

        // Propiedades públicas (Las que usa Eval() en el ASPX)
        public int IdNoticia { get { return _idNoticia; } set { _idNoticia = value; } }
        public string Titulo { get { return _titulo; } set { _titulo = value; } }
        public string Contenido { get { return _contenido; } set { _contenido = value; } }
        public DateTime FechaPublicacion { get { return _fecha; } set { _fecha = value; } }
        public string EmailUsuario { get { return _emailUsuario; } set { _emailUsuario = value; } }

        // Constructores
        public ENNoticia() { }
        public ENNoticia(int id, string tit, string cont, DateTime fecha, string user)
        {
            this.IdNoticia = id;
            this.Titulo = tit;
            this.Contenido = cont;
            this.FechaPublicacion = fecha;
            this.EmailUsuario = user;
        }

        // Métodos de persistencia
        public bool Create() { return new CADNoticia().Create(this); }
        public bool Read() { return new CADNoticia().Read(this); }
        public List<ENNoticia> ReadAll() { return new CADNoticia().ReadAll(); }
        public bool Update() { return new CADNoticia().Update(this); }
        public bool Delete() { return new CADNoticia().Delete(this); }
    }
}