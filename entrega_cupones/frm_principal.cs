using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using LinqToExcel;
using System.Data.OleDb;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using entrega_cupones.Clases;
using System.Web.UI;
using Bunifu.Framework.UI;
using entrega_cupones.Formularios;
using Microsoft.Reporting.WinForms;

//root@localhost:3306  jdbc:mysql://localhost:3306/?user=root


namespace entrega_cupones
{
  public partial class frm_principal : Form
  {
    lts_sindicatoDataContext db_socios = new lts_sindicatoDataContext();
    Buscadores buscar = new Buscadores();
    convertir_imagen conv_img = new convertir_imagen();
    public int user_id { get; set; }

    public frm_principal(int id_usuario, string user_name, string dni, string rol, int rolID)
    {
      InitializeComponent();
      user_id = id_usuario;
      lbl_usuario.Text = user_name;
      lbl_rol.Text = rol;
      btn_pedicuro.Enabled = true;
      var foto = buscar.get_titular(dni).foto;

      if (foto != null)
      {
        Picture_box_user.Image = conv_img.ByteArrayToImage(foto.ToArray());
      }
      activar_usuarios_objetos(rol, rolID);
    }

    private void activar_usuarios_objetos(string rol, int rolID)
    {
      if (user_id == 19 ||
       user_id == 18 ||
       user_id == 14 ||
       user_id == 3 ||
       user_id == 4)
      {
        btn_ActivarSocio.Visible = true;
      }
      else
      {
        btn_ActivarSocio.Visible = false;
      }

      usuarios user = new usuarios();
      var permisos = user.get_permisos(rolID); // obtengo la lista de los permisos de un usuario

      var controles = pnl_menu.Controls.OfType<BunifuFlatButton>().ToList(); // obtengo la lista de controles

      foreach (var permisoControl in permisos.ToList())
      {
        foreach (var control in controles)
        {

          if (control.Name == permisoControl.objeto)//(rolcontrols.ControlNOmbre == item.Name)
          {
            if (permisoControl.permiso == 1)
            {
              control.Enabled = true;
            }
            else
            {
              control.Enabled = false;
            }
          }
        }
      }
    }

    private void Frm_principal_Load(object sender, EventArgs e)
    {
      cbx_buscar_por.SelectedIndex = 0;
      cbx_filtrar.SelectedIndex = 1;
      dgv_mostrar_socios.AutoGenerateColumns = false;
      dgv_mostrar_socios.Sort(dgv_mostrar_socios.Columns[0], ListSortDirection.Ascending);
    }

    public void RecorrerControles(System.Windows.Forms.Control control)
    {
      //Recorremos con un ciclo for each cada control que hay en la colección Controls
      foreach (System.Windows.Forms.Control contHijo in control.Controls)
      {
        //Preguntamos si el control tiene uno o mas controles dentro del mismo con la propiedad 'HasChildren'
        //Si el control tiene 1 o más controles, entonces llamamos al procedimiento de forma recursiva, para que siga recorriendo los demás controles 
        if (contHijo.HasChildren) this.RecorrerControles(contHijo);
        //Aqui va la lógica de lo queramos hacer, en mi ejemplo, voy a pintar de color azul el fondo de todos los controles
        // contHijo.BackColor = Color.Blue;
        if (contHijo.Name == "btn_futbol")
        {
          contHijo.Enabled = true;
        }
      }
    }

    private void Btn_entrega_cupones_Click(object sender, EventArgs e)
    {

      Frm_socios f_socios = new Frm_socios();
      f_socios.ShowDialog();
    }

    private void Btn_menu_Click(object sender, EventArgs e)
    {
      if (pnl_menu.Width == 45)
      {
        pnl_menu.Visible = false;
        pnl_menu.Width = 138;
        Btn_menu.Location = new Point(106, 3);
        pnl_menu_transition.ShowSync(pnl_menu);
      }
      else
      {
        pnl_menu.Visible = false;
        pnl_menu.Width = 45;
        Btn_menu.Location = new Point(12, 3);
        pnl_menu_transition.ShowSync(pnl_menu);
      }
    }

    private void Btn_close_Click(object sender, EventArgs e)
    {
      btn_close.Enabled = false;
      Close();
    }

    private void btn_mochila_Click(object sender, EventArgs e)
    {
      //panel8.Visible = true;
      frm_mochilas f_mochilas = new frm_mochilas();
      f_mochilas.Show();
    }

    private void btn_quinchos_Click(object sender, EventArgs e)
    {

      if (dgv_mostrar_socios.Rows.Count > 0) // verifico si el DGV_mostar_socios no esta vacio para poder usar el boton de quinchos
      {
        frm_quinchos f_quinchos = new frm_quinchos();
        f_quinchos.lbl_cuil.Text = lbl_cuil.Text;
        f_quinchos.lbl_socio.Text = lbl_nombre.Text;

        var var_buscar_foto = db_socios.fotos.Where(x => x.FOTOS_CUIL == Convert.ToDouble(lbl_cuil.Text) && x.FOTOS_CODFLIAR == 0);
        if (var_buscar_foto.Count() > 0)
        {
          f_quinchos.picbox_socio_referente.Image = ByteArrayToImage(var_buscar_foto.Single().FOTOS_FOTO.ToArray());
          f_quinchos.btn_sin_foto.Visible = false;
        }
        else
        {
          f_quinchos.picbox_socio_referente.Image = null;
          f_quinchos.btn_sin_foto.Visible = true;
        }
        f_quinchos.Show();
      }
    }

    private void btn_pedicuro_Click(object sender, EventArgs e)
    {
      //System.IO.MemoryStream ms = new System.IO.MemoryStream();

      //picbox_socio.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
      //fotos1 fts = new fotos1();
      //fts.foto = ms.GetBuffer();
      //db_socios.fotos1.InsertOnSubmit(fts);
      //db_socios.SubmitChanges();
      frm_inspecciones f_inspecciones = new frm_inspecciones();
      f_inspecciones.Show();


    }

    private void btn_masajista_Click(object sender, EventArgs e)
    {
      //var benef = from a in db_socios.fotos1
      //            where a.id_foto >= 4
      //            select new
      //            {
      //                //image.GetThumbnailImage(20/*width*/, 40/*height*/, null, IntPtr.Zero))
      //                //var img = ByteArrayToImage(a.foto.ToArray())
      //                //using(var thumbnail = image.GetThumbnailImage(20/*width*/, 40/*height*/, null, IntPtr.Zero))
      //                foto =  (ByteArrayToImage(a.foto.ToArray())).GetThumbnailImage(70,70,null,IntPtr.Zero)
      //                //foto = ByteArrayToImage(a.foto.ToArray()) 

      //            };
      //dgv_familiar_a_cargo.DataSource = benef;
      //var benf = db_socios.fotos1.Where(x => x.id_foto == 4).Single();

      //picbox_socio.Image = ByteArrayToImage(benf.foto.ToArray()); //Image.FromFile(benf.foto);

      ////ByteArrayToImage(img.alumno_foto.ToArray());
    }

    private byte[] ImageToByteArray(System.Drawing.Image imageIn)
    {
      using (MemoryStream ms = new MemoryStream())
      {
        imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
        return ms.ToArray();
      }
    }

    public Image ByteArrayToImage(byte[] byteArrayIn)
    {
      using (MemoryStream ms = new MemoryStream(byteArrayIn))
      {
        Image returnImage = Image.FromStream(ms);
        return returnImage;
      }
    }

    private void btn_asesoria_Click(object sender, EventArgs e)
    {
      OpenFileDialog ofd = new OpenFileDialog();
      ofd.ShowDialog();
      if (ofd.FileName.Equals("") == false)
      {

        //ruta del archivo
        //string ruta_excel = "E:\\SEC\\Diego\\Socios.xlsx";// Application.StartupPath + "\\Socios.xlsx";
        // creamos libro a partir de la ruta
        var libro = new ExcelQueryFactory(ofd.FileName);
        // Consulta con linq
        var res = from row in libro.Worksheet("inicio")
                  select new
                  {
                    cuil = row[0].Value,
                    apenom = row[1].Value,
                    nro_afil = row[2].Value
                    //provincia = row[4].Value
                  };
        dgv_excell.DataSource = res.ToList();
      }
    }

    private void btn_colonia_Click(object sender, EventArgs e)
    {
      //frm_eventos f_eventos = new frm_eventos();
      //f_eventos._cuil = Convert.ToDouble(lbl_cuil.Text);
      //f_eventos.UsuarioID = user_id;
      //f_eventos.ShowDialog();

      frm_Eventos2 f_eventos = new frm_Eventos2();
      f_eventos._cuil = Convert.ToDouble(lbl_cuil.Text);
      f_eventos._UsuarioId = user_id;
      if (lbl_estado_socio.Text.Trim() == "NO SOCIO")
      {
        f_eventos._EsSocio = false;
      }
      else
      {
        f_eventos._EsSocio = true;
      }
      f_eventos.ShowDialog();

    }

    private void panel3_Paint(object sender, PaintEventArgs e)
    {

    }

    private void btn_actualizar_Click(object sender, EventArgs e)
    {
      //Limpio la tabla de Socios
      db_socios.limpiar_tabla_socios();
      //string conexion = "Provider=Microsoft.ACE.OleDB.12.0;Data Source = E:\\SEC\\importacion de BD\\nueva carpeta\\socios.xlsx;Extended Properties=Excel 12.0 Xml; HDR=Yes";
      string conexion = "Provider=Microsoft.ACE.OleDB.12.0;Data Source = E:\\SEC\\importacion de BD\\nueva carpeta\\socios.xlsx;Extended Properties=\"Excel 12.0; HDR=Yes\"";
      OleDbConnection origen = default(OleDbConnection);
      origen = new OleDbConnection(conexion);
      origen.Open();

      OleDbCommand seleccion = default(OleDbCommand);
      seleccion = new OleDbCommand("Select * From [socios$]", origen);

      OleDbDataAdapter adaptador = new OleDbDataAdapter();
      adaptador.SelectCommand = seleccion;

      DataSet ds = new DataSet();

      adaptador.Fill(ds);
      dgv_excell.DataSource = ds.Tables[0];

      origen.Close();

      SqlConnection conexion_destino = new SqlConnection();
      conexion_destino.ConnectionString = "Data Source = DROP\\DPS_17; Initial Catalog = sindicato; Integrated Security = True";
      //"                                       Data Source = sindicato; Initial Catalog=sindicato; Integrated Security = True";

      conexion_destino.Open();

      SqlBulkCopy importar = default(SqlBulkCopy);

      importar = new SqlBulkCopy(conexion_destino);
      importar.DestinationTableName = "socios";
      importar.WriteToServer(ds.Tables[0]);
      conexion_destino.Close();
      lbl_total_socios.Text = lbl_total_socios.Text + " " + Convert.ToString(ds.Tables[0].Rows.Count);

    }

    private void Dgv_socios_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {

    }

    private void Txt_dni_OnValueChanged(object sender, EventArgs e)
    {
      //buscar_socios(Txt_dni.Text.Trim());
    }

    private void buscar_socios(string dato)
    {

      if (dato == "") // si el parametro "dato" a buscar no contiene nada entonces se muestran todos los socios de acuerdo al filtro
      {
        buscar_en_blanco();
      }
      else
      {
        if (cbx_buscar_por.SelectedItem.ToString() == "D.N.I.") // si esta chequeado buscar por DNI
        {
          // llamo a la funciona buscar_por_dni y le paso el parametro dato para que aplique el filtro
          buscar_por_dni(dato);
        }
        if (cbx_buscar_por.SelectedItem.ToString() == "Apellido y Nombre") // si esta chequeado buscar por apellido y nombre
        {
          // llamo a la funcion buscar_por_apeynom y le paso el parametro dato para que aplique el filtro
          buscar_por_apeynom(dato);
        }
        if (cbx_buscar_por.SelectedItem.ToString() == "Empresa") // si esta chequeado buscar por empresa
        {
          // llamo a la funcion buscar_por_empresa y le paso el parametro dato para que aplique el filtro
          buscar_por_empresa(dato);
        }
      }
    }

    private void btn_update_Click(object sender, EventArgs e)
    {

      buscar_socios(Txt_dni.Text);
    }

    private void Txt_dni_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter)
      {
        buscar_socios(Txt_dni.Text);
      }
    }

    private void cbx_filtrar_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void buscar_por_dni(string dato)
    {
      using (var context = new lts_sindicatoDataContext())
      {
        string estado = verificar_filtro();

        socios soc = new socios();
        var result = soc.GetListadoSociosPorDniAyn(estado, dato);

        dgv_mostrar_socios.DataSource = result;
        //var socio = (from b in (from a in context.soccen
        //                        where (estado == "2") ? a.SOCCEN_ESTADO != '2' : a.SOCCEN_ESTADO == Convert.ToByte(estado)
        //                        join sc in context.maesoc on a.SOCCEN_CUIL equals sc.MAESOC_CUIL
        //                        join se in context.socemp on a.SOCCEN_CUIL equals se.SOCEMP_CUIL
        //                        where se.SOCEMP_ULT_EMPRESA == 'S'
        //                        join empr in context.maeemp on se.SOCEMP_CUITE equals empr.MAEEMP_CUIT
        //                        select new
        //                        {
        //                          numero_socio = sc.MAESOC_NROAFIL.Trim(),
        //                          dni_socio = sc.MAESOC_NRODOC.Trim(),
        //                          apeynom = sc.MAESOC_APELLIDO.Trim() + " " + sc.MAESOC_NOMBRE.Trim(),
        //                          empresa = empr.MAEEMP_RAZSOC,
        //                          empresa_cuit = empr.MAEEMP_CUIT,
        //                        })
        //             where b.apeynom.Contains(dato) || b.dni_socio.Contains(dato)
        //             select b).OrderBy(x => x.apeynom).ToList();

        //dgv_mostrar_socios.DataSource = socio.ToList();

        //lbl_total_socios.Text = "Cantidad de Socios: " + Convert.ToString(socio.Count());
        if (dgv_mostrar_socios.Rows.Count > 0)
        {
          // btn_presente.Visible = true;
          btn_para_inspeccion.Enabled = true;

        }
        else
        {
          // btn_presente.Visible = false;
          btn_para_inspeccion.Enabled = false;
        }
      }
    }

    private void buscar_por_apeynom(string dato)
    {

      string estado = verificar_filtro();
      if (estado == "Todos")
      {
        var socio = (from a in db_socios.soccen
                     join sc in db_socios.maesoc on a.SOCCEN_CUIL equals sc.MAESOC_CUIL
                     where sc.MAESOC_APELLIDO.Contains(dato) || sc.MAESOC_NOMBRE.Contains(dato)
                     join se in db_socios.socemp on a.SOCCEN_CUIL equals se.SOCEMP_CUIL
                     where se.SOCEMP_ULT_EMPRESA == 'S'
                     join empr in db_socios.maeemp on se.SOCEMP_CUITE equals empr.MAEEMP_CUIT
                     select new
                     {
                       numero_socio = sc.MAESOC_NROAFIL.Trim(),
                       dni_socio = sc.MAESOC_NRODOC.Trim(),
                       apeynom = sc.MAESOC_APELLIDO.Trim() + " " + sc.MAESOC_NOMBRE.Trim(),
                       empresa = empr.MAEEMP_CUIT
                     }).OrderBy(x => x.apeynom);
        dgv_mostrar_socios.DataSource = socio.ToList();
        lbl_total_socios.Text = "Cantidad de Socios: " + Convert.ToString(socio.Count());
      }
      else
      {


        var socio = (from a in db_socios.soccen
                     where a.SOCCEN_ESTADO == Convert.ToByte(estado)
                     join sc in db_socios.maesoc on a.SOCCEN_CUIL equals sc.MAESOC_CUIL
                     where sc.MAESOC_APELLIDO.Contains(dato)
                     join se in db_socios.socemp on a.SOCCEN_CUIL equals se.SOCEMP_CUIL
                     where se.SOCEMP_ULT_EMPRESA == 'S'
                     join empr in db_socios.maeemp on se.SOCEMP_CUITE equals empr.MAEEMP_CUIT
                     select new
                     {
                       numero_socio = sc.MAESOC_NROAFIL.Trim(),
                       dni_socio = sc.MAESOC_NRODOC.Trim(),
                       apeynom = sc.MAESOC_APELLIDO.Trim() + " " + sc.MAESOC_NOMBRE.Trim(),
                       empresa = empr.MAEEMP_RAZSOC
                     }).OrderBy(x => x.apeynom);
        dgv_mostrar_socios.DataSource = socio.ToList();
        lbl_total_socios.Text = "Cantidad de Socios: " + Convert.ToString(socio.Count());
      }
    }

    private void buscar_por_empresa(string dato)
    {
      using (var context = new lts_sindicatoDataContext())
      {
        string estado = verificar_filtro();
        var socio = (from a in context.soccen
                     where (estado == "2") ? a.SOCCEN_ESTADO != '2' : a.SOCCEN_ESTADO == Convert.ToByte(estado)
                     join sc in context.maesoc on a.SOCCEN_CUIL equals sc.MAESOC_CUIL
                     join se in context.socemp on a.SOCCEN_CUIL equals se.SOCEMP_CUIL
                     where se.SOCEMP_ULT_EMPRESA == 'S'
                     join empr in context.maeemp on se.SOCEMP_CUITE equals empr.MAEEMP_CUIT
                     where (empr.MAEEMP_RAZSOC.Contains(dato) || empr.MAEEMP_NOMFAN.Contains(dato))
                     select new
                     {
                       numero_socio = sc.MAESOC_NROAFIL.Trim(),
                       dni_socio = sc.MAESOC_NRODOC.Trim(),
                       apeynom = sc.MAESOC_APELLIDO.Trim() + " " + sc.MAESOC_NOMBRE.Trim(),
                       empresa = empr.MAEEMP_RAZSOC,
                       empresa_cuit = empr.MAEEMP_CUIT,
                     }).OrderBy(x => x.apeynom).ToList();


        if (socio.Count > 0)
        {
          btn_para_inspeccion.Enabled = true;
        }
        else
        {
          btn_para_inspeccion.Enabled = false;
        }

        dgv_mostrar_socios.DataSource = socio.ToList();
        lbl_total_socios.Text = "Cantidad de Socios: " + Convert.ToString(socio.Count());
      }
    }

    private void buscar_en_blanco()
    {
      string estado = verificar_filtro();
      if (estado == "Todos")
      {
        var socio = (from a in db_socios.soccen
                     join sc in db_socios.maesoc on a.SOCCEN_CUIL equals sc.MAESOC_CUIL
                     join se in db_socios.socemp on a.SOCCEN_CUIL equals se.SOCEMP_CUIL
                     where se.SOCEMP_ULT_EMPRESA == 'S'
                     join empr in db_socios.maeemp on se.SOCEMP_CUITE equals empr.MAEEMP_CUIT
                     select new
                     {
                       numero_socio = sc.MAESOC_NROAFIL.Trim(),
                       dni_socio = sc.MAESOC_NRODOC.Trim(),
                       apeynom = sc.MAESOC_APELLIDO.Trim() + " " + sc.MAESOC_NOMBRE.Trim(),
                       empresa = empr.MAEEMP_RAZSOC
                     }).OrderBy(x => x.apeynom);
        dgv_mostrar_socios.DataSource = socio.ToList();
        lbl_total_socios.Text = "Cantidad de Socios: " + Convert.ToString(socio.Count());
      }
      else
      {
        var socio = (from a in db_socios.soccen
                     where a.SOCCEN_ESTADO == Convert.ToInt32(estado)
                     join sc in db_socios.maesoc on a.SOCCEN_CUIL equals sc.MAESOC_CUIL
                     join se in db_socios.socemp on a.SOCCEN_CUIL equals se.SOCEMP_CUIL
                     where se.SOCEMP_ULT_EMPRESA == 'S'
                     join empr in db_socios.maeemp on se.SOCEMP_CUITE equals empr.MAEEMP_CUIT

                     select new
                     {
                       numero_socio = sc.MAESOC_NROAFIL.Trim(),
                       dni_socio = sc.MAESOC_NRODOC.Trim(),
                       apeynom = sc.MAESOC_APELLIDO.Trim() + " " + sc.MAESOC_NOMBRE.Trim(),
                       empresa = empr.MAEEMP_RAZSOC
                     }).OrderBy(x => x.apeynom);
        dgv_mostrar_socios.DataSource = socio.ToList();
        lbl_total_socios.Text = "Cantidad de Socios: " + Convert.ToString(socio.Count());

      }
    }

    private string verificar_filtro()
    {
      string estado = "";

      if (cbx_filtrar.SelectedItem.ToString() == "Socios")
      {
        estado = "1";
      }
      if (cbx_filtrar.SelectedItem.ToString() == "NO Socios")
      {
        estado = "0";
      }
      if (cbx_filtrar.SelectedItem.ToString() == "Todos")
      {
        estado = "2";
      }

      return estado;
    }

    private void mostrar_benef(string v)
    {
      double cuil_titular = db_socios.maesoc.Where(x => x.MAESOC_NRODOC == v).FirstOrDefault().MAESOC_CUIL;
      using (var context = new lts_sindicatoDataContext())
      {
        var var_beneficiario = from sf in context.socflia
                               where sf.SOCFLIA_CUIL == cuil_titular
                               join ms in context.maeflia on sf.SOCFLIA_CODFLIAR equals ms.MAEFLIA_CODFLIAR
                               select new
                               {
                                 apeynom = ms.MAEFLIA_APELLIDO.Trim() + " " + ms.MAEFLIA_NOMBRE.Trim(),
                                 parentesco = (sf.SOCFLIA_PARENT == 1) ? "CONYUGE" :
                                                    (sf.SOCFLIA_PARENT == 2) ? "HIJO MENOR DE 16" :
                                                    (sf.SOCFLIA_PARENT == 3) ? "HIJO MENOR DE 18" :
                                                    (sf.SOCFLIA_PARENT == 4) ? "HIJO MENOR DE 21" :
                                                    (sf.SOCFLIA_PARENT == 5) ? "HIJO MAYOR DE 21" : "",
                                 codigo_fliar = sf.SOCFLIA_CODFLIAR,
                               };
        dgv_mostrar_beneficiario.DataSource = var_beneficiario.ToList();
        if (dgv_mostrar_beneficiario.Rows.Count == 0)
        {
          picbox_beneficiario.Image = null;
          btn_sin_imagen_benef.Visible = true;
        }
      }

      //double cuil_titular = db_socios.maesoc.Where(x => x.MAESOC_NRODOC == v).FirstOrDefault().MAESOC_CUIL;
      //var var_beneficiario = from sf in db_socios.socflia
      //                       where sf.SOCFLIA_CUIL == cuil_titular
      //                       join ms in db_socios.maeflia on sf.SOCFLIA_CODFLIAR equals ms.MAEFLIA_CODFLIAR
      //                       select new
      //                       {
      //                            apeynom = ms.MAEFLIA_APELLIDO.Trim() + " " + ms.MAEFLIA_NOMBRE.Trim(),
      //                            parentesco = (sf.SOCFLIA_PARENT == 1) ? "CONYUGE" :
      //                                            (sf.SOCFLIA_PARENT == 2) ? "HIJO MENOR DE 16" :
      //                                            (sf.SOCFLIA_PARENT == 3) ? "HIJO MENOR DE 18" :
      //                                            (sf.SOCFLIA_PARENT == 4) ? "HIJO MENOR DE 21" :
      //                                            (sf.SOCFLIA_PARENT == 5) ? "HIJO MAYOR DE 21" : "",
      //                            codigo_fliar = sf.SOCFLIA_CODFLIAR,
      //                       };
      //dgv_mostrar_beneficiario.DataSource = var_beneficiario.ToList();
      //if (dgv_mostrar_beneficiario.Rows.Count == 0)
      //{
      //    picbox_beneficiario.Image = null;
      //    btn_sin_imagen_benef.Visible = true;
      //}                      
    }

    private void mostrar_benef_datos(long codigo_benef)
    {
      var var_dato_benef = from benef in db_socios.maeflia
                           where benef.MAEFLIA_CODFLIAR == codigo_benef
                           select new
                           {
                             codigo_fliar = benef.MAEFLIA_CODFLIAR,
                             apellido = benef.MAEFLIA_APELLIDO.Trim(),
                             nombre = benef.MAEFLIA_NOMBRE.Trim(),
                             dni = benef.MAEFLIA_NRODOC,
                             fnac = benef.MAEFLIA_FECNAC,
                             fingreso = benef.MAEFLIA_FECING,
                             sexo = benef.MAEFLIA_SEXO,
                             edad = calcular_edad(benef.MAEFLIA_FECNAC),
                             estado_civil = benef.MAEFLIA_ESTCIVIL,
                             //estado
                             estudia = benef.MAEFLIA_ESTUDIA,
                             discapacitado = benef.MAEFLIA_DISCAPAC,
                           };
      if (var_dato_benef.Count() > 0)
      {
        //var_dato_benef.ToList();
        txt_benef_ape.Text = var_dato_benef.Single().apellido;
        txt_benef_nombre.Text = var_dato_benef.Single().nombre;
        txt_benef_dni.Text = var_dato_benef.Single().dni.ToString();
        // si la fecha de nacimiento no es correcta entonces muestro null

        if (var_dato_benef.Single().fnac.Date == Convert.ToDateTime("01/01/1000").Date)
        {
          dtp_fnac.Text = "";
          txt_benef_edad.Text = "";
        }
        else
        {
          dtp_fnac.Value = var_dato_benef.Single().fnac.Date;
        }
        //dtp_fnac.Value = var_dato_benef.Single().fnac.Date == Convert.ToDateTime("01/01/1000").Date ? null : var_dato_benef.Single().fnac.Date;

        if (var_dato_benef.Single().fingreso.Date == Convert.ToDateTime("01/01/1000").Date)
        {
          dtp_fingreso.Text = "";
          // txt_benef_edad.Text = "";
        }
        else
        {
          dtp_fingreso.Value = var_dato_benef.Single().fingreso.Date;
        }
        //dtp_fingreso.Value = var_dato_benef.Single().fingreso.Date;
        cbx_benef_sexo.SelectedIndex = var_dato_benef.Single().sexo == 'F' ? 0 : 1;// .Text = var_dato_benef.Single().sexo.ToString();

        obtener_estadocivil(var_dato_benef.Single().estado_civil);
        //cbx_benef_estado_civil.Text = var_dato_benef.Single().estado_civil.ToString();

        int cod_parentesco = db_socios.socflia.Where(x => x.SOCFLIA_CODFLIAR == Convert.ToInt32(dgv_mostrar_beneficiario.CurrentRow.Cells["codigo_fliar"].Value)).Single().SOCFLIA_PARENT;
        switch (cod_parentesco)
        {
          case 1:
            cbx_benef_parentesco.SelectedIndex = 0; // conyugue
            break;
          case 2:
            cbx_benef_parentesco.SelectedIndex = 1; //hijo > 16
            break;
          case 3:
            cbx_benef_parentesco.SelectedIndex = 2; //hijo > 18
            break;
          case 4:
            cbx_benef_parentesco.SelectedIndex = 3; //hijo < 21
            break;
          case 5:
            cbx_benef_parentesco.SelectedIndex = 4; //hijo > 21
            break;
          default:
            break;
        }

        txt_benef_edad.Text = var_dato_benef.Single().edad.ToString();
        swt_benef_estudia.Value = var_dato_benef.Single().estudia == 0 ? false : true;

        //swt_benef_activo.Value = db_socios.soccen.Where(x=>x.SOCCEN_CUIL == )
        swt_benef_discapa.Value = (var_dato_benef.Single().discapacitado == 0) ? false : true;

      }
      else
      {
        limpiar_datos_beneficiario();

      }
    }

    private void limpiar_datos_beneficiario()
    {
      txt_benef_ape.Text = "";
      txt_benef_nombre.Text = "";
      txt_benef_dni.Text = "";
      dtp_fnac.Text = "";
      txt_benef_edad.Text = "";
      dtp_fingreso.Text = "";
      cbx_benef_sexo.Text = "";
      txt_benef_edad.Text = "";
      swt_benef_estudia.Value = false;
      swt_benef_discapa.Value = false;
    }

    private string obtener_estadocivil(char estcivil)
    {
      string estadovicivil = string.Empty;
      switch (estcivil)
      {
        case 'S':
          cbx_benef_estado_civil.SelectedIndex = 0; //soltero
          estadovicivil = "Soltero";
          break;
        case 'C':
          cbx_benef_estado_civil.SelectedIndex = 1; //casado
          estadovicivil = "Casado";
          break;
        case 'B':
          cbx_benef_estado_civil.SelectedIndex = 2; //cocubino
          estadovicivil = "Concubino";
          break;
        case 'D':
          cbx_benef_estado_civil.SelectedIndex = 3; //divorciado
          estadovicivil = "Divorciado";
          break;
        case 'V':
          cbx_benef_estado_civil.SelectedIndex = 4; //Viudo
          estadovicivil = "Viudo";
          break;
        default:
          break;
      }
      return estadovicivil;
    }

    private string obtener_localidad(int codloc)
    {
      if (codloc != 0)
      {
        return db_socios.localidad.Where(x => x.MAELOC_CODLOC == codloc).First().MAELOC_NOMBRE.Trim();
      }
      else
      {
        return "Sin Localidad Asignada";
      }
    }

    private void mostrar_historial_servicios(string dni)
    {
      var serv = (from servi in db_socios.recibos.Where(x => x.dni.Equals(dni))
                  select new
                  {
                    serv_fecha = servi.PERIODO,   //servi.PERIODO.Value.Day + "/"+ servi.PERIODO.Value.Year,
                    serv_descrip = servi.CONCEPTO
                  }).OrderBy(x => x.serv_fecha);
      dgv_historial_servicios.DataSource = serv.ToList();
      if (dgv_historial_servicios.Rows.Count >= 1) // me posiciono en la ultima fila del dgv_aportes
      {
        dgv_historial_servicios.CurrentCell = dgv_historial_servicios.Rows[dgv_historial_servicios.Rows.Count - 1].Cells[0];
      }
    }

    private void mostrar_datos_socio(string dni)
    {
      using (var context = new lts_sindicatoDataContext())
      {

        var datos_socio = context.maesoc.Where(x => x.MAESOC_NRODOC == dni).First();    //db_socios.socios.Where(x => x.DNI.Equals(dni)).First();

        convertir_imagen cnvimg = new convertir_imagen();
        socios soc = new socios();

        btn_sin_imagen.Visible = false;
        picbox_socio.Image = cnvimg.ByteArrayToImage(soc.get_foto_titular_binary(datos_socio.MAESOC_CUIL).ToArray());

        lbl_nro_socio.Text = datos_socio.MAESOC_NROAFIL.ToString();
        lbl_nombre.Text = datos_socio.MAESOC_APELLIDO.Trim() + " " + datos_socio.MAESOC_NOMBRE.Trim();
        lbl_dni.Text = datos_socio.MAESOC_NRODOC;
        lbl_cuil.Text = datos_socio.MAESOC_CUIL.ToString();
        lbl_sexo.Text = datos_socio.MAESOC_SEXO.ToString();
        lbl_empresa.Text = dgv_mostrar_socios.CurrentRow.Cells[3].Value.ToString();
        lbl_cuit.Text = dgv_mostrar_socios.CurrentRow.Cells["CUIT_"].Value.ToString().Trim();
        lbl_domicilio.Text = datos_socio.MAESOC_BARRIO + " " + datos_socio.MAESOC_CALLE.Trim() + " " + datos_socio.MAESOC_NROCALLE;
        lbl_localidad.Text = obtener_localidad(datos_socio.MAESOC_CODLOC);
        lbl_codigo_postal.Text = datos_socio.MAESOC_CODPOS;
        lbl_edad.Text = calcular_edad(datos_socio.MAESOC_FECHANAC).ToString();
        lbl_telefono.Text = datos_socio.MAESOC_TELCEL;
        lbl_estado_civil.Text = obtener_estadocivil(datos_socio.MAESOC_ESTCIV);
        var es_socio = db_socios.soccen.Where(x => x.SOCCEN_CUIL == datos_socio.MAESOC_CUIL).Single();
        if (es_socio.SOCCEN_ESTADO == 1)
        {
          if (datos_socio.MAESOC_NROAFIL.Trim() == "")
          {
            lbl_estado_socio.Text = "PASIVO";
            //btn_colonia.Enabled = true;
          }
          else
          {
            lbl_estado_socio.Text = "ACTIVO";
            //btn_colonia.Enabled = true;
          }
        }
        else
        {
          lbl_estado_socio.Text = "NO SOCIO";
          //btn_colonia.Enabled = false;
        }
      }


    }

    private void mostrar_ddjj(Int64 cuil, Int64 cuite) // Calculo los aportes del socio segun la declaracion jurada de la empresa
    {
      using (var context = new lts_sindicatoDataContext())
      {
        var ddjj = (from dj in context.ddjj
                    join empr in context.maeemp on dj.cuite equals empr.MAEEMP_CUIT
                    where dj.cuil == cuil
                    select new
                    {
                      periodo = dj.periodo,
                      aporte_ley = (dj.impo + dj.impoaux) * 0.02,
                      aporte_socios = (dj.item2 == true) ? (dj.impo + dj.impoaux) * 0.02 : 0,
                      cuit = dj.cuite,
                      razons = empr.MAEEMP_RAZSOC
                      //aporte_pagado = (db_socios.ddjjt.Where(x => x.periodo == dj.periodo).Max(x => x.rect))// (djt.fpago.Value == null) ? "NO":"SI"
                    }).OrderBy(x => x.periodo);
        dgv_mostrar_aportes.DataSource = ddjj.ToList();

        if (dgv_mostrar_aportes.Rows.Count >= 1) // me posiciono en la ultima fila del dgv_aportes
        {
          dgv_mostrar_aportes.CurrentCell = dgv_mostrar_aportes.Rows[dgv_mostrar_aportes.Rows.Count - 1].Cells[0];
          //dgv_mostrar_aportes.Rows[dgv_mostrar_aportes.Rows.Count - 1].Selected = true;
          dgv_mostrar_aportes.Rows[dgv_mostrar_aportes.Rows.Count - 1].Selected = true;
        }
      }
    }

    private int calcular_edad(DateTime fecha_nac)
    {
      int edad = 0;
      DateTime fecha_actual = DateTime.Today;
      edad = fecha_actual.Year - fecha_nac.Year;
      if ((fecha_actual.Month < fecha_nac.Month) || (fecha_actual.Month == fecha_nac.Month && fecha_actual.Day < fecha_nac.Day))
      {
        edad--;
      }
      return edad;
    }

    private void btn_inspectores_Click(object sender, EventArgs e)
    {
      //frm_inspecciones f_inspecciones = new frm_inspecciones();
      //f_inspecciones.ShowDialog();
      frm_actas f_actas_asig = new frm_actas();
      f_actas_asig.Show();
    }

    private void mysql_conex()
    {
      MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
      builder.Server = "localhost";
      builder.UserID = "root";
      builder.Password = "root";
      builder.Database = "fotos_";
      MySqlConnection conn = new MySqlConnection(builder.ToString());
      //MySqlCommand cmd = conn.CreateCommand();

      conn.Open();
      MySqlCommand command = new MySqlCommand();
      command.Connection = conn;
      MySqlDataAdapter da = new MySqlDataAdapter();
      string sqlstr = "select fotos.FOTOS_FOTO, concat(maeflia.MAEFLIA_NRODOC, ' ', maeflia.MAEFLIA_APELLIDO) , maeflia.MAEFLIA_NOMBRE,socflia.SOCFLIA_PARENT from maesoc join socflia on socflia.SOCFLIA_CUIL = maesoc.maesoc_cuil join maeflia on maeflia.MAEFLIA_CODFLIAR = socflia.SOCFLIA_CODFLIAR left join fotos on fotos.FOTOS_CODFLIAR = maeflia.MAEFLIA_CODFLIAR where maesoc.maesoc_nrodoc = " + dgv_mostrar_socios.CurrentRow.Cells[1].Value.ToString();// 27269547761";
      da.SelectCommand = new MySqlCommand(sqlstr, conn);
      DataTable tabla = new DataTable();
      da.Fill(tabla);
      BindingSource bndg_source = new BindingSource();
      dgv_excell.DataSource = tabla;
      conn.Close();

      //cmd.ExecuteNonQuery();
    }

    private void Btn_mostrar_cupones_Click(object sender, EventArgs e)
    {
      mysql_conex();
    }

    private void btn_odontologo_Click(object sender, EventArgs e)
    {
      Frm_socios f_socios = new Frm_socios();
      f_socios.Show();
    }

    private void dgv_mostrar_socios_SelectionChanged(object sender, EventArgs e)
    {
      mostrar_datos_socio(dgv_mostrar_socios.CurrentRow.Cells[1].Value.ToString());
      mostrar_benef(dgv_mostrar_socios.CurrentRow.Cells[1].Value.ToString());
      mostrar_historial_servicios(dgv_mostrar_socios.CurrentRow.Cells[1].Value.ToString());
      var cuil_s = db_socios.maesoc.Where(x => x.MAESOC_NRODOC == dgv_mostrar_socios.CurrentRow.Cells[1].Value.ToString());// .Equals(dni)).First();
      Int64 cuil_socio = 0;
      if (cuil_s.Count() > 0)
      {
        cuil_socio = Convert.ToInt64(cuil_s.Where(x => x.MAESOC_NRODOC == dgv_mostrar_socios.CurrentRow.Cells[1].Value.ToString()).FirstOrDefault().MAESOC_CUIL);
      }
      var cuit_s = db_socios.socemp.Where(x => x.SOCEMP_CUIL == cuil_socio && x.SOCEMP_ULT_EMPRESA == 'S');
      Int64 cuit_empresa = 0;
      if (cuit_s.Count() > 0)
      {
        cuit_empresa = Convert.ToInt64(cuit_s.Where(x => x.SOCEMP_CUIL == cuil_socio && x.SOCEMP_ULT_EMPRESA == 'S').FirstOrDefault().SOCEMP_CUITE);
      }
      mostrar_ddjj(cuil_socio, cuit_empresa);
    }

    private void dgv_mostrar_beneficiario_SelectionChanged(object sender, EventArgs e)
    {
      Int64 cod_f = Convert.ToInt64(dgv_mostrar_beneficiario.CurrentRow.Cells[2].Value.ToString());

      convertir_imagen cnvimg = new convertir_imagen();
      socios soc = new socios();
      btn_sin_imagen_benef.Visible = false;

      picbox_beneficiario.Image = cnvimg.ByteArrayToImage(soc.get_foto_benef_binary(cod_f).ToArray());

      //mostrar_foto_benef(cod_f);
      mostrar_benef_datos(cod_f);
    }

    private void mostrar_foto_benef(Int64 cod_fliar)
    {
      var var_buscar_foto = db_socios.fotos.Where(x => x.FOTOS_CODFLIAR == cod_fliar);//x.FOTOS_CUIL == datos_socio.MAESOC_CUIL && x.FOTOS_CODFLIAR == 0);
      if (var_buscar_foto.Count() > 0)
      {
        picbox_beneficiario.Image = ByteArrayToImage(var_buscar_foto.Single().FOTOS_FOTO.ToArray());
        btn_sin_imagen_benef.Visible = false;
      }
      else
      {
        picbox_beneficiario.Image = null;
        btn_sin_imagen_benef.Visible = true;
      }
    }

    private void dgv_mostrar_aportes_SelectionChanged(object sender, EventArgs e)
    {
      using (var context = new lts_sindicatoDataContext())
      {
        var empresa = from a in context.maeemp where a.MEEMP_CUIT_STR == dgv_mostrar_aportes.CurrentRow.Cells["cuit"].Value.ToString().Trim() select a;
        if (empresa.Count() > 0)
        {
          lbl_aporte_empresa.Text = empresa.SingleOrDefault().MAEEMP_RAZSOC.ToString();
          lbl_cuit_aporte.Text = dgv_mostrar_aportes.CurrentRow.Cells[3].Value.ToString();
        }
      }
    }

    private void swt_discapa_OnValueChange(object sender, EventArgs e)
    {
      if (swt_benef_discapa.Value == true)
      {
        lbl_discapa.Text = "SI";
      }
      else
      {
        lbl_discapa.Text = "NO";
      }
    }

    private void swt_estudia_OnValueChange_1(object sender, EventArgs e)
    {
      if (swt_benef_estudia.Value == true)
      {
        lbl_estudia.Text = "SI";
      }
      else
      {
        lbl_estudia.Text = "NO";
      }
    }

    private void swt_activo_OnValueChange(object sender, EventArgs e)
    {
      if (swt_benef_activo.Value == true)
      {
        lbl_activo.Text = "SI";
      }
      else
      {
        lbl_activo.Text = "NO";
      }
    }

    private void dgv_mostrar_socios_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {

    }

    private void btn_futbol_Click(object sender, EventArgs e)
    {
      frm_futbol f_futbol = new frm_futbol();
      f_futbol.Show();
    }

    private void bunifuFlatButton2_Click(object sender, EventArgs e)
    {

    }

    private void btn_edades_Click(object sender, EventArgs e)
    {
      frm_edades f_edades = new frm_edades();
      f_edades.Show();
      //var partidos_ = from a in db_socios.partidos where a.PARTIDOID == Convert.ToInt32(fila.Cells["idPartido"].Value) select a;
      //var jugadores_equipo_1 = (from a in db_socios.jugadores
      //                          where a.JUG_EQUIPOID == 1
      //                          join ju in db_socios.maesoc on a.JUG_SOCCENCUIL equals ju.MAESOC_CUIL
      //                          join fo in db_socios.fotos on a.JUG_SOCCENCUIL equals fo.FOTOS_CUIL
      //                          join ex in db_socios.jugadores_exepciones on a.JUG_EQUIPOID equals ex.equipo_ID into fff
      //                          from ex in fff.DefaultIfEmpty()
      //                              // join f in DB_soc_mysql.fotos on a.SOCFLIA_CODFLIAR equals f.FOTOS_CODFLIAR into fo //aqui se aplica el left join de sql
      //                              //from f in fo.DefaultIfEmpty()

      //                          where fo.FOTOS_CODFLIAR == 0
      //                          select new
      //                          {
      //                              campeonato = db_socios.campeonatos.Where(x => x.CAMPID == partidos.Single().PARTIDO_CAMPID).Single().CAMPNOMBRE,
      //                              equipo = db_socios.Cells[1].Value.ToString(),
      //                              fecha = partidos.Single().PARTIDOFECHA,
      //                              hora = partidos.Single().PARTIDOHORA,
      //                              fase = db_socios.fases.Where(x => x.FASE_ID == partidos.Single().PARTIDO_FASEID).Single().FASENOMBRE,
      //                              categoria = fila.Cells[4].Value.ToString(),
      //                              cancha = db_socios.canchas.Where(x => x.CANCHAID == partidos.Single().PARTIDO_CANCHAID).Single().CANCHANOMBRE,
      //                              partido = partidos.Single().PARTIDOID,
      //                              nrosocio = ju.MAESOC_NROAFIL,
      //                              apellido = ju.MAESOC_APELLIDO.Trim(),
      //                              nombre = ju.MAESOC_NOMBRE.Trim(),
      //                              dni = ju.MAESOC_NRODOC,
      //                              //foto = db_sindicato.fotos.Where(x => x.FOTOS_CUIL == ju.MAESOC_CUIL && x.FOTOS_CODFLIAR == 0).Count() > 0 ? fo.FOTOS_FOTO : null,
      //                              foto = db_socios.fotos.Where(x => x.FOTOS_CUIL == ju.MAESOC_CUIL && x.FOTOS_CODFLIAR == 0).Count() > 0 ? fo.FOTOS_FOTO : null,
      //                              nrofecha = partidos.Single().PARTIDONROFECHA
      //                          }).OrderBy(x => x.apellido);

    }

    private void dgv_mostrar_beneficiario_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {

    }

    private void btn_masajista_MouseMove(object sender, MouseEventArgs e)
    {

    }

    private void bunifuFlatButton1_Click(object sender, EventArgs e)
    {
      frm_ingreso_poli f_ing_poli = new frm_ingreso_poli();
      f_ing_poli.ShowDialog();
    }

    private void btn_presente_Click(object sender, EventArgs e)
    {
      string dni = lbl_dni.Text.Trim();
      int ya_ingresado = db_socios.eventos.Where(x => x.eventos_nombre == dni).Count();
      if (ya_ingresado > 0)
      {
        MessageBox.Show("Socio ya ingreso a la ASAMBLEA");
      }
      else
      {
        eventos evento = new eventos();
        evento.eventos_nombre = lbl_dni.Text.Trim();
        evento.eventos_estado = Convert.ToInt32(lbl_nro_socio.Text.Trim());
        db_socios.eventos.InsertOnSubmit(evento);
        db_socios.SubmitChanges();
        Txt_dni.Text = "";
      }
      Txt_dni.Focus();
    }

    private void btn_para_inspeccion_Click(object sender, EventArgs e)
    {
      para_inspeccionar f_para_Inspeccionar = new para_inspeccionar(Convert.ToDouble(lbl_cuil.Text.Trim()));
      f_para_Inspeccionar.txt_dni.Text = lbl_dni.Text;
      f_para_Inspeccionar.txt_cuil.Text = lbl_cuil.Text;
      f_para_Inspeccionar.txt_cuit.Text = lbl_cuit.Text;
      f_para_Inspeccionar.txt_empresa.Text = lbl_empresa.Text;
      f_para_Inspeccionar.txt_nombre.Text = lbl_nombre.Text;
      f_para_Inspeccionar.txt_socio.Text = lbl_nro_socio.Text;
      f_para_Inspeccionar.id_usuario = user_id;
      f_para_Inspeccionar.Show();
    }

    private void btn_imprimir_aportes_Click(object sender, EventArgs e)
    {
      imprimir_aportes();
    }

    private void imprimir_aportes()
    {
      try
      {
        using (var context = new lts_sindicatoDataContext())
        {
          DS_cupones dscpn = new DS_cupones();
          DataTable dt_impresion_ddjj = dscpn.ddjj_por_empleado;
          dt_impresion_ddjj.Clear();
          foreach (DataGridViewRow fila in dgv_mostrar_aportes.Rows)
          {
            DataRow row = dt_impresion_ddjj.NewRow();

            row["periodo"] = Convert.ToDateTime(fila.Cells["periodo"].Value).Date;
            row["ley"] = Convert.ToDecimal(fila.Cells["aporte_ley"].Value);
            row["socio"] = Convert.ToDecimal(fila.Cells["aporte_socio"].Value);
            row["empresa"] = fila.Cells["razons"].Value;
            row["cuit"] = fila.Cells["cuit"].Value;
            row["dni"] = Convert.ToInt32(lbl_dni.Text.Trim());
            row["empleado"] = lbl_nombre.Text.Trim();
            dt_impresion_ddjj.Rows.Add(row);
          }
          reportes frm_reportes = new reportes();
          frm_reportes.nombreReporte = "rpt_ddjj_por_empleado";
          frm_reportes.ddjj_por_empleado = dt_impresion_ddjj;
          frm_reportes.Show();
        }
      }
      catch (Exception)
      {

        throw;
      }


      //impresion_comprobante insert = new impresion_comprobante();
      //try
      //{
      //    foreach (DataGridViewRow fila in dgv_mostrar_aportes.Rows)
      //    {
      //        insert.fecha1 = Convert.ToDateTime(fila.Cells["periodo"].Value).Date;
      //        insert.aporte_ley = Convert.ToDecimal(fila.Cells["aporte_ley"].Value);
      //        insert.aporte_socio = Convert.ToDecimal(fila.Cells["aporte_socio"].Value);
      //        db_socios.impresion_comprobante.InsertOnSubmit(insert);
      //    }
      //    db_socios.SubmitChanges();
      //}
      //catch (Exception)
      //{
      //    throw;
      //}
    }

    private void btn_minimizar_Click(object sender, EventArgs e)
    {
      this.WindowState = FormWindowState.Minimized;
    }

    private void btn_ActivarSocio_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show("Esta seguro de pasar a ''SOCIO ACTIVO'' ", "¡¡¡ ATENCION !!!", MessageBoxButtons.YesNo) == DialogResult.Yes)
      {
        //socios soc = new socios();
        //soc.ActivarSocio(Convert.ToDouble( lbl_cuil.Text.Trim()));

        //var activar_socio = from a in db_socios.soccen where a.SOCCEN_CUIL == Convert.ToDouble(lbl_cuil.Text) select a;

        using (lts_sindicatoDataContext context = new lts_sindicatoDataContext())
        {
          context.ExecuteCommand("update fotos_.soccen set SOCCEN_ESTADO = 1 where SOCCEN_CUIL = " + Convert.ToDouble(lbl_cuil.Text));
        }

        //if (activar_socio.Count() > 0)
        //{
        //  activar_socio.SingleOrDefault().SOCCEN_ESTADO = 1;
        //  db_socios.SubmitChanges();

        MessageBox.Show("El Socio " + lbl_nombre.Text.Trim() + " Ya se encuentra activado. Por favor Actualice la Busqueda. ", "¡¡¡ ATENCION !!!");
        //}
      }
    }

    private void btn_prueba_Click(object sender, EventArgs e)
    {

      //frm_Prueba f_prueba = new frm_Prueba();

      //f_prueba.Show();
      frm_cobranzas f_cobranzas = new frm_cobranzas();
      f_cobranzas.Show();

    }
  }
}
