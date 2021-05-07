using entrega_cupones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace entrega_cupones.Metodos
{
  class mtdActas
  {
    public static  mdlActa ActaReturn = new mdlActa();
    
    public static int ObtenerNroDeActa()
    {
      using (var context = new lts_sindicatoDataContext())
      {
        return context.Acta.Count() > 0 ? context.Acta.Max(x => x.Numero) + 1 : 1;
      }
    }
    
    public static void GuardarActaCabecera(List<EstadoDDJJ> ddjjt, DateTime FechaDeConfeccion, DateTime desde, DateTime hasta, DateTime vencimiento, int empresaId, string cuit, int cantidadEmpleados, decimal InteresMensual, decimal InteresDiario,List<mdlCuadroAmortizacion> _PlanDePago)
    {
      using (var context = new lts_sindicatoDataContext())
      {
        int NroDeActa = ObtenerNroDeActa();
        
        int NroDePlan =  mtdCobranzas.AsentarPlan(cuit,NroDeActa,_PlanDePago);

        MessageBox.Show("Se grabo el Plan de Pago con exito");

        Acta acta = new Acta();
        acta.Fecha = FechaDeConfeccion;
        acta.Numero = NroDeActa;
        acta.EmpresaCuit = cuit;
        acta.Desde = desde;
        acta.Hasta = hasta;
        acta.Vencimiento = vencimiento;
        acta.EmpresaId = empresaId;
        acta.Capital = Math.Round(ddjjt.Sum(x => x.Capital), 2);
        acta.EmpresaCuit = cuit;
        acta.Interes = Math.Round(ddjjt.Sum(x => x.Interes), 2);
        acta.Total = Math.Round(ddjjt.Sum(x => x.Total), 2);
        acta.PlanDePago = NroDePlan;
        acta.InspectorId = 0;
        acta.EmpleadosCantidad = cantidadEmpleados;
        acta.InteresMensualAplicado = InteresMensual;
        acta.InteresDiarioAplicado = InteresDiario;

        context.Acta.InsertOnSubmit(acta);
        context.SubmitChanges();

        MessageBox.Show("Se grabo el Acta con exito");
        int actaId = context.Acta.Where(x => x.EmpresaCuit == acta.EmpresaCuit && x.Numero == acta.Numero).SingleOrDefault().Id;

        GuardarActaDetalle(ddjjt, acta.Numero, acta.EmpresaCuit, actaId);
        MessageBox.Show("Se grabo el detalle del Acta con exito");
      }
    }
    
    public static void GuardarActaDetalle(List<EstadoDDJJ> ddjjt, int actaNumero, string actaCuit, int actaId)
    {
      using (var context = new lts_sindicatoDataContext())
      {
        foreach (var periodo in ddjjt)
        {
          ActasDetalle actadet = new ActasDetalle
          {
            NumeroDeActa = actaNumero,
            ActaId = actaId,
            Periodo = periodo.Periodo,
            CantidadEmpleados = periodo.Empleados,
            CantidadSocios = periodo.Socios,
            TotalSueldoEmpleados = periodo.TotalSueldoEmpleados,
            TotalSueldoSocios = periodo.TotalSueldoSocios,
            TotalAporteEmpleados = periodo.AporteLey,
            TotalAporteSocios =periodo.AporteSocio,
            FechaDePago = Convert.ToDateTime(periodo.FechaDePago),
            ImporteDepositado = periodo.ImporteDepositado,
            DiasDeMora = periodo.DiasDeMora,
            DeudaGenerada = periodo.Capital,
            InteresGenerado = periodo.Interes,
            Total = periodo.Total,
            PerNoDec = periodo.PerNoDec

          };
          context.ActasDetalle.InsertOnSubmit(actadet);
          context.SubmitChanges();
        }
      }
    }
    
    public static  mdlActa GetActa (int NroDeActa)
    {
      using (var context = new lts_sindicatoDataContext())
      {
        var acta = from a in context.Acta.Where(x => x.Numero == NroDeActa)
                   join b in context.maeemp on a.EmpresaCuit equals b.MEEMP_CUIT_STR
                   select new mdlActa
                   {
                     NroActa = a.Numero,
                     Cuit = a.EmpresaCuit,
                     Domicilio = b.MAEEMP_CALLE.Trim() + " " + b.MAEEMP_NRO,
                     RazonSocial = b.MAEEMP_RAZSOC,
                     Desde = (DateTime)a.Desde,
                     Hasta = (DateTime)a.Hasta,
                     Importe = a.Total,
                     NroDePlan = a.PlanDePago
                   };
        ActaReturn = acta.FirstOrDefault();
        return  ActaReturn;
      }
    }

    public static List<mdlDeudaParaRanking> DeudaParaRanking()
    {
      DateTime FechaVacia = Convert.ToDateTime("01/01/0001");
      List<mdlDeudaParaRanking> deudaParaRanking = new List<mdlDeudaParaRanking>();

      using (var context = new lts_sindicatoDataContext())
      {
        foreach (var empresa in context.maeemp)
        {
          mdlDeudaParaRanking dpr = new mdlDeudaParaRanking();
          dpr.Cuit = empresa.MEEMP_CUIT_STR;
          dpr.Empresa = empresa.MAEEMP_RAZSOC.Trim();
          dpr.Deuda = CalcularDeudaRanking(empresa.MEEMP_CUIT_STR);
          if (dpr.Deuda > 0 )
          {
            deudaParaRanking.Add(dpr);
          }
        }
      }
      
      return deudaParaRanking.OrderByDescending(x=>x.Deuda).ToList();

    }

    public static decimal CalcularDeudaRanking(string cuit)
    {
      //List<mdlDeudaParaRanking> deudaParaRanking = new List<mdlDeudaParaRanking>();
      using (var context = new lts_sindicatoDataContext())
      {
        var deuda = context.ddjjt.Where(x => x.CUIT_STR == cuit && x.fpago == null).Sum(x => x.titem1 + x.titem2);



        //var ddjj = context.ddjjt.Where(x => x.CUIT_STR == cuit && x.fpago == null).Sum(x=>x.titem1 + x.titem2);


        return Convert.ToDecimal(deuda);
      }
    }
  }
}
