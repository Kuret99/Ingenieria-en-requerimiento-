using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class SessionManager_43BO
    {
        // Variable estática para almacenar la instancia única de SesionManagger
        private static SessionManager_43BO _instancia;

        // Variable para almacenar el usuario activo reandonly object se usa para evitar que se modifique directamente desde fuera de la clase
        public static readonly object _lock = new object();

        // Propiedad para acceder           a la persona en sesión y evitar que sea modificada directamente desde fuera de la clase
        public User_43BO Usuario { get; private set; }

        public Familia_43BO PermisosUsuario { get; set; } // Propiedad para guardar el árbol


        // propiedad para verificar si hay una sesión activa, devuelve true si _instancia no es null, lo que indica que hay un usuario en sesión, y false si _instancia es null

        private SessionManager_43BO() { }



        public static SessionManager_43BO Instancia
        {
            // Propiedad estática para acceder a la instancia única de SesionManagger
            get
            {
                //// Verifica si la instancia ya ha sido creada
                //if (_instancia == null)
                //{


                //    throw new Exception("La sesión no ha sido iniciada. Por favor, inicie sesión primero.");
                //}
                return _instancia;

            }
        }

        public static void IniciarSesion_43BO(User_43BO usuario)
        {
            lock (_lock)
            {
                //if (_instancia != null)
                //{
                //    throw new Exception("Ya existe una sesión activa. Debe cerrar la anterior antes de iniciar otra.");
                //}
                //_instancia = new SessionManager_43BO();
                //_instancia.Usuario = usuario;
                if (_instancia == null)
                {
                    _instancia = new SessionManager_43BO();
                }
                _instancia.Usuario = usuario;

            }

        }
        

        public static void CerrarSesion_43BO()
        {

            lock (_lock)
            {
                _instancia = null; // Elimina la instancia actual
            }
        }

        public static bool EsUsuarioActual_43BO(string username)
        {
            // Si nadie está logueado, devuelve false
            if (_instancia == null || _instancia.Usuario == null) return false;


            return username.Trim().StartsWith(_instancia.Usuario.DNI_43BO.ToString());
        }

        public static bool VerificarSesionActiva_43BO()
        {
            return _instancia != null;
        }
        public bool TienePermiso_43BO(string nombrePermisoBuscado)
        {
            if (PermisosUsuario == null) return false;

            // Llama a tu método recursivo que obtiene la lista sin duplicados
            List<string> permisosTotales = PermisosUsuario.ObtenerPermisos_43BO();

            // Devuelve true si el permiso buscado está en esa lista
            return permisosTotales.Contains(nombrePermisoBuscado);
        }
    }
}
