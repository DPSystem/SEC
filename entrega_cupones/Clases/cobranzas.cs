using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace entrega_cupones.Clases
{
  public class cobranzas
  {

    public class ClsCuadroDeAmortizacion
    {
      public string Cuota { get; set; }
      public double ImporteDeCuota { get; set; }
      public double Interes { get; set; }
      public double Amortizado { get; set; }
      public double AAmortizar { get; set; }
      public int ReciboDeCobro { get; set; }
      public DateTime FechaDeVenc { get; set; }
      public int Estado { get; set; }
    }

    public class fechas
    {
      public DateTime Desde { get; set; }
      public DateTime Hasta { get; set; }
    }

    public class NoAsignadas
    {
      public int Acta { get; set; }
      public string Empresa { get; set; }
      public string CobradorAsignado { get; set; }
    }
    public class ClsActa2
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
    public class clsActa
    {
      public int Id { get; set; }
      public int NroActa { get; set; }
      public decimal Importe { get; set; }
      public string CobradoTotalmente { get; set; }
      public decimal ImporteCobradoTotalmente { get; set; }
      public int Emitidas { get; set; }
      public decimal ImporteEmitidas { get; set; }
      public int ConDeuda { get; set; }
      public decimal ImporteConDeuda { get; set; }
      public int NoCobradas { get; set; }
      public decimal ImporteNoCobradas { get; set; }
      public int CobroParcial { get; set; }
      public decimal ImporteCobroParcial { get; set; }
      public decimal ImprorteFaltaCobrar { get; set; }
      public int EnEstudio { get; set; }
      public decimal ImporteEnEstudio { get; set; }
      public IEnumerable<ACTAS> AE_ { get; set; }
      public IEnumerable<ACTAS> TA_ { get; set; }
      public IEnumerable<ACTAS> CD_ { get; set; }
      public IEnumerable<ACTAS> NC_ { get; set; }
      public IEnumerable<ACTAS> CP_ { get; set; }
    }

    public List<ClsActa2> Acta2 = new List<ClsActa2>();

    public fechas IntervaloDeFechas = new fechas();

    public List<clsActa> actas = new List<clsActa>();

    public clsActa resumen = new clsActa();

    public clsActa todasLasActas = new clsActa();

    public List<ClsActa2> TodasLasActas2 = new List<ClsActa2>();

    public IEnumerable<ClsActa2> get_resumen2(string desde, string hasta, string inspector, int TodasLasActas)
    {
      if (TodasLasActas == 1)
      {
        desde = "01/2000";
        hasta = "01/3000";
      }
      var IntervaloDeFechas = ObtenerIntervaloDeFechas(desde, hasta);

      using (var context = new lts_sindicatoDataContext())
      {
        var actas = from a in context.ACTAS
                    join b in context.AsignarCobranza
                          on a.ACTA equals b.Acta
                          into c
                    from d in c.DefaultIfEmpty()
                      //join f in context.Cobradores
                      //      on d.CobradorID equals f.ID
                      //      into g
                      //from h in g.DefaultIfEmpty()
                    join f in context.Usuarios
                          on d.CobradorID equals f.idUsuario
                          into g
                    from h in g.DefaultIfEmpty()
                    where (a.ACTA > 0) &&
                          (inspector == "TODOS" ? a.INSPECTOR != "TODOS" : a.INSPECTOR == inspector) &&
                          (a.DEUDATOTAL > 0) &&
                          (a.FECHA >= IntervaloDeFechas.Desde && a.FECHA <= IntervaloDeFechas.Hasta.Date)
                    orderby a.FECHA
                    select new
                    {
                      a.FECHA,
                      a.ACTA,
                      a.EMPRESA,
                      a.CUIT,
                      a.DOMICILIO,
                      a.DESDE,
                      a.HASTA,
                      a.DEUDAHISTORICA,
                      a.INTERESES,
                      a.DEUDAACTUALIZADA,
                      a.INTERESFINANC,
                      a.DEUDATOTAL,
                      a.COBRADOTOTALMENTE,
                      a.INSPECTOR,
                      a.OBSERVACIONES,
                      a.IMPORTECOBRADO,
                      a.DIFERENCIA,
                      a.ESTUDIO_JURIDICO,
                      a.MONTO_CERTIF_ACTA,
                      cobrador = (int?)d.CobradorID,
                      CobradorNombre = h.Apellido + " " + h.Nombre,
                      NroAsignacion = (int?)d.NroAsignacion,
                      ImporteInteresActualizado = ObtenerImporteDeInteres(Convert.ToDateTime(a.FECHA), (a.DIFERENCIA < 0) ? Convert.ToDecimal(a.DIFERENCIA * -1) : 0, 3),
                      ImporteDeudaActualizada = ObtenerDeudaTotalConInteres(Convert.ToDateTime(a.FECHA), (a.DIFERENCIA < 0) ? Convert.ToDecimal(a.DIFERENCIA * -1) : 0, 3, Convert.ToDecimal(a.DIFERENCIA))
                    };

        foreach (var item in actas.ToList())
        {
          ClsActa2 insert = new ClsActa2();

          insert.FECHA = Convert.ToDateTime(item.FECHA);
          insert.ACTA = Convert.ToInt32(item.ACTA);
          insert.EMPRESA = item.EMPRESA;
          insert.CUIT = Convert.ToDouble(item.CUIT);
          insert.DOMICILIO = item.DOMICILIO;
          insert.DESDE = Convert.ToDateTime(item.DESDE);
          insert.HASTA = Convert.ToDateTime(item.HASTA);
          insert.DEUDAHISTORICA = Convert.ToDouble(item.DEUDAHISTORICA);
          insert.INTERESES = Convert.ToDouble(item.INTERESES);
          insert.DEUDAACTUALIZADA = Convert.ToDouble(item.DEUDAACTUALIZADA);
          insert.INTERESFINANC = Convert.ToDouble(item.INTERESFINANC);
          insert.DEUDATOTAL = Convert.ToDouble(item.DEUDATOTAL);
          insert.COBRADOTOTALMENTE = item.COBRADOTOTALMENTE != null ? item.COBRADOTOTALMENTE.ToString() : "";
          insert.INSPECTOR = item.INSPECTOR;
          insert.OBSERVACIONES = item.OBSERVACIONES;
          insert.IMPORTECOBRADO = Convert.ToDouble(item.IMPORTECOBRADO);
          insert.DIFERENCIA = Convert.ToDouble(item.DIFERENCIA);
          insert.ESTUDIO_JURIDICO = Convert.ToInt32(item.ESTUDIO_JURIDICO);
          insert.COBRADOR = Convert.ToInt32(item.cobrador);
          insert.CobradorNombre = item.CobradorNombre;
          insert.NROASIGNACION = Convert.ToInt32(item.NroAsignacion);
          insert.ImporteInteresActualizado = item.ImporteInteresActualizado;
          insert.IMPORTEDEUDAACTUALIZADA = item.ImporteDeudaActualizada;
          Acta2.Add(insert);
        }
        if (Acta2.Count() == 0)
        {

        }
        return Acta2.ToList();
      }
    }
    public clsActa get_resumen(string desde, string hasta, string inspector)
    {
      using (var context = new lts_sindicatoDataContext())
      {
        var IntervaloDeFechas = ObtenerIntervaloDeFechas(desde, hasta);

        IEnumerable<ACTAS> Actas = context.ACTAS.Where(x => x.FECHA >= IntervaloDeFechas.Desde &&
                                  x.FECHA <= IntervaloDeFechas.Hasta.Date &&
                                  (inspector == "TODOS" ? x.INSPECTOR != "TODOS" : x.INSPECTOR == inspector)
                                  )
                                  .OrderBy(x => x.FECHA);

        IEnumerable<ACTAS> ActasEmitidas = Actas.Where(x => x.ACTA > 0);


        resumen.AE_ = ActasEmitidas.ToList();

        return resumen;
      }
    }
    public clsActa get_TodasLasActas(string inspector)
    {
      using (var context = new lts_sindicatoDataContext())
      {
        IEnumerable<ACTAS> Actas = context.ACTAS.Where(x =>
                                  inspector == "TODOS" ? x.INSPECTOR != "TODOS" : x.INSPECTOR == inspector
                                  )
                                  .OrderBy(x => x.FECHA);

        IEnumerable<ACTAS> ActasEmitidas = Actas.Where(x => x.ACTA > 0); //Actas.Where(x => x.ACTA > 0).ToList();

        todasLasActas.TA_ = ActasEmitidas.ToList();
      }
      return todasLasActas;
    }
    public bool ValidarFecha(string fecha)
    {
      return DateTime.TryParse(fecha, out DateTime date);
    }
    public fechas ObtenerIntervaloDeFechas(string desde, string hasta)
    {

      Func_Utiles fu = new Func_Utiles();


      if (desde == "  /" && hasta == "  /")
      {
        IntervaloDeFechas.Desde = Convert.ToDateTime("01/01/2000");// new DateTime(01/01/2000);
        IntervaloDeFechas.Hasta = Convert.ToDateTime(fu.GetDia31DelMes(fu.generar_ceros(DateTime.Now.Month.ToString(), 2)) + "/" + fu.generar_ceros(DateTime.Now.Month.ToString(), 2) + "/" + DateTime.Now.Year.ToString()).Date;
      }

      if (desde == "  /" && hasta != "  /")
      {
        IntervaloDeFechas.Desde = Convert.ToDateTime("01/01/2000");// new DateTime(01/01/2000);
        IntervaloDeFechas.Hasta = Convert.ToDateTime(fu.GetDia31DelMes(hasta.Substring(0, 2)) + "/" + hasta);
      }

      if (desde != "  /" && hasta == "  /")
      {
        IntervaloDeFechas.Desde = Convert.ToDateTime("01" + "/" + desde);
        IntervaloDeFechas.Hasta = Convert.ToDateTime("01/01/3000");// new DateTime(01/01/2000);
      }

      if (desde != "  /" && hasta != "  /")
      {
        //string fecha_2 = fu.GetDia31DelMes(hasta.Substring(0, 2));
        IntervaloDeFechas.Desde = Convert.ToDateTime("01/" + desde);
        IntervaloDeFechas.Hasta = Convert.ToDateTime(fu.GetDia31DelMes(hasta.Substring(0, 2)) + "/" + hasta);// new DateTime(01/01/2000);
      }
      return IntervaloDeFechas;
    }
    public decimal ObtenerImporteDeInteres(DateTime FechaActa, decimal TotalDeuda, decimal interes_)
    {
      Func_Utiles fu = new Func_Utiles();
      DateTime FechaDesde = FechaActa;
      string dia = fu.GetDia31DelMes(fu.generar_ceros(FechaDesde.Month.ToString(), 2));
      FechaDesde = Convert.ToDateTime(dia + "/" + fu.generar_ceros(FechaDesde.Month.ToString(), 2) + "/" + FechaDesde.Year.ToString());
      double dias = Convert.ToInt32((DateTime.Today.Date - FechaDesde.Date).TotalDays);
      decimal meses = Convert.ToDecimal(dias / 30);
      decimal interes = Convert.ToDecimal(meses * interes_);
      decimal ImporteDelInteres = Convert.ToDecimal((TotalDeuda * (interes / 100)));
      return ImporteDelInteres > 0 ? ImporteDelInteres : 0;
    }

    public decimal ObtenerDeudaTotalConInteres(DateTime FechaActa, decimal TotalDeuda, decimal interes_, decimal Diferencia)
    {
      return (Diferencia * -1) + ObtenerImporteDeInteres(FechaActa, TotalDeuda, interes_);
    }

    public string ObtenerNombreCobrador(int nroActa)
    {
      using (var context = new lts_sindicatoDataContext())
      {
        var cobradorId = context.AsignarCobranza.Where(x => x.Acta == nroActa).Select(x => x.CobradorID).SingleOrDefault();
        var cobrador = context.Cobradores.Where(x => x.ID == cobradorId).Select(x => new { nombre = x.Apellido + " " + x.Nombre }).SingleOrDefault();
        if (cobrador == null)
        {
          return "Sin Asignar";
        }
        else
        {
          return cobrador.nombre.ToString();
        }
      }
    }

    public IEnumerable<AsignarCobranza> GetAsignacionDeCobranzas()
    {
      using (lts_sindicatoDataContext context = new lts_sindicatoDataContext())
      {
        return context.AsignarCobranza.ToList();
      }
    }

    public IEnumerable<Cobradores> GetCobradores()
    {
      using (lts_sindicatoDataContext context = new lts_sindicatoDataContext())
      {
        return context.Cobradores.ToList(); //IEnumerable<Cobradores> 
      }
    }

    public int GetNroDeAsignacion(int CobradorId)
    {
      using (var context = new lts_sindicatoDataContext())
      {

        Asignaciones insert = new Asignaciones();
        int num = 0;
        if (context.Asignaciones.Max(x => x.Numero) > 0)
        {
          insert.Numero = context.Asignaciones.Max(x => x.Numero) + 1;
        }
        else
        {
          insert.Numero = 1;
        }
        num = Convert.ToInt32(insert.Numero);
        insert.Fecha = DateTime.Now;
        insert.CobradorId = CobradorId;

        context.Asignaciones.InsertOnSubmit(insert);
        context.SubmitChanges();

        return num;//Convert.ToInt32(insert.Numero);

      }
    }

    public void AsignarNumeroDeAsignacion(int CobradorId, int NroDeAsignacion)
    {
      using (var context = new lts_sindicatoDataContext())
      {

        var NoAsignadas = context.AsignarCobranza.Where(x => x.NroAsignacion == 0);
        if (NoAsignadas.Count() > 0)
        {
          //int NumeroDeAsignacion = GetNroDeAsignacion(CobradorId);
          foreach (var Item in NoAsignadas.ToList())
          {
            Item.NroAsignacion = NroDeAsignacion;//NumeroDeAsignacion;
            context.SubmitChanges();
          }
        }
      }
    }

    public double ObtenerValorDeCuota(double Deuda, double TazaDeInteres, int Cuotas)
    {
      double ValorDeCuota = Cuotas == 1 ? Deuda : (Deuda * TazaDeInteres) / 1 - (Math.Pow(1 + TazaDeInteres, -Cuotas));
      return ValorDeCuota;
    }

    public double ObtenerIntereses(double Capital, double TazaDeInteres)
    {
      return Capital * TazaDeInteres;
    }

    public IEnumerable<ClsCuadroDeAmortizacion> ObtenerCuadroDeAmortizacion(double CapitalInicial, double TazaDeInteres, int CantidadDeCuotas, double ImporteDeCuota, double Anticipo, DateTime VencDeEntrega, DateTime VencDeCuota)
    {
      List<ClsCuadroDeAmortizacion> CuadroDeAmortizacion = new List<ClsCuadroDeAmortizacion>();

      //double ImporteDeCuota = ObtenerValorDeCuota(CaptialInicial, TazaDeInteres, CantidadDeCuotas);
      if (CantidadDeCuotas == 1)
      {
        //double Deuda = CaptialInicial;
        ClsCuadroDeAmortizacion Insert__ = new ClsCuadroDeAmortizacion();
        Insert__.Cuota = CantidadDeCuotas.ToString();
        Insert__.ImporteDeCuota = CapitalInicial;
        Insert__.Interes = 0;
        Insert__.Amortizado = 0;
        Insert__.AAmortizar = 0;
        Insert__.FechaDeVenc = VencDeCuota.Date;
        CuadroDeAmortizacion.Add(Insert__);
      }
      else
      {
        double Deuda = CapitalInicial;

        ClsCuadroDeAmortizacion Insert_ = new ClsCuadroDeAmortizacion();
        VencDeCuota = VencDeCuota.AddMonths(-1);
        Insert_.Cuota = "Anticipo";
        Insert_.ImporteDeCuota = Anticipo;
        Insert_.Interes = 0;
        Insert_.Amortizado = 0;
        Insert_.AAmortizar = 0;
        Insert_.FechaDeVenc = VencDeEntrega.Date;
        CuadroDeAmortizacion.Add(Insert_);

        for (int cuota = 0; cuota < CantidadDeCuotas; cuota++)
        {
          ClsCuadroDeAmortizacion Insert = new ClsCuadroDeAmortizacion();
          Insert.Cuota = (cuota + 1).ToString();
          Insert.ImporteDeCuota = Math.Round(ImporteDeCuota, 2);
          Insert.Interes = Math.Round(Deuda * TazaDeInteres, 2);
          Insert.Amortizado = Math.Round(ImporteDeCuota - Insert.Interes, 2);
          Insert.AAmortizar = Math.Round(Deuda - Insert.Amortizado, 2);
          VencDeCuota = ControlarDiaHabil(VencDeCuota.Date.AddMonths(1));
          Insert.FechaDeVenc = VencDeCuota;
          Deuda -= Insert.Amortizado;
          CuadroDeAmortizacion.Add(Insert);
        }
      }
      return CuadroDeAmortizacion;
    }
    public IEnumerable<ClsCuadroDeAmortizacion> LimpiarDgvPlanDePagos()
    {
      List<ClsCuadroDeAmortizacion> CuadroDeAmortizacion = new List<ClsCuadroDeAmortizacion>();
      return CuadroDeAmortizacion;
    }
    public DateTime ControlarDiaHabil(DateTime fecha)
    {
      DateTime fecha_ = fecha;
      if (fecha.DayOfWeek.ToString() == "Sunday")
      {
        fecha_ = fecha_.AddDays(1);
      }
      if (fecha.DayOfWeek.ToString() == "Saturday")
      {
        fecha_ = fecha_.AddDays(2);
      }
      return fecha_;
    }
    public void InsertarNovedad(string cuit, int NroDeAsignacion, string Novedad, int MensajeAlCobrador, int UserId)
    {
      using (var context = new lts_sindicatoDataContext())
      {
        Novedades Insert = new Novedades();
        Insert.Acta = 1;
        Insert.Fecha = DateTime.Now;
        Insert.Usuario = UserId;
        Insert.Novedad = Novedad;
        Insert.CUIT = cuit;
        Insert.NumeroDeAsignacion = NroDeAsignacion;
        if (MensajeAlCobrador == 1)
        {
          Insert.MensajeAlCobrador = 1; // 1= si tiene Mensaje
          Insert.EstadoMensajeAlCobrador = 0; // 0 = no esta leido
        }
        context.Novedades.InsertOnSubmit(Insert);
        context.SubmitChanges();

      }
    }
    public void ModificarNovedad(string cuit, int NroDeAsignacionViejo, int NroDeAsignacionNuevo, int UserId)
    {
      using (var context = new lts_sindicatoDataContext())
      {
        var novedades = context.Novedades.Where(x => x.CUIT == cuit && x.NumeroDeAsignacion == NroDeAsignacionViejo);

        foreach (var item in novedades)
        {
          item.NumeroDeAsignacion = NroDeAsignacionNuevo;
          context.SubmitChanges();
        }
      }
    }
    public void ModificarAsignacionDeCobranza(string cuit, int NroDeAsignacionViejo, int NroDeAsignacionNuevo, int CobradorId)
    {
      using (var context = new lts_sindicatoDataContext())
      {
        var cambio = context.AsignarCobranza.Where(x => x.CUIT == cuit && x.NroAsignacion == NroDeAsignacionViejo);

        foreach (var item in cambio)
        {
          item.NroAsignacion = NroDeAsignacionNuevo;
          item.CobradorID = CobradorId;
          context.SubmitChanges();
        }
      }
    }
    public void AsignarNroDeAsignacionALaNovedad(int NroDeAsignacion)
    {
      using (var context = new lts_sindicatoDataContext())
      {
        var NovedadesParaAsignar = context.Novedades.Where(x => x.NumeroDeAsignacion == 0);
        if (NovedadesParaAsignar.Count() > 0)
        {
          //int NumeroDeAsignacion = GetNroDeAsignacion(CobradorId);
          foreach (var Item in NovedadesParaAsignar.ToList())
          {
            Item.NumeroDeAsignacion = NroDeAsignacion;//NumeroDeAsignacion;
            context.SubmitChanges();
          }
        }
      }
    }

  }
}








