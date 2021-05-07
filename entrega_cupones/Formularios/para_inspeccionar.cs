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
    
    public partial class para_inspeccionar : Form
    {
        public int id_usuario { get; set; }

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


        public para_inspeccionar(double cuil)
        {
            InitializeComponent();
            // Buscadores obtener_foto = new Buscadores();
            futbol obtener_foto = new futbol();
            convertir_imagen conv_img = new convertir_imagen();
            picbox_socio.Image = conv_img.ByteArrayToImage(obtener_foto.get_Foto(cuil).ToArray());// .get_foto(cuil).ToArray());

        }

        private void btn_aceptar_Click(object sender, EventArgs e)
        {
            if (pasa_control() == true)
            {
                using (var context = new lts_sindicatoDataContext())
                {
                    ParaInspeccion insert = new ParaInspeccion();
                    insert.CUIL = txt_cuil.Text.ToString().Trim();
                    insert.CUIT = txt_cuit.Text.ToString().Trim();
                    insert.FECHA = DateTime.Today;
                    insert.ESTADO = 0 ;
                    try
                    {
                        context.ParaInspeccion.InsertOnSubmit(insert);
                        context.SubmitChanges();
                    }

                    catch (Exception)
                    {
                        throw;
                    }

                    try
                    {
                        if (txt_comentario.Text != string.Empty)
                        {
                            comentarios comit = new comentarios();
                            comit.ID_USUARIO = id_usuario;
                            comit.COMENTARIO = txt_comentario.Text;
                            comit.FECHA = DateTime.Today;
                            comit.PI_ID = context.ParaInspeccion.OrderByDescending(x => x.ID).First().ID;
                            context.comentarios.InsertOnSubmit(comit);
                            context.SubmitChanges();

                            MessageBox.Show("¡¡¡¡¡ Datos para la inspeccion, Cargados Existosamente !!!!! ");

                            btn_aceptar.Enabled = false;
                            Close();
                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
            }
        }

        private void btn_cerrar_inspeccionar_Click(object sender, EventArgs e)
        {
            btn_cancelar.Enabled = false;
            Close();

        }

        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            btn_cancelar.Enabled = false;
            Close();

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private bool pasa_control()
        {
            if (cbx_motivo.SelectedIndex > 0 )
            {
                return true;
            }
            else
            {
                MessageBox.Show("Falta seleccionar el ''MOTIVO''");
                return false;
            }
        }
    }
}
