using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using entrega_cupones.Clases;
using System.Drawing.Imaging;
using Microsoft.Reporting.WebForms;

namespace entrega_cupones.Formularios
{
  public partial class frm_Prueba : Form
  {
    public frm_Prueba()
    {
      InitializeComponent();
    }

    private void frm_Prueba_Load(object sender, EventArgs e)
    {
      aleToolStripMenuItem.BackColor = Color.Azure;
    }

    private void btn_Imprimir_Click(object sender, EventArgs e)
    {
      try
      {   //Instanciamos un LocalReport, le indicamos el report a imprimir y le cargamos los datos
        LocalReport rdlc = new LocalReport();
        rdlc.ReportEmbeddedResource = "entrega_cupones.Reportes.rpt_EntradaInvitadoDDEDC.rdlc";
        rdlc.DataSources.Add(new ReportDataSource("DataSet1", LoadSalesData()));
        //Imprime el report
        Impresor imp = new entrega_cupones.Impresor();

        imp.Imprime(rdlc);
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }
    private DataTable LoadSalesData()
    {
      DS_cupones Ds = new DS_cupones();
      DataTable Dt = Ds.EntradaDDEDC;
      Dt.Clear();

      DataRow Dr = Dt.NewRow();
      Dr["NumeroDeRecibo"] = 1;
      Dt.Rows.Add(Dr);
      return Dt;

    }

    private void btn_ComprimirImagen_Click(object sender, EventArgs e)
    {

    }

    private void btn_ListarImagenes_Click(object sender, EventArgs e)
    {
      using (var context = new lts_sindicatoDataContext())
      {

        // D:\BackUp\SEC_Fotos_Afiliados



        //string destino = @"D:\BackUp\SEC_Fotos_Afiliados\" + txtNombres.Text + ".Jpeg";
        //picFoto.Image.Save(destino, ImageFormat.Jpeg);

        //convertir_imagen CnvImg = new convertir_imagen();
        var fotos = from a in context.fotos.Where(x => x.FOTOS_CODFLIAR == 0)//.Take(10) //where a.FOTOS_CODFLIAR == 0 Take(10)
                    select new
                    {
                      cuil = a.FOTOS_CUIL,
                      tamaño = a.FOTOS_FOTO.Length,
                      tamaño2 = a.FOTOS_FOTO.Length / 1024//CnvImg.ByteArrayToImage(a.FOTOS_FOTO.ToArray()).Size.ToString()
                    };
        dgv1.DataSource = fotos.ToList();
        lbl_TotalFotos.Text = fotos.Count().ToString();

      }
    }

    private void btn_GuardarFoto_Click(object sender, EventArgs e)
    {
      using (var context = new lts_sindicatoDataContext())
      {
        string destino = string.Empty; //@"D:\BackUp\SEC_Fotos_Afiliados\" + txtNombres.Text + ".Jpeg";
        //picFoto.Image.Save(destino, ImageFormat.Jpeg);

        Image Imagen;
        convertir_imagen cnvImg = new convertir_imagen();
        var fotos = from a in context.fotos.Where(x => x.FOTOS_CODFLIAR == 0)//.Take(100) //where a.FOTOS_CODFLIAR == 0 Take(10)
                    select new
                    {
                      foto = a.FOTOS_FOTO,
                      cuil = a.FOTOS_CUIL,
                      tamaño = a.FOTOS_FOTO.Length,
                      tamaño2 = a.FOTOS_FOTO.Length / 1024//CnvImg.ByteArrayToImage(a.FOTOS_FOTO.ToArray()).Size.ToString()
                    };

        foreach (var item in fotos.ToList())
        {
          Imagen = cnvImg.ByteArrayToImage(item.foto.ToArray());
          destino = @"D:\BackUp\SEC_Fotos_Afiliados\" + item.cuil + ".Jpeg";

          Bitmap newBmp = new Bitmap(Imagen);
          Imagen.Dispose();
          newBmp.Save(destino, ImageFormat.Jpeg);

        }
      }
    }

    private void btn_GuardarFotoBenef_Click(object sender, EventArgs e)
    {

      using (var context = new lts_sindicatoDataContext())
      {
        string PathCarpeta, PathFoto = string.Empty; //@"D:\BackUp\SEC_Fotos_Afiliados\" + txtNombres.Text + ".Jpeg";
        //picFoto.Image.Save(destino, ImageFormat.Jpeg);

        Image Imagen;
        convertir_imagen cnvImg = new convertir_imagen();

        var fotos = (from a in context.fotos//.Take(50)
                     where a.FOTOS_CODFLIAR > 0
                     group a by a.FOTOS_CUIL into benef
                     select new { Titular = benef.Key, FotoBenef = benef.ToList() });

        foreach (var tit in fotos.ToList())
        {
          if (tit.FotoBenef.Count() > 0)
          {
            PathCarpeta = @"D:\BackUp\SEC_Fotos_Beneficiarios\" + tit.Titular.ToString();
            DirectoryInfo di = Directory.CreateDirectory(PathCarpeta);
            foreach (var benef in tit.FotoBenef)
            {
              Imagen = cnvImg.ByteArrayToImage(benef.FOTOS_FOTO.ToArray());
              PathFoto = PathCarpeta + "\\" + benef.FOTOS_CODFLIAR.ToString() + ".Jpeg";
              Bitmap newBmp = new Bitmap(Imagen);
              Imagen.Dispose();
              newBmp.Save(PathFoto, ImageFormat.Jpeg);
            }
          }
        }
      }
    }

    private void btn_CopiarFotos_Click(object sender, EventArgs e)
    {
      convertir_imagen cnvImg = new convertir_imagen();
      Image fto;

      using (var context = new lts_sindicatoDataContext())
      {
        var fotos = from a in context.fotos.Where(x => x.FOTOS_CODFLIAR > 0)//.Take(30) //where a.FOTOS_CODFLIAR == 0 Take(10)
                    select a;

        if (fotos.Count() > 0)
        {
          lbl_Inicio.Text = DateTime.Now.ToLongTimeString();
          foreach (var foto in fotos)
          {
            using (var context2 = new lts_sindicatoDataContext())
            {
              fotos2 FotoInsert = new fotos2();
              FotoInsert.FOTOS_CUIL = foto.FOTOS_CUIL;
              FotoInsert.FOTOS_CODFLIAR = foto.FOTOS_CODFLIAR;
              FotoInsert.FOTOS_FECHA = foto.FOTOS_FECHA;
              fto = cnvImg.ByteArrayToImage(foto.FOTOS_FOTO.ToArray());
              FotoInsert.FOTOS_FOTO = cnvImg.ConvertImageToByteArray(fto);
              context2.fotos2.InsertOnSubmit(FotoInsert);
              context2.SubmitChanges();
            }
          }
          lbl_Fin.Text = DateTime.Now.ToLongTimeString();
        }
      }
    }

    private void btn_Join_Click(object sender, EventArgs e)
    {
      using (var context = new lts_sindicatoDataContext())
      {
        //var ActaCobrador = from a in context.ACTAS
        //                   join b in context.AsignarCobranza
        //                   on a.ACTA equals b.Acta
        //                   into c
        //                   from d in c.DefaultIfEmpty()
        //                   select new
        //                   {
        //                     Acta = a.ACTA, 
        //                     cobrador = (int?)d.CobradorID 
        //                   };
        //dgv1.DataSource = ActaCobrador.ToList();

        cobranzas cbr = new cobranzas();
        string inspector = "HERRERA";

        var IntervaloDeFechas = cbr.ObtenerIntervaloDeFechas(msk_Desde.Text, msk_Hasta.Text);

        //var actas = from a in context.ACTAS
        //            join b in context.AsignarCobranza
        //                  on a.ACTA equals b.Acta
        //                  into c
        //            from d in c.DefaultIfEmpty()
        //            join f in context.Cobradores
        //                  on d.CobradorID equals f.ID
        //                  into g
        //            from h in g.DefaultIfEmpty()
        //            where a.ACTA > 0 &&
        //                  inspector == "TODOS" ? a.INSPECTOR != "TODOS" : a.INSPECTOR == inspector  &&
        //                  a.DEUDATOTAL > 0 &&
        //                  a.FECHA >= IntervaloDeFechas.Desde &&
        //                  a.FECHA <= IntervaloDeFechas.Hasta.Date
        //            orderby a.FECHA
        //            select new
        var actas = from a in context.ACTAS
                    join b in context.AsignarCobranza
                          on a.ACTA equals b.Acta
                          into c
                    from d in c.DefaultIfEmpty()
                    join f in context.Cobradores
                          on d.CobradorID equals f.ID
                          into g
                    from h in g.DefaultIfEmpty()
                    where a.ACTA > 0 &&
                          inspector == "TODOS" ? a.INSPECTOR != "TODOS" : a.INSPECTOR == inspector &&
                          a.DEUDATOTAL > 0 &&
                          a.FECHA >= IntervaloDeFechas.Desde && a.FECHA <= IntervaloDeFechas.Hasta.Date

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
                      h.Nombre,
                      ImporteInteresActualizado = cbr.ObtenerImporteDeInteres(Convert.ToDateTime(a.FECHA), (a.DIFERENCIA < 0) ? Convert.ToDecimal(a.DIFERENCIA * -1) : 0, 3),
                      ImporteDeudaActualizada = cbr.ObtenerDeudaTotalConInteres(Convert.ToDateTime(a.FECHA), (a.DIFERENCIA < 0) ? Convert.ToDecimal(a.DIFERENCIA * -1) : 0, 3, Convert.ToDecimal(a.DIFERENCIA))
                    };

        dgv1.DataSource = actas.ToList();
        lbl_CantidadDeActas.Text = actas.Count().ToString();

      }
    }

    private void btn_CalcularInteres_Click(object sender, EventArgs e)
    {
      calcular_coeficientes coef = new calcular_coeficientes();
      txt_interes.Text = coef.CalcularInteresResarcitorio(Convert.ToDateTime("01/" + msk_Desde.Text), Convert.ToDateTime("01/" + msk_Hasta.Text), 0, Convert.ToDouble(txt_importe.Text), 1, 11).ToString();
    }

    private void btn_empleados_Click(object sender, EventArgs e)
    {
      ImprimirEmpleados(chk_PorEmpresa.Checked == true ? Convert.ToDouble(txt_CUIT.Text):0);
    }

    private void ImprimirEmpleados(double cuit)
    {
      using (var context = new lts_sindicatoDataContext())
      {

        DateTime desde = Convert.ToDateTime("01/" + msk_Desde.Text);
        DateTime hasta = Convert.ToDateTime("01/" + msk_Hasta.Text);
        double sueldo = txt_Sueldo.Text == "" ? 0 : Convert.ToDouble(txt_Sueldo.Text);

        var empresa_ = context.maeemp;

        var agrupado = (from a in context.ddjj
                       where
                       (cuit > 0 ? (a.cuite == cuit) : a.cuite > 0) &&
                       (a.periodo >= desde && a.periodo <= hasta) &&
                       (cbx_Sueldo.Text == "DESDE" ? a.impo >= sueldo :
                       cbx_Sueldo.Text == "HASTA" ? a.impo <= sueldo :
                       cbx_Sueldo.Text == "IGUAL A" ? a.impo == sueldo : a.impo >= 0) &&
                       (cbx_Jornada.Text == "COMPLETA" ? a.jorp == false :
                       cbx_Jornada.Text == "PARCIAL" ? a.jorp == true : a.jorp == true || a.jorp == false)
                       group a by a.cuite into g
                       select new 
                       {
                         CUIT = g.Key, 
                         cantidad = g.ToList().Count(), 
                         empresa = empresa_.Where(x => x.MAEEMP_CUIT == g.Key).SingleOrDefault().MAEEMP_RAZSOC.Trim(), 
                         domicilio = empresa_.Where(x => x.MAEEMP_CUIT == g.Key).SingleOrDefault().MAEEMP_CALLE.Trim()  + " Nº: " + empresa_.Where(x => x.MAEEMP_CUIT == g.Key).SingleOrDefault().MAEEMP_NRO.Trim() 
                       }
                       ).OrderByDescending(x => x.cantidad);
        dgv1.DataSource = agrupado;
        lbl_CantidadDeActas.Text = agrupado.Count().ToString();
      }
    }

    private void dgv1_SelectionChanged(object sender, EventArgs e)
    {
      MostrarDetalleEmpleados(Convert.ToDouble(dgv1.CurrentRow.Cells[0].Value));
    }

    private void MostrarDetalleEmpleados(double cuit)
    {
      using (var context = new lts_sindicatoDataContext())
      {
        DateTime desde = Convert.ToDateTime("01/" + msk_Desde.Text);
        DateTime hasta = Convert.ToDateTime("01/" + msk_Hasta.Text);
        double sueldo = txt_Sueldo.Text == "" ? 0 : Convert.ToDouble(txt_Sueldo.Text);


        var q = from s in context.ddjj
                where
                (s.periodo >= desde && s.periodo <= hasta)
                group s by s.cuil into g
                select new { cuil = g.Key, MaxRect = g.Max(s => s.rect) };


        var dj = from a in context.ddjj
                 join b in q on a.cuil equals b.cuil
                 where (a.periodo >= desde && a.periodo <= hasta)
                 select a;

        //dgv2.DataSource = dj;

        var empl = (from a in dj
                    join datos in context.maesoc on a.cuil equals datos.MAESOC_CUIL
                    join cat in context.categorias_empleado on datos.MAESOC_CODCAT equals cat.MAECAT_CODCAT
                    join act in context.actividad on datos.MAESOC_CODACT equals act.MAEACT_CODACT
                    join empresa in context.socemp on a.cuil equals empresa.SOCEMP_CUIL

                    where

                    empresa.SOCEMP_ULT_EMPRESA == 'S' &&
                    (a.cuite == cuit) &&
                    (a.periodo >= desde && a.periodo <= hasta) &&
                    (cbx_Sueldo.Text == "DESDE" ? a.impo >= sueldo :
                    cbx_Sueldo.Text == "HASTA" ? a.impo <= sueldo :
                    cbx_Sueldo.Text == "IGUAL A" ? a.impo == sueldo : a.impo >= 0) &&
                    (cbx_Jornada.Text == "COMPLETA" ? a.jorp == false :
                    cbx_Jornada.Text == "PARCIAL" ? a.jorp == true : a.jorp == true || a.jorp == false)

                    select new

                    {
                      periodo = a.periodo,
                      CUIL = String.Format("{0:g}", a.cuil),
                      nombre = (datos.MAESOC_APELLIDO.Trim() + " " + datos.MAESOC_NOMBRE.Trim()),
                      aporte_ley = (a.impo + a.impoaux) * 0.02,
                      aporte_socio = (a.item2 == true) ? ((a.impo + a.impoaux) * 0.02) : 0,
                      sueldo = a.impo,
                      jornada = (a.jorp == true) ? "JP" : "JC",
                      categoria = cat.MAECAT_NOMCAT.Trim(),
                      acuerdo = a.impoaux,
                      licencia = (a.lic != "0000") ? "SI" : "NO",
                      fecha_ing = (empresa.SOCEMP_FECHAING == null) ? " " : empresa.SOCEMP_FECHAING.ToString(),
                      fecha_baja = (empresa.SOCEMP_FECHABAJA == null) ? " " : empresa.SOCEMP_FECHABAJA.ToString()

                    }).OrderBy(x => x.periodo).ThenBy(x => x.nombre).ToList();

        dgv2.DataSource = empl;
      }
    }

    private void chk_PorEmpresa_CheckedChanged(object sender, EventArgs e)
    {
      if (chk_PorEmpresa.Checked == true)
      {

      }
    }

    private void btn_BuscarEmpresa_Click(object sender, EventArgs e)
    {
      frm_buscar_empresa f_busc_emp = new frm_buscar_empresa();
      f_busc_emp.DatosPasadosPrueba += new frm_buscar_empresa.PasarDatosPrueba(ejecutar);
      f_busc_emp.viene_desde = 4;
      f_busc_emp.ShowDialog();

    }

    public void ejecutar(string empresa, string cuit)
    {
      txt_RazonSocial.Text = empresa;
      txt_CUIT.Text = cuit;
    }

    private void btn_VerMenuNuevo_Click(object sender, EventArgs e)
    {
      //frm_Principal2 f_Principal2 = new frm_Principal2();
      //f_Principal2.Show();
    }
  }
}



