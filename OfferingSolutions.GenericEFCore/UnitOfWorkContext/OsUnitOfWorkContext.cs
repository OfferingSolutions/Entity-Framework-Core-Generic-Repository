using Microsoft.EntityFrameworkCore;
using OfferingSolutions.GenericEFCore.BaseContext;

namespace OfferingSolutions.GenericEFCore.UnitOfWorkContext
{
    public class OsUnitOfWorkContext : ContextBase, IOsUnitOfWorkContext
    {
        public OsUnitOfWorkContext(DbContext databaseContext)
            : base(databaseContext)
        {

        }
    }
}