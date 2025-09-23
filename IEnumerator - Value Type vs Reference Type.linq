<Query Kind="Program" />

void Main()
{
  var list = new List<int>() { 0, 1, 2, 3, 4, 5 };
  
  //NOTE: explicitly creates a reference type ...
  //IEnumerator<int> enumerator = list.GetEnumerator();
  //var enumerator = list.GetEnumerator() as IEnumerator<int>;
  
  //NOTE: creates a value type, and unless it's explicitly being passed as a
  //      reference, calling methods like "MoveNext" will always be performed
  //      on that copy and will never end ...
  var enumerator = list.GetEnumerator();
  
  //NOTE: force a reference to be passed in, and in this example will, the
  //      end will be reached ...
  while(MoveNext(ref enumerator))
  {
    Console.WriteLine(enumerator.Current);
  }
  
  //NOTE: re-assigns the enumerator to the value type variable ...
  enumerator = list.GetEnumerator();
  
  //NOTE: if not passing in an Ienumerator<int>, which is a reference type, a copy will
  //      be created upon calling "MoveNext", resulting in the actual "MoveNext" always
  //      being called on a copy and thus creating an infinite loop ...
  while(MoveNext(enumerator))
  {
    Console.WriteLine(enumerator.Current);
  }

  Console.ReadLine();
}

//NOTE: if not passing in an Ienumerator<int>, which is a reference type, a copy will
//      be created upon calling "MoveNext", resulting in the actual "MoveNext" always
//      being called on a copy and thus creating an infinite loop ...
private static bool MoveNext(IEnumerator<int> enumerator)
  => enumerator.MoveNext();

//NOTE: regardless of how the variable being passed in, from the calling method/code,
//      the "MoveNext" will be called on the refrence, and thus eventually the end
//      will be reached ...
private static bool MoveNext(ref List<int>.Enumerator enumerator)
  => enumerator.MoveNext();
