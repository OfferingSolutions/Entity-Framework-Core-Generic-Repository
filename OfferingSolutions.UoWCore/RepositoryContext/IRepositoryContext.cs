using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OfferingSolutions.UoWCore.RepositoryContext
{
    public interface IRepositoryContext<T> : IDisposable where T : class
    {
        IQueryable<T> GetAll(Expression<Func<T, bool>> predicate = null,
           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
           Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
           int? skip = null, int? take = null);

        IQueryable<T> GetAll(Expression<Func<T, bool>> predicate = null,
        string orderBy = null, string orderDirection = "asc",
         Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
         int? skip = null, int? take = null);

        IQueryable<T> GetAll();
        IQueryable<T> GetAll(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAll(Func<IQueryable<T>, IIncludableQueryable<T, object>> include);

        Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
        int? skip = null, int? take = null);

        Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null,
        string orderBy = null, string orderDirection = "asc",
         Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
         int? skip = null, int? take = null);

        Task<IQueryable<T>> GetAllAsync();
        Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> predicate);
        Task<IQueryable<T>> GetAllAsync(Func<IQueryable<T>, IIncludableQueryable<T, object>> include);

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

        int Save();

        Task<int> SaveASync();
    }
}