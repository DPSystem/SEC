using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entrega_cupones.Clases
{
  class Actas
  {
    public class ClsActa
    {
      public DateTime FECHA { get; set; }
      public int ACTA { get; set; }
      public string EMPRESA { get; set; }
      public double CUIT { get; set; }
      public string DOMICILIO { get; set; }
      public DateTime DESDE { get; set; }
      public DateTime HASTA { get; set; }
      public double DEUDAHISTORICA { get; set; }
      public double INTERESES { get; set; }
      public double DEUDAACTUALIZADA { get; set; }
      public double INTERESFINANC { get; set; }
      public double DEUDATOTAL { get; set; }
      public string COBRADOTOTALMENTE { get; set; }
      public string INSPECTOR { get; set; }
      public string OBSERVACIONES { get; set; }
      public double IMPORTECOBRADO { get; set; }
      public double DIFERENCIA { get; set; }
      public int ESTUDIO_JURIDICO { get; set; }
      public int COBRADOR { get; set; }
      public string CobradorNombre { get; set; }
      public int NROASIGNACION { get; set; }
      public decimal ImporteInteresActualizado { get; set; }
      public decimal IMPORTEDEUDAACTUALIZADA { get; set; }

    }

    public List<ClsActa> ActaslLst = new List<ClsActa>();

    
  }
}
