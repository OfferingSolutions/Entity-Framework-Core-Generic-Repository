using OfferingSolutions.UoWCore.RepositoryContext;
using OfferingSolutions.UoWCore.SampleApp;
using OfferingSolutions.UoWCore.SampleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApp.ExampleRepositories
{
    public class PersonRepository : RepositoryContextImpl<Person>, IPersonRepository
    {
        public PersonRepository(DataBaseContext dbContext)
            : base(dbContext)
        {

        }

        public void MyNewFunction(int id)
        {
            //Do Something
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
