<Query Kind="Program" />

void Main()
{
  var numbers = GetNumbers(100);
  
  numbers.Where(n => n % 2 == 0)
         .Dump("numbers.Where(n => n % 2 == 0)", 0)
         .Any()
         .Dump("numbers.Where(n => n % 2 == 0).Any()", 0);
  
  numbers.Any(n => n % 2 == 0)
         .Dump("numbers.Any(n => n % 2 == 0)", 0);
}

// You can define other methods, fields, classes and namespaces here
public IEnumerable<int> GetNumbers(int max_value = 10000)
{
  for(int i = 0; i < max_value; i++)
    yield return i;
}