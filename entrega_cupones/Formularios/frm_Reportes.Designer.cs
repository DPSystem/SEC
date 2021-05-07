namespace entrega_cupones.Formularios
{
  partial class frm_Reportes
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
      this.rv = new Microsoft.Reporting.WinForms.ReportViewer();
      this.SuspendLayout();
      // 
      // rv
      // 
      this.rv.Dock = System.Windows.Forms.DockStyle.Fill;
      this.rv.Location = new System.Drawing.Point(0, 0);
      this.rv.Name = "rv";
      this.rv.ServerReport.BearerToken = null;
      this.rv.Size = new System.Drawing.Size(800, 450);
      this.rv.TabIndex = 0;
      // 
      // frm_Reportes
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(800, 450);
      this.Controls.Add(this.rv);
      this.Name = "frm_Reportes";
      this.Text = "frm_Reportes";
      this.Load += new System.EventHandler(this.frm_Reportes_Load);
      this.ResumeLayout(false);

    }

    #endregion

    private Microsoft.Reporting.WinForms.ReportViewer rv;
  }
}