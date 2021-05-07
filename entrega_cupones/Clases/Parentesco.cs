using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entrega_cupones.Clases
{
  class Parentesco
  {

    public List<Cls_Parentesco> LstParenteso = new List<Cls_Parentesco>();
    public Cls_Parentesco Parent = new Cls_Parentesco();

    public class Cls_Parentesco
    {
      public int parent_id { get; set; }
      public string parent_descrip { get; set; }
      public int parent_estado { get; set; }
      public int parent_codigo { get; set; }
    }

    public List<Cls_Parentesco> GetParentescoTodos()
    {
      using (lts_sindicatoDataContext context = new lts_sindicatoDataContext())
      {
        foreach (var item in context.parentesco.ToList().OrderBy(x => x.parent_descrip))
        {
          Cls_Parentesco insert = new Cls_Parentesco();
          insert.parent_id = item.parent_id;
          insert.parent_descrip = item.parent_descrip;
          insert.parent_estado = item.parent_estado;
          insert.parent_codigo = item.parent_codigo;
          LstParenteso.Add(insert);
        }
        return LstParenteso;
      }
    }

    public Cls_Parentesco GetParentescoDescrip(int _ParentCodigo)
    {
      using (lts_sindicatoDataContext context = new lts_sindicatoDataContext())
      {
        var parent_nombre = from a in context.parentesco where a.parent_codigo == _ParentCodigo select a; // .Where(x => x.parent_codigo == _ParentCodigo).Select(x => x.parent_descrip);
        if (parent_nombre.Count() > 0)
        {
          foreach (var item in parent_nombre.ToList())
          {
            //Cls_Parentesco insert = new Cls_Parentesco();
            Parent.parent_id = item.parent_id;
            Parent.parent_descrip = item.parent_descrip;
            Parent.parent_estado = item.parent_estado;
            Parent.parent_codigo = item.parent_codigo;
            //LstParenteso.Add(insert);
          }
        }
        return Parent;
      }
    }
  }
}
