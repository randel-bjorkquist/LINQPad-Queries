<Query Kind="Program">
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

//NOTE: In this scenario, multiple threads increment the counter simultaneously,
//      causing lost increments and inconsistent results.
void Main()
{
  WithRaceCondition();
  FixedWithLock();
}

public void WithRaceCondition()
{
  int counter = 0;

  Parallel.For(0, 1000, i => { 
    counter++; // IMPORTANT: multiple threads writing at the same time
  });
  
  Console.WriteLine(counter); // OUTPUT: unpredictable results
}

public void FixedWithLock()
{
  int counter = 0;
  object locker = new object();

  Parallel.For(0, 1000, i => {
    //NOTE: By introducing synchronizattion (here with lock), we ensure that only 
    //      one thread can access the shared resource at a time.
    lock(locker)
      counter++;  // NOTE: safe access
  });
  
  Console.WriteLine(counter); // OUTPUT: always 1000
}