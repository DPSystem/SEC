using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace entrega_cupones.Formularios
{
  public partial class frm_Reportes : Form
  {

    public string NombreDelReporte = string.Empty;

    public DataTable dt = new DataTable();

    public string Parametro1 { get; set; }
    public string Parametro2 { get; set; }
    public string Parametro3 { get; set; }
    public string Parametro4 { get; set; }
    public string Parametro5 { get; set; }
    public string Parametro6 { get; set; }
    public string Parametro7 { get; set; }
    public string Parametro8 { get; set; }
    public string Parametro9 { get; set; }
    public string Parametro10 { get; set; }
    public string Parametro11 { get; set; }
    public string Parametro12 { get; set; }
    public string Parametro13 { get; set; }
    public string Parametro14 { get; set; }

    public frm_Reportes()
    {
      InitializeComponent();
    }

    private void frm_Reportes_Load(object sender, EventArgs e)
    {

      var reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
      //reportDataSource1.Name = "DataSet1";
      reportDataSource1.Value = dt;
      this.rv.LocalReport.DataSources.Add(reportDataSource1);

      if (NombreDelReporte == "entrega_cupones.Reportes.Prueba.rdlc")//"SecSantiago.Reportes.rpt_VerificacionDeDeuda.rdlc")
      {
        this.rv.LocalReport.ReportEmbeddedResource = NombreDelReporte;
      }
        this.rv.RefreshReport();
    }
  }//D:\Proyectos\entrega_cupones\entrega_cupones\Reportes\Prueba.rdlc
}
