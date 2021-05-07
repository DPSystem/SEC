using entrega_cupones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entrega_cupones.Metodos
{
  class mtdVDCabecera
  {
    public static void InsertVerificacionDeudaCabecera(
      int InspectorVerificacionId,
      string cuit,
      DateTime FechaDeConfeccion,
      DateTime desde,
      DateTime hasta,
      DateTime vencimiento,
      int empresaId,
      decimal Capital,
      decimal Interes,
      decimal Total,
      int PlanDePago,
      int cantidadEmpleados,
      decimal InteresMensual,
      decimal InteresDiario
      )
    {
      using (var context = new lts_sindicatoDataContext())
      {

        VD_Cabecera VDCabecera = new VD_Cabecera
        {
          VerificacionId = InspectorVerificacionId,
          EmpresaCuit = cuit,
          Fecha = DateTime.Now,
          Numero = 0,
          Desde = desde,
          Hasta = hasta,
          Vencimiento = vencimiento,
          EmpresaId = empresaId,
          Capital = Capital,
          Interes = Interes,
          Total = Total,
          PlanDePago = PlanDePago,
          EmpleadosCantidad = cantidadEmpleados,
          InteresMensualAplicado = InteresMensual,
          InteresDiarioAplicado = InteresDiario
        };
        context.VD_Cabecera.InsertOnSubmit(VDCabecera);
        context.SubmitChanges();

      }
    }

    public static int GetVD_CabeceraId()
    {
      using (var context = new lts_sindicatoDataContext())
      {
        if (context.VD_Cabecera.Count() > 0)
        {
          return context.VD_Cabecera.Max(x => x.Id);
        }
        else
        {
          return 0;
        }
      }
    }

   public static int YaEstaAsignada(string cuit)
    {
      using (var context = new lts_sindicatoDataContext())
      {
        var YaAsignada = from a in context.InspectorVerificacion where a.CUIT == cuit && a.Estado == 0 select new {a.Id };
        if (YaAsignada.Count() > 0)
        {
          return YaAsignada.Single().Id;
        }
        else
        {
          return 0;
        }
      }
    }
  }
}



