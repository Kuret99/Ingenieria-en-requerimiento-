using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Servicios; 
namespace Proyecto_IngSoftware
{
    public partial class GestionUs : Form
    {
        BLL.BllUser_43BO bll = new BLL.BllUser_43BO(); // Instancia de la capa de negocio para manejar usuarios
        private bool Modificar_43BO = false; // Variable para controlar si se está modificando o creando un nuevo usuario   
        public GestionUs()
        {
            InitializeComponent();
            Btns_43BO();
            ActualizarDGV_43BO();
        }

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
                txtRol.Text = dgvUsaurio.CurrentRow.Cells["Rol_43BO"].Value.ToString();
                txtEmail.Text = dgvUsaurio.CurrentRow.Cells["Email_43BO"].Value.ToString();

                // Los mantenemos deshabilitados hasta que presione "Modificar" o "Crear"
                txtDNI.Enabled = false;
                txtNom.Enabled = false;
                txtApe.Enabled = false;
                txtRol.Enabled = false;
                txtEmail.Enabled = false;
            }
        }



        private void btnCrear_Click_1(object sender, EventArgs e)
        {
            // Activamos botones para crear un nuevo usuario
            btnApli.Enabled = true; // Solo se activa al querer guardar
            txtDNI.Enabled = true;
            Modificar_43BO = false; // Indicamos que se está creando un nuevo usuario

        }

        private void ActualizarDGV_43BO()
        {
            try
            {
                dgvUsaurio.DataSource = null;
                dgvUsaurio.DataSource = bll.ListarUsuarios_43BO();

                // no moestramos el hash porque nod eberia apercer en el dgv 
                if (dgvUsaurio.Columns.Contains("Hash_43BO")) dgvUsaurio.Columns["Hash_43BO"].Visible = false;

                dgvUsaurio.Columns["Activo_43BO"].ReadOnly = true;
                dgvUsaurio.Columns["Bloqueado_43BO"].ReadOnly = true;

                // cambio manualmenre la acbecera para que no lea exacatamente lo que esta en la BD
                dgvUsaurio.Columns["DNI_43BO"].HeaderText = "DNI";
                dgvUsaurio.Columns["Nombre_43BO"].HeaderText = "Nombre";
                dgvUsaurio.Columns["Apellido_43BO"].HeaderText = "Apellido";
                dgvUsaurio.Columns["Email_43BO"].HeaderText = "Email";
                dgvUsaurio.Columns["Rol_43BO"].HeaderText = "Rol";
                dgvUsaurio.Columns["Activo_43BO"].HeaderText = "Activo";
                dgvUsaurio.Columns["Bloqueado_43BO"].HeaderText = "Bloqueado";


                //foreach para cambiar colores de la fla a aquellos con cuentas desactivadas
                foreach (DataGridViewRow fila in dgvUsaurio.Rows)
                {
                    if (fila.Cells["Activo_43BO"].Value != null)
                    {
                        bool activo = (bool)fila.Cells["Activo_43BO"].Value;
                        if (!activo)
                        {
                            fila.DefaultCellStyle.BackColor = Color.LightCoral;
                        }
                        else
                        {
                            fila.DefaultCellStyle.BackColor = Color.White;
                        }
                    }
                }



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

                txtRol.Enabled = true;
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
                // La BLL se encarga de todo, nosotros solo le pasamos los datos
                if (!int.TryParse(txtDNI.Text, out int dni))
                {
                    MessageBox.Show("El DNI debe ser un número válido.");
                    return;
                }

                if (string.IsNullOrEmpty(txtNom.Text) || string.IsNullOrEmpty(txtApe.Text) || string.IsNullOrEmpty(txtRol.Text) || string.IsNullOrEmpty(txtEmail.Text))
                {
                    MessageBox.Show("Todos los campos deben ser completados.");
                    return;
                }
                else
                {
                    if (Modificar_43BO)
                    {
                        bll.ModificarUser_43BO(dni, txtRol.Text, txtEmail.Text);

                        Modificar_43BO = false;


                        txtDNI.Enabled = true;
                        txtNom.Enabled = true;
                        txtApe.Enabled = true;

                        MessageBox.Show("Operación realizada con éxito.");

                    }
                    else
                    {

                        bll.InsertarUser_43BO(dni, txtNom.Text, txtApe.Text, txtRol.Text, txtEmail.Text);
                        MessageBox.Show("Usuario creado con éxito.");
                    }// Para que se vea el cambio en el cuadro gris
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
            txtRol.Clear();
            txtEmail.Clear();

            Btns_43BO(); // Tu método que resetea los botones
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
                        bll.Eliminar_43BO(dni, !estadoactual);
                        ActualizarDGV_43BO();
                        MessageBox.Show($"El usuario ha sido {result.ToLower()} correctamente.");
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

        private void dgvUsaurio_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Verificamos que estemos evaluando la columna correcta por su nombre técnico
            if (dgvUsaurio.Columns[e.ColumnIndex].Name == "Activo_43BO")
            {
                // Si el valor es false (usuario inactivo)
                if (e.Value != null && (bool)e.Value == false)
                {
                    // Pintamos el fondo de TODA la fila de un color distintivo
                    dgvUsaurio.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightCoral;
                    dgvUsaurio.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
                }
                else
                {
                    // Si está activo, volvemos al color normal (blanco o el que tengas por defecto)
                    dgvUsaurio.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                }
            }
        }

    }
    
}
