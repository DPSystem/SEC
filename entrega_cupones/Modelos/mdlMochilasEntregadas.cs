using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entrega_cupones.Modelos
{
  class mdlMochilasEntregadas
  {
    public DateTime? FechaDeEntrega { get; set; }
    public int NroDeCupon { get; set; }
    public string Mochila { get; set; }
    public char? Sexo { get; set; }
    public int IdMochila { get; set; }
    public int? IdMochilaRetirada { get; set; }
    public int EnStock { get; set; }
    public string CodigoPostal { get; set; }
    public string  ApenomSocio{ get; set; }
    public string  ApenomBenef { get; set; }
    public string Entregado { get; set; }
  }
}
