using hada_ProyectoGrupo.Library.EN;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace hada_ProyectoGrupo.Library.CAD
{
    public class CADNoticia
    {
        private string constring;

        public CADNoticia()
        {
            constring = ConfigurationManager.ConnectionStrings["HadaEsports"].ConnectionString;
        }

        public bool Create(ENNoticia en)
        {
            bool creado = false;
            SqlConnection c = new SqlConnection(constring);
            try
            {
                c.Open();
                // Ajustado a los nombres de tu tabla: IdUsuario e ImagenUrl
                string query = "INSERT INTO Noticia (Titulo, Contenido, FechaPublicacion, IdUsuario, ImagenUrl) VALUES (@tit, @cont, @fecha, @user, @img)";
                SqlCommand com = new SqlCommand(query, c);
                com.Parameters.AddWithValue("@tit", en.Titulo);
                com.Parameters.AddWithValue("@cont", en.Contenido);
                com.Parameters.AddWithValue("@fecha", en.FechaPublicacion);
                com.Parameters.AddWithValue("@user", en.EmailUsuario);
                com.Parameters.AddWithValue("@img", (object)en.ImagenUrl ?? DBNull.Value);

                creado = com.ExecuteNonQuery() > 0;
            }
            catch (Exception ex) { throw ex; }
            finally { c.Close(); }
            return creado;
        }

        public bool Read(ENNoticia en)
        {
            bool leido = false;
            SqlConnection c = new SqlConnection(constring);
            try
            {
                c.Open();
                string query = "SELECT * FROM Noticia WHERE IdNoticia = @id";
                SqlCommand com = new SqlCommand(query, c);
                com.Parameters.AddWithValue("@id", en.IdNoticia);
                SqlDataReader dr = com.ExecuteReader();
                if (dr.Read())
                {
                    en.Titulo = dr["Titulo"].ToString();
                    en.Contenido = dr["Contenido"].ToString();
                    en.FechaPublicacion = DateTime.Parse(dr["FechaPublicacion"].ToString());
                    en.EmailUsuario = dr["IdUsuario"].ToString();
                    en.ImagenUrl = dr["ImagenUrl"].ToString(); // Lectura de la nueva columna
                    leido = true;
                }
            }
            catch (Exception ex) { throw ex; }
            finally { c.Close(); }
            return leido;
        }

        public List<ENNoticia> ReadAll()
        {
            List<ENNoticia> lista = new List<ENNoticia>();
            SqlConnection c = new SqlConnection(constring);
            try
            {
                c.Open();
                string query = "SELECT * FROM Noticia";
                SqlCommand com = new SqlCommand(query, c);
                SqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    ENNoticia n = new ENNoticia();
                    n.IdNoticia = int.Parse(dr["IdNoticia"].ToString());
                    n.Titulo = dr["Titulo"].ToString();
                    n.Contenido = dr["Contenido"].ToString();
                    n.FechaPublicacion = DateTime.Parse(dr["FechaPublicacion"].ToString());
                    n.EmailUsuario = dr["IdUsuario"].ToString();
                    n.ImagenUrl = dr["ImagenUrl"].ToString(); // Lectura para la galería
                    lista.Add(n);
                }
            }
            catch (Exception ex) { throw ex; }
            finally { c.Close(); }
            return lista;
        }

        public bool Update(ENNoticia en)
        {
            bool modificado = false;
            SqlConnection c = new SqlConnection(constring);
            try
            {
                c.Open();
                // Actualización incluyendo ImagenUrl y usando IdUsuario
                string query = "UPDATE Noticia SET Titulo=@tit, Contenido=@cont, FechaPublicacion=@fecha, IdUsuario=@user, ImagenUrl=@img WHERE IdNoticia=@id";
                SqlCommand com = new SqlCommand(query, c);
                com.Parameters.AddWithValue("@id", en.IdNoticia);
                com.Parameters.AddWithValue("@tit", en.Titulo);
                com.Parameters.AddWithValue("@cont", en.Contenido);
                com.Parameters.AddWithValue("@fecha", en.FechaPublicacion);
                com.Parameters.AddWithValue("@user", en.EmailUsuario);
                com.Parameters.AddWithValue("@img", (object)en.ImagenUrl ?? DBNull.Value);

                modificado = com.ExecuteNonQuery() > 0;
            }
            catch (Exception ex) { throw ex; }
            finally { c.Close(); }
            return modificado;
        }

        public bool Delete(ENNoticia en)
        {
            bool borrado = false;
            SqlConnection c = new SqlConnection(constring);
            try
            {
                c.Open();
                string query = "DELETE FROM Noticia WHERE IdNoticia = @id";
                SqlCommand com = new SqlCommand(query, c);
                com.Parameters.AddWithValue("@id", en.IdNoticia);
                borrado = com.ExecuteNonQuery() > 0;
            }
            catch (Exception ex) { throw ex; }
            finally { c.Close(); }
            return borrado;
        }
    }
}