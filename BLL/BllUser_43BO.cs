using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicios; 
using DAL;

namespace BLL
{
    public class BllUser_43BO
    {
        DALUser_43BO DALuser = new DALUser_43BO();
 

        public void InsertarUser_43BO(int dni, string nom, string ape, string rol,string email) 
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

        public List<User_43BO> ListarUsuarios_43BO()
        {
            //de aca retorna la lista de usarios que fue cargado con el .fill del aadapter
          
            return DALuser.ListarUsuarios_43BO();
        }
    }
}
