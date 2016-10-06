using Es.Udc.DotNet.PracticaMaD.Model.GrupoDao;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.GrupoService
{
    public interface IGrupoService
    {
        /// <summary>
        /// Crea Un nuevo grupo
        /// </summary>
        /// <param name="grupo">Grupo a dar de alta</param>
        /// <param name="idUsuario">ID del usuario que crea el grupo</param>
        /// <returns>Id del grupo nuevo</returns>
        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="DuplicateInstanceException"/>
        long CrearGrupo(Grupo grupo, long idUsuario);

        /// <summary>
        /// Crea Un nuevo grupo
        /// </summary>
        /// <returns>Bloque de grupos</returns>
        /// <param name="startIndex">Indice del primer resultado de la búsqueda</param>
        /// <param name="count">Número de resultados devueltos</param>
        BloqueGrupos VerGrupos(int startIndex, int count);

        /// <summary>
        /// Da de alta a un usuario en un grupo
        /// </summary>
        /// <param name="idGrupo">Id del grupo</param>
        /// <param name="idUsuario">ID del usuario que se da de alta en el grupo</param>
        /// <exception cref="InstanceNotFoundException"/>
        void AltaGrupo(long idUsuario, long idGrupo);

        /// <summary>
        /// Da de baja a un usuario en un grupo, si el grupo se queda vacio, se elimina
        /// </summary>
        /// <param name="idGrupo">Id del grupo</param>
        /// <param name="idUsuario">ID del usuario dueño del grupo</param>
        /// <exception cref="InstanceNotFoundException"/>
        void BajaGrupo(long idUsuario, long idGrupo);

        /// <summary>
        /// Muestra la lista de grupos de un usuario
        /// </summary>
        /// <param name="idUsuario">ID del usuario del cual queremos obtener los grupos</param>
        /// <returns>Lista de DTO de grupos de un usuario</returns>
        /// <exception cref="InstanceNotFoundException"/>
        List<GrupoDTO> BuscarPorUsuario(long idUsuario);

        /// <summary>
        /// Busca un grupo por su identificador
        /// </summary>
        /// <param name="idGrupo">ID del grupo</param>
        /// <returns>DTO de un grupo</returns>
        /// <exception cref="InstanceNotFoundException"/>
        GrupoDTO BuscarGrupo(long idGrupo);

        /// <summary>
        /// Busca un grupo por su identificador
        /// </summary>
        /// <param name="idGrupo">ID del grupo</param>
        /// <returns>Grupo</returns>
        /// <exception cref="InstanceNotFoundException"/>
        Grupo BuscarUnGrupo(long idGrupo);

    }
}
