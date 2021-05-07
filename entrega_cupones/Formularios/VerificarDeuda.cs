using entrega_cupones.Clases;
using entrega_cupones.Metodos;
using entrega_cupones.Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace entrega_cupones.Formularios
{
  public partial class VerificarDeuda : Form
  {
    List<EstadoDDJJ> _ddjj = new List<EstadoDDJJ>();
    List<mdlCuadroAmortizacion> _PlanDePago = new List<mdlCuadroAmortizacion>();
    List<EmpleadoAportePorPeriodo> _AporteEstadoSocio = new List<EmpleadoAportePorPeriodo>();

    public VerificarDeuda()
    {
      InitializeComponent();
    }
    public void EstadoEmpleado()
    {
      using (var Context = new lts_sindicatoDataContext())
      {
        var ListadoAportes = Context.ddjj.
         Select(row => new EmpleadoAportePorPeriodo
         {
           Cuit = row.CUIT_STR,
           Periodo = (DateTime)row.periodo,
           Apellido = "",
           Nombre = "",
           Dni = row.cuil.ToString(),
           Sueldo = (decimal)(row.impo + row.impoaux),
           AporteLey = (decimal)((row.impo + row.impoaux) * 0.02),
           AporteSocio = (decimal)((row.item2 == true) ? (row.impo + row.impoaux) * 0.02 : 0),
           Jornada = row.jorp == true ? "Parcial" : "Completa",
         }).OrderBy(x => x.Apellido);
        _AporteEstadoSocio.AddRange(ListadoAportes);
      }
    }
    private void VerificarDeuda_Load(object sender, EventArgs e)
    {
      // Maxihogar 30646757327
      // Las Malvinas 30566692887
      Icon = new Icon("C:\\SEC_Gestion\\Imagen\\icono.ico");
      dgv_ddjj.AutoGenerateColumns = false;
      dgv_PlanDePagos.AutoGenerateColumns = false;

      string MesDesde = mtdFuncUtiles.generar_ceros(DateTime.Today.AddYears(-5).Month.ToString(), 2);
      string AñoDesde = DateTime.Today.AddYears(-5).Year.ToString();
      string MesHasta = mtdFuncUtiles.generar_ceros(DateTime.Today.Month.ToString(), 2);
      string AñoHasta = DateTime.Today.Year.ToString();

      msk_Desde.Text = MesDesde + "/" + AñoDesde;
      msk_Hasta.Text = MesHasta + "/" + AñoHasta;
      msk_Vencimiento.Text = DateTime.Today.Date.AddDays(15).ToString();

      cbx_TipoDeInteres.SelectedIndex = 0;
      Cargar_cbxInspectores();

      //EstadoEmpleado();
    }
    private void Cargar_cbxInspectores()
    {
      cbx_Inspectores.DisplayMember = "Nombre";
      cbx_Inspectores.ValueMember = "Id";
      cbx_Inspectores.DataSource = mtdInspectores.Get_Inspectores();
    }

    private void BuscarEmpresa()
    {
      frm_buscar_empresa formBuscarEmpresa = new frm_buscar_empresa();
      formBuscarEmpresa.viene_desde = 6;
      AddOwnedForm(formBuscarEmpresa);
      formBuscarEmpresa.ShowDialog();

    }
    private void btn_BuscarEmpresa_Click(object sender, EventArgs e)
    {
      BuscarEmpresa();
    }
    private void btn_CalcularDeuda_Click(object sender, EventArgs e)
    {
      _ddjj.Clear();
      _ddjj = mtdEmpresas.ListadoDDJJT(
              txt_CUIT.Text,
              Convert.ToDateTime("01/" + msk_Desde.Text),
              Convert.ToDateTime("01/" + msk_Hasta.Text),
              Convert.ToDateTime(msk_Vencimiento.Text),
              cbx_TipoDeInteres.SelectedIndex,
              Convert.ToDecimal(txt_InteresDiario.Text)
              );

      dgv_ddjj.DataSource = _ddjj;

      PintarPerNoDec();
      CalcularTotales();

      if (_ddjj == null)
      {
        DesactivarBotones();
      }
      else
      {
        ActivarBotones();
        txt_DeudaInicial.Text = txt_Total.Text;
      }
    }
    private void CalcularTotales()
    {
      txt_Total.Text = Math.Round(_ddjj.Where(x => x.Acta == "").Sum(x => x.Total), 2).ToString("N2");
      txt_Pagado.Text = Math.Round(_ddjj.Where(x => x.Acta == "").Sum(x => x.ImporteDepositado), 2).ToString("N2");
      txt_Deuda.Text = Math.Round(_ddjj.Where(x => x.Acta == "").Sum(x => x.Capital), 2).ToString("N2");
      txt_TotalInteres.Text = Math.Round(_ddjj.Where(x => x.Acta == "").Sum(x => x.Interes), 2).ToString("N2");
      txt_PerNoDec.Text = _ddjj.Where(x => x.Acta == "").Count(x => x.PerNoDec == 1).ToString();
      txt_DeudaInicial.Text = txt_Total.Text;
      txt_Anticipo.Text = "";
      txt_DeudaPlan.Text = txt_Total.Text;
      VerPlanDePago();
    }
    private void PintarPerNoDec()
    {
      foreach (DataGridViewRow fila in dgv_ddjj.Rows)
      {
        if (Convert.ToInt32(fila.Cells["PerNoDec"].Value) == 1)
        {
          fila.DefaultCellStyle.BackColor = System.Drawing.Color.PaleVioletRed;
        }
      }
    }
    private void btn_EliminarFila_Click(object sender, EventArgs e)
    {
      EliminarFila();
    }
    private void EliminarFila()
    {
      if (MessageBox.Show("Esta Seguro que desea ELIMINAR el Periodo seleccionado?", "ATENCION", MessageBoxButtons.YesNo) == DialogResult.Yes)
      {
        int Index = dgv_ddjj.CurrentRow.Index;

        DateTime Periodo = Convert.ToDateTime(dgv_ddjj.CurrentRow.Cells["Periodo"].Value);
        int Rectificacion = Convert.ToInt32(dgv_ddjj.CurrentRow.Cells["Rectificacion"].Value);

        _ddjj.RemoveAll(x => x.Periodo == Periodo &&
        x.Rectificacion == Rectificacion);

        dgv_ddjj.DataSource = _ddjj.ToList();
        dgv_ddjj.CurrentCell = Index == 0 ? dgv_ddjj.Rows[Index].Cells[0] : dgv_ddjj.Rows[Index - 1].Cells[0];
        dgv_ddjj.Rows[0].Selected = false;

        CalcularTotales();
        PintarPerNoDec();
      }
    }
    private void ActivarBotones()
    {
      btn_EliminarFila.Enabled = true;
      btn_CopiarAnterior.Enabled = true;
      btn_CopiarSiguiente.Enabled = true;
      btn_EmitirActa.Enabled = true;
    }
    private void DesactivarBotones()
    {
      btn_EliminarFila.Enabled = false;
      btn_CopiarAnterior.Enabled = false;
      btn_CopiarSiguiente.Enabled = false;
      btn_EmitirActa.Enabled = false;
    }
    private void btn_CopiarAnterior_Click(object sender, EventArgs e)
    {
      CopiarPeriodo(false, dgv_ddjj.CurrentRow.Index - 1);
    }
    private void CopiarPeriodo(bool copiarSiguiente, int Index)
    {
      // decimal taza = Convert.ToDecimal(txt_Interes.Text);

      DateTime PeriodoACopiar = Convert.ToDateTime(dgv_ddjj.Rows[Index].Cells["Periodo"].Value);

      DateTime PeriodoActual = Convert.ToDateTime(dgv_ddjj.Rows[copiarSiguiente == true ? Index - 1 : Index + 1].Cells["Periodo"].Value);

      var FilaActual = _ddjj.FirstOrDefault(x => x.Periodo == PeriodoActual);
      var FilaACopiar = _ddjj.FirstOrDefault(x => x.Periodo == PeriodoACopiar);

      if (PeriodoACopiar.Month == 12 || PeriodoACopiar.Month == 6)
      {
        FilaActual.AporteLey = FilaACopiar.AporteLey - (FilaACopiar.AporteLey * Convert.ToDecimal(0.50));
        FilaActual.AporteSocio = FilaACopiar.AporteSocio - (FilaACopiar.AporteSocio * Convert.ToDecimal(0.50));
        FilaActual.Capital = FilaActual.AporteLey + FilaActual.AporteSocio;
        FilaActual.Interes = Math.Round(mtdEmpresas.CalcularInteres(null, FilaActual.Periodo, FilaActual.Capital, Convert.ToDateTime(msk_Vencimiento.Text), cbx_TipoDeInteres.SelectedIndex, Convert.ToDecimal(txt_InteresDiario.Text)), 2);
        FilaActual.TotalSueldoEmpleados = FilaACopiar.TotalSueldoEmpleados - (FilaACopiar.TotalSueldoEmpleados * Convert.ToDecimal(0.50));
        FilaActual.TotalSueldoSocios = FilaACopiar.TotalSueldoSocios - (FilaACopiar.TotalSueldoSocios * Convert.ToDecimal(0.50));
      }
      else
      {
        if (PeriodoActual.Month == 6 || PeriodoActual.Month == 12)
        {
          FilaActual.AporteLey = FilaACopiar.AporteLey + (FilaACopiar.AporteLey * Convert.ToDecimal(0.50));
          FilaActual.AporteSocio = FilaACopiar.AporteSocio + (FilaACopiar.AporteSocio * Convert.ToDecimal(0.50));
          FilaActual.Capital = FilaActual.AporteLey + FilaActual.AporteSocio;
          FilaActual.Interes = Math.Round(mtdEmpresas.CalcularInteres(null, FilaActual.Periodo, FilaActual.Capital, Convert.ToDateTime(msk_Vencimiento.Text), cbx_TipoDeInteres.SelectedIndex, Convert.ToDecimal(txt_InteresDiario.Text)), 2);
          FilaActual.TotalSueldoEmpleados = FilaACopiar.TotalSueldoEmpleados + (FilaACopiar.TotalSueldoEmpleados * Convert.ToDecimal(0.50));
          FilaActual.TotalSueldoSocios = FilaACopiar.TotalSueldoSocios + (FilaACopiar.TotalSueldoSocios * Convert.ToDecimal(0.50));
        }
        else
        {
          FilaActual.AporteLey = FilaACopiar.AporteLey;
          FilaActual.AporteSocio = FilaACopiar.AporteSocio;
          FilaActual.Capital = FilaActual.AporteLey + FilaActual.AporteSocio;
          FilaActual.Interes = Math.Round(mtdEmpresas.CalcularInteres(null, FilaActual.Periodo, FilaActual.Capital, Convert.ToDateTime(msk_Vencimiento.Text), cbx_TipoDeInteres.SelectedIndex, Convert.ToDecimal(txt_InteresDiario.Text)), 2);
          FilaActual.TotalSueldoEmpleados = FilaACopiar.TotalSueldoEmpleados;
          FilaActual.TotalSueldoSocios = FilaACopiar.TotalSueldoSocios;
        }
      }


      FilaActual.Rectificacion = 0;
      FilaActual.FechaDePago = null;//Convert.ToDateTime("01/01/0001");
      FilaActual.ImporteDepositado = Convert.ToDecimal(0.00);
      FilaActual.InteresCobrado = Math.Round(Convert.ToDecimal(0.00), 2);
      FilaActual.Socios = FilaACopiar.Socios;
      FilaActual.Empleados = FilaACopiar.Empleados;
      FilaActual.DiasDeMora = mtdEmpresas.CalcularDias(PeriodoActual, Convert.ToDateTime(msk_Vencimiento.Text));
      FilaActual.Total = FilaActual.Interes + FilaActual.Capital;
      FilaActual.PerNoDec = 0;
      dgv_ddjj.DataSource = _ddjj.ToList();

      CalcularTotales();
      PintarPerNoDec();

    }
    private void dgv_ddjj_SelectionChanged(object sender, EventArgs e)
    {
      btn_CopiarAnterior.Enabled = dgv_ddjj.CurrentRow.Index == 0 ? false : true;
      btn_CopiarSiguiente.Enabled = dgv_ddjj.CurrentRow.Index == dgv_ddjj.Rows.Count - 1 ? false : true;
      btn_IngresoManual.Enabled = Convert.ToInt32(dgv_ddjj.CurrentRow.Cells["PerNoDec"].Value) == 1 ? true : false; // dgv_ddjj.CurrentRow.Cells["PerNoDec"].Value.ToString() == "1";
      //MostrarEmpleados();
    }
    private void btn_CopiarSiguiente_Click(object sender, EventArgs e)
    {
      CopiarPeriodo(true, dgv_ddjj.CurrentRow.Index + 1);
    }
    private void btn_ImprimirDeuda_Click(object sender, EventArgs e)
    {
      DS_cupones ds = new DS_cupones();
      DataTable dt_ActasDetalle = ds.ActasDetalle;

      dt_ActasDetalle.Clear();
      int Color = 0; ;
      foreach (var periodo in _ddjj.Where(x => x.Acta == ""))
      {
        Color += 1;
        DataRow row = dt_ActasDetalle.NewRow();
        row["NumeroDeActa"] = 0;
        row["Periodo"] = periodo.Periodo;
        row["CantidadDeEmpleados"] = periodo.Empleados;
        row["CantidadSocios"] = periodo.Socios;
        row["TotalSueldoEmpleados"] = periodo.TotalSueldoEmpleados;
        row["TotalSueldoSocios"] = periodo.TotalSueldoSocios;
        row["TotalAporteEmpleados"] = periodo.AporteLey;
        row["TotalAporteSocios"] = periodo.AporteSocio;
        row["FechaDePago"] = periodo.FechaDePago == null ? "" : periodo.FechaDePago.Value.Date.ToShortDateString();
        row["ImporteDepositado"] = periodo.ImporteDepositado;
        row["DiasDeMora"] = periodo.DiasDeMora;
        row["DeudaGenerada"] = periodo.Capital;
        row["InteresGenerado"] = periodo.Interes;
        row["Total"] = periodo.Total;
        row["Color"] = Color;
        row["Logo"] = mtdConvertirImagen.ImageToByteArray(Image.FromFile("C:\\SEC_Gestion\\Imagen\\Logo_reporte.png"));
        dt_ActasDetalle.Rows.Add(row);
      }

      Empresa empresa = mtdEmpresas.GetEmpresa(txt_CUIT.Text);

      reportes formReporte = new reportes();

      formReporte.dt = dt_ActasDetalle;
      formReporte.dt2 = mtdFilial.Get_DatosFilial();

      formReporte.Parametro1 = empresa.MAEEMP_RAZSOC.Trim();
      formReporte.Parametro2 = empresa.MEEMP_CUIT_STR;
      formReporte.Parametro3 = "-";
      formReporte.Parametro4 = Math.Round(_ddjj.Where(x => x.Acta == "").Sum(x => x.Capital), 2).ToString("N2");
      formReporte.Parametro5 = Math.Round(_ddjj.Where(x => x.Acta == "").Sum(x => x.Interes), 2).ToString("N2");
      formReporte.Parametro6 = Math.Round(_ddjj.Where(x => x.Acta == "").Sum(x => x.Total), 2).ToString("N2");
      formReporte.Parametro8 = " ";
      formReporte.Parametro9 = msk_Vencimiento.Text;
      formReporte.Parametro10 = txt_PerNoDec.Text;

      formReporte.NombreDelReporte = "entrega_cupones.Reportes.rpt_VerificacionDeDeuda.rdlc";
      formReporte.Show();
    }
    private void btn_IngresoManual_Click(object sender, EventArgs e)
    {
      PasarDatosIngresoManual();
    }
    private void PasarDatosIngresoManual()
    {
      frm_IngresoManualDDJJ formIngresoManual = new frm_IngresoManualDDJJ();

      formIngresoManual._FechaDeVencimiento = Convert.ToDateTime(msk_Vencimiento.Text);
      formIngresoManual._TipoDeInteres = cbx_TipoDeInteres.SelectedIndex;
      formIngresoManual._TazaInteres = Convert.ToDecimal(txt_InteresDiario.Text);
      //formIngresoManual._TazaInteres = Convert.ToDecimal(txt_Interes.Text);

      formIngresoManual.txt_Periodo.Text = Convert.ToDateTime(dgv_ddjj.CurrentRow.Cells["Periodo"].Value).ToShortDateString();
      formIngresoManual.txt_FechaDePago.Text = msk_Vencimiento.Text;
      //formIngresoManual.txt_Rectificacion.Text = dgv_ddjj.CurrentRow.Cells["Rectificacion"].Value.ToString();
      //formIngresoManual.txt_AporteLey.Text = dgv_ddjj.CurrentRow.Cells["AporteLey"].Value.ToString();
      //formIngresoManual.txt_AporteSocio.Text = dgv_ddjj.CurrentRow.Cells["AporteSocio"].Value.ToString();
      //formIngresoManual.txt_Depositado.Text = dgv_ddjj.CurrentRow.Cells["ImporteDepositado"].Value.ToString();
      formIngresoManual.txt_DiasDeMora.Text = dgv_ddjj.CurrentRow.Cells["DiasDeMora"].Value.ToString();
      formIngresoManual.txt_CantidadEmpleados.Text = dgv_ddjj.CurrentRow.Cells["Empleados"].Value.ToString();
      formIngresoManual.txt_CantidadSocios.Text = dgv_ddjj.CurrentRow.Cells["Socios"].Value.ToString();
      formIngresoManual.txt_TotalAporte.Text = dgv_ddjj.CurrentRow.Cells["Capital"].Value.ToString();
      formIngresoManual.txt_Intereses.Text = dgv_ddjj.CurrentRow.Cells["Interes"].Value.ToString();
      formIngresoManual.txt_Total.Text = dgv_ddjj.CurrentRow.Cells["Total"].Value.ToString();

      AddOwnedForm(formIngresoManual);
      formIngresoManual.ShowDialog();

      GuardarIngresoManual();

      CalcularTotales();

      dgv_ddjj.DataSource = _ddjj.ToList();

      PintarPerNoDec();

    }
    private void GuardarIngresoManual()
    {
      DateTime Periodo = Convert.ToDateTime(dgv_ddjj.CurrentRow.Cells["Periodo"].Value);
      var registro = _ddjj.FirstOrDefault(x => x.Periodo == Periodo);

      registro.Periodo = (DateTime)dgv_ddjj.CurrentRow.Cells["Periodo"].Value;
      registro.Rectificacion = (int)dgv_ddjj.CurrentRow.Cells["Rectificacion"].Value;
      registro.AporteLey = (decimal)dgv_ddjj.CurrentRow.Cells["AporteLey"].Value;
      registro.AporteSocio = (decimal)dgv_ddjj.CurrentRow.Cells["AporteSocio"].Value;
      registro.ImporteDepositado = (decimal)dgv_ddjj.CurrentRow.Cells["ImporteDepositado"].Value;
      registro.DiasDeMora = (int)dgv_ddjj.CurrentRow.Cells["DiasDeMora"].Value;
      registro.Empleados = (int)dgv_ddjj.CurrentRow.Cells["Empleados"].Value;
      registro.Socios = (int)dgv_ddjj.CurrentRow.Cells["Socios"].Value;
      registro.Capital = (decimal)dgv_ddjj.CurrentRow.Cells["Capital"].Value;
      registro.Interes = (decimal)dgv_ddjj.CurrentRow.Cells["Interes"].Value;
      registro.Total = (decimal)dgv_ddjj.CurrentRow.Cells["Total"].Value;

    }
    private void dgv_ddjj_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {

    }
    private void MostrarEmpleados()
    {
      DateTime Periodo = Convert.ToDateTime(dgv_ddjj.CurrentRow.Cells["Periodo"].Value);

      int Rectificacion = Convert.ToInt32(dgv_ddjj.CurrentRow.Cells["Rectificacion"].Value);

      frm_EmpleadoEstadoDDJJ frmEmpleadosDetalle = new frm_EmpleadoEstadoDDJJ();
      frmEmpleadosDetalle.txt_CUIT.Text = txt_CUIT.Text;
      frmEmpleadosDetalle.txt_Empresa.Text = txt_BuscarEmpesa.Text;
      frmEmpleadosDetalle.Periodo = Periodo;
      frmEmpleadosDetalle.Rectificacion = Rectificacion;
      frmEmpleadosDetalle.txt_Periodo.Text = Periodo.ToString("dd/MM/yyyy");

      frmEmpleadosDetalle.Show();
    }
    private void btn_PeriodoDetalle_Click(object sender, EventArgs e)
    {
      MostrarEmpleados();
    }
    private void btn_EmitirActa_Click(object sender, EventArgs e)
    {
      if (_ddjj.Where(x => x.Acta != "").Count() > 0)
      {
        MessageBox.Show("No se puede emitir el acta por que hay periodos que pertencen a otra acta. Por favor corregir el intervalo de fechas.", "ATENCION");
      }
      else
      {
        if (_ddjj.Where(x => x.PerNoDec == 1).Count() > 0)
        {
          MessageBox.Show("No se puede emitir el acta por que hay periodos que no estan declarados. Por favor verificar periodos.", "ATENCION");
        }
        else
        {
          EmitirActa();
        }
      }

    }
    private void EmitirActa()
    {
      frm_GenerarActa formActasGenerar = new frm_GenerarActa();
      formActasGenerar._PreActa = _ddjj;
      formActasGenerar._PlanDePago = _PlanDePago;
      formActasGenerar.txt_CUIT.Text = txt_CUIT.Text;
      formActasGenerar.txt_RazonSocial.Text = txt_BuscarEmpesa.Text;
      formActasGenerar.msk_Desde.Text = msk_Desde.Text;
      formActasGenerar.msk_Hasta.Text = msk_Hasta.Text;
      formActasGenerar.msk_Vencimiento.Text = msk_Vencimiento.Text;
      formActasGenerar.msk_LibroSueldoDesde.Text = msk_Desde.Text;
      formActasGenerar.msk_LibroSueldoHasta.Text = msk_Hasta.Text;
      formActasGenerar.msk_ReciboSueldoDesde.Text = msk_Desde.Text;
      formActasGenerar.msk_ReciboSueldoHasta.Text = msk_Hasta.Text;
      formActasGenerar.msk_BoletaDepositoDesde.Text = msk_Desde.Text;
      formActasGenerar.msk_BoletaDepositoHasta.Text = msk_Hasta.Text;
      formActasGenerar.txt_Total.Text = txt_Total.Text;
      formActasGenerar.txt_Interes.Text = txt_Interes.Text;
      formActasGenerar.txt_InteresDiario.Text = txt_InteresDiario.Text;
      formActasGenerar.txt_Cuotas.Text = txt_CantidadDeCuotas.Text;
      formActasGenerar.txt_ImporteDeCuota.Text = txt_ImporteDeCuota.Text;
      formActasGenerar.Show();
    }
    private void btn_VerPlanDePago_Click(object sender, EventArgs e)
    {
      VerPlanDePago();
    }
    private void VerPlanDePago()
    {
      if (string.IsNullOrEmpty(txt_InteresPlan.Text) || string.IsNullOrWhiteSpace(txt_InteresPlan.Text) || txt_InteresPlan.Text == "0")
      {
        MessageBox.Show("El Interes debe ser mator que Cero.", "ATENCION !!!!!!");
        txt_InteresPlan.Focus();
      }
      else
      {
        if (txt_CantidadDeCuotas.Text == "1")
        {
          txt_Anticipo.Text = "0";
          TraerPlanDePago();
        }

        if (txt_CantidadDeCuotas.Text != "1")
        {
          if (txt_CantidadDeCuotas.Text.Trim() == "")
          {
            MessageBox.Show("Debe Ingresar al menos una cuota.", "ATENCION !!!!!!");
            txt_CantidadDeCuotas.Focus();
          }
          else
          {
            TraerPlanDePago();
          }
        }
      }
    }
    private void TraerPlanDePago()
    {
      //obtengo el importe de la cuota, si la cuota es 1 entonces el interes es 0% sino se aplica el 3% lo mismo para el cuadro de amortizacion
      txt_ImporteDeCuota.Text = mtdCobranzas.ObtenerValorDeCuota(
        Convert.ToDecimal(txt_DeudaPlan.Text),
         //txt_CantidadDeCuotas.Text == "1" ? 0 : 0.03,
         txt_CantidadDeCuotas.Text == "1" ? 0 : Convert.ToDecimal(txt_InteresPlan.Text) / 100,
        Convert.ToInt32(txt_CantidadDeCuotas.Text)
        ).ToString("N2");

      _PlanDePago = mtdCobranzas.ObtenerCuadroDeAmortizacion(
        Convert.ToDouble(txt_DeudaPlan.Text),
        txt_CantidadDeCuotas.Text == "1" ? 0 : 0.03,
        Convert.ToInt32(txt_CantidadDeCuotas.Text),
        Convert.ToDouble(txt_ImporteDeCuota.Text),
        Convert.ToDouble(txt_Anticipo.Text),
        Convert.ToDateTime(dtp_VencAnticipo.Value),
        Convert.ToDateTime(dtp_VencCuota.Value.AddMonths(1)),
        Convert.ToDecimal(txt_DeudaInicial.Text)
        );

      dgv_PlanDePagos.DataSource = _PlanDePago;

    }
    private void txt_Anticipo_TextChanged(object sender, EventArgs e)
    {
      if (txt_Anticipo.Text != "")
      {
        txt_DeudaPlan.Text = (Convert.ToDouble(txt_DeudaInicial.Text) - Convert.ToDouble(txt_Anticipo.Text)).ToString("N2");
        //VerPlanDePago();
      }
      else
      {
        txt_Anticipo.Text = "0";
      }

    }
    private void txt_CantidadDeCuotas_TextChanged(object sender, EventArgs e)
    {
      if (txt_CantidadDeCuotas.Text == "0")
      {
        txt_CantidadDeCuotas.Text = "1";
      }

    }
    private void btn_ImprimirPlanDePago2_Click(object sender, EventArgs e)
    {
      ImprimirPlanDePago();
    }
    private void ImprimirPlanDePago()
    {
      string tf = _PlanDePago.Sum(x => x.ImporteDeCuota).ToString("N2"); //(decimal) Math.Round(dt.AsEnumerable().Sum(r => r.Field<double>("ImporteDeCuota")), 2);
      mtdCobranzas.ImprimirPlanDePago(_PlanDePago, txt_BuscarEmpesa.Text, txt_CUIT.Text, "", txt_Total.Text, tf.ToString(), "");
    }
    private void btn_VerRanking_Click(object sender, EventArgs e)
    {
      using (var context = new lts_sindicatoDataContext())
      {
        List<mdlDeudaParaRanking> _dpr = new List<mdlDeudaParaRanking>();

        List<mdlDDJJpr> _ddjjpr = new List<mdlDDJJpr>();

        var dj3 = from a in context.ddjjt
                  where a.periodo >= Convert.ToDateTime("01/" + msk_Desde.Text) &&
                  a.periodo <= Convert.ToDateTime("01/" + msk_Hasta.Text) &&
                  a.acta == 0 // && a.CUIT_STR == "27172155753"
                  select new mdlDDJJpr
                  {
                    cuit = a.CUIT_STR,
                    importe = Convert.ToDecimal(a.titem1 + a.titem2),
                    depositado = Convert.ToDecimal(a.impban1),
                    periodo = Convert.ToDateTime(a.periodo),
                    rectificacion = Convert.ToInt32(a.rect)
                    //acta = mtdEmpresas.GetNroDeActa(Convert.ToDateTime(a.periodo), a.CUIT_STR)
                  };
        _ddjjpr.AddRange(dj3);

        var agrupado = from a in _ddjjpr group a by new { a.cuit, a.periodo } into grupo where grupo.Count() > 1 select grupo; // _ddjjpr.GroupBy(x => x.periodo).Where(x => x.Count() > 1);

        foreach (var item in agrupado)
        {
          foreach (var registro in item)
          {
            if (registro.depositado == 0)
            {
              _ddjjpr.RemoveAll(x => x.periodo == registro.periodo && x.rectificacion == registro.rectificacion & x.cuit == registro.cuit);
            }
          }
        }

        var CUITAgrupado = from djs in _ddjjpr
                           group djs by djs.cuit
                           into grupoCUIT
                           select new mdlDeudaParaRanking
                           {
                             Cuit = grupoCUIT.Key,
                             Empresa = mtdEmpresas.GetEmpresa(grupoCUIT.Key) != null ? mtdEmpresas.GetEmpresa(grupoCUIT.Key).MAEEMP_RAZSOC.Trim().ToString() : "desconocida",
                             Deuda = grupoCUIT.Sum(x => x.importe) - grupoCUIT.Sum(x => x.depositado),
                           };

        dgv_Ranking.DataSource = CUITAgrupado.OrderByDescending(x => x.Deuda).ToList();
      }
    }
    private void cbx_TipoDeInteres_SelectedIndexChanged(object sender, EventArgs e)
    {
      // SelectedIndex => 0 = Manual ; 1 = AFIP
      if (cbx_TipoDeInteres.SelectedIndex == 1)
      {
        txt_Interes.Enabled = false;
        txt_InteresDiario.Enabled = false;
      }
      else
      {
        txt_Interes.Enabled = true;
        txt_InteresDiario.Enabled = true;
      }
    }
    private void txt_Interes_TextChanged(object sender, EventArgs e)
    {
      if (txt_Interes.Text == "")
      {
        txt_Interes.Text = "0";
      }
      else
      {
        txt_InteresDiario.Text = Math.Round((Convert.ToDecimal(txt_Interes.Text) / 30), 6).ToString();
      }
    }

    private void btn_CalcularDifAporteSocio_Click(object sender, EventArgs e)
    {

    }

    private void cbx_Inspectores_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void btn_ConfirmAsignacion_Click(object sender, EventArgs e)
    {
      ConfirmarAsignacion();

    }

    private void ConfirmarAsignacion()
    {
      //PREGUNTO SI YA ESTA ASIGNADA ESTA EMPRESA Y TIENE UNA VERIFICACION DE DEUDA SIN CERRAR/CANCELAR/ACTA
      if (mtdVDCabecera.YaEstaAsignada(txt_CUIT.Text.Trim().ToString()) == 0)
      {
        int InspectorVDDId = mtdInspectorVerificacion.InsertInspectorVerificacion(Convert.ToInt32(cbx_Inspectores.SelectedValue), txt_CUIT.Text.Trim());
        int CantidadEmpleados = _ddjj.Where(x => x.Periodo == _ddjj.Max(y => y.Periodo)).FirstOrDefault().Empleados;

        mtdVDCabecera.InsertVerificacionDeudaCabecera(
          InspectorVDDId,
          txt_CUIT.Text.Trim().ToString(),
          DateTime.Now,
          Convert.ToDateTime(msk_Desde.Text),
          Convert.ToDateTime(msk_Hasta.Text),
          Convert.ToDateTime(msk_Vencimiento.Text),
          0,
          Convert.ToDecimal(txt_Deuda.Text),         //Math.Round(_ddjj.Sum(x => x.Capital), 2)
          Convert.ToDecimal(txt_TotalInteres.Text),
          Convert.ToDecimal(txt_Total.Text),
          0,
         CantidadEmpleados,
         Convert.ToDecimal(txt_Interes.Text),
         Convert.ToDecimal(txt_InteresDiario.Text)
          );

        int VD_CaberecaId = mtdVDCabecera.GetVD_CabeceraId();

        mtdVDDetalle.InsertVerificacionDetalle(_ddjj, VD_CaberecaId, true);
      }
      else
      {
        MessageBox.Show("Ya tiene asignada una verificacion de Deuda");
      }
    }

    private void btn_Actualizar_VD_Click(object sender, EventArgs e)
    {
      Actualizar_VD();
    }

    private void Actualizar_VD()
    {
      int CantidadDeActas = _ddjj.Count(x => x.Acta != "");
      if (CantidadDeActas == 0)
      {
        int VD_CabeceraId = mtdVDCabecera.YaEstaAsignada(txt_CUIT.Text.Trim().ToString());
        if (VD_CabeceraId > 0)
        {
          //mtdEmpresas.GetNroVerifDeuda(txt_CUIT.Text.Trim().ToString(),)
          mtdVDDetalle.InsertVerificacionDetalle(_ddjj, VD_CabeceraId, false);

        }
        else
        {
          MessageBox.Show("Esta verificacion de Deuda, No tiene asignado ningun Isnpector");
        }
      }
      else
      {
        MessageBox.Show("Debe Excluir los periodos que pertescan a un Acta ");
      }
    }
  }
}

