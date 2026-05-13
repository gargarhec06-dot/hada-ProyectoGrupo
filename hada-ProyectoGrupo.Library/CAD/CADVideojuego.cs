using hada_ProyectoGrupo.Library.EN;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace hada_ProyectoGrupo.Library.CAD
{
    public class CADVideojuego
    {
        private string constring;
        public CADVideojuego()
        {
            constring = ConfigurationManager.ConnectionStrings["HadaEsports"].ToString();
        }

        public bool Create(ENVideojuego en)
        {
            bool ok = false;
            SqlConnection c = new SqlConnection(constring);
            try
            {
                c.Open();
                string query = "INSERT INTO Videojuego (nombre, descripcion, tipo, edadminima, icon_url, cara_url) VALUES (@nom, @desc, @tipo, @em, @iurl, @curl)";
                SqlCommand com = new SqlCommand(query, c);
                com.Parameters.AddWithValue("@nom", en.Nombre);
                com.Parameters.AddWithValue("@desc", en.Descripcion);
                com.Parameters.AddWithValue("@tipo", en.Tipo);
                com.Parameters.AddWithValue("@em", en.EdadMinima);
                com.Parameters.AddWithValue("@iurl", en.IconUrl);
                com.Parameters.AddWithValue("@curl", en.CaratulaUrl);
                if (com.ExecuteNonQuery() > 0) ok = true;
            }
            catch (Exception ex) { throw new Exception("Error al crear videojuego: " + ex.Message.ToString()); }
            finally { c.Close(); }
            return ok;
        }

        public bool Read(ENVideojuego en)
        {
            bool ok = false;
            SqlConnection c = new SqlConnection(constring);
            try
            {
                c.Open();
                string query = "SELECT * FROM Videojuego WHERE codigo = @cod";
                SqlCommand com = new SqlCommand(query, c);
                com.Parameters.AddWithValue("@cod", en.Codigo);
                SqlDataReader dr = com.ExecuteReader();
                if (dr.Read())
                {
                    en.Codigo = (int)dr["codigo"];
                    en.Nombre = dr["nombre"].ToString();
                    en.Descripcion = dr["descripcion"].ToString();
                    en.Tipo = dr["tipo"].ToString();
                    en.EdadMinima = (int)dr["edadminima"];
                    en.IconUrl = dr["icon_url"].ToString();
                    en.CaratulaUrl = dr["cara_url"].ToString();
                    ok = true;
                }
                dr.Close();
            }
            catch (Exception) { ok = false; }
            finally { c.Close(); }
            return ok;
        }

        public bool Update(ENVideojuego en)
        {
            bool ok = false;
            SqlConnection c = new SqlConnection(constring);
            try
            {
                c.Open();
                string query = "UPDATE Videojuego SET nombre=@nom, descripcion=@desc, tipo=@tipo, edadminima=@em, icon_url=@iurl, cara_url=@curl WHERE codigo=@cod";
                SqlCommand com = new SqlCommand(query, c);
                com.Parameters.AddWithValue("@cod", en.Codigo);
                com.Parameters.AddWithValue("@nom", en.Nombre);
                com.Parameters.AddWithValue("@desc", en.Descripcion);
                com.Parameters.AddWithValue("@tipo", en.Tipo);
                com.Parameters.AddWithValue("@em", en.EdadMinima);
                com.Parameters.AddWithValue("@iurl", en.IconUrl);
                com.Parameters.AddWithValue("@curl", en.CaratulaUrl);
                if (com.ExecuteNonQuery() > 0) ok = true;
            }
            catch (Exception ex) { throw new Exception("Error al hacer update videojuego: " + ex.Message.ToString()); }
            finally { c.Close(); }
            return ok;
        }

        public bool Delete(ENVideojuego en)
        {
            bool ok = false;
            SqlConnection c = new SqlConnection(constring);
            try
            {
                c.Open();
                string query = "DELETE FROM Videojuego WHERE codigo = @cod";
                SqlCommand com = new SqlCommand(query, c);
                com.Parameters.AddWithValue("@cod", en.Codigo);
                if (com.ExecuteNonQuery() > 0) ok = true;
            }
            catch (Exception) { ok = false; }
            finally { c.Close(); }
            return ok;
        }
        public List<ENVideojuego> ReadAll()
        {
            List<ENVideojuego> lista = new List<ENVideojuego>();

            SqlConnection c = new SqlConnection(constring);
            try
            {
                c.Open();
                string query = "SELECT * FROM Videojuego";
                SqlCommand com = new SqlCommand(query, c);
                SqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    ENVideojuego en = new ENVideojuego();
                    en.Codigo = (int)dr["codigo"];
                    en.Nombre = dr["nombre"].ToString();
                    en.Descripcion = dr["descripcion"].ToString();
                    en.Tipo = dr["tipo"].ToString();
                    en.EdadMinima = (int)dr["edadminima"];
                    en.IconUrl = dr["icon_url"].ToString();
                    en.CaratulaUrl = dr["cara_url"].ToString();
                    lista.Add(en);
                }
                dr.Close();
            }
            catch (Exception) { }
            finally { c.Close(); }

            return lista;
        }

        public List<ENVideojuego> ReadAllFiltered(ENVideojuego filter, int ed_max)
        {
            List<ENVideojuego> lista = new List<ENVideojuego>();

            SqlConnection c = new SqlConnection(constring);
            try
            {
                c.Open();
                string query = "SELECT * FROM Videojuego WHERE nombre LIKE '%"
                    + filter.Nombre
                    + "%' AND tipo LIKE '"
                    + filter.Tipo
                    + "' AND "
                    + filter.EdadMinima
                    + " <= edadminima AND edadminima <= " + ed_max.ToString();
                SqlCommand com = new SqlCommand(query, c);
                SqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    ENVideojuego en = new ENVideojuego();
                    en.Codigo = (int)dr["codigo"];
                    en.Nombre = dr["nombre"].ToString();
                    en.Descripcion = dr["descripcion"].ToString();
                    en.Tipo = dr["tipo"].ToString();
                    en.EdadMinima = (int)dr["edadminima"];
                    en.IconUrl = dr["icon_url"].ToString();
                    en.CaratulaUrl = dr["cara_url"].ToString();
                    lista.Add(en);
                }
                dr.Close();
            }
            catch (Exception) { }
            finally { c.Close(); }

            return lista;
        }
    }
}
