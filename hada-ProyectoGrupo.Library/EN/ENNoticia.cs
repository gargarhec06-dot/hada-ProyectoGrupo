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
        private string _imagenUrl;
        private int _visitas;
        private int _likes; 

        // Propiedades públicas (Las que usa Eval() en el ASPX)
        public int IdNoticia { get { return _idNoticia; } set { _idNoticia = value; } }
        public string Titulo { get { return _titulo; } set { _titulo = value; } }
        public string Contenido { get { return _contenido; } set { _contenido = value; } }
        public DateTime FechaPublicacion { get { return _fecha; } set { _fecha = value; } }
        public string EmailUsuario { get { return _emailUsuario; } set { _emailUsuario = value; } }
        public string ImagenUrl { get { return _imagenUrl; } set { _imagenUrl = value; } }
        public int Visitas { get { return _visitas; } set { _visitas = value; } }
        public int Likes { get { return _likes; } set { _likes = value; } }

        // Constructores
        public ENNoticia()
        {
            _imagenUrl = "";
            _visitas = 0;
            _likes = 0;     
        }

        public ENNoticia(int id, string tit, string cont, DateTime fecha, string user, string img, int visitas, int likes)
        {
            this.IdNoticia = id;
            this.Titulo = tit;
            this.Contenido = cont;
            this.FechaPublicacion = fecha;
            this.EmailUsuario = user;
            this.ImagenUrl = img;
            this.Visitas = visitas;
            this.Likes = likes; 
        }

        // Métodos de persistencia
        public bool Create() { return new CADNoticia().Create(this); }
        public bool Read() { return new CADNoticia().Read(this); }
        public List<ENNoticia> ReadAll() { return new CADNoticia().ReadAll(); }
        public bool Update() { return new CADNoticia().Update(this); }
        public bool Delete() { return new CADNoticia().Delete(this); }

        // Método para incrementar visualizaciones
        public void IncrementarVisitas(int id)
        {
            new CADNoticia().IncrementarVisitas(id);
        }

        //  MÉTODOS PARA LIKES
        // Agrega o quita el like según si ya existe (Toggle)
        public void ToggleLike(string emailUsuario)
        {
            new CADNoticia().ToggleLike(this.IdNoticia, emailUsuario);
        }

        // Comprueba si este usuario ya le ha dado like a la noticia
        public bool UsuarioYaDioLike(string emailUsuario)
        {
            return new CADNoticia().VerificarLike(this.IdNoticia, emailUsuario);
        }
    }
}