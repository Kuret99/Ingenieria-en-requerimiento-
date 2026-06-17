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
        BLL.BllUser_43BO blluser = new BLL.BllUser_43BO();
        BLL.BLLBitacora_43BO bllBitacora = new BLL.BLLBitacora_43BO();
        List<User_43BO> todoslosusuarios;
        private bool Modificar_43BO = false;
        private Dictionary<string, string> _dic;
        BLLpatente_43BO bllpatente = new BLLpatente_43BO();

        // ══════════════════════════════════════════════════════════════════
        // DICCIONARIO DE GESTIÓN DE USUARIOS (Mapeado con tu Enum Real)
        // ══════════════════════════════════════════════════════════════════
        private readonly Dictionary<string, Permisos_43BO> _mapaGestionUsuarios = new Dictionary<string, Permisos_43BO>
        {
            { "btnCrear", Permisos_43BO.GestionUsuarios_Alta },
            { "btnModi", Permisos_43BO.GestionUsuarios_Modificar },
            { "btnDes", Permisos_43BO.GestionUsuarios_Desbloquear },
            { "btnAct", Permisos_43BO.GestionUsuarios_ActivarDesactivar }
        };

        public GestionUs()
        {
            InitializeComponent();
            GestorIdioma_43BO.Instancia.Suscribir_43BO(this);
            Btns_43BO();
            ActualizarDGV_43BO();
            ConfigurarComboBoxRoles_43BO();

            // [Lugar 1] Control inicial al abrir la pantalla
            AsignadorPermisos_43BO.Aplicar(this, _mapaGestionUsuarios);
        }

        // --- IMPLEMENTACIÓN DEL PATRÓN OBSERVER ---
        public void ActualizarIdioma_43BO(Dictionary<string, string> dic)
        {
            _dic = dic;

            // Traduce controles 
            this.Text = GetTexto("gestion_usuarios_titulo");
            rbActivos.Text = GetTexto("gestion_usuarios_rb_activos");
            rbTodos.Text = GetTexto("gestion_usuarios_rb_todos");
            rbBloqueados.Text = GetTexto("gestion_usuarios_rb_bloqueados");
            btnCrear.Text = GetTexto("gestion_usuarios_btn_crear");
            btnDes.Text = GetTexto("gestion_usuarios_btn_desbloquear");
            btnModi.Text = GetTexto("gestion_usuarios_btn_modificar");
            btnAct.Text = GetTexto("gestion_usuarios_btn_act_desact");
            btnApli.Text = GetTexto("gestion_usuarios_btn_aplicar");
            btnCanc.Text = GetTexto("gestion_usuarios_btn_cancelar");
            btnSalir.Text = GetTexto("gestion_usuarios_btn_salir");

            FormatoDgv_43BO();
        }

        private string GetTexto(string key)
        {
            if (_dic != null && _dic.ContainsKey(key))
                return _dic[key];
            return key;
        }

        // Desuscripción activa para evitar memory leaks
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            GestorIdioma_43BO.Instancia.Desuscribir_43BO(this);
            base.OnFormClosing(e);
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
                MessageBox.Show("Error al cargar los roles desde la BD: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Btns_43BO()
        {
            btnModi.Enabled = false; btnDes.Enabled = false; btnAct.Enabled = false;
            btnCrear.Enabled = true; btnApli.Enabled = false; btnSalir.Enabled = true;
        }

        private void FormatoDgv_43BO()
        {
            if (dgvUsaurio.Columns.Count == 0 || _dic == null) return;

            void SetHeader(string col, string key)
            {
                if (dgvUsaurio.Columns.Contains(col))
                    dgvUsaurio.Columns[col].HeaderText = GetTexto(key);
            }

            SetHeader("DNI_43BO", "dgv_dni");
            SetHeader("Nombre_43BO", "dgv_nombre");
            SetHeader("Apellido_43BO", "dgv_apellido");
            SetHeader("Email_43BO", "dgv_email");
            SetHeader("RolNombre", "dgv_rol");
            SetHeader("Activo_43BO", "dgv_activo");
            SetHeader("Bloqueado_43BO", "dgv_bloqueado");

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
                    RolNombre = u.Rol != null ? u.Rol.Nombre_43BO : "Sin Rol",
                    u.Bloqueado_43BO,
                    u.Activo_43BO
                }).ToList();
                FormatoDgv_43BO();
            }
            catch (Exception ex)
            {
                MessageBox.Show(GetTexto(ex.Message), GetTexto("titulo_error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvUsaurio_CellClick_43BO(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvUsaurio.CurrentRow != null)
            {
                btnModi.Enabled = true; btnDes.Enabled = true; btnAct.Enabled = true; btnCrear.Enabled = false;

                // [Lugar 2] Re-evalúa tras forzar la activación por clicks en la grilla
                AsignadorPermisos_43BO.Aplicar(this, _mapaGestionUsuarios);

                txtDNI.Text = dgvUsaurio.CurrentRow.Cells["DNI_43BO"].Value.ToString();
                txtNom.Text = dgvUsaurio.CurrentRow.Cells["Nombre_43BO"].Value.ToString();
                txtApe.Text = dgvUsaurio.CurrentRow.Cells["Apellido_43BO"].Value.ToString();
                txtEmail.Text = dgvUsaurio.CurrentRow.Cells["Email_43BO"].Value.ToString();
                cmbRol.SelectedValue = todoslosusuarios.FirstOrDefault(u => u.DNI_43BO.ToString() == txtDNI.Text)?.Rol?.IdRol_43BO;
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
                if (Modificar_43BO) blluser.ModificarUser_43BO(int.Parse(txtDNI.Text), rol, txtEmail.Text);
                else blluser.InsertarUser_43BO(int.Parse(txtDNI.Text), txtNom.Text, txtApe.Text, rol, txtEmail.Text);

                MessageBox.Show(GetTexto("gestion_usuarios_msg_exito"), GetTexto("titulo_excelente"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                ActualizarDGV_43BO();
                Btns_43BO();

                // [Lugar 3] Re-evalúa tras guardar la operación exitosa
                AsignadorPermisos_43BO.Aplicar(this, _mapaGestionUsuarios);
            }
            catch (Exception ex)
            {
                MessageBox.Show(GetTexto(ex.Message), GetTexto("titulo_error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCanc_Click(object sender, EventArgs e)
        {
            Modificar_43BO = false; btnApli.Enabled = false; btnCrear.Enabled = true;
            txtDNI.Clear(); txtNom.Clear(); txtApe.Clear(); txtEmail.Clear();
            cmbRol.SelectedIndex = -1;
            Btns_43BO();

            // [Lugar 4] Re-evalúa al restaurar el estado original del formulario
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

                // [Lugar 5] Re-evalúa tras cambiar el estado lógico del usuario
                AsignadorPermisos_43BO.Aplicar(this, _mapaGestionUsuarios);
            }
            catch (Exception ex)
            {
                MessageBox.Show(GetTexto(ex.Message), GetTexto("titulo_error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDes_Click(object sender, EventArgs e)
        {
            try
            {
                blluser.DesbloquearUser_43BO(int.Parse(txtDNI.Text));
                ActualizarDGV_43BO();
                Btns_43BO();

                // [Lugar 6] Re-evalúa tras efectuar el desbloqueo
                AsignadorPermisos_43BO.Aplicar(this, _mapaGestionUsuarios);
            }
            catch (Exception ex)
            {
                MessageBox.Show(GetTexto(ex.Message), GetTexto("titulo_error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e) => this.Close();
        private void rbActivos_CheckedChanged(object sender, EventArgs e) => ActualizarDGV_43BO();
        private void rbTodos_CheckedChanged(object sender, EventArgs e) => ActualizarDGV_43BO();
        private void rbBloqueados_CheckedChanged(object sender, EventArgs e) => ActualizarDGV_43BO();
    }
}