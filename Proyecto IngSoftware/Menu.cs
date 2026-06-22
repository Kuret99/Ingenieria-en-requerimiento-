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
                        AsignadorPermisos_43BO.AplicarMenu(this.menuStrip1, _mapaMenu);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(GetTexto(ex.Message), GetTexto("titulo_error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 1. TRADUCE LA BARRA DE ARRIBA Y SUBMENÚS (Se corrigió la clave para que machee "menu.admin", "menu.usuario", etc.)
        private void TraducirMenu(ToolStripItemCollection items)
        {
            foreach (ToolStripItem item in items)
            {

                string clave = "menu." + item.Name.ToLower().Replace("toolstripmenuitem", "");

                // --- COPIA ESTA LÍNEA PARA VER LA VERDAD ---
                System.Diagnostics.Debug.WriteLine("Control: " + item.Name + " | Llave generada por el sistema: " + clave);
                // -------------------------------------------


                //string clave = "menu." + item.Name.ToLower().Replace("toolstripmenuitem", "");

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

        // 2. NUEVA FUNCIÓN GLOBAL: TRADUCE TEXTBOXES, LABELS, BOTONES Y DATAGRIDVIEWS DE CUALQUIER FORMhijo
        public void TraducirFormulario(Form formulario)
        {
            if (_dic == null) return;

            // Traducir el título de la ventana del formulario
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
                // Buscar clave directa basada en el nombre del control (ej: "lbl_dni", "btn_apply")
                string clave = c.Name.ToLower();
                if (_dic.ContainsKey(clave))
                {
                    c.Text = _dic[clave];
                }

                // Si es un DataGridView, traduce los headers de las columnas
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

                // Recursividad por si los controles están adentro de Paneles, GroupBox, etc.
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
            TraducirFormulario(Gu_43BO); // <--- Aplica la traducción completa al abrir
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
            TraducirFormulario(Aud_43BO); // <--- Aplica la traducción completa al abrir
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
            TraducirFormulario(Gperfiles); // <--- Aplica la traducción completa al abrir
        }

        public void ActualizarIdioma_43BO(Dictionary<string, string> dic)
        {
            _dic = dic;
            TraducirMenu(menuStrip1.Items);

            // Si hay ventanas hijas abiertas cuando cambian el idioma, las vuelve a traducir en caliente
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

        private void cambiarContraseñaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CC_43BO = new CambioContraseña();
            CC_43BO.MdiParent = this;
            CC_43BO.Show();
            TraducirFormulario(CC_43BO);
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
                            AsignadorPermisos_43BO.AplicarMenu(this.menuStrip1, _mapaMenu);
                        }
                    }
                }
            }
        }

        private void cambiarIdiomaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new CambiarIdioma().ShowDialog(this);
        }
    }
}