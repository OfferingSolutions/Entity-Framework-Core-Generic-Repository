using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OfferingSolutions.GenericEFCore.RepositoryBase
{
    internal interface IGenericRepositoryBase<T> where T : class
    {
        IQueryable<T> GetAll(Expression<Func<T, bool>> predicate = null,
           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
           Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
           int? skip = null, int? take = null);

        IQueryable<T> GetAll(Expression<Func<T, bool>> predicate = null,
        string orderBy = null, string orderDirection = "asc",
         Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
         int? skip = null, int? take = null);

        Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
        int? skip = null, int? take = null);

        Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null,
        string orderBy = null, string orderDirection = "asc",
         Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
         int? skip = null, int? take = null);

        T GetSingle(
          Expression<Func<T, bool>> predicate = null,
          Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);

        Task<T> GetSingleAsync(
          Expression<Func<T, bool>> predicate = null,
          Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);

        void Add(T toAdd);
        void AddAsync(T entity);
        T Update(T toUpdate);
        void Delete(T entity);
        void Delete(Expression<Func<T, bool>> predicate);

        int Count();
    }
}