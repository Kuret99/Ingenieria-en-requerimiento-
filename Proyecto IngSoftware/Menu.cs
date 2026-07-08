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
                    // el filtro de permisos del menu va SIEMPRE, tenga o no la contra de fabrica
                    // sino el usuario ve submenus que no le corresponden
                    AsignadorPermisos_43BO.AplicarMenu(this.menuStrip1, _mapaMenu);

                    if (bll.EsContraseñaDeFabrica_43BO(usuarioLogueado))
                    {
                        MessageBox.Show(ObtenerTexto("menu_msg_cambiar_contra_fabrica"), ObtenerTexto("titulo_atencion"), MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        // abro directo el cambio de contraseña (antes por error se abria el de idioma)
                        // al abrirlo por codigo no pasa por el permiso del menu, asi que funciona
                        // aunque el usuario no tenga la patente Usuario_CambioContraseña
                        CambioContraseña frmCambio = new CambioContraseña();
                        frmCambio.MdiParent = this;
                        frmCambio.Show();
                        TraducirFormulario(frmCambio);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ObtenerTexto(ex.Message), ObtenerTexto("titulo_error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show(ObtenerTexto("menu.msg_acceso_denegado"));
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
                MessageBox.Show(ObtenerTexto("menu.msg_acceso_denegado"));
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
                MessageBox.Show(ObtenerTexto("menu.msg_acceso_denegado"));
                return;
            }
            Gperfiles = new GestionPerfiles();
            Gperfiles.MdiParent = this;
            Gperfiles.Show();
            TraducirFormulario(Gperfiles);
        }

        // backup bd, solo lo puede abrir el admin
        private void backupBDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var usuario = SessionManager_43BO.Instancia.Usuario;

            if (usuario == null || usuario.Rol == null || usuario.Rol.Nombre_43BO == null ||
                !usuario.Rol.Nombre_43BO.Trim().Equals("Administrador", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show(ObtenerTexto("menu.msg_acceso_denegado"), ObtenerTexto("titulo_error", "Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            BackupBD_43BO frmBackup = new BackupBD_43BO();
            frmBackup.MdiParent = this;
            frmBackup.Show();
            TraducirFormulario(frmBackup);
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

        // unico punto de traduccion: todo pasa por el gestor
        private string ObtenerTexto(string clave, string porDefecto = null)
            => GestorIdioma_43BO.Instancia.ObtenerTexto_43BO(clave, porDefecto ?? clave);

        private void cerrarSesiobnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(ObtenerTexto("menu_msg_confirmar_cerrar_sesion"), ObtenerTexto("titulo_confirmar", "Confirmar"), MessageBoxButtons.YesNo) == DialogResult.Yes)
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
                MessageBox.Show(ObtenerTexto("menu.msg_acceso_denegado"), ObtenerTexto("titulo_error", "Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show(ObtenerTexto("menu.msg_acceso_denegado"), ObtenerTexto("titulo_error", "Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        // reevaluo el menu con las patentes del nuevo usuario, siempre
                        AsignadorPermisos_43BO.AplicarMenu(this.menuStrip1, _mapaMenu);

                        if (bll.EsContraseñaDeFabrica_43BO(usuarioLogueado))
                        {
                            MessageBox.Show(ObtenerTexto("menu_msg_cambiar_contra_fabrica"), ObtenerTexto("titulo_atencion"), MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            // aca tambien va el cambio de contraseña, no el de idioma
                            CambioContraseña frmCambio = new CambioContraseña();
                            frmCambio.MdiParent = this;
                            frmCambio.Show();
                            TraducirFormulario(frmCambio);
                        }
                    }
                }
            }
        }
    }
}