using entrega_cupones.Clases;
using entrega_cupones.Metodos;
using entrega_cupones.Modelos;
using Org.BouncyCastle.Crypto.Tls;
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
  public partial class frm_VerRecibos : Form
  {
    public int _NroDeActa;
    public int _NroDeRecibo;
    List<mdlVerRecibos> _VerRecibos = new List<mdlVerRecibos>();

    public frm_VerRecibos()
    {
      InitializeComponent();
    }

    private void frm_VerRecibos_Load(object sender, EventArgs e)
    {
      dgv_VerRecibos.AutoGenerateColumns = false;
      VerRecibos();
    }

    private void VerRecibos()
    {
      using (var context = new lts_sindicatoDataContext())
      {
        var ff = from a in context.Recibos
                 where a.NroDeActa == _NroDeActa
                 join b in context.RecibosDetalle on a.NroAutomatico equals b.NroAut
                 select new mdlVerRecibos
                 {
                   Cuota = b.NroCuota.ToString(),
                   Importe = Convert.ToDecimal(b.Importe),
                   NroReciboAutomatico = a.NroAutomatico.ToString(),
                   FechaAutomatica = Convert.ToDateTime(a.FechaAutomatica),
                   NroReciboManual = a.NroManual == null ? "" : a.NroManual,
                   FechaManual = a.FechaManual == null ? "" : a.FechaManual,
                   Aprobado = a.Aprobado == true ? "SI" : "NO",
                   AprobadoUsuarioNombre = (from c in context.Usuarios where c.idUsuario == a.AprobadoUsuarioId select new { apenom = c.Apellido + " " + c.Nombre }).Single().apenom,
                   AprobadoUsuarioId = (int) (a.AprobadoUsuarioId == null? 0 : a.AprobadoUsuarioId),
                   AprobadoFecha = Convert.ToDateTime(a.AprobadoFecha)
                 };
        _VerRecibos.AddRange(ff);

        foreach (var item in _VerRecibos)
        {
          var efec = context.Efectivo.Where(x => x.NroDeRecibo == Convert.ToInt32(item.NroReciboAutomatico));
          if (efec.Count() > 0)
          {
            item.Efectivo = Convert.ToDecimal(efec.SingleOrDefault().Importe);
          }
        }
        dgv_VerRecibos.DataSource = _VerRecibos;
      }
    }

    private void dgv_VerRecibos_SelectionChanged(object sender, EventArgs e)
    {
      MostrarCheques();
      MostrarRecibosManuales();
      MostrarTransf();
      txt_Efectivo.Text = dgv_VerRecibos.CurrentRow.Cells["Efectivo"].Value.ToString();
    }

    private void MostrarCheques()
    {
      using (var context = new lts_sindicatoDataContext())
      {
        int NroDeRecibo = Convert.ToInt32(dgv_VerRecibos.CurrentRow.Cells["Recibo"].Value);
        var cheques = from a in context.cheques
                      where (a.NroDeRecibo == NroDeRecibo)
                      select new mdlCargaDeCheques
                      {
                        FechaEmision = a.FechaDeEmision,
                        Importe = (decimal)a.Importe,
                        Numero = a.NroDeCheque,
                        NombreDeBanco = mtdCobranzas.GetNombreDelBanco((int)a.BancoID)
                      };
        dgv_Cheques.DataSource = cheques.ToList();
      }
    }

    private void MostrarTransf()
    {
      using (var context = new lts_sindicatoDataContext())
      {
        int NroDeRecibo = Convert.ToInt32(dgv_VerRecibos.CurrentRow.Cells["Recibo"].Value);
        var transf = from a in context.Transferencias
                     where (a.NroDeRecibo == NroDeRecibo)
                     select new mdlTransf
                     {
                       Fecha = (DateTime)a.FechaDeTransf,
                       Importe = (decimal)a.Monto,
                       NroDeTransf = a.NroDeTransf,
                       Banco = mtdCobranzas.GetNombreDelBanco((int)a.BancoId)
                     };
        dgv_Transf.DataSource = transf.ToList();
      }
    }

    private void MostrarRecibosManuales()
    {
      txt_FechaManual.Text = dgv_VerRecibos.CurrentRow.Cells["FechaManual"].Value.ToString();
      txt_ReciboManual.Text = dgv_VerRecibos.CurrentRow.Cells["ReciboManual"].Value.ToString();
    }

    private void btn_ImprimirTodos_Click(object sender, EventArgs e)
    {
      using (var context = new lts_sindicatoDataContext())
      {
        var ff = from a in context.Recibos
                 where a.NroDeActa == _NroDeActa
                 join b in context.RecibosDetalle on a.NroAutomatico equals b.NroAut
                 select new mdlRecibosReporte
                 {
                   Cuota = b.NroCuota.ToString(),
                   Importe = Convert.ToDecimal(b.Importe),
                   NroReciboAutomatico = a.NroAutomatico.ToString(),
                   FechaAutomatica = Convert.ToDateTime(a.FechaAutomatica),
                   NroReciboManual = a.NroManual == null ? "" : a.NroManual,
                   FechaManual = a.FechaManual == null ? "" : a.FechaManual,
                   Cheques = getCheques((int)b.NroAut)
                 };
      }
    }
    private List<mdlCargaDeCheques> getCheques(int NroDeRecibo)
    {
      using (var context = new lts_sindicatoDataContext())
      {
        var cheques = from a in context.cheques
                      where (a.NroDeRecibo == NroDeRecibo)
                      select new mdlCargaDeCheques
                      {
                        FechaEmision = a.FechaDeEmision,
                        Importe = (decimal)a.Importe,
                        Numero = a.NroDeCheque,
                        NombreDeBanco = mtdCobranzas.GetNombreDelBanco((int)a.BancoID)
                      };
        return cheques.ToList();
      }
    }

    private void btn_AprobarRecibo_Click_1(object sender, EventArgs e)
    {
        
    }
  }
}
