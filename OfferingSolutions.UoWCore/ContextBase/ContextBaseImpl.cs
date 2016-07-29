using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OfferingSolutions.UoWCore.UnitOfWork;

namespace OfferingSolutions.UoWCore.ContextBase
{
    public class ContextBaseImpl
    {
        private readonly IOsUnitOfWork _osUnitOfWork;

        public ContextBaseImpl(DbContext databaseContext)
        {
            _osUnitOfWork = new OsUnitOfWork(databaseContext);
        }

        public int Save()
        {
            return _osUnitOfWork.Save();
        }

        public Task<int> SaveASync()
        {
            return _osUnitOfWork.SaveASync();
        }

        public void Dispose()
        {
            _osUnitOfWork.Dispose();
        }

        public void Add<T>(T toAdd) where T : class
        {
            _osUnitOfWork.GetRepository<T>().Add(toAdd);
        }

        public void Update<T>(T toUpdate) where T : class
        {
            _osUnitOfWork.GetRepository<T>().Update(toUpdate);
        }

        public void Delete<T>(T toDelete) where T : class
        {
            _osUnitOfWork.GetRepository<T>().Delete(toDelete);
        }

        public T GetSingle<T>(Expression<Func<T, bool>> predicate, List<Expression<Func<T, object>>> includes = null) where T : class
        {
            return _osUnitOfWork.GetRepository<T>().FindBy(predicate, includes);
        }

        public Task<T> GetSingleASync<T>(Expression<Func<T, bool>> predicate, List<Expression<Func<T, object>>> includes = null) where T : class
        {
            return _osUnitOfWork.GetRepository<T>().FindByASync(predicate, includes);
        }

        public T GetSingleBy<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return _osUnitOfWork.GetRepository<T>().GetSingleBy(predicate);
        }

        public Task<T> GetSingleByASync<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return _osUnitOfWork.GetRepository<T>().GetSingleByASync(predicate);
        }

        public IQueryable<T> GetAll<T>(Expression<Func<T, bool>> filter = null,
           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
           List<Expression<Func<T, object>>> includes = null) where T : class
        {
            return _osUnitOfWork.GetRepository<T>().GetAll(filter, orderBy, includes);
        }

        public Task<IQueryable<T>> GetAllASync<T>(Expression<Func<T, bool>> filter = null,
           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
           List<Expression<Func<T, object>>> includes = null) where T : class
        {
            return _osUnitOfWork.GetRepository<T>().GetAllASync(filter, orderBy, includes);
        }
    }
}
