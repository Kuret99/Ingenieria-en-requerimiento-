using Servicios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DAL
{
    public class DALUser_43BO
    {
        private AccesoBD_43BO acceso = new AccesoBD_43BO();

        public void InsertarUser_43BO(User_43BO Usuario)
        {
            // Agregamos Hash_43BO, Bloqueado_43BO y Activo_43BO a la consulta
            string query = "INSERT INTO Usuarios_43BO (DNI_43BO, Nombre_43BO, Apellido_43BO, Email_43BO, Hash_43BO, Bloqueado_43BO, Activo_43BO,Rol_43BO) " +
                           "VALUES (@dni, @nom, @ape, @mail, @hash, @bloq, @act,@Rol)";

            SqlParameter[] parametros = {
          new SqlParameter("@dni", Usuario.DNI_43BO),
          new SqlParameter("@nom", Usuario.Nombre_43BO),
          new SqlParameter("@ape", Usuario.Apellido_43BO),
          new SqlParameter("@mail", Usuario.Email_43BO),
          new SqlParameter("@hash", Usuario.Hash_43BO),
          new SqlParameter("@bloq", Usuario.Bloqueado_43BO),  // El bool (false)
          new SqlParameter("@act", Usuario.Activo_43BO),       // El bool (true)
          new SqlParameter("@rol", Usuario.Rol_43BO),
    };

            acceso.Escribir_43BO(query, parametros);
        }

        public List<User_43BO> ListarUsuarios_43BO()
        {
            List<User_43BO> lista = new List<User_43BO>();

            //llamamos a leer en DalAcceso
            DataTable tabla = acceso.Leer_43BO("SELECT * FROM Usuarios_43BO");

            //  Recorremos la tabla y le llenamos con la info que le llega desde la BD
            foreach (DataRow fila in tabla.Rows)
            {
                User_43BO u = new User_43BO();
                u.DNI_43BO = Convert.ToInt32(fila["DNI_43BO"]);
                u.Nombre_43BO = fila["Nombre_43BO"].ToString();
                u.Apellido_43BO = fila["Apellido_43BO"].ToString();
                u.Email_43BO = fila["Email_43BO"].ToString();
                u.Rol_43BO = fila["Rol_43BO"].ToString();
                u.Activo_43BO = Convert.ToBoolean(fila["Activo_43BO"]);
                u.Bloqueado_43BO = Convert.ToBoolean(fila["Bloqueado_43BO"]);

                lista.Add(u); // Agregamos el objeto a la lista
            }

            return lista;
        }

        public int ModificarUser_43BO(int dni, string rol, string email)
        {
            // Solo permitimos SET de Rol y Email (preguntar si solo era eso por ahora))
            string query = "UPDATE Usuarios_43BO SET Rol_43BO = @rol, Email_43BO = @mail WHERE DNI_43BO = @dni";

            SqlParameter[] parametros = new SqlParameter[]
            {
               new SqlParameter("@dni", dni),
               new SqlParameter("@rol", rol),
               new SqlParameter("@mail", email)
            };

            AccesoBD_43BO acceso = new AccesoBD_43BO();
            return acceso.Escribir_43BO(query, parametros);
        }

        public int EliminarUser_43BO(int dni, bool activo)
        {

            // En lugar de eliminar actualizo el campo Activo_43BO a false
            string query = "UPDATE Usuarios_43BO SET Activo_43BO = @activo WHERE DNI_43BO = @dni";
            SqlParameter[] parametros = new SqlParameter[]
            {
               new SqlParameter("@dni", dni),
               new SqlParameter("@activo", activo)
            };

            return acceso.Escribir_43BO(query, parametros);
        }

        public void CambiarContraseña_43BO(string username, string Nuevacontra) 
        {
            string query = "UPDATE Usuarios_43BO SET Hash_43BO = @nuevaContra WHERE (CAST (DNI_43BO AS VARCHAR) + trim(Nombre_43BO) ) = @user";

            SqlParameter[] parametros = new SqlParameter[]
            {
               new SqlParameter("@nuevaContra", Nuevacontra),
            new SqlParameter("@user", username)

            };

            try
            {
                int filasAdectadas = acceso.Escribir_43BO(query, parametros);

                if (filasAdectadas == 0)
                {
                    throw new Exception("No se encontró el usuario para cambiar la contraseña.");
                }
            }
            catch(Exception ex)
            {
                throw new Exception("Error al cambiar la contraseña. Por favor, inténtelo de nuevo.");

            }

        }

        public User_43BO BuscarUserName_43BO(string UserName)
        {

            string query = "Select *  from Usuarios_43BO WHERE ( CAST (DNI_43BO AS VARCHAR) + trim(Nombre_43BO)) = @Username";

            SqlParameter[] parametros = new SqlParameter[]
            {
               new SqlParameter("@Username", UserName)
            };

            DataTable tabla = acceso.Leer_43BO(query, parametros);

            if (tabla.Rows.Count > 0)
            {
                DataRow fila = tabla.Rows[0];
                User_43BO u = new User_43BO();

                u.DNI_43BO = Convert.ToInt32(fila["DNI_43BO"]);
                u.Nombre_43BO = fila["Nombre_43BO"].ToString();
                u.Apellido_43BO = fila["Apellido_43BO"].ToString();
                u.Email_43BO = fila["Email_43BO"].ToString();
                u.Rol_43BO = fila["Rol_43BO"].ToString();
                u.Hash_43BO = fila["Hash_43BO"].ToString();
                u.Activo_43BO = Convert.ToBoolean(fila["Activo_43BO"]);
                u.Bloqueado_43BO = Convert.ToBoolean(fila["Bloqueado_43BO"]);
                return u;

            }
            return null; // Si no se encuentra el usuario, retornamos null 

        }
        public int BloquearUser_43BO(string username)
        {
            string query = "UPDATE Usuarios_43BO SET bloqueado_43BO = 1 where (CAST(DNI_43BO AS VARCHAR) + TRIM(Nombre_43BO) ) = @user ";

            SqlParameter[] parametros = { new SqlParameter("@user", username) };

            return acceso.Escribir_43BO(query, parametros);
        }

        public int DesbloquearUser_43BO(int dni, string contraReset)
        {
            
            string query = "UPDATE Usuarios_43BO SET Bloqueado_43BO = 0, Hash_43BO = @hash WHERE DNI_43BO = @dni";

            SqlParameter[] parametros = {
        new SqlParameter("@dni", dni),
        new SqlParameter("@hash", contraReset)
    };

            return acceso.Escribir_43BO(query, parametros);
        }
    }
}