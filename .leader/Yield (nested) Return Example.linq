<Query Kind="Program" />

void Main()
{
  Numbers(0, 10).Dump();
  Numbers(10).Dump();
}


public IEnumerable<int> Numbers(int maxValue = int.MaxValue)
{
  return Numbers(0, maxValue);
}

public IEnumerable<int> Numbers(int minValue = 0, int maxValue = int.MaxValue)
{
  var random = new Random();
  var count  = random.Next(minValue, maxValue);
  
  if(count == 0)
  {
    yield break;
  }
  
  for(int i = 0; i < count; i++)
  {
    yield return i;
  }
}
