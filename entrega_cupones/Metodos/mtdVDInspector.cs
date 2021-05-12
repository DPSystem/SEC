using entrega_cupones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entrega_cupones.Metodos
{
  class mtdVDInspector
  {
    public static int Insert_VDInspector(VD_Inspector VDInspector)//int IdInspector, string Cuit,int UserId)
    {
      using (var context = new lts_sindicatoDataContext())
      {
        context.VD_Inspector.InsertOnSubmit(VDInspector);
        context.SubmitChanges();
        return context.VD_Inspector.Max(x => x.Id); //retorna el Id del al relacion inspector verificacion de deuca
      }
    }

    public static void Update_VDInspector(VD_Inspector VDInspector)
    {
      using (var context = new lts_sindicatoDataContext())
      {
        context.SubmitChanges();
      }
    }

    public static int YaEstaAsignada(string cuit)
    {
      using (var context = new lts_sindicatoDataContext())
      {
        var YaAsignada = from a in context.VD_Inspector where a.CUIT == cuit && a.Estado == 0 select new { a.Id };
        if (YaAsignada.Count() > 0)
        {
          return YaAsignada.Single().Id;
        }
        else
        {
          return 0;
        }
      }
    }

    public static int Get_InspectorId(int VDId)
    {
      using (var context = new lts_sindicatoDataContext())
      {
        int InspectorId = (from a in context.VD_Inspector where a.Id == VDId select new { a.InspectorId }).Single().InspectorId;
        return InspectorId;
      }
    }

    public static string Get_VDCuit(int VDId)
    {
      using (var context = new lts_sindicatoDataContext())
      {
        string VD_Cuit = (from a in context.VD_Inspector where a.Id == VDId select new { a.CUIT }).Single().CUIT;
        return VD_Cuit;
      }
    }
  }
}
