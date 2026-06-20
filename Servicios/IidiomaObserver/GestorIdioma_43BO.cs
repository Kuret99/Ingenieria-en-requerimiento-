using Newtonsoft.Json;
using Servicios.IidiomaObserver;
using System.Collections.Generic;

public class GestorIdioma_43BO
{
    private static GestorIdioma_43BO _instancia;
    private List<IdiomaObserver_43BO> _suscriptores = new List<IdiomaObserver_43BO>();

    //almcenar el estado para que no sea null
    private Dictionary<string, string> _diccionarioActual;

    public static GestorIdioma_43BO Instancia
    {
        get
        {
            if (_instancia == null) _instancia = new GestorIdioma_43BO();
            return _instancia;
        }
    }

    private GestorIdioma_43BO() { }

    public void Suscribir_43BO(IdiomaObserver_43BO obs)
    {
        _suscriptores.Add(obs);
        if (_diccionarioActual != null) obs.ActualizarIdioma_43BO(_diccionarioActual);
    }

    public void Desuscribir_43BO(IdiomaObserver_43BO obs) => _suscriptores.Remove(obs);

    public void Notificar_43BO(Dictionary<string, string> nuevoDiccionario)
    {
        _diccionarioActual = nuevoDiccionario;
        foreach (var sub in _suscriptores)
        {
            sub.ActualizarIdioma_43BO(nuevoDiccionario);
        }
    }

    public void CargarIdioma_43BO(string codigoIdioma)
    {
       
        string json = GestorArchivosIdioma_43BO.ObtenerContenidoJson_43BO(codigoIdioma);
        var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        Notificar_43BO(dict);
    }
}