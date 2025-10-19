<Query Kind="Program" />

void Main()
{
  var p = new Person { FirstName  = " John "
                      ,MiddleName = " Jim "
                      ,LastName   = " Jones " };

  string _p = p;
  $"'{_p}'".Dump("_p = p");
  
  p.Dump("p.Dump()");
}

public class Person
{
  public string FirstName   { get; set; }
  public string MiddleName  { get; set; }
  public string LastName    { get; set; }
  
  public static implicit operator string(Person person)
  {
    if (person == null)
    {
      return null;
    }

    return person.ToString();
  }

  public override string ToString()
  {
    return  (string.IsNullOrWhiteSpace(FirstName)  ? string.Empty : FirstName.Trim())
          + (string.IsNullOrWhiteSpace(MiddleName) ? string.Empty : $" {MiddleName.Trim()}")
          + (string.IsNullOrWhiteSpace(LastName)   ? string.Empty : $" {LastName.Trim()}");
  }
}