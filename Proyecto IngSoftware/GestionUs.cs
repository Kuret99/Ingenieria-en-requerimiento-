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
            //Ahora se habilitan los boton que necesitan un usuario antes 
            btnModi.Enabled = true;
            btnDes.Enabled = true;
            btnAct.Enabled = true;

            txtDNI.Text = dgvUsaurio.CurrentRow.Cells["DNI"].Value.ToString();
            txtNom.Text = dgvUsaurio.CurrentRow.Cells["Nombre"].Value.ToString();
            txtApe.Text = dgvUsaurio.CurrentRow.Cells["Apellido"].Value.ToString();
            txtRol.Text = dgvUsaurio.CurrentRow.Cells["Rol"].Value.ToString();
            txtEmail.Text = dgvUsaurio.CurrentRow.Cells["Email"].Value.ToString();
        }

   

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Modificar_43BO = true;
            txtDNI.Enabled = false; // El DNI no suele cambiarse si es la PK
            btnApli.Enabled = true;
        }

        private void btnApli_Click(object sender, EventArgs e)
        {
            try
            {
                // La BLL se encarga de todo, nosotros solo le pasamos los datos
               if(!int.TryParse(txtDNI.Text, out int dni))
                {
                    MessageBox.Show("El DNI debe ser un número válido.");
                    return;
                }

                if (string.IsNullOrEmpty(txtNom.Text) || string.IsNullOrEmpty(txtApe.Text) || string.IsNullOrEmpty(txtRol.Text) || string.IsNullOrEmpty(txtEmail.Text))
                {
                    MessageBox.Show("Todos los campos deben ser completados.");
                    return; 
                }
                else {
                    bll.InsertarUser_43BO(
                         dni,
                        txtNom.Text,
                        txtApe.Text,
                        txtRol.Text,
                        txtEmail.Text



                    );

                    MessageBox.Show("Operación realizada con éxito.");
                   
                }// Para que se vea el cambio en el cuadro gris
            }
         

            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            ActualizarDGV_43BO();
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
                // 1. Limpiamos la fuente de datos actual (buena práctica)
                dgvUsaurio.DataSource = null;

                // 2. Llamamos a la BLL y le asignamos la lista de objetos
                dgvUsaurio.DataSource = bll.ListarUsuarios_43BO();

                // 3. (Opcional) Ocultar columnas sensibles como la contraseña si la trajeras
                 dgvUsaurio.Columns["Contraseña"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar la grilla: " + ex.Message);
            }
        }
    } 
    
}
