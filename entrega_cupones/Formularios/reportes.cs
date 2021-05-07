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
    public string nombreReporte = string.Empty;

    public DataTable dt, dt2, dt3, dt4, dt5 = new DataTable();

    public int Nro_cupon { get; set; }

    public string NombreDelReporte = string.Empty;


    ReportDataSource reportDtSrc1 = new ReportDataSource();
    ReportDataSource reportDtSrc2 = new ReportDataSource();
    ReportDataSource reportDtSrc3 = new ReportDataSource();
    ReportDataSource reportDtSrc4 = new ReportDataSource();

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
    public string Parametro15 { get; set; }
    public string Parametro16 { get; set; }
    public string Parametro17 { get; set; }
    public string Parametro18 { get; set; }
    public string Parametro19 { get; set; }
    public string Parametro20 { get; set; }
    public string Parametro21 { get; set; }

    public DataTable historial_pagos { get; set; }
    public DataTable denuncias { get; set; }
    public DataTable partidos { get; set; }
    public DataTable ddjj_por_empleado { get; set; }
    public DataTable dtCupondiaDelNiño { get; set; }
    public DataTable DtEntradaDDEDC { get; set; }
    public DataTable DtCajas { get; set; }
    public DataTable dtCobranzas { get; set; }
    public DataTable DtResumenActas { get; set; }
    public DataTable DtResumenInspectores { get; set; }
    public DataTable DtPlanDePago { get; set; }
    public DataTable DtEntregaDeMochilas { get; set; }
    public DataTable DtDiaDeLaMujer { get; set; }
    public DataTable DTResumenEmpleados { get; set; }

    public reportes()
    {
      InitializeComponent();
    }

    private void reportes_Load(object sender, EventArgs e)
    {
      reportDtSrc1.Name = "DataSet1";
      reportDtSrc1.Value = dt;

      reportDtSrc2.Name = "DataSet2";
      reportDtSrc2.Value = dt2;

      rv.LocalReport.DataSources.Add(reportDtSrc1);
      rv.LocalReport.DataSources.Add(reportDtSrc2);

      cambiar_reporte();
    }

    private void cambiar_reporte()
    {

      if (NombreDelReporte == "entrega_cupones.Reportes.rpt_VerificacionDeDeuda.rdlc")
      {
        rv.LocalReport.ReportEmbeddedResource = NombreDelReporte;
        //Array que contendrá los parámetros
        ReportParameter[] parameters = new ReportParameter[10];
        //Establecemos el valor de los parámetros
        parameters[0] = new ReportParameter("Empresa", Parametro1);
        parameters[1] = new ReportParameter("Cuit", Parametro2);
        parameters[2] = new ReportParameter("NumeroDeActa", Parametro3);
        parameters[3] = new ReportParameter("TotalDeuda", Parametro4);
        parameters[4] = new ReportParameter("TotalInteres", Parametro5);
        parameters[5] = new ReportParameter("TotalGeneral", Parametro6);
        parameters[6] = new ReportParameter("Original", Parametro7);
        parameters[7] = new ReportParameter("InformeDeInspector", Parametro8);
        parameters[8] = new ReportParameter("Vencimiento", Parametro9);
        parameters[9] = new ReportParameter("PerNoDec", Parametro10);

        this.rv.LocalReport.SetParameters(parameters);
      }

      if (NombreDelReporte == "entrega_cupones.Reportes.rpt_VerificacionDeDeudaSGO.rdlc")
      {
        rv.LocalReport.ReportEmbeddedResource = NombreDelReporte;
        //Array que contendrá los parámetros
        ReportParameter[] parameters = new ReportParameter[10];
        //Establecemos el valor de los parámetros
        parameters[0] = new ReportParameter("Empresa", Parametro1);
        parameters[1] = new ReportParameter("Cuit", Parametro2);
        parameters[2] = new ReportParameter("NumeroDeActa", Parametro3);
        parameters[3] = new ReportParameter("TotalDeuda", Parametro4);
        parameters[4] = new ReportParameter("TotalInteres", Parametro5);
        parameters[5] = new ReportParameter("TotalGeneral", Parametro6);
        parameters[6] = new ReportParameter("Original", Parametro7);
        parameters[7] = new ReportParameter("InformeDeInspector", Parametro8);
        parameters[8] = new ReportParameter("Vencimiento", Parametro9);
        parameters[9] = new ReportParameter("PerNoDec", Parametro10);

        this.rv.LocalReport.SetParameters(parameters);
      }

      if (NombreDelReporte == "entrega_cupones.Reportes.rpt_ActaCabecera.rdlc")
      {
        this.rv.LocalReport.ReportEmbeddedResource = NombreDelReporte;

        //Array que contendrá los parámetros
        ReportParameter[] parameters = new ReportParameter[21];
        //Establecemos el valor de los parámetros
        parameters[0] = new ReportParameter("Numero", Parametro1);
        parameters[1] = new ReportParameter("RazonSocial", Parametro2);
        parameters[2] = new ReportParameter("Domicilio", Parametro3);
        parameters[3] = new ReportParameter("Desde", Parametro4);
        parameters[4] = new ReportParameter("Hasta", Parametro5);
        parameters[5] = new ReportParameter("Vencimiento", Parametro6);
        parameters[6] = new ReportParameter("Cuit", Parametro7);
        parameters[7] = new ReportParameter("Total", Parametro8);

        parameters[8] = new ReportParameter("ActasAnteriores", Parametro9);
        parameters[9] = new ReportParameter("InicioDeActividades", Parametro10);
        parameters[10] = new ReportParameter("EmpleadoCantidad", Parametro11);
        parameters[11] = new ReportParameter("Telefono", Parametro12);
        parameters[12] = new ReportParameter("FechaDeConfeccion", Parametro13);
        parameters[13] = new ReportParameter("Lugar", Parametro14);
        parameters[14] = new ReportParameter("Dia", Parametro15);
        parameters[15] = new ReportParameter("Mes", Parametro16);
        parameters[16] = new ReportParameter("Ano", Parametro17);
        parameters[17] = new ReportParameter("Persona", Parametro18);
        parameters[18] = new ReportParameter("Relacion", Parametro19);
        parameters[19] = new ReportParameter("NumToWords", Parametro20);
        parameters[20] = new ReportParameter("Observaciones", Parametro21);

        this.rv.LocalReport.SetParameters(parameters);
        this.rv.RefreshReport();
      }

      if (NombreDelReporte == "entrega_cupones.Reportes.rpt_ActaDetalle.rdlc")
      {
        this.rv.LocalReport.ReportEmbeddedResource = NombreDelReporte;
        //Array que contendrá los parámetros
        ReportParameter[] parameters = new ReportParameter[9];
        //Establecemos el valor de los parámetros
        parameters[0] = new ReportParameter("Empresa", Parametro1);
        parameters[1] = new ReportParameter("Cuit", Parametro2);
        parameters[2] = new ReportParameter("NumeroDeActa", Parametro3);
        parameters[3] = new ReportParameter("TotalDeuda", Parametro4);
        parameters[4] = new ReportParameter("TotalInteres", Parametro5);
        parameters[5] = new ReportParameter("TotalGeneral", Parametro6);
        parameters[6] = new ReportParameter("Original", Parametro7);
        parameters[7] = new ReportParameter("InformeDeInspector", Parametro8);
        parameters[8] = new ReportParameter("Vencimiento", Parametro9);

        this.rv.LocalReport.SetParameters(parameters);
        this.rv.RefreshReport();
      }

      if (NombreDelReporte == "entrega_cupones.Reportes.rpt_PlanDePagoActa_SGO.rdlc")
      {
        this.rv.LocalReport.ReportEmbeddedResource = NombreDelReporte;
        //Array que contendrá los parámetros
        ReportParameter[] parameters = new ReportParameter[7];
        //Establecemos el valor de los parámetros
        parameters[0] = new ReportParameter("TipoDeInforme", Parametro1);
        parameters[1] = new ReportParameter("prmRazonSocial", Parametro2);
        parameters[2] = new ReportParameter("prmCUIT", Parametro3);
        parameters[3] = new ReportParameter("prmNombreDeFantasia", "");
        parameters[4] = new ReportParameter("prmDeudaInicial", Parametro5);
        parameters[5] = new ReportParameter("prmTotalFinanciado", Parametro6);
        parameters[6] = new ReportParameter("prmActa", Parametro7);

        this.rv.LocalReport.SetParameters(parameters);
        this.rv.RefreshReport();
      }

      if (NombreDelReporte == "entrega_cupones.Reportes.rpt_PlanDePagoActa.rdlc")
      {
        this.rv.LocalReport.ReportEmbeddedResource = NombreDelReporte;
        //Array que contendrá los parámetros
        ReportParameter[] parameters = new ReportParameter[7];
        //Establecemos el valor de los parámetros
        parameters[0] = new ReportParameter("TipoDeInforme", Parametro1);
        parameters[1] = new ReportParameter("prmRazonSocial", Parametro2);
        parameters[2] = new ReportParameter("prmCUIT", Parametro3);
        parameters[3] = new ReportParameter("prmNombreDeFantasia", "");
        parameters[4] = new ReportParameter("prmDeudaInicial", Parametro5);
        parameters[5] = new ReportParameter("prmTotalFinanciado", Parametro6);
        parameters[6] = new ReportParameter("prmActa", Parametro7);

        this.rv.LocalReport.SetParameters(parameters);
        this.rv.RefreshReport();
      }

      if (NombreDelReporte == "rpt_ddjj_por_empleado")
      {
        rv.LocalReport.ReportEmbeddedResource = "entrega_cupones.Reportes.rpt_ddjj_por_empleado.rdlc";
      }

      if (nombreReporte == "planilla_partidos")
      {
        rv.LocalReport.ReportEmbeddedResource = "entrega_cupones.Reportes.planilla_partidos.rdlc";
        //reportViewer1.LocalReport.ReportPath = Path.Combine(entrega_cupones.reportes, "planilla_partidos.rdlc");
        //nueva_fuente_de_datos.Name = "DataSet1";
        //nueva_fuente_de_datos.Value = impresion_comprobanteBindingSource;
        //rv.LocalReport.DataSources.Add(nueva_fuente_de_datos);
      }

      if (nombreReporte == "planilla_arbitro")
      {
        rv.LocalReport.ReportEmbeddedResource = "entrega_cupones.Reportes.informe_arbitro.rdlc";
        //nueva_fuente_de_datos.Name = "DataSet1";
        //nueva_fuente_de_datos.Value = impresion_comprobanteBindingSource;
        //rv.LocalReport.DataSources.Add(nueva_fuente_de_datos);
      }

      if (nombreReporte == "simulacion_actas")
      {
        rv.LocalReport.ReportEmbeddedResource = "entrega_cupones.simulacion_actas.rdlc";

        //nueva_fuente_de_datos.Name = "DataSet1";
        //nueva_fuente_de_datos.Value = impresion_comprobanteBindingSource;
        //rv.LocalReport.DataSources.Add(nueva_fuente_de_datos);
      }

      if (nombreReporte == "certificado_de_deuda")
      {
        rv.LocalReport.ReportEmbeddedResource = "entrega_cupones.Reportes.Certificado_de_Deuda.rdlc";
        //nueva_fuente_de_datos.Name = "DataSet1";
        //nueva_fuente_de_datos.Value = impresion_comprobanteBindingSource;
        //rv.LocalReport.DataSources.Add(nueva_fuente_de_datos);
      }

      if (nombreReporte == "rpt_asig_inspector")
      {
        rv.LocalReport.ReportEmbeddedResource = "entrega_cupones.Reportes.rpt_asig_inspector.rdlc";
        //nueva_fuente_de_datos.Name = "DataSet1";
        //nueva_fuente_de_datos.Value = impresion_comprobanteBindingSource;
        //rv.LocalReport.DataSources.Add(nueva_fuente_de_datos);
      }

      if (nombreReporte == "rpt_equipos")
      {
        rv.LocalReport.ReportEmbeddedResource = "entrega_cupones.Reportes.rpt_equipos.rdlc";
        //nueva_fuente_de_datos.Name = "DataSet1";
        //nueva_fuente_de_datos.Value = impresion_comprobanteBindingSource;
        //rv.LocalReport.DataSources.Add(nueva_fuente_de_datos);
      }

      if (nombreReporte == "rpt_jugadores_inscriptos")
      {
        rv.LocalReport.ReportEmbeddedResource = "entrega_cupones.Reportes.rpt_jugadores_inscriptos.rdlc";
        //nueva_fuente_de_datos.Name = "DataSet1";
        //nueva_fuente_de_datos.Value = impresion_comprobanteBindingSource;
        //rv.LocalReport.DataSources.Add(nueva_fuente_de_datos);
      }

      if (nombreReporte == "rpt_consulta_ddjj")
      {
        rv.LocalReport.ReportEmbeddedResource = "entrega_cupones.Reportes.rpt_consulta_ddjj.rdlc";

        //reportDataSource1.Name = "DataSet1";
        //reportDtSrc2.Value = dt;
        rv.LocalReport.DataSources.Add(reportDtSrc1);

        reportDtSrc2.Name = "DataSet2";
        reportDtSrc2.Value = dt2;
        rv.LocalReport.DataSources.Add(reportDtSrc2);

        reportDtSrc3.Name = "DataSet3";
        reportDtSrc3.Value = dt3;
        rv.LocalReport.DataSources.Add(reportDtSrc3);

        reportDtSrc4.Name = "DataSet4";
        reportDtSrc4.Value = dt4;
        rv.LocalReport.DataSources.Add(reportDtSrc4);

        //nueva_fuente_de_datos.Name = "DataSet1";
        //nueva_fuente_de_datos.Value = impresion_comprobanteBindingSource;
        //rv.LocalReport.DataSources.Add(nueva_fuente_de_datos);

        //nuevoDS.Name = "DataSet2";
        //nuevoDS.Value = impresion_actasBindingSource;
        //rv.LocalReport.DataSources.Add(nuevoDS);

        //nuevoDS3.Name = "DataSet3";
        //nuevoDS3.Value = historial_pagos;
        //rv.LocalReport.DataSources.Add(nuevoDS3);

        //nuevoDS4.Name = "DataSet4";
        //nuevoDS4.Value = denuncias;
        //rv.LocalReport.DataSources.Add(nuevoDS4);

      }

      if (nombreReporte == "rpt_partidos_5")
      {
        rv.LocalReport.ReportEmbeddedResource = "entrega_cupones.Reportes.rpt_partidos_5.rdlc";
        //nueva_fuente_de_datos.Name = "DataSet1";
        //nueva_fuente_de_datos.Value = partidos;
        //rv.LocalReport.DataSources.Add(nueva_fuente_de_datos);
      }

      if (nombreReporte == "rpt_ddjj_por_empleado")
      {
        rv.LocalReport.ReportEmbeddedResource = "entrega_cupones.Reportes.rpt_ddjj_por_empleado.rdlc";
        //nueva_fuente_de_datos.Name = "DataSet1";
        //nueva_fuente_de_datos.Value = ddjj_por_empleado;
        //rv.LocalReport.DataSources.Add(nueva_fuente_de_datos);
      }

      if (NombreDelReporte == "rpt_ddjj_por_empleado_Banda")
      {
        rv.LocalReport.ReportEmbeddedResource = "entrega_cupones.Reportes.rpt_ddjj_por_empleado_Banda.rdlc";
        //nueva_fuente_de_datos.Name = "DataSet1";
        //nueva_fuente_de_datos.Value = ddjj_por_empleado;
        //rv.LocalReport.DataSources.Add(nueva_fuente_de_datos);
      }

      if (nombreReporte == "rpt_CuponDiaDelNiño_2")
      {
        rv.LocalReport.ReportEmbeddedResource = "entrega_cupones.Reportes.rpt_CuponDiaDelNiño_2.rdlc";
        //nueva_fuente_de_datos.Name = "DataSet1";
        //nueva_fuente_de_datos.Value = dtCupondiaDelNiño;
        //rv.LocalReport.DataSources.Add(nueva_fuente_de_datos);
      }

      if (nombreReporte == "rpt_CuponDiaDelNiño_TRH")
      {
        rv.LocalReport.ReportEmbeddedResource = "entrega_cupones.Reportes.rpt_CuponDiaDelNiño_TRH.rdlc";
        //nueva_fuente_de_datos.Name = "DataSet1";
        //nueva_fuente_de_datos.Value = dtCupondiaDelNiño;
        //rv.LocalReport.DataSources.Add(nueva_fuente_de_datos);
      }

      if (nombreReporte == "rpt_EntradaSocioDDEDC")
      {
        rv.LocalReport.ReportEmbeddedResource = "entrega_cupones.Reportes.rpt_EntradaSocioDDEDC.rdlc";
        //nueva_fuente_de_datos.Name = "DataSet1";
        //nueva_fuente_de_datos.Value = DtEntradaDDEDC;
        //rv.LocalReport.DataSources.Add(nueva_fuente_de_datos);
      }

      if (nombreReporte == "rpt_EntradaInvitadoDDEDC")
      {
        rv.LocalReport.ReportEmbeddedResource = "entrega_cupones.Reportes.rpt_EntradaInvitadoDDEDC.rdlc";
        //nueva_fuente_de_datos.Name = "DataSet1";
        //nueva_fuente_de_datos.Value = DtEntradaDDEDC;
        //rv.LocalReport.DataSources.Add(nueva_fuente_de_datos);
      }

      if (nombreReporte == "rpt_ConsultaDeCaja")
      {
        rv.LocalReport.ReportEmbeddedResource = "entrega_cupones.Reportes.rpt_ConsultaDeCaja.rdlc";
        //nueva_fuente_de_datos.Name = "DataSet1";
        //nueva_fuente_de_datos.Value = DtCajas;
        //rv.LocalReport.DataSources.Add(nueva_fuente_de_datos);
      }

      if (nombreReporte == "rpt_CobranzasEmpresasConDeuda")
      {
        //Array que contendrá los parámetros
        ReportParameter[] parameters = new ReportParameter[5];
        //Establecemos el valor de los parámetros
        parameters[0] = new ReportParameter("TipoDeInforme", Parametro1);
        parameters[1] = new ReportParameter("prmDesde", Parametro2);
        parameters[2] = new ReportParameter("prmHasta", Parametro3);
        parameters[3] = new ReportParameter("prmInspector", Parametro4);
        parameters[4] = new ReportParameter("prmInteres", Parametro5);
        //Pasamos el array de los parámetros al ReportViewer

        rv.LocalReport.ReportEmbeddedResource = "entrega_cupones.Reportes.rpt_CobranzasEmpresasConDeuda.rdlc";
        //nueva_fuente_de_datos.Name = "DataSet1";
        //nueva_fuente_de_datos.Value = dtCobranzas;
        //rv.LocalReport.DataSources.Add(nueva_fuente_de_datos);
        rv.LocalReport.SetParameters(parameters);
      }

      if (NombreDelReporte == "rpt_PlanDePago")
      {
        //Array que contendrá los parámetros
        ReportParameter[] parameters = new ReportParameter[6];
        //Establecemos el valor de los parámetros
        parameters[0] = new ReportParameter("TipoDeInforme", Parametro1);
        parameters[1] = new ReportParameter("prmRazonSocial", Parametro2);
        parameters[2] = new ReportParameter("prmCUIT", Parametro3);
        parameters[3] = new ReportParameter("prmNombreDeFantasia", Parametro4);
        parameters[4] = new ReportParameter("prmDeudaInicial", Parametro5);
        parameters[5] = new ReportParameter("prmTotalFinanciado", Parametro6);

        //Pasamos el array de los parámetros al ReportViewer

        rv.LocalReport.ReportEmbeddedResource = "entrega_cupones.Reportes.rpt_PlanDePago.rdlc";
        //nueva_fuente_de_datos.Name = "DataSet1";
        //nueva_fuente_de_datos.Value = DtPlanDePago;

        //rv.LocalReport.DataSources.Add(nueva_fuente_de_datos);
        rv.LocalReport.SetParameters(parameters);
      }

      if (NombreDelReporte == "entrega_cupones.Reportes.rpt_EntregaDeMochila.rdlc")
      {

        //Sólo tenemos que añadir la siguiente línea de código antes de que se muestre el informe:
        string ReportNameStore = "Cpn_" + Parametro2 + "_DNI " + Parametro10.Trim() + "_" + Parametro15 + "_NSOC " + Parametro3 + "_ST_" + Parametro9.Trim() + "_SB_" + Parametro6.Trim() + "_MOCHI_" + Parametro8;

        rv.LocalReport.DisplayName = ReportNameStore;

        rv.LocalReport.ReportEmbeddedResource = NombreDelReporte;
        //Array que contendrá los parámetros
        ReportParameter[] parameters = new ReportParameter[15];
        //Establecemos el valor de los parámetros
        parameters[0] = new ReportParameter("Parametro1", Parametro1);
        parameters[1] = new ReportParameter("Parametro2", Parametro2);
        parameters[2] = new ReportParameter("Parametro3", Parametro3);
        parameters[3] = new ReportParameter("Parametro4", Parametro4);
        parameters[4] = new ReportParameter("Parametro5", Parametro5);
        parameters[5] = new ReportParameter("Parametro6", Parametro6);
        parameters[6] = new ReportParameter("Parametro7", Parametro7);
        parameters[7] = new ReportParameter("Parametro8", Parametro8);
        parameters[8] = new ReportParameter("Parametro9", Parametro9);
        parameters[9] = new ReportParameter("Parametro10", Parametro10);
        parameters[10] = new ReportParameter("Parametro11", Parametro11);
        parameters[11] = new ReportParameter("Parametro12", Parametro12);
        parameters[12] = new ReportParameter("Parametro13", Parametro13);
        parameters[13] = new ReportParameter("Parametro14", Parametro14);
        parameters[14] = new ReportParameter("Parametro15", Parametro15);

        //Pasamos el array de los parámetros al ReportViewer

        this.rv.LocalReport.SetParameters(parameters);

        this.rv.RefreshReport();

      }

      if (nombreReporte == "rpt_EntradaDiaDeLaMujer")
      {
        //Array que contendrá los parámetros
        ReportParameter[] parameters = new ReportParameter[9];
        //Establecemos el valor de los parámetros
        parameters[0] = new ReportParameter("Parametro1", Parametro1);
        parameters[1] = new ReportParameter("Parametro2", Parametro2);
        parameters[2] = new ReportParameter("Parametro3", Parametro3);
        parameters[3] = new ReportParameter("Parametro4", Parametro4);
        parameters[4] = new ReportParameter("Parametro5", Parametro5);
        parameters[5] = new ReportParameter("Parametro6", Parametro6);
        parameters[6] = new ReportParameter("Parametro7", Parametro7);
        parameters[7] = new ReportParameter("Parametro8", Parametro8);
        parameters[8] = new ReportParameter("Parametro9", Parametro9);

        //Pasamos el array de los parámetros al ReportViewer

        rv.LocalReport.ReportEmbeddedResource = "entrega_cupones.Reportes.rpt_EntradaDiaDeLaMujer.rdlc";
        //nueva_fuente_de_datos.Name = "DataSet1";
        //nueva_fuente_de_datos.Value = DtDiaDeLaMujer;

        //rv.LocalReport.DataSources.Add(nueva_fuente_de_datos);
        rv.LocalReport.SetParameters(parameters);
      }

      rv.RefreshReport();
    }
  }
}
