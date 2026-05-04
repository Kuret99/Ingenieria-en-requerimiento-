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
    public partial class Menu : Form
    {
        private GestionUs Gu_43BO;
        private CambioContraseña CC_43BO;
        private BllUser_43BO bll = new BLL.BllUser_43BO();
        public Menu()
        {
            InitializeComponent();
        }

        private void gestionUsuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Gu_43BO = new GestionUs();

            Gu_43BO.MdiParent = this;

            Gu_43BO.Show();
        }

        private void cerrarSesiobnToolStripMenuItem_Click(object sender, EventArgs e)
        {

            DialogResult result = MessageBox.Show("¿Está seguro que desea cerrar sesión?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                  
                 
                    bll.CerrarSesion_43BO();


                    Login frmLogin = new Login();
                    frmLogin.Show();

                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cerrar sesión: " + ex.Message);
                }
            }
        }

        private void cambiarContraseñaToolStripMenuItem_Click(object sender, EventArgs e)
        {

            CC_43BO = new CambioContraseña();
            CC_43BO.MdiParent = this;

            CC_43BO.Show();
        }
    }
}
