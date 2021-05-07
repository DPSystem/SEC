using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
//using CrystalDecisions.CrystalReports.Engine;
using entrega_cupones.Clases;
using Microsoft.Reporting.WebForms;

namespace entrega_cupones.Formularios
{
  public partial class frm_Eventos2 : Form
  {

    #region Efecto Shadow

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

    public int _UsuarioId; // var que almacena el Id del usuario
    public double _cuil; // Variable global que almacena el cuil que viene del form de busqueda
    public bool _EsSocio; // Variable que indica si el empleado es socio o no.
    public decimal _Total; // lleva el total del comprobante.
    public bool _ImprimirSocio;
    public bool _ImprimirAcompañante;
    public bool _ImprimirNoSocio;
    public bool _ImprimirEntradaSinCargo;
    public int _CajaId; // lleva el Id d ela caja abierta por el usuario.
    public bool _ReimprimirEntradaAcompañante; // Variale que indica si la entrada de acompañante esta disponible para reimprimir
    public bool _ReimprimirEntradaSocio;// Variale que indica si la entrada del Socio esta disponible para reimprimir
    public bool _ReimprimirEntradaNoSocio; // Variale que indica si la entrada de NO SOCIO esta disponible para reimprimir

    public int _NumeroDeEntradaSocio;
    public int _NumeroDeComprobanteAcompañante;
    public int _NumeroDeComprobanteNoSocio;

    entrega_cupones.Clases.eventos evnt = new entrega_cupones.Clases.eventos();

    public frm_Eventos2()
    {
      InitializeComponent();
    }

    private void frm_Eventos2_Load(object sender, EventArgs e)
    {
      cbx_CantidadEntradaAcompañante.SelectedIndex = 0;
      cargar_combo_eventos();
      MostrarDatosDeSocio();
      ControlarSiEsSocio();
      ConsultaDeCaja();
      ControlarSiEstaImpreso();
      ControlarAperturaDeCaja();
      TotalDeEntradasEntregadas();
      if (_UsuarioId == 14 || _UsuarioId == 4 || _UsuarioId == 3)
      {
        btn_ImprimirSinCargo.Visible = true;
        lbl_TotalRecaudacion.Visible = true;
        txt_TotalRecaudacion.Visible = true;
        btn_reimprimir.Enabled = true;
      }
      else
      {
        btn_ImprimirSinCargo.Visible = false;
        lbl_TotalRecaudacion.Visible = false;
        txt_TotalRecaudacion.Visible = false;
        btn_reimprimir.Enabled = false;

      }

    }

    private void ControlarSiEsSocio()
    {
      if (_EsSocio)
      {
        //btn_ImprimirEntradaSocio.Text = "Imprimir Entrada Socio";
        lbl_Titulo.Text = "Eventos - Emision de Entradas Para Socios";
        chk_ImprimeSocio.Checked = true;

      }
      else
      {
        //btn_ImprimirEntradaSocio.Text = "Imprimir Entrada NO Socio";
        lbl_Titulo.Text = "Eventos - Emision de Entradas Para NO Socios";
        chk_ImprimeSocio.Text = "Entrada para NO Socio";
      }
    }

    private void ControlarAperturaDeCaja()
    {
      Caja caja = new Caja();
      _CajaId = caja.ControlarAperturaDeCaja(_UsuarioId);
      if (_CajaId > 0)
      {
        btn_AbrirCaja.Enabled = false;
        btn_CerrarCaja.Enabled = true;
        btn_ConsultarCaja.Enabled = true;
      }
      else
      {
        MessageBox.Show("Para poder entregar entradas debera reliazar una ''Apertura de Caja''");
        btn_AbrirCaja.Enabled = true;
        btn_ImprimirEntrada.Enabled = false;
        btn_ConsultarCaja.Enabled = false;
      }
    }

    private void cargar_combo_eventos()
    {
      cbx_eventos.DisplayMember = "eventos_nombre";
      cbx_eventos.ValueMember = "eventos_id";
      cbx_eventos.DataSource = evnt.get_todos();
    }

    private void MostrarDatosDeSocio()
    {
      socios GetDatosSocio = new socios();
      convertir_imagen ConvertirImagen = new convertir_imagen();
      var DatosSocio = GetDatosSocio.get_datos_socio(_cuil,0);

      txt_NroSocio.Text = DatosSocio.nrosocio;
      txt_Nombre.Text = DatosSocio.apellido.Trim() + " " + DatosSocio.nombre.Trim();
      txt_Dni.Text = DatosSocio.dni.Trim();
      txt_Empresa.Text = DatosSocio.empresa.Trim();

      var foto = GetDatosSocio.get_foto_titular_binary(_cuil);
      picbox_socio.Image = ConvertirImagen.ByteArrayToImage(foto.ToArray());
    }

    private void btn_cerrar_Click(object sender, EventArgs e)
    {
      Close();
    }

    private void btn_minimizar_Click(object sender, EventArgs e)
    {
      this.WindowState = FormWindowState.Minimized;
    }

    private void ImprimirEntradaSocio()
    {
      EventosCupones InsertarEntradaSocio = new EventosCupones();
      int EventoId = Convert.ToInt32(cbx_eventos.SelectedValue);
      int NumeroDeEntrada = InsertarEntradaSocio.InsertarEntradaSocio(EventoId, _cuil, _UsuarioId);

      LlenarDtEntradaDDEDC(NumeroDeEntrada, 0);
    }

    private void ImprimirEntradaInvitadoNoSocio(bool EsAcompañante, int NumeroComprobante)
    {
      EventosCupones InsertarEntradaInvitado = new EventosCupones();
      int EventoId = Convert.ToInt32(cbx_eventos.SelectedValue);
      int NumeroDeComprobante = InsertarEntradaInvitado.InsertarEntradaInvitadoNOSocio(EventoId, _cuil, _UsuarioId, EsAcompañante, NumeroComprobante);

      LlenarDtEntradaDDEDC(0, NumeroDeComprobante);

    }

    private void ImprimirEntradaSinCargo(int NumeroComprobante)
    {
      EventosCupones InsertarEntradaSinCargo = new EventosCupones();
      int EventoId = Convert.ToInt32(cbx_eventos.SelectedValue);
      int NumeroDeComprobante = InsertarEntradaSinCargo.InsertarEntradaSinCargo(EventoId, _cuil, _UsuarioId, NumeroComprobante);

      ImprimirSinCargo();

    }

    private void ConsultaDeCaja()
    {
      EventosCupones EvntCpn = new EventosCupones();
      int EventoId = Convert.ToInt32(cbx_eventos.SelectedValue);
      txt_Caja.Text = EvntCpn.ConsultaDeCaja(_UsuarioId, EventoId).ToString();
    }

    private void ControlarSiEstaImpreso()
    {
      EventosCupones ControlEstaImpreso = new EventosCupones();
      int EventoId = Convert.ToInt32(cbx_eventos.SelectedValue);

      var EntradasImpresas = ControlEstaImpreso.ControlarEntradasImpresas(EventoId, _cuil);

      //asigno valores a las varibles publicas que despues me van indicar si puedo imprimir los distintos casos
      _ReimprimirEntradaAcompañante = false;
      _ReimprimirEntradaNoSocio = false;
      _ReimprimirEntradaSocio = false;
      _ImprimirAcompañante = true;
      if (_EsSocio)
      {
        _ImprimirSocio = true;
        _ImprimirNoSocio = false;
      }
      else
      {
        _ImprimirSocio = false;
        _ImprimirNoSocio = true;
      }


      if (EntradasImpresas.Count() > 0)
      {

        foreach (var item in EntradasImpresas.ToList())
        {
          if (item.EsAcompañante == true)
          {
            //chk_ImprimeAcompañante.Enabled = false;
            //cbx_CantidadEntradaAcompañante.Enabled = false;
            //_ImprimirAcompañante = false; // entrada de acompañante ya impresa. no imprimir,
            _ReimprimirEntradaAcompañante = true;
            _NumeroDeComprobanteAcompañante = item.NumeroDeComprobante;
          }
          else
          {
            //chk_ImprimeSocio.Enabled = false;
            if (item.NumeroDeEntrada == 0) // si no es acompañante entonces ver si es Socio o No Socio
            {
              _ImprimirNoSocio = false;
              _ReimprimirEntradaNoSocio = true;
              _NumeroDeComprobanteNoSocio = item.NumeroDeComprobante;
              chk_ImprimeSocio.Checked = false;
            }
            else
            {
              _ImprimirSocio = false;
              _ReimprimirEntradaSocio = true;
              _NumeroDeEntradaSocio = item.NumeroDeEntrada;
              chk_ImprimeSocio.Enabled = false;
            }
          }
          if (item.SinCargo)
          {
            _ImprimirEntradaSinCargo = true;
            chk_EntradaSinCargo.Enabled = false;
            btn_ImprimirSinCargo.Enabled = false;
          }
          else
          {
            _ImprimirEntradaSinCargo = false;
            chk_EntradaSinCargo.Enabled = true;
            btn_ImprimirSinCargo.Enabled = true;
          }
        }
        //btn_ImprimirEntrada.Enabled = (EntradasImpresas.Count() >= 2) ? false : true;

      }

    }

    private void chk_ImprimeSocio_CheckedChanged(object sender, EventArgs e)
    {
      if (chk_ImprimeSocio.Checked == true) // si chequeado == true
      {
        if (_EsSocio == false) //si NO es Socio
        {
          _Total = _Total + 100;
        }
      }
      else //Lo deschequeo y pregunto si es socios
      {
        if (_EsSocio == false) //SI NO es Socio
        {
          if (_Total > 0)
          {
            _Total = _Total - 100;
          }

        }
      }
      txt_TotalComprobante.Text = _Total.ToString();

    }

    private void chk_ImprimeAcompañante_CheckedChanged(object sender, EventArgs e)
    {
      CalcularCostoAcompañante();
    }

    private void CalcularCostoAcompañante()
    {
      if (chk_ImprimeAcompañante.Checked == true)
      {
        cbx_CantidadEntradaAcompañante.Enabled = true;
        _Total = (100 * Convert.ToInt16(cbx_CantidadEntradaAcompañante.SelectedItem));
        if (_EsSocio == false && chk_ImprimeSocio.Checked == true)
        {
          _Total = _Total + 100;
        }
      }
      else
      {
        if (_Total > 0)
        {
          _Total = _Total - (100 * Convert.ToInt16(cbx_CantidadEntradaAcompañante.SelectedItem));
        }
        cbx_CantidadEntradaAcompañante.Enabled = false;
      }
      txt_TotalComprobante.Text = _Total.ToString();
    }

    private void btn_ImprimirEntrada_Click_1(object sender, EventArgs e)
    {

      int NumeroComprobante = 0;
      //chequeo si hay que generar un numero de comprobante
      if ((_ImprimirAcompañante == true && chk_ImprimeAcompañante.Checked == true) ||
        (_ImprimirNoSocio == true && chk_ImprimeSocio.Checked == true))
      {
        NumeroComprobante = GenerarNumeroDeComprobante();
      }

      if (_ImprimirSocio)
      {
        if (chk_ImprimeSocio.Checked == true)
        {
          ImprimirEntradaSocio();
        }
      }

      if (_ImprimirNoSocio)
      {
        if (chk_ImprimeSocio.Checked == true)
        {
          ImprimirEntradaInvitadoNoSocio(false, NumeroComprobante);
        }
      }

      if (_ImprimirAcompañante)
      {
        if (chk_ImprimeAcompañante.Checked == true)
        {
          for (int i = 1; i <= Convert.ToInt16(cbx_CantidadEntradaAcompañante.SelectedItem); i++)
          {
            ImprimirEntradaInvitadoNoSocio(true, NumeroComprobante);
          }
        }
      }

      if (_ImprimirEntradaSinCargo)
      {
        if (chk_EntradaSinCargo.Checked == true)
        {
          ImprimirEntradaSinCargo(NumeroComprobante);
        }
      }

      ControlarSiEstaImpreso();
      ConsultaDeCaja();
      TotalDeEntradasEntregadas();
    }

    private int GenerarNumeroDeComprobante()
    {
      int NumeroComprobante = 0;

      using (var context = new lts_sindicatoDataContext())
      {
        if (context.Comprobantes.Count() > 0)
        {
          NumeroComprobante = Convert.ToInt32(context.Comprobantes.Max(x => x.Numero) + 1);
        }
        else
        {
          NumeroComprobante = 1;
        }
        Comprobantes InsertarComprobante = new Comprobantes();
        InsertarComprobante.Numero = NumeroComprobante;
        InsertarComprobante.Importe = Convert.ToDecimal(txt_TotalComprobante.Text);
        context.Comprobantes.InsertOnSubmit(InsertarComprobante);
        context.SubmitChanges();
        return NumeroComprobante;
      }
    }

    private void btn_AbrirCaja_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show("Esta seguro de Realizar la apertura de caja", "¡¡¡ ATENCION !!!", MessageBoxButtons.YesNo) == DialogResult.Yes)
      {
        Caja caja = new Caja();
        caja.AbrirCaja(_UsuarioId);
        MessageBox.Show("Caja Abierta con exito.!!!", "INFORMACION");
        btn_ImprimirEntrada.Enabled = true;
      }
    }

    private void btn_CerrarCaja_Click(object sender, EventArgs e)
    {

      if (MessageBox.Show("Esta seguro de Cerra la caja por el monto de  " + txt_Caja.Text, "¡¡¡ ATENCION !!!", MessageBoxButtons.YesNo) == DialogResult.Yes)
      {
        int EventoId = Convert.ToInt32(cbx_eventos.SelectedValue);
        Caja caja = new Caja();
        caja.CerrarCaja(_CajaId, _UsuarioId, EventoId);
        MessageBox.Show("Caja Cerrada con exito.!!!", "INFORMACION");
      }
    }

    private void btn_ConsultarCaja_Click(object sender, EventArgs e)
    {
      int EventoId = Convert.ToInt32(cbx_eventos.SelectedValue);
      Caja caja = new Caja();
      var DetalleCaja = caja.ConsultaDetalleDeCaja(_UsuarioId, EventoId);

      DS_cupones Ds = new DS_cupones();
      DataTable Dt = Ds.Cajas;
      Dt.Clear();

      if (DetalleCaja.Count() > 0)
      {
        foreach (var item in DetalleCaja)
        {
          DataRow Dr = Dt.NewRow();
          Dr["FechaApertura"] = item.Hora;
          Dr["NroDeComprobante"] = item.NmeroDeComprobante;
          Dr["Monto"] = Convert.ToDecimal(item.ValorComprobante);
          Dt.Rows.Add(Dr);
        }
      }

      reportes frm_reportes = new reportes();
      frm_reportes.DtCajas = Dt;
      frm_reportes.nombreReporte = "rpt_ConsultaDeCaja";
      frm_reportes.Show();
    }

    private void btn_reimprimir_Click(object sender, EventArgs e)
    {
      pnl_Reimprimir.Visible = true;

      btn_ReimprimirSocio.Enabled = (_ReimprimirEntradaSocio);

      btn_ReimprimirNoSocio.Enabled = (_ReimprimirEntradaNoSocio);

      btn_ReimprimirAcompañanante.Enabled = (_ReimprimirEntradaAcompañante);
     
    }

    public void LlenarDtEntradaDDEDC(int NumeroDeEntrada, int NumeroDeComprobante)
    {
      DS_cupones Ds = new DS_cupones();
      DataTable Dt = Ds.EntradaDDEDC;
      Dt.Clear();
      DataRow Dr = Dt.NewRow();

      if (_EsSocio)
      {
        convertir_imagen ConvertirImagen = new convertir_imagen();
        Dr["Nombre"] = txt_Nombre.Text;
        Dr["DNI"] = txt_Dni.Text;
        Dr["Empresa"] = txt_Empresa.Text;
        Dr["NumeroDeSocio"] = txt_NroSocio.Text;
        Dr["NumeroDeEntrada"] = NumeroDeEntrada;
        Dr["NumeroDeRecibo"] = NumeroDeComprobante;
        Dr["EsInvitado"] = "NO";
        Dr["Foto"] = ConvertirImagen.ImageToByteArray(picbox_socio.Image);
        Dr["Reimpresion"] = "0";
      }
      else
      {
        Func_Utiles fu = new Func_Utiles();
        Dr["NumeroDeRecibo"] = fu.generar_ceros(NumeroDeComprobante.ToString(), 5);
        Dr["DNI"] = fu.generar_ceros(txt_Dni.Text, 10);
      }
      Dt.Rows.Add(Dr);

      string ReporteAMostrar = string.Empty;

      if (_EsSocio)
      {
        if (NumeroDeEntrada > 0) //pregunto pro la entrada = a cero por que entonces Es socio
        {
          //frm_reportes.nombreReporte = "rpt_EntradaSocioDDEDC";
          ReporteAMostrar = "entrega_cupones.Reportes.rpt_EntradaSocioDDEDC.rdlc";
        }
        else
        {
          //frm_reportes.nombreReporte = "rpt_EntradaInvitadoDDEDC";
          ReporteAMostrar = "entrega_cupones.Reportes.rpt_EntradaInvitadoDDEDC.rdlc";
        }
      }
      else
      {
        //frm_reportes.nombreReporte = "rpt_EntradaInvitadoDDEDC";
        ReporteAMostrar = "entrega_cupones.Reportes.rpt_EntradaInvitadoDDEDC.rdlc";
      }


      try
      {   //Instanciamos un LocalReport, le indicamos el report a imprimir y le cargamos los datos
        LocalReport rdlc = new LocalReport();
        rdlc.ReportEmbeddedResource = ReporteAMostrar;
        rdlc.DataSources.Add(new ReportDataSource("DataSet1", Dt));
        //Imprime el report
        Impresor imp = new entrega_cupones.Impresor();

        imp.Imprime(rdlc);
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }

    }

    private void cbx_CantidadEntradaAcompañante_SelectedIndexChanged(object sender, EventArgs e)
    {
      CalcularCostoAcompañante();
    }

    private void chk_EntradaSinCargo_CheckedChanged(object sender, EventArgs e)
    {
      if (chk_EntradaSinCargo.Checked == true)
      {
        _ImprimirEntradaSinCargo = true;
      }
      else
      {
        _ImprimirEntradaSinCargo = false;
      }
    }

    private void btn_ImprimirSinCargo_Click(object sender, EventArgs e)
    {
      ImprimirEntradaSinCargo(0);
    }

    private void ImprimirSinCargo()
    {
      Func_Utiles fu = new Func_Utiles();

      DS_cupones Ds = new DS_cupones();
      DataTable Dt = Ds.EntradaDDEDC;
      Dt.Clear();
      DataRow Dr = Dt.NewRow();

      Dr["NumeroDeEntrada"] = 0;
      Dr["NumeroDeRecibo"] = 0;
      Dr["EsInvitado"] = "NO";
      Dr["NumeroDeRecibo"] = fu.generar_ceros("0", 3);
      Dr["DNI"] = fu.generar_ceros(txt_Dni.Text, 10);
      Dr["SinCargo"] = "SI";

      Dt.Rows.Add(Dr);

      string ReporteAMostrar = "entrega_cupones.Reportes.rpt_EntradaInvitadoDDEDC.rdlc";


      try
      {   //Instanciamos un LocalReport, le indicamos el report a imprimir y le cargamos los datos
        LocalReport rdlc = new LocalReport();
        rdlc.ReportEmbeddedResource = ReporteAMostrar;
        rdlc.DataSources.Add(new ReportDataSource("DataSet1", Dt));
        //Imprime el report
        Impresor imp = new entrega_cupones.Impresor();

        imp.Imprime(rdlc);
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
      ControlarSiEstaImpreso();
      ConsultaDeCaja();
    }

    private void TotalDeEntradasEntregadas()
    {
      using (var context = new lts_sindicatoDataContext())
      {
        int EventoId = Convert.ToInt32(cbx_eventos.SelectedValue);
        var total = context.eventos_cupones.Where(x => x.eventcupon_evento_id == EventoId);

        txt_Socio.Text = total.Where(x => x.event_cupon_nro > 0).Count().ToString();
        txt_Invitados.Text = total.Where(x => x.Invitado == 1).Count().ToString(); // total.Select(x => x.Invitado == 1).Count().ToString();
        txt_NoSocio.Text = total.Where(x => x.Invitado == 0 && x.event_cupon_nro == 0).Count().ToString();//total.Select(x => x.Invitado == 0 && x.event_cupon_nro == 0).Count().ToString();
        txt_SinCargo.Text = total.Where(x => x.Invitado == 2).Count().ToString();
        txt_total.Text = total.Count().ToString();
        txt_TotalRecaudacion.Text = context.Comprobantes.Sum(x => x.Importe).ToString();
      }
    }

    private void btn_SalirDeReimpresion_Click(object sender, EventArgs e)
    {
      pnl_Reimprimir.Visible = false;
    }

    private void btn_ReimprimirSocio_Click(object sender, EventArgs e)
    {
      if (_ReimprimirEntradaSocio)
      {
        LlenarDataSetReImpresion(0, 1);
        pnl_Reimprimir.Visible = false;
      }
    }

    private void btn_ReimprimirNoSocio_Click(object sender, EventArgs e)
    {

      if (_ReimprimirEntradaNoSocio)
      {
        LlenarDataSetReImpresion(_NumeroDeComprobanteNoSocio, 2);
        pnl_Reimprimir.Visible = false;
      }
    }

    private void btn_ReimprimirAcompañanante_Click(object sender, EventArgs e)
    {
      if (_ReimprimirEntradaAcompañante)
      {
        LlenarDataSetReImpresion(_NumeroDeComprobanteAcompañante, 3);
        pnl_Reimprimir.Visible = false;
      }
    }

    private void LlenarDataSetReImpresion(int NumeroDeComprobante, int BotonPresionado)
    {
      DS_cupones Ds = new DS_cupones();
      DataTable Dt = Ds.EntradaDDEDC;
      Dt.Clear();
      DataRow Dr = Dt.NewRow();

      string ReporteAMostrar = string.Empty;

      if (BotonPresionado == 1)
      {
        convertir_imagen ConvertirImagen = new convertir_imagen();
        Dr["Nombre"] = txt_Nombre.Text;
        Dr["DNI"] = txt_Dni.Text;
        Dr["Empresa"] = txt_Empresa.Text;
        Dr["NumeroDeSocio"] = txt_NroSocio.Text;
        Dr["NumeroDeEntrada"] = _NumeroDeEntradaSocio;
        Dr["NumeroDeRecibo"] = 0;
        Dr["EsInvitado"] = "NO";
        Dr["Foto"] = ConvertirImagen.ImageToByteArray(picbox_socio.Image);
        Dr["Reimpresion"] = "1";
        ReporteAMostrar = "entrega_cupones.Reportes.rpt_EntradaSocioDDEDC.rdlc";
      }

      if (BotonPresionado == 2 || BotonPresionado == 3)
      {
        Func_Utiles fu = new Func_Utiles();
        Dr["NumeroDeRecibo"] = fu.generar_ceros(NumeroDeComprobante.ToString(), 5);
        Dr["DNI"] = fu.generar_ceros(txt_Dni.Text, 10);
        Dr["Reimpresion"] = "1";
        ReporteAMostrar = "entrega_cupones.Reportes.rpt_EntradaInvitadoDDEDC.rdlc";
      }

      Dt.Rows.Add(Dr);

      try
      {   //Instanciamos un LocalReport, le indicamos el report a imprimir y le cargamos los datos
        LocalReport rdlc = new LocalReport();
        rdlc.ReportEmbeddedResource = ReporteAMostrar;
        rdlc.DataSources.Add(new ReportDataSource("DataSet1", Dt));
        //Imprime el report
        Impresor imp = new entrega_cupones.Impresor();

        imp.Imprime(rdlc);
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }

    }
  }
}


