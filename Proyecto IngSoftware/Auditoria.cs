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

             
                DataTable tablaDatos = bllBitacora.ObtenerAuditoriaPorFecha_43BO(inicio, fin, modulo);

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
            cmbCriticidad.Items.Add("1 - Baja");
            cmbCriticidad.Items.Add("2 - Media");
            cmbCriticidad.Items.Add("3 - Alta");
            cmbCriticidad.SelectedIndex = 0;

            // Clavamos las fechas por defecto (Fin hoy, Inicio 3 días atrás)
            dtpFechaFin.Value = DateTime.Now;
            dtpFechaInicio.Value = DateTime.Now.AddDays(-3);

          
            txtNombre.Clear();
            txtApellido.Clear();
            txtUsername.Clear();

            CargarGrillaFiltrada_43BO();
        }



  
        
   
        private void button1_Click(object sender, EventArgs e)
        {
            CargarGrillaFiltrada_43BO();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ResetearComponentes_43BO();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dgvAuditoria.Rows.Count == 0)
            {
                MessageBox.Show("No hay registros en la grilla para exportar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Documento PDF (*.pdf)|*.pdf";
            sfd.FileName = $"Reporte_Auditoria_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Creamos el documento PDF en orientación hoironztal para que entren todas las columnas holgadamente
                    Document doc = new Document(PageSize.A4.Rotate(), 20f, 20f, 30f, 30f);
                    PdfWriter.GetInstance(doc, new FileStream(sfd.FileName, FileMode.Create));

                    doc.Open();

                    /// esto es pa cnfigurar los estilos de texto que vamos a usar en el PDF (títulos, encabezados, celdas, etc)
                    BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                    iTextSharp.text.Font tituloFont = new iTextSharp.text.Font(bf, 18, iTextSharp.text.Font.BOLD, new BaseColor(26, 54, 93));
                    iTextSharp.text.Font subTituloFont = new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.ITALIC, BaseColor.DARK_GRAY);
                    iTextSharp.text.Font headerFont = new iTextSharp.text.Font(bf, 9, iTextSharp.text.Font.BOLD, BaseColor.WHITE);
                    iTextSharp.text.Font cellFont = new iTextSharp.text.Font(bf, 8.5f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

                    //esto solo es el titulo y su formato 
                    Paragraph titulo = new Paragraph("REPORTE DE AUDITORÍA Y BITACORA", tituloFont);
                    titulo.Alignment = Element.ALIGN_LEFT;
                    doc.Add(titulo);


                    // Agregamos una línea de metadatos debajo del título para mostrar fecha de generación, filtros aplicados y dmemas tc (esto es opcional pero queda más profesional)
                    string crit = cmbCriticidad.SelectedItem != null ? cmbCriticidad.SelectedItem.ToString() : "Todas";
                    Paragraph metadata = new Paragraph($"Generado el: {DateTime.Now:dd/MM/yyyy HH:mm} | Módulo: {cmbModulo.SelectedValue} | Criticidad: {crit}", subTituloFont);
                    metadata.SpacingAfter = 20f;
                    doc.Add(metadata);

                    int columnasVisibles = 0;
                    // Contamos cuantas columnas hay visibles en la grilla para para que cuincida con las que deberian aparecer en este PDF (si no pongo esto rompe y crea celdas en blanco)
                    foreach (DataGridViewColumn col in dgvAuditoria.Columns)
                    {
                        if (col.Visible) columnasVisibles++;
                    }

                    //esto crea la tabla en el pdf 
                    PdfPTable pdfTable = new PdfPTable(columnasVisibles);
                    pdfTable.WidthPercentage = 100;


                    // Agregamos las celdas de encabezado con el estilo definido (fondo azul, texto blanco, centrado, etc)
                    foreach (DataGridViewColumn column in dgvAuditoria.Columns)
                    {
                        if (column.Visible)
                        {
                            PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText, headerFont));
                            cell.BackgroundColor = new BaseColor(43, 108, 176);
                            cell.Padding = 6;
                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            pdfTable.AddCell(cell);
                        }
                    }

                    foreach (DataGridViewRow row in dgvAuditoria.Rows)
                    {
                        if (row.IsNewRow) continue;

                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            if (cell.OwningColumn.Visible)
                            {
                                string valorCelda = cell.Value != null ? cell.Value.ToString() : "";
                                PdfPCell pdfCell = new PdfPCell(new Phrase(valorCelda, cellFont));
                                pdfCell.Padding = 5;
                                pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;

                                if (cell.OwningColumn.HeaderText == "ID" ||
                                    cell.OwningColumn.HeaderText.Contains("Fecha") ||
                                    cell.OwningColumn.HeaderText == "Criticidad")
                                {
                                    pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                }
                                else
                                {
                                    pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
                                }

                                pdfTable.AddCell(pdfCell);
                            }
                        }
                    }

                    doc.Add(pdfTable);
                    doc.Close();

                    MessageBox.Show("Reporte PDF exportado con éxito.", "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al generar el PDF: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
