using entrega_cupones.Clases;
using entrega_cupones.Metodos;
using entrega_cupones.Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace entrega_cupones.Formularios
{
  public partial class frm_CobroDeActas : Form
  {
    int _NroDeActa = 0;
    int _NroDePlan = 0;
    decimal _ACobrar = 0;
    decimal _Recaudado = 0;
    decimal _Diferencia = 0;
    decimal _Efectivo = 0;
    decimal _Transferencias = 0;
    decimal _Tarjetas = 0;
    decimal _Canjes = 0;

    List<mdlCargaDeCheques> _ChequesCargados = new List<mdlCargaDeCheques>();
    List<mdlEstadoPlanDePago> _Plan = new List<mdlEstadoPlanDePago>();
    // List<mdlVerRecibos> _VerRecibos = new List<mdlVerRecibos>();

    public frm_CobroDeActas()
    {
      InitializeComponent();
    }

    private void frm_CobroDeActas_Load(object sender, EventArgs e)
    {
      dgv_PlanDePagos.AutoGenerateColumns = false;

      msk_FechaDeRemito.ValidatingType = typeof(System.DateTime);
      msk_FechaDeRemito.TypeValidationCompleted += new TypeValidationEventHandler(msk_FechaDeRemito_TypeValidationCompleted);
      CargarCbxBancos();

      //msk_FechaDeRemito.KeyDown += new KeyEventHandler(msk_FechaDeRemito_keyDown);
    }

    private void CargarCbxBancos()
    {
      List<mdlBancos> ListadoBancos = mtdCobranzas.ListadoBancos();
      cbx_Banco.DisplayMember = "Nombre";
      cbx_Banco.ValueMember = "Id";
      cbx_Banco.DataSource = ListadoBancos;

      cbx_BancoTransf.DisplayMember = "Nombre";
      cbx_BancoTransf.ValueMember = "Id";
      cbx_BancoTransf.DataSource = ListadoBancos;
    }

    private void btn_Buscar_Click(object sender, EventArgs e)
    {
      BuscarActa();
    }

    private void BuscarActa()
    {
      var acta = mtdActas.GetActa(Convert.ToInt32(txt_NumeroDeActa.Text));
      if (acta != null)
      {
        _NroDeActa = acta.NroActa;
        _NroDePlan = acta.NroDePlan;
        txt_RazonSocial.Text = acta.RazonSocial;
        txt_CUIT.Text = acta.Cuit;
        txt_Domicilio.Text = acta.Domicilio;
        msk_Desde.Text = acta.Desde.ToString("MM/yyyy");
        msk_Hasta.Text = acta.Hasta.ToString("MM/yyyy");
        lbl_ActaSinRegistro.Visible = false;
        MostrarPlanDePago(_NroDePlan);

      }
      else
      {
        lbl_ActaSinRegistro.Visible = true;
        txt_RazonSocial.Text = "";
        txt_CUIT.Text = "";
        txt_Domicilio.Text = "";
        msk_Desde.Text = "";
        msk_Hasta.Text = "";
        lbl_PlanDePago.Text = "Plan de Pago";
        _NroDePlan = 0;
        List<mdlEstadoPlanDePago> s = new List<mdlEstadoPlanDePago>();
        dgv_PlanDePagos.DataSource = s.ToList();
      }
    }

    private void MostrarPlanDePago(int NroPlan)
    {
      try
      {
        _Plan = mtdCobranzas.GetEstadoPlanDePago(NroPlan);
        //Esto es para sumar en el interes con el valor de la cuota en el List<T>

        foreach (var item in _Plan)
        {
          string mensaje = string.Empty;
          try
          {

            var s = _Plan.Where(x => x.Cuota == item.Cuota).First();

            if (s.Cancelado == "NO")
            {
              decimal ImporteDeCuota = s.ImporteDeCuota - s.ImporteCobrado;
              s.Interes = mtdIntereses.GetInteresResar((DateTime)s.FechaDeVenc, DateTime.Today, ImporteDeCuota, 1, (DateTime)s.FechaDeVenc);
            }
            s.Total = (s.Interes + s.Total) - s.ImporteCobrado;

          }
          catch (Exception e)
          {
            MessageBox.Show(mensaje + e.Message);
          }
        }
        /////
        if (_Plan.Count() > 0)
        {
          dgv_PlanDePagos.DataSource = _Plan.ToList();

          lbl_PlanDePago.Text = "Plan de Pago Nº " + _NroDePlan.ToString();// + acta.NroDePlan.ToString();

        }
        else
        {
          lbl_PlanDePago.Text = "Plan de Pago";
        }
      }
      catch (Exception e)
      {
        MessageBox.Show("Mostrar Plan de Pago " + e.Message);
        //throw;
      }
      BloquearCuotasCanceladas();
    }

    private void InteresCuota()
    {

    }

    private void txt_NumeroDeActa_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter)
      {
        BuscarActa();
      }
    }

    private void dgv_PlanDePagos_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {

    }

    private void dgv_PlanDePagos_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Space)
      {
        dgv_PlanDePagos.CurrentRow.Cells["Check"].Value = Convert.ToBoolean(dgv_PlanDePagos.CurrentRow.Cells["Check"].Value) == true ? false : true;
        GetAPagar();
        GetDiferencia();
      }
    }

    private void GetAPagar()
    {
      decimal ACobrar = 0;
      foreach (DataGridViewRow Fila in dgv_PlanDePagos.Rows)
      {
        if (Convert.ToBoolean(Fila.Cells["Check"].Value))
        {
          ACobrar += Convert.ToDecimal(Fila.Cells["Total"].Value);
        }
      }
      txt_ACobrarTotal.Text = ACobrar.ToString("N2");
    }

    private void btn_CargarCheque_Click_1(object sender, EventArgs e)
    {
      SiExisteCheque();
      GetDiferencia();
    }

    private void SiExisteCheque()
    {
      if (_ChequesCargados.Count > 0)
      {
        var xxxx = _ChequesCargados.Where(x => x.Numero == txt_NroCheque.Text).Count();
        if (xxxx > 0)
        {
          MessageBox.Show("El Cheque Nº " + txt_NroCheque.Text + " ya existe", "ATENCION");
        }
        else
        {
          CargarDgvCheques();
        }
      }
      else
      {
        CargarDgvCheques();
      }
    }

    private void CargarDgvCheques()
    {
      mdlCargaDeCheques CargarCheque = new mdlCargaDeCheques
      {
        FechaEmision = Convert.ToDateTime(msk_EmisionCheque.Text),
        Numero = txt_NroCheque.Text,
        Importe = Convert.ToDecimal(txt_ImporteCheque.Text),
        FechaVenc = Convert.ToDateTime(msk_FechaPagoCheque.Text),
        BancoId = (int)cbx_Banco.SelectedValue,
        NombreDeBanco = cbx_Banco.Text

      };
      _ChequesCargados.Add(CargarCheque);
      dgv_Cheques.DataSource = _ChequesCargados.ToList();
      txt_TotalCheques.Text = _ChequesCargados.Sum(x => x.Importe).ToString("N2");
      txt_PagosCheques.Text = _ChequesCargados.Sum(x => x.Importe).ToString("N2");
      GetDiferencia();
    }

    private void btn_AsentarEfectivo_Click(object sender, EventArgs e)
    {
      decimal b = 0;
      if (txt_AsentarEfectivo.Text != "")
      {
        b = Convert.ToDecimal(txt_AsentarEfectivo.Text);
      }
      txt_PagosEfectivo.Text = b.ToString("N2"); //string.Format("N2", txt_AsentarEfectivo.Text);
      GetDiferencia();
    }

    private void GetDiferencia()
    {
      _ACobrar = Convert.ToDecimal(txt_ACobrarTotal.Text);
      _Efectivo = Convert.ToDecimal(txt_PagosEfectivo.Text);
      decimal Cheques = Convert.ToDecimal(txt_PagosCheques.Text);
      _Transferencias = Convert.ToDecimal(txt_PagosTransf.Text);
      _Tarjetas = Convert.ToDecimal(txt_PagosTarjetas.Text);
      _Canjes = Convert.ToDecimal(txt_PagosCanje.Text);

      _Recaudado = _Efectivo + Cheques + _Transferencias + _Tarjetas + _Canjes;
      _Diferencia = _Recaudado - _ACobrar;

      txt_Diferencia.Text = _Diferencia.ToString("N2"); //Diferencia == 0 ? "Sin Diferencia" :Diferencia >0 ?
      lbl_Diferencia.Text = _Diferencia > 0 ? "Sobra: " : _Diferencia == 0 ? "Sin Dif. : " : "Faltan";

    }

    private void btn_QuitarCheque_Click(object sender, EventArgs e)
    {
      QuitarCheque();
    }

    private void QuitarCheque()
    {
      _ChequesCargados.RemoveAll(x => x.Numero == dgv_Cheques.CurrentRow.Cells["Numero"].Value.ToString());
      dgv_Cheques.DataSource = _ChequesCargados.ToList();
      txt_TotalCheques.Text = _ChequesCargados.Sum(x => x.Importe).ToString("N2");
      txt_PagosCheques.Text = _ChequesCargados.Sum(x => x.Importe).ToString("N2");
      GetDiferencia();
    }

    private void btn_Cobrar_Click(object sender, EventArgs e)
    {
      Cobrar();
      Limpiar();
      BuscarActa();
    }

    private void Cobrar()
    {
      using (var context = new lts_sindicatoDataContext())
      {
        int NroRecibo = mtdCobranzas.GetNroRecibo();
        if (_ACobrar > 0)
        {
          mtdCobranzas.GrabarNroDeRecibo(NroRecibo, _NroDeActa, _Recaudado, msk_ReciboManualVenc.Text, txt_ReciboManualNro.Text);

          if (_Efectivo > 0)
          {
            mtdCobranzas.GrabarEfectivo(NroRecibo, _NroDeActa, _Efectivo);
          }
          if (_ChequesCargados.Count > 0)
          {
            mtdCobranzas.GrabarCheques(_ChequesCargados, NroRecibo);
          }
          if (_Transferencias > 0)
          {
            mtdCobranzas.GrabarTransf(NroRecibo, _NroDeActa, txt_NroTransf.Text, Convert.ToInt32(cbx_BancoTransf.SelectedValue), Convert.ToDecimal(txt_PagosTransf.Text), Convert.ToDateTime(msk_FechaTrasf.Text), 0);
          }
          if (_Tarjetas > 0)
          {
          }
          if (_Canjes > 0)
          {
          }
        }


        foreach (DataGridViewRow fila in dgv_PlanDePagos.Rows)
        {
          if (fila.Cells["Check"].Value != null) //Pregunto si el valor de la fila es distinto de null
          {
            mdlEstadoPlanDePago pln = _Plan.Where(x => x.Cuota == (int)fila.Cells["CuotaDelPlan"].Value).SingleOrDefault();
            if ((bool)fila.Cells["Check"].Value && Convert.ToString(fila.Cells["Cancelado"].Value) == "NO") // pregunto si la fila esta chequeada
            {
              if (_Diferencia == 0)
              {
                ImputarPagoEnCuota(fila, 0, NroRecibo, pln);
              }
              else
              {
                if (pln.Total >= _Recaudado)
                {
                  ImputarPagoEnCuota(fila, 1, NroRecibo, pln);
                }
                else
                {
                  ImputarPagoEnCuota(fila, 0, NroRecibo, pln);
                }
              }
            }
          }
        }
      }
    }

    private void ImputarPagoEnCuota(DataGridViewRow fila, int imp, int NroRecibo, mdlEstadoPlanDePago pln) //imp =  1 significa que es pago parcial
    {
      using (var context = new lts_sindicatoDataContext())
      {
        // >>>> Imputo el pago en la cuota correspondiente
        var Cuota = context.PlanDetalle.Where(x => x.NroPlanDePago == _NroDePlan && x.Cuota == (int)fila.Cells["CuotaDelPlan"].Value).SingleOrDefault();
        Cuota.InteresResarcitorio = Convert.ToDecimal(fila.Cells["InteresDeCuota"].Value);
        Cuota.DiasDeMora = (int)fila.Cells["DiasDeMora"].Value;
        Cuota.ImporteCobrado = imp == 0 ? (decimal)fila.Cells["Total"].Value + Cuota.ImporteCobrado : Cuota.ImporteCobrado + _Recaudado;//(decimal)fila.Cells["Total"].Value - _Diferencia;
        Cuota.Cancelado = imp == 0 ? "1" : "0";
        if (imp == 0)
        {
          Cuota.FechaCancela = DateTime.Now;
        }
        context.SubmitChanges();
        // <<<< Fin de imputacion en cuota

        // >>>> Guardo el Detalle del recibo. 
        RecibosDetalle RcbDetalle = new RecibosDetalle
        {
          CuotaId = pln.CuotaId,
          NroCuota = pln.Cuota,
          NroAut = NroRecibo,
          FechaAut = DateTime.Now,
          NroManual = 1,
          FechaManual = DateTime.Now,
          TipoId = 1,
          ConceptoId = 1,
          ModoPagoId = 1,
          UsuarioId = 1,
          Importe = imp == 0 ? (decimal)fila.Cells["Total"].Value : _Recaudado
        };
        context.RecibosDetalle.InsertOnSubmit(RcbDetalle);
        context.SubmitChanges();
        // <<<<< fin de guardado del detalle de recibo

        _ACobrar -= (decimal)fila.Cells["Total"].Value;
        _Recaudado -= (decimal)fila.Cells["Total"].Value;
      }
    }

    private void Limpiar()
    {
      //SolapaEfectivo
      txt_AsentarEfectivo.Text = "";
      // Solapa Cheques
      msk_EmisionCheque.Text = "";
      msk_FechaPagoCheque.Text = "";
      txt_ImporteCheque.Text = "";
      txt_NroCheque.Text = "";
      txt_TotalCheques.Text = "";
      _ChequesCargados.Clear();
      // Solapa Transferencia
      dgv_Cheques.DataSource = _ChequesCargados;
      txt_MontoTransf.Text = "";
      txt_NroTransf.Text = "";
      msk_FechaTrasf.Text = "";
      // Solapa Canjes
      msk_FechaDeFactura.Text = "";
      msk_FechaDeRemito.Text = "";
      txt_FacturaCanje.Text = "";
      txt_MontoCanje.Text = "";
      txt_RemitoCanje.Text = "";
      //variables publicas
      _ACobrar = 0;
      _Recaudado = 0;
      _Diferencia = 0;
      //A Cobrar
      txt_ACobrarTotal.Text = "0.00";
      // Recaudado
      txt_PagosEfectivo.Text = "0.00";
      txt_PagosCheques.Text = "0.00";
      txt_PagosTarjetas.Text = "0.00";
      txt_PagosTransf.Text = "0.00";
      txt_PagosCanje.Text = "0.00";
      // Diferencia
      txt_Diferencia.Text = "0.00";
      // Recibo Manual
      txt_ReciboManualNro.Text = "";
      msk_ReciboManualVenc.Text = "";
      // Variables Globales
      _Efectivo = 0;
      _Transferencias = 0;
      _Tarjetas = 0;
      _Canjes = 0;
    }

    private void BloquearCuotasCanceladas()
    {
      foreach (DataGridViewRow fila in dgv_PlanDePagos.Rows)
      {
        if (Convert.ToString(fila.Cells["Cancelado"].Value) == "SI")
        {
          //fila.Cells["Check"].ReadOnly = false;
          fila.ReadOnly = true;
          fila.DefaultCellStyle.BackColor = Color.SpringGreen;
        }
      }
    }

    private void btn_VerRecibosDelActa_Click(object sender, EventArgs e)
    {

      frm_VerRecibos frmVerRecibos = new frm_VerRecibos();
      frmVerRecibos._NroDeActa = _NroDeActa;
      frmVerRecibos.lbl_RecibosDeActa.Text = "Recibos del Acta Nº " + _NroDeActa.ToString();
      frmVerRecibos.ShowDialog();

    }

    private void btn_Salir_Click(object sender, EventArgs e)
    {
      Close();
    }

    private void btn_CargarTransf_Click(object sender, EventArgs e)
    {
      AsentarTransf();
    }

    private void AsentarTransf()
    {
      decimal b = 0;
      if (txt_MontoTransf.Text != "")
      {
        b = Convert.ToDecimal(txt_MontoTransf.Text);
      }
      txt_PagosTransf.Text = b.ToString("N2");
      GetDiferencia();
    }

    private void msk_FechaDeRemito_TypeValidationCompleted(object sender, TypeValidationEventArgs e)
    {
      if (!e.IsValidInput)
      {
        //toolTip1.ToolTipTitle = "Fecha no valida";
        //toolTip1.Show("La fecha ingresado no es valida, por favor ingrese con el siguiente formato dd/mm/aaaa", msk_FechaDeRemito);
      }
    }
  }
}
