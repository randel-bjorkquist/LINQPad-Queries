<Query Kind="Program" />

void Main()
{
  Data.UIDs.Dump("Data.UIDs");
  Data.UIDs.HasItems().Dump("Data.UIDs.HasItems()");
  Data.UIDs.IsNullOrEmpty().Dump("Data.UIDs.IsNullOrEmpty()");
}

public static class Data
{
//  public static IEnumerable<string> UIDs => new List<string>();
//  public static IEnumerable<string> UIDs => new List<string> {"1", "2"};
  public static IEnumerable<string> UIDs => new List<string> {"1"};
//  public static IEnumerable<int> UIDs => new List<int> {1, 2};
//  public static IEnumerable<string> UIDs => Enumerable.Empty<string>();
//  public static IEnumerable<string> UIDs => null;
}

public static class EnumerableExtensions
{
  //This can be used in place of source.Any() and is similar to string.IsNullOrEmpty()
  public static bool HasItems<T>(this IEnumerable<T> source)
  {
    return source?.Any() ?? false;
  }

  public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
  {
    return source == null || !source.Any();
  }
}