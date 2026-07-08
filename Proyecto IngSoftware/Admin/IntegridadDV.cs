using BLL;
using Servicios;
using Servicios.IidiomaObserver;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Proyecto_IngSoftware
{
    // form de reparacion, solo lo ve el admin cuando el dv da mal en el login
    public partial class IntegridadDV_43BO : Form, IdiomaObserver_43BO
    {
        private BllDV_43BO bllDv_43BO = new BllDV_43BO();
        private Dictionary<string, string> _dic;

        // admin validado en el login: firma los logs de recalculo/restore porque
        // en este punto todavia no hay sesion iniciada en el SessionManager
        private readonly User_43BO _responsable;

        public IntegridadDV_43BO(List<string> tablasInconsistentes, User_43BO responsable = null)
        {
            _responsable = responsable;
            InitializeComponent();

            // me suscribo al gestor asi me llega el diccionario que ya cargo el login
            GestorIdioma_43BO.Instancia.Suscribir_43BO(this);

            if (tablasInconsistentes != null)
            {
                foreach (string tabla in tablasInconsistentes)
                {
                    lstTablas.Items.Add(tabla);
                }
            }
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
            this.Text = ObtenerTexto("integridad_titulo", "Integridad de Datos - Administrador");
            lblTitulo.Text = ObtenerTexto("integridad_lbl_titulo", "¡Inconsistencia de datos detectada!");
            lblMensaje.Text = ObtenerTexto("integridad_lbl_mensaje", "La verificación de los Dígitos Verificadores (DVH / DVV) no coincide con los valores persistidos en la BD. Elija una acción para continuar:");
            lblTablas.Text = ObtenerTexto("integridad_lbl_tablas", "Tablas inconsistentes:");
            btnRecalcular.Text = ObtenerTexto("integridad_btn_recalcular", "RECALCULAR EL DV");
            btnRestore.Text = ObtenerTexto("integridad_btn_restore", "RESTORE BD");
            btnSalir.Text = ObtenerTexto("integridad_btn_salir", "SALIR");
        }

        // aca se fuerza la generacion del dv, no arregla nada solo acepta lo que hay
        private void btnRecalcular_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                bllDv_43BO.GenerarDV_43BO(_responsable);
                Cursor = Cursors.Default;

                MessageBox.Show(ObtenerTexto("integridad_msg_recalculado", "El DV fue recalculado correctamente. Vuelva a iniciar sesión."),
                                ObtenerTexto("titulo_excelente", "Éxito"), MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK; // se limpia y vuelve al login
                this.Close();
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MessageBox.Show(ObtenerTexto("integridad_error_recalcular", "Error al recalcular el DV: ") + ObtenerTexto(ex.Message, ex.Message),
                                ObtenerTexto("titulo_error", "Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // abre el gui del restore para elegir un backup
        private void btnRestore_Click(object sender, EventArgs e)
        {
            using (RestoreBD_43BO frmRestore = new RestoreBD_43BO(_responsable))
            {
                if (frmRestore.ShowDialog(this) == DialogResult.OK)
                {
                    // restore hecho, se limpia y vuelve al login
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }

        // se sale del sistema sin resolver nada
        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void IntegridadDV_43BO_FormClosed(object sender, FormClosedEventArgs e)
        {
            // me desuscribo para no dejar referencias colgadas en el gestor
            GestorIdioma_43BO.Instancia.Desuscribir_43BO(this);
        }
    }
}
