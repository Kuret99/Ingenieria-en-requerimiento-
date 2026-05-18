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
    public partial class Menu : Form
    {
        private GestionUs Gu_43BO;
        private CambioContraseña CC_43BO;
        private Auditoria Aud_43BO;
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

        private void Menu_Load(object sender, EventArgs e)
        {
            try
            {
                var usuarioLogueado = SessionManager_43BO.Instancia.Usuario;

                if (usuarioLogueado != null)
                {
            
                    string contraFabricaPlana = usuarioLogueado.DNI_43BO.ToString() + usuarioLogueado.Apellido_43BO.Trim();
                    string contraFabricaHash = CriptoManager_43BO.GenerarHash_43BO(contraFabricaPlana);

                 
                    if (usuarioLogueado.Hash_43BO == contraFabricaHash)
                    {
                        MessageBox.Show("Por motivos de seguridad, debe modificar su contraseña de fábrica antes de operar en el sistema.");

               
                        CambioContraseña frmCambio = new CambioContraseña();

                     
                        frmCambio.MdiParent = this;
                        frmCambio.StartPosition = FormStartPosition.CenterScreen;

                        frmCambio.Show();

                      
                        AdminToolStripMenuItem.Enabled = false;
                        masterToolStripMenuItem.Enabled = false;
                        ventaToolStripMenuItem.Enabled = false;
                        stockToolStripMenuItem.Enabled = false;



                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al verificar estado de la cuenta: " + ex.Message);
            }
        }

        private void bitacoraToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Aud_43BO = new Auditoria();
            Aud_43BO.MdiParent = this;

            Aud_43BO.Show();
        }
    }
}
