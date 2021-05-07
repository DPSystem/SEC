namespace entrega_cupones
{
    partial class frm_edades
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
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
      this.dgv_edades = new System.Windows.Forms.DataGridView();
      this.edad = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.F = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.M = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.btn_imprimir = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.lbl_total_edades = new System.Windows.Forms.Label();
      this.cbx_localidad = new System.Windows.Forms.ComboBox();
      this.label2 = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.dgv_edades)).BeginInit();
      this.SuspendLayout();
      // 
      // dgv_edades
      // 
      this.dgv_edades.AllowUserToAddRows = false;
      this.dgv_edades.AllowUserToDeleteRows = false;
      this.dgv_edades.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgv_edades.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.edad,
            this.F,
            this.M,
            this.cantidad});
      this.dgv_edades.Location = new System.Drawing.Point(7, 98);
      this.dgv_edades.Name = "dgv_edades";
      this.dgv_edades.ReadOnly = true;
      this.dgv_edades.RowHeadersVisible = false;
      dataGridViewCellStyle5.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.dgv_edades.RowsDefaultCellStyle = dataGridViewCellStyle5;
      this.dgv_edades.Size = new System.Drawing.Size(406, 163);
      this.dgv_edades.TabIndex = 0;
      // 
      // edad
      // 
      dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
      this.edad.DefaultCellStyle = dataGridViewCellStyle1;
      this.edad.HeaderText = "Edad";
      this.edad.Name = "edad";
      this.edad.ReadOnly = true;
      // 
      // F
      // 
      dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
      this.F.DefaultCellStyle = dataGridViewCellStyle2;
      this.F.HeaderText = "Mujer";
      this.F.Name = "F";
      this.F.ReadOnly = true;
      // 
      // M
      // 
      dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
      this.M.DefaultCellStyle = dataGridViewCellStyle3;
      this.M.HeaderText = "Varon";
      this.M.Name = "M";
      this.M.ReadOnly = true;
      // 
      // cantidad
      // 
      dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
      this.cantidad.DefaultCellStyle = dataGridViewCellStyle4;
      this.cantidad.HeaderText = "Cantidad";
      this.cantidad.Name = "cantidad";
      this.cantidad.ReadOnly = true;
      // 
      // btn_imprimir
      // 
      this.btn_imprimir.Location = new System.Drawing.Point(285, 49);
      this.btn_imprimir.Name = "btn_imprimir";
      this.btn_imprimir.Size = new System.Drawing.Size(128, 34);
      this.btn_imprimir.TabIndex = 1;
      this.btn_imprimir.Text = "Imprimir";
      this.btn_imprimir.UseVisualStyleBackColor = true;
      this.btn_imprimir.Click += new System.EventHandler(this.btn_imprimir_Click);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
      this.label1.Location = new System.Drawing.Point(294, 264);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(55, 17);
      this.label1.TabIndex = 2;
      this.label1.Text = "Total :";
      // 
      // lbl_total_edades
      // 
      this.lbl_total_edades.AutoSize = true;
      this.lbl_total_edades.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
      this.lbl_total_edades.Location = new System.Drawing.Point(361, 264);
      this.lbl_total_edades.Name = "lbl_total_edades";
      this.lbl_total_edades.Size = new System.Drawing.Size(17, 17);
      this.lbl_total_edades.TabIndex = 3;
      this.lbl_total_edades.Text = "0";
      // 
      // cbx_localidad
      // 
      this.cbx_localidad.FormattingEnabled = true;
      this.cbx_localidad.Location = new System.Drawing.Point(66, 16);
      this.cbx_localidad.Name = "cbx_localidad";
      this.cbx_localidad.Size = new System.Drawing.Size(345, 21);
      this.cbx_localidad.TabIndex = 4;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(7, 20);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(56, 13);
      this.label2.TabIndex = 5;
      this.label2.Text = "Localidad:";
      // 
      // frm_edades
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(420, 290);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.cbx_localidad);
      this.Controls.Add(this.lbl_total_edades);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.btn_imprimir);
      this.Controls.Add(this.dgv_edades);
      this.Name = "frm_edades";
      this.Text = "frm_edades";
      this.Load += new System.EventHandler(this.frm_edades_Load);
      ((System.ComponentModel.ISupportInitialize)(this.dgv_edades)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_edades;
        private System.Windows.Forms.Button btn_imprimir;
        private System.Windows.Forms.DataGridViewTextBoxColumn edad;
        private System.Windows.Forms.DataGridViewTextBoxColumn F;
        private System.Windows.Forms.DataGridViewTextBoxColumn M;
        private System.Windows.Forms.DataGridViewTextBoxColumn cantidad;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_total_edades;
        private System.Windows.Forms.ComboBox cbx_localidad;
        private System.Windows.Forms.Label label2;
    }
}