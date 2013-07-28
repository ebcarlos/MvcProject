using Common.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Common.BaseClass
{
    /// <summary>
    /// Class to manage entities
    /// </summary>
    /// <typeparam name="T_Entitie">Type of entitie (Table)</typeparam>
    abstract public class BaseRepository<T_Entitie> : IDisposable
        where T_Entitie : class
    {
        #region Constructor & Disposable

        private DbContext _context;

        public BaseRepository(string ConnectionString)
        {
            _context = new DbContext(ConnectionString);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        #endregion Constructor & Disposable

        #region Methods

        /// <summary>
        /// Get a single entity from the database with its id
        /// </summary>
        /// <param name="id">Id to search</param>
        /// <param name="bEnableLazyLoading">Determines whether objects accessible by navigation property are loaded automatically</param>
        public T_Entitie GetById(int id, bool bEnableLazyLoading = false)
        {
            return _context.Set<T_Entitie>().Find(id);
        }

        /// <summary>
        /// Get all entities from the database
        /// </summary>
        /// <param name="bEnableLazyLoading">Determines whether objects accessible by navigation property are loaded automatically</param>
        public IList<T_Entitie> Get(bool bEnableLazyLoading = false)
        {
            _context.Configuration.LazyLoadingEnabled = bEnableLazyLoading;
            return _context.Set<T_Entitie>().ToList();
        }

        /// <summary>
        /// Get all entities from the database which valid an expression
        /// </summary>
        /// <param name="expression">Expression to validate</param>
        /// <param name="bEnableLazyLoading">Determines whether objects accessible by navigation property are loaded automatically</param>
        public IList<T_Entitie> Get(Expression<Func<T_Entitie, bool>> expression, bool bEnableLazyLoading = false)
        {
            _context.Configuration.LazyLoadingEnabled = bEnableLazyLoading;
            return _context.Set<T_Entitie>().Where(expression).ToList();
        }

        /// <summary>
        /// Count entities in the database
        /// </summary>
        public int Count()
        {
            return _context.Set<T_Entitie>().Count();
        }

        /// <summary>
        /// Count entities in the database which valid an expression
        /// </summary>
        /// <param name="expression">Expression to validate</param>
        public int Count(Expression<Func<T_Entitie, bool>> expression)
        {
            return _context.Set<T_Entitie>().Where(expression).Count();
        }

        /// <summary>
        /// Add an entity in the database
        /// </summary>
        public void Add(T_Entitie entity)
        {
            _context.Set<T_Entitie>().Add(entity);
        }

        /// <summary>
        /// Remove an entity in the database
        /// </summary>
        public void Remove(T_Entitie entity)
        {
            _context.Set<T_Entitie>().Remove(entity);
        }

        /// <summary>
        /// Edit an entity in the database
        /// </summary>
        public void Edit(T_Entitie entity)
        {
            var entry = _context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                _context.Set<T_Entitie>().Attach(entity);
                entry = _context.Entry(entity);
            }
            entry.State = EntityState.Modified;
        }

        /// <summary>
        /// Save changes in the database
        /// </summary>
        public int SaveChanges()
        {
            try
            {
                return SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Log.Error(string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                                            eve.Entry.Entity.GetType().Name, eve.Entry.State));

                    foreach (var ve in eve.ValidationErrors)
                        Log.Error(string.Format("-> Property: \"{0}\", Error: \"{1}\"",
                                                ve.PropertyName, ve.ErrorMessage));
                }
                throw;
            }
        }

        #endregion Methods
    }
}