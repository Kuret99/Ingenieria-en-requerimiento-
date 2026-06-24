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

        // 1. Agregamos los submenús al diccionario mapeador de patentes
        private readonly Dictionary<string, Permisos_43BO> _mapaMenu = new Dictionary<string, Permisos_43BO>
        {
            { "AdminToolStripMenuItem", Permisos_43BO.Menu_SeccionAdmin },
            { "masterToolStripMenuItem", Permisos_43BO.Menu_SeccionMaster },
            { "ventaToolStripMenuItem", Permisos_43BO.Menu_SeccionVenta },
            { "CompraToolStripMenuItem", Permisos_43BO.Menu_SeccionCompra },
            { "reporteToolStripMenuItem", Permisos_43BO.Menu_SeccionReporte },
            
            // Reemplazá acá por el "Name" exacto que tengan tus ToolStripMenuItems en el diseñador:
            { "cambiarContraseñaToolStripMenuItem", Permisos_43BO.Usuario_CambioContraseña },
            { "cambiarIdiomaToolStripMenuItem", Permisos_43BO.Usuario_CambioIdioma }
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
                        // Esto ocultará o deshabilitará automáticamente los botones mapeados si no tienen la patente
                        AsignadorPermisos_43BO.AplicarMenu(this.menuStrip1, _mapaMenu);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(GetTexto(ex.Message), GetTexto("titulo_error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TraducirMenu(ToolStripItemCollection items)
        {
            foreach (ToolStripItem item in items)
            {
                string clave = "menu." + item.Name.ToLower().Replace("toolstripmenuitem", "");

                if (_dic != null && _dic.ContainsKey(clave))
                {
                    item.Text = _dic[clave];
                }

                if (item is ToolStripMenuItem dropDown)
                {
                    TraducirMenu(dropDown.DropDownItems);
                }
            }
        }

        public void TraducirFormulario(Form formulario)
        {
            if (_dic == null) return;

            if (_dic.ContainsKey(formulario.Name.ToLower() + "_titulo"))
            {
                formulario.Text = _dic[formulario.Name.ToLower() + "_titulo"];
            }

            TraducirControlesInternos(formulario.Controls);
        }

        private void TraducirControlesInternos(Control.ControlCollection controles)
        {
            foreach (Control c in controles)
            {
                string clave = c.Name.ToLower();
                if (_dic.ContainsKey(clave))
                {
                    c.Text = _dic[clave];
                }

                if (c is DataGridView dgv)
                {
                    foreach (DataGridViewColumn col in dgv.Columns)
                    {
                        string claveCol = col.Name.ToLower();
                        if (_dic.ContainsKey(claveCol))
                        {
                            col.HeaderText = _dic[claveCol];
                        }
                    }
                }

                if (c.Controls.Count > 0)
                {
                    TraducirControlesInternos(c.Controls);
                }
            }
        }

        private void gestionUsuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!SessionManager_43BO.Instancia.Permisos.Contains("GestionUsuarios_Acceso"))
            {
                MessageBox.Show(GetTexto("menu.msg_acceso_denegado"));
                return;
            }
            Gu_43BO = new GestionUs();
            Gu_43BO.MdiParent = this;
            Gu_43BO.Show();
            TraducirFormulario(Gu_43BO);
        }

        private void bitacoraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!SessionManager_43BO.Instancia.Permisos.Contains("Auditoria_Acceso"))
            {
                MessageBox.Show(GetTexto("menu.msg_acceso_denegado"));
                return;
            }
            Aud_43BO = new Auditoria();
            Aud_43BO.MdiParent = this;
            Aud_43BO.Show();
            TraducirFormulario(Aud_43BO);
        }

        private void gestionPerfilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!SessionManager_43BO.Instancia.Permisos.Contains("GestionPerfiles_Acceso"))
            {
                MessageBox.Show(GetTexto("menu.msg_acceso_denegado"));
                return;
            }
            Gperfiles = new GestionPerfiles();
            Gperfiles.MdiParent = this;
            Gperfiles.Show();
            TraducirFormulario(Gperfiles);
        }

        public void ActualizarIdioma_43BO(Dictionary<string, string> dic)
        {
            _dic = dic;
            TraducirMenu(menuStrip1.Items);

            foreach (Form hijo in this.MdiChildren)
            {
                TraducirFormulario(hijo);
            }
        }

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

        // 2. Controlamos por seguridad el clic de Cambio de Contraseña
        private void cambiarContraseñaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!SessionManager_43BO.Instancia.Permisos.Contains("Usuario_CambioContraseña"))
            {
                MessageBox.Show(GetTexto("menu.msg_acceso_denegado"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            CC_43BO = new CambioContraseña();
            CC_43BO.MdiParent = this;
            CC_43BO.Show();
            TraducirFormulario(CC_43BO);
        }

        // 3. Controlamos por seguridad el clic de Cambio de Idioma
        private void cambiarIdiomaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!SessionManager_43BO.Instancia.Permisos.Contains("Usuario_CambioIdioma"))
            {
                MessageBox.Show(GetTexto("menu.msg_acceso_denegado"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            new CambiarIdioma().ShowDialog(this);
        }

        private void reLoginToolStripMenuItem_Click(object sender, EventArgs e)
        {
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
                            // Volvemos a evaluar el menú completo con las patentes del nuevo usuario logueado
                            AsignadorPermisos_43BO.AplicarMenu(this.menuStrip1, _mapaMenu);
                        }
                    }
                }
            }
        }
    }
}