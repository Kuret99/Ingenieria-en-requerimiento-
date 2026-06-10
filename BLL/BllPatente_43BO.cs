using DAL;
using Servicios;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class BLLpatente_43BO
    {
        private readonly DALpatente_43BO _dal = new DALpatente_43BO();

        // ALTAS
       // public int RegistrarPatente_43BO(string nombre) => _dal.InsertarPatente_43BO(nombre);
        public int RegistrarFamilia_43BO(string nombre) => _dal.InsertarFamilia_43BO(nombre);
        public int RegistrarRol_43BO(string nombre) => _dal.InsertarRol_43BO(nombre);

        // LISTADOS
        public List<Patente_43BO> ListarTodasLasPatentes_43BO() => _dal.ListarTodasLasPatentes_43BO();
        public List<Familia_43BO> ListarTodasLasFamilias_43BO() => _dal.ListarTodasLasFamilias_43BO();
        public List<Familia_43BO> ListarTodosLosRoles_43BO() => _dal.ListarTodosLosRoles_43BO();

  
        public bool AgregarComponenteHijo_43BO(Rol_43BO padre, Rol_43BO hijo, bool esModoRol)
        {
            if (padre == null || hijo == null) throw new ArgumentNullException();

            if (hijo is Familia_43BO familiaHija)
            {
                // Forzamos la carga de todos sus hijos recursivamente
                HidratarFamiliaRecursivo_43BO(familiaHija);
            }
            // ---------------------------

            if (padre is Familia_43BO familiaPadre)
            {
                // Ahora sí, llamamos al método que ya tiene la lógica de validación
                familiaPadre.Agregar_43BO(hijo);
            }

            return _dal.VincularHijo_43BO(padre, hijo, esModoRol);
        }

        public bool QuitarComponenteHijo_43BO(Rol_43BO padre, Rol_43BO hijo, bool esModoRol)
        {
            return _dal.DesvincularHijo_43BO(padre, hijo, esModoRol);
        }

        // CARGA RECURSIVA
        public void HidratarRolCompleto_43BO(Familia_43BO rolRaiz)
        {
            if (rolRaiz == null) return;
            rolRaiz.LimpiarHijos_43BO();

            foreach (var p in _dal.ObtenerPatentesRol_43BO(rolRaiz.IdRol_43BO)) rolRaiz.AgregarHijo_43BO(p);
            foreach (var f in _dal.ObtenerFamiliasRol_43BO(rolRaiz.IdRol_43BO))
            {
                HidratarFamiliaRecursivo_43BO(f);
                rolRaiz.AgregarHijo_43BO(f);
            }
        }

        public void HidratarFamiliaRecursivo_43BO(Familia_43BO familiaPadre)
        {
            if (familiaPadre == null) return;
            familiaPadre.LimpiarHijos_43BO();

            foreach (var p in _dal.ObtenerPatentesDeFamilia_43BO(familiaPadre.IdRol_43BO)) familiaPadre.AgregarHijo_43BO(p);
            foreach (var f in _dal.ObtenerFamiliasHijas_43BO(familiaPadre.IdRol_43BO))
            {
                HidratarFamiliaRecursivo_43BO(f);
                familiaPadre.AgregarHijo_43BO(f);
            }
        }

        // ELIMINACIÓN
        public void EliminarRol_43BO(int idRol) => _dal.EliminarRol_43BO(idRol);
        public void EliminarFamilia_43BO(int idFamilia) => _dal.EliminarFamilia_43BO(idFamilia);

        public Familia_43BO ObtenerArbolDePermisos_43BO(int idRol)
        {
            Familia_43BO rolRaiz = new Familia_43BO();
            rolRaiz.IdRol_43BO = idRol;
            HidratarRolCompleto_43BO(rolRaiz);
            return rolRaiz;
        }
    }
}