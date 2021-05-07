using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entrega_cupones.Clases
{
  public class Localidad
  {

    public IEnumerable<localidades> GetLocalidades()
    {
      using (var context = new lts_sindicatoDataContext())
      {
        IEnumerable<localidades> localidad = context.localidades.Where(x => x.idprovincias == 14).ToList();

        //var localidad = (from a in context.localidades
        //                where a.idprovincias == 14
        //                orderby a.nombre
        //                select new
        //                {
        //                  Id = a.ID,
        //                  nombre = a.nombre + "[ " + a.codigopostal + " ]",
        //                  codigopostal = a.codigopostal,
        //                  idlocalicades =  a.idlocalidades,
        //                  idprovincias = a.idprovincias
        //                }).ToList();

        return localidad;
      }
    }

    public string GetCodigoPostal(string cuit)
    {
      using (var context = new lts_sindicatoDataContext())
      {
        var codigoPostal = context.maeemp.Where(x => x.MEEMP_CUIT_STR == cuit);
        if (codigoPostal.Count() > 0)
        {
          return codigoPostal.First().MAEEMP_CODPOS.First().ToString();
        }
        else
        {
          return "0";
        }
      }
    }
  }
}
