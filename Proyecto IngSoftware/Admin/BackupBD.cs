using BLL;
using Servicios.IidiomaObserver;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Proyecto_IngSoftware
{
    // form para generar backups desde el menu admin
    // sin backups generados aca el restore no tiene de donde elegir
    public partial class BackupBD_43BO : Form, IdiomaObserver_43BO
    {
        private BllDV_43BO bllDv_43BO = new BllDV_43BO();
        private Dictionary<string, string> _dic;

        public BackupBD_43BO()
        {
            InitializeComponent();

            // me suscribo al gestor asi me llega el diccionario cargado
            GestorIdioma_43BO.Instancia.Suscribir_43BO(this);
        }

        public void ActualizarIdioma_43BO(Dictionary<string, string> dic)
        {
            _dic = dic;
            AplicarTextos();
        }

        // unico punto de traduccion: todo pasa por el gestor
        private string ObtenerTexto(string clave, string porDefecto = null)
        {
            return GestorIdioma_43BO.Instancia.ObtenerTexto_43BO(clave, porDefecto ?? clave);
        }

        private void AplicarTextos()
        {
            this.Text = ObtenerTexto("backup_titulo", "Backup BD");
            lblTitulo.Text = ObtenerTexto("backup_lbl_titulo", "Backup de la BD");
            lblLista.Text = ObtenerTexto("backup_lbl_lista", "Backups disponibles:");
            btnCambiarCarpeta.Text = ObtenerTexto("backup_btn_cambiar_carpeta", "Cambiar carpeta...");
            btnGenerar.Text = ObtenerTexto("backup_btn_generar", "Generar Backup");
            btnCerrar.Text = ObtenerTexto("backup_btn_cerrar", "Cerrar");
            MostrarCarpeta_43BO();
        }

        private void BackupBD_43BO_Load(object sender, EventArgs e)
        {
            MostrarCarpeta_43BO();
            CargarBackups_43BO();
        }

        private void MostrarCarpeta_43BO()
        {
            lblCarpeta.Text = ObtenerTexto("backup_lbl_carpeta", "Carpeta actual: ") + BllDV_43BO.ObtenerRutaBackups_43BO();
        }

        private void CargarBackups_43BO()
        {
            lstBackups.Items.Clear();
            foreach (string archivo in bllDv_43BO.ListarBackups_43BO())
            {
                lstBackups.Items.Add(archivo);
            }
        }

        // el admin elige donde guardar y queda recordado para las proximas veces
        // (se guarda en RutaBackup_43BO.txt al lado del exe)
        private void btnCambiarCarpeta_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialogo = new FolderBrowserDialog())
            {
                dialogo.Description = ObtenerTexto("backup_dialogo_carpeta", "Elija la carpeta donde se guardarán los backups de la BD");
                dialogo.SelectedPath = BllDV_43BO.ObtenerRutaBackups_43BO();

                if (dialogo.ShowDialog(this) == DialogResult.OK)
                {
                    try
                    {
                        BllDV_43BO.GuardarRutaBackups_43BO(dialogo.SelectedPath);
                        MostrarCarpeta_43BO();
                        CargarBackups_43BO();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ObtenerTexto("backup_error_carpeta", "No se pudo usar esa carpeta: ") + ObtenerTexto(ex.Message, ex.Message),
                                        ObtenerTexto("titulo_error", "Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                string archivo = bllDv_43BO.GenerarBackup_43BO();
                Cursor = Cursors.Default;

                MessageBox.Show(ObtenerTexto("backup_msg_exito", "Backup generado correctamente: ") + "\n" + archivo,
                                ObtenerTexto("backup_titulo", "Backup BD"), MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarBackups_43BO();
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MessageBox.Show(ObtenerTexto("backup_error", "Error al generar el backup: ") + ObtenerTexto(ex.Message, ex.Message),
                                ObtenerTexto("titulo_error", "Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BackupBD_43BO_FormClosed(object sender, FormClosedEventArgs e)
        {
            // me desuscribo para no dejar referencias colgadas en el gestor
            GestorIdioma_43BO.Instancia.Desuscribir_43BO(this);
        }
    }
}
