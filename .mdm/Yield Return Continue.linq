<Query Kind="Program" />

void Main()
{
  //Data.GetGUIs().Dump("GetGUIs()");
  //
  //Data.GetAllNumbers().Dump("GetAllNumbers()");
  //Data.GetEvenNumbers().Dump("GetEvenNumbers()");
  //Data.GetOddNumbers().Dump("GetOddNumbers()");
  
  Data.GetAll(yield_break: false).Dump("GetAll(yield_break: false)", 0);
  Data.GetAll(yield_break: true).Dump("GetAll(yield_break: true)", 0);
}

public static class Data
{
  private static readonly List<int> numbers
    = new List<int> {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10};

  private static readonly List<string> guids_2_exclude 
    = new List<string> { "GUID-0004", "GUID-0005", "GUID-0006" };
  
  private static IEnumerable<string> GUIs()
  {
    yield return "GUID-0001";
    yield return "GUID-0002";
    yield return "GUID-0003";
    yield return "GUID-0004";
    yield return "GUID-0005";
    yield return "GUID-0006";
    yield return "GUID-0007";
    yield return "GUID-0008"; 
    yield return "GUID-0009";    
  }
  
  public static IEnumerable<string> GetGUIs()
  {
    foreach(var guid in Data.GUIs())
    {
      if(guids_2_exclude.Contains(guid))
        continue;

      yield return guid;
    }
  }
  
  public enum NumberType
  {
     All  = 0
    ,Even = 1
    ,Odd  = 2
  }

  public static IEnumerable<int> GetAllNumbers()
    => GetNumbers(NumberType.All);

  public static IEnumerable<int> GetEvenNumbers()
    => GetNumbers(NumberType.Even);

  public static IEnumerable<int> GetOddNumbers()
    => GetNumbers(NumberType.Odd);

  private static IEnumerable<int> GetNumbers(NumberType type = NumberType.All)
  {
    foreach (var number in numbers)
    {
      switch(type)
      {
        case NumberType.All:                                                    break;        
        case NumberType.Even: if (number % 2 != 0 || number == 0) { continue; } break;
        case NumberType.Odd:  if (number % 2 != 1)                { continue; } break;

        //case NumberType.All:                                      { yield return number; } break;
        //case NumberType.Even: if (number % 2 == 0 && number != 0) { yield return number; } break;
        //case NumberType.Odd:  if (number % 2 == 1)                { yield return number; } break;
      }
      
      yield return number;
    }      
  }
  
  public static IEnumerable<object> GetAll(bool yield_break)
  {
      yield return GetGUIs();
      yield return GetAllNumbers();

    if(yield_break)
    {
      yield break;
    }
      
      yield return GetEvenNumbers();
      yield return GetOddNumbers();
  }
}