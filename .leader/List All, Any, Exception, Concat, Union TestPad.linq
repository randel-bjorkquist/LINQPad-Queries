<Query Kind="Program" />

void Main()
{
  List<int> longlist  = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
  List<int> shortlist = new List<int> { 0, 2, 4, 6, 8, 10 };

  List<int> unionlist = longlist.Union(shortlist).ToList();

  unionlist.Dump("unionlist", 0);

  List<int> concatlist = longlist.Concat(shortlist).ToList();
  concatlist.Dump("concatlist", 0);

  //var x = longlist.Select( i => i )
  //                .Except( shortlist );
  //                 
  //var z = shortlist.Select( i => i )
  //                 .Except( longlist );

  var x = longlist.Except(shortlist);

  var z = shortlist.Except(longlist);

  Console.WriteLine("long list 'except':");
  Console.WriteLine(string.Join(", "
                                 , x.Select(i => i)
                                   .OrderBy(o => o)));

  Console.WriteLine("");
  Console.WriteLine("short list 'except':");
  Console.WriteLine(string.Join(", "
                                 , z.Select(i => i)
                                   .OrderBy(o => o)));

  shortlist.All(i => longlist.Contains(i))
           .Dump("short list 'all' long list:");

  longlist.All(i => shortlist.Contains(i))
          .Dump("long list 'all' short list:");

  unionlist.All(i => shortlist.Contains(i))
           .Dump("union list 'all' short list:", 0);
  
  unionlist.All(i => longlist.Contains(i))
           .Dump("union list 'all' long list:", 0);
  
  shortlist.All(i => unionlist.Contains(i))
           .Dump("short list 'all' union list:", 0);

  longlist.All(i => unionlist.Contains(i))
          .Dump("long list 'all' union list:", 0);

  shortlist.Any(i => longlist.Contains(i))
           .Dump("short list 'any':");

  longlist.Any(i => shortlist.Contains(i))
          .Dump("long list 'any':");

  var a = longlist.Select(i => i)
                  .Intersect(shortlist);

  Console.WriteLine("");
  Console.WriteLine("long list 'intersect':");
  Console.WriteLine(string.Join(", "
                                 , a.Select(i => i)
                                   .OrderBy(o => o)));

  var b = shortlist.Select(i => i)
                   .Intersect(longlist);

  Console.WriteLine("");
  Console.WriteLine("short list 'intersect':");
  Console.WriteLine(string.Join(", "
                                 , b.Select(i => i)
                                   .OrderBy(o => o)));
}
