using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entrega_cupones.Metodos
{
  class mtdRecibos
  {
    public static void AprobarRecibo(int NroRecibo)
    {
      using (var context = new lts_sindicatoDataContext())
      {
        var rcb = from a in context.Recibos where a.NroAutomatico == NroRecibo select a;
        
      }
    }
  }
}
