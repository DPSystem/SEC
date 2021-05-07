using entrega_cupones.Modelos;
using Org.BouncyCastle.Asn1.Mozilla;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace entrega_cupones.Metodos
{
  class mtdCobranzas
  {
    public static List<mdlEstadoPlanDePago> _EstadoPlan = new List<mdlEstadoPlanDePago>();

    public static decimal ObtenerValorDeCuota(decimal Deuda, decimal TazaDeInteres, int Cuotas)
    {
      decimal v = (Deuda * TazaDeInteres);
      decimal w = (decimal)(Math.Pow(1 + (double)TazaDeInteres, -Cuotas));
      //decimal ValorDeCuota = Cuotas == 1 ? Deuda : TazaDeInteres > 0 ? (v) / (1 - w) : Deuda;
      decimal ValorDeCuota = Cuotas == 1 ? Deuda : (v) / (1 - w);

      return ValorDeCuota;
    }

    public static decimal ObtenerIntereses(decimal Capital, decimal TazaDeInteres)
    {
      return Capital * TazaDeInteres;
    }

    public static List<mdlCuadroAmortizacion> ObtenerCuadroDeAmortizacion(double CapitalInicial, double TazaDeInteres, int CantidadDeCuotas, double ImporteDeCuota, double Anticipo, DateTime VencDeEntrega, DateTime VencDeCuota, decimal DeudaInicial)
    {
      List<mdlCuadroAmortizacion> CuadroDeAmortizacion = new List<mdlCuadroAmortizacion>();

      //double ImporteDeCuota = ObtenerValorDeCuota(CaptialInicial, TazaDeInteres, CantidadDeCuotas);
      if (CantidadDeCuotas == 1)
      {
        //double Deuda = CaptialInicial;
        mdlCuadroAmortizacion Insert__ = new mdlCuadroAmortizacion();
        Insert__.Cuota = CantidadDeCuotas.ToString();
        Insert__.ImporteDeCuota = CapitalInicial;
        Insert__.Interes = 0;
        Insert__.Amortizado = 0;
        Insert__.AAmortizar = 0;
        Insert__.FechaDeVenc = VencDeCuota.Date;
        Insert__.DeudaInicial = DeudaInicial;
        CuadroDeAmortizacion.Add(Insert__);
      }
      else
      {
        double Deuda = CapitalInicial;

        mdlCuadroAmortizacion Insert_ = new mdlCuadroAmortizacion();
        VencDeCuota = VencDeCuota.AddMonths(-1);
        Insert_.Cuota = "Anticipo";
        Insert_.ImporteDeCuota = Anticipo;
        Insert_.Interes = 0;
        Insert_.Amortizado = 0;
        Insert_.AAmortizar = 0;
        Insert_.FechaDeVenc = VencDeEntrega.Date;
        Insert_.DeudaInicial = DeudaInicial;
        CuadroDeAmortizacion.Add(Insert_);

        for (int cuota = 0; cuota < CantidadDeCuotas; cuota++)
        {
          mdlCuadroAmortizacion Insert = new mdlCuadroAmortizacion();
          Insert.Cuota = (cuota + 1).ToString();
          Insert.ImporteDeCuota = Math.Round(ImporteDeCuota, 2);
          Insert.Interes = Math.Round(Deuda * TazaDeInteres, 2);
          Insert.Amortizado = Math.Round(ImporteDeCuota - Insert.Interes, 2);
          Insert.AAmortizar = Math.Round(Deuda - Insert.Amortizado, 2);
          VencDeCuota = ControlarDiaHabil(VencDeCuota.Date.AddMonths(1));
          Insert.FechaDeVenc = VencDeCuota;
          Insert.DeudaInicial = DeudaInicial;
          Deuda -= Insert.Amortizado;

          CuadroDeAmortizacion.Add(Insert);
        }
      }
      return CuadroDeAmortizacion;
    }

    public static List<mdlCuadroAmortizacion> LimpiarDgvPlanDePagos()
    {
      List<mdlCuadroAmortizacion> CuadroDeAmortizacion = new List<mdlCuadroAmortizacion>();
      return CuadroDeAmortizacion;
    }

    public static DateTime ControlarDiaHabil(DateTime fecha)
    {
      DateTime fecha_ = fecha;
      if (fecha.DayOfWeek.ToString() == "Sunday")
      {
        fecha_ = fecha_.AddDays(1);
      }
      if (fecha.DayOfWeek.ToString() == "Saturday")
      {
        fecha_ = fecha_.AddDays(2);
      }
      return fecha_;
    }

    public static int AsentarPlan(string cuit, int NroDeActa, List<mdlCuadroAmortizacion> _PlanDePago)
    {
      int NroDePlan = 0;

      using (var context = new lts_sindicatoDataContext())
      {
        NroDePlan = context.PlanesDePago.ToList().Count() == 0 ? 1 : context.PlanesDePago.Max(x => x.NroDePlan) + 1;

        PlanesDePago Insert = new PlanesDePago();
        Insert.Fecha = DateTime.Now;
        Insert.CUIT = cuit; // dgv_EmpresaAfectada.CurrentRow.Cells["cuit"].Value.ToString();
        Insert.NroDeAsignacion = 0;
        Insert.NroDePlan = NroDePlan;
        Insert.Estado = 1;
        Insert.DeudaInicial = _PlanDePago.First().DeudaInicial;
        Insert.Acta = NroDeActa;
        context.PlanesDePago.InsertOnSubmit(Insert);
        context.SubmitChanges();

        CargarPlanDetalle(_PlanDePago, NroDePlan);
      }
      return NroDePlan;
    }

    public static void CargarPlanDetalle(List<mdlCuadroAmortizacion> _PlanDePago, int NroDePlan)
    {
      using (var context = new lts_sindicatoDataContext())
      {
        foreach (var item in _PlanDePago)
        {
          PlanDetalle InsertPlanDetalle = new PlanDetalle();
          InsertPlanDetalle.NroPlanDePago = NroDePlan;
          InsertPlanDetalle.Cuota = item.Cuota == "Anticipo" ? 0 : Convert.ToInt32(item.Cuota); //fila.Cells["CuotaDelPlan"].Value.ToString() == "Anticipo" ? 0 : Convert.ToInt32(fila.Cells["CuotaDelPlan"].Value);
          InsertPlanDetalle.ImporteCuota = (decimal)item.ImporteDeCuota; //Convert.ToDecimal(fila.Cells["ImporteDeCuota"].Value);
          InsertPlanDetalle.Interes = (decimal)item.Interes; //Convert.ToDecimal(fila.Cells["InteresDeCuota"].Value);
          InsertPlanDetalle.Amortizado = (decimal)item.Amortizado; //Convert.ToDecimal(fila.Cells["Amortizado"].Value);
          InsertPlanDetalle.AAmortizar = (decimal)item.AAmortizar; //Convert.ToDecimal(fila.Cells["AAmortizar"].Value);
          InsertPlanDetalle.FechaVenc = item.FechaDeVenc; // Convert.ToDateTime(fila.Cells["FechaDeVencimiento"].Value);
          InsertPlanDetalle.Estado = 1;
          InsertPlanDetalle.InteresResarcitorio = 0;
          InsertPlanDetalle.DiasDeMora = 0;
          InsertPlanDetalle.ImporteCobrado = 0;
          InsertPlanDetalle.Cancelado = "0";

          context.PlanDetalle.InsertOnSubmit(InsertPlanDetalle);
          context.SubmitChanges();
        }
      }
    }

    public static void ImprimirPlanDePago(List<mdlCuadroAmortizacion> _PlanDePago, string RazonSocial, string Cuit, string Inspector, string DeudaInical, string TotalFinanciado, string NroDeActa)
    {
      DS_cupones ds = new DS_cupones();
      DataTable dt = ds.ImpresionPlanDePago;
      dt.Clear();
      foreach (var item in _PlanDePago)
      {
        DataRow row = dt.NewRow();
        row["Cuota"] = item.Cuota;
        row["FechaDeVencimiento"] = item.FechaDeVenc;
        row["ImporteDeCuota"] = item.ImporteDeCuota;

        dt.Rows.Add(row);
      }
      reportes frmReportes = new reportes();
      frmReportes.dt = dt;
      frmReportes.dt2 = mtdFilial.Get_DatosFilial();
      frmReportes.NombreDelReporte = "entrega_cupones.Reportes.rpt_PlanDePagoActa.rdlc";
      frmReportes.Parametro1 = "Plan de Pago";
      frmReportes.Parametro2 = RazonSocial;//dgv_EmpresaAfectada.CurrentRow.Cells["Empresa"].Value.ToString();
      frmReportes.Parametro3 = Cuit;//dgv_EmpresaAfectada.CurrentRow.Cells["CUIT"].Value.ToString();
      frmReportes.Parametro4 = Inspector; //dgv_EmpresaAfectada.CurrentRow.Cells["Inspector"].Value.ToString();
      frmReportes.Parametro5 = DeudaInical;//txt_DeudaInicial.Text;
      frmReportes.Parametro6 = TotalFinanciado;
      frmReportes.Parametro7 = NroDeActa;
      frmReportes.Show();
    }

    public static List<mdlEstadoPlanDePago> GetEstadoPlanDePago(int NroPlanDePago)
    {

      _EstadoPlan.Clear();

      using (var context = new lts_sindicatoDataContext())
      {
        try
        {
          var EstadoDePlan = from a in context.PlanDetalle.Where(x => x.NroPlanDePago == NroPlanDePago)
                             select new mdlEstadoPlanDePago
                             {
                               CuotaId = a.Id,
                               Cuota = (int)a.Cuota,
                               ImporteDeCuota = (decimal)a.ImporteCuota,
                               FechaDeVenc = (DateTime)a.FechaVenc,
                               Dias = mtdFuncUtiles.CalcularDias((DateTime)a.FechaVenc, DateTime.Today),
                               Interes = (decimal)a.InteresResarcitorio,
                               //mtdIntereses.GetInteresResar((DateTime)a.FechaVenc, DateTime.Today, (decimal)a.ImporteCuota, 1, (DateTime)a.FechaVenc),
                               ImporteCobrado = (decimal)a.ImporteCobrado,
                               Total = (decimal)a.ImporteCuota, //+ mtdIntereses.GetInteresResar((DateTime)a.FechaVenc, DateTime.Today, (decimal)a.ImporteCuota, 1, (DateTime)a.FechaVenc)) - (decimal)a.ImporteCobrado,
                               Cancelado = a.Cancelado == "0" ? "NO" : "SI"
                             };

          _EstadoPlan.AddRange(EstadoDePlan);
        }
        catch (Exception e)
        {

          MessageBox.Show("Get Estado Pla de Pago" + e.Message);
        }
        return _EstadoPlan.ToList();
      }
    }

    public static int GetNroRecibo()
    {
      using (var context = new lts_sindicatoDataContext())
      {
        int Numero = context.Recibos.Count() == 0 ? 1 : (int)context.Recibos.Max(x => x.NroAutomatico) + 1;
        return Numero;
      }
    }

    public static void GrabarNroDeRecibo(int NroDeRecibo, int NroDeActa, decimal ImporteDeRecibo, string FechaReciboManual, string NroReciboManual)
    {
      using (var context = new lts_sindicatoDataContext())
      {
        Recibos rcb = new Recibos
        {
          NroAutomatico = NroDeRecibo,
          FechaAutomatica = DateTime.Now,
          NroDeActa = NroDeActa,
          Importe = ImporteDeRecibo,
          Estado = 1,
          FechaManual = FechaReciboManual,
          NroManual = NroReciboManual,

        };
        context.Recibos.InsertOnSubmit(rcb);
        context.SubmitChanges();
      }
    }

    public static void GrabarEfectivo(int NroDerecibo, int NroDeActa, Decimal Importe)
    {
      using (var context = new lts_sindicatoDataContext())
      {
        Efectivo efectivo = new Efectivo
        {
          NroDeRecibo = NroDerecibo,
          NroDeActa = NroDeActa,
          Importe = Importe,
          Fecha = DateTime.Now
        };
        context.Efectivo.InsertOnSubmit(efectivo);
        context.SubmitChanges();

      }
    }

    public static void GrabarCheques(List<mdlCargaDeCheques> ChequesCargados, int NroDerecibo)
    {
      using (var context = new lts_sindicatoDataContext())
      {
        foreach (var item in ChequesCargados)
        {
          cheques cheque = new cheques
          {
            NroDeRecibo = NroDerecibo,
            FechaDeEmision = item.FechaEmision,
            FechaDeVencimiento = item.FechaVenc,
            Importe = item.Importe,
            NroDeCheque = item.Numero,
            BancoID = item.BancoId,
            Estado = 1
          };
          context.cheques.InsertOnSubmit(cheque);
          context.SubmitChanges();
        }
      }
    }

    public static void GrabarTransf(int NroDerecibo, int NroDeActa, string NroDeTransf, int BancoId, decimal Monto, DateTime FechaDeTransf, int UsuarioId)
    {
      using (var context = new lts_sindicatoDataContext())
      {
        Transferencias transf = new Transferencias
        {
          NroDeRecibo = NroDerecibo,
          NroDeActa = NroDeActa,
          NroDeTransf = NroDeTransf,
          BancoId = BancoId,
          Monto = Monto,
          FechaDeTransf = FechaDeTransf,
          FechaDeCarga = DateTime.Now,
          UsuarioId = UsuarioId
        };
        context.Transferencias.InsertOnSubmit(transf);
        context.SubmitChanges();
      }
    }

    public static void GrabarCanje(int NroDeActa, int NroDerecibo, DateTime? FechaDeRemito, DateTime? FechaFactura, Decimal Importe, string Descripcion, int UsuarioId)
    {
      using (var context = new lts_sindicatoDataContext())
      {
        canjes canje = new canjes
        {
          NroDeActa = NroDeActa,
          NroDeRecibo = NroDerecibo,
          FechaRemito = Convert.ToDateTime(FechaDeRemito),
          FechaFactura = Convert.ToDateTime(FechaFactura),
          ImporteFactura = Importe,
          Descripcion = Descripcion,
          UsuarioId = UsuarioId
        };
        context.canjes.InsertOnSubmit(canje);
        context.SubmitChanges();
      }
    }

    public static List<mdlBancos> ListadoBancos()
    {
      using (var context = new lts_sindicatoDataContext())
      {
        var listadobancos = from a in context.Bancos
                            select new mdlBancos
                            {
                              Id = a.ID,
                              Nombre = a.BAN_SUCURSAL == null ? a.BAN_NOMBRE : a.BAN_NOMBRE + " - " + a.BAN_SUCURSAL

                            };
        return listadobancos.ToList();
      }
    }

    public static string GetNombreDelBanco(int BancoId)
    {
      using (var context = new lts_sindicatoDataContext())
      {
        var Nombre = context.Bancos.Where(x => x.ID == BancoId);//.SingleOrDefault().BAN_NOMBRE;
        if (Nombre.Count() > 0)
        {
          return Nombre.Single().BAN_NOMBRE;
        }
        else
        {
          return "No Especifica";
        }
      }
    }
  }
}
