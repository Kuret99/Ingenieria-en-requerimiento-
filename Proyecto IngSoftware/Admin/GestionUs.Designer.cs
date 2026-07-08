namespace Proyecto_IngSoftware
{
    partial class GestionUs
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
            this.btnCrear = new System.Windows.Forms.Button();
            this.btnDes = new System.Windows.Forms.Button();
            this.btnModi = new System.Windows.Forms.Button();
            this.btnAct = new System.Windows.Forms.Button();
            this.btnApli = new System.Windows.Forms.Button();
            this.btnCanc = new System.Windows.Forms.Button();
            this.rbActivos = new System.Windows.Forms.RadioButton();
            this.rbTodos = new System.Windows.Forms.RadioButton();
            this.dgvUsaurio = new System.Windows.Forms.DataGridView();
            this.rbBloqueados = new System.Windows.Forms.RadioButton();
            this.btnSalir = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDNI = new System.Windows.Forms.TextBox();
            this.txtNom = new System.Windows.Forms.TextBox();
            this.txtApe = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbRol = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsaurio)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCrear
            // 
            this.btnCrear.Location = new System.Drawing.Point(920, 44);
            this.btnCrear.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCrear.Name = "btnCrear";
            this.btnCrear.Size = new System.Drawing.Size(115, 41);
            this.btnCrear.TabIndex = 0;
            this.btnCrear.Text = "Crear";
            this.btnCrear.UseVisualStyleBackColor = true;
            this.btnCrear.Click += new System.EventHandler(this.btnCrear_Click_1);
            // 
            // btnDes
            // 
            this.btnDes.Location = new System.Drawing.Point(920, 117);
            this.btnDes.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnDes.Name = "btnDes";
            this.btnDes.Size = new System.Drawing.Size(115, 41);
            this.btnDes.TabIndex = 1;
            this.btnDes.Text = "Desbloquear";
            this.btnDes.UseVisualStyleBackColor = true;
            this.btnDes.Click += new System.EventHandler(this.btnDes_Click);
            // 
            // btnModi
            // 
            this.btnModi.Location = new System.Drawing.Point(920, 193);
            this.btnModi.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnModi.Name = "btnModi";
            this.btnModi.Size = new System.Drawing.Size(115, 41);
            this.btnModi.TabIndex = 2;
            this.btnModi.Text = "Modificar";
            this.btnModi.UseVisualStyleBackColor = true;
            this.btnModi.Click += new System.EventHandler(this.btnModi_Click);
            // 
            // btnAct
            // 
            this.btnAct.Location = new System.Drawing.Point(920, 270);
            this.btnAct.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAct.Name = "btnAct";
            this.btnAct.Size = new System.Drawing.Size(115, 41);
            this.btnAct.TabIndex = 3;
            this.btnAct.Text = "Act / Desact";
            this.btnAct.UseVisualStyleBackColor = true;
            this.btnAct.Click += new System.EventHandler(this.btnAct_Click);
            // 
            // btnApli
            // 
            this.btnApli.Location = new System.Drawing.Point(629, 336);
            this.btnApli.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnApli.Name = "btnApli";
            this.btnApli.Size = new System.Drawing.Size(115, 41);
            this.btnApli.TabIndex = 4;
            this.btnApli.Text = "Aplicar";
            this.btnApli.UseVisualStyleBackColor = true;
            this.btnApli.Click += new System.EventHandler(this.btnApli_Click);
            // 
            // btnCanc
            // 
            this.btnCanc.Location = new System.Drawing.Point(776, 336);
            this.btnCanc.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCanc.Name = "btnCanc";
            this.btnCanc.Size = new System.Drawing.Size(115, 41);
            this.btnCanc.TabIndex = 5;
            this.btnCanc.Text = "Cancelar";
            this.btnCanc.UseVisualStyleBackColor = true;
            this.btnCanc.Click += new System.EventHandler(this.btnCanc_Click);
            // 
            // rbActivos
            // 
            this.rbActivos.AutoSize = true;
            this.rbActivos.Location = new System.Drawing.Point(33, 16);
            this.rbActivos.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rbActivos.Name = "rbActivos";
            this.rbActivos.Size = new System.Drawing.Size(72, 20);
            this.rbActivos.TabIndex = 6;
            this.rbActivos.TabStop = true;
            this.rbActivos.Text = "Activos";
            this.rbActivos.UseVisualStyleBackColor = true;
            this.rbActivos.CheckedChanged += new System.EventHandler(this.rbActivos_CheckedChanged);
            // 
            // rbTodos
            // 
            this.rbTodos.AutoSize = true;
            this.rbTodos.Location = new System.Drawing.Point(152, 15);
            this.rbTodos.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rbTodos.Name = "rbTodos";
            this.rbTodos.Size = new System.Drawing.Size(68, 20);
            this.rbTodos.TabIndex = 7;
            this.rbTodos.TabStop = true;
            this.rbTodos.Text = "Todos";
            this.rbTodos.UseVisualStyleBackColor = true;
            this.rbTodos.CheckedChanged += new System.EventHandler(this.rbTodos_CheckedChanged);
            // 
            // dgvUsaurio
            // 
            this.dgvUsaurio.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUsaurio.Location = new System.Drawing.Point(16, 44);
            this.dgvUsaurio.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvUsaurio.Name = "dgvUsaurio";
            this.dgvUsaurio.RowHeadersWidth = 51;
            this.dgvUsaurio.Size = new System.Drawing.Size(875, 266);
            this.dgvUsaurio.TabIndex = 8;
            this.dgvUsaurio.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUsaurio_CellClick_43BO);
            // 
            // rbBloqueados
            // 
            this.rbBloqueados.AutoSize = true;
            this.rbBloqueados.Location = new System.Drawing.Point(271, 16);
            this.rbBloqueados.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rbBloqueados.Name = "rbBloqueados";
            this.rbBloqueados.Size = new System.Drawing.Size(102, 20);
            this.rbBloqueados.TabIndex = 9;
            this.rbBloqueados.TabStop = true;
            this.rbBloqueados.Text = "Bloqueados";
            this.rbBloqueados.UseVisualStyleBackColor = true;
            this.rbBloqueados.CheckedChanged += new System.EventHandler(this.rbBloqueados_CheckedChanged);
            // 
            // btnSalir
            // 
            this.btnSalir.Location = new System.Drawing.Point(889, 428);
            this.btnSalir.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(135, 41);
            this.btnSalir.TabIndex = 10;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 326);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 16);
            this.label1.TabIndex = 11;
            this.label1.Text = "DNI";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 361);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 16);
            this.label2.TabIndex = 12;
            this.label2.Text = "Nombre";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 390);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 16);
            this.label3.TabIndex = 13;
            this.label3.Text = "Apellido";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 422);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 16);
            this.label4.TabIndex = 14;
            this.label4.Text = "Rol";
            // 
            // txtDNI
            // 
            this.txtDNI.Location = new System.Drawing.Point(92, 318);
            this.txtDNI.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtDNI.Name = "txtDNI";
            this.txtDNI.Size = new System.Drawing.Size(132, 22);
            this.txtDNI.TabIndex = 15;
            // 
            // txtNom
            // 
            this.txtNom.Location = new System.Drawing.Point(92, 350);
            this.txtNom.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtNom.Name = "txtNom";
            this.txtNom.Size = new System.Drawing.Size(132, 22);
            this.txtNom.TabIndex = 16;
            // 
            // txtApe
            // 
            this.txtApe.Location = new System.Drawing.Point(92, 382);
            this.txtApe.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtApe.Name = "txtApe";
            this.txtApe.Size = new System.Drawing.Size(132, 22);
            this.txtApe.TabIndex = 17;
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(92, 444);
            this.txtEmail.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(132, 22);
            this.txtEmail.TabIndex = 19;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 453);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 16);
            this.label5.TabIndex = 20;
            this.label5.Text = "Email";
            // 
            // cmbRol
            // 
            this.cmbRol.FormattingEnabled = true;
            this.cmbRol.Location = new System.Drawing.Point(92, 411);
            this.cmbRol.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbRol.Name = "cmbRol";
            this.cmbRol.Size = new System.Drawing.Size(132, 24);
            this.cmbRol.TabIndex = 21;
            // 
            // GestionUs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1040, 478);
            this.Controls.Add(this.cmbRol);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtApe);
            this.Controls.Add(this.txtNom);
            this.Controls.Add(this.txtDNI);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.rbBloqueados);
            this.Controls.Add(this.dgvUsaurio);
            this.Controls.Add(this.rbTodos);
            this.Controls.Add(this.rbActivos);
            this.Controls.Add(this.btnCanc);
            this.Controls.Add(this.btnApli);
            this.Controls.Add(this.btnAct);
            this.Controls.Add(this.btnModi);
            this.Controls.Add(this.btnDes);
            this.Controls.Add(this.btnCrear);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "GestionUs";
            this.Text = "GestionUs";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsaurio)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCrear;
        private System.Windows.Forms.Button btnDes;
        private System.Windows.Forms.Button btnModi;
        private System.Windows.Forms.Button btnAct;
        private System.Windows.Forms.Button btnApli;
        private System.Windows.Forms.Button btnCanc;
        private System.Windows.Forms.RadioButton rbActivos;
        private System.Windows.Forms.RadioButton rbTodos;
        private System.Windows.Forms.DataGridView dgvUsaurio;
        private System.Windows.Forms.RadioButton rbBloqueados;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDNI;
        private System.Windows.Forms.TextBox txtNom;
        private System.Windows.Forms.TextBox txtApe;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbRol;
    }
}