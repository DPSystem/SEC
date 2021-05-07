using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entrega_cupones.Modelos
{
  class mdlDeudaParaRanking
  {
    public string Cuit { get; set; }
    public string Empresa { get; set; }
    public decimal Deuda { get; set; }

    public decimal ley { get; set; }
    public decimal  socio { get; set; }
    public decimal depositado { get; set; }

  }
}
