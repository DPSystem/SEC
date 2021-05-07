using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entrega_cupones.Modelos
{
  class mdlRecibosReporte
  {
    public string Cuota { get; set; }
    public decimal Importe { get; set; }
    public DateTime FechaAutomatica { get; set; }
    public string NroReciboAutomatico { get; set; }
    public string NroReciboManual { get; set; }
    public string FechaManual { get; set; }
    public decimal Efectivo { get; set; }
    public List<mdlCargaDeCheques> Cheques { get; set; }
    public List<mdlTransf> Transf { get; set; }
  }
}
