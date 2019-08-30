using OfferingSolutions.GenericEFCore.RepositoryContext;
using OfferingSolutions.GenericEFCore.SampleApp.Models;

namespace SampleApp.ExampleRepositories
{
    interface IPersonRepository : IGenericRepositoryContext<Person>
    {
        void MyNewFunction(int id);
    }
}
