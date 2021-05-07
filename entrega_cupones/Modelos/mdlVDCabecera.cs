using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entrega_cupones.Modelos
{
  class mdlVDCabecera
  {
    public int Id { get; set; }
    public int VerificacionId { get; set; }
    public DateTime Fecha { get; set; }
    public int Numero { get; set; }
    public DateTime Desde { get; set; }
    public DateTime Hasta { get; set; }
    public DateTime Vencimiento { get; set; }
    public int EmpresaId { get; set; }
    public string Cuit { get; set; }
    public decimal Capital { get; set; }
    public decimal Interes { get; set; }
    public decimal Total { get; set; }
    public int PlanDePago { get; set; }
    public int EmpleadosCantidad { get; set; }
    public decimal InteresMensualAplicado { get; set; }
    public decimal InteresDiarioAplicado { get; set; }
    public int Estado { get; set; }
  }
}
