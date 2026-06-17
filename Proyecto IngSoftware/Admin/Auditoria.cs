using GUI_43BO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Servicios;
using Servicios.IidiomaObserver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_IngSoftware
{
    public partial class Auditoria : Form, IdiomaObserver_43BO
    {
        private BLL.BLLBitacora_43BO bllBitacora = new BLL.BLLBitacora_43BO();
        private bool estaReseteando = false;
        private Dictionary<string, string> _dic;
        
        
        private readonly Dictionary<string, Permisos_43BO> _mapaAuditoria = new Dictionary<string, Permisos_43BO>
        {
            { "btnAplicar", Permisos_43BO.Auditoria_Consultar },
            { "btnImprimir", Permisos_43BO.Auditoria_Imprimir }
        };

        public Auditoria()
        {
            InitializeComponent();
            GestorIdioma_43BO.Instancia.Suscribir_43BO(this);
        }

        private void Auditoria_Load(object sender, EventArgs e)
        {
            dtpFechaInicio.MaxDate = DateTime.MaxValue;
            dtpFechaFin.MaxDate = DateTime.MaxValue;

            ResetearComponentes_43BO();
            AsignadorPermisos_43BO.Aplicar(this, _mapaAuditoria);
        }

        // --- IMPLEMENTACIÓN DEL PATRÓN OBSERVER ---
        public void ActualizarIdioma_43BO(Dictionary<string, string> dic)
        {
            _dic = dic;

            // Traducimos los textos principales del formulario
            this.Text = GetTexto("auditoria_titulo");
            btnAplicar.Text = GetTexto("auditoria_btn_aplicar");
            btnLimpiar.Text = GetTexto("auditoria_btn_limpiar");
            btnImprimir.Text = GetTexto("auditoria_btn_imprimir");

            // Traducimos el combo dinámico de criticidad sin perder la selección actual
            PoblarComboCriticidad();

            // Traducimos las cabeceras de la grilla si es que ya tiene datos cargados
            FormatoDgvAuditoria_43BO();
        }

        // Método helper seguro para obtener los strings del JSON
        private string GetTexto(string key)
        {
            if (_dic != null && _dic.ContainsKey(key))
                return _dic[key];
            return key;
        }

        // Traduce dinámicamente el combo de criticidad conservando la experiencia de usuario
        private void PoblarComboCriticidad()
        {
            int indexPrevio = cmbCriticidad.SelectedIndex >= 0 ? cmbCriticidad.SelectedIndex : 0;

            cmbCriticidad.Items.Clear();
            cmbCriticidad.Items.Add(GetTexto("auditoria_crit_todas"));
            cmbCriticidad.Items.Add(GetTexto("auditoria_crit_alta"));
            cmbCriticidad.Items.Add(GetTexto("auditoria_crit_media"));
            cmbCriticidad.Items.Add(GetTexto("auditoria_crit_baja"));

            if (cmbCriticidad.Items.Count > indexPrevio)
            {
                cmbCriticidad.SelectedIndex = indexPrevio;
            }
        }

        // Traduce dinámicamente las cabeceras basándose en el nombre real de la columna
        private void FormatoDgvAuditoria_43BO()
        {
            if (dgvAuditoria.Columns.Count == 0 || _dic == null) return;

            void TraducirColumna(string nombreColumna, string claveJson)
            {
                if (dgvAuditoria.Columns.Contains(nombreColumna))
                {
                    dgvAuditoria.Columns[nombreColumna].HeaderText = GetTexto(claveJson);
                }
            }

            TraducirColumna("DNI", "dgv_dni");
            TraducirColumna("Nombre", "dgv_nombre");
            TraducirColumna("Apellido", "dgv_apellido");
            TraducirColumna("Username", "dgv_username");
            TraducirColumna("Fecha", "dgv_fecha");
            TraducirColumna("Modulo", "dgv_modulo");
            TraducirColumna("Evento Realizado", "dgv_evento_realizado");
            TraducirColumna("Criticidad", "dgv_criticidad");
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            GestorIdioma_43BO.Instancia.Desuscribir_43BO(this);
            base.OnFormClosing(e);
        }

        private void CargarGrillaFiltrada_43BO()
        {
            try
            {
                DateTime inicio = dtpFechaInicio.Value;
                DateTime fin = dtpFechaFin.Value;
                string modulo = cmbModulo.SelectedValue.ToString();

                DataTable tablaDatos = bllBitacora.ListarBitacora_43BO(inicio, fin, modulo);

                if (tablaDatos != null)
                {
                    DataView vistaFiltrada = new DataView(tablaDatos);
                    List<string> filtrosExtra = new List<string>();

                    // --- FILTROS DE COMBOS ---
                    if (cmbCriticidad.SelectedIndex > 0)
                    {
                        string numeroCriticidad = cmbCriticidad.SelectedItem.ToString().Substring(0, 1);
                        filtrosExtra.Add($"[Criticidad] = {numeroCriticidad}");
                    }

                    if (cmbEvento.SelectedItem != null)
                    {
                        filtrosExtra.Add($"[Evento Realizado] = '{cmbEvento.SelectedItem.ToString()}'");
                    }

                    // --- FILTROS DE TEXTO ---
                    if (!string.IsNullOrEmpty(txtNombre.Text))
                    {
                        filtrosExtra.Add($"[Nombre] LIKE '%{txtNombre.Text.Trim()}%'");
                    }

                    if (!string.IsNullOrEmpty(txtApellido.Text))
                    {
                        filtrosExtra.Add($"CONVERT([Apellido], 'System.String') LIKE '%{txtApellido.Text.Trim()}%'");
                    }

                    if (!string.IsNullOrEmpty(txtUsername.Text))
                    {
                        string valorBusqueda = txtUsername.Text.Trim();
                        filtrosExtra.Add($"Username LIKE '%{valorBusqueda}%'");
                    }

                    if (filtrosExtra.Count > 0)
                    {
                        vistaFiltrada.RowFilter = string.Join(" AND ", filtrosExtra);
                    }
                    else
                    {
                        vistaFiltrada.RowFilter = "";
                    }

                    dgvAuditoria.DataSource = null;
                    dgvAuditoria.DataSource = vistaFiltrada;

                    if (dgvAuditoria.Columns.Count > 0)
                    {
                        dgvAuditoria.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        FormatoDgvAuditoria_43BO();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(GetTexto("auditoria_msg_error_cargar") + ex.Message, GetTexto("titulo_error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ResetearComponentes_43BO()
        {
            estaReseteando = true;

            DateTime hoy = DateTime.Now.Date;

            if (dtpFechaInicio.Value > hoy) dtpFechaInicio.Value = hoy;
            if (dtpFechaFin.Value > hoy) dtpFechaFin.Value = hoy;

            dtpFechaInicio.MaxDate = hoy;
            dtpFechaFin.MaxDate = hoy;

            dtpFechaFin.Value = hoy;
            dtpFechaInicio.Value = hoy.AddDays(-3);

            cmbModulo.DataSource = Enum.GetValues(typeof(Modulo_43BO));
            cmbEvento.DataSource = Enum.GetValues(typeof(Evento_43BO));

            // Cargamos el combo usando nuestra función localizada
            PoblarComboCriticidad();
            cmbCriticidad.SelectedIndex = 0;

            txtNombre.Clear();
            txtApellido.Clear();
            txtUsername.Clear();

            estaReseteando = false;

            CargarGrillaFiltrada_43BO();
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            CargarGrillaFiltrada_43BO();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            ResetearComponentes_43BO();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (dgvAuditoria.Rows.Count == 0)
            {
                MessageBox.Show(GetTexto("auditoria_msg_no_datos"), GetTexto("titulo_atencion"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Documento PDF (*.pdf)|*.pdf";
            sfd.FileName = $"Reporte_Auditoria_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    DataView vista = (DataView)dgvAuditoria.DataSource;
                    DataTable tablaLimpia = vista.ToTable();

                    string crit = cmbCriticidad.SelectedItem != null ? cmbCriticidad.SelectedItem.ToString() : GetTexto("auditoria_crit_todas");
                    string mod = cmbModulo.SelectedValue != null ? cmbModulo.SelectedValue.ToString() : GetTexto("auditoria_mod_todos");

                    bllBitacora.Imprimir_43BO(sfd.FileName, tablaLimpia, crit, mod);

                    MessageBox.Show(GetTexto("auditoria_msg_export_exito"), GetTexto("titulo_excelente"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(GetTexto(ex.Message), GetTexto("titulo_error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void cmbModulo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (estaReseteando) return;

            cmbEvento.DataSource = null;
            cmbEvento.Items.Clear();

            if (cmbModulo.SelectedItem is Modulo_43BO modulo)
            {
                switch (modulo)
                {
                    case Modulo_43BO.Usuario:
                        cmbEvento.Items.Add(Evento_43BO.Login);
                        cmbEvento.Items.Add(Evento_43BO.Logout);
                        cmbEvento.Items.Add(Evento_43BO.Crear);
                        cmbEvento.Items.Add(Evento_43BO.Desactivar);
                        cmbEvento.Items.Add(Evento_43BO.modificar);
                        cmbEvento.Items.Add(Evento_43BO.Bloqueo);
                        cmbEvento.Items.Add(Evento_43BO.Desbloqueo);
                        break;

                    case Modulo_43BO.Ventas:
                        break;

                    case Modulo_43BO.Compras:
                        break;
                }
            }
        }

        private void dgvAuditoria_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (estaReseteando) return;

            if (e.RowIndex >= 0 && e.RowIndex < dgvAuditoria.Rows.Count)
            {
                DataGridViewRow fila = dgvAuditoria.Rows[e.RowIndex];

                string nombre = fila.Cells["Nombre"].Value?.ToString();
                string apellido = fila.Cells["Apellido"].Value?.ToString();
                string username = fila.Cells["Username"].Value?.ToString();

                txtNombre.Text = nombre;
                txtApellido.Text = apellido;
                txtUsername.Text = username;
            }
        }
    }
}