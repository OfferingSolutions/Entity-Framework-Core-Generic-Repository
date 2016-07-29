using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OfferingSolutions.UoWCore.UnitOfWorkContext
{
    public interface IOsUnitOfWorkContext : IDisposable
    {
        IQueryable<T> GetAll<T>(Expression<Func<T, bool>> filter = null,
           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
           List<Expression<Func<T, object>>> includes = null) where T : class;

        Task<IQueryable<T>> GetAllASync<T>(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            List<Expression<Func<T, object>>> includes = null) where T : class;

        T GetSingle<T>(Expression<Func<T, bool>> predicate, List<Expression<Func<T, object>>> includes = null) where T : class;

        Task<T> GetSingleASync<T>(Expression<Func<T, bool>> predicate, List<Expression<Func<T, object>>> includes = null) where T : class;

        T GetSingleBy<T>(Expression<Func<T, bool>> predicate) where T : class;

        Task<T> GetSingleByASync<T>(Expression<Func<T, bool>> predicate) where T : class;

        void Add<T>(T toAdd) where T : class;

        void Update<T>(T toUpdate) where T : class;

        void Delete<T>(T toDelete) where T : class;

        int Save();

        Task<int> SaveASync();
    }
}
