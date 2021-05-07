using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entrega_cupones.Modelos
{
  class mdlCargaDeCheques
  {
    public DateTime? FechaEmision { get; set; }
    public string Numero { get; set; }
    public decimal Importe { get; set; }
    public DateTime? FechaVenc { get; set; }
    public int BancoId { get; set; }
    public string NombreDeBanco { get; set; }

  }
}
