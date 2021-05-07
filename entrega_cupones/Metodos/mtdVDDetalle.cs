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
    public static void InsertVerificacionDetalle(List<EstadoDDJJ> ddjjt, int VD_CabeceraId, bool ParaInsertar)
    {
      foreach (var periodo in ddjjt)
      {
        if (ParaInsertar)
        {
          InsertVDDetalle(periodo, VD_CabeceraId);
        }
        else
        {
          if (periodo.VerifDeuda == "")
          {
            InsertVDDetalle(periodo, VD_CabeceraId);
          }
          else
          {
            UpdateVDDetalle(periodo, VD_CabeceraId,periodo.Periodo, periodo.Rectificacion);
          }
        }
      }
    }

    public static void InsertVDDetalle(EstadoDDJJ periodo, int VD_CabeceraId)
    {
      using (var context = new lts_sindicatoDataContext())
      {
        VD_Detalle VDDetalle = new VD_Detalle
        {
          VD_CabeceraId = VD_CabeceraId,
          NumeroDeActa = 0,
          ActaId = 0,
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
          PerNoDec = periodo.PerNoDec

        };
        context.VD_Detalle.InsertOnSubmit(VDDetalle);
        context.SubmitChanges();
      }
    }

    public static void UpdateVDDetalle(EstadoDDJJ ObjPeriodo, int VD_CabeceraId,DateTime Periodo, int Rectificacion)
    {
      using (var context = new lts_sindicatoDataContext())
      {
        var PeriodoDeVDDetalle = context.VD_Detalle.Where(x => x.VD_CabeceraId == VD_CabeceraId && x.Periodo == Periodo && x.Rectificacion == Rectificacion).Single();
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

    //public static EstadoDDJJ GetPerNoDecInVDDetalle()
    //{

    //}
  }
}

