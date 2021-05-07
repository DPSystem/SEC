using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace entrega_cupones.Metodos
{
  class mtdFuncUtiles
  {
    public static string generar_ceros(string valor, int tamaño)
    {
      string ceros = null;
      for (int i = 0; i < tamaño - valor.Length; i++)
      {
        ceros += "0";
      }
      valor = ceros + valor;
      return valor;
    }
    public static string generar_blancos(string valor, int tamaño)
    {
      string blancos = null;
      for (int i = 0; i < tamaño - valor.Length; i++)
      {
        blancos += " ";
      }
      valor = valor + blancos;
      return valor;
    }
    public static int calcular_edad(DateTime fecha_nac)
    {

      int edad = 0;
      DateTime fecha_actual = DateTime.Today;
      edad = fecha_actual.Year - fecha_nac.Year;
      if ((fecha_actual.Month < fecha_nac.Month) || (fecha_actual.Month == fecha_nac.Month && fecha_actual.Day < fecha_nac.Day))
      {
        edad--;
      }
      return edad;
    }
    public static void limpiar_dgv(DataGridView dgv)
    {
      while (dgv.Rows.Count > 0)
      {
        dgv.Rows.RemoveAt(0);
      }
    }
    public static string GetDia31DelMes(string mes)
    {
      string _mes = string.Empty;
      if (mes == "04" || mes == "06" || mes == "09" || mes == "11")
      {
        _mes = "30";
      }
      if (mes == "01" || mes == "03" || mes == "05" || mes == "07" || mes == "08" || mes == "10" || mes == "12")
      {
        _mes = "31";
      }
      if (mes == "02")
      {
        _mes = "28";
      }
      return _mes;
    }
    public static int CalcularDias(DateTime Desde, DateTime Hasta)
    {
      DateTime FechaVencimientoPeriodo = Desde.AddMonths(1).AddDays(14);
      int dias_ = Convert.ToInt32((Hasta - FechaVencimientoPeriodo).TotalDays);
      int dias = (int)(Hasta -  Desde ).TotalDays;
      
      return dias > 0 ? dias : 0;
    }
    public static string GetLocalidad(int codloc)
    {
      string localidad = string.Empty;
      using (var context = new lts_sindicatoDataContext())
      {
        if (codloc != 0)
        {
          var loc = context.localidad.Where(x => x.MAELOC_CODLOC == codloc);
          if (loc.Count() > 0)
          {
            localidad = !string.IsNullOrEmpty(loc.First().MAELOC_NOMBRE) ? loc.First().MAELOC_NOMBRE.Trim() : "";
          }
        }
        else
        {
          localidad = "NO ASIGNADA";
        }
      }
      return localidad;
    }
  }
}

