using entrega_cupones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entrega_cupones.Metodos
{
  class mtdInspectores
  {
    public static List<mdlInspector> Get_Inspectores()
    {
      using (var context = new lts_sindicatoDataContext())
      {
        var inspectores = (from a in context.inspectores
                           where a.ID_INSPECTOR != 7
                           select new mdlInspector
                           {
                             Id = a.ID_INSPECTOR,
                             Apellido = a.APELLIDO,
                             Nombre = a.APELLIDO + " " + a.NOMBRE,
                             Estudio = (int)a.ESTUDIO
                           }).OrderBy(x => x.Apellido).ThenBy(x => x.Nombre);
        return inspectores.ToList();

      }
    }

    public static mdlInspector Get_Inspector(int InspectorId)
    {
      using (var context = new lts_sindicatoDataContext())
      {
        var inspector = (from a in context.inspectores
                           where a.ID_INSPECTOR ==  InspectorId
                           select new mdlInspector
                           {
                             Id = a.ID_INSPECTOR,
                             Apellido = a.APELLIDO,
                             Nombre = a.APELLIDO + " " + a.NOMBRE,
                             Estudio = (int)a.ESTUDIO
                           }).OrderBy(x => x.Apellido).ThenBy(x => x.Nombre);
        return inspector.Single();

      }
    }
  }
}
