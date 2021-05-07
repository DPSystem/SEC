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
  public partial class frm_eventos : Form
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

    public double _cuil; // Variable global que almacena el cuil que viene del form de busqueda
    public int UsuarioID = 0;
    Clases.eventos evnt = new Clases.eventos();

    public frm_eventos()
    {
      InitializeComponent();
    }

    private void btn_cerrar_Click(object sender, EventArgs e)
    {
      btn_cerrar.Enabled = false;
      Close();
    }

    private void frm_eventos_Load(object sender, EventArgs e)
    {
      if (UsuarioID == 19 ||
        UsuarioID == 18 ||
        UsuarioID == 14 ||
        UsuarioID == 3 ||
        UsuarioID == 4)
      {
        //btn_cargar_exepcion.Enabled = true;
        gbx_exepciones.Enabled = true;
        btn_reimprimir.Enabled = true;
        gbx_exepciones.Visible = true;
      }
      else
      {
        gbx_exepciones.Enabled = false;
        btn_reimprimir.Enabled = false;
        gbx_exepciones.Visible = false;
        //btn_cargar_exepcion.Enabled = false;
      }

      cbx_sexo.SelectedIndex = 1;
      dgv_titu_benef.AutoGenerateColumns = false;
      cargar_combo_eventos();
      Cargar_TituBenef();
      Cargar_cbx_parentesco();
      BloquearFilaPorEdad();
      ObtenerTotalesEntregados();
    }

    private void BloquearFilaPorEdad()
    {
      EventosCupones EvntCpn = new EventosCupones();
      foreach (DataGridViewRow fila in dgv_titu_benef.Rows)
      {
        int edad = Convert.ToInt32(fila.Cells["Edad"].Value);
        if (edad > 12)
        {
          fila.ReadOnly = true;
        }
        else
        {
          if (Convert.ToInt32(fila.Cells["Exepcion"].Value) == 1)
          {
            if (EvntCpn.ExisteExepcion(Convert.ToInt32(fila.Cells["ExepcionID"].Value))) // si Exepcion ya emitio cupon
            {
              fila.Cells["Estado"].Value = "Emitido";
              fila.ReadOnly = true;
            }
          }
          else
          {
            if (EvntCpn.ExisteFamiliar(Convert.ToInt32(fila.Cells["CodigoFliar"].Value)))
            {
              fila.Cells["Estado"].Value = "Emitido [Nº: " + Convert.ToString(EvntCpn.GetNumeroCupon(Convert.ToInt32(fila.Cells["CodigoFliar"].Value))) + "]";
              fila.ReadOnly = true;
            }
          }
        }
      }
    }

    private void Cargar_cbx_parentesco()
    {
      Clases.Parentesco parent = new Clases.Parentesco();
      cbx_parentesco.DisplayMember = "parent_descrip";
      cbx_parentesco.ValueMember = "parent_id";
      cbx_parentesco.DataSource = parent.GetParentescoTodos();
    }

    private void cargar_combo_eventos()
    {
      cbx_eventos.DisplayMember = "eventos_nombre";
      cbx_eventos.ValueMember = "eventos_id";
      cbx_eventos.DataSource = evnt.get_todos();
    }

    private void Cargar_TituBenef()
    {
      socios soc = new socios();
      foreach (var item in soc.Get_Titular_Benef(_cuil, Convert.ToInt32(cbx_eventos.SelectedValue)))
      {
        dgv_titu_benef.Rows.Add();
        int fila = dgv_titu_benef.Rows.Count - 1;
        dgv_titu_benef.Rows[fila].Cells["nombre"].Value = item.nombre;
        dgv_titu_benef.Rows[fila].Cells["Parentesco"].Value = item.Parentesco;
        dgv_titu_benef.Rows[fila].Cells["CodigoFliar"].Value = item.CodigoFliar;
        dgv_titu_benef.Rows[fila].Cells["dni"].Value = item.Cuil;
        dgv_titu_benef.Rows[fila].Cells["sexo"].Value = item.Sexo;
        dgv_titu_benef.Rows[fila].Cells["Edad"].Value = item.Edad;
        dgv_titu_benef.Rows[fila].Cells["Exepcion"].Value = 0;
        dgv_titu_benef.Rows[fila].Cells["Emitir"].Value = Properties.Resources.impresora_PNG_24; //D:\Proyectos\entrega_cupones\entrega_cupones\Resources\impresora (1).png;

      }
      cargar_exepciones();
    }

    private void cargar_exepciones()
    {
      EventosExepciones evntexp = new EventosExepciones();
      Func_Utiles func_util = new Func_Utiles();
      Parentesco prnt = new Parentesco();

      foreach (var item in evntexp.GetListadoExepciones(_cuil))
      {
        dgv_titu_benef.Rows.Add();
        int fila = dgv_titu_benef.Rows.Count - 1;
        dgv_titu_benef.Rows[fila].Cells["nombre"].Value = item.EventExepApellido + " " + item.EventExepNombre;
        dgv_titu_benef.Rows[fila].Cells["Parentesco"].Value = prnt.GetParentescoDescrip(item.EventExepParent).parent_descrip;
        dgv_titu_benef.Rows[fila].Cells["CodigoFliar"].Value = 0;
        dgv_titu_benef.Rows[fila].Cells["dni"].Value = item.EventExepDni;
        dgv_titu_benef.Rows[fila].Cells["sexo"].Value = item.EventExpSexo;
        dgv_titu_benef.Rows[fila].Cells["Edad"].Value = func_util.calcular_edad(item.EventFechaNac);
        dgv_titu_benef.Rows[fila].Cells["Exepcion"].Value = 1;
        dgv_titu_benef.Rows[fila].Cells["ExepcionID"].Value = item.EventExepId;
        dgv_titu_benef.Rows[fila].Cells["Emitir"].Value = Properties.Resources.impresora_PNG_24; //D:\Proyectos\entrega_cupones\entrega_cupones\Resources\impresora (1).png;
      }
    }

    private void dgv_titu_benef_SelectionChanged(object sender, EventArgs e)
    {
      socios soc = new socios();
      convertir_imagen img = new convertir_imagen();

      double Codigo_Fliar;

      //dgv_jugadores_inscriptos.CurrentRow.Cells["jugcuil"].Value
      Codigo_Fliar = Convert.ToDouble(dgv_titu_benef.CurrentRow.Cells["CodigoFliar"].Value);

      if (Codigo_Fliar > 0)
      {
        picbox_socio.Image = img.ByteArrayToImage(soc.get_foto_benef_binary(Codigo_Fliar).ToArray());
      }
      else
      {
        if ((Codigo_Fliar == 0) && (Convert.ToInt32(dgv_titu_benef.CurrentRow.Cells["Exepcion"].Value) == 0)) // es por que es titular
        {
          picbox_socio.Image = img.ByteArrayToImage(soc.get_foto_titular_binary(_cuil).ToArray());

        }
        else // Es por que es Exepcion
        {
          picbox_socio.Image = img.ByteArrayToImage(soc.get_foto_benef_binary(1).ToArray());
        }

      }
    }

    private void dgv_titu_benef_CurrentCellDirtyStateChanged(object sender, EventArgs e)
    {
      // Esta seccion de codigo controla que los check box se actualizen.

      if (dgv_titu_benef.IsCurrentCellDirty)
      {
        dgv_titu_benef.CommitEdit(DataGridViewDataErrorContexts.Commit);
      }
    }

    private void btn_emitir_cupon_Click(object sender, EventArgs e)
    {
      //RecorrerDgvTituBenef();
      EmitirCupon_();
    }

    private void ObtenerTotalesEntregados()
    {
      EventosCupones EvntCpn = new EventosCupones();
      EventosExepciones EvntExp = new EventosExepciones();
      txt_total_cupones.Text = EvntCpn.GetCantidadCupones().ToString();
      txt_total_exepciones.Text = EvntExp.GetCantidadExepciones().ToString();
    }

    private void EmitirCupon_()
    {

      EventosCupones EvntCpn = new EventosCupones();
      DS_cupones ds = new DS_cupones();
      DataTable dt = ds.cupon_dia_niño;
      dt.Clear();
      socios soc = new socios();
      DataGridViewRow fila = dgv_titu_benef.CurrentRow;
      DataRow dr = dt.NewRow();
      var datos = soc.get_datos_socio(_cuil,0);
      int edad = Convert.ToInt32(fila.Cells["Edad"].Value);

      if (edad <= 12)
      {
        if (Convert.ToInt32(fila.Cells["Exepcion"].Value) == 1) // si es Exepcion
        {
          if (EvntCpn.ExisteExepcion(Convert.ToInt32(fila.Cells["ExepcionID"].Value))) // si Exepcion ya emitio cupon
          {
            MessageBox.Show("Ya se emitio cupon para Exepcion.");
          }
          else
          {
            EventosCupones evntcpn = new EventosCupones();
            int nrocupon = evntcpn.EventosCuponesInsertar(
              Convert.ToInt32(cbx_eventos.SelectedValue),
              Convert.ToDouble(fila.Cells["dni"].Value),
              Convert.ToInt32(fila.Cells["CodigoFliar"].Value),
              Convert.ToInt32(fila.Cells["ExepcionID"].Value),
              0,
              UsuarioID,
              string.Empty,
              0,
              datos.CuilStr,
              0
              );

            dr["titu_apenom"] = datos.apellido + " " + datos.nombre;
            dr["titu_dni"] = datos.dni;
            dr["titu_empresa"] = datos.empresa;
            dr["titu_nrosocio"] = datos.nrosocio;
            dr["titu_foto"] = soc.get_foto_titular_binary(_cuil).ToArray();
            dr["benef_apenom"] = fila.Cells["nombre"].Value;
            dr["benef_dni"] = fila.Cells["Dni"].Value;
            dr["benef_sexo"] = fila.Cells["sexo"].Value;
            dr["benef_edad"] = fila.Cells["Edad"].Value;
            dr["benef_foto"] = soc.get_foto_benef_binary(1).ToArray();
            dr["event_nrocupon"] = nrocupon;
            dr["event_fechaentrega"] = DateTime.Now;
            dr["event_cupon_ID"] = EvntCpn.GetCuponID();
            dt.Rows.Add(dr);
          }
        }
        else
        {
          if (EvntCpn.ExisteFamiliar(Convert.ToInt32(fila.Cells["CodigoFliar"].Value)))
          {
            MessageBox.Show("Ya se emitio cupon para Beneficiario.");
          }
          else
          {
            EventosCupones evntcpn = new EventosCupones();

            int nrocupon = evntcpn.EventosCuponesInsertar(
              Convert.ToInt32(cbx_eventos.SelectedValue),
              Convert.ToDouble(fila.Cells["dni"].Value),
              Convert.ToInt32(fila.Cells["CodigoFliar"].Value),
              Convert.ToInt32(fila.Cells["ExepcionID"].Value),
              0,
              UsuarioID,
              string.Empty,
              0,
              datos.CuilStr,
              0
              );

            dr["titu_apenom"] = datos.apellido + " " + datos.nombre;
            dr["titu_dni"] = datos.dni;
            dr["titu_empresa"] = datos.empresa;
            dr["titu_nrosocio"] = datos.nrosocio;
            dr["titu_foto"] = soc.get_foto_titular_binary(_cuil).ToArray();
            dr["benef_apenom"] = fila.Cells["nombre"].Value;
            dr["benef_dni"] = fila.Cells["Dni"].Value;
            dr["benef_sexo"] = fila.Cells["sexo"].Value;
            dr["benef_edad"] = fila.Cells["Edad"].Value;
            dr["benef_foto"] = soc.get_foto_benef_binary(Convert.ToInt32(fila.Cells["CodigoFliar"].Value)).ToArray();
            dr["event_nrocupon"] = nrocupon;
            dr["event_fechaentrega"] = DateTime.Now;
            dr["event_cupon_ID"] = EvntCpn.GetCuponID();
            dt.Rows.Add(dr);
          }
        }
        if (dt.Rows.Count > 0)
        {
          ImprimirCupones(dt);
          fila.Cells["Estado"].Value = "Emitido";
        }
      }
    }

    private void RecorrerDgvTituBenef()
    {
      EventosCupones EvntCpn = new EventosCupones();
      DS_cupones ds = new DS_cupones();
      DataTable dt = ds.cupon_dia_niño;
      dt.Clear();
      socios soc = new socios();
      foreach (DataGridViewRow fila in dgv_titu_benef.Rows)
      {
        if (Convert.ToBoolean(fila.Cells["EmitirCupon"].Value))
        {
          DataRow dr = dt.NewRow();
          var datos = soc.get_datos_socio(_cuil,0);
          if (Convert.ToInt32(fila.Cells["Exepcion"].Value) == 1) // si es Exepcion
          {
            if (EvntCpn.ExisteExepcion(Convert.ToInt32(fila.Cells["ExepcionID"].Value))) // si Exepcion ya emitio cupon
            {
              MessageBox.Show("Ya se emitio cupon para Exepcion.");
            }
            else
            {
              EventosCupones evntcpn = new EventosCupones();
              int nrocupon = evntcpn.EventosCuponesInsertar(
                Convert.ToInt32(cbx_eventos.SelectedValue),
                Convert.ToDouble(fila.Cells["dni"].Value),
                Convert.ToInt32(fila.Cells["CodigoFliar"].Value),
                Convert.ToInt32(fila.Cells["ExepcionID"].Value),
                0,
                UsuarioID,
                string.Empty,
                0,
                datos.CuilStr,
                0
                );


              dr["titu_apenom"] = datos.apellido + " " + datos.nombre;
              dr["titu_dni"] = datos.dni;
              dr["titu_empresa"] = datos.empresa;
              dr["titu_nrosocio"] = datos.nrosocio;
              dr["titu_foto"] = soc.get_foto_titular_binary(_cuil).ToArray();
              dr["benef_apenom"] = fila.Cells["nombre"].Value;
              dr["benef_dni"] = fila.Cells["Dni"].Value;
              dr["benef_sexo"] = fila.Cells["sexo"].Value;
              dr["benef_edad"] = fila.Cells["Edad"].Value;
              dr["benef_foto"] = soc.get_foto_benef_binary(1).ToArray();
              dr["event_nrocupon"] = nrocupon;
              dr["event_fechaentrega"] = DateTime.Now;
              dt.Rows.Add(dr);
            }
          }
          else
          {
            if (EvntCpn.ExisteFamiliar(Convert.ToInt32(fila.Cells["CodigoFliar"].Value)))
            {
              MessageBox.Show("Ya se emitio cupon para Beneficiario.");
            }
            else
            {
              EventosCupones evntcpn = new EventosCupones();

              int nrocupon = evntcpn.EventosCuponesInsertar(
                Convert.ToInt32(cbx_eventos.SelectedValue),
                Convert.ToDouble(fila.Cells["dni"].Value),
                Convert.ToInt32(fila.Cells["CodigoFliar"].Value),
                Convert.ToInt32(fila.Cells["ExepcionID"].Value),
                0,
                UsuarioID,
                string.Empty,
                0,
                datos.CuilStr,
                0
                );

              dr["titu_apenom"] = datos.apellido + " " + datos.nombre;
              dr["titu_dni"] = datos.dni;
              dr["titu_empresa"] = datos.empresa;
              dr["titu_nrosocio"] = datos.nrosocio;
              dr["titu_foto"] = soc.get_foto_titular_binary(_cuil).ToArray();
              dr["benef_apenom"] = fila.Cells["nombre"].Value;
              dr["benef_dni"] = fila.Cells["Dni"].Value;
              dr["benef_sexo"] = fila.Cells["sexo"].Value;
              dr["benef_edad"] = fila.Cells["Edad"].Value;
              dr["benef_foto"] = soc.get_foto_benef_binary(Convert.ToInt32(fila.Cells["CodigoFliar"].Value)).ToArray();
              dr["event_nrocupon"] = nrocupon;
              dr["event_fechaentrega"] = DateTime.Now;
              dt.Rows.Add(dr);

            }
          }
        }
      }
      if (dt.Rows.Count > 0)
      {
        ImprimirCupones(dt);
      }

    }

    private void ImprimirCupones(DataTable dt)
    {
      reportes frm_reportes = new reportes();
      frm_reportes.dtCupondiaDelNiño = dt;
      frm_reportes.nombreReporte = "rpt_CuponDiaDelNiño_2";
      //frm_reportes.nombreReporte = "rpt_CuponDiaDelNiño_TRH";
      frm_reportes.Show();
    }

    private void btn_cargar_exepcion_Click(object sender, EventArgs e)
    {
      EventosExepciones EventExep = new EventosExepciones();
      if (EventExep.GetExisteExepcion(txt_dni.Text.Trim()).EventExepDni != null)
      {
        MessageBox.Show("Ya se cargo la exepcion.!!!");
        txt_dni.Focus();
      }
      else
      {
        Clases.Parentesco prnt = new Clases.Parentesco();
        var insert = EventExep.InsertarExepciones(
          txt_apellido.Text.Trim(),
          txt_nombre.Text.Trim(), txt_dni.Text.Trim(),
          Convert.ToDateTime(msk_fecha_nac.Text),
          cbx_sexo.SelectedItem.ToString(),
          Convert.ToInt32(cbx_parentesco.SelectedValue),
          _cuil
          );

        dgv_titu_benef.Rows.Add();
        int fila = dgv_titu_benef.Rows.Count - 1;
        dgv_titu_benef.Rows[fila].Cells["nombre"].Value = insert.EventExepApellido + " " + insert.EventExepNombre;
        dgv_titu_benef.Rows[fila].Cells["Parentesco"].Value = prnt.GetParentescoDescrip(insert.EventExepParent).parent_descrip;
        dgv_titu_benef.Rows[fila].Cells["dni"].Value = insert.EventExepDni;
        dgv_titu_benef.Rows[fila].Cells["sexo"].Value = insert.EventExpSexo;
        dgv_titu_benef.Rows[fila].Cells["Edad"].Value = txt_edad.Text;
        dgv_titu_benef.Rows[fila].Cells["Exepcion"].Value = 1;
        dgv_titu_benef.Rows[fila].Cells["exepcionID"].Value = insert.EventExepId;
        dgv_titu_benef.Rows[fila].Cells["Emitir"].Value = Properties.Resources.impresora_PNG_24;
        MessageBox.Show("Verificar al final del listado y presionar imprimir.", "Carga de exepcion exitosa");
        limpiar_textbox();
        disable_textbox();
      }
    }

    private void msk_fecha_nac_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
    {

    }

    private void msk_fecha_nac_KeyDown(object sender, KeyEventArgs e)
    {
      if (msk_fecha_nac.MaskCompleted)
      {
        if (Keys.Enter == e.KeyCode)
        {
          Func_Utiles fnc = new Func_Utiles();
          int edad = fnc.calcular_edad(Convert.ToDateTime(msk_fecha_nac.Text));
          txt_edad.Text = edad.ToString();
          if (edad >= 0 && edad < 13)
          {
            txt_apellido.Enabled = true;
            txt_dni.Enabled = true;
            txt_edad.Enabled = true;
            txt_nombre.Enabled = true;
            cbx_parentesco.Enabled = true;
            cbx_sexo.Enabled = true;
            btn_cargar_exepcion.Enabled = true;
            txt_apellido.Focus();
          }
          else
          {
            MessageBox.Show("La edad no corresponde para el evento");
            disable_textbox();
          }
        }
        else
        {
          if (Keys.Back == e.KeyCode)
          {
            disable_textbox();
          }
        }
      }
      else
      {
        disable_textbox();
      }
    }

    private void disable_textbox()
    {
      txt_apellido.Enabled = false;
      txt_dni.Enabled = false;
      txt_edad.Enabled = false;
      txt_nombre.Enabled = false;
      cbx_parentesco.Enabled = false;
      cbx_sexo.Enabled = false;
      btn_cargar_exepcion.Enabled = false;
    }

    private void limpiar_textbox()
    {
      txt_apellido.Text = "";
      txt_dni.Text = "";
      txt_edad.Text = "";
      txt_nombre.Text = "";
    }

    private void txt_apellido_KeyDown(object sender, KeyEventArgs e)
    {
      if (Keys.Enter == e.KeyCode)
      {
        txt_nombre.Focus();
      }
    }

    private void txt_nombre_KeyDown(object sender, KeyEventArgs e)
    {
      if (Keys.Enter == e.KeyCode)
      {
        txt_dni.Focus();
      }
    }

    private void txt_dni_KeyDown(object sender, KeyEventArgs e)
    {
      if (Keys.Enter == e.KeyCode)
      {
        cbx_parentesco.Focus();
      }
    }

    private void cbx_parentesco_KeyDown(object sender, KeyEventArgs e)
    {
      if (Keys.Enter == e.KeyCode)
      {
        btn_cargar_exepcion.Focus();
      }
    }

    private void dgv_titu_benef_CellClick(object sender, DataGridViewCellEventArgs e)
    {
      //MessageBox.Show("" + dgv_titu_benef.Rows[e.RowIndex].Cells["nombre"].Value.ToString());
    }

    private void dgv_titu_benef_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
      try
      {

        if (this.dgv_titu_benef.Columns[e.ColumnIndex].Name == "Emitir")
        {
          EmitirCupon_();
        }
      }
      catch (Exception)
      {
      }
    }

    private void btn_minimizar_Click(object sender, EventArgs e)
    {
      this.WindowState = FormWindowState.Minimized;
    }

    private void btn_reimprimir_Click(object sender, EventArgs e)
    {
      EventosCupones evntcpn = new EventosCupones();
      if (Convert.ToInt32(dgv_titu_benef.CurrentRow.Cells["edad"].Value) < 12)
      {
        int exepID = Convert.ToInt32(dgv_titu_benef.CurrentRow.Cells["ExepcionID"].Value);
        int codigofliar = Convert.ToInt32(dgv_titu_benef.CurrentRow.Cells["CodigoFliar"].Value);

        ImprimirCupones(evntcpn.GetReimpresionCupon(codigofliar, exepID));

        //if (exepID > 0) // si Exepcion ya emitio cupon
        //{
        //  var sss = evntcpn.GetReimpresionCupon(codigofliar,exepID);
        //}
        //else
        //{
        //  var sss = evntcpn.GetReimpresionCupon(codigofliar, exepID);
        //}
      }
    }
  }
}
