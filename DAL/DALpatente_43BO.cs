using Servicios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class DALpatente_43BO
    {
        AccesoBD_43BO acceso = new AccesoBD_43BO();

        public List<int> ObtenerIdsPermisosPorRol_43BO(int idRol)
        {
            List<int> lista = new List<int>();
            string query = "SELECT IdPatente_43BO FROM Rol_Patente WHERE IdRol_43BO = @idRol";
            SqlParameter[] param = { new SqlParameter("@idRol", idRol) };

            DataTable dt = acceso.Leer_43BO(query, param);
            foreach (DataRow fila in dt.Rows)
            {
                lista.Add(Convert.ToInt32(fila["IdPatente_43BO"]));
            }
            return lista;
        }

        public List<Patente_43BO> ObtenerPatentesRol_43BO(int idRol)
        {
            List<Patente_43BO> lista = new List<Patente_43BO>();
            string query = @"SELECT p.IdPatente_43BO, p.NombrePatente_43BO 
                             FROM Patente_43BO p
                             INNER JOIN Rol_Patente rp ON p.IdPatente_43BO = rp.IdPatente_43BO
                             WHERE rp.IdRol_43BO = @idRol";

            SqlParameter[] parametros = { new SqlParameter("@idRol", idRol) };
            DataTable dt = acceso.Leer_43BO(query, parametros);

            foreach (DataRow row in dt.Rows)
            {
                lista.Add(new Patente_43BO
                {
                    IdRol_43BO = Convert.ToInt32(row["IdPatente_43BO"]),
                    Nombre_43BO = row["NombrePatente_43BO"].ToString()
                });
            }
            return lista;
        }

        public List<Familia_43BO> ObtenerFamiliasRol_43BO(int idRol)
        {
            List<Familia_43BO> lista = new List<Familia_43BO>();
            string query = @"SELECT f.IdFamilia_43BO, f.NombreFamilia_43BO
                             FROM Familia_43BO f
                             INNER JOIN Rol_Familia rf ON f.IdFamilia_43BO = rf.IdFamilia_43BO
                             WHERE rf.IdRol_43BO = @idRol";

            SqlParameter[] parametros = { new SqlParameter("@idRol", idRol) };
            DataTable dt = acceso.Leer_43BO(query, parametros);

            foreach (DataRow row in dt.Rows)
            {
                lista.Add(new Familia_43BO
                {
                    IdRol_43BO = Convert.ToInt32(row["IdFamilia_43BO"]),
                    Nombre_43BO = row["NombreFamilia_43BO"].ToString()
                });
            }
            return lista;
        }

        public List<Patente_43BO> ObtenerPatentesDeFamilia_43BO(int idFamilia)
        {
            List<Patente_43BO> lista = new List<Patente_43BO>();
            string query = @"SELECT p.IdPatente_43BO, p.NombrePatente_43BO 
                             FROM Patente_43BO p
                             INNER JOIN Patente_Familia pf ON p.IdPatente_43BO = pf.IdPatente_43BO
                             WHERE pf.IdFamilia_43BO = @idFamilia";

            SqlParameter[] parametros = { new SqlParameter("@idFamilia", idFamilia) };
            DataTable dt = acceso.Leer_43BO(query, parametros);

            foreach (DataRow row in dt.Rows)
            {
                lista.Add(new Patente_43BO
                {
                    IdRol_43BO = Convert.ToInt32(row["IdPatente_43BO"]),
                    Nombre_43BO = row["NombrePatente_43BO"].ToString()
                });
            }
            return lista;
        }

        public List<Familia_43BO> ObtenerFamiliasHijas_43BO(int idFamiliaPadre)
        {
            List<Familia_43BO> lista = new List<Familia_43BO>();
            string query = @"SELECT f.IdFamilia_43BO, f.NombreFamilia_43BO
                             FROM Familia_43BO f
                             INNER JOIN Familia_Familia ff ON f.IdFamilia_43BO = ff.IdFamiliaHijo_43BO
                             WHERE ff.IdFamiliaPadre_43BO = @idFamiliaPadre";

            SqlParameter[] parametros = { new SqlParameter("@idFamiliaPadre", idFamiliaPadre) };
            DataTable dt = acceso.Leer_43BO(query, parametros);

            foreach (DataRow row in dt.Rows)
            {
                lista.Add(new Familia_43BO
                {
                    IdRol_43BO = Convert.ToInt32(row["IdFamilia_43BO"]),
                    Nombre_43BO = row["NombreFamilia_43BO"].ToString()
                });
            }
            return lista;
        }

        // ══════════════════════════════════════════════════════════════════
        // LISTAR CATÁLOGOS COMPLETOS
        // ══════════════════════════════════════════════════════════════════
        public List<Patente_43BO> ListarTodasLasPatentes_43BO()
        {
            List<Patente_43BO> lista = new List<Patente_43BO>();
            string query = "SELECT IdPatente_43BO, NombrePatente_43BO FROM Patente_43BO ORDER BY NombrePatente_43BO";
            DataTable dt = acceso.Leer_43BO(query);
            foreach (DataRow row in dt.Rows)
            {
                lista.Add(new Patente_43BO { IdRol_43BO = Convert.ToInt32(row["IdPatente_43BO"]), Nombre_43BO = row["NombrePatente_43BO"].ToString() });
            }
            return lista;
        }

        public List<Familia_43BO> ListarTodasLasFamilias_43BO()
        {
            List<Familia_43BO> lista = new List<Familia_43BO>();
            string query = "SELECT IdFamilia_43BO, NombreFamilia_43BO FROM Familia_43BO ORDER BY NombreFamilia_43BO";
            DataTable dt = acceso.Leer_43BO(query);
            foreach (DataRow row in dt.Rows)
            {
                lista.Add(new Familia_43BO { IdRol_43BO = Convert.ToInt32(row["IdFamilia_43BO"]), Nombre_43BO = row["NombreFamilia_43BO"].ToString() });
            }
            return lista;
        }

        public List<Familia_43BO> ListarTodosLosRoles_43BO()
        {
            List<Familia_43BO> lista = new List<Familia_43BO>();
            string query = "SELECT IdRol_43BO, NombreRol_43BO FROM Rol_43BO ORDER BY NombreRol_43BO";
            DataTable dt = acceso.Leer_43BO(query);
            foreach (DataRow row in dt.Rows)
            {
                lista.Add(new Familia_43BO { IdRol_43BO = Convert.ToInt32(row["IdRol_43BO"]), Nombre_43BO = row["NombreRol_43BO"].ToString() });
            }
            return lista;
        }

    
        // 
        public int InsertarPatente_43BO(string nombre)
        {
            string query = "INSERT INTO Patente_43BO (NombrePatente_43BO) OUTPUT INSERTED.IdPatente_43BO VALUES (@nombre)";
            SqlParameter[] parametros = { new SqlParameter("@nombre", nombre) };
            return EjecutarScalar_43BO(query, parametros);
        }

        public int InsertarFamilia_43BO(string nombre)
        {
            string query = "INSERT INTO Familia_43BO (NombreFamilia_43BO) OUTPUT INSERTED.IdFamilia_43BO VALUES (@nombre)";
            SqlParameter[] parametros = { new SqlParameter("@nombre", nombre) };
            return EjecutarScalar_43BO(query, parametros);
        }

        public int InsertarRol_43BO(string nombre)
        {
            string query = "INSERT INTO Rol_43BO (NombreRol_43BO) OUTPUT INSERTED.IdRol_43BO VALUES (@nombre)";
            SqlParameter[] parametros = { new SqlParameter("@nombre", nombre) };
            return EjecutarScalar_43BO(query, parametros);
        }

   
        public bool VincularHijo_43BO(Rol_43BO padre, Rol_43BO hijo, bool esModoRol)
        {
          
            if (padre.IdRol_43BO <= 0 || hijo.IdRol_43BO <= 0)
            {
                System.Diagnostics.Debug.WriteLine($"ERROR: ID Inválido. Padre: {padre.IdRol_43BO}, Hijo: {hijo.IdRol_43BO}");
                return false; // Retornamos false porque la operación no se puede realizar
            }

            string query = "";

            // ... (Mantén tu lógica de if/else para la query igual, está bien) ...
            if (esModoRol)
            {
                if (hijo is Patente_43BO) query = "INSERT INTO Rol_Patente (IdRol_43BO, IdPatente_43BO) VALUES (@idPadre, @idHijo)";
                else if (hijo is Familia_43BO) query = "INSERT INTO Rol_Familia (IdRol_43BO, IdFamilia_43BO) VALUES (@idPadre, @idHijo)";
            }
            else
            {
                if (hijo is Patente_43BO) query = "INSERT INTO Patente_Familia (IdFamilia_43BO, IdPatente_43BO) VALUES (@idPadre, @idHijo)";
                else if (hijo is Familia_43BO) query = "INSERT INTO Familia_Familia (IdFamiliaPadre_43BO, IdFamiliaHijo_43BO) VALUES (@idPadre, @idHijo)";
            }

    
            try
            {
                if (!string.IsNullOrEmpty(query))
                {
                    SqlParameter[] parametros = new SqlParameter[] {
                new SqlParameter("@idPadre", padre.IdRol_43BO),
                new SqlParameter("@idHijo", hijo.IdRol_43BO)
            };

                    int filas = acceso.Escribir_43BO(query, parametros);
                    return filas > 0; // Devuelve true si se insertó algo
                }
            }
            catch (Exception ex)
            {
              
                System.Diagnostics.Debug.WriteLine("Error de BD: " + ex.Message);
                return false;
            }

            return false;
        }

   
        public bool DesvincularHijo_43BO(Rol_43BO padre, Rol_43BO hijo, bool esModoRol)
        {
            string query = "";

            // LÓGICA CON CONDICIONAL DE MODO
            if (esModoRol)
            {
          
                if (hijo is Patente_43BO)
                {
                    query = "DELETE FROM Rol_Patente WHERE IdRol_43BO = @idPadre AND IdPatente_43BO = @idHijo";
                }
                else if (hijo is Familia_43BO)
                {
                    query = "DELETE FROM Rol_Familia WHERE IdRol_43BO = @idPadre AND IdFamilia_43BO = @idHijo";
                }
            }
            else
            {
                if (hijo is Patente_43BO)
                {
                    query = "DELETE FROM Patente_Familia WHERE IdFamilia_43BO = @idPadre AND IdPatente_43BO = @idHijo";
                }
                else if (hijo is Familia_43BO)
                {
                    query = "DELETE FROM Familia_Familia WHERE IdFamiliaPadre_43BO = @idPadre AND IdFamiliaHijo_43BO = @idHijo";
                }
            }

            if (string.IsNullOrEmpty(query)) return false;

            SqlParameter[] parametros = new SqlParameter[] {
        new SqlParameter("@idPadre", padre.IdRol_43BO),
        new SqlParameter("@idHijo", hijo.IdRol_43BO)
    };

            int filasAfectadas = acceso.Escribir_43BO(query, parametros);

            return filasAfectadas > 0;
        }

        // ELIMINACIÓN DE COMPONENTES
       
        public void EliminarRol_43BO(int idRol)
        {
            acceso.Escribir_43BO("DELETE FROM Rol_Patente WHERE IdRol_43BO = @id", new SqlParameter[] { new SqlParameter("@id", idRol) });
            acceso.Escribir_43BO("DELETE FROM Rol_Familia WHERE IdRol_43BO = @id", new SqlParameter[] { new SqlParameter("@id", idRol) });
            acceso.Escribir_43BO("DELETE FROM Rol_43BO WHERE IdRol_43BO = @id", new SqlParameter[] { new SqlParameter("@id", idRol) });
        }

        public void EliminarFamilia_43BO(int idFamilia)
        {
            acceso.Escribir_43BO("DELETE FROM Patente_Familia WHERE IdFamilia_43BO = @id", new SqlParameter[] { new SqlParameter("@id", idFamilia) });
            acceso.Escribir_43BO("DELETE FROM Familia_Familia WHERE IdFamiliaPadre_43BO = @id OR IdFamiliaHijo_43BO = @id", new SqlParameter[] { new SqlParameter("@id", idFamilia) });
            acceso.Escribir_43BO("DELETE FROM Rol_Familia WHERE IdFamilia_43BO = @id", new SqlParameter[] { new SqlParameter("@id", idFamilia) });
            acceso.Escribir_43BO("DELETE FROM Familia_43BO WHERE IdFamilia_43BO = @id", new SqlParameter[] { new SqlParameter("@id", idFamilia) });
        }

        private int EjecutarScalar_43BO(string query, SqlParameter[] parametros)
        {
            using (SqlConnection con = new SqlConnection("Data Source=Usuario;Initial Catalog=Ing.Software;Integrated Security=True"))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                if (parametros != null) cmd.Parameters.AddRange(parametros);
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
    }
}