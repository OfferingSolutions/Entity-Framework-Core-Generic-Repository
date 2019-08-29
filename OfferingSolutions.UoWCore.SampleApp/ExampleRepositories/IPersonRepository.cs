using OfferingSolutions.UoWCore.RepositoryContext;
using OfferingSolutions.UoWCore.SampleApp.Models;

namespace SampleApp.ExampleRepositories
{
    interface IPersonRepository : IRepositoryContext<Person>
    {
        void MyNewFunction(int id);
    }
}
