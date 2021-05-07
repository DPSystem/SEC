using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entrega_cupones.Modelos
{
  class mdlAportes
  {
    public DateTime Periodo { get; set; }
    public decimal  AporteLey { get; set; }
    public decimal  AporteSocio { get; set; }
    public string RazonSocial { get; set; }
    public decimal  Sueldo { get; set; }
  }
}
