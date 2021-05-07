using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace entrega_cupones.Clases
{
  class socios
  {
    public socio soc = new socio();



    public ClsEmpresa empr = new ClsEmpresa();

    public class ClsEmpresa
    {
      public string nombre { get; set; }
      public string cuit { get; set; }
    }

    public List<socio_2> LstSocio = new List<socio_2>();

    public List<ClsTitularBenef> Lst_Titular_benef = new List<ClsTitularBenef>();

    public Binary foto { get; set; }

    public class socio
    {
      public string nombre { get; set; }
      public string apellido { get; set; }
      public string comentario { get; set; }
      public string nrosocio { get; set; }
      public string empresa { get; set; }
      public string dni { get; set; }
      public string cuit { get; set; }
      public double cuil { get; set; }
      public string  CuilStr { get; set; }
    }

    public class socio_2
    {
      public string numero_socio { get; set; }
      public string dni_socio { get; set; }
      public string apeynom { get; set; }
      public string empresa { get; set; }
      public string empresa_cuit { get; set; }
    }

    public class clsBeneficiario
    {
      public string Nombre { get; set; }
      public string Parentesco { get; set; }
      public string codigo_fliar { get; set; }
    }

    public List<clsBeneficiario> lstBenef = new List<clsBeneficiario>();

    public class ClsTitularBenef
    {
      public string nombre { get; set; }
      public string Parentesco { get; set; }
      public string Cuil { get; set; }
      public char Sexo { get; set; }
      public int Edad { get; set; }
      public long CodigoFliar { get; set; }
      //public bool Emitir { get; set; }
      //public int Impreso { get; set; }
    }

    public int calcular_edad(DateTime fecha_nac)
    {
      int edad = 0;
      DateTime fecha_actual = DateTime.Today;
      edad = fecha_actual.Year - fecha_nac.Year;
      if ((fecha_actual.Month < fecha_nac.Month) || (fecha_actual.Month == fecha_nac.Month && fecha_actual.Day < fecha_nac.Day))
      {
        edad--;
      }
      return edad;
    }

    public socio get_datos_socio(double cuil, int NroDni)
    {
      using (lts_sindicatoDataContext context = new lts_sindicatoDataContext())
      {
        var datosSocio = cuil != 0 ? from a in context.maesoc where a.MAESOC_CUIL == cuil select a :
          from a in context.maesoc where a.MAESOC_NRODOC == NroDni.ToString() select a;

        if (datosSocio.Count() > 0)
        {
          soc.apellido = datosSocio.First().MAESOC_APELLIDO.Trim();
          soc.nombre = datosSocio.First().MAESOC_NOMBRE.Trim();
          soc.comentario = get_comentario(cuil.ToString());
          soc.nrosocio = datosSocio.First().MAESOC_NROAFIL;
          soc.empresa = GetEmpresa(datosSocio.First().MAESOC_CUIL);
          soc.dni = datosSocio.First().MAESOC_NRODOC;
          soc.cuil = datosSocio.First().MAESOC_CUIL;
          soc.CuilStr = datosSocio.First().MAESOC_CUIL_STR;
        }
      }
      return soc;
    }

    public string get_comentario(string cuil)
    {
      string comit = string.Empty;
      using (lts_sindicatoDataContext context = new lts_sindicatoDataContext())
      {

        var comentario = (from a in context.ParaInspeccion
                          where a.CUIL == cuil && a.ESTADO == 0
                          select a).OrderByDescending(x => x.ID);
        if (comentario.Count() > 0)
        {
          comit = context.comentarios.Where(x => x.PI_ID == comentario.First().ID).Select(x => x.COMENTARIO).First().ToString();
        }
      }
      return comit;
    }

    public Binary get_foto_benef_binary(double cod_fliar)
    {
      convertir_imagen cnv_img = new convertir_imagen();
      using (var context = new lts_sindicatoDataContext())
      {
        var foto_ = from a in context.fotos where a.FOTOS_CODFLIAR == cod_fliar select a;
        if (foto_.Count() > 0)
        {
          foto = foto_.FirstOrDefault().FOTOS_FOTO;
        }
        else
        {
          // cuando no hay foto trae la imagen del contorno del usuario.
          foto = cnv_img.ImageToByteArray(Properties.Resources.contorno_usuario);
        }
      };
      return foto;
    }

    public Binary get_foto_titular_binary(double cuil)
    {
      convertir_imagen cnv_img = new convertir_imagen();
      using (var context = new lts_sindicatoDataContext())
      {
        var foto_ = from a in context.fotos where a.FOTOS_CUIL == cuil && a.FOTOS_CODFLIAR == 0 select a;
        if (foto_.Count() > 0)
        {
          foto = foto_.FirstOrDefault().FOTOS_FOTO;
        }
        else
        {
          // cuando no hay foto trae la imagen del contorno del usuario.
          foto = cnv_img.ImageToByteArray(Properties.Resources.contorno_usuario);
        }
      };
      return foto;
    }

    public List<ClsTitularBenef> Get_Titular_Benef(double cuil, int TipoDeEvento)
    {
      using (lts_sindicatoDataContext context = new lts_sindicatoDataContext())
      {
        Func_Utiles fu = new Func_Utiles();
        Parentesco parent = new Parentesco();


        var Titu = (from a in context.soccen
                    where a.SOCCEN_CUIL == cuil
                    join b in context.maesoc on a.SOCCEN_CUIL equals b.MAESOC_CUIL
                    select new
                    {
                      nombre = b.MAESOC_APELLIDO.Trim() + " " + b.MAESOC_NOMBRE.Trim(),
                      parent = "Titular",
                      dni = b.MAESOC_NRODOC,
                      sexo = b.MAESOC_SEXO,
                      edad = fu.calcular_edad(b.MAESOC_FECHANAC),
                      codigofliar = 0
                    }).ToList();

        var Benef = (from a in context.socflia
                     where a.SOCFLIA_CUIL == cuil
                     join b in context.maeflia on a.SOCFLIA_CODFLIAR equals b.MAEFLIA_CODFLIAR
                     select new
                     {
                       nombre = b.MAEFLIA_APELLIDO.Trim() + " " + b.MAEFLIA_NOMBRE.Trim(),
                       parent = parent.GetParentescoDescrip(a.SOCFLIA_PARENT).parent_descrip,
                       dni = Convert.ToString(b.MAEFLIA_NRODOC),
                       sexo = b.MAEFLIA_SEXO,
                       edad = fu.calcular_edad(b.MAEFLIA_FECNAC),
                       codigofliar = Convert.ToInt32(b.MAEFLIA_CODFLIAR)
                     }).ToList();


        //var Titu_Benef = Titu.Union(Benef);
        var Titu_Benef = TipoDeEvento != 3 ? Titu.Union(Benef) : Titu;
        if (Titu_Benef.Count() > 0)
        {
          foreach (var item in Titu_Benef)
          {
            ClsTitularBenef tb = new ClsTitularBenef();
            tb.nombre = item.nombre;
            tb.Parentesco = item.parent;
            tb.Cuil = item.dni;
            tb.Sexo = item.sexo;
            tb.Edad = item.edad;
            tb.CodigoFliar = item.codigofliar;
            Lst_Titular_benef.Add(tb);
          }
        }
        return Lst_Titular_benef;
      }
    }

    public string GetEmpresa(double cuil)
    {
      using (lts_sindicatoDataContext context = new lts_sindicatoDataContext())
      {
        var soc_empresa = from a in context.socemp where a.SOCEMP_CUIL == cuil && a.SOCEMP_ULT_EMPRESA == 'S' select a;
        if (soc_empresa.Count() > 0)
        {

          double cuite = soc_empresa.First().SOCEMP_CUITE;
          var empresa = from a in context.maeemp where a.MAEEMP_CUIT == cuite select a;
          if (empresa.Count() > 0)
          {
            return empresa.First().MAEEMP_RAZSOC;
          }
          else
          {
            return string.Empty;
          }
        }
        else
        {
          return string.Empty;
        }
      }
    }

    public ClsEmpresa GetEmpresaNombreCuil(double cuil)
    {
      using (lts_sindicatoDataContext context = new lts_sindicatoDataContext())
      {
        var soc_empresa = from a in context.socemp where a.SOCEMP_CUIL == cuil && a.SOCEMP_ULT_EMPRESA == 'S' select a;
        if (soc_empresa.Count() > 0)
        {

          double cuite = soc_empresa.First().SOCEMP_CUITE;
          var empresa = from a in context.maeemp where a.MAEEMP_CUIT == cuite select a;
          if (empresa.Count() > 0)
          {
            empr.nombre = empresa.First().MAEEMP_RAZSOC;
            empr.cuit = empresa.First().MEEMP_CUIT_STR;
            return empr;
          }
          else
          {
            empr.nombre = "SIN EMPRESA ASIGNADA";
            empr.cuit = "0";
            return empr;
          }
        }
        else
        {
          empr.nombre = "SIN EMPRESA ASIGNADA";
          empr.cuit = "0";
          return empr;
        }
      }
    }

    public List<socio_2> GetListadoSociosPorDniAyn(string estado, string dato)
    {

      // 31739266 - 25168935
      using (lts_sindicatoDataContext context = new lts_sindicatoDataContext())
      {
        var socios_ = (from b in (from a in context.soccen
                                  where (estado == "2") ? a.SOCCEN_ESTADO != '2' : a.SOCCEN_ESTADO == Convert.ToByte(estado)
                                  join sc in context.maesoc on a.SOCCEN_CUIL equals sc.MAESOC_CUIL
                                  where ((sc.MAESOC_APELLIDO.Contains(dato)) || (sc.MAESOC_NOMBRE.Contains(dato)) || sc.MAESOC_NRODOC.Contains(dato))
                                  select new
                                  {
                                    numero_socio = sc.MAESOC_NROAFIL.Trim(),
                                    dni_socio = sc.MAESOC_NRODOC.Trim(),
                                    apeynom = sc.MAESOC_APELLIDO.Trim() + " " + sc.MAESOC_NOMBRE.Trim(),
                                    empresa = GetEmpresaNombreCuil(sc.MAESOC_CUIL).nombre, //(GetEmpresa(sc.MAESOC_CUIL) == "" ? "SIN EMPRESA ASIGNADA": "nombre de la empresa")//GetEmpresa(sc.MAESOC_CUIL)
                                    empresa_cuit = GetEmpresaNombreCuil(sc.MAESOC_CUIL).cuit
                                  })
                       where b.apeynom.Contains(dato) || b.dni_socio.Contains(dato)
                       select b).OrderBy(x => x.apeynom).ToList();

        if (socios_.Count() > 0)
        {
          foreach (var item in socios_.ToList())
          {
            socio_2 insert = new socio_2();
            insert.numero_socio = item.numero_socio;
            insert.apeynom = item.apeynom;
            insert.dni_socio = item.dni_socio;
            insert.empresa = item.empresa;
            insert.empresa_cuit = item.empresa_cuit;
            LstSocio.Add(insert);
          }
        }
        return LstSocio;
      }
    }

    public void ActivarSocio(double cuil)
    {

      lts_sindicatoDataContext context = new lts_sindicatoDataContext();

      var activar_socio = context.soccen.Where(x => x.SOCCEN_CUIL == Convert.ToDouble(cuil));

      activar_socio.Single().SOCCEN_ESTADO = 1;
      context.SubmitChanges();

    }

    public double GetCuilPorCodFliar(int _codfliar)
    {
      using (lts_sindicatoDataContext context = new lts_sindicatoDataContext())
      {
        double cuil = 0;
        var cuiltitular = from a in context.socflia
                          where a.SOCFLIA_CODFLIAR == _codfliar
                          select new { cuil = a.SOCFLIA_CUIL };
        if (cuiltitular.Count() > 0)
        {
          cuil = cuiltitular.First().cuil;
        }
        return cuil;
      }
    }

    public List<clsBeneficiario> LimpiarDgvBenef()
    {
      //clsBeneficiario bnf = new clsBeneficiario();
      //bnf.Nombre = "";
      //bnf.codigo_fliar = "1";
      //bnf.Parentesco = "0";
      //lstBenef.Add(bnf);
      return lstBenef;
    }
  }
}
