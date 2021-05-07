using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entrega_cupones.Modelos
{
  class mdlSocio
  {
    public string NroDeSocio { get; set; }
    public string NroDNI { get; set; }
    public string ApeNom { get; set; }
    public string CUIT { get; set; }
    public string  RazonSocial { get; set; }
    public bool EsSocio { get; set; }
    public string CUIL { get; set; }
    public string CodigoPostal { get; set; }
    public bool JornadaParcial { get; set; }
    public int Categoria { get; set; }
    public string FechaBaja { get; set; }
    public int Jubilado { get; set; }
    public string EstadoCivil { get; set; }
    public string  Sexo { get; set; }
    public string FechaNacimiento { get; set; }
    public string Edad { get; set; }
    public string Calle { get; set; }
    public string NroCalle { get; set; }
    public string Barrio { get; set; }
    public string Localidad { get; set; }
    public string Telefono { get; set; }
    public string EmpresaNombre { get; set; }
    public string  EmpresaTelefono { get; set; }
    public string EmpresaDomicilio { get; set; }
    public string EmpresaContador { get; set; }
    public string EmpresaContadorTelefono { get; set; }
    public string EmpresaContadorEmail { get; set; }
    public string EmpresaEmail { get; set; }
    public string   EmpresaCodigoPostal { get; set; }
    public string EmpresaLocalidad { get; set; }
    public List<mdlAportes> Aportes { get; set; }
    public bool Carencia { get; set; }

  }
}
