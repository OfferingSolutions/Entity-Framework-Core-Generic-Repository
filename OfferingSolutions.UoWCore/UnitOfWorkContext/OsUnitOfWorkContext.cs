using Microsoft.EntityFrameworkCore;
using OfferingSolutions.UoWCore.ContextBase;

namespace OfferingSolutions.UoWCore.UnitOfWorkContext
{
    public class OsUnitOfWorkContext : ContextBaseImpl, IOsUnitOfWorkContext
    {
        public OsUnitOfWorkContext(DbContext databaseContext)
            : base(databaseContext)
        {

        }
    }
}