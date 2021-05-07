using entrega_cupones.Clases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entrega_cupones.Metodos
{
  class mtdFilial
  {
    public static DataTable Get_DatosFilial()
    {
      DS_cupones ds = new DS_cupones();
      DataTable dt_Filial = ds.Filial;
      dt_Filial.Clear();

      using (var context = new lts_sindicatoDataContext())
      {
        foreach (var item in context.Filial)
        {
          DataRow row = dt_Filial.NewRow();
          row["Nombre"] = item.Nombre;
          row["Domicilio"] = item.Domicilio + " - " + item.Provincia + " - " + item.Localidad + " - " + item.Telefono + " - " + item.Email;
          row["Localidad"] = item.Localidad;
          row["Telefono"] = item.Telefono;
          row["Provincia"] = item.Provincia;
          row["Email"] = item.Email;
          row["Logo"] = mtdConvertirImagen.ImageToByteArray(Image.FromFile("C:\\SEC_Gestion\\Imagen\\Logo_reporte.png"));
          dt_Filial.Rows.Add(row);
        }
      }
      return dt_Filial;
    }
  }
}
