using DAL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace BLL
{
    public class BllUser_43BO
    {
        private DALUser_43BO DALuser = new DALUser_43BO();
        private BLLBitacora_43BO bllBi = new BLLBitacora_43BO();
        private DALpatente_43BO dalPatente = new DALpatente_43BO();

        // cf de contador de fallos y ultIntentos para poder reiniciar el contador deepues de X contador de tiempo 
        private static Dictionary<string, int> cf = new Dictionary<string, int>();

        private static Dictionary<string, DateTime> ultIntentos = new Dictionary<string, DateTime>();

        public void CambiarContraseña_43BO(string usaername, string contraAc, string contraNu, string confi)
        {
            User_43BO usuario = DALuser.BuscarUserName_43BO(usaername);

            if (usuario == null)
            {
                throw new Exception("error_usuario_no_encontrado");
            }

            string contraActual = CriptoManager_43BO.GenerarHash_43BO(contraAc);
            if (usuario.Hash_43BO != contraActual)
            {
                throw new Exception("error_password_incorrecta");
            }

            if (contraNu != confi)
            {
                throw new Exception("error_passwords_no_coinciden");
            }
            else
            {
                string contraNueva = CriptoManager_43BO.GenerarHash_43BO(contraNu);
                DALuser.CambiarContraseña_43BO(usaername, contraNueva);
            }
        }

        public bool ValidarLogin_43BO(string UserName, string ContraDefault)
        {
            //agregamos esto aca apra el relogin
            if (SessionManager_43BO.Instancia != null && SessionManager_43BO.Instancia.Usuario != null)
            {
                throw new Exception("error_sesion_ya_activa");
            }

            User_43BO usaurio = DALuser.BuscarUserName_43BO(UserName);

            if (usaurio == null)
            {
                throw new Exception("error_login_incorrecto");
            }

            if (usaurio.Bloqueado_43BO)
            {
                throw new Exception("error_usuario_bloqueado");
            }

            if (!usaurio.Activo_43BO)
            {
                throw new Exception("error_cuenta_desactivada");
            }

            string contra = CriptoManager_43BO.GenerarHash_43BO(ContraDefault);

            if (usaurio.Hash_43BO == contra)
            {
                ReiniciarIn_43BO(UserName);
                bllBi.GuardarLog_43BO(usaurio, Modulo_43BO.Usuario, Evento_43BO.Login, 1);

                List<string> permisosDelUsuario = ObtenerPermisos_43BO(usaurio);

                SessionManager_43BO.IniciarSesion_43BO(usaurio, permisosDelUsuario, usaurio.Idioma_43BO);
                GestorIdioma_43BO.Instancia.CargarIdioma_43BO(usaurio.Idioma_43BO);

                return true;
            }
            else
            {
                ManejarFallos_43BO(usaurio, UserName);
                return false;
            }
        }

        private void ManejarFallos_43BO(User_43BO us, string username)
        {
            DateTime ahora = DateTime.Now;

            if (!cf.ContainsKey(username) || (ahora - ultIntentos[username]).TotalHours >= 2)
            {
                cf[username] = 1;
            }
            else
            {
                cf[username]++;
            }

            ultIntentos[username] = ahora;

            if (cf[username] >= 3)
            {
                DALuser.BloquearUser_43BO(username);

                bllBi.GuardarLog_43BO(us, Modulo_43BO.Usuario, Evento_43BO.Bloqueo, 3); // Log de bloqueo por intentos fallidos

                ReiniciarIn_43BO(username);// Bloqueamos al usuario después de 2 intentos fallidos

                throw new Exception("error_bloqueo_multiples_intentos");
            }
            int intentosRestantes = 3 - cf[username];
            // esto verlo porqeu es dinamico me peude romper
            throw new Exception($"error_intentos_restantes|{intentosRestantes}");
        }

        private void ReiniciarIn_43BO(string username)
        {
            if (cf.ContainsKey(username)) cf.Remove(username);
            if (ultIntentos.ContainsKey(username)) ultIntentos.Remove(username);
        }

        public void CerrarSesion_43BO()
        {
            var user = SessionManager_43BO.Instancia.Usuario;

            if (user != null)
            {
                bllBi.GuardarLog_43BO(user, Modulo_43BO.Usuario, Evento_43BO.Logout, 1);
            }

            SessionManager_43BO.CerrarSesion_43BO();
        }

        public void InsertarUser_43BO(int dni, string nom, string ape, Rol_43BO rol, string email)
        {
            var usuariosExistentes = DALuser.ListarUsuarios_43BO();

            //solo era este if bldo
            if (usuariosExistentes.Any(u => u.DNI_43BO == dni))
            {
                throw new Exception("error_dni_ya_registrado");
            }

            string contraseñaPlana = dni.ToString() + ape.Trim();

            string contraseñaDefault = CriptoManager_43BO.GenerarHash_43BO(contraseñaPlana);

            User_43BO usuario = new User_43BO();

            usuario.DNI_43BO = dni;
            usuario.Nombre_43BO = nom;
            usuario.Apellido_43BO = ape;
            usuario.Rol = rol;
            usuario.Email_43BO = email;
            usuario.Hash_43BO = contraseñaDefault;
            usuario.Bloqueado_43BO = false; // Por defecto no bloqueado
            usuario.Activo_43BO = true; // Por defecto activo

            DALuser.InsertarUser_43BO(usuario);
        }

        public int ModificarUser_43BO(int dni, Rol_43BO rol, string email)
        {
            return DALuser.ModificarUser_43BO(dni, rol, email);
        }

        public void Eliminar_43BO(int dni, bool activo)
        {
            try
            {
                int filasAfectadas = DALuser.EliminarUser_43BO(dni, activo);

                if (filasAfectadas == 0)
                {
                    throw new Exception("error_actualizar_estado");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DesbloquearUser_43BO(int dni)
        {
            //cone esto obtenengo el dni para asi rearmar la contraseña y resetearla una vez que le desbloqueemos la cuenta
            User_43BO usaurio = DALuser.ListarUsuarios_43BO().Find(u => u.DNI_43BO == dni);
            if (usaurio != null)
            {
                string contraDefault = usaurio.DNI_43BO.ToString() + usaurio.Apellido_43BO.Trim();
                string contraReset = CriptoManager_43BO.GenerarHash_43BO(contraDefault);

                DALuser.DesbloquearUser_43BO(dni, contraReset);

                User_43BO admin = SessionManager_43BO.Instancia.Usuario ?? usaurio;
                bllBi.GuardarLog_43BO(admin, Modulo_43BO.Usuario, Evento_43BO.Desbloqueo, 2);
            }
        }

        // necesito esto aca para hacer que en el menu si estas con la clave reseteada o ingresas con la cuenta de fabrica te haga cambiarla apra poder seguir
        public bool EsContraseñaDeFabrica_43BO(User_43BO usuario)
        {
            if (usuario == null) return false;

            string contraFabricaPlana = usuario.DNI_43BO.ToString() + usuario.Apellido_43BO.Trim();
            string contraFabricaHash = CriptoManager_43BO.GenerarHash_43BO(contraFabricaPlana);

            return usuario.Hash_43BO == contraFabricaHash;
        }

        public List<string> ObtenerPermisos_43BO(User_43BO usuario)
        {
            if (usuario == null || usuario.Rol == null) return new List<string>();

            List<int> idsPermisos = dalPatente.ObtenerPermisos_43BO(usuario.Rol.IdRol_43BO);

            List<string> nombresPermisos = new List<string>();

            foreach (int id in idsPermisos)
            {
                string nombre = Enum.GetName(typeof(Servicios.Permisos_43BO), id);

                if (nombre != null)
                {
                    nombresPermisos.Add(nombre);
                }
            }
            return nombresPermisos;
        }

        public void CambiarIdiomaUsuario_43BO(int dni_43BO, string nuevoIdioma_43BO)
        {
            DALuser.ActualizarIdioma_43BO(dni_43BO, nuevoIdioma_43BO);

            if (SessionManager_43BO.Instancia.Usuario != null && SessionManager_43BO.Instancia.Usuario.DNI_43BO == dni_43BO)
            {
                SessionManager_43BO.Instancia.Usuario.Idioma_43BO = nuevoIdioma_43BO;
            }
        }


        public List<User_43BO> ListarUsuarios_43BO()
        {
            //de aca retorna la lista de usarios que fue cargado con el .fill del aadapter
            return DALuser.ListarUsuarios_43BO();
        }
    }
}