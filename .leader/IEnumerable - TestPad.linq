<Query Kind="Program" />

void Main()
{
  IEnumerable<string> strings = new List<string>{ "string 1"
                                                 ,"string 2"
                                                 ,"string 3" };
  strings.Dump();
  
  strings = null;
  
  (strings?.ToList() ?? Enumerable.Empty<string>().ToList()).Dump();
  //var x = typeof(Enumerable.Empty<string>());
}

// You can define other methods, fields, classes and namespaces here
