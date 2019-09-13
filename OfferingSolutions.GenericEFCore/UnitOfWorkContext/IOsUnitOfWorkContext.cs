using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OfferingSolutions.GenericEFCore.UnitOfWorkContext
{
    public interface IOsUnitOfWorkContext : IDisposable
    {
        void Add<T>(T entity) where T : class;
        void AddAsync<T>(T entity) where T : class;
        int Count<T>() where T : class;
        int Count<T>(Expression<Func<T, bool>> predicate) where T : class;
        void Delete<T>(Expression<Func<T, bool>> predicate) where T : class;
        void Delete<T>(T toDelete) where T : class;

        IQueryable<T> GetAll<T>() where T : class;
        IQueryable<T> GetAll<T>(Expression<Func<T, bool>> predicate) where T : class;
        IQueryable<T> GetAll<T>(Func<IQueryable<T>, IIncludableQueryable<T, object>> include) where T : class;

        IQueryable<T> GetAll<T>(
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, 
            int? skip = null, int? take = null) where T : class;

        IQueryable<T> GetAll<T>(
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            string orderBy = null, 
            string orderDirection = "asc", 
            int? skip = null, int? take = null) where T : class;

        Task<IQueryable<T>> GetAllAsync<T>(
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, 
            int? skip = null, int? take = null) where T : class;

        Task<IQueryable<T>> GetAllAsync<T>(
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            string orderBy = null, 
            string orderDirection = "asc", 
            int? skip = null, int? take = null) where T : class;

        Task<IQueryable<T>> GetAllAsync<T>() where T : class;
        Task<IQueryable<T>> GetAllAsync<T>(Expression<Func<T, bool>> predicate) where T : class;
        Task<IQueryable<T>> GetAllAsync<T>(Func<IQueryable<T>, IIncludableQueryable<T, object>> include) where T : class;

        T GetSingle<T>(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null) where T : class;
        Task<T> GetSingleAsync<T>(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null) where T : class;
        int Save();
        Task<int> SaveASync();
        T Update<T>(T entity) where T : class;
    }
}
