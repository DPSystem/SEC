using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace entrega_cupones
{
    public partial class frm_cobros : Form
    {
        lts_sindicatoDataContext db_sindicato = new lts_sindicatoDataContext();

        public string cuit, razon_social;

        #region codigo para efecto shadow

        private bool Drag;
        private int MouseX;
        private int MouseY;

        private const int WM_NCHITTEST = 0x84;
        private const int HTCLIENT = 0x1;
        private const int HTCAPTION = 0x2;

        private bool m_aeroEnabled;

        private const int CS_DROPSHADOW = 0x00020000;
        private const int WM_NCPAINT = 0x0085;
        private const int WM_ACTIVATEAPP = 0x001C;

        [System.Runtime.InteropServices.DllImport("dwmapi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);
        [System.Runtime.InteropServices.DllImport("dwmapi.dll")]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);
        [System.Runtime.InteropServices.DllImport("dwmapi.dll")]

        public static extern int DwmIsCompositionEnabled(ref int pfEnabled);
        [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
            );

        public struct MARGINS
        {
            public int leftWidth;
            public int rightWidth;
            public int topHeight;
            public int bottomHeight;
        }
        protected override CreateParams CreateParams
        {
            get
            {
                m_aeroEnabled = CheckAeroEnabled();
                CreateParams cp = base.CreateParams;
                if (!m_aeroEnabled)
                    cp.ClassStyle |= CS_DROPSHADOW; return cp;
            }
        }
        private bool CheckAeroEnabled()
        {
            if (Environment.OSVersion.Version.Major >= 6)
            {
                int enabled = 0; DwmIsCompositionEnabled(ref enabled);
                return (enabled == 1) ? true : false;
            }
            return false;
        }
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_NCPAINT:
                    if (m_aeroEnabled)
                    {
                        var v = 2;
                        DwmSetWindowAttribute(this.Handle, 2, ref v, 4);
                        MARGINS margins = new MARGINS()
                        {
                            bottomHeight = 1,
                            leftWidth = 0,
                            rightWidth = 0,
                            topHeight = 0
                        }; DwmExtendFrameIntoClientArea(this.Handle, ref margins);
                    }
                    break;
                default: break;
            }
            base.WndProc(ref m);
            if (m.Msg == WM_NCHITTEST && (int)m.Result == HTCLIENT) m.Result = (IntPtr)HTCAPTION;
        }
        private void PanelMove_MouseDown(object sender, MouseEventArgs e)
        {
            Drag = true;
            MouseX = Cursor.Position.X - this.Left;
            MouseY = Cursor.Position.Y - this.Top;
        }
        private void PanelMove_MouseMove(object sender, MouseEventArgs e)
        {
            if (Drag)
            {
                this.Top = Cursor.Position.Y - MouseY;
                this.Left = Cursor.Position.X - MouseX;
            }
        }
        private void PanelMove_MouseUp(object sender, MouseEventArgs e) { Drag = false; }

        #endregion

        public frm_cobros()
        {
            InitializeComponent();
        }

        private void btn_cerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_cobros_Load(object sender, EventArgs e)
        {
            lbl_cuit.Text = cuit;
            lbl_razon_social.Text = razon_social;
            Cargar_cbx_bancos();
            mostrar_actas_involucradas();

        }

        private void Cargar_cbx_bancos()
        {
            var ban = from a in db_sindicato.Bancos
                      select new { id = a.ID, ban_nombre = a.BAN_NOMBRE.Trim() +  " [" + a.BAN_SUCURSAL.Trim() +"]" };
            cbx_banco.DisplayMember = "ban_nombre";
            cbx_banco.ValueMember = "id";
            cbx_banco.DataSource = ban.ToList();

        }

        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        //private void btn_buscar_empresa_Click(object sender, EventArgs e)
        //{
        //    frm_buscar_empresa f_buscar_empresa = new frm_buscar_empresa();
        //    f_buscar_empresa.DatosPasadosCobros += new frm_buscar_empresa.PasarDatosCobros(ejecutar);
        //    f_buscar_empresa.viene_desde = 2;
        //    f_buscar_empresa.ShowDialog();
        //    if (cuit != "")
        //    {
        //        get_actas();
        //    }

        //}

        //private void get_actas()
        //{
        //    var actas_involucradas = from a in db_sindicato.ACTAS
        //                             where a.CUIT == Convert.ToDouble(cuit.Text.Trim()) && (a.COBRADOTOTALMENTE != "SI" || a.COBRADOTOTALMENTE == null)
        //                             select new
        //                             {
        //                                 id = a.ID_ACTA ,
        //                                 acta = a.ACTA
        //                             };
        //    cbx_actas.DisplayMember = "acta";
        //    cbx_actas.ValueMember = "id";
        //    cbx_actas.DataSource = actas_involucradas.ToList();

        //}

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbx_tipo_pago_SelectedIndexChanged(object sender, EventArgs e)
        {

            switch (cbx_tipo_pago.SelectedIndex)
            {
                case 0 :
                    cbx_banco.Enabled = false;
                    break;
                case 1:
                    cbx_banco.Enabled = true;
                    break;
                case 2:
                    cbx_banco.Enabled = false;
                    break;
            }

            
        }

        public void ejecutar(string cuit, string razon_social)
        {
            //cuit = cuit;
            //razon_social = razon_social;
        }

        private void dgv_actas_inv_asig_SelectionChanged(object sender, EventArgs e)
        {
            mostrar_comprobantes();
        }

        private void mostrar_comprobantes()
        {

            var comprobantes_actas = from comp in db_sindicato.COBROS
                                     where comp.ACTA == Convert.ToInt32(dgv_actas_inv_asig.CurrentRow.Cells["num_acta"].Value)
                                     select new
                                     {
                                         cobro_id = comp.Id,
                                         cuota = (comp.CONCEPTO == "2") ? (comp.CUOTAX.ToString() + " de " + comp.CANTIDAD_CUOTAS.ToString()) : ("Anticipo"),
                                         fecha_venc = comp.FECHA_VENC,
                                         fecha = comp.FECHARECAUDACION,
                                         comprobante = comp.RECIBO,
                                         importe = comp.TOTAL
                                     };
            dgv_cobros.DataSource = comprobantes_actas.ToList();
            if (dgv_cobros.Rows.Count > 0)
            {
                foreach (DataGridViewRow fila in dgv_cobros.Rows)
                {
                    double dias = (DateTime.Today.Date - Convert.ToDateTime(fila.Cells["f_venc"].Value).Date).TotalDays;
                    if (fila.Cells["cuota"].Value.ToString() != "Anticipo")
                    {

                        if ((DateTime.Today - Convert.ToDateTime(fila.Cells["f_venc"].Value)).TotalDays > 0)
                        {
                            fila.Cells["dias_atraso"].Value = dias;//(DateTime.Today - Convert.ToDateTime(fila.Cells["f_venc"].Value)).TotalDays;
                            fila.Cells["interes_mora"].Value = (0.01 * dias) * Convert.ToDouble(fila.Cells["monto_pago"].Value);
                        }
                        else
                        {
                            fila.Cells["dias_atraso"].Value = "0";
                        }

                    }
                }
            }
        }

        private void mostrar_actas_involucradas()
        {
            var actas_involucradas = from act in db_sindicato.ACTAS
                                     where act.CUIT == Convert.ToInt64(lbl_cuit.Text)
                                     select new
                                     {
                                         fecha_asig = act.FECHA_ASIG,
                                         acta = act.ACTA,
                                         desde = act.DESDE,
                                         hasta = act.HASTA,
                                         estado = act.COBRADOTOTALMENTE,
                                         inspector = act.INSPECTOR,
                                         importe_acta = act.DEUDATOTAL
                                     };

            dgv_actas_inv_asig.DataSource = actas_involucradas.ToList();


            if (actas_involucradas.Count() == 0)
            {
                dgv_cobros.DataSource = null;
                dgv_cobros.Refresh();
            }
            else
            {
                //btn_cobrar.Enabled = true;
              
            }


        }
    }
}
