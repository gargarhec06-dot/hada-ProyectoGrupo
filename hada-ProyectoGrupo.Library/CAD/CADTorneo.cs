using hada_ProyectoGrupo.Library.EN;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace hada_ProyectoGrupo.Library.CAD
{
    public class CADTorneo
    {
        private string s;

        public CADTorneo()
        {
            s = ConfigurationManager.ConnectionStrings["HadaEsports"].ToString();
        }

        public bool Create(ENTorneo en)
        {
            bool ok = false;
            SqlConnection c = new SqlConnection(s);
            try
            {
                c.Open();
                string query = "INSERT INTO Torneo (id_videojuego, precioInscripcion, nombre, descripcion, profesional, costeOrganizacion, fecha, ubicacion) VALUES (@vid, @pre, @nom, @des, @pro, @cos, @fec, @ubi)";
                SqlCommand com = new SqlCommand(query, c);
                com.Parameters.AddWithValue("@vid", en.IdVideojuego);
                com.Parameters.AddWithValue("@pre", en.PrecioInscripcion);
                com.Parameters.AddWithValue("@nom", en.Nombre);
                com.Parameters.AddWithValue("@des", en.Descripcion);
                com.Parameters.AddWithValue("@pro", en.Profesional);
                com.Parameters.AddWithValue("@cos", en.CosteOrganizacion);
                com.Parameters.AddWithValue("@fec", en.Fecha);
                com.Parameters.AddWithValue("@ubi", en.Ubicacion);
                if (com.ExecuteNonQuery() > 0) ok = true;
            }
            catch (Exception ex) 
            {
                ok = false;
                System.Diagnostics.Debug.WriteLine("ERROR CREATE TORNEO: " + ex.Message);
            }
            finally { c.Close(); }
            return ok;
        }

        public bool Read(ENTorneo en)
        {
            bool ok = false;
            SqlConnection c = new SqlConnection(s);
            try
            {
                c.Open();
                string query = "SELECT * FROM Torneo WHERE codigo = @cod";
                SqlCommand com = new SqlCommand(query, c);
                com.Parameters.AddWithValue("@cod", en.Codigo);
                SqlDataReader dr = com.ExecuteReader();
                if (dr.Read())
                {
                    en.IdVideojuego = (int)dr["id_videojuego"];
                    en.PrecioInscripcion = float.Parse(dr["precioInscripcion"].ToString());
                    en.Nombre = dr["nombre"].ToString();
                    en.Descripcion = dr["descripcion"].ToString();
                    en.Profesional = (bool)dr["profesional"];
                    en.CosteOrganizacion = float.Parse(dr["costeOrganizacion"].ToString());
                    en.Fecha = (DateTime)dr["fecha"];
                    en.Ubicacion = dr["ubicacion"].ToString();
                    ok = true;
                }
                dr.Close();
            }
            catch (Exception) { ok = false; }
            finally { c.Close(); }
            return ok;
        }

        public bool Update(ENTorneo en)
        {
            bool ok = false;
            SqlConnection c = new SqlConnection(s);
            try
            {
                c.Open();
                string query = "UPDATE Torneo SET id_videojuego=@vid, precioInscripcion=@pre, nombre=@nom, descripcion=@des, profesional=@pro, costeOrganizacion=@cos, fecha=@fec, ubicacion=@ubi WHERE codigo=@cod";
                SqlCommand com = new SqlCommand(query, c);
                com.Parameters.AddWithValue("@cod", en.Codigo);
                com.Parameters.AddWithValue("@vid", en.IdVideojuego);
                com.Parameters.AddWithValue("@pre", en.PrecioInscripcion);
                com.Parameters.AddWithValue("@nom", en.Nombre);
                com.Parameters.AddWithValue("@des", en.Descripcion);
                com.Parameters.AddWithValue("@pro", en.Profesional);
                com.Parameters.AddWithValue("@cos", en.CosteOrganizacion);
                com.Parameters.AddWithValue("@fec", en.Fecha);
                com.Parameters.AddWithValue("@ubi", en.Ubicacion);
                if (com.ExecuteNonQuery() > 0) ok = true;
            }
            catch (Exception) { ok = false; }
            finally { c.Close(); }
            return ok;
        }

        public bool Delete(ENTorneo en)
        {
            bool ok = false;
            SqlConnection c = new SqlConnection(s);
            try
            {
                c.Open();
                string query = "DELETE FROM Torneo WHERE codigo = @cod";
                SqlCommand com = new SqlCommand(query, c);
                com.Parameters.AddWithValue("@cod", en.Codigo);
                if (com.ExecuteNonQuery() > 0) ok = true;
            }
            catch (Exception) { ok = false; }
            finally { c.Close(); }
            return ok;
        }

        public List<ENTorneo> ReadAll()
        {
            List<ENTorneo> lista = new List<ENTorneo>();
            SqlConnection c = new SqlConnection(s);
            try
            {
                c.Open();
                string query = "SELECT * FROM Torneo";
                SqlCommand com = new SqlCommand(query, c);
                SqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    ENTorneo en = new ENTorneo();
                    en.Codigo = (int)dr["codigo"];
                    en.IdVideojuego = (int)dr["id_videojuego"];
                    en.PrecioInscripcion = float.Parse(dr["precioInscripcion"].ToString());
                    en.Nombre = dr["nombre"].ToString();
                    en.Descripcion = dr["descripcion"].ToString();
                    en.Profesional = (bool)dr["profesional"];
                    en.CosteOrganizacion = float.Parse(dr["costeOrganizacion"].ToString());
                    en.Fecha = (DateTime)dr["fecha"];
                    en.Ubicacion = dr["ubicacion"].ToString();
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