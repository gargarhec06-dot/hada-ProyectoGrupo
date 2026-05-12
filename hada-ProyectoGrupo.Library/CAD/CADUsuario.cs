using hada_ProyectoGrupo.Library.EN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace hada_ProyectoGrupo.Library.CAD
{
    public class CADUsuario
    {

        private string constring;

        public CADUsuario() 
        {
            constring = ConfigurationManager.ConnectionStrings["HadaEsports"].ToString();
        }

        public bool Login(ENUsuario en)
        {
            bool ok = false;
            SqlConnection c = new SqlConnection(constring);
            try
            {
                c.Open();
                string sql = "SELECT nombre, apellidos, fecha_nacimiento, pais, saldo_cartera, verificado " +
                             "FROM [Usuario] WHERE email = @email AND password = @pass";
                SqlCommand com = new SqlCommand(sql, c);
                com.Parameters.AddWithValue("@email", en.Email);
                com.Parameters.AddWithValue("@pass", en.Password);
                SqlDataReader reader = com.ExecuteReader();
                if (reader.Read())
                {
                    // Rellena el objeto con los datos de la BD
                    en.Nombre = reader["nombre"].ToString();
                    en.Apellidos = reader["apellidos"] == DBNull.Value ? "" : reader["apellidos"].ToString();
                    en.Fecha_Nacimiento = Convert.ToDateTime(reader["fecha_nacimiento"]);
                    en.Pais = reader["pais"] == DBNull.Value ? "" : reader["pais"].ToString();
                    en.Saldo_cartera = reader["saldo_cartera"] == DBNull.Value ? 0 : Convert.ToSingle(reader["saldo_cartera"]);
                    en.Verificado = Convert.ToBoolean(reader["verificado"]);
                    ok = true;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                ok = false;
                Console.WriteLine("User operation has failed. Error: {0}", ex.Message);
            }
            finally { c.Close(); }
            return ok;
        }

        public bool Create(ENUsuario en)
        {
            bool ok = true;
            SqlConnection c = new SqlConnection(constring);
            try
            {
                c.Open();

                string sql = "INSERT INTO [Usuario] (email, password, nombre, apellidos, fecha_nacimiento, pais) " +
                             "VALUES (@email, @pass, @nom, @ape, @fec, @pais)";

                SqlCommand com = new SqlCommand(sql, c);
                com.Parameters.AddWithValue("@email", en.Email);
                com.Parameters.AddWithValue("@pass", en.Password);
                com.Parameters.AddWithValue("@nom", en.Nombre);
                com.Parameters.AddWithValue("@ape", (object)en.Apellidos ?? DBNull.Value); 
                com.Parameters.AddWithValue("@fec", en.Fecha_Nacimiento);
                com.Parameters.AddWithValue("@pais", (object)en.Pais ?? DBNull.Value);

                com.ExecuteNonQuery(); 
            }
            catch (Exception ex)
            {
                ok = false;
                Console.WriteLine("User operation has failed. Error: {0}",ex.Message);
            }
            finally { c.Close(); }

            return ok;

        }

        public bool Read(ENUsuario en)
        {
            bool ok = false;
            SqlConnection c = new SqlConnection(constring);
            try
            {
                c.Open();
                string query = "SELECT * FROM Usuario WHERE email = @email";
                SqlCommand com = new SqlCommand(query, c);
                com.Parameters.AddWithValue("@email", en.Email);
                SqlDataReader dr = com.ExecuteReader();
                if (dr.Read())
                {
                    en.Email = dr["email"].ToString();
                    en.Password = dr["password"].ToString();
                    en.Nombre = dr["nombre"]?.ToString() ?? "";
                    en.Apellidos = dr["apellidos"] == DBNull.Value ? "" : dr["apellidos"].ToString();
                    en.Fecha_Nacimiento = dr["fecha_nacimiento"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(dr["fecha_nacimiento"]);
                    en.Pais = dr["pais"] == DBNull.Value ? "" : dr["pais"].ToString();
                    en.Saldo_cartera = dr["saldo_cartera"] == DBNull.Value ? 0f : Convert.ToSingle(dr["saldo_cartera"]);
                    en.Verificado = dr["verificado"] == DBNull.Value ? false : Convert.ToBoolean(dr["verificado"]);
                    ok = true;
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Error en Read: " + ex.Message);
            }
            finally { c.Close(); }
            return ok;
        }

        public bool Update(ENUsuario en)
        {
            bool ok = false;
            SqlConnection c = new SqlConnection(constring);
            try
            {
                c.Open();
                string sql = @"UPDATE Usuario 
                       SET nombre = @nombre, apellidos = @apellidos, pais = @pais";

                // Si hay contraseña nueva, la actualizamos
                if (!string.IsNullOrEmpty(en.Password))
                {
                    sql += ", password = @password";
                }

                sql += " WHERE email = @email";

                SqlCommand com = new SqlCommand(sql, c);
                com.Parameters.AddWithValue("@nombre", en.Nombre);
                com.Parameters.AddWithValue("@apellidos", (object)en.Apellidos ?? DBNull.Value);
                com.Parameters.AddWithValue("@pais", (object)en.Pais ?? DBNull.Value);
                com.Parameters.AddWithValue("@email", en.Email);

                if (!string.IsNullOrEmpty(en.Password))
                {
                    com.Parameters.AddWithValue("@password", en.Password);
                }

                if (com.ExecuteNonQuery() > 0) ok = true;
            }
            catch (Exception)
            {
                ok = false;
            }
            finally { c.Close(); }
            return ok;
        }

        public bool Delete(ENUsuario en)
        {
            bool ok = false;
            SqlConnection c = new SqlConnection(constring);
            try
            {
                c.Open();
                string sql = "DELETE FROM Usuario WHERE email = @email";
                SqlCommand com = new SqlCommand(sql, c);
                com.Parameters.AddWithValue("@email", en.Email);
                int filas = com.ExecuteNonQuery();
                ok = filas > 0;
            }
            catch (Exception ex)
            {
                // Guardar el error para depurar
                System.Diagnostics.Debug.WriteLine("Error al eliminar: " + ex.Message);
                ok = false;
            }
            finally { c.Close(); }
            return ok;
        }
    }
}
