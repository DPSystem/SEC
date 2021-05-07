using entrega_cupones.Clases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace entrega_cupones.Formularios
{
  public partial class frm_VerActasAsignadasParaCobrar : Form
  {
    public int CobradorId;
    public int NroDeAsignacion;
    public int NroDeAsignacionViejo;
    public double Anticipo;
    public string CobradorNombre;
    public int UserId;

    public class cls_ActasParaCobrar
    {
      public DateTime Fecha { get; set; }
      public int Acta { get; set; }
      public DateTime desde { get; set; }
      public DateTime Hasta { get; set; }
      public decimal DeudaOriginal { get; set; }
      public decimal FaltaCobrar { get; set; }
      public decimal ImporteCobrado { get; set; }
      public decimal Diferencia { get; set; }
    }

    public class actas_
    {
      public IEnumerable<cls_ActasParaCobrar> apc { get; set; }
    }

    public frm_VerActasAsignadasParaCobrar()
    {
      InitializeComponent();
    }

    private void frm_VerActasAsignadasParaCobrar_Load(object sender, EventArgs e)
    {
      this.Text = "Cobrador: " + CobradorNombre;
      dgv_ActasParaCobrar.AutoGenerateColumns = false;
      dgv_PlanDePagos.AutoGenerateColumns = false;
      dgv_DetallePlanDePago.AutoGenerateColumns = false;

      VerActasAsignadasParaCobrar();
      CargarCbx_Cobradores();
    }

    private void CargarCbx_Cobradores()
    {
      using (var context = new lts_sindicatoDataContext())
      {
        var cbr = context.Usuarios.Select(x => new { nombre = x.Apellido + " " + x.Nombre, x.idUsuario, x.DNI })
            .OrderBy(x => x.nombre).ToList();

        cbx_Cobrador.DisplayMember = "nombre";
        cbx_Cobrador.ValueMember = "idUsuario";
        cbx_Cobrador.DataSource = cbr.ToList();
      }
    }
    private void VerActasAsignadasParaCobrar()
    {
      using (var context = new lts_sindicatoDataContext())
      {
        var AgruparPorCuit = from a in context.AsignarCobranza
                             where a.CobradorID == CobradorId && a.NroAsignacion == NroDeAsignacion
                             group a by a.CUIT into g
                             select new
                             {
                               cuit = g.Key,
                             };

        var Asignadas = (from a in AgruparPorCuit
                         join b in context.maeemp on a.cuit equals b.MEEMP_CUIT_STR
                         select new
                         {
                           CUIT = a.cuit.Trim(),
                           RazonSocial = b.MAEEMP_RAZSOC.Trim(),
                           NombreFantasia = b.MAEEMP_NOMFAN.Trim(),
                           Domicilio = b.MAEEMP_CALLE.Trim() + " Nº" + b.MAEEMP_NRO.Trim() + " C:P.: " + b.MAEEMP_CODPOS.Trim(),
                           TelefonoEmpresa = b.MAEEMP_TEL.Trim(),
                           EmailEmpresa = b.MAEEMP_EMAIL.Trim(),
                           EstudioContable = b.MAEEMP_ESTUDIO_CONTACTO.Trim(),
                           TelefonoEstudioContable = b.MAEEMP_ESTUDIO_TEL.Trim(),
                           EmailEstudio = b.MAEEMP_ESTUDIO_EMAIL.Trim(),

                         }).OrderBy(x => x.RazonSocial).ToList();

        dgv_EmpresaAfectada.DataSource = Asignadas.ToList();
        //dgv_DetallePlanDePago.DataSource = dgv_EmpresaAfectada.CurrentRow.Cells["cuit"].Value.ToString();
      }
    }

    private void ObtenerActasParaCobrar(string cuit)
    {
      using (var context = new lts_sindicatoDataContext())
      {
        cobranzas cbr = new cobranzas();

        var ActasSinActualizar = (from a in context.AsignarCobranza
                                  where a.CobradorID == CobradorId && a.NroAsignacion == NroDeAsignacion && cuit == a.CUIT
                                  join b in context.ACTAS on a.Acta equals b.ACTA
                                  select new
                                  {
                                    Fecha = b.FECHA,
                                    Acta = b.ACTA,
                                    desde = b.DESDE,
                                    hasta = b.HASTA,
                                    DeudaOriginal = b.DEUDATOTAL,
                                    ImporteCobrado = b.IMPORTECOBRADO,
                                    FaltaCobrar = b.DIFERENCIA,
                                    Diferencia = b.DIFERENCIA,
                                    Inspector = b.INSPECTOR
                                  }).ToList();

        var ActasParaCobrar = (ActasSinActualizar.Select(x =>
        new
        {
          x.Fecha,
          x.Acta,
          x.desde,
          x.hasta,
          x.DeudaOriginal,
          x.ImporteCobrado,
          x.FaltaCobrar,
          x.Diferencia,
          x.Inspector,
          Interes = cbr.ObtenerImporteDeInteres(Convert.ToDateTime(x.Fecha),
                                                (x.Diferencia < 0) ? Convert.ToDecimal(x.Diferencia * -1) : 0,
                                                Convert.ToDecimal(txt_Interes.Text)),
          ImporteDeudaActualizada = cbr.ObtenerImporteDeInteres(Convert.ToDateTime(x.Fecha), (x.Diferencia < 0) ? Convert.ToDecimal(x.Diferencia * -1) : 0, Convert.ToDecimal(txt_Interes.Text)) + Convert.ToDecimal(x.Diferencia * -1)
        }));

        dgv_ActasParaCobrar.DataSource = ActasParaCobrar.ToList();
        txt_TotalDeudaInicial.Text = Math.Round(Convert.ToDecimal(ActasParaCobrar.Sum(x => x.DeudaOriginal)), 2).ToString("N2");
        txt_TotalIntereses.Text = Math.Round(ActasParaCobrar.Sum(x => x.Interes), 2).ToString("N2");
        txt_TotalDeuda.Text = Math.Round(ActasParaCobrar.Sum(x => x.ImporteDeudaActualizada), 2).ToString("N2");
        txt_DeudaInicial.Text = txt_TotalDeuda.Text;
        txt_Anticipo.Text = (Convert.ToDouble(txt_TotalDeuda.Text) * 0.10).ToString("N2");
        Anticipo = Convert.ToDouble(txt_Anticipo.Text);
        dtp_VencAnticipo.Value = DateTime.Today.AddDays(5);
        dtp_VencCuota.Value = DateTime.Today.AddMonths(1);

        txt_Deuda.Text = (Convert.ToDouble(txt_TotalDeuda.Text) - Convert.ToDouble(txt_Anticipo.Text)).ToString("N2");


      }
    }

    private void dgv_VerActasAsignadasParaCobrar_SelectionChanged(object sender, EventArgs e)
    {
      ObtenerActasParaCobrar(dgv_EmpresaAfectada.CurrentRow.Cells["cuit"].Value.ToString());
      MostrarNovedades(dgv_EmpresaAfectada.CurrentRow.Cells["cuit"].Value.ToString(), NroDeAsignacion);
      //txt_Anticipo.Text = "";
      txt_CantidadDeCuotas.Text = "1";
      txt_ImporteDeCuota.Text = "";
      cobranzas cbr = new cobranzas();
      dgv_PlanDePagos.DataSource = cbr.LimpiarDgvPlanDePagos().ToList();
      MostrarPagosDelPlan(dgv_EmpresaAfectada.CurrentRow.Cells["cuit"].Value.ToString());

    }

    private void btn_Actualizar_Click(object sender, EventArgs e)
    {
      ObtenerActasParaCobrar(dgv_EmpresaAfectada.CurrentRow.Cells["cuit"].Value.ToString());
    }

    private void btn_GuardarComentario_Click(object sender, EventArgs e)
    {
      if (txt_Novedades.Text.Trim() != "")
      {

        cobranzas cbr = new cobranzas();
        cbr.InsertarNovedad(dgv_EmpresaAfectada.CurrentRow.Cells["cuit"].Value.ToString(), NroDeAsignacion, txt_Novedades.Text.Trim(),0,UserId);
        MessageBox.Show("Novedad Cargada con exito. !!!", ">>>>> Atencion <<<<<<");
        MostrarNovedades(dgv_EmpresaAfectada.CurrentRow.Cells["cuit"].Value.ToString(), NroDeAsignacion);
        txt_Novedades.Text = "";

      }
      else
      {
        MessageBox.Show("Debe ingresar un comentario.", "Atencion !!!!");
      }
    }

    private void MostrarNovedades(string Cuit, int NumeroDeAsignacion)
    {
      using (var context = new lts_sindicatoDataContext())
      {
        var novedades = context.Novedades
                        .Where(x => x.CUIT == Cuit && x.NumeroDeAsignacion == NumeroDeAsignacion)
                        .Select(x => new
                        {
                          x.Fecha,
                          x.Novedad
                        })
                        .OrderBy(x => x.Fecha);

        dgv_Novedades.DataSource = novedades.ToList();
        this.dgv_Novedades.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
      }
    }

    private void MostrarPagosDeActa(int NroDeActa)
    {
      using (var context = new lts_sindicatoDataContext())
      {
        var MostrarPagosDeActa = context.COBROS.Where(x => x.ACTA == NroDeActa)
          .Select(x => new
          {
            Cuota = x.CUOTAS,
            F_Venc = x.FECHA_VENC,
            F_Cobro = x.FECHARECAUDACION,
            Recibo = x.RECIBO,
            Importe = x.IMPORTE,
            Interes = x.INTERES,
            Total = x.TOTAL
          });

        dgv_CobrosDeActa.DataSource = MostrarPagosDeActa.ToList();
      }
    }

    private void MostrarPagosDelPlan(string cuit)
    {
      using (var context = new lts_sindicatoDataContext())
      {
        var PlanDePago = context.PlanesDePago.Where(x => x.CUIT == cuit && x.Estado == 1);
        if (PlanDePago.Count() > 0)
        {
          calcular_coeficientes CIR = new calcular_coeficientes();
          var DetallePlanDePago = from a in context.PlanDetalle
                                  where a.NroPlanDePago == PlanDePago.Single().NroDePlan
                                  select new
                                  {
                                    a.Id,
                                    a.Cuota,
                                    //a.FechaDePago,
                                    a.FechaVenc,
                                    a.AAmortizar,
                                    a.Amortizado,
                                    a.ImporteCuota,
                                    a.Interes,
                                    a.NroPlanDePago,
                                    DiasDeMora = Convert.ToInt32((DateTime.Today.Date - Convert.ToDateTime(a.FechaVenc).Date).TotalDays),
                                    a.ImporteCobrado,
                                    //a.ReciboDeCobro,
                                    //InteresResarcitorio = CIR.CalcularInteresResarcitorio(Convert.ToDateTime(a.FechaVenc), Convert.ToDateTime(a.FechaDePago) == null ? DateTime.Now : Convert.ToDateTime(a.FechaDePago), Convert.ToDouble(a.ImporteCobrado), Convert.ToDouble(a.ImporteCuota), 1, a.Id),
                                    //total = CIR.ObtenerTotalDeCuotaDePlanDePago(Convert.ToDateTime(a.FechaVenc), Convert.ToDateTime(a.FechaDePago) == null ? DateTime.Now : Convert.ToDateTime(a.FechaDePago), Convert.ToDouble(a.ImporteCobrado), Convert.ToDouble(a.ImporteCuota), 1, a.Id)
                                  };
          dgv_DetallePlanDePago.DataSource = DetallePlanDePago.ToList();
        }
        else
        {
          dgv_DetallePlanDePago.DataSource = null;
        }
      }
    }

    private void btn_VerPlanDePago_Click(object sender, EventArgs e)
    {
      if (txt_CantidadDeCuotas.Text == "1")
      {
        txt_Anticipo.Text = "0";
        TraerPlanDePago();
      }

      if (txt_CantidadDeCuotas.Text != "1")
      {
        if (txt_CantidadDeCuotas.Text.Trim() == "")
        {
          MessageBox.Show("Debe Ingresar al menos una cuota.", "ATENCION !!!!!!");
          txt_CantidadDeCuotas.Focus();
        }
        else
        {
          //if (Convert.ToDouble(txt_Anticipo.Text) >= Anticipo)
          //{
          TraerPlanDePago();
          //}
          //else
          //{
          //  MessageBox.Show("El anticipo debe ser como minimo el 10% de la Deuda", "ATENCION !!!!!!");
          //  txt_Anticipo.Focus();
          //}
        }
      }
    }

    private void TraerPlanDePago()
    {
      cobranzas cbr = new cobranzas();
      //obtengo el importe de la cuota, si la cuota es 1 entonces el interes es 0% sino se aplica el 3% lo mismo para el cuadro de amortizacion
      txt_ImporteDeCuota.Text = cbr.ObtenerValorDeCuota(
        Convert.ToDouble(txt_Deuda.Text),
        txt_CantidadDeCuotas.Text == "1" ? 0 : 0.03,
        Convert.ToInt32(txt_CantidadDeCuotas.Text)
        ).ToString("N2");

      dgv_PlanDePagos.DataSource = cbr.ObtenerCuadroDeAmortizacion(
        Convert.ToDouble(txt_Deuda.Text),
        txt_CantidadDeCuotas.Text == "1" ? 0 : 0.03,
        Convert.ToInt32(txt_CantidadDeCuotas.Text),
        Convert.ToDouble(txt_ImporteDeCuota.Text),
        Convert.ToDouble(txt_Anticipo.Text),
        Convert.ToDateTime(dtp_VencAnticipo.Value),
        Convert.ToDateTime(dtp_VencCuota.Value)
        ).ToList();
    }

    private void txt_Anticipo_TextChanged(object sender, EventArgs e)
    {
      if (txt_Anticipo.Text != "")
      {

        txt_Deuda.Text = (Convert.ToDouble(txt_DeudaInicial.Text) - Convert.ToDouble(txt_Anticipo.Text)).ToString("N2");
      }
      else
      {
        txt_Anticipo.Text = "0";
      }
    }

    private void dgv_ActasParaCobrar_SelectionChanged(object sender, EventArgs e)
    {
      MostrarPagosDeActa(Convert.ToInt32(dgv_ActasParaCobrar.CurrentRow.Cells["Acta"].Value.ToString()));
      lbl_DetalleDePagoDeActa.Text = "Detalle de Pagos Acta Nº " + dgv_ActasParaCobrar.CurrentRow.Cells["Acta"].Value.ToString();
    }

    private void dgv_VerActasAsignadasParaCobrar_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {

    }

    private void txt_CantidadDeCuotas_TextChanged(object sender, EventArgs e)
    {
      if (txt_CantidadDeCuotas.Text == "0")
      {
        txt_CantidadDeCuotas.Text = "1";
      }
    }

    private void btn_AsentarPlan_Click(object sender, EventArgs e)
    {
      AsentarPlan();
    }

    private void AsentarPlan()
    {
      string cuit = dgv_EmpresaAfectada.CurrentRow.Cells["cuit"].Value.ToString();
      if (MessageBox.Show("Esta seguro de Asentar el Plan de Pago? ", "¡¡¡ ATENCION !!!", MessageBoxButtons.YesNo) == DialogResult.Yes)
      {

        if (!ExistePlanVigente(cuit, NroDeAsignacion))
        {
          using (var context = new lts_sindicatoDataContext())
          {
            var NroDePlan = context.PlanesDePago.ToList().Count() == 0 ? 1 : context.PlanesDePago.Max(x => x.NroDePlan) + 1;

            PlanesDePago Insert = new PlanesDePago();
            Insert.Fecha = DateTime.Now;
            Insert.CUIT = dgv_EmpresaAfectada.CurrentRow.Cells["cuit"].Value.ToString();
            Insert.NroDeAsignacion = NroDeAsignacion;
            Insert.NroDePlan = NroDePlan;
            Insert.Estado = 1;
            Insert.DeudaInicial = Convert.ToDecimal(txt_DeudaInicial.Text);
            context.PlanesDePago.InsertOnSubmit(Insert);
            context.SubmitChanges();

            CargarPlanDetalle(dgv_PlanDePagos, NroDePlan);

            Novedades InsertNovedad = new Novedades();
            InsertNovedad.Fecha = DateTime.Now;
            InsertNovedad.Usuario = UserId;
            InsertNovedad.NumeroDeAsignacion = NroDeAsignacion;
            InsertNovedad.CUIT = cuit;
            if (txt_CantidadDeCuotas.Text == "1")
            {
              InsertNovedad.Novedad = "Se Confirma Plan de Pago bajo las siguientes condiciones: " +
                 "Deuda Inical: $" + txt_DeudaInicial.Text + " - Compromiso de pago hasta el dia " + dtp_VencCuota.Value.Date.ToString("dd-MM-yyyy") +
                  " - el importe a abonar es de: $" + txt_DeudaInicial.Text + " - NOTA IMPORTANTE !!! ->>> En caso de NO ABONAR dentro de la fecha pactada" +
                  ", El plan CAERA hasta llegar a un nuevo acuerdo."
                 ;
            }
            else
            {
              InsertNovedad.Novedad = "Se Confirma Plan de Pago bajo las siguientes condiciones: " +
                "Deuda Inical: $" + txt_DeudaInicial.Text +
                " - Anticipo:  $" + txt_Anticipo.Text +
                " - Abonar Anticipo hasta el dia " + dtp_VencAnticipo.Value.Date +
                " - Una vez Abonado el Anticipo Toma Vigencia el PLan de Pago de " + txt_CantidadDeCuotas.Text +
                "  Cuotas de $" + txt_ImporteDeCuota.Text + " Con vencimiento de la 1ª cuota el dia  " + dtp_VencCuota.Value.Date.ToString("dd-MM-yyyy") +
                " - NOTA IMPORTANTE !!! ->>> En caso NO ABONAR el anticipo dentro de la fecha pactada, El plan CAERA hasta nuevo acuerdo."
                ;
            }
            context.Novedades.InsertOnSubmit(InsertNovedad);
            context.SubmitChanges();

            MostrarNovedades(cuit, NroDeAsignacion);
          }
        }
        else
        {
          MessageBox.Show("Ya esiste un plan vigente para esta Empresa !!!!!");
        }
      }
    }

    private void CargarPlanDetalle(DataGridView dgv, int NroDePlan)
    {
      foreach (DataGridViewRow fila in dgv.Rows)
      {
        using (var context = new lts_sindicatoDataContext())
        {
          PlanDetalle InsertPlanDetalle = new PlanDetalle();
          InsertPlanDetalle.NroPlanDePago = NroDePlan;
          InsertPlanDetalle.Cuota = fila.Cells["CuotaDelPlan"].Value.ToString() == "Anticipo" ? 0 : Convert.ToInt32(fila.Cells["CuotaDelPlan"].Value);
          InsertPlanDetalle.ImporteCuota = Convert.ToDecimal(fila.Cells["ImporteDeCuota"].Value);
          InsertPlanDetalle.Interes = Convert.ToDecimal(fila.Cells["InteresDeCuota"].Value);
          InsertPlanDetalle.Amortizado = Convert.ToDecimal(fila.Cells["Amortizado"].Value);
          InsertPlanDetalle.AAmortizar = Convert.ToDecimal(fila.Cells["AAmortizar"].Value);
          InsertPlanDetalle.FechaVenc = Convert.ToDateTime(fila.Cells["FechaDeVencimiento"].Value);
          InsertPlanDetalle.Estado = 1;
          InsertPlanDetalle.DiasDeMora = 0;
          InsertPlanDetalle.ImporteCobrado = 0;

          context.PlanDetalle.InsertOnSubmit(InsertPlanDetalle);
          context.SubmitChanges();
        }
      }
    }

    private bool ExistePlanVigente(string CUIT, int NroDeAsig)
    {
      using (var context = new lts_sindicatoDataContext())
      {
        return context.PlanesDePago.Where(x => x.CUIT == CUIT && x.NroDeAsignacion == NroDeAsig && (x.Estado == 2 || x.Estado == 1)).ToList().Count() > 0 ? true : false;
      }
    }

    private void btn_ImprimirPlanDePago_Click(object sender, EventArgs e)
    {
      ImprimirPlanDePago(dgv_DetallePlanDePago, dgv_EmpresaAfectada.CurrentRow.Cells["cuit"].Value.ToString());
    }

    private void ImprimirPlanDePago(DataGridView dgv, string cuit)
    {
      try
      {
        using (var context = new lts_sindicatoDataContext())
        {
          DS_cupones dsPlan = new DS_cupones();
          DataTable dt = dsPlan.ImpresionPlanDePago;
          dt.Clear();

          foreach (DataGridViewRow fila in dgv.Rows)
          {
            DataRow row = dt.NewRow();

            row["Cuota"] = fila.Cells[1].Value;
            row["ImporteDeCuota"] = fila.Cells[2].Value;
            row["FechaDeVencimiento"] = fila.Cells[3].Value;
            row["DiasDeVencimiento"] = fila.Cells[4].Value;
            row["Intereses"] = Math.Round(Convert.ToDecimal(fila.Cells[5].Value), 2);
            row["Total"] = fila.Cells[6].Value;
            row["FechaDeCobro"] = fila.Cells[7].Value;
            row["Recibo"] = fila.Cells[8].Value;
            row["ImporteCobrado"] = fila.Cells[9].Value;

            dt.Rows.Add(row);
          }
          reportes frm_reportes = new reportes();
          frm_reportes.nombreReporte = "rpt_PlanDePago";
          frm_reportes.DtPlanDePago = dt;
          frm_reportes.Parametro1 = "Plan de Pago";
          frm_reportes.Parametro2 = dgv_EmpresaAfectada.CurrentRow.Cells["Empresa"].Value.ToString();
          frm_reportes.Parametro3 = dgv_EmpresaAfectada.CurrentRow.Cells["CUIT"].Value.ToString();
          frm_reportes.Parametro4 = dgv_EmpresaAfectada.CurrentRow.Cells["Inspector"].Value.ToString();

          var DeudaInicial = context.PlanesDePago.Where(x => x.CUIT == dgv_EmpresaAfectada.CurrentRow.Cells["cuit"].Value.ToString() && x.Estado == 1);
          frm_reportes.Parametro5 = DeudaInicial.Count() > 0 ? DeudaInicial.Single().DeudaInicial.ToString() : "";

          frm_reportes.Parametro6 = Math.Round( dt.AsEnumerable().Sum(r => r.Field<double>("Total")),2).ToString();
          frm_reportes.Show();
        }
      }
      catch (Exception)
      {
        throw;
      }
    }

    private void ImprimirPlanDePago2(DataGridView dgv, string cuit)
    {
      try
      {
        using (var context = new lts_sindicatoDataContext())
        {
          DS_cupones dsPlan = new DS_cupones();
          DataTable dt = dsPlan.ImpresionPlanDePago;
          dt.Clear();

          foreach (DataGridViewRow fila in dgv.Rows)
          {
            DataRow row = dt.NewRow();

            row["Cuota"] = fila.Cells[0].Value;
            row["ImporteDeCuota"] = fila.Cells[1].Value;
            row["FechaDeVencimiento"] = fila.Cells[5].Value;
            //row["DiasDeVencimiento"] = fila.Cells[4].Value;
            //row["Intereses"] = Math.Round(Convert.ToDecimal(fila.Cells[5].Value), 2);
            //row["Total"] = fila.Cells[6].Value;
            //row["FechaDeCobro"] = fila.Cells[7].Value;
            //row["Recibo"] = fila.Cells[8].Value;
            //row["ImporteCobrado"] = fila.Cells[9].Value;

            dt.Rows.Add(row);
          }
          reportes frm_reportes = new reportes();
          frm_reportes.nombreReporte = "rpt_PlanDePago";
          frm_reportes.DtPlanDePago = dt;
          frm_reportes.Parametro1 = "Plan de Pago";
          frm_reportes.Parametro2 = dgv_EmpresaAfectada.CurrentRow.Cells["Empresa"].Value.ToString();
          frm_reportes.Parametro3 = dgv_EmpresaAfectada.CurrentRow.Cells["CUIT"].Value.ToString();
          frm_reportes.Parametro4 = dgv_EmpresaAfectada.CurrentRow.Cells["Inspector"].Value.ToString();
          frm_reportes.Parametro5 = txt_DeudaInicial.Text;
          frm_reportes.Parametro6 = Math.Round( dt.AsEnumerable().Sum(r => r.Field<double>("ImporteDeCuota")),2).ToString();
          frm_reportes.Show();
        }
      }
      catch (Exception)
      {
        throw;
      }
    }

    private void btn_ImprimirPlanDePago2_Click(object sender, EventArgs e)
    {
      ImprimirPlanDePago2(dgv_PlanDePagos, dgv_EmpresaAfectada.CurrentRow.Cells["cuit"].Value.ToString());
    }

    private void btn_MensajeAlCobrador_Click(object sender, EventArgs e)
    {
      if (txt_Novedades.Text.Trim() != "")
      {

        cobranzas cbr = new cobranzas();
        cbr.InsertarNovedad(dgv_EmpresaAfectada.CurrentRow.Cells["cuit"].Value.ToString(), NroDeAsignacion, txt_Novedades.Text.Trim(),1,UserId);
        MessageBox.Show("Novedad Cargada con exito. !!!", ">>>>> Atencion <<<<<<");
        MostrarNovedades(dgv_EmpresaAfectada.CurrentRow.Cells["cuit"].Value.ToString(), NroDeAsignacion);
        txt_Novedades.Text = "";

      }
      else
      {
        MessageBox.Show("Debe ingresar un comentario.", "Atencion !!!!");
      }
    }

    private void GuardarComentario()
    {
      if (txt_Novedades.Text.Trim() != "")
      {

        cobranzas cbr = new cobranzas();
        cbr.InsertarNovedad(dgv_EmpresaAfectada.CurrentRow.Cells["cuit"].Value.ToString(), NroDeAsignacion, txt_Novedades.Text.Trim(),1,UserId);
        MessageBox.Show("Novedad Cargada con exito. !!!", ">>>>> Atencion <<<<<<");
        MostrarNovedades(dgv_EmpresaAfectada.CurrentRow.Cells["cuit"].Value.ToString(), NroDeAsignacion);
        txt_Novedades.Text = "";

      }
      else
      {
        MessageBox.Show("Debe ingresar un comentario.", "Atencion !!!!");
      }
    }

    private void cbx_Cobrador_SelectedIndexChanged(object sender, EventArgs e)
    {
      
    }

    private void btn_CambiarCobrador_Click(object sender, EventArgs e)
    {

      if (MessageBox.Show("Esta seguro de Cambiar de Cobrador? ", "¡¡¡ ATENCION !!!", MessageBoxButtons.YesNo) == DialogResult.Yes)
      {
        NroDeAsignacionViejo = NroDeAsignacion;
        string cuit = dgv_EmpresaAfectada.CurrentRow.Cells["cuit"].Value.ToString();
        cobranzas cbr = new cobranzas();
        int NroDeAsignacion_ = cbr.GetNroDeAsignacion(Convert.ToInt32(cbx_Cobrador.SelectedValue));
        string Novedad = "Cambiado a Cobrador : " + cbx_Cobrador.Text;

        cbr.AsignarNumeroDeAsignacion(Convert.ToInt32(cbx_Cobrador.SelectedValue), NroDeAsignacion_);
        cbr.ModificarNovedad(cuit, NroDeAsignacionViejo, NroDeAsignacion_, UserId);
        cbr.ModificarAsignacionDeCobranza(cuit, NroDeAsignacionViejo, NroDeAsignacion_, Convert.ToInt32(cbx_Cobrador.SelectedValue));
        cbr.InsertarNovedad(cuit, NroDeAsignacion_, Novedad, 0, UserId);
        MostrarNovedades(cuit, NroDeAsignacion_);

      }
    }
  }
}
