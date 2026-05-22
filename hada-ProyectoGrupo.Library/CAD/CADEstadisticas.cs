using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace hada_ProyectoGrupo.Library.CAD
{
    public class CADEstadisticas
    {
        private string constring;

        public CADEstadisticas()
        {
           
            constring = ConfigurationManager.ConnectionStrings["HadaEsports"].ConnectionString;
        }

        // 1. Total de usuarios registrados
        public int ObtenerTotalUsuarios()
        {
            int total = 0;
            using (SqlConnection c = new SqlConnection(constring))
            {
                string query = "SELECT COUNT(*) FROM Usuario";
                SqlCommand cmd = new SqlCommand(query, c);
                c.Open();
                total = (int)cmd.ExecuteScalar();
            }
            return total;
        }

        // 2. Videojuegos con más torneos
        public Dictionary<string, int> TorneosPorJuego()
        {
            Dictionary<string, int> stats = new Dictionary<string, int>();
            using (SqlConnection c = new SqlConnection(constring))
            {
                string query = @"SELECT v.nombre, COUNT(t.codigo) as Total 
                                 FROM VideoJuego v
                                 LEFT JOIN Torneo t ON v.codigo = t.id_videojuego
                                 GROUP BY v.nombre 
                                 ORDER BY Total DESC";
                SqlCommand cmd = new SqlCommand(query, c);
                c.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    stats.Add(dr["nombre"].ToString(), Convert.ToInt32(dr["Total"]));
                }
            }
            return stats;
        }

        // 3. Cuántos jugadores hay en cada juego
        public Dictionary<string, int> JugadoresPorJuego()
        {
            Dictionary<string, int> stats = new Dictionary<string, int>();
            using (SqlConnection c = new SqlConnection(constring))
            {
                string query = @"SELECT v.nombre, COUNT(j.codigo) as Total 
                                 FROM VideoJuego v
                                 LEFT JOIN Jugador j ON v.codigo = j.juego
                                 GROUP BY v.nombre";
                SqlCommand cmd = new SqlCommand(query, c);
                c.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    stats.Add(dr["nombre"].ToString(), Convert.ToInt32(dr["Total"]));
                }
            }
            return stats;
        }

        

        // 4. Patrocinadores más activos 
        public Dictionary<string, int> PatrocinadoresMasActivos()
        {
            Dictionary<string, int> stats = new Dictionary<string, int>();
            using (SqlConnection c = new SqlConnection(constring))
            {
                string query = @"SELECT p.nombre, COUNT(pa.Codigo) as NumTorneos
                                 FROM Patrocinador p
                                 JOIN Patrocinio pa ON p.IdPatrocinador = pa.IdPatrocinador
                                 GROUP BY p.IdPatrocinador, p.nombre
                                 ORDER BY NumTorneos DESC";
                SqlCommand cmd = new SqlCommand(query, c);
                c.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    stats.Add(dr["nombre"].ToString(), Convert.ToInt32(dr["NumTorneos"]));
                }
            }
            return stats;
        }
    }
}