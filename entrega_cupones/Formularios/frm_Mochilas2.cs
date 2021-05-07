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
  public partial class frm_Mochilas2 : Form
  {

    public double _cuil; // Variable global que almacena el cuil que viene del form de busqueda
    public int UsuarioID = 0;
    Clases.eventos evnt = new Clases.eventos();

    public frm_Mochilas2()
    {
      InitializeComponent();
    }

    private void frm_Mochilas2_Load(object sender, EventArgs e)
    {
      MostrarTurno();

      if (UsuarioID == 19 ||
      UsuarioID == 18 ||
      UsuarioID == 14 ||
      UsuarioID == 3 ||
      UsuarioID == 4 ||
      UsuarioID == 7
      )
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
      //Cargar_TituBenef();
      Cargar_cbx_parentesco();
      //BloquearFilaPorEdad();
      ObtenerTotalesEntregados();
      CargarCbxMochilas();
    }
    private void MostrarTurno()
    {
      EventosCupones EvntCpn = new EventosCupones();
      lbl_Turno.Text = "Turno: " + EvntCpn.GetDiaHoraDelTurno(EvntCpn.ConsultarTurno(Convert.ToString(_cuil)));
    }
    private void CargarCbxMochilas()
    {
      using (var context = new lts_sindicatoDataContext())
      {
        var mochilas = from a in context.articulos
                       select new
                       {
                         a.ID,
                         Mochila = a.Descripcion + (a.Sexo == 'F' ? " - MUJER" : " - VARON")
                       };
        cbx_Mochilas.DisplayMember = "Mochila";
        cbx_Mochilas.ValueMember = "ID";
        cbx_Mochilas.DataSource = mochilas.ToList();
      }
    }
    private void ObtenerTotalesEntregados()
    {
      EventosCupones EvntCpn = new EventosCupones();
      EventosExepciones EvntExp = new EventosExepciones();

    }
    private void BloquearFilaPorEdad()
    {
      EventosCupones EvntCpn = new EventosCupones();

      foreach (DataGridViewRow fila in dgv_titu_benef.Rows)
      {
        int TipoDeEvento = Convert.ToInt32(cbx_eventos.SelectedValue);
        if (TipoDeEvento == 3) // el evento es Dia de la Mujer
        {
          if (fila.Cells["Parentesco"].Value.ToString() == "Titular")
          {
            if (fila.Cells["sexo"].Value.ToString() == "F")
            {
              if (EvntCpn.ExisteTitular(Convert.ToInt32(fila.Cells["dni"].Value), TipoDeEvento))
              {
                fila.Cells["Estado"].Value = "Emitido [Nº: " + Convert.ToString(EvntCpn.GetNumeroCuponPorCuil(Convert.ToDouble(fila.Cells["dni"].Value), Convert.ToInt32(cbx_eventos.SelectedValue))).ToString() + "]";
                fila.ReadOnly = true;
                btn_emitir_cupon.Enabled = false;
              }
            }
            else
            {
              fila.ReadOnly = true;
              fila.Cells["Estado"].Value = "No Corresponde";
              btn_emitir_cupon.Enabled = false;
            }
            fila.ReadOnly = true;
          }
        }
        if (TipoDeEvento == 4) // el evento es Entrega de Mochilas
        {
          int edad = Convert.ToInt32(fila.Cells["Edad"].Value);
          if (edad < 1 || edad > 22)
          {
            fila.Cells["Estado"].Value = "No Corresponde";
            fila.ReadOnly = true;
          }

          if (Convert.ToInt32(fila.Cells["Exepcion"].Value) == 1)
          {
            if (EvntCpn.ExisteExepcion(Convert.ToInt32(fila.Cells["ExepcionID"].Value))) // si Exepcion ya emitio cupon
            {
              fila.Cells["Estado"].Value = "Emitido [Nº: " + EvntCpn.GetNumeroCuponPorCuil(Convert.ToDouble(fila.Cells["dni"].Value), Convert.ToInt32(cbx_eventos.SelectedValue)).ToString() + "]";
              fila.ReadOnly = true;
              btn_emitir_cupon.Enabled = false;
            }
          }
          else
          {
            if (EvntCpn.ExisteFamiliar(Convert.ToInt32(fila.Cells["CodigoFliar"].Value)))
            {
              fila.Cells["Estado"].Value = "Emitido [Nº: " + Convert.ToString(EvntCpn.GetNumeroCupon(Convert.ToInt32(fila.Cells["CodigoFliar"].Value))) + "]";
              fila.ReadOnly = true;
              btn_emitir_cupon.Enabled = false;
            }
          }
        }
      }
    }

    private void HabilitarBotonEmitirCupon(int exepcion, int exepcionID, int codigoDeFamiliar, int edad, int TipoDeEvento, bool EsTitular) //, string Sexo)
    {
      EventosCupones EvntCpn = new EventosCupones();

      if (TipoDeEvento == 3 && EsTitular == true)// && Sexo == "F")
      {
        btn_emitir_cupon.Enabled = true;
      }
      else
      {
        btn_emitir_cupon.Enabled = false;
      }

      if (TipoDeEvento == 4)
      {
        if (edad < 1 || edad > 22)
        {
          btn_emitir_cupon.Enabled = false;
        }
        else
        {
          if (exepcion == 1)
          {
            btn_emitir_cupon.Enabled = EvntCpn.ExisteExepcion(exepcionID) == true ? false : true;
          }
          else
          {
            btn_emitir_cupon.Enabled = EvntCpn.ExisteFamiliar(codigoDeFamiliar) == true ? false : true;
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

    private void Cargar_TituBenef()
    {
      if (dgv_titu_benef.Rows.Count > 0)
      {
        Func_Utiles fn = new Func_Utiles();
        fn.limpiar_dgv(dgv_titu_benef);
      }

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
      if (Convert.ToInt32(cbx_eventos.SelectedValue) == 4)
      {
        cargar_exepciones();
      }

    }

    private void cargar_combo_eventos()
    {
      cbx_eventos.DisplayMember = "eventos_nombre";
      cbx_eventos.ValueMember = "eventos_id";
      cbx_eventos.DataSource = evnt.get_todos();
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

    private void EmitirCupon_()
    {

      EventosCupones EvntCpn = new EventosCupones();
      DS_cupones ds = new DS_cupones();
      DataTable dt = ds.cupon_dia_niño;
      dt.Clear();
      socios soc = new socios();
      DataGridViewRow fila = dgv_titu_benef.CurrentRow;
      DataRow dr = dt.NewRow();
      var datos = soc.get_datos_socio(_cuil, 0);
      int edad = Convert.ToInt32(fila.Cells["Edad"].Value);
      int nroDeCupon = 0;
      int Termas = chk_Termas.Checked ? 1 : 0;

      if (edad > 1 || edad < 22)
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
              Convert.ToInt32(cbx_Mochilas.SelectedValue),
              UsuarioID,
              txt_QuienRetira.Text,
              chk_FondoDeDesempleo.Checked == true ? 1 : 0,
              datos.CuilStr,
              chk_Termas.Checked ? 1 : 0
              );
            nroDeCupon = nrocupon;

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
            dr["reimpresion"] = "0";
            dt.Rows.Add(dr);
          }
        }
        else // no es exepcion y pregunto si es entrega de Mochilas y pregunto se existe el familiar par enviar mensaje de que ya se emitio el cupon y si no existe Asigno el Cupon
        {
          if (Convert.ToInt32(cbx_eventos.SelectedValue) == 4 ?
            EvntCpn.ExisteFamiliar(Convert.ToInt32(fila.Cells["CodigoFliar"].Value)) :
           EvntCpn.ExisteTitular(Convert.ToInt32(fila.Cells["Dni"].Value), Convert.ToInt32(cbx_eventos.SelectedValue)))
          {
            MessageBox.Show("Ya se emitio cupon para Beneficiario.");
          }
          else
          {
            EventosCupones evntcpn = new EventosCupones();

            //Inserto el cupon y devuelvo el nro de Cupon

            int nrocupon = evntcpn.EventosCuponesInsertar(
              Convert.ToInt32(cbx_eventos.SelectedValue),
              Convert.ToDouble(fila.Cells["dni"].Value),
              Convert.ToInt32(fila.Cells["CodigoFliar"].Value),
              Convert.ToInt32(fila.Cells["ExepcionID"].Value),
              Convert.ToInt32(cbx_Mochilas.SelectedValue),
              UsuarioID,
              txt_QuienRetira.Text,
              chk_FondoDeDesempleo.Checked == true ? 1 : 0,
              datos.CuilStr,
              chk_Termas.Checked ? 1 : 0
              );
            nroDeCupon = nrocupon;

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
            dr["reimpresion"] = "0";
            dr["Turno"] = EvntCpn.GetDiaHoraDelTurno(EvntCpn.GetTurno(Convert.ToString(_cuil), Termas));
            dr["Logo"] = mtdConvertirImagen.ImageToByteArray(Image.FromFile("C:\\SEC_Gestion\\Imagen\\Logo_reporte.png"));
            dt.Rows.Add(dr);
          }
        }
        if (dt.Rows.Count > 0)
        {
          ImprimirCupones(dt, nroDeCupon, datos.nrosocio, fila.Cells["Edad"].Value.ToString(), datos.dni, dt.Rows[0]["titu_apenom"].ToString(), datos.empresa, fila.Cells["Dni"].Value.ToString(), fila.Cells["nombre"].Value.ToString());

          fila.Cells["Estado"].Value = "Emitido [Nº: " + EvntCpn.GetNumeroCuponPorCuil(Convert.ToDouble(fila.Cells["dni"].Value), Convert.ToInt32(cbx_eventos.SelectedValue)).ToString() + "]";
          btn_emitir_cupon.Enabled = false;

          //Muestro el Turno. En el Label del Formulario
          MostrarTurno();
        }
      }
    }

    private void ImprimirCupones(DataTable dt, int nroDeCupon, string nroDeSocio, string edad, string dniTitular, string apenomTitular, string empresa, string dniBeneficiario, string apenomBeneficiario)
    {
      usuarios usr = new usuarios();
      string TipoDeMochila = string.Empty;
      reportes frm_reportes = new reportes();
      EventosCupones EvntCpn = new EventosCupones();

      if (Convert.ToInt32(cbx_eventos.SelectedValue) == 4) // Es entrega de mochila
      {
        using (var context = new lts_sindicatoDataContext())
        {
          int MochilaID = Convert.ToInt32(context.eventos_cupones.Where(x => x.event_cupon_nro == nroDeCupon).SingleOrDefault().ArticuloID);
          var mochi = from a in context.articulos
                      where a.ID == MochilaID
                      select new
                      {
                        tipoMochila = a.Descripcion + " - " + (a.Sexo == 'F' ? "MUJER" : "VARON")
                      };

          TipoDeMochila = mochi.SingleOrDefault().tipoMochila;

        }

        //frm_reportes.nombreReporte = "rpt_EntregaDeMochila";
        frm_reportes.NombreDelReporte = "entrega_cupones.Reportes.rpt_EntregaDeMochila.rdlc";
        //frm_reportes.DtEntregaDeMochilas = dt;
        frm_reportes.dt = dt;
        frm_reportes.dt2 = Metodos.mtdFilial.Get_DatosFilial();
        frm_reportes.Parametro1 = "MOCHILAS 2021 - CUPON DE ENTREGA Nº " + nroDeCupon.ToString(); // Encabezado del cupon
        frm_reportes.Parametro2 = nroDeCupon.ToString(); // Nro de cupon
        frm_reportes.Parametro3 = nroDeSocio.Trim(); // Nro de Socio
        frm_reportes.Parametro4 = edad.Trim();//edad del Beneficiario
        frm_reportes.Parametro5 = dniBeneficiario; //dni del Beneficiario
        frm_reportes.Parametro6 = apenomBeneficiario; // mombre del beneficiario
        frm_reportes.Parametro7 = empresa.Trim(); // Empresa del titular
        frm_reportes.Parametro8 = TipoDeMochila; // que tipo de mochila lleva primaria/secundaria/Jardin
        frm_reportes.Parametro9 = apenomTitular; // Nombre del Titular
        frm_reportes.Parametro10 = dniTitular; //Dni del titular
        frm_reportes.Parametro11 = usr.ObtenerNombreDeUsuario(UsuarioID); //Usuario nombre 
        frm_reportes.Parametro12 = DateTime.Now.ToString(); //Fecha
        frm_reportes.Parametro13 = txt_QuienRetira.Text; //quien retira el Cupon
        frm_reportes.Parametro14 = chk_FondoDeDesempleo.Checked == true ? "Fdo. Desempleo: SI" : "Fdo. Desempleo: NO";
        frm_reportes.Parametro15 = "Turno: " + EvntCpn.GetDiaHoraDelTurno(EvntCpn.ConsultarTurno(Convert.ToString(_cuil)));
      }
      if (Convert.ToInt32(cbx_eventos.SelectedValue) == 3) // Es Dia de la Mujer
      {
        frm_reportes.nombreReporte = "rpt_EntradaDiaDeLaMujer";
        frm_reportes.DtDiaDeLaMujer = dt;
        frm_reportes.Parametro1 = " FIESTA DIA DE LA MUJER 2020"; // Encabezado del cupon
        frm_reportes.Parametro2 = nroDeCupon.ToString(); // Nro de cupon
        frm_reportes.Parametro3 = nroDeSocio.Trim(); // Nro de Socio
        frm_reportes.Parametro4 = apenomTitular; // Nombre del Titular
        frm_reportes.Parametro5 = dniTitular; //Dni del titular
        frm_reportes.Parametro6 = empresa.Trim(); // Empresa del titular
        frm_reportes.Parametro7 = "20 de Marzo - 22 hs - Polideportivo Empleados de Comercio - El Zanjon";
        frm_reportes.Parametro8 = "Entrada válida unicamente para Socias , es Intransferible \n (sin las bendi)";
        //frm_reportes.Parametro9 = ; // Nombre del Titular

      }
      frm_reportes.Show();

      //FIESTA EMPLEADOS DE COMERCIO 2019 - DOMINGO 22 DE SEPTIEMBRE - Open 23hs
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

    private void dgv_titu_benef_SelectionChanged_1(object sender, EventArgs e)
    {
      int cantfilas = dgv_titu_benef.Rows.Count;
      if (cantfilas > 0) // este if lo hago por que me da error cuando limpio el grid y llamo a mostrar los beneficiarios de las mochilas
      {
        socios soc = new socios();
        convertir_imagen img = new convertir_imagen();
        int edad = Convert.ToInt32(dgv_titu_benef.CurrentRow.Cells["Edad"].Value);
        double Codigo_Fliar;

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

        HabilitarBotonEmitirCupon(Convert.ToInt32(dgv_titu_benef.CurrentRow.Cells["Exepcion"].Value),
          Convert.ToInt32(dgv_titu_benef.CurrentRow.Cells["ExepcionId"].Value),
          Convert.ToInt32(dgv_titu_benef.CurrentRow.Cells["CodigoFliar"].Value),
          edad,
          Convert.ToInt32(cbx_eventos.SelectedValue),
          (Codigo_Fliar == 0 ? true : false)
          );
      }
    }

    private void dgv_titu_benef_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
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

    private void dgv_titu_benef_CurrentCellDirtyStateChanged_1(object sender, EventArgs e)
    {
      // Esta seccion de codigo controla que los check box se actualizen.

      if (dgv_titu_benef.IsCurrentCellDirty)
      {
        dgv_titu_benef.CommitEdit(DataGridViewDataErrorContexts.Commit);
      }
    }

    private void btn_cargar_exepcion_Click_1(object sender, EventArgs e)
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

    private void txt_apellido_KeyDown_1(object sender, KeyEventArgs e)
    {
      if (Keys.Enter == e.KeyCode)
      {
        txt_nombre.Focus();
      }
    }

    private void txt_nombre_KeyDown_1(object sender, KeyEventArgs e)
    {
      if (Keys.Enter == e.KeyCode)
      {
        txt_dni.Focus();
      }
    }

    private void txt_dni_KeyDown_1(object sender, KeyEventArgs e)
    {

      if (Keys.Enter == e.KeyCode)
      {
        cbx_parentesco.Focus();
      }
    }

    private void cbx_parentesco_KeyDown_1(object sender, KeyEventArgs e)
    {
      if (Keys.Enter == e.KeyCode)
      {
        btn_cargar_exepcion.Focus();
      }
    }

    private void btn_reimprimir_Click_1(object sender, EventArgs e)
    {
      EventosCupones evntcpn = new EventosCupones();
      //if (Convert.ToInt32(dgv_titu_benef.CurrentRow.Cells["edad"].Value) < 12)
      //{
      int exepID = Convert.ToInt32(dgv_titu_benef.CurrentRow.Cells["ExepcionID"].Value);
      int codigofliar = Convert.ToInt32(dgv_titu_benef.CurrentRow.Cells["CodigoFliar"].Value);
      int NroDni = Convert.ToInt32(dgv_titu_benef.CurrentRow.Cells["dni"].Value);

      DataTable dt = new DataTable();
      //DataRow dr = dt.Rows.Add();
      dt = Convert.ToInt32(cbx_eventos.SelectedValue) == 4 ? evntcpn.GetReimpresionCupon(codigofliar, exepID) : evntcpn.GetReimpresionCuponDiaDeLaMujer(NroDni, Convert.ToInt32(cbx_eventos.SelectedValue));
      //DataRow dr = dt.Rows;
      string edad = dt.Rows[0]["benef_edad"].ToString();
      ImprimirCupones(dt,
        Convert.ToInt32(dt.Rows[0]["event_nrocupon"].ToString()),
        dt.Rows[0]["titu_nrosocio"].ToString(),
        dt.Rows[0]["benef_edad"].ToString(),
        dt.Rows[0]["titu_dni"].ToString(),
        dt.Rows[0]["titu_apenom"].ToString(),
        dt.Rows[0]["titu_empresa"].ToString(),
        dt.Rows[0]["benef_dni"].ToString(),
        dt.Rows[0]["benef_apenom"].ToString());
      //ImprimirCupones(dt, nroDeCupon, datos.nrosocio, fila.Cells["Edad"].Value.ToString(), datos.dni, apenomTitular, datos.empresa, fila.Cells["Dni"].Value.ToString(), fila.Cells["nombre"].Value.ToString());
      //}
    }

    private void btn_emitir_cupon_Click_1(object sender, EventArgs e)
    {
      if (Convert.ToInt32(cbx_eventos.SelectedValue) == 4)
      {
        if (MessageBox.Show("\nEsta seguro de Emitir el Cupon de retiro de Mochila para el Beneficiario \n- Nombre: "
          + dgv_titu_benef.CurrentRow.Cells["nombre"].Value.ToString() +
          "\n" +
          "\n- Mochila: " + cbx_Mochilas.Text +
          "\n" +
          "\n- Retira: " + txt_QuienRetira.Text +
          "\n" +
          "\n- Fondo de Desempleo: " + (chk_FondoDeDesempleo.Checked == true ? "SI" : "NO")
          , "¡¡¡ ATENCION !!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning
          ) == DialogResult.Yes)
        {
          EmitirCupon_();
        }
      }
      if (Convert.ToInt32(cbx_eventos.SelectedValue) == 3)
      {
        EmitirCupon_();
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
          if (edad >= 2 && edad < 22)
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

    private void cbx_eventos_SelectedIndexChanged(object sender, EventArgs e)
    {
      int TipoEvento = Convert.ToInt32(cbx_eventos.SelectedValue);
      if (TipoEvento == 4) // Entrega de Mochilas
      {
        cbx_Mochilas.Visible = true;
        lbl_Mochila.Visible = true;
        Cargar_TituBenef();
        BloquearFilaPorEdad();
        lbl_TotalCuponesEmitidos.Visible = false;
        lbl_CuponesEmitidos.Visible = false;
      }
      else if (TipoEvento == 3) // Dia de la Mujer
      {
        cbx_Mochilas.Visible = false;
        lbl_Mochila.Visible = false;
        Cargar_TituBenef();
        BloquearFilaPorEdad();
        lbl_TotalCuponesEmitidos.Visible = true;
        lbl_TotalCuponesEmitidos.Text = CuponesEmitidos().ToString();
        lbl_CuponesEmitidos.Visible = true;
      }

    }

    private int CuponesEmitidos()
    {
      using (lts_sindicatoDataContext context = new lts_sindicatoDataContext())
      {
        return context.eventos_cupones.Where(x => x.eventcupon_evento_id == Convert.ToInt32(cbx_eventos.SelectedValue)).Count();
      }
    }
  }
}


