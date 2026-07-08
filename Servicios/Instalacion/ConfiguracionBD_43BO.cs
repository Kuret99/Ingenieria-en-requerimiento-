using System;
using System.IO;

namespace Servicios.Instalacion
{
    // guarda y lee la instancia de SQL elegida en un archivo "conexion.cfg" al lado del exe.
    // asi la app pregunta una sola vez y las proximas veces arranca directo.
    public static class ConfiguracionBD_43BO
    {
        public const string NombreBD = "Ing.Software";

        private static string RutaArchivo
        {
            get { return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "conexion.cfg"); }
        }

        public static string LeerInstanciaGuardada()
        {
            if (!File.Exists(RutaArchivo)) return null;
            string c = File.ReadAllText(RutaArchivo).Trim();
            return string.IsNullOrEmpty(c) ? null : c;
        }

        public static void GuardarInstancia(string instancia)
        {
            File.WriteAllText(RutaArchivo, instancia ?? "");
        }

        public static string ArmarConnectionString(string instancia, string bd = NombreBD)
        {
            return $"Data Source={instancia};Initial Catalog={bd};Integrated Security=True";
        }
    }
}
