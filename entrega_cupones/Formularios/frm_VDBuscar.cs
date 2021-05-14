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
  public partial class frm_VDBuscar : Form
  {
    public List<VD_Inspector> _VD_Inspector = new List<VD_Inspector>();
    public frm_VDBuscar()
    {
      InitializeComponent();
    }

    private void frm_VDBuscar_Load(object sender, EventArgs e)
    {
      dgv_VD.AutoGenerateColumns = false;
      Get_VDListado();
    }

    public void Get_VDListado()
    {
      dgv_VD.DataSource = _VD_Inspector = mtdVDInspector.Get_VDListado2();
    }

    private void btn_VerVD_Click(object sender, EventArgs e)
    {
      VD_Ver();
    }

    public void VD_Ver()
    {
      var VD = _VD_Inspector.Where(x => x.Id == (int)dgv_VD.CurrentRow.Cells["Id"].Value).Single();

      frm_VerVD f_VerVD = new frm_VerVD();
      f_VerVD._VDId = VD.Id;
      f_VerVD.txt_CUIT.Text = VD.CUIT;
      f_VerVD.txt_BuscarEmpesa.Text = "";
      f_VerVD.msk_Desde.Text = VD.Desde.ToString();
      f_VerVD.msk_Hasta.Text = VD.Hasta.ToString();
      f_VerVD.msk_Vencimiento.Text = VD.FechaVenc.ToString();
      f_VerVD.cbx_TipoDeInteres.SelectedIndex = (int)VD.TipoInteres; 
      f_VerVD.txt_Interes.Text = VD.InteresMensual.ToString(); 
      f_VerVD.txt_InteresDiario.Text = VD.InteresDiario.ToString();
      f_VerVD.Show();

    }
  }
}
