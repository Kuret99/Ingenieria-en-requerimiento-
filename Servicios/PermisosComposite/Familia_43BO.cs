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
                lista.Add(hijo); //agrega el hijo directo
                if (hijo is Familia_43BO subFamilia)
                {
                  
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
           
            var misDescendientes = this.ObtenerTodosLosDescendientes_43BO();

         
            var nuevoArbol = new List<Rol_43BO> { hijo };
            if (hijo is Familia_43BO fHijo)
                nuevoArbol.AddRange(fHijo.ObtenerTodosLosDescendientes_43BO());

        
            foreach (var item in nuevoArbol)
            {
                // Si el ítem a agregar ya está en mis descendientes, o es el mismo padre
                if (misDescendientes.Any(d => d.IdRol_43BO == item.IdRol_43BO && d.GetType() == item.GetType())
                    || (this.IdRol_43BO == item.IdRol_43BO && this.GetType() == item.GetType()))
                {
                    string msgBase = GestorIdioma_43BO.Instancia.ObtenerTexto_43BO("gestionperfiles_error_elemento_existente");
                    throw new Exception(string.Format(msgBase, item.Nombre_43BO));
                }
            }

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