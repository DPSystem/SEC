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
  public partial class frm_buscar_empresa : Form
  {
  
    public int viene_desde; // 1 - viene de desde actas / 2 - viene desde cobros
    public delegate void PasarDatos(string empresa, string domicilio, string cuit, string estudio, string telefono, string localidad);
    public event PasarDatos DatosPasados;

    public delegate void PasarDatosCobros(string cuit, string razon_social);
    public event PasarDatosCobros DatosPasadosCobros;

    public delegate void PasarDatosActa(string razon_social, string cuit);
    public event PasarDatosActa DatosPasadosCargarActa;

    public delegate void PasarDatosPrueba(string razon_social, string cuit);
    public event PasarDatosPrueba DatosPasadosPrueba;

    public delegate void PasarDatosFrmPrincipal(string razon_social, string cuit);
    public event PasarDatosFrmPrincipal PasarDatosFrmPrincipal_;

    public frm_buscar_empresa()
    {
      InitializeComponent();
    }
    lts_sindicatoDataContext DB_Sindicato = new lts_sindicatoDataContext();

    private void btn_cerrar_busqueda_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void frm_buscar_empresa_Load(object sender, EventArgs e)
    {
      txt_buscar_empresa.Focus();
      mostrar_todos();

    }

    private void mostrar_todos()
    {

      var empresas = (from a in DB_Sindicato.maeemp
                      select new
                      {
                        _CUIT = a.MAEEMP_CUIT,
                        _razonsocial = a.MAEEMP_RAZSOC,
                        _nombre_fantasia = a.MAEEMP_NOMFAN,
                        //_deuda_aproximada =  calcular_coeficientes()
                      }).OrderBy(x => x._razonsocial);
      dgv_buscar_empresas.DataSource = empresas.ToList();
      lbl_cantidad_empresas_encontradas.Text = "Total Empresas: " + empresas.Count().ToString();

    }

    private void txt_buscar_empresa_TextChanged(object sender, EventArgs e)
    {

      //var empresas = (from a in DB_Sindicato.maeemp
      //                    // where a.cuitstr.Contains(txt_buscar_empresa.Text.Trim()) ||
      //                where Convert.ToString(a.MEEMP_CUIT_STR).Contains(txt_buscar_empresa.Text.Trim()) ||
      //                a.MAEEMP_RAZSOC.Contains(txt_buscar_empresa.Text) ||
      //                a.MAEEMP_NOMFAN.Contains(txt_buscar_empresa.Text)//.Where( x => Convert.ToString(x.cuit).Any(x.cuit.ToString().Contains(txt_buscar_empresa.Text)))// .Contains(txt_buscar_empresa.Text) //  .Hierarchy.Any(x.cuit.ToString().Contains(txt_buscar_empresa.Text)) // .Contains(txt_buscar_empresa.Text))
      //                select new

      //                {
      //                    _CUIT = a.MAEEMP_CUIT,
      //                    _razonsocial = a.MAEEMP_RAZSOC,
      //                    _nombre_fantasia = a.MAEEMP_NOMFAN
      //                }

      //               ).OrderBy(x => x._razonsocial);
      //dgv_buscar_empresas.DataSource = empresas.ToList();
      //lbl_cantidad_empresas_encontradas.Text = "Total Empresas: " + empresas.Count().ToString();


    }

    private void buscar_empresas()
    {
      var empresas = (from a in DB_Sindicato.maeemp
                        // where a.cuitstr.Contains(txt_buscar_empresa.Text.Trim()) ||
                      where Convert.ToString(a.MEEMP_CUIT_STR).Contains(txt_buscar_empresa.Text.Trim()) ||
                      a.MAEEMP_RAZSOC.Contains(txt_buscar_empresa.Text) ||
                      a.MAEEMP_NOMFAN.Contains(txt_buscar_empresa.Text)//.Where( x => Convert.ToString(x.cuit).Any(x.cuit.ToString().Contains(txt_buscar_empresa.Text)))// .Contains(txt_buscar_empresa.Text) //  .Hierarchy.Any(x.cuit.ToString().Contains(txt_buscar_empresa.Text)) // .Contains(txt_buscar_empresa.Text))
                      select new
                      {
                        _CUIT = a.MAEEMP_CUIT,
                        _razonsocial = a.MAEEMP_RAZSOC,
                        _nombre_fantasia = a.MAEEMP_NOMFAN
                      }

                     ).OrderBy(x => x._razonsocial);
      dgv_buscar_empresas.DataSource = empresas.ToList();
      lbl_cantidad_empresas_encontradas.Text = "Total Empresas: " + empresas.Count().ToString();
    }

    private void dgv_buscar_empresas_SelectionChanged(object sender, EventArgs e)
    {
      var mostar_datos = (from a in DB_Sindicato.maeemp.Where(x => x.MAEEMP_CUIT == Convert.ToDouble(dgv_buscar_empresas.CurrentRow.Cells["cuit"].Value))

                          select new
                          {
                            _domicilio = a.MAEEMP_CALLE.Trim() + " Nº: " + a.MAEEMP_NRO.Trim(),
                            _telefono = a.MAEEMP_TEL.Trim(),
                            _telefono_estudio = a.MAEEMP_ESTUDIO_TEL,
                            _localidad = DB_Sindicato.localidad.Where(x => x.MAELOC_CODLOC == a.MAEEMP_CODLOC).First().MAELOC_NOMBRE,
                            _estudio = a.MAEEMP_ESTUDIO_CONTACTO.Trim() + " " + a.MAEEMP_ESTUDIO_TEL.Trim()
                          }).FirstOrDefault();
      txt_buscar_empresa_domicilio.Text = mostar_datos._domicilio;
      txt_buscar_empresa_telefono.Text = mostar_datos._telefono + " - " + mostar_datos._telefono_estudio;
      txt_localidad.Text = mostar_datos._localidad;
      txt_buscar_empresa_estudio.Text = mostar_datos._estudio;
    }

    private void btn_estado_empresa_Click(object sender, EventArgs e)
    {
      if (dgv_buscar_empresas.Rows.Count > 0)
      {
        pasar();
        this.Close();
      }
    }

    private void txt_buscar_empresa_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Down)
      {
        dgv_buscar_empresas.Focus();
      }

      if (e.KeyCode == Keys.Enter)
      {
        buscar_empresas();
        //pasar();
        //this.Close();
      }
    }

    private void pasar_datos()
    {
      if (dgv_buscar_empresas.Rows.Count > 0)
      {
        DatosPasados(
        dgv_buscar_empresas.CurrentRow.Cells["razonsocial"].Value.ToString(),
        txt_buscar_empresa_domicilio.Text,
        dgv_buscar_empresas.CurrentRow.Cells["cuit"].Value.ToString(),
        txt_buscar_empresa_estudio.Text,
        txt_buscar_empresa_telefono.Text,
        txt_localidad.Text
    );
      }
    }

    private void pasar_datos_cobros()
    {
      if (dgv_buscar_empresas.Rows.Count > 0)
      {
        DatosPasadosCobros(
        dgv_buscar_empresas.CurrentRow.Cells["cuit"].Value.ToString(),
        dgv_buscar_empresas.CurrentRow.Cells["razonsocial"].Value.ToString()
        );
      }

    }

    private void PasarDatosCargarActas()
    {
      if (dgv_buscar_empresas.Rows.Count > 0)
      {
        DatosPasadosCargarActa(
         dgv_buscar_empresas.CurrentRow.Cells["razonsocial"].Value.ToString().Trim(),
         dgv_buscar_empresas.CurrentRow.Cells["cuit"].Value.ToString()
       
        );
      }

    }

    private void PasarDatosPruebaDeListado()
    {
      if (dgv_buscar_empresas.Rows.Count > 0)
      {
        DatosPasadosPrueba(
         dgv_buscar_empresas.CurrentRow.Cells["razonsocial"].Value.ToString().Trim(),
         dgv_buscar_empresas.CurrentRow.Cells["cuit"].Value.ToString()
        );
      }

    }

    private void PasarDatosFrmPrincipal2()
    {
      if (dgv_buscar_empresas.Rows.Count > 0)
      {
        PasarDatosFrmPrincipal_(
         dgv_buscar_empresas.CurrentRow.Cells["razonsocial"].Value.ToString().Trim(),
         dgv_buscar_empresas.CurrentRow.Cells["cuit"].Value.ToString()
        );
      }

    }

    private void PasarDatosFrmVerificarDeuda()
    {
      VerificarDeuda formVerDeuda = Owner as VerificarDeuda;
      formVerDeuda.txt_BuscarEmpesa.Text = dgv_buscar_empresas.CurrentRow.Cells["RazonSocial"].Value.ToString().Trim();
      formVerDeuda.txt_CUIT.Text = dgv_buscar_empresas.CurrentRow.Cells["Cuit"].Value.ToString().Trim();
      formVerDeuda.txt_Domicilio.Text = txt_buscar_empresa_domicilio.Text.Trim();
      //formVerDeuda.txt_Domicilio.Text = dgv_buscar_empresas.CurrentRow.Cells["Domicilio"].Value.ToString().Trim() + " " +
      //  dgv_buscar_empresas.CurrentRow.Cells["Numero"].Value.ToString().Trim() + " " +
      //  dgv_buscar_empresas.CurrentRow.Cells["Localidad"].Value.ToString().Trim();
      Close();
    }

    private void dgv_buscar_empresas_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter)
      {
        pasar();
        this.Close();
      }
    }

    private void dgv_buscar_empresas_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      pasar();
      this.Close();
    }

    private void pasar()
    {
      if (viene_desde == 1) pasar_datos();
      if (viene_desde == 2) pasar_datos_cobros();
      if (viene_desde == 3) PasarDatosCargarActas();
      if (viene_desde == 4) PasarDatosPruebaDeListado();
      if (viene_desde == 5) PasarDatosFrmPrincipal2();
      if (viene_desde == 6) PasarDatosFrmVerificarDeuda();
    }

    private void dgv_buscar_empresas_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {

    }
  }
}
