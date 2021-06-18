using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entrega_cupones.Metodos
{
  #region Descuentos
  //**** DESCUENTOS ******
  //  Sobre lo REMUNERATIVO calculamos:
  //11% de Jubilación
  //3% de Obra Social
  //3% según Ley 19.032
  //2% de Aporte Solidario y OBLIGATORIO con destino al Sindicato de Empleados de Comercio.Esté el empleado afiliado o no al sindicato. (según el Art. 100 del CCT 130/75)
  //0,5% de Aporte Solidario OBLIGATORIO con destino a FAECyS.Esté el empleado afiliado o no al sindicato.  (según el Art. 100 del CCT 130/75)
  //2% con destino al sindicato para empleados afiliados al gremio.Para este ejemplo consideré que no está afiliado por eso no se hizo el cálculo.Hay que tener en cuenta también que según la zona, este porcentaje puede cambiar.
  //$100 con  destino a OSECAC
  #endregion

  #region Remuneraciones
  // 1) Antiguedad: adicional por antigüedad es el 1% del básico de convenio por cada año de antigüedad.
  // Es decir ( sueldo basico *  años de antiguedad ) / 100
  // 2) Presentismo Según lo establecido en artículo 40 del CCT 130/75 el adicional por Asistencia y Puntualidad es la DOCEAVA parte de las remuneraciones del mes, en este caso las remuneraciones del mes son el Básico + Antigüedad.
  // Es decir (Sueldo Basico +  Antiguedad ) / 12 = Presentismo


  #endregion

  class mtdSueldos
  {
    //*** DESCUENTOS **** 
    public static decimal DescuentoJubilacion(decimal SueldoBasico)
    {
      decimal desc = SueldoBasico * (decimal)0.11;
      return desc;
    }
    
    public static decimal DescuentoObraSocial(decimal SueldoBasico, decimal ANR1,decimal ANR2)
    {
      decimal desc = (SueldoBasico + ANR1 + ANR2) * (decimal)0.03;
      return desc;
    }
    
    public static decimal DescuentoLey19302(decimal SueldoBasico)
    {
      decimal desc = SueldoBasico * (decimal)0.03;
      return desc;
    }
    
    public static decimal DescuentoAporteLey(decimal SueldoBasico,decimal ANR1, decimal ANR2)
    {
      decimal desc = ( SueldoBasico + ANR1 + ANR2) * (decimal)0.02;
      return desc;
    }
    
    public static decimal DescuentoAporteSocio(decimal SueldoBasico, bool EsSocio, decimal AporteAnterior, bool JornadaParcial)
    {
      decimal desc;
      if (JornadaParcial)
      {
        desc = EsSocio == true ? ((SueldoBasico * 2) * (decimal)0.02) - AporteAnterior : 0;
      }
      else
      {
        desc = EsSocio == true ? (SueldoBasico * (decimal)0.02) - AporteAnterior : 0;
      }
      return desc > 0 ? desc : 0; // si es positivo quiere decir que le pagan de mas, si es negativo quiere decir que es falta pagar el improte calculado
    }
    
    public static decimal DescuentoAporteSocioEscala(decimal SueldoBasico, bool EsSocio, decimal AporteAnterior, bool JornadaParcial)
    {
      decimal desc;
      if (JornadaParcial)
      {
        desc = EsSocio == true ? ((SueldoBasico * 2) * (decimal)0.02) : 0;
      }
      else
      {
        desc = EsSocio == true ? (SueldoBasico * (decimal)0.02) : 0;
      }
      return desc;
    }
    
    public static decimal DescuentoFAECyS(decimal SueldoBasico,decimal APNR1,decimal APNR2)
    {
      decimal desc = (SueldoBasico + APNR1+APNR2) * (decimal)0.005;
      return desc;
    }
    
    public static decimal DescuentoOSECAC()
    {
      decimal desc = 100;
      return desc;
    }

    //*** REMUNERACIONES **** 

    public static decimal GetAntiguedad(decimal SueldoBasico, int Antiguedad)
    {
      return (SueldoBasico * Antiguedad) / 100;
    }

    public static decimal GetPresentismo(decimal SueldoBasico, int Antiguedad)
    {
      //(Sueldo Basico + Antiguedad ) / 12 = Presentismo
      return (SueldoBasico + ((SueldoBasico * Antiguedad) / 100)) / 12;
    }

    public static decimal GetTotalHaberes(decimal SueldoBasico, int Antiguedad, decimal APNR1, decimal APNR2)
    {
      return (GetAntiguedad(SueldoBasico, Antiguedad) + GetPresentismo(SueldoBasico, Antiguedad) + SueldoBasico) + APNR1 + APNR2;
    }

    public static decimal GetTotalDescuentos(decimal SueldoBasico, bool EsSocio, decimal AporteAnterior, bool JornadaParcial,decimal ANR1,decimal ANR2)
    {
      decimal descuentos =
      DescuentoJubilacion(SueldoBasico) +
      DescuentoObraSocial(SueldoBasico,ANR1,ANR2) +
      DescuentoLey19302(SueldoBasico) +
      DescuentoAporteLey(SueldoBasico,ANR1,ANR2) +
      DescuentoAporteSocioEscala(SueldoBasico, EsSocio, AporteAnterior, JornadaParcial) +
      DescuentoFAECyS(SueldoBasico, ANR1,ANR2) +
      DescuentoOSECAC();

      return descuentos;
    }
    
    public static decimal GetSueldoDif(decimal SueldoBasico, bool EsSocio, decimal AporteAnterior, bool JornadaParcial, int Antiguedad, decimal SueldoDeclarado,decimal ANR1,decimal ANR2)
    {
      decimal haberes = SueldoBasico; //GetTotalHaberes(SueldoBasico, Antiguedad);
      decimal Descuentos = GetTotalDescuentos(SueldoBasico, EsSocio, AporteAnterior, JornadaParcial,ANR1,ANR2);
      decimal Diferencia = haberes - SueldoDeclarado;//(haberes - Descuentos) - SueldoDeclarado;

      return Diferencia > 0 ? Diferencia : 0;
    }
    
    public static decimal GetBasicoJubPres(decimal SueldoBasico, bool EsSocio, decimal AporteAnterior, bool JornadaParcial, int Antiguedad, decimal SueldoDeclarado)
    {
      decimal descuentos = SueldoBasico; //(SueldoBasico - GetTotalDescuentos(SueldoBasico, EsSocio, AporteAnterior, JornadaParcial));

      return descuentos;
    }
    
    public static decimal CalcularDiferencia(decimal Escala, decimal AporteSocio, bool Jornada)
    {
      decimal Diferencia = 0;

      if (Jornada && AporteSocio > 0) // Parcial
      {
        Diferencia = Escala * (decimal)0.02;
        if (AporteSocio < Diferencia)
        {
          Diferencia -= AporteSocio;
        }
      }
      return Diferencia;
    }

  }
}
