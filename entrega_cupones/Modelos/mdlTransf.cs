using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entrega_cupones.Modelos
{
  class mdlTransf
  {
    public DateTime Fecha { get; set; }
    public string NroDeTransf { get; set; }
    public string Banco { get; set; }
    public decimal  Importe { get; set; }
  }
}
