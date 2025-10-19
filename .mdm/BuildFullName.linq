<Query Kind="Program" />

void Main()
{
  string value = BuildFullName(null, null, null);
  Console.WriteLine($"BuildFullName(null, null, null) = '{value}' ");
  Console.WriteLine();

  value = BuildFullName("first_name", null, null);
  Console.WriteLine($"BuildFullName(\"first_name\", null, null) = '{value}' ");

  value = BuildFullName(null, "middle_name", null);
  Console.WriteLine($"BuildFullName(null, \"middle_name\", null) = '{value}' ");

  value = BuildFullName(null, null, "last_name");
  Console.WriteLine($"BuildFullName(null, null, \"last_name\") = '{value}' ");
  Console.WriteLine();

  value = BuildFullName("first_name", "middle_name", null);
  Console.WriteLine($"BuildFullName(\"first_name\", \"middle_name\", null) = '{value}' ");

  value = BuildFullName("first_name", null, "last_name");
  Console.WriteLine($"BuildFullName(\"first_name\", null, \"last_name\") = '{value}' ");
  Console.WriteLine();

  value = BuildFullName(null, "middle_name", "last_name");
  Console.WriteLine($"BuildFullName(null, \"middle_name\", \"last_name\") = '{value}' ");
  Console.WriteLine();

  value = BuildFullName("first_name", "middle_name", "last_name");
  Console.WriteLine($"BuildFullName(\"first_name\", \"middle_name\", \"last_name\") = '{value}' ");
}

private string BuildFullName(string first_name, string middle_name, string last_name)
{
  #region StringBuilder (commented out)
  
  //var full_name = new StringBuilder();
  
  #region Option 1 (commented out)
  //
  //full_name.Append(!string.IsNullOrWhiteSpace(first_name)  ? first_name.Trim()   + " " : "" );
  //full_name.Append(!string.IsNullOrWhiteSpace(middle_name) ? middle_name.Trim()  + " " : "" );
  //full_name.Append(!string.IsNullOrWhiteSpace(last_name)   ? last_name.Trim()          : "" );
  //
  #endregion Option 1
  
  #region Option 2 (commented out)
  //
  //if(!string.IsNullOrWhiteSpace(first_name))
  //  full_name.Append(first_name.Trim());
  //
  //if(!string.IsNullOrWhiteSpace(middle_name))
  //  full_name.Append(middle_name.Trim());
  //
  //if(!string.IsNullOrWhiteSpace(last_name))
  //  full_name.Append(last_name.Trim());
  //
  #endregion Option 2
  
  //return full_name.ToString();
  
  #endregion
  
  #region Inline string concant
  
  return ((!string.IsNullOrWhiteSpace(first_name)  ? first_name.Trim()   + " " : "" )  +
          (!string.IsNullOrWhiteSpace(middle_name) ? middle_name.Trim()  + " " : "" )  +
          (!string.IsNullOrWhiteSpace(last_name)   ? last_name.Trim()          : "" )).Trim();

  #endregion
}

