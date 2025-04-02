<Query Kind="Program" />

void Main()
{
  IEnumerable<int> IDs = new List<int>{ 1, 2, 3, 4, 5, 5, 3, 1, 0 };
  
  #region default separator ','
  
  "Using the default separator ',' as the delimeter, no leading or trailing spaces for padding ...".Dump();
  
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

  #endregion

  #region custom separator, comma space ', '
  
  "".Dump();
  "Using the string comma space \", \" as the delimeter, only a trailing spaces for padding ...".Dump();

  IDs?.ToCSV(", ")
      .Dump("IDs.ToCSV(\", \")", 0);

  IDs?.Distinct()
      .ToCSV(", ")
      .Dump("IDs?.Distinct().ToCSV(\", \")", 0);

  IDs?.Order()
      .ToCSV(", ")
      .Dump("IDs?.Order().ToCSV(\", \")", 0);

  IDs?.OrderDescending()
      .ToCSV(", ")
      .Dump("IDs?.OrderDescending().ToCSV(\", \")", 0);
  
  #endregion
  
  #region custom separator, pipe '|'
  
  "".Dump();
  "Using a pipe character '|' as the delimeter, no spaces for padding ...".Dump();

  IDs?.ToCSV('|')
      .Dump("IDs.ToCSV('|')", 0);

  IDs?.Distinct()
      .ToCSV('|')
      .Dump("IDs?.Distinct().ToCSV('|')", 0);

  IDs?.Order()
      .ToCSV('|')
      .Dump("IDs?.Order().ToCSV('|')", 0);

  IDs?.OrderDescending()
      .ToCSV('|')
      .Dump("IDs?.OrderDescending().ToCSV('|')", 0);

  #endregion
  
  #region custom separator, tab '\t'
  
  "".Dump();
  "Using a tab character '\\t' as the delimeter, no spaces for padding ...".Dump();
  
  IDs?.ToCSV('\t')
      .Dump("IDs.ToCSV(\"\\t\")", 0);

  IDs?.Distinct()
      .ToCSV('\t')
      .Dump("IDs?.Distinct().ToCSV('\\t)", 0);

  IDs?.Order()
      .ToCSV('\t')
      .Dump("IDs?.Order().ToCSV('\\t)", 0);

  IDs?.OrderDescending()
      .ToCSV('\t')
      .Dump("IDs?.OrderDescending().ToCSV('\\t')", 0);
  
  #endregion
  
  #region custom separator, " & "
  
  "".Dump();
  "Using the string string \" & \" as the delimeter, with leading and trailing spaces for padding ...".Dump();
  
  IDs?.ToCSV(" & ")
      .Dump("IDs.ToCSV(\" & \")", 0);

  IDs?.Distinct()
      .ToCSV(" & ")
      .Dump("IDs?.Distinct().ToCSV(\" & \")", 0);

  IDs?.Order()
      .ToCSV(" & ")
      .Dump("IDs?.Order().ToCSV(\" & \")", 0);

  IDs?.OrderDescending()
      .ToCSV(" & ")
      .Dump("IDs?.OrderDescending().ToCSV(\" & \")", 0);
      
  #endregion
}

public static class EnumerableExtensions
{
  public static string? ToCSV<T>(this IEnumerable<T> source)
  {
    return source.ToCSV<T>(',');
  }

  public static string? ToCSV<T>(this IEnumerable<T> source, char separator)
  {
    return source == null
                   ? null
                   : string.Join(separator, source);
  }
  
  public static string? ToCSV<T>(this IEnumerable<T> source, string separator)
  {
    return source == null
                   ? null
                   : string.Join(separator, source);
  }
}
