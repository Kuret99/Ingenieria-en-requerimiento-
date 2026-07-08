namespace Proyecto_IngSoftware
{
    partial class IntegridadDV_43BO
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblMensaje = new System.Windows.Forms.Label();
            this.lblTablas = new System.Windows.Forms.Label();
            this.lstTablas = new System.Windows.Forms.ListBox();
            this.btnRecalcular = new System.Windows.Forms.Button();
            this.btnRestore = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.SuspendLayout();
            //
            // lblTitulo
            //
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.Firebrick;
            this.lblTitulo.Location = new System.Drawing.Point(107, 20);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(340, 20);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "¡Inconsistencia de datos detectada!";
            //
            // lblMensaje
            //
            this.lblMensaje.Location = new System.Drawing.Point(28, 52);
            this.lblMensaje.Name = "lblMensaje";
            this.lblMensaje.Size = new System.Drawing.Size(500, 34);
            this.lblMensaje.TabIndex = 1;
            this.lblMensaje.Text = "La verificación de los Dígitos Verificadores (DVH / DVV) no coincide con los val" +
    "ores persistidos en la BD. Elija una acción para continuar:";
            //
            // lblTablas
            //
            this.lblTablas.AutoSize = true;
            this.lblTablas.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTablas.Location = new System.Drawing.Point(28, 95);
            this.lblTablas.Name = "lblTablas";
            this.lblTablas.Size = new System.Drawing.Size(146, 16);
            this.lblTablas.TabIndex = 2;
            this.lblTablas.Text = "Tablas inconsistentes:";
            //
            // lstTablas
            //
            this.lstTablas.FormattingEnabled = true;
            this.lstTablas.Location = new System.Drawing.Point(31, 118);
            this.lstTablas.Name = "lstTablas";
            this.lstTablas.Size = new System.Drawing.Size(497, 108);
            this.lstTablas.TabIndex = 3;
            //
            // btnRecalcular
            //
            this.btnRecalcular.Location = new System.Drawing.Point(31, 248);
            this.btnRecalcular.Name = "btnRecalcular";
            this.btnRecalcular.Size = new System.Drawing.Size(150, 45);
            this.btnRecalcular.TabIndex = 4;
            this.btnRecalcular.Text = "RECALCULAR EL DV";
            this.btnRecalcular.UseVisualStyleBackColor = true;
            this.btnRecalcular.Click += new System.EventHandler(this.btnRecalcular_Click);
            //
            // btnRestore
            //
            this.btnRestore.Location = new System.Drawing.Point(204, 248);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(150, 45);
            this.btnRestore.TabIndex = 5;
            this.btnRestore.Text = "RESTORE BD";
            this.btnRestore.UseVisualStyleBackColor = true;
            this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);
            //
            // btnSalir
            //
            this.btnSalir.Location = new System.Drawing.Point(378, 248);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(150, 45);
            this.btnSalir.TabIndex = 6;
            this.btnSalir.Text = "SALIR";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            //
            // IntegridadDV_43BO
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(560, 318);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.btnRestore);
            this.Controls.Add(this.btnRecalcular);
            this.Controls.Add(this.lstTablas);
            this.Controls.Add(this.lblTablas);
            this.Controls.Add(this.lblMensaje);
            this.Controls.Add(this.lblTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "IntegridadDV_43BO";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Integridad de Datos - Administrador";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.IntegridadDV_43BO_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblMensaje;
        private System.Windows.Forms.Label lblTablas;
        private System.Windows.Forms.ListBox lstTablas;
        private System.Windows.Forms.Button btnRecalcular;
        private System.Windows.Forms.Button btnRestore;
        private System.Windows.Forms.Button btnSalir;
    }
}
