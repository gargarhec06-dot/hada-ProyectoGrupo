using hada_ProyectoGrupo.Library.EN;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace hada_ProyectoGrupo.Library.CAD
{
    public class CADPremio
    {
        private string s;

        public CADPremio()
        {
            s = ConfigurationManager.ConnectionStrings["HadaEsports"].ToString();
        }

        public bool Create(ENPremio en)
        {
            bool ok = false;
            SqlConnection c = new SqlConnection(s);
            try
            {
                c.Open();
                string query = "INSERT INTO Premio (codigo_premio, torneo, nombre, posicion, descripcion, moneda, monetario, no_monetario) " +
                               "VALUES (@cod, @tor, @nom, @pos, @des, @mon, @val, @nmo)";
                SqlCommand com = new SqlCommand(query, c);
                com.Parameters.AddWithValue("@cod", en.Codigo);
                com.Parameters.AddWithValue("@tor", en.Torneo);
                com.Parameters.AddWithValue("@nom", en.Nombre);
                com.Parameters.AddWithValue("@pos", en.Posicion);
                com.Parameters.AddWithValue("@des", en.Descripcion);
                com.Parameters.AddWithValue("@mon", en.Moneda);
                com.Parameters.AddWithValue("@val", en.Monetario);
                com.Parameters.AddWithValue("@nmo", en.NoMonetario);

                if (com.ExecuteNonQuery() > 0) ok = true;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { c.Close(); }
            return ok;
        }

        public bool Read(ENPremio en)
        {
            bool ok = false;
            SqlConnection c = new SqlConnection(s);
            try
            {
                c.Open();
                string query = "SELECT * FROM Premio WHERE codigo_premio = @cod";
                SqlCommand com = new SqlCommand(query, c);
                com.Parameters.AddWithValue("@cod", en.Codigo);

                SqlDataReader dr = com.ExecuteReader();
                if (dr.Read())
                {
                    en.Torneo = (int)dr["torneo"];
                    en.Nombre = dr["nombre"].ToString();
                    en.Posicion = (int)dr["posicion"];
                    en.Descripcion = dr["descripcion"].ToString();
                    en.Moneda = dr["moneda"].ToString();
                    en.Monetario = (int)dr["monetario"];
                    en.NoMonetario = dr["no_monetario"].ToString();
                    ok = true;
                }
                dr.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { c.Close(); }
            return ok;
        }

        public bool Update(ENPremio en)
        {
            bool ok = false;
            SqlConnection c = new SqlConnection(s);
            try
            {
                c.Open();
                string query = "UPDATE Premio SET torneo=@tor, nombre=@nom, posicion=@pos, descripcion=@des, moneda=@mon, monetario=@val, no_monetario=@nmo " +
                               "WHERE codigo_premio=@cod";
                SqlCommand com = new SqlCommand(query, c);
                com.Parameters.AddWithValue("@cod", en.Codigo);
                com.Parameters.AddWithValue("@tor", en.Torneo);
                com.Parameters.AddWithValue("@nom", en.Nombre);
                com.Parameters.AddWithValue("@pos", en.Posicion);
                com.Parameters.AddWithValue("@des", en.Descripcion);
                com.Parameters.AddWithValue("@mon", en.Moneda);
                com.Parameters.AddWithValue("@val", en.Monetario);
                com.Parameters.AddWithValue("@nmo", en.NoMonetario);

                if (com.ExecuteNonQuery() > 0) ok = true;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { c.Close(); }
            return ok;
        }

        public bool Delete(ENPremio en)
        {
            bool ok = false;
            SqlConnection c = new SqlConnection(s);
            try
            {
                c.Open();
                string query = "DELETE FROM Premio WHERE codigo_premio = @cod";
                SqlCommand com = new SqlCommand(query, c);
                com.Parameters.AddWithValue("@cod", en.Codigo);

                if (com.ExecuteNonQuery() > 0) ok = true;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { c.Close(); }
            return ok;
        }

        public List<ENPremio> ReadAll()
        {
            List<ENPremio> lista = new List<ENPremio>();
            SqlConnection c = new SqlConnection(s);
            try
            {
                c.Open();
                string query = "SELECT * FROM Premio";
                SqlCommand com = new SqlCommand(query, c);
                SqlDataReader dr = com.ExecuteReader();

                while (dr.Read())
                {
                    ENPremio en = new ENPremio();
                    en.Codigo = (int)dr["codigo_premio"];
                    en.Torneo = (int)dr["torneo"];
                    en.Nombre = dr["nombre"].ToString();
                    en.Posicion = (int)dr["posicion"];
                    en.Descripcion = dr["descripcion"].ToString();
                    en.Moneda = dr["moneda"].ToString();
                    en.Monetario = (int)dr["monetario"];
                    en.NoMonetario = dr["no_monetario"].ToString();
                    lista.Add(en);
                }
                dr.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { c.Close(); }
            return lista;
        }
    }
}