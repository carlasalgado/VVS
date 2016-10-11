using System;
using System.Data;
using System.Linq;

using Es.Udc.DotNet.ModelUtil.Exceptions;
using Microsoft.Practices.Unity;
using Es.Udc.DotNet.ModelUtil.Log;
using System.Data.Entity;
using System.Collections.Generic;
using System.Data.Entity.Core;


namespace Es.Udc.DotNet.ModelUtil.Dao
{
    public class GenericDaoEntityFramework<E, PK> : IGenericDao<E, PK>
        where E : class
    {

        // entityClass is set in the constructor of this class
        private Type entityClass;

        // Context must be set by means of Context property
        private DbContext dbContext;


        public GenericDaoEntityFramework()
        {
            this.entityClass = typeof(E);
        }

        [Dependency]
        public DbContext Context
        {
            set
            {
                this.dbContext = value;
            }

            get
            {
                return dbContext;
            }

        }


        #region IGenericDao<E> Members

        public void Create(E entity)
        {
            dbContext.Set<E>().Add(entity);
            dbContext.SaveChanges();            
        }

        /// <exception cref="InstanceNotFoundException"/>
        public E Find(PK id)
        {
            E result = dbContext.Set<E>().Find(id);

            if (result == null)
                throw new InstanceNotFoundException(id, entityClass.FullName);
            else
                return result;

        }

        public Boolean Exists(PK id)
        {

            return (dbContext.Set<E>().Find(id) != null);
            
        }

        public void Update(E entity)
        {
            dbContext.Entry<E>(entity).State = EntityState.Modified;
            dbContext.SaveChanges();

        }

        /// <exception cref="InstanceNotFoundException"/>
        public void Remove(PK id)
        {           
            E objectToRemove = default(E);

            try
            {
                // First we need to find the object
                objectToRemove = this.Find(id);
                dbContext.Set<E>().Remove(objectToRemove);
                dbContext.SaveChanges();
            }
            catch (InstanceNotFoundException)
            {
                throw;
            }
            catch (InvalidOperationException)
            {
                throw new InstanceNotFoundException(id, entityClass.FullName);
            }
            catch(OptimisticConcurrencyException)
            {
                var context = 
                    ((System.Data.Entity.Infrastructure.IObjectContextAdapter)dbContext).ObjectContext;

                context.Refresh(System.Data.Entity.Core.Objects.RefreshMode.ClientWins, objectToRemove);
                context.DeleteObject(objectToRemove);
                context.SaveChanges();
            }
        }

        public List<E> GetAllElements()
        {
            return dbContext.Set<E>().ToList<E>();
        }

        #endregion

    }

}