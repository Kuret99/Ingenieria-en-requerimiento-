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
                   
                    if (bll.EsContraseñaDeFabrica_43BO(usuarioLogueado))
                    {
                        MessageBox.Show("Por motivos de seguridad, debe modificar su contraseña de fábrica antes de operar en el sistema.");

                        CambioContraseña frmCambio = new CambioContraseña();
                        frmCambio.MdiParent = this;
                        frmCambio.StartPosition = FormStartPosition.CenterScreen;
                        frmCambio.Show();

                       
                        AdminToolStripMenuItem.Enabled = false;
                        masterToolStripMenuItem.Enabled = false;
                        ventaToolStripMenuItem.Enabled = false;
                        CompraToolStripMenuItem.Enabled = false;
                    }
                    else
                    {
                        // AHORAAA SIIIIIIIIIIII ESTO va a poder simular lo de roles y permisos para ams adelante
                        List<string> permisosActivos = bll.ObtenerPermisos_43BO(usuarioLogueado);

                        string listaDebug = string.Join(", ", permisosActivos);
                      
                        MessageBox.Show("Permisos encontrados en la base: " + (string.IsNullOrEmpty(listaDebug) ? "NINGUNO" : listaDebug));

                        //el menu solo habilita o deshabilita según los permisos o que tiene permitido el user
                        AdminToolStripMenuItem.Enabled = permisosActivos.Contains("GestionUsuarios_Acceso");
                        masterToolStripMenuItem.Enabled = permisosActivos.Contains("Master");
                        ventaToolStripMenuItem.Enabled = permisosActivos.Contains("Venta");
                        CompraToolStripMenuItem.Enabled = permisosActivos.Contains("Stock");
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

        private void reLoginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // Instanciamos el Login existente
                Login ReLogin = new Login();

                // Hacemos que se abra centrado respecto al menú principal
                ReLogin.StartPosition = FormStartPosition.CenterParent;

                // Lo abrimos como un cuadro de diálogo modal (bloquea el menú de fondo hasta que se cierre o rebote)
                ReLogin.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al ejecutar el ReLogin: " + ex.Message);
            }

        }
    }
}
