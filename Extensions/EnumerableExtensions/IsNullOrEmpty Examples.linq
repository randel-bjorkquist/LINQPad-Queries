<Query Kind="Program" />

void Main()
{
  List<int>? emptyList = [];
  List<int>? nullList = null;

  IsNullOrEmpty(emptyList);
  IsNullOrEmpty(nullList);

  List<int>? intList = [1, 2, 3];
  IsNullOrEmpty(intList);

  "".Dump();
  
  //NOTE: Normal calling syntax ...
  emptyList.IsNullOrEmpty()
         .Dump("emptyList.IsNullOrEmpty()");
  
  nullList.IsNullOrEmpty()
          .Dump("nullList.IsNullOrEmpty()");

  intList.IsNullOrEmpty()
         .Dump("intList.IsNullOrEmpty()");

  "".Dump();
  
  //NOTE: Shows the actual value of the conditional statement. See how "nullList" shows "null" 
  //      and both "emptyList" and "intList" varles are either true or false ...
  (emptyList?.Any()).Dump("emptyList?.Any()");
  (nullList?.Any()).Dump("nullList?.Any()");
  (intList?.Any()).Dump("intList?.Any()");
}

//NOTE: The only purpose for this method is to show how the extension method can be used.
//      Normally the extension method will be appended to the end of the collection,
//      like: emptyList.IsNullOrEmpty() 
//         or nullList.IsNullOrEmpty() 
//         or intList.IsNullOrEmpty()
void IsNullOrEmpty<T>(IEnumerable<T>? collection)
{
  if (collection.IsNullOrEmpty())
    Console.WriteLine("null or empty");
  else
    Console.WriteLine("list not null nor empty");
}

public static class IEnumerableExtensions
{
  public static bool IsNullOrEmpty<T>(this IEnumerable<T>? source)
    => !(source?.Any() ?? false); // or any other implementation you want
}