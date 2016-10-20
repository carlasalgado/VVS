using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.EventoDao
{
    public class EventoDaoEntityFramework :
        GenericDaoEntityFramework<Evento, Int64>, IEventoDao
    {
        public EventoDaoEntityFramework() {   }

        public List<Evento> BuscarEventos(List<String> keyWords, int startIndex=0, int count=0) {
            DbSet<Evento> eventos = Context.Set<Evento>();

            var resultado =
                (from e in eventos
                 where keyWords.All(s => e.nombre.Contains(s))
                 orderby e.fecha ascending
                 select e).Skip(startIndex);

            if (count > 0) return resultado.Take(count).ToList();

            return resultado.ToList();
        }

        public List<Evento> BuscarEventos(int startIndex = 0, int count = 0)
        {
            DbSet<Evento> eventos = Context.Set<Evento>();

            var resultado =
                (from e in eventos
                 orderby e.fecha ascending
                 select e).Skip(startIndex);

            if (count > 0) return resultado.Take(count).ToList();

            return resultado.ToList();
        }

        public Int64 B()
        {
            return 0;
        }
    }
}
