namespace entrega_cupones.Formularios
{
  partial class frm_Mochilas2
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
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
      this.btn_reimprimir = new Bunifu.Framework.UI.BunifuFlatButton();
      this.btn_emitir_cupon = new Bunifu.Framework.UI.BunifuFlatButton();
      this.picbox_socio = new System.Windows.Forms.PictureBox();
      this.dgv_titu_benef = new System.Windows.Forms.DataGridView();
      this.nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Parentesco = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.CodigoFliar = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Exepcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.exepcionID = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dni = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.sexo = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Edad = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.EmitirCupon = new System.Windows.Forms.DataGridViewCheckBoxColumn();
      this.Emitir = new System.Windows.Forms.DataGridViewImageColumn();
      this.Estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.bunifuCustomLabel12 = new Bunifu.Framework.UI.BunifuCustomLabel();
      this.cbx_eventos = new System.Windows.Forms.ComboBox();
      this.lbl_Mochila = new Bunifu.Framework.UI.BunifuCustomLabel();
      this.cbx_Mochilas = new System.Windows.Forms.ComboBox();
      this.bunifuCustomLabel11 = new Bunifu.Framework.UI.BunifuCustomLabel();
      this.txt_QuienRetira = new System.Windows.Forms.TextBox();
      this.chk_FondoDeDesempleo = new System.Windows.Forms.CheckBox();
      this.gbx_exepciones = new System.Windows.Forms.GroupBox();
      this.bunifuCustomLabel8 = new Bunifu.Framework.UI.BunifuCustomLabel();
      this.cbx_sexo = new System.Windows.Forms.ComboBox();
      this.msk_fecha_nac = new System.Windows.Forms.MaskedTextBox();
      this.btn_cargar_exepcion = new Bunifu.Framework.UI.BunifuFlatButton();
      this.bunifuCustomLabel7 = new Bunifu.Framework.UI.BunifuCustomLabel();
      this.cbx_parentesco = new System.Windows.Forms.ComboBox();
      this.txt_edad = new System.Windows.Forms.TextBox();
      this.bunifuCustomLabel2 = new Bunifu.Framework.UI.BunifuCustomLabel();
      this.bunifuCustomLabel6 = new Bunifu.Framework.UI.BunifuCustomLabel();
      this.bunifuCustomLabel5 = new Bunifu.Framework.UI.BunifuCustomLabel();
      this.bunifuCustomLabel4 = new Bunifu.Framework.UI.BunifuCustomLabel();
      this.bunifuCustomLabel3 = new Bunifu.Framework.UI.BunifuCustomLabel();
      this.txt_nombre = new System.Windows.Forms.TextBox();
      this.txt_apellido = new System.Windows.Forms.TextBox();
      this.txt_dni = new System.Windows.Forms.TextBox();
      this.lbl_CuponesEmitidos = new System.Windows.Forms.Label();
      this.lbl_TotalCuponesEmitidos = new System.Windows.Forms.Label();
      this.lbl_Turno = new System.Windows.Forms.Label();
      this.chk_Termas = new System.Windows.Forms.CheckBox();
      ((System.ComponentModel.ISupportInitialize)(this.picbox_socio)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.dgv_titu_benef)).BeginInit();
      this.gbx_exepciones.SuspendLayout();
      this.SuspendLayout();
      // 
      // btn_reimprimir
      // 
      this.btn_reimprimir.Active = false;
      this.btn_reimprimir.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(174)))), ((int)(((byte)(70)))));
      this.btn_reimprimir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(151)))), ((int)(((byte)(119)))));
      this.btn_reimprimir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
      this.btn_reimprimir.BorderRadius = 5;
      this.btn_reimprimir.ButtonText = "Reimprimir";
      this.btn_reimprimir.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btn_reimprimir.DisabledColor = System.Drawing.Color.Gray;
      this.btn_reimprimir.Iconcolor = System.Drawing.Color.Transparent;
      this.btn_reimprimir.Iconimage = global::entrega_cupones.Properties.Resources.refresh_arrow;
      this.btn_reimprimir.Iconimage_right = null;
      this.btn_reimprimir.Iconimage_right_Selected = null;
      this.btn_reimprimir.Iconimage_Selected = null;
      this.btn_reimprimir.IconMarginLeft = 0;
      this.btn_reimprimir.IconMarginRight = 0;
      this.btn_reimprimir.IconRightVisible = false;
      this.btn_reimprimir.IconRightZoom = 0D;
      this.btn_reimprimir.IconVisible = true;
      this.btn_reimprimir.IconZoom = 50D;
      this.btn_reimprimir.IsTab = false;
      this.btn_reimprimir.Location = new System.Drawing.Point(688, 288);
      this.btn_reimprimir.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.btn_reimprimir.Name = "btn_reimprimir";
      this.btn_reimprimir.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(151)))), ((int)(((byte)(119)))));
      this.btn_reimprimir.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(74)))), ((int)(((byte)(0)))));
      this.btn_reimprimir.OnHoverTextColor = System.Drawing.Color.White;
      this.btn_reimprimir.selected = false;
      this.btn_reimprimir.Size = new System.Drawing.Size(123, 34);
      this.btn_reimprimir.TabIndex = 448;
      this.btn_reimprimir.Text = "Reimprimir";
      this.btn_reimprimir.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.btn_reimprimir.Textcolor = System.Drawing.Color.White;
      this.btn_reimprimir.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_reimprimir.Click += new System.EventHandler(this.btn_reimprimir_Click_1);
      // 
      // btn_emitir_cupon
      // 
      this.btn_emitir_cupon.Active = false;
      this.btn_emitir_cupon.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(174)))), ((int)(((byte)(70)))));
      this.btn_emitir_cupon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(151)))), ((int)(((byte)(119)))));
      this.btn_emitir_cupon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
      this.btn_emitir_cupon.BorderRadius = 5;
      this.btn_emitir_cupon.ButtonText = "Emitir Cupon";
      this.btn_emitir_cupon.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btn_emitir_cupon.DisabledColor = System.Drawing.Color.Gray;
      this.btn_emitir_cupon.Iconcolor = System.Drawing.Color.Transparent;
      this.btn_emitir_cupon.Iconimage = global::entrega_cupones.Properties.Resources.check30___32;
      this.btn_emitir_cupon.Iconimage_right = null;
      this.btn_emitir_cupon.Iconimage_right_Selected = null;
      this.btn_emitir_cupon.Iconimage_Selected = null;
      this.btn_emitir_cupon.IconMarginLeft = 0;
      this.btn_emitir_cupon.IconMarginRight = 0;
      this.btn_emitir_cupon.IconRightVisible = true;
      this.btn_emitir_cupon.IconRightZoom = 0D;
      this.btn_emitir_cupon.IconVisible = true;
      this.btn_emitir_cupon.IconZoom = 50D;
      this.btn_emitir_cupon.IsTab = false;
      this.btn_emitir_cupon.Location = new System.Drawing.Point(688, 246);
      this.btn_emitir_cupon.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.btn_emitir_cupon.Name = "btn_emitir_cupon";
      this.btn_emitir_cupon.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(151)))), ((int)(((byte)(119)))));
      this.btn_emitir_cupon.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(74)))), ((int)(((byte)(0)))));
      this.btn_emitir_cupon.OnHoverTextColor = System.Drawing.Color.White;
      this.btn_emitir_cupon.selected = false;
      this.btn_emitir_cupon.Size = new System.Drawing.Size(123, 34);
      this.btn_emitir_cupon.TabIndex = 445;
      this.btn_emitir_cupon.Text = "Emitir Cupon";
      this.btn_emitir_cupon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.btn_emitir_cupon.Textcolor = System.Drawing.Color.White;
      this.btn_emitir_cupon.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_emitir_cupon.Click += new System.EventHandler(this.btn_emitir_cupon_Click_1);
      // 
      // picbox_socio
      // 
      this.picbox_socio.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.picbox_socio.Location = new System.Drawing.Point(12, 128);
      this.picbox_socio.Name = "picbox_socio";
      this.picbox_socio.Size = new System.Drawing.Size(110, 110);
      this.picbox_socio.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.picbox_socio.TabIndex = 443;
      this.picbox_socio.TabStop = false;
      // 
      // dgv_titu_benef
      // 
      this.dgv_titu_benef.AllowUserToAddRows = false;
      this.dgv_titu_benef.AllowUserToDeleteRows = false;
      this.dgv_titu_benef.AllowUserToResizeColumns = false;
      this.dgv_titu_benef.AllowUserToResizeRows = false;
      this.dgv_titu_benef.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
      dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
      dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
      dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.dgv_titu_benef.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
      this.dgv_titu_benef.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgv_titu_benef.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nombre,
            this.Parentesco,
            this.CodigoFliar,
            this.Exepcion,
            this.exepcionID,
            this.dni,
            this.sexo,
            this.Edad,
            this.EmitirCupon,
            this.Emitir,
            this.Estado});
      this.dgv_titu_benef.Cursor = System.Windows.Forms.Cursors.Hand;
      this.dgv_titu_benef.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
      this.dgv_titu_benef.EnableHeadersVisualStyles = false;
      this.dgv_titu_benef.Location = new System.Drawing.Point(127, 128);
      this.dgv_titu_benef.Name = "dgv_titu_benef";
      this.dgv_titu_benef.ReadOnly = true;
      this.dgv_titu_benef.RowHeadersVisible = false;
      this.dgv_titu_benef.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.dgv_titu_benef.Size = new System.Drawing.Size(684, 110);
      this.dgv_titu_benef.TabIndex = 442;
      this.dgv_titu_benef.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_titu_benef_CellContentClick_1);
      this.dgv_titu_benef.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgv_titu_benef_CurrentCellDirtyStateChanged_1);
      this.dgv_titu_benef.SelectionChanged += new System.EventHandler(this.dgv_titu_benef_SelectionChanged_1);
      // 
      // nombre
      // 
      this.nombre.DataPropertyName = "nombre";
      this.nombre.HeaderText = "Apellido y Nombre";
      this.nombre.Name = "nombre";
      this.nombre.ReadOnly = true;
      this.nombre.Width = 230;
      // 
      // Parentesco
      // 
      this.Parentesco.DataPropertyName = "Parentesco";
      this.Parentesco.HeaderText = "Parentesco";
      this.Parentesco.Name = "Parentesco";
      this.Parentesco.ReadOnly = true;
      this.Parentesco.Width = 120;
      // 
      // CodigoFliar
      // 
      this.CodigoFliar.DataPropertyName = "CodigoFliar";
      this.CodigoFliar.HeaderText = "cod fliar";
      this.CodigoFliar.Name = "CodigoFliar";
      this.CodigoFliar.ReadOnly = true;
      this.CodigoFliar.Visible = false;
      // 
      // Exepcion
      // 
      this.Exepcion.HeaderText = "exepcion";
      this.Exepcion.Name = "Exepcion";
      this.Exepcion.ReadOnly = true;
      this.Exepcion.Visible = false;
      // 
      // exepcionID
      // 
      this.exepcionID.HeaderText = "exepcionID";
      this.exepcionID.Name = "exepcionID";
      this.exepcionID.ReadOnly = true;
      this.exepcionID.Visible = false;
      // 
      // dni
      // 
      this.dni.DataPropertyName = "cuil";
      this.dni.HeaderText = "D.N.I.";
      this.dni.Name = "dni";
      this.dni.ReadOnly = true;
      this.dni.Width = 85;
      // 
      // sexo
      // 
      this.sexo.DataPropertyName = "Sexo";
      this.sexo.HeaderText = "Sexo";
      this.sexo.Name = "sexo";
      this.sexo.ReadOnly = true;
      this.sexo.Width = 50;
      // 
      // Edad
      // 
      this.Edad.DataPropertyName = "Edad";
      this.Edad.HeaderText = "Edad";
      this.Edad.Name = "Edad";
      this.Edad.ReadOnly = true;
      this.Edad.Width = 50;
      // 
      // EmitirCupon
      // 
      this.EmitirCupon.HeaderText = "Emitir Cupon";
      this.EmitirCupon.Name = "EmitirCupon";
      this.EmitirCupon.ReadOnly = true;
      this.EmitirCupon.Visible = false;
      this.EmitirCupon.Width = 90;
      // 
      // Emitir
      // 
      this.Emitir.HeaderText = "Emitir";
      this.Emitir.Name = "Emitir";
      this.Emitir.ReadOnly = true;
      this.Emitir.Visible = false;
      this.Emitir.Width = 50;
      // 
      // Estado
      // 
      dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
      this.Estado.DefaultCellStyle = dataGridViewCellStyle2;
      this.Estado.HeaderText = "Estado";
      this.Estado.Name = "Estado";
      this.Estado.ReadOnly = true;
      this.Estado.Width = 120;
      // 
      // bunifuCustomLabel12
      // 
      this.bunifuCustomLabel12.AutoSize = true;
      this.bunifuCustomLabel12.BackColor = System.Drawing.Color.Transparent;
      this.bunifuCustomLabel12.Enabled = false;
      this.bunifuCustomLabel12.Font = new System.Drawing.Font("Century Gothic", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.bunifuCustomLabel12.ForeColor = System.Drawing.Color.Black;
      this.bunifuCustomLabel12.Location = new System.Drawing.Point(9, 11);
      this.bunifuCustomLabel12.Name = "bunifuCustomLabel12";
      this.bunifuCustomLabel12.Size = new System.Drawing.Size(57, 17);
      this.bunifuCustomLabel12.TabIndex = 440;
      this.bunifuCustomLabel12.Text = "Evento:";
      // 
      // cbx_eventos
      // 
      this.cbx_eventos.BackColor = System.Drawing.Color.White;
      this.cbx_eventos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbx_eventos.Font = new System.Drawing.Font("Century Gothic", 9.5F);
      this.cbx_eventos.ForeColor = System.Drawing.Color.Black;
      this.cbx_eventos.FormattingEnabled = true;
      this.cbx_eventos.Location = new System.Drawing.Point(72, 7);
      this.cbx_eventos.Name = "cbx_eventos";
      this.cbx_eventos.Size = new System.Drawing.Size(486, 25);
      this.cbx_eventos.TabIndex = 441;
      this.cbx_eventos.SelectedIndexChanged += new System.EventHandler(this.cbx_eventos_SelectedIndexChanged);
      // 
      // lbl_Mochila
      // 
      this.lbl_Mochila.AutoSize = true;
      this.lbl_Mochila.BackColor = System.Drawing.Color.Transparent;
      this.lbl_Mochila.Enabled = false;
      this.lbl_Mochila.Font = new System.Drawing.Font("Century Gothic", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_Mochila.ForeColor = System.Drawing.Color.Black;
      this.lbl_Mochila.Location = new System.Drawing.Point(18, 246);
      this.lbl_Mochila.Name = "lbl_Mochila";
      this.lbl_Mochila.Size = new System.Drawing.Size(63, 17);
      this.lbl_Mochila.TabIndex = 451;
      this.lbl_Mochila.Text = "Mochila:";
      // 
      // cbx_Mochilas
      // 
      this.cbx_Mochilas.BackColor = System.Drawing.Color.White;
      this.cbx_Mochilas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbx_Mochilas.Font = new System.Drawing.Font("Century Gothic", 9.5F);
      this.cbx_Mochilas.ForeColor = System.Drawing.Color.Black;
      this.cbx_Mochilas.FormattingEnabled = true;
      this.cbx_Mochilas.Location = new System.Drawing.Point(85, 242);
      this.cbx_Mochilas.Name = "cbx_Mochilas";
      this.cbx_Mochilas.Size = new System.Drawing.Size(264, 25);
      this.cbx_Mochilas.TabIndex = 452;
      // 
      // bunifuCustomLabel11
      // 
      this.bunifuCustomLabel11.AutoSize = true;
      this.bunifuCustomLabel11.BackColor = System.Drawing.Color.Transparent;
      this.bunifuCustomLabel11.Enabled = false;
      this.bunifuCustomLabel11.Font = new System.Drawing.Font("Century Gothic", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.bunifuCustomLabel11.ForeColor = System.Drawing.Color.Black;
      this.bunifuCustomLabel11.Location = new System.Drawing.Point(15, 279);
      this.bunifuCustomLabel11.Name = "bunifuCustomLabel11";
      this.bunifuCustomLabel11.Size = new System.Drawing.Size(91, 17);
      this.bunifuCustomLabel11.TabIndex = 453;
      this.bunifuCustomLabel11.Text = "Quien Retira:";
      // 
      // txt_QuienRetira
      // 
      this.txt_QuienRetira.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txt_QuienRetira.Location = new System.Drawing.Point(112, 276);
      this.txt_QuienRetira.Name = "txt_QuienRetira";
      this.txt_QuienRetira.Size = new System.Drawing.Size(237, 22);
      this.txt_QuienRetira.TabIndex = 454;
      this.txt_QuienRetira.Text = "TITULAR";
      // 
      // chk_FondoDeDesempleo
      // 
      this.chk_FondoDeDesempleo.AutoSize = true;
      this.chk_FondoDeDesempleo.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_FondoDeDesempleo.Location = new System.Drawing.Point(365, 275);
      this.chk_FondoDeDesempleo.Name = "chk_FondoDeDesempleo";
      this.chk_FondoDeDesempleo.Size = new System.Drawing.Size(193, 23);
      this.chk_FondoDeDesempleo.TabIndex = 456;
      this.chk_FondoDeDesempleo.Text = "Es Fondo de Desempleo";
      this.chk_FondoDeDesempleo.UseVisualStyleBackColor = true;
      // 
      // gbx_exepciones
      // 
      this.gbx_exepciones.Controls.Add(this.bunifuCustomLabel8);
      this.gbx_exepciones.Controls.Add(this.cbx_sexo);
      this.gbx_exepciones.Controls.Add(this.msk_fecha_nac);
      this.gbx_exepciones.Controls.Add(this.btn_cargar_exepcion);
      this.gbx_exepciones.Controls.Add(this.bunifuCustomLabel7);
      this.gbx_exepciones.Controls.Add(this.cbx_parentesco);
      this.gbx_exepciones.Controls.Add(this.txt_edad);
      this.gbx_exepciones.Controls.Add(this.bunifuCustomLabel2);
      this.gbx_exepciones.Controls.Add(this.bunifuCustomLabel6);
      this.gbx_exepciones.Controls.Add(this.bunifuCustomLabel5);
      this.gbx_exepciones.Controls.Add(this.bunifuCustomLabel4);
      this.gbx_exepciones.Controls.Add(this.bunifuCustomLabel3);
      this.gbx_exepciones.Controls.Add(this.txt_nombre);
      this.gbx_exepciones.Controls.Add(this.txt_apellido);
      this.gbx_exepciones.Controls.Add(this.txt_dni);
      this.gbx_exepciones.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.gbx_exepciones.Location = new System.Drawing.Point(12, 38);
      this.gbx_exepciones.Name = "gbx_exepciones";
      this.gbx_exepciones.Size = new System.Drawing.Size(799, 84);
      this.gbx_exepciones.TabIndex = 445;
      this.gbx_exepciones.TabStop = false;
      this.gbx_exepciones.Text = "Exepciones";
      this.gbx_exepciones.Visible = false;
      // 
      // bunifuCustomLabel8
      // 
      this.bunifuCustomLabel8.AutoSize = true;
      this.bunifuCustomLabel8.BackColor = System.Drawing.Color.Transparent;
      this.bunifuCustomLabel8.Enabled = false;
      this.bunifuCustomLabel8.Font = new System.Drawing.Font("Century Gothic", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.bunifuCustomLabel8.ForeColor = System.Drawing.Color.Black;
      this.bunifuCustomLabel8.Location = new System.Drawing.Point(557, 22);
      this.bunifuCustomLabel8.Name = "bunifuCustomLabel8";
      this.bunifuCustomLabel8.Size = new System.Drawing.Size(41, 17);
      this.bunifuCustomLabel8.TabIndex = 436;
      this.bunifuCustomLabel8.Text = "Sexo:";
      // 
      // cbx_sexo
      // 
      this.cbx_sexo.BackColor = System.Drawing.Color.White;
      this.cbx_sexo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbx_sexo.Enabled = false;
      this.cbx_sexo.Font = new System.Drawing.Font("Century Gothic", 9.5F);
      this.cbx_sexo.ForeColor = System.Drawing.Color.Black;
      this.cbx_sexo.FormattingEnabled = true;
      this.cbx_sexo.Items.AddRange(new object[] {
            "F",
            "M"});
      this.cbx_sexo.Location = new System.Drawing.Point(603, 19);
      this.cbx_sexo.Name = "cbx_sexo";
      this.cbx_sexo.Size = new System.Drawing.Size(67, 25);
      this.cbx_sexo.TabIndex = 435;
      // 
      // msk_fecha_nac
      // 
      this.msk_fecha_nac.Location = new System.Drawing.Point(60, 21);
      this.msk_fecha_nac.Mask = "00/00/0000";
      this.msk_fecha_nac.Name = "msk_fecha_nac";
      this.msk_fecha_nac.Size = new System.Drawing.Size(73, 22);
      this.msk_fecha_nac.TabIndex = 434;
      this.msk_fecha_nac.ValidatingType = typeof(System.DateTime);
      this.msk_fecha_nac.MaskInputRejected += new System.Windows.Forms.MaskInputRejectedEventHandler(this.msk_fecha_nac_MaskInputRejected);
      this.msk_fecha_nac.KeyDown += new System.Windows.Forms.KeyEventHandler(this.msk_fecha_nac_KeyDown);
      // 
      // btn_cargar_exepcion
      // 
      this.btn_cargar_exepcion.Active = false;
      this.btn_cargar_exepcion.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(174)))), ((int)(((byte)(70)))));
      this.btn_cargar_exepcion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(151)))), ((int)(((byte)(119)))));
      this.btn_cargar_exepcion.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
      this.btn_cargar_exepcion.BorderRadius = 5;
      this.btn_cargar_exepcion.ButtonText = "Cargar";
      this.btn_cargar_exepcion.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btn_cargar_exepcion.DisabledColor = System.Drawing.Color.Gray;
      this.btn_cargar_exepcion.Enabled = false;
      this.btn_cargar_exepcion.Iconcolor = System.Drawing.Color.Transparent;
      this.btn_cargar_exepcion.Iconimage = global::entrega_cupones.Properties.Resources.check30___32;
      this.btn_cargar_exepcion.Iconimage_right = null;
      this.btn_cargar_exepcion.Iconimage_right_Selected = null;
      this.btn_cargar_exepcion.Iconimage_Selected = null;
      this.btn_cargar_exepcion.IconMarginLeft = 0;
      this.btn_cargar_exepcion.IconMarginRight = 0;
      this.btn_cargar_exepcion.IconRightVisible = false;
      this.btn_cargar_exepcion.IconRightZoom = 0D;
      this.btn_cargar_exepcion.IconVisible = true;
      this.btn_cargar_exepcion.IconZoom = 50D;
      this.btn_cargar_exepcion.IsTab = false;
      this.btn_cargar_exepcion.Location = new System.Drawing.Point(676, 26);
      this.btn_cargar_exepcion.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.btn_cargar_exepcion.Name = "btn_cargar_exepcion";
      this.btn_cargar_exepcion.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(151)))), ((int)(((byte)(119)))));
      this.btn_cargar_exepcion.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(74)))), ((int)(((byte)(0)))));
      this.btn_cargar_exepcion.OnHoverTextColor = System.Drawing.Color.White;
      this.btn_cargar_exepcion.selected = false;
      this.btn_cargar_exepcion.Size = new System.Drawing.Size(117, 34);
      this.btn_cargar_exepcion.TabIndex = 431;
      this.btn_cargar_exepcion.Text = "Cargar";
      this.btn_cargar_exepcion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.btn_cargar_exepcion.Textcolor = System.Drawing.Color.White;
      this.btn_cargar_exepcion.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_cargar_exepcion.Click += new System.EventHandler(this.btn_cargar_exepcion_Click);
      // 
      // bunifuCustomLabel7
      // 
      this.bunifuCustomLabel7.AutoSize = true;
      this.bunifuCustomLabel7.BackColor = System.Drawing.Color.Transparent;
      this.bunifuCustomLabel7.Enabled = false;
      this.bunifuCustomLabel7.Font = new System.Drawing.Font("Century Gothic", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.bunifuCustomLabel7.ForeColor = System.Drawing.Color.Black;
      this.bunifuCustomLabel7.Location = new System.Drawing.Point(403, 58);
      this.bunifuCustomLabel7.Name = "bunifuCustomLabel7";
      this.bunifuCustomLabel7.Size = new System.Drawing.Size(84, 17);
      this.bunifuCustomLabel7.TabIndex = 430;
      this.bunifuCustomLabel7.Text = "Parentesco:";
      // 
      // cbx_parentesco
      // 
      this.cbx_parentesco.BackColor = System.Drawing.Color.White;
      this.cbx_parentesco.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbx_parentesco.Enabled = false;
      this.cbx_parentesco.Font = new System.Drawing.Font("Century Gothic", 9.5F);
      this.cbx_parentesco.ForeColor = System.Drawing.Color.Black;
      this.cbx_parentesco.FormattingEnabled = true;
      this.cbx_parentesco.Location = new System.Drawing.Point(494, 50);
      this.cbx_parentesco.Name = "cbx_parentesco";
      this.cbx_parentesco.Size = new System.Drawing.Size(176, 25);
      this.cbx_parentesco.TabIndex = 429;
      // 
      // txt_edad
      // 
      this.txt_edad.BackColor = System.Drawing.Color.White;
      this.txt_edad.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.txt_edad.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txt_edad.Location = new System.Drawing.Point(60, 53);
      this.txt_edad.Name = "txt_edad";
      this.txt_edad.ReadOnly = true;
      this.txt_edad.Size = new System.Drawing.Size(73, 22);
      this.txt_edad.TabIndex = 428;
      // 
      // bunifuCustomLabel2
      // 
      this.bunifuCustomLabel2.AutoSize = true;
      this.bunifuCustomLabel2.BackColor = System.Drawing.Color.Transparent;
      this.bunifuCustomLabel2.Enabled = false;
      this.bunifuCustomLabel2.Font = new System.Drawing.Font("Century Gothic", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.bunifuCustomLabel2.ForeColor = System.Drawing.Color.Black;
      this.bunifuCustomLabel2.Location = new System.Drawing.Point(6, 21);
      this.bunifuCustomLabel2.Name = "bunifuCustomLabel2";
      this.bunifuCustomLabel2.Size = new System.Drawing.Size(57, 17);
      this.bunifuCustomLabel2.TabIndex = 426;
      this.bunifuCustomLabel2.Text = "F. Nac.:";
      // 
      // bunifuCustomLabel6
      // 
      this.bunifuCustomLabel6.AutoSize = true;
      this.bunifuCustomLabel6.BackColor = System.Drawing.Color.Transparent;
      this.bunifuCustomLabel6.Enabled = false;
      this.bunifuCustomLabel6.Font = new System.Drawing.Font("Century Gothic", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.bunifuCustomLabel6.ForeColor = System.Drawing.Color.Black;
      this.bunifuCustomLabel6.Location = new System.Drawing.Point(149, 58);
      this.bunifuCustomLabel6.Name = "bunifuCustomLabel6";
      this.bunifuCustomLabel6.Size = new System.Drawing.Size(65, 17);
      this.bunifuCustomLabel6.TabIndex = 425;
      this.bunifuCustomLabel6.Text = "Nombre:";
      // 
      // bunifuCustomLabel5
      // 
      this.bunifuCustomLabel5.AutoSize = true;
      this.bunifuCustomLabel5.BackColor = System.Drawing.Color.Transparent;
      this.bunifuCustomLabel5.Enabled = false;
      this.bunifuCustomLabel5.Font = new System.Drawing.Font("Century Gothic", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.bunifuCustomLabel5.ForeColor = System.Drawing.Color.Black;
      this.bunifuCustomLabel5.Location = new System.Drawing.Point(153, 21);
      this.bunifuCustomLabel5.Name = "bunifuCustomLabel5";
      this.bunifuCustomLabel5.Size = new System.Drawing.Size(65, 17);
      this.bunifuCustomLabel5.TabIndex = 424;
      this.bunifuCustomLabel5.Text = "Apellido:";
      // 
      // bunifuCustomLabel4
      // 
      this.bunifuCustomLabel4.AutoSize = true;
      this.bunifuCustomLabel4.BackColor = System.Drawing.Color.Transparent;
      this.bunifuCustomLabel4.Enabled = false;
      this.bunifuCustomLabel4.Font = new System.Drawing.Font("Century Gothic", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.bunifuCustomLabel4.ForeColor = System.Drawing.Color.Black;
      this.bunifuCustomLabel4.Location = new System.Drawing.Point(403, 21);
      this.bunifuCustomLabel4.Name = "bunifuCustomLabel4";
      this.bunifuCustomLabel4.Size = new System.Drawing.Size(47, 17);
      this.bunifuCustomLabel4.TabIndex = 423;
      this.bunifuCustomLabel4.Text = "D.N.I.:";
      // 
      // bunifuCustomLabel3
      // 
      this.bunifuCustomLabel3.AutoSize = true;
      this.bunifuCustomLabel3.BackColor = System.Drawing.Color.Transparent;
      this.bunifuCustomLabel3.Enabled = false;
      this.bunifuCustomLabel3.Font = new System.Drawing.Font("Century Gothic", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.bunifuCustomLabel3.ForeColor = System.Drawing.Color.Black;
      this.bunifuCustomLabel3.Location = new System.Drawing.Point(17, 58);
      this.bunifuCustomLabel3.Name = "bunifuCustomLabel3";
      this.bunifuCustomLabel3.Size = new System.Drawing.Size(46, 17);
      this.bunifuCustomLabel3.TabIndex = 422;
      this.bunifuCustomLabel3.Text = "Edad:";
      // 
      // txt_nombre
      // 
      this.txt_nombre.Enabled = false;
      this.txt_nombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txt_nombre.Location = new System.Drawing.Point(220, 53);
      this.txt_nombre.Name = "txt_nombre";
      this.txt_nombre.Size = new System.Drawing.Size(173, 22);
      this.txt_nombre.TabIndex = 421;
      // 
      // txt_apellido
      // 
      this.txt_apellido.Enabled = false;
      this.txt_apellido.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txt_apellido.Location = new System.Drawing.Point(220, 19);
      this.txt_apellido.Name = "txt_apellido";
      this.txt_apellido.Size = new System.Drawing.Size(173, 22);
      this.txt_apellido.TabIndex = 420;
      // 
      // txt_dni
      // 
      this.txt_dni.Enabled = false;
      this.txt_dni.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txt_dni.Location = new System.Drawing.Point(456, 21);
      this.txt_dni.Name = "txt_dni";
      this.txt_dni.Size = new System.Drawing.Size(90, 22);
      this.txt_dni.TabIndex = 419;
      // 
      // lbl_CuponesEmitidos
      // 
      this.lbl_CuponesEmitidos.AutoSize = true;
      this.lbl_CuponesEmitidos.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_CuponesEmitidos.Location = new System.Drawing.Point(15, 310);
      this.lbl_CuponesEmitidos.Name = "lbl_CuponesEmitidos";
      this.lbl_CuponesEmitidos.Size = new System.Drawing.Size(128, 17);
      this.lbl_CuponesEmitidos.TabIndex = 457;
      this.lbl_CuponesEmitidos.Text = "Cupones Emitidos:";
      // 
      // lbl_TotalCuponesEmitidos
      // 
      this.lbl_TotalCuponesEmitidos.AutoSize = true;
      this.lbl_TotalCuponesEmitidos.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_TotalCuponesEmitidos.Location = new System.Drawing.Point(161, 310);
      this.lbl_TotalCuponesEmitidos.Name = "lbl_TotalCuponesEmitidos";
      this.lbl_TotalCuponesEmitidos.Size = new System.Drawing.Size(15, 17);
      this.lbl_TotalCuponesEmitidos.TabIndex = 458;
      this.lbl_TotalCuponesEmitidos.Text = "0";
      // 
      // lbl_Turno
      // 
      this.lbl_Turno.Font = new System.Drawing.Font("Century Gothic", 11F, System.Drawing.FontStyle.Bold);
      this.lbl_Turno.Location = new System.Drawing.Point(572, 9);
      this.lbl_Turno.Name = "lbl_Turno";
      this.lbl_Turno.Size = new System.Drawing.Size(239, 23);
      this.lbl_Turno.TabIndex = 459;
      this.lbl_Turno.Text = "Turno:";
      // 
      // chk_Termas
      // 
      this.chk_Termas.AutoSize = true;
      this.chk_Termas.Checked = true;
      this.chk_Termas.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chk_Termas.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_Termas.Location = new System.Drawing.Point(365, 242);
      this.chk_Termas.Name = "chk_Termas";
      this.chk_Termas.Size = new System.Drawing.Size(107, 23);
      this.chk_Termas.TabIndex = 460;
      this.chk_Termas.Text = "Filial Termas";
      this.chk_Termas.UseVisualStyleBackColor = true;
      // 
      // frm_Mochilas2
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(819, 332);
      this.Controls.Add(this.chk_Termas);
      this.Controls.Add(this.lbl_Turno);
      this.Controls.Add(this.lbl_TotalCuponesEmitidos);
      this.Controls.Add(this.lbl_CuponesEmitidos);
      this.Controls.Add(this.btn_reimprimir);
      this.Controls.Add(this.gbx_exepciones);
      this.Controls.Add(this.btn_emitir_cupon);
      this.Controls.Add(this.bunifuCustomLabel12);
      this.Controls.Add(this.cbx_eventos);
      this.Controls.Add(this.chk_FondoDeDesempleo);
      this.Controls.Add(this.picbox_socio);
      this.Controls.Add(this.lbl_Mochila);
      this.Controls.Add(this.cbx_Mochilas);
      this.Controls.Add(this.txt_QuienRetira);
      this.Controls.Add(this.bunifuCustomLabel11);
      this.Controls.Add(this.dgv_titu_benef);
      this.Name = "frm_Mochilas2";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Entrega de Mochilas  2021";
      this.Load += new System.EventHandler(this.frm_Mochilas2_Load);
      ((System.ComponentModel.ISupportInitialize)(this.picbox_socio)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.dgv_titu_benef)).EndInit();
      this.gbx_exepciones.ResumeLayout(false);
      this.gbx_exepciones.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private Bunifu.Framework.UI.BunifuFlatButton btn_reimprimir;
    private Bunifu.Framework.UI.BunifuFlatButton btn_emitir_cupon;
    private System.Windows.Forms.PictureBox picbox_socio;
    private System.Windows.Forms.DataGridView dgv_titu_benef;
    private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel12;
    private System.Windows.Forms.ComboBox cbx_eventos;
    private Bunifu.Framework.UI.BunifuCustomLabel lbl_Mochila;
    private System.Windows.Forms.ComboBox cbx_Mochilas;
    private System.Windows.Forms.DataGridViewTextBoxColumn nombre;
    private System.Windows.Forms.DataGridViewTextBoxColumn Parentesco;
    private System.Windows.Forms.DataGridViewTextBoxColumn CodigoFliar;
    private System.Windows.Forms.DataGridViewTextBoxColumn Exepcion;
    private System.Windows.Forms.DataGridViewTextBoxColumn exepcionID;
    private System.Windows.Forms.DataGridViewTextBoxColumn dni;
    private System.Windows.Forms.DataGridViewTextBoxColumn sexo;
    private System.Windows.Forms.DataGridViewTextBoxColumn Edad;
    private System.Windows.Forms.DataGridViewCheckBoxColumn EmitirCupon;
    private System.Windows.Forms.DataGridViewImageColumn Emitir;
    private System.Windows.Forms.DataGridViewTextBoxColumn Estado;
    private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel11;
    public System.Windows.Forms.TextBox txt_QuienRetira;
    private System.Windows.Forms.CheckBox chk_FondoDeDesempleo;
    private System.Windows.Forms.GroupBox gbx_exepciones;
    private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel8;
    private System.Windows.Forms.ComboBox cbx_sexo;
    private System.Windows.Forms.MaskedTextBox msk_fecha_nac;
    private Bunifu.Framework.UI.BunifuFlatButton btn_cargar_exepcion;
    private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel7;
    private System.Windows.Forms.ComboBox cbx_parentesco;
    public System.Windows.Forms.TextBox txt_edad;
    private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel2;
    private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel6;
    private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel5;
    private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel4;
    private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel3;
    public System.Windows.Forms.TextBox txt_nombre;
    public System.Windows.Forms.TextBox txt_apellido;
    public System.Windows.Forms.TextBox txt_dni;
        private System.Windows.Forms.Label lbl_CuponesEmitidos;
        private System.Windows.Forms.Label lbl_TotalCuponesEmitidos;
    private System.Windows.Forms.Label lbl_Turno;
    private System.Windows.Forms.CheckBox chk_Termas;
  }
}