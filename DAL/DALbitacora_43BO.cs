using DAL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

public class DALBitacora_43BO
{
    AccesoBD_43BO acceso = new AccesoBD_43BO();

    public void RegistrarEvento_43BO(Bitacora_43BO log)
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
}
