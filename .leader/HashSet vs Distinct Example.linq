<Query Kind="Program" />

//RESOURCE URL: 
// => https://stackoverflow.com/questions/6298679/whats-better-for-creating-distinct-data-structures-hashset-or-linqs-distinct

void Main()
{
  #region Data Setup
  
  HashSet<int> hash = new HashSet<int>();
  List<int>    list = new List<int>();

  var d = Enumerable.Range(1, 100)
                    .ToList()
                    .Repeat(100);
  
  #endregion                    
  
  #region Build Method(s)
  
  // ---------------------------------------------------------------------
  Benchmark(() => { 
    hash.Clear();
    
    foreach(var item in d) 
    {
      hash.Add(item);
    }
  });
  
  // ---------------------------------------------------------------------
  Benchmark(() => {
    list.Clear();
                    
    foreach (var item in d)
    {
      list.Add(item);
    }
  
    list = list.Distinct()
               .ToList(); 
  });
  
  // ---------------------------------------------------------------------
  Benchmark(() => {
    hash.Clear();
    
    foreach(var item in d)
    {
      hash.Add(item);
    }
  
    list = hash.ToList();
  });
  
  
  // ---------------------------------------------------------------------
  Benchmark(() => {
    hash.Clear();
    hash = d.ToHashSet();
  });
  #endregion
  
  #region Removing Duplicates
  
  // ---------------------------------------------------------------------
  //Benchmark(() =>{
  //  hash = new HashSet<int>(d);
  //});
  
  // ---------------------------------------------------------------------  
  //Benchmark(() => {
  //    list = d.Distinct().ToList();
  //});
  
  #endregion
}


public static void Benchmark(Action method, int iterations = 10000)
{
  Stopwatch sw = new Stopwatch();
  sw.Start();

  for (int i = 0; i < iterations; i++)
  {
    method();
  }

  sw.Stop();
  sw.Elapsed
    .TotalMilliseconds
    .ToString()
    .Dump();
}

public static class CollectionExtensions
{
  public static List<T> Repeat<T>(this ICollection<T> lst, int count)
  {
    if (count < 0)
    {
      throw new ArgumentOutOfRangeException("count");
    }

    var ret = Enumerable.Empty<T>();

    for (var i = 0; i < count; i++)
    {
      ret = ret.Concat(lst);
    }

    return ret.ToList();
  }
}

