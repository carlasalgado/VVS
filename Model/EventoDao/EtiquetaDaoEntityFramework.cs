using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.EventoDao
{
    public class EtiquetaDaoEntityFramework :
        GenericDaoEntityFramework<Etiqueta, Int64>, IEtiquetaDao
    {
        public Etiqueta BuscarPorNombre(String nombre) {

            DbSet<Etiqueta> etiquetas = Context.Set<Etiqueta>();

            var resultado =
                (from e in etiquetas
                 where e.nombre.ToLower() == nombre.ToLower()
                 select e);

            Etiqueta etiqueta = resultado.FirstOrDefault<Etiqueta>();
            return etiqueta;
        }

        public Collection<Etiqueta> NubeEtiquetas()
        {
            DbSet<Etiqueta> etiquetas = Context.Set<Etiqueta>();
            Collection<Etiqueta> collEtiquetas = new Collection<Etiqueta>();

            var resultado =
                (from e in etiquetas
                 orderby e.Comentario.Count descending
                 select e);

            foreach (Etiqueta e in resultado.ToList())
            {
                collEtiquetas.Add(e);
            }
            return collEtiquetas;
        }
    }
}