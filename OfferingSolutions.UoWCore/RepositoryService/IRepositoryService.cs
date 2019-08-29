using System;
using Microsoft.EntityFrameworkCore;
using OfferingSolutions.UoWCore.RepositoryBase;

namespace OfferingSolutions.UoWCore.RepositoryService
{
    internal interface IRepositoryService
    {
        DbContext DbContext { get; set; }

        IRepositoryBase<T> GetGenericRepository<T>() where T : class;

        T GetCustomRepository<T>(Func<DbContext, object> factory = null) where T : class;
    }
}