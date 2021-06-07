using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entrega_cupones.Modelos
{
  public class mdlVDDetalle
  {
    public int Id { get; set; }
    public int VDInspectorId { get; set; }
    public DateTime? Periodo { get; set; }
    public int Rectificacion { get; set; }
    public int CantidadEmpleados { get; set; }
    public int CantidadSocios { get; set; }
    public decimal TotalSueldoEmpleados { get; set; }
    public decimal TotalSueldoSocios { get; set; }
    public decimal TotalAporteEmpleados { get; set; }
    public decimal TotalAporteSocios { get; set; }
    public DateTime? FechaDePago { get; set; }
    public decimal ImporteDepositado { get; set; }
    public int DiasDeMora { get; set; }
    public decimal  DeudaGenerada { get; set; }
    public decimal InteresGenerado { get; set; }
    public decimal Total { get; set; }
    public int PerNoDec { get; set; }
    public int ActaId { get; set; }
    public int NumeroDeActa { get; set; }
    public int  Estado { get; set; }

  }
}
