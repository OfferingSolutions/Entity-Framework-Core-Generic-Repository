using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Dynamic.Core;

namespace OfferingSolutions.GenericEFCore.RepositoryBase
{
    internal class GenericRepositoryBase<T> : IGenericRepositoryBase<T> where T : class
    {
        private readonly DbContext _dataBaseContext;

        public GenericRepositoryBase(DbContext context)
        {
            _dataBaseContext = context;
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate = null,
          Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
          Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
          int? skip = null, int? take = null)
        {
            IQueryable<T> query = GetQueryable(predicate, include);

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (skip != null && skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (take != null && take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query;

        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate = null,
        string orderBy = null, string orderDirection = "asc",
         Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
         int? skip = null, int? take = null)
        {
            IQueryable<T> query = GetQueryable(predicate, include);

            if (orderBy != null)
            {
                query = query.OrderBy(orderBy, orderDirection);
            }

            if (skip != null && skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (take != null && take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query;
        }

        public Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null,
         Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
         Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
         int? skip = null, int? take = null)
        {
            IQueryable<T> query = GetQueryable(predicate, include);

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (skip != null && skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (take != null && take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return new Task<IQueryable<T>>(() => query);

        }

        public Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null,
        string orderBy = null, string orderDirection = "asc",
         Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
         int? skip = null, int? take = null)
        {
            IQueryable<T> query = GetQueryable(predicate, include);

            if (orderBy != null)
            {
                query = query.OrderBy(orderBy, orderDirection);
            }

            if (skip != null && skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (take != null && take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return new Task<IQueryable<T>>(() => query);
        }

        /// <summary>
        /// Returns a single instance of T but throws exception if none is found
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
         public T GetSingle(
          Expression<Func<T, bool>> predicate = null,
          Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = GetQueryable(predicate, include);

            return query.FirstOrDefault();
        }

        public Task<T> GetSingleAsync(
          Expression<Func<T, bool>> predicate = null,
          Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = GetQueryable(predicate, include);

            return query.FirstOrDefaultAsync();
        }

        public virtual void Add(T entity)
        {
            _dataBaseContext.Set<T>().Add(entity);
        }

        public virtual void AddAsync(T entity)
        {
            _dataBaseContext.Set<T>().AddAsync(entity);
        }

        public T Update(T entity)
        {
            _dataBaseContext.Set<T>().Update(entity);
            return entity;
        }

        public void Delete(Expression<Func<T, bool>> predicate)
        {
            var entity = GetSingle(predicate: predicate);
            _dataBaseContext.Set<T>().Remove(entity);
        }

        public void Delete(T entity)
        {
            _dataBaseContext.Set<T>().Remove(entity);
        }

        public int Count()
        {
            return _dataBaseContext.Set<T>().Count();
        }

        private IQueryable<T> GetQueryable(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = _dataBaseContext.Set<T>();

            if (include != null)
            {
                query = include(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return query;
        }
    }
}