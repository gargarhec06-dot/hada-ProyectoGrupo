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
                // Contamos cuántos usuarios tienen ese email y password
                string sql = "SELECT COUNT(*) FROM [Usuario] WHERE email = @email AND password = @pass";

                SqlCommand com = new SqlCommand(sql, c);
                com.Parameters.AddWithValue("@email", en.Email);
                com.Parameters.AddWithValue("@pass", en.Password);

                int count = Convert.ToInt32(com.ExecuteScalar()); 

                if (count == 1) ok = true; 
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

        public bool Update(ENUsuario en)
        {
            bool ok = true; 


            return ok;
        }

        public bool Delete(ENUsuario en)
        {
            bool ok = true; 


            return ok;
        }
    }
}
