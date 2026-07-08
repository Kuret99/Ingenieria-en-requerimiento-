using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    // Entidad que representa una fila de la tabla DV_43BO (Digitos Verificadores)
    public class DV_43BO
    {
        public string NombreTabla_43BO { get; set; }

        public long DVH_43BO { get; set; }

        public long DVV_43BO { get; set; }
    }
}
