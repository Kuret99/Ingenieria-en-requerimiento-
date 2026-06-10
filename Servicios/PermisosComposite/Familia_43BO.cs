using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class Familia_43BO : Rol_43BO
    {
        private List<Rol_43BO> _hijos = new List<Rol_43BO>();

        public List<Rol_43BO> ObtenerHijos_43BO() { return this._hijos; }

        public List<Rol_43BO> ObtenerTodosLosDescendientes_43BO()
        {
            var lista = new List<Rol_43BO>();
            foreach (var hijo in _hijos)
            {
                lista.Add(hijo); // Agregamos el hijo directo
                if (hijo is Familia_43BO subFamilia)
                {
                    // Agregamos recursivamente todo lo que tenga el hijo
                    lista.AddRange(subFamilia.ObtenerTodosLosDescendientes_43BO());
                }
            }
            return lista;
        }


        public bool BuscarJerarquia_43BO(Rol_43BO hijoBuscado)
        {
            foreach (var hijo in _hijos)
            {
                if (hijo.IdRol_43BO == hijoBuscado.IdRol_43BO && hijo.GetType() == hijoBuscado.GetType())
                    return true;

                if (hijo is Familia_43BO subFamilia)
                {
                    if (subFamilia.BuscarJerarquia_43BO(hijoBuscado))
                        return true;
                }
            }
            return false;
        }

        public void Agregar_43BO(Rol_43BO hijo)
        {
            // 1. Obtener todos los descendientes actuales del PADRE (yo mismo)
            var misDescendientes = this.ObtenerTodosLosDescendientes_43BO();

            // 2. Obtener todos los descendientes del HIJO que queremos agregar
            var descendientesDelHijo = new List<Rol_43BO>();
            descendientesDelHijo.Add(hijo); // El hijo mismo es parte del conflicto
            if (hijo is Familia_43BO fHijo)
                descendientesDelHijo.AddRange(fHijo.ObtenerTodosLosDescendientes_43BO());

            // 3. COMPARAR: ¿Alguno de los elementos que voy a agregar ya está en mi lista?
            foreach (var item in descendientesDelHijo)
            {
                // Buscamos si existe alguno con el mismo ID y Tipo
                bool existe = misDescendientes.Any(d => d.IdRol_43BO == item.IdRol_43BO && d.GetType() == item.GetType());

                if (existe)
                {
                    throw new Exception($"No se puede agregar '{hijo.Nombre_43BO}' porque contiene elementos ('{item.Nombre_43BO}') que ya están presentes en la estructura actual.");
                }
            }

            // 4. Si pasamos todas las pruebas, agregamos
            _hijos.Add(hijo);
        }

        public override List<string> ObtenerPermisos_43BO()
        {
            List<string> listaTotal = new List<string>();
            foreach (var hijo in _hijos)
                listaTotal.AddRange(hijo.ObtenerPermisos_43BO());
            return listaTotal.Distinct().ToList();
        }

        public void LimpiarHijos_43BO() => this._hijos.Clear();
        public void AgregarHijo_43BO(Rol_43BO hijo) => this._hijos.Add(hijo);
    }
}