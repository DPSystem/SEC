using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entrega_cupones.Modelos
{
  public class mdlVDListado 
  {
    public int Id { get; set; }
    public DateTime Fecha { get; set; }
    public string CUIT { get; set; }
    public string Empresa { get; set; }
    public decimal Importe { get; set; }
    public int Estado { get; set; }

  }
}
