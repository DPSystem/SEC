using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entrega_cupones.Modelos
{
  public class DDJJT
  {
    public int Id { get; set; }
    public DateTime periodo { get; set; }
    public double cuit { get; set; }
    public int rect { get; set; }
    public DateTime fpres { get; set; }
    public DateTime fpago { get; set; }
    public DateTime fproc { get; set; }
    public DateTime fmod { get; set; }
    public string usuario { get; set; }
    public decimal titem2 { get; set; }
    public decimal titem1 { get; set; }
    public decimal titem3 { get; set; }
    public int ctrlleg { get; set; }
    public decimal ctrlimp { get; set; }
    public decimal impex1 { get; set; }
    public int estado { get; set; }
    public DateTime fpago2 { get; set; }
    public decimal impban1 { get; set; }
    public decimal impban2 { get; set; }
    public int trans1 { get; set; }
    public int trans2 { get; set; }
    public decimal titem4 { get; set; }
    public decimal impex2 { get; set; }
    public int repite { get; set; }
    public decimal impex3 { get; set; }
    public decimal impban3 { get; set; }
    public DateTime fpago3 { get; set; }
    public int trans3 { get; set; }
    public string nver { get; set; }
    public int acta { get; set; }
    public int medio1 { get; set; }
    public int medio2 { get; set; }
    public int medio3 { get; set; }
    public string CuitStr { get; set; }
  }
}
