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
        private string _cadenaConexion;
        public AccesoBD_43BO()
        {
            _cadenaConexion = ConfigurationManager.ConnectionStrings["Ing.Software"].ConnectionString;
        }
        public int Escribir_43BO(string query, SqlParameter[] parametros = null)
        {
            using (SqlConnection con = new SqlConnection(_cadenaConexion))
            {
                using (SqlCommand cm = new SqlCommand(query, con))
                {
                    if (parametros != null) cm.Parameters.AddRange(parametros);

                    con.Open();
                    return cm.ExecuteNonQuery(); // Devuelve filas afectadas
                }
            }
        }

        public DataTable Leer_43BO(string query, SqlParameter[] parametros = null)
        {
            DataTable tabla = new DataTable();

            // 1. Establece la conexión usando la cadena del App.config
            using (SqlConnection conexion = new SqlConnection(_cadenaConexion))
            {
                // 2. Prepara el comando SQL
                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    // 3. Agrega parámetros si existen para evitar SQL Injection
                    if (parametros != null) comando.Parameters.AddRange(parametros);

                    // 4. Usa un Adapter para llenar el DataTable (el "DataSet" de tu diagrama)
                    SqlDataAdapter adapter = new SqlDataAdapter(comando);
                    adapter.Fill(tabla);
                }
            }
            // 5. Devuelve la tabla con los datos en bruto
            return tabla;
        }
    }
}
