using BLL;
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

            // me suscribo aca tmb para el idioma
            GestorIdioma_43BO.Instancia.Suscribir_43BO(this);
        }

        public void ActualizarIdioma_43BO(Dictionary<string, string> dic)
        {
            // se actualiza cuando me avisan q cambio el idioma
            _diccionario = dic;
            AplicarTextosEstaticos();
        }

        private string ObtenerTexto(string clave, string porDefecto)
        {
            // funcion utilitaria
            return _diccionario != null && _diccionario.ContainsKey(clave) ? _diccionario[clave] : porDefecto;
        }

        private void AplicarTextosEstaticos()
        {
            if (_diccionario == null) return;

            // seteo los textos aca
            this.Text = ObtenerTexto("cambiocontraseña._cambio_de_contrase_a", "Cambio de Contraseña");

            label1.Text = ObtenerTexto("cambiocontraseña._nombre_de_usuario", "Nombre De Usuario");
            label2.Text = ObtenerTexto("cambiocontraseña._contrase_a_actual", "Contraseña Actual");
            label3.Text = ObtenerTexto("cambiocontraseña._nueva_contrase_a", "Nueva Contraseña");
            label4.Text = ObtenerTexto("cambiocontraseña._confirmar_nueva_contrase_a", "Confirmar Nueva Contraseña");

            button1.Text = ObtenerTexto("cambiocontraseña._aceptar", "Aceptar");
            btnCancelar.Text = ObtenerTexto("cambiocontraseña._cancelar", "Cancelar");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // checkeo q no esten vacios
            if (string.IsNullOrEmpty(txtUser.Text) || string.IsNullOrEmpty(txtConAct.Text) ||
                string.IsNullOrEmpty(txtConNueva.Text) || string.IsNullOrEmpty(txtConfirmar.Text))
            {
                MessageBox.Show(ObtenerTexto("msg_campos_vacios", "Por favor, complete todos los campos obligatorios."),
                                "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // llamo a la bll pa q haga el cambio
                bll.CambiarContraseña_43BO(txtUser.Text, txtConAct.Text, txtConNueva.Text, txtConfirmar.Text);

                MessageBox.Show(ObtenerTexto("cambiocontraseña_msg_exito", "Contraseña cambiada exitosamente. La sesión se cerrará."),
                                "Excelente", MessageBoxButtons.OK, MessageBoxIcon.Information);

                bll.CerrarSesion_43BO();

                Login login = new Login();
                login.Show();

                // cierro todo
                if (this.MdiParent != null)
                    this.MdiParent.Close();
                else
                    this.Close();
            }
            catch (Exception ex)
            {
                string mensajeError = ex.Message;
                string mensajeFinal = "";

                // 1. Verificamos si es el caso especial de "key|valor"
                if (mensajeError.Contains("|"))
                {
                    string[] partes = mensajeError.Split('|');
                    // Usamos tu método existente ObtenerTexto
                    string plantilla = ObtenerTexto(partes[0], partes[0]);
                    mensajeFinal = string.Format(plantilla, partes[1]);
                }
                else
                {
                    // 2. Es un error simple, lo traducimos con el método que ya tenés
                    mensajeFinal = ObtenerTexto(mensajeError, mensajeError);
                }

                // Mostramos el mensaje ya traducido
                MessageBox.Show(mensajeFinal, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // salir del form
            this.Close();
        }
    }
}