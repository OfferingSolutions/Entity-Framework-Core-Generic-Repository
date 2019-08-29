using Microsoft.EntityFrameworkCore;
using OfferingSolutions.UoWCore.BaseContext;

namespace OfferingSolutions.UoWCore.UnitOfWorkContext
{
    public class OsUnitOfWorkContext : ContextBase, IOsUnitOfWorkContext
    {
        public OsUnitOfWorkContext(DbContext databaseContext)
            : base(databaseContext)
        {

        }
    }
}