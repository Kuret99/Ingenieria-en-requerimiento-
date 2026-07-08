using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Sql;
using System.Diagnostics;

namespace Servicios.Instalacion
{
    // detecta instancias de SQL Server (Express, etc.) y de LocalDB de la maquina,
    // para que el usuario elija en el primer arranque en cual instalar/usar la base.
    public static class DetectorInstancias_43BO
    {
        public static List<string> DetectarInstancias()
        {
            var lista = new List<string>();

            foreach (var i in DetectarSqlServer())
                if (!lista.Contains(i)) lista.Add(i);

            foreach (var i in DetectarLocalDB())
                if (!lista.Contains(i)) lista.Add(i);

            // por las dudas, siempre ofrezco la instancia local tipica
            if (!lista.Contains(@".\SQLEXPRESS")) lista.Add(@".\SQLEXPRESS");

            return lista;
        }

        // instancias de SQL Server visibles (usa el enumerador nativo de .NET)
        private static List<string> DetectarSqlServer()
        {
            var lista = new List<string>();
            try
            {
                DataTable dt = SqlDataSourceEnumerator.Instance.GetDataSources();
                string equipoLocal = Environment.MachineName;

                foreach (DataRow fila in dt.Rows)
                {
                    string servidor = fila["ServerName"] != null ? fila["ServerName"].ToString() : "";
                    string instancia = fila["InstanceName"] != null ? fila["InstanceName"].ToString() : "";
                    if (string.IsNullOrEmpty(servidor)) continue;

                    // si es el equipo local lo normalizo a ".\Instancia" (mas confiable para conectar)
                    bool esLocal = servidor.Equals(equipoLocal, StringComparison.OrdinalIgnoreCase);
                    string prefijo = esLocal ? "." : servidor;

                    string completo = string.IsNullOrEmpty(instancia)
                        ? prefijo
                        : prefijo + "\\" + instancia;

                    lista.Add(completo);
                }
            }
            catch
            {
                // si el enumerador falla, sigo con LocalDB / default
            }
            return lista;
        }

        // instancias de LocalDB usando "sqllocaldb info"
        private static List<string> DetectarLocalDB()
        {
            var lista = new List<string>();
            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = "sqllocaldb",
                    Arguments = "info",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                };

                using (var p = Process.Start(psi))
                {
                    string salida = p.StandardOutput.ReadToEnd();
                    p.WaitForExit();

                    foreach (var linea in salida.Split('\n'))
                    {
                        string nombre = linea.Trim();
                        if (!string.IsNullOrEmpty(nombre))
                            lista.Add(@"(localdb)\" + nombre);
                    }
                }
            }
            catch
            {
                // no hay LocalDB o no esta el comando: no pasa nada
            }
            return lista;
        }
    }
}
