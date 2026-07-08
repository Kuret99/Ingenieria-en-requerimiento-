namespace Proyecto_IngSoftware
{
    partial class BackupBD_43BO
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
            this.lblCarpeta = new System.Windows.Forms.Label();
            this.btnCambiarCarpeta = new System.Windows.Forms.Button();
            this.lblLista = new System.Windows.Forms.Label();
            this.lstBackups = new System.Windows.Forms.ListBox();
            this.btnGenerar = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            //
            // lblTitulo
            //
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(200, 18);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(178, 20);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Backup de la BD";
            //
            // lblCarpeta
            //
            this.lblCarpeta.AutoEllipsis = true;
            this.lblCarpeta.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCarpeta.Location = new System.Drawing.Point(28, 52);
            this.lblCarpeta.Name = "lblCarpeta";
            this.lblCarpeta.Size = new System.Drawing.Size(380, 34);
            this.lblCarpeta.TabIndex = 1;
            this.lblCarpeta.Text = "Carpeta actual:";
            //
            // btnCambiarCarpeta
            //
            this.btnCambiarCarpeta.Location = new System.Drawing.Point(418, 50);
            this.btnCambiarCarpeta.Name = "btnCambiarCarpeta";
            this.btnCambiarCarpeta.Size = new System.Drawing.Size(130, 30);
            this.btnCambiarCarpeta.TabIndex = 2;
            this.btnCambiarCarpeta.Text = "Cambiar carpeta...";
            this.btnCambiarCarpeta.UseVisualStyleBackColor = true;
            this.btnCambiarCarpeta.Click += new System.EventHandler(this.btnCambiarCarpeta_Click);
            //
            // lblLista
            //
            this.lblLista.AutoSize = true;
            this.lblLista.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLista.Location = new System.Drawing.Point(28, 95);
            this.lblLista.Name = "lblLista";
            this.lblLista.Size = new System.Drawing.Size(142, 16);
            this.lblLista.TabIndex = 3;
            this.lblLista.Text = "Backups disponibles:";
            //
            // lstBackups
            //
            this.lstBackups.FormattingEnabled = true;
            this.lstBackups.HorizontalScrollbar = true;
            this.lstBackups.Location = new System.Drawing.Point(31, 118);
            this.lstBackups.Name = "lstBackups";
            this.lstBackups.Size = new System.Drawing.Size(517, 134);
            this.lstBackups.TabIndex = 4;
            //
            // btnGenerar
            //
            this.btnGenerar.Location = new System.Drawing.Point(120, 274);
            this.btnGenerar.Name = "btnGenerar";
            this.btnGenerar.Size = new System.Drawing.Size(140, 40);
            this.btnGenerar.TabIndex = 5;
            this.btnGenerar.Text = "Generar Backup";
            this.btnGenerar.UseVisualStyleBackColor = true;
            this.btnGenerar.Click += new System.EventHandler(this.btnGenerar_Click);
            //
            // btnCerrar
            //
            this.btnCerrar.Location = new System.Drawing.Point(320, 274);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(140, 40);
            this.btnCerrar.TabIndex = 6;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            //
            // BackupBD_43BO
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(580, 336);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.btnGenerar);
            this.Controls.Add(this.lstBackups);
            this.Controls.Add(this.lblLista);
            this.Controls.Add(this.btnCambiarCarpeta);
            this.Controls.Add(this.lblCarpeta);
            this.Controls.Add(this.lblTitulo);
            this.Name = "BackupBD_43BO";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Backup BD";
            this.Load += new System.EventHandler(this.BackupBD_43BO_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.BackupBD_43BO_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblCarpeta;
        private System.Windows.Forms.Button btnCambiarCarpeta;
        private System.Windows.Forms.Label lblLista;
        private System.Windows.Forms.ListBox lstBackups;
        private System.Windows.Forms.Button btnGenerar;
        private System.Windows.Forms.Button btnCerrar;
    }
}
