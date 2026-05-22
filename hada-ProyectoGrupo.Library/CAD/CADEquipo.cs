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
            s = ConfigurationManager.ConnectionStrings["HadaEsports"].ToString();
        }

        public bool Create(ENEquipo en)
        {
            bool ok = false;
            SqlConnection c = new SqlConnection(s);
            try
            {
                c.Open();
                string query = "INSERT INTO Equipo (nombre, fecha_creacion, logo_url, descripcion, id_capitan, max_jugadores) VALUES (@nom, @fech, @log, @des, @id_c, @max)";
                SqlCommand com = new SqlCommand(query, c);
                com.Parameters.AddWithValue("@nom", en.Nombre);
                com.Parameters.AddWithValue("@fech", en.Fecha_creacion);
                com.Parameters.AddWithValue("@log", en.Logo_url);
                com.Parameters.AddWithValue("@des", en.Descripcion);
                com.Parameters.AddWithValue("@id_c", en.Id_capitan);
                com.Parameters.AddWithValue("@max", en.Max_jugadores);
                if (com.ExecuteNonQuery() > 0) ok = true;
            }
            catch (Exception ex) { throw new Exception("Error al crear equipo: " + ex.Message); }
            finally { c.Close(); }
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
                    en.Max_jugadores = (int)dr["max_jugadores"];
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
                string query = "UPDATE Equipo SET nombre=@nom, fecha_creacion=@fech, logo_url=@log, descripcion=@des, id_capitan=@id_c, max_jugadores=@max WHERE id_equipo=@id_e";
                SqlCommand com = new SqlCommand(query, c);
                com.Parameters.AddWithValue("@id_e", en.Id_equipo);
                com.Parameters.AddWithValue("@nom", en.Nombre);
                com.Parameters.AddWithValue("@fech", en.Fecha_creacion);
                com.Parameters.AddWithValue("@log", en.Logo_url);
                com.Parameters.AddWithValue("@des", en.Descripcion);
                com.Parameters.AddWithValue("@id_c", en.Id_capitan);
                com.Parameters.AddWithValue("@max", en.Max_jugadores);
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
                string queryCapitan = "UPDATE Equipo SET id_capitan = NULL WHERE id_equipo = @id_e";
                SqlCommand comCapitan = new SqlCommand(queryCapitan, c);
                comCapitan.Parameters.AddWithValue("@id_e", en.Id_equipo);
                comCapitan.ExecuteNonQuery();

                //Desvincula los jugadores
                string queryJugadores = "UPDATE Jugador SET equipo_actual = NULL, buscando_equipo = 1 WHERE equipo_actual = @id_e";
                SqlCommand comJugadores = new SqlCommand(queryJugadores, c);
                comJugadores.Parameters.AddWithValue("@id_e", en.Id_equipo);
                comJugadores.ExecuteNonQuery();
                string query = "DELETE FROM Equipo WHERE id_equipo = @id_e";
                SqlCommand com = new SqlCommand(query, c);
                com.Parameters.AddWithValue("@id_e", en.Id_equipo);
                if (com.ExecuteNonQuery() > 0) ok = true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error al eliminar equipo: " + ex.Message);
                ok = false;
            }
            finally { c.Close(); }
            return ok;
        }

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
                    en.Max_jugadores = (int)dr["max_jugadores"];
                    lista.Add(en);
                }
                dr.Close();
            }
            catch (Exception) { }
            finally { c.Close(); }
            return lista;
        }

        public List<EquipoConMiembros> ReadAllConMiembros()
        {
            List<EquipoConMiembros> lista = new List<EquipoConMiembros>();
            SqlConnection c = new SqlConnection(s);
            try
            {
                c.Open();
                string query = @"
                    SELECT E.id_equipo,
                           E.nombre,
                           E.logo_url,
                           E.descripcion,
                           E.id_capitan,
                           E.max_jugadores,
                           COUNT(J.codigo) AS miembros_actuales
                    FROM Equipo E
                    LEFT JOIN Jugador J ON J.equipo_actual = E.id_equipo
                    GROUP BY E.id_equipo, E.nombre, E.logo_url,
                             E.descripcion, E.id_capitan, E.max_jugadores";

                SqlCommand com = new SqlCommand(query, c);
                SqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    EquipoConMiembros em = new EquipoConMiembros();
                    em.Id_equipo = (int)dr["id_equipo"];
                    em.Nombre = dr["nombre"].ToString();
                    em.Logo_url = dr["logo_url"] == DBNull.Value ? "" : dr["logo_url"].ToString();
                    em.Descripcion = dr["descripcion"] == DBNull.Value ? "" : dr["descripcion"].ToString();
                    em.Id_capitan = dr["id_capitan"] == DBNull.Value ? 0 : (int)dr["id_capitan"];
                    em.Max_jugadores = (int)dr["max_jugadores"];
                    em.MiembrosActuales = (int)dr["miembros_actuales"];
                    lista.Add(em);
                }
                dr.Close();
            }
            catch (Exception) { }
            finally { c.Close(); }
            return lista;
        }

        public ENEquipo ReadByCapitan(int idCapitan)
        {
            ENEquipo en = null;
            SqlConnection c = new SqlConnection(s);
            try
            {
                c.Open();
                string query = "SELECT * FROM Equipo WHERE id_capitan = @id";
                SqlCommand com = new SqlCommand(query, c);
                com.Parameters.AddWithValue("@id", idCapitan);
                SqlDataReader dr = com.ExecuteReader();
                if (dr.Read())
                {
                    en = new ENEquipo();
                    en.Id_equipo = (int)dr["id_equipo"];
                    en.Nombre = dr["nombre"].ToString();
                    en.Id_capitan = (int)dr["id_capitan"];
                }
                dr.Close();
            }
            catch (Exception) { }
            finally { c.Close(); }
            return en;
        }
    }
    public class EquipoConMiembros
    {
        public int Id_equipo { get; set; }
        public string Nombre { get; set; }
        public string Logo_url { get; set; }
        public string Descripcion { get; set; }
        public int Id_capitan { get; set; }
        public int Max_jugadores { get; set; }
        public int MiembrosActuales { get; set; }
    }
}