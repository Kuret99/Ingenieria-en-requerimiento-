using DAL;

namespace BLL
{
    // Fachada de instalacion para respetar capas: la UI (Program) habla SOLO con la BLL,
    // nunca directo con la DAL. Aca delego en InstaladorBD (DAL).
    public static class BLLInstalador_43BO
    {
        public static string NombreBD
        {
            get { return InstaladorBD_43BO.NOMBRE_BD; }
        }

        // para diagnostico: muestra que cadena de conexion esta usando realmente la app
        public static string CadenaActual
        {
            get { return InstaladorBD_43BO.CadenaActual; }
        }

        // Crea la base si no existe, usando la instancia configurada en AccesoBD (App.config / default).
        public static void AsegurarBaseDatos()
        {
            InstaladorBD_43BO.AsegurarBaseDatos();
        }
    }
}
