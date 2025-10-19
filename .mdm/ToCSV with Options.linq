<Query Kind="Program" />

void Main()
{
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

  #region ID Lists (int)

  IEnumerable<int> IDs = new List<int> { 1, 2, 3, 4, 5, 5, 3, 1, 0 };
  IDs.Dump("IDs", 0);

  IDs.Distinct()
     .Order()
     .ToCSV()
     .Dump("IDs.Distinct().Order().ToCSV()");

  IDs.Distinct()
     .OrderDescending()
     .ToCSV()
     .Dump("IDs.Distinct().OrderDescending().ToCSV()");

  IDs.ToCSV(",", ToCSVOptions.Distinct | ToCSVOptions.Order)
     .Dump("IDs.ToCSV(\",\", ToCSVOptions.Distinct | ToCSVOptions.Order)");

  IDs.ToCSV(",", ToCSVOptions.Distinct | ToCSVOptions.OrderBy)
     .Dump("IDs.ToCSV(\",\", ToCSVOptions.Distinct | ToCSVOptions.OrderBy)");

  IDs.ToCSV(",", ToCSVOptions.Distinct | ToCSVOptions.OrderDescending)
     .Dump("IDs.ToCSV(\",\", ToCSVOptions.Distinct | ToCSVOptions.OrderDescending)");

  IDs.ToCSV(",", ToCSVOptions.Distinct | ToCSVOptions.OrderByDescending)
     .Dump("IDs.ToCSV(\",\", ToCSVOptions.Distinct | ToCSVOptions.OrderByDescending)");

  //These are examples of of converting a list of IDs into a string to be used
  //in a SQL IN statement ... WHERE Employee.ID IN ( comma separated list, bracketed with single-quotes )
  var sql_where_statements = new List<string>();
  
  sql_where_statements.Add($"WHERE Employee.ID IN ('{IDs.ToCSV("','")}')");

  sql_where_statements.Add($"WHERE Employee.ID IN ('{IDs.Distinct().ToCSV("','")}')");
  sql_where_statements.Add($"WHERE Employee.ID IN ('{IDs.ToCSV("','", ToCSVOptions.Distinct)}')");

  //ERROR: DO the OrderBy on the list of ints before converting to csv ...
  sql_where_statements.Add($"WHERE Employee.ID IN ('{IDs.OrderBy(o => o).ToCSV("','")}')");
  sql_where_statements.Add($"WHERE Employee.ID IN ('{IDs.ToCSV("','", ToCSVOptions.OrderBy)}')");

  //ERROR: DO the OrderBy on the list of ints before converting to csv ...
  sql_where_statements.Add($"WHERE Employee.ID IN ('{IDs.Distinct().OrderBy(o => o).ToCSV("','")}')");
  sql_where_statements.Add($"WHERE Employee.ID IN ('{IDs.ToCSV("','", ToCSVOptions.Distinct | ToCSVOptions.OrderBy)}')");

  sql_where_statements.Dump("SQL WHERE Statements", 0);

  #endregion

  #region UID Lists (strings)

  IEnumerable<string> UIDs = new List<string> { "001", "002", "003", "004", "005", "005", "003", "001", "000" };
  UIDs.Dump("UIDs", 0);

  UIDs.Distinct()
      .Order()
      .ToCSV()
      .Dump("UIDs.Distinct().Order().ToCSV()");

  UIDs.Distinct()
      .OrderDescending()
      .ToCSV()
      .Dump("UIDs.Distinct().OrderDescending().ToCSV()");

  UIDs.ToCSV(",", ToCSVOptions.Distinct | ToCSVOptions.Order)
      .Dump("UIDs.ToCSV(\",\", ToCSVOptions.Distinct | ToCSVOptions.Order)");

  UIDs.ToCSV(",", ToCSVOptions.Distinct | ToCSVOptions.OrderBy)
      .Dump("UIDs.ToCSV(\",\", ToCSVOptions.Distinct | ToCSVOptions.OrderBy)");

  UIDs.ToCSV(",", ToCSVOptions.Distinct | ToCSVOptions.OrderDescending)
      .Dump("UIDs.ToCSV(\",\", ToCSVOptions.Distinct | ToCSVOptions.OrderDescending)");

  UIDs.ToCSV(",", ToCSVOptions.Distinct | ToCSVOptions.OrderByDescending)
      .Dump("UIDs.ToCSV(\",\", ToCSVOptions.Distinct | ToCSVOptions.OrderByDescending)");

  //These are examples of of converting a list of IDs into a string to be used
  //in a SQL IN statement ... WHERE Employee.ID IN ( comma separated list, bracketed with single-quotes )
  //var sql_where_statements = new List<string>();
  sql_where_statements = new List<string>();

  sql_where_statements.Add($"WHERE Employee.ID IN ('{UIDs.ToCSV("','")}')");

  sql_where_statements.Add($"WHERE Employee.ID IN ('{UIDs.Distinct().ToCSV("','")}')");
  sql_where_statements.Add($"WHERE Employee.ID IN ('{UIDs.ToCSV("','", ToCSVOptions.Distinct)}')");

  sql_where_statements.Add($"WHERE Employee.ID IN ('{UIDs.OrderBy(o => o).ToCSV("','")}')");
  sql_where_statements.Add($"WHERE Employee.ID IN ('{UIDs.ToCSV("','", ToCSVOptions.OrderBy)}')");

  sql_where_statements.Add($"WHERE Employee.ID IN ('{UIDs.Distinct().OrderBy(o => o).ToCSV("','")}')");
  sql_where_statements.Add($"WHERE Employee.ID IN ('{UIDs.ToCSV("','", ToCSVOptions.Distinct | ToCSVOptions.OrderBy)}')");

  sql_where_statements.Dump("SQL WHERE Statements", 0);

  #endregion
}

[Flags]
public enum ToCSVOptions
{
  None              =  0
 ,Distinct          =  1
 ,Order             =  2
 ,OrderBy           =  4
 ,OrderDescending   =  8
 ,OrderByDescending = 16
}

public static class EnumerableExtensions
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
      
      case ToCSVOptions.Order:
        return source?.Order();
      
      case ToCSVOptions.OrderBy:
        return source?.OrderBy(o => o);
        
      case ToCSVOptions.OrderDescending:
        return source?.OrderDescending();
        
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
