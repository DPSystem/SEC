using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entrega_cupones.Clases
{
  public class Cobrador
  {
    
    public class cbr
    {
      public IEnumerable<Cobradores> Cobra { get; set; }
    }
    public cbr cobrador_ = new cbr();
    public IEnumerable<Cobradores> getCobradores()
    {
      using (var context = new lts_sindicatoDataContext())
      {
        cobrador_.Cobra = context.Cobradores.Where(x => x.Estado == 1).OrderBy(x => x.Apellido).ThenBy(x => x.Nombre);
        return cobrador_.Cobra;
        //return context.Cobradores.Where (x=>x.Estado == 1)
        //  .Select(x=>new {nombre =  x.Apellido + " " + x.Nombre, ID = x.ID,DNI = x.DNI });
      }
    }

    public string GetNombreDeCobradorPorID(int id) 
    {
      using (var context = new lts_sindicatoDataContext())
      {
        var nombre = (from a in context.Usuarios where a.idUsuario == id select new { Emisor = a.Apellido }).ToList();
        return  nombre.Count() > 0 ?  nombre.Single().Emisor.Trim():  "";
      }
    }

  }
}
