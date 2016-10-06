using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.EtiquetaDao
{
    public interface IEtiquetaDao : IGenericDao<Etiqueta, Int64>
    {
        /// <summary>
        /// Busca una etiqueta por nombre
        /// </summary>
        /// <param name="nombre">Nombre de la etiqueta</param>
        /// <returns>Etiqueta creada</returns>
        Etiqueta BuscarPorNombre(String nombre);

        /// <summary>
        /// Muestra todas las etiquetas ordenadas por numero de comentarios asociados de mayor a menor
        /// </summary>
        /// <returns>Lista de etiquetas ordenadas</returns>
        List<Etiqueta> NubeEtiquetas();
    }
}
