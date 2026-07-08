using System;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;

namespace DAL
{
    // se encarga de saber si la base existe y de crearla corriendo el EsquemaCompleto_43BO.sql.
    // se conecta a "master" con la instancia elegida por el usuario en el primer arranque.
    public static class InstaladorBD_43BO
    {
        public const string NOMBRE_BD = "Ing.Software";

        // para diagnostico: la cadena que realmente esta usando la app (App.config / default)
        public static string CadenaActual
        {
            get { return AccesoBD_43BO.ConnectionString; }
        }

        private static string ConnMaster(string instancia)
        {
            return $"Data Source={instancia};Initial Catalog=master;Integrated Security=True";
        }

        public static bool ExisteBaseDatos(string instancia)
        {
            using (var conn = new SqlConnection(ConnMaster(instancia)))
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "SELECT COUNT(1) FROM sys.databases WHERE name = @n", conn))
                {
                    cmd.Parameters.AddWithValue("@n", NOMBRE_BD);
                    return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
            }
        }

        // Punto de entrada del primer arranque (silencioso): toma la instancia que ya resuelve
        // AccesoBD (App.config / default) y, si la base no existe, la crea corriendo el script.
        public static void AsegurarBaseDatos()
        {
            string instancia;
            try
            {
                instancia = new SqlConnectionStringBuilder(AccesoBD_43BO.ConnectionString).DataSource;
            }
            catch
            {
                instancia = @".\SQLEXPRESS";
            }

            if (!ExisteBaseDatos(instancia))
            {
                InstalarBaseDatos(instancia);
            }
        }

        // corre el script completo (esquema + datos). El script crea la base si no existe
        // y luego hace USE [Ing.Software]; por eso se ejecuta todo sobre la MISMA conexion.
        public static void InstalarBaseDatos(string instancia)
        {
            string rutaScript = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory, "EsquemaCompleto_43BO.sql");

            if (!File.Exists(rutaScript))
                throw new Exception("No se encontro el script de instalacion: " + rutaScript);

            string script = File.ReadAllText(rutaScript);

            // SqlCommand no entiende "GO"; separo el script en lotes por las lineas GO
            var lotes = Regex.Split(script, @"^\s*GO\s*$",
                RegexOptions.Multiline | RegexOptions.IgnoreCase);

            using (var conn = new SqlConnection(ConnMaster(instancia)))
            {
                conn.Open();
                foreach (var lote in lotes)
                {
                    if (string.IsNullOrWhiteSpace(lote)) continue;
                    using (var cmd = new SqlCommand(lote, conn) { CommandTimeout = 120 })
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
