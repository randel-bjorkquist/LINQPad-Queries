<Query Kind="Program" />

void Main()
{
  #region Join & ToCSV - default separator ',' on a null collection variable
  
  "Forcing calls to both Join & ToCSV on a null collection variable ...".Dump();
  //NOTE: ToList, Distinct, Order, OrderDescending all require @null to NOT BE null
  
  IEnumerable<int> @null = null;

  @null.Join()                //DO NOT use the ? opporator, it'll skip the call to Join()
       .Dump("@null.Join()");
  
  @null.ToCSV()               //DO NOT use the ? opporator, it'll skip the call to ToCSV()
       .Dump("@null.ToCSV()");

  #endregion

  #region Join & ToCSV - default separator ',' on an empty collection variable

  "Forcing calls to both Join & ToCSV on an empty collection variable ...".Dump();

  //NOTE: ToList, Distinct, Order, OrderDescending DO NOTHING on an empty collection

  IEnumerable<int> ids = [];
  
  ids.Join()
     .Dump("ids.Join()", 0);

  ids.ToCSV()
     .Dump("ids.ToCSV()", 0);

  #endregion

  IEnumerable<int> IDs = new List<int> { 1, 2, 3, 4, 5, 5, 3, 1, 0 };
  
  "variable IDs ...".Dump();
  
  IDs.ToList()
     .Dump("ID List", 0);

  #region Join - default separator ','
  
  "Using the default separator ',' as the delimeter, no leading or trailing spaces for padding ...".Dump();
  
  // using the default, comma ',' delimeter with no spaces for padding ...
  IDs.Join()
     .Dump("IDs.Join()", 0);

  IDs.Distinct()
     .Join()
     .Dump("IDs.Distinct().Join()", 0);

  IDs.Order()
     .Join()
     .Dump("IDs.Order().Join()", 0);

  IDs.OrderDescending()
     .Join()
     .Dump("IDs.OrderDescending().Join()", 0);

  #endregion

  #region Join - custom separator, comma space ", "
  
  "".Dump();
  "Using the string comma-space \", \" as the delimeter, only a trailing spaces for padding ...".Dump();

  IDs.Join(", ")
     .Dump("IDs.Join(\", \")", 0);

  IDs.Distinct()
     .Join(", ")
     .Dump("IDs.Distinct().Join(\", \")", 0);

  IDs.Order()
     .Join(", ")
     .Dump("IDs.Order().Join(\", \")", 0);

  IDs.OrderDescending()
     .Join(", ")
     .Dump("IDs.OrderDescending().Join(\", \")", 0);
  
  #endregion
  
  #region Join - custom separator, pipe '|'
  
  "".Dump();
  "Using a pipe character '|' as the delimeter, no spaces for padding ...".Dump();

  IDs.Join('|')
     .Dump("IDs.Join('|')", 0);

  IDs.Distinct()
     .Join('|')
     .Dump("IDs.Distinct().Join('|')", 0);

  IDs.Order()
     .Join('|')
     .Dump("IDs.Order().Join('|')", 0);

  IDs.OrderDescending()
     .Join('|')
     .Dump("IDs.OrderDescending().Join('|')", 0);

  #endregion
  
  #region Join - custom separator, tab '\t'
  
  "".Dump();
  "Using a tab character '\\t' as the delimeter, no spaces for padding ...".Dump();
  
  IDs.Join('\t')
     .Dump("IDs.Join(\"\\t\")", 0);

  IDs.Distinct()
     .Join('\t')
     .Dump("IDs.Distinct().Join('\\t)", 0);

  IDs.Order()
     .Join('\t')
     .Dump("IDs.Order().Join('\\t)", 0);

  IDs.OrderDescending()
     .Join('\t')
     .Dump("IDs.OrderDescending().Join('\\t')", 0);
  
  #endregion
  
  #region Join - custom separator, " & "
  
  "".Dump();
  "Using the string string \" & \" as the delimeter, with leading and trailing spaces for padding ...".Dump();
  
  IDs.Join(" & ")
     .Dump("IDs.Join(\" & \")", 0);

  IDs.Distinct()
     .Join(" & ")
     .Dump("IDs.Distinct().Join(\" & \")", 0);

  IDs.Order()
     .Join(" & ")
     .Dump("IDs.Order().Join(\" & \")", 0);

  IDs.OrderDescending()
     .Join(" & ")
     .Dump("IDs.OrderDescending().Join(\" & \")", 0);
      
  #endregion
  
  #region ToCSV - default separator ','
  
  "Using the default separator ',' as the delimeter, no leading or trailing spaces for padding ...".Dump();
  
  // using the default, comma ',' delimeter with no spaces for padding ...
  IDs.ToList()
     .Dump("ID List", 0);

  IDs.ToCSV()
     .Dump("IDs.ToCSV()", 0);

  IDs.Distinct()
     .ToCSV()
     .Dump("IDs.Distinct().ToCSV()", 0);

  IDs.Order()
     .ToCSV()
     .Dump("IDs.Order().ToCSV()", 0);

  IDs.OrderDescending()
     .ToCSV()
     .Dump("IDs.OrderDescending().ToCSV()", 0);

  #endregion

  #region ToCSV - custom separator, comma space ", "
  
  "".Dump();
  "Using the string comma-space \", \" as the delimeter, only a trailing spaces for padding ...".Dump();

  IDs.ToCSV(", ")
     .Dump("IDs.ToCSV(\", \")", 0);

  IDs.Distinct()
     .ToCSV(", ")
     .Dump("IDs.Distinct().ToCSV(\", \")", 0);

  IDs.Order()
     .ToCSV(", ")
     .Dump("IDs.Order().ToCSV(\", \")", 0);

  IDs.OrderDescending()
     .ToCSV(", ")
     .Dump("IDs.OrderDescending().ToCSV(\", \")", 0);
  
  #endregion
  
  #region ToCSV - custom separator, pipe '|'
  
  "".Dump();
  "Using a pipe character '|' as the delimeter, no spaces for padding ...".Dump();

  IDs.ToCSV('|')
     .Dump("IDs.ToCSV('|')", 0);

  IDs.Distinct()
     .ToCSV('|')
     .Dump("IDs.Distinct().ToCSV('|')", 0);

  IDs.Order()
     .ToCSV('|')
     .Dump("IDs.Order().ToCSV('|')", 0);

  IDs.OrderDescending()
     .ToCSV('|')
     .Dump("IDs.OrderDescending().ToCSV('|')", 0);

  #endregion
  
  #region ToCSV - custom separator, tab '\t'
  
  "".Dump();
  "Using a tab character '\\t' as the delimeter, no spaces for padding ...".Dump();
  
  IDs.ToCSV('\t')
     .Dump("IDs.ToCSV(\"\\t\")", 0);

  IDs.Distinct()
     .ToCSV('\t')
     .Dump("IDs.Distinct().ToCSV('\\t)", 0);

  IDs.Order()
     .ToCSV('\t')
     .Dump("IDs.Order().ToCSV('\\t)", 0);

  IDs.OrderDescending()
     .ToCSV('\t')
     .Dump("IDs.OrderDescending().ToCSV('\\t')", 0);
  
  #endregion
  
  #region ToCSV - custom separator, " & "
  
  "".Dump();
  "Using the string string \" & \" as the delimeter, with leading and trailing spaces for padding ...".Dump();
  
  IDs.ToCSV(" & ")
     .Dump("IDs.ToCSV(\" & \")", 0);

  IDs.Distinct()
     .ToCSV(" & ")
     .Dump("IDs.Distinct().ToCSV(\" & \")", 0);

  IDs.Order()
     .ToCSV(" & ")
     .Dump("IDs.Order().ToCSV(\" & \")", 0);

  IDs.OrderDescending()
     .ToCSV(" & ")
     .Dump("IDs.OrderDescending().ToCSV(\" & \")", 0);
      
  #endregion
}

public static class EnumerableExtensions
{
  public static string? Join<T>(this IEnumerable<T> source)
    => source.Join<T>(',');
  
  public static string? Join<T>(this IEnumerable<T> source, char separator) 
    => source is null ? null 
                      : string.Join(separator, source);
    
  public static string? Join<T>(this IEnumerable<T> source, string separator)
    => source is null ? null 
                      : string.Join(separator, source);

  public static string? ToCSV<T>(this IEnumerable<T> source)    
    => Join<T>(source);

  public static string? ToCSV<T>(this IEnumerable<T> source, char separator)
    => Join<T>(source, separator);  
    
  public static string? ToCSV<T>(this IEnumerable<T> source, string separator) 
    => Join<T>(source, separator);
}
