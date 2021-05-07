using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entrega_cupones.Clases
{
  public class Inspector
  {
    public IEnumerable<inspectores> Get_Inspectores()
    {
      using (var context = new lts_sindicatoDataContext())
      {
        IEnumerable<inspectores> inspectores = context.inspectores.OrderBy(x => x.APELLIDO).OrderBy(x=>x.APELLIDO).ToList();
        return inspectores;
      }
    }
  }
}
