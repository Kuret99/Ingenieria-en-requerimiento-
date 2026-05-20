using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class Rol_43BO
    {
       
            public int IdRol_43BO { get; set; }
            public string Nombre_43BO { get; set; }

        public List<Rol_43BO> Hijos { get; set; } = new List<Rol_43BO>();

       
        public List<string> ObtenerPermisos_43BO()
        {
            List<string> listaPermisos = new List<string>();

         

            // simulo los permisoso del compisite con esta lista
            if (this.Nombre_43BO.Trim() == "Administrador")
            {
                //admin  tiene acceso a todos los permisos individuales
                listaPermisos.Add("Admin");
                listaPermisos.Add("Master");
                listaPermisos.Add("Venta");
                listaPermisos.Add("Stock");
            }
            else if (this.Nombre_43BO.Trim() == "Básico")
            { // el basico dsolo tendria esos prmiso
                listaPermisos.Add("Venta");
                listaPermisos.Add("Stock");
            }

            return listaPermisos;

        }

    }
}
