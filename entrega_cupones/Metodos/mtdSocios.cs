using entrega_cupones.Clases;
using entrega_cupones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entrega_cupones.Metodos
{
  class mtdSocios
  {
    public static List<DDJJ> _ddjj = new List<DDJJ>();

    public static List<mdlSocio> _Socios = new List<mdlSocio>();

    public static List<Empresa> _Empresas = new List<Empresa>();

    public static List<mdlSocio> GetSocios(int _FiltroDeSocio, int _BuscarPor, string _CodigoPostal, int _NroDeSocio, int _Jordada, int _CodigoCategoria, int _ActivoEnEmpresa, int _Jubilados, string DatoABuscar, string cuit, bool Carencia)
    {

      using (var context = new lts_sindicatoDataContext())
      {
        Func_Utiles fnc = new Func_Utiles();
        //var ddjj_ = from a in context.ddjj select new { a.periodo, a.cuil, a.jorp };

        DateTime _FechaDeBaja = Convert.ToDateTime("01-01-1000");

        _Socios.Clear();
        _Socios = (from a in context.maesoc
                   join b in context.soccen on a.MAESOC_CUIL equals b.SOCCEN_CUIL
                   into g
                   from essocio in g.DefaultIfEmpty()
                   join sc in context.socemp on a.MAESOC_CUIL equals sc.SOCEMP_CUIL
                   join empr in context.maeemp on sc.SOCEMP_CUITE equals empr.MAEEMP_CUIT
                   where (sc.SOCEMP_ULT_EMPRESA == 'S') &&
                   (_FiltroDeSocio == 0 ? essocio.SOCCEN_ESTADO >= 0 : _FiltroDeSocio == 1 ? essocio.SOCCEN_ESTADO == 1 : essocio.SOCCEN_ESTADO == 0) &&
                   (_BuscarPor == 0 ? a.MAESOC_NRODOC == DatoABuscar.Trim() : _BuscarPor == 1 ? a.APENOM.Contains(DatoABuscar) : _BuscarPor == 2 ? empr.MEEMP_CUIT_STR == cuit : a.MAESOC_CUIL_STR != "0") &&
                   (_CodigoPostal == "0" ? a.MAESOC_CODPOS != _CodigoPostal : a.MAESOC_CODPOS == _CodigoPostal) &&
                   (_NroDeSocio == 0 ? a.MAESOC_NROAFIL != "0" : _NroDeSocio == 1 ? a.MAESOC_NROAFIL != "" : a.MAESOC_NROAFIL == "") &&
                   (_CodigoCategoria == 0 ? a.MAESOC_CODCAT != _CodigoCategoria : a.MAESOC_CODCAT == _CodigoCategoria) &&
                   (_Jubilados == 0 ? a.MAESOC_JUBIL != 4 : _Jubilados == 1 ? a.MAESOC_JUBIL == 1 : a.MAESOC_JUBIL == 0)
                   select new mdlSocio
                   {
                     NroDeSocio = a.MAESOC_NROAFIL,
                     NroDNI = a.MAESOC_NRODOC.Trim(),
                     ApeNom = a.MAESOC_APELLIDO.Trim() + " " + a.MAESOC_NOMBRE.Trim(),
                     CUIT = empr.MEEMP_CUIT_STR,
                     RazonSocial = empr.MAEEMP_RAZSOC.Trim(),
                     EsSocio = essocio.SOCCEN_ESTADO == 1 ? true : false,
                     CUIL = a.MAESOC_CUIL_STR,
                     CodigoPostal = a.MAESOC_CODPOS,
                     //JornadaParcial = false,//(from c in _ddjj where c.cuil.Contains(a.MAESOC_CUIL_STR) select new { c.periodo, jorp = Convert.ToBoolean(c.jorp) } ).OrderByDescending(x=>x.periodo).FirstOrDefault().jorp , //_ddjj.Where(x => x.cuil == a.MAESOC_CUIL_STR).OrderByDescending(x => x.periodo).FirstOrDefault().jorp,
                     Categoria = a.MAESOC_CODCAT,
                     FechaBaja = sc.SOCEMP_FECHABAJA == _FechaDeBaja ? "" : sc.SOCEMP_FECHABAJA.ToString(),
                     Jubilado = a.MAESOC_JUBIL,
                     EstadoCivil = a.MAESOC_ESTCIV.ToString(),
                     Edad = fnc.calcular_edad(a.MAESOC_FECHANAC).ToString(),
                     Calle = a.MAESOC_CALLE,
                     Barrio = a.MAESOC_BARRIO,
                     NroCalle = a.MAESOC_NROCALLE,
                     Localidad = mtdFuncUtiles.GetLocalidad(a.MAESOC_CODLOC), //fnc.GetLocalidad(a.MAESOC_CODLOC),
                     Telefono = a.MAESOC_TEL,
                     EmpresaNombre = empr.MAEEMP_NOMFAN,
                     EmpresaTelefono = empr.MAEEMP_TEL,
                     EmpresaDomicilio = empr.MAEEMP_CALLE + " Nº" + empr.MAEEMP_NRO,
                     EmpresaContador = empr.MAEEMP_ESTUDIO_CONTACTO,
                     EmpresaContadorTelefono = empr.MAEEMP_ESTUDIO_TEL,
                     EmpresaContadorEmail = empr.MAEEMP_ESTUDIO_EMAIL,
                     EmpresaEmail = empr.MAEEMP_EMAIL,
                     EmpresaCodigoPostal = empr.MAEEMP_CODPOS,
                     EmpresaLocalidad = mtdFuncUtiles.GetLocalidad(Convert.ToInt32(empr.MAEEMP_CODLOC)),
                     //Aportes = GetAportes(a.MAESOC_CUIL_STR)
                     Carencia = false
                   }).ToList();
        //_BuscarPor
        // 0 D.N.I.
        // 1 Apellido y Nombre
        // 2 Empresa
        // 3 Todas las Empresas

        if (_Socios.Count() > 0)
        {
          Getddjj(_BuscarPor, cuit);
        }

        var empresa = from a in context.maeemp
                      select new Empresa
                      {
                        MEEMP_CUIT_STR = a.MEEMP_CUIT_STR,
                        MAEEMP_RAZSOC = a.MAEEMP_RAZSOC,
                      };

        _Empresas.AddRange(empresa.ToList());

        _Socios.ForEach(x => x.JornadaParcial = GetJornada(_BuscarPor, x.CUIL));
        _Socios.ForEach(x => x.Aportes = GetAportes(x.CUIL));
        _Socios.ForEach(x => x.Carencia = VerificarCarencia(x.Aportes.Max(y => y.Periodo)));

        if (Carencia)
        {
          return _Socios.Where(x => x.Carencia == false).OrderBy(x => x.ApeNom).ToList();
        }
        else
        {
          return _Socios.OrderBy(x => x.ApeNom).ToList();
        }
        //return _Socios.Where(x => x.Carencia == false).OrderBy(x => x.ApeNom).ToList();
      }
    }

    public static bool GetJornada(int buscarpor, string CUIL)
    {

      return _ddjj.Where(x => x.cuil == CUIL).OrderByDescending(x => x.periodo).FirstOrDefault().jorp;
    }

    public static void Getddjj(int buscarPor, string cuit)
    {
      using (var context = new lts_sindicatoDataContext())
      {
        switch (buscarPor)
        {
          case 0:

            List<DDJJ> dj0 = (from a in context.ddjj
                              where
                              a.CUIL_STR == _Socios.FirstOrDefault().CUIL
                              select new DDJJ
                              {
                                periodo = Convert.ToDateTime(a.periodo),
                                cuil = a.CUIL_STR,
                                jorp = a.jorp,
                                AporteLey = Convert.ToDecimal((a.impo + a.impoaux) * 0.02),
                                AporteSocio = Convert.ToDecimal((a.item2 == true) ? (a.impo + a.impoaux) * 0.02 : 0),
                                Sueldo = Convert.ToDecimal(a.impo + a.impoaux),
                                cuite = a.CUIT_STR
                              }).ToList();
            _ddjj.Clear();
            _ddjj.AddRange(dj0);
            break;

          case 1:

            _ddjj.Clear();
            foreach (var item in _Socios.ToList())
            {
              List<DDJJ> dj1 = (from a in context.ddjj
                                where
                                a.CUIL_STR == item.CUIL
                                select new DDJJ
                                {
                                  periodo = Convert.ToDateTime(a.periodo),
                                  cuil = a.CUIL_STR,
                                  jorp = a.jorp,
                                  AporteLey = Convert.ToDecimal((a.impo + a.impoaux) * 0.02),
                                  AporteSocio = Convert.ToDecimal((a.item2 == true) ? (a.impo + a.impoaux) * 0.02 : 0),
                                  Sueldo = Convert.ToDecimal(a.impo + a.impoaux),
                                  cuite = a.CUIT_STR
                                }).ToList();
              _ddjj.AddRange(dj1);
            }
            break;

          case 2:

            List<DDJJ> dj2 = (from a in context.ddjj
                              where
                              a.CUIT_STR == cuit
                              select new DDJJ
                              {
                                periodo = Convert.ToDateTime(a.periodo),
                                cuil = a.CUIL_STR,
                                jorp = a.jorp,
                                AporteLey = Convert.ToDecimal((a.impo + a.impoaux) * 0.02),
                                AporteSocio = Convert.ToDecimal((a.item2 == true) ? (a.impo + a.impoaux) * 0.02 : 0),
                                Sueldo = Convert.ToDecimal(a.impo + a.impoaux),
                                cuite = a.CUIT_STR
                              }).ToList();
            _ddjj.Clear();
            _ddjj.AddRange(dj2);
            break;

            //case 3:
            //  _ddjj.Clear();
            //  foreach (var item in _Socios.ToList())
            //  {
            //    List<DDJJ> dj3 = (from a in context.ddjj
            //                      where
            //                      a.CUIT_STR != cuit
            //                      select new DDJJ
            //                      {
            //                        periodo = Convert.ToDateTime(a.periodo),
            //                        cuil = a.CUIL_STR,
            //                        jorp = a.jorp,
            //                        AporteLey = Convert.ToDecimal((a.impo + a.impoaux) * 0.02),
            //                        AporteSocio = Convert.ToDecimal((a.item2 == true) ? (a.impo + a.impoaux) * 0.02 : 0),
            //                        cuite = a.CUIT_STR
            //                      }).ToList();

            //    _ddjj.AddRange(dj3);
            //  }
            //  break;
        }
      }
    }

    public static List<mdlAportes> GetAportes(string CUIL)
    {

      using (var context = new lts_sindicatoDataContext())
      {
        var Aportes = from a in _ddjj
                      where a.cuil == CUIL
                      select new mdlAportes
                      {
                        Periodo = Convert.ToDateTime(a.periodo).Date,
                        AporteLey = a.AporteLey, //Convert.ToDecimal((a.impo + a.impoaux) * 0.02),
                        AporteSocio = a.AporteSocio,
                        RazonSocial = getRazonSocial(a.cuite),//Convert.ToDecimal((a.item2 == true) ? (a.impo + a.impoaux) * 0.02 : 0),
                        Sueldo = a.Sueldo
                      };
        return Aportes.OrderByDescending(x => x.Periodo).ToList();
      }
    }

    public static string getRazonSocial(string cuit)
    {
      return _Empresas.Where(x => x.MEEMP_CUIT_STR == cuit).FirstOrDefault().MAEEMP_RAZSOC.Trim();
      //return razsoc;//_Empresas.Where(x => x.MEEMP_CUIT_STR == cuit).Select(x => x.MAEEMP_RAZSOC).ToString();
    }

    private static bool VerificarCarencia(DateTime ultimaDDJJ)
    {
      DateTime UltimoPeriodoDeclarado = ultimaDDJJ;
      DateTime hoy = DateTime.Now;

      int meses = Convert.ToInt32((hoy - UltimoPeriodoDeclarado).TotalDays) / 30;

      if (meses > 3)
      {
        return true;
      }
      else
      {
        return false;
      }

      //    int F = 0;
      //  int ContadorDeAportes = 0;

      //  if (meses <= 3)
      //  {
      //    switch (meses)
      //    {
      //      case 1:
      //        F = Fila - 1;
      //        break;
      //      case 2:
      //        F = Fila - 2;
      //        break;
      //      case 3:
      //        F = Fila - 3;
      //        break;
      //      default:

      //        break;
      //    }
      //  }
      //  else
      //  {
      //    F = 0;
      //    Fila = 0;
      //  }
      //  for (int i = F; i < Fila; i++)
      //  {
      //    if (Convert.ToDecimal(dgv_MostrarAportes.Rows[i].Cells["aporte_socio"].Value) > 0)
      //    {
      //      ContadorDeAportes += 1;
      //    }
      //  }
      //  return ContadorDeAportes > 0 ? true : false;
    }
  }
}


