using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entrega_cupones.Clases
{
  public class ranking
  {
    lts_sindicatoDataContext db_sindicato = new lts_sindicatoDataContext();

    List<empresas_con_deuda> lista_emp_deuda = new List<empresas_con_deuda>();

    public class empresas_con_deuda
    {
      public string cuit { get; set; }
      public string empresa { get; set; }
      public string domicilio { get; set; }
      public string localidad { get; set; }
      public double deuda { get; set; }
      public DateTime ultimo_periodo { get; set; }
      public string telefono { get; set; }
      public string estudio { get; set; }
    }

    public List<empresas_con_deuda> get_empresas()
    {
      var empresa_deuda = from a in db_sindicato.maeemp
                            //join l in db_sindicato.localidad on a.MAEEMP_CODLOC equals l.MAELOC_CODLOC
                            //where a.MAEEMP_CODLOC == 2216
                          select new
                          {
                            cuit = a.MEEMP_CUIT_STR,
                            empresa = a.MAEEMP_RAZSOC,
                            domicilio = a.MAEEMP_CALLE.Trim() + " " + a.MAEEMP_NRO,
                            loc = "SGO",
                            telefono = a.MAEEMP_TEL,
                            estudio = a.MAEEMP_ESTUDIO_CONTACTO// l.MAELOC_NOMBRE,
                          };
      if (empresa_deuda.Count() > 0)
      {

        foreach (var empresa in empresa_deuda.ToList())
        {
          Func_Utiles func_utiles = new Func_Utiles();
          empresas_con_deuda emp_deu = new empresas_con_deuda();
          DateTime ultimo_periodo = obtener_periodo(empresa.cuit);


          if (ultimo_periodo.Date == Convert.ToDateTime("01/01/1900").Date)
          {
            DateTime cinco_atras = DateTime.Now.AddYears(-5);
            ultimo_periodo = Convert.ToDateTime("01/" + func_utiles.generar_ceros(cinco_atras.Month.ToString(), 2) + "/" + cinco_atras.Year.ToString());
          }
          //if (ultimo_periodo.Date != Convert.ToDateTime("01/01/1900").Date)
          //{
          emp_deu.cuit = empresa.cuit;
          emp_deu.empresa = empresa.empresa.Trim();
          emp_deu.domicilio = empresa.domicilio.Trim();
          emp_deu.localidad = empresa.loc.Trim();
          emp_deu.telefono = empresa.telefono;
          emp_deu.estudio = empresa.estudio;
          emp_deu.deuda = obtener_deudas(ultimo_periodo, empresa.cuit);
          lista_emp_deuda.Add(emp_deu);
          emp_deu.ultimo_periodo = ultimo_periodo;
          //} 
        }

      }
      return lista_emp_deuda;
    }

    public DateTime obtener_periodo(string cuit)
    {
      using (var context = new lts_sindicatoDataContext())
      {
        DateTime u_p = Convert.ToDateTime("01/01/1900");
        var ult_per = from a in context.ACTAS
                      where a.CUIT == Convert.ToDouble(cuit) && a.HASTA != null
                      select new { ultimo_periodo = a.HASTA };

        if (ult_per.Count() > 0 || ult_per == null)
        {
          u_p = ult_per.Max(x => x.ultimo_periodo).Value;
          u_p = u_p.AddMonths(1);
          //obtener_deudas(ult_per.First().ultimo_periodo);
        }
        return u_p;
      }
    }

    public Double obtener_deudas(DateTime ultimo_per, string cuit)
    {
      DateTime venc_acta = Convert.ToDateTime("31/01/2019"); // DateTime.Today.AddMonths(1);
      calcular_coeficientes coef = new calcular_coeficientes();
      double deuda = 0;
      var deudas = from V in (from a in db_sindicato.ddjjt
                              where a.cuit == Convert.ToDouble(cuit) && a.periodo >= ultimo_per && a.impban1 == 0// && a.impban1 > 0  // && a.impban1 > 0
                              select new
                              {
                                capital = a.titem1 + a.titem2,
                                fecha_pago = a.fpago,
                                periodo = a.periodo,
                                pagado = a.impban1,
                              })
                   select new
                   {
                     deuda = (V.fecha_pago != null) ?
                       coef.calcular_coeficiente_A(Convert.ToDateTime(V.periodo), Convert.ToDateTime(V.fecha_pago), Convert.ToDouble(V.capital), Convert.ToDouble(V.pagado), venc_acta)
                       :
                       coef.calcular_coeficiente_B(Convert.ToDateTime(V.periodo), Convert.ToDouble(V.capital), Convert.ToDouble(V.pagado), venc_acta)
                   };
      deuda = deudas.ToList().Sum(x => x.deuda);

      return deuda;
    }
  }
}
