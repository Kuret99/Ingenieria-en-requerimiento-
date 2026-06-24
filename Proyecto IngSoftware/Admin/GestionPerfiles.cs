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
        private bool _modoGestionRoles = true;
        private Dictionary<string, string> _diccionario;
        private bool _editandoModo = false;
        private string _modoActual = "";
     

        private readonly Dictionary<string, Permisos_43BO> _mapaGestionPerfiles = new Dictionary<string, Permisos_43BO>
        {
            { "button7", Permisos_43BO.GestionPerfiles_AsignarRelaciones },
            { "button8", Permisos_43BO.GestionPerfiles_AsignarRelaciones },
            { "btnCrear", Permisos_43BO.GestionPerfiles_ConfigurarEstructura },
            { "btnModificar", Permisos_43BO.GestionPerfiles_ConfigurarEstructura },
            { "btnEliminarRol", Permisos_43BO.GestionPerfiles_ConfigurarEstructura }
        };

        public GestionPerfiles()
        {
            InitializeComponent();
            ConfigurarUI_43BO();
           
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

            this.Text = ObtenerTexto("gestionperfiles_titulo", "Gestión de Perfiles y Permisos");
            btnEliminarRol.Text = ObtenerTexto("gestionperfiles._eliminar_seleccionado", "Eliminar Seleccionado");

       
            btnCrear.Text = _editandoModo
                ? ObtenerTexto("gestionperfiles._finalizar", "✔ Finalizar")
                : ObtenerTexto("gestionperfiles._crear", "Crear");

            btnModificar.Text = _editandoModo
                ? ObtenerTexto("gestionperfiles._finalizar", "✔ Finalizar")
                : ObtenerTexto("gestionperfiles._modificar", "Modificar");
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

                if (treeView1.Nodes.Count > 0) treeView1.SelectedNode = treeView1.Nodes[0];
                else { _rolActual = null; RefrescarListas(); }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ObtenerTexto("msg_error_general", "Error al cargar datos: ") + ex.Message, ObtenerTexto("titulo_error", "Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarPantallaSegunModo()
        {
            treeView1.Nodes.Clear();

            string tituloModo = _modoGestionRoles ? ObtenerTexto("perfiles_modo_roles", "MODO ROLES") : ObtenerTexto("perfiles_modo_familias", "MODO FAMILIAS");
            groupBox1.Text = $"{ObtenerTexto("gestionperfiles._configuracion_de_relaciones", "Configurador de Relaciones")} - {tituloModo}";
            btnCambiarVista.Text = _modoGestionRoles ? ObtenerTexto("perfiles_cambiar_a_familias", "Cambiar a Vista Familias") : ObtenerTexto("perfiles_cambiar_a_roles", "Cambiar a Vista Roles");

            if (_modoGestionRoles)
            {
                foreach (var rol in _bll.ListarTodosLosRoles_43BO())
                {
                    _bll.HidratarRolCompleto_43BO(rol);
                  
                    TreeNode nodoRol = new TreeNode((rol is Familia_43BO ? "📁 " : "🔑 ") + rol.Nombre_43BO) { Tag = rol };
                    AgregarHijosAlArbol(nodoRol, rol);
                    treeView1.Nodes.Add(nodoRol);
                }
            }
            else
            {
                foreach (var fam in _bll.ListarTodasLasFamilias_43BO())
                {
                    _bll.HidratarFamiliaRecursivo_43BO(fam);
                    TreeNode nodoFam = new TreeNode((fam is Familia_43BO ? "📁 " : "🔑 ") + fam.Nombre_43BO) { Tag = fam };
                    AgregarHijosAlArbol(nodoFam, fam);
                    treeView1.Nodes.Add(nodoFam);
                }
            }
            treeView1.ExpandAll();
            AsignadorPermisos_43BO.Aplicar(this, _mapaGestionPerfiles);
            ControlarEstadoControles();
        }

        private void RefrescarListas()
        {
            if (_rolActual == null)
            {
                listBox1.DataSource = null;
                listBox2.DataSource = null;
                return;
            }

            try
            {
              
                listBox1.DataSource = null;
                listBox1.DataSource = _rolActual.ObtenerHijos_43BO();
                listBox1.DisplayMember = "Nombre_43BO";

                var catalogoGeneral = _bll.ListarTodasLasFamilias_43BO().Cast<Rol_43BO>()
                                          .Concat(_bll.ListarTodasLasPatentes_43BO().Cast<Rol_43BO>()).ToList();

                var idsAsignados = _rolActual.ObtenerHijos_43BO().Select(x => x.IdRol_43BO).ToList();

                listBox2.DataSource = null;
                listBox2.DataSource = catalogoGeneral
                                        .Where(x => !idsAsignados.Contains(x.IdRol_43BO) && x.IdRol_43BO != _rolActual.IdRol_43BO)
                                        .ToList();
                listBox2.DisplayMember = "Nombre_43BO";
            }
            catch (Exception ex)
            {
                _rolActual = null;
                MessageBox.Show("Error al cargar datos: " + ex.Message);
            }
        }



        private void AgregarHijosAlArbol(TreeNode nodoVisualPadre, Rol_43BO componentePadre)
        {
            if (componentePadre is Familia_43BO familia)
            {
                foreach (var hijo in familia.ObtenerHijos_43BO())
                {
                    TreeNode nodoHijo = new TreeNode((hijo is Familia_43BO ? "📁 " : "🔑 ") + hijo.Nombre_43BO) { Tag = hijo };

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
                groupBox2.Text = $"{ObtenerTexto("gestionperfiles._configuracion_de_relaciones", "Configuración de relaciones")} - {seleccionado.Nombre_43BO}";

                if (_modoGestionRoles) _bll.HidratarRolCompleto_43BO(_rolActual);
                else _bll.HidratarFamiliaRecursivo_43BO(_rolActual);

                RefrescarListas();
                ControlarEstadoControles();
            }
        }

        private void btnCambiarVista_Click(object sender, EventArgs e)
        {
            _editandoModo = false;
            _modoGestionRoles = !_modoGestionRoles;
            _rolActual = null;
            CargarDatos_43BO();
        }


        private void button7_Click(object sender, EventArgs e)
        {
            if (_rolActual == null)
            {
                MessageBox.Show(ObtenerTexto("msg_seleccionar_rol", "Por favor, seleccione un rol o familia."),
                                ObtenerTexto("titulo_atencion", "Atención"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (listBox2.Items.Count == 0 || listBox2.SelectedItem == null)
            {
                MessageBox.Show(ObtenerTexto("msg_seleccionar_item_a_agregar", "Debe seleccionar un componente de la lista para poder agregarlo."),
                                ObtenerTexto("titulo_atencion", "Atención"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var itemParaAgregar = (Rol_43BO)listBox2.SelectedItem;

            try
            {
                _bll.AgregarComponenteHijo_43BO(_rolActual, itemParaAgregar, _modoGestionRoles);

               
                if (_modoGestionRoles) _bll.HidratarRolCompleto_43BO(_rolActual);
                else _bll.HidratarFamiliaRecursivo_43BO(_rolActual);

                RefrescarListas();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString(), "Detalle del Error Real");

                string mensajeTraducido = ObtenerTexto(ex.Message, "Error en la estructura: " + ex.Message);
                MessageBox.Show(mensajeTraducido, ObtenerTexto("titulo_error", "Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                CargarDatos_43BO();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (_rolActual == null) return;
            if (listBox1.SelectedItem == null)
            {
                MessageBox.Show(ObtenerTexto("msg_seleccionar_item_a_quitar", "Debe seleccionar un componente de la lista para poder quitarlo."),
                                ObtenerTexto("titulo_atencion", "Atención"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                MessageBox.Show(ex.Message, ObtenerTexto("titulo_error", "Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_editandoModo)
                {
                    string titulo = ObtenerTexto("msg_inputbox_titulo", "Nuevo Componente");
                    string prompt = ObtenerTexto("msg_inputbox_prompt", "Ingrese el nombre:");
                    string nombre = Interaction.InputBox(prompt, titulo, "");

                    if (string.IsNullOrWhiteSpace(nombre)) return;

                    int nuevoId = _modoGestionRoles ? _bll.RegistrarRol_43BO(nombre) : _bll.RegistrarFamilia_43BO(nombre);
                    _rolActual = new Familia_43BO { IdRol_43BO = nuevoId, Nombre_43BO = nombre };

                    _editandoModo = true;
                    _modoActual = "CREAR";
                    RefrescarListas();
                    btnCrear.Text = ObtenerTexto("gestionperfiles._finalizar", "✔ Finalizar");
                }
                else if (_modoActual == "CREAR")
                {
                    if (_rolActual == null || _rolActual.ObtenerHijos_43BO().Count == 0)
                    {
                        MessageBox.Show(ObtenerTexto("msg_error_perfil_vacio", "No se puede finalizar la creación. Debe asignar al menos una patente o familia al perfil."),
                                        ObtenerTexto("titulo_atencion", "Atención"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
               

                    _editandoModo = false;
                    _modoActual = "";
                    _rolActual = null;

                    btnCrear.Text = ObtenerTexto("gestionperfiles._crear", "Crear");
                    CargarDatos_43BO();
                }
                ControlarEstadoControles();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ObtenerTexto(ex.Message, ex.Message), ObtenerTexto("titulo_error", "Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (_rolActual == null)
            {
                MessageBox.Show(ObtenerTexto("msg_seleccionar_rol", "Por favor, seleccione un rol o familia."));
                return;
            }

            try
            {
                if (!_editandoModo)
                {
                    string titulo = _modoGestionRoles
                        ? ObtenerTexto("gestionperfiles_modificar_rol", "Modify Role")
                        : ObtenerTexto("gestionperfiles_modificar_familia", "Modify Family");

                    string mensajeInput = ObtenerTexto("gestionperfiles_msg_ingrese_nuevo_nombre", "Enter new name:");
                    string nuevoNombre = Interaction.InputBox(mensajeInput, titulo, _rolActual.Nombre_43BO);
                    if (string.IsNullOrWhiteSpace(nuevoNombre)) return;
                    _bll.ModificarNombreComponente_43BO(_rolActual.IdRol_43BO, nuevoNombre, _modoGestionRoles);
                    _rolActual.Nombre_43BO = nuevoNombre;
                    _editandoModo = true;
                    _modoActual = "MODIFICAR";
                    btnModificar.Text = ObtenerTexto("gestionperfiles._finalizar", "✔ Finalizar");
                }
                else if (_modoActual == "MODIFICAR")
                {
                   
                    if (_rolActual == null || _rolActual.ObtenerHijos_43BO().Count == 0)
                    {
                        MessageBox.Show(ObtenerTexto("msg_error_perfil_vacio", "No se puede finalizar la modificación. El perfil no puede quedar vacío, debe tener al menos una patente o familia."),
                                        ObtenerTexto("titulo_atencion", "Atención"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return; 
                    }
            

                    _editandoModo = false;
                    _modoActual = "";
                    CargarDatos_43BO();
                    btnModificar.Text = ObtenerTexto("gestionperfiles._modificar", "Modificar");
                }

                AplicarTextosEstaticos();
                ControlarEstadoControles();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ObtenerTexto(ex.Message, ex.Message),
                                ObtenerTexto("titulo_error", "Error"),
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
           
        }
        

        private void btnEliminarRol_Click(object sender, EventArgs e)
        {
            if (_rolActual == null) return;

            try
            {
                if (_modoGestionRoles)
                    _bll.EliminarRol_43BO(_rolActual.IdRol_43BO);
                else
                    _bll.EliminarFamilia_43BO(_rolActual.IdRol_43BO);

                _rolActual = null;
                CargarDatos_43BO();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ObtenerTexto(ex.Message, ex.Message),
                                ObtenerTexto("titulo_error", "Error"),
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }


        private void ControlarEstadoControles()
        {
            bool tieneSeleccion = (_rolActual != null);
            listBox1.Enabled = tieneSeleccion && _editandoModo;
            listBox2.Enabled = tieneSeleccion && _editandoModo;
            button7.Enabled = tieneSeleccion && _editandoModo;
            button8.Enabled = tieneSeleccion && _editandoModo;

            if (_editandoModo)
            {
                //si edito, esto hace que solo se pueda hacer una accion a la vez, o sea crear o modificar
                btnCrear.Enabled = (_modoActual == "CREAR");
                btnModificar.Enabled = (_modoActual == "MODIFICAR");
            }
            else
            {
                // y aca si no estoy editando, habilito los botones de crear y modificar si hay seleccion
                btnModificar.Enabled = tieneSeleccion;
                btnCrear.Enabled = true;
            }

            btnEliminarRol.Enabled = tieneSeleccion && !_editandoModo;
            btnCambiarVista.Enabled = !_editandoModo;
            treeView1.Enabled = !_editandoModo;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e) { }
        private void listBox2_SelectedIndexChanged(object sender, EventArgs e) { }
    }
}