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
using entrega_cupones.Modelos;

namespace entrega_cupones.Formularios
{
  public partial class frm_EntregarMochila : Form
  {
    public int UsuarioId;
    public string UsuarioNombre;
    public class ResumenDeEntrega
    {
      public int MyProperty { get; set; }
    }
    public frm_EntregarMochila()
    {
      InitializeComponent();
    }

    private void btn_BuscarCupon_Click(object sender, EventArgs e)
    {
      BuscarCupon(txt_NroDeCupon.Text == "" ? 0 : Convert.ToInt32(txt_NroDeCupon.Text));
    }

    private void BuscarCupon(int NroDeCupon)
    {
      using (var context = new lts_sindicatoDataContext())
      {
        var entregado = from a in context.eventos_cupones where a.event_cupon_nro == NroDeCupon select new { a.FechaDeEntregaArticulo, a.ArticuloID };
        if (entregado.Count() > 0)
        {

          if (entregado.Single().FechaDeEntregaArticulo != null)
          {
            txt_MochilaEntregada.Text = "ENTREGADO";
            txt_FechaDeEntrega.Text = entregado.Single().FechaDeEntregaArticulo.ToString();
            btn_EntregarMochila.Enabled = false;
          }
          else
          {
            txt_MochilaEntregada.Text = "NO ENTREGADO";
            txt_FechaDeEntrega.Text = "---------";
            btn_EntregarMochila.Enabled = true;
            cbx_Mochilas.SelectedValue = entregado.Single().ArticuloID;
            btn_EntregarMochila.Focus();
          }
        }
        else
        {
          txt_MochilaEntregada.Text = "CUPON NO EXISTE";
          txt_FechaDeEntrega.Text = "---------";
          btn_EntregarMochila.Enabled = false;
        }
      }
    }

    private void txt_NroDeCupon_KeyDown(object sender, KeyEventArgs e)
    {
      if (Keys.Enter == e.KeyCode)
      {
        BuscarCupon(txt_NroDeCupon.Text == "" ? 0 : Convert.ToInt32(txt_NroDeCupon.Text));
      }
    }

    private void btn_EntregarMochila_Click(object sender, EventArgs e)
    {
      EntregarMochila(txt_NroDeCupon.Text == "" ? 0 : Convert.ToInt32(txt_NroDeCupon.Text));
    }

    private void EntregarMochila(int NroCupon)
    {
      using (var datacontext = new lts_sindicatoDataContext())
      {
        var entregado = (from a in datacontext.eventos_cupones where a.event_cupon_nro == NroCupon && a.eventcupon_evento_id == 4 select a).Single();
        var art = (from a in datacontext.articulos where a.ID == entregado.ArticuloID select a).Single();
        entregado.FechaDeEntregaArticulo = DateTime.Now;
        entregado.ArticuloIDRetira = Convert.ToInt32(cbx_Mochilas.SelectedValue);
        entregado.UsuarioIdCheckRetiro = UsuarioId;
        art.Cantidad -= 1;
        datacontext.SubmitChanges();
        MessageBox.Show("La Mochila fue entregada con exito !!!", "Entrega de Mochilas");
        txt_FechaDeEntrega.Text = "";
        txt_NroDeCupon.Text = "";
        txt_MochilaEntregada.Text = "";
        txt_NroDeCupon.Focus();
        btn_EntregarMochila.Enabled = false;
        ListadoMochilasEntregadas();
      }
    }

    private void frm_EntregarMochila_Load(object sender, EventArgs e)
    {
      this.Text = "Entrega de Mochilas - Usuario: " + UsuarioNombre;
      dgv_MochilasEntregadas.AutoGenerateColumns = false;
      
      CargarCbxMochilas();
      CargarLocalidad();
      ListadoMochilasEntregadas();
      //cbx_localidad.SelectedIndex = 0;
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

    private void CargarLocalidad()
    {
      using (var context = new lts_sindicatoDataContext())
      {
        var loc = (from a in context.localidades where a.idprovincias == 14 select a).OrderBy(x => x.nombre);
        cbx_localidad.DisplayMember = "nombre";
        cbx_localidad.ValueMember = "codigopostal";

        cbx_localidad.DataSource = loc.ToList();
      }
    }

    private void ListadoMochilasEntregadas()
    {
      using (var context = new lts_sindicatoDataContext())
      {
        Func_Utiles Func = new Func_Utiles();
        var loc = context.localidades.Where(x => x.idprovincias == 14).ToList();

        var LstMochilasEntregadas = from a in context.eventos_cupones
                                    join mochi in context.articulos on a.ArticuloID equals mochi.ID
                                    where a.ArticuloID == 1

                                    join MF in context.maeflia on a.eventcupon_maesoc_cuil equals Convert.ToDouble(MF.MAEFLIA_NRODOC)
                                    select a;
         //  join SF in context.socflia on MF.MAEFLIA_CODFLIAR equals SF.SOCFLIA_CODFLIAR
         //  join MS in context.maesoc on SF.SOCFLIA_CUIL equals MS.MAESOC_CUIL
         //  where (cbx_localidad.SelectedValue.ToString() == "0" ? MS.MAESOC_CODPOS != cbx_localidad.SelectedValue.ToString() : MS.MAESOC_CODPOS == cbx_localidad.SelectedValue.ToString())
         //  select new
         //  {
         //    FechaDeEntrega = a.FechaDeEntregaArticulo,
         //    NroDeCupon = a.event_cupon_nro,
         //    Mochila = mochi.Sexo == 'F' ? mochi.Descripcion + " - MUJER" : mochi.Descripcion + " - VARON",
         //    mochi.Sexo,
         //    IdMochila = a.ArticuloID,
         //    IdMochilaRetirada = a.ArticuloIDRetira,
         //    EnStock = mochi.Cantidad,
         //    CodigoPostal = MS.MAESOC_CODPOS
         //  };

         var LstMochilasEntregadas1 = from a in context.eventos_cupones
                                    join mochi in context.articulos on a.ArticuloID equals mochi.ID
                                    join MF in context.maeflia on a.eventcupon_maesoc_cuil equals Convert.ToDouble(MF.MAEFLIA_NRODOC)
                                    join SF in context.socflia on MF.MAEFLIA_CODFLIAR equals SF.SOCFLIA_CODFLIAR
                                    join MS in context.maesoc on SF.SOCFLIA_CUIL equals MS.MAESOC_CUIL
                                    //where (cbx_localidad.SelectedValue.ToString() == "0" ? MS.MAESOC_CODPOS != cbx_localidad.SelectedValue.ToString() : MS.MAESOC_CODPOS == cbx_localidad.SelectedValue.ToString())
                                    select new mdlMochilasEntregadas
                                    {
                                      FechaDeEntrega = a.FechaDeEntregaArticulo,
                                      NroDeCupon = a.event_cupon_nro,
                                      Mochila = mochi.Sexo == 'F' ? mochi.Descripcion + " - MUJER" : mochi.Descripcion + " - VARON",
                                      Sexo= mochi.Sexo,
                                      IdMochila = (int) a.ArticuloID,
                                      IdMochilaRetirada = (int)a.ArticuloIDRetira,
                                      EnStock = (int) mochi.Cantidad,
                                      CodigoPostal = MS.MAESOC_CODPOS,
                                      ApenomSocio = MS.MAESOC_APELLIDO.Trim() + " " + MS.MAESOC_NOMBRE.Trim(),
                                      ApenomBenef = MF.MAEFLIA_APELLIDO.Trim() + " " + MF.MAEFLIA_NOMBRE.Trim(),
                                      Entregado = a.FechaDeEntregaArticulo == null ? "NO":"SI"
                                    };

       
        //(cbx_localidad.SelectedValue.ToString() == "0" ? maesocio.MAESOC_CODPOS != cbx_localidad.SelectedValue.ToString() : maesocio.MAESOC_CODPOS == cbx_localidad.SelectedValue.ToString())
        //var LstMochilasEntregadas = from a in LstMochilasEntregadas_
        //                            join b in loc on a.CodigoPostal equals Convert.ToInt16( b.codigopostal)
        //                            select a;

        if (LstMochilasEntregadas1.Count() > 0)
        {
          Func.limpiar_dgv(dgv_Resumen);
          //dgv_MochilasEntregadas.DataSource = LstMochilasEntregadas.Where(x => x.FechaDeEntrega != null).OrderByDescending(x => x.FechaDeEntrega).ToList();
          dgv_MochilasEntregadas.DataSource = LstMochilasEntregadas1.OrderBy(x=>x.ApenomSocio).ToList();
          txt_TotalCuponMochilas.Text = dgv_MochilasEntregadas.Rows.Count.ToString();
          lbl_SinRegistros1.Visible = false;
          lbl_SinRegistros2.Visible = false;

          dgv_Resumen.Rows.Add();
          int fila = dgv_Resumen.Rows.Count - 1;
          dgv_Resumen.Rows[fila].Cells["Mochila"].Value = "JARDIN    - MUJER";
          dgv_Resumen.Rows[fila].Cells["CuponesEmitidos"].Value = LstMochilasEntregadas1.Where(x => x.IdMochila == 1 ).Count().ToString();
          dgv_Resumen.Rows[fila].Cells["MochilasEntregadas"].Value = LstMochilasEntregadas1.Where(x => x.IdMochila == 1 && x.FechaDeEntrega != null).Count().ToString();
          dgv_Resumen.Rows[fila].Cells["FaltanEntregar"].Value = (Convert.ToInt32(dgv_Resumen.Rows[fila].Cells["CuponesEmitidos"].Value) - Convert.ToInt32(dgv_Resumen.Rows[fila].Cells["MochilasEntregadas"].Value)).ToString();
          dgv_Resumen.Rows[fila].Cells["EnStock"].Value = context.articulos.Where(x => x.ID == 1).Single().Cantidad.ToString();
          dgv_Resumen.Rows[fila].Cells["StockRealSinReserva"].Value = (Convert.ToInt32(dgv_Resumen.Rows[fila].Cells["EnStock"].Value) - Convert.ToInt32(dgv_Resumen.Rows[fila].Cells["FaltanEntregar"].Value)).ToString();
          dgv_Resumen.Rows[fila].Cells["PorcentajeDeEntrega"].Value = ((Convert.ToDouble(dgv_Resumen.Rows[fila].Cells["MochilasEntregadas"].Value) * 100) / Convert.ToDouble(context.articulos.Where(x => x.ID == 1).Single().StockInicial)).ToString("N2");

          dgv_Resumen.Rows.Add();
          fila = dgv_Resumen.Rows.Count - 1;
          dgv_Resumen.Rows[fila].Cells["Mochila"].Value = "JARDIN    - VARON";
          dgv_Resumen.Rows[fila].Cells["CuponesEmitidos"].Value = LstMochilasEntregadas1.Where(x => x.IdMochila == 2 ).Count().ToString();
          dgv_Resumen.Rows[fila].Cells["MochilasEntregadas"].Value = LstMochilasEntregadas1.Where(x => x.IdMochila == 2 && x.FechaDeEntrega != null).Count().ToString();
          dgv_Resumen.Rows[fila].Cells["FaltanEntregar"].Value = (Convert.ToInt32(dgv_Resumen.Rows[fila].Cells["CuponesEmitidos"].Value) - Convert.ToInt32(dgv_Resumen.Rows[fila].Cells["MochilasEntregadas"].Value)).ToString();
          dgv_Resumen.Rows[fila].Cells["EnStock"].Value = context.articulos.Where(x => x.ID == 2).Single().Cantidad.ToString();
          dgv_Resumen.Rows[fila].Cells["StockRealSinReserva"].Value = (Convert.ToInt32(dgv_Resumen.Rows[fila].Cells["EnStock"].Value) - Convert.ToInt32(dgv_Resumen.Rows[fila].Cells["FaltanEntregar"].Value)).ToString();
          dgv_Resumen.Rows[fila].Cells["PorcentajeDeEntrega"].Value = ((Convert.ToDouble(dgv_Resumen.Rows[fila].Cells["MochilasEntregadas"].Value) * 100) / Convert.ToDouble(context.articulos.Where(x => x.ID == 2).Single().StockInicial)).ToString("N2");

          dgv_Resumen.Rows.Add();
          fila = dgv_Resumen.Rows.Count - 1;
          dgv_Resumen.Rows[fila].Cells["CuponesEmitidos"].Value = LstMochilasEntregadas1.Where(x => x.IdMochila == 3).Count().ToString();
          dgv_Resumen.Rows[fila].Cells["Mochila"].Value = "PRIMARIA 1 - MUJER";
          dgv_Resumen.Rows[fila].Cells["MochilasEntregadas"].Value = LstMochilasEntregadas1.Where(x => x.IdMochila == 3 && x.FechaDeEntrega != null).Count().ToString();
          dgv_Resumen.Rows[fila].Cells["FaltanEntregar"].Value = (Convert.ToInt32(dgv_Resumen.Rows[fila].Cells["CuponesEmitidos"].Value) - Convert.ToInt32(dgv_Resumen.Rows[fila].Cells["MochilasEntregadas"].Value)).ToString();
          dgv_Resumen.Rows[fila].Cells["EnStock"].Value = context.articulos.Where(x => x.ID == 3).Single().Cantidad.ToString();
          dgv_Resumen.Rows[fila].Cells["StockRealSinReserva"].Value = (Convert.ToInt32(dgv_Resumen.Rows[fila].Cells["EnStock"].Value) - Convert.ToInt32(dgv_Resumen.Rows[fila].Cells["FaltanEntregar"].Value)).ToString();
          dgv_Resumen.Rows[fila].Cells["PorcentajeDeEntrega"].Value = ((Convert.ToDouble(dgv_Resumen.Rows[fila].Cells["MochilasEntregadas"].Value) * 100) / Convert.ToDouble(context.articulos.Where(x => x.ID == 3).Single().StockInicial)).ToString("N2");

          dgv_Resumen.Rows.Add();
          fila = dgv_Resumen.Rows.Count - 1;
          dgv_Resumen.Rows[fila].Cells["CuponesEmitidos"].Value = LstMochilasEntregadas1.Where(x => x.IdMochila == 4).Count().ToString();
          dgv_Resumen.Rows[fila].Cells["Mochila"].Value = "PRIMARIA 1 - VARON";
          dgv_Resumen.Rows[fila].Cells["MochilasEntregadas"].Value = LstMochilasEntregadas1.Where(x => x.IdMochila == 4 && x.FechaDeEntrega != null).Count().ToString();
          dgv_Resumen.Rows[fila].Cells["FaltanEntregar"].Value = (Convert.ToInt32(dgv_Resumen.Rows[fila].Cells["CuponesEmitidos"].Value) - Convert.ToInt32(dgv_Resumen.Rows[fila].Cells["MochilasEntregadas"].Value)).ToString();
          dgv_Resumen.Rows[fila].Cells["EnStock"].Value = context.articulos.Where(x => x.ID == 4).Single().Cantidad.ToString();
          dgv_Resumen.Rows[fila].Cells["StockRealSinReserva"].Value = (Convert.ToInt32(dgv_Resumen.Rows[fila].Cells["EnStock"].Value) - Convert.ToInt32(dgv_Resumen.Rows[fila].Cells["FaltanEntregar"].Value)).ToString();
          dgv_Resumen.Rows[fila].Cells["PorcentajeDeEntrega"].Value = ((Convert.ToDouble(dgv_Resumen.Rows[fila].Cells["MochilasEntregadas"].Value) * 100) / Convert.ToDouble(context.articulos.Where(x => x.ID == 4).Single().StockInicial)).ToString("N2");

          dgv_Resumen.Rows.Add();
          fila = dgv_Resumen.Rows.Count - 1;
          dgv_Resumen.Rows[fila].Cells["CuponesEmitidos"].Value = LstMochilasEntregadas1.Where(x => x.IdMochila == 5).Count().ToString();
          dgv_Resumen.Rows[fila].Cells["Mochila"].Value = "PRIMARIA 2 - MUJER";
          dgv_Resumen.Rows[fila].Cells["MochilasEntregadas"].Value = LstMochilasEntregadas1.Where(x => x.IdMochila == 5 && x.FechaDeEntrega != null).Count().ToString();
          dgv_Resumen.Rows[fila].Cells["FaltanEntregar"].Value = (Convert.ToInt32(dgv_Resumen.Rows[fila].Cells["CuponesEmitidos"].Value) - Convert.ToInt32(dgv_Resumen.Rows[fila].Cells["MochilasEntregadas"].Value)).ToString();
          dgv_Resumen.Rows[fila].Cells["EnStock"].Value = context.articulos.Where(x => x.ID == 5).Single().Cantidad.ToString();
          dgv_Resumen.Rows[fila].Cells["StockRealSinReserva"].Value = (Convert.ToInt32(dgv_Resumen.Rows[fila].Cells["EnStock"].Value) - Convert.ToInt32(dgv_Resumen.Rows[fila].Cells["FaltanEntregar"].Value)).ToString();
          dgv_Resumen.Rows[fila].Cells["PorcentajeDeEntrega"].Value = ((Convert.ToDouble(dgv_Resumen.Rows[fila].Cells["MochilasEntregadas"].Value) * 100) / Convert.ToDouble(context.articulos.Where(x => x.ID == 5).Single().StockInicial)).ToString("N2");

          dgv_Resumen.Rows.Add();
          fila = dgv_Resumen.Rows.Count - 1;
          dgv_Resumen.Rows[fila].Cells["CuponesEmitidos"].Value = LstMochilasEntregadas1.Where(x => x.IdMochila == 6).Count().ToString();
          dgv_Resumen.Rows[fila].Cells["Mochila"].Value = "PRIMARIA 2 - VARON";
          dgv_Resumen.Rows[fila].Cells["MochilasEntregadas"].Value = LstMochilasEntregadas1.Where(x => x.IdMochila == 6 && x.FechaDeEntrega != null).Count().ToString();
          dgv_Resumen.Rows[fila].Cells["FaltanEntregar"].Value = (Convert.ToInt32(dgv_Resumen.Rows[fila].Cells["CuponesEmitidos"].Value) - Convert.ToInt32(dgv_Resumen.Rows[fila].Cells["MochilasEntregadas"].Value)).ToString();
          dgv_Resumen.Rows[fila].Cells["EnStock"].Value = context.articulos.Where(x => x.ID == 6).Single().Cantidad.ToString();
          dgv_Resumen.Rows[fila].Cells["StockRealSinReserva"].Value = (Convert.ToInt32(dgv_Resumen.Rows[fila].Cells["EnStock"].Value) - Convert.ToInt32(dgv_Resumen.Rows[fila].Cells["FaltanEntregar"].Value)).ToString();
          dgv_Resumen.Rows[fila].Cells["PorcentajeDeEntrega"].Value = ((Convert.ToDouble(dgv_Resumen.Rows[fila].Cells["MochilasEntregadas"].Value) * 100) / Convert.ToDouble(context.articulos.Where(x => x.ID == 6).Single().StockInicial)).ToString("N2");

          dgv_Resumen.Rows.Add();
          fila = dgv_Resumen.Rows.Count - 1;
          dgv_Resumen.Rows[fila].Cells["CuponesEmitidos"].Value = LstMochilasEntregadas1.Where(x => x.IdMochila == 7).Count().ToString();
          dgv_Resumen.Rows[fila].Cells["Mochila"].Value = "SECUNDARIA - MUJER";
          dgv_Resumen.Rows[fila].Cells["MochilasEntregadas"].Value = LstMochilasEntregadas1.Where(x => x.IdMochila == 7 && x.FechaDeEntrega != null).Count().ToString();
          dgv_Resumen.Rows[fila].Cells["FaltanEntregar"].Value = (Convert.ToInt32(dgv_Resumen.Rows[fila].Cells["CuponesEmitidos"].Value) - Convert.ToInt32(dgv_Resumen.Rows[fila].Cells["MochilasEntregadas"].Value)).ToString();
          dgv_Resumen.Rows[fila].Cells["EnStock"].Value = context.articulos.Where(x => x.ID == 7).Single().Cantidad.ToString();
          dgv_Resumen.Rows[fila].Cells["StockRealSinReserva"].Value = (Convert.ToInt32(dgv_Resumen.Rows[fila].Cells["EnStock"].Value) - Convert.ToInt32(dgv_Resumen.Rows[fila].Cells["FaltanEntregar"].Value)).ToString();
          dgv_Resumen.Rows[fila].Cells["PorcentajeDeEntrega"].Value = ((Convert.ToDouble(dgv_Resumen.Rows[fila].Cells["MochilasEntregadas"].Value) * 100) / Convert.ToDouble(context.articulos.Where(x => x.ID == 7).Single().StockInicial)).ToString("N2");

          dgv_Resumen.Rows.Add();
          fila = dgv_Resumen.Rows.Count - 1;
          dgv_Resumen.Rows[fila].Cells["CuponesEmitidos"].Value = LstMochilasEntregadas1.Where(x => x.IdMochila == 8).Count().ToString();
          dgv_Resumen.Rows[fila].Cells["Mochila"].Value = "SECUNDARIA - VARON";
          dgv_Resumen.Rows[fila].Cells["MochilasEntregadas"].Value = LstMochilasEntregadas1.Where(x => x.IdMochila == 8 && x.FechaDeEntrega != null).Count().ToString();
          dgv_Resumen.Rows[fila].Cells["FaltanEntregar"].Value = (Convert.ToInt32(dgv_Resumen.Rows[fila].Cells["CuponesEmitidos"].Value) - Convert.ToInt32(dgv_Resumen.Rows[fila].Cells["MochilasEntregadas"].Value)).ToString();
          dgv_Resumen.Rows[fila].Cells["EnStock"].Value = context.articulos.Where(x => x.ID == 8).Single().Cantidad.ToString();
          dgv_Resumen.Rows[fila].Cells["StockRealSinReserva"].Value = (Convert.ToInt32(dgv_Resumen.Rows[fila].Cells["EnStock"].Value) - Convert.ToInt32(dgv_Resumen.Rows[fila].Cells["FaltanEntregar"].Value)).ToString();
          dgv_Resumen.Rows[fila].Cells["PorcentajeDeEntrega"].Value = ((Convert.ToDouble(dgv_Resumen.Rows[fila].Cells["MochilasEntregadas"].Value) * 100) / Convert.ToDouble(context.articulos.Where(x => x.ID == 8).Single().StockInicial)).ToString("N2");
        }
        else
        {
          dgv_MochilasEntregadas.DataSource = null;
          txt_TotalCuponMochilas.Text = "0";
          Func.limpiar_dgv(dgv_Resumen);
          lbl_SinRegistros1.Visible = true;
          lbl_SinRegistros2.Visible = true;
        }
      }
    }

    private void btn_Refresh_Click(object sender, EventArgs e)
    {
      ListadoMochilasEntregadas();
    }

    private void btn_ActualizarLocalidad_Click(object sender, EventArgs e)
    {
      ListadoMochilasEntregadas();
    }

    private void cbx_localidad_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
  }
}
