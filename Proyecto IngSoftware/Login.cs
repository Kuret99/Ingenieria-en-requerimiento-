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

                if (SessionManager_43BO.VerificarSesionActiva_43BO())
                {
                    MessageBox.Show("Ya existe una sesión activa en el sistema. Debe cerrar la sesión actual antes de intentar un nuevo ingreso.",
                                    "Acceso Denegado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return; // Bloqueamos totalmente el acceso
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
