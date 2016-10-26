using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.GrupoDao
{

    //Tanto si no existe el grupo como si este no tiene recomendaciones de eventos, devuelve lista vacia
    public class RecomendacionDaoEntityFramework :
    GenericDaoEntityFramework<Recomendacion, Int64>, IRecomendacionDao
    {

        public bool BuscarRecomendacion(long idGrupo, long idEvento) {
            DbSet<Recomendacion> recomendaciones = Context.Set<Recomendacion>();

            var result =
                (from r in recomendaciones
                 from g in r.Grupo
                 where ((g.idGrupo == idGrupo)&&(r.Evento.idEvento == idEvento))              
                 orderby r.idRecomendacion ascending
                 select r);

            return (result.Count() != 0);
        }

    }
}
