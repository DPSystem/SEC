using System;
using System.Data.Linq;
using System.Windows.Forms;
using entrega_cupones.Clases;

namespace entrega_cupones
{
  public partial class frm_ingreso_poli : Form
    {
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


        //lts_sindicatoDataContext db_sindicato = new lts_sindicatoDataContext();
        //Buscadores buscar = new Buscadores();

        public frm_ingreso_poli()
        {
            InitializeComponent();
        }

        private void frm_ingreso_poli_Load(object sender, EventArgs e)
        {

        }

        private void btn_cerrar_Click(object sender, EventArgs e)
        {
            Close();

        }

        private void btn_buscar_Click(object sender, EventArgs e)
        {
            buscar_socio();
        }
        private void buscar_socio()
        {
            //2726954776100046488819 fiore
            //2726954776127269547761
            string dato = txt_buscar.Text;//txt_buscar.Text.Substring(2, 8);
            if (dato.Length > 11)
            {
                if (dato.Substring(11, 3) == "000") // entonces es socio beneficiario sino es titular
                {
                    Buscadores buscar = new Buscadores();
                    dato = dato.Substring(14, 8);
                    buscar.get_beneficiario(dato);
                    mostrar_datos_personales(buscar);
                    mostrar_foto(buscar.dat_soc.foto, buscar.dat_soc.estado);
                }
                else  //es socio titular
                {
                    Buscadores buscar = new Buscadores();
                    dato = dato.Substring(2, 8);
                    buscar.get_titular(dato);
                    mostrar_datos_personales(buscar);
                    mostrar_foto(buscar.dat_soc.foto, buscar.dat_soc.estado);
                }
            }
            else
            {
                if (dato.Length > 6 && dato.Length <= 8) // Se ingreso un DNI por tipeo
                {
                    buscar_soccen_benef(dato);
                }
                else
                {
                    if (dato.Length > 8 && dato.Length <= 10)
                    {
                        if ((dato.Length == 9))
                        {
                            buscar_soccen_benef(dato.Substring(1, 8));
                        }
                        else
                        {
                            buscar_soccen_benef(dato.Substring(2, 8));
                        }
                    }
                    else
                    {
                        //picbox_socio.Image = entrega_cupones.Pro
                        lbl_apto.Text = "SOCIO NO ENCONTRADO";
                    }
                }
            }
        }

        private void buscar_soccen_benef(string dni)
        {
            Buscadores buscar_ = new Buscadores();
            //buscar_.get_titular(dni);
            buscar_.get_beneficiario(dni);
            if (buscar_.dat_soc != null)
            {
                mostrar_datos_personales(buscar_);
                mostrar_foto(buscar_.dat_soc.foto, buscar_.dat_soc.estado);
            }
            else
            {

                Buscadores buscar = new Buscadores();
                //buscar.get_beneficiario(dni); 
                buscar.get_titular(dni);
                if (buscar.dat_soc != null)
                {
                    mostrar_datos_personales(buscar);
                    mostrar_foto(buscar.dat_soc.foto, buscar.dat_soc.estado);
                }
                else
                {
                    //picbox_socio.Image = entrega_cupones.Properties.Resources.imagen_no_disponible_PNG;
                    lbl_apto.Text = "SOCIO NO ENCONTRADO";
                }
            }
        }

        private void mostrar_foto(Binary fot, int estado)
        {
            if (fot != null)
            {
                convertir_imagen conv_img = new convertir_imagen();
                picbox_socio.Image = conv_img.ByteArrayToImage(fot.ToArray());
            }
            else
            {
                //picbox_socio.Image = entrega_cupones.Properties.Resources.imagen_no_disponible_PNG;
            }
            if (estado == 1)
            {
                lbl_apto.Text = "SOCIO APTO";
            }
            else
            {
                lbl_apto.Text = "SOCIO NO APTO";
            }
        }

        private void mostrar_datos_personales(Buscadores datos_personales)
        {
            lbl_nombre.Text = datos_personales.dat_soc.nombre;
            lbl_dni.Text = datos_personales.dat_soc.dni;
            lbl_sexo.Text = datos_personales.dat_soc.sexo.ToString();
            lbl_nro_socio.Text = datos_personales.dat_soc.nrosocio.ToString();

        }

        private void txt_buscar_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_buscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buscar_socio();
            }
        }
    }
}
