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
        //creo este dictionary para poder filtrar los eventos segu el modulo seleccionaods
        private Dictionary<Modulo_43BO, List<Evento_43BO>> _mapaEventosPorModulo = new Dictionary<Modulo_43BO, List<Evento_43BO>>()
        {
            { Modulo_43BO.Usuario, new List<Evento_43BO> {
                Evento_43BO.Login, Evento_43BO.Logout, Evento_43BO.Bloqueo,
                Evento_43BO.Desbloqueo, Evento_43BO.modificar, Evento_43BO.Desactivar
            }},
            { Modulo_43BO.Perfiles, new List<Evento_43BO> {
                Evento_43BO.CrearRol, Evento_43BO.ModificarRol, Evento_43BO.EliminarRol,
                Evento_43BO.AsignarRol, Evento_43BO.QuitarRol, Evento_43BO.CrearFamilia,
                Evento_43BO.ModificarFamilia, Evento_43BO.EliminarFamilia, Evento_43BO.AsignarFamilia,
                Evento_43BO.QuitarFamilia
            }},
            // modulo Admin: acciones de mantenimiento de la BD
            { Modulo_43BO.Admin, new List<Evento_43BO> {
                Evento_43BO.Backup, Evento_43BO.RecalcularDV, Evento_43BO.RestaurarBackup
            }}
        };

        public Auditoria()
        {
            InitializeComponent();
            GestorIdioma_43BO.Instancia.Suscribir_43BO(this);
        }

        private void Auditoria_Load(object sender, EventArgs e)
        {
            // Limitar al día de hoy
            dtpFechaInicio.MaxDate = DateTime.Today;
            dtpFechaFin.MaxDate = DateTime.Today;

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


            var listaModulos = ObtenerEnumTraducido<Modulo_43BO>("enum_modulo");
            cmbModulo.DataSource = listaModulos; // DataSource primero
            cmbModulo.DisplayMember = "Texto";    // Luego los miembros
            cmbModulo.ValueMember = "Id";

            // CARGA DE EVENTOS
            var listaEventos = ObtenerEnumTraducido<Evento_43BO>("enum_evento");
            cmbEvento.DataSource = listaEventos; // DataSource primero
            cmbEvento.DisplayMember = "Texto";
            cmbEvento.ValueMember = "Id";

            PoblarComboCriticidad();
            FormatoDgvAuditoria_43BO();
            CargarGrillaFiltrada_43BO();
        }

        // unico punto de traduccion: todo pasa por el gestor
        private string ObtenerTexto(string clave, string porDefecto = null)
        {
            return GestorIdioma_43BO.Instancia.ObtenerTexto_43BO(clave, porDefecto ?? clave);
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
                string evento = (cmbEvento.SelectedValue != null) ? cmbEvento.SelectedValue.ToString() : "";
                string criticidad = (cmbCriticidad.SelectedIndex > 0) ? cmbCriticidad.SelectedIndex.ToString() : "";

                DataTable tablaDatos = bllBitacora.ListarBitacora_43BO(inicio, fin, modulo, evento, criticidad);

                // Validamos si la tabla tiene filas
                if (tablaDatos != null && tablaDatos.Rows.Count > 0)
                {
                    dgvAuditoria.DataSource = tablaDatos;
                    if (dgvAuditoria.Columns.Count > 0)
                    {
                        dgvAuditoria.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        FormatoDgvAuditoria_43BO();
                    }
                }
                else
                {
                    // Limpiamos la grilla y avisamos
                    dgvAuditoria.DataSource = null;
                    MessageBox.Show(ObtenerTexto("msg_sin_resultados"), ObtenerTexto("titulo_info", "Info"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ObtenerTexto("msg_error_general") + " " + ex.Message);
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

        private void cmbModulo_SelectedIndexChanged(object sender, EventArgs e)
        {
            // evita entrar au bucle infnitio 
            if (estaReseteando || cmbModulo.SelectedValue == null) return;

            //obtengo el valro como string
            string val = cmbModulo.SelectedValue.ToString();

            //
            if (string.IsNullOrEmpty(val))
            {
                cmbEvento.DataSource = ObtenerEnumTraducido<Evento_43BO>("enum_evento");
                cmbEvento.Enabled = true;
                return;
            }

            //convierto el string a enum para poder buscar en el diccionario de eventos
            if (Enum.TryParse(val, out Modulo_43BO mod))
            {
                // si el modulo tiene 
                if (_mapaEventosPorModulo.ContainsKey(mod))
                {
                    var lista = new List<object>();
                    lista.Add(new { Id = "", Texto = ObtenerTexto("auditoria_opt_todos", "Todos") });

                    foreach (var ev in _mapaEventosPorModulo[mod])
                    {
                        lista.Add(new { Id = ev.ToString(), Texto = ObtenerTextoOPredefinido("enum_evento", ev) });
                    }

                 
                    cmbEvento.DataSource = lista;
                    cmbEvento.DisplayMember = "Texto";
                    cmbEvento.ValueMember = "Id";
                    cmbEvento.Enabled = true;
                }
                else
                {
                    // si el modulo no teine evento comoventa,compra etc mostraria esto
                    cmbEvento.DataSource = new List<object> { new { Id = "", Texto = ObtenerTexto("auditoria_opt_na", "N/A") } };
                    cmbEvento.Enabled = false;
                }
            }

        }

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

        private void btnImprimir_Click(object sender, EventArgs e) 
        {
            //verifico qu haya daos en la grila
            if (dgvAuditoria.DataSource == null || !(dgvAuditoria.DataSource is DataTable dt))
            {
                MessageBox.Show(ObtenerTexto("auditoria_msg_sin_datos_imprimir", "No hay datos para imprimir."),
                                ObtenerTexto("titulo_atencion", "Atención"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // abro el dialogo para guardar el PDF
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "PDF (*.pdf)|*.pdf";
                sfd.FileName = "Reporte_Auditoria_" + DateTime.Now.ToString("yyyyMMdd_HHmm") + ".pdf";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        //guardo los filtros actuales
                        string criticidad = cmbCriticidad.Text;
                        string modulo = cmbModulo.Text;

                        //yllamamos al metodo de la bll ychau
                        bllBitacora.Imprimir_43BO(sfd.FileName, dt, criticidad, modulo);

                        MessageBox.Show(ObtenerTexto("auditoria_msg_pdf_generado", "PDF generado correctamente."),
                                        ObtenerTexto("titulo_excelente", "Éxito"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ObtenerTexto("msg_error_general", "Se produjo un error: ") + ObtenerTexto(ex.Message, ex.Message),
                                        ObtenerTexto("titulo_error", "Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }



        private object ObtenerEnumTraducido<T>(string prefijoJson) where T : Enum
        {
            // llamma al lista orginal 
            var lista = Enum.GetValues(typeof(T))
                .Cast<T>()
                .Select(valor => new
                {
                    Id = valor.ToString(),
                    //aca llamo al metodo auxilia
                    Texto = ObtenerTextoOPredefinido(prefijoJson, valor)
                })
                .ToList();

            // agrego la opcion todos al principio (traducida, igual que el combo de criticidad)
            var listaConTodos = new List<object>();
            listaConTodos.Add(new { Id = "", Texto = ObtenerTexto("auditoria_opt_todos", "Todos") });
            listaConTodos.AddRange(lista);

            return listaConTodos;
        }
        //estiy quemadisimo esperoq eu funcione basicamente hace que si no encuentra la traduccion del enum en el json devuelve el nombre del enum   
        private string ObtenerTextoOPredefinido(string prefijo, Enum valor)
        {
            string clave = $"{prefijo}_{valor.ToString().ToLower()}";
            string texto = GestorIdioma_43BO.Instancia.ObtenerTexto_43BO(clave);

            // Si la traducción no existe, devolvemos el nombre del Enum como respaldo
            if (string.IsNullOrEmpty(texto) || texto == clave) return valor.ToString();
            return texto;
        }
    }
}