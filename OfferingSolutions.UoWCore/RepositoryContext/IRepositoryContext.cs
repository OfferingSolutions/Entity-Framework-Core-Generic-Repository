using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OfferingSolutions.UoWCore.RepositoryContext
{
    public interface IRepositoryContext<T> : IDisposable where T : class
    {
        IQueryable<T> GetAll(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            List<Expression<Func<T, object>>> includes = null);

        Task<IQueryable<T>> GetAllASync(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            List<Expression<Func<T, object>>> includes = null);

        T GetSingleBy(Expression<Func<T, bool>> predicate);

        Task<T> GetSingleByASync(Expression<Func<T, bool>> predicate);

        T GetSingle(Expression<Func<T, bool>> predicate, List<Expression<Func<T, object>>> includes = null);

        Task<T> GetSingleASync(Expression<Func<T, bool>> predicate, List<Expression<Func<T, object>>> includes = null);

        void Add(T toAdd);

        void Update(T toUpdate);

        void Delete(T entity);

        int Save();

        Task<int> SaveASync();
    }
}