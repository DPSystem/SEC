using entrega_cupones.Metodos;
using entrega_cupones.Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace entrega_cupones.Formularios
{
  public partial class frm_GenerarActa : Form
  {
    public List<EstadoDDJJ> _PreActa;
    public List<mdlCuadroAmortizacion> _PlanDePago;

    public frm_GenerarActa()
    {
      InitializeComponent();
    }

    private void btn_GenerarActa_Click(object sender, EventArgs e)
    {
      GenerarActa();
      btn_Imprimir.Enabled = true;
    }

    private void GenerarActa()
    {
      DateTime desde = Convert.ToDateTime("01/" + msk_Desde.Text);
      DateTime hasta = Convert.ToDateTime("01/" + msk_Hasta.Text);
      DateTime Vencimiento = Convert.ToDateTime(msk_Vencimiento.Text);
      DateTime FechaDeConfeccion = Convert.ToDateTime(msk_FechaConfeccion.Text);


      decimal InteresMensual = Convert.ToDecimal(txt_Interes.Text);
      decimal InteresDiario = Convert.ToDecimal(txt_InteresDiario.Text);
      int EmpresaId = 0;
      using (var context = new lts_sindicatoDataContext())
      {
        EmpresaId = context.maeemp.Where(x => x.MEEMP_CUIT_STR == txt_CUIT.Text).FirstOrDefault().Id;
      }
      mtdActas.GuardarActaCabecera(_PreActa, FechaDeConfeccion, desde, hasta, Vencimiento, EmpresaId, txt_CUIT.Text, Convert.ToInt32(txt_CantidadEmpleado.Text), InteresMensual, InteresDiario, _PlanDePago);
    }

    private void frm_Generar_Acta_Load(object sender, EventArgs e)
    {
      using (var context = new lts_sindicatoDataContext())
      {
        txt_NumeroDeActa.Text = mtdActas.ObtenerNroDeActa().ToString();
        Empresa empresa = mtdEmpresas.GetEmpresa(txt_CUIT.Text);

        txt_Domicilio.Text = empresa.MAEEMP_CALLE != null ? empresa.MAEEMP_CALLE.Trim() + " " + empresa.MAEEMP_NRO : empresa.MAEEMP_CALLE;
        txt_CodigoPostal.Text = empresa.MAEEMP_CODPOS.ToString();
        txt_Localidad.Text = empresa.MAEEMP_CODLOC.ToString();
        msk_FechaConfeccion.Text = DateTime.Today.Date.ToString();
        txt_CantidadEmpleado.Text = _PreActa.Where(x => x.Periodo == _PreActa.Max(y => y.Periodo)).FirstOrDefault().Empleados.ToString();
      }
    }

    private void btn_Imprimir_Click(object sender, EventArgs e)
    {
      ImprimirActaCabecera();
      ImprimirPlanDePago();
    }

    private void ImprimirActaCabecera()
    {
      DateTime desde = Convert.ToDateTime("01/" + msk_Desde.Text);
      DateTime hasta = Convert.ToDateTime("01/" + msk_Hasta.Text);
      DateTime Vencimiento = Convert.ToDateTime(msk_Vencimiento.Text);

      int NumeroDeActa = 0;
      using (var context = new lts_sindicatoDataContext())
      {
        NumeroDeActa = context.Acta.Where(x => x.Numero == Convert.ToInt32(txt_NumeroDeActa.Text)).FirstOrDefault().Numero;
      }

      DateTime FechaDeConfeccion = Convert.ToDateTime(msk_FechaConfeccion.Text);

      reportes formReporte = new reportes();

      formReporte.Parametro1 = NumeroDeActa.ToString();
      formReporte.Parametro2 = txt_RazonSocial.Text.Trim();
      formReporte.Parametro3 = txt_Domicilio.Text.Trim() + "" + txt_Localidad.Text;
      formReporte.Parametro4 = desde.ToString("MM/yyyy");
      formReporte.Parametro5 = hasta.ToString("MM/yyyy");
      formReporte.Parametro6 = Vencimiento.ToString("dd/MM/yyyy");
      formReporte.Parametro7 = txt_CUIT.Text;
      formReporte.Parametro8 = _PreActa.Sum(x => x.Total).ToString("N2");
      formReporte.Parametro9 = txt_ActasAnteriores.Text;
      formReporte.Parametro10 = msk_InicioDeActividad.Text;
      formReporte.Parametro11 = txt_CantidadEmpleado.Text;
      formReporte.Parametro12 = txt_Telefono.Text;
      formReporte.Parametro13 = msk_FechaConfeccion.Text;
      formReporte.Parametro14 = txt_Lugar.Text;
      formReporte.Parametro15 = FechaDeConfeccion.Day.ToString();
      formReporte.Parametro16 = mtdFechas.NombreDelMes(FechaDeConfeccion.Month); //FechaDeConfeccion.Month.ToString("mm");
      formReporte.Parametro17 = FechaDeConfeccion.Year.ToString();
      formReporte.Parametro18 = txt_persona.Text;
      formReporte.Parametro19 = txt_Relacion.Text;
      formReporte.Parametro20 = mtdNum2words.enletras(txt_Total.Text);
      formReporte.Parametro21 = txt_Observaciones.Text;

      formReporte.dt = mtdFilial.Get_DatosFilial();
      formReporte.NombreDelReporte = "entrega_cupones.Reportes.rpt_ActaCabecera.rdlc";
      formReporte.Show();

      ImprimirActaDetalle();
    }

    private void ImprimirActaDetalle()
    {
      DS_cupones ds = new DS_cupones();
      DataTable dt_ActasDetalle = ds.ActasDetalle;
      dt_ActasDetalle.Clear();
      int NumeroDeActa = 0;
      using (var context = new lts_sindicatoDataContext())
      {
        NumeroDeActa = context.Acta.Where(x => x.Numero == Convert.ToInt32(txt_NumeroDeActa.Text)).FirstOrDefault().Numero;
      }
      int color = 0;
      string fecha2 = "";
      foreach (var periodo in _PreActa)
      {
        DataRow row = dt_ActasDetalle.NewRow();
        row["NumeroDeActa"] = NumeroDeActa;
        row["Periodo"] = periodo.Periodo;
        row["CantidadDeEmpleados"] = periodo.Empleados;
        row["CantidadSocios"] = periodo.Socios;
        row["TotalSueldoEmpleados"] = periodo.TotalSueldoEmpleados;
        row["TotalSueldoSocios"] = periodo.TotalSueldoSocios;
        row["TotalAporteEmpleados"] = periodo.AporteLey;
        row["TotalAporteSocios"] = periodo.AporteSocio;
        fecha2 = periodo.FechaDePago.ToString();
        if (fecha2 != "")
        {
          fecha2 = Convert.ToDateTime(periodo.FechaDePago).Date.ToString("dd/MM/yyy");
        }
        row["FechaDePago"] = fecha2;//periodo.FechaDePago.ToString();//== null ? "01/01/0001" : periodo.FechaDePago.Value.Date.ToString();
        row["ImporteDepositado"] = periodo.ImporteDepositado;
        row["DiasDeMora"] = periodo.DiasDeMora;
        row["DeudaGenerada"] = periodo.Capital;
        row["InteresGenerado"] = periodo.Interes;
        row["Total"] = periodo.Total;
        row["Color"] = color;
        color = color == 1 ? 0 : 1;
        dt_ActasDetalle.Rows.Add(row);
      }

      Empresa empresa = mtdEmpresas.GetEmpresa(txt_CUIT.Text);

      reportes formReporte = new reportes();
      formReporte.dt = dt_ActasDetalle;
      formReporte.dt2 = mtdFilial.Get_DatosFilial();

      formReporte.Parametro1 = empresa.MAEEMP_RAZSOC.Trim();
      formReporte.Parametro2 = empresa.MEEMP_CUIT_STR;
      formReporte.Parametro3 = mtdFuncUtiles.generar_ceros(NumeroDeActa.ToString(), 6);
      formReporte.Parametro4 = _PreActa.Sum(x => x.Capital).ToString("N2");
      formReporte.Parametro5 = _PreActa.Sum(x => x.Interes).ToString("N2");
      formReporte.Parametro6 = _PreActa.Sum(x => x.Total).ToString("N2");
      formReporte.Parametro7 = "Original";
      formReporte.Parametro8 = " ";
      formReporte.Parametro9 = msk_Vencimiento.Text;
      formReporte.NombreDelReporte = "entrega_cupones.Reportes.rpt_ActaDetalle.rdlc";
      formReporte.Show();
    }

    private void ImprimirPlanDePago()
    {
     
      string tf = _PlanDePago.Sum(x => x.ImporteDeCuota).ToString("N2"); //(decimal) Math.Round(dt.AsEnumerable().Sum(r => r.Field<double>("ImporteDeCuota")), 2);
      mtdCobranzas.ImprimirPlanDePago(_PlanDePago, txt_RazonSocial.Text, txt_CUIT.Text, txt_persona.Text, txt_Total.Text, tf.ToString(), txt_NumeroDeActa.Text);

    }

    private void btn_Cancelar_Click(object sender, EventArgs e)
    {
      Close();
    }
  }
}
