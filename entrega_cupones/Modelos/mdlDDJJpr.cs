using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entrega_cupones.Modelos
{
  class mdlDDJJpr
  {
    public string cuit { get; set; }
    public decimal importe { get; set; }
    public decimal depositado { get; set; }
    public DateTime periodo  { get; set; }
    public int rectificacion { get; set; }
    public string acta { get; set; }
  }

}
