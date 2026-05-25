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
        private static readonly string _cadenaConexion = "Data Source=Usuario;Initial Catalog=Ing.Software;Integrated Security=True";


        public AccesoBD_43BO()
        {
           
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

      
            using (SqlConnection conexion = new SqlConnection(_cadenaConexion))
            {
                
                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
               if (parametros != null) comando.Parameters.AddRange(parametros);

                   
                    SqlDataAdapter adapter = new SqlDataAdapter(comando);
                    adapter.Fill(tabla);
                }
            }
           
            return tabla;
        }
    }
}
