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
    public partial class GestionUs : Form
    {
        BLL.BllUser_43BO blluser = new BLL.BllUser_43BO();
        BLL.BLLBitacora_43BO bllBitacora = new BLL.BLLBitacora_43BO();
        List<User_43BO> todoslosusuarios; 
        private bool Modificar_43BO = false; // <---- con eso controlo si estoy apicando una creacion o una modificiacion   
        public GestionUs()
        {
            InitializeComponent();
            Btns_43BO();
            ActualizarDGV_43BO();
            ConfigurarComboBoxRoles_43BO();

        }
        private void ConfigurarComboBoxRoles_43BO()
        {
            // Creamos los objetos directamente
            List<Rol_43BO> roles = new List<Rol_43BO>
    {
        new Rol_43BO { IdRol_43BO = 1, Nombre_43BO = "Administrador" },
        new Rol_43BO { IdRol_43BO = 2, Nombre_43BO = "Básico" }
    };

            cmbRol.DataSource = roles;
            cmbRol.DisplayMember = "Nombre_43BO";
            cmbRol.ValueMember = "IdRol_43BO";
            cmbRol.SelectedIndex = -1;
        }

        //private void ConfigurarComboBoxRoles_43BO()
        //{
        //    // Creamos la lista de objetos Rol para el combo
        //    List<Rol_43BO> roles = new List<Rol_43BO>
        //    {
        //        new Rol_43BO { IdRol_43BO = 1, Nombre_43BO = "Administrador" },
        //        new Rol_43BO { IdRol_43BO = 2, Nombre_43BO = "Básico" }
        //    };

        //    cmbRol.DataSource = roles;
        //    cmbRol.DisplayMember = "Nombre_43BO"; // Lo que ve el usuario en pantalla
        //    cmbRol.ValueMember = "IdRol_43BO";     // El ID real por detrás
        //    cmbRol.SelectedIndex = -1;
        //}
        private void Btns_43BO()
        {
            btnModi.Enabled = false;
            btnDes.Enabled = false;
            btnAct.Enabled = false;

            btnCrear.Enabled = true;
            btnApli.Enabled = false; // Solo se activa al querer guardar
            btnSalir.Enabled = true;
        }

        private void dgvUsuarios_CellClick_43BO(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvUsaurio.CurrentRow != null)
            {
                // Habilitamos botones de acción
                btnModi.Enabled = true;
                btnDes.Enabled = true;
                btnAct.Enabled = true;
                btnCrear.Enabled = false;

                // Llenamos los datos con los nombres correctos
                txtDNI.Text = dgvUsaurio.CurrentRow.Cells["DNI_43BO"].Value.ToString();
                txtNom.Text = dgvUsaurio.CurrentRow.Cells["Nombre_43BO"].Value.ToString();
                txtApe.Text = dgvUsaurio.CurrentRow.Cells["Apellido_43BO"].Value.ToString();
             
                txtEmail.Text = dgvUsaurio.CurrentRow.Cells["Email_43BO"].Value.ToString();

                int dniSeleccionado = Convert.ToInt32(txtDNI.Text);
                User_43BO usuarioSeleccionado = todoslosusuarios.FirstOrDefault(u => u.DNI_43BO == dniSeleccionado);

                if (usuarioSeleccionado != null && usuarioSeleccionado.Rol != null)
                {
                    cmbRol.SelectedValue = usuarioSeleccionado.Rol.IdRol_43BO;
                }

                // Los mantenemos deshabilitados hasta que presione "Modificar" o "Crear"
                txtDNI.Enabled = false;
                txtNom.Enabled = false;
                txtApe.Enabled = false;
                cmbRol.Enabled = false; 
                txtEmail.Enabled = false;
            }
        }



        private void btnCrear_Click_1(object sender, EventArgs e)
        {
            // Activamos botones para crear un nuevo usuario
            //btnApli.Enabled = true; // Solo se activa al querer guardar
            //txtDNI.Enabled = true;

            //Modificar_43BO = false; // Indicamos que se está creando un nuevo usuario

            btnApli.Enabled = true;
            txtDNI.Enabled = true;
            txtNom.Enabled = true;
            txtApe.Enabled = true;
            cmbRol.Enabled = true;
            txtEmail.Enabled = true;
            Modificar_43BO = false;

        }


        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormatoDgv_43BO()
        {
            if (dgvUsaurio.Columns.Count == 0) return;

            // Nombres de las cabeceras vinculadas a las propiedades del tipo anónimo anterior
            if (dgvUsaurio.Columns.Contains("DNI_43BO")) dgvUsaurio.Columns["DNI_43BO"].HeaderText = "DNI";
            if (dgvUsaurio.Columns.Contains("Nombre_43BO")) dgvUsaurio.Columns["Nombre_43BO"].HeaderText = "Nombre";
            if (dgvUsaurio.Columns.Contains("Apellido_43BO")) dgvUsaurio.Columns["Apellido_43BO"].HeaderText = "Apellido";
            if (dgvUsaurio.Columns.Contains("Email_43BO")) dgvUsaurio.Columns["Email_43BO"].HeaderText = "Email";
            if (dgvUsaurio.Columns.Contains("RolNombre")) dgvUsaurio.Columns["RolNombre"].HeaderText = "Rol";
            if (dgvUsaurio.Columns.Contains("Activo_43BO")) dgvUsaurio.Columns["Activo_43BO"].HeaderText = "Activo";
            if (dgvUsaurio.Columns.Contains("Bloqueado_43BO")) dgvUsaurio.Columns["Bloqueado_43BO"].HeaderText = "Bloqueado";

            // roerdenamos la columna del nombre del Rol a la posición que quieras 
            if (dgvUsaurio.Columns.Contains("RolNombre")) dgvUsaurio.Columns["RolNombre"].DisplayIndex = 4;

            // `pintamos los colores sando las celdas directas de la fila
            foreach (DataGridViewRow fila in dgvUsaurio.Rows)
            {
                if (fila.Cells["Activo_43BO"].Value != null && fila.Cells["Bloqueado_43BO"].Value != null)
                {
                    bool activo = (bool)fila.Cells["Activo_43BO"].Value;
                    bool bloqueado = (bool)fila.Cells["Bloqueado_43BO"].Value;

                    if (bloqueado)
                        fila.DefaultCellStyle.BackColor = Color.Khaki;
                    else if (!activo)
                        fila.DefaultCellStyle.BackColor = Color.LightCoral;
                    else
                        fila.DefaultCellStyle.BackColor = Color.White;
                }
            }
        }


        //private void Filtrar_43BO()
        //{

        //    if (todoslosusuarios == null) return; // Si la lista no está cargada, no hacemos nada

        //    List<User_43BO> listafiltrada;

        //    if (string.IsNullOrEmpty(txtFiltro.Text))
        //    {
        //        listafiltrada = todoslosusuarios; // Sin filtro, mostramos todo
        //    }
        //    else
        //    {
        //        string filtro = txtFiltro.Text.ToLower();
        //        listafiltrada = todoslosusuarios.Where(u => u.Nombre_43BO.ToLower().Contains(filtro) || u.Apellido_43BO.ToLower().Contains(filtro)).ToList();
        //    }

        //}

        private void ActualizarDGV_43BO()
        {
            try
            {
                // Cargamos la lista GLOBAL
                todoslosusuarios = blluser.ListarUsuarios_43BO();

                List<User_43BO> listaFiltrada = null;

                // Decidimos qué mostrar según el RadioButton marcado
                if (rbActivos.Checked)
                {
                    listaFiltrada = todoslosusuarios.Where(u => u.Activo_43BO == true).ToList();
                }
                else if (rbBloqueados.Checked)
                {
                    listaFiltrada = todoslosusuarios.Where(u => u.Bloqueado_43BO == true).ToList();
                }
                else
                {
                    listaFiltrada = todoslosusuarios;
                }

                if (listaFiltrada == null) listaFiltrada = new List<User_43BO>();

               
                dgvUsaurio.DataSource = null;
                dgvUsaurio.DataSource = listaFiltrada.Select(u => new
                {
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
                MessageBox.Show("Error al cargar la grilla: " + ex.Message);
            }
        }

        private void btnModi_Click(object sender, EventArgs e)
        {
            if (dgvUsaurio.CurrentRow != null)
            {
                Modificar_43BO = true;
                btnApli.Enabled = true;
                btnCrear.Enabled = false;

                //propiedaddes que no deberian cambiarse 
                txtApe.Enabled = false;
                txtDNI.Enabled = false;
                txtNom.Enabled = false;

                cmbRol.Enabled = true;
                txtEmail.Enabled = true;


            }
            else
            {
                MessageBox.Show("Seleccione algun usuario para modificar.");

            }
        }

        private void btnApli_Click(object sender, EventArgs e)
        {
            try
            {
                //preguntar si este tipo de validacion que no esta en un metodo concreto se documenta
                if (!int.TryParse(txtDNI.Text, out int dni))
                {
                    MessageBox.Show("El DNI debe ser un número válido.");
                    return;
                }

                if (txtDNI.Text.Length != 8)
                {
                    MessageBox.Show("El DNI debe tener  8 dígitos.");
                    return;
                }


                Rol_43BO rolSeleccionado = (Rol_43BO)cmbRol.SelectedItem;

                if (string.IsNullOrEmpty(txtNom.Text) || string.IsNullOrEmpty(txtApe.Text) || cmbRol.SelectedItem == null || string.IsNullOrEmpty(txtEmail.Text))
                {
                    MessageBox.Show("Todos los campos deben ser completados.");
                    return;
                }


                else 
                {
                 

                    if (Modificar_43BO)
                    {
                    
                        blluser.ModificarUser_43BO(dni, rolSeleccionado, txtEmail.Text);

                        //pongo null por ahora porqeu no se cuadno va a esatr el login asi que el sessionmanager esta de adorno
                        bllBitacora.GuardarLog_43BO(null, Modulo_43BO.Usuario, Evento_43BO.modificar, 2);

                        Modificar_43BO = false;


                        txtDNI.Enabled = true;
                        txtNom.Enabled = true;
                        txtApe.Enabled = true;

                        MessageBox.Show(
                      "Operación realizada con éxito.");

                    }
                    else
                    {
                       blluser.InsertarUser_43BO(dni, txtNom.Text, txtApe.Text,rolSeleccionado, txtEmail.Text);

                        //lo mismo
                        bllBitacora.GuardarLog_43BO(null, Modulo_43BO.Usuario, Evento_43BO.Crear, 2);
                        MessageBox.Show("Usuario creado con éxito.");
                    }
                }
            }


            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            ActualizarDGV_43BO();
        }

        private void btnCanc_Click(object sender, EventArgs e)
        {
            Modificar_43BO = false;
            btnApli.Enabled = false;
            btnCrear.Enabled = true;

            // Limpiamos y bloqueamos
            txtDNI.Clear();
            txtNom.Clear();
            txtApe.Clear();
            cmbRol.SelectedIndex = -1;
            txtEmail.Clear();

            Btns_43BO(); //resetea los botones
        }

        private void btnAct_Click(object sender, EventArgs e)
        {
            if (dgvUsaurio.CurrentRow != null)
            {
                try
                {
                    int dni = int.Parse(txtDNI.Text);
                    bool estadoactual = (bool)dgvUsaurio.CurrentRow.Cells["Activo_43BO"].Value;
                    string operacion = estadoactual ? "DESACTIVAR" : "ACTIVAR";
                    string result = estadoactual ? "desactivado" : "activado";
                    string mensaje = $"¿Está seguro que desea {operacion} al usuario con DNI {dni}?";

                    DialogResult respuesta = MessageBox.Show(mensaje, "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if(respuesta == DialogResult.Yes)
                    {
                        blluser.Eliminar_43BO(dni, !estadoactual);
                        bllBitacora.GuardarLog_43BO(null, Modulo_43BO.Usuario, Evento_43BO.Desactivar, 2);
                        ActualizarDGV_43BO();
                        MessageBox.Show($"El usuario ha sido {result.ToLower()} correctamente.");
                    }
                    else 
                    {
                        MessageBox.Show("Operación cancelada por el usuario.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al desactivar la cuenta: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Seleccione un usuario para actualizar.");
            }
        }

        private void btnDes_Click(object sender, EventArgs e)
        {
            if (dgvUsaurio.CurrentRow != null)
            {
                try
                {
                    int dni = int.Parse(txtDNI.Text);

                    bool estaBloqueado = (bool)dgvUsaurio.CurrentRow.Cells["Bloqueado_43BO"].Value;

                    if (!estaBloqueado)
                    {
                        MessageBox.Show("El usuario no se encuentra bloqueado.");
                        return;
                    }

                    DialogResult res = MessageBox.Show($"¿Desea desbloquear al usuario {dni}?",
                                       "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (res == DialogResult.Yes)
                    {
                        
                        blluser.DesbloquearUser_43BO(dni);

                        MessageBox.Show("Usuario desbloqueado con éxito.");

                        Btns_43BO();
                        ActualizarDGV_43BO();
                        }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al intentar desbloquear: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un usuario de la lista.");
            }
        }

        private void rbActivos_CheckedChanged(object sender, EventArgs e)
        {
            if (rbActivos.Checked)
            {
                ActualizarDGV_43BO();
            }
        }

        private void rbTodos_CheckedChanged(object sender, EventArgs e)
        {
            if (rbTodos.Checked)
            {
                ActualizarDGV_43BO();
            }
        }

        private void rbBloqueados_CheckedChanged(object sender, EventArgs e)
        {
            ActualizarDGV_43BO();
        }


        //metodo para probar si cambiaab el color de la fila segun el estado del usaurio pero no me convencio seguir creando metodos aprte para esa clase de funcionaldiad
        //private void dgvUsaurio_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        //{
        //    // Verificamos que estemos evaluando la columna correcta por su nombre técnico
        //    if (dgvUsaurio.Columns[e.ColumnIndex].Name == "Activo_43BO")
        //    {
        //        // Si el valor es false (usuario inactivo)
        //        if (e.Value != null && (bool)e.Value == false)
        //        {
        //            // Pintamos el fondo de TODA la fila de un color distintivo
        //            dgvUsaurio.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightCoral;
        //            dgvUsaurio.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
        //        }
        //        else
        //        {
        //            // Si está activo, volvemos al color normal (blanco o el que tengas por defecto)
        //            dgvUsaurio.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
        //        }
        //    }
        //}

   

    }
    
}
