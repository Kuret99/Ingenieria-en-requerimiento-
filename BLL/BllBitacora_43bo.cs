using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Servicios;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BLL
{
    public class BLLBitacora_43BO
    {
        DALBitacora_43BO dal = new DALBitacora_43BO();

        public void GuardarLog_43BO(User_43BO usaurio, Modulo_43BO modulo, Evento_43BO evento, int criticidad)
        {
            Bitacora_43BO bi = new Bitacora_43BO();
          
            //como todavia no tengo el login tuve que mporvisar un dni de un usaurio para testear
           
            
            bi.log_43BO = SessionManager_43BO.Instancia.Usuario;

            bi.Modulo = modulo;
            bi.Evento = evento;
            bi.Fecha_43BO = DateTime.Now;
            bi.Criticidad_43BO = criticidad;
            
            dal.GuardarLog_43BO(bi);
        }

        public DataTable ListarBitacora_43BO(DateTime fInicio, DateTime fFin, string modulo)
        {
            
            return dal.ListarBitacora_43BO(fInicio, fFin, modulo);
        }
        public void Imprimir_43BO(string ruta, DataTable datos, string criticidad, string modulo)
        {
            try
            {
                // configuramos l dcoumento PDF igual que con los CSV de jorge
                Document doc = new Document(PageSize.A4.Rotate(), 20f, 20f, 30f, 30f);
                PdfWriter.GetInstance(doc, new FileStream(ruta, FileMode.Create));
                doc.Open();

                // agregamos  fuentes y estilos que querramos 
                BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                Font tituloFont = new Font(bf, 18, Font.BOLD, new BaseColor(26, 54, 93));
                Font subTituloFont = new Font(bf, 10, Font.ITALIC, BaseColor.DARK_GRAY);
                Font headerFont = new Font(bf, 9, Font.BOLD, BaseColor.WHITE);
                Font cellFont = new Font(bf, 8.5f, Font.NORMAL, BaseColor.BLACK);

                // aca pngo ls  encabezados del PDF
                Paragraph titulo = new Paragraph("REPORTE DE AUDITORÍA Y BITACORA", tituloFont);
                doc.Add(titulo);

                Paragraph metadata = new Paragraph($"Generado el: {DateTime.Now:dd/MM/yyyy HH:mm} | Módulo: {modulo} | Criticidad: {criticidad}", subTituloFont);
                metadata.SpacingAfter = 20f;
                doc.Add(metadata);

                // crea la tabla del PDF usando el ancho del datatabñe
                PdfPTable pdfTable = new PdfPTable(datos.Columns.Count);
                pdfTable.WidthPercentage = 100;

                // reccorre las columnas del datatble apra crear bine los encabezados
                foreach (DataColumn columna in datos.Columns)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(columna.ColumnName, headerFont));
                    cell.BackgroundColor = new BaseColor(43, 108, 176);
                    cell.Padding = 6;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    pdfTable.AddCell(cell);
                }

                // recorre las filas y las celdas del datatable para llenar la tabla del PDF con los datos
                 foreach (DataRow fila in datos.Rows)
                {
                    foreach (var celda in fila.ItemArray)
                    {
                        string valorCelda = celda != null ? celda.ToString() : "";
                        PdfPCell pdfCell = new PdfPCell(new Phrase(valorCelda, cellFont));
                        pdfCell.Padding = 5;
                        pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                        pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;

                        pdfTable.AddCell(pdfCell);
                    }
                }
                // y por ultimo esto solo agrga la tabla al documento y lo cierra para que se genre el PDF final
                doc.Add(pdfTable);
                doc.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Error en BLL al generar PDF: " + ex.Message);
            }
        }
    }
}
