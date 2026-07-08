using BLL;
using System;
using System.Windows.Forms;

namespace Proyecto_IngSoftware
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Primer arranque: si la base no existe en la instancia configurada, se crea sola
            // (en silencio) desde EsquemaCompleto_43BO.sql. La instancia sale del App.config
            // (o del default local si el App.config no la tiene). Sin ventanas ni formularios.
            try
            {
                BLLInstalador_43BO.AsegurarBaseDatos();
            }
            catch (Exception ex)
            {
                // muestro la cadena real que uso, para diagnosticar a que instancia intenta conectar
                MessageBox.Show(
                    "No se pudo preparar la base de datos:\n" + ex.Message +
                    "\n\n--- DIAGNOSTICO ---\nConexion usada: " + BLLInstalador_43BO.CadenaActual,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Application.Run(new Login());
        }
    }
}
