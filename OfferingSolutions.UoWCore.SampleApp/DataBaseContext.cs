using Microsoft.EntityFrameworkCore;
using OfferingSolutions.UoWCore.SampleApp.Models;

namespace OfferingSolutions.UoWCore.SampleApp
{
    public class DataBaseContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Thing> Things { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=UoWCore;Trusted_Connection=True;");
        }
    }
}
