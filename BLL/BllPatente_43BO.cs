using DAL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLpatente_43BO
    {
        // Instanciamos la DAL que acabás de crear
        DALpatente_43BO dal = new DALpatente_43BO();

        // Este es el método que vas a llamar desde el Login o SessionManager
        public Familia_43BO ObtenerArbolDePermisos_43BO(int idRol)
        {
            // Creamos un "nodo raíz" (Familia) que va a contener todo el árbol de este Rol
            Familia_43BO rolRaiz = new Familia_43BO();
            rolRaiz.IdRol_43BO = idRol;

            // 1. Traemos las patentes directas que tiene este rol y las agregamos
            List<Patente_43BO> patentesDelRol = dal.ObtenerPatentesRol_43BO(idRol);
            foreach (var p in patentesDelRol)
            {
                rolRaiz.Agregar(p);
            }

            // 2. Traemos las familias directas que tiene este rol
            List<Familia_43BO> familiasDelRol = dal.ObtenerFamiliasRol_43BO(idRol);
            foreach (var f in familiasDelRol)
            {
                rolRaiz.Agregar(f);

                // Acá disparamos la recursividad para llenar cada familia por dentro
                LlenarFamiliaRecursiva_43BO(f);
            }

            return rolRaiz; // Te devuelve el árbol completo armado
        }

        // Método privado encargado de la magia de la recursividad
        private void LlenarFamiliaRecursiva_43BO(Familia_43BO familiaPadre)
        {
            // 1. Buscamos y agregamos las patentes (hojas) de esta familia
            List<Patente_43BO> patentesHijas = dal.ObtenerPatentesDeFamilia_43BO(familiaPadre.IdRol_43BO);
            foreach (var p in patentesHijas)
            {
                familiaPadre.Agregar(p);
            }

            // 2. Buscamos y agregamos las sub-familias (ramas) de esta familia
            List<Familia_43BO> familiasHijas = dal.ObtenerFamiliasHijas_43BO(familiaPadre.IdRol_43BO);
            foreach (var f in familiasHijas)
            {
                familiaPadre.Agregar(f);

                // RECURSIVIDAD: Como es una familia, volvemos a llamarnos a nosotros mismos
                LlenarFamiliaRecursiva_43BO(f);
            }
        }
    }
}
