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
    public partial class generar_Actas : Form
    {

        lts_sindicatoDataContext db_sindicato = new lts_sindicatoDataContext();

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

        public int act_id;
        public generar_Actas()
        {
            InitializeComponent();
        }

        private void generar_Acta_Load(object sender, EventArgs e)
        {

        }

        private void btn_cerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btn_cargar_acta_Click(object sender, EventArgs e)
        {
            cargar_acta();
            ////this.Close();
        }

        private void cargar_acta()
        {
            try
            {
                var cargar_acta = (from a in db_sindicato.ACTAS.Where(x => x.ID_ACTA == act_id) select a).Single();
                
                cargar_acta.FECHA = dtp_fecha_gen_acta.Value;
                cargar_acta.ACTA = Convert.ToDouble(txt_acta_nro.Text);
                cargar_acta.CUIT = Convert.ToDouble(lbl_cuit.Text);
                cargar_acta.DESDE = Convert.ToDateTime("01/" + txt_acta_desde.Text);
                cargar_acta.DEUDAHISTORICA = Convert.ToDouble(txt_acta_capital.Text);
                cargar_acta.INTERESES = Convert.ToDouble(txt_acta_interes.Text);
                cargar_acta.DEUDATOTAL = Convert.ToDouble(txt_acta_subtotal.Text);
                if (chk_cargar_financiacion.Checked)
                {
                    cargar_acta.ANTICIPO = Convert.ToDecimal(txt_acta_anticipo.Text);
                    cargar_acta.TASA = Convert.ToDecimal(txt_acta_tasa.Text);
                    cargar_acta.INTERESFINANC = Convert.ToDouble(txt_acta_interes_financ.Text);
                    cargar_acta.COEFICIENTE = Convert.ToDecimal(txt_acta_coeficiente.Text);
                    cargar_acta.CANTIDADCUOTAS = Convert.ToDouble(txt_acta_cuotas.Text);
                    cargar_acta.IMPORTE_CUOTA = Convert.ToDecimal(txt_acta_importe_cuota.Text);

                    // Genero el Registro para cobros.
                    if (txt_acta_anticipo.Text != "0.00") // SI hay anticipo, genero el registro para efecuar el cobro
                    {
                        COBROS cobro_ = new COBROS();
                        cobro_.ACTA = Convert.ToDouble(txt_acta_nro.Text);
                        cobro_.CUIT = Convert.ToDouble(lbl_cuit.Text.Trim());
                        cobro_.CONCEPTO = "1"; // 1 - ANTICIPO
                        cobro_.IMPORTE = Convert.ToDouble(txt_acta_anticipo.Text);
                        cobro_.TOTAL = Convert.ToDouble(txt_acta_anticipo.Text);
                        cobro_.FECHA_VENC = String.Format("{0:d}", dtp_venc_anticipo.Value);
                        db_sindicato.COBROS.InsertOnSubmit(cobro_);
                        db_sindicato.SubmitChanges();
                    }
                    // Genero el registro para cobros de cuotas de actas
                    DateTime fecha_venc = dtp_venc_anticipo.Value; //.Date.AddMonths(1);
                    for (int i = 1; i <= Convert.ToInt16(txt_acta_cuotas.Text); i++)
                    {

                        COBROS cobro = new COBROS();
                        cobro.ACTA = Convert.ToDouble(txt_acta_nro.Text);
                        cobro.CUIT = Convert.ToDouble(lbl_cuit.Text.Trim());
                        cobro.CONCEPTO = "2"; // 2 - CUOTA DE PLAN DE PAGO DE ACTA
                        cobro.IMPORTE = Convert.ToDouble(txt_acta_importe_cuota.Text);
                        cobro.TOTAL = Convert.ToDouble(txt_acta_importe_cuota.Text);
                        cobro.FECHA_VENC = String.Format("{0:d}", fecha_venc.Date.AddMonths(i));
                        cobro.CUOTAX = i.ToString();
                        cobro.CANTIDAD_CUOTAS = txt_acta_cuotas.Text;
                        db_sindicato.COBROS.InsertOnSubmit(cobro);
                        db_sindicato.SubmitChanges();
                    }

                }
                db_sindicato.SubmitChanges();
                cargar_novedad();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                throw;
            }

        }

        private void cargar_novedad()
        {
            try
            {
                // Cargar en la tabla novedades la generacion del acta
                //int ultima_acta = db_sindicato.ACTAS.OrderByDescending(x => x.ID_ACTA).First().ID_ACTA;
                //var ultima_acta = db_sindicato.ACTAS.Where(x => x.ID_ACTA == act_id).Single().ID_ACTA ; //  .OrderByDescending(x => x.ID_ACTA).First().ID_ACTA;
                Novedades nov = new Novedades();
                nov.Fecha = DateTime.Now;
                nov.Id_Acta = act_id;
                nov.Novedad = "Generacion de Acta";
                db_sindicato.Novedades.InsertOnSubmit(nov);
                db_sindicato.SubmitChanges();
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        private void chk_cargar_financiacion_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_cargar_financiacion.Checked == true)
            {
                panel1.Enabled = true;
            }
            else
            {
                panel1.Enabled = false;
            }
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void bunifuTileButton1_Click(object sender, EventArgs e)
        {

        }
    }
}
