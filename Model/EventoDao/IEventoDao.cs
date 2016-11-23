using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.EventoDao
{
    public interface IEventoDao : IGenericDao<Evento, Int64>
    {
        /// <summary>
        /// Busca los eventos cuyo nombre coincida con las palabras clave que deseemos ordenados, mas reciente primero.
        /// </summary>
        /// <param name="palabrasClave">Palabras clave para la busqueda de eventos</param>
        /// <param name="startIndex">Indice del primer resultado de la búsqueda</param>
        /// <param name="count">Número de resultados devueltos</param>
        /// <returns>Lista de eventos que coinciden con las palabras clave</returns>

        Collection<Evento> BuscarEventos(Collection<String> palabrasClave);        

        Collection<Evento> BuscarEventos(Collection<String> palabrasClave, int startIndex, int count);

        /// <summary>
        /// Muestra todos los eventos ordenados, mas reciente primero.
        /// </summary>
        /// <param name="startIndex">Indice del primer resultado de la búsqueda</param>
        /// <param name="count">Número de resultados devueltos</param>
        /// <returns>Lista de eventos que coinciden con las palabras clave</returns>
        Collection<Evento> BuscarEventos();        
        Collection<Evento> BuscarEventos(int startIndex, int count);

    }
}
