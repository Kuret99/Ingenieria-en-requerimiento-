using DAL;
using Servicios;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BLL
{
    public class BllDV_43BO
    {
        private DALdv_43BO DALdv = new DALdv_43BO();

        // para dejar registro en la bitacora de las acciones de mantenimiento
        private readonly BLLBitacora_43BO _bllBitacora = new BLLBitacora_43BO();

        // carpeta por defecto si el admin nunca eligio una
        private static readonly string _rutaPorDefecto_43BO = @"C:\Backups43BO";

        // aca me acuerdo de la carpeta que eligio el admin, queda al lado del exe
        // asi funciona en cualquier compu donde se instale o almenos deberia 
        private static readonly string _archivoConfig_43BO =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "RutaBackup_43BO.txt");

        // devuelve la carpeta que eligio el admin o la default (y la crea la primera vez)
        public static string ObtenerRutaBackups_43BO()
        {
            string ruta = _rutaPorDefecto_43BO;

            try
            {
                if (File.Exists(_archivoConfig_43BO))
                {
                    string guardada = File.ReadAllText(_archivoConfig_43BO).Trim();
                    if (!string.IsNullOrEmpty(guardada))
                    {
                        ruta = guardada;
                    }
                }
            }
            catch
            {
                // si el txt esta roto sigo con la default y listo
            }

            if (!Directory.Exists(ruta))
            {
                Directory.CreateDirectory(ruta);
            }

            return ruta;
        }

        // guarda la carpeta que eligio el admin para las proximas veces
        public static void GuardarRutaBackups_43BO(string ruta)
        {
            if (string.IsNullOrEmpty(ruta)) return;

            if (!Directory.Exists(ruta))
            {
                Directory.CreateDirectory(ruta);
            }

            File.WriteAllText(_archivoConfig_43BO, ruta);
        }

        

        // fuerza la generacion del dv, es lo mismo que pasa solo despues de cada persistencia
        // responsable: en el flujo de login (reparacion de DV) todavia no hay sesion,
        // asi que se pasa el admin validado para que el log quede a su nombre
        public void GenerarDV_43BO(User_43BO responsable = null)
        {
            DALdv.GenerarYPersistirDV_43BO();

            // recalcular el DV es una accion sensible -> criticidad alta
            _bllBitacora.GuardarLog_43BO(Modulo_43BO.Admin, Evento_43BO.RecalcularDV, 3, responsable);
        }

        // -------------------- revision --------------------

        // compara el dv calculado en el momento contra el que esta guardado en la tabla dv
        // si devuelve false en la lista queda el detalle de que tablas no coinciden
        public bool VerificarDV_43BO(out List<string> tablasInconsistentes)
        {
            tablasInconsistentes = new List<string>();

            // genero el objeto dv igual que en la generacion pero sin persistirlo
            List<DV_43BO> calculados = DALdv.CalcularDV_43BO();

            // consulto lo que hay guardado en la tabla dv con el select
            List<DV_43BO> guardados = DALdv.ConsultarDV_43BO();

            if (guardados.Count == 0)
            {
                // nunca se genero el dv, lo marco inconsistente para que el admin lo genere
                tablasInconsistentes.Add("DV_43BO (la tabla esta vacia, nunca se genero el DV)");
                return false;
            }

            // comparo el dvh y dvv de cada tabla contra lo guardado
            foreach (DV_43BO calc in calculados)
            {
                DV_43BO almacenado = guardados.FirstOrDefault(g => g.NombreTabla_43BO == calc.NombreTabla_43BO);

                if (almacenado == null ||
                    almacenado.DVH_43BO != calc.DVH_43BO ||
                    almacenado.DVV_43BO != calc.DVV_43BO)
                {
                    tablasInconsistentes.Add(calc.NombreTabla_43BO);
                }
            }

            // si hay diferencias el login se encarga de mostrar el mensaje y actuar
            return tablasInconsistentes.Count == 0;
        }

        // -------------------- reparacion --------------------

        // genera un .bak con fecha y hora en el nombre y devuelve la ruta
        // responsable opcional: el backup se dispara desde el menu (hay sesion), pero
        // se deja el parametro por consistencia con el resto de las acciones admin
        public string GenerarBackup_43BO(string carpeta = null, User_43BO responsable = null)
        {
            if (string.IsNullOrEmpty(carpeta))
            {
                carpeta = ObtenerRutaBackups_43BO();
            }
            else if (!Directory.Exists(carpeta))
            {
                Directory.CreateDirectory(carpeta);
            }

            string archivo = Path.Combine(carpeta,
                "Backup_43BO_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".bak");

            try
            {
                DALdv.BackupBD_43BO(archivo);
            }
            catch (Exception ex)
            {
                // el error tipico es que la cuenta del servicio de SQL Server no tiene
                // permiso de escritura en la carpeta (Operating system error 5). Lo traduzco
                // a una clave que el form sabe mostrar en el idioma actual.
                string m = ex.Message ?? "";
                if (m.Contains("Operating system error 5") ||
                    m.IndexOf("Access is denied", StringComparison.OrdinalIgnoreCase) >= 0 ||
                    m.IndexOf("Acceso denegado", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    throw new Exception("error_backup_acceso_denegado");
                }
                throw; // cualquier otro error se re-lanza tal cual
            }

            // el backup es benigno pero igual queda auditado -> criticidad media
            _bllBitacora.GuardarLog_43BO(Modulo_43BO.Admin, Evento_43BO.Backup, 2, responsable);

            return archivo;
        }

        // lista los .bak de la carpeta recordada, del mas nuevo al mas viejo
        public List<string> ListarBackups_43BO()
        {
            string carpeta = ObtenerRutaBackups_43BO();

            if (!Directory.Exists(carpeta))
            {
                return new List<string>();
            }

            return Directory.GetFiles(carpeta, "*.bak")
                            .OrderByDescending(f => File.GetCreationTime(f))
                            .ToList();
        }

        // restaura el backup elegido y regenera el dv para que quede todo normalizado
        // responsable: igual que en el recalculo, en el login todavia no hay sesion
        public void RestaurarBackup_43BO(string rutaArchivo, User_43BO responsable = null)
        {
            if (string.IsNullOrEmpty(rutaArchivo) || !File.Exists(rutaArchivo))
            {
                // tiro la clave y el form la traduce con el diccionario
                throw new Exception("error_backup_no_encontrado");
            }

            DALdv.RestoreBD_43BO(rutaArchivo);

          
            // (lo logueo ANTES de recalcular el DV; si logueara despues, el propio
            //  registro del restore quedaria fuera del DV recien calculado)
            _bllBitacora.GuardarLog_43BO(Modulo_43BO.Admin, Evento_43BO.RestaurarBackup, 3, responsable);

            // despues del restore recalculo el dv asi la bd restaurada queda aceptada
            DALdv.GenerarYPersistirDV_43BO();
        }
    }
}
