using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entrega_cupones.Modelos
{
  public class mdlEstadoPlanDePago
  {
    public int CuotaId { get; set; }
    public int Cuota { get; set; }
    public decimal ImporteDeCuota { get; set; }
    public DateTime FechaDeVenc { get; set; }
    public int Dias { get; set; }
    public decimal Interes { get; set; }
    public decimal ImporteCobrado { get; set; }
    public decimal  Total { get; set; }
    public decimal Amortizado { get; set; } 
    public decimal AAmortizar { get; set; }
    public int ReciboDeCobro { get; set; }
    public int Estado { get; set; }
    public decimal DeudaInicial { get; set; }
    public string Cancelado { get; set; }
  }
}
