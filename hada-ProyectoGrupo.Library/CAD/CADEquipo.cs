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
    public class CADEquipo
    {
        private string s;
        public CADEquipo()
        {
            s= ConfigurationManager.ConnectionStrings["HadaEsports"].ToString();
        }

        public bool Create(ENEquipo en)
        {
            bool ok = false;
            SqlConnection c = new SqlConnection(s);

            try
            {
                c.Open();

                string query = @"INSERT INTO Equipo
                        (nombre, fecha_creacion, logo_url, descripcion, id_capitan)
                        VALUES
                        (@nom, @fech, @log, @des, @id_c)";

                SqlCommand com = new SqlCommand(query, c);

                com.Parameters.AddWithValue("@nom", en.Nombre);
                com.Parameters.AddWithValue("@fech", en.Fecha_creacion);
                com.Parameters.AddWithValue("@log", en.Logo_url);
                com.Parameters.AddWithValue("@des", en.Descripcion);
                com.Parameters.AddWithValue("@id_c", en.Id_capitan);

                if (com.ExecuteNonQuery() > 0)
                    ok = true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear equipo: " + ex.Message);
            }
            finally
            {
                c.Close();
            }

            return ok;
        }

        public bool Read(ENEquipo en)
        {
            bool ok = false;
            SqlConnection c = new SqlConnection(s);
            try
            {
                c.Open();
                string query = "SELECT * FROM Equipo WHERE id_equipo = @id_e";
                SqlCommand com = new SqlCommand(query, c);
                com.Parameters.AddWithValue("@id_e", en.Id_equipo);
                SqlDataReader dr = com.ExecuteReader();
                if (dr.Read())
                {
                    en.Id_equipo = (int)dr["id_equipo"];
                    en.Nombre = dr["nombre"].ToString();
                    en.Fecha_creacion = (DateTime)dr["fecha_creacion"];
                    en.Logo_url = dr["logo_url"].ToString();
                    en.Descripcion = dr["descripcion"].ToString();
                    en.Id_capitan = (int)dr["id_capitan"];
                    ok = true;
                }
                dr.Close();
            }
            catch (Exception) { ok = false; }
            finally { c.Close(); }
            return ok;
        }

        public bool Update(ENEquipo en)
        {

            bool ok = false;
            SqlConnection c = new SqlConnection(s);
            try
            {
                c.Open();
                string query = "UPDATE Equipo SET nombre=@nom , fecha_creacion=@fech , logo_url=@log , descripcion=@des ,  id_capitan=@id_c WHERE id_equipo=@id_e";
                SqlCommand com = new SqlCommand(query, c);
                com.Parameters.AddWithValue("@id_e", en.Id_equipo);
                com.Parameters.AddWithValue("@nom", en.Nombre);
                com.Parameters.AddWithValue("@fech", en.Fecha_creacion);
                com.Parameters.AddWithValue("@log", en.Logo_url);
                com.Parameters.AddWithValue("@des", en.Descripcion);
                com.Parameters.AddWithValue("@id_c", en.Id_capitan);
                if (com.ExecuteNonQuery() > 0) ok = true;
            }
            catch (Exception) { ok = false; }
            finally { c.Close(); }
            return ok;
        }

        public bool Delete(ENEquipo en)
        {
            bool ok = false;
            SqlConnection c = new SqlConnection(s);
            try
            {
                c.Open();
                string query = "DELETE FROM Equipo WHERE id_equipo = @id_e";
                SqlCommand com = new SqlCommand(query, c);
                com.Parameters.AddWithValue("@id_e", en.Id_equipo);
                if (com.ExecuteNonQuery() > 0) ok = true;
            }
            catch (Exception) { ok = false; }
            finally { c.Close(); }
            return ok;
        }

        // Nuevo método siguiendo el estilo de CADTorneo
        public List<ENEquipo> ReadAll()
        {
            List<ENEquipo> lista = new List<ENEquipo>();
            SqlConnection c = new SqlConnection(s);
            try
            {
                c.Open();
                string query = "SELECT * FROM Equipo";
                SqlCommand com = new SqlCommand(query, c);
                SqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    ENEquipo en = new ENEquipo();
                    en.Id_equipo = (int)dr["id_equipo"];
                    en.Nombre = dr["nombre"].ToString();
                    en.Fecha_creacion = (DateTime)dr["fecha_creacion"];
                    en.Logo_url = dr["logo_url"].ToString();
                    en.Descripcion = dr["descripcion"].ToString();
                    en.Id_capitan = (int)dr["id_capitan"];
                    lista.Add(en);
                }
                dr.Close();
            }
            catch (Exception) { }
            finally { c.Close(); }
            return lista;
        }
        public int GetLastId()
        {
            int lastId = 0;
            SqlConnection c = new SqlConnection(s);
            try
            {
                c.Open();
                SqlCommand com = new SqlCommand("SELECT ISNULL(MAX(id_equipo), 0) FROM Equipo", c);
                lastId = (int)com.ExecuteScalar();
            }
            catch (Exception) { }
            finally { c.Close(); }
            return lastId;
        }

        // este método lo usamos para saber si un jugador es capitán de un equipo, necesario para poder implementar la inscripción de un equipo a un
        // torneo (solo el capitán puede inscribir al equipo)
        public List<ENEquipo> ReadByCapitan(int codigoJugador)
        {
            List<ENEquipo> lista = new List<ENEquipo>();
            SqlConnection c = new SqlConnection(s);
            try
            {
                c.Open();
                string query = "SELECT * FROM Equipo WHERE id_capitan = @cap";
                SqlCommand com = new SqlCommand(query, c);
                com.Parameters.AddWithValue("@cap", codigoJugador);
                SqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    ENEquipo en = new ENEquipo();
                    en.Id_equipo = (int)dr["id_equipo"];
                    en.Nombre = dr["nombre"].ToString();
                    en.Id_capitan = (int)dr["id_capitan"];
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
