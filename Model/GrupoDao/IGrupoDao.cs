using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.GrupoDao
{
    public interface IGrupoDao : IGenericDao<Grupo, Int64>
    {
        /// <summary>
        /// Busca los grupos de un usuario
        /// </summary>
        /// <param name="usrId">Id del usuario</param>
        /// <returns>Lista de grupos del usuario</returns>
        Collection<Grupo> BuscarPorUsuario(long usrId);

        /// <summary>
        /// Busca un grupo por nombre
        /// </summary>
        /// <param name="nombre">Nombre del grupo</param>
        /// <returns>Grupo con el nombre a buscar</returns>
        Grupo BuscarPorNombre(string nombre);

        /// <summary>
        /// Muestra todos los grupos
        /// </summary>
        /// <returns>Lista de grupos con paginacion</returns>
        Collection<Grupo> MostrarGrupos(int startIndex, int count);
    }
}
