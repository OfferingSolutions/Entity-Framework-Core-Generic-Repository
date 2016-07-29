using System;
using System.Threading.Tasks;
using OfferingSolutions.UoWCore.RepositoryBase;

namespace OfferingSolutions.UoWCore.UnitOfWork
{
    internal interface IOsUnitOfWork : IDisposable
    {
        IRepositoryBase<T> GetRepository<T>() where T : class;

        int Save();

        Task<int> SaveASync();
    }
}