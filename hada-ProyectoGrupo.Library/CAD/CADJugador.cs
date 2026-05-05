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

        public CADJugador() {
            s = ConfigurationManager.ConnectionStrings["HadaEsports"].ToString();
        }

        //Falta implementar juego y vida

        public bool Create(ENJugador en)
        {
            bool ok = true;

            SqlConnection c = new SqlConnection(s);
            try
            {
                c.Open();
                string query = "INSERT INTO Jugador (codigo,email_usuario,apodo,winrate,nivel,hardware,buscando_equipo,equipo_actual,juego,rol,kda) VALUES (@cod,@email,@apodo,@win,@niv,@hard,@buse,@equip,@juego,@rol,@kda)";
                SqlCommand com = new SqlCommand(query, c);
                com.Parameters.AddWithValue("@cod", en.Codigo);
                com.Parameters.AddWithValue("@email", en.Email_usuario);
                com.Parameters.AddWithValue("@apodo", en.Apodo);
                com.Parameters.AddWithValue("@win", en.Winrate);
                com.Parameters.AddWithValue("@niv", en.Nivel);
                com.Parameters.AddWithValue("@hard", en.Hardware);
                com.Parameters.AddWithValue("@buse", en.Buscando_equipo);
                com.Parameters.AddWithValue("@equip", en.Equipo_actual);
                com.Parameters.AddWithValue("@juego", en.Juego);
                com.Parameters.AddWithValue("@rol", en.Rol_principal);
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
                    en.Winrate = Convert.ToSingle(dr["winrate"]);
                    en.Nivel = (int)dr["Nivel"];
                    en.Hardware = dr["hardware"].ToString();
                    en.Buscando_equipo = (bool)dr["buscando_equipo"];
                    en.Equipo_actual = (int)dr["equipo_actual"];
                    en.Juego = (int)dr["Juego"];
                    en.Rol_principal = dr["rol"].ToString();
                    en.Kda_promedio = Convert.ToSingle(dr["kda"]);
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
                string query = "UPDATE Jugador SET codigo=@cod ,email=@email , apodo=@apodo , winrate=@win , nivel=@niv , hardware=@hard , buscando_equipo=@buse , equipo_actual=@equip , juego=@juego , rol=@rol , kda=@kda WHERE codigo=@cod";
                SqlCommand com = new SqlCommand(query, c);
                com.Parameters.AddWithValue("@cod", en.Codigo);
                com.Parameters.AddWithValue("@email", en.Email_usuario);
                com.Parameters.AddWithValue("@apodo", en.Apodo);
                com.Parameters.AddWithValue("@win", en.Winrate);
                com.Parameters.AddWithValue("@niv", en.Nivel);
                com.Parameters.AddWithValue("@hard", en.Hardware);
                com.Parameters.AddWithValue("@buse", en.Buscando_equipo);
                com.Parameters.AddWithValue("@equip", en.Equipo_actual);
                com.Parameters.AddWithValue("@juego", en.Juego);
                com.Parameters.AddWithValue("@rol", en.Rol_principal);
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
                    // Leer los valores del DataReader y asignarlos al objeto
                    en.Codigo = (int)dr["codigo"];
                    en.Email_usuario = dr["email_usuario"].ToString();
                    en.Apodo = dr["apodo"].ToString();
                    en.Winrate = Convert.ToSingle(dr["winrate"]);
                    en.Nivel = (int)dr["Nivel"];
                    en.Hardware = dr["hardware"].ToString();
                    en.Buscando_equipo = (bool)dr["buscando_equipo"];
                    en.Equipo_actual = (int)dr["equipo_actual"];
                    en.Juego = (int)dr["Juego"];
                    en.Rol_principal = dr["rol"].ToString();
                    en.Kda_promedio = Convert.ToSingle(dr["kda"]);
                    lista.Add(en);
                }
                dr.Close();
            }
            catch (Exception ex) { throw new Exception("Error en ReadAll: " + ex.Message); }
            finally { c.Close(); }
            return lista;
        }
    }
}
