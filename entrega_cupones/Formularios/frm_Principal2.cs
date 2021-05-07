using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using entrega_cupones.Clases;
using entrega_cupones.Metodos;
using entrega_cupones.Modelos;

namespace entrega_cupones.Formularios
{
  public partial class frm_Principal2 : Form
  {
    public int _UserId;
    public int _UserRol;
    public string _RolNombre;
    public string _UserDNI;
    public string _User_Cuil;
    public string _UserNombre;
    public bool _EsSocio;


    int _BuscarPor = 0;
    int _FiltroDeSocio = 0;
    string _CUIT = "0";
    string _CodigoPostal = "0";
    int _NroDeSocio = 0;
    int _Jordada = 0;
    int _CodigoCategoria = 0;
    int _ActivoEnEmpresa = 0;
    int _Jubilados = 0;

    DateTime _FechaDeBaja = Convert.ToDateTime("01-01-1000");

    Func_Utiles fnc = new Func_Utiles();
    Buscadores buscar = new Buscadores();
    convertir_imagen cnvimg = new convertir_imagen();

    List<mdlSocio> _Socios = new List<mdlSocio>();


    public frm_Principal2(int id, string Nombre, string dni, string rol_nombre, int rol_Id)
    {
      InitializeComponent();

      _UserId = id;
      _UserNombre = Nombre;
      _UserDNI = dni;
      _RolNombre = rol_nombre;
      _UserRol = rol_Id;
    }

    private void frm_Principal2_Load(object sender, EventArgs e)
    {
      Icon = new Icon("C:\\SEC_Gestion\\Imagen\\icono.ico");
      CargarLocalidad();
      CargarCategorias();

      cbx_buscar_por.SelectedIndex = 0;
      cbx_filtrar.SelectedIndex = 1;
      cbx_NroDeSocio.SelectedIndex = 0;
      cbx_Jornada.SelectedIndex = 0;
      cbx_Jornada.SelectedIndex = 0;
      cbx_ActivoEnEmpresa.SelectedIndex = 0;
      cbx_Jubilados.SelectedIndex = 0;
      dgv_MostrarSocios.AutoGenerateColumns = false;
      cbx_Localidad.SelectedIndex = 0;
      cbx_Categoria.SelectedIndex = 0;

      lbl_Usuario.Text = _UserNombre;
      lbl_Rol.Text = _RolNombre;
      //buscar.get_titular(_UserDNI).foto.ToArray();
      var foto = buscar.get_titular(_UserDNI);
      if (foto.foto != null)
      {
        roundPictureBox1.Image = cnvimg.ByteArrayToImage(foto.foto.ToArray());
      }
    }

    private void CargarLocalidad()
    {
      using (var context = new lts_sindicatoDataContext())
      {
        var loc = (from a in context.localidades where a.idprovincias == 14 select a).OrderBy(x => x.nombre);
        cbx_Localidad.DisplayMember = "nombre";
        cbx_Localidad.ValueMember = "codigopostal";

        cbx_Localidad.DataSource = loc.ToList();
      }
    }

    private void CargarCategorias()
    {
      using (var context = new lts_sindicatoDataContext())
      {
        var cat = (from a in context.categorias_empleado select a).OrderBy(x => x.MAECAT_NOMCAT);
        cbx_Categoria.DisplayMember = "MAECAT_NOMCAT";
        cbx_Categoria.ValueMember = "MAECAT_CODCAT";

        cbx_Categoria.DataSource = cat.ToList();
      }
    }

    private void btn_Buscar_Click(object sender, EventArgs e)
    {
      BuscarSocio(txt_Busqueda.Text, Convert.ToDouble(string.IsNullOrEmpty(txt_CUIT.Text) ? "0" : txt_CUIT.Text), chk_VerificarCarencia.Checked);
    }

    private void BuscarSocio(string DatoABuscar, double cuit, bool Carecnia)
    {
      using (var context = new lts_sindicatoDataContext())
      {
        _Socios.Clear();
        dgv_MostrarSocios.DataSource = _Socios.ToList();
        _Socios.AddRange(mtdSocios.GetSocios(_FiltroDeSocio, _BuscarPor, _CodigoPostal, _NroDeSocio, _Jordada, _CodigoCategoria, _ActivoEnEmpresa, _Jubilados, DatoABuscar, cuit.ToString(), Carecnia));
        dgv_MostrarSocios.DataSource = _Socios.ToList();


        if (_Socios.Count() == 0)
        {
          lbl_SinRegistrosSocios.Visible = true;
          LimpiarCampos();
        }
        else
        {
          lbl_SinRegistrosSocios.Visible = false;
        }

        lbl_CantidadEmpleados.Text = "Empleados: " + _Socios.Count().ToString();
        lbl_total_socios.Text = "Socios: " + _Socios.Count(x => x.EsSocio == true).ToString();
        lbl_CantidadNOSocios.Text = "NO Socios: " + _Socios.Count(x => x.EsSocio == false).ToString();

      }
    }

    private void LimpiarCampos()
    {
      var ControlesPanel = this.Controls.OfType<Panel>().ToList();

      foreach (var paneles in ControlesPanel)
      {
        var panel = paneles.Controls.OfType<TextBox>().ToList();
        foreach (var txt in panel)
        {
          if (txt.Name != "txt_Busqueda" && txt.Name != "txt_CUIT")
          {
            txt.Text = "";
          }
        }
      }

      MostrarAportes2(0);
      MostrarBeneficiarios(0);

      picbox_socio.Image = null;
      picbox_beneficiario.Image = null;
      lbl_Parentesco.Text = "-----";
    }

    private void cbx_buscar_por_SelectedIndexChanged(object sender, EventArgs e)
    {
      _BuscarPor = cbx_buscar_por.SelectedIndex; // almaceno en variable el valor del item seleccionado en el combo busacar por tipo
      if (cbx_buscar_por.SelectedIndex == 2) // selecciona busqueda por empresa
      {
        btn_BuscarEmpresa.Enabled = true;
        txt_CUIT.Text = "0";
        BuscarEmpresa();
      }
      else
      {
        btn_BuscarEmpresa.Enabled = false;
        txt_Busqueda.Text = "";
        txt_CUIT.Text = "0";
      }

    }

    public void ejecutar(string empresa, string cuit)
    {
      if (empresa == "" || cuit == "")
      {
        txt_CUIT.Text = "0";
        _CUIT = "0";
      }
      else
      {
        txt_Busqueda.Text = empresa;
        txt_CUIT.Text = cuit;
        _CUIT = cuit;
      }
    }

    private void btn_BuscarEmpresa_Click(object sender, EventArgs e)
    {
      if (_BuscarPor == 2)
      {
        BuscarEmpresa();
      }
    }

    private void BuscarEmpresa()
    {
      frm_buscar_empresa f_busc_emp = new frm_buscar_empresa();
      f_busc_emp.PasarDatosFrmPrincipal_ += new frm_buscar_empresa.PasarDatosFrmPrincipal(ejecutar);// .PasarDatosActa(ejecutar);
      f_busc_emp.viene_desde = 5;
      f_busc_emp.ShowDialog();
      BuscarSocio(txt_Busqueda.Text, Convert.ToDouble(String.IsNullOrEmpty(txt_CUIT.Text) ? "0" : txt_CUIT.Text), chk_VerificarCarencia.Checked);
    }

    private void dgv_MostrarSocios_SelectionChanged(object sender, EventArgs e)
    {
      double cuil = Convert.ToDouble(dgv_MostrarSocios.CurrentRow.Cells["CUIL"].Value);
      double cuit = Convert.ToDouble(dgv_MostrarSocios.CurrentRow.Cells["CUIT_"].Value);
      MostrarFotoTitular(cuil);
      MostrarDatosTitular2(cuil);
      MostrarAportes2(cuil);
      MostrarBeneficiarios(cuil);
      MostrarDatosEmpresa(cuit);

      //MostrarFotoTitular(cuil);
      //MostrarDatosTitular(cuil);
      //MostrarAportes(cuil);
      //MostrarBeneficiarios(cuil);
      //MostrarDatosEmpresa(cuit);
    }

    private void MostrarFotoTitular(double cuil)
    {
      convertir_imagen cnvimg = new convertir_imagen();
      socios soc = new socios();

      var foto = soc.get_foto_titular_binary(cuil);
      picbox_socio.Image = cnvimg.ByteArrayToImage(foto.ToArray());
    }

    private void MostrarDatosTitular2(double cuil)
    {

      if (_Socios.Count() > 0)
      {
        int i = dgv_MostrarSocios.CurrentRow.Index;

        txt_NroSocio.Text = _Socios[i].NroDeSocio;
        _EsSocio = _Socios[i].EsSocio;
        txt_Estado.Text = _Socios[i].EsSocio ? "Socio Activo" : "No Socio";
        txt_Nombre.Text = _Socios[i].ApeNom;
        txt_DNI.Text = _Socios[i].NroDNI;
        txt_CUIL.Text = _Socios[i].CUIL;
        txt_CUIT2.Text = _Socios[i].CUIT;
        txt_EmpresaNombre.Text = _Socios[i].RazonSocial;
        txt_EstadoCivil.Text = _Socios[i].EstadoCivil;
        txt_Sexo.Text = _Socios[i].Sexo;
        txt_FechaNacimiento.Text = _Socios[i].FechaNacimiento;
        txt_Edad.Text = _Socios[i].Edad;
        string calle = _Socios[i].Calle == null ? "" : _Socios[i].Calle.Trim();
        string nrocalle = _Socios[i].NroCalle == null ? "" : "Nº " + _Socios[i].NroCalle.Trim();
        string barrio = _Socios[i].Barrio == null ? "" : "Bº " + _Socios[i].Barrio.Trim();
        txt_Domicilio.Text = calle + " " + nrocalle + " " + barrio;
        txt_Localidad.Text = _Socios[i].Localidad;
        txt_CodigoPostal.Text = (_Socios[i].CodigoPostal == null || _Socios[i].CodigoPostal == "" ? "No Asignada" : _Socios[i].CodigoPostal);
        txt_Telefono.Text = _Socios[i].Telefono.Trim() + " // "; // + _Socios[i].te .Trim();
        txt_Jornada.Text = _Socios[i].JornadaParcial ? "PARCIAL" : "COMPLETA";
        txt_Categoria.Text = _Socios[i].Categoria == 0 ? "No Especifica" : "otra";
        //context.categorias_empleado.Where(x => x.MAECAT_CODCAT == Datos.MAESOC_CODCAT).SingleOrDefault().MAECAT_NOMCAT.Trim();
        ////string fechabaja = context.socemp.Where(x => x.SOCEMP_CUIL == cuil && x.SOCEMP_ULT_EMPRESA == 'S').SingleOrDefault().SOCEMP_FECHABAJA.ToString("d");
        ////txt_FechaBaja.Text = fechabaja == "01/01/1000" ? "" : fechabaja;
      }

    }

    private void MostrarDatosTitular(double cuil)
    {
      using (var context = new lts_sindicatoDataContext())
      {
        var DatosSocio = from a in context.maesoc.Where(x => x.MAESOC_CUIL == cuil) select a;

        if (DatosSocio.Count() > 0)
        {
          var Datos = DatosSocio.SingleOrDefault();
          //txt_NroSocio.Text = dgv_MostrarSocios.CurrentRow.Cells["numero_soc"].Value.ToString();// Datos.MAESOC_NROAFIL.ToString();
          txt_NroSocio.Text = dgv_MostrarSocios.CurrentRow.Cells["numero_soc"].Value.ToString();// Datos.MAESOC_NROAFIL.ToString();
          txt_Estado.Text = (context.soccen.Where(x => x.SOCCEN_CUIL == cuil).SingleOrDefault().SOCCEN_ESTADO == 1 ? "Socio Activo" : "No Socio");
          txt_Nombre.Text = dgv_MostrarSocios.CurrentRow.Cells["ayn"].Value.ToString();
          txt_DNI.Text = dgv_MostrarSocios.CurrentRow.Cells["dni_socio"].Value.ToString();
          txt_CUIL.Text = dgv_MostrarSocios.CurrentRow.Cells["CUIL"].Value.ToString();
          txt_CUIT2.Text = dgv_MostrarSocios.CurrentRow.Cells["CUIT_"].Value.ToString();
          txt_EmpresaNombre.Text = dgv_MostrarSocios.CurrentRow.Cells["socio_empresa"].Value.ToString();
          txt_EstadoCivil.Text = Datos.MAESOC_ESTCIV.ToString();
          txt_Sexo.Text = Datos.MAESOC_SEXO.ToString();
          txt_FechaNacimiento.Text = Datos.MAESOC_FECHANAC.ToString("d");
          txt_Edad.Text = fnc.calcular_edad(Datos.MAESOC_FECHANAC).ToString();
          string calle = Datos.MAESOC_CALLE == null ? "" : "Calle: " + Datos.MAESOC_CALLE.Trim();
          string nrocalle = Datos.MAESOC_NROCALLE == null ? "" : "Nº " + Datos.MAESOC_NROCALLE.Trim();
          string barrio = Datos.MAESOC_BARRIO == null ? "" : "Bº " + Datos.MAESOC_BARRIO.Trim();
          txt_Domicilio.Text = calle + " " + nrocalle + " " + barrio;
          txt_Localidad.Text = fnc.GetLocalidad(Convert.ToInt32(Datos.MAESOC_CODLOC));
          txt_CodigoPostal.Text = (Datos.MAESOC_CODPOS == null || Datos.MAESOC_CODPOS == "" ? "No Asignada" : Datos.MAESOC_CODPOS.Trim());
          txt_Telefono.Text = Datos.MAESOC_TEL.Trim() + " // " + Datos.MAESOC_TELCEL.Trim();
          txt_Jornada.Text = context.ddjj.Where(x => x.cuil == cuil).OrderByDescending(x => x.periodo).FirstOrDefault().jorp == true ? "PARCIAL" : "COMPLETA";
          txt_Categoria.Text = Datos.MAESOC_CODCAT == 0 ? "No Especifica" : context.categorias_empleado.Where(x => x.MAECAT_CODCAT == Datos.MAESOC_CODCAT).SingleOrDefault().MAECAT_NOMCAT.Trim();
          //string fechabaja = context.socemp.Where(x => x.SOCEMP_CUIL == cuil && x.SOCEMP_ULT_EMPRESA == 'S').SingleOrDefault().SOCEMP_FECHABAJA.ToString("d");
          //txt_FechaBaja.Text = fechabaja == "01/01/1000" ? "" : fechabaja;
        }
      }
    }

    private void MostrarAportes2(double cuil)
    {
      if (_Socios.Count() > 0)
      {
        int i = dgv_MostrarSocios.CurrentRow.Index;
        dgv_MostrarAportes.DataSource = _Socios[i].Aportes;
        lbl_SinRegistrosAportes.Visible = _Socios[i].Aportes.Count > 0 ? false : true;
        btn_ImprimirAportes.Enabled = true;
      }
      else
      {
        lbl_SinRegistrosAportes.Visible = true;
        btn_ImprimirAportes.Enabled = false;
        List<mdlAportes> aaa = new List<mdlAportes>();
        dgv_MostrarAportes.DataSource = aaa;
      }
    }

    private void MostrarAportes(double cuil)
    {
      using (var context = new lts_sindicatoDataContext())
      {
        var ddjj = from a in context.ddjj
                   join emp in context.maeemp on a.cuite equals emp.MAEEMP_CUIT
                   where a.cuil == cuil
                   orderby a.periodo
                   select new
                   {
                     Periodo = a.periodo,
                     AporteLey = (a.impo + a.impoaux) * 0.02,
                     AporteSocio = (a.item2 == true) ? (a.impo + a.impoaux) * 0.02 : 0,
                     Sueldo = a.impo + a.impoaux,
                     Cuit = a.cuite,
                     RazonSocial = emp.MAEEMP_RAZSOC.Trim()
                   };
        dgv_MostrarAportes.DataSource = ddjj.ToList();
        if (ddjj.Count() > 0)
        {
          lbl_SinRegistrosAportes.Visible = false;
          dgv_MostrarAportes.CurrentCell = dgv_MostrarAportes.Rows[dgv_MostrarAportes.Rows.Count - 1].Cells[0];
          dgv_MostrarAportes.Rows[dgv_MostrarAportes.Rows.Count - 1].Selected = true;
          btn_ImprimirAportes.Enabled = true;

          txt_Estado.Text = VerificarCarencia() == true ? "Socio Activo" : "NO Es Socio";
        }
        else
        {
          lbl_SinRegistrosAportes.Visible = true;
          btn_ImprimirAportes.Enabled = false;
        }
      }
    }

    private bool VerificarCarencia()
    {

      int Fila = dgv_MostrarAportes.Rows.Count;
      DateTime UltimoPeriodoDeclarado = Convert.ToDateTime(dgv_MostrarAportes.Rows[Fila - 1].Cells["periodo"].Value);
      DateTime hoy = DateTime.Now;
      int meses = Convert.ToInt32((hoy - UltimoPeriodoDeclarado).TotalDays) / 30;
      int F = 0;
      int ContadorDeAportes = 0;
      if (meses <= 3)
      {
        switch (meses)
        {
          case 1:
            F = Fila - 1;
            break;
          case 2:
            F = Fila - 2;
            break;
          case 3:
            F = Fila - 3;
            break;
          default:

            break;
        }
      }
      else
      {
        F = 0;
        Fila = 0;
      }
      for (int i = F; i < Fila; i++)
      {
        if (Convert.ToDecimal(dgv_MostrarAportes.Rows[i].Cells["aporte_socio"].Value) > 0)
        {
          ContadorDeAportes += 1;
        }
      }
      return ContadorDeAportes > 0 ? true : false;
    }

    private void MostrarBeneficiarios(double cuil)
    {
      using (var context = new lts_sindicatoDataContext())
      {
        socios soc = new socios();
        var Beneficiario = from a in context.socflia
                           join Familiar in context.maeflia on a.SOCFLIA_CODFLIAR equals Familiar.MAEFLIA_CODFLIAR
                           where a.SOCFLIA_CUIL == cuil
                           select new
                           {
                             Nombre = Familiar.MAEFLIA_APELLIDO.Trim() + " " + Familiar.MAEFLIA_NOMBRE.Trim(),
                             Parentesco = (a.SOCFLIA_PARENT == 1) ? "CONYUGE" :
                                                    (a.SOCFLIA_PARENT == 2) ? "HIJO MENOR DE 16" :
                                                    (a.SOCFLIA_PARENT == 3) ? "HIJO MENOR DE 18" :
                                                    (a.SOCFLIA_PARENT == 4) ? "HIJO MENOR DE 21" :
                                                    (a.SOCFLIA_PARENT == 5) ? "HIJO MAYOR DE 21" : "",
                             CodigoDeBenef = Familiar.MAEFLIA_CODFLIAR,
                             DNI = Familiar.MAEFLIA_NRODOC,
                             FechaDeNacimiento = Familiar.MAEFLIA_FECNAC,
                             Edad = soc.calcular_edad(Familiar.MAEFLIA_FECNAC)
                           };
        dgv_MostrarBeneficiario.DataSource = Beneficiario.ToList();
        if (Beneficiario.Count() == 0)
        {
          picbox_beneficiario.Image = null;
          lbl_Parentesco.Text = "-----";
        }
        lbl_SinRegistrosBeneficiarios.Visible = Beneficiario.Count() > 0 ? false : true;
      }
    }

    private void MostrarDatosEmpresa(double cuit)
    {
      if (_Socios.Count() > 0)
      {
        int i = dgv_MostrarSocios.CurrentRow.Index;

        txt_EmpresaNombre.Text = _Socios[i].EmpresaNombre;
        txt_RazonSocial.Text = _Socios[i].RazonSocial;
        txt_CUIT2.Text = _Socios[i].CUIT;
        txt_EmpresaTelefono.Text = _Socios[i].EmpresaTelefono;
        txt_EmpresaDomicilio.Text = _Socios[i].EmpresaDomicilio;
        txt_EmpresaEstudio.Text = _Socios[i].EmpresaContador;
        txt_EmpresaEmail.Text = _Socios[i].EmpresaEmail;
        txt_EmpresaCodigoPostal.Text = _Socios[i].EmpresaCodigoPostal;
        txt_Localidad.Text = _Socios[i].EmpresaLocalidad;
      }

    }

    private void MostrarDatosEmpresa2(double cuit)
    {
      using (var context = new lts_sindicatoDataContext())
      {
        var empresa = from a in context.maeemp
                      where a.MAEEMP_CUIT == cuit
                      select new
                      {
                        Nombre = a.MAEEMP_NOMFAN.Trim(),
                        RazonSocial = a.MAEEMP_RAZSOC == null ? "" : a.MAEEMP_RAZSOC.Trim(),
                        CUIT = a.MEEMP_CUIT_STR.ToString(),
                        Telefono = a.MAEEMP_TEL,
                        Domicilio = a.MAEEMP_CALLE.Trim() + " Nº " + a.MAEEMP_NRO.Trim(),
                        Estudio = a.MAEEMP_ESTUDIO_CONTACTO.Trim(),
                        EstudioTelefono = a.MAEEMP_ESTUDIO_TEL.Trim(),
                        EstudioEmail = a.MAEEMP_ESTUDIO_EMAIL.Trim(),
                        CodigoPostal = a.MAEEMP_CODPOS.Trim(),
                        Localidad = fnc.GetLocalidad(Convert.ToInt32(a.MAEEMP_CODLOC)).Trim()
                      };
        if (empresa.Count() > 0)
        {
          var emp = empresa.SingleOrDefault();
          txt_EmpresaNombre.Text = emp.Nombre;
          txt_RazonSocial.Text = emp.RazonSocial;
          txt_CUIT2.Text = emp.CUIT;
          txt_EmpresaTelefono.Text = emp.Telefono;
          txt_EmpresaDomicilio.Text = emp.Domicilio;
          txt_EmpresaEstudio.Text = emp.Estudio;
          txt_EmpresaEmail.Text = emp.EstudioEmail;
          txt_EmpresaCodigoPostal.Text = emp.CodigoPostal;
          txt_Localidad.Text = emp.Localidad;
        }
      }
    }

    private void txt_Busqueda_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter)
      {
        BuscarSocio(txt_Busqueda.Text, Convert.ToDouble(String.IsNullOrEmpty(txt_CUIT.Text) ? "0" : txt_CUIT.Text), chk_VerificarCarencia.Checked);
      }
    }

    private void dgv_MostrarSocios_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
    }

    private void dgv_MostrarBeneficiario_SelectionChanged(object sender, EventArgs e)
    {
      MostrarFotoBeneficiario(Convert.ToDouble(dgv_MostrarBeneficiario.CurrentRow.Cells["codigo_fliar"].Value));
    }

    private void MostrarFotoBeneficiario(double CodigoDeFamiliar)
    {
      using (var context = new lts_sindicatoDataContext())
      {
        convertir_imagen cnvimg = new convertir_imagen();
        socios soc = new socios();

        var foto = soc.get_foto_benef_binary(CodigoDeFamiliar);
        picbox_beneficiario.Image = cnvimg.ByteArrayToImage(foto.ToArray());
        lbl_Parentesco.Text = dgv_MostrarBeneficiario.CurrentRow.Cells["parentesco"].Value.ToString();
      }
    }

    private void btn_ImprimirAportes_Click(object sender, EventArgs e)
    {
      ImprimirAportes();
    }

    private void ImprimirAportes()
    {
      try
      {
        using (var context = new lts_sindicatoDataContext())
        {
          DS_cupones dscpn = new DS_cupones();
          DataTable dt_impresion_ddjj = dscpn.ddjj_por_empleado;
          dt_impresion_ddjj.Clear();
          foreach (DataGridViewRow fila in dgv_MostrarAportes.Rows)
          {
            DataRow row = dt_impresion_ddjj.NewRow();

            row["periodo"] = Convert.ToDateTime(fila.Cells["periodo"].Value).Date;
            row["ley"] = Convert.ToDecimal(fila.Cells["aporte_ley"].Value);
            row["socio"] = Convert.ToDecimal(fila.Cells["aporte_socio"].Value);
            row["empresa"] = fila.Cells["razons"].Value;
            row["cuit"] = fila.Cells["cuit"].Value;
            row["dni"] = Convert.ToInt32(txt_DNI.Text.Trim());
            row["empleado"] = txt_Nombre.Text.Trim();
            row["Sueldo"] = fila.Cells["Sueldo"].Value;
            row["Logo"] = mtdConvertirImagen.ImageToByteArray(Image.FromFile("C:\\SEC_Gestion\\Imagen\\Logo_reporte.png"));
            dt_impresion_ddjj.Rows.Add(row);
          }
          reportes frm_reportes = new reportes();
          frm_reportes.NombreDelReporte = "rpt_ddjj_por_empleado";
          //frm_reportes.ddjj_por_empleado = dt_impresion_ddjj;
          frm_reportes.dt = dt_impresion_ddjj;
          frm_reportes.Show();
        }
      }
      catch (Exception)
      {
        throw;
      }
    }

    private void cbx_filtrar_SelectedIndexChanged_1(object sender, EventArgs e)
    {
      _FiltroDeSocio = cbx_filtrar.SelectedIndex;
    }

    private void cbx_Localidad_SelectedIndexChanged_1(object sender, EventArgs e)
    {
      _CodigoPostal = cbx_Localidad.SelectedValue.ToString();
    }

    private void cbx_NroDeSocio_SelectedIndexChanged_1(object sender, EventArgs e)
    {
      _NroDeSocio = cbx_NroDeSocio.SelectedIndex;
    }

    private void cbx_Jornada_SelectedIndexChanged_1(object sender, EventArgs e)
    {
      _Jordada = cbx_Jornada.SelectedIndex;
    }

    private void cbx_Categoria_SelectedIndexChanged(object sender, EventArgs e)
    {
      _CodigoCategoria = Convert.ToInt32(cbx_Categoria.SelectedValue);
    }

    private void cbx_ActivoEnEmpresa_SelectedIndexChanged_1(object sender, EventArgs e)
    {
      _ActivoEnEmpresa = cbx_ActivoEnEmpresa.SelectedIndex;
    }

    private void cbx_Jubilados_SelectedIndexChanged_1(object sender, EventArgs e)
    {
      _Jubilados = cbx_Jubilados.SelectedIndex;
    }

    private void menuCupones_Click(object sender, EventArgs e)
    {

    }

    private void menuQuinchos_Click(object sender, EventArgs e)
    {
      frm_quinchos f_quinnchos = new frm_quinchos();
      f_quinnchos.ShowDialog();
    }

    private void menu_VerificarDeuda_Click(object sender, EventArgs e)
    {
      VerificarDeuda f_VerificarDeuda = new VerificarDeuda();
      f_VerificarDeuda.ShowDialog();
    }

    private void menuCobros_Click(object sender, EventArgs e)
    {
      frm_CobroDeActas frmCobroDeActa = new frm_CobroDeActas();
      frmCobroDeActa.Show();

    }

    private void btn_Anterior_Click(object sender, EventArgs e)
    {
      frm_principal frmPrincipal = new frm_principal(_UserId, _UserNombre, _UserDNI, _RolNombre, _UserRol);
      frmPrincipal.Show();
    }

    private void menu_RendicionDeCobroDeActa_Click(object sender, EventArgs e)
    {
      frm_CobroDeActas frmCobroDeActa = new frm_CobroDeActas();
      frmCobroDeActa.Show();
    }

    private void menuMochilasEmitirCupon_Click(object sender, EventArgs e)
    {
      if (_EsSocio  )
      {
      frm_Mochilas2 f_mochilas = new frm_Mochilas2();
      f_mochilas._cuil = Convert.ToDouble(txt_CUIL.Text);
      f_mochilas.UsuarioID = _UserId;
      f_mochilas.ShowDialog();
      }
      else
      {
        MessageBox.Show("El empleado no es socio Activo por lo tanto no puede emitir un cupon.......");
      }
    }

    private void menuMochilasEntregar_Click(object sender, EventArgs e)
    {
      frm_EntregarMochila f_EntregaMochila = new frm_EntregarMochila();
      f_EntregaMochila.UsuarioId = _UserId;
      f_EntregaMochila.UsuarioNombre = _UserNombre;
      f_EntregaMochila.ShowDialog();
    }

    private void menuMochilasEdades_Click(object sender, EventArgs e)
    {
      frm_edades f_edades = new frm_edades();
      f_edades.Show();
    }
  }
}
