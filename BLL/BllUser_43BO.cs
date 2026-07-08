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
        private BLLpatente_43BO bllPatente = new BLLpatente_43BO();

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

            // la nueva no puede ser la misma que la actual, sino no cambio nada
            if (contraNu == contraAc)
            {
                throw new Exception("error_contra_igual_actual");
            }

            // la nueva no puede ser la de fabrica (dni+apellido) sino queda en loop
            // pidiendo el cambio obligatorio cada vez que entra
            string contraFabricaPlana = usuario.DNI_43BO.ToString() + usuario.Apellido_43BO.Trim();
            if (contraNu == contraFabricaPlana)
            {
                throw new Exception("error_contra_igual_fabrica");
            }

            string contraNueva = CriptoManager_43BO.GenerarHash_43BO(contraNu);
            DALuser.CambiarContraseña_43BO(usaername, contraNueva);
        }

        // valida usuario + contraseña SIN iniciar sesion todavia.
        // devuelve el User si las credenciales son correctas; si no, maneja el contador
        // de intentos y tira las mismas excepciones de siempre (bloqueado, intentos, etc.).
        // Lo separo de iniciar sesion para que el login pueda autenticar PRIMERO y recien
        // despues chequear la integridad del DV (asi la clave incorrecta descuenta intentos
        // aunque la BD este inconsistente, en vez de tapar todo con el mensaje de DV).
        public User_43BO VerificarCredenciales_43BO(string UserName, string ContraDefault)
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

            if (usaurio.Hash_43BO != contra)
            {
                ManejarFallos_43BO(usaurio, UserName); // siempre tira excepcion (intentos / bloqueo)
                return null;                            // no se alcanza, es por las dudas
            }

            ReiniciarIn_43BO(UserName);
            return usaurio;
        }

        // arranca la sesion de un usuario YA autenticado: carga permisos, idioma y loguea el Login.
        public void CompletarLogin_43BO(User_43BO usuario)
        {
            if (usuario == null) throw new Exception("error_login_incorrecto");

            List<string> permisosDelUsuario = ObtenerPermisosDeRol_43BO(usuario);

            SessionManager_43BO.IniciarSesion_43BO(usuario, permisosDelUsuario, usuario.Idioma_43BO);
            GestorIdioma_43BO.Instancia.CargarIdioma_43BO(usuario.Idioma_43BO);
            bllBi.GuardarLog_43BO(Modulo_43BO.Usuario, Evento_43BO.Login, 1);
        }

        // login clasico (valida + inicia sesion en un solo paso). Se mantiene por compatibilidad.
        public bool ValidarLogin_43BO(string UserName, string ContraDefault)
        {
            User_43BO usuario = VerificarCredenciales_43BO(UserName, ContraDefault);
            CompletarLogin_43BO(usuario);
            return true;
        }

        // helper: true si el usuario pertenece al rol Administrador
        public bool EsAdmin_43BO(User_43BO usuario)
        {
            return usuario != null && usuario.Rol != null && usuario.Rol.Nombre_43BO != null &&
                   usuario.Rol.Nombre_43BO.Trim().Equals("Administrador", StringComparison.OrdinalIgnoreCase);
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

                // le paso el usuario bloqueado porque aca todavia no hay sesion iniciada
                bllBi.GuardarLog_43BO( Modulo_43BO.Usuario, Evento_43BO.Bloqueo, 3, us); // Log de bloqueo por intentos fallidos

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
            // el idioma se pisa en la bd recien aca al cerrar sesion (pedido del profe)
            // durante la sesion solo vive en el SessionManager
            var sesion = SessionManager_43BO.Instancia;
            if (sesion != null && sesion.Usuario != null && !string.IsNullOrEmpty(sesion.idioma))
            {
                DALuser.CambiarIdiomaUsuario_43BO(sesion.Usuario.DNI_43BO, sesion.idioma);
            }

            bllBi.GuardarLog_43BO(Modulo_43BO.Usuario, Evento_43BO.Logout, 1);

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

             
                bllBi.GuardarLog_43BO( Modulo_43BO.Usuario, Evento_43BO.Desbloqueo, 2);
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

        public List<string> ObtenerPermisosDeRol_43BO(User_43BO usuario)
        {
            if (usuario == null || usuario.Rol == null) return new List<string>();

            // delegue la responsabilidad a la clase que realmente sabe de patentes/permisos
            return bllPatente.ObtenerPermisosDeRol_43BO(usuario.Rol.IdRol_43BO);
        }

        public void CambiarIdiomaUsuario_43BO(int dni_43BO, string nuevoIdioma_43BO)
        {
            // ojo: aca ya no se escribe en la bd, solo queda en la sesion
            // la bd se actualiza recien cuando se cierra la sesion (CerrarSesion_43BO)

            if (SessionManager_43BO.Instancia.Usuario != null && SessionManager_43BO.Instancia.Usuario.DNI_43BO == dni_43BO)
            {
                SessionManager_43BO.Instancia.ActualizarIdioma(nuevoIdioma_43BO);
            }

         
            string json = GestorArchivosIdioma_43BO.ObtenerContenidoJson_43BO(nuevoIdioma_43BO);
            var nuevoDiccionario = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

            // Notificar al gestor para que actualice la UI
            GestorIdioma_43BO.Instancia.Notificar_43BO(nuevoDiccionario);
        }


        public List<User_43BO> ListarUsuarios_43BO()
        {
            //de aca retorna la lista de usarios que fue cargado con el .fill del aadapter
            return DALuser.ListarUsuarios_43BO();
        }

        // valida usuario+contraseña y chequea que el rol sea administrador
        // lo uso en el login cuando el dv da mal para decidir si muestro el form de reparacion
        // ojo que esto no inicia sesion, solo valida
        public bool EsAdministradorValido_43BO(string userName, string contra)
        {
            return ObtenerAdministradorValido_43BO(userName, contra) != null;
        }

        // igual que EsAdministradorValido_43BO pero devuelve el usuario admin validado
        // (o null si no valida). Lo necesito para el flujo de reparacion de DV en el login:
        // ahi todavia no hay sesion, asi que este usuario es el que firma los logs de
        // Recalcular DV / Restore en la bitacora.
        public User_43BO ObtenerAdministradorValido_43BO(string userName, string contra)
        {
            User_43BO usuario = DALuser.BuscarUserName_43BO(userName);

            if (usuario == null || usuario.Rol == null) return null;

            string hash = CriptoManager_43BO.GenerarHash_43BO(contra);
            if (usuario.Hash_43BO != hash) return null;

            bool esAdmin = usuario.Rol.Nombre_43BO != null &&
                   usuario.Rol.Nombre_43BO.Trim().Equals("Administrador", StringComparison.OrdinalIgnoreCase);

            return esAdmin ? usuario : null;
        }
    }
}