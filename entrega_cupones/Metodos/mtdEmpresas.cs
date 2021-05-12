using entrega_cupones.Modelos;
using Microsoft.VisualBasic;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using entrega_cupones.Metodos;
using Org.BouncyCastle.Asn1.Mozilla;
using System.Windows.Forms;

namespace entrega_cupones.Metodos
{
  class mtdEmpresas
  {
    
    public static Empresa empresa = new Empresa();

    public static List<EstadoDDJJ> _ddjj = new List<EstadoDDJJ>();
    public static List<EstadoDDJJ> ListadoDDJJT(string cuit, DateTime desde, DateTime hasta, DateTime FechaVencimiento, int TipoInteres, decimal TazaInteres)
    {
      _ddjj.Clear();
      using (var context = new lts_sindicatoDataContext())
      {
        var _EstadoDDJJ = context.ddjjt.Where(x => x.CUIT_STR == cuit && (x.periodo >= desde && x.periodo <= hasta))
         .Select(row => new EstadoDDJJ
         {
           Periodo = Convert.ToDateTime(row.periodo),
           Rectificacion = (int)row.rect,
           AporteLey = (decimal)row.titem1,
           AporteSocio = (decimal)row.titem2,
           TotalSueldoEmpleados = (decimal)row.titem1 / Convert.ToDecimal(0.02),
           TotalSueldoSocios = (decimal)row.titem2 / Convert.ToDecimal(0.02),
           FechaDePago = row.fpago == null ? null : row.fpago,
           ImporteDepositado = (decimal)row.impban1,
           Empleados = context.ddjj.Where(x => x.CUIT_STR == cuit && (x.periodo == row.periodo) && (x.rect == row.rect)).Count(),
           Socios = context.ddjj.Where(x => x.CUIT_STR == cuit && (x.periodo == row.periodo) && x.rect == row.rect && x.item2 == true).Count(),
           Capital =
                    CalcularCapital((decimal)(row.impban1), (decimal)row.titem1, (decimal)row.titem2,
                    Convert.ToDateTime(row.fpago), FechaVencimiento, Convert.ToDateTime(row.periodo), TipoInteres, TazaInteres
           ),

           Interes =
                    CalcularInteres(Convert.ToDateTime(row.fpago), Convert.ToDateTime(row.periodo),
                    CalcularCapital((decimal)(row.impban1), (decimal)row.titem1, (decimal)row.titem2,
                    Convert.ToDateTime(row.fpago), FechaVencimiento, Convert.ToDateTime(row.periodo), TipoInteres, TazaInteres),
                    FechaVencimiento, TipoInteres, TazaInteres
           ),

           DiasDeMora = CalcularDias(Convert.ToDateTime(row.periodo), Convert.ToDateTime(row.fpago) == null ? FechaVencimiento : Convert.ToDateTime(row.fpago)), //Convert.ToDecimal(0.99)

           Total =
                  CalcularTotal(
                    CalcularCapital((decimal)(row.impban1), (decimal)row.titem1, (decimal)row.titem2, Convert.ToDateTime(row.fpago), FechaVencimiento, Convert.ToDateTime(row.periodo), TipoInteres, TazaInteres),

                    CalcularInteres(
                                     Convert.ToDateTime(row.fpago), Convert.ToDateTime(row.periodo),
                                     CalcularCapital(
                                                    (decimal)(row.impban1), (decimal)row.titem1, (decimal)row.titem2,
                                                    Convert.ToDateTime(row.fpago), FechaVencimiento, Convert.ToDateTime(row.periodo), TipoInteres, TazaInteres),
                                                    FechaVencimiento, TipoInteres, TazaInteres)),

           Acta = GetNroDeActa(Convert.ToDateTime(row.periodo), row.CUIT_STR),
           VerifDeuda = GetNroVerifDeuda(cuit, Convert.ToDateTime(row.periodo), Convert.ToInt32(row.rect), false, Convert.ToDateTime(row.fpago))


         });

        _ddjj.AddRange(_EstadoDDJJ);
        EliminarRectificacion();
        return _ddjj.Union(GenerarPerNoDec(desde, hasta, cuit)).OrderBy(x => x.Periodo).ToList();
      }

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

    public static decimal DifAporteSocioJorPar(string Cuit, DateTime Periodo, int Rectif)
    {
      List<EmpleadoAportePorPeriodo> lists = mtdEmpleados.ListadoEmpleadoAporte(Cuit, Periodo, Rectif);
      decimal DiferenciaAporteSocioJorPar = lists.Where(x => x.Jornada == "Parcial").Sum(x => x.AporteSocioDif);
      return DiferenciaAporteSocioJorPar;
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

    public static decimal CalcularTotal2(DateTime? FechaDePago, DateTime Periodo, decimal importe, DateTime FechaVencimiento, int TipoInteres, decimal TazaInteres)
    {


      decimal Interes = CalcularInteres(FechaDePago, Periodo, importe, FechaVencimiento, TipoInteres, TazaInteres);//CalcularInteres(FechaDePago, Periodo, importe, FechaVencimiento);
      if (FechaDePago == null)
      {
        return Interes + importe;//CalcularInteres(FechaDePago, Periodo, importe, taza, FechaVencimiento) + importe;
      }
      else
      {
        return Interes;
      }
    }
    public static void EliminarRectificacion()
    {
      var agrupado = _ddjj.GroupBy(x => x.Periodo).Where(x => x.Count() > 1);
      foreach (var item in agrupado)
      {
        foreach (var registro in item)
        {
          if (registro.ImporteDepositado == 0)
          {
            _ddjj.RemoveAll(x => x.Periodo == registro.Periodo && x.Rectificacion == registro.Rectificacion);
          }
        }
      }
    }
    public static List<EstadoDDJJ> GenerarPerNoDec(DateTime Desde, DateTime Hasta, string cuit)
    {
      List<EstadoDDJJ> Periodos = new List<EstadoDDJJ>();
      DateTime periodo = Desde; // Convert.ToDateTime("01/" + msk_Desde.Text);
      DateTime hasta = Hasta; // Convert.ToDateTime("01/" + msk_Hasta.Text);

      while (periodo <= hasta)
      {
        if (_ddjj.Where(x => x.Periodo == periodo).Count() == 0)
        {
          EstadoDDJJ PerNoDec = new EstadoDDJJ();
          PerNoDec.Periodo = periodo;
          PerNoDec.Rectificacion = 0;
          PerNoDec.AporteLey = 0;
          PerNoDec.AporteSocio = 0;
          PerNoDec.FechaDePago = null;
          PerNoDec.ImporteDepositado = 0;
          PerNoDec.Empleados = 0;
          PerNoDec.Socios = 0;
          PerNoDec.Capital = 0;
          PerNoDec.Interes = 0;
          PerNoDec.Total = 0;
          PerNoDec.Acta = GetNroDeActa(periodo, cuit);
          PerNoDec.VerifDeuda = GetNroVerifDeuda(cuit, periodo, 0, false, null);
          PerNoDec.PerNoDec = 1;
          Periodos.Add(PerNoDec);
        }
        periodo = periodo.AddMonths(1);
      }
      return Periodos;
    }
    public DataTable ImprimirDeuda(DataTable dt, List<EstadoDDJJ> deuda)
    {
      dt.Clear();

      foreach (var periodo in deuda)
      {
        DataRow row = dt.NewRow();
        row["NumeroDeActa"] = 0;
        row["Periodo"] = periodo.Periodo;
        row["CantidadDeEmpleados"] = periodo.Empleados;
        row["CantidadSocios"] = periodo.Socios;
        row["TotalSueldoEmpleados"] = periodo.TotalSueldoEmpleados;
        row["TotalSueldoSocios"] = periodo.TotalSueldoSocios;
        row["TotalAporteEmpleados"] = periodo.AporteLey;
        row["TotalAporteSocios"] = periodo.AporteSocio;
        row["FechaDePago"] = periodo.FechaDePago;
        row["ImporteDepositado"] = periodo.ImporteDepositado;
        row["DiasDeMora"] = periodo.DiasDeMora;
        row["DeudaGenerada"] = periodo.Capital;
        row["InteresGenerado"] = periodo.Interes;
        row["Total"] = periodo.Total;
        dt.Rows.Add(row);
      }
      return dt;
    }
    public static Empresa GetEmpresa(string Cuit)
    {
      using (var context = new lts_sindicatoDataContext())
      {
        var emp = context.maeemp.Where(x => x.MEEMP_CUIT_STR == Cuit).Select(
          x => new Empresa
          {
            MAEEMP_CUIT = (float)x.MAEEMP_CUIT,
            MAEEMP_NOMFAN = x.MAEEMP_NOMFAN.Trim(),
            MAEEMP_RAZSOC = x.MAEEMP_RAZSOC.Trim(),
            MAEEMP_CALLE = x.MAEEMP_CALLE.Trim(),
            MAEEMP_NRO = x.MAEEMP_NRO.Trim(),
            MAEEMP_CODPROV = x.MAEEMP_CODPROV.ToString(),
            MAEEMP_CODLOC = x.MAEEMP_CODLOC.ToString(),
            MAEEMP_CODPOS = x.MAEEMP_CODPOS.ToString(),
            MAEEMP_EMAIL = x.MAEEMP_EMAIL.ToString(),
            //MAEEMP_CREDMAX =  x.MAEEMP_CREDMAX,
            MAEEMP_CONDCRED = x.MAEEMP_CONDCRED.ToString(),
            //MAEEMP_CONDIVA =  ,
            //MAEEMP_ACTUALIZA = (bool) x.MAEEMP_ACTUALIZA ,
            MAEEMP_ESTUDIO_CONTACTO = x.MAEEMP_ESTUDIO_CONTACTO.ToString(),
            MAEEMP_ESTUDIO_TEL = x.MAEEMP_ESTUDIO_TEL.ToString(),
            MAEEMP_ESTUDIO_EMAIL = x.MAEEMP_ESTUDIO_EMAIL.ToString(),
            MEEMP_CUIT_STR = x.MEEMP_CUIT_STR
          }).FirstOrDefault();
        empresa = emp;
        return empresa;
      }
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

    public static List<EstadoDDJJ> VD_ListadoDDJJT(string cuit, DateTime desde, DateTime hasta, DateTime FechaVencimiento, int TipoInteres, decimal TazaInteres, int VDId)
    {
      _ddjj.Clear();
      using (var context = new lts_sindicatoDataContext())
      {
        var  _EstadoDDJJ = context.VD_Detalle.Where(x => x.VDInspectorId == VDId)
         .Select(row => new EstadoDDJJ
         {
           Periodo = row.Periodo.Date, // Convert.ToDateTime(row.Periodo),
           Rectificacion = (int)row.Rectificacion,
           AporteLey = (decimal)row.TotalAporteEmpleados,
           AporteSocio = (decimal)row.TotalAporteSocios,
           TotalSueldoEmpleados = (decimal)row.TotalSueldoEmpleados, /*.titem1 / Convert.ToDecimal(0.02)*/
           TotalSueldoSocios = (decimal)row.TotalSueldoSocios, //.titem2 / Convert.ToDecimal(0.02),
           FechaDePago = row.FechaDePago == null ? null : row.FechaDePago, //.fpago == null ? null : row.fpago,
           ImporteDepositado = (decimal)row.ImporteDepositado, //row.impban1,
           Empleados = row.CantidadEmpleados,//context.ddjj.Where(x => x.CUIT_STR == cuit && (x.periodo == row.periodo) && (x.rect == row.rect)).Count(),
           Socios = row.CantidadSocios,//context.ddjj.Where(x => x.CUIT_STR == cuit && (x.periodo == row.periodo) && x.rect == row.rect && x.item2 == true).Count(),
           Capital =
                    CalcularCapital((decimal)row.ImporteDepositado, (decimal)row.TotalSueldoEmpleados, (decimal)row.TotalSueldoSocios,
                    Convert.ToDateTime(row.FechaDePago), FechaVencimiento, Convert.ToDateTime(row.Periodo), TipoInteres, TazaInteres
           ),

           Interes =
                    CalcularInteres(Convert.ToDateTime(row.FechaDePago), Convert.ToDateTime(row.Periodo),
                    CalcularCapital((decimal)(row.ImporteDepositado), (decimal)row.TotalSueldoEmpleados, (decimal)row.TotalSueldoSocios,
                    Convert.ToDateTime(row.FechaDePago), FechaVencimiento, Convert.ToDateTime(row.Periodo), TipoInteres, TazaInteres),
                    FechaVencimiento, TipoInteres, TazaInteres
           ),

           DiasDeMora = CalcularDias(Convert.ToDateTime(row.Periodo), Convert.ToDateTime(row.FechaDePago) == null ? FechaVencimiento : Convert.ToDateTime(row.FechaDePago)), //Convert.ToDecimal(0.99)

           Total =
                  CalcularTotal(
                    CalcularCapital((decimal)(row.ImporteDepositado), (decimal)row.TotalSueldoEmpleados, (decimal)row.TotalSueldoSocios, Convert.ToDateTime(row.TotalSueldoSocios), FechaVencimiento, Convert.ToDateTime(row.Periodo), TipoInteres, TazaInteres),

                    CalcularInteres(
                                     Convert.ToDateTime(row.FechaDePago), Convert.ToDateTime(row.Periodo),
                                     CalcularCapital(
                                                    (decimal)(row.ImporteDepositado), (decimal)row.TotalSueldoEmpleados, (decimal)row.TotalSueldoSocios,
                                                    Convert.ToDateTime(row.FechaDePago), FechaVencimiento, Convert.ToDateTime(row.Periodo), TipoInteres, TazaInteres),
                                                    FechaVencimiento, TipoInteres, TazaInteres)),

           Acta = GetNroDeActa(Convert.ToDateTime(row.Periodo), mtdVDInspector.Get_VDCuit((int)row.VDInspectorId)),
           VerifDeuda = GetNroVerifDeuda(cuit, Convert.ToDateTime(row.Periodo), Convert.ToInt32(row.Rectificacion), false, Convert.ToDateTime(row.FechaDePago))
         });

        //( _ddjj.AddRange(_EstadoDDJJ);
        //EliminarRectificacion();
        //_ddjj.Union(GenerarPerNoDec(desde, hasta, cuit)).OrderBy(x => x.Periodo).ToList();
        //return _ddjj.OrderBy(x => x.Periodo).ToList();
        return (List<EstadoDDJJ>)_EstadoDDJJ;
      }
    }
  }
}
