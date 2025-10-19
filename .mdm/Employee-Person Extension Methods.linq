<Query Kind="Program" />

void Main()
{
  var employee = new Employee("123-abc", "fred", "flintstone");
  var person   = new Person("barney", "rubble");

  employee.Dump("Employee", 0);
  employee.ConvertToPerson().Dump("Employee Converted to Person", 0);

  person.Dump("Person", 0);  
  person.ConvertToEmployee().Dump("Person Converted to Employee", 0);

  var emplyee_barney_rubble = new Employee(person) { ID = "987-zxy" };
  emplyee_barney_rubble.Dump("emplyee_barney_rubble", 0);
}

public static class PersonExtensions
{
  public static Employee ConvertToEmployee(this Person person)
  {
    return person == null
            ? null
            : new Employee(person);
  }
}

public class Person
{
  public Person() { }
  public Person(string first_name, string last_name) {
    FirstName = first_name;
    LastName  = last_name;
  }
  public Person(Employee employee) {
    FirstName = employee?.FirstName;
    LastName = employee?.LastName;
  }
  
  public string FirstName { get; set; }
  public string LastName  { get; set; }
}

public static class EmployeeExtensions
{
  public static Person ConvertToPerson(this Employee employee)
  {
    return employee == null
            ? null
            : new Person(employee);
  }
}

public class Employee
{
  public Employee() { }
  public Employee(string id, string first_name, string last_name) { 
    ID = id;
    FirstName = first_name;
    LastName  = last_name;
  }
  public Employee(Person person) { 
    FirstName = person?.FirstName;
    LastName  = person?.LastName;
  }
  
  public string ID        { get; set; }
  
  public string FirstName { get; set; }
  public string LastName  { get; set; }
}