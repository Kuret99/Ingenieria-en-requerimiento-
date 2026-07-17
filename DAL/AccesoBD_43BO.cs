using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DAL
{
    public class AccesoBD_43BO
    {
        // Cadena de conexion resuelta siempre desde el App.config (con fallback al default
        // local .\SQLEXPRESS). Toda la DAL se conecta usando esto. Es de solo lectura: no hay
        // flujo de instalacion con seleccion manual de instancia, el instalador (BLLInstalador_43BO
        // / InstaladorBD_43BO) usa esta misma cadena tal cual la resuelve el App.config.
        public static string ConnectionString
        {
            get { return ObtenerCadenaDeConfig_43BO(); }
        }

        // lee la cadena del App.config; si no esta o esta vacia, usa la instancia local por defecto.
        private static string ObtenerCadenaDeConfig_43BO()
        {
            try
            {
                var cs = System.Configuration.ConfigurationManager.ConnectionStrings["Ing.Software"];
                if (cs != null && !string.IsNullOrWhiteSpace(cs.ConnectionString))
                {
                    // Blindaje: a veces OneDrive restaura el App.config ORIGINAL con el placeholder
                    // "Data Source=Usuario" (una instancia que no existe). Si aparece ese valor basura,
                    // lo ignoro y uso el default local, asi la app arranca igual.
                    if (cs.ConnectionString.IndexOf("Data Source=Usuario", StringComparison.OrdinalIgnoreCase) < 0)
                    {
                        return cs.ConnectionString;
                    }
                }
            }
            catch
            {
                // si el .config esta roto o falta, sigo con el default de abajo
            }

            // default: instancia local SQLEXPRESS de la misma maquina
            return @"Data Source=.\SQLEXPRESS;Initial Catalog=Ing.Software;Integrated Security=True";
        }


        public AccesoBD_43BO()
        {

        }

        // esto lo necesita DALdv_43BO para armar la conexion a master en el backup/restore
        public static string ObtenerCadenaConexion_43BO()
        {
            return ConnectionString;
        }


        public int Escribir_43BO(string comandoText, SqlParameter[] parametros = null, CommandType tipoComando = CommandType.Text)
        {
            int filasAfectadas;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cm = new SqlCommand(comandoText, con))
                {
                    cm.CommandType = tipoComando; //aca por dfecto viene query si no es un Sp

                    if (parametros != null) cm.Parameters.AddRange(parametros);
                    con.Open();
                    filasAfectadas = cm.ExecuteNonQuery();
                }
            }

            // cada vez que se escribe algo en la bd se recalcula el dv entero
            DALdv_43BO.RecalcularTrasPersistencia_43BO();

            return filasAfectadas;
        }

        public DataTable Leer_43BO(string comandoText, SqlParameter[] parametros = null, CommandType tipoComando = CommandType.Text)
        {
            DataTable tabla = new DataTable();
            using (SqlConnection conexion = new SqlConnection(ConnectionString))
            {
                using (SqlCommand comando = new SqlCommand(comandoText, conexion))
                {
                    comando.CommandType = tipoComando; //lo mismo

                    if (parametros != null) comando.Parameters.AddRange(parametros);
                    SqlDataAdapter adapter = new SqlDataAdapter(comando);
                    adapter.Fill(tabla);
                }
            }
            return tabla;
        }

        public int EjecutarScalar_43BO(string comandoText, SqlParameter[] parametros = null, CommandType tipoComando = CommandType.Text)
        {
            int resultado;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cm = new SqlCommand(comandoText, con))
                {
                    cm.CommandType = tipoComando;
                    if (parametros != null) cm.Parameters.AddRange(parametros);
                    con.Open();
                    resultado = Convert.ToInt32(cm.ExecuteScalar());
                }
            }

            // los sp de insercion tambien escriben asi que aca tambien recalculo
            DALdv_43BO.RecalcularTrasPersistencia_43BO();

            return resultado;
        }
    }
}


