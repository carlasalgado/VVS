
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.EventoService
{
    public interface IEventoService
    {
        #region Eventos
        /// <summary>
        /// Busca los eventos cuyo nombre coincida con las palabras clave que deseemos ordenados, mas reciente primero.
        /// </summary>
        /// <param name="palabrasClave">Palabras clave para la busqueda de eventos</param>
        /// <param name="startIndex">Indice del primer resultado de la búsqueda</param>
        /// <param name="count">Número de resultados devueltos</param>
        /// <returns>Lista de eventos que coinciden con las palabras clave</returns>

        BloqueEventos BusquedaEventos(String keyWords);        

        BloqueEventos BusquedaEventos(String keyWords, int startIndex, int count);

        /// <summary>
        /// Busca un evento por su identificador.
        /// </summary>
        /// <param name="idEvento">Identificador del evento.</param>
        /// <returns>DTO de un evento.</returns>
        /// <exception cref="InstanceNotFoundException"/>
        EventoDTO BuscarEvento(long idEvento);
        #endregion

        #region Comentarios
        /// <summary>
        /// Añade un comentario a un evento
        /// </summary>
        /// <param name="idEvento">Identificador del evento.</param>
        /// <param name="idUsuario">Identificador del usuario.</param>
        /// <param name="texto">Texto del comentario a añadir.</param>
        /// <returns>Identificador del nuevo comentario.</returns>
        /// <exception cref="InstanceNotFoundException"/>
        long AnadirComentario(long idEvento, long idUsuario, String texto);

        /// <summary>
        /// Modifica un comentario de un evento, solo si el usuario es propietario del comentario.
        /// </summary>
        /// <param name="idComentario">Identificador del comentario.</param>
        /// <param name="idUsuario">Identificador del usuario.</param>
        /// <param name="texto">Texto del comentario a modificar.</param>
        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="UsuarioNoPropietarioException"/>       
        void ModificarComentario(long idComentario, long idUsuario, String texto);

        /// <summary>
        /// Elimina un comentario de un evento, solo si el usuario es propietario del comentario.
        /// </summary>
        /// <param name="idComentario">Identificador del comentario.</param>
        /// <param name="idUsuario">Identificador del usuario.</param>
        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="UsuarioNoPropietarioException"/>      
        void EliminarComentario(long idComentario, long idUsuario);

        /// <summary>
        /// Muestra los comentarios de un evento.
        /// </summary>
        /// <param name="idEvento">Identificador del evento.</param>
        /// <param name="startIndex">Indice del primer resultado de la búsqueda</param>
        /// <param name="count">Número de resultados devueltos</param>
        /// <returns>Bloque de comentarios</returns>
        /// <exception cref="InstanceNotFoundException"/>

        BloqueComentarios VerComentarios(long idEvento);        

        BloqueComentarios VerComentarios(long idEvento, int startIndex, int count);

        /// <summary>
        /// Busca un comentario por su identificador.
        /// </summary>
        /// <param name="idComentario">Identificador del comentario.</param>
        /// <returns>DTO de un comentario</returns>
        /// <exception cref="InstanceNotFoundException"/>
        ComentarioDTO BuscarComentario(long idComentario);

        /// <summary>
        /// Busca los comentarios de un usuario sobre un evento.
        /// </summary>
        /// <param name="idUsuario">Identificador del usuario.</param>
        /// <param name="idEvento">Identificador del evento.</param>
        /// <returns>Una lista de DTOs de comentarios</returns>
        /// <exception cref="InstanceNotFoundException"/>
        Collection<ComentarioDTO> BuscarComentarioPorUsuario(long idUsuario, long idEvento);
        #endregion

        #region Recomendaciones

        /// <summary>
        /// Recomienda un evento a una lista de grupos.
        /// </summary>
        /// <param name="idEvento">Identificador del evento.</param>
        /// <param name="grupos">Lista de grupos a recomendar el evento.</param>
        /// <param name="texto">Texto de la recomendacion.</param>
        /// <returns>Recomendacion</returns>
        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="SinGruposException"/>
        Recomendacion RecomendarEvento(long idEvento, Collection<Grupo> grupos, String texto);

        /// <summary>
        /// Muestra las recomendaciones de un de un usuario
        /// </summary>
        /// <param name="idUsuario">Identificador del usuario.</param>
        /// <returns>Lista de DTOS de recomendaciones</returns>
        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="SinGruposException"/>
        Collection<RecomendacionDTO> MostrarRecomendaciones(long idUsuario);

        /// <summary>
        /// Indica si el grupo ya tiene el evento recomendado
        /// </summary>
        /// <param name="idEvento">Identificador del evento.</param>
        /// <param name="idGrupo">Identificador del grupo.</param>
        /// <returns>Booleano indicando si el evento ya ha sido recomendado al grupo</returns>
        /// <exception cref="InstanceNotFoundException"/>
        bool GrupoRecomendado(long idEvento, long idGrupo);
        #endregion

        #region Etiquetas
        /// <summary>
        /// Crea una etiqueta nueva
        /// </summary>
        /// <param name="nombreEtiqueta">Nombre de la etiqueta nueva.</param>
        /// <returns>La nueva etiqueta</returns>
        /// <exception cref="DuplicateInstanceException"/>
        Etiqueta CrearEtiqueta(String nombreEtiqueta);

        /// <summary>
        /// Muestra las etiquetas asociadas a un comentario
        /// </summary>
        /// <param name="idComentario">Identificador del comentario.</param>
        /// <returns>Lista de etiquetas de un comentario</returns>
        /// <exception cref="InstanceNotFoundException"/>
        Collection<Etiqueta> EtiquetasDeComentario(long idComentario);

        /// <summary>
        /// Muestra todas las etiquetas ordenadas por numero de apariciones en los comentarios. 
        /// Las mas usadas primero.
        /// </summary>
        /// <returns>Lista ordenada de etiquetas</returns>
        Collection<Etiqueta> NubeEtiquetas();

        /// <summary>
        /// Sustituye las etiquetas de un comentario por las etiquetas que se le pasen. 
        /// </summary>
        /// <param name="idComentario">Identificador del comentario.</param>
        /// <param name="etiquetas">Lista de etiquetas que se asociaran con el comentario.</param>
        /// <exception cref="InstanceNotFoundException"/>
        void AnadirEtiqueta(long idComentario, Collection<Etiqueta> etiquetas);

        /// <summary>
        /// Muestra los comentarios asociados con una etiqueta 
        /// </summary>
        /// <param name="etiqueta">Nombre de la etiqueta.</param>
        /// <returns>Lista de DTOs de comentarios asociados con una etiqueta</returns>
        /// <exception cref="InstanceNotFoundException"/>
        Collection<ComentarioDTO> MostrarComentariosEtiqueta(String etiqueta);

        /// <summary>
        /// Busca una etiqueta por su identificador
        /// </summary>
        /// <param name="idEtiqueta">Identificador de la etiqueta.</param>
        /// <returns>Etiqueta</returns>
        /// <exception cref="InstanceNotFoundException"/>
        Etiqueta EtiquetaPorId(long idEtiqueta);
        #endregion
    }


}
