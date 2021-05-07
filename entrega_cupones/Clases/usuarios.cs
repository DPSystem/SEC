using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entrega_cupones.Clases
{
  public class usuarios
  {
    List<permisos> lst_permisos = new List<permisos>();


    public class permisos
    {
      public string objeto { get; set; }
      public int permiso { get; set; }
    }

    public List<permisos> get_permisos(int rolID)
    {
      using (var context = new lts_sindicatoDataContext())
      {
        //var permisos = from a in context.RolesControls
        //               join c in context.Controls on a.Control equals c.IdControls
        //               where a.IdRol == rolID
        //               select new
        //               {
        //                 control = c.Nombre,
        //                 estado = a.Estado
        //               };
        var permisos = from a in context.UsuariosObjetos
                       join c in context.Objetos on a.ObjetoId equals c.ID
                       where a.UsuarioId == rolID
                       select new
                       {
                         ObjetoNombre = c.Nombre,
                         a.Estado
                       };
        if (permisos.Count() > 0)
        {
          foreach (var item in permisos.ToList())
          {
            permisos permiso = new permisos();
            permiso.objeto = item.ObjetoNombre;
            permiso.permiso = Convert.ToInt32(item.Estado); // para saber si tiene permiso para ese control
            lst_permisos.Add(permiso);
          }
          //foreach (var item in permisos.ToList())
          //{
          //  permisos permiso = new permisos();
          //  permiso.objeto = item.control;
          //  permiso.permiso = item.estado; // para saber si tiene permiso para ese control
          //  lst_permisos.Add(permiso);
          //}
        }
      }
      return lst_permisos;
    }

    public string ObtenerNombreDeUsuario(int UsuarioId)
    {
      using (var context =  new lts_sindicatoDataContext())
      {
        var UsuarioNombre = context.Usuarios.Where(x => x.idUsuario == UsuarioId);
        return (UsuarioNombre.Count() > 0) ? UsuarioNombre.FirstOrDefault().Usuario : "";
      }
    }
  }
}
