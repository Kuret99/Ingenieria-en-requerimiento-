using Servicios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public  class DALpatente_43BO
    {
       
        AccesoBD_43BO acceso = new AccesoBD_43BO();

        //Trae las patentes directas asociadas a un ROL
        public List<Patente_43BO> ObtenerPatentesRol_43BO(int idRol)
        {
            List<Patente_43BO> lista = new List<Patente_43BO>();

            string query = @"SELECT p.IdPatente_43BO, p.NombrePatente_43BO 
                             FROM Patente_43BO p
                             INNER JOIN Rol_Patente rp ON p.IdPatente_43BO = rp.IdPatente_43BO
                             WHERE rp.IdRol_43BO = @idRol";

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@idRol", idRol)
            };

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

        //trae las familias directas asociadas a un ROL
        public List<Familia_43BO> ObtenerFamiliasRol_43BO(int idRol)
        {
            List<Familia_43BO> lista = new List<Familia_43BO>();

            string query = @"SELECT f.IdFamilia_43BO, f.NombreFamilia_43BO
                             FROM Familia_43BO f
                             INNER JOIN Rol_Familia rf ON f.IdFamilia_43BO = rf.IdFamilia_43BO
                             WHERE rf.IdRol_43BO = @idRol";

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@idRol", idRol)
            };

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

        //Trae las patentes que prtenecfen a una FAMIliA (Hojas)
        public List<Patente_43BO> ObtenerPatentesDeFamilia_43BO(int idFamilia)
        {
            List<Patente_43BO> lista = new List<Patente_43BO>();

            string query = @"SELECT p.IdPatente_43BO, p.NombrePatente_43BO
                             FROM Patente_43BO p
                             INNER JOIN Patente_Familia pf ON p.IdPatente_43BO = pf.IdPatente_43BO
                             WHERE pf.IdFamilia_43BO = @idFamilia";

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@idFamilia", idFamilia)
            };

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

        //rtae las famias hijas de una FAMILIA (Recursividad)
        public List<Familia_43BO> ObtenerFamiliasHijas_43BO(int idFamiliaPadre)
        {
            List<Familia_43BO> lista = new List<Familia_43BO>();

            string query = @"SELECT f.IdFamilia_43BO, f.NombreFamilia_43BO
                             FROM Familia_43BO f
                             INNER JOIN Familia_Familia ff ON f.IdFamilia_43BO = ff.IdFamiliaHijo_43BO
                             WHERE ff.IdFamiliaPadre_43BO = @idFamiliaPadre";

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@idFamiliaPadre", idFamiliaPadre)
            };

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

    }
}
