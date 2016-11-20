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
    public class EventoDaoEntityFramework :
        GenericDaoEntityFramework<Evento, Int64>, IEventoDao
    {
        public EventoDaoEntityFramework() {   }
        public Collection<Evento> BuscarEventos(Collection<String> keyWords)
        {
            return BuscarEventos(keyWords, 0, 0);
        }

        public Collection<Evento> BuscarEventos(Collection<String> keyWords, int startIndex, int count) {
            DbSet<Evento> eventos = Context.Set<Evento>();
            Collection<Evento> coll = new Collection<Evento>();
            var resultado =
                (from e in eventos
                 where keyWords.All(s => e.nombre.Contains(s))
                 orderby e.fecha ascending
                 select e).Skip(startIndex);

            if (count > 0)
            {
                foreach (Evento evento in resultado.Take(count).ToList())
                {
                    coll.Add(evento);
                }
                return coll;
            }

            foreach (Evento evento in resultado.ToList())
            {
                coll.Add(evento);
            }
            return coll;
        }

        public Collection<Evento> BuscarEventos() {
            return BuscarEventos(0, 0);
        }


        public Collection<Evento> BuscarEventos(int startIndex, int count)
        {
            DbSet<Evento> eventos = Context.Set<Evento>();
            Collection<Evento> coll = new Collection<Evento>();

            var resultado =
                (from e in eventos
                 orderby e.fecha ascending
                 select e).Skip(startIndex);

            if (count > 0)
            {
                foreach (Evento evento in resultado.Take(count).ToList())
                {
                    coll.Add(evento);
                }
                return coll;
            }

            foreach (Evento evento in resultado.ToList())
            {
                coll.Add(evento);
            }
            return coll;
        }
    }
}
