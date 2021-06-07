using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entrega_cupones.Modelos
{
  public class mdlVDInspector
  {
    public int Id { get; set; }
    public int InspectorId { get; set; }
    public int EmpresaId { get; set; }
    public string CUIT { get; set; }
    public DateTime FechaAsignacion { get; set; }
    public int Estado { get; set; }
    public DateTime? FechaCierre { get; set; }
    public DateTime Desde { get; set; }
    public DateTime Hasta { get; set; }
    public DateTime FechaVenc { get; set; }
    public int TipoInteres { get; set; }
    public decimal InteresMensual { get; set; }
    public decimal InteresDiario { get; set; }
    public decimal Capital { get; set; }
    public decimal Interes { get; set; }
    public decimal Total { get; set; }
    public int UserId { get; set; }
    public string Empresa { get; set; }
    public string  Domicilio { get; set; }

  }
}
