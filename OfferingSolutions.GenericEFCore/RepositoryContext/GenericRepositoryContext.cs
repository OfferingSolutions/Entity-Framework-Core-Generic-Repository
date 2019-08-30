using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using OfferingSolutions.GenericEFCore.BaseContext;

namespace OfferingSolutions.GenericEFCore.RepositoryContext
{
    public class GenericRepositoryContext<T> : ContextBase, IGenericRepositoryContext<T> where T : class
    {
        public GenericRepositoryContext(DbContext databaseContext)
            : base(databaseContext)
        {

        }

        public virtual void Add(T entity)
        {
            base.Add(entity);
        }

        public virtual void AddAsync(T entity)
        {
            base.AddAsync(entity);
        }

        public virtual void Delete(T entity)
        {
            base.Delete(entity);
        }

        public virtual IQueryable<T> GetAll()
        {
            return base.GetAll<T>(null, null, null, null, null);
        }

        public virtual IQueryable<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            return base.GetAll<T>(predicate, null, null, null, null);
        }
        public virtual Task<IQueryable<T>> GetAllAsync()
        {
            return base.GetAllAsync<T>(null, null, null, null, null);
        }

        public virtual Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> predicate)
        {
            return base.GetAllAsync(predicate, null, null, null, null);
        }

        public virtual void Delete(Expression<Func<T, bool>> predicate)
        {
            base.Delete(predicate);
        }

        public virtual IQueryable<T> GetAll(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, int? skip = null, int? take = null)
        {
            return base.GetAll(predicate, orderBy, include, skip, take);
        }

        public virtual IQueryable<T> GetAll(Expression<Func<T, bool>> predicate = null, string orderBy = null, string orderDirection = "asc", Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, int? skip = null, int? take = null)
        {
            return base.GetAll(predicate, orderBy, orderDirection, include, skip, take);
        }

        public virtual Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, int? skip = null, int? take = null)
        {
            return base.GetAllAsync(predicate, orderBy, include, skip, take);
        }

        public virtual Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, string orderBy = null, string orderDirection = "asc", Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, int? skip = null, int? take = null)
        {
            return base.GetAllAsync(predicate, orderBy, orderDirection, include, skip, take);
        }

        public virtual T GetSingle(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            return base.GetSingle(predicate, include);
        }

        public virtual Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            return base.GetSingleAsync(predicate, include);
        }

        public virtual T Update(T toUpdate)
        {
            return base.Update(toUpdate);
        }

        public virtual int Count()
        {
            return base.Count<T>();
        }

        public virtual IQueryable<T> GetAll(Func<IQueryable<T>, IIncludableQueryable<T, object>> include)
        {
            return base.GetAll(include);
        }

        public virtual Task<IQueryable<T>> GetAllAsync(Func<IQueryable<T>, IIncludableQueryable<T, object>> include)
        {
            return base.GetAllAsync(include);
        }
    }
}
