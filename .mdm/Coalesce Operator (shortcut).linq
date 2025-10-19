<Query Kind="Program" />

void Main()
{
  //Person me = null;  
  Person me = new Person { FirstName = "Randel", LastName = "Bjorkquist" };
  
  me ??= new Person { FirstName = "Fred", LastName = "Flintstone" };
  
  me.Dump();
}

private class Person
{
  public string FirstName;
  public string LastName;
}