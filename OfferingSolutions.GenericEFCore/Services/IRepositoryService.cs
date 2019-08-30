using System;
using Microsoft.EntityFrameworkCore;
using OfferingSolutions.GenericEFCore.RepositoryBase;

namespace OfferingSolutions.GenericEFCore.Services
{
    internal interface IRepositoryService
    {
        DbContext DbContext { get; set; }

        IGenericRepositoryBase<T> GetGenericRepository<T>() where T : class;

        T GetCustomRepository<T>(Func<DbContext, object> factory = null) where T : class;
    }
}