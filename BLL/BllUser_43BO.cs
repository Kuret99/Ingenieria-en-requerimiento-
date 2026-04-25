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

   
            usuario.DNI = dni;
            usuario.Nombre = nom;
            usuario.Apellido = ape;
            usuario.Rol = rol;
            usuario.Email = email;
            usuario.Contraseña = contraseñaDefault;
            usuario.Bloqueado= false; // Por defecto no bloqueado
            usuario.Activo = true; // Por defecto activo

            DALuser.InsertarUser_43BO(usuario);

        }

        public List<User_43BO> ListarUsuarios_43BO()
        {
            // Instanciamos la DAL y le pedimos la lista
          
            return DALuser.ListarUsuarios_43BO();
        }
    }
}
