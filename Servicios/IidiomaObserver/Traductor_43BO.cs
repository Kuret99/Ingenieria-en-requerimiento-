using Newtonsoft.Json; 
using System.Collections.Generic;

public static class Traductor_43BO
{
    public static void CambiarIdioma_43BO(string jsonContenido)
    {
        var diccionario = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonContenido);
        GestorIdioma_43BO.Instancia.Notificar_43BO(diccionario);
    }
}