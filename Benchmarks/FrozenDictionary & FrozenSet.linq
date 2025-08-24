<Query Kind="Program">
  <NuGetReference>BenchmarkDotNet</NuGetReference>
  <Namespace>BenchmarkDotNet.Attributes</Namespace>
  <Namespace>BenchmarkDotNet.Running</Namespace>
  <Namespace>System.Collections.Frozen</Namespace>
  <Namespace>System.Collections.Immutable</Namespace>
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
#LINQPad optimize+ //release mode

void Main()
{
  var frozen_dictionary_summary = BenchmarkRunner.Run<FrozenDictionaryBenchmark>();
  //frozen_dictionary_summary.Dump("BenchmarkDotNet Report Summary (FrozenDictionary)", 0);
  
  var frozen_set_summary = BenchmarkRunner.Run<FrozenSetBenchmark>();
  //frozen_set_summary.Dump("BenchmarkDotNet Report Summary (FrozenSet)", 0);
}

[MemoryDiagnoser]
public class FrozenDictionaryBenchmark
{
  private Dictionary<int, int>? _dictionary                    { get; set; }
  private ImmutableDictionary<int, int>? _immutable_dictionary { get; set; }
  private FrozenDictionary<int, int>? _frozen_dictionary       { get; set; }
  
  [Benchmark]
  public void CreateDictionary()
  {
    _dictionary = Enumerable.Range(0, 100)
                            .ToDictionary(key => key);

    //Create Dictionary ...
    //Dictionary<int, int> frozen_dictionary = Enumerable.Range(0, 100)
    //                                                   .ToDictionary(key => key);

  }

  [Benchmark(Baseline = true)]
  public void CreateFrozenDictionary()
  {
    //Create FrozenDictionary ...
    _frozen_dictionary = Enumerable.Range(0, 100)
                                   .ToFrozenDictionary(key => key);

    //Create FrozenDictionary ...
    //FrozenDictionary<int, int> frozen_dictionary = Enumerable.Range(0, 100)
    //                                                         .ToFrozenDictionary(key => key);
    //
    // NOTE: Force enumeration
    //foreach (var number in frozen_dictionary) { }
  }

  [Benchmark]
  public void CreateImmutableDictionary()
  {
    _immutable_dictionary = Enumerable.Range(0, 100)
                                      .ToImmutableDictionary(key => key);

    //Create Dictionary ...
    //Dictionary<int, int> frozen_dictionary = Enumerable.Range(0, 100)
    //                                                   .ToDictionary(key => key);

  }

  [Benchmark]
  public void TryGetValueDictionary()
  {
    _dictionary?.TryGetValue(75, out int result);
  }

  [Benchmark]
  public void TryGetValueImmutableDictionary()
  {
    _immutable_dictionary?.TryGetValue(75, out int result);
  }

  [Benchmark]
  public void TryGetValueFrozenDictionary()
  {
    _frozen_dictionary?.TryGetValue(75, out int result);
  }
}

[MemoryDiagnoser]
public class FrozenSetBenchmark
{
  private List<int>? _list                           { get; set; }  
  
  private HashSet<int>? _hashset                     { get; set; }
  private ImmutableHashSet<int>? _immutable_hashset  { get; set; }
  
  private FrozenSet<int>? _frozen_set                { get; set; }
  
  [Benchmark]
  public void CreateList()
  {
    _list = Enumerable.Range(0, 100)
                      .ToList();
  }

  [Benchmark]
  public void CreateHashSet()
  {
    //Create HashSet ...
    _hashset = Enumerable.Range(0, 100)
                         .ToHashSet();
  }

  [Benchmark]
  public void CreateImmutableHashSet()
  {
    //Create HashSet ...
    _immutable_hashset = Enumerable.Range(0, 100)
                                   .ToImmutableHashSet();
  }

  [Benchmark(Baseline = true)]
  public void CreateFrozenSet()
  {
    //Create FrozenSet ...
    _frozen_set = Enumerable.Range(0, 100)
                            .ToFrozenSet();

    // NOTE: Force enumeration
    //foreach (var number in frozen_set) { }
  }

  [Benchmark]
  public void TryGetValueList()
  {
    _list?.Find(i => i == 75);
  }

  [Benchmark]
  public void TryGetValueHashSet()
  {
    _hashset?.TryGetValue(75, out int result);
  }

  [Benchmark]
  public void TryGetValueImmutableHashSet()
  {
    _immutable_hashset?.TryGetValue(75, out int result);
  }

  [Benchmark]
  public void TryGetValueFrozenSet()
  {
    _frozen_set?.TryGetValue(75, out int result);
  }
}














