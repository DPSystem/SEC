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
  public partial class frm_EmpleadoEstadoDDJJ : Form
  {
    public List<mdlDDJJEmpleado> _DetallePeriodo = new List<mdlDDJJEmpleado>();
    public DateTime Periodo;
    public int Rectificacion;
    public frm_EmpleadoEstadoDDJJ()
    {
      InitializeComponent();
    }

    private void frm_EmpleadoEstadoDDJJ_Load(object sender, EventArgs e)
    {
      dgv_DetallePeriodo.AutoGenerateColumns = false;
      _DetallePeriodo.AddRange(mtdEmpleados.ListadoEmpleadoAporte(txt_CUIT.Text, Periodo, Rectificacion));
      dgv_DetallePeriodo.DataSource = _DetallePeriodo;
    }

    private void dgv_DetallePeriodo_SelectionChanged(object sender, EventArgs e)
    {
      int index = dgv_DetallePeriodo.CurrentRow.Index;
      //Haberes
      txt_SueldoBasico.Text = _DetallePeriodo[index].Escala.ToString("N2");
      txt_Antiguedad.Text = _DetallePeriodo[index].AntiguedadImporte.ToString("N2");
      txt_Presentismo.Text = _DetallePeriodo[index].Presentismo.ToString("N2");
      //Descuentos
      txt_Jubilacion.Text = _DetallePeriodo[index].Jubilacion.ToString("N2");
      txt_Ley19302.Text = _DetallePeriodo[index].Ley19302.ToString("N2");
      txt_ObraSocial.Text = _DetallePeriodo[index].ObraSocial.ToString("N2");
      txt_AporteLey.Text = _DetallePeriodo[index].AporteLeyDif.ToString("N2");
      txt_AporteSocio.Text = _DetallePeriodo[index].AporteSocioEscala.ToString("N2");
      txt_FAECyS.Text = _DetallePeriodo[index].FAECys.ToString("N2");
      txt_Osecac.Text = _DetallePeriodo[index].OSECAC.ToString("N2");
      //Totales
      txt_TotalHaberes.Text = _DetallePeriodo[index].TotalHaberes.ToString("N2");
      txt_TotalDescuentos.Text = _DetallePeriodo[index].TotalDescuentos.ToString("N2");
      txt_TotalNeto.Text = (_DetallePeriodo[index].TotalHaberes - _DetallePeriodo[index].TotalDescuentos).ToString("N2");
      txt_SueldoDeclarado.Text = _DetallePeriodo[index].Sueldo.ToString("N2");
      txt_Diferencia.Text = ((_DetallePeriodo[index].TotalHaberes - _DetallePeriodo[index].TotalDescuentos) - _DetallePeriodo[index].Sueldo).ToString("N2");

      // Resumen
      txt_CantidadEmpleados.Text = _DetallePeriodo.Count.ToString();
      txt_CantidadJorandaCompleta.Text = _DetallePeriodo.Count(x => x.Jornada == "Completa").ToString();
      txt_CantidadJornadaParcial.Text = _DetallePeriodo.Count(x => x.Jornada == "Parcial").ToString();
      txt_DifAporteSocio.Text = _DetallePeriodo.Where(x=>x.Jornada == "Parcial") .Sum(x => x.AporteSocioDif).ToString("N2");
      txt_DifSueldo.Text = _DetallePeriodo.Sum(x => x.SueldoDif).ToString("N2");
    }

    private void dgv_DetallePeriodo_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {

    }
  }
}
