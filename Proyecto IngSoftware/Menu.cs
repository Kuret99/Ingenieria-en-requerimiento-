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
    public partial class Menu : Form
    {
        private GestionUs Gu;
        public Menu()
        {
            InitializeComponent();
        }

        private void gestionUsuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Gu = new GestionUs();

            Gu.MdiParent = this;

            Gu.Show();
        }

        private void cerrarSesiobnToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
            DialogResult result = MessageBox.Show("¿Está seguro que desea cerrar sesión?","Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                   
                    var usuarioActual = Servicios.SessionManager_43BO.Instancia.Usuario;
                    BLL.BLLBitacora_43BO bllBitacora = new BLL.BLLBitacora_43BO();

                    bllBitacora.GuardarLog_43BO(usuarioActual, Servicios.Modulo_43BO.Usuario, Servicios.Evento_43BO.Logout, 1);

                   
                    Servicios.SessionManager_43BO.CerrarSesion_43BO();

                  
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
    }
}
