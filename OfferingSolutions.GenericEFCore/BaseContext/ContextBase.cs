using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using OfferingSolutions.GenericEFCore.UnitOfWork;

namespace OfferingSolutions.GenericEFCore.BaseContext
{
    public class ContextBase
    {
        private readonly IOsUnitOfWork _osUnitOfWork;

        public ContextBase(DbContext databaseContext)
        {
            _osUnitOfWork = new OsUnitOfWork(databaseContext);
        }

        public int Save()
        {
            return _osUnitOfWork.Save();
        }

        public Task<int> SaveASync()
        {
            return _osUnitOfWork.SaveAsync();
        }

        public void Dispose()
        {
            _osUnitOfWork.Dispose();
        }

        public int Count<T>() where T : class
        {
            return _osUnitOfWork.GetRepository<T>().Count();
        }


        public IQueryable<T> GetAll<T>() where T : class
        {
            return _osUnitOfWork.GetRepository<T>().GetAll(null, null, null, null, null);
        }

        public IQueryable<T> GetAll<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return _osUnitOfWork.GetRepository<T>().GetAll(predicate, null, null, null, null);
        }

        public IQueryable<T> GetAll<T>(Expression<Func<T, bool>> predicate = null,
         Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
         Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
         int? skip = null, int? take = null) where T : class
        {
            return _osUnitOfWork.GetRepository<T>().GetAll(predicate, include, orderBy, skip, take);
        }

        public IQueryable<T> GetAll<T>(Expression<Func<T, bool>> predicate = null,
         Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
        string orderBy = null, string orderDirection = "asc",
         int? skip = null, int? take = null) where T : class
        {
            return _osUnitOfWork.GetRepository<T>().GetAll(predicate, include, orderBy, orderDirection, skip, take);
        }

        public Task<IQueryable<T>> GetAllAsync<T>(Expression<Func<T, bool>> predicate = null,
         Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
         Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
         int? skip = null, int? take = null) where T : class
        {
            return _osUnitOfWork.GetRepository<T>().GetAllAsync(predicate, include, orderBy, skip, take);
        }

        public Task<IQueryable<T>> GetAllAsync<T>(Expression<Func<T, bool>> predicate = null,
         Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
        string orderBy = null, string orderDirection = "asc",
         int? skip = null, int? take = null) where T : class
        {
            return _osUnitOfWork.GetRepository<T>().GetAllAsync(predicate, include, orderBy, orderDirection, skip, take);
        }

        public Task<IQueryable<T>> GetAllAsync<T>() where T : class
        {
            return _osUnitOfWork.GetRepository<T>().GetAllAsync(null, null, null, null, null);
        }

        public Task<IQueryable<T>> GetAllAsync<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return _osUnitOfWork.GetRepository<T>().GetAllAsync(predicate, null, null, null, null);
        }

        public IQueryable<T> GetAll<T>(Func<IQueryable<T>, IIncludableQueryable<T, object>> include) where T : class
        {
            return _osUnitOfWork.GetRepository<T>().GetAll(null, include, null,  null, null);
        }

        public Task<IQueryable<T>> GetAllAsync<T>(Func<IQueryable<T>, IIncludableQueryable<T, object>> include) where T : class
        {
            return _osUnitOfWork.GetRepository<T>().GetAllAsync(null, include, null,  null, null);
        }

        public T GetSingle<T>(
         Expression<Func<T, bool>> predicate = null,
         Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null) where T : class
        {
            return _osUnitOfWork.GetRepository<T>().GetSingle(predicate, include);
        }

        public Task<T> GetSingleAsync<T>(
          Expression<Func<T, bool>> predicate = null,
          Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null) where T : class
        {
            return _osUnitOfWork.GetRepository<T>().GetSingleAsync(predicate, include);
        }

        public virtual void Add<T>(T entity) where T : class
        {
            _osUnitOfWork.GetRepository<T>().Add(entity);
        }

        public virtual void AddAsync<T>(T entity) where T : class
        {
            _osUnitOfWork.GetRepository<T>().AddAsync(entity);
        }

        public T Update<T>(T entity) where T : class
        {
            return _osUnitOfWork.GetRepository<T>().Update(entity);
        }

        public void Delete<T>(T toDelete) where T : class
        {
            _osUnitOfWork.GetRepository<T>().Delete(toDelete);
        }

        public void Delete<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            _osUnitOfWork.GetRepository<T>().Delete(predicate);
        }
    }
}
