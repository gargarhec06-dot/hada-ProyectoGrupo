using hada_ProyectoGrupo.Library.EN;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hada_ProyectoGrupo.Library.CAD
{
    public class CADPartida
    {
        private string s;
        public CADPartida ()
        {
            s = ConfigurationManager.ConnectionStrings["HadaEsports"].ToString();
        }

        public bool Create(ENPartida en)
        {
            bool ok = true;
            SqlConnection c = new SqlConnection(s);
            try
            {
                c.Open();
                string query = @"INSERT INTO Partida (torneo, fecha, videojuego, enlace_repeticion, equipo_ganador)
                                VALUES (@tor, @fec, @vid, @er, @eg)";
                SqlCommand com = new SqlCommand(query, c);
                //com.Parameters.AddWithValue("@cod", en.Code);
                com.Parameters.AddWithValue("@tor", en.Torneo);
                com.Parameters.AddWithValue("@fec", en.Fecha);
                com.Parameters.AddWithValue("@vid", en.Videojuego);
                com.Parameters.AddWithValue("@er", en.EnlaceDeRepeticion);
                com.Parameters.AddWithValue("@eg", en.Ganador);

                if (com.ExecuteNonQuery() > 0) ok = true;

                List<ENPartida> list = new ENPartida().ReadByTorneo(en.Torneo);
                en.Code = list.Last().Code;

                int iter = 2;
                foreach (int item in en.Perdedores)
                {
                    string subquery = @"INSERT INTO Partida_EquiposPerdedores (id_partida, id_equipo, posicion)
                                VALUES (@cod, @eq, @pos)";
                    SqlCommand subcom = new SqlCommand(subquery, c);
                    subcom.Parameters.AddWithValue("@cod", en.Code);
                    subcom.Parameters.AddWithValue("@eq", item);
                    subcom.Parameters.AddWithValue("@pos", iter);

                    if (subcom.ExecuteNonQuery() == 0) ok = false;

                    iter++;
                }

                foreach (int item in en.Jugadores)
                {
                    string subquery = @"INSERT INTO Partida_Jugadores (id_partida, id_jugador)
                                VALUES (@cod, @ju)";
                    SqlCommand subcom = new SqlCommand(subquery, c);
                    subcom.Parameters.AddWithValue("@cod", en.Code);
                    subcom.Parameters.AddWithValue("@ju", item);

                    if (subcom.ExecuteNonQuery() == 0) ok = false;

                    iter++;
                }
            }
            catch (Exception) { ok = false; }
            finally { c.Close(); }
            return ok;
        }

        public bool Read(ENPartida en)
        {
            bool ok = false;

            SqlConnection c = new SqlConnection(s);
            try
            {
                c.Open();
                string query = @"SELECT *
                         FROM Partida p
                         WHERE p.codigo = @cod";
                SqlCommand com = new SqlCommand(query, c);
                com.Parameters.AddWithValue("@cod", en.Code);
                SqlDataReader dr = com.ExecuteReader();
                if (dr.Read())
                {
                    en.Code = (int)dr["codigo"];
                    en.Torneo = (int)dr["torneo"];
                    en.Fecha = (DateTime)dr["fecha"];
                    en.Videojuego = (int)dr["videojuego"];
                    en.EnlaceDeRepeticion = dr["enlace_repeticion"].ToString();
                    en.Ganador = (int)dr["equipo_ganador"];

                    dr.Close();

                    string subquery_jugadores = @"SELECT *
                         FROM Partida_Jugadores p
                         WHERE p.id_partida = @cod";
                    SqlCommand com_jugador = new SqlCommand(subquery_jugadores, c);
                    com_jugador.Parameters.AddWithValue("@cod", en.Code);
                    SqlDataReader dr_jugador = com_jugador.ExecuteReader();
                    List<int> jugador = new List<int>();
                    while (dr_jugador.Read())
                    {
                        jugador.Add((int)dr_jugador["id_jugador"]);
                    }
                    dr_jugador.Close();
                    en.Jugadores = jugador.ToArray();

                    string subquery_perdedores = @"SELECT *
                         FROM Partida_EquiposPerdedores p
                         WHERE p.id_partida = @cod
                         ORDER BY p.posicion ASC";
                    SqlCommand com_perdedores = new SqlCommand(subquery_perdedores, c);
                    com_perdedores.Parameters.AddWithValue("@cod", en.Code);
                    SqlDataReader dr_perdedores = com_perdedores.ExecuteReader();
                    List<int> perdedores = new List<int>();
                    while (dr_perdedores.Read())
                    {
                        perdedores.Add((int)dr_perdedores["id_equipo"]);
                    }
                    dr_perdedores.Close();
                    en.Perdedores = perdedores.ToArray();

                    ok = true;
                }
                dr.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); ok = false; }

            finally { c.Close(); }

            return ok;
        }

        public bool Update(ENPartida en)
        {
            bool ok = true;
            SqlConnection c = new SqlConnection(s);
            //try
            {
                c.Open();

                string query = @"UPDATE Partida
                                SET torneo=@tor, fecha=@fec, videojuego=@vid, enlace_repeticion=@er, equipo_ganador=@eg
                                WHERE codigo=@cod";
                SqlCommand com = new SqlCommand(query, c);
                com.Parameters.AddWithValue("@cod", en.Code);
                com.Parameters.AddWithValue("@tor", en.Torneo);
                com.Parameters.AddWithValue("@fec", en.Fecha);
                com.Parameters.AddWithValue("@vid", en.Videojuego);
                com.Parameters.AddWithValue("@er", en.EnlaceDeRepeticion);
                com.Parameters.AddWithValue("@eg", en.Ganador);

                if (com.ExecuteNonQuery() > 0) { ok = true; }
                else { return false; }

                {
                    string subquery = @"DELETE FROM Partida_EquiposPerdedores WHERE id_partida = @cod";
                    SqlCommand subcom = new SqlCommand(subquery, c);
                    subcom.Parameters.AddWithValue("@cod", en.Code);

                    subcom.ExecuteNonQuery();
                }
                {
                    string subquery = @"DELETE FROM Partida_Jugadores WHERE id_partida = @cod";
                    SqlCommand subcom = new SqlCommand(subquery, c);
                    subcom.Parameters.AddWithValue("@cod", en.Code);

                    subcom.ExecuteNonQuery();
                }
                int iter = 2;
                foreach (int item in en.Perdedores)
                {
                    string subquery = @"INSERT INTO Partida_EquiposPerdedores (id_partida, id_equipo, posicion)
                                VALUES (@cod, @eq, @pos)";
                    SqlCommand subcom = new SqlCommand(subquery, c);
                    subcom.Parameters.AddWithValue("@cod", en.Code);
                    subcom.Parameters.AddWithValue("@eq", item);
                    subcom.Parameters.AddWithValue("@pos", iter);

                    if (subcom.ExecuteNonQuery() == 0) ok = false;

                    iter++;
                }

                foreach (int item in en.Jugadores)
                {
                    string subquery = @"INSERT INTO Partida_Jugadores (id_partida, id_jugador)
                                VALUES (@cod, @ju)";
                    SqlCommand subcom = new SqlCommand(subquery, c);
                    subcom.Parameters.AddWithValue("@cod", en.Code);
                    subcom.Parameters.AddWithValue("@ju", item);

                    if (subcom.ExecuteNonQuery() == 0) ok = false;

                    iter++;
                }
            }
            //catch (Exception) { ok = false; }
            //finally { c.Close(); }

            return ok;
        }

        public bool Delete(ENPartida en)
        {
            bool ok = false;
            SqlConnection c = new SqlConnection(s);
            try
            {
                c.Open();

                {
                    string subquery = @"DELETE FROM Partida_EquiposPerdedores WHERE id_partida = @cod";
                    SqlCommand subcom = new SqlCommand(subquery, c);
                    subcom.Parameters.AddWithValue("@cod", en.Code);

                    subcom.ExecuteNonQuery();
                }
                {
                    string subquery = @"DELETE FROM Partida_Jugadores WHERE id_partida = @cod";
                    SqlCommand subcom = new SqlCommand(subquery, c);
                    subcom.Parameters.AddWithValue("@cod", en.Code);

                    subcom.ExecuteNonQuery();
                }

                string query = @"DELETE FROM Partida WHERE codigo = @cod";
                SqlCommand com = new SqlCommand(query, c);
                com.Parameters.AddWithValue("@cod", en.Code);
                if (com.ExecuteNonQuery() > 0) ok = true;

            } catch(Exception) { ok = false; }
            finally { c.Close(); }
            return ok;
        }

        /* No necesaria
        /// <summary>
        /// Devuelve todas las partidas salvo que ocurra un error
        /// Disponibilidad por si tenemos que filtrarlas por algún criterio
        /// </summary>
        /// <returns>Lista con todas las partidas</returns>
        public List<ENPartida> ReadAll()
        {
            List<ENPartida> partidas = new List<ENPartida>();

            return partidas;
        }*/

        public List<ENPartida> ReadByTorneo(int torneo)
        {
            List<ENPartida> lista = new List<ENPartida>();
            SqlConnection c = new SqlConnection(s);
            try
            {
                c.Open();
                string query = @"SELECT *
                         FROM Partida p
                         WHERE p.torneo = @tor";
                SqlCommand com = new SqlCommand(query, c);
                com.Parameters.AddWithValue("@tor", torneo);
                SqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    ENPartida en = new ENPartida();
                    en.Code = (int)dr["codigo"];
                    en.Read();
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
