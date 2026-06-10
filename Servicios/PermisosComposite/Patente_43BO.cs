using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class Patente_43BO : Rol_43BO
    {
        public override List<string> ObtenerPermisos_43BO()
        {
            // El permiso individual retorna una lista con su propio nombre
            return new List<string> { this.Nombre_43BO };
        }
    }
}
