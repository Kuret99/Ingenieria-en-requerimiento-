using Servicios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALdv_43BO
    {
        private AccesoBD_43BO acceso = new AccesoBD_43BO();

        // nombre de la fila especial que guarda el dv de toda la bd
        public const string NombreTotalBD_43BO = "TOTAL_BD_43BO";

        // bandera anti loop: guardar el dv tambien pasa por Escribir_43BO
        // y sin esto se recalcularia infinitamente
        [ThreadStatic]
        private static bool _persistiendoDV;

        // las tablas que entran en el calculo (la dv obvio queda afuera)
        private static readonly string[] _tablasVerificadas =
        {
            "Usuarios_43BO",
            "Rol_43BO",
            "Patente_43BO",
            "Familia_43BO",
            "Familia_Familia",
            "Rol_Familia",
            "Rol_Patente",
            "Patente_Familia",
            "Bitacora_43BO"
        };

        // -------------------- generacion --------------------

        // arma el objeto dv completo (una fila por tabla + la fila total de la bd)
        // no persiste nada, lo usan tanto la generacion como la revision del login
        public List<DV_43BO> CalcularDV_43BO()
        {
            List<DV_43BO> lista = new List<DV_43BO>();

            long dvhBD = 0; // aca voy acumulando el dvh de toda la bd
            long dvvBD = 0; // y aca el dvv de toda la bd

            foreach (string tabla in _tablasVerificadas)
            {
                DataTable datos = acceso.Leer_43BO("SELECT * FROM [" + tabla + "]");

                // dvh de cada registro (columna a columna) y los sumo para el dvh de la tabla
                long dvhTabla = 0;
                foreach (DataRow fila in datos.Rows)
                {
                    long dvhRegistro = 0;
                    foreach (DataColumn col in datos.Columns)
                    {
                        dvhRegistro += ConvertirValorHex_43BO(fila[col]);
                    }
                    dvhTabla += dvhRegistro;
                }

                // dvv de cada columna (registro a registro) y los sumo para el dvv de la tabla
                long dvvTabla = 0;
                foreach (DataColumn col in datos.Columns)
                {
                    long dvvColumna = 0;
                    foreach (DataRow fila in datos.Rows)
                    {
                        dvvColumna += ConvertirValorHex_43BO(fila[col]);
                    }
                    dvvTabla += dvvColumna;
                }

                lista.Add(new DV_43BO
                {
                    NombreTabla_43BO = tabla,
                    DVH_43BO = dvhTabla,
                    DVV_43BO = dvvTabla
                });

                dvhBD += dvhTabla;
                dvvBD += dvvTabla;
            }

            // la fila especial con el dv de toda la bd
            lista.Add(new DV_43BO
            {
                NombreTabla_43BO = NombreTotalBD_43BO,
                DVH_43BO = dvhBD,
                DVV_43BO = dvvBD
            });

            return lista;
        }

        // convierte el valor de una celda a su equivalente hexadecimal y devuelve la suma
        // cada caracter se pasa a su codigo hexa y se suman todos
        private long ConvertirValorHex_43BO(object valor)
        {
            if (valor == null || valor == DBNull.Value) return 0;

            string texto;

            if (valor is DateTime)
            {
                // formato fijo para que no me cambie el calculo segun la config regional de la compu
                texto = ((DateTime)valor).ToString("yyyyMMddHHmmss", CultureInfo.InvariantCulture);
            }
            else if (valor is bool)
            {
                texto = ((bool)valor) ? "1" : "0";
            }
            else
            {
                texto = Convert.ToString(valor, CultureInfo.InvariantCulture).Trim();
            }

            long suma = 0;
            foreach (char c in texto)
            {
                // el equivalente hexa del caracter (ej: 'A' -> "41" -> 65)
                string hex = ((int)c).ToString("X");
                suma += Convert.ToInt64(hex, 16);
            }
            return suma;
        }

        // calcula el dv y lo guarda en la tabla dv
        public void GenerarYPersistirDV_43BO()
        {
            List<DV_43BO> dvs = CalcularDV_43BO();

            _persistiendoDV = true;
            try
            {
                acceso.Escribir_43BO("DELETE FROM DV_43BO");

                foreach (DV_43BO dv in dvs)
                {
                    SqlParameter[] parametros =
                    {
                        new SqlParameter("@tabla", dv.NombreTabla_43BO),
                        new SqlParameter("@dvh", dv.DVH_43BO),
                        new SqlParameter("@dvv", dv.DVV_43BO)
                    };
                    acceso.Escribir_43BO(
                        "INSERT INTO DV_43BO (NombreTabla_43BO, DVH_43BO, DVV_43BO) VALUES (@tabla, @dvh, @dvv)",
                        parametros);
                }
            }
            finally
            {
                _persistiendoDV = false;
            }
        }

        // esto lo llama AccesoBD_43BO despues de cada escritura
        // si la escritura fue del propio dv no recalcula (sino loop infinito)
        public static void RecalcularTrasPersistencia_43BO()
        {
            if (_persistiendoDV) return;
            new DALdv_43BO().GenerarYPersistirDV_43BO();
        }

        // -------------------- revision --------------------

        // el select de la tabla dv para comparar contra lo calculado
        public List<DV_43BO> ConsultarDV_43BO()
        {
            List<DV_43BO> lista = new List<DV_43BO>();

            DataTable tabla = acceso.Leer_43BO("SELECT NombreTabla_43BO, DVH_43BO, DVV_43BO FROM DV_43BO");

            foreach (DataRow fila in tabla.Rows)
            {
                lista.Add(new DV_43BO
                {
                    NombreTabla_43BO = fila["NombreTabla_43BO"].ToString().Trim(),
                    DVH_43BO = Convert.ToInt64(fila["DVH_43BO"]),
                    DVV_43BO = Convert.ToInt64(fila["DVV_43BO"])
                });
            }
            return lista;
        }

        // -------------------- backup / restore --------------------

        // ojo: el .bak lo escribe sql server, no la app, asi que la carpeta
        // tiene que tener permisos para el servicio de sql
        public void BackupBD_43BO(string rutaArchivo)
        {
            SqlConnectionStringBuilder builder =
                new SqlConnectionStringBuilder(AccesoBD_43BO.ObtenerCadenaConexion_43BO());
            string nombreBD = builder.InitialCatalog;

            using (SqlConnection con = new SqlConnection(builder.ConnectionString))
            {
                using (SqlCommand cm = new SqlCommand(
                    "BACKUP DATABASE [" + nombreBD + "] TO DISK = @ruta WITH INIT", con))
                {
                    cm.CommandTimeout = 300;
                    cm.Parameters.Add(new SqlParameter("@ruta", rutaArchivo));
                    con.Open();
                    cm.ExecuteNonQuery();
                }
            }
        }

        public void RestoreBD_43BO(string rutaArchivo)
        {
            SqlConnectionStringBuilder builder =
                new SqlConnectionStringBuilder(AccesoBD_43BO.ObtenerCadenaConexion_43BO());
            string nombreBD = builder.InitialCatalog;

            // el restore se hace conectado a master pq no podes restaurar la bd que estas usando
            builder.InitialCatalog = "master";

            using (SqlConnection con = new SqlConnection(builder.ConnectionString))
            {
                con.Open();

                // corto las conexiones activas sino el restore no arranca nunca
                using (SqlCommand cm = new SqlCommand(
                    "ALTER DATABASE [" + nombreBD + "] SET SINGLE_USER WITH ROLLBACK IMMEDIATE", con))
                {
                    cm.CommandTimeout = 300;
                    cm.ExecuteNonQuery();
                }

                try
                {
                    using (SqlCommand cm = new SqlCommand(
                        "RESTORE DATABASE [" + nombreBD + "] FROM DISK = @ruta WITH REPLACE", con))
                    {
                        cm.CommandTimeout = 600;
                        cm.Parameters.Add(new SqlParameter("@ruta", rutaArchivo));
                        cm.ExecuteNonQuery();
                    }
                }
                finally
                {
                    using (SqlCommand cm = new SqlCommand(
                        "ALTER DATABASE [" + nombreBD + "] SET MULTI_USER", con))
                    {
                        cm.CommandTimeout = 300;
                        cm.ExecuteNonQuery();
                    }
                }
            }

            // limpio el pool para que no queden conexiones apuntando a la bd vieja
            SqlConnection.ClearAllPools();
        }
    }
}
