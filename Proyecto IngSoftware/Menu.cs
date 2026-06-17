using BLL;
using GUI_43BO;
using Servicios;
using Servicios.IidiomaObserver;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Proyecto_IngSoftware
{
    public partial class Menu : Form, IdiomaObserver_43BO
    {
        private GestionUs Gu_43BO;
        private CambioContraseña CC_43BO;
        private Auditoria Aud_43BO;
        private BllUser_43BO bll = new BLL.BllUser_43BO();
        private GestionPerfiles Gperfiles;
        private Dictionary<string, string> _dic;

        private readonly Dictionary<string, Permisos_43BO> _mapaMenu = new Dictionary<string, Permisos_43BO>
        {
            { "AdminToolStripMenuItem", Permisos_43BO.Menu_SeccionAdmin },
            { "masterToolStripMenuItem", Permisos_43BO.Menu_SeccionMaster },
            { "ventaToolStripMenuItem", Permisos_43BO.Menu_SeccionVenta },
            { "CompraToolStripMenuItem", Permisos_43BO.Menu_SeccionCompra },
            { "reporteToolStripMenuItem", Permisos_43BO.Menu_SeccionReporte }
        };

        public Menu()
        {
            InitializeComponent();
            GestorIdioma_43BO.Instancia.Suscribir_43BO(this);
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
                        MessageBox.Show(GetTexto("menu_msg_cambiar_contra_fabrica"), GetTexto("titulo_atencion"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        CambiarIdioma frmCambio = new CambiarIdioma();
                        frmCambio.MdiParent = this;
                        frmCambio.Show();
                    }
                    else
                    {
                        // Limpieza visual del Menú
                        AsignadorPermisos_43BO.AplicarMenu(this.menuStrip1, _mapaMenu);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(GetTexto(ex.Message), GetTexto("titulo_error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void gestionUsuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!SessionManager_43BO.Instancia.Permisos.Contains("GestionUsuarios_Acceso"))
            {
                MessageBox.Show("Acceso denegado.");
                return;
            }
            Gu_43BO = new GestionUs();
            Gu_43BO.MdiParent = this;
            Gu_43BO.Show();
        }

        private void bitacoraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!SessionManager_43BO.Instancia.Permisos.Contains("Auditoria_Acceso"))
            {
                MessageBox.Show("Acceso denegado.");
                return;
            }
            Aud_43BO = new Auditoria();
            Aud_43BO.MdiParent = this;
            Aud_43BO.Show();
        }

        private void gestionPerfilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!SessionManager_43BO.Instancia.Permisos.Contains("GestionPerfiles_Acceso"))
            {
                MessageBox.Show("Acceso denegado.");
                return;
            }
            Gperfiles = new GestionPerfiles();
            Gperfiles.MdiParent = this;
            Gperfiles.Show();
        }


        public void ActualizarIdioma_43BO(Dictionary<string, string> dic) { _dic = dic; }
        private string GetTexto(string key) => (_dic != null && _dic.ContainsKey(key)) ? _dic[key] : key;

        private void cerrarSesiobnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(GetTexto("menu_msg_confirmar_cerrar_sesion"), "Confirmar", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                bll.CerrarSesion_43BO();
                new Login().Show();
                this.Close();
            }
        }

        private void cambiarContraseñaToolStripMenuItem_Click(object sender, EventArgs e) { CC_43BO = new CambioContraseña(); CC_43BO.MdiParent = this; CC_43BO.Show(); }
        private void reLoginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //uso using porque quiero que se liberen los recursos del formulario de login una vez que se cierre
            using (Login frmLogin = new Login())
            {
                if (frmLogin.ShowDialog() == DialogResult.OK)
                {
                    foreach (Form hijo in this.MdiChildren)
                    {
                        hijo.Close();
                    }
                    var usuarioLogueado = SessionManager_43BO.Instancia.Usuario;

                    if (usuarioLogueado != null)
                    {
                        if (bll.EsContraseñaDeFabrica_43BO(usuarioLogueado))
                        {
                            MessageBox.Show(GetTexto("menu_msg_cambiar_contra_fabrica"), GetTexto("titulo_atencion"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            CambiarIdioma frmCambio = new CambiarIdioma();
                            frmCambio.MdiParent = this;
                            frmCambio.Show();
                        }
                        else
                        {
                            AsignadorPermisos_43BO.AplicarMenu(this.menuStrip1, _mapaMenu);
                        }
                    }
                }
            }
        }
        private void cambiarIdiomaToolStripMenuItem_Click(object sender, EventArgs e) { new CambiarIdioma().ShowDialog(this);

        }
    }
 }
  