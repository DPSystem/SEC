using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entrega_cupones.Modelos
{
  class mdlCategoriaEmpleado
  {
    public int Id { get; set; }
    public int CodigoCategoria { get; set; }
    public string Descripcion { get; set; }

    [Column(TypeName = "decimal(8, 2)")]
    public decimal Importe { get; set; }

  }
}
