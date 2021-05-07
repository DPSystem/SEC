namespace entrega_cupones.Formularios
{
  partial class frm_CobranzasAsignadas
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
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
      this.dgv_CbrAsignadas = new System.Windows.Forms.DataGridView();
      this.dataGridViewTextBoxColumn86 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.numero = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn89 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn90 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn88 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.panel6 = new System.Windows.Forms.Panel();
      this.bunifuCustomLabel4 = new Bunifu.Framework.UI.BunifuCustomLabel();
      this.btn_VerAsignadas = new Bunifu.Framework.UI.BunifuFlatButton();
      this.label1 = new System.Windows.Forms.Label();
      this.lbl_Cobrador = new System.Windows.Forms.Label();
      this.btn_Actualizar = new Bunifu.Framework.UI.BunifuFlatButton();
      this.dgv_Novedades = new System.Windows.Forms.DataGridView();
      this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Empresa = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.NroDeAsignacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Comentario = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Emisor = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.panel1 = new System.Windows.Forms.Panel();
      this.bunifuCustomLabel1 = new Bunifu.Framework.UI.BunifuCustomLabel();
      ((System.ComponentModel.ISupportInitialize)(this.dgv_CbrAsignadas)).BeginInit();
      this.panel6.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dgv_Novedades)).BeginInit();
      this.panel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // dgv_CbrAsignadas
      // 
      this.dgv_CbrAsignadas.AllowUserToAddRows = false;
      this.dgv_CbrAsignadas.AllowUserToDeleteRows = false;
      this.dgv_CbrAsignadas.AllowUserToResizeRows = false;
      dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
      this.dgv_CbrAsignadas.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
      this.dgv_CbrAsignadas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgv_CbrAsignadas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn86,
            this.numero,
            this.dataGridViewTextBoxColumn89,
            this.dataGridViewTextBoxColumn90,
            this.dataGridViewTextBoxColumn88});
      dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
      dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
      dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
      this.dgv_CbrAsignadas.DefaultCellStyle = dataGridViewCellStyle7;
      this.dgv_CbrAsignadas.Location = new System.Drawing.Point(3, 74);
      this.dgv_CbrAsignadas.Name = "dgv_CbrAsignadas";
      this.dgv_CbrAsignadas.ReadOnly = true;
      this.dgv_CbrAsignadas.RowHeadersVisible = false;
      this.dgv_CbrAsignadas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.dgv_CbrAsignadas.Size = new System.Drawing.Size(332, 204);
      this.dgv_CbrAsignadas.TabIndex = 566;
      // 
      // dataGridViewTextBoxColumn86
      // 
      this.dataGridViewTextBoxColumn86.DataPropertyName = "fecha";
      dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle2.Format = "dd/MM/yyyy";
      this.dataGridViewTextBoxColumn86.DefaultCellStyle = dataGridViewCellStyle2;
      this.dataGridViewTextBoxColumn86.HeaderText = "Fecha";
      this.dataGridViewTextBoxColumn86.Name = "dataGridViewTextBoxColumn86";
      this.dataGridViewTextBoxColumn86.ReadOnly = true;
      this.dataGridViewTextBoxColumn86.Width = 70;
      // 
      // numero
      // 
      this.numero.DataPropertyName = "numero";
      dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
      this.numero.DefaultCellStyle = dataGridViewCellStyle3;
      this.numero.HeaderText = "Nº";
      this.numero.Name = "numero";
      this.numero.ReadOnly = true;
      this.numero.Width = 50;
      // 
      // dataGridViewTextBoxColumn89
      // 
      this.dataGridViewTextBoxColumn89.DataPropertyName = "cobradas";
      dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
      this.dataGridViewTextBoxColumn89.DefaultCellStyle = dataGridViewCellStyle4;
      this.dataGridViewTextBoxColumn89.HeaderText = "Cobradas";
      this.dataGridViewTextBoxColumn89.Name = "dataGridViewTextBoxColumn89";
      this.dataGridViewTextBoxColumn89.ReadOnly = true;
      this.dataGridViewTextBoxColumn89.Width = 55;
      // 
      // dataGridViewTextBoxColumn90
      // 
      this.dataGridViewTextBoxColumn90.DataPropertyName = "noCobradas";
      dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
      dataGridViewCellStyle5.Format = "N0";
      dataGridViewCellStyle5.NullValue = null;
      this.dataGridViewTextBoxColumn90.DefaultCellStyle = dataGridViewCellStyle5;
      this.dataGridViewTextBoxColumn90.HeaderText = "No Cobradas";
      this.dataGridViewTextBoxColumn90.Name = "dataGridViewTextBoxColumn90";
      this.dataGridViewTextBoxColumn90.ReadOnly = true;
      this.dataGridViewTextBoxColumn90.Width = 75;
      // 
      // dataGridViewTextBoxColumn88
      // 
      this.dataGridViewTextBoxColumn88.DataPropertyName = "total";
      dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
      this.dataGridViewTextBoxColumn88.DefaultCellStyle = dataGridViewCellStyle6;
      this.dataGridViewTextBoxColumn88.HeaderText = "Total";
      this.dataGridViewTextBoxColumn88.Name = "dataGridViewTextBoxColumn88";
      this.dataGridViewTextBoxColumn88.ReadOnly = true;
      this.dataGridViewTextBoxColumn88.Width = 55;
      // 
      // panel6
      // 
      this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(151)))), ((int)(((byte)(119)))));
      this.panel6.Controls.Add(this.bunifuCustomLabel4);
      this.panel6.Location = new System.Drawing.Point(3, 42);
      this.panel6.Name = "panel6";
      this.panel6.Size = new System.Drawing.Size(332, 26);
      this.panel6.TabIndex = 565;
      // 
      // bunifuCustomLabel4
      // 
      this.bunifuCustomLabel4.AutoSize = true;
      this.bunifuCustomLabel4.BackColor = System.Drawing.Color.Transparent;
      this.bunifuCustomLabel4.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.bunifuCustomLabel4.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.bunifuCustomLabel4.Location = new System.Drawing.Point(44, 2);
      this.bunifuCustomLabel4.Name = "bunifuCustomLabel4";
      this.bunifuCustomLabel4.Size = new System.Drawing.Size(247, 19);
      this.bunifuCustomLabel4.TabIndex = 197;
      this.bunifuCustomLabel4.Text = "Ver Estado de Actas Asignadas";
      // 
      // btn_VerAsignadas
      // 
      this.btn_VerAsignadas.Active = false;
      this.btn_VerAsignadas.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(174)))), ((int)(((byte)(70)))));
      this.btn_VerAsignadas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(151)))), ((int)(((byte)(119)))));
      this.btn_VerAsignadas.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
      this.btn_VerAsignadas.BorderRadius = 5;
      this.btn_VerAsignadas.ButtonText = "Mostrar";
      this.btn_VerAsignadas.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.btn_VerAsignadas.DisabledColor = System.Drawing.Color.Gray;
      this.btn_VerAsignadas.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_VerAsignadas.Iconcolor = System.Drawing.Color.Transparent;
      this.btn_VerAsignadas.Iconimage = global::entrega_cupones.Properties.Resources.magnifier13;
      this.btn_VerAsignadas.Iconimage_right = null;
      this.btn_VerAsignadas.Iconimage_right_Selected = null;
      this.btn_VerAsignadas.Iconimage_Selected = null;
      this.btn_VerAsignadas.IconMarginLeft = 0;
      this.btn_VerAsignadas.IconMarginRight = 0;
      this.btn_VerAsignadas.IconRightVisible = false;
      this.btn_VerAsignadas.IconRightZoom = 0D;
      this.btn_VerAsignadas.IconVisible = true;
      this.btn_VerAsignadas.IconZoom = 40D;
      this.btn_VerAsignadas.IsTab = false;
      this.btn_VerAsignadas.Location = new System.Drawing.Point(189, 285);
      this.btn_VerAsignadas.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.btn_VerAsignadas.Name = "btn_VerAsignadas";
      this.btn_VerAsignadas.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(151)))), ((int)(((byte)(119)))));
      this.btn_VerAsignadas.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(74)))), ((int)(((byte)(0)))));
      this.btn_VerAsignadas.OnHoverTextColor = System.Drawing.Color.White;
      this.btn_VerAsignadas.selected = false;
      this.btn_VerAsignadas.Size = new System.Drawing.Size(146, 33);
      this.btn_VerAsignadas.TabIndex = 567;
      this.btn_VerAsignadas.Text = "Mostrar";
      this.btn_VerAsignadas.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.btn_VerAsignadas.Textcolor = System.Drawing.Color.Black;
      this.btn_VerAsignadas.TextFont = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_VerAsignadas.Click += new System.EventHandler(this.btn_VerAsignadas_Click);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Century Gothic", 10F);
      this.label1.ForeColor = System.Drawing.Color.Gainsboro;
      this.label1.Location = new System.Drawing.Point(12, 9);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(80, 19);
      this.label1.TabIndex = 568;
      this.label1.Text = "Cobrador:";
      // 
      // lbl_Cobrador
      // 
      this.lbl_Cobrador.AutoSize = true;
      this.lbl_Cobrador.Font = new System.Drawing.Font("Century Gothic", 10F);
      this.lbl_Cobrador.ForeColor = System.Drawing.Color.Gainsboro;
      this.lbl_Cobrador.Location = new System.Drawing.Point(97, 9);
      this.lbl_Cobrador.Name = "lbl_Cobrador";
      this.lbl_Cobrador.Size = new System.Drawing.Size(44, 19);
      this.lbl_Cobrador.TabIndex = 569;
      this.lbl_Cobrador.Text = "-------";
      // 
      // btn_Actualizar
      // 
      this.btn_Actualizar.Active = false;
      this.btn_Actualizar.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(174)))), ((int)(((byte)(70)))));
      this.btn_Actualizar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(151)))), ((int)(((byte)(119)))));
      this.btn_Actualizar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
      this.btn_Actualizar.BorderRadius = 5;
      this.btn_Actualizar.ButtonText = "Actualizar";
      this.btn_Actualizar.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btn_Actualizar.DisabledColor = System.Drawing.Color.Gray;
      this.btn_Actualizar.Iconcolor = System.Drawing.Color.Transparent;
      this.btn_Actualizar.Iconimage = global::entrega_cupones.Properties.Resources.refresh_arrow;
      this.btn_Actualizar.Iconimage_right = null;
      this.btn_Actualizar.Iconimage_right_Selected = null;
      this.btn_Actualizar.Iconimage_Selected = null;
      this.btn_Actualizar.IconMarginLeft = 0;
      this.btn_Actualizar.IconMarginRight = 0;
      this.btn_Actualizar.IconRightVisible = false;
      this.btn_Actualizar.IconRightZoom = 0D;
      this.btn_Actualizar.IconVisible = true;
      this.btn_Actualizar.IconZoom = 50D;
      this.btn_Actualizar.IsTab = false;
      this.btn_Actualizar.Location = new System.Drawing.Point(3, 285);
      this.btn_Actualizar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.btn_Actualizar.Name = "btn_Actualizar";
      this.btn_Actualizar.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(151)))), ((int)(((byte)(119)))));
      this.btn_Actualizar.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(74)))), ((int)(((byte)(0)))));
      this.btn_Actualizar.OnHoverTextColor = System.Drawing.Color.White;
      this.btn_Actualizar.selected = false;
      this.btn_Actualizar.Size = new System.Drawing.Size(146, 33);
      this.btn_Actualizar.TabIndex = 570;
      this.btn_Actualizar.Text = "Actualizar";
      this.btn_Actualizar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.btn_Actualizar.Textcolor = System.Drawing.Color.Black;
      this.btn_Actualizar.TextFont = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      // 
      // dgv_Novedades
      // 
      this.dgv_Novedades.AllowUserToAddRows = false;
      this.dgv_Novedades.AllowUserToDeleteRows = false;
      this.dgv_Novedades.AllowUserToOrderColumns = true;
      dataGridViewCellStyle8.BackColor = System.Drawing.Color.White;
      this.dgv_Novedades.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle8;
      this.dgv_Novedades.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
      this.dgv_Novedades.BackgroundColor = System.Drawing.Color.White;
      this.dgv_Novedades.CausesValidation = false;
      dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
      dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
      dataGridViewCellStyle9.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold);
      dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
      dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
      dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.dgv_Novedades.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
      this.dgv_Novedades.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgv_Novedades.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.Empresa,
            this.NroDeAsignacion,
            this.Comentario,
            this.Emisor});
      this.dgv_Novedades.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
      this.dgv_Novedades.Location = new System.Drawing.Point(341, 74);
      this.dgv_Novedades.Name = "dgv_Novedades";
      this.dgv_Novedades.ReadOnly = true;
      this.dgv_Novedades.RowHeadersVisible = false;
      dataGridViewCellStyle12.BackColor = System.Drawing.Color.Silver;
      this.dgv_Novedades.RowsDefaultCellStyle = dataGridViewCellStyle12;
      this.dgv_Novedades.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
      this.dgv_Novedades.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.dgv_Novedades.Size = new System.Drawing.Size(780, 204);
      this.dgv_Novedades.TabIndex = 571;
      // 
      // dataGridViewTextBoxColumn1
      // 
      this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
      this.dataGridViewTextBoxColumn1.DataPropertyName = "Fecha";
      dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle10.Format = "d";
      dataGridViewCellStyle10.NullValue = null;
      this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle10;
      this.dataGridViewTextBoxColumn1.HeaderText = "Fecha";
      this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
      this.dataGridViewTextBoxColumn1.ReadOnly = true;
      this.dataGridViewTextBoxColumn1.Width = 80;
      // 
      // Empresa
      // 
      this.Empresa.DataPropertyName = "Empresa";
      this.Empresa.HeaderText = "Empresa";
      this.Empresa.Name = "Empresa";
      this.Empresa.ReadOnly = true;
      this.Empresa.Width = 180;
      // 
      // NroDeAsignacion
      // 
      this.NroDeAsignacion.DataPropertyName = "NroDeAsig";
      this.NroDeAsignacion.HeaderText = "Asig";
      this.NroDeAsignacion.Name = "NroDeAsignacion";
      this.NroDeAsignacion.ReadOnly = true;
      this.NroDeAsignacion.Width = 40;
      // 
      // Comentario
      // 
      this.Comentario.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
      this.Comentario.DataPropertyName = "Novedad";
      dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
      dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.Comentario.DefaultCellStyle = dataGridViewCellStyle11;
      this.Comentario.HeaderText = "Novedades";
      this.Comentario.Name = "Comentario";
      this.Comentario.ReadOnly = true;
      this.Comentario.Width = 355;
      // 
      // Emisor
      // 
      this.Emisor.DataPropertyName = "Emisor";
      this.Emisor.HeaderText = "Emisor";
      this.Emisor.Name = "Emisor";
      this.Emisor.ReadOnly = true;
      // 
      // panel1
      // 
      this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(151)))), ((int)(((byte)(119)))));
      this.panel1.Controls.Add(this.bunifuCustomLabel1);
      this.panel1.Location = new System.Drawing.Point(341, 42);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(780, 26);
      this.panel1.TabIndex = 572;
      // 
      // bunifuCustomLabel1
      // 
      this.bunifuCustomLabel1.AutoSize = true;
      this.bunifuCustomLabel1.BackColor = System.Drawing.Color.Transparent;
      this.bunifuCustomLabel1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.bunifuCustomLabel1.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.bunifuCustomLabel1.Location = new System.Drawing.Point(292, 2);
      this.bunifuCustomLabel1.Name = "bunifuCustomLabel1";
      this.bunifuCustomLabel1.Size = new System.Drawing.Size(178, 19);
      this.bunifuCustomLabel1.TabIndex = 197;
      this.bunifuCustomLabel1.Text = "Mensajes al Cobrador";
      // 
      // frm_CobranzasAsignadas
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
      this.ClientSize = new System.Drawing.Size(1124, 323);
      this.Controls.Add(this.panel1);
      this.Controls.Add(this.dgv_Novedades);
      this.Controls.Add(this.btn_Actualizar);
      this.Controls.Add(this.lbl_Cobrador);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.btn_VerAsignadas);
      this.Controls.Add(this.dgv_CbrAsignadas);
      this.Controls.Add(this.panel6);
      this.Name = "frm_CobranzasAsignadas";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Actas para Cobrar";
      this.Load += new System.EventHandler(this.frm_CobranzasAsignadas_Load);
      ((System.ComponentModel.ISupportInitialize)(this.dgv_CbrAsignadas)).EndInit();
      this.panel6.ResumeLayout(false);
      this.panel6.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dgv_Novedades)).EndInit();
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private Bunifu.Framework.UI.BunifuFlatButton btn_VerAsignadas;
    private System.Windows.Forms.DataGridView dgv_CbrAsignadas;
    private System.Windows.Forms.Panel panel6;
    private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel4;
    private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn86;
    private System.Windows.Forms.DataGridViewTextBoxColumn numero;
    private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn89;
    private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn90;
    private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn88;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label lbl_Cobrador;
    private Bunifu.Framework.UI.BunifuFlatButton btn_Actualizar;
    private System.Windows.Forms.DataGridView dgv_Novedades;
    private System.Windows.Forms.Panel panel1;
    private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel1;
    private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
    private System.Windows.Forms.DataGridViewTextBoxColumn Empresa;
    private System.Windows.Forms.DataGridViewTextBoxColumn NroDeAsignacion;
    private System.Windows.Forms.DataGridViewTextBoxColumn Comentario;
    private System.Windows.Forms.DataGridViewTextBoxColumn Emisor;
  }
}