using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace Es.Udc.DotNet.PracticaMaD.Model.ComentarioDao
{
    public class ComentarioDaoEntityFramework :
        GenericDaoEntityFramework<Comentario, Int64>, IComentarioDao
    {
        //Tanto si no existe el evento como si este no tiene comentarios, devuelve lista vacia
        public List<Comentario> VerComentarios(long idEvento, int startIndex = 0, int count = 0) {
            DbSet<Comentario> comentarios = Context.Set<Comentario>();

            var resultado =
                   (from c in comentarios
                    where c.Evento.idEvento == idEvento
                    orderby c.fecha descending
                    select c).Skip(startIndex);

            if (count > 0) return resultado.Take(count).ToList();

            return resultado.ToList();


        }

        public List<Comentario> BuscarPorUsuario(long idUsuario, long idEvento) {
            DbSet<Comentario> comentarios = Context.Set<Comentario>();

            var resultado =
                (from c in comentarios
                 where ((c.Evento.idEvento == idEvento) && (c.UserProfile.usrId == idUsuario))
                 orderby c.fecha descending
                 select c);

            return resultado.ToList();
        }

    }
}