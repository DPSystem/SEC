using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entrega_cupones.Clases
{
  class EventosArticulos
  {

    public ClsEventoArticulo EventoArticulo = new ClsEventoArticulo();

    public class ClsEventoArticulo
    {
      public int EventoArt_EventID { get; set; }
      public int EventoArt_ArtID { get; set; }
      public int EventoArt_Estado { get; set; }
    }


    //public ClsEventoArticulo Insertar(int eventoID)
    //{
    //  try
    //  {
    //    using (lts_sindicatoDataContext context = new lts_sindicatoDataContext())
    //    {
    //      var arts = from a in context.eventos_art where a.event_art_event_id == eventoID select a;
    //      if (arts.Count() > 0 )
    //      {
    //        foreach (var item in arts.ToList())
    //        {
              

    //        }
    //      }

    //    }
    //  }
    //  catch (Exception)
    //  {

    //    throw;
    //  }

    //}
  }
}
