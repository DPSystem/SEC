using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace entrega_cupones.Metodos
{
  class mtdIntereses
  {
    public static decimal GetInteresAFIP(DateTime Periodo, DateTime FechaDePago, decimal ImporteDeuda, int TipoInteres, DateTime FechaDeVencimiento, bool CoeficienteB)
    {

      //if (FechaDePago == Convert.ToDateTime("01/01/0001"))
      //{
      //  FechaDePago = FechaDeVencimiento;
      //}

      //if (CoeficienteB)
      //{
      //  FechaDeVencimiento = Periodo.AddMonths(1).AddDays(14);
      //  FechaDePago = FechaDeVencimiento;
      //}
      //else
      //{
      //  FechaDeVencimiento = Periodo.AddMonths(1).AddDays(14);
      //}

      ////FechaDeVencimiento = Periodo.AddMonths(1).AddDays(14);

      DateTime Aux = FechaDeVencimiento;
      if (FechaDePago == null || FechaDePago == Convert.ToDateTime("01/01/0001"))
      {
        FechaDeVencimiento = Periodo.AddMonths(1).AddDays(14);
        FechaDePago = Aux;
      }
      else
      {
        if (CoeficienteB == true)
        {
          FechaDeVencimiento = FechaDePago;
          FechaDePago = Aux;
        }
        else
        {
          FechaDeVencimiento = Periodo.AddMonths(1).AddDays(14);
          //FechaDePago = Aux;
        }
        //FechaDeVencimiento = FechaDePago;
        //FechaDePago = Aux;
      }  

      decimal interes = 0;
      int dias = 0;
      decimal interesDiario = 0;
      decimal interesPeriodo = 0;
      decimal interesTotal = 0;

      using (var context = new lts_sindicatoDataContext())
      {
        var Inicio = context.Intereses.Where(x => x.TipoDeInteres == TipoInteres && FechaDeVencimiento >= x.Desde && FechaDeVencimiento <= x.Hasta).Single();

        var Final = context.Intereses.Where(x => x.TipoDeInteres == TipoInteres && FechaDePago >= x.Desde && FechaDePago <= x.Hasta).Single();

        var Periodos = context.Intereses.Where(x => x.TipoDeInteres == TipoInteres && x.Id >= Inicio.Id && x.Id <= Final.Id).OrderBy(x => x.Desde);

        foreach (var item in Periodos)
        {
          interesDiario = 0;
          interesPeriodo = 0;
          if (FechaDeVencimiento < FechaDePago) //Para no calcular interes
          {

            if (Inicio.Id == Final.Id) //para saber si estamos en un solo intervalo
            {
              dias = Convert.ToInt32((FechaDePago - FechaDeVencimiento).TotalDays);
            }
            else
            {
              if (Inicio.Id == item.Id) // este es el primer registro
              {
                dias = Convert.ToInt32((Convert.ToDateTime(item.Hasta) - FechaDeVencimiento).TotalDays);
              }

              if (Final.Id == item.Id) // Este Es el ultimo registro
              {
                dias = Convert.ToInt32((FechaDePago - Convert.ToDateTime(item.Desde)).TotalDays);
              }
              if (item.Id > Inicio.Id && item.Id < Final.Id)
              {
                dias = Convert.ToInt32((Convert.ToDateTime(item.Hasta) - Convert.ToDateTime(item.Desde)).TotalDays);
              }
            }
            interesDiario = Convert.ToDecimal(dias * item.Diario); //dias > 90 ?  Convert.ToDecimal((dias - 1) * item.Diario) : Convert.ToDecimal(dias * item.Diario);
            interesPeriodo = (Convert.ToDecimal(ImporteDeuda) * interesDiario) / 100;
            interesTotal += interesPeriodo;

            interes += (Math.Round(Convert.ToDecimal(ImporteDeuda), 6) * Convert.ToDecimal(dias * item.Diario)) / 100;
          }
          else
          {
            interes = 0;
          }
        }
      }
      return interes;
    }
    public static decimal GetInteresAFIP2(DateTime Periodo, DateTime FechaDePago, decimal ImporteDeuda, int TipoInteres, DateTime FechaDeVencimiento, bool CoeficienteB)
    {

      if (FechaDePago == Convert.ToDateTime("01/01/0001"))
      {
        FechaDePago = FechaDeVencimiento;
      }

      if (CoeficienteB)
      {
        FechaDeVencimiento = Periodo.AddMonths(1).AddDays(14); 
        FechaDePago = FechaDeVencimiento;
      }
      else
      {
        FechaDeVencimiento = Periodo.AddMonths(1).AddDays(14);
      }

      //FechaDeVencimiento = Periodo.AddMonths(1).AddDays(14);

      decimal interes = 0;
      int dias = 0;
      decimal interesDiario = 0;
      decimal interesPeriodo = 0;
      decimal interesTotal = 0;

      using (var context = new lts_sindicatoDataContext())
      {
        var Inicio = context.Intereses.Where(x => x.TipoDeInteres == TipoInteres && FechaDeVencimiento >= x.Desde && FechaDeVencimiento <= x.Hasta).Single();

        var Final = context.Intereses.Where(x => x.TipoDeInteres == TipoInteres && FechaDePago >= x.Desde && FechaDePago <= x.Hasta).Single();

        var Periodos = context.Intereses.Where(x => x.TipoDeInteres == TipoInteres && x.Id >= Inicio.Id && x.Id <= Final.Id).OrderBy(x => x.Desde);

        foreach (var item in Periodos)
        {
          interesDiario = 0;
          interesPeriodo = 0;
          if (FechaDeVencimiento < FechaDePago) //Para no calcular interes
          {

            if (Inicio.Id == Final.Id) //para saber si estamos en un solo intervalo
            {
              dias = Convert.ToInt32((FechaDePago - FechaDeVencimiento).TotalDays);
            }
            else
            {
              if (Inicio.Id == item.Id) // este es el primer registro
              {
                dias = Convert.ToInt32((Convert.ToDateTime(item.Hasta) - FechaDeVencimiento).TotalDays);
              }

              if (Final.Id == item.Id) // Este Es el ultimo registro
              {
                dias = Convert.ToInt32((FechaDePago - Convert.ToDateTime(item.Desde)).TotalDays);
              }
              if (item.Id > Inicio.Id && item.Id < Final.Id)
              {
                dias = Convert.ToInt32((Convert.ToDateTime(item.Hasta) - Convert.ToDateTime(item.Desde)).TotalDays);
              }
            }
            interesDiario = Convert.ToDecimal(dias * item.Diario); //dias > 90 ?  Convert.ToDecimal((dias - 1) * item.Diario) : Convert.ToDecimal(dias * item.Diario);
            interesPeriodo = (Convert.ToDecimal(ImporteDeuda) * interesDiario) / 100;
            interesTotal += interesPeriodo;

            interes += (Math.Round(Convert.ToDecimal(ImporteDeuda), 6) * Convert.ToDecimal(dias * item.Diario)) / 100;
          }
          else
          {
            interes = 0;
          }
        }
      }
      return interes;
    }

    public static decimal GetInteresResar(DateTime Periodo, DateTime FechaDePago, decimal ImporteDeuda, int TipoInteres, DateTime FechaDeVencimiento)
    {
      if (FechaDePago == Convert.ToDateTime("01/01/0001"))
      {
        FechaDePago = FechaDeVencimiento;
      }

      decimal interes = 0;
      int dias = 0;

      using (var context = new lts_sindicatoDataContext())
      {
        var Inicio = context.Intereses.Where(x => x.TipoDeInteres == TipoInteres && FechaDeVencimiento >= x.Desde && FechaDeVencimiento <= x.Hasta).Single();

        var Final = context.Intereses.Where(x => x.TipoDeInteres == TipoInteres && FechaDePago >= x.Desde && FechaDePago <= x.Hasta).Single();

        var Periodos = context.Intereses.Where(x => x.TipoDeInteres == TipoInteres && x.Id >= Inicio.Id && x.Id <= Final.Id).OrderBy(x => x.Desde);

        foreach (var item in Periodos)
        {
          if (FechaDeVencimiento < FechaDePago) //Para no calcular interes
          {

            if (Inicio.Id == Final.Id) //para saber si estamos en un solo intervalo
            {
              dias = Convert.ToInt32((FechaDePago - FechaDeVencimiento.AddDays(0)).TotalDays);
            }
            else
            {
              if (Inicio.Id == item.Id) // este es el primer registro
              {
                dias = Convert.ToInt32((Convert.ToDateTime(item.Hasta) - FechaDeVencimiento.AddDays(0)).TotalDays);
              }

              if (Final.Id == item.Id) // Este Es el ultimo registro
              {
                dias = Convert.ToInt32((FechaDePago - Convert.ToDateTime(item.Desde)).TotalDays);
              }

              if (item.Id > Inicio.Id && item.Id < Final.Id)
              {
                dias = Convert.ToInt32((Convert.ToDateTime(item.Hasta) - Convert.ToDateTime(item.Desde)).TotalDays);
              }
            }

            interes += (Math.Round(Convert.ToDecimal(ImporteDeuda), 4) * Convert.ToDecimal(dias * item.Diario)) / 100;
          }
          else
          {
            interes = 0;
          }
        }
      }
      return interes;
    }

    public static decimal CalcularInteresAFIP(DateTime? FechaDePago, DateTime Periodo, DateTime FechaDeVencimientoDeuda, decimal Importe,int TipoDeInteres, decimal TazaInteres)
    {
      DateTime FechaDeVencimientoPeriodo = Periodo.AddMonths(1).AddDays(14);
      decimal Interes = 0;
      if (FechaDePago == Convert.ToDateTime("01/01/0001") || FechaDePago == null)
      {
        Interes = GetCoeficienteB(FechaDeVencimientoPeriodo, FechaDeVencimientoPeriodo, Importe, TipoDeInteres, TazaInteres, FechaDeVencimientoDeuda, FechaDePago,Periodo);
      }
      else
      {
        if (FechaDePago > FechaDeVencimientoPeriodo)
        {
          Interes = GetCoeficienteB(Convert.ToDateTime(FechaDePago), FechaDeVencimientoDeuda, Importe, TipoDeInteres, TazaInteres, FechaDeVencimientoDeuda, FechaDePago,Periodo);
        }
      }
      return Interes;
    }
    public static decimal GetInteresManual(DateTime? FechaDePago, DateTime Periodo, DateTime FechaDeVencimientoDeuda, decimal Importe, decimal TazaInteres)
    {
      DateTime FechaDeVencimientoPeriodo = Periodo.AddMonths(1).AddDays(14);
      decimal Interes = 0;
      if (FechaDePago == Convert.ToDateTime("01/01/0001") || FechaDePago == null)
      {
        Interes = GetCoeficienteB(FechaDeVencimientoPeriodo, FechaDeVencimientoPeriodo, Importe,0, TazaInteres, FechaDeVencimientoDeuda,FechaDePago,Periodo);
      }
      else
      {
        if (FechaDePago > FechaDeVencimientoPeriodo)
        {
          Interes = GetCoeficienteB(Convert.ToDateTime(FechaDePago), FechaDeVencimientoDeuda, Importe,0, TazaInteres, FechaDeVencimientoDeuda, FechaDePago,Periodo);
        }
      }
      return Interes;
    }

    //public static decimal CalcularInteresManual(DateTime Desde, DateTime Hasta, decimal Importe, decimal TazaDeInteres, DateTime FechaDeVencimientoDeDeuda)

    //{
    //  //Coeficiente A
    //  int dias = mtdFuncUtiles.CalcularDias(Desde, Hasta);
    //  var interes = (dias * TazaDeInteres * Importe) / 100;

    //  //Coeficiente B
    //  dias = mtdFuncUtiles.CalcularDias(Hasta, FechaDeVencimientoDeDeuda);
    //  interes += (dias * TazaDeInteres * Importe) / 100;

    //  return interes;
    //}

    public static decimal GetCoeficienteA(DateTime Desde, DateTime Hasta, decimal Importe, int TipoInteres, decimal TazaDeInteres,DateTime Periodo, DateTime FechaDeVencimientoDeActa)
    {
      //Coeficiente A
      int dias = mtdFuncUtiles.CalcularDias(Desde, Hasta);
      decimal interes = 0;
      if (TipoInteres == 0) // TIpoInteres == 0 
      {
        interes = (dias * TazaDeInteres * Importe) / 100; 
      }
      else
      {
        interes = GetInteresAFIP(Periodo, Hasta, Importe, TipoInteres, FechaDeVencimientoDeActa,false);
      }
      //var interes = (dias * TazaDeInteres * Importe) / 100;
      return interes;
    }

    public static decimal GetCoeficienteB(DateTime Desde, DateTime Hasta, decimal Importe, int TipoDeInteres, decimal TazaDeInteres, DateTime FechaDeVencimientoDeDeuda, DateTime? FechaDePago, DateTime Periodo)
    {
      //Coeficiente B
      int dias = 0;
      decimal interes = 0;
      if (FechaDePago == Convert.ToDateTime("01/01/0001") || FechaDePago == null)
      {
        if (TipoDeInteres == 0)
        {
          dias = mtdFuncUtiles.CalcularDias(Desde, FechaDeVencimientoDeDeuda);
          interes = ((dias * TazaDeInteres * Importe) / 100);
        }
        else
        {

          interes = GetInteresAFIP(Periodo,Convert.ToDateTime(FechaDePago),Importe,1,FechaDeVencimientoDeDeuda,true);
            //GetInteresAFIP(Periodo, Hasta, Importe, TipoInteres, FechaDeVencimientoDeActa);
        }


        //dias = mtdFuncUtiles.CalcularDias(Desde, FechaDeVencimientoDeDeuda);
        //interes = ((dias * TazaDeInteres * Importe) / 100);
      }
      else
      {
        if (TipoDeInteres == 0)
        {
          dias = mtdFuncUtiles.CalcularDias(Convert.ToDateTime(FechaDePago), FechaDeVencimientoDeDeuda);
          interes = (dias * TazaDeInteres * Importe) / 100;
        }
        else
        {

          interes = GetInteresAFIP(Periodo, Convert.ToDateTime(FechaDePago), Importe, 1, FechaDeVencimientoDeDeuda,true);
          //GetInteresAFIP(Periodo, Hasta, Importe, TipoInteres, FechaDeVencimientoDeActa);
        }


        dias = mtdFuncUtiles.CalcularDias(Convert.ToDateTime(FechaDePago), FechaDeVencimientoDeDeuda);
        interes = (dias * TazaDeInteres * Importe) / 100;

        //dias = mtdFuncUtiles.CalcularDias(Convert.ToDateTime(FechaDePago), FechaDeVencimientoDeDeuda);
        //interes = (dias * TazaDeInteres * Importe) / 100;
      }

      return interes;
    }

    public static decimal CalcularInteres(DateTime? FechaDePago, DateTime Periodo, decimal importe, DateTime FechaVencimiento, int TipoInteres, decimal Interes)
    {
      decimal interes;
      if (TipoInteres == 1)
      {
        //interes = mtdIntereses.GetInteresAFIP(Periodo, Convert.ToDateTime(FechaDePago), importe, TipoInteres, FechaVencimiento); //CalcularInteres(FechaDePago, Periodo, importe, FechaVencimiento);
        //CalcularInteresAFIP
        interes = mtdIntereses.CalcularInteresAFIP(FechaDePago, Periodo, FechaVencimiento, importe, TipoInteres, Interes); //CalcularInteres(FechaDePago, Periodo, importe, FechaVencimiento);
      }
      else
      {
        interes = mtdIntereses.GetInteresManual(FechaDePago, Periodo, FechaVencimiento, importe, Interes); //CalcularInteresManual(Periodo,FechaVencimiento,importe,Interes);
      }
      return interes;
    }
  }
}
