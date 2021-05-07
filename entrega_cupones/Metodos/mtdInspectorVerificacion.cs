using entrega_cupones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entrega_cupones.Metodos
{
  class mtdInspectorVerificacion
  {
    public static int InsertInspectorVerificacion(int IdInspector, string Cuit)
    {
      using (var context = new lts_sindicatoDataContext())
      {
        var InspectorVerif = new InspectorVerificacion();
        InspectorVerif.IdInspector = IdInspector;
        InspectorVerif.FechaAsignacion = DateTime.Now;
        InspectorVerif.CUIT = Cuit;
        context.InspectorVerificacion.InsertOnSubmit(InspectorVerif);
        context.SubmitChanges();
        return context.InspectorVerificacion.Max(x => x.Id); //retorna el Id del al relacion inspector verificacion de deuca
      }
    }
  }
}
