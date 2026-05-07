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
    public class CADJugador
    {
        private string s;

        public CADJugador()
        {
            s = ConfigurationManager.ConnectionStrings["HadaEsports"].ToString();
        }

        public bool Create(ENJugador en)
        {
            bool ok = false;
            SqlConnection c = new SqlConnection(s);
            try
            {
                c.Open();
                string query = "INSERT INTO Jugador (email_usuario, apodo, winrate, nivel, hardware, buscando_equipo, equipo_actual, juego, rol, KDA) " +
                               "VALUES (@email, @apodo, @win, @niv, @hard, @buse, @equip, @juego, @rol, @kda)";
                SqlCommand com = new SqlCommand(query, c);
                com.Parameters.AddWithValue("@email", en.Email_usuario);
                com.Parameters.AddWithValue("@apodo", en.Apodo);
                com.Parameters.AddWithValue("@win", en.Winrate);
                com.Parameters.AddWithValue("@niv", en.Nivel);
                com.Parameters.AddWithValue("@hard", string.IsNullOrEmpty(en.Hardware) ? (object)DBNull.Value : en.Hardware);
                com.Parameters.AddWithValue("@buse", en.Buscando_equipo);
                com.Parameters.AddWithValue("@equip", en.Equipo_actual == 0 ? (object)DBNull.Value : en.Equipo_actual);
                com.Parameters.AddWithValue("@juego", en.Juego == 0 ? (object)DBNull.Value : en.Juego);
                com.Parameters.AddWithValue("@rol", string.IsNullOrEmpty(en.Rol_principal) ? (object)DBNull.Value : en.Rol_principal);
                com.Parameters.AddWithValue("@kda", en.Kda_promedio);
                if (com.ExecuteNonQuery() > 0) ok = true;
            }
            catch (Exception) { ok = false; }
            finally { c.Close(); }
            return ok;
        }

        public bool Read(ENJugador en)
        {
            bool ok = false;
            SqlConnection c = new SqlConnection(s);
            try
            {
                c.Open();
                string query = "SELECT * FROM Jugador WHERE codigo = @cod";
                SqlCommand com = new SqlCommand(query, c);
                com.Parameters.AddWithValue("@cod", en.Codigo);
                SqlDataReader dr = com.ExecuteReader();
                if (dr.Read())
                {
                    en.Codigo = (int)dr["codigo"];
                    en.Email_usuario = dr["email_usuario"].ToString();
                    en.Apodo = dr["apodo"].ToString();
                    en.Winrate = dr["winrate"] == DBNull.Value ? 0f : Convert.ToSingle(dr["winrate"]);
                    en.Nivel = dr["nivel"] == DBNull.Value ? 1 : (int)dr["nivel"];
                    en.Hardware = dr["hardware"] == DBNull.Value ? "" : dr["hardware"].ToString();
                    en.Buscando_equipo = dr["buscando_equipo"] == DBNull.Value ? false : (bool)dr["buscando_equipo"];
                    en.Equipo_actual = dr["equipo_actual"] == DBNull.Value ? 0 : (int)dr["equipo_actual"];
                    en.Juego = dr["juego"] == DBNull.Value ? 0 : (int)dr["juego"];
                    en.Rol_principal = dr["rol"] == DBNull.Value ? "" : dr["rol"].ToString();
                    en.Kda_promedio = dr["KDA"] == DBNull.Value ? 0f : Convert.ToSingle(dr["KDA"]);
                    ok = true;
                }
                dr.Close();
            }
            catch (Exception) { ok = false; }
            finally { c.Close(); }
            return ok;
        }

        public bool Update(ENJugador en)
        {
            bool ok = false;
            SqlConnection c = new SqlConnection(s);
            try
            {
                c.Open();
                string query = @"UPDATE Jugador SET email_usuario=@email, apodo=@apodo, winrate=@win, nivel=@niv, hardware=@hard, buscando_equipo=@buse, equipo_actual=@equip, juego=@juego, rol=@rol, KDA=@kda WHERE codigo=@cod";
                SqlCommand com = new SqlCommand(query, c);
                com.Parameters.AddWithValue("@cod", en.Codigo);
                com.Parameters.AddWithValue("@email", en.Email_usuario);
                com.Parameters.AddWithValue("@apodo", en.Apodo);
                com.Parameters.AddWithValue("@win", en.Winrate);
                com.Parameters.AddWithValue("@niv", en.Nivel);
                com.Parameters.AddWithValue("@hard", string.IsNullOrEmpty(en.Hardware) ? (object)DBNull.Value : en.Hardware);
                com.Parameters.AddWithValue("@buse", en.Buscando_equipo);
                com.Parameters.AddWithValue("@equip", en.Equipo_actual == 0 ? (object)DBNull.Value : en.Equipo_actual);
                com.Parameters.AddWithValue("@juego", en.Juego == 0 ? (object)DBNull.Value : en.Juego);
                com.Parameters.AddWithValue("@rol", string.IsNullOrEmpty(en.Rol_principal) ? (object)DBNull.Value : en.Rol_principal);
                com.Parameters.AddWithValue("@kda", en.Kda_promedio);
                if (com.ExecuteNonQuery() > 0) ok = true;
            }
            catch (Exception) { ok = false; }
            finally { c.Close(); }
            return ok;
        }

        public bool Delete(ENJugador en)
        {
            bool ok = false;
            SqlConnection c = new SqlConnection(s);
            try
            {
                c.Open();
                string query = "DELETE FROM Jugador WHERE codigo = @cod";
                SqlCommand com = new SqlCommand(query, c);
                com.Parameters.AddWithValue("@cod", en.Codigo);
                if (com.ExecuteNonQuery() > 0) ok = true;
            }
            catch (Exception) { ok = false; }
            finally { c.Close(); }
            return ok;
        }

        public List<ENJugador> ReadAll()
        {
            List<ENJugador> lista = new List<ENJugador>();
            SqlConnection c = new SqlConnection(s);
            try
            {
                c.Open();
                string query = "SELECT * FROM Jugador";
                SqlCommand com = new SqlCommand(query, c);
                SqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    ENJugador en = new ENJugador();
                    en.Codigo = (int)dr["codigo"];
                    en.Email_usuario = dr["email_usuario"].ToString();
                    en.Apodo = dr["apodo"].ToString();
                    en.Winrate = dr["winrate"] == DBNull.Value ? 0f : Convert.ToSingle(dr["winrate"]);
                    en.Nivel = dr["nivel"] == DBNull.Value ? 1 : (int)dr["nivel"];
                    en.Hardware = dr["hardware"] == DBNull.Value ? "" : dr["hardware"].ToString();
                    en.Buscando_equipo = dr["buscando_equipo"] == DBNull.Value ? false : (bool)dr["buscando_equipo"];
                    en.Equipo_actual = dr["equipo_actual"] == DBNull.Value ? 0 : (int)dr["equipo_actual"];
                    en.Juego = dr["juego"] == DBNull.Value ? 0 : (int)dr["juego"];
                    en.Rol_principal = dr["rol"] == DBNull.Value ? "" : dr["rol"].ToString();
                    en.Kda_promedio = dr["KDA"] == DBNull.Value ? 0f : Convert.ToSingle(dr["KDA"]);
                    lista.Add(en);
                }
                dr.Close();
            }
            catch (Exception ex) { throw new Exception("Error en ReadAll: " + ex.Message); }
            finally { c.Close(); }
            return lista;
        }

        // para saber que jugadores están asociados con un usuario
        public List<ENJugador> ReadByEmail(string email)
        {
            List<ENJugador> lista = new List<ENJugador>();
            SqlConnection c = new SqlConnection(s);
            try
            {
                c.Open();
                string query = "SELECT * FROM Jugador WHERE email_usuario = @email";
                SqlCommand com = new SqlCommand(query, c);
                com.Parameters.AddWithValue("@email", email);
                SqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    ENJugador en = new ENJugador();
                    en.Codigo = (int)dr["codigo"];
                    en.Email_usuario = dr["email_usuario"].ToString();
                    en.Apodo = dr["apodo"].ToString();
                    en.Equipo_actual = dr["equipo_actual"] == DBNull.Value ? 0 : (int)dr["equipo_actual"];
                    en.Rol = dr["rol"].ToString();
                    en.Kda = dr["kda"] == DBNull.Value ? 0 : float.Parse(dr["kda"].ToString());
                    en.Nivel = dr["nivel"] == DBNull.Value ? 0 : (int)dr["nivel"];
                    en.Winrate = dr["winrate"] == DBNull.Value ? 0 : float.Parse(dr["winrate"].ToString());
                    en.Buscando_equipo = (bool)dr["buscando_equipo"];
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