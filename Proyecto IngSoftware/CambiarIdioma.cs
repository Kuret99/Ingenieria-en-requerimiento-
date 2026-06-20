using Newtonsoft.Json;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Proyecto_IngSoftware
{
    public partial class CambiarIdioma : Form
    {
        
        private class OpcionIdioma
        {
            public string Texto { get; set; }
            public string Codigo { get; set; }
        }

        public CambiarIdioma()
        {
            InitializeComponent();
            ConfigurarFormulario_43BO();
        }

        private void ConfigurarFormulario_43BO()
        {
           
            this.Text = "Idioma / Language";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;

         
            List<OpcionIdioma> lista = new List<OpcionIdioma>
            {
                new OpcionIdioma { Texto = "Español", Codigo = "es" },
                new OpcionIdioma { Texto = "English", Codigo = "en" },
                new OpcionIdioma { Texto = "Português", Codigo = "pr" }
            };

            comboBox1.DataSource = lista;
            comboBox1.DisplayMember = "Texto";  // Lo que ve el usuario
            comboBox1.ValueMember = "Codigo";   // Lo que usamos internamente ("es", "en", "pt")

            comboBox1.SelectedIndex = 0; // Por defecto arranca en Español
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedValue is string codigoIdioma)
            {
                try
                {
                  
                    int dniUsuario = SessionManager_43BO.Instancia.Usuario.DNI_43BO;

                    BLL.BllUser_43BO bllUser = new BLL.BllUser_43BO();
                    bllUser.CambiarIdiomaUsuario_43BO(dniUsuario, codigoIdioma);

                    
                    string json = GestorArchivosIdioma_43BO.ObtenerContenidoJson_43BO(codigoIdioma);
                    var nuevoDiccionario = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

                    GestorIdioma_43BO.Instancia.Notificar_43BO(nuevoDiccionario);

                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cambiar el idioma: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            
            this.Close();
        }
    }
}
