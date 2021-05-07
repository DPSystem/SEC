namespace entrega_cupones
{
    partial class frm_ingreso_poli
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
            this.bunifuElipse1 = new Bunifu.Framework.UI.BunifuElipse(this.components);
            this.bunifuCustomLabel1 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btn_cerrar = new Bunifu.Framework.UI.BunifuImageButton();
            this.btn_cerrar_actas = new Bunifu.Framework.UI.BunifuImageButton();
            this.txt_buscar = new System.Windows.Forms.TextBox();
            this.bunifuCustomLabel12 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.bunifuSeparator1 = new Bunifu.Framework.UI.BunifuSeparator();
            this.lbl_sexo = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.bunifuCustomLabel20 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.lbl_empresa = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.bunifuCustomLabel10 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.lbl_dni = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.lbl_nombre = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.bunifuCustomLabel6 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.lbl_nro_socio = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.bunifuCustomLabel3 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.bunifuCustomLabel8 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.lbl_apto = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.bunifuSeparator2 = new Bunifu.Framework.UI.BunifuSeparator();
            this.picbox_socio = new System.Windows.Forms.PictureBox();
            this.btn_buscar = new Bunifu.Framework.UI.BunifuFlatButton();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btn_cerrar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_cerrar_actas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbox_socio)).BeginInit();
            this.SuspendLayout();
            // 
            // bunifuElipse1
            // 
            this.bunifuElipse1.ElipseRadius = 5;
            this.bunifuElipse1.TargetControl = this;
            // 
            // bunifuCustomLabel1
            // 
            this.bunifuCustomLabel1.AutoSize = true;
            this.bunifuCustomLabel1.Font = new System.Drawing.Font("Century Gothic", 18.75F, System.Drawing.FontStyle.Bold);
            this.bunifuCustomLabel1.ForeColor = System.Drawing.Color.White;
            this.bunifuCustomLabel1.Location = new System.Drawing.Point(9, 11);
            this.bunifuCustomLabel1.Name = "bunifuCustomLabel1";
            this.bunifuCustomLabel1.Size = new System.Drawing.Size(237, 29);
            this.bunifuCustomLabel1.TabIndex = 149;
            this.bunifuCustomLabel1.Text = "Control de Ingreso";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.panel2.Controls.Add(this.btn_cerrar);
            this.panel2.Controls.Add(this.bunifuCustomLabel1);
            this.panel2.Controls.Add(this.btn_cerrar_actas);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(643, 51);
            this.panel2.TabIndex = 4;
            // 
            // btn_cerrar
            // 
            this.btn_cerrar.BackColor = System.Drawing.Color.Transparent;
            this.btn_cerrar.Image = global::entrega_cupones.Properties.Resources.cross_close_or_delete_circular_interface_button_symbol;
            this.btn_cerrar.ImageActive = null;
            this.btn_cerrar.Location = new System.Drawing.Point(593, 8);
            this.btn_cerrar.Name = "btn_cerrar";
            this.btn_cerrar.Size = new System.Drawing.Size(43, 36);
            this.btn_cerrar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btn_cerrar.TabIndex = 5;
            this.btn_cerrar.TabStop = false;
            this.btn_cerrar.Zoom = 10;
            this.btn_cerrar.Click += new System.EventHandler(this.btn_cerrar_Click);
            // 
            // btn_cerrar_actas
            // 
            this.btn_cerrar_actas.BackColor = System.Drawing.Color.Transparent;
            this.btn_cerrar_actas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_cerrar_actas.Image = global::entrega_cupones.Properties.Resources.cross_close_or_delete_circular_interface_button_symbol;
            this.btn_cerrar_actas.ImageActive = null;
            this.btn_cerrar_actas.Location = new System.Drawing.Point(1308, 10);
            this.btn_cerrar_actas.Name = "btn_cerrar_actas";
            this.btn_cerrar_actas.Size = new System.Drawing.Size(31, 30);
            this.btn_cerrar_actas.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btn_cerrar_actas.TabIndex = 148;
            this.btn_cerrar_actas.TabStop = false;
            this.btn_cerrar_actas.Zoom = 10;
            // 
            // txt_buscar
            // 
            this.txt_buscar.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txt_buscar.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txt_buscar.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_buscar.Location = new System.Drawing.Point(14, 88);
            this.txt_buscar.MaxLength = 50;
            this.txt_buscar.Name = "txt_buscar";
            this.txt_buscar.ShortcutsEnabled = false;
            this.txt_buscar.Size = new System.Drawing.Size(498, 22);
            this.txt_buscar.TabIndex = 365;
            this.txt_buscar.TextChanged += new System.EventHandler(this.txt_buscar_TextChanged);
            this.txt_buscar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_buscar_KeyDown);
            // 
            // bunifuCustomLabel12
            // 
            this.bunifuCustomLabel12.AutoSize = true;
            this.bunifuCustomLabel12.Font = new System.Drawing.Font("Century Gothic", 10F);
            this.bunifuCustomLabel12.ForeColor = System.Drawing.Color.Black;
            this.bunifuCustomLabel12.Location = new System.Drawing.Point(12, 66);
            this.bunifuCustomLabel12.Name = "bunifuCustomLabel12";
            this.bunifuCustomLabel12.Size = new System.Drawing.Size(253, 19);
            this.bunifuCustomLabel12.TabIndex = 364;
            this.bunifuCustomLabel12.Text = "Buscar por D.N.I. / Apellido Nombre ";
            // 
            // bunifuSeparator1
            // 
            this.bunifuSeparator1.BackColor = System.Drawing.Color.Transparent;
            this.bunifuSeparator1.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(105)))), ((int)(((byte)(105)))));
            this.bunifuSeparator1.LineThickness = 4;
            this.bunifuSeparator1.Location = new System.Drawing.Point(0, 121);
            this.bunifuSeparator1.Name = "bunifuSeparator1";
            this.bunifuSeparator1.Size = new System.Drawing.Size(643, 12);
            this.bunifuSeparator1.TabIndex = 369;
            this.bunifuSeparator1.Transparency = 255;
            this.bunifuSeparator1.Vertical = false;
            // 
            // lbl_sexo
            // 
            this.lbl_sexo.BackColor = System.Drawing.Color.White;
            this.lbl_sexo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_sexo.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_sexo.ForeColor = System.Drawing.Color.Black;
            this.lbl_sexo.Location = new System.Drawing.Point(577, 196);
            this.lbl_sexo.Name = "lbl_sexo";
            this.lbl_sexo.Size = new System.Drawing.Size(29, 19);
            this.lbl_sexo.TabIndex = 386;
            this.lbl_sexo.Text = "xxxxxx";
            // 
            // bunifuCustomLabel20
            // 
            this.bunifuCustomLabel20.AutoSize = true;
            this.bunifuCustomLabel20.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuCustomLabel20.ForeColor = System.Drawing.Color.Black;
            this.bunifuCustomLabel20.Location = new System.Drawing.Point(530, 197);
            this.bunifuCustomLabel20.Name = "bunifuCustomLabel20";
            this.bunifuCustomLabel20.Size = new System.Drawing.Size(41, 17);
            this.bunifuCustomLabel20.TabIndex = 385;
            this.bunifuCustomLabel20.Text = "Sexo:";
            // 
            // lbl_empresa
            // 
            this.lbl_empresa.BackColor = System.Drawing.Color.White;
            this.lbl_empresa.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_empresa.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_empresa.ForeColor = System.Drawing.Color.Black;
            this.lbl_empresa.Location = new System.Drawing.Point(347, 222);
            this.lbl_empresa.Name = "lbl_empresa";
            this.lbl_empresa.Size = new System.Drawing.Size(288, 19);
            this.lbl_empresa.TabIndex = 376;
            this.lbl_empresa.Text = "xxxxxx";
            // 
            // bunifuCustomLabel10
            // 
            this.bunifuCustomLabel10.AutoSize = true;
            this.bunifuCustomLabel10.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuCustomLabel10.ForeColor = System.Drawing.Color.Black;
            this.bunifuCustomLabel10.Location = new System.Drawing.Point(275, 228);
            this.bunifuCustomLabel10.Name = "bunifuCustomLabel10";
            this.bunifuCustomLabel10.Size = new System.Drawing.Size(67, 17);
            this.bunifuCustomLabel10.TabIndex = 375;
            this.bunifuCustomLabel10.Text = "Empresa:";
            // 
            // lbl_dni
            // 
            this.lbl_dni.BackColor = System.Drawing.Color.White;
            this.lbl_dni.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_dni.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_dni.ForeColor = System.Drawing.Color.Black;
            this.lbl_dni.Location = new System.Drawing.Point(347, 194);
            this.lbl_dni.Name = "lbl_dni";
            this.lbl_dni.Size = new System.Drawing.Size(92, 19);
            this.lbl_dni.TabIndex = 374;
            this.lbl_dni.Text = "xxxxxx";
            // 
            // lbl_nombre
            // 
            this.lbl_nombre.BackColor = System.Drawing.Color.White;
            this.lbl_nombre.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_nombre.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_nombre.ForeColor = System.Drawing.Color.Black;
            this.lbl_nombre.Location = new System.Drawing.Point(347, 166);
            this.lbl_nombre.Name = "lbl_nombre";
            this.lbl_nombre.Size = new System.Drawing.Size(288, 19);
            this.lbl_nombre.TabIndex = 373;
            this.lbl_nombre.Text = "xxxxxx";
            // 
            // bunifuCustomLabel6
            // 
            this.bunifuCustomLabel6.AutoSize = true;
            this.bunifuCustomLabel6.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuCustomLabel6.ForeColor = System.Drawing.Color.Black;
            this.bunifuCustomLabel6.Location = new System.Drawing.Point(277, 168);
            this.bunifuCustomLabel6.Name = "bunifuCustomLabel6";
            this.bunifuCustomLabel6.Size = new System.Drawing.Size(65, 17);
            this.bunifuCustomLabel6.TabIndex = 372;
            this.bunifuCustomLabel6.Text = "Nombre:";
            // 
            // lbl_nro_socio
            // 
            this.lbl_nro_socio.AutoSize = true;
            this.lbl_nro_socio.BackColor = System.Drawing.Color.White;
            this.lbl_nro_socio.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_nro_socio.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_nro_socio.ForeColor = System.Drawing.Color.Black;
            this.lbl_nro_socio.Location = new System.Drawing.Point(347, 139);
            this.lbl_nro_socio.Name = "lbl_nro_socio";
            this.lbl_nro_socio.Size = new System.Drawing.Size(46, 19);
            this.lbl_nro_socio.TabIndex = 371;
            this.lbl_nro_socio.Text = "xxxxxx";
            // 
            // bunifuCustomLabel3
            // 
            this.bunifuCustomLabel3.AutoSize = true;
            this.bunifuCustomLabel3.Enabled = false;
            this.bunifuCustomLabel3.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuCustomLabel3.ForeColor = System.Drawing.Color.Black;
            this.bunifuCustomLabel3.Location = new System.Drawing.Point(275, 139);
            this.bunifuCustomLabel3.Name = "bunifuCustomLabel3";
            this.bunifuCustomLabel3.Size = new System.Drawing.Size(66, 17);
            this.bunifuCustomLabel3.TabIndex = 370;
            this.bunifuCustomLabel3.Text = "Nº Socio:";
            // 
            // bunifuCustomLabel8
            // 
            this.bunifuCustomLabel8.AutoSize = true;
            this.bunifuCustomLabel8.BackColor = System.Drawing.SystemColors.Control;
            this.bunifuCustomLabel8.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuCustomLabel8.ForeColor = System.Drawing.Color.Black;
            this.bunifuCustomLabel8.Location = new System.Drawing.Point(295, 198);
            this.bunifuCustomLabel8.Name = "bunifuCustomLabel8";
            this.bunifuCustomLabel8.Size = new System.Drawing.Size(47, 17);
            this.bunifuCustomLabel8.TabIndex = 393;
            this.bunifuCustomLabel8.Text = "D.N.I.:";
            // 
            // lbl_apto
            // 
            this.lbl_apto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(62)))), ((int)(((byte)(70)))));
            this.lbl_apto.Font = new System.Drawing.Font("Stencil", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_apto.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lbl_apto.Location = new System.Drawing.Point(4, 344);
            this.lbl_apto.Name = "lbl_apto";
            this.lbl_apto.Size = new System.Drawing.Size(634, 67);
            this.lbl_apto.TabIndex = 394;
            this.lbl_apto.Text = "Socio no Apto";
            this.lbl_apto.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bunifuSeparator2
            // 
            this.bunifuSeparator2.BackColor = System.Drawing.Color.Transparent;
            this.bunifuSeparator2.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(105)))), ((int)(((byte)(105)))));
            this.bunifuSeparator2.LineThickness = 4;
            this.bunifuSeparator2.Location = new System.Drawing.Point(0, 333);
            this.bunifuSeparator2.Name = "bunifuSeparator2";
            this.bunifuSeparator2.Size = new System.Drawing.Size(642, 10);
            this.bunifuSeparator2.TabIndex = 395;
            this.bunifuSeparator2.Transparency = 255;
            this.bunifuSeparator2.Vertical = false;
            // 
            // picbox_socio
            // 
            this.picbox_socio.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picbox_socio.Image = global::entrega_cupones.Properties.Resources.imagen_no_disponible_PNG;
            this.picbox_socio.Location = new System.Drawing.Point(14, 139);
            this.picbox_socio.Name = "picbox_socio";
            this.picbox_socio.Size = new System.Drawing.Size(232, 188);
            this.picbox_socio.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picbox_socio.TabIndex = 367;
            this.picbox_socio.TabStop = false;
            // 
            // btn_buscar
            // 
            this.btn_buscar.Active = false;
            this.btn_buscar.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.btn_buscar.BackColor = System.Drawing.Color.Transparent;
            this.btn_buscar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_buscar.BorderRadius = 0;
            this.btn_buscar.ButtonText = "Buscar";
            this.btn_buscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_buscar.DisabledColor = System.Drawing.Color.Gray;
            this.btn_buscar.ForeColor = System.Drawing.Color.Black;
            this.btn_buscar.Iconcolor = System.Drawing.Color.Transparent;
            this.btn_buscar.Iconimage = global::entrega_cupones.Properties.Resources.searching_a_person__2_;
            this.btn_buscar.Iconimage_right = null;
            this.btn_buscar.Iconimage_right_Selected = null;
            this.btn_buscar.Iconimage_Selected = null;
            this.btn_buscar.IconMarginLeft = 0;
            this.btn_buscar.IconMarginRight = 0;
            this.btn_buscar.IconRightVisible = true;
            this.btn_buscar.IconRightZoom = 0D;
            this.btn_buscar.IconVisible = true;
            this.btn_buscar.IconZoom = 90D;
            this.btn_buscar.IsTab = false;
            this.btn_buscar.Location = new System.Drawing.Point(526, 66);
            this.btn_buscar.Name = "btn_buscar";
            this.btn_buscar.Normalcolor = System.Drawing.Color.Transparent;
            this.btn_buscar.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(129)))), ((int)(((byte)(77)))));
            this.btn_buscar.OnHoverTextColor = System.Drawing.Color.White;
            this.btn_buscar.selected = false;
            this.btn_buscar.Size = new System.Drawing.Size(111, 49);
            this.btn_buscar.TabIndex = 366;
            this.btn_buscar.Text = "Buscar";
            this.btn_buscar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_buscar.Textcolor = System.Drawing.Color.Black;
            this.btn_buscar.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_buscar.Click += new System.EventHandler(this.btn_buscar_Click);
            // 
            // frm_ingreso_poli
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(643, 420);
            this.Controls.Add(this.bunifuSeparator2);
            this.Controls.Add(this.lbl_apto);
            this.Controls.Add(this.bunifuCustomLabel8);
            this.Controls.Add(this.lbl_sexo);
            this.Controls.Add(this.bunifuCustomLabel20);
            this.Controls.Add(this.lbl_empresa);
            this.Controls.Add(this.bunifuCustomLabel10);
            this.Controls.Add(this.lbl_dni);
            this.Controls.Add(this.lbl_nombre);
            this.Controls.Add(this.bunifuCustomLabel6);
            this.Controls.Add(this.lbl_nro_socio);
            this.Controls.Add(this.bunifuCustomLabel3);
            this.Controls.Add(this.bunifuSeparator1);
            this.Controls.Add(this.picbox_socio);
            this.Controls.Add(this.btn_buscar);
            this.Controls.Add(this.txt_buscar);
            this.Controls.Add(this.bunifuCustomLabel12);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frm_ingreso_poli";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frm_ingreso_poli";
            this.Load += new System.EventHandler(this.frm_ingreso_poli_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btn_cerrar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_cerrar_actas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbox_socio)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Bunifu.Framework.UI.BunifuElipse bunifuElipse1;
        private Bunifu.Framework.UI.BunifuImageButton btn_cerrar;
        private System.Windows.Forms.Panel panel2;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel1;
        private Bunifu.Framework.UI.BunifuImageButton btn_cerrar_actas;
        public System.Windows.Forms.TextBox txt_buscar;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel12;
        private Bunifu.Framework.UI.BunifuFlatButton btn_buscar;
        private System.Windows.Forms.PictureBox picbox_socio;
        private Bunifu.Framework.UI.BunifuSeparator bunifuSeparator1;
        private Bunifu.Framework.UI.BunifuCustomLabel lbl_sexo;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel20;
        private Bunifu.Framework.UI.BunifuCustomLabel lbl_empresa;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel10;
        private Bunifu.Framework.UI.BunifuCustomLabel lbl_dni;
        private Bunifu.Framework.UI.BunifuCustomLabel lbl_nombre;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel6;
        private Bunifu.Framework.UI.BunifuCustomLabel lbl_nro_socio;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel3;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel8;
        private Bunifu.Framework.UI.BunifuCustomLabel lbl_apto;
        private Bunifu.Framework.UI.BunifuSeparator bunifuSeparator2;
    }
}