using BLL;
using GUI_43BO;
using Servicios;
using Servicios.IidiomaObserver;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Proyecto_IngSoftware
{
    public partial class GestionUs : Form, IdiomaObserver_43BO
    {
        private BLL.BllUser_43BO blluser = new BLL.BllUser_43BO();
        private BLL.BLLBitacora_43BO bllBitacora = new BLL.BLLBitacora_43BO();
        private List<User_43BO> todoslosusuarios;
        private bool Modificar_43BO = false;
        private BLLpatente_43BO bllpatente = new BLLpatente_43BO();
        private Dictionary<string, string> _diccionario;

        private readonly Dictionary<string, Permisos_43BO> _mapaGestionUsuarios = new Dictionary<string, Permisos_43BO>
        {
            { "btnCrear", Permisos_43BO.GestionUsuarios_Alta },
            { "btnModi", Permisos_43BO.GestionUsuarios_Modificar },
            { "btnDes", Permisos_43BO.GestionUsuarios_Desbloquear },
            { "btnAct", Permisos_43BO.GestionUsuarios_ActivarDesactivar },
            { "btnApli", Permisos_43BO.GestionUsuarios_Aplicar }
        };

        public GestionUs()
        {
            InitializeComponent();
            GestorIdioma_43BO.Instancia.Suscribir_43BO(this);
            Btns_43BO();
            ActualizarDGV_43BO();
            ConfigurarComboBoxRoles_43BO();

            AsignadorPermisos_43BO.Aplicar(this, _mapaGestionUsuarios);
        }

        public void ActualizarIdioma_43BO(Dictionary<string, string> dic)
        {
            _diccionario = dic;

            // Título del formulario
            this.Traducir(dic, "gestion_usuarios_titulo");

            // RadioButtons
            rbActivos.Traducir(dic, "gestion_usuarios_rb_activos");
            rbTodos.Traducir(dic, "gestion_usuarios_rb_todos");
            rbBloqueados.Traducir(dic, "gestion_usuarios_rb_bloqueados");

            // Labels (Asegurate de que estos sean los nombres de tus labels en el Designer)
            label1.Traducir(dic, "dgv_dni");
            label2.Traducir(dic, "dgv_nombre");
            label3.Traducir(dic, "dgv_apellido");
            label4.Traducir(dic, "dgv_rol");
            label5.Traducir(dic, "dgv_email");

            // Botones
            btnCrear.Traducir(dic, "gestion_usuarios_btn_crear");
            btnDes.Traducir(dic, "gestion_usuarios_btn_desbloquear");
            btnModi.Traducir(dic, "gestion_usuarios_btn_modificar");
            btnAct.Traducir(dic, "gestion_usuarios_btn_act_desact");
            btnApli.Traducir(dic, "gestion_usuarios_btn_aplicar");
            btnCanc.Traducir(dic, "gestion_usuarios_btn_cancelar");
            btnSalir.Traducir(dic, "gestion_usuarios_btn_salir");

            // La grilla siempre al final para mantener el formato
            FormatoDgv_43BO();
        }

        private string ObtenerTexto(string clave, string porDefecto)
        {
            return _diccionario != null && _diccionario.ContainsKey(clave) ? _diccionario[clave] : porDefecto;
        }

        private void ConfigurarComboBoxRoles_43BO()
        {
            try
            {
                var rolesReales = bllpatente.ListarTodosLosRoles_43BO();
                cmbRol.DataSource = rolesReales;
                cmbRol.DisplayMember = "Nombre_43BO";
                cmbRol.ValueMember = "IdRol_43BO";
                cmbRol.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ObtenerTexto("gestionus_error_al_cargar_los_roles_desde_la_bd", "Error al cargar los roles desde la BD: ") + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Btns_43BO()
        {
            btnModi.Enabled = false; btnDes.Enabled = false; btnAct.Enabled = false;
            btnCrear.Enabled = true; btnApli.Enabled = false; btnSalir.Enabled = true;
        }

        private void FormatoDgv_43BO()
        {
            if (dgvUsaurio.Columns.Count == 0) return;

            void SetHeader(string col, string key, string defaultText)
            {
                if (dgvUsaurio.Columns.Contains(col))
                    dgvUsaurio.Columns[col].HeaderText = ObtenerTexto(key, defaultText);
            }

            SetHeader("DNI_43BO", "dgv_dni", "DNI");
            SetHeader("Nombre_43BO", "dgv_nombre", "Nombre");
            SetHeader("Apellido_43BO", "dgv_apellido", "Apellido");
            SetHeader("Email_43BO", "dgv_email", "Email");
            SetHeader("RolNombre", "dgv_rol", "Rol");
            SetHeader("Activo_43BO", "dgv_activo", "Activo");
            SetHeader("Bloqueado_43BO", "dgv_bloqueado", "Bloqueado");

            if (dgvUsaurio.Columns.Contains("RolNombre"))
                dgvUsaurio.Columns["RolNombre"].DisplayIndex = 4;

            foreach (DataGridViewRow fila in dgvUsaurio.Rows)
            {
                bool activo = Convert.ToBoolean(fila.Cells["Activo_43BO"].Value);
                bool bloqueado = Convert.ToBoolean(fila.Cells["Bloqueado_43BO"].Value);

                if (bloqueado) fila.DefaultCellStyle.BackColor = Color.Khaki;
                else if (!activo) fila.DefaultCellStyle.BackColor = Color.LightCoral;
                else fila.DefaultCellStyle.BackColor = Color.White;
            }
        }

        private void ActualizarDGV_43BO()
        {
            try
            {
                todoslosusuarios = blluser.ListarUsuarios_43BO();
                var lista = rbActivos.Checked ? todoslosusuarios.Where(u => u.Activo_43BO).ToList() :
                            rbBloqueados.Checked ? todoslosusuarios.Where(u => u.Bloqueado_43BO).ToList() : todoslosusuarios;

                dgvUsaurio.DataSource = lista.Select(u => new {
                    u.DNI_43BO,
                    u.Nombre_43BO,
                    u.Apellido_43BO,
                    u.Email_43BO,
                    RolNombre = u.Rol != null ? u.Rol.Nombre_43BO : ObtenerTexto("gestionus_sin_rol", "Sin Rol"),
                    u.Bloqueado_43BO,
                    u.Activo_43BO
                }).ToList();
                FormatoDgv_43BO();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ObtenerTexto("msg_error_general", "Error: ") + ex.Message, ObtenerTexto("titulo_error", "Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvUsaurio_CellClick_43BO(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvUsaurio.CurrentRow != null)
            {
                btnModi.Enabled = true; btnDes.Enabled = true; btnAct.Enabled = true; btnCrear.Enabled = false;
                AsignadorPermisos_43BO.Aplicar(this, _mapaGestionUsuarios);

                txtDNI.Text = dgvUsaurio.CurrentRow.Cells["DNI_43BO"].Value.ToString();
                txtNom.Text = dgvUsaurio.CurrentRow.Cells["Nombre_43BO"].Value.ToString();
                txtApe.Text = dgvUsaurio.CurrentRow.Cells["Apellido_43BO"].Value.ToString();
                txtEmail.Text = dgvUsaurio.CurrentRow.Cells["Email_43BO"].Value.ToString();
                cmbRol.SelectedValue = todoslosusuarios.FirstOrDefault(u => u.DNI_43BO.ToString() == txtDNI.Text)?.Rol?.IdRol_43BO ?? -1;

                txtDNI.Enabled = false; txtNom.Enabled = false; txtApe.Enabled = false; cmbRol.Enabled = false; txtEmail.Enabled = false;
            }
        }

        private void btnCrear_Click_1(object sender, EventArgs e)
        {
            btnApli.Enabled = true; txtDNI.Enabled = true; txtNom.Enabled = true;
            txtApe.Enabled = true; cmbRol.Enabled = true; txtEmail.Enabled = true;
            Modificar_43BO = false;
        }

        private void btnModi_Click(object sender, EventArgs e)
        {
            Modificar_43BO = true; btnApli.Enabled = true; btnCrear.Enabled = false;
            txtApe.Enabled = false; txtDNI.Enabled = false; txtNom.Enabled = false;
            cmbRol.Enabled = true; txtEmail.Enabled = true;
        }

        private void btnApli_Click(object sender, EventArgs e)
        {
            try
            {
                Rol_43BO rol = (Rol_43BO)cmbRol.SelectedItem;
                if (Modificar_43BO)
                    blluser.ModificarUser_43BO(int.Parse(txtDNI.Text), rol, txtEmail.Text);
                else
                    blluser.InsertarUser_43BO(int.Parse(txtDNI.Text), txtNom.Text, txtApe.Text, rol, txtEmail.Text);

                MessageBox.Show(ObtenerTexto("gestion_usuarios_msg_exito", "Operación exitosa."), ObtenerTexto("titulo_excelente", "Éxito"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                ActualizarDGV_43BO();
                Btns_43BO();
                AsignadorPermisos_43BO.Aplicar(this, _mapaGestionUsuarios);
            }
            catch (Exception ex)
            {
                string mensajeError = ex.Message;

                // Si el mensaje tiene un "|" significa que trae parametros
                if (mensajeError.Contains("|"))
                {
                    string[] partes = mensajeError.Split('|');
                    //se traduce la llave
                    mensajeError = string.Format(ObtenerTexto(partes[0], partes[0]), partes[1]);
                }
                else
                {
                    // Traducimos el error comun
                    mensajeError = ObtenerTexto(mensajeError, mensajeError);
                }

                MessageBox.Show(ObtenerTexto("msg_error_general", "Se produjo un error: ") + mensajeError,
                                ObtenerTexto("msg_titulo_error", "Error"),
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private void btnCanc_Click(object sender, EventArgs e)
        {
            Modificar_43BO = false; btnApli.Enabled = false; btnCrear.Enabled = true;
            txtDNI.Clear(); txtNom.Clear(); txtApe.Clear(); txtEmail.Clear();
            cmbRol.SelectedIndex = -1;
            Btns_43BO();
            AsignadorPermisos_43BO.Aplicar(this, _mapaGestionUsuarios);
        }

        private void btnAct_Click(object sender, EventArgs e)
        {
            try
            {
                int dni = int.Parse(txtDNI.Text);
                bool act = (bool)dgvUsaurio.CurrentRow.Cells["Activo_43BO"].Value;
                blluser.Eliminar_43BO(dni, !act);
                ActualizarDGV_43BO();
                Btns_43BO();
                AsignadorPermisos_43BO.Aplicar(this, _mapaGestionUsuarios);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ObtenerTexto("msg_error_general", "Error: ") + ex.Message, ObtenerTexto("titulo_error", "Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDes_Click(object sender, EventArgs e)
        {
            try
            {
                blluser.DesbloquearUser_43BO(int.Parse(txtDNI.Text));
                ActualizarDGV_43BO();
                Btns_43BO();
                AsignadorPermisos_43BO.Aplicar(this, _mapaGestionUsuarios);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ObtenerTexto("msg_error_general", "Error: ") + ex.Message, ObtenerTexto("titulo_error", "Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e) => this.Close();
        private void rbActivos_CheckedChanged(object sender, EventArgs e) => ActualizarDGV_43BO();
        private void rbTodos_CheckedChanged(object sender, EventArgs e) => ActualizarDGV_43BO();
        private void rbBloqueados_CheckedChanged(object sender, EventArgs e) => ActualizarDGV_43BO();
    }
}