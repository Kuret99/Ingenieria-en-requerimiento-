using BLL;
using Servicios.Instalacion;
using System;
using System.Windows.Forms;

namespace Proyecto_IngSoftware
{
    // Form del primer arranque: detecta instancias de SQL, deja elegir una, crea la base si
    // no existe y recuerda la eleccion. Hecho por codigo (sin Designer) para no depender de .resx.
    // Respeta capas: usa BLL y Servicios.Instalacion, NUNCA DAL directo.
    public class FRMSeleccionInstancia_43BO : Form
    {
        private ComboBox cmbInstancias;
        private Button btnDetectar;
        private Button btnContinuar;
        private Button btnCancelar;
        private Label lblTitulo;

        public string InstanciaElegida { get; private set; }

        public FRMSeleccionInstancia_43BO()
        {
            ConstruirUI();
            Detectar();
        }

        private void ConstruirUI()
        {
            this.Text = "Configuración de base de datos";
            this.Width = 470;
            this.Height = 200;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            lblTitulo = new Label
            {
                Left = 15,
                Top = 20,
                Width = 430,
                Text = "Elegí la instancia de SQL Server donde instalar/usar la base:"
            };

            cmbInstancias = new ComboBox
            {
                Left = 15,
                Top = 50,
                Width = 310,
                DropDownStyle = ComboBoxStyle.DropDown
            };

            btnDetectar = new Button { Left = 335, Top = 49, Width = 110, Text = "Detectar" };
            btnDetectar.Click += (s, e) => Detectar();

            btnContinuar = new Button { Left = 235, Top = 115, Width = 100, Text = "Continuar" };
            btnContinuar.Click += Continuar;

            btnCancelar = new Button { Left = 345, Top = 115, Width = 100, Text = "Cancelar" };
            btnCancelar.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };

            this.Controls.Add(lblTitulo);
            this.Controls.Add(cmbInstancias);
            this.Controls.Add(btnDetectar);
            this.Controls.Add(btnContinuar);
            this.Controls.Add(btnCancelar);

            this.AcceptButton = btnContinuar;
            this.CancelButton = btnCancelar;
        }

        private void Detectar()
        {
            cmbInstancias.Items.Clear();
            foreach (var i in DetectorInstancias_43BO.DetectarInstancias())
                cmbInstancias.Items.Add(i);

            if (cmbInstancias.Items.Count > 0)
                cmbInstancias.SelectedIndex = 0;
        }

        private void Continuar(object sender, EventArgs e)
        {
            string instancia = (cmbInstancias.Text ?? "").Trim();
            if (string.IsNullOrEmpty(instancia))
            {
                MessageBox.Show("Elegí o escribí una instancia de SQL.");
                return;
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;

                // si la base no existe, la creo corriendo el script completo
                if (!BLLInstalador_43BO.ExisteBaseDatos(instancia))
                {
                    BLLInstalador_43BO.InstalarBaseDatos(instancia);
                    MessageBox.Show("Base de datos instalada correctamente.");
                }

                // dejo la conexion configurada y recuerdo la instancia para las proximas veces
                BLLInstalador_43BO.ConfigurarConexion(instancia);
                ConfiguracionBD_43BO.GuardarInstancia(instancia);

                InstanciaElegida = instancia;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo conectar o instalar en esa instancia:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
    }
}
