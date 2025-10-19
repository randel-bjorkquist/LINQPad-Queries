<Query Kind="Program" />

void Main()
{
  Generators.Print("blah");
  Generators.Print(22);

  Console.WriteLine();
  
  Generators.Database.Print("blah");
  Generators.Database.Print(22);
  
  Console.WriteLine();
  
  Generators.Service.Print("blah");
  Generators.Service.Print(22);
}


public static class Generators
{
  public static void Print<T>(T value)
  {
    $"Print from Generators => '{value}'".Dump();
  }
  
  public static class Database
  {
    public static void Print<T>(T value)
    {
      $"Print from Generators.Database => '{value}'".Dump();
    }
  }

  public static class Service
  {
    public static void Print<T>(T value)
    {
      $"Print from Generators.Service => '{value}'".Dump();
    }  
  }
}