using Org.BouncyCastle.Asn1.IsisMtt.X509;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entrega_cupones.Modelos
{
  public class mdlDDJJEmpleado
  {
    public string Cuit { get; set; }
    public DateTime Periodo { get; set; }
    public int Rectificacion { get; set; }
    public string Dni { get; set; }
    public string Apellido { get; set; }
    public string Nombre { get; set; }
    public decimal Sueldo { get; set; }
    public string Jornada { get; set; }
    public decimal AporteLey { get; set; }
    public decimal AporteSocio { get; set; }
    public decimal Escala { get; set; }
    public string Categoria { get; set; }
    public DateTime FechaIngreso { get; set; }
    public int Antiguedad { get; set; }
    public decimal Diferencia { get; set; }
    public decimal AntiguedadImporte { get; set; }
    public decimal Presentismo { get; set; }
    public decimal Jubilacion { get; set; }
    public decimal ObraSocial { get; set; }
    public decimal Ley19302 { get; set; }
    public decimal AporteLeyDif { get; set; }
    public decimal AporteSocioDif { get; set; }
    public decimal AporteSocioEscala { get; set; }
    public decimal SueldoDif { get; set; }
    public decimal FAECys { get; set; }
    public decimal OSECAC { get; set; }
    public decimal TotalHaberes { get; set; }
    public decimal TotalHaberes2 { get; set; }
    public decimal TotalDescuentos { get; set; }
    public decimal BasicoJubPres { get; set; }
    public decimal AcuerdoNR1 { get; set; }
    public decimal AcuerdoNR2 { get; set; }


  }
}
