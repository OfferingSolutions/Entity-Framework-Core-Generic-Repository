using System;
using System.Threading.Tasks;
using OfferingSolutions.GenericEFCore.RepositoryBase;

namespace OfferingSolutions.GenericEFCore.UnitOfWork
{
    internal interface IOsUnitOfWork : IDisposable
    {
        IGenericRepositoryBase<T> GetRepository<T>() where T : class;

        int Save();

        Task<int> SaveAsync();
    }
}