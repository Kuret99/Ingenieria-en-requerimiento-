using DAL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

public class DALBitacora_43BO
{
    AccesoBD_43BO acceso = new AccesoBD_43BO();

    public void GuardarLog_43BO(Bitacora_43BO log)
    {
        string query = "INSERT INTO Bitacora_43BO (DNIuser_43BO, Fecha_43BO, Modulo_43BO, Evento_43BO, Criticidad_43BO) " +
                  "VALUES (@dni, @fecha, @modulo, @evento, @crit)";
        SqlParameter[] parametros = {
            new SqlParameter("@dni", log.log_43BO.DNI_43BO), 
            new SqlParameter("@fecha", log.Fecha_43BO),
            new SqlParameter("@modulo", log.Modulo.ToString()), 
            new SqlParameter("@evento", log.Evento.ToString()), 
            new SqlParameter("@crit", log.Criticidad_43BO)
        };

        acceso.Escribir_43BO(query, parametros);
    }

    public DataTable ListarBitacora_43BO(DateTime fInicio, DateTime fFin, string modulo) 
    {

      //  List<Bitacora_43BO> lista = new List<Bitacora_43BO>();



        string query = "SELECT B.IdEvento_43BO AS [ID], " +
                   "B.Fecha_43BO AS [Fecha y Hora], " +
                   "B.Modulo_43BO AS [Módulo], " +
                   "B.Evento_43BO AS [Evento Realizado], " +
                   "B.Criticidad_43BO AS [Criticidad], " +
                   "B.DNIuser_43BO AS [DNI Operador], " +
                   "U.Nombre_43BO AS [Nombre Usuario], " +
                   "U.Apellido_43BO AS [Apellido Usuario] " +
                   "FROM Bitacora_43BO B " +
                   "INNER JOIN Usuarios_43BO U ON B.DNIuser_43BO = U.DNI_43BO " +
                   "WHERE B.Fecha_43BO BETWEEN @fInicio AND @fFin AND B.Modulo_43BO = @modulo";

        SqlParameter[] parametros = {
        new SqlParameter("@fInicio", fInicio.Date),
        new SqlParameter("@fFin", fFin.Date.AddDays(1).AddTicks(-1)),
        new SqlParameter("@modulo", modulo)
    };

        // Obtenemos la tabla directo de la base de datos
        DataTable tabla = acceso.Leer_43BO(query, parametros);

        // Devolvemos la tabla de una, sin mapear nada
        return tabla;

        
    }


}
