using LaYumba.Functional;
using static LaYumba.Functional.F;
using static Microsoft.AspNetCore.Http.Results;

namespace SampleWeb
{

  public static class PeopleEndpoint
  {
    
    public static ConnectionString conn = "Server=(localdb)\\MSSQLLocalDb;Database=Foo;User Id=foobar;Password=foobar;";

    public static Option<Person> getPerson(int id)
    {
      SqlTemplate select = "SELECT * FROM People WHERE Id = @Id";

      var queryById = conn.Retrieve<Person>(select);

      var LookupPerson = (int id) => queryById(new { Id = id }).FirstOrDefault();

      var person = LookupPerson(id);

      if (person == null)
      {
        return None;
      } else
      {
        return Some(person);
      }
    }

    public static void Configure(
      this WebApplication app,
      Func<int, Option<Person>> getPerson
    ) => app.MapGet("/person/{id}", (int id) =>
    {
      Console.WriteLine($"/person/{id}");

      return getPerson(id).Match(
        None: () => NotFound(),
        Some: (x) => Ok(x)
        );
    });


    //SqlTemplate select = "SELECT * FROM EMPLOYEES"
    //        , sqlById = $"{select} WHERE ID = @Id"
    //        , sqlByName = $"{select} WHERE LASTNAME = @LastName";

    //// queryById : object → IEnumerable<Employee>
    //var queryById = conn.Retrieve<Employee>(sqlById);

    //// queryByLastName : object → IEnumerable<Employee>
    //var queryByLastName = conn.Retrieve<Employee>(sqlByName);

    //// LookupEmployee : Guid → Option<Employee>
    //Option<Employee> LookupEmployee(Guid id)
    //   => queryById(new { Id = id }).FirstOrDefault();

    //// FindEmployeesByLastName : string → IEnumerable<Employee>
    //IEnumerable<Employee> FindEmployeesByLastName(string lastName)
    //   => queryByLastName(new { LastName = lastName });
  }
}
