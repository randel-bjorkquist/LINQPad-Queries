<Query Kind="Program" />

void Main()
{
  var date = GetDateSince1970(null);
  Console.WriteLine($"GetDateSince1970(null) = '{date ?? "null"}'");

  date = GetDateSince1970("joe");
  Console.WriteLine($"GetDateSince1970(\"joe\") = '{date ?? "null"}'");

  date = GetDateSince1970("14592");
  Console.WriteLine($"GetDateSince1970(\"14592\") = '{date ?? "null"}'");
  Console.WriteLine();
}

private string GetDateSince1970(string daysSince1970)
{
  if(int.TryParse(daysSince1970, out int days))
  {
    return new DateTime(1970, 1, 1).AddDays(days)
                                   .ToShortDateString();
  }
  
  return daysSince1970;
}

