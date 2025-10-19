<Query Kind="Program" />

void Main()
{
  Numbers(0).Dump("Numbers(0)");
  Numbers(1).Dump("Numbers(1)");
  Numbers(9).Dump("Numbers(9)");
  
  //-----------------------------------------------
  Console.WriteLine();
  
  foreach(int i in Numbers(10).Take(3))
  {
    $"Number {i}".Dump();
  }
  
  //-----------------------------------------------
  Console.WriteLine();
  
  foreach(int i in Numbers_1_through_10().Take(3))
  {
    $"Number {i}".Dump();
  }  
}

public static IEnumerable<int> Numbers(int max)
{
  if(max == 0)
  {
    yield break;
  }
  
  for(int i = 0; i < max; i++) 
  {
    $"Returning {i}".Dump();
    yield return i;
  }
}

public static IEnumerable<int> Numbers_1_through_10()
{
  $"Returning {1}".Dump();
  yield return 1;

  $"Returning {2}".Dump();
  yield return 2;

  $"Returning {3}".Dump();
  yield return 3;

  $"Returning {4}".Dump();
  yield return 4;

  $"Returning {5}".Dump();
  yield return 5;

  $"Returning {6}".Dump();
  yield return 6;

  $"Returning {7}".Dump();
  yield return 7;

  $"Returning {8}".Dump();
  yield return 8;

  $"Returning {9}".Dump();
  yield return 9;

  $"Returning {10}".Dump();
  yield return 10; 
}
