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
  public partial class frm_Actas2 : Form
  {
    public frm_Actas2()
    {
      InitializeComponent();
    }

    private void frm_Actas2_Load(object sender, EventArgs e)
    {
      MostarActas();
      CargarCbxInspectores();
    }

    private void CargarCbxInspectores()
    {
      using (var context = new lts_sindicatoDataContext())
      {
        var cbr = context.Usuarios.Select(x => new { nombre = x.Apellido + " " + x.Nombre, x.idUsuario, x.DNI })
            .OrderBy(x => x.nombre).ToList();

        cbx_Inspector.DisplayMember = "nombre";
        cbx_Inspector.ValueMember = "idUsuario";
        cbx_Inspector.DataSource = cbr.ToList();

      }
    }

    private void MostarActas()
    {
      using (var context = new lts_sindicatoDataContext())
      {
        var TodasLasActas = (from a in context.ACTAS

                             join e in context.maeemp on a.CUIT_STR equals e.MEEMP_CUIT_STR
                             into f
                             from empresas in f.DefaultIfEmpty()

                             join usuarios in context.Usuarios on a.InspectorId equals usuarios.idUsuario

                             //where a.InspectorId == 15 

                             select new
                             {
                               FechaDeConfeccion = a.FECHA,
                               NroDeActa = a.ACTA,
                               Empresa = empresas.MAEEMP_RAZSOC.Trim(),
                               Cuit = empresas.MEEMP_CUIT_STR,
                               Desde = a.DESDE,
                               Hasta = a.HASTA,
                               DeudaHistorica = a.DEUDAHISTORICA,
                               DeudaActualizada = a.DEUDATOTAL,
                               ImporteCobrado = a.IMPORTECOBRADO,
                               Diferencia = a.DIFERENCIA,
                               Inspector = usuarios.Apellido

                             }).OrderBy(x => x.FechaDeConfeccion);
        dgv_Actas.DataSource = TodasLasActas.ToList();
        lbl_TotalActas.Text = TodasLasActas.Count().ToString();

      }
    }

    private void btn_GuardarActa_Click(object sender, EventArgs e)
    {
      GuardarActa();
    }

    private void GuardarActa()
    {
      using (var context = new lts_sindicatoDataContext())
      {
        ACTAS insert = new ACTAS();
        insert.ACTA = Convert.ToInt32(txt_NroDeActa.Text);
        insert.FECHA = dtp_FechaDeEmision.Value.Date;
        insert.FECHA_VENC_ACTA = dtp_FechaDeVencimiento.Value;
        insert.DESDE = Convert.ToDateTime("01/" + msk_Desde.Text);
        insert.HASTA = Convert.ToDateTime("01/" + msk_Hasta.Text);
        insert.DEUDAHISTORICA = Convert.ToDouble(txt_Capital.Text);
        insert.INTERESES = Convert.ToDouble(txt_Interes.Text);
        insert.DEUDAACTUALIZADA = insert.DEUDAHISTORICA + insert.INTERESES;
        insert.INTERESFINANC = Convert.ToDouble(txt_InteresDeFinanciacion.Text);
        insert.IMPORTECOBRADO = 0;
        insert.DEUDATOTAL = insert.DEUDAHISTORICA + insert.INTERESES;
        insert.InspectorId = Convert.ToInt32(cbx_Inspector.SelectedValue);
        insert.CUIT_STR = txt_CUIT.Text;
        insert.EMPRESA = txt_RazonSocial.Text;

        context.ACTAS.InsertOnSubmit(insert);
        context.SubmitChanges();
        MostarActas();
      }
    }

    private void btn_BuscarEmpresa_Click(object sender, EventArgs e)
    {
      frm_buscar_empresa f_busc_emp = new frm_buscar_empresa();
      f_busc_emp.DatosPasadosCargarActa += new frm_buscar_empresa.PasarDatosActa(ejecutar);
      f_busc_emp.viene_desde = 3;
      f_busc_emp.ShowDialog();

    }

    public void ejecutar(string empresa, string cuit)
    {
      txt_RazonSocial.Text = empresa;
      txt_CUIT.Text = cuit;
    }

    private void btn_CancelarActa_Click(object sender, EventArgs e)
    {
      LimpiarCamposDeCargarActas();
    }

    private void LimpiarCamposDeCargarActas()
    {
      txt_NroDeActa.Text = "0.00";
      dtp_FechaDeEmision.Value = DateTime.Today;
      dtp_FechaDeVencimiento.Value = DateTime.Today;
      msk_Desde.Text = "__/____"; ;
      msk_Hasta.Text = "__/____";
      txt_Capital.Text = "0.00";
      txt_Interes.Text = "0.00";

      txt_InteresDeFinanciacion.Text = "0.00";
      txt_DeudaActualizada.Text = "0.00";
      txt_TotalDeuda.Text = "0.00";
      txt_InteresDeFinanciacion.Text = "0.00";
      txt_CUIT.Text = "";
      txt_RazonSocial.Text = "";
    }

    private void btn_BuscarActa_Click(object sender, EventArgs e)
    {
      BuscarActa(Convert.ToInt32(txt_ActaBuscar.Text));
    }

    private void BuscarActa(int acta)
    {
      using (var context = new lts_sindicatoDataContext())
      {
        var actaBuscada = from a in context.ACTAS
                          where a.ACTA == acta
                          select new
                          {
                            acta,
                            cuit = a.CUIT_STR,
                            empresa = context.maeemp.Where(x => x.MEEMP_CUIT_STR == a.CUIT_STR).SingleOrDefault().MAEEMP_RAZSOC.Trim()
                          };
        if (actaBuscada.Count() == 0)
        {
          MessageBox.Show("Acta no encontrada, por favor verifique los datos a buscar");
        }
        else
        {
          txt_EmpresaEncontrada.Text = actaBuscada.SingleOrDefault().empresa;
          HabilitarTextBoxCobros();
        }

      }
    }

    private void HabilitarTextBoxCobros()
    {

    }

    private void cbx_FormaDePago_SelectedIndexChanged(object sender, EventArgs e)
    {
      int seleccionado = cbx_FormaDePago.SelectedIndex;  //efectivo

      if (seleccionado == 0) //efectivo
      {
        LimpiarCargarCheques();
      }
      else
      {
        if (seleccionado == 1) //cheque
        {
          HabilitarCargarCheques();
        }
        else //==2 Canje
        {
          LimpiarCargarCheques();
        }
      }
    }

    private void LimpiarCargarCheques()
    {
      txt_NroDeCheque.Enabled = false;
      cbx_Bancos.Enabled = false;
      dtp_VencDeCheque.Enabled = false;
      txt_Emisor.Enabled = false;
      msk_CuitEmisor.Enabled = false;
      txt_NroDeCuenta.Enabled = false;
      btn_CargarCheque.Enabled = false;
    }

    private void HabilitarCargarCheques()
    {
      txt_NroDeCheque.Enabled = true;
      cbx_Bancos.Enabled = true;
      dtp_VencDeCheque.Enabled = true;
      txt_Emisor.Enabled = true;
      msk_CuitEmisor.Enabled = true;
      txt_NroDeCuenta.Enabled = true;
      btn_CargarCheque.Enabled = true;
    }

    private void btn_CargarCheque_Click(object sender, EventArgs e)
    {
      if (ValidarNroDeCheque(txt_NroDeCheque.Text.Trim()) == false)
      {
        CargarChequeEnDgv();
        lbl_CantidadCheques.Text = GetCantidadDeCheuesCArgados().ToString();
      }
      else
      {
        MessageBox.Show("El Cheque Nº " + txt_NroDeCheque.Text.Trim() + " Ya esxiste. Por favor verificar si ingreso correctamente el Nº del cheque");
        txt_NroDeCheque.Focus();
      }

    }

    private int GetCantidadDeCheuesCArgados()
    {
      return dgv_ChequesCargados.Rows.Count;
    }

    private void CargarChequeEnDgv()
    {
      dgv_ChequesCargados.Rows
        .Add(
        txt_NroDeCheque.Text.Trim(),
        txt_Importe.Text.Trim(),
        dtp_VencDeCheque.Value.Date,
        txt_Emisor.Text.Trim(),
        msk_CuitEmisor.Text.Trim(),
        txt_NroDeCuenta.Text.Trim()
        );
    }

    private bool ValidarNroDeComprobante(string comprobante)
    {
      using (var context = new lts_sindicatoDataContext())
      {
        if (comprobante != "")
        {
          var existe = context.COBROS.Where(x => x.RECIBO == Convert.ToDouble(comprobante));
          return existe.Count() > 0 ? true : false;
        }
        //var existe = context.COBROS.Where(x => x.RECIBO == Convert.ToDouble(comprobante));
        else
        {
          return false;
        }
      }
    }

    private void txt_NroDeRecibo_Leave(object sender, EventArgs e)
    {
      if (ValidarNroDeComprobante(txt_NroDeRecibo.Text.Trim()))
      {
        MessageBox.Show("El comprobante Nº " + txt_NroDeRecibo.Text.Trim() + " Ya esxiste. Por favor verificar si ingreso correctamente el Nº de Comprobante");
        txt_NroDeRecibo.Focus();
      }
    }

    private void txt_ActaBuscar_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter)
      {
        BuscarActa(Convert.ToInt32(txt_ActaBuscar.Text));
      }
    }

    private bool ValidarNroDeCheque(string nroDeCheque)
    {
      bool existeNroDeCheque = false;
      foreach (DataGridViewRow fila in dgv_ChequesCargados.Rows)
      {
        if (fila.Cells["NrodeCheque"].Value.ToString() == nroDeCheque)
        {
          existeNroDeCheque = true;
        }
      }
      return existeNroDeCheque;
    }

    private void btn_EliminarCheque_Click(object sender, EventArgs e)
    {
      if (dgv_ChequesCargados.Rows.Count > 0)
      {
        if (MessageBox.Show("Esta Seguro de eliminar de la lista el Cheque", "ATENCION", MessageBoxButtons.YesNo) == DialogResult.Yes)
        {
          dgv_ChequesCargados.Rows.Remove(dgv_ChequesCargados.CurrentRow);
        }
      }
    }
  }
}
