using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.EventoDao
{
    public interface IComentarioDao : IGenericDao<Comentario, Int64>
    {
        /// <summary>
        /// Muestra los comentarios de un evento con paginacion
        /// </summary>
        /// <param name="idEvento">id del evento que queremos ver los comentarios</param>
        /// <param name="startIndex">Indice del primer resultado de la búsqueda</param>
        /// <param name="count">Número de resultados devueltos</param>
        /// <returns>La lista de comentarios del evento</returns>
        Collection<Comentario> VerComentarios(long idEvento, int startIndex = 0, int count = 0);

        /// <summary>
        /// Muestra los comentarios creados por un usuario en un evento
        /// </summary>
        /// <param name="idUsuario">Id del usuario</param>
        /// <param name="idEvento">Id del evento</param>
        /// <returns>La lista de comentarios del evento creados por el usuario</returns>
        Collection<Comentario> BuscarPorUsuario(long idUsuario, long idEvento);
    }
}
