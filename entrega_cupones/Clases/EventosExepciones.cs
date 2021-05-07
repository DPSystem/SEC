using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entrega_cupones.Clases
{
  class EventosExepciones
  {

    public List<cls_EventosExep> lst_EventosExepciones = new List<cls_EventosExep>();

    public cls_EventosExep varEventoExepecion = new cls_EventosExep();

    public class cls_EventosExep
    {
      public int EventExepId { get; set; }
      public string EventExepApellido { get; set; }
      public string EventExepNombre { get; set; }
      public string EventExepDni { get; set; }
      public DateTime EventFechaNac { get; set; }
      public string EventExpSexo { get; set; }
      public int EventExepParent { get; set; }
      public double EventSocioCuil { get; set; }
    }

    public cls_EventosExep GetExisteExepcion(string dni)
    {
      using (lts_sindicatoDataContext context = new lts_sindicatoDataContext())
      {
        var exepcion = from a in context.eventos_exep where a.event_exep_dni == dni select a;
        if (exepcion.Count() > 0)
        {
          varEventoExepecion.EventExepId = exepcion.FirstOrDefault().event_exep_id;
          varEventoExepecion.EventExepApellido = exepcion.FirstOrDefault().event_exep_apellido;
          varEventoExepecion.EventExepNombre = exepcion.FirstOrDefault().event_exep_nombre;
          varEventoExepecion.EventExepDni = exepcion.FirstOrDefault().event_exep_dni;
          varEventoExepecion.EventExepParent = exepcion.FirstOrDefault().event_exep_parent;
          varEventoExepecion.EventSocioCuil = exepcion.FirstOrDefault().event_exep_socio_cuil;
        }
        return varEventoExepecion;
      }
    }

    public cls_EventosExep InsertarExepciones(string apellido, string nombre, string dni, DateTime fechanac, string sexo, int parentescoId, double socioCuil)
    {
      using (lts_sindicatoDataContext context = new lts_sindicatoDataContext())
      {
        try
        {
          eventos_exep insert = new eventos_exep();
          insert.event_exep_apellido = apellido.ToUpper();
          insert.event_exep_nombre = nombre.ToUpper();
          insert.event_exep_dni = dni;
          insert.event_exep_fechanac = fechanac;
          insert.event_exep_sexo = sexo;
          insert.event_exep_parent = parentescoId;
          insert.event_exep_socio_cuil = socioCuil;
          context.eventos_exep.InsertOnSubmit(insert);
          context.SubmitChanges();

          varEventoExepecion.EventExepId = context.eventos_exep.Max(x => x.event_exep_id);
          varEventoExepecion.EventExepApellido = apellido;
          varEventoExepecion.EventExepNombre = nombre;
          varEventoExepecion.EventExepDni = dni;
          varEventoExepecion.EventFechaNac = fechanac;
          varEventoExepecion.EventExpSexo = sexo;
          varEventoExepecion.EventExepParent = parentescoId;
          varEventoExepecion.EventSocioCuil = socioCuil;

          return varEventoExepecion;
        }
        catch (Exception)
        {
          throw;
        }
      }
    }

    public List<cls_EventosExep> GetListadoExepciones(double cuil)
    {
      using (lts_sindicatoDataContext context = new lts_sindicatoDataContext())
      {
        var exepcion = from a in context.eventos_exep where a.event_exep_socio_cuil == cuil select a;
        if (exepcion.Count() > 0)
        {
          foreach (var item in exepcion)
          {
            cls_EventosExep insert = new cls_EventosExep();
            insert.EventExepId = item.event_exep_id;
            insert.EventExepApellido = item.event_exep_apellido;
            insert.EventExepNombre = item.event_exep_nombre;
            insert.EventExepDni = item.event_exep_dni;
            insert.EventExpSexo = item.event_exep_sexo;
            insert.EventFechaNac = Convert.ToDateTime(item.event_exep_fechanac);
            insert.EventExepParent = item.event_exep_parent;
            insert.EventSocioCuil = item.event_exep_socio_cuil;
            lst_EventosExepciones.Add(insert);
          }
        }
        return lst_EventosExepciones;
      }
    }

    public int GetCantidadExepciones()
    {
      using (lts_sindicatoDataContext context = new lts_sindicatoDataContext())
      {
        var cupones_exepciones = from a in context.eventos_cupones  where a.event_cupon_nro == 0 select a;
        return cupones_exepciones.Count();
      }
    }

    public double GetCuilExepcionPorID(int _exepcionID)
    {
      using (lts_sindicatoDataContext context = new lts_sindicatoDataContext())
      {
        var cuil = from a in context.eventos_exep where a.event_exep_id == _exepcionID select a;
        if (cuil.Count() > 0 )
        {
          return cuil.FirstOrDefault().event_exep_socio_cuil;
        }
        else
        {
          return 0;
        }
      }
    }
  }
}
