using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OfferingSolutions.GenericEFCore.SampleApp.Models;
using OfferingSolutions.GenericEFCore.Tests.ExampleRepositories;
using System.Collections.Generic;

namespace OfferingSolutions.GenericEFCore.Tests
{
    [TestClass]
    public class CustomRepositoryTests
    {
        [TestMethod]
        public void Insert_Adds_To_Database()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DataBaseContext>()
                .UseInMemoryDatabase(databaseName: nameof(Insert_Adds_To_Database))
                .Options;

            // Act
            using (IPersonRepository personRepository = new PersonRepository(new DataBaseContext(options)))
            {
                personRepository.Add(new Person() { Name = "John Doe" });
                personRepository.Save();
            }

            // Assert
            using (IPersonRepository personRepository = new PersonRepository(new DataBaseContext(options)))
            {
                var savedPerson = personRepository.GetSingle(x => x.Name == "John Doe");
                Assert.IsNotNull(savedPerson);
                Assert.IsNotNull(savedPerson.Id);
            }
        }

        [TestMethod]
        public void Count_Counts_Correct()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DataBaseContext>()
                .UseInMemoryDatabase(databaseName: nameof(Insert_Adds_To_Database))
                .Options;

            // Act
            using (IPersonRepository personRepository = new PersonRepository(new DataBaseContext(options)))
            {
                personRepository.Add(new Person() { Name = "John Doe" });
                personRepository.Add(new Person() { Name = "Jane Doe" });
                personRepository.Save();
            }

            // Assert
            using (IPersonRepository personRepository = new PersonRepository(new DataBaseContext(options)))
            {
                var countWithPredicate = personRepository.Count(x => x.Name == "John Doe");
                Assert.AreEqual(1, countWithPredicate);
                var overallCount = personRepository.Count();
                Assert.AreEqual(2, overallCount);
            }
        }

        [TestMethod]
        public void Update_Modifies_Entry_In_Database()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DataBaseContext>()
                .UseInMemoryDatabase(databaseName: nameof(Update_Modifies_Entry_In_Database))
                .Options;

            // Act
            using (IPersonRepository personRepository = new PersonRepository(new DataBaseContext(options)))
            {
                personRepository.Add(new Person() { Name = "John Doe" });
                personRepository.Save();
                var savedPerson = personRepository.GetSingle(x => x.Name == "John Doe");
                savedPerson.Name = "Jane Doe";
                personRepository.Update(savedPerson);
                personRepository.Save();
            }

            // Assert
            using (IPersonRepository personRepository = new PersonRepository(new DataBaseContext(options)))
            {
                var savedPerson = personRepository.GetSingle(x => x.Name == "Jane Doe");
                Assert.IsNotNull(savedPerson);
                Assert.IsNotNull(savedPerson.Id);
            }
        }

        [TestMethod]
        public void DeleteByEntity_Removes_Entry_From_Database()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DataBaseContext>()
                .UseInMemoryDatabase(databaseName: nameof(DeleteByEntity_Removes_Entry_From_Database))
                .Options;

            // Act
            using (IPersonRepository personRepository = new PersonRepository(new DataBaseContext(options)))
            {
                personRepository.Add(new Person() { Name = "John Doe" });
                personRepository.Add(new Person() { Name = "Jane Doe" });
                personRepository.Save();
                var savedPerson = personRepository.GetSingle(x => x.Name == "John Doe");
                personRepository.Delete(savedPerson);
                personRepository.Save();
            }

            // Assert
            using (IPersonRepository personRepository = new PersonRepository(new DataBaseContext(options)))
            {
                var personCount = personRepository.Count();
                Assert.AreEqual(1, personCount);
            }
        }

        [TestMethod]
        public void DeleteById_Removes_Entry_From_Database()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DataBaseContext>()
                .UseInMemoryDatabase(databaseName: nameof(DeleteById_Removes_Entry_From_Database))
                .Options;

            // Act
            using (IPersonRepository personRepository = new PersonRepository(new DataBaseContext(options)))
            {
                personRepository.Add(new Person() { Name = "John Doe" });
                personRepository.Add(new Person() { Name = "Jane Doe" });
                personRepository.Save();
                var savedPerson = personRepository.GetSingle(x => x.Name == "John Doe");
                personRepository.Delete(x => x.Id == savedPerson.Id);
                personRepository.Save();
            }

            // Assert
            using (IPersonRepository personRepository = new PersonRepository(new DataBaseContext(options)))
            {
                var personCount = personRepository.Count();
                Assert.AreEqual(1, personCount);
            }
        }

        [TestMethod]
        public void Include_Selects_Child_Items_From_Database()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DataBaseContext>()
                .UseInMemoryDatabase(databaseName: nameof(Include_Selects_Child_Items_From_Database))
                .Options;

            // Act
            using (IPersonRepository personRepository = new PersonRepository(new DataBaseContext(options)))
            {
                Person john = new Person()
                {
                    Name = "John Doe",
                    Things = new List<Thing>() {
                    new Thing() { Name = "Thing1"} }
                };

                Person jane = new Person()
                {
                    Name = "Jane Doe",
                    Things = new List<Thing>() {
                    new Thing() { Name = "Thing2"} }
                };

                personRepository.Add(john);
                personRepository.Add(jane);
                personRepository.Save();
            }

            // Assert
            using (IPersonRepository personRepository = new PersonRepository(new DataBaseContext(options)))
            {
                var personCount = personRepository.Count();
                Assert.AreEqual(2, personCount);

                var johnFromDb = personRepository
                    .GetSingle(x => x.Name == "John Doe", 
                    source => source.Include(x => x.Things));

                Assert.IsNotNull(johnFromDb);
                Assert.IsNotNull(johnFromDb.Things);
                Assert.AreEqual(1, johnFromDb.Things.Count);

                var janeFromDb = personRepository
                    .GetSingle(x => x.Name == "Jane Doe",
                    source => source.Include(x => x.Things));

                Assert.IsNotNull(janeFromDb);
                Assert.IsNotNull(janeFromDb.Things);
                Assert.AreEqual(1, janeFromDb.Things.Count);
            }
        }
    }
}
