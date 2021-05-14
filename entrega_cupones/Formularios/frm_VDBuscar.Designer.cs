
namespace entrega_cupones.Formularios
{
  partial class frm_VDBuscar
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
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
      this.panel1 = new System.Windows.Forms.Panel();
      this.bunifuCustomLabel1 = new Bunifu.Framework.UI.BunifuCustomLabel();
      this.dgv_VD = new System.Windows.Forms.DataGridView();
      this.btn_VerVD = new System.Windows.Forms.Button();
      this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.VD_Fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.VD_Cuit = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.VD_Empresa = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.VD_Acta = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.VD_Importe = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.VD_Estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.ImporteDepositado = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.DiasDeMora = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.InteresCobrado = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Empleados = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Socios = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.AporteSocioDif = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Capital = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Interes = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Total = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Acta = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.VerificacionDeuda = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.panel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dgv_VD)).BeginInit();
      this.SuspendLayout();
      // 
      // panel1
      // 
      this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
      this.panel1.Controls.Add(this.btn_VerVD);
      this.panel1.Controls.Add(this.bunifuCustomLabel1);
      this.panel1.Controls.Add(this.dgv_VD);
      this.panel1.Location = new System.Drawing.Point(0, 2);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(800, 320);
      this.panel1.TabIndex = 598;
      // 
      // bunifuCustomLabel1
      // 
      this.bunifuCustomLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(151)))), ((int)(((byte)(119)))));
      this.bunifuCustomLabel1.Font = new System.Drawing.Font("Century Gothic", 11F, System.Drawing.FontStyle.Bold);
      this.bunifuCustomLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(49)))), ((int)(((byte)(49)))));
      this.bunifuCustomLabel1.Location = new System.Drawing.Point(6, 4);
      this.bunifuCustomLabel1.Name = "bunifuCustomLabel1";
      this.bunifuCustomLabel1.Size = new System.Drawing.Size(789, 22);
      this.bunifuCustomLabel1.TabIndex = 590;
      this.bunifuCustomLabel1.Text = "Busqueda de Verificacion de Deuda";
      this.bunifuCustomLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // dgv_VD
      // 
      this.dgv_VD.AllowUserToAddRows = false;
      this.dgv_VD.AllowUserToResizeRows = false;
      dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
      dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
      this.dgv_VD.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
      this.dgv_VD.BorderStyle = System.Windows.Forms.BorderStyle.None;
      dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
      dataGridViewCellStyle2.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
      dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.dgv_VD.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
      this.dgv_VD.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgv_VD.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.VD_Fecha,
            this.VD_Cuit,
            this.VD_Empresa,
            this.VD_Acta,
            this.VD_Importe,
            this.VD_Estado,
            this.ImporteDepositado,
            this.DiasDeMora,
            this.InteresCobrado,
            this.Empleados,
            this.Socios,
            this.AporteSocioDif,
            this.Capital,
            this.Interes,
            this.Total,
            this.Acta,
            this.VerificacionDeuda});
      this.dgv_VD.Location = new System.Drawing.Point(6, 29);
      this.dgv_VD.Name = "dgv_VD";
      this.dgv_VD.RowHeadersVisible = false;
      this.dgv_VD.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.dgv_VD.Size = new System.Drawing.Size(789, 241);
      this.dgv_VD.TabIndex = 536;
      // 
      // btn_VerVD
      // 
      this.btn_VerVD.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_VerVD.Location = new System.Drawing.Point(713, 276);
      this.btn_VerVD.Name = "btn_VerVD";
      this.btn_VerVD.Size = new System.Drawing.Size(82, 38);
      this.btn_VerVD.TabIndex = 607;
      this.btn_VerVD.Text = "Ver VD";
      this.btn_VerVD.UseVisualStyleBackColor = true;
      this.btn_VerVD.Click += new System.EventHandler(this.btn_VerVD_Click);
      // 
      // Id
      // 
      this.Id.DataPropertyName = "Id";
      dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
      dataGridViewCellStyle3.Format = "N2";
      dataGridViewCellStyle3.NullValue = null;
      this.Id.DefaultCellStyle = dataGridViewCellStyle3;
      this.Id.HeaderText = "Numero";
      this.Id.Name = "Id";
      this.Id.Width = 70;
      // 
      // VD_Fecha
      // 
      this.VD_Fecha.DataPropertyName = "FechaAsignacion";
      dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
      this.VD_Fecha.DefaultCellStyle = dataGridViewCellStyle4;
      this.VD_Fecha.HeaderText = "Fecha";
      this.VD_Fecha.Name = "VD_Fecha";
      this.VD_Fecha.Width = 120;
      // 
      // VD_Cuit
      // 
      this.VD_Cuit.DataPropertyName = "Cuit";
      dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
      dataGridViewCellStyle5.Format = "N2";
      dataGridViewCellStyle5.NullValue = null;
      this.VD_Cuit.DefaultCellStyle = dataGridViewCellStyle5;
      this.VD_Cuit.HeaderText = "CUIT";
      this.VD_Cuit.Name = "VD_Cuit";
      // 
      // VD_Empresa
      // 
      dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      this.VD_Empresa.DefaultCellStyle = dataGridViewCellStyle6;
      this.VD_Empresa.HeaderText = "Empresa";
      this.VD_Empresa.Name = "VD_Empresa";
      this.VD_Empresa.Width = 300;
      // 
      // VD_Acta
      // 
      dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
      dataGridViewCellStyle7.Format = "N2";
      dataGridViewCellStyle7.NullValue = null;
      this.VD_Acta.DefaultCellStyle = dataGridViewCellStyle7;
      this.VD_Acta.HeaderText = "Acta";
      this.VD_Acta.Name = "VD_Acta";
      this.VD_Acta.Visible = false;
      // 
      // VD_Importe
      // 
      this.VD_Importe.DataPropertyName = "Total";
      dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
      dataGridViewCellStyle8.Format = "N2";
      dataGridViewCellStyle8.NullValue = null;
      this.VD_Importe.DefaultCellStyle = dataGridViewCellStyle8;
      this.VD_Importe.HeaderText = "Importe";
      this.VD_Importe.Name = "VD_Importe";
      this.VD_Importe.Width = 80;
      // 
      // VD_Estado
      // 
      this.VD_Estado.DataPropertyName = "Estado";
      dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
      dataGridViewCellStyle9.Format = "N2";
      dataGridViewCellStyle9.NullValue = null;
      this.VD_Estado.DefaultCellStyle = dataGridViewCellStyle9;
      this.VD_Estado.HeaderText = "Estado";
      this.VD_Estado.Name = "VD_Estado";
      // 
      // ImporteDepositado
      // 
      this.ImporteDepositado.DataPropertyName = "ImporteDepositado";
      dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
      dataGridViewCellStyle10.Format = "N2";
      dataGridViewCellStyle10.NullValue = null;
      this.ImporteDepositado.DefaultCellStyle = dataGridViewCellStyle10;
      this.ImporteDepositado.HeaderText = "Depositado ";
      this.ImporteDepositado.Name = "ImporteDepositado";
      this.ImporteDepositado.Visible = false;
      this.ImporteDepositado.Width = 80;
      // 
      // DiasDeMora
      // 
      this.DiasDeMora.DataPropertyName = "DiasDeMora";
      dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
      this.DiasDeMora.DefaultCellStyle = dataGridViewCellStyle11;
      this.DiasDeMora.HeaderText = "Dias de Mora";
      this.DiasDeMora.Name = "DiasDeMora";
      this.DiasDeMora.Visible = false;
      this.DiasDeMora.Width = 70;
      // 
      // InteresCobrado
      // 
      this.InteresCobrado.DataPropertyName = "InteresCobrado";
      dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
      dataGridViewCellStyle12.Format = "N2";
      dataGridViewCellStyle12.NullValue = null;
      this.InteresCobrado.DefaultCellStyle = dataGridViewCellStyle12;
      this.InteresCobrado.HeaderText = "Interes Cobrado";
      this.InteresCobrado.Name = "InteresCobrado";
      this.InteresCobrado.Visible = false;
      this.InteresCobrado.Width = 80;
      // 
      // Empleados
      // 
      this.Empleados.DataPropertyName = "Empleados";
      dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
      this.Empleados.DefaultCellStyle = dataGridViewCellStyle13;
      this.Empleados.HeaderText = "Empleados ";
      this.Empleados.Name = "Empleados";
      this.Empleados.Visible = false;
      this.Empleados.Width = 80;
      // 
      // Socios
      // 
      this.Socios.DataPropertyName = "Socios";
      dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
      this.Socios.DefaultCellStyle = dataGridViewCellStyle14;
      this.Socios.HeaderText = "Socios";
      this.Socios.Name = "Socios";
      this.Socios.Visible = false;
      this.Socios.Width = 70;
      // 
      // AporteSocioDif
      // 
      this.AporteSocioDif.DataPropertyName = "AporteSocioDifJorPar";
      dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
      dataGridViewCellStyle15.Format = "N2";
      dataGridViewCellStyle15.NullValue = null;
      this.AporteSocioDif.DefaultCellStyle = dataGridViewCellStyle15;
      this.AporteSocioDif.HeaderText = "Dif Aporte Soc Jor Parcial";
      this.AporteSocioDif.Name = "AporteSocioDif";
      this.AporteSocioDif.Visible = false;
      // 
      // Capital
      // 
      this.Capital.DataPropertyName = "Capital";
      dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
      dataGridViewCellStyle16.Format = "N2";
      dataGridViewCellStyle16.NullValue = null;
      this.Capital.DefaultCellStyle = dataGridViewCellStyle16;
      this.Capital.HeaderText = "Deuda";
      this.Capital.Name = "Capital";
      this.Capital.Visible = false;
      this.Capital.Width = 80;
      // 
      // Interes
      // 
      this.Interes.DataPropertyName = "Interes";
      dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
      dataGridViewCellStyle17.Format = "N2";
      dataGridViewCellStyle17.NullValue = null;
      this.Interes.DefaultCellStyle = dataGridViewCellStyle17;
      this.Interes.HeaderText = "Interes";
      this.Interes.Name = "Interes";
      this.Interes.Visible = false;
      this.Interes.Width = 80;
      // 
      // Total
      // 
      this.Total.DataPropertyName = "Total";
      dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
      dataGridViewCellStyle18.Format = "N2";
      dataGridViewCellStyle18.NullValue = null;
      this.Total.DefaultCellStyle = dataGridViewCellStyle18;
      this.Total.HeaderText = "Total";
      this.Total.Name = "Total";
      this.Total.Visible = false;
      this.Total.Width = 80;
      // 
      // Acta
      // 
      this.Acta.DataPropertyName = "Acta";
      dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
      dataGridViewCellStyle19.Format = "N2";
      dataGridViewCellStyle19.NullValue = null;
      this.Acta.DefaultCellStyle = dataGridViewCellStyle19;
      this.Acta.HeaderText = "Acta";
      this.Acta.Name = "Acta";
      this.Acta.Visible = false;
      this.Acta.Width = 65;
      // 
      // VerificacionDeuda
      // 
      this.VerificacionDeuda.DataPropertyName = "VerifDeuda";
      dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
      this.VerificacionDeuda.DefaultCellStyle = dataGridViewCellStyle20;
      this.VerificacionDeuda.HeaderText = "Verif Deuda";
      this.VerificacionDeuda.Name = "VerificacionDeuda";
      this.VerificacionDeuda.Visible = false;
      this.VerificacionDeuda.Width = 80;
      // 
      // frm_VDBuscar
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(800, 321);
      this.Controls.Add(this.panel1);
      this.Name = "frm_VDBuscar";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Verificacion de Deuda - Buscar";
      this.Load += new System.EventHandler(this.frm_VDBuscar_Load);
      this.panel1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.dgv_VD)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel panel1;
    private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel1;
    public System.Windows.Forms.DataGridView dgv_VD;
    private System.Windows.Forms.Button btn_VerVD;
    private System.Windows.Forms.DataGridViewTextBoxColumn Id;
    private System.Windows.Forms.DataGridViewTextBoxColumn VD_Fecha;
    private System.Windows.Forms.DataGridViewTextBoxColumn VD_Cuit;
    private System.Windows.Forms.DataGridViewTextBoxColumn VD_Empresa;
    private System.Windows.Forms.DataGridViewTextBoxColumn VD_Acta;
    private System.Windows.Forms.DataGridViewTextBoxColumn VD_Importe;
    private System.Windows.Forms.DataGridViewTextBoxColumn VD_Estado;
    private System.Windows.Forms.DataGridViewTextBoxColumn ImporteDepositado;
    private System.Windows.Forms.DataGridViewTextBoxColumn DiasDeMora;
    private System.Windows.Forms.DataGridViewTextBoxColumn InteresCobrado;
    private System.Windows.Forms.DataGridViewTextBoxColumn Empleados;
    private System.Windows.Forms.DataGridViewTextBoxColumn Socios;
    private System.Windows.Forms.DataGridViewTextBoxColumn AporteSocioDif;
    private System.Windows.Forms.DataGridViewTextBoxColumn Capital;
    private System.Windows.Forms.DataGridViewTextBoxColumn Interes;
    private System.Windows.Forms.DataGridViewTextBoxColumn Total;
    private System.Windows.Forms.DataGridViewTextBoxColumn Acta;
    private System.Windows.Forms.DataGridViewTextBoxColumn VerificacionDeuda;
  }
}