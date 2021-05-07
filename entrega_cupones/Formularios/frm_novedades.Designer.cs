namespace entrega_cupones
{
    partial class frm_novedades
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_novedades));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.bunifuElipse1 = new Bunifu.Framework.UI.BunifuElipse(this.components);
            this.bunifuCustomLabel4 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.txt_comentario = new System.Windows.Forms.TextBox();
            this.btn_cancelar = new Bunifu.Framework.UI.BunifuFlatButton();
            this.bunifuSeparator1 = new Bunifu.Framework.UI.BunifuSeparator();
            this.lbl_cuit = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.lbl_razon_social = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.bunifuCustomLabel3 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.bunifuCustomLabel2 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.btn_cargar_acta = new Bunifu.Framework.UI.BunifuFlatButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.bunifuCustomLabel1 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.btn_cerrar = new Bunifu.Framework.UI.BunifuImageButton();
            this.bunifuCustomLabel5 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.bunifuCustomLabel6 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.dgv_novedades = new System.Windows.Forms.DataGridView();
            this.fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.novedad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bunifuCustomLabel7 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btn_cerrar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_novedades)).BeginInit();
            this.SuspendLayout();
            // 
            // bunifuElipse1
            // 
            this.bunifuElipse1.ElipseRadius = 5;
            this.bunifuElipse1.TargetControl = this;
            // 
            // bunifuCustomLabel4
            // 
            this.bunifuCustomLabel4.AutoSize = true;
            this.bunifuCustomLabel4.Font = new System.Drawing.Font("Century Gothic", 10F);
            this.bunifuCustomLabel4.ForeColor = System.Drawing.Color.Black;
            this.bunifuCustomLabel4.Location = new System.Drawing.Point(12, 135);
            this.bunifuCustomLabel4.Name = "bunifuCustomLabel4";
            this.bunifuCustomLabel4.Size = new System.Drawing.Size(147, 19);
            this.bunifuCustomLabel4.TabIndex = 336;
            this.bunifuCustomLabel4.Text = "Ingrese Comentario:";
            // 
            // txt_comentario
            // 
            this.txt_comentario.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_comentario.Location = new System.Drawing.Point(13, 157);
            this.txt_comentario.MaxLength = 500;
            this.txt_comentario.Multiline = true;
            this.txt_comentario.Name = "txt_comentario";
            this.txt_comentario.Size = new System.Drawing.Size(501, 82);
            this.txt_comentario.TabIndex = 328;
            // 
            // btn_cancelar
            // 
            this.btn_cancelar.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(174)))), ((int)(((byte)(70)))));
            this.btn_cancelar.BackColor = System.Drawing.Color.Tomato;
            this.btn_cancelar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_cancelar.BorderRadius = 5;
            this.btn_cancelar.ButtonText = "Cancelar";
            this.btn_cancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_cancelar.DisabledColor = System.Drawing.Color.Gray;
            this.btn_cancelar.Iconcolor = System.Drawing.Color.Transparent;
            this.btn_cancelar.Iconimage = global::entrega_cupones.Properties.Resources.cancel_square_button__2_;
            this.btn_cancelar.Iconimage_right = null;
            this.btn_cancelar.Iconimage_right_Selected = null;
            this.btn_cancelar.Iconimage_Selected = null;
            this.btn_cancelar.IconMarginLeft = 0;
            this.btn_cancelar.IconMarginRight = 0;
            this.btn_cancelar.IconRightVisible = false;
            this.btn_cancelar.IconRightZoom = 0D;
            this.btn_cancelar.IconVisible = true;
            this.btn_cancelar.IconZoom = 50D;
            this.btn_cancelar.IsTab = false;
            this.btn_cancelar.Location = new System.Drawing.Point(405, 245);
            this.btn_cancelar.Name = "btn_cancelar";
            this.btn_cancelar.Normalcolor = System.Drawing.Color.Tomato;
            this.btn_cancelar.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(174)))), ((int)(((byte)(70)))));
            this.btn_cancelar.OnHoverTextColor = System.Drawing.Color.White;
            this.btn_cancelar.selected = false;
            this.btn_cancelar.Size = new System.Drawing.Size(109, 31);
            this.btn_cancelar.TabIndex = 330;
            this.btn_cancelar.Text = "Cancelar";
            this.btn_cancelar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btn_cancelar.Textcolor = System.Drawing.Color.White;
            this.btn_cancelar.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_cancelar.Click += new System.EventHandler(this.btn_cancelar_Click);
            // 
            // bunifuSeparator1
            // 
            this.bunifuSeparator1.BackColor = System.Drawing.Color.Transparent;
            this.bunifuSeparator1.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(105)))), ((int)(((byte)(105)))));
            this.bunifuSeparator1.LineThickness = 4;
            this.bunifuSeparator1.Location = new System.Drawing.Point(0, 120);
            this.bunifuSeparator1.Name = "bunifuSeparator1";
            this.bunifuSeparator1.Size = new System.Drawing.Size(521, 10);
            this.bunifuSeparator1.TabIndex = 335;
            this.bunifuSeparator1.Transparency = 255;
            this.bunifuSeparator1.Vertical = false;
            // 
            // lbl_cuit
            // 
            this.lbl_cuit.AutoSize = true;
            this.lbl_cuit.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_cuit.ForeColor = System.Drawing.Color.Black;
            this.lbl_cuit.Location = new System.Drawing.Point(78, 75);
            this.lbl_cuit.Name = "lbl_cuit";
            this.lbl_cuit.Size = new System.Drawing.Size(36, 19);
            this.lbl_cuit.TabIndex = 334;
            this.lbl_cuit.Text = "xxx";
            // 
            // lbl_razon_social
            // 
            this.lbl_razon_social.AutoSize = true;
            this.lbl_razon_social.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_razon_social.ForeColor = System.Drawing.Color.Black;
            this.lbl_razon_social.Location = new System.Drawing.Point(118, 99);
            this.lbl_razon_social.Name = "lbl_razon_social";
            this.lbl_razon_social.Size = new System.Drawing.Size(45, 19);
            this.lbl_razon_social.TabIndex = 333;
            this.lbl_razon_social.Text = "xxxx";
            // 
            // bunifuCustomLabel3
            // 
            this.bunifuCustomLabel3.AutoSize = true;
            this.bunifuCustomLabel3.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuCustomLabel3.ForeColor = System.Drawing.Color.Black;
            this.bunifuCustomLabel3.Location = new System.Drawing.Point(7, 77);
            this.bunifuCustomLabel3.Name = "bunifuCustomLabel3";
            this.bunifuCustomLabel3.Size = new System.Drawing.Size(62, 19);
            this.bunifuCustomLabel3.TabIndex = 332;
            this.bunifuCustomLabel3.Text = "C.U.I.T.:";
            // 
            // bunifuCustomLabel2
            // 
            this.bunifuCustomLabel2.AutoSize = true;
            this.bunifuCustomLabel2.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuCustomLabel2.ForeColor = System.Drawing.Color.Black;
            this.bunifuCustomLabel2.Location = new System.Drawing.Point(7, 100);
            this.bunifuCustomLabel2.Name = "bunifuCustomLabel2";
            this.bunifuCustomLabel2.Size = new System.Drawing.Size(111, 19);
            this.bunifuCustomLabel2.TabIndex = 331;
            this.bunifuCustomLabel2.Text = "Razon Social:";
            // 
            // btn_cargar_acta
            // 
            this.btn_cargar_acta.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(174)))), ((int)(((byte)(70)))));
            this.btn_cargar_acta.BackColor = System.Drawing.Color.Tomato;
            this.btn_cargar_acta.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_cargar_acta.BorderRadius = 5;
            this.btn_cargar_acta.ButtonText = "Grabar";
            this.btn_cargar_acta.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_cargar_acta.DisabledColor = System.Drawing.Color.Gray;
            this.btn_cargar_acta.Iconcolor = System.Drawing.Color.Transparent;
            this.btn_cargar_acta.Iconimage = ((System.Drawing.Image)(resources.GetObject("btn_cargar_acta.Iconimage")));
            this.btn_cargar_acta.Iconimage_right = null;
            this.btn_cargar_acta.Iconimage_right_Selected = null;
            this.btn_cargar_acta.Iconimage_Selected = null;
            this.btn_cargar_acta.IconMarginLeft = 0;
            this.btn_cargar_acta.IconMarginRight = 0;
            this.btn_cargar_acta.IconRightVisible = false;
            this.btn_cargar_acta.IconRightZoom = 0D;
            this.btn_cargar_acta.IconVisible = true;
            this.btn_cargar_acta.IconZoom = 50D;
            this.btn_cargar_acta.IsTab = false;
            this.btn_cargar_acta.Location = new System.Drawing.Point(286, 245);
            this.btn_cargar_acta.Name = "btn_cargar_acta";
            this.btn_cargar_acta.Normalcolor = System.Drawing.Color.Tomato;
            this.btn_cargar_acta.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(174)))), ((int)(((byte)(70)))));
            this.btn_cargar_acta.OnHoverTextColor = System.Drawing.Color.White;
            this.btn_cargar_acta.selected = false;
            this.btn_cargar_acta.Size = new System.Drawing.Size(109, 31);
            this.btn_cargar_acta.TabIndex = 329;
            this.btn_cargar_acta.Text = "Grabar";
            this.btn_cargar_acta.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btn_cargar_acta.Textcolor = System.Drawing.Color.White;
            this.btn_cargar_acta.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_cargar_acta.Click += new System.EventHandler(this.btn_cargar_acta_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Tomato;
            this.panel2.Controls.Add(this.bunifuCustomLabel1);
            this.panel2.Controls.Add(this.btn_cerrar);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(524, 51);
            this.panel2.TabIndex = 327;
            // 
            // bunifuCustomLabel1
            // 
            this.bunifuCustomLabel1.AutoSize = true;
            this.bunifuCustomLabel1.Font = new System.Drawing.Font("Century Gothic", 18.75F, System.Drawing.FontStyle.Bold);
            this.bunifuCustomLabel1.ForeColor = System.Drawing.Color.White;
            this.bunifuCustomLabel1.Location = new System.Drawing.Point(9, 11);
            this.bunifuCustomLabel1.Name = "bunifuCustomLabel1";
            this.bunifuCustomLabel1.Size = new System.Drawing.Size(280, 29);
            this.bunifuCustomLabel1.TabIndex = 149;
            this.bunifuCustomLabel1.Text = "Carga de Novedades";
            // 
            // btn_cerrar
            // 
            this.btn_cerrar.BackColor = System.Drawing.Color.Transparent;
            this.btn_cerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_cerrar.Image = global::entrega_cupones.Properties.Resources.cross_close_or_delete_circular_interface_button_symbol;
            this.btn_cerrar.ImageActive = null;
            this.btn_cerrar.Location = new System.Drawing.Point(486, 12);
            this.btn_cerrar.Name = "btn_cerrar";
            this.btn_cerrar.Size = new System.Drawing.Size(31, 30);
            this.btn_cerrar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btn_cerrar.TabIndex = 148;
            this.btn_cerrar.TabStop = false;
            this.btn_cerrar.Zoom = 10;
            this.btn_cerrar.Click += new System.EventHandler(this.btn_cerrar_Click);
            // 
            // bunifuCustomLabel5
            // 
            this.bunifuCustomLabel5.AutoSize = true;
            this.bunifuCustomLabel5.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuCustomLabel5.ForeColor = System.Drawing.Color.Black;
            this.bunifuCustomLabel5.Location = new System.Drawing.Point(84, 54);
            this.bunifuCustomLabel5.Name = "bunifuCustomLabel5";
            this.bunifuCustomLabel5.Size = new System.Drawing.Size(111, 19);
            this.bunifuCustomLabel5.TabIndex = 338;
            this.bunifuCustomLabel5.Text = "No Asiganda";
            // 
            // bunifuCustomLabel6
            // 
            this.bunifuCustomLabel6.AutoSize = true;
            this.bunifuCustomLabel6.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuCustomLabel6.ForeColor = System.Drawing.Color.Black;
            this.bunifuCustomLabel6.Location = new System.Drawing.Point(7, 54);
            this.bunifuCustomLabel6.Name = "bunifuCustomLabel6";
            this.bunifuCustomLabel6.Size = new System.Drawing.Size(78, 19);
            this.bunifuCustomLabel6.TabIndex = 337;
            this.bunifuCustomLabel6.Text = "ACTA Nº:";
            // 
            // dgv_novedades
            // 
            this.dgv_novedades.AllowUserToAddRows = false;
            this.dgv_novedades.AllowUserToDeleteRows = false;
            this.dgv_novedades.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dgv_novedades.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_novedades.BackgroundColor = System.Drawing.Color.White;
            this.dgv_novedades.CausesValidation = false;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_novedades.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_novedades.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_novedades.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.fecha,
            this.novedad});
            this.dgv_novedades.GridColor = System.Drawing.Color.Silver;
            this.dgv_novedades.Location = new System.Drawing.Point(11, 282);
            this.dgv_novedades.Name = "dgv_novedades";
            this.dgv_novedades.ReadOnly = true;
            this.dgv_novedades.RowHeadersVisible = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_novedades.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgv_novedades.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_novedades.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_novedades.Size = new System.Drawing.Size(503, 165);
            this.dgv_novedades.TabIndex = 339;
            // 
            // fecha
            // 
            this.fecha.DataPropertyName = "fecha";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.BottomCenter;
            dataGridViewCellStyle3.Format = "g";
            dataGridViewCellStyle3.NullValue = null;
            this.fecha.DefaultCellStyle = dataGridViewCellStyle3;
            this.fecha.HeaderText = "Fecha";
            this.fecha.Name = "fecha";
            this.fecha.ReadOnly = true;
            // 
            // novedad
            // 
            this.novedad.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.novedad.DataPropertyName = "novedad";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.BottomLeft;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.novedad.DefaultCellStyle = dataGridViewCellStyle4;
            this.novedad.HeaderText = "Novedad";
            this.novedad.Name = "novedad";
            this.novedad.ReadOnly = true;
            this.novedad.Width = 380;
            // 
            // bunifuCustomLabel7
            // 
            this.bunifuCustomLabel7.AutoSize = true;
            this.bunifuCustomLabel7.Font = new System.Drawing.Font("Century Gothic", 10F);
            this.bunifuCustomLabel7.ForeColor = System.Drawing.Color.Black;
            this.bunifuCustomLabel7.Location = new System.Drawing.Point(12, 260);
            this.bunifuCustomLabel7.Name = "bunifuCustomLabel7";
            this.bunifuCustomLabel7.Size = new System.Drawing.Size(61, 19);
            this.bunifuCustomLabel7.TabIndex = 340;
            this.bunifuCustomLabel7.Text = "Historial";
            // 
            // frm_novedades
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 455);
            this.Controls.Add(this.bunifuCustomLabel7);
            this.Controls.Add(this.dgv_novedades);
            this.Controls.Add(this.bunifuCustomLabel5);
            this.Controls.Add(this.bunifuCustomLabel6);
            this.Controls.Add(this.bunifuCustomLabel4);
            this.Controls.Add(this.txt_comentario);
            this.Controls.Add(this.btn_cancelar);
            this.Controls.Add(this.bunifuSeparator1);
            this.Controls.Add(this.lbl_cuit);
            this.Controls.Add(this.lbl_razon_social);
            this.Controls.Add(this.bunifuCustomLabel3);
            this.Controls.Add(this.bunifuCustomLabel2);
            this.Controls.Add(this.btn_cargar_acta);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frm_novedades";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frm_novedades";
            this.Load += new System.EventHandler(this.frm_novedades_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btn_cerrar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_novedades)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Bunifu.Framework.UI.BunifuElipse bunifuElipse1;
        public Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel5;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel6;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel4;
        public System.Windows.Forms.TextBox txt_comentario;
        private Bunifu.Framework.UI.BunifuFlatButton btn_cancelar;
        private Bunifu.Framework.UI.BunifuSeparator bunifuSeparator1;
        public Bunifu.Framework.UI.BunifuCustomLabel lbl_cuit;
        public Bunifu.Framework.UI.BunifuCustomLabel lbl_razon_social;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel3;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel2;
        private Bunifu.Framework.UI.BunifuFlatButton btn_cargar_acta;
        private System.Windows.Forms.Panel panel2;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel1;
        private Bunifu.Framework.UI.BunifuImageButton btn_cerrar;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel7;
        private System.Windows.Forms.DataGridView dgv_novedades;
        private System.Windows.Forms.DataGridViewTextBoxColumn fecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn novedad;
    }
}