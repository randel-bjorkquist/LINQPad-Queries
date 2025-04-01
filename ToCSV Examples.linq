<Query Kind="Program" />

void Main()
{
  IEnumerable<int> IDs = new List<int>{ 1, 2, 3, 4, 5, 5, 3, 1, 0 };
  
  "".Dump("Results ...");
  
  // using the default, comma ',' delimeter with no spaces for padding ...
  IDs?.ToList()
      .Dump("ID List", 0);

  IDs?.ToCSV()
      .Dump("IDs.ToCSV()", 0);

  IDs?.Distinct()
      .ToCSV()
      .Dump("IDs?.Distinct().ToCSV()", 0);

  IDs?.Order()
      .ToCSV()
      .Dump("IDs?.Order().ToCSV()", 0);

  IDs?.OrderDescending()
      .ToCSV()
      .Dump("IDs?.OrderDescending().ToCSV()", 0);

  // using the colon ':' delimeter with no spaces for padding ...
  IDs?.ToCSV(":")
      .Dump("IDs.ToCSV(\":\")", 0);

  IDs?.Distinct()
      .ToCSV(":")
      .Dump("IDs?.Distinct().ToCSV(\":\")", 0);
      
  IDs?.Order()
      .ToCSV(":")
      .Dump("IDs?.Order().ToCSV(\":\")", 0);

  IDs?.OrderDescending()
      .ToCSV(":")
      .Dump("IDs?.OrderDescending().ToCSV(\":\")", 0);

  // using the pipe '|' delimeter with no spaces for padding ...
  IDs?.ToCSV("|")
      .Dump("IDs.ToCSV(\"|\")", 0);

  IDs?.Distinct()
      .ToCSV("|")
      .Dump("IDs?.Distinct().ToCSV(\"|\")", 0);

  IDs?.Order()
      .ToCSV("|")
      .Dump("IDs?.Order().ToCSV(\"|\")", 0);

  IDs?.OrderDescending()
      .ToCSV("|")
      .Dump("IDs?.OrderDescending().ToCSV(\"|\")", 0);
}

public static class EnumerableExtensions
{
  public static string? ToCSV<T>(this IEnumerable<T> source)
  {
    return source.ToCSV<T>(",");
  }

  public static string? ToCSV<T>(this IEnumerable<T> source, string separator)
  {
    return source == null
                   ? null
                   : string.Join(separator, source);

  }
}
