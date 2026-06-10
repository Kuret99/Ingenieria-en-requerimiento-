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

          
            try
            {
                string json = GestorArchivosIdioma_43BO.ObtenerContenidoJson_43BO("es");
                var dic = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                _diccionario = dic;

              
                GestorIdioma_43BO.Instancia.Notificar_43BO(dic);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar idioma: " + ex.Message);
            }

       
            GestorIdioma_43BO.Instancia.Suscribir_43BO(this);
        }

        public void ActualizarIdioma_43BO(Dictionary<string, string> dic)
        {
            _diccionario = dic;
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            //si sto  falll _diccionario es null o falta la clave
            if (_diccionario == null)
            {
                MessageBox.Show("ERROR: El diccionario no se cargó.");
                return;
            }

            if (!_diccionario.ContainsKey("login_msg_exito"))
            {
                MessageBox.Show("ERROR: La clave 'login_msg_exito' no existe en el JSON.");
                return;
            }

            try
            {
                if (string.IsNullOrEmpty(txtUser.Text) || string.IsNullOrEmpty(txtContra.Text))
                {
                    MessageBox.Show(_diccionario["login_msg_incompleto"]);
                    return;
                }

                if (bll_43BO.ValidarLogin_43BO(txtUser.Text, txtContra.Text))
                {
                    MessageBox.Show(_diccionario["login_msg_exito"]);
                    Menu menu_43BO = new Menu();
                    menu_43BO.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Usuario o contraseña incorrectos");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}