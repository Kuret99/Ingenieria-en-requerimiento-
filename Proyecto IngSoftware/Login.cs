using BLL;
using Servicios;
using Servicios.IidiomaObserver;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Proyecto_IngSoftware
{
    public partial class Login : Form, IdiomaObserver_43BO
    {
        private BllUser_43BO bll_43BO = new BllUser_43BO();
        private Dictionary<string, string> _diccionario;

        public Login()
        {
            InitializeComponent();

            if (!GestorIdioma_43BO.Instancia.IdiomAnterior)
            {
                GestorIdioma_43BO.Instancia.CargarIdioma_43BO("es");
            }
            GestorIdioma_43BO.Instancia.Suscribir_43BO(this);
        }

        public void ActualizarIdioma_43BO(Dictionary<string, string> dic)
        {
            // se actualiza el dic cuando cambia el idioma
            _diccionario = dic;
            AplicarTextosEstaticos();
        }

        private string ObtenerTexto(string clave, string porDefecto)
        {
            // funcion para q no explote todo si falta una clave
            return _diccionario != null && _diccionario.ContainsKey(clave) ? _diccionario[clave] : porDefecto;
        }

        private void AplicarTextosEstaticos()
        {
            if (_diccionario == null) return;

            // pongo los textos q van en las label
            label1.Text = ObtenerTexto("login._lbl_usuario", "UserName:");
            label2.Text = ObtenerTexto("login._lbl_contrasena", "Contraseña:");
            btnAceptar.Text = ObtenerTexto("login._aceptar", "Aceptar");
            this.Text = ObtenerTexto("login._titulo", "Login");
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            // boton d login q valida
            try
            {
                if (string.IsNullOrEmpty(txtUser.Text) || string.IsNullOrEmpty(txtContra.Text))
                {
                    MessageBox.Show(ObtenerTexto("login.msg_incompleto", "Por favor, complete todos los campos."),
                                    ObtenerTexto("msg_titulo_error", "Error"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (bll_43BO.ValidarLogin_43BO(txtUser.Text, txtContra.Text))
                {
                    // si sale bien
                    MessageBox.Show(ObtenerTexto("login_msg_exito", "Bienvenido al sistema."),
                                    "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (this.Modal)
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        Menu menu_43BO = new Menu();
                        menu_43BO.Show();
                        this.Hide();
                    }
                }
                else
                {
                    // aca si pusieron mal la clave
                    MessageBox.Show(ObtenerTexto("login.msg_incorrecto", "Usuario o contraseña incorrectos."),
                                    ObtenerTexto("msg_titulo_error", "Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {

                if (_diccionario == null)
                {
                    MessageBox.Show("¡El diccionario es NULL! El idioma no se cargó.");
                    return;
                }
                string mensajeError = ex.Message;

                // Si el mensaje tiene un "|" significa que trae parámetros (como los intentos restantes)
                if (mensajeError.Contains("|"))
                {
                    string[] partes = mensajeError.Split('|');
                    // Traducimos la llave (ej: "error_intentos_restantes") y reemplazamos el {0}
                    mensajeError = string.Format(ObtenerTexto(partes[0], partes[0]), partes[1]);
                }
                else
                {
                    // Traducimos el error común usando tu gestor
                    mensajeError = ObtenerTexto(mensajeError, mensajeError);
                }

                MessageBox.Show(ObtenerTexto("msg_error_general", "Se produjo un error: ") + mensajeError,
                                ObtenerTexto("msg_titulo_error", "Error"),
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }
    }
}