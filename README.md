# Generic Repository Pattern for Entity Framework Core

Offering you a complete abstraction of the UnitOfWork-Pattern & Repository pattern with the basic CRUD-Operations, the Repository Pattern and extended functions like CustomRepositores all in one small lib. Made for the Entity Framework Core.

See the Sample-Project how this works.

## Installation

See Nuget to load this package:
[Nuget](https://www.nuget.org/packages/OfferingSolutions.GenericEFCore)

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

## Methods

### Get all entities

```
IQueryable<T> GetAll();
Task<IQueryable<T>> GetAllAsync()
```

Example: `personRepository.GetAll();`

### Gets all entities with predicate

```
IQueryable<T> GetAll([predicate])
Task<IQueryable<T>> GetAllAsync([predicate])
```

Example: `personRepository.GetAll(x => x.Name == "John Doe");`

### Gets all entities and includes child entities

```
IQueryable<T> GetAll([include])
Task<IQueryable<T>> GetAllAsync([include])
```

Example: `personRepository.GetAll(source => source.Include(y => y.Things));`

### Gets all entities with predicate and includes a child entity

```
IQueryable<T> GetAll([predicate],[include])
Task<IQueryable<T>> GetAllAsync([predicate],[include])
```

Example: `personRepository.GetAll(x => x.Name == "John Doe", source => source.Include(y => y.Things));`

### Gets all entities with predicate, includes, orderby, skip & take; all nullable (orderby as lambda)

```
IQueryable<T> GetAll([predicate],[include],[orderBy],[skip],[take])
Task<IQueryable<T>> GetAllAsync([predicate],[include],[orderBy],[skip],[take])
```

Example: `personRepository.GetAll(predicate: x => x.Name == "John Doe", source => source.Include(y => y.Things), orderBy: q => q.OrderBy(d => d.Name));`

### Gets all entities with predicate, includes, orderby (string), orderDirection (string), skip & take; all nullable

```
IQueryable<T> GetAll([predicate],[include],[orderBy],[orderDirection],[skip],[take])
Task<IQueryable<T>> GetAllAsync([predicate],[include],[orderBy],[orderDirection],[skip],[take])
```

Example: `personRepository.GetAll(predicate: x => x.Name == "John Doe", source => source.Include(y => y.Things), orderBy: "Name", "asc", 5, 5);`

### Gets single entity with predicate and include; all nullable

```
T GetSingle([predicate],[include]);
Task<T> GetSingleAsync([predicate],[include])
```

Example: `personRepository.GetSingle(x => x.Name == "John Doe");` or `personRepository.GetSingle(x => x.Name == "John Doe", source => source.Include(y => y.Things));`

### Adding an entity

```
void Add([entity]);
void AddAsync([entity])
```

Example: `personRepository.Add(new Person() { Name = "John Doe" });`

### Updating an entity

```
T Update([entity])
```

Example: `personRepository.Update(changedPerson);`

### Deleting an entity

```
void Delete([entity]);
void Delete([predicate]);
```

Example: `personRepository.Delete(toDelete);` or `personRepository.Delete(x => x.Id == id);`

### Count entities

```
int Count();
int Count([predicate]);
```

Example: `personRepository.Count();` or `personRepository.Count(x => x.Name == "John Doe");`

### Saving entities

```
int Save()
Task<int> SaveAsync()
```

Example: `personRepository.Save();`

## Complete Unit of Work

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
