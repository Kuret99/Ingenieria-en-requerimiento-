using BLL;
using GUI_43BO;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Servicios;
using Servicios.IidiomaObserver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Proyecto_IngSoftware
{
    public partial class GestionPerfiles : Form, IdiomaObserver_43BO
    {
        private readonly BLLpatente_43BO _bll = new BLLpatente_43BO();
        private List<Patente_43BO> _patentes = new List<Patente_43BO>();
        private Familia_43BO _rolActual = null;
        private Rol_43BO _hijoActual = null;
        private bool _modoGestionRoles = true;
        private Dictionary<string, string> _diccionario;
        private bool _editandoModo = false; // <-- Controla el estado de edición de la pantalla

        private readonly Dictionary<string, Permisos_43BO> _mapaGestionPerfiles = new Dictionary<string, Permisos_43BO>
        {
            { "button7", Permisos_43BO.GestionPerfiles_AsignarRelaciones },      // Botón <<--
            { "button8", Permisos_43BO.GestionPerfiles_AsignarRelaciones },      // Botón -->>
            { "btnCrear", Permisos_43BO.GestionPerfiles_ConfigurarEstructura },     // Crear Rol/Familia
            { "btnModificar", Permisos_43BO.GestionPerfiles_ConfigurarEstructura }, // Modificar Rol/Familia
            { "btnEliminarRol", Permisos_43BO.GestionPerfiles_ConfigurarEstructura } // Eliminar Rol/Familia
        };

        public GestionPerfiles()
        {
            InitializeComponent();
            ConfigurarUI_43BO();

            // Cargo idioma inicial 
            try
            {
                string json = GestorArchivosIdioma_43BO.ObtenerContenidoJson_43BO("es");
                _diccionario = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                AplicarTextosEstaticos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar idioma inicial: " + ex.Message);
            }

            // Se suscribe a los cambios de idioma
            GestorIdioma_43BO.Instancia.Suscribir_43BO(this);

            CargarDatos_43BO();
        }

        public void ActualizarIdioma_43BO(Dictionary<string, string> dic)
        {
            _diccionario = dic;
            AplicarTextosEstaticos();
        }

        private string ObtenerTexto(string clave, string porDefecto)
        {
            return _diccionario != null && _diccionario.ContainsKey(clave) ? _diccionario[clave] : porDefecto;
        }

        private void AplicarTextosEstaticos()
        {
            if (_diccionario == null) return;

            this.Text = ObtenerTexto("perfiles_titulo", "Gestión de Perfiles y Permisos (Composite)");
            btnEliminarRol.Text = ObtenerTexto("perfiles_btn_eliminar", "Eliminar Seleccionado");

            ConfigurarPantallaSegunModo();
        }

        private void ConfigurarUI_43BO()
        {
            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
            listBox2.SelectedIndexChanged += listBox2_SelectedIndexChanged;
        }

        private void CargarDatos_43BO()
        {
            try
            {
                _patentes = _bll.ListarTodasLasPatentes_43BO();
                ConfigurarPantallaSegunModo();

                if (treeView1.Nodes.Count > 0)
                {
                    treeView1.SelectedNode = treeView1.Nodes[0];
                }
                else
                {
                    _rolActual = null;
                    RefrescarListas();
                }
            }
            catch (Exception ex)
            {
                string msgError = ObtenerTexto("auditoria_msg_error_cargar", "Error al cargar datos: ");
                MessageBox.Show(msgError + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarPantallaSegunModo()
        {
            treeView1.Nodes.Clear();

            if (_modoGestionRoles)
            {
                groupBox1.Text = ObtenerTexto("perfiles_modo_roles", "Configurador de Relaciones - MODO ROLES");
                btnCambiarVista.Text = ObtenerTexto("perfiles_btn_cambiar_familias", "Cambiar a Vista Familias");
                btnCrear.Text = ObtenerTexto("perfiles_nuevo_rol", "Nuevo Rol");

                // Si está editando mantiene el texto de confirmación, sino usa el dinámico
                btnModificar.Text = _editandoModo ? "✔ Finalizar Edición" : ObtenerTexto("perfiles_modificar_rol", "Modificar Rol");

                foreach (var rol in _bll.ListarTodosLosRoles_43BO())
                {
                    _bll.HidratarRolCompleto_43BO(rol);
                    TreeNode nodoRol = new TreeNode(rol.Nombre_43BO) { Tag = rol };
                    AgregarHijosAlArbol(nodoRol, rol);
                    treeView1.Nodes.Add(nodoRol);
                }
            }
            else
            {
                groupBox1.Text = ObtenerTexto("perfiles_modo_familias", "Configurador de Relaciones - MODO FAMILIAS");
                btnCambiarVista.Text = ObtenerTexto("perfiles_btn_cambiar_roles", "Cambiar a Vista Roles");
                btnCrear.Text = ObtenerTexto("perfiles_nueva_familia", "Nueva Familia");

                btnModificar.Text = _editandoModo ? "✔ Finalizar Edición" : ObtenerTexto("perfiles_modificar_familia", "Modificar Familia");

                foreach (var fam in _bll.ListarTodasLasFamilias_43BO())
                {
                    _bll.HidratarFamiliaRecursivo_43BO(fam);
                    TreeNode nodoFam = new TreeNode(fam.Nombre_43BO) { Tag = fam };
                    AgregarHijosAlArbol(nodoFam, fam);
                    treeView1.Nodes.Add(nodoFam);
                }
            }
            treeView1.ExpandAll();

            // Aplicamos los permisos y actualizamos estados visuales
            AsignadorPermisos_43BO.Aplicar(this, _mapaGestionPerfiles);
            ControlarEstadoControles();
        }

        private void RefrescarListas()
        {
            if (_rolActual == null)
            {
                listBox1.DataSource = null;
                listBox1.Items.Clear();
                listBox2.DataSource = null;
                listBox2.Items.Clear();
                return;
            }

            listBox1.DataSource = null;
            listBox1.Items.Clear();
            var hijos = _rolActual.ObtenerHijos_43BO();
            listBox1.DataSource = hijos;
            listBox1.DisplayMember = "Nombre_43BO";

            listBox2.DataSource = null;
            listBox2.Items.Clear();

            var todasLasFamilias = _bll.ListarTodasLasFamilias_43BO().Cast<Rol_43BO>().ToList();
            var todasLasPatentes = _bll.ListarTodasLasPatentes_43BO().Cast<Rol_43BO>().ToList();
            var catalogoGeneral = todasLasFamilias.Concat(todasLasPatentes).ToList();

            var idsAsignados = hijos.Select(x => x.IdRol_43BO).ToList();
            var disponibles = catalogoGeneral
                .Where(x => !idsAsignados.Contains(x.IdRol_43BO) && x.IdRol_43BO != _rolActual.IdRol_43BO)
                .ToList();

            listBox2.DataSource = disponibles;
            listBox2.DisplayMember = "Nombre_43BO";
        }

        private void AgregarHijosAlArbol(TreeNode nodoVisualPadre, Rol_43BO componentePadre)
        {
            if (componentePadre is Familia_43BO familia)
            {
                foreach (var hijo in familia.ObtenerHijos_43BO())
                {
                    TreeNode nodoHijo = hijo is Familia_43BO ? new TreeNode($"📁 {hijo.Nombre_43BO}") { Tag = hijo } : new TreeNode($"🔑 {hijo.Nombre_43BO}") { Tag = hijo };
                    if (hijo is Familia_43BO sub) AgregarHijosAlArbol(nodoHijo, sub);
                    nodoVisualPadre.Nodes.Add(nodoHijo);
                }
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node?.Tag is Familia_43BO seleccionado)
            {
                _rolActual = seleccionado;
                string prefijoRelaciones = ObtenerTexto("perfiles_lbl_config_relaciones", "Configuración de Relaciones");
                groupBox2.Text = $"{prefijoRelaciones} - {seleccionado.Nombre_43BO}";

                if (_modoGestionRoles) _bll.HidratarRolCompleto_43BO(_rolActual);
                else _bll.HidratarFamiliaRecursivo_43BO(_rolActual);

                RefrescarListas();
                ControlarEstadoControles(); // <-- Evaluamos bloqueos al cambiar de nodo
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e) { }
        private void listBox2_SelectedIndexChanged(object sender, EventArgs e) { }

        private void btnCambiarVista_Click(object sender, EventArgs e)
        {
            _editandoModo = false; // Cancelamos edición si cambia de vista
            _modoGestionRoles = !_modoGestionRoles;
            _rolActual = null;
            CargarDatos_43BO();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedItem == null)
            {
                MessageBox.Show(ObtenerTexto("perfiles_msg_seleccionar_primero", "Por favor, selecciona un elemento primero."));
                return;
            }

            var itemParaAgregar = (Rol_43BO)listBox2.SelectedItem;
            int idRolEditado = _rolActual.IdRol_43BO;

            try
            {
                _bll.AgregarComponenteHijo_43BO(_rolActual, itemParaAgregar, _modoGestionRoles);

                //  
                if (_modoGestionRoles) _bll.HidratarRolCompleto_43BO(_rolActual);
                else _bll.HidratarFamiliaRecursivo_43BO(_rolActual);

                RefrescarListas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de validación: " + ex.Message);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (_rolActual == null || listBox1.SelectedItem == null)
            {
                MessageBox.Show(ObtenerTexto("perfiles_msg_seleccionar_quitar", "Por favor, selecciona un elemento para quitar."));
                return;
            }

            var itemParaQuitar = (Rol_43BO)listBox1.SelectedItem;

            try
            {
                _bll.QuitarComponenteHijo_43BO(_rolActual, itemParaQuitar, _modoGestionRoles);

               
                if (_modoGestionRoles) _bll.HidratarRolCompleto_43BO(_rolActual);
                else _bll.HidratarFamiliaRecursivo_43BO(_rolActual);

                RefrescarListas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al quitar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            string titulo = _modoGestionRoles ? ObtenerTexto("perfiles_nuevo_rol", "Nuevo Rol") : ObtenerTexto("perfiles_nueva_familia", "Nueva Familia");
            string nombre = Interaction.InputBox(ObtenerTexto("perfiles_msg_ingrese_nombre", "Ingrese el nombre:"), titulo, "");

            if (string.IsNullOrWhiteSpace(nombre)) return;

            try
            {
                if (_modoGestionRoles) _bll.RegistrarRol_43BO(nombre);
                else _bll.RegistrarFamilia_43BO(nombre);

                CargarDatos_43BO();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al crear: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (_rolActual == null)
            {
                MessageBox.Show(ObtenerTexto("perfiles_msg_seleccionar_modificar", "Por favor, selecciona un elemento del árbol para modificar."));
                return;
            }

            if (!_editandoModo)
            {

                string titulo = _modoGestionRoles ? ObtenerTexto("perfiles_modificar_rol", "Modificar Rol") : ObtenerTexto("perfiles_modificar_familia", "Modificar Familia");
                string nuevoNombre = Interaction.InputBox(ObtenerTexto("perfiles_msg_ingrese_nuevo_nombre", "Ingrese el nuevo nombre:"), titulo, _rolActual.Nombre_43BO);

                if (!string.IsNullOrWhiteSpace(nuevoNombre) && nuevoNombre != _rolActual.Nombre_43BO)
                {
                    try
                    {
                        _bll.ModificarNombreComponente_43BO(_rolActual.IdRol_43BO, nuevoNombre, _modoGestionRoles);

                        //is no exploto, actualizo el estado en memoria
                        _rolActual.Nombre_43BO = nuevoNombre;

                       
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al modificar el nombre: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                _editandoModo = true;
                // cambio el btn para que diga finaliar edicion (no sabia qu podia copair y pegar un emoji)
                btnModificar.Text = "✔ Finalizar Edición";
            }
            else
            {
             
                _editandoModo = false;
                btnModificar.Text = _modoGestionRoles
                    ? ObtenerTexto("perfiles_modificar_rol", "Modificar Rol")
                    : ObtenerTexto("perfiles_modificar_familia", "Modificar Familia");

            
                int idGuardado = _rolActual.IdRol_43BO;
                CargarDatos_43BO();

               
                foreach (TreeNode nodo in treeView1.Nodes)
                {
                    if (nodo.Tag is Rol_43BO r && r.IdRol_43BO == idGuardado)
                    {
                        treeView1.SelectedNode = nodo;
                        break;
                    }
                }
            }
            ControlarEstadoControles();
        }

        private void btnEliminarRol_Click(object sender, EventArgs e)
        {
            if (_rolActual == null) return;
            if (_modoGestionRoles) _bll.EliminarRol_43BO(_rolActual.IdRol_43BO); else _bll.EliminarFamilia_43BO(_rolActual.IdRol_43BO);
            _rolActual = null;
            CargarDatos_43BO();
        }

        private void ControlarEstadoControles()
        {
            bool tieneSeleccion = (_rolActual != null);


            listBox1.Enabled = tieneSeleccion && _editandoModo;
            listBox2.Enabled = tieneSeleccion && _editandoModo;
            button7.Enabled = tieneSeleccion && _editandoModo; // Botón <<--
            button8.Enabled = tieneSeleccion && _editandoModo; // Botón -->>

         
            btnModificar.Enabled = tieneSeleccion;
            btnCrear.Enabled = !_editandoModo;       
            btnEliminarRol.Enabled = tieneSeleccion && !_editandoModo;
            btnCambiarVista.Enabled = !_editandoModo;

        
            treeView1.Enabled = !_editandoModo;
        }
    }
}