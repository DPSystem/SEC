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

namespace entrega_cupones.Formularios
{
  public partial class frm_CobranzasAsignadas : Form
  {
    public int CobradorId;
    public string CobradorNombre;
    public frm_CobranzasAsignadas()
    {
      InitializeComponent();
    }

    private void frm_CobranzasAsignadas_Load(object sender, EventArgs e)
    {
      lbl_Cobrador.Text = CobradorNombre;
      CargarActasAsignadas(CobradorId);
      if (dgv_CbrAsignadas.Rows.Count == 0 )
      {
        btn_VerAsignadas.Enabled = false;
      }
      else
      {
        btn_VerAsignadas.Enabled = true;
      }
      MostrarMensajesAlCobrador();

    }
   
    private void CargarActasAsignadas(int CobradorId)
    {
      using (var context = new lts_sindicatoDataContext())
      {
        var CobranzaAsignada2 = context.Asignaciones
                                .Where(x => x.CobradorId == CobradorId)
                                .Select(x => new
                                {
                                  x.Fecha,
                                  numero = x.Numero,
                                  total = context.AsignarCobranza.Where(y => y.CobradorID == CobradorId && y.NroAsignacion == x.Numero).Count(),
                                  cobradas = context.AsignarCobranza.Where(y => y.CobradorID == CobradorId && y.NroAsignacion == x.Numero && y.Estado == 1).Count(),
                                  noCobradas = context.AsignarCobranza.Where(y => y.CobradorID == CobradorId && y.NroAsignacion == x.Numero && y.Estado == 0).Count()
                                });

        if (CobranzaAsignada2.Count() > 0)
        {
          dgv_CbrAsignadas.DataSource = CobranzaAsignada2.ToList();
        }
      }
    }

    private void btn_VerAsignadas_Click(object sender, EventArgs e)
    {
      frm_VerActasAsignadasParaCobrar f_VerActasAsignadasParaCobrar = new frm_VerActasAsignadasParaCobrar();
      f_VerActasAsignadasParaCobrar.CobradorId = CobradorId;
      f_VerActasAsignadasParaCobrar.CobradorNombre = CobradorNombre;
      f_VerActasAsignadasParaCobrar.Text = "Gestion de Cobranzas - Usuario: " + CobradorNombre;
      f_VerActasAsignadasParaCobrar.NroDeAsignacion = Convert.ToInt32(dgv_CbrAsignadas.CurrentRow.Cells["numero"].Value.ToString());
      f_VerActasAsignadasParaCobrar.ShowDialog();
    }

    private void MostrarMensajesAlCobrador()
    {
      Buscadores busc = new Buscadores();
      Cobrador cbr = new Cobrador();
      using (var context = new lts_sindicatoDataContext())
      {
        var novedades = (from a in context.Novedades where a.MensajeAlCobrador == 1 && a.EstadoMensajeAlCobrador == 0
                         join b in context.AsignarCobranza on a.CUIT equals b.CUIT
                         where b.CobradorID == CobradorId
                         select new
                         {
                           a.Fecha,
                           a.Novedad,
                           Empresa = busc.GetEmpresaPorCUIT(b.CUIT),
                           NroDeAsig = a.NumeroDeAsignacion,
                           Emisor = cbr.GetNombreDeCobradorPorID(Convert.ToInt32( a.Usuario))
                         }).OrderBy(x => x.Fecha);

        dgv_Novedades.DataSource = novedades.ToList();
        this.dgv_Novedades.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
      }
    }
  }
}

