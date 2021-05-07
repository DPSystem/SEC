using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entrega_cupones.Modelos
{
  class mdlActa
  {
    public int NroActa { get; set; }
    public string Cuit { get; set; }
    public string RazonSocial { get; set; }
    public string Domicilio { get; set; }
    public DateTime Desde { get; set; }
    public DateTime Hasta { get; set; }
    public decimal Importe { get; set; }
    public int NroDePlan { get; set; }
  }
}
