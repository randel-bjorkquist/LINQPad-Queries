<Query Kind="Program" />

void Main()
{
  var numbers = GetNumbers(100);
//  numbers.Dump("numbers", 0);
//  
//  numbers.Take(10).Dump("numbers.Take(10)", 0);
//  numbers.Take(10).Dump("numbers.Take(10)", 0);
//  numbers.Take(10).Dump("numbers.Take(10)", 0);
  
  while(numbers.Any()){
    var list = numbers.Take(10);
    list.Dump(0);
    numbers = numbers.Skip(10);
  }
}

// You can define other methods, fields, classes and namespaces here
public IEnumerable<int> GetNumbers(int max_value = 10000)
{
  for(int i = 0; i < max_value; i++)
    yield return i;
}