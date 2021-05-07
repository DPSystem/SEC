using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using entrega_cupones.Formularios;
using System.Windows.Forms;

namespace entrega_cupones
{
  static class Program
  {
    /// <summary>
    /// Punto de entrada principal para la aplicación.
    /// </summary>

    [STAThread]
    static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);

      // instancio un formulario de login, lo abro  y controlo el dialog.result 
      // si da OK abro el frm_principal con la Application.Run

      Login frm_login = new Login();
      frm_login.ShowDialog();

      if (frm_login.DialogResult == DialogResult.OK)
      {
        int id = frm_login.id_usuario;
        string user = frm_login.usuario;
        string dni = frm_login.dni;
        string rol = frm_login.rol;
        int rolID = Convert.ToInt32(frm_login.rolID);
        //        Application.Run(new frm_principal(id,user, dni,rol,rolID)); 
        Application.Run(new frm_Principal2(id, user, dni, rol, rolID));
      }
    }
  }
}
