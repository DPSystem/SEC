using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entrega_cupones.Modelos
{
  public class mdlCuadroAmortizacion
  {
    public string Cuota { get; set; }
    public double ImporteDeCuota { get; set; }
    public double Interes { get; set; }
    public double Amortizado { get; set; }
    public double AAmortizar { get; set; }
    public int ReciboDeCobro { get; set; }
    public DateTime FechaDeVenc { get; set; }
    public int Estado { get; set; }
    public decimal DeudaInicial { get; set; }
  }
}
