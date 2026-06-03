using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class Familia_43BO : Rol_43BO
    {
        // Esta lista guarda los hijos (ya sean otras familias o patentes individuales)
        private List<Rol_43BO> _hijos = new List<Rol_43BO>();

        public void Agregar(Rol_43BO hijo)
        {
            // Validamos que el hijo no contenga permisos que ya tengamos
            List<string> permisosActuales = this.ObtenerPermisos_43BO();
            List<string> nuevosPermisos = hijo.ObtenerPermisos_43BO();

            foreach (var p in nuevosPermisos)
            {
                if (permisosActuales.Contains(p))
                {
                    throw new Exception($"Conflicto: El permiso '{p}' ya existe en esta jerarquía.");
                }
            }

            _hijos.Add(hijo);
        }

        public override List<string> ObtenerPermisos_43BO()
        {
            List<string> listaTotal = new List<string>();

            // Recursividad: le pedimos a cada hijo sus permisos
            foreach (var hijo in _hijos)
            {
                listaTotal.AddRange(hijo.ObtenerPermisos_43BO());
            }

            // Distinct asegura que no haya duplicados en el nivel superior
            return listaTotal.Distinct().ToList();
        }
    }
}
