using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OfferingSolutions.UoWCore.ContextBase;

namespace OfferingSolutions.UoWCore.RepositoryContext
{
    public class RepositoryContextImpl<T> : ContextBaseImpl, IRepositoryContext<T> where T : class
    {
        public RepositoryContextImpl(DbContext databaseContext)
            : base(databaseContext)
        {

        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<Expression<Func<T, object>>> includes = null)
        {
            return base.GetAll(filter, orderBy, includes);
        }

        public Task<IQueryable<T>> GetAllASync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<Expression<Func<T, object>>> includes = null)
        {
            return base.GetAllASync(filter, orderBy, includes);
        }

        public virtual T GetSingleBy(Expression<Func<T, bool>> predicate)
        {
            return base.GetSingleBy<T>(predicate);
        }

        public Task<T> GetSingleByASync(Expression<Func<T, bool>> predicate)
        {
            return base.GetSingleByASync<T>(predicate);
        }

        public Task<T> GetSingleASync(Expression<Func<T, bool>> predicate, List<Expression<Func<T, object>>> includes = null)
        {
            return base.GetSingleASync(predicate, includes);
        }

        public virtual T GetSingle(Expression<Func<T, bool>> predicate, List<Expression<Func<T, object>>> includes = null)
        {
            return base.GetSingle(predicate, includes);
        }

        public virtual void Add(T toAdd)
        {
            base.Add(toAdd);
        }

        public virtual void Update(T toUpdate)
        {
            base.Update(toUpdate);
        }

        public virtual void Delete(T entity)
        {
            base.Delete<T>(entity);
        }
    }
}
