using OfferingSolutions.GenericEFCore.RepositoryContext;
using OfferingSolutions.GenericEFCore.SampleApp.Models;

namespace OfferingSolutions.GenericEFCore.Tests.ExampleRepositories
{
    interface IPersonRepository : IGenericRepositoryContext<Person>
    {
        void MyNewFunction(int id);
    }
}
