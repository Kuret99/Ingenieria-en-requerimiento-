using BLL;
using Servicios;
using Servicios.IidiomaObserver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Proyecto_IngSoftware
{
    // gui del restore, lista los backups (el mas nuevo primero) y restaura el elegido
    public partial class RestoreBD_43BO : Form, IdiomaObserver_43BO
    {
        private BllDV_43BO bllDv_43BO = new BllDV_43BO();
        private Dictionary<string, string> _dic;

        // admin que dispara el restore (viene del login, todavia sin sesion) para firmar el log
        private readonly User_43BO _responsable;

        public RestoreBD_43BO(User_43BO responsable = null)
        {
            _responsable = responsable;
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
            this.Text = ObtenerTexto("restore_titulo", "Restore BD");
            lblTitulo.Text = ObtenerTexto("restore_lbl_titulo", "Restore de la BD");
            lblIndicacion.Text = ObtenerTexto("restore_lbl_indicacion", "Elija el backup a restaurar. Se recomienda el más reciente para perder la mínima cantidad de datos:");
            btnRestaurar.Text = ObtenerTexto("restore_btn_restaurar", "Restaurar");
            btnBuscar.Text = ObtenerTexto("restore_btn_buscar", "Buscar otro...");
            btnCancelar.Text = ObtenerTexto("restore_btn_cancelar", "Cancelar");
        }

        private void RestoreBD_43BO_Load(object sender, EventArgs e)
        {
            CargarBackups_43BO();
        }

        private void CargarBackups_43BO()
        {
            lstBackups.Items.Clear();

            foreach (string archivo in bllDv_43BO.ListarBackups_43BO())
            {
                lstBackups.Items.Add(archivo);
            }

            if (lstBackups.Items.Count > 0)
            {
                // dejo seleccionado el mas nuevo asi se pierde lo minimo posible
                lstBackups.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show(string.Format(ObtenerTexto("restore_msg_sin_backups",
                                    "No se encontraron backups en {0}. Puede buscar un .bak en otra carpeta con el botón 'Buscar otro...'"),
                                    BllDV_43BO.ObtenerRutaBackups_43BO()),
                                ObtenerTexto("restore_titulo", "Restore BD"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // por si el backup esta en otra carpeta o en un pendrive
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialogo = new OpenFileDialog())
            {
                dialogo.Title = ObtenerTexto("restore_dialogo_titulo", "Elegir archivo de backup");
                dialogo.Filter = "Backup SQL Server (*.bak)|*.bak";
                dialogo.InitialDirectory = BllDV_43BO.ObtenerRutaBackups_43BO();

                if (dialogo.ShowDialog(this) == DialogResult.OK)
                {
                    if (!lstBackups.Items.Contains(dialogo.FileName))
                    {
                        lstBackups.Items.Add(dialogo.FileName);
                    }
                    lstBackups.SelectedItem = dialogo.FileName;
                }
            }
        }

        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            if (lstBackups.SelectedItem == null)
            {
                MessageBox.Show(ObtenerTexto("restore_msg_seleccione", "Seleccione un backup para restaurar."),
                                ObtenerTexto("restore_titulo", "Restore BD"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string ruta = lstBackups.SelectedItem.ToString();

            // aviso antes porque esto pisa la bd entera
            DialogResult confirmar = MessageBox.Show(
                string.Format(ObtenerTexto("restore_msg_confirmar",
                    "Se restaurará la base de datos desde: {0}. Se perderán los datos cargados después de ese backup. ¿Desea continuar?"),
                    Path.GetFileName(ruta)),
                ObtenerTexto("titulo_atencion", "Atención"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirmar != DialogResult.Yes) return;

            try
            {
                Cursor = Cursors.WaitCursor;
                bllDv_43BO.RestaurarBackup_43BO(ruta, _responsable);
                Cursor = Cursors.Default;

                MessageBox.Show(ObtenerTexto("restore_msg_exito", "Restore ejecutado correctamente. Vuelva a iniciar sesión."),
                                ObtenerTexto("restore_titulo", "Restore BD"), MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK; // se limpia y vuelve al login
                this.Close();
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MessageBox.Show(ObtenerTexto("restore_error", "Error al ejecutar el restore: ") + ObtenerTexto(ex.Message, ex.Message),
                                ObtenerTexto("titulo_error", "Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void RestoreBD_43BO_FormClosed(object sender, FormClosedEventArgs e)
        {
            // me desuscribo para no dejar referencias colgadas en el gestor
            GestorIdioma_43BO.Instancia.Desuscribir_43BO(this);
        }
    }
}
