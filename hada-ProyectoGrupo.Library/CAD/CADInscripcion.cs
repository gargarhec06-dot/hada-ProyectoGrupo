using hada_ProyectoGrupo.Library.EN;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace hada_ProyectoGrupo.Library.CAD
{
    public class CADInscripcion
    {
        private string s;

        public CADInscripcion()
        {
            s = ConfigurationManager.ConnectionStrings["HadaEsports"].ToString();
        }

        public bool Create(ENInscripcion en)
        {
            bool ok = false;
            SqlConnection c = new SqlConnection(s); 
            try
            {
                c.Open(); 

                string query = "INSERT INTO Inscripcion (id_equipo, id_torneo, fecha_inscripcion, estado, cuota_pagada, moneda) " +
                               "VALUES (@eq, @tor, @fec, @est, @cuo, @mon)";

                SqlCommand com = new SqlCommand(query, c); 
                
                com.Parameters.AddWithValue("@eq", en.Id_equipo); 
                com.Parameters.AddWithValue("@tor", en.Id_torneo); 
                com.Parameters.AddWithValue("@fec", en.Fecha_inscripcion); 
                com.Parameters.AddWithValue("@est", en.Estado); 
                com.Parameters.AddWithValue("@cuo", en.Cuota_pagada); 
                com.Parameters.AddWithValue("@mon", en.Moneda); 

                
                if (com.ExecuteNonQuery() > 0) ok = true;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { c.Close(); } 
            return ok;
        }

        public bool Read(ENInscripcion en)
        {
            bool ok = false;
            SqlConnection c = new SqlConnection(s);
            try
            {
                c.Open();
                string query = "SELECT * FROM Inscripcion WHERE id_inscripcion = @id";
                SqlCommand com = new SqlCommand(query, c);
                com.Parameters.AddWithValue("@id", en.Id_inscripcion);

                SqlDataReader dr = com.ExecuteReader(); 
                if (dr.Read()) 
                {
                    en.Id_equipo = (int)dr["id_equipo"]; 
                    en.Id_torneo = (int)dr["id_torneo"]; 
                    en.Fecha_inscripcion = (DateTime)dr["fecha_inscripcion"];
                    en.Estado = dr["estado"].ToString(); 
                    en.Cuota_pagada = float.Parse(dr["cuota_pagada"].ToString()); 
                    en.Moneda = dr["moneda"].ToString(); 
                    ok = true;
                }
                dr.Close(); 
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { c.Close(); }
            return ok;
        }

        public bool Update(ENInscripcion en)
        {
            bool ok = false;
            SqlConnection c = new SqlConnection(s);
            try
            {
                c.Open();
                string query = "UPDATE Inscripcion SET id_equipo=@eq, id_torneo=@tor, fecha_inscripcion=@fec, " +
                               "estado=@est, cuota_pagada=@cuo, moneda=@mon WHERE id_inscripcion=@id";
                SqlCommand com = new SqlCommand(query, c);
                com.Parameters.AddWithValue("@id", en.Id_inscripcion);
                com.Parameters.AddWithValue("@eq", en.Id_equipo);
                com.Parameters.AddWithValue("@tor", en.Id_torneo);
                com.Parameters.AddWithValue("@fec", en.Fecha_inscripcion);
                com.Parameters.AddWithValue("@est", en.Estado);
                com.Parameters.AddWithValue("@cuo", en.Cuota_pagada);
                com.Parameters.AddWithValue("@mon", en.Moneda);

                if (com.ExecuteNonQuery() > 0) ok = true;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { c.Close(); }
            return ok;
        }

        public bool Delete(ENInscripcion en)
        {
            bool ok = false;
            SqlConnection c = new SqlConnection(s);
            try
            {
                c.Open();
                string query = "DELETE FROM Inscripcion WHERE id_inscripcion = @id";
                SqlCommand com = new SqlCommand(query, c);
                com.Parameters.AddWithValue("@id", en.Id_inscripcion);

                if (com.ExecuteNonQuery() > 0) ok = true;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { c.Close(); }
            return ok;
        }

        public List<ENInscripcion> ReadAll()
        {
            List<ENInscripcion> lista = new List<ENInscripcion>();
            SqlConnection c = new SqlConnection(s);
            try
            {
                c.Open();
                string query = "SELECT * FROM Inscripcion";
                SqlCommand com = new SqlCommand(query, c);
                SqlDataReader dr = com.ExecuteReader(); 

                while (dr.Read()) 
                {
                    ENInscripcion en = new ENInscripcion();
                    en.Id_inscripcion = (int)dr["id_inscripcion"]; 
                    en.Id_equipo = (int)dr["id_equipo"]; 
                    en.Id_torneo = (int)dr["id_torneo"]; 
                    en.Fecha_inscripcion = (DateTime)dr["fecha_inscripcion"]; 
                    en.Estado = dr["estado"].ToString(); 
                    en.Cuota_pagada = float.Parse(dr["cuota_pagada"].ToString()); 
                    en.Moneda = dr["moneda"].ToString(); 
                    lista.Add(en); 
                }
                dr.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { c.Close(); }
            return lista;
        }

        public List<ENEquipo> ReadEquiposByTorneo(int idTorneo)
        {
            List<ENEquipo> lista = new List<ENEquipo>();
            SqlConnection c = new SqlConnection(s);
            try
            {
                c.Open();
                string query = @"SELECT e.id_equipo, e.nombre 
                         FROM Equipo e
                         JOIN Inscripcion i ON e.id_equipo = i.id_equipo
                         WHERE i.id_torneo = @tor AND i.estado != 'Rechazado'";
                SqlCommand com = new SqlCommand(query, c);
                com.Parameters.AddWithValue("@tor", idTorneo);
                SqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    ENEquipo en = new ENEquipo();
                    en.Id_equipo = (int)dr["id_equipo"];
                    en.Nombre = dr["nombre"].ToString();
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
