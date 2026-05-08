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
                // SQL con los nombres de la tabla
                string query = "INSERT INTO Noticia (Titulo, Contenido, FechaPublicacion, IdUsuario) VALUES (@tit, @cont, @fecha, @user)";
                SqlCommand com = new SqlCommand(query, c);
                com.Parameters.AddWithValue("@tit", en.Titulo);
                com.Parameters.AddWithValue("@cont", en.Contenido);
                com.Parameters.AddWithValue("@fecha", en.FechaPublicacion);
                com.Parameters.AddWithValue("@user", en.EmailUsuario);

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
                string query = "UPDATE Noticia SET Titulo=@tit, Contenido=@cont, FechaPublicacion=@fecha, IdUsuario=@user WHERE IdNoticia=@id";
                SqlCommand com = new SqlCommand(query, c);
                com.Parameters.AddWithValue("@id", en.IdNoticia);
                com.Parameters.AddWithValue("@tit", en.Titulo);
                com.Parameters.AddWithValue("@cont", en.Contenido);
                com.Parameters.AddWithValue("@fecha", en.FechaPublicacion);
                com.Parameters.AddWithValue("@user", en.EmailUsuario);
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