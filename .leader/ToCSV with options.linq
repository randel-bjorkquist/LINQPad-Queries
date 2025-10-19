<Query Kind="Program" />

void Main()
{
  var uids = new List<string> { "0xf97bbe3", "0xf97b5b7", "0xf97b53e", "0xf97b4e0", "0xf97a018" };

  var sql = $"WHERE OrganizationUID IN ('{uids.ToCSV("', '")}')";
  sql.Dump("SQL -> WHERE IN");  
  
  #region ttaIDs NULL
  
  IEnumerable<int> ttaIDs = null;
  ttaIDs.Dump("ttaIDs", 0);
  
  var ttaIdList = ttaIDs?.ToList();
  ttaIdList.Dump("ttaIdList", 0);
  
  ttaIdList.ToCSV()
           .Dump("ttaIdList.ToCSV()", 0 );

  ttaIdList.ToCSV(ToCSVOptions.Distinct)
           .Dump("ttrIdList.ToCSV(ToCSVOptions.Distinct)", 0);

  ttaIdList.ToCSV(ToCSVOptions.OrderBy)
           .Dump("ttaIdList.ToCSV(ToCSVOptions.OrderBy)", 0);

  ttaIdList.ToCSV(ToCSVOptions.OrderByDescending)
           .Dump("ttaIdList.ToCSV(ToCSVOptions.OrderByDescending)", 0);

  ttaIdList.ToCSV(ToCSVOptions.Distinct | ToCSVOptions.OrderBy)
           .Dump("ttaIdList.ToCSV(ToCSVOptions.Distinct & ToCSVOptions.OrderBy)", 0);

  ttaIdList.ToCSV(ToCSVOptions.Distinct | ToCSVOptions.OrderByDescending)
           .Dump("ttaIdList.ToCSV(ToCSVOptions.Distinct & ToCSVOptions.OrderByDescending)", 0);
  
  ttaIdList.ToCSV(":")
           .Dump("ttaIdList.ToCSV(':')", 0);

  ttaIdList.ToCSV(":", ToCSVOptions.Distinct)
           .Dump("ttrIdList.ToCSV(':', ToCSVOptions.Distinct)", 0);

  ttaIdList.ToCSV(":", ToCSVOptions.OrderBy)
           .Dump("ttaIdList.ToCSV(':', ToCSVOptions.OrderBy)", 0);

  ttaIdList.ToCSV(":", ToCSVOptions.OrderByDescending)
           .Dump("ttaIdList.ToCSV(':', ToCSVOptions.OrderByDescending)", 0);

  ttaIdList.ToCSV(":", ToCSVOptions.Distinct | ToCSVOptions.OrderBy)
           .Dump("ttaIdList.ToCSV(':', ToCSVOptions.Distinct & ToCSVOptions.OrderBy)", 0);

  ttaIdList.ToCSV(":", ToCSVOptions.Distinct | ToCSVOptions.OrderByDescending)
           .Dump("ttaIdList.ToCSV(':', ToCSVOptions.Distinct & ToCSVOptions.OrderByDescending)", 0);

  #endregion
  
  #region ttrIDs NOT NULL
  
  IEnumerable<int> ttrIDs = new List<int>{ 1, 2, 3, 4, 5, 5, 3, 1, 0 };
  ttrIDs.Dump("ttrIDs", 0);
  
  var ttrIdList = ttrIDs?.ToList();
  ttrIdList.Dump("ttrIdList", 0);
  
  ttrIdList.ToCSV()
           .Dump("ttrIdList.ToCSV()", 0);

  ttrIdList.ToCSV(ToCSVOptions.Distinct)
           .Dump("ttrIdList.ToCSV(ToCSVOptions.Distinct)", 0);

  ttrIdList.ToCSV(ToCSVOptions.OrderBy)
           .Dump("ttrIdList.ToCSV(ToCSVOptions.OrderBy)", 0);

  ttrIdList.ToCSV(ToCSVOptions.OrderByDescending)
           .Dump("ttrIdList.ToCSV(ToCSVOptions.OrderByDescending)", 0);

  ttrIdList.ToCSV(ToCSVOptions.Distinct | ToCSVOptions.OrderBy)
           .Dump("ttrIdList.ToCSV(ToCSVOptions.Distinct & ToCSVOptions.OrderBy)", 0);

  ttrIdList.ToCSV(ToCSVOptions.Distinct | ToCSVOptions.OrderByDescending)
           .Dump("ttrIdList.ToCSV(ToCSVOptions.Distinct & ToCSVOptions.OrderByDescending)", 0);
  
  ttrIdList.ToCSV(":")
           .Dump("ttrIdList.ToCSV(':')", 0);

  ttrIdList.ToCSV(":", ToCSVOptions.Distinct)
           .Dump("ttrIdList.ToCSV(':', ToCSVOptions.Distinct)", 0);

  ttrIdList.ToCSV(":", ToCSVOptions.OrderBy)
           .Dump("ttrIdList.ToCSV(':', ToCSVOptions.OrderBy)", 0);

  ttrIdList.ToCSV(":", ToCSVOptions.OrderByDescending)
           .Dump("ttrIdList.ToCSV(':', ToCSVOptions.OrderByDescending)", 0);

  ttrIdList.ToCSV(":", ToCSVOptions.Distinct | ToCSVOptions.OrderBy)
           .Dump("ttrIdList.ToCSV(':', ToCSVOptions.Distinct & ToCSVOptions.OrderBy)", 0);

  ttrIdList.ToCSV(":", ToCSVOptions.Distinct | ToCSVOptions.OrderByDescending)
           .Dump("ttrIdList.ToCSV(':', ToCSVOptions.Distinct & ToCSVOptions.OrderByDescending)", 0);

  #endregion
}

[Flags]
public enum ToCSVOptions
{
  None              = 0
 ,Distinct          = 1
 ,OrderBy           = 2
 ,OrderByDescending = 4
}

public static class MyExtensions
{
  public static string ToCSV<T>(this IEnumerable<T> source )
  {
    return source.ToCSV<T>(ToCSVOptions.None);
  }
  
  public static string ToCSV<T>(this IEnumerable<T> source, ToCSVOptions options)
  {
    return source.ToCSV<T>(",", options);
  }

  public static string ToCSV<T>(this IEnumerable<T> source, string separator)
  {
    return source.ToCSV<T>(separator, ToCSVOptions.None);
  }

  public static string ToCSV<T>(this IEnumerable<T> source, string separator, ToCSVOptions options)
  {
    foreach(Enum flag in options.GetFlags())
    {
      source = source.ToCSV<T>(flag);
    }
    
    return source == null 
                   ? null 
                   : string.Join(separator, source);
  }
  
  private static IEnumerable<T> ToCSV<T>(this IEnumerable<T> source, Enum options)
  {
    switch(options)
    {
      case ToCSVOptions.Distinct:
        return source?.Distinct();
      
      case ToCSVOptions.OrderBy:
        return source?.OrderBy(o => o);
        
      case ToCSVOptions.OrderByDescending:
        return source?.OrderByDescending(o => o);
      
      case ToCSVOptions.None:
      default:
        return source;
    }
  }

  public static IEnumerable<Enum> GetFlags(this Enum e)
  {
    return Enum.GetValues(e.GetType())
               .Cast<Enum>()
               .Where(v => !Equals((int)(object)v, 0)
                        && e.HasFlag(v));
  }
}
