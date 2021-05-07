using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entrega_cupones.Clases
{
  class EventosCupones
  {
    public cls_EventosCupones varEventCupon = new cls_EventosCupones();

    public List<cls_EventosCupones> lst_cupones = new List<cls_EventosCupones>();

    public ClsBeneficiarioExepcion benefexep = new ClsBeneficiarioExepcion();

    public List<ClsDetalleDeEntrada> LstDetalleEntradas = new List<ClsDetalleDeEntrada>();

    public class ClsDetalleDeEntrada
    {
      public DateTime FechaDeEntrega { get; set; }
      public int NumeroDeEntrada { get; set; }
      public string Usuario { get; set; }
      public decimal Costo { get; set; }
      public bool EsAcompañante { get; set; }
      public int NumeroDeComprobante { get; set; }
      public bool SinCargo { get; set; }
    }

    public class cls_EventosCupones
    {
      public int eventcupon_evento_id { get; set; }
      public double eventcupon_maesoc_cuil { get; set; }
      public int eventcupon_maeflia_codfliar { get; set; }
      public int event_cupon_event_exep_id { get; set; }
      public int event_cupon_nro { get; set; }
      public DateTime event_cupon_fecha { get; set; }
    }

    public class ClsBeneficiarioExepcion
    {
      public string nombre { get; set; }
      public string parentesco { get; set; }
      public string dni { get; set; }
      public int edad { get; set; }
      public string sexo { get; set; }
      public int codigofliar { get; set; }
      public int exepxionID { get; set; }
    }

    public  string GetDiaHoraDelTurno(int TurnoId)
    {
      using (var context = new lts_sindicatoDataContext())
      {
        var turno = from a in context.Turnos where a.Id == TurnoId select a;
        if (turno.Count() > 0)
        {
          return turno.First().Dia.Value.Date.ToString("dd/MM/yyyy") + " a las  " + turno.First().Hora.ToString();
        }
        else
        {
          return "Sin Asignar";
        }
      }
    }

    public int ConsultarTurno(string CuilSocio)
    {
      using (var context = new lts_sindicatoDataContext())
      {
        var tur = (from a in context.Turnos where a.CuilSocio == CuilSocio select a);
        if (tur.Count() == 0)
        {
          return 0 ;
        }
        else
        {
          return tur.First().Id;
        }
      }
    }

    public  int GetTurno(string CuilSocio, int Termas)
    {
      int TurnoId = 0;
      using (var context = new lts_sindicatoDataContext())
      {
        var tur = (from a in context.Turnos where a.CuilSocio == CuilSocio select a);
        if (tur.Count() == 0)
        {
          Turnos turnos = ((from a in context.Turnos where a.CuilSocio == "0" && 
                            a.Localidad == (Termas == 1 ? 1:0)
                            select a).OrderBy(x => x.Dia).ThenBy(x => x.Hora)).FirstOrDefault();
          turnos.CuilSocio = CuilSocio;
          TurnoId = turnos.Id;
          context.SubmitChanges();
        }
        else
        {
          TurnoId = tur.FirstOrDefault().Id;
        }
      }
      return TurnoId;
    }

    public int EventosCuponesInsertar(int eventoID, double cuil, int CodigoFliar, int ExepcionID, int ArticuloID, int UsuarioID, string QuienRetira, int FondoDeDesempleo, string cuilSocio, int Termas)
    {
      using (lts_sindicatoDataContext context = new lts_sindicatoDataContext())
      {
        eventos_cupones insert = new eventos_cupones();

        if (context.eventos_cupones.Count() > 0)
        {
          insert.event_cupon_nro = context.eventos_cupones.Max(x => x.event_cupon_nro) + 1;
        }
        else
        {
          insert.event_cupon_nro = 1;
        }

        insert.TurnoId = GetTurno(cuilSocio,Termas);
        insert.eventcupon_evento_id = eventoID;
        insert.eventcupon_maesoc_cuil = cuil;
        insert.eventcupon_maeflia_codfliar = CodigoFliar;
        insert.event_cupon_event_exep_id = ExepcionID;
        insert.event_cupon_fecha = DateTime.Now;
        insert.UsuarioId = UsuarioID;
        insert.ArticuloID = ArticuloID;
        insert.QuienRetiraCupon = QuienRetira;
        insert.FondoDeDesempleo = FondoDeDesempleo;
        insert.CuilStr = cuilSocio;

        context.eventos_cupones.InsertOnSubmit(insert);
        context.SubmitChanges();

        return insert.event_cupon_nro;
      }
    }

    public bool ExisteTitular(int NroDni, int TipoDeEvento)
    {
      using (lts_sindicatoDataContext context = new lts_sindicatoDataContext())
      {
        var ExisteExepxion = from a in context.eventos_cupones
                             where a.eventcupon_maesoc_cuil == NroDni && a.eventcupon_evento_id == TipoDeEvento
                             select a;
        if (ExisteExepxion.Count() > 0)
        {
          return true;
        }
        else
        {
          return false;
        }
      }
    }

    public bool ExisteExepcion(int ID)
    {
      using (lts_sindicatoDataContext context = new lts_sindicatoDataContext())
      {
        var ExisteExepxion = from a in context.eventos_cupones where a.event_cupon_event_exep_id == ID select a;
        if (ExisteExepxion.Count() > 0)
        {
          return true;
        }
        else
        {
          return false;
        }
      }
    }

    public bool ExisteFamiliar(int ID)
    {
      using (lts_sindicatoDataContext context = new lts_sindicatoDataContext())
      {
        var ExisteExepxion = from a in context.eventos_cupones where a.eventcupon_maeflia_codfliar == ID select a;
        if (ExisteExepxion.Count() > 0 && ID > 0)
        {
          return true;
        }
        else
        {
          return false;
        }
      }
    }

    public int GetCuponID()
    {
      using (lts_sindicatoDataContext context = new lts_sindicatoDataContext())
      {
        var CuponId = from a in context.eventos_cupones select new { Id = a.eventcupon_id };
        if (CuponId.Count() > 0)
        {
          return CuponId.ToList().Max(x => x.Id);
        }
        else return 0;
      }
    }

    public List<cls_EventosCupones> GetTodos()
    {
      using (lts_sindicatoDataContext context = new lts_sindicatoDataContext())
      {
        var cupones_todos = from a in context.eventos_cupones select a;
        if (cupones_todos.Count() > 0)
        {
          cls_EventosCupones insert = new cls_EventosCupones();
          foreach (var item in cupones_todos)
          {
            insert.eventcupon_evento_id = item.eventcupon_evento_id;
            insert.eventcupon_maesoc_cuil = item.eventcupon_maesoc_cuil;
            insert.eventcupon_maeflia_codfliar = item.eventcupon_maeflia_codfliar;
            insert.event_cupon_event_exep_id = item.event_cupon_event_exep_id;
            insert.event_cupon_fecha = DateTime.Now;
            lst_cupones.Add(insert);
          }
        }
        return lst_cupones;
      }
    }

    public int GetCantidadCupones()
    {
      using (lts_sindicatoDataContext context = new lts_sindicatoDataContext())
      {
        var cupones_todos = from a in context.eventos_cupones where a.event_cupon_nro > 0 select a;
        return cupones_todos.Count();
      }
    }

    public int GetNumeroCupon(int codfliar)
    {
      using (lts_sindicatoDataContext context = new lts_sindicatoDataContext())
      {
        var nro_cupon = from a in context.eventos_cupones where a.eventcupon_maeflia_codfliar == codfliar select new { nrocupon = a.event_cupon_nro };
        if (nro_cupon.Count() > 0)
        {
          return nro_cupon.First().nrocupon;
        }
        else
        {
          return 0;
        }

      }
    }

    public int GetNumeroCuponPorID(int id)
    {
      using (lts_sindicatoDataContext context = new lts_sindicatoDataContext())
      {
        var nro_cupon = from a in context.eventos_cupones where a.ComprobanteId == id select new { nrocupon = a.event_cupon_nro };
        if (nro_cupon.Count() > 0)
        {
          return nro_cupon.First().nrocupon;
        }
        else
        {
          return 0;
        }

      }
    }

    public int GetNumeroCuponPorCuil(double cuil, int TipoDeEvento)
    {
      using (lts_sindicatoDataContext context = new lts_sindicatoDataContext())
      {
        var nro_cupon = from a in context.eventos_cupones
                        where a.eventcupon_maesoc_cuil == cuil
&& a.eventcupon_evento_id == TipoDeEvento
                        select new { nrocupon = a.event_cupon_nro };
        if (nro_cupon.Count() > 0)
        {
          return nro_cupon.First().nrocupon;
        }
        else
        {
          return 0;
        }

      }
    }


    public DataTable GetReimpresionCupon(int _codigofliar, int _exepcionID) //, double _cuil, string _edad, string dni, string _sexo, string edad)
    {
      using (lts_sindicatoDataContext context = new lts_sindicatoDataContext())
      {
        int a_buscar = 0;
        if (_codigofliar > 0)
        {
          a_buscar = _codigofliar;
        }
        else
        {
          a_buscar = _exepcionID;
        }

        var ExisteExepcion = from a in context.eventos_cupones where _codigofliar > 0 ? a.eventcupon_maeflia_codfliar == _codigofliar : a.event_cupon_event_exep_id == _exepcionID select a;

        DS_cupones ds = new DS_cupones();
        DataTable dt = ds.cupon_dia_niño;
        dt.Clear();
        if (ExisteExepcion.Count() > 0)
        {
          EventosCupones EvntCpn = new EventosCupones();
          ClsBeneficiarioExepcion datos_benef = new ClsBeneficiarioExepcion();

          socios soc = new socios();
          EventosExepciones EvntExep = new EventosExepciones();

          DataRow dr = dt.NewRow();
          var datos = _codigofliar > 0 ?
            soc.get_datos_socio(soc.GetCuilPorCodFliar(_codigofliar), 0) :
            soc.get_datos_socio(EvntExep.GetCuilExepcionPorID(_exepcionID), 0);


          dr["titu_apenom"] = datos.apellido + " " + datos.nombre;
          dr["titu_dni"] = datos.dni;
          dr["titu_empresa"] = datos.empresa;
          dr["titu_nrosocio"] = datos.nrosocio;
          dr["titu_foto"] = soc.get_foto_titular_binary(datos.cuil).ToArray();//ExisteExepcion.First().eventcupon_maesoc_cuil).ToArray();
          if (_codigofliar == 0)
          {
            datos_benef = EvntCpn.GetDatosExepcion(_exepcionID);
            dr["benef_foto"] = soc.get_foto_benef_binary(1).ToArray();
          }
          else
          {
            datos_benef = EvntCpn.GetDatosBenef(_codigofliar);
            dr["benef_foto"] = soc.get_foto_benef_binary(_codigofliar).ToArray();
          }

          dr["benef_apenom"] = datos_benef.nombre;//fila.Cells["nombre"].Value;
          dr["benef_dni"] = datos_benef.dni;//fila.Cells["Dni"].Value;
          dr["benef_sexo"] = datos_benef.sexo;//fila.Cells["sexo"].Value;
          dr["benef_edad"] = datos_benef.edad;

          dr["event_nrocupon"] = ExisteExepcion.First().event_cupon_nro;
          dr["event_fechaentrega"] = DateTime.Now;
          dr["event_cupon_ID"] = ExisteExepcion.First().eventcupon_id;
          dr["reimpresion"] = "1"; //Para indicar que es la reimprecion del cupon
          dt.Rows.Add(dr);
          return dt;
        }
        return dt;
      }
    }

    public DataTable GetReimpresionCuponDiaDeLaMujer(int NroDni, int TipoDeEvento)
    {
      using (var context = new lts_sindicatoDataContext())
      {
        var ExisteCupon = from a in context.eventos_cupones where a.eventcupon_maesoc_cuil == NroDni && a.eventcupon_evento_id == TipoDeEvento select a;
        DS_cupones ds = new DS_cupones();
        DataTable dt = ds.cupon_dia_niño;
        dt.Clear();
        if (ExisteCupon.Count() > 0)
        {
          socios soc = new socios();
          DataRow dr = dt.NewRow();

          var datos = soc.get_datos_socio(0, Convert.ToInt32(ExisteCupon.Single().eventcupon_maesoc_cuil));

          dr["titu_apenom"] = datos.apellido + " " + datos.nombre;
          dr["titu_dni"] = datos.dni;
          dr["titu_empresa"] = datos.empresa;
          dr["titu_nrosocio"] = datos.nrosocio;
          dr["titu_foto"] = soc.get_foto_titular_binary(datos.cuil).ToArray();//ExisteExepcion.First().eventcupon_maesoc_cuil).ToArray();

          dr["event_nrocupon"] = ExisteCupon.First().event_cupon_nro;
          dr["event_fechaentrega"] = DateTime.Now;
          dr["event_cupon_ID"] = ExisteCupon.First().eventcupon_id;
          dr["reimpresion"] = "1"; //Para indicar que es la reimprecion del cupon
          dt.Rows.Add(dr);
        }
        return dt;
      }
    }

    public ClsBeneficiarioExepcion GetDatosBenef(double _codfliar)
    {
      using (lts_sindicatoDataContext context = new lts_sindicatoDataContext())
      {
        Func_Utiles fu = new Func_Utiles();
        Parentesco parent = new Parentesco();
        var Benef = from a in context.socflia
                    where a.SOCFLIA_CODFLIAR == _codfliar
                    join b in context.maeflia on a.SOCFLIA_CODFLIAR equals b.MAEFLIA_CODFLIAR
                    select new
                    {
                      nombre = b.MAEFLIA_APELLIDO.Trim() + " " + b.MAEFLIA_NOMBRE.Trim(),
                      parent = parent.GetParentescoDescrip(a.SOCFLIA_PARENT).parent_descrip,
                      dni = Convert.ToString(b.MAEFLIA_NRODOC),
                      sexo = b.MAEFLIA_SEXO,
                      edad = fu.calcular_edad(b.MAEFLIA_FECNAC),
                      codigofliar = Convert.ToInt32(b.MAEFLIA_CODFLIAR)
                    };
        if (Benef.Count() > 0)
        {
          benefexep.nombre = Benef.First().nombre;
          benefexep.parentesco = Benef.First().parent;
          benefexep.dni = Benef.First().dni;
          benefexep.sexo = Benef.First().sexo.ToString();
          benefexep.edad = Benef.First().edad;
          benefexep.codigofliar = Benef.First().codigofliar;
        }
        return benefexep;
      }

    }

    public ClsBeneficiarioExepcion GetDatosExepcion(double _expecionID)
    {
      using (lts_sindicatoDataContext context = new lts_sindicatoDataContext())
      {
        Func_Utiles fu = new Func_Utiles();
        Parentesco parent = new Parentesco();

        var Benef = from a in context.eventos_exep
                    where a.event_exep_id == _expecionID
                    select new
                    {
                      nombre = a.event_exep_apellido.Trim() + " " + a.event_exep_nombre.Trim(),
                      parent = parent.GetParentescoDescrip(a.event_exep_parent).parent_descrip,
                      dni = Convert.ToString(a.event_exep_dni),
                      sexo = a.event_exep_sexo,
                      edad = fu.calcular_edad(Convert.ToDateTime(a.event_exep_fechanac)),
                      codigofliar = 0
                    };
        //var Benef = from a in context.socflia
        //            where a.SOCFLIA_CUIL == 
        //            join b in context.maeflia on a.SOCFLIA_CODFLIAR equals b.MAEFLIA_CODFLIAR
        //select new
        //{
        //  nombre = b.MAEFLIA_APELLIDO.Trim() + " " + b.MAEFLIA_NOMBRE.Trim(),
        //  parent = parent.GetParentescoDescrip(a.SOCFLIA_PARENT).parent_descrip,
        //  dni = Convert.ToString(b.MAEFLIA_NRODOC),
        //  sexo = b.MAEFLIA_SEXO,
        //  edad = fu.calcular_edad(b.MAEFLIA_FECNAC),
        //  codigofliar = Convert.ToInt32(b.MAEFLIA_CODFLIAR)
        //};
        if (Benef.Count() > 0)
        {
          benefexep.nombre = Benef.First().nombre;
          benefexep.parentesco = Benef.First().parent;
          benefexep.dni = Benef.First().dni;
          benefexep.sexo = Benef.First().sexo.ToString();
          benefexep.edad = Benef.First().edad;
          benefexep.codigofliar = Benef.First().codigofliar;
        }
        return benefexep;
      }
    }

    public int InsertarEntradaSocio(int eventoID, double cuil, int UsuarioId)
    {
      using (lts_sindicatoDataContext context = new lts_sindicatoDataContext())
      {
        eventos_cupones insert = new eventos_cupones();

        if (context.eventos_cupones.Where(x => x.eventcupon_evento_id == eventoID).Count() > 0)
        {
          insert.event_cupon_nro = context.eventos_cupones.Where(x => x.eventcupon_evento_id == eventoID).Max(x => x.event_cupon_nro) + 1;
        }
        else
        {
          insert.event_cupon_nro = 1;
        }
        insert.eventcupon_evento_id = eventoID;
        insert.eventcupon_maesoc_cuil = cuil;
        insert.eventcupon_maeflia_codfliar = 0;
        insert.event_cupon_event_exep_id = 0;
        insert.event_cupon_fecha = DateTime.Now;
        insert.UsuarioId = UsuarioId;
        insert.Invitado = 0;
        insert.CajaId = 0;
        insert.Costo = 0;
        insert.ComprobanteId = 0;
        context.eventos_cupones.InsertOnSubmit(insert);
        context.SubmitChanges();
        return insert.event_cupon_nro;
      }
    }

    public int InsertarEntradaInvitadoNOSocio(int eventoID, double cuil, int UsuarioId, bool EsInvitado, int NumeroComprobante)
    {
      using (lts_sindicatoDataContext context = new lts_sindicatoDataContext())
      {
        eventos_cupones insert = new eventos_cupones();
        insert.event_cupon_nro = 0;
        insert.eventcupon_evento_id = eventoID;
        insert.eventcupon_maesoc_cuil = cuil;
        insert.eventcupon_maeflia_codfliar = 0;
        insert.event_cupon_event_exep_id = 0;
        insert.event_cupon_fecha = DateTime.Now;
        insert.UsuarioId = UsuarioId;
        insert.CajaId = 0;
        insert.Costo = 100;
        insert.Invitado = (EsInvitado == true) ? 1 : 0;
        insert.ComprobanteId = NumeroComprobante;
        context.eventos_cupones.InsertOnSubmit(insert);
        context.SubmitChanges();
        return Convert.ToInt32(insert.ComprobanteId);
      }
    }

    public int InsertarEntradaSinCargo(int eventoID, double cuil, int UsuarioId, int NumeroComprobante)
    {
      using (lts_sindicatoDataContext context = new lts_sindicatoDataContext())
      {
        eventos_cupones insert = new eventos_cupones();
        insert.event_cupon_nro = 0;
        insert.eventcupon_evento_id = eventoID;
        insert.eventcupon_maesoc_cuil = cuil;
        insert.eventcupon_maeflia_codfliar = 0;
        insert.event_cupon_event_exep_id = 0;
        insert.event_cupon_fecha = DateTime.Now;
        insert.UsuarioId = UsuarioId;
        insert.CajaId = 0;
        insert.Costo = 0;
        insert.Invitado = 2; // el numero 2 es para indicar que es entrada sin cargo
        insert.ComprobanteId = NumeroComprobante;
        context.eventos_cupones.InsertOnSubmit(insert);
        context.SubmitChanges();
        return Convert.ToInt32(insert.ComprobanteId);
      }
    }

    public decimal ConsultaDeCaja(int UsuarioId, int EventoId)
    {
      using (var context = new lts_sindicatoDataContext())
      {
        var caja = context.eventos_cupones.Where(x => x.UsuarioId == UsuarioId && x.eventcupon_evento_id == EventoId && x.CajaId == 0);
        if (caja.Count() > 0)
        {
          return Convert.ToDecimal(caja.Sum(x => x.Costo));
        }
        else
        {
          return 0;
        }
      }
    }

    public List<ClsDetalleDeEntrada> ControlarEntradasImpresas(int EventoId, double Cuil)
    {
      using (var context = new lts_sindicatoDataContext())
      {
        usuarios Usuarios = new usuarios();
        var EstaImpreso = context.eventos_cupones.Where(x => x.eventcupon_maesoc_cuil == Cuil);
        if (EstaImpreso.Count() > 0)
        {
          foreach (var item in EstaImpreso.ToList())
          {
            ClsDetalleDeEntrada DetalleEntrada = new ClsDetalleDeEntrada();
            DetalleEntrada.FechaDeEntrega = item.event_cupon_fecha;
            DetalleEntrada.NumeroDeEntrada = item.event_cupon_nro;
            DetalleEntrada.Usuario = Usuarios.ObtenerNombreDeUsuario(Convert.ToInt32(item.UsuarioId));
            DetalleEntrada.Costo = Convert.ToDecimal(item.Costo);
            DetalleEntrada.EsAcompañante = (item.Invitado == 1) ? true : false;
            DetalleEntrada.NumeroDeComprobante = Convert.ToInt32(item.ComprobanteId);
            DetalleEntrada.SinCargo = (item.Invitado == 2) ? true : false;
            LstDetalleEntradas.Add(DetalleEntrada);
          }
        }
        return LstDetalleEntradas;
      }
    }

    public void ReImpresion()
    {

    }



  }
}

