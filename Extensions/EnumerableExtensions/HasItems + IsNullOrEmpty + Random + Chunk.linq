<Query Kind="Program" />

void Main()
{
  #region R&D CODE
  //
  //// Sample data
  //var numbers                 = new List<int> { 1, 3, 5, 7 };   // No evens
  //var numbersWithEven         = new List<int> { 1, 2, 3 };      // Has an even (2)
  //IEnumerable<int> nullSource = null;
  //
  //// Usage: Checks if any even number exists
  //bool hasEvenInNumbers         = numbers.IsNullOrEmpty(x => x % 2 == 0);
  //bool hasEvenInNumbersWithEven = numbersWithEven.IsNullOrEmpty(x => x % 2 == 0);
  //bool hasEvenInNull            = nullSource.IsNullOrEmpty(x => x % 2 == 0);
  //
  //Console.WriteLine($"Numbers has even? {hasEvenInNumbers}");  // False
  //Console.WriteLine($"Numbers with even has even? {hasEvenInNumbersWithEven}");  // True
  //Console.WriteLine($"Null source has even? {hasEvenInNull}");  // False
  //
  //nullSource.IsNullOrEmpty(x => x % 2 == 0).Dump();
  //
  #endregion
  
  // Sample data for testing
  var numbers = new List<int> { 1, 3, 5, 7 };  // Odds only
  var numbersWithEven = new List<int> { 1, 2, 3, 4, 5 };  // Has evens
  IEnumerable<int> nullSource = null;
  var emptyList = new List<string>();
  var fruits = new List<string> { "apple", "banana", "cherry", "date" };

  Console.WriteLine("=== EnumerableExtensions Tests ===\n");

  // 1. HasItems<T>
  Console.WriteLine("1. HasItems:");
  Console.WriteLine($"numbers.HasItems(): {numbers.HasItems()}");  // True
  Console.WriteLine($"emptyList.HasItems(): {emptyList.HasItems()}");  // False
  Console.WriteLine($"nullSource.HasItems(): {nullSource.HasItems()}");  // False
  Console.WriteLine();

  // 2. IsNullOrEmpty<T> (parameterless)
  Console.WriteLine("2. IsNullOrEmpty (no predicate):");
  Console.WriteLine($"numbers.IsNullOrEmpty(): {numbers.IsNullOrEmpty()}");  // False
  Console.WriteLine($"emptyList.IsNullOrEmpty(): {emptyList.IsNullOrEmpty()}");  // True
  Console.WriteLine($"nullSource.IsNullOrEmpty(): {nullSource.IsNullOrEmpty()}");  // True
  Console.WriteLine();

  // 3. IsNullOrEmpty<T> (with predicate) - Checks if null OR has any matching elements
  Console.WriteLine("3. IsNullOrEmpty (with predicate - has even?):");
  Console.WriteLine($"numbers.IsNullOrEmpty(x => x % 2 == 0): {numbers.IsNullOrEmpty(x => x % 2 == 0)}");  // False (non-null, no evens)
  Console.WriteLine($"numbersWithEven.IsNullOrEmpty(x => x % 2 == 0): {numbersWithEven.IsNullOrEmpty(x => x % 2 == 0)}");  // True (non-null, has evens)
  Console.WriteLine($"nullSource.IsNullOrEmpty(x => x % 2 == 0): {nullSource.IsNullOrEmpty(x => x % 2 == 0)}");  // True (null)
  Console.WriteLine($"emptyList.IsNullOrEmpty(x => x.Length > 0): {(emptyList as IEnumerable<string>).IsNullOrEmpty(x => x.Length > 0)}");  // False (non-null, no matches)
  Console.WriteLine();

  // 4. OrEmptyIfNull<T>
  Console.WriteLine("4. OrEmptyIfNull:");
  var safeNumbers = nullSource.OrEmptyIfNull();
  Console.WriteLine($"nullSource.OrEmptyIfNull().Count(): {safeNumbers.Count()}");  // 0 (empty)
  var upperFruits = fruits.OrEmptyIfNull().Select(f => f.ToUpper());
  Console.WriteLine("Upper fruits: " + string.Join(", ", upperFruits));  // APPLE, BANANA, CHERRY, DATE
  Console.WriteLine();

  // 5. Random<T> (full shuffle)
  Console.WriteLine("5. Random (full shuffle):");
  var randomFruits = fruits.Random().ToList();
  Console.WriteLine("Shuffled fruits: " + string.Join(", ", randomFruits));  // Random order, e.g., CHERRY, APPLE, DATE, BANANA
  Console.WriteLine();

  // 6. Random<T> (with count)
  Console.WriteLine("6. Random (3 items):");
  var randomNumbers = numbersWithEven.Random(3).ToList();
  Console.WriteLine("Random numbers (3): " + string.Join(", ", randomNumbers));  // e.g., 4, 1, 2 (random unique)
  Console.WriteLine();

  // 7. Chunk<T>
  Console.WriteLine("7. Chunk (size 2):");
  var fruitChunks = fruits.Chunk(2);
  int chunkNum = 1;
  foreach (var chunk in fruitChunks)
  {
      Console.WriteLine($"Chunk {chunkNum}: " + string.Join(", ", chunk));  // Chunk 1: apple, banana; Chunk 2: cherry, date
      chunkNum++;
  }
  Console.WriteLine();

  // Edge case: Chunk empty/null
  var emptyChunks = emptyList.Chunk(2);
  Console.WriteLine($"emptyList.Chunk(2).Count(): {emptyChunks.Count()}");  // 0
  Console.WriteLine($"nullSource.Chunk(2).Count(): {nullSource.Chunk(2).Count()}");  // 0 (assuming extension handles null)

  Console.WriteLine("\nTests complete!");  
}

public static partial class EnumerableExtensions
{
  //This can be used in place of source.Any() and is similar to string.IsNullOrEmpty()
  public static bool HasItems<T>(this IEnumerable<T>? source)
    => source?.Any() ?? false;  

  public static bool IsNullOrEmpty<T>(this IEnumerable<T>? source)
    => source?.Any() != true;

#region COMMENTED OUT: original version
//
//  public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
//  {
//    if (source == null)
//    {
//      return true;
//    }
//
//    using (IEnumerator<T> e = source.GetEnumerator())
//    {
//      if (!e.MoveNext())
//      {
//        return true;
//      }
//    }
//
//    return false;
//  }
//
#endregion

  //public static bool IsNullOrEmpty<T>(this IEnumerable<T>? source, Func<T, bool> predicate) => !source?.Any(predicate) ?? true;
  public static bool IsNullOrEmpty<T>(this IEnumerable<T>? source, Func<T, bool> predicate)
  {
    return !source?.Any(predicate) ?? true;
    
    #region R&D CODE
    //
    //if (predicate == null) 
    //  throw new ArgumentNullException(nameof(predicate));
    //
    //return source == null || !source.Any(predicate);
    //
    //if (predicate == null) 
    //    throw new ArgumentNullException(nameof(predicate));
    //
    //if (source == null)
    //    return true;
    //
    //// Explicitly filter to a list and check length (as per your "length of the list" suggestion)
    //var filtered = source.Where(predicate).ToList();
    //return filtered.Count == 0;
    //
    #endregion
  }

#region COMMENTED OUT: original version
//
//  public static bool IsNullOrEmpty<T>(this IEnumerable<T> source, Func<T, bool> predicate)
//  {
//    if (predicate == null)
//    {
//      throw new ArgumentNullException(nameof(predicate));
//    }
//    
//    if (source == null)
//    {
//      return true;
//    }
//
//    foreach (T element in source)
//    {
//      if (predicate(element))
//      {
//        return true;
//      }
//    }
//
//    return false;
//  }
//
#endregion

  public static IEnumerable<T> OrEmptyIfNull<T>(this IEnumerable<T>? source)
  {
    return source ?? Enumerable.Empty<T>();
  }

  /// <summary>
  /// Shuffles and returns the entire source.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="source"></param>
  /// <returns></returns>
  public static IEnumerable<T> Random<T>(this IEnumerable<T>? source)
  {
    return source.Random(int.MaxValue);
  }

  /// <summary>
  /// Returns an IEnumerable of random items. The number of items will be equal to,
  /// whichever is smaller, the supplied 'count' argument or the source count.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="source"></param>
  /// <param name="count"></param>
  /// <returns></returns>
  public static IEnumerable<T> Random<T>(this IEnumerable<T>? source, int count)
  {
    if (source.IsNullOrEmpty())
        yield break;

    var list = source.ToList();
    count = Math.Min(count, list.Count);

    // Fisher-Yates shuffle for efficiency (O(n))
    var indices = Enumerable.Range(0, list.Count).ToArray();
    var random  = new Random();
    
    for (int i = indices.Length - 1; i > 0; i--)
    {
      int j = random.Next(0, i + 1);
      (indices[i], indices[j]) = (indices[j], indices[i]);
    }

    foreach (int idx in indices.Take(count))
        yield return list[idx];
  }

  public static IEnumerable<IEnumerable<T>> Chunk<T>(this IEnumerable<T>? source, int chunksize)
  {
    if (source?.Any() != true || chunksize <= 0)
        yield break;

    using var enumerator = source.GetEnumerator();
    while (enumerator.MoveNext())
    {
        var chunk = new List<T> { enumerator.Current };
        for (int i = 1; i < chunksize && enumerator.MoveNext(); i++)
        {
            chunk.Add(enumerator.Current);
        }
        yield return chunk;
    }
  }
}