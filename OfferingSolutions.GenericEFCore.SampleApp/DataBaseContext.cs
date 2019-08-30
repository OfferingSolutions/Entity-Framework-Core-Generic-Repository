using Microsoft.EntityFrameworkCore;
using OfferingSolutions.GenericEFCore.SampleApp.Models;

namespace OfferingSolutions.GenericEFCore.SampleApp
{
    public class DataBaseContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Thing> Things { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=GenericEFCore;Trusted_Connection=True;");
        }
    }
}
