using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public abstract class Rol_43BO
    {
        public int IdRol_43BO { get; set; }
        public string Nombre_43BO { get; set; }

        // Este método obliga a todas las clases derivadas a definir cómo obtienen permisos
        public abstract List<string> ObtenerPermisos_43BO();
    }
}
