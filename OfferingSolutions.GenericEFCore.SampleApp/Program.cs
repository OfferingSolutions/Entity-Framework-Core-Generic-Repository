using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using OfferingSolutions.GenericEFCore.SampleApp.Models;
using OfferingSolutions.GenericEFCore.UnitOfWorkContext;
using SampleApp.ExampleRepositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OfferingSolutions.GenericEFCore.SampleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                using (IOsUnitOfWorkContext unitOfWorkContext = new OsUnitOfWorkContext(new DataBaseContext()))
                {
                    Person person = new Person()
                    {
                        Age = 35,
                        Name = "Fabian",
                        Things = new List<Thing>() {
                        new Thing()
                        {
                            Name = "A Thing"
                        }
                    }
                    };

                    // Adding a new Entity, for example "Person"
                    unitOfWorkContext.Add(person);

                    // Savechanges
                    unitOfWorkContext.Save();

                    unitOfWorkContext.SaveASync();

                    // Get all Persons
                    var persons = unitOfWorkContext.GetAll<Person>().ToList();
                    var personsAsync = unitOfWorkContext.GetAllAsync<Person>();

                    foreach (var item in persons)
                    {
                        Console.WriteLine(item.Name + " " + item.Age);
                    }

                    List<Person> allPersonsOnAge35 = unitOfWorkContext.GetAll<Person>(x => x.Age == 35).ToList();

                    Console.WriteLine(allPersonsOnAge35.Count);

                    // Get all Persons with the age of 35 ordered by Name
                    List<Person> allPersonsOnAge35Ordered = unitOfWorkContext.GetAll<Person>(x => x.Age == 35, orderBy: q => q.OrderBy(d => d.Name)).ToList();

                    // Get all Persons with the age of 35 ordered by Name and include its properties
                    Func<IQueryable<Person>, IIncludableQueryable<Person, object>> include = source => source.Include(y => y.Things);

                    List<Person> allPersonsOnAge35OrderedAndWithThings = unitOfWorkContext.GetAll<Person>(
                        x => x.Age == 35,
                        orderBy: q => q.OrderBy(d => d.Name),
                        include: include)
                        .ToList();

                    // Get all Persons and include its properties
                    List<Person> allPersonsWithThings = unitOfWorkContext.GetAll<Person>(include: include).ToList();

                    // Find a single Person with a specific name, is null if not found
                    Person findBy = unitOfWorkContext.GetSingle<Person>(x => x.Id == 6);
                    var findByASync = unitOfWorkContext.GetSingleAsync<Person>(x => x.Id == 6);

                    // Find a single Person with a specific name and include its siblings
                    Person findByWithThings = unitOfWorkContext.GetSingle<Person>(x => x.Name == "Fabian", include: include);

                    // Find a person by id but throws exception if not found
                    // var personWithIdMayThrowException = unitOfWorkContext.GetSingleBy<Person>(x => x.Id == 6);
                    // var personWithIdMayThrowExceptionASync = unitOfWorkContext.GetSingleByASync<Person>(x => x.Id == 6);

                    // Update an existing person
                    Person findOneToUpdate = unitOfWorkContext.GetSingle<Person>(x => x.Name == "Fabian");
                    findOneToUpdate.Name = "Fabian2";

                    unitOfWorkContext.Update(person);
                    unitOfWorkContext.Save();

                    Person findOneAfterUpdate = unitOfWorkContext.GetSingle<Person>(x => x.Name == "Fabian2");

                    // Deleting a Person by Id or by entity
                    unitOfWorkContext.Delete(person);
                }

                ///////////////////////////////////////////////////////////////////
                // Custom repositories
                ///////////////////////////////////////////////////////////////////

                using (IPersonRepository personRepository = new PersonRepository(new DataBaseContext()))
                {
                    personRepository.Add(new Person() { Name = "John Doe" });
                    personRepository.Save();
                    var receivedPerson = personRepository.GetSingle(x => x.Name == "John Doe", source => source.Include(y => y.Things));
                    var allPersons = personRepository.GetAll(x => x.Name == "John Doe");
                    var allPersonsWithThings = personRepository.GetAll(x => x.Name == "John Doe", source => source.Include(y => y.Things), q => q.OrderBy(d => d.Name));
                    Console.WriteLine(receivedPerson);

                    // Does something good ...
                    personRepository.MyNewFunction(6);
                }

                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}
