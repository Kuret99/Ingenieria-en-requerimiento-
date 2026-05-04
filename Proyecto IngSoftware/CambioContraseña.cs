using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
namespace Proyecto_IngSoftware
{
    
    public partial class CambioContraseña : Form
    {

        private BllUser_43BO bll = new BLL.BllUser_43BO();
        public CambioContraseña()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtUser.Text)|| string.IsNullOrEmpty(txtConAct.Text)|| string.IsNullOrEmpty(txtConNueva.Text)|| string.IsNullOrEmpty(txtConfirmar.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos.");
                return;
            }
            try 
            {

                bll.CambiarContraseña_43BO(txtUser.Text, txtConAct.Text, txtConNueva.Text, txtConfirmar.Text);
            }
            catch 
            {
            }

        }
    }
}
