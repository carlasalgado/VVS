using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.RecomendacionDao
{
    public interface IRecomendacionDao : IGenericDao<Recomendacion, Int64>
    {
        /// <summary>
        /// Indica si un evento está recomendado en un grupo
        /// </summary>
        /// <param name="idGrupo">Id del grupo</param>
        /// <param name="idEvento">Id del evento</param>
        /// <returns>Si esta recomendado TRUE, si no FALSE/returns>
        bool BuscarRecomendacion(long idGrupo, long idEvento);

    }
}
