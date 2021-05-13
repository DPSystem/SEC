using entrega_cupones.Metodos;
using entrega_cupones.Modelos;
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
  public partial class frm_VerVD : Form
  {
    public List<VD_Detalle> _VDDetalle = new List<VD_Detalle>();
    public List<EstadoDDJJ> _ddjj = new List<EstadoDDJJ>();
    public int _VDId;
    public frm_VerVD()
    {
      InitializeComponent();
    }

    private void frm_VerVD_Load(object sender, EventArgs e)
    {
      dgv_VD.AutoGenerateColumns = false;
      VD_Mostrar();
      
    }

    private void VD_Mostrar()
    {
      _VDDetalle = mtdVDDetalle.Get_VDD(_VDId);
      dgv_VD.DataSource = _VDDetalle;
      BindingSource bindingSource = new BindingSource();
      bindingSource.DataSource = _VDDetalle;
      //dataGridView1.DataSource = bindingSource;
      CalcularTotales();
    }

    private void CalcularTotales()
    {
      txt_Total.Text = Math.Round(_VDDetalle.Sum(x => x.Total), 2).ToString("N2");
      txt_Pagado.Text = Math.Round(_VDDetalle.Sum(x => x.ImporteDepositado), 2).ToString("N2");
      txt_Deuda.Text = Math.Round(_VDDetalle.Sum(x => x.DeudaGenerada), 2).ToString("N2");
      txt_TotalInteres.Text = Math.Round(_VDDetalle.Sum(x => x.InteresGenerado), 2).ToString("N2");
      txt_PerNoDec.Text = _VDDetalle.Count(x => x.PerNoDec == 1).ToString();
      txt_DeudaInicial.Text = txt_Total.Text;
      txt_Anticipo.Text = "";
      txt_DeudaPlan.Text = txt_Total.Text;
    }

    private void btn_CalcularDeuda_Click(object sender, EventArgs e)
    {
      CalcularDeuda();
    }

    private void CalcularDeuda()
    {
      _VDDetalle = mtdVDDetalle.VD_ListadoDDJJT(_VDDetalle,
                    txt_CUIT.Text,
                    Convert.ToDateTime("01/" + msk_Desde.Text),
                    Convert.ToDateTime("01/" + msk_Hasta.Text),
                    Convert.ToDateTime(msk_Vencimiento.Text),
                    cbx_TipoDeInteres.SelectedIndex,
                    Convert.ToDecimal(txt_InteresDiario.Text),
                    _VDId
                    );
      VD_Mostrar();
    }

    private void btn_Actualizar_VD_Click(object sender, EventArgs e)
    {
      using (var context = new lts_sindicatoDataContext())
      {
        foreach (var periodo in _VDDetalle)
        {

          var VDDetalle = context.VD_Detalle.Where(x => x.Id == periodo.Id).SingleOrDefault();
         
          VDDetalle.Periodo = periodo.Periodo;
          VDDetalle.Rectificacion = periodo.Rectificacion;
          VDDetalle.CantidadEmpleados = periodo.CantidadEmpleados;
          VDDetalle.CantidadSocios = periodo.CantidadSocios;
          VDDetalle.TotalSueldoEmpleados = periodo.TotalSueldoEmpleados;
          VDDetalle.TotalSueldoSocios = periodo.TotalSueldoSocios;
          VDDetalle.TotalAporteEmpleados = periodo.TotalAporteEmpleados;
          VDDetalle.TotalAporteSocios = periodo.TotalAporteSocios;
          VDDetalle.FechaDePago = Convert.ToDateTime(periodo.FechaDePago);
          VDDetalle.ImporteDepositado = periodo.ImporteDepositado;
          VDDetalle.DiasDeMora = periodo.DiasDeMora;
          VDDetalle.DeudaGenerada = periodo.DeudaGenerada;
          VDDetalle.InteresGenerado = periodo.InteresGenerado;
          VDDetalle.Total = periodo.Total;
          VDDetalle.PerNoDec = periodo.PerNoDec;
          VDDetalle.ActaId = 0;
          VDDetalle.NumeroDeActa = 0;
          VDDetalle.Estado = 0;
          context.SubmitChanges();
        };
      }
    }

    private void txt_Interes_TextChanged(object sender, EventArgs e)
    {
      if (txt_Interes.Text == "")
      {
        txt_Interes.Text = "0";
      }
      txt_InteresDiario.Text = mtdIntereses.CalcularInteresDiario(txt_Interes.Text);
    }
  }
}

