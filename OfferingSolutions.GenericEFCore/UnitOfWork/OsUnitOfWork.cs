using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OfferingSolutions.GenericEFCore.RepositoryBase;
using OfferingSolutions.GenericEFCore.Services;

namespace OfferingSolutions.GenericEFCore.UnitOfWork
{
    internal class OsUnitOfWork : IOsUnitOfWork
    {
        private readonly DbContext _dbContext;
        private readonly IRepositoryService _repositoryService;

        public OsUnitOfWork(DbContext dbContext)
        {
            _dbContext = dbContext;

            if (_repositoryService == null)
            {
                _repositoryService = new RepositoryServiceImpl();
            }

            _repositoryService.DbContext = _dbContext;
        }

        int IOsUnitOfWork.Save()
        {
            return _dbContext.SaveChanges();
        }

        Task<int> IOsUnitOfWork.SaveAsync()
        {
            return _dbContext.SaveChangesAsync();
        }

        void IDisposable.Dispose()
        {
            _dbContext.Dispose();
        }

        IGenericRepositoryBase<T> IOsUnitOfWork.GetRepository<T>()
        {
            return _repositoryService.GetGenericRepository<T>();
        }
    }
}