namespace entrega_cupones.Formularios
{
  partial class frm_eventos
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_eventos));
      this.bunifuCustomLabel1 = new Bunifu.Framework.UI.BunifuCustomLabel();
      this.panel2 = new System.Windows.Forms.Panel();
      this.btn_minimizar = new Bunifu.Framework.UI.BunifuImageButton();
      this.btn_cerrar = new Bunifu.Framework.UI.BunifuImageButton();
      this.bunifuCustomLabel12 = new Bunifu.Framework.UI.BunifuCustomLabel();
      this.cbx_eventos = new System.Windows.Forms.ComboBox();
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
      this.txt_total_cupones = new System.Windows.Forms.TextBox();
      this.bunifuCustomLabel9 = new Bunifu.Framework.UI.BunifuCustomLabel();
      this.txt_total_exepciones = new System.Windows.Forms.TextBox();
      this.bunifuCustomLabel10 = new Bunifu.Framework.UI.BunifuCustomLabel();
      this.btn_reimprimir = new Bunifu.Framework.UI.BunifuFlatButton();
      this.btn_emitir_cupon = new Bunifu.Framework.UI.BunifuFlatButton();
      this.picbox_socio = new System.Windows.Forms.PictureBox();
      this.panel2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.btn_minimizar)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.btn_cerrar)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.dgv_titu_benef)).BeginInit();
      this.gbx_exepciones.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.picbox_socio)).BeginInit();
      this.SuspendLayout();
      // 
      // bunifuCustomLabel1
      // 
      this.bunifuCustomLabel1.AutoSize = true;
      this.bunifuCustomLabel1.Font = new System.Drawing.Font("Century Gothic", 18.75F, System.Drawing.FontStyle.Bold);
      this.bunifuCustomLabel1.ForeColor = System.Drawing.Color.White;
      this.bunifuCustomLabel1.Location = new System.Drawing.Point(9, 11);
      this.bunifuCustomLabel1.Name = "bunifuCustomLabel1";
      this.bunifuCustomLabel1.Size = new System.Drawing.Size(105, 29);
      this.bunifuCustomLabel1.TabIndex = 149;
      this.bunifuCustomLabel1.Text = "Eventos";
      // 
      // panel2
      // 
      this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(151)))), ((int)(((byte)(119)))));
      this.panel2.Controls.Add(this.btn_minimizar);
      this.panel2.Controls.Add(this.bunifuCustomLabel1);
      this.panel2.Controls.Add(this.btn_cerrar);
      this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel2.Location = new System.Drawing.Point(0, 0);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(871, 51);
      this.panel2.TabIndex = 3;
      // 
      // btn_minimizar
      // 
      this.btn_minimizar.BackColor = System.Drawing.Color.Transparent;
      this.btn_minimizar.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btn_minimizar.Image = global::entrega_cupones.Properties.Resources.minus__1_;
      this.btn_minimizar.ImageActive = null;
      this.btn_minimizar.Location = new System.Drawing.Point(796, 10);
      this.btn_minimizar.Name = "btn_minimizar";
      this.btn_minimizar.Size = new System.Drawing.Size(22, 30);
      this.btn_minimizar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
      this.btn_minimizar.TabIndex = 150;
      this.btn_minimizar.TabStop = false;
      this.btn_minimizar.Zoom = 10;
      this.btn_minimizar.Click += new System.EventHandler(this.btn_minimizar_Click);
      // 
      // btn_cerrar
      // 
      this.btn_cerrar.BackColor = System.Drawing.Color.Transparent;
      this.btn_cerrar.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btn_cerrar.Image = global::entrega_cupones.Properties.Resources.cross_close_or_delete_circular_interface_button_symbol;
      this.btn_cerrar.ImageActive = null;
      this.btn_cerrar.Location = new System.Drawing.Point(832, 10);
      this.btn_cerrar.Name = "btn_cerrar";
      this.btn_cerrar.Size = new System.Drawing.Size(31, 30);
      this.btn_cerrar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
      this.btn_cerrar.TabIndex = 147;
      this.btn_cerrar.TabStop = false;
      this.btn_cerrar.Zoom = 10;
      this.btn_cerrar.Click += new System.EventHandler(this.btn_cerrar_Click);
      // 
      // bunifuCustomLabel12
      // 
      this.bunifuCustomLabel12.AutoSize = true;
      this.bunifuCustomLabel12.BackColor = System.Drawing.Color.Transparent;
      this.bunifuCustomLabel12.Enabled = false;
      this.bunifuCustomLabel12.Font = new System.Drawing.Font("Century Gothic", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.bunifuCustomLabel12.ForeColor = System.Drawing.Color.Black;
      this.bunifuCustomLabel12.Location = new System.Drawing.Point(6, 61);
      this.bunifuCustomLabel12.Name = "bunifuCustomLabel12";
      this.bunifuCustomLabel12.Size = new System.Drawing.Size(57, 17);
      this.bunifuCustomLabel12.TabIndex = 177;
      this.bunifuCustomLabel12.Text = "Evento:";
      // 
      // cbx_eventos
      // 
      this.cbx_eventos.BackColor = System.Drawing.Color.White;
      this.cbx_eventos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbx_eventos.Font = new System.Drawing.Font("Century Gothic", 9.5F);
      this.cbx_eventos.ForeColor = System.Drawing.Color.Black;
      this.cbx_eventos.FormattingEnabled = true;
      this.cbx_eventos.Location = new System.Drawing.Point(69, 57);
      this.cbx_eventos.Name = "cbx_eventos";
      this.cbx_eventos.Size = new System.Drawing.Size(790, 25);
      this.cbx_eventos.TabIndex = 178;
      // 
      // dgv_titu_benef
      // 
      this.dgv_titu_benef.AllowUserToAddRows = false;
      this.dgv_titu_benef.AllowUserToDeleteRows = false;
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
      this.dgv_titu_benef.Location = new System.Drawing.Point(124, 193);
      this.dgv_titu_benef.Name = "dgv_titu_benef";
      this.dgv_titu_benef.ReadOnly = true;
      this.dgv_titu_benef.RowHeadersVisible = false;
      this.dgv_titu_benef.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.dgv_titu_benef.Size = new System.Drawing.Size(739, 110);
      this.dgv_titu_benef.TabIndex = 179;
      this.dgv_titu_benef.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_titu_benef_CellClick);
      this.dgv_titu_benef.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_titu_benef_CellContentClick);
      this.dgv_titu_benef.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgv_titu_benef_CurrentCellDirtyStateChanged);
      this.dgv_titu_benef.SelectionChanged += new System.EventHandler(this.dgv_titu_benef_SelectionChanged);
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
      this.gbx_exepciones.Location = new System.Drawing.Point(9, 91);
      this.gbx_exepciones.Name = "gbx_exepciones";
      this.gbx_exepciones.Size = new System.Drawing.Size(855, 97);
      this.gbx_exepciones.TabIndex = 181;
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
      this.bunifuCustomLabel8.Location = new System.Drawing.Point(592, 29);
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
      this.cbx_sexo.Location = new System.Drawing.Point(638, 26);
      this.cbx_sexo.Name = "cbx_sexo";
      this.cbx_sexo.Size = new System.Drawing.Size(67, 25);
      this.cbx_sexo.TabIndex = 435;
      // 
      // msk_fecha_nac
      // 
      this.msk_fecha_nac.Location = new System.Drawing.Point(75, 28);
      this.msk_fecha_nac.Mask = "00/00/0000";
      this.msk_fecha_nac.Name = "msk_fecha_nac";
      this.msk_fecha_nac.Size = new System.Drawing.Size(100, 22);
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
      this.btn_cargar_exepcion.Iconimage = ((System.Drawing.Image)(resources.GetObject("btn_cargar_exepcion.Iconimage")));
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
      this.btn_cargar_exepcion.Location = new System.Drawing.Point(737, 29);
      this.btn_cargar_exepcion.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.btn_cargar_exepcion.Name = "btn_cargar_exepcion";
      this.btn_cargar_exepcion.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(151)))), ((int)(((byte)(119)))));
      this.btn_cargar_exepcion.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(74)))), ((int)(((byte)(0)))));
      this.btn_cargar_exepcion.OnHoverTextColor = System.Drawing.Color.White;
      this.btn_cargar_exepcion.selected = false;
      this.btn_cargar_exepcion.Size = new System.Drawing.Size(106, 35);
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
      this.bunifuCustomLabel7.Location = new System.Drawing.Point(438, 65);
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
      this.cbx_parentesco.Location = new System.Drawing.Point(529, 57);
      this.cbx_parentesco.Name = "cbx_parentesco";
      this.cbx_parentesco.Size = new System.Drawing.Size(176, 25);
      this.cbx_parentesco.TabIndex = 429;
      this.cbx_parentesco.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbx_parentesco_KeyDown);
      // 
      // txt_edad
      // 
      this.txt_edad.BackColor = System.Drawing.Color.White;
      this.txt_edad.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.txt_edad.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txt_edad.Location = new System.Drawing.Point(75, 60);
      this.txt_edad.Name = "txt_edad";
      this.txt_edad.ReadOnly = true;
      this.txt_edad.Size = new System.Drawing.Size(100, 22);
      this.txt_edad.TabIndex = 428;
      // 
      // bunifuCustomLabel2
      // 
      this.bunifuCustomLabel2.AutoSize = true;
      this.bunifuCustomLabel2.BackColor = System.Drawing.Color.Transparent;
      this.bunifuCustomLabel2.Enabled = false;
      this.bunifuCustomLabel2.Font = new System.Drawing.Font("Century Gothic", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.bunifuCustomLabel2.ForeColor = System.Drawing.Color.Black;
      this.bunifuCustomLabel2.Location = new System.Drawing.Point(6, 28);
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
      this.bunifuCustomLabel6.Location = new System.Drawing.Point(185, 65);
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
      this.bunifuCustomLabel5.Location = new System.Drawing.Point(189, 28);
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
      this.bunifuCustomLabel4.Location = new System.Drawing.Point(438, 28);
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
      this.bunifuCustomLabel3.Location = new System.Drawing.Point(17, 65);
      this.bunifuCustomLabel3.Name = "bunifuCustomLabel3";
      this.bunifuCustomLabel3.Size = new System.Drawing.Size(46, 17);
      this.bunifuCustomLabel3.TabIndex = 422;
      this.bunifuCustomLabel3.Text = "Edad:";
      // 
      // txt_nombre
      // 
      this.txt_nombre.Enabled = false;
      this.txt_nombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txt_nombre.Location = new System.Drawing.Point(256, 60);
      this.txt_nombre.Name = "txt_nombre";
      this.txt_nombre.Size = new System.Drawing.Size(173, 22);
      this.txt_nombre.TabIndex = 421;
      this.txt_nombre.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_nombre_KeyDown);
      // 
      // txt_apellido
      // 
      this.txt_apellido.Enabled = false;
      this.txt_apellido.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txt_apellido.Location = new System.Drawing.Point(256, 26);
      this.txt_apellido.Name = "txt_apellido";
      this.txt_apellido.Size = new System.Drawing.Size(173, 22);
      this.txt_apellido.TabIndex = 420;
      this.txt_apellido.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_apellido_KeyDown);
      // 
      // txt_dni
      // 
      this.txt_dni.Enabled = false;
      this.txt_dni.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txt_dni.Location = new System.Drawing.Point(491, 28);
      this.txt_dni.Name = "txt_dni";
      this.txt_dni.Size = new System.Drawing.Size(90, 22);
      this.txt_dni.TabIndex = 419;
      this.txt_dni.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_dni_KeyDown);
      // 
      // txt_total_cupones
      // 
      this.txt_total_cupones.BackColor = System.Drawing.Color.White;
      this.txt_total_cupones.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.txt_total_cupones.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txt_total_cupones.Location = new System.Drawing.Point(762, 317);
      this.txt_total_cupones.Name = "txt_total_cupones";
      this.txt_total_cupones.ReadOnly = true;
      this.txt_total_cupones.Size = new System.Drawing.Size(100, 22);
      this.txt_total_cupones.TabIndex = 436;
      // 
      // bunifuCustomLabel9
      // 
      this.bunifuCustomLabel9.AutoSize = true;
      this.bunifuCustomLabel9.BackColor = System.Drawing.Color.Transparent;
      this.bunifuCustomLabel9.Enabled = false;
      this.bunifuCustomLabel9.Font = new System.Drawing.Font("Century Gothic", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.bunifuCustomLabel9.ForeColor = System.Drawing.Color.Black;
      this.bunifuCustomLabel9.Location = new System.Drawing.Point(654, 320);
      this.bunifuCustomLabel9.Name = "bunifuCustomLabel9";
      this.bunifuCustomLabel9.Size = new System.Drawing.Size(105, 17);
      this.bunifuCustomLabel9.TabIndex = 435;
      this.bunifuCustomLabel9.Text = "Total Cupones:";
      // 
      // txt_total_exepciones
      // 
      this.txt_total_exepciones.BackColor = System.Drawing.Color.White;
      this.txt_total_exepciones.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.txt_total_exepciones.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txt_total_exepciones.Location = new System.Drawing.Point(762, 345);
      this.txt_total_exepciones.Name = "txt_total_exepciones";
      this.txt_total_exepciones.ReadOnly = true;
      this.txt_total_exepciones.Size = new System.Drawing.Size(100, 22);
      this.txt_total_exepciones.TabIndex = 438;
      // 
      // bunifuCustomLabel10
      // 
      this.bunifuCustomLabel10.AutoSize = true;
      this.bunifuCustomLabel10.BackColor = System.Drawing.Color.Transparent;
      this.bunifuCustomLabel10.Enabled = false;
      this.bunifuCustomLabel10.Font = new System.Drawing.Font("Century Gothic", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.bunifuCustomLabel10.ForeColor = System.Drawing.Color.Black;
      this.bunifuCustomLabel10.Location = new System.Drawing.Point(642, 350);
      this.bunifuCustomLabel10.Name = "bunifuCustomLabel10";
      this.bunifuCustomLabel10.Size = new System.Drawing.Size(118, 17);
      this.bunifuCustomLabel10.TabIndex = 437;
      this.bunifuCustomLabel10.Text = "Total Exepciones:";
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
      this.btn_reimprimir.Location = new System.Drawing.Point(382, 324);
      this.btn_reimprimir.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.btn_reimprimir.Name = "btn_reimprimir";
      this.btn_reimprimir.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(151)))), ((int)(((byte)(119)))));
      this.btn_reimprimir.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(74)))), ((int)(((byte)(0)))));
      this.btn_reimprimir.OnHoverTextColor = System.Drawing.Color.White;
      this.btn_reimprimir.selected = false;
      this.btn_reimprimir.Size = new System.Drawing.Size(112, 34);
      this.btn_reimprimir.TabIndex = 439;
      this.btn_reimprimir.Text = "Reimprimir";
      this.btn_reimprimir.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.btn_reimprimir.Textcolor = System.Drawing.Color.White;
      this.btn_reimprimir.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_reimprimir.Click += new System.EventHandler(this.btn_reimprimir_Click);
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
      this.btn_emitir_cupon.Iconimage = ((System.Drawing.Image)(resources.GetObject("btn_emitir_cupon.Iconimage")));
      this.btn_emitir_cupon.Iconimage_right = null;
      this.btn_emitir_cupon.Iconimage_right_Selected = null;
      this.btn_emitir_cupon.Iconimage_Selected = null;
      this.btn_emitir_cupon.IconMarginLeft = 0;
      this.btn_emitir_cupon.IconMarginRight = 0;
      this.btn_emitir_cupon.IconRightVisible = false;
      this.btn_emitir_cupon.IconRightZoom = 0D;
      this.btn_emitir_cupon.IconVisible = true;
      this.btn_emitir_cupon.IconZoom = 50D;
      this.btn_emitir_cupon.IsTab = false;
      this.btn_emitir_cupon.Location = new System.Drawing.Point(93, 324);
      this.btn_emitir_cupon.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.btn_emitir_cupon.Name = "btn_emitir_cupon";
      this.btn_emitir_cupon.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(151)))), ((int)(((byte)(119)))));
      this.btn_emitir_cupon.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(74)))), ((int)(((byte)(0)))));
      this.btn_emitir_cupon.OnHoverTextColor = System.Drawing.Color.White;
      this.btn_emitir_cupon.selected = false;
      this.btn_emitir_cupon.Size = new System.Drawing.Size(135, 34);
      this.btn_emitir_cupon.TabIndex = 432;
      this.btn_emitir_cupon.Text = "Emitir Cupon";
      this.btn_emitir_cupon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.btn_emitir_cupon.Textcolor = System.Drawing.Color.White;
      this.btn_emitir_cupon.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_emitir_cupon.Visible = false;
      this.btn_emitir_cupon.Click += new System.EventHandler(this.btn_emitir_cupon_Click);
      // 
      // picbox_socio
      // 
      this.picbox_socio.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.picbox_socio.Location = new System.Drawing.Point(9, 193);
      this.picbox_socio.Name = "picbox_socio";
      this.picbox_socio.Size = new System.Drawing.Size(110, 110);
      this.picbox_socio.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.picbox_socio.TabIndex = 180;
      this.picbox_socio.TabStop = false;
      // 
      // frm_eventos
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.WhiteSmoke;
      this.ClientSize = new System.Drawing.Size(871, 375);
      this.Controls.Add(this.btn_reimprimir);
      this.Controls.Add(this.txt_total_exepciones);
      this.Controls.Add(this.bunifuCustomLabel10);
      this.Controls.Add(this.txt_total_cupones);
      this.Controls.Add(this.bunifuCustomLabel9);
      this.Controls.Add(this.btn_emitir_cupon);
      this.Controls.Add(this.picbox_socio);
      this.Controls.Add(this.dgv_titu_benef);
      this.Controls.Add(this.bunifuCustomLabel12);
      this.Controls.Add(this.cbx_eventos);
      this.Controls.Add(this.panel2);
      this.Controls.Add(this.gbx_exepciones);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.Name = "frm_eventos";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "frm_eventos";
      this.Load += new System.EventHandler(this.frm_eventos_Load);
      this.panel2.ResumeLayout(false);
      this.panel2.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.btn_minimizar)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.btn_cerrar)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.dgv_titu_benef)).EndInit();
      this.gbx_exepciones.ResumeLayout(false);
      this.gbx_exepciones.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.picbox_socio)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel1;
    private System.Windows.Forms.Panel panel2;
    private Bunifu.Framework.UI.BunifuImageButton btn_cerrar;
    private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel12;
    private System.Windows.Forms.ComboBox cbx_eventos;
    private System.Windows.Forms.DataGridView dgv_titu_benef;
    private System.Windows.Forms.PictureBox picbox_socio;
    private System.Windows.Forms.GroupBox gbx_exepciones;
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
    private Bunifu.Framework.UI.BunifuFlatButton btn_cargar_exepcion;
    private Bunifu.Framework.UI.BunifuFlatButton btn_emitir_cupon;
    private System.Windows.Forms.MaskedTextBox msk_fecha_nac;
    private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel8;
    private System.Windows.Forms.ComboBox cbx_sexo;
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
    private Bunifu.Framework.UI.BunifuImageButton btn_minimizar;
    public System.Windows.Forms.TextBox txt_total_cupones;
    private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel9;
    public System.Windows.Forms.TextBox txt_total_exepciones;
    private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel10;
    private Bunifu.Framework.UI.BunifuFlatButton btn_reimprimir;
  }
}