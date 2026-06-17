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


      
        public int Escribir_43BO(string comandoText, SqlParameter[] parametros = null, CommandType tipoComando = CommandType.Text)
        {
            using (SqlConnection con = new SqlConnection(_cadenaConexion))
            {
                using (SqlCommand cm = new SqlCommand(comandoText, con))
                {
                    cm.CommandType = tipoComando; //aca por dfecto viene query si no es un Sp

                    if (parametros != null) cm.Parameters.AddRange(parametros);
                    con.Open();
                    return cm.ExecuteNonQuery();
                }
            }
        }

        public DataTable Leer_43BO(string comandoText, SqlParameter[] parametros = null, CommandType tipoComando = CommandType.Text)
        {
            DataTable tabla = new DataTable();
            using (SqlConnection conexion = new SqlConnection(_cadenaConexion))
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
            using (SqlConnection con = new SqlConnection(_cadenaConexion))
            {
                using (SqlCommand cm = new SqlCommand(comandoText, con))
                {
                    cm.CommandType = tipoComando;
                    if (parametros != null) cm.Parameters.AddRange(parametros);
                    con.Open();
                    return Convert.ToInt32(cm.ExecuteScalar());
                }
            }
        }
    }
}


