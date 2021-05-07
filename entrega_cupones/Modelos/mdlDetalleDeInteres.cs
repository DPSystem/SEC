using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entrega_cupones.Modelos
{
  class mdlDetalleDeInteres
  {
    public DateTime Periodo { get; set; }
    public DateTime Desde { get; set; }
    public DateTime Hasta { get; set; }
    public decimal Importe { get; set; }
    public decimal Taza { get; set; }
    public decimal Interes { get; set; }

  }
}
