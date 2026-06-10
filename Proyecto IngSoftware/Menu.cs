using BLL;
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

        public Menu()
        {
            InitializeComponent();
            GestorIdioma_43BO.Instancia.Suscribir_43BO(this);
        }

       
        public void ActualizarIdioma_43BO(Dictionary<string, string> dic)
        {
            _dic = dic;

            // Menú Admin
            AdminToolStripMenuItem.Text = GetTexto("menu_admin");
            gestionUsuarioToolStripMenuItem.Text = GetTexto("menu_admin_gestion_usuario");
            bitacoraToolStripMenuItem.Text = GetTexto("menu_admin_auditoria");
            gestionPerfilesToolStripMenuItem.Text = GetTexto("menu_admin_gestion_perfiles");

            // Menú Usuario
            UserToolStripMenuItem.Text = GetTexto("menu_usuario");
            cambiarContraseñaToolStripMenuItem.Text = GetTexto("menu_usuario_cambiar_contra");
            cerrarSesiobnToolStripMenuItem.Text = GetTexto("menu_usuario_cerrar_sesion");
            cambiarIdiomaToolStripMenuItem.Text = GetTexto("menu_usuario_cambiar_idioma");
            reLoginToolStripMenuItem.Text = GetTexto("menu_usuario_relogin");

            // Otros
            masterToolStripMenuItem.Text = GetTexto("menu_master");
            ventaToolStripMenuItem.Text = GetTexto("menu_venta");
            CompraToolStripMenuItem.Text = GetTexto("menu_compra");
            reporteToolStripMenuItem.Text = GetTexto("menu_reporte");
            ayudaToolStripMenuItem.Text = GetTexto("menu_ayuda");
        }

  
        private string GetTexto(string key)
        {
            if (_dic != null && _dic.ContainsKey(key))
                return _dic[key];
            return key; // Si es un error crudo de SQL o BLL devuelve la cadena original sin romperse
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            GestorIdioma_43BO.Instancia.Desuscribir_43BO(this);
            base.OnFormClosing(e);
        }

        private void gestionUsuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Gu_43BO = new GestionUs();
            Gu_43BO.MdiParent = this;
            Gu_43BO.Show();
        }

        private void cerrarSesiobnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                GetTexto("menu_msg_confirmar_cerrar_sesion"),
                GetTexto("titulo_confirmacion"),
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

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
                    MessageBox.Show(GetTexto(ex.Message), GetTexto("titulo_error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        MessageBox.Show(
                            GetTexto("menu_msg_cambiar_contra_fabrica"),
                            GetTexto("titulo_atencion"),
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );

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
                        List<string> permisosActivos = bll.ObtenerPermisos_43BO(usuarioLogueado);

                        // Traducimos el cartel de depuración de permisos usando componentes del JSON
                        string detallePermisos = string.IsNullOrEmpty(listaDebug(permisosActivos)) ? GetTexto("menu_msg_ninguno") : listaDebug(permisosActivos);
                        MessageBox.Show(
                            GetTexto("menu_msg_permisos_encontrados") + " " + detallePermisos,
                            GetTexto("titulo_atencion"),
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );

                        AdminToolStripMenuItem.Enabled = permisosActivos.Contains("GestionUsuarios_Acceso");
                        masterToolStripMenuItem.Enabled = permisosActivos.Contains("Master");
                        ventaToolStripMenuItem.Enabled = permisosActivos.Contains("Venta");
                        CompraToolStripMenuItem.Enabled = permisosActivos.Contains("Stock");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(GetTexto(ex.Message), GetTexto("titulo_error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string listaDebug(List<string> lista) => string.Join(", ", lista);

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
                Login ReLogin = new Login();
                ReLogin.StartPosition = FormStartPosition.CenterParent;
                ReLogin.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(GetTexto(ex.Message), GetTexto("titulo_error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gestionPerfilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Gperfiles = new GestionPerfiles();
            Gperfiles.MdiParent = this;
            Gperfiles.Show();
        }

        private void cambiarIdiomaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CambiarIdioma ci = new CambiarIdioma();
            ci.ShowDialog(this);
        }
    }
}