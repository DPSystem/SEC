using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Data.Linq;

namespace entrega_cupones.Clases
{
    class futbol
    {
        public Binary foto { get; set; }
        public string equipo_nombre { get; set; }
        public string equipo_categoria { get; set; }
        public int nro_fecha { get; set; }
        public int cantidad_fechas_sancion { get; set; }

        public List<cls_sancionados> lst_sanciones = new List<cls_sancionados>();
        public List<cls_equipos> lst_equipos = new List<cls_equipos>();
        public List<cls_categorias> lst_categorias = new List<cls_categorias>();

        public class cls_categorias
        {
            public int catid { get; set; }
            public string catnombre { get; set; }
            public int catedadlimite { get; set; }
            public int catestado { get; set; }
        }

        public class cls_equipos
        {
            public int equipoid { get; set; }
            public string equiponombre { get; set; }
            public int equipocat_id { get; set; }
        }

        public class cls_sancionados
        {
            public int fecha { get; set; }
            public string  de { get; set; }
            public int total_fechas { get; set; }
            public string cumplio { get; set; }
        }

        public Binary get_Foto(double cuil)
        {
            convertir_imagen cnv_img = new convertir_imagen();
            using (var context = new lts_sindicatoDataContext())
            {
                var foto_ = from a in context.fotos where a.FOTOS_CUIL == cuil && a.FOTOS_CODFLIAR == 0 select a;
                if (foto_.Count() > 0)
                {
                    foto = foto_.Single().FOTOS_FOTO;
                }
                else
                {
                    // cuando no hay foto trae la imagen del contorno del usuario.
                    foto = cnv_img.ImageToByteArray(Properties.Resources.contorno_usuario);
                }
            };
            return foto;
        }

        public string get_equipo_nombre(int equipo_id)
        {
            using (var context = new lts_sindicatoDataContext())
            {

                var nombre_equipo = from a in context.equipos where a.EQUIPOID == equipo_id select a;
                if (nombre_equipo.Count() > 0)
                {
                    equipo_nombre = nombre_equipo.Single().EQUIPONOMBRE;
                }
                
            }
            return equipo_nombre;
        }

        public string get_equipo_categoria(int cat_id)
        {
            using (var context = new lts_sindicatoDataContext())
            {
                var nombre_equipo = from a in context.categorias where a.CATID == cat_id select a;
                equipo_categoria = nombre_equipo.Single().CATNOMBRE;
            }
            return equipo_categoria;
        }

        public string get_ya_inscripto(double cuil)
        {
            using (var context = new lts_sindicatoDataContext())
            {
                var ya_inscripto = from a in context.jugadores
                                   where a.JUG_SOCCENCUIL == cuil
                                   select new
                                   {
                                       equipo_id = a.JUG_EQUIPOID,
                                   };
                if (ya_inscripto.Count() > 0)
                {
                    equipo_nombre = get_equipo_nombre(ya_inscripto.Single().equipo_id);
                }
                else
                {
                    equipo_nombre = string.Empty;
                }
                return equipo_nombre;
            }
        }

        public void insertar_jugador(int equipo_id, string nro_afil, double cuil, string nombre, string apellido, int tipo)
        {
            using (var context = new lts_sindicatoDataContext())
            {
                try
                {
                    jugadores insert = new jugadores();
                    insert.JUG_EQUIPOID = equipo_id;
                    insert.JUG_MAESOC_NROAFIL = nro_afil;
                    insert.JUG_SOCCENCUIL = cuil;
                    insert.JUG_NOMBRE = nombre.ToUpper();
                    insert.JUG_APELLIDO = apellido.ToUpper();
                    insert.JUG_TIPO = tipo;
                    insert.JUG_FOTO = get_Foto(cuil);
                    context.jugadores.InsertOnSubmit(insert);
                    context.SubmitChanges();
                }
                catch (Exception)
                {

                    throw;
                }

            }
        }

        public void crear_sancion(int jugador_id, DateTime desde, DateTime hasta, int cantidad)
        {
            using (var context = new lts_sindicatoDataContext())
            {
                for (int i = 0; i < cantidad; i++)
                {
                    sanciones sanci = new sanciones();
                    sanci.ID_JUG = jugador_id;
                    sanci.FECHA_DESDE = desde;
                    sanci.FECHA_HASTA = hasta;
                    sanci.CANTIDAD_FECHAS = cantidad;
                    sanci.NRO_FECHA = i + 1;
                    sanci.FECHA_PARTIDO = desde;
                    //f = f.AddDays(7);
                    sanci.ID_PARTIDO = 0;
                    context.sanciones.InsertOnSubmit(sanci);
                    context.SubmitChanges();
                }
            }
        }

        public int get_nro_fecha_sancion(int jugador_id)
        {
            nro_fecha = 0;
            using (var context = new lts_sindicatoDataContext())
            {
                var obtener_sancion = (from a in context.sanciones
                                       where a.ID_JUG == jugador_id && a.ID_PARTIDO == 0
                                       select a);
                if (obtener_sancion.Count() > 0)
                {
                    nro_fecha = obtener_sancion.Min(x => x.NRO_FECHA);
                }
            }
            return nro_fecha;
        }

        public int get_cantidad_fecha_sancion(int jugador_id)
        {
            cantidad_fechas_sancion = 0;
            using (var context = new lts_sindicatoDataContext())
            {
                var obtener_sancion = (from a in context.sanciones
                                       where a.ID_JUG == jugador_id && a.ID_PARTIDO == 0
                                       select a);
                if (obtener_sancion.Count() > 0)
                {
                    cantidad_fechas_sancion = obtener_sancion.Min(x => x.CANTIDAD_FECHAS);
                }
            }
            return cantidad_fechas_sancion;
        }

        public List<cls_sancionados> get_lista_sanciones(int id_jugador)
        {
            try
            {
                using (var context = new lts_sindicatoDataContext())
                {
                    var sncs = from a in context.sanciones where a.ID_JUG == id_jugador select a;
                    if (sncs.Count() > 0)
                    {
                        foreach (var item in sncs.ToList())
                        {
                            cls_sancionados sncs_insert = new cls_sancionados();
                            sncs_insert.fecha = item.NRO_FECHA;
                            sncs_insert.de = "de";
                            sncs_insert.total_fechas = item.CANTIDAD_FECHAS;
                            sncs_insert.cumplio = get_fecha_cumplio_sancion(Convert.ToInt32(item.ID_PARTIDO));
                            lst_sanciones.Add(sncs_insert);
                        }
                    }
                    return lst_sanciones;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private string get_fecha_cumplio_sancion(int idPartido)
        {
            try
            {
                using (lts_sindicatoDataContext context = new lts_sindicatoDataContext())
                {
                    string fecha = string.Empty;
                    var fecha_cumplimiento = from a in context.partidos where a.PARTIDOID == idPartido select new { a.PARTIDOFECHA };

                    if (fecha_cumplimiento.Count() > 0 )
                    {
                        //fecha = fecha_cumplimiento.First().PARTIDOFECHA.Date.ToShortDateString();
                        fecha = "SI";
                    }
                    else
                    {
                        fecha = "NO";
                    }
                    return fecha;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<cls_equipos> get_equipos_todos()
        {
            try
            {
                using (lts_sindicatoDataContext context = new lts_sindicatoDataContext())
                {
                    var equipos = (from a in context.equipos select a).ToList().OrderBy(x=>x.EQUIPONOMBRE);
                    if (equipos.Count() > 0 )
                    {
                        foreach (var item in equipos.ToList())
                        {
                            cls_equipos eq = new cls_equipos();
                            eq.equipoid = item.EQUIPOID;
                            eq.equiponombre = item.EQUIPONOMBRE;
                            eq.equipocat_id = item.EQUIPO_CATID;
                            lst_equipos.Add(eq);
                        }
                    }
                    return lst_equipos;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<cls_equipos> get_equipos_por_categoria(int catID)
        {
            try
            {
                using (lts_sindicatoDataContext context = new lts_sindicatoDataContext())
                {
                    var equipos = from a in context.equipos where a.EQUIPO_CATID == catID orderby a.EQUIPONOMBRE select a;
                    if (equipos.Count() > 0)
                    {
                        foreach (var item in equipos.ToList())
                        {
                            cls_equipos eq = new cls_equipos();
                            eq.equipoid = item.EQUIPOID;
                            eq.equiponombre = item.EQUIPONOMBRE;
                            eq.equipocat_id = item.EQUIPO_CATID;
                            lst_equipos.Add(eq);
                        }
                    }
                    return lst_equipos;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<cls_categorias> get_categorias_todas()
        {
            try
            {
                using (lts_sindicatoDataContext context = new lts_sindicatoDataContext())
                {
                    var categoria = from a in context.categorias orderby a.CATNOMBRE select a;
                    if (categoria.Count() > 0)
                    {
                        foreach (var item in categoria.ToList())
                        {
                            cls_categorias cat = new cls_categorias();
                            cat.catid = item.CATID;
                            cat.catnombre = item.CATNOMBRE;
                            cat.catestado = item.CATESTADO;
                            cat.catedadlimite = item.CATEDADLIMITE;
                            lst_categorias.Add(cat);
                        }
                    }
                    return lst_categorias;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<cls_categorias> get_categorias_por_id(int catID)
        {
            try
            {
                using (lts_sindicatoDataContext context = new lts_sindicatoDataContext())
                {
                    var categoria = from a in context.categorias where a.CATID == catID orderby a.CATNOMBRE select a;
                    if (categoria.Count() > 0)
                    {
                        foreach (var item in categoria.ToList())
                        {
                            cls_categorias cat = new cls_categorias();
                            cat.catid = item.CATID;
                            cat.catnombre = item.CATNOMBRE;
                            cat.catestado = item.CATESTADO;
                            cat.catedadlimite = item.CATEDADLIMITE;
                            lst_categorias.Add(cat);
                        }
                    }
                    return lst_categorias;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
