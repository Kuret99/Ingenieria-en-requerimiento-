using BLL;
using Servicios;
using Servicios.IidiomaObserver;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Proyecto_IngSoftware
{
    public partial class Login : Form, IdiomaObserver_43BO
    {
        private BllUser_43BO bll_43BO = new BllUser_43BO();
        private Dictionary<string, string> _diccionario;

        public Login()
        {
            InitializeComponent();

            if (!GestorIdioma_43BO.Instancia.IdiomAnterior)
            {
                GestorIdioma_43BO.Instancia.CargarIdioma_43BO("es");
            }
            GestorIdioma_43BO.Instancia.Suscribir_43BO(this);
        }

        public void ActualizarIdioma_43BO(Dictionary<string, string> dic)
        {
            // se actualiza el dic cuando cambia el idioma
            _diccionario = dic;
            AplicarTextosEstaticos();
        }

        private string ObtenerTexto(string clave, string porDefecto = null)
        {
            // unico punto de traduccion: todo pasa por el gestor
            return GestorIdioma_43BO.Instancia.ObtenerTexto_43BO(clave, porDefecto ?? clave);
        }

        private void AplicarTextosEstaticos()
        {
            if (_diccionario == null) return;

            // pongo los textos q van en las label
            label1.Text = ObtenerTexto("login._lbl_usuario", "UserName:");
            label2.Text = ObtenerTexto("login._lbl_contrasena", "Contraseña:");
            btnAceptar.Text = ObtenerTexto("login._aceptar", "Aceptar");
            this.Text = ObtenerTexto("login._titulo", "Login");
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            // boton d login q valida
            try
            {
                if (string.IsNullOrEmpty(txtUser.Text) || string.IsNullOrEmpty(txtContra.Text))
                {
                    MessageBox.Show(ObtenerTexto("login.msg_incompleto", "Por favor, complete todos los campos."),
                                    ObtenerTexto("titulo_error", "Error"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 1) autentico PRIMERO. Si la clave esta mal, VerificarCredenciales tira la
                //    excepcion de intentos/bloqueo y la muestra el catch. Asi la contraseña
                //    incorrecta descuenta intentos aunque el DV este inconsistente, en vez de
                //    tapar todo con el mensaje de "sistema no disponible".
                User_43BO usuario_43BO = bll_43BO.VerificarCredenciales_43BO(txtUser.Text, txtContra.Text);

                // 2) recien con credenciales validas verifico la integridad de datos (dvh/dvv)
                BllDV_43BO bllDv_43BO = new BllDV_43BO();
                List<string> tablasInconsistentes_43BO;

                if (!bllDv_43BO.VerificarDV_43BO(out tablasInconsistentes_43BO))
                {
                    // el admin puede reparar; cualquier otro rol solo recibe el aviso y se sale
                    if (bll_43BO.EsAdmin_43BO(usuario_43BO))
                    {
                        // le paso el admin autenticado asi el recalculo/restore quedan firmados
                        // en la bitacora (en este punto todavia no inicie sesion)
                        using (IntegridadDV_43BO frmDv_43BO = new IntegridadDV_43BO(tablasInconsistentes_43BO, usuario_43BO))
                        {
                            frmDv_43BO.ShowDialog(this);
                        }

                        // limpio la pantalla y se vuelve al login para un nuevo acceso
                        txtUser.Clear();
                        txtContra.Clear();
                        txtUser.Focus();
                        return;
                    }

                    // no es admin: aviso generico y se sale del sistema
                    MessageBox.Show(ObtenerTexto("dv_msg_inconsistencia",
                                        "El sistema no está disponible en este momento. Contacte con el administrador."),
                                    ObtenerTexto("titulo_error", "Error"),
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                    return;
                }

                // 3) DV ok -> arranco la sesion del usuario ya autenticado
                bll_43BO.CompletarLogin_43BO(usuario_43BO);

                MessageBox.Show(ObtenerTexto("login_msg_exito", "Bienvenido al sistema."),
                                ObtenerTexto("titulo_info", "Info"), MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (this.Modal)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    Menu menu_43BO = new Menu();
                    menu_43BO.Show();
                    this.Hide();
                }
            }
            catch (Exception ex)
            {

                if (_diccionario == null)
                {
                    MessageBox.Show("¡El diccionario es NULL! El idioma no se cargó.");
                    return;
                }
                string mensajeError = ex.Message;

                // Si el mensaje tiene un "|" significa que trae parámetros (como los intentos restantes)
                if (mensajeError.Contains("|"))
                {
                    string[] partes = mensajeError.Split('|');
                    // Traducimos la llave (ej: "error_intentos_restantes") y reemplazamos el {0}.
                    // Este caso ya es un mensaje claro ("Contraseña incorrecta. Intentos restantes: n"),
                    // asi que NO le anteponemos el generico "Se produjo un error:".
                    string msgIntentos = string.Format(ObtenerTexto(partes[0], partes[0]), partes[1]);
                    MessageBox.Show(msgIntentos,
                                    ObtenerTexto("titulo_error", "Error"),
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                    return;
                }

                // Traducimos el error común usando tu gestor
                mensajeError = ObtenerTexto(mensajeError, mensajeError);

                MessageBox.Show(ObtenerTexto("msg_error_general", "Se produjo un error: ") + mensajeError,
                                ObtenerTexto("titulo_error", "Error"),
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }
    }
}