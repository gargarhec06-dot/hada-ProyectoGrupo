using hada_ProyectoGrupo.Library.EN;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace hada_ProyectoGrupo.Library.CAD
{
    public class CADPatrocinador
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["HadaEsports"].ConnectionString;

        public CADPatrocinador() { }

        public int Create(ENPatrocinador en)
        {
            int nuevoId = -1;
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "INSERT INTO Patrocinador (Nombre, Telefono, Email, PaginaWeb, InicioContrato, FinContrato) " +
                                   "VALUES (@Nombre, @Telefono, @Email, @PaginaWeb, @InicioContrato, @FinContrato); " +
                                   "SELECT SCOPE_IDENTITY();"; // devuelve el ID recién insertado
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Nombre", en.Nombre);
                    cmd.Parameters.AddWithValue("@Telefono", en.Telefono);
                    cmd.Parameters.AddWithValue("@Email", en.Email);
                    cmd.Parameters.AddWithValue("@PaginaWeb", (object)en.PaginaWeb ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@InicioContrato", en.InicioContrato);
                    cmd.Parameters.AddWithValue("@FinContrato", en.FinContrato);
                    nuevoId = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Create Patrocinador failed. Error: {0}", ex.Message);
            }
            return nuevoId;
        }

        public bool Read(ENPatrocinador en)
        {
            bool ok = false;
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT * FROM Patrocinador WHERE IdPatrocinador = @id";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@id", en.IdPatrocinador);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        en.Nombre = reader["Nombre"].ToString();
                        en.Telefono = reader["Telefono"].ToString();
                        en.Email = reader["Email"].ToString();
                        en.PaginaWeb = reader["PaginaWeb"].ToString();
                        en.InicioContrato = Convert.ToDateTime(reader["InicioContrato"]);
                        en.FinContrato = Convert.ToDateTime(reader["FinContrato"]);
                        ok = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Read Patrocinador failed. Error: {0}", ex.Message);
            }
            return ok;
        }

        public bool Update(ENPatrocinador en)
        {
            bool ok = false;
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "UPDATE Patrocinador SET Nombre = @Nombre, Telefono = @Telefono, " +
                                   "Email = @Email, PaginaWeb = @PaginaWeb, " +
                                   "InicioContrato = @InicioContrato, FinContrato = @FinContrato " +
                                   "WHERE IdPatrocinador = @id";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@id", en.IdPatrocinador);
                    cmd.Parameters.AddWithValue("@Nombre", en.Nombre);
                    cmd.Parameters.AddWithValue("@Telefono", en.Telefono);
                    cmd.Parameters.AddWithValue("@Email", en.Email);
                    cmd.Parameters.AddWithValue("@PaginaWeb", (object)en.PaginaWeb ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@InicioContrato", en.InicioContrato);
                    cmd.Parameters.AddWithValue("@FinContrato", en.FinContrato);
                    cmd.ExecuteNonQuery();
                    ok = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Update Patrocinador failed. Error: {0}", ex.Message);
            }
            return ok;
        }

        public bool Delete(ENPatrocinador en)
        {
            bool ok = false;
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "DELETE FROM Patrocinador WHERE IdPatrocinador = @id";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@id", en.IdPatrocinador);
                    cmd.ExecuteNonQuery();
                    ok = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Delete Patrocinador failed. Error: {0}", ex.Message);
            }
            return ok;
        }

        public List<ENPatrocinador> ReadAll()
        {
            List<ENPatrocinador> lista = new List<ENPatrocinador>();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT * FROM Patrocinador";
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        ENPatrocinador p = new ENPatrocinador();
                        p.IdPatrocinador = Convert.ToInt32(reader["IdPatrocinador"]);
                        p.Nombre = reader["Nombre"].ToString();
                        p.Telefono = reader["Telefono"].ToString();
                        p.Email = reader["Email"].ToString();
                        p.PaginaWeb = reader["PaginaWeb"].ToString();
                        p.InicioContrato = Convert.ToDateTime(reader["InicioContrato"]);
                        p.FinContrato = Convert.ToDateTime(reader["FinContrato"]);
                        lista.Add(p);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ReadAll Patrocinador failed. Error: {0}", ex.Message);
            }
            return lista;
        }

        public List<ENTorneoPatrocinador> ReadTorneos(int idPatrocinador)
        {
            List<ENTorneoPatrocinador> lista = new List<ENTorneoPatrocinador>();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT T.nombre, P.Cantidad FROM Patrocinio P " +
                                   "JOIN Torneo T ON P.Codigo = T.codigo " +
                                   "WHERE P.IdPatrocinador = @id";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@id", idPatrocinador);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        lista.Add(new ENTorneoPatrocinador
                        {
                            NombreTorneo = reader["nombre"].ToString(),
                            Cantidad = Convert.ToDecimal(reader["Cantidad"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ReadTorneos Patrocinador failed. Error: {0}", ex.Message);
            }
            return lista;
        }


        public bool CreatePatrocinio(int idPatrocinador, int codigoTorneo, int cantidad)
        {
            bool ok = false;
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "INSERT INTO Patrocinio (IdPatrocinador, Codigo, Cantidad) " +
                                   "VALUES (@idPatrocinador, @codigo, @cantidad)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@idPatrocinador", idPatrocinador);
                    cmd.Parameters.AddWithValue("@codigo", codigoTorneo);
                    cmd.Parameters.AddWithValue("@cantidad", cantidad);
                    cmd.ExecuteNonQuery();
                    ok = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("CreatePatrocinio failed. Error: {0}", ex.Message);
            }
            return ok;
        }


        public List<ENTorneoPatrocinador> ReadPatrocinios(int idPatrocinador)
        {
            List<ENTorneoPatrocinador> lista = new List<ENTorneoPatrocinador>();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT Codigo, Cantidad FROM Patrocinio WHERE IdPatrocinador = @id";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@id", idPatrocinador);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        lista.Add(new ENTorneoPatrocinador
                        {
                            CodigoTorneo = (int)reader["Codigo"],
                            Cantidad = Convert.ToDecimal(reader["Cantidad"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ReadPatrocinios failed. Error: {0}", ex.Message);
            }
            return lista;
        }

        public bool DeletePatrocinios(int idPatrocinador)
        {
            bool ok = false;
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "DELETE FROM Patrocinio WHERE IdPatrocinador = @id";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@id", idPatrocinador);
                    cmd.ExecuteNonQuery();
                    ok = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("DeletePatrocinios failed. Error: {0}", ex.Message);
            }
            return ok;
        }

        public List<ENPatrocinador> ReadFiltrado(string nombre, int codigoTorneo)
        {
            List<ENPatrocinador> lista = new List<ENPatrocinador>();
            SqlConnection c = new SqlConnection(connectionString);
            try
            {
                c.Open();
                string query = "SELECT DISTINCT P.* FROM Patrocinador P ";

                if (codigoTorneo > 0)
                    query += "JOIN Patrocinio PT ON P.IdPatrocinador = PT.IdPatrocinador ";

                query += "WHERE 1=1 ";

                if (!string.IsNullOrEmpty(nombre))
                    query += "AND P.Nombre LIKE @nombre ";

                if (codigoTorneo > 0)
                    query += "AND PT.Codigo = @codigo ";

                SqlCommand com = new SqlCommand(query, c);

                if (!string.IsNullOrEmpty(nombre))
                    com.Parameters.AddWithValue("@nombre", "%" + nombre + "%");

                if (codigoTorneo > 0)
                    com.Parameters.AddWithValue("@codigo", codigoTorneo);

                SqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    ENPatrocinador p = new ENPatrocinador();
                    p.IdPatrocinador = Convert.ToInt32(dr["IdPatrocinador"]);
                    p.Nombre = dr["Nombre"].ToString();
                    p.Telefono = dr["Telefono"].ToString();
                    p.Email = dr["Email"].ToString();
                    p.PaginaWeb = dr["PaginaWeb"].ToString();
                    p.InicioContrato = Convert.ToDateTime(dr["InicioContrato"]);
                    p.FinContrato = Convert.ToDateTime(dr["FinContrato"]);
                    lista.Add(p);
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ReadFiltrado Patrocinador failed. Error: {0}", ex.Message);
            }
            finally { c.Close(); }
            return lista;
        }


    }

}