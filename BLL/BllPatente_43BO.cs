using DAL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public class BLLpatente_43BO
    {
        private readonly DALpatente_43BO _dal = new DALpatente_43BO();
        private readonly BLLBitacora_43BO _bllBitacora = new BLLBitacora_43BO();

        // ALTAS
        public int RegistrarFamilia_43BO(string nombre)
        {
            if (ListarTodasLasFamilias_43BO().Any(x => x.Nombre_43BO.Equals(nombre, StringComparison.OrdinalIgnoreCase)))
                throw new Exception("error_nombre_familia_existe");

            int id = _dal.RegistrarFamilia_43BO(nombre);

            _bllBitacora.GuardarLog_43BO(Modulo_43BO.Perfiles, Evento_43BO.CrearFamilia, 1);

            return id;
        }

        public int RegistrarRol_43BO(string nombre)
        {
            if (ListarTodosLosRoles_43BO().Any(x => x.Nombre_43BO.Equals(nombre, StringComparison.OrdinalIgnoreCase)))
                throw new Exception("error_nombre_rol_existe");

            int id = _dal.RegistrarRol_43BO(nombre);

            _bllBitacora.GuardarLog_43BO(Modulo_43BO.Perfiles, Evento_43BO.CrearRol, 1);

            return id;
        }

        // LISTADOS
        public List<Patente_43BO> ListarTodasLasPatentes_43BO() => _dal.ListarTodasLasPatentes_43BO();
        public List<Familia_43BO> ListarTodasLasFamilias_43BO() => _dal.ListarTodasLasFamilias_43BO();
        public List<Familia_43BO> ListarTodosLosRoles_43BO() => _dal.ListarTodosLosRoles_43BO();

        // VINCULACIONES 
        public bool AgregarComponenteHijo_43BO(Rol_43BO padre, Rol_43BO hijo, bool esModoRol)
        {
            if (padre == null || hijo == null) throw new ArgumentNullException();

            // evitq ue se agrege asi mismo
            if (padre.IdRol_43BO == hijo.IdRol_43BO)
            {
                throw new Exception("error_componente_mismo");
            }

            if (padre is Familia_43BO familiaPadre)
            {
                if (familiaPadre.ObtenerHijos_43BO().Any(x => x.IdRol_43BO == hijo.IdRol_43BO))
                {
                    throw new Exception("error_componente_duplicado_directo");
                }

                if (esModoRol) HidratarRolCompleto_43BO(familiaPadre);
                else HidratarFamiliaRecursivo_43BO(familiaPadre);

                
                HashSet<int> patentesDelPadre = new HashSet<int>();
                ObtenerTodosLosIdsPatentes_43BO(familiaPadre, patentesDelPadre);

                // 
                if (hijo is Familia_43BO familiaHijo)
                {
                    HidratarFamiliaRecursivo_43BO(familiaHijo);
                }
                HashSet<int> patentesDelHijo = new HashSet<int>();
                ObtenerTodosLosIdsPatentes_43BO(hijo, patentesDelHijo);

                // .
                foreach (int idPatente in patentesDelHijo)
                {
                    if (patentesDelPadre.Contains(idPatente))
                    {
                        // Podés usar una excepción genérica de duplicidad en la estructura
                        throw new Exception("error_patente_perfil_superior");
                    }
                }



                // ent este bloqeu recorro todas las familias existentes para ver si alguna de ellas contiene al padr
                foreach (var fam in ListarTodasLasFamilias_43BO())
                {
                    if (fam.IdRol_43BO == padre.IdRol_43BO) continue;

                    HidratarFamiliaRecursivo_43BO(fam);

                    if (ContieneComponente_43BO(fam, padre.IdRol_43BO))
                    {
                        HashSet<int> patentesDelAncestro = new HashSet<int>();
                        ObtenerTodosLosIdsPatentes_43BO(fam, patentesDelAncestro);

                        foreach (int idPatente in patentesDelHijo)
                        {
                            if (patentesDelAncestro.Contains(idPatente))
                            {
                                throw new Exception("error_patente_familia_superior");
                            }
                        }
                    }
                }
             

                // 
                if (hijo is Familia_43BO)
                {
                    if (familiaPadre.BuscarJerarquia_43BO(hijo))
                    {
                        throw new Exception("error_componente_en_jerarquia");
                    }
                }
            }

            // Si no saltó ninguna excepción, recién ahí impacta la base de datos
            bool resultado = _dal.AgregarComponenteHijo_43BO(padre, hijo, esModoRol);

            if (resultado)
            {
                var evento = esModoRol ? Evento_43BO.AsignarRol : Evento_43BO.AsignarFamilia;
                _bllBitacora.GuardarLog_43BO(Modulo_43BO.Perfiles, evento, 1);
            }

            return resultado;
        }

        public bool QuitarComponenteHijo_43BO(Rol_43BO padre, Rol_43BO hijo, bool esModoRol)
        {
            if (padre == null || hijo == null) throw new ArgumentNullException();

            if (padre is Familia_43BO familiaPadre)
            {
                if (esModoRol) HidratarRolCompleto_43BO(familiaPadre);
                else HidratarFamiliaRecursivo_43BO(familiaPadre);

                if (familiaPadre.ObtenerHijos_43BO().Count <= 1)
                {
                    throw new Exception("error_quitar_componente_minimo");
                }
            }
            

            bool resultado = _dal.QuitarComponenteHijo_43BO(padre, hijo, esModoRol);

            if (resultado)
            {
                var evento = esModoRol ? Evento_43BO.QuitarRol : Evento_43BO.QuitarFamilia;
                _bllBitacora.GuardarLog_43BO(Modulo_43BO.Perfiles, evento, 2);
            }

            return resultado;
        }

        // esto carga on recursvividad
        public void HidratarRolCompleto_43BO(Familia_43BO rolRaiz)
        {
            if (rolRaiz == null) return;
            rolRaiz.LimpiarHijos_43BO();

            foreach (var p in _dal.ObtenerPatentesRol_43BO(rolRaiz.IdRol_43BO))
            {
                rolRaiz.ObtenerHijos_43BO().Add(p);
            }

            foreach (var f in _dal.ObtenerFamiliasRol_43BO(rolRaiz.IdRol_43BO))
            {
                HidratarFamiliaRecursivo_43BO(f);
                rolRaiz.ObtenerHijos_43BO().Add(f);
            }
        }

        public void HidratarFamiliaRecursivo_43BO(Familia_43BO familiaPadre, HashSet<int> visitados = null)
        {
            if (familiaPadre == null) return;
            if (visitados == null) visitados = new HashSet<int>();

            if (visitados.Contains(familiaPadre.IdRol_43BO)) return;
            visitados.Add(familiaPadre.IdRol_43BO);

            familiaPadre.LimpiarHijos_43BO();

            // Poblamos las patentes directas sin validar duplicidad de jerarquía superior
            foreach (var p in _dal.ObtenerPatentesDeFamilia_43BO(familiaPadre.IdRol_43BO))
            {
                if (!familiaPadre.ObtenerHijos_43BO().Any(x => x.IdRol_43BO == p.IdRol_43BO))
                {
                    familiaPadre.ObtenerHijos_43BO().Add(p);
                }
            }

            // Poblamos las subfamilias recursivamente
            foreach (var f in _dal.ObtenerFamiliasHijas_43BO(familiaPadre.IdRol_43BO))
            {
                HidratarFamiliaRecursivo_43BO(f, visitados);
                if (!familiaPadre.ObtenerHijos_43BO().Any(x => x.IdRol_43BO == f.IdRol_43BO))
                {
                    familiaPadre.ObtenerHijos_43BO().Add(f);
                }
            }
        }

        // ELIMINACIÓN
        public void EliminarRol_43BO(int idRol)
        {
            var bllUser = new BllUser_43BO();

            var todosLosUsuarios = bllUser.ListarUsuarios_43BO();
            if (todosLosUsuarios.Any(u => u.Rol != null && u.Rol.IdRol_43BO == idRol))
            {
                throw new Exception("error_rol_en_uso");
            }

            _dal.EliminarRol_43BO(idRol);
            _bllBitacora.GuardarLog_43BO(Modulo_43BO.Perfiles, Evento_43BO.EliminarRol, 3);
        }

        public void EliminarFamilia_43BO(int idFamilia)
        {
            var bllUser = new BllUser_43BO();
            var todosLosUsuarios = bllUser.ListarUsuarios_43BO();
            if (todosLosUsuarios.Any(u => u.Rol != null && u.Rol.IdRol_43BO == idFamilia))
            {
                throw new Exception("error_familia_en_uso");
            }

            _dal.EliminarFamilia_43BO(idFamilia);
            _bllBitacora.GuardarLog_43BO(Modulo_43BO.Perfiles, Evento_43BO.EliminarFamilia, 3);
        }

        // MODIFICACION
        public bool ModificarNombreComponente_43BO(int id, string nuevoNombre, bool esModoRol)
        {
            var lista = esModoRol ? ListarTodosLosRoles_43BO() : ListarTodasLasFamilias_43BO();

            if (lista.Any(x => x.Nombre_43BO.Equals(nuevoNombre, StringComparison.OrdinalIgnoreCase) && x.IdRol_43BO != id))
                throw new Exception("error_nombre_en_uso");

            bool resultado = _dal.ModificarNombreComponente_43BO(id, nuevoNombre, esModoRol);

            if (resultado)
            {
                var evento = esModoRol ? Evento_43BO.ModificarRol : Evento_43BO.ModificarFamilia;
                _bllBitacora.GuardarLog_43BO(Modulo_43BO.Perfiles, evento, 2);
            }

            return resultado;
        }

        public List<string> ObtenerPermisosDeRol_43BO(int idRol)
        {
            List<int> idsPermisos = _dal.ObtenerPermisos_43BO(idRol);
            List<string> nombresPermisos = new List<string>();

            foreach (int id in idsPermisos)
            {
                string nombre = Enum.GetName(typeof(Servicios.Permisos_43BO), id);
                if (nombre != null)
                {
                    nombresPermisos.Add(nombre);
                }
            }
            return nombresPermisos;
        }

        private void ObtenerTodosLosIdsPatentes_43BO(Rol_43BO componente, HashSet<int> ids)
        {
            if (componente is Patente_43BO)
            {
                ids.Add(componente.IdRol_43BO);
            }
            else if (componente is Familia_43BO familia)
            {
                foreach (var h in familia.ObtenerHijos_43BO())
                {
                    ObtenerTodosLosIdsPatentes_43BO(h, ids);
                }
            }
        }

        private bool ContieneComponente_43BO(Rol_43BO actual, int idBuscado)
        {
            if (actual.IdRol_43BO == idBuscado) return true;

            if (actual is Familia_43BO familia)
            {
                foreach (var h in familia.ObtenerHijos_43BO())
                {
                    if (ContieneComponente_43BO(h, idBuscado)) return true;
                }
            }
            return false;
        }
    }
}