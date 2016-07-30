using OfferingSolutions.UoWCore.RepositoryContext;
using OfferingSolutions.UoWCore.SampleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApp.ExampleRepositories
{
    interface IPersonRepository : IRepositoryContext<Person>
    {
        void MyNewFunction(int id);
    }
}
