using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using entrega_cupones.Clases;

namespace entrega_cupones.Formularios
{
  public partial class frm_cobranzas : Form
  {
    public string _desde;
    public string _hasta;
    public int UserId;

    public DataTable DtResumenInspectores;


    public frm_cobranzas()
    {
      InitializeComponent();
      this.tttxt_Mensaje.SetToolTip(this.btn_VerAsignadas, "Ver Asignadas");
      this.tttxt_Mensaje.SetToolTip(this.btn_EliminarAsignacion, "Eliminar Asignacion");
      this.tttxt_Mensaje.SetToolTip(this.btn_ModificarAsignacion, "Modificar Asignacion");
      this.tttxt_Mensaje.SetToolTip(this.msk_Desde, "Ingrese el periodo desde - mes / año");
      this.tttxt_Mensaje.SetToolTip(this.msk_Hasta, "Ingrese el periodo hasta - mes / año");

    }
    private void frm_cobranzas_Load(object sender, EventArgs e)
    {
      dgv_Emitidas.AutoGenerateColumns = false;
      dgv_Cobradas.AutoGenerateColumns = false;
      dgv_ConDeuda.AutoGenerateColumns = false;
      dgv_NoCobradas.AutoGenerateColumns = false;
      dgv_CobroParcial.AutoGenerateColumns = false;
      dgv_Diferencia.AutoGenerateColumns = false;

      dgv_Resumen.Rows.Add(4);

      dgv_Resumen.Rows[0].Cells["Titulo"].Value = "Cantidad:";
      dgv_Resumen.Rows[1].Cells["Titulo"].Value = "Importe:";
      dgv_Resumen.Rows[2].Cells["Titulo"].Value = "Interes:";
      dgv_Resumen.Rows[3].Cells["Titulo"].Value = "Total:";

      CargarCbx_Inspectores();
      CargarCbx_Cobradores();
      resumen();
    }
    private void CargarCbx_Inspectores()
    {
      Inspector inspecto = new Inspector();
      cbx_Inspector.DisplayMember = "APELLIDO";
      cbx_Inspector.ValueMember = "ID_INSPECTOR";
      cbx_Inspector.DataSource = inspecto.Get_Inspectores();
    }
    private void CargarCbx_Localidades()
    {
      Localidad loc = new Localidad();

      cbx_Localidad.DisplayMember = "nombre";
      cbx_Localidad.ValueMember = "codigopostal";
      cbx_Localidad.DataSource = loc.GetLocalidades();

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

        cbx_cobrador2.DisplayMember = "nombre";
        cbx_cobrador2.ValueMember = "idUsuario";
        cbx_cobrador2.DataSource = cbr.ToList();

      }
    }
    private void resumen()
    {
      chk_ChekTodosCobrosParcial.Checked = false;
      chk_ChekTodosConDeuda.Checked = false;
      chk_SeleccionarTodas.Checked = false;

      ValidarFechas();

      if (_desde != string.Empty & _hasta != string.Empty)
      {
        cobranzas cbr = new cobranzas();
        cobranzas cbr2 = new cobranzas();

        var ActasEmitidas2 = cbr.get_resumen2(_desde, _hasta, cbx_Inspector.Text.Trim(), 0);

        var TodasLasActas = cbr2.get_resumen2(_desde, _hasta, "TODOS", 1);

        var ActasEmitidas = (chk_ActasRelacionadas.Checked ?
              (from a in TodasLasActas
               join ae in ActasEmitidas2
               on a.CUIT equals ae.CUIT
               select a)
              : (ActasEmitidas2))
              .ToList().Distinct().OrderBy(x => x.EMPRESA);

        var Emitidas = ActasEmitidas.Where(x => x.ACTA > 0 && x.DEUDATOTAL > 0).ToList();

        var Cobradas = ActasEmitidas.Where(x => x.COBRADOTOTALMENTE == "SI" & x.DIFERENCIA == 0).ToList();

        var NoCobradas = ActasEmitidas.Where(x => x.IMPORTECOBRADO == 0 && x.DEUDATOTAL > 0).ToList();

        var AsigCbr = (from a in cbr.GetAsignacionDeCobranzas()
                       join Cobra in cbr.GetCobradores() on a.CobradorID equals Cobra.ID
                       select new { a.Acta, nombre = Cobra.Apellido + " " + Cobra.Nombre, }).ToList();


        var ConDeuda = ActasEmitidas.Where(x => x.DIFERENCIA != 0).ToList();//ActasEmitidas.Where(x => x.DIFERENCIA < 0).ToList();


        var CobroParcial = ActasEmitidas.Where(x => x.IMPORTECOBRADO > 0 && x.DIFERENCIA != 0).ToList(); //ActasEmitidas.Where(x => x.IMPORTECOBRADO > 0 && x.DIFERENCIA < 0).ToList();

        dgv_Resumen.Rows[0].Cells["Emitidas"].Value = Emitidas.Count().ToString();
        dgv_Resumen.Rows[1].Cells["Emitidas"].Value = Math.Round(Convert.ToDecimal(Emitidas.Sum(x => x.DEUDATOTAL)), 2).ToString("N2");

        dgv_Resumen.Rows[0].Cells["Cobradas"].Value = Cobradas.Count().ToString(); ;
        dgv_Resumen.Rows[1].Cells["Cobradas"].Value = Math.Round(Convert.ToDecimal(Cobradas.Sum(x => x.DEUDATOTAL)), 2).ToString("N2"); ;

        dgv_Resumen.Rows[0].Cells["NoCobradas"].Value = NoCobradas.Count().ToString();
        dgv_Resumen.Rows[1].Cells["NoCobradas"].Value = Math.Round(Convert.ToDecimal(NoCobradas.Sum(x => x.DEUDATOTAL)), 2).ToString("N2");
        dgv_Resumen.Rows[2].Cells["NoCobradas"].Value = Math.Round(Convert.ToDecimal(NoCobradas.Sum(x => x.ImporteInteresActualizado)), 2).ToString("N2");
        dgv_Resumen.Rows[3].Cells["NoCobradas"].Value = (Convert.ToDecimal(dgv_Resumen.Rows[1].Cells["NoCobradas"].Value) + Convert.ToDecimal(dgv_Resumen.Rows[2].Cells["NoCobradas"].Value)).ToString("N2");

        dgv_Resumen.Rows[0].Cells["ConDeuda"].Value = ConDeuda.Count().ToString();
        dgv_Resumen.Rows[1].Cells["ConDeuda"].Value = Math.Round(Convert.ToDecimal(ConDeuda.Sum(x => x.DEUDATOTAL)), 2).ToString("N2");
        dgv_Resumen.Rows[2].Cells["ConDeuda"].Value = Math.Round(Convert.ToDecimal(ConDeuda.Sum(x => x.ImporteInteresActualizado)), 2).ToString("N2");
        dgv_Resumen.Rows[3].Cells["ConDeuda"].Value = (Convert.ToDecimal(dgv_Resumen.Rows[1].Cells["ConDeuda"].Value) + Convert.ToDecimal(dgv_Resumen.Rows[2].Cells["ConDeuda"].Value)).ToString("N2");

        dgv_Resumen.Rows[0].Cells["CobroParcial"].Value = CobroParcial.Count().ToString();
        dgv_Resumen.Rows[1].Cells["CobroParcial"].Value = Math.Round(Convert.ToDecimal(CobroParcial.Sum(x => x.IMPORTECOBRADO)), 2).ToString("N2");

        dgv_Resumen.Rows[1].Cells["FaltaCobrar"].Value = Math.Round(Convert.ToDecimal(CobroParcial.Sum(x => x.DIFERENCIA * -1)), 2).ToString("N2"); ;
        dgv_Resumen.Rows[2].Cells["FaltaCobrar"].Value = Math.Round(Convert.ToDecimal(CobroParcial.Sum(x => x.ImporteInteresActualizado)), 2).ToString("N2"); ;
        dgv_Resumen.Rows[3].Cells["FaltaCobrar"].Value = (Convert.ToDecimal(dgv_Resumen.Rows[1].Cells["FaltaCobrar"].Value) + Convert.ToDecimal(dgv_Resumen.Rows[2].Cells["FaltaCobrar"].Value)).ToString("N2");


        dgv_Emitidas.DataSource = Emitidas;//ActasEmitidas.Where(x => x.ACTA > 0 && x.DEUDATOTAL > 0).ToList();//cbr.resumen.AE_;
        PintarAsignados(dgv_Emitidas);
        dgv_Emitidas.Refresh();

        dgv_Cobradas.DataSource = Cobradas;//ActasEmitidas.Where(x => x.COBRADOTOTALMENTE == "SI" & x.DIFERENCIA == 0).ToList();//cbr.resumen.CT_;
        PintarAsignados(dgv_Cobradas);

        dgv_NoCobradas.DataSource = NoCobradas;// ActasEmitidas.Where(x => x.IMPORTECOBRADO == 0 && x.DEUDATOTAL > 0).ToList();//(x => x.COBRADOTOTALMENTE != "SI").ToList();//cbr.resumen.NC_;
        PintarAsignados(dgv_NoCobradas);
        dgv_NoCobradas.Refresh();

        dgv_ConDeuda.DataSource = ConDeuda;// ActasEmitidas.Where(x => x.DIFERENCIA != 0).ToList();//cbr.resumen.CD_;
        PintarAsignados(dgv_ConDeuda);
        dgv_ConDeuda.Refresh();

        dgv_CobroParcial.DataSource = CobroParcial;// ActasEmitidas.Where(x => x.IMPORTECOBRADO > 0 && x.DIFERENCIA != 0).ToList();//cbr.resumen.CP_;
        PintarAsignados(dgv_CobroParcial);
        dgv_CobroParcial.Refresh();

        var PorInspector = ActasEmitidas.GroupBy(x => x.INSPECTOR).ToList();

        DS_cupones dscpn = new DS_cupones();
        DtResumenInspectores = dscpn.ResumenInspectores;
        DtResumenInspectores.Clear();

        foreach (var item in PorInspector)
        {
          DataRow row = DtResumenInspectores.NewRow();
          row["Inspector"] = item.Key;

          row["Emitidas"] = Emitidas.Where(x => x.INSPECTOR == item.Key).Count();
          row["EmitidasImporte"] = Math.Round(Convert.ToDecimal(Emitidas.Where(x => x.INSPECTOR == item.Key).Sum(x => x.DEUDATOTAL)), 2).ToString("N2");

          row["Cobradas"] = Cobradas.Where(x => x.INSPECTOR == item.Key).Count();
          row["CobradasImporte"] = Math.Round(Convert.ToDecimal(Cobradas.Where(x => x.INSPECTOR == item.Key).Sum(x => x.DEUDATOTAL)), 2).ToString("N2"); ;

          row["ConDeuda"] = ConDeuda.Where(x => x.INSPECTOR == item.Key).Count();
          row["ConDeudaImporte"] = Math.Round(Convert.ToDecimal(ConDeuda.Where(x => x.INSPECTOR == item.Key).Sum(x => x.DEUDATOTAL)), 2).ToString("N2");
          row["ConDeudaInteres"] = Math.Round(Convert.ToDecimal(ConDeuda.Where(x => x.INSPECTOR == item.Key).Sum(x => x.ImporteInteresActualizado)), 2).ToString("N2");
          row["ConDeudaTotal"] = Math.Round((Convert.ToDecimal(row["ConDeudaImporte"]) + Convert.ToDecimal(row["ConDeudaInteres"])), 2).ToString("N2");

          row["NoCobradas"] = NoCobradas.Where(x => x.INSPECTOR == item.Key).Count();
          row["NoCobradasImporte"] = Math.Round(Convert.ToDecimal(NoCobradas.Where(x => x.INSPECTOR == item.Key).Sum(x => x.DEUDATOTAL)), 2).ToString("N2");
          row["NoCobradasInteres"] = Math.Round(Convert.ToDecimal(NoCobradas.Where(x => x.INSPECTOR == item.Key).Sum(x => x.ImporteInteresActualizado)), 2).ToString("N2");
          row["NoCobradasTotal"] = Math.Round((Convert.ToDecimal(row["NoCobradasImporte"]) + Convert.ToDecimal(row["NoCobradasInteres"])), 2).ToString("N2");

          row["CobroParcial"] = CobroParcial.Where(x => x.INSPECTOR == item.Key).Count();
          row["CobroParcialImporte"] = Math.Round(Convert.ToDecimal(CobroParcial.Where(x => x.INSPECTOR == item.Key).Sum(x => x.IMPORTECOBRADO)), 2).ToString("N2");

          row["FaltaCobrar"] = "";//Math.Round(Convert.ToDecimal(CobroParcial.Where(x => x.INSPECTOR == item.Key).Sum(x => x.DIFERENCIA * -1)), 2).ToString("N2");
          row["FaltaCobrarImporte"] = Math.Round(Convert.ToDecimal(CobroParcial.Where(x => x.INSPECTOR == item.Key).Sum(x => x.DIFERENCIA * -1)), 2).ToString("N2");
          row["FaltaCobrarInteres"] = Math.Round(Convert.ToDecimal(CobroParcial.Where(x => x.INSPECTOR == item.Key).Sum(x => x.ImporteInteresActualizado)), 2).ToString("N2");
          row["FaltaCobrarTotal"] = Math.Round((Convert.ToDecimal(row["FaltaCobrarImporte"]) + Convert.ToDecimal(row["FaltaCobrarInteres"])), 2).ToString("N2");

          DtResumenInspectores.Rows.Add(row);
        }

      }
    }

    private void PintarAsignados(DataGridView dgv)
    {
      DataGridViewCellStyle style = new DataGridViewCellStyle();
      style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
      int FinDeCadena = dgv.Name.Length - 4;
      string NombreCelda = "Cobrador" + dgv.Name.ToString().Substring(4, FinDeCadena);

      foreach (DataGridViewRow fila in dgv.Rows)
      {

        if (fila.Cells[NombreCelda].Value.ToString() != "0")
        {
          // Para chequear las que ya estan asignadas y no se las va a desbloquear o desquequear
          if (fila.Cells[0].GetType() == typeof(DataGridViewCheckBoxCell))
          {
            fila.Cells[0].Value = true;
            fila.ReadOnly = true;
          }
          foreach (DataGridViewCell columna in fila.Cells)
          {
            columna.Style.BackColor = Color.FromArgb(119, 221, 119);
          }
        }
      }
    }

    private void btn_Actualizar_Click(object sender, EventArgs e)
    {
      resumen(); //ValidarFechas();
    }

    private void ValidarFechas()
    {
      if (msk_Desde.MaskFull == true)
      {
        _desde = msk_Desde.Text;
      }
      else
      {
        if (msk_Desde.MaskCompleted == true)
        {
          //MessageBox.Show("fecha DESDE esta Totalmente Completa");
          _desde = msk_Desde.Text;
        }
        else
        {
          if (msk_Desde.Text == "  /")
          {
            _desde = msk_Desde.Text;
          }
          else
          {
            MessageBox.Show("fecha DESDE esta incompleta");
            return;
          }
        }
      }

      if (msk_Hasta.MaskFull)
      {
        _hasta = msk_Hasta.Text;
      }
      else
      {
        if (msk_Hasta.MaskCompleted)
        {
          _hasta = msk_Hasta.Text;
        }
        else
        {
          if (msk_Hasta.Text == "  /")
          {
            _hasta = msk_Hasta.Text;
          }
          else
          {
            MessageBox.Show("fecha HASTA esta incompleta");
            return;
          }
        }
      }
    }

    private void Btn_Actualizar_Click_1(object sender, EventArgs e)
    {
      resumen();
    }
    private void btn_minimizar_Click(object sender, EventArgs e)
    {
      this.WindowState = FormWindowState.Minimized;
    }
    private void btn_cerrar_Click(object sender, EventArgs e)
    {
      Close();
    }
    private void cbx_Inspector_SelectedIndexChanged(object sender, EventArgs e)
    {
      resumen();
    }
    private void btn_Imprimir_Click(object sender, EventArgs e)
    {
      string tab = string.Empty;

      tab = tab_Cobranzas.SelectedTab.Text;

      switch (tab) // el para metro 0 y 1 es por la columna del check box de los dgv
      {
        case "Emitidas":
          Imprimir(dgv_Emitidas, 0);
          break;

        case "Cobradas":
          Imprimir(dgv_Cobradas, 0);
          break;

        case "Con Deuda":
          Imprimir(dgv_ConDeuda, 0);
          break;

        case "NO Cobradas":
          Imprimir(dgv_NoCobradas, 1);
          break;

        case "Cobro Parcial":
          Imprimir(dgv_CobroParcial, 1);
          break;
      }
    }
    private void Imprimir(DataGridView dgv, int col)
    {
      try
      {
        using (var context = new lts_sindicatoDataContext())
        {
          DS_cupones dscpn = new DS_cupones();
          DataTable dt = dscpn.Cobranzas;
          dt.Clear();

          foreach (DataGridViewRow fila in dgv.Rows)
          {
            DataRow row = dt.NewRow();

            row["Fecha"] = fila.Cells[0 + col].Value;
            row["Acta"] = fila.Cells[1 + col].Value;
            row["Empresa"] = fila.Cells[2 + col].Value;
            row["DeudaOriginal"] = fila.Cells[10 + col].Value;
            row["ImporteCobrado"] = fila.Cells[12 + col].Value;
            row["FaltaCobrar"] = fila.Cells[13 + col].Value;
            row["Interes"] = fila.Cells[14 + col].Value;
            row["DeudaActualizada"] = fila.Cells[15 + col].Value;
            row["Inspector"] = fila.Cells[16 + col].Value;

            dt.Rows.Add(row);
          }
          reportes frm_reportes = new reportes();
          frm_reportes.nombreReporte = "rpt_CobranzasEmpresasConDeuda";
          frm_reportes.dtCobranzas = dt;
          frm_reportes.Parametro1 = " Listado de Actas " + tab_Cobranzas.SelectedTab.Text.ToUpper();
          frm_reportes.Parametro2 = msk_Desde.Text;
          frm_reportes.Parametro3 = msk_Hasta.Text;
          frm_reportes.Parametro4 = cbx_Inspector.Text.Trim();
          frm_reportes.Parametro5 = txt_Interes.Text.Trim();
          frm_reportes.Show();
        }
      }
      catch (Exception)
      {
        throw;
      }
    }
    private void ActualizarDeuda(DataGridView Dgv)
    {
      cobranzas cbr = new cobranzas();

      foreach (DataGridViewRow fila in Dgv.Rows)
      {
        if (Convert.ToDecimal(fila.Cells["Diferencia"].Value) < 0)
        {
          DateTime FechaVencimientoDeActa = Convert.ToDateTime(fila.Cells["FECHA"].Value);

          //decimal ImporteDelActa = Convert.ToDecimal(fila.Cells["DeudaTotal"].Value);
          decimal ImporteDelActa = Convert.ToDecimal(fila.Cells["Diferencia"].Value) * -1; //multiplico por -1 por que toma el campo de diferencia y siempre va a ser negativo si es que falta cobrar
          decimal ImporteDelInteres = cbr.ObtenerImporteDeInteres(FechaVencimientoDeActa, ImporteDelActa, Convert.ToDecimal(txt_Interes.Text));
          fila.Cells["IMPORTEINTERESACTUALIZADO"].Value = ImporteDelInteres;
          fila.Cells["ImporteDeudaActualizada"].Value = ImporteDelInteres + ImporteDelActa;
        }
      }
    }
    private void btn_Asignar_Click(object sender, EventArgs e)
    {
      string tab = string.Empty;

      tab = tab_Cobranzas.SelectedTab.Text;

      switch (tab)
      {
        //case "NO Cobradas":
        //  asignarCobranzas(dgv_NoCobradas, tab);
        //  break;

        //case "Cobro Parcial":
        //  asignarCobranzas(dgv_CobroParcial, tab);
        //  break;
        case "Con Deuda":
          asignarCobranzas(dgv_ConDeuda, tab);
          break;
      }
    }
    private void asignarCobranzas(DataGridView dgv, string tab)
    {
      if (MessageBox.Show("Esta seguro de asignar las Actas " + tab.ToUpper() + " desde el periodo " + msk_Desde.Text + " hasta " + msk_Hasta.Text + " al Cobrador " + cbx_Cobrador.Text, "¡¡¡ ATENCION !!!", MessageBoxButtons.YesNo) == DialogResult.Yes)
      {
        List<cobranzas.NoAsignadas> ActasNoAsignadas = new List<cobranzas.NoAsignadas>();
        bool AlgunInsertado = false;
        cobranzas.NoAsignadas insert = new cobranzas.NoAsignadas();

        foreach (DataGridViewRow fila in dgv.Rows)
        {
          //bool v = Convert.ToBoolean(fila.Cells[0].Value);
          if (Convert.ToBoolean(fila.Cells[0].Value))
          {
            using (var context = new lts_sindicatoDataContext())
            {
              int nroActa = Convert.ToInt32(fila.Cells[2].Value);

              if (VerificarQueNoEsteAsignado(nroActa) == false)
              {

                AsignarCobranza asignar = new AsignarCobranza();
                asignar.Acta = nroActa; //Convert.ToInt32(fila.Cells["dataGridViewTextBoxColumn36"].Value); // Acta
                asignar.CobradorID = Convert.ToInt32(cbx_Cobrador.SelectedValue); // 
                asignar.CUIT = fila.Cells[4].Value.ToString(); //cuit
                asignar.Estado = 0;
                asignar.Fecha = DateTime.Now;
                asignar.NroAsignacion = 0;

                context.AsignarCobranza.InsertOnSubmit(asignar);
                context.SubmitChanges();

                cobranzas cbr1 = new cobranzas();
                string novedad = "Asignacion para el Inicio de Gestion de Cobranzas";
                cbr1.InsertarNovedad(fila.Cells[4].Value.ToString(), 0, novedad, 0, UserId);
                AlgunInsertado = true;
              }
              else
              {
                insert.Acta = nroActa;
                insert.Empresa = fila.Cells[2].Value.ToString();
                //insert.CobradorAsignado = 
                ActasNoAsignadas.Add(insert);
              }
            }
          }
        }

        if (AlgunInsertado == true)
        {


          cobranzas cbr = new cobranzas();
          int NroDeAsignacion_ = cbr.GetNroDeAsignacion(Convert.ToInt32(cbx_Cobrador.SelectedValue));

          cbr.AsignarNumeroDeAsignacion(Convert.ToInt32(cbx_Cobrador.SelectedValue), NroDeAsignacion_);
          cbr.AsignarNroDeAsignacionALaNovedad(NroDeAsignacion_);
          ChequearParaCobranzas(dgv, false);
          MostrarAsignacionDeCobranzaPorInspector(Convert.ToInt32(cbx_cobrador2.SelectedValue));
        }
        else
        {
          MessageBox.Show("No Selecciono ninguna acta para Asignar al cobrador " + cbx_Cobrador.Text);
        }
      }
    }
    private bool VerificarQueNoEsteAsignado(int nroActa)
    {
      using (var context = new lts_sindicatoDataContext())
      {
        var asignadas = context.AsignarCobranza.Where(x => x.Acta == nroActa);
        if (asignadas.Count() > 0)
        {
          return true;
        }
        else
        {
          return false;
        }
      }
    }
    private void MostrarCobrador(DataGridView dgv)
    {
      int FinDeCadena = dgv.Name.Length - 4;
      string NombreCelda = "CobradorNombre" + dgv.Name.ToString().Substring(4, FinDeCadena);
      string NombreCelda2 = "NroAsignacion" + dgv.Name.ToString().Substring(4, FinDeCadena);
      if (dgv.CurrentRow.Cells[NombreCelda].Value == null)
      {
        lbl_Cobrador.Text = "Sin Asignar";
        lbl_NroDeCampaña.Text = "--";
      }
      else
      {
        lbl_Cobrador.Text = dgv.CurrentRow.Cells[NombreCelda].Value.ToString();
        lbl_NroDeCampaña.Text = dgv.CurrentRow.Cells[NombreCelda2].Value.ToString();
      }
      //lbl_Cobrador.Text = dgv.CurrentRow.Cells[NombreCelda].Value == null ? "": dgv.CurrentRow.Cells[NombreCelda].Value.ToString();


      //cobranzas cbr = new cobranzas();

      //int NroActa = Convert.ToInt32(dgv.CurrentRow.Cells[2].Value);
      //lbl_Cobrador.Text = cbr.ObtenerNombreCobrador(NroActa);
    }
    private void tab_Cobranzas_Selecting(object sender, TabControlCancelEventArgs e)
    {
      string tab = string.Empty;

      tab = tab_Cobranzas.SelectedTab.Text;

      switch (tab)
      {
        case "NO Cobradas":
          btn_Asignar.Enabled = true;
          //resumen();
          break;

        case "Cobro Parcial":
          btn_Asignar.Enabled = true;
          //resumen();
          break;

        case "Con Deuda":
          btn_Asignar.Enabled = true;
          //resumen();
          break;

        default:
          btn_Asignar.Enabled = false;
          //lbl_Cobrador.Text = "---";
          break;
      }
    }
    private void dgv_ConDeuda_SelectionChanged(object sender, EventArgs e)
    {
      if (dgv_ConDeuda.Focused)
      {
        MostrarCobrador(dgv_ConDeuda);
      }
    }
    private void dgv_CobroParcial_SelectionChanged(object sender, EventArgs e)
    {
      if (dgv_CobroParcial.Focused)
      {
        MostrarCobrador(dgv_CobroParcial);
      }

    }
    private void dgv_NoCobradas_SelectionChanged(object sender, EventArgs e)
    {
      if (dgv_NoCobradas.Focused)
      {
        MostrarCobrador(dgv_NoCobradas);
      }

    }
    private void dgv_Cobradas_SelectionChanged(object sender, EventArgs e)
    {
      MostrarCobrador(dgv_Cobradas);
    }
    private void dgv_Emitidas_SelectionChanged(object sender, EventArgs e)
    {
      if (dgv_Emitidas.Focused)
      {
        MostrarCobrador(dgv_Emitidas);
      }
    }
    private void chk_SeleccionarTodas_CheckedChanged(object sender, EventArgs e)
    {
      if (chk_SeleccionarTodas.Checked)
      {
        ChequearParaCobranzas(dgv_NoCobradas, true);
      }
      else
      {
        ChequearParaCobranzas(dgv_NoCobradas, false);
      }
    }
    private void ChequearParaCobranzas(DataGridView dgv, bool check)
    {
      foreach (DataGridViewRow fila in dgv.Rows)
      {
        if (check)
        {
          fila.Cells[0].Value = true;
        }
        else
        {
          fila.Cells[0].Value = false;
        }
      }
    }
    private void chk_ChekTodosCobrosParcial_CheckedChanged(object sender, EventArgs e)
    {
      if (chk_ChekTodosCobrosParcial.Checked)
      {
        ChequearParaCobranzas(dgv_CobroParcial, true);
      }
      else
      {
        ChequearParaCobranzas(dgv_CobroParcial, false);
      }
    }
    private void dgv_NoCobradas_CurrentCellDirtyStateChanged(object sender, EventArgs e)
    {
      // Esta seccion de codigo controla que los check box se actualizen.

      if (dgv_NoCobradas.IsCurrentCellDirty)
      {
        dgv_NoCobradas.CommitEdit(DataGridViewDataErrorContexts.Commit);
      }
    }
    private void dgv_ConDeuda_CurrentCellDirtyStateChanged(object sender, EventArgs e)
    {
      // Esta seccion de codigo controla que los check box se actualizen.

      if (dgv_ConDeuda.IsCurrentCellDirty)
      {
        dgv_ConDeuda.CommitEdit(DataGridViewDataErrorContexts.Commit);
      }
    }
    private void dgv_CobroParcial_CurrentCellDirtyStateChanged(object sender, EventArgs e)
    {
      // Esta seccion de codigo controla que los check box se actualizen.

      if (dgv_CobroParcial.IsCurrentCellDirty)
      {
        dgv_CobroParcial.CommitEdit(DataGridViewDataErrorContexts.Commit);
      }
    }
    private void cbx_cobrador2_SelectedIndexChanged(object sender, EventArgs e)
    {
      MostrarAsignacionDeCobranzaPorInspector(Convert.ToInt32(cbx_cobrador2.SelectedValue));
    }
    private void MostrarAsignacionDeCobranzaPorInspector(int CobradorId)
    {
      using (var context = new lts_sindicatoDataContext())
      {
        var CobranzaAsignada2 = context.Asignaciones
                                .Where(x => x.CobradorId == CobradorId)
                                .Select(x => new
                                {
                                  x.Fecha,
                                  numero = x.Numero,
                                  total = context.AsignarCobranza.Where(y => y.CobradorID == CobradorId && y.NroAsignacion == x.Numero).Count(),
                                  cobradas = context.AsignarCobranza.Where(y => y.CobradorID == CobradorId && y.NroAsignacion == x.Numero && y.Estado == 1).Count(),
                                  noCobradas = context.AsignarCobranza.Where(y => y.CobradorID == CobradorId && y.NroAsignacion == x.Numero && y.Estado == 0).Count()
                                });

        if (CobranzaAsignada2.Count() > 0)
        {
          dgv_CbrAsignadas.DataSource = CobranzaAsignada2.ToList();
          btn_VerAsignadas.Enabled = true;
        }
        else
        {
          dgv_CbrAsignadas.DataSource = CobranzaAsignada2;
          btn_VerAsignadas.Enabled = false;
        }

      }
    }
    private void btn_ImprimirResumen_Click(object sender, EventArgs e)
    {
      ImprimirResumen();
    }
    private void ImprimirResumen()
    {
      try
      {
        using (var context = new lts_sindicatoDataContext())
        {
          DS_cupones dscpn = new DS_cupones();
          DataTable dt = dscpn.ResumenActas;
          dt.Clear();

          foreach (DataGridViewRow fila in dgv_Resumen.Rows)
          {
            DataRow row = dt.NewRow();

            row["EncabezadoRow"] = fila.Cells[0].Value;
            row["Emitidas"] = fila.Cells[1].Value;
            row["Cobradas"] = fila.Cells[2].Value;
            row["ConDeuda"] = fila.Cells[3].Value;
            row["NoCobradas"] = fila.Cells[4].Value;
            row["CobroParcial"] = fila.Cells[5].Value;
            row["FaltaCobrar"] = fila.Cells[6].Value;
            row["EnAbogado"] = fila.Cells[7].Value;

            dt.Rows.Add(row);
          }

          reportes frm_reportes = new reportes();
          frm_reportes.DtResumenActas = dt;
          frm_reportes.DtResumenInspectores = DtResumenInspectores;
          frm_reportes.nombreReporte = "rpt_ResumenActas";
          frm_reportes.Parametro1 = " RESUMEN DE ACTAS ";
          frm_reportes.Parametro2 = msk_Desde.Text;
          frm_reportes.Parametro3 = msk_Hasta.Text;
          frm_reportes.Parametro4 = cbx_Inspector.Text.Trim();
          frm_reportes.Parametro5 = txt_Interes.Text.Trim();
          frm_reportes.Show();
        }
      }
      catch (Exception)
      {
        throw;
      }

    }
    private void btn_VerAsignadas_Click(object sender, EventArgs e)
    {
      frm_VerActasAsignadasParaCobrar f_VerActasAsignadasParaCobrar = new frm_VerActasAsignadasParaCobrar();
      f_VerActasAsignadasParaCobrar.CobradorId = Convert.ToInt32(cbx_cobrador2.SelectedValue);
      f_VerActasAsignadasParaCobrar.NroDeAsignacion = Convert.ToInt32(dgv_CbrAsignadas.CurrentRow.Cells["numero"].Value.ToString());
      f_VerActasAsignadasParaCobrar.CobradorNombre = cbx_cobrador2.Text;
      f_VerActasAsignadasParaCobrar.UserId = UserId;
      f_VerActasAsignadasParaCobrar.ShowDialog();
    }
    private void chk_ChekTodosConDeuda_CheckedChanged(object sender, EventArgs e)
    {
      if (chk_ChekTodosConDeuda.Checked)
      {
        ChequearParaCobranzas(dgv_ConDeuda, true);
      }
      else
      {
        ChequearParaCobranzas(dgv_ConDeuda, false);
      }
    }

    private void cbx_Cobrador_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void btnImprimirCobros_Click(object sender, EventArgs e)
    {
      ImprimirCobros();
    }

    private void ImprimirCobros()
    {
      using (var context = new lts_sindicatoDataContext())
      {
        //inspector == "TODOS" ? a.INSPECTOR != "TODOS" : a.INSPECTOR == inspector) &&

        string inspector = cbx_Inspector.Text.Trim();
        DateTime desde = Convert.ToDateTime("01/" + _desde);
        DateTime hasta = Convert.ToDateTime("01/" + _hasta);

        var cobros = from a in context.COBROS
                     where
                     (desde == hasta ? Convert.ToDateTime(a.FECHARECAUDACION).Date == Convert.ToDateTime(desde).Date : (a.FECHARECAUDACION >= desde && a.FECHARECAUDACION <= hasta))// &&
                     //(inspector == "TODOS" ? a.INSPECTOR != "TODOS" : a.INSPECTOR == inspector)
                     group a by a.INSPECTOR into agrupado
                     select new
                     {
                       Inspector = agrupado.Key,
                       Importe = agrupado.Sum(x => x.TOTAL),
                       //Cantidad = agrupado.Count(y=>y.Id)
                     };
      }
    }

    private void panel2_Paint(object sender, PaintEventArgs e)
    {

    }

    private void panel1_Paint(object sender, PaintEventArgs e)
    {

    }
  }
}
