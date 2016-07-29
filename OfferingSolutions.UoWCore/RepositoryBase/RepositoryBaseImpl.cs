using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace OfferingSolutions.UoWCore.RepositoryBase
{
    internal class RepositoryBaseImpl<T> : IRepositoryBase<T> where T : class
    {
        private readonly DbContext _dataBaseContext;

        public RepositoryBaseImpl(DbContext context)
        {
            _dataBaseContext = context;
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> filter = null, 
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, 
            List<Expression<Func<T, object>>> includes = null)
        {
            IQueryable<T> query = GetQueryable(filter, includes);

            if (orderBy != null)
            {
                return orderBy(query);
            }

            return query;
        }

        public Task<IQueryable<T>> GetAllASync(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<Expression<Func<T, object>>> includes = null)
        {
            IQueryable<T> query = GetQueryable(filter, includes);

            if (orderBy != null)
            {
                return new Task<IQueryable<T>>(() => orderBy(query));
            }

            return new Task<IQueryable<T>>(() => query);
        }

        /// <summary>
        /// Returns a single instance of T but throws exception if none is found
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual T GetSingleBy(Expression<Func<T, bool>> predicate)
        {
            return _dataBaseContext.Set<T>().Single(predicate);
        }

        public virtual Task<T> GetSingleByASync(Expression<Func<T, bool>> predicate)
        {
            return _dataBaseContext.Set<T>().SingleAsync(predicate);
        }

        public virtual T FindBy(Expression<Func<T, bool>> wherePredicate, List<Expression<Func<T, object>>> includes = null)
        {
            IQueryable<T> query = _dataBaseContext.Set<T>();
            if (includes != null)
            {
                foreach (var includeProperty in includes)
                {
                    query = query.Include(includeProperty);
                }
            }
            return query.Where(wherePredicate).FirstOrDefault();
        }

        public virtual Task<T> FindByASync(Expression<Func<T, bool>> wherePredicate, List<Expression<Func<T, object>>> includes = null)
        {
            IQueryable<T> query = _dataBaseContext.Set<T>();
            if (includes != null)
            {
                foreach (var includeProperty in includes)
                {
                    query = query.Include(includeProperty);
                }
            }
            return query.Where(wherePredicate).FirstOrDefaultAsync();
        }

        public virtual void Add(T toAdd)
        {
            _dataBaseContext.Set<T>().Add(toAdd);
        }

        public virtual void Update(T toUpdate)
        {
            _dataBaseContext.Entry(toUpdate).State = EntityState.Modified;
        }
        
        public virtual void Delete(T entity)
        {
            _dataBaseContext.Set<T>().Remove(entity);
        }

        private IQueryable<T> GetQueryable(Expression<Func<T, bool>> filter, List<Expression<Func<T, object>>> includes)
        {
            IQueryable<T> query = _dataBaseContext.Set<T>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if(includes != null)
            {
                foreach (var includeProperty in includes)
                {
                    query = query.Include(includeProperty);
                }
            }

            return query;
        }
    }
}