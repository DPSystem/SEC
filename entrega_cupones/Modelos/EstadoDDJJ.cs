using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entrega_cupones.Modelos
{
  public class EstadoDDJJ
  {
    public DateTime Periodo { get; set; }
    public int Rectificacion { get; set; }
    public decimal AporteLey { get; set; }
    public decimal AporteSocio { get; set; }
    public DateTime? FechaDePago { get; set; }
    public decimal ImporteDepositado { get; set; }
    public int Empleados { get; set; }
    public int Socios { get; set; }
    public decimal TotalSueldoEmpleados { get; set; }
    public decimal TotalSueldoSocios { get; set; }
    public decimal Capital { get; set; }
    public int DiasDeMora { get; set; }
    public decimal InteresCobrado { get; set; }
    public decimal Interes { get; set; }
    public decimal Total { get; set; }
    public int PerNoDec { get; set; }
    public string Acta { get; set; }
    public decimal AporteSocioDifJorPar { get; set; }
    public string VerifDeuda { get; set; }
  }
}
