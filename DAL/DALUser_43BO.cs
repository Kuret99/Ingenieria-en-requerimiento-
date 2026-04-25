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
    public  class DALUser_43BO
    {
        private AccesoBD_43BO acceso = new AccesoBD_43BO();

        public void InsertarUser_43BO(User_43BO unUsuario)
        {
            // Agregamos Hash_43BO, Bloqueado_43BO y Activo_43BO a la consulta
            string query = "INSERT INTO Usuarios_43BO (DNI_43BO, Nombre_43BO, Apellido_43BO, Email_43BO, Hash_43BO, Bloqueado_43BO, Activo_43BO,Rol_43BO) " +
                           "VALUES (@dni, @nom, @ape, @mail, @hash, @bloq, @act,@Rol)";

            SqlParameter[] parametros = {
         new SqlParameter("@dni", unUsuario.DNI),
          new SqlParameter("@nom", unUsuario.Nombre),
          new SqlParameter("@ape", unUsuario.Apellido),
          new SqlParameter("@mail", unUsuario.Email),
          new SqlParameter("@hash", unUsuario.Contraseña), // El hash que generaste
          new SqlParameter("@bloq", unUsuario.Bloqueado),  // El bool (false)
          new SqlParameter("@act", unUsuario.Activo),       // El bool (true)
          new SqlParameter("@rol", unUsuario.Rol),
    };

            acceso.Escribir(query, parametros);
        }

        public List<User_43BO> ListarUsuarios_43BO()
        {
            List<User_43BO> lista = new List<User_43BO>();

            // 1. Llamamos al método Leer de la clase de Acceso (que devuelve DataTable)
            DataTable tabla = acceso.Leer("SELECT * FROM Usuarios_43BO");

            // 2. Recorremos la tabla y creamos un Objeto por cada fila (El mapeo)
            foreach (DataRow fila in tabla.Rows)
            {
                User_43BO u = new User_43BO();
                u.DNI = Convert.ToInt32(fila["DNI_43BO"]);
                u.Nombre = fila["Nombre_43BO"].ToString();
                u.Apellido = fila["Apellido_43BO"].ToString();
                u.Email = fila["Email_43BO"].ToString();
                u.Rol = fila["Rol_43BO"].ToString();
                u.Activo = Convert.ToBoolean(fila["Activo_43BO"]);
                u.Bloqueado = Convert.ToBoolean(fila["Bloqueado_43BO"]);

                lista.Add(u); // Agregamos el objeto a la lista
            }

            return lista; // Este es el ":Object" (la lista de objetos) que vuelve en tu diagrama
        }
    }
}
