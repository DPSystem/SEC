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

namespace entrega_cupones
{
    public partial class Login : Form
    {
        public int id_usuario = 0;
        public string usuario = string.Empty;
        public string dni = string.Empty;
        public string rol = string.Empty;
        public string rolID = string.Empty;
        int progreso = 0;

        public Login()
        {
            InitializeComponent();
        }

        private void btn_cerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btn_aceptar_Click(object sender, EventArgs e)
        {
            validar_obenter_usuario();
          
        }

        private void validar_obenter_usuario()
        {
            if (obtener_usuario())
            {
                timer1.Start();
                mostrar_progreso_login();
            }
            else
            {
                mostrar_error_login();
            }
        }

        private void mostrar_progreso_login()
        {
            picbox_error.Visible = false;
            lbl_login_error.Visible = false;
            CircleProgressBar.Visible = true;
            lbl_cargando.Visible = true;
            picbox_logo.Visible = false;
        }

        private void mostrar_error_login()
        {
            picbox_error.Visible = true;
            lbl_login_error.Visible = true;
            CircleProgressBar.Visible = false;
            lbl_cargando.Visible = false;
            picbox_logo.Visible = false;
        }

        private void mostrar_logo()
        {
            picbox_error.Visible = false;
            lbl_login_error.Visible = false;
            CircleProgressBar.Visible = false;
            lbl_cargando.Visible = false;
            picbox_logo.Visible = true;
        }

        private bool obtener_usuario()
        {
            Buscadores buscar = new Buscadores();
            buscar.GetUsuario(txt_usuario.Text, txt_contraseña.Text);

            if (buscar.dat_soc.Id > 0 )
            {
                id_usuario = buscar.dat_soc.Id;
                usuario = buscar.dat_soc.nombre;
                dni = buscar.dat_soc.dni;
                rol = buscar.dat_soc.empresa; // Aqui almaceno el rol del usuario
                rolID = buscar.dat_soc.nrosocio;
                return true;
            }
            else
            {
                return false;
            }
        }

        private void CargarProgressBar()
        {
            progreso += 1;
            CircleProgressBar.Value = progreso;

            if (CircleProgressBar.Value == 100)
            {
                timer1.Stop();
                progreso = 0;
                CircleProgressBar.Visible = false;
                lbl_cargando.Visible = false;
                //this.id_usuario = 12;
                this.DialogResult = DialogResult.OK; // coloco el dialog result a OK para que inicie el formulario principal
                
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            CargarProgressBar();
        }

        private void txt_usuario_Enter(object sender, EventArgs e)
        {
            mostrar_logo();
        }

        private void txt_contraseña_Enter(object sender, EventArgs e)
        {
            mostrar_logo();
        }

        private void txt_usuario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_contraseña.Focus();
            }
        }

        private void txt_contraseña_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                validar_obenter_usuario();
            }
        }
    }
}
