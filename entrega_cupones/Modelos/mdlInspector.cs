using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entrega_cupones.Modelos
{
  class mdlInspector
  {
    public int Id { get; set; }
    public string  Apellido { get; set; }
    public string Nombre { get; set; }
    public string Telefono { get; set; }
    public int? Estudio { get; set; }
  }
}
