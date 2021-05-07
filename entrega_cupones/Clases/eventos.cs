using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entrega_cupones.Clases
{
  class eventos
  {
    public List<cls_eventos> lst_eventos = new List<cls_eventos>();
    

    public class cls_eventos
    {
      public int eventos_id { get; set; }
      public string eventos_nombre { get; set; }
      public int eventos_estado { get; set; }
      public DateTime eventos_inicio { get; set; }
      public DateTime eventos_fin { get; set; }
      public DateTime  eventos_horafin { get; set; }
    }

    public List<cls_eventos> get_todos()
    {
      using (lts_sindicatoDataContext context = new lts_sindicatoDataContext())
      {
        var eventos_ = from a in context.eventos where a.eventos_estado == 1 orderby a.eventos_nombre select a;
        if (eventos_.Count() > 0 )
        {
          foreach (var item in eventos_.ToList())
          {
            cls_eventos insert = new cls_eventos();
            insert.eventos_id = item.eventos_id;
            insert.eventos_nombre = item.eventos_nombre;
            insert.eventos_estado = item.eventos_estado;
            insert.eventos_inicio = Convert.ToDateTime( item.eventos_inicio);
            insert.eventos_fin = Convert.ToDateTime(item.eventos_fin);
            //insert.eventos_horafin = item.eventos_horafin;
            lst_eventos.Add(insert);
          }
        }
        return lst_eventos;
      }
    }

    //public cls_EventosExep GetEventoExep()
    //{

    //}

  }
}
