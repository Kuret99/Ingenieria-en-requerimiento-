using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class SessionManager_43BO
    {
        private static SessionManager_43BO _instancia;
        public static readonly object _lock = new object();

        public User_43BO Usuario { get; private set; }

        //al finl nnocreo usarlo 
        //public Familia_43BO PermisosUsuario { get; set; } 

       
        public List<string> Permisos { get; private set; }
        public string idioma { get; private set; }

        private SessionManager_43BO() { }

        public static SessionManager_43BO Instancia
        {
            get { return _instancia; }
        }

        public static void IniciarSesion_43BO(User_43BO usuario, List<string> permisos, string idioma)
        {
            lock (_lock)
            {
                if (_instancia == null)
                {
                    _instancia = new SessionManager_43BO();
                }
                _instancia.Usuario = usuario;
                _instancia.Permisos = permisos;
                _instancia.idioma = idioma; 
            }
        }

        public static void CerrarSesion_43BO()
        {
            lock (_lock)
            {
                _instancia = null;
            }
        }

        //public static bool EsUsuarioActual_43BO(string username)
        //{
        //    if (_instancia == null || _instancia.Usuario == null) return false;
        //    return username.Trim().StartsWith(_instancia.Usuario.DNI_43BO.ToString());
        //}
    }
}
