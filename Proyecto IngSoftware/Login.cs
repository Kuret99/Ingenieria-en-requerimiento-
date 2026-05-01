using BLL;
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
                  bool ingreso = bll.ValidarLogin_43BO(txtUser.Text, txtContra.Text);


                if (ingreso)
                {
                    MessageBox.Show("Ingreso Exitoso");

                    menu = new Menu();

                    menu.Show();

                    this.Hide();

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
