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
using entrega_cupones.Formularios;

namespace entrega_cupones
{
  public partial class frm_actas : Form
  {
    lts_sindicatoDataContext db_sindicato = new lts_sindicatoDataContext();
    Func_Utiles func_utiles = new Func_Utiles();
    DS_cupones DScpn = new DS_cupones();

    #region codigo para efecto shadow

    private bool Drag;
    private int MouseX;
    private int MouseY;

    private const int WM_NCHITTEST = 0x84;
    private const int HTCLIENT = 0x1;
    private const int HTCAPTION = 0x2;

    private bool m_aeroEnabled;

    private const int CS_DROPSHADOW = 0x00020000;
    private const int WM_NCPAINT = 0x0085;
    private const int WM_ACTIVATEAPP = 0x001C;

    [System.Runtime.InteropServices.DllImport("dwmapi.dll")]
    public static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);
    [System.Runtime.InteropServices.DllImport("dwmapi.dll")]
    public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);
    [System.Runtime.InteropServices.DllImport("dwmapi.dll")]

    public static extern int DwmIsCompositionEnabled(ref int pfEnabled);
    [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
    private static extern IntPtr CreateRoundRectRgn(
        int nLeftRect,
        int nTopRect,
        int nRightRect,
        int nBottomRect,
        int nWidthEllipse,
        int nHeightEllipse
        );

    public struct MARGINS
    {
      public int leftWidth;
      public int rightWidth;
      public int topHeight;
      public int bottomHeight;
    }
    protected override CreateParams CreateParams
    {
      get
      {
        m_aeroEnabled = CheckAeroEnabled();
        CreateParams cp = base.CreateParams;
        if (!m_aeroEnabled)
          cp.ClassStyle |= CS_DROPSHADOW; return cp;
      }
    }
    private bool CheckAeroEnabled()
    {
      if (Environment.OSVersion.Version.Major >= 6)
      {
        int enabled = 0; DwmIsCompositionEnabled(ref enabled);
        return (enabled == 1) ? true : false;
      }
      return false;
    }
    protected override void WndProc(ref Message m)
    {
      switch (m.Msg)
      {
        case WM_NCPAINT:
          if (m_aeroEnabled)
          {
            var v = 2;
            DwmSetWindowAttribute(this.Handle, 2, ref v, 4);
            MARGINS margins = new MARGINS()
            {
              bottomHeight = 1,
              leftWidth = 0,
              rightWidth = 0,
              topHeight = 0
            }; DwmExtendFrameIntoClientArea(this.Handle, ref margins);
          }
          break;
        default: break;
      }
      base.WndProc(ref m);
      if (m.Msg == WM_NCHITTEST && (int)m.Result == HTCLIENT) m.Result = (IntPtr)HTCAPTION;
    }
    private void PanelMove_MouseDown(object sender, MouseEventArgs e)
    {
      Drag = true;
      MouseX = Cursor.Position.X - this.Left;
      MouseY = Cursor.Position.Y - this.Top;
    }
    private void PanelMove_MouseMove(object sender, MouseEventArgs e)
    {
      if (Drag)
      {
        this.Top = Cursor.Position.Y - MouseY;
        this.Left = Cursor.Position.X - MouseX;
      }
    }
    private void PanelMove_MouseUp(object sender, MouseEventArgs e) { Drag = false; }

    #endregion

    public frm_actas()
    {
      InitializeComponent();
    }

    private void frm_actas_Load(object sender, EventArgs e)
    {
      cargar_cbx_actas_inspectores();
      cargar_seguimiento();
      dgv_cobros.AutoGenerateColumns = false;
    }

    private void cargar_cbx_actas_inspectores()

    {
      var inspector = (from inspec in db_sindicato.inspectores select inspec).OrderBy(x => x.APELLIDO);

      cbx_actas_inspectores.DisplayMember = "apellido";
      cbx_actas_inspectores.ValueMember = "id_inspector";
      cbx_actas_inspectores.DataSource = inspector.ToList();

      cbx_inspector_seguimiento.DisplayMember = "apellido";
      cbx_inspector_seguimiento.ValueMember = "id_inspector";
      cbx_inspector_seguimiento.DataSource = inspector.ToList();
    }

    private void btn_cerrar_actas_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void txt_actas_buscar_empresa_Click(object sender, EventArgs e)
    {
      limpiar_dgv_actas_dj_empresas();
      frm_buscar_empresa f_busc_emp = new frm_buscar_empresa();
      f_busc_emp.DatosPasados += new frm_buscar_empresa.PasarDatos(ejecutar);
      f_busc_emp.viene_desde = 1;
      f_busc_emp.ShowDialog();
      // txt_actas_empresa.Text = empresa;
    }

    public void ejecutar(string empresa, string domicilio, string cuit, string estudio, string telefono, string localidad)
    {
      txt_actas_empresa.Text = empresa;
      txt_actas_domicilio.Text = domicilio;
      txt_actas_cuit.Text = cuit;
      txt_actas_estudio.Text = estudio;
      txt_actas_telefono.Text = telefono;
      txt_actas_localidad.Text = localidad;
      if (cuit != string.Empty || cuit != "")
      {
        mostrar_actas_involucradas();
      }
    }

    private void btn_verificar_periodo_Click(object sender, EventArgs e)
    {
      calcular_deuda();
    }

    private void limpiar_dgv_actas_dj_empresas()
    {
      while (dgv_actas_dj_empresa.Rows.Count != 0)
      {
        dgv_actas_dj_empresa.Rows.RemoveAt(0);
      }

    }

    private void calcular_deuda()
    {
      if (txt_actas_cuit.Text != "")
      {
        DateTime desde = Convert.ToDateTime("01/" + txt_actas_desde.Text);
        DateTime hasta = Convert.ToDateTime("01/" + txt_actas_hasta.Text);
        if (desde <= hasta)
        {
          // Vaciar dgv_actas_dj_empresas
          limpiar_dgv_actas_dj_empresas();

          //var sp = db_sindicato.sp_get_ddjjt(Convert.ToInt64(txt_actas_cuit.Text), desde, hasta).ToList();
          //var actualizar_empresa = db_sindicato.sp_get_deuda(desde, hasta, Convert.ToDateTime(txt_actas_venc.Text).Date, Convert.ToInt64(txt_actas_cuit.Text)).ToList();
          var dj_empresa = (from a in db_sindicato.ddjjt
                            where (a.periodo >= desde && a.periodo <= hasta) && (a.cuit == Convert.ToInt64(txt_actas_cuit.Text))
                            select new
                            {
                              periodo = a.periodo,
                              rectifi = a.rect,
                              aporteley = a.titem1,
                              aportesocio = a.titem2,
                              fpago = a.fpago,
                              importe_depositado = a.impban1 + a.impban2,
                              impban1 = a.impban1,
                              total = a.impban1,
                              capital = a.titem1 + a.titem2
                            }).ToList().OrderBy(x => x.periodo);
          //cargar el DGV con los periodos encontrados
          foreach (var item in dj_empresa)
          {
            dgv_actas_dj_empresa.Rows.Add();
            int fila = dgv_actas_dj_empresa.Rows.Count - 1;
            dgv_actas_dj_empresa.Rows[fila].Cells["periodo"].Value = item.periodo;//.ToString().Substring(3,7);
            dgv_actas_dj_empresa.Rows[fila].Cells["rectificacion"].Value = item.rectifi;
            dgv_actas_dj_empresa.Rows[fila].Cells["aporte_ley"].Value = item.aporteley;
            dgv_actas_dj_empresa.Rows[fila].Cells["aporte_socio"].Value = item.aportesocio;
            dgv_actas_dj_empresa.Rows[fila].Cells["fecha_pago"].Value = (item.fpago == null) ? string.Empty : item.fpago.ToString().Substring(0, 10);
            dgv_actas_dj_empresa.Rows[fila].Cells["importe_depositado"].Value = item.impban1;
            dgv_actas_dj_empresa.Rows[fila].Cells["capital"].Value = (item.capital == 0 && item.importe_depositado != 0) ? item.impban1 : item.capital;
            dgv_actas_dj_empresa.Rows[fila].Cells["total"].Value = (item.fpago != null) ?
                calcular_coeficiente_A(Convert.ToDateTime(item.periodo), Convert.ToDateTime(item.fpago), Convert.ToDouble(item.capital), Convert.ToDouble(item.total))
                :
                calcular_coeficiente_B(Convert.ToDateTime(item.periodo), Convert.ToDouble(item.capital), Convert.ToDouble(item.total));

            //variable para el interes
            double interes = Convert.ToDouble(dgv_actas_dj_empresa.Rows[fila].Cells["total"].Value) - Convert.ToDouble(dgv_actas_dj_empresa.Rows[fila].Cells["capital"].Value);
            dgv_actas_dj_empresa.Rows[fila].Cells["interes"].Value = (interes < 0) ? (dgv_actas_dj_empresa.Rows[fila].Cells["total"].Value) : (interes);
          }

          //txt_actas_total_con_interes.Text = sumar_totales().ToString("c");
          generar_periodos_faltantes();
          calcular_cantidad_empleados();
          totales_de_consulta();
          mostrar_actas_involucradas();
          if (dgv_actas_dj_empresa.Rows.Count == 0)
          {
            btn_quitar_periodo.Enabled = false;
          }
          else
          {
            btn_quitar_periodo.Enabled = true;
          }

        }
        else
        {
          MessageBox.Show("El periodo [ DESDE ] debe ser menor o igual que el periodo [ HASTA ] ");
        }
      }
      else
      {
        MessageBox.Show("NO hay empresas para verificar los periodos", "ATENCION");
      }
    }

    private double calcular_coeficiente_A(DateTime periodo, DateTime fpago, double tot_periodo, double pagado)
    {
      if (tot_periodo == 0 && pagado != 0)
      {
        tot_periodo = pagado;
      }

      double tot_interes_mora_de_pago = 0;
      double tot_intereses_a_la_fecha = 0;
      double coef_A = 0;
      double coef_B = 0;
      DateTime fecha_vencimiento = periodo.AddMonths(1);
      fecha_vencimiento = fecha_vencimiento.AddDays(14);

      if (fpago > fecha_vencimiento)
      {
        coef_A = Math.Round(((fpago - fecha_vencimiento).TotalDays * 0.001), 5);
        tot_interes_mora_de_pago = tot_periodo - pagado + (pagado * coef_A);
        coef_B = ((Convert.ToDateTime(txt_actas_venc.Text) - fecha_vencimiento).TotalDays * 0.001) + 1;
        tot_intereses_a_la_fecha = ((tot_periodo - pagado) * coef_B) + ((pagado * coef_A) * (coef_B - coef_A));
      }
      return tot_intereses_a_la_fecha; //tot_interes_mora_de_pago;
    }

    private double calcular_coeficiente_B(DateTime periodo, double tot_periodo, double pagado)
    {
      if (tot_periodo == 0 && pagado != 0)
      {
        tot_periodo = pagado;
      }
      double tot_intereses_a_la_fecha = 0;
      double coef_A = 0;
      double coef_B = 0;
      DateTime fecha_vencimiento = periodo.AddMonths(1);
      fecha_vencimiento = fecha_vencimiento.AddDays(14);
      coef_B = ((Convert.ToDateTime(txt_actas_venc.Text) - fecha_vencimiento).TotalDays * 0.001) + 1;
      tot_intereses_a_la_fecha = ((tot_periodo - pagado) * coef_B) + ((pagado * coef_A) * (coef_B - coef_A));

      return tot_intereses_a_la_fecha; //tot_interes_mora_de_pago;
    }

    private void generar_periodos_faltantes()
    {
      // Se genera una lista y se llena con los periodos  del dgv_actas_dj_empresa
      List<DateTime> lista1 = new List<DateTime>();

      foreach (DataGridViewRow fila in dgv_actas_dj_empresa.Rows)
      {
        lista1.Add(Convert.ToDateTime(fila.Cells["periodo"].Value));
      }

      // Se genera una lista y se llena  con los periodos  de la tabla de periodos
      List<DateTime> lista2 = new List<DateTime>();

      foreach (var fila in db_sindicato.secuencia_periodos.Where(x => x.periodo >= Convert.ToDateTime("01/" + txt_actas_desde.Text) && x.periodo <= Convert.ToDateTime("01/" + txt_actas_hasta.Text)))
      {
        lista2.Add(Convert.ToDateTime(fila.periodo));
      }
      // Se genera una variable con los periodos de la lista2 que no esten en la lista1 y obtengo los periodos faltantes
      var periodo_faltante = from p in lista2.Except(lista1) select new { periodo = p.Date };

      int f = 0;
      foreach (var item in periodo_faltante.ToList())
      {
        dgv_actas_dj_empresa.Rows.Add();
        f = dgv_actas_dj_empresa.Rows.Count - 1;
        dgv_actas_dj_empresa.Rows[f].DefaultCellStyle.BackColor = Color.PaleVioletRed;
        dgv_actas_dj_empresa.Rows[f].Cells["periodo"].Value = item.periodo.Date;
        dgv_actas_dj_empresa.Rows[f].Cells["rectificacion"].Value = 0;
        dgv_actas_dj_empresa.Rows[f].Cells["aporte_ley"].Value = 0;
        dgv_actas_dj_empresa.Rows[f].Cells["aporte_ley"].ReadOnly = false;
        dgv_actas_dj_empresa.Rows[f].Cells["aporte_socio"].Value = 0;
        dgv_actas_dj_empresa.Rows[f].Cells["aporte_socio"].ReadOnly = false;
        dgv_actas_dj_empresa.Rows[f].Cells["fecha_pago"].Value = "NO Declara";
        dgv_actas_dj_empresa.Rows[f].Cells["importe_depositado"].Value = 0;
        dgv_actas_dj_empresa.Rows[f].Cells["empleados"].Value = 0;
        dgv_actas_dj_empresa.Rows[f].Cells["empleados"].ReadOnly = false;
        dgv_actas_dj_empresa.Rows[f].Cells["socios"].Value = 0;
        dgv_actas_dj_empresa.Rows[f].Cells["socios"].ReadOnly = false;
        dgv_actas_dj_empresa.Rows[f].Cells["capital"].Value = 0;
        dgv_actas_dj_empresa.Rows[f].Cells["interes"].Value = 0;
        dgv_actas_dj_empresa.Rows[f].Cells["total"].Value = 0;
        dgv_actas_dj_empresa.Rows[f].Cells["nodeclara"].Value = 1;
      }

      dgv_actas_dj_empresa.Sort(this.dgv_actas_dj_empresa.Columns["periodo"], ListSortDirection.Ascending);

    }

    private void totales_de_consulta()
    {
      decimal total_periodos = 0, total_NO_declarados = 0, total_pagados = 0, total_NO_pagados = 0;
      decimal total_aporte2 = 0, total_aporte_socios = 0, total_depositado = 0, total_interes = 0, total_con_interes = 0;
      decimal total_capital = 0;

      foreach (DataGridViewRow fila in dgv_actas_dj_empresa.Rows)
      {
        total_periodos = dgv_actas_dj_empresa.Rows.Count;

        if (fila.Cells["fecha_pago"].Value.ToString() == "NO Declara") total_NO_declarados += 1;
        if (fila.Cells["fecha_pago"].Value.ToString() == "") total_NO_pagados += 1;
        if ((fila.Cells["fecha_pago"].Value.ToString() != "") && (fila.Cells["fecha_pago"].Value.ToString() != "NO Declara")) total_pagados += 1;
        total_aporte2 += Convert.ToDecimal(fila.Cells["aporte_ley"].Value);
        total_aporte_socios += Convert.ToDecimal(fila.Cells["aporte_socio"].Value);
        total_depositado += Convert.ToDecimal(fila.Cells["importe_depositado"].Value);
        total_interes += Convert.ToDecimal(fila.Cells["interes"].Value);
        total_capital += Convert.ToDecimal(fila.Cells["capital"].Value);
        total_con_interes += Convert.ToDecimal(fila.Cells["total"].Value);
      }

      txt_actas_total_periodos.Text = total_periodos.ToString();
      txt_actas_no_declarados.Text = total_NO_declarados.ToString();
      txt_actas_pagados.Text = total_pagados.ToString();
      txt_actas_no_pagados.Text = total_NO_pagados.ToString();
      txt_actas_aporte2.Text = total_aporte2.ToString("N");
      txt_actas_aportes_socio.Text = total_aporte_socios.ToString("N");
      txt_actas_depositado.Text = total_depositado.ToString("N");
      txt_actas_intereses.Text = total_interes.ToString("N");
      txt_capital.Text = total_capital.ToString("N");
      txt_actas_total_con_interes.Text = total_con_interes.ToString("N");
    }

    private void calcular_cantidad_empleados()
    {
      foreach (DataGridViewRow fila in dgv_actas_dj_empresa.Rows)
      {
        var cant_emp = db_sindicato.ddjj.Where(
            x => x.cuite == Convert.ToDouble(txt_actas_cuit.Text) &&
            (x.periodo == Convert.ToDateTime(fila.Cells["periodo"].Value))
            && x.rect == Convert.ToInt16(fila.Cells["rectificacion"].Value));
        fila.Cells["empleados"].Value = cant_emp.Count(); //Count(x => x.item1);
        fila.Cells["socios"].Value = cant_emp.Count(x => x.item2);
      }
    }

    private void btn_quitar_periodo_Click(object sender, EventArgs e)
    {
      dgv_actas_dj_empresa.Rows.RemoveAt(dgv_actas_dj_empresa.CurrentRow.Index);
      totales_de_consulta();
      if (dgv_actas_dj_empresa.Rows.Count == 0) btn_quitar_periodo.Enabled = false;
    }

    private void mostrar_actas_involucradas()
    {
      using (var context = new lts_sindicatoDataContext())
      {
        Buscadores buscar = new Buscadores();
        var prueba = context.ACTAS.Where(x => x.CUIT == Convert.ToInt64(txt_actas_cuit.Text.Trim()));

        var actas_involucradas = from act in context.ACTAS
                                 where act.CUIT == Convert.ToDouble(txt_actas_cuit.Text.Trim())
                                 select new
                                 {
                                   fecha_asig = act.FECHA_ASIG,
                                   acta = act.ACTA,
                                   desde = act.DESDE,
                                   hasta = act.HASTA,
                                   estado = act.COBRADOTOTALMENTE,
                                   inspector = (act.ESTUDIO_JURIDICO == 0 || act.ESTUDIO_JURIDICO == null) ? act.INSPECTOR : act.INSPECTOR_ANTERIOR,
                                   importe_acta = (act.ACTA != null) ? Convert.ToDecimal(act.DEUDATOTAL) : act.ASIG_DEUDA_APROX,
                                   estudio_inspector = (act.ESTUDIO_JURIDICO == 1) ? act.INSPECTOR : "-"//(buscar.get_un_inspector(Convert.ToInt16(act.ESTUDIO_INSPECTOR)).apellido).ToString():("-") //buscar.get_un_inspector(Convert.ToInt16(act.ESTUDIO_INSPECTOR)).apellido
                                                                                                        //(act.ESTUDIO_INSPECTOR == null) ? (buscar.get_un_inspector(Convert.ToInt16(act.ESTUDIO_INSPECTOR)).apellido) : (estudio_inspector)
                                 };
        dgv_actas_inv_asig.DataSource = actas_involucradas.ToList();


        if (actas_involucradas.Count() == 0)
        {
          dgv_cobros.DataSource = null;
          dgv_cobros.Refresh();
          btn_cobrar.Enabled = false;
        }
        else
        {
          btn_cobrar.Enabled = true;
          //reportes_CR f_reportes_CR = new reportes_CR()
          //List < DataGridViewRow > rows = (from item in dgv_actas_inv_asig.Rows.Cast<DataGridViewRow>()
          //                                   let fecha = Convert.ToDateTime(item.Cells["fecha_asig"].Value ?? string.Empty)
          //                                   let desde = Convert.ToString(item.Cells["desde"].Value ?? string.Empty)
          //                                   let descripcion = Convert.ToString(item.Cells["descripcion"].Value ?? string.Empty)
          //                                   where clave.Contains(busqueda) ||
          //                                          modelo.Contains(busqueda) ||
          //                                          descripcion.Contains(busqueda)
          //                                   select item).ToList<DataGridViewRow>();
        }
      }

    }

    private void dgv_actas_inv_asig_SelectionChanged(object sender, EventArgs e)
    {
      mostrar_comprobantes();
    }

    private void mostrar_comprobantes()
    {

      var comprobantes_actas = from comp in db_sindicato.COBROS
                               where comp.ACTA == Convert.ToInt32(dgv_actas_inv_asig.CurrentRow.Cells["num_acta"].Value)
                               select new
                               {
                                 cobro_id = comp.Id,
                                 cuota = (comp.CONCEPTO == "2") ? (comp.CUOTAX.ToString() + " de " + comp.CANTIDAD_CUOTAS.ToString()) : ("Anticipo"),
                                 fecha_venc = comp.FECHA_VENC,
                                 fecha = comp.FECHARECAUDACION,
                                 comprobante = comp.RECIBO,
                                 importe = comp.TOTAL
                               };
      if (dgv_cobros.Rows.Count > 0)
      {
        dgv_cobros.DataSource = comprobantes_actas.ToList();
        foreach (DataGridViewRow fila in dgv_cobros.Rows)
        {
          double dias = (DateTime.Today.Date - Convert.ToDateTime(fila.Cells["f_venc"].Value).Date).TotalDays;
          if (fila.Cells["cuota"].Value.ToString() != "Anticipo")
          {

            if ((DateTime.Today - Convert.ToDateTime(fila.Cells["f_venc"].Value)).TotalDays > 0)
            {
              fila.Cells["dias_atraso"].Value = dias;//(DateTime.Today - Convert.ToDateTime(fila.Cells["f_venc"].Value)).TotalDays;
              fila.Cells["interes_mora"].Value = (0.01 * dias) * Convert.ToDouble(fila.Cells["monto_pago"].Value);
            }
            else
            {
              fila.Cells["dias_atraso"].Value = "0";
            }

          }
        }
      }
    }

    private void btn_actas_asignar_inspector_Click(object sender, EventArgs e)
    {
      if (txt_actas_empresa.Text != "")
      {
        asignar_acta();
      }
    }

    private bool controlar_periodos_asignados()
    {
      bool ok = true;

      if (dgv_actas_dj_empresa.Rows.Count > 0)
      {
        string mensaje = string.Empty;
        DateTime desde = Convert.ToDateTime("01/" + txt_actas_desde.Text);
        DateTime hasta = Convert.ToDateTime("01/" + txt_actas_hasta.Text);
        foreach (DataGridViewRow fila in dgv_actas_inv_asig.Rows)
        {
          if (desde <= hasta)
          {
            if (
                (
                Convert.ToDateTime(fila.Cells["acta_desde"].Value) >= desde && Convert.ToDateTime(fila.Cells["acta_desde"].Value) <= hasta
               ||
                 Convert.ToDateTime(fila.Cells["acta_hasta"].Value) >= desde && Convert.ToDateTime(fila.Cells["acta_hasta"].Value) <= hasta
                 )
               ||
                (
                desde >= Convert.ToDateTime(fila.Cells["acta_desde"].Value) && desde <= Convert.ToDateTime(fila.Cells["acta_hasta"].Value)
               ||
                 hasta >= Convert.ToDateTime(fila.Cells["acta_hasta"].Value) && hasta <= Convert.ToDateTime(fila.Cells["acta_hasta"].Value))
                 )
            {

              if (fila.Cells["num_acta"].Value != null)
              {

                if (fila.Cells["num_acta"].Value.ToString() != "")
                {
                  mensaje += "El periodo pertenece al acta generada Nº " + fila.Cells["num_acta"].Value.ToString();
                  ok = false;
                }
                else
                {
                  mensaje += "El periodo pertenece al acta asignada para el inspector " + fila.Cells["inspector"].Value.ToString();
                  ok = false;
                }
              }
              else
              {
                mensaje += "El periodo pertenece al acta asignada para el inspector " + fila.Cells["acta_inspector"].Value.ToString();
                ok = false;
              }
            }
          }
          else
          {
            MessageBox.Show("El periodo [ DESDE ] debe ser menor o iguan que el periodo [ HASTA ] ");
          }
        }
        if (mensaje != string.Empty)
        {
          MessageBox.Show(mensaje, "Imposible asignar el Acta");
        }
      }
      return ok;

    }

    private void asignar_acta()
    {
      if (controlar_periodos_asignados() && buscar_cuit_asignado())
      {
        dgv_actas_asignar.Rows.Add();
        int f = dgv_actas_asignar.Rows.Count - 1;
        dgv_actas_asignar.Rows[f].Cells["asignacion"].Value = DateTime.Now.Date.ToShortDateString(); ;
        dgv_actas_asignar.Rows[f].Cells["cuit"].Value = txt_actas_cuit.Text;
        dgv_actas_asignar.Rows[f].Cells["empresa"].Value = txt_actas_empresa.Text;
        dgv_actas_asignar.Rows[f].Cells["inspector"].Value = cbx_actas_inspectores.Text;
        dgv_actas_asignar.Rows[f].Cells["desde"].Value = txt_actas_desde.Text;
        dgv_actas_asignar.Rows[f].Cells["hasta"].Value = txt_actas_hasta.Text;
        dgv_actas_asignar.Rows[f].Cells["deuda_aproximada"].Value = txt_actas_total_con_interes.Text; //txt_actas_total_con_interes.Text.Substring(1, txt_actas_total_con_interes.TextLength - 1);
        btn_actas_quitar.Enabled = true;
        btn_actas_aplicar.Enabled = true;
      }
    }

    private bool buscar_cuit_asignado()
    {
      bool ok = true;
      foreach (DataGridViewRow fila in dgv_actas_asignar.Rows)
      {
        if (fila.Cells["cuit"].Value.ToString().Trim() == txt_actas_cuit.Text.Trim())
        {
          ok = false;
          MessageBox.Show("La empresa ya se encuentra en la lista de asignacion para el Inspector [ " + fila.Cells["inspector"].Value.ToString() + " ]", "IMPORTANTE");
        }
      }
      return ok;
    }

    private void btn_actas_aplicar_Click(object sender, EventArgs e)
    {

      foreach (DataGridViewRow fila in dgv_actas_asignar.Rows)
      {
        ACTAS acta = new ACTAS();
        acta.CUIT = Convert.ToInt64(fila.Cells["cuit"].Value.ToString());
        acta.FECHA_ASIG = Convert.ToDateTime(fila.Cells["asignacion"].Value);
        acta.ASIG_DESDE = Convert.ToDateTime("01/" + fila.Cells["desde"].Value);
        acta.ASIG_HASTA = Convert.ToDateTime("01/" + fila.Cells["hasta"].Value);
        acta.DESDE = Convert.ToDateTime("01/" + fila.Cells["desde"].Value);
        acta.HASTA = Convert.ToDateTime("01/" + fila.Cells["hasta"].Value);
        acta.INSPECTOR = fila.Cells["inspector"].Value.ToString();
        acta.ASIG_DEUDA_APROX = Convert.ToDecimal(fila.Cells["deuda_aproximada"].Value);
        db_sindicato.ACTAS.InsertOnSubmit(acta);
        db_sindicato.SubmitChanges();
      }
      while (dgv_actas_asignar.Rows.Count != 0)
      {
        dgv_actas_asignar.Rows.RemoveAt(0);
      }
      cargar_seguimiento();
      mostrar_actas_involucradas();

    }

    private void btn_actas_quitar_Click(object sender, EventArgs e)
    {
      dgv_actas_asignar.Rows.Remove(dgv_actas_asignar.CurrentRow);
      if (dgv_actas_asignar.Rows.Count > 1)
      {
        btn_actas_quitar.Enabled = true;
      }
      else
      {
        btn_actas_quitar.Enabled = false;
        btn_actas_aplicar.Enabled = false;
      }
    }

    private void cargar_seguimiento()
    {
      func_utiles.limpiar_dgv(dgv_seguimiento);
      using (var context = new lts_sindicatoDataContext())
      {
        string filtro = cbx_inspector_seguimiento.Text;

        var segui = (from a in context.ACTAS  //db_sindicato.ACTAS
                     where a.ESTUDIO_JURIDICO == 0 ?
                     a.ACTA.ToString() == null && (filtro == "TODOS" ? a.INSPECTOR != "TODOS" : a.INSPECTOR == filtro)
                     :
                     (filtro == "TODOS" ? a.INSPECTOR != "TODOS" : a.INSPECTOR == filtro)

                     //where a.ACTA.ToString() == null && (filtro == "TODOS" ? a.INSPECTOR != "TODOS" : a.INSPECTOR == filtro)
                     select new
                     {
                       fecha_asignacion = a.FECHA_ASIG,
                       cuit = a.CUIT,
                       empresa = context.maeemp.Where(x => x.MAEEMP_CUIT == a.CUIT).Single().MAEEMP_RAZSOC,
                       //empresa = context.maeemp.Where(x => x.MEEMP_CUIT_STR == a.CUIT.ToString()).Single().MAEEMP_RAZSOC,
                       inspector = a.INSPECTOR,
                       dias = a.FECHA_ASIG == null ? 0 : (DateTime.Today - Convert.ToDateTime(a.FECHA_ASIG)).TotalDays,
                       id_acta = a.ID_ACTA,
                       capital = a.DEUDATOTAL,
                       deuda = a.ESTUDIO_JURIDICO == 1 ? a.MONTO_CERTIF_ACTA : Convert.ToDecimal(a.ASIG_DEUDA_APROX),
                       acta = a.ACTA
                     }).OrderByDescending(x => x.dias).ToList();

        if (segui.Count > 0)
        {
          decimal total_deuda_asignada = 0;
          foreach (var item in segui)
          {
            dgv_seguimiento.Rows.Add();
            int f = dgv_seguimiento.Rows.Count - 1;
            dgv_seguimiento.Rows[f].Cells["dgv_segui_asignacion"].Value = item.fecha_asignacion;
            dgv_seguimiento.Rows[f].Cells["dgv_segui_cuit"].Value = item.cuit;
            dgv_seguimiento.Rows[f].Cells["dgv_segui_empresa"].Value = item.empresa;
            dgv_seguimiento.Rows[f].Cells["dgv_segui_inspector"].Value = item.inspector.Trim();
            dgv_seguimiento.Rows[f].Cells["dgv_segui_dias"].Value = item.dias.ToString();
            dgv_seguimiento.Rows[f].Cells["dgv_segui_capital"].Value = item.capital;
            dgv_seguimiento.Rows[f].Cells["dgv_segui_deuda"].Value = item.deuda;
            dgv_seguimiento.Rows[f].Cells["id_acta"].Value = item.id_acta;
            dgv_seguimiento.Rows[f].Cells["dgv_segui_acta"].Value = item.acta;

            total_deuda_asignada += Convert.ToDecimal(item.deuda);
          }
          lbl_cantidad_asignada.Text = segui.Count.ToString();
          lbl_total_deuda_asignada.Text = total_deuda_asignada.ToString("N2");
        }
        else
        {
          lbl_cantidad_asignada.Text = "0";
        }

      }
    }

    private void mostrar_mensajes()
    {

    }

    private void btn_imprimir_consulta_Click(object sender, EventArgs e)
    {
      //limpio la tabla de impresion

      var im = from a in db_sindicato.impresion_comprobante select a;
      foreach (var item in im)
      {
        db_sindicato.impresion_comprobante.DeleteOnSubmit(item);
        db_sindicato.SubmitChanges();
      }

      var imprimir_actas = from a in db_sindicato.impresion_actas select a;

      foreach (var item in imprimir_actas)
      {
        db_sindicato.impresion_actas.DeleteOnSubmit(item);
        db_sindicato.SubmitChanges();
      }

      DataTable dtperiodos = DScpn.ActasDetalle;
      dtperiodos.Clear();

      int Color = 0; ;

      for (int f = 0; f < dgv_actas_dj_empresa.Rows.Count; f++)
      {

        Color += 1;
        DataRow row = dtperiodos.NewRow();
        row["NumeroDeActa"] = 0;
        row["Periodo"] = Convert.ToDateTime(dgv_actas_dj_empresa.Rows[f].Cells["periodo"].Value);
        row["CantidadDeEmpleados"] = Convert.ToInt16(dgv_actas_dj_empresa.Rows[f].Cells["empleados"].Value);
        row["CantidadSocios"] = Convert.ToInt16(dgv_actas_dj_empresa.Rows[f].Cells["socios"].Value);
        row["TotalSueldoEmpleados"] = 0;
        row["TotalSueldoSocios"] = 0;
        row["TotalAporteEmpleados"] = Convert.ToDecimal(dgv_actas_dj_empresa.Rows[f].Cells["aporte_ley"].Value);
        row["TotalAporteSocios"] = Convert.ToDecimal(dgv_actas_dj_empresa.Rows[f].Cells["aporte_socio"].Value);
        row["FechaDePago"] = (dgv_actas_dj_empresa.Rows[f].Cells["fecha_pago"].Value != null) ? dgv_actas_dj_empresa.Rows[f].Cells["fecha_pago"].Value.ToString() : "";
        row["ImporteDepositado"] = Convert.ToDecimal(dgv_actas_dj_empresa.Rows[f].Cells["importe_depositado"].Value);
        row["DiasDeMora"] = 0;
        row["DeudaGenerada"] = Convert.ToDecimal(dgv_actas_dj_empresa.Rows[f].Cells["capital"].Value);
        row["InteresGenerado"] = Convert.ToDecimal(dgv_actas_dj_empresa.Rows[f].Cells["interes"].Value);
        row["Total"] = Convert.ToDecimal(dgv_actas_dj_empresa.Rows[f].Cells["total"].Value);
        row["Color"] = Color;
        dtperiodos.Rows.Add(row);

        DataTable dtTotalesDDJJ = DScpn.TotalesDDJJ;
        dtTotalesDDJJ.Clear();

        DataRow Row = dtTotalesDDJJ.NewRow();

        ////Periodos
        Row["Periodos"] = txt_actas_total_periodos.Text;
        Row["Pagados"] = Convert.ToInt32(txt_actas_pagados.Text);
        Row["NoPagados"] = Convert.ToInt32(txt_actas_no_pagados.Text);
        Row["NoDeclarados"] = txt_actas_no_declarados.Text;

        ////Aportes
        Row["AporteLey"] = txt_actas_aporte2.Text;
        Row["AporteSocio"] = txt_actas_aportes_socio.Text;
        Row["TotalAportes"] = (Convert.ToDecimal(txt_actas_aporte2.Text) + Convert.ToDecimal(txt_actas_aportes_socio.Text)).ToString("N");

        ////Deuda
        Row["TotalDeuda"] = txt_capital.Text;
        Row["TotalDepositado"] = txt_actas_depositado.Text;
        Row["TotalIntereses"] = txt_actas_intereses.Text;
        Row["Total"] = txt_actas_total_con_interes.Text;

        //impresion_comprobante imp = new impresion_comprobante();
        //imp.empresa = txt_actas_empresa.Text;
        //imp.cuit = txt_actas_cuit.Text;
        //imp.localidad = txt_actas_localidad.Text;
        //imp.telefono = txt_actas_telefono.Text;
        //imp.estudio = txt_actas_estudio.Text;
        //imp.domicilio = txt_actas_domicilio.Text;
        //imp.desde = txt_actas_desde.Text;
        //imp.hasta = txt_actas_hasta.Text;
        //imp.periodo = Convert.ToDateTime(dgv_actas_dj_empresa.Rows[f].Cells["periodo"].Value);
        //imp.rectificacion = Convert.ToInt16(dgv_actas_dj_empresa.Rows[f].Cells["rectificacion"].Value);
        //imp.aporte_ley = Convert.ToDecimal(dgv_actas_dj_empresa.Rows[f].Cells["aporte_ley"].Value);
        //imp.aporte_socio = Convert.ToDecimal(dgv_actas_dj_empresa.Rows[f].Cells["aporte_socio"].Value);
        //imp.fechapago = (dgv_actas_dj_empresa.Rows[f].Cells["fecha_pago"].Value != null) ? dgv_actas_dj_empresa.Rows[f].Cells["fecha_pago"].Value.ToString() : "";
        //imp.importe_depositado = Convert.ToDecimal(dgv_actas_dj_empresa.Rows[f].Cells["importe_depositado"].Value);
        //imp.cantidad_empleados = Convert.ToInt16(dgv_actas_dj_empresa.Rows[f].Cells["empleados"].Value);
        //imp.cantidad_socios = Convert.ToInt16(dgv_actas_dj_empresa.Rows[f].Cells["socios"].Value);
        //imp.capital = Convert.ToDecimal(dgv_actas_dj_empresa.Rows[f].Cells["capital"].Value);
        //imp.interes = Convert.ToDecimal(dgv_actas_dj_empresa.Rows[f].Cells["interes"].Value);
        //imp.total = Convert.ToDecimal(dgv_actas_dj_empresa.Rows[f].Cells["total"].Value);

        ////Periodos
        //imp.benef_dni = txt_actas_total_periodos.Text;
        //imp.nro_entrega = Convert.ToInt32(txt_actas_pagados.Text);
        //imp.nro_socio = Convert.ToInt32(txt_actas_no_pagados.Text);
        //imp.socio_dni = txt_actas_no_declarados.Text;
        ////Aportes
        //imp.benef_apenom = txt_actas_aporte2.Text;
        //imp.benef_tipo_mochila = txt_actas_aportes_socio.Text;
        //imp.COL1NOMBRE = (Convert.ToDecimal(txt_actas_aporte2.Text) + Convert.ToDecimal(txt_actas_aportes_socio.Text)).ToString("N");
        ////Deuda
        //imp.EQUIPO = txt_capital.Text;
        //imp.benef_legajo = txt_actas_depositado.Text;
        //imp.benef_fdo_desempleo = txt_actas_intereses.Text;
        //imp.comentario = txt_actas_total_con_interes.Text;

        //db_sindicato.impresion_comprobante.InsertOnSubmit(imp);
        //db_sindicato.SubmitChanges();
      }

      DataTable DTHistorial_pagos = DScpn.historial_pagos;// .Tables["historial_pagos"];
      DTHistorial_pagos.Clear();

      DataTable dt_denuncias = DScpn.denuncias;
      dt_denuncias.Clear();

      DataTable dtActasInvolucradas = DScpn.impresion_actas;
      dtActasInvolucradas.Clear();

      var actas_involucradas = (from act in db_sindicato.ACTAS
                                where act.CUIT == Convert.ToInt64(txt_actas_cuit.Text)
                                select new
                                {
                                  fecha_asig = act.FECHA_ASIG,
                                  acta = act.ACTA,
                                  desde = act.DESDE,
                                  hasta = act.HASTA,
                                  estado = act.COBRADOTOTALMENTE,
                                  inspector = act.INSPECTOR,
                                  importe_acta = act.DEUDATOTAL
                                }).ToList();

      foreach (var item in actas_involucradas.ToList())
      {
        DataRow row = dtActasInvolucradas.NewRow();

        //impresion_actas imp_act = new impresion_actas();
        //imp_act.fecha1 = item.fecha_asig;
        //imp_act.acta = item.acta.ToString();
        //imp_act.fecha2 = item.desde;
        //imp_act.fecha3 = item.hasta;
        //imp_act.cobrado = item.estado == null ? "NO" : item.estado.ToString();
        //imp_act.inspector = item.inspector;
        //imp_act.importe = Convert.ToDecimal(item.importe_acta);

        //db_sindicato.impresion_actas.InsertOnSubmit(imp_act);
        //db_sindicato.SubmitChanges();


        if (item.fecha_asig != null)
        {
          row["fecha1"] = item.fecha_asig;
        }
        //row["fecha1"] =  item.fecha_asig == null? null : item.fecha_asig;
        row["acta"] = item.acta.ToString();
        row["fecha2"] = item.desde;
        row["fecha3"] = item.hasta;
        row["cobrado"] = item.estado == null ? "NO" : item.estado.ToString();
        row["inspector"] = item.inspector;
        row["importe"] = Convert.ToDecimal(item.importe_acta);

        dtActasInvolucradas.Rows.Add(row);
        //asignar_cobros_DsHistorial_pagos(Convert.ToDouble(item.acta));
        using (lts_sindicatoDataContext context = new lts_sindicatoDataContext())
        {
          var historial_cobros = (from a in context.COBROS
                                  where Convert.ToInt32(a.ACTA) == item.acta
                                  select a).ToList();
          foreach (var item_ in historial_cobros)
          {
            DataRow Row = DTHistorial_pagos.NewRow();
            Row["acta"] = item_.ACTA;
            if (item_.FECHARECAUDACION != null)
            {
              Row["fecha"] = item_.FECHARECAUDACION;
            }
            // == null ? DBNull.Value : item_.FECHARECAUDACION;
            Row["recibo"] = item_.RECIBO;
            Row["importe"] = item_.TOTAL;
            DTHistorial_pagos.Rows.Add(Row);
          }
        }
      }
      //Asigno dt_denuncias  los valores para imprimir
      using (lts_sindicatoDataContext context = new lts_sindicatoDataContext())
      {
        var denuncias = (from a in context.ParaInspeccion
                         where a.ESTADO == 0 && a.CUIT == txt_actas_cuit.Text.Trim()
                         select a).ToList();
        if (denuncias.Count() > 0)
        {
          foreach (var item in denuncias)
          {
            Clases.socios soc = new socios();
            soc.get_datos_socio(Convert.ToDouble(item.CUIL), 0);
            DataRow row = dt_denuncias.NewRow();
            row["cuil"] = item.CUIL;
            row["apellido"] = soc.soc.apellido;
            row["nombre"] = soc.soc.nombre;
            row["comentario"] = soc.soc.comentario;
            row["fecha"] = item.FECHA;
            dt_denuncias.Rows.Add(row);
          }
        }
      }

      reportes frm_reportes = new reportes();
      frm_reportes.nombreReporte = "rpt_consulta_ddjj";
      //frm_reportes.historial_pagos = DTHistorial_pagos;
      //frm_reportes.denuncias = dt_denuncias;
      frm_reportes.dt = dtperiodos;
      frm_reportes.dt2 = dtActasInvolucradas;
      frm_reportes.dt3 = DTHistorial_pagos;
      frm_reportes.dt4 = dt_denuncias;
      frm_reportes.Show();
    }

    private void btn_imprimir_empleados_Click(object sender, EventArgs e)
    {
      ImprimirEmpleados();

      ////limpio la tabla de impresion
      //var im = from a in db_sindicato.impresion_comprobante select a;
      //foreach (var item in im)
      //{
      //  db_sindicato.impresion_comprobante.DeleteOnSubmit(item);
      //  db_sindicato.SubmitChanges();
      //}

      //DateTime desde = Convert.ToDateTime("01/" + txt_actas_desde.Text);
      //DateTime hasta = Convert.ToDateTime("01/" + txt_actas_hasta.Text);

      //var emp = (from a in db_sindicato.ddjj
      //           where a.cuite == Convert.ToDouble(txt_actas_cuit.Text) && a.periodo >= desde && a.periodo <= hasta
      //           join datos in db_sindicato.maesoc on a.cuil equals datos.MAESOC_CUIL
      //           join cat in db_sindicato.categorias_empleado on datos.MAESOC_CODCAT equals cat.MAECAT_CODCAT
      //           join act in db_sindicato.actividad on datos.MAESOC_CODACT equals act.MAEACT_CODACT
      //           join soce in db_sindicato.socemp on a.cuil equals soce.SOCEMP_CUIL
      //           where soce.SOCEMP_ULT_EMPRESA == 'S'
      //           orderby a.periodo
      //           select new
      //           {
      //             periodo = a.periodo,//String.Format("{0:dd/MM/yyyy}", a.periodo),
      //             CUIL = String.Format("{0:g}", a.cuil),
      //             nombre = (datos.MAESOC_APELLIDO.Trim() + " " + datos.MAESOC_NOMBRE.Trim()),
      //             aporte_ley = (a.impo + a.impoaux) * 0.02, // String.Format("{0:C}", ((a.impo + a.impoaux) * 0.02)),
      //             aporte_socio = (a.item2 == true) ? ((a.impo + a.impoaux) * 0.02) : 0, //String.Format("{0:C}", ((a.item2 == true) ? (a.impo + a.impoaux) * 0.02 : 0)),
      //             sueldo = a.impo,//String.Format("{0:C}", a.impo),
      //             acuerdo = a.impoaux, //String.Format("{0:C}", a.impoaux),
      //             licencia = (a.lic != "0000") ? "SI" : "NO",
      //             jornada = (a.jorp == true) ? "JP" : "JC",
      //             fecha_ing = (soce.SOCEMP_FECHAING == null) ? " " : soce.SOCEMP_FECHAING.ToString(), //String.Format("{0:dd/MM/yyyy}", soce.SOCEMP_FECHAING),
      //             fecha_baja = (soce.SOCEMP_FECHABAJA == null) ? " " : soce.SOCEMP_FECHABAJA.ToString(),
      //             categoria = cat.MAECAT_NOMCAT.Trim()

      //           }).ToList();//.OrderBy(x=>Convert.ToDateTime( x.periodo));
      //                       //dgv_1.DataSource = emp.ToList();//.ThenBy(x=>x.APENOM).ToList();

      //foreach (var item in emp)
      //{
      //  impresion_comprobante imp = new impresion_comprobante();
      //  imp.fecha1 = item.periodo;
      //  imp.cuit = item.CUIL;
      //  imp.socio_apenom = item.nombre;
      //  imp.aporte_ley = Convert.ToDecimal(item.aporte_ley);
      //  imp.aporte_socio = Convert.ToDecimal(item.aporte_socio);
      //  imp.importe_depositado = Convert.ToDecimal(item.sueldo);
      //  imp.interes = Convert.ToDecimal(item.acuerdo);
      //  imp.COL2NOMBRE = item.licencia;
      //  imp.COL1NOMBRE = item.jornada;
      //  imp.COL2EMPRESA = item.fecha_ing;
      //  imp.COL1EMPRESA = item.fecha_baja; //item.fecha_baja == " " ? " " : item.fecha_baja.ToString().Substring(9,2);// + "/" + item.fecha_baja.Substring(6,2) + "/" + item.fecha_baja.Substring(1,4);
      //  imp.CATEGORIA = item.categoria;
      //  db_sindicato.impresion_comprobante.InsertOnSubmit(imp);
      //  db_sindicato.SubmitChanges();
      //}
      ////reportes_CR f_reportes = new reportes_CR(1);

      ////f_reportes.Show();
    }

    private void ImprimirEmpleados()
    {
      using (var context = new lts_sindicatoDataContext())
      {
        DateTime desde = Convert.ToDateTime("01/" + txt_actas_desde.Text);
        DateTime hasta = Convert.ToDateTime("01/" + txt_actas_hasta.Text);

        var empl = (from a in context.ddjj
                    join datos in context.maesoc on a.cuil equals datos.MAESOC_CUIL
                    join cat in context.categorias_empleado on datos.MAESOC_CODCAT equals cat.MAECAT_CODCAT
                    join act in context.actividad on datos.MAESOC_CODACT equals act.MAEACT_CODACT
                    join empresa in context.socemp on a.cuil equals empresa.SOCEMP_CUIL
                    where empresa.SOCEMP_ULT_EMPRESA == 'S'
                    where (a.cuite == Convert.ToDouble(txt_actas_cuit.Text)) && a.periodo == desde // (a.periodo >= desde && a.periodo <= hasta)
                    && (a.impo <= 22000)
                    //&& (a.jorp == true) a.cuil == 20167719946 && 
                    select new
                    {
                      periodo = a.periodo,//String.Format("{0:dd/MM/yyyy}", a.periodo),
                      CUIL = String.Format("{0:g}", a.cuil),
                      nombre = (datos.MAESOC_APELLIDO.Trim() + " " + datos.MAESOC_NOMBRE.Trim()),
                      aporte_ley = (a.impo + a.impoaux) * 0.02, // String.Format("{0:C}", ((a.impo + a.impoaux) * 0.02)),
                      aporte_socio = (a.item2 == true) ? ((a.impo + a.impoaux) * 0.02) : 0, //String.Format("{0:C}", ((a.item2 == true) ? (a.impo + a.impoaux) * 0.02 : 0)),
                      sueldo = a.impo,//String.Format("{0:C}", a.impo),
                      acuerdo = a.impoaux, //String.Format("{0:C}", a.impoaux),
                      licencia = (a.lic != "0000") ? "SI" : "NO",
                      jornada = (a.jorp == true) ? "JP" : "JC",
                      fecha_ing = (empresa.SOCEMP_FECHAING == null) ? " " : empresa.SOCEMP_FECHAING.ToString(), //String.Format("{0:dd/MM/yyyy}", soce.SOCEMP_FECHAING),
                      fecha_baja = (empresa.SOCEMP_FECHABAJA == null) ? " " : empresa.SOCEMP_FECHABAJA.ToString(),
                      categoria = cat.MAECAT_NOMCAT.Trim()
                    }).OrderBy(x => x.periodo).ToList();

        DataTable DTResumenEmpleados = DScpn.EmpleadosResumen;
        DTResumenEmpleados.Clear();

        foreach (var item in empl)
        {
          DataRow Row = DTResumenEmpleados.NewRow();
          Row["Periodo"] = item.periodo;
          Row["CUIL"] = item.CUIL;
          Row["Nombre"] = item.nombre;
          Row["AporteLey"] = item.aporte_ley;
          Row["AporteSocio"] = item.aporte_socio;
          Row["Licencia"] = item.licencia;
          Row["FechaAlta"] = item.fecha_ing;
          Row["FechaBaja"] = item.fecha_baja;
          Row["Sueldo"] = item.sueldo;
          Row["Categoria"] = item.categoria;
          DTResumenEmpleados.Rows.Add(Row);
        }

        //reportes frm_reportes = new reportes();
        //frm_reportes.nombreReporte = "rpt_EmpleadoResumen";
        //frm_reportes.historial_pagos = DTResumenEmpleados;
        //frm_reportes.Show();


      }
    }

    string generar_ceros(string valor, int tamaño)
    {
      string ceros = null;
      for (int i = 0; i < tamaño - valor.Length; i++)
      {
        ceros += "0";
      }
      valor = ceros + valor;
      return valor;
    }

    private void btn_generar_acta_Click(object sender, EventArgs e)
    {
      generar_Actas f_gen_Acta = new generar_Actas();
      f_gen_Acta.lbl_cuit.Text = dgv_seguimiento.CurrentRow.Cells["dgv_segui_cuit"].Value.ToString();
      f_gen_Acta.lbl_razon_social.Text = dgv_seguimiento.CurrentRow.Cells["dgv_segui_empresa"].Value.ToString();
      f_gen_Acta.act_id = Convert.ToInt32(dgv_seguimiento.CurrentRow.Cells["id_acta"].Value);
      f_gen_Acta.Show();
      cargar_seguimiento();
    }

    private void cbx_inspector_seguimiento_SelectedIndexChanged(object sender, EventArgs e)
    {
      cargar_seguimiento();
    }

    private void btn_comentario_Click(object sender, EventArgs e)
    {
      frm_novedades f_novedades = new frm_novedades();
      f_novedades.id_acta = Convert.ToInt32(dgv_seguimiento.CurrentRow.Cells["id_acta"].Value);
      f_novedades.lbl_cuit.Text = dgv_seguimiento.CurrentRow.Cells["dgv_segui_cuit"].Value.ToString();
      f_novedades.lbl_razon_social.Text = dgv_seguimiento.CurrentRow.Cells["dgv_segui_empresa"].Value.ToString();
      f_novedades.ShowDialog();
    }

    private void materialFlatButton1_Click(object sender, EventArgs e)
    {
      frm_cobros co = new frm_cobros();
      co.Show();
    }

    private void btn_imprimir_seguimiento_Click(object sender, EventArgs e)
    {
      using (lts_sindicatoDataContext context = new lts_sindicatoDataContext())
      {
        func_utiles.limpiar_tabla_impresion();

        foreach (DataGridViewRow fila in dgv_seguimiento.Rows)
        {
          impresion_comprobante imp_insp_asig = new impresion_comprobante();
          imp_insp_asig.FECHA = Convert.ToDateTime(fila.Cells["dgv_segui_asignacion"].Value).Date; //fecha de asignacion
          imp_insp_asig.cuit = fila.Cells["dgv_segui_cuit"].Value.ToString();
          imp_insp_asig.empresa = fila.Cells["dgv_segui_empresa"].Value.ToString().Trim();
          imp_insp_asig.socio_apenom = fila.Cells["dgv_segui_inspector"].Value.ToString(); //Inspector
          imp_insp_asig.cantidad_socios = Convert.ToInt16(fila.Cells["dgv_segui_dias"].Value); // Dias de seguimiento
          imp_insp_asig.importe_depositado = Convert.ToDecimal(fila.Cells["dgv_segui_deuda"].Value); //deuda actualizada
          imp_insp_asig.aporte_ley = Convert.ToDecimal(fila.Cells["dgv_segui_capital"].Value); // Capital del ACTA
          imp_insp_asig.benef_edad = Convert.ToInt16(fila.Cells["dgv_segui_acta"].Value);
          db_sindicato.impresion_comprobante.InsertOnSubmit(imp_insp_asig);
          db_sindicato.SubmitChanges();
        }
      }

      //if (cbx_inspector_seguimiento.Text == "TODOS")
      //{
      //    reportes frm_reportes = new reportes();
      //    frm_reportes.nombreReporte = "rpt_asig_inspector_todos";
      //    frm_reportes.Show();
      //}
      //else
      //{
      reportes frm_reportes = new reportes();
      frm_reportes.nombreReporte = "rpt_asig_inspector";
      frm_reportes.Show();
      //}

    }

    private void btn_cobrar_Click(object sender, EventArgs e)
    {
      frm_cobros f_cobros = new frm_cobros();
      f_cobros.cuit = txt_actas_cuit.Text.Trim();
      f_cobros.razon_social = txt_actas_empresa.Text.Trim();
      f_cobros.ShowDialog();
    }

    private void btn_ranking_Click(object sender, EventArgs e)
    {
      ranking ran = new ranking();
      dgv_ranking_.DataSource = ran.get_empresas().OrderByDescending(x => x.deuda).ToList();
    }

    private void btn_pasar_a_consulta_Click(object sender, EventArgs e)
    {
      txt_actas_desde.Text = (dgv_ranking_.CurrentRow.Cells["ultimo_periodo"].Value).ToString().Substring(3, 7);
      txt_actas_hasta.Text = DateTime.Now.Date.ToString().Substring(3, 7);
      txt_actas_cuit.Text = dgv_ranking_.CurrentRow.Cells["_cuit"].Value.ToString();
      txt_actas_domicilio.Text = dgv_ranking_.CurrentRow.Cells["domicilio"].Value.ToString();
      txt_actas_empresa.Text = dgv_ranking_.CurrentRow.Cells["_empresa"].Value.ToString();
      txt_actas_telefono.Text = dgv_ranking_.CurrentRow.Cells["telefono"].Value.ToString();
      txt_actas_estudio.Text = dgv_ranking_.CurrentRow.Cells["estudio"].Value.ToString();
    }

    private void btn_certificacion_deuda_Click(object sender, EventArgs e)
    {
      reportes frm_reportes = new reportes();
      frm_reportes.nombreReporte = "certificado_de_deuda";
      frm_reportes.Show();
    }

    private void dgv_actas_dj_empresa_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      if (dgv_actas_dj_empresa.CurrentRow.Cells["nodeclara"].Value.ToString() == "1")
      {
        //dgv_actas_dj_empresa.CurrentRow.Cells["aporte_ley"].ReadOnly = false;
        dgv_actas_dj_empresa.Columns["aporte_ley"].ReadOnly = false;
      }

      //if (dgv_actas_dj_empresa.CurrentRow.Cells["nodeclarado"].Value.ToString() == "1")
      //{
      //   dgv_actas_dj_empresa.Rows[e.RowIndex].Cells[e.ColumnIndex].edi
      //}



      ////if (dgv_actas_dj_empresa.CurrentRow.Cells["fecha_pago"].Value.ToString() == "NO Declara")

      ////{
      ////    //dgv_actas_dj_empresa.SelectionMode = DataGridViewSelectionMode.CellSelect;
      ////    dgv_actas_dj_empresa.CurrentRow.Cells["fecha_pago"].ReadOnly = false;
      ////    //dgv_actas_dj_empresa.CurrentRow.Cells[e.ColumnIndex].
      ////}
      ////else
      ////{
      ////    dgv_actas_dj_empresa.CurrentRow.Cells[e.ColumnIndex].ReadOnly = true;
      ////}
    }

    private void btn_pasar_estudio_Click(object sender, EventArgs e)
    {
      Buscadores codigo_postal = new Buscadores();
      Estudio_Juridico f_estudio_Juridico = new Estudio_Juridico();
      f_estudio_Juridico.lbl_cuit.Text = txt_actas_cuit.Text.Trim();
      f_estudio_Juridico.lbl_razon_social.Text = txt_actas_empresa.Text.Trim();
      f_estudio_Juridico.lbl_domicilio.Text = txt_actas_domicilio.Text.Trim();
      f_estudio_Juridico.lbl_localidad.Text = txt_actas_localidad.Text.Trim();
      f_estudio_Juridico.lbl_codigo_postal.Text = codigo_postal.get_codigo_postal(txt_actas_cuit.Text.Trim());
      f_estudio_Juridico.ShowDialog();
    }

    private void get_informe_inspector_porMes()
    {
      using (var context = new lts_sindicatoDataContext())
      {
        DateTime f = Convert.ToDateTime("23/01/2018");
        var inf = from a in context.ACTAS
                    //join empr in context.maeemp on a.CUIT equals empr.MAEEMP_CUIT
                  where a.FECHA >= f.Date
                  group a by new { a.INSPECTOR } into grp_inspector
                  select grp_inspector;
        //{
        //    fecha = grp_inspector. FECHA,
        //    acta = grp_inspector.ACTA,
        //    desde = grp_inspector.DESDE,
        //    hasta = grp_inspector.HASTA,
        //    deuda_determinada = grp_inspector.DEUDATOTAL,
        //    inspector = a.INSPECTOR
        //};
        //}).GroupBy(x=>x.inspector);

        //foreach (var item in inf)
        //{
        //    foreach (var actas_ in item)
        //    {
        //        actas_.ACTA
        //    }
        //}
        //dgv_1.DataSource = in;
      }
    }

    private void btn_ingreso_manual_Click(object sender, EventArgs e)
    {
      get_informe_inspector_porMes();
    }
  }
}

