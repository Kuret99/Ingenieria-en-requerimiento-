using GUI_43BO;
using Servicios;
using Servicios.IidiomaObserver;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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

        public void ActualizarIdioma_43BO(Dictionary<string, string> dic)
        {
            _dic = dic;

            this.Traducir(dic, "auditoria_titulo");

            // Botones
            btnAplicar.Traducir(dic, "auditoria_bt_filtrar");
            btnLimpiar.Traducir(dic, "auditoria_bt_cancelar");
            btnImprimir.Traducir(dic, "auditoria_btn_imprimir");

            label1.Traducir(dic, "dgv_nombre");
            label2.Traducir(dic, "dgv_apellido");
            label3.Traducir(dic, "dgv_usema");
            label4.Traducir(dic, "dgv_criticid");
            label5.Traducir(dic, "lbl_fechainicio");
            label6.Traducir(dic, "lbl_fechafin");
            label7.Traducir(dic, "auditoria_lbl_modulo");
            label8.Traducir(dic, "auditoria_lbl_evento");

            cmbModulo.DataSource = ObtenerEnumTraducido<Modulo_43BO>("enum_modulo");
            cmbModulo.DisplayMember = "Texto";
            cmbModulo.ValueMember = "Id";

            // 3. Cargar ComboBox de Eventos
            cmbEvento.DataSource = ObtenerEnumTraducido<Evento_43BO>("enum_evento");
            cmbEvento.DisplayMember = "Texto";
            cmbEvento.ValueMember = "Id";

            PoblarComboCriticidad();
            FormatoDgvAuditoria_43BO();
            CargarGrillaFiltrada_43BO();
        }

        private string GetTexto(string key)
        {
            return (_dic != null && _dic.ContainsKey(key)) ? _dic[key] : key;
        }

        private void PoblarComboCriticidad()
        {
            int indexPrevio = cmbCriticidad.SelectedIndex >= 0 ? cmbCriticidad.SelectedIndex : 0;
            cmbCriticidad.Items.Clear();
            cmbCriticidad.Items.Add(GestorIdioma_43BO.Instancia.ObtenerTexto_43BO("auditoria_crit_todas"));
            cmbCriticidad.Items.Add(GestorIdioma_43BO.Instancia.ObtenerTexto_43BO("auditoria_crit_alta"));
            cmbCriticidad.Items.Add(GestorIdioma_43BO.Instancia.ObtenerTexto_43BO("auditoria_crit_media"));
            cmbCriticidad.Items.Add(GestorIdioma_43BO.Instancia.ObtenerTexto_43BO("auditoria_crit_baja"));

            if (cmbCriticidad.Items.Count > indexPrevio) cmbCriticidad.SelectedIndex = indexPrevio;
        }

        private void FormatoDgvAuditoria_43BO()
        {
            if (dgvAuditoria.Columns.Count == 0 || _dic == null) return;

            void TraducirColumna(string nombreColumna, string claveJson)
            {
                if (dgvAuditoria.Columns.Contains(nombreColumna))
                    dgvAuditoria.Columns[nombreColumna].HeaderText = GestorIdioma_43BO.Instancia.ObtenerTexto_43BO(claveJson);
            }

            TraducirColumna("DNI", "dgv_dni");
            TraducirColumna("Nombre", "dgv_nombre");
            TraducirColumna("Apellido", "dgv_apellido");
            TraducirColumna("Username", "dgv_usema");
            TraducirColumna("Fecha y Hora", "lbl_fechainicio");
            TraducirColumna("Módulo", "auditoria_lbl_modulo");
            TraducirColumna("Evento Realizado", "auditoria_lbl_evento");
            TraducirColumna("Criticidad", "dgv_criticid");
        }

        private void CargarGrillaFiltrada_43BO()
        {
            try
            {
                DateTime inicio = dtpFechaInicio.Value;
                DateTime fin = dtpFechaFin.Value;
                string modulo = (cmbModulo.SelectedValue != null) ? cmbModulo.SelectedValue.ToString() : "";

                DataTable tablaDatos = bllBitacora.ListarBitacora_43BO(inicio, fin, modulo);

                if (tablaDatos != null)
                {
                    dgvAuditoria.DataSource = tablaDatos;
                    if (dgvAuditoria.Columns.Count > 0)
                    {
                        dgvAuditoria.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        FormatoDgvAuditoria_43BO();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(GetTexto("msg_error_general") + " " + ex.Message);
            }
        }

        private void ResetearComponentes_43BO()
        {
            estaReseteando = true;
            DateTime hoy = DateTime.Now.Date;
            dtpFechaInicio.Value = hoy.AddDays(-3);
            dtpFechaFin.Value = hoy;
            PoblarComboCriticidad();
            cmbCriticidad.SelectedIndex = 0;
            txtNombre.Clear(); txtApellido.Clear(); txtUsername.Clear();
            estaReseteando = false;
            CargarGrillaFiltrada_43BO();
        }

        private void btnAplicar_Click(object sender, EventArgs e) => CargarGrillaFiltrada_43BO();
        private void btnLimpiar_Click(object sender, EventArgs e) => ResetearComponentes_43BO();

        private void cmbModulo_SelectedIndexChanged(object sender, EventArgs e) { if (estaReseteando) return; }

        private void dgvAuditoria_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvAuditoria.Rows[e.RowIndex];
                txtNombre.Text = fila.Cells["Nombre"].Value?.ToString();
                txtApellido.Text = fila.Cells["Apellido"].Value?.ToString();
                txtUsername.Text = fila.Cells["Username"].Value?.ToString();
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e) { /* Tu lógica previa */ }

        private object ObtenerEnumTraducido<T>(string prefijoJson) where T : Enum
        {
            return Enum.GetValues(typeof(T))
                .Cast<T>()
                .Select(valor => new
                {
                    Id = valor,
                    Texto = GestorIdioma_43BO.Instancia.ObtenerTexto_43BO($"{prefijoJson}_{valor.ToString().ToLower()}")
                })
                .ToList();
        }
    }
}