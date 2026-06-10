using BLL;
using Newtonsoft.Json; // Aseguramos el uso de JSON
using Servicios;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Servicios.IidiomaObserver;

namespace Proyecto_IngSoftware
{
    public partial class CambioContraseña : Form, IdiomaObserver_43BO
    {
        private BllUser_43BO bll = new BllUser_43BO();
        private Dictionary<string, string> _diccionario;

        public CambioContraseña()
        {
            InitializeComponent();

            // Cargamos el idioma inicial de forma segura
            try
            {
                string json = GestorArchivosIdioma_43BO.ObtenerContenidoJson_43BO("es");
                _diccionario = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                AplicarTextosEstaticos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar idioma inicial: " + ex.Message);
            }

           
            GestorIdioma_43BO.Instancia.Suscribir_43BO(this);
        }

        public void ActualizarIdioma_43BO(Dictionary<string, string> dic)
        {
            _diccionario = dic;
            AplicarTextosEstaticos();
        }

        private string ObtenerTexto(string clave, string porDefecto)
        {
            return _diccionario != null && _diccionario.ContainsKey(clave) ? _diccionario[clave] : porDefecto;
        }

        private void AplicarTextosEstaticos()
        {
            if (_diccionario == null) return;

          
            this.Text = ObtenerTexto("cambiocon_titulo", "Cambio de Contraseña");

            label5.Text = ObtenerTexto("cambiocon_titulo", "Cambio de contraseña");

         
            label1.Text = ObtenerTexto("cambiocon_lbl_usuario", "Nombre De Usuario");
            label2.Text = ObtenerTexto("cambiocon_lbl_actual", "Contraseña Actual");
            label3.Text = ObtenerTexto("cambiocon_lbl_nueva", "Nueva Contraseña");
            label4.Text = ObtenerTexto("cambiocon_lbl_confirmar", "Confirmar Nueva Contraseña");
          
           
            button1.Text = ObtenerTexto("cambiocon_btn_aceptar", "Aceptar");
            btnCancelar.Text = ObtenerTexto("cambiocon_btn_cancelar", "Cancelar");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (string.IsNullOrEmpty(txtUser.Text) || string.IsNullOrEmpty(txtConAct.Text) ||
                string.IsNullOrEmpty(txtConNueva.Text) || string.IsNullOrEmpty(txtConfirmar.Text))
            {
                string msgVacios = ObtenerTexto("msg_campos_vacios", "Por favor, complete todos los campos obligatorios.");
                MessageBox.Show(msgVacios, "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Procesar cambio en la capa de negocio
                bll.CambiarContraseña_43BO(txtUser.Text, txtConAct.Text, txtConNueva.Text, txtConfirmar.Text);

            
                string msgExito = ObtenerTexto("cambiocon_msg_exito", "Contraseña cambiada exitosamente. La sesión se cerrará y deberá volver a loguearse.");
                MessageBox.Show(msgExito, "Excelente", MessageBoxButtons.OK, MessageBoxIcon.Information);

              
                bll.CerrarSesion_43BO();

               
                Login login = new Login();
                login.Show();

                if (this.MdiParent != null)
                {
                    this.MdiParent.Close();
                }
                else
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}