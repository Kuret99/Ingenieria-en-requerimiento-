using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class GestorArchivosIdioma_43BO
{
    public static string ObtenerContenidoJson_43BO(string codigoIdioma)
    {
   
        string ruta_43BO = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Idiomas", $"{codigoIdioma}.json");

        if (!File.Exists(ruta_43BO))
            ruta_43BO = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Idiomas", "es.json"); // Default

        return File.ReadAllText(ruta_43BO);
    }
}