# Generic Repository Pattern for Entity Framework Core

Offering you a complete abstraction of the UnitOfWork-Pattern & Repository pattern with the basic CRUD-Operations, the Repository Pattern and extended functions like CustomRepositores all in one small lib. Made for the Entity Framework Core.

See the Sample-Project how this works.

## Installation

See Nuget to load this package:
[Nuget](https://www.nuget.org/packages/OfferingSolutions.GenericEFCore/0.0.2)

```
Install-Package Install-Package OfferingSolutions.GenericEFCore
```

```
dotnet add package OfferingSolutions.GenericEFCore
```

Have fun. Hope this helps :)

## Usage

### Example for Generic Repositories

```c#
interface IPersonRepository : IGenericRepositoryContext<Person>
{

}
```

```c#
public class PersonRepository : GenericRepositoryContext<Person>, IPersonRepository
{
    public PersonRepository(DataBaseContext dbContext)
        : base(dbContext)
    {

    }
}
```

Or you can add and overwrite methods

```c#
interface IPersonRepository : IGenericRepositoryContext<Person>
{
    void MyNewFunction(int id);

    void Add(Person toAdd);
}
```

```c#
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
```

```c#
using (IPersonRepository personRepository = new PersonRepository(new DataBaseContext()))
{
    personRepository.Add(new Person() { Name = "John Doe" });
    personRepository.Save();
    var receivedPerson = personRepository.GetSingle(x => x.Name == "John Doe");
    Console.WriteLine(receivedPerson);

    // Does something good ...
    personRepository.MyNewFunction(6);
}
```

### Methods

| Name                                                                                              | Description                                                                                                      | Example                                                                                                                                                            |
| ------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| `IQueryable<T> GetAll()`                                                                          | Gets all entities                                                                                                | `personRepository.GetAll();`                                                                                                                                       |
| `IQueryable<T> GetAll([predicate])`                                                               | Gets all entities with predicate                                                                                 | `personRepository.GetAll(x => x.Name == "John Doe");`                                                                                                              |
| `IQueryable<T> GetAll([include])`                                                                 | Gets all entities and includes a child entity                                                                    | `personRepository.GetAll(x => x.Name == "John Doe", source => source.Include(y => y.Things));`                                                                     |
| `IQueryable<T> GetAll([predicate],[include])`                                                     | Gets all entities with predicate and includes a child entity                                                     | `personRepository.GetAll(source => source.Include(y => y.Things));`                                                                                                |
| `IQueryable<T> GetAll([predicate],[include],[orderBy],[skip],[take])`                             | Gets all entities with predicate, includes, orderby, skip & take; all nullable                                   | `personRepository.GetAll(predicate: x => x.Name == "John Doe", orderBy: q => q.OrderBy(d => d.Name), source => source.Include(y => y.Things));`                    |
| `Task<IQueryable<T>> GetAllAsync()`                                                               | Gets all entities                                                                                                | `personRepository.GetAllAsync();`                                                                                                                                  |
| `Task<IQueryable<T>> GetAllAsync([predicate])`                                                    | Gets all entities with predicate                                                                                 | `personRepository.GetAllAsync(x => x.Name == "John Doe");`                                                                                                         |
| `Task<IQueryable<T>> GetAllAsync([include])`                                                      | Gets all entities and includes a child entity                                                                    | `personRepository.GetAllAsync(source => source.Include(y => y.Things));`                                                                                           |
| `Task<IQueryable<T>> GetAllAsync([predicate],[include])`                                          | Gets all entities by predicate and includes a child entity                                                       | `personRepository.GetAllAsync(x => x.Name == "John Doe", source => source.Include(y => y.Things));`                                                                |
| `Task<IQueryable<T>> GetAllAsync([predicate],[include],[orderBy],[skip],[take])`                  | Gets all entities with predicate, includes, orderby, skip & take; all nullable                                   | `personRepository.GetAllAsync(x => x.Name == "John Doe", q => q.OrderBy(d => d.Name), source => source.Include(y => y.Things));`                                   |
| `Task<IQueryable<T>> GetAllAsync([predicate],[include],[orderBy],[orderDirection],[skip],[take])` | Gets all entities with predicate, includes, orderby (string), orderDirection (string), skip & take; all nullable | `personRepository.GetAllAsync(x => x.Name == "John Doe", "Name", "asc", source => source.Include(y => y.Things));`                                                 |
| `T GetSingle([predicate],[include])`                                                              | Gets single entity with predicate and include; all nullable                                                      | `personRepository.GetSingle(x => x.Name == "John Doe");` or `personRepository.GetSingle(x => x.Name == "John Doe", source => source.Include(y => y.Things));`      |
| `Task<T> GetSingleAsync([predicate],[include])`                                                   | Gets single entity with predicate and include; all nullable                                                      | `personRepository.GetSingleAsync(x => x.Name == "John Doe");` or `personRepository.GetSingle(x => x.Name == "John Doe", source => source.Include(y => y.Things));` |
| `void Add([entity])`                                                                              | Adds an entity                                                                                                   | `personRepository.Add(new Person() { Name = "John Doe" });`                                                                                                        |
| `void AddAsync([entity])`                                                                         | Adds an entity                                                                                                   | `personRepository.AddAsync(new Person() { Name = "John Doe" });`                                                                                                   |
| `T Update([entity])`                                                                              | Updates an entity                                                                                                | `personRepository.Update(changedPerson);`                                                                                                                          |
| `void Delete([entity])`                                                                           | Deletes an entity by complete entity                                                                             | `personRepository.Delete(toDelete);`                                                                                                                               |
| `void Delete([predicate])`                                                                        | Deletes an entity by predicate                                                                                   | `personRepository.Delete(x => x.Id == id);`                                                                                                                        |
| `int Count()`                                                                                     | Counts all Items in that repository                                                                              | `personRepository.Count();`                                                                                                                                        |
| `int Save()`                                                                                      | Saves                                                                                                            | `personRepository.Count();`                                                                                                                                        |
| `Task<int> SaveAsync()`                                                                           | Saves (async)                                                                                                    | `personRepository.Count();`                                                                                                                                        |

### Example for complete Unit of Work

```c#
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
```
