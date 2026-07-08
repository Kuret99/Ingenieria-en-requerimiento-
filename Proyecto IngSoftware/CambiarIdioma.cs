using BLL;
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
        BllUser_43BO bllUser = new BLL.BllUser_43BO();
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
                    
                    
                    bllUser.CambiarIdiomaUsuario_43BO(SessionManager_43BO.Instancia.Usuario.DNI_43BO, codigoIdioma);

                    this.Close();
                }
                catch (Exception ex)
                {
                    // traduzco directo con el gestor porque este form no tiene diccionario propio
                    MessageBox.Show(GestorIdioma_43BO.Instancia.ObtenerTexto_43BO("cambiaridioma_error_al_cambiar_el_idioma", "Error al cambiar el idioma:") + " " + ex.Message,
                                    GestorIdioma_43BO.Instancia.ObtenerTexto_43BO("titulo_error", "Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            
            this.Close();
        }
    }
}
