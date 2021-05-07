using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using entrega_cupones.Metodos;
using entrega_cupones.Modelos;

namespace entrega_cupones.Formularios
{
  public partial class frm_IngresoManualDDJJ : Form
  {
    public DateTime _FechaDeVencimiento;
    public int _TipoDeInteres;
    public decimal _TazaInteres;
    public decimal _VarSueldo;
    public decimal _VarSueldoJC;
    public decimal _VarSueldoJP;

    public frm_IngresoManualDDJJ()
    {
      InitializeComponent();
    }

    private void frm_IngresoManualDDJJ_Load(object sender, EventArgs e)
    {
      DateTime periodo = Convert.ToDateTime(txt_Periodo.Text);

      using (var context = new lts_sindicatoDataContext())
      {

        cbx_CategoriaCompleta.DisplayMember = "Descripcion";
        cbx_CategoriaCompleta.ValueMember = "CodigoCategoria";
        cbx_CategoriaCompleta.DataSource = mtdCategorias.Get_CategoriaConSueldo(periodo, 1);

        cbx_CategoriaParcial.DisplayMember = "Descripcion";
        cbx_CategoriaParcial.ValueMember = "CodigoCategoria";
        cbx_CategoriaParcial.DataSource = mtdCategorias.Get_CategoriaConSueldo(periodo, 2);

      }
    }

    private void btn_Aceptar_Click(object sender, EventArgs e)
    {

      if (MessageBox.Show("Ha verificado correctamente los datos ingresados?  De no hacerlo presione CANCELAR y verifique nuevamente.", "ATENCION", MessageBoxButtons.OKCancel) == DialogResult.OK)
      {

        GuardarCambios();

        Close();
      }
    }

    private void GuardarCambios()
    {
      VerificarDeuda formverificardeuda = Owner as VerificarDeuda;
      formverificardeuda.dgv_ddjj.CurrentRow.Cells["Periodo"].Value = txt_Periodo.Text;
      formverificardeuda.dgv_ddjj.CurrentRow.Cells["Rectificacion"].Value = "0";
      formverificardeuda.dgv_ddjj.CurrentRow.Cells["AporteLey"].Value = Convert.ToDecimal(txt_TotalAporteLey.Text);
      formverificardeuda.dgv_ddjj.CurrentRow.Cells["AporteSocio"].Value = Convert.ToDecimal(txt_TotalAporteSocio.Text);
      formverificardeuda.dgv_ddjj.CurrentRow.Cells["ImporteDepositado"].Value = "0";
      formverificardeuda.dgv_ddjj.CurrentRow.Cells["DiasDeMora"].Value = Convert.ToDecimal(txt_DiasDeMora.Text);
      formverificardeuda.dgv_ddjj.CurrentRow.Cells["Empleados"].Value = Convert.ToDecimal(txt_CantidadEmpleados.Text);
      formverificardeuda.dgv_ddjj.CurrentRow.Cells["Socios"].Value = Convert.ToDecimal(txt_CantidadSocios.Text);
      formverificardeuda.dgv_ddjj.CurrentRow.Cells["Capital"].Value = Convert.ToDecimal(txt_TotalAporte.Text);
      formverificardeuda.dgv_ddjj.CurrentRow.Cells["Interes"].Value = Convert.ToDecimal(txt_Intereses.Text);
      formverificardeuda.dgv_ddjj.CurrentRow.Cells["Total"].Value = Convert.ToDecimal(txt_Total.Text);
      formverificardeuda.dgv_ddjj.CurrentRow.Cells["PerNoDec"].Value = 0;
    }

    private void btn_Limpiar_Click(object sender, EventArgs e)
    {
      foreach (TextBox txt in this.Controls.OfType<TextBox>().ToList())
      {
        if (txt.Name != "txt_Periodo")
        {
          txt.Text = "0";
        }
      }
    }

    private void btn_Cancelar_Click(object sender, EventArgs e)
    {
      Close();
    }

    private void txt_CantidadSocios_TextChanged(object sender, EventArgs e)
    {

    }

    private void btn_CalcularDeuda_Click(object sender, EventArgs e)
    {
      using (var context = new lts_sindicatoDataContext())
      {
        decimal Porcentaje = Convert.ToDecimal(0.02);
        DateTime Periodo = Convert.ToDateTime(txt_Periodo.Text);

        decimal Sueldo = Convert.ToDecimal((from a in context.EscalaSalarial
                                            where a.Periodo == Periodo && a.CodCategoria == 1
                                            select new { Sueldo = a.Importe }).Single().Sueldo);

        decimal AporteLey = (Sueldo * Convert.ToInt16(txt_CantidadEmpleados.Text)) * Porcentaje;
        decimal AporteSocio = (Sueldo * Convert.ToDecimal(txt_CantidadSocios.Text)) * Porcentaje;
        decimal Deuda = AporteLey + AporteSocio;
        decimal Intereses = mtdIntereses.CalcularInteres(null, Periodo, Deuda, _FechaDeVencimiento, _TipoDeInteres, _TazaInteres);
        //txt_Deuda.Text = Deuda.ToString();
        txt_Intereses.Text = Intereses.ToString();
        txt_Total.Text = (Deuda + Intereses).ToString();
      }
    }

    private void Get_Sueldo()
    {
      txt_CantidadEmpleados.Text = (Convert.ToInt32(txt_EmpleadosJC.Text) + Convert.ToInt32(txt_EmpleadosJP.Text)).ToString();
      txt_CantidadSocios.Text = (Convert.ToInt32(txt_SociosJC.Text) + Convert.ToInt32(txt_SociosJP.Text)).ToString();

      txt_SueldoJC.Text = _VarSueldoJC.ToString("N2");//(Convert.ToInt32(txt_EmpleadosJC.Text) * _VarSueldoJC).ToString("N2");
      txt_AporteLeyJC.Text = (_VarSueldoJC * Convert.ToInt16(txt_EmpleadosJC.Text) * Convert.ToDecimal(0.02)).ToString("N2");
      txt_AporteSocioJC.Text = (_VarSueldoJC * Convert.ToInt16(txt_SociosJC.Text) * Convert.ToDecimal(0.02)).ToString("N2");

      txt_SueldoJP.Text = _VarSueldoJP.ToString("N2");//(Convert.ToInt32(txt_EmpleadosJC.Text) * _VarSueldoJC).ToString("N2");
      txt_AporteLeyJP.Text = (_VarSueldoJP * Convert.ToInt16(txt_EmpleadosJP.Text) * Convert.ToDecimal(0.02)).ToString("N2");
      txt_AporteSocioJP.Text = (_VarSueldoJP * Convert.ToInt16(txt_SociosJP.Text) * Convert.ToDecimal(0.02)).ToString("N2");

      txt_TotalAporteLey.Text = (Convert.ToDecimal(txt_AporteLeyJC.Text) + Convert.ToDecimal(txt_AporteLeyJP.Text)).ToString("N2");
      txt_TotalAporteSocio.Text = (Convert.ToDecimal(txt_AporteSocioJC.Text) + Convert.ToDecimal(txt_AporteSocioJP.Text)).ToString("N2");

      txt_TotalAporte.Text = (Convert.ToDecimal(txt_TotalAporteLey.Text) + Convert.ToDecimal(txt_TotalAporteSocio.Text)).ToString("N2");

      DateTime Periodo = Convert.ToDateTime(txt_Periodo.Text);
      txt_DiasDeMora.Text = mtdFuncUtiles.CalcularDias(Periodo.AddMonths(1).AddDays(14), _FechaDeVencimiento).ToString();

      decimal Intereses = mtdIntereses.CalcularInteres(null, Periodo.AddMonths(1).AddDays(14), Convert.ToDecimal(txt_TotalAporte.Text), _FechaDeVencimiento, _TipoDeInteres, _TazaInteres);

      txt_Intereses.Text = Intereses.ToString("N2");
      txt_Total.Text = (Intereses + Convert.ToDecimal(txt_TotalAporte.Text)).ToString("N2");
    }

    private void cbx_CategoriaCompleta_SelectedIndexChanged(object sender, EventArgs e)
    {
      _VarSueldoJC = mtdCategorias.GetSueldoDeCategoria((int)cbx_CategoriaCompleta.SelectedValue, Convert.ToDateTime(txt_Periodo.Text), 1);
      Get_Sueldo();
    }

    private void cbx_CategoriaParcial_SelectedIndexChanged(object sender, EventArgs e)
    {
      _VarSueldoJP = mtdCategorias.GetSueldoDeCategoria((int)cbx_CategoriaParcial.SelectedValue, Convert.ToDateTime(txt_Periodo.Text), 2);
      Get_Sueldo();
    }

    private void btn_Refresh_Click(object sender, EventArgs e)
    {
      Get_Sueldo();
    }
  }
}
