using Microsoft.EntityFrameworkCore;
using OfferingSolutions.GenericEFCore.SampleApp.Models;

namespace OfferingSolutions.GenericEFCore.Tests
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext()
        { }

        public DataBaseContext(DbContextOptions<DataBaseContext> options)
            : base(options)
        { }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Thing> Things { get; set; }
    }
}
