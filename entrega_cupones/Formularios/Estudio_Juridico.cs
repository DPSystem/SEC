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
using System.Globalization;


namespace entrega_cupones.Formularios
{
  public partial class Estudio_Juridico : Form
  {
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

    public Estudio_Juridico()
    {
      InitializeComponent();
    }

    private void btn_cerrar_Click(object sender, EventArgs e)
    {
      btn_cerrar.Enabled = false;
      Close();
    }

    private void Estudio_Juridico_Load(object sender, EventArgs e)
    {
      try
      {
        Buscadores buscar = new Buscadores();
        cargar_cbx_inspectores();
        var a = buscar.get_actas_involucradas(lbl_cuit.Text);
        dgv_actas_estudio.DataSource = a.ToList();
      }
      catch (Exception ex)
      {
        MessageBox.Show("" + ex.Message);
        throw;
      }

    }

    private void cargar_cbx_inspectores()
    {
      try
      {
        Buscadores buscar = new Buscadores();
        cbx_estudios.DisplayMember = "apellido";
        cbx_estudios.ValueMember = "id_inspector";
        cbx_estudios.DataSource = buscar.get_inspectores().Where(x => x.esEstudio == 1).OrderBy(x => x.apellido).ToList();
      }
      catch (Exception ex)
      {
        MessageBox.Show("" + ex.Message);
        throw;
      }
      //Buscadores buscar = new Buscadores();
      //cbx_estudios.DisplayMember = "apellido";
      //cbx_estudios.ValueMember = "id_inspector";
      //cbx_estudios.DataSource = buscar.get_inspectores().Where(x=>x.esEstudio == 1).OrderBy(x => x.apellido).ToList();
    }

    private void btn_imprimir_notificacion_Click(object sender, EventArgs e)
    {
      Func_Utiles func_Utiles = new Func_Utiles();
      func_Utiles.limpiar_tabla_impresion();
      num2words num_a_letras = new num2words();

      using (var context = new lts_sindicatoDataContext())
      {
        var imp = from a in context.impresion_actas select a;
        string actas = string.Empty;
        foreach (DataGridViewRow fila in dgv_actas_estudio.Rows)
        {
          if (Convert.ToBoolean(fila.Cells["Imput"].Value))
          {
            impresion_comprobante im = new impresion_comprobante();
            im.empresa = lbl_razon_social.Text;
            im.cuit = lbl_cuit.Text;
            im.domicilio = lbl_domicilio.Text;
            im.localidad = lbl_localidad.Text;
            im.COL1EMPRESA = lbl_codigo_postal.Text; //Codigo postal de la empresa
            im.nro_socio = Convert.ToInt16(fila.Cells["num_acta"].Value); //Numero de ACTA
            im.aporte_ley = Convert.ToDecimal(fila.Cells["importe_act"].Value); //Valor del acta actualizada
            im.aporte_socio = Convert.ToDecimal(lbl_total_deuda.Text);//valor total de la deuda actualizada
            im.COL1NOMBRE = num_a_letras.enletras(lbl_total_deuda.Text); // valor en letras de la deuda actualizada
            im.COL2EMPRESA = lbl_actas.Text; // Actas involucradas
            im.COL2NOMBRE = fila.Cells["num_acta"].Value.ToString() + cbx_estudios.SelectedValue.ToString(); // numero de Notifiacion de deuda. cmpuesta por NºACTA + codigo de Inspectoro estudio juridico
            im.COL2NROSOCIO = dtp_vencimiento.Value.Date.ToShortDateString(); // fecha de vencimiento de la certificacion de deuda
                                                                              //im.telefono = lbl_total_fojas.Text; // cantidad de fojas que compone la nota
            context.impresion_comprobante.InsertOnSubmit(im);
          }
        }
        context.SubmitChanges();
      }

      reportes frm_reportes = new reportes();
      frm_reportes.nombreReporte = "certificado_de_deuda";
      frm_reportes.Show();
    }

    private void sumar_importes()
    {
      decimal total_deuda = 0;
      lbl_actas.Text = "";

      foreach (DataGridViewRow fila in dgv_actas_estudio.Rows)
      {
        if (Convert.ToBoolean(fila.Cells["Imput"].Value))
        {
          total_deuda = total_deuda + Convert.ToDecimal(fila.Cells["importe_act"].Value);
          if (lbl_actas.Text == "")
          {
            lbl_actas.Text = fila.Cells["num_acta"].Value.ToString();
          }
          else
          {
            lbl_actas.Text = lbl_actas.Text + " - " + fila.Cells["num_acta"].Value.ToString();
          }

          double dias = 0;
          double meses = 0;
          double coeficiente = 0;
          double actualizado = 0;
          dias = (dtp_vencimiento.Value - Convert.ToDateTime(fila.Cells["vencimiento"].Value)).TotalDays;
          meses = Math.Round((dias / (365.25 / 12)), 5); // formula que obtiene la cantidad de meses con los dias exactos hasta la fecha de vencimiento de notif de deuda
          coeficiente = ((meses * 3) * 0.01) + 1;// cantidad de meses x 3% mensual a ese resultado lo pultilico po 0.01 para obtener el porciento y le sumo  1
          actualizado = Convert.ToDouble(fila.Cells["importe_acta"].Value) * coeficiente; //importe_acta
          fila.Cells["importe_act"].Value = actualizado;
        }
      }
      lbl_total_deuda.Text = total_deuda.ToString("N2");
    }



    private void dgv_actas_estudio_CurrentCellDirtyStateChanged(object sender, EventArgs e)
    {
      // Esta seccion de codigo controla que los check box se actualizen.

      if (dgv_actas_estudio.IsCurrentCellDirty)
      {
        dgv_actas_estudio.CommitEdit(DataGridViewDataErrorContexts.Commit);
      }
    }

    private void dgv_actas_estudio_CellEndEdit(object sender, DataGridViewCellEventArgs e)
    {
      sumar_importes();
    }

    private void btn_asignar_Click(object sender, EventArgs e)
    {
      asignar_acta_estudio();
    }

    private void asignar_acta_estudio()
    {
      using (lts_sindicatoDataContext context = new lts_sindicatoDataContext())
      {
        foreach (DataGridViewRow fila in dgv_actas_estudio.Rows)
        {
          if (Convert.ToBoolean(fila.Cells["Imput"].Value))
          {
            var acta = (from a in context.ACTAS.Where(x => x.ACTA == Convert.ToDouble(fila.Cells["num_acta"].Value)) select a).FirstOrDefault();
            acta.ESTUDIO_INSPECTOR = Convert.ToInt16(cbx_estudios.SelectedValue);
            acta.ESTUDIO_JURIDICO = 1;
            acta.NUM_CERTIF = acta.ACTA + cbx_estudios.SelectedValue.ToString();
            acta.MONTO_CERTIF = Convert.ToDecimal(lbl_total_deuda.Text);
            acta.MONTO_CERTIF_ACTA = Convert.ToDecimal(fila.Cells["importe_act"].Value);
            acta.INSPECTOR_ANTERIOR = acta.INSPECTOR;
            acta.INSPECTOR = cbx_estudios.Text;
            acta.FECHA_ASIG_ANTERIOR = acta.FECHA_ASIG;
            acta.FECHA_ASIG = DateTime.Now.Date;
            context.SubmitChanges();
            MessageBox.Show("Deuda asignada con exito.");
            Close();
          }
        }
      }
    }

    private void dgv_actas_estudio_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {

    }
  }
}