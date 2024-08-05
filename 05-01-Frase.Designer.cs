namespace teacherCamilaCodeDev
{
    partial class frmFrase
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFrase));
            this.pnlFrase = new System.Windows.Forms.Panel();
            this.btnVoltar = new System.Windows.Forms.Button();
            this.btnDesativar = new System.Windows.Forms.Button();
            this.btnAlterar = new System.Windows.Forms.Button();
            this.btnCadastrar = new System.Windows.Forms.Button();
            this.dgvFrase = new System.Windows.Forms.DataGridView();
            this.pnlPesquisar = new System.Windows.Forms.Panel();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.txtFrase = new System.Windows.Forms.TextBox();
            this.lblStatusPesquisa = new System.Windows.Forms.Label();
            this.lblFrasePesquisa = new System.Windows.Forms.Label();
            this.lblFrase = new System.Windows.Forms.Label();
            this.pnlFrase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFrase)).BeginInit();
            this.pnlPesquisar.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlFrase
            // 
            this.pnlFrase.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.pnlFrase.Controls.Add(this.btnVoltar);
            this.pnlFrase.Controls.Add(this.btnDesativar);
            this.pnlFrase.Controls.Add(this.btnAlterar);
            this.pnlFrase.Controls.Add(this.btnCadastrar);
            this.pnlFrase.Controls.Add(this.dgvFrase);
            this.pnlFrase.Controls.Add(this.pnlPesquisar);
            this.pnlFrase.Controls.Add(this.lblFrase);
            this.pnlFrase.Location = new System.Drawing.Point(5, 5);
            this.pnlFrase.Name = "pnlFrase";
            this.pnlFrase.Size = new System.Drawing.Size(1200, 600);
            this.pnlFrase.TabIndex = 3;
            // 
            // btnVoltar
            // 
            this.btnVoltar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(0)))), ((int)(((byte)(111)))));
            this.btnVoltar.BackgroundImage = global::teacherCamilaCodeDev.Properties.Resources.btnVoltar;
            this.btnVoltar.FlatAppearance.BorderSize = 0;
            this.btnVoltar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(0)))), ((int)(((byte)(111)))));
            this.btnVoltar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(0)))), ((int)(((byte)(111)))));
            this.btnVoltar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVoltar.Location = new System.Drawing.Point(1124, 22);
            this.btnVoltar.Name = "btnVoltar";
            this.btnVoltar.Size = new System.Drawing.Size(50, 50);
            this.btnVoltar.TabIndex = 6;
            this.btnVoltar.UseVisualStyleBackColor = false;
            this.btnVoltar.Click += new System.EventHandler(this.btnVoltar_Click);
            // 
            // btnDesativar
            // 
            this.btnDesativar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(0)))), ((int)(((byte)(111)))));
            this.btnDesativar.Enabled = false;
            this.btnDesativar.FlatAppearance.BorderSize = 0;
            this.btnDesativar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDesativar.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDesativar.ForeColor = System.Drawing.Color.White;
            this.btnDesativar.Location = new System.Drawing.Point(70, 524);
            this.btnDesativar.Name = "btnDesativar";
            this.btnDesativar.Size = new System.Drawing.Size(180, 50);
            this.btnDesativar.TabIndex = 5;
            this.btnDesativar.Text = "EXCLUIR";
            this.btnDesativar.UseVisualStyleBackColor = false;
            this.btnDesativar.Click += new System.EventHandler(this.btnDesativar_Click);
            // 
            // btnAlterar
            // 
            this.btnAlterar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(0)))), ((int)(((byte)(111)))));
            this.btnAlterar.Enabled = false;
            this.btnAlterar.FlatAppearance.BorderSize = 0;
            this.btnAlterar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAlterar.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAlterar.ForeColor = System.Drawing.Color.White;
            this.btnAlterar.Location = new System.Drawing.Point(519, 524);
            this.btnAlterar.Name = "btnAlterar";
            this.btnAlterar.Size = new System.Drawing.Size(180, 50);
            this.btnAlterar.TabIndex = 4;
            this.btnAlterar.Text = "ALTERAR";
            this.btnAlterar.UseVisualStyleBackColor = false;
            this.btnAlterar.Click += new System.EventHandler(this.btnAlterar_Click);
            // 
            // btnCadastrar
            // 
            this.btnCadastrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(0)))), ((int)(((byte)(111)))));
            this.btnCadastrar.FlatAppearance.BorderSize = 0;
            this.btnCadastrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCadastrar.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCadastrar.ForeColor = System.Drawing.Color.White;
            this.btnCadastrar.Location = new System.Drawing.Point(957, 524);
            this.btnCadastrar.Name = "btnCadastrar";
            this.btnCadastrar.Size = new System.Drawing.Size(180, 50);
            this.btnCadastrar.TabIndex = 3;
            this.btnCadastrar.Text = "CADASTRAR";
            this.btnCadastrar.UseVisualStyleBackColor = false;
            this.btnCadastrar.Click += new System.EventHandler(this.btnCadastrar_Click);
            // 
            // dgvFrase
            // 
            this.dgvFrase.AllowUserToAddRows = false;
            this.dgvFrase.AllowUserToDeleteRows = false;
            this.dgvFrase.AllowUserToResizeColumns = false;
            this.dgvFrase.AllowUserToResizeRows = false;
            this.dgvFrase.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvFrase.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.dgvFrase.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFrase.EnableHeadersVisualStyles = false;
            this.dgvFrase.Location = new System.Drawing.Point(24, 189);
            this.dgvFrase.MultiSelect = false;
            this.dgvFrase.Name = "dgvFrase";
            this.dgvFrase.ReadOnly = true;
            this.dgvFrase.RowHeadersVisible = false;
            this.dgvFrase.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFrase.Size = new System.Drawing.Size(1150, 315);
            this.dgvFrase.TabIndex = 2;
            this.dgvFrase.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFrase_CellClick);
            this.dgvFrase.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvFrase_ColumnHeaderMouseClick);
            // 
            // pnlPesquisar
            // 
            this.pnlPesquisar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
            this.pnlPesquisar.Controls.Add(this.cmbStatus);
            this.pnlPesquisar.Controls.Add(this.txtFrase);
            this.pnlPesquisar.Controls.Add(this.lblStatusPesquisa);
            this.pnlPesquisar.Controls.Add(this.lblFrasePesquisa);
            this.pnlPesquisar.Location = new System.Drawing.Point(24, 117);
            this.pnlPesquisar.Name = "pnlPesquisar";
            this.pnlPesquisar.Size = new System.Drawing.Size(1150, 50);
            this.pnlPesquisar.TabIndex = 1;
            // 
            // cmbStatus
            // 
            this.cmbStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
            this.cmbStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbStatus.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Items.AddRange(new object[] {
            "TODOS",
            "ATIVO",
            "INATIVO",
            "DESATIVADO"});
            this.cmbStatus.Location = new System.Drawing.Point(906, 15);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(229, 27);
            this.cmbStatus.TabIndex = 3;
            this.cmbStatus.SelectedIndexChanged += new System.EventHandler(this.cmbStatus_SelectedIndexChanged);
            // 
            // txtFrase
            // 
            this.txtFrase.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
            this.txtFrase.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFrase.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFrase.Location = new System.Drawing.Point(93, 18);
            this.txtFrase.Name = "txtFrase";
            this.txtFrase.Size = new System.Drawing.Size(712, 19);
            this.txtFrase.TabIndex = 2;
            this.txtFrase.TextChanged += new System.EventHandler(this.txtFrase_TextChanged);
            // 
            // lblStatusPesquisa
            // 
            this.lblStatusPesquisa.AutoSize = true;
            this.lblStatusPesquisa.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatusPesquisa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.lblStatusPesquisa.Location = new System.Drawing.Point(824, 15);
            this.lblStatusPesquisa.Name = "lblStatusPesquisa";
            this.lblStatusPesquisa.Size = new System.Drawing.Size(76, 22);
            this.lblStatusPesquisa.TabIndex = 1;
            this.lblStatusPesquisa.Text = "Status:";
            // 
            // lblFrasePesquisa
            // 
            this.lblFrasePesquisa.AutoSize = true;
            this.lblFrasePesquisa.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFrasePesquisa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.lblFrasePesquisa.Location = new System.Drawing.Point(17, 15);
            this.lblFrasePesquisa.Name = "lblFrasePesquisa";
            this.lblFrasePesquisa.Size = new System.Drawing.Size(70, 22);
            this.lblFrasePesquisa.TabIndex = 0;
            this.lblFrasePesquisa.Text = "Frase:";
            // 
            // lblFrase
            // 
            this.lblFrase.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(0)))), ((int)(((byte)(111)))));
            this.lblFrase.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblFrase.Font = new System.Drawing.Font("Arial", 33.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFrase.ForeColor = System.Drawing.Color.White;
            this.lblFrase.Location = new System.Drawing.Point(0, 0);
            this.lblFrase.Name = "lblFrase";
            this.lblFrase.Size = new System.Drawing.Size(1200, 100);
            this.lblFrase.TabIndex = 0;
            this.lblFrase.Text = "F R A S E";
            this.lblFrase.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmFrase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.NavajoWhite;
            this.ClientSize = new System.Drawing.Size(1210, 610);
            this.Controls.Add(this.pnlFrase);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmFrase";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Frase";
            this.TransparencyKey = System.Drawing.Color.NavajoWhite;
            this.Load += new System.EventHandler(this.frmFrase_Load);
            this.pnlFrase.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFrase)).EndInit();
            this.pnlPesquisar.ResumeLayout(false);
            this.pnlPesquisar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlFrase;
        private System.Windows.Forms.Button btnVoltar;
        private System.Windows.Forms.Button btnDesativar;
        private System.Windows.Forms.Button btnAlterar;
        private System.Windows.Forms.Button btnCadastrar;
        private System.Windows.Forms.DataGridView dgvFrase;
        private System.Windows.Forms.Panel pnlPesquisar;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.TextBox txtFrase;
        private System.Windows.Forms.Label lblStatusPesquisa;
        private System.Windows.Forms.Label lblFrasePesquisa;
        private System.Windows.Forms.Label lblFrase;
    }
}