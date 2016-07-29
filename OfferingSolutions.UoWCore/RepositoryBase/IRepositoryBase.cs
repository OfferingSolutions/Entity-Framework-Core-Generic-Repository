using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OfferingSolutions.UoWCore.RepositoryBase
{
    internal interface IRepositoryBase<T> where T : class
    {
        IQueryable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<Expression<Func<T, object>>> includes = null);

        Task<IQueryable<T>> GetAllASync(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<Expression<Func<T, object>>> includes = null);

        T GetSingleBy(Expression<Func<T, bool>> predicate);
        Task<T> GetSingleByASync(Expression<Func<T, bool>> predicate);
        T FindBy(Expression<Func<T, bool>> wherePredicate, List<Expression<Func<T, object>>> includes = null);
        Task<T> FindByASync(Expression<Func<T, bool>> wherePredicate, List<Expression<Func<T, object>>> includes = null);
        void Add(T toAdd);
        void Update(T toUpdate);
        void Delete(T entity);
    }
}