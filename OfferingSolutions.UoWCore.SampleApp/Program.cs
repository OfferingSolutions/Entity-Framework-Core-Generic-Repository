using Microsoft.EntityFrameworkCore;
using OfferingSolutions.UoWCore.UnitOfWorkContext;
using SampleApp;
using SampleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace OfferingSolutions.UoWCore.SampleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                using (IOsUnitOfWorkContext unitOfWorkContext = new OsUnitOfWorkContext(new DataBaseContext()))
                {
                    Person person = new Person() { Age = 35, Name = "Fabian", Things = new List<Thing>() {
                        new Thing()
                        {
                            Name = "A Thing"
                        }
                    } };

                    //Adding a new Entity, for example "Person"
                    unitOfWorkContext.Add(person);

                    //Savechanges
                    unitOfWorkContext.Save();

                    unitOfWorkContext.SaveASync();

                    // Get all Persons
                    var persons = unitOfWorkContext.GetAll<Person>().ToList();
                    var personsAsync = unitOfWorkContext.GetAllASync<Person>();

                    foreach (var item in persons)
                    {
                        Console.WriteLine(item.Name + " " + item.Age);
                    }

                    List<Person> allPersonsOnAge35 = unitOfWorkContext.GetAll<Person>(x => x.Age == 35).ToList();

                    Console.WriteLine(allPersonsOnAge35.Count);

                    // Get all Persons with the age of 35 ordered by Name
                    List<Person> allPersonsOnAge35Ordered = unitOfWorkContext.GetAll<Person>(x => x.Age == 35, orderBy: q => q.OrderBy(d => d.Name)).ToList();

                    // Get all Persons with the age of 35 ordered by Name and include its properties
                    var incl = new List<Expression<Func<Person, object>>>() { p => p.Things };
                    List<Person> allPersonsOnAge35OrderedAndWithThings = unitOfWorkContext.GetAll<Person>(
                        x => x.Age == 35,
                        orderBy: q => q.OrderBy(d => d.Name),
                        includes: incl).ToList();

                    // Get all Persons and include its properties
                    List<Person> allPersonsWithThings = unitOfWorkContext.GetAll<Person>(includes: incl).ToList();

                    // Find a single Person with a specific name, is null if not found
                    Person findBy = unitOfWorkContext.GetSingle<Person>(x => x.Id == 6);
                    var findByASync = unitOfWorkContext.GetSingleASync<Person>(x => x.Id == 6);

                    // Find a single Person with a specific name and include its siblings
                    Person findByWithThings = unitOfWorkContext.GetSingle(x => x.Name == "Fabian", includes: incl);

                    // Find a person by id but throws exception if not found
                    var personWithIdMayThrowException = unitOfWorkContext.GetSingleBy<Person>(x => x.Id == 6);
                    var personWithIdMayThrowExceptionASync = unitOfWorkContext.GetSingleByASync<Person>(x => x.Id == 6);

                    //Update an existing person
                    unitOfWorkContext.Update(person);

                    //Deleting a Person by Id or by entity
                    unitOfWorkContext.Delete(person);
                }

                Console.ReadLine();
            }
            catch (System.Exception ex)
            {

                throw;
            }
        }
    }
}
