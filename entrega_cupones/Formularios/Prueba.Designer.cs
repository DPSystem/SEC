namespace entrega_cupones.Formularios
{
  partial class frm_Prueba
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Prueba));
      this.btn_Imprimir = new System.Windows.Forms.Button();
      this.btn_ComprimirImagen = new System.Windows.Forms.Button();
      this.dgv1 = new System.Windows.Forms.DataGridView();
      this.btn_ListarImagenes = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.lbl_TotalFotos = new System.Windows.Forms.Label();
      this.btn_GuardarFoto = new System.Windows.Forms.Button();
      this.btn_GuardarFotoBenef = new System.Windows.Forms.Button();
      this.btn_CopiarFotos = new System.Windows.Forms.Button();
      this.label2 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.lbl_Inicio = new System.Windows.Forms.Label();
      this.lbl_Fin = new System.Windows.Forms.Label();
      this.btn_Join = new System.Windows.Forms.Button();
      this.label4 = new System.Windows.Forms.Label();
      this.lbl_CantidadDeActas = new System.Windows.Forms.Label();
      this.label10 = new System.Windows.Forms.Label();
      this.label9 = new System.Windows.Forms.Label();
      this.msk_Hasta = new System.Windows.Forms.MaskedTextBox();
      this.msk_Desde = new System.Windows.Forms.MaskedTextBox();
      this.btn_CalcularInteres = new System.Windows.Forms.Button();
      this.txt_importe = new System.Windows.Forms.TextBox();
      this.label5 = new System.Windows.Forms.Label();
      this.label6 = new System.Windows.Forms.Label();
      this.txt_interes = new System.Windows.Forms.TextBox();
      this.txt_Sueldo = new System.Windows.Forms.TextBox();
      this.label7 = new System.Windows.Forms.Label();
      this.btn_empleados = new System.Windows.Forms.Button();
      this.cbx_Sueldo = new System.Windows.Forms.ComboBox();
      this.cbx_Jornada = new System.Windows.Forms.ComboBox();
      this.label8 = new System.Windows.Forms.Label();
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.toolStripSplitButton2 = new System.Windows.Forms.ToolStripSplitButton();
      this.diegoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.aleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
      this.ordenesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripSplitButton();
      this.menu1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripButton1 = new System.Windows.Forms.ToolStripSplitButton();
      this.toolStripSplitButton4 = new System.Windows.Forms.ToolStripSplitButton();
      this.toolStripSplitButton3 = new System.Windows.Forms.ToolStripSplitButton();
      this.toolStripButton2 = new System.Windows.Forms.ToolStripSplitButton();
      this.dgv2 = new System.Windows.Forms.DataGridView();
      this.label12 = new System.Windows.Forms.Label();
      this.txt_CUIT = new System.Windows.Forms.TextBox();
      this.chk_PorEmpresa = new System.Windows.Forms.CheckBox();
      this.txt_RazonSocial = new System.Windows.Forms.TextBox();
      this.btn_BuscarEmpresa = new System.Windows.Forms.Button();
      this.label11 = new System.Windows.Forms.Label();
      this.btn_VerMenuNuevo = new System.Windows.Forms.Button();
      ((System.ComponentModel.ISupportInitialize)(this.dgv1)).BeginInit();
      this.toolStrip1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dgv2)).BeginInit();
      this.SuspendLayout();
      // 
      // btn_Imprimir
      // 
      this.btn_Imprimir.Location = new System.Drawing.Point(288, 73);
      this.btn_Imprimir.Name = "btn_Imprimir";
      this.btn_Imprimir.Size = new System.Drawing.Size(162, 42);
      this.btn_Imprimir.TabIndex = 0;
      this.btn_Imprimir.Text = "Imprimir Directo a Impresora";
      this.btn_Imprimir.UseVisualStyleBackColor = true;
      this.btn_Imprimir.Click += new System.EventHandler(this.btn_Imprimir_Click);
      // 
      // btn_ComprimirImagen
      // 
      this.btn_ComprimirImagen.Location = new System.Drawing.Point(692, 12);
      this.btn_ComprimirImagen.Name = "btn_ComprimirImagen";
      this.btn_ComprimirImagen.Size = new System.Drawing.Size(112, 42);
      this.btn_ComprimirImagen.TabIndex = 1;
      this.btn_ComprimirImagen.Text = "Comprimir Imagen";
      this.btn_ComprimirImagen.UseVisualStyleBackColor = true;
      this.btn_ComprimirImagen.Visible = false;
      this.btn_ComprimirImagen.Click += new System.EventHandler(this.btn_ComprimirImagen_Click);
      // 
      // dgv1
      // 
      this.dgv1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgv1.Location = new System.Drawing.Point(241, 199);
      this.dgv1.Name = "dgv1";
      this.dgv1.Size = new System.Drawing.Size(745, 149);
      this.dgv1.TabIndex = 2;
      this.dgv1.SelectionChanged += new System.EventHandler(this.dgv1_SelectionChanged);
      // 
      // btn_ListarImagenes
      // 
      this.btn_ListarImagenes.Location = new System.Drawing.Point(338, 12);
      this.btn_ListarImagenes.Name = "btn_ListarImagenes";
      this.btn_ListarImagenes.Size = new System.Drawing.Size(112, 42);
      this.btn_ListarImagenes.TabIndex = 3;
      this.btn_ListarImagenes.Text = "Listar Imagenes";
      this.btn_ListarImagenes.UseVisualStyleBackColor = true;
      this.btn_ListarImagenes.Visible = false;
      this.btn_ListarImagenes.Click += new System.EventHandler(this.btn_ListarImagenes_Click);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(335, 57);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(63, 13);
      this.label1.TabIndex = 4;
      this.label1.Text = "Total Fotos:";
      this.label1.Visible = false;
      // 
      // lbl_TotalFotos
      // 
      this.lbl_TotalFotos.AutoSize = true;
      this.lbl_TotalFotos.Location = new System.Drawing.Point(394, 57);
      this.lbl_TotalFotos.Name = "lbl_TotalFotos";
      this.lbl_TotalFotos.Size = new System.Drawing.Size(13, 13);
      this.lbl_TotalFotos.TabIndex = 5;
      this.lbl_TotalFotos.Text = "0";
      this.lbl_TotalFotos.Visible = false;
      // 
      // btn_GuardarFoto
      // 
      this.btn_GuardarFoto.Location = new System.Drawing.Point(456, 12);
      this.btn_GuardarFoto.Name = "btn_GuardarFoto";
      this.btn_GuardarFoto.Size = new System.Drawing.Size(112, 42);
      this.btn_GuardarFoto.TabIndex = 6;
      this.btn_GuardarFoto.Text = "Guardar Fotos Titular";
      this.btn_GuardarFoto.UseVisualStyleBackColor = true;
      this.btn_GuardarFoto.Visible = false;
      this.btn_GuardarFoto.Click += new System.EventHandler(this.btn_GuardarFoto_Click);
      // 
      // btn_GuardarFotoBenef
      // 
      this.btn_GuardarFotoBenef.Location = new System.Drawing.Point(574, 12);
      this.btn_GuardarFotoBenef.Name = "btn_GuardarFotoBenef";
      this.btn_GuardarFotoBenef.Size = new System.Drawing.Size(112, 42);
      this.btn_GuardarFotoBenef.TabIndex = 7;
      this.btn_GuardarFotoBenef.Text = "Guardar Fotos Beneficiario";
      this.btn_GuardarFotoBenef.UseVisualStyleBackColor = true;
      this.btn_GuardarFotoBenef.Visible = false;
      this.btn_GuardarFotoBenef.Click += new System.EventHandler(this.btn_GuardarFotoBenef_Click);
      // 
      // btn_CopiarFotos
      // 
      this.btn_CopiarFotos.Location = new System.Drawing.Point(456, 60);
      this.btn_CopiarFotos.Name = "btn_CopiarFotos";
      this.btn_CopiarFotos.Size = new System.Drawing.Size(112, 42);
      this.btn_CopiarFotos.TabIndex = 8;
      this.btn_CopiarFotos.Text = "Copiar fotos a Tabla fotos";
      this.btn_CopiarFotos.UseVisualStyleBackColor = true;
      this.btn_CopiarFotos.Visible = false;
      this.btn_CopiarFotos.Click += new System.EventHandler(this.btn_CopiarFotos_Click);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(570, 60);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(35, 13);
      this.label2.TabIndex = 9;
      this.label2.Text = "Inicio:";
      this.label2.Visible = false;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(581, 89);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(24, 13);
      this.label3.TabIndex = 10;
      this.label3.Text = "Fin;";
      this.label3.Visible = false;
      // 
      // lbl_Inicio
      // 
      this.lbl_Inicio.AutoSize = true;
      this.lbl_Inicio.Location = new System.Drawing.Point(608, 60);
      this.lbl_Inicio.Name = "lbl_Inicio";
      this.lbl_Inicio.Size = new System.Drawing.Size(13, 13);
      this.lbl_Inicio.TabIndex = 11;
      this.lbl_Inicio.Text = "0";
      this.lbl_Inicio.Visible = false;
      // 
      // lbl_Fin
      // 
      this.lbl_Fin.AutoSize = true;
      this.lbl_Fin.Location = new System.Drawing.Point(608, 89);
      this.lbl_Fin.Name = "lbl_Fin";
      this.lbl_Fin.Size = new System.Drawing.Size(13, 13);
      this.lbl_Fin.TabIndex = 12;
      this.lbl_Fin.Text = "0";
      this.lbl_Fin.Visible = false;
      // 
      // btn_Join
      // 
      this.btn_Join.Location = new System.Drawing.Point(229, 376);
      this.btn_Join.Name = "btn_Join";
      this.btn_Join.Size = new System.Drawing.Size(103, 42);
      this.btn_Join.TabIndex = 13;
      this.btn_Join.Text = "Join Actas con Cobradores";
      this.btn_Join.UseVisualStyleBackColor = true;
      this.btn_Join.Visible = false;
      this.btn_Join.Click += new System.EventHandler(this.btn_Join_Click);
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(769, 354);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(97, 13);
      this.label4.TabIndex = 14;
      this.label4.Text = "Cantidad de Actas:";
      // 
      // lbl_CantidadDeActas
      // 
      this.lbl_CantidadDeActas.AutoSize = true;
      this.lbl_CantidadDeActas.Location = new System.Drawing.Point(908, 354);
      this.lbl_CantidadDeActas.Name = "lbl_CantidadDeActas";
      this.lbl_CantidadDeActas.Size = new System.Drawing.Size(35, 13);
      this.lbl_CantidadDeActas.TabIndex = 15;
      this.lbl_CantidadDeActas.Text = "label5";
      // 
      // label10
      // 
      this.label10.AutoSize = true;
      this.label10.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label10.Location = new System.Drawing.Point(642, 122);
      this.label10.Name = "label10";
      this.label10.Size = new System.Drawing.Size(49, 17);
      this.label10.TabIndex = 533;
      this.label10.Text = "Hasta:";
      // 
      // label9
      // 
      this.label9.AutoSize = true;
      this.label9.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label9.Location = new System.Drawing.Point(511, 121);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(52, 17);
      this.label9.TabIndex = 532;
      this.label9.Text = "Desde:";
      this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // msk_Hasta
      // 
      this.msk_Hasta.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.msk_Hasta.Location = new System.Drawing.Point(697, 122);
      this.msk_Hasta.Mask = "00/0000";
      this.msk_Hasta.Name = "msk_Hasta";
      this.msk_Hasta.Size = new System.Drawing.Size(61, 23);
      this.msk_Hasta.TabIndex = 531;
      this.msk_Hasta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // msk_Desde
      // 
      this.msk_Desde.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.msk_Desde.Location = new System.Drawing.Point(567, 121);
      this.msk_Desde.Mask = "00/0000";
      this.msk_Desde.Name = "msk_Desde";
      this.msk_Desde.Size = new System.Drawing.Size(62, 23);
      this.msk_Desde.TabIndex = 530;
      this.msk_Desde.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.msk_Desde.ValidatingType = typeof(System.DateTime);
      // 
      // btn_CalcularInteres
      // 
      this.btn_CalcularInteres.Location = new System.Drawing.Point(218, 488);
      this.btn_CalcularInteres.Name = "btn_CalcularInteres";
      this.btn_CalcularInteres.Size = new System.Drawing.Size(84, 39);
      this.btn_CalcularInteres.TabIndex = 534;
      this.btn_CalcularInteres.Text = "Intereses";
      this.btn_CalcularInteres.UseVisualStyleBackColor = true;
      this.btn_CalcularInteres.Visible = false;
      this.btn_CalcularInteres.Click += new System.EventHandler(this.btn_CalcularInteres_Click);
      // 
      // txt_importe
      // 
      this.txt_importe.Location = new System.Drawing.Point(261, 424);
      this.txt_importe.Name = "txt_importe";
      this.txt_importe.Size = new System.Drawing.Size(72, 20);
      this.txt_importe.TabIndex = 535;
      this.txt_importe.Visible = false;
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label5.Location = new System.Drawing.Point(196, 425);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(63, 17);
      this.label5.TabIndex = 536;
      this.label5.Text = "Importe:";
      this.label5.Visible = false;
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label6.Location = new System.Drawing.Point(196, 452);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(57, 17);
      this.label6.TabIndex = 538;
      this.label6.Text = "Interes: ";
      this.label6.Visible = false;
      // 
      // txt_interes
      // 
      this.txt_interes.Location = new System.Drawing.Point(261, 451);
      this.txt_interes.Name = "txt_interes";
      this.txt_interes.Size = new System.Drawing.Size(72, 20);
      this.txt_interes.TabIndex = 537;
      this.txt_interes.Visible = false;
      // 
      // txt_Sueldo
      // 
      this.txt_Sueldo.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txt_Sueldo.Location = new System.Drawing.Point(411, 122);
      this.txt_Sueldo.Name = "txt_Sueldo";
      this.txt_Sueldo.Size = new System.Drawing.Size(94, 23);
      this.txt_Sueldo.TabIndex = 540;
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label7.Location = new System.Drawing.Point(211, 124);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(55, 17);
      this.label7.TabIndex = 541;
      this.label7.Text = "Sueldo:";
      // 
      // btn_empleados
      // 
      this.btn_empleados.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_empleados.Location = new System.Drawing.Point(937, 117);
      this.btn_empleados.Name = "btn_empleados";
      this.btn_empleados.Size = new System.Drawing.Size(91, 27);
      this.btn_empleados.TabIndex = 542;
      this.btn_empleados.Text = "Listar";
      this.btn_empleados.UseVisualStyleBackColor = true;
      this.btn_empleados.Click += new System.EventHandler(this.btn_empleados_Click);
      // 
      // cbx_Sueldo
      // 
      this.cbx_Sueldo.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cbx_Sueldo.FormattingEnabled = true;
      this.cbx_Sueldo.Items.AddRange(new object[] {
            " TODOS",
            "DESDE",
            "HASTA",
            "IGUAL A"});
      this.cbx_Sueldo.Location = new System.Drawing.Point(267, 121);
      this.cbx_Sueldo.Name = "cbx_Sueldo";
      this.cbx_Sueldo.Size = new System.Drawing.Size(121, 25);
      this.cbx_Sueldo.TabIndex = 543;
      // 
      // cbx_Jornada
      // 
      this.cbx_Jornada.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cbx_Jornada.FormattingEnabled = true;
      this.cbx_Jornada.Items.AddRange(new object[] {
            " TODOS",
            "PARCIAL",
            "COMPLETA"});
      this.cbx_Jornada.Location = new System.Drawing.Point(829, 119);
      this.cbx_Jornada.Name = "cbx_Jornada";
      this.cbx_Jornada.Size = new System.Drawing.Size(102, 25);
      this.cbx_Jornada.TabIndex = 545;
      // 
      // label8
      // 
      this.label8.AutoSize = true;
      this.label8.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label8.Location = new System.Drawing.Point(765, 122);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(66, 17);
      this.label8.TabIndex = 544;
      this.label8.Text = "Jornada:";
      // 
      // toolStrip1
      // 
      this.toolStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
      this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Left;
      this.toolStrip1.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.toolStrip1.ImageScalingSize = new System.Drawing.Size(50, 50);
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSplitButton2,
            this.toolStripSplitButton1,
            this.toolStripDropDownButton1,
            this.toolStripButton1,
            this.toolStripSplitButton4,
            this.toolStripSplitButton3,
            this.toolStripButton2});
      this.toolStrip1.Location = new System.Drawing.Point(0, 0);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
      this.toolStrip1.Size = new System.Drawing.Size(179, 670);
      this.toolStrip1.TabIndex = 546;
      this.toolStrip1.Text = "toolStrip1";
      // 
      // toolStripSplitButton2
      // 
      this.toolStripSplitButton2.DropDownButtonWidth = 25;
      this.toolStripSplitButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.diegoToolStripMenuItem,
            this.aleToolStripMenuItem});
      this.toolStripSplitButton2.Image = global::entrega_cupones.Properties.Resources.dollars_bag__3_;
      this.toolStripSplitButton2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.toolStripSplitButton2.ImageTransparentColor = System.Drawing.SystemColors.MenuHighlight;
      this.toolStripSplitButton2.Name = "toolStripSplitButton2";
      this.toolStripSplitButton2.Size = new System.Drawing.Size(176, 54);
      this.toolStripSplitButton2.Text = "     Recepcion";
      this.toolStripSplitButton2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // diegoToolStripMenuItem
      // 
      this.diegoToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
      this.diegoToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
      this.diegoToolStripMenuItem.Name = "diegoToolStripMenuItem";
      this.diegoToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
      this.diegoToolStripMenuItem.Text = "Diego";
      // 
      // aleToolStripMenuItem
      // 
      this.aleToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
      this.aleToolStripMenuItem.Name = "aleToolStripMenuItem";
      this.aleToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
      this.aleToolStripMenuItem.Text = "Ale";
      // 
      // toolStripSplitButton1
      // 
      this.toolStripSplitButton1.DropDownButtonWidth = 25;
      this.toolStripSplitButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ordenesToolStripMenuItem});
      this.toolStripSplitButton1.Image = global::entrega_cupones.Properties.Resources.football__4_;
      this.toolStripSplitButton1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripSplitButton1.Name = "toolStripSplitButton1";
      this.toolStripSplitButton1.Size = new System.Drawing.Size(176, 54);
      this.toolStripSplitButton1.Text = "     Cobranzas";
      this.toolStripSplitButton1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // ordenesToolStripMenuItem
      // 
      this.ordenesToolStripMenuItem.Name = "ordenesToolStripMenuItem";
      this.ordenesToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
      this.ordenesToolStripMenuItem.Text = "Ordenes";
      // 
      // toolStripDropDownButton1
      // 
      this.toolStripDropDownButton1.DropDownButtonWidth = 25;
      this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu1ToolStripMenuItem});
      this.toolStripDropDownButton1.Image = global::entrega_cupones.Properties.Resources.triumph_soccer_ball_symbol__2_;
      this.toolStripDropDownButton1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
      this.toolStripDropDownButton1.Size = new System.Drawing.Size(176, 54);
      this.toolStripDropDownButton1.Text = "     Inspectores";
      this.toolStripDropDownButton1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // menu1ToolStripMenuItem
      // 
      this.menu1ToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
      this.menu1ToolStripMenuItem.Checked = true;
      this.menu1ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
      this.menu1ToolStripMenuItem.Name = "menu1ToolStripMenuItem";
      this.menu1ToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
      this.menu1ToolStripMenuItem.Text = "Menu 1";
      // 
      // toolStripButton1
      // 
      this.toolStripButton1.DropDownButtonWidth = 25;
      this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
      this.toolStripButton1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton1.Name = "toolStripButton1";
      this.toolStripButton1.Size = new System.Drawing.Size(176, 54);
      this.toolStripButton1.Text = "     Tesoreria";
      this.toolStripButton1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // toolStripSplitButton4
      // 
      this.toolStripSplitButton4.DropDownButtonWidth = 25;
      this.toolStripSplitButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButton4.Image")));
      this.toolStripSplitButton4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.toolStripSplitButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripSplitButton4.Name = "toolStripSplitButton4";
      this.toolStripSplitButton4.Size = new System.Drawing.Size(176, 54);
      this.toolStripSplitButton4.Text = "     Empadrona";
      this.toolStripSplitButton4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // toolStripSplitButton3
      // 
      this.toolStripSplitButton3.DropDownButtonWidth = 25;
      this.toolStripSplitButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButton3.Image")));
      this.toolStripSplitButton3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.toolStripSplitButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripSplitButton3.Name = "toolStripSplitButton3";
      this.toolStripSplitButton3.Size = new System.Drawing.Size(176, 54);
      this.toolStripSplitButton3.Text = "     Igiene y Seg.";
      this.toolStripSplitButton3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // toolStripButton2
      // 
      this.toolStripButton2.DropDownButtonWidth = 25;
      this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
      this.toolStripButton2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButton2.Name = "toolStripButton2";
      this.toolStripButton2.Size = new System.Drawing.Size(176, 54);
      this.toolStripButton2.Text = "     Gremial";
      this.toolStripButton2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // dgv2
      // 
      this.dgv2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgv2.Location = new System.Drawing.Point(338, 376);
      this.dgv2.Name = "dgv2";
      this.dgv2.Size = new System.Drawing.Size(737, 272);
      this.dgv2.TabIndex = 547;
      // 
      // label12
      // 
      this.label12.AutoSize = true;
      this.label12.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label12.Location = new System.Drawing.Point(727, 173);
      this.label12.Name = "label12";
      this.label12.Size = new System.Drawing.Size(39, 17);
      this.label12.TabIndex = 552;
      this.label12.Text = "CUIT:";
      // 
      // txt_CUIT
      // 
      this.txt_CUIT.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txt_CUIT.Location = new System.Drawing.Point(772, 170);
      this.txt_CUIT.Name = "txt_CUIT";
      this.txt_CUIT.Size = new System.Drawing.Size(104, 23);
      this.txt_CUIT.TabIndex = 551;
      // 
      // chk_PorEmpresa
      // 
      this.chk_PorEmpresa.AutoSize = true;
      this.chk_PorEmpresa.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_PorEmpresa.Location = new System.Drawing.Point(241, 172);
      this.chk_PorEmpresa.Name = "chk_PorEmpresa";
      this.chk_PorEmpresa.Size = new System.Drawing.Size(107, 21);
      this.chk_PorEmpresa.TabIndex = 553;
      this.chk_PorEmpresa.Text = "Por Empresa";
      this.chk_PorEmpresa.UseVisualStyleBackColor = true;
      this.chk_PorEmpresa.CheckedChanged += new System.EventHandler(this.chk_PorEmpresa_CheckedChanged);
      // 
      // txt_RazonSocial
      // 
      this.txt_RazonSocial.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txt_RazonSocial.Location = new System.Drawing.Point(486, 170);
      this.txt_RazonSocial.Name = "txt_RazonSocial";
      this.txt_RazonSocial.Size = new System.Drawing.Size(235, 23);
      this.txt_RazonSocial.TabIndex = 554;
      // 
      // btn_BuscarEmpresa
      // 
      this.btn_BuscarEmpresa.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_BuscarEmpresa.Location = new System.Drawing.Point(354, 168);
      this.btn_BuscarEmpresa.Name = "btn_BuscarEmpresa";
      this.btn_BuscarEmpresa.Size = new System.Drawing.Size(61, 27);
      this.btn_BuscarEmpresa.TabIndex = 555;
      this.btn_BuscarEmpresa.Text = "Buscar";
      this.btn_BuscarEmpresa.UseVisualStyleBackColor = true;
      this.btn_BuscarEmpresa.Click += new System.EventHandler(this.btn_BuscarEmpresa_Click);
      // 
      // label11
      // 
      this.label11.AutoSize = true;
      this.label11.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label11.Location = new System.Drawing.Point(421, 172);
      this.label11.Name = "label11";
      this.label11.Size = new System.Drawing.Size(62, 17);
      this.label11.TabIndex = 556;
      this.label11.Text = "Raz Soc:";
      // 
      // btn_VerMenuNuevo
      // 
      this.btn_VerMenuNuevo.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_VerMenuNuevo.Location = new System.Drawing.Point(810, 12);
      this.btn_VerMenuNuevo.Name = "btn_VerMenuNuevo";
      this.btn_VerMenuNuevo.Size = new System.Drawing.Size(91, 42);
      this.btn_VerMenuNuevo.TabIndex = 557;
      this.btn_VerMenuNuevo.Text = "Ver Menu";
      this.btn_VerMenuNuevo.UseVisualStyleBackColor = true;
      this.btn_VerMenuNuevo.Click += new System.EventHandler(this.btn_VerMenuNuevo_Click);
      // 
      // frm_Prueba
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.SystemColors.ButtonFace;
      this.ClientSize = new System.Drawing.Size(1109, 670);
      this.Controls.Add(this.btn_VerMenuNuevo);
      this.Controls.Add(this.label11);
      this.Controls.Add(this.btn_BuscarEmpresa);
      this.Controls.Add(this.txt_RazonSocial);
      this.Controls.Add(this.chk_PorEmpresa);
      this.Controls.Add(this.label12);
      this.Controls.Add(this.txt_CUIT);
      this.Controls.Add(this.dgv2);
      this.Controls.Add(this.toolStrip1);
      this.Controls.Add(this.cbx_Jornada);
      this.Controls.Add(this.label8);
      this.Controls.Add(this.cbx_Sueldo);
      this.Controls.Add(this.btn_empleados);
      this.Controls.Add(this.label7);
      this.Controls.Add(this.txt_Sueldo);
      this.Controls.Add(this.label6);
      this.Controls.Add(this.txt_interes);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.txt_importe);
      this.Controls.Add(this.btn_CalcularInteres);
      this.Controls.Add(this.label10);
      this.Controls.Add(this.label9);
      this.Controls.Add(this.msk_Hasta);
      this.Controls.Add(this.msk_Desde);
      this.Controls.Add(this.lbl_CantidadDeActas);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.btn_Join);
      this.Controls.Add(this.lbl_Fin);
      this.Controls.Add(this.lbl_Inicio);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.btn_CopiarFotos);
      this.Controls.Add(this.btn_GuardarFotoBenef);
      this.Controls.Add(this.btn_GuardarFoto);
      this.Controls.Add(this.lbl_TotalFotos);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.btn_ListarImagenes);
      this.Controls.Add(this.dgv1);
      this.Controls.Add(this.btn_ComprimirImagen);
      this.Controls.Add(this.btn_Imprimir);
      this.Name = "frm_Prueba";
      this.Text = "Prueba";
      this.Load += new System.EventHandler(this.frm_Prueba_Load);
      ((System.ComponentModel.ISupportInitialize)(this.dgv1)).EndInit();
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dgv2)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btn_Imprimir;
    private System.Windows.Forms.Button btn_ComprimirImagen;
    private System.Windows.Forms.DataGridView dgv1;
    private System.Windows.Forms.Button btn_ListarImagenes;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label lbl_TotalFotos;
    private System.Windows.Forms.Button btn_GuardarFoto;
    private System.Windows.Forms.Button btn_GuardarFotoBenef;
    private System.Windows.Forms.Button btn_CopiarFotos;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label lbl_Inicio;
    private System.Windows.Forms.Label lbl_Fin;
    private System.Windows.Forms.Button btn_Join;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label lbl_CantidadDeActas;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.MaskedTextBox msk_Hasta;
    private System.Windows.Forms.MaskedTextBox msk_Desde;
    private System.Windows.Forms.Button btn_CalcularInteres;
    private System.Windows.Forms.TextBox txt_importe;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.TextBox txt_interes;
    private System.Windows.Forms.TextBox txt_Sueldo;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.Button btn_empleados;
    private System.Windows.Forms.ComboBox cbx_Sueldo;
    private System.Windows.Forms.ComboBox cbx_Jornada;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton1;
    private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton2;
    private System.Windows.Forms.ToolStripSplitButton toolStripDropDownButton1;
    private System.Windows.Forms.ToolStripMenuItem menu1ToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem diegoToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem aleToolStripMenuItem;
    private System.Windows.Forms.DataGridView dgv2;
    private System.Windows.Forms.Label label12;
    private System.Windows.Forms.TextBox txt_CUIT;
    private System.Windows.Forms.CheckBox chk_PorEmpresa;
    private System.Windows.Forms.TextBox txt_RazonSocial;
    private System.Windows.Forms.Button btn_BuscarEmpresa;
    private System.Windows.Forms.Label label11;
    private System.Windows.Forms.ToolStripMenuItem ordenesToolStripMenuItem;
    private System.Windows.Forms.ToolStripSplitButton toolStripButton1;
    private System.Windows.Forms.ToolStripSplitButton toolStripButton2;
    private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton3;
    private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton4;
    private System.Windows.Forms.Button btn_VerMenuNuevo;
  }
}