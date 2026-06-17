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
            SqlParameter[] param = { new SqlParameter("@idRol", idRol) };

            DataTable dt = acceso.Leer_43BO("ObtenerIdsPermisosPorRol_43BO", param, CommandType.StoredProcedure);
            foreach (DataRow fila in dt.Rows)
            {
                lista.Add(Convert.ToInt32(fila["IdPatente_43BO"]));
            }
            return lista;
        }

        // 
        public List<Patente_43BO> ObtenerPatentesRol_43BO(int idRol)
        {
            List<Patente_43BO> lista = new List<Patente_43BO>();
            SqlParameter[] parametros = {
                new SqlParameter("@idPadre", idRol),
                new SqlParameter("@tipo", "PatentesRol")
            };

            DataTable dt = acceso.Leer_43BO("ObtenerComponentesHijos_43BO", parametros, CommandType.StoredProcedure);
            foreach (DataRow row in dt.Rows)
            {
                lista.Add(new Patente_43BO
                {
                    IdRol_43BO = Convert.ToInt32(row["Id_43BO"]),
                    Nombre_43BO = row["Nombre_43BO"].ToString()
                });
            }
            return lista;
        }

        public List<Familia_43BO> ObtenerFamiliasRol_43BO(int idRol)
        {
            List<Familia_43BO> lista = new List<Familia_43BO>();
            SqlParameter[] parametros = {
                new SqlParameter("@idPadre", idRol),
                new SqlParameter("@tipo", "FamiliasRol")
            };

            DataTable dt = acceso.Leer_43BO("ObtenerComponentesHijos_43BO", parametros, CommandType.StoredProcedure);
            foreach (DataRow row in dt.Rows)
            {
                lista.Add(new Familia_43BO
                {
                    IdRol_43BO = Convert.ToInt32(row["Id_43BO"]),
                    Nombre_43BO = row["Nombre_43BO"].ToString()
                });
            }
            return lista;
        }

        public List<Patente_43BO> ObtenerPatentesDeFamilia_43BO(int idFamilia)
        {
            List<Patente_43BO> lista = new List<Patente_43BO>();
            SqlParameter[] parametros = {
                new SqlParameter("@idPadre", idFamilia),
                new SqlParameter("@tipo", "PatentesFamilia")
            };

            DataTable dt = acceso.Leer_43BO("ObtenerComponentesHijos_43BO", parametros, CommandType.StoredProcedure);
            foreach (DataRow row in dt.Rows)
            {
                lista.Add(new Patente_43BO
                {
                    IdRol_43BO = Convert.ToInt32(row["Id_43BO"]),
                    Nombre_43BO = row["Nombre_43BO"].ToString()
                });
            }
            return lista;
        }

        public List<Familia_43BO> ObtenerFamiliasHijas_43BO(int idFamiliaPadre)
        {
            List<Familia_43BO> lista = new List<Familia_43BO>();
            SqlParameter[] parametros = {
                new SqlParameter("@idPadre", idFamiliaPadre),
                new SqlParameter("@tipo", "FamiliasHijas")
            };

            DataTable dt = acceso.Leer_43BO("ObtenerComponentesHijos_43BO", parametros, CommandType.StoredProcedure);
            foreach (DataRow row in dt.Rows)
            {
                lista.Add(new Familia_43BO
                {
                    IdRol_43BO = Convert.ToInt32(row["Id_43BO"]),
                    Nombre_43BO = row["Nombre_43BO"].ToString()
                });
            }
            return lista;
        }

        // 
        public List<Patente_43BO> ListarTodasLasPatentes_43BO()
        {
            List<Patente_43BO> lista = new List<Patente_43BO>();
            SqlParameter[] parametros = { new SqlParameter("@tipo", "Patente") };

            DataTable dt = acceso.Leer_43BO("ListarTodo_43BO", parametros, CommandType.StoredProcedure);
            foreach (DataRow row in dt.Rows)
            {
                lista.Add(new Patente_43BO { IdRol_43BO = Convert.ToInt32(row["Id_43BO"]), Nombre_43BO = row["Nombre_43BO"].ToString() });
            }
            return lista;
        }

        public List<Familia_43BO> ListarTodasLasFamilias_43BO()
        {
            List<Familia_43BO> lista = new List<Familia_43BO>();
            SqlParameter[] parametros = { new SqlParameter("@tipo", "Familia") };

            DataTable dt = acceso.Leer_43BO("ListarTodo_43BO", parametros, CommandType.StoredProcedure);
            foreach (DataRow row in dt.Rows)
            {
                lista.Add(new Familia_43BO { IdRol_43BO = Convert.ToInt32(row["Id_43BO"]), Nombre_43BO = row["Nombre_43BO"].ToString() });
            }
            return lista;
        }

        public List<Familia_43BO> ListarTodosLosRoles_43BO()
        {
            List<Familia_43BO> lista = new List<Familia_43BO>();
            SqlParameter[] parametros = { new SqlParameter("@tipo", "Rol") };

            DataTable dt = acceso.Leer_43BO("ListarTodo_43BO", parametros, CommandType.StoredProcedure);
            foreach (DataRow row in dt.Rows)
            {
                lista.Add(new Familia_43BO { IdRol_43BO = Convert.ToInt32(row["Id_43BO"]), Nombre_43BO = row["Nombre_43BO"].ToString() });
            }
            return lista;
        }

        //
        public int InsertarFamilia_43BO(string nombre)
        {
            SqlParameter[] parametros = { new SqlParameter("@nombre", nombre), new SqlParameter("@tipo", "Familia") };
            return acceso.EjecutarScalar_43BO("InsertarComponente_43BO", parametros, CommandType.StoredProcedure);
        }

        public int InsertarRol_43BO(string nombre)
        {
            SqlParameter[] parametros = { new SqlParameter("@nombre", nombre), new SqlParameter("@tipo", "Rol") };
            return acceso.EjecutarScalar_43BO("InsertarComponente_43BO", parametros, CommandType.StoredProcedure);
        }

        // 
        public bool VincularHijo_43BO(Rol_43BO padre, Rol_43BO hijo, bool esModoRol)
        {
            if (padre.IdRol_43BO <= 0 || hijo.IdRol_43BO <= 0) return false;

            string tipoSubModulo = esModoRol
                ? ((hijo is Patente_43BO) ? "RolPatente" : "RolFamilia")
                : ((hijo is Patente_43BO) ? "PatenteFamilia" : "FamiliaFamilia");

            try
            {
                SqlParameter[] parametros = {
                    new SqlParameter("@idPadre", padre.IdRol_43BO),
                    new SqlParameter("@idHijo", hijo.IdRol_43BO),
                    new SqlParameter("@tipo", tipoSubModulo)
                };
                int filas = acceso.Escribir_43BO("VincularComponente_43BO", parametros, CommandType.StoredProcedure);
                return filas > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DesvincularHijo_43BO(Rol_43BO padre, Rol_43BO hijo, bool esModoRol)
        {
            if (padre.IdRol_43BO <= 0 || hijo.IdRol_43BO <= 0) return false;

            string tipoSubModulo = esModoRol
                ? ((hijo is Patente_43BO) ? "RolPatente" : "RolFamilia")
                : ((hijo is Patente_43BO) ? "PatenteFamilia" : "FamiliaFamilia");

            try
            {
                SqlParameter[] parametros = {
                    new SqlParameter("@idPadre", padre.IdRol_43BO),
                    new SqlParameter("@idHijo", hijo.IdRol_43BO),
                    new SqlParameter("@tipo", tipoSubModulo)
                };
                int filasAfectadas = acceso.Escribir_43BO("DesvincularComponente_43BO", parametros, CommandType.StoredProcedure);
                return filasAfectadas > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // 
        public void EliminarRol_43BO(int idRol)
        {
            SqlParameter[] parametros = { new SqlParameter("@idComponente", idRol), new SqlParameter("@tipo", "Rol") };
            acceso.Escribir_43BO("EliminarComponente_43BO", parametros, CommandType.StoredProcedure);
        }

        public void EliminarFamilia_43BO(int idFamilia)
        {
            SqlParameter[] parametros = { new SqlParameter("@idComponente", idFamilia), new SqlParameter("@tipo", "Familia") };
            acceso.Escribir_43BO("EliminarComponente_43BO", parametros, CommandType.StoredProcedure);
        }

        // 
        public bool ModificarNombreComponente_43BO(int id, string nuevoNombre, bool esModoRol)
        {
            SqlParameter[] parametros = {
                new SqlParameter("@Id", id),
                new SqlParameter("@NuevoNombre", nuevoNombre),
                new SqlParameter("@Tipo", esModoRol ? "Rol" : "Familia")
            };

          
            int filasAfectadas = acceso.Escribir_43BO("ModificarNombreComponente_43BO", parametros, CommandType.StoredProcedure);
            return filasAfectadas > 0;
        }
    }
}