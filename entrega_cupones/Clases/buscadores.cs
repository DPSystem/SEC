using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using entrega_cupones.Clases;

namespace entrega_cupones.Clases
{
  public class Buscadores
  {
    lts_sindicatoDataContext db_sindicato = new lts_sindicatoDataContext();

    public List<Edades> lst_edades = new List<Edades>(); // lista de tipo edades

    public List<prueba> pru = new List<prueba>();

    public List<ActasInvolucradas> lst_actas_involucradas = new List<ActasInvolucradas>();

    public List<inspectores> lst_inspectores = new List<inspectores>();

    public inspectores inspector = new inspectores();

    public Soc dat_soc = new Soc();

    public roles rol = new roles();

    public string codigo_postal;

    double cod = 0;

    int estado = 0;

    Binary foto;

    public Buscadores()
    {

    }

    public class Soc
    {
      public int Id { get; set; }
      public string cuil { get; set; }
      public Binary foto { get; set; }
      public byte estado { get; set; }
      public string dni { get; set; }
      public string nombre { get; set; }
      public char sexo { get; set; }
      public string empresa { get; set; }
      public string nrosocio { get; set; }

    }

    public class Edades
    {
      public string Sexo { get; set; }
      public int Edad { get; set; }
    }

    public class prueba
    {
      public int edad { get; set; }
      public string sexo { get; set; }
    }

    public class ActasInvolucradas
    {
      public DateTime fecha_asig { get; set; }
      public double acta { get; set; }
      public DateTime desde { get; set; }
      public DateTime hasta { get; set; }
      public string estado { get; set; }
      public string inspector { get; set; }
      public double importe_acta { get; set; }
    }

    public class inspectores
    {
      public int id_inspector { get; set; }
      public int esEstudio { get; set; }
      public string apellido { get; set; }


    }

    public class roles
    {
      public int id_rol { get; set; }
      public string rol_nombre { get; set; }
    }

    public List<prueba> pr(string codigo_postal)
    {
      //    var it = from a in db_sindicato.maesoc select a;//where a.MAESOC_NRODOC == "26954776" select a;
      //        //db_sindicato.maeflia.Where(x => x.MAEFLIA_NRODOC == 26954776).Select(x => x.MAEFLIA_NRODOC).ToList();

      //    foreach (var item in it.ToList())
      //    {
      //        prueba p = new prueba();
      //        Func_Utiles func = new Func_Utiles();
      //        p.edad = func.calcular_edad(item.MAESOC_FECHANAC);
      //        p.sexo = item.MAESOC_SEXO.ToString();
      //        pru.Add(p);
      //    }
      //    return pru;


      Func_Utiles funciones_utiles = new Func_Utiles();

      var edad_mochilas = (from a in db_sindicato.soccen
                           join sf in db_sindicato.socflia on a.SOCCEN_CUIL equals sf.SOCFLIA_CUIL
                           join flia in db_sindicato.maeflia on sf.SOCFLIA_CODFLIAR equals flia.MAEFLIA_CODFLIAR
                           join maesocio in db_sindicato.maesoc on a.SOCCEN_CUIL equals maesocio.MAESOC_CUIL
                           where a.SOCCEN_ESTADO == 1 && (codigo_postal == "0" ? maesocio.MAESOC_CODPOS != codigo_postal : maesocio.MAESOC_CODPOS == codigo_postal)
                           select new
                           {
                             sexo = flia.MAEFLIA_SEXO,
                             edad = funciones_utiles.calcular_edad(flia.MAEFLIA_FECNAC),
                           }).ToList();
      if (edad_mochilas.Count() > 0)
      {
        prueba edad_ = new prueba();
        foreach (var item in edad_mochilas.ToList())
        {
          edad_.sexo = item.sexo.ToString();
          edad_.edad = item.edad;
          pru.Add(edad_);
        }
      }
      return (pru);
    }

    public Soc get_beneficiario(string dni) // busco un socio beneficiario desde un DNI 
    {
      var c = from a in db_sindicato.maeflia
              where a.MAEFLIA_NRODOC == Convert.ToDouble(dni)
              select new
              {
                codfliar = a.MAEFLIA_CODFLIAR,
                nrodni = a.MAEFLIA_NRODOC,
                nombre = a.MAEFLIA_NOMBRE.Trim() + " " + a.MAEFLIA_APELLIDO.Trim(),
                sexo = a.MAEFLIA_SEXO,

              };
      if (c.Count() > 0)
      {

        cod = c.Max(x => x.codfliar);
        var sss = from a in c
                  where a.codfliar == cod
                  select a;
        dat_soc.dni = sss.Single().nrodni.ToString();
        dat_soc.nombre = sss.Single().nombre;
        dat_soc.sexo = sss.First().sexo;

        var cuil = from a in db_sindicato.socflia where a.SOCFLIA_CODFLIAR == sss.Single().codfliar select a;

        var nrosoc = (from a in db_sindicato.maesoc
                      where a.MAESOC_CUIL == cuil.First().SOCFLIA_CUIL
                      select a).First().MAESOC_NROAFIL;
        dat_soc.nrosocio = nrosoc.ToString();

        var e = from a in db_sindicato.socflia
                where a.SOCFLIA_CODFLIAR == cod
                select new { estado = a.SOCFLIA_ESTADO };
        if (e.Count() > 0)
        {
          estado = e.Single().estado;
        }
        var f = from a in db_sindicato.fotos where a.FOTOS_CODFLIAR == cod select new { foto = a.FOTOS_FOTO };
        if (f.Count() > 0)
        {
          foto = f.First().foto;
        }
        else
        {
          foto = null;
        }
        dat_soc.cuil = "0";
        dat_soc.foto = foto;
        dat_soc.estado = Convert.ToByte(estado);
      }
      else
      {
        dat_soc = null;
      }
      return dat_soc;
    }

    public Soc get_titular(string dni) // busco un socio titular desde un DNI
    {
      var socio_maesoc = from a in db_sindicato.maesoc
                         where a.MAESOC_NRODOC == dni
                         select new
                         {
                           cuil = a.MAESOC_CUIL,
                           nrodni = a.MAESOC_NRODOC,
                           nombre = a.MAESOC_APELLIDO.Trim() + " " + a.MAESOC_NOMBRE,
                           sexo = a.MAESOC_SEXO,
                           nrosocio = a.MAESOC_NROAFIL
                         };
      if (socio_maesoc.Count() > 0)
      {
        dat_soc.dni = socio_maesoc.First().nrodni;
        dat_soc.nombre = socio_maesoc.First().nombre;
        dat_soc.sexo = socio_maesoc.First().sexo;
        dat_soc.nrosocio = socio_maesoc.First().nrosocio;
        var socio_soccen = from a in db_sindicato.soccen
                           join fot in db_sindicato.fotos on a.SOCCEN_CUIL equals fot.FOTOS_CUIL
                           where a.SOCCEN_CUIL == socio_maesoc.First().cuil
                           select new
                           {
                             cuil = fot.FOTOS_CUIL,
                             foto = fot.FOTOS_FOTO,
                             estado = a.SOCCEN_ESTADO
                           };
        if (socio_soccen.Count() > 0)
        {
          socio_soccen.First();
          dat_soc.cuil = socio_soccen.First().cuil.ToString();
          dat_soc.foto = socio_soccen.First().foto;
          dat_soc.estado = socio_soccen.First().estado;
        }
      }
      //else
      //{
      //  dat_soc = null;
      //}
      return dat_soc;
    }

    public List<Edades> get_edades(string codigo_postal)
    {
      Func_Utiles funciones_utiles = new Func_Utiles();

      var edad_mochilas = (from a in db_sindicato.soccen
                           join sf in db_sindicato.socflia on a.SOCCEN_CUIL equals sf.SOCFLIA_CUIL
                           join flia in db_sindicato.maeflia on sf.SOCFLIA_CODFLIAR equals flia.MAEFLIA_CODFLIAR
                           join maesocio in db_sindicato.maesoc on a.SOCCEN_CUIL equals maesocio.MAESOC_CUIL
                           where a.SOCCEN_ESTADO == 1 && (codigo_postal == "0" ? maesocio.MAESOC_CODPOS != codigo_postal : maesocio.MAESOC_CODPOS == codigo_postal)
                           select new
                           {
                             sexo = flia.MAEFLIA_SEXO,
                             edad = funciones_utiles.calcular_edad(flia.MAEFLIA_FECNAC),
                           }).ToList();
      if (edad_mochilas.Count() > 0)
      {
        Edades edad_ = new Edades();
        foreach (var item in edad_mochilas.ToList())
        {
          edad_.Sexo = item.sexo.ToString();
          edad_.Edad = item.edad;
          lst_edades.Add(edad_);
        }
      }
      return (lst_edades);

    } //Obtengo un listado con las edades de un codigo postal 

    public Soc GetUsuario(string Usr, string Pwd) // obtengo el Id del usuario para la variable de sesion
    {
      //Soc User = new Soc();
      using (var context = new lts_sindicatoDataContext())
      {
        var usuario = from a in context.Usuarios where a.Usuario == Usr && a.Password == Pwd select a;
        if (usuario.Count() > 0)
        {
          dat_soc.Id = usuario.Single().idUsuario;
          dat_soc.nombre = usuario.First().Usuario;
          dat_soc.dni = usuario.First().DNI;
          dat_soc.empresa = get_roles(usuario.First().idRol).rol_nombre;// paso el onmbre del rol
          dat_soc.nrosocio = usuario.First().idRol.ToString(); // Paso el ID del rol para  los peemisos 
        }
      }
      return dat_soc;
    }

    public roles get_roles(int idrol)
    {
      using (var context = new lts_sindicatoDataContext())
      {
        var r = from a in context.Roles where a.IdRol == idrol select a;
        rol.id_rol = r.First().IdRol;
        rol.rol_nombre = r.First().NombreRol;
      }
      return rol;
    } // Obtengo un rol pasando un Id Rol

    public List<ActasInvolucradas> get_actas_involucradas(string cuit)
    {
      using (var context = new lts_sindicatoDataContext())
      {
        var actas_inv = from act in context.ACTAS
                        where act.CUIT == Convert.ToInt64(cuit)
                        select new
                        {
                          fecha_asig = act.FECHA_ASIG,
                          acta = act.ACTA,
                          desde = act.DESDE,
                          hasta = act.HASTA,
                          estado = act.COBRADOTOTALMENTE,
                          inspector = act.INSPECTOR,
                          importe_acta = act.DEUDATOTAL
                        };
        if (actas_inv.Count() > 0)
        {
          foreach (var item in actas_inv.ToList())
          {
            ActasInvolucradas ai = new ActasInvolucradas();
            ai.fecha_asig = Convert.ToDateTime(item.fecha_asig);
            ai.acta = Convert.ToDouble(item.acta);
            ai.desde = Convert.ToDateTime(item.desde);
            ai.hasta = Convert.ToDateTime(item.hasta);
            ai.estado = item.estado;
            ai.inspector = item.inspector;
            ai.importe_acta = Convert.ToDouble(item.importe_acta);
            lst_actas_involucradas.Add(ai);
          }
        }
      }
      return lst_actas_involucradas;
    } // obtengo las actas involucradas segun un CUIT 

    public List<inspectores> get_inspectores()
    {
      using (var context = new lts_sindicatoDataContext())
      {
        var inspect = from a in context.inspectores select a;
        if (inspect.Count() > 0)
        {
          foreach (var item in inspect.ToList())
          {
            inspectores insp = new inspectores();
            insp.id_inspector = item.ID_INSPECTOR;
            insp.apellido = item.APELLIDO.Trim();
            insp.esEstudio = Convert.ToInt16(item.ESTUDIO);
            lst_inspectores.Add(insp);
          }
        }
      }
      return lst_inspectores;
    } //Obtengo una lista de inspectores

    public inspectores get_un_inspector(int id_inspector)
    {
      using (var inspectores = new lts_sindicatoDataContext())
      {
        var inspector_ = from a in inspectores.inspectores
                         where a.ID_INSPECTOR == id_inspector
                         select a;
        if (inspector_.Count() > 0)
        {
          inspector.apellido = inspector_.FirstOrDefault().APELLIDO;
          inspector.esEstudio = Convert.ToInt16(inspector_.FirstOrDefault().ESTUDIO);
        }
        else
        {
          inspector.apellido = "";
        }
      }
      return inspector;
    } //Obtengo un inspector en particular

    public string get_codigo_postal(string cuit)
    {
      using (var context = new lts_sindicatoDataContext())
      {
        var cod_post = from a in context.maeemp
                       where a.MEEMP_CUIT_STR == cuit || a.MAEEMP_CUIT == Convert.ToDouble(cuit)
                       select new { codigo_postal = a.MAEEMP_CODPOS };
        //cod_post.Count() > 0 ? return cod_post.First().codigo_postal : return "";
        if (cod_post.Count() > 0)
        {
          return cod_post.First().codigo_postal;
        }
        else
        {
          return "";
        }
      }
    } // Obtengo una la localidad atravez de un CUIT

    public Binary get_foto(double cuil)  // obtengo la foto de un SOCIO beneficiario desde un CUIL
    {
      using (var context = new lts_sindicatoDataContext())
      {
        var foto_ = from a in context.fotos
                    where a.FOTOS_CUIL == cuil
                    select new
                    {
                      foto = a.FOTOS_FOTO
                    };
        if (foto_.Count() > 0)
        {
          foto = foto_.FirstOrDefault().foto;
        }
      }
      return foto;
    }

    public Binary get_foto(int codfliar)  // obtengo la foto de un SOCIO beneficiario desde un CUIL
    {
      using (var context = new lts_sindicatoDataContext())
      {
        var foto_ = from a in context.fotos
                    where a.FOTOS_CODFLIAR == codfliar
                    select new
                    {
                      foto = a.FOTOS_FOTO
                    };
        if (foto_.Count() > 0)
        {
          foto = foto_.FirstOrDefault().foto;
        }
      }
      return foto;
    }

    public string GetEmpresaPorCUIT(string cuit)
    {
      using (var context = new lts_sindicatoDataContext() )
      {
        string ret = string.Empty;
        var empresa = (from a in context.maeemp where a.MEEMP_CUIT_STR == cuit select new { Empresa = a.MAEEMP_RAZSOC.Trim()}).ToList();
        if (empresa.Count() > 0 )
        {
          ret = empresa.Single().Empresa;
        }
        return ret;  //context.maeemp.Where(x => x.MEEMP_CUIT_STR == cuit).Select(x => x.MAEEMP_RAZSOC);
      }
    }

  }
}
