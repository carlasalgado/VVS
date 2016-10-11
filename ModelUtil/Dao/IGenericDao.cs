using System;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System.Collections.Generic;

namespace Es.Udc.DotNet.ModelUtil.Dao
{
    /// <summary>
    /// Interfaces with Generic Dao Operations
    /// </summary>
    /// <typeparam name="E">Entity Type</typeparam>
    /// <typeparam name="PK">Primary Key Type.</typeparam>
    public interface IGenericDao<E, PK> 
    {

        /// <summary>
        /// Creates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Create(E entity);

        /// <summary>
        /// Finds the entity with the specified primary key.
        /// </summary>
        /// <param name="id">The entity id.</param>
        /// <exception cref="InstanceNotFoundException"></exception>
        /// <returns>The entity</returns>
        E Find(PK id);

        /// <summary>
        /// Determines if the specified entity exists.
        /// </summary>
        /// <param name="id">The  entity id.</param>
        /// <returns>True if entity exists, else otherwise.</returns>
        Boolean Exists(PK id);

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity to be updated.</param>        
        void Update(E entity);

        /// <summary>
        /// Removes the specified entity.
        /// </summary>
        /// <param name="id">The entity id.</param>
        /// <exception cref="InstanceNotFoundException"></exception>
        void Remove(PK id);


        /// <summary>
        /// Gets all elements of th especied entity.
        /// </summary>
        /// <returns></returns>
        List<E> GetAllElements();
    }
}
