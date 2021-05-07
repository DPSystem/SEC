using entrega_cupones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entrega_cupones.Metodos
{
  class mtdCategorias
  {
    public static List<mdlCategoriaEmpleado> Get_CategoriaConSueldo(DateTime Periodo, int Jornada)
    {
      List<mdlCategoriaEmpleado> cat = new List<mdlCategoriaEmpleado>();
      using (var context = new lts_sindicatoDataContext())
      {
        var Categoria = (from a in context.categorias_empleado
                         join b in context.EscalaSalarial on a.MAECAT_CODCAT equals b.CodCategoria
                         where Periodo == b.Periodo
                         select new mdlCategoriaEmpleado
                         {
                           Id = a.Id,
                           CodigoCategoria = (int)a.MAECAT_CODCAT,
                           Descripcion = a.MAECAT_NOMCAT + " ( $ " + (Jornada == 1 ? b.Importe : b.Importe / 2).ToString() + " )",
                           Importe = Convert.ToDecimal(Jornada == 1 ? b.Importe : b.Importe / 2)
                         }).OrderBy(x => x.Descripcion);
        cat.AddRange(Categoria);
      }
      return cat;
    }

    public static string GetImporteDescripcion(decimal Importe, int Jornada)
    {
      string ImporteStr = (Jornada == 1 ? Importe : Importe / 2).ToString();
      ImporteStr = string.Format("0.00", ImporteStr);
      return ImporteStr;
    }

    public static decimal GetSueldoDeCategoria(int CodigoCategoria, DateTime Periodo,int Jornada)
    {
      using (var context =  new lts_sindicatoDataContext())
      {
        var s =  from a in context.EscalaSalarial where a.CodCategoria == CodigoCategoria && a.Periodo == Periodo select a;

        return (decimal)  (Jornada == 1 ? s.FirstOrDefault().Importe  : s.FirstOrDefault().Importe / 2);
      }
    }
  }
}
