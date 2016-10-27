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
    public class ComentarioDaoEntityFramework :
        GenericDaoEntityFramework<Comentario, Int64>, IComentarioDao
    {
        //Tanto si no existe el evento como si este no tiene comentarios, devuelve lista vacia
        public Collection<Comentario> VerComentarios(long idEvento, int startIndex, int count) {
            DbSet<Comentario> comentarios = Context.Set<Comentario>();
            Collection<Comentario> coll = new Collection<Comentario>();

            var resultado =
                   (from c in comentarios
                    where c.Evento.idEvento == idEvento
                    orderby c.fecha descending
                    select c).Skip(startIndex);

            if (count > 0)
            {

                foreach (Comentario comentario in resultado.Take(count).ToList())
                {
                    coll.Add(comentario);
                }
                return coll;
            }

            foreach (Comentario comentario in resultado.ToList())
            {
                coll.Add(comentario);
            }
            return coll;

        }

        public Collection<Comentario> BuscarPorUsuario(long idUsuario, long idEvento) {
            DbSet<Comentario> comentarios = Context.Set<Comentario>();
            Collection<Comentario> coll = new Collection<Comentario>();

            var resultado =
                (from c in comentarios
                 where ((c.Evento.idEvento == idEvento) && (c.UserProfile.usrId == idUsuario))
                 orderby c.fecha descending
                 select c);

            foreach (Comentario comentario in resultado.ToList())
            {
                coll.Add(comentario);
            }
                     
            return coll;
        }

    }
}