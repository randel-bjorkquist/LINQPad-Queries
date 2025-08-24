<Query Kind="Program">
  <NuGetReference>BenchmarkDotNet</NuGetReference>
  <Namespace>BenchmarkDotNet.Attributes</Namespace>
  <Namespace>BenchmarkDotNet.Running</Namespace>
  <RuntimeVersion>8.0</RuntimeVersion>
</Query>

/*******************************************************************************************************************************************
//NOTE: the way to fix the "shadow copy" issue, is to build the BenchmarkDotNet into a DLL
// Validating benchmarks:
//  * Assembly LINQPadQuery, Version=1.0.0.805, Culture=neutral, PublicKeyToken=null is located in temp. If you are running benchmarks 
      from xUnit you need to disable shadow copy. It's not supported by design.

//  * Assembly LINQPadQuery which defines benchmarks is non-optimized
      Benchmark was built without optimization enabled (most probably a DEBUG configuration). Please, build it in RELEASE.
      If you want to debug the benchmarks, please see https://benchmarkdotnet.org/articles/guides/troubleshooting.html#debugging-benchmarks.
      Please enable optimizations in your LINQPad. Go to Preferences -> Query and select "compile with /optimize+"

//NOTE: Alternative to changing the LINQPad environment setting, is to add the line #LINQPad optimize+ //release mode below the comment
//      block to force this LINQPad Query to run in "release mode"; add the line #LINQPad optimize- //debug mode blow the comment block
//      to force it into "debug mode" ...
//        #LINQPad optimize- //debug mode
//        #LINQPad optimize+ //release mode
*******************************************************************************************************************************************/
//#LINQPad optimize- //debug mode
//#LINQPad optimize+ //release mode

void Main()
{
  var summary = BenchmarkRunner.Run<EvenNumbersBenchmark>();
  summary.Dump("BenchmarkDotNet Report Summary", 0);
}

[MemoryDiagnoser]
public class EvenNumbersBenchmark
{
  //[Params(100, 1000, 10000)]
  [Params(100)]
  public int Max { get; set; }
  
  [Benchmark(Baseline = true)]
  public void GetEvenNumbers()
  {
    var numbers = GetEvenNumbers(Max);
    
    // NOTE: Force enumeration
    foreach (var number in numbers) {}
  }

  [Benchmark]
  public void GetEvenNumbersEfficiently()
  {
    var numbers = GetEvenNumbersEfficiently(Max);

    // NOTE: Force enumeration
    foreach (var number in numbers) { }
  }

  public IEnumerable<int> GetEvenNumbers(int max)
  {
    var numbers = new List<int>();

    for(int index = 0; index <= max; index++)
    {
      if(index % 2 == 0)
      {
        numbers.Add(item: index);
      }
    }
    
    return numbers;
  }

  public IEnumerable<int> GetEvenNumbersEfficiently(int max)
  {
    for(int index = 0; index <= max; index++)
    {
      if(index % 2 == 0)
      {
        yield return index;
      }
    }
  }  
}
