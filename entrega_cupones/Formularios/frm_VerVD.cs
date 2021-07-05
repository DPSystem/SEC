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
  public partial class frm_VerVD : Form
  {
    public List<mdlVDDetalle> _VDDetalle = new List<mdlVDDetalle>();

    public List<EstadoDDJJ> _ddjj = new List<EstadoDDJJ>();

    public int _VDId;

    public List<mdlDDJJEmpleado> _DDJJEmpleados = new List<mdlDDJJEmpleado>();

    public frm_VerVD()
    {
      InitializeComponent();
    }

    private void frm_VerVD_Load(object sender, EventArgs e)
    {
      dgv_VD.AutoGenerateColumns = false;
      dgv_DetallePeriodo.AutoGenerateColumns = false;
      dgv_ReciboSueldo.AutoGenerateColumns = false;
      VD_Mostrar();
      //Cargar_DDJJEmpleado();
    }

    private void VD_Mostrar()
    {
      _VDDetalle = mtdVDDetalle.Get_VDD(_VDId);
      dgv_VD.DataSource = _VDDetalle;
      BindingSource bindingSource = new BindingSource();
      bindingSource.DataSource = _VDDetalle;

      //dataGridView1.DataSource = bindingSource;
      Simulacion();
    }

    private void Cargar_DDJJEmpleado()
    {
      _DDJJEmpleados.Clear();
      _DDJJEmpleados = mtdEmpleados.ListadoEmpleadoAporte
        (
        txt_CUIT.Text,
        Convert.ToDateTime(dgv_VD.CurrentRow.Cells["Periodo"].Value),
        Convert.ToInt32(dgv_VD.CurrentRow.Cells["Rectificacion"].Value)
        );

     
      dgv_DetallePeriodo.DataSource = _DDJJEmpleados.ToList();
    }

    private void CalcularTotales()
    {
      //txt_Total.Text = Math.Round(_VDDetalle.Sum(x => x.Total), 2).ToString("N2");
      //txt_Pagado.Text = Math.Round(_VDDetalle.Sum(x => x.ImporteDepositado), 2).ToString("N2");
      //txt_Deuda.Text = Math.Round(_VDDetalle.Sum(x => x.DeudaGenerada), 2).ToString("N2");
      //txt_TotalInteres.Text = Math.Round(_VDDetalle.Sum(x => x.InteresGenerado), 2).ToString("N2");
      //txt_PerNoDec.Text = _VDDetalle.Count(x => x.PerNoDec == 1).ToString();
      //txt_DeudaInicial.Text = txt_Total.Text;
      //txt_Anticipo.Text = "";
      //txt_DeudaPlan.Text = txt_Total.Text;


      decimal InteresResarcitorio = 0;
      InteresResarcitorio = _VDDetalle.Where(x => x.NumeroDeActa == 0 && x.DiasDeMora > 0 && x.FechaDePago != null).Sum(x => x.DeudaGenerada);
      txt_Total.Text = Math.Round(_VDDetalle.Where(x => x.NumeroDeActa == 0).Sum(x => x.Total), 2).ToString("N2");
      txt_Pagado.Text = Math.Round(_VDDetalle.Where(x => x.NumeroDeActa == 0).Sum(x => x.ImporteDepositado), 2).ToString("N2");
      txt_Deuda.Text = Math.Round(_VDDetalle.Where(x => x.NumeroDeActa == 0 && x.DiasDeMora > 0 && x.FechaDePago == null).Sum(x => x.DeudaGenerada), 2).ToString("N2");
      txt_TotalInteres.Text = Math.Round(_VDDetalle.Where(x => x.NumeroDeActa == 0).Sum(x => x.InteresGenerado) + InteresResarcitorio, 2).ToString("N2");
      txt_PerNoDec.Text = _VDDetalle.Where(x => x.NumeroDeActa == 0).Count(x => x.PerNoDec == 1).ToString();
      txt_DeudaInicial.Text = txt_Total.Text;
      txt_Anticipo.Text = "";
      txt_DeudaPlan.Text = txt_Total.Text;

    }

    private void btn_CalcularDeuda_Click(object sender, EventArgs e)
    {
      Simulacion();
    }

    private void Simulacion()
    {
      _VDDetalle = mtdVDDetalle.VD_Simulacion(_VDDetalle,
                    txt_CUIT.Text,
                    Convert.ToDateTime("01/" + msk_Desde.Text),
                    Convert.ToDateTime("01/" + msk_Hasta.Text),
                    Convert.ToDateTime(msk_Vencimiento.Text),
                    cbx_TipoDeInteres.SelectedIndex,
                    Convert.ToDecimal(txt_InteresDiario.Text),
                    _VDId
                    );
      dgv_VD.DataSource = _VDDetalle;
      CalcularTotales();
    }

    private void btn_Actualizar_VD_Click(object sender, EventArgs e)
    {
      using (var context = new lts_sindicatoDataContext())
      {
        foreach (var periodo in _VDDetalle)
        {
          var VDDetalle = context.VD_Detalle.Where(x => x.Id == periodo.Id).SingleOrDefault();
          DateTime? FechaDePago = null;
          if (FechaDePago != null)
          {
            Convert.ToDateTime(periodo.FechaDePago);
          }

          VDDetalle.Periodo = Convert.ToDateTime(periodo.Periodo);
          VDDetalle.Rectificacion = periodo.Rectificacion;
          VDDetalle.CantidadEmpleados = periodo.CantidadEmpleados;
          VDDetalle.CantidadSocios = periodo.CantidadSocios;
          VDDetalle.TotalSueldoEmpleados = periodo.TotalSueldoEmpleados;
          VDDetalle.TotalSueldoSocios = periodo.TotalSueldoSocios;
          VDDetalle.TotalAporteEmpleados = periodo.TotalAporteEmpleados;
          VDDetalle.TotalAporteSocios = periodo.TotalAporteSocios;
          VDDetalle.FechaDePago = FechaDePago;//periodo.FechaDePago == null ? null : Convert.ToDateTime(periodo.FechaDePago);
          VDDetalle.ImporteDepositado = periodo.ImporteDepositado;
          VDDetalle.DiasDeMora = periodo.DiasDeMora;
          VDDetalle.DeudaGenerada = periodo.DeudaGenerada;
          VDDetalle.InteresGenerado = periodo.InteresGenerado;
          VDDetalle.Total = periodo.Total;
          VDDetalle.PerNoDec = periodo.PerNoDec;
          VDDetalle.ActaId = 0;
          VDDetalle.NumeroDeActa = 0;
          VDDetalle.Estado = 0;
          context.SubmitChanges();
        };

        // Actualizo la tabla de VD_Inspector
        var UpdateVD = (from a in context.VD_Inspector where a.Id == _VDId select a).SingleOrDefault();
        UpdateVD.Desde = Convert.ToDateTime(msk_Desde.Text);
        UpdateVD.Hasta = Convert.ToDateTime(msk_Hasta.Text);
        UpdateVD.FechaVenc = Convert.ToDateTime(msk_Vencimiento.Text);
        UpdateVD.TipoInteres = cbx_TipoDeInteres.SelectedIndex;
        UpdateVD.InteresMensual = Convert.ToDecimal(txt_Interes.Text);
        UpdateVD.InteresDiario = Convert.ToDecimal(txt_InteresDiario.Text);
        UpdateVD.Capital = Convert.ToDecimal(txt_Deuda.Text);         //Math.Round(_ddjj.Sum(x => x.Capital), 2)
        UpdateVD.Interes = Convert.ToDecimal(txt_TotalInteres.Text);
        UpdateVD.Total = Convert.ToDecimal(txt_Total.Text);
        context.SubmitChanges();
         
      }
    }

    private void txt_Interes_TextChanged(object sender, EventArgs e)
    {
      if (txt_Interes.Text == "")
      {
        txt_Interes.Text = "0";
      }
      txt_InteresDiario.Text = mtdIntereses.CalcularInteresDiario(txt_Interes.Text);
    }

    private void btn_CopiarSiguiente_Click(object sender, EventArgs e)
    {
      CopiarPeriodo(true);
    }

    private void CopiarPeriodo(bool CopiarSiguiente)
    {
      // la Fila Actual y obtengo el Id del periodo actual
      int Index = dgv_VD.CurrentRow.Index;
      int VDId_Actual = Convert.ToInt32(dgv_VD.CurrentRow.Cells["Id"].Value);

      // VDD_Id para buscar en _VDDetalle y asi obtener el Id a copiar y lo guardo en la Variable PeriodoACopiar
      int VD_Id = Convert.ToInt32(dgv_VD.Rows[CopiarSiguiente == true ? Index + 1 : Index - 1].Cells["Id"].Value);
      mdlVDDetalle ACopiar = _VDDetalle.FirstOrDefault(x => x.Id == VD_Id);

      decimal TotalSueldoEmpleados = ACopiar.Periodo.Value.Month == 12 || ACopiar.Periodo.Value.Month == 6 ? (ACopiar.TotalAporteEmpleados / 3) * 2 : ACopiar.TotalAporteEmpleados;
      // Busco en _VDDetalle el periodo a modificar 
      mdlVDDetalle AModificar = _VDDetalle.FirstOrDefault(x => x.Id == VDId_Actual);

      //Comienzo a Copiar desde la Variable PeriodoACopiar a la variable PeriodoAModificar
      
      AModificar.TotalSueldoEmpleados = CalcularDifAguinaldo(Convert.ToDateTime(ACopiar.Periodo), ACopiar.TotalAporteEmpleados, Convert.ToDateTime(AModificar.Periodo));
      AModificar.TotalSueldoSocios = CalcularDifAguinaldo(Convert.ToDateTime(ACopiar.Periodo), ACopiar.TotalAporteEmpleados, Convert.ToDateTime(AModificar.Periodo));
      AModificar.TotalAporteEmpleados = CalcularDifAguinaldo(Convert.ToDateTime(ACopiar.Periodo), ACopiar.TotalAporteEmpleados, Convert.ToDateTime(AModificar.Periodo));
      AModificar.TotalAporteSocios = CalcularDifAguinaldo(Convert.ToDateTime(ACopiar.Periodo), ACopiar.TotalAporteSocios, Convert.ToDateTime(AModificar.Periodo));
      AModificar.DiasDeMora = mtdEmpresas.CalcularDias(Convert.ToDateTime(AModificar.Periodo), Convert.ToDateTime(msk_Vencimiento.Text));
      AModificar.CantidadEmpleados = ACopiar.CantidadEmpleados;
      AModificar.CantidadSocios = ACopiar.CantidadSocios;
      AModificar.DeudaGenerada = AModificar.TotalAporteSocios + AModificar.TotalAporteSocios;
      AModificar.InteresGenerado = Math.Round(mtdEmpresas.CalcularInteres(null, Convert.ToDateTime(AModificar.Periodo), AModificar.DeudaGenerada, Convert.ToDateTime(msk_Vencimiento.Text), cbx_TipoDeInteres.SelectedIndex, Convert.ToDecimal(txt_InteresDiario.Text)), 2);
      AModificar.Total = AModificar.DeudaGenerada + AModificar.InteresGenerado;
      Simulacion();

    }

    public decimal CalcularDifAguinaldo(DateTime PerioACopiar, decimal Importe, DateTime PeriodoAModificar)
    {
      if (PerioACopiar.Month == 12 || PerioACopiar.Month == 6)
      {
        Importe = (Importe / 3) * 2;
      }

      if (PeriodoAModificar.Month == 6 || PeriodoAModificar.Month == 12)
      {
        Importe = (Importe * Convert.ToDecimal("0.50")) + Importe;
      }
      return Importe;
    }

    private void btn_CopiarAnterior_Click(object sender, EventArgs e)
    {
      CopiarPeriodo(false);
    }

    private void dgv_VD_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {

    }

    private void dgv_VD_SelectionChanged(object sender, EventArgs e)
    {
      btn_CopiarAnterior.Enabled = dgv_VD.CurrentRow.Index == 0 ? false : true;
      btn_CopiarSiguiente.Enabled = dgv_VD.CurrentRow.Index == dgv_VD.Rows.Count - 1 ? false : true;
      btn_IngresoManual.Enabled = Convert.ToInt32(dgv_VD.CurrentRow.Cells["PerNoDec"].Value) == 1 ? true : false;
      Cargar_DDJJEmpleado();

    }

    private void MostrarDetalleDDJJ()
    {
      if (_DDJJEmpleados.Count > 0)
      {
        // jubilacion y ley 19032 sobre base de Basico + antiguedad + presentismo
        // (obra Social, SEC Ley , SEC Socio, FAECyS) sobre base de (Basico + antiguedad + presentismo + ANR1 + ACNR2)

        int index = dgv_DetallePeriodo.CurrentRow.Index ;

        txt_Categoria.Text = _DDJJEmpleados[index].Categoria;
        txt_Antigue.Text = _DDJJEmpleados[index].Antiguedad.ToString();
        txt_Jornada.Text = _DDJJEmpleados[index].Jornada; 

        decimal Base1 = _DDJJEmpleados[index].Escala + _DDJJEmpleados[index].AntiguedadImporte + _DDJJEmpleados[index].Presentismo;

        //Haberes
        txt_SueldoBasico.Text = _DDJJEmpleados[index].Escala.ToString("N2");
        txt_Antiguedad.Text = _DDJJEmpleados[index].AntiguedadImporte.ToString("N2");
        txt_Presentismo.Text = _DDJJEmpleados[index].Presentismo.ToString("N2");
        txt_Acuerdo.Text = _DDJJEmpleados[index].AcuerdoNR1.ToString("N2");
        txt_Acuerdo2.Text = _DDJJEmpleados[index].AcuerdoNR2 .ToString("N2");

        //Descuentos
        txt_Jubilacion.Text = _DDJJEmpleados[index].Jubilacion.ToString("N2");
        txt_Ley19302.Text = _DDJJEmpleados[index].Ley19302.ToString("N2");
        txt_ObraSocial.Text = _DDJJEmpleados[index].ObraSocial.ToString("N2");
        txt_AporteLey.Text = _DDJJEmpleados[index].AporteLey.ToString("N2");//.AporteLeyDif.ToString("N2");
        txt_AporteSocio.Text = _DDJJEmpleados[index].AporteSocio.ToString("N2"); //.AporteSocioEscala.ToString("N2");
        txt_FAECyS.Text = _DDJJEmpleados[index].FAECys.ToString("N2");
        txt_Osecac.Text = _DDJJEmpleados[index].OSECAC.ToString("N2");

        //Totales
        txt_TotalHaberes.Text = _DDJJEmpleados[index].TotalHaberes.ToString("N2");  
        txt_TotalDescuentos.Text = _DDJJEmpleados[index].TotalDescuentos.ToString("N2");
        txt_TotalNeto.Text = (_DDJJEmpleados[index].TotalHaberes - _DDJJEmpleados[index].TotalDescuentos).ToString("N2");
        txt_SueldoDeclarado.Text = _DDJJEmpleados[index].Sueldo.ToString("N2");
        txt_Diferencia.Text = ((_DDJJEmpleados[index].TotalHaberes - _DDJJEmpleados[index].TotalDescuentos) - _DDJJEmpleados[index].Sueldo).ToString("N2");

        // Resumen
        txt_CantidadEmpleados.Text = _DDJJEmpleados.Count.ToString();
        txt_CantidadJorandaCompleta.Text = _DDJJEmpleados.Count(x => x.Jornada == "Completa").ToString();
        txt_CantidadJornadaParcial.Text = _DDJJEmpleados.Count(x => x.Jornada == "Parcial").ToString();
        txt_DifAporteSocio.Text = _DDJJEmpleados.Where(x => x.Jornada == "Parcial").Sum(x => x.AporteSocioDif).ToString("N2");
        txt_DifSueldo.Text = _DDJJEmpleados.Sum(x => x.SueldoDif).ToString("N2");
      }
    }
    private void dgv_DetallePeriodo_SelectionChanged(object sender, EventArgs e)
    {
      MostrarDetalleDDJJ();
      MostrarSueldoSegunEscala();
    }

    private void MostrarSueldoSegunEscala()
    {
      using (var context = new lts_sindicatoDataContext())
      {
        dgv_ReciboSueldo.DataSource = (from a in context.ReciboSueldoConceptos select a).ToList();
      }
    }

    private void dgv_DetallePeriodo_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {

    }
  }
}

