using DAL;
using Servicios;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class BllUser_43BO
    {
        private DALUser_43BO DALuser = new DALUser_43BO();
        private BLLBitacora_43BO bllBi = new BLLBitacora_43BO();

        // cf de contador de fallos y ultIntentos para poder reiniciar el contador deepues de X contador de tiempo 
        private static Dictionary<string, int> cf = new Dictionary<string, int>();

        private static Dictionary<string, DateTime> ultIntentos = new Dictionary<string, DateTime>();

        public void CambiarContraseña_43BO(string usaername, string contraAc, string contraNu, string confi) 
        {
            User_43BO usuario = DALuser.BuscarUserName_43BO(usaername);

            if (usuario == null)
            { 
                throw new Exception("Usuario no encontrado"); 
            }

            string contraActual = CriptoManager_43BO.GenerarHash_43BO(contraAc);
            if (usuario.Hash_43BO != contraActual) 
            {
            throw new Exception("Contraseña incorrecta");
            }

            if (contraNu != confi)
            {
                throw new Exception("La nueva contraseña y la confirmación no coinciden");
            }
            else 
            {
            string contraNueva = CriptoManager_43BO.GenerarHash_43BO(contraNu);
            DALuser.CambiarContraseña_43BO(usaername, contraNueva);
            throw new Exception("Contraseña cambiada exitosamente");
            }

        }


        public bool ValidarLogin_43BO(string UserName, string ContraDefault)
        {
            //traigo esto para poder laburar con los atributos e ir apsando de validacion en validacion 
            User_43BO usaurio = DALuser.BuscarUserName_43BO(UserName);

            if (usaurio == null)
            {
                throw new Exception("Usuario o contraseña incorrectas");
            }

            if (usaurio.Bloqueado_43BO)
            {
                throw new Exception("Usuario bloqueado. Por favor, contacte al administrador.");
            }


            string contra = CriptoManager_43BO.GenerarHash_43BO(ContraDefault);


            if (usaurio.Hash_43BO == contra)
            {
                ReiniciarIn_43BO(UserName);


                SessionManager_43BO.IniciarSesion_43BO(usaurio);

                bllBi.GuardarLog_43BO(usaurio, Modulo_43BO.Usuario, Evento_43BO.Login, 1); // Log de éxito de login

                return true; // Login exitoso
            }
            else
            {
                // aca usamos usuario y user name porque si llega  fallar se le suma al intento al username que puso ese usuario y no al usuario en base de da
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

                throw new Exception("Usuario bloqueado por múltiples intentos fallidos, contacte al administrador.");
            }
            int intentosRestantes = 3 - cf[username];
            throw new Exception($"Quedan:{intentosRestantes} intentos restantezs  ");
        }

        private void ReiniciarIn_43BO(string username)
        {
            if (cf  .ContainsKey(username)) cf.Remove(username);
            if (ultIntentos.ContainsKey(username)) ultIntentos.Remove(username);
        }

        public void CerrarSesion_43BO()
        {
          
            var user = SessionManager_43BO.Instancia.Usuario;

            if (user != null)
            {
                
                
                bllBi.GuardarLog_43BO(user,Modulo_43BO.Usuario,Evento_43BO.Logout, 1);
            }

          
            SessionManager_43BO.CerrarSesion_43BO();
        }

        public void InsertarUser_43BO(int dni, string nom, string ape, string rol, string email)
        {
            string contraseñaPlana = dni.ToString() + ape.Trim();

            string contraseñaDefault = CriptoManager_43BO.GenerarHash_43BO(contraseñaPlana);

            User_43BO usuario = new User_43BO();


            usuario.DNI_43BO = dni;
            usuario.Nombre_43BO = nom;
            usuario.Apellido_43BO = ape;
            usuario.Rol_43BO = rol;
            usuario.Email_43BO = email;
            usuario.Hash_43BO = contraseñaDefault;
            usuario.Bloqueado_43BO = false; // Por defecto no bloqueado
            usuario.Activo_43BO = true; // Por defecto activo

            DALuser.InsertarUser_43BO(usuario);

        }

        public int ModificarUser_43BO(int dni, string rol, string email)
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
                    throw new Exception("No se pudo actualizar el estado en la base de datos.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DesbloquearUser_43BO(int dni)
        {
            DALuser.DesbloquearUser_43BO(dni);

            User_43BO admin = SessionManager_43BO.Instancia.Usuario;

            bllBi.GuardarLog_43BO(admin, Modulo_43BO.Usuario, Evento_43BO.Desbloqueo, 2); // Log de desbloqueo de usuario

        }

        public List<User_43BO> ListarUsuarios_43BO()
        {
            //de aca retorna la lista de usarios que fue cargado con el .fill del aadapter

            return DALuser.ListarUsuarios_43BO();
        }
    }
}
