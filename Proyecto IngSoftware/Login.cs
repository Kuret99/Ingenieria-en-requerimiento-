using BLL;
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
    public partial class Login : Form
    {
        private BllUser_43BO bll = new BllUser_43BO();
        private Menu menu;
        public Login()
        {
            InitializeComponent();
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtUser.Text) || string.IsNullOrEmpty(txtContra.Text))
                {
                    MessageBox.Show("por favir complete los campos.");
                    return;

                }

                if (SessionManager_43BO.EsUsuarioActual_43BO(txtUser.Text))
                {
                    // 1. Traemos el objeto limpio desde la instancia del Singleton
                    var usuarioEnSesion = SessionManager_43BO.Instancia.Usuario;

                    // 2. Armamos el Username concatenando DNI y Nombre en el mismo formato que usa tu DAL
                    string usernameInstancia = usuarioEnSesion.DNI_43BO.ToString() + usuarioEnSesion.Nombre_43BO.Trim();

                    // 3. Lo escupimos en el cartel
                    MessageBox.Show($"Operación Denegada: El usuario '{usernameInstancia}' ya tiene una instancia activa en el sistema.",
                                    "Control de Concurrencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                bool ingreso = bll.ValidarLogin_43BO(txtUser.Text, txtContra.Text);


                if (ingreso)
                {
                 

                    MessageBox.Show("Ingreso Exitoso");

                    // --- MANEJO DE VENTANAS (RE-LOGIN VS INICIO) ---
                    if (Application.OpenForms["Menu"] != null)
                    {
                        this.Close(); // Es ReLogin, cerramos solo la ventanita flotante
                    }
                    else
                    {
                        // Es el arranque inicial del sistema
                        menu = new Menu();
                        menu.Show();
                        this.Hide();
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return;
            }








        }
    }
}
