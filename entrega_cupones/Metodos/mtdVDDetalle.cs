using entrega_cupones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entrega_cupones.Metodos
{
  class mtdVDDetalle
  {
    public static void Insert_VDDetalle(List<EstadoDDJJ> ddjjt, int VDDId, bool ParaInsertar)
    {
      foreach (var periodo in ddjjt)
      {
        if (ParaInsertar)
        {
          InsertVDDetalle(periodo, VDDId);
        }
        else
        {
          if (periodo.VerifDeuda == "")
          {
            InsertVDDetalle(periodo, VDDId);
          }
          else
          {
            UpdateVDDetalle(periodo, VDDId,periodo.Periodo, periodo.Rectificacion, Convert.ToDateTime( periodo.FechaDePago));
          }
        }
      }
    }

    public static void InsertVDDetalle(EstadoDDJJ periodo, int VDInspectorId)
    {
      using (var context = new lts_sindicatoDataContext())
      {
        VD_Detalle VDDetalle = new VD_Detalle
        {
          VDInspectorId = VDInspectorId,
          Periodo = periodo.Periodo,
          Rectificacion = periodo.Rectificacion,
          CantidadEmpleados = periodo.Empleados,
          CantidadSocios = periodo.Socios,
          TotalSueldoEmpleados = periodo.TotalSueldoEmpleados,
          TotalSueldoSocios = periodo.TotalSueldoSocios,
          TotalAporteEmpleados = periodo.AporteLey,
          TotalAporteSocios = periodo.AporteSocio,
          FechaDePago = Convert.ToDateTime(periodo.FechaDePago),
          ImporteDepositado = periodo.ImporteDepositado,
          DiasDeMora = periodo.DiasDeMora,
          DeudaGenerada = periodo.Capital,
          InteresGenerado = periodo.Interes,
          Total = periodo.Total,
          PerNoDec = periodo.PerNoDec,
          ActaId = 0,
          NumeroDeActa = 0,
          Estado = 0 
        };
        context.VD_Detalle.InsertOnSubmit(VDDetalle);
        context.SubmitChanges();
      }
    }

    public static void UpdateVDDetalle(EstadoDDJJ ObjPeriodo, int VD_CabeceraId,DateTime Periodo, int Rectificacion, DateTime FechaDePago)
    {
      using (var context = new lts_sindicatoDataContext())
      {
        var PeriodoDeVDDetalle = context.VD_Detalle.Where(x => x.VDInspectorId == VD_CabeceraId && x.Periodo == Periodo && x.Rectificacion == Rectificacion && x.FechaDePago == FechaDePago).Single();
        if (PeriodoDeVDDetalle != null )
        {
          PeriodoDeVDDetalle.CantidadEmpleados = ObjPeriodo.Empleados;
          PeriodoDeVDDetalle.CantidadSocios = ObjPeriodo.Socios;
          PeriodoDeVDDetalle.TotalSueldoEmpleados = ObjPeriodo.TotalSueldoEmpleados;
          PeriodoDeVDDetalle.TotalSueldoSocios = ObjPeriodo.TotalSueldoSocios;
          PeriodoDeVDDetalle.TotalAporteEmpleados = ObjPeriodo.AporteLey;
          PeriodoDeVDDetalle.TotalAporteSocios = ObjPeriodo.AporteSocio;
          PeriodoDeVDDetalle.FechaDePago = Convert.ToDateTime(ObjPeriodo.FechaDePago);
          PeriodoDeVDDetalle.ImporteDepositado = ObjPeriodo.ImporteDepositado;
          PeriodoDeVDDetalle.DiasDeMora = ObjPeriodo.DiasDeMora;
          PeriodoDeVDDetalle.DeudaGenerada = ObjPeriodo.Capital;
          PeriodoDeVDDetalle.InteresGenerado = ObjPeriodo.Interes;
          PeriodoDeVDDetalle.Total = ObjPeriodo.Total;
          PeriodoDeVDDetalle.PerNoDec = ObjPeriodo.PerNoDec;
          context.SubmitChanges();
        }
      }
    }

    public static List<VD_Detalle> Get_VDD(int VDId)
    {
      using (var context =  new lts_sindicatoDataContext())
      {        
        var VD = from a in context.VD_Detalle where a.VDInspectorId == VDId select a;
        return VD.ToList();
      }
    }

    public static List<VD_Detalle> VD_ListadoDDJJT(List<VD_Detalle> VD_Detalle, string cuit, DateTime desde, DateTime hasta, DateTime FechaVencimiento, int TipoInteres, decimal TazaInteres, int VDId)
    {
      using (var context = new lts_sindicatoDataContext())
      {
        //  var _EstadoDDJJ = context.VD_Detalle.Where(x => x.VDInspectorId == VDId)
        //   .Select(row => new VD_Detalle
        foreach (var vD in VD_Detalle)
        {



          // vD.Periodo = Periodo.Date, // Convert.ToDateTime(row.Periodo),
          //vD.Rectificacion = (int)row.Rectificacion,
          //vD.AporteLey = (decimal)row.TotalAporteEmpleados,
          // vD.AporteSocio = (decimal)row.TotalAporteSocios,
          // vD.TotalSueldoEmpleados = (decimal)row.TotalSueldoEmpleados, /*.titem1 / Convert.ToDecimal(0.02)*/
          // vD.TotalSueldoSocios = (decimal)row.TotalSueldoSocios, //.titem2 / Convert.ToDecimal(0.02),
          //  vD.FechaDePago = row.FechaDePago == null ? null : row.FechaDePago, //.fpago == null ? null : row.fpago,
          // vD.ImporteDepositado = (decimal)row.ImporteDepositado, //row.impban1,
          //  vD.Empleados = row.CantidadEmpleados,//context.ddjj.Where(x => x.CUIT_STR == cuit && (x.periodo == row.periodo) && (x.rect == row.rect)).Count(),
          // vD.Socios = row.CantidadSocios,//context.ddjj.Where(x => x.CUIT_STR == cuit && (x.periodo == row.periodo) && x.rect == row.rect && x.item2 == true).Count(),
          vD.DeudaGenerada =
                   CalcularCapital((decimal)vD.ImporteDepositado, (decimal)vD.TotalSueldoEmpleados, (decimal)vD.TotalSueldoSocios,
                   Convert.ToDateTime(vD.FechaDePago), FechaVencimiento, Convert.ToDateTime(vD.Periodo), TipoInteres, TazaInteres
          );

          vD.InteresGenerado =
                   CalcularInteres(Convert.ToDateTime(vD.FechaDePago), Convert.ToDateTime(vD.Periodo),
                   CalcularCapital((decimal)(vD.ImporteDepositado), (decimal)vD.TotalSueldoEmpleados, (decimal)vD.TotalSueldoSocios,
                   Convert.ToDateTime(vD.FechaDePago), FechaVencimiento, Convert.ToDateTime(vD.Periodo), TipoInteres, TazaInteres),
                   FechaVencimiento, TipoInteres, TazaInteres
          );

          vD.DiasDeMora = CalcularDias(Convert.ToDateTime(vD.Periodo), Convert.ToDateTime(vD.FechaDePago) == null ? FechaVencimiento : Convert.ToDateTime(vD.FechaDePago)); //Convert.ToDecimal(0.99)

          vD.Total =
                 CalcularTotal(
                   CalcularCapital((decimal)(vD.ImporteDepositado), (decimal)vD.TotalSueldoEmpleados, (decimal)vD.TotalSueldoSocios, Convert.ToDateTime(vD.TotalSueldoSocios), FechaVencimiento, Convert.ToDateTime(vD.Periodo), TipoInteres, TazaInteres),

                   CalcularInteres(
                                    Convert.ToDateTime(vD.FechaDePago), Convert.ToDateTime(vD.Periodo),
                                    CalcularCapital(
                                                   (decimal)(vD.ImporteDepositado), (decimal)vD.TotalSueldoEmpleados, (decimal)vD.TotalSueldoSocios,
                                                   Convert.ToDateTime(vD.FechaDePago), FechaVencimiento, Convert.ToDateTime(vD.Periodo), TipoInteres, TazaInteres),
                                                   FechaVencimiento, TipoInteres, TazaInteres));

          vD.NumeroDeActa = Convert.ToInt32(GetNroDeActa(Convert.ToDateTime(vD.Periodo), mtdVDInspector.Get_VDCuit((int)vD.VDInspectorId)));
          vD.VDInspectorId =Convert.ToInt32(  GetNroVerifDeuda(cuit, Convert.ToDateTime(vD.Periodo), Convert.ToInt32(vD.Rectificacion), false, Convert.ToDateTime(vD.FechaDePago)));
           //});
         };
      }
      return VD_Detalle;
    }
    public static decimal CalcularCapital(decimal Depositado, decimal titem1, decimal titem2, DateTime? FechaDePago, DateTime FechaDeVencimientoDeActa, DateTime Periodo, int TipoDeInteres, decimal TazaDeInteres)
    {
      decimal Capital = Depositado - (titem1 + titem2);

      if (Capital > 0)
      {
        Capital = 0; // POR QUE PAGO CON SISTEMA NUEVO QUE YA INCLUYE LOS INTERESES
      }
      else
      {
        if (Capital < 0)
        {
          Capital = Capital * -1; // IMPORTE NO DEPOSITADO
        }
        else
        {
          if (Capital == 0) // QUE EL CAPITAL ES IGUAL A LA SUMA DE LOS 2 %
          {
            Capital = (titem1 + titem2);
          }
        }
        // si Fecha de Pago es no null entonces calculo el coeficiente A
        if (!(FechaDePago == Convert.ToDateTime("01/01/0001") || FechaDePago == null))
        {
          Capital = mtdIntereses.GetCoeficienteA(Periodo.AddMonths(1).AddDays(14), Convert.ToDateTime(FechaDePago), Capital, TipoDeInteres, TazaDeInteres, Periodo, FechaDeVencimientoDeActa);
        }
      }
      return Capital;
    }

    public static decimal CalcularInteres(DateTime? FechaDePago, DateTime Periodo, decimal importe, DateTime FechaVencimiento, int TipoInteres, decimal Interes)
    {
      decimal interes;
      if (TipoInteres == 1) //TipoInteres == 1 => AFIP
      {
        interes = mtdIntereses.CalcularInteresAFIP(FechaDePago, Periodo, FechaVencimiento, importe, TipoInteres, Interes);// .GetInteresAFIP(Periodo, Convert.ToDateTime(FechaDePago), importe, TipoInteres, FechaVencimiento); 
        //interes = mtdIntereses.GetInteresAFIP(Periodo, Convert.ToDateTime(FechaDePago), importe, TipoInteres, FechaVencimiento); //CalcularInteres(FechaDePago, Periodo, importe, FechaVencimiento);
      }
      else
      {
        interes = mtdIntereses.GetInteresManual(FechaDePago, Periodo, FechaVencimiento, importe, Interes); //CalcularInteresManual(Periodo,FechaVencimiento,importe,Interes);
      }
      return interes;
    }

    public static int CalcularDias(DateTime Periodo, DateTime FechaVencimiento)
    {
      DateTime FechaVencimientoPeriodo = Periodo.AddMonths(1).AddDays(14);
      int dias = Convert.ToInt32((FechaVencimiento - FechaVencimientoPeriodo).TotalDays);
      return dias > 0 ? dias : 0;
    }

    public static decimal CalcularTotal(decimal ImporteNoDepositado, decimal Interes)
    {
      return ImporteNoDepositado + Interes;
    }

    public static string GetNroDeActa(DateTime Periodo, string Cuit)
    {
      using (var context = new lts_sindicatoDataContext())
      {
        //var NroActa = context.ACTAS.Where(x => x.CUIT_STR == Cuit && (x.DESDE <= Periodo && Periodo <= x.HASTA)).FirstOrDefault();
        var NroActa = context.Acta.Where(x => x.EmpresaCuit == Cuit && (x.Desde <= Periodo && Periodo <= x.Hasta)).FirstOrDefault();
        return NroActa == null ? "" : NroActa.Numero.ToString();
      }
    }

    public static string GetNroVerifDeuda(string cuit, DateTime Periodo, int Rectificacion, bool PerNoDec, DateTime? FechaDePago)
    {
      //if (FechaDePago == null)
      //{
      //  FechaDePago = Convert.ToDateTime("0001 - 01 - 01 00:00:00.0000000");
      //}
      using (var context = new lts_sindicatoDataContext())
      {
        string vd = "";
        var InspectorVerifId = from a in context.VD_Inspector where a.CUIT == cuit && a.Estado == 0 select new { a.Id };
        if (InspectorVerifId.Count() > 0)
        {
          var VerifDeuda = context.VD_Detalle.Where(x => x.VDInspectorId == InspectorVerifId.Single().Id && x.Periodo == Periodo && x.Rectificacion == Rectificacion && x.FechaDePago == FechaDePago);
          if (VerifDeuda.Count() > 0)
          {
            if (PerNoDec)
            {
              vd = VerifDeuda.Single().Id.ToString();
            }
            else
            {
              vd = VerifDeuda.Single().VDInspectorId.ToString();
            }
          }
        }
        return vd;
      }
    }
  }
}

