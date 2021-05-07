using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace entrega_cupones
{
  public partial class reportes : Form
  {


    public reportes()
    {
      InitializeComponent();
    }
    public int Nro_cupon { get; set; }
    public string nombreReporte { get; set; }
    public string  Parametro1 { get; set; }
    public DataTable historial_pagos { get; set; }
    public DataTable denuncias { get; set; }
    public DataTable partidos { get; set; }
    public DataTable ddjj_por_empleado { get; set; }
    public DataTable dtCupondiaDelNiño { get; set; }
    public DataTable DtEntradaDDEDC { get; set; }
    public DataTable DtCajas { get; set; }
    public DataTable dtCobranzas { get; set; }

    private void reportes_Load(object sender, EventArgs e)
    {
      // TODO: esta línea de código carga datos en la tabla 'DS_cupones.impresion_actas' Puede moverla o quitarla según sea necesario.
      this.impresion_actasTableAdapter.Fill(this.DS_cupones.impresion_actas);
      // TODO: esta línea de código carga datos en la tabla 'DS_cupones.impresion_comprobante' Puede moverla o quitarla según sea necesario.
      this.impresion_comprobanteTableAdapter.Fill(this.DS_cupones.impresion_comprobante);
      //this.impresion_actasTableAdapter1.Fill(DS_cupones.impresion_actas);
      // TODO: esta línea de código carga datos en la tabla 'DS_cupones.imprimir_cupon' Puede moverla o quitarla según sea necesario.
      //this.imprimir_cuponTableAdapter.Fill(this.DS_cupones.imprimir_cupon,12);
      this.reportViewer1.RefreshReport();
      cambiar_reporte(nombreReporte);
    }

    private void cambiar_reporte(string nombre_reporte)
    {
      ReportDataSource nueva_fuente_de_datos = new ReportDataSource();
      ReportDataSource nuevoDS = new ReportDataSource();
      ReportDataSource nuevoDS3 = new ReportDataSource();
      ReportDataSource nuevoDS4 = new ReportDataSource();

      reportViewer1.LocalReport.DataSources.Clear();
      reportViewer1.LocalReport.Refresh();
      if (nombre_reporte == "planilla_partidos")
      {
        reportViewer1.LocalReport.ReportEmbeddedResource = "entrega_cupones.planilla_partidos.rdlc";
        //reportViewer1.LocalReport.ReportPath = Path.Combine(entrega_cupones.reportes, "planilla_partidos.rdlc");
        nueva_fuente_de_datos.Name = "DataSet1";
        nueva_fuente_de_datos.Value = impresion_comprobanteBindingSource;
        reportViewer1.LocalReport.DataSources.Add(nueva_fuente_de_datos);
      }
      if (nombre_reporte == "planilla_arbitro")
      {
        reportViewer1.LocalReport.ReportEmbeddedResource = "entrega_cupones.informe_arbitro.rdlc";
        nueva_fuente_de_datos.Name = "DataSet1";
        nueva_fuente_de_datos.Value = impresion_comprobanteBindingSource;
        reportViewer1.LocalReport.DataSources.Add(nueva_fuente_de_datos);
      }

      if (nombre_reporte == "simulacion_actas")
      {
        reportViewer1.LocalReport.ReportEmbeddedResource = "entrega_cupones.simulacion_actas.rdlc";

        nueva_fuente_de_datos.Name = "DataSet1";
        nueva_fuente_de_datos.Value = impresion_comprobanteBindingSource;
        reportViewer1.LocalReport.DataSources.Add(nueva_fuente_de_datos);
      }

      if (nombre_reporte == "certificado_de_deuda")
      {
        reportViewer1.LocalReport.ReportEmbeddedResource = "entrega_cupones.Reportes.Certificado_de_Deuda.rdlc";
        nueva_fuente_de_datos.Name = "DataSet1";
        nueva_fuente_de_datos.Value = impresion_comprobanteBindingSource;
        reportViewer1.LocalReport.DataSources.Add(nueva_fuente_de_datos);
      }

      if (nombre_reporte == "rpt_asig_inspector")
      {
        reportViewer1.LocalReport.ReportEmbeddedResource = "entrega_cupones.Reportes.rpt_asig_inspector.rdlc";
        nueva_fuente_de_datos.Name = "DataSet1";
        nueva_fuente_de_datos.Value = impresion_comprobanteBindingSource;
        reportViewer1.LocalReport.DataSources.Add(nueva_fuente_de_datos);
      }

      if (nombre_reporte == "rpt_equipos")
      {
        reportViewer1.LocalReport.ReportEmbeddedResource = "entrega_cupones.Reportes.rpt_equipos.rdlc";
        nueva_fuente_de_datos.Name = "DataSet1";
        nueva_fuente_de_datos.Value = impresion_comprobanteBindingSource;
        reportViewer1.LocalReport.DataSources.Add(nueva_fuente_de_datos);
      }

      if (nombre_reporte == "rpt_jugadores_inscriptos")
      {
        reportViewer1.LocalReport.ReportEmbeddedResource = "entrega_cupones.Reportes.rpt_jugadores_inscriptos.rdlc";
        nueva_fuente_de_datos.Name = "DataSet1";
        nueva_fuente_de_datos.Value = impresion_comprobanteBindingSource;
        reportViewer1.LocalReport.DataSources.Add(nueva_fuente_de_datos);
      }

      if (nombre_reporte == "rpt_consulta_ddjj")
      {
        reportViewer1.LocalReport.ReportEmbeddedResource = "entrega_cupones.Reportes.rpt_consulta_ddjj.rdlc";

        nueva_fuente_de_datos.Name = "DataSet1";
        nueva_fuente_de_datos.Value = impresion_comprobanteBindingSource;
        reportViewer1.LocalReport.DataSources.Add(nueva_fuente_de_datos);

        nuevoDS.Name = "DataSet2";
        nuevoDS.Value = impresion_actasBindingSource;
        reportViewer1.LocalReport.DataSources.Add(nuevoDS);

        nuevoDS3.Name = "DataSet3";
        nuevoDS3.Value = historial_pagos;
        reportViewer1.LocalReport.DataSources.Add(nuevoDS3);

        nuevoDS4.Name = "DataSet4";
        nuevoDS4.Value = denuncias;
        reportViewer1.LocalReport.DataSources.Add(nuevoDS4);
      }

      if (nombre_reporte == "rpt_partidos_5")
      {
        reportViewer1.LocalReport.ReportEmbeddedResource = "entrega_cupones.Reportes.rpt_partidos_5.rdlc";
        nueva_fuente_de_datos.Name = "DataSet1";
        nueva_fuente_de_datos.Value = partidos;
        reportViewer1.LocalReport.DataSources.Add(nueva_fuente_de_datos);
      }

      if (nombre_reporte == "rpt_ddjj_por_empleado")
      {
        reportViewer1.LocalReport.ReportEmbeddedResource = "entrega_cupones.Reportes.rpt_ddjj_por_empleado.rdlc";
        nueva_fuente_de_datos.Name = "DataSet1";
        nueva_fuente_de_datos.Value = ddjj_por_empleado;
        reportViewer1.LocalReport.DataSources.Add(nueva_fuente_de_datos);
      }

      if (nombre_reporte == "rpt_CuponDiaDelNiño_2")
      {
        reportViewer1.LocalReport.ReportEmbeddedResource = "entrega_cupones.Reportes.rpt_CuponDiaDelNiño_2.rdlc";
        nueva_fuente_de_datos.Name = "DataSet1";
        nueva_fuente_de_datos.Value = dtCupondiaDelNiño;
        reportViewer1.LocalReport.DataSources.Add(nueva_fuente_de_datos);
      }

      if (nombre_reporte == "rpt_CuponDiaDelNiño_TRH")
      {
        reportViewer1.LocalReport.ReportEmbeddedResource = "entrega_cupones.Reportes.rpt_CuponDiaDelNiño_TRH.rdlc";
        nueva_fuente_de_datos.Name = "DataSet1";
        nueva_fuente_de_datos.Value = dtCupondiaDelNiño;
        reportViewer1.LocalReport.DataSources.Add(nueva_fuente_de_datos);
      }

      if (nombre_reporte == "rpt_EntradaSocioDDEDC")
      {
        reportViewer1.LocalReport.ReportEmbeddedResource = "entrega_cupones.Reportes.rpt_EntradaSocioDDEDC.rdlc";
        nueva_fuente_de_datos.Name = "DataSet1";
        nueva_fuente_de_datos.Value = DtEntradaDDEDC;
        reportViewer1.LocalReport.DataSources.Add(nueva_fuente_de_datos);
      }

      //rpt_EntradaInvitadoDDEDC
      if (nombre_reporte == "rpt_EntradaInvitadoDDEDC")
      {
        reportViewer1.LocalReport.ReportEmbeddedResource = "entrega_cupones.Reportes.rpt_EntradaInvitadoDDEDC.rdlc";
        nueva_fuente_de_datos.Name = "DataSet1";
        nueva_fuente_de_datos.Value = DtEntradaDDEDC;
        reportViewer1.LocalReport.DataSources.Add(nueva_fuente_de_datos);
      }

      if (nombre_reporte == "rpt_ConsultaDeCaja")
      {
        reportViewer1.LocalReport.ReportEmbeddedResource = "entrega_cupones.Reportes.rpt_ConsultaDeCaja.rdlc";
        nueva_fuente_de_datos.Name = "DataSet1";
        nueva_fuente_de_datos.Value = DtCajas;
        reportViewer1.LocalReport.DataSources.Add(nueva_fuente_de_datos);
      }

      if (nombre_reporte == "rpt_CobranzasEmpresasConDeuda")
      {
        //Array que contendrá los parámetros
        ReportParameter[] parameters = new ReportParameter[1];
        //Establecemos el valor de los parámetros
        parameters[0] = new ReportParameter("TipoDeInforme", Parametro1);
        //Pasamos el array de los parámetros al ReportViewer
        

        reportViewer1.LocalReport.ReportEmbeddedResource = "entrega_cupones.Reportes.rpt_CobranzasEmpresasConDeuda.rdlc";
        nueva_fuente_de_datos.Name = "DataSet1";
        nueva_fuente_de_datos.Value = dtCobranzas;
        reportViewer1.LocalReport.DataSources.Add(nueva_fuente_de_datos);
        reportViewer1.LocalReport.SetParameters(parameters);
      }
    }

    private void reportViewer1_Load(object sender, EventArgs e)
    {

    }
  }
}
