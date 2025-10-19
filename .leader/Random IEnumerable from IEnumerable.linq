<Query Kind="Program" />

void Main()
{
  var source = GenerateList(10)
                  .ToList()
                  .Dump("GenerateList(10).ToList()", 0);

  source.Random()
        .ToList()
        .Dump("source.Random()", 0);

  var result = source.Random(3)
                     .ToList()
                     .Dump("result.Random(3)", 0);

  //Items NOT selected/added to the 'results'
  source.Except(result).Dump("source.Except(result)", 0);
  
  //Items randomly picked from 'source'.
  result.Except(source).Dump("result.Except(source)", 0);
  
  List<int> more_ints = null;
  more_ints.Random(0)
           .Dump("List<int> more_ints = null", 0);
  
  //TEST CODE: confirms a foreach DOES NOT BREAK after calling 
  //'Random(x)' on as null value.
  //foreach(var x in more_ints.Random(10))
  //{
  //  x.Dump("x");
  //}
}

public static IEnumerable<int> GenerateList(int count)
{
  var random  = new Random();
  var values  = new List<int>();
  
  #region YIELD RETURN CODE
  
  for(int i = 0; i < count; ++i)
  {
    yield return random.Next();
  }
  
  #endregion
  
  #region NON-YIELD RETURN CODE
  //
  //var results = new List<int>();
  //
  //for(int i = 0; i < count; i++)
  //{
  //  results.Add(random.Next());
  //}
  //
  //return results;
  //
  #endregion
}

#region COMMENTED OUT: R&D

//public static IEnumerable<string> GenerateList()
//public static string GenerateList()
//{
//  var random  = new Random();
//  var list    = new List<string> { "one", "two", "three", "four" };
//
//  int index = random.Next(list.Count);
//
//  return list[index].Dump();
//}

#endregion

/// <summary>
/// Returns a Random IEnumerable<T>, with the max number of elements to equal
/// the source count or the supplied 'count' value, which ever is smaller.
/// </summary>
public static class EnumerableExtensions
{
  public static IEnumerable<T> Random<T>(this IEnumerable<T> source)
  {
    return source.Random(source.Count());
  }
  
  public static IEnumerable<T> Random<T>(this IEnumerable<T> source, int count)
  {
    if(source.IsNullOrEmpty())
    {
      yield break;
    }

    if(source.Count() < count)
    {
      count = source.Count();
    }
    
    var list = source.ToList();

    var random  = new Random();
    var indexs  = new List<int>();
    
    //Loops until the number of returned items has reached 'count'.
    while(indexs.Count() < count)
    {
      int index = 0;
      
      //Makes sure each index value is only used once.
      do{      
        index = random.Next(source.Count());         
      }while(indexs.Contains(index));
      
      indexs.Add(index);
      
      yield return list[index];
    }
    
#region COMMENTED OUT: for-loop 
//    
//    for(int i = 0; i < count; i++)
//    {
//      int index;
//      
//      do
//      {
//        index = random.Next(count);
//      }while(indexs.Contains(index));
//        
//      indexs.Add(index);
//      
//      yield return list[index];
//    }
//
#endregion
  }
            
  //This can be used in place of source.Any() and is similar to string.IsNullOrEmpty()
  public static bool HasItems<T>(this IEnumerable<T> source)
  {
    return source?.Any() ?? false;
  }
    
  public static bool IsNullOrEmpty<T>( this IEnumerable<T> source )
  {
    if( source == null )
    {
      return true;
    }
    
    using( IEnumerator<T> e = source.GetEnumerator() )
    {
      if( !e.MoveNext() )
      {
        return true;
      }
    }
    
    return false;
  }
    
  public static bool IsNullOrEmpty<T>( this IEnumerable<T> source, Func<T, bool> predicate )
  {
    if( predicate == null )
    {
      throw new ArgumentNullException( "predicate" );
    }
    
    foreach( T element in source )
    {
      if( predicate( element ))
      {
        return true;
      }
    }
    
    return false;
  }

  public static IEnumerable<T> OrEmptyIfNull<T>(this IEnumerable<T> source)
  {
    return source ?? Enumerable.Empty<T>();
  }  
}






















