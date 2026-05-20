using System.IO; 
using iTextSharp.text;
using iTextSharp.text.pdf;
using Servicios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_IngSoftware
{
    public partial class Auditoria : Form
    {

        private BLL.BLLBitacora_43BO bllBitacora = new BLL.BLLBitacora_43BO();

        public Auditoria()
        {
            InitializeComponent();

        }

        private void Auditoria_Load(object sender, EventArgs e)
        {

            ResetearComponentes_43BO();
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
                    // esto es para aplicar los filtros adicionales que se hayan puesto en los cmbos y txtboxs (ademas de la fecha y modulo que ya se aplican en la consulta SQL) 
                    DataView vistaFiltrada = new DataView(tablaDatos);
                    // y aca vamos a ir armando una lista de filtros para despues aplicarlos todos jntoss)
                    List<string> filtrosExtra = new List<string>();

                    // --- FILTROS DE COMBOS ---
                    if (cmbCriticidad.SelectedIndex > 0)
                    {
                        string numeroCriticidad = cmbCriticidad.SelectedItem.ToString().Substring(0, 1);
                        filtrosExtra.Add($"[Criticidad] = {numeroCriticidad}");
                    }

                    if (cmbEvento.SelectedValue != null)
                    {
                        filtrosExtra.Add($"[Evento Realizado] = '{cmbEvento.SelectedValue.ToString()}'");
                    }

                    // --- FILTROS DE TEXTO ---
                   
                    if (!string.IsNullOrEmpty(txtNombre.Text))
                    {
                        filtrosExtra.Add($"[Nombre Usuario] LIKE '%{txtNombre.Text.Trim()}%'");
                    }

                    if (!string.IsNullOrEmpty(txtApellido.Text))
                    {
                        filtrosExtra.Add($"[Apellido Usuario] LIKE '%{txtApellido.Text.Trim()}%'");
                    }

                    if (!string.IsNullOrEmpty(txtUsername.Text))
                    {
                        filtrosExtra.Add($"[DNI Operador] LIKE '%{txtUsername.Text.Trim()}%'");
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
                    }
                }
            }
       



        
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar la bitácora: " + ex.Message);
            }
        }

        private void ResetearComponentes_43BO()
        {
            // Volvemos a cargar las opciones de los Enums
            cmbModulo.DataSource = Enum.GetValues(typeof(Modulo_43BO));
            cmbEvento.DataSource = Enum.GetValues(typeof(Evento_43BO));

            // Reseteamos el combo de Criticidad a "Todas"
            cmbCriticidad.Items.Clear();
            cmbCriticidad.Items.Add("Todas");
            cmbCriticidad.Items.Add("1 - Alta");
            cmbCriticidad.Items.Add("2 - Media");
            cmbCriticidad.Items.Add("3 - Baja");
            cmbCriticidad.SelectedIndex = 0;

            // Clavamos las fechas por defecto (Fin hoy, Inicio 3 días atrás)
            dtpFechaFin.Value = DateTime.Now;
            dtpFechaInicio.Value = DateTime.Now.AddDays(-3);

          
            txtNombre.Clear();
            txtApellido.Clear();
            txtUsername.Clear();

            CargarGrillaFiltrada_43BO();
        }



  
        
        // este boton aplica los cambios que se haya puesto en los filtros
        private void btnAplicar_Click(object sender, EventArgs e)
        {
            CargarGrillaFiltrada_43BO();
        }

        //este boton solo limpia
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            ResetearComponentes_43BO();
        }

        // y esteboton es el que imprime
        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (dgvAuditoria.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos para exportar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Documento PDF (*.pdf)|*.pdf";
            sfd.FileName = $"Reporte_Auditoria_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // aca lo que hacemos es tomar la fuente de datos del DataGridView (que es un DataView) y convertirlo a un DataTable limpio
                    DataView vista = (DataView)dgvAuditoria.DataSource;
                    // y ahora le saco el filtro para que me imprima todo lo que se ve en el datagrid (porque si no, al ser un DataView, me imprimia todo lo que habia originalmente sin aplicar los filtros visuales del datagrid)
                    DataTable tablaLimpia = vista.ToTable();

                    string crit = cmbCriticidad.SelectedItem != null ? cmbCriticidad.SelectedItem.ToString() : "Todas";
                    string mod = cmbModulo.SelectedValue != null ? cmbModulo.SelectedValue.ToString() : "Todos";

                 
                    bllBitacora.Imprimir_43BO(sfd.FileName, tablaLimpia, crit, mod);

                    MessageBox.Show("Reporte PDF exportado con éxito.", "Excelente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        
    }
   
}

