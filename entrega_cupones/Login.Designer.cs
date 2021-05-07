namespace entrega_cupones
{
    partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbl_login_error = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbl_1 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.btn_cerrar = new Bunifu.Framework.UI.BunifuImageButton();
            this.lbl_cargando = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.CircleProgressBar = new Bunifu.Framework.UI.BunifuCircleProgressbar();
            this.btn_aceptar = new Bunifu.Framework.UI.BunifuFlatButton();
            this.bunifuCustomLabel5 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.txt_contraseña = new Bunifu.Framework.UI.BunifuMetroTextbox();
            this.bunifuCustomLabel2 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.txt_usuario = new Bunifu.Framework.UI.BunifuMetroTextbox();
            this.picbox_logo = new System.Windows.Forms.PictureBox();
            this.picbox_error = new System.Windows.Forms.PictureBox();
            this.DragControl = new Bunifu.Framework.UI.BunifuDragControl(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.DragControl_2 = new Bunifu.Framework.UI.BunifuDragControl(this.components);
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btn_cerrar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbox_logo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbox_error)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(49)))), ((int)(((byte)(60)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lbl_login_error);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.lbl_cargando);
            this.panel1.Controls.Add(this.CircleProgressBar);
            this.panel1.Controls.Add(this.btn_aceptar);
            this.panel1.Controls.Add(this.bunifuCustomLabel5);
            this.panel1.Controls.Add(this.txt_contraseña);
            this.panel1.Controls.Add(this.bunifuCustomLabel2);
            this.panel1.Controls.Add(this.txt_usuario);
            this.panel1.Controls.Add(this.picbox_logo);
            this.panel1.Controls.Add(this.picbox_error);
            this.panel1.Location = new System.Drawing.Point(1, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(329, 396);
            this.panel1.TabIndex = 13;
            // 
            // lbl_login_error
            // 
            this.lbl_login_error.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.lbl_login_error.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_login_error.ForeColor = System.Drawing.Color.White;
            this.lbl_login_error.Location = new System.Drawing.Point(12, 181);
            this.lbl_login_error.Name = "lbl_login_error";
            this.lbl_login_error.Size = new System.Drawing.Size(306, 34);
            this.lbl_login_error.TabIndex = 28;
            this.lbl_login_error.Text = "Usuario o Contraseña Incorrecto";
            this.lbl_login_error.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_login_error.Visible = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.panel2.Controls.Add(this.lbl_1);
            this.panel2.Controls.Add(this.btn_cerrar);
            this.panel2.Location = new System.Drawing.Point(-1, 1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(329, 41);
            this.panel2.TabIndex = 25;
            // 
            // lbl_1
            // 
            this.lbl_1.BackColor = System.Drawing.Color.Transparent;
            this.lbl_1.Font = new System.Drawing.Font("Century Gothic", 18F);
            this.lbl_1.ForeColor = System.Drawing.Color.Silver;
            this.lbl_1.Location = new System.Drawing.Point(72, 4);
            this.lbl_1.Name = "lbl_1";
            this.lbl_1.Size = new System.Drawing.Size(179, 33);
            this.lbl_1.TabIndex = 0;
            this.lbl_1.Text = "Iniciar Sesion";
            this.lbl_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_cerrar
            // 
            this.btn_cerrar.BackColor = System.Drawing.Color.Transparent;
            this.btn_cerrar.Image = ((System.Drawing.Image)(resources.GetObject("btn_cerrar.Image")));
            this.btn_cerrar.ImageActive = null;
            this.btn_cerrar.Location = new System.Drawing.Point(288, 5);
            this.btn_cerrar.Name = "btn_cerrar";
            this.btn_cerrar.Size = new System.Drawing.Size(39, 32);
            this.btn_cerrar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btn_cerrar.TabIndex = 18;
            this.btn_cerrar.TabStop = false;
            this.btn_cerrar.Zoom = 10;
            this.btn_cerrar.Click += new System.EventHandler(this.btn_cerrar_Click);
            // 
            // lbl_cargando
            // 
            this.lbl_cargando.AutoSize = true;
            this.lbl_cargando.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(49)))), ((int)(((byte)(60)))));
            this.lbl_cargando.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_cargando.ForeColor = System.Drawing.Color.White;
            this.lbl_cargando.Location = new System.Drawing.Point(127, 196);
            this.lbl_cargando.Name = "lbl_cargando";
            this.lbl_cargando.Size = new System.Drawing.Size(96, 17);
            this.lbl_cargando.TabIndex = 24;
            this.lbl_cargando.Text = "Cargando.....";
            this.lbl_cargando.Visible = false;
            // 
            // CircleProgressBar
            // 
            this.CircleProgressBar.animated = false;
            this.CircleProgressBar.animationIterval = 5;
            this.CircleProgressBar.animationSpeed = 200;
            this.CircleProgressBar.AutoSize = true;
            this.CircleProgressBar.BackColor = System.Drawing.Color.Transparent;
            this.CircleProgressBar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("CircleProgressBar.BackgroundImage")));
            this.CircleProgressBar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.CircleProgressBar.ForeColor = System.Drawing.Color.Gainsboro;
            this.CircleProgressBar.LabelVisible = true;
            this.CircleProgressBar.LineProgressThickness = 8;
            this.CircleProgressBar.LineThickness = 5;
            this.CircleProgressBar.Location = new System.Drawing.Point(99, 69);
            this.CircleProgressBar.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.CircleProgressBar.MaxValue = 100;
            this.CircleProgressBar.Name = "CircleProgressBar";
            this.CircleProgressBar.ProgressBackColor = System.Drawing.Color.Gainsboro;
            this.CircleProgressBar.ProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(177)))), ((int)(((byte)(136)))));
            this.CircleProgressBar.Size = new System.Drawing.Size(126, 126);
            this.CircleProgressBar.TabIndex = 23;
            this.CircleProgressBar.Value = 0;
            this.CircleProgressBar.Visible = false;
            // 
            // btn_aceptar
            // 
            this.btn_aceptar.Active = false;
            this.btn_aceptar.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.btn_aceptar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.btn_aceptar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_aceptar.BorderRadius = 0;
            this.btn_aceptar.ButtonText = "Ingresar";
            this.btn_aceptar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_aceptar.DisabledColor = System.Drawing.Color.Gray;
            this.btn_aceptar.Font = new System.Drawing.Font("Century Gothic", 10F);
            this.btn_aceptar.Iconcolor = System.Drawing.Color.Transparent;
            this.btn_aceptar.Iconimage = null;
            this.btn_aceptar.Iconimage_right = null;
            this.btn_aceptar.Iconimage_right_Selected = null;
            this.btn_aceptar.Iconimage_Selected = null;
            this.btn_aceptar.IconMarginLeft = 0;
            this.btn_aceptar.IconMarginRight = 0;
            this.btn_aceptar.IconRightVisible = true;
            this.btn_aceptar.IconRightZoom = 0D;
            this.btn_aceptar.IconVisible = true;
            this.btn_aceptar.IconZoom = 55D;
            this.btn_aceptar.IsTab = true;
            this.btn_aceptar.Location = new System.Drawing.Point(12, 344);
            this.btn_aceptar.Margin = new System.Windows.Forms.Padding(5);
            this.btn_aceptar.Name = "btn_aceptar";
            this.btn_aceptar.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.btn_aceptar.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(177)))), ((int)(((byte)(136)))));
            this.btn_aceptar.OnHoverTextColor = System.Drawing.Color.White;
            this.btn_aceptar.selected = false;
            this.btn_aceptar.Size = new System.Drawing.Size(308, 43);
            this.btn_aceptar.TabIndex = 21;
            this.btn_aceptar.Text = "Ingresar";
            this.btn_aceptar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btn_aceptar.Textcolor = System.Drawing.Color.White;
            this.btn_aceptar.TextFont = new System.Drawing.Font("Century Gothic", 10F);
            this.btn_aceptar.Click += new System.EventHandler(this.btn_aceptar_Click);
            // 
            // bunifuCustomLabel5
            // 
            this.bunifuCustomLabel5.AutoSize = true;
            this.bunifuCustomLabel5.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuCustomLabel5.ForeColor = System.Drawing.Color.White;
            this.bunifuCustomLabel5.Location = new System.Drawing.Point(127, 282);
            this.bunifuCustomLabel5.Name = "bunifuCustomLabel5";
            this.bunifuCustomLabel5.Size = new System.Drawing.Size(84, 17);
            this.bunifuCustomLabel5.TabIndex = 6;
            this.bunifuCustomLabel5.Text = "Contraseña";
            // 
            // txt_contraseña
            // 
            this.txt_contraseña.BorderColorFocused = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(177)))), ((int)(((byte)(136)))));
            this.txt_contraseña.BorderColorIdle = System.Drawing.Color.Gray;
            this.txt_contraseña.BorderColorMouseHover = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(177)))), ((int)(((byte)(136)))));
            this.txt_contraseña.BorderThickness = 2;
            this.txt_contraseña.characterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txt_contraseña.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_contraseña.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_contraseña.ForeColor = System.Drawing.Color.Silver;
            this.txt_contraseña.isPassword = true;
            this.txt_contraseña.Location = new System.Drawing.Point(11, 303);
            this.txt_contraseña.Margin = new System.Windows.Forms.Padding(4);
            this.txt_contraseña.MaxLength = 32767;
            this.txt_contraseña.Name = "txt_contraseña";
            this.txt_contraseña.Size = new System.Drawing.Size(309, 29);
            this.txt_contraseña.TabIndex = 5;
            this.txt_contraseña.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txt_contraseña.Enter += new System.EventHandler(this.txt_contraseña_Enter);
            this.txt_contraseña.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_contraseña_KeyDown);
            // 
            // bunifuCustomLabel2
            // 
            this.bunifuCustomLabel2.AutoSize = true;
            this.bunifuCustomLabel2.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuCustomLabel2.ForeColor = System.Drawing.Color.White;
            this.bunifuCustomLabel2.Location = new System.Drawing.Point(142, 228);
            this.bunifuCustomLabel2.Name = "bunifuCustomLabel2";
            this.bunifuCustomLabel2.Size = new System.Drawing.Size(54, 17);
            this.bunifuCustomLabel2.TabIndex = 2;
            this.bunifuCustomLabel2.Text = "Usuario";
            // 
            // txt_usuario
            // 
            this.txt_usuario.BorderColorFocused = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(177)))), ((int)(((byte)(136)))));
            this.txt_usuario.BorderColorIdle = System.Drawing.Color.Gray;
            this.txt_usuario.BorderColorMouseHover = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(177)))), ((int)(((byte)(136)))));
            this.txt_usuario.BorderThickness = 2;
            this.txt_usuario.characterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txt_usuario.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_usuario.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_usuario.ForeColor = System.Drawing.Color.Silver;
            this.txt_usuario.isPassword = false;
            this.txt_usuario.Location = new System.Drawing.Point(11, 249);
            this.txt_usuario.Margin = new System.Windows.Forms.Padding(4);
            this.txt_usuario.MaxLength = 32767;
            this.txt_usuario.Name = "txt_usuario";
            this.txt_usuario.Size = new System.Drawing.Size(309, 29);
            this.txt_usuario.TabIndex = 1;
            this.txt_usuario.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txt_usuario.Enter += new System.EventHandler(this.txt_usuario_Enter);
            this.txt_usuario.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_usuario_KeyDown);
            // 
            // picbox_logo
            // 
            this.picbox_logo.Image = ((System.Drawing.Image)(resources.GetObject("picbox_logo.Image")));
            this.picbox_logo.Location = new System.Drawing.Point(83, 48);
            this.picbox_logo.Name = "picbox_logo";
            this.picbox_logo.Size = new System.Drawing.Size(167, 165);
            this.picbox_logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picbox_logo.TabIndex = 26;
            this.picbox_logo.TabStop = false;
            this.picbox_logo.Visible = false;
            // 
            // picbox_error
            // 
            this.picbox_error.Image = ((System.Drawing.Image)(resources.GetObject("picbox_error.Image")));
            this.picbox_error.Location = new System.Drawing.Point(99, 56);
            this.picbox_error.Name = "picbox_error";
            this.picbox_error.Size = new System.Drawing.Size(135, 124);
            this.picbox_error.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picbox_error.TabIndex = 27;
            this.picbox_error.TabStop = false;
            this.picbox_error.Visible = false;
            // 
            // DragControl
            // 
            this.DragControl.Fixed = true;
            this.DragControl.Horizontal = true;
            this.DragControl.TargetControl = this.panel2;
            this.DragControl.Vertical = true;
            // 
            // timer1
            // 
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // DragControl_2
            // 
            this.DragControl_2.Fixed = true;
            this.DragControl_2.Horizontal = true;
            this.DragControl_2.TargetControl = this.lbl_1;
            this.DragControl_2.Vertical = true;
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.ClientSize = new System.Drawing.Size(331, 398);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Login";
            this.Opacity = 0.92D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btn_cerrar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbox_logo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbox_error)).EndInit();
            this.ResumeLayout(false);

        }


        #endregion

        private System.Windows.Forms.Panel panel1;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel5;
        private Bunifu.Framework.UI.BunifuMetroTextbox txt_contraseña;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel2;
        private Bunifu.Framework.UI.BunifuMetroTextbox txt_usuario;
        private Bunifu.Framework.UI.BunifuCustomLabel lbl_1;
        private Bunifu.Framework.UI.BunifuImageButton btn_cerrar;
        private Bunifu.Framework.UI.BunifuDragControl DragControl;
        private Bunifu.Framework.UI.BunifuFlatButton btn_aceptar;
        private Bunifu.Framework.UI.BunifuCircleProgressbar CircleProgressBar;
        private Bunifu.Framework.UI.BunifuCustomLabel lbl_cargando;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panel2;
        private Bunifu.Framework.UI.BunifuDragControl DragControl_2;
        private System.Windows.Forms.PictureBox picbox_logo;
        private System.Windows.Forms.PictureBox picbox_error;
        private Bunifu.Framework.UI.BunifuCustomLabel lbl_login_error;
    }
}