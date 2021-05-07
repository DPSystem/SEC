using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entrega_cupones.Clases
{
  public class Caja
  {

    public List<ClsDetalleCaja> DetalleCaja = new List<ClsDetalleCaja>();

    public class ClsDetalleCaja
    {
      public int NmeroDeComprobante { get; set; }
      public decimal ValorComprobante { get; set; }
      public DateTime Hora { get; set; }

    }


    public int ControlarAperturaDeCaja(int UsuarioId)
    {
      using (var context = new lts_sindicatoDataContext())
      {
        var Caja = context.Cajas.Where(x => x.UserId == UsuarioId && x.FechaApertura.Value.Date == DateTime.Now.Date
                                      && x.FechaCierre.Value == null);
        if (Caja.Count() > 0)
        {
          return Caja.FirstOrDefault().Id;
        }
        else
        {
          return 0;
        }
      }
    }

    public void AbrirCaja(int UsuarioId)
    {
      using (var context = new lts_sindicatoDataContext())
      {
        Cajas Insert = new Cajas();
        Insert.UserId = UsuarioId;
        Insert.FechaApertura = DateTime.Now;
        context.Cajas.InsertOnSubmit(Insert);
        context.SubmitChanges();
      }
    }

    public void CerrarCaja(int CajaId, int UsuarioId, int EventoId)
    {
      using (var context = new lts_sindicatoDataContext())
      {
        var caja = context.Cajas.Where(x => x.Id == CajaId).First();
        caja.FechaCierre = DateTime.Now;
        context.SubmitChanges();

        var CierreDeCaja = context.eventos_cupones.Where
          (x => x.UsuarioId == UsuarioId && x.CajaId == 0 && x.eventcupon_evento_id == EventoId);
        if (CierreDeCaja.Count() > 0)
        {
          foreach (var item in CierreDeCaja.ToList())
          {
            item.CajaId = CajaId;
          }
          context.SubmitChanges();
        }
      }
    }

    public List<ClsDetalleCaja> ConsultaDetalleDeCaja(int UsuarioId, int EventoId)
    {
      using (var context = new lts_sindicatoDataContext())
      {
        //var caja = context.eventos_cupones.Where(x => x.UsuarioId == UsuarioId && 
        //                                         x.eventcupon_evento_id == EventoId && x.
        //                                         CajaId == 0 && x.Costo > 0 &&
        //                                         x.Invitado == 1);
        var caja = context.eventos_cupones.Where(x => x.UsuarioId == UsuarioId && x.eventcupon_evento_id == EventoId && x.CajaId == 0);

        var caja2 = context.eventos_cupones.Where(x => x.UsuarioId == UsuarioId &&
                                                 x.eventcupon_evento_id == EventoId && x.
                                                 CajaId == 0 && x.Costo > 0).GroupBy(x => x.ComprobanteId).Select(y =>
                                                   new
                                                   {
                                                     id = y.Key,
                                                     total = y.Sum(x => x.Costo)
                                                   });
        if (caja2.Count() > 0)
        {
          foreach (var item in caja2)
          {
            ClsDetalleCaja Insert = new ClsDetalleCaja();
            Insert.NmeroDeComprobante = Convert.ToInt32(item.id);
            Insert.ValorComprobante = Convert.ToDecimal(item.total);
            Insert.Hora = context.eventos_cupones.Where(x => x.ComprobanteId == item.id).Select(x => x.event_cupon_fecha).FirstOrDefault();
            DetalleCaja.Add(Insert);
          }
        }



        //if (caja.Count() > 0)
        //{
        //  foreach (var item in caja)
        //  {
        //    ClsDetalleCaja Insert = new ClsDetalleCaja();
        //    Insert.NmeroDeComprobante = Convert.ToInt32(item.ComprobanteId);
        //    Insert.ValorComprobante = Convert.ToDecimal(context.Comprobantes.Where(x => x.ComprobanteId == item.ComprobanteId).First().Importe);
        //    Insert.Hora = item.event_cupon_fecha;
        //    DetalleCaja.Add(Insert);
        //  }
        //}
        return DetalleCaja;
      }
    }
  }
}
