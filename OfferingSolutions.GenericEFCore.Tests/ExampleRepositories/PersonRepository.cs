using OfferingSolutions.GenericEFCore.RepositoryContext;
using OfferingSolutions.GenericEFCore.SampleApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace OfferingSolutions.GenericEFCore.Tests.ExampleRepositories
{
    public class PersonRepository : GenericRepositoryContext<Person>, IPersonRepository
    {
        public PersonRepository(DataBaseContext dbContext)
            : base(dbContext)
        {

        }

        public void MyNewFunction(int id)
        {
            //Do Something
        }

        public new List<Person> GetAll()
        {
            return base.GetAll().ToList();
        }

        public override void Add(Person toAdd)
        {
            MyAdditionalAddFunction();
            base.Add(toAdd);
        }

        private void MyAdditionalAddFunction()
        {
            //Do something else...
        }
    }
}
