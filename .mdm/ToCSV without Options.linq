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

  //NOTE: I'm not a big fan of having to surround the expression with
  //      parenthesis ... but understand why I have to do it.
  (ttaIdList?.Distinct())
           .ToCSV()
           .Dump("(ttaIdList?.Distinct()).ToCSV()", 0);

  //NOTE: I DO NOT LIKE THIS ... it changes what 'ttaIdList' is, from 'null' 
  //      to an 'Enumerable.Empty<int>()' ... BUT, if the goal would be to
  //      remove the need to check for 'null', it might make sense ???
  (ttaIdList?.Distinct() ?? Enumerable.Empty<int>())
           .ToCSV()
           .Dump("(ttaIdList?.Distinct()).ToCSV()", 0);

//  ttaIdList.ToCSV(ToCSVOptions.Distinct)
//           .Dump("ttrIdList.ToCSV(ToCSVOptions.Distinct)", 0);
//
//  ttaIdList.ToCSV(ToCSVOptions.OrderBy)
//           .Dump("ttaIdList.ToCSV(ToCSVOptions.OrderBy)", 0);
//
//  ttaIdList.ToCSV(ToCSVOptions.OrderByDescending)
//           .Dump("ttaIdList.ToCSV(ToCSVOptions.OrderByDescending)", 0);
//
//  ttaIdList.ToCSV(ToCSVOptions.Distinct | ToCSVOptions.OrderBy)
//           .Dump("ttaIdList.ToCSV(ToCSVOptions.Distinct & ToCSVOptions.OrderBy)", 0);
//
//  ttaIdList.ToCSV(ToCSVOptions.Distinct | ToCSVOptions.OrderByDescending)
//           .Dump("ttaIdList.ToCSV(ToCSVOptions.Distinct & ToCSVOptions.OrderByDescending)", 0);
//  
//  ttaIdList.ToCSV(":")
//           .Dump("ttaIdList.ToCSV(':')", 0);
//
//  ttaIdList.ToCSV(":", ToCSVOptions.Distinct)
//           .Dump("ttrIdList.ToCSV(':', ToCSVOptions.Distinct)", 0);
//
//  ttaIdList.ToCSV(":", ToCSVOptions.OrderBy)
//           .Dump("ttaIdList.ToCSV(':', ToCSVOptions.OrderBy)", 0);
//
//  ttaIdList.ToCSV(":", ToCSVOptions.OrderByDescending)
//           .Dump("ttaIdList.ToCSV(':', ToCSVOptions.OrderByDescending)", 0);
//
//  ttaIdList.ToCSV(":", ToCSVOptions.Distinct | ToCSVOptions.OrderBy)
//           .Dump("ttaIdList.ToCSV(':', ToCSVOptions.Distinct & ToCSVOptions.OrderBy)", 0);
//
//  ttaIdList.ToCSV(":", ToCSVOptions.Distinct | ToCSVOptions.OrderByDescending)
//           .Dump("ttaIdList.ToCSV(':', ToCSVOptions.Distinct & ToCSVOptions.OrderByDescending)", 0);

  #endregion
  
  #region ttrIDs NOT NULL
  
  IEnumerable<int> ttrIDs = new List<int>{ 1, 2, 3, 4, 5, 5, 3, 1, 0 };
  ttrIDs.Dump("ttrIDs", 0);
  
  var ttrIdList = ttrIDs?.ToList();
  ttrIdList.Dump("ttrIdList", 0);
  
  ttrIdList.ToCSV()
           .Dump("ttrIdList.ToCSV()", 0);

  ttrIdList.Distinct()
           .ToCSV()
           .Dump("ttrIdList.Distinct().ToCSV()", 0);

  ttrIdList.Order()
           .ToCSV()
           .Dump("ttrIdList.Order().ToCSV()", 0);

  ttrIdList.OrderBy(o => o)
           .ToCSV()
           .Dump("ttrIdList.OrderBy(o => o).ToCSV()", 0);

  ttrIdList.OrderDescending()
           .ToCSV()
           .Dump("ttrIdList.OrderDescending().ToCSV()", 0);

  ttrIdList.OrderByDescending(o => o)
           .ToCSV()
           .Dump("ttrIdList.OrderByDescending(o => o).ToCSV()", 0);

  ttrIdList.Distinct()
           .Order()
           .ToCSV()
           .Dump("ttrIdList.Distinct().Order().ToCSV()", 0);

  ttrIdList.Distinct()
           .OrderDescending()
           .ToCSV()
           .Dump("ttrIdList.Distinct().OrderDescending().ToCSV()", 0);

  
  ttrIdList.ToCSV(":")
           .Dump("ttrIdList.ToCSV(\":\")", 0);

  ttrIdList.Distinct()
           .ToCSV(":")
           .Dump("ttrIdList.Distinct().ToCSV(\":\")", 0);

  ttrIdList.Order()
           .ToCSV(":")
           .Dump("ttrIdList.Order().ToCSV(\":\")", 0);

  ttrIdList.OrderDescending()
           .ToCSV(":")
           .Dump("ttrIdList.OrderDescending().ToCSV(\":\")", 0);

  ttrIdList.Distinct()
           .Order()
           .ToCSV(":")
           .Dump("ttrIdList.Distinct().Order().ToCSV(\":\")", 0);

  ttrIdList.Distinct()
           .OrderDescending()
           .ToCSV(":")
           .Dump("ttrIdList.Distinct().OrderDescending().ToCSV(\":\")", 0);

  
  ttrIdList.ToCSV("|")
           .Dump("ttrIdList.ToCSV(\"|\")", 0);

  ttrIdList.Distinct()
           .ToCSV("|")
           .Dump("ttrIdList.Distinct().ToCSV(\"|\")", 0);

  ttrIdList.Order()
           .ToCSV("|")
           .Dump("ttrIdList.Order().ToCSV(\"|\")", 0);

  ttrIdList.OrderDescending()
           .ToCSV("|")
           .Dump("ttrIdList.OrderDescending().ToCSV(\"|\")", 0);

  ttrIdList.Distinct()
           .Order()
           .ToCSV("|")
           .Dump("ttrIdList.Distinct().Order().ToCSV(\"|\")", 0);

  ttrIdList.Distinct()
           .OrderDescending()
           .ToCSV("|")
           .Dump("ttrIdList.Distinct().OrderDescending().ToCSV(\"|\")", 0);

  #endregion

  #region ID Lists (int)

  IEnumerable<int> IDs = new List<int> { 1, 2, 3, 4, 5, 5, 3, 1, 0 };
  IDs.Dump("IDs", 0);
  
  IDs.Distinct()
     .Order()
     .ToCSV()
     .Dump("IDs.Distinct().Order().ToCSV()");
  
  //These are examples of of converting a list of IDs into a string to be used
  //in a SQL IN statement ... WHERE Employee.ID IN ( comma separated list, bracketed with single-quotes )
  var sql_where_statements = new List<string>();
  
  sql_where_statements.Add($"WHERE Employee.ID IN ('{IDs.ToCSV("','")}')");
  sql_where_statements.Add($"WHERE Employee.ID IN ('{IDs.Distinct().ToCSV("','")}')");
  
  sql_where_statements.Add($"WHERE Employee.ID IN ('{IDs.Order().ToCSV("','")}')");
  sql_where_statements.Add($"WHERE Employee.ID IN ('{IDs.OrderBy(o => o).ToCSV("','")}')");
  
  sql_where_statements.Add($"WHERE Employee.ID IN ('{IDs.OrderDescending().ToCSV("','")}')");
  sql_where_statements.Add($"WHERE Employee.ID IN ('{IDs.OrderByDescending(o => o).ToCSV("','")}')");
  
  sql_where_statements.Add($"WHERE Employee.ID IN ('{IDs.Distinct().Order().ToCSV("','")}')");
  sql_where_statements.Add($"WHERE Employee.ID IN ('{IDs.Distinct().OrderBy(o => o).ToCSV("','")}')");
  sql_where_statements.Add($"WHERE Employee.ID IN ('{IDs.Distinct().OrderDescending().ToCSV("','")}')");
  sql_where_statements.Add($"WHERE Employee.ID IN ('{IDs.Distinct().OrderByDescending(o => o).ToCSV("','")}')");

  sql_where_statements.Dump("SQL WHERE Statements", 0);
  
  #endregion

  #region UID Lists (strings)

  IEnumerable<string> UIDs = new List<string> { "001", "002", "003", "004", "005", "005", "003", "001", "000" };
  UIDs.Dump("UIDs", 0);

  UIDs.Distinct()
      .Order()
      .ToCSV()
      .Dump("UIDs.Distinct().Order().ToCSV()");

  //These are examples of of converting a list of IDs into a string to be used
  //in a SQL IN statement ... WHERE Employee.ID IN ( comma separated list, bracketed with single-quotes )
  //var sql_where_statements = new List<string>();
  sql_where_statements = new List<string>();

  sql_where_statements.Add($"WHERE Employee.ID IN ('{UIDs.ToCSV("','")}')");
  sql_where_statements.Add($"WHERE Employee.ID IN ('{UIDs.Distinct().ToCSV("','")}')");
  
  sql_where_statements.Add($"WHERE Employee.ID IN ('{UIDs.Order().ToCSV("','")}')");
  sql_where_statements.Add($"WHERE Employee.ID IN ('{UIDs.OrderBy(o => o).ToCSV("','")}')");
  
  sql_where_statements.Add($"WHERE Employee.ID IN ('{UIDs.OrderDescending().ToCSV("','")}')");
  sql_where_statements.Add($"WHERE Employee.ID IN ('{UIDs.OrderByDescending(o => o).ToCSV("','")}')");
  
  sql_where_statements.Add($"WHERE Employee.ID IN ('{UIDs.Distinct().Order().ToCSV("','")}')");
  sql_where_statements.Add($"WHERE Employee.ID IN ('{UIDs.Distinct().OrderBy(o => o).ToCSV("','")}')");
  sql_where_statements.Add($"WHERE Employee.ID IN ('{UIDs.Distinct().OrderDescending().ToCSV("','")}')");
  sql_where_statements.Add($"WHERE Employee.ID IN ('{UIDs.Distinct().OrderByDescending(o => o).ToCSV("','")}')");
  
  sql_where_statements.Dump("SQL WHERE Statements");
  
  #endregion
}

public static class EnumerableExtensions
{
  public static string ToCSV<T>(this IEnumerable<T> source )
  {
    return source.ToCSV<T>(",");
  }

  public static string ToCSV<T>(this IEnumerable<T> source, string separator)
  {
    return source == null 
                   ? null 
                   : string.Join(separator, source);

  }
}
